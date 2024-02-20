using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access for Application Approval flow.
    /// </summary>
    /// <remarks>Written by Sherin Augusta , Logapriya.S, Abilash.A, Premchander.K </remarks>

    [RoutePrefix("api/AgrMstApplicationApproval")]
    [Authorize]
    public class AgrMstApplicationApprovalController : ApiController
    {

        DaAgrMstApplicationApproval objMstApplicationapproval = new DaAgrMstApplicationApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Get Application Details
        [ActionName("Getapplicationdetails")]
        [HttpGet]
        public HttpResponseMessage Getapplicationdetails(string application_gid)
        {
            applicationdetials values = new applicationdetials();
            objMstApplicationapproval.DaGetapplicationdetails(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Application Hierarchy List
        [ActionName("Getapplicationhierarchylist")]
        [HttpGet]
        public HttpResponseMessage Getapplicationhierarchylist(string application_gid, string employee_gid)
        {
            applicationhierarchy values = new applicationhierarchy();
            objMstApplicationapproval.DaGetapplicationhierarchylist(values, application_gid, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Application Initiate
        [ActionName("Getappapprovalinitiate")]
        [HttpGet]
        public HttpResponseMessage Getappapprovalinitiate(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationapproval.DaGetappapprovalinitiate(application_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Add Application Comment
        [ActionName("PostApplicationcommentadd")]
        [HttpPost]
        public HttpResponseMessage PostApplicationcommentadd(mdlcomment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationapproval.DaPostApplicationcommentadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
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
            objMstApplicationapproval.DaGetApprovalSummary(values, application_gid);
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
            objMstApplicationapproval.DaGetAppcommentsSummary(values, application_gid);
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
            objMstApplicationapproval.DaGetAppcommentdesc(values, applicationcomment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Head Approval 
        [ActionName("PostApplicationHeadApproval")]
        [HttpPost]
        public HttpResponseMessage PostApplicationHeadApproval(mdlappapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstApplicationapproval.DaPostApplicationHeadApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
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
            objMstApplicationapproval.DaGetAppApprovalNewSummary(getsessionvalues.employee_gid, values);
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
            objMstApplicationapproval.DaGetAppApprovalRejectedSummary(getsessionvalues.employee_gid, values);
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
            objMstApplicationapproval.DaGetAppApprovalHoldSummary(getsessionvalues.employee_gid, values);
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
            objMstApplicationapproval.DaGetAppApprovalApprovedSummary(getsessionvalues.employee_gid, values);
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
            objMstApplicationapproval.DaGetAppCommentStatus(values, application_gid, getsessionvalues.employee_gid);
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
            objMstApplicationapproval.DaGetUpdateCommentStatus(values, applicationcomment_gid, application_gid, close_remarks, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BusinessApplicationCount")]
        [HttpGet]
        public HttpResponseMessage BusinessApplicationCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            businessApplicationCount values = new businessApplicationCount();
            objMstApplicationapproval.DaBusinessApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Bussiness Upcoming Approval Application

        [ActionName("GetApplicationUpcomingSummary")]
        [HttpGet]
        public HttpResponseMessage GetApplicationUpcomingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstApplicationapproval.DaGetApplicationUpcomingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
