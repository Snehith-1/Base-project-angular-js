using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rsk.Models;
using ems.rsk.DataAccess;

namespace ems.rsk.Controllers
{
    [RoutePrefix("api/MyCustomer")]
    [Authorize]

    public class MyCustomerController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMyCustomer objDaMyCustomer = new DaMyCustomer();

        [ActionName("GetZonalCustomerRMDetail")]
        [HttpGet]
        public HttpResponseMessage GetZonalCustomerRMDetail()
        {
            customerRMdtl objMdlCustomer = new customerRMdtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyCustomer.DaGetZonalCustomerRMDetail(objMdlCustomer, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
    }
}
