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
    [RoutePrefix("api/MstHRLoanTypeofFinancialAssistance")]
    [Authorize]
    public class MstHRLoanTypeofFinancialAssistanceController : ApiController
    {
        DaMstHRLoanTypeofFinancialAssistance objDaMstHRLoanTypeofFinancialAssistance = new DaMstHRLoanTypeofFinancialAssistance();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetHRLoanTypeofFinancialAssistance")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanTypeofFinancialAssistance()
        {
            MdlMstHRLoanTypeofFinancialAssistance objtypeoffinancialassistance = new MdlMstHRLoanTypeofFinancialAssistance();
            objDaMstHRLoanTypeofFinancialAssistance.DaGetHRLoanTypeofFinancialAssistance(objtypeoffinancialassistance);
            return Request.CreateResponse(HttpStatusCode.OK, objtypeoffinancialassistance);
        }

        [ActionName("CreateHRLoanTypeofFinancialAssistance")]
        [HttpPost]
        public HttpResponseMessage CreateHRLoanTypeofFinancialAssistance(typeoffinancialassistance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTypeofFinancialAssistance.DaCreateHRLoanTypeofFinancialAssistance(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditHRLoanTypeofFinancialAssistance")]
        [HttpGet]
        public HttpResponseMessage EditHRLoanTypeofFinancialAssistance(string hrloantypeoffinancialassistance_gid)
        {
            typeoffinancialassistance values = new typeoffinancialassistance();
            objDaMstHRLoanTypeofFinancialAssistance.DaEditHRLoanTypeofFinancialAssistance(hrloantypeoffinancialassistance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRLoanTypeofFinancialAssistance")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanTypeofFinancialAssistance(typeoffinancialassistance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTypeofFinancialAssistance.DaUpdateHRLoanTypeofFinancialAssistance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanTypeofFinancialAssistance")]
        [HttpPost]
        public HttpResponseMessage InactiveHRLoanTypeofFinancialAssistance(typeoffinancialassistance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTypeofFinancialAssistance.DaInactiveHRLoanTypeofFinancialAssistance(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanTypeofFinancialAssistanceHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveHRLoanTypeofFinancialAssistanceHistory(string hrloantypeoffinancialassistance_gid)
        {
            TypeofFinancialAssistanceInactiveHistory objtypeoffinancialassistancehistory = new TypeofFinancialAssistanceInactiveHistory();
            objDaMstHRLoanTypeofFinancialAssistance.DaInactiveHRLoanTypeofFinancialAssistanceHistory(objtypeoffinancialassistancehistory, hrloantypeoffinancialassistance_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objtypeoffinancialassistancehistory);
        }
        [ActionName("DeleteHRLoanTypeofFinancialAssistance")]
        [HttpGet]
        public HttpResponseMessage DeleteHRLoanTypeofFinancialAssistance(string hrloantypeoffinancialassistance_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTypeofFinancialAssistance.DaDeleteHRLoanTypeofFinancialAssistance(hrloantypeoffinancialassistance_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}