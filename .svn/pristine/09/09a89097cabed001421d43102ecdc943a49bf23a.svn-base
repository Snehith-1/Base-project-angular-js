using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.master;

namespace EMS.Reports.Controllers.ems.master
{
    [RoutePrefix("api/MstCadLsa")]
    public class MstCadLsaController : ApiController
    {

        [HttpGet]
        [ActionName("GetCADLSAreport")]
        public HttpResponseMessage GetCADLSAreport(string generatelsa_gid)
        {
            DaMstCadLsa objFunction = new DaMstCadLsa();
            var path = objFunction.DaGetCADLSAreport(generatelsa_gid);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}

