using System;
using System.Collections.Generic;
using System.Text;

namespace Strike.Common.Models.Feeds
{
    public class TestFeed
    {
        public string ID { get; set; }
        public string SiteName { get; set; }
        public string Teams { get; set; }
        public double Odds { get; set; }
        public string Type { get { return "testfeed"; } }
    }
}
