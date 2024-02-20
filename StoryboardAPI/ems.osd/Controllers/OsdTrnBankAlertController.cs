using ems.osd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.osd.Models;
using System.Net.Http;
using System.Net;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdTrnBankAlert")]
    [AllowAnonymous]
    public class OsdTrnBankAlertController : ApiController
    {
        DaOsdTrnBankAlert objDaAccess = new DaOsdTrnBankAlert();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //[ActionName("PostMailContent")]
        //[HttpPost]
        //public HttpResponseMessage PostMailContent()
        //{
        //    result values = new result();
        //    var bodyStream = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
        //    bodyStream.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
        //    var bodyText = bodyStream.ReadToEnd();
        //    var bodyText = bodyStream.ReadToEnd();


        //    // return bodyText;
        //    objDaAccess.DaPostMailContent(values, bodyText);
        //    return Request.CreateResponse(HttpStatusCode.OK );
        //}
        [ActionName("GetServiceReqDtl")]
        [HttpGet]
        public HttpResponseMessage GetServiceReqDtl(string bankalert2allocated_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();
            objDaAccess.DaGetServiceReqDtl(values, bankalert2allocated_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEmployee values = new MdlEmployee();
            objDaAccess.DaGetEmployee(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();
            objDaAccess.DaGetEmployeeName(getsessionvalues.user_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllocatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetAllocatedSummary( values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocatedAssignedSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedAssignedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetAllocatedAssignedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMTransferSummary")]
        [HttpGet]
        public HttpResponseMessage GetRMTransferSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetRMTransferSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocatedDtl")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedDtl(string bankalert2allocated_gid, string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();         
            objDaAccess.DaGetAllocatedDtl(bankalert2allocated_gid, customer_gid,values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocatedDetail")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedDtl(string bankalert2allocated_gid, string customer_gid, string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();
            objDaAccess.DaGetAllocatedDetail(bankalert2allocated_gid, customer_gid, customer_urn, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetNotAllocatedDtl")]
        [HttpGet]
        public HttpResponseMessage GetNotAllocatedDtl(string bankalert2notallocated_gid)
        {
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();
            objDaAccess.DaGetNotAllocatedDtl(bankalert2notallocated_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("RMDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage RMDocumentUpload()
        {
            System.Web.HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objresult = new result();
            objDaAccess.DaRMDocumentUpload(httpRequest, getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostRMStatus")]
        [HttpPost]
        public HttpResponseMessage PostRMUpdation(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostRMUpdation(objstatus,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("PostRMSendback")]
        [HttpPost]
        public HttpResponseMessage PostRMSendback(MdlSendbacktobrs objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostRMSendback(objstatus, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("PostRHapproval")]
        [HttpPost]
        public HttpResponseMessage PostRHapproval(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostRHapproval(objstatus, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetRMUploadedDoc")]
        [HttpGet]
        public HttpResponseMessage GetRMUploadedDoc()
        {
            MdlcDocList values = new MdlcDocList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetRMUploadedDoc(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteRMUploadedDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteRMUploadedDoc(string fileupload_gid)
        {
            result objResult = new result();
            objDaAccess.DaDeleteRMUploadedDoc(fileupload_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    
        [ActionName("GetRMStatusDtl")]
        [HttpGet]
        public HttpResponseMessage GetRMStatusDtl(string bankalert2allocated_gid )
        {
            MdlViewRMStatus values = new MdlViewRMStatus();
            objDaAccess.DaGetRMStatusDtl(bankalert2allocated_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBAMpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetBAMpendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetBAMpendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBAMtransferSummary")]
        [HttpGet]
        public HttpResponseMessage GetBAMtransferSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetBAMtransferSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBAMDtlpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetBAMDtlpendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetBAMDtlpendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBAMRHApprovalpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetBAMRHApprovalpendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetBAMRHApprovalpendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetBAMOperationpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetBAMOperationpendingSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetBAMOperationpendingSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Post2AssignOperationTeamandAckComplete")]
        [HttpPost]
        public HttpResponseMessage Post2AssignOperationTeamandAckComplete(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPost2AssignOperationTeamandAckComplete(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("Post2OperationTeam")]
        [HttpPost]
        public HttpResponseMessage Post2OperationTeam(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPost2OperationTeam(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("Posttranasfe2Assign")]
        [HttpPost]
        public HttpResponseMessage Posttranasfe2Assign(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPosttranasfe2Assign(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetOperationStatusDtl")]
        [HttpGet]
        public HttpResponseMessage GetOperationStatusDtl(string bankalert2allocated_gid)
        {
            MdlRMStatus values = new MdlRMStatus();
            objDaAccess.DaGetOperationStatusDtl(bankalert2allocated_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCount")]
        [HttpGet]
        public HttpResponseMessage GetCount()
        {
            MdlBankAlertCount values = new MdlBankAlertCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetCount(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMTempDelete")]
        [HttpGet]
        public HttpResponseMessage GetRMTempDelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetRMTempDelete(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOperationTempDelete")]
        [HttpGet]
        public HttpResponseMessage GetOperationTempDelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetOperationTempDelete(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteOperationUploadedDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteOperationUploadedDoc(string fileupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument objResult = new uploaddocument();
            objDaAccess.DaDeleteOperationUploadedDoc(fileupload_gid, objResult, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("Posttransfer")]
        [HttpPost]
        public HttpResponseMessage Posttransfer(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPosttransfer( getsessionvalues.employee_gid, objstatus);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetticketTransferLog")]
        [HttpGet]
        public HttpResponseMessage GetticketTransferLog(string bankalert2allocated_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();
            objDaAccess.DaGetticketTransferLog(values, bankalert2allocated_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocatedCount")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertCount values = new MdlBankAlertCount();
            objDaAccess.GetAllocatedCount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomer2RM")]
        [HttpGet]
        public HttpResponseMessage GetCustomer2RM(string bankalert2allocated_gid)
        {
            MdlOsdTrnBankAlert values = new MdlOsdTrnBankAlert();
            objDaAccess.DaGetCustomer2RM(bankalert2allocated_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRM")]
        [HttpPost]
        public HttpResponseMessage PostRM(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostRM(getsessionvalues.employee_gid, objstatus);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetSeenHistory")]
        [HttpGet]
        public HttpResponseMessage GetSeenHistory()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankNotification values = new MdlBankNotification();
            objDaAccess.DaGetSeenHistory( getsessionvalues.employee_gid,getsessionvalues.user_gid,  values);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        [ActionName("GetBankalertNotification")]
        [HttpGet]
        public HttpResponseMessage GetBankalertNotification()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankNotification values = new MdlBankNotification();
            objDaAccess.DaGetBankalertNotification(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetServiceprivilege")]
        [HttpGet]
        public HttpResponseMessage GetServiceprivilege()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankNotification values = new MdlBankNotification();
            objDaAccess.DaGetServiceprivilege(getsessionvalues.user_gid,getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBAMOperationCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetBAMOperationCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetBAMOperationCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertCompleted values = new MdlBankAlertCompleted();
            objDaAccess.DaGetCompletedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnReconciliationSummary")]
        [HttpGet]
        public HttpResponseMessage GetUnReconciliationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBankAlertAllocated values = new MdlBankAlertAllocated();
            objDaAccess.DaGetUnReconciliationSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnReconDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage UnReconDocumentUpload()
        {
            System.Web.HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objresult = new result();
            objDaAccess.DaUnReconDocumentUpload(httpRequest, getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("PostUnconciliationStatusUpdation")]
        [HttpPost]
        public HttpResponseMessage PostUnconciliationStatusUpdation(MdlRMStatus objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostUnconciliationStatusUpdation(objstatus, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("DeleteUnReconUploadedDoc")]
        [HttpGet]
        public HttpResponseMessage DeleteUnReconUploadedDoc(string fileupload_gid)
        {
            result objResult = new result();
            objDaAccess.DaDeleteUnReconUploadedDoc(fileupload_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("GetUnReconUploadedDoc")]
        [HttpGet]
        public HttpResponseMessage GetUnReconUploadedDoc(string banktransc_gid)
        {
            MdlcDocList values = new MdlcDocList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetUnReconUploadedDoc(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetunreconAllocatedDetail")]
        [HttpGet]
        public HttpResponseMessage GetunreconAllocatedDetail(string bankalert2allocated_gid, string customer_gid, string customer_urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOsdTrnunreconBankAlert values = new MdlOsdTrnunreconBankAlert();
            objDaAccess.DaGetunreconAllocatedDetail(bankalert2allocated_gid, customer_gid, customer_urn, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
