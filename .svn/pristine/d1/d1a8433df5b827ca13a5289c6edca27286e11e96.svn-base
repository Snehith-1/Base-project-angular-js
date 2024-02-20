using ems.hrloan.DataAccess;
using ems.hrloan.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.hrloan.Controllers
{
    [RoutePrefix("api/MstHRLoanHRMappingApprovals")]
    [Authorize]
    public class MstHRLoanHRMappingApprovalsController : ApiController
    {
        DaMstHRLoanHRMappingApprovals objDaMstHRLoanHRMappingApprovals = new DaMstHRLoanHRMappingApprovals();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        
        [ActionName("GetHRMapping")]
        [HttpGet]
        public HttpResponseMessage GetHRMapping()
        {
            MdlMstHRLoanHRMappingApprovals values = new MdlMstHRLoanHRMappingApprovals();
            objDaMstHRLoanHRMappingApprovals.DaGetHRMapping(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateHRMapping")]
        [HttpPost]
        public HttpResponseMessage CreateHRMapping(MdlMstHRLoanHRMappingApprovals values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRMappingApprovals.DaCreateHRMapping(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditHRMapping")]
        [HttpGet]
        public HttpResponseMessage EditHRMapping(string hrmapping_gid)
        {
            MdlMstHRLoanHRMappingApprovals values = new MdlMstHRLoanHRMappingApprovals();
            objDaMstHRLoanHRMappingApprovals.DaEditHRMapping(hrmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRMapping")]
        [HttpPost]
        public HttpResponseMessage UpdateHRMapping(MdlMstHRLoanHRMappingApprovals values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRMappingApprovals.DaUpdateHRMapping(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveHRMapping")]
        [HttpPost]
        public HttpResponseMessage InactiveHRMapping(MdlMstHRLoanHRMappingApprovals values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRMappingApprovals.DaInactiveHRMapping(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteHRMapping")]
        [HttpGet]
        public HttpResponseMessage DeleteHRMapping(string hrmapping_gid,string hrmapping_name)
        {
            MdlMstHRLoanHRMappingApprovals values = new MdlMstHRLoanHRMappingApprovals();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRMappingApprovals.DaDeleteHRMapping(hrmapping_gid, hrmapping_name, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("HRMappingInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage HRMappingInactiveLogview(string hrmapping_gid)
        {
            MdlMstHRLoanHRMappingApprovals values = new MdlMstHRLoanHRMappingApprovals();
            objDaMstHRLoanHRMappingApprovals.DaHRMappingInactiveLogview(hrmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            MdlMstHRLoanHRMappingApprovals objmaster = new MdlMstHRLoanHRMappingApprovals();
            objDaMstHRLoanHRMappingApprovals.DaGetEmployeelist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }       

        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string hrmapping_gid)
        {
            hrmappingemployee values = new hrmappingemployee();
            objDaMstHRLoanHRMappingApprovals.DaGetEmployeeName(hrmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetManagerName")]
        [HttpGet]
        public HttpResponseMessage GetManagerName()
        {
            hrmappingemployee values = new hrmappingemployee();
            objDaMstHRLoanHRMappingApprovals.DaGetManagerName(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApproverName")]
        [HttpGet]
        public HttpResponseMessage GetApproverName()
        {
            hrmappingemployee values = new hrmappingemployee();
            objDaMstHRLoanHRMappingApprovals.DaGetApproverName(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}