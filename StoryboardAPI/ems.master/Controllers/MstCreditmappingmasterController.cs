using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.master.DataAccess;
using ems.master.Models;

namespace ems.master.Controllers
{
    [RoutePrefix("api/creditmastermapping")]
    [Authorize]
    public class MstCreditmappingmasterController : ApiController
    {
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCreditmappingmaster objDacreditmappingMaster = new DaMstCreditmappingmaster();


        [ActionName("employeelist")]
        [HttpGet]
        public HttpResponseMessage GetRoleSummary()
        {
            Mdlemployee objemployeelist = new Mdlemployee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDacreditmappingMaster.Daemployeelist(objemployeelist);
            return Request.CreateResponse(HttpStatusCode.OK, objemployeelist);
        }
    }
}
