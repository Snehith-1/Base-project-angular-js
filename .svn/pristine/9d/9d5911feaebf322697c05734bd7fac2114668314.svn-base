using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.vp.Models;
using ems.utilities.Models;
using ems.vp.DataAccess;

namespace StoryboardAPI.Controllers.ems.vendorPortal
{
    [RoutePrefix("api/releaseManagement")]
    [Authorize]

    public class releaseManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaReleaseManagement objDaReleaseManagement = new DaReleaseManagement();

        [ActionName("releaseMgmt")]
        [HttpGet]
        public HttpResponseMessage GetReleaseDetails()
        {
            ReleaseManagement objReleaseData = new ReleaseManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaReleaseManagement.DaGetReleaseDetails(objReleaseData, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objReleaseData);
        }

        [ActionName("statusCompleted")]
        [HttpPost]
        public HttpResponseMessage PostStatusCompleted(statusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaReleaseManagement.DaPostStatusCompleted(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("issue2Release")]
        [HttpGet]
        public HttpResponseMessage GetViewIssue2Release(string release_gid)
        {
            releaseData objGetReleaseData = new releaseData();
            var status = objDaReleaseManagement.DaGetViewIssue2Release(release_gid, objGetReleaseData);
            return Request.CreateResponse(HttpStatusCode.OK, objGetReleaseData);
        }


    }
}