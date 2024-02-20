using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using vcidex_kyc.Function;
using vcidex_kyc.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace vcidex_kyc.Controllers
{

    /// <summary>
    /// This Controllers will provide access to third party API for verifying the KYC details of the customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>

    [RoutePrefix("api/AgrKyc")]
    public class AgrKycController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [AllowAnonymous]
        [HttpPost]
        [ActionName("PANNumber")]
        public HttpResponseMessage PostPanNumber(PanNumberModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPanNumberDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("PANVerification")]
        public HttpResponseMessage PostPANVerification(PanVerificationModel Values) //
        {
            if (ModelState.IsValid)
            {
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPanVerificationDetails(Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("PANAadhaarLink")]
        public HttpResponseMessage PostPANAadhaarLink(PanNumberModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPANAaadharLinkDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("DrivingLicenseAuthentication")]
        public HttpResponseMessage PostDrivingLicenseAuthentication(DrivingLicenseModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetDrivingLicenseAuthenticationDtl(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("VoterID")]
        public HttpResponseMessage PostVoterIDDetails(VoterIDModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetVoterIDDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Passport")]
        public HttpResponseMessage PostPassport(PassportModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPassportDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GSTSBPAN")]
        public HttpResponseMessage PostGSTSBPAN(PanNumberModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.Getgstsbpandetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GSTVerification")]
        public HttpResponseMessage PostGSTVerification(GSTVerificationModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GSTVerificationDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GSPGSTReturnFiling")]
        public HttpResponseMessage PostGSPGSTReturnFiling(GSTVerificationModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetGSPGSTReturnFiling(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GSTAuthentication")]
        public HttpResponseMessage PostGSTDetailed(GSTAuthenticationModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetGSTDetailed(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("CompanyLLP")]
        public HttpResponseMessage PostCompanyLLP(CompanyLLP Values) //
        {
            if (ModelState.IsValid)
            {

                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetCompanyLLP(Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("CompanyLLP_no")]
        public HttpResponseMessage PostCompanyLLP_no(CIN Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetCompanyLLP_no(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("mcasignatories")]
        public HttpResponseMessage Postmcasignatories(MCASignatories Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.Getmcasignatories(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("uam")]
        public HttpResponseMessage Postuam(uam Values) //
        {
            if (ModelState.IsValid)
            {

                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.Getuam(Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("FSSAI")]
        public HttpResponseMessage PostFSSAI(Fssai Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetFssaidetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("fda")]
        public HttpResponseMessage Postfda(Fda Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetFdadetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("ebA")]
        public HttpResponseMessage PostebA(EBA Values) //
        {
            if (ModelState.IsValid)
            {

                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetebaDetails(Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("tele")]
        public HttpResponseMessage Posttele(TelephoneAuthentication Values) //
        {
            if (ModelState.IsValid)
            {

                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetTeleDetails(Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("BankAccVerification")]
        public HttpResponseMessage PostBankAccVerification(BankAccVerification Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetBankAccVerification(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("IfscVerification")]
        public HttpResponseMessage PostIfscVerification(IfscVerification Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetIfscVerification(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("ITRVOCR")]
        public HttpResponseMessage PostITRVOCR() //
        {
            if (ModelState.IsValid)
            {
                HttpRequest httpRequest;
                httpRequest = HttpContext.Current.Request;
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetITRVOCRDetails(httpRequest);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("ChequeOCR")]
        public HttpResponseMessage PostChequeOCR() //
        {
            if (ModelState.IsValid)
            {
                HttpRequest httpRequest;
                httpRequest = HttpContext.Current.Request;
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetChequeOCR(httpRequest);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("TANauthetication")]
        public HttpResponseMessage PostTANauthetication(tan Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPostTANauthetication(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("TANCompanyauthetication")]
        public HttpResponseMessage TANCompanyauthetication(tan Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPostTANCompanyauthetication(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [ActionName("IECDetailed")]
        public HttpResponseMessage PostIECDetailed(iec_detailed Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.PostIECDetailed(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("ShopAndEstablishment")]
        public HttpResponseMessage GetShopAndEstablishment(ShopAndEstablishmentRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetShopAndEstablishment(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("PropertyTax")]
        public HttpResponseMessage GetPropertyTax(PropertyTaxRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetPropertyTax(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("VehicleRCAuthAdvanced")]
        public HttpResponseMessage GetVehicleRCAuthAdvanced(VehicleRCAuthAdvancedRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetVehicleRCAuthAdvanced(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("VehicleRCSearch")]
        public HttpResponseMessage GetVehicleRCSearch(VehicleRCSearchRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetVehicleRCSearch(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("LPGIDAuthentication")]
        public HttpResponseMessage GetLPGIDAuthentication(LPGIDAuthenticationRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaAgrKyc ObjDaAgrKyc = new DaAgrKyc();
                var response = ObjDaAgrKyc.GetLPGIDAuthentication(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


    }
}