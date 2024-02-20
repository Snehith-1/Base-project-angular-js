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
    [RoutePrefix("api/MstMarketingCallType")]
    [Authorize]
    public class MstMarketingCallTypeController : ApiController
    {

        DaMstMarketingCallType objDaMstMarketingCallType = new DaMstMarketingCallType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetCreateMarketingCallType")]
        [HttpGet]
        public HttpResponseMessage GetCreateMarketingCallType()
        {
            MdlMstMarketingCallType objapplication360 = new MdlMstMarketingCallType();
            objDaMstMarketingCallType.DaGetCreateMarketingCallType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateMarketingCallType")]
        [HttpPost]
        public HttpResponseMessage CreateMarketingCallType(marketingcalltype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallType.DaCreateMarketingCallType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditMarketingCallType")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCallType(string marketingcalltype_gid)
        {
            marketingcalltype values = new marketingcalltype();
            objDaMstMarketingCallType.DaEditMarketingCallType(marketingcalltype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingCallType")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingCallType(marketingcalltype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallType.DaUpdateMarketingCallType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMarketingCallType")]
        [HttpPost]
        public HttpResponseMessage InactiveMarketingCallType(marketingcalltype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallType.DaInactiveMarketingCallType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMarketingCallTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveMarketingCallTypeHistory(string marketingcalltype_gid)
        {
            MarketingCallTypeInactiveHistory objapplicationhistory = new MarketingCallTypeInactiveHistory();
            objDaMstMarketingCallType.DaInactiveMarketingCallTypeHistory(objapplicationhistory, marketingcalltype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteMarketingCallType")]
        [HttpGet]
        public HttpResponseMessage DeleteMarketingCallType(string marketingcalltype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallType.DaDeleteMarketingCallType(marketingcalltype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}