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

namespace StoryboardAPI.Controllers.ems.rsk
{
    [RoutePrefix("api/visitReport")]
    [Authorize]

    public class visitReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaVisitReport objDaVisitReport = new DaVisitReport();

        [ActionName("postVisitReport")]
        [HttpPost]
        public HttpResponseMessage PostVisitReport(visitreport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaPostVisitReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postVisitReportGenerate")]
        [HttpPost]
        public HttpResponseMessage postVisitReportGenerate(visitreport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DapostVisitReportGenerate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getvisitreportdtl")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportDtl(string allocationdtl_gid)
        {
            visitreport objvisitreport = new visitreport();
            objDaVisitReport.DaGetVisitReportDtl(allocationdtl_gid, objvisitreport);
            return Request.CreateResponse(HttpStatusCode.OK, objvisitreport);
        }

        [ActionName("visitReportUpload")]
        [HttpPost]
        public HttpResponseMessage PostvisitReportUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaVisitReport.DaPostvisitReportUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("visitReportPhotoUpload")]
        [HttpPost]
        public HttpResponseMessage PostVisitReportPhoto()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaVisitReport.DaPostVisitReportPhoto(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("visitReportUploadcancel")]
        [HttpGet]
        public HttpResponseMessage GetVisitRptDocumentCancel(string visitreport_documentGid)
        {
            document values = new document();
            objDaVisitReport.DaGetVisitRptDocumentCancel(visitreport_documentGid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("visitReportPhotocancel")]
        [HttpGet]
        public HttpResponseMessage GetVisitRptPhotoCancel(string visitreport_photoGid)
        {
            document values = new document();
            objDaVisitReport.DaGetVisitRptPhotoCancel(visitreport_photoGid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getvisitReportDocument")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportDocument(string allocationdtl_gid)
        {
            visitreportdocumentList objvalues = new visitreportdocumentList();
            objDaVisitReport.DaGetVisitReportDocument(objvalues, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("getvisitReportPhoto")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportPhoto(string allocationdtl_gid)
        {
            visitreportphotoList objvalues = new visitreportphotoList();
            objDaVisitReport.DaGetVisitReportPhoto(objvalues, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetSanctionTenurePeriod")]
        [HttpGet]
        public HttpResponseMessage GetSanctionTenurePeriod(string allocationdtl_gid)
        {
            sanctionloanlist objvalues = new sanctionloanlist();
            objDaVisitReport.DaGetSanctionTenurePeriod(objvalues, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetPreVisitRMUpload")]
        [HttpGet]
        public HttpResponseMessage GetPreVisitRMUpload(string allocationdtl_gid)
        {
            uploaddocument values = new uploaddocument();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaGetPreVisitRMUpload(values,allocationdtl_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostScheduleLog")]
        [HttpPost]
        public HttpResponseMessage PostScheduleLog(schedulelogdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaPostScheduleLog(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCallLog")]
        [HttpPost]
        public HttpResponseMessage PostCallLog(calllogdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaPostCallLog(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllocationLogDetail")]
        [HttpGet]
        public HttpResponseMessage GetAllocationLogDetail(string allocationdtl_gid)
        {
            allocationlogdtl objvalues = new allocationlogdtl();
            objDaVisitReport.DaGetAllocationLogDetail(allocationdtl_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetRMTodayActivity")]
        [HttpGet]
        public HttpResponseMessage GetRMTodayActivity()
        {
            todayactivityList values = new todayactivityList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaGetRMTodayActivity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMCustomerDetails")]
        [HttpGet]
        public HttpResponseMessage GetRMCustomerDetails(string state_gid,string district_gid)
        {
            mycustomerdtllist values = new mycustomerdtllist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaGetRMCustomerDetails(state_gid, district_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMCalenderDtl")]
        [HttpGet]
        public HttpResponseMessage GetRMCalenderDtl()
        {
            calendarevent values = new calendarevent();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaGetRMCalenderDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCallLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetCallLogDetails(string calllog_gid)
        {
            calllogedit values = new calllogedit();
            objDaVisitReport.DaGetEditcalllog(calllog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("patchCallLogUpdate")]
        [HttpPost]
        public HttpResponseMessage patchCallLogUpdate(calllogedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaPostUpdatecalllog(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetScheduleLogDetails(string schedulelog_gid)
        {
            schedulelogedit values = new schedulelogedit();
            objDaVisitReport.DaGetEditschedule(schedulelog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("patchScheduleLogUpdate")]
        [HttpPost]
        public HttpResponseMessage patchScheduleLogUpdate(schedulelogedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReport.DaPostUpdateScheduleLog(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleLogHistory")]
        [HttpGet]
        public HttpResponseMessage GetScheduleLogHistory(string schedulelog_gid)
        {
            schedulelogdtlhistory values = new schedulelogdtlhistory();
            objDaVisitReport.DaGetScheduleLogHistory(values, schedulelog_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostScheduleStatus")]
        [HttpPost]
        public HttpResponseMessage PostScheduleStatus(schedulestatus values)
        {
            objDaVisitReport.DaPostScheduleStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetScheduleInfo")]
        [HttpGet]
        public HttpResponseMessage GetScheduleInfo(string allocationdtl_gid)
        {
            scheduleinfo values = new scheduleinfo();
            objDaVisitReport.DaGetScheduleInfo(allocationdtl_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
