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
    [RoutePrefix("api/MstHRLoanSeverity")]
    [Authorize]
    public class MstHRLoanSeverityController : ApiController
    {
        DaMstHRLoanSeverity objDaMstHRLoanSeverity = new DaMstHRLoanSeverity();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetHRLoanSeverity")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanSeverity()
        {
            MdlMstHRLoanSeverity objhrloanseverity = new MdlMstHRLoanSeverity();
            objDaMstHRLoanSeverity.DaGetHRLoanSeverity(objhrloanseverity);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloanseverity);
        }

        [ActionName("CreateHRLoanSeverity")]
        [HttpPost]
        public HttpResponseMessage CreateHRLoanSeverity(hrloanseverity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanSeverity.DaCreateHRLoanSeverity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditHRLoanSeverity")]
        [HttpGet]
        public HttpResponseMessage EditHRLoanSeverity(string hrloanseverity_gid)
        {
            hrloanseverity values = new hrloanseverity();
            objDaMstHRLoanSeverity.DaEditHRLoanSeverity(hrloanseverity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRLoanSeverity")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanSeverity(hrloanseverity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanSeverity.DaUpdateHRLoanSeverity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanSeverity")]
        [HttpPost]
        public HttpResponseMessage InactiveHRLoanSeverity(hrloanseverity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanSeverity.DaInactiveHRLoanSeverity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanSeverityHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveHRLoanSeverityHistory(string hrloanseverity_gid)
        {
            HRLoanSeverityInactiveHistory objhrloanseverityhistory = new HRLoanSeverityInactiveHistory();
            objDaMstHRLoanSeverity.DaInactiveHRLoanSeverityHistory(objhrloanseverityhistory, hrloanseverity_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloanseverityhistory);
        }
        [ActionName("DeleteHRLoanSeverity")]
        [HttpGet]
        public HttpResponseMessage DeleteHRLoanSeverity(string hrloanseverity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanSeverity.DaDeleteHRLoanSeverity(hrloanseverity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}