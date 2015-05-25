using DomoHard.Data;
using DomoService.Domain;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DomoService.Dao
{
    public class EnergyDao : MongoAbstractDao
    {
        private readonly string energyCol = "TeleInfoData";

        /// <summary>
        /// Gets last index
        /// </summary>
        /// <returns>last index</returns>
        public EnergyIndex GetCurrentEnergyIndexes()
        {
            EnergyIndex index = new EnergyIndex();

            var collection = this.database.GetCollection<TeleInfoData>(this.energyCol);
            TeleInfoData data = collection.AsQueryable().OrderByDescending(e => e.Date).FirstOrDefault();

            if (data != null)
            {
                index.PeekHours = data.PeekHourCpt / 1000;
                index.LowHours = data.LowHourCpt / 1000;
            }

            return index;
        }

        /// <summary>
        /// Gets First index of the date
        /// </summary>
        /// <param name="date">date to get</param>
        /// <returns>first index of the date</returns>
        public EnergyIndex GetEnergyIndexesAt(DateTime date)
        {
            EnergyIndex index = new EnergyIndex();

            var collection = this.database.GetCollection<TeleInfoData>(this.energyCol);
            TeleInfoData data = collection.AsQueryable().Where(e => e.Date > date).OrderBy(e => e.Date).FirstOrDefault();

            if (data != null)
            {
                index.PeekHours = data.PeekHourCpt / 1000;
                index.LowHours = data.LowHourCpt / 1000;
            }

            return index;
        }

        public TeleInfoData[] GetEnergyIndexesFrom(DateTime date)
        {
            var collection = this.database.GetCollection<TeleInfoData>(this.energyCol);
            TeleInfoData[] data = collection.AsQueryable().Where(e => e.Date > date).OrderBy(e => e.Date).ToArray();

            return data;
        }
    }

}
