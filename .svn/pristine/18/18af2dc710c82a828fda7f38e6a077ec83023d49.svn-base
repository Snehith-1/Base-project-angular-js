using System;
using System.Collections.Generic;

namespace ems.master.Models
{
    public class MdlMstCreditStatusAdd : result
    {
        public string bureauname_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureau_score { get; set; }
        public string bureaugenerated_date { get; set; }
        public string lastyear_turnover { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagency_gid { get; set; }
        public string creditrating_gid { get; set; }
        public string creditrating_name { get; set; }
        public string creditrating_date { get; set; }
        public string creditratingexpiry_date { get; set; }
        public string buyer_gid { get; set; }
        public List<upload_list> upload_list { get; set; }
        public List<creditstatuslist> creditstatuslist { get; set; }
        public List<bureauscore_list> bureauscore_list { get; set; }
        public List<uploaddocument> uploaddocument { get; set; }
    }
    public class creditstatuslist : result
    {
        public string buyer_gid { get; set; }
        public string buyer_code { get; set; }
        public string buyer_name { get; set; }
        public string credit_status { get; set; }
        public string department_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string creditActive_status { get; set; }
        public List<bureauscore_list> bureauscore_list { get; set; }
    }
    public class bureauscore_list
    {
        public string bureauname_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureau_score { get; set; }
        public string bureaugenerated_date { get; set; }
        public string lastyear_turnover { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagency_gid { get; set; }
        public string creditrating_gid { get; set; }
        public string creditrating_name { get; set; }
        public string creditrating_date { get; set; }
        public string creditratingexpiry_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string bureauscoreadd_GID { get; set; }
        public List<upload_list> upload_list { get; set; }
    }
    public class uploaddocument : result
    {
        public List<upload_list> upload_list { get; set; }
    }
    public class upload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlAttachmentList
    {
        public string mailattachment_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_extension { get; set; }
        public string composemailattachment_gid { get; set; }
    }
    public class buyeredit : result
    {
        public string buyer_gid { get; set; }
        public string buyer_code { get; set; }
        public string buyer_name { get; set; }
        public string editcoi_date { get; set; }
        public DateTime coi_date { get; set; }
        public string editbusinessstart_date { get; set; }
        public DateTime businessstart_date { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string cin_no { get; set; }
        public string pan_no { get; set; }
        public string gst_no { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string cap_limit { get; set; }
        public string overall_limit { get; set; }
        public string buyer_limit { get; set; }
        public string guarantor_limit { get; set; }
        public string borrower_limit { get; set; }
        public string credit_status { get; set; }
        public string creditActive_status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public string approval_remarks { get; set; }
        public List<buyerInactive_List> buyerInactive_List  { get;set;}
        public List<buyerApproval_List> buyerApproval_List { get; set; }
    }
    public class buyerInactive_List
    {
        public string buyer_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string remarks { get; set; }
        public string creditActive_status { get; set; }
    }
    public class buyerApproval_List
    {
        public string buyer_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string approval_remarks { get; set; }
        public string creditstatus_Approval { get; set; }
    }
    public class MdlEmail_address : result
    {
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string buyer2emailaddress_gid { get; set; }
        public string buyer_gid { get; set; }
        public List<email_list> email_list { get; set; }
    }
    public class email_list
    {
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string buyer2emailaddress_gid { get; set; }
        public string buyer_gid { get; set; }
    }
    public class MdlBank_Details : result
    {
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string buyer2bank_gid { get; set; }
        public string buyer_gid { get; set; }
        public string micr_code { get; set; }
        public string bank_address { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public List<bank_list> bank_list { get; set; }
    }
    public class bank_list
    {
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string buyer2bank_gid { get; set; }
        public string buyer_gid { get; set; }
        public string micr_code { get; set; }
        public string bank_address { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
    }
    public class buyer_count
    {
        public string count_pending { get; set; }
        public string count_approved { get; set; }
        public string count_nonapproved { get; set; }
    }
}