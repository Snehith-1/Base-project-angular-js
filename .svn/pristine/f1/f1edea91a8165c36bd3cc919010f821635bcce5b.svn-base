using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to Export Credit Report.
    /// </summary>
    /// <remarks>Written by Abilash.A, Premchander.K </remarks>

    [RoutePrefix("api/AgrMstCreditAllocationReport")]
    [Authorize]

    public class AgrMstCreditAllocationReportController : ApiController
    {
        DaAgrMstCreditAllocationReport objDaAgrMstCreditAllocationReport = new DaAgrMstCreditAllocationReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("MstCreditSummary")]
        [HttpGet]
        public HttpResponseMessage MstCreditSummary()
        {
            MdlAgrMstCreditAllocationReport objMstCreditAllocationReport = new MdlAgrMstCreditAllocationReport();
            objDaAgrMstCreditAllocationReport.DaMstCreditSummary(objMstCreditAllocationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCreditAllocationReport);
        }

        [ActionName("ExportMstCreditReport")]
        [HttpGet]
        public HttpResponseMessage ExportMstCreditReport()
        {
            MdlAgrMstCreditAllocationReport objMstCreditAllocationReport = new MdlAgrMstCreditAllocationReport();
            objDaAgrMstCreditAllocationReport.DaExportMstCreditReport(objMstCreditAllocationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCreditAllocationReport);
        }
        [ActionName("MstSupplierCreditSummary")]
        [HttpGet]
        public HttpResponseMessage MstSupplierCreditSummary()
        {
            MdlAgrMstCreditAllocationReport objMstCreditAllocationReport = new MdlAgrMstCreditAllocationReport();
            objDaAgrMstCreditAllocationReport.DaMstSupplierCreditSummary(objMstCreditAllocationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCreditAllocationReport);
        }

        [ActionName("ExportMstSupplierCreditReport")]
        [HttpGet]
        public HttpResponseMessage ExportMstSupplierCreditReport()
        {
            MdlAgrMstCreditAllocationReport objMstCreditAllocationReport = new MdlAgrMstCreditAllocationReport();
            objDaAgrMstCreditAllocationReport.DaExportMstSupplierCreditReport(objMstCreditAllocationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCreditAllocationReport);
        }
    }
}