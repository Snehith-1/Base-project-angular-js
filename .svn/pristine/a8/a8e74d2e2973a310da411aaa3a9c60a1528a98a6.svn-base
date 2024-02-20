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
    /// This Controllers will provide access to fetch data from credit mapping master in custopedia.
    /// </summary>
    /// <remarks>Written by Abilash.A</remarks>

    [RoutePrefix("api/AgrMstSuprCreditMapping")]
    [Authorize]

    public class AgrMstSuprCreditMappingController : ApiController
    {
        DaAgrMstSuprCreditMapping objDaMstCreditMapping = new DaAgrMstSuprCreditMapping();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Credit Group Add
        [ActionName("PostCreditGroupAdd")]
        [HttpPost]
        public HttpResponseMessage PostCreditGroupAdd(MdlCreditGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditgroupaddAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Group Summary
        [ActionName("GetCreditGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditGroupSummary()
        {
            MdlCreditGroup objmaster = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditGroupSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetCreditGroupEdit")]
        [HttpGet]
        public HttpResponseMessage GetCreditGroupEdit(string creditmapping_gid)
        {
            MdlCreditGroup objmaster = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditGroupEdit(creditmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostCreditGroupUpdate")]
        [HttpPost]
        public HttpResponseMessage PostCreditGroupUpdate(MdlCreditGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditGroupUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCredit2Heads")]
        [HttpGet]
        public HttpResponseMessage GetCredit2Heads(string creditmapping_gid)
        {
            MdlCreditGroup objmaster = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCredit2Heads(creditmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetCreditgroupHeads")]
        [HttpGet]
        public HttpResponseMessage GetCreditgroupHeads(string creditmapping_gid)
        {
            creditheads values = new creditheads();
            objDaMstCreditMapping.DaGetCreditgroupHeads(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditgroupInactive")]
        [HttpPost]
        public HttpResponseMessage PostCreditgroupInactive(CreditGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditgroupInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditgroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetCreditgroupInactiveLogview(string creditmapping_gid)
        {
            MdlCreditGroup values = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditgroupInactiveLogview(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditassignUpdate")]
        [HttpPost]
        public HttpResponseMessage PostCreditassignUpdate(MdlCreditheadassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditassignUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditReassignUpdate")]
        [HttpPost]
        public HttpResponseMessage GetCreditReassignUpdate(MdlCreditheadassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaGetCreditReassignUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditgroupname")]
        [HttpGet]
        public HttpResponseMessage GetCreditgroupname(string creditmapping_gid)
        {
            MdlCreditGroup values = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditgroupname(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetReassignedLog")]
        [HttpGet]
        public HttpResponseMessage GetReassignedLog(string application_gid)
        {
            MdlreassignedlogInfo objreassignedlogInfo = new MdlreassignedlogInfo();
            objDaMstCreditMapping.DaGetReassignedLog(application_gid, objreassignedlogInfo);
            return Request.CreateResponse(HttpStatusCode.OK, objreassignedlogInfo);
        }
    }
}