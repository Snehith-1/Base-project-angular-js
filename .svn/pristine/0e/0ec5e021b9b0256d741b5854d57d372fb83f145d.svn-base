using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Collections.Generic;

/// <summary>
/// (It's used for Credit Assignment and My Application pages)AppCreditUnderWriting Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstAppCreditUnderWriting")]
    [Authorize]

    public class MstAppCreditUnderWritingController : ApiController
    {
        DaMstAppCreditUnderWriting objMstAppCreditUnderWriting = new DaMstAppCreditUnderWriting();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Genetic Code
        [ActionName("PostGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostGeneticCode(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Genetic Code
        [ActionName("GetGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetGeneticCodeList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objMstAppCreditUnderWriting.DaGetGeneticCodeList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditGeneticCode(string creditgeneticcode_gid)
        {
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objMstAppCreditUnderWriting.DaEditGeneticCode(creditgeneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateGeneticCode(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteGeneticCode(string creditgeneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objMstAppCreditUnderWriting.DaDeleteGeneticCode(creditgeneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Save Social/Trade Capital
        [ActionName("SocialAndTradeCapitalSave")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSave(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaSocialAndTradeCapitalSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage GetSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objMstAppCreditUnderWriting.DaGetSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Social/Trade Capital
        [ActionName("EditSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage EditSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objMstAppCreditUnderWriting.DaEditSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Submit Social/Trade Capital
        [ActionName("SocialAndTradeCapitalSubmit")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSubmit(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaSocialAndTradeCapitalSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Social/Trade Capital
        [ActionName("SocialAndTradeCapitalUpdate")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalUpdate(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaSocialAndTradeCapitalUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Save PSL Data Flagging
        [ActionName("PSLDataFlaggingSave")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingSave(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPSLDataFlaggingSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage GetPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objMstAppCreditUnderWriting.DaGetPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit PSL Data Flagging
        [ActionName("EditPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage EditPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objMstAppCreditUnderWriting.DaEditPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Submit PSL Data Flagging
        [ActionName("PSLDataFlaggingSubmit")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingSubmit(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPSLDataFlaggingSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update PSL Data Flagging
        [ActionName("PSLDataFlaggingUpdate")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingUpdate(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPSLDataFlaggingUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get PSL Drop Down List
        [ActionName("GetPSLDropdownList")]
        [HttpGet]
        public HttpResponseMessage GetPSLDropdownList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaGetPSLDropdownList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Existing Bank Facility
        [ActionName("PostExistingBankFacility")]
        [HttpPost]
        public HttpResponseMessage PostExistingBankFacility(MdlMstCUWExistingBankFacility values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostExistingBankFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Existing Bank Facility
        [ActionName("GetExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage GetExistingBankFacility(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objMstAppCreditUnderWriting.DaGetExistingBankFacility(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Existing Bank Facility
        [ActionName("EditExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage EditExistingBankFacility(string existingbankfacility_gid)
        {
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objMstAppCreditUnderWriting.DaEditExistingBankFacility(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Existing Bank Facility
        [ActionName("UpdateExistingBankFacility")]
        [HttpPost]
        public HttpResponseMessage UpdateExistingBankFacility(MdlMstCUWExistingBankFacility values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateExistingBankFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Existing Bank Facility
        [ActionName("DeleteExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage DeleteExistingBankFacility(string existingbankfacility_gid)
        {
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objMstAppCreditUnderWriting.DaDeleteExistingBankFacility(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Repayment Track
        [ActionName("PostRepaymentTrack")]
        [HttpPost]
        public HttpResponseMessage PostRepaymentTrack(MdlMstCUWRepaymentTrack values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostRepaymentTrack(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Repayment Track
        [ActionName("GetRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage GetRepaymentTrack(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objMstAppCreditUnderWriting.DaGetRepaymentTrack(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Repayment Track
        [ActionName("EditRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage EditRepaymentTrack(string creditrepaymentdtl_gid)
        {
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objMstAppCreditUnderWriting.DaEditRepaymentTrack(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Repayment Track
        [ActionName("UpdateRepaymentTrack")]
        [HttpPost]
        public HttpResponseMessage UpdateRepaymentTrack(MdlMstCUWRepaymentTrack values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateRepaymentTrack(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Repayment Track
        [ActionName("DeleteRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage DeleteRepaymentTrack(string creditrepaymentdtl_gid)
        {
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objMstAppCreditUnderWriting.DaDeleteRepaymentTrack(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Lender Type List

        [ActionName("LenderTypeList")]
        [HttpGet]
        public HttpResponseMessage LenderTypeList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaLenderTypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Account Classification List

        [ActionName("CreditAccountClassificationList")]
        [HttpGet]
        public HttpResponseMessage CreditAccountClassificationList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaCreditAccountClassificationList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Instalment Frequency List

        [ActionName("CreditInstalmentFrequencyList")]
        [HttpGet]
        public HttpResponseMessage CreditInstalmentFrequencyList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaCreditInstalmentFrequencyList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Funded Type Indicator List

        [ActionName("FundedTypeIndicatorList")]
        [HttpGet]
        public HttpResponseMessage FundedTypeIndicatorList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaFundedTypeIndicatorList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Underwriting Facility Type List

        [ActionName("CreditUnderwritingFacilityTypeList")]
        [HttpGet]
        public HttpResponseMessage CreditUnderwritingFacilityTypeList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaCreditUnderwritingFacilityTypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Bank Name List

        [ActionName("BankNameList")]
        [HttpGet]
        public HttpResponseMessage BankNameList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaBankNameList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Client Details List

        [ActionName("ClientDetailsList")]
        [HttpGet]
        public HttpResponseMessage ClientDetailsList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objMstAppCreditUnderWriting.DaClientDetailsList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        // Supplier Add
        [ActionName("PostCreditSupplier")]
        [HttpPost]
        public HttpResponseMessage PostCreditSupplier(MdlMstSupplier values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCreditSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get supplier Info
        [ActionName("GetCreditSupplierList")]
        [HttpGet]
        public HttpResponseMessage GetCreditSupplierList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplier values = new MdlMstSupplier();
            objMstAppCreditUnderWriting.DaGetCreditSupplierList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditSupplier")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditSupplier(string creditsupplier_gid)
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objMstAppCreditUnderWriting.DaEditGetCreditSupplier(creditsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditSupplier")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditSupplier(MdlMstSupplier values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateCreditSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditSupplier")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditSupplier(string creditsupplier_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplier values = new MdlMstSupplier();
            objMstAppCreditUnderWriting.DaDeleteCreditSupplier(creditsupplier_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Buyer
        [ActionName("PostCreditBuyer")]
        [HttpPost]
        public HttpResponseMessage PostCreditBuyer(MdlMstCreditBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCreditBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetCreditBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCreditBuyerList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objMstAppCreditUnderWriting.DaGetCreditBuyerList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBuyer")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditBuyer(string creditbuyer_gid)
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objMstAppCreditUnderWriting.DaEditGetCreditBuyer(creditbuyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditBuyer")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditBuyer(MdlMstCreditBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateCreditBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditBuyer")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditBuyer(string creditbuyer_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objMstAppCreditUnderWriting.DaDeleteCreditBuyer(creditbuyer_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Credit Observations
        [ActionName("PostCreditObservation")]
        [HttpPost]
        public HttpResponseMessage PostCreditObservation(MdlMstCreditObservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCreditObservation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetCreditObservationList")]
        [HttpGet]
        public HttpResponseMessage GetCreditObservationList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objMstAppCreditUnderWriting.DaGetCreditObservationList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditObservation")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditObservation(string creditobservation_gid)
        {
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objMstAppCreditUnderWriting.DaEditGetCreditObservation(creditobservation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditObservation")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditObservation(MdlMstCreditObservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateCreditObservation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditObservation")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditObservation(string creditobservation_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objMstAppCreditUnderWriting.DaDeleteCreditObservation(creditobservation_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditBank")]
        [HttpPost]
        public HttpResponseMessage PostCreditBank(MdlCreditBankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCreditBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCrediBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetCrediBankAccDtl(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objMstAppCreditUnderWriting.DaGetCrediBankAccDtl(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Supplier Master
        [ActionName("GetSupplierList")]
        [HttpGet]
        public HttpResponseMessage GetSupplierList()
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objMstAppCreditUnderWriting.DaGetSupplierList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Buyer Master
        [ActionName("GetCreBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCreBuyerList()
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objMstAppCreditUnderWriting.DaGetCreBuyerList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Credit Policy Compliance
        [ActionName("GetCrepolicy")]
        [HttpGet]
        public HttpResponseMessage GetCrepolicy()
        {
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objMstAppCreditUnderWriting.DaGetCrepolicy(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Account Type
        [ActionName("GetCreditAccountType")]
        [HttpGet]
        public HttpResponseMessage GetCreditAccountType()
        {
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objMstAppCreditUnderWriting.DaGetCreditAccountType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("chequeleafdocumentUpload")]
        [HttpPost]
        public HttpResponseMessage chequeleafdocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            credituploaddocument documentname = new credituploaddocument();
            objMstAppCreditUnderWriting.DachequeleafdocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("DeleteCreditcheque")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditcheque(string creditbankdtl2cheque_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            credituploaddocument values = new credituploaddocument();
            objMstAppCreditUnderWriting.DaDeleteCreditcheque(creditbankdtl2cheque_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Underwritting View

        [ActionName("GetCrediBankAccList")]
        [HttpGet]
        public HttpResponseMessage GetCrediBankAccList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objMstAppCreditUnderWriting.DaGetCrediBankAccList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditOperationsView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOperationsView(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objMstAppCreditUnderWriting.DaGetCreditOperationsView(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditBuyerTextData")]
        [HttpGet]
        public HttpResponseMessage GetCreditBuyerTextData(string creditbuyer_gid)
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objMstAppCreditUnderWriting.DaGetCreditBuyerTextData(creditbuyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditSupplierTextData")]
        [HttpGet]
        public HttpResponseMessage GetCreditSupplierTextData(string creditsupplier_gid)
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objMstAppCreditUnderWriting.DaGetCreditSupplierTextData(creditsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditBankDocumentUpload")]
        [HttpGet]
        public HttpResponseMessage GetCreditBankDocumentUpload(string creditbankdtl_gid)
        {
            credituploaddocument values = new credituploaddocument();
            objMstAppCreditUnderWriting.DaGetCreditBankDocumentUpload(creditbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //New 
        [ActionName("ChequeTmpClear")]
        [HttpGet]
        public HttpResponseMessage ChequeTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstAppCreditUnderWriting.DaChequeTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditBankAccDtl(string creditbankdtl_gid)
        {
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objMstAppCreditUnderWriting.DaEditGetCreditBankAccDtl(creditbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditBankAccDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditBankAccDtl(MdlCreditBankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.UpdateCreditBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletecreditBankAcc")]
        [HttpGet]
        public HttpResponseMessage DeletecreditBankAcc(string creditbankdtl_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objMstAppCreditUnderWriting.DaDeletecreditBankAcc(creditbankdtl_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditExistingBankDtlRemarks")]
        [HttpGet]
        public HttpResponseMessage GetCreditExistingBankDtlRemarks(string existingbankfacility_gid)
        {
            MdlMstExistingRemarks values = new MdlMstExistingRemarks();
            objMstAppCreditUnderWriting.DaGetCreditExistingBankDtlRemarks(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditRepaymentDtlRemarks")]
        [HttpGet]
        public HttpResponseMessage GetCreditRepaymentDtlRemarks(string creditrepaymentdtl_gid)
        {
            MdlMstRepaymentRemarks values = new MdlMstRepaymentRemarks();
            objMstAppCreditUnderWriting.DaGetCreditRepaymentDtlRemarks(creditrepaymentdtl_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionEditDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objMstAppCreditUnderWriting.DaInstitutionEditDocumentTmpList(institution_gid, getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionEditDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
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
            objMstAppCreditUnderWriting.DaInstitutionEditForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objMstAppCreditUnderWriting.DaInstitutionEditForm60TmpList(institution_gid, getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionEditForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Institution GST List

        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string institution_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objMstAppCreditUnderWriting.DaInstitutionGSTList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Mobile Number List

        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string institution_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaInstitutionMobileNoList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Email Address List

        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string institution_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaInstitutionEmailAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Address List

        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string institution_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstAppCreditUnderWriting.DaInstitutionAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution License List

        [ActionName("InstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseList(string institution_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstAppCreditUnderWriting.DaInstitutionLicenseList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionDocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objMstAppCreditUnderWriting.DaInstitutionDocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionForm60DocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm60DocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objMstAppCreditUnderWriting.DaInstitutionForm60DocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Details Edit

        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string institution_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objMstAppCreditUnderWriting.DaInstitutionDetailsEdit(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Institution Details Update

        [ActionName("UpdateInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateInstitutionDtl(values, getsessionvalues.employee_gid);
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
            objMstAppCreditUnderWriting.DaInstitutionGSTTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionMobileNoTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionEmailAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstAppCreditUnderWriting.DaInstitutionLicenseTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGSTList")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGSTList(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution GST

        [ActionName("EditInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionGST(string institution2branch_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objMstAppCreditUnderWriting.DaEditInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution GST

        [ActionName("UpdateInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution GST

        [ActionName("DeleteInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionGST(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objMstAppCreditUnderWriting.DaDeleteInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Mobile No

        [ActionName("PostInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Mobile No

        [ActionName("EditInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionMobileNo(string institution2mobileno_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaEditInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Institution Mobile No

        [ActionName("UpdateInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Mobile No

        [ActionName("DeleteInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionMobileNo(string institution2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaDeleteInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Email Address

        [ActionName("PostInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Email Address

        [ActionName("EditInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionEmailAddress(string institution2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaEditInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Email Address

        [ActionName("DeleteInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionEmailAddress(string institution2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaDeleteInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Address Details  

        [ActionName("PostInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Address Details 

        [ActionName("EditInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionAddressDetail(string institution2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstAppCreditUnderWriting.DaEditInstitutionAddressDetail(institution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateInstitutionAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Address Details 

        [ActionName("DeleteInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionAddressDetail(string institution2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstAppCreditUnderWriting.DaDeleteInstitutionAddressDetail(institution2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution License Details

        [ActionName("PostInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionLicenseDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution License Details 

        [ActionName("EditInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstAppCreditUnderWriting.DaEditInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution License Details 

        [ActionName("UpdateInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateInstitutionLicenseDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution License Details 

        [ActionName("DeleteInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstAppCreditUnderWriting.DaDeleteInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Temp Clear 

        [ActionName("GetIntitutionTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIntitutionTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstAppCreditUnderWriting.DaGetIntitutionTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Mobile Number Add 
        [ActionName("PostIndividualMobileNumber")]
        [HttpPost]
        public HttpResponseMessage PostIndividualMobileNumber(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostIndividualMobileNumber(getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetIndividualMobileNoTempList(contact_gid, getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetIndividualMobileNoList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Mobile No

        [ActionName("EditIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIndividualMobileNo(string contact2mobileno_gid)
        {
            MdlContactMobileNo values = new MdlContactMobileNo();
            objMstAppCreditUnderWriting.DaEditIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Individual Mobile No

        [ActionName("UpdateIndividualMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualMobileNo(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateIndividualMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Mobile No
        [ActionName("DeleteIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualMobileNo(string contact2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objMstAppCreditUnderWriting.DaDeleteIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Add 
        [ActionName("PostIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostIndividualEmailAddress(getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetIndividualEmailAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetIndividualEmailAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Email Address

        [ActionName("EditIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualEmailAddress(string contact2email_gid)
        {
            MdlContactEmail values = new MdlContactEmail();
            objMstAppCreditUnderWriting.DaEditIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateIndividualEmailAddress(getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaDeleteIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstAppCreditUnderWriting.DaGetIndividualAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstAppCreditUnderWriting.DaGetIndividualAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Address Details 

        [ActionName("EditIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualAddress(string contact2address_gid)
        {
            MdlContactAddress values = new MdlContactAddress();
            objMstAppCreditUnderWriting.DaEditIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualAddress(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstAppCreditUnderWriting.DaDeleteIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualProofDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage IndividualProofDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstAppCreditUnderWriting.DaIndividualProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualProofTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstAppCreditUnderWriting.DaGetIndividualProofTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstAppCreditUnderWriting.DaGetIndividualProofList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualProofDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualProofDelete(string contact2idproof_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstAppCreditUnderWriting.DaIndividualProofDelete(contact2idproof_gid, values);
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
            objMstAppCreditUnderWriting.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objMstAppCreditUnderWriting.DaGetIndividualDocTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocList(string contact_gid)
        {
            MdlContactDocument values = new MdlContactDocument();
            objMstAppCreditUnderWriting.DaGetIndividualDocList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objMstAppCreditUnderWriting.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividual")]
        [HttpGet]
        public HttpResponseMessage EditIndividual(string contact_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objMstAppCreditUnderWriting.DaEditIndividual(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Update 
        [ActionName("UpdateIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividual(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Temp Clear 
        [ActionName("GetIndividualTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIndividualTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstAppCreditUnderWriting.GetIndividualTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // General Contact Person Mobile No

        [ActionName("PostAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaGetAppMobileNoTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaGetAppMobileNoList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditAppMobileNo(string application2contact_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaEditAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Mobile No
        [ActionName("DeleteAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteAppMobileNo(string application2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstAppCreditUnderWriting.DaDeleteAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Person Email Address

        [ActionName("PostAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaGetAppEmailAddressTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaGetAppEmailAddressList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Email Address

        [ActionName("EditAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditAppEmailAddress(string application2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaEditAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Email Address

        [ActionName("UpdateAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAppEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstAppCreditUnderWriting.DaDeleteAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code
        [ActionName("PostAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostAppGeneticCode(getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetAppGeneticCodeTempList(application_gid, getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetAppGeneticCodeList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditAppGeneticCode(string application2geneticcode_gid)
        {
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objMstAppCreditUnderWriting.DaEditAppGeneticCode(application2geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteAppGeneticCode(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objMstAppCreditUnderWriting.DaDeleteAppGeneticCode(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstAppCreditUnderWriting.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetApplicationBasicDetailsTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Add Address Details  

        [ActionName("PostGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostGroupAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Group Address Details 

        [ActionName("EditGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupAddressDetail(string group2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstAppCreditUnderWriting.DaEditGroupAddressDetail(group2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Group Address Details 

        [ActionName("UpdateGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateGroupAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Group Address Details 

        [ActionName("DeleteGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupAddressDetail(string group2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstAppCreditUnderWriting.DaDeleteGroupAddressDetail(group2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Group Bank Details  

        [ActionName("PostGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostGroupBankDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Group Bank Details 

        [ActionName("EditGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupBankDetail(string group2bank_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstAppCreditUnderWriting.DaEditGroupBankDetail(group2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Group Address Details 

        [ActionName("UpdateGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateGroupBankDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Group Bank Details 

        [ActionName("DeleteGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupBankDetail(string group2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstAppCreditUnderWriting.DaDeleteGroupBankDetail(group2bank_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage GroupDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstAppCreditUnderWriting.DaGroupDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GroupDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentDelete(string group2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objMstAppCreditUnderWriting.DaGroupDocumentDelete(group2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Address List

        [ActionName("GroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressList(string group_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstAppCreditUnderWriting.DaGroupAddressList(group_gid, values);
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
            objMstAppCreditUnderWriting.DaGroupAddressTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Bank List

        [ActionName("GroupBankList")]
        [HttpGet]
        public HttpResponseMessage GroupBankList(string group_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstAppCreditUnderWriting.DaGroupBankList(group_gid, values);
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
            objMstAppCreditUnderWriting.DaGroupBankTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentList(string group_gid)
        {
            MdlGroupDocument values = new MdlGroupDocument();
            objMstAppCreditUnderWriting.DaGroupDocumentList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objMstAppCreditUnderWriting.DaGroupDocumentTmpList(group_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGroup")]
        [HttpGet]
        public HttpResponseMessage EditGroup(string group_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objMstAppCreditUnderWriting.DaEditGroup(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Details Update

        [ActionName("UpdateGroupDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateGroupDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Clear 

        [ActionName("GetGroupTempClear")]
        [HttpGet]
        public HttpResponseMessage GetGroupTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstAppCreditUnderWriting.DaGetGroupTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation 
        [ActionName("PostHypoDoc")]
        [HttpPost]
        public HttpResponseMessage PostHypoDoc()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objMstAppCreditUnderWriting.DaPostHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("deleteHypoDoc")]
        [HttpGet]
        public HttpResponseMessage deleteHypoDoc(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objMstAppCreditUnderWriting.DadeleteHypoDoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        // Hypothecation Document Temp List

        [ActionName("HypothecationDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage HypothecationDocumentTempList(string application2hypothecation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objMstAppCreditUnderWriting.DaHypothecationDocumentTempList(getsessionvalues.employee_gid, application2hypothecation_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("PostHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteHypothecation")]
        [HttpGet]
        public HttpResponseMessage DeleteHypothecation(string application2hypothecation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objMstAppCreditUnderWriting.DaDeleteHypothecation(application2hypothecation_gid, values, getsessionvalues.employee_gid);
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
            objMstAppCreditUnderWriting.DaHypothecationDetailsEdit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation Details Update

        [ActionName("HypothecationDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage HypothecationDetailsUpdate(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaHypothecationDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ServicechargeEdit")]
        [HttpGet]
        public HttpResponseMessage ServicechargeEdit(string application2servicecharge_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductCharges values = new MdlProductCharges();
            objMstAppCreditUnderWriting.DaServicechargeEdit(application2servicecharge_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ServicechargeUpdate")]
        [HttpPost]
        public HttpResponseMessage ServicechargeUpdate(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaServicechargeUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUnderwrite")]
        [HttpPost]
        public HttpResponseMessage PostUnderwrite(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostUnderwrite(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CAMocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CAMocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlMstCC values = new MdlMstCC();
            objMstAppCreditUnderWriting.DaCAMocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CAMdoc_delete")]
        [HttpGet]
        public HttpResponseMessage getcamdoc_delete(string application2camdoc_gid, string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objMstAppCreditUnderWriting.Dagetcamdoc_delete(application2camdoc_gid,application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("GetCAM")]
        [HttpGet]
        public HttpResponseMessage GetCAM(string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objMstAppCreditUnderWriting.DaGetCAM(application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("ImportExcelBankStatement")]
        [HttpPost]
        public HttpResponseMessage ImportExcelIndividual()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportExcelBankStatement(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBankStatementList")]
        [HttpGet]
        public HttpResponseMessage GetBankStatementList(string credit_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBankStatement values = new MdlMstCUWBankStatement();
            objMstAppCreditUnderWriting.DaGetBankStatementList(credit_gid, application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BankStatementExportExcel")]
        [HttpPost]
        public HttpResponseMessage BankStatementExportExcel(BankStatementExportExcel objBankStatementExportExcel)
        {
            objMstAppCreditUnderWriting.DaBankStatementTemplateExport(objBankStatementExportExcel);
            return Request.CreateResponse(HttpStatusCode.OK, objBankStatementExportExcel);
        }
        // P & L Template 1

        [ActionName("ImportProfitLoss")]
        [HttpPost]
        public HttpResponseMessage ImportProfitLoss()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportProfitLoss(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetProfitLoss")]
        [HttpGet]
        public HttpResponseMessage GetProfitLoss( string application_gid, string credit_gid, string template_name )
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProfitLoss values = new MdlMstProfitLoss();
            objMstAppCreditUnderWriting.DaGetProfitLoss( application_gid, credit_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //FSA Summary

        [ActionName("GetFSASummary")]
        [HttpGet]
        public HttpResponseMessage GetFSASUmmary(string credit_gid, string application_gid)
        {
            MdlMstFSASummary values = new MdlMstFSASummary();
            objMstAppCreditUnderWriting.DaGetFSASUmmary(credit_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Balance Sheet Template 1

        [ActionName("ImportExcelBalanceSheetTemplate1")]
        [HttpPost]
        public HttpResponseMessage ImportExcelBalanceSheetTemplate1()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportExcelBalanceSheetTemplate1(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBalanceSheetTemplate1List")]
        [HttpGet]
        public HttpResponseMessage GetBalanceSheetTemplate1List(string credit_gid, string application_gid, string template_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBalancesheettemplate1 values = new MdlMstCUWBalancesheettemplate1();
            objMstAppCreditUnderWriting.DaGetBalanceSheetTemplate1List(credit_gid, application_gid, template_type, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Summary Template

        [ActionName("ImportExcelSummaryTemplate1")]
        [HttpPost]
        public HttpResponseMessage ImportExcelSummaryTemplate1()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportExcelSummaryTemplate1(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetSummaryTemplate1View")]
        [HttpGet]
        public HttpResponseMessage GetSummaryTemplate1View(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSummaryTemplate1View values = new MdlSummaryTemplate1View();
            objMstAppCreditUnderWriting.DaGetSummaryTemplate1View(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Balance Sheet Template 2

        [ActionName("ImportExcelBalanceSheetTemplate2")]
        [HttpPost]
        public HttpResponseMessage ImportExcelBalanceSheetTemplate2()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportExcelBalanceSheetTemplate2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBalanceSheetTemplate2List")]
        [HttpGet]
        public HttpResponseMessage GetBalanceSheetTemplate2List(string credit_gid, string application_gid, string template_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBalancesheettemplate2 values = new MdlMstCUWBalancesheettemplate2();
            objMstAppCreditUnderWriting.DaGetBalanceSheetTemplate2List(credit_gid, application_gid, template_type, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //P & L-Template 2

        [ActionName("ImportProfitLossTemp2")]
        [HttpPost]
        public HttpResponseMessage ImportProfitLossTemp2()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportProfitLossTemp2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ImportExcelSummaryTemplate2")]
        [HttpPost]
        public HttpResponseMessage ImportExcelSummaryTemplate2()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstAppCreditUnderWriting.DaImportExcelSummaryTemplate2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetSummaryTemplate2View")]
        [HttpGet]
        public HttpResponseMessage GetSummaryTemplate2View(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSummaryTemplate2View values = new MdlSummaryTemplate2View();
            objMstAppCreditUnderWriting.DaGetSummaryTemplate2View(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
 

        [ActionName("GetApplicationCreditAprovalinfo")]
        [HttpGet]
        public HttpResponseMessage GetApplicationCreditAprovalinfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstcreditApprovalInfo values = new MdlMstcreditApprovalInfo();
            objMstAppCreditUnderWriting.DaGetApplicationCreditAprovalinfo(application_gid, values);
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
            objMstAppCreditUnderWriting.DaGetPANForm60TempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objMstAppCreditUnderWriting.DaGetPANForm60List(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objMstAppCreditUnderWriting.DaContactPANAbsenceReasonList(contact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("UpdatePANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage UpdatePANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdatePANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string contact_gid)
        {
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objMstAppCreditUnderWriting.DaEditPANAbsenceReasonList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANForm60DocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PANForm60DocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstAppCreditUnderWriting.DaPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("PANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage PANForm60Delete(string contact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objMstAppCreditUnderWriting.DaPANForm60Delete(contact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANReasonsCheck")]
        [HttpGet]
        public HttpResponseMessage PANReasonsCheck()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objMstAppCreditUnderWriting.DaPANReasonsCheck(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProfitLossTemp2List")]
        [HttpGet]
        public HttpResponseMessage GetProfitLossTemp2List(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProfitLosstemp2 values = new MdlMstProfitLosstemp2();
            objMstAppCreditUnderWriting.DaGetProfitLossTemp2List(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductDetailAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDetailAdd(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostProductDetailAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstAppCreditUnderWriting.DaGetAppProductTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAppProductDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteAppProductDtl(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailAdd values = new MdlMstProductDetailAdd();
            objMstAppCreditUnderWriting.DaDeleteAppProductDtl(application2product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstAppCreditUnderWriting.DaGetAppProductList(application_gid, getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstAppCreditUnderWriting.DaGetInstitutionLivestockList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Institution Equipment Holding
        [ActionName("PostInstitutionEquipmentHolding")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEquipmentHolding(MdlMstEquipmentHolding values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionEquipmentHolding(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }//Delete Institution Equipment Holding
        [ActionName("DeleteInstitutionEquipmentHolding")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionEquipmentHolding(string institution2equipment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstAppCreditUnderWriting.DaDeleteInstitutionEquipmentHolding(institution2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //View Institution Equipment Holding
        [ActionName("GetEquipmentHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetEquipmentHoldingView(string institution2equipment_gid)
        {
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstAppCreditUnderWriting.DaGetEquipmentHoldingView(institution2equipment_gid, values);
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
            objMstAppCreditUnderWriting.DaGetEditInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Institution Livestock Holding
        [ActionName("PostInstitutionLivestock")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLivestock(MdlMstLivestock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostInstitutionLivestock(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Institution Livestock Holding
        [ActionName("DeleteInstitutionLivestock")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionLivestock(string institution2livestock_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstAppCreditUnderWriting.DaDeleteInstitutionLivestock(institution2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Institution Livestock Holding
        [ActionName("GetLivestockHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetLivestockHoldingView(string institution2livestock_gid)
        {
            MdlMstLivestock values = new MdlMstLivestock();
            objMstAppCreditUnderWriting.DaGetLivestockHoldingView(institution2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
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
            objMstAppCreditUnderWriting.DaGetEditInstitutionLivestockList(getsessionvalues.employee_gid, institution_gid, values);
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
            objMstAppCreditUnderWriting.DaGetContactEquipmentHoldingList(getsessionvalues.employee_gid, contact_gid, values);
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
            objMstAppCreditUnderWriting.DaGetEditContactEquipmentHoldingList(getsessionvalues.employee_gid, contact_gid, values);
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
            objMstAppCreditUnderWriting.DaGetContactLivestockList(getsessionvalues.employee_gid, contact_gid, values);
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
            objMstAppCreditUnderWriting.DaGetEditContactLivestockList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Contact Equipment Holding
        [ActionName("PostContactEquipmentHolding")]
        [HttpPost]
        public HttpResponseMessage PostContactEquipmentHolding(MdlMstEquipmentHolding values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostContactEquipmentHolding(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Contact Equipment Holding
        [ActionName("DeleteContactEquipmentHolding")]
        [HttpGet]
        public HttpResponseMessage DeleteContactEquipmentHolding(string contact2equipment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstAppCreditUnderWriting.DaDeleteContactEquipmentHolding(contact2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //View Contact Equipment Holding
        [ActionName("GetContactEquipmentHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetContactEquipmentHoldingView(string contact2equipment_gid)
        {
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstAppCreditUnderWriting.DaGetContactEquipmentHoldingView(contact2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Contact Livestock Holding
        [ActionName("PostContactLivestock")]
        [HttpPost]
        public HttpResponseMessage PostContactLivestock(MdlMstLivestock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostContactLivestock(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Delete Contact Livestock Holding
        [ActionName("DeleteContactLivestock")]
        [HttpGet]
        public HttpResponseMessage DeleteContactLivestock(string contact2livestock_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstAppCreditUnderWriting.DaDeleteContactLivestock(contact2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Contact Livestock Holding
        [ActionName("GetContactLivestockHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetContactLivestockHoldingView(string contact2livestock_gid)
        {
            MdlMstLivestock values = new MdlMstLivestock();
            objMstAppCreditUnderWriting.DaGetContactLivestockHoldingView(contact2livestock_gid, values);
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
            objMstAppCreditUnderWriting.DaGetGroupEquipmentHoldingList(getsessionvalues.employee_gid, group_gid, values);
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
            objMstAppCreditUnderWriting.DaGetGroupLivestockList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Group Equipment Holding
        [ActionName("PostGroupEquipmentHolding")]
        [HttpPost]
        public HttpResponseMessage PostGroupEquipmentHolding(MdlMstEquipmentHolding values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostGroupEquipmentHolding(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Group Equipment Holding
        [ActionName("DeleteGroupEquipmentHolding")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupEquipmentHolding(string group2equipment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstAppCreditUnderWriting.DaDeleteGroupEquipmentHolding(group2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //View Group Equipment Holding
        [ActionName("GetGroupEquipmentHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetGroupEquipmentHoldingView(string group2equipment_gid)
        {
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objMstAppCreditUnderWriting.DaGetGroupEquipmentHoldingView(group2equipment_gid, values);
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
            objMstAppCreditUnderWriting.DaGetEditGroupEquipmentHoldingList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Group Livestock Holding
        [ActionName("PostGroupLivestock")]
        [HttpPost]
        public HttpResponseMessage PostGroupLivestock(MdlMstLivestock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostGroupLivestock(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Group Livestock Holding
        [ActionName("DeleteGroupLivestock")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupLivestock(string group2livestock_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objMstAppCreditUnderWriting.DaDeleteGroupLivestock(group2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Group Livestock Holding
        [ActionName("GetGroupLivestockHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetGroupLivestockHoldingView(string group2livestock_gid)
        {
            MdlMstLivestock values = new MdlMstLivestock();
            objMstAppCreditUnderWriting.DaGetGroupLivestockHoldingView(group2livestock_gid, values);
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
            objMstAppCreditUnderWriting.DaGetEditGroupLivestockList(getsessionvalues.employee_gid, group_gid, values);
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
            objMstAppCreditUnderWriting.DaGetInstitutionReceivableList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Account Type
        [ActionName("GetGuaranteeProgramType")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeProgramType()
        {
            MdlGuaranteeProgramType values = new MdlGuaranteeProgramType();
            objMstAppCreditUnderWriting.DaGetGuaranteeProgramType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostGuaranteeDtlAdd")]
        [HttpPost]
        public HttpResponseMessage PostGuaranteeDtlAdd(MdlGuaranteeDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostGuaranteeDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GuaranteeDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage GuaranteeDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            GuaranteeDocumentUpload documentname = new GuaranteeDocumentUpload();
            objMstAppCreditUnderWriting.DaGuaranteeDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        //New 
        [ActionName("GuaranteeDocTmpClear")]
        [HttpGet]
        public HttpResponseMessage GuaranteeDocTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objMstAppCreditUnderWriting.DaGuaranteeDocTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionGuaranteeDtl")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionGuaranteeDtl(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objMstAppCreditUnderWriting.DaGetInstitutionGuaranteeDtl(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGuaranteeDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteGuaranteeDtl(string creditguaranteedtl_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objMstAppCreditUnderWriting.DaDeleteGuaranteeDtl(creditguaranteedtl_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGuaranteeDtlDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteGuaranteeDtlDocument(string creditguaranteedtldocument_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            GuaranteeDocumentUpload values = new GuaranteeDocumentUpload();
            objMstAppCreditUnderWriting.DaDeleteGuaranteeDtlDocument(creditguaranteedtldocument_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGuaranteeRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeRemarksView(string creditguaranteedtl_gid)
        {
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objMstAppCreditUnderWriting.DaGetGuaranteeRemarksView(creditguaranteedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGuaranteeDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeDocDtl(string creditguaranteedtl_gid)
        {

            GuaranteeDocumentUpload values = new GuaranteeDocumentUpload();
            objMstAppCreditUnderWriting.DaGetGuaranteeDocDtl(creditguaranteedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingDtlSummary")]
        [HttpGet]
        public HttpResponseMessage GetColendingDtlSummary(string application_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetColendingDtlSummary(getsessionvalues.employee_gid, application_gid, credit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetColendingRemarksView(string portfolio_gid)
        {
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetColendingRemarksView(portfolio_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetColendingDocDtl(string portfolio_gid)
        {

            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetColendingDocDtl(portfolio_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostColendingDtlAdd")]
        [HttpPost]
        public HttpResponseMessage PostColendingDtlAdd(MdlColendingDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostColendingDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ColendingDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage ColendingDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlColendingDtl documentname = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaColendingDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteColendingDtlDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteColendingDtlDocument(string creditcolendingdtl_gid, string creditcolendingdtldocument_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaDeleteColendingDtlDocument(creditcolendingdtl_gid, creditcolendingdtldocument_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingDtlsView")]
        [HttpGet]
        public HttpResponseMessage GetColendingDtlsView(string creditcolendingdtl_gid, string application_gid)
        {
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetColendingDtlsView(creditcolendingdtl_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ColendingDtlDocumentView")]
        [HttpGet]
        public HttpResponseMessage ColendingDtlDocumentView(string creditcolendingdtl_gid, string credit_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaColendingDtlDocumentView(getsessionvalues.employee_gid,creditcolendingdtl_gid, credit_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ColendingDocTmpClear")]
        [HttpGet]
        public HttpResponseMessage ColendingDocTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaColendingDocTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditTaggedPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditTaggedPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditTaggedPendingDtl values = new MdlCreditTaggedPendingDtl();
            objMstAppCreditUnderWriting.DaGetCreditTaggedPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditVerificationDocTmpClear")]
        [HttpGet]
        public HttpResponseMessage CreditVerificationDocTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditVerificationDtl values = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaCreditVerificationDocTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCreditVerificationDtl")]
        [HttpPost]
        public HttpResponseMessage PostCreditVerificationDtl(MdlCreditVerificationDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCreditVerificationDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditVerificationDocUpload")]
        [HttpPost]
        public HttpResponseMessage CreditVerificationDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlCreditVerificationDtl documentname = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaCreditVerificationDocUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteCreditVerificationDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditVerificationDoc(string creditverificationdoc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditVerificationDtl values = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaDeleteCreditVerificationDoc(creditverificationdoc_gid,  values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditVerificationDtlView")]
        [HttpGet]
        public HttpResponseMessage GetCreditVerificationDtlView(string creditverification_gid, string application_gid)
        {
            MdlCreditVerificationDtl values = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCreditVerificationDtlView(creditverification_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditVerificationDocView")]
        [HttpGet]
        public HttpResponseMessage CreditVerificationDocView(string creditverification_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditVerificationDtl values = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaCreditVerificationDocView(getsessionvalues.employee_gid, creditverification_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingVerificationSummary")]
        [HttpGet]
        public HttpResponseMessage GetColendingVerificationSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditVerificationDtl values = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaGetColendingVerificationSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostConfirmCreditVerification")]
        [HttpPost]
        public HttpResponseMessage PostConfirmCreditVerification(MdlCreditVerificationDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostConfirmCreditVerification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditTaggedCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditTaggedCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditTaggedCompletedDtl values = new MdlCreditTaggedCompletedDtl();
            objMstAppCreditUnderWriting.DaGetCreditTaggedCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCApprovedVerificationSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovedVerificationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlccapprovedverifyDtl values = new MdlccapprovedverifyDtl();
            objMstAppCreditUnderWriting.DaGetCCApprovedVerificationSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CCVerificationDocTmpClear")]
        [HttpGet]
        public HttpResponseMessage CCVerificationDocTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaCCVerificationDocTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCVerificationDtlView")]
        [HttpGet]
        public HttpResponseMessage GetCCVerificationDtlView(string ccapprovedverification_gid, string application_gid)
        {
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCCVerificationDtlView(ccapprovedverification_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CCVerificationDocView")]
        [HttpGet]
        public HttpResponseMessage CCVerificationDocView(string ccapprovedverification_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaCCVerificationDocView(getsessionvalues.employee_gid, ccapprovedverification_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCCApprovedVerificationDtl")]
        [HttpPost]
        public HttpResponseMessage PostCCApprovedVerificationDtl(MdlCCVerificationDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCCApprovedVerificationDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CCVerificationDocUpload")]
        [HttpPost]
        public HttpResponseMessage CCVerificationDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlCCVerificationDtl documentname = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaCCVerificationDocUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteCCVerificationDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteCCVerificationDoc(string ccapprovedverificationdoc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaDeleteCCVerificationDoc(ccapprovedverificationdoc_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostConfirmCCVerification")]
        [HttpPost]
        public HttpResponseMessage PostConfirmCCVerification(MdlCCVerificationDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostConfirmCCVerification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCColendingVerificationSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCColendingVerificationSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCCColendingVerificationSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFinalApprovedVerifiedSummary")]
        [HttpGet]
        public HttpResponseMessage GetFinalApprovedVerifiedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaGetFinalApprovedVerifiedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ColendingVerificationCount")]
        [HttpGet]
        public HttpResponseMessage ColendingVerificationCount()
        {
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaColendingVerificationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostColendingApplicabilityStatusAdd")]
        [HttpPost]
        public HttpResponseMessage PostColendingApplicabilityStatusAdd(MdlColendingDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostColendingApplicabilityStatusAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingApplicabilityStatus")]
        [HttpGet]
        public HttpResponseMessage GetColendingApplicabilityStatus(string application_gid, string credit_gid)
        {
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetColendingApplicabilityStatus(application_gid, credit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCApprovedApplicabilityView")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovedApplicabilityView(string application_gid)
        {
            MdlCCVerificationDtl values = new MdlCCVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCCApprovedApplicabilityView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditApplicabilityView")]
        [HttpGet]
        public HttpResponseMessage GetCreditApplicabilityView(string application_gid)
        {
            MdlCreditVerificationDtl values = new MdlCreditVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCreditApplicabilityView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyApplCreditApplicabilityView")]
        [HttpGet]
        public HttpResponseMessage GetMyApplCreditApplicabilityView(string application_gid)
        {
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetMyApplCreditApplicabilityView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CadVerificationDocTmpClear")]
        [HttpGet]
        public HttpResponseMessage CadVerificationDocTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaCadVerificationDocTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadVerificationDtlView")]
        [HttpGet]
        public HttpResponseMessage GetCadVerificationDtlView(string cadapprovedverification_gid, string application_gid)
        {
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCadVerificationDtlView(cadapprovedverification_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CadVerificationDocView")]
        [HttpGet]
        public HttpResponseMessage CadVerificationDocView(string cadapprovedverification_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaCadVerificationDocView(getsessionvalues.employee_gid, cadapprovedverification_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCadApprovedVerificationDtl")]
        [HttpPost]
        public HttpResponseMessage PostCadApprovedVerificationDtl(MdlCadVerificationDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostCadApprovedVerificationDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CadVerificationDocUpload")]
        [HttpPost]
        public HttpResponseMessage CadVerificationDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlCadVerificationDtl documentname = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaCadVerificationDocUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DeleteCadVerificationDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteCadVerificationDoc(string cadapprovedverification_gid, string cadapprovedverificationdoc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaDeleteCadVerificationDoc(cadapprovedverification_gid, cadapprovedverificationdoc_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadColendingVerificationSummary")]
        [HttpGet]
        public HttpResponseMessage GetCadColendingVerificationSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCadColendingVerificationSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostConfirmCadVerification")]
        [HttpPost]
        public HttpResponseMessage PostConfirmCadVerification(MdlCadVerificationDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaPostConfirmCadVerification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadApprovedApplicabilityView")]
        [HttpGet]
        public HttpResponseMessage GetCadApprovedApplicabilityView(string application_gid)
        {
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCadApprovedApplicabilityView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadApprovedColendingHistory")]
        [HttpGet]
        public HttpResponseMessage GetCadApprovedColendingHistory(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCadApprovedColendingHistory(application_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadColendingRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetCadColendingRemarksView(string cadapprovedverificationlog_gid)
        {
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCadColendingRemarksView(cadapprovedverificationlog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCcApprovedColendingHistory")]
        [HttpGet]
        public HttpResponseMessage GetCcApprovedColendingHistory(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCcApprovedColendingHistory(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCcColendingRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetCcColendingRemarksView(string ccapprovedverificationlog_gid)
        {
            MdlCadVerificationDtl values = new MdlCadVerificationDtl();
            objMstAppCreditUnderWriting.DaGetCcColendingRemarksView(ccapprovedverificationlog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyApplApplicabilityView")]
        [HttpGet]
        public HttpResponseMessage GetMyApplApplicabilityView(string application_gid, string credit_gid)
        {
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetMyApplApplicabilityView(application_gid, credit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLimitManagementDtlView")]
        [HttpGet]
        public HttpResponseMessage GetLimitManagementDtlView(string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLimitManagementDtl values = new MdlLimitManagementDtl();
            objMstAppCreditUnderWriting.DaGetLimitManagementDtlView(customer_urn, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadUrnCustomerDtlView")]
        [HttpGet]
        public HttpResponseMessage GetCadUrnCustomerDtlView(string customer_urn)
        {
            MdlLimitManagementDtl values = new MdlLimitManagementDtl();
            objMstAppCreditUnderWriting.DaGetCadUrnCustomerDtlView(customer_urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Transcation

        [ActionName("GetColendingBasicView")]
        [HttpGet]
        public HttpResponseMessage GetColendingBasicView(string colendingprogram_gid, string applicant_type, string application_gid)
        {
            MdlTrnColending values = new MdlTrnColending();
            objMstAppCreditUnderWriting.DaGetColendingBasicView(colendingprogram_gid, applicant_type, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetColendingScorecarddtl")]
        [HttpGet]
        public HttpResponseMessage GetColendingScorecarddtl(string colendingprogram_gid, string applicant_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingGroupQuestiondtl values = new MdlColendingGroupQuestiondtl();
            objMstAppCreditUnderWriting.DaGetColendingScorecarddtl(colendingprogram_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingScorecardViewdtl")]
        [HttpGet]
        public HttpResponseMessage GetColendingScorecardViewdtl(string colendingprogram_gid, string applicant_type, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingGroupQuestiondtl values = new MdlColendingGroupQuestiondtl();
            objMstAppCreditUnderWriting.DaGetColendingScorecardViewdtl(colendingprogram_gid, applicant_type, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetColendingQuestionScore")]
        [HttpPost]
        public HttpResponseMessage GetColendingQuestionScore(MdlColendingGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            List<MdlColendingGroupScore> values = new List<MdlColendingGroupScore>();
            objMstAppCreditUnderWriting.DaGetColendingQuestionScore(values, data, data.colendingquestionrule_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaveScoreCard")]
        [HttpPost]
        public HttpResponseMessage SaveScoreCard(MdlColendingGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaSaveScoreCard(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("SubmitScoreCard")]
        [HttpPost]
        public HttpResponseMessage SubmitScoreCard(MdlColendingGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAppCreditUnderWriting.DaSubmitScoreCard(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [ActionName("MyAppGetLimitManagementDtlView")]
        [HttpGet]
        public HttpResponseMessage MyAppGetLimitManagementDtlView(string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLimitManagementDtl values = new MdlLimitManagementDtl();
            objMstAppCreditUnderWriting.DaMyAppGetLimitManagementDtlView(customer_urn, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppCadUrnCustomerDtlView")]
        [HttpGet]
        public HttpResponseMessage GetMyAppCadUrnCustomerDtlView(string customer_urn)
        {
            MdlLimitManagementDtl values = new MdlLimitManagementDtl();
            objMstAppCreditUnderWriting.DaGetMyAppCadUrnCustomerDtlView(customer_urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVerifyColendingDtlSummary")]
        [HttpGet]
        public HttpResponseMessage GetVerifyColendingDtlSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlColendingDtl values = new MdlColendingDtl();
            objMstAppCreditUnderWriting.DaGetVerifyColendingDtlSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}