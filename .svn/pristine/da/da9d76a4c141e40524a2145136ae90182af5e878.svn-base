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
    [RoutePrefix("api/LglTrnDNTrackerCBO")]
    [Authorize]
    public class LglTrnDNTrackerCBOController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLglTrnDNTrackerCBO objDaLglTrnDNTrackerCBO = new DaLglTrnDNTrackerCBO();

        [ActionName("getCBOPendingList")]
        [HttpGet]
        public HttpResponseMessage GetCBOPendingList()
        {
            MdlDNpendingList objDNpendingList = new MdlDNpendingList();
            objDaLglTrnDNTrackerCBO.DaGetCBOPendingList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getCBOGeneratedList")]
        [HttpGet]
        public HttpResponseMessage GetCBOGeneratedList()
        {
            MdlDNGeneratedList objDNpendingList = new MdlDNGeneratedList();
            objDaLglTrnDNTrackerCBO.DaGetCBOGeneratedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getCBOSkippedList")]
        [HttpGet]
        public HttpResponseMessage GetOthersSkippedList()
        {
            MdlDNSkippedList objDNpendingList = new MdlDNSkippedList();
            objDaLglTrnDNTrackerCBO.DaGetCBOSkippedList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getCBOExclusionList")]
        [HttpGet]
        public HttpResponseMessage GetCBOExclusionList()
        {
            MdlDNexclusionList objDNpendingList = new MdlDNexclusionList();
            objDaLglTrnDNTrackerCBO.DaGetCBOExclusionList(getsessionvalues.employee_gid, objDNpendingList);
            return Request.CreateResponse(HttpStatusCode.OK, objDNpendingList);
        }
        [ActionName("getCBOLegalSRList")]
        [HttpGet]
        public HttpResponseMessage GetCBOLegalSRList()
        {
            MdlDNlegalsrList objlegalSR = new MdlDNlegalsrList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerCBO.DaGetCBOLegalSRList(objlegalSR, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("GetCBO_Count")]
        [HttpGet]
        public HttpResponseMessage GetCBO_Count()
        {
            count_dtl values = new count_dtl();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLglTrnDNTrackerCBO.DaGetCBO_Count(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
