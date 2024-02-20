using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Web;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to Amendment master to Add, edit, view, active, inactive the records
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>


    [RoutePrefix("api/AgrMstAmendment")]
    [Authorize]
    public class AgrMstAmendmentController : ApiController
    {
        DaAgrMstAmendment objDaAgrMstAmendment = new DaAgrMstAmendment();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetAmendment")]
        [HttpGet]
        public HttpResponseMessage GetAmendment()
        {
            MdlAgrMstAmendment objamendment = new MdlAgrMstAmendment();
            objDaAgrMstAmendment.DaGetAmendment(objamendment);
            return Request.CreateResponse(HttpStatusCode.OK, objamendment);
        }

        [ActionName("CreateAmendment")]
        [HttpPost]
        public HttpResponseMessage CreateAmendment(amendmentlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstAmendment.DaCreateAmendment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAmendment")]
        [HttpGet]
        public HttpResponseMessage EditAmendment(string amendment_gid)
        {
            amendmentlist values = new amendmentlist();
            objDaAgrMstAmendment.DaEditAmendment(amendment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAmendment")]
        [HttpPost]
        public HttpResponseMessage UpdateAmendment(amendmentlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstAmendment.DaUpdateAmendment(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveAmendment")]
        [HttpPost]
        public HttpResponseMessage InactiveAmendment(amendmentlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstAmendment.DaInactiveAmendment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveAmendmentHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveAmendmentHistory(string amendment_gid)
        {
            AmendmentInactiveHistory objamendmenthistory = new AmendmentInactiveHistory();
            objDaAgrMstAmendment.DaInactiveAmendmentHistory(objamendmenthistory, amendment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objamendmenthistory);
        }
        [ActionName("DeleteAmendment")]
        [HttpGet]
        public HttpResponseMessage DeleteAmendment(string amendment_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstAmendment.DaDeleteAmendment(amendment_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }        
    }
}