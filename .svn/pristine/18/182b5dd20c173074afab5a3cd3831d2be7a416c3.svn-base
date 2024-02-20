using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This controllers provide access for various Defferal & Convenat Document Reports
    /// </summary>
    /// <remarks>Written by Venkatesh, Premchander.K </remarks>


    [RoutePrefix("api/AgrPmgReport")]
    [Authorize]

    public class AgrPmgReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrPmgReport objDaAgrPmgReport = new DaAgrPmgReport();

        [ActionName("GetScannedDocMakerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocMakerPendingSummary()
        {
            rptscannedmakerapplicationlist values = new rptscannedmakerapplicationlist(); 
            objDaAgrPmgReport.DaGetScannedDocMakerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDocCheckerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocCheckerPendingSummary()
        {
            rptscannedmakerapplicationlist values = new rptscannedmakerapplicationlist(); 
            objDaAgrPmgReport.DaGetScannedDocCheckerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDocApproverPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocApproverPendingSummary()
        {
            rptscannedmakerapplicationlist values = new rptscannedmakerapplicationlist(); 
            objDaAgrPmgReport.DaGetScannedDocApproverPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocMakerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocMakerPendingSummary()
        {
            rptphyiscalmakerapplicationlist values = new rptphyiscalmakerapplicationlist(); 
            objDaAgrPmgReport.DaGetPhysicalDocMakerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetPhysicalDocCheckerPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocCheckerPendingSummary()
        {
            rptphyiscalmakerapplicationlist values = new rptphyiscalmakerapplicationlist(); 
            objDaAgrPmgReport.DaGetPhysicalDocCheckerPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocApproverPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocApproverPendingSummary()
        {
            rptphyiscalmakerapplicationlist values = new rptphyiscalmakerapplicationlist(); 
            objDaAgrPmgReport.DaGetPhysicalDocApproverPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppPhysicalDocCount")]
        [HttpGet]
        public HttpResponseMessage GetAppPhysicalDocCount()
        {
            CadScannedCount values = new CadScannedCount(); 
            objDaAgrPmgReport.DaGetAppPhysicalDocCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppScannedDocCount")]
        [HttpGet]
        public HttpResponseMessage GetAppScannedDocCount()
        {
            CadScannedCount values = new CadScannedCount(); 
            objDaAgrPmgReport.DaGetAppScannedDocCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedDefPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetScannedDefPendingReport(string lsstatus)
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaAgrPmgReport.DaGetScannedDefPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetPhysicalDefPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDefPendingReport(string lsstatus)
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaAgrPmgReport.DaGetPhysicalDefPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetScannedCovPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetScannedCovPendingReport(string lsstatus)
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaAgrPmgReport.DaGetScannedCovPendingReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }



        [ActionName("GetPhysicalCovPendingReport")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalCovPendingReport(string lsstatus)
        {
            Mdlscannedapplicationexport objMstApplicationReport = new Mdlscannedapplicationexport();
            objDaAgrPmgReport.DaGetPhysicalCovPendingReport(lsstatus,objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetSuprDocChecklistReport")]
        [HttpGet]
        public HttpResponseMessage GetSuprDocChecklistReport(string lsstatus)
        {
            MdlAgrSuprDocChecklistReport objMstApplicationReport = new MdlAgrSuprDocChecklistReport();
            objDaAgrPmgReport.DaGetSuprDocChecklistReport(lsstatus, objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetSuprDocChecklistReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetSuprDocChecklistReportSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrSuprDocChecklistReport values = new MdlAgrSuprDocChecklistReport();
            objDaAgrPmgReport.DaGetSuprDocChecklistReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetSuprDocChecklistReportCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSuprDocChecklistReportCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrSuprDocChecklistReport values = new MdlAgrSuprDocChecklistReport();
            objDaAgrPmgReport.DaGetSuprDocChecklistReportCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprDocChecklistReportApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetSuprDocChecklistReportApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrSuprDocChecklistReport values = new MdlAgrSuprDocChecklistReport();
            objDaAgrPmgReport.DaGetSuprDocChecklistReportApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprDocChecklistPendingCount")]
        [HttpGet]
        public HttpResponseMessage GetSuprDocChecklistPendingCount()
        {
            SuprDocumentCount values = new SuprDocumentCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrPmgReport.DaGetSuprDocChecklistPendingCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}
