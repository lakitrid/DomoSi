using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DomoRest
{
    [ServiceContract]
    public interface ICommandService
    {
        [OperationContract]
        [WebGet(UriTemplate = "Ping", ResponseFormat = WebMessageFormat.Json)]
		DateTime Ping();

		[OperationContract]
		[WebGet(UriTemplate = "Status", ResponseFormat = WebMessageFormat.Json)]
		string Status();

		[OperationContract]
		[WebGet(UriTemplate = "Modules", ResponseFormat = WebMessageFormat.Json, BodyStyle= WebMessageBodyStyle.Wrapped)]
		List<Module> GetModules ();
    }
}
