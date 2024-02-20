using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.hbapiconn.Contoller
{
    /// <summary>
    /// Controller containing APIs for connecting to HyperbridgeAPI for posting Contract(SamAgro) to External ERP.
    /// </summary>
    /// <remarks>Written by Praveen Raj</remarks>
    [RoutePrefix("api/SamAgroHBAPIContract")]
    [Authorize]
    public class SamAgroHBAPIContractController : ApiController
    {
        FnSamAgroHBAPIContract objFnSamAgroHBAPIContract = new FnSamAgroHBAPIContract();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        dbconn objdbconn = new dbconn();
        string msSQL, lsinstitution_gid, lscontact_gid, lsbuyeronboard_gid;
        bool postResult;
        string lsapplicant_type;

        [ActionName("PostContractToERP")]
        [HttpGet]
        public HttpResponseMessage PostContractToERP(string application_gid)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();

            msSQL = " select buyeronboard_gid from agr_mst_tapplication" +
                 " where application_gid = '" + application_gid + "'";
            lsbuyeronboard_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select institution_gid from agr_mst_tbyronboard2institution" +
                   " where application_gid = '" + lsbuyeronboard_gid + "' and stakeholder_type='Applicant'";
            lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select contact_gid from agr_mst_tbyronboardcontact" +
               " where application_gid = '" + lsbuyeronboard_gid + "' and stakeholder_type='Applicant'";
            lscontact_gid = objdbconn.GetExecuteScalar(msSQL);

            if (!String.IsNullOrEmpty(lsinstitution_gid))
            {
                lsapplicant_type = "Institution";
            }
            else if (!String.IsNullOrEmpty(lscontact_gid))
            {
                lsapplicant_type = "Individual";
            }

            objMdlHBAPIConnDAResponse = objFnSamAgroHBAPIContract.PostContractHBAPI(application_gid, lsbuyeronboard_gid);

            return Request.CreateResponse(HttpStatusCode.OK, objMdlHBAPIConnDAResponse);
        }

        [ActionName("limit")]
        [HttpGet]
        public HttpResponseMessage limit(string buyeronboard_gid)
        {
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();

            

            objFnSamAgroHBAPIContract.fnNsLimitAPI(buyeronboard_gid);

            return Request.CreateResponse(HttpStatusCode.OK, objMdlHBAPIConnDAResponse);
        }

    }
}