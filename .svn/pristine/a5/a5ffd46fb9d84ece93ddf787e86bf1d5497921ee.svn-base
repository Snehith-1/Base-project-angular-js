using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.iasn.Models;
using ems.iasn.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;

namespace ems.iasn.Controllers
{
    [RoutePrefix("api/IasnTrnAuditLog")]
    [Authorize]
    public class IasnTrnAuditLogController : ApiController
    {
        DaIasnAuditLog objDaAccess = new DaIasnAuditLog();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("AuditLog")]
        [HttpGet]
        public HttpResponseMessage AuditLog(string email_gid)
        {
            MdlAuditLogHistory values = new iasn.Models.MdlAuditLogHistory();
            objDaAccess.DaGetAuditLogSummary(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostAuditView")]
        [HttpGet]
        public HttpResponseMessage PostAuditView(string email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostAuditView (email_gid, getsessionvalues.user_gid );
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("ComposeAuditLog")]
        [HttpGet]
        public HttpResponseMessage ComposeAuditLog(string composemail_gid)
        {
            MdlAuditLogHistory values = new iasn.Models.MdlAuditLogHistory();
            objDaAccess.DaComposeAuditLog(composemail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostComposeAuditView")]
        [HttpGet]
        public HttpResponseMessage PostComposeAuditView(string composemail_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostComposeAuditView(composemail_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
