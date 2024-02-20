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
    [RoutePrefix("api/LglTrnDNTrackerFPO")]
    [Authorize]
    public class LglTrnDnTrackerFPOController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLglTrnDNTrackerFPO objDaLglTrnDNTrackerFPO = new DaLglTrnDNTrackerFPO();

        [ActionName("getFPOPendingList")]
        [HttpGet]
        public HttpResponseMessage GetFPOPendingList()
        {
            MdlDNpendingList objDNpendingList = new MdlDNpendingList();
            objDaLglTrnDNTrackerFPO.DaGetFPOPendingList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getFPOGeneratedList")]
        [HttpGet]
        public HttpResponseMessage GetFPOGeneratedList()
        {
            MdlDNGeneratedList objDNpendingList = new MdlDNGeneratedList();
            objDaLglTrnDNTrackerFPO.DaGetFPOGeneratedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getFPOSkippedList")]
        [HttpGet]
        public HttpResponseMessage GetFPOSkippedList()
        {
            MdlDNSkippedList objDNpendingList = new MdlDNSkippedList();
            objDaLglTrnDNTrackerFPO.DaGetFPOSkippedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getFPOExclusionList")]
        [HttpGet]
        public HttpResponseMessage GetFPOExclusionList()
        {
            MdlDNexclusionList objDNpendingList = new MdlDNexclusionList();
            objDaLglTrnDNTrackerFPO.DaGetFPOExclusionList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getFPOLegalSRList")]
        [HttpGet]
        public HttpResponseMessage GetFPOLegalSRList()
        {
            MdlDNlegalsrList objlegalSR = new MdlDNlegalsrList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerFPO.DaGetFPOLegalSRList(objlegalSR, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("GetFPO_Count")]
        [HttpGet]
        public HttpResponseMessage GetFPO_Count()
        {
            count_dtl values = new count_dtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerFPO.DaGetFPO_Count(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
