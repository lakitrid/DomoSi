using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Fr.Lakitrid.DomoDb;
using Fr.Lakitrid.DomoDb.Entities;

namespace Fr.Lakitrid.DomoCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);
#if DEBUG
            using (var db = new DomoContext())
            {
                db.Database.CreateIfNotExists();
            } 

            DomoCoreService.Run();
#else
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[] 
            { 
                new DomoCoreService() 
            };
            ServiceBase.Run(servicesToRun);
#endif
        }
    }
}
