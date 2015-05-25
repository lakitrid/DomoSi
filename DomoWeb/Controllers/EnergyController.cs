using DomoService.Domain;
using DomoService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DomoWeb.Controllers
{
    public class EnergyController : ApiController
    {
        // GET api/values
        public EnergyStat Get()
        {
            var service = new EnergyService();
            return service.GetCurrentEnergyIndexes();
        }
    }
}