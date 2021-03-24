using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public struct RankMessage
    {
        public string id { get; set; }
        public double rank { get; set; }

        public RankMessage(string id, double rank)
        {
            this.id = id;
            this.rank = rank;
        }
    }
}
