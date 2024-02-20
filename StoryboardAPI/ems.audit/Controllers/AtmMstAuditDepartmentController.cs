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
    [RoutePrefix("api/AtmMstAuditDepartment")]
    [Authorize]
    public class AtmMstAuditDepartmentController : ApiController
    {
        DaAtmMstAuditDepartment objDaAtmMstAuditDepartment = new DaAtmMstAuditDepartment();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //AtmMstAuditDepartmentCOntroller

        [ActionName("GetAuditDepartment")]
        [HttpGet]
        public HttpResponseMessage GetAuditDepartment()
        {
            MdlAuditDepartment values = new MdlAuditDepartment();
            objDaAtmMstAuditDepartment.DaGetAuditDepartment(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddAuditDepartment")]
        [HttpPost]
        public HttpResponseMessage AddAuditDepartment(MdlAuditDepartment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditDepartment.DaAddAuditDepartment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditDepartment")]
        [HttpGet]
        public HttpResponseMessage EditAuditDepartment(string auditdepartment_gid)
        {
            MdlAuditDepartment values = new MdlAuditDepartment();
            objDaAtmMstAuditDepartment.DaEditAuditDepartment(auditdepartment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditDepartment")]
        [HttpPost]
        public HttpResponseMessage UpdateSupplier(MdlAuditDepartment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditDepartment.DaUpdateAuditDepartment(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAuditDepartment")]
        [HttpPost]
        public HttpResponseMessage InactiveSupplier(MdlAuditDepartment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditDepartment.DaInactiveAuditDepartment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAuditDepartment")]
        [HttpGet]
        public HttpResponseMessage DeleteSupplier(string auditdepartment_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditDepartment.DaDeleteAuditDepartment(auditdepartment_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditDepartmentInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage AuditDepartmentInactiveLogview(string auditdepartment_gid)
        {
            MdlAuditDepartment values = new MdlAuditDepartment();
            objDaAtmMstAuditDepartment.DaAuditDepartmentInactiveLogview(auditdepartment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMappingDepartment")]
        [HttpGet]
        public HttpResponseMessage GetMappingDepartment()
        {
            MdlAuditDepartment values = new MdlAuditDepartment();
            objDaAtmMstAuditDepartment.DaGetMappingDepartment(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditDepartmentActive")]
        [HttpGet]
        public HttpResponseMessage GetAuditDepartmentActive()
        {
            MdlAuditDepartment values = new MdlAuditDepartment();
            objDaAtmMstAuditDepartment.DaGetAuditDepartmentActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
