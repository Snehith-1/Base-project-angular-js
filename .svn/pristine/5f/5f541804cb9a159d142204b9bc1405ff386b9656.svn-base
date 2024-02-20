using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.its.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;
using ems.its.DataAccess;

namespace StoryboardAPI.Controllers.its
{
    [RoutePrefix("api/myApprovals")]
    [Authorize]
    public class myApprovalsController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //fnMyApprovals objfnMyApprovals = new fnMyApprovals();
        DaMyApprovals objDaApprovals = new DaMyApprovals();
        //  My Approval.........//

        [ActionName("myapproval")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentApprovalDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            myapproval objdepartmentapproval = new myapproval();
            //objdepartmentapproval = objfnMyApprovals.getdepartmentapproval_fn(getsessionvalues.employee_gid, getsessionvalues.user_gid);
            var status = objDaApprovals.DAGetDepartmentApproval(objdepartmentapproval, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdepartmentapproval);
        }

        //  Department Approve........//

        [ActionName("departmentApproveclick")]
        [HttpPost]
        public HttpResponseMessage PostDepartmentApprove(departmentapproved values)
        {
            departmentapproved objdepartmentapproved = new departmentapproved();
            var status = objDaApprovals.DaPostDepartmentApprove(values);
            return Request.CreateResponse(HttpStatusCode.OK, objdepartmentapproved);
        }

        // Department Reject..........//
        [ActionName("departmentreject")]
        [HttpPost]
        public HttpResponseMessage PostDepartmentReject(departmentreject values)
        {
            var status = objDaApprovals.DaPostDepartmentReject(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Department Internal Approval..........//

        [ActionName("departmentinternal")]
        [HttpPost]
        public HttpResponseMessage PostDepartmentInternal(departmentinternal values)
        {
            departmentinternal objdepartmentinternal = new departmentinternal();
            var status = objDaApprovals.DaPostDepartmentInternal(values);
            return Request.CreateResponse(HttpStatusCode.OK, objdepartmentinternal);
        }

        // Service Approve...//

        [ActionName("serviceapprove")]
        [HttpPost]
        public HttpResponseMessage PostServiceApprove(serviceapprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaApprovals.DaPostServiceApprove(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Service Reject ....//

        [ActionName("servicereject")]
        [HttpPost]
        public HttpResponseMessage PostServiceReject(servicereject values)
        {
            servicereject objservicereject = new servicereject();
            var status = objDaApprovals.DaPostServiceReject(values);
            return Request.CreateResponse(HttpStatusCode.OK, objservicereject);
        }

        // Service Internal Approval ....//

        [ActionName("serviceinternalapprove")]
        [HttpPost]
        public HttpResponseMessage PostServiceInternal(serviceinternal values)
        {
            var status = objDaApprovals.DaPostServiceInternal(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Management Approve ....//

        [ActionName("manageapprove")]
        [HttpPost]
        public HttpResponseMessage PostManageApprove(managementapprove values)
        {
            var status = objDaApprovals.DaPostManageApprove(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Management Reject ....//
        [ActionName("managereject")]
        [HttpPost]
        public HttpResponseMessage PostManageReject(managementreject values)
        {
            var status = objDaApprovals.DaPostManageReject(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // View Department details....//
        [ActionName("viewdepartment")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentDetails(string serviceapproval_gid)
        {
            viewdepartment objviewdepartment = new viewdepartment();
            var status = objDaApprovals.DaGetViewDepartment(serviceapproval_gid, objviewdepartment);
            return Request.CreateResponse(HttpStatusCode.OK, objviewdepartment);
        }

        // View Service details....//
        [ActionName("viewservice")]
        [HttpGet]
        public HttpResponseMessage GetServiceDetails(string serviceapproval_gid)
        {
            viewservice objviewservice = new viewservice();
            var status = objDaApprovals.DaGetViewService(serviceapproval_gid, objviewservice);
            return Request.CreateResponse(HttpStatusCode.OK, objviewservice);
        }

        // View Management details....//
        [ActionName("viewmanagement")]
        [HttpGet]
        public HttpResponseMessage GetManagementDetails(string serviceapproval_gid)
        {
            viewmanagement objviewmanagement = new viewmanagement();
            var status = objDaApprovals.DaGetViewManagement(serviceapproval_gid, objviewmanagement);
            return Request.CreateResponse(HttpStatusCode.OK, objviewmanagement);
        }

        // Dependency Approval ...//

        [ActionName("dependencyapprove")]
        [HttpPost]
        public HttpResponseMessage PostDependencyApprove(dependencyapprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaApprovals.DaPostDependencyApprove(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Dependency Reject ...//

        [ActionName("dependencyreject")]
        [HttpPost]
        public HttpResponseMessage PostDependencyRreject(dependencyreject values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaApprovals.DaPostDependencyReject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // CAB Approval ...//

        [ActionName("cabapprove")]
        [HttpPost]
        public HttpResponseMessage PostCabApprove(cabapprove values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaApprovals.DaPostCabApprove(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // CAB Reject ...//

        [ActionName("cabreject")]
        [HttpPost]
        public HttpResponseMessage PostCabReject(cabreject values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaApprovals.DaPostCabReject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Release Details ...//

        [ActionName("releasedetails")]
        [HttpGet]
        public HttpResponseMessage GetReleaseDetails(string release_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            releaseapprovaldetails objreleasedetails = new releaseapprovaldetails();
            var status = objDaApprovals.DaGetReleaseDetails(release_gid, objreleasedetails, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objreleasedetails);
        }
        [ActionName("uatdetails")]
        [HttpGet]
        public HttpResponseMessage GetIssueReleaseDetails(string issuetracker_gid)
        {
            uatlog objuatdetails = new uatlog();
            var status = objDaApprovals.DaGetUatDetails(issuetracker_gid, objuatdetails);
            return Request.CreateResponse(HttpStatusCode.OK, objuatdetails);
        }

        [ActionName("ApprovalRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetApprovalRemarksView(string release_gid)
        {
            cabapprove values = new cabapprove();
            objDaApprovals.DaGetApprovalRemarksView(release_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
