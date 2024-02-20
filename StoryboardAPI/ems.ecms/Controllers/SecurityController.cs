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
    /// security Controller Class containing API methods for accessing the  DataAccess class DaSecurityType
    ///   Security Type - create, update, edit, delete, Active log, status update, view
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/security")]
    [Authorize]
    public class SecurityController : ApiController
    {
        DaSecurityType objDaSecurity = new DaSecurityType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("createSecurityType")]
        [HttpPost]
        public HttpResponseMessage CreateSecurityType(securitytype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSecurity.DaPostcreateSecurityType(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getSecuritytype")]
        [HttpGet]
        public HttpResponseMessage GetSecuritytype()
        {
            MdlSecurity objMdlsecuritytype = new MdlSecurity();
           objDaSecurity.DaGetSecuritytype(objMdlsecuritytype);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlsecuritytype);
        }

        [ActionName("GetSecurityTypeEdit")]
        [HttpGet]
        public HttpResponseMessage GetSecurityTypeEdit(string securitytype_gid)
        {
            securitytype values = new securitytype();
           objDaSecurity.DaGetEditSecuritytype (securitytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("securityTypeUpdate")]
        [HttpPost]
        public HttpResponseMessage SecurityTypeUpdate(securitytype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSecurity .DaPostUpdateSecurityType(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("securityTypeDelete")]
        [HttpGet]
        public HttpResponseMessage SecurityTypeDelete(string securitytype_gid)
        {
            securitytype values = new securitytype();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSecurity .DaPostDeleteSecuritytype(securitytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("securityTypeStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage securityTypeStatusUpdate(securitytype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSecurity.DasecurityTypeStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string securitytype_gid)
        {
            MdlSecurity values = new MdlSecurity();
            objDaSecurity.DaGetActiveLog(securitytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
