/*
 * Copyright 2018 KevDever 
 * See LICENSE.md
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Test.CommandLineParsing;
using Newtonsoft;
using Newtonsoft.Json;

namespace InternetSpeedLogger
{
    public class CommandLineArguments
    {
        public bool? Silent { get; set; }
        public bool? HideResults { get; set; }
        public bool? PersistResults { get; set; }
        public string Frequency { get; set; }
        public int? MaxRuns { get; set; }
        public bool? Help { get; set; }
    }

    class Program
    {
        static string HelpInfo = @"Options: 
/Silent suppresses status messages. 
/HideResults suppresses the speedtest results. 
/PersistResults saves the speedtest results to the database. 
/Frequency={HH:MM:SS} specifies that the speedtest repeats at the specified interval.
/MaxRuns={number} lets you specify the maximum number of times the test will run, if you specified a Frequency.";

        static async Task Main(string[] args)
        {
            var cancelTokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Cancelling...");
                cancelTokenSource.Cancel();
            };

            var parsedArgs = new CommandLineArguments();
            CommandLineParser.ParseArguments(parsedArgs, args);

            if (parsedArgs.Help == true)
            {
                Console.WriteLine(HelpInfo);
                return;
            }

            var options = new TestRunnerOptions();

            options.Silent = parsedArgs.Silent == true ? true : false;
            options.HideResults = parsedArgs.HideResults == true ? true : false;
            options.ResultRepository = parsedArgs.PersistResults == true ? new Database.ResultRepository() : null;
            options.CancellationToken = cancelTokenSource.Token;

            string intervalStr = "";

            if (parsedArgs.Frequency != null)
            {
                var components = parsedArgs.Frequency.Split(':');
                if (components.Count() != 3)
                {
                    Console.WriteLine("the Frequency argument must be of the form HH:MM:SS, such as /Frequency=3:15:00 to repeat every 3 hours 15 minutes.");
                    return;
                }
                try
                {
                    var frequencySpecs = components.Select(n => int.Parse(n)).ToArray();
                    if (frequencySpecs.Any(i => i < 0))
                    {
                        Console.WriteLine("Only positive integers supported within Frequency argument");
                        return;
                    }
                    options.Frequency = new TimeSpan(frequencySpecs[0], frequencySpecs[1], frequencySpecs[2]);
                }
                catch
                {
                    Console.WriteLine("Something went wrong interpreting the Frequency argument. Please see the /help.");
                    return;
                }
                intervalStr = $" Speed tests will be run at intervals of {options.Frequency.ToString("h'h 'm'm 's's'")}{(options.MaxRuns > 0 ? $" a total of {options.MaxRuns} times" : " until you terminate this program")}.";
            }

            if (parsedArgs.MaxRuns != null)
            {
                if (parsedArgs.MaxRuns < 0)
                {
                    Console.WriteLine("Max Runs argument must be greater than 0");
                    return;
                }
                options.MaxRuns = (int)parsedArgs.MaxRuns;
            }

            var resultsStr = $" Results will {(options.ResultRepository is null ? "not " : "")}be saved to the database.";
            var displayStr = $" Results will {(options.HideResults ? "not " : "")}be displayed.";
            var quietStr = $" Status messages will {(options.Silent ? "not " : "")}be displayed.";

            Console.WriteLine($"Welcome." + intervalStr + resultsStr + displayStr + quietStr);

            using (var testRunner = new TestRunner(options))
                await testRunner.Begin();

            Console.WriteLine("All tests complete. Press any key to exit...");
            Console.ReadKey();
        }
    }

}
