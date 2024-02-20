using ems.osd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.osd.Models;
using System.Net.Http;
using System.Net;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdTrnBankMail")]
    [AllowAnonymous]
    public class OsdTrnBankMailController : ApiController
    {
        DaOsdTrnBankMail objDaAccess = new DaOsdTrnBankMail();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("PostMailContent")]
        [HttpPost]
        public HttpResponseMessage PostMailContent()
        {

            Mailcontent values = new Mailcontent();
            var bodyStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();

            // return bodyText;
            objDaAccess.DaPostMailContent(values, bodyText);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
