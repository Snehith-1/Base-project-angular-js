using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.audit.Controllers
{
    [RoutePrefix("api/AtmMstChecklistMaster")]
    [Authorize]
    public class AtmMstChecklistMasterController : ApiController
    {
        DaAtmMstChecklistMaster objDaAtmMstChecklistMaster = new DaAtmMstChecklistMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //AtmMstAuditDepartmentCOntroller

        [ActionName("GetChecklistMaster")]
        [HttpGet]
        public HttpResponseMessage GetChecklistMaster()
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetChecklistMaster(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostChecklistMaster")]
        [HttpPost]
        public HttpResponseMessage PostChecklistMaster(MdlAtmMstChecklistMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaPostChecklistMaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistMasterAdd")]
        [HttpGet]
        public HttpResponseMessage GetChecklistMasterAdd(string checklistmaster_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetChecklistMasterAdd(checklistmaster_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostChecklistMasterAdd")]
        [HttpPost]
        public HttpResponseMessage PostChecklistMasterAdd(MdlAtmMstChecklistMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaPostChecklistMasterAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditChecklistMaster")]
        [HttpGet]
        public HttpResponseMessage EditChecklistMaster(string checklistmasteradd_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaEditChecklistMaster(checklistmasteradd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateChecklistMaster")]
        [HttpPost]
        public HttpResponseMessage UpdateChecklistMaster(MdlAtmMstChecklistMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaUpdateChecklistMaster(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteChecklistMaster")]
        [HttpGet]
        public HttpResponseMessage DeleteChecklistMaster(string checklistmaster_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaDeleteChecklistMaster(checklistmaster_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteChecklistMasterAdd")]
        [HttpGet]
        public HttpResponseMessage DeleteChecklistMasterAdd(string checklistmasteradd_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaDeleteChecklistMasterAdd(checklistmasteradd_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistMasterIntent")]
        [HttpGet]
        public HttpResponseMessage GetChecklistMasterIntent(string checklistmasteradd_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetChecklistMasterIntent(checklistmasteradd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistMasterDescription")]
        [HttpGet]
        public HttpResponseMessage GetChecklistMasterDescription(string checklistmasteradd_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetChecklistMasterDescription(checklistmasteradd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistMasterAuditor")]
        [HttpGet]
        public HttpResponseMessage GetChecklistMasterAuditor(string checklistmasteradd_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetChecklistMasterAuditor(checklistmasteradd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistMasterAuditorName")]
        [HttpGet]
        public HttpResponseMessage GetChecklistMasterAuditorName(string checklistmaster_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetChecklistMasterAuditorName(checklistmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("ImportExcelChecklist")]
        [HttpPost]
        public HttpResponseMessage ImportExcelChecklist()
        {
            HttpRequest httpRequest;
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaAtmMstChecklistMaster.DaImportExcelChecklist(httpRequest, getsessionvalues.employee_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("EditChecklistMasterAudit")]
        [HttpGet]
        public HttpResponseMessage EditChecklistMasterAudit(string checklistmaster_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaEditChecklistMasterAudit(checklistmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateChecklistMasterAudit")]
        [HttpPost]
        public HttpResponseMessage UpdateChecklistMasterAudit(MdlAtmMstChecklistMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaUpdateChecklistMasterAudit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointStatus")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointStatus()
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetCheckpointStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMultipleCheckpointgroup")]
        [HttpPost]
        public HttpResponseMessage GetMultipleCheckpointgroup(MdlAtmMstChecklistMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaGetMultipleCheckpointgroup(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditMultipleCheckpointgroup")]
        [HttpPost]
        public HttpResponseMessage GetEditMultipleCheckpointgroup(MdlAtmMstChecklistMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstChecklistMaster.DaGetEditMultipleCheckpointgroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckpointgroupMultiple")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointgroupMultiple(string checkpointgroup_gid)
        {
            MdlAtmMstChecklistMaster values = new MdlAtmMstChecklistMaster();
            objDaAtmMstChecklistMaster.DaGetCheckpointgroupMultiple(values,checkpointgroup_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}