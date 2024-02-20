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
    [RoutePrefix("api/OsdMstActivity")]
    [Authorize]

    public class OsdMstActivityController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdMstActivity objDaOsdMstActivity = new DaOsdMstActivity();

        [ActionName("PostActivityAdd")]
        [HttpPost]
        public HttpResponseMessage PostActivityAdd(activityadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivity.DaPostActivityAdd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivityUpdate")]
        [HttpPost]
        public HttpResponseMessage GetActivityUpdate(activityadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivity.DaGetActivityUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivityDelete")]
        [HttpGet]
        public HttpResponseMessage GetActivityDelete(string activitymaster_gid)
        {
            result values = new result();
            objDaOsdMstActivity.DaGetActivityDelete(activitymaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivityView")]
        [HttpGet]
        public HttpResponseMessage GetActivityView(string activitymaster_gid)
        {
            activityadd values = new activityadd();
            objDaOsdMstActivity.DaGetActivityView(activitymaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivitySummary")]
        [HttpGet]
        public HttpResponseMessage GetActivitySummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            actvitydtllist values = new actvitydtllist();
            objDaOsdMstActivity.DaGetActivitySummary(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetTeamSummary(string department_gid)
        {
            supportdtllist values = new supportdtllist();
            objDaOsdMstActivity.DaGetTeamSummary(values, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActivity")]
        [HttpGet]
        public HttpResponseMessage GetActivity(string department_gid)
        {
            activitylist values = new activitylist();
            objDaOsdMstActivity.DaGetActivity(values, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeptTeam")]
        [HttpGet]
        public HttpResponseMessage GetDeptTeam( string department_gid)
        {
            supportdtllist values = new supportdtllist();
            objDaOsdMstActivity.DaGetDeptTeam(values, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExportActivityReport")]
        [HttpGet]
        public HttpResponseMessage ExportActivityReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            result values = new result();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivity.DaGetExportActivityReport(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
