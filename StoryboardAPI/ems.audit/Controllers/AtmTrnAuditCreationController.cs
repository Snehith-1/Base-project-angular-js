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
    [RoutePrefix("api/AtmTrnAuditCreation")]
    [Authorize]
    public class AtmTrnAuditCreationController : ApiController
    {
        DaAtmTrnAuditCreation objDaAtmTrnAuditCreation = new DaAtmTrnAuditCreation();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
     
        [ActionName("GetAuditCreation")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreation()
        {
           
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAuditCreation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAudit360View")]
        [HttpGet]
        public HttpResponseMessage GetAudit360View(string auditcreation_gid)
        {

            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAudit360View( values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSample")]
        [HttpGet]
        public HttpResponseMessage GetSample(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnAuditCreation.DaGetSample(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignedInformation")]
        [HttpGet]
        public HttpResponseMessage GetAssignedInformation(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            assignedinformation values = new assignedinformation();
            objDaAtmTrnAuditCreation.DaGetAssignedInformation(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAuditCreation")]
        [HttpPost]
        public HttpResponseMessage PostAuditCreation(MdlAtmTrnAuditCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaPostAuditCreation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditCreation")]
        [HttpGet]
        public HttpResponseMessage EditAuditCreation(string auditcreation_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaEditAuditCreation(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditCreation")]
        [HttpPost]
        public HttpResponseMessage UpdateChecklistMaster(MdlAtmTrnAuditCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaUpdateAuditCreation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TrnCheckpointCreation")]
        [HttpGet]
        public HttpResponseMessage TrnCheckpointCreation(string auditcreation_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaTrnCheckpointCreation(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckpointCreation")]
        [HttpGet]
        public HttpResponseMessage CheckpointCreation(string checklistmaster_gid,string auditcreation_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaCheckpointCreation(checklistmaster_gid, auditcreation_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CheckpointAuditcreation")]
        [HttpGet]
        public HttpResponseMessage CheckpointAuditcreation(string checklistmaster_gid, string auditcreation_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaCheckpointAuditcreation(checklistmaster_gid, auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChecklistAssign")]
        [HttpPost]
        public HttpResponseMessage PostChecklistAssign(auditchecklistassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaPostChecklistAssign( values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChecklistAssignView")]
        [HttpGet]
        public HttpResponseMessage ChecklistAssignView(string auditcreation_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaChecklistAssignView(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteChecklistAssign")]
        [HttpPost]
        public HttpResponseMessage DeleteChecklistAssign(auditchecklistassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaDeleteChecklistAssign(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetAuditCreationIntent")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationIntent(string auditcreation2checklist_gid)
        {
            auditchecklistassign values = new auditchecklistassign();
            objDaAtmTrnAuditCreation.DaGetAuditCreationIntent(auditcreation2checklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditCreationDescription")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationDescription(string auditcreation2checklist_gid)
        {
            auditchecklistassign values = new auditchecklistassign();
            objDaAtmTrnAuditCreation.DaGetAuditCreationDescription(auditcreation2checklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditCreationAuditor")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationAuditor(string auditcreation2checklist_gid)
        {
            auditchecklistassign values = new auditchecklistassign();
            objDaAtmTrnAuditCreation.DaGetAuditCreationAuditor(auditcreation2checklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyClosedAuditTask")]
        [HttpGet]
        public HttpResponseMessage GetMyClosedAuditTask()
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetMyClosedAuditTask(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMyOpenAuditTask")]
        [HttpGet]
        public HttpResponseMessage GetMyOpenAuditTask()
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetMyOpenAuditTask(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyApprovedAuditTask")]
        [HttpGet]
        public HttpResponseMessage GetMyApprovedAuditTask()
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetMyApprovedAuditTask(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyOnholdAuditTask")]
        [HttpGet]
        public HttpResponseMessage GetMyOnholdAuditTask()
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetMyOnholdAuditTask(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditCreationCounts")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationCounts()
        {
            getAuditCount values = new getAuditCount();
            objDaAtmTrnAuditCreation.DaGetAuditCreationCounts(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeleteSampleImport")]
        [HttpGet]
        public HttpResponseMessage GetDeleteSampleImport(string sampleimport_gid)
        {
            result values = new result();
            objDaAtmTrnAuditCreation.DaGetDeleteSampleImport(values, sampleimport_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditName")]
        [HttpGet]
        public HttpResponseMessage GetAuditName(string auditdepartment_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAuditName(auditdepartment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditNameDetail")]
        [HttpGet]
        public HttpResponseMessage GetAuditNameDetail(string checklistmaster_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAuditnameDetail(checklistmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditCreationApprover")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAuditCreationApprover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditCreationRejected")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationRejected()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAuditCreationRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditDepartmentList")]
        [HttpGet]
        public HttpResponseMessage GetAuditDepartmentList(string auditdepartment_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetAuditDepartmentList(auditdepartment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAuditApprovalChecklistAssign")]
        [HttpPost]
        public HttpResponseMessage PostAuditApprovalChecklistAssign(auditcreationapprovallist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaPostAuditApprovalChecklistAssign(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyApprovedAudit")]
        [HttpGet]
        public HttpResponseMessage GetMyApprovedAudit()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaGetMyApprovedAudit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovedAuditCounts")]
        [HttpGet]
        public HttpResponseMessage GetApprovedAuditCounts()
        {            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            getAuditCount values = new getAuditCount();
            objDaAtmTrnAuditCreation.DaGetApprovedAuditCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("observationfill")]
        [HttpGet]
        public HttpResponseMessage observationfill(string auditcreation_gid)
        {
            result values = new result();
            objDaAtmTrnAuditCreation.Daobservationfill(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCompletedAudit")]
        [HttpGet]
        public HttpResponseMessage GetCompletedAudit()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnCompletedAudit values = new MdlAtmTrnCompletedAudit();
            objDaAtmTrnAuditCreation.DaGetCompletedAudit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSampleAssign")]
        [HttpPost]
        public HttpResponseMessage PostSampleAssign(auditchecklistassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaPostSampleAssign(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSampleAssignUpdate")]
        [HttpPost]
        public HttpResponseMessage PostSampleAssignUpdate(auditchecklistassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaPostSampleAssignUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SampleObservationScore")]
        [HttpGet]
        public HttpResponseMessage SampleObservationScore(string auditcreation_gid, string sampleimport_gid,string observationscoresample_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaSampleObservationScore(auditcreation_gid, sampleimport_gid, observationscoresample_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMultipleAuditee")]
        [HttpPost]
        public HttpResponseMessage PostMultipleAuditee(multipleauditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaPostMultipleAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MultipleAuditeeEdit")]
        [HttpPost]
        public HttpResponseMessage MultipleAuditeeEdit(multipleauditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaMultipleAuditeeEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeeList")]
        [HttpGet]
        public HttpResponseMessage GetCheckListToCheckpoint()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaGetAuditeeList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeeSummaryList")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeSummaryList(string auditcreation_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaGetAuditeeSummaryList(auditcreation_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditee")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditee(string multipleauditee_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaDeleteAuditee(multipleauditee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempAssignedAuditeeList")]
        [HttpGet]
        public HttpResponseMessage GetTempAssignedAuditeeList(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaGetTempAssignedAuditeeList(auditcreation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TempDeleteAuditee")]
        [HttpGet]
        public HttpResponseMessage TempDeleteAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaTempDeleteAuditee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditeeList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditeeList(string multipleauditee_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaDeleteAuditeeList(multipleauditee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InitiateAuditRejected")]
        [HttpPost]
        public HttpResponseMessage InitiateAuditRejected(multipleauditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaInitiateAuditRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAuditee")]
        [HttpGet]
        public HttpResponseMessage EditAuditee(string multipleauditee_gid)
        {
            MdlAtmTrnAuditCreation values = new MdlAtmTrnAuditCreation();
            objDaAtmTrnAuditCreation.DaEditAuditee(multipleauditee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAuditee")]
        [HttpPost]
        public HttpResponseMessage UpdateAuditee(MdlAtmTrnAuditCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnAuditCreation.DaUpdateAuditee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAuditeeList")]
        [HttpGet]
        public HttpResponseMessage GetEditAuditeeList(string multipleauditee_gid)
        {
            multipleauditee values = new multipleauditee();
            objDaAtmTrnAuditCreation.DaGetEditAuditeeList(multipleauditee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }

}