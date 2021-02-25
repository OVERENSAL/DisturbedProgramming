using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Valuator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStorage _storage;

        public IndexModel(IStorage storage)
        {
            _storage = storage;
        }

        public IActionResult OnPost(string text)
        {
            string id = Guid.NewGuid().ToString();

            string similarityKey = "SIMILARITY-" + id;
            _storage.Store(similarityKey, GetSimilarity(text, id).ToString());
            //TODO: посчитать similarity и сохранить в БД по ключу similarityKey

            string textKey = "TEXT-" + id;
            _storage.Store(textKey, text);

            string rankKey = "RANK-" + id;
            _storage.Store(rankKey, GetRank(text));
            //TODO: посчитать rank и сохранить в БД по ключу rankKey

            return Redirect($"summary?id={id}");
        }

        private int GetSimilarity(string text, string id)
        {
            var keys = _storage.GetKeys();

            return keys.Any(item => item.Substring(0, 5) == "TEXT-" && _storage.Load(item) == text) ? 1 : 0;
        }

        public string GetRank(string text)
        {
            double nonLetterCount = text.Count(x => !char.IsLetter(x));

            return ((double)(nonLetterCount / text.Length)).ToString();
        }
    }
}
