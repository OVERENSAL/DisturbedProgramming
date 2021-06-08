using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public static class Constants
    {
        public const string TEXT = "TEXT-";
        public const string RANK = "RANK-";
        public const string SIMILARITY = "SIMILARITY-";

        public const string Host = "localhost";
        public const int Port = 6379;
        public static string DB_RUS = Environment.GetEnvironmentVariable("DB_RUS");
        public static string DB_EU = Environment.GetEnvironmentVariable("DB_EU");
        public static string DB_OTHER = Environment.GetEnvironmentVariable("DB_OTHER");

        public static Dictionary<string, string> COUNTRIES_TO_REGIONS = new Dictionary<string, string>()
        {
            ["Россия"] = "localhost:6000",
            ["Франция"] = "localhost:6001",
            ["Германия"] = "localhost:6001",
            ["США"] = "localhost:6002",
            ["Индия"] = "localhost:6002"
        };
    }
}
