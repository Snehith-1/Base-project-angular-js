using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.idas.Models;
using ems.idas.DataAccess;


namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasTrnSanctionDoc")]
    [Authorize]
    public class IdasTrnSanctionDocController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnSanctionDoc objDaSanctionDoc = new DaIdasTrnSanctionDoc();

        [ActionName("SanctionDtlsView")]
        [HttpGet]
        public HttpResponseMessage SanctionDtlsView(string sanction_gid)
        {
            MdlSanctionDtlsView objResult = new MdlSanctionDtlsView();
            objDaSanctionDoc.DaGetSanctionDtlsView(sanction_gid ,objResult  );
            return Request.CreateResponse(HttpStatusCode.OK, objResult );
        }


        [ActionName("SanctionDocCreate")]
        [HttpPost]
        public HttpResponseMessage PostSanctionDoc(MdlsanctionDocDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostSanctionDoc(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionDocDelete")]
        [HttpGet]
        public HttpResponseMessage PostSanctionDoc(string sanctiondocument_gid)
        {
            result objResult = new result();
            objDaSanctionDoc.DaPostSanctionDocDelete(sanctiondocument_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("MakerCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage GetMkrChrSummaryr()
        {
            MdlSummaryList objSanctionDtls= new MdlSummaryList();
            objDaSanctionDoc .DaGetMakerCheckerSummary (objSanctionDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objSanctionDtls);
        }

        [ActionName("RmSummary")]
        [HttpGet]
        public HttpResponseMessage RmSummary()
        {
            MdlSummaryList objSanctionDtls = new MdlSummaryList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaGetRmSummary(objSanctionDtls,getsessionvalues .employee_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objSanctionDtls);
        }
        [ActionName("SanctionSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionSummary()
        {
            MdlSanctionDocSummaryList objSanctionDtls = new MdlSanctionDocSummaryList();
           
            objDaSanctionDoc.DaGetSanctionSummary(objSanctionDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objSanctionDtls);
        }

        [ActionName("ScanDocSummary")]
        [HttpGet]
        public HttpResponseMessage GetScannDocSummary(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlScannDocList objSanctionDtls = new MdlScannDocList();
            objDaSanctionDoc.DaGetScanDocSummary(sanction_gid, objSanctionDtls,getsessionvalues .user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objSanctionDtls);
        }

        [ActionName("DocumentConfirmation")]
        [HttpGet]
        public HttpResponseMessage PostDocumentConfirmation(string sanctiondocument_gid, string confirmation_type)
        {
            MdlDocConversation values = new MdlDocConversation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostDocumentConfirmation(getsessionvalues.user_gid, sanctiondocument_gid, confirmation_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DocVerifyMkr")]
        [HttpGet]
        public HttpResponseMessage PostDocVerifyMkr(string sanctiondocument_gid)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostMakerDocVerify(getsessionvalues.user_gid, sanctiondocument_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK,objResult);
        }
        [ActionName("DocVerifyChkr")]
        [HttpGet]
        public HttpResponseMessage PostDocVerifyChkr(string sanctiondocument_gid)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostCheckerDocVerify(getsessionvalues.user_gid, sanctiondocument_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocCadQuery")]
        [HttpPost]
        public HttpResponseMessage DocCadQuery(MdlDocConversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostScannDocQuery(values,getsessionvalues.user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName ("DocRmResponse")]
        [HttpPost ]
        public HttpResponseMessage DocRmResponse(MdlDocConversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostScanDocResponse(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DocNoQueryRmResponse")]
        [HttpGet]
        public HttpResponseMessage DocNoQueryRmResponse(string docconversation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaSanctionDoc.DaPostNoQueryRmResponse(getsessionvalues.user_gid,docconversation_gid,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocScanFinalRemarks")]
        [HttpPost]
        public HttpResponseMessage DocMkrFinalRemarks(MdlScannDocSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostMkrFinalRemarks(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ScanDocConversationInternal")]
        [HttpGet]
        public HttpResponseMessage ScanDocConversationInternal(string sanctiondocument_gid)
        {
            docconlist values = new docconlist();
           
            objDaSanctionDoc.DaGetScanDocConInternal(values, sanctiondocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ScanDocConversationExternal")]
        [HttpGet]
        public HttpResponseMessage ScanDocConversationExternal(string sanctiondocument_gid)
        {
            docconlist values = new docconlist();

            objDaSanctionDoc.DaGetScanDocConExternal(values, sanctiondocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CommonDocUpload")]
        [HttpPost]
        public HttpResponseMessage CommonDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaSanctionDoc.DaPostCommonDocumentUpload(httpRequest, objResult, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetCommonDoc")]
        [HttpGet]
        public HttpResponseMessage Getconversedoc(string sanction_gid)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocumentlist values = new uploaddocumentlist();
            objDaSanctionDoc.DaGetCommonUploadedDocument(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CommonDocDelete")]
        [HttpGet]
        public HttpResponseMessage CommonDocDelete(string commondocument_gid)
        {
            result values = new Models.result ();
            objDaSanctionDoc.DaPostDeleteCommonDocument(commondocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ConversationDocUpload")]
        [HttpPost]
        public HttpResponseMessage conversationdocupload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSanctionDoc.DaPostConverseUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetConverseDoc")]
        [HttpGet]
        public HttpResponseMessage Getconversedoc()
        {
           
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocumentlist values = new uploaddocumentlist();
            objDaSanctionDoc.DaGetconversedoc(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteConverseDoc")]
        [HttpGet]
        public HttpResponseMessage deleteconversedoc(string conversationdocument_gid)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDaSanctionDoc.DaDeleteConverseDoc(conversationdocument_gid, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("RaiseConversation")]
        [HttpPost]
        public HttpResponseMessage DocumnetConversation(MdlDocConversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaPostScannDocQuery(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ScanDocConExport")]
        [HttpPost]
        public HttpResponseMessage ScanDocConExport(MdlExportConversation values)
        {
           
            objDaSanctionDoc.DaGetConReportExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaggedDocList")]
        [HttpGet]
        public HttpResponseMessage GetUnTagDocList(string sanction_gid)
        {
            MdlTaggedDocumentList  objDocumentList = new MdlTaggedDocumentList();
            objDaSanctionDoc.DaGetTaggedDocList(objDocumentList, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objDocumentList);
        }

        [ActionName("GetDocDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetDocDetailsView(string sanctiondocument_gid)
        {
            MdlScannDocSummary values = new MdlScannDocSummary();
            objDaSanctionDoc.DaGetDocDetailsView(values, sanctiondocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostScanDocDate")]
        [HttpPost]
        public HttpResponseMessage PostScanDocDate(MdlScannDocSummary values)
        {
            objDaSanctionDoc.DaPostScanDocumentDate(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

      
        [ActionName("ConversationUpload")]
        [HttpPost]
        public HttpResponseMessage ConversationUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaSanctionDoc.DaPostUpload(httpRequest,getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetDocComments")]
        [HttpGet]
        public HttpResponseMessage GetDocComments(string sanctiondocument_gid)
        {
            MdlsanctionDocDtls values = new MdlsanctionDocDtls();
            objDaSanctionDoc.DaGetDocComments(values, sanctiondocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Bulk Document Verification
        [ActionName("MkrChrBulkDocVerification")]
        [HttpPost]
        public HttpResponseMessage MkrChrBulkDocVerification(MdlBulkverification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionDoc.DaMkrChrBulkDocVerification(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
   
}
