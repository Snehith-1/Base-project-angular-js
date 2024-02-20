using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.idas.DataAccess;
using ems.idas.Models;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasDashboard")]
    [Authorize]
    public class IdasDashboardController : ApiController
    {
           DaIdasDashboard objDaAccess = new DaIdasDashboard();
            session_values Objgetgid = new session_values();
            logintoken getsessionvalues = new logintoken();

        [ActionName("IdasUserPrivilege")]
        [HttpGet]
        public HttpResponseMessage GetIdasUserPrivilege(string module_gid)
        {
            idasUserPrivilege objResult = new idasUserPrivilege();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetIdasPrivilege(getsessionvalues.user_gid, module_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetCadDashboardSummary")]
        [HttpGet]
        public HttpResponseMessage GetCadDashboardSummary(string caddropdown, string from_date, string to_date)
        {
            MdlCadDashboard values = new MdlCadDashboard();
            objDaAccess.DaGetCadDashboardSummary(caddropdown, from_date, to_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
