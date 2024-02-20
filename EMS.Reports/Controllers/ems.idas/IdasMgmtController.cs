using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.idas;


namespace EMS.Reports.Controllers.ems.idas
{
    [RoutePrefix("api/IdasMgmt")]
    public class IdasMgmtController : ApiController
    {
        [HttpGet]
        [ActionName("ComplianceCertificate")]
        public HttpResponseMessage CaseCreation(string sanction_gid)
        {
            idas_rpt_ComplianceCertificate objFunction = new idas_rpt_ComplianceCertificate();
            var path = objFunction.DaGetCaseCreation(sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}
