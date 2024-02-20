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
    [RoutePrefix("api/RmsTrnJobTracker")]
    [Authorize]
    public class RmsTrnJobTrackerController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRmsTrnJobTracker objDaJobTracker = new DaRmsTrnJobTracker();

        [ActionName("GetJobTrackerSummary")]
        [HttpGet]
        public HttpResponseMessage GetJobSummary()
        {
            JobTrackerList values = new JobTrackerList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJobTracker.DaGetJobTrackerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("GetJobPositionDetails")]
        [HttpGet]
        public HttpResponseMessage GetJobPositionDetails(string jobposition_gid)
        {
            jobPositionDetails values = new jobPositionDetails();
            objDaJobTracker.DaGetJobPositionDetails(values, jobposition_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetJobCodeViewDetails")]
        [HttpGet]
        public HttpResponseMessage GetJobCodeViewDetails(string jobposition_gid)
        {
            jobCodeViewDetails values = new jobCodeViewDetails();
            objDaJobTracker.DaGetJobCodeViewDetails(values, jobposition_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("JobCodePositionUpdate")]
        [HttpPost]
        public HttpResponseMessage JobCodePositionUpdate(jobPositionDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJobTracker.DaGetJobPositionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("JobCodeClose")]
        [HttpPost]
        public HttpResponseMessage JobCodeClose(jobPositionDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJobTracker.DaGetJobCodeClose(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRecruiterSummary")]
        [HttpGet]
        public HttpResponseMessage GetRecruiterSummary(string jobposition_gid)
        {
            recruiterList values = new recruiterList ();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaJobTracker.DaGetRecruiterSummary(getsessionvalues.employee_gid, jobposition_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        //[ActionName("GetCandidateDetailSummary")]
        //[HttpGet]
        //public HttpResponseMessage GetCandidateDetailSummary()
        //{
        //    JobTrackerList values = new JobTrackerList();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaJobTracker.DaGetJobTrackerSummary(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);

        //}
    }
}
