using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.osd.Models;
using ems.osd.DataAccess;
using System.Net.Http;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdTrnRequestApproval")]
    [AllowAnonymous]

    public class OsdTrnRequestApprovalController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdTrnRequestApproval objDaOsdTrnRequestApproval = new DaOsdTrnRequestApproval();

        [ActionName("PostRequestApproved")]
        [HttpPost]
        public HttpResponseMessage PostRequestApproved(requestapproval values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaPostRequestApproved(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostRequestRejected")]
        [HttpPost]
        public HttpResponseMessage PostRequestRejected(requestapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaPostRequestRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostRequestCancelled")]
        [HttpPost]
        public HttpResponseMessage PostRequestCancelled(requestapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaPostRequestCancelled(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRequestDtl")]
        [HttpGet]
        public HttpResponseMessage GetRequestDtl(string approval_token)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            requesttokendtl objvalues = new requesttokendtl();
            objDaOsdTrnRequestApproval.DaGetRequestDtl(approval_token, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            requestapproval values = new requestapproval();
            objDaOsdTrnRequestApproval.DaGetApprovalSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }      

        [ActionName("GetApprovaldetails")]
        [HttpGet]
        public HttpResponseMessage GetApprovaldetails(string requestapproval_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            requestapproval values = new requestapproval();
            objDaOsdTrnRequestApproval.DaGetApprovaldetails(values, getsessionvalues.employee_gid, requestapproval_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRHApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetRHApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            requestapproval values = new requestapproval();
            objDaOsdTrnRequestApproval.DaGetRHApprovalSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRHApprovaldetails")]
        [HttpGet]
        public HttpResponseMessage GetRHApprovaldetails(string bankalertrefundapprl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            requestapproval values = new requestapproval();
            objDaOsdTrnRequestApproval.DaGetRHApprovaldetails(values, getsessionvalues.employee_gid, bankalertrefundapprl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostRHApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostRHApprovalUpdate(requestapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaPostRHApprovalUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostRHApprovalReject")]
        [HttpPost]
        public HttpResponseMessage PostRHApprovalReject(requestapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaPostRHApprovalReject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRHApprovalDtlsByToken")]
        [HttpGet]
        public HttpResponseMessage GetRHApprovalDtlsByToken(string bankalert2allocated_gid)
        {
            requestapproval objvalues = new requestapproval();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaGetRHApprovalDtlsByToken(objvalues, bankalert2allocated_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetRHRejectedDtlsByToken")]
        [HttpGet]
        public HttpResponseMessage GetRHRejectedDtlsByToken(string bankalert2allocated_gid)
        {
            requestapproval objvalues = new requestapproval();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdTrnRequestApproval.DaGetRHRejectedDtlsByToken(objvalues, bankalert2allocated_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }


    }
}
