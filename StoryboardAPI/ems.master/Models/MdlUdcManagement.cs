using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlUdcManagement : result
    {        
        public string udcmanagement_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string sanctionrefno_gid { get; set; }
        public string sanctionrefno_name { get; set; }
        public string udc_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string application_gid { get; set; }
        public List<udcmanagement_list> udcmanagement_list { get; set; }
        public List<cadapplicationlist> cadapplicationlist { get; set; }
    }

    public class udcmanagement_list
    {
        public string udcmanagement_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string sanctionrefno_gid { get; set; }
        public string sanctionrefno_name { get; set; }
        public string udc_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string udcmanagement2cheque_gid { get; set; }
        public string cheque_no { get; set; }
        public string stakeholder_name { get; set; }
        public string cheque_type { get; set; }
        public string api_code { get; set; }
    }

    public class MdlCheque : result
    {
        public string udcmanagement2cheque_gid { get; set; }
        public string udcmanagement_gid { get; set; }
        public string stakeholder_gid { get; set; }
        public string stakeholder_name { get; set; }
        public string stakeholder_type { get; set; }
        public string designation { get; set; }
        public string accountholder_name { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string cheque_no { get; set; }
        public string ifsc_code { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string mergedbankingentity_gid { get; set; }
        public string mergedbankingentity_name { get; set; }
        public string special_condition { get; set; }
        public string general_remarks { get; set; }
        public string cts_enabled { get; set; }
        public string cheque_type { get; set; }
        public string date_chequetype { get; set; }
        public string date_chequepresentation { get; set; }
        public string status_chequepresentation { get; set; }
        public string date_chequeclearance { get; set; }
        public string status_chequeclearance { get; set; }
        public DateTime datechequetype { get; set; }
        public DateTime datechequepresentation { get; set; }
        public DateTime datechequeclearance { get; set; }
        public List<cheque_list> cheque_list { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
    }

    public class cheque_list
    {
        public string udcmanagement2cheque_gid { get; set; }
        public string udcmanagement_gid { get; set; }
        public string stakeholder_gid { get; set; }
        public string stakeholder_name { get; set; }
        public string stakeholder_type { get; set; }
        public string designation { get; set; }
        public string accountholder_name { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string cheque_no { get; set; }
        public string ifsc_code { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string mergedbankingentity_gid { get; set; }
        public string mergedbankingentity_name { get; set; }
        public string special_condition { get; set; }
        public string general_remarks { get; set; }
        public string cts_enabled { get; set; }
        public string cheque_type { get; set; }
        public string date_chequetype { get; set; }
        public string date_chequepresentation { get; set; }
        public string status_chequepresentation { get; set; }
        public string date_chequeclearance { get; set; }
        public string status_chequeclearance { get; set; }
    }


    public class MdlChequeDocument : result
    {
        public string cheque2document_gid { get; set; }
        public string udcmanagement2cheque_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<chequedocument_list> chequedocument_list { get; set; }
    }

    public class chequedocument_list
    {
        public string cheque2document_gid { get; set; }
        public string udcmanagement2cheque_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }

    }

    public class MdlDropDownUdc : result
    {
        public List<bankname_list> bankname_list { get; set; }

    }

    public class bankname_list
    {
        public string bankname_gid { get; set; }
        public string bankname_name { get; set; }
    }

    public class CustomersList : result
    {
        public List<Customer> CustomerList { get; set; }
    }


    public class Customer
    {
        public string application_gid { get; set; }
        public string customerref_name { get; set; }
    }

    public class StakeholdersList : result
    {
        public List<Stakeholder> StakeholderList { get; set; }
    }


    public class Stakeholder
    {
        public string stakeholder_gid { get; set; }
        public string stakeholder_name { get; set; }
        public string stakeholder_type { get; set; }
        public string designation { get; set; }
    }
    public class CadChequeCount : result
    {
        public string MakerPendingCount { get; set; }
        public string MakerFollowUpCount { get; set; }
        public string CheckerPendingCount { get; set; }
        public string CheckerFollowUpCount { get; set; }
        public string ApproverPendingCount { get; set; }
        public string CompletedCount { get; set; }
    }
    public class Mdlmakerchequedetails : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string maker_gid { get; set; }
        public string checker_gid { get; set; }
        public string approver_gid { get; set; }
    }
    public class Mdlcheckerchequedetails : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string maker_gid { get; set; }
        public string checker_gid { get; set; }
        public string approver_gid { get; set; }
    }
    public class Mdlapprovalchequedetails : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
    }
    public class MdlChequeApprovalDetails : result
    {
        public string application_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string approver_name { get; set; }
        public string maker_approveddate { get; set; }
        public string checker_approveddate { get; set; }
        public string approver_approveddate { get; set; }
    }

}