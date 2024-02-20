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
    [RoutePrefix("api/IdasTrnDocConversation")]
    [Authorize]
    public class IdasTrnDocConversationController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasDocConversation objDaSanctionDoc = new DaIdasDocConversation();

        [ActionName("GetUploadDoc")]
        [HttpGet]
        public HttpResponseMessage DocConversation(string docconversation_gid)
        {
            uploaddocumentlist values = new uploaddocumentlist();

            objDaSanctionDoc.DaGetUploadDocument(values, docconversation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTypeOfCopy")]
        [HttpPost]
        public HttpResponseMessage PostTypeOfCopy(types_of_copy values)
        {
           
            objDaSanctionDoc.DaPostTypesOfCopy(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
