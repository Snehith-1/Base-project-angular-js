using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCreditAllocationReport")]
    [Authorize]
    public class MstCreditAllocationReportController : ApiController
    {
        DaMstCreditAllocationReport objDaMstCreditAllocationReport = new DaMstCreditAllocationReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
       

        [ActionName("MstCreditSummary")]
        [HttpGet]
        public HttpResponseMessage MstCreditSummary()
        {
            MstCreditAllocationReport objMstCreditAllocationReport = new MstCreditAllocationReport();
            objDaMstCreditAllocationReport.DaMstCreditSummary(objMstCreditAllocationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCreditAllocationReport);
        }

        [ActionName("ExportMstCreditReport")]
        [HttpGet]
        public HttpResponseMessage ExportMstCreditReport()
        {
            MstCreditAllocationReport objMstCreditAllocationReport = new MstCreditAllocationReport();
            objDaMstCreditAllocationReport.DaExportMstCreditReport(objMstCreditAllocationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCreditAllocationReport);
        }
    }
}