﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
            Rank = Convert.ToDouble(_storage.Load("RANK-" + id));
            Similarity = Convert.ToDouble(_storage.Load("SIMILARITY-" + id));
            //TODO: проинициализировать свойства Rank и Similarity сохранёнными в БД значениями
        }
    }
}
