using ems.hrloan.DataAccess;
using ems.hrloan.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Web;

namespace ems.hrloan.Controllers
{
    [RoutePrefix("api/MstHRLoanRequestCompleted")]
    [Authorize]
    public class MstHRLoanRequestCompletedController : ApiController
    {
        DaMstHRLoanRequestCompleted objDaHRLoanRequestCompleted = new DaMstHRLoanRequestCompleted();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
                     
        [ActionName("GetHRloanDetailsCompleted")]
        [HttpGet]
        public HttpResponseMessage GetHRloanDetailsCompleted()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequestCompleted objHRLoanRequestCompleted = new MdlMstHRLoanRequestCompleted();
            objDaHRLoanRequestCompleted.DaGetHRloanDetailsCompleted(objHRLoanRequestCompleted, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequestCompleted);
        }
        [ActionName("GetHRloanDetailsRejected")]
        [HttpGet]
        public HttpResponseMessage GetHRloanDetailsRejected()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequestCompleted objHRLoanRequestRejected = new MdlMstHRLoanRequestCompleted();
            objDaHRLoanRequestCompleted.DaGetHRloanDetailsRejected(objHRLoanRequestRejected, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequestRejected);
        }      
        
      

    }
}