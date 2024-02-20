using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.rsk.Models;
using ems.rsk.DataAccess;
using ems.utilities.Functions;


namespace StoryboardAPI.Controllers.ems.rsk
{
    [RoutePrefix("api/DASTracker")]
    [Authorize]

    public class DASTrackerController : ApiController
    {
        DaDASTracker objDASTracker = new DaDASTracker();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("addacknowledgedbuyers")]
        [HttpPost]
        public HttpResponseMessage poststartpenalityalert(acknowledgedbuyers value)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            acknowledgedbuyers values = new acknowledgedbuyers();
            objDASTracker.DaPostAcknowledgedBuyers(getsessionvalues.employee_gid, value);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getacknowledgedbuyers")]
        [HttpGet]
        public HttpResponseMessage GetAcknowledgedBuyers(string customer_gid)
        {
            listacknowledgedbuyers values = new listacknowledgedbuyers();
            objDASTracker.DaGetAcknowledgedBuyers(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deleteacknowledgedbuyers")]
        [HttpGet]
        public HttpResponseMessage DeleteAcknowledgedBuyers(string acknowledgedbuyers_gid)
        {
            acknowledgedbuyers values = new acknowledgedbuyers();
            objDASTracker.DaDeleteAcknowledgedBuyers(acknowledgedbuyers_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updateacknowledgedbuyers")]
        [HttpPost]
        public HttpResponseMessage PostUpdateAcknowledgedBuyers(acknowledgedbuyers value)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            acknowledgedbuyers values = new acknowledgedbuyers();
            objDASTracker.DaPostUpdateAcknowledgedBuyers(getsessionvalues.employee_gid, value);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("addremitterbuyers")]
        [HttpPost]
        public HttpResponseMessage PostRemitterBuyers(remitterbuyers value)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            remitterbuyers values = new remitterbuyers();
            objDASTracker.DaPostRemitterBuyers(getsessionvalues.employee_gid, value);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getremitterbuyers")]
        [HttpGet]
        public HttpResponseMessage GetRemitterBuyers(string customer_gid)
        {
            listremitterbuyers values = new listremitterbuyers();
            objDASTracker.DaGetRemitterBuyers(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("deleteremitterbuyers")]
        [HttpGet]
        public HttpResponseMessage DeleteRemitterBuyers(string remitterbuyers_gid)
        {
            remitterbuyers values = new remitterbuyers();
            objDASTracker.DaDeleteRemitterBuyers(remitterbuyers_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
