using System;
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
    /// This Controllers will provide access to various functionalities embedded in supplier scanned document flow 
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Praveen Raj.R, Premchander.K</remarks>

    [RoutePrefix("api/AgrMstSuprScannedDocument")]
    [Authorize]

    public class AgrMstSuprScannedDocumentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrMstSuprScannedDocument objDaAgrMstSuprScannedDocument = new DaAgrMstSuprScannedDocument();

        [ActionName("GetCADScannedDocMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocMakerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupMakerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocFollowupMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocCheckerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocApprovalCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocApprovalCheckerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocApprovalCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupCheckerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocFollowupCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocApproverSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCADScannedDocFollowupApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupApproverSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocFollowupApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocCompletedSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetCADScannedDocCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADAppScannedDocCount")]
        [HttpGet]
        public HttpResponseMessage CADAppScannedDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaCADAppScannedDocCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnScannedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnScannedDocList(string credit_gid, string application_gid)
        {
            ScannnedDocTaggedDocumentList values = new ScannnedDocTaggedDocumentList();
            objDaAgrMstSuprScannedDocument.DaGetCADTrnScannedDocList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ScannedDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage ScannedDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            scanneduploaddocumentlist documentname = new scanneduploaddocumentlist();
            objDaAgrMstSuprScannedDocument.DaScannedDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocument(string documentcheckdtl_gid, string signeddocument_flag)
        {
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaAgrMstSuprScannedDocument.DaGetScannedDocument(documentcheckdtl_gid, signeddocument_flag, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetRMScannedDocument(string documentcheckdtl_gid, string signeddocument_flag)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaAgrMstSuprScannedDocument.DaGetRMScannedDocument(documentcheckdtl_gid, signeddocument_flag, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPageloadScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetPageloadScannedDocument(string credit_gid, string lstype)
        {
            objDaAgrMstSuprScannedDocument.DaGetPageloadScannedDocument(credit_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("cancelscanneduploaddocument")]
        [HttpGet]
        public HttpResponseMessage cancelscanneduploaddocument(string scanneddocument_gid)
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.Dacancelscanneduploaddocument(scanneddocument_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //[ActionName("GetTempScannedDocument")]
        //[HttpGet]
        //public HttpResponseMessage GetTempScannedDocument(string documentcheckdtl_gid)
        //{
        //    scanneduploaddocumentlist values = new scanneduploaddocumentlist();
        //    objDaAgrMstSuprScannedDocument.DaGetScannedDocument(documentcheckdtl_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("PostMovedtoSignedDoc")]
        [HttpPost]
        public HttpResponseMessage PostMovedtoSignedDoc(movedtosigneddoc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostMovedtoSignedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage PostDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage UpdateDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaUpdateDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GettaggedDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedDeferralinfo(string documentcheckdtl_gid)
        {
            deferraltagged objresult = new deferraltagged();
            objDaAgrMstSuprScannedDocument.DaGettaggedDeferralinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GettaggedHistoryDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedHistoryDeferralinfo(string deferraltagdoc_gid)
        {
            deferraltagged objresult = new deferraltagged();
            objDaAgrMstSuprScannedDocument.DaGettaggedHistoryDeferralinfo(deferraltagdoc_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetMStDeferraltag")]
        [HttpGet]
        public HttpResponseMessage GetMStDeferraltag(string documentcheckdtl_gid, string lstype)
        {
            Mstdeferraltag objresult = new Mstdeferraltag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetMStDeferraltag(documentcheckdtl_gid, lstype, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // CAD Query ...//
        [ActionName("PostAppcadqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcadqueryadd(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostAppcadqueryadd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAppcadresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostAppcadresponsequery(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostAppcadresponsequery(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppcadQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetAppcadQuerySummary(string documentcheckdtl_gid)
        {
            mslcadquerylist objresult = new mslcadquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetAppcadQuerySummary(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("QueryDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage QueryDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objresult = new result();
            objDaAgrMstSuprScannedDocument.DaQueryDocumentUpload(httpRequest, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("cancelQueryuploaddocument")]
        [HttpGet]
        public HttpResponseMessage cancelQueryuploaddocument(string tagquerydocument_gid)
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DacancelQueryuploaddocument(tagquerydocument_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearQueryuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearQueryuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DatmpclearQueryuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearRMuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearRMuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DatmpclearRMuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            objDaAgrMstSuprScannedDocument.DaGetQueryDocument(tagquery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTmpQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetTmpQueryDocument(tagquery_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMSummary()
        {
            customerRMsummarylist objresult = new customerRMsummarylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetRMSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetMyTeamRMSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyTeamRMSummary()
        {
            customerRMsummarylist objresult = new customerRMsummarylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetMyTeamRMSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostInitiateExtensionorwaiver")]
        [HttpPost]
        public HttpResponseMessage PostInitiateExtensionorwaiver(mdlinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInitiatedExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedExtensionorwaiver(string documentcheckdtl_gid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetInitiatedExtensionorwaiver(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetApprovalExtensionwaiver")]
        [HttpGet]
        public HttpResponseMessage GetApprovalExtensionwaiver(string initiateextendorwaiver_gid)
        {
            mdlapprovaldtllist objresult = new mdlapprovaldtllist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetApprovalExtensionwaiver(objresult, initiateextendorwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostextenstionwaiverApproval")]
        [HttpPost]
        public HttpResponseMessage PostextenstionwaiverApproval(mdlapprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostextenstionwaiverApproval(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeferralApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalSummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetDeferralApprovalSummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralApprovalHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalHistorySummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaGetDeferralApprovalHistorySummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostDocumentConfirmationDoc")]
        [HttpPost]
        public HttpResponseMessage PostDocumentConfirmationDoc(mdldocumentconfirmation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostDocumentConfirmationDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostScannedCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostScannedCovenantPeriods(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DaPostScannedCovenantPeriods(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedCovenantPeriodsSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedCovenantPeriodsSummary(string groupdocumentdtl_gid)
        {
            mdlscannedcovenantperiodlist objresult = new mdlscannedcovenantperiodlist();
            objDaAgrMstSuprScannedDocument.DaGetScannedCovenantPeriodsSummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetcancelscannedCovenantPeriod")]
        [HttpPost]
        public HttpResponseMessage GetcancelscannedCovenantPeriod(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaAgrMstSuprScannedDocument.DaGetcancelscannedCovenantPeriod(values, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralHistorySummary(string groupdocumentdtl_gid)
        {
            deferraltaggedlist objresult = new deferraltaggedlist();
            objDaAgrMstSuprScannedDocument.DaGetDeferralHistorySummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetScannedGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetScannedGeneralInfo(string application_gid)
        {
            mdlscannedgeneral objresult = new mdlscannedgeneral();
            objDaAgrMstSuprScannedDocument.DaGetScannedGeneralInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetConfirmationValidation")]
        [HttpGet]
        public HttpResponseMessage GetConfirmationValidation(string groupdocumentcheckdtl_gid, string lstype)
        {
            result objresult = new result();
            objDaAgrMstSuprScannedDocument.DaGetConfirmationValidation(objresult, groupdocumentcheckdtl_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDuplicateDeferralTagged")]
        [HttpGet]
        public HttpResponseMessage GetDuplicateDeferralTagged(string application_gid)
        {
            result objresult = new result();
            objDaAgrMstSuprScannedDocument.DaGetDuplicateDeferralTagged(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UpdateScannedApproval")]
        [HttpGet]
        public HttpResponseMessage UpdateScannedApproval(string lstype, string processtypeassign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaAgrMstSuprScannedDocument.DaUpdateScannedApproval(objresult, lstype, processtypeassign_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("postMakerCheckerConversation")]
        [HttpPost]
        public HttpResponseMessage postMakerCheckerConversation(mdlmakercheckerconversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSuprScannedDocument.DapostMakerCheckerConversation(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMakerCheckerConversation")]
        [HttpGet]
        public HttpResponseMessage GetMakerCheckerConversation(string groupdocumentcheckdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmakercheckerconversationlist objresult = new mdlmakercheckerconversationlist();
            objDaAgrMstSuprScannedDocument.DaGetMakerCheckerConversation(objresult, groupdocumentcheckdtl_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetCompletedDocumentInfo")]
        [HttpGet]
        public HttpResponseMessage GetCompletedDocumentInfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            GetCompletedInfo objresult = new GetCompletedInfo();
            objDaAgrMstSuprScannedDocument.DaGetCompletedDocumentInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}