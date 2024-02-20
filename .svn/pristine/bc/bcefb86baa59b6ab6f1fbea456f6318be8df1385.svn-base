using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rsk.Models;
using ems.rsk.DataAccess;


namespace ems.rsk.Controllers
{
    [RoutePrefix("api/ExclusionList")]
    [Authorize]

    public class ExclusionListController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaExclusionList objDaExclusionList = new DaExclusionList();


        [ActionName("GetExclusionAllocation")]
        [HttpGet]
        public HttpResponseMessage GetExclusionAllocation(string customer_urn,string allocationdtl_gid,string exclusion_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaExclusionList.DaGetExclusionAllocation(customer_urn, allocationdtl_gid, exclusion_reason, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExclusionZonalExport")]
        [HttpGet]
        public HttpResponseMessage GetExclusionZonalExport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            exportexclusion values = new exportexclusion();
            objDaExclusionList.DaGetExclusionZonalExport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExclusionSummary")]
        [HttpGet]
        public HttpResponseMessage GetExclusionSummary()
        {
            exclusioncustomerlist values = new exclusioncustomerlist();
            objDaExclusionList.DaGetExclusionSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExclusionExport")]
        [HttpGet]
        public HttpResponseMessage GetExclusionExport()
        {
            exportexclusion values = new exportexclusion();
            objDaExclusionList.DaGetExclusionExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
