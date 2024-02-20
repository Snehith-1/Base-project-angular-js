using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for ApplicationReport in Samfin)ApplicationReport Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    //public class MstApplicationReport : result
    //{
    //    public string lspath { get; set; }
    //    public string lsname { get; set; }
    
    //}
    public class MstApplicationReport : result
    {
        public List<MstAppSummaryList> MstAppSummaryList { get; set; }
        public List<documentpendinglist> documentpendinglist { get; set; }
        public List<MstCCSummaryList> MstCCSummaryList { get; set; }
        public List<BuyerReport_list> BuyerReport_list { get; set; }
        public List<MstDocumentChecklList> MstDocumentChecklList { get; set; }
        public List<DocumentCount> DocumentCount { get; set; }
        public string application_no { get; set; }
        public string customerref_name { get; set; }
        public string vertical_name { get; set; }
        public string applicant_type { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string customer_name { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
        public string created_date { get; set; }
        public string submitted_date { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string overalllimit_amount { get; set; }

    }
    public class MstAppSummaryList
    {
        public string application_no { get; set; }
        public string customerref_name { get; set; }
        public string vertical_name { get; set; }
        public string applicant_type { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string submitted_date { get; set; }
        public string updated_date { get; set; }
        public string overalllimit_amount { get; set; }
        public string updated_by { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
    }


    public class MstCCSummaryList
    {
        public string submitted_date { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string region { get; set; }
        public string vertical_name { get; set; }
        public string overalllimit_amount { get; set; }
        public string ccmeeting_date { get; set; }
        public string ccgroup_name { get; set; }
       
    }

    public class ApplicationListCount : result
    {
        public string count_Report { get; set; }
        public string count_submit { get; set; }
        public string count_incomplete { get; set; }
      
    }

    public class SanctionMISSummary : result
    {
        public List<SanctionMISdtl> SanctionMISdtl_list { get; set; }
        public List<hypothecation> hypothecation_list { get; set; }
    }

    public class SanctionMISdtl 
    {
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string sanction_amount { get; set; }
    }

    public class hypothecation
    {
        //public string application_gid { get; set; }
        //public string application2sanction_gid { get; set; }
        //public string application_no { get; set; }
        //public string customer_urn { get; set; }
        //public string customer_name { get; set; }
        //public string sanction_refno { get; set; }
        //public string sanction_date { get; set; }
        //public string sanction_amount { get; set; }

    public string application_no { get; set; }
    public string customer_urn { get; set; }
    public string customer_name { get; set; }
    public string region { get; set; }
    public string vertical { get; set; }
    public string constitution { get; set; }
    public string sanction_refno { get; set; }
    public string sanction_date { get; set; }
    public string sanction_amount { get; set; }
    public string applicant_type { get; set; }
    public string security_type { get; set; }
    public string security_value { get; set; }
    public string security_assessed_on { get; set; }
    public string asset_id { get; set; }
    public string roc_filling_id { get; set; }  
    public string cersai_Filling_id { get; set; }
    public string security_description { get; set; }
    public string observation_summary { get; set; }
    public string primary_security { get; set; }

    }
    public class reportcadsanctiondetails : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
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
        public DateTime ccapproved_datetime { get; set; }
        public DateTime sanctionfrom_datetime { get; set; }
        public DateTime sanctiontill_datetime { get; set; }
        public List<sanctionto_list> sanctionto_list { get; set; }
        public List<cadaddress_list> cadaddress_list { get; set; }
        public List<cadmobileno_list> cadmobileno_list { get; set; }
        public List<cademail_list> cademail_list { get; set; }
    }

    public class reportmdltemplate : result
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
    }

    public class mdlreportsanction : result
    {
        public string pending_count { get; set; }
        public string completed_count { get; set; }
        public string approved_count { get; set; }
        public List<reportsanctiondetails> reportsanctiondetails_list { get; set; }
    }

    public class reportsanctiondetails : result
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
    }

    public class ReportUploadCADDocumentname : result
    {
        public List<ReportUploadCADES_DocumentList> UploadCADES_DocumentList { get; set; }
        public List<ReportDeviationCADMail_DocumentList> DeviationCADMail_DocumentList { get; set; }
    }

    public class ReportUploadCADES_DocumentList : result
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

    public class ReportDeviationCADMail_DocumentList : result
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

    public class BuyerReport_list : result
    {
        public string buyer_gid { get; set; }
        public string buyer_code { get; set; }
        public string buyer_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string creditActive_status { get; set; }
        public string credit_status { get; set; }
        public string creditstatus_Approval { get; set; }
        public string contactperson_name { get; set; }
    }
    public class MstDocumentChecklList
    {

        public string Application_Ref_number { get; set; }
        public string Application_Name { get; set; }
        public string Customer_URN { get; set; }
        public string CC_Approved_Date { get; set; }
        public string Sanction_Reference_No { get; set; }
        public string Maker_Name { get; set; }
        public string Checker_Name { get; set; }
        public string Approver_Name { get; set; }
        public string Institution_Name { get; set; }
        public string Individual_Name { get; set; }
        public string Group_Name { get; set; }
        public string Stakeholder_Type { get; set; }
        public string Deferral_Document_Checklist { get; set; }
        public string Covenant_Document_Checklist { get; set; }
    

    }

    public class DocumentCount : result
    {
        public string cadmaker_count { get; set; }      
        public string cadchecker_count { get; set; }     
        public string cadcheckerapproval_count { get; set; }
        public string cadapproved_count { get; set; }
    }

    public class ExportExcelAddprogram : result
    {
        //public List<Company_BuyerOnboardReport_list> Company_BuyerOnboardReport_list { get; set; }
        //public List<General_BuyerOnboardReport_list> General_BuyerOnboardReport_list { get; set; }
        //public List<Individual_BuyerOnboardReport_list> Individual_BuyerOnboardReport_list { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }

    public class documentpendinglist : result
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
    }

    public class CadConsolidatedRportCount : result
    {
        public string SanctionCount { get; set; }
        public string LSACount { get; set; }
        public string DocumentCheckListCount { get; set; }
        public string SoftcopyVettingCount { get; set; }
        public string OriginalCopyVettingCount { get; set; }
        
    }
}