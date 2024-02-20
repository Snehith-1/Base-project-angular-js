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
    [RoutePrefix("api/MstHRLoanPurpose")]
    [Authorize]
    public class MstHRLoanPurposeController : ApiController
    {
        DaMstHRLoanPurpose objDaMstHRLoanPurpose = new DaMstHRLoanPurpose();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetHRLoanPurpose")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanPurpose()
        {
            MdlMstHRLoanPurpose objhrloanpurpose = new MdlMstHRLoanPurpose();
            objDaMstHRLoanPurpose.DaGetHRLoanPurpose(objhrloanpurpose);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloanpurpose);
        }

        [ActionName("CreateHRLoanPurpose")]
        [HttpPost]
        public HttpResponseMessage CreateHRLoanPurpose(hrloanpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanPurpose.DaCreateHRLoanPurpose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditHRLoanPurpose")]
        [HttpGet]
        public HttpResponseMessage EditHRLoanPurpose(string hrloanpurpose_gid)
        {
            hrloanpurpose values = new hrloanpurpose();
            objDaMstHRLoanPurpose.DaEditHRLoanPurpose(hrloanpurpose_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRLoanPurpose")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanPurpose(hrloanpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanPurpose.DaUpdateHRLoanPurpose(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanPurpose")]
        [HttpPost]
        public HttpResponseMessage InactiveHRLoanPurpose(hrloanpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanPurpose.DaInactiveHRLoanPurpose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanPurposeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveHRLoanPurposeHistory(string hrloanpurpose_gid)
        {
            HRLoanPurposeInactiveHistory objhrloanpurposehistory = new HRLoanPurposeInactiveHistory();
            objDaMstHRLoanPurpose.DaInactiveHRLoanPurposeHistory(objhrloanpurposehistory, hrloanpurpose_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloanpurposehistory);
        }
        [ActionName("DeleteHRLoanPurpose")]
        [HttpGet]
        public HttpResponseMessage DeleteHRLoanPurpose(string hrloanpurpose_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanPurpose.DaDeleteHRLoanPurpose(hrloanpurpose_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}