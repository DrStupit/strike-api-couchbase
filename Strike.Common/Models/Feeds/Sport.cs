using System;
using System.Collections.Generic;
using System.Text;

namespace Strike.Common.Models.Feeds
{
    public class Sport
    {
        public string Key { get; set; }
        public bool active { get; set; }
        public string Group { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
        public string Type { get { return "sport"; } }
    }
}
