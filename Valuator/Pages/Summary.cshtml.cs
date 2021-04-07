using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CommonLib;

namespace Valuator.Pages
{
    public class SummaryModel : PageModel
    {
        private readonly IStorage _storage;

        public SummaryModel(IStorage storage)
        {
            _storage = storage;
        }

        public double Rank { get; set; }
        public double Similarity { get; set; }

        public void OnGet(string id)
        {
            Rank = Convert.ToDouble(_storage.Load(Constants.RANK + id, id));
            Similarity = Convert.ToDouble(_storage.Load(Constants.SIMILARITY + id, id));
        }
    }
}
