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
    [RoutePrefix("api/MstHRLoanTermsandConditions")]
    [Authorize]
    public class MstHRLoanTermsandConditionsController : ApiController
    {
        DaMstHRLoanTermsandConditions objDaMstHRLoanTermsandConditions = new DaMstHRLoanTermsandConditions();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetHRLoanTermsandConditions")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanTermsandConditions()
        {
            MdlMstHRLoanTermsandConditions objhrloantermsandconditions = new MdlMstHRLoanTermsandConditions();
            objDaMstHRLoanTermsandConditions.DaGetHRLoanTermsandConditions(objhrloantermsandconditions);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloantermsandconditions);
        }

        [ActionName("CreateHRLoanTermsandConditions")]
        [HttpPost]
        public HttpResponseMessage CreateHRLoanTermsandConditions(hrloantermsandconditions values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTermsandConditions.DaCreateHRLoanTermsandConditions(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditHRLoanTermsandConditions")]
        [HttpGet]
        public HttpResponseMessage EditHRLoanTermsandConditions(string hrloantermsandconditions_gid)
        {
            hrloantermsandconditions values = new hrloantermsandconditions();
            objDaMstHRLoanTermsandConditions.DaEditHRLoanTermsandConditions(hrloantermsandconditions_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRLoanTermsandConditions")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanTermsandConditions(hrloantermsandconditions values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTermsandConditions.DaUpdateHRLoanTermsandConditions(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanTermsandConditions")]
        [HttpPost]
        public HttpResponseMessage InactiveHRLoanTermsandConditions(hrloantermsandconditions values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTermsandConditions.DaInactiveHRLoanTermsandConditions(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanTermsandConditionsHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveHRLoanTermsandConditionsHistory(string hrloantermsandconditions_gid)
        {
            HRLoanTermsandConditionsInactiveHistory objhrloantermsandconditionshistory = new HRLoanTermsandConditionsInactiveHistory();
            objDaMstHRLoanTermsandConditions.DaInactiveHRLoanTermsandConditionsHistory(objhrloantermsandconditionshistory, hrloantermsandconditions_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloantermsandconditionshistory);
        }
        [ActionName("DeleteHRLoanTermsandConditions")]
        [HttpGet]
        public HttpResponseMessage DeleteHRLoanTermsandConditions(string hrloantermsandconditions_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTermsandConditions.DaDeleteHRLoanTermsandConditions(hrloantermsandconditions_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}