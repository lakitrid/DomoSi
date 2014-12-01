using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fr.Lakitrid.DomoDb;
using Fr.Lakitrid.DomoDb.Entities;

namespace Fr.Lakitrid.DomoCore
{
    public class DataSerieService
    {
        public DataSerieService()
        {                
        }

        public void SetData(string serieName, DateTime date, decimal value)
        {
            using(var db = new DomoContext())
            {
                DataSerie serie = db.DataSerie.FirstOrDefault(e => e.Name.Equals(serieName));
                if(serie == null)
                {
                    return;
                }

                db.Sample.Add(new Sample { Date = date, Value = value, SerieId = serie.Id });
                db.SaveChanges();
            }
        }
    }
}
