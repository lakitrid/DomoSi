using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Fr.Lakitrid.DomoCore
{
    partial class DomoCoreService : ServiceBase
    {
        private WebServiceHost host;

        public DomoCoreService()
        {
            this.InitializeComponent();
        }

#if DEBUG
        /// <summary>
        /// Entry point for debug of the service
        /// </summary>        
        public static void Run()
        {
            DomoCoreService service = new DomoCoreService();
            service.OnStart(null);
            Console.ReadKey();
            service.OnStop();
        }
#endif

        protected override void OnStart(string[] args)
        {
            string url = "http://localhost:5050/";
            WebHttpBinding b = new WebHttpBinding();
            this.host = new WebServiceHost(typeof(CoreService), new Uri(url));
            this.host.AddServiceEndpoint(typeof(ICoreService), b, "");
            this.host.Open();
        }

        protected override void OnStop()
        {
            this.host.Close();
        }
    }
}
