using System;
using System.Threading.Tasks;

namespace EventsLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var eventLogger = new EventsLogger();
            eventLogger.Start();
            await Task.Delay(-1);
        }
    }
}
