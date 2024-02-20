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

        public HttpResponseMessage GetOnDutySummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            onduty_detail_list objonduty_details = new onduty_detail_list();
            objDaHrmDashboard.DaGetOnDutySummary(getsessionvalues.employee_gid, objonduty_details);
            return Request.CreateResponse(HttpStatusCode.OK, objonduty_details);
        }
        [ActionName("compOffSummary")]
        [HttpGet]

        public HttpResponseMessage GetCompOffSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            compoff_list objcompoff_details = new compoff_list();
            objDaHrmDashboard.DaGetCompOffSummary(getsessionvalues.employee_gid, objcompoff_details);
            return Request.CreateResponse(HttpStatusCode.OK, objcompoff_details);
        }
        [ActionName("permissionSummary")]
        [HttpGet]

        public HttpResponseMessage GetPermissionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            permission_details_list objpermission_details = new permission_details_list();
            objDaHrmDashboard.DaGetPermissionSummary(getsessionvalues.employee_gid, objpermission_details);
            return Request.CreateResponse(HttpStatusCode.OK, objpermission_details);
        }

        [ActionName("loginPendingDelete")]
        [HttpGet]

        public HttpResponseMessage PostLoginPendingDelete(string attendancelogintmp_gid)
        {
            loginsummary_list objloginsummary = new loginsummary_list();
            objDaHrmDashboard.DaPostLoginPendingDelete(attendancelogintmp_gid, objloginsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objloginsummary);
        }

        [ActionName("logoutPendingDelete")]
        [HttpGet]

        public HttpResponseMessage PostLogoutPendingDelete(string attendancetmp_gid, logoutsummary_list values)
        {
           // logoutsummary_list objlogoutsummary = new logoutsummary_list();
            objDaHrmDashboard.DaPostLogoutPendingDelete(attendancetmp_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ODPendingDelete")]
        [HttpGet]

        public HttpResponseMessage PostODPendingDelete(string ondutytracker_gid)
        {
           onduty_details values = new onduty_details();
            objDaHrmDashboard.DaPostODpendingDelete(ondutytracker_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values );
        }

        [ActionName("compoffPendingDelete")]
        [HttpGet]

        public HttpResponseMessage GetCompoffPendingDelete(string compensatoryoff_gid)
        {
            compoffSummary_details values = new compoffSummary_details();
            objDaHrmDashboard.DaPostCompoffPendingDelete(compensatoryoff_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("permissionPendingDelete")]
        [HttpGet]

        public HttpResponseMessage GetPermissionPendingDelete(string permissiondtl_gid)
        {
            permissionSummary_details values = new permissionSummary_details();
            objDaHrmDashboard.DaPostPermissionDelete(permissiondtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("monthlyAttendence")]
        [HttpGet]
        public HttpResponseMessage GetMonthlyAttendence()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            monthlyAttendence objmonthlyAttendence = new monthlyAttendence();
            objDaHrmDashboard.DaGetMonthlyAttendence(objmonthlyAttendence,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmonthlyAttendence);
        }

        [ActionName("monthlyAttendenceReport")]
        [HttpGet]
        public HttpResponseMessage GetmonthlyAttendenceReport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            monthlyAttendenceReport objmonthlyAttendence = new monthlyAttendenceReport();
            objDaHrmDashboard.DamonthlyAttendenceReport(getsessionvalues.employee_gid, objmonthlyAttendence);
            return Request.CreateResponse(HttpStatusCode.OK, objmonthlyAttendence);
        }

        [ActionName("Announcement")]
        [HttpGet]
        public HttpResponseMessage GetAnnouncement()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlannouncement objannouncement = new mdlannouncement();
            objDaHrmDashboard.DaGetAnnouncement(getsessionvalues.employee_gid, objannouncement);
            return Request.CreateResponse(HttpStatusCode.OK, objannouncement);
        }
    }
}
