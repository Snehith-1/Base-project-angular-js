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
    [RoutePrefix("api/MarMstMilletRequire")]
    [Authorize]
    public class MarMstMilletRequireController : ApiController
    {
        DaMarMstMilletRequire objDaMarMstMilletRequire = new DaMarMstMilletRequire();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetMilletRequire")]
        [HttpGet]
        public HttpResponseMessage GetMilletRequire()
        {
            MdlMarMstmilletRequire values = new MdlMarMstmilletRequire();
            objDaMarMstMilletRequire.DaGetMilletRequire(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateMilletRequire")]
        [HttpPost]
        public HttpResponseMessage CreateMilletRequire(MdlMarMstmilletRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstMilletRequire.DaCreateMilletRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMilletRequire")]
        [HttpGet]
        public HttpResponseMessage EditMilletRequire(string milletrequire_gid)
        {
            MdlMarMstmilletRequire values = new MdlMarMstmilletRequire();
            objDaMarMstMilletRequire.DaEditMilletRequire(milletrequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMilletRequire")]
        [HttpPost]
        public HttpResponseMessage UpdateMilletRequire(MdlMarMstmilletRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstMilletRequire.DaUpdateMilletRequire(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMilletRequire")]
        [HttpPost] 
        public HttpResponseMessage InactiveMilletRequire(MdlMarMstmilletRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstMilletRequire.DaInactiveMilletRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteMilletRequire")]
        [HttpGet]
        public HttpResponseMessage DeleteMilletRequire(string milletrequire_gid)
        {
            MdlMarMstmilletRequire values = new MdlMarMstmilletRequire();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMarMstMilletRequire.DaDeleteMilletRequire(milletrequire_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MilletRequireInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage MilletRequireInactiveLogview(string milletrequire_gid)
        {
            MdlMarMstmilletRequire values = new MdlMarMstmilletRequire();
            objDaMarMstMilletRequire.DaMilletRequireInactiveLogview(milletrequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMilletRequireActive")]
        [HttpGet]
        public HttpResponseMessage GetMilletRequireActive()
        {
            MdlMarMstmilletRequire values = new MdlMarMstmilletRequire();
            objDaMarMstMilletRequire.DaGetMilletRequireActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}