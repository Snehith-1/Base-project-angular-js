using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
    /// <summary>
    ///To upload Repayment Statement it digitize the axis,kotak,icici,sbi,rbl,idfc Banktranscation to get matched/Nonmatched  cases
    ///To overcome the excel upload taking long time to split the process in dump the temp table in all records.
    /// User will clicking  Process the temp table data push into repayament table  then knockoff the matched records.
    /// </summary>
    /// <remarks>Written by Motches,Sherin</remarks>
    public class MdlRepaymentReconcillation:result
    {
        public List<upload_list2> upload_list { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
    }
    public class upload_list2
    {
        public string reconcildoc_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string lspath { get; set; }

    }
    public class repaymentlist : result
    {
        public List<repaymentlist> repayment_list { get; set; }
        public string JSONdata { get; set; }
        public string offsetlimit { get; set; }

        public string bankrepaytransc_gid { get; set;}
        public string account_number { get; set;}
        public string account_code { get; set;}
        public string created_by { get; set;}
        public string created_date { get; set;}
        public string transaction_date { get; set;}
        public string transaction_id { get; set;}
        public string repayment_date { get; set; }
        public string repayment_amount { get; set; }
        public string principal { get; set; }
        public string normal_interest { get; set; }
        public string forfeiture_waiver { get; set; }
        public string user_id { get; set; }
        public string repayment_type { get; set; }
        public string penal_interest { get; set; }
        public string old_dpd { get; set; }
        public string instrument { get; set; }
        public string dpd { get; set; }
        public string repaymentpending_count { get; set; }
        public string repaymentmatched_count { get; set; }
        public string interest_tds { get; set; }
        public string repayreconcildoc_gid { get; set; }
        public string remarks { get; set; }
        public string penal_interest_tds { get; set; }


    }
    public class repayment_cocillationlist : result
    {
        public List<repayment_cocillationlist> repayment_cocillation_list { get; set; }
        public List<repayment_cocillationlist> repayment_lmshistory { get; set; }
        
        public string bankrepaytransc_gid { get; set; }
        public string repayreconcildoc_gid { get; set; }
        public string account_number { get; set; }
        public string transaction_date { get; set; }
        public string repayment_date { get; set; }
        public string transaction_id { get; set; }
        public string repayment_amount { get; set; }
        public string principal { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string banktransc_gid { get; set; }
        public string repayment_type { get; set; }
        public string penal_interest { get; set; }
        public string tagged_status { get; set; }
        public string knockoff_status { get; set; }
        public string repay_reconc_count { get; set; }
        public string repay_unreconc_count { get; set; }

        public string instrument { get; set; }
        public string account_code { get; set; }
        public string knockoff_date { get; set; }

    }

    public class repayment_unrecocillationlist : result
    {
        public List<repayment_unrecocillationlist> repayment_unrecocillation_list { get; set; }
        public string bankrepaytransc_gid { get; set; }
        public string repayreconcildoc_gid { get; set; }
        public string account_number { get; set; }
        public string transaction_date { get; set; }
        public string repayment_date { get; set; }
        public string transaction_id { get; set; }
        public string repayment_amount { get; set; }
        public string principal { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string repayment_type { get; set; }
        public string penal_interest { get; set; }
        public string tagged_status { get; set; }
        public string knockoff_status { get; set; }
        public string repay_reconc_count { get; set; }
        public string repay_unreconc_count { get; set; }

        public string instrument { get; set; }
        public string account_code { get; set; }

    }


    public class cocillationlist : result
    {
        public List<cocillationlist> creditmatched_list { get; set; }
        public List<cocillationlist> Debitmatched_list { get; set; }
        public List<cocillationlist> creditpartialmatched_list { get; set; }
        public List<cocillationlist> debitpartialmatched_list { get; set; }
        public List<cocillationlist> creditUnmatchedUnassigned_list { get; set; }
        public List<cocillationlist> creditunmatchedassigned_list { get; set; }
        public List<cocillationlist> debitUnmatchedUnassigned_list { get; set; }
        public List<cocillationlist> debitmatchedassigned_list { get; set; }
        public List<cocillationlist> CreditClosed_list { get; set; }
        public List<cocillationlist> debitClosed_list { get; set; }
        public string created_by { get; set; }

        public string brs_status { get; set; }
        public string creditsum_count { get; set; }
        public string debitsum_count { get; set; }
        public string unreconassigncredit_count { get; set; }
        public string unreconassigndebit_count { get; set; }
        public string unreconclosecredit_count { get; set; }
        public string unreconclosedebit_count { get; set; }
        public string manualknockoff_remarks { get; set; }
        public string unreconfin_count { get; set; }
        public string unrecondebitfin_count { get; set; }
      
        public string bankrepaytransc_gid { get; set; }

        public string credit_count { get; set; }
        public string creditmatch_count { get; set; }

        public string partialcredit_count { get; set; }
        public string unmatchunassign_count { get; set; }

        public string creditclose_count { get; set; }

        public string debit_count { get; set; }
        public string debitmatch_count { get; set; }
        public string partialdebit_count { get; set; }

        public string debitclose_count { get; set; }

        public string unmatchassign_count { get; set; }
        public string knockoff_date { get; set; }
        public string banktransc_gid { get; set; }
        public string trn_date { get; set; }
        public string banktransc_refno { get; set; }
        public string cr_dr { get; set; }
        public string tagged_status { get; set; }
        public string transact_particulars { get; set; }
        public string transact_val { get; set; }
        public string knockoff_status { get; set; }
        public string knockoff_flag { get; set; }
        public string reconc_count { get; set; }
        public string unreconc_count { get; set; }
        public string reconcredit_count { get; set; }
        public string recondebit_count { get; set; }
        public string unreconcredit_count { get; set; }
        public string unrecondebit_count { get; set; }
        public string unreconpendingdebit_count { get; set; }

        public string unreconpendingcredit_count { get; set; }
        public string unreconreassignpendingcredit_count { get; set; }
        public string unreconreassignpendingdebit_count { get; set; }
        public string unreconpending_count { get; set; }
        public string unreconcompdebit_count { get; set; }
        public string unreconcompcredit_count { get; set; }
        public string unreconcomp_count { get; set; }
      public string bank_name { get; set; }

    }
    public class Mdllmsrepayprocess:result
    {
        public string reconcildoc_gid { get; set; }
        public string bankrepaytransc_gid { get; set; }
        public string account_number { get; set; }
        public string account_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string transaction_date { get; set; }
        public string transaction_id { get; set; }
        public string repayment_date { get; set; }
        public string repayment_amount { get; set; }
        public string principal { get; set; }
        public string normal_interest { get; set; }
        public string forfeiture_waiver { get; set; }
        public string user_id { get; set; }
        public string repayment_type { get; set; }
        public string penal_interest { get; set; }
        public string old_dpd { get; set; }
        public string instrument { get; set; }
        public string dpd { get; set; }
        public string repaymentpending_count { get; set; }
        public string repaymentmatched_count { get; set; }
        public string interest_tds { get; set; }
        public string repayreconcildoc_gid { get; set; }
        public string remarks { get; set; }
        public string penal_interest_tds { get; set; }
        public string transact_val { get; set; }
        public string urn_no { get; set; }
    }

    public class MdllmsTmprepay: result
    {
        public string repayreconcildoc_gid { get; set; } 
    }
    public class MdlImportbanktransactiondtl 
    {
        public string banktransc_gid { get; set; }
        public string banktransc_refno { get; set; }
        public string remaining_amount { get; set; }
        public string transact_val { get; set; }
        public string cr_dr { get; set; }
        public string transact_particulars { get; set;}
        public string knockoff_status { get; set;} 
        public string bankrepaytransc_gid { get; set;} 

        public string trn_date { get; set; }

    }

}
