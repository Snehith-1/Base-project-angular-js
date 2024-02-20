using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ems.audit.Controllers
{

    [RoutePrefix("api/AtmMstPositiveConfirmity")]
    [Authorize]

    public class AtmMstPositiveConfirmityController : ApiController
    {

        DaAtmMstPositiveConfirmity objDaAtmMstPositiveConfirmity = new DaAtmMstPositiveConfirmity();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetPositiveConfirmity")]
        [HttpGet]
        public HttpResponseMessage GetPositiveConfirmity()
        {
            MdlAtmMstPositiveConfirmity values = new MdlAtmMstPositiveConfirmity();
            objDaAtmMstPositiveConfirmity.DaGetPositiveConfirmity(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CreatePositiveConfirmity")]
        [HttpPost]
        public HttpResponseMessage CreatePositiveConfirmity(MdlAtmMstPositiveConfirmity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstPositiveConfirmity.DaCreatePositiveConfirmity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPositiveConfirmity")]
        [HttpGet]
        public HttpResponseMessage EditPositiveConfirmity(string positiveconfirmity_gid)
        {
            MdlAtmMstPositiveConfirmity values = new MdlAtmMstPositiveConfirmity();
            objDaAtmMstPositiveConfirmity.DaEditPositiveConfirmity(positiveconfirmity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePositiveConfirmity")]
        [HttpPost]
        public HttpResponseMessage UpdatePositiveConfirmity(MdlAtmMstPositiveConfirmity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstPositiveConfirmity.DaUpdatePositiveConfirmity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePositiveConfirmity")]
        [HttpPost]
        public HttpResponseMessage InactivePositiveConfirmity(MdlAtmMstPositiveConfirmity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstPositiveConfirmity.DaInactivePositiveConfirmity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePositiveConfirmity")]
        [HttpGet]
        public HttpResponseMessage DeletePositiveConfirmity(string positiveconfirmity_gid)
        {
            MdlAtmMstPositiveConfirmity values = new MdlAtmMstPositiveConfirmity();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstPositiveConfirmity.DaDeletePositiveConfirmity(positiveconfirmity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PositiveConfirmityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage PositiveConfirmityInactiveLogview(string positiveconfirmity_gid)
        {
            MdlAtmMstPositiveConfirmity values = new MdlAtmMstPositiveConfirmity();
            objDaAtmMstPositiveConfirmity.DaPositiveConfirmityInactiveLogview(positiveconfirmity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPositiveConfirmityActive")]
        [HttpGet]
        public HttpResponseMessage GetPositiveConfirmityActive()
        {
            MdlAtmMstPositiveConfirmity values = new MdlAtmMstPositiveConfirmity();
            objDaAtmMstPositiveConfirmity.DaGetPositiveConfirmityActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
