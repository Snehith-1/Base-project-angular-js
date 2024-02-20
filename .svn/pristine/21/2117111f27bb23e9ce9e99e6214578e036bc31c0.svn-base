using System;
using System.Collections.Generic;

namespace ems.master.Models
{
    public class MdlOpsApplicationView : result
    {
        public string opsapplication_gid { get; set; }
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
        public string application_no { get; set; }
        public string vernacular_language { get; set; }
        public string primary_mobileno { get; set; }
        public string primary_email { get; set; }
        public string landline_no { get; set; }
        public string designation_type { get; set; }
        public string contactperson_name { get; set; }
        public string primaryvaluechain_name { get; set; }
        public string secondaryvaluechain_name { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string borrower_flag { get; set; }
        public string borrower_type { get; set; }

        public List<opsmobilenumber_list> opsmobilenumber_list { get; set; }
        public List<opsmail_list> opsmail_list { get; set; }
        public List<opsgeneticdetails_list> opsgeneticdetails_list { get; set; }
    }
    public class opsmobilenumber_list : result
    {
        public string opsapplication_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_mobileno { get; set; }
    }
    public class opsmail_list : result
    {
        public string opsapplication_gid { get; set; }
        public string opsapplication2email_gid { get; set; }
        public string email_address { get; set; }
    }
    public class opsgeneticdetails_list : result
    {
        public string opsapplication_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }
    }
    public class MdlOPSInstitutionDtlView : result
    {
        public string opsapplication_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string date_incorporation { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string cin_no { get; set; }
        public string official_telephoneno { get; set; }
        public string officialemail_address { get; set; }
        public string companytype_name { get; set; }
        public string escrow { get; set; }
        public string lastyear_turnover { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string ratingas_on { get; set; }
        public string amlcategory_name { get; set; }
        public string businesscategory_name { get; set; }
        public string primaryinstitution_mobileno { get; set; }
        public string primaryinstitution_email { get; set; }

        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string bureauscore_date { get; set; }
        public string cicdocument_name { get; set; }
        public string cicdocument_path { get; set; }
        public string borrower_type { get; set; }

        public string urn_status { get; set; }
        public string urn { get; set; }

        public List<opsgst_list> opsgst_list { get; set; }
        public List<opsinstituionmail_list> opsinstituionmail_list { get; set; }
        public List<opsinstituionmobilenumber_list> opsinstituionmobilenumber_list { get; set; }
        public List<opsaddress_list> opsaddress_list { get; set; }
        public List<opsinstitutionform60_list> opsinstitutionform60_list { get; set; }
        public List<opsinstitutiondoc_list> opsinstitutiondoc_list { get; set; }
        public List<opslicense_list> opslicense_list { get; set; }
    }
    public class opsgst_list
    {
        public string opsinstitution2branch_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_registered { get; set; }
        public string gst_no { get; set; }
    }
    public class opsinstituionmail_list : result
    {
        public string email_address { get; set; }
    }
    public class opsinstituionmobilenumber_list : result
    {
        public string opsinstitution_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_no { get; set; }
    }
    public class opsaddress_list
    {
        public string opsinstitution2address_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
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
        public string address_typegid { get; set; }
        public string landmark { get; set; }
    }
    public class opsinstitutionform60_list : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string opsinstitution2form60documentupload_gid { get; set; }
    }

    public class opsinstitutiondoc_list : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
        public string document_id { get; set; }
        public string opsinstitution2documentupload_gid { get; set; }
    }
    public class opslicense_list
    {
        public string opsinstitution2licensedtl_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string licensetype_gid { get; set; }
        public string licensetype_name { get; set; }
        public string license_number { get; set; }
        public string licenseissue_date { get; set; }
        public string licenseexpiry_date { get; set; }
    }
    public class MdlOPSIndividualDtlView : result
    {
        public string opsapplication_gid { get; set; }
        public string borrower_type { get; set; }
        public string opscontact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_dob { get; set; }
        public string age { get; set; }
        public string gender_name { get; set; }
        public string main_occupation { get; set; }
        public string pep_status { get; set; }
        public string pepverified_date { get; set; }
        public string maritalstatus_name { get; set; }
        public string father_name { get; set; }
        public string father_dob { get; set; }
        public string father_age { get; set; }
        public string mother_name { get; set; }
        public string mother_dob { get; set; }
        public string mother_age { get; set; }
        public string spouse_name { get; set; }
        public string spouse_dob { get; set; }
        public string spouse_age { get; set; }
        public string educationalqualification_name { get; set; }
        public string annual_income { get; set; }
        public string monthly_income { get; set; }
        public string user_type { get; set; }
        public string ownershiptype_name { get; set; }
        public string propertyholder_name { get; set; }
        public string residencetype_name { get; set; }
        public string currentresidence_years { get; set; }
        public string branch_distance { get; set; }
        public string primaryindividual_mobileno { get; set; }
        public string primaryindividual_email { get; set; }

        public string bureauname_gid { get; set; }
        public string indbureauname_name { get; set; }
        public string indbureau_score { get; set; }
        public string indobservations { get; set; }
        public string indbureauscore_date { get; set; }
        public string indbureau_response { get; set; }
        public string indcicdocument_name { get; set; }
        public string indcicinddocument_path { get; set; }

        public string group_name { get; set; }
        public string profile { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string fathernominee_status { get; set; }
        public string mothernominee_status { get; set; }
        public string spousenominee_status { get; set; }
        public string othernominee_status { get; set; }
        public string relationshiptype { get; set; }
        public string nomineefirst_name { get; set; }
        public string nominee_middlename { get; set; }
        public string nominee_lastname { get; set; }
        public string nominee_dob { get; set; }
        public string nominee_age { get; set; }
        public string totallandinacres { get; set; }
        public string cultivatedland { get; set; }
        public string previouscrop { get; set; }
        public string prposedcrop { get; set; }
        public string institution_name { get; set; }

        public List<opscontactaddress_list> opscontactaddress_list { get; set; }
        public List<opscontactemail_list> opscontactemail_list { get; set; }
        public List<opscontactmobileno_list> opscontactmobileno_list { get; set; }
        public List<opscontactidproof_list> opscontactidproof_list { get; set; }
        public List<uploadopsindividualdoc_list> uploadopsindividualdoc_list { get; set; }
    }
    public class opscontactaddress_list
    {
        public string opscontact2address_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string primary_status { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
    public class opscontactemail_list
    {
        public string opscontact2email_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }
    }
    public class opscontactmobileno_list
    {
        public string opscontact2mobileno_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
    }
    public class opscontactidproof_list
    {
        public string opscontact2idproof_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string idproof_gid { get; set; }
        public string idproof_name { get; set; }
        public string idproof_no { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
    }
    public class uploadopsindividualdoc_list
    {
        public string opscontact2document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
    }
    public class MdlOPSCreditView : result
    {
        public List<opsindividual_List> opsindividual_List { get; set; }
        public List<opsinstitution_List> opsinstitution_List { get; set; }
    }
    public class opsindividual_List : result
    {
        public string opscontact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_dob { get; set; }
        public string main_occupation { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string stakeholder_type { get; set; }
        public string company_name { get; set; }
    }
    
