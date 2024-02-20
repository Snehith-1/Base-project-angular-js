using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.iasn.Models;
using ems.iasn.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;

namespace ems.iasn.Controllers
{
    [RoutePrefix("api/IasnTrnMyWorkItem")]
    [Authorize]
    public class IasnTrnMyWorkItemController : ApiController
    {
        DaIasnTrnMyWorkItem objDaAccess = new DaIasnTrnMyWorkItem();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("Allotted")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemAllottedSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemAllottedSummary(getsessionvalues.employee_gid,getsessionvalues .user_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Pending")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemPendingSummary(getsessionvalues.employee_gid,getsessionvalues .user_gid , values );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Pushback")]
        [HttpGet]
        public HttpResponseMessage MyWorkItemPushbackSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemPushbackSummary(getsessionvalues.employee_gid,getsessionvalues .user_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Forward")]
        [HttpGet]
        public HttpResponseMessage DaGetMyWorkItemForwardSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemForwardSummary(getsessionvalues.employee_gid,getsessionvalues .user_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Close")]
        [HttpGet]
        public HttpResponseMessage MyWorkItemCloseSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemCloseSummary(getsessionvalues.employee_gid,getsessionvalues .user_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Archival")]
        [HttpPost]
        public HttpResponseMessage DaGetMyWorkItemArchivalSummary(MdlArchivalCondition objCondition)
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemArchivalSummary(getsessionvalues.employee_gid,getsessionvalues .user_gid , values, objCondition);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MailSeen")]
        [HttpGet]
        public HttpResponseMessage MailSeen(string email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostEmailSeen(email_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [ActionName("MyWorkItemCounts")]
        [HttpGet]
        public HttpResponseMessage MyWorkItemCounts()
        {
            MyWorkItemListCount values = new MyWorkItemListCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemCounts(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
