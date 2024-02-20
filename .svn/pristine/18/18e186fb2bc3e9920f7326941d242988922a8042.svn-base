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
    [RoutePrefix("api/OsdTrnTicketManagement")]
    [Authorize]

    public class OsdTrnTicketManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdTrnTicketManagement objDaOsdTrnTicketManagement = new DaOsdTrnTicketManagement();

        [ActionName("GetServiceRequestSummary")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            servicerequestdtllist values = new servicerequestdtllist();
            objDaOsdTrnTicketManagement.DaGetServiceRequestSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyWorkInProgressSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyWorkInProgressSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            workinprogresslist values = new workinprogresslist();
            objDaOsdTrnTicketManagement.DaGetMyWorkInProgressSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            completedlist values = new completedlist();
            objDaOsdTrnTicketManagement.DaGetMyCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyClosedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyClosedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            closedlist values = new closedlist();
            objDaOsdTrnTicketManagement.DaGetMyClosedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRejectCancelSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectCancelSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            closedlist values = new closedlist();
            objDaOsdTrnTicketManagement.DaGetRejectCancelSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCountSummary")]
        [HttpGet]
        public HttpResponseMessage GetCountSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            countlist values = new countlist();
            objDaOsdTrnTicketManagement.DaGetCountSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // TransferAllocation
        [ActionName("PostTransferAllocation")]
        [HttpPost]
        public HttpResponseMessage PostTransferAllocation(transferdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaPostTransferAllocation(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPriority")]
        [HttpPost]
        public HttpResponseMessage PostPriority(transferdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaPostPriority(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Transfer Member Details
        [ActionName("GetTransferMemberlist")]
        [HttpGet]
        public HttpResponseMessage GetTransferMemberlist(string servicerequest_gid)
        {
            transferlist values = new transferlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaGetTransferMemberlist(values, getsessionvalues.employee_gid, servicerequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActivityEdit")]
        [HttpGet]
        public HttpResponseMessage GetActivityEdit(string servicerequest_gid)
        {
            transferdtl values = new transferdtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaGetActivityEdit(values, getsessionvalues.employee_gid, servicerequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPrioritylist")]
        [HttpGet]
        public HttpResponseMessage GetPrioritylist(string servicerequest_gid)
        {
            transferlist values = new transferlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaGetPrioritylist(values, getsessionvalues.employee_gid, servicerequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllForwardSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllForwardSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            forwardlist values = new forwardlist();
            objDaOsdTrnTicketManagement.DaGetAllForwardSummary(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("ZipDocument")]
        //[HttpPost]
        //public HttpResponseMessage PostUploadDocument(mdlzip values)
        //{

        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaOsdTrnTicketManagement.CreateDocumentationZipFile(values, getsessionvalues.employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("txtfile")]
        [HttpPost]
        public HttpResponseMessage txtfile(servicerequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.txtfile(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            servicerequestdtllist values = new servicerequestdtllist();
            objDaOsdTrnTicketManagement.DaGetApprovalPendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetManagerSummary")]
        [HttpGet]

        public HttpResponseMessage GetManagerSummary(string department_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            managerdtllist values = new managerdtllist();
            objDaOsdTrnTicketManagement.DaGetManagerSummary(values,department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSelfAllocation")]
        [HttpPost]
        public HttpResponseMessage PostSelfAllocation(allocatedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaPostSelfAllocation(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocateManagerlist")]
        [HttpGet]
        public HttpResponseMessage GetAllocateManagerlist(string servicerequest_gid)
        {
            selfallocatelist values = new selfallocatelist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnTicketManagement.DaGetAllocateManagerlist(values, getsessionvalues.employee_gid, servicerequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
