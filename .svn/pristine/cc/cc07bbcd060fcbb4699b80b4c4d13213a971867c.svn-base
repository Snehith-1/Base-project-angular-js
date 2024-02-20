using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.ep.Models;
using ems.utilities.Functions;
using System.Web;
using ems.ep.DataAccess;

namespace StoryboardAPI.Controllers.ems.ep
{
    [RoutePrefix("api/epwelcome")]
    [Authorize]

    public class epwelcomeController : ApiController
    {
        DaEpWelcome objdaWelcome = new DaEpWelcome();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetExternalDetails")]
        [HttpGet]
        public HttpResponseMessage GetExternalDetails(string externaluser_gid)
        {
            externaldetails values = new externaldetails();
            objdaWelcome.DaGetExternalDetails(values, externaluser_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
