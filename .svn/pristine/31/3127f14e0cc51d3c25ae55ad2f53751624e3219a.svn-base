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
/// (It's used for CAD page in Samfin)CADApplication Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>
namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCADApplication")]
    [Authorize]

    public class MstCADApplicationController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCADApplication objDaMstCADApplication = new DaMstCADApplication();

        //Basic Details
        [ActionName("GetApplicationBasicView")]
        [HttpGet]
        public HttpResponseMessage GetApplicationBasicView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetApplicationBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGeneticDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetGeneticDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetGeneticDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMobileMailDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetMobileMailDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetMobileMailDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductChargesDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesDtl(string application_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetProductChargesDtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstCADApplication.DaGetIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstCADApplication.DaGetInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetRMDetailsView(string application_gid)
        {
            MdlRMDtlView values = new MdlRMDtlView();
            objDaMstCADApplication.DaGetRMDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCAM")]
        [HttpGet]
        public HttpResponseMessage GetCAM(string application_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCC objdocumentcancel = new MdlMstCC();
            objDaMstCADApplication.DaGetCAM(application_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("GetCollateralDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetCollateralDocDtl(string application2loan_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetCollateralDocDtl(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHypoDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetHypoDocDtl(string application2hypothecation_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetHypoDocDtl(application2hypothecation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPurposeofLoan")]
        [HttpGet]
        public HttpResponseMessage GetPurposeofLoan(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetPurposeofLoan(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanProgramValueChain")]
        [HttpGet]
        public HttpResponseMessage GetLoanProgramValueChain(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetLoanProgramValueChain(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoantoBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetLoantoBuyerList(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetLoantoBuyerList(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditGroup")]
        [HttpGet]
        public HttpResponseMessage EditGroup(string group_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaMstCADApplication.DaEditGroup(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressList(string group_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaGroupAddressList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupBankList")]
        [HttpGet]
        public HttpResponseMessage GroupBankList(string group_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaMstCADApplication.DaGroupBankList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentList(string group_gid)
        {
            MdlGroupDocument values = new MdlGroupDocument();
            objDaMstCADApplication.DaGroupDocumentList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaMstCADApplication.DaGetGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetGrouptoMemberList(string group_gid)
        {
            MdlMstGroupMember values = new MdlMstGroupMember();
            objDaMstCADApplication.DaGetGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUnderwrite")]
        [HttpPost]
        public HttpResponseMessage PostUnderwrite(MdlMstCUWGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostUnderwrite(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetGeneticCodeList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGeneticCode values = new MdlGeneticCode();
            objDaMstCADApplication.DaGetGeneticCodeList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDown values = new MdlDropDown();
            objDaMstCADApplication.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSectorcategory")]
        [HttpGet]
        public HttpResponseMessage GetSectorcategory(string product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSectorcategory values = new MdlSectorcategory();
            objDaMstCADApplication.DaGetSectorcategory(getsessionvalues.employee_gid, product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApplicationBasicDetailsTempClear")]
        [HttpGet]
        public HttpResponseMessage GetApplicationBasicDetailsTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCADApplication.DaGetApplicationBasicDetailsTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCADApplication.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaGetAppMobileNoList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVarietyDtl")]
        [HttpGet]
        public HttpResponseMessage GetVarietyDtl(string variety_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSectorcategory values = new MdlSectorcategory();
            objDaMstCADApplication.DaGetVarietyDtl(getsessionvalues.employee_gid, variety_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaGetAppMobileNoTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditAppMobileNo(string application2contact_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaEditAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateAppMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteAppMobileNo(string application2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaDeleteAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaGetAppEmailAddressTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaGetAppEmailAddressList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditAppEmailAddress(string application2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaEditAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAppEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAppEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaDeleteAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppGeneticCodeTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppGeneticCodeTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaMstCADApplication.DaGetAppGeneticCodeTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetAppGeneticCodeList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaMstCADApplication.DaGetAppGeneticCodeList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditAppGeneticCode(string application2geneticcode_gid)
        {
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaMstCADApplication.DaEditAppGeneticCode(application2geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateAppGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateAppGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateAppGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteAppGeneticCode(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaMstCADApplication.DaDeleteAppGeneticCode(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Institution
        [ActionName("GetIndividualTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIndividualTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCADApplication.GetIndividualTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIntitutionTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIntitutionTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCADApplication.DaGetIntitutionTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditProductcharges")]
        [HttpGet]
        public HttpResponseMessage GetEditProductcharges(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCADApplication.DaGetEditProductcharges(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGSTInstitution")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTInstitution(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaMstCADApplication.DaDeleteGSTInstitution(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionGSTList")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGSTList(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionForm_60DocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionForm_60DocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("InstitutionForm_60DocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm_60DocumentDelete(string institution2form60documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("SaveInstitutionDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionDtlAdd(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaSaveInstitutionDtlAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitInstitutionDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtlAdd(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaSubmitInstitutionDtlAdd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionLicenseList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaMstCADApplication.DaGetInstitutionLicenseList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionGSTList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaMstCADApplication.DaGetInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaGetInstitutionMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaGetInstitutionEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaGetInstitutionAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionGST(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaMstCADApplication.DaDeleteInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionMobileNo(string institution2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaDeleteInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionEmailAddress(string institution2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaDeleteInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionAddressDetail(string institution2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaDeleteInstitutionAddressDetail(institution2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionLicenseDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaMstCADApplication.DaDeleteInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("InstitutionDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentDelete(string institution2documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string institution_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objDaMstCADApplication.DaInstitutionGSTList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string institution_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaInstitutionMobileNoList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string institution_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaInstitutionEmailAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string institution_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaInstitutionAddressList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseList(string institution_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaMstCADApplication.DaInstitutionLicenseList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionDocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionDocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionForm60DocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm60DocumentList(string institution_gid)
        {
            institutionuploaddocument values = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionForm60DocumentList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string institution_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objDaMstCADApplication.DaInstitutionDetailsEdit(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionLicenseTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaMstCADApplication.DaInstitutionLicenseTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionGSTTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaMstCADApplication.DaInstitutionGSTTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionMobileNoTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaInstitutionMobileNoTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEmailAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaInstitutionEmailAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaInstitutionAddressTmpList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionGST(string institution2branch_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objDaMstCADApplication.DaEditInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEditForm_60DocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionEditForm_60DocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionEditForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("InstitutionEditForm60TmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditForm60TmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument values = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionEditForm60TmpList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEditForm_60DocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditForm_60DocumentDelete(string institution2form60documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionEditForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("EditInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionMobileNo(string institution2mobileno_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objDaMstCADApplication.DaEditInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionEmailAddress(string institution2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objDaMstCADApplication.DaEditInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionAddressDetail(string institution2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaEditInstitutionAddressDetail(institution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateInstitutionAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEditDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionEditDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionEditDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("InstitutionEditDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditDocumentTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument values = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionEditDocumentTmpList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEditDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditDocumentDelete(string institution2documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objDaMstCADApplication.DaInstitutionEditDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("EditInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objDaMstCADApplication.DaEditInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateInstitutionLicenseDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Individual
        [ActionName("GetGroupList")]
        [HttpGet]
        public HttpResponseMessage GetGroupList(string application_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objDaMstCADApplication.DaGetGroupList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCompanyList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyList(string application_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objDaMstCADApplication.DaGetCompanyList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage PANAbsenceReasonList()
        {
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objDaMstCADApplication.DaPANAbsenceReasonList(objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }
        [ActionName("MobileNumberAdd")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAdd(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaMobileNumberAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaMstCADApplication.DaGetMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MobileNoDelete")]
        [HttpGet]
        public HttpResponseMessage MobileNoDelete(string contact2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaMstCADApplication.DaMobileNoDelete(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmailAddressAdd")]
        [HttpPost]
        public HttpResponseMessage EmailAddressAdd(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaEmailAddressAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaMstCADApplication.DaGetEmailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EmailAddressDelete")]
        [HttpGet]
        public HttpResponseMessage EmailAddressDelete(string contact2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaMstCADApplication.DaEmailAddressDelete(contact2email_gid, values);
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
            objDaMstCADApplication.DaIndividualProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objDaMstCADApplication.DaGetIndividualProofList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualProofDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualProofDelete(string contact2idproof_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objDaMstCADApplication.DaIndividualProofDelete(contact2idproof_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaMstCADApplication.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AddressAdd")]
        [HttpPost]
        public HttpResponseMessage AddressAdd(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaAddressAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AddressDelete")]
        [HttpGet]
        public HttpResponseMessage AddressDelete(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaMstCADApplication.DaAddressDelete(contact2address_gid, values);
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
            objDaMstCADApplication.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objDaMstCADApplication.DaGetIndividualDocList(getsessionvalues.employee_gid, values, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objDaMstCADApplication.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaveIndividualDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SaveInSaveIndividualDtlAdddividualEditDtl(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaSaveIndividualDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitIndividualDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualDtlAdd(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaSubmitIndividualDtlAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANForm60List")]
        [HttpGet]
        public HttpResponseMessage PANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaMstCADApplication.PANForm60List(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage PostPANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostPANAbsenceReasons(values, getsessionvalues.employee_gid);
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
            objDaMstCADApplication.DaPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaMstCADApplication.DaGetPANForm60List(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage PANForm60Delete(string contact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaMstCADApplication.DaPANForm60Delete(contact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualMobileNoList")]
        [HttpGet]
        public HttpResponseMessage IndividualMobileNoList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaMstCADApplication.DaGetIndividualMobileNoList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualEmailAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaMstCADApplication.DaGetIndividualEmailAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaMstCADApplication.DaGetIndividualAddressList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objDaMstCADApplication.DaGetIndividualProofList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualDocList")]
        [HttpGet]
        public HttpResponseMessage IndividualDocList(string contact_gid)
        {
            MdlContactDocument values = new MdlContactDocument();
            objDaMstCADApplication.IndividualDocList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetContactGroupList")]
        [HttpGet]
        public HttpResponseMessage GetContactGroupList(string contact_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objDaMstCADApplication.DaGetContactGroupList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetContactCompanyList")]
        [HttpGet]
        public HttpResponseMessage GetContactCompanyList(string contact_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objDaMstCADApplication.DaGetContactCompanyList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditIndividual")]
        [HttpGet]
        public HttpResponseMessage EditIndividual(string contact_gid)
        {
            MdlMstContact values = new MdlMstContact();
            objDaMstCADApplication.DaEditIndividual(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string contact_gid)
        {
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objDaMstCADApplication.DaEditPANAbsenceReasonList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objDaMstCADApplication.DaContactPANAbsenceReasonList(contact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }
        [ActionName("UpdatePANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage UpdatePANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdatePANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPANForm60TempList")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60TempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaMstCADApplication.DaGetPANForm60TempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualMobileNoList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaMstCADApplication.DaGetIndividualMobileNoTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIndividualMobileNo(string contact2mobileno_gid)
        {
            MdlContactMobileNo values = new MdlContactMobileNo();
            objDaMstCADApplication.DaEditIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateIndividualMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualMobileNo(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateIndividualMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualEmailAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objDaMstCADApplication.DaGetIndividualEmailAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualEmailAddress(string contact2email_gid)
        {
            MdlContactEmail values = new MdlContactEmail();
            objDaMstCADApplication.DaEditIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualEmailAddress(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateIndividualEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objDaMstCADApplication.DaGetIndividualAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualAddress(string contact2address_gid)
        {
            MdlContactAddress values = new MdlContactAddress();
            objDaMstCADApplication.DaEditIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualAddress(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualProofTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objDaMstCADApplication.DaGetIndividualProofTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objDaMstCADApplication.DaGetIndividualDocTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividual(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Group
        [ActionName("GetGroupTempClear")]
        [HttpGet]
        public HttpResponseMessage GetGroupTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCADApplication.DaGetGroupTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GetGroupAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaGetGroupAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostGroupAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupAddressDetail(string group2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaDeleteGroupAddressDetail(group2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostGroupBankDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupBankDetail(string group2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaMstCADApplication.DaDeleteGroupBankDetail(group2bank_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGroupBankList")]
        [HttpGet]
        public HttpResponseMessage GetGroupBankList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaMstCADApplication.DaGetGroupBankList(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaGroupDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetGroupDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objDaMstCADApplication.DaGetGroupDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentDelete(string group2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objDaMstCADApplication.DaGroupDocumentDelete(group2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGroupDtlAdd")]
        [HttpPost]
        public HttpResponseMessage SubmitGroupDtlAdd(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaSubmitGroupDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupAddressTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaGroupAddressTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupAddressDetail(string group2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaEditGroupAddressDetail(group2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateGroupAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupBankTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupBankTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaMstCADApplication.DaGroupBankTmpList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupBankDetail(string group2bank_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaMstCADApplication.DaEditGroupBankDetail(group2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateGroupBankDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GroupDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentTmpList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objDaMstCADApplication.DaGroupDocumentTmpList(group_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGroupDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupDtl(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateGroupDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //ProductCharges
        [ActionName("GetProductChargesTempClear")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCADApplication.DaGetProductChargesTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetproductDropDown")]
        [HttpGet]
        public HttpResponseMessage GetproductDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductDropDown values = new MdlProductDropDown();
            objDaMstCADApplication.DaGetproductDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLimit")]
        [HttpGet]
        public HttpResponseMessage GetEditLimit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCADApplication.DaGetEditLimit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLoanDtl")]
        [HttpGet]
        public HttpResponseMessage GetEditLoanDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaMstCADApplication.DaGetEditLoanDtl(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LoanDetailList")]
        [HttpGet]
        public HttpResponseMessage LoanDetailList(string application_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaLoanDetailList(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BuyerDetailsList")]
        [HttpGet]
        public HttpResponseMessage BuyerDetailsList(string application2loan_gid)
        {
            MdlMstBuyer values = new MdlMstBuyer();
            objDaMstCADApplication.DaBuyerDetailsList(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CollateralDetailsList")]
        [HttpGet]
        public HttpResponseMessage CollateralDetailsList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaMstCADApplication.DaCollateralDetailsList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HypothecationDetailsList")]
        [HttpGet]
        public HttpResponseMessage HypothecationDetailsList(string application_gid)
        {
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaMstCADApplication.DaHypothecationDetailsList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditproduct")]
        [HttpGet]
        public HttpResponseMessage GetEditproduct(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlList values = new MdlList();
            objDaMstCADApplication.DaGetEditproduct(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductChargesEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesEdit(string application_gid)
        {
            MdlProductCharges values = new MdlProductCharges();
            objDaMstCADApplication.DaGetProductChargesEdit(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage GetLoanSubProduct(string loanproduct_gid, string application_gid, string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaMstCADApplication.DaGetLoanSubProduct(loanproduct_gid, application_gid, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostLoanEditDtl")]
        [HttpPost]
        public HttpResponseMessage PostLoanEditDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostLoanEditDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LoanTempDetailList")]
        [HttpGet]
        public HttpResponseMessage LoanTempDetailList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaMstCADApplication.DaLoanTempDetailList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteLoanDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteLoanDetail(string application2loan_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaMstCADApplication.DaDeleteLoanDetail(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBuyerInfo")]
        [HttpGet]
        public HttpResponseMessage GetBuyerInfo(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objDaMstCADApplication.DaGetBuyerInfo(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBuyer")]
        [HttpPost]
        public HttpResponseMessage PostBuyer(MdlMstBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BuyerTempDetailsList")]
        [HttpGet]
        public HttpResponseMessage BuyerTempDetailsList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objDaMstCADApplication.DaBuyerTempDetailsList(getsessionvalues.employee_gid, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BuyerDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage BuyerDetailsEdit(string application2buyer_gid)
        {
            MdlMstBuyer values = new MdlMstBuyer();
            objDaMstCADApplication.DaBuyerDetailsEdit(application2buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("BuyerDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage BuyerDetailsUpdate(MdlMstBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DBuyerDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteBuyerDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteBuyerDetails(string application2buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objDaMstCADApplication.DaDeleteBuyerDetails(application2buyer_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCollateral")]
        [HttpPost]
        public HttpResponseMessage PostCollateral(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostCollateral(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CollateralTempDetailsList")]
        [HttpGet]
        public HttpResponseMessage CollateralTempDetailsList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaMstCADApplication.DaCollateralTempDetailsList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CollateralDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage CollateralDetailsEdit(string application2collateral_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaMstCADApplication.DaCollateralDetailsEdit(application2collateral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CollateralDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage CollateralDocumentTempList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objDaMstCADApplication.DaCollateralDocumentTempList(getsessionvalues.employee_gid, application2loan_gid, objfilename);
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
            objDaMstCADApplication.DaEditcollateraldocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("deletecollateraldoc")]
        [HttpGet]
        public HttpResponseMessage deletecollateraldoc(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objDaMstCADApplication.Dadeletecollateraldoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }       
        [ActionName("CollateralDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage CollateralDetailsUpdate(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaCollateralDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteCollateralDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteCollateralDetails(string application2collateral_gid)
        {
            MdlMstCollatertal values = new MdlMstCollatertal();
            objDaMstCADApplication.DaDeleteCollateralDetails(application2collateral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HypothecationTempDetailsList")]
        [HttpGet]
        public HttpResponseMessage HypothecationTempDetailsList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaMstCADApplication.DaHypothecationTempDetailsList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HypothecationDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage HypothecationDetailsEdit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaMstCADApplication.DaHypothecationDetailsEdit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HypothecationDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage HypothecationDocumentTempList(string application2hypothecation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objDaMstCADApplication.DaHypothecationDocumentTempList(getsessionvalues.employee_gid, application2hypothecation_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
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
            objDaMstCADApplication.DaEditHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("deleteHypoDoc")]
        [HttpGet]
        public HttpResponseMessage deleteHypoDoc(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objDaMstCADApplication.DadeleteHypoDoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("HypothecationDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage HypothecationDetailsUpdate(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaHypothecationDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteHypothecationDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteHypothecationDetails(string application2hypothecation_gid)
        {
            MdlMstHypothecation values = new MdlMstHypothecation();
            objDaMstCADApplication.DaDeleteHypothecationDetails(application2hypothecation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("postcollateraldocument")]
        [HttpPost]
        public HttpResponseMessage postcollateraldocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objDaMstCADApplication.Dapostcollateraldocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("PostHypoDoc")]
        [HttpPost]
        public HttpResponseMessage PostHypoDoc()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Documentname documentname = new Documentname();
            objDaMstCADApplication.DaPostHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("UpdateProductCharges")]
        [HttpPost]
        public HttpResponseMessage UpdateProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateOverallLimit")]
        [HttpPost]
        public HttpResponseMessage UpdateOverallLimit(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateOverallLimit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteLoan")]
        [HttpPost]
        public HttpResponseMessage DeleteLoan(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            //  MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaMstCADApplication.DaDeleteLoan(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteCharge")]
        [HttpGet]
        public HttpResponseMessage DeleteCharge(string application2servicecharge_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductCharges values = new MdlProductCharges();
            objDaMstCADApplication.DaDeleteCharge(application2servicecharge_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditServiceCharges")]
        [HttpPost]
        public HttpResponseMessage PostEditServiceCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostEditServiceCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditLoanLimit")]
        [HttpPost]
        public HttpResponseMessage GetEditLoanLimit(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            // MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaMstCADApplication.GetEditLoanLimit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproduct")]
        [HttpGet]
        public HttpResponseMessage Getproduct(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlList values = new MdlList();
            objDaMstCADApplication.DaGetproduct(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LoanDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage LoanDetailsEdit(string application2loan_gid)
        {
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objDaMstCADApplication.DaLoanDetailsEdit(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CollateralDocumentList")]
        [HttpGet]
        public HttpResponseMessage CollateralDocumentList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objfilename = new Documentname();
            objDaMstCADApplication.DaCollateralDocumentList(application2loan_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("LoanDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage LoanDetailsUpdate(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaLoanDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ServicechargeEdit")]
        [HttpGet]
        public HttpResponseMessage ServicechargeEdit(string application2servicecharge_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductCharges values = new MdlProductCharges();
            objDaMstCADApplication.DaServicechargeEdit(application2servicecharge_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ServicechargeUpdate")]
        [HttpPost]
        public HttpResponseMessage ServicechargeUpdate(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaServicechargeUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadApplicationBasicView")]
        [HttpGet]
        public HttpResponseMessage GetCadApplicationBasicView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetCadApplicationBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGeneticDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetCadGeneticDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetCadGeneticDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadMobileMailDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetCadMobileMailDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetCadMobileMailDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadProductChargesDtl")]
        [HttpGet]
        public HttpResponseMessage GetCadProductChargesDtl(string application_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetCadProductChargesDtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetCadInstitutionList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstCADApplication.DaGetCadInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetCadIndividualList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstCADApplication.DaGetCadIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCadGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaMstCADApplication.DaGetCadGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetCadGrouptoMemberList(string group_gid)
        {
            MdlMstGroupMember values = new MdlMstGroupMember();
            objDaMstCADApplication.DaGetCadGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadRMDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetCadRMDetailsView(string application_gid)
        {
            MdlRMDtlView values = new MdlRMDtlView();
            objDaMstCADApplication.DaGetCadRMDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadVisitReportList")]
        [HttpGet]
        public HttpResponseMessage GetCadVisitReportList(string application_gid, string statusupdated_by)
        {

            MdlMstVisitPersonView values = new MdlMstVisitPersonView();
            objDaMstCADApplication.DaGetCadVisitReportList(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGradingToolDtls")]
        [HttpGet]
        public HttpResponseMessage GetCadGradingToolDtls(string application_gid, string statusupdated_by)
        {

            MdlMstGradeToolView values = new MdlMstGradeToolView();
            objDaMstCADApplication.DaGetCadGradingToolDtls(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGradingTool")]
        [HttpGet]
        public HttpResponseMessage GetCadGradingTool(string application_gid, string statusupdated_by)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objDaMstCADApplication.DaCadGetGradingTool(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditVisitReportList")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditVisitReportList(string application_gid, string statusupdated_by)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objDaMstCADApplication.DaGetCadCreditVisitReportList(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadLoantoBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCadLoantoBuyerList(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetCadLoantoBuyerList(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadLoanProgramValueChain")]
        [HttpGet]
        public HttpResponseMessage GetCadLoanProgramValueChain(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetCadLoanProgramValueChain(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadPurposeofLoan")]
        [HttpGet]
        public HttpResponseMessage GetCadPurposeofLoan(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objDaMstCADApplication.DaGetCadPurposeofLoan(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadAppLimitInfoDtl")]
        [HttpGet]
        public HttpResponseMessage GetCadAppLimitInfoDtl(string application_gid)
        {
            limitandproductslist objlsamgmt = new limitandproductslist();
            objDaMstCADApplication.DaGetCadAppLimitInfoDtl(objlsamgmt, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("GetCadGurantorInstitutionView")]
        [HttpGet]
        public HttpResponseMessage GetCadGurantorInstitutionView(string institution_gid)
        {

            MdlMstInstitutionDtlView values = new MdlMstInstitutionDtlView();
            objDaMstCADApplication.DaGetCadGurantorInstitutionView(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadInstitutionBureauList")]
        [HttpGet]
        public HttpResponseMessage GetCadInstitutionBureauList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlInstitutionBureau values = new MdlInstitutionBureau();
            objDaMstCADApplication.DaGetCadInstitutionBureauList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditOperationsView")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditOperationsView(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaMstCADApplication.DaGetCadCreditOperationsView(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetCadGeneticCodeList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWGeneticCode values = new MdlMstCUWGeneticCode();
            objDaMstCADApplication.DaGetCadGeneticCodeList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadSocialAndTradeCapital")]
        [HttpGet]
        public HttpResponseMessage GetCadSocialAndTradeCapital(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaMstCADApplication.DaGetCadSocialAndTradeCapital(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadPSLDataFlagging")]
        [HttpGet]
        public HttpResponseMessage GetCadPSLDataFlagging(string credit_gid, string applicant_type)
        {
            MdlMstAppCreditUnderWriting values = new MdlMstAppCreditUnderWriting();
            objDaMstCADApplication.DaGetCadPSLDataFlagging(credit_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditSupplierList")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditSupplierList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSupplier values = new MdlMstSupplier();
            objDaMstCADApplication.DaGetCadCreditSupplierList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditBuyerList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objDaMstCADApplication.DaGetCadCreditBuyerList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCrediBankAcctList")]
        [HttpGet]
        public HttpResponseMessage GetCadCrediBankAcctList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditBankAcc values = new MdlCreditBankAcc();
            objDaMstCADApplication.DaGetCadCrediBankAcctList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadExistingBankFacility")]
        [HttpGet]
        public HttpResponseMessage GetCadExistingBankFacility(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWExistingBankFacility values = new MdlMstCUWExistingBankFacility();
            objDaMstCADApplication.DaGetCadExistingBankFacility(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadRepaymentTrack")]
        [HttpGet]
        public HttpResponseMessage GetCadRepaymentTrack(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCUWRepaymentTrack values = new MdlMstCUWRepaymentTrack();
            objDaMstCADApplication.DaGetCadRepaymentTrack(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditObservationList")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditObservationList(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditObservation values = new MdlMstCreditObservation();
            objDaMstCADApplication.DaGetCadCreditObservationList(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditRepaymentDtlRemarks")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditRepaymentDtlRemarks(string creditrepaymentdtl_gid)
        {
            MdlMstRepaymentRemarks values = new MdlMstRepaymentRemarks();
            objDaMstCADApplication.DaGetCadCreditRepaymentDtlRemarks(creditrepaymentdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditExistingBankDtlRemarks")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditExistingBankDtlRemarks(string existingbankfacility_gid)
        {
            MdlMstExistingRemarks values = new MdlMstExistingRemarks();
            objDaMstCADApplication.DaGetCadCreditExistingBankDtlRemarks(existingbankfacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditSupplierTextData")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditSupplierTextData(string creditsupplier_gid)
        {
            MdlMstSupplier values = new MdlMstSupplier();
            objDaMstCADApplication.DaGetCadCreditSupplierTextData(creditsupplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditBuyerTextData")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditBuyerTextData(string creditbuyer_gid)
        {
            MdlMstCreditBuyer values = new MdlMstCreditBuyer();
            objDaMstCADApplication.DaGetCadCreditBuyerTextData(creditbuyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCreditBankDocumentUpload")]
        [HttpGet]
        public HttpResponseMessage GetCadCreditBankDocumentUpload(string creditbankdtl_gid)
        {
            credituploaddocument values = new credituploaddocument();
            objDaMstCADApplication.DaGetCadCreditBankDocumentUpload(creditbankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCICInstitutionDtl")]
        [HttpGet]
        public HttpResponseMessage GetCadCICInstitutionDtl(string institution2bureau_gid)
        {
            MdlCICInstitution values = new MdlCICInstitution();
            objDaMstCADApplication.DaGetCadCICInstitutionDtl(institution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CadCICUploadInstitutionDocList")]
        [HttpGet]
        public HttpResponseMessage CadCICUploadInstitutionDocList(string institution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objDaMstCADApplication.DaCadCICUploadInstitutionDocList(institution2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGurantorIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetCadGurantorIndividualView(string contact_gid)
        {

            MdlMstIndividualDtlView values = new MdlMstIndividualDtlView();
            objDaMstCADApplication.DaGetCadGurantorIndividualView(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadContactBureauList")]
        [HttpGet]
        public HttpResponseMessage GetCadContactBureauList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objDaMstCADApplication.DaGetCadContactBureauList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCICIndividualDtl")]
        [HttpGet]
        public HttpResponseMessage GetCadCICIndividualDtl(string contact2bureau_gid)
        {
            MdlCICIndividual values = new MdlCICIndividual();
            objDaMstCADApplication.DaGetCadCICIndividualDtl(contact2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadCICUploadIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetCadCICUploadIndividualDocList(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objDaMstCADApplication.DaGetCadCICUploadIndividualDocList(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGroupView")]
        [HttpGet]
        public HttpResponseMessage GetCadGroupView(string group_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaMstCADApplication.DaGetCadGroupView(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GetCadGroupAddressList(string group_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objDaMstCADApplication.DaGetCadGroupAddressList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGroupBankList")]
        [HttpGet]
        public HttpResponseMessage GetCadGroupBankList(string group_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objDaMstCADApplication.DaGetCadGroupBankList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetCadGroupDocumentList(string group_gid)
        {
            MdlGroupDocument values = new MdlGroupDocument();
            objDaMstCADApplication.DaGetCadGroupDocumentList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadVisitReportDtls")]
        [HttpGet]
        public HttpResponseMessage GetCadVisitReportDtls(string visitreport_gid)
        {

            MdlMstVisitPersonView values = new MdlMstVisitPersonView();
            objDaMstCADApplication.DaGetCadVisitReportDtls(visitreport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadVisitContactList")]
        [HttpGet]
        public HttpResponseMessage GetCadVisitContactList(string applicationvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaMstCADApplication.DaGetCadVisitContactList(getsessionvalues.employee_gid, applicationvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadGradingView")]
        [HttpGet]
        public HttpResponseMessage GetCadGradingView(string application2gradingtool_gid)
        {
            MdlMstGradeToolView values = new MdlMstGradeToolView();
            objDaMstCADApplication.DaGetCadGradingView(application2gradingtool_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objDaMstCADApplication.DaGetAppProductList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductDetailAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDetailAdd(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostProductDetailAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objDaMstCADApplication.DaGetAppProductTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAppProductDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteAppProductDtl(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailAdd values = new MdlMstProductDetailAdd();
            objDaMstCADApplication.DaDeleteAppProductDtl(application2product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Institution Equipment Holding
        [ActionName("PostInstitutionEquipmentHolding")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEquipmentHolding(MdlMstEquipmentHolding values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionEquipmentHolding(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Institution Equipment Holding
        [ActionName("DeleteInstitutionEquipmentHolding")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionEquipmentHolding(string institution2equipment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaDeleteInstitutionEquipmentHolding(institution2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Institution Equipment Holding
        [ActionName("GetEquipmentHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetEquipmentHoldingView(string institution2equipment_gid)
        {
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetEquipmentHoldingView(institution2equipment_gid, values);
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
            objDaMstCADApplication.DaGetInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Institution Livestock Holding
        [ActionName("PostInstitutionLivestock")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLivestock(MdlMstLivestock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionLivestock(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaGetInstitutionLivestockList(getsessionvalues.employee_gid, institution_gid, values);
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
            objDaMstCADApplication.DaDeleteInstitutionLivestock(institution2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Institution Livestock Holding
        [ActionName("GetLivestockHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetLivestockHoldingView(string institution2livestock_gid)
        {
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetLivestockHoldingView(institution2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaGetEditInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, institution_gid, values);
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
            objDaMstCADApplication.DaGetEditInstitutionLivestockList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Contact Equipment Holding
        [ActionName("PostContactEquipmentHolding")]
        [HttpPost]
        public HttpResponseMessage PostContactEquipmentHolding(MdlMstEquipmentHolding values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostContactEquipmentHolding(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Contact Equipment Holding List
        [ActionName("GetContactEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetContactEquipmentHoldingList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetContactEquipmentHoldingList(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaDeleteContactEquipmentHolding(contact2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //View Contact Equipment Holding
        [ActionName("GetContactEquipmentHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetContactEquipmentHoldingView(string contact2equipment_gid)
        {
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetContactEquipmentHoldingView(contact2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Contact Livestock Holding
        [ActionName("PostContactLivestock")]
        [HttpPost]
        public HttpResponseMessage PostContactLivestock(MdlMstLivestock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostContactLivestock(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Contact Livestock Holding
        [ActionName("GetContactLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetContactLivestockList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetContactLivestockList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Contact Livestock Holding
        [ActionName("DeleteContactLivestock")]
        [HttpGet]
        public HttpResponseMessage DeleteContactLivestock(string contact2livestock_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaDeleteContactLivestock(contact2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Contact Livestock Holding
        [ActionName("GetContactLivestockHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetContactLivestockHoldingView(string contact2livestock_gid)
        {
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetContactLivestockHoldingView(contact2livestock_gid, values);
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
            objDaMstCADApplication.DaGetEditContactEquipmentHoldingList(getsessionvalues.employee_gid, contact_gid, values);
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
            objDaMstCADApplication.DaGetEditContactLivestockList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Add Institution Equipment Holding
        [ActionName("GetAddInstitutionEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEquipmentHoldingList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetAddInstitutionEquipmentHoldingList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Add Institution Livestock Holding
        [ActionName("GetAddInstitutionLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionLivestockList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetAddInstitutionLivestockList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Add Contact Equipment Holding
        [ActionName("GetEditAddContactEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetContactEquipmentHoldingList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetEditAddContactEquipmentHoldingList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Edit Add Contact Livestock Holding
        [ActionName("GetEditAddContactLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetContactLivestockList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetEditAddContactLivestockList(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Group Equipment Holding
        [ActionName("PostGroupEquipmentHolding")]
        [HttpPost]
        public HttpResponseMessage PostGroupEquipmentHolding(MdlMstEquipmentHolding values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostGroupEquipmentHolding(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Group Equipment Holding List
        [ActionName("GetGroupEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetGroupEquipmentHoldingList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetGroupEquipmentHoldingList(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaDeleteGroupEquipmentHolding(group2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //View Group Equipment Holding
        [ActionName("GetGroupEquipmentHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetGroupEquipmentHoldingView(string group2equipment_gid)
        {
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetGroupEquipmentHoldingView(group2equipment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Group Livestock Holding
        [ActionName("PostGroupLivestock")]
        [HttpPost]
        public HttpResponseMessage PostGroupLivestock(MdlMstLivestock values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostGroupLivestock(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Group Livestock Holding List
        [ActionName("GetGroupLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetGroupLivestockList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetGroupLivestockList(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaDeleteGroupLivestock(group2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //View Group Livestock Holding
        [ActionName("GetGroupLivestockHoldingView")]
        [HttpGet]
        public HttpResponseMessage GetGroupLivestockHoldingView(string group2livestock_gid)
        {
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetGroupLivestockHoldingView(group2livestock_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Group Edit Add Equipment Holding
        [ActionName("GetEditAddGroupEquipmentHoldingList")]
        [HttpGet]
        public HttpResponseMessage GetGroupEquipmentHoldingList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEquipmentHolding values = new MdlMstEquipmentHolding();
            objDaMstCADApplication.DaGetEditAddGroupEquipmentHoldingList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Group Edit Add Livestock Holding
        [ActionName("GetEditAddGroupLivestockList")]
        [HttpGet]
        public HttpResponseMessage GetGroupLivestockList(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLivestock values = new MdlMstLivestock();
            objDaMstCADApplication.DaGetEditAddGroupLivestockList(getsessionvalues.employee_gid, group_gid, values);
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
            objDaMstCADApplication.DaGetEditGroupEquipmentHoldingList(getsessionvalues.employee_gid, group_gid, values);
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
            objDaMstCADApplication.DaGetEditGroupLivestockList(getsessionvalues.employee_gid, group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppLoanProductList")]
        [HttpGet]
        public HttpResponseMessage GetAppLoanProductList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objDaMstCADApplication.DaGetAppLoanProductList(application2loan_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductDtlAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDtlAdd(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostProductDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductDtlList")]
        [HttpGet]
        public HttpResponseMessage GetProductDtlList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objDaMstCADApplication.DaGetProductDtlList(application2loan_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Institution Receivable
        [ActionName("PostInstitutionReceivable")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionReceivable(MdlMstReceivable values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostInstitutionReceivable(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Institution Receivable List
        [ActionName("GetInstitutionReceivableList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionReceivableList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstReceivable values = new MdlMstReceivable();
            objDaMstCADApplication.DaGetInstitutionReceivableList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Delete Institution Receivable
        [ActionName("DeleteInstitutionReceivable")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionReceivable(string institution2receivable_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstReceivable values = new MdlMstReceivable();
            objDaMstCADApplication.DaDeleteInstitutionReceivable(institution2receivable_gid, values);
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
            objDaMstCADApplication.DaGetInstitutionReceivableList(getsessionvalues.employee_gid, institution_gid, values);
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
            objDaMstCADApplication.DaGetEditInstitutionReceivableList(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Account Type
        [ActionName("GetGuaranteeProgramType")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeProgramType()
        {
            MdlGuaranteeProgramType values = new MdlGuaranteeProgramType();
            objDaMstCADApplication.DaGetGuaranteeProgramType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostGuaranteeDtlAdd")]
        [HttpPost]
        public HttpResponseMessage PostGuaranteeDtlAdd(MdlGuaranteeDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostGuaranteeDtlAdd(getsessionvalues.employee_gid, values);
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
            objDaMstCADApplication.DaGuaranteeDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
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
            objDaMstCADApplication.DaGuaranteeDocTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionGuaranteeDtl")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionGuaranteeDtl(string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objDaMstCADApplication.DaGetInstitutionGuaranteeDtl(credit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGuaranteeDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteGuaranteeDtl(string cadcreditguaranteedtl_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objDaMstCADApplication.DaDeleteGuaranteeDtl(cadcreditguaranteedtl_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGuaranteeDtlDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteGuaranteeDtlDocument(string cadcreditguaranteedtldocument_gid, string credit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            GuaranteeDocumentUpload values = new GuaranteeDocumentUpload();
            objDaMstCADApplication.DaDeleteGuaranteeDtlDocument(cadcreditguaranteedtldocument_gid, credit_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGuaranteeRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeRemarksView(string cadcreditguaranteedtl_gid)
        {
            MdlGuaranteeDtl values = new MdlGuaranteeDtl();
            objDaMstCADApplication.DaGetGuaranteeRemarksView(cadcreditguaranteedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGuaranteeDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeDocDtl(string cadcreditguaranteedtl_gid)
        {

            GuaranteeDocumentUpload values = new GuaranteeDocumentUpload();
            objDaMstCADApplication.DaGetGuaranteeDocDtl(cadcreditguaranteedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardAppValidatePANAadhar")]
        [HttpPost]
        public HttpResponseMessage GetOnboardAppValidatePANAadhar(MdlonboardValidatedtl values)
        {
            objDaMstCADApplication.DaGetOnboardAppValidatePANAadhar(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRenewalAppValidatePANAadhar")]
        [HttpPost]
        public HttpResponseMessage DaGetRenewalAppValidatePANAadhar(MdlonboardValidatedtl values)
        {
            objDaMstCADApplication.DaGetRenewalAppValidatePANAadhar(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerCreateLMSView")]
        [HttpGet]
        public HttpResponseMessage GetCustomerCreateLMSView(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetCustomerCreateLMSView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getbankaccno")]
        [HttpGet]
        public HttpResponseMessage Getbankaccno(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetbankaccno(values,application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getgstno")]
        [HttpGet]
        public HttpResponseMessage Getgstno(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetgstno(values,application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerCreatebankdetails")]
        [HttpGet]
        public HttpResponseMessage GetCustomerCreatebankdetails(string bankaccount_number)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetCustomerCreatebankdetails(bankaccount_number, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCustomerCreationLMS")]
        [HttpPost]
        public HttpResponseMessage PostCustomerCreationLMS(MdlcustomercreationLMS values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostCustomerCreationLMS(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerInitiatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerInitiatedSummary()
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaGetCustomerInitiatedSummary(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCustomerURN")]
        [HttpPost]
        public HttpResponseMessage PostCustomerURN(MdlcustomercreationLMS values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostCustomerURN(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getgstbankaccnodetails")]
        [HttpGet]
        public HttpResponseMessage Getgstbankaccnodetails(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetgstbankaccnodetails(application_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCustomerlmsreject")]
        [HttpPost]
        public HttpResponseMessage PostCustomerlmsreject(MdlcustomercreationLMS values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaPostCustomerlmsreject(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerUpdatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerUpdatedSummary()
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaGetCustomerUpdatedSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerUpdatingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerUpdatingSummary(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaGetCustomerUpdatingSummary(values,application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerRejectedSummary()
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaGetCustomerRejectedSummary(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerRejectingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCustomerRejectingSummary(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCADApplication.DaGetCustomerRejectingSummary(values,application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExportLmsReport2")]
        [HttpGet]
        public HttpResponseMessage GetExportLmsReport2(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetExportLmsReport2(values,application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetExportLmsReport1")]
        [HttpGet]
        public HttpResponseMessage GetExportLmsReport1(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetExportLmsReport1(values,application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getlogstatusdetails")]
        [HttpGet]
        public HttpResponseMessage Getlogstatusdetails(string application_gid)
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetlogstatusdetails(application_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcount")]
        [HttpGet]
        public HttpResponseMessage Getcount()
        {
            MdlcustomercreationLMS values = new MdlcustomercreationLMS();
            objDaMstCADApplication.DaGetcount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetLoanDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanDetailsUrnView")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetailsUrnView(string customer_urn)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objDaMstCADApplication.DaGetLoanDetailsUrnView(customer_urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualDocListAdd")]
        [HttpGet]
        public HttpResponseMessage IndividualDocTempList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objDaMstCADApplication.DaGetIndividualDocListAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
    

}