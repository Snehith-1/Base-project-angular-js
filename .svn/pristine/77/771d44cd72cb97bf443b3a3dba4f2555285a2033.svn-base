using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.lgl.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using ems.lgl.DataAccess;

namespace ems.lgl.Controllers
{
    [RoutePrefix("api/LglTrnDNTrackerAE")]
    [Authorize]
    public class LglTrnDNTrackerAEController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLglTrnDNTrackerAE objDaLglTrnDNTrackerAE = new DaLglTrnDNTrackerAE();

        [ActionName("getAEPendingList")]
        [HttpGet]
        public HttpResponseMessage GetAEPendingList()
        {
            MdlDNpendingList objDNpendingList = new MdlDNpendingList();
            objDaLglTrnDNTrackerAE.DaGetAEPendingList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getAEGeneratedList")]
        [HttpGet]
        public HttpResponseMessage GetAEGeneratedList()
        {
            MdlDNGeneratedList objDNpendingList = new MdlDNGeneratedList();
            objDaLglTrnDNTrackerAE.DaGetAEGeneratedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getAESkippedList")]
        [HttpGet]
        public HttpResponseMessage GetAESkippedList()
        {
            MdlDNSkippedList objDNpendingList = new MdlDNSkippedList();
            objDaLglTrnDNTrackerAE.DaGetAESkippedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getAEExclusionList")]
        [HttpGet]
        public HttpResponseMessage GetAEExclusionList()
        {
            MdlDNexclusionList objDNpendingList = new MdlDNexclusionList();
            objDaLglTrnDNTrackerAE.DaGetAEExclusionList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getAELegalSRList")]
        [HttpGet]
        public HttpResponseMessage GetAELegalSRList()
        {
            MdlDNlegalsrList objlegalSR = new MdlDNlegalsrList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerAE.DaGetAELegalSRList(objlegalSR, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("Getcustomerupdatedetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerupdatedetails(string urn)
        {
            customereditlist values = new customereditlist();
            objDaLglTrnDNTrackerAE.DaGetCustomer(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocationHistory")]
        [HttpGet]
        public HttpResponseMessage GetAllocationHistory(string urn)
        {
            overallhistoryallocationlist values = new overallhistoryallocationlist();
            objDaLglTrnDNTrackerAE.DaGetAllocationHistory(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAE_Count")]
        [HttpGet]
        public HttpResponseMessage GetAE_Count()
        {
            count_dtl values = new count_dtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerAE.DaGetAE_Count(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
