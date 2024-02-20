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
    [RoutePrefix("api/MstEnquiryRequire")]
    [Authorize]
    public class MstEnquiryRequireController : ApiController
    {
        DaMstEnquiryRequire objDaMstEnquiryRequire = new DaMstEnquiryRequire();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetEnquiryRequire")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryRequire()
        {
            MdlMstEnquiryRequire values = new MdlMstEnquiryRequire();
            objDaMstEnquiryRequire.DaGetEnquiryRequire(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateEnquiryRequire")]
        [HttpPost]
        public HttpResponseMessage CreateEnquiryRequire(MdlMstEnquiryRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstEnquiryRequire.DaCreateEnquiryRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditEnquiryRequire")]
        [HttpGet]
        public HttpResponseMessage EditEnquiryRequire(string enquiryrequire_gid)
        {
            MdlMstEnquiryRequire values = new MdlMstEnquiryRequire();
            objDaMstEnquiryRequire.DaEditEnquiryRequire(enquiryrequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateEnquiryRequire")]
        [HttpPost]
        public HttpResponseMessage UpdateEnquiryRequire(MdlMstEnquiryRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstEnquiryRequire.DaUpdateEnquiryRequire(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveEnquiryRequire")]
        [HttpPost]
        public HttpResponseMessage InactiveEnquiryRequire(MdlMstEnquiryRequire values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstEnquiryRequire.DaInactiveEnquiryRequire(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEnquiryRequire")]
        [HttpGet]
        public HttpResponseMessage DeleteEnquiryRequire(string enquiryrequire_gid)
        {
            MdlMstEnquiryRequire values = new MdlMstEnquiryRequire();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstEnquiryRequire.DaDeleteEnquiryRequire(enquiryrequire_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EnquiryRequireInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage EnquiryRequireInactiveLogview(string enquiryrequire_gid)
        {
            MdlMstEnquiryRequire values = new MdlMstEnquiryRequire();
            objDaMstEnquiryRequire.DaEnquiryRequireInactiveLogview(enquiryrequire_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryRequireActive")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryRequireActive()
        {
            MdlMstEnquiryRequire values = new MdlMstEnquiryRequire();
            objDaMstEnquiryRequire.DaGetEnquiryRequireActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}