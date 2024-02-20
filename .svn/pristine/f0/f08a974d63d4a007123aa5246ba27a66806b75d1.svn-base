using ems.hrloan.DataAccess;
using ems.hrloan.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ems.hrloan.Controllers
{
    [RoutePrefix("api/RptEmployeeLoanReport")]
    [Authorize]
    public class RptEmployeeLoanReportController : ApiController
    {

        DaRptEmployeeLoanReport objDaEmployeeLoanReport = new DaRptEmployeeLoanReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();       
        

        //Count
        [ActionName("GetReportCounts")]
        [HttpGet]
        public HttpResponseMessage GetReportCounts()
        {
            EmployeeLoanReportcount values = new EmployeeLoanReportcount();
            objDaEmployeeLoanReport.DaGetReportCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        //Loan Summary
        [ActionName("GetEmployeeLoanReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeLoanReportSummary()
        {
            MdlRptEmployeeLoanReport objRptSummary = new MdlRptEmployeeLoanReport();
            objDaEmployeeLoanReport.DaGetEmployeeLoanReportSummary(objRptSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objRptSummary);
        }
        //Loan  Export
        [ActionName("ExportLoanReport")]
        [HttpGet]
        public HttpResponseMessage ExportLoanReport()
        {
            MdlRptEmployeeLoanReport values = new MdlRptEmployeeLoanReport();
            objDaEmployeeLoanReport.DaGetExportLoanReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}