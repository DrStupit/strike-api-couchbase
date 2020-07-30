using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Common.Models.Odds
{
    public class OddsResponse
    {
        public bool Success { get; set; }
        public List<ListingOptions> Data { get; set; }
    }

    public class ListingOptions
    {
        public string Sport_Key { get; set; }
        public string Sport_Nice { get; set; }
        public List<string> Teams { get; set; }
        public int Commence_Time { get; set; }
        public string Home_Team { get; set; }
        public List<Sites> Sites { get; set; }
        public int Sites_Count { get; set; }
        public string Type { get { return "offered-odds"; } }
    }

    public class Sites
    {
        public string Site_Key { get; set; }
        public string Site_Nice { get; set; }
        public int Last_Update { get; set; }
        public Odds Odds { get; set; }
    }

    public class Odds
    {
        public List<double> H2H { get; set; }
        public Spread spreads { get; set; }
    }

    public class Spread
    {
        public List<double> Odds { get; set; }
        public List<string> Points { get; set; }
    }
}
