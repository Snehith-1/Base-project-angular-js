using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;
using ems.ecms.DataAccess;
using ems.utilities.Functions;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using ems.utilities.Models;
namespace ems.ecms.Controllers
{
    /// <summary>
    /// Deferral Controller Class containing API methods for accessing the  DataAccess class DaDeferral 
    /// Create defferal, show deferral records, set deferral to loan, show rm details in a table, usercode of employee, export excel of npa, export excel for defferal,
    /// 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/deferral")]
    [Authorize]
    public class DeferralController : ApiController
    {
      
        DaDeferral objDaDeferral = new DaDeferral();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("deferral")]
        [HttpGet]
        public HttpResponseMessage Deferral()
        {
            deferral objDeferral = new deferral();
            objDaDeferral.DaGetDeferral(objDeferral);
            return Request.CreateResponse(HttpStatusCode.OK, objDeferral);
        }

        [ActionName("createDeferral")]
        [HttpPost]
        public HttpResponseMessage CreateDeferral(createDeferral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
         objDaDeferral.DaPostCreateDeferral(values,getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       

        [ActionName("loan2Deferral")]
        [HttpPost]
        public HttpResponseMessage Loan2Deferral(loan2Deferral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral .DaPostLoan2Deferral(values,getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deferralSummary")]
        [HttpGet]
        public HttpResponseMessage GetDeferralSummary(string loan_gid)
        {
            deferralSummary objdeferralsummary = new deferralSummary();
           objDaDeferral.DaGetDeferralSummary(objdeferralsummary, loan_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }
        [ActionName("getrmSummary")]
        [HttpGet]
        public HttpResponseMessage getrmDeferralSummary()
        {
            rmdeferralSummary objdeferralsummary = new rmdeferralSummary();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral .DaGetRMSummary(objdeferralsummary, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }


        [ActionName("rm")]
        [HttpGet]
        public HttpResponseMessage RM()
        {
            deferralSummary objdeferralsummary = new deferralSummary();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaDeferral .DaGetRm(objdeferralsummary, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("UserCode")]
        [HttpGet]
        public HttpResponseMessage UserCode()
        {
            usercode objusercode = new usercode();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral.DaGetUserCode(objusercode, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objusercode);
        }


        [ActionName("rmdeferraldetails")]
        [HttpGet]
        public HttpResponseMessage RMDeferralDetails(string relationshipmgmt_gid)
        {
            deferralSummary objdeferralsummary = new deferralSummary();
             objDaDeferral .DaGetRmDeferralDetails(objdeferralsummary, relationshipmgmt_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("directDeferralSummary")]
        [HttpGet]
        public HttpResponseMessage GetDirectDeferralSummary()
        {
            deferralSummary objdeferralsummary = new deferralSummary();

            objDaDeferral .DaGetrmDeferralSummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("CheckerApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetCheckerApprovalSummary()
        {
            deferralSummary objdeferralsummary = new deferralSummary();
            objDaDeferral.DaGetCheckerApprovalSummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("deferralreportsummary")]
        [HttpGet]
        public HttpResponseMessage DeferralReportsummary()
        {
            deferralSummary objdeferralsummary = new deferralSummary();
               objDaDeferral.DaGetDeferralReportSummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }



        [ActionName("manageDeferralSummary")]
        [HttpGet]
        public HttpResponseMessage ManageDeferralSummary()
        {
            managedeferralSummary objdeferralsummary = new managedeferralSummary();
           objDaDeferral.DaGetManageDeferralSummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("cadApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage cadApprovalSummary()
        {
            managedeferralSummary objdeferralsummary = new managedeferralSummary();
            objDaDeferral.DaGetcadApprovalSummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("reopensummary")]
        [HttpGet]
        public HttpResponseMessage ReOpenSummary()
        {
            rmdeferralSummary objdeferralsummary = new rmdeferralSummary();
            objDaDeferral .DaGetReOpenSummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("directDeferralSummaryview")]
        [HttpGet]
        public HttpResponseMessage GetDirectDeferralSummaryView()
        {
            deferralSummary objdeferralsummary = new deferralSummary();

            objDaDeferral.DaGetRmDeferral(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("reportView")]
        [HttpGet]
        public HttpResponseMessage ReportView()
        {
            deferralSummary objdeferralsummary = new deferralSummary();

           objDaDeferral.DaGetReportView(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("export")]
        [HttpPost]
        public HttpResponseMessage Export(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
           objDaDeferral.DaGetExport(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("excel")]
        [HttpPost]
        public HttpResponseMessage Excel(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
            objDaDeferral.DaGetExcel(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        //NPA Tagged Export
        [ActionName("exportnpa")]
        [HttpPost]
        public HttpResponseMessage GetNPAExport(deferralSummary objdeferralsummary)
        {
            var employee_gid = "";
            objDaDeferral.DaGetNPAExport(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("excelexport")]
        [HttpPost]
        public HttpResponseMessage ExcelExport(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
           objDaDeferral .DaGetExcelExport(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("CheckerExcelExport")]
        [HttpPost]
        public HttpResponseMessage CheckerExcelExport(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
            objDaDeferral.DaGetCheckerExcelExport(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("directDeferralSummaryreport")]
        [HttpPost]
        public HttpResponseMessage DirectDeferralSummaryreport(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
            objDaDeferral .DaGetDirectDeferralReport(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        [ActionName("directDeferralSummaryreportview")]
        [HttpPost]
        public HttpResponseMessage DirectDeferralSummaryreportview(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
            objDaDeferral.DaGetDirectDeferralReport(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }


        [ActionName("reportsummaryview")]
        [HttpPost]
        public HttpResponseMessage Reportsummaryview(deferralSummary objdeferralsummary)
        {

            var employee_gid = "";
           objDaDeferral .DaGetReports(objdeferralsummary, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

       
        [ActionName("UploadDocument")]
        [HttpPost]
        public HttpResponseMessage PostUploadDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaDeferral .DaPostUploaddocuments(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("uploaddeferraldocumentbycad")]
        [HttpPost]
        public HttpResponseMessage PostUploadDocumentbycad()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaDeferral .DaPostDocumentbyCad(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("UploadcadDocument")]
        [HttpPost]
        public HttpResponseMessage UploadcadDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
             objDaDeferral .DaPostUploadcadDocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("getDeferralDocument")]
        [HttpGet]
        public HttpResponseMessage GetDeferralDocument(string deferral_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaDeferral .DaGetDeferralDocuments(documentname, deferral_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("getDeferraldetail")]
        [HttpGet]
        public HttpResponseMessage GetDeferraldetail(string deferral_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaDeferral .DaGetDeferralDetail(documentname, deferral_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("getReopen")]
        [HttpGet]
        public HttpResponseMessage GetReopen(string deferral_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaDeferral.DaGetReOpen(documentname, deferral_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("deferralmasterSubmit")]
        [HttpPost]
        public HttpResponseMessage Create(deferrelmaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral.DaPostInsertDeferral(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deferralmasterSummary")]
        [HttpGet]
        public HttpResponseMessage DeferralSummary()
        {
            deferral values = new deferral();
            objDaDeferral.DaGetDeferral(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




        [ActionName("getdeferralstages")]
        [HttpGet]
        public HttpResponseMessage GetDeferralStages(string deferral_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname values = new UploadDocumentname();
            UploadDocumentList lstvalues = new UploadDocumentList();
           objDaDeferral .DaGetDeferralStages(deferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getcaddoc")]
        [HttpGet]
        public HttpResponseMessage Getcaddoc(string deferral_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname values = new UploadDocumentname();
            UploadDocumentList lstvalues = new UploadDocumentList();
            objDaDeferral .DaGetcaddoc(deferral_gid, values, lstvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("checkerlist")]
        [HttpGet]
        public HttpResponseMessage Getcheckerlist(string deferral_gid)
        {
            UploadDocumentname lstvalues = new UploadDocumentname();
            objDaDeferral.DaGetcheckerlist(deferral_gid, lstvalues);
            return Request.CreateResponse(HttpStatusCode.OK, lstvalues);
        }


        [ActionName("getApprove")]
        [HttpPost]
        public HttpResponseMessage GetApprove(mdldeferralgetapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaDeferral .DaGetApprove(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deferralApprove")]
        [HttpPost]
        public HttpResponseMessage DeferralApprove(mdldeferralgetapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaDeferral .DaPostDeferralApprove(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerVerify")]
        [HttpPost]
        public HttpResponseMessage CheckerVerify(deferralsupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral.DaPostCheckerVerify(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deferralTransfer")]
        [HttpPost]
        public HttpResponseMessage DeferralTransfer(mdldeferralgetapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral .DaGetDeferralTransfer(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerBulkVerify")]
        [HttpPost]
        public HttpResponseMessage CheckerBulkVerify(mdldeferralgetapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral.DaGetCheckerBulkVerify(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("customer2loan")]
        [HttpGet]
        public HttpResponseMessage Customer2loan(string customer_gid)
        {
            mdlcustomer2loan values = new mdlcustomer2loan();
            objDaDeferral.DaGetCustomer2Loan(values,customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("Getdeferralupdate")]
        [HttpGet]
        public HttpResponseMessage Getdeferralupdate(string deferral_gid)
        {
            deferraledit values = new deferraledit();
           objDaDeferral.DaGetEditDeferral(deferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deferralUpdate")]
        [HttpPost]
        public HttpResponseMessage Updatedeferral(deferraledit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral .DaPostUpdateDeferral(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deferralDelete")]
        [HttpGet]
        public HttpResponseMessage Deletedeferral(string deferral_gid)
        {
            deferraledit values = new deferraledit();
            objDaDeferral .DaPostDeleteDeferral(deferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getdeferraldetails")]
        [HttpGet]
        public HttpResponseMessage Getdeferraldetails(string deferral_gid)
        {
            createDeferral values = new createDeferral();
            objDaDeferral .DaGetDeferralDetails(deferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("update")]
        [HttpPost]
        public HttpResponseMessage DeferralsUpdate(deferralsupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDeferral.DaPostDeferralsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deferraldeleterecords")]
        [HttpGet]
        public HttpResponseMessage DeferralDeleteRecords(string deferral_gid)
        {
            deferralsupdate values = new deferralsupdate();
            objDaDeferral .DaPostDeferralDelete(deferral_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UserReport2export")]
        [HttpPost]
        public HttpResponseMessage UserReport2export(deferralSummary objuser2report)
        {
            objDaDeferral.DaUserReport2export(objuser2report);
            return Request.CreateResponse(HttpStatusCode.OK, objuser2report);
        }

        [ActionName("User2reportsummary")]
        [HttpGet]
        public HttpResponseMessage User2reportsummary()
        {
            deferralSummary objdeferralsummary = new deferralSummary();
            objDaDeferral.DaUser2reportsummary(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }

        //[ActionName("User2reportsummarysearch")]
        //[HttpGet]
        //public HttpResponseMessage User2reportsummarysearch()
        //{
        //    deferralSummary objdeferralsummary = new deferralSummary();
        //    objDaDeferral.DaUser2reportsummarysearch(objdeferralsummary);
        //    return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        //}

        [ActionName("User2reportsummarysearch")]
        [HttpPost]
        public HttpResponseMessage User2reportsummarysearch(deferralSummary objdeferralsummary)
        {
            objDaDeferral.DaUser2reportsummarysearch(objdeferralsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdeferralsummary);
        }
    }
}
