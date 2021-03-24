using System;
using System.Text;
using System.Linq;
using CommonLib;
using NATS.Client;
using Microsoft.Extensions.Logging;

namespace RankCalculate
{
    class RankCalculate
    {
        private readonly IConnection connection;
        private readonly IAsyncSubscription subscription;
        public RankCalculate(IStorage storage)
        {
            connection = new ConnectionFactory().CreateConnection();
            subscription = connection.SubscribeAsync("processRank", (sender, args) =>
            {
                string id = Encoding.UTF8.GetString(args.Message.Data);
                string textKey = Constants.TEXT + id;

                if (storage.IsKeyExist(textKey))
                {
                    string text = storage.Load(textKey);
                    string rank = GetRank(text);
                    storage.Store(Constants.RANK + id, rank);
                }
            }
            );
        }

        public void Start()
        {
            subscription.Start();

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();

            subscription.Unsubscribe();

            connection.Drain();
            connection.Close();
        }

        private string GetRank(string text)
        {
            int lettersCount = text.Count(char.IsLetter);

            return Math.Round(((text.Length - lettersCount) / (double)text.Length), 2).ToString();
        }
    }
}
