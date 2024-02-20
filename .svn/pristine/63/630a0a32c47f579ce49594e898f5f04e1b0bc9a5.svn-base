using ems.businessteam.DataAccess;
using ems.businessteam.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.businessteam.Controllers
{
    [RoutePrefix("api/Marketing")]
    [Authorize]
    public class MarketingController : ApiController
    {
        DaMarketing objMarketing = new DaMarketing();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Mobile No
        [ActionName("PostMarketingCallMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostMarketingCallMobileNo(MdlMarketingCallMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostMarketingCallMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingCallMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallMobileNo values = new MdlMarketingCallMobileNo();
            objMarketing.DaGetMarketingCallMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }    
       
        [ActionName("MarketingCallMobileNoTempList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallMobileNoTempList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallMobileNo values = new MdlMarketingCallMobileNo();
            objMarketing.DaMarketingCallMobileNoTempList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallMobileNoList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallMobileNoList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallMobileNo values = new MdlMarketingCallMobileNo();
            objMarketing.DaMarketingCallMobileNoList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMarketingCallMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCallMobileNo(string marketingcall2mobileno_gid)
        {
            MdlMarketingCallMobileNo values = new MdlMarketingCallMobileNo();
            objMarketing.DaEditMarketingCallMobileNo(marketingcall2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingCallMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingCallMobileNo(MdlMarketingCallMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaUpdateMarketingCallMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallMobileNoDelete")]
        [HttpGet]
        public HttpResponseMessage MarketingCallMobileNoDelete(string marketingcall2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallMobileNo values = new MdlMarketingCallMobileNo();
            objMarketing.DaMarketingCallMobileNoDelete(marketingcall2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //LeadStatus
        [ActionName("PostMarketingCallLeadstatus")]
        [HttpPost]
        public HttpResponseMessage PostMarketingCallLeadstatus(MdlMarketingCallLeadstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostMarketingCallLeadstatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Email
        [ActionName("PostMarketingCallEmail")]
        [HttpPost]
        public HttpResponseMessage PostMarketingCallEmail(MdlMarketingCallEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostMarketingCallEmail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        //api/Marketing/GetMarketingCallLeadstatusList
        [ActionName("GetMarketingCallLeadstatusList")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallLeadstatusList(string marketingcall_gid)
        {

            MdlMarketingCallLeadstatus values = new MdlMarketingCallLeadstatus();
            objMarketing.DaGetMarketingCallLeadstatusList(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingCallEmailList")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallEmailList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallEmail values = new MdlMarketingCallEmail();
            objMarketing.DaGetMarketingCallEmailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallEmailTempList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallEmailTempList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallEmail values = new MdlMarketingCallEmail();
            objMarketing.DaMarketingCallEmailTempList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallEmailList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallEmailList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallEmail values = new MdlMarketingCallEmail();
            objMarketing.DaMarketingCallEmailList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMarketingCallEmail")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCallEmail(string marketingcall2email_gid)
        {
            MdlMarketingCallEmail values = new MdlMarketingCallEmail();
            objMarketing.DaEditMarketingCallEmail(marketingcall2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingCallEmail")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingCallEmail(MdlMarketingCallEmail values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaUpdateMarketingCallEmail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallLeadstatusDelete")]
        [HttpGet]
        public HttpResponseMessage MarketingCallLeadstatusDelete(string marketingcall2leadstatus_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallLeadstatus values = new MdlMarketingCallLeadstatus();
            objMarketing.DaMarketingCallLeadstatusDelete(marketingcall2leadstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallEmailDelete")]
        [HttpGet] 
        public HttpResponseMessage MarketingCallEmailDelete(string marketingcall2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallEmail values = new MdlMarketingCallEmail();
            objMarketing.DaMarketingCallEmailDelete(marketingcall2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //follow up list
        [ActionName("GetMarketingCallMyFollowUpList")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallMyFollowUpList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallFollowUp values = new MdlMarketingCallFollowUp();
            objMarketing.DaGetMarketingCallMyFollowUpList(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




        //Follow Up

        [ActionName("PostMarketingCallFollowUpMg")]
        [HttpPost]
        public HttpResponseMessage PostMarketingCallFollowUpMg(MdlMarketingCallFollowUp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostMarketingCallFollowUpMg(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Follow Up

        [ActionName("PostMarketingCallFollowUp")]
        [HttpPost]
        public HttpResponseMessage PostMarketingCallFollowUp(MdlMarketingCallFollowUp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostMarketingCallFollowUp(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

      

        [ActionName("GetMarketingCallFollowUpList")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallFollowUpList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallFollowUp values = new MdlMarketingCallFollowUp();
            objMarketing.DaGetMarketingCallFollowUpList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallFollowUpTempList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallFollowUpTempList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallFollowUp values = new MdlMarketingCallFollowUp();
            objMarketing.DaMarketingCallFollowUpTempList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallFollowUpList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallFollowUpList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallFollowUp values = new MdlMarketingCallFollowUp();
            objMarketing.DaMarketingCallFollowUpList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMarketingCallFollowUp")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCallFollowUp(string marketingcall2followup_gid)
        {
            MdlMarketingCallFollowUp values = new MdlMarketingCallFollowUp();
            objMarketing.DaEditMarketingCallFollowUp(marketingcall2followup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingCallFollowUp")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingCallFollowUp(MdlMarketingCallFollowUp values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaUpdateMarketingCallFollowUp(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallFollowUpDelete")]
        [HttpGet]
        public HttpResponseMessage MarketingCallFollowUpDelete(string marketingcall2followup_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallFollowUp values = new MdlMarketingCallFollowUp();
            objMarketing.DaMarketingCallFollowUpDelete(marketingcall2followup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Address

        [ActionName("PostMarketingCallAddress")]
        [HttpPost]
        public HttpResponseMessage PostMarketingCallAddress(MdlMarketingCallAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostMarketingCallAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingCallAddressList")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallAddress values = new MdlMarketingCallAddress();
            objMarketing.DaGetMarketingCallAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallAddressTempList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallAddressTempList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallAddress values = new MdlMarketingCallAddress();
            objMarketing.DaMarketingCallAddressTempList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallAddressList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallAddressList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallAddress values = new MdlMarketingCallAddress();
            objMarketing.DaMarketingCallAddressList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMarketingCallAddress")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCallAddress(string marketingcall2address_gid)
        {
            MdlMarketingCallAddress values = new MdlMarketingCallAddress();
            objMarketing.DaEditMarketingCallAddress(marketingcall2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMarketingCallAddress")]
        [HttpPost]
        public HttpResponseMessage UpdateMarketingCallAddress(MdlMarketingCallAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaUpdateMarketingCallAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallAddressDelete")]
        [HttpGet]
        public HttpResponseMessage MarketingCallAddressDelete(string marketingcall2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCallAddress values = new MdlMarketingCallAddress();
            objMarketing.DaMarketingCallAddressDelete(marketingcall2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //IB Callapi/Marketing/MarketingCallSave
        [ActionName("MarketingCallSave")]
        [HttpPost]
        public HttpResponseMessage MarketingCallSave(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MarketingUnassignedLeadSubmit")]
        [HttpPost]
        public HttpResponseMessage MarketingUnassignedLeadSubmit(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingUnassignedLeadSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MarketingCallSubmit")]
        [HttpPost]
        public HttpResponseMessage MarketingCallSubmit(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingLeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetMarketingLeadSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMarketingLeadSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditMarketingCall")]
        [HttpGet]
        public HttpResponseMessage EditMarketingCall(string marketingcall_gid)
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaEditMarketingCall(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallEditSave")]
        [HttpPost]
        public HttpResponseMessage MarketingCallEditSave(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallEditSave(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallEditSubmit")]
        [HttpPost]
        public HttpResponseMessage MarketingCallEditSubmit(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallEditSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallEditUpdate")]
        [HttpPost]
        public HttpResponseMessage MarketingCallEditUpdate(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallEditUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MarketingCallFormUpdate")]
        [HttpPost]
        public HttpResponseMessage MarketingCallFormUpdate(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallFormUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallTempClear")]
        [HttpGet]
        public HttpResponseMessage MarketingCallTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMarketing.DaMarketingCallTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetCompletedMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompletedMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetCompletedMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetRejectedMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetRejectedMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GetFollowUpMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetFollowUpMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetFollowUpMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //
        [ActionName("GetClosedMyleadsMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetClosedMyleadsMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetClosedMyleadsMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //
        [ActionName("GetClosedMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetClosedMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetClosedMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //api/Marketing/GetMarketingCallAssignedView
        [ActionName("GetMarketingCallAssignedView")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallAssignedView(string marketingcall_gid)
        {

            MdlMarketingCallView values = new MdlMarketingCallView();
            objMarketing.DaGetMarketingCallAssignedView(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

      
        [ActionName("GetMarketingCallReportView")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallReportView(string marketingcall_gid)
        {

            MdlMarketingCallView values = new MdlMarketingCallView();
            objMarketing.DaGetMarketingCallReportView(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Transfer

        [ActionName("MarketingCallDetailsForTransfer")]
        [HttpGet]
        public HttpResponseMessage MarketingCallDetailsForTransfer(string marketingcall_gid)
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaMarketingCallDetailsForTransfer(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallTransferEmployee")]
        [HttpPost]
        public HttpResponseMessage MarketingCallTransferEmployee(MdlMarketingCallTransfer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallTransferEmployee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Employee Side

        [ActionName("GetEmpAssignedMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpAssignedMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEmpAssignedMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpInProgressMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpInProgressMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEmpInProgressMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpTaggedSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpTaggedSummary()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaGetEmpTaggedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpTransferredMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpTransferredMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEmpTransferredMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpFollowUpMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpFollowUpMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEmpFollowUpMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmpCompletedMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpCompletedMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEmpCompletedMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEmpRejectedMarketingCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetEmpRejectedMarketingCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEmpRejectedMarketingCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallCount")]
        [HttpGet]
        public HttpResponseMessage MarketingCallCount()
        {
            MarketingCallCount values = new MarketingCallCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmployeeMarketingCallCount")]
        [HttpGet]
        public HttpResponseMessage EmployeeMarketingCallCount()
        {
            MarketingCallCount values = new MarketingCallCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaEmployeeMarketingCallCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Completed Details
        [ActionName("GetMarketingCallCompletedView")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallCompletedView(string marketingcall_gid)
        {

            MdlMarketingCallcompleteView values = new MdlMarketingCallcompleteView();
            objMarketing.DaGetMarketingCallCompletedView(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      

        // Acknowledgement API
        [ActionName("PostUpdateAck")]
        [HttpPost]
        public HttpResponseMessage PostUpdateAck(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostUpdateAck(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Reject
        [ActionName("RejectMarketingCall")]
        [HttpPost]
        public HttpResponseMessage RejectMarketingCall(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaRejectMarketingCall(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCompletedCall")]
        [HttpPost]
        public HttpResponseMessage PostCompletedCall(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostCompletedCall(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCloseCall")]
        [HttpPost]
        public HttpResponseMessage PostCloseCall(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaPostCloseCall(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Common Page
        [ActionName("GetAssignedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetAssignedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetAssignedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // CompletedMarketingCallSummary
        [ActionName("GetCompletedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetCompletedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetCompletedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // RejectedMarketingCallSummary
        [ActionName("GetRejectedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetRejectedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // FollowUpMarketingCallSummary
        [ActionName("GetFollowUpCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetFollowUpCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetFollowUpCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // ClosedMarketingCallSummary
        [ActionName("GetClosedCallSummary")]
        [HttpGet]
        public HttpResponseMessage GetClosedCallSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetClosedCallSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingAssignedCallCount")]
        [HttpGet]
        public HttpResponseMessage MarketingAssignedCallCount()
        {
            MarketingCallCount values = new MarketingCallCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingAssignedCallCount(getsessionvalues.employee_gid, values);
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
            objMarketing.DaCallProofDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("MarketingCallProofDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallProofDocumentTmpList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objMarketing.DaMarketingCallProofDocumentTmpList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("MarketingLeadFormDocumentList")]
        //[HttpGet]
        //public HttpResponseMessage MarketingLeadFormDocumentList(string marketingcall_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    callproofuploaddocument values = new callproofuploaddocument();
        //    objMarketing.DaMarketingLeadFormDocumentList(marketingcall_gid, getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("MarketingCallProofDocumentList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallProofDocumentList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objMarketing.DaMarketingCallProofDocumentList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallProofDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage MarketingCallProofDocumentDelete(string MarketingCallproofdocupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument objfilename = new callproofuploaddocument();
            objMarketing.DaMarketingCallProofDocumentDelete(MarketingCallproofdocupload_gid, objfilename, getsessionvalues.employee_gid);
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
            objMarketing.DaCallRecordingDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("MarketingCallRecordingDocumentTmpList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallRecordingDocumentTmpList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objMarketing.DaMarketingCallRecordingDocumentTmpList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallRecordingDocumentList")]
        [HttpGet]
        public HttpResponseMessage MarketingCallRecordingDocumentList(string marketingcall_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument values = new callproofuploaddocument();
            objMarketing.DaMarketingCallRecordingDocumentList(marketingcall_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingCallRecordingDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage MarketingCallRecordingDocumentDelete(string MarketingCallrecordingocupload_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            callproofuploaddocument objfilename = new callproofuploaddocument();
            objMarketing.DaMarketingCallRecordingDocumentDelete(MarketingCallrecordingocupload_gid, objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }

        [ActionName("MarketingCallDocTempClear")]
        [HttpGet]
        public HttpResponseMessage MarketingCallDocTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMarketing.DaMarketingCallDocTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MarketingReportSummary")]
        [HttpGet]
        public HttpResponseMessage MarketingReportSummary()
        {
            MarketingReport objTeleCallingReport = new MarketingReport();
            objMarketing.DaGetMarketingReportSummary(objTeleCallingReport);
            return Request.CreateResponse(HttpStatusCode.OK, objTeleCallingReport);
        } 

        [ActionName("ExportMarketingReport")]
        [HttpGet]
        public HttpResponseMessage ExportMarketingReport()
        {
            MarketingReport objTeleCallingReport = new MarketingReport();
            objMarketing.DaExportMarketingReport(objTeleCallingReport);
            return Request.CreateResponse(HttpStatusCode.OK, objTeleCallingReport);
        }
        [ActionName("GetEntity")]
        [HttpGet]
        public HttpResponseMessage GetEntity()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEntity(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMarketingSourceofContact")]
        [HttpGet]
        public HttpResponseMessage GetMarketingSourceofContact()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMarketingSourceofContact(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMarketingCallType")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallType()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMarketingCallType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLeadRequestType")]
        [HttpGet]
        public HttpResponseMessage GetLeadRequestType()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetLeadRequestType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMarketingTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage GetMarketingTelecallingFunction()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMarketingTelecallingFunction(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMarketingCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage GetMarketingCallReceivedNumber()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMarketingCallReceivedNumber(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MarketingCallAssignedClosed")]
        [HttpPost]
        public HttpResponseMessage MarketingCallAssignedClosed(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingCallAssignedClosed(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBaselocation")]
        [HttpGet]
        public HttpResponseMessage GetBaselocation(string employee_gid)
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetBaselocation(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanProduct")]
        [HttpGet]
        public HttpResponseMessage GetLoanProduct()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetLoanProduct(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage GetLoanSubProduct()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetLoanSubProduct(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAgrLoanProduct")]
        [HttpGet]
        public HttpResponseMessage GetAgrLoanProduct()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetAgrLoanProduct(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAgrLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage GetAgrLoanSubProduct()
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetAgrLoanSubProduct(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MarketingRejected")]
        [HttpPost]
        public HttpResponseMessage MarketingRejected(MdlMarketingCall values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMarketing.DaMarketingRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentList(string marketingcall_gid)
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetDocumentList(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMilletDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetMilletDocumentList(string marketingcall_gid)
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetMilletDocumentList(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEnquiryDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetEnquiryDocumentList(string marketingcall_gid)
        {
            MdlMarketingCall values = new MdlMarketingCall();
            objMarketing.DaGetEnquiryDocumentList(marketingcall_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}