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
    [RoutePrefix("api/OsdTrnMyTicket")]
    [Authorize]


    public class OsdTrnMyTicketController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdTrnMyTicket objDaOsdTrnMyTicket = new DaOsdTrnMyTicket();

        [ActionName("GetAllottedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllottedSummary()
        {
            allottedlist values = new allottedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetAllottedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessUnitStatusMyActivityTempClear")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnitStatusMyActivityTempClear()
        {
            allottedlist values = new allottedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetBusinessUnitStatusMyActivityTempClear(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetForwardDocumentUploadtempclear")]
        [HttpGet]
        public HttpResponseMessage GetForwardDocumentUploadtempclear()
        {
            allottedlist values = new allottedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetForwardDocumentUploadtempclear(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCompleteDocumentUploadtempclear")]
        [HttpGet]
        public HttpResponseMessage GetCompleteDocumentUploadtempclear()
        {
            allottedlist values = new allottedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetCompleteDocumentUploadtempclear(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessUnitStatusMyActivityDrop")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnitStatusMyActivityDrop()
        {
            allottedlist values = new allottedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetBusinessUnitStatusMyActivityDrop(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessUnitStatusMyActivityList")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnitStatusMyActivityList(string servicerequest_gid)
        {
            allottedlist values = new allottedlist();
            objDaOsdTrnMyTicket.DaGetBusinessUnitStatusMyActivityList(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetservicerequestactivityhistoryList")]
        [HttpGet]
        public HttpResponseMessage GetservicerequestactivityhistoryList(string servicerequest_gid)
        {
            allottedlist values = new allottedlist();
            objDaOsdTrnMyTicket.DaGetservicerequestactivityhistoryList(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessUnitStatusMyActivityComplete")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnitStatusMyActivityComplete(string servicerequest_gid)
        {
            allottedlist values = new allottedlist();
            objDaOsdTrnMyTicket.DaGetBusinessUnitStatusMyActivityComplete(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBusinessUnitStatusMyActivity")]
        [HttpGet]
        public HttpResponseMessage DeleteBusinessUnitStatusMyActivity(string businessstatusactivity_gid)
        {
            allottedlist values = new allottedlist();
            objDaOsdTrnMyTicket.DaDeleteBusinessUnitStatusMyActivity(businessstatusactivity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWorkInProgressSummary")]
        [HttpGet]
        public HttpResponseMessage GetWorkInProgressSummary()
        {
            workinprogresslist values = new workinprogresslist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetWorkInProgressSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompletedSummary()
        {
            completedlist values = new completedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetClosedSummary")]
        [HttpGet]
        public HttpResponseMessage GetClosedSummary()
        {
            closedlist values = new closedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetClosedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        // Allotted360 View
        [ActionName("GetAllotted360")]
        [HttpGet]
        public HttpResponseMessage GetAllotted360(string servicerequest_gid)
        {
            allotteddtl values = new allotteddtl();
            objDaOsdTrnMyTicket.DaGetAllotted360(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        [ActionName("Gettempclear")]
        [HttpGet]
        public HttpResponseMessage Gettempclear()
        {
            approvallist values = new approvallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGettempclear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        // Acknowledgement API
        [ActionName("PostUpdateAck")]
        [HttpPost]
        public HttpResponseMessage PostUpdateAck(allotteddtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostUpdateAck(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Acknowledgement Completed API
        [ActionName("PostAckCompleteRequest")]
        [HttpPost]
        public HttpResponseMessage PostAckCompleteRequest(ackcomplete values) 
        {
        
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostAckCompletedRequest(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostSendRequestor")]
        [HttpPost]
        public HttpResponseMessage PostSendRequestor(requestordtl values)
        {
            requestordtl objquerydetails = new requestordtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostSendRequestor(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("ConversationDocUpload")]
        [HttpPost]
        public HttpResponseMessage conversationdocupload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            upload_document documentname = new upload_document();
            objDaOsdTrnMyTicket.DaPostConverseUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetRequestorlist")]
        [HttpGet]
        public HttpResponseMessage GetRequestorlist(string servicerequest_gid)
        {
            requestorlist objquerydetails = new requestorlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetRequestorlist(getsessionvalues.employee_gid, servicerequest_gid, objquerydetails);
            return Request.CreateResponse(HttpStatusCode.OK, objquerydetails);
        }
        [ActionName("GetBusinessunitStatusMyActivity")]
        [HttpGet]
        public HttpResponseMessage GetBusinessunitStatusMyActivity(string servicerequest_gid)
        {
            requestorlist objquerydetails = new requestorlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetBusinessunitStatusMyActivity(servicerequest_gid, objquerydetails);
            return Request.CreateResponse(HttpStatusCode.OK, objquerydetails);
        }

        //Get My Activity Count 
        [ActionName("GetActivityCount")]
        [HttpGet]
        public HttpResponseMessage GetActivityCount()
        {
            countlist values = new countlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetActivityCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("PostrequestRejected")]
        //[HttpPost]
        //public HttpResponseMessage PostrequestRejected(completed values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaOsdTrnMyTicket.DaPostrequestRejected(getsessionvalues.user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("GetCompletedRequest")]
        [HttpPost]
        public HttpResponseMessage PostCompletedRequest(completed values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostCompletedRequest(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostApprovalGet")]
        [HttpPost]
        public HttpResponseMessage PostApprovalGet(approvaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostApprovalGet(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBusinessUnitStatusMyActivity")]
        [HttpPost]
        public HttpResponseMessage PostBusinessUnitStatusMyActivity(forwarddtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostBusinessUnitStatusMyActivity(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBusinessUnitStatusWorkinProgress")]
        [HttpPost]
        public HttpResponseMessage PostBusinessUnitStatusWorkinProgress(forwarddtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostBusinessUnitStatusWorkinProgress(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetApprovalDtls(string servicerequest_gid)
        {
            approvallist objvalues = new approvallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetApprovalDtls(servicerequest_gid, getsessionvalues.employee_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }


        [ActionName("GetAssetDtls")]
        [HttpGet]
        public HttpResponseMessage GetAssetDtls(string servicerequest_gid)
        {
            approvallist values = new approvallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetAssetDtls(getsessionvalues.employee_gid, values, servicerequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Forward API
        [ActionName("ForwardTicket")]
        [HttpPost]
        public HttpResponseMessage ForwardTicket(forwarddtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostForwardTicket(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetForwardSummary")]
        [HttpGet]
        public HttpResponseMessage GetForwardSummary()
        {
            forwardlist values = new forwardlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetForwardSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Allotted360 View
        [ActionName("GetForward360")]
        [HttpGet]
        public HttpResponseMessage GetForward360(string servicerequest_gid)
        {
            allotteddtl values = new allotteddtl();
            objDaOsdTrnMyTicket.DaGetForward360(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ForwardDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PostForwardDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaOsdTrnMyTicket.DaPostForwardDocumentUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetForwardtmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GetForwardtmpDocumentDelete(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            uploaddocument objvalues = new uploaddocument();
            objDaOsdTrnMyTicket.DaGetForwardtmpDocumentDelete(tmp_documentGid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        // Transfer Summary
        [ActionName("GetTransferSummary")]
        [HttpGet]
        public HttpResponseMessage GetTransferSummary()
        {
            transferlist values = new transferlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetTransferSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Confirm Document Upload

        [ActionName("CompleteDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PostCompleteDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaOsdTrnMyTicket.DaPostCompleteDocumentUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        // Delete Temp Doc Deltails on Completed button
        [ActionName("GettmpCompleteDocDelete")]
        [HttpGet]
        public HttpResponseMessage GettmpCompleteDocDelete(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            uploaddocument objvalues = new uploaddocument();
            objDaOsdTrnMyTicket.DaGettmpCompleteDocDelete(tmp_documentGid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        // Multiple forward details
        [ActionName("GetMultipleForward")]
        [HttpGet]
        public HttpResponseMessage GetMultipleForward(string servicerequest_gid)
        {
            forwardlist values = new forwardlist();
            objDaOsdTrnMyTicket.DaGetMultipleForward(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
  // Reopen History Details
        [ActionName("GetReopenHistory")]
        [HttpGet]
        public HttpResponseMessage GetReopenHistory(string requestreopen_gid)
        {
            reopenhistory values = new reopenhistory();
            objDaOsdTrnMyTicket.DaGetReopenHistory(requestreopen_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    // Get Completed Details
        [ActionName("GetCompletedDetails")]
        [HttpGet]
        public HttpResponseMessage GetCompletedDetails(string servicerequest_gid)
        {
            completedlist values = new completedlist();
            objDaOsdTrnMyTicket.DaGetCompletedDetails(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Reject
        [ActionName("RejectRequest")]
        [HttpPost]
        public HttpResponseMessage PostRejectRequest(reject values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostRejectRequest(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Members on Approval
        [ActionName("TempApprovalMember")]
        [HttpPost]
        public HttpResponseMessage PostTempApprovalMember(approvaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaPostTempApprovalMember(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TmpApprovalMembersView")]
        [HttpGet]
        public HttpResponseMessage GetTmpApprovalMembersView(string servicerequest_gid)
        {

            approvaldtl values = new approvaldtl();
            objDaOsdTrnMyTicket.DaGetTmpApprovalMembersView(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TmpApprovalMembersDelete")]
        [HttpPost]
        public HttpResponseMessage GetTmpApprovalMembersDelete(approvaldtl values)
        {
            objDaOsdTrnMyTicket.DaGetTmpApprovalMembersDelete(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TmpAllMembersDeleteFn")]
        [HttpGet]
        public HttpResponseMessage GetTmpAllMembersDeleteFn(approvaldtl values, string servicerequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetTmpAllMembersDeleteFn(values, servicerequest_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TmpAllMembersDelete")]
        [HttpGet]
        public HttpResponseMessage GetTmpAllMembersDelete(approvaldtl values)
        {
            
            objDaOsdTrnMyTicket.DaGetTmpAllMembersDelete(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmployeeNotIn")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeNotIn(string servicerequest_gid)
        {

            supportteamviewdtl values = new supportteamviewdtl();
            objDaOsdTrnMyTicket.DaGetEmployeeNotIn(values, servicerequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Approval Details Based on RequestApproval Gid
        [ActionName("GetApprovalDtlsByToken")]
        [HttpGet]
        public HttpResponseMessage GetApprovalDtlsByToken(string requestapproval_gid)
        {
            approvallist objvalues = new approvallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetApprovalDtlsByToken(requestapproval_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetServiceRequestForwardView360Update")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestForwardView360Update(string servicerequest_gid)
        {
            servicerequestview values = new servicerequestview();
            objDaOsdTrnMyTicket.DaGetServiceRequestForwardView360Update(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestTransferView360Update")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestTransferView360Update(string servicerequest_gid)
        {
            servicerequestview values = new servicerequestview();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetServiceRequestTransferView360Update(servicerequest_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetRequestRemarks")]
        [HttpGet]
        public HttpResponseMessage GetRequestRemarks(string requestapproval_gid)
        {
            requestapproval values = new requestapproval();
            objDaOsdTrnMyTicket.DaGetRequestRemarks(values,requestapproval_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalPendingSummary()
        {
            allottedlist values = new allottedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnMyTicket.DaGetApprovalPendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
