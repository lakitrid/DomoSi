module App.Domain {
    "use strict";

    export class EnergyStat {
        public Current: EnergyIndex
        public StartIndex: EnergyStatVals
        public PrevStartIndex: EnergyStatVals
    }
}  