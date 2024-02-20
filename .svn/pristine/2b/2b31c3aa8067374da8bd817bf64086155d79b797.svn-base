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
    /// This Controllers will help to push and pull data's for original copy vetting
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A, Premchandar.K </remarks>

    [RoutePrefix("api/AgrTrnPhysicalDocument")]
    [Authorize]

    public class AgrTrnPhysicalDocumentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrTrnPhysicalDocument objDaAgrTrnPhysicalDocument = new DaAgrTrnPhysicalDocument();

        [ActionName("GetCADPhysicalDocMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocMakerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupMakerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocFollowupMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocCheckerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocApprovalCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocApprovalCheckerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocApprovalCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupCheckerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocFollowupCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocApproverSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCADPhysicalDocFollowupApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupApproverSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocFollowupApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocCompletedSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADAppPhysicalDocCount")]
        [HttpGet]
        public HttpResponseMessage CADAppPhysicalDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaCADAppPhysicalDocCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnPhysicalDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnPhysicalDocList(string credit_gid, string application_gid)
        {
            PhysicalDocTaggedDocumentList values = new PhysicalDocTaggedDocumentList();
            objDaAgrTrnPhysicalDocument.DaGetCADTrnPhysicalDocList(values, credit_gid, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PhysicalDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PhysicalDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            scanneduploaddocumentlist documentname = new scanneduploaddocumentlist();
            objDaAgrTrnPhysicalDocument.DaPhysicalDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        // end - changed //

        [ActionName("GetPhysicalDocument")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocument(string documentcheckdtl_gid)
        {
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalDocument(documentcheckdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetRMScannedDocument(string documentcheckdtl_gid, string signeddocument_flag)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaAgrTrnPhysicalDocument.DaGetRMScannedDocument(documentcheckdtl_gid, signeddocument_flag, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         

        [ActionName("cancelphysicaluploaddocument")]
        [HttpGet]
        public HttpResponseMessage cancelphysicaluploaddocument(string physicaldocument_gid)
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.Dacancelphysicaluploaddocument(physicaldocument_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //[ActionName("GetTempScannedDocument")]
        //[HttpGet]
        //public HttpResponseMessage GetTempScannedDocument(string documentcheckdtl_gid)
        //{
        //    scanneduploaddocumentlist values = new scanneduploaddocumentlist();
        //    objDaAgrTrnPhysicalDocument.DaGetScannedDocument(documentcheckdtl_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("PostMovedtoSignedDoc")]
        [HttpPost]
        public HttpResponseMessage PostMovedtoSignedDoc(movedtosigneddoc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostMovedtoSignedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhysicalDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostPhysicalDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePhysicalDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage UpdateDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaUpdatePhysicalDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GettaggedDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedDeferralinfo(string documentcheckdtl_gid)
        {
            deferraltagged objresult = new deferraltagged();
            objDaAgrTrnPhysicalDocument.DaGettaggedDeferralinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GettaggedHistoryDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedHistoryDeferralinfo(string deferraltagdoc_gid)
        {
            deferraltagged objresult = new deferraltagged();
            objDaAgrTrnPhysicalDocument.DaGettaggedHistoryDeferralinfo(deferraltagdoc_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalMStDeferraltag")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalMStDeferraltag(string documentcheckdtl_gid, string lstype)
        {
            Mstdeferraltag objresult = new Mstdeferraltag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetPhysicalMStDeferraltag(documentcheckdtl_gid, lstype, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // CAD Query ...//
        [ActionName("PostAppcadPhysicalqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcadPhysicalqueryadd(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostAppcadPhysicalqueryadd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAppcadresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostAppcadresponsequery(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostAppcadresponsequery(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalAppcadQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalAppcadQuerySummary(string documentcheckdtl_gid)
        {
            mslcadquerylist objresult = new mslcadquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetPhysicalAppcadQuerySummary(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PhysicalQueryDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PhysicalQueryDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.DaPhysicalQueryDocumentUpload(httpRequest, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
         
        [ActionName("tmpphysicalclearQueryuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpphysicalclearQueryuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DatmpphysicalclearQueryuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearRMuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearRMuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DatmpclearRMuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            objDaAgrTrnPhysicalDocument.DaGetQueryDocument(tagquery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTmpPhysicalQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpPhysicalQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetTmpPhysicalQueryDocument(tagquery_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMSummary()
        {
            customerRMsummarylist objresult = new customerRMsummarylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetRMSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
         

        [ActionName("PostPhysicalInitiateExtensionorwaiver")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalInitiateExtensionorwaiver(mdlinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostPhysicalInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInitiatedExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedExtensionorwaiver(string documentcheckdtl_gid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetInitiatedExtensionorwaiver(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetApprovalExtensionwaiver")]
        [HttpGet]
        public HttpResponseMessage GetApprovalExtensionwaiver(string initiateextendorwaiver_gid)
        {
            mdlapprovaldtllist objresult = new mdlapprovaldtllist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetApprovalExtensionwaiver(objresult, initiateextendorwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostPhysicalextenstionwaiverApproval")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalextenstionwaiverApproval(mdldocumentapprovaldtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostPhysicalextenstionwaiverApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeferralApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalSummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetDeferralApprovalSummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralApprovalHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalHistorySummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetDeferralApprovalHistorySummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostPhysicalDocumentConfirmationDoc")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalDocumentConfirmationDoc(mdldocumentconfirmation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostPhysicalDocumentConfirmationDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhysicalCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalCovenantPeriods(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostPhysicalCovenantPeriods(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalCovenantPeriodsSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalCovenantPeriodsSummary(string groupdocumentdtl_gid)
        {
            mdlscannedcovenantperiodlist objresult = new mdlscannedcovenantperiodlist();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalCovenantPeriodsSummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetcancelPhysicalCovenantPeriod")]
        [HttpPost]
        public HttpResponseMessage GetcancelPhysicalCovenantPeriod(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.DaGetcancelPhysicalCovenantPeriod(values, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralHistorySummary(string groupdocumentdtl_gid)
        {
            deferraltaggedlist objresult = new deferraltaggedlist();
            objDaAgrTrnPhysicalDocument.DaGetDeferralHistorySummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalGeneralInfo(string application_gid)
        {
            mdlscannedgeneral objresult = new mdlscannedgeneral();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalGeneralInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalConfirmationValidation")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalConfirmationValidation(string groupdocumentcheckdtl_gid, string lstype)
        {
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalConfirmationValidation(objresult, groupdocumentcheckdtl_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDuplicateDeferralTagged")]
        [HttpGet]
        public HttpResponseMessage GetDuplicateDeferralTagged(string application_gid)
        {
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.DaGetDuplicateDeferralTagged(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UpdatePhysicalApproval")]
        [HttpGet]
        public HttpResponseMessage UpdatePhysicalApproval(string lstype, string processtypeassign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.DaUpdatePhysicalApproval(objresult, lstype, processtypeassign_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("postPhysicalMakerCheckerConversation")]
        [HttpPost]
        public HttpResponseMessage postPhysicalMakerCheckerConversation(mdlmakercheckerconversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DapostPhysicalMakerCheckerConversation(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMakerCheckerConversation")]
        [HttpGet]
        public HttpResponseMessage GetMakerCheckerConversation(string groupdocumentcheckdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmakercheckerconversationlist objresult = new mdlmakercheckerconversationlist();
            objDaAgrTrnPhysicalDocument.DaGetMakerCheckerConversation(objresult, groupdocumentcheckdtl_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalInstitutionList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalIndividualList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalGrouptoMemberList(string group_gid)
        {
            MdlMstGroupMember values = new MdlMstGroupMember();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaAgrTrnPhysicalDocument.DaGetPhysicalGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetCADPhysicalDocFollowupSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PhysicalDocumentMultiUpload")]
        [HttpPost]
        public HttpResponseMessage PhysicalDocumentMultiUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            physicaluploaddocumentlist documentname = new physicaluploaddocumentlist();
            objDaAgrTrnPhysicalDocument.DaPhysicalDocumentMultiUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetQueryRaisedinfo")]
        [HttpGet]
        public HttpResponseMessage GetQueryRaisedinfo(string documentcheckdtl_gid)
        {
            MdlTagQueryCheckpointList objresult = new MdlTagQueryCheckpointList();
            objDaAgrTrnPhysicalDocument.DaGetQueryRaisedinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetConfirmationValidation")]
        [HttpGet]
        public HttpResponseMessage GetConfirmationValidation(string groupdocumentcheckdtl_gid, string lstype)
        {
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.DaGetConfirmationValidation(objresult, groupdocumentcheckdtl_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostMultipleInitiateExtensionorwaiver")]
        [HttpPost]
        public HttpResponseMessage PostMultipleInitiateExtensionorwaiver(mdlmultipleinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostMultipleInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhysicalDocSent")]
        [HttpPost]
        public HttpResponseMessage DaPostPhysicalDocSent(Mdldocumentsend values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostPhysicalDocSent(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tmpclearQueryuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearQueryuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DatmpclearQueryuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetUpdateCADRaiseQueryStatus")]
        [HttpPost]
        public HttpResponseMessage GetUpdateCADRaiseQueryStatus(mdlcadquerystatusupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetUpdateCADRaiseQueryStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tmpcleartagquerydocument")]
        [HttpGet]
        public HttpResponseMessage tmpcleartagquerydocument(string tagquery_gid)
        {
            result objresult = new result();
            objDaAgrTrnPhysicalDocument.Datmpcleartagquerydocument(tagquery_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetInitiatedApprovalExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedApprovalExtensionorwaiver(string approval_initiationgid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetInitiatedApprovalExtensionorwaiver(objresult, approval_initiationgid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostUpdateTagToDefStatus")]
        [HttpPost]
        public HttpResponseMessage PostUpdateTagToDefStatus(mdlGroupDocStatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaPostUpdateTagToDefStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPendingQueryDelete")]
        [HttpGet]
        public HttpResponseMessage GetPendingQueryDelete(string tagquery_gid)
        {
            result values = new result();
            objDaAgrTrnPhysicalDocument.DaGetPendingQueryDelete(values, tagquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetUpdateDocumentSubmission")]
        [HttpPost]
        public HttpResponseMessage GetUpdateDocumentSubmission(mdlsubmissiondateupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrTrnPhysicalDocument.DaGetUpdateDocumentSubmission(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OriginalcopyVettingDocExport")]
        [HttpPost]
        public HttpResponseMessage OriginalcopyVettingDocExport(MdlCADExportConversation values)
        {

            objDaAgrTrnPhysicalDocument.DaOriginalcopyVettingDocExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}

 