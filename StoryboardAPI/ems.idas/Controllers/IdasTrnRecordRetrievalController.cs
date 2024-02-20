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
    [RoutePrefix("api/IdasTrnRecordRetrieval")]
    [Authorize]
    public class IdasTrnRecordRetrievalController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnRecordRetrieval objDaAccess = new DaIdasTrnRecordRetrieval();
        result objResult = new result();

        [ActionName("CreateRetrievalReq")]
        [HttpPost]
        public HttpResponseMessage GetDocumentList(MdlIdasRecordRequest objRecordRetrieval)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult= objDaAccess.DaPostRetrievalRequest(getsessionvalues.user_gid, objRecordRetrieval);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("RetrievalRequestDocDtls")]
        [HttpPost]
        public HttpResponseMessage DaPostRetrievalRequestDocDtls(MdlIdasRecordDtls objRecordRetrieval)
        {
            MdlTrnRequiredlist values = new MdlTrnRequiredlist();
             objDaAccess.DaGetReteivalReqBatchDtls(objRecordRetrieval,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRequestDtls")]
        [HttpGet]
        public HttpResponseMessage DaDeleteRequestDtls(string tmpretrievalrequestdtls_gid=null)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaDeleteRequestDtls(tmpretrievalrequestdtls_gid,getsessionvalues.user_gid,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("TmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage DaTmpDocumentDelete(string uploaddocument_gid=null)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaTmpDocumentDelete(uploaddocument_gid, getsessionvalues.user_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetRetrievalReqSummary")]
        [HttpGet]
        public HttpResponseMessage DaGetRetrievalReqSummary()
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasRecordReqSummarylist objResult =new MdlIdasRecordReqSummarylist();
            objDaAccess.DaGetRetrievalReqSummary(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetRetrievalTempSummary")]
        [HttpGet]
        public HttpResponseMessage GetRetrievalTempSummary()
        {

            MdlIdasRecordReceivedSummaryList objResult = new MdlIdasRecordReceivedSummaryList();
            objDaAccess.DaGetRetrievalTemporarySummary(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetRetrievalPermanentSummary")]
        [HttpGet]
        public HttpResponseMessage DaGetRetrievalPermanentSummary()
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasRecordReceivedSummaryList objResult = new MdlIdasRecordReceivedSummaryList();
            objDaAccess.DaGetRetrievalPermanentSummary(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetReDespatchedSummary")]
        [HttpGet]
        public HttpResponseMessage GetReDespatchedSummary()
        { 
            MdlReDespatchSummaryList objResult = new MdlReDespatchSummaryList();
            objDaAccess.DaGetReDespatchSummary(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("RetrievalReqView")]
        [HttpGet]
        public HttpResponseMessage RetrievalReqView(string retrievalrequest_gid)
        {
            MdlRecordReqView objResult = new MdlRecordReqView();
            objDaAccess.DaGetRetrievalReqView(objResult, retrievalrequest_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ReDespatchView360")]
        [HttpGet]
        public HttpResponseMessage ReDespatchView360(string redespatch_gid)
        {
            MdlRedespatch360 objResult = new MdlRedespatch360();
            objDaAccess.DaGetReDespatchView(redespatch_gid,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("UploadDocument")]
        [HttpPost]
        public HttpResponseMessage CommonDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
         
            objDaAccess.DaPostUploadDocument(httpRequest, objResult, getsessionvalues. user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetTmpUploadDocument")]
        [HttpGet]
        public HttpResponseMessage DaGetTmpUploadDocument()
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasUploadDocumentList objResult = new MdlIdasUploadDocumentList();
            objDaAccess.DaGetTmpUploadDocument(objResult,getsessionvalues .user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DaGetUploadDocument")]
        [HttpGet]
        public HttpResponseMessage DaGetUploadDocument(string retrievalapprovadoc_gid)
        {
            MdlIdasUploadDocumentList objResult = new MdlIdasUploadDocumentList();
            objDaAccess.DaGetUploadDocument(objResult, retrievalapprovadoc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }


        [ActionName("Ensure")]
        [HttpGet]
        public HttpResponseMessage Ensure(string retrievalrequestdtls_gid)
        {
           objResult = objDaAccess.DaPostEnsure(retrievalrequestdtls_gid);
           return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("BatchList")]
        [HttpPost]
        public HttpResponseMessage BatchList(MdlReferene values)
        {
            MdlBatchList objResult = new MdlBatchList();
            //string[] reference= { };
            //if (reference_value!=null)
            //{
            //    reference = System.Text.RegularExpressions.Regex.Split(reference_value, ":");
            //}else
            //{
            //    reference =new string[2]{ "null","null"};
            //}
            objDaAccess.DaGetBatchList(objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ReconciliationCount")]
        [HttpPost]
        public HttpResponseMessage ReconciliationCount(MdlReferene values)
        {
            MdlReconciliationCount  objResult = new MdlReconciliationCount();
            //string[] reference = { };
            //if (reference_value != null)
            //{
            //    reference = System.Text.RegularExpressions.Regex.Split(reference_value, ":");
            //}
            //else
            //{
            //    reference = new string[2] { "null", "null" };
            //}
            objDaAccess.DaGetReconciliationCount (objResult, values );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocReceivedDtls")]
        [HttpPost]
        public HttpResponseMessage DaPostDocReceived(MdlPostDocDtlReceived values)
        {
          
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostDocReceivedDtls(values,getsessionvalues .user_gid ,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocReceived")]
        [HttpPost]
        public HttpResponseMessage DocReceived(MdlDocumentReceived values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostDocReceived(values,objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetDocReceived")]
        [HttpGet]
        public HttpResponseMessage DaGetDocReceived(string retrievalrequest_gid)
        {

            MdlDocumentReceived values = new MdlDocumentReceived();
            objDaAccess.DaGetDocReceived(retrievalrequest_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DespatchCustomer")]
        [HttpGet]
        public HttpResponseMessage DaGetDespatchCustomer()
        {
            MdlCustomerList  values = new MdlCustomerList();
            objDaAccess.DaGetDespatchedCusomer(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ReDespatch")]
        [HttpPost]
        public HttpResponseMessage DaPostReDespatch(MdlRedespatch values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostDocReDespatched( getsessionvalues .user_gid ,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DespatchBox")]
        [HttpGet]
        public HttpResponseMessage DaGetDespatchBox()
        {
            MdlBoxList  values = new Models.MdlBoxList ();
            objDaAccess.DaGetBoxList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
