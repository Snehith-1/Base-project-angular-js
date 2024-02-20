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
    [RoutePrefix("api/IdasTrnPhyDoc")]
    [Authorize]
    public class IdasTrnPhyDocController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnPhysicalDoc objDaPhyDoc = new DaIdasTrnPhysicalDoc();
        result objResult = new result();

        [ActionName("PhysicalDocumentPendingSummary")]
        [HttpGet]
        public HttpResponseMessage PhysicalDocumentPendingSummary()
        {
            MdlPhyDocSummaryList objDaAccess = new MdlPhyDocSummaryList();
            objDaPhyDoc.DaGetPhyDocPendingSummary(objDaAccess);
            return Request.CreateResponse(HttpStatusCode.OK, objDaAccess);
        }
        [ActionName("PhysicalDocumentCreatedSummary")]
        [HttpGet]
        public HttpResponseMessage PhysicalDocumentCreatedSummary()
        {
            MdlPhyDocSummaryList objDaAccess = new MdlPhyDocSummaryList();
            objDaPhyDoc.DaGetPhyDocCreatedSummary(objDaAccess);
            return Request.CreateResponse(HttpStatusCode.OK, objDaAccess);
        }

        [ActionName("GetPhyUnVerifiedCount")]
        [HttpGet]
        public HttpResponseMessage GetPhyUnVerifiedCount(string sanction_gid)
        {
            MdlPhyDocUnverifiedCount objDaAccess = new MdlPhyDocUnverifiedCount();
            objDaPhyDoc.DaGetPhyDocUnVerifiedCount(sanction_gid,objDaAccess);
            return Request.CreateResponse(HttpStatusCode.OK, objDaAccess);
        }


        [ActionName("PhyDocVerify")]
        [HttpGet]
        public HttpResponseMessage PhyDocVerify(string sanctiondocument_gid)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPhyDoc.DaPostPhyDocVerify(getsessionvalues.user_gid, sanctiondocument_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocPhyFinalRemarks")]
        [HttpPost]
        public HttpResponseMessage DocPhyFinalRemarks(MdlScannDocSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPhyDoc .DaPostPhyFinalRemarks(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DocBatch")]
        [HttpPost]
        public HttpResponseMessage DocBatch(MdlBatch values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPhyDoc .DaPostBatch(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhyDocDate")]
        [HttpPost]
        public HttpResponseMessage PostPhyDocDate(MdlScannDocSummary values)
        {
            objDaPhyDoc .DaPostPhyDocumentDate(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PhyDocCadQuery")]
        [HttpPost]
        public HttpResponseMessage DocCadQuery(MdlDocConversation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPhyDoc.DaPostPhyDocQuery(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("NoQuery")]
        [HttpGet]
        public HttpResponseMessage NoQuery(string sanctiondocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPhyDoc.DaPostNoQuery(sanctiondocument_gid, getsessionvalues.user_gid,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);

        }

        [ActionName("PhyDocConversationExternal")]
        [HttpGet]
        public HttpResponseMessage ScanDocConversationExternal(string sanctiondocument_gid)
        {
            docconlist values = new docconlist();

            objDaPhyDoc .DaGetPhyDocConExternal(values, sanctiondocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPhyDocumentType")]
        [HttpPost]
        public HttpResponseMessage PostTypeOfCopy(types_of_copy values)
        {

            objDaPhyDoc.DaPostTypesOfCopy(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Bulk Document Verification
        [ActionName("PostBulkDocVerification")]
        [HttpPost]
        public HttpResponseMessage PostBulkDocVerification(MdlBulkverification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaPhyDoc.DaPostBulkDocVerification(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("FutureDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            result values = new result();
            objDaPhyDoc.DaFutureDateCheck(date, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostLSAStatus")]
        [HttpPost]
        public HttpResponseMessage PostLSAStatus(MdlDocConversation values)
        {
            objDaPhyDoc.DaPostLSAStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
