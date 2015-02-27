using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoDataServ
{
    public class DataLog
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string JsonData { get; set; }        
    }
}
