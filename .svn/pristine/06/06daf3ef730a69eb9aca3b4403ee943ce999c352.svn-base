using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.master.Models;
using ems.master.DataAccess;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstTelecall")]
    [Authorize]
    public class MstTelecallController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstTelecall objDaMstTelecall = new DaMstTelecall();
        [ActionName("GetTelecall")]
        [HttpGet]
        public HttpResponseMessage GetTelecall(string urn)
        {
            MdlMstTelecall values = new MdlMstTelecall();
            objDaMstTelecall.DaGetTelecall(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetTelecall_info")]
        [HttpGet]
        public HttpResponseMessage GetTelecall_info(string telecall_gid)
        {
            telecallaccount_info values = new telecallaccount_info();
            objDaMstTelecall.DaGetTelecall_info(telecall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
    }
}
