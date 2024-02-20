using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.ecms.Models;
using ems.ecms.DataAccess;
using ems.utilities.Functions;

namespace ems.ecms.Controllers
{
    /// <summary>
    /// penalityAlert Controller Class containing API methods for accessing the  DataAccess class DaPenalityAlert
    ///     penalitydetails - view, update,  send alert mails, show details based on customer, Alert with start and stop in system and mail.
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/penalityAlert")]
    [Authorize]
    public class PenalityAlertController : ApiController
    {
        DaPenalityAlert objDaPenality = new DaPenalityAlert();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("penalityManagement")]
        [HttpGet]
        public HttpResponseMessage MailManagement()
        {
            MdlCustomerAlert objpenalityAlert = new MdlCustomerAlert();
            objDaPenality.DaPenalityMgmt(objpenalityAlert); 
            return Request.CreateResponse(HttpStatusCode.OK, objpenalityAlert);
        }

        [ActionName("Getcustomerpenalitydetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerpenalitydetails(string customeralert_gid)
        {
            mailalert values = new mailalert();
           objDaPenality .DaGetPenalityDetails(values, customeralert_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getpenalityrecorddetails")]
        [HttpGet]
        public HttpResponseMessage Getpenalityrecorddetails(string customeralert_gid)
        {
            mailalert objMdlalert = new mailalert();
            objDaPenality.DaGetPenalityRecords(customeralert_gid, objMdlalert);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlalert);
        }

        [ActionName("startpenalityalert")]
        [HttpPost]
        public HttpResponseMessage Poststartpenalityalert(mailalert value)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaPenality.DaPostStartPenalityAlert(getsessionvalues.employee_gid, value.customeralert_gid, value);
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        [ActionName("endpenalityalert")]
        [HttpPost]
        public HttpResponseMessage Postendpenalityalert(mailalert value)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaPenality.DaPostendpPenalityAlert(getsessionvalues.employee_gid, value.customeralert_gid, value);
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
    }
}
