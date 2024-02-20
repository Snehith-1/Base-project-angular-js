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
    [RoutePrefix("api/CADKyc")]
    public class CADKycController : ApiController
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetPanNumberDetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetPanVerificationDetails(Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetPANAaadharLinkDetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetDrivingLicenseAuthenticationDtl(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetVoterIDDetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetPassportDetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.Getgstsbpandetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GSTVerificationDetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetGSPGSTReturnFiling(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetGSTDetailed(getsessionvalues.employee_gid, Values);
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

                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetCompanyLLP(Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetCompanyLLP_no(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.Getmcasignatories(getsessionvalues.employee_gid, Values);
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

                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.Getuam(Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetFssaidetails(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetFdadetails(getsessionvalues.employee_gid, Values);
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

                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetebaDetails(Values);
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

                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetTeleDetails(Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetBankAccVerification(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetIfscVerification(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetITRVOCRDetails(httpRequest);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetChequeOCR(httpRequest);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetPostTANauthetication(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.PostIECDetailed(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetShopAndEstablishment(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetPropertyTax(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetVehicleRCAuthAdvanced(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetVehicleRCSearch(getsessionvalues.employee_gid, Values);
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
                DaCADKyc ObjDaCADKyc = new DaCADKyc();
                var response = ObjDaCADKyc.GetLPGIDAuthentication(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


    }
}
