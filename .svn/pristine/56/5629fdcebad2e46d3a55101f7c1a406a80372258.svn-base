using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

/// <summary>
/// (It's used for Admin)Admin Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Logapriya </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstAdminApplication")]
    [Authorize]
    public class MstAdminApplicationController : ApiController
    {
        DaMstAdminApplication objMstAdminApplication = new DaMstAdminApplication();
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
        [ActionName("PostCreditManagerRevokeApplication")]
        [HttpPost]
        public HttpResponseMessage PostCreditManagerRevokeApplication(Mdlcreditrevoke values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstAdminApplication.DaPostCreditManagerRevokeApplication(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditManagerHistoryLog")]
        [HttpGet]
        public HttpResponseMessage GetCreditManagerHistoryLog(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditHistoryLog values = new MdlCreditHistoryLog();
            objMstAdminApplication.DaGetCreditManagerHistoryLog(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditManagerRejectedRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetCreditManagerRejectedRemarksView(string creditmanagerrejectrevokereasonlog_gid)
        {
            MdlCreditHistoryApplLog values = new MdlCreditHistoryApplLog();
            objMstAdminApplication.DaGetCreditManagerRejectedRemarksView(creditmanagerrejectrevokereasonlog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditManagerRejectedRevokeRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetCreditManagerRejectedRevokeRemarksView(string creditmanagerrevokelog_gid)
        {
            MdlCreditHistoryApplLog values = new MdlCreditHistoryApplLog();
            objMstAdminApplication.DaGetCreditManagerRejectedRevokeRemarksView(creditmanagerrevokelog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}