using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.lgl.Models;
using ems.utilities.Functions;
using System.Web;
using ems.lgl.DataAccess;

namespace StoryboardAPI.Controllers.ems.lgl
{
    [RoutePrefix("api/CustomerDashboard")]
    [Authorize]

    public class CustomerDashboardController : ApiController
    {
       
        DaCustomerDashboard objdaCustomerDashboard = new DaCustomerDashboard();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("Getcustomerloandetails")]
        [HttpGet]
        public HttpResponseMessage GetCustomerLoanDetails(string customer_gid)
        {
            loanlist values = new loanlist();
            objdaCustomerDashboard.DaGetCustomerLoanDetails(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetlawyerSRassign")]
        [HttpGet]
        public HttpResponseMessage GetLawyerSRAssign(string legalSR_gid)
        {
            assignSRLawyer values = new assignSRLawyer();
            objdaCustomerDashboard.DaGetLawyerSRAssign(values, legalSR_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomerdocumentdetails")]
        [HttpGet]
        public HttpResponseMessage GetCustomerDocumentDetails(string customer_gid)
        {
            UploadDocument_name values = new UploadDocument_name();
            objdaCustomerDashboard.DaGetCustomerDocumentDetails(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("assignSRLawyer")]
        [HttpPost]
        public HttpResponseMessage PostAssignCompliance(assignlawyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            assignlawyer objvalues = new assignlawyer();
            var status = objdaCustomerDashboard.DaPostAssignCompliance(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomermaildetails")]
        [HttpGet]
        public HttpResponseMessage GetCustomerMailDetails(string customer_gid)
        {
            composemail_list values = new composemail_list();
            objdaCustomerDashboard.DaGetCustomerMailDetails(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomermail")]
        [HttpGet]
        public HttpResponseMessage GetCustomerMail(string composemail_gid)
        {
            composemail values = new composemail();
            objdaCustomerDashboard.DaGetCustomerMail(values, composemail_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UploadDocument")]
        [HttpPost]
        public HttpResponseMessage PostDocumentUpload()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocument_name documentname = new UploadDocument_name();
            objdaCustomerDashboard.DaPostDocumentUpload(httpRequest, getsessionvalues.employee_gid, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Compose Mail //
        [ActionName("sendcomposemail")]
        [HttpPost]
        public HttpResponseMessage PostComposeMail(composemail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaCustomerDashboard.DaPostComposeMail(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDemandNotice")]
        [HttpGet]
        public HttpResponseMessage GetDemandNotice(string customer_gid)
        {
            demandnotice values = new demandnotice();
            var status = objdaCustomerDashboard.DaGetDemandNotice(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetLawyerPayment")]
        [HttpGet]
        public HttpResponseMessage GetLawyerPayment()
        {
            invoicedtlList values = new invoicedtlList();
            objdaCustomerDashboard.DaGetLawyerPayment(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
