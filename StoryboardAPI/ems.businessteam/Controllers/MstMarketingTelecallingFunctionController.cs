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
    [RoutePrefix("api/MstMarketingTelecallingFunction")]
    [Authorize]
    public class MstMarketingTelecallingFunctionController : ApiController
    {

        DaMstMarketingTelecallingFunction objDaMstMarketingTelecallingFunction = new DaMstMarketingTelecallingFunction();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetMarketingTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage GetMarketingTelecallingFunction()
        {
            MdlMstMarketingTelecallingFunction objapplication360 = new MdlMstMarketingTelecallingFunction();
            objDaMstMarketingTelecallingFunction.DaGetMarketingTelecallingFunction(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateMarketingTelecallingFunction")]
        [HttpPost]
        public HttpResponseMessage CreateMarketingTelecallingFunction(marketingtelecallingfunction values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingTelecallingFunction.DaCreateMarketingTelecallingFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditMarketingTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage EditMarketingTelecallingFunction(string marketingtelecallingfunction_gid)
        {
            marketingtelecallingfunction values = new marketingtelecallingfunction();
            objDaMstMarketingTelecallingFunction.DaEditMarketingTelecallingFunction(marketingtelecallingfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingTelecallingFunction")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingTelecallingFunction(marketingtelecallingfunction values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingTelecallingFunction.DaUpdateMarketingTelecallingFunction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMarketingTelecallingFunction")]
        [HttpPost]
        public HttpResponseMessage InactiveMarketingTelecallingFunction(marketingtelecallingfunction values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingTelecallingFunction.DaInactiveMarketingTelecallingFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMarketingTelecallingFunctionHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveMarketingTelecallingFunctionHistory(string marketingtelecallingfunction_gid)
        {
            MarketingTelecallingFunctionInactiveHistory objapplicationhistory = new MarketingTelecallingFunctionInactiveHistory();
            objDaMstMarketingTelecallingFunction.DaInactiveMarketingTelecallingFunctionHistory(objapplicationhistory, marketingtelecallingfunction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteMarketingTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage DeleteMarketingTelecallingFunction(string marketingtelecallingfunction_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingTelecallingFunction.DaDeleteMarketingTelecallingFunction(marketingtelecallingfunction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}