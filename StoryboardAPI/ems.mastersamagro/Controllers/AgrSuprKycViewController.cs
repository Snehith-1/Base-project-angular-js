﻿using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Data.Odbc;
using Newtonsoft.Json;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to view the verified and unverified KYC details of the customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>

    [RoutePrefix("api/AgrSuprKycView")]
    [Authorize]

    public class AgrSuprKycViewController : ApiController
    {
        DaAgrKycView objDaKycView = new DaAgrKycView();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        string msSQL;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;


        [ActionName("GetPANAuthenticationDtl")]
        [HttpGet]
        public HttpResponseMessage GetPANAuthenticationDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAuthentication values = new MdlPANAuthentication();
            objDaKycView.DaGetPANAuthenticationDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPANAadhaarLinkDtl")]
        [HttpGet]
        public HttpResponseMessage GetPANAadhaarLinkDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPANAadhaarLink values = new MdlPANAadhaarLink();
            objDaKycView.DaGetPANAadhaarLinkDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDLAuthenticationDtl")]
        [HttpGet]
        public HttpResponseMessage GetDLAuthenticationDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDLAuthentication values = new MdlDLAuthentication();
            objDaKycView.DaGetDLAuthenticationDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEPICAuthenticationDtl")]
        [HttpGet]
        public HttpResponseMessage GetEPICAuthenticationDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEPICAuthentication values = new MdlEPICAuthentication();
            objDaKycView.DaGetEPICAuthenticationDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetIFSCAuthenticationDtl")]
        [HttpGet]
        public HttpResponseMessage GetIFSCAuthenticationDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIFSCAuthentication values = new MdlIFSCAuthentication();
            objDaKycView.DaGetIFSCAuthenticationDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankAccVerificationDtl")]
        [HttpGet]
        public HttpResponseMessage GetBankAccVerificationDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccVerification values = new MdlBankAccVerification();
            objDaKycView.DaGetBankAccVerificationDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetGSTSBPANDtl")]
        [HttpGet]
        public HttpResponseMessage GetGSTSBPANDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGSTSBPAN values = new MdlGSTSBPAN();
            objDaKycView.DaGetGSTSBPANDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPassportAuthenticationDtl")]
        [HttpGet]
        public HttpResponseMessage GetPassportAuthenticationDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPassportAuthentication values = new MdlPassportAuthentication();
            objDaKycView.DaGetPassportAuthenticationDtl(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DrivingLicenseViewDetails")]
        [HttpGet]
        public HttpResponseMessage DrivingLicenseViewDetails(string kycdlauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDrivingLicenseDetails values = new MdlDrivingLicenseDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_mst_tsuprkycdlauthentication where kycdlauthentication_gid='" + kycdlauthentication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlDrivingLicenseDetails>(objODBCDatareader["response"].ToString());

            }
            objODBCDatareader.Close();
            //   objDaKycView.DaDrivingLicenseViewDetails(kycdlauthentication_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VoterIDViewDetails")]
        [HttpGet]
        public HttpResponseMessage VoterIDViewDetails(string kycepicauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVoterIDDetails values = new MdlVoterIDDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_mst_tsuprkycepicauthentication where kycepicauthentication_gid='" + kycepicauthentication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlVoterIDDetails>(objODBCDatareader["response"].ToString());

            }
            objODBCDatareader.Close();
            //   objDaKycView.DaVoterIDViewDetails(kycepicauthentication_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PassportViewDetails")]
        [HttpGet]
        public HttpResponseMessage PassportViewDetails(string kycpassportauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPassportDetails values = new MdlPassportDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_mst_tsuprkycpassportauthentication where kycpassportauthentication_gid='" + kycpassportauthentication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlPassportDetails>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaKycView.DaPassportViewDetails(kycpassportauthentication_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GSTSBPANViewDetails")]
        [HttpGet]
        public HttpResponseMessage GSTSBPANViewDetails(string kycgstsbpan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGSTSBPANDetails values = new MdlGSTSBPANDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_mst_tsuprkycgstsbpan where kycgstsbpan_gid='" + kycgstsbpan_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlGSTSBPANDetails>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaKycView.DaGSTSBPANViewDetails(kycgstsbpan_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IFSCViewDetails")]
        [HttpGet]
        public HttpResponseMessage IFSCViewDetails(string kycifscauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIfscVerificationDetails values = new MdlIfscVerificationDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_mst_tsuprkycifscauthentication where kycifscauthentication_gid='" + kycifscauthentication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlIfscVerificationDetails>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaKycView.DaIFSCViewDetails(kycgstsbpan_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BankAccViewDetails")]
        [HttpGet]
        public HttpResponseMessage BankAccViewDetails(string kycbankaccverification_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAccVerificationDetails values = new MdlBankAccVerificationDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_mst_tsuprkycbankaccverification where kycbankaccverification_gid='" + kycbankaccverification_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlBankAccVerificationDetails>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaKycView.DaBankAccViewDetails(kycbankaccverification_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}

