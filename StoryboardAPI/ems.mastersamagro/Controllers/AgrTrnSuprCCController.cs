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
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A </remarks>

    [RoutePrefix("api/AgrTrnSuprCC")]
    [Authorize]
    public class AgrTrnSuprCCController : ApiController
    {
        DaAgrTrnSuprCC objDaAgrTrnSuprCC = new DaAgrTrnSuprCC();
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
            objDaAgrTrnSuprCC.DaGetCCPendingSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetCCScheduledSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetMeetingCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCcount")]
        [HttpGet]
        public HttpResponseMessage GetCCcount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCcount values = new MdlCCcount();
            objDaAgrTrnSuprCC.DaGetCCcount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApplicantInfo")]
        [HttpGet]
        public HttpResponseMessage GetApplicantInfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC values = new MdlMstCC();
            objDaAgrTrnSuprCC.DaGetApplicantInfo(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostScheduleMeeting")]
        [HttpPost]
        public HttpResponseMessage PostScheduleMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostScheduleMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetScheduleMeeting")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeeting(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnSuprCC.DaGetScheduleMeeting(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RecheduleMeeting")]
        [HttpPost]
        public HttpResponseMessage RecheduleMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaRecheduleMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CancelMeeting")]
        [HttpPost]
        public HttpResponseMessage CancelMeeting(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaCancelMeeting(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetScheduleMeetingLog")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnSuprCC.DaGetScheduleMeetingLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcc2member")]
        [HttpPost]
        public HttpResponseMessage Getcc2member(MdlMstCCMember values)
        {
            objDaAgrTrnSuprCC.DaGetcc2member(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("OtherEmployee")]
        [HttpPost]
        public HttpResponseMessage getEmployee(MdlOtherEmployee objMdlEmployee)
        {
            //  MdlOtherEmployee objMdlEmployee = new MdlOtherEmployee();
            objDaAgrTrnSuprCC.DaGetEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        [ActionName("GetCCCalenderDtl")]
        [HttpGet]
        public HttpResponseMessage GetCCCalenderDtl()
        {
            calendarevent values = new calendarevent();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetCCCalenderDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDescSave")]
        [HttpPost]
        public HttpResponseMessage MOMDescSave(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaMOMDescSave(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaMOMDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MOMDescSubmit")]
        [HttpPost]
        public HttpResponseMessage MOMDescSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaMOMDescSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Query Conversation
        [ActionName("PostSendCCRequestor")]
        [HttpPost]
        public HttpResponseMessage PostSendCCRequestor(ccrequestordtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostSendCCRequestor(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetRequestorlist(getsessionvalues.employee_gid, application_gid, values);
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
            objDaAgrTrnSuprCC.DaConversationCCDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
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
            objDaAgrTrnSuprCC.DaGetAdminPrivilege(getsessionvalues.employee_gid, application_gid, values);
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
            objDaAgrTrnSuprCC.DaGetMOMDescription(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Post Attendance
        [ActionName("PostCCAttendance")]
        [HttpPost]
        public HttpResponseMessage PostCCAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostCCAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostothersAttendance")]
        [HttpPost]
        public HttpResponseMessage PostothersAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostothersAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //MOM Submit
        [ActionName("PostMOMSubmit")]
        [HttpPost]
        public HttpResponseMessage PostMOMSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostMOMSubmit(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Undo Attendance
        [ActionName("PostUndoCCAttendance")]
        [HttpPost]
        public HttpResponseMessage PostUndoCCAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostUndoCCAttendance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUndoOthersAttendance")]
        [HttpPost]
        public HttpResponseMessage PostUndoOthersAttendance(MdlCCAttendance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostUndoOthersAttendance(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetMOMApprovalFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCCApprove")]
        [HttpPost]
        public HttpResponseMessage PostCCApprove(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostCCApprove(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaViewCCRemarks(getsessionvalues.employee_gid, ccmeeting2members_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ViewOtherRemarks")]
        [HttpGet]
        public HttpResponseMessage ViewOtherRemarks(string ccmeeting2othermembers_gid, string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaViewOtherRemarks(getsessionvalues.employee_gid, ccmeeting2othermembers_gid, application_gid, values);
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
            objDaAgrTrnSuprCC.DaGetCCSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetCCCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetApprovalFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MOM_delete")]
        [HttpGet]
        public HttpResponseMessage MOM_delete(string application2momdoc_gid, string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objDaAgrTrnSuprCC.DaMOM_delete(application2momdoc_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("PostRevertCCtoCredit")]
        [HttpPost]
        public HttpResponseMessage PostRevertCCtoCredit(MdlCCRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostRevertCCtoCredit(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetCCtoCreditSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppRevertReasonRemarks")]
        [HttpGet]
        public HttpResponseMessage GetAppRevertReasonRemarks(string application_gid)
        {
            MdlappCreditassign values = new MdlappCreditassign();
            objDaAgrTrnSuprCC.DaGetAppRevertReasonRemarks(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleMeetingBasicLog")]
        [HttpGet]
        public HttpResponseMessage GetScheduleMeetingBasicLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnSuprCC.DaGetScheduleMeetingBasicLog(application_gid, getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetCCApprovalPendingSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrTrnSuprCC.DaGetCCMeetingCalenderView(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalInitiate")]
        [HttpGet]
        public HttpResponseMessage GetApprovalInitiate(string application_gid, string ccmeeting2members_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnSuprCC.DaGetApprovalInitiate(application_gid, ccmeeting2members_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostApprovalInitiate")]
        [HttpPost]
        public HttpResponseMessage PostApprovalInitiate(MdlMstCCschedule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostApprovalInitiate(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CancelApprovalInitiate")]
        [HttpGet]
        public HttpResponseMessage CancelApprovalInitiate(string application_gid, string ccmeeting2members_gid)
        {
            MdlMstCCschedule values = new MdlMstCCschedule();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaCancelApprovalInitiate(values, application_gid, ccmeeting2members_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCCschedule values = new MdlMstCCschedule();
            objDaAgrTrnSuprCC.DaGetApprovalList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalShowFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalShowFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetApprovalShowFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMomFlag")]
        [HttpGet]
        public HttpResponseMessage GetMomFlag(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetMomFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMOMDescriptions")]
        [HttpGet]
        public HttpResponseMessage GetMOMDescriptions(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetMOMDescriptions(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMOMReapproval")]
        [HttpGet]
        public HttpResponseMessage GetMOMReapproval(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetMOMReapproval(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMOMRemail")]
        [HttpGet]
        public HttpResponseMessage GetMOMRemail(string application_gid)
        {
            MdlMstCC values = new MdlMstCC();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaGetMOMRemail(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostReMOMSubmit")]
        [HttpPost]
        public HttpResponseMessage PostReMOMSubmit(MdlMstCC values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCC.DaPostReMOMSubmit(getsessionvalues.employee_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PastDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            result values = new result();
            objDaAgrTrnSuprCC.DaPastDateCheck(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCtoCreditLog")]
        [HttpGet]
        public HttpResponseMessage GetCCtoCreditLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCRevert values = new MdlCCRevert();
            objDaAgrTrnSuprCC.DaGetCCtoCreditLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnSuprCC.DaGetCCApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}