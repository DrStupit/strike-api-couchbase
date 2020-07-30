using System;
using System.Collections.Generic;
using System.Text;

namespace Strike.Common.Models.Feeds
{
    public class SportResponse
    {
        public bool Success { get; set; }
        public List<Sport> Data { get; set; }
    }
}
