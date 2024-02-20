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
    /// This Controllers will provide access to add, edit, view datas in credit stages.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>


    [RoutePrefix("api/AgrTrnAppCreditUnderWriting")]
    [Authorize]

    public class AgrTrnAppCreditUnderWritingController : ApiController
    {
        DaAgrTrnAppCreditUnderWriting objAgrTrnAppCreditUnderWriting = new DaAgrTrnAppCreditUnderWriting();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        // Genetic Code
        [ActionName("PostGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostGeneticCode(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostGeneticCode(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetGeneticCodeList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditGeneticCode(string creditgeneticcode_gid)
        {
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objAgrTrnAppCreditUnderWriting.DaEditGeneticCode(creditgeneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateGeneticCode(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteGeneticCode(string creditgeneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objAgrTrnAppCreditUnderWriting.DaDeleteGeneticCode(creditgeneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Save Social/Trade Capital
        [ActionName("SocialAndTradeCapitalSave")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSave(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaSocialAndTradeCapitalSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage GetSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objAgrTrnAppCreditUnderWriting.DaGetSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Social/Trade Capital
        [ActionName("EditSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage EditSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objAgrTrnAppCreditUnderWriting.DaEditSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Submit Social/Trade Capital
        [ActionName("SocialAndTradeCapitalSubmit")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSubmit(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaSocialAndTradeCapitalSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Social/Trade Capital
        [ActionName("SocialAndTradeCapitalUpdate")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalUpdate(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaSocialAndTradeCapitalUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Save PSL Data Flagging
        [ActionName("PSLDataFlaggingSave")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingSave(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPSLDataFlaggingSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage GetPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objAgrTrnAppCreditUnderWriting.DaGetPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit PSL Data Flagging
        [ActionName("EditPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage EditPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objAgrTrnAppCreditUnderWriting.DaEditPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Submit PSL Data Flagging
        [ActionName("PSLDataFlaggingSubmit")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingSubmit(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPSLDataFlaggingSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update PSL Data Flagging
        [ActionName("PSLDataFlaggingUpdate")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingUpdate(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPSLDataFlaggingUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get PSL Drop Down List
        [ActionName("GetPSLDropdownList")]
        [HttpGet]
        public HttpResponseMessage GetPSLDropdownList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaGetPSLDropdownList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Existing Bank Facility
        [ActionName("PostExistingBankFacility")]
        [HttpPost]
        public HttpResponseMessage PostExistingBankFacility(MdlMstCUWExistingBankFacility values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostExistingBankFacility(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetExistingBankFacility(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Existing Bank Facility
        [ActionName("EditExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage EditExistingBankFacility(string existingbankfacility_gid)
        {
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objAgrTrnAppCreditUnderWriting.DaEditExistingBankFacility(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Existing Bank Facility
        [ActionName("UpdateExistingBankFacility")]
        [HttpPost]
        public HttpResponseMessage UpdateExistingBankFacility(MdlMstCUWExistingBankFacility values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateExistingBankFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Existing Bank Facility
        [ActionName("DeleteExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage DeleteExistingBankFacility(string existingbankfacility_gid)
        {
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objAgrTrnAppCreditUnderWriting.DaDeleteExistingBankFacility(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Repayment Track
        [ActionName("PostRepaymentTrack")]
        [HttpPost]
        public HttpResponseMessage PostRepaymentTrack(MdlMstCUWRepaymentTrack values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostRepaymentTrack(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetRepaymentTrack(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Repayment Track
        [ActionName("EditRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage EditRepaymentTrack(string creditrepaymentdtl_gid)
        {
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objAgrTrnAppCreditUnderWriting.DaEditRepaymentTrack(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Repayment Track
        [ActionName("UpdateRepaymentTrack")]
        [HttpPost]
        public HttpResponseMessage UpdateRepaymentTrack(MdlMstCUWRepaymentTrack values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateRepaymentTrack(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Repayment Track
        [ActionName("DeleteRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage DeleteRepaymentTrack(string creditrepaymentdtl_gid)
        {
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objAgrTrnAppCreditUnderWriting.DaDeleteRepaymentTrack(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Lender Type List

        [ActionName("LenderTypeList")]
        [HttpGet]
        public HttpResponseMessage LenderTypeList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaLenderTypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Account Classification List

        [ActionName("CreditAccountClassificationList")]
        [HttpGet]
        public HttpResponseMessage CreditAccountClassificationList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaCreditAccountClassificationList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Instalment Frequency List

        [ActionName("CreditInstalmentFrequencyList")]
        [HttpGet]
        public HttpResponseMessage CreditInstalmentFrequencyList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaCreditInstalmentFrequencyList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Funded Type Indicator List

        [ActionName("FundedTypeIndicatorList")]
        [HttpGet]
        public HttpResponseMessage FundedTypeIndicatorList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaFundedTypeIndicatorList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Underwriting Facility Type List

        [ActionName("CreditUnderwritingFacilityTypeList")]
        [HttpGet]
        public HttpResponseMessage CreditUnderwritingFacilityTypeList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaCreditUnderwritingFacilityTypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Bank Name List

        [ActionName("BankNameList")]
        [HttpGet]
        public HttpResponseMessage BankNameList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaBankNameList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Client Details List

        [ActionName("ClientDetailsList")]
        [HttpGet]
        public HttpResponseMessage ClientDetailsList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objAgrTrnAppCreditUnderWriting.DaClientDetailsList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Supplier Add
        [ActionName("PostCreditSupplier")]
        [HttpPost]
        public HttpResponseMessage PostCreditSupplier(MdlMstSupplier values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostCreditSupplier(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetCreditSupplierList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditSupplier")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditSupplier(string creditsupplier_gid)
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objAgrTrnAppCreditUnderWriting.DaEditGetCreditSupplier(creditsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditSupplier")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditSupplier(MdlMstSupplier values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateCreditSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditSupplier")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditSupplier(string creditsupplier_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplier values = new MdlMstSupplier();
            objAgrTrnAppCreditUnderWriting.DaDeleteCreditSupplier(creditsupplier_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Buyer
        [ActionName("PostCreditBuyer")]
        [HttpPost]
        public HttpResponseMessage PostCreditBuyer(MdlMstCreditBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostCreditBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCreditBuyerList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objAgrTrnAppCreditUnderWriting.DaGetCreditBuyerList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBuyer")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditBuyer(string creditbuyer_gid)
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objAgrTrnAppCreditUnderWriting.DaEditGetCreditBuyer(creditbuyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditBuyer")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditBuyer(MdlMstCreditBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateCreditBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditBuyer")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditBuyer(string creditbuyer_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objAgrTrnAppCreditUnderWriting.DaDeleteCreditBuyer(creditbuyer_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Credit Observations
        [ActionName("PostCreditObservation")]
        [HttpPost]
        public HttpResponseMessage PostCreditObservation(MdlMstCreditObservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostCreditObservation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditObservationList")]
        [HttpGet]
        public HttpResponseMessage GetCreditObservationList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objAgrTrnAppCreditUnderWriting.DaGetCreditObservationList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditObservation")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditObservation(string creditobservation_gid)
        {
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objAgrTrnAppCreditUnderWriting.DaEditGetCreditObservation(creditobservation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditObservation")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditObservation(MdlMstCreditObservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateCreditObservation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditObservation")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditObservation(string creditobservation_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objAgrTrnAppCreditUnderWriting.DaDeleteCreditObservation(creditobservation_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditBank")]
        [HttpPost]
        public HttpResponseMessage PostCreditBank(MdlCreditBankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostCreditBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCrediBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetCrediBankAccDtl(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objAgrTrnAppCreditUnderWriting.DaGetCrediBankAccDtl(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Supplier Master
        [ActionName("GetSupplierList")]
        [HttpGet]
        public HttpResponseMessage GetSupplierList()
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objAgrTrnAppCreditUnderWriting.DaGetSupplierList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Buyer Master
        [ActionName("GetCreBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCreBuyerList()
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objAgrTrnAppCreditUnderWriting.DaGetCreBuyerList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Credit Policy Compliance
        [ActionName("GetCrepolicy")]
        [HttpGet]
        public HttpResponseMessage GetCrepolicy()
        {
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objAgrTrnAppCreditUnderWriting.DaGetCrepolicy(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Account Type
        [ActionName("GetCreditAccountType")]
        [HttpGet]
        public HttpResponseMessage GetCreditAccountType()
        {
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objAgrTrnAppCreditUnderWriting.DaGetCreditAccountType(values);
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
            objAgrTrnAppCreditUnderWriting.DachequeleafdocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("DeleteCreditcheque")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditcheque(string creditbankdtl2cheque_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            credituploaddocument values = new credituploaddocument();
            objAgrTrnAppCreditUnderWriting.DaDeleteCreditcheque(creditbankdtl2cheque_gid, credit_gid, values, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaGetCrediBankAccList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditOperationsView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOperationsView(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objAgrTrnAppCreditUnderWriting.DaGetCreditOperationsView(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditBuyerTextData")]
        [HttpGet]
        public HttpResponseMessage GetCreditBuyerTextData(string creditbuyer_gid)
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objAgrTrnAppCreditUnderWriting.DaGetCreditBuyerTextData(creditbuyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditSupplierTextData")]
        [HttpGet]
        public HttpResponseMessage GetCreditSupplierTextData(string creditsupplier_gid)
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objAgrTrnAppCreditUnderWriting.DaGetCreditSupplierTextData(creditsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditBankDocumentUpload")]
        [HttpGet]
        public HttpResponseMessage GetCreditBankDocumentUpload(string creditbankdtl_gid)
        {
            credituploaddocument values = new credituploaddocument();
            objAgrTrnAppCreditUnderWriting.DaGetCreditBankDocumentUpload(creditbankdtl_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaChequeTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditBankAccDtl(string creditbankdtl_gid)
        {
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objAgrTrnAppCreditUnderWriting.DaEditGetCreditBankAccDtl(creditbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditBankAccDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditBankAccDtl(MdlCreditBankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.UpdateCreditBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletecreditBankAcc")]
        [HttpGet]
        public HttpResponseMessage DeletecreditBankAcc(string creditbankdtl_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objAgrTrnAppCreditUnderWriting.DaDeletecreditBankAcc(creditbankdtl_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditExistingBankDtlRemarks")]
        [HttpGet]
        public HttpResponseMessage GetCreditExistingBankDtlRemarks(string existingbankfacility_gid)
        {
            MdlMstExistingRemarks values = new MdlMstExistingRemarks();
            objAgrTrnAppCreditUnderWriting.DaGetCreditExistingBankDtlRemarks(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditRepaymentDtlRemarks")]
        [HttpGet]
        public HttpResponseMessage GetCreditRepaymentDtlRemarks(string creditrepaymentdtl_gid)
        {
            MdlMstRepaymentRemarks values = new MdlMstRepaymentRemarks();
            objAgrTrnAppCreditUnderWriting.DaGetCreditRepaymentDtlRemarks(creditrepaymentdtl_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEditDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEditDocumentTmpList(institution_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEditDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEditForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEditForm60TmpList(institution_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEditForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Institution GST List

        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string institution_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objAgrTrnAppCreditUnderWriting.DaInstitutionGSTList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Mobile Number List

        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string institution_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objAgrTrnAppCreditUnderWriting.DaInstitutionMobileNoList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Email Address List

        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string institution_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objAgrTrnAppCreditUnderWriting.DaInstitutionEmailAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Address List

        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string institution_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objAgrTrnAppCreditUnderWriting.DaInstitutionAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution License List

        [ActionName("InstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseList(string institution_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objAgrTrnAppCreditUnderWriting.DaInstitutionLicenseList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionDocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objAgrTrnAppCreditUnderWriting.DaInstitutionDocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionForm60DocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm60DocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objAgrTrnAppCreditUnderWriting.DaInstitutionForm60DocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Details Edit

        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string institution_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objAgrTrnAppCreditUnderWriting.DaInstitutionDetailsEdit(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Institution Details Update

        [ActionName("UpdateInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateInstitutionDtl(values, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionGSTTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionMobileNoTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionEmailAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaInstitutionLicenseTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGSTList")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGSTList(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution GST

        [ActionName("EditInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionGST(string institution2branch_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objAgrTrnAppCreditUnderWriting.DaEditInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution GST

        [ActionName("UpdateInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateInstitutionGST(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Mobile No

        [ActionName("PostInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Mobile No

        [ActionName("EditInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionMobileNo(string institution2mobileno_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objAgrTrnAppCreditUnderWriting.DaEditInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Institution Mobile No

        [ActionName("UpdateInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateInstitutionMobileNo(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Email Address

        [ActionName("PostInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Email Address

        [ActionName("EditInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionEmailAddress(string institution2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objAgrTrnAppCreditUnderWriting.DaEditInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateInstitutionEmailAddress(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Address Details  

        [ActionName("PostInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostInstitutionAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Address Details 

        [ActionName("EditInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionAddressDetail(string institution2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objAgrTrnAppCreditUnderWriting.DaEditInstitutionAddressDetail(institution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateInstitutionAddressDetail(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteInstitutionAddressDetail(institution2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution License Details

        [ActionName("PostInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostInstitutionLicenseDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution License Details 

        [ActionName("EditInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objAgrTrnAppCreditUnderWriting.DaEditInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution License Details 

        [ActionName("UpdateInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateInstitutionLicenseDetail(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteInstitutionLicenseDetail(institution2licensedtl_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetIntitutionTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Mobile Number Add 
        [ActionName("PostIndividualMobileNumber")]
        [HttpPost]
        public HttpResponseMessage PostIndividualMobileNumber(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostIndividualMobileNumber(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetIndividualMobileNoTempList(contact_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetIndividualMobileNoList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Mobile No

        [ActionName("EditIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIndividualMobileNo(string contact2mobileno_gid)
        {
            MdlContactMobileNo values = new MdlContactMobileNo();
            objAgrTrnAppCreditUnderWriting.DaEditIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Individual Mobile No

        [ActionName("UpdateIndividualMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualMobileNo(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateIndividualMobileNo(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Add 
        [ActionName("PostIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostIndividualEmailAddress(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetIndividualEmailAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetIndividualEmailAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Email Address

        [ActionName("EditIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualEmailAddress(string contact2email_gid)
        {
            MdlContactEmail values = new MdlContactEmail();
            objAgrTrnAppCreditUnderWriting.DaEditIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateIndividualEmailAddress(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage PostIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objAgrTrnAppCreditUnderWriting.DaGetIndividualAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objAgrTrnAppCreditUnderWriting.DaGetIndividualAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Individual Address Details 

        [ActionName("EditIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualAddress(string contact2address_gid)
        {
            MdlContactAddress values = new MdlContactAddress();
            objAgrTrnAppCreditUnderWriting.DaEditIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualAddress(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objAgrTrnAppCreditUnderWriting.DaDeleteIndividualAddress(contact2address_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaIndividualProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualProofTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objAgrTrnAppCreditUnderWriting.DaGetIndividualProofTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objAgrTrnAppCreditUnderWriting.DaGetIndividualProofList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualProofDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualProofDelete(string contact2idproof_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objAgrTrnAppCreditUnderWriting.DaIndividualProofDelete(contact2idproof_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objAgrTrnAppCreditUnderWriting.DaGetIndividualDocTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocList(string contact_gid)
        {
            MdlContactDocument values = new MdlContactDocument();
            objAgrTrnAppCreditUnderWriting.DaGetIndividualDocList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objAgrTrnAppCreditUnderWriting.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividual")]
        [HttpGet]
        public HttpResponseMessage EditIndividual(string contact_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objAgrTrnAppCreditUnderWriting.DaEditIndividual(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Update 
        [ActionName("UpdateIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividual(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateIndividual(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.GetIndividualTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // General Contact Person Mobile No

        [ActionName("PostAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objAgrTrnAppCreditUnderWriting.DaGetAppMobileNoTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objAgrTrnAppCreditUnderWriting.DaGetAppMobileNoList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditAppMobileNo(string application2contact_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objAgrTrnAppCreditUnderWriting.DaEditAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateAppMobileNo(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Person Email Address

        [ActionName("PostAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objAgrTrnAppCreditUnderWriting.DaGetAppEmailAddressTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objAgrTrnAppCreditUnderWriting.DaGetAppEmailAddressList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Email Address

        [ActionName("EditAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditAppEmailAddress(string application2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objAgrTrnAppCreditUnderWriting.DaEditAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Email Address

        [ActionName("UpdateAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAppEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objAgrTrnAppCreditUnderWriting.DaDeleteAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code
        [ActionName("PostAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostAppGeneticCode(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetAppGeneticCodeTempList(application_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetAppGeneticCodeList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditAppGeneticCode(string application2geneticcode_gid)
        {
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objAgrTrnAppCreditUnderWriting.DaEditAppGeneticCode(application2geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteAppGeneticCode(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objAgrTrnAppCreditUnderWriting.DaDeleteAppGeneticCode(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objAgrTrnAppCreditUnderWriting.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetApplicationBasicDetailsTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Add Address Details  

        [ActionName("PostGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostGroupAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Group Address Details 

        [ActionName("EditGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupAddressDetail(string group2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objAgrTrnAppCreditUnderWriting.DaEditGroupAddressDetail(group2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Group Address Details 

        [ActionName("UpdateGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateGroupAddressDetail(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteGroupAddressDetail(group2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Group Bank Details  

        [ActionName("PostGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostGroupBankDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Group Bank Details 

        [ActionName("EditGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupBankDetail(string group2bank_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objAgrTrnAppCreditUnderWriting.DaEditGroupBankDetail(group2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Group Address Details 

        [ActionName("UpdateGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateGroupBankDetail(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaDeleteGroupBankDetail(group2bank_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGroupDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GroupDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentDelete(string group2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objAgrTrnAppCreditUnderWriting.DaGroupDocumentDelete(group2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Address List

        [ActionName("GroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressList(string group_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objAgrTrnAppCreditUnderWriting.DaGroupAddressList(group_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGroupAddressTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Bank List

        [ActionName("GroupBankList")]
        [HttpGet]
        public HttpResponseMessage GroupBankList(string group_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objAgrTrnAppCreditUnderWriting.DaGroupBankList(group_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGroupBankTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentList(string group_gid)
        {
            MdlGroupDocument values = new MdlGroupDocument();
            objAgrTrnAppCreditUnderWriting.DaGroupDocumentList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objAgrTrnAppCreditUnderWriting.DaGroupDocumentTmpList(group_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGroup")]
        [HttpGet]
        public HttpResponseMessage EditGroup(string group_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objAgrTrnAppCreditUnderWriting.DaEditGroup(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Details Update

        [ActionName("UpdateGroupDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateGroupDtl(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetGroupTempClear(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaPostHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("deleteHypoDoc")]
        [HttpGet]
        public HttpResponseMessage deleteHypoDoc(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objAgrTrnAppCreditUnderWriting.DadeleteHypoDoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaHypothecationDocumentTempList(getsessionvalues.employee_gid, application2hypothecation_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("PostHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteHypothecation")]
        [HttpGet]
        public HttpResponseMessage DeleteHypothecation(string application2hypothecation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objAgrTrnAppCreditUnderWriting.DaDeleteHypothecation(application2hypothecation_gid, values, getsessionvalues.employee_gid);
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
            objAgrTrnAppCreditUnderWriting.DaHypothecationDetailsEdit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Hypothecation Details Update

        [ActionName("HypothecationDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage HypothecationDetailsUpdate(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaHypothecationDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ServicechargeEdit")]
        [HttpGet]
        public HttpResponseMessage ServicechargeEdit(string application2servicecharge_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductCharges values = new MdlProductCharges();
            objAgrTrnAppCreditUnderWriting.DaServicechargeEdit(application2servicecharge_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ServicechargeUpdate")]
        [HttpPost]
        public HttpResponseMessage ServicechargeUpdate(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaServicechargeUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUnderwrite")]
        [HttpPost]
        public HttpResponseMessage PostUnderwrite(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaPostUnderwrite(getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaCAMocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CAMdoc_delete")]
        [HttpGet]
        public HttpResponseMessage getcamdoc_delete(string application2camdoc_gid, string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objAgrTrnAppCreditUnderWriting.Dagetcamdoc_delete(application2camdoc_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("GetCAM")]
        [HttpGet]
        public HttpResponseMessage GetCAM(string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objAgrTrnAppCreditUnderWriting.DaGetCAM(application_gid, objdocumentcancel);
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
            objAgrTrnAppCreditUnderWriting.DaImportExcelBankStatement(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBankStatementList")]
        [HttpGet]
        public HttpResponseMessage GetBankStatementList(string credit_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBankStatement values = new MdlMstCUWBankStatement();
            objAgrTrnAppCreditUnderWriting.DaGetBankStatementList(credit_gid, application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BankStatementExportExcel")]
        [HttpPost]
        public HttpResponseMessage BankStatementExportExcel(BankStatementExportExcel objBankStatementExportExcel)
        {
            objAgrTrnAppCreditUnderWriting.DaBankStatementTemplateExport(objBankStatementExportExcel);
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
            objAgrTrnAppCreditUnderWriting.DaImportProfitLoss(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetProfitLoss")]
        [HttpGet]
        public HttpResponseMessage GetProfitLoss(string application_gid, string credit_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProfitLoss values = new MdlMstProfitLoss();
            objAgrTrnAppCreditUnderWriting.DaGetProfitLoss(application_gid, credit_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //FSA Summary

        [ActionName("GetFSASummary")]
        [HttpGet]
        public HttpResponseMessage GetFSASUmmary(string credit_gid, string application_gid)
        {
            MdlMstFSASummary values = new MdlMstFSASummary();
            objAgrTrnAppCreditUnderWriting.DaGetFSASUmmary(credit_gid, application_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaImportExcelBalanceSheetTemplate1(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBalanceSheetTemplate1List")]
        [HttpGet]
        public HttpResponseMessage GetBalanceSheetTemplate1List(string credit_gid, string application_gid, string template_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBalancesheettemplate1 values = new MdlMstCUWBalancesheettemplate1();
            objAgrTrnAppCreditUnderWriting.DaGetBalanceSheetTemplate1List(credit_gid, application_gid, template_type, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaImportExcelSummaryTemplate1(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetSummaryTemplate1View")]
        [HttpGet]
        public HttpResponseMessage GetSummaryTemplate1View(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSummaryTemplate1View values = new MdlSummaryTemplate1View();
            objAgrTrnAppCreditUnderWriting.DaGetSummaryTemplate1View(credit_gid, application_gid, template_name, values);
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
            objAgrTrnAppCreditUnderWriting.DaImportExcelBalanceSheetTemplate2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBalanceSheetTemplate2List")]
        [HttpGet]
        public HttpResponseMessage GetBalanceSheetTemplate2List(string credit_gid, string application_gid, string template_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBalancesheettemplate2 values = new MdlMstCUWBalancesheettemplate2();
            objAgrTrnAppCreditUnderWriting.DaGetBalanceSheetTemplate2List(credit_gid, application_gid, template_type, getsessionvalues.employee_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaImportProfitLossTemp2(httpRequest, getsessionvalues.employee_gid, objResult);
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
            objAgrTrnAppCreditUnderWriting.DaImportExcelSummaryTemplate2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetProfitLossTemp2List")]
        [HttpGet]
        public HttpResponseMessage GetProfitLossTemp2List(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProfitLosstemp2 values = new MdlMstProfitLosstemp2();
            objAgrTrnAppCreditUnderWriting.DaGetProfitLossTemp2List(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSummaryTemplate2View")]
        [HttpGet]
        public HttpResponseMessage GetSummaryTemplate2View(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSummaryTemplate2View values = new MdlSummaryTemplate2View();
            objAgrTrnAppCreditUnderWriting.DaGetSummaryTemplate2View(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetApplicationCreditAprovalinfo")]
        [HttpGet]
        public HttpResponseMessage GetApplicationCreditAprovalinfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstcreditApprovalInfo values = new MdlMstcreditApprovalInfo();
            objAgrTrnAppCreditUnderWriting.DaGetApplicationCreditAprovalinfo(application_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaGetPANForm60TempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objAgrTrnAppCreditUnderWriting.DaGetPANForm60List(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objAgrTrnAppCreditUnderWriting.DaContactPANAbsenceReasonList(contact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("UpdatePANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage UpdatePANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdatePANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string contact_gid)
        {
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objAgrTrnAppCreditUnderWriting.DaEditPANAbsenceReasonList(contact_gid, values);
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
            objAgrTrnAppCreditUnderWriting.DaPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("PANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage PANForm60Delete(string contact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objAgrTrnAppCreditUnderWriting.DaPANForm60Delete(contact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANReasonsCheck")]
        [HttpGet]
        public HttpResponseMessage PANReasonsCheck()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objAgrTrnAppCreditUnderWriting.DaPANReasonsCheck(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getoldnewapplication")]
        [HttpGet]
        public HttpResponseMessage Getoldnewapplication(string onboard_gid,string product_gid,string program_gid)
        {
            MdlMstGetoldnewapplicationid values = new MdlMstGetoldnewapplicationid();
            objAgrTrnAppCreditUnderWriting.Dagetoldnewapplication(onboard_gid, product_gid, program_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductvariety")]
        [HttpGet]
        public HttpResponseMessage getproductvariety(string newapplication2loan_gid, string oldapplication2loan_gid)
        {

            MdlMstGetoldnewapplicationid values = new MdlMstGetoldnewapplicationid();
            objAgrTrnAppCreditUnderWriting.Dagetproductvariety(values, newapplication2loan_gid, oldapplication2loan_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoan2Supplierdtl")]
        [HttpGet]
        public HttpResponseMessage GetLoan2Supplierdtl(string application_gid, string newapplication2loan_gid, string oldapplication2loan_gid, string tmp_status)
        {
            MdlSupplierdtlList values = new MdlSupplierdtlList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaGetLoan2Supplierdtl(newapplication2loan_gid,getsessionvalues.employee_gid, values, tmp_status,oldapplication2loan_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoan2SupplierPaymentdtl")]
        [HttpGet]
        public HttpResponseMessage GetLoan2SupplierPaymentdtl(string application_gid, string newapplication2loan_gid, string oldapplication2loan_gid, string tmp_status)
        {
            MdlSupplierPaymentdtlList values = new MdlSupplierPaymentdtlList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnAppCreditUnderWriting.DaGetLoan2SupplierPaymentdtl( newapplication2loan_gid,application_gid, oldapplication2loan_gid, getsessionvalues.employee_gid, values,tmp_status);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanProgramValueChain")]
        [HttpGet]
        public HttpResponseMessage GetLoanProgramValueChain(string newapplication2loan_gid, string oldapplication2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objAgrTrnAppCreditUnderWriting.DaGetLoanProgramValueChain(oldapplication2loan_gid,values, newapplication2loan_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductprogramgid")]
        [HttpGet]
        public HttpResponseMessage Getproductprogramgid(string application_gid)
        {
            MdlMstGetoldnewapplicationid values = new MdlMstGetoldnewapplicationid();
            objAgrTrnAppCreditUnderWriting.Dagetproductprogramgid(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproductapplicationloangid")]
        [HttpGet]
        public HttpResponseMessage Getproductapplicationloangid(string application2loan_gid, string application_gid)
        {
            MdlMstGetoldnewapplicationid values = new MdlMstGetoldnewapplicationid();
            objAgrTrnAppCreditUnderWriting.Dagetproductapplicationloangid(application2loan_gid, values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getrenewalamendmentflag")]
        [HttpGet]
        public HttpResponseMessage Getrenewalamendmentflag(string application_gid)
        {
            MdlMstGetoldnewapplicationid values = new MdlMstGetoldnewapplicationid();
            objAgrTrnAppCreditUnderWriting.Dagetrenewalamendmentflag(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}