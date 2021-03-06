﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoHard.Data
{
    /// <summary>
    /// TeleInfo Data
    /// </summary>
    public class TeleInfoData : SensorData
    {
        public TeleInfoData()
        {
            this.Type = "TeleInfoData";
        }

        public ObjectId Id { get; set; }

        /// <summary>
        /// Meter Id
        /// </summary>
        public string MeterId { get; set; }

        /// <summary>
        /// Date of the Data sample
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Account intensity in A
        /// </summary>
        public int AccountIntensity { get; set; }

        /// <summary>
        /// Peek hour compteur in Wh
        /// </summary>
        public decimal PeekHourCpt { get; set; }

        /// <summary>
        /// Low hour compteur in Wh
        /// </summary>
        public decimal LowHourCpt { get; set; }

        /// <summary>
        /// Actual intensity used in A
        /// </summary>
        public int ActualIntensity { get; set; }

        /// <summary>
        /// Max intensity used from the start of the Meter use in A
        /// </summary>
        public int MaxIntensity { get; set; }

        /// <summary>
        /// Aparrent power in VA
        /// </summary>
        public int ApparentPower { get; set; }

        public bool HasExceed { get; set; }

        public override string Serialize()
        {
            return string.Empty;
        }
    }
}
