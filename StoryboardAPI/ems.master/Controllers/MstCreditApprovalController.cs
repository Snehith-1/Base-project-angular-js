using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCreditApproval")]
    [Authorize]
    public class MstCreditApprovalController : ApiController
    {

        DaMstCreditApproval objMstcreditapproval = new DaMstCreditApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetMyAppAssignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppAssignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppAssignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppSubmittedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppSubmittedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppSubmittedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppSubmittedtoccSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppSubmittedtoccSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppSubmittedtoccSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppcreditSubmittedtoccSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppcreditSubmittedtoccSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppcreditSubmittedtoccSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppcreditRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppcreditRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyAppcreditRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcreditheadsview")]
        [HttpGet]
        public HttpResponseMessage Getcreditheadsview(string application_gid)
        {
            MdlappCreditassign objVisitor = new MdlappCreditassign();
            objMstcreditapproval.DaGetcreditheadsview(objVisitor, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }
        // Application Initiate
        [ActionName("Getappcreditapprovalinitiate")]
        [HttpGet]
        public HttpResponseMessage Getappcreditapprovalinitiate(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstcreditapproval.DaGetappcreditapprovalinitiate(application_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Application Comment
        [ActionName("PostAppcreditqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcreditqueryadd(mdlcreditquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstcreditapproval.DaPostAppcreditqueryadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Approval Summary
        [ActionName("GetAppcreditApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applcreditapproval values = new applcreditapproval();
            objMstcreditapproval.DaGetAppcreditApprovalSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Query Summary
        [ActionName("GetAppcreditquerysSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppquerysSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applcreditapproval values = new applcreditapproval();
            objMstcreditapproval.DaGetAppcreditquerySummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //rm Query Summary
        [ActionName("GetApprmquerysSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprmquerysSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applcreditapproval values = new applcreditapproval();
            objMstcreditapproval.DaGetApprmquerysSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // View query Description
        [ActionName("GetAppcreditqueryesc")]
        [HttpGet]
        public HttpResponseMessage GetAppcreditqueryesc(string appcreditquery_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcreditquery values = new mdlcreditquery();
            objMstcreditapproval.DaGetGetAppcreditqueryesc(values, appcreditquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Head Approval 
        [ActionName("PostAppcreditHeadApproval")]
        [HttpPost]
        public HttpResponseMessage PostAppcreditHeadApproval(mdlappcreditapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstcreditapproval.DaPostAppcreditHeadApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // query status
        [ActionName("GetAppqueryStatus")]
        [HttpGet]
        public HttpResponseMessage GetAppqueryStatus(string application_gid, string appcreditapproval_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlquerystatus values = new mdlquerystatus();
            objMstcreditapproval.DaGetAppqueryStatus(values, application_gid, appcreditapproval_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // query status
        [ActionName("CADGetAppqueryStatus")]
        [HttpGet]
        public HttpResponseMessage CADGetAppqueryStatus(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlquerystatus values = new mdlquerystatus();
            objMstcreditapproval.DaCADGetAppqueryStatus(values, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update query status       
        [ActionName("GetUpdatequerStatus")]
        [HttpGet]
        public HttpResponseMessage GetUpdatequerStatus(string appcreditquery_gid, string application_gid, string close_remarks)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlquerystatus values = new mdlquerystatus();
            objMstcreditapproval.DaGetUpdatequerStatus(values, appcreditquery_gid, application_gid, close_remarks, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditApplicationCount")]
        [HttpGet]
        public HttpResponseMessage CreditApplicationCount()
        {
            credtiApplicationCount values = new credtiApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstcreditapproval.DaCreditApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyApplicationCount")]
        [HttpGet]
        public HttpResponseMessage MyApplicationCount()
        {
            credtiApplicationCount values = new credtiApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstcreditapproval.DaMyApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Approval Summary
        [ActionName("GetAppcreditApprovallogSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppcreditApprovallogSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applcreditapproval values = new applcreditapproval();
            objMstcreditapproval.GetAppcreditApprovallogSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyUpcomingApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyUpcomingApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetMyUpcomingApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditapplicationdetails")]
        [HttpGet]
        public HttpResponseMessage GetCreditapplicationdetails(string appcreditapproval_gid)
        {
            applicationdetials values = new applicationdetials();
            objMstcreditapproval.DaGetCreditapplicationdetails(values, appcreditapproval_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objMstcreditapproval.DaGetCCApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditApproval(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstcreditapproval.DaUpdateCreditApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Manager Reject 
        [ActionName("PostAppCreditManagerReject")]
        [HttpPost]
        public HttpResponseMessage PostAppCreditManagerReject(MdlappcreditManagerreject values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstcreditapproval.DaPostAppCreditManagerReject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Manager Reject Send Back Condition
        [ActionName("GetAppCreditManagerRejectsendback")]
        [HttpGet]
        public HttpResponseMessage GetAppCreditManagerRejectsendback(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlappcreditManagerreject values = new MdlappcreditManagerreject();
            objMstcreditapproval.DaGetAppCreditManagerRejectsendback(values, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // App credit Rejected Summary
        [ActionName("GetAppcreditRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppcreditRejectedSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Applcreditrejected values = new Applcreditrejected();
            objMstcreditapproval.DaGetAppcreditRejectedSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
