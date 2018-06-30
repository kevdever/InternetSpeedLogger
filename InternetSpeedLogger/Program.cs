/*
 * Copyright 2018 KevDever 
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace InternetSpeedLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("testing ");

            var proc = new Process();
            var info = new ProcessStartInfo("speedtest-cli.exe");
            info.Arguments = "--json";
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            proc.StartInfo = info;
            proc.Start();
            var output = await proc.StandardOutput.ReadToEndAsync();
            var obj = JsonConvert.DeserializeObject<dynamic>(output);
            Console.WriteLine("results: ");
            foreach (var item in obj)
            {
                Console.WriteLine($"{item.Name}: {item.Value}");
            }


            var res = JsonConvert.DeserializeObject<Result>(output);

            using (var context = new Database.SqlServerDb())
            {
                context.Results.Add(res);
                await context.SaveChangesAsync();
            }


            Console.ReadKey();
        }
    }

}
