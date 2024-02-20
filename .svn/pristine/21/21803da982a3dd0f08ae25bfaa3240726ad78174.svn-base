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
    [RoutePrefix("api/TrnHRLoanHRPayment")]
    [Authorize]
    public class TrnHRLoanHRPaymentController : ApiController
    {
        DaTrnHRLoanHRPayment objDaHRLoanHRPayment = new DaTrnHRLoanHRPayment();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHRloanHRheadPaymentDetailscount")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadPaymentDetailscount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRPayment values = new MdlTrnHRLoanHRPayment();
            objDaHRLoanHRPayment.DaGetHRloanHRheadPaymentDetailscount(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadPaymentDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadPaymentDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRPayment values = new MdlTrnHRLoanHRPayment();
            objDaHRLoanHRPayment.DaGetHRloanHRheadPaymentDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHrLoanHRPaymentUpdate")]
        [HttpPost]
        public HttpResponseMessage PostHrLoanHRPaymentUpdate(MdlTrnHRLoanHRPayment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaHRLoanHRPayment.DaPostHrLoanHRPaymentUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanDetailsApprovedPayment")]
        [HttpGet]
        public HttpResponseMessage GetHRloanDetailsApprovedPayment()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnHRLoanHRPayment objHRLoanApprovedPayment = new MdlTrnHRLoanHRPayment();
            objDaHRLoanHRPayment.DaGetHRloanDetailsApprovedPayment(objHRLoanApprovedPayment, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanApprovedPayment);
        }

    }
}