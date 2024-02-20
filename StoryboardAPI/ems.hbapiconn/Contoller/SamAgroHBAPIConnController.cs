using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.hbapiconn.Controllers
{
    [RoutePrefix("api/SamAgroHBAPIConn")]
    [Authorize]
    public class SamAgroHBAPIConnController : ApiController
    {
        FnSamAgroHBAPIConn objFnSamAgroHBAPIConn = new FnSamAgroHBAPIConn();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        dbconn objdbconn = new dbconn();
        string msSQL, lsinstitution_gid, lscontact_gid, lsbuyeronboard_gid;
        bool postResult;
        string lsapplicant_type;
        
        [ActionName("PostBuyerToERP")]
        [HttpGet]
        public HttpResponseMessage PostBuyerToERP(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();

            msSQL = " select concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as approvalperson_name from adm_mst_tuser a " +
                   " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   " where b.employee_gid = '" + getsessionvalues.employee_gid + "'";
            string lsapprovalperson_name = objdbconn.GetExecuteScalar(msSQL);


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
        
        [ActionName("PostSupplierToERP")]
        [HttpGet]
        public HttpResponseMessage PostSupplierToERP(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();

            msSQL = " select concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as approvalperson_name from adm_mst_tuser a " +
                   " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                   " where b.employee_gid = '" + getsessionvalues.employee_gid + "'";
            string lsapprovalperson_name = objdbconn.GetExecuteScalar(msSQL);

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

        [ActionName("PostEmployeeToERP")]
        [HttpGet]
        public HttpResponseMessage PostEmployeeToERP(string employee_externalid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlHBAPIConnControllerResponse objMdlHBAPIConnControllerResponse = new MdlHBAPIConnControllerResponse();
             var postResult = objFnSamAgroHBAPIConn.PostEmployeeHBAPI(employee_externalid);
            
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

       
    }
}