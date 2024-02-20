using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.lgl.Models;
using ems.utilities.Functions;
using System.Web;
using ems.lgl.DataAccess;

namespace ems.lgl.Controllers
{
    [RoutePrefix("api/optDashboard")]
    [Authorize]
    public class OptDashboardController : ApiController
    {
        DaOptDashboard objoptvalues = new DaOptDashboard();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("getprivilegeGID")]
        [HttpGet]
        public HttpResponseMessage getGID()
        {
            mdloptDashboard values = new mdloptDashboard();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objoptvalues.DaGetGID(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
