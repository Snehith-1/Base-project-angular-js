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
    [RoutePrefix("api/MstMarketingSourceOfContact")]
    [Authorize]
    public class MstMarketingSourceOfContactController : ApiController
    {

        DaMstMarketingSourceOfContact objDaMstMarketingSourceOfContact = new DaMstMarketingSourceOfContact();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetMarketingSourceofContact")]
        [HttpGet]
        public HttpResponseMessage GetMarketingSourceofContact()
        {
            MdlMstMarketingSourceOfContact objapplication360 = new MdlMstMarketingSourceOfContact();
            objDaMstMarketingSourceOfContact.DaGetMarketingSourceofContact(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateMarketingSourceofContact")]
        [HttpPost]
        public HttpResponseMessage CreateMarketingSourceofContact(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingSourceOfContact.DaCreateMarketingSourceofContact(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditMarketingSourceofContact")]
        [HttpGet]
        public HttpResponseMessage EditMarketingSourceofContact(string marketingsourceofcontact_gid)
        {
            application360 values = new application360();
            objDaMstMarketingSourceOfContact.DaEditMarketingSourceofContact(marketingsourceofcontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingSourceofContact")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingSourceofContact(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingSourceOfContact.DaUpdateMarketingSourceofContact(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveMarketingSourceofContact")]
        [HttpPost]
        public HttpResponseMessage InactiveMarketingSourceofContact(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingSourceOfContact.DaInactiveMarketingSourceofContact(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveMarketingSourceofContactHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveMarketingSourceofContactHistory(string marketingsourceofcontact_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaMstMarketingSourceOfContact.DaInactiveMarketingSourceofContactHistory(objapplicationhistory, marketingsourceofcontact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteMarketingSourceofContact")]
        [HttpGet]
        public HttpResponseMessage DeleteMarketingSourceofContact(string marketingsourceofcontact_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstMarketingSourceOfContact.DaDeleteMarketingSourceofContact(marketingsourceofcontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}