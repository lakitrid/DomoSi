using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoHard.Data
{
    /// <summary>
    /// Sensor Data common informations
    /// </summary>
    public abstract class SensorData
    {
        /// <summary>
        /// Type of the sensor data
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Serialize the sensor Data
        /// </summary>
        /// <returns>serialized Data</returns>
        public abstract string Serialize();
    }
}
