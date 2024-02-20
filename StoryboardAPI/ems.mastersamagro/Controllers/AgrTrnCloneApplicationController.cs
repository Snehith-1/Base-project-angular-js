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
    /// This Controllers will help to clone records from buyer onboard for renewal, amendment & short closing
    /// </summary>
    /// <remarks>Written by Logapriya.S, Abilash.A </remarks>

    [RoutePrefix("api/AgrTrnCloneApplication")]
    [Authorize]
    public class AgrTrnCloneApplicationController : ApiController
    {
        DaAgrTrnCloneApplication objAgrTrnCloneApplication = new DaAgrTrnCloneApplication();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        // Application Renewal
        [ActionName("PostRenewalAdd")]
        [HttpPost]
        public HttpResponseMessage PostRenewalAdd(MdlMstCloneRenewalAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnCloneApplication.DaPostRenewalAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardLimitManagementdtl")]
        [HttpGet]
        public HttpResponseMessage GetOnboardLimitManagementdtl(string onboard_gid)
        {
            MdlRenewalOnboardLimitManagement values = new MdlRenewalOnboardLimitManagement();
            objAgrTrnCloneApplication.DaGetOnboardLimitManagementdtl(onboard_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardLimitFacilitydtl")]
        [HttpGet]
        public HttpResponseMessage GetOnboardLimitFacilitydtl(string application_gid)
        {
            MdlRenewalFaclilityList values = new MdlRenewalFaclilityList();
            objAgrTrnCloneApplication.DaGetOnboardLimitFacilitydtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApplCloneHistoryDtlLog")]
        [HttpGet]
        public HttpResponseMessage GetApplCloneHistoryDtlLog(string onboard_gid)
        {
            MdlApplCloneHistoryDtlLog values = new MdlApplCloneHistoryDtlLog();
            objAgrTrnCloneApplication.DaGetApplCloneHistoryDtlLog(onboard_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Amentment Master List
        [ActionName("GetAmentmentMasterList")]
        [HttpGet]
        public HttpResponseMessage GetCityList()
        {
            MdlAmentmentMasterList objMdlAmentmentMasterList = new MdlAmentmentMasterList();
            objAgrTrnCloneApplication.DaGetAmentmentMasterList(objMdlAmentmentMasterList);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAmentmentMasterList);
        }
        // Application Amendment
        [ActionName("PostAmendmentAdd")]
        [HttpPost]
        public HttpResponseMessage PostAmendmentAdd(MdlMstCloneAmendmentAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnCloneApplication.DaPostAmendmentAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetHistoryLogRemarksView")]
        [HttpGet]
        public HttpResponseMessage GetHistoryLogRemarksView(string application_gid)
        {
            MdlApplCloneHistoryDtlLog values = new MdlApplCloneHistoryDtlLog();
            objAgrTrnCloneApplication.DaGetHistoryLogRemarksView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Application Short Closing
        [ActionName("PostShortClosingAdd")]
        [HttpPost]
        public HttpResponseMessage PostShortClosingAdd(MdlMstCloneShortClosingAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrTrnCloneApplication.DaPostShortClosingAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}

