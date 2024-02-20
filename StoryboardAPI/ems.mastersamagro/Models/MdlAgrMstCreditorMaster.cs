using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// <summary>
// This Models provide access for various Single and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Other Creditor Master 
// </summary>
// <remarks>Written by Premchander.K </remarks>

namespace ems.mastersamagro.Models
{
    public class MdlcreditorMaster : result
    {
        public List<spocid_list> spocid_list { get; set; }
        public List<MdlcreditorCreation> MdlcreditorCreation { get; set; }

        public bool status { get; set; }
        public string message { get; set; }
        public string master_gid { get; set; }
        public string master_name { get; set; }
        public string deleted_by { get; set; }
        public string deleted_date { get; set; }
        public string master_value { get; set; }
        public string creditor_gid { get; set; }
        public string creditor2address_gid { get; set; }
        public string creditor_address { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string remarks { get; set; }

        //public string spoc_list { get; set; }
        public string spoc_phoneno { get; set; }
        public string spoc_phonenolist { get; set; }
        public string spoc_id { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string lsemployee_gid { get; set; }

        public List<creditoragreement_upload> creditoragreement_upload { get; set; }
        public List<Mdlcreditorspoc> Mdlcreditorspoc { get; set; }
        public List<Mdlcreditoragreementdtllist> Mdlcreditoragreementdtllist { get; set; }


    }

    public class Mdlcreditoragreementdtllist : result

    {
        public string creditor2agreement_gid { get; set; }
        public string samcontactperson_gid { get; set; }
        public string samcontactperson_name { get; set; }
        public string agreementinvolvement_type { get; set; }
        public string creditor2agreement_no { get; set; }
        public string samcontact_perssonname { get; set; }
        public string samcontact_perssonid { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string creditor_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string creditor2address_gid { get; set; }
        public string creditor_address { get; set; }
    }

    public class Mdlcreditorwarehouseagency : result
    {
        public List<warehouseagency_list> warehouseagency_list { get; set; }

    }

    public class warehouseagency_list
    {

        public string creditor_gid { get; set; }
        public string Applicant_name { get; set; }

    }

    public class MdlcreditorSummary : result
    {
        public List<MdlcreditorCreation> MdlcreditorCreation { get; set; }
    }

    public class MdlcreditorCreation : result

    {

        public List<multiloanproduct_list> multiloanproduct_list { get; set; }

        public List<multiloansubproduct_list> multiloansubproduct_list { get; set; }
        public List<creditor2facility_list> creditor2facility_list { get; set; }

        public string query_status { get; set; }
        public string creditor_name { get; set; }
        public string creditor_gid { get; set; }
        public string Gstflag { get; set; }
        public string creditorref_no { get; set; }
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }        
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string contactperson_name { get; set; }
        public string contact_no { get; set; }
        public string email_id { get; set; }
        public string pan_no { get; set; }
        public string Applicant_name { get; set; }
        public string Applicant_category { get; set; }
        public string Applicanttype_gid { get; set; }
        public string Applicant_type { get; set; }
        public string aadhar_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string rejected_date { get; set; }
        public string rejected_by { get; set; }
        public string approved_date { get; set; }
        public string approved_by { get; set; }
        public string approval_submitteddate { get; set; }
        public string approval_status { get; set; }
        public string creditorapproval_status { get; set; }
        public string approval_remarks { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string erp_id { get; set; }
        public string api_code { get; set; }
        public List<facility_list> facility_list { get; set; }
        public List<creditorproduct_list> creditorproduct_list { get; set; }

        public List<creditorprogram_list> creditorprogram_list { get; set; }
        //public string erp_id { get; set; }

    }

    public class creditorprogram_list

    {
        public string loanproduct_gid { get; set; }

        public string loanproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }

    }

    public class creditorproduct_list

    {
        public string loanproduct_gid { get; set; }

        public string loanproduct_name { get; set; }

    }

    public class multiloanproduct_list

    {
        public string loanproduct_gid { get; set; }

        public string loanproduct_name { get; set; }

    }

    public class multiloansubproduct_list

    {
        public string loanproduct_gid { get; set; }

        public string loanproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }

    }

    public class creditor2facility_list
    {

        public string creditorfacility_gid { get; set; }
        public string creditorfacility_name { get; set; }
    }

   
    public class creditoragreement_upload : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string creditor_gid { get; set; }
        public string creditoragreement2docupload_gid { get; set; }
        public string creditor2address_gid { get; set; }
        public string creditor_address { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string creditor2agreement_gid { get; set; }
    }

    

