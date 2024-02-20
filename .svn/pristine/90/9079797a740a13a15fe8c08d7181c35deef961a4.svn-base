using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/TeleCalling")]
    [Authorize]
    public class TeleCallingController : ApiController
    {
        DaTeleCalling objDaTeleCalling = new DaTeleCalling();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Mobile No
        [ActionName("PostIBCallMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostIBCallMobileNo(MdlIBCallMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostIBCallMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetIBCallMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetIBCallMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallMobileNo values = new MdlIBCallMobileNo();
            objDaTeleCalling.DaGetIBCallMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }    
       
        [ActionName("IBCallMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage IBCallMobileNoTempList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallMobileNo values = new MdlIBCallMobileNo();
            objDaTeleCalling.DaIBCallMobileNoTempList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallMobileNoList")]
        [HttpGet]
        public HttpResponseMessage IBCallMobileNoList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallMobileNo values = new MdlIBCallMobileNo();
            objDaTeleCalling.DaIBCallMobileNoList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIBCallMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditIBCallMobileNo(string inboundcall2mobileno_gid)
        {
            MdlIBCallMobileNo values = new MdlIBCallMobileNo();
            objDaTeleCalling.DaEditIBCallMobileNo(inboundcall2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIBCallMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateIBCallMobileNo(MdlIBCallMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaUpdateIBCallMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallMobileNoDelete")]
        [HttpGet]
        public HttpResponseMessage IBCallMobileNoDelete(string inboundcall2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallMobileNo values = new MdlIBCallMobileNo();
            objDaTeleCalling.DaIBCallMobileNoDelete(inboundcall2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Email
        [ActionName("PostIBCallEmail")]
        [HttpPost]
        public HttpResponseMessage PostIBCallEmail(MdlIBCallEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostIBCallEmail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIBCallEmailList")]
        [HttpGet]
        public HttpResponseMessage GetIBCallEmailList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallEmail values = new MdlIBCallEmail();
            objDaTeleCalling.DaGetIBCallEmailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallEmailTempList")]
        [HttpGet]
        public HttpResponseMessage IBCallEmailTempList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallEmail values = new MdlIBCallEmail();
            objDaTeleCalling.DaIBCallEmailTempList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallEmailList")]
        [HttpGet]
        public HttpResponseMessage IBCallEmailList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallEmail values = new MdlIBCallEmail();
            objDaTeleCalling.DaIBCallEmailList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIBCallEmail")]
        [HttpGet]
        public HttpResponseMessage EditIBCallEmail(string inboundcall2email_gid)
        {
            MdlIBCallEmail values = new MdlIBCallEmail();
            objDaTeleCalling.DaEditIBCallEmail(inboundcall2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIBCallEmail")]
        [HttpPost]
        public HttpResponseMessage UpdateIBCallEmail(MdlIBCallEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaUpdateIBCallEmail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallEmailDelete")]
        [HttpGet]
        public HttpResponseMessage IBCallEmailDelete(string inboundcall2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallEmail values = new MdlIBCallEmail();
            objDaTeleCalling.DaIBCallEmailDelete(inboundcall2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Follow Up

        [ActionName("PostIBCallFollowUp")]
        [HttpPost]
        public HttpResponseMessage PostIBCallFollowUp(MdlIBCallFollowUp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostIBCallFollowUp(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIBCallFollowUpList")]
        [HttpGet]
        public HttpResponseMessage GetIBCallFollowUpList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallFollowUp values = new MdlIBCallFollowUp();
            objDaTeleCalling.DaGetIBCallFollowUpList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallFollowUpTempList")]
        [HttpGet]
        public HttpResponseMessage IBCallFollowUpTempList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallFollowUp values = new MdlIBCallFollowUp();
            objDaTeleCalling.DaIBCallFollowUpTempList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallFollowUpList")]
        [HttpGet]
        public HttpResponseMessage IBCallFollowUpList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallFollowUp values = new MdlIBCallFollowUp();
            objDaTeleCalling.DaIBCallFollowUpList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIBCallFollowUp")]
        [HttpGet]
        public HttpResponseMessage EditIBCallFollowUp(string inboundcall2followup_gid)
        {
            MdlIBCallFollowUp values = new MdlIBCallFollowUp();
            objDaTeleCalling.DaEditIBCallFollowUp(inboundcall2followup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIBCallFollowUp")]
        [HttpPost]
        public HttpResponseMessage UpdateIBCallFollowUp(MdlIBCallFollowUp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaUpdateIBCallFollowUp(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallFollowUpDelete")]
        [HttpGet]
        public HttpResponseMessage IBCallFollowUpDelete(string inboundcall2followup_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallFollowUp values = new MdlIBCallFollowUp();
            objDaTeleCalling.DaIBCallFollowUpDelete(inboundcall2followup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Address

        [ActionName("PostIBCallAddress")]
        [HttpPost]
        public HttpResponseMessage PostIBCallAddress(MdlIBCallAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostIBCallAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIBCallAddressList")]
        [HttpGet]
        public HttpResponseMessage GetIBCallAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallAddress values = new MdlIBCallAddress();
            objDaTeleCalling.DaGetIBCallAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallAddressTempList")]
        [HttpGet]
        public HttpResponseMessage IBCallAddressTempList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallAddress values = new MdlIBCallAddress();
            objDaTeleCalling.DaIBCallAddressTempList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallAddressList")]
        [HttpGet]
        public HttpResponseMessage IBCallAddressList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallAddress values = new MdlIBCallAddress();
            objDaTeleCalling.DaIBCallAddressList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIBCallAddress")]
        [HttpGet]
        public HttpResponseMessage EditIBCallAddress(string inboundcall2address_gid)
        {
            MdlIBCallAddress values = new MdlIBCallAddress();
            objDaTeleCalling.DaEditIBCallAddress(inboundcall2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIBCallAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateIBCallAddress(MdlIBCallAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaUpdateIBCallAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallAddressDelete")]
        [HttpGet]
        public HttpResponseMessage IBCallAddressDelete(string inboundcall2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCallAddress values = new MdlIBCallAddress();
            objDaTeleCalling.DaIBCallAddressDelete(inboundcall2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //IB Call
        [ActionName("IBCallSave")]
        [HttpPost]
        public HttpResponseMessage IBCallSave(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallSubmit")]
        [HttpPost]
        public HttpResponseMessage IBCallSubmit(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIBCall")]
        [HttpGet]
        public HttpResponseMessage EditIBCall(string inboundcall_gid)
        {
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaEditIBCall(inboundcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallEditSave")]
        [HttpPost]
        public HttpResponseMessage IBCallEditSave(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallEditSave(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallEditSubmit")]
        [HttpPost]
        public HttpResponseMessage IBCallEditSubmit(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallEditSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallEditUpdate")]
        [HttpPost]
        public HttpResponseMessage IBCallEditUpdate(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallEditUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallTempClear")]
        [HttpGet]
        public HttpResponseMessage IBCallTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaTeleCalling.DaIBCallTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetCompletedIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompletedIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetCompletedIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetRejectedIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetRejectedIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetFollowUpIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetFollowUpIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetFollowUpIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetClosedIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetClosedIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetClosedIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIBCallAssignedView")]
        [HttpGet]
        public HttpResponseMessage GetIBCallAssignedView(string inboundcall_gid)
        {

            MdlIBCallView values = new MdlIBCallView();
            objDaTeleCalling.DaGetIBCallAssignedView(inboundcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIBCallReportView")]
        [HttpGet]
        public HttpResponseMessage GetIBCallReportView(string inboundcall_gid)
        {

            MdlIBCallView values = new MdlIBCallView();
            objDaTeleCalling.DaGetIBCallReportView(inboundcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Transfer

        [ActionName("IBCallDetailsForTransfer")]
        [HttpGet]
        public HttpResponseMessage IBCallDetailsForTransfer(string inboundcall_gid)
        {
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaIBCallDetailsForTransfer(inboundcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallTransferEmployee")]
        [HttpPost]
        public HttpResponseMessage IBCallTransferEmployee(MdlIBCallTransfer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallTransferEmployee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Employee Side

        [ActionName("GetEmpAssignedIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpAssignedIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetEmpAssignedIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpInProgressIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpInProgressIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetEmpInProgressIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpTaggedSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpTaggedSummary()
        {
            MdlIBCall values = new MdlIBCall();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaGetEmpTaggedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpTransferredIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpTransferredIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetEmpTransferredIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpFollowUpIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpFollowUpIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DGetEmpFollowUpIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpCompletedIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpCompletedIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetEmpCompletedIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //GetEmpRejectedIBCallSummary
        [ActionName("GetEmpRejectedIBCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpRejectedIBCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetEmpRejectedIBCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("IBCallCount")]
        [HttpGet]
        public HttpResponseMessage IBCallCount()
        {
            IBCallCount values = new IBCallCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmployeeIBCallCount")]
        [HttpGet]
        public HttpResponseMessage EmployeeIBCallCount()
        {
            IBCallCount values = new IBCallCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaEmployeeIBCallCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Completed Details
        [ActionName("GetIBCallCompletedView")]
        [HttpGet]
        public HttpResponseMessage GetIBCallCompletedView(string inboundcall_gid)
        {

            MdlIBCallcompleteView values = new MdlIBCallcompleteView();
            objDaTeleCalling.DaGetIBCallCompletedView(inboundcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Acknowledgement API
        [ActionName("PostUpdateAck")]
        [HttpPost]
        public HttpResponseMessage PostUpdateAck(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostUpdateAck(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Reject
        [ActionName("RejectIBCall")]
        [HttpPost]
        public HttpResponseMessage RejectIBCall(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaRejectIBCall(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCompletedCall")]
        [HttpPost]
        public HttpResponseMessage PostCompletedCall(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostCompletedCall(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCloseCall")]
        [HttpPost]
        public HttpResponseMessage PostCloseCall(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaPostCloseCall(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Common Page
        [ActionName("GetAssignedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetAssignedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetAssignedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // CompletedIBCallSummary
        [ActionName("GetCompletedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompletedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetCompletedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // RejectedIBCallSummary
        [ActionName("GetRejectedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetRejectedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // FollowUpIBCallSummary
        [ActionName("GetFollowUpCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetFollowUpCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetFollowUpCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // ClosedIBCallSummary
        [ActionName("GetClosedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetClosedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetClosedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBAssignedCallCount")]
        [HttpGet]
        public HttpResponseMessage IBAssignedCallCount()
        {
            IBCallCount values = new IBCallCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBAssignedCallCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CallProofDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CallProofDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            callproofuploaddocument documentname = new callproofuploaddocument();
            objDaTeleCalling.DaCallProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("IBCallProofDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage IBCallProofDocumentTmpList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objDaTeleCalling.DaIBCallProofDocumentTmpList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallProofDocumentList")]
        [HttpGet]
        public HttpResponseMessage IBCallProofDocumentList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objDaTeleCalling.DaIBCallProofDocumentList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallProofDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage IBCallProofDocumentDelete(string ibcallproofdocupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument objfilename = new callproofuploaddocument();
            objDaTeleCalling.DaIBCallProofDocumentDelete(ibcallproofdocupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        [ActionName("CallRecordingDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CallRecordingDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            callproofuploaddocument documentname = new callproofuploaddocument();
            objDaTeleCalling.DaCallRecordingDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("IBCallRecordingDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage IBCallRecordingDocumentTmpList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objDaTeleCalling.DaIBCallRecordingDocumentTmpList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallRecordingDocumentList")]
        [HttpGet]
        public HttpResponseMessage IBCallRecordingDocumentList(string inboundcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objDaTeleCalling.DaIBCallRecordingDocumentList(inboundcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IBCallRecordingDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage IBCallRecordingDocumentDelete(string ibcallrecordingocupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument objfilename = new callproofuploaddocument();
            objDaTeleCalling.DaIBCallRecordingDocumentDelete(ibcallrecordingocupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        [ActionName("IBCallDocTempClear")]
        [HttpGet]
        public HttpResponseMessage IBCallDocTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaTeleCalling.DaIBCallDocTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TeleCallingReportSummary")]
        [HttpGet]
        public HttpResponseMessage GetBankReportSummary()
        {
            TeleCallingReport objTeleCallingReport = new TeleCallingReport();
            objDaTeleCalling.DaGetTeleCallingReportSummary(objTeleCallingReport);
            return Request.CreateResponse(HttpStatusCode.OK, objTeleCallingReport);
        } 

        [ActionName("ExportTelecallingReport")]
        [HttpGet]
        public HttpResponseMessage GetExportBankReport()
        {
            TeleCallingReport objTeleCallingReport = new TeleCallingReport();
            objDaTeleCalling.DaExportTelecallingReport(objTeleCallingReport);
            return Request.CreateResponse(HttpStatusCode.OK, objTeleCallingReport);
        }
        [ActionName("GetEntity")]
        [HttpGet]
        public HttpResponseMessage GetEntity()
        {
            MdlIBCall values = new MdlIBCall();
            objDaTeleCalling.DaGetEntity(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IBCallAssignedClosed")]
        [HttpPost]
        public HttpResponseMessage IBCallAssignedClosed(MdlIBCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTeleCalling.DaIBCallAssignedClosed(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}