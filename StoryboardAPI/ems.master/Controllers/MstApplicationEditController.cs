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
/// (It's used for Application Creation Edit in Samfin)ApplicationEdit Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstApplicationEdit")]
    [Authorize]

    public class MstApplicationEditController : ApiController
    {
        DaMstApplicationEdit objMstApplicationEdit = new DaMstApplicationEdit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Edit Social And Trade Capital

        [ActionName("SocialAndTradeEdit")]
        [HttpGet]
        public HttpResponseMessage SocialAndTradeEdit(string application_gid)
        {
            MdlMstApplicationEdit values = new MdlMstApplicationEdit();
            objMstApplicationEdit.DaSocialAndTradeEdit(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SocialAndTradeCapitalUpdate")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalUpdate(MdlMstApplicationEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSocialAndTradeCapitalUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualEdit")]
        [HttpGet]
        public HttpResponseMessage CICIndividualEdit(string contact2bureau_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationEdit.DaCICIndividualEdit(contact2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualDocEdit")]
        [HttpGet]
        public HttpResponseMessage CICIndividualDocEdit(string contact_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaCICIndividualDocEdit(contact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CICInstitutionEdit")]
        [HttpGet]
        public HttpResponseMessage CICInstitutionEdit(string institution2bureau_gid)
        {
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationEdit.DaCICInstitutionEdit(institution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICInstitutionDocEdit")]
        [HttpGet]
        public HttpResponseMessage CICInstitutionDocEdit(string institution_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaCICInstitutionDocEdit(institution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualUpdate")]
        [HttpPost]
        public HttpResponseMessage CICIndividualUpdate(MdlCICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaCICIndividualUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICInstitutionUpdate")]
        [HttpPost]
        public HttpResponseMessage CICInstitutionUpdate(MdlCICInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaCICInstitutionUpdate(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaCICIndividualDocumentUploadEdit(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("DaTempCICUploadDocDelete")]
        [HttpGet]
        public HttpResponseMessage DaTempCICUploadDocDelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationEdit.DaTempCICUploadDocDelete(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSocialTradeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSocialTradeSummary(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationEdit.DaGetSocialTradeSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCICEditIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetCICIndividualSummary(string application_gid)
        {

            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationEdit.DaGetCICIndividualSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCICEditInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetCICInstitutionSummary(string application_gid)
        {

            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationEdit.DaGetCICInstitutionSummary(application_gid, values);
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
            objMstApplicationEdit.DaInstitutionEditDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaInstitutionEditDocumentTmpList(institution_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaInstitutionEditDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaInstitutionEditForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaInstitutionEditForm60TmpList(institution_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaInstitutionEditForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Institution GST List

        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string institution_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objMstApplicationEdit.DaInstitutionGSTList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Mobile Number List

        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string institution_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationEdit.DaInstitutionMobileNoList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Email Address List

        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string institution_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationEdit.DaInstitutionEmailAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Address List

        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string institution_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationEdit.DaInstitutionAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution License List

        [ActionName("InstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseList(string institution_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstApplicationEdit.DaInstitutionLicenseList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionDocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objMstApplicationEdit.DaInstitutionDocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionForm60DocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm60DocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objMstApplicationEdit.DaInstitutionForm60DocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Details Edit

        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string institution_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objMstApplicationEdit.DaInstitutionDetailsEdit(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Institution Details Update

        [ActionName("UpdateInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateInstitutionDtl(values, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaInstitutionGSTTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstApplicationEdit.DaInstitutionMobileNoTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstApplicationEdit.DaInstitutionEmailAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstApplicationEdit.DaInstitutionAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstApplicationEdit.DaInstitutionLicenseTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product & Charges Edit

        [ActionName("GetProductChargesEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesEdit(string application_gid)
        {
            MdlProductCharges values = new MdlProductCharges();
            objMstApplicationEdit.DaGetProductChargesEdit(application_gid, values);
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
            objMstApplicationEdit.DaLoanDetailList(application_gid, values, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaLoanTempDetailList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Details Edit

        [ActionName("LoanDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage LoanDetailsEdit(string application2loan_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objMstApplicationEdit.DaLoanDetailsEdit(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Details Update

        [ActionName("LoanDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage LoanDetailsUpdate(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaLoanDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Loan Detail

        [ActionName("DeleteLoanDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteLoanDetail(string application2loan_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objMstApplicationEdit.DaDeleteLoanDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        // Buyer Details List

        [ActionName("BuyerDetailsList")]
        [HttpGet]
        public HttpResponseMessage BuyerDetailsList(string application2loan_gid)
        {
            MdlMstBuyer values = new MdlMstBuyer();
            objMstApplicationEdit.DaBuyerDetailsList(application2loan_gid, values);
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
            objMstApplicationEdit.DaBuyerTempDetailsList(getsessionvalues.employee_gid, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Buyer Details Edit

        [ActionName("BuyerDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage BuyerDetailsEdit(string application2buyer_gid)
        {
            MdlMstBuyer values = new MdlMstBuyer();
            objMstApplicationEdit.DaBuyerDetailsEdit(application2buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Buyer Details Update

        [ActionName("BuyerDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage BuyerDetailsUpdate(MdlMstBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DBuyerDetailsUpdate(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaDeleteBuyerDetails(application2buyer_gid, values, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaCollateralDetailsList(getsessionvalues.employee_gid,application_gid, values);
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
            objMstApplicationEdit.DaCollateralTempDetailsList(getsessionvalues.employee_gid, application_gid, values);
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
            objMstApplicationEdit.DaCollateralDetailsEdit(application2collateral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Collateral Details Update

        [ActionName("CollateralDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage CollateralDetailsUpdate(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaCollateralDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Collateral Details

        [ActionName("DeleteCollateralDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteCollateralDetails(string application2collateral_gid)
        {
            MdlMstCollatertal values = new MdlMstCollatertal();
            objMstApplicationEdit.DaDeleteCollateralDetails(application2collateral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation Details List

        [ActionName("HypothecationDetailsList")]
        [HttpGet]
        public HttpResponseMessage HypothecationDetailsList(string application_gid)
        {
            MdlMstHypothecation values = new MdlMstHypothecation();
            objMstApplicationEdit.DaHypothecationDetailsList(application_gid, values);
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
            objMstApplicationEdit.DaHypothecationTempDetailsList(getsessionvalues.employee_gid, application_gid, values);
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
            objMstApplicationEdit.DaHypothecationDetailsEdit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditHypothecation")]
        [HttpGet]
        public HttpResponseMessage GetEditHypothecation(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objMstApplicationEdit.DaGetEditHypothecation(application_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        // Hypothecation Details Update

        [ActionName("HypothecationDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage HypothecationDetailsUpdate(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaHypothecationDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateOverallLimit")]
        [HttpPost]
        public HttpResponseMessage UpdateOverallLimit(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateOverallLimit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostLoanEditDtl")]
        [HttpPost]
        public HttpResponseMessage PostLoanEditDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostLoanEditDtl(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetEditLimit(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLoanLimit")]
        [HttpPost]
        public HttpResponseMessage GetEditLoanLimit(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           // MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationEdit.GetEditLoanLimit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditproduct")]
        [HttpGet]
        public HttpResponseMessage GetEditproduct(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlList values = new MdlList();
            objMstApplicationEdit.DaGetEditproduct(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditServiceCharges")]
        [HttpPost]
        public HttpResponseMessage PostEditServiceCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostEditServiceCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteHypothecationDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteHypothecationDetails(string application2hypothecation_gid)
        {
            MdlMstHypothecation values = new MdlMstHypothecation();
            objMstApplicationEdit.DaDeleteHypothecationDetails(application2hypothecation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLoanDtl")]
        [HttpGet]
        public HttpResponseMessage GetEditLoanDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objMstApplicationEdit.DaGetEditLoanDtl(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update Product Charges

        [ActionName("UpdateProductCharges")]
        [HttpPost]
        public HttpResponseMessage UpdateProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateProductCharges(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetProductChargesTempClear(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaCollateralDocumentTempList(getsessionvalues.employee_gid, application2loan_gid, objfilename);
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
            objMstApplicationEdit.DaCollateralDocumentList(application2loan_gid, objfilename);
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
            objMstApplicationEdit.DaHypothecationDocumentTempList(getsessionvalues.employee_gid, application2hypothecation_gid, objfilename);
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
            objMstApplicationEdit.DaEditcollateraldocument(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaEditHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objMstApplicationEdit.DaPostIndividualMobileNumber(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetIndividualMobileNoTempList(contact_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetIndividualMobileNoList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Mobile No

        [ActionName("EditIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIndividualMobileNo(string contact2mobileno_gid)
        {
            MdlContactMobileNo values = new MdlContactMobileNo();
            objMstApplicationEdit.DaEditIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        // Update Individual Mobile No

        [ActionName("UpdateIndividualMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualMobileNo(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateIndividualMobileNo(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaDeleteIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Add 
        [ActionName("PostIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostIndividualEmailAddress(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetIndividualEmailAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetIndividualEmailAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Email Address

        [ActionName("EditIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualEmailAddress(string contact2email_gid)
        {
            MdlContactEmail values = new MdlContactEmail();
            objMstApplicationEdit.DaEditIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateIndividualEmailAddress(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaDeleteIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationEdit.DaGetIndividualAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationEdit.DaGetIndividualAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Address Details 

        [ActionName("EditIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualAddress(string contact2address_gid)
        {
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationEdit.DaEditIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualAddress(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationEdit.DaDeleteIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetIndividualProofTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstApplicationEdit.DaGetIndividualProofTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstApplicationEdit.DaGetIndividualProofList(contact_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objMstApplicationEdit.DaGetIndividualDocTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocList(string contact_gid)
        {
            MdlContactDocument values = new MdlContactDocument();
            objMstApplicationEdit.DaGetIndividualDocList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objMstApplicationEdit.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividual")]
        [HttpGet]
        public HttpResponseMessage EditIndividual(string contact_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objMstApplicationEdit.DaEditIndividual(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Update 
        [ActionName("UpdateIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividual(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualSummary(string application_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objMstApplicationEdit.DaGetIndividualSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBasicDetailsSummary")]
        [HttpGet]
        public HttpResponseMessage GetBasicDetailsSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationEdit.DaGetBasicDetailsSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Contact Person Mobile No

        [ActionName("PostAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationEdit.DaGetAppMobileNoTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationEdit.DaGetAppMobileNoList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditAppMobileNo(string application2contact_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationEdit.DaEditAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateAppMobileNo(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaDeleteAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Contact Person Email Address

        [ActionName("PostAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationEdit.DaGetAppEmailAddressTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationEdit.DaGetAppEmailAddressList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Email Address

        [ActionName("EditAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditAppEmailAddress(string application2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationEdit.DaEditAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAppEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationEdit.DaDeleteAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code
        [ActionName("PostAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostAppGeneticCode(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetAppGeneticCodeTempList(application_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetAppGeneticCodeList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditAppGeneticCode(string application2geneticcode_gid)
        {
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objMstApplicationEdit.DaEditAppGeneticCode(application2geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("DeleteAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteAppGeneticCode(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objMstApplicationEdit.DaDeleteAppGeneticCode(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppBasic")]
        [HttpGet]
        public HttpResponseMessage EditAppBasic(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationEdit.DaEditAppBasic(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationEdit.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetApplicationBasicDetailsTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditProceed")]
        [HttpGet]
        public HttpResponseMessage EditProceed(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaEditProceed(application_gid, values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppProceed")]
        [HttpPost]
        public HttpResponseMessage EditAppProceed(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaEditAppProceed(getsessionvalues.employee_gid,getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppReProceed")]
        [HttpPost]
        public HttpResponseMessage EditAppReProceed(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaEditAppReProceed(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Edit Save
        [ActionName("SaveInstitutionEditDtl")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionEditDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveInstitutionEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Institution Edit - Add Save
        [ActionName("SaveInstitutionDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionDtlAdd(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveInstitutionDtlAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Institution Edit Submit
        [ActionName("SubmitInstitutionEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionEditDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitInstitutionEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Institution Edit -add Submit
        [ActionName("SubmitInstitutionDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtlAdd(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitInstitutionDtlAdd(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit Save
        [ActionName("SaveIndividualEditDtl")]
        [HttpPost]
        public HttpResponseMessage SaveIndividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveIndividualEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit - Add Save
        [ActionName("SaveIndividualDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveInSaveIndividualDtlAdddividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveIndividualDtlAdd( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit Submit
        [ActionName("SubmitIndividualEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitIndividualEditDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitIndividualDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualDtlAdd(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitIndividualDtlAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SocialAndTradeCapitalsubmit")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSave(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSocialAndTradeCapitalsubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CICUploadIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocList(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationEdit.DaCICUploadIndividualDocList(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadInstitutionDocList")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocList(string institution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationEdit.DaCICUploadInstitutionDocList(institution2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadIndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocDelete(string tmpcicdocument_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationEdit.DaCICUploadIndividualDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadInstitutionDocDelete")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocDelete(string tmpcicdocument_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationEdit.DaCICUploadInstitutionDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditProductcharges")]
        [HttpGet]
        public HttpResponseMessage GetEditProductcharges(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationEdit.DaGetEditProductcharges(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditLoanDtl")]
        [HttpPost]
        public HttpResponseMessage PostEditLoanDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostEditLoanDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaveEditProductCharges")]
        [HttpPost]
        public HttpResponseMessage SaveEditProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveEditProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitEditProductCharges")]
        [HttpPost]
        public HttpResponseMessage SubmitEditProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitEditProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostEditHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostEditHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostEditCollateral")]
        [HttpPost]
        public HttpResponseMessage PostEditCollateral(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaPostEditCollateral(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Group Address List

        [ActionName("GroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressList(string group_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationEdit.DaGroupAddressList(group_gid, values);
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
            objMstApplicationEdit.DaGroupAddressTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Bank List

        [ActionName("GroupBankList")]
        [HttpGet]
        public HttpResponseMessage GroupBankList(string group_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstApplicationEdit.DaGroupBankList(group_gid, values);
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
            objMstApplicationEdit.DaGroupBankTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentList(string group_gid)
        {
            MdlGroupDocument values = new MdlGroupDocument();
            objMstApplicationEdit.DaGroupDocumentList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objMstApplicationEdit.DaGroupDocumentTmpList(group_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditGroup")]
        [HttpGet]
        public HttpResponseMessage EditGroup(string group_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objMstApplicationEdit.DaEditGroup(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Group Edit Save
        [ActionName("SaveGroupDtlEdit")]
        [HttpPost]
        public HttpResponseMessage SaveGroupEditDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveGroupDtlEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Group Edit - Add Save
        [ActionName("SaveGroupDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveGrouplDtlAdd(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSaveGroupDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Group Edit Submit
        [ActionName("SubmitGroupEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitGroupDtlEdit(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitGroupDtlEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGroupDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitGroupDtlAdd(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaSubmitGroupDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objMstApplicationEdit.DaGetGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Details Update

        [ActionName("UpdateGroupDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateGroupDtl(getsessionvalues.employee_gid, values);
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
            objMstApplicationEdit.DaGetPANForm60TempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objMstApplicationEdit.DaGetPANForm60List(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objMstApplicationEdit.DaContactPANAbsenceReasonList(contact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("UpdatePANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage UpdatePANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdatePANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string contact_gid)
        {
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objMstApplicationEdit.DaEditPANAbsenceReasonList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstApplicationEdit.DaGetAppProductList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAppProductDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteAppProductDtl(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailAdd values = new MdlMstProductDetailAdd();
            objMstApplicationEdit.DaDeleteAppProductDtl(application2product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstApplicationEdit.DaGetAppProductTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationEdit.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Institution Equipment Holding
        [ActionName("GetInstitutionEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEquipmentHoldingList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstApplicationEdit.DaGetInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Institution Livestock Holding
        [ActionName("GetInstitutionLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionLivestockList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstApplicationEdit.DaGetInstitutionLivestockList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Institution Equipment Holding
        [ActionName("GetEditInstitutionEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetEditInstitutionEquipmentHoldingList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstApplicationEdit.DaGetEditInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Institution Livestock Holding
        [ActionName("GetEditInstitutionLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetEditInstitutionLivestockList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstApplicationEdit.DaGetEditInstitutionLivestockList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Contact Equipment Holding
        [ActionName("GetContactEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetContactEquipmentHoldingList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstApplicationEdit.DaGetContactEquipmentHoldingList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Contact Equipment Holding
        [ActionName("GetEditContactEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetEditContactEquipmentHoldingList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstApplicationEdit.DaGetEditContactEquipmentHoldingList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Contact Livestock Holding
        [ActionName("GetContactLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetContactLivestockList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstApplicationEdit.DaGetContactLivestockList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Contact Livestock Holding
        [ActionName("GetEditContactLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetEditContactLivestockList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstApplicationEdit.DaGetEditContactLivestockList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Group Equipment Holding
        [ActionName("GetGroupEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetGroupEquipmentHoldingList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstApplicationEdit.DaGetGroupEquipmentHoldingList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Group Equipment Holding
        [ActionName("GetEditGroupEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetEditGroupEquipmentHoldingList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstApplicationEdit.DaGetEditGroupEquipmentHoldingList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Group Livestock Holding
        [ActionName("GetGroupLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetGroupLivestockList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstApplicationEdit.DaGetGroupLivestockList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Group Livestock Holding
        [ActionName("GetEditGroupLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetEditGroupLivestockList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstApplicationEdit.DaGetEditGroupLivestockList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Edit Institution Receivable List
        [ActionName("GetEditInstitutionReceivableList")]
        [HttpGet]
        public HttpResponseMessage GetEditInstitutionReceivableList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstReceivable values = new MdlMstReceivable();
            objMstApplicationEdit.DaGetEditInstitutionReceivableList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Institution Receivable List
        [ActionName("GetInstitutionReceivableList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionReceivableList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstReceivable values = new MdlMstReceivable();
            objMstApplicationEdit.DaGetInstitutionReceivableList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Deleteinstitution")]
        [HttpGet]
        public HttpResponseMessage Deleteinstitution(string institution_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationEdit.DaDeleteinstitution(institution_gid, application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}