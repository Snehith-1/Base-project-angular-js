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
    [RoutePrefix("api/AtmTrnMyAuditTaskAuditee")]
    [Authorize]
    public class AtmTrnMyAuditTaskAuditeeController : ApiController
    {
        DaAtmTrnMyAuditTaskAuidtee objDaDaAtmTrnMyAuditTaskAuditee = new DaAtmTrnMyAuditTaskAuidtee();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetMyAuditTaskAuditee")]
        [HttpGet]
        public HttpResponseMessage GetMyAuditTaskAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetMyAuditTaskAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMyAuditTaskAuditeeMaker")]
        [HttpGet]
        public HttpResponseMessage GetMyAuditTaskAuditeeMaker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetMyAuditTaskAuditeeMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAuditTaskAuditeeChecker")]
        [HttpGet]
        public HttpResponseMessage GetMyAuditTaskAuditeeChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetMyAuditTaskAuditeeChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("EditMyAuditTaskAuditee")]
        [HttpGet]
        public HttpResponseMessage EditMyAuditTaskAuditee(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaEditMyAuditTaskAuditee(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyAuditTaskViewAuditee")]
        [HttpGet]
        public HttpResponseMessage MyAuditTaskViewAuditee(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaMyAuditTaskViewAuditee(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAuditTaskCounts")]
        [HttpGet]
        public HttpResponseMessage GetMyAuditTaskCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetMyAuditTaskCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompletedAuditee")]
        [HttpGet]
        public HttpResponseMessage GetCompletedAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetCompletedAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetClosedAuditee")]
        [HttpGet]
        public HttpResponseMessage GetClosedAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetClosedAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetOpenAuditee")]
        [HttpGet]
        public HttpResponseMessage GetOpenAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetOpenAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetHoldAuditee")]
        [HttpGet]
        public HttpResponseMessage GetOnholdAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetHoldAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTaggedSampleTask")]
        [HttpGet]
        public HttpResponseMessage GetTaggedSampleTask()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetTaggedSampleTask(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAuditTaskCheckerCounts")]
        [HttpGet]
        public HttpResponseMessage GetMyAuditTaskCheckerCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetMyAuditTaskCheckerCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckerCompletedAuditee")]
        [HttpGet]
        public HttpResponseMessage GetCheckerCompletedAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetCheckerCompletedAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckerClosedAuditee")]
        [HttpGet]
        public HttpResponseMessage GetCheckerClosedAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetCheckerClosedAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCheckerOpenAuditee")]
        [HttpGet]
        public HttpResponseMessage GetCheckerOpenAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetCheckerOpenAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCheckerHoldAuditee")]
        [HttpGet]
        public HttpResponseMessage GetCheckerHoldAuditee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetCheckerHoldAuditee(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTaggedSampleChecker")]
        [HttpGet]
        public HttpResponseMessage GetTaggedSampleChecker()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetTaggedSampleChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditeeIntent")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeIntent(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetAuditeeIntent(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditeeDescription")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeDescription(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetAuditeeDescription(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditeeNotes")]
        [HttpGet]
        public HttpResponseMessage GetAuditeeNotes(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetAuditeeNotes(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckpointObservationAuditee")]
        [HttpGet]
        public HttpResponseMessage GetCheckpointObservation()
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetCheckpointObservationAuditee(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCheckpointObservationAuditee")]
        [HttpGet]
        public HttpResponseMessage EditCheckpointObservationAuditee(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaEditCheckpointObservationAuditee(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //[ActionName("CheckpointObservationViewAuditee")]
        //[HttpGet]
        //public HttpResponseMessage CheckpointObservationViewAuditee(string auditcreation_gid)
        //{
        //    MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
        //    objDaDaAtmTrnMyAuditTaskAuditee.DaCheckpointObservationViewAuditee(auditcreation_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("PostQuerydetail")]
        [HttpPost]
        public HttpResponseMessage PostQuerydetail(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaPostQuerydetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetQuerydetaillist")]
        [HttpGet]
        public HttpResponseMessage GetQuerydetaillist(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetQuerydetaillist(getsessionvalues.employee_gid, auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("PostRaiseQuerysample")]
        //[HttpPost]
        //public HttpResponseMessage PostRaiseQuerysample(MdlAtmTrnMyAuditTaskAuditee values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaDaAtmTrnMyAuditTaskAuditee.DaPostRaiseQuerysample(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        [ActionName("PostSampleQuerydetail")]
        [HttpPost]
        public HttpResponseMessage PostSampleQuerydetail(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaPostSampleQuerydetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSampleQuerydetaillist")]
        [HttpGet]
        public HttpResponseMessage GetSampleQuerydetaillist(string sampleraisequery_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetSampleQuerydetaillist(getsessionvalues.employee_gid, sampleraisequery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTagUserAudit")]
        [HttpPost]
        public HttpResponseMessage PostTagUserAudit(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaPostTagUserAudit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTagUserAuditview")]
        [HttpGet]
        public HttpResponseMessage GetTagUserAuditview(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetTagUserAuditview( values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("closequery")]
        [HttpPost]
        public HttpResponseMessage closequery(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.Daclosequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("closequerysummary")]
        [HttpGet]
        public HttpResponseMessage closequerysummary(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.Daclosequerysummary( values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetEmployeeName(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("closequerysample")]
        [HttpPost]
        public HttpResponseMessage closequerysample(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.Daclosequerysample(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("closesamplequerysummary")]
        [HttpGet]
        public HttpResponseMessage closesamplequerysummary(string sampleimport_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.Daclosesamplequerysummary(values, sampleimport_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetclosequeryAudit")]
        [HttpGet]
        public HttpResponseMessage GetclosequeryAudit(string auditcreation_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetclosequeryAudit(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSampleclosequery")]
        [HttpGet]
        public HttpResponseMessage GetSampleclosequery(string sampleimport_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaDaAtmTrnMyAuditTaskAuditee.DaGetSampleclosequery(values, sampleimport_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAuditeeCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditeeCheckerApproval(auditeecheckerapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaPostAuditeeCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditeeCheckerRejected")]
        [HttpPost]
        public HttpResponseMessage PostAuditeeCheckerRejected(auditeecheckerapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDaAtmTrnMyAuditTaskAuditee.DaPostAuditeeCheckerRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostcloseSamplequery")]
        [HttpPost]
        public HttpResponseMessage PostcloseSamplequery(sampleraiseclosequery_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaDaAtmTrnMyAuditTaskAuditee.DaPostcloseSamplequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ResponseDocUpload")]
        [HttpPost]
        public HttpResponseMessage ResponseDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            responsedoc_upload documentname = new responsedoc_upload();
            objDaDaAtmTrnMyAuditTaskAuditee.DaResponseDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        
        [ActionName("AuditeeMakerView")]
        [HttpGet]
        public HttpResponseMessage AuditeeMakerView(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnMyAuditTaskAuditee.DaAuditeeMakerView(auditcreation_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditeeSampleMakerView")]
        [HttpGet]
        public HttpResponseMessage AuditeeSampleMakerView(string sampleimport_gid,string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnMyAuditTaskAuditee.DaAuditeeSampleMakerView(sampleimport_gid,auditcreation_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditeeCheckerView")]
        [HttpGet]
        public HttpResponseMessage AuditeeCheckerView(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnMyAuditTaskAuditee.DaAuditeeCheckerView(auditcreation_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditeeSampleCheckerView")]
        [HttpGet]
        public HttpResponseMessage AuditeeSampleCheckerView(string sampleimport_gid, string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnAuditorMaker values = new MdlAtmTrnAuditorMaker();
            objDaDaAtmTrnMyAuditTaskAuditee.DaAuditeeSampleCheckerView(sampleimport_gid, auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
