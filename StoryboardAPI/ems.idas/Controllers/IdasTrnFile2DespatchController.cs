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
    [RoutePrefix("api/IdasTrnFile2Despatch")]
    [Authorize]
    public class IdasTrnFile2DespatchController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnFile2Despatch objDaCartonBox = new DaIdasTrnFile2Despatch();
        result objResult = new result();

        [ActionName("BatchSummary")]
        [HttpGet]
        public HttpResponseMessage GetBatch()
        {
            batchlist objBatchDtls = new batchlist();
            objDaCartonBox.DaGetBatch(objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("TaggedBatchDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetTaggedBatchDtls(string cartonbox_gid)
        {
            batchlist objBatchDtls = new batchlist();
            objDaCartonBox.DaGetTaggedBatchDtls(cartonbox_gid,objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }
        [ActionName("BoxDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetBoxDtls(string cartonbox_gid)
        {
            MdlCartonBox objBatchDtls = new MdlCartonBox();
            objDaCartonBox.DaGetBoxDtls(objBatchDtls,cartonbox_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("TaggedBoxDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetTaggedBoxDtls(string despatch_gid)
        {
            CartonBoxlist objBatchDtls = new CartonBoxlist();
            objDaCartonBox.DaGetTaggedBoxDtls(objBatchDtls, despatch_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("DespatchDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetDespatchDtls(string despatch_gid)
        {
            MdlDespatch objBatchDtls = new MdlDespatch();
            objDaCartonBox.DaGetDespatchDtls(objBatchDtls, despatch_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("DespatchDocument")]
        [HttpGet]
        public HttpResponseMessage DaGetDespatchDocument(string despatch_gid)
        {
            uploaddocumentlist objBatchDtls = new uploaddocumentlist();
            objDaCartonBox.DaGetDespatchDocument(despatch_gid,objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("CartonBoxSummary")]
        [HttpGet]
        public HttpResponseMessage GetCartonBoxSummary()
        {
            CartonBoxlist objBatchDtls = new CartonBoxlist();
            objDaCartonBox.DaGetCartonBoxSummary(objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("DespatchSummary")]
        [HttpGet]
        public HttpResponseMessage GetDespatchSummary()
        {
            DespatchList  objBatchDtls = new DespatchList();
            objDaCartonBox.DaGetDespatchSummary(objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("CreateCartonBox")]
        [HttpPost]
        public HttpResponseMessage PostCartonBoxCreate(MdlCartonBox values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCartonBox.DaPostCartonBoxCreate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateDespatch")]
        [HttpPost]
        public HttpResponseMessage PostCreateDespatch(MdlDespatch  values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCartonBox.DaPostCreateDespatch(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetFileCount")]
        [HttpGet]
        public HttpResponseMessage GetFileCount()
        {
            MdlFilecount objBatchDtls = new MdlFilecount();
            objDaCartonBox.DaGetFileCount(objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("BatchStampNo")]
        [HttpPost]
        public HttpResponseMessage PostBatchStamp(MdlBatchStamp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCartonBox.DaPostUpdateBatchStamp(values,objResult ,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult );

        }

        [ActionName("EditBarCode")]
        [HttpGet]
        public HttpResponseMessage EditBarCode(string sanction_gid)
        {
            MdlbatchSummary objBatchDtls = new MdlbatchSummary();
            objDaCartonBox.DaEditBarCode(sanction_gid, objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        [ActionName("UpdateBarCode")]
        [HttpPost]
        public HttpResponseMessage UpdateBarCode(MdlBatchStamp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCartonBox.DaUpdateBarCode(values, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("BatchPendingSummary")]
        [HttpGet]
        public HttpResponseMessage BatchPendingSummary()
        {
            batchlist objBatchDtls = new batchlist();
            objDaCartonBox.DaBatchPendingSummary(objBatchDtls);
            return Request.CreateResponse(HttpStatusCode.OK, objBatchDtls);
        }

        //Batch Export Excel
        [ActionName("BatchExportExcel")]
        [HttpGet]
        public HttpResponseMessage BatchExportExcel()
        {
            MdlbatchSummary values = new MdlbatchSummary();
            objDaCartonBox.DaBatchExportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BatchImportExcel")]
        [HttpPost]
        public HttpResponseMessage BatchImportExcel()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlbatchSummary values = new MdlbatchSummary();
            objDaCartonBox.DaBatchImportExcel(httpRequest, getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
