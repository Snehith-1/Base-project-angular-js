using ems.brs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
  /// <summary>
 ///To upload KOTAK Statement it digitize the LMS trnascation to get matched cases and bank transaction summary
 /// </summary>
 /// <remarks>Written by Motchesh</remarks>
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class MdlKotakReconcillation : result
    {
        public List<bank_list> bank_list { get; set; }
      
    }
    public class bank_list
    {
        public string bank_gid { get; set; }
        public string bank_name { get; set; }

        public string bankpending_count { get; set; }
        public string bankmatched_count { get; set; }

    }
    public class UploadExcel:result
    {
        public List<upload_list> upload_list { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }

    }
}
    public class upload_list
    {
       public string  reconcildoc_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string created_by { get; set; }
         public string created_date { get; set; }
       public string  lspath { get; set; }
        
}
public class transactionlist:result
{
    public List<transactionlist> transaction_list { get; set; }
    public List<BankAlertUnreconciliation_list> BankAlertUnreconciliation_list { get; set; }
    public List<BankAlertUnreconciliation_list> BankAlertUnreconciliationcredit_list { get; set; }
    public List<BankAlertUnreconciliation_list> BankAlertUnreconciliationdebit_list { get; set; }
    public List<bankdtl_list> bankdtl_list { get; set; }
    public string JSONdata { get; set; }
    public string offsetlimit { get; set; }
    public string banktransc_gid { get; set; }
    public string knockoff_status { get; set; }
    public string bankconfig_gid { get; set; }
    public string brsbank_gid { get; set; }
    public string bank_gid { get; set; }
    public string reconcildoc_gid { get; set; }
    public string trn_date { get; set; }
    public string value_date { get; set; }
    public string payment_date { get; set; }
    public string transact_particulars { get; set; }
    public string debit_amt { get; set; }
    public string credit_amt { get; set; }
    public string transact_val { get; set; }
    public string remarks { get; set; }
    public string cr_dr { get; set; }
    public string chq_no { get; set; }
    public string transc_balance { get; set; }
    public string created_by { get; set; }
    public string created_date { get; set; }
    public string bank_name { get; set; }
    public string branch_name { get; set; }
    public string acc_no { get; set; }
    public string rm_remarks { get; set; }
    public string allocated_status { get; set; }
    public string rm_status { get; set; }
    public string custref_no { get; set; }
   public string banktransc_refno { get; set; }
    public string baselocation_name { get; set; }
    public string manualknockoff_remarks { get; set; }
    public string remaining_amount { get; set; }
    public string assigned_rm { get; set; }
    public string brstransactiondetails_flag { get; set; }
    public string assignedrm_gid { get; set; }
    public string rmsendback_on { get; set; }
    public string sendback_reason { get; set; }

    public string brstransactiondetailsadvice_flag { get; set; }

}
public class bankdtl_list
{
    public string bankdtl_gid { get; set; }
    public string bankdtl_name { get; set; }
}
public class BankAlertUnreconciliation_list
{
    public string tagemployee_gid { get; set; }
    public string banktransc_gid { get; set; }
    public string taggedmember_gid { get; set; }
    public string taggedmember_name { get; set; }
    public string tagged_remarks { get; set; }
    public string brs_status { get; set; }
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
    public string knockoff_status { get; set; }
    public string aging { get; set; }
    public string manualknockoff_remarks { get; set; }
    public string transact_particulars { get; set; }
    public string cr_dr { get; set; }
    public string transact_val { get; set; }
    public string closed_date { get; set; }
    public string closed_by { get; set; }
    public string assigned_date { get; set; }
    public string assigned_by { get; set; }
    public string assigned_to { get; set; }
    public string remaining_amount { get; set; }
    public string transaction_aging { get; set; }
}
public class uploadlist : result
{
    public List<uploadlist> uploadtemplatelist { get; set; }
    public string reconcildoc_gid { get; set; }
    public string created_by { get; set; }
    public string created_date { get; set; }
    public string  repaydoc_status { get; set; }
    public string file_name { get; set; }
    public string file_path { get; set; }
    public string repayreconcildoc_gid { get; set; }
    public string transac_count { get; set; }
    public string repayment_count { get; set; }
    public string Status { get; set; }
    public string total_count { get; set; }
    public string pending_count { get; set; }
    public string closed_count { get; set; }
   
public string norepayment_flag { get; set; }

    public string JSONdata { get; set; }

}
