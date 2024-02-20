using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.audit.Controllers
{
    [RoutePrefix("api/AtmRptAuditReports")]
    [Authorize]

    public class AtmRptAuditReportsController : ApiController
    {

        DaAtmRptAuditReports objDaAtmRptAuditReports = new DaAtmRptAuditReports();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetAuditReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditReportSummary()
        {

            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditReportSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetAuditReportSummaryExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetAuditReportSummaryExcelExport()
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditReportSummaryExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualAuditReportSummaryExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetIndividualAuditReportSummaryExcelExport(string auditcreation_gid)
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetIndividualAuditReportSummaryExcelExport(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAuditReports")]
        [HttpGet]
        public HttpResponseMessage EditAuditReports(string auditcreation_gid)
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaEditAuditReport(auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditObservationReportExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetAuditObservationReportExcelExport(string auditcreation_gid)
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditObservationReportExcelExport(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditSampleQueryReportExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetAuditSampleQueryReportExcelExport(string auditcreation_gid)
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditSampleQueryReportExcelExport(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditSampleReportExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetAuditSampleReportExcelExport(string auditcreation_gid)
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditSampleReportExcelExport(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditObservationSampleReportExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetAuditObservationSampleReportExcelExport(string auditcreation_gid)
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditObservationSampleReportExcelExport(values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditReportExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetAuditReportExcelExport()
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditReportExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitSamfinCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitSamfinCustomerSummary()
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditVisitSamfinCustomerSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitSamagroCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitSamagroCustomerSummary()
        {
            MdlAtmRptAuditReports values = new MdlAtmRptAuditReports();
            objDaAtmRptAuditReports.DaGetAuditVisitSamagroCustomerSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSubmitAuditVisitReport")]
        [HttpPost]
        public HttpResponseMessage PostSubmitAuditVisitReport(MdlAtmRptAuditVisitReport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostSubmitAuditVisitReport(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditVisitContactNo")]
        [HttpPost]
        public HttpResponseMessage PostAuditVisitContactNo(mstVisitpersoncontact_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditVisitContactNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditPersonDetails")]
        [HttpPost]
        public HttpResponseMessage PostAuditPersonDetails(mstVisitpersondtl_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditPersonDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisittmpContactList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisittmpContactList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaAtmRptAuditReports.DaGetAuditVisittmpContactList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditVisitAddress")]
        [HttpPost]
        public HttpResponseMessage PostAuditVisitAddress(mstVisitpersonaddress_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditVisitAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        [ActionName("PostAuditVisitDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PostAuditVisitDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentList values = new UploadDocumentList();
            objDaAtmRptAuditReports.DaPostAuditVisitDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AuditVisitPhotoUpload")]
        [HttpPost]
        public HttpResponseMessage AuditVisitPhotoUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentList values = new UploadDocumentList();
            objDaAtmRptAuditReports.DaAuditVisitPhotoUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAuditVisittmpDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisittmpDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisittmpDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisittmpPersondtlList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisittmpPersondtlList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisittmpPersondtlList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisittmpAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisittmpAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisittmpAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitReportList")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportList()
        {
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetVisitReportList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteVisittmpDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentList values = new UploadDocumentList();
            objDaAtmRptAuditReports.DaDeleteVisittmpDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteVisittmpPhoto")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpPhoto()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadphotoList values = new UploadphotoList();
            objDaAtmRptAuditReports.DaDeleteVisittmpPhoto(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteVisittmpContact")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpContact()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersoncontact_list values = new mstVisitpersoncontact_list();
            objDaAtmRptAuditReports.DaDeleteVisittmpContact(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteVisittmppersondtl")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmppersondtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaAtmRptAuditReports.DaDeleteVisittmppersondtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteVisittmpAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersonaddress_list values = new mstVisitpersonaddress_list();
            objDaAtmRptAuditReports.DaDeleteVisittmpAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditPersonDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage PostAuditPersonDetailsUpdate(mstVisitpersondtl_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditPersonDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitContactList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitContactList(string auditvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaAtmRptAuditReports.DaGetAuditVisitContactList(getsessionvalues.employee_gid, auditvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAuditVisitpersondtl")]
        [HttpGet]
        public HttpResponseMessage EditAuditVisitpersondtl(string auditvisit2person_gid)
        {
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaAtmRptAuditReports.DaEditAuditVisitpersondtl(auditvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditVisittmpAddressList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditVisittmpAddressList(string auditvisit2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersonaddress_list values = new mstVisitpersonaddress_list();
            objDaAtmRptAuditReports.DaDeleteAuditVisittmpAddressList(auditvisit2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditVisittmpContactList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditVisittmpContactList(string auditvisitperson2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersoncontact_list values = new mstVisitpersoncontact_list();
            objDaAtmRptAuditReports.DaDeleteAuditVisittmpContactList(auditvisitperson2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditVisittmppersondtlList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditVisittmppersondtlList(string auditvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaAtmRptAuditReports.DaDeleteAuditVisittmppersondtlList(auditvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportDtls")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportDtls(string auditvisit_gid)
        {

            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportDtls(auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitDocumentList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitDocumentList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAuditVisitReport")]
        [HttpGet]
        public HttpResponseMessage EditAuditVisitReport(string auditvisit_gid)
        {
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaEditAuditVisitReport(auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitPersondtlList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitPersondtlList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitPersondtlList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitAddressList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitAddressList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitPhotosList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitPhotosList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitPhotosList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAuditVisitaddress")]
        [HttpGet]
        public HttpResponseMessage EditAuditVisitaddress(string auditvisit2address_gid)
        {
            mstVisitpersonaddress_list values = new mstVisitpersonaddress_list();
            objDaAtmRptAuditReports.DaEditAuditVisitaddress(auditvisit2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAuditVisitPersondtlList")]
        [HttpGet]
        public HttpResponseMessage GetEditAuditVisitPersondtlList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetEditAuditVisitPersondtlList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitedplace")]
        [HttpGet]
        public HttpResponseMessage GetVisitedplace()
        {
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetVisitedplace(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAuditVisitContactList")]
        [HttpGet]
        public HttpResponseMessage GetEditAuditVisitContactList(string auditvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objDaAtmRptAuditReports.DaGetEditAuditVisitContactList(getsessionvalues.employee_gid, auditvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisittmpPhotosList")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisittmpPhotosList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisittmpPhotosList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditVisittmpPhotoList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditVisittmpPhotoList(string auditvisit2photo_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadphotoList values = new UploadphotoList();
            objDaAtmRptAuditReports.DaDeleteAuditVisittmpPhotoList(auditvisit2photo_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditVisitReport")]
        [HttpPost]
        public HttpResponseMessage PostAuditVisitReport(MdlAtmRptAuditVisitReport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditVisitReport(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUpdateAuditVisitReportUpdate")]
        [HttpPost]
        public HttpResponseMessage PostUpdateAuditVisitReportUpdate(MdlAtmRptAuditVisitReport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostUpdateAuditVisitReportUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditVisitReportApproval")]
        [HttpPost]
        public HttpResponseMessage PostAuditVisitReportApproval(MdlAtmRptAuditVisitReport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditVisitReportApproval(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditVisitReportApprovalUpdate")]
        [HttpPost]
        public HttpResponseMessage PostAuditVisitReportApprovalUpdate(MdlAtmRptAuditVisitReport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditVisitReportApprovalUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAuditVisitAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEditAuditVisitAddressList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetEditAuditVisitAddressList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAuditVisitPhotoList")]
        [HttpGet]
        public HttpResponseMessage GetEditAuditVisitPhotoList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetEditAuditVisitPhotoList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAuditVisitDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetEditAuditVisitDocumentList(string auditvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetEditAuditVisitDocumentList(getsessionvalues.employee_gid, auditvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportApprovalPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportApprovalPendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportApprovalPendingSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportApprovalApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportApprovalApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportApprovalApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportManagementApprovedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportManagementApprovedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportManagementApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportManagementPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportManagementPendingSummary()
        {
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportManagementPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAuditVisitReportCounts")]
        [HttpGet]
        public HttpResponseMessage GetAuditVisitReportCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaGetAuditVisitReportCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteAuditVisittmpDocumentList")]
        [HttpGet]
        public HttpResponseMessage DeleteAuditVisittmpDocumentList(string auditvisit2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaDeleteAuditVisittmpDocumentList(auditvisit2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAuditPersonaddressUpdate")]
        [HttpPost]
        public HttpResponseMessage PostAuditPersonaddressUpdate(mstVisitpersonaddress_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmRptAuditReports.DaPostAuditPersonaddressUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ExportAuditVisitPendingReport")]
        [HttpGet]
        public HttpResponseMessage ExportAuditVisitPendingReport()
        {
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaExportAuditVisitPendingReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ExportAuditVisitApprovedReport")]
        [HttpGet]
        public HttpResponseMessage ExportAuditVisitApprovedReport()
        {
            MdlAtmRptAuditVisitReport values = new MdlAtmRptAuditVisitReport();
            objDaAtmRptAuditReports.DaExportAuditVisitApprovedReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
