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
    /// This Controllers will help to pull data's from back end and trigger approvals from credit risk approval stage 
    /// </summary>
    /// <remarks>Written by Venkatesh </remarks>

    [RoutePrefix("api/AgrTrnCreditApproval")]
    [Authorize]
    public class AgrTrnCreditApprovalController : ApiController
    {

        DaAgrTrnCreditApproval objAgrTrncreditapproval = new DaAgrTrnCreditApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetMyAppAssignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppAssignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppAssignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppSubmittedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppSubmittedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppSubmittedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppSubmittedtoccSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppSubmittedtoccSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppSubmittedtoccSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppcreditSubmittedtoccSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppcreditSubmittedtoccSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppcreditSubmittedtoccSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppcreditRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppcreditRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyAppcreditRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcreditheadsview")]
        [HttpGet]
        public HttpResponseMessage Getcreditheadsview(string application_gid)
        {
            MdlappCreditassign objVisitor = new MdlappCreditassign();
            objAgrTrncreditapproval.DaGetcreditheadsview(objVisitor, application_gid);
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
            objAgrTrncreditapproval.DaGetappcreditapprovalinitiate(application_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Application Comment
        [ActionName("PostAppcreditqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcreditqueryadd(mdlcreditquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrncreditapproval.DaPostAppcreditqueryadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
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
            objAgrTrncreditapproval.DaGetAppcreditApprovalSummary(values, application_gid);
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
            objAgrTrncreditapproval.DaGetAppcreditquerySummary(values, application_gid);
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
            objAgrTrncreditapproval.DaGetApprmquerysSummary(values, application_gid);
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
            objAgrTrncreditapproval.DaGetGetAppcreditqueryesc(values, appcreditquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Head Approval 
        [ActionName("PostAppcreditHeadApproval")]
        [HttpPost]
        public HttpResponseMessage PostAppcreditHeadApproval(mdlappcreditapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrncreditapproval.DaPostAppcreditHeadApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // query status
        [ActionName("GetAppqueryStatus")]
        [HttpGet]
        public HttpResponseMessage GetAppqueryStatus(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlquerystatus values = new mdlquerystatus();
            objAgrTrncreditapproval.DaGetAppqueryStatus(values, application_gid, getsessionvalues.employee_gid);
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
            objAgrTrncreditapproval.DaGetUpdatequerStatus(values, appcreditquery_gid, application_gid, close_remarks, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditApplicationCount")]
        [HttpGet]
        public HttpResponseMessage CreditApplicationCount()
        {
            credtiApplicationCount values = new credtiApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrncreditapproval.DaCreditApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyApplicationCount")]
        [HttpGet]
        public HttpResponseMessage MyApplicationCount()
        {
            credtiApplicationCount values = new credtiApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrncreditapproval.DaMyApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrncreditapproval.GetAppcreditApprovallogSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetCCApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditApproval(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrncreditapproval.DaUpdateCreditApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditAutoApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditAutoApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetCreditAutoApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ShortClosingUpdate")]
        [HttpGet]
        public HttpResponseMessage ShortClosingUpdate(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objAgrTrncreditapproval.DaShortClosingUpdate(application_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Credit Upcoming Approval Application
        [ActionName("GetMyUpcomingApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyUpcomingApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrncreditapproval.DaGetMyUpcomingApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAppCreditManagerReject")]
        [HttpPost]
        public HttpResponseMessage PostAppCreditManagerReject(MdlappcreditManagerreject values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrncreditapproval.DaPostAppCreditManagerReject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppCreditManagerRejectsendback")]
        [HttpGet]
        public HttpResponseMessage GetAppCreditManagerRejectsendback(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlappcreditManagerreject values = new MdlappcreditManagerreject();
            objAgrTrncreditapproval.DaGetAppCreditManagerRejectsendback(values, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADGetAppqueryStatus")]
        [HttpGet]
        public HttpResponseMessage CADGetAppqueryStatus(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlquerystatus values = new mdlquerystatus();
            objAgrTrncreditapproval.DaCADGetAppqueryStatus(values, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppcreditRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppcreditRejectedSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Applcreditrejected values = new Applcreditrejected();
            objAgrTrncreditapproval.DaGetAppcreditRejectedSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
