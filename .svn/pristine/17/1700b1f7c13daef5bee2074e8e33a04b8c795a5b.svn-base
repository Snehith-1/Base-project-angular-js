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

    [RoutePrefix("api/AgrSuprKyc")]
    public class AgrSuprKycController : ApiController
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetPanNumberDetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetPanVerificationDetails(Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetPANAaadharLinkDetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetDrivingLicenseAuthenticationDtl(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetVoterIDDetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetPassportDetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.Getgstsbpandetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GSTVerificationDetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetGSPGSTReturnFiling(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetGSTDetailed(getsessionvalues.employee_gid, Values);
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

                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetCompanyLLP(Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetCompanyLLP_no(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.Getmcasignatories(getsessionvalues.employee_gid, Values);
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

                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.Getuam(Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetFssaidetails(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetFdadetails(getsessionvalues.employee_gid, Values);
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

                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetebaDetails(Values);
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

                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetTeleDetails(Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetBankAccVerification(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetIfscVerification(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetITRVOCRDetails(httpRequest);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetChequeOCR(httpRequest);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetPostTANauthetication(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.PostIECDetailed(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetShopAndEstablishment(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetPropertyTax(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetVehicleRCAuthAdvanced(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetVehicleRCSearch(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrSuprKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrSuprKyc.GetLPGIDAuthentication(getsessionvalues.employee_gid, Values);
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
                DaAgrSuprKyc ObjDaAgrKyc = new DaAgrSuprKyc();
                var response = ObjDaAgrKyc.GetPostTANCompanyauthetication(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


    }
}