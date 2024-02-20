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
    /// This Controllers will provide access to edit datas from various stages in Supplier Application creation (General, Company, Individual, Overall limit, Product, charges, trade, Bureau & Done)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>


    [RoutePrefix("api/AgrMstSuprApplicationEdit")]
    [Authorize]

    public class AgrMstSuprApplicationEditController : ApiController
    {
        DaAgrMstSuprApplicationEdit objDaAgrMstApplicationEdit = new DaAgrMstSuprApplicationEdit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Edit Social And Trade Capital

        [ActionName("SocialAndTradeEdit")]
        [HttpGet]
        public HttpResponseMessage SocialAndTradeEdit(string application_gid)
        {
            MdlMstApplicationEdit values = new MdlMstApplicationEdit();
            objDaAgrMstApplicationEdit.DaSocialAndTradeEdit(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SocialAndTradeCapitalUpdate")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalUpdate(MdlMstApplicationEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSocialAndTradeCapitalUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualEdit")]
        [HttpGet]
        public HttpResponseMessage CICIndividualEdit(string contact2bureau_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstApplicationEdit.DaCICIndividualEdit(contact2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualDocEdit")]
        [HttpGet]
        public HttpResponseMessage CICIndividualDocEdit(string contact_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaCICIndividualDocEdit(contact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CICInstitutionEdit")]
        [HttpGet]
        public HttpResponseMessage CICInstitutionEdit(string institution2bureau_gid)
        {
            MdlCICInstitution values = new MdlCICInstitution();
            objDaAgrMstApplicationEdit.DaCICInstitutionEdit(institution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICInstitutionDocEdit")]
        [HttpGet]
        public HttpResponseMessage CICInstitutionDocEdit(string institution_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaCICInstitutionDocEdit(institution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualUpdate")]
        [HttpPost]
        public HttpResponseMessage CICIndividualUpdate(MdlCICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaCICIndividualUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICInstitutionUpdate")]
        [HttpPost]
        public HttpResponseMessage CICInstitutionUpdate(MdlCICInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaCICInstitutionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualDocumentUploadEdit")]
        [HttpPost]
        public HttpResponseMessage CICIndividualDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaAgrMstApplicationEdit.DaCICIndividualDocumentUploadEdit(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("DaTempCICUploadDocDelete")]
        [HttpGet]
        public HttpResponseMessage DaTempCICUploadDocDelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstApplicationEdit.DaTempCICUploadDocDelete(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSocialTradeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSocialTradeSummary(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrMstApplicationEdit.DaGetSocialTradeSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCICEditIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetCICIndividualSummary(string application_gid)
        {

            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstApplicationEdit.DaGetCICIndividualSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCICEditInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetCICInstitutionSummary(string application_gid)
        {

            MdlCICInstitution values = new MdlCICInstitution();
            objDaAgrMstApplicationEdit.DaGetCICInstitutionSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Edit Document Upload

        [ActionName("InstitutionEditDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionEditDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionEditDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Institution Edit Document Upload Temp List

        [ActionName("InstitutionEditDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditDocumentTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument values = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionEditDocumentTmpList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Edit Document Delete

        [ActionName("InstitutionEditDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditDocumentDelete(string institution2documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionEditDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Institution Edit Form-60 Document Upload

        [ActionName("InstitutionEditForm_60DocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionEditForm_60DocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionEditForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Institution Edit Form-60 Document Upload Temp List

        [ActionName("InstitutionEditForm60TmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditForm60TmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument values = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionEditForm60TmpList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Edit Form-60 Document Delete

        [ActionName("InstitutionEditForm_60DocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditForm_60DocumentDelete(string institution2form60documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionEditForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Institution GST List

        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string institution_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objDaAgrMstApplicationEdit.DaInstitutionGSTList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Mobile Number List

        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string institution_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaInstitutionMobileNoList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Email Address List

        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string institution_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaAgrMstApplicationEdit.DaInstitutionEmailAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Address List

        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string institution_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaAgrMstApplicationEdit.DaInstitutionAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution License List

        [ActionName("InstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseList(string institution_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaAgrMstApplicationEdit.DaInstitutionLicenseList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionDocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionDocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionForm60DocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm60DocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objDaAgrMstApplicationEdit.DaInstitutionForm60DocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Details Edit

        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string institution_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objDaAgrMstApplicationEdit.DaInstitutionDetailsEdit(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Institution Details Update

        [ActionName("UpdateInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution GST Temp List

        [ActionName("InstitutionGSTTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaAgrMstApplicationEdit.DaInstitutionGSTTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Mobile Number Temp List

        [ActionName("InstitutionMobileNoTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaInstitutionMobileNoTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Email Address Temp List

        [ActionName("InstitutionEmailAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaAgrMstApplicationEdit.DaInstitutionEmailAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Address Temp List

        [ActionName("InstitutionAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaAgrMstApplicationEdit.DaInstitutionAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution License Temp List

        [ActionName("InstitutionLicenseTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaAgrMstApplicationEdit.DaInstitutionLicenseTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product & Charges Edit

        [ActionName("GetProductChargesEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesEdit(string application_gid)
        {
            MdlProductCharges values = new MdlProductCharges();
            objDaAgrMstApplicationEdit.DaGetProductChargesEdit(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Detail List

        [ActionName("LoanDetailList")]
        [HttpGet]
        public HttpResponseMessage LoanDetailList(string application_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaLoanDetailList(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Temp Detail List

        [ActionName("LoanTempDetailList")]
        [HttpGet]
        public HttpResponseMessage LoanTempDetailList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaAgrMstApplicationEdit.DaLoanTempDetailList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Details Edit

        [ActionName("LoanDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage LoanDetailsEdit(string application2loan_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaAgrMstApplicationEdit.DaLoanDetailsEdit(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Details Update

        [ActionName("LoanDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage LoanDetailsUpdate(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaLoanDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Loan Detail

        [ActionName("DeleteLoanDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteLoanDetail(string application2loan_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaAgrMstApplicationEdit.DaDeleteLoanDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        // Buyer Details List

        [ActionName("BuyerDetailsList")]
        [HttpGet]
        public HttpResponseMessage BuyerDetailsList(string application2loan_gid)
        {
            MdlMstBuyer values = new MdlMstBuyer();
            objDaAgrMstApplicationEdit.DaBuyerDetailsList(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Buyer Temp Details List

        [ActionName("BuyerTempDetailsList")]
        [HttpGet]
        public HttpResponseMessage BuyerTempDetailsList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objDaAgrMstApplicationEdit.DaBuyerTempDetailsList(getsessionvalues.employee_gid, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Buyer Details Edit

        [ActionName("BuyerDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage BuyerDetailsEdit(string application2buyer_gid)
        {
            MdlMstBuyer values = new MdlMstBuyer();
            objDaAgrMstApplicationEdit.DaBuyerDetailsEdit(application2buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Buyer Details Update

        [ActionName("BuyerDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage BuyerDetailsUpdate(MdlMstBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DBuyerDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Delete Buyer Details

        [ActionName("DeleteBuyerDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteBuyerDetails(string application2buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objDaAgrMstApplicationEdit.DaDeleteBuyerDetails(application2buyer_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Collateral Details List

        [ActionName("CollateralDetailsList")]
        [HttpGet]
        public HttpResponseMessage CollateralDetailsList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaAgrMstApplicationEdit.DaCollateralDetailsList(getsessionvalues.employee_gid,application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Collateral Temp Details List

        [ActionName("CollateralTempDetailsList")]
        [HttpGet]
        public HttpResponseMessage CollateralTempDetailsList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaAgrMstApplicationEdit.DaCollateralTempDetailsList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        // Collateral Details Edit

        [ActionName("CollateralDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage CollateralDetailsEdit(string application2collateral_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaAgrMstApplicationEdit.DaCollateralDetailsEdit(application2collateral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Collateral Details Update

        [ActionName("CollateralDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage CollateralDetailsUpdate(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaCollateralDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Collateral Details

        [ActionName("DeleteCollateralDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteCollateralDetails(string application2collateral_gid)
        {
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaAgrMstApplicationEdit.DaDeleteCollateralDetails(application2collateral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation Details List

        [ActionName("HypothecationDetailsList")]
        [HttpGet]
        public HttpResponseMessage HypothecationDetailsList(string application_gid)
        {
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaAgrMstApplicationEdit.DaHypothecationDetailsList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation Temp Details List

        [ActionName("HypothecationTempDetailsList")]
        [HttpGet]
        public HttpResponseMessage HypothecationTempDetailsList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaAgrMstApplicationEdit.DaHypothecationTempDetailsList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation Details Edit

        [ActionName("HypothecationDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage HypothecationDetailsEdit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaAgrMstApplicationEdit.DaHypothecationDetailsEdit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditHypothecation")]
        [HttpGet]
        public HttpResponseMessage GetEditHypothecation(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaAgrMstApplicationEdit.DaGetEditHypothecation(application_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        // Hypothecation Details Update

        [ActionName("HypothecationDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage HypothecationDetailsUpdate(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaHypothecationDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateOverallLimit")]
        [HttpPost]
        public HttpResponseMessage UpdateOverallLimit(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateOverallLimit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostLoanEditDtl")]
        [HttpPost]
        public HttpResponseMessage PostLoanEditDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostLoanEditDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Delete Hypothecation Details
        [ActionName("GetEditLimit")]
        [HttpGet]
        public HttpResponseMessage GetEditLimit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrMstApplicationEdit.DaGetEditLimit(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLoanLimit")]
        [HttpPost]
        public HttpResponseMessage GetEditLoanLimit(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           // MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrMstApplicationEdit.GetEditLoanLimit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditproduct")]
        [HttpGet]
        public HttpResponseMessage GetEditproduct(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlList values = new MdlList();
            objDaAgrMstApplicationEdit.DaGetEditproduct(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditServiceCharges")]
        [HttpPost]
        public HttpResponseMessage PostEditServiceCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostEditServiceCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteHypothecationDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteHypothecationDetails(string application2hypothecation_gid)
        {
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaAgrMstApplicationEdit.DaDeleteHypothecationDetails(application2hypothecation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLoanDtl")]
        [HttpGet]
        public HttpResponseMessage GetEditLoanDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaAgrMstApplicationEdit.DaGetEditLoanDtl(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update Product Charges

        [ActionName("UpdateProductCharges")]
        [HttpPost]
        public HttpResponseMessage UpdateProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Delete Product Charges

        [ActionName("GetProductChargesTempClear")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrMstApplicationEdit.DaGetProductChargesTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Collateral Document Temp List

        [ActionName("CollateralDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage CollateralDocumentTempList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objDaAgrMstApplicationEdit.DaCollateralDocumentTempList(getsessionvalues.employee_gid, application2loan_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Collateral Document Temp List

        [ActionName("CollateralDocumentList")]
        [HttpGet]
        public HttpResponseMessage CollateralDocumentList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objDaAgrMstApplicationEdit.DaCollateralDocumentList(application2loan_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Hypothecation Document Temp List

        [ActionName("HypothecationDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage HypothecationDocumentTempList(string application2hypothecation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objDaAgrMstApplicationEdit.DaHypothecationDocumentTempList(getsessionvalues.employee_gid, application2hypothecation_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        [ActionName("Editcollateraldocument")]
        [HttpPost]
        public HttpResponseMessage Editcollateraldocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objDaAgrMstApplicationEdit.DaEditcollateraldocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("EditHypoDoc")]
        [HttpPost]
        public HttpResponseMessage EditHypoDoc()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objDaAgrMstApplicationEdit.DaEditHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Individual

        // Mobile Number Add 
        [ActionName("PostIndividualMobileNumber")]
        [HttpPost]
        public HttpResponseMessage PostIndividualMobileNumber(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostIndividualMobileNumber(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Mobile No List
        [ActionName("GetIndividualMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualMobileNoList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaAgrMstApplicationEdit.DaGetIndividualMobileNoTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Mobile No List
        [ActionName("GetIndividualMobileNoList")]
        [HttpGet]
        public HttpResponseMessage IndividualMobileNoList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaAgrMstApplicationEdit.DaGetIndividualMobileNoList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Mobile No

        [ActionName("EditIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIndividualMobileNo(string contact2mobileno_gid)
        {
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaAgrMstApplicationEdit.DaEditIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        // Update Individual Mobile No

        [ActionName("UpdateIndividualMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualMobileNo(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateIndividualMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Mobile No----------//
        [ActionName("DeleteIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualMobileNo(string contact2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaAgrMstApplicationEdit.DaDeleteIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Add 
        [ActionName("PostIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostIndividualEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Email Address List
        [ActionName("GetIndividualEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualEmailAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaAgrMstApplicationEdit.DaGetIndividualEmailAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Get Email Address List
        [ActionName("GetIndividualEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualEmailAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaAgrMstApplicationEdit.DaGetIndividualEmailAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Email Address

        [ActionName("EditIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualEmailAddress(string contact2email_gid)
        {
            MdlContactEmail values = new MdlContactEmail();
            objDaAgrMstApplicationEdit.DaEditIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateIndividualEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Email Address
        [ActionName("DeleteIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualEmailAddress(string contact2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaAgrMstApplicationEdit.DaDeleteIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaAgrMstApplicationEdit.DaGetIndividualAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaAgrMstApplicationEdit.DaGetIndividualAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Address Details 

        [ActionName("EditIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualAddress(string contact2address_gid)
        {
            MdlContactAddress values = new MdlContactAddress();
            objDaAgrMstApplicationEdit.DaEditIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualAddress(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaAgrMstApplicationEdit.DaDeleteIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetIndividualProofTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objDaAgrMstApplicationEdit.DaGetIndividualProofTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objDaAgrMstApplicationEdit.DaGetIndividualProofList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("IndividualDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage IndividualDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaAgrMstApplicationEdit.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objDaAgrMstApplicationEdit.DaGetIndividualDocTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocList(string contact_gid)
        {
            MdlContactDocument values = new MdlContactDocument();
            objDaAgrMstApplicationEdit.DaGetIndividualDocList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objDaAgrMstApplicationEdit.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividual")]
        [HttpGet]
        public HttpResponseMessage EditIndividual(string contact_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objDaAgrMstApplicationEdit.DaEditIndividual(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Update 
        [ActionName("UpdateIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividual(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualSummary(string application_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objDaAgrMstApplicationEdit.DaGetIndividualSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBasicDetailsSummary")]
        [HttpGet]
        public HttpResponseMessage GetBasicDetailsSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrMstApplicationEdit.DaGetBasicDetailsSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Contact Person Mobile No

        [ActionName("PostAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaGetAppMobileNoTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaGetAppMobileNoList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditAppMobileNo(string application2contact_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaEditAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Mobile No----------//
        [ActionName("DeleteAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteAppMobileNo(string application2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaDeleteAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Contact Person Email Address

        [ActionName("PostAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaAgrMstApplicationEdit.DaGetAppEmailAddressTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaAgrMstApplicationEdit.DaGetAppEmailAddressList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Email Address

        [ActionName("EditAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditAppEmailAddress(string application2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaAgrMstApplicationEdit.DaEditAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAppEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaAgrMstApplicationEdit.DaDeleteAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code
        [ActionName("PostAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Genetic Code
        [ActionName("GetAppGeneticCodeTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppGeneticCodeTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaAgrMstApplicationEdit.DaGetAppGeneticCodeTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Genetic Code
        [ActionName("GetAppGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetAppGeneticCodeList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaAgrMstApplicationEdit.DaGetAppGeneticCodeList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditAppGeneticCode(string application2geneticcode_gid)
        {
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaAgrMstApplicationEdit.DaEditAppGeneticCode(application2geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("DeleteAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteAppGeneticCode(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaAgrMstApplicationEdit.DaDeleteAppGeneticCode(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrMstApplicationEdit.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Clear 
        [ActionName("GetApplicationBasicDetailsTempClear")]
        [HttpGet]
        public HttpResponseMessage GetApplicationBasicDetailsTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrMstApplicationEdit.DaGetApplicationBasicDetailsTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditProceed")]
        [HttpGet]
        public HttpResponseMessage EditProceed(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaEditProceed(application_gid, values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppProceed")]
        [HttpPost]
        public HttpResponseMessage EditAppProceed(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaEditAppProceed(getsessionvalues.employee_gid,getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppReProceed")]
        [HttpPost]
        public HttpResponseMessage EditAppReProceed(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaEditAppReProceed(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Edit Save
        [ActionName("SaveInstitutionEditDtl")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionEditDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveInstitutionEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Institution Edit - Add Save
        [ActionName("SaveInstitutionDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionDtlAdd(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveInstitutionDtlAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Institution Edit Submit
        [ActionName("SubmitInstitutionEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionEditDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitInstitutionEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Institution Edit -add Submit
        [ActionName("SubmitInstitutionDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtlAdd(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitInstitutionDtlAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit Save
        [ActionName("SaveIndividualEditDtl")]
        [HttpPost]
        public HttpResponseMessage SaveIndividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveIndividualEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit - Add Save
        [ActionName("SaveIndividualDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveInSaveIndividualDtlAdddividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveIndividualDtlAdd( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit Submit
        [ActionName("SubmitIndividualEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitIndividualEditDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitIndividualDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualDtlAdd(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitIndividualDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SocialAndTradeCapitalsubmit")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSave(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSocialAndTradeCapitalsubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CICUploadIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocList(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstApplicationEdit.DaCICUploadIndividualDocList(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadInstitutionDocList")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocList(string institution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objDaAgrMstApplicationEdit.DaCICUploadInstitutionDocList(institution2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadIndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocDelete(string tmpcicdocument_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstApplicationEdit.DaCICUploadIndividualDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadInstitutionDocDelete")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocDelete(string tmpcicdocument_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstApplicationEdit.DaCICUploadInstitutionDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditProductcharges")]
        [HttpGet]
        public HttpResponseMessage GetEditProductcharges(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrMstApplicationEdit.DaGetEditProductcharges(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditLoanDtl")]
        [HttpPost]
        public HttpResponseMessage PostEditLoanDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostEditLoanDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaveEditProductCharges")]
        [HttpPost]
        public HttpResponseMessage SaveEditProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveEditProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitEditProductCharges")]
        [HttpPost]
        public HttpResponseMessage SubmitEditProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitEditProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostEditHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostEditHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostEditCollateral")]
        [HttpPost]
        public HttpResponseMessage PostEditCollateral(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaPostEditCollateral(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Group Address List

        [ActionName("GroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressList(string group_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaAgrMstApplicationEdit.DaGroupAddressList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Address Temp List

        [ActionName("GroupAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaAgrMstApplicationEdit.DaGroupAddressTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Bank List

        [ActionName("GroupBankList")]
        [HttpGet]
        public HttpResponseMessage GroupBankList(string group_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaAgrMstApplicationEdit.DaGroupBankList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Bank Temp List

        [ActionName("GroupBankTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupBankTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaAgrMstApplicationEdit.DaGroupBankTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentList(string group_gid)
        {
            MdlGroupDocument values = new MdlGroupDocument();
            objDaAgrMstApplicationEdit.DaGroupDocumentList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objDaAgrMstApplicationEdit.DaGroupDocumentTmpList(group_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditGroup")]
        [HttpGet]
        public HttpResponseMessage EditGroup(string group_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaAgrMstApplicationEdit.DaEditGroup(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Group Edit Save
        [ActionName("SaveGroupDtlEdit")]
        [HttpPost]
        public HttpResponseMessage SaveGroupEditDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveGroupDtlEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Group Edit - Add Save
        [ActionName("SaveGroupDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveGrouplDtlAdd(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSaveGroupDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Group Edit Submit
        [ActionName("SubmitGroupEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitGroupDtlEdit(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitGroupDtlEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGroupDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitGroupDtlAdd(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaSubmitGroupDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaAgrMstApplicationEdit.DaGetGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Details Update

        [ActionName("UpdateGroupDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdateGroupDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //PANForm60
        [ActionName("GetPANForm60TempList")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60TempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaAgrMstApplicationEdit.DaGetPANForm60TempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaAgrMstApplicationEdit.DaGetPANForm60List(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objDaAgrMstApplicationEdit.DaContactPANAbsenceReasonList(contact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("UpdatePANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage UpdatePANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplicationEdit.DaUpdatePANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string contact_gid)
        {
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objDaAgrMstApplicationEdit.DaEditPANAbsenceReasonList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objDaAgrMstApplicationEdit.DaGetAppProductList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAppProductDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteAppProductDtl(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaAgrMstApplicationEdit.DaDeleteAppProductDtl(application2product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objDaAgrMstApplicationEdit.DaGetAppProductTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}