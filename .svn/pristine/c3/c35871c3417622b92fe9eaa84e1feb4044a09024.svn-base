using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to various functionalities in AC management flow.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>


    [RoutePrefix("api/AgrTrnCC")]
    [Authorize]
    public class AgrTrnCCController : ApiController
    {
        DaAgrTrnCC objDaAgrTrnCC = new DaAgrTrnCC();
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
            objDaAgrTrnCC.DaGetCCPendingSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetCCScheduledSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetMeetingCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCcount")]
        [HttpGet]
        public HttpResponseMessage GetCCcount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCcount values = new MdlCCcount();
            objDaAgrTrnCC.DaGetCCcount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApplicantInfo")]
        [HttpGet]
        public HttpResponseMessage GetApplicantInfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC values = new MdlMstCC();
            objDaAgrTrnCC.DaGetApplicantInfo(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostScheduleMeeting")]
        [HttpPost]
        public HttpResponseMessage PostScheduleMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostScheduleMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetScheduleMeeting")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeeting(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnCC.DaGetScheduleMeeting(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RecheduleMeeting")]
        [HttpPost]
        public HttpResponseMessage RecheduleMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaRecheduleMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CancelMeeting")]
        [HttpPost]
        public HttpResponseMessage CancelMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaCancelMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetScheduleMeetingLog")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnCC.DaGetScheduleMeetingLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcc2member")]
        [HttpPost]
        public HttpResponseMessage Getcc2member(MdlMstCCMember values)
        {
            objDaAgrTrnCC.DaGetcc2member(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("OtherEmployee")]
        [HttpPost]
        public HttpResponseMessage getEmployee(MdlOtherEmployee objMdlEmployee)
        {
            //  MdlOtherEmployee objMdlEmployee = new MdlOtherEmployee();
            objDaAgrTrnCC.DaGetEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        [ActionName("GetCCCalenderDtl")]
        [HttpGet]
        public HttpResponseMessage GetCCCalenderDtl()
        {
            calendarevent values = new calendarevent();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetCCCalenderDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDescSave")]
        [HttpPost]
        public HttpResponseMessage MOMDescSave(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaMOMDescSave(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaMOMDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDescSubmit")]
        [HttpPost]
        public HttpResponseMessage MOMDescSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaMOMDescSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Query Conversation
        [ActionName("PostSendCCRequestor")]
        [HttpPost]
        public HttpResponseMessage PostSendCCRequestor(ccrequestordtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostSendCCRequestor(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetRequestorlist(getsessionvalues.employee_gid, application_gid, values);
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
            objDaAgrTrnCC.DaConversationCCDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
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
            objDaAgrTrnCC.DaGetAdminPrivilege(getsessionvalues.employee_gid, application_gid, values);
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
            objDaAgrTrnCC.DaGetMOMDescription(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Post Attendance
        [ActionName("PostCCAttendance")]
        [HttpPost]
        public HttpResponseMessage PostCCAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostCCAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostothersAttendance")]
        [HttpPost]
        public HttpResponseMessage PostothersAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostothersAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //MOM Submit
        [ActionName("PostMOMSubmit")]
        [HttpPost]
        public HttpResponseMessage PostMOMSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostMOMSubmit(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Undo Attendance
        [ActionName("PostUndoCCAttendance")]
        [HttpPost]
        public HttpResponseMessage PostUndoCCAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostUndoCCAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUndoOthersAttendance")]
        [HttpPost]
        public HttpResponseMessage PostUndoOthersAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostUndoOthersAttendance(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetMOMApprovalFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCCApprove")]
        [HttpPost]
        public HttpResponseMessage PostCCApprove(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostCCApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // CC View Remarks
        [ActionName("ViewCCRemarks")]
        [HttpGet]
        public HttpResponseMessage ViewCCRemarks(string ccmeeting2members_gid, string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaViewCCRemarks(getsessionvalues.employee_gid, ccmeeting2members_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ViewOtherRemarks")]
        [HttpGet]
        public HttpResponseMessage ViewOtherRemarks(string ccmeeting2othermembers_gid, string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaViewOtherRemarks(getsessionvalues.employee_gid, ccmeeting2othermembers_gid, application_gid, values);
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
            objDaAgrTrnCC.DaGetCCSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetCCCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetApprovalFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MOM_delete")]
        [HttpGet]
        public HttpResponseMessage MOM_delete(string application2momdoc_gid, string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objDaAgrTrnCC.DaMOM_delete(application2momdoc_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("PostRevertCCtoCredit")]
        [HttpPost]
        public HttpResponseMessage PostRevertCCtoCredit(MdlCCRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostRevertCCtoCredit(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetCCtoCreditSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppRevertReasonRemarks")]
        [HttpGet]
        public HttpResponseMessage GetAppRevertReasonRemarks(string application_gid)
        {
            MdlappCreditassign values = new MdlappCreditassign();
            objDaAgrTrnCC.DaGetAppRevertReasonRemarks(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleMeetingBasicLog")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingBasicLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnCC.DaGetScheduleMeetingBasicLog(application_gid, getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetCCApprovalPendingSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetCCMeetingCalenderView(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalInitiate")]
        [HttpGet]
        public HttpResponseMessage GetApprovalInitiate(string application_gid, string ccmeeting2members_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnCC.DaGetApprovalInitiate(application_gid, ccmeeting2members_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostApprovalInitiate")]
        [HttpPost]
        public HttpResponseMessage PostApprovalInitiate(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostApprovalInitiate(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CancelApprovalInitiate")]
        [HttpGet]
        public HttpResponseMessage CancelApprovalInitiate(string application_gid, string ccmeeting2members_gid)
        {
            MdlMstCCschedule values = new MdlMstCCschedule();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaCancelApprovalInitiate(values, application_gid, ccmeeting2members_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnCC.DaGetApprovalList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalShowFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalShowFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetApprovalShowFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMomFlag")]
        [HttpGet]
        public HttpResponseMessage GetMomFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetMomFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMOMDescriptions")]
        [HttpGet]
        public HttpResponseMessage GetMOMDescriptions(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetMOMDescriptions(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMOMReapproval")]
        [HttpGet]
        public HttpResponseMessage GetMOMReapproval(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetMOMReapproval(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMOMRemail")]
        [HttpGet]
        public HttpResponseMessage GetMOMRemail(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaGetMOMRemail(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostReMOMSubmit")]
        [HttpPost]
        public HttpResponseMessage PostReMOMSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostReMOMSubmit(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PastDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            result values = new result();
            objDaAgrTrnCC.DaPastDateCheck(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCtoCreditLog")]
        [HttpGet]
        public HttpResponseMessage GetCCtoCreditLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCRevert values = new MdlCCRevert();
            objDaAgrTrnCC.DaGetCCtoCreditLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnCC.DaGetCCApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // CC Pending Summary
        [ActionName("GetAdminCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetAdminCCSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnCC.DaGetAdminCCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleMeetingList")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnCC.DaGetScheduleMeetingList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CCApplicationCount")]
        [HttpGet]
        public HttpResponseMessage CCApplicationCount()
        {
            CCCount_list values = new CCCount_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaCCApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetACAutoApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetACAutoApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnCC.DaGetACAutoApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCcMeetingSkip")]
        [HttpPost]
        public HttpResponseMessage PostCcMeetingSkip(MdlCcMeetingSkip values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnCC.DaPostCcMeetingSkip(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnCC.DaGetGetCCMeetingSkipHistory(getsessionvalues.employee_gid, values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CC Meeting Skip Reason
        [ActionName("GetCCMeetingSkippedReason")]
        [HttpGet]
        public HttpResponseMessage GetCCMeetingSkippedReason(string ccmeetingskip_gid)
        {
            MdlCcMeetingSkip values = new MdlCcMeetingSkip();
            objDaAgrTrnCC.DaGetCCMeetingSkippedReason(values, ccmeetingskip_gid);
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
            objDaAgrTrnCC.DaGetCCMeetingSkipSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Sendback CC Meeting Skip Reason
        [ActionName("GetSendbackCCMeetingSkippedReason")]
        [HttpGet]
        public HttpResponseMessage GetSendbackCCMeetingSkippedReason(string application_gid)
        {
            MdlCcMeetingSkip values = new MdlCcMeetingSkip();
            objDaAgrTrnCC.DaGetSendbackCCMeetingSkippedReason(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}