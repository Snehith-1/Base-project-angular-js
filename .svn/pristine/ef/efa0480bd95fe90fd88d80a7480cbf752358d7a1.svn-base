using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This controllers provide access to buyer cheque management flow
    /// </summary>
    /// <remarks>Written by Premchandar.K </remarks>


    [RoutePrefix("api/AgrUdcManagement")]
    [Authorize]

    public class AgrUdcManagementController : ApiController
    {

        DaAgrUdcManagement objDaAgrUdcManagement = new DaAgrUdcManagement();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("PostChequeDetail")]
        [HttpPost]
        public HttpResponseMessage PostChequeDetail(MdlCheque values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaPostChequeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetChequeSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaAgrUdcManagement.DaGetChequeSummary(getsessionvalues.employee_gid, values);
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
            objDaAgrUdcManagement.DaChequeDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetChequeDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaAgrUdcManagement.DaGetChequeDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentDelete(string cheque2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlChequeDocument values = new MdlChequeDocument();
            objDaAgrUdcManagement.DaChequeDocumentDelete(cheque2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateChequeDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateChequeDetail(MdlCheque values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaUpdateChequeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("ChequeDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage ChequeDetailsEdit(string udcmanagement2cheque_gid)
        {
            MdlCheque values = new MdlCheque();
            objDaAgrUdcManagement.DaChequeDetailsEdit(udcmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeList")]
        [HttpGet]
        public HttpResponseMessage ChequeList(string udcmanagement_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaAgrUdcManagement.DaChequeList(getsessionvalues.employee_gid, udcmanagement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentList(string udcmanagement2cheque_gid)
        {
            MdlChequeDocument values = new MdlChequeDocument();
            objDaAgrUdcManagement.DaChequeDocumentList(udcmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteChequeDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteChequeDetail(string udcmanagement2cheque_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCheque values = new MdlCheque();
            objDaAgrUdcManagement.DaDeleteChequeDetail(udcmanagement2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUdcSummary")]
        [HttpGet]
        public HttpResponseMessage GetUdcSummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetUdcSummary(getsessionvalues.employee_gid, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UdcDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage UdcDetailsEdit(string udcmanagement_gid)
        {
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaUdcDetailsEdit(udcmanagement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateUdcDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateUdcDetail(MdlUdcManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaUpdateUdcDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteUdc")]
        [HttpGet]
        public HttpResponseMessage DeleteUdc(string udcmanagement_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaDeleteUdc(udcmanagement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDropDownUdc")]
        [HttpGet]
        public HttpResponseMessage GetDropDownUdc()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDownUdc values = new MdlDropDownUdc();
            objDaAgrUdcManagement.DaGetDropDownUdc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomers")]
        [HttpGet]
        public HttpResponseMessage GetCustomers()
        {
            CustomersList values = new CustomersList();
            objDaAgrUdcManagement.DaGetCustomers(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetStakeholders")]
        [HttpGet]
        public HttpResponseMessage GetStakeholders(string application_gid)
        {
            StakeholdersList values = new StakeholdersList();
            objDaAgrUdcManagement.DaGetStakeholders(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUdcTempClear")]
        [HttpGet]
        public HttpResponseMessage GetUdcTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrUdcManagement.DaGetUdcTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetChequeMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeFollowupMakerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetChequeFollowupMakerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetChequeCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeFollowupCheckerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetChequeFollowupCheckerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeApproverSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetChequeApproverSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetChequeCompletedApprover")]
        [HttpGet]
        public HttpResponseMessage GetChequeCompletedApprover()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUdcManagement values = new MdlUdcManagement();
            objDaAgrUdcManagement.DaGetChequeCompletedApprover(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CADChequeSummaryCount")]
        [HttpGet]
        public HttpResponseMessage CADSanctionSummaryCount()
        {
            CadChequeCount values = new CadChequeCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaCADChequeSummaryCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChequeMakerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostChequeCheckerSubmit(Mdlmakerchequedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaPostChequeMakerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChequeCheckerSubmit")]
        [HttpPost]
        public HttpResponseMessage PostChequeCheckerSubmit(Mdlcheckerchequedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaPostChequeCheckerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostChequeApproval")]
        [HttpPost]
        public HttpResponseMessage PostChequeApproval(Mdlapprovalchequedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrUdcManagement.DaPostChequeApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApprovalDetails")]
        [HttpGet]
        public HttpResponseMessage GetApprovalDetails(string application_gid)
        {
            MdlChequeApprovalDetails values = new MdlChequeApprovalDetails();
            objDaAgrUdcManagement.DaGetApprovalDetails(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}