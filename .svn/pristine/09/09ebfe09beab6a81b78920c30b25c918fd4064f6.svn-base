using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.rsk.DataAccess;
using ems.rsk.Models;

namespace ems.rsk.Controllers
{
    [RoutePrefix("api/RskDashboard")]
    [Authorize]

    public class RskDashboardController : ApiController
    {
        DaRskDashboard objDaAccess = new DaRskDashboard();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetRskPrivilege")]
        [HttpGet]
        public HttpResponseMessage GetRskPrivilege(string user_gid)
        {
            rskprivilege objresult = new rskprivilege();
            objDaAccess.DaGetRskPrivilege(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetRMAllocateCountdtl")]
        [HttpGet]
        public HttpResponseMessage GetRMAllocateCountdtl(string assigned_RM,string qualified_status)
        {
            customerstatusList objresult = new customerstatusList();
            objDaAccess.GetRMAllocateCountdtl(assigned_RM, qualified_status, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetAllocationDtl")]
        [HttpGet]
        public HttpResponseMessage GetAllocationDtl()
        {
            allocationvisitgraphlist objresult = new allocationvisitgraphlist();
            objDaAccess.DaGetAllocationDtl(objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}
