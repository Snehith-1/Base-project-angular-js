using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.idas.DataAccess;
using ems.idas.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/CourierReport")]
    [Authorize]
    public class CourierReportController : ApiController
    {
        // GET: idasTrnCourierReport
        DaCourierReport objDaCourierReport = new DaCourierReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("CourierReportSummary")]
        [HttpGet]
        public HttpResponseMessage CourierReportSummary()
        {
            CourierReportSummary objCourierReport = new CourierReportSummary();
            objDaCourierReport.DaCourierReportSummary(objCourierReport);
            return Request.CreateResponse(HttpStatusCode.OK, objCourierReport);
        }

        [ActionName("ReportSearch")]
        [HttpGet]
        public HttpResponseMessage ReportSearch(string courier_type, string customer_name)
        {
            CourierReportSummary objCourierReport = new CourierReportSummary();
            objDaCourierReport.DaReportSearch(courier_type, customer_name, objCourierReport);
            return Request.CreateResponse(HttpStatusCode.OK, objCourierReport);
        }

        [ActionName("ExportReport")]
        [HttpGet]
        public HttpResponseMessage ExportReport(string courier_type, string customer_name)
        {
            CourierReportSummary objCourierReport = new CourierReportSummary();
            objDaCourierReport.DaExportReport(courier_type, customer_name, objCourierReport);
            return Request.CreateResponse(HttpStatusCode.OK, objCourierReport);
        }
       
    }
}