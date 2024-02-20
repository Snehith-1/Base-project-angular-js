using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.idas.Models;
using ems.idas.DataAccess;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasTrnSentMail")]
    [Authorize]
    public class IdasTrnSentMailController : ApiController
    {

        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnSentMail objDaSentMail = new DaIdasTrnSentMail();

        [ActionName("PostSendMail")]
        [HttpPost]
        public HttpResponseMessage PostSendMail(sendmail objsendmail)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSentMail.DaPostSendMail(objsendmail, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objsendmail);
        }

        [ActionName("GetSentMailSummary")]
        [HttpGet]
        public HttpResponseMessage GetSentMailSummary(string sanction_gid)
        {

            sendmail_list objResult = new sendmail_list();
            objDaSentMail.DaGetSentMailSummary(sanction_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

       

    }
}
