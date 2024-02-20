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
    /// This Controllers will provide access for Revoking options and Hierarchy changes for the Samagro flow records at various stages (Application creation, Business Approval, View, Delete, Upload, Download and Approvals) in Admin Page.
    /// </summary>
    /// <remarks>Written by L.Sundar Rajan </remarks>

    [RoutePrefix("api/AgrAdminApplication")]
    [Authorize]
    public class AgrAdminApplicationController : ApiController
    {
        DaAgrAdminApplication objMstAdminApplication = new DaAgrAdminApplication();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetBusinessRejectedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessRejectedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRejectedAppl values = new MdlRejectedAppl();
            objMstAdminApplication.DaGetBusinessRejectedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessHoldApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHoldApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHoldAppl values = new MdlHoldAppl();
            objMstAdminApplication.DaGetBusinessHoldApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRejectRevokeApplication")]
        [HttpPost]
        public HttpResponseMessage PostRejectRevokeApplication(mdlrejectrevoke values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostRejectRevokeApplication(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessRevokedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessRevokedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRevokedAppl values = new MdlRevokedAppl();
            objMstAdminApplication.DaGetBusinessRevokedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessPendingHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetBusinessPendingHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRevokedAppl values = new MdlRevokedAppl();
            objMstAdminApplication.DaGetBusinessPendingHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRevokedAppl values = new MdlRevokedAppl();
            objMstAdminApplication.DaGetBusinessHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ApplicationRevokeCount")]
        [HttpGet]
        public HttpResponseMessage ApplicationRevokeCount()
        {
            RevokeApplicationCount values = new RevokeApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaApplicationRevokeCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHistoryRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetHistoryRemarksView(string businessrejectrevokelog_gid)
        {
            MdlRevokedAppl values = new MdlRevokedAppl();
            objMstAdminApplication.DaGetHistoryRemarksView(businessrejectrevokelog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingHistoryRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetPendingHistoryRemarksView(string applicationapproval_gid)
        {
            MdlRevokedAppl values = new MdlRevokedAppl();
            objMstAdminApplication.DaGetPendingHistoryRemarksView(applicationapproval_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditRejectedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditRejectedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditRejectedAppl values = new MdlCreditRejectedAppl();
            objMstAdminApplication.DaGetCreditRejectedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditHoldApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditHoldApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditHoldAppl values = new MdlCreditHoldAppl();
            objMstAdminApplication.DaGetCreditHoldApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreditApplicationRevokeCount")]
        [HttpGet]
        public HttpResponseMessage CreditApplicationRevokeCount()
        {
            CreditRevokeApplicationCount values = new CreditRevokeApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaCreditApplicationRevokeCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCreditRevokeApplication")]
        [HttpPost]
        public HttpResponseMessage PostCreditRevokeApplication(Mdlcreditrevoke values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostCreditRevokeApplication(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditRevokedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditRevokedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditRevokedAppl values = new MdlCreditRevokedAppl();
            objMstAdminApplication.DaGetCreditRevokedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditPendingHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetCreditPendingHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditPendingHistoryAppl values = new MdlCreditPendingHistoryAppl();
            objMstAdminApplication.DaGetCreditPendingHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetCreditHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditHistoryLog values = new MdlCreditHistoryLog();
            objMstAdminApplication.DaGetCreditHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditPendingHistoryRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetCreditPendingHistoryRemarksView(string appcreditapproval_gid)
        {
            MdlCreditPendingHistoryAppl values = new MdlCreditPendingHistoryAppl();
            objMstAdminApplication.DaGetCreditPendingHistoryRemarksView(appcreditapproval_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditHistoryRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetCreditHistoryRemarksView(string creditrevokelog_gid)
        {
            MdlCreditHistoryApplLog values = new MdlCreditHistoryApplLog();
            objMstAdminApplication.DaGetCreditHistoryRemarksView(creditrevokelog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetBusinessStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetCreditStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCcStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetCcStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetCcStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadPendingStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetCadPendingStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetCadPendingStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCadAcceptedStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetCadAcceptedStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetCadAcceptedStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HierarchyUpdateApplCount")]
        [HttpGet]
        public HttpResponseMessage HierarchyUpdateApplCount()
        {
            MdlHierarchyUpdateApplCount values = new MdlHierarchyUpdateApplCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaHierarchyUpdateApplCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getapplicationdetails")]
        [HttpGet]
        public HttpResponseMessage Getapplicationdetails(string application_gid)
        {
            Mdlapplicationdetials values = new Mdlapplicationdetials();
            objMstAdminApplication.DaGetapplicationdetails(values, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAllHierarchyverticalListSearch")]
        [HttpPost]
        public HttpResponseMessage PostAllHierarchyverticalListSearch(MdlAdminRMMappingview values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostAllHierarchyverticalListSearch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBusinessHierarchyUpdate")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHierarchyUpdate(MdlBusinessHierarchyUpdateDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostBusinessHierarchyUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHierarchyUpdateHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetHierarchyUpdateHistoryLog(string application_gid)
        {
            MdlHierarchyUpdateHistoryLog objreassignedlogInfo = new MdlHierarchyUpdateHistoryLog();
            objMstAdminApplication.DaGetHierarchyUpdateHistoryLog(application_gid, objreassignedlogInfo);
            return Request.CreateResponse(HttpStatusCode.OK, objreassignedlogInfo);
        }
        [ActionName("GetIncompleteStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetIncompleteStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetIncompleteStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHierarchyUpdateRemarks")]
        [HttpGet]
        public HttpResponseMessage GetHierarchyUpdateRemarks(string businesshierarchyupdatelog_gid)
        {
            MdlHierarchyUpdateRemarks values = new MdlHierarchyUpdateRemarks();
            objMstAdminApplication.DaGetHierarchyUpdateRemarks(values, businesshierarchyupdatelog_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductRejectedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductRejectedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditRejectedAppl values = new MdlCreditRejectedAppl();
            objMstAdminApplication.DaGetProductRejectedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductHoldApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductHoldApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditRejectedAppl values = new MdlCreditRejectedAppl();
            objMstAdminApplication.DaGetProductHoldApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductStageSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductStageSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBusinessHierarchyUpdate values = new MdlBusinessHierarchyUpdate();
            objMstAdminApplication.DaGetProductStageSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductRevokedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductRevokedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditRevokedAppl values = new MdlCreditRevokedAppl();
            objMstAdminApplication.DaGetProductRevokedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductRevokeApplication")]
        [HttpPost]
        public HttpResponseMessage PostProductRevokeApplication(Mdlcreditrevoke values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostProjectRevokeApplication(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductPendingHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetProductPendingHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditPendingHistoryAppl values = new MdlCreditPendingHistoryAppl();
            objMstAdminApplication.DaGetProductPendingHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetProductHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditHistoryLog values = new MdlCreditHistoryLog();
            objMstAdminApplication.DaGetProductHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductPendingHistory")]
        [HttpGet]
        public HttpResponseMessage GetProductPendingHistory(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProductPendingHistoryAppl values = new MdlProductPendingHistoryAppl();
            objMstAdminApplication.DaGetProductPendingHistory(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ProductApplicationRevokeCount")]
        [HttpGet]
        public HttpResponseMessage ProductApplicationRevokeCount()
        {
            CreditRevokeApplicationCount values = new CreditRevokeApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaProductApplicationRevokeCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPriductHistoryRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetProductHistoryRemarksView(string productrevokelog_gid)
        {
            MdlCreditHistoryApplLog values = new MdlCreditHistoryApplLog();
            objMstAdminApplication.DaGetProductHistoryRemarksView(productrevokelog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductPendingHistoryRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetProductPendingHistoryRemarksView(string appproductapproval_gid)
        {
            MdlCreditPendingHistoryAppl values = new MdlCreditPendingHistoryAppl();
            objMstAdminApplication.DaGetProductPendingHistoryRemarksView(appproductapproval_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostCreditManagerRevokeApplication")]
        [HttpPost]
        public HttpResponseMessage PostCreditManagerRevokeApplication(Mdlcreditrevoke values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostCreditManagerRevokeApplication(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCreditManagerRejectedApplSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditManagerRejectedApplSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditRejectedAppl values = new MdlCreditRejectedAppl();
            objMstAdminApplication.DaGetCreditManagerRejectedApplSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}