using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.master.Models;
using ems.master.DataAccess;
using System.Web;

namespace ems.master.Controllers
{
    [RoutePrefix("api/UdcManagement")]
    [Authorize]

    public class UdcManagementController : ApiController
    {
        DaUdcManagement objDaUdcManagement = new DaUdcManagement();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("PostChequeDetail")]
        [HttpPost]
        public HttpResponseMessage PostChequeDetail(MdlCheque values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaPostChequeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetChequeSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaUdcManagement.DaGetChequeSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage ChequeDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaUdcManagement.DaChequeDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetChequeDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaUdcManagement.DaGetChequeDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentDelete(string cheque2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaUdcManagement.DaChequeDocumentDelete(cheque2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateChequeDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateChequeDetail(MdlCheque values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaUpdateChequeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        

        [ActionName("ChequeDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage ChequeDetailsEdit(string udcmanagement2cheque_gid)
        {
            MdlCheque values = new MdlCheque();
            objDaUdcManagement.DaChequeDetailsEdit(udcmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeList")]
        [HttpGet]
        public HttpResponseMessage ChequeList(string udcmanagement_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaUdcManagement.DaChequeList(getsessionvalues.employee_gid,udcmanagement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentList(string udcmanagement2cheque_gid)
        {
            MdlChequeDocument values = new MdlChequeDocument();
            objDaUdcManagement.DaChequeDocumentList(udcmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteChequeDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteChequeDetail(string udcmanagement2cheque_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaUdcManagement.DaDeleteChequeDetail(udcmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUdcSummary")]
        [HttpGet]
        public HttpResponseMessage GetUdcSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetUdcSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UdcDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage UdcDetailsEdit(string udcmanagement_gid)
        {
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaUdcDetailsEdit(udcmanagement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateUdcDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateUdcDetail(MdlUdcManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaUpdateUdcDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteUdc")]
        [HttpGet]
        public HttpResponseMessage DeleteUdc(string udcmanagement_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaDeleteUdc(udcmanagement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDropDownUdc")]
        [HttpGet]
        public HttpResponseMessage GetDropDownUdc()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDownUdc values = new MdlDropDownUdc();
            objDaUdcManagement.DaGetDropDownUdc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomers")]
        [HttpGet]
        public HttpResponseMessage GetCustomers()
        {
            CustomersList values = new CustomersList();
            objDaUdcManagement.DaGetCustomers(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetStakeholders")]
        [HttpGet]
        public HttpResponseMessage GetStakeholders(string application_gid)
        {
            StakeholdersList values = new StakeholdersList();
            objDaUdcManagement.DaGetStakeholders(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUdcTempClear")]
        [HttpGet]
        public HttpResponseMessage GetUdcTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaUdcManagement.DaGetUdcTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetChequeMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetChequeFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetChequeCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeFollowupCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetChequeFollowupCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeApproverSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetChequeApproverSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeCompletedApprover")]
        [HttpGet]
        public HttpResponseMessage GetChequeCompletedApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaUdcManagement.DaGetChequeCompletedApprover(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CADChequeSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADSanctionSummaryCount()
        {
            CadChequeCount values = new CadChequeCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaCADChequeSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChequeMakerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostChequeCheckerSubmit(Mdlmakerchequedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaPostChequeMakerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChequeCheckerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostChequeCheckerSubmit(Mdlcheckerchequedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaPostChequeCheckerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChequeApproval")]
        [HttpPost]
        public HttpResponseMessage PostChequeApproval(Mdlapprovalchequedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUdcManagement.DaPostChequeApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalDetails")]
        [HttpGet]
        public HttpResponseMessage GetApprovalDetails(string application_gid)
        {
            MdlChequeApprovalDetails values = new MdlChequeApprovalDetails();
            objDaUdcManagement.DaGetApprovalDetails(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}