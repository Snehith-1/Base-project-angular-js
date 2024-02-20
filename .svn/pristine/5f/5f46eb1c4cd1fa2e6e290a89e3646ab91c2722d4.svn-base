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
    [RoutePrefix("api/AtmMstAuditFrequency")]
    [Authorize]
    public class AtmMstAuditFrequencyController : ApiController
    {
        DaAtmMstAuditFrequency objDaAtmMstAuditFrequency = new DaAtmMstAuditFrequency();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetAuditFrequency")]
        [HttpGet]
        public HttpResponseMessage GetAuditFrequency()
        {
            MdlAtmMstAuditFrequency values = new MdlAtmMstAuditFrequency();
            objDaAtmMstAuditFrequency.DaGetAuditFrequency(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateAuditFrequency")]
        [HttpPost]
        public HttpResponseMessage CreateAuditFrequency(MdlAtmMstAuditFrequency values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditFrequency.DaCreateAuditFrequency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditFrequency")]
        [HttpGet]
        public HttpResponseMessage EditAuditFrequency(string auditfrequency_gid)
        {
            MdlAtmMstAuditFrequency values = new MdlAtmMstAuditFrequency();
            objDaAtmMstAuditFrequency.DaEditAuditFrequency(auditfrequency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditFrequency")]
        [HttpPost]
        public HttpResponseMessage UpdateAuditFrequency(MdlAtmMstAuditFrequency values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditFrequency.DaUpdateAuditFrequency(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAuditFrequency")]
        [HttpPost]
        public HttpResponseMessage InactiveAuditFrequency(MdlAtmMstAuditFrequency values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditFrequency.DaInactiveAuditFrequency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAuditFrequency")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditFrequency(string auditfrequency_gid)
        {
            MdlAtmMstAuditFrequency values = new MdlAtmMstAuditFrequency();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditFrequency.DaDeleteAuditFrequency(auditfrequency_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditFrequencyInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage AuditFrequencyInactiveLogview(string auditfrequency_gid)
        {
            MdlAtmMstAuditFrequency values = new MdlAtmMstAuditFrequency();
            objDaAtmMstAuditFrequency.DaAuditFrequencyInactiveLogview(auditfrequency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
