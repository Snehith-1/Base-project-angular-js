using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// (It's used for flow after CAD Accepted in Samfin)CAD Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlMstCAD : result
    {
        public string contactperson_name { get; set; }
        public string application_gid { get; set; }
        public string sanction_type { get; set; }
        public string primary_address { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string loanfacility_amount { get; set; }
        public string rate_interest { get; set; }
        public string facility_mode { get; set; }
        public string tenureoverall_limit { get; set; }
        public string creditapproved_date { get; set; }
        public string vertical_code { get; set; }
        public string loan_type { get; set; }
        public string facility_type { get; set; }
        public string buffer_days { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
        public List<sanctionto_list> sanctionto_list { get; set; }
        public List<cadbuyer_list> cadbuyer_list { get; set; }
        public List<producttype_list> producttype_list { get; set; }
        public List<productsubtype_list> productsubtype_list { get; set; }
        public List<cadmomdocument_list> cadmomdocument_list { get; set; }
        public List<cadcamdocument_list> cadcamdocument_list { get; set; }
        public List<appsanction_list> appsanction_list { get; set; }
        public List<cadccmember_list> cadccmember_list { get; set; }
        public List<sanctiontype_list> sanctiontype_list { get; set; }
        public List<cadmobileno_list> cadmobileno_list { get; set; }
        public List<cademail_list> cademail_list { get; set; }
        public List<cadapplicationlist> cadapplicationlist { get; set; }
        public List<cadaddress_list> cadaddress_list { get; set; }
        public List<creditbuyer_list> creditbuyer_list { get; set; }
        public List<rmbuyer_list> rmbuyer_list { get; set; }
        public List<CADDocument> CADDocument { get; set; }
        public List<TaggedDocument> TaggedDocument { get; set; }

        public List<sanctiondocument_list> sanctiondocument_list { get; set; }

        public string[] documenttype_gid { get; set; }
        public string[] document_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string credit_gid { get; set; }
        public string applicant_type { get; set; }
        public string companydocument_name { get; set; }
        public string lstype { get; set; }
        public string taggedby { get; set; }
        public string pslupdated_date { get; set; }
        public string pslcompleted_flag { get; set; }
        public string pslcsacomplete_flag { get; set; }
        public string cadgroup_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string menu_gid { get; set; }
        public string menu_name { get; set; }
        public string maker_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_gid { get; set; }
        public string checker_name { get; set; }
        public string approver_gid { get; set; }
        public string approver_name { get; set; }
        public string cadaccepted_type { get; set; }
    }
    public class MdlProcessType : result
    {
        public string processtypeassign_gid { get; set; }
        public string processtype_gid { get; set; }
        public string processtype_name { get; set; }
        public string cadgroup_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string menu_gid { get; set; }
        public string menu_name { get; set; }
        public string maker_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_gid { get; set; }
        public string checker_name { get; set; }
        public string approver_gid { get; set; }
        public string approver_name { get; set; }
        public string application_gid { get; set; }
        public List<menulist> menulist { get; set; }
        public string applyall_flag { get; set; }
    }
    public class menulist : result
    {
        public string menu_gid { get; set; }
        public string menu_name { get; set; }
    }

    public class MdlMstProcessTypeSummary : result
    {
        public List<processtype_list> processtype_list { get; set; }
        public string application_gid { get; set; }
        public string processtype_name { get; set; }
        public string cadgroup_name { get; set; }
        public string cadgroup_gid { get; set; }
    }
    public class processtype_list : result
    {
        public string processtypeassign_gid { get; set; }
        public string processtype_name { get; set; }
        public string cadgroup_name { get; set; }
        public string menu_name { get; set; }
        public string checker_name { get; set; }
        public string maker_name { get; set; }
        public string approver_name { get; set; }
        public string application_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlUpdateProcessType : result
    {
        public string application_gid { get; set; }
        public string process_type { get; set; }
        public string processtype_remarks { get; set; }
        public string processupdated_by { get; set; }
        public string processupdated_date { get; set; }
    }
    public class cadapplicationlist : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string ccapproved_date { get; set; }
        public string creditgroup_name { get; set; }
        public string ccgroup_name { get; set; }
        public string ccadmin_name { get; set; }
        public string cadsentback_by { get; set; }
        public string cadsentback_date { get; set; }
        public string cadgroup_name { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string ccrejected_by { get; set; }
        public string ccrejected_date { get; set; }
        public string docchecklist_approvalflag { get; set; }
        public string approval { get; set; }
        public string checkerapproved_by { get; set; }
        public string checkerapproved_on { get; set; }
        public string sanction_status { get; set; }
        public string cadgroupname { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string pslupdated_date { get; set; }
        public string pslupdated_by { get; set; }
        public string pslcompleteremarks { get; set; }
        public string rm_name { get; set; }
        public string ccsubmitted_by { get; set; }
        public string lsmakersummary_flag { get; set; }
        public string followup_status { get; set; }
        public string sanction_refno { get; set; }
        public string approver_approveddate { get; set; }
        public string customer_urn { get; set; }
        public string approved_on { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string relationshipmanager_name { get; set; }
        public string overalllimit_amount { get; set; }
        public string renewal_status { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_status { get; set; }
        public string enhancement_flag { get; set; }
        public string cusapproval_status { get; set; }
        public string renewalapproval_status { get; set; }
        public string enhancementapproval_status { get; set; }
        public string history_flag { get; set; }
        public string ccmeetingskipcolor_flag { get; set; }
        public string verification_flag { get; set; }
        public string tagged_by { get; set; }
        public string tagged_date { get; set; }
        public string remarks { get; set; }
        public string customer2tag_gid { get; set; }
        public string npatag_flag { get; set; }
        public string legaltag_flag { get; set; }
        public string lms_status { get; set; }
        public string renewalenhancementapproval_status { get; set; }
        public string LMS_status { get; set; }
    }
    public class CadApplicationCount : result
    {
        public string cadreview_count { get; set; }
        public string sentbackcc_count { get; set; }
        public string accept_count { get; set; }
        public string backtounderwriting_count { get; set; }
        public string ccrejected_count { get; set; }
        public Int16 lstotalcount { get; set; }
        public string urngrouping_count { get; set; }
    }
    public class cadaddress_list
    {
        public string address_gid { get; set; }
        public string address { get; set; }
    }
    public class cadmobileno_list
    {
        public string mobileno_gid { get; set; }
        public string mobile_no { get; set; }
    }
    public class cademail_list
    {
        public string email_gid { get; set; }
        public string email_address { get; set; }
    }
    public class sanctiontype_list
    {
        public string sanctiontype_gid { get; set; }
        public string sanctiontype_name { get; set; }
    }
    public class sanctionto_list
    {
        public string sanctionto_name { get; set; }
        public string sanctionto_gid { get; set; }
    }
    public class cadbuyer_list : result
    {
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string buyer_exposure { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string uploaded_by { get; set; }
        public string uploaded_date { get; set; }
        public string baldocument_gid { get; set; }
        public string application2buyer_gid { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string balance_limit { get; set; }
        public string bill_tenure { get; set; }
        public string margin { get; set; }
    }
    public class producttype_list
    {
        public string producttype_gid { get; set; }
        public string product_type { get; set; }
    }
    public class productsubtype_list
    {
        public string productsub_type { get; set; }
        public string productsubtype_gid { get; set; }
    }
    public class cadmomdocument_list : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
        public string application2momdoc_gid { get; set; }
        public string updated_date { get; set; }
        public string uploaded_by { get; set; }
    }
    public class cadcamdocument_list : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
        public string updated_date { get; set; }
        public string uploaded_by { get; set; }
        public string application2camdoc_gid { get; set; }
    }
    public class cadccmember_list
    {
        public string CCMember_name { get; set; }
        public string ccmember_gid { get; set; }
        public string ccmembermaster_gid { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string attendance_status { get; set; }
        public string approval_status { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class appsanction_list : result
    {
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string application_name { get; set; }
        public string ccapproved_date { get; set; }
        public string application_no { get; set; }
        public string application_gid { get; set; }
        public string created_date { get; set; }
        public string sanctionto_name { get; set; }
        public string sanction_status { get; set; }

        public string accepted_status { get; set; }
        public string accepted_reason { get; set; }
        public char rbo_status { get; set; }
        public string esign_status { get; set; }
        public string remarks { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string edit_flag { get; set; }
        public string lsemployeegid { get; set; }

        public string customername { get; set; }
        public string application2sanctionlog_gid { get; set; }
        public string created_by { get; set; }
        public string customer_urn { get; set; }
        public string checkerapproval_flag { get; set; }
        public string submitedtoapproval_status { get; set; }

        public string sanctionsubmittoapprovallog_gid { get; set; }
        public string cadaccepted_type { get; set; }

    }
    public class cadsanctiondetails : result
    {
        public string application_gid { get; set; }
        public string reset_flag { get; set; }
        public string batch_status { get; set; }
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public DateTime sanctionDate { get; set; }
        public string entity { get; set; }
        public string sanction_amount { get; set; }
        public string branch_name { get; set; }
        public string branch_gid { get; set; }
        public string customername { get; set; }
        public string created_date { get; set; }
        public string entity_gid { get; set; }
        public List<buyer_list> buyer_list { get; set; }
        public List<cadloanfacilitytype_list> cadloanfacilitytype_list { get; set; }
        public string applicability_category { get; set; }
        public string paycard { get; set; }
        public string ccapprovedby_gid { get; set; }
        public string esdeclaration_status { get; set; }
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string checkerletter_flag { get; set; }
        public string checkerapproval_flag { get; set; }
        public string sanction_status { get; set; }
        public string sanctionapprovallog_gid { get; set; }
        public string checkerreject_remarks { get; set; }
        public string sanctionfrom_date { get; set; }
        public DateTime sanctionfromDate { get; set; }
        public string sanctiontill_date { get; set; }
        public DateTime sanctiontillDate { get; set; }
        public string application_type { get; set; }
        public string sanctionto_gid { get; set; }
        public string sanctionto_name { get; set; }
        public string contactpersonaddress_gid { get; set; }
        public string contactperson_address { get; set; }
        public string contactperson_name { get; set; }
        public string contactperson_number { get; set; }
        public string contactpersonemail_address { get; set; }
        public string applicationtype_gid { get; set; }
        public string contactpersonmobileno_gid { get; set; }
        public string contactpersonemail_gid { get; set; }
        public string application_name { get; set; }
        public string sanction_type { get; set; }
        public string natureof_proposal { get; set; }
        public string ccapproved_date { get; set; }

        public string makerfile_path { get; set; }
        public string makerfile_name { get; set; }
        public string sanctionletter_status { get; set; }
        public string template_content { get; set; }
        public string makersubmitted_by { get; set; }
        public string makersubmitted_on { get; set; }
        public string sanctiongenerated_by { get; set; }
        public string sanctiongenerated_on { get; set; }

        public string template_name { get; set; }
        public string checkerapproved_by { get; set; }
        public string checkerupdated_on { get; set; }
        public string checkerpushback_remarks { get; set; }
       // public string checkerapproval_flag { get; set; }
        public string checkerapproved_on { get; set; }
        public string digitalsignature_flag { get; set; }

        public DateTime ccapproved_datetime { get; set; }
        public DateTime sanctionfrom_datetime { get; set; }
        public DateTime sanctiontill_datetime { get; set; }
        public List<sanctionto_list> sanctionto_list { get; set; }
        public List<cadaddress_list> cadaddress_list { get; set; }
        public List<cadmobileno_list> cadmobileno_list { get; set; }
        public List<cademail_list> cademail_list { get; set; }
    }
    public class cadloanfacilitytype_list : result
    {
        public string margin { get; set; }
        public string document_limit { get; set; }
        public string tenure { get; set; }
        public string revolving_type { get; set; }
        public string expiry_date { get; set; }
        public string customer2sanction_gid { get; set; }
        public string sanction2loanfacilitytype_gid { get; set; }
        public string loanfacility_type { get; set; }
        public string loanfacility_gid { get; set; }
        public string loanmaster_gid { get; set; }
        public string loanTitle { get; set; }
        public string loanfacility_amount { get; set; }
        public string interchangeability { get; set; }
        public string report_structure { get; set; }
        public string loanfacilityref_no { get; set; }
        public string proposed_roi { get; set; }
        public string penal_interest { get; set; }
        public string interchangeability_amount { get; set; }
        public string total_documentlimit { get; set; }
        public string totalloanfacility_amount { get; set; }
        public string productsubtype_gid { get; set; }
        public string productsub_type { get; set; }
        public string application2loan_gid { get; set; }
        public string principalfrequency_name { get; set; }
        public string interestfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string interestfrequency_gid { get; set; }
    }

    public class mdltemplate : result
    {
        public string makerfile_path { get; set; }
        public string makerfile_name { get; set; }
        public string sanctionletter_status { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
        public List<template_list> template_list { get; set; }
        public string sanctionletter_flag { get; set; }
        public string checkerapproval_flag { get; set; }
        public string checkerletter_flag { get; set; }
        public string checkerpushback_remarks { get; set; }
        public string sanction_status { get; set; }
        public string digitalsignature_flag { get; set; }
        public string checkerupdated_by { get; set; }
        public string checkerupdated_on { get; set; }
        public string makersubmitted_by { get; set; }
        public string makersubmitted_on { get; set; }
        public string template_gid { get; set; }
        public string approved_by { get; set; }
        public string approved_date { get; set; }
        public string defaulttemplate_content { get; set; }
        public string mstcontent_flag { get; set; }
        public List<MdlTemplatedtl> MdlTemplatedtl { get; set; }
    }
    public class cadtemplate_list : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
        public string lsname { get; set; }
        public string lspath { get; set; }
       
        public string sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string application_name { get; set; }
        public string ccapproved_date { get; set; }
        public string contactperson { get; set; }
        public string address { get; set; }
        public string mobileno { get; set; }
        public string email { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string employee_mobileno { get; set; }
        public string employee_mailid { get; set; }
        public string addoncharge { get; set; }
        public string validity_months { get; set; }

        public string loanfacility_type { get; set; }
        public string document_limit { get; set; }
        public string revolving_type { get; set; }
        public string tenure { get; set; }
        public string loanfacility_amount { get; set; }
        public string proposed_roi { get; set; }
        public string margin { get; set; }
        public string purpose_lending { get; set; }
        public string interest_amount { get; set; }
        public string facilityamount_words { get; set; }

        public string lsname1 { get; set; }
        public string lspath1 { get; set; }
        public string document_path { get; set; }
        public List<cadloanfacilitytype_list> cadloanfacilitytype_list { get; set; }

        public string pushback_remarks { get; set; }
        public string reject_remarks { get; set; }
        public string sanction_status { get; set; }
        public string lstab { get; set; }
        public string maker_gid { get; set;}
        public string checker_gid { get; set; }
        public string approver_gid { get; set; }
        public string application_gid { get; set; }
        public string defaulttemplate_content { get; set; }
    }

    public class sanctiondetailsList : result
    {
        public string pending_count { get; set; }
        public string completed_count { get; set; }
        public string approved_count { get; set; }
        public List<sanctiondetails> sanctiondetails { get; set; }
    }

    public class CadSanctionCount : result
    {
        public string cadmaker_count { get; set; }
        public string makerfollowup_count { get; set; }
        public string cadchecker_count { get; set; }
        public string checkerfollowup_count { get; set; }
        public string cadcheckerapproval_count { get; set; }

        public string MakerPendingCount { get; set; }
        public string MakerFollowUpCount { get; set; }
        public string CheckerPendingCount { get; set; }
        public string CheckerFollowUpCount { get; set; }
        public string ApproverPendingCount { get; set; }
        public string CompletedCount { get; set; }
        public string approvalcompleted_count { get; set; }
        public string AcceptedCount { get; set; }
    }
    public class Mdlloanfacility_type : result
    {
        public string customer2sanction_gid { get; set; }
        public string customer2loanfacilitytype_gid { get; set; }
        public string sanction2loanfacilitytype_gid { get; set; }
        public string sanction_amount { get; set; }
        public string loanfacility_type { get; set; }
        public string loanfacility_amount { get; set; }
        public string loanfacility_gid { get; set; }
        public string loanmaster_gid { get; set; }
        public string loanTitle { get; set; }
        public string totalloanfacility_amount { get; set; }
        public string facility_type_gid { get; set; }
        public string lsacreate_gid { get; set; }
        public string limitinfodtl_gid { get; set; }
        public string margin { get; set; }
        public string document_limit { get; set; }
        public string tenure { get; set; }
        public string revolving_type { get; set; }
        public string expiry_date { get; set; }
        public string interchangeability { get; set; }
        public string report_structure { get; set; }
        public string total_documentlimit { get; set; }
        public string loanfacilityref_no { get; set; }
        public List<cadloanfacilitytype_list> cadloanfacilitytype_list { get; set; }
        public string interchangeability_amount { get; set; }
        public string applicable_condition { get; set; }
        public string existing_limit { get; set; }
        public string proposed_roi { get; set; }
        public string penal_interest { get; set; }
        public string productsub_type { get; set; }
        public string productsubtype_gid { get; set; }
        public List<cadtemplate_list> cadtemplate_list { get; set; }
    }
    public class sanctiondetails : result
    {
        public string created_by { get; set; }
        public string application_gid { get; set; }
        public string created_date { get; set; }
        public string application2sanction_gid { get; set; }
        public string sanction_status { get; set; }
        public string sanctionapprovallog_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string customername { get; set; }
        public string cadgroup_name { get; set; }
        public string reset_flag { get; set; }
        public string application_no { get; set; }
        public string ccapproved_date { get; set; }
        public string approval_status { get; set; }
        public string ccgroup_name { get; set; }
        public string checkerreject_remarks { get; set; }
        public string checkerapproval_flag { get; set; }
        public string remarks { get; set; }
        public string cadgroupname { get; set; }
        public string customer_urn { get; set; }
        public string approver_approveddate { get; set; }
        public string edit_flag { get; set; }
        public string lsemployeegid { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
        public string submitedtoapproval_status { get; set; }
        public string esignreinitiate_flag { get; set; }
    }
    public class rmbuyer_list
    {
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string balance_limit { get; set; }
        public string top_buyer { get; set; }
        public string bill_tenuredays { get; set; }
        public string margin { get; set; }
        public string application2buyer_gid { get; set; }
    }
    public class CADDocument : result
    {
        public string documenttype_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string companydocument_name { get; set; }
        public string individualdocument_name { get; set; }
        public string groupdocument_name { get; set; }
        public string document_name { get; set; }
        public string document_gid { get; set; }
        public string covenant_type { get; set; }
    }
    public class TaggedDocument
    {
        public string documenttype_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string documenttype_count { get; set; }
        public string companydocument_name { get; set; }
        public string covenant_type { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string companydocument_gid { get; set; }
        public string taggedby { get; set; }
        public string covenantperiod { get; set; }
        public string individual2document_gid { get; set; }
        public string group2document_gid { get; set; }
        public string tagged_name { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string groupcovdocumentchecklist_gid { get; set; }

        public string document_status { get; set; }
        public string buffer_days { get; set; }
    }
    public class MdlDocChecklistdetails : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string maker_gid { get; set; }
        public string checker_gid { get; set; }
        public string approver_gid { get; set; }
    }
    public class MdlMstAssignmentview : result
    {
        public List<assignment_list> assignment_list { get; set; }
        public string application_gid { get; set; }
        public string processtype_remarks { get; set; }
        public string process_type { get; set; }
        public string processupdated_by { get; set; }
        public string processupdated_date { get; set; }
        public List<Reassignedlog_list> Reassignedlog_list { get; set; }
    }

    public class Reassignedlog_list : result
    {
        public string processtypeassignlog_gid { get; set; }
        public string application_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string approver_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class assignment_list : result
    {
        public string processtypeassign_gid { get; set; }
        public string application_gid { get; set; }
        public string processtype_name { get; set; }
        public string cadgroup_name { get; set; }
        public string menu_name { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string approver_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string processtype_remarks { get; set; }
        public string process_type { get; set; }
        public string processupdated_by { get; set; }
        public string processupdated_date { get; set; }
        public string cadgroup_gid { get; set; }
        public string menu_gid { get; set; }
        public string approved_date { get; set; }
        public string overall_approvalstatus { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string vertical_name { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_amount { get; set; }
        public string queryraised_on { get; set; }
        public string queryclosed_on { get; set; }
        public string rmupload_date { get; set; }
        public string lsa_amount { get; set; }
        public string maker_approvalflag { get; set; }
        public string checker_approvalflag { get; set; }
        public string approver_approvalflag { get; set; }
        public string sanction_status { get; set; }
    }
    public class MdlMstCADGetMenu : result
    {
        public List<MdlMstCADGetMenuList> menu_list { get; set; }
    }
    public class MdlMstCADGetMenuList : result
    {
        public string menu_name { get; set; }
        public string menu_gid { get; set; }
    }
    public class MdlCADRevert : result
    {
        public string application_gid { get; set; }
        public string cadtocc_reason { get; set; }
        public string cadtocredit_reason { get; set; }
    }

    public class MdlMstCADCompany : result
    {
        public List<CADDocument> CADDocument { get; set; }
    }

    public class MdlCovenantPeriodlist : result
    {
        public string taggedby { get; set; }
        public List<CovenantPeriod> CovenantPeriod { get; set; }

    }
    public class CovenantPeriod
    {
        public string covenantperiod { get; set; }
        public string buffer_days { get; set; }
        public string documenttype_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string individual2document_gid { get; set; }
        public string group2document_gid { get; set; }
        public string companydocument_gid { get; set; }
        public string covenant_type { get; set; }
        public string lstype { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public bool covenantchecked { get; set; }
        public string groupcovdocumentchecklist_gid { get; set; }
    }

    public class scannedmakerapplicationlist : result
    {
        public List<scannedmakerapplication> scannedmakerapplication { get; set; }
    }

    public class scannedmakerapplication
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string ccapproved_date { get; set; }
        public string creditgroup_name { get; set; }
        public string ccgroup_name { get; set; }
        public string cadgroupname { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string approval { get; set; }
        public string processtypeassign_gid { get; set; }
        public string overall_approvalstatus { get; set; }
        public string completed_on { get; set; }
        public string sanction_refno { get; set; }
        public string customer_urn { get; set; }
        public string followupstatus { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
    }
    public class MdlReassignCadApplication : result
    {
        public string application_gid { get; set; }
        public string cadgroup_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string menu_gid { get; set; }
        public string menu_name { get; set; }
        public string maker_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_gid { get; set; }
        public string checker_name { get; set; }
        public string approver_gid { get; set; }
        public string approver_name { get; set; }
    }
    public class MdlMstCADAssignment : result
    {
        public string processtypeassign_gid { get; set; }
    } 

    public class UploadCADDocumentname : result
    {
        public List<UploadCADES_DocumentList> UploadCADES_DocumentList { get; set; }
        public List<DeviationCADMail_DocumentList> DeviationCADMail_DocumentList { get; set; }
    }

    public class UploadCADES_DocumentList : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }

    public class DeviationCADMail_DocumentList : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }

    public class Sanctionlimitvalidation : result
    {
        public string total_documentlimit { get; set; }
    }
    //CAD To CC Log
    public class MdlCADtoCCMeetingLog : result
    {
        public string application_gid { get; set; }
        public string cadtoccmeeting_reason { get; set; }
        public List<cadtoccmeetinglog_list> cadtoccmeetinglog_list { get; set; }
    }
    public class cadtoccmeetinglog_list : result
    {
        public string application_gid { get; set; }
        public string cadtoccmeeting_reason { get; set; }
        public string cadtocclog_gid { get; set; }
        public string sentbackcadtocc_date { get; set; }
        public string sentbackcadtocc_by { get; set; }
    }
    public class MdlCADtoCreditLog : result
    {
        public string application_gid { get; set; }
        public string cctocredit_reason { get; set; }
        public List<cadtocreditlog_list> cadtocreditlog_list { get; set; }
    }
    public class cadtocreditlog_list : result
    {
        public string application_gid { get; set; }
        public string cadtocredit_reason { get; set; }
        public string cadtocreditlog_gid { get; set; }
        public string sentbackcadtocredit_date { get; set; }
        public string sentbackcadtocredit_by { get; set; }
    }

    public class MdlCreditWithoutCCLog : result
    {
        public string application_gid { get; set; }
        public string creditwithoutcc_reason { get; set; }
        public List<creditwithoutcclog_list> creditwithoutcclog_list { get; set; }
    }
    public class creditwithoutcclog_list : result
    {
        public string application_gid { get; set; }
        public string creditwithoutcc_reason { get; set; }
        public string ccmeetingskip_gid { get; set; }
        public string sentbackcreditwithoutcc_date { get; set; }
        public string sentbackcreditwithoutcc_by { get; set; }
    }
    public class MdlSanctionApprovalDetails : result
    {
        public string application_gid { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string approver_name { get; set; }
        public string maker_approveddate { get; set; }
        public string checker_approveddate { get; set; }
        public string approver_approveddate { get; set; }
    }
    public class MdlCADAcceptDetails : result
    {
        public string application_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
    }

    public class PersonalGuarantee : result
    {
        public string Guarantor_name { get; set; } 
    }

    public class sanctiondocument_list : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
        public string application2sanctiondoc_gid { get; set; }
    }

    public class customerurntag : result
    {
        public string customer_name { get; set; }
        public string tag_type { get; set; }
        public string currentcustomer_urn { get; set; }
        public string tag_remarks { get; set; }
        public string urn_number { get; set; }
        public string customer2tag_gid { get; set; }
        public string taggednpa_count { get; set; }
        public string taggedlegal_count { get; set; }
        public List<customerurntag_list> customerurntag_list { get; set; }
    }
    public class customerurntag_list : result
    {
        public string customer_name { get; set; }
        public string tag_type { get; set; }
        public string currentcustomer_urn { get; set; }
        public string tag_remarks { get; set; }
        public string tag_status { get; set; }
        public string urn_number { get; set; }
        public string customer2tag_gid { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        
    }
    public class MdlCadDocumentSubmissionFlag : result
    {
        public string docsubmission_flag { get; set; }

    }
    public class MdlcustomercreationLMS : result 
    { 
        public string application_gid { get; set; }
        public string customername { get; set; }
        public string address1 { get; set;}
        public string address2 { get; set;}
        public string state { get; set;}
        public string city { get; set;}
        public string country { get; set;}
        public string sector { get; set;}
        public string institution_gid { get; set;}
        public string customer_urn { get; set;} 
        public string rm_name { get; set;}
        public string idproof_name { get; set;}

        public string idproof_no { get; set;}

        public string pincode { get; set;}

        public string RMName { get; set;}

        public string userid { get; set;}
        public string RMmailID { get; set;}
        public string mobilenumber { get; set;}
        public string emailaddress { get; set; }
        public string Firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string bankaccount_number { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string accountholder_name { get; set; }

        public string total_count { get; set; }

        public string lms_statuslog { get; set; }
        public string lms_status { get; set; }

        public string rejected_remarks { get; set; }

        public string lsname { get; set; }

        public string lscloudpath { get; set; }

        public string lspath { get; set; }

        public string update_flag { get; set; }

        public string reject_flag { get; set; }
        public string application_no { get; set; }        
        public string encorefindcust_status { get; set; }

        public List<gstcuc_list> gst_list { get; set; }
        //public List<gst_list> gst_list { get; set; }
        public List<bankaccnoinstitution_list> bankaccnoinstitution_list { get; set; }
        public List<bankaccnocontact_list> bankaccnocontact_list { get; set; }

        public List<appinitiatedlist> initiated_list { get; set; }
        public List<apprejectedlist> rejected_list { get; set; }
        public List<appupdatedlist> updated_list { get; set; }

        public string Gst { get; set; }

        public string cuclms_gid { get; set; }
    }
    public class gstcuc_list: result
    {
        public string Gst { get;set; }
        public string institution_gid { get; set; }
    }
    public class bankaccnoinstitution_list : result
    {
        public string bankaccount_number { get; set; }
        public string application_gid { get; set; }
    }
    public class bankaccnocontact_list : result
    {
        public string bankaccount_number { get; set; }
        public string application_gid { get; set; }

    }
    public class apprejectedlist : result
    {
        public string application_gid { get; set; }
        public string bankaccount_number { get; set; }
        public string Gst { get; set; }
        public string initiated_by { get; set; }
        public string initiated_date { get; set; }
        public string lms_status { get; set; }
        public string cuclms_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string rejected_remarks { get; set; }
        public string application_no { get; set; }
    }
    public class appinitiatedlist : result
    {
        public string application_gid { get; set; }
        public string bankaccount_number { get; set; }
        public string Gst { get; set; }
        public string initiated_by { get; set; }
        public string initiated_date { get; set; }
        public string lms_status { get; set; }
        public string cuclms_gid { get; set; }
        public string customer_name { get; set; }
        public string application_no { get; set; }


    }
    public class appupdatedlist : result
    {
        public string application_gid { get; set; }
        public string bankaccount_number { get; set; }
        public string Gst { get; set; }
        public string initiated_by { get; set; }
        public string initiated_date { get; set; }
        public string lms_status { get; set; }
        public string cuclms_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string application_no { get; set; }



    }
   
}