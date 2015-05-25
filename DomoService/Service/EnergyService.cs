using DomoHard.Data;
using DomoService.Dao;
using DomoService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomoService.Service
{
    public class EnergyService
    {
        public EnergyStat GetCurrentEnergyIndexes()
        {
            EnergyDao energyDao = new EnergyDao();
            EnergyStat stats = new EnergyStat();

            stats.Current = energyDao.GetCurrentEnergyIndexes();

            DateTime date = DateTime.Now.Date;
            DateTime requestDate = date.AddDays(date.DayOfYear * -1).AddYears(-1);

            TeleInfoData[] datas = energyDao.GetEnergyIndexesFrom(requestDate);

            stats.StartIndex.Add(Period.Day, this.GetAt(date, datas));
            stats.PrevStartIndex.Add(Period.Day, this.GetAt(date.AddDays(-1), datas));

            DateTime startOfWeek = date.AddDays((int)date.DayOfWeek * -1);
            stats.StartIndex.Add(Period.Week, this.GetAt(startOfWeek, datas));
            stats.PrevStartIndex.Add(Period.Week, this.GetAt(startOfWeek.AddDays(-7), datas));

            DateTime startOfMonth = date.AddDays(date.Day * -1);
            stats.StartIndex.Add(Period.Month, this.GetAt(startOfMonth, datas));
            stats.PrevStartIndex.Add(Period.Month, this.GetAt(startOfMonth.AddMonths(-1), datas));

            DateTime startOfYear = date.AddDays(date.DayOfYear * -1);
            stats.StartIndex.Add(Period.Year, this.GetAt(startOfYear, datas));
            stats.PrevStartIndex.Add(Period.Year, this.GetAt(date.AddYears(-1), datas));

            return stats;
        }

        private EnergyIndex GetAt(DateTime date, TeleInfoData[] datas)
        {
            EnergyIndex index = new EnergyIndex();

            TeleInfoData data = datas.Where(e => e.Date > date).OrderBy(e => e.Date).FirstOrDefault();

            if (data != null)
            {
                index.PeekHours = data.PeekHourCpt / 1000;
                index.LowHours = data.LowHourCpt / 1000;
            }

            return index;
        }
    }
}
