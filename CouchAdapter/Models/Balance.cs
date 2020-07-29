using System;
using System.Collections.Generic;
using System.Text;

namespace CouchAdapter.Models
{
    public class Balance
    {
        public string ID { get; set; }
        public long PunterID { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal PendingBalance { get; set; }
        public string Type { get { return "balance"; } }
    }
}
