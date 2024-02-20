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
    [RoutePrefix("api/MstHRLoanTenure")]
    [Authorize]
    public class MstHRLoanTenureController : ApiController
    {

        DaMstHRLoanTenure objDaMstHRLoanTenure = new DaMstHRLoanTenure();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        
       
        [ActionName("GetHRLoanTenure")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanTenure()
        {
            MdlMstHRLoanTenure objhrloantenure = new MdlMstHRLoanTenure();
            objDaMstHRLoanTenure.DaGetHRLoanTenure(objhrloantenure);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloantenure);
        }

        [ActionName("CreateHRLoanTenure")]
        [HttpPost]
        public HttpResponseMessage CreateHRLoanTenure(tenure values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTenure.DaCreateHRLoanTenure(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateHRLoanTenureUpdate")]
        [HttpPost]
        public HttpResponseMessage CreateHRLoanTenureUpdate(tenure values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTenure.DaCreateHRLoanTenureUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHRLoanTenureDropDown")]
        [HttpGet]
        public HttpResponseMessage GetHRLoanTenureDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanTenure values = new MdlMstHRLoanTenure();
            objDaMstHRLoanTenure.DaGetHRLoanTenureDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditHRLoanTenure")]
        [HttpGet]
        public HttpResponseMessage EditHRLoanTenure(string hrloantenure_gid)
        {
            tenure values = new tenure();
            objDaMstHRLoanTenure.DaEditHRLoanTenure(hrloantenure_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRLoanTenure")]
        [HttpPost]
        public HttpResponseMessage UpdateHRLoanTenure(tenure values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTenure.DaUpdateHRLoanTenure(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanTenure")]
        [HttpPost]
        public HttpResponseMessage InactiveHRLoanTenure(hrloantenure values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTenure.DaInactiveHRLoanTenure(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRLoanTenureHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveHRLoanTenureHistory(string hrloantenure_gid)
        {
            HRLoanTenureInactiveHistory objtenurehistory = new HRLoanTenureInactiveHistory();
            objDaMstHRLoanTenure.DaInactiveHRLoanTenureHistory(objtenurehistory, hrloantenure_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objtenurehistory);
        }
        [ActionName("DeleteHRLoanTenure")]
        [HttpGet]
        public HttpResponseMessage DeleteHRLoanTenure(string hrloantenure_gid,string hrloantypeoffinancialassistance_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanTenure.DaDeleteHRLoanTenure(hrloantenure_gid, hrloantypeoffinancialassistance_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Gettenurelog")]
        [HttpGet]
        public HttpResponseMessage Gettenurelog(string hrloantenure_gid)
        {
            MdlMstHRLoanTenure hrloantenurelog = new MdlMstHRLoanTenure();
            objDaMstHRLoanTenure.DaGettenurelog(hrloantenurelog, hrloantenure_gid);
            return Request.CreateResponse(HttpStatusCode.OK, hrloantenurelog);
        }

    }
}