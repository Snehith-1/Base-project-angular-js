using ems.osd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.osd.Models;
using System.Net.Http;
using System.Net;
using System.Configuration;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdTrnCustomerQueryMgmt")]
    [Authorize]
    public class OsdTrnCustomerQueryMgmtController : ApiController
    {
        DaOsdTrnCustomerQueryMgmt objDaAccess = new DaOsdTrnCustomerQueryMgmt();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        [ActionName("CustomerQueryPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerQueryPendingSummary()
        {
            MdlQueryPending values = new MdlQueryPending();
            objDaAccess.DaCustomerQueryPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerQueryView")]
        [HttpGet]
        public HttpResponseMessage GetCustomerQueryView(string email_gid)
        {
            MdlQueryview values = new MdlQueryview();
            objDaAccess.DaCustomerQueryView(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerQueryAttachments")]
        [HttpGet]
        public HttpResponseMessage CustomerQueryAttachments(string email_gid)
        {
            MdlQueryAttachments values = new MdlQueryAttachments();
            objDaAccess.DaGetCustomerQueryAttachments(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostTicketAssign")]
        [HttpPost]
        public HttpResponseMessage PostTicketAssign(Ticketassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostTicketAssign(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerQueryAssignSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerQueryAssignSummary()
        {
            MdlQueryAssign values = new MdlQueryAssign();
            objDaAccess.DaCustomerQueryAssignSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignedQuery360")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignedQuery360(string email_gid)
        {
            MdlAssignedQuery360 values = new MdlAssignedQuery360();
            objDaAccess.DaCustomerAssignedQuery360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditLog")]
        [HttpGet]
        public HttpResponseMessage AuditLog(string email_gid)
        {
            MdlAuditLogHistory values = new MdlAuditLogHistory();
            objDaAccess.DaGetAuditLogSummary(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditView")]
        [HttpGet]
        public HttpResponseMessage PostAuditView(string email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostAuditView(email_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [ActionName("CustomerAssignedQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignedQuerySummary()
        {
            MdlAssignedQuery values = new MdlAssignedQuery();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCustomerAssignedQuerySummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEmailSignature")]
        [HttpPost]
        public HttpResponseMessage PostEmailSignature(MdlEmailSignature values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostEmailSignature(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetEmailSignature")]
        [HttpGet]
        public HttpResponseMessage GetEmailSugnature()
        {
            MdlEmailSignature values = new MdlEmailSignature();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetEmailSignature( getsessionvalues.user_gid,values );
            return Request.CreateResponse(HttpStatusCode.OK, values );
        }
        [ActionName("PostDecision")]
        [HttpPost]
        public HttpResponseMessage DaPostDecision(MdlDecisionhistory values)
        {

            values.mailcontent = values.mailcontent.Replace("/Public/", ConfigurationManager.AppSettings["mailurl"]);
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostDecision(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("MailAttachment")]
        [HttpPost]
        public HttpResponseMessage conversationdocupload()
        {
            System.Web.HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            objResult = objDaAccess.DaPostUploadAttachment(httpRequest, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetMailAttachment")]
        [HttpGet]
        public HttpResponseMessage GetMailAttachment()
        {
            MdlcDocList values = new MdlcDocList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMailAttachment(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAttachment")]
        [HttpGet]
        public HttpResponseMessage DeleteAttachment(string mailattachment_gid)
        {
            objResult = objDaAccess.DaPostMailAttachmentDelete(mailattachment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
     // Temp Delete
        [ActionName("Mailtempdelete")]
        [HttpGet]
        public HttpResponseMessage Mailtempdelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaMailtempdelete(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [ActionName("TransferLog")]
        [HttpGet]
        public HttpResponseMessage TransferLog(string lsemail_gid)
        {
            MdlTransferLogList values = new MdlTransferLogList();
            objDaAccess.DaGetTransferLog(lsemail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TicketTransfer")]
        [HttpPost]
        public HttpResponseMessage PostTicketTransfer(MdlAssignTo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostTicketTransfer(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerQueryCloseSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerQueryCloseSummary()
        {
            MdlQueryClose values = new MdlQueryClose();
            objDaAccess.DaCustomerQueryCloseSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerQueryClose360")]
        [HttpGet]
        public HttpResponseMessage GetCustomerQueryClose360(string email_gid)
        {
            MdlQueryClose360 values = new MdlQueryClose360();
            objDaAccess.DaCustomerQueryClose360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryCloseSummary")]
        [HttpGet]
        public HttpResponseMessage CustomerAssignQueryCloseSummary()
        {
            MdlAssignedQueryClose values = new MdlAssignedQueryClose();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCustomerAssignQueryCloseSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryClose360")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignQueryClose360(string email_gid)
        {
            MdlAssignQueryClose360 values = new MdlAssignQueryClose360();
            objDaAccess.DaCustomerAssignQueryClose360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryReplySummary")]
        [HttpGet]
        public HttpResponseMessage CustomerAssignQueryReplySummary()
        {
            MdlAssignedQueryReply values = new MdlAssignedQueryReply();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCustomerAssignQueryReplySummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryReply360")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignQueryReply360(string email_gid)
        {
            MdlAssignQueryReply360 values = new MdlAssignQueryReply360();
            objDaAccess.DaCustomerAssignQueryReply360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryForwardSummary")]
        [HttpGet]
        public HttpResponseMessage CustomerAssignQueryForwardSummary()
        {
            MdlAssignedQueryForward values = new MdlAssignedQueryForward();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCustomerAssignQueryForwardSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryForward360")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignQueryForward360(string email_gid)
        {
            MdlAssignQueryForward360 values = new MdlAssignQueryForward360();
            objDaAccess.DaCustomerAssignQueryForward360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryTransferSummary")]
        [HttpGet]
        public HttpResponseMessage CustomerAssignQueryTransferSummary()
        {
            MdlAssignedQueryTransfer values = new MdlAssignedQueryTransfer();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCustomerAssignQueryTransferSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CustomerAssignQueryTransfer360")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAssignQueryTransfer360(string email_gid)
        {
            MdlAssignQueryTransfer360 values = new MdlAssignQueryTransfer360();
            objDaAccess.DaCustomerAssignQueryTransfer360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ComposeMail360")]
        [HttpGet]
        public HttpResponseMessage ComposeMail360(string email_gid)
        {
            MdlComposeMail360List values = new MdlComposeMail360List();
            objDaAccess.DaComposeMail360(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AssignedQueryCount")]
        [HttpGet]
        public HttpResponseMessage AssignedQueryCount()
        {
            AssignedQueryCount values = new AssignedQueryCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaAssignedQueryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("QueryAssignmentCount")]
        [HttpGet]
        public HttpResponseMessage QueryAssignmentCount()
        {
            QueryAssignmentCount values = new QueryAssignmentCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaQueryAssignmentCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TransferEmployee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            MdlTransferEmployeeList values = new MdlTransferEmployeeList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaTransferEmployee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MailSeen")]
        [HttpGet]
        public HttpResponseMessage MailSeen(string email_gid)
        {
            objDaAccess.PostEmailSeen(email_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("ReferenceMail")]
        [HttpGet]
        public HttpResponseMessage ReferenceMail(string email_gid)
        {
            MdlQueryview values = new MdlQueryview();
            objDaAccess.DaReferenceMail(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
