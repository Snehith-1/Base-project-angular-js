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

namespace StoryboardAPI.Controllers.ems.hrm
{
    [RoutePrefix("api/hrmDashboard")]
    [Authorize]

    public class hrmDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHrmDashboard objDaHrmDashboard = new DaHrmDashboard();
        [ActionName("punchIn")]
        [HttpPost]

        public HttpResponseMessage PostIAttendanceLogin(mdliAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostIAttendanceLogin(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("punchOut")]
        [HttpPost]

        public HttpResponseMessage PostIAttendanceLogout(mdliAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostIAttendanceLogout(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("iattendence")]
        [HttpGet]

        public HttpResponseMessage iAttendancepunchout()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdliAttendance objiAttendance = new mdliAttendance();
            objDaHrmDashboard.DaIAttendencePunchOut(getsessionvalues.employee_gid, objiAttendance);
            return Request.CreateResponse(HttpStatusCode.OK, objiAttendance);
        }

        [ActionName("applyLoginReq")]
        [HttpPost]

        public HttpResponseMessage PostLoginReq(mdlloginreq values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostAttendanceLogin(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("applyLogoutReq")]
        [HttpPost]

        public HttpResponseMessage PostLogoutReq(mdllogoutreq values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostAttendanceLogout(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("applyonduty")]
        [HttpPost]

        public HttpResponseMessage PostApplyOnduty(applyondutydetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostApplyOnduty(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("applyPermission")]
        [HttpPost]
        public HttpResponseMessage PostApplyPermission(permission_details values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostApplyPermission(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("applyCompoffReq")]
        [HttpPost]

        public HttpResponseMessage PostCompoffReq(mdlcompffreq values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHrmDashboard.DaPostCompoff(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("loginSummary")]
        [HttpGet]

        public HttpResponseMessage LoginSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlloginsummary objloginsummary = new mdlloginsummary();
            objDaHrmDashboard.DaGetLoginSummary(getsessionvalues.employee_gid, objloginsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objloginsummary);
        }
        [ActionName("logoutSummary")]
        [HttpGet]

        public HttpResponseMessage LogoutSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdllogoutsummary objlogoutsummary = new mdllogoutsummary();
            objDaHrmDashboard.DaGetLogoutSummary(getsessionvalues.employee_gid, objlogoutsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objlogoutsummary);
        }

        [ActionName("ondutySummary")]
        [HttpGet]

        public HttpResponseMessage getondutysummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            onduty_detail_list objonduty_details = new onduty_detail_list();
            objonduty_details = objfnHrmDashboard.getondutysummary(getsessionvalues.employee_gid, objonduty_details);
            return Request.CreateResponse(HttpStatusCode.OK, objonduty_details);
        }
        [ActionName("compOffSummary")]
        [HttpGet]

        public HttpResponseMessage getcompoffsummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            compoff_list objcompoff_details = new compoff_list();
            objcompoff_details = objfnHrmDashboard.getcompoffsummary(getsessionvalues.employee_gid, objcompoff_details);
            return Request.CreateResponse(HttpStatusCode.OK, objcompoff_details);
        }
        [ActionName("permissionSummary")]
        [HttpGet]

        public HttpResponseMessage getpermissionsummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            permission_details_list objpermission_details = new permission_details_list();
            objpermission_details = objfnHrmDashboard.getpermissionsummary(getsessionvalues.employee_gid, objpermission_details);
            return Request.CreateResponse(HttpStatusCode.OK, objpermission_details);
        }

        [ActionName("loginPendingDelete")]
        [HttpGet]

        public HttpResponseMessage loginPendingDelete(string attendancelogintmp_gid)
        {
            loginsummary_list objloginsummary = new loginsummary_list();
            objloginsummary = objfnHrmDashboard.loginpending_delete(attendancelogintmp_gid, objloginsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objloginsummary);
        }

        [ActionName("logoutPendingDelete")]
        [HttpGet]

        public HttpResponseMessage logoutPendingDelete(string attendancetmp_gid, logoutsummary_list values)
        {
            logoutsummary_list objlogoutsummary = new logoutsummary_list();
            objlogoutsummary = objfnHrmDashboard.logoutpending_delete(attendancetmp_gid, objlogoutsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objlogoutsummary);
        }

        [ActionName("ODPendingDelete")]
        [HttpGet]

        public HttpResponseMessage ODPendingDelete(string ondutytracker_gid, onduty_detail_list values)
        {
            onduty_details objODsummary = new onduty_details();
            objODsummary = objfnHrmDashboard.ODpending_delete(ondutytracker_gid, objODsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objODsummary);
        }

        [ActionName("compoffPendingDelete")]
        [HttpGet]

        public HttpResponseMessage compoffPendingDelete(string compensatoryoff_gid)
        {
            compoffSummary_details objCompoffsummary = new compoffSummary_details();
            objCompoffsummary = objfnHrmDashboard.compoffpending_delete(compensatoryoff_gid, objCompoffsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objCompoffsummary);
        }

        [ActionName("permissionPendingDelete")]
        [HttpGet]

        public HttpResponseMessage permissionPendingDelete(string permission_gid)
        {
            permissionSummary_details objpermissionsummary = new permissionSummary_details();
            objpermissionsummary = objfnHrmDashboard.permissionpending_delete(permission_gid, objpermissionsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objpermissionsummary);
        }



        [ActionName("monthlyAttendence")]
        [HttpGet]
        public HttpResponseMessage getmonthlyAttendence()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            monthlyAttendence objmonthlyAttendence = new monthlyAttendence();
            objmonthlyAttendence = objfnHrmDashboard.getmonthlyattendence_fn(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmonthlyAttendence);
        }

    }
}