    public class creditorlist : result
    {
        public string creditor_gid { get; set; }
        public string creditor_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
    }
    public class creditoradd_list
    {
        public string creditor_gid { get; set; }
        public string creditor_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string sa_status { get; set; }
        public string sa_id { get; set; }
        public string sa_name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string vernacular_language { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string overalllimit_amount { get; set; }
        public string processing_fee { get; set; }
        public string doc_charges { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string productcharge_flag { get; set; }
        public string economical_flag { get; set; }
        public string creditor_status { get; set; }
        public string applicant_type { get; set; }
        public string opscreditor_gid { get; set; }
        public string updated_date { get; set; }
        public string approval_status { get; set; }
        public string submitted_date { get; set; }
        public string ccsubmitted_date { get; set; }
        public string submitted_by { get; set; }
        public string ccsubmitted_by { get; set; }
        public string region { get; set; }
        public string ccgroup_name { get; set; }
        public string ccmeeting_date { get; set; }
        public string ccmeting_time { get; set; }
        public string scheduled_date { get; set; }
        public string now_date { get; set; }
        public string cccompleted_date { get; set; }
        public string updated_by { get; set; }
        public string createdby { get; set; }
        public string creditorapproval_gid { get; set; }
        public string headapproval_status { get; set; }
        public string initiate_flag { get; set; }
        public string headapproval_date { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditheadapproval_status { get; set; }
        public string creditgroup_name { get; set; }
        public string creditassigned_date { get; set; }
        public string creditassigned_by { get; set; }
        public string creditassigned_to { get; set; }
        public string rmquery_flag { get; set; }
        public string momupdated_by { get; set; }
        public string momupdated_date { get; set; }
    }

    public class MdlcreditorMobileNo : result
    {
        public string creditor2contact_gid { get; set; }
        public string whatsapp_no { get; set; }
        public string creditor2mobileno_gid { get; set; }
        public string institution_gid { get; set; }
        public string creditor_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public List<creditormobileno_list> creditormobileno_list { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opscreditor_gid { get; set; }
        public string opscreditor2contact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class creditormobileno_list
    {
        public string creditor2contact_gid { get; set; }
        public string creditor_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string creditor2mobileno_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opscreditor2contact_gid { get; set; }
    }
    public class MdlcreditorEmailAddress : result
    {
        public string creditor2email_gid { get; set; }
        public string creditor_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string institution_gid { get; set; }
        public List<creditoremailaddress_list> creditoremailaddress_list { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opscreditor2email_gid { get; set; }
        public string opscreditor_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class creditoremailaddress_list
    {
        public string creditor2email_gid { get; set; }
        public string creditor_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opscreditor2email_gid { get; set; }
        public string opscreditor_gid { get; set; }
    }


    public class creditorDocumentname : result
    {
        public List<creditorDocumentList> creditorDocumentList { get; set; }
    }
    public class creditorDocumentList
    {
        public string creditorDocument_name { get; set; }
        public string creditorDocument_path { get; set; }
        public string creditorDocument_gid { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string creditorDocument_type { get; set; }
        public string updated_date { get; set; }
        public string creditorDocument_title { get; set; }
        public string creditor2hypothecation_gid { get; set; }
        public string creditor2collateral_gid { get; set; }
        public string creditor2loan_gid { get; set; }
        public string opscreditor2hypothecation_gid { get; set; }
    }

    public class MdlcreditorGST : result
    {
        public string creditor2branch_gid { get; set; }
        public string creditor_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_no { get; set; }
        public string gst_registered { get; set; }
        public creditorGSTDetails[] GSTArray { get; set; }
        public List<creditorgst_list> creditorgst_list { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class MdlCreditorGSTHeadOffice : result
    {
        public string creditor2branch_gid { get; set; }
        public string creditor_gid { get; set; }
        public string employee_gid { get; set; }
    }
    public class creditorgst_list
    {
        public string creditor2branch_gid { get; set; }
        public string creditor_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_registered { get; set; }
        public string headoffice_status { get; set; }
        public string gst_no { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string state_code { get; set; }
        public string authentication_status { get; set; }
        public string returnfilling_status { get; set; }
        public string verification_status { get; set; }
    }
    public class creditorGSTDetails
    {
        public string authStatus { get; set; }
        public string applicationStatus { get; set; }
        public string emailId { get; set; }
        public string gstinId { get; set; }
        public string gstinRefId { get; set; }
        public string mobNum { get; set; }
        public string pan { get; set; }
        public string regType { get; set; }
        public string registrationName { get; set; }
        public string tinNumber { get; set; }
    }

    public class MdlcreditorAddressDetails : result
    {
        public string address_type { get; set; }
        public string primary_address { get; set; }
        public string primary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_gid { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address_typegid { get; set; }
        public string landmark { get; set; }
        public string creditor2address_gid { get; set; }
        public string creditor_gid { get; set; }
        public string group2address_gid { get; set; }
        public string group_gid { get; set; }
        public List<creditoraddress_list> creditoraddress_list { get; set; }
        public string opsinstitution2address_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class creditoraddress_list
    {
        public string creditor2address_gid { get; set; }
        public string group2address_gid { get; set; }
        public string address_type { get; set; }
        public string primary_address { get; set; }
        public string primary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_gid { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address_typegid { get; set; }
        public string landmark { get; set; }
        public string opsinstitution2address_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
    }

    public class MdlcreditorBankDetails : result
    {
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string group2bank_gid { get; set; }
        public string group_gid { get; set; }
        public List<creditorbank_list> creditorbank_list { get; set; }
        public string opsgroup2bank_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class creditorbank_list
    {
        public string group2bank_gid { get; set; }
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string opsgroup2bank_gid { get; set; }
    }


    public class creditorCount
    {
        public string newcreditor_count { get; set; }
        public string rejected_count { get; set; }
        public string hold_count { get; set; }
        public string ccapproved_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class AssigncreditorCount
    {
        public string pending_count { get; set; }
        public string assigned_count { get; set; }
        public string submittedtocc_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }


   
    public class MdlcreditorSectorcategory : result
    {
        public List<creditorvarietyname_list> creditorvarietyname_list { get; set; }
    }


    public class creditorvarietyname_list : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string creditor2commodity_gid { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string creditor_gid { get; set; }
        public string hsn_code { get; set; }
    }

    public class Mdlcreditorspoc : result
    {
        public List<creditorspoc_list> creditorspoc_list { get; set; }

        public string creditor2spoc_gid { get; set; }
        public string creditor_gid { get; set; }
        public string spoc_id { get; set; }
        public string spoc_name { get; set; }
        public string spocmobile_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }

    public class creditorspoc_list : result
    {

        public string creditor2spoc_gid { get; set; }
        public string creditor_gid { get; set; }
        public string spoc_id { get; set; }
        public string spoc_name { get; set; }
        public string spocmobile_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }

    public class creditorapprovaldtl
    {
        public List<creditorapproval_list> creditorapproval_list { get; set; }
    }

    public class creditorapproval_list : result
    {
        public string approval_name { get; set; }
        public string approval_remarks { get; set; }
        public string approved_date { get; set; }
        public string approval_flag { get; set; }

    }

    public class MdlcreditorEmployeeList
    {
        public List<employeedtl> employeedtl { get; set; }
    }

    public class creditoremployeedtl
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

    public class Mdlcreditorcheque : result
    {
        public string creditor2cheque_gid { get; set; }
        public string creditor_gid { get; set; }
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
        public List<creditorcheque_list> creditorcheque_list { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string primary_status { get; set; }
    }

    public class creditorcheque_list
    {
        public string creditor2cheque_gid { get; set; }
        public string creditor_gid { get; set; }
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
        public string date_creditorchequetype { get; set; }
        public string date_creditorchequepresentation { get; set; }
        public string status_creditorchequepresentation { get; set; }
        public string date_creditorchequeclearance { get; set; }
        public string status_creditorchequeclearance { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string primary_status { get; set; }

    }


    public class MdlcreditorchequeDocument : result
    {
        public string creditorcheque2document_gid { get; set; }
        public string creditor2cheque_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<creditorchequedocument_list> creditorchequedocument_list { get; set; }
    }

    public class creditorchequedocument_list
    {
        public string creditorcheque2document_gid { get; set; }
        public string creditor2cheque_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }

    }

    public class creditorchequeuploaddocument : result
    {
        public List<chequeupload_list> chequeupload_list { get; set; }
    }
    public class chequeupload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    //    public class MdlDropDownUdc : result
    //    {
    //        public List<bankname_list> bankname_list { get; set; }

    //    }

    //    public class bankname_list
    //    {
    //        public string bankname_gid { get; set; }
    //        public string bankname_name { get; set; }
    //    }
    //}

    public class Mdlcreditor2warehouse : result
    {
        public List<creditor2warehouse_list> creditor2warehouse_list { get; set; }

        public List<trade2creditor_list> trade2creditor_list { get; set; }
        public string creditor_gid { get; set; }
        public string Applicant_name { get; set; }

        public string applicationtrade2warehouse_gid { get; set; }
        public string application2trade_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string application_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string warehouse_agency { get; set; }
        public string warehouse_name { get; set; }
        public string typeofwarehouse_name { get; set; }
        public string volume_uomgid { get; set; }
        public string volume_uom { get; set; }
        public string totalcapacity_volume { get; set; }
        public string totalcapacity_area { get; set; }
        public string totalcapacityarea_uomgid { get; set; }
        public string area_uom { get; set; }
        public string warehouse2address_gid { get; set; }
        public string warehouse_address { get; set; }
        public string capacity_commodity { get; set; }
        public string capacity_panina { get; set; }

        public string Applicant_category { get; set; }

        public string designation_type { get; set; }

        public string contactperson_name { get; set; }

        public string contact_no { get; set; }

        public string email_id { get; set; }

        public string pan_no { get; set; }

    }

    public class trade2creditor_list
    {

        public string creditor_gid { get; set; }
        public string Applicant_name { get; set; }

        public string applicationtrade2creditor_gid { get; set; }
        public string applicationtrade2warehouse_gid { get; set; }
        public string application2trade_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string application_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string warehouse_agency { get; set; }
        public string warehouse_name { get; set; }
        public string typeofwarehouse_name { get; set; }
        public string volume_uomgid { get; set; }
        public string volume_uom { get; set; }
        public string totalcapacity_volume { get; set; }
        public string totalcapacity_area { get; set; }
        public string totalcapacityarea_uomgid { get; set; }
        public string area_uom { get; set; }
        public string warehouse2address_gid { get; set; }
        public string warehouse_address { get; set; }
        public string capacity_commodity { get; set; }
        public string capacity_panina { get; set; }

        public string Applicant_category { get; set; }

        public string designation_type { get; set; }

        public string contactperson_name { get; set; }

        public string contact_no { get; set; }

        public string email_id { get; set; }

        public string pan_no { get; set; }
    }

    public class creditor2warehouse_list
    {

        public string creditor_gid { get; set; }
        public string Applicant_name { get; set; }

        public string applicationtrade2warehouse_gid { get; set; }  
        public string application2trade_gid { get; set; }  
        public string application2loan_gid { get; set; }  
        public string application_gid { get; set; } 
        public string warehouse_gid { get; set; }
        public string warehouse_agency { get; set; }
        public string warehouse_name { get; set; }
        public string typeofwarehouse_name { get; set; }
        public string volume_uomgid { get; set; }
        public string volume_uom { get; set; }
        public string totalcapacity_volume { get; set; }
        public string totalcapacity_area { get; set; }
        public string totalcapacityarea_uomgid { get; set; }
        public string area_uom { get; set; }
        public string warehouse2address_gid { get; set; }
        public string warehouse_address { get; set; }
        public string capacity_commodity { get; set; }
        public string capacity_panina { get; set; }


    }


    public class Mdlcreditorapproval : result
    {

        public string creditor_gid { get; set; }
        public string approval_submittedflag { get; set; }
        public string approval_submitteddate { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string initiated_remarks { get; set; }
        public List<Mdlcreditorapproval_list> Mdlcreditorapproval_list { get; set; }

    }

    public class Mdlcreditorapproval_list
    {
        public string creditor_gid { get; set; }
        public string approval_submittedflag { get; set; }
        public string approval_submitteddate { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
    }

    public static class CreditorStatus
    {
        public const string
              Pending = "N",
              Approved = "Y",
              Rejected = "R";
    }

    public class RMCreditorCountdtl : result
    {
        public string Open_Creditor { get; set; }
        public string ApprovalPending_Creditor { get; set; }
        public string Approved_Creditor { get; set; }
        public string Rejected_Creditor { get; set; }
        public string PendingProduct_Creditor { get; set; }
        public string PendingPMG_Warehouse { get; set; }
        public string Total_Creditor { get; set; }
    }


    public class mdlcreditorraisequery : result
    {
        public string creditorquery_gid { get; set; }
        public string creditor_gid { get; set; }
        public string query_title { get; set; }
        public string description { get; set; }
        public string openquery_flag { get; set; }
        public string close_remarks { get; set; }
        public List<creditorraisequerylist> creditorraisequerylist { get; set; }
    }

    public class creditorraisequerylist
    {
        public string creditorquery_gid { get; set; }
        public string creditor_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string query_status { get; set; }
        public string close_remarks { get; set; }
    }


}


