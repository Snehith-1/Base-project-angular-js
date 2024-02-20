using ems.osd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.osd.Models;
using System.Net.Http;
using System.Net;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdTrnBankAlertReport")]
    [AllowAnonymous]
    public class OsdTrnBankAlertReportController : ApiController
    {
        DaOsdTrnBankAlertReport objDaOsdTrnBankAlertReport = new DaOsdTrnBankAlertReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Report Summary
        [ActionName("BankReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetBankReportSummary()
        {
            OsdTrnBankAlertReport objBankReportSummary = new OsdTrnBankAlertReport();
            objDaOsdTrnBankAlertReport.DaGetBankReportSummary(objBankReportSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objBankReportSummary);
        }

        //Bank Alert Export
        [ActionName("ExportBankReport")]
        [HttpGet]
        public HttpResponseMessage GetExportBankReport()
        {
            OsdTrnBankAlertReport objExportBankReport = new OsdTrnBankAlertReport();
            objDaOsdTrnBankAlertReport.DaGetExportBankReport(objExportBankReport);
            return Request.CreateResponse(HttpStatusCode.OK, objExportBankReport);
        }
    }
}