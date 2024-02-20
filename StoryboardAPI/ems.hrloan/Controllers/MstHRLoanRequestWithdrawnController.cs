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
    [RoutePrefix("api/MstHRLoanRequestWithdrawn")]
    [Authorize]
    public class MstHRLoanRequestWithdrawnController : ApiController
    {
        DaMstHRLoanRequestWithdrawn objDaHRLoanRequestWithdrawn = new DaMstHRLoanRequestWithdrawn();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
       
        
        [ActionName("GetHRloanDetailsWithdrawn")]
        [HttpGet]
        public HttpResponseMessage GetHRloanDetailsWithdrawn()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanRequestWithdrawn objHRLoanRequestWithdrawn = new MdlMstHRLoanRequestWithdrawn();
            objDaHRLoanRequestWithdrawn.DaGetHRloanDetailsWithdrawn(objHRLoanRequestWithdrawn, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objHRLoanRequestWithdrawn);
        }
       

    }
}