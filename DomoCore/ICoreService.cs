using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Fr.Lakitrid.DomoCore
{
    [ServiceContract]
    public interface ICoreService
    {
        [OperationContract]
        [WebGet(UriTemplate = "ping", ResponseFormat = WebMessageFormat.Json)]
        DateTime Ping();

        [OperationContract]
        [WebInvoke(Method="PUT", UriTemplate = "setData", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SetData(string serieName, DateTime date, decimal value);

        [OperationContract]
        [WebGet(UriTemplate = "getData", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [return:MessageParameter(Name="serieName")]
        string GetData();
    }
}
