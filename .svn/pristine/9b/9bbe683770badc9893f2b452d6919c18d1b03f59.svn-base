using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rsk.Models;
using ems.rsk.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Newtonsoft.Json.Converters;
using RestSharp;
using ems.storage.Functions;

namespace ems.rsk.Controllers
{
    [RoutePrefix("api/ObservationReport")]
    [Authorize]

    public class ObservationReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaObservationReport objDaObservationReport = new DaObservationReport();
        Fnazurestorage objcmnstorage = new Fnazurestorage();

        [ActionName("GetViewObservationReportDtl")]
        [HttpGet]
        public HttpResponseMessage GetViewObservationReportDtl(string allocationdtl_gid)
        {
            observationreportdtl objreportdtl = new observationreportdtl();
            objDaObservationReport.DaGetViewObservationReportDtl(allocationdtl_gid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }
        [ActionName("ATRReportpdfcontent")]
        [HttpGet]
        public HttpResponseMessage ATRReportpdfcontent(string observation_reportgid)
        {
            atrPDFcontent objatrPDFcontent = new atrPDFcontent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "/ObservationReport/getobservationpdf?observation_reportgid=" + observation_reportgid);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            string path = JsonConvert.DeserializeObject<string>(response.Content);
            var pathArray = path.Split(new string[] { "EMS/" }, StringSplitOptions.None);
            var fileNameArrray = path.Split(new string[] { "Report/" }, StringSplitOptions.None);
            objatrPDFcontent.file_path = pathArray[1].ToString();
            objatrPDFcontent.file_name = fileNameArrray[1].ToString();
            ls_response = objcmnstorage.DaFileUploadDocument(objatrPDFcontent.file_path);
            objatrPDFcontent.file_path = objcmnstorage.EncryptData(objatrPDFcontent.file_path);
            var byName = (IDictionary<string, object>)ls_response;
            objatrPDFcontent.status = (bool)byName["status"];
            return Request.CreateResponse(HttpStatusCode.OK, objatrPDFcontent);

        }

        

        [ActionName("GetViewObservationdtl")]
        [HttpGet]
        public HttpResponseMessage GetViewObservationdtl(string observation_reportgid)
        {
            observationdtl objreportdtl = new observationdtl();
            objDaObservationReport.DaGetViewObservationdtl(observation_reportgid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("GetViewObservationCriticalDtl")]
        [HttpGet]
        public HttpResponseMessage GetViewObservationCriticalDtl(string observation_reportgid)
        {
            criticalobservationlist objreportdtl = new criticalobservationlist();
            objDaObservationReport.DaGetViewObservationCriticalDtl(observation_reportgid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("GetTmpCriticaldtl")]
        [HttpGet]
        public HttpResponseMessage GetTmpCriticaldtl(string allocationdtl_gid)
        {
            criticalobservationlist objreportdtl = new criticalobservationlist();
            objDaObservationReport.DaGetTmpCriticaldtl(allocationdtl_gid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("tmpTier1documentclear")]
        [HttpGet]
        public HttpResponseMessage tmpTier1documentclear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            document objresult = new document();
            objDaObservationReport.DaGetTmpTier1DocumentClear(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("GetDeleteCriticalObser")]
        [HttpGet]
        public HttpResponseMessage GetDeleteCriticalObser(string tmpcritical_observationgid)
        {
            result objreportdtl = new result();
            objDaObservationReport.DaGetDeleteCriticalObser(tmpcritical_observationgid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("GetDeleteTrnCriticalObser")]
        [HttpGet]
        public HttpResponseMessage GetDeleteTrnCriticalObser(string critical_observationgid)
        {
            result objreportdtl = new result();
            objDaObservationReport.GetDeleteTrnCriticalObser(critical_observationgid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("PostObservationReport")]
        [HttpPost]
        public HttpResponseMessage PostObservationReport(observationdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaObservationReport.DaPostObservationReport(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostObservationCritical")]
        [HttpPost]
        public HttpResponseMessage PostObservationCritical(criticalobservation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaObservationReport.DaPostObservationCritical(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostObservationCriticalRemarks")]
        [HttpPost]
        public HttpResponseMessage PostObservationCriticalRemarks(criticalobservationdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaObservationReport.DaPostObservationCriticalRemarks(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostObservationRemarksSubmit")]
        [HttpPost]
        public HttpResponseMessage PostObservationRemarksSubmit(criticalobservationdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaObservationReport.DaPostObservationRemarksSubmit(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetObservationReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetObservationReportSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            observationreportlist objreportdtl = new observationreportlist();
            objDaObservationReport.DaGetObservationReportSummary(getsessionvalues.employee_gid,objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("PostTier1Format")]
        [HttpPost]
        public HttpResponseMessage PostTier1Format(tier1format values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaObservationReport.DaPostTier1Format(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateTier1Format")]
        [HttpPost]
        public HttpResponseMessage PostUpdateTier1Format(tier1format values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaObservationReport.DaPostUpdateTier1Format(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier1FormatDtl")]
        [HttpGet]
        public HttpResponseMessage GetTier1FormatDtl(string observation_reportgid)
        {
            tier1format objreportdtl = new tier1format();
            objDaObservationReport.DaGetTier1FormatDtl(observation_reportgid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

    }
}
