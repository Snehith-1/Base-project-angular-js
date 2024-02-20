using ems.brs.Dataacess;
using ems.brs.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.brs.Controllers
{
    /// <summary>
    ///To upload KOTAK Statement it digitize the LMS trnascation to get matched/Nonmatched cases and bank transaction summary
    /// </summary>
    /// <remarks>Written by Motches</remarks>
    [RoutePrefix("api/KotakReconcillation")]
    [Authorize]
    public class KotakReconcillationController : ApiController
    {
        DaKotakReconcillation objDaKotakReconcillation = new DaKotakReconcillation();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Bank dropdown

        [ActionName("GetBank")]
        [HttpGet]
        public HttpResponseMessage GetBank()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlKotakReconcillation objKotakReconcillation = new MdlKotakReconcillation();
            objDaKotakReconcillation.DaGetBank(objKotakReconcillation, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objKotakReconcillation);
        }
        //BRS Kotak template excel
        [ActionName("BRSExcelSample")]
        [HttpPost]
        public HttpResponseMessage BRSExcelSample()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadExcel documentname = new UploadExcel();
            objDaKotakReconcillation.DaPostBRSExcelSample(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetTransactionSummary")]
        [HttpGet]
        public HttpResponseMessage GetTransactionSummary()
        {
            transactionlist values = new transactionlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaKotakReconcillation.DaGetTransactionSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBankTransactionMatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetBankTransactionMatchedSummary()
        {
            transactionlist values = new transactionlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaKotakReconcillation.DaGetBankTransactionMatchedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBankSummaryCounts")]
        [HttpGet]
        public HttpResponseMessage GetBankSummaryCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            bank_list values = new bank_list();
            objDaKotakReconcillation.DaGetBankSummaryCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteTransactiondata")]
        [HttpGet]
        public HttpResponseMessage DeleteTransactiondata(string banktransc_gid)
        {
            transactionlist values = new transactionlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaKotakReconcillation.DaDeleteTransactiondata(banktransc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUploadTemplateSummary")]
        [HttpGet]
        public HttpResponseMessage GetUploadTemplateSummary()
        {
            uploadlist values = new uploadlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaKotakReconcillation.DaGetUploadTemplateSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteTemplatedata")]
        [HttpGet]
        public HttpResponseMessage DeleteTemplatedata(string reconcildoc_gid)
        {
            transactionlist values = new transactionlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaKotakReconcillation.DaDeleteTemplatedata(reconcildoc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Bank Name List

        [ActionName("BankNameList")]
        [HttpGet]
        public HttpResponseMessage BankNameList()
        {
            transactionlist values = new transactionlist();
            objDaKotakReconcillation.DaBankNameList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}