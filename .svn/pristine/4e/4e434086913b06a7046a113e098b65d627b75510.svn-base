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
    [RoutePrefix("api/MstRepayment")]
    [Authorize]
    public class MstRepaymentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstRepayment objDaMstRepayment = new DaMstRepayment();
        [ActionName("GetRepayment")]
        [HttpGet]
        public HttpResponseMessage GetTelecall(string urn)
        {
            MdlMstRepayment values = new MdlMstRepayment();
            objDaMstRepayment.DaGetRepayment(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRepayment_list")]
        [HttpGet]
        public HttpResponseMessage GetRepayment_list(string account_no)
        {
            MdlMstRepayment values = new MdlMstRepayment();
            objDaMstRepayment.DaGetRepayment_list(account_no, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRepayment_info")]
        [HttpGet]
        public HttpResponseMessage GetRepayment_info(string repyment_gid)
        {
            MdlRepaymentInfo values = new MdlRepaymentInfo();
            objDaMstRepayment.DaGetRepayment_info(repyment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
