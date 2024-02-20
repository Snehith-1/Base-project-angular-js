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
    [RoutePrefix("api/OsdTrnServiceRequest")]
    [Authorize]

    public class OsdTrnServiceRequestController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdTrnServiceRequest objDaOsdTrnServiceRequest = new DaOsdTrnServiceRequest();

        [ActionName("GetActivityList")]
        [HttpGet]
        public HttpResponseMessage GetActivityList()
        {
            actvitydtllist values = new actvitydtllist();
            objDaOsdTrnServiceRequest.DaGetActivityList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeptActivity")]
        [HttpGet]
        public HttpResponseMessage GetDeptActivity(string department_gid)
        {
            actvitydtllist values = new actvitydtllist();
            objDaOsdTrnServiceRequest.DaGetDeptActivity(values, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("RequestDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PostDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaOsdTrnServiceRequest.DaPostDocumentUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GettmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GettmpDocumentDelete(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            uploaddocument objvalues = new uploaddocument();
            objDaOsdTrnServiceRequest.DaGettmpDocumentDelete(tmp_documentGid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("PostServiceRequest")]
        [HttpPost]
        public HttpResponseMessage PostServiceRequest(servicerequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaPostServiceRequest(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestSummary")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            servicerequestdtllist values = new servicerequestdtllist();
            objDaOsdTrnServiceRequest.DaGetServiceRequestSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestView")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestView(string servicerequest_gid)
        {

            servicerequestview values = new servicerequestview();
            objDaOsdTrnServiceRequest.DaGetServiceRequestView(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage Gettempdelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaOsdTrnServiceRequest.DaGettempdelete(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetClosedRequest")]
        [HttpGet]
        public HttpResponseMessage GetClosedRequest(string servicerequest_gid)
        {
            completed values = new completed();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetClosedRequest(getsessionvalues.user_gid, servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCloseSummary")]
        [HttpGet]
        public HttpResponseMessage GetCloseSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            servicerequestdtllist values = new servicerequestdtllist();
            objDaOsdTrnServiceRequest.DaGetCloseSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaggedSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaggedSummary()
        {
            taggedlist values = new taggedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetTaggedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestCount")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestCount()
        {
            requestcount values = new requestcount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetServiceRequestCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetForwardRequestSummary")]
        [HttpGet]
        public HttpResponseMessage GetForwardRequestSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            forwardrequestdtllist values = new forwardrequestdtllist();
            objDaOsdTrnServiceRequest.DaGetForwardRequestSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RequestViewDocUpload")]
        [HttpPost]
        public HttpResponseMessage PostRequestViewDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            servicerequestview documentname = new servicerequestview();
            objDaOsdTrnServiceRequest.DaPostRequestViewDocUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetTrnDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GetTrnDocumentDelete(string servicereqdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            servicerequestview objvalues = new servicerequestview();
            objDaOsdTrnServiceRequest.DaGetTrnDocumentDelete(servicereqdocument_gid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("ForwardViewDocUpload")]
        [HttpPost]
        public HttpResponseMessage PostForwardViewDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            allotteddtl documentname = new allotteddtl();
            objDaOsdTrnServiceRequest.DaPostForwardViewDocUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }


        [ActionName("GetTrnForwardDocDelete")]
        [HttpGet]
        public HttpResponseMessage GetTrnForwardDocDelete(string forwardreqdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            allotteddtl objvalues = new allotteddtl();
            objDaOsdTrnServiceRequest.DaGetTrnForwardDocDelete(forwardreqdocument_gid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

   // Reopen Submit
        [ActionName("PostReopenRequest")]
        [HttpPost]
        public HttpResponseMessage PostReopenRequest(reopenrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaPostReopenRequest(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

   // Reopen Document Upload
        [ActionName("PostReopenDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PostReopenDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            servicerequestview documentname = new servicerequestview();
            objDaOsdTrnServiceRequest.DaPostReopenDocumentUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
  // Reopen Document Delete
        [ActionName("GetReopenDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GetReopenDocumentDelete(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            servicerequestview objvalues = new servicerequestview();
            objDaOsdTrnServiceRequest.DaGetReopenDocumentDelete(tmp_documentGid, values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
    // Delete Service Request
        [ActionName("ServiceRequestDelete")]
        [HttpGet]
        public HttpResponseMessage ServiceRequestDelete(string servicerequest_gid)
        {
            servicerequestview values = new servicerequestview();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaServiceRequestDelete(values, servicerequest_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Exclude Already Tagged poeple 
        [ActionName("TagEmployee")]
        [HttpGet]
        public HttpResponseMessage getEmployee(string servicerequest_gid)
        {
            MdlEmployee objMdlEmployee = new MdlEmployee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetEmployee(objMdlEmployee, servicerequest_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        // Get employees without who raised the ticket
        [ActionName("GetEmployees")]
        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEmployee values = new MdlEmployee();
            objDaOsdTrnServiceRequest.DaGetEmployees(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Reopen Summary
        [ActionName("GetReopenSummary")]
        [HttpGet]
        public HttpResponseMessage GetReopenSummary()
        {
            reopenreqlist values = new reopenreqlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetReopenSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Rejected Summary
        [ActionName("GetRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedSummary()
        {
            rejectedreqlist values = new rejectedreqlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetRejectedSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Cancelled Summary
        [ActionName("GetCancelledSummary")]
        [HttpGet]
        public HttpResponseMessage GetCancelledSummary()
        {
            cancelledreqlist values = new cancelledreqlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetCancelledSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tag Member in Chat
        [ActionName("PostTagMemberInChat")]
        [HttpPost]
        public HttpResponseMessage PostTagMemberInChat(reopenrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaPostTagMemberInChat(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestViewUpdate")]
        [HttpGet]
        public HttpResponseMessage ServiceRequestViewUpdate(string servicerequest_gid)
        {
            servicerequestview values = new servicerequestview();
            objDaOsdTrnServiceRequest.DaGetServiceRequestViewUpdate(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSendRequestorCreate")]
        [HttpPost]
        public HttpResponseMessage PostSendRequestorCreate(requestordtl values)
        {
            requestordtl objquerydetails = new requestordtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaPostSendRequestorCreate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestForwardViewUpdate")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestForwardViewUpdate(string servicerequest_gid)
        {
            servicerequestview values = new servicerequestview();
            objDaOsdTrnServiceRequest.DaGetServiceRequestForwardViewUpdate(servicerequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSendRequestorForward")]
        [HttpPost]
        public HttpResponseMessage PostSendRequestorForward(requestordtl values)
        {
            requestordtl objquerydetails = new requestordtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaPostSendRequestorForward(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceRequestTagViewUpdate")]
        [HttpGet]
        public HttpResponseMessage GetServiceRequestTagViewUpdate(string servicerequest_gid)
        {
            servicerequestview values = new servicerequestview();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnServiceRequest.DaGetServiceRequestTagViewUpdate(servicerequest_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
