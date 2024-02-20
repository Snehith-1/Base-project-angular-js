using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.lp.Models;
using ems.lp.DataAccess;
using ems.utilities.Functions;
using System.Web;

namespace ems.lp.Controllers
{
    [RoutePrefix("api/LawyerInvoice")]
    [AllowAnonymous]

    public class LawyerInvoiceController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLawyerInvoice objDaLawyerInvoice = new DaLawyerInvoice();

        [ActionName("postinvoicedetails")]
        [HttpPost]
        public HttpResponseMessage postinvoicedetails(invoicedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            invoicedtl objinvoicedtl = new invoicedtl();
             objDaLawyerInvoice .DaPostInvoiceDetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }

        [ActionName("getinvoicedtlList")]
        [HttpGet]
        public HttpResponseMessage getinvoicedtlList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            invoicedtlList objinvoicedtl = new invoicedtlList();
            objDaLawyerInvoice.DaGetInvoicedtlList( getsessionvalues. user_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }

        [ActionName("tmpdocumentdelete")]
        [HttpGet]
        public HttpResponseMessage tmpdocumentdelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            document values = new document();
            objDaLawyerInvoice .DaTmpDocumentDelete(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getinvoicedetails")]
        [HttpGet]
        public HttpResponseMessage getsanctiondetails(string lawyerinvoicedtl_gid)
        {
            invoicedtl objinvoicedtl = new invoicedtl();
            objDaLawyerInvoice .DaGetInvoicedtl(lawyerinvoicedtl_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }

        [ActionName("getcanceldocument")]
        [HttpGet]
        public HttpResponseMessage getcanceldocument(string invoice_documentgid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocument_name objresult = new UploadDocument_name();
            objDaLawyerInvoice .DaGetCancelDocument (invoice_documentgid, objresult,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UploadDocument")]
        [HttpPost]
        public HttpResponseMessage PostUploadDocument()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocument_name documentname = new UploadDocument_name();
          objDaLawyerInvoice.DaGetDocumentUpload (httpRequest,getsessionvalues. user_gid, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("EditUploadDocument")]
        [HttpPost]
        public HttpResponseMessage EditUploadDocument()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocument_name documentname = new UploadDocument_name();
            objDaLawyerInvoice.DaEditUploadDocument(httpRequest, getsessionvalues.user_gid, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Getlegalsr")]
        [HttpGet]
        public HttpResponseMessage Getlegalsr()
        {
          string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mslcasedtl objinvoicedtl = new Mslcasedtl();
            objDaLawyerInvoice.DaGetlegalsr( getsessionvalues.user_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }
        [ActionName("Getlegalservices")]
        [HttpGet]
        public HttpResponseMessage Getlegalservices(string case_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
         Mslcasedtl objinvoicedtl = new Mslcasedtl();
            objDaLawyerInvoice.DaGetlegalservices(case_type,getsessionvalues.user_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }

        [ActionName("updateinvoicedetails")]
        [HttpPost]
        public HttpResponseMessage updateinvoicedetails(invoicedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
        //    invoicedtl objinvoicedtl = new invoicedtl();
            objDaLawyerInvoice.Daupdateinvoicedetails(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("geteditdocument")]
        [HttpGet]
        public HttpResponseMessage geteditdocument(string lawyerinvoicedtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocument_name objresult = new UploadDocument_name();
            objDaLawyerInvoice.geteditdocument(lawyerinvoicedtl_gid, getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("updateinvoicestatus")]
        [HttpPost]
        public HttpResponseMessage updateinvoicestatus(invoicedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            //    invoicedtl objinvoicedtl = new invoicedtl();
            objDaLawyerInvoice.Daupdateinvoicestatus(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCaseType")]
        [HttpGet]
        public HttpResponseMessage GetCaseType()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mslcasedtl objinvoicedtl = new Mslcasedtl();
            objDaLawyerInvoice.GetCaseType(getsessionvalues.user_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }
        [ActionName("Geteditlegalservices")]
        [HttpGet]
        public HttpResponseMessage Geteditlegalservices()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mslcasedtl objinvoicedtl = new Mslcasedtl();
            objDaLawyerInvoice.DaGeteditlegalservices( getsessionvalues.user_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }
        [ActionName("GetPaymentSummary")]
        [HttpGet]
        public HttpResponseMessage GetPaymentSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            invoicedtlList objinvoicedtl = new invoicedtlList();
            objDaLawyerInvoice.DaGetPaymentSummary(getsessionvalues.employee_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }
        [ActionName("GetinvoiceListSummary")]
        [HttpGet]
        public HttpResponseMessage GetinvoiceListSummary(string caseref_gid)
        {
         
            invoicedtlList objinvoicedtl = new invoicedtlList();
            objDaLawyerInvoice.DaGetinvoiceListSummary(caseref_gid, objinvoicedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objinvoicedtl);
        }
    }
}
