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
    [RoutePrefix("api/IdasNocAndNdc")]
    [Authorize]
    public class IdasNocAndNdcController : ApiController
    {
        DaIdasNocAndNdc objDaIdasNocAndNdc = new DaIdasNocAndNdc();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Add 
        [ActionName("GetIdasNocAndNdc")]
        [HttpGet]
        public HttpResponseMessage GetIdasNocAndNdc(nocandndc values)
        {
            MdlIdasNocAndNdc objnocandndc = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetIdasNocAndNdc(objnocandndc);
            return Request.CreateResponse(HttpStatusCode.OK, objnocandndc);
        }
    

        [ActionName("CreateIdasNocAndNdc")]
        [HttpPost]
        public HttpResponseMessage CreateIdasNocAndNdc(nocandndc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIdasNocAndNdc.DaCreateIdasNocAndNdc(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Events - Drop Down
        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDropDown values = new MdlDropDown();
            objDaIdasNocAndNdc.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Document Upload
        [ActionName("GetNocDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetNocDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasNocAndNdc values = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetNocDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetNocDocumentEditList")]
        [HttpGet]
        public HttpResponseMessage GetNocDocumentEditList(string nocandndc_gid)
        {
            MdlIdasNocAndNdc values = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetNocDocumentEditList(nocandndc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetNocDocumentTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetNocDocumentTempEditList(string nocandndc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasNocAndNdc values = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetNocDocumentTempEditList(nocandndc_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("NocDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage NocDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlIdasNocAndNdc documentname = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaNocDocumentUpload(httpRequest, documentname, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("TempClear")]
        [HttpGet]
        public HttpResponseMessage TempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            MdlIdasNocAndNdc objvalues = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Overall Delete

        [ActionName("NocandNdcDelete")]
        [HttpGet]
        public HttpResponseMessage NocandNdcDelete(string nocandndc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            MdlIdasNocAndNdc objvalues = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaNocandNdcDelete(nocandndc_gid, values, objvalues, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Document
        [ActionName("GetNocDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GetNocDocumentDelete(string nocandndcdocument_gid, string nocandndc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasNocAndNdc values = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetNocDocumentDelete(nocandndcdocument_gid, values, getsessionvalues.employee_gid, nocandndc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetNocDocumentAddDelete")]
        [HttpGet]
        public HttpResponseMessage GetNocDocumentAddDelete(string nocandndcdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasNocAndNdc documentname = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetNocDocumentAddDelete(nocandndcdocument_gid, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        //Edit

        [ActionName("EditNoc")]
        [HttpGet]
        public HttpResponseMessage EditNoc(string nocandndc_gid)
        {
            MdlIdasNocAndNdc values = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaEditNoc(nocandndc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateNoc")]
        [HttpPost]
        public HttpResponseMessage UpdateNoc(MdlIdasNocAndNdc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaIdasNocAndNdc.DaUpdateNoc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //ExportExcel Report Page

        [ActionName("ExportExcelNoc")]
        [HttpGet]
        public HttpResponseMessage ExportExcelNoc()
        {
            MdlIdasNocAndNdc objnocandndc = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaExportExcelNoc(objnocandndc);
            return Request.CreateResponse(HttpStatusCode.OK, objnocandndc);
        }

        //Report Page Summary
        [ActionName("GetIdasNocReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetIdasNocReportSummary(nocandndc values)
        {
            MdlIdasNocAndNdc objnocandndc = new MdlIdasNocAndNdc();
            objDaIdasNocAndNdc.DaGetIdasNocReportSummary(objnocandndc);
            return Request.CreateResponse(HttpStatusCode.OK, objnocandndc);
        }


    }
}