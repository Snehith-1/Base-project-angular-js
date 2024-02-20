using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.vp.Models;
using ems.vp.DataAccess;

namespace StoryboardAPI.Controllers.ems.vendorPortal
{
    [RoutePrefix("api/issue")]
    [Authorize]
    public class issueController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIssue objDaIssue = new DaIssue();

        [ActionName("vendordtl")]
        [HttpGet]
        public HttpResponseMessage GetVendorDetail()
        {
            vendordtl objvendordtl = new vendordtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIssue.DaGetVendorDetail(getsessionvalues.user_gid, objvendordtl);
            return Request.CreateResponse(HttpStatusCode.OK, objvendordtl);
        }

        // Get Issue Tracker Table data

        [ActionName("trackertable")]
        [HttpGet]
        public HttpResponseMessage GetIssueData()
        {
            issuetrackertable objissuetrackertable = new issuetrackertable();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIssue.DaGetIssueData(objissuetrackertable, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objissuetrackertable);
        }

        // Get View Issue data

        [ActionName("viewissue")]
        [HttpGet]
        public HttpResponseMessage GetViewData(string issue_gid)
        {
            viewIssueDoc objviewIssueDoc = new viewIssueDoc();
            objDaIssue.DaGetViewData(issue_gid, objviewIssueDoc);
            return Request.CreateResponse(HttpStatusCode.OK, objviewIssueDoc);
        }

        [ActionName("statusLog")]
        [HttpGet]
        public HttpResponseMessage GetStatusLog(string issue_gid)
        {
            releaseData objGetReleaseData = new releaseData();
            objDaIssue.DaGetStatusLog(objGetReleaseData, issue_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objGetReleaseData);
        }

        [ActionName("chat")]
        [HttpGet]
        public HttpResponseMessage GetChatData(string issue_gid)
        {
            issuechat objissuechat = new issuechat();
            objDaIssue.DaGetChatData(issue_gid, objissuechat);
            return Request.CreateResponse(HttpStatusCode.OK, objissuechat);
        }

        // Vendor send log

        [ActionName("sendlog")]
        [HttpPost]
        public HttpResponseMessage SendIssueChat(chatlog values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIssue.DaSendIssueChat(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("state")]
        [HttpGet]
        public HttpResponseMessage GetStatus(string issuetracker_gid)
        {
            issuestate objissuestate = new issuestate();
            objDaIssue.DaGetStatus(issuetracker_gid, objissuestate);
            return Request.CreateResponse(HttpStatusCode.OK, objissuestate);
        }

        // Post Issue Status

        [ActionName("updatestatus")]
        [HttpPost]
        public HttpResponseMessage PostStatus(statusUpdate values)
        {
            statusUpdate objstatusUpdate = new statusUpdate();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIssue.DaPostStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}