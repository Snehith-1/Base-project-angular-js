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
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

/// <summary>
/// (It's used for Kyc)Kyc Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Praveen Raj </remarks>
namespace vcidex_kyc.Controllers
{
    [RoutePrefix("api/Kyc")]
    public class KycController : ApiController
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetPanNumberDetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetPanVerificationDetails(Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetPANAaadharLinkDetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetDrivingLicenseAuthenticationDtl(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetVoterIDDetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetPassportDetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.Getgstsbpandetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GSTVerificationDetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetGSPGSTReturnFiling(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetGSTDetailed(getsessionvalues.employee_gid, Values);
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

                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetCompanyLLP(Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetCompanyLLP_no(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.Getmcasignatories(getsessionvalues.employee_gid, Values);
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

                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.Getuam(Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetFssaidetails(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetFdadetails(getsessionvalues.employee_gid, Values);
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

                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetebaDetails(Values);
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

                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetTeleDetails(Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetBankAccVerification(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetIfscVerification(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetITRVOCRDetails(httpRequest);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetChequeOCR(httpRequest);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetPostTANauthetication(getsessionvalues.employee_gid ,Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.PostIECDetailed(getsessionvalues.employee_gid,Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetShopAndEstablishment(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetPropertyTax(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetVehicleRCAuthAdvanced(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetVehicleRCSearch(getsessionvalues.employee_gid, Values);
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
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetLPGIDAuthentication(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("UDYAMNumber")]
        public HttpResponseMessage PostUdyamNumber(UdyamNumberModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaKyc ObjDaKyc = new DaKyc();
                var response = ObjDaKyc.GetUdyamNumberDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


    }
}
