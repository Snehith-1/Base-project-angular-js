using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

/// <summary>
/// (It's used for Softcopy & Original copy document report pages in Samfin)CadAppDocReport Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>
namespace ems.master.Controllers
{

    [RoutePrefix("api/MstCadAppDocReport")]
    [Authorize]

    public class MstCadAppDocReportController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCadAppDocReport objDaMstCadAppDocReport = new DaMstCadAppDocReport();

        [ActionName("GetScannedDocMakerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocMakerPendingSummary()
        {
            rptscannedmakerapplicationlist values = new rptscannedmakerapplicationlist();
            objDaMstCadAppDocReport.DaGetScannedDocMakerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDocCheckerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocCheckerPendingSummary()
        {
            rptscannedmakerapplicationlist values = new rptscannedmakerapplicationlist();
            objDaMstCadAppDocReport.DaGetScannedDocCheckerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDocApproverPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocApproverPendingSummary()
        {
            rptscannedmakerapplicationlist values = new rptscannedmakerapplicationlist();
            objDaMstCadAppDocReport.DaGetScannedDocApproverPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocMakerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocMakerPendingSummary()
        {
            rptphyiscalmakerapplicationlist values = new rptphyiscalmakerapplicationlist();
            objDaMstCadAppDocReport.DaGetPhysicalDocMakerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetPhysicalDocCheckerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocCheckerPendingSummary()
        {
            rptphyiscalmakerapplicationlist values = new rptphyiscalmakerapplicationlist();
            objDaMstCadAppDocReport.DaGetPhysicalDocCheckerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocApproverPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocApproverPendingSummary()
        {
            rptphyiscalmakerapplicationlist values = new rptphyiscalmakerapplicationlist();
            objDaMstCadAppDocReport.DaGetPhysicalDocApproverPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppPhysicalDocCount")]
        [HttpGet]
        public HttpResponseMessage GetAppPhysicalDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            objDaMstCadAppDocReport.DaGetAppPhysicalDocCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppScannedDocCount")]
        [HttpGet]
        public HttpResponseMessage GetAppScannedDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            objDaMstCadAppDocReport.DaGetAppScannedDocCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDefPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetScannedDefPendingReport(string lsstatus)
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaMstCadAppDocReport.DaGetScannedDefPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetPhysicalDefPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDefPendingReport(string lsstatus)
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaMstCadAppDocReport.DaGetPhysicalDefPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetScannedCovPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetScannedCovPendingReport()
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaMstCadAppDocReport.DaGetScannedCovPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetPhysicalCovPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalCovPendingReport()
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaMstCadAppDocReport.DaGetPhysicalCovPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }
    }
}