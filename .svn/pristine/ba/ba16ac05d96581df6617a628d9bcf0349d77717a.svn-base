using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rms.Models;
using ems.rms.DataAccess;

namespace ems.rms.Controllers
{
    [RoutePrefix("api/RmsTrnMyrecuitment")]
    [Authorize]
    public class RmsTrnMyrecuitmentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRmsTrnMyRecruitment objDaMyRecruitment = new DaRmsTrnMyRecruitment();
        [ActionName("myrecruitmentsummary")]
        [HttpGet]
        public HttpResponseMessage Getmyrecruitmentsummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRmsTrnMyRecruitment objRmsTrnMyRecruitment = new MdlRmsTrnMyRecruitment();
            objDaMyRecruitment.DaGetmyrecruitmentsummary(getsessionvalues.employee_gid,objRmsTrnMyRecruitment);
            return Request.CreateResponse(HttpStatusCode.OK, objRmsTrnMyRecruitment);
        }
        [ActionName("GetJobinfo")]
        [HttpGet]
        public HttpResponseMessage GetJobinfo(string jobposition_gid)
        {
          
            MdlJobinfo objRmsTrnMyRecruitment = new MdlJobinfo();
            objDaMyRecruitment.DaGetJobinfo(objRmsTrnMyRecruitment, jobposition_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objRmsTrnMyRecruitment);
        }
        [ActionName("postcandidateinfo")]
        [HttpPost]
        public HttpResponseMessage postcandidateinfo(mdlcandidateinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.Dapostcandidateinfo(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcandidatesummary")]
        [HttpGet]
        public HttpResponseMessage Getcandidatesummary(string jobposition_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRmsTrnMyRecruitment values = new MdlRmsTrnMyRecruitment();
            objDaMyRecruitment.DaGetcandidatesummary(values, jobposition_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCandidateinfo")]
        [HttpGet]
        public HttpResponseMessage GetCandidateinfo(string jobposition_gid,string recruitmentadd_gid)
        {
          
        MdlRmsTrnMyRecruitment values = new MdlRmsTrnMyRecruitment();
            objDaMyRecruitment.DaGetCandidateinfo(values, jobposition_gid, recruitmentadd_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("postcandidatevalidation")]
        [HttpPost]
        public HttpResponseMessage postcandidatevalidation(Mdlinterviewprocess values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.Dapostcandidatevalidation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostInterviewDetails")]
        [HttpPost]
        public HttpResponseMessage PostInterviewDetails(Mdlinterviewprocess values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.DaPostInterviewDetails(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Poststep3interviewstatus")]
        [HttpPost]
        public HttpResponseMessage Poststep3interviewstatus(Mdlinterviewprocess values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.DaPostInterviewStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostJoiningStatus")]
        [HttpPost]
        public HttpResponseMessage PostJoiningStatus(Mdlinterviewprocess values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.DaPostJoiningStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostJobStaus")]
        [HttpPost]
        public HttpResponseMessage PostJobStaus(Mdlinterviewprocess values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.DaPostJobStaus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditCandidateinfo")]
        [HttpGet]
        public HttpResponseMessage EditCandidateinfo( string recruitmentadd_gid)
        {

            mdlcandidateinfo values = new mdlcandidateinfo();
            objDaMyRecruitment.DaEditCandidateinfo(values, recruitmentadd_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatecandidateinfo")]
        [HttpPost]
        public HttpResponseMessage updatecandidateinfo(mdlcandidateinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.Daupdatecandidateinfo(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCloningSummary")]
        [HttpGet]
        public HttpResponseMessage GetCloningSummary(string jobposition_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRmsTrnMyRecruitment values = new MdlRmsTrnMyRecruitment();
            objDaMyRecruitment.DaGetCloningSummary(values, jobposition_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetJobCodeInfo")]
        [HttpGet]
        public HttpResponseMessage GetJobInfo(string jobposition_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRmsTrnMyRecruitment values = new MdlRmsTrnMyRecruitment();
            objDaMyRecruitment.DaGetJobInfo(values, jobposition_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("postcloning")]
        [HttpPost]
        public HttpResponseMessage PostAssigSPOC(MdlCloning values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyRecruitment.Dapostcloning(values, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
