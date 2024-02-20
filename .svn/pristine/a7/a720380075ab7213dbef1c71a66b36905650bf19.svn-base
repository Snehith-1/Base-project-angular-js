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
    /// This Controllers will help to create contracts for pmg accepted records
    /// </summary>
    /// <remarks>Written by Venkatesh </remarks>

    [RoutePrefix("api/AgrTrnContract")]
    [Authorize]

    public class AgrTrnContractController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrTrnContract objDaAgrTrnContract = new DaAgrTrnContract();

    [ActionName("GetContractMakerSummary")]
    [HttpGet]
    public HttpResponseMessage GetContractMakerSummary()
    {
        string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
        objDaAgrTrnContract.DaGetContractMakerSummary(getsessionvalues.employee_gid, values);
        return Request.CreateResponse(HttpStatusCode.OK, values);
    }

        [ActionName("ContractToCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage ContractToCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaContractToCheckerSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("ContractApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage ContractApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaContractApprovalSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        // Count
        [ActionName("CADContractSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADContractSummaryCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaCADContractSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Checker Count

        //[ActionName("CADContractCheckerCount")]
        //[HttpGet]
        //public HttpResponseMessage CADContractCheckerCount()
        //{
        //    CadSanctionCount values = new CadSanctionCount();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaAgrTrnContract.DaCADContractCheckerCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //Follow up summary
        [ActionName("GetContractFollowupMakerSummary")]
        
        [HttpGet]
        public HttpResponseMessage GetContractFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnContract.DaGetContractFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContractToCheckerFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage ContractToCheckerFollowupSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaContractToCheckerFollowupSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        //Document upload add

        [ActionName("ContractDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage ContractDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaAgrTrnContract.DaContractDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        //Document Upload Edit

        //[ActionName("ContractEditDocumentUpload")]
        //[HttpPost]
        //public HttpResponseMessage ContractEditDocumentUpload()
        //{
        //    HttpRequest httpRequest;
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    httpRequest = HttpContext.Current.Request;
        //    uploaddocument values = new uploaddocument();
        //    objDaAgrTrnContract.DaContractEditDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("ContractDocList")]
        [HttpGet]
        public HttpResponseMessage ContractDocList(string application2sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnContract.DaContractDocList(application2sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage TmpDocumentDelete(string contractdocumentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument objvalues = new uploaddocument();
            objDaAgrTrnContract.DaTmpDocumentDelete(contractdocumentupload_gid, objvalues, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("ContractSummaryDocList")]
        [HttpGet]
        public HttpResponseMessage ContractSummaryDocList(string application2sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaAgrTrnContract.DaContractSummaryDocList(application2sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Letter Moved To Checker 
        [ActionName("PostProceedToChecker")]
        [HttpPost]
        public HttpResponseMessage PostProceedToChecker(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaPostProceedToChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Contrate Creation
        [ActionName("PostCADSanction")]
        [HttpPost]
        public HttpResponseMessage PostCADSanction(cadsanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaPostCADSanction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetContract")]
        [HttpGet]
        public HttpResponseMessage GetContract(string application_gid)
        {
            MdlMstCAD objapplication360 = new MdlMstCAD();
            objDaAgrTrnContract.DaGetContract(application_gid,objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        //Contract Document Edit
        [ActionName("ContractEditDocList")]
        [HttpGet]
        public HttpResponseMessage ContractEditDocList(string application2sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnContract.DaContractEditDocList(application2sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContractDocEditActList")]
        [HttpGet]
        public HttpResponseMessage ContractDocEditActList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnContract.DaContractDocEditActList( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetContractDtl")]
        [HttpGet]
        public HttpResponseMessage GetContractDtl(string application2sanction_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaAgrTrnContract.DaGetContractDtl(application2sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Update Contract

        [ActionName("UpdateContractDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateContractDetail(MdlMstApplicationView values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaUpdateContractDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Contract Summary
        [ActionName("GetAppContractSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppContractSummary(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnContract.DaGetAppContractSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Template

        [ActionName("GetCADTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetCADTemplateDetails(string application2sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaAgrTrnContract.DaGetCADTemplateDetails(values, application2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADContractLetterSummary")]
        [HttpGet]
        public HttpResponseMessage CADContractLetterSummary(string application2sanction_gid)
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaCADContractLetterSummary(application2sanction_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        // Sanction Letter Save 

        [ActionName("CADContractLetterSave")]
        [HttpPost]
        public HttpResponseMessage CADContractLetterSave(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaCADContractLetterSave(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Letter Submit 
        [ActionName("CADContractLetterSubmit")]
        [HttpPost]
        public HttpResponseMessage CADContractLetterSubmit(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaCADContractLetterSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionLetterSummary")]
        [HttpGet]
        public HttpResponseMessage CADSanctionLetterSummary(string application2sanction_gid)
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaCADSanctionLetterSummary(application2sanction_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        // Sanction Letter Save 
        [ActionName("CADSanctionLetterSave")]
        [HttpPost]
        public HttpResponseMessage CADSanctionLetterSave(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaCADSanctionLetterSave(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionLetterSubmit")]
        [HttpPost]
        public HttpResponseMessage CADSanctionLetterSubmit(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaCADSanctionLetterSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalDetails")]
        [HttpGet]
        public HttpResponseMessage GetApprovalDetails(string application_gid)
        {
            MdlChequeApprovalDetails values = new MdlChequeApprovalDetails();
            objDaAgrTrnContract.DaGetApprovalDetails(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProceedToApproval")]
        [HttpPost]
        public HttpResponseMessage PostProceedToApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaPostProceedToApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionSubmitToApproval")]
        [HttpPost]
        public HttpResponseMessage SanctionSubmitToApproval(appsanction_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaSanctionSubmitToApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDigitalSignature")]
        [HttpGet]
        public HttpResponseMessage PostDigitalSignature(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            cadtemplate_list values = new cadtemplate_list();
            objDaAgrTrnContract.DaPostDigitalSignature(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckerApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaUpdateCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Pushback To Maker 
        [ActionName("PusbackToMaker")]
        [HttpPost]
        public HttpResponseMessage PusbackToMaker(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnContract.DaPusbackToMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDetails(string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaAgrTrnContract.DaGetTemplateDetails(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateLogDetails(string sanctionapprovallog_gid, string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaAgrTrnContract.DaGetTemplateLogDetails(values, sanctionapprovallog_gid, sanction_gid);
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
            objDaAgrTrnContract.DAGetAppSanctionSummary(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPDFGenerate")]
        [HttpGet]
        public HttpResponseMessage GetPDFGenerate(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            cadtemplate_list values = new cadtemplate_list();
            objDaAgrTrnContract.DaGetPDFGenerate(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApprovalCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage ApprovalCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaApprovalCompletedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }


        [ActionName("SanctionNotAcceptedSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionNotAcceptedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaSanctionNotAcceptedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("SanctionAcceptedSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionAcceptedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaSanctionAcceptedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("tempcontractdelete")]
        [HttpGet]
        public HttpResponseMessage gettempcontractdelete(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaAgrTrnContract.DaGettempcontractdelete(getsessionvalues.employee_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        //customer 360 contract summary    
        [ActionName("GetAppContract360Summary")]
        [HttpGet]
        public HttpResponseMessage GetAppContract360Summary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCAD values = new MdlMstCAD();
            objDaAgrTrnContract.DAGetAppContract360Summary(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContractTempClear")]
        [HttpGet]
        public HttpResponseMessage ContractTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            MdlMstCAD objvalues = new MdlMstCAD();
            objDaAgrTrnContract.DaContractTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContractRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage ContractRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaAgrTrnContract.DaContractRejectedSummary(objsanctiondetailsList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }


    }

}
