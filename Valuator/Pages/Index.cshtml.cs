using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommonLib;
using NATS.Client;
using System.Text;

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

        public IActionResult OnPost(string text)
        {
            string id = Guid.NewGuid().ToString();

            string similarityKey = Constants.SIMILARITY + id;
            _storage.Store(similarityKey, GetSimilarity(text, id).ToString());

            string textKey = Constants.TEXT + id;
            _storage.Store(textKey, text);

            connection.Publish("processRank", Encoding.UTF8.GetBytes(id));

            return Redirect($"summary?id={id}");
        }

        private int GetSimilarity(string text, string id)
        {
            var keys = _storage.GetKeys();

            return keys.Any(item => item.Substring(0, 5) == Constants.TEXT && _storage.Load(item) == text) ? 1 : 0;
        }
    }
}
