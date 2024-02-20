using ems.osd.DataAccess;
using ems.osd.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdRptAllTickets")]
    [Authorize]
    public class OsdRptAllTicketsController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdRptAllTickets objDaOsdRptAllTickets = new DaOsdRptAllTickets();

        [ActionName("GetAllTicketsSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllTicketsSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            servicerequestdtllist values = new servicerequestdtllist();
            objDaOsdRptAllTickets.DaGetAllTicketsSummary(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAllTicketsSummarySearch")]
        [HttpPost]
        public HttpResponseMessage PostAllTicketsSummarySearch(servicerequestdtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdRptAllTickets.DaPostAllTicketsSummarySearch(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TicketExport")]
        [HttpPost]
        public HttpResponseMessage TicketExport(servicerequestdtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdRptAllTickets.DaTicketExport(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
