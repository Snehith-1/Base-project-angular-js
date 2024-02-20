using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to add datas from various stages in Supplier Application creation (General, Company, Individual, Overall limit, Product, charges, trade, Bureau & Done)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>


    [RoutePrefix("api/AgrMstSuprApplicationAdd")]
    [Authorize]
    public class AgrMstSuprApplicationAddController : ApiController
    {
        DaAgrMstSuprApplicationAdd objMstApplicationAdd = new DaAgrMstSuprApplicationAdd();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
     
        
        //Get Events - Drop Down
        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDown values = new MdlDropDown();
            objMstApplicationAdd.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Events - Product&charges Drop Down
        [ActionName("GetproductDropDown")]
        [HttpGet]
        public HttpResponseMessage GetproductDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductDropDown values = new MdlProductDropDown();
            objMstApplicationAdd.DaGetproductDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Genetic Code
        [ActionName("GetGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetGeneticCodeList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGeneticCode values = new MdlGeneticCode();
            objMstApplicationAdd.DaGetGeneticCodeList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);           
        }
     
        //General Details Save
        [ActionName("SaveGeneralDtl")]
        [HttpPost]
        public HttpResponseMessage SaveGeneralDtl(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSaveGeneralDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGeneralDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitGeneralDtl(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSubmitGeneralDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Contact Person Mobile No

        [ActionName("PostMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetAppMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationAdd.DaGetAppMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete Mobile No----------//
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage deleteMmobileno(string application2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationAdd.DaDeleteMobileNo(application2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Contact Person Email Address

        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAppEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationAdd.DaGetAppEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string application2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationAdd.DaDeleteEmailAddress(application2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Social/Capital Trade

        [ActionName("SocialAndTradeCapitalSave")]
        [HttpPost]
        public HttpResponseMessage SocialAndTradeCapitalSave(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSocialAndTradeCapitalSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Genetic Code
        [ActionName("PostGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostGeneticCode(MdlMstGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGenetic")]
        [HttpGet]
        public HttpResponseMessage DeleteGenetic(string application2geneticcode_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGeneticCode values = new MdlMstGeneticCode();
            objMstApplicationAdd.DaDeleteGenetic(application2geneticcode_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Product and Charges
        [ActionName("PostLoanDtl")]
        [HttpPost]
        public HttpResponseMessage PostLoanDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostLoanDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteLoan")]
        [HttpPost]
        public HttpResponseMessage DeleteLoan (MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
          //  MdlMstLoanDtl values = new MdlMstLoanDtl();
            objMstApplicationAdd.DaDeleteLoan( values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBuyer")]
        [HttpPost]
        public HttpResponseMessage PostBuyer(MdlMstBuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCollateral")]
        [HttpPost]
        public HttpResponseMessage PostCollateral(MdlMstCollatertal values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostCollateral(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteCollateral")]
        [HttpGet]
        public HttpResponseMessage DeleteCollateral(string application2collateral_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCollatertal values = new MdlMstCollatertal();
            objMstApplicationAdd.DaDeleteCollateral(application2collateral_gid, values, getsessionvalues.employee_gid);
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
            objMstApplicationAdd.Dapostcollateraldocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("deletecollateraldoc")]
        [HttpGet]
        public HttpResponseMessage deletecollateraldoc(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objMstApplicationAdd.Dadeletecollateraldoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
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
            objMstApplicationAdd.DaPostHypoDoc(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("deleteHypoDoc")]
        [HttpGet]
        public HttpResponseMessage deleteHypoDoc(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Documentname objdocumentcancel = new Documentname();
            objMstApplicationAdd.DadeleteHypoDoc(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("PostHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteHypothecation")]
        [HttpGet]
        public HttpResponseMessage DeleteHypothecation(string application2hypothecation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objMstApplicationAdd.DaDeleteHypothecation(application2hypothecation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage GetLoanSubProduct(string loanproduct_gid,string application_gid,string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objMstApplicationAdd.DaGetLoanSubProduct(loanproduct_gid,application_gid, application2loan_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanDtl")]
        [HttpGet]
        public HttpResponseMessage GetLoanDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLoanDtl values = new MdlMstLoanDtl();
            objMstApplicationAdd.DaGetLoanDtl(  application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBuyerInfo")]
        [HttpGet]
        public HttpResponseMessage GetBuyerInfo(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objMstApplicationAdd.DaGetBuyerInfo(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteBuyer")]
        [HttpGet]
        public HttpResponseMessage DeleteBuyer(string application2buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBuyer values = new MdlMstBuyer();
            objMstApplicationAdd.DaDeleteBuyer(application2buyer_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteCharge")]
        [HttpGet]
        public HttpResponseMessage DeleteCharge(string application2servicecharge_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductCharges values = new MdlProductCharges();
            objMstApplicationAdd.DaDeleteCharge(application2servicecharge_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductCharges")]
        [HttpPost]
        public HttpResponseMessage PostProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSubmitProductCharges")]
        [HttpPost]
        public HttpResponseMessage PostSubmitProductCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostSubmitProductCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitApplication")]
        [HttpPost]
        public HttpResponseMessage SubmitApplication(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSubmitApplication(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Add Institution GST

        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGSTList")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGSTList(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution GST List

        [ActionName("GetInstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionGSTList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objMstApplicationAdd.DaGetInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution GST

        [ActionName("EditInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionGST(string institution2branch_gid)
        {
            MdlMstGST values = new MdlMstGST();
            objMstApplicationAdd.DaEditInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution GST

        [ActionName("UpdateInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution GST

        [ActionName("DeleteInstitutionGST")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionGST(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objMstApplicationAdd.DaDeleteInstitutionGST(institution2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Mobile No

        [ActionName("PostInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Mobile No

        [ActionName("GetInstitutionMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationAdd.DaGetInstitutionMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Mobile No

        [ActionName("EditInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionMobileNo(string institution2mobileno_gid)
        {
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationAdd.DaEditInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Institution Mobile No

        [ActionName("UpdateInstitutionMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionMobileNo(MdlMstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateInstitutionMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Mobile No

        [ActionName("DeleteInstitutionMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionMobileNo(string institution2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstMobileNo values = new MdlMstMobileNo();
            objMstApplicationAdd.DaDeleteInstitutionMobileNo(institution2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Email Address

        [ActionName("PostInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Email Address List

        [ActionName("GetInstitutionEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationAdd.DaGetInstitutionEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Email Address

        [ActionName("EditInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionEmailAddress(string institution2email_gid)
        {
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationAdd.DaEditInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Email Address

        [ActionName("UpdateInstitutionEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionEmailAddress(MdlMstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateInstitutionEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Email Address

        [ActionName("DeleteInstitutionEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionEmailAddress(string institution2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstEmailAddress values = new MdlMstEmailAddress();
            objMstApplicationAdd.DaDeleteInstitutionEmailAddress(institution2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution Address Details  

        [ActionName("PostInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostInstitutionAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Address List

        [ActionName("GetInstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationAdd.DaGetInstitutionAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution Address Details 

        [ActionName("EditInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionAddressDetail(string institution2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationAdd.DaEditInstitutionAddressDetail(institution2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution Address Details 

        [ActionName("UpdateInstitutionAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateInstitutionAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution Address Details 

        [ActionName("DeleteInstitutionAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionAddressDetail(string institution2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationAdd.DaDeleteInstitutionAddressDetail(institution2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Institution License Details

        [ActionName("PostInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostInstitutionLicenseDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution License List

        [ActionName("GetInstitutionLicenseList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionLicenseList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstApplicationAdd.DaGetInstitutionLicenseList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Institution License Details 

        [ActionName("EditInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage EditInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstApplicationAdd.DaEditInstitutionLicenseDetail(institution2licensedtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution License Details 

        [ActionName("UpdateInstitutionLicenseDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateInstitutionLicenseDetail(MdlMstLicenseDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateInstitutionLicenseDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Institution License Details 

        [ActionName("DeleteInstitutionLicenseDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionLicenseDetail(string institution2licensedtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstLicenseDetails values = new MdlMstLicenseDetails();
            objMstApplicationAdd.DaDeleteInstitutionLicenseDetail(institution2licensedtl_gid, values);
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
            institutionuploaddocument documentname = new institutionuploaddocument();
            objMstApplicationAdd.DaInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Institution Document Delete

        [ActionName("InstitutionDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionDocumentDelete(string institution2documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objMstApplicationAdd.DaInstitutionDocumentDelete(institution2documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        // Institution Form-60 Document Upload

        [ActionName("InstitutionForm_60DocumentUpload")]
        [HttpPost]
        public HttpResponseMessage InstitutionForm_60DocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            institutionuploaddocument documentname = new institutionuploaddocument();
            objMstApplicationAdd.DaInstitutionForm_60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        // Institution Form-60 Document Delete

        [ActionName("InstitutionForm_60DocumentDelete")]
        [HttpGet]
        public HttpResponseMessage InstitutionForm_60DocumentDelete(string institution2form60documentupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            institutionuploaddocument objfilename = new institutionuploaddocument();
            objMstApplicationAdd.DaInstitutionForm_60DocumentDelete(institution2form60documentupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        //Institution Details Save

        [ActionName("SaveInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage SaveInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSaveInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtl(MdlMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSubmitInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Temp Clear 

        [ActionName("GetIntitutionTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIntitutionTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstApplicationAdd.DaGetIntitutionTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Mobile Number Add 
        [ActionName("MobileNumberAdd")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAdd(MdlContactMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaMobileNumberAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Mobile No List
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objMstApplicationAdd.DaGetMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Mobile No----------//
        [ActionName("MobileNoDelete")]
        [HttpGet]
        public HttpResponseMessage MobileNoDelete(string contact2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactMobileNo values = new MdlContactMobileNo();
            objMstApplicationAdd.DaMobileNoDelete(contact2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Email Address Add 
        [ActionName("EmailAddressAdd")]
        [HttpPost]
        public HttpResponseMessage EmailAddressAdd(MdlContactEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaEmailAddressAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Email Address List
        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objMstApplicationAdd.DaGetEmailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmailAddressDelete")]
        [HttpGet]
        public HttpResponseMessage EmailAddressDelete(string contact2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactEmail values = new MdlContactEmail();
            objMstApplicationAdd.DaEmailAddressDelete(contact2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("AddressAdd")]
        [HttpPost]
        public HttpResponseMessage AddressAdd(MdlContactAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaAddressAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationAdd.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddressDelete")]
        [HttpGet]
        public HttpResponseMessage AddressDelete(string contact2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationAdd.DaAddressDelete(contact2address_gid, values);
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
            objMstApplicationAdd.DaIndividualProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProofList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstApplicationAdd.DaGetIndividualProofList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualProofDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualProofDelete(string contact2idproof_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactIdProof values = new MdlContactIdProof();
            objMstApplicationAdd.DaIndividualProofDelete(contact2idproof_gid, values);
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
            objMstApplicationAdd.DaIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage IndividualDocTempList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objMstApplicationAdd.DaGetIndividualDocList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage IndividualDocDelete(string contact2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactDocument values = new MdlContactDocument();
            objMstApplicationAdd.DaIndividualDocDelete(contact2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Save 
        [ActionName("IndividualSave")]
        [HttpPost]
        public HttpResponseMessage IndividualSave(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaIndividualSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Save 
        [ActionName("IndividualSubmit")]
        [HttpPost]
        public HttpResponseMessage IndividualSubmit(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaIndividualSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPostalCodeDetails")]
        [HttpGet]
        public HttpResponseMessage GetPostalCodeDetails(string postal_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstApplicationAdd.DaGetPostalCodeDetails(postal_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Temp Clear 
        [ActionName("GetIndividualTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIndividualTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstApplicationAdd.GetIndividualTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCICUploadIndividual")]
        [HttpPost]
        public HttpResponseMessage PostCICUploadIndividual(MdlCICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostCICUploadIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCICUploadInstitution")]
        [HttpPost]
        public HttpResponseMessage PostCICUploadInstitution(MdlCICInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostCICUploadInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICIndividualDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CICIndividualDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstApplicationAdd.DaCICIndividualDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("CICInstitutionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CICInstitutionDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objMstApplicationAdd.DaCICInstitutionDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("CICUploadIndividualDocTempList")]
        [HttpGet]
        public HttpResponseMessage CICUploadIndividualDocTempList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationAdd.DaCICUploadIndividualDocTempList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CICUploadInstitutionDocTempList")]
        [HttpGet]
        public HttpResponseMessage CICUploadInstitutionDocTempList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationAdd.DaCICUploadInstitutionDocTempList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("TempCICUploadIndividualDocDelete")]
        [HttpGet]
        public HttpResponseMessage TempCICUploadIndividualDocDelete(string tmpcicdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationAdd.DaTempCICUploadIndividualDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempCICUploadInstitutionDocDelete")]
        [HttpGet]
        public HttpResponseMessage TempCICUploadInstitutionDocDelete(string tmpcicdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationAdd.DaTempCICUploadInstitutionDocDelete(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCICIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetCICIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationAdd.DaGetCICIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCICInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetCICInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationAdd.DaGetCICInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual();
            objMstApplicationAdd.DaGetIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationAdd.DaGetInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOverallInfo")]
        [HttpGet]
        public HttpResponseMessage GetOverallInfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetOverallInfo(application_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetGeneralInfo()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetGeneralInfo( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApplicationSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetApplicationSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGeneral")]
        [HttpGet]
        public HttpResponseMessage DeleteGeneral(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaDeleteGeneral(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Deleteindividual")]
        [HttpGet]
        public HttpResponseMessage Deleteindividual(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICIndividual values = new MdlCICIndividual ();
            objMstApplicationAdd.DaDeleteindividual(contact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Deleteinstitution")]
        [HttpGet]
        public HttpResponseMessage Deleteinstitution(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCICInstitution values = new MdlCICInstitution();
            objMstApplicationAdd.DaDeleteinstitution(institution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductList")]
        [HttpGet]
        public HttpResponseMessage GetProductList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetProductList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Institution Edit Summmary

        [ActionName("GetInstitutionEditSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEditSummary(string application_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objMstApplicationAdd.DaGetInstitutionEditSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Product Charges Edit Summmary

        [ActionName("GetProductChargesEditSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesEditSummary(string application_gid)
        {
            MdlProductCharges values = new MdlProductCharges();
            objMstApplicationAdd.DaGetProductChargesEditSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSocialTradeSummary")]
        [HttpGet]
        public HttpResponseMessage GetSocialTradeSummary(string application_gid)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetSocialTradeSummary(application_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Temp Clear
        [ActionName("GetAppTempClear")]
        [HttpGet]
        public HttpResponseMessage GetAppTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetAppTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProceed")]
        [HttpGet]
        public HttpResponseMessage GetProceed()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetProceed(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAppProceed")]
        [HttpPost]
        public HttpResponseMessage PostAppProceed(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostAppProceed(getsessionvalues.employee_gid,getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppAssignmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppAssignmentSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetAppAssignmentSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempApp")]
        [HttpGet]
        public HttpResponseMessage GetTempApp()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstApplicationAdd.GetTempApp(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteApplicationAdd")]
        [HttpGet]
        public HttpResponseMessage DeleteApplicationAdd(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaDeleteApplicationAdd(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppSocialTradeSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppSocialTradeSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetAppSocialTradeSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppProductcharges")]
        [HttpGet]
        public HttpResponseMessage GetAppProductcharges()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetAppProductcharges(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetDOB")]
        [HttpGet]
        public HttpResponseMessage GetDOB(string age)
        {
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetDOB(age, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ProductchargesTmpClear")]
        [HttpGet]
        public HttpResponseMessage ProductchargesTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaProductchargesTmpClear( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostcheckDate")]
        [HttpPost]
        public HttpResponseMessage PostcheckDate(MdlMstInstitutionAdd values)
        {
            objMstApplicationAdd.DaPostcheckDate(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitOverallLimit")]
        [HttpPost]
        public HttpResponseMessage SubmitOverallLimit(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaSubmitOverallLimit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostServiceCharges")]
        [HttpPost]
        public HttpResponseMessage PostServiceCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostServiceCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLimit")]
        [HttpGet]
        public HttpResponseMessage GetLimit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetLimit(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getproduct")]
        [HttpGet]
        public HttpResponseMessage Getproduct(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlList values = new MdlList();
            objMstApplicationAdd.DaGetproduct(application_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetServiceCharge")]
        [HttpGet]
        public HttpResponseMessage GetServiceCharge()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductCharges values = new MdlProductCharges();
            objMstApplicationAdd.DaGetServiceCharge(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAddHypothecation")]
        [HttpGet]
        public HttpResponseMessage GetAddHypothecation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHypothecation values = new MdlMstHypothecation();
            objMstApplicationAdd.DaGetHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Group Address Details  

        [ActionName("PostGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostGroupAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Group Address List

        [ActionName("GetGroupAddressList")]
        [HttpGet]
        public HttpResponseMessage GetGroupAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationAdd.DaGetGroupAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Group Address Details 

        [ActionName("EditGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupAddressDetail(string group2address_gid)
        {
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationAdd.DaEditGroupAddressDetail(group2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Group Address Details 

        [ActionName("UpdateGroupAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupAddressDetail(MdlMstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateGroupAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Group Address Details 

        [ActionName("DeleteGroupAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupAddressDetail(string group2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstApplicationAdd.DaDeleteGroupAddressDetail(group2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Add Group Bank Details  

        [ActionName("PostGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage PostGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostGroupBankDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Group Bank List

        [ActionName("GetGroupBankList")]
        [HttpGet]
        public HttpResponseMessage GetGroupBankList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstApplicationAdd.DaGetGroupBankList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Group Bank Details 

        [ActionName("EditGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage EditGroupBankDetail(string group2bank_gid)
        {
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstApplicationAdd.DaEditGroupBankDetail(group2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Group Address Details 

        [ActionName("UpdateGroupBankDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupBankDetail(MdlMstBankDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateGroupBankDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Group Bank Details 

        [ActionName("DeleteGroupBankDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupBankDetail(string group2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstBankDetails values = new MdlMstBankDetails();
            objMstApplicationAdd.DaDeleteGroupBankDetail(group2bank_gid, getsessionvalues.employee_gid, values);
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
            objMstApplicationAdd.DaGroupDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetGroupDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objMstApplicationAdd.DaGetGroupDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentDelete(string group2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGroupDocument values = new MdlGroupDocument();
            objMstApplicationAdd.DaGroupDocumentDelete(group2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        //Group Save

        [ActionName("GroupSave")]
        [HttpPost]
        public HttpResponseMessage GroupSave(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaGroupSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Submit

        [ActionName("GroupSubmit")]
        [HttpPost]
        public HttpResponseMessage GroupSubmit(MdlMstGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaGroupSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGroup values = new MdlMstGroup();
            objMstApplicationAdd.DaGetGroupSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Clear 

        [ActionName("GetGroupTempClear")]
        [HttpGet]
        public HttpResponseMessage GetGroupTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstApplicationAdd.DaGetGroupTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Grop List
        [ActionName("GetGroupList")]
        [HttpGet]
        public HttpResponseMessage GetGroupList(string application_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objMstApplicationAdd.DaGetGroupList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Contact Grop List
        [ActionName("GetContactGroupList")]
        [HttpGet]
        public HttpResponseMessage GetContactGroupList(string contact_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objMstApplicationAdd.DaGetContactGroupList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGSTState")]
        [HttpGet]
        public HttpResponseMessage GetGSTState(string gst_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objMstApplicationAdd.DaGetGSTState(gst_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("FutureDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            result values = new result();
            objMstApplicationAdd.DaFutureDateCheck(date, values);
           
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Company List
        [ActionName("GetCompanyList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyList(string application_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objMstApplicationAdd.DaGetCompanyList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Get Contact Company List
        [ActionName("GetContactCompanyList")]
        [HttpGet]
        public HttpResponseMessage GetContactCompanyList(string contact_gid)
        {
            MdlDropDown values = new MdlDropDown();
            objMstApplicationAdd.DaGetContactCompanyList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGSTInstitution")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTInstitution(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objMstApplicationAdd.DaDeleteGSTInstitution(getsessionvalues.employee_gid, institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       [ActionName("ImportExcelIndividual")]
        [HttpPost]
        public HttpResponseMessage ImportExcelIndividual()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstApplicationAdd.DaImportExcelIndividual(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ImportExcelInstitution")]
        [HttpPost]
        public HttpResponseMessage ImportExcelInstitution()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstApplicationAdd.DaImportExcelInstitution(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ImportExcelGroup")]
        [HttpPost]
        public HttpResponseMessage ImportExcelGroup()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objMstApplicationAdd.DaImportExcelGroup(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        //Get New Application

        [ActionName("GetApplicationNewSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationNewSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetApplicationNewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Rejected Applicaiton
        [ActionName("GetApplicationRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetApplicationRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Hold Applicaiton
        [ActionName("GetApplicationHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetApplicationHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Approved Applicaiton
        [ActionName("GetApplicationApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationapprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetApplicationApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ApplicationCount")]
        [HttpGet]
        public HttpResponseMessage ApplicationCount()
        {
            ApplicationCount values = new ApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppAssignedAssignmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppAssignedAssignmentSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetAppAssignedAssignmentSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppPendingAssignmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppPendingAssignmentSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetAppPendingAssignmentSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AssignApplicationCount")]
        [HttpGet]
        public HttpResponseMessage AssignApplicationCount()
        {
            AssignApplicationCount values = new AssignApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaAssignApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 
        [ActionName("GetApplSubmittedToCCSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplSubmittedToCCSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationAdd.DaGetApplSubmittedToCCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }     

        //Get Contact Bureau List
        [ActionName("GetContactBureauList")]
        [HttpGet]
        public HttpResponseMessage GetContactBureauList(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objMstApplicationAdd.DaGetContactBureauList(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteGroup(string group_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGroup values = new MdlMstGroup();
            objMstApplicationAdd.DaDeleteGroup(group_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("DeleteContactBureau")]
        [HttpGet]
        public HttpResponseMessage DeleteContactBureau(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objMstApplicationAdd.DaDeleteContactBureau(contact2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("UpdateCICUploadIndividual")]
        [HttpPost]
        public HttpResponseMessage UpdateCICUploadIndividual(MdlCICIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateCICUploadIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Institution
        [ActionName("GetInstitutionBureauList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionBureauList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlInstitutionBureau values = new MdlInstitutionBureau();
            objMstApplicationAdd.DaGetInstitutionBureauList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteInstitutionBureau")]
        [HttpGet]
        public HttpResponseMessage DeleteInstitutionBureau(string institution2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlInstitutionBureau values = new MdlInstitutionBureau();
            objMstApplicationAdd.DaDeleteInstitutionBureau(institution2bureau_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateCICUploadInstitution")]
        [HttpPost]
        public HttpResponseMessage UpdateCICUploadInstitution(MdlCICInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateCICUploadInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualBureauTempClear")]
        [HttpGet]
        public HttpResponseMessage GetIndividualBureauTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstApplicationAdd.DaGetIndividualBureauTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionBureauTempClear")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionBureauTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstApplicationAdd.DaGetInstitutionBureauTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalHierarchyFlag")]
        [HttpGet]
        public HttpResponseMessage GetApprovalHierarchyFlag(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlApprovalHierarchy values = new MdlApprovalHierarchy();
            objMstApplicationAdd.DaGetApprovalHierarchyFlag(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalHierarchyChangeList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalHierarchyChangeList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlApprovalHierarchyChange values = new MdlApprovalHierarchyChange();
            objMstApplicationAdd.DaGetApprovalHierarchyChangeList(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateApprovalHierarchyChange")]
        [HttpPost]
        public HttpResponseMessage UpdateApprovalHierarchyChange(MdlMstUpdateApproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateApprovalHierarchyChange(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSectorcategory")]
        [HttpGet]
        public HttpResponseMessage GetSectorcategory(string product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSectorcategory values = new MdlSectorcategory();
            objMstApplicationAdd.DaGetSectorcategory(getsessionvalues.employee_gid, product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVarietyDtl")]
        [HttpGet]
        public HttpResponseMessage GetVarietyDtl(string variety_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlSectorcategory values = new MdlSectorcategory();
            objMstApplicationAdd.DaGetVarietyDtl(getsessionvalues.employee_gid, variety_gid, values);
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
            objMstApplicationAdd.DaPANForm60DocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("PANForm60Delete")]
        [HttpGet]
        public HttpResponseMessage PANForm60Delete(string contact2panform60_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objMstApplicationAdd.DaPANForm60Delete(contact2panform60_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANForm60List")]
        [HttpGet]
        public HttpResponseMessage GetPANForm60List()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactPANForm60 values = new MdlContactPANForm60();
            objMstApplicationAdd.DaGetPANForm60List(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PANAbsenceReasonList")]
        [HttpGet]
        public HttpResponseMessage PANAbsenceReasonList()
        {
            MdlPANAbsenceReason objMdlPANAbsenceReason = new MdlPANAbsenceReason();
            objMstApplicationAdd.DaPANAbsenceReasonList(objMdlPANAbsenceReason);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlPANAbsenceReason);
        }

        [ActionName("PostPANAbsenceReasons")]
        [HttpPost]
        public HttpResponseMessage PostPANAbsenceReasons(MdlPANAbsenceReason values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostPANAbsenceReasons(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PANReasonsCheck")]
        [HttpGet]
        public HttpResponseMessage PANReasonsCheck()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAbsenceReason values = new MdlPANAbsenceReason();
            objMstApplicationAdd.DaPANReasonsCheck(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProductDetailAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDetailAdd(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostProductDetailAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductDetailList")]
        [HttpGet]
        public HttpResponseMessage GetProductDetailList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstApplicationAdd.DaGetProductDetailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteProductDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteProductDetail(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailAdd values = new MdlMstProductDetailAdd();
            objMstApplicationAdd.DaDeleteProductDetail(application2product_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProduct")]
        [HttpPost]
        public HttpResponseMessage PostProduct(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductDtlList")]
        [HttpGet]
        public HttpResponseMessage GetProductDtlList(string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstApplicationAdd.DaGetProductDtlList(application2loan_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductDtlAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDtlAdd(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostProductDtlAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Trade Details - START

        [ActionName("GetScopedtl")]
        [HttpGet]
        public HttpResponseMessage GetScopedtl()
        {
            MdlScopeList values = new MdlScopeList();
            objMstApplicationAdd.DaGetScopedtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTradedtl")]
        [HttpPost]
        public HttpResponseMessage PostTradedtl(MdlTradedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostTradedtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetApplicationTradeList")]
        //[HttpGet]
        //public HttpResponseMessage GetApplicationTradeList(string application_gid)
        //{ 
        //    MdlTradeList values = new MdlTradeList();
        //    objMstApplicationAdd.DaGetApplicationTradeList(application_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("GetApplicationTradeList")]
        [HttpGet]
        public HttpResponseMessage GetApplicationTradeList(string application_gid,string tmp_status)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTradeList values = new MdlTradeList();  
            objMstApplicationAdd.DaGetApplicationTradeList(application_gid,getsessionvalues.employee_gid, tmp_status, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApplicationTradeViewdtl")]
        [HttpGet]
        public HttpResponseMessage GetApplicationTradeViewdtl(string application2trade_gid)
        {
            MdlTradedtl values = new MdlTradedtl();
            objMstApplicationAdd.DaGetApplicationTradeViewdtl(application2trade_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateTradeDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateTradeDtl(MdlTradedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaUpdateTradeDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTradeDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteTradeDtl(string application2trade_gid)
        { 
            result values = new result();
            objMstApplicationAdd.DaDeleteTradeDtl(application2trade_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TradeTmpClear")]
        [HttpGet]
        public HttpResponseMessage TradeTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objMstApplicationAdd.DaTradeTmpClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("GetTradeproduct")]
        [HttpGet]
        public HttpResponseMessage GetTradeproduct(string application_gid)
        { 
            MdlList values = new MdlList();
            objMstApplicationAdd.DaGetTradeproduct(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Trade Details - END

        // Customer Info - Rating Add START

        [ActionName("PostRatingdtl")]
        [HttpPost]
        public HttpResponseMessage PostRatingdtl(MdlRatingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationAdd.DaPostRatingdtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionRatingList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionRatingList(string institution_gid, string tmp_status)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRatingList values = new MdlRatingList();
            objMstApplicationAdd.DaGetInstitutionRatingList(institution_gid, getsessionvalues.employee_gid, tmp_status, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRatingDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteRatingDtl(string institution2ratingdetail_gid)
        {
            result values = new result();
            objMstApplicationAdd.DaDeleteRatingDtl(institution2ratingdetail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Customer Info - Rating Add END

        // Product - All the product & Commodity
        [ActionName("Getmilestonepaymentdtl")]
        [HttpGet]
        public HttpResponseMessage Getmilestonepaymentdtl()
        {
            MdlmilestonepaymentList values = new MdlmilestonepaymentList();
            objMstApplicationAdd.DaGetmilestonepaymentdtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetNatureFormStateofCommodity")]
        [HttpGet]
        public HttpResponseMessage GetNatureFormStateofCommodity()
        {
            NatureFormStateofCommodityList values = new NatureFormStateofCommodityList();
            objMstApplicationAdd.DaGetNatureFormStateofCommodity(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetChargeproduct")]
        [HttpGet]
        public HttpResponseMessage DaGetChargeproduct(string application_gid)
        {
            MdlList values = new MdlList();
            objMstApplicationAdd.DaGetChargeproduct(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOnboardAppValidatePANAadhar")]
        [HttpPost]
        public HttpResponseMessage GetOnboardAppValidatePANAadhar(MdlonboardValidatedtl values)
        {
            objMstApplicationAdd.DaGetOnboardAppValidatePANAadhar(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}

