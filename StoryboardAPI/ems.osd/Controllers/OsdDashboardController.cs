using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.osd.Models;
using ems.osd.DataAccess;


namespace ems.osd.Controllers
{
    [RoutePrefix("api/osddashboard")]
    [Authorize]
    public class OsdDashboardController : ApiController
    {
        DaOsdTrnDashboard objDaAccess = new DaOsdTrnDashboard();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("osdprivilege")]
        [HttpGet]
        public HttpResponseMessage osdprivilege(string user_gid)
        {
            osdprivilege objresult = new osdprivilege();
            objDaAccess.DaOsdPrevilege(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}
