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
    /// customerAlertGenerate Controller Class containing API methods for accessing the  DataAccess class DaCustomerAlertGenerate
    /// To get mail History View, Generate mail content, Mail mangament, Get customer mail details, Send mail , History of send mails
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/customerAlertGenerate")]
    [Authorize]
    public class CustomerAlertGenerateController : ApiController
    {
        DaCustomerAlertGenerate objDaCustomerAlert = new DaCustomerAlertGenerate();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("Generate")]
        [HttpPost]
        public HttpResponseMessage Generate(MdlCustomerAlert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerAlert .DaGetCustomerGenerate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("mailManagement")]
        [HttpGet]
        public HttpResponseMessage MailManagement()
        {
            MdlCustomerAlert objCutomerAlert = new MdlCustomerAlert();
          objDaCustomerAlert .DaGetmailManagement(objCutomerAlert);
            return Request.CreateResponse(HttpStatusCode.OK, objCutomerAlert);
        }





        [ActionName("Getcustomerdetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerdetails(string customeralert_gid)
        {
            mailalert values = new mailalert();
            objDaCustomerAlert.DaGetcustomerdetails(customeralert_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("sendMail")]
        [HttpPost]
        public HttpResponseMessage SendMail(mailalert values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerAlert.DaPostsendMail(values,getsessionvalues.employee_gid );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("mailHistory")]
        [HttpGet]
        public HttpResponseMessage MailHistory(string customer_gid)
        {
            mdlmailHistory objCutomerAlert = new mdlmailHistory();
             objDaCustomerAlert .DaGetmailHistory(objCutomerAlert,customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objCutomerAlert);
        }

        [ActionName("mailHistoryView")]
        [HttpGet]
        public HttpResponseMessage MailHistoryView(string customermail_gid)
        {
            mdlmailHistory objCutomerAlert = new mdlmailHistory();
            objDaCustomerAlert.DaGetmailHistoryView(objCutomerAlert, customermail_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objCutomerAlert);
        }



    }
}
