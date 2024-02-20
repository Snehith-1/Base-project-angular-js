using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


namespace ems.master.Controllers
{
    [RoutePrefix("api/MstSAOnboardingIndividual")]
    [Authorize]

    public class MstSAOnboardingIndividualController : ApiController
    {
        DaMstSAOnboardingIndividual objDaSAIndividual = new DaMstSAOnboardingIndividual();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //Get Events - Drop Down
        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDownList values = new MdlDropDownList();
            objDaSAIndividual.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSABureauIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetSABureauIndividualList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaGetSABureauInstitutionList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Auto gen
        [ActionName("AutogenerateID")]
        [HttpGet]
        public HttpResponseMessage AutogenerateID()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            saAutoGenerate_ID values = new saAutoGenerate_ID();
            objDaSAIndividual.DaAutoGenerateID(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Mobile Number 
        //Add
        [ActionName("MobileNumberAdd")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAdd(MdlContactMobileNoSA values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaMobileNumberAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MobileNumberAddInEdit")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAddInEdit(MdlContactMobileNoSA values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaMobileNumberAddInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Mobile No List
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSA valuessa = new MdlContactMobileNoSA();
            objDaSAIndividual.DaGetMobileNoList(getsessionvalues.employee_gid, valuessa);
            return Request.CreateResponse(HttpStatusCode.OK, valuessa);
        }
        [ActionName("GetMobileNoEditList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoEditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSA values = new MdlContactMobileNoSA();
            objDaSAIndividual.DaGetMobileNoEditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSA values = new MdlContactMobileNoSA();
            objDaSAIndividual.DaGetMobileNoTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete Mobile No
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteMobileNo(string sacontact2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSA values = new MdlContactMobileNoSA();
            objDaSAIndividual.DaDeleteMobileNo(sacontact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Delete mobile no
        [ActionName("TempDeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage TempDeleteMobileNo()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSAInstituion values = new MdlContactMobileNoSAInstituion();
            objDaSAIndividual.DaTempDeleteMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Email Id

        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlsaOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEmailAddressInEdit")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddressInEdit(MdlsaOnboardEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostEmailAddressInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardEmailAddress values = new MdlsaOnboardEmailAddress();
            objDaSAIndividual.DaGetEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressEditList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressEditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardEmailAddress values = new MdlsaOnboardEmailAddress();
            objDaSAIndividual.DaGetEmailAddressEditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardEmailAddress values = new MdlsaOnboardEmailAddress();
            objDaSAIndividual.DaGetEmailAddressTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string sacontact2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardEmailAddress values = new MdlsaOnboardEmailAddress();
            objDaSAIndividual.DaDeleteEmailAddress(sacontact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempEmailAddress")]
        [HttpGet]
        public HttpResponseMessage TempEmailAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Address
        [ActionName("GetPostalCodeDetails")]
        [HttpGet]
        public HttpResponseMessage GetPostalCodeDetails(string postal_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardAddress values = new MdlSaOnboardAddress();
            objDaSAIndividual.DaGetPostalCodeDetails(postal_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAddress")]
        [HttpPost]
        public HttpResponseMessage PostAaddress(MdlSaOnboardAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAddressInEdit")]
        [HttpPost]
        public HttpResponseMessage PostAddressInEdit(MdlSaOnboardAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostAddressInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempAddress")]
        [HttpGet]
        public HttpResponseMessage TempAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAddress(string sacontact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardAddress values = new MdlSaOnboardAddress();
            objDaSAIndividual.DaDeleteAddress(sacontact2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardAddress values = new MdlSaOnboardAddress();
            objDaSAIndividual.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddressEditList")]
        [HttpGet]
        public HttpResponseMessage GetAddressEditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardAddress values = new MdlSaOnboardAddress();
            objDaSAIndividual.DaGetAddressEditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAddressTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardAddress values = new MdlSaOnboardAddress();
            objDaSAIndividual.DaGetAddressTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Prospects
        [ActionName("AddProspects")]
        [HttpPost]
        public HttpResponseMessage AddProspects(MdlsaOnboardProspects values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaAddProspects(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddProspectsInEdit")]
        [HttpPost]
        public HttpResponseMessage AddProspectsInEdit(MdlsaOnboardProspects values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaAddProspectsInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProspectsList")]
        [HttpGet]
        public HttpResponseMessage GetProspectsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardProspects values = new MdlsaOnboardProspects();
            objDaSAIndividual.DaGetprospectsList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProspectsEditList")]
        [HttpGet]
        public HttpResponseMessage GetProspectsEditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardProspects values = new MdlsaOnboardProspects();
            objDaSAIndividual.DaGetProspectsEditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProspectsTempList")]
        [HttpGet]
        public HttpResponseMessage GetProspectsTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardProspects values = new MdlsaOnboardProspects();
            objDaSAIndividual.DaGetProspectsTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempProspects")]
        [HttpGet]
        public HttpResponseMessage TempProspects()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempProspects(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete prospects ----------//
        [ActionName("DeleteProspects")]
        [HttpGet]
        public HttpResponseMessage DeleteProspects(string saprospects_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardProspects values = new MdlsaOnboardProspects();
            objDaSAIndividual.DaDeleteProspects(saprospects_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Document
        //[ActionName("AddDocuments")]
        //[HttpPost]
        //public HttpResponseMessage AddDocuments()
        //{
        //    HttpRequest httpRequest;
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    httpRequest = HttpContext.Current.Request;
        //    uploaddocument documentname = new uploaddocument();
        //    objDaSAIndividual.DaAddDocuments(httpRequest, documentname, getsessionvalues.employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, documentname);
        //}

        [ActionName("AddDocuments")]
        [HttpPost]
        public HttpResponseMessage AddDocuments()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSAIndividual.DaAddDocuments(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);

        }

        [ActionName("GetDocumentsList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardDocument values = new MdlsaOnboardDocument();
            objDaSAIndividual.DaGetDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocumentEditList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentEditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardDocument values = new MdlsaOnboardDocument();
            objDaSAIndividual.DaGetDocumentEditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardDocument values = new MdlsaOnboardDocument();
            objDaSAIndividual.DaGetDocumentTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempDocuments")]
        [HttpGet]
        public HttpResponseMessage TempDocuments()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempDocuments(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete Documents ----------//
        [ActionName("DeleteDocuments")]
        [HttpGet]
        public HttpResponseMessage DeleteDocuments(string sadocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardDocument values = new MdlsaOnboardDocument();
            objDaSAIndividual.DaDeleteDocuments(sadocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Save   
        [ActionName("OnboardSave")]
        [HttpPost]
        public HttpResponseMessage OnboardSave(MdlMstSAOnboard values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaOnboardSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Submit
        [ActionName("OnboardSubmit")]
        [HttpPost]
        public HttpResponseMessage OnboardSubmit(MdlMstSAOnboard values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaOnboardSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("OnboardSubmitSaveasdraft")]
        [HttpPost]
        public HttpResponseMessage OnboardSubmitSaveasdraft(MdlMstSAOnboard values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaOnboardSubmitSaveasdraft(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RMReportingManager")]
        [HttpGet]
        public HttpResponseMessage RMReportingManager()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardRM valuessa = new MdlsaOnboardRM();
            objDaSAIndividual.DaRMReportingManager(getsessionvalues.employee_gid, valuessa);
            return Request.CreateResponse(HttpStatusCode.OK, valuessa);
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
            objDaSAIndividual.DaPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("TempPanDoc")]
        [HttpGet]
        public HttpResponseMessage TempPanDoc()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempPanDoc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage PANForm60Delete(string sacontact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60sa values = new MdlContactPANForm60sa();
            objDaSAIndividual.DaPANForm60Delete(sacontact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60sa values = new MdlContactPANForm60sa();
            objDaSAIndividual.DaGetPANForm60List(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60EditList")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60EditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60sa values = new MdlContactPANForm60sa();
            objDaSAIndividual.DaGetPANForm60EditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60TempList")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60TempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60sa values = new MdlContactPANForm60sa();
            objDaSAIndividual.DaGetPANForm60TempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage PANAbsenceReasonList()
        {
            MdlPANAbsenceReasonsa objMdlPANAbsenceReason = new MdlPANAbsenceReasonsa();
            objDaSAIndividual.DaPANAbsenceReasonList(objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("PostPANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage PostPANAbsenceReasons(MdlPANAbsenceReasonsa values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostPANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PANReasonsCheck")]
        [HttpGet]
        public HttpResponseMessage PANReasonsCheck()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReasonsa values = new MdlPANAbsenceReasonsa();
            objDaSAIndividual.DaPANReasonsCheck(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage EditPANAbsenceReasonList(string sacontact_gid)
        {
            MdlPANAbsenceReasonsa values = new MdlPANAbsenceReasonsa();
            objDaSAIndividual.DaEditPANAbsenceReasonList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ContactPANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage ContactPANAbsenceReasonList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReasonsa objMdlPANAbsenceReason = new MdlPANAbsenceReasonsa();
            objDaSAIndividual.DaContactPANAbsenceReasonList(sacontact_gid, getsessionvalues.employee_gid, objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }
        //[ActionName("BureauList")]
        //[HttpGet]
        //public HttpResponseMessage BureauList(string sacontact_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlSACICIndividual objMdlSACICIndividual = new MdlSACICIndividual();
        //    objDaSAIndividual.DaBureauList(sacontact_gid, getsessionvalues.employee_gid, objMdlSACICIndividual);
        //    return Request.CreateResponse(HttpStatusCode.OK, objMdlSACICIndividual);
        //}
        //Edit
        [ActionName("IndividualDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage IndividualDetailsEdit(string sacontact_gid)
        {
            Individualedit values = new Individualedit();
            objDaSAIndividual.DaIndividualDetailsEdit(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualUpdate(Individualedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaIndividualUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Summary

        [ActionName("GetOnboardSummary")]
        [HttpGet]
        public HttpResponseMessage GetOnboardSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetOnboardSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetOnboardCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetOnboardCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSAVerfiyPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerfiyPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSAVerifyInitiatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerifyInitiatedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerifyInitiatedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete
        [ActionName("DeleteIndividual")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividual(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAIndividual.DaDeleteIndividual(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Verification

        [ActionName("ApprovalInitatedDetail")]
        [HttpGet]
        public HttpResponseMessage ApprovalInitatedDetail(string sacontact_gid)
        {
            Individualedit values = new Individualedit();
            objDaSAIndividual.DaApprovalInitatedDetail(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApprovalInitated")]
        [HttpPost]
        public HttpResponseMessage ApprovalInitated(MdlMstIndividualApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaApprovalInitated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerApprovalEdit")]
        [HttpPost]
        public HttpResponseMessage CheckerApprovalEdit(MdlMstIndividualApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaCheckerApprovalEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSABureauInstitution")]
        [HttpPost]
        public HttpResponseMessage PostSABureauInstitution(MdlSACICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostSABureauInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSABureauInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetSABureauInstitutionList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaGetSABureauInstitutionList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSABureauIndividualTempList")]
        [HttpGet]
        public HttpResponseMessage GetSABureauIndividualTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaGetSABureauIndividualTempList(sacontact_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaInstitutionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage SaInstitutionDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSAIndividual.DaSaInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("SAUploadIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage SAUploadIndividualDocList(string saindividual2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaSAUploadIndividualDocList(saindividual2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SAUploadIndDocList")]
        [HttpGet]
        public HttpResponseMessage SAUploadIndDocList(string saindividual2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaSAUploadIndDocList(saindividual2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SABureauView")]
        [HttpGet]
        public HttpResponseMessage SABureauView(string saindividual2bureau_gid)
        {
            MdlSACICIndividual values = new MdlSACICIndividual();
            objDaSAIndividual.DaSABureauView(saindividual2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempBureauDocuments")]
        [HttpGet]
        public HttpResponseMessage TempBureauDocuments()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempBureauDocuments(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempBureau")]
        [HttpGet]
        public HttpResponseMessage TempBureau()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempBureau(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteContactBureau")]
        [HttpGet]
        public HttpResponseMessage DeleteContactBureau(string saindividual2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objDaSAIndividual.DaDeleteContactBureau(saindividual2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBureauDocuments")]
        [HttpGet]
        public HttpResponseMessage DeleteBureauDocuments(string individualsabureaudocumentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaDeleteBureauDocuments(individualsabureaudocumentupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // cancel cheque
        [ActionName("SaInstCancelChequeUpload")]
        [HttpPost]
        public HttpResponseMessage SaInstCancelChequeUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSAIndividual.DaSaInstCancelChequeUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetSaChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetSaChequeDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaChequeDocument values = new MdlSaChequeDocument();
            objDaSAIndividual.DaGetSaChequeDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSaChequeDocumentEditList")]
        [HttpGet]
        public HttpResponseMessage GetSaChequeDocumentEditList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaChequeDocument values = new MdlSaChequeDocument();
            objDaSAIndividual.DaGetSaChequeDocumentEditList(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Documents ----------//
        [ActionName("ChequeDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentDelete(string individualcancelchequeupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaSAIndividual.DaChequeDocumentDelete(individualcancelchequeupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Approval

        [ActionName("PostApprove")]
        [HttpPost]
        public HttpResponseMessage PostApprove(MdlApproveind values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRejected")]
        [HttpPost]
        public HttpResponseMessage PostRejected(MdlApproveind values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApprovalInitiateSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovalInitiateSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSaApprovalInitiateSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalInitiateSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalInitiateSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetApprovalInitiateSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Mail Document

        [ActionName("SaMailDocument")]
        [HttpPost]
        public HttpResponseMessage SaMailDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSAIndividual.DaSaMailDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("SAMailDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage SAMailDocumentTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaSAMailDocumentTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SAMailDocumentList")]
        [HttpGet]
        public HttpResponseMessage SAMailDocumentList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaSAMailDocumentList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempSAMailDocument")]
        [HttpGet]
        public HttpResponseMessage TempSAMailDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempSAMailDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSAMailDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteSAMailDocument(string saindividualmaildocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaDeleteSAMailDocument(saindividualmaildocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Verification Document

        [ActionName("SaVerifyDocument")]
        [HttpPost]
        public HttpResponseMessage SaVerifyDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSAIndividual.DaSaVerifyDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("SaVerifyDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage SaVerifyDocumentTempList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaSaVerifyDocumentTempList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaVerifyDocumentList")]
        [HttpGet]
        public HttpResponseMessage SaVerifyDocumentList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAIndividual.DaSaVerifyDocumentList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempSaVerifyDocument")]
        [HttpGet]
        public HttpResponseMessage TempSaVerifyDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempSaVerifyDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSaVerifyDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteSaVerifyDocument(string saindividualverifydocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaDeleteSaVerifyDocument(saindividualverifydocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyMakerindividualPending")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyMakerindividualPending()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerfiyMakerIndividualPending(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyindividualPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyindividualPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerfiyindividualPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyindividualMappingPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyindividualMappingPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerfiyindividualMappingPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerifyMakerIndividualInitiatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerifyMakerIndividualInitiatedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerifyMakerIndividualInitiatedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateSAMapping")]
        [HttpPost]
        public HttpResponseMessage CreateSAMapping(MdlsaOnboardSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaCreateSAMapping(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateSAMappingAssignment")]
        [HttpPost]
        public HttpResponseMessage CreateSAMappingAssignment(MdlsaOnboardSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaCreateSAMappingAssignment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MakerIndividualInitated")]
        [HttpPost]
        public HttpResponseMessage MakerIndividualInitated(MdlMstIndividualApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaMakerIndividualInitated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssignedInformation")]
        [HttpGet]
        public HttpResponseMessage GetAssignedInformation(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstIndividualApprovalList values = new MdlMstIndividualApprovalList();
            objDaSAIndividual.DaGetAssignedInformation(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMakerIndividualRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostMakerIndividualRaiseQuery(Mdlmakerindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostMakerIndividualRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMakerIndividualRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetMakerIndividualRaiseQuery(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmakerindividualraisequery values = new Mdlmakerindividualraisequery();
            objDaSAIndividual.DaGetMakerIndividualRaiseQuery(sacontact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostMakerIndividualresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostMakerIndividualresponsequery(Mdlmakerindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostMakerIndividualresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCheckerIndividualRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostCheckerIndividualRaiseQuery(mdlcheckerindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostCheckerIndividualRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckerIndividualRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetCheckerIndividualRaiseQuery(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcheckerindividualraisequery values = new mdlcheckerindividualraisequery();
            objDaSAIndividual.DaGetCheckerIndividualRaiseQuery(sacontact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostCheckerIndividualresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostCheckerIndividualresponsequery(mdlcheckerindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostCheckerIndividualresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostApproverIndividualRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostApproverIndividualRaiseQuery(Mdlapproverindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostApproverIndividualRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApproverIndividualRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetApproverIndividualRaiseQuery(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlapproverindividualraisequery values = new Mdlapproverindividualraisequery();
            objDaSAIndividual.DaGetApproverIndividualRaiseQuery(sacontact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostApproverIndividualresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostApproverIndividualresponsequery(Mdlapproverindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaPostApproverIndividualresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MakerIndividualsaveasdraftInitated")]
        [HttpPost]
        public HttpResponseMessage MakerSaveasdraftApprovalInitated(MdlMstIndividualApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaMakerIndividualsaveasdraftInitated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRejectedInitiateSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedInitiateSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetRejectedInitiateSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaPendingAssignmentCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaPendingAssignmentCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAIndividual.DaGetSaPendingAssignmentCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaAssignmentCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaAssignmentCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAIndividual.DaGetSaAssignmentCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApprovedCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAIndividual.DaGetSaApprovedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApproverCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaApproverCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAIndividual.DaGetSaApproverCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaMakerCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaMakerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAIndividual.DaGetSaMakerCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaCheckerCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaCheckerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAIndividual.DaGetSaCheckerCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateSAmappingLog")]
        [HttpGet]
        public HttpResponseMessage CreateSAmappingLog(string sacontact_gid)
        {
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaCreateSAmappingLog(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("FutureDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaFutureDateCheck(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempChequeDocuments")]
        [HttpGet]
        public HttpResponseMessage TempChequeDocuments()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAIndividual.DaTempChequeDocuments(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEntityDefaultID")]
        [HttpGet]
        public HttpResponseMessage GetEntityDefaultID()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            string entityID = string.Empty;
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
           // entityID=objDaSAIndividual.DaGetEntityDefaultID(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, entityID);
        }
        [ActionName("GetCodecreationIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetCodecreationIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetCodecreationIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCodecreationIndividualCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCodecreationIndividualCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetCodecreationIndividualCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualActivityWebSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualActivityWebSummary(string samfin_code, string samagro_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetIndividualActivityWebSummary(getsessionvalues.employee_gid,samfin_code, samagro_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualActivityManagementSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualActivityManagementSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetIndividualActivityManagementSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SamcodesAutoGenerate")]
        [HttpGet]
        public HttpResponseMessage SamcodesAutoGenerate(string sacontact_gid)
        {
           
            Codes values = new Codes();
            objDaSAIndividual.DaSamcodesAutoGenerate( sacontact_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardIndividualRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetOnboardIndividualRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetOnboardIndividualRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardIndividualGroupingSummary")]
        [HttpGet]
        public HttpResponseMessage GetOnboardIndividualGroupingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetOnboardIndividualGroupingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualRenewalGroupingSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualRenewalGroupingSummary(string samfin_code,string samagro_code,string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetIndividualRenewalGroupingSummary(getsessionvalues.employee_gid,samfin_code,samagro_code,sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEntitytype")]
        [HttpGet]
        public HttpResponseMessage GetEntitytype(string saentitytype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetEntitytype(saentitytype_gid, getsessionvalues.employee_gid, values);
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
            objDaSAIndividual.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("DocumentUploadList")]
        [HttpGet]
        public HttpResponseMessage DocumentUploadList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAIndividual.DaDocumentUploadList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentDelete(string sadocument_gid)
        {

            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAIndividual.DaUploadDocumentDelete(sadocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentTmpList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAIndividual.DaUploadDocumentTmpList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualTempClear")]
        [HttpGet]
        public HttpResponseMessage IndividualTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAIndividual.DaIndividualTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DocumentUploadViewList")]
        [HttpGet]
        public HttpResponseMessage DocumentUploadViewList(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAIndividual.DaDocumentUploadViewList(sacontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualRenewal")]
        [HttpPost]
        public HttpResponseMessage IndividualRenewal(Individualedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaIndividualRenewal(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualCodePendingReport")]
        [HttpGet]
        public HttpResponseMessage IndividualCodePendingReport()
        {
            MdlMstSAOnboard values = new MdlMstSAOnboard();
            objDaSAIndividual.DaIndividualCodePendingReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualCodeCompletedReport")]
        [HttpGet]
        public HttpResponseMessage IndividualCodeCompletedReport()
        {
            MdlMstSAOnboard values = new MdlMstSAOnboard();
            objDaSAIndividual.DaIndividualCodeCompletedReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualMakerRejected")]
        [HttpPost]
        public HttpResponseMessage IndividualMakerRejected(MdlApproveind values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaIndividualMakerRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualCheckerRejected")]
        [HttpPost]
        public HttpResponseMessage IndividualCheckerRejected(MdlApproveind values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAIndividual.DaIndividualCheckerRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyIndividualMakerDistractSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyIndividualMakerDistractSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerfiyIndividualMakerDistractSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyIndividualCheckerDistractSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyIndividualCheckerDistractSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetSAVerfiyIndividualCheckerDistractSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualDeferredSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDeferredSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAIndividual.DaGetIndividualDeferredSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}