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
    [RoutePrefix("api/AtmMstAuditPriority")]
    [Authorize]

    public class AtmMstAuditPriorityController : ApiController
    {
        DaAtmMstAuditPriority objDaAtmMstAuditPriority = new DaAtmMstAuditPriority();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetAuditPriority")]
        [HttpGet]
        public HttpResponseMessage GetAuditPriority()
        {
            MdlAtmMstAuditPriority values = new MdlAtmMstAuditPriority();
            objDaAtmMstAuditPriority.DaGetAuditPriority(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateAuditPriority")]
        [HttpPost]
        public HttpResponseMessage CreateAuditPriority(MdlAtmMstAuditPriority values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditPriority.DaCreateAuditPriority(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditAuditPriority")]
        [HttpGet]
        public HttpResponseMessage EditAuditPriority(string auditpriority_gid)
        {
            MdlAtmMstAuditPriority values = new MdlAtmMstAuditPriority();
            objDaAtmMstAuditPriority.DaEditAuditPriority(auditpriority_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditPriority")]
        [HttpPost]
        public HttpResponseMessage UpdateAuditPriority(MdlAtmMstAuditPriority values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditPriority.DaUpdateAuditPriority(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveAuditPriority")]
        [HttpPost]
        public HttpResponseMessage InactiveAuditPriority(MdlAtmMstAuditPriority values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditPriority.DaInactiveAuditPriority(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAuditPriority")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditPriority(string auditpriority_gid)
        {
            MdlAtmMstAuditPriority values = new MdlAtmMstAuditPriority();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditPriority.DaDeleteAuditPriority(auditpriority_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditPriorityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage AuditPriorityInactiveLogview(string auditpriority_gid)
        {
            MdlAtmMstAuditPriority values = new MdlAtmMstAuditPriority();
            objDaAtmMstAuditPriority.DaAuditPriorityInactiveLogview(auditpriority_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditPriorityActive")]
        [HttpGet]
        public HttpResponseMessage GetAuditPriorityActive()
        {
            MdlAtmMstAuditPriority values = new MdlAtmMstAuditPriority();
            objDaAtmMstAuditPriority.DaGetAuditPriorityActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
