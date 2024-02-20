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
    [RoutePrefix("api/MstGuarantor")]
    [Authorize]
    public class MstGuarantorController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstGuarantor objDaMstGuarantor = new DaMstGuarantor();

        [ActionName("GetGuarantorList")]
        [HttpGet]
        public HttpResponseMessage getguarantorlist()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGuarantor values = new MdlMstGuarantor();
            objDaMstGuarantor.DaGetGuarantorList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //--------Get Customer 2 user Details--------//
        [ActionName("GetList")]
        [HttpGet]
        public HttpResponseMessage GetList(string guarantor_id)
        {
            MdlCustomer values = new MdlCustomer();
            objDaMstGuarantor.DaGetList(guarantor_id, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
