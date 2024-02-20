using System;
using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace ems.master.Controllers
{
    [RoutePrefix("api/MstScannedDocument")]
    [Authorize]

    public class MstScannedDocumentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstScannedDocument objDaMstScannedDocument = new DaMstScannedDocument();

        [ActionName("GetCADScannedDocMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocMakerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupMakerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocFollowupMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocCheckerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocApprovalCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocApprovalCheckerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocApprovalCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupCheckerSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocFollowupCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocApproverSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCADScannedDocFollowupApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupApproverSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocFollowupApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADScannedDocCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocCompletedSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         
        [ActionName("CADAppScannedDocCount")]
        [HttpGet]
        public HttpResponseMessage CADAppScannedDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaCADAppScannedDocCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnScannedDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnScannedDocList(string credit_gid, string application_gid)
        {
            ScannnedDocTaggedDocumentList values = new ScannnedDocTaggedDocumentList();
            objDaMstScannedDocument.DaGetCADTrnScannedDocList(values, credit_gid, application_gid);
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
            objDaMstScannedDocument.DaScannedDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocument(string documentcheckdtl_gid, string signeddocument_flag)
        {
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaMstScannedDocument.DaGetScannedDocument(documentcheckdtl_gid, signeddocument_flag, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetRMScannedDocument(string documentcheckdtl_gid, string signeddocument_flag)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaMstScannedDocument.DaGetRMScannedDocument(documentcheckdtl_gid, signeddocument_flag, values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPageloadScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetPageloadScannedDocument(string credit_gid, string lstype)
        {
            objDaMstScannedDocument.DaGetPageloadScannedDocument(credit_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("cancelscanneduploaddocument")]
        [HttpGet]
        public HttpResponseMessage cancelscanneduploaddocument(string scanneddocument_gid)
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.Dacancelscanneduploaddocument(scanneddocument_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //[ActionName("GetTempScannedDocument")]
        //[HttpGet]
        //public HttpResponseMessage GetTempScannedDocument(string documentcheckdtl_gid)
        //{
        //    scanneduploaddocumentlist values = new scanneduploaddocumentlist();
        //    objDaMstScannedDocument.DaGetScannedDocument(documentcheckdtl_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("PostMovedtoSignedDoc")]
        [HttpPost]
        public HttpResponseMessage PostMovedtoSignedDoc(movedtosigneddoc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostMovedtoSignedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateTagToDefStatus")]
        [HttpPost]
        public HttpResponseMessage PostUpdateTagToDefStatus(mdlGroupDocStatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostUpdateTagToDefStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage PostDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage UpdateDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaUpdateDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GettaggedDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedDeferralinfo(string documentcheckdtl_gid)
        {
            deferraltagged objresult = new deferraltagged(); 
            objDaMstScannedDocument.DaGettaggedDeferralinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetQueryRaisedinfo")]
        [HttpGet]
        public HttpResponseMessage GetQueryRaisedinfo(string documentcheckdtl_gid)
        {
            MdlTagQueryCheckpointList objresult = new MdlTagQueryCheckpointList();
            objDaMstScannedDocument.DaGetQueryRaisedinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GettaggedHistoryDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedHistoryDeferralinfo(string deferraltagdoc_gid)
        {
            deferraltagged objresult = new deferraltagged(); 
            objDaMstScannedDocument.DaGettaggedHistoryDeferralinfo(deferraltagdoc_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetMStDeferraltag")]
        [HttpGet]
        public HttpResponseMessage GetMStDeferraltag(string documentcheckdtl_gid, string lstype)
        {
            Mstdeferraltag objresult = new Mstdeferraltag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetMStDeferraltag(documentcheckdtl_gid, lstype, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // CAD Query ...//
        [ActionName("PostAppcadqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcadqueryadd(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostAppcadqueryadd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAppcadresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostAppcadresponsequery(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostAppcadresponsequery(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAppcadQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetAppcadQuerySummary(string documentcheckdtl_gid)
        {
            mslcadquerylist objresult = new mslcadquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetAppcadQuerySummary(objresult, documentcheckdtl_gid);
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
            objDaMstScannedDocument.DaQueryDocumentUpload(httpRequest, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("cancelQueryuploaddocument")]
        [HttpGet]
        public HttpResponseMessage cancelQueryuploaddocument(string tagquerydocument_gid)
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DacancelQueryuploaddocument(tagquerydocument_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearQueryuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearQueryuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DatmpclearQueryuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearRMuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearRMuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DatmpclearRMuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpcleartagquerydocument")]
        [HttpGet]
        public HttpResponseMessage tmpcleartagquerydocument(string tagquery_gid)
        {
            result objresult = new result(); 
            objDaMstScannedDocument.Datmpcleartagquerydocument(tagquery_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            objDaMstScannedDocument.DaGetQueryDocument(tagquery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTmpQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetTmpQueryDocument(tagquery_gid,getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMSummary()
        {
            customerRMsummarylist objresult = new customerRMsummarylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetRMSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetMyTeamRMSummary")]
        [HttpGet]
        public HttpResponseMessage GetMyTeamRMSummary()
        {
            customerRMsummarylist objresult = new customerRMsummarylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetMyTeamRMSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostInitiateExtensionorwaiver")]
        [HttpPost]
        public HttpResponseMessage PostInitiateExtensionorwaiver(mdlinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInitiatedExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedExtensionorwaiver(string documentcheckdtl_gid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetInitiatedExtensionorwaiver(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetApprovalExtensionwaiver")]
        [HttpGet]
        public HttpResponseMessage GetApprovalExtensionwaiver(string initiateextendorwaiver_gid)
        {
            mdlapprovaldtllist objresult = new mdlapprovaldtllist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetApprovalExtensionwaiver(objresult, initiateextendorwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostextenstionwaiverApproval")]
        [HttpPost]
        public HttpResponseMessage PostextenstionwaiverApproval(mdldocumentapprovaldtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostextenstionwaiverApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeferralApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalSummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetDeferralApprovalSummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralApprovalHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalHistorySummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetDeferralApprovalHistorySummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostDocumentConfirmationDoc")]
        [HttpPost]
        public HttpResponseMessage PostDocumentConfirmationDoc(mdldocumentconfirmation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostDocumentConfirmationDoc(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostScannedCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostScannedCovenantPeriods(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostScannedCovenantPeriods(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScannedCovenantPeriodsSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannedCovenantPeriodsSummary(string groupdocumentdtl_gid)
        {
            mdlscannedcovenantperiodlist objresult = new mdlscannedcovenantperiodlist(); 
            objDaMstScannedDocument.DaGetScannedCovenantPeriodsSummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetcancelscannedCovenantPeriod")]
        [HttpPost]
        public HttpResponseMessage GetcancelscannedCovenantPeriod(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaMstScannedDocument.DaGetcancelscannedCovenantPeriod(values, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralHistorySummary(string groupdocumentdtl_gid)
        {
            deferraltaggedlist objresult = new deferraltaggedlist();
            objDaMstScannedDocument.DaGetDeferralHistorySummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetScannedGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetScannedGeneralInfo(string application_gid)
        {
            mdlscannedgeneral objresult = new mdlscannedgeneral();
            objDaMstScannedDocument.DaGetScannedGeneralInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetConfirmationValidation")]
        [HttpGet]
        public HttpResponseMessage GetConfirmationValidation(string groupdocumentcheckdtl_gid,string lstype)
        {
            result objresult = new result();
            objDaMstScannedDocument.DaGetConfirmationValidation(objresult, groupdocumentcheckdtl_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDuplicateDeferralTagged")]
        [HttpGet]
        public HttpResponseMessage GetDuplicateDeferralTagged(string application_gid)
        {
            result objresult = new result();
            objDaMstScannedDocument.DaGetDuplicateDeferralTagged(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UpdateScannedApproval")]
        [HttpGet]
        public HttpResponseMessage UpdateScannedApproval(string lstype, string processtypeassign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaMstScannedDocument.DaUpdateScannedApproval(objresult, lstype, processtypeassign_gid,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("postMakerCheckerConversation")]
        [HttpPost]
        public HttpResponseMessage postMakerCheckerConversation(mdlmakercheckerconversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaMstScannedDocument.DapostMakerCheckerConversation(getsessionvalues.user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMakerCheckerConversation")]
        [HttpGet]
        public HttpResponseMessage GetMakerCheckerConversation(string groupdocumentcheckdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmakercheckerconversationlist objresult = new mdlmakercheckerconversationlist();
            objDaMstScannedDocument.DaGetMakerCheckerConversation(objresult, groupdocumentcheckdtl_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("GetScannedDocApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetScannedDocApprovalDtls(string application_gid)
        {
            MdlScannedDocApprovalDetails values = new MdlScannedDocApprovalDetails();
            objDaMstScannedDocument.DaGetScannedDocApprovalDtls(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompletedDocumentInfo")]
        [HttpGet]
        public HttpResponseMessage GetCompletedDocumentInfo(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            GetCompletedInfo objresult = new GetCompletedInfo();
            objDaMstScannedDocument.DaGetCompletedDocumentInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetScannedApprovalList")]
        [HttpGet]
        public HttpResponseMessage GetScannedApprovalList(string application_gid)
        {
            mdlScannedApprovaldtl objresult = new mdlScannedApprovaldtl();
            objDaMstScannedDocument.DaGetScannedApprovalList(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("ScannedDocumentMultiUpload")]
        [HttpPost]
        public HttpResponseMessage ScannedDocumentMultiUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            scanneduploaddocumentlist documentname = new scanneduploaddocumentlist();
            objDaMstScannedDocument.DaScannedDocumentMultiUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        } 
        [ActionName("PostMultipleInitiateExtensionorwaiver")]
        [HttpPost]
        public HttpResponseMessage PostMultipleInitiateExtensionorwaiver(mdlmultipleinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaPostMultipleInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInitiatedApprovalExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedApprovalExtensionorwaiver(string approval_initiationgid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetInitiatedApprovalExtensionorwaiver(objresult, approval_initiationgid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        } 
        [ActionName("GetCADScannedDocFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADScannedDocFollowupSummary()
        {
            scannedmakerapplicationlist values = new scannedmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetCADScannedDocFollowupSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUpdateCADRaiseQueryStatus")]
        [HttpPost]
        public HttpResponseMessage GetUpdateCADRaiseQueryStatus(mdlcadquerystatusupdate values)
        { 
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetUpdateCADRaiseQueryStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUpdateDocumentSubmission")]
        [HttpPost]
        public HttpResponseMessage GetUpdateDocumentSubmission(mdlsubmissiondateupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetUpdateDocumentSubmission(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPendingQueryDelete")]
        [HttpGet]
        public HttpResponseMessage GetPendingQueryDelete(string tagquery_gid)
        {
            result values = new result(); 
            objDaMstScannedDocument.DaGetPendingQueryDelete(values, tagquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("SoftcopyVettingDocExport")]
        [HttpPost]
        public HttpResponseMessage SoftcopyVettingDocExport(MdlCADExportConversation values)
        {

            objDaMstScannedDocument.DaSoftcopyVettingDocExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAppcadQuerySummaryRm")]
        [HttpGet]
        public HttpResponseMessage GetAppcadQuerySummaryRm(string documentcheckdtl_gid)
        {
            mslcadquerylist objresult = new mslcadquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstScannedDocument.DaGetAppcadQuerySummaryRm(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}