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
    [RoutePrefix("api/MstHRLoanApprovalsRejected")]
    [Authorize]
    public class MstHRLoanApprovalsRejectedController : ApiController
    {
        DaMstHRLoanApprovalsRejected objDaHRLoanRequestdtl = new DaMstHRLoanApprovalsRejected();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();             
        
        
        [ActionName("GetHRloanRequestDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanRequestDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanApprovalsRejected values = new MdlMstHRLoanApprovalsRejected();
            objDaHRLoanRequestdtl.DaGetHRloanRequestDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanHRheadRequestDetails")]
        [HttpGet]
        public HttpResponseMessage GetHRloanHRheadRequestDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanApprovalsRejected values = new MdlMstHRLoanApprovalsRejected();
            objDaHRLoanRequestdtl.DaGetHRloanHRheadRequestDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHRloanApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetHRloanApprovalSummary(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanApprovalsRejected values = new MdlMstHRLoanApprovalsRejected();
            objDaHRLoanRequestdtl.DaGetHRloanApprovalSummary(values, getsessionvalues.user_gid, getsessionvalues.employee_gid, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("DaGetHRloanRequestviewDetails")]
        [HttpGet]
        public HttpResponseMessage DaGetHRloanRequestviewDetails(string request_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Rejectedrequestsummary values = new Rejectedrequestsummary();
            objDaHRLoanRequestdtl.DaGetHRloanRequestviewDetails(values, getsessionvalues.user_gid, getsessionvalues.employee_gid, request_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}