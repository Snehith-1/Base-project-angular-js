using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.ep.Models;
using ems.ep.DataAccess;

namespace StoryboardAPI.Controllers.ems.ep
{
    [RoutePrefix("api/caseAllocation")]
    [Authorize]

    public class CaseAllocationController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCaseAllocation objdaCaseAllocation = new DaCaseAllocation();
        [ActionName("getExternalallocateddtl")]
        [HttpGet]
        public HttpResponseMessage GetExternalAllocatedDtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            allocationdtlList objmappingdtl = new allocationdtlList();
             objdaCaseAllocation.DaGetExternalAllocatedDtl(getsessionvalues.user_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }
        [ActionName("GetExternalVisitCancelLog")]
        [HttpGet]
        public HttpResponseMessage GetExternalVisitCancelLog(string allocationdtl_gid)
        {
            visistreportcancelList values = new visistreportcancelList();
            objdaCaseAllocation.DaGetExternalVisitCancelLog(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
