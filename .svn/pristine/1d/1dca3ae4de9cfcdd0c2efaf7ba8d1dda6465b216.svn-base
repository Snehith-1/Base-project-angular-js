using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;
using ems.ecms.DataAccess;
using ems.utilities.Functions;

using ems.utilities.Models;


namespace ems.ecms.Controllers
{
    /// <summary>
    /// covenanttype Controller Class containing API methods for accessing the  DataAccess class DaCovenanttype 
    /// To create covenant type, update covenat type, delete covenant type, summary of covenant type
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/covenanttype")]
    [Authorize]
    public class CovenanttypeController : ApiController
    {
       
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCovenanttype objDaCovenantType = new DaCovenanttype();
        [ActionName("createcovenanttype")]
        [HttpPost]
        public HttpResponseMessage Createcovenanttype(mdlcreateconvenantType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCovenantType.DaPostCreateCovenanttype(values,getsessionvalues.employee_gid );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getcovenanttype")]
        [HttpGet]
        public HttpResponseMessage Getcovenanttype()
        {
            MdlCovenanttype objcovenanttype = new MdlCovenanttype();
            objDaCovenantType .DaGetCovenanttype(objcovenanttype);
            return Request.CreateResponse(HttpStatusCode.OK, objcovenanttype);
        }

        [ActionName("GetCovenantTypeupdate")]
        [HttpGet]
        public HttpResponseMessage GetCovenantTypeupdate(string covenanttype_gid)
        {
            covenantypeedit values = new covenantypeedit();
            objDaCovenantType.DaGetEditCovenanttype(covenanttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("covenantTypeUpdate")]
        [HttpPost]
        public HttpResponseMessage CovenantTypeUpdate(covenantypeedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCovenantType .DaPostUpdateCovenanttype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("covenantTypeDelete")]
        [HttpGet]
        public HttpResponseMessage CovenantTypeDelete(string covenanttype_gid)
        {
            covenantypeedit values = new covenantypeedit();
           objDaCovenantType.DaPostDeleteCovenanttype(covenanttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}
