using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.its.Models;
using ems.its.DataAccess;

namespace StoryboardAPI.Controllers.ems.its
{
    [RoutePrefix("api/deployment")]
    [Authorize]
    public class deploymentController : ApiController
    {
        DaDeployment objdaDeployment = new DaDeployment();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Get Client List

        [ActionName("getClientList")]
        [HttpGet]
        public HttpResponseMessage GetClient()
        {
            client_list objResult = new client_list();
            var status = objdaDeployment.DaGetClientList(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        // Get Project List

        [ActionName("getProjectList")]
        [HttpGet]
        public HttpResponseMessage GetProjectList(string id)
        {
            project_list objResult = new project_list();
            var status = objdaDeployment.DaGetProjectList(id, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        // Add Deployment

        [ActionName("addDeployment")]
        [HttpPost]
        public HttpResponseMessage AddDeployment(add values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objdaDeployment.DaAddDeployment(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        // Get Summary

        [ActionName("summary")]
        [HttpGet]
        public HttpResponseMessage GetSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            summary_list objResult = new summary_list();
            var status = objdaDeployment.DaGetSummary(objResult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }


        // Get Edit Data

        [ActionName("edit")]
        [HttpGet]
        public HttpResponseMessage GetEditData(string id)
        {
            edit objResult = new edit();
            var status = objdaDeployment.DaEditDeployment(id, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        // Update Deployment Record

        [ActionName("update")]
        [HttpPost]
        public HttpResponseMessage UpdateRecord(update values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objdaDeployment.DaUpdateDeployment(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        // View Deployment Data

        [ActionName("view")]
        [HttpGet]
        public HttpResponseMessage ViewDeployment(string id)
        {
            view objResult = new view();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objdaDeployment.DaViewDeployment(id, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        // delete Deployment Data

        [ActionName("delete")]
        [HttpPost]
        public HttpResponseMessage DelRecord(string id)
        {
            var status = objdaDeployment.DaDelRecord(id);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        // Get Employees

        [ActionName("employee")]
        [HttpGet]
        public HttpResponseMessage GetEmployee()
        {
            employee objResult = new employee();
            var status = objdaDeployment.DaGetEmployee(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        // Approve Deployment

        [ActionName("approve")]
        [HttpPost]
        public HttpResponseMessage DepApprove(string id)
        {
            var status = objdaDeployment.Approve(id);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        // Reject Deployment

        [ActionName("reject")]
        [HttpPost]
        public HttpResponseMessage DepReject(string id)
        {
            var status = objdaDeployment.Reject(id);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }

        [ActionName("deploymentStatus")]
        [HttpPost]
        public HttpResponseMessage PostStatus(deploymentStatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objdaDeployment.UpdateDepStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, status);
        }
    }
}
