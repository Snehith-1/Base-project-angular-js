using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to supplier business approval process in aplication creation flow.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>

    [RoutePrefix("api/AgrTrnSuprApplicationApproval")]
    [Authorize]

    public class AgrTrnSuprApplicationApprovalController : ApiController

    {
        DaAgrTrnSuprApplicationApproval objDaAgrTrnApplicationApproval = new DaAgrTrnSuprApplicationApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Get Application Details
        [ActionName("Getapplicationdetails")]
        [HttpGet]
        public HttpResponseMessage Getapplicationdetails(string application_gid)
        {
            applicationdetials values = new applicationdetials();
            objDaAgrTrnApplicationApproval.DaGetapplicationdetails(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Application Hierarchy List
        [ActionName("Getapplicationhierarchylist")]
        [HttpGet]
        public HttpResponseMessage Getapplicationhierarchylist(string application_gid, string employee_gid)
        {
            applicationhierarchy values = new applicationhierarchy();
            objDaAgrTrnApplicationApproval.DaGetapplicationhierarchylist(values, application_gid, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Application Initiate
        [ActionName("Getappapprovalinitiate")]
        [HttpGet]
        public HttpResponseMessage Getappapprovalinitiate(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnApplicationApproval.DaGetappapprovalinitiate(application_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Add Application Comment
        [ActionName("PostApplicationcommentadd")]
        [HttpPost]
        public HttpResponseMessage PostApplicationcommentadd(mdlcomment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnApplicationApproval.DaPostApplicationcommentadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Approval Summary
        [ActionName("GetAppApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applicationapproval values = new applicationapproval();
            objDaAgrTrnApplicationApproval.DaGetApprovalSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Comment Summary
        [ActionName("GetAppcommentsSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppcommentsSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applicationapproval values = new applicationapproval();
            objDaAgrTrnApplicationApproval.DaGetAppcommentsSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // View Comment Description
        [ActionName("GetAppcommentdesc")]
        [HttpGet]
        public HttpResponseMessage GetAppcommentdesc(string applicationcomment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcommentdesc values = new mdlcommentdesc();
            objDaAgrTrnApplicationApproval.DaGetAppcommentdesc(values, applicationcomment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Head Approval 
        [ActionName("PostApplicationHeadApproval")]
        [HttpPost]
        public HttpResponseMessage PostApplicationHeadApproval(mdlappapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnApplicationApproval.DaPostApplicationHeadApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get New Approval Application

        [ActionName("GetApplicationNewSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationNewSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnApplicationApproval.DaGetAppApprovalNewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Rejected Approval Applicaiton
        [ActionName("GetApplicationRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnApplicationApproval.DaGetAppApprovalRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Hold Approval Applicaiton
        [ActionName("GetApplicationHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnApplicationApproval.DaGetAppApprovalHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Approved Approval Applicaiton
        [ActionName("GetApplicationApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationapprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnApplicationApproval.DaGetAppApprovalApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Comment status
        [ActionName("GetAppCommentStatus")]
        [HttpGet]
        public HttpResponseMessage GetAppCommentStatus(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcommentstatus values = new mdlcommentstatus();
            objDaAgrTrnApplicationApproval.DaGetAppCommentStatus(values, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update Comment status       
        [ActionName("GetUpdateCommentStatus")]
        [HttpGet]
        public HttpResponseMessage GetUpdateCommentStatus(string applicationcomment_gid, string application_gid, string close_remarks)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcommentstatus values = new mdlcommentstatus();
            objDaAgrTrnApplicationApproval.DaGetUpdateCommentStatus(values, applicationcomment_gid, application_gid, close_remarks, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BusinessApplicationCount")]
        [HttpGet]
        public HttpResponseMessage BusinessApplicationCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            businessApplicationCount values = new businessApplicationCount();
            objDaAgrTrnApplicationApproval.DaBusinessApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