    public class opsinstitution_List : result
    {
        public string opsinstitution_gid { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string cin_no { get; set; }
        public string companytype_name { get; set; }
        public string date_incorporation { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string stakeholder_type { get; set; }
    }
    
    public class MdlOPSRMDtlView : result
    {
        public string department_name { get; set; }
        public string RM_Name { get; set; }
        public string opsapplicationinitiated_date { get; set; }
    }
    public class MdlOPSProductChargesView : result
    {
        public string overalllimit_amount { get; set; }
        public string validityoveralllimit_year { get; set; }
        public string validityoveralllimit_month { get; set; }
        public string validityoveralllimit_days { get; set; }
        public string calculationoveralllimit_validity { get; set; }
        public string enduse_purpose { get; set; }
        public string opsapplication2hypothecation_gid { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string security_value { get; set; }
        public string securityassessed_date { get; set; }
        public string asset_id { get; set; }
        public string roc_fillingid { get; set; }
        public string CERSAI_fillingid { get; set; }
        public string hypoobservation_summary { get; set; }
        public string primary_security { get; set; }

        public List<opsLoan_list> opsLoan_list { get; set; }
        public List<opscollateral_list> opscollateral_list { get; set; }
        public List<opshypo_list> opshypo_list { get; set; }
        public List<opsHypoDocumentList> opsHypoDocumentList { get; set; }
        public List<opsCollatralDocumentList> opsCollatralDocumentList { get; set; }
        public List<opsbuyer_list> opsbuyer_list { get; set; }
        public List<opsservicecharge_List> opsservicecharge_List { get; set; }

    }
    public class opsLoan_list : result
    {
        public string opsapplication2loan_gid { get; set; }
        public string facilityrequested_date { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
        public string loanfacility_amount { get; set; }
        public string loan_type { get; set; }
        public string rate_interest { get; set; }
        public string penal_interest { get; set; }
        public string facilityoverall_limit { get; set; }
        public string tenureoverall_limit { get; set; }
        public string facility_type { get; set; }
        public string facility_mode { get; set; }
        public string principalfrequency_name { get; set; }
        public string interestfrequency_name { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public string scheme_type { get; set; }

    }
    public class opscollateral_list : result
    {
        public string opsapplication2loan_gid { get; set; }
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string collateralobservation_summary { get; set; }
    }
    public class opshypo_list : result
    {
        public string opsapplication2hypothecation_gid { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string security_value { get; set; }
        public string securityassessed_date { get; set; }
        public string asset_id { get; set; }
        public string roc_fillingid { get; set; }
        public string CERSAI_fillingid { get; set; }
        public string hypoobservation_summary { get; set; }
        public string primary_security { get; set; }
    }
    public class opsHypoDocumentList
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
        public string document_title { get; set; }
        public string opsapplication2hypothecation_gid { get; set; }
        public string opsapplication2collateral_gid { get; set; }
    }
    public class opsCollatralDocumentList
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
        public string document_title { get; set; }
        public string opsapplication2collateral_gid { get; set; }
    }
    public class opsbuyer_list
    {
        public string opsapplication2buyer_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string balance_limit { get; set; }
        public string bill_tenure { get; set; }
        public string margin { get; set; }
    }
    public class opsservicecharge_List : result
    {
        public string processing_fee { get; set; }
        public string processing_collectiontype { get; set; }
        public string doc_charges { get; set; }
        public string doccharge_collectiontype { get; set; }
        public string fieldvisit_charge { get; set; }
        public string fieldvisit_collectiontype { get; set; }
        public string adhoc_fee { get; set; }
        public string adhoc_collectiontype { get; set; }
        public string life_insurance { get; set; }
        public string lifeinsurance_collectiontype { get; set; }
        public string acct_insurance { get; set; }
        public string total_collect { get; set; }
        public string total_deduct { get; set; }
        public string producttype_gid { get; set; }
        public string product_type { get; set; }
    }
    public class MdlOPSGroup : result
    {
        public string group_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string group_name { get; set; }
        public string date_of_formation { get; set; }
        public string group_type { get; set; }
        public string groupmember_count { get; set; }
        public string groupdocument_name { get; set; }
        public string groupurn_status { get; set; }
        public string group_urn { get; set; }
        public string group_status { get; set; }
        public List<opsgroup_list> opsgroup_list { get; set; }
    }
    public class opsgroup_list
    {
        public string opsgroup_gid { get; set; }
        public string group_name { get; set; }
        public string date_of_formation { get; set; }
        public string grouptype_gid { get; set; }
        public string grouptype_name { get; set; }
        public string groupmember_count { get; set; }
        public string groupdocument_name { get; set; }
        public string groupurn_status { get; set; }
        public string group_urn { get; set; }
        public string group_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string group_type { get; set; }
    }
    public class MdlOPSGroupMember : result
    {
        public string opsgroup_gid { get; set; }
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string stakeholder_type { get; set; }
        public List<opsgroupmember_list> opsgroupmember_list { get; set; }

    }
    public class opsgroupmember_list : result
    {
        public string opsgroup_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string stakeholder_type { get; set; }
    }
    public class MdlOPSAddressDetails : result
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
        public string address_typegid { get; set; }
        public string landmark { get; set; }
        public string institution2address_gid { get; set; }
        public string institution_gid { get; set; }
        public string group2address_gid { get; set; }
        public string group_gid { get; set; }
        public List<opsaddress_list> opsaddress_list { get; set; }
    }
    public class MdOPSBankDetails : result
    {
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string opsgroup2bank_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public List<opsbank_list> opsbank_list { get; set; }
    }
    public class opsbank_list
    {
        public string opsgroup2bank_gid { get; set; }
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
    }
    public class OPSGroupDocument : result
    {
        public string opsgroup2document_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<opsgroupdocument_list> opsgroupdocument_list { get; set; }
    }

    public class opsgroupdocument_list
    {
        public string opsgroup2document_gid { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlOPSContact : result
    {
        public string opscontact_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string application_no { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string individual_dob { get; set; }
        public string age { get; set; }
        public string gender_gid { get; set; }
        public string gender_name { get; set; }
        public string educationalqualification_gid { get; set; }
        public string educationalqualification_name { get; set; }
        public string main_occupation { get; set; }
        public string annual_income { get; set; }
        public string monthly_income { get; set; }
        public string pep_status { get; set; }
        public string pepverified_date { get; set; }
        public string stakeholdertype_gid { get; set; }
        public string stakeholdertype_name { get; set; }
        public string stakeholder_type { get; set; }
        public string user_type { get; set; }
        public string maritalstatus_gid { get; set; }
        public string maritalstatus_name { get; set; }
        public string father_firstname { get; set; }
        public string father_middlename { get; set; }
        public string father_lastname { get; set; }
        public string father_dob { get; set; }
        public string father_age { get; set; }
        public string mother_firstname { get; set; }
        public string mother_middlename { get; set; }
        public string mother_lastname { get; set; }
        public string mother_dob { get; set; }
        public string mother_age { get; set; }
        public string spouse_firstname { get; set; }
        public string spouse_middlename { get; set; }
        public string spouse_lastname { get; set; }
        public string spouse_dob { get; set; }
        public string spouse_age { get; set; }
        public string ownershiptype_gid { get; set; }
        public string ownershiptype_name { get; set; }
        public string propertyholder_gid { get; set; }
        public string propertyholder_name { get; set; }
        public string residencetype_gid { get; set; }
        public string residencetype_name { get; set; }
        public string currentresidence_years { get; set; }
        public string branch_distance { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string cicuploaddocument_name { get; set; }
        public string cicuploaddocument_path { get; set; }
        public string contact_status { get; set; }
        public string incometype_gid { get; set; }
        public string incometype_name { get; set; }
        public DateTime spousedob { get; set; }
        public DateTime motherdob { get; set; }
        public DateTime fatherdob { get; set; }
        public DateTime pepverifieddate { get; set; }
        public DateTime individualdob { get; set; }
        public List<opscontact_list> opscontact_list { get; set; }
        public List<opscicuploadindividual_list> opscicuploadindividual_list { get; set; }
        public List<uploadopsindividualdoc_list> uploadopsindividualdoc_list { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string profile { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string fathernominee_status { get; set; }
        public string mothernominee_status { get; set; }
        public string spousenominee_status { get; set; }
        public string othernominee_status { get; set; }
        public string relationshiptype { get; set; }
        public string nomineefirst_name { get; set; }
        public string nominee_middlename { get; set; }
        public string nominee_lastname { get; set; }
        public string nominee_dob { get; set; }
        public string nominee_age { get; set; }
        public string totallandinacres { get; set; }
        public string cultivatedland { get; set; }
        public string previouscrop { get; set; }
        public string prposedcrop { get; set; }
        public string institution_gid { get; set; }
        public string institution_name { get; set; }
    }
    public class opscontact_list
    {
        public string opscontact_gid { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string individual_name { get; set; }
        public string stakeholder_type { get; set; }
        public string contact_status { get; set; }
    }
    public class opscicuploadindividual_list
    {
        public string opscontact_gid { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
    }
    public class MdlOPSContactAddress : result
    {
        public string contact2address_gid { get; set; }
        public string contact_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string primary_status { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public List<opscontactaddress_list> opscontactaddress_list { get; set; }
    }
    public class MdlOPSContactIdProof : result
    {
        public string opscontact2idproof_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string idproof_gid { get; set; }
        public string idproof_name { get; set; }
        public string idproof_no { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<opscontactidproof_list> opscontactidproof_list { get; set; }
        public List<uploadopsindividualdoc_list> uploadopsindividualdoc_list { get; set; }
    }
    public class MdlOPSContactMobileNumber : result
    {
        public string primaryindividual_mobileno { get; set; }
        public List<opsindividualmobileno_list> opsindividualmobileno_list { get; set; }

    }
    public class opsindividualmobileno_list : result
    {
        public string opscontact_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_no { get; set; }
    }
    public class MdlOPSContactEmail : result
    {
        public string primaryindividual_email { get; set; }
        public List<opsindividualemail_list> opsindividualemail_list { get; set; }
    }
    public class opsindividualemail_list : result
    {
        public string email_address { get; set; }
    }
    public class MdlOPSContactBureau : result
    {
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string bureauscore_date { get; set; }
        public string cicdocument_name { get; set; }
        public string cicdocument_path { get; set; }
    }
}