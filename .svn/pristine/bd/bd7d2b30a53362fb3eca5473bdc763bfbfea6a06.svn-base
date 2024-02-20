using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.hrm.Models;
using ems.hrm.DataAccess;

namespace ems.hrm.Controllers
{
    public class MyTeamController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMyTeam objDaMyTeam = new DaMyTeam();

        [ActionName("myteam")]
        [HttpGet]
        public HttpResponseMessage getmyteam()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            myteam objmyteam = new myteam();
            var status = objDaMyTeam.DaGetTeam(getsessionvalues.employee_gid, objmyteam);
            return Request.CreateResponse(HttpStatusCode.OK, objmyteam);
        }

        [ActionName("teamemployeehiearchy")]
        [HttpGet]
        public HttpResponseMessage getteamemployeehiearchy(string employee_gid)
        {
            myteam objmyteam = new myteam();
            var status = objDaMyTeam.DaGetTeam(employee_gid, objmyteam);
            return Request.CreateResponse(HttpStatusCode.OK, objmyteam);
        }

        [ActionName("teamemployeeprofile")]
        [HttpGet]
        public HttpResponseMessage getteamemployeeprofile(string employee_gid)
        {
            employeedetails objemployeedetails = new employeedetails();
            var status = objDaMyTeam.DaGetTeamemployeeprofile(employee_gid, objemployeedetails);
            return Request.CreateResponse(HttpStatusCode.OK, objemployeedetails);
        }
    }
}
