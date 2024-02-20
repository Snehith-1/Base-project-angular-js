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
    [RoutePrefix("api/TrnHRLoanHRVerifications")]
    [Authorize]
    public class TrnHRLoanHRVerificationsController : ApiController
    {
        DaTrnHRLoanHRVerifications objDaHRLoanHRVerifications = new DaTrnHRLoanHRVerifications();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHRloanHRheadVerificationsDetailscount")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadVerificationsDetailscount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRVerifications values = new MdlTrnHRLoanHRVerifications();
            objDaHRLoanHRVerifications.DaGetHRloanHRheadVerificationsDetailscount(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadVerificationsDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadVerificationsDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRVerifications values = new MdlTrnHRLoanHRVerifications();
            objDaHRLoanHRVerifications.DaGetHRloanHRheadVerificationsDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadVerificationsDetailsApproved")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadVerificationsDetailsApproved()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRVerifications values = new MdlTrnHRLoanHRVerifications();
            objDaHRLoanHRVerifications.DaGetHRloanHRheadVerificationsDetailsApproved(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadVerificationsDetailsRejected")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadVerificationsDetailsRejected()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRVerifications values = new MdlTrnHRLoanHRVerifications();
            objDaHRLoanHRVerifications.DaGetHRloanHRheadVerificationsDetailsRejected(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRLoanDropDown")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRVerifications values = new MdlTrnHRLoanHRVerifications();
            objDaHRLoanHRVerifications.DaGetHRLoanDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempDocumentsList")]
        [HttpGet]
        public HttpResponseMessage TempDocumentsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload1 values = new MdlHRLoanDocumentUpload1();
            objDaHRLoanHRVerifications.DaTempDocumentsList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUploadList")]
        [HttpGet]
        public HttpResponseMessage GetUploadList(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload1 values = new MdlHRLoanDocumentUpload1();
            objDaHRLoanHRVerifications.DaGetUploadList(request_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HRLoanDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage HRLoanDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument1 documentname = new uploaddocument1();
            objDaHRLoanHRVerifications.DaHRLoanDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);

        }
        [ActionName("HRLoanDocumentList")]
        [HttpGet]
        public HttpResponseMessage HRLoanDocumentList(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload1 values = new MdlHRLoanDocumentUpload1();
            objDaHRLoanHRVerifications.DaHRLoanDocumentList(request_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocumentsDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentsDelete(string hrspecialdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanDocumentUpload1 values = new MdlHRLoanDocumentUpload1();
            objDaHRLoanHRVerifications.DaUploadDocumentsDelete(hrspecialdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoantermsandcondtn")]
        [HttpPost]
        public HttpResponseMessage PostHrLoantermsandcondtn(MdlTrnHRLoanHRVerifications values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRVerifications.DaPostHrLoantermsandcondtn(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("termsandcondtnview")]
        [HttpGet]
        public HttpResponseMessage termsandcondtnview(string request_gid)
        {
            Mdltermsandcondt objMdltermsandcondt = new Mdltermsandcondt();
            objDaHRLoanHRVerifications.Datermsandcondtnview(objMdltermsandcondt, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdltermsandcondt);
        }
        [ActionName("GetTCflag")]
        [HttpGet]
        public HttpResponseMessage GetTCflag(string request_gid)
        {
            Mdltermsandcondt objMdltermsandcondt = new Mdltermsandcondt();
            objDaHRLoanHRVerifications.DaGetTCflag(objMdltermsandcondt, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdltermsandcondt);
        }
        [ActionName("PostHrLoanHRVerifyApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanHRVerifyApprovalUpdate(MdlTrnHRLoanHRVerifications values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRVerifications.DaPostHrLoanHRVerifyApprovalUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanHRVerifyRejectUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanHRVerifyRejectUpdate(MdlTrnHRLoanHRVerifications values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRVerifications.DaPostHrLoanHRVerifyRejectUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanVerifyApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanVerifyApprovalUpdate(MdlTrnHRLoanHRVerifications values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRVerifications.DaPostHrLoanVerifyApprovalUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostManagerRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostManagerRaiseQuery(MdlTrnHRLoanHRVerifications values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRVerifications.DaPostManagerRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetManagerRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetManagerRaiseQuery(string request_gid)
        {
            MdlTrnHRLoanHRVerifications values = new MdlTrnHRLoanHRVerifications();
            objDaHRLoanHRVerifications.DaGetManagerRaiseQuery(request_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostManagerresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostManagerresponsequery(MdlTrnHRLoanHRVerifications values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRVerifications.DaPostManagerresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HRLoanPaymentDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage HRLoanPaymentDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadpaymentdocument documentname = new uploadpaymentdocument();
            objDaHRLoanHRVerifications.DaHRLoanPaymentDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);

        }
        [ActionName("HRLoanPaymentDocumentList")]
        [HttpGet]
        public HttpResponseMessage HRLoanPaymentDocumentList(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanPaymentDocumentUpload values = new MdlHRLoanPaymentDocumentUpload();
            objDaHRLoanHRVerifications.DaHRLoanPaymentDocumentList(request_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadPaymentDocumentsDelete")]
        [HttpGet]
        public HttpResponseMessage UploadPaymentDocumentsDelete(string hrrepaymentdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHRLoanPaymentDocumentUpload values = new MdlHRLoanPaymentDocumentUpload();
            objDaHRLoanHRVerifications.DaUploadPaymentDocumentsDelete(hrrepaymentdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HRLoanGetApprovedInterest")]
        [HttpGet]
        public HttpResponseMessage HRLoanGetApprovedInterest(string request_gid)
        {
            MdlHRLoanPaymentDocumentUpload values = new MdlHRLoanPaymentDocumentUpload();
            objDaHRLoanHRVerifications.DaHRLoanGetApprovedInterest(request_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}