using System;
using NATS.Client;
using System.Linq;
using System.Text.Json;
using CommonLib;

namespace EventLogger
{
    class EventLogger
    {
        private readonly IConnection connection = new ConnectionFactory().CreateConnection();
        private readonly IAsyncSubscription subscription;

        public EventLogger()
        { 
            subscription = connection.SubscribeAsync("valuator.logging.similarity", (sender, args) =>
            {
                SimilarityMessage message = JsonSerializer.Deserialize<SimilarityMessage>(args.Message.Data);
                Console.WriteLine($"Subject: {args.Message.Subject} Id {message.id} Similarity {message.similarity}");
            }
            );

            subscription = connection.SubscribeAsync("rankCalculate.logging.rank", (sender, args) =>
            {
                RankMessage message = JsonSerializer.Deserialize<RankMessage>(args.Message.Data);
                Console.WriteLine($"Subject: {args.Message.Subject} Id {message.id} Rank {message.rank}");
            }
            );

            Console.WriteLine("EventLogger is started");
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
    }
}
