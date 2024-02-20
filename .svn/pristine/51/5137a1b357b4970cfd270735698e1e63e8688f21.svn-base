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
    [RoutePrefix("api/MstHRLoanApprovalsApproved")]
    [Authorize]
    public class MstHRLoanApprovalsApprovedController : ApiController
    {
        DaMstHRLoanApprovalsApproved objDaHRLoanRequestdtl = new DaMstHRLoanApprovalsApproved();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetHRloanRequestDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanRequestDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanApprovalsApproved values = new MdlMstHRLoanApprovalsApproved();
            objDaHRLoanRequestdtl.DaGetHRloanRequestDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadRequestDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadRequestDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanApprovalsApproved values = new MdlMstHRLoanApprovalsApproved();
            objDaHRLoanRequestdtl.DaGetHRloanHRheadRequestDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetHRloanApprovalSummary(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanApprovalsApproved values = new MdlMstHRLoanApprovalsApproved();
            objDaHRLoanRequestdtl.DaGetHRloanApprovalSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        [ActionName("DaGetHRloanRequestviewDetails")]
        [HttpGet]
        public HttpResponseMessage DaGetHRloanRequestviewDetails(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Approvedrequestsummary values = new Approvedrequestsummary();
            objDaHRLoanRequestdtl.DaGetHRloanRequestviewDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}