using System;
using System.Threading.Tasks;

namespace EventLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var eventLogger = new EventLogger();
            eventLogger.Start();
            await Task.Delay(-1);
        }
    }
}
