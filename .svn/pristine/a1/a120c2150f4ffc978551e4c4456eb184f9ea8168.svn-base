using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;


// <summary>
// This controllers provide access for various Single and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Other Creditor Master 
// </summary>
// <remarks>Written by Premchander.K </remarks>

namespace ems.mastersamagro.Controllers
{
    [RoutePrefix("api/AgrMstCreditorMaster")]
    [Authorize]
    public class AgrMstCreditorMasterController : ApiController
    {
        DaAgrMstCreditorMaster objAgrMstCreditorMaster = new DaAgrMstCreditorMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("PostCreditorMasterGST")]
        [HttpPost]
        public HttpResponseMessage PostCreditorMasterGST(MdlcreditorGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostCreditorMasterGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditorMasterGSTList")]
        [HttpPost]
        public HttpResponseMessage PostCreditorMasterGSTList(MdlcreditorGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostCreditorMasterGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get CreditorMaster GST List

        [ActionName("GetCreditorMasterGSTList")]
        [HttpGet]
        public HttpResponseMessage GetCreditorMasterGSTList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorGST values = new MdlcreditorGST();
            objAgrMstCreditorMaster.DaGetCreditorMasterGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlCreditorGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOfficeEdit")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOfficeEdit(MdlCreditorGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaUpdateGSTHeadOfficeEdit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit CreditorMaster GST

        [ActionName("EditCreditorMasterGST")]
        [HttpGet]
        public HttpResponseMessage EditCreditorMasterGST(string creditor2branch_gid)
        {
            MdlcreditorGST values = new MdlcreditorGST();
            objAgrMstCreditorMaster.DaEditCreditorMasterGST(creditor2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update CreditorMaster GST

        [ActionName("UpdateCreditorMasterGST")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditorMasterGST(MdlcreditorGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaUpdateCreditorMasterGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete CreditorMaster GST

        [ActionName("DeleteCreditorMasterGST")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditorMasterGST(string creditor2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorGST values = new MdlcreditorGST();
            objAgrMstCreditorMaster.DaDeleteCreditorMasterGST(creditor2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostcreditorAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostcreditorAddressDetail(MdlcreditorAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostcreditorAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Warehouse Address List

        [ActionName("GetcreditorAddressList")]
        [HttpGet]
        public HttpResponseMessage GetcreditorAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorAddressDetails values = new MdlcreditorAddressDetails();
            objAgrMstCreditorMaster.DaGetcreditorAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Warehouse Address Details 

        [ActionName("EditcreditorAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditcreditorAddressDetail(string creditor2address_gid)
        {
            MdlcreditorAddressDetails values = new MdlcreditorAddressDetails();
            objAgrMstCreditorMaster.DaEditcreditorAddressDetail(creditor2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Warehouse Address Details 

        [ActionName("UpdatecreditorAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdatecreditorAddressDetail(MdlcreditorAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaUpdatecreditorAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Warehouse Address Details 

        [ActionName("DeletecreditorAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeletecreditorAddressDetail(string creditor2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorAddressDetails values = new MdlcreditorAddressDetails();
            objAgrMstCreditorMaster.DaDeletecreditorAddressDetail(creditor2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostChequeDetail")]
        [HttpPost]
        public HttpResponseMessage PostChequeDetail(Mdlcreditorcheque values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostChequeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetChequeSummary")]
        [HttpGet]
        public HttpResponseMessage GetChequeSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditorcheque values = new Mdlcreditorcheque();
            objAgrMstCreditorMaster.DaGetChequeSummary(getsessionvalues.employee_gid, values);
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
            creditorchequeuploaddocument documentname = new creditorchequeuploaddocument();
            objAgrMstCreditorMaster.DaChequeDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetChequeDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorchequeDocument values = new MdlcreditorchequeDocument();
            objAgrMstCreditorMaster.DaGetChequeDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentDelete(string creditorcheque2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorchequeDocument values = new MdlcreditorchequeDocument();
            objAgrMstCreditorMaster.DaChequeDocumentDelete(creditorcheque2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateChequeDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateChequeDetail(Mdlcreditorcheque values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaUpdateChequeDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("ChequeDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage ChequeDetailsEdit(string creditor2cheque_gid)
        {
            Mdlcreditorcheque values = new Mdlcreditorcheque();
            objAgrMstCreditorMaster.DaChequeDetailsEdit(creditor2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeList")]
        [HttpGet]
        public HttpResponseMessage ChequeList(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditorcheque values = new Mdlcreditorcheque();
            objAgrMstCreditorMaster.DaChequeList(getsessionvalues.employee_gid, creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ChequeDocumentList")]
        [HttpGet]
        public HttpResponseMessage ChequeDocumentList(string creditor2cheque_gid)
        {
            MdlcreditorchequeDocument values = new MdlcreditorchequeDocument();
            objAgrMstCreditorMaster.DaChequeDocumentList(creditor2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteChequeDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteChequeDetail(string creditor2cheque_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditorcheque values = new Mdlcreditorcheque();
            objAgrMstCreditorMaster.DaDeleteChequeDetail(creditor2cheque_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostcreditorAgreementDetails")]
        [HttpPost]
        public HttpResponseMessage PostcreditorAgreementDetails(Mdlcreditoragreementdtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostcreditorAgreementDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetcreditorAgreementDetails")]
        [HttpGet]
        public HttpResponseMessage GetcreditorAgreementDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorMaster values = new MdlcreditorMaster();
            objAgrMstCreditorMaster.DaGetcreditorAgreementDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("creditorDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage creditorDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlcreditorMaster values = new MdlcreditorMaster();
            objAgrMstCreditorMaster.Dacreditordocumentupload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("creditordoc_delete")]
        [HttpGet]
        public HttpResponseMessage creditordoc_delete(string creditoragreement2docupload_gid, string creditor2agreement_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorMaster values = new MdlcreditorMaster();
            objAgrMstCreditorMaster.Dagetcreditordoc_delete(creditoragreement2docupload_gid, creditor2agreement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcreditordocument")]
        [HttpGet]
        public HttpResponseMessage Getcreditordocument(string creditor2agreement_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorMaster values = new MdlcreditorMaster();
            objAgrMstCreditorMaster.Dagetcreditordocument(getsessionvalues.employee_gid, creditor2agreement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAgreementDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteAgreementDetail(string creditor2agreement_gid)
        {
            result values = new result();
            objAgrMstCreditorMaster.DaDeleteAgreementDetail(creditor2agreement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditorTmpClear")]
        [HttpGet]
        public HttpResponseMessage GetCreditorTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaGetCreditorTmpClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("PostCreditorSubmit")]
        [HttpPost]
        public HttpResponseMessage PostCreditorSubmit(MdlcreditorCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostCreditorSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetNewcreditorSummary")]
        [HttpGet]
        public HttpResponseMessage GetNewcreditorSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetNewcreditorSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCreditorDetails")]
        [HttpGet]
        public HttpResponseMessage EditCreditorDetails(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaEditcreditorDetails(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("creditorGSTTmpList")]
        [HttpGet]
        public HttpResponseMessage creditorGSTTmpList(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorGST values = new MdlcreditorGST();
            objAgrMstCreditorMaster.DacreditorGSTTmpList(getsessionvalues.employee_gid, creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("creditorAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage creditorAddressTmpList(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorAddressDetails values = new MdlcreditorAddressDetails();
            objAgrMstCreditorMaster.DacreditorAddressTmpList(getsessionvalues.employee_gid, creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetcreditorAgreementDetailsTmpList")]
        [HttpGet]
        public HttpResponseMessage GetcreditorAgreementDetailsTmpList(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorMaster values = new MdlcreditorMaster();
            objAgrMstCreditorMaster.DaGetcreditorAgreementDetailsTmpList(getsessionvalues.employee_gid, creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("creditorDocumentUploadTmpList")]
        [HttpGet]
        public HttpResponseMessage creditorDocumentUploadTmpList(string creditor2agreement_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorMaster values = new MdlcreditorMaster();
            objAgrMstCreditorMaster.DacreditorDocumentUploadTmpList(creditor2agreement_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatecreditorDtl")]
        [HttpPost]
        public HttpResponseMessage UpdatewarehouseDtl(MdlcreditorCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaUpdatecreditorDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetChequeSummaryTmplist")]
        [HttpGet]
        public HttpResponseMessage GetChequeSummaryTmplist(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditorcheque values = new Mdlcreditorcheque();
            objAgrMstCreditorMaster.DaGetChequeSummaryTmplist(getsessionvalues.employee_gid, creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditorSubmitApproval")]
        [HttpPost]
        public HttpResponseMessage PostCreditorSubmitApproval(Mdlcreditorapproval values)
        {
           
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostCreditorSubmitApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditorApproval")]
        [HttpPost]
        public HttpResponseMessage PostCreditorApproval(Mdlcreditorapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostCreditorApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRejectedCreditorSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedCreditorSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetRejectedCreditorSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditorApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditorApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetCreditorApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditorApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditorApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetCreditorApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetRMRejectedCreditorSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMRejectedCreditorSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetRMRejectedCreditorSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMCreditorApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMCreditorApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetRMCreditorApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMCreditorApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMCreditorApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetRMCreditorApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMCreditorOpenSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMCreditorOpenSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetRMCreditorOpenSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMCreditorCountDetail")]
        [HttpGet]
        public HttpResponseMessage GetRMCreditorCountDetail()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RMCreditorCountdtl values = new RMCreditorCountdtl();
            objAgrMstCreditorMaster.DaGetRMCreditorCountDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalCreditorCountDetail")]
        [HttpGet]
        public HttpResponseMessage GetApprovalCreditorCountDetail()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RMCreditorCountdtl values = new RMCreditorCountdtl();
            objAgrMstCreditorMaster.DaGetApprovalCreditorCountDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGSTCreditor")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTCreditor(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorGST values = new MdlcreditorGST();
            objAgrMstCreditorMaster.DaDeleteGSTCreditor(getsessionvalues.employee_gid, creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditorApproveRejectSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditorApproveRejectSummary(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaGetCreditorApproveRejectSummary(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CreditorProductEdit")]
        [HttpGet]
        public HttpResponseMessage CreditorProductEdit(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaCreditorProductEdit(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CreditorProgramEdit")]
        [HttpGet]
        public HttpResponseMessage CreditorProgramEdit(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaCreditorProgramEdit(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditorSubProductEdit")]
        [HttpGet]
        public HttpResponseMessage CreditorSubProductEdit(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaCreditorSubProductEdit(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("CreditorProgramView")]
        [HttpGet]
        public HttpResponseMessage CreditorProgramView(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaCreditorProgramView(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditorProductView")]
        [HttpGet]
        public HttpResponseMessage CreditorProductView(string creditor_gid)
        {
            MdlcreditorCreation values = new MdlcreditorCreation();
            objAgrMstCreditorMaster.DaCreditorProductView(creditor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditorRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostCreditorRaiseQuery(mdlcreditorraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostCreditorRaiseQuery(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditorRaiseQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditorRaiseQuerySummary(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcreditorraisequery values = new mdlcreditorraisequery();
            objAgrMstCreditorMaster.DaGetCreditorRaiseQuerySummary(values, creditor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOpenQueryStatus")]
        [HttpGet]
        public HttpResponseMessage GetOpenQueryStatus(string creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcreditorraisequery values = new mdlcreditorraisequery();
            objAgrMstCreditorMaster.DaGetOpenQueryStatus(values, creditor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetRaiseQuerydesc")]
        [HttpGet]
        public HttpResponseMessage GetRaiseQuerydesc(string creditorquery_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcreditorraisequery values = new mdlcreditorraisequery();
            objAgrMstCreditorMaster.DaGetRaiseQuerydesc(values, creditorquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("PostUpdateQueryStatus")]
        [HttpPost]
        public HttpResponseMessage PostUpdateQueryStatus(mdlcreditorraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostUpdateQueryStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getcreditorapplicanttype")]
        [HttpGet]
        public HttpResponseMessage Getcreditorapplicanttype()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetcreditorapplicanttype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovedcreditorSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovedcreditorSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlcreditorSummary values = new MdlcreditorSummary();
            objAgrMstCreditorMaster.DaGetApprovedcreditorSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTrade2CreditorDtl")]
        [HttpPost]
        public HttpResponseMessage PostTrade2CreditorDtl(Mdlcreditor2warehouse values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostTrade2CreditorDtl(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTrade2CreditorDtl")]
        [HttpGet]
        public HttpResponseMessage GetTrade2CreditorDtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaGetTrade2CreditorDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteTrade2CreditorDtl")]
        [HttpGet]
        public HttpResponseMessage DeleteTrade2CreditorDtl(string applicationtrade2creditor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaDeleteTrade2CreditorDtl(applicationtrade2creditor_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTrade2CreditorTmpDtl")]
        [HttpGet]
        public HttpResponseMessage GetTrade2CreditorTmpDtl(string application_gid, string application2trade_gid, string tmp_status, string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaGetTrade2CreditorTmpDtl(getsessionvalues.employee_gid, application_gid, application2trade_gid, tmp_status, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostEditTrade2CreditorDtl")]
        [HttpPost]
        public HttpResponseMessage PostEditTrade2CreditorDtl(Mdlcreditor2warehouse values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostEditTrade2CreditorDtl(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostEditTrade2WarehouseDetail")]
        [HttpPost]
        public HttpResponseMessage PostEditTrade2WarehouseDetail(Mdlcreditor2warehouse values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstCreditorMaster.DaPostEditTrade2WarehouseDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditTrade2CreditorTmpDtl")]
        [HttpGet]
        public HttpResponseMessage GetEditTrade2CreditorTmpDtl(string application_gid, string application2trade_gid, string tmp_status, string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaGetEditTrade2CreditorTmpDtl(getsessionvalues.employee_gid, application_gid, application2trade_gid, tmp_status, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetEditTrade2WarehouseTmpDetail")]
        [HttpGet]
        public HttpResponseMessage GetEditTrade2WarehouseTmpDetail(string application_gid, string application2trade_gid, string tmp_status, string application2loan_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaGetEditTrade2WarehouseTmpDetail(getsessionvalues.employee_gid, application_gid, application2trade_gid, tmp_status, application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditTrade2CreditorDtl")]
        [HttpGet]
        public HttpResponseMessage GetEditTrade2CreditorDtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaGetEditTrade2CreditorDtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetEditTrade2WarehouseDetail")]
        [HttpGet]
        public HttpResponseMessage GetEditTrade2WarehouseDetail(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcreditor2warehouse values = new Mdlcreditor2warehouse();
            objAgrMstCreditorMaster.DaGetEditTrade2WarehouseDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}