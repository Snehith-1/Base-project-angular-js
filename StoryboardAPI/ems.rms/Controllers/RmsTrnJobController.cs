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
    [RoutePrefix("api/RmsTrnJob")]
    [Authorize]
    public class RmsTrnJobController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRmsTrnJob objDaJob = new DaRmsTrnJob();

        [ActionName("GetJobSummary")]
        [HttpGet]
        public HttpResponseMessage GetJobSummary()
        {
            JobList values = new JobList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetJobSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);           
        }
        [ActionName("GetAssignSPOCSummary")]
        [HttpGet]
        public HttpResponseMessage GetAssignSPOCSummary(string jobposition_gid)
        {
            AssignSPOCSummaryList values = new AssignSPOCSummaryList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetAssignSPOCSummary(getsessionvalues.employee_gid, jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSearchAssignSPOCSummary")]
        [HttpPost]
        public HttpResponseMessage GetSearchAssignSPOCSummary(SearchAssignSPOCSummaryList values)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetsearchAssignSPOCSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetShareTeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetShareTeamSummary(string jobposition_gid)
        {
            ShareTeamSummaryList values = new ShareTeamSummaryList();            
            objDaJob.DaGetShareTeamSummary(jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        [ActionName("GetinlineRecruiterSummary")]
        [HttpGet]
        public HttpResponseMessage GetinlineRecruiterSummary(string jobposition_gid)
        {
            inlinerecruiterList values = new inlinerecruiterList();
            objDaJob.DaGetinlineRecruiterSummary(jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetSPOCvalidation")]
        [HttpGet]
        public HttpResponseMessage GetSPOCvalidation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            JobList values = new JobList();
            objDaJob.DGetSPOCvalidation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("GetUnAssignSPOCSummary")]
        [HttpGet]
        public HttpResponseMessage GetUnAssignSPOCSummary(string jobposition_gid)
        {
            UnAssignSPOCSummaryList values = new UnAssignSPOCSummaryList();            
            objDaJob.DaGetUnAssignSPOCSummary( jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetTodayInterview")]
        [HttpGet]
        public HttpResponseMessage GetTodayInterview()
        {
            TodayInterviewList values = new TodayInterviewList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetTodayInterviewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetInterview")]
        [HttpGet]
        public HttpResponseMessage GetInterview()
        {
            InterviewList values = new InterviewList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetTodaySummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetBusinessUnit")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnit()
        {
            BusinessUnitList values = new BusinessUnitList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetBusinessUnit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetBusinessUnitteam")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnitteam(string businessunit_gid)
        {
            BusinessUnitteamList values = new BusinessUnitteamList();            
            objDaJob.DaGetBusinessUnitteam(businessunit_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetAssignSPOC")]
        [HttpGet]
        public HttpResponseMessage GetAssignSPOC(string businessunit_gid)
        {
            AssignSPOCList values = new AssignSPOCList();            
            objDaJob.DaGetAssignSPOC(businessunit_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetRecruiterList")]
        [HttpPost]
        public HttpResponseMessage GetRecruiterList(RecruiterList values)
        {
                 
            objDaJob.DaGetRecruiter( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetLocationList")]
        [HttpGet]
        public HttpResponseMessage GetLocationList()
        {
            JobLocationList values = new JobLocationList();
            objDaJob.DaGetJobLocation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetJobDetails")]
        [HttpGet]
        public HttpResponseMessage GetJobDetails(string jobposition_gid)
        {
            JobDetails values = new JobDetails();
            objDaJob.DaGetJobDetails(values, jobposition_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetJobRecruiterFreezeDetails")]
        [HttpGet]
        public HttpResponseMessage GetJobFreezeDetails(JobRecruiterFreezeDetails values)
        {
           
            objDaJob.DaGetJobRecruiterFreezeDetails(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetJobCodeFreezeDetails")]
        [HttpGet]
        public HttpResponseMessage GetJobCodeFreezeDetails(string jobposition_gid)
        {
            JobFreezeDetails values = new JobFreezeDetails();
            objDaJob.DaGetJobCodeFreezeDetails(values, jobposition_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("JobUpdate")]
        [HttpPost]
        public HttpResponseMessage JobUpdate(JobDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetJobUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("JobRecruiterFreeze")]
        [HttpPost]
        public HttpResponseMessage JobRecruiterFreeze(JobRecruiterFreeze values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetJobRecruiterFreeze(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("JobRecruiterUnFreeze")]
        [HttpPost]
        public HttpResponseMessage JobRecruiterUnFreeze(JobRecruiterFreeze values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetRecruiterUnFreeze(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("JobCodeFreeze")]
        [HttpPost]
        public HttpResponseMessage JobCodeFreeze(JobcodeFreeze values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetJobCodeFreeze(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("JobCodeUnFreeze")]
        [HttpPost]
        public HttpResponseMessage JobCodeUnFreeze(JobcodeFreeze values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaGetJobCodeUnFreeze(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessunitteamDetails")]
        [HttpGet]
        public HttpResponseMessage GetBusinessunitteamDetails(string jobposition_gid)
        {
            businessunitteamList values = new businessunitteamList();
            objDaJob.DaGetBusinessUnitNameDetails(jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        [ActionName("GetSharingDetails")]
        [HttpGet]
        public HttpResponseMessage GetSharingDetails(string jobposition_gid)
        {
            sharingdetailList values = new sharingdetailList();
            objDaJob.DaGetSharingDetails(jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        

        [ActionName("PostAssignSPOC")]
        [HttpPost]
        public HttpResponseMessage PostAssigSPOC(AddAssignSPOC values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaPostAssignSPOC(values, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUnAssignSPOC")]
        [HttpPost]
        public HttpResponseMessage PostUnAssigSPOC(AddUnAssignSPOC values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaPostUnAssignSPOC(values, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostShareTeam")]
        [HttpPost]
        public HttpResponseMessage PostShareTeam(AddShareTeam values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaPostShareTeam(values, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostJob")]
        [HttpPost]
        public HttpResponseMessage PostJob(AddJob values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJob.DaPostJob(values, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("Getsharingcolourcode")]
        //[HttpGet]
        //public HttpResponseMessage Getsharingcolourcode(string jobposition_gid)
        //{
        //    JobFreezeDetails values = new JobFreezeDetails();
        //    objDaJob.DaGetsharingcolourcode(values, jobposition_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
    }
}
