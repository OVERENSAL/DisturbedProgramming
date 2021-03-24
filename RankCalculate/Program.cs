using CommonLib;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RankCalculate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var storage = new Storage();
            var rankCalculator = new RankCalculate(storage);
            rankCalculator.Start();
            await Task.Delay(-1);
        }
    }
}
