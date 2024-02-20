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
    [RoutePrefix("api/LglTrnDNTrackerRetail")]
    [Authorize]
    public class LglTrnDNTrackerRetailController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLglTrnDNTrackerRetail objDaLglTrnDNTrackerRetail = new DaLglTrnDNTrackerRetail();

        [ActionName("getRetailPendingList")]
        [HttpGet]
        public HttpResponseMessage GetRetailPendingList()
        {
            MdlDNpendingList objDNpendingList = new MdlDNpendingList();
            objDaLglTrnDNTrackerRetail.DaGetRetailPendingList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getRetailGeneratedList")]
        [HttpGet]
        public HttpResponseMessage GetRetailGeneratedList()
        {
            MdlDNGeneratedList objDNpendingList = new MdlDNGeneratedList();
            objDaLglTrnDNTrackerRetail.DaGetRetailGeneratedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getRetailSkippedList")]
        [HttpGet]
        public HttpResponseMessage GetRetailSkippedList()
        {
            MdlDNSkippedList objDNpendingList = new MdlDNSkippedList();
            objDaLglTrnDNTrackerRetail.DaGetRetailSkippedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getRetailExclusionList")]
        [HttpGet]
        public HttpResponseMessage GetRetailExclusionList()
        {
            MdlDNexclusionList objDNpendingList = new MdlDNexclusionList();
            objDaLglTrnDNTrackerRetail.DaGetRetailExclusionList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getRetailLegalSRList")]
        [HttpGet]
        public HttpResponseMessage GetRetailLegalSRList()
        {
            MdlDNlegalsrList objlegalSR = new MdlDNlegalsrList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerRetail.DaGetRetailLegalSRList(objlegalSR, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("GetRetail_Count")]
        [HttpGet]
        public HttpResponseMessage GetRetail_Count()
        {
            count_dtl values = new count_dtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerRetail.DaGetRetail_Count(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
