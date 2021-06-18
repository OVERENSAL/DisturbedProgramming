using System;
using System.Text;
using System.Linq;
using CommonLib;
using NATS.Client;
using System.Text.Json;

namespace RankCalculator
{
    class RankCalculator
    {
        private readonly IConnection connection = new ConnectionFactory().CreateConnection();
        private readonly IAsyncSubscription subscription;
        public RankCalculator(IStorage storage)
        {
            subscription = connection.SubscribeAsync("processRank", (sender, args) =>
            {
                string id = Encoding.UTF8.GetString(args.Message.Data);
                string textKey = Constants.TEXT + id;

                if (storage.IsKeyExist(textKey))
                {
                    string text = storage.Load(textKey);
                    string rank = GetRank(text);
                    storage.Store(Constants.RANK + id, rank);

                    connection.Publish("rankCalculator.logging.rank", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(rank)));
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

            return Math.Round(((lettersCount) / (double)text.Length), 2).ToString();
        }
    }
}
