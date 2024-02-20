using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstRMPostCCWaiver")]
    [Authorize]
    public class MstRMPostCCWaiverController : ApiController
    {
        DaMstRMPostCCWaiver objDaMstRMPostCCWaiver = new DaMstRMPostCCWaiver();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetApplicationWaiverDetail")]
        [HttpGet]
        public HttpResponseMessage GetApplicationWaiverDetails(string application_gid)
        {
            MdlApplicationWaiverDetail values = new MdlApplicationWaiverDetail();
            objDaMstRMPostCCWaiver.DaGetApplicationWaiverDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerAndLANFromURN")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAndLANFromURN(string urn)
        {
            MdlApplicationWaiverDetail values = new MdlApplicationWaiverDetail();
            objDaMstRMPostCCWaiver.DaGetCustomerAndLANFromURN(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApplicationWaiverMaster")]
        [HttpGet]
        public HttpResponseMessage GetApplicationWaiverMaster()
        {
            MdlApplicationWaiverMaster values = new MdlApplicationWaiverMaster();
            objDaMstRMPostCCWaiver.DaGetApplicationWaiverMaster(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Document Upload

        [ActionName("WaiverDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage WaiverDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaMstRMPostCCWaiver.DaWaiverDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetWaiverDocList")]
        [HttpGet]
        public HttpResponseMessage GetWaiverDocList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWaiverDocument values = new MdlWaiverDocument();
            objDaMstRMPostCCWaiver.DaGetWaiverDocList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WaiverDocDelete")]
        [HttpGet]
        public HttpResponseMessage WaiverDocDelete(string rmpostccwaiver2document_gid)
        {
            MdlWaiverDocument values = new MdlWaiverDocument();
            objDaMstRMPostCCWaiver.DaWaiverDocDelete(rmpostccwaiver2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostApprovalMember")]
        [HttpPost]
        public HttpResponseMessage PostApprovalMember(MdlWaiverApprovalMember values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRMPostCCWaiver.DaPostApprovalMember(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalMemberList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalMemberList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWaiverApprovalMember values = new MdlWaiverApprovalMember();
            objDaMstRMPostCCWaiver.DaGetApprovalMemberList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApprovalMemberDelete")]
        [HttpGet]
        public HttpResponseMessage ApprovalMemberDelete(string rmpostccwaiver2approvalmember_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWaiverApprovalMember values = new MdlWaiverApprovalMember();
            objDaMstRMPostCCWaiver.DaApprovalMemberDelete(rmpostccwaiver2approvalmember_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitRMPostCCWaiver")]
        [HttpPost]
        public HttpResponseMessage SubmitRMPostCCWaiver(MdlRMPostCCWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRMPostCCWaiver.DaSubmitRMPostCCWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionWaiverSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionWaiverSummary(string application_gid)
        {
            MdlRMPostCCWaiver values = new MdlRMPostCCWaiver();
            objDaMstRMPostCCWaiver.DaGetSanctionWaiverSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWaiverTempClear")]
        [HttpGet]
        public HttpResponseMessage GetWaiverTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstRMPostCCWaiver.DaGetWaiverTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditRMPostCCWaiver")]
        [HttpGet]
        public HttpResponseMessage EditRMPostCCWaiver(string rmpostccwaiver_gid)
        {
            MdlRMPostCCWaiver values = new MdlRMPostCCWaiver();
            objDaMstRMPostCCWaiver.DaEditRMPostCCWaiver(rmpostccwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WaiverDocList")]
        [HttpGet]
        public HttpResponseMessage WaiverDocList(string rmpostccwaiver_gid)
        {
            MdlWaiverDocument values = new MdlWaiverDocument();
            objDaMstRMPostCCWaiver.DaWaiverDocList(rmpostccwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApprovalMemberList")]
        [HttpGet]
        public HttpResponseMessage ApprovalMemberList(string rmpostccwaiver_gid)
        {
            MdlWaiverApprovalMember values = new MdlWaiverApprovalMember();
            objDaMstRMPostCCWaiver.DaApprovalMemberList(rmpostccwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WaiverDocTempList")]
        [HttpGet]
        public HttpResponseMessage WaiverDocTempList(string rmpostccwaiver_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWaiverDocument values = new MdlWaiverDocument();
            objDaMstRMPostCCWaiver.DaWaiverDocTempList(rmpostccwaiver_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApprovalMemberTempList")]
        [HttpGet]
        public HttpResponseMessage ApprovalMemberTempList(string rmpostccwaiver_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWaiverApprovalMember values = new MdlWaiverApprovalMember();
            objDaMstRMPostCCWaiver.DaApprovalMemberTempList(rmpostccwaiver_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateRMPostCCWaiver")]
        [HttpPost]
        public HttpResponseMessage UpdateRMPostCCWaiver(MdlRMPostCCWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRMPostCCWaiver.DaUpdateRMPostCCWaiver(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRMPostCCWaiver")]
        [HttpGet]
        public HttpResponseMessage DeleteRMPostCCWaiver(string rmpostccwaiver_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRMPostCCWaiver values = new MdlRMPostCCWaiver();
            objDaMstRMPostCCWaiver.DaDeleteRMPostCCWaiver(rmpostccwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLANWaiverSummary")]
        [HttpGet]
        public HttpResponseMessage GetLANWaiverSummary(string application_gid)
        {
            MdlRMPostCCWaiver values = new MdlRMPostCCWaiver();
            objDaMstRMPostCCWaiver.DaGetLANWaiverSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWaiverApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetWaiverApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRMPostCCWaiver values = new MdlRMPostCCWaiver();
            objDaMstRMPostCCWaiver.DaGetWaiverApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WaiverApprovalDetailList")]
        [HttpGet]
        public HttpResponseMessage WaiverApprovalDetailList(string rmpostccwaiver_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWaiverApprovalDetail values = new MdlWaiverApprovalDetail();
            objDaMstRMPostCCWaiver.DaWaiverApprovalDetailList(rmpostccwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostWaiverApproved")]
        [HttpPost]
        public HttpResponseMessage PostRequestApproved(waiverapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRMPostCCWaiver.DaPostWaiverApproved(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostWaiverRejected")]
        [HttpPost]
        public HttpResponseMessage PostWaiverRejected(waiverapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRMPostCCWaiver.DaPostWaiverRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWaiverApprovalHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetWaiverApprovalHistorySummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRMPostCCWaiver values = new MdlRMPostCCWaiver();
            objDaMstRMPostCCWaiver.DaGetWaiverApprovalHistorySummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WaiverApprovalHistoryList")]
        [HttpPost]
        public HttpResponseMessage WaiverApprovalHistoryList(MdlRMPostCCWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);     
            objDaMstRMPostCCWaiver.DaWaiverApprovalHistoryList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDescDocAppPersons")]
        [HttpGet]
        public HttpResponseMessage GetDescDocAppPersons(string rmpostccwaiver_gid)
        { 
            MdlDescDocAppPersons values = new MdlDescDocAppPersons();
            objDaMstRMPostCCWaiver.DaGetDescDocAppPersons(rmpostccwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}