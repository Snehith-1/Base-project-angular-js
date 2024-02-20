using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.rsk;

namespace EMS.Reports.Controllers.ems.rsk
{
    [RoutePrefix("api/ObservationReport")]
    public class ObservationReportController : ApiController
    {
        [HttpGet]
        [ActionName("getobservationpdf")]
        public HttpResponseMessage getobservationpdf(string observation_reportgid)
        {
            rpt_observationReport objFunction = new rpt_observationReport();
            var path = objFunction.getobservationReport(observation_reportgid);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}
