using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoHard.Data
{
    public class OregonData
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public int Channel { get; set; }

        public decimal? Temperature { get; set; }

        public decimal BatteryLevel { get; set; }

        public bool IsValid()
        {
            if(this.Channel == 0 || !this.Temperature.HasValue)
            {
                return false;
            }

            return true;
        }
    }
}
