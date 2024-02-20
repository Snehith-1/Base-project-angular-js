using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.rms.Models;
using ems.rms.DataAccess;
namespace StoryboardAPI.Controllers.ems.rms
{
    [RoutePrefix("api/team2Recruiter")]
    [Authorize]
    public class team2RecruiterController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        Dateam2Recruiter objdateam2recruiter = new Dateam2Recruiter();

        [ActionName("businessunitteamrecruiter")]
        [HttpGet]
        public HttpResponseMessage getteam2recruiter(string businessunit2team_gid)
        {
            mdlteam2recruiter objteam2recruiter = new mdlteam2recruiter();
            objdateam2recruiter.Dagetteam2recruiter(businessunit2team_gid, objteam2recruiter);
         //   objteam2recruiter = objfnteam2recruiter.getteam2recruiter_fn(businessunit2team_gid, objteam2recruiter);
            return Request.CreateResponse(HttpStatusCode.OK, objteam2recruiter);
        }

    }
}
