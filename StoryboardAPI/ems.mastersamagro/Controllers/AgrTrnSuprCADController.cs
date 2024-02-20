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
    /// This Controllers will provide access to various functionalities in Post Tac Activity menu (Summaries, Process - accept/sendback cases)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>


    [RoutePrefix("api/AgrTrnSuprCAD")]
    [Authorize]

    public class AgrTrnSuprCADController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrTrnSuprCAD objDaAgrTrnSuprCAD = new DaAgrTrnSuprCAD();

        [ActionName("UpdateProcessType")]
        [HttpPost]
        public HttpResponseMessage UpdateIBCallMobileNo(MdlUpdateProcessType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaUpdateProcessType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingCADReviewSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingCADReviewSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetPendingCADReviewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSentBackToCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetSentBackToCCSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetSentBackToCCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADAcceptedCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADAcceptedCustomerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADAcceptedCustomerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSentBackToUnderwritingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSentBackToUnderwritingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetSentBackToUnderwritingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCCRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADApplicationCount")]
        [HttpGet]
        public HttpResponseMessage MyApplicationCount()
        {
            CadApplicationCount values = new CadApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCADApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADMembers")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupEdit(string cadgroup_gid)
        {
            MdlCadGroup objmaster = new MdlCadGroup();
            objDaAgrTrnSuprCAD.DaGetCADMembers(cadgroup_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("PostProcessType")]
        [HttpPost]
        public HttpResponseMessage PostCADGroup(MdlProcessType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostProcessType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Summary
        [ActionName("GetProcessTypeSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupAssignmentSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProcessTypeSummary objmaster = new MdlMstProcessTypeSummary();
            objDaAgrTrnSuprCAD.DaGetProcessTypeSummary(application_gid, objmaster, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Get CAD Basic Deatil
        [ActionName("GetCADBasicView")]
        [HttpGet]
        public HttpResponseMessage GetCADBasicView(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Sanction To List
        [ActionName("GetSanctionToList")]
        [HttpGet]
        public HttpResponseMessage GetSanctionToList(string sanctiontype_name, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetSanctionToList(sanctiontype_name, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Address Detail
        [ActionName("GetContactPersonDetail")]
        [HttpGet]
        public HttpResponseMessage GetContactPersonDetail(string sanctionto_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetContactPersonDetail(sanctionto_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Buyer List
        [ActionName("GetBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetBuyerList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetBuyerList(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Product Type List
        [ActionName("GetProductList")]
        [HttpGet]
        public HttpResponseMessage GetProductList(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetProductList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Sub Product Type List
        [ActionName("GetSubProductList")]
        [HttpGet]
        public HttpResponseMessage GetSubProductList(string producttype_gid, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetSubProductList(producttype_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Product Details
        [ActionName("GetProductDetail")]
        [HttpGet]
        public HttpResponseMessage GetProductDetail(string application_gid, string productsubtype_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetProductDetail(application_gid, productsubtype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get MOM, CAM Documents
        [ActionName("GetMOMCAMDocument")]
        [HttpGet]
        public HttpResponseMessage GetMOMCAMDocument(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetMOMCAMDocument(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Loan Detail Updation
        [ActionName("UpdateLoanDetails")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanDetails(cadloanfacilitytype_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaUpdateLoanDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Edit Loan Detail
        [ActionName("GetLoanDetail")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetail(string application2loan_gid)
        {
            cadloanfacilitytype_list values = new cadloanfacilitytype_list();
            objDaAgrTrnSuprCAD.DaGetLoanDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Sanction Creation
        [ActionName("PostCADSanction")]
        [HttpPost]
        public HttpResponseMessage PostCADSanction(cadsanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostCADSanction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postlimitproductinfo")]
        [HttpPost]
        public HttpResponseMessage Postlimitproductinfo(limitandproducts values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostlimitproductinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetSanctionLimitInfoDtl(string application_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaGetSanctionLimitInfoDtl(objlsamgmt, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetApp2SanctionLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetApp2SanctionLimitInfoDtl(string application_gid, string application2sanction_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            objDaAgrTrnSuprCAD.DaGetApp2SanctionLimitInfoDtl(objlsamgmt, application_gid, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetAppLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetAppLimitInfoDtl(string application_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            objDaAgrTrnSuprCAD.DaGetAppLimitInfoDtl(objlsamgmt, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("SanctionUpdatelimitproduct")]
        [HttpPost]
        public HttpResponseMessage SanctionUpdatelimitproduct(limitandproducts values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaSanctionUpdatelimitproduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Sanction Summary
        [ActionName("GetAppSanctionSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppSanctionSummary(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DAGetAppSanctionSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionDtls")]
        [HttpGet]
        public HttpResponseMessage CADSanctionDtls(string sanction_gid)
        {
            cadsanctiondetails values = new cadsanctiondetails();
            objDaAgrTrnSuprCAD.DaCADSanctionDtls(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetCADTemplateDetails(string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaAgrTrnSuprCAD.DaGetCADTemplateDetails(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionLetterSummary")]
        [HttpGet]
        public HttpResponseMessage CADSanctionLetterSummary(string sanction_gid)
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnSuprCAD.DaCADSanctionLetterSummary(sanction_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("SanctionContent")]
        [HttpPost]
        public HttpResponseMessage SanctionContent(cadtemplate_list values)
        {
            objDaAgrTrnSuprCAD.DaSanctionContent(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Sanction2Facility")]
        [HttpPost]
        public HttpResponseMessage Sanction2Facility(cadtemplate_list values)
        {

            objDaAgrTrnSuprCAD.DaPostTemplateSanction2Facility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionMultipleFacility")]
        [HttpPost]
        public HttpResponseMessage SanctionMultipleFacility(cadtemplate_list values)
        {

            objDaAgrTrnSuprCAD.DaPostTemplateSanctionMultipleFacility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionStandbyLine")]
        [HttpPost]
        public HttpResponseMessage SanctionStandbyLine(cadtemplate_list values)
        {

            objDaAgrTrnSuprCAD.DaPostTemplateSanctionStandbyLine(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DBSColending")]
        [HttpPost]
        public HttpResponseMessage DBSColending(cadtemplate_list values)
        {

            objDaAgrTrnSuprCAD.DaPostTemplateDBSColending(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Sanction Letter Submit 
        [ActionName("CADSanctionLetterSubmit")]
        [HttpPost]
        public HttpResponseMessage CADSanctionLetterSubmit(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCADSanctionLetterSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Sanction Letter Moved To Checker 
        [ActionName("PostProceedToChecker")]
        [HttpPost]
        public HttpResponseMessage PostProceedToChecker(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostProceedToChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionToCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionToCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnSuprCAD.DaSanctionToCheckerSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("SanctionToCheckerFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionToCheckerFollowupSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnSuprCAD.DaSanctionToCheckerFollowupSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("PostProceedToApproval")]
        [HttpPost]
        public HttpResponseMessage PostProceedToApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostProceedToApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Pushback To Maker 
        [ActionName("PusbackToMaker")]
        [HttpPost]
        public HttpResponseMessage PusbackToMaker(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPusbackToMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage CheckerApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnSuprCAD.DaCheckerApprovalSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("ApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage ApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnSuprCAD.DaApprovalCompletedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("PostDigitalSignature")]
        [HttpGet]
        public HttpResponseMessage PostDigitalSignature(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            cadtemplate_list values = new cadtemplate_list();
            objDaAgrTrnSuprCAD.DaPostDigitalSignature(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPDFGenerate")]
        [HttpGet]
        public HttpResponseMessage GetPDFGenerate(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            cadtemplate_list values = new cadtemplate_list();
            objDaAgrTrnSuprCAD.DaGetPDFGenerate(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionLetterLogDownload")]
        [HttpGet]
        public HttpResponseMessage SanctionLetterLogDownload(string sanctionapprovallog_gid)
        {
            cadtemplate_list values = new cadtemplate_list();
            objDaAgrTrnSuprCAD.DaSanctionLetterLogDownload(sanctionapprovallog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckerApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaUpdateCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateLogDetails(string sanctionapprovallog_gid, string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaAgrTrnSuprCAD.DaGetTemplateLogDetails(values, sanctionapprovallog_gid, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDetails(string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaAgrTrnSuprCAD.DaGetTemplateDetails(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADAppSanctionCount")]
        [HttpGet]
        public HttpResponseMessage CADAppSanctionCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCADAppSanctionCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADLoanFacilityTemplateList")]
        [HttpGet]
        public HttpResponseMessage GetCADLoanFacilityTemplateList(string sanction_gid)
        {
            Mdlloanfacility_type values = new Mdlloanfacility_type();
            objDaAgrTrnSuprCAD.DaGetCADLoanFacilityTemplateList(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Document Type List
        [ActionName("GetDocumentTypeList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentTypeList(string credit_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaAgrTrnSuprCAD.DaGetDocumentTypeList(credit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Create Document Type Check List
        [ActionName("PostDocumentCheckList")]
        [HttpPost]
        public HttpResponseMessage PostDocumentCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostDocumentCheckList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Individual Document
        [ActionName("GetIndividualTypeList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualTypeList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetIndividualTypeList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Create Document Type Check List
        [ActionName("PostIndividualCheckList")]
        [HttpPost]
        public HttpResponseMessage PostIndividualCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostIndividualCheckList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Document
        [ActionName("GetGroupTypeList")]
        [HttpGet]
        public HttpResponseMessage GetGroupTypeList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetGroupTypeList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Create Document Type Check List
        [ActionName("PostGroupCheckList")]
        [HttpPost]
        public HttpResponseMessage PostGroupCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostGroupCheckList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Document Tagged List
        [ActionName("GetCADTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADTrnTaggedDocList(values, credit_gid);
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
            objDaAgrTrnSuprCAD.DaUnTagDocument(documentcheckdtl_gid, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Create Document Check List for ALL 
        [ActionName("CheckALLDocumentList")]
        [HttpPost]
        public HttpResponseMessage CheckALLDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCheckALLDocumentList(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSanctionEdit")]
        [HttpGet]
        public HttpResponseMessage GetSanctionEdit(string application2sanction_gid)
        {
            cadsanctiondetails values = new cadsanctiondetails();
            objDaAgrTrnSuprCAD.DaGetSanctionEdit(application2sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUpdateSanction")]
        [HttpPost]
        public HttpResponseMessage PostUpdateSanction(cadsanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostUpdateSanction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADDocChecklistMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADDocChecklistFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADDocChecklistFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistMakerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistMakerSubmit(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostDocChecklistMakerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADDocChecklistCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADDocChecklistCheckerFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistCheckerFollowupSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADDocChecklistCheckerFollowupSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistCheckerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistCheckerSubmit(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostDocChecklistCheckerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADDocChecklistApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistApproval")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistApproval(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostDocChecklistApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetSanctionMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetSanctionFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADSanctionSummaryCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCADSanctionSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CADDocChecklistSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADDocChecklistSummaryCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCADDocChecklistSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssignmentView")]
        [HttpGet]
        public HttpResponseMessage GetAssignmentView(string application_gid)
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrTrnSuprCAD.DaGetAssignmentView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSentbackToCCUnderwritingView")]
        [HttpGet]
        public HttpResponseMessage GetSentbackToCCUnderwritingView(string application_gid)
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrTrnSuprCAD.DaGetSentbackToCCUnderwritingView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Menu
        [ActionName("GetMenu")]
        [HttpGet]
        public HttpResponseMessage DaGetMenu(string application_gid)
        {
            MdlMstCADGetMenu objmenu = new MdlMstCADGetMenu();
            objDaAgrTrnSuprCAD.DaGetMenu(application_gid, objmenu);
            return Request.CreateResponse(HttpStatusCode.OK, objmenu);
        }

        [ActionName("PostRevertCADtoCC")]
        [HttpPost]
        public HttpResponseMessage PostRevertCADtoCC(MdlCADRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostRevertCADtoCC(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRevertCADtoCredit")]
        [HttpPost]
        public HttpResponseMessage PostRevertCADtoCredit(MdlCADRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostRevertCADtoCredit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Covenant Document Type List
        [ActionName("GetCovenantDocumentTypeList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantDocumentTypeList(string credit_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaAgrTrnSuprCAD.DaGetCovenantDocumentTypeList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCovenantIndividualDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantIndividualDocumentList(string credit_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaAgrTrnSuprCAD.DaGetCovenantIndividualDocumentList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCovenantGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantGroupDocumentList(string credit_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaAgrTrnSuprCAD.DaGetCovenantGroupDocumentList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADCovenantTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADCovenantTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADCovenantTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnCovenantTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnCovenantTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetCADTrnCovenantTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostCovenantPeriods(MdlCovenantPeriodlist values)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostCovenantPeriods(values, objResult, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetAppRevertReasonRemarks")]
        [HttpGet]
        public HttpResponseMessage GetAppRevertReasonRemarks(string application_gid)
        {
            MdlappCreditassign values = new MdlappCreditassign();
            objDaAgrTrnSuprCAD.DaGetAppRevertReasonRemarks(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePSLCompleted")]
        [HttpPost]
        public HttpResponseMessage UpdatePSLCompleted(pslcsacomplete values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaUpdatePSLCompleted(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLCSACompleteSummary")]
        [HttpGet]
        public HttpResponseMessage GetPSLCSACompleteSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetPSLCSACompleteSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLCSAManagementSummary")]
        [HttpGet]
        public HttpResponseMessage GetPSLCSAManagementSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetPSLCSAManagementSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLCompleteFlag")]
        [HttpGet]
        public HttpResponseMessage GetPSLCompleteFlag(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaGetPSLCompleteFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Letter Save 
        [ActionName("CADSanctionLetterSave")]
        [HttpPost]
        public HttpResponseMessage CADSanctionLetterSave(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaCADSanctionLetterSave(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADGroupDtl")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupDtl(string application_gid, string menu_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaGetCADGroupDtl(getsessionvalues.employee_gid, application_gid, menu_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostReassignCADApplication")]
        [HttpPost]
        public HttpResponseMessage PostReassignCADApplication(MdlReassignCadApplication values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnSuprCAD.DaPostReassignCADApplication(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetReassignApplicationView")]
        [HttpGet]
        public HttpResponseMessage GetReassignApplicationView(string application_gid, string menu_gid)
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaAgrTrnSuprCAD.DaGetReassignApplicationView(application_gid, menu_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteCADAssignment")]
        [HttpGet]
        public HttpResponseMessage DeleteCADAssignment(string processtypeassign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCADAssignment values = new MdlMstCADAssignment();
            objDaAgrTrnSuprCAD.DaDeleteCADAssignment(processtypeassign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Document Upload //

        [ActionName("Uploades_declarationdocument")]
        [HttpPost]
        public HttpResponseMessage Uploades_declarationdocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadCADDocumentname documentname = new UploadCADDocumentname();
            objDaAgrTrnSuprCAD.DaUploades_declarationdocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("Getesdocument")]
        [HttpGet]
        public HttpResponseMessage Getesdocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaAgrTrnSuprCAD.DaGetesdocument(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("GetApp2sanctionesdocument")]
        [HttpGet]
        public HttpResponseMessage GetApp2sanctionesdocument(string application2sanction_gid)
        {
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaAgrTrnSuprCAD.DaGetApp2sanctionesdocument(objdocument, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("Uploadmaildocument")]
        [HttpPost]
        public HttpResponseMessage Uploadmaildocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadCADDocumentname documentname = new UploadCADDocumentname();
            objDaAgrTrnSuprCAD.DaUploadmaildocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetMaildocument")]
        [HttpGet]
        public HttpResponseMessage GetMaildocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaAgrTrnSuprCAD.DaGetMaildocument(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }


        [ActionName("GetApp2sanctionMaildocument")]
        [HttpGet]
        public HttpResponseMessage GetApp2sanctionMaildocument(string application2sanction_gid)
        {
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaAgrTrnSuprCAD.DaGetApp2sanctionMaildocument(objdocument, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("uploadesdocumentadd_delete")]
        [HttpGet]
        public HttpResponseMessage uploadesdocumentadd_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaAgrTrnSuprCAD.DaGetuploadesdocumentadd_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("Maildocumentadd_delete")]
        [HttpGet]
        public HttpResponseMessage Maildocumentadd_delete(string document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaAgrTrnSuprCAD.DaMaildocumentadd_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempcamdelete(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaAgrTrnSuprCAD.DaGetTempDelete(getsessionvalues.employee_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("GetSanctionlimitvalidation")]
        [HttpGet]
        public HttpResponseMessage GetSanctionlimitvalidation(string application2sanction_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Sanctionlimitvalidation values = new Sanctionlimitvalidation();
            objDaAgrTrnSuprCAD.DaGetSanctionlimitvalidation(values, application2sanction_gid, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADtoCCMeetingLog")]
        [HttpGet]
        public HttpResponseMessage GetCADtoCCMeetingLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCADtoCCMeetingLog values = new MdlCADtoCCMeetingLog();
            objDaAgrTrnSuprCAD.DaGetCADtoCCMeetingLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADtoCreditLog")]
        [HttpGet]
        public HttpResponseMessage GetCADtoCreditLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCADtoCreditLog values = new MdlCADtoCreditLog();
            objDaAgrTrnSuprCAD.DaGetCADtoCreditLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADtoCCLog")]
        [HttpGet]
        public HttpResponseMessage GetCADtoCCLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCADtoCCLog values = new MdlCADtoCCLog();
            objDaAgrTrnSuprCAD.DaGetCADtoCCLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetDocChecklistApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetDocChecklistApprovalDtls(string application_gid)
        {
            MdlSanctionApprovalDetails values = new MdlSanctionApprovalDetails();
            objDaAgrTrnSuprCAD.DaGetDocChecklistApprovalDtls(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocChecklistApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDocChecklistApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnSuprCAD.DaGetDocChecklistApprovalCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}