using ems.idas.DataAccess;
using ems.idas.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasCourierManagement")]
    [Authorize]
    public class IdasCourierManagementController : ApiController
    {
        DaIdasCourierManagement objDaAccess = new DaIdasCourierManagement();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        // Get the Summary Details
        [ActionName("GetCourierMgmt")]
        [HttpGet]
        public HttpResponseMessage GetCourierMgnt(string courier_type)
        {
            MdlIdasCourierManagement objCourierList = new MdlIdasCourierManagement();
            objDaAccess.DaGetCourierMgmt(courier_type,objCourierList);
            return Request.CreateResponse(HttpStatusCode.OK, objCourierList);
        }

        // Insert Courier Details
        [ActionName("IdasCourierSubmit")]
        [HttpPost]
        public HttpResponseMessage PostCourierDetail(CourierMgmt values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DapostCourierDtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Edit the Courier Details
        [ActionName("GetEditCourierDetail")]
        [HttpGet]
        public HttpResponseMessage GetEditCourierDetail(string courier_gid)
        {
            CourierMgmt values = new CourierMgmt();
            objDaAccess.DaGetEditCourierDtl(values, courier_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update the Courier Details
        [ActionName("PostUpdateCourier")]
        [HttpPost]
        public HttpResponseMessage PostUpdateCourier(CourierMgmt values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostUpdateCourier(getsessionvalues.user_gid,getsessionvalues .employee_gid , values, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("CourierDocUpload")]
        [HttpPost]
        public HttpResponseMessage courierdocupload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadcourierdocument documentname = new uploadcourierdocument();
            objDaAccess.DaPostcourierdocupload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetCourierDoc")]
        [HttpGet]
        public HttpResponseMessage Getcourierdoc()
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploadcourierdocumentlist values = new uploadcourierdocumentlist();
            objDaAccess.DaGetcourierdoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCourierDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteCourierDoc(string courierdocument_gid)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaAccess.DaDeleteCourierDoc(courierdocument_gid, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("CourierCount")]
        [HttpGet]
        public HttpResponseMessage CourierCount()
        {
            courier_count values = new Models.courier_count();
            objDaAccess.DaGetCourierCount (values );
            return Request.CreateResponse(HttpStatusCode.OK, values );
        }

        [ActionName("CourierAckList")]
        [HttpGet]
        public HttpResponseMessage CourierAckList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasCourierManagement objCourierList = new MdlIdasCourierManagement();
            objDaAccess.DaCourierAckList(getsessionvalues.employee_gid, objCourierList);
            return Request.CreateResponse(HttpStatusCode.OK, objCourierList);
        }

        [ActionName("CourierAckView")]
        [HttpGet]
        public HttpResponseMessage CourierAckView(string courierMgmt_gid)
        {
            CourierMgmt values = new CourierMgmt();
            objDaAccess.DaCourierAckView(courierMgmt_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AckStatus")]
        [HttpPost]
        public HttpResponseMessage AckStatus(CourierMgmt values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaAckStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetACKNotification")]
        [HttpGet]
        public HttpResponseMessage GetACKNotification()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            CourierMgmt values = new CourierMgmt();
            objDaAccess.DaGetACKNotification(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}