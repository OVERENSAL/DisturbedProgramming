﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public struct SimilarityMessage
    {
        public string id { get; set; }
        public double similarity { get; set; }

        public SimilarityMessage(string id, double similarity)
        {
            this.id = id;
            this.similarity = similarity;
        }
    }
}
