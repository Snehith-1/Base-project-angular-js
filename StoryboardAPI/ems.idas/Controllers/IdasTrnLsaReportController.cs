using ems.idas.DataAccess;
using ems.idas.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ems.idas.Controllers
{
    [RoutePrefix("api/idasTrnLsaReport")]
    [Authorize]
    public class IdasTrnLsaReportController : ApiController
    {

        DaIdasTrnLsaReport objDaIdasTrnLsaReport = new DaIdasTrnLsaReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetidasLsaSummary")]
        [HttpGet]
        public HttpResponseMessage GetidasLsaSummary()
        {
            idasTrnLsaReportSummary values = new idasTrnLsaReportSummary();
            objDaIdasTrnLsaReport.DaGetidasLsaSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomer2sanction")]
        [HttpGet]
        public HttpResponseMessage Getcustomer2sanction(string customer_gid)
        {
            idasTrnLsaReportSummary values = new idasTrnLsaReportSummary();
            objDaIdasTrnLsaReport.DaGetcustomer2sanction(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lsafilter")]
        [HttpPost]
        public HttpResponseMessage lsafilter(idasTrnLsaReportSummary objidasTrnLsaReportSummary)
        {
            objDaIdasTrnLsaReport.Dalsafilter(objidasTrnLsaReportSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objidasTrnLsaReportSummary);
        }

        //LSA List Export
        [ActionName("IdasExportExcel")]
        [HttpPost]
        public HttpResponseMessage IdasExportExcel(idasLsaReportSummary objidasLsaReportSummary)
        {
            objDaIdasTrnLsaReport.DaIdasExportExcel(objidasLsaReportSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objidasLsaReportSummary);
        }

        // Colletral Summary Report
        [ActionName("GetColletarlSummary")]
        [HttpGet]
        public HttpResponseMessage GetColletarlSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlsecurity values = new Mdlsecurity();
            objDaIdasTrnLsaReport.DaGetColletarlSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Colletral Report Excel
        [ActionName("ColletralReportExcel")]
        [HttpGet]
        public HttpResponseMessage ColletralReportExcel()
        {
            Mdlsecurity values = new Mdlsecurity();
            objDaIdasTrnLsaReport.DaColletralReportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}