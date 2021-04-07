using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommonLib;
using NATS.Client;
using System.Text;
using System.Text.Json;

namespace Valuator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStorage _storage;
        private readonly IConnection connection = new ConnectionFactory().CreateConnection();

        public IndexModel(IStorage storage)
        {
            _storage = storage;
        }

        public IActionResult OnPost(string text, string country)
        {
            string id = Guid.NewGuid().ToString();

            _storage.SaveId(id, country);

            string similarityKey = Constants.SIMILARITY + id;
            _storage.Store(similarityKey, id, GetSimilarity(text, id).ToString());

            SimilarityMessage similarityMessage = new SimilarityMessage(id, GetSimilarity(text, id));
            connection.Publish("valuator.logging.similarity", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(similarityMessage)));

            string textKey = Constants.TEXT + id;
            _storage.Store(textKey, id, text);

            connection.Publish("processRank", Encoding.UTF8.GetBytes(id));

            return Redirect($"summary?id={id}");
        }

        private int GetSimilarity(string text, string id)
        {
            var keys = _storage.GetKeys();

            foreach(string key in keys) {
                if (text.Equals(_storage.Load(Constants.TEXT + key, key)))
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
