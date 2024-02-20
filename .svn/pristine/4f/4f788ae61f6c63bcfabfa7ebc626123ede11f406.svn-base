using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.osd.Models;
using ems.osd.DataAccess;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdMstSupportTeam")]
    [Authorize]

    public class OsdMstSupportTeamController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdMstSupportTeam objDaOsdMstSupportTeam = new DaOsdMstSupportTeam();

        [ActionName("PostSupportTeamAdd")]
        [HttpPost]
        public HttpResponseMessage PostSupportTeamAdd(supportteamdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstSupportTeam.DaPostSupportTeamAdd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupportTeamUpdate")]
        [HttpPost]
        public HttpResponseMessage GetSupportTeamUpdate(supportteamdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstSupportTeam.DaGetSupportTeamUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupportTeamDelete")]
        [HttpGet]
        public HttpResponseMessage GetSupportTeamDelete(string supportteam_gid)
        {
            result values = new result();
            objDaOsdMstSupportTeam.DaGetSupportTeamDelete(supportteam_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupportTeamView")]
        [HttpGet]
        public HttpResponseMessage GetSupportTeamView(string supportteam_gid)
        {
            supportteamviewdtl values = new supportteamviewdtl();
            objDaOsdMstSupportTeam.DaGetSupportTeamView(supportteam_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupportTeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetActivitySummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            supportdtllist values = new supportdtllist();
            objDaOsdMstSupportTeam.DaGetSupportTeamSummary(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTeamMember")]
        [HttpGet]
        public HttpResponseMessage GetTeamMember(string supportteam_gid)
        {
            supportteamviewdtl values = new supportteamviewdtl();
            objDaOsdMstSupportTeam.DaGetTeamMember(supportteam_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Team Member Except Current User
        [ActionName("GetTeamMemberExcept")]
        [HttpGet]
        public HttpResponseMessage GetTeamMemberExcept(string supportteam_gid)
        {
            supportteamviewdtl values = new supportteamviewdtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstSupportTeam.DaGetTeamMemberExcept(supportteam_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Team Members Except Assigned Member
        [ActionName("PostTeamMemberExceptAssigned")]
        [HttpPost]
        public HttpResponseMessage PostTeamMemberExceptAssigned(supportteamviewdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstSupportTeam.DaPostTeamMemberExceptAssigned(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // All Team Members
        [ActionName("GetAllTeamMember")]
        [HttpGet]
        public HttpResponseMessage GetAllTeamMember()
        {
            supportteamviewdtl values = new supportteamviewdtl();
            objDaOsdMstSupportTeam.DaGetAllTeamMember(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // All Dept Team Members
        [ActionName("GetDeptAllTeamMember")]
        [HttpGet]
        public HttpResponseMessage GetDeptAllTeamMember(string department_gid)
        {
            supportteamviewdtl values = new supportteamviewdtl();
            objDaOsdMstSupportTeam.DaGetDeptAllTeamMember(values, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
