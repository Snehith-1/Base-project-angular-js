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
    [RoutePrefix("api/MstHRLoanRequest")]
    [Authorize]
    public class MstHRLoanRequestController : ApiController
    {
        DaMstHRLoanRequest objDaHRLoanRequest = new DaMstHRLoanRequest();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetFinType")]
        [HttpGet]
        public HttpResponseMessage GetFinType()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequest objHRLoanRequest = new MdlMstHRLoanRequest();
            objDaHRLoanRequest.DaGetFinType(objHRLoanRequest, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }

        [ActionName("GetSeverity")]
        [HttpGet]
        public HttpResponseMessage GetSeverity()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequest objHRLoanRequest = new MdlMstHRLoanRequest();
            objDaHRLoanRequest.DaGetSeverity(objHRLoanRequest, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }
        [ActionName("GetPurpose")]
        [HttpGet]
        public HttpResponseMessage GetPurpose()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequest objHRLoanRequest = new MdlMstHRLoanRequest();
            objDaHRLoanRequest.DaGetPurpose(objHRLoanRequest, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }
        
        [ActionName("GetEmployeeDetails")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequest objHRLoanRequest = new MdlMstHRLoanRequest();
            objDaHRLoanRequest.DaGetEmployeeDetails(objHRLoanRequest, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }
        [ActionName("PostHrloanRequest")]
        [HttpPost]
        public HttpResponseMessage PostHrloanRequest(hrloanrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequest.DaPostHRLoanRequest(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HRLoanRequestSaveasdraft")]
        [HttpPost]
        public HttpResponseMessage HRLoanRequestSaveasdraft(hrloanrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequest.DaHRLoanRequestSaveasdraft(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostHRLoanSaveasdraft")]
        [HttpPost]
        public HttpResponseMessage PostHRLoanSaveasdraft(hrloanrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequest.DaPostHRLoanSaveasdraft(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateHRLoanSaveasdraft")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanSaveasdraft(hrloanrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequest.DaUpdateHRLoanSaveasdraft(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage Gettempdelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaHRLoanRequest.DaGettempdelete(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanDetailscount")]
        [HttpGet]
        public HttpResponseMessage GetHRloanDetailscount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequest objHRLoanRequest = new MdlMstHRLoanRequest();
            objDaHRLoanRequest.DaGetHRloanDetailscount(objHRLoanRequest, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }
        [ActionName("GetHRloanDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequest objHRLoanRequest = new MdlMstHRLoanRequest();
           objDaHRLoanRequest.DaGetHRloanDetails(objHRLoanRequest, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
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
            objDaHRLoanRequest.DaPostDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
       
        
        [ActionName("GetTenureName")]
        [HttpGet]
        public HttpResponseMessage GetTenureName(string fintype_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            hrloanrequest objHRLoanRequest = new hrloanrequest();
            result values = new result();
            objDaHRLoanRequest.DaGetTenureName(objHRLoanRequest, values, getsessionvalues.employee_gid, fintype_name);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }
        [ActionName("GetTenureNameEdit")]
        [HttpGet]
        public HttpResponseMessage GetTenureNameEdit(string fintype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            hrloanrequest objHRLoanRequest = new hrloanrequest();
            result values = new result();
            objDaHRLoanRequest.DaGetTenureNameEdit(objHRLoanRequest, values, getsessionvalues.employee_gid, fintype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequest);
        }

        [ActionName("GetUploadDocumentsList")]
        [HttpGet]
        public HttpResponseMessage GetUploadDocumentsList(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload values = new MdlHRLoanDocumentUpload();
            objDaHRLoanRequest.DaGetUploadDocumentsList(request_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddUploadDocumentsList")]
        [HttpGet]
        public HttpResponseMessage GetAddUploadDocumentsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload values = new MdlHRLoanDocumentUpload();
            objDaHRLoanRequest.DaGetAddUploadDocumentsList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocumentsDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentsDelete(string hrreqdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload values = new MdlHRLoanDocumentUpload();
            objDaHRLoanRequest.DaUploadDocumentsDelete(hrreqdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("EditHRLoanRequest")]
        [HttpGet]
        public HttpResponseMessage EditHRLoanRequest(string request_gid)
        {
            hrloanrequest values = new hrloanrequest();
            objDaHRLoanRequest.DaEditHRLoanRequest(request_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRLoanRequest")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanRequest(hrloanrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequest.DaUpdateHRLoanRequest(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanwithdrawUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanwithdrawUpdate(hrloanrequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanRequest.DaPostHrLoanwithdrawUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRLoanStatusRequest")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanStatusRequest(string request_gid)
        {
            hrloanrequest values = new hrloanrequest();
            objDaHRLoanRequest.DaGetHRLoanStatusRequest(values, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}