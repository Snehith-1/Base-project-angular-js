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
    [RoutePrefix("api/MstStartupRequire")]
    [Authorize]
    public class MstStartupRequireController : ApiController
    {
        DaMstStartupRequire objDaMstStartupRequire = new DaMstStartupRequire();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetStartupRequire")]
        [HttpGet]
        public HttpResponseMessage GetStartupRequire()
        {
            MdlMstStartupRequire values = new MdlMstStartupRequire();
            objDaMstStartupRequire.DaGetStartupRequire(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateStartupRequire")]
        [HttpPost]
        public HttpResponseMessage CreateStartupRequire(MdlMstStartupRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstStartupRequire.DaCreateStartupRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditStartupRequire")]
        [HttpGet]
        public HttpResponseMessage EditStartupRequire(string startuprequire_gid)
        {
            MdlMstStartupRequire values = new MdlMstStartupRequire();
            objDaMstStartupRequire.DaEditStartupRequire(startuprequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateStartupRequire")]
        [HttpPost]
        public HttpResponseMessage UpdateStartupRequire(MdlMstStartupRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstStartupRequire.DaUpdateStartupRequire(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveStartupRequire")]
        [HttpPost]
        public HttpResponseMessage InactiveStartupRequire(MdlMstStartupRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstStartupRequire.DaInactiveStartupRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteStartupRequire")]
        [HttpGet]
        public HttpResponseMessage DeleteStartupRequire(string startuprequire_gid)
        {
            MdlMstStartupRequire values = new MdlMstStartupRequire();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstStartupRequire.DaDeleteStartupRequire(startuprequire_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("StartupRequireInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage StartupRequireInactiveLogview(string startuprequire_gid)
        {
            MdlMstStartupRequire values = new MdlMstStartupRequire();
            objDaMstStartupRequire.DaStartupRequireInactiveLogview(startuprequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetStartupRequireActive")]
        [HttpGet]
        public HttpResponseMessage GetStartupRequireActive()
        {
            MdlMstStartupRequire values = new MdlMstStartupRequire();
            objDaMstStartupRequire.DaGetStartupRequireActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}