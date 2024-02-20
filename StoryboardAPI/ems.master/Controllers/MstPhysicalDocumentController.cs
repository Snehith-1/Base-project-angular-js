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
    [RoutePrefix("api/MstPhysicalDocument")]
    [Authorize]

    public class MstPhysicalDocumentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstPhysicalDocument objDaMstPhysicalDocument = new DaMstPhysicalDocument();

        [ActionName("GetCADPhysicalDocMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocMakerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocFollowupMakerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupMakerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocFollowupMakerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocCheckerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocApprovalCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocApprovalCheckerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocApprovalCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocFollowupCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupCheckerSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocFollowupCheckerSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocApproverSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCADPhysicalDocFollowupApproverSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupApproverSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocFollowupApproverSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADPhysicalDocCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocCompletedSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CADAppPhysicalDocCount")]
        [HttpGet]
        public HttpResponseMessage CADAppPhysicalDocCount()
        {
            CadScannedCount values = new CadScannedCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaCADAppPhysicalDocCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADTrnPhysicalDocList")]
        [HttpGet]
        public HttpResponseMessage GetCADTrnPhysicalDocList(string credit_gid, string application_gid)
        {
            PhysicalDocTaggedDocumentList values = new PhysicalDocTaggedDocumentList();
            objDaMstPhysicalDocument.DaGetCADTrnPhysicalDocList(values, credit_gid, application_gid);
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
            objDaMstPhysicalDocument.DaPhysicalDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        // end - changed //

        [ActionName("GetPhysicalDocument")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocument(string documentcheckdtl_gid)
        {
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaMstPhysicalDocument.DaGetPhysicalDocument(documentcheckdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMScannedDocument")]
        [HttpGet]
        public HttpResponseMessage GetRMScannedDocument(string documentcheckdtl_gid, string signeddocument_flag)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            scanneduploaddocumentlist values = new scanneduploaddocumentlist();
            objDaMstPhysicalDocument.DaGetRMScannedDocument(documentcheckdtl_gid, signeddocument_flag, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         

        [ActionName("cancelphysicaluploaddocument")]
        [HttpGet]
        public HttpResponseMessage cancelphysicaluploaddocument(string physicaldocument_gid)
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.Dacancelphysicaluploaddocument(physicaldocument_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        //[ActionName("GetTempScannedDocument")]
        //[HttpGet]
        //public HttpResponseMessage GetTempScannedDocument(string documentcheckdtl_gid)
        //{
        //    scanneduploaddocumentlist values = new scanneduploaddocumentlist();
        //    objDaMstPhysicalDocument.DaGetScannedDocument(documentcheckdtl_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("PostMovedtoSignedDoc")]
        [HttpPost]
        public HttpResponseMessage PostMovedtoSignedDoc(movedtosigneddoc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostMovedtoSignedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhysicalDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostPhysicalDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePhysicalDeferralTaggedDoc")]
        [HttpPost]
        public HttpResponseMessage UpdateDeferralTaggedDoc(deferraltagged values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaUpdatePhysicalDeferralTaggedDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GettaggedDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedDeferralinfo(string documentcheckdtl_gid)
        {
            deferraltagged objresult = new deferraltagged();
            objDaMstPhysicalDocument.DaGettaggedDeferralinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GettaggedHistoryDeferralinfo")]
        [HttpGet]
        public HttpResponseMessage GettaggedHistoryDeferralinfo(string deferraltagdoc_gid)
        {
            deferraltagged objresult = new deferraltagged();
            objDaMstPhysicalDocument.DaGettaggedHistoryDeferralinfo(deferraltagdoc_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalMStDeferraltag")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalMStDeferraltag(string documentcheckdtl_gid, string lstype)
        {
            Mstdeferraltag objresult = new Mstdeferraltag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetPhysicalMStDeferraltag(documentcheckdtl_gid, lstype, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // CAD Query ...//
        [ActionName("PostAppcadPhysicalqueryadd")]
        [HttpPost]
        public HttpResponseMessage PostAppcadPhysicalqueryadd(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostAppcadPhysicalqueryadd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAppcadresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostAppcadresponsequery(mdlcadquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostAppcadresponsequery(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalAppcadQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalAppcadQuerySummary(string documentcheckdtl_gid)
        {
            mslcadquerylist objresult = new mslcadquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetPhysicalAppcadQuerySummary(objresult, documentcheckdtl_gid);
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
            objDaMstPhysicalDocument.DaPhysicalQueryDocumentUpload(httpRequest, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
         
        [ActionName("tmpphysicalclearQueryuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpphysicalclearQueryuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DatmpphysicalclearQueryuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearRMuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearRMuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DatmpclearRMuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            objDaMstPhysicalDocument.DaGetQueryDocument(tagquery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTmpPhysicalQueryDocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpPhysicalQueryDocument(string tagquery_gid)
        {
            querydocumentlist values = new querydocumentlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetTmpPhysicalQueryDocument(tagquery_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMSummary()
        {
            customerRMsummarylist objresult = new customerRMsummarylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetRMSummary(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
         

        [ActionName("PostPhysicalInitiateExtensionorwaiver")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalInitiateExtensionorwaiver(mdlinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostPhysicalInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInitiatedExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedExtensionorwaiver(string documentcheckdtl_gid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetInitiatedExtensionorwaiver(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetApprovalExtensionwaiver")]
        [HttpGet]
        public HttpResponseMessage GetApprovalExtensionwaiver(string initiateextendorwaiver_gid)
        {
            mdlapprovaldtllist objresult = new mdlapprovaldtllist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetApprovalExtensionwaiver(objresult, initiateextendorwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostPhysicalextenstionwaiverApproval")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalextenstionwaiverApproval(mdldocumentapprovaldtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostPhysicalextenstionwaiverApproval(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeferralApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalSummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetDeferralApprovalSummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralApprovalHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralApprovalHistorySummary()
        {
            mdldeferralapprovallist objresult = new mdldeferralapprovallist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetDeferralApprovalHistorySummary(objresult, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostPhysicalDocumentConfirmationDoc")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalDocumentConfirmationDoc(mdldocumentconfirmation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostPhysicalDocumentConfirmationDoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhysicalCovenantPeriods")]
        [HttpPost]
        public HttpResponseMessage PostPhysicalCovenantPeriods(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostPhysicalCovenantPeriods(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalCovenantPeriodsSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalCovenantPeriodsSummary(string groupdocumentdtl_gid)
        {
            mdlscannedcovenantperiodlist objresult = new mdlscannedcovenantperiodlist();
            objDaMstPhysicalDocument.DaGetPhysicalCovenantPeriodsSummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetcancelPhysicalCovenantPeriod")]
        [HttpPost]
        public HttpResponseMessage GetcancelPhysicalCovenantPeriod(mdlscannedcovenantperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaMstPhysicalDocument.DaGetcancelPhysicalCovenantPeriod(values, objresult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeferralHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralHistorySummary(string groupdocumentdtl_gid)
        {
            deferraltaggedlist objresult = new deferraltaggedlist();
            objDaMstPhysicalDocument.DaGetDeferralHistorySummary(objresult, groupdocumentdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalGeneralInfo")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalGeneralInfo(string application_gid)
        {
            mdlscannedgeneral objresult = new mdlscannedgeneral();
            objDaMstPhysicalDocument.DaGetPhysicalGeneralInfo(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalConfirmationValidation")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalConfirmationValidation(string groupdocumentcheckdtl_gid, string lstype)
        {
            result objresult = new result();
            objDaMstPhysicalDocument.DaGetPhysicalConfirmationValidation(objresult, groupdocumentcheckdtl_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDuplicateDeferralTagged")]
        [HttpGet]
        public HttpResponseMessage GetDuplicateDeferralTagged(string application_gid)
        {
            result objresult = new result();
            objDaMstPhysicalDocument.DaGetDuplicateDeferralTagged(objresult, application_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("UpdatePhysicalApproval")]
        [HttpGet]
        public HttpResponseMessage UpdatePhysicalApproval(string lstype, string processtypeassign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objresult = new result();
            objDaMstPhysicalDocument.DaUpdatePhysicalApproval(objresult, lstype, processtypeassign_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("postPhysicalMakerCheckerConversation")]
        [HttpPost]
        public HttpResponseMessage postPhysicalMakerCheckerConversation(mdlmakercheckerconversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DapostPhysicalMakerCheckerConversation(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMakerCheckerConversation")]
        [HttpGet]
        public HttpResponseMessage GetMakerCheckerConversation(string groupdocumentcheckdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmakercheckerconversationlist objresult = new mdlmakercheckerconversationlist();
            objDaMstPhysicalDocument.DaGetMakerCheckerConversation(objresult, groupdocumentcheckdtl_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPhysicalInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalInstitutionList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstPhysicalDocument.DaGetPhysicalInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalIndividualList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objDaMstPhysicalDocument.DaGetPhysicalIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalGrouptoMemberList(string group_gid)
        {
            MdlMstGroupMember values = new MdlMstGroupMember();
            objDaMstPhysicalDocument.DaGetPhysicalGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalGroupSummary(string application_gid)
        {
            MdlMstGroup values = new MdlMstGroup();
            objDaMstPhysicalDocument.DaGetPhysicalGroupSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalDocApprovalDtls")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalDocApprovalDtls(string application_gid)
        {
            MdlScannedDocApprovalDetails values = new MdlScannedDocApprovalDetails();
            objDaMstPhysicalDocument.DaGetPhysicalDocApprovalDtls(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhysicalDocSent")]
        [HttpPost]
        public HttpResponseMessage DaPostPhysicalDocSent(Mdldocumentsend values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaMstPhysicalDocument.DaPostPhysicalDocSent(getsessionvalues.user_gid, values);
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
            objDaMstPhysicalDocument.DaPhysicalDocumentMultiUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetCADPhysicalDocFollowupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADPhysicalDocFollowupSummary()
        {
            physicalmakerapplicationlist values = new physicalmakerapplicationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetCADPhysicalDocFollowupSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUpdateDocumentSubmission")]
        [HttpPost]
        public HttpResponseMessage GetUpdateDocumentSubmission(mdlsubmissiondateupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetUpdateDocumentSubmission(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetQueryRaisedinfo")]
        [HttpGet]
        public HttpResponseMessage GetQueryRaisedinfo(string documentcheckdtl_gid)
        {
            MdlTagQueryCheckpointList objresult = new MdlTagQueryCheckpointList();
            objDaMstPhysicalDocument.DaGetQueryRaisedinfo(documentcheckdtl_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("tmpclearQueryuploaded")]
        [HttpGet]
        public HttpResponseMessage tmpclearQueryuploaded()
        {
            result objresult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DatmpclearQueryuploaded(getsessionvalues.user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetPendingQueryDelete")]
        [HttpGet]
        public HttpResponseMessage GetPendingQueryDelete(string tagquery_gid)
        {
            result values = new result();
            objDaMstPhysicalDocument.DaGetPendingQueryDelete(values, tagquery_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUpdateCADRaiseQueryStatus")]
        [HttpPost]
        public HttpResponseMessage GetUpdateCADRaiseQueryStatus(mdlcadquerystatusupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetUpdateCADRaiseQueryStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tmpcleartagquerydocument")]
        [HttpGet]
        public HttpResponseMessage tmpcleartagquerydocument(string tagquery_gid)
        {
            result objresult = new result();
            objDaMstPhysicalDocument.Datmpcleartagquerydocument(tagquery_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("PostMultipleInitiateExtensionorwaiver")]
        [HttpPost] 
        public HttpResponseMessage PostMultipleInitiateExtensionorwaiver(mdlmultipleinitiateextendwaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostMultipleInitiateExtensionorwaiver(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateTagToDefStatus")]
        [HttpPost]
        public HttpResponseMessage PostUpdateTagToDefStatus(mdlGroupDocStatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaPostUpdateTagToDefStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetConfirmationValidation")]
        [HttpGet]
        public HttpResponseMessage GetConfirmationValidation(string groupdocumentcheckdtl_gid, string lstype)
        {
            result objresult = new result();
            objDaMstPhysicalDocument.DaGetConfirmationValidation(objresult, groupdocumentcheckdtl_gid, lstype);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetInitiatedApprovalExtensionorwaiver")]
        [HttpGet]
        public HttpResponseMessage GetInitiatedApprovalExtensionorwaiver(string approval_initiationgid)
        {
            mdlinitiateextendwaiverlist objresult = new mdlinitiateextendwaiverlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetInitiatedApprovalExtensionorwaiver(objresult, approval_initiationgid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("OriginalcopyVettingDocExport")]
        [HttpPost]
        public HttpResponseMessage OriginalcopyVettingDocExport(MdlCADExportConversation values)
        {

            objDaMstPhysicalDocument.DaOriginalcopyVettingDocExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPhysicalAppcadQuerySummaryRm")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalAppcadQuerySummaryRm(string documentcheckdtl_gid)
        {
            mslcadquerylist objresult = new mslcadquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstPhysicalDocument.DaGetPhysicalAppcadQuerySummaryRm(objresult, documentcheckdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}

 