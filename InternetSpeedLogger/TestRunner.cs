﻿/*
 * Copyright 2018 KevDever 
 * See LICENSE.md
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetSpeedLogger
{
    public class TestRunner : IDisposable
    {
        /// <summary>
        /// Invoked only when repeating tests are used with a non-zero MaxRuns (from Options)
        /// </summary>
        public event EventHandler TestsComplete;

        private readonly TestRunnerOptions _options;

        private int _runCounter = 0;
        private System.Timers.Timer _timer;
        private readonly Func<Task> Execute;
        private readonly bool _silent;

        public TestRunner(TestRunnerOptions options)
        {
            _options = options;
            _silent = options.Silent;

            if (_options.Frequency != default)
            {
                _timer = new System.Timers.Timer(_options.Frequency.TotalMilliseconds);
                _timer.AutoReset = true;
                _timer.Elapsed += async (_, __) =>
                {
                    if (_options.MaxRuns > 0 && ++_runCounter > _options.MaxRuns)
                    {
                        _timer.Stop();
                        TestsComplete?.Invoke(this, EventArgs.Empty);
                    }

                    await Execute().ConfigureAwait(false);
                };
            }

            if (_options.ResultRepository != null)
            {
                Execute = async () =>
                {
                    var model = await RunSpeedtest().ConfigureAwait(false);

                    if (model is null)
                        return;

                    await _options.ResultRepository.AddResult(model);
                    if (!_silent)
                        Console.WriteLine("Results saved.");
                };
            }
            else
            {
                Execute = async () => await RunSpeedtest().ConfigureAwait(false);
            }
        }

        public async Task Begin()
        {
            if (_timer is null)
                await Execute().ConfigureAwait(false);
            else
            {
                var tcs = new TaskCompletionSource<Task>();
                TestsComplete += (_, __) =>
                {
                    tcs.SetResult(Task.CompletedTask);
                };
                _timer.Start();
                await tcs.Task;
            }
        }

        private async Task<Models.Result> RunSpeedtest()
        {
            if (!_silent)
                Console.WriteLine("Begin speed test");

            string jsonResult;

            try
            {
                using (var proc = new Process())
                {
                    var info = new ProcessStartInfo("speedtest-cli.exe");
                    info.Arguments = "--json";
                    info.UseShellExecute = false;
                    info.RedirectStandardOutput = true;
                    info.RedirectStandardError = true;
                    proc.StartInfo = info;
                    proc.Start();
                    jsonResult = await proc.StandardOutput.ReadToEndAsync();
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong with the speedtest. {DateTime.Now:T}.  Message: {e.Message}");
                return null;
            }

            Models.Result result;

            try
            {
                result = JsonConvert.DeserializeObject<Models.Result>(jsonResult);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Somethign went wrong parsing the results from the speedtest. {DateTime.Now:T}");
                return null;
            }

            if (!_silent)
                Console.WriteLine("Speed test complete.");

            if (!_options.HideResults)
            {
                Console.WriteLine($"speed test results at {DateTime.Now:T}");
                foreach (var item in JsonConvert.DeserializeObject<dynamic>(jsonResult))
                {
                    Console.WriteLine($"{item.Name}: {item.Value}");
                }
            }

            return result;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}