using DomoDataServ;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DomoHardServ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string mongoConnectionString = ConfigurationManager.AppSettings["mongoConnectionString"];
            string mongoDbName = ConfigurationManager.AppSettings["mongoDbName"];
            string teleInfoPort = ConfigurationManager.AppSettings["TeleInfoPort"];

            DataLogger logger = new DataLogger(mongoConnectionString, mongoDbName);

            logger.AddData(new DataLog() { Date = DateTime.Now, Type = "Log", JsonData = "{ Message = 'Application Start' }" });
            Console.ReadLine();
            logger.AddData(new DataLog() { Date = DateTime.Now, Type = "Log", JsonData = "{ Message = 'Application Close' }" });
        }
    }
}
