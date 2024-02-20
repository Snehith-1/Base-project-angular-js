using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.audit.Controllers
{
    [RoutePrefix("api/AtmMstAuditType")]
    [Authorize]
    public class AtmMstAuditTypeController : ApiController
    {
        DaAtmMstAuditType objDaAtmMstAuditType = new DaAtmMstAuditType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetAuditType")]
        [HttpGet]
        public HttpResponseMessage GetAuditType()
        {
            MdlAtmMstAuditType values = new MdlAtmMstAuditType();
            objDaAtmMstAuditType.DaGetAuditType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateAuditType")]
        [HttpPost]
        public HttpResponseMessage CreateAuditType(MdlAtmMstAuditType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditType.DaCreateAuditType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditType")]
        [HttpGet]
        public HttpResponseMessage EditAuditType(string audittype_gid)
        {
            MdlAtmMstAuditType values = new MdlAtmMstAuditType();
            objDaAtmMstAuditType.DaEditAuditType(audittype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditType")]
        [HttpPost]
        public HttpResponseMessage UpdateAuditType(MdlAtmMstAuditType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditType.DaUpdateAuditType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAuditType")]
        [HttpPost]
        public HttpResponseMessage InactiveAuditType(MdlAtmMstAuditType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditType.DaInactiveAuditType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAuditType")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditType(string audittype_gid)
        {
            MdlAtmMstAuditType values = new MdlAtmMstAuditType();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditType.DaDeleteAuditType(audittype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage AuditTypeInactiveLogview(string audittype_gid)
        {
            MdlAtmMstAuditType values = new MdlAtmMstAuditType();
            objDaAtmMstAuditType.DaAuditTypeInactiveLogview(audittype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditTypeActive")]
        [HttpGet]
        public HttpResponseMessage GetAuditTypeActive()
        {
            MdlAtmMstAuditType values = new MdlAtmMstAuditType();
            objDaAtmMstAuditType.DaGetAuditTypeActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}