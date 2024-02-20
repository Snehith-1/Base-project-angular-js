using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.its.Models;
using ems.utilities.Functions;
using System.Web;
using ems.utilities.Models;
using ems.its.DataAccess;

namespace StoryboardAPI.Controllers.its
{
    [RoutePrefix("api/viewServiceTicket")]
    [Authorize]
    public class viewServiceTicketController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //fnViewServiceTicket objfnViewServiceTicket = new fnViewServiceTicket();
        DaViewServiceTicket objDaViewServiceTicket = new DaViewServiceTicket();

        //  View Service Ticket .......//

        [ActionName("viewserviceticket")]
        [HttpGet]
        public HttpResponseMessage GetViewServiceTicket()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            viewserviceticket objviewserviceticket = new viewserviceticket();
            objDaViewServiceTicket.DaGetViewServiceticket(objviewserviceticket, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objviewserviceticket);
        }

        //  Close Ticket //
        [ActionName("closeticket")]
        [HttpPost]
        public HttpResponseMessage PostCloseTicket(closeticket values)
        {
          objDaViewServiceTicket.DaPostCloseTicket(values.complaint_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("incompleteticket")]
        [HttpGet]
        public HttpResponseMessage GetIncompleteTicket(string complaint_gid)
        {
            incompleteticket objincompleteticket = new incompleteticket();
            var status = objDaViewServiceTicket.DaGetIncompleteTicket(complaint_gid,objincompleteticket);
            return Request.CreateResponse(HttpStatusCode.OK, objincompleteticket);
        }

        // View Uploaded Document

        [ActionName("viewdocument")]
        [HttpGet]
        public HttpResponseMessage GetUploadedDocuments(string complaint_gid)
        {
            viewDocument objviewDocument = new viewDocument();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            var status = objDaViewServiceTicket.DaGetUploadDoc(objviewDocument, complaint_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objviewDocument);
        }

        // Popup Ticket Details....//

        [ActionName("ticketdetails_view")]
        [HttpGet]
        public HttpResponseMessage GetTicketDetailsView(string complaint_gid)
        {
            viewticket_details objviewticketdetails = new viewticket_details();
            var status = objDaViewServiceTicket.DaGetTicketDetails(complaint_gid, objviewticketdetails);
            return Request.CreateResponse(HttpStatusCode.OK, objviewticketdetails);
        }

        // Response Log...//
        [ActionName("response_logdetails")]
        [HttpPost]
        public HttpResponseMessage PostResponseLog(responselog values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
         objDaViewServiceTicket.DaPostResponseLog(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Response Log Deatils View.....//

        [ActionName("responselogdetails_view")]
        [HttpGet]
        public HttpResponseMessage GetResponseLogDetails(string complaint_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            responselogdetails objresponselogdetails = new responselogdetails();
            objDaViewServiceTicket.DaGetResponseLogDetails(complaint_gid, objresponselogdetails, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresponselogdetails);
        }

    }
}
