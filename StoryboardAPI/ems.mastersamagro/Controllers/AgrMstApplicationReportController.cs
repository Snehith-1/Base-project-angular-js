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
    /// This Controllers will provide access to various excel reports in Samagro
    /// </summary>
    /// <remarks>Written by Abilash.A, Kalaiarasan Premchandar.K </remarks>

    [RoutePrefix("api/AgrMstApplicationReport")]
    [Authorize]
    public class AgrMstApplicationReportController : ApiController
    {

        DaAgrMstApplicationReport objDaAgrMstApplicationReport = new DaAgrMstApplicationReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //Application Export
        [ActionName("ExportMstAppReport")]
        [HttpGet]
        public HttpResponseMessage GetMstApplicationReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstApplicationReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Count
        [ActionName("ApplicationCounts")]
        [HttpGet]
        public HttpResponseMessage GetApplicationCounts()
        {
            ApplicationListCount values = new ApplicationListCount();
            objDaAgrMstApplicationReport.DaGetApplicationCounts(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Application Summary
        [ActionName("MstAppSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstAppSummary()
        {
            MdlAgrMstApplicationReport objMstAppSummary = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstAppSummary(objMstAppSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstAppSummary);
        }

        //CC Report Summary
        [ActionName("MstCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstCCSummary()
        {
            MdlAgrMstApplicationReport objMstCCSummary = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstCCSummary(objMstCCSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCCSummary);
        }

        //CC Export
        [ActionName("ExportMstCCReport")]
        [HttpGet]
        public HttpResponseMessage GetMstCCReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstCCReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Supplier Application Export
        [ActionName("ExportMstSupplierAppReport")]
        [HttpGet]
        public HttpResponseMessage GetExportMstSupplierAppReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetExportMstSupplierAppReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Supplier Count
        [ActionName("SupplierApplicationCounts")]
        [HttpGet]
        public HttpResponseMessage GetSupplierApplicationCounts()
        {
            ApplicationListCount values = new ApplicationListCount();
            objDaAgrMstApplicationReport.DaGetSupplierApplicationCounts(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Supplier Application Summary
        [ActionName("MstSupplierAppSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstSupplierAppSummary()
        {
            MdlAgrMstApplicationReport objMstAppSummary = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstSupplierAppSummary(objMstAppSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstAppSummary);
        }

        //Supplier CC Report Summary
        [ActionName("MstSupplierCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstSupplierCCSummary()
        {
            MdlAgrMstApplicationReport objMstCCSummary = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstSupplierCCSummary(objMstCCSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCCSummary);
        }

        //Supplier CC Export
        [ActionName("ExportMstSupplierCCReport")]
        [HttpGet]
        public HttpResponseMessage GetMstSupplierCCReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstSupplierCCReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //CAD Accepted export excel
        [ActionName("GetExportCADAcceptedAppReport")]
        [HttpGet]
        public HttpResponseMessage GetExportCADAcceptedAppReport()
        {
            ExportExcelReturnData objMstApplicationReport = new ExportExcelReturnData();
            objDaAgrMstApplicationReport.DaGetExportCADAcceptedAppReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }


        [ActionName("ExportDocCheckMakerPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckMakerPendingReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaExportDocCheckMakerPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("ExportDocCheckCheckerPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckCheckerPendingReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaExportDocCheckCheckerPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("ExportDocCheckApprovalPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckApprovalPendingReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaExportDocCheckApprovalPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetCADDocChecklistReportCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstApplicationReport values = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetCADDocChecklistReportCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADDocChecklistReportApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstApplicationReport values = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetCADDocChecklistReportApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstApplicationReport values = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetCADDocChecklistReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocChecklistPendingCount")]
        [HttpGet]
        public HttpResponseMessage GetDocChecklistPendingCount()
        {
            DocumentCount values = new DocumentCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationReport.DaGetDocChecklistPendingCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Warehouse Report Summary
        [ActionName("GetWarehouseSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstWarehouseSummary()
        {
            MdlAgrMstApplicationReport objMstWarehouseSummary = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstWarehouseSummary(objMstWarehouseSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstWarehouseSummary);
        }
        //Warehouse Report
        [ActionName("ExportMstWarehouseReport")]
        [HttpGet]
        public HttpResponseMessage ExportMstWarehouseReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstWarehouseReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Other Creditor Report Summary
        [ActionName("GetOtherCreditorSummary")]
        [HttpGet]
        public HttpResponseMessage GetOtherCreditorSummary()
        {
            MdlAgrMstApplicationReport objMstOtherCreditorSummary = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstOtherCreditorSummary(objMstOtherCreditorSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstOtherCreditorSummary);
        }
        //Other Creditor Report
        [ActionName("ExportMstOtherCreditorReport")]
        [HttpGet]
        public HttpResponseMessage ExportMstOtherCreditorReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetMstOtherCreditorReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetPMGDocChecklistApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetPMGDocChecklistApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstApplicationReport values = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaGetPMGDocChecklistApprovalCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExportDocCheckApprovalCompletedReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckApprovalCompletedReport()
        {
            MdlAgrMstApplicationReport objMstApplicationReport = new MdlAgrMstApplicationReport();
            objDaAgrMstApplicationReport.DaExportDocCheckApprovalCompletedReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }


        [ActionName("GetConsolidatedSanctionReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedSanctionReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrMstApplicationReport.DaGetConsolidatedSanctionReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConsolidatedLSAReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedLSAReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrMstApplicationReport.DaGetConsolidatedLSAReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConsolidatedDocumentChecklistReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedDocumentChecklistReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrMstApplicationReport.DaGetConsolidatedDocumentChecklistReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConsolidatedSoftcopyVettingReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedSoftcopyVettingReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrMstApplicationReport.DaGetConsolidatedSoftcopyVettingReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetConsolidatedOriginalCopyVettingReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedOriginalCopyVettingReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrMstApplicationReport.DaGetConsolidatedOriginalCopyVettingReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADConsolidatedReportCount")]
        [HttpGet]
        public HttpResponseMessage CADConsolidatedReportCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            CadConsolidatedRportCount values = new CadConsolidatedRportCount();
            objDaAgrMstApplicationReport.DaCADConsolidatedReportCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}