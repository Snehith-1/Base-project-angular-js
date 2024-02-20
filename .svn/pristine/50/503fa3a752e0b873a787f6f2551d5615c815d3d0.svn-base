using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.iasn.Models;
using ems.iasn.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;

namespace ems.isan.Controllers
{
    [RoutePrefix ("api/IasnTrnWorkItem")]
    [Authorize]
    public class IasnTrnWorkItemController : ApiController
    {
        DaIasnTrnWorkItem objDaAccess = new DaIasnTrnWorkItem();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

      
        [ActionName("IsnEmployee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            MdlIsnEmployeelist values = new MdlIsnEmployeelist();           
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetEmployee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName ("WorkItemSummary")] 
        [HttpGet]
        public HttpResponseMessage GetWorkItemSummary()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemSummary( values );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemCounts")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemCounts()
        {
            WorkItemListCount values = new WorkItemListCount();
            objDaAccess.DaGetCountofWorkitems(getsessionvalues.employee_gid, getsessionvalues.user_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemPendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemPendingSummary()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemPendingSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemPushbackSummary")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemPushbackSummary()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemPushbackSummary (values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemForwardSummary")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemForwardSummary()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemForwardSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemCloseSummary")]
        [HttpGet]
        public HttpResponseMessage WorkItemCloseSummary()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemCloseSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemArchivalSummary")]
        [HttpPost]
        public HttpResponseMessage WorkItemArchivalSummary(MdlArchivalCondition objConditions)
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemArchivalSummary(values, objConditions);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyTeamWorkItemSummary")]
        [HttpGet]
        public HttpResponseMessage MyTeamWorkItemSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyTeamWorkItemSummary (getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyWorkItemSummary")]
        [HttpGet]
        public HttpResponseMessage MyWorkItemSummary()
        {
            WorkItemList values = new WorkItemList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMyWorkItemSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WorkItemView")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemview(string lsemail_gid)
        {
            MdlWorkItem values = new MdlWorkItem();
            objDaAccess.DaGetWorkItemview(lsemail_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssignTo")]
        [HttpPost]
        public HttpResponseMessage PostAssignTo(MdlAssignTo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            
            objResult =  objDaAccess.DaPostWoktItemIncharge(values,getsessionvalues.employee_gid,getsessionvalues .user_gid  );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("PostDecision")]
        [HttpPost]
        public HttpResponseMessage DaPostDecision(MdlDecisionhistory values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostDecision(values,getsessionvalues.user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("PostArchival")]
        [HttpPost]
        public HttpResponseMessage DaPostArchival(MdlArchival values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostArchival( getsessionvalues.user_gid,values );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DecisionHistory")]
        [HttpGet]
        public HttpResponseMessage DecisionHistory(string lsemail_gid)
        {
            MdlDecisionhistorySummary values = new MdlDecisionhistorySummary();
            objDaAccess.DaGetDecisionHistory(values,lsemail_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TransferLog")]
        [HttpGet]
        public HttpResponseMessage TransferLog(string lsemail_gid)
        {
            MdlTransferLogList values = new MdlTransferLogList();
            objDaAccess.DaGetTransferLog (lsemail_gid ,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CustomerArchival")]
        [HttpGet]
        public HttpResponseMessage DaGetArchivalCustomer()
        {
            MdlArchivalCustomerList values = new MdlArchivalCustomerList();
            objDaAccess.DaGetCustomerArchival( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SpecificArchival")]
        [HttpGet]
        public HttpResponseMessage SpecificArchival()
        {
            MdlArchivalCustomerList values = new MdlArchivalCustomerList();
            objDaAccess.DaGetSpecificArchival (values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MailAttchment")]
        [HttpPost]
        public HttpResponseMessage conversationdocupload()
        {
            System.Web.HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            objResult=objDaAccess.DaPostUploadAttchment(httpRequest , getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("MailAttchment")]
        [HttpGet]
        public HttpResponseMessage GetMailAttchment()
        {
            MdlcDocList values = new MdlcDocList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetMailAttachmentDoc( values, getsessionvalues.user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

      

        [ActionName("DeleteAttchment")]
        [HttpGet]
        public HttpResponseMessage DeleteAttchment(string mailattachment_gid)
        {
            objResult = objDaAccess.DaPostMailAttchmentDelete(mailattachment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("EmployeeEmailID")]
        [HttpGet]
        public HttpResponseMessage EmployeeEmailID(string employee_gid)
        {
            MdlMailID values = new MdlMailID();
            objDaAccess.DaGetEmployeeMailId (employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Merged")]
        [HttpGet]
        public HttpResponseMessage GetMergedWorkItem(string lsemail_gid)
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetMergedWorkItemview (lsemail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EmployeeProfile")]
        [HttpGet]
        public HttpResponseMessage EmployeeProfile(string employee_gid)
        {
            MdlProfile values = new MdlProfile();
            objDaAccess.EmployeeProfile(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MailSeen")]
        [HttpGet]
        public HttpResponseMessage MailSeen(string email_gid)
        {
            objDaAccess.PostEmailSeen(email_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("ReferenceMail")]
        [HttpGet]
        public HttpResponseMessage ReferenceMail(string email_gid)
        {
            MdlReferenceMailList values = new MdlReferenceMailList();
            objDaAccess.DaGetReferenceMail(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DecisionHistoryMail")]
        [HttpGet]
        public HttpResponseMessage DecisionHistoryMail(string email_gid)
        {
            MdlDecisionhistorySummary values = new MdlDecisionhistorySummary();
            objDaAccess.DaGetDecisionHistoryMail(values, email_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ArchivalCounts")]
        [HttpGet]
        public HttpResponseMessage GetArchivalCounts()
        {
            mdlarchivalcount values = new mdlarchivalcount();
            objDaAccess.DaGetArchivalCounts(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
//Consolidated Page
        [ActionName("ConsolidatedWorkItem")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedWorkItem()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetconsolidatedWorkItem(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssignZone")]
        [HttpPost]
        public HttpResponseMessage AssignZone(MdlAssignTo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objResult = objDaAccess.DaPostAssignZone(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("WorkItemArchivalSummary")]
        [HttpGet]
        public HttpResponseMessage WorkItemArchivalSummary()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaWorkItemArchivalSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

   //Export Excel For Consolidate Report Page
        [ActionName("GetConsolidateExcel")]
        [HttpGet]
        public HttpResponseMessage GetConsolidateExcel()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlConsolidateWorkItem values = new MdlConsolidateWorkItem();
            objDaAccess.DaGetConsolidateExcel(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ComposeMailSummary")]
        [HttpGet]
        public HttpResponseMessage ComposeMailSummary()
        {
            ComposeMailList values = new ComposeMailList();
            objDaAccess.DaComposeMailSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    // Compose Mail
        [ActionName("ComposeMail")]
        [HttpPost]
        public HttpResponseMessage ComposeMail(ComposeMailList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaComposeMail(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
   // Compose Mail Document Upload
        [ActionName("ComposeMailAttachment")]
        [HttpPost]
        public HttpResponseMessage ComposeMailAttachment()
        {
            System.Web.HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            objResult = objDaAccess.DaComposeMailAttachment(httpRequest, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
   // Get Compose Mail Document List
        [ActionName("GetComposeMailAttachment")]
        [HttpGet]
        public HttpResponseMessage GetComposeMailAttachment()
        {
            MdlcDocList values = new MdlcDocList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetComposeMailAttachment(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
  // Compose Mail Document Delete
        [ActionName("DeleteComposeMailAttachment")]
        [HttpGet]
        public HttpResponseMessage DeleteComposeMailAttachment(string composemailattachment_gid)
        {
            objResult = objDaAccess.DaDeleteComposeMailAttachment(composemailattachment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
  // Temp Delete
        [ActionName("Mailtempdelete")]
        [HttpGet]
        public HttpResponseMessage Mailtempdelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaMailtempdelete(getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
 // Compose Mail View
        [ActionName("ComposeMailview")]
        [HttpGet]
        public HttpResponseMessage ComposeMailview(string lscomposemail_gid)
        {
            ComposeMailList values = new ComposeMailList();
            objDaAccess.DaComposeMailview(lscomposemail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ComposeMailDecision")]
        [HttpPost]
        public HttpResponseMessage ComposeMailDecision(MdlArchival values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaComposeMailDecision(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ComposeReferenceMail")]
        [HttpGet]
        public HttpResponseMessage ComposeReferenceMail(string composemail_gid)
        {
            MdlReferenceMailList values = new MdlReferenceMailList();
            objDaAccess.DaComposeReferenceMail(composemail_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("HoldWorkItem")]
        [HttpPost]
        public HttpResponseMessage HoldWorkItem(hold values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaHoldWorkItem(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("HoldLogDetails")]
        [HttpGet]
        public HttpResponseMessage HoldLogDetails(string lsemail_gid, string assigned_flag)
        {
            MdlHoldLogList values = new MdlHoldLogList();
            objDaAccess.DaHoldLogDetails(lsemail_gid, assigned_flag, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AssignedCountList")]
        [HttpGet]
        public HttpResponseMessage AssignedCountList()
        {
            MyWorkItemListCount values = new MyWorkItemListCount();
            objDaAccess.DaAssignedCountList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Customer 
        [ActionName("UpdateCustomer")]
        [HttpPost]
        public HttpResponseMessage UpdateCustomer(MdlArchivalCondition values)
        {
            objDaAccess.DaUpdateCustomer(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

 //Consolidate Report Page
        [ActionName("GetConsolidatedReport")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedReport()
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetConsolidatedReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

 //Export Excel For Consolidated Report
        [ActionName("GetConsolidatedReportExcel")]
        [HttpGet]
        public HttpResponseMessage GetConsolidatedReportExcel(string emailfrom_date, string emailto_date)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlConsolidateWorkItem values = new MdlConsolidateWorkItem();
            objDaAccess.DaGetConsolidatedReportExcel(emailfrom_date, emailto_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
