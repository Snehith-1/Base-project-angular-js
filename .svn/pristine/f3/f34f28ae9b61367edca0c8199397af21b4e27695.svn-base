using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.hbapiconn.Controllers
{
    [RoutePrefix("api/MobileSamAgroHBAPIConn")]
    [AllowAnonymous]
    public class MobileSamAgroHBAPIConnController : ApiController
    {
        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        string APIKeyConfigured = ConfigurationManager.AppSettings["Adaptor_APIkey"].ToString();
        IEnumerable<string> headerAPIkeyValues = null;
        dbconn objdbconn = new dbconn();
        string msSQL, lsinstitution_gid, lscontact_gid;
        bool postResult;
        
        [ActionName("PostBuyerToERP")]
        [HttpGet]
        public HttpResponseMessage PostBuyerToERP(string application_gid, string lsapprovalperson_name)
        {
            if (Request.Headers.TryGetValues("Adaptor_APIkey", out headerAPIkeyValues))
            {
                var secretKey = headerAPIkeyValues.First();
                if (!string.IsNullOrEmpty(secretKey) && APIKeyConfigured.Equals(secretKey))
                {
                    if (ModelState.IsValid)
                    {

                        MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();



                        msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                               " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                        lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
                           " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
                        lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

                        if (!String.IsNullOrEmpty(lsinstitution_gid))
                        {
                            objMdlHBAPIConnDAResponse = objFnSamAgroHBAPIConn.PostBuyerInstitutionHBAPI(application_gid, lsapprovalperson_name);
                        }
                        else if (!String.IsNullOrEmpty(lscontact_gid))
                        {
                            objMdlHBAPIConnDAResponse = objFnSamAgroHBAPIConn.PostBuyerContactHBAPI(application_gid, lsapprovalperson_name);
                        }


                        return Request.CreateResponse(HttpStatusCode.OK, objMdlHBAPIConnDAResponse);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
            }
          
            return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "API key is invalid.");
        }

        [ActionName("PostSupplierToERP")]
        [HttpGet]
        public HttpResponseMessage PostSupplierToERP(string application_gid, string lsapprovalperson_name)
        {
            if (Request.Headers.TryGetValues("Adaptor_APIkey", out headerAPIkeyValues))
            {
                var secretKey = headerAPIkeyValues.First();
                if (!string.IsNullOrEmpty(secretKey) && APIKeyConfigured.Equals(secretKey))
                {
                    if (ModelState.IsValid)
                    {
                        MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();


            msSQL = " select institution_gid from agr_mst_tsupronboard2institution" +
                   " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
            lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select contact_gid from agr_mst_tsupronboardcontact" +
               " where application_gid = '" + application_gid + "' and stakeholder_type='Applicant'";
            lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

            if (!String.IsNullOrEmpty(lsinstitution_gid))
            {
                objMdlHBAPIConnDAResponse = objFnSamAgroHBAPIConn.PostSupplierInstitutionHBAPI(application_gid, lsapprovalperson_name);
            }
            else if (!String.IsNullOrEmpty(lscontact_gid))
            {
                objMdlHBAPIConnDAResponse = objFnSamAgroHBAPIConn.PostSupplierContactHBAPI(application_gid, lsapprovalperson_name);
            }

            return Request.CreateResponse(HttpStatusCode.OK, objMdlHBAPIConnDAResponse);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "API key is invalid.");
        }   

        [ActionName("PostEmployeeToERP")]
        [HttpGet]
        public HttpResponseMessage PostEmployeeToERP(string employee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlHBAPIConnControllerResponse objMdlHBAPIConnControllerResponse = new MdlHBAPIConnControllerResponse();
           
             var postResult = objFnSamAgroHBAPIConn.PostEmployeeHBAPI(employee_gid);
            
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }
    }
}