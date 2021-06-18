using CommonLib;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace RankCalculator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var storage = new Storage();
            var rankCalculator = new RankCalculator(storage);
            rankCalculator.Start();
            await Task.Delay(-1);
        }
    }
}
