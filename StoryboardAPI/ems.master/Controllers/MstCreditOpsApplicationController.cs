using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCreditOpsApplication")]
    [Authorize]

    public class MstCreditOpsApplicationController : ApiController
    {
        DaMstCreditOpsApplication objMstCreditOpsApplication = new DaMstCreditOpsApplication();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetSanctionRefnoDropDown")]
        [HttpGet]
        public HttpResponseMessage GetSanctionRefnoDropDown(string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSanctionDropDown values = new MdlSanctionDropDown();
            objMstCreditOpsApplication.DaGetSanctionRefnoDropDown(getsessionvalues.employee_gid, customer_urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSanctionDtls")]
        [HttpGet]
        public HttpResponseMessage GetSanctionDtls(string application2sanction_gid, string application_gid)
        {
            RMsanctiondetails values = new RMsanctiondetails();
            objMstCreditOpsApplication.DaGetSanctionDtls(application2sanction_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RMDisbursementDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage RMDisbursementDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            disbursementuploaddocument documentname = new disbursementuploaddocument();
            objMstCreditOpsApplication.DaRMDisbursementDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteRMDisbursementDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditcheque(string rmdisbursementdocument_gid, string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            disbursementuploaddocument values = new disbursementuploaddocument();
            objMstCreditOpsApplication.DaDeleteRMDisbursementDocument(rmdisbursementdocument_gid, rmdisbursementrequest_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Temp Clear 

        [ActionName("GetRMDisbursementTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIntitutionTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.DaGetRMDisbursementTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductChargesDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesDtl(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstCreditOpsApplication.DaGetProductChargesDtl(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductView")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetailsView(string application_gid)
        {
            MdlMstProductView values = new MdlMstProductView();
            objMstCreditOpsApplication.DaGetProductView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLSABankDocumentUpload")]
        [HttpGet]
        public HttpResponseMessage GetLSABankDocumentUpload(string lsabankaccdtl_gid)
        {
            MdlLSAuploadeddocument values = new MdlLSAuploadeddocument();
            objMstCreditOpsApplication.DaGetLSABankDocumentUpload(lsabankaccdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementRequestAdd")]
        [HttpPost]
        public HttpResponseMessage PostCreditGroupAdd(MdlDisbursementRequestAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementRequestAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementRequestSummary")]
        [HttpGet]        
        public HttpResponseMessage GetDisbursementRequestSummary(string customer_urn,string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementRequestSummary(getsessionvalues.employee_gid, customer_urn, application_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostConfirmDisbursementAcct")]
        [HttpPost]
        public HttpResponseMessage PostConfirmDisbursementAcct(MdlConfirmDisbursementAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostConfirmDisbursementAcct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditDisbursementBankDtls")]
        [HttpGet]
        public HttpResponseMessage GetCreditDisbursementBankDtls(string application_gid)
        {
            RMsanctiondetails values = new RMsanctiondetails();
            objMstCreditOpsApplication.DaGetCreditDisbursementBankDtls(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLSADisbursementBankDtls")]
        [HttpGet]
        public HttpResponseMessage GetCreditDisbursementDtls(string application_gid)
        {
            RMsanctiondetails values = new RMsanctiondetails();
            objMstCreditOpsApplication.DaGetLSADisbursementBankDtls(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsGroupDropDown")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsGroupDropDown(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditOpsGroupDropDown values = new MdlCreditOpsGroupDropDown();
            objMstCreditOpsApplication.DaGetCreditOpsGroupDropDown(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOps2Heads")]
        [HttpGet]
        public HttpResponseMessage GetCredit2Heads(string creditopsgroupmapping_gid)
        {
            MdlCreditOps2Heads objmaster = new MdlCreditOps2Heads();
            objMstCreditOpsApplication.DaGetCreditOps2Heads(creditopsgroupmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("PostDisbursementAssignment")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementAssignment(MdlDisbursementAssignment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementAssignment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementAssignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementAssignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementAssignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbursementAssignCount")]
        [HttpGet]
        public HttpResponseMessage DisbursementAssignCount()
        {
            DisbursementAssignCount values = new DisbursementAssignCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaDisbursementAssignCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementEdit")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementEdit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequestAdd values = new MdlDisbursementRequestAdd();
            objMstCreditOpsApplication.DaGetDisbursementEdit(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementUpdate")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementUpdate(MdlDisbursementRequestAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementApprove")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementApprove(MdlDisbursementRequestAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbursementCount")]
        [HttpGet]
        public HttpResponseMessage DisbursementCount()
        {
            DisbursementAssignCount values = new DisbursementAssignCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaDisbursementCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMDisbursementDtlView")]
        [HttpGet]
        public HttpResponseMessage GetRMDisbursementDtlView(string application_gid)
        {
            MdlDisbursementDtlView values = new MdlDisbursementDtlView();
            objMstCreditOpsApplication.DaGetRMDisbursementDtlView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMakerDisbursementDtlView")]
        [HttpGet]
        public HttpResponseMessage GetMakerDisbursementDtlView(string application_gid)
        {
            MdlDisbursementDtlView values = new MdlDisbursementDtlView();
            objMstCreditOpsApplication.DaGetMakerDisbursementDtlView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckerDisbursementDtlView")]
        [HttpGet]
        public HttpResponseMessage GetCheckerDisbursementDtlView(string application_gid)
        {
            MdlDisbursementDtlView values = new MdlDisbursementDtlView();
            objMstCreditOpsApplication.DaGetCheckerDisbursementDtlView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSancLsApprovalFlag")]
        [HttpGet]
        public HttpResponseMessage GetSancLsApprovalFlag(string application_gid)
        {
            MdlSancLsApprovalFlag values = new MdlSancLsApprovalFlag();
            objMstCreditOpsApplication.DaGetSancLsApprovalFlag(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementDocSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementRequestEdit")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementRequestEdit(string rmdisbursementrequest_gid)
        {
            MdlDisbursementRequestAdd values = new MdlDisbursementRequestAdd();
            objMstCreditOpsApplication.DaGetDisbursementRequestEdit(rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocEditSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocEditSummary(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementDocEditSummary(getsessionvalues.employee_gid, rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbDocumentDeferral")]
        [HttpPost]
        public HttpResponseMessage PostDisbDocumentDeferral(MdlDisbDocumentDeferral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbDocumentDeferral(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbDocumentDeferralSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbDocumentDeferralSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbDocumentDeferral values = new MdlDisbDocumentDeferral();
            objMstCreditOpsApplication.DaGetDisbDocumentDeferralSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDeviationApprovalGroupName")]
        [HttpGet]
        public HttpResponseMessage GetDeviationApprovalGroupName()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDeviationApprovalDropDown values = new MdlDeviationApprovalDropDown();
            objMstCreditOpsApplication.DaGetDeviationApprovalGroupName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDeviationApprovalSubGroupName")]
        [HttpGet]
        public HttpResponseMessage GetDeviationApprovalSubGroupName(string deviationapprovalgroup_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDeviationApprovalDropDown values = new MdlDeviationApprovalDropDown();
            objMstCreditOpsApplication.DaGetDeviationApprovalSubGroupName(getsessionvalues.employee_gid, deviationapprovalgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDeviationApprovalMemberName")]
        [HttpGet]
        public HttpResponseMessage GetDeviationApprovalMemberName(string deviationapprovalgroup_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDeviationApprovalDropDown values = new MdlDeviationApprovalDropDown();
            objMstCreditOpsApplication.DaGetDeviationApprovalMemberName(getsessionvalues.employee_gid, deviationapprovalgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDeviationApprovalManagerName")]
        [HttpGet]
        public HttpResponseMessage GetDeviationApprovalManagerName(string deviationapprovalgroup_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDeviationApprovalDropDown values = new MdlDeviationApprovalDropDown();
            objMstCreditOpsApplication.DaGetDeviationApprovalManagerName(getsessionvalues.employee_gid, deviationapprovalgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbDocDefApprovalConfig")]
        [HttpPost]
        public HttpResponseMessage PostDisbDocDefApprovalConfig(MdlDisbDocumentDeferral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbDocDefApprovalConfig(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbDoDefApprovalConfigSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbDoDefApprovalConfigSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbDocumentDeferral values = new MdlDisbDocumentDeferral();
            objMstCreditOpsApplication.DaGetDisbDoDefApprovalConfigSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbDeferralDocView")]
        [HttpGet]
        public HttpResponseMessage GetDisbDeferralDocView(string disbursementdocdeferral_gid)
        {
            MdlDisbDocumentDeferral values = new MdlDisbDocumentDeferral();
            objMstCreditOpsApplication.DaGetDisbDeferralDocView(disbursementdocdeferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbDeferralDocInactive")]
        [HttpPost]
        public HttpResponseMessage PostDisbDeferralDocInactive(MdlDisbDocumentDeferral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbDeferralDocInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDisbDeferralDocInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetDisbDeferralDocInactiveLogview(string disbursementdocdeferral_gid)
        {
            MdlDisbDocumentDeferral values = new MdlDisbDocumentDeferral();
            objMstCreditOpsApplication.DaGetDisbDeferralDocInactiveLogview(disbursementdocdeferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbDeferralDocEdit")]
        [HttpGet]
        public HttpResponseMessage GetDisbDeferralDocEdit(string disbdocdeferralapprovalconfig_gid)
        {
            MdlDisbDocumentDeferral values = new MdlDisbDocumentDeferral();
            objMstCreditOpsApplication.DaGetDisbDeferralDocEdit(disbdocdeferralapprovalconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PosDisbDeferralDocUpdate")]
        [HttpPost]
        public HttpResponseMessage PosDisbDeferralDocUpdate(MdlDisbDocumentDeferral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPosDisbDeferralDocUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetProductBuyerList(string application_gid)
        {
            MdlMstProductBuyer values = new MdlMstProductBuyer();
            objMstCreditOpsApplication.DaGetProductBuyerList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ImportExcelDisbFarmerIndividual")]
        [HttpPost]
        public HttpResponseMessage ImportExcelIndividual()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstCreditOpsApplication.DaImportExcelDisbFarmerIndividual(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetDisbFarmerIndividualImportLog")]
        [HttpGet]
        public HttpResponseMessage GetDisbFarmerIndividualImportLog(string application_gid)
        {
            MdlExcelImportApplication values = new MdlExcelImportApplication();
            objMstCreditOpsApplication.DaGetDisbFarmerIndividualImportLog(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFarmerIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetFarmerIndividualSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFarmerIndividualSummary values = new MdlFarmerIndividualSummary();
            objMstCreditOpsApplication.DaGetFarmerIndividualSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbFarmerIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetDisbFarmerIndividualView(string farmercontact_gid)
        {
            MdlFarmerIndividualDtlView values = new MdlFarmerIndividualDtlView();
            objMstCreditOpsApplication.DaGetDisbFarmerIndividualView(farmercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbsupplierdocumentUpload")]
        [HttpPost]
        public HttpResponseMessage DisbsupplierdocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            disbsupplieruploaddocument documentname = new disbsupplieruploaddocument();
            objMstCreditOpsApplication.DaDisbsupplierdocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteDisbsupplierdocument")]
        [HttpGet]
        public HttpResponseMessage DeleteDisbsupplierdocument(string disbsupplierbankdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            disbsupplieruploaddocument values = new disbsupplieruploaddocument();
            objMstCreditOpsApplication.DaDeleteDisbsupplierdocument(disbsupplierbankdocument_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementSupplier")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementSupplier(MdlDisbSupplierBankAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementSupplierSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementSupplierSummary(string rmdisbursementrequest_gid)
        {
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbursementSupplierSummary(rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Supplier Doc Temp Clear 
        [ActionName("GetDisbSupplierDocTempClear")]
        [HttpGet]
        public HttpResponseMessage GetDisbSupplierDocTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.DaGetDisbSupplierDocTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbSupplierDtlView")]
        [HttpGet]
        public HttpResponseMessage GetDisbSupplierDtlView(string disbursementsupplier_gid)
        {
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbSupplierDtlView(disbursementsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbsupplierdocumentView")]
        [HttpGet]
        public HttpResponseMessage DisbsupplierdocumentView(string disbursementsupplier_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            disbsupplieruploaddocument values = new disbsupplieruploaddocument();
            objMstCreditOpsApplication.DaDisbsupplierdocumentView(disbursementsupplier_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Disbursement Doc Temp Clear 
        [ActionName("GetDisbursementDocTempClear")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.DaGetDisbursementDocTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitCoapplicantContactDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitCoapplicantContactDtlAdd(MdlMstCoApplicantContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaSubmitCoapplicantContactDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CoapplicantContactDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CoapplicantContactDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstCreditOpsApplication.DaCoapplicantContactDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetCoapplicantContactDocList")]
        [HttpGet]
        public HttpResponseMessage GetCoapplicantContactDocList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCoapplicantContactDocument values = new MdlCoapplicantContactDocument();
            objMstCreditOpsApplication.DaGetCoapplicantContactDocList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CoapplicantContactDocDelete")]
        [HttpGet]
        public HttpResponseMessage CoapplicantContactDocDelete(string coapplicantcontact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCoapplicantContactDocument values = new MdlCoapplicantContactDocument();
            objMstCreditOpsApplication.DaCoapplicantContactDocDelete(coapplicantcontact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CoapplicantPANForm60DocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CoapplicantPANForm60DocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstCreditOpsApplication.DaCoapplicantPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetCoapplicantPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCoapplicantContactPANForm60 values = new MdlCoapplicantContactPANForm60();
            objMstCreditOpsApplication.DaGetCoapplicantPANForm60List(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CoapplicantPANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage CoapplicantPANForm60Delete(string coapplicantcontact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCoapplicantContactPANForm60 values = new MdlCoapplicantContactPANForm60();
            objMstCreditOpsApplication.DaCoapplicantPANForm60Delete(coapplicantcontact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Temp Clear 
        [ActionName("GetCoApplicantTempClear")]
        [HttpGet]
        public HttpResponseMessage GetCoApplicantTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.GetCoApplicantTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbCoApplicantDtlView")]
        [HttpGet]
        public HttpResponseMessage GetDisbCoApplicantDtlView(string contactcoapplicant_gid)
        {
            MdlCoApplicantDtlView values = new MdlCoApplicantDtlView();
            objMstCreditOpsApplication.DaGetDisbCoApplicantDtlView(contactcoapplicant_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLSARefNoDropDown")]
        [HttpGet]
        public HttpResponseMessage GetLSARefNoDropDown(string application2sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLSARefNoDropDown values = new MdlMstLSARefNoDropDown();
            objMstCreditOpsApplication.DaGetLSARefNoDropDown(getsessionvalues.employee_gid, application2sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbFarmerIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbFarmerIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFarmerIndividualSummary values = new MdlFarmerIndividualSummary();
            objMstCreditOpsApplication.DaGetDisbFarmerIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbSupplierSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbSupplierSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbSupplierSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbFarmerSupplierTempClear")]
        [HttpGet]
        public HttpResponseMessage GetDisbFarmerSupplierTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.DaGetDisbFarmerSupplierTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementCreditOpsView")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementCreditOpsView(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequestAdd values = new MdlDisbursementRequestAdd();
            objMstCreditOpsApplication.DaGetDisbursementCreditOpsView(getsessionvalues.employee_gid, rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRmDisbursementDocumentDtl")]
        [HttpGet]
        public HttpResponseMessage GetRmDisbursementDocumentDtl(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetRmDisbursementDocumentDtl(getsessionvalues.employee_gid, rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbFarmerIndividualCreditOps")]
        [HttpGet]
        public HttpResponseMessage GetDisbFarmerIndividualCreditOps(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFarmerIndividualSummary values = new MdlFarmerIndividualSummary();
            objMstCreditOpsApplication.DaGetDisbFarmerIndividualCreditOps(getsessionvalues.employee_gid, rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDidbScannedDocList")]
        [HttpGet]
        public HttpResponseMessage GetDidbScannedDocList(string application_gid)
        {
            ScannnedDocTaggedDocumentList values = new ScannnedDocTaggedDocumentList();
            objMstCreditOpsApplication.DaGetDidbScannedDocList(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDidbPhysicalDocList")]
        [HttpGet]
        public HttpResponseMessage GetDidbPhysicalDocList(string application_gid)
        {
            PhysicalDocTaggedDocumentList values = new PhysicalDocTaggedDocumentList();
            objMstCreditOpsApplication.DaGetDidbPhysicalDocList(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditOpsSupplierDisbAmountUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditOpsSupplierDisbAmountUpdate(MdlDisbSupplierBankAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaCreditOpsSupplierDisbAmountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsSupplierDisbAmountView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsSupplierDisbAmountView(string disbursementsupplier_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetCreditOpsSupplierDisbAmountView(getsessionvalues.employee_gid, disbursementsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditOpsFarmerDisbAmountUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditOpsFarmerDisbAmountUpdate(MdlFarmerIndividualDtlView values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaCreditOpsFarmerDisbAmountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsFarmerDisbAmountView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsFarmerDisbAmountView(string farmercontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFarmerIndividualDtlView values = new MdlFarmerIndividualDtlView();
            objMstCreditOpsApplication.DaGetCreditOpsFarmerDisbAmountView(getsessionvalues.employee_gid, farmercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementRequestReject")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementRequestReject(MdlDisbursementReject values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementRequestReject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequest values = new MdlDisbursementRequest();
            objMstCreditOpsApplication.DaGetDisbursementRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditOpsCheckerFarmerDisbAmountUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditOpsCheckerFarmerDisbAmountUpdate(MdlFarmerIndividualDtlView values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaCreditOpsCheckerFarmerDisbAmountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsCheckerFarmerDisbAmountView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsCheckerFarmerDisbAmountView(string farmercontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFarmerIndividualDtlView values = new MdlFarmerIndividualDtlView();
            objMstCreditOpsApplication.DaGetCreditOpsCheckerFarmerDisbAmountView(getsessionvalues.employee_gid, farmercontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditOpsCheckerSupplierDisbAmountUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditOpsCheckerSupplierDisbAmountUpdate(MdlDisbSupplierBankAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaCreditOpsCheckerSupplierDisbAmountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsCheckerSupplierDisbAmountView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsCheckerSupplierDisbAmountView(string disbursementsupplier_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetCreditOpsCheckerSupplierDisbAmountView(getsessionvalues.employee_gid, disbursementsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbApplicantBankDtl")]
        [HttpPost]
        public HttpResponseMessage PostDisbApplicantBankDtl(MdlDisbSupplierBankAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbApplicantBankDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbApplicantdocumentUpload")]
        [HttpPost]
        public HttpResponseMessage DisbApplicantdocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            disbsupplieruploaddocument documentname = new disbsupplieruploaddocument();
            objMstCreditOpsApplication.DaDisbApplicantdocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteDisbApplicantdocument")]
        [HttpGet]
        public HttpResponseMessage DeleteDisbApplicantdocument(string disbapplicantbankdocument_gid,string disbapplicantbankdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            disbsupplieruploaddocument values = new disbsupplieruploaddocument();
            objMstCreditOpsApplication.DaDeleteDisbApplicantdocument(disbapplicantbankdocument_gid, disbapplicantbankdtl_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbApplicantSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbApplicantSummary(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbApplicantSummary(rmdisbursementrequest_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbApplicantDocTempClear")]
        [HttpGet]
        public HttpResponseMessage GetDisbApplicantDocTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.DaGetDisbApplicantDocTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbApplicantBankDtlView")]
        [HttpGet]
        public HttpResponseMessage GetDisbApplicantBankDtlView(string disbapplicantbankdtl_gid)
        {
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbApplicantBankDtlView(disbapplicantbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbApplicantBankAcctDocView")]
        [HttpGet]
        public HttpResponseMessage DisbApplicantBankAcctDocView(string disbapplicantbankdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            disbsupplieruploaddocument values = new disbsupplieruploaddocument();
            objMstCreditOpsApplication.DaDisbApplicantBankAcctDocView(disbapplicantbankdtl_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementApprovalDtlView")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementApprovalDtlView(string rmdisbursementrequest_gid)
        {
            MdlDisbursementDtlView values = new MdlDisbursementDtlView();
            objMstCreditOpsApplication.DaGetDisbursementApprovalDtlView(rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocumentSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocumentSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementDocument values = new MdlDisbursementDocument();
            objMstCreditOpsApplication.DaGetDisbursementDocumentSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocumentApprovalConfigSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocumentApprovalConfigSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementDocument values = new MdlDisbursementDocument();
            objMstCreditOpsApplication.DaGetDisbursementDocumentApprovalConfigSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementDocument")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementDocument(MdlDisbursementDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementDocumentApprovalConfig")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementDocumentApprovalConfig(MdlDisbursementDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementDocumentApprovalConfig(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocumentView")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocumentView(string verticaldisbursementdocument_gid)
        {
            MdlDisbursementDocument values = new MdlDisbursementDocument();
            objMstCreditOpsApplication.DaGetDisbursementDocumentView(verticaldisbursementdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementDocumentInactive")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementDocumentInactive(MdlDisbursementDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementDocumentInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocumentInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocumentInactiveLogview(string verticaldisbursementdocument_gid)
        {
            MdlDisbursementDocument values = new MdlDisbursementDocument();
            objMstCreditOpsApplication.DaGetDisbursementDocumentInactiveLogview(verticaldisbursementdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementDocumentEdit")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementDocumentEdit(string disbursementdocumentapprovalconfig_gid)
        {
            MdlDisbursementDocument values = new MdlDisbursementDocument();
            objMstCreditOpsApplication.DaGetDisbursementDocumentEdit(disbursementdocumentapprovalconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementDocumentUpdate")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementDocumentUpdate(MdlDisbursementDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementDocumentUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementBankAccountSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementBankAccountSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementBankAccount values = new MdlDisbursementBankAccount();
            objMstCreditOpsApplication.DaGetDisbursementBankAccountSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementBankAccountApprovalConfigSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementBankAccountApprovalConfigSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementBankAccount values = new MdlDisbursementBankAccount();
            objMstCreditOpsApplication.DaGetDisbursementBankAccountApprovalConfigSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementBankAccount")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementBankAccount(MdlDisbursementBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementBankAccount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementBankAccountApprovalConfig")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementBankAccountApprovalConfig(MdlDisbursementBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementBankAccountApprovalConfig(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementBankAccountView")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementBankAccountView(string disbursementbankaccount_gid)
        {
            MdlDisbursementBankAccount values = new MdlDisbursementBankAccount();
            objMstCreditOpsApplication.DaGetDisbursementBankAccountView(disbursementbankaccount_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementBankAccountInactive")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementBankAccountInactive(MdlDisbursementBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementBankAccountInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementbankAccountInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementbankAccountInactiveLogview(string disbursementbankaccount_gid)
        {
            MdlDisbursementBankAccount values = new MdlDisbursementBankAccount();
            objMstCreditOpsApplication.DaGetDisbursementbankAccountInactiveLogview(disbursementbankaccount_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementBankAccountEdit")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementBankAccountEdit(string disbursementbankaccountapprovalconfig_gid)
        {
            MdlDisbursementBankAccount values = new MdlDisbursementBankAccount();
            objMstCreditOpsApplication.DaGetDisbursementBankAccountEdit(disbursementbankaccountapprovalconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementBankAccountUpdate")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementBankAccountUpdate(MdlDisbursementBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementBankAccountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow30Summary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow30Summary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementODBelow30 values = new MdlDisbursementODBelow30();
            objMstCreditOpsApplication.DaGetDisbursementODBelow30Summary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow30ApprovalConfigSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow30ApprovalConfigSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementODBelow30 values = new MdlDisbursementODBelow30();
            objMstCreditOpsApplication.DaGetDisbursementODBelow30ApprovalConfigSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow30")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow30(MdlDisbursementODBelow30 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow30(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow30ApprovalConfig")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow30ApprovalConfig(MdlDisbursementODBelow30 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow30ApprovalConfig(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow30View")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow30View(string disbursementodbelow30_gid)
        {
            MdlDisbursementODBelow30 values = new MdlDisbursementODBelow30();
            objMstCreditOpsApplication.DaGetDisbursementODBelow30View(disbursementodbelow30_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow30Inactive")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow30Inactive(MdlDisbursementODBelow30 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow30Inactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow30InactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow30InactiveLogview(string disbursementodbelow30_gid)
        {
            MdlDisbursementODBelow30 values = new MdlDisbursementODBelow30();
            objMstCreditOpsApplication.DaGetDisbursementODBelow30InactiveLogview(disbursementodbelow30_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow30Edit")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow30Edit(string disbursementodbelow30approvalconfig_gid)
        {
            MdlDisbursementODBelow30 values = new MdlDisbursementODBelow30();
            objMstCreditOpsApplication.DaGetDisbursementODBelow30Edit(disbursementodbelow30approvalconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow30Update")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow30Update(MdlDisbursementODBelow30 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow30Update(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDisbursementODBelow90Summary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow90Summary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementODBelow90 values = new MdlDisbursementODBelow90();
            objMstCreditOpsApplication.DaGetDisbursementODBelow90Summary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow90ApprovalConfigSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow90ApprovalConfigSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementODBelow90 values = new MdlDisbursementODBelow90();
            objMstCreditOpsApplication.DaGetDisbursementODBelow90ApprovalConfigSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow90")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow90(MdlDisbursementODBelow90 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow90(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow90ApprovalConfig")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow90ApprovalConfig(MdlDisbursementODBelow90 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow90ApprovalConfig(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow90View")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow90View(string disbursementodbelow90_gid)
        {
            MdlDisbursementODBelow90 values = new MdlDisbursementODBelow90();
            objMstCreditOpsApplication.DaGetDisbursementODBelow90View(disbursementodbelow90_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow90Inactive")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow90Inactive(MdlDisbursementODBelow90 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow90Inactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow90InactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow90InactiveLogview(string disbursementodbelow90_gid)
        {
            MdlDisbursementODBelow90 values = new MdlDisbursementODBelow90();
            objMstCreditOpsApplication.DaGetDisbursementODBelow90InactiveLogview(disbursementodbelow90_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementODBelow90Edit")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementODBelow90Edit(string disbursementodbelow90approvalconfig_gid)
        {
            MdlDisbursementODBelow90 values = new MdlDisbursementODBelow90();
            objMstCreditOpsApplication.DaGetDisbursementODBelow90Edit(disbursementodbelow90approvalconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDisbursementODBelow90Update")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementODBelow90Update(MdlDisbursementODBelow90 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementODBelow90Update(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPenalWaiverSummary")]
        [HttpGet]
        public HttpResponseMessage GetPenalWaiverSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPenalWaiver values = new MdlPenalWaiver();
            objMstCreditOpsApplication.DaGetPenalWaiverSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPenalWaiverApprovalConfigSummary")]
        [HttpGet]
        public HttpResponseMessage GetPenalWaiverApprovalConfigSummary(string vertical_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPenalWaiver values = new MdlPenalWaiver();
            objMstCreditOpsApplication.DaGetPenalWaiverApprovalConfigSummary(getsessionvalues.employee_gid, vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPenalWaiver")]
        [HttpPost]
        public HttpResponseMessage PostPenalWaiver(MdlPenalWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostPenalWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPenalWaiverApprovalConfig")]
        [HttpPost]
        public HttpResponseMessage PostPenalWaiverApprovalConfig(MdlPenalWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostPenalWaiverApprovalConfig(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPenalWaiverView")]
        [HttpGet]
        public HttpResponseMessage GetPenalWaiverView(string penalwaiver_gid)
        {
            MdlPenalWaiver values = new MdlPenalWaiver();
            objMstCreditOpsApplication.DaGetPenalWaiverView(penalwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPenalWaiverInactive")]
        [HttpPost]
        public HttpResponseMessage PostPenalWaiverInactive(MdlPenalWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostPenalWaiverInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPenalWaiverInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetPenalWaiverInactiveLogview(string penalwaiver_gid)
        {
            MdlPenalWaiver values = new MdlPenalWaiver();
            objMstCreditOpsApplication.DaGetPenalWaiverInactiveLogview(penalwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPenalWaiverEdit")]
        [HttpGet]
        public HttpResponseMessage GetPenalWaiverEdit(string penalwaiverapprovalconfig_gid)
        {
            MdlPenalWaiver values = new MdlPenalWaiver();
            objMstCreditOpsApplication.DaGetPenalWaiverEdit(penalwaiverapprovalconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPenalWaiverUpdate")]
        [HttpPost]
        public HttpResponseMessage PostPenalWaiverUpdate(MdlPenalWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostPenalWaiverUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementApplicantView")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementApplicantView(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequestAdd values = new MdlDisbursementRequestAdd();
            objMstCreditOpsApplication.DaGetDisbursementApplicantView(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementRejectedView")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementRejectedView(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementRequestAdd values = new MdlDisbursementRequestAdd();
            objMstCreditOpsApplication.DaGetDisbursementRejectedView(getsessionvalues.employee_gid, rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostConfirmDisbursementRequest")]
        [HttpPost]
        public HttpResponseMessage PostConfirmDisbursementRequest(MdlConfirmDisbursementAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostConfirmDisbursementRequest(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankAccountTempClear")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstCreditOpsApplication.DaGetBankAccountTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApplicantSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicantSummary(string application_gid, string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementDtlView values = new MdlDisbursementDtlView();
            objMstCreditOpsApplication.DaGetApplicantSummary(rmdisbursementrequest_gid, getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetNewApplicantSummary")]
        [HttpGet]
        public HttpResponseMessage GetNewApplicantSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbursementDtlView values = new MdlDisbursementDtlView();
            objMstCreditOpsApplication.DaGetNewApplicantSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDisbursementAmount")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementAmount(MdlConfirmDisbursementAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementAmount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbAppilicantDtlView")]
        [HttpGet]
        public HttpResponseMessage GetDisbAppilicantDtlView(string disbapplicantbankdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbAppilicantDtlView(getsessionvalues.employee_gid, disbapplicantbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateBankAccountDetails")]
        [HttpPost]
        public HttpResponseMessage PostUpdateBankAccountDetails(MdlBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostUpdateBankAccountDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditOpsApplicantDisbAmountUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditOpsApplicantDisbAmountUpdate(MdlDisbCreditOpsApplicantBankAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaCreditOpsApplicantDisbAmountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsApplicantDisbAmountView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsApplicantDisbAmountView(string disbursementamount_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbCreditOpsApplicantBankAcct values = new MdlDisbCreditOpsApplicantBankAcct();
            objMstCreditOpsApplication.DaGetCreditOpsApplicantDisbAmountView(getsessionvalues.employee_gid, disbursementamount_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CheckerApplicantDisbAmountUpdate")]
        [HttpPost]
        public HttpResponseMessage CheckerApplicantDisbAmountUpdate(MdlDisbCreditOpsApplicantBankAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaCheckerApplicantDisbAmountUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckerApplicantDisbAmountView")]
        [HttpGet]
        public HttpResponseMessage GetCheckerApplicantDisbAmountView(string disbursementamount_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbCreditOpsApplicantBankAcct values = new MdlDisbCreditOpsApplicantBankAcct();
            objMstCreditOpsApplication.DaGetCheckerApplicantDisbAmountView(getsessionvalues.employee_gid, disbursementamount_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DisbursementAmountCal")]
         [HttpGet]
        public HttpResponseMessage DisbursementAmountCal(string application_gid, string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlConfirmDisbursementAcct values = new MdlConfirmDisbursementAcct();
            objMstCreditOpsApplication.DaDisbursementAmountCal(getsessionvalues.employee_gid, rmdisbursementrequest_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DisbursementAmountCalValidation")]
        [HttpPost]
        public HttpResponseMessage DisbursementAmountCalValidation(MdlConfirmDisbursementAcct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            //MdlConfirmDisbursementAcct values = new MdlConfirmDisbursementAcct();
            objMstCreditOpsApplication.DaDisbursementAmountCalValidation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //instrument Encore Integration - Payment Option Dropdown 

        [ActionName("Getinstrumentlist")]
        [HttpGet]
        public HttpResponseMessage DaGetinstrumentlist()
        {
            mdlinstrument objmaster = new mdlinstrument();
            objMstCreditOpsApplication.DaGetinstrumentlist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("DeleteDisbFarmerDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteDisbFarmerDtl(string farmercontact_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaDeleteDisbFarmerDtl(farmercontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Maker Farmer Disbursement Amount Export
        [ActionName("ExportFarmerDisbAmount")]
        [HttpGet]
        public HttpResponseMessage GetExportFarmerDisbAmount(string rmdisbursementrequest_gid, string application_gid)
        {
            MdlExcelImportApplication objMstExportFarmerDisbAmount = new MdlExcelImportApplication();
            objMstCreditOpsApplication.DaGetExportFarmerDisbAmount(rmdisbursementrequest_gid, application_gid,objMstExportFarmerDisbAmount);
            return Request.CreateResponse(HttpStatusCode.OK, objMstExportFarmerDisbAmount);
        }
        // Maker Farmer Disbursement Amount Import
        [ActionName("ImportExcelFarmerDisbAmount")]
        [HttpPost]
        public HttpResponseMessage ImportExcelFarmerDisbAmount()
        {
            HttpRequest httpRequest;
            MdlExcelImportApplication objMstExportFarmerDisbAmount = new MdlExcelImportApplication();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstCreditOpsApplication.DaImportExcelFarmerDisbAmount(httpRequest, getsessionvalues.employee_gid, objResult, objMstExportFarmerDisbAmount);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Checker Farmer Disbursement Amount Export
        [ActionName("ExportCheckerFarmerDisbAmount")]
        [HttpGet]
        public HttpResponseMessage GetExportCheckerFarmerDisbAmount(string rmdisbursementrequest_gid, string application_gid)
        {
            MdlExcelImportApplication objMstExportFarmerDisbAmount = new MdlExcelImportApplication();
            objMstCreditOpsApplication.DaGetExportCheckerFarmerDisbAmount(rmdisbursementrequest_gid, application_gid, objMstExportFarmerDisbAmount);
            return Request.CreateResponse(HttpStatusCode.OK, objMstExportFarmerDisbAmount);
        }
        // Checker Farmer Disbursement Amount Import
        [ActionName("ImportExcelCheckerFarmerDisbAmount")]
        [HttpPost]
        public HttpResponseMessage ImportExcelCheckerFarmerDisbAmount()
        {
            HttpRequest httpRequest;
            MdlExcelImportApplication objMstExportCheckerFarmerDisbAmount = new MdlExcelImportApplication();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstCreditOpsApplication.DaImportExcelCheckerFarmerDisbAmount(httpRequest, getsessionvalues.employee_gid, objResult, objMstExportCheckerFarmerDisbAmount);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetSupplierNameDropDown")]
        [HttpGet]
        public HttpResponseMessage GetSupplierNameDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplierName values = new MdlMstSupplierName();
            objMstCreditOpsApplication.DaGetSupplierNameDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSupplierIfscCodeDropDown")]
        [HttpGet]
        public HttpResponseMessage GetSupplierIfscCodeDropDown(string supplier_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplierIfscCode values = new MdlMstSupplierIfscCode();
            objMstCreditOpsApplication.DaGetSupplierIfscCodeDropDown(supplier_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDispSuplBankAcctDtlView")]
        [HttpGet]
        public HttpResponseMessage GetDispSuplBankAcctDtlView(string supplier2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDispSuplBankAcctDtl values = new MdlDispSuplBankAcctDtl();
            objMstCreditOpsApplication.DaGetDispSuplBankAcctDtlView(getsessionvalues.employee_gid, supplier2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDisbursementEditSupplierSummary")]
        [HttpGet]
        public HttpResponseMessage GetDisbursementEditSupplierSummary(string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDisbSupplierBankAcct values = new MdlDisbSupplierBankAcct();
            objMstCreditOpsApplication.DaGetDisbursementEditSupplierSummary(getsessionvalues.employee_gid,rmdisbursementrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Co-Applicant Details Export Excel
        [ActionName("CoApplicantExportExcel")]
        [HttpGet]
        public HttpResponseMessage GetCoApplicantExportExcel(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlExcelImportApplication objMstCoApplicantExportExcel = new MdlExcelImportApplication();
            objMstCreditOpsApplication.DaGetCoApplicantExportExcel(getsessionvalues.employee_gid, application_gid, objMstCoApplicantExportExcel);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCoApplicantExportExcel);
        }
        // Co-Applicant Details Import Excel
        [ActionName("ImportExcelDisbCoApplicant")]
        [HttpPost]
        public HttpResponseMessage ImportExcelDisbCoApplicant()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstCreditOpsApplication.DaImportExcelDisbCoApplicant(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Co-Applicant Details Edit Export Excel
        [ActionName("CoApplicantEditExportExcel")]
        [HttpGet]
        public HttpResponseMessage GetCoApplicantEditExportExcel(string application_gid, string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlExcelImportApplication objMstCoApplicantExportExcel = new MdlExcelImportApplication();
            objMstCreditOpsApplication.DaGetCoApplicantEditExportExcel(getsessionvalues.employee_gid, application_gid, rmdisbursementrequest_gid,objMstCoApplicantExportExcel);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCoApplicantExportExcel);
        }
        [ActionName("PostDisbursementMakerApprove")]
        [HttpPost]
        public HttpResponseMessage PostDisbursementMakerApprove(MdlDisbursementRequestAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstCreditOpsApplication.DaPostDisbursementMakerApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}