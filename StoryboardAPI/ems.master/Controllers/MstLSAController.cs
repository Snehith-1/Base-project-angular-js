using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Collections.Generic;
using System;
using ems.storage.Functions;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstLSA")]
    [Authorize]

    public class MstLSAController : ApiController
    {
        session_values Objgetgid = new session_values();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        logintoken getsessionvalues = new logintoken();
        DaMstLSA objDaMstLSA = new DaMstLSA();

        [ActionName("GetLSAMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSAMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSAMakerSummaryList values = new MdlLSAMakerSummaryList();
            objDaMstLSA.DaGetLSAMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLSAFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSAFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSAMakerSummaryList values = new MdlLSAMakerSummaryList();
            objDaMstLSA.DaGetLSAFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLSAPendingCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSAPendingCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSACheckerSummaryList values = new MdlLSACheckerSummaryList();
            objDaMstLSA.DaGetLSAPendingCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetLSAFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSAFollowupCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSACheckerSummaryList values = new MdlLSACheckerSummaryList();
            objDaMstLSA.DaGetLSAFollowupCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLSAPendingApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSAPendingApproverSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSAApproverSummaryList values = new MdlLSAApproverSummaryList();
            objDaMstLSA.DaGetLSAPendingApproverSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetLSACompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSACompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSAApproverSummaryList values = new MdlLSAApproverSummaryList();
            objDaMstLSA.DaGetLSACompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //GetLSAReinitiateEligible
        [ActionName("GetLSAReinitiateEligible")]
        [HttpGet]
        public HttpResponseMessage GetLSAReinitiateEligible()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLSAApproverSummaryList values = new MdlLSAApproverSummaryList();
            objDaMstLSA.DaGetLSAReinitiateEligible(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CADLSASummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADLSASummaryCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            CadLSACount values = new CadLSACount();
            objDaMstLSA.DaCADLSASummaryCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGenerateLSAMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetGenerateLSAMakerSummary(string application_gid)
        {
            MdlGenerateLSAMakerSummaryList values = new MdlGenerateLSAMakerSummaryList();
            objDaMstLSA.DaGetGenerateLSAMakerSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetlsaGeneratevalidation")]
        [HttpGet]
        public HttpResponseMessage GetlsaGeneratevalidation(string application_gid,string generatelsa_gid)
        {
            result values = new result();
            objDaMstLSA.DaGetlsaGeneratevalidation(application_gid, generatelsa_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // -------------------Start - Limit & Products -------------------------------------------//

        [ActionName("PostLimitInfo")]
        [HttpPost]
        public HttpResponseMessage PostLimitInfo(limitandproducts values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostLimitInfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLSAApplicationLimitInfo")]
        [HttpGet]
        public HttpResponseMessage GetLSAApplicationLimitInfo(string application_gid,string application2sanction_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            objDaMstLSA.DaGetLSAApplicationLimitInfo(objlsamgmt, application_gid, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetLimitInfoDtl(string generatelsa_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            objDaMstLSA.DaGetLimitInfoDtl(objlsamgmt, generatelsa_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetLimitProductInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetLimitProductInfoDtl(string limitproductinfodtl_gid)
        {
            limitandproducts objlsamgmt = new limitandproducts();
            objDaMstLSA.DaGetLimitProductInfoDtl(objlsamgmt, limitproductinfodtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("Updatelimitproduct")]
        [HttpPost]
        public HttpResponseMessage Updatelimitproduct(limitandproducts values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaMstLSA.DaUpdatelimitproduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // -------------------End - Limit & Products -------------------------------------------//

        // ------------------ Start - Bank Account Details ------------------------------------//

        [ActionName("GetApplicationNameDetails")]
        [HttpGet]
        public HttpResponseMessage GetApplicationNameDetails(string application_gid)
        {
            bankapplicationNameinfolist objlsamgmt = new bankapplicationNameinfolist();
            objDaMstLSA.DaGetApplicationNameDetails(application_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("PostBankAccountDetails")]
        [HttpPost]
        public HttpResponseMessage PostBankAccountDetails(MdlBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostBankAccountDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateBankAccountDetails")]
        [HttpPost]
        public HttpResponseMessage PostUpdateBankAccountDetails(MdlBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostUpdateBankAccountDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeleteLSABankAccountdtl")]
        [HttpGet]
        public HttpResponseMessage GetDeleteLSABankAccountdtl(string lsabankaccdtl_gid)
        {
            result values = new result();
            objDaMstLSA.DaGetDeleteLSABankAccountdtl(lsabankaccdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lsachequeleafdocumentUpload")]
        [HttpPost]
        public HttpResponseMessage lsachequeleafdocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            lsauploaddocument documentname = new lsauploaddocument();
            objDaMstLSA.DalsachequeleafdocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetLSAChequeLeafTmpdoc")]
        [HttpGet]
        public HttpResponseMessage GetLSAChequeLeafTmpdoc()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            lsauploaddocument objlsamgmt = new lsauploaddocument();
            objDaMstLSA.DaGetLSAChequeLeafTmpdoc(getsessionvalues.employee_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetLSAchequeleafDocument")]
        [HttpGet]
        public HttpResponseMessage DaGetLSAchequeleafDocument(string lsabankaccdtl_gid)
        {
            lsauploaddocument objlsamgmt = new lsauploaddocument();
            objDaMstLSA.DaGetLSAchequeleafDocument(lsabankaccdtl_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetDeleteLSAchequeleafDocument")]
        [HttpGet]
        public HttpResponseMessage DaGetDeleteLSAchequeleafDocument(string lsachequeleafdocument_gid)
        {
            result objlsamgmt = new result();
            objDaMstLSA.DaGetDeleteLSAchequeleafDocument(lsachequeleafdocument_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetTmpClearchequeleafDocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpClearchequeleafDocument()
        { 
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstLSA.DaGetTmpClearchequeleafDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLSABankAccountSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSABankAccountSummary(string generatelsa_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccountlist objlsamgmt = new MdlBankAccountlist();
            objDaMstLSA.DaGetLSABankAccountSummary(generatelsa_gid, objlsamgmt, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("GetCreditBankAccountSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditBankAccountSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccountlist objlsamgmt = new MdlBankAccountlist();
            objDaMstLSA.DaGetCreditBankAccountSummary(application_gid, objlsamgmt, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetLSABankAccountDisSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSABankAccountDisSummary(string generatelsa_gid, string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccountlist objlsamgmt = new MdlBankAccountlist();
            objDaMstLSA.DaGetLSABankAccountDisSummary(generatelsa_gid, rmdisbursementrequest_gid, objlsamgmt, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetCreditBankAccountDisSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditBankAccountDisSummary(string application_gid, string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccountlist objlsamgmt = new MdlBankAccountlist();
            objDaMstLSA.DaGetCreditBankAccountDisSummary(application_gid, rmdisbursementrequest_gid, objlsamgmt, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }


        [ActionName("updateDisbursementStatus")]
        [HttpPost]
        public HttpResponseMessage updateDisbursementStatus(MdlBankAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaupdateDisbursementStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLSABankAccountdetail")]
        [HttpGet]
        public HttpResponseMessage GetLSABankAccountdetail(string lsabankaccdtl_gid)
        {
            MdlBankAccount objlsamgmt = new MdlBankAccount();
            objDaMstLSA.DaGetLSABankAccountdetail(lsabankaccdtl_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetCreditBankAccountdetail")]
        [HttpGet]
        public HttpResponseMessage GetCreditBankAccountdetail(string creditbankdtl_gid)
        {
            MdlBankAccount objlsamgmt = new MdlBankAccount();
            objDaMstLSA.DaGetCreditBankAccountdetail(creditbankdtl_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        // ------------------ End - Bank Account Details ------------------------------------// 

        //---------------Start - Fees and Charges Details ---------------------------------//

        [ActionName("GetlsaFeeschargesDetail")]
        [HttpGet]
        public HttpResponseMessage GetlsaFeeschargesDetail(string generatelsa_gid)
        {
            lsaFeecharges objlsamgmt = new lsaFeecharges();
            objDaMstLSA.DaGetlsaFeeschargesDetail(generatelsa_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("GetBankName")]
        [HttpGet]
        public HttpResponseMessage GetBankName()
        {
            MdlBankNamelist objvalues = new MdlBankNamelist();
            objDaMstLSA.DaGetBankName(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("PostDocumentCharge")]
        [HttpPost]
        public HttpResponseMessage PostDocumentCharge(lsadocumentationcharge values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostDocumentCharge(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetlsachargesDetail")]
        [HttpGet]
        public HttpResponseMessage GetlsachargesDetail(string lsafeescharge_gid, string charge_type)
        {
            lsadocumentationcharge values = new lsadocumentationcharge();
            objDaMstLSA.DaGetlsachargesDetail(lsafeescharge_gid, charge_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateChargeDetail")]
        [HttpPost]
        public HttpResponseMessage PostUpdateChargeDetail(lsadocumentationcharge values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostUpdateChargeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProcessingFee")]
        [HttpPost]
        public HttpResponseMessage PostProcessingFee(lsaprocessingfees values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostProcessingFee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postcollateraldetails")]
        [HttpPost]
        public HttpResponseMessage Postcollateraldetails(lsacollateraldetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostcollateraldetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postLSAcollateraldocument")]
        [HttpPost]
        public HttpResponseMessage postLSAcollateraldocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objDaMstLSA.DapostLSAcollateraldocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }


        [ActionName("GetLSACollateraldocument")]
        [HttpGet]
        public HttpResponseMessage GetLSACollateraldocument(string application2loan_gid)
        {
            UploadLSADocumentname objlsamgmt = new UploadLSADocumentname();
            objDaMstLSA.DaGetLSACollateraldocument(objlsamgmt, application2loan_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
         
        [ActionName("GetCollateralTmpClear")]
        [HttpGet]
        public HttpResponseMessage GetCollateralTmpClear(string generatelsa_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaGetCollateralTmpClear(generatelsa_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("deletelsacollateraldoc")]
        [HttpGet]
        public HttpResponseMessage deletelsacollateraldoc(string document_gid) 
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objDaMstLSA.Dadeletelsacollateraldoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("PostUpdatecollateraldetails")]
        [HttpPost]
        public HttpResponseMessage PostUpdatecollateraldetails(lsacollateraldetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostUpdateCollateralDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetlsaCollateralDetail")]
        [HttpGet]
        public HttpResponseMessage GetlsaCollateralDetail(string application2loan_gid)
        {
            lsacollateraldetails values = new lsacollateraldetails();
            objDaMstLSA.DaGetlsaCollateralDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // --------------End - Fees and Charges Details -----------------------------------//

        [ActionName("PostlsaGeneralUploaddocument")]
        [HttpPost]
        public HttpResponseMessage PostlsaGeneralUploaddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadLSADocumentname documentname = new UploadLSADocumentname();
            objDaMstLSA.DaPostlsaGeneralUploaddocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetLSAGeneraldocument")]
        [HttpGet]
        public HttpResponseMessage GetLSAGeneraldocument(string generatelsa_gid)
        {
            UploadLSADocumentname objlsamgmt = new UploadLSADocumentname();
            objDaMstLSA.DaGetLSAGeneraldocument(objlsamgmt, generatelsa_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetDeleteLSAuploadeddocument")]
        [HttpGet]
        public HttpResponseMessage GetDeleteLSAuploadeddocument(string lsauploaddocument_gid)
        {
            result objlsamgmt = new result();
            objDaMstLSA.DaGetDeleteLSAuploadeddocument(objlsamgmt, lsauploaddocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("Getcompliancecheckinfo")]
        [HttpGet]
        public HttpResponseMessage Getcompliancecheckinfo(string generatelsa_gid)
        {
            lsacompliancecheck objlsamgmt = new lsacompliancecheck();
            objDaMstLSA.DaGetcompliancecheckinfo(objlsamgmt, generatelsa_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("PostProceedtoChecker")]
        [HttpPost]
        public HttpResponseMessage PostProceedtoChecker(lsacompliancecheck values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostProceedtoChecker(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProceedtoApprover")]
        [HttpPost]
        public HttpResponseMessage PostProceedtoApprover(lsacompliancecheck values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostProceedtoApprover(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostLSAApproved")]
        [HttpGet]
        public HttpResponseMessage PostLSAApproved(string generatelsa_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objvalues = new result();
            objDaMstLSA.DaPostLSAApproved(getsessionvalues.user_gid, generatelsa_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetlsaProductname")]
        [HttpGet]
        public HttpResponseMessage GetlsaProductname(string application_gid)
        {
            LsaProductnamelist values = new LsaProductnamelist();
            objDaMstLSA.DaGetlsaProductname(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lsabranch")]
        [HttpGet]
        public HttpResponseMessage GetBranchdtl()
        {
            branchlistdtl objlsamgmt = new branchlistdtl();
            objDaMstLSA.DaGetBranchdtl(objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("PostServiceCharges")]
        [HttpPost]
        public HttpResponseMessage PostServiceCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaPostServiceCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLSApdf")]
        [HttpGet]
        public HttpResponseMessage GetLSApdf(string generatelsa_gid)
        {
            lsa_doc objlsadoc = new lsa_doc();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "MstCadLsa/GetCADLSAreport?generatelsa_gid=" + generatelsa_gid);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            string path = JsonConvert.DeserializeObject<string>(response.Content);
            var pathArray = path.Split(new string[] { "EMS/" }, StringSplitOptions.None);
            var fileNameArrray = path.Split(new string[] { "Report/" }, StringSplitOptions.None);
            objlsadoc.file_path   = pathArray[1].ToString();
            objlsadoc.file_name = fileNameArrray[1].ToString();
            //string enc_patharray = objcmnstorage.EncryptData(patharray1);
            ls_response = objcmnstorage.DaFileUploadDocument(objlsadoc.file_path);
            objlsadoc.file_path = objcmnstorage.EncryptData(objlsadoc.file_path);
            var byName = (IDictionary<string, object>)ls_response;
            objlsadoc.status = (bool)byName["status"];
            return Request.CreateResponse(HttpStatusCode.OK, objlsadoc);
        }

        [ActionName("GetBankAccountTempClear")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstLSA.DaGetBankAccountTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postLSAcollateraldocumentAdd")]
        [HttpPost]
        public HttpResponseMessage postLSAcollateraldocumentAdd()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objDaMstLSA.DapostLSAcollateraldocumentAdd(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetLSAChequeLeafTmpdocedit")]
        [HttpGet]
        public HttpResponseMessage GetLSAChequeLeafTmpdocedit(string lsabankaccdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            lsauploaddocument objlsamgmt = new lsauploaddocument();
            objDaMstLSA.DaGetLSAChequeLeafTmpdocedit(lsabankaccdtl_gid, getsessionvalues.employee_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetLSACollateraldocumentEdit")]
        [HttpGet]
        public HttpResponseMessage GetLSACollateraldocumentEdit(string application2loan_gid, string generatelsa_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadLSADocumentname objlsamgmt = new UploadLSADocumentname();
            objDaMstLSA.DaGetLSACollateraldocumentEdit(objlsamgmt, generatelsa_gid, application2loan_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetBankAccountStatus")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountStatus(string application_gid,string rmdisbursementrequest_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccountlist objlsamgmt = new MdlBankAccountlist();
            objDaMstLSA.DaGetBankAccountStatus(application_gid, rmdisbursementrequest_gid,objlsamgmt, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("SubmitLSAReinitiate")]
        [HttpPost]
        public HttpResponseMessage SubmitLSAReinitiate(MdlLSAReinitiate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLSA.DaSubmitLSAReinitiate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //LSA Report Summary
        [ActionName("GetLSAReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetLSAReportSummary()
        {
            MdlLSAReportSummaryList values = new MdlLSAReportSummaryList();
            objDaMstLSA.DaGetLSAReportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //LSA Report Excel Export
        [ActionName("GetLSAReportExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetLSAReportExcelExport()
        {
            MdlLSAReportExcel values = new MdlLSAReportExcel();
            objDaMstLSA.DaGetLSAReportExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}