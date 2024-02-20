using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.rsk;

namespace EMS.Reports.Controllers.ems.rsk
{
    [RoutePrefix("api/VisitReport")]
    public class VisitReportController : ApiController
    {
        [HttpGet]
        [ActionName("getvisitreportpdf")]
        public HttpResponseMessage getvisitreportpdf(string allocationdtl_gid)
        {
            rpt_visitReport objFunction = new rpt_visitReport();
            var path = objFunction.getvisitReport(allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}
