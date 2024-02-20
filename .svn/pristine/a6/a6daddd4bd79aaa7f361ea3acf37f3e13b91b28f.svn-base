using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Http.Results;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCourierManagement")]
    [Authorize]

    public class MstCourierManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCourierManagement objDaMstCourierManagement = new DaMstCourierManagement();      

        // Cad Accepted - Customer Name Drop Down
        [ActionName("GetCadCustomerName")]
        [HttpGet]
        public HttpResponseMessage GetCadCustomerName()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCadCustomerName values = new MdlCadCustomerName();
            objDaMstCourierManagement.DaGetCadCustomerName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Cad Accepted - Customer Sanction Drop Down
        [ActionName("Getcustomer2sanction")]
        [HttpGet]
        public HttpResponseMessage Getcustomer2sanction(string application_gid)
        {
            MdlCadCustomerName values = new MdlCadCustomerName();
            objDaMstCourierManagement.DaGetcustomer2sanction(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Insert Courier Details
        [ActionName("SubmitCourierDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitCourierDtl(MdlCourierDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCourierManagement.DaSubmitCourierDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Courier Document Upload
        [ActionName("CourierDocUpload")]
        [HttpPost]
        public HttpResponseMessage CourierDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Mdluploadcourierdoc values = new Mdluploadcourierdoc();
            objDaMstCourierManagement.DaCourierDocUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Courier Document Summary
        [ActionName("GetCourierDoc")]
        [HttpGet]
        public HttpResponseMessage Getcourierdoc()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcourierdocumentlist values = new Mdlcourierdocumentlist();
            objDaMstCourierManagement.DaGetcourierdoc(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Delete Courier Management
        [ActionName("DeleteCourierDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteCourierDoc(string courierdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaMstCourierManagement.DaDeleteCourierDoc(courierdocument_gid, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Courier Company Drop Down
        [ActionName("GetCourierCompany")]
        [HttpGet]
        public HttpResponseMessage GetCourierCompany()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCourierCompany values = new MdlCourierCompany();
            objDaMstCourierManagement.DaGetCourierCompany(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Edit the Courier Details
        [ActionName("GetEditCourierDetail")]
        [HttpGet]
        public HttpResponseMessage GetEditCourierDetail(string courier_gid)
        {
            MdlEditCourierMgmt values = new MdlEditCourierMgmt();
            objDaMstCourierManagement.DaGetEditCourierDtl(values, courier_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update the Courier Details
        [ActionName("PostUpdateCourier")]
        [HttpPost]
        public HttpResponseMessage PostUpdateCourier(MdlEditCourierMgmt values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCourierManagement.DaPostUpdateCourier(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get the Summary Details
        [ActionName("GetCourierMgmt")]
        [HttpGet]
        public HttpResponseMessage GetCourierMgnt(string courier_type)
        {
            MdlCourierManagement values = new MdlCourierManagement();
            objDaMstCourierManagement.DaGetCourierMgmt(courier_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Courier Count
        [ActionName("CourierCount")]
        [HttpGet]
        public HttpResponseMessage CourierCount()
        {
            courier_count values = new Models.courier_count();
            objDaMstCourierManagement.DaGetCourierCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Courier Acknowledge Notification
        [ActionName("GetACKNotification")]
        [HttpGet]
        public HttpResponseMessage GetACKNotification()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCourierManagement values = new MdlCourierManagement();
            objDaMstCourierManagement.DaGetACKNotification(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Courier Acknowledgement List
        [ActionName("CourierAckList")]
        [HttpGet]
        public HttpResponseMessage CourierAckList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCourierManagement values = new MdlCourierManagement();
            objDaMstCourierManagement.DaCourierAckList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Courier Acknowledgement View
        [ActionName("CourierAckView")]
        [HttpGet]
        public HttpResponseMessage CourierAckView(string courierMgmt_gid)
        {
            MdlEditCourierMgmt values = new MdlEditCourierMgmt();
            objDaMstCourierManagement.DaCourierAckView(courierMgmt_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update Courier Acknowledgement Status
        [ActionName("AckStatus")]
        [HttpPost]
        public HttpResponseMessage AckStatus(MdlEditCourierMgmt values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCourierManagement.DaAckStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Courier Document - Temp Clear 
        [ActionName("GetCourierDocTempClear")]
        [HttpGet]
        public HttpResponseMessage GetCourierDocTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCourierManagement.DaGetCourierDocTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Get Courier Edit Document Summary
        [ActionName("GetEditCourierDoc")]
        [HttpGet]
        public HttpResponseMessage GetEditCourierDoc(string courier_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEditCourierMgmt values = new MdlEditCourierMgmt();
            objDaMstCourierManagement.DaGetEditCourierDoc(getsessionvalues.employee_gid, courier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}