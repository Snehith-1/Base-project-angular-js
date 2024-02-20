using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Http;

/// <summary>
/// (It's used for Softcopy and Original copy report page in Samfin)CadCovenentReport Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{

    [RoutePrefix("api/MstCadCovenentReport")]
    [Authorize]

    public class MstCadCovenentReportController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCadCovenentReport objDaMstCadCovenentReport = new DaMstCadCovenentReport();

        [ActionName("GetScannedDocMakerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocMakerPendingSummary()
        {
            reportscannedmakerapplicationlist values = new reportscannedmakerapplicationlist();
            objDaMstCadCovenentReport.DaGetScannedDocMakerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDocCheckerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocCheckerPendingSummary()
        {
            reportscannedmakerapplicationlist values = new reportscannedmakerapplicationlist();
            objDaMstCadCovenentReport.DaGetScannedDocCheckerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDocApproverPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocApproverPendingSummary()
        {
            reportscannedmakerapplicationlist values = new reportscannedmakerapplicationlist();
            objDaMstCadCovenentReport.DaGetScannedDocApproverPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocMakerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocMakerPendingSummary()
        {
            reportphyiscalmakerapplicationlist values = new reportphyiscalmakerapplicationlist();
            objDaMstCadCovenentReport.DaGetPhysicalDocMakerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetPhysicalDocCheckerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocCheckerPendingSummary()
        {
            reportphyiscalmakerapplicationlist values = new reportphyiscalmakerapplicationlist();
            objDaMstCadCovenentReport.DaGetPhysicalDocCheckerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocApproverPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocApproverPendingSummary()
        {
            reportphyiscalmakerapplicationlist values = new reportphyiscalmakerapplicationlist();
            objDaMstCadCovenentReport.DaGetPhysicalDocApproverPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppPhysicalDocCount")]
        [HttpGet]
        public HttpResponseMessage GetAppPhysicalDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            objDaMstCadCovenentReport.DaGetAppPhysicalDocCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppScannedDocCount")]
        [HttpGet]
        public HttpResponseMessage GetAppScannedDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            objDaMstCadCovenentReport.DaGetAppScannedDocCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDefPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetScannedDefPendingReport(string lsstatus)
        {
            Mdlscannedapplexport objMstApplicationReport = new Mdlscannedapplexport();
            objDaMstCadCovenentReport.DaGetScannedDefPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetPhysicalDefPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDefPendingReport(string lsstatus)
        {
            Mdlscannedapplexport objMstApplicationReport = new Mdlscannedapplexport();
            objDaMstCadCovenentReport.DaGetPhysicalDefPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetScannedCovPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetScannedCovPendingReport(string lsstatus)
        {
            Mdlscannedapplexport objMstApplicationReport = new Mdlscannedapplexport();
            objDaMstCadCovenentReport.DaGetScannedCovPendingReport(lsstatus,objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetPhysicalCovPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalCovPendingReport(string lsstatus)
        {
            Mdlscannedapplexport objMstApplicationReport = new Mdlscannedapplexport();
            objDaMstCadCovenentReport.DaGetPhysicalCovPendingReport(lsstatus,objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }
    }
}