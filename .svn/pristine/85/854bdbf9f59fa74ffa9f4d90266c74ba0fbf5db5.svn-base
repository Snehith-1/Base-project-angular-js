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
    [RoutePrefix("api/MyUnreconciliationManagement")]
    [Authorize]
    public class MyUnreconciliationManagementController : ApiController
    {
        DaMyUnreconciliationManagement objDaMyUnreconciliationManagement = new DaMyUnreconciliationManagement();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();     

        [ActionName("UnreconPendingExport")]
        [HttpPost]
        public HttpResponseMessage UnreconPendingExport(MyUnreconciliation_list values)
        {
        
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaUnreconPendingExport(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyunreConciliationSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyunreConciliationSummary()
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetMyunreConciliationSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyunreConciliationSummaryCount")]
        [HttpGet]
        public HttpResponseMessage GetMyunreConciliationSummaryCount()
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetMyunreConciliationSummaryCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyunreConciliationSummarySearch")]
        [HttpPost]
        public HttpResponseMessage GetMyunreConciliationSummarySearch(MdlMyUnreconciliationManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetMyunreConciliationSummarySearch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBank")]
        [HttpGet]
        public HttpResponseMessage GetBank()
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            objDaMyUnreconciliationManagement.DaGetBank(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Bank Name List

        [ActionName("BankNameList")]
        [HttpGet]
        public HttpResponseMessage BankNameList(string bank_gid)
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            objDaMyUnreconciliationManagement.DaBankNameList(values, bank_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnreconClosedExport")]
        [HttpPost]
        public HttpResponseMessage UnreconClosedExport(MyUnreconciliationClose_list values)
        {
           
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaUnreconClosedExport(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyunreConciliationClosedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyunreConciliationClosedSummary()
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetMyunreConciliationClosedSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyunreConciliationClosedSummarySearch")]
        [HttpPost]
        public HttpResponseMessage GetMyunreConciliationClosedSummarySearch(MdlMyUnreconciliationManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetMyunreConciliationClosedSummarySearch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyUnreconReportView")]
        [HttpGet]
        public HttpResponseMessage GetMyUnreconReportView(string banktransc_gid)
        {
            transaction_list values = new transaction_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetMyUnreconReportView(banktransc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTransferredHistoryView")]
        [HttpGet]
        public HttpResponseMessage GetTransferredHistory(string banktransc_gid)
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetTransferredHistoryView(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssignedHistoryView")]
        [HttpGet]
        public HttpResponseMessage GetAssignedHistory(string banktransc_gid)
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetAssignedHistoryView(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AllocatedPendingExport")]
        [HttpGet]
        public HttpResponseMessage AllocatedPendingExport()
        {
            unreconallocatependingrpt_list values = new unreconallocatependingrpt_list();           
            objDaMyUnreconciliationManagement.DaExportAllocatedPendingReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocatedPendingReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedPendingReportSummary()
        {
            MdlMyUnreconciliationManagement values = new MdlMyUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyUnreconciliationManagement.DaGetAllocatedPendingReportSummary(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
   
}