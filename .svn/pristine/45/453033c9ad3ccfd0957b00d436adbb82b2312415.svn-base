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
/// (It's used for pages in CAD Flow)CADFlow Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{

    [RoutePrefix("api/MstCADFlow")]
    [Authorize]

    public class MstCadFlowController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCadFlow objDaMstCadFlow = new DaMstCadFlow();

        [ActionName("CADSanctionSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADSanctionSummaryCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaCADSanctionSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
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
            objDaMstCadFlow.DaGetBuyerList(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempcamdelete(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaMstCadFlow.DaGetTempDelete(getsessionvalues.employee_gid, application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        // Get CAD Basic Deatil
        [ActionName("GetCADBasicView")]
        [HttpGet]
        public HttpResponseMessage GetCADBasicView(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCadFlow.DaGetCADBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Product Type List
        [ActionName("GetProductList")]
        [HttpGet]
        public HttpResponseMessage GetProductList(string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCadFlow.DaGetProductList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcreditheadsview")]
        [HttpGet]
        public HttpResponseMessage Getcreditheadsview(string application_gid)
        {
            MdlappCreditassign objVisitor = new MdlappCreditassign();
            objDaMstCadFlow.DaGetcreditheadsview(objVisitor, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }

        // Get Application Hierarchy List
        [ActionName("Getapplicationhierarchylist")]
        [HttpGet]
        public HttpResponseMessage Getapplicationhierarchylist(string application_gid, string employee_gid)
        {
            applicationhierarchy values = new applicationhierarchy();
            objDaMstCadFlow.DaGetapplicationhierarchylist(values, application_gid, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Sanction To List
        [ActionName("GetSanctionToList")]
        [HttpGet]
        public HttpResponseMessage GetSanctionToList(string sanctiontype_name, string application_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCadFlow.DaGetSanctionToList(sanctiontype_name, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Address Detail
        [ActionName("GetContactPersonDetail")]
        [HttpGet]
        public HttpResponseMessage GetContactPersonDetail(string sanctionto_gid)
        {
            MdlMstCAD values = new MdlMstCAD();
            objDaMstCadFlow.DaGetContactPersonDetail(sanctionto_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit Loan Detail
        [ActionName("GetLoanDetail")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetail(string application2loan_gid)
        {
            cadloanfacilitytype_list values = new cadloanfacilitytype_list();
            objDaMstCadFlow.DaGetLoanDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       

        [ActionName("Postlimitproductinfo")]
        [HttpPost]
        public HttpResponseMessage Postlimitproductinfo(limitandproducts values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaPostlimitproductinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Detail Updation
        [ActionName("UpdateLoanDetails")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanDetails(cadloanfacilitytype_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaUpdateLoanDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductChargesDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesDtl(string application_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCadFlow.DaGetProductChargesDtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPurposeofLoan")]
        [HttpGet]
        public HttpResponseMessage GetPurposeofLoan(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCadFlow.DaGetPurposeofLoan(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanProgramValueChain")]
        [HttpGet]
        public HttpResponseMessage GetLoanProgramValueChain(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCadFlow.DaGetLoanProgramValueChain(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADSanctionDtls")]
        [HttpGet]
        public HttpResponseMessage CADSanctionDtls(string sanction_gid)
        {
            cadsanctiondetails values = new cadsanctiondetails();
            objDaMstCadFlow.DaCADSanctionDtls(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionContent")]
        [HttpPost]
        public HttpResponseMessage SanctionContent(cadtemplate_list values)
        {
            objDaMstCadFlow.DaSanctionContent(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionMultipleFacility")]
        [HttpPost]
        public HttpResponseMessage SanctionMultipleFacility(cadtemplate_list values)
        {

            objDaMstCadFlow.DaPostTemplateSanctionMultipleFacility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckerApproval(cadtemplate_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaUpdateCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetApplicationBasicView")]
        [HttpGet]
        public HttpResponseMessage GetApplicationBasicView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCadFlow.DaGetApplicationBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstCadFlow.DaGetIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstCadFlow.DaGetInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaMstCadFlow.DaGetGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetGrouptoMemberList(string group_gid)
        {
            MdlMstGroupMember values = new MdlMstGroupMember();
            objDaMstCadFlow.DaGetGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistMakerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistMakerSubmit(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaPostDocChecklistMakerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistCheckerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistCheckerSubmit(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaPostDocChecklistCheckerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDocChecklistApproval")]
        [HttpPost]
        public HttpResponseMessage PostDocChecklistApproval(MdlDocChecklistdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaPostDocChecklistApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionCommonTemplate")]
        [HttpPost]
        public HttpResponseMessage SanctionCommonTemplate(cadtemplate_list values)
        {
            objDaMstCadFlow.DaSanctionCommonTemplate(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetproductDropDown")]
        [HttpGet]
        public HttpResponseMessage GetproductDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductDropDown values = new MdlProductDropDown();
            objDaMstCadFlow.DaGetproductDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetScannedGeneralInfo(string application_gid)
        {
            mdlscannedgeneral objresult = new mdlscannedgeneral();
            objDaMstCadFlow.DaGetScannedGeneralInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("CADAppSanctionCount")]
        [HttpGet]
        public HttpResponseMessage CADAppSanctionCount()
        {
            CadSanctionCount values = new CadSanctionCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCadFlow.DaCADAppSanctionCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Bank Account Details List
        [ActionName("BankAccountDetailsList")]
        [HttpGet]
        public HttpResponseMessage BankAccountDetailsList(string application_gid)
        {
            MdlMstBankAccountDetails values = new MdlMstBankAccountDetails();
            objDaMstCadFlow.DaBankAccountDetailsList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetlsaProductname")]
        [HttpGet]
        public HttpResponseMessage GetlsaProductname(string application_gid)
        {
            LsaProductnamelist values = new LsaProductnamelist();
            objDaMstCadFlow.DaGetlsaProductname(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}