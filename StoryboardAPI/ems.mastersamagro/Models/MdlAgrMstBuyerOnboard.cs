using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This models will store values to add single and multiple datas in Buyer/Supplier Onboard stage.  (Includes overall summary adding of onboarded buyer general, company & individual info & initiate, approve & reject records)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K </remarks>

    public static class OnboardAppStatus
    {
        public const string
              Pending = "N",
              Approved = "Y",
              Rejected = "R";
    } 
    public class MdlMstOnboardApplicationlist : result
    { 
        public List<onboardapplicationdtl> onboardapplicationdtl { get; set; } 
    }

    public class onboardapplicationdtl
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string vertical_name { get; set; }
        public string onboard_approvalstatus { get; set; }
        public string applicant_type { get; set; } 
        public string submittedto_approval { get; set; }
        public string onboard_applicationstatus { get; set; }
        public string onboard_approvedby { get; set; }
        public string onboard_approveddate { get; set; }
        public string application_initiateddate { get; set; }
        public string application_initiatedby { get; set; }
        public string virtualaccount_number { get; set; }
        public string lgltag_status { get; set; }
        public string erp_id { get; set; }
        public string initiated_by { get; set; }
        public string submitted_by { get; set; }
        public string vacreation_status { get; set; }
        public string query_status { get; set; }

        public string sourced_by { get; set; }
    }

    public class MdlOnboardApproval :result
    {
        public string application_gid { get; set; }
        public string approval_remarks { get; set; }
        public string approval_status { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string onboarding_status { get; set; }
    }

    public class MdlOnboardApprovalCountdtl : result
    {
        public string BuyerApprovalPending { get; set; }
        public string BuyerApproved { get; set; }
        public string SupplierApprovalPending { get; set; }
        public string SupplierApproved { get; set; }
        public string BuyerRejected { get; set; }
        public string SupplierRejected { get; set; }
    }

    public class MdlMstBuyerOnboardProductDetailAdd : result
    {
        public string application2product_gid { get; set; }
        public string application_gid { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string application2loan_gid { get; set; }
        public string hsn_code { get; set; }
        public string unitpricevalue_commodity { get; set; }
        public string natureformstate_commoditygid { get; set; }
        public string natureformstate_commodity { get; set; }
        public string qualityof_commodity { get; set; }
        public string quantity { get; set; }
        public string uom_gid { get; set; }
        public string uom_name { get; set; }
        public string milestone_applicability { get; set; }
        public string insurance_applicability { get; set; }
        public string milestonepayment_gid { get; set; }
        public string milestonepayment_name { get; set; }
        public string sa_payout { get; set; }
        public string insurance_availability { get; set; }
        public string insurance_percent { get; set; }
        public string insurance_cost { get; set; }
        public string net_yield { get; set; }
        public string markto_marketvalue { get; set; }
        public string pricereference_source { get; set; }
        public string headingdesc_product { get; set; }
        public string typeofsupply_naturegid { get; set; }
        public string typeofsupply_naturename { get; set; }
        public string sectorclassification_gid { get; set; }
        public string sectorclassification_name { get; set; }
        public string creditperiod_years { get; set; }
        public string creditperiod_months { get; set; }
        public string creditperiod_days { get; set; }
        public string overallcreditperiod_limit { get; set; }
        public string commodity_margin { get; set; }
        public string commoditynet_yield { get; set; }
        public List<mstBuyerOnboardproduct_list> mstBuyerOnboardproduct_list { get; set; }
    }
    public class MdlMstBuyerOnboardProductDetailList : result
    {

        public List<mstBuyerOnboardproduct_list> mstBuyerOnboardproduct_list { get; set; }
    }
    public class mstBuyerOnboardproduct_list
    {
        public string application2product_gid { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string application2loan_gid { get; set; }
        public string hsn_code { get; set; }
        public string unitpricevalue_commodity { get; set; }
        public string natureformstate_commodity { get; set; }
        public string natureformstate_commoditygid { get; set; }
        public string qualityof_commodity { get; set; }
        public string quantity { get; set; }
        public string uom_gid { get; set; }
        public string uom_name { get; set; }
        public string headingdesc_product { get; set; }
        public string typeofsupply_naturename { get; set; }
        public string sectorclassification_name { get; set; }
    }

    public class MdlMstBuyerOnboardMobileNo : result
    {
        public string application2contact_gid { get; set; }
        public string whatsapp_no { get; set; }
        public string institution2mobileno_gid { get; set; }
        public string institution_gid { get; set; }
        public string application_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public List<mstBuyerOnboardmobileno_list> mstBuyerOnboardmobileno_list { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string opsapplication2contact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstBuyerOnboardmobileno_list
    {
        public string application2contact_gid { get; set; }
        public string application_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string institution2mobileno_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opsapplication2contact_gid { get; set; }
    }
    public class MdlMstBuyerOnboardEmailAddress : result
    {
        public string application2email_gid { get; set; }
        public string application_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string institution_gid { get; set; }
        public List<mstBuyerOnboardemailaddress_list> mstBuyerOnboardemailaddress_list { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsapplication2email_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstBuyerOnboardemailaddress_list
    {
        public string application2email_gid { get; set; }
        public string application_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opsapplication2email_gid { get; set; }
        public string opsapplication_gid { get; set; }
    }
    public class MdlMstBuyerOnboardGeneticCode : result
    {
        public string application2geneticcode_gid { get; set; }
        public string application_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }
        public string geneticcode_gid { get; set; }
        public List<mstBuyerOnboardgeneticcode_list> mstBuyerOnboardgeneticcode_list { get; set; }
        public string opsapplication2geneticcode_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstBuyerOnboardgeneticcode_list
    {
        public string application2geneticcode_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }
        public string geneticcode_gid { get; set; }
        public string application_gid { get; set; }
        public string opsapplication2geneticcode_gid { get; set; }
        public string opsapplication_gid { get; set; }
    }

    public class MdlMstBuyerOnboardApplicationAdd : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
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
        public string saname_gid { get; set; }
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
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string application_status { get; set; }
        public string proceed_flag { get; set; }
        public string applicant_type { get; set; }
        public string economical_flag { get; set; }
        public string productcharge_flag { get; set; }
        public string productcharges_status { get; set; }
        public string loanfacility_amount { get; set; }
        public string hypothecation_flag { get; set; }
        public string approval_submittedflag { get; set; }
        public List<vernacularlanguage_list> vernacularlanguage_list { get; set; }
        public List<vernacularlang_list> vernacularlang_list { get; set; }
        public List<applicationadd_list> applicationadd_list { get; set; }
        public List<applicationlist> applicationlist { get; set; }
        public List<basicdetails_list> basicdetails_list { get; set; }

        public string dob;
        public string opsapplication_gid { get; set; }
        public string opsapplication_no { get; set; }
        public string statusupdated_by { get; set; }
        public string cluster_head { get; set; }
        public string regional_head { get; set; }
        public string zonal_head { get; set; }
        public string business_head { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string approveinitiated_flag { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string onboarding_status { get; set; }
        public string buyersuppliertype_gid { get; set; }
        public string buyersuppliertype_name { get; set; }

    }

  
    public class BuyerOnboardapplicationadd_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
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
        public string application_status { get; set; }
        public string applicant_type { get; set; }
        public string opsapplication_gid { get; set; }
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
        public string applicationapproval_gid { get; set; }
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
        public string productdesk_gid { get; set; }
        public string productquery_status { get; set; }
        public string productdesk_name { get; set; }
        public string product_managername { get; set; }
    }
    //public class basicdetails_list
    //{
    //    public string application_no { get; set; }
    //    public string application_gid { get; set; }
    //    public string customer_urn { get; set; }
    //    public string customer_name { get; set; }
    //    public string created_date { get; set; }
    //    public string created_by { get; set; }
    //    public string application_status { get; set; }
    //    public string product_gid { get; set; }
    //    public string variety_gid { get; set; }
    //}

    public class MdlMstBuyerOnboardGST : result
    {
        public string institution2branch_gid { get; set; }
        public string institution_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_no { get; set; }
        public string gst_registered { get; set; }
        public string headoffice_status { get; set; }
        public InstitutionBuyerOnboardGSTDetails[] GSTArray { get; set; }
        public List<mstBuyerOnboardgst_list> mstBuyerOnboardgst_list { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstBuyerOnboardgst_list
    {
        public string headoffice_status { get; set; }
        public string institution2branch_gid { get; set; }
        public string institution_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_registered { get; set; }
        public string gst_no { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string state_code { get; set; }
        public string authentication_status { get; set; }
        public string returnfilling_status { get; set; }
        public string verification_status { get; set; }
    }
    public class InstitutionBuyerOnboardGSTDetails
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
    public class MdlMstBuyerOnboardLicenseDetails : result
    {
        public string institution2licensedtl_gid { get; set; }
        public string institution_gid { get; set; }
        public string licensetype_gid { get; set; }
        public string licensetype_name { get; set; }
        public string license_number { get; set; }
        public string licenseissue_date { get; set; }
        public string licenseexpiry_date { get; set; }
        public string licenseissue_dateedit { get; set; }
        public string licenseexpiry_dateedit { get; set; }
        public List<mstBuyerOnboardlicense_list> mstBuyerOnboardlicense_list { get; set; }
        public string opsinstitution2licensedtl_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstBuyerOnboardlicense_list
    {
        public string institution2licensedtl_gid { get; set; }
        public string institution_gid { get; set; }
        public string licensetype_gid { get; set; }
        public string licensetype_name { get; set; }
        public string license_number { get; set; }
        public string licenseissue_date { get; set; }
        public string licenseexpiry_date { get; set; }
        public string opsinstitution2licensedtl_gid { get; set; }
    }
    public class institutionBuyerOnboarduploaddocument : result
    {
        public string institution2form60documentupload_gid { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string institution_gid { get; set; }
        public List<institutionupload_list> institutionupload_list { get; set; }
    }
    public class institutionBuyerOnboardupload_list
    {
        public string institution2documentupload_gid { get; set; }
        public string institution_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
        public string document_id { get; set; }
        public string institution2form60documentupload_gid { get; set; }
        public string uploaded_by { get; set; }
        public string uploaded_date { get; set; }
        public string opsinstitution2documentupload_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsinstitution2form60documentupload_gid { get; set; }
    }
    public class MdlMstBuyerOnboardInstitutionAdd : result
    {
        public string Gstflag { get; set; }
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string date_incorporation { get; set; }
        public string businessstartdate { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string companypan_no { get; set; }
        public string cin_no { get; set; }
        public string official_telephoneno { get; set; }
        public string official_mailid { get; set; }
        public string companytype_gid { get; set; }
        public string companytype_name { get; set; }
        public string stakeholdertype_gid { get; set; }
        public string stakeholder_type { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string ratingas_on { get; set; }
        public string amlcategory_gid { get; set; }
        public string amlcategory_name { get; set; }
        public string businesscategory_gid { get; set; }
        public string businesscategory_name { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation_gid { get; set; }
        public string designation { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string lastyear_turnover { get; set; }
        public string escrow { get; set; }
        public string application_gid { get; set; }
        public string editdate_incorporation { get; set; }
        public string editbusinessstart_date { get; set; }
        public DateTime dateincorporation { get; set; }
        public DateTime businessstart_date { get; set; }
        public string editratingas_on { get; set; }
        public DateTime ratingason { get; set; }
        public string editstart_date { get; set; }
        public DateTime startdate { get; set; }
        public string editend_date { get; set; }
        public DateTime enddate { get; set; }
        public string application_no { get; set; }
        public string institution_status { get; set; }
        public List<institutionBuyerOnboard_list> institutionBuyerOnboard_list { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string opsapplication_gid { get; set; }
        public string opsapplication_no { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
        public string incometax_returnsstatus { get; set; }
        public string revenue { get; set; }
        public string profit { get; set; }
        public string fixed_assets { get; set; }
        public string sundrydebt_adv { get; set; }
        public string tan_number { get; set; }
        public string lei_renewaldate { get; set; }
        public string msme_registration { get; set; }
        public string lglentity_id { get; set; }
        public string kin { get; set; }
        public string editlei_renewaldate { get; set; }
    }
    public class institutionBuyerOnboard_list : result
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string cin_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string stakeholder_type { get; set; }
        public string date_incorporation { get; set; }
        public string businessstart_date { get; set; }
        public string institution_status { get; set; }
    }
    public class MdlMstBuyerOnboardAddressDetails : result
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
        public string institution2address_gid { get; set; }
        public string institution_gid { get; set; }
        public string group2address_gid { get; set; }
        public string group_gid { get; set; }
        public List<mstBuyerOnboardaddress_list> mstBuyerOnboardaddress_list { get; set; }
        public string opsinstitution2address_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstBuyerOnboardaddress_list
    {
        public string institution2address_gid { get; set; }
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

    public class BuyerOnboardinstitutionuploaddocument : result
    {
        public string institution2form60documentupload_gid { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string institution_gid { get; set; }
        public List<BuyerOnboardinstitutionupload_list> BuyerOnboardinstitutionupload_list { get; set; }
    }
    public class BuyerOnboardinstitutionupload_list
    {
        public string institution2documentupload_gid { get; set; }
        public string institution_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
        public string document_id { get; set; }
        public string institution2form60documentupload_gid { get; set; }
        public string uploaded_by { get; set; }
        public string uploaded_date { get; set; }
        public string opsinstitution2documentupload_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsinstitution2form60documentupload_gid { get; set; }
    }

    public class MdlBuyerOnboardRatingList
    {
        public List<MdlBuyerOnboardRatingdtl> MdlBuyerOnboardRatingdtl { get; set; }
    }

    public class MdlBuyerOnboardRatingdtl : result
    {
        public string institution2ratingdetail_gid { get; set; }
        public string institution_gid { get; set; }
        public string creditrating_agencygid { get; set; }
        public string creditrating_agencyname { get; set; }
        public string creditrating_gid { get; set; }
        public string creditrating_name { get; set; }
        public string assessed_on { get; set; }
        public string creditrating_link { get; set; }
        public string application_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public bool tmpadd_status { get; set; }
    }

    public class MdlBuyerOnboardInstitution2BankAcc : result
    {
        public string institution2bankdtl_gid { get; set; }
        public string institution_gid { get; set; }
        public string application_gid { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebook_status { get; set; }
        public string primary_status { get; set; }
        public string accountopen_date { get; set; }
        public DateTime accountopendate { get; set; }
        public List<BuyerOnboardinstitution2bankacc_list> BuyerOnboardinstitution2bankacc_list { get; set; }
    }

    public class BuyerOnboardinstitution2bankacc_list
    {
        public string institution2bankdtl_gid { get; set; }
        public string institution_gid { get; set; }
        public string buyer_gid { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string accountholder_name { get; set; }
        public string accounttype_name { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string accountopen_date { get; set; }
        public string joinaccount_status { get; set; }
        public string joinaccount_name { get; set; }
        public string chequebook_status { get; set; }
        public string primary_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }

    public class MdlBuyerOnboardPANAbsenceReason : result
    {
        public string contact_gid { get; set; }
        public List<BuyerOnboardpanabsencereason_list> BuyerOnboardpanabsencereason_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public List<BuyerOnboardcontactpanabsencereason_list> BuyerOnboardcontactpanabsencereason_list { get; set; }
    }
    public class BuyerOnboardpanabsencereason_list
    {
        public string panabsencereason { get; set; }
        public bool check_status { get; set; }
    }
    public class BuyerOnboardcontactpanabsencereason_list
    {
        public string panabsencereason { get; set; }
    }
    public class MdlBuyerOnboardContactPANForm60 : result
    {
        public string sacontact2panform60_gid { get; set; }
        public string contact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<BuyerOnboardcontactpanform60_list> BuyerOnboardcontactpanform60_list { get; set; }
    }

    public class BuyerOnboardcontactpanform60_list
    {
        public string sacontact2panform60_gid { get; set; }
        public string contact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
    }


    public class BuyerOnboarduploaddocument : result
    {
        public List<BuyerOnboardupload_list> BuyerOnboardupload_list { get; set; }
    }
    public class BuyerOnboardupload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class MdlBuyerOnboardContactIdProof : result
    {
        public string contact2idproof_gid { get; set; }
        public string contact_gid { get; set; }
        public string idproof_gid { get; set; }
        public string idproof_name { get; set; }
        public string idproof_no { get; set; }
        public string idproof_dob { get; set; }
        public string file_no { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<contactBuyerOnboardidproof_list> contactBuyerOnboardidproof_list { get; set; }
        public List<uploadidBuyerOnboardproofdoc_list> uploadidBuyerOnboardproofdoc_list { get; set; }
    }

    public class contactBuyerOnboardidproof_list
    {
        public string contact2idproof_gid { get; set; }
        public string contact_gid { get; set; }
        public string idproof_gid { get; set; }
        public string idproof_name { get; set; }
        public string idproof_no { get; set; }
        public string idproof_dob { get; set; }
        public string file_no { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string opscontact2idproof_gid { get; set; }
    }

    public class uploadidBuyerOnboardproofdoc_list
    {
        public string tmpindividualproofdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class MdlBuyerOnboardContactDocument : result
    {
        public string contact2document_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<uploadBuyerOnboardindividualdoc_list> uploadBuyerOnboardindividualdoc_list { get; set; }
    }

    public class uploadBuyerOnboardindividualdoc_list
    {
        public string contact2document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
        public string opscontact2document_gid { get; set; }
    }

    public class MdlBuyerOnboardContactMobileNo : result
    {
        public string contact2mobileno_gid { get; set; }
        public string contact_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public List<BuyerOnboardcontactmobileno_list> BuyerOnboardcontactmobileno_list { get; set; }
        public string opscontact2mobileno_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class BuyerOnboardcontactmobileno_list
    {
        public string contact2mobileno_gid { get; set; }
        public string contact_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string opscontact2mobileno_gid { get; set; }
    }

    public class MdlBuyerOnboardContactEmail : result
    {
        public string contact2email_gid { get; set; }
        public string contact_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }
        public List<BuyerOnboardcontactemail_list> BuyerOnboardcontactemail_list { get; set; }
        public string opscontact_gid { get; set; }
        public string opscontact2email_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class BuyerOnboardcontactemail_list
    {
        public string contact2email_gid { get; set; }
        public string contact_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }
        public string opscontact2email_gid { get; set; }
    }

    public class MdlBuyerOnboardContactAddress : result
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
        public string latitude { get; set; }
        public string longitude { get; set; }
        public List<BuyerOnboardcontactaddress_list> BuyerOnboardcontactaddress_list { get; set; }
        public string opscontact2address_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class BuyerOnboardcontactaddress_list
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
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string opscontact2address_gid { get; set; }
    }

    public class MdlMstBuyerOnboardContact : result
    {
        public string contact_gid { get; set; }
        public string application_gid { get; set; }
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
        public string designation_gid { get; set; }
        public string designation_name { get; set; }
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
        public List<contact_list> contact_list { get; set; }
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
        public string opscontact_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string opsapplication_no { get; set; }
        public string statusupdated_by { get; set; }
        public List<BuyerOnboardpanabsencereason_list> BuyerOnboardpanabsencereason_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public List<BuyerOnboardcontactpanabsencereason_list> BuyerOnboardcontactpanabsencereason_list { get; set; }
        public string pan_status { get; set; }
    }


    public class MdlBuyerOnboardCICIndividual : result
    {
        public string contact_gid { get; set; }
        public string contact2bureau_gid { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public DateTime bureauscoredate { get; set; }
        public string cicdocument_name { get; set; }
        public string cicdocument_path { get; set; }
        public string document_content { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string overalllimit_amount { get; set; }
        public string processing_fee { get; set; }
        public string doc_charges { get; set; }
        public string application_gid { get; set; }
        public string bureauscoredateedit { get; set; }
        public List<BuyerOnboardcicindividual_list> BuyerOnboardcicindividual_list { get; set; }
        
    }

    public class MdlEnableApproval : result
    {
        public string contact_gid { get; set; }
        public string institution_status { get; set; }
        public string contact_stakeholder { get; set; }
        public string institution_gid { get; set; }
        public string company_stakeholder { get; set; }
        public string contact_status { get; set; }
        public List<EnableApproval_list> EnableApproval_list { get; set; }

    }

    public class EnableApproval_list 
    {
        public string contact_gid { get; set; }
        public string institution_status { get; set; }
        public string contact_stakeholder { get; set; }
        public string institution_gid { get; set; }
        public string company_stakeholder { get; set; }
        public string contact_status { get; set; }

    }

    public class MdlBuyerOnboardCICInstitution : result
    {
        public string institution_gid { get; set; }
        public string institution2bureau_gid { get; set; }
        public string company_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public DateTime bureauscoredate_edit { get; set; }
        public string bureauscoredateedit { get; set; }
        public List<BuyerOnboardcicinstitution_list> BuyerOnboardcicinstitution_list { get; set; }
    }

    public class BuyerOnboardcicinstitution_list
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string date_incorporation { get; set; }
        public string stakeholder_type { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string institution_status { get; set; }
        public string businessstart_date { get; set; }
    }


    public class BuyerOnboardcicindividual_list
    {
        public string contact_gid { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string contact_status { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string stakeholder_type { get; set; }
        public string institution_name { get; set; }
        public string group_name { get; set; }
    }

    public class MdlMstOnboardApplicationView : result
    {
        public string application_gid { get; set; }
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
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string borrower_flag { get; set; }
        public string borrower_type { get; set; }
        public string momapproval_flag { get; set; }
        public string approval_status { get; set; }
        public List<mobilenumber_list> mobilenumber_list { get; set; }
        public List<mail_list> mail_list { get; set; }
        public List<geneticdetails_list> geneticdetails_list { get; set; }
        public List<mstproduct_list> mstproduct_list { get; set; }
        public string creditgroup_name { get; set; }
        public string businessapproved_date { get; set; }
        public string ccapproved_date { get; set; }
        public string region { get; set; } 
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string customer_urnno { get; set; } 
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string pan_status { get; set; }
        public string cccompleted_flag { get; set; }
        public string urn_status { get; set; }
        public string application_initiateddate { get; set; }
        public string application_initiatedremarks { get; set; }
        public string customerref_name { get; set; }
        public string buyersuppliertype_name { get; set; }
    }

    public class MdlMstOnboardIndividual : result
    {
        public List<OnboardIndividual_List> OnboardIndividual_List { get; set; } 

    }
    public class OnboardIndividual_List : result
    {
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_dob { get; set; }
        public string main_occupation { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string stakeholder_type { get; set; }
    }

     public class MdlMstOnboardInstitution : result
    {
        public List<OnboardInstitution_List> OnboardInstitution_List { get; set; }

    }

    public class OnboardInstitution_List : result
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string cin_no { get; set; }
        public string companytype_name { get; set; }
        public string date_incorporation { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string stakeholder_type { get; set; }
        public string incometax_returnsstatus { get; set; }
        public string revenue { get; set; }
        public string profit { get; set; }
        public string fixed_assets { get; set; }
        public string sundrydebt_adv { get; set; }
    }

    public class MdlRmtransferlist : result
    {
        public List<MdlRmtransferdtl> MdlRmtransferdtl { get; set; } 
    }

    public class MdlRmtransferdtl : result
    {
        public string onboard_gid { get; set; }
        public string transferfrom_employeegid { get; set; }
        public string transferfrom_employeename { get; set; }
        public string transferto_employeegid { get; set; }
        public string transferto_employeename { get; set; }
        public string transfer_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlOboardlglstatus : result
    {
        public List<MdlOboardlglstatuslist> MdlOboardlglstatuslist { get; set; }
    }

    public class MdlOboardlglstatuslist : result
    {
        public string onboard_gid { get; set; }
        public string lgltag_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
      
    }

    public class MdlonboardValidatedtl : result
    {
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string customername { get; set; } 
        public string panoraadhar { get; set; }
        public string contact_gid { get; set; }
        public string institution_gid { get; set; }
        public string onboard_gid { get; set; }
        public string application_gid { get; set; }
        public string stakeholder_type { get; set; }
        public string created_by { get; set; }
        public string lscreatedby_name { get; set; }
        public Int16 pancount { get; set; }
        public string lsrejectcount { get; set; }
        public string lstotalpancount { get; set; }
        public string lspancount { get; set; }
        public string lsnonapplicantpancount { get; set; }
    } 
    public class MdlonboardInitiateDetail : result
    {
        public string buyer_id { get; set; }
        public string buyer_name { get; set; } 
        public string virtualaccount_number { get; set; }
        public string supplier_name { get; set; }
        public string supplier_id { get; set; }
        public List<onboardapplicationList> onboardapplicationList { get; set; }
        public List<loanproductlist> loanproductlist { get; set; } 
    } 

    public class onboardapplicationList
    {
        public string application_no { get; set; }
        public string approval_status { get; set; }
        public string product_name { get; set; }
        public string program_name { get; set; }
        public string initiated_remarks { get; set; }
        public string created_byname { get; set; }
        public string initiated_date { get; set; }
        public string onboarding_status { get; set; }
    } 
    public class MdlBuyerOnboardIndividual2BankAcc : result
    {
        public string contact2bankdtl_gid { get; set; }
        public string contact_gid { get; set; }
        public string application_gid { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebook_status { get; set; }
        public string primary_status { get; set; }
        public string accountopen_date { get; set; }
        public DateTime accountopendate { get; set; }
        public List<BuyerOnboardIndividual2bankacc_list> BuyerOnboardIndividual2bankacc_list { get; set; }
    }

    public class BuyerOnboardIndividual2bankacc_list
    {
        public string contact2bankdtl_gid { get; set; }
        public string contact_gid { get; set; }
        public string buyer_gid { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string accountholder_name { get; set; }
        public string accounttype_name { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string accountopen_date { get; set; }
        public string joinaccount_status { get; set; }
        public string joinaccount_name { get; set; }
        public string chequebook_status { get; set; }
        public string primary_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }

    public class MdlOnboardLimitManagement : result
    {
        public string customerref_name { get; set; }
        public string application_no { get; set; }
        public string lgltag_status { get; set; }
        public List<MdlProductTypeList> MdlProductTypeList { get; set; }
        public List<MdlProductSubTypeList> MdlProductSubTypeList { get; set; }
        public List<MdlApplicationList> MdlApplicationList { get; set; }
        public List<MdlFaclilitydtl> MdlFaclilitydtl { get; set; } 
        public List<MdlProductSubTypeList> MdlProductSubTypeApplicationList { get; set; }
        public List<MdlPmgExpiryDate> MdlPmgExpiryDate { get; set; }
    }

    public class MdlPmgExpiryDate
    {
        public string processupdated_date { get; set; }
        
    }

    public class MdlProductTypeList
    {
        public string producttype_name { get; set; }
        public string producttype_gid { get; set; }
        public string application2loan_gid { get; set; }
    }

    public class MdlProductSubTypeList
    {
        public string application_gid { get; set; }
        public string productsubtype_name { get; set; }
        public string productsubtype_gid { get; set; }
        public string producttype_gid { get; set; }
    }

    public class MdlApplicationList
    {
        public string application_no { get; set; }
        public string contract_id { get; set; }
        public string contract_status { get; set; }
        public string application_gid { get; set; }
        public string product_overallamount { get; set; }
        public string processupdated_date { get; set; }
        public string renewal_flag { get; set; }
        public string amendment_flag { get; set; }
        public string application2loan_gid { get; set; }
    }

    public class MdlFaclilityList : result
    {
        public List<MdlFaclilitydtl> MdlFaclilitydtl { get; set; }
    }

    public class MdlFaclilitydtl
    {
        public string application_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string facility { get; set; }
        public string ApprovedLimit { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string FacilityStatus { get; set; }
        public string UtilizedLimit { get; set; }
        public string AvailableLimit { get; set; }
        public string processupdated_date { get; set; }
        public string overdue_balance { get; set; }

    }

    public class mdlraisequery : result
    {
        public string byronboard_gid { get; set; }
        public string application_gid { get; set; }
        public string query_title { get; set; }
        public string description { get; set; }
        public string openquery_flag { get; set; }
        public string close_remarks { get; set; }

        public string onboardquery_gid { get; set; }
        public List<byrraisequerylist> byrraisequerylist { get; set; }
    }

    public class byrraisequerylist
    {
        public string onboardquery_gid { get; set; }
        public string byronboard_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string query_status { get; set; }
        public string close_remarks { get; set; }
    }

}