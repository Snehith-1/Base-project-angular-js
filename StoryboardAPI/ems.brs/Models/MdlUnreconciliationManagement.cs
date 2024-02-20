using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
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
    public class MdlUnreconciliationManagement:result

    {
        public string assigned_to { get; set; }
        public string brs_status { get; set; }
        public string assigned_toname { get; set; }
        public string assigned_remarks { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string banktransc_gid { get; set; }
        public string transfer_to { get; set; }
        public string transfer_toname { get; set; }
        public string transfer_reason { get; set; }
        public string transfer_by { get; set; }
        public string transfered_date { get; set; }
        public string department_name { get; set; }
        public string employee_name { get; set; }
        public string assignby_gid { get; set; }
        public string assignby_name { get; set; }
        public string activity_gid { get; set; }
        public string activity_name { get; set; }
        public string samfincustomer_gid { get; set; }
        public string samfincustomer_name { get; set; }
        public string amount { get; set; }
        public string transaction_remarks { get; set; }
        public string action_name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string remaindingamount_gid { get; set; }
        public string remainding_amount { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
        public List<assignedlist> assignedlist { get; set; }
        public List<transferlist> transferlist { get; set; }
        public List<unrecontransactionlist> unrecontransactionlist { get; set; }
        public List<BrsUnreconciliation_list> BrsUnreconciliation_list { get; set; }

        public List<sendbacklist> sendbacklist { get; set; }
        public List<adjustadviceemployeeshowlist> adjustadviceemployeeshowlist { get; set; }

        public List<adjustadvicelist> adjustadvicelist { get; set; }
        public List<remainingamountlist> remainingamountlist { get; set; }


    }
    public class unrecontransactionlist : result
    {
        public string banktransc_gid { get; set; }
        public string unrecontransactiondetails_gid { get; set; }
        public string department_name { get; set; }
        public string assignby_gid { get; set; }
        public string assignby_name { get; set; }
        public string activity_gid { get; set; }
        public string activity_name { get; set; }
        public string amount { get; set; }
        public string transaction_remarks { get; set; }
        public string action_name { get; set; }
        public string remaining_amount { get; set; }
        public string samfincustomer_name { get; set; }
        public string account_number { get; set; }
        public string transaction_id { get; set; }
        public string urn_no { get; set; }
        public string transaction_date { get; set; }
        public string repayment_date { get; set; }

    }
    public class assignedlist : result
    {

        public string banktransc_gid { get; set; }
        public string taggedmember_gid { get; set; }
        public string taggedmember_name { get; set; }
        public string tagged_remarks { get; set; }
        public string tagged_date { get; set; }
        public string assigned_by { get; set; }
        public string samfincustomer_gid { get; set; }
        public string samfincustomer_name { get; set; }

    }
    public class remainingamountlist
    {
       
        public string banktransc_gid { get; set; }
        public string transact_particulars { get; set; }
    }
    public class adjustadvicelist
    {
        public string adjustadvicedetails_gid { get; set; }
        public string adjustadvice_name { get; set; }
        public string banktransc_gid { get; set; }
        public string transact_particulars { get; set; }
    }
    public class adjustadviceemployeeshowlist
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class sendbacklist : result
    {

        public string banktransc_gid { get; set; }
        public string sendback_by { get; set; }
        public string sendback_date { get; set; }
        public string sendback_reason { get; set; }
      public string remainding_amount { get; set; }
    }
    public class transferlist : result
    {
        public string banktransc_gid { get; set; }
        public string transfer_to { get; set; }
        public string transfer_toname { get; set; }
        public string transfer_reason { get; set; }
        public string transfer_by { get; set; }
        public string transfered_date { get; set; }
    }
    public class unrecocillationlist:result
    {
        public List<unrecocillationlist> unrecocillation_list { get; set; }
        public List<unrecocillationlist> unrecocillationpendingcredit_list { get; set; }
        public List<unrecocillationlist> unrecocillationpendingdebit_list { get; set; }
        public List<unrecocillationfinlist> unrecocillationfinpendingcredit_list { get; set; }
        public List<unrecocillationfinlist> unrecocillationfinpendingdebit_list { get; set; }
        public string aging { get; set; }
        public string created_by { get; set; }
        public string banktransc_gid { get; set; }
        public string trn_date { get; set; }
        public string banktransc_refno { get; set; }
        public string cr_dr { get; set; }
        public string transact_particulars { get; set; }
        public string transact_val { get; set; }
        public string knockoff_status { get; set; }
        public string knockoff_flag { get; set; }
        public string remaining_amount { get; set; }
        public string transaction_aging { get; set; }
        public string tagged_status { get; set; }   
        public string bank_name { get; set; }
        public string knockoffby_finance { get; set; }

    }
    public class unrecocillationfinlist
    {
        public string aging { get; set; }
        public string created_by { get; set; }
        public string banktransc_gid { get; set; }
        public string trn_date { get; set; }
        public string banktransc_refno { get; set; }
        public string cr_dr { get; set; }
        public string transact_particulars { get; set; }
        public string transact_val { get; set; }
        public string knockoff_status { get; set; }
        public string knockoff_flag { get; set; }
        public string remaining_amount { get; set; }
        public string transaction_aging { get; set; }
        public string tagged_status { get; set; }
        public string bank_name { get; set; }
        public string knockoffby_finance { get; set; }
    }
    public class unrecocillationTaglist : result
    {
        public List<unrecocillationTaglist> unrecocillationtag_list { get; set; }

        public string banktransc_gid { get; set; }
        public string trn_date { get; set; }
        public string banktransc_refno { get; set; }
        public string cr_dr { get; set; }
        public string transact_particulars { get; set; }
        public string transact_val { get; set; }
        public string knockoff_status { get; set; }
        public string knockoff_flag { get; set; }
        
        public string tagged_status { get; set; }
        public string bank_name { get; set; }
        public List<reassignemployee_list> reassignemployee_list { get; set; }

    }

    public class MdlEmployeeExpCurid : result
    {
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class reassignemployee_list
    {
        public string banktransc_gid { get; set; }
        public string assigned_toname { get; set; }
        public string tagged_date { get; set; }
        public string assigned_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string rmtagged_remarks { get; set; }

    }
    public class BrsUnreconciliation_list
    {
        public string customer_urn { get; set; }
        public string email_date { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string seen_flag { get; set; }
        public string ticketref_no { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string unreconallocation_gid { get; set; }
        public string tagemployee_gid { get; set; }
        public string banktransc_gid { get; set; }
        public string taggedmember_gid { get; set; }
        public string taggedmember_name { get; set; }
        public string tagged_remarks { get; set; }
        public string tagged_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string trn_date { get; set; }
        public string custref_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string acc_no { get; set; }
        public string transc_balance { get; set; }
        public string tagged_status { get; set; }
        public string operation_status { get; set; }
        public string brs_flag { get; set; }
        public string assigned_date { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string remaining_amount { get; set; }
        public string transact_val { get; set; }
        public string transact_particulars { get; set; }

        public string transactionaging { get; set; }
    }
    public class MdlUnreconciliationStatusUpdate : result
    {
        public string sendback_reason { get; set; }
        public string rm_status { get; set; }
        public string rm_remarks { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string assigned_to { get; set; }
        public string assigned_toname { get; set; }
        public string assigned_remarks { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string transfer_type { get; set; }
        public string tickettransfer_remarks { get; set; }
        public string rmtransfer_remarks { get; set; }
        public string unreconciliation_count { get; set; }
        public List<rmdocument_list> rmdocument_list { get; set; }
        public string[] rmfilename { get; set; }
        public string rmfilepath { get; set; }
        public string banktransc_gid { get; set; }
        public string updation_remarks { get; set; }
        public string transact_val { get; set; }
        public string lsadjusted_amount { get; set; }
        public string cbounreconciliation_status { get; set; }
        public string fileupload_gid { get; set; }
       public string employee_name { get; set; }
        public string department_name { get; set; }


    }
    public class rmdocument_list
    {
        public string fileupload_gid { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }
}