using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;
using System.Web;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to add single and multiple datas in Supplier Onboard stage.  (Includes overall summary adding of onboarded buyer general, company & individual info & initiate, approve & reject records)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K </remarks>

    [RoutePrefix("api/AgrMstSupplierOnboard")]
    [Authorize]
    public class AgrMstSupplierOnboardController : ApiController 
    {
        DaAgrMstSupplierOnboard objDaAgrMstSupplierOnboard = new DaAgrMstSupplierOnboard();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();  

        [ActionName("GetGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetGeneralInfo()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardApplicationAdd values = new MdlMstBuyerOnboardApplicationAdd();
            objDaAgrMstSupplierOnboard.DaGetGeneralInfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("GetIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardCICIndividual values = new MdlBuyerOnboardCICIndividual();
            objDaAgrMstSupplierOnboard.DaGetIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardCICInstitution values = new MdlBuyerOnboardCICInstitution();
            objDaAgrMstSupplierOnboard.DaGetInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardApplicationGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetOnboardApplicationGeneralInfo(string application_gid)
        {
            MdlMstOnboardApplicationView values = new MdlMstOnboardApplicationView();
            objDaAgrMstSupplierOnboard.DaGetOnboardApplicationGeneralInfo(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardIndividualInfo")]
        [HttpGet]
        public HttpResponseMessage GetOnboardIndividualInfo(string application_gid)
        {

            MdlMstOnboardIndividual values = new MdlMstOnboardIndividual();
            objDaAgrMstSupplierOnboard.DaGetOnboardIndividualInfo(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardInstitutionInfo")]
        [HttpGet]
        public HttpResponseMessage GetOnboardInstitutionInfo(string application_gid)
        {

            MdlMstOnboardInstitution values = new MdlMstOnboardInstitution();
            objDaAgrMstSupplierOnboard.DaGetOnboardInstitutionInfo(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardInstitutionView")]
        [HttpGet]
        public HttpResponseMessage GetOnboardInstitutionView(string institution_gid)
        {

            MdlMstInstitutionDtlView values = new MdlMstInstitutionDtlView();
            objDaAgrMstSupplierOnboard.DaGetOnboardInstitutionView(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetGurantorIndividualView(string contact_gid)
        {

            MdlMstIndividualDtlView values = new MdlMstIndividualDtlView();
            objDaAgrMstSupplierOnboard.DaGetGurantorIndividualView(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
          
        [ActionName("GetBuyerApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetBuyerApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetBuyerApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBuyerApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetBuyerApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetBuyerApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBuyerRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetBuyerRejectedSummary(string FromRM)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetBuyerRejectedSummary(FromRM, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetSupplierApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSupplierApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetSupplierApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupplierApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSupplierApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetSupplierApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupplierRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSupplierRejectedSummary(string FromRM)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetSupplierRejectedSummary(FromRM, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMApprovalCountDetail")]
        [HttpGet]
        public HttpResponseMessage GetRMApprovalCountDetail()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOnboardApprovalCountdtl values = new MdlOnboardApprovalCountdtl();
            objDaAgrMstSupplierOnboard.DaGetRMApprovalCountDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApproverPendingCountDetail")]
        [HttpGet]
        public HttpResponseMessage GetApproverPendingCountDetail()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOnboardApprovalCountdtl values = new MdlOnboardApprovalCountdtl();
            objDaAgrMstSupplierOnboard.DaGetApproverPendingCountDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApproverApprovedCountDetail")]
        [HttpGet]
        public HttpResponseMessage GetApproverApprovedCountDetail()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOnboardApprovalCountdtl values = new MdlOnboardApprovalCountdtl();
            objDaAgrMstSupplierOnboard.DaGetApproverApprovedCountDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRejectedCountDetail")]
        [HttpGet]
        public HttpResponseMessage GetRejectedCountDetail(string FromRM)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOnboardApprovalCountdtl values = new MdlOnboardApprovalCountdtl();
            objDaAgrMstSupplierOnboard.DaGetRejectedCountDetail(FromRM, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Customer Approval Onboarding

        [ActionName("GetBuyerOnboardingApprovalPending")]
        [HttpGet]
        public HttpResponseMessage GetBuyerOnboardingApprovalPending()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetBuyerOnboardingApprovalPending(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprOnboardingApprovalPending")]
        [HttpGet]
        public HttpResponseMessage GetSuprOnboardingApprovalPending()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetSuprOnboardingApprovalPending(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBuyerOnboardApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetBuyerOnboardApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetBuyerOnboardApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSupplierOnboardApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSupplierOnboardApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstOnboardApplicationlist values = new MdlMstOnboardApplicationlist();
            objDaAgrMstSupplierOnboard.DaGetSupplierOnboardApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("PostSupplierOnboardApproval")]
        [HttpPost]
        public HttpResponseMessage PostSupplierOnboardApproval(MdlOnboardApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostSupplierOnboardApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInitiateSupplierApplication")]
        [HttpPost]
        public HttpResponseMessage PostInitiateSupplierApplication(MdlOnboardApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInitiateSupplierApplication(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProductDetailAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDetailAdd(MdlMstBuyerOnboardProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostProductDetailAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductDetailList")]
        [HttpGet]
        public HttpResponseMessage GetProductDetailList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardProductDetailList values = new MdlMstBuyerOnboardProductDetailList();
            objDaAgrMstSupplierOnboard.DaGetProductDetailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteProductDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteProductDetail(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardProductDetailAdd values = new MdlMstBuyerOnboardProductDetailAdd();
            objDaAgrMstSupplierOnboard.DaDeleteProductDetail(application2product_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostMobileNo(MdlMstBuyerOnboardMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboard.DaGetAppMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete Mobile No----------//
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage deleteMmobileno(string application2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboard.DaDeleteMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Contact Person Email Address

        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlMstBuyerOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboard.DaGetAppEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboard.DaDeleteEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code
        [ActionName("PostGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostGeneticCode(MdlMstBuyerOnboardGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGenetic")]
        [HttpGet]
        public HttpResponseMessage DeleteGenetic(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardGeneticCode values = new MdlMstBuyerOnboardGeneticCode();
            objDaAgrMstSupplierOnboard.DaDeleteGenetic(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitGeneralDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitGeneralDtl(MdlMstBuyerOnboardApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaSubmitGeneralDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlMstBuyerOnboardGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGSTList")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGSTList(MdlMstBuyerOnboardGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution GST List

        [ActionName("GetInstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionGSTList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardGST values = new MdlMstBuyerOnboardGST();
            objDaAgrMstSupplierOnboard.DaGetInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution GST

        [ActionName("EditInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionGST(string institution2branch_gid)
        {
            MdlMstBuyerOnboardGST values = new MdlMstBuyerOnboardGST();
            objDaAgrMstSupplierOnboard.DaEditInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution GST

        [ActionName("UpdateInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionGST(MdlMstBuyerOnboardGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution GST

        [ActionName("DeleteInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionGST(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardGST values = new MdlMstBuyerOnboardGST();
            objDaAgrMstSupplierOnboard.DaDeleteInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGSTInstitution")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTInstitution(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardGST values = new MdlMstBuyerOnboardGST();
            objDaAgrMstSupplierOnboard.DaDeleteGSTInstitution(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Mobile No

        [ActionName("PostInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionMobileNo(MdlMstBuyerOnboardMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Mobile No

        [ActionName("GetInstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboard.DaGetInstitutionMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Mobile No

        [ActionName("EditInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionMobileNo(string institution2mobileno_gid)
        {
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboard.DaEditInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Institution Mobile No

        [ActionName("UpdateInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionMobileNo(MdlMstBuyerOnboardMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Mobile No

        [ActionName("DeleteInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionMobileNo(string institution2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboard.DaDeleteInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Email Address

        [ActionName("PostInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEmailAddress(MdlMstBuyerOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Email Address List

        [ActionName("GetInstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboard.DaGetInstitutionEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Email Address

        [ActionName("EditInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionEmailAddress(string institution2email_gid)
        {
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboard.DaEditInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionEmailAddress(MdlMstBuyerOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Email Address

        [ActionName("DeleteInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionEmailAddress(string institution2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboard.DaDeleteInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Address Details  

        [ActionName("PostInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionAddressDetail(MdlMstBuyerOnboardAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Address List

        [ActionName("GetInstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardAddressDetails values = new MdlMstBuyerOnboardAddressDetails();
            objDaAgrMstSupplierOnboard.DaGetInstitutionAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Address Details 

        [ActionName("EditInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionAddressDetail(string institution2address_gid)
        {
            MdlMstBuyerOnboardAddressDetails values = new MdlMstBuyerOnboardAddressDetails();
            objDaAgrMstSupplierOnboard.DaEditInstitutionAddressDetail(institution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionAddressDetail(MdlMstBuyerOnboardAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateInstitutionAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Address Details 

        [ActionName("DeleteInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionAddressDetail(string institution2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardAddressDetails values = new MdlMstBuyerOnboardAddressDetails();
            objDaAgrMstSupplierOnboard.DaDeleteInstitutionAddressDetail(institution2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution License Details

        [ActionName("PostInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLicenseDetail(MdlMstBuyerOnboardLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionLicenseDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution License List

        [ActionName("GetInstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionLicenseList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardLicenseDetails values = new MdlMstBuyerOnboardLicenseDetails();
            objDaAgrMstSupplierOnboard.DaGetInstitutionLicenseList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution License Details 

        [ActionName("EditInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            MdlMstBuyerOnboardLicenseDetails values = new MdlMstBuyerOnboardLicenseDetails();
            objDaAgrMstSupplierOnboard.DaEditInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution License Details 

        [ActionName("UpdateInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionLicenseDetail(MdlMstBuyerOnboardLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateInstitutionLicenseDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution License Details 

        [ActionName("DeleteInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardLicenseDetails values = new MdlMstBuyerOnboardLicenseDetails();
            objDaAgrMstSupplierOnboard.DaDeleteInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document Upload

        [ActionName("InstitutionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            BuyerOnboardinstitutionuploaddocument documentname = new BuyerOnboardinstitutionuploaddocument();
            objDaAgrMstSupplierOnboard.DaInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Institution Document Delete

        [ActionName("InstitutionDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentDelete(string institution2documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            BuyerOnboardinstitutionuploaddocument objfilename = new BuyerOnboardinstitutionuploaddocument();
            objDaAgrMstSupplierOnboard.DaInstitutionDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        [ActionName("PostRatingdtl")]
        [HttpPost]
        public HttpResponseMessage PostRatingdtl(MdlBuyerOnboardRatingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostRatingdtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionRatingList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionRatingList(string institution_gid, string tmp_status)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardRatingList values = new MdlBuyerOnboardRatingList();
            objDaAgrMstSupplierOnboard.DaGetInstitutionRatingList(institution_gid, getsessionvalues.employee_gid, tmp_status, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRatingDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteRatingDtl(string institution2ratingdetail_gid)
        {
            result values = new result();
            objDaAgrMstSupplierOnboard.DaDeleteRatingDtl(institution2ratingdetail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionBank")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionBank(MdlBuyerOnboardInstitution2BankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostInstitutionBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetInstitutionBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionBankAccDtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardInstitution2BankAcc values = new MdlBuyerOnboardInstitution2BankAcc();
            objDaAgrMstSupplierOnboard.DaGetInstitutionBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Institution2bankTmpList")]
        [HttpGet]
        public HttpResponseMessage Institution2bankTmpList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardInstitution2BankAcc values = new MdlBuyerOnboardInstitution2BankAcc();
            objDaAgrMstSupplierOnboard.DaInstitution2bankTmpList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetCreditBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseAddressDetail(string institution2bankdtl_gid)
        {
            MdlBuyerOnboardInstitution2BankAcc values = new MdlBuyerOnboardInstitution2BankAcc();
            objDaAgrMstSupplierOnboard.DaEditGetCreditBankAccDtl(institution2bankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateInstitutionBankAccDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionBankAccDtl(MdlBuyerOnboardInstitution2BankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateInstitutionBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteinstitutionBankAcc")]
        [HttpGet]
        public HttpResponseMessage DeleteinstitutionBankAcc(string institution2bankdtl_gid, string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardInstitution2BankAcc values = new MdlBuyerOnboardInstitution2BankAcc();
            objDaAgrMstSupplierOnboard.DaDeleteinstitutionBankAcc(institution2bankdtl_gid, institution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtl(MdlMstBuyerOnboardInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaSubmitInstitutionDtl(values, getsessionvalues.employee_gid);
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
            BuyerOnboarduploaddocument documentname = new BuyerOnboarduploaddocument();
            objDaAgrMstSupplierOnboard.DaPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("PANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage PANForm60Delete(string contact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactPANForm60 values = new MdlBuyerOnboardContactPANForm60();
            objDaAgrMstSupplierOnboard.DaPANForm60Delete(contact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactPANForm60 values = new MdlBuyerOnboardContactPANForm60();
            objDaAgrMstSupplierOnboard.DaGetPANForm60List(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetEditPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactPANForm60 values = new MdlBuyerOnboardContactPANForm60();
            objDaAgrMstSupplierOnboard.DaGetEditPANForm60List(getsessionvalues.employee_gid, contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage PANAbsenceReasonList()
        {
            MdlBuyerOnboardPANAbsenceReason objMdlPANAbsenceReason = new MdlBuyerOnboardPANAbsenceReason();
            objDaAgrMstSupplierOnboard.DaPANAbsenceReasonList(objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("PostPANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage PostPANAbsenceReasons(MdlBuyerOnboardPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostPANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PANReasonsCheck")]
        [HttpGet]
        public HttpResponseMessage PANReasonsCheck()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardPANAbsenceReason values = new MdlBuyerOnboardPANAbsenceReason();
            objDaAgrMstSupplierOnboard.DaPANReasonsCheck(values, getsessionvalues.employee_gid);
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
            BuyerOnboarduploaddocument documentname = new BuyerOnboarduploaddocument();
            objDaAgrMstSupplierOnboard.DaIndividualProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactIdProof values = new MdlBuyerOnboardContactIdProof();
            objDaAgrMstSupplierOnboard.DaGetIndividualProofList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualProofDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualProofDelete(string contact2idproof_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactIdProof values = new MdlBuyerOnboardContactIdProof();
            objDaAgrMstSupplierOnboard.DaIndividualProofDelete(contact2idproof_gid, values);
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
            BuyerOnboarduploaddocument documentname = new BuyerOnboarduploaddocument();
            objDaAgrMstSupplierOnboard.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage IndividualDocTempList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactDocument values = new MdlBuyerOnboardContactDocument();
            objDaAgrMstSupplierOnboard.DaGetIndividualDocList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualDocListEdit")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocListEdit(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactDocument values = new MdlBuyerOnboardContactDocument();
            objDaAgrMstSupplierOnboard.DaGetIndividualDocListEdit(getsessionvalues.employee_gid,contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactDocument values = new MdlBuyerOnboardContactDocument();
            objDaAgrMstSupplierOnboard.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MobileNumberAdd")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAdd(MdlBuyerOnboardContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaMobileNumberAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Mobile No List
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactMobileNo values = new MdlBuyerOnboardContactMobileNo();
            objDaAgrMstSupplierOnboard.DaGetMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Mobile No----------//
        [ActionName("MobileNoDelete")]
        [HttpGet]
        public HttpResponseMessage MobileNoDelete(string contact2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactMobileNo values = new MdlBuyerOnboardContactMobileNo();
            objDaAgrMstSupplierOnboard.DaMobileNoDelete(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Add 
        [ActionName("EmailAddressAdd")]
        [HttpPost]
        public HttpResponseMessage EmailAddressAdd(MdlBuyerOnboardContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaEmailAddressAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Email Address List
        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactEmail values = new MdlBuyerOnboardContactEmail();
            objDaAgrMstSupplierOnboard.DaGetEmailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmailAddressDelete")]
        [HttpGet]
        public HttpResponseMessage EmailAddressDelete(string contact2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactEmail values = new MdlBuyerOnboardContactEmail();
            objDaAgrMstSupplierOnboard.DaEmailAddressDelete(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("AddressAdd")]
        [HttpPost]
        public HttpResponseMessage AddressAdd(MdlBuyerOnboardContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaAddressAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactAddress values = new MdlBuyerOnboardContactAddress();
            objDaAgrMstSupplierOnboard.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddressDelete")]
        [HttpGet]
        public HttpResponseMessage AddressDelete(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactAddress values = new MdlBuyerOnboardContactAddress();
            objDaAgrMstSupplierOnboard.DaAddressDelete(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualSubmit")]
        [HttpPost]
        public HttpResponseMessage IndividualSubmit(MdlMstBuyerOnboardContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaIndividualSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("suprronboardTmpClear")]
        [HttpGet]
        public HttpResponseMessage suprronboardTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardApplicationAdd values = new MdlMstBuyerOnboardApplicationAdd();
            objDaAgrMstSupplierOnboard.DasuprronboardTmpClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 
        [ActionName("GetSupplierOnboardSubmitApproval")]
        [HttpGet]
        public HttpResponseMessage GetSupplierOnboardSubmitApproval(string application_gid)
        {
            result values = new result();
            objDaAgrMstSupplierOnboard.DaGetSupplierOnboardSubmitApproval(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
 
        [ActionName("GetonboardTempClear")]
        [HttpGet]
        public HttpResponseMessage GetonboardTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardApplicationAdd values = new MdlMstBuyerOnboardApplicationAdd();
            objDaAgrMstSupplierOnboard.DaGetonboardTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompanyList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyList(string application_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objDaAgrMstSupplierOnboard.DaGetCompanyList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetsuprInstitutionRatingList")]
        [HttpGet]
        public HttpResponseMessage GetsuprInstitutionRatingList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRatingList values = new MdlRatingList();
            objDaAgrMstSupplierOnboard.DaGetsuprInstitutionRatingList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetsuprPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetsuprPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objDaAgrMstSupplierOnboard.DaGetsuprPANForm60List(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deleteinstitution")]
        [HttpGet]
        public HttpResponseMessage Deleteinstitution(string institution_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objDaAgrMstSupplierOnboard.DaDeleteinstitution(institution_gid, application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deleteindividual")]
        [HttpGet]
        public HttpResponseMessage Deleteindividual(string contact_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objDaAgrMstSupplierOnboard.DaDeleteindividual(contact_gid, application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostIndividualBank")]
        [HttpPost]
        public HttpResponseMessage PostIndividualBank(MdlBuyerOnboardIndividual2BankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostIndividualBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetIndividualBankAccDtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardIndividual2BankAcc values = new MdlBuyerOnboardIndividual2BankAcc();
            objDaAgrMstSupplierOnboard.DaGetIndividualBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Individual2bankTmpList")]
        [HttpGet]
        public HttpResponseMessage Individual2bankTmpList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardIndividual2BankAcc values = new MdlBuyerOnboardIndividual2BankAcc();
            objDaAgrMstSupplierOnboard.DaIndividual2bankTmpList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGetIndividualBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage EditGetIndividualBankAccDtl(string contact2bankdtl_gid)
        {
            MdlBuyerOnboardIndividual2BankAcc values = new MdlBuyerOnboardIndividual2BankAcc();
            objDaAgrMstSupplierOnboard.DaEditGetIndividualBankAccDtl(contact2bankdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateIndividualBankAccDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualBankAccDtl(MdlBuyerOnboardIndividual2BankAcc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateIndividualBankAccDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteIndividualBankAcc")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualBankAcc(string contact2bankdtl_gid, string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardIndividual2BankAcc values = new MdlBuyerOnboardIndividual2BankAcc();
            objDaAgrMstSupplierOnboard.DaDeleteIndividualBankAcc(contact2bankdtl_gid, contact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprIndividualBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetbyrIndividualBankAccDtl(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlindividual2bankacc values = new Mdlindividual2bankacc();
            objDaAgrMstSupplierOnboard.DaGetSuprIndividualBankAccDtl(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprInstitutionBankAccDtl")]
        [HttpGet]
        public HttpResponseMessage GetbyrInstitutionBankAccDtl(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlInstitution2BankAcc values = new MdlInstitution2BankAcc();
            objDaAgrMstSupplierOnboard.DaGetSuprInstitutionBankAccDtl(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GeSuprtonboardInitiateDetail")]
        [HttpGet]
        public HttpResponseMessage GeSuprtonboardInitiateDetail(string supplieronboard_gid)
        {
            MdlonboardInitiateDetail values = new MdlonboardInitiateDetail();
            objDaAgrMstSupplierOnboard.DaGeSupronboardInitiateDetail(supplieronboard_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprValidateProductProgram")]
        [HttpGet]
        public HttpResponseMessage GetSuprValidateProductProgram(string loanproduct_gid, string supplieronboard_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstSupplierOnboard.DaGetSuprValidateProductProgram(loanproduct_gid, supplieronboard_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcompanydocumentlist")]
        [HttpGet]
        public HttpResponseMessage Getcompanydocumentlist()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            BuyerOnboardinstitutionuploaddocument values = new BuyerOnboardinstitutionuploaddocument();
            objDaAgrMstSupplierOnboard.DaGetcompanydocumentlist(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getcompanyeditdocumentlist")]
        [HttpGet]
        public HttpResponseMessage Getcompanyeditdocumentlist(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            BuyerOnboardinstitutionuploaddocument values = new BuyerOnboardinstitutionuploaddocument();
            objDaAgrMstSupplierOnboard.DaGetcompanyeditdocumentlist(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostOnboardlglstatus")]
        [HttpPost]
        public HttpResponseMessage PostOnboardlglstatus(MdlOboardlglstatuslist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostOnboardlglstatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardLgltagstatusLog")]
        [HttpGet]
        public HttpResponseMessage GetOnboardLgltagstatusLog(string onboard_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOboardlglstatus values = new MdlOboardlglstatus();
            objDaAgrMstSupplierOnboard.DaGetOnboardLgltagstatusLog(onboard_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostOnboardTranferRM")]
        [HttpPost]
        public HttpResponseMessage PostOnboardTranferRM(MdlRmtransferdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaPostOnboardTranferRM(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardTranferRMLog")]
        [HttpGet]
        public HttpResponseMessage GetOnboardTranferRMLog(string onboard_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRmtransferlist values = new MdlRmtransferlist();
            objDaAgrMstSupplierOnboard.DaGetOnboardTranferRMLog(onboard_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOnboardValidatePANAadhar")]
        [HttpPost]
        public HttpResponseMessage GetOnboardValidatePANAadhar(MdlonboardValidatedtl values)
        {
            objDaAgrMstSupplierOnboard.DaGetOnboardValidatePANAadhar(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboard.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}