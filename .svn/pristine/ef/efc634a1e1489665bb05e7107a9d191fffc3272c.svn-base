using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.masterng.DataAccess;
using ems.masterng.Models;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.masterng.Controllers
{
    [RoutePrefix("api/KycNg")]
    public class KycNgController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [Authorize]
        [HttpPost]
        [ActionName("GetCINFromPAN")]
        public HttpResponseMessage GetCINFromPAN(PanNumberModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaKycNg ObjDaKycNg = new DaKycNg();
                var response = ObjDaKycNg.GetCINFromPAN(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("PANNumber")]
        public HttpResponseMessage PostPanNumber(PanNumberModel Values) //
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaKycNg ObjDaKycNg = new DaKycNg();
                var response = ObjDaKycNg.GetPanNumberDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetLEINumber")]
        public HttpResponseMessage GetLEINumber(PanNumberModel Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                DaKycNg ObjDaKycNg = new DaKycNg();
                var response = ObjDaKycNg.GetLEINumberDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

    }
}