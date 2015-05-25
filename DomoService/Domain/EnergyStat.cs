using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoService.Domain
{
    public class EnergyStat
    {
        public EnergyStat()
        {
            this.StartIndex = new Dictionary<Period, EnergyIndex>();
            this.PrevStartIndex = new Dictionary<Period, EnergyIndex>();
        }

        public EnergyIndex Current { get; set; }

        public Dictionary<Period, EnergyIndex> StartIndex { get; private set; }

        public Dictionary<Period, EnergyIndex> PrevStartIndex { get; private set; }
    }
}
