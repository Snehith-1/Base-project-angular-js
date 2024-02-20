using ems.brs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.brs.Dataacess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.brs.Controllers
{
    [RoutePrefix("api/RepaymentReconcillation")]
    [Authorize]
    public class RepaymentReconcillationController:ApiController
    {
        DaRepaymentReconcillation objDaRepaymentReconcillation = new DaRepaymentReconcillation();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //BRS Repayment template excel
        [ActionName("BRSRepaymentExcelSample")]
        [HttpPost]
        public HttpResponseMessage BRSRepaymentExcelSample()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlRepaymentReconcillation documentname = new MdlRepaymentReconcillation();
            objDaRepaymentReconcillation.DaPostBRSRepaymentExcelSample(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetRepaymentPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentPendingSummary()
        {
            repaymentlist values = new repaymentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentPendingSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRepaymentMatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentMatchedSummary()
        {
            repaymentlist values = new repaymentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentMatchedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRepaymentSummaryCounts")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentSummaryCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            repaymentlist values = new repaymentlist();
            objDaRepaymentReconcillation.DaGetRepaymentSummaryCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRepaymentTemplateSummary")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentTemplateSummary()
        {
            uploadlist values = new uploadlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentTemplateSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetreConcillationSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetreConcillationSummary()
        //{
        //    cocillationlist values = new cocillationlist();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaRepaymentReconcillation.DaGetreConcillationSummary(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("GetCreditMatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditMatchedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetCreditMatchedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditPartialMatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditPartialMatchedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetCreditPartialMatchedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditUnmatchedUnassignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditUnmatchedUnassignedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetCreditUnmatchedUnassignedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditUnmatchedassignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditUnmatchedassignedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetCreditUnmatchedassignedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditclosedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditclosedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetCreditClosedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetunreConcillationSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetunreConcillationSummary()
        //{
        //    cocillationlist values = new cocillationlist();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaRepaymentReconcillation.DaGetunreConcillationSummary(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("GetDebitMatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDebitMatchedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetDebitMatchedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDebitPartialMatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDebitPartialMatchedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetDebitPartialMatchedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDebitUnmatchedUnassignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDebitUnmatchedUnassignedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetDebitUnmatchedUnassignedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDebitUnmatchedassignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDebitUnmatchedassignedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetDebitUnmatchedassignedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDebitclosedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDebitclosedSummary()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetDebitClosedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRepaymentdata")]
        [HttpGet]
        public HttpResponseMessage DeleteRepaymentdata(string bankrepaytransc_gid)
        {
            repaymentlist values = new repaymentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaDeleteRepaymentdata(bankrepaytransc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteRepaymentTemplatedata")]
        [HttpGet]
        public HttpResponseMessage DeleteRepaymentTemplatedata(string repayreconcildoc_gid)
        {
            transactionlist values = new transactionlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaDeleteRepaymentTemplatedata(repayreconcildoc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDebitCount")]
        [HttpGet]
        public HttpResponseMessage GetDebitCount()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetDebitCount(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditCount")]
        [HttpGet]
        public HttpResponseMessage GetCreditCount()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetCreditCount(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRepaymentTemplateCount")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentTemplateCount()
        {
            uploadlist values = new uploadlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentTemplateCount(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRepaymentUnReconcillationSummary")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentUnReconcillationSummary()
        {
            repayment_unrecocillationlist values = new repayment_unrecocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentUnReconcillationSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRepaymentReconcillationSummary")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentReconcillationSummary()
        {
            repayment_cocillationlist values = new repayment_cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentReconcillationSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRepaymentReconcillationCount")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentReconcillationCount()
        {
            repayment_cocillationlist values = new repayment_cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetRepaymentReconcillationCount(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLMSPartialhistory")]
        [HttpGet]
        public HttpResponseMessage GetLMSPartialhistory( string banktransc_gid)
        {
            repayment_cocillationlist values = new repayment_cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaGetLMSPartialhistory(values, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BRSLmsprocess")]
        [HttpPost]
        public HttpResponseMessage BRSLmsprocess(MdllmsTmprepay values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRepaymentReconcillation.DaBRSLmsprocess(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}