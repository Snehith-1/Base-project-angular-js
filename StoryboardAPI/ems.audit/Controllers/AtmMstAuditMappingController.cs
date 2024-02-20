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
    [RoutePrefix("api/AtmMstAuditMapping")]
    [Authorize]
    public class AtmMstAuditMappingController : ApiController
    {
        DaAtmMstAuditMapping objDaAtmMstAuditMapping = new DaAtmMstAuditMapping();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetAuditMapping")]
        [HttpGet]
        public HttpResponseMessage GetAuditMapping()
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaGetAuditMapping(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateAuditMapping")]
        [HttpPost]
        public HttpResponseMessage CreateAuditMapping(MdlAtmMstAuditMapping values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditMapping.DaCreateAuditMapping(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditMapping")]
        [HttpGet]
        public HttpResponseMessage EditAuditMapping(string auditmapping_gid)
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaEditAuditMapping(auditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditMapping")]
        [HttpPost]
        public HttpResponseMessage UpdateAuditMapping(MdlAtmMstAuditMapping values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditMapping.DaUpdateAuditMapping(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAuditMapping")]
        [HttpPost]
        public HttpResponseMessage InactiveAuditMapping(MdlAtmMstAuditMapping values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditMapping.DaInactiveAuditMapping(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAuditMapping")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditMapping(string auditmapping_gid)
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstAuditMapping.DaDeleteAuditMapping(auditmapping_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditMappingInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage AuditMappingInactiveLogview(string auditmapping_gid)
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaAuditMappingInactiveLogview(auditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditMappingMaker")]
        [HttpGet]
        public HttpResponseMessage GetAuditMappingMaker()
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaGetAuditMappingMaker(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditMappingChecker")]
        [HttpGet]
        public HttpResponseMessage GetAuditMappingChecker()
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaGetAuditMappingChecker(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditMappingApprover")]
        [HttpGet]
        public HttpResponseMessage GetAuditMappingApprover()
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaGetAuditMappingApprover(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string auditmapping_gid)
        {
            Auditmappingemployee values = new Auditmappingemployee();
            objDaAtmMstAuditMapping.DaGetEmployeeName(auditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditChecker")]
        [HttpGet]
        public HttpResponseMessage GetAuditChecker(string checklistmaster_gid)
        {
            MdlAtmMstAuditMapping values = new MdlAtmMstAuditMapping();
            objDaAtmMstAuditMapping.DaGetAuditChecker(checklistmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}