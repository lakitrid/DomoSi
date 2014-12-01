using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fr.Lakitrid.DomoCore
{
    public class CoreService : ICoreService
    {
        public DateTime Ping()
        {
            return DateTime.UtcNow;
        }

        public void SetData(string serieName, DateTime date, decimal value)
        {
            new DataSerieService().SetData(serieName, date, value);
        }

        public string GetData()
        {
            return "test";

            //return new { serieName = "Test", date = DateTime.UtcNow, value = (decimal)1.25 };
        }
    }
}
