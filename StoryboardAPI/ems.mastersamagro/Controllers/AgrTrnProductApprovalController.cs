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
    /// This Controllers will provide access to product desk flow by posting and getting necessary data for the applications
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A & Logapriya.S </remarks>

    [RoutePrefix("api/AgrTrnProductApproval")]
    [Authorize]

    public class AgrTrnProductApprovalController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrTrnProductApproval objDaAgrTrnProductApproval = new DaAgrTrnProductApproval();

        [ActionName("GetAppProductPendingAssignmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppProductPendingAssignmentSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnProductApproval.DaGetAppProductPendingAssignmentSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssignProductApplicationCount")]
        [HttpGet]
        public HttpResponseMessage AssignProductApplicationCount()
        {
            AssignApplicationCount values = new AssignApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnProductApproval.DaAssignProductApplicationCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         
        [ActionName("GetProductApprovalManagerMember")]
        [HttpGet]
        public HttpResponseMessage GetProductApprovalManagerMember(string productdesk_gid)
        {
            MdlProductGroup objmaster = new MdlProductGroup();
            objDaAgrTrnProductApproval.DaGetProductApprovalManagerMember(productdesk_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostProductAssign")]
        [HttpPost]
        public HttpResponseMessage PostProductAssign(Mdlproductdeskassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnProductApproval.DaPostProductAssign(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppProductAssignedAssignmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppProductAssignedAssignmentSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objDaAgrTrnProductApproval.DaGetAppProductAssignedAssignmentSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppProductAprovalinfo")]
        [HttpGet]
        public HttpResponseMessage GetAppProductAprovalinfo(string application_gid)
        {
            Mdlproductdeskassign objmaster = new Mdlproductdeskassign();
            objDaAgrTrnProductApproval.DaGetAppProductAprovalinfo(application_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetProductReassignUpdate")]
        [HttpPost]
        public HttpResponseMessage GetProductReassignUpdate(Mdlproductdeskassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnProductApproval.DaGetProductReassignUpdate(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductReassignedLog")]
        [HttpGet]
        public HttpResponseMessage GetProductReassignedLog(string application_gid)
        {
            MdlProductreassignedlogInfo objreassignedlogInfo = new MdlProductreassignedlogInfo();
            objDaAgrTrnProductApproval.DaGetProductReassignedLog(application_gid, objreassignedlogInfo);
            return Request.CreateResponse(HttpStatusCode.OK, objreassignedlogInfo);
        }

        // My assignment Page

        [ActionName("GetMyAppProductAssignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppProductAssignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetMyAppProductAssignedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyApplicationProductCount")]
        [HttpGet]
        public HttpResponseMessage MyApplicationProductCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductApplicationCount values = new MdlProductApplicationCount();
            objDaAgrTrnProductApproval.DaMyApplicationProductCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Application Initiate
        [ActionName("Getappproductmemberapproval")]
        [HttpGet]
        public HttpResponseMessage Getappproductmemberapproval(string appproductapproval_gid,string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrTrnProductApproval.DaGetappproductmemberapproval(appproductapproval_gid,application_gid, getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppProductSubmittedSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppProductSubmittedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetMyAppProductSubmittedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyAppProductRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppProductRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetMyAppProductRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        /// Product Approval page 
        /// 
        [ActionName("GetMyAppProductApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyAppProductApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetMyAppProductApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ProductApplicationCount")]
        [HttpGet]
        public HttpResponseMessage ProductApplicationCount()
        {
            MdlProductApplicationCount values = new MdlProductApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnProductApproval.DaProductApplicationCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetProductApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductRejectHoldSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductRejectHoldSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetProductRejectHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Product Desc Query
        [ActionName("PostAppProductqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppProductqueryadd(mdlproductquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnProductApproval.DaPostAppProductqueryadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //rm Query Summary
        [ActionName("GetApprmquerysSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprmquerysSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applproductapproval values = new applproductapproval();
            objDaAgrTrnProductApproval.DaGetApprmquerysSummary(getsessionvalues.employee_gid, values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 
        [ActionName("PostAppProductManagerApproval")]
        [HttpPost]
        public HttpResponseMessage PostAppProductManagerApproval(MdlProductApprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaAgrTrnProductApproval.DaPostAppProductManagerApproval(values, getsessionvalues.employee_gid,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 

        // View query Description
        [ActionName("GetAppcreditqueryesc")]
        [HttpGet]
        public HttpResponseMessage GetAppcreditqueryesc(string appproductquery_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlproductquery values = new mdlproductquery();
            objDaAgrTrnProductApproval.DaGetGetAppcreditqueryesc(values, appproductquery_gid);
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
            objDaAgrTrnProductApproval.DaGetAppqueryStatus(values, application_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //rm Query Summary
        [ActionName("GetAppProductrmquerysSummary")]
        [HttpGet]
        public HttpResponseMessage GetAppProductrmquerysSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applproductapproval values = new applproductapproval();
            objDaAgrTrnProductApproval.DaGetAppProductrmquerysSummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update query status       
        [ActionName("GetProductUpdatequeryStatus")]
        [HttpGet]
        public HttpResponseMessage GetProductUpdatequeryStatus(string appproductquery_gid, string application_gid, string close_remarks)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlquerystatus values = new mdlquerystatus();
            objDaAgrTrnProductApproval.DaGetProductUpdatequeryStatus(values, appproductquery_gid, application_gid, close_remarks, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Raise Manager Credit Query
        [ActionName("PostManagerqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostManagerqueryadd(mdlproductquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnProductApproval.DaPostManagerqueryadd(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Manager Query Summary
        [ActionName("GetManagerquerySummary")]
        [HttpGet]
        public HttpResponseMessage GetManagerquerySummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            applproductapproval values = new applproductapproval();
            objDaAgrTrnProductApproval.DaGetManagerquerySummary(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMemberMangerApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetMemberMangerApprovalDtls(string application_gid )
        {
            MdlProductApprovaldtl values = new MdlProductApprovaldtl();
            objDaAgrTrnProductApproval.DaGetMemberMangerApprovalDtls(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        public HttpResponseMessage GetAutoApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductApproval values = new MdlMstProductApproval();
            objDaAgrTrnProductApproval.DaGetAutoApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}