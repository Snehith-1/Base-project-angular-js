using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.rms.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.rms.DataAccess;

namespace StoryboardAPI.Controllers.ems.rms
{
    [RoutePrefix("api/businessunitTeam")]
    [Authorize]
    public class businessunitTeamController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DabusinessunitTeam objdabusinessunitteam = new DabusinessunitTeam();

        [ActionName("businessunitteam")]
        [HttpGet]
        public HttpResponseMessage getbusinessunitteam(string businessunit_gid)
        {

            mdlbusinessunitteam objbusinessunitteam_name = new mdlbusinessunitteam();
            objdabusinessunitteam.DagetbusinessunitTeam_Name(businessunit_gid, objbusinessunitteam_name);
            return Request.CreateResponse(HttpStatusCode.OK, objbusinessunitteam_name);
        }
    }
}
