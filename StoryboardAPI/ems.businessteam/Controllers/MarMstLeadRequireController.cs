using ems.businessteam.DataAccess;
using ems.businessteam.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.businessteam.Controllers
{
    [RoutePrefix("api/MarMstLeadRequire")]
    [Authorize]
    public class MarMstLeadRequireController : ApiController
    {
        DaMarMstLeadRequire objDaMarMstLeadRequire = new DaMarMstLeadRequire();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetLeadRequire")]
        [HttpGet]
        public HttpResponseMessage GetLeadRequire()
        {
            MdlMarMstLeadRequire values = new MdlMarMstLeadRequire();
            objDaMarMstLeadRequire.DaGetLeadRequire(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateLeadRequire")]
        [HttpPost]
        public HttpResponseMessage CreateLeadRequire(MdlMarMstLeadRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstLeadRequire.DaCreateLeadRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLeadRequire")]
        [HttpGet]
        public HttpResponseMessage EditLeadRequire(string leadrequire_gid)
        {
            MdlMarMstLeadRequire values = new MdlMarMstLeadRequire();
            objDaMarMstLeadRequire.DaEditLeadRequire(leadrequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLeadRequire")]
        [HttpPost]
        public HttpResponseMessage UpdateLeadRequire(MdlMarMstLeadRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstLeadRequire.DaUpdateLeadRequire(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLeadRequire")]
        [HttpPost]
        public HttpResponseMessage InactiveLeadRequire(MdlMarMstLeadRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstLeadRequire.DaInactiveLeadRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLeadRequire")]
        [HttpGet]
        public HttpResponseMessage DeleteLeadRequire(string leadrequire_gid)
        {
            MdlMarMstLeadRequire values = new MdlMarMstLeadRequire();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstLeadRequire.DaDeleteLeadRequire(leadrequire_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LeadRequireInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage LeadRequireInactiveLogview(string leadrequire_gid)
        {
            MdlMarMstLeadRequire values = new MdlMarMstLeadRequire();
            objDaMarMstLeadRequire.DaLeadRequireInactiveLogview(leadrequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLeadRequireActive")]
        [HttpGet]
        public HttpResponseMessage GetLeadRequireActive()
        {
            MdlMarMstLeadRequire values = new MdlMarMstLeadRequire();
            objDaMarMstLeadRequire.DaGetLeadRequireActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}