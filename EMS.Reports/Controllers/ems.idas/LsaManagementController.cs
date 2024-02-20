using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.idas;

namespace EMS.Reports.Controllers.ems.idas
{
    [RoutePrefix("api/lsaReport")]
    public class LsaManagementController : ApiController
    {

        [HttpGet]
        [ActionName("GetLSAreport")]
        public HttpResponseMessage GetlsaReport(string lsacreate_gid)
        {
            ids_rpt_lsaManagement objFunction = new ids_rpt_lsaManagement();
            var path = objFunction.DaGetLSAManagement(lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}

