using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.hrm.Models;
using ems.hrm.DataAccess;

namespace ems.hrm.Controllers
{
    [RoutePrefix("api/myLeave")]
    [Authorize]

    public class MyLeaveController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMyLeave objDaMyLeave = new DaMyLeave();

        [ActionName("myLeave")]
        [HttpGet]
        public HttpResponseMessage getmyLeave()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmyLeave objmyLeave = new mdlmyLeave();
            var status = objDaMyLeave.DaGetMyLeave(getsessionvalues.employee_gid, objmyLeave);
            return Request.CreateResponse(HttpStatusCode.OK, objmyLeave);

        }
    }
}
