using ems.hrloan.DataAccess;
using ems.hrloan.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Web;

namespace ems.hrloan.Controllers
{
    [RoutePrefix("api/MstHRLoanDrmApproval")]
    [Authorize]
    public class MstHRLoanDrmApprovalController : ApiController
    {
        DaMstHRLoanDrmApproval objDaHRLoanRequestdtl = new DaMstHRLoanDrmApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //droup down
        //Get Events - Drop Down
        [ActionName("UploadDocumentsDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentsDelete(string hrreuploaddocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlReuploaddeletDocument values = new MdlReuploaddeletDocument();
            objDaHRLoanRequestdtl.DaUploadDocumentsDelete(hrreuploaddocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //
        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlhrDropDownList values = new MdlhrDropDownList();
            objDaHRLoanRequestdtl.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UploadList")]
        [HttpGet]
        public HttpResponseMessage UploadList(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlhrDocument values = new MdlhrDocument();
            objDaHRLoanRequestdtl.DaUploadList(request_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //
        [ActionName("GetUploadDocumentsList")]
        [HttpGet]
        public HttpResponseMessage GetUploadDocumentsList(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlhrreuploadDocument values = new MdlhrreuploadDocument();
            objDaHRLoanRequestdtl.DaGetUploadDocumentsList(request_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("HRLoanReUpload")]
        [HttpPost]
        public HttpResponseMessage HRLoanReUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument2 documentname = new uploaddocument2();
            objDaHRLoanRequestdtl.ReUploadDocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);

        }
        [ActionName("GetHRloanRequestDetailscount")]
        [HttpGet]
        public HttpResponseMessage GetHRloanRequestDetailscount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetHRloanRequestDetailscount(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHRloanRequestDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanRequestDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetHRloanRequestDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadRequestDetailscount")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadRequestDetailscount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetHRloanHRheadRequestDetailscount(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadRequestDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadRequestDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetHRloanHRheadRequestDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetHRloanApprovalSummary(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetHRloanApprovalSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanDRMApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanDRMApprovalUpdate(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoanDRMApprovalUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanDRMRejectUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanDRMRejectUpdate(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoanDRMRejectUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanFHApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanFHApprovalUpdate(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoanFHApprovalUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanFHRejectUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanFHRejectUpdate(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoanFHRejectUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanHRApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanHRApprovalUpdate(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoanHRApprovalUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanHRRejectUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanHRRejectUpdate(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoanHRRejectUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DaGetHRloanRequestviewDetails")]
        [HttpGet]
        public HttpResponseMessage DaGetHRloanRequestviewDetails(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            requestsummary values = new requestsummary();
            objDaHRLoanRequestdtl.DaGetHRloanRequestviewDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDRMRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostDRMRaiseQuery(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostDRMRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostFHRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostFHRaiseQuery(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostFHRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHRRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostHRRaiseQuery(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHRRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDRMRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetDRMRaiseQuery(string request_gid)
        {
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetDRMRaiseQuery(request_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFHRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetFHRaiseQuery(string request_gid)
        {
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetFHRaiseQuery(request_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetHRRaiseQuery(string request_gid)
        {
            MdlMstHRLoanDrmApproval values = new MdlMstHRLoanDrmApproval();
            objDaHRLoanRequestdtl.DaGetHRRaiseQuery(request_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDRMresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostDRMresponsequery(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostDRMresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostFHresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostFHresponsequery(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostFHresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHRresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostHRresponsequery(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHRresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoantremsandconditionacknwlg")]
        [HttpPost]
        public HttpResponseMessage PostHrLoantremsandconditionacknwlg(MdlMstHRLoanDrmApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequestdtl.DaPostHrLoantremsandconditionacknwlg(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}