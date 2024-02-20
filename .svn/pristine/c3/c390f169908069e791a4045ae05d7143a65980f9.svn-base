using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.lp.Models;
using ems.lp.DataAccess;
using ems.utilities.Functions;
using System.Web;


namespace ems.lp.Controllers
{
    [RoutePrefix("api/welcome")]
    [AllowAnonymous]

    public class WelcomeController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaWelcome objdaWelcome = new DaWelcome();

        [ActionName("Getlawyerdetails")]
        [HttpGet]
        public HttpResponseMessage Getlawyerdetails(string lawyerregister_gid)
        {
            lawyerdetails values = new lawyerdetails();
             objdaWelcome.DaGetLawyerDetails(values, lawyerregister_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLawyerEmail")]
        [HttpPost]
        public HttpResponseMessage PostLawyerEmail(lawyerdetails values)
        {
            //lawyerdetails values = new lawyerdetails();
            objdaWelcome.DaPostLawyerEmail(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updatelawyerpassword")]
        [HttpPost]
        public HttpResponseMessage updatelawyerpassword(lawyerdetails values)
        {
            //lawyerdetails values = new lawyerdetails();
            objdaWelcome.Daupdatelawyerpassword(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
