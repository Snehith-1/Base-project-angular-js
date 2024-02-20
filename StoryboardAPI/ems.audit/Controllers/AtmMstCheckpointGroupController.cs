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
    [RoutePrefix("api/AtmMstCheckpointGroup")]
    [Authorize]

    public class AtmMstCheckpointGroupController : ApiController
    {
        DaAtmMstCheckpointGroup objDaAtmMstCheckpointGroup = new DaAtmMstCheckpointGroup();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetCheckpointGroup")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointGroup()
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointGroup( values);
            return Request.CreateResponse(HttpStatusCode.OK,values);
        }

        [ActionName("GetCheckpointGroupName")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointGroupName(string checkpointgroup_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointGroupName(checkpointgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCheckpointGroup")]
        [HttpPost]
        public HttpResponseMessage CreateCheckpointGroup(MdlAtmMstCheckpointGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaCreateCheckpointGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCheckpointGroup")]
        [HttpGet]
        public HttpResponseMessage EditCheckpointGroup(string checkpointgroup_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaEditCheckpointGroup(checkpointgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCheckpointGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckpointGroup(MdlAtmMstCheckpointGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaUpdateCheckpointGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCheckpointGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveCheckpointGroup(MdlAtmMstCheckpointGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaInactiveCheckpointGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCheckpointGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteCheckpointGroup(string checkpointgroup_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaDeleteCheckpointGroup(checkpointgroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckpointGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CheckpointGroupInactiveLogview(string checkpointgroup_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaCheckpointGroupInactiveLogview(checkpointgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCheckpointAdd")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointAdd(string checkpointgroup_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointAdd(checkpointgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCheckpointAdd")]
        [HttpPost]
        public HttpResponseMessage PostCheckpointAdd(MdlAtmMstCheckpointGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaPostCheckpointAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditCheckpoint")]
        [HttpGet]
        public HttpResponseMessage EditChecklistMaster(string checkpointgroupadd_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaEditCheckpoint(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCheckpoint")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckpoint(MdlAtmMstCheckpointGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaUpdateCheckpoint(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("DeleteCheckpointAdd")]
        [HttpGet]
        public HttpResponseMessage DeleteCheckpointAdd(string checkpointgroupadd_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaDeleteCheckpointAdd(checkpointgroupadd_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointIntent")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointIntent(string checkpointgroupadd_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointIntent(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointDescription")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointDescription(string checkpointgroupadd_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointDescription(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointNotestoAuditor")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointNotestoAuditor(string checkpointgroupadd_gid)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointNotestoAuditor(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ImportExcelCheckpoint")]
        [HttpPost]
        public HttpResponseMessage ImportExcelCheckpoint()
        {
            HttpRequest httpRequest;
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaAtmMstCheckpointGroup.DaImportExcelCheckpoint(httpRequest, getsessionvalues.employee_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("CreateCheckListToCheckpoint")]
        [HttpPost]
        public HttpResponseMessage CreateCheckListToCheckpoint(checklistcheckpoint values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaCreateCheckListToCheckpoint(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateCheckListToCheckpoint")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckListToCheckpoint(checklistsamplecheckpoint values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaUpdateCheckListToCheckpoint(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckListToCheckpoint")]
        [HttpGet]
        public HttpResponseMessage GetCheckListToCheckpoint()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaGetCheckListToCheckpoint(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistToCheckpointCreate")]
        [HttpGet]
        public HttpResponseMessage GetChecklistToCheckpointCreate(string checkpointgroupadd_gid,string auditcreation_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaGetChecklistToCheckpointCreate(checkpointgroupadd_gid,auditcreation_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChecklistToCheckpointView")]
        [HttpGet]
        public HttpResponseMessage GetChecklistToCheckpointView(string checkpointgroupadd_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaGetCheckListToCheckpointView(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSampleToCheckpointCreate")]
        [HttpGet]
        public HttpResponseMessage GetSampleToCheckpointCreate(string checkpointgroupadd_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaGetSampleToCheckpointCreate(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteChecklist2Checkpoint")]
        [HttpGet]
        public HttpResponseMessage DeleteChecklist2Checkpoint(string checklist2checkpoint)
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaDeleteChecklist2Checkpoint(checklist2checkpoint, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointList")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaGetCheckpointList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCheckpointList")]
        [HttpGet]
        public HttpResponseMessage DeleteCheckpointList(string checklist2checkpoint)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaDeleteCheckpointList(checklist2checkpoint, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempDeleteCheckpointList")]
        [HttpGet]
        public HttpResponseMessage TempDeleteCheckpointList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaTempDeleteCheckpointList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointgroupActive")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointgroupActive()
        {
            MdlAtmMstCheckpointGroup values = new MdlAtmMstCheckpointGroup();
            objDaAtmMstCheckpointGroup.DaGetCheckpointgroupActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempCheckpointCheckList")]
        [HttpGet]
        public HttpResponseMessage GetTempCheckpointCheckList(string checkpointgroupadd_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaGetTempCheckpointCheckList(checkpointgroupadd_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCheckpointCheckList")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointCheckList(string checkpointgroupadd_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            objDaAtmMstCheckpointGroup.DaGetCheckpointCheckList(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMultipleAuditee")]
        [HttpPost]
        public HttpResponseMessage PostMultipleAuditee(multipleauditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaPostMultipleAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MultipleAuditeeEdit")]
        [HttpPost]
        public HttpResponseMessage MultipleAuditeeEdit(multipleauditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstCheckpointGroup.DaMultipleAuditeeEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeeList")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaGetAuditeeList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeeSummaryList")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeSummaryList(string checkpointgroupadd_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaGetAuditeeSummaryList(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeeCheckpointSummaryList")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeCheckpointSummaryList(string checkpointgroupadd_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaGetAuditeeCheckpointSummaryList(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditee")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditee(string multipleauditee_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaDeleteAuditee(multipleauditee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempAssignedAuditeeList")]
        [HttpGet]
        public HttpResponseMessage GetTempAssignedAuditeeList(string checkpointgroupadd_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaGetTempAssignedAuditeeList(checkpointgroupadd_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempDeleteAuditee")]
        [HttpGet]
        public HttpResponseMessage TempDeleteAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaTempDeleteAuditee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditeeList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditeeList(string multipleauditee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaDeleteAuditeeList(multipleauditee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeecheckpointList")]
        [HttpGet]
        public HttpResponseMessage GetAuditeecheckpointList(string checkpointgroup_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmMstCheckpointGroup.DaGetAuditeecheckpointList(checkpointgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}