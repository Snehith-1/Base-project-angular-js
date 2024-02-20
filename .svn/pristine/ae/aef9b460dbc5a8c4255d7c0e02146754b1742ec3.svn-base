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
    /// <remarks>Written by Logapriya </remarks>

    [RoutePrefix("api/AgrTrnSuprCreditApproval")]
    [Authorize]
    public class AgrTrnSuprCreditApprovalController : ApiController
    {

        DaAgrTrnSuprCreditApproval objAgrTrnSuprcreditapproval = new DaAgrTrnSuprCreditApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetMyAppAssignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppAssignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppAssignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppSubmittedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppSubmittedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppSubmittedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppSubmittedtoccSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppSubmittedtoccSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppSubmittedtoccSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppcreditSubmittedtoccSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppcreditSubmittedtoccSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppcreditSubmittedtoccSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyAppcreditRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppcreditRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetMyAppcreditRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcreditheadsview")]
        [HttpGet]
        public HttpResponseMessage Getcreditheadsview(string application_gid)
        {
            MdlappCreditassign objVisitor = new MdlappCreditassign();
            objAgrTrnSuprcreditapproval.DaGetcreditheadsview(objVisitor, application_gid);
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
            objAgrTrnSuprcreditapproval.DaGetappcreditapprovalinitiate(application_gid, getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Application Comment
        [ActionName("PostAppcreditqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcreditqueryadd(mdlcreditquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnSuprcreditapproval.DaPostAppcreditqueryadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
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
            objAgrTrnSuprcreditapproval.DaGetAppcreditApprovalSummary(values, application_gid);
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
            objAgrTrnSuprcreditapproval.DaGetAppcreditquerySummary(values, application_gid);
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
            objAgrTrnSuprcreditapproval.DaGetApprmquerysSummary(values, application_gid);
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
            objAgrTrnSuprcreditapproval.DaGetGetAppcreditqueryesc(values, appcreditquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Head Approval 
        [ActionName("PostAppcreditHeadApproval")]
        [HttpPost]
        public HttpResponseMessage PostAppcreditHeadApproval(mdlappcreditapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnSuprcreditapproval.DaPostAppcreditHeadApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
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
            objAgrTrnSuprcreditapproval.DaGetAppqueryStatus(values, application_gid, getsessionvalues.employee_gid);
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
            objAgrTrnSuprcreditapproval.DaGetUpdatequerStatus(values, appcreditquery_gid, application_gid, close_remarks, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditApplicationCount")]
        [HttpGet]
        public HttpResponseMessage CreditApplicationCount()
        {
            credtiApplicationCount values = new credtiApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnSuprcreditapproval.DaCreditApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyApplicationCount")]
        [HttpGet]
        public HttpResponseMessage MyApplicationCount()
        {
            credtiApplicationCount values = new credtiApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnSuprcreditapproval.DaMyApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
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
            objAgrTrnSuprcreditapproval.GetAppcreditApprovallogSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditApproval values = new MdlMstCreditApproval();
            objAgrTrnSuprcreditapproval.DaGetCCApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
