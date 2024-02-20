using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// (It's used for ApplicationReport in Samfin)ApplicationReport Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstApplicationReport")]
    [Authorize]
    public class MstApplicationReportController : ApiController
    {

        DaMstApplicationReport objDaMstApplicationReport = new DaMstApplicationReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        
        //Application Export
        [ActionName("ExportMstAppReport")]
        [HttpGet]
        public HttpResponseMessage GetMstApplicationReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstApplicationReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Application Visit Export
        [ActionName("ExportMstAppVisitReport")]
        [HttpGet]
        public HttpResponseMessage GetMstApplicationVisitReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstApplicationVisitReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }
        //PSLCSA Management Completed Export Excel
        [ActionName("ExportMstPSLCSAManagement")]
        [HttpGet]
        public HttpResponseMessage GetMstPSLCSAManagement()
        {
            MstApplicationReport objMstPSLCSAManagement = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstPSLCSAManagement(objMstPSLCSAManagement);
            return Request.CreateResponse(HttpStatusCode.OK, objMstPSLCSAManagement);
        }

        //PSLCSA Management Pending Export Excel
        [ActionName("ExportMstPSLCSAManagementPending")]
        [HttpGet]
        public HttpResponseMessage GetMstPSLCSAManagementPending()
        {
            MstApplicationReport objMstPSLCSAManagementPending = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstPSLCSAManagementPending(objMstPSLCSAManagementPending);
            return Request.CreateResponse(HttpStatusCode.OK, objMstPSLCSAManagementPending);
        }

        //Count
        [ActionName("ApplicationCounts")]
        [HttpGet]
        public HttpResponseMessage GetApplicationCounts()
        {
            ApplicationListCount values = new ApplicationListCount();
            objDaMstApplicationReport.DaGetApplicationCounts(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        //Application Summary
        [ActionName("MstAppSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstAppSummary()
        {
            MstApplicationReport objMstAppSummary = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstAppSummary(objMstAppSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstAppSummary);
        }

        //CC Report Summary
        [ActionName("MstCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetMstCCSummary()
        {
            MstApplicationReport objMstCCSummary = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstCCSummary(objMstCCSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCCSummary);
        }

        //CC Export
        [ActionName("ExportMstCCReport")]
        [HttpGet]
        public HttpResponseMessage GetMstCCReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstCCReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Sanction MIS Summary
        [ActionName("GetSanctionMISSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISSummary()
        {
            SanctionMISSummary values = new SanctionMISSummary();
            objDaMstApplicationReport.DaGetSanctionMISSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Sanction MIS Summary View
        [ActionName("CADSanctionDtls")]
        [HttpGet]
        public HttpResponseMessage CADSanctionDtls(string application2sanction_gid)
        {
            reportcadsanctiondetails values = new reportcadsanctiondetails();
            objDaMstApplicationReport.DaCADSanctionDtls(application2sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDetails(string application2sanction_gid)
        {
            reportmdltemplate values = new reportmdltemplate();
            objDaMstApplicationReport.DaGetTemplateDetails(values, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionLetterSummary")]
        [HttpGet]
        public HttpResponseMessage CADSanctionLetterSummary(string application2sanction_gid)
        {
            mdlreportsanction objmdlreportsanction = new mdlreportsanction();
            objDaMstApplicationReport.DaCADSanctionLetterSummary(application2sanction_gid, objmdlreportsanction);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlreportsanction);
        }

        [ActionName("Getesdocument")]
        [HttpGet]
        public HttpResponseMessage Getesdocument(string application2sanction_gid)

        {
            ReportUploadCADDocumentname objdocument = new ReportUploadCADDocumentname();
            objDaMstApplicationReport.DaGetesdocument(objdocument, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("GetMaildocument")]
        [HttpGet]
        public HttpResponseMessage GetMaildocument(string application2sanction_gid)

        {
            ReportUploadCADDocumentname objdocument = new ReportUploadCADDocumentname();
            objDaMstApplicationReport.DaGetMaildocument(objdocument, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("ExportSanctionMISReport")]
        [HttpGet]
        public HttpResponseMessage ExportSanctionMISReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportSanctionMISReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Buyer Report Export Excel
        [ActionName("ExportBuyerReport")]
        [HttpGet]
        public HttpResponseMessage ExportBuyerReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportBuyerReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Buyer Report Summary Export Excel
        [ActionName("GetBuyerReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetBuyerReportSummary()
        {
            MstApplicationReport objMstAppSummary = new MstApplicationReport();
            objDaMstApplicationReport.DaGetBuyerReportSummary(objMstAppSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objMstAppSummary);

        }

        //Sanction MIS Summary - Maker
        [ActionName("GetSanctionMISMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISMakerSummary()
        {
            SanctionMISSummary values = new SanctionMISSummary();
            objDaMstApplicationReport.DaGetSanctionMISMakerSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExportSanctionMISMakerReport")]
        [HttpGet]
        public HttpResponseMessage ExportSanctionMISMakerReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportSanctionMISMakerReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Sanction MIS Summary - Checker
        [ActionName("GetSanctionMISCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISCheckerSummary()
        {
            SanctionMISSummary values = new SanctionMISSummary();
            objDaMstApplicationReport.DaGetSanctionMISCheckerSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExportSanctionMISCheckerReport")]
        [HttpGet]
        public HttpResponseMessage ExportSanctionMISCheckerReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportSanctionMISCheckerReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Sanction MIS Summary - Approver
        [ActionName("GetSanctionMISApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISApproverSummary()
        {
            SanctionMISSummary values = new SanctionMISSummary();
            objDaMstApplicationReport.DaGetSanctionMISApproverSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExportSanctionMISApproverReport")]
        [HttpGet]
        public HttpResponseMessage ExportSanctionMISApproverReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportSanctionMISApproverReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Sanction MIS Summary - Approved
        [ActionName("GetSanctionMISApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISApprovedSummary()
        {
            SanctionMISSummary values = new SanctionMISSummary();
            objDaMstApplicationReport.DaGetSanctionMISApprovedSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExportSanctionMISApprovedReport")]
        [HttpGet]
        public HttpResponseMessage ExportSanctionMISApprovedReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportSanctionMISApprovedReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GethypothecationSummary")]
        [HttpGet]
        public HttpResponseMessage GethypothecationSummary()
        {
            SanctionMISSummary values = new SanctionMISSummary();
            objDaMstApplicationReport.DaGethypothecationSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExporthypothecationSummaryReport")]
        [HttpGet]
        public HttpResponseMessage ExporthypothecationSummaryReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExporthypothecationSummaryReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("ExportDocCheckMakerPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckMakerPendingReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportDocCheckMakerPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("ExportDocCheckCheckerPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckCheckerPendingReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportDocCheckCheckerPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("ExportDocCheckApprovalPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckApprovalPendingReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportDocCheckApprovalPendingReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("GetCADDocChecklistReportCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstApplicationReport.DaGetCADDocChecklistReportCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADDocChecklistReportApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstApplicationReport.DaGetCADDocChecklistReportApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstApplicationReport.DaGetCADDocChecklistReportSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocChecklistPendingCount")]
        [HttpGet]
        public HttpResponseMessage GetDocChecklistPendingCount()
        {
            DocumentCount values = new DocumentCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstApplicationReport.DaGetDocChecklistPendingCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Application TAT Export
        [ActionName("ExportMstTatAppReport")]
        [HttpGet]
        public HttpResponseMessage ExportMstTatAppReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportMstTatAppReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Program Master Export Excel
        [ActionName("ExportExcelAddprogram")]
        [HttpGet]
        public HttpResponseMessage ExportExcelAddprogram()
        {
            ExportExcelAddprogram objMstApplicationReport = new ExportExcelAddprogram();
            objDaMstApplicationReport.DaExportExcelAddprogram(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //CAD Accepted Customers Export Excel
        [ActionName("ExportexcelCADAcceptedCus")]
        [HttpGet]
        public HttpResponseMessage ExportexcelCADAcceptedCus()
        {
            ExportExcelAddprogram objMstApplicationReport = new ExportExcelAddprogram();
            objDaMstApplicationReport.DaExportexcelCADAcceptedCus(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Product Export Excel
        [ActionName("ExportExcelAddProduct")]
        [HttpGet]
        public HttpResponseMessage GetMstProductReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaGetMstProductReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //TAT CAD Accepted Customers Export Excel
        [ActionName("TATExportexcelCADAcceptedCus")]
        [HttpGet]
        public HttpResponseMessage TATExportexcelCADAcceptedCus()
        {
            ExportExcelAddprogram objMstApplicationReport = new ExportExcelAddprogram();
            objDaMstApplicationReport.DaTATExportexcelCADAcceptedCus(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }
        //document checklist credit approval summary
        [ActionName("GetCADDocChecklistReportApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistReportApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstApplicationReport.DaGetCADDocChecklistReportApprovalCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetCADDocChecklistReportApprovalSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetCADDocChecklistReportApprovalSummary()
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlMstCAD values = new MdlMstCAD();
        //    objDaMstApplicationReport.DaGetCADDocChecklistReportApprovalSummary(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("ExportDocCheckApprovalCompletedReport")]
        [HttpGet]
        public HttpResponseMessage ExportDocCheckApprovalCompletedReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportDocCheckApprovalCompletedReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }


        //Export Excel 


        [ActionName("LoanODDispersement")]
        [HttpGet]
        public HttpResponseMessage LoanODDispersement(string rmdisbursementrequest_gid)
        {
            ExportExcelAddprogram objMstApplicationReport = new ExportExcelAddprogram();
            objDaMstApplicationReport.DaLoanODDispersement(objMstApplicationReport, rmdisbursementrequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("DispersementNEFT")]
        [HttpGet]
        public HttpResponseMessage DispersementNEFT(string rmdisbursementrequest_gid)
        {
            ExportExcelAddprogram objMstApplicationReport = new ExportExcelAddprogram();
            objDaMstApplicationReport.DaDispersementNEFT(objMstApplicationReport, rmdisbursementrequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        [ActionName("ODAccountopen")]
        [HttpGet]
        public HttpResponseMessage ODAccountopen(string application_gid)
        {
            ExportExcelAddprogram objMstApplicationReport = new ExportExcelAddprogram();
            objDaMstApplicationReport.DaODAccountopen(objMstApplicationReport, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Company Document

        [ActionName("ExportMstCompanyReport")]
        [HttpGet]
        public HttpResponseMessage GetExportMstCompanyReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaGetExportMstCompanyReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Group Document

        [ActionName("ExportMstGroupReport")]
        [HttpGet]
        public HttpResponseMessage GetExportMstGroupReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportMstGroupReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Individual Document

        [ActionName("ExportMstIndividualReport")]
        [HttpGet]
        public HttpResponseMessage GetExportMstIndividualReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportMstIndividualReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }

        //Encore Document

        [ActionName("ExportMstEncoreReport")]
        [HttpGet]
        public HttpResponseMessage GetExportMstEncoreReport()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstApplicationReport.DaExportMstEncoreReport(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }



        [ActionName("GetConsolidatedSanctionReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedSanctionReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstApplicationReport.DaGetConsolidatedSanctionReport( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConsolidatedLSAReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedLSAReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstApplicationReport.DaGetConsolidatedLSAReport( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConsolidatedDocumentChecklistReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedDocumentChecklistReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstApplicationReport.DaGetConsolidatedDocumentChecklistReport( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConsolidatedSoftcopyVettingReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedSoftcopyVettingReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstApplicationReport.DaGetConsolidatedSoftcopyVettingReport( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetConsolidatedOriginalCopyVettingReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedOriginalCopyVettingReport()
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstApplicationReport.DaGetConsolidatedOriginalCopyVettingReport( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADConsolidatedReportCount")]
        [HttpGet]
        public HttpResponseMessage CADConsolidatedReportCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            CadConsolidatedRportCount values = new CadConsolidatedRportCount();
            objDaMstApplicationReport.DaCADConsolidatedReportCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}