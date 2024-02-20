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
    [RoutePrefix("api/LglTrnDNTrackerOthers")]
    [Authorize]
    public class LglTrnDNTrackerOthersController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLglTrnDNTrackerOthers objDaLglTrnDNTrackerOthers = new DaLglTrnDNTrackerOthers();

        [ActionName("getOthersPendingList")]
        [HttpGet]
        public HttpResponseMessage GetOthersPendingList()
        {
            MdlDNpendingList objDNpendingList = new MdlDNpendingList();
            objDaLglTrnDNTrackerOthers.DaGetOthersPendingList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getOthersGeneratedList")]
        [HttpGet]
        public HttpResponseMessage GetOthersGeneratedList()
        {
            MdlDNGeneratedList objDNpendingList = new MdlDNGeneratedList();
            objDaLglTrnDNTrackerOthers.DaGetOthersGeneratedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getOthersSkippedList")]
        [HttpGet]
        public HttpResponseMessage GetOthersSkippedList()
        {
            MdlDNSkippedList objDNpendingList = new MdlDNSkippedList();
            objDaLglTrnDNTrackerOthers.DaGetOthersSkippedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getOthersExclusionList")]
        [HttpGet]
        public HttpResponseMessage GetOthersExclusionList()
        {
            MdlDNexclusionList objDNpendingList = new MdlDNexclusionList();
            objDaLglTrnDNTrackerOthers.DaGetOthersExclusionList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getOthersLegalSRList")]
        [HttpGet]
        public HttpResponseMessage GetOthersLegalSRList()
        {
            MdlDNlegalsrList objlegalSR = new MdlDNlegalsrList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerOthers.DaGetOthersLegalSRList(objlegalSR, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("GetOthers_Count")]
        [HttpGet]
        public HttpResponseMessage GetOthers_Count()
        {
            count_dtl values = new count_dtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerOthers.DaGetOthers_Count(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
