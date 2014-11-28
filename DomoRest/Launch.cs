using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DomoRest
{
    public class Launch
    {
        public static void Main()
        {
			// Initialize Module List :
			if (!ModuleManager.Instance.Load()) {
				Console.WriteLine ("Loading module failed");
			}

            string url = "http://localhost:8080/";
            WebHttpBinding b = new WebHttpBinding();
            WebServiceHost host = new WebServiceHost(typeof(CommandService), new Uri(url));
            host.AddServiceEndpoint(typeof(ICommandService), b, "");
            host.Open();
            Console.WriteLine("--- type [CR] to quit ---");
            Console.ReadLine();
            host.Close();
        }
    }
}
