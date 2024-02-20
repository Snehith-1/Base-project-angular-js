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
    /// This Controllers will provide access to edit single and multiple datas in Supplier Onboard stage.  (Includes editing of onboarded buyer general, company & individual info)
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A, Premchander.K </remarks>

    [RoutePrefix("api/AgrMstSupplierOnboardEdit")]
    [Authorize]

    public class AgrMstSupplierOnboardEditController : ApiController
    {

        DaAgrMstSupplierOnboardEdit objDaAgrMstSupplierOnboardEdit = new DaAgrMstSupplierOnboardEdit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetGeneralInfoEdit")]
        [HttpGet]
        public HttpResponseMessage GetGeneralInfoEdit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardApplicationAdd values = new MdlMstBuyerOnboardApplicationAdd();
            objDaAgrMstSupplierOnboardEdit.DaGetGeneralInfoEdit(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualSummaryEdit")]
        [HttpGet]
        public HttpResponseMessage GetIndividualSummaryEdit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardCICIndividual values = new MdlBuyerOnboardCICIndividual();
            objDaAgrMstSupplierOnboardEdit.DaGetIndividualSummaryEdit(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionSummaryEdit")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionSummaryEdit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardCICInstitution values = new MdlBuyerOnboardCICInstitution();
            objDaAgrMstSupplierOnboardEdit.DaGetInstitutionSummaryEdit(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("SubmitInstitutionDtlEdit")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtlEdit(MdlMstBuyerOnboardInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaSubmitInstitutionDtlEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardInstitutionAdd values = new MdlMstBuyerOnboardInstitutionAdd();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionDetailsEdit(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution GST List

        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardGST values = new MdlMstBuyerOnboardGST();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionGSTList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Mobile Number List

        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionMobileNoList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Email Address List

        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionEmailAddressList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Address List

        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardAddressDetails values = new MdlMstBuyerOnboardAddressDetails();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionAddressList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution License List

        [ActionName("InstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage InstitutionLicenseList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardLicenseDetails values = new MdlMstBuyerOnboardLicenseDetails();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionLicenseList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Institution Document List

        [ActionName("InstitutionDocumentList")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionBuyerOnboarduploaddocument values = new institutionBuyerOnboarduploaddocument();
            objDaAgrMstSupplierOnboardEdit.DaInstitutionDocumentList(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualMobileNoList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactMobileNo values = new MdlBuyerOnboardContactMobileNo();
            objDaAgrMstSupplierOnboardEdit.DaGetIndividualMobileNoTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetIndividualEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualEmailAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactEmail values = new MdlBuyerOnboardContactEmail();
            objDaAgrMstSupplierOnboardEdit.DaGetIndividualEmailAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAddressTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactAddress values = new MdlBuyerOnboardContactAddress();
            objDaAgrMstSupplierOnboardEdit.DaGetIndividualAddressTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualProofTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactIdProof values = new MdlBuyerOnboardContactIdProof();
            objDaAgrMstSupplierOnboardEdit.DaGetIndividualProofTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactDocument values = new MdlBuyerOnboardContactDocument();
            objDaAgrMstSupplierOnboardEdit.DaGetIndividualDocTempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividual")]
        [HttpGet]
        public HttpResponseMessage EditIndividual(string contact_gid)
        {
            MdlMstBuyerOnboardContact values = new MdlMstBuyerOnboardContact();
            objDaAgrMstSupplierOnboardEdit.DaEditIndividual(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60TempList")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60TempList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactPANForm60 values = new MdlBuyerOnboardContactPANForm60();
            objDaAgrMstSupplierOnboardEdit.DaGetPANForm60TempList(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactPANForm60 values = new MdlBuyerOnboardContactPANForm60();
            objDaAgrMstSupplierOnboardEdit.DaGetPANForm60List(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardPANAbsenceReason objMdlPANAbsenceReason = new MdlBuyerOnboardPANAbsenceReason();
            objDaAgrMstSupplierOnboardEdit.DaContactPANAbsenceReasonList(contact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string contact_gid)
        {
            MdlBuyerOnboardPANAbsenceReason values = new MdlBuyerOnboardPANAbsenceReason();
            objDaAgrMstSupplierOnboardEdit.DaEditPANAbsenceReasonList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividual(MdlMstBuyerOnboardContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetAppMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboardEdit.DaGetAppMobileNoTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboardEdit.DaGetAppEmailAddressTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppGeneticCodeTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppGeneticCodeTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardGeneticCode values = new MdlMstBuyerOnboardGeneticCode();
            objDaAgrMstSupplierOnboardEdit.DaGetAppGeneticCodeTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppProductTempList")]
        [HttpGet]
        public HttpResponseMessage GetAppProductTempList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyerOnboardProductDetailList values = new MdlMstBuyerOnboardProductDetailList();
            objDaAgrMstSupplierOnboardEdit.DaGetAppProductTempList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstBuyerOnboardApplicationAdd values = new MdlMstBuyerOnboardApplicationAdd();
            objDaAgrMstSupplierOnboardEdit.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstBuyerOnboardApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaveInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionDtl(MdlMstBuyerOnboardInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaSaveInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualSave")]
        [HttpPost]
        public HttpResponseMessage IndividualSave(MdlMstBuyerOnboardContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaIndividualSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaveInstitutionEditDtl")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionEditDtl(MdlMstBuyerOnboardInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaSaveInstitutionEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaveIndividualEditDtl")]
        [HttpPost]
        public HttpResponseMessage SaveIndividualEditDtl(MdlMstBuyerOnboardContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaSaveIndividualEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitIndividualEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualEditDtl(MdlMstBuyerOnboardContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaSubmitIndividualEditDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitInstitutionEditDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionEditDtl(MdlMstBuyerOnboardInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaSubmitInstitutionEditDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualSubmit")]
        [HttpPost]
        public HttpResponseMessage IndividualSubmit(MdlMstBuyerOnboardContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaIndividualSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAppGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteAppGeneticCode(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objDaAgrMstSupplierOnboardEdit.DaDeleteAppGeneticCode(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualAddress(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBuyerOnboardContactAddress values = new MdlBuyerOnboardContactAddress();
            objDaAgrMstSupplierOnboardEdit.DaDeleteIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAppEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditAppEmailAddress(string application2email_gid)
        {
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboardEdit.DaEditAppEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateAppEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateAppEmailAddress(MdlMstBuyerOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateAppEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditAppMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditAppMobileNo(string application2contact_gid)
        {
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboardEdit.DaEditAppMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAppMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateAppMobileNo(MdlMstBuyerOnboardMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateAppMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionGST(string institution2branch_gid)
        {
            MdlMstBuyerOnboardGST values = new MdlMstBuyerOnboardGST();
            objDaAgrMstSupplierOnboardEdit.DaEditInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution GST

        [ActionName("UpdateInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionGST(MdlMstBuyerOnboardGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionMobileNo(string institution2mobileno_gid)
        {
            MdlMstBuyerOnboardMobileNo values = new MdlMstBuyerOnboardMobileNo();
            objDaAgrMstSupplierOnboardEdit.DaEditInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Institution Mobile No

        [ActionName("UpdateInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionMobileNo(MdlMstBuyerOnboardMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionEmailAddress(string institution2email_gid)
        {
            MdlMstBuyerOnboardEmailAddress values = new MdlMstBuyerOnboardEmailAddress();
            objDaAgrMstSupplierOnboardEdit.DaEditInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionEmailAddress(MdlMstBuyerOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionAddressDetail(string institution2address_gid)
        {
            MdlMstBuyerOnboardAddressDetails values = new MdlMstBuyerOnboardAddressDetails();
            objDaAgrMstSupplierOnboardEdit.DaEditInstitutionAddressDetail(institution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionAddressDetail(MdlMstBuyerOnboardAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateInstitutionAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividualMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIndividualMobileNo(string contact2mobileno_gid)
        {
            MdlBuyerOnboardContactMobileNo values = new MdlBuyerOnboardContactMobileNo();
            objDaAgrMstSupplierOnboardEdit.DaEditIndividualMobileNo(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        // Update Individual Mobile No

        [ActionName("UpdateIndividualMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualMobileNo(MdlBuyerOnboardContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateIndividualMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividualEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualEmailAddress(string contact2email_gid)
        {
            MdlBuyerOnboardContactEmail values = new MdlBuyerOnboardContactEmail();
            objDaAgrMstSupplierOnboardEdit.DaEditIndividualEmailAddress(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateIndividualEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualEmailAddress(MdlBuyerOnboardContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateIndividualEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividualAddress")]
        [HttpGet]
        public HttpResponseMessage EditIndividualAddress(string contact2address_gid)
        {
            MdlBuyerOnboardContactAddress values = new MdlBuyerOnboardContactAddress();
            objDaAgrMstSupplierOnboardEdit.DaEditIndividualAddress(contact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateIndividualAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualAddress(MdlBuyerOnboardContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateIndividualAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSuprRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostSuprRaiseQuery(mdlraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaPostSuprRaiseQuery(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetSuprQuerySummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlraisequery values = new mdlraisequery();
            objDaAgrMstSupplierOnboardEdit.DaGetSuprQuerySummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUpdateSuprQueryStatus")]
        [HttpPost]
        public HttpResponseMessage GetUpdateSuprQueryStatus(mdlraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaGetUpdateSuprQueryStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSuprRaiseQuerydesc")]
        [HttpGet]
        public HttpResponseMessage GetSuprRaiseQuerydesc(string onboardquery_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlraisequery values = new mdlraisequery();
            objDaAgrMstSupplierOnboardEdit.DaGetSuprRaiseQuerydesc(values, onboardquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOpenSuprQueryStatus")]
        [HttpGet]
        public HttpResponseMessage GetOpenQueryStatus(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlraisequery values = new mdlraisequery();
            objDaAgrMstSupplierOnboardEdit.DaGetOpenSuprQueryStatus(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSupplierOnboardEdit.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}