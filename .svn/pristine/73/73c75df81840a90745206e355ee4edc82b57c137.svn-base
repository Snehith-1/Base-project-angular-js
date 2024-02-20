using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System;

/// <summary>
/// (It's used for pages in CC Schedule)MstCC Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCC")]
    [Authorize]
    public class MstCCController : ApiController
    {
        DaMstCC objDaMstCC = new DaMstCC();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //CC Meeting Pending Summary
        [ActionName("GetCCPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Meeting Scheduled Summary
        [ActionName("GetCCScheduledSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCScheduledSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCScheduledSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Meeting Scheduled-Completed Summary
        [ActionName("GetMeetingCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMeetingCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetMeetingCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCcount")]
        [HttpGet]
        public HttpResponseMessage GetCCcount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCcount values = new MdlCCcount();
            objDaMstCC.DaGetCCcount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApplicantInfo")]
        [HttpGet]
        public HttpResponseMessage GetApplicantInfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC values = new MdlMstCC();
            objDaMstCC.DaGetApplicantInfo(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostScheduleMeeting")]
        [HttpPost]
        public HttpResponseMessage PostScheduleMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostScheduleMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       
        [ActionName("GetScheduleMeeting")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeeting(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetScheduleMeeting(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RecheduleMeeting")]
        [HttpPost]
        public HttpResponseMessage RecheduleMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaRecheduleMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CancelMeeting")]
        [HttpPost]
        public HttpResponseMessage CancelMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaCancelMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetScheduleMeetingLog")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetScheduleMeetingLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcc2member")]
        [HttpPost]
        public HttpResponseMessage Getcc2member(MdlMstCCMember values)
        {
            objDaMstCC.DaGetcc2member(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("OtherEmployee")]
        [HttpPost]
        public HttpResponseMessage getEmployee(MdlOtherEmployee objMdlEmployee)
        {
          //  MdlOtherEmployee objMdlEmployee = new MdlOtherEmployee();
            objDaMstCC.DaGetEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        [ActionName("GetCCCalenderDtl")]
        [HttpGet]
        public HttpResponseMessage GetCCCalenderDtl()
        {
            calendarevent values = new calendarevent();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetCCCalenderDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDescSave")]
        [HttpPost]
        public HttpResponseMessage MOMDescSave(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaMOMDescSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage MOMDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlMstCC values = new MdlMstCC();
            objDaMstCC.DaMOMDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDescSubmit")]
        [HttpPost]
        public HttpResponseMessage MOMDescSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaMOMDescSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Query Conversation
        [ActionName("PostSendCCRequestor")]
        [HttpPost]
        public HttpResponseMessage PostSendCCRequestor(ccrequestordtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostSendCCRequestor(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Query Conversation List
        [ActionName("GetCCRequestorlist")]
        [HttpGet]
        public HttpResponseMessage GetRequestorlist(string application_gid)
        {
            ccrequestorlist values = new ccrequestorlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetRequestorlist(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Query Conversation Document Upload
        [ActionName("ConversationCCDocUpload")]
        [HttpPost]
        public HttpResponseMessage ConversationCCDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            upload_document documentname = new upload_document();
            objDaMstCC.DaConversationCCDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        //Privilege Based COndition
        [ActionName("GetAdminPrivilege")]
        [HttpGet]
        public HttpResponseMessage GetAdminPrivilege(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetAdminPrivilege(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // MOM Description
        [ActionName("GetMOMDescription")]
        [HttpGet]
        public HttpResponseMessage GetMOMDescription(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetMOMDescription(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Post Attendance
        [ActionName("PostCCAttendance")]
        [HttpPost]
        public HttpResponseMessage PostCCAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostCCAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostothersAttendance")]
        [HttpPost]
        public HttpResponseMessage PostothersAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostothersAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //MOM Submit
        [ActionName("PostMOMSubmit")]
        [HttpPost]
        public HttpResponseMessage PostMOMSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostMOMSubmit(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Undo Attendance
        [ActionName("PostUndoCCAttendance")]
        [HttpPost]
        public HttpResponseMessage PostUndoCCAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostUndoCCAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUndoOthersAttendance")]
        [HttpPost]
        public HttpResponseMessage PostUndoOthersAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostUndoOthersAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // MOM Approval Enable Flag
        [ActionName("GetMOMApprovalFlag")]
        [HttpGet]
        public HttpResponseMessage GetMOMApprovalFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetMOMApprovalFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCCApprove")]
        [HttpPost]
        public HttpResponseMessage PostCCApprove(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostCCApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC View Remarks
        [ActionName("ViewCCRemarks")]
        [HttpGet]
        public HttpResponseMessage ViewCCRemarks(string ccmeeting2members_gid,string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaViewCCRemarks(getsessionvalues.employee_gid, ccmeeting2members_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ViewOtherRemarks")]
        [HttpGet]
        public HttpResponseMessage ViewOtherRemarks(string ccmeeting2othermembers_gid, string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaViewOtherRemarks(getsessionvalues.employee_gid, ccmeeting2othermembers_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC Pending Summary
        [ActionName("GetCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAdminCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetAdminCCSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetAdminCCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC Completed Summary
        [ActionName("GetCCCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalFlag( string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetApprovalFlag(getsessionvalues.employee_gid,  application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MOM_delete")]
        [HttpGet]
        public HttpResponseMessage MOM_delete(string application2momdoc_gid, string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objDaMstCC.DaMOM_delete(application2momdoc_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("PostRevertCCtoCredit")]
        [HttpPost]
        public HttpResponseMessage PostRevertCCtoCredit(MdlCCRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostRevertCCtoCredit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Sent back to Credit Summary
        [ActionName("GetCCtoCreditSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCtoCreditSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCtoCreditSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppRevertReasonRemarks")]
        [HttpGet]
        public HttpResponseMessage GetAppRevertReasonRemarks(string application_gid)
        {
            MdlappCreditassign values = new MdlappCreditassign();
            objDaMstCC.DaGetAppRevertReasonRemarks(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleMeetingBasicLog")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingBasicLog(string application_gid, string ccschedulemeetinglog_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetScheduleMeetingBasicLog(application_gid, ccschedulemeetinglog_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC Approval Pending Summary
        [ActionName("GetCCApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC Schedule Meeting Calender View
        [ActionName("GetCCMeetingCalenderView")]
        [HttpGet]
        public HttpResponseMessage GetCCMeetingCalenderView()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCMeetingCalenderView(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalInitiate")]
        [HttpGet]
        public HttpResponseMessage GetApprovalInitiate(string application_gid, string ccmeeting2members_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetApprovalInitiate(application_gid, ccmeeting2members_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostApprovalInitiate")]
        [HttpPost]
        public HttpResponseMessage PostApprovalInitiate(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostApprovalInitiate(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CancelApprovalInitiate")]
        [HttpGet]
        public HttpResponseMessage CancelApprovalInitiate(string application_gid, string ccmeeting2members_gid)
        {
            MdlMstCCschedule values = new MdlMstCCschedule();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaCancelApprovalInitiate(values, application_gid, ccmeeting2members_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetApprovalList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalShowFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalShowFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetApprovalShowFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMomFlag")]
        [HttpGet]
        public HttpResponseMessage GetMomFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetMomFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMOMDescriptions")]
        [HttpGet]
        public HttpResponseMessage GetMOMDescriptions(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetMOMDescriptions(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMOMReapproval")]
        [HttpGet]
        public HttpResponseMessage GetMOMReapproval(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetMOMReapproval(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMOMRemail")]
        [HttpGet]
        public HttpResponseMessage GetMOMRemail(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetMOMRemail(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostReMOMSubmit")]
        [HttpPost]
        public HttpResponseMessage PostReMOMSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostReMOMSubmit(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PastDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            result values = new result();
            objDaMstCC.DaPastDateCheck(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCtoCreditLog")]
        [HttpGet]
        public HttpResponseMessage GetCCtoCreditLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCRevert values = new MdlCCRevert();
            objDaMstCC.DaGetCCtoCreditLog(application_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCC.DaGetCCApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCMeetingHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetCCMeetingHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCMeetingHistoryLog values = new MdlCCMeetingHistoryLog();
            objDaMstCC.DaGetCCMeetingHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCMeetingApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetCCMeetingApprovalDtls(string application_gid,string ccschedulemeeting_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetCCMeetingApprovalDtls(application_gid, ccschedulemeeting_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditScheduleMeetingBasicLog")]
        [HttpGet]
        public HttpResponseMessage GetCreditScheduleMeetingBasicLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetCreditScheduleMeetingBasicLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleMeetingList")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaMstCC.DaGetScheduleMeetingList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("CCApplicationCount")]
        [HttpGet]
        public HttpResponseMessage CCApplicationCount()
        {
            CCCount_list values = new CCCount_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaCCApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC Meeting Skip
        [ActionName("PostCcMeetingSkip")]
        [HttpPost]
        public HttpResponseMessage PostCcMeetingSkip(MdlCcMeetingSkip values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaPostCcMeetingSkip(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Meeting Skip Summary
        [ActionName("GetCCMeetingSkipSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCMeetingSkipSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCcMeetingSkip values = new MdlCcMeetingSkip();
            objDaMstCC.DaGetCCMeetingSkipSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Meeting Skip Reason
        [ActionName("GetCCMeetingSkippedReason")]
        [HttpGet]
        public HttpResponseMessage GetCCMeetingSkippedReason(string ccmeetingskip_gid)
        {
            MdlCcMeetingSkip values = new MdlCcMeetingSkip();
            objDaMstCC.DaGetCCMeetingSkippedReason(values, ccmeetingskip_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Meeting Skip History
        [ActionName("GetCCMeetingSkipHistory")]
        [HttpGet]
        public HttpResponseMessage GetGetCCMeetingSkipHistory(string application_gid)
        {
            MdlCcMeetingSkip values = new MdlCcMeetingSkip();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCC.DaGetGetCCMeetingSkipHistory(getsessionvalues.employee_gid, values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Sendback CC Meeting Skip Reason
        [ActionName("GetSendbackCCMeetingSkippedReason")]
        [HttpGet]
        public HttpResponseMessage GetSendbackCCMeetingSkippedReason(string application_gid)
        {
            MdlCcMeetingSkip values = new MdlCcMeetingSkip();
            objDaMstCC.DaGetSendbackCCMeetingSkippedReason(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
