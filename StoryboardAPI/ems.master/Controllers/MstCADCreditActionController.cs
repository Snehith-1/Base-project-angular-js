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
/// (It's used for pages in credit action icon in cad accepted page)CADCreditAction Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCADCreditAction")]
    [Authorize]
    public class MstCADCreditActionController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCADCreditAction objDaCADCreditAction = new DaCADCreditAction();

        [ActionName("GetCreditOperationsView")]
        [HttpGet]
        public HttpResponseMessage GetCreditOperationsView(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaCADCreditAction.DaGetCreditOperationsView(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Individual Document
        [ActionName("GetIndividualTypeList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualTypeList(string credit_gid, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaCADCreditAction.DaGetIndividualTypeList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ApplyALLDocumentList")]
        [HttpPost]
        public HttpResponseMessage ApplyALLDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaApplyALLDocumentList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ApplyALLCovenantDocumentList")]
        [HttpPost]
        public HttpResponseMessage ApplyALLCovenantDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaApplyALLCovenantDocumentList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Group Document
        [ActionName("GetGroupTypeList")]
        [HttpGet]
        public HttpResponseMessage GetGroupTypeList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaCADCreditAction.DaGetGroupTypeList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Document Type List
        [ActionName("GetDocumentTypeList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentTypeList(string credit_gid, string application_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaCADCreditAction.DaGetDocumentTypeList(credit_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaCADCreditAction.DaGetCADTrnTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Document Tagged List
        [ActionName("GetCADTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaCADCreditAction.DaGetCADTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Untag Document Type 
        [ActionName("UnTagDocument")]
        [HttpGet]
        public HttpResponseMessage UnTagDocument(string documentcheckdtl_gid)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUnTagDocument(documentcheckdtl_gid, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        // Create Document Type Check List
        [ActionName("PostDocumentCheckList")]
        [HttpPost]
        public HttpResponseMessage PostDocumentCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostDocumentCheckList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Create Document Check List for ALL 
        [ActionName("CheckALLDocumentList")]
        [HttpPost]
        public HttpResponseMessage CheckALLDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaCheckALLDocumentList(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Covenant Document Type List
        [ActionName("GetCovenantDocumentTypeList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantDocumentTypeList(string credit_gid, string application_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaCADCreditAction.DaGetCovenantDocumentTypeList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnCovenantTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnCovenantTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaCADCreditAction.DaGetCADTrnCovenantTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADCovenantTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADCovenantTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaCADCreditAction.DaGetCADCovenantTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostCovenantPeriods(MdlCovenantPeriodlist values)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCovenantPeriods(values, objResult, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetInstitutionBureauTempClear")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionBureauTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaCADCreditAction.DaGetInstitutionBureauTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Institution
        [ActionName("GetInstitutionBureauList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionBureauList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlInstitutionBureau values = new MdlInstitutionBureau();
            objDaCADCreditAction.DaGetInstitutionBureauList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values); 
        }

        [ActionName("CICUploadInstitutionDocList")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocList(string institution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objDaCADCreditAction.DaCICUploadInstitutionDocList(institution2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCICUploadInstitution")]
        [HttpPost]
        public HttpResponseMessage PostCICUploadInstitution(MdlCICInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCICUploadInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteInstitutionBureau")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionBureau(string institution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlInstitutionBureau values = new MdlInstitutionBureau();
            objDaCADCreditAction.DaDeleteInstitutionBureau(institution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICInstitutionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CICInstitutionDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaCADCreditAction.DaCICInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("CICUploadInstitutionDocDelete")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocDelete(string tmpcicdocument_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaCADCreditAction.DaCICUploadInstitutionDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICInstitutionEdit")]
        [HttpGet]
        public HttpResponseMessage CICInstitutionEdit(string institution2bureau_gid)
        {
            MdlCICInstitution values = new MdlCICInstitution();
            objDaCADCreditAction.DaCICInstitutionEdit(institution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCICUploadInstitution")]
        [HttpPost]
        public HttpResponseMessage UpdateCICUploadInstitution(MdlCICInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateCICUploadInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Genetic Code
        //Get Genetic Code
        [ActionName("GetGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetGeneticCodeList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objDaCADCreditAction.DaGetGeneticCodeList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostGeneticCode(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditGeneticCode(string creditgeneticcode_gid)
        {
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objDaCADCreditAction.DaEditGeneticCode(creditgeneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateGeneticCode(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteGeneticCode(string creditgeneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objDaCADCreditAction.DaDeleteGeneticCode(creditgeneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Economic Capital
        [ActionName("EditSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage EditSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaCADCreditAction.DaEditSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage GetSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaCADCreditAction.DaGetSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Submit Social/Trade Capital
        [ActionName("SocialAndTradeCapitalSubmit")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSubmit(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaSocialAndTradeCapitalSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Social/Trade Capital
        [ActionName("SocialAndTradeCapitalUpdate")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalUpdate(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaSocialAndTradeCapitalUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //PSL
        // Get PSL Drop Down List
        [ActionName("GetPSLDropdownList")]
        [HttpGet]
        public HttpResponseMessage GetPSLDropdownList()
        {
            MdlPSLDropDown values = new MdlPSLDropDown();
            objDaCADCreditAction.DaGetPSLDropdownList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit PSL Data Flagging
        [ActionName("EditPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage EditPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaCADCreditAction.DaEditPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage GetPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaCADCreditAction.DaGetPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Save PSL Data Flagging
        [ActionName("PSLDataFlaggingSave")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingSave(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPSLDataFlaggingSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Submit PSL Data Flagging
        [ActionName("PSLDataFlaggingSubmit")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingSubmit(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPSLDataFlaggingSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update PSL Data Flagging
        [ActionName("PSLDataFlaggingUpdate")]
        [HttpPost]
        public HttpResponseMessage PSLDataFlaggingUpdate(MdlMstAppCreditUnderWriting values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPSLDataFlaggingUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Supplier

        //Get supplier Info
        [ActionName("GetCreditSupplierList")]
        [HttpGet]
        public HttpResponseMessage GetCreditSupplierList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplier values = new MdlMstSupplier();
            objDaCADCreditAction.DaGetCreditSupplierList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Supplier Add
        [ActionName("PostCreditSupplier")]
        [HttpPost]
        public HttpResponseMessage PostCreditSupplier(MdlMstSupplier values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCreditSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditSupplier")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditSupplier(string creditsupplier_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplier values = new MdlMstSupplier();
            objDaCADCreditAction.DaDeleteCreditSupplier(creditsupplier_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditSupplier")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditSupplier(string creditsupplier_gid)
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objDaCADCreditAction.DaEditGetCreditSupplier(creditsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Account Type
        [ActionName("GetCreditAccountType")]
        [HttpGet]
        public HttpResponseMessage GetCreditAccountType()
        {
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objDaCADCreditAction.DaGetCreditAccountType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditSupplier")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditSupplier(MdlMstSupplier values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateCreditSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Buyer
        [ActionName("PostCreditBuyer")]
        [HttpPost]
        public HttpResponseMessage PostCreditBuyer(MdlMstCreditBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCreditBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCreditBuyerList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objDaCADCreditAction.DaGetCreditBuyerList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBuyer")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditBuyer(string creditbuyer_gid)
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objDaCADCreditAction.DaEditGetCreditBuyer(creditbuyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditBuyer")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditBuyer(MdlMstCreditBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateCreditBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditBuyer")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditBuyer(string creditbuyer_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objDaCADCreditAction.DaDeleteCreditBuyer(creditbuyer_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Bank Account

        [ActionName("GetCrediBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetCrediBankAccDtl(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objDaCADCreditAction.DaGetCrediBankAccDtl(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeTmpClear")]
        [HttpGet]
        public HttpResponseMessage ChequeTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaCADCreditAction.DaChequeTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditBank")]
        [HttpPost]
        public HttpResponseMessage PostCreditBank(MdlCreditBankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCreditBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletecreditBankAcc")]
        [HttpGet]
        public HttpResponseMessage DeletecreditBankAcc(string creditbankdtl_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objDaCADCreditAction.DaDeletecreditBankAcc(creditbankdtl_gid, credit_gid, values, getsessionvalues.employee_gid);
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
            objDaCADCreditAction.DachequeleafdocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("DeleteCreditcheque")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditcheque(string creditbankdtl2cheque_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            credituploaddocument values = new credituploaddocument();
            objDaCADCreditAction.DaDeleteCreditcheque(creditbankdtl2cheque_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditBankAccDtl(string creditbankdtl_gid)
        {
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objDaCADCreditAction.DaEditGetCreditBankAccDtl(creditbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditBankAccDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditBankAccDtl(MdlCreditBankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.UpdateCreditBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Existing Bank Facility
        [ActionName("GetExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage GetExistingBankFacility(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objDaCADCreditAction.DaGetExistingBankFacility(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

      
        [ActionName("PostExistingBankFacility")]
        [HttpPost]
        public HttpResponseMessage PostExistingBankFacility(MdlMstCUWExistingBankFacility values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostExistingBankFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Existing Bank Facility
        [ActionName("DeleteExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage DeleteExistingBankFacility(string existingbankfacility_gid)
        {
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objDaCADCreditAction.DaDeleteExistingBankFacility(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage EditExistingBankFacility(string existingbankfacility_gid)
        {
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objDaCADCreditAction.DaEditExistingBankFacility(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Existing Bank Facility
        [ActionName("UpdateExistingBankFacility")]
        [HttpPost]
        public HttpResponseMessage UpdateExistingBankFacility(MdlMstCUWExistingBankFacility values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateExistingBankFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Repayment Track
        [ActionName("PostRepaymentTrack")]
        [HttpPost]
        public HttpResponseMessage PostRepaymentTrack(MdlMstCUWRepaymentTrack values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostRepaymentTrack(getsessionvalues.employee_gid, values);
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
            objDaCADCreditAction.DaGetRepaymentTrack(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Repayment Track
        [ActionName("EditRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage EditRepaymentTrack(string creditrepaymentdtl_gid)
        {
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objDaCADCreditAction.DaEditRepaymentTrack(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Repayment Track
        [ActionName("UpdateRepaymentTrack")]
        [HttpPost]
        public HttpResponseMessage UpdateRepaymentTrack(MdlMstCUWRepaymentTrack values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateRepaymentTrack(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Delete Repayment Track
        [ActionName("DeleteRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage DeleteRepaymentTrack(string creditrepaymentdtl_gid)
        {
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objDaCADCreditAction.DaDeleteRepaymentTrack(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //BankStatement
        [ActionName("ImportExcelBankStatement")]
        [HttpPost]
        public HttpResponseMessage ImportExcelIndividual()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaCADCreditAction.DaImportExcelBankStatement(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetBankStatementList")]
        [HttpGet]
        public HttpResponseMessage GetBankStatementList(string credit_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBankStatement values = new MdlMstCUWBankStatement();
            objDaCADCreditAction.DaGetBankStatementList(credit_gid, application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BankStatementExportExcel")]
        [HttpPost]
        public HttpResponseMessage BankStatementExportExcel(BankStatementExportExcel objBankStatementExportExcel)
        {
            objDaCADCreditAction.DaBankStatementTemplateExport(objBankStatementExportExcel);
            return Request.CreateResponse(HttpStatusCode.OK, objBankStatementExportExcel);
        }

        //FSA
        //FSA Summary

        [ActionName("GetFSASummary")]
        [HttpGet]
        public HttpResponseMessage GetFSASUmmary(string credit_gid, string application_gid)
        {
            MdlMstFSASummary values = new MdlMstFSASummary();
            objDaCADCreditAction.DaGetFSASUmmary(credit_gid, application_gid, values);
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
            objDaCADCreditAction.DaImportExcelBalanceSheetTemplate1(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ImportExcelBalanceSheetTemplate2")]
        [HttpPost]
        public HttpResponseMessage ImportExcelBalanceSheetTemplate2()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaCADCreditAction.DaImportExcelBalanceSheetTemplate2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
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
            objDaCADCreditAction.DaImportProfitLoss(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
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
            objDaCADCreditAction.DaImportProfitLossTemp2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
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
            objDaCADCreditAction.DaImportExcelSummaryTemplate1(httpRequest, getsessionvalues.employee_gid, objResult);
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
            objDaCADCreditAction.DaImportExcelSummaryTemplate2(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }



        //Credit Observations
        [ActionName("PostCreditObservation")]
        [HttpPost]
        public HttpResponseMessage PostCreditObservation(MdlMstCreditObservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCreditObservation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditObservationList")]
        [HttpGet]
        public HttpResponseMessage GetCreditObservationList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objDaCADCreditAction.DaGetCreditObservationList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditObservation")]
        [HttpGet]
        public HttpResponseMessage EditGetCreditObservation(string creditobservation_gid)
        {
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objDaCADCreditAction.DaEditGetCreditObservation(creditobservation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditObservation")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditObservation(MdlMstCreditObservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateCreditObservation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditObservation")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditObservation(string creditobservation_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objDaCADCreditAction.DaDeleteCreditObservation(creditobservation_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Individual
        //Document Checklist

        // Create Document Type Check List
        [ActionName("PostIndividualCheckList")]
        [HttpPost]
        public HttpResponseMessage PostIndividualCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostIndividualCheckList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Covenant

        [ActionName("GetCovenantIndividualDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantIndividualDocumentList(string credit_gid, string application_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaCADCreditAction.DaGetCovenantIndividualDocumentList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Bureau

        [ActionName("GetIndividualBureauTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIndividualBureauTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaCADCreditAction.DaGetIndividualBureauTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Contact Bureau List
        [ActionName("GetContactBureauList")]
        [HttpGet]
        public HttpResponseMessage GetContactBureauList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objDaCADCreditAction.DaGetContactBureauList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocList(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objDaCADCreditAction.DaCICUploadIndividualDocList(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCICUploadIndividual")]
        [HttpPost]
        public HttpResponseMessage PostCICUploadIndividual(MdlCICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostCICUploadIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteContactBureau")]
        [HttpGet]
        public HttpResponseMessage DeleteContactBureau(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objDaCADCreditAction.DaDeleteContactBureau(contact2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCICUploadIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateCICUploadIndividual(MdlCICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaUpdateCICUploadIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadIndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocDelete(string tmpcicdocument_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaCADCreditAction.DaCICUploadIndividualDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualEdit")]
        [HttpGet]
        public HttpResponseMessage CICIndividualEdit(string contact2bureau_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaCADCreditAction.DaCICIndividualEdit(contact2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group

        // Create Document Type Check List
        [ActionName("PostGroupCheckList")]
        [HttpPost]
        public HttpResponseMessage PostGroupCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADCreditAction.DaPostGroupCheckList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCovenantGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantGroupDocumentList(string credit_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaCADCreditAction.DaGetCovenantGroupDocumentList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CICIndividualDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaCADCreditAction.DaCICIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetProfitLoss")]
        [HttpGet]
        public HttpResponseMessage GetProfitLoss(string application_gid, string credit_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProfitLoss values = new MdlMstProfitLoss();
            objDaCADCreditAction.DaGetProfitLoss(application_gid, credit_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBalanceSheetTemplate1List")]
        [HttpGet]
        public HttpResponseMessage GetBalanceSheetTemplate1List(string credit_gid, string application_gid, string template_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBalancesheettemplate1 values = new MdlMstCUWBalancesheettemplate1();
            objDaCADCreditAction.DaGetBalanceSheetTemplate1List(credit_gid, application_gid, template_type, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetBalanceSheetTemplate2List")]
        [HttpGet]
        public HttpResponseMessage GetBalanceSheetTemplate2List(string credit_gid, string application_gid, string template_type)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWBalancesheettemplate2 values = new MdlMstCUWBalancesheettemplate2();
            objDaCADCreditAction.DaGetBalanceSheetTemplate2List(credit_gid, application_gid, template_type, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSummaryTemplate1View")]
        [HttpGet]
        public HttpResponseMessage GetSummaryTemplate1View(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSummaryTemplate1View values = new MdlSummaryTemplate1View();
            objDaCADCreditAction.DaGetSummaryTemplate1View(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSummaryTemplate2View")]
        [HttpGet]
        public HttpResponseMessage GetSummaryTemplate2View(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSummaryTemplate2View values = new MdlSummaryTemplate2View();
            objDaCADCreditAction.DaGetSummaryTemplate2View(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProfitLossTemp2List")]
        [HttpGet]
        public HttpResponseMessage GetProfitLossTemp2List(string credit_gid, string application_gid, string template_name)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProfitLosstemp2 values = new MdlMstProfitLosstemp2();
            objDaCADCreditAction.DaGetProfitLossTemp2List(credit_gid, application_gid, template_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Label
        [ActionName("Getlabels")]
        [HttpGet]
        public HttpResponseMessage Getlabels(string credit_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaCADCreditAction.DaGetlabels(credit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}