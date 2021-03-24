using CommonLib;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RankCalculate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));
            var storage = new Storage();
            var rankCalculator = new RankCalculate(loggerFactory.CreateLogger<RankCalculate>(), storage);
            rankCalculator.Start();
            await Task.Delay(-1);
        }
    }
}
