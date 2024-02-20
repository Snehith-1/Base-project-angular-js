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
/// (It's used for flow after CAD Accepted in Samfin)CAD Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>
namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCAD")]
    [Authorize]

    public class MstCADController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCAD objDaMstCAD = new DaMstCAD();

        [ActionName("UpdateProcessType")]
        [HttpPost]
        public HttpResponseMessage UpdateIBCallMobileNo(MdlUpdateProcessType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaUpdateProcessType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingCADReviewSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingCADReviewSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetPendingCADReviewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSentBackToCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetSentBackToCCSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetSentBackToCCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADAcceptedCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADAcceptedCustomerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADAcceptedCustomerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSentBackToUnderwritingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSentBackToUnderwritingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetSentBackToUnderwritingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCCRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADApplicationCount")]
        [HttpGet]
        public HttpResponseMessage MyApplicationCount()
        {
            CadApplicationCount values = new CadApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCADApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADMembers")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupEdit(string cadgroup_gid)
        {
            MdlCadGroup objmaster = new MdlCadGroup();
            objDaMstCAD.DaGetCADMembers(cadgroup_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("PostProcessType")]
        [HttpPost]
        public HttpResponseMessage PostCADGroup(MdlProcessType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostProcessType(values, getsessionvalues.employee_gid);
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
            objDaMstCAD.DaGetProcessTypeSummary(application_gid, objmaster, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Get CAD Basic Deatil
        [ActionName("GetCADBasicView")]
        [HttpGet]
        public HttpResponseMessage GetCADBasicView(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Sanction To List
        [ActionName("GetSanctionToList")]
        [HttpGet]
        public HttpResponseMessage GetSanctionToList(string sanctiontype_name, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetSanctionToList(sanctiontype_name, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Address Detail
        [ActionName("GetContactPersonDetail")]
        [HttpGet]
        public HttpResponseMessage GetContactPersonDetail(string sanctionto_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetContactPersonDetail(sanctionto_gid, values);
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
            objDaMstCAD.DaGetBuyerList(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Product Type List
        [ActionName("GetProductList")]
        [HttpGet]
        public HttpResponseMessage GetProductList(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetProductList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Sub Product Type List
        [ActionName("GetSubProductList")]
        [HttpGet]
        public HttpResponseMessage GetSubProductList(string producttype_gid, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetSubProductList(producttype_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Product Details
        [ActionName("GetProductDetail")]
        [HttpGet]
        public HttpResponseMessage GetProductDetail(string application_gid, string productsubtype_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetProductDetail(application_gid, productsubtype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get MOM, CAM Documents
        [ActionName("GetMOMCAMDocument")]
        [HttpGet]
        public HttpResponseMessage GetMOMCAMDocument(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetMOMCAMDocument(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Loan Detail Updation
        [ActionName("UpdateLoanDetails")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanDetails(cadloanfacilitytype_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaUpdateLoanDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Edit Loan Detail
        [ActionName("GetLoanDetail")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetail(string application2loan_gid)
        {
            cadloanfacilitytype_list values = new cadloanfacilitytype_list();
            objDaMstCAD.DaGetLoanDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Sanction Creation
        [ActionName("PostCADSanction")]
        [HttpPost]
        public HttpResponseMessage PostCADSanction(cadsanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostCADSanction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postlimitproductinfo")]
        [HttpPost]
        public HttpResponseMessage Postlimitproductinfo(limitandproducts values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostlimitproductinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("GetSanctionLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetSanctionLimitInfoDtl(string application_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaGetSanctionLimitInfoDtl(objlsamgmt, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetApp2SanctionLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetApp2SanctionLimitInfoDtl(string application_gid,string application2sanction_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist(); 
            objDaMstCAD.DaGetApp2SanctionLimitInfoDtl(objlsamgmt, application_gid, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("GetAppLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetAppLimitInfoDtl(string application_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist(); 
            objDaMstCAD.DaGetAppLimitInfoDtl(objlsamgmt, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("SanctionUpdatelimitproduct")]
        [HttpPost]
        public HttpResponseMessage SanctionUpdatelimitproduct(limitandproducts values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaSanctionUpdatelimitproduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Sanction Summary
        [ActionName("GetAppSanctionSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppSanctionSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DAGetAppSanctionSummary(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionDtls")]
        [HttpGet]
        public HttpResponseMessage CADSanctionDtls(string sanction_gid)
        {
            cadsanctiondetails values = new cadsanctiondetails();
            objDaMstCAD.DaCADSanctionDtls(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetCADTemplateDetails(string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaMstCAD.DaGetCADTemplateDetails(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionLetterSummary")]
        [HttpGet]
        public HttpResponseMessage CADSanctionLetterSummary(string sanction_gid)
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaCADSanctionLetterSummary(sanction_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }
        
        [ActionName("SanctionContent")]
        [HttpPost]
        public HttpResponseMessage SanctionContent(cadtemplate_list values)
        {
            objDaMstCAD.DaSanctionContent(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Sanction2Facility")]
        [HttpPost]
        public HttpResponseMessage Sanction2Facility(cadtemplate_list values)
        {

            objDaMstCAD.DaPostTemplateSanction2Facility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionMultipleFacility")]
        [HttpPost]
        public HttpResponseMessage SanctionMultipleFacility(cadtemplate_list values)
        {

            objDaMstCAD.DaPostTemplateSanctionMultipleFacility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionStandbyLine")]
        [HttpPost]
        public HttpResponseMessage SanctionStandbyLine(cadtemplate_list values)
        {

            objDaMstCAD.DaPostTemplateSanctionStandbyLine(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DBSColending")]
        [HttpPost]
        public HttpResponseMessage DBSColending(cadtemplate_list values)
        {

            objDaMstCAD.DaPostTemplateDBSColending(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       

        // Sanction Letter Submit 
        [ActionName("CADSanctionLetterSubmit")]
        [HttpPost]
        public HttpResponseMessage CADSanctionLetterSubmit(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCADSanctionLetterSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Sanction Letter Moved To Checker 
        [ActionName("PostProceedToChecker")]
        [HttpPost]
        public HttpResponseMessage PostProceedToChecker(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostProceedToChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionToCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionToCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaSanctionToCheckerSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("SanctionToCheckerFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionToCheckerFollowupSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaSanctionToCheckerFollowupSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("PostProceedToApproval")]
        [HttpPost]
        public HttpResponseMessage PostProceedToApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostProceedToApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Pushback To Maker 
        [ActionName("PusbackToMaker")]
        [HttpPost]
        public HttpResponseMessage PusbackToMaker(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPusbackToMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage CheckerApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaCheckerApprovalSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("ApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage ApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaApprovalCompletedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("PostDigitalSignature")]
        [HttpGet]
        public HttpResponseMessage PostDigitalSignature(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            cadtemplate_list values = new cadtemplate_list();
            objDaMstCAD.DaPostDigitalSignature(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPDFGenerate")]
        [HttpGet]
        public HttpResponseMessage GetPDFGenerate(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            cadtemplate_list values = new cadtemplate_list();
            objDaMstCAD.DaGetPDFGenerate(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionLetterLogDownload")]
        [HttpGet]
        public HttpResponseMessage SanctionLetterLogDownload(string sanctionapprovallog_gid)
        {
            cadtemplate_list values = new cadtemplate_list();
            objDaMstCAD.DaSanctionLetterLogDownload(sanctionapprovallog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckerApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaUpdateCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateLogDetails(string sanctionapprovallog_gid, string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaMstCAD.DaGetTemplateLogDetails(values, sanctionapprovallog_gid, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDetails(string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaMstCAD.DaGetTemplateDetails(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADAppSanctionCount")]
        [HttpGet]
        public HttpResponseMessage CADAppSanctionCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCADAppSanctionCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADLoanFacilityTemplateList")]
        [HttpGet]
        public HttpResponseMessage GetCADLoanFacilityTemplateList(string sanction_gid)
        {
            Mdlloanfacility_type values = new Mdlloanfacility_type();
            objDaMstCAD.DaGetCADLoanFacilityTemplateList(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Document Type List
        [ActionName("GetDocumentTypeList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentTypeList(string credit_gid, string application_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaMstCAD.DaGetDocumentTypeList(credit_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Create Document Type Check List
        [ActionName("PostDocumentCheckList")]
        [HttpPost]
        public HttpResponseMessage PostDocumentCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostDocumentCheckList(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Individual Document
        [ActionName("GetIndividualTypeList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualTypeList(string credit_gid, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetIndividualTypeList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Create Document Type Check List
        [ActionName("PostIndividualCheckList")]
        [HttpPost]
        public HttpResponseMessage PostIndividualCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostIndividualCheckList(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ApplyALLDocumentList")]
        [HttpPost]
        public HttpResponseMessage ApplyALLDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaApplyALLDocumentList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ApplyALLCovenantDocumentList")]
        [HttpPost]
        public HttpResponseMessage ApplyALLCovenantDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaApplyALLCovenantDocumentList(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Group Document
        [ActionName("GetGroupTypeList")]
        [HttpGet]
        public HttpResponseMessage GetGroupTypeList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetGroupTypeList(values,credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Create Document Type Check List
        [ActionName("PostGroupCheckList")]
        [HttpPost]
        public HttpResponseMessage PostGroupCheckList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostGroupCheckList(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Document Tagged List
        [ActionName("GetCADTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADTrnTaggedDocList(values, credit_gid);
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
            objDaMstCAD.DaUnTagDocument(documentcheckdtl_gid, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Create Document Check List for ALL 
        [ActionName("CheckALLDocumentList")]
        [HttpPost]
        public HttpResponseMessage CheckALLDocumentList(MdlMstCAD values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCheckALLDocumentList(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSanctionEdit")]
        [HttpGet]
        public HttpResponseMessage GetSanctionEdit(string application2sanction_gid)
        {
            cadsanctiondetails values = new cadsanctiondetails();
            objDaMstCAD.DaGetSanctionEdit(application2sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUpdateSanction")]
        [HttpPost]
        public HttpResponseMessage PostUpdateSanction(cadsanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostUpdateSanction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADDocChecklistMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADDocChecklistFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADDocChecklistFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistMakerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistMakerSubmit(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostDocChecklistMakerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADDocChecklistCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADDocChecklistCheckerFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistCheckerFollowupSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADDocChecklistCheckerFollowupSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistCheckerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistCheckerSubmit(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostDocChecklistCheckerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADDocChecklistApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADDocChecklistApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADDocChecklistApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistApproval")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistApproval(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostDocChecklistApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetSanctionMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetSanctionFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADSanctionSummaryCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCADSanctionSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CADDocChecklistSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADDocChecklistSummaryCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCADDocChecklistSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssignmentView")]
        [HttpGet]
        public HttpResponseMessage GetAssignmentView(string application_gid)
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstCAD.DaGetAssignmentView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSentbackToCCUnderwritingView")]
        [HttpGet]
        public HttpResponseMessage GetSentbackToCCUnderwritingView(string application_gid)
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstCAD.DaGetSentbackToCCUnderwritingView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Menu
        [ActionName("GetMenu")]
        [HttpGet]
        public HttpResponseMessage DaGetMenu(string application_gid)
        {
            MdlMstCADGetMenu objmenu = new MdlMstCADGetMenu();
            objDaMstCAD.DaGetMenu(application_gid, objmenu);
            return Request.CreateResponse(HttpStatusCode.OK, objmenu);
        }

        [ActionName("PostRevertCADtoCC")]
        [HttpPost]
        public HttpResponseMessage PostRevertCADtoCC(MdlCADRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostRevertCADtoCC(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRevertCADtoCredit")]
        [HttpPost]
        public HttpResponseMessage PostRevertCADtoCredit(MdlCADRevert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostRevertCADtoCredit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Covenant Document Type List
        [ActionName("GetCovenantDocumentTypeList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantDocumentTypeList(string credit_gid, string application_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaMstCAD.DaGetCovenantDocumentTypeList(values,credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCovenantIndividualDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantIndividualDocumentList(string credit_gid, string application_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaMstCAD.DaGetCovenantIndividualDocumentList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCovenantGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCovenantGroupDocumentList(string credit_gid)
        {
            MdlMstCADCompany values = new MdlMstCADCompany();
            objDaMstCAD.DaGetCovenantGroupDocumentList(values,credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADCovenantTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADCovenantTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADCovenantTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnCovenantTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnCovenantTaggedDocList(string credit_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADTrnCovenantTaggedDocList(values, credit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostCovenantPeriods(MdlCovenantPeriodlist values)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostCovenantPeriods(values, objResult, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        } 
        [ActionName("GetAppRevertReasonRemarks")]
        [HttpGet]
        public HttpResponseMessage GetAppRevertReasonRemarks(string application_gid)
        {
            MdlappCreditassign values = new MdlappCreditassign();
            objDaMstCAD.DaGetAppRevertReasonRemarks(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePSLCompleted")]
        [HttpPost]
        public HttpResponseMessage UpdatePSLCompleted(pslcsacomplete values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaUpdatePSLCompleted(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLCSACompleteSummary")]
        [HttpGet]
        public HttpResponseMessage GetPSLCSACompleteSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetPSLCSACompleteSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLCSAManagementSummary")]
        [HttpGet]
        public HttpResponseMessage GetPSLCSAManagementSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetPSLCSAManagementSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPSLCompleteFlag")]
        [HttpGet]
        public HttpResponseMessage GetPSLCompleteFlag(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaGetPSLCompleteFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Letter Save 
        [ActionName("CADSanctionLetterSave")]
        [HttpPost]
        public HttpResponseMessage CADSanctionLetterSave(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaCADSanctionLetterSave(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADGroupDtl")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupDtl(string application_gid, string menu_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaGetCADGroupDtl(getsessionvalues.employee_gid, application_gid, menu_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostReassignCADApplication")]
        [HttpPost]
        public HttpResponseMessage PostReassignCADApplication(MdlReassignCadApplication values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostReassignCADApplication(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetReassignApplicationView")]
        [HttpGet]
        public HttpResponseMessage GetReassignApplicationView(string application_gid, string menu_gid)
        {
            MdlMstAssignmentview values = new MdlMstAssignmentview();
            objDaMstCAD.DaGetReassignApplicationView(application_gid, menu_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteCADAssignment")]
        [HttpGet]
        public HttpResponseMessage DeleteCADAssignment(string processtypeassign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCADAssignment values = new MdlMstCADAssignment();
            objDaMstCAD.DaDeleteCADAssignment(processtypeassign_gid, values);
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
            objDaMstCAD.DaUploades_declarationdocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("Getesdocument")]
        [HttpGet]
        public HttpResponseMessage Getesdocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaMstCAD.DaGetesdocument(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("GetApp2sanctionesdocument")]
        [HttpGet]
        public HttpResponseMessage GetApp2sanctionesdocument(string application2sanction_gid) 
        {
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaMstCAD.DaGetApp2sanctionesdocument(objdocument, application2sanction_gid);
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
            objDaMstCAD.DaUploadmaildocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetMaildocument")]
        [HttpGet]
        public HttpResponseMessage GetMaildocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaMstCAD.DaGetMaildocument(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }


        [ActionName("GetApp2sanctionMaildocument")]
        [HttpGet]
        public HttpResponseMessage GetApp2sanctionMaildocument(string application2sanction_gid)
        {
            UploadCADDocumentname objdocument = new UploadCADDocumentname();
            objDaMstCAD.DaGetApp2sanctionMaildocument(objdocument, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("uploadesdocumentadd_delete")]
        [HttpGet]
        public HttpResponseMessage uploadesdocumentadd_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaMstCAD.DaGetuploadesdocumentadd_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("Maildocumentadd_delete")]
        [HttpGet]
        public HttpResponseMessage Maildocumentadd_delete(string document_gid) 
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaMstCAD.DaMaildocumentadd_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempcamdelete(string application_gid) 
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaMstCAD.DaGetTempDelete(getsessionvalues.employee_gid, application_gid,objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("GetSanctionlimitvalidation")]
        [HttpGet]
        public HttpResponseMessage GetSanctionlimitvalidation(string application2sanction_gid,string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Sanctionlimitvalidation values = new Sanctionlimitvalidation();
            objDaMstCAD.DaGetSanctionlimitvalidation(values, application2sanction_gid, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
 
        [ActionName("GetCADtoCCMeetingLog")]
        [HttpGet]
        public HttpResponseMessage GetCADtoCCMeetingLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCADtoCCMeetingLog values = new MdlCADtoCCMeetingLog();
            objDaMstCAD.DaGetCADtoCCMeetingLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADtoCreditLog")]
        [HttpGet]
        public HttpResponseMessage GetCADtoCreditLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCADtoCreditLog values = new MdlCADtoCreditLog();
            objDaMstCAD.DaGetCADtoCreditLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditWithoutCCLog")]
        [HttpGet]
        public HttpResponseMessage GetCreditWithoutCCLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditWithoutCCLog values = new MdlCreditWithoutCCLog();
            objDaMstCAD.DaGetCreditWithoutCCLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalDetails")]
        [HttpGet]
        public HttpResponseMessage GetApprovalDetails(string application_gid)
        {
            MdlSanctionApprovalDetails values = new MdlSanctionApprovalDetails();
            objDaMstCAD.DaGetApprovalDetails(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocChecklistApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetDocChecklistApprovalDtls(string application_gid)
        {
            MdlSanctionApprovalDetails values = new MdlSanctionApprovalDetails();
            objDaMstCAD.DaGetDocChecklistApprovalDtls(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocChecklistApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetDocChecklistApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetDocChecklistApprovalCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADUrnGroupingDtlsSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADUrnGroupingDtlsSummary(string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.GetCADUrnGroupingDtlsSummary(customer_urn,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADUrnGroupingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADUrnGroupingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADUrnGroupingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADAcceptDetails")]
        [HttpGet]
        public HttpResponseMessage GetCADAcceptDetails(string application_gid)
        {
            MdlCADAcceptDetails values = new MdlCADAcceptDetails();
            objDaMstCAD.DaGetCADAcceptDetails(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMCADUrnGroupingSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMCADUrnGroupingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetRMCADUrnGroupingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMMyCustomerListSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMMyCustomerListSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetRMMyCustomerListSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMRnewalApplicationSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMRnewalApplicationSummary(string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetRMRnewalApplicationSummary(customer_urn, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SanctionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage SanctionDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaSanctionDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SanctionDocDelete")]
        [HttpGet]
        public HttpResponseMessage SanctionDocDelete(string application2sanctiondoc_gid, string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD objdocumentcancel = new MdlMstCAD();
            objDaMstCAD.DaSanctionDocDelete(application2sanctiondoc_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("GetSanction")]
        [HttpGet]
        public HttpResponseMessage GetSanction(string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD objdocumentcancel = new MdlMstCAD();
            objDaMstCAD.DaGetSanction(application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("URNTag")]
        [HttpPost]
        public HttpResponseMessage URNTag(customerurntag values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaURNTag(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADUrnGroupingLegalTagSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADUrnGroupingLegalTagSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADUrnGroupingLegalTagSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADUrnGroupingNPATagSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADUrnGroupingNPATagSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaGetCADUrnGroupingNPATagSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("URNUntag")]
        [HttpPost]
        public HttpResponseMessage URNUntag(customerurntag values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaURNUntag(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("URNNPAtagHistory")]
        [HttpGet]
        public HttpResponseMessage URNNPAtagHistory(string customer2tag_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            customerurntag values = new customerurntag();
            objDaMstCAD.DaURNNPAtagHistory(customer2tag_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("URNLegaltagHistory")]
        [HttpGet]
        public HttpResponseMessage URNLegaltagHistory(string customer2tag_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            customerurntag values = new customerurntag();
            objDaMstCAD.DaURNLegaltagHistory(customer2tag_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("URNTagCount")]
        [HttpGet]
        public HttpResponseMessage URNTagCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            customerurntag values = new customerurntag();
            objDaMstCAD.DaURNTagCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionPopup")]
        [HttpGet]
        public HttpResponseMessage SanctionPopup(string application_gid)
        {
            appsanction_list objmaster = new appsanction_list();
            objDaMstCAD.DaSanctionPopup(application_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("SanctionAccepte")]
        [HttpPost]
        public HttpResponseMessage SanctionAccepte(appsanction_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaSanctionAccepte(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("SanctionAcceptedLog")]
        [HttpGet]
        public HttpResponseMessage SanctionAcceptedLog(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaSanctionAcceptedLog(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionAcceptedSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionAcceptedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaSanctionAcceptedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("SanctionNotAcceptedSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionNotAcceptedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaMstCAD.DaSanctionNotAcceptedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("SanctionHistory")]
        [HttpGet]
        public HttpResponseMessage SanctionHistory(string application2sanction_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCAD.DaSanctionHistory(application2sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionDtlslog")]
        [HttpGet]
        public HttpResponseMessage CADSanctionDtlslog(string sanctionsubmittoapprovallog_gid)
        {
            reportcadsanctiondetails values = new reportcadsanctiondetails();
            objDaMstCAD.DaCADSanctionDtlslog(sanctionsubmittoapprovallog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDetailslog")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDetailslog(string sanctionsubmittoapprovallog_gid)
        {
            reportmdltemplate values = new reportmdltemplate();
            objDaMstCAD.DaGetTemplateDetailslog(values, sanctionsubmittoapprovallog_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getesdocumentlog")]
        [HttpGet]
        public HttpResponseMessage Getesdocumentlog(string sanctionsubmittoapprovallog_gid)

        {
            ReportUploadCADDocumentname objdocument = new ReportUploadCADDocumentname();
            objDaMstCAD.DaGetesdocumentlog(objdocument, sanctionsubmittoapprovallog_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("GetMaildocumentlog")]
        [HttpGet]
        public HttpResponseMessage GetMaildocumentlog(string sanctionsubmittoapprovallog_gid)

        {
            ReportUploadCADDocumentname objdocument = new ReportUploadCADDocumentname();
            objDaMstCAD.DaGetMaildocumentlog(objdocument, sanctionsubmittoapprovallog_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }


        [ActionName("SanctionSubmitToApproval")]
        [HttpPost]
        public HttpResponseMessage SanctionSubmitToApproval(appsanction_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaSanctionSubmitToApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApp2SanctionLimitInfoSubmitDtl")]
        [HttpGet]
        public HttpResponseMessage GetApp2SanctionLimitInfoSubmitDtl(string sanctionsubmittoapprovallog_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            objDaMstCAD.DaGetApp2SanctionLimitInfoSubmitDtl(objlsamgmt, sanctionsubmittoapprovallog_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("PostReUpdateSanction")]
        [HttpPost]
        public HttpResponseMessage PostReUpdateSanction(cadsanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaPostReUpdateSanction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionHistory")]
        [HttpGet]
        public HttpResponseMessage GetSanctionHistory(string sanctionsubmittoapprovallog_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD objdocumentcancel = new MdlMstCAD();
            objDaMstCAD.DaGetSanctionHistory(sanctionsubmittoapprovallog_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        //npa report
        [ActionName("ExportNpaReport")]
        [HttpGet]
        public HttpResponseMessage GetExportNpaReport()
        {
            MdlMstCAD objMstCAD = new MdlMstCAD();
            objDaMstCAD.DaGetExportNpaReport(objMstCAD);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCAD);
        }

        //Legal report
        [ActionName("ExportLegalReport")]
        [HttpGet]
        public HttpResponseMessage GetExportLegalReport()
        {
            MdlMstCAD objMstCAD = new MdlMstCAD();
            objDaMstCAD.DaExportLegalReport(objMstCAD);
            return Request.CreateResponse(HttpStatusCode.OK, objMstCAD);
        }
        [ActionName("GetCadDocumentSubmissionFlag")]
        [HttpGet]
        public HttpResponseMessage GetCadDocumentSubmissionFlag(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadDocumentSubmissionFlag values = new MdlCadDocumentSubmissionFlag();
            objDaMstCAD.DaGetCadDocumentSubmissionFlag(getsessionvalues.employee_gid,application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Sent Back Process Type Update
        [ActionName("SentBackUpdateProcessType")]
        [HttpPost]
        public HttpResponseMessage SentBackUpdateProcessType(MdlUpdateProcessType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCAD.DaSentBackUpdateProcessType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}