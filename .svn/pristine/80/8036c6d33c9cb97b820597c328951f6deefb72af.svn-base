using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace ems.audit.Controllers
{
    [RoutePrefix("api/AtmTrnAuditorMaker")]
    [Authorize]
    public class AtmTrnAuditorMakerController : ApiController
    {

        DaAtmTrnAuditorMaker objDaDaAtmTrnAuditorMaker = new DaAtmTrnAuditorMaker();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();



        [ActionName("GetAuditorMaker")]
        [HttpGet]
        public HttpResponseMessage GetAuditorMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditorMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditorCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditorApproverSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditorMaker")]
        [HttpGet]
        public HttpResponseMessage EditAuditorMaker(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaEditAuditorMaker(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditorMakerView")]
        [HttpGet]
        public HttpResponseMessage AuditorMakerView(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaAuditorMakerView(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetAuditorMakerCheckpointObservation")]
        [HttpGet]
        public HttpResponseMessage GetAuditorMakerCheckpointObservation()
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMakerCheckpointObservation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditorMakerCheckpointObservation")]
        [HttpGet]
        public HttpResponseMessage EditAuditorMakerCheckpointObservation(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaEditAuditorMakerCheckpointObservation(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("AuditorMakerCheckpointObservationView")]
        [HttpGet]
        public HttpResponseMessage AuditorMakerCheckpointObservationView(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaAuditorMakerCheckpointObservationView(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAuditorMakerCheckpointObservation")]
        [HttpPost]
        public HttpResponseMessage PostAuditorMakerCheckpointObservation(makercheckpointobservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorMakerCheckpointObservation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorMakerObservationTotalAmount")]
        [HttpPost]
        public HttpResponseMessage PostAuditorMakerObservationTotalAmount(makercheckpointobservationadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorMakerObservationTotalAmount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditorMakerStatus")]
        [HttpPost]
        public HttpResponseMessage GetAuditorMakerStatus(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMakerStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompletedAuditorMaker")]
        [HttpGet]
        public HttpResponseMessage GetCompletedAuditorMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetCompletedAuditorMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        [ActionName("GetClosedAuditorMaker")]
        [HttpGet]
        public HttpResponseMessage GetClosedAuditorMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetClosedAuditorMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOpenAuditorMaker")]
        [HttpGet]
        public HttpResponseMessage GetOpenAuditorMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetOpenAuditorMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOnholdAuditorMaker")]
        [HttpGet]
        public HttpResponseMessage GetOnholdAuditorMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetOnholdAuditorMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorMakerCounts")]
        [HttpGet]
        public HttpResponseMessage GetAuditorMakerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMakerCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompletedAuditorChecker")]
        [HttpGet]
        public HttpResponseMessage GetCompletedAuditorChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetCompletedAuditorChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetClosedAuditorChecker")]
        [HttpGet]
        public HttpResponseMessage GetClosedAuditorChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetClosedAuditorChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOpenAuditorChecker")]
        [HttpGet]
        public HttpResponseMessage GetOpenAuditorChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetOpenAuditorChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOnholdAuditorChecker")]
        [HttpGet]
        public HttpResponseMessage GetOnholdAuditorChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetOnholdAuditorChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorCheckerCounts")]
        [HttpGet]
        public HttpResponseMessage GetAuditorCheckerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorCheckerCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetClosedAuditorApprover")]
        [HttpGet]
        public HttpResponseMessage GetClosedAuditorApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetClosedAuditorApprover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCompletedAuditorApprover")]
        [HttpGet]
        public HttpResponseMessage GetCompletedAuditorApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetCompletedAuditorApprover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOpenAuditorApprover")]
        [HttpGet]
        public HttpResponseMessage GetOpenAuditorApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetOpenAuditorApprover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOnholdAuditorApprover")]
        [HttpGet]
        public HttpResponseMessage GetOnholdAuditorApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetOnholdAuditorApprover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorApproverCounts")]
        [HttpGet]
        public HttpResponseMessage GetAuditorApproverCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorApproverCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditCreationIntent")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationIntent(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditCreationIntent(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditCreationDescription")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationDescription(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditCreationDescription(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditCreationAuditor")]
        [HttpGet]
        public HttpResponseMessage GetAuditCreationAuditor(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditCreationAuditor(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }       
        [ActionName("PostRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostRaiseQuery(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignedQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetAssignedQuerySummary(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaAssignedQuerySummary(getsessionvalues.employee_gid, values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RaisedsampleQuerySummary")]
        [HttpGet]
        public HttpResponseMessage RaisedsampleQuerySummary(string sampleimport_gid)
        {
            samplequerydatalist values = new samplequerydatalist(); 
            objDaDaAtmTrnAuditorMaker.DaRaisedsampleQuerySummary(values, sampleimport_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        
        [ActionName("PostReplyToQuery")]
        [HttpPost]
        public HttpResponseMessage PostReplyToQuery(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostReplyToQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssignedQuery")]
        [HttpGet]
        public HttpResponseMessage EditAssignedQuery(string raisequery_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaEditAssignedQuery(raisequery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRepliedQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetRepliedQuerySummary(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaRepliedQuerySummary(getsessionvalues.employee_gid, values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string raisequery_gid)
        {
            employelist values = new employelist();
            objDaDaAtmTrnAuditorMaker.DaGetEmployeeName(raisequery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAuditorCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditorCheckerApproval(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorCheckerRejected")]
        [HttpPost]
        public HttpResponseMessage PostAuditorCheckerRejected(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorCheckerRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CheckerApprovalView")]
        [HttpGet]
        public HttpResponseMessage CheckerApprovalView(string auditcreation_gid)
        {

            initialapprovaldtl values = new initialapprovaldtl();
            objDaDaAtmTrnAuditorMaker.DaCheckerApprovalView(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditorApproval(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorSampleApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditorSampleApproval(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorSampleApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorRejected")]
        [HttpPost]
        public HttpResponseMessage PostAuditorRejected(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditorApprovalView")]
        [HttpGet]
        public HttpResponseMessage AuditorApprovalView(string auditcreation_gid)
        {

            initialapprovaldtl values = new initialapprovaldtl();
            objDaDaAtmTrnAuditorMaker.DaAuditorApprovalView(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostAuditorGetApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditorGetApproval(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorGetApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditeeGetApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditeeGetApproval(initialapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditeeGetApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostMakerInitiateApproval")]
        [HttpPost]
        public HttpResponseMessage PostMakerInitiateApproval(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostMakerInitiateApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorMakerInitiateSample")]
        [HttpPost]
        public HttpResponseMessage PostAuditorMakerInitiateSample(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorMakerInitiateSample(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMakerInitiateStatus")]
        [HttpGet]
        public HttpResponseMessage GetMakerInitiateStatus(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetMakerInitiateStatus(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetStatusRemarks")]
        [HttpGet]
        public HttpResponseMessage GetStatusRemarks(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetStatusRemarks(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTaggedSampleMaker")]
        [HttpGet]
        public HttpResponseMessage GetTaggedSampleMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetTaggedSampleMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTaggedSampleChecker")]
        [HttpGet]
        public HttpResponseMessage GetTaggedSampleChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetTaggedSampleChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTaggedSampleApprover")]
        [HttpGet]
        public HttpResponseMessage GetTaggedSampleApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetTaggedSampleApprover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSampleResponseQuery")]
        [HttpGet]
        public HttpResponseMessage GetSampleResponseQuery(string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetSampleResponseQuery(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditObservatioRemarks")]
        [HttpPost]
        public HttpResponseMessage AuditObservatioRemarks(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.Daauditobservatioremarks(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AuditObservatioRemarksview")]
        [HttpGet]
        public HttpResponseMessage AuditObservatioRemarksview(string auditcreation2checklist_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.Daauditobservatioremarksview(auditcreation2checklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorMakerViewOverallscore")]
        [HttpGet]
        public HttpResponseMessage GetAuditorMakerViewOverallscore(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMakerViewOverallscore(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditorMakerobservationscore")]
        [HttpGet]
        public HttpResponseMessage GetAuditorMakerobservationscore(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorMakerobservationscore(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorSampleObservationTotalAmount")]
        [HttpPost]
        public HttpResponseMessage PostAuditorSampleObservationTotalAmount(makercheckpointobservationadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorSampleObservationTotalAmount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditorUpdateSampleObservationTotalAmount")]
        [HttpPost]
        public HttpResponseMessage PostAuditorUpdateSampleObservationTotalAmount(makercheckpointobservationadd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditorUpdateSampleObservationTotalAmount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMakerSampleInitiateApproval")]
        [HttpPost]
        public HttpResponseMessage PostMakerSampleInitiateApproval(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostMakerSampleInitiateApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditorSampleView")]
        [HttpGet]
        public HttpResponseMessage AuditorSampleView(string auditcreation_gid,string sampleimport_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaAuditorSampleView(auditcreation_gid,sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditorSampleViewOverallscore")]
        [HttpGet]
        public HttpResponseMessage GetAuditorSampleViewOverallscore(string auditcreation_gid, string sampleimport_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorSampleViewOverallscore(auditcreation_gid, sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCheckpointAgainstObservation")]
        [HttpPost]
        public HttpResponseMessage PostCheckpointAgainstObservation(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostCheckpointAgainstObservation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSampleCheckpointAgainstObservation")]
        [HttpPost]
        public HttpResponseMessage PostSampleCheckpointAgainstObservation(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostSampleCheckpointAgainstObservation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSampleCheckpointAssign")]
        [HttpPost]
        public HttpResponseMessage PostSampleCheckpointAssign(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostSampleCheckpointAssign(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSampleCheckpointAssignUpdate")]
        [HttpPost]
        public HttpResponseMessage PostSampleCheckpointAssignUpdate(auditchecklistassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostSampleCheckpointAssignUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCheckpointObservation")]
        [HttpPost]
        public HttpResponseMessage PostCheckpointObservation(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostCheckpointObservation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCheckpointObservationUpdate")]
        [HttpPost]
        public HttpResponseMessage PostCheckpointObservationUpdate(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostCheckpointObservationUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSampleToCheckpoint")]
        [HttpGet]
        public HttpResponseMessage GetSampleToCheckpoint(string checkpointgroupadd_gid,string sampleimport_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            objDaDaAtmTrnAuditorMaker.DaGetSampleToCheckpoint(checkpointgroupadd_gid, sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditorSampleFlag")]
        [HttpGet]
        public HttpResponseMessage GetAuditorSampleFlag(string checkpointgroupadd_gid, string sampleimport_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorSampleFlag(checkpointgroupadd_gid, sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditorCheckpointFlag")]
        [HttpGet]
        public HttpResponseMessage GetAuditorCheckpointFlag(string checkpointgroupadd_gid, string auditcreation_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorCheckpointFlag(checkpointgroupadd_gid, auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditorSampleName")]
        [HttpGet]
        public HttpResponseMessage GetAuditorSampleName(string auditcreation_gid, string sampleimport_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaGetAuditorSampleName(auditcreation_gid, sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MakerObservationSampleView")]
        [HttpGet]
        public HttpResponseMessage MakerObservationSampleView(string sampleimport_gid)
        {
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnAuditorMaker.DaMakerObservationSampleView(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMultipleAuditeecheckerApproval")]
        [HttpGet]
        public HttpResponseMessage GetMultipleAuditeecheckerApproval(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            initialapprovaldtl values = new initialapprovaldtl();
            objDaDaAtmTrnAuditorMaker.DaGetMultipleAuditeecheckerApproval(values,auditcreation_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostOverallCheckpointAgainstObservation")]
        [HttpPost]
        public HttpResponseMessage PostOverallCheckpointAgainstObservation(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostOverallCheckpointAgainstObservation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("OverallSelectedSummary")]
        [HttpGet]
        public HttpResponseMessage OverallSelectedSummary(string checkpointgroupadd_gid)
        {
            checklistcheckpoint values = new checklistcheckpoint();
            objDaDaAtmTrnAuditorMaker.DaOverallSelectedSummary(checkpointgroupadd_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostOverallCheckpointAgainstSample")]
        [HttpPost]
        public HttpResponseMessage PostOverallCheckpointAgainstSample(MdlAtmTrnAuditorMaker values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostOverallCheckpointAgainstSample(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RaiseQueryHistorySummary")]
        [HttpGet]
        public HttpResponseMessage RaiseQueryHistorySummary(string auditcreation_gid)
        {
            initialapprovaldtl values = new initialapprovaldtl();
            objDaDaAtmTrnAuditorMaker.DaRaiseQueryHistorySummary(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AssignedTagUser")]
        [HttpGet]
        public HttpResponseMessage AssignedTagUser(string sampleimport_gid)
        {
            initialapprovaldtl values = new initialapprovaldtl();
            objDaDaAtmTrnAuditorMaker.DaAssignedTagUser(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
     
        }
        [ActionName("PostAuditRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostAuditRaiseQuery(auditraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditRaisedQuerySummary")]
        [HttpGet]
        public HttpResponseMessage AuditRaisedQuerySummary(string auditcreation_gid)
        {
            auditraisequery values = new auditraisequery();
            objDaDaAtmTrnAuditorMaker.DaAuditRaisedQuerySummary(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditQuerydetail")]
        [HttpPost]
        public HttpResponseMessage PostAuditQuerydetail(auditraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditQuerydetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditQuerydetaillist")]
        [HttpGet]
        public HttpResponseMessage GetAuditQuerydetaillist(string auditraisequery_gid)
        {
            auditraisequery values = new auditraisequery();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaGetAuditQuerydetaillist(getsessionvalues.employee_gid, auditraisequery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditCloseQuery")]
        [HttpPost]
        public HttpResponseMessage PostAuditCloseQuery(auditraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnAuditorMaker.DaPostAuditCloseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditResponseDocUpload")]
        [HttpPost]
        public HttpResponseMessage AuditResponseDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            responsedoc_upload documentname = new responsedoc_upload();
            objDaDaAtmTrnAuditorMaker.DaAuditResponseDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
    }
}