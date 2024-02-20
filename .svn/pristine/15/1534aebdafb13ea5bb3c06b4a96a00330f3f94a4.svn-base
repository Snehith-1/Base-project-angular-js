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
        [RoutePrefix("api/MstSAOnboardingInstitution")]
        [Authorize]

    public class MstSAOnboardingInstitutionController : ApiController
    {
        DaMstSAOnboardingInstitution objDaSAInstitution = new DaMstSAOnboardingInstitution();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        // Mobile number

        // Mobile Number Add 
        [ActionName("MobileNumberAdd")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAdd(MdlContactMobileNoSAInstituion valuessa)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaMobileNumberAdd(getsessionvalues.employee_gid, valuessa);
            return Request.CreateResponse(HttpStatusCode.OK, valuessa);
        }
        [ActionName("MobileNumberAddInEdit")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAddInEdit(MdlContactMobileNoSAInstituion valuessa)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaMobileNumberAddInEdit(getsessionvalues.employee_gid, valuessa);
            return Request.CreateResponse(HttpStatusCode.OK, valuessa);
        }
        // Get Mobile No List
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSAInstituion valuessa = new MdlContactMobileNoSAInstituion();
            objDaSAInstitution.DaGetMobileNoList(getsessionvalues.employee_gid, valuessa);
            return Request.CreateResponse(HttpStatusCode.OK, valuessa);
        }

        //----------- Delete Mobile No----------//
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteMobileNo(string sainstitution2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSAInstituion values = new MdlContactMobileNoSAInstituion();
            objDaSAInstitution.DaDeleteMobileNo(sainstitution2mobileno_gid, values);
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
            objDaSAInstitution.DaTempDeleteMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Mobile Number Edit
        [ActionName("MobileNoEdit")]
        [HttpGet]
        public HttpResponseMessage MobileNoEdit(string sainstitution2mobileno_gid)
        {
            MdlContactMobileNoSAInstituion values = new MdlContactMobileNoSAInstituion();
            objDaSAInstitution.DaMobileNoEdit(sainstitution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Mobile Number Update
        [ActionName("MobileNoUpdate")]
        [HttpPost]
        public HttpResponseMessage MobileNoUpdate(MdlContactMobileNoSAInstituion values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaMobileNoUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Email address

        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlsaOnboardInstiEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostEmailAddressInEdit")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddressInEdit(MdlsaOnboardInstiEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostEmailAddressInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaGetEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string sainstitution2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaDeleteEmailAddress(sainstitution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempEmailAddress")]
        [HttpGet]
        public HttpResponseMessage TempEmailAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEmailAddressList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaInstitutionEmailAddressList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Email Address Edit
        [ActionName("EmailAddressEdit")]
        [HttpGet]
        public HttpResponseMessage EmailAddressEdit(string sainstitution2email_gid)
        {
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaEmailAddressEdit(sainstitution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Email Address Update
        [ActionName("EmailAddressUpdate")]
        [HttpPost]
        public HttpResponseMessage EmailAddressUpdate(MdlsaOnboardInstiEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaEmailAddressUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEmailAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressTempList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaGetEmailAddressTempList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    

        // Address

        [ActionName("PostAddress")]
        [HttpPost]
        public HttpResponseMessage PostAaddress(MdlSaOnboardInstiAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAddressInEdit")]
        [HttpPost]
        public HttpResponseMessage PostAddressInEdit(MdlSaOnboardInstiAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostAddressInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAddress(string sainstitution2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardInstiAddress values = new MdlSaOnboardInstiAddress();
            objDaSAInstitution.DaDeleteAddress(sainstitution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempAddress")]
        [HttpGet]
        public HttpResponseMessage TempAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardInstiAddress values = new MdlSaOnboardInstiAddress();
            objDaSAInstitution.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage InstitutionAddressList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardInstiAddress values = new MdlSaOnboardInstiAddress();
            objDaSAInstitution.DaInstitutionAddressList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Address Details Edit
        [ActionName("AddressDetailEdit")]
        [HttpGet]
        public HttpResponseMessage AddressDetailEdit(string sainstitution2address_gid)
        {
            MdlSaOnboardInstiAddress values = new MdlSaOnboardInstiAddress();
            objDaSAInstitution.DaAddressDetailEdit(sainstitution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Address Details Update
        [ActionName("AddressDetailUpdate")]
        [HttpPost]
        public HttpResponseMessage AddressDetailUpdate(MdlSaOnboardInstiAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaAddressDetailUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddressTempList")]
        [HttpGet]
        public HttpResponseMessage GetAddressTempList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardInstiAddress values = new MdlSaOnboardInstiAddress();
            objDaSAInstitution.DaGetAddressTempList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPostalCodeDetails")]
        [HttpGet]
        public HttpResponseMessage GetPostalCodeDetails(string postal_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaOnboardInstiAddress values = new MdlSaOnboardInstiAddress();
            objDaSAInstitution.DaGetPostalCodeDetails(postal_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Gst
        [ActionName("PostGST")]
        [HttpPost]
        public HttpResponseMessage PostGST(MdlSAOnboardInstiGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlSAOnboardInstiGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInstitutionGSTInEdit")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGSTInEdit(MdlSAOnboardInstiGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostInstitutionGSTInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGSTList")]
        [HttpGet]
        public HttpResponseMessage GetGSTList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAOnboardInstiGST values = new MdlSAOnboardInstiGST();
            objDaSAInstitution.DaGetGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGST")]
        [HttpGet]
        public HttpResponseMessage DeleteGST(string sainstitution2gst_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAOnboardInstiGST values = new MdlSAOnboardInstiGST();
            objDaSAInstitution.DaDeleteGST(sainstitution2gst_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGSTInstitution")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTInstitution(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objDaSAInstitution.DaDeleteGSTInstitution(getsessionvalues.employee_gid, sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempGST")]
        [HttpGet]
        public HttpResponseMessage TempGST()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAOnboardInstiGST values = new MdlSAOnboardInstiGST();
            objDaSAInstitution.DaInstitutionGSTList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionGSTEditList")]
        [HttpGet]
        public HttpResponseMessage InstitutionGSTEditList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAOnboardInstiGST values = new MdlSAOnboardInstiGST();
            objDaSAInstitution.DaInstitutionGSTEditList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GSTEdit")]
        [HttpGet]
        public HttpResponseMessage GSTEdit(string sainstitution2gst_gid)
        {
            MdlSAOnboardInstiGST values = new MdlSAOnboardInstiGST();
            objDaSAInstitution.DaGSTEdit(sainstitution2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GSTUpdate")]
        [HttpPost]
        public HttpResponseMessage GSTUpdate(MdlSAOnboardInstiGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaGSTUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGSTState")]
        [HttpGet]
        public HttpResponseMessage GetGSTState(string gst_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAOnboardInstiGST values = new MdlSAOnboardInstiGST();
            objDaSAInstitution.DaGetGSTState(gst_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Individual

        [ActionName("PostIndividualDetails")]
        [HttpPost]
        public HttpResponseMessage PostIndividualDetails(MdlMstSAOnboardInstiIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostIndividualDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostIndividualDetailsInEdit")]
        [HttpPost]
        public HttpResponseMessage PostIndividualDetailsInEdit(MdlMstSAOnboardInstiIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostIndividualDetailsInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSAOnboardInstiIndividual values = new MdlMstSAOnboardInstiIndividual();
            objDaSAInstitution.DaGetIndividualList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividual")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividual(string sainst_individual_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSAOnboardInstiIndividual values = new MdlMstSAOnboardInstiIndividual();
            objDaSAInstitution.DaDeleteIndividual(sainst_individual_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
   
        [ActionName("IndividualList")]
        [HttpGet]
        public HttpResponseMessage IndividualList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSAOnboardInstiIndividual values = new MdlMstSAOnboardInstiIndividual();
            objDaSAInstitution.DaIndividualList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempIndividual")]
        [HttpGet]
        public HttpResponseMessage TempIndividual()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSAOnboardInstiIndividual values = new MdlMstSAOnboardInstiIndividual();
            objDaSAInstitution.DaGetIndividualList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Edit
        [ActionName("IndividualEdit")]
        [HttpGet]
        public HttpResponseMessage IndividualEdit(string sainst_individual_gid)
        {
            MdlMstSAOnboardInstiIndividual values = new MdlMstSAOnboardInstiIndividual();
            objDaSAInstitution.DaIndividualEdit(sainst_individual_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Update
        [ActionName("IndividualUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualUpdate(MdlMstSAOnboardInstiIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaIndividualUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
                   
        //Prospects
        [ActionName("AddIndividualProspects")]
        [HttpPost]
        public HttpResponseMessage AddIndividualProspects(MdlsaOnboardIstitutionProspects values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaAddIndividualProspects(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddIndividualProspectsInEdit")]
        [HttpPost]
        public HttpResponseMessage AddIndividualProspectsInEdit(MdlsaOnboardIstitutionProspects values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaAddIndividualProspectsInEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionProspectsList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionProspectsList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardIstitutionProspects values = new MdlsaOnboardIstitutionProspects();
            objDaSAInstitution.DaInstitutionProspectsList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete prospects ----------//
        [ActionName("DeleteProspects")]
        [HttpGet]
        public HttpResponseMessage DeleteProspects(string saprospects_institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardIstitutionProspects values = new MdlsaOnboardIstitutionProspects();
            objDaSAInstitution.DaDeleteProspects(saprospects_institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempProspects")]
        [HttpGet]
        public HttpResponseMessage TempProspects()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempProspects(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetprospectsList")]
        [HttpGet]
        public HttpResponseMessage GetprospectsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardIstitutionProspects values = new MdlsaOnboardIstitutionProspects();
            objDaSAInstitution.DaGetprospectsList( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProspectsEditList")]
        [HttpGet]
        public HttpResponseMessage GetProspectsEditList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardIstitutionProspects values = new MdlsaOnboardIstitutionProspects();
            objDaSAInstitution.DaGetProspectsEditList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Prospects Edit
        [ActionName("ProspectsEdit")]
        [HttpGet]
        public HttpResponseMessage ProspectsEdit(string saprospects_institution_gid)
        {
            MdlsaOnboardIstitutionProspects values = new MdlsaOnboardIstitutionProspects();
            objDaSAInstitution.DaProspectsEdit(saprospects_institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Prospects Update
        [ActionName("ProspectsUpdate")]
        [HttpPost]
        public HttpResponseMessage ProspectsUpdate(MdlsaOnboardIstitutionProspects values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaProspectsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

//Document
        [ActionName("AddDocuments")]
        [HttpPost]
        public HttpResponseMessage AddDocuments(MdlsaOnboardInstitutionDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaAddDocuments(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionDocumentList")]
        [HttpGet]  
        public HttpResponseMessage InstitutionDocumentList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAInstitution.DaInstitutionDocumentList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("TempDocuments")]
        [HttpGet]
        public HttpResponseMessage TempDocuments()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempDocuments(getsessionvalues.employee_gid, values);
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
            uploaddocument documentname = new uploaddocument();
            objDaSAInstitution.DaInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);

        }

        [ActionName("GetUploadDocumentsList")]
        [HttpGet]
        public HttpResponseMessage GetUploadDocumentsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAInstitution.DaGetUploadDocumentsList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UploadDocumentsDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentsDelete(string sainstidocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAInstitution.DaUploadDocumentsDelete(sainstidocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocList")]
        [HttpGet]
        public HttpResponseMessage UploadDocList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAInstitution.DaUploadDocList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadList")]
        [HttpGet]
        public HttpResponseMessage UploadList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAInstitution.DaUploadList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        // Overall save and submit

        [ActionName("OnboardSave")]
        [HttpPost]
        public HttpResponseMessage OnboardSave(MdlMstSAOnboardInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaSaOnboardSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OnboardSubmit")]
        [HttpPost]
        public HttpResponseMessage OnboardSubmit(MdlMstSAOnboardInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaOnboardSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OnboardSubmitSaveasdraft")]
        [HttpPost]
        public HttpResponseMessage OnboardSubmitSaveasdraft(MdlMstSAOnboardInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaOnboardSubmitSaveasdraft(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Update

        [ActionName("CompanyEditUpdate")]
        [HttpPost]
        public HttpResponseMessage buyerEditUpdate(Institutionedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaInstitutionEditUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitution")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitution(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstitutionDocument values = new MdlsaOnboardInstitutionDocument();
            objDaSAInstitution.DaDeleteInstitution(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Rm name

        [ActionName("GetRMName")]
        [HttpGet]
        public HttpResponseMessage GetRMName()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardRM values = new MdlsaOnboardRM();
            objDaSAInstitution.DaGetRMName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMNameGid")]
        [HttpGet]
        public HttpResponseMessage GetRMNameGid()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardRM values = new MdlsaOnboardRM();
            objDaSAInstitution.DaGetRMNameGid(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Events - Drop Down
        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDownList values = new MdlDropDownList();
            objDaSAInstitution.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AutogenerateID")]
        [HttpGet]
        public HttpResponseMessage AutogenerateID()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            saAutoGenerate_ID values = new saAutoGenerate_ID();
            objDaSAInstitution.DaAutoGenerateID(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
     
        [ActionName("InstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage InstitutionMobileNoList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSAInstituion values = new MdlContactMobileNoSAInstituion();
            objDaSAInstitution.DaInstitutionMobileNoList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
                
        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string sacontactinstitution_gid)
        {
            Institutionedit values = new Institutionedit();
            objDaSAInstitution.DaInstitutionDetailsEdit(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionEditSave")]
        [HttpPost]
        public HttpResponseMessage InstitutionEditSave(Institutionedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaInstitutionEditSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetTempMobileNoList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNoSAInstituion values = new MdlContactMobileNoSAInstituion();
            objDaSAInstitution.DaGetTempMobileNoList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    
        //Summary

        [ActionName("GetSaInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaInstitutionCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaInstitutionCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSAVerfiyPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSAVerifyInitiatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerifyInitiatedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerifyInitiatedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        //Approval Initiated


        [ActionName("GetSaApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSaApprovalInitiatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovalInitiatedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaApprovalInitiatedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostApprove")]
        [HttpPost]
        public HttpResponseMessage PostApprove(MdlApprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRejected")]
        [HttpPost]
        public HttpResponseMessage PostRejected(MdlApprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Verification
        [ActionName("ApprovalInitated")]
        [HttpPost]
        public HttpResponseMessage ApprovalInitated(MdlMstInitiateApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaApprovalInitated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerApprovalEdit")]
        [HttpPost]
        public HttpResponseMessage CheckerApprovalEdit(MdlMstInitiateApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaCheckerApprovalEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApprovalInitatedDetail")]
        [HttpGet]
        public HttpResponseMessage ApprovalInitatedDetail(string sacontactinstitution_gid)
        {
            Institutionedit values = new Institutionedit();
            objDaSAInstitution.DaApprovalInitatedDetail(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSABureauInstitution")]
        [HttpPost]
        public HttpResponseMessage PostSABureauInstitution(MdlSACICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostSABureauInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSABureauInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetSABureauInstitutionList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaGetSABureauInstitutionList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSABureauInstitutionTempList")]
        [HttpGet]
        public HttpResponseMessage GetSABureauInstitutionTempList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaGetSABureauInstitutionTempList(sacontactinstitution_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("SABureauView")]
        [HttpGet]
        public HttpResponseMessage SABureauView(string sainstitution2bureau_gid)
        {
            MdlSACICIndividual values = new MdlSACICIndividual();
            objDaSAInstitution.DaSABureauView(sainstitution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteContactBureau")]
        [HttpGet]
        public HttpResponseMessage DeleteContactBureau(string sainstitution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objDaSAInstitution.DaDeleteContactBureau(sainstitution2bureau_gid, values);
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
            objDaSAInstitution.DaSaInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("SAUploadIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage SAUploadIndividualDocList(string sainstitution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaSAUploadIndividualDocList(sainstitution2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SAUploadIndDocList")]
        [HttpGet]
        public HttpResponseMessage SAUploadIndDocList(string sainstitution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaSAUploadIndDocList(sainstitution2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempBureauDocuments")]
        [HttpGet]
        public HttpResponseMessage TempBureauDocuments()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempBureauDocuments(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempBureau")]
        [HttpGet]
        public HttpResponseMessage TempBureau()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempBureau(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBureauDocuments")]
        [HttpGet]
        public HttpResponseMessage DeleteBureauDocuments(string institutionsabureaudocumentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaDeleteBureauDocuments(institutionsabureaudocumentupload_gid, values);
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
            objDaSAInstitution.DaSaInstCancelChequeUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetSaChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetSaChequeDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaChequeDocument values = new MdlSaChequeDocument();
            objDaSAInstitution.DaGetSaChequeDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSaChequeDocumentEditList")]
        [HttpGet]
        public HttpResponseMessage GetSaChequeDocumentEditList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSaChequeDocument values = new MdlSaChequeDocument();
            objDaSAInstitution.DaGetSaChequeDocumentEditList(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
            

            //----------- Delete Documents ----------//
            [ActionName("ChequeDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentDelete(string institutioncancelchequeupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaSAInstitution.DaChequeDocumentDelete(institutioncancelchequeupload_gid, values);
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
            objDaSAInstitution.DaSaMailDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("SAMailDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage SAMailDocumentTempList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaSAMailDocumentTempList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SAMailDocumentList")]
        [HttpGet]
        public HttpResponseMessage SAMailDocumentList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaSAMailDocumentList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempSAMailDocument")]
        [HttpGet]
        public HttpResponseMessage TempSAMailDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempSAMailDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSAMailDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteSAMailDocument(string sainstitutionmaildocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaDeleteSAMailDocument(sainstitutionmaildocument_gid, values);
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
            objDaSAInstitution.DaSaVerifyDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("SaVerifyDocumentTempList")]
        [HttpGet]
        public HttpResponseMessage SaVerifyDocumentTempList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaSaVerifyDocumentTempList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaVerifyDocumentList")]
        [HttpGet]
        public HttpResponseMessage SaVerifyDocumentList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSAInstituteBureau values = new MdlSAInstituteBureau();
            objDaSAInstitution.DaSaVerifyDocumentList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempSaVerifyDocument")]
        [HttpGet]
        public HttpResponseMessage TempSaVerifyDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempSaVerifyDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSaVerifyDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteSaVerifyDocument(string sainstitutionverifydocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaDeleteSaVerifyDocument(sainstitutionverifydocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyMakerInstitutionPending")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyMakerInstitutionPending()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyMakerInstitutionPending(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerifyMakerInstitutionInitiated")]
        [HttpGet]
        public HttpResponseMessage GetSAVerifyMakerInstitutionInitiated()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerifyMakerInstitutionInitiated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyinstitutionPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyinstitutionPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyinstitutionPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyinstitutionMappingPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyinstitutionMappingPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyinstitutionMappingPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MakerApprovalInitated")]
        [HttpPost]
        public HttpResponseMessage MakerApprovalInitated(MdlMstInitiateApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaMakerApprovalInitated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateSAMapping")]
        [HttpPost]
        public HttpResponseMessage CreateSAMapping(MdlMstInitiateApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaCreateSAMapping(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateSAMappingAssignment")]
        [HttpPost]
        public HttpResponseMessage CreateSAMappingAssignment(MdlMstInitiateApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaCreateSAMappingAssignment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssignedInformation")]
        [HttpGet]
        public HttpResponseMessage GetAssignedInformation(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstInitiateApprovalList values = new MdlMstInitiateApprovalList();
            objDaSAInstitution.DaGetAssignedInformation(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMakerInstitutionRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostMakerInstitutionRaiseQuery(Mdlmakerinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostMakerInstitutionRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMakerInstitutionRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetMakerInstitutionRaiseQuery(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmakerinstitutionraisequery values = new Mdlmakerinstitutionraisequery();
            objDaSAInstitution.DaGetMakerInstitutionRaiseQuery(sacontactinstitution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostMakerInstitutionresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostMakerInstitutionresponsequery(Mdlmakerinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostMakerInstitutionresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCheckerInstitutionRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostCheckerInstitutionRaiseQuery(mdlcheckerinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostCheckerInstitutionRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckerInstitutionRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetCheckerInstitutionRaiseQuery(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcheckerinstitutionraisequery values = new mdlcheckerinstitutionraisequery();
            objDaSAInstitution.DaGetCheckerInstitutionRaiseQuery(sacontactinstitution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostCheckerInstitutionresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostCheckerInstitutionresponsequery(mdlcheckerinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostCheckerInstitutionresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostApproverInstitutionRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostApproverInstitutionRaiseQuery(Mdlapproverinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostApproverInstitutionRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApproverInstitutionRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetApproverInstitutionRaiseQuery(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlapproverinstitutionraisequery values = new Mdlapproverinstitutionraisequery();
            objDaSAInstitution.DaGetApproverInstitutionRaiseQuery(sacontactinstitution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostApproverInstitutionresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostApproverInstitutionresponsequery(Mdlapproverinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaPostApproverInstitutionresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MakerSaveasdraftApprovalInitated")]
        [HttpPost]
        public HttpResponseMessage MakerSaveasdraftApprovalInitated(MdlMstInitiateApprovalList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaMakerSaveasdraftApprovalInitated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyinstitutionRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyinstitutionRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyinstitutionRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyinstitutionFinalApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyinstitutionFinalApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyinstitutionFinalApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaPendingAssignmentCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaPendingAssignmentCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSaPendingAssignmentCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaAssignmentCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaAssignmentCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSaAssignmentCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApprovedCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSaApprovedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApproverCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaApproverCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSaApproverCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaMakerCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaMakerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSaMakerCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaCheckerCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaCheckerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSaCheckerCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateSAmappingLog")]
        [HttpGet]
        public HttpResponseMessage CreateSAmappingLog(string sacontactinstitution_gid)
        { 
            MdlMstInitiateApprovalList values = new MdlMstInitiateApprovalList();
            objDaSAInstitution.DaCreateSAmappingLog(sacontactinstitution_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("FutureDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaFutureDateCheck(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DateCheck")]
        [HttpGet]
        public HttpResponseMessage DateCheck(string date)
        {
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaDateCheck(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempChequeDocuments")]
        [HttpGet]
        public HttpResponseMessage TempChequeDocuments()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardInstiEmailAddress values = new MdlsaOnboardInstiEmailAddress();
            objDaSAInstitution.DaTempChequeDocuments(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetcodecreationSummary")]
        [HttpGet]
        public HttpResponseMessage GetcodecreationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetcodecreationSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetcodecreationCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetcodecreationCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetcodecreationCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionActivityWebSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionActivityWebSummary(string samfin_code,string sam_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetInstitutionActivityWebSummary(getsessionvalues.employee_gid,samfin_code,sam_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionActivityManagementSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionActivityManagementSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetInstitutionActivityManagementSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSacodecreationcompletedCounts")]
        [HttpGet]
        public HttpResponseMessage GetSacodecreationcompletedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSacodecreationcompletedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSacodecreationCounts")]
        [HttpGet]
        public HttpResponseMessage GetSacodecreationCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSAInstitution.DaGetSacodecreationCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SamcodesAutoGenerate")]
        [HttpGet]
        public HttpResponseMessage SamcodesAutoGenerate()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            Codes values = new Codes();
            objDaSAInstitution.DaSamcodesAutoGenerate(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaInstitutionRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaInstitutionRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaInstitutionGroupingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionGroupingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaInstitutionGroupingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaInstitutionRenewalGroupingSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionRenewalGroupingSummary(string samfin_code,string samagro_code,string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSaInstitutionRenewalGroupingSummary(getsessionvalues.employee_gid, samfin_code, samagro_code, sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionRenewal")]
        [HttpPost]
        public HttpResponseMessage InstitutionRenewal(Institutionedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaInstitutionRenewal(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionUploadDocument")]
        [HttpPost]
        public HttpResponseMessage InstitutionUploadDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSAInstitution.DaInstitutionUploadDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("DocumentUploadList")]
        [HttpGet]
        public HttpResponseMessage DocumentUploadList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAInstitution.DaDocumentUploadList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentDelete(string sainstidocument_gid)
        {

            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAInstitution.DaUploadDocumentDelete(sainstidocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage UploadDocumentTmpList(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAInstitution.DaUploadDocumentTmpList(sacontactinstitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionTempClear")]
        [HttpGet]
        public HttpResponseMessage InstitutionTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RenewaldocumentList values = new RenewaldocumentList();
            objDaSAInstitution.DaInstitutionTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionCodePendingReport")]
        [HttpGet]
        public HttpResponseMessage InstitutionCodePendingReport()
        {
            MdlMstSAOnboardInstitution values = new MdlMstSAOnboardInstitution();
            objDaSAInstitution.DaInstitutionCodePendingReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionCodeCompletedReport")]
        [HttpGet]
        public HttpResponseMessage InstitutionCodecompletedReport()
        {
            MdlMstSAOnboardInstitution values = new MdlMstSAOnboardInstitution();
            objDaSAInstitution.DaInstitutionCodeCompletedReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionMakerRejected")]
        [HttpPost]
        public HttpResponseMessage InstitutionMakerRejected(MdlApprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaInstitutionMakerRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionCheckerRejected")]
        [HttpPost]
        public HttpResponseMessage InstitutionCheckerRejected(MdlApprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSAInstitution.DaInstitutionCheckerRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyInstitutionCheckerDistractSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyInstitutionCheckerDistractSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyInstitutionCheckerDistractSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSAVerfiyInstitutionMakerDistractSummary")]
        [HttpGet]
        public HttpResponseMessage GetSAVerfiyInstitutionMakerDistractSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetSAVerfiyInstitutionMakerDistractSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionDeferredSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionDeferredSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSAInstitution.DaGetInstitutionDeferredSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}

