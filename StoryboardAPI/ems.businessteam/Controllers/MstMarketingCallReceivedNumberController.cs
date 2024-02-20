using ems.businessteam.DataAccess;
using ems.businessteam.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.businessteam.Controllers
{
    [RoutePrefix("api/MstMarketingCallReceivedNumber")]
    [Authorize]

    public class MstMarketingCallReceivedNumberController : ApiController
    {

        DaMstMarketingCallReceivedNumber objDaMstMarketingCallReceivedNumber = new DaMstMarketingCallReceivedNumber();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetMarketingCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallReceivedNumber()
        {
            MdlMstMarketingCallReceivedNumber objapplication360 = new MdlMstMarketingCallReceivedNumber();
            objDaMstMarketingCallReceivedNumber.DaGetMarketingCallReceivedNumber(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateMarketingCallReceivedNumber")]
        [HttpPost]
        public HttpResponseMessage CreateMarketingCallReceivedNumber(marketingcallreceivednumber values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallReceivedNumber.DaCreateMarketingCallReceivedNumber(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditMarketingCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCallReceivedNumber(string marketingcallreceivednumber_gid)
        {
            marketingcallreceivednumber values = new marketingcallreceivednumber();
            objDaMstMarketingCallReceivedNumber.DaEditMarketingCallReceivedNumber(marketingcallreceivednumber_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingCallReceivedNumber")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingCallReceivedNumber(marketingcallreceivednumber values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallReceivedNumber.DaUpdateMarketingCallReceivedNumber(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMarketingCallReceivedNumber")]
        [HttpPost]
        public HttpResponseMessage InactiveMarketingCallReceivedNumber(marketingcallreceivednumber values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallReceivedNumber.DaInactiveMarketingCallReceivedNumber(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMarketingCallReceivedNumberHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveMarketingCallReceivedNumberHistory(string marketingcallreceivednumber_gid)
        {
            MarketingCallReceivedNumberInactiveHistory objapplicationhistory = new MarketingCallReceivedNumberInactiveHistory();
            objDaMstMarketingCallReceivedNumber.DaInactiveMarketingCallReceivedNumberHistory(objapplicationhistory, marketingcallreceivednumber_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteMarketingCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage DeleteMarketingCallReceivedNumber(string marketingcallreceivednumber_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingCallReceivedNumber.DaDeleteMarketingCallReceivedNumber(marketingcallreceivednumber_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}