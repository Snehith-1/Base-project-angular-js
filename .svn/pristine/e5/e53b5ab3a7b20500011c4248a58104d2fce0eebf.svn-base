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
    [RoutePrefix("api/MstLeadRequestType")]
    [Authorize]
    public class MstLeadRequestTypeController : ApiController
    {

        DaMstBDLeadRequestType objDaMstLeadRequestType = new DaMstBDLeadRequestType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetLeadRequestType")]
        [HttpGet]
        public HttpResponseMessage GetLeadRequestType()
        {
            MdlBDLeadRequestType values = new MdlBDLeadRequestType();
            objDaMstLeadRequestType.DaGetLeadRequestType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateLeadRequestType")]
        [HttpPost]
        public HttpResponseMessage CreateLeadRequestType(leadrequesttype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLeadRequestType.DaCreateLeadRequestType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditLeadRequestType")]
        [HttpGet]
        public HttpResponseMessage EditLeadRequestType(string leadrequesttype_gid)
        {
            leadrequesttype values = new leadrequesttype();
            objDaMstLeadRequestType.DaEditLeadRequestType(leadrequesttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLeadRequestType")]
        [HttpPost]
        public HttpResponseMessage UpdateLeadRequestType(leadrequesttype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLeadRequestType.DaUpdateLeadRequestType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLeadRequestType")]
        [HttpPost]
        public HttpResponseMessage InactiveLeadRequestType(leadrequesttype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLeadRequestType.DaInactiveLeadRequestType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLeadRequestTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveLeadRequestTypeHistory(string leadrequesttype_gid)
        {
            leadrequesttypeInactiveHistory values = new leadrequesttypeInactiveHistory();
            objDaMstLeadRequestType.DaInactiveLeadRequestTypeHistory(values, leadrequesttype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLeadRequestType")]
        [HttpGet]
        public HttpResponseMessage DeleteLeadRequestType(string leadrequesttype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstLeadRequestType.DaDeleteLeadRequestType(leadrequesttype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}