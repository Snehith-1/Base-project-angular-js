using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.hbapiconn.Contoller
{
    [RoutePrefix("api/SamAgroHAPIOtherCreditor")]
    [Authorize]
    public class SamAgroHAPIOtherCreditorController : ApiController
    {
        FnSamAgroHAPIOtherCreditor objFnSamAgroHAPIOtherCreditor = new FnSamAgroHAPIOtherCreditor();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        dbconn objdbconn = new dbconn();
        string msSQL, lsinstitution_gid, lscontact_gid;
        bool postResult;
        string lsApplicant_category;

        //Other Creditor Approve Event

        [ActionName("PostOtherCreditorAddHAPI")]
        [HttpGet]
        public HttpResponseMessage PostOtherCreditorAddHAPI(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlHBAPIConnDAResponse objMdlHBAPIConnDAResponse = new MdlHBAPIConnDAResponse();
            objMdlHBAPIConnDAResponse = objFnSamAgroHAPIOtherCreditor.DaPostOtherCreditorAddHAPI(creditor_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlHBAPIConnDAResponse);

        }
    }
}