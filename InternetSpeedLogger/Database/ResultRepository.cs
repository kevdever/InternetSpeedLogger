using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetSpeedLogger.Database
{
    public class ResultRepository : IResultRepository
    {
        public async Task AddResult(Models.Result result)
        {
            using (var context = new SqlServerDb())
            {
                var client = await context.Clients.Where(c => c.Ip.Equals(result.Client.Ip) && c.Isp.Equals(result.Client.Isp)).SingleOrDefaultAsync();
                if (client != null)
                {
                    result.Client = null;
                    result.ClientId = client.ClientId;
                }

                var server = await context.Servers.Where(s => s.Id == result.Server.Id).SingleOrDefaultAsync();
                if (server != null)
                {
                    result.Server = null;
                    result.ServerId = server.Id;
                }

                try
                {
                    context.Results.Add(result);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Something went wrong saving the results to the database: {e.Message}. These results were discarded. Timestamp: {DateTime.Now:T}");
                }
            }
        }
    }
}
