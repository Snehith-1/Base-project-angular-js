using ems.brs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.brs.Dataacess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.brs.Controllers
{
    /// <summary>
    ///The uploaded Pending ticket is showing Unrecon-Management Pending Summary. Assigned ticket showing assigned summary and Re assign ticket showing reassign summary.
    ///Matched and Closed cases showing Closed Summary
    ///In Pending Summary we can adjust/advice the transaction amount or assign any one person.
    ///Assigned person is closed the ticket or send back to the customer.
    /// If the user select Adjust against the Repayment/ Refund  there is no changes in remaining amount.
    ///  If the user select Booked in LMS / FA the remaining amount is reduce based on  user amount.
    ///  remaining is 0.00 the ticket is acknowledge or closed.
    /// </summary>
    /// <remarks>Written by Santhana Kumar </remarks>
    [RoutePrefix("api/UnreconciliationManagement")]
    [Authorize]
    public class UnreconciliationManagementController : ApiController
    {
        DaUnreconciliationManagement objDaUnreconciliationManagement = new DaUnreconciliationManagement();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetunreConcillationSummary")]
        [HttpGet]
        public HttpResponseMessage GetunreConcillationSummary()
        {
            unrecocillationlist values = new unrecocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetunreConcillationSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetunreConreassignpendingSummary")]
        [HttpGet]
        public HttpResponseMessage GetunreConreassignpendingSummary()
        {
            unrecocillationlist values = new unrecocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetunreConreassignpendingSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //[ActionName("GetunreConcillationTagCompleted")]
        //[HttpGet]
        //public HttpResponseMessage GetunreConcillationTagCompleted()
        //{
        //    unrecocillationTaglist values = new unrecocillationTaglist();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaUnreconciliationManagement.DaGetunreConcillationTagCompleted(values, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("GetAllocatedDetail")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedDetail(string banktransc_gid)
        {
            transactionlist values = new transactionlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetAllocatedDetail(banktransc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Post2Assign")]
        [HttpPost]
        public HttpResponseMessage Post2Assign(MdlUnreconciliationManagement objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPost2Assign(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("Post2ReAssign")]
        [HttpPost]
        public HttpResponseMessage DaPost2ReAssign(MdlUnreconciliationManagement objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPost2ReAssign(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetUnReconciliationAssigned")]
        [HttpGet]
        public HttpResponseMessage GetUnReconciliationAssigned()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            transactionlist values = new transactionlist();
            objDaUnreconciliationManagement.DaGetUnReconciliationAssignedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnReconciliationClosed")]
        [HttpGet]
        public HttpResponseMessage GetUnReconciliationClosed()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            transactionlist values = new transactionlist();
            objDaUnreconciliationManagement.DaGetUnReconciliationClosedSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAssignedHistory")]
        [HttpGet]
        public HttpResponseMessage GetAssignedHistory(string banktransc_gid)
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetAssignedHistory(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetunreConcillationCount")]
        [HttpGet]
        public HttpResponseMessage GetunreConcillationCount()
        {
            cocillationlist values = new cocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetunreConcillationCount(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Post2Transfer")]
        [HttpPost]
        public HttpResponseMessage Post2Transfer(MdlUnreconciliationManagement objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPost2Transfer(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("PostManualMatch")]
        [HttpPost]
        public HttpResponseMessage PostManualMatch(cocillationlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPostManualMatch(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTransferredHistory")]
        [HttpGet]
        public HttpResponseMessage GetTransferredHistory(string banktransc_gid)
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetTransferredHistory(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetTransferreddisablestatus")]
        //[HttpGet]
        //public HttpResponseMessage GetTransferreddisablestatus()
        //{
        //    cocillationlist values = new cocillationlist();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaUnreconciliationManagement.DaGetTransferreddisablestatus(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("GetEmployee")]
        [HttpGet]
        public HttpResponseMessage GetEmployee()
        {
            MdlEmployeeExpCurid objMdlEmployee = new MdlEmployeeExpCurid();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetEmployee(objMdlEmployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

         [ActionName("GetReassignEmployee")]
        [HttpGet]
        public HttpResponseMessage GetReassignEmployee(string tagemployee_gid)
        {
            MdlEmployeeExpCurid objMdlEmployee = new MdlEmployeeExpCurid();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetReassignEmployee(objMdlEmployee, getsessionvalues.employee_gid, tagemployee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        [ActionName("GetReassignemployeeLog")]
        [HttpGet]
        public HttpResponseMessage GetReassignemployeeLog(string banktransc_gid)
        {
            unrecocillationTaglist values = new unrecocillationTaglist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetReassignemployeeLog(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSendBackHistory")]
        [HttpGet]
        public HttpResponseMessage GetSendBackHistory(string banktransc_gid)
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetSendBackHistory(values, getsessionvalues.employee_gid, banktransc_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUnreconTransactionDetails")]
        [HttpPost]
        public HttpResponseMessage PostUnreconTransactionDetails(MdlUnreconciliationManagement objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPostUnreconTransactionDetails(objstatus, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetUnreconTransactionList")]
        [HttpGet]
        public HttpResponseMessage GetUnreconTransactionList(string banktransc_gid)
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetUnreconTransactionList(banktransc_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconBankTransactionList")]
        [HttpGet]
        public HttpResponseMessage GetUnreconBankTransactionList(string banktransc_gid)
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetUnreconBankTransactionList(banktransc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBrsUnReconciliationSummary")]
        [HttpGet]
        public HttpResponseMessage GetBrsUnReconciliationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetBrsUnReconciliationSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostUnconciliationStatusUpdation")]
        [HttpPost]
        public HttpResponseMessage PostUnconciliationStatusUpdation(MdlUnreconciliationStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPostUnconciliationStatusUpdation(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnconPendingStatusUpdation")]
        [HttpPost]
        public HttpResponseMessage UnconPendingStatusUpdation(MdlUnreconciliationStatusUpdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaUnconPendingStatusUpdation(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostRMSendback")]
        [HttpPost]
        public HttpResponseMessage PostRMSendback(MdlUnreconciliationStatusUpdate objstatus)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaPostRMSendback(objstatus, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatus);
        }
        [ActionName("GetAllocatedCount")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUnreconciliationStatusUpdate values = new MdlUnreconciliationStatusUpdate();
            objDaUnreconciliationManagement.GetAllocatedCount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDepartmentName")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentName()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUnreconciliationStatusUpdate values = new MdlUnreconciliationStatusUpdate();
            objDaUnreconciliationManagement.DaGetDepartmentName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconTransactionDelete")]
        [HttpGet]
        public HttpResponseMessage GetUnreconTransactionDelete(string unrecontransactiondetails_gid,string banktransc_gid)
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetUnreconTransactionDelete(unrecontransactiondetails_gid,banktransc_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSamfinCustomerSummary")]
        [HttpGet]
        public HttpResponseMessage GetSamfinCustomerSummary()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetSamfinCustomerSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAdjustAdviceEmployeeWiseShow")]
        [HttpGet]
        public HttpResponseMessage GetAdjustAdviceEmployeeWiseShow()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetAdjustAdviceEmployeeWiseShow(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetunreConcillationFinanceSummary")]
        [HttpGet]
        public HttpResponseMessage GetunreConcillationFinanceSummary()
        {
            unrecocillationlist values = new unrecocillationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUnreconciliationManagement.DaGetunreConcillationFinanceSummary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconCreditSummaryManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconCreditSummaryManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconCreditSummaryManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconCreditFinancePendingManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconCreditFinancePendingManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconCreditFinancePendingManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconCreditReassignPendingSummaryManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconCreditReassignPendingSummaryManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconCreditReassignPendingSummaryManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnreconCreditAssignedManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconCreditAssignedManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconCreditAssignedManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnreconCreditClosedManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconCreditClosedManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconCreditClosedManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // debit
        [ActionName("GetUnreconDebitPendingManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconDebitPendingManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconDebitPendingManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconDebitFinancePendingManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconDebitFinancePendingManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconDebitFinancePendingManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUnreconDebitReassignPendingSummaryManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconDebitReassignPendingSummaryManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconDebitReassignPendingSummaryManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnreconDebitAssignedManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconDebitAssignedManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconDebitAssignedManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUnreconDebitClosedManagementExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconDebitClosedManagementExcelExport()
        {
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconDebitClosedManagementExcelExport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetUnreconciliationAssignedSummaryExcelExport")]
        [HttpGet]
        public HttpResponseMessage GetUnreconciliationAssignedSummaryExcelExport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlUnreconciliationManagement values = new MdlUnreconciliationManagement();
            objDaUnreconciliationManagement.DaGetUnreconciliationAssignedSummaryExcelExport(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }

}
