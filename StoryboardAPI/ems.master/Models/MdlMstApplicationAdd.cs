using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for Application Creation Add in Samfin)ApplicationAdd Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlDropDown : result
    {
        public List<vertical_list> vertical_list { get; set; }
        public List<verticaltaggs_list> verticaltaggs_list { get; set; }
        public List<constitutionlist> constitutionlist { get; set; }
        public List<businessunitlist> businessunitlist { get; set; }
        public List<valuechainlist> valuechainlist { get; set; }
        public List<vernacularlang_list> vernacularlang_list { get; set; }
        public List<associatemasterlist> associatemasterlist { get; set; }
        public List<designationlist> designationlist { get; set; }
        public List<grouplist> grouplist { get; set; }
        public List<institutionlist> institutionlist { get; set; }
        public List<creditgrouplist> creditgrouplist { get; set; }
        public List<program_list> program_list { get; set; }
        public List<productname_list> productname_list { get; set; }
    }
    public class vertical_list
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string vertical_code { get; set; }
    }
    public class verticaltaggs_list
    {
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
    }
    public class constitutionlist
    {
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string constitution_code { get; set; }
    }
    public class businessunitlist
    {
        public string businessunit_gid { get; set; }
        public string businessunit_code { get; set; }
        public string businessunit_name { get; set; }
    }
    public class valuechainlist
    {
        public string valuechain_gid { get; set; }
        public string valuechain_code { get; set; }
        public string valuechain_name { get; set; }
        public string bureau_code { get; set; }
    }
    public class vernacularlang_list
    {
        public string vernacularlanguage_gid { get; set; }
        public string vernacular_language { get; set; }
    }
    public class associatemasterlist
    {
        public string associatemaster_gid { get; set; }
        public string name { get; set; }
        public string associate_code { get; set; }
        public string rdbstatus { get; set; }
    }
    public class designationlist
    {
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
    }
    public class MdlGeneticCode : result
    {
        public List<genetic_list> genetic_list { get; set; }
    }
    public class genetic_list
    {
        public string geneticcode_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }
    }
    public class MdlMstApplicationAdd : result
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
        public List<primaryvaluechain_list> primaryvaluechain_list { get; set; }
        public List<secondaryvaluechain_list> secondaryvaluechain_list { get; set; }
        public List<vernacularlanguage_list> vernacularlanguage_list { get; set; }
        public List<applicationadd_list> applicationadd_list { get; set; }
        public List<applicationlist> applicationlist { get; set; }
        public List<basicdetails_list> basicdetails_list { get; set; }
        public List<valuechainlist> valuechainlist { get; set; }
        public List<vernacularlang_list> vernacularlang_list { get; set; }       
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
        public string approval_status { get; set; }
        public string creditgroup_status { get; set; }
        
    }

    public class vernacularlanguage_list
    {
        public string vernacularlanguage_gid { get; set; }
        public string vernacular_language { get; set; }
    }
    public class applicationlist : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
    }
    public class applicationadd_list
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
        public string renewal_flag { get; set; }
        public string history_flag { get; set; }
        public string sentback_flag { get; set; }
        public string enhancement_flag { get; set; }
        public string ccmeetingskip_flag { get; set; }
        public string ccmeetingskipcolor_flag { get; set; }
    }
    public class basicdetails_list
    {
        public string application_no { get; set; }
        public string application_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string application_status { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
    }
    public class MdlMstMobileNo : result
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
        public List<mstmobileno_list> mstmobileno_list { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string opsapplication2contact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstmobileno_list
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
    public class MdlMstEmailAddress : result
    {
        public string application2email_gid { get; set; }
        public string application_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string institution_gid { get; set; }
        public List<mstemailaddress_list> mstemailaddress_list { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsapplication2email_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstemailaddress_list
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
    public class MdlMstGeneticCode : result
    {
        public string application2geneticcode_gid { get; set; }
        public string application_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }
        public string geneticcode_gid { get; set; }
        public List<mstgeneticcode_list> mstgeneticcode_list { get; set; }
        public string opsapplication2geneticcode_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstgeneticcode_list
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
    public class MdlMstLoanDtl : result
    {
        public string producttype_gid { get; set; }
        public string application_gid { get; set; }
        public string facilityrequested_date { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
        public string loantype_gid { get; set; }
        public string loan_type { get; set; }
        public string facilityloan_amount { get; set; }
        public string rate_interest { get; set; }

        public string roi_margin { get; set; }
        public string penal_interest { get; set; }
        public string facilityvalidity_year { get; set; }
        public string facilityvalidity_month { get; set; }
        public string facilityvalidity_days { get; set; }
        public string facilityoverall_limit { get; set; }
        public string tenureproduct_year { get; set; }
        public string tenureproduct_month { get; set; }
        public string tenureproduct_days { get; set; }
        public string tenureoverall_limit { get; set; }
        public string scheme_type { get; set; }
        public string facility_type { get; set; }
        public string facility_mode { get; set; }
        public string productsubtype_gid { get; set; }
        public string principalfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string program_gid { get; set; }
        public string program { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public string application2loan_gid { get; set; }
        public string collateral_status { get; set; }
        public string buyer_status { get; set; }
        public string source_type { get; set; }
        public string margin { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string collateralobservation_summary { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string enduse_purpose { get; set; }
        public string overalllimit_amount { get; set; }
        public string loanfacility_amount { get; set; }
        public List<mstloan_list> mstloan_list { get; set; }
        public List<DocumentList> DocumentList { get; set; }
        public List<servicecharges_list> servicecharges_list { get; set; }
        public List<primaryvaluechain_list> primaryvaluechain_list { get; set; }
        public List<secondaryvaluechain_list> secondaryvaluechain_list { get; set; }
        public List<valuechainlist> valuechainlist { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string loandetailsvalidation_flag { get; set; }
    }


    public class mstloan_list
    {
        public string application2loan_gid { get; set; }
        public string application_gid { get; set; }
        public string facilityrequested_date { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
        public string loantype_gid { get; set; }
        public string loan_type { get; set; }
        public string facilityloan_amount { get; set; }
        public string rate_interest { get; set; }
        public string roi_margin { get; set; }
        public string penal_interest { get; set; }
        public string facilityvalidity_year { get; set; }
        public string facilityvalidity_month { get; set; }
        public string facilityvalidity_days { get; set; }
        public string facilityoverall_limit { get; set; }
        public string tenureproduct_year { get; set; }
        public string tenureproduct_month { get; set; }
        public string tenureproduct_days { get; set; }
        public string tenureoverall_limit { get; set; }
        public string scheme_type { get; set; }
        public string facility_type { get; set; }
        public string facility_mode { get; set; }
        public string principalfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public string loanfacility_amount { get; set; }
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string collateralobservation_summary { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string producttype_gid { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
    }
    public class MdlMstBuyer : result
    {
        public string application2buyer_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string application_gid { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string balance_limit { get; set; }
        public string bill_tenure { get; set; }
        public string margin { get; set; }
        public List<mstbuyer_list> mstbuyer_list { get; set; }
    }
    public class mstbuyer_list
    {
        public string application2buyer_gid { get; set; }
        public string application_gid { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string balance_limit { get; set; }
        public string bill_tenure { get; set; }
        public string margin { get; set; }
    }
    public class MdlProductDropDown : result
    {
        public List<loanproductlist> loanproductlist { get; set; }
        public List<loantypelist> loantypelist { get; set; }
        public List<principalfrequencylist> principalfrequencylist { get; set; }
        public List<interestfrequencylist> interestfrequencylist { get; set; }
        public List<buyerlist> buyerlist { get; set; }
        public List<securitytype_list> securitytype_list { get; set; }
        public List<programlist> programlist { get; set; }      
    }
    public class loanproductlist
    {
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
    }
    public class loantypelist
    {
        public string loantype_gid { get; set; }
        public string loan_type { get; set; }
    }
    public class principalfrequencylist
    {
        public string principalfrequency_gid { get; set; }
        public string principalfrequency_name { get; set; }
    }
    public class interestfrequencylist
    {
        public string interestfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
    }
    public class buyerlist
    {
        public string buyer_name { get; set; }
        public string buyer_gid { get; set; }
    }
    public class securitytype_list
    {
        public string securitytype_gid { get; set; }
        public string security_type { get; set; }
    }
    public class programlist
    {
        public string program_gid { get; set; }
        public string program { get; set; }
    }
    public class program_list
    {
        public string program_gid { get; set; }
        public string program { get; set; }
    }
    public class productname_list
    {
        public string product_gid { get; set; }
        public string product_name { get; set; }
    }
    public class creditgrouplist
    {
        public string creditmapping_gid { get; set; }
        public string creditgroup_name { get; set; }
    }
    public class MdlMstCollatertal : result
    {
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string collateralobservation_summary { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string application2collateral_gid { get; set; }
        public string application_gid { get; set; }
        public List<collatertal_list> collatertal_list { get; set; }
        public List<DocumentList> DocumentList { get; set; }
    }
    public class collatertal_list
    {
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string collateralobservation_summary { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string application2collateral_gid { get; set; }
    }
    public class MdlMstHypothecation : result
    {
        public string securitytype_gid { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string security_value { get; set; }
        public string securityassessed_date { get; set; }
        public string asset_id { get; set; }
        public string roc_fillingid { get; set; }
        public string CERSAI_fillingid { get; set; }
        public string hypoobservation_summary { get; set; }
        public string primary_security { get; set; }
        public string application2hypothecation_gid { get; set; }
        public string application_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public List<hypothecation_list> hypothecation_list { get; set; }
        public List<DocumentList> DocumentList { get; set; }
        public string opsapplication2hypothecation_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class hypothecation_list
    {
        public string securitytype_gid { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string security_value { get; set; }
        public string securityassessed_date { get; set; }
        public string asset_id { get; set; }
        public string roc_fillingid { get; set; }
        public string CERSAI_fillingid { get; set; }
        public string hypoobservation_summary { get; set; }
        public string primary_security { get; set; }
        public string application2hypothecation_gid { get; set; }
        public string application_gid { get; set; }
        public string opsapplication2hypothecation_gid { get; set; }
    }
    public class Documentname : result
    {
        public List<DocumentList> DocumentList { get; set; }
        public string application2loan_gid { get; set; }
        public string application2hypothecation_gid { get; set; }
    }
    public class DocumentList
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
        public string application2hypothecation_gid { get; set; }
        public string application2collateral_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string opsapplication2hypothecation_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string migration_flag { get; set; }

    }
    public class MdlProductCharges : result
    {
        public string application2servicecharge_gid { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string overalllimit_amount { get; set; }
        public string validityoveralllimit_year { get; set; }
        public string validityoveralllimit_month { get; set; }
        public string validityoveralllimit_days { get; set; }
        public string calculationoveralllimit_validity { get; set; }

        public string enduse_purpose { get; set; }
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
        public string principalfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public List<productcharges_list> productcharges_list { get; set; }
        public List<servicecharges_list> servicecharges_list { get; set; }
        public string economical_flag { get; set; }
        public string productcharge_flag { get; set; }
        public string productcharges_status { get; set; }
        public string applicant_type { get; set; }
        public string product_type { get; set; }
        public string producttype_gid { get; set; }
        public string fieldvisit_charges_collectiontype { get; set; }
        public string acctinsurance_collectiontype { get; set; }
        public List<producttypelist> producttypelist { get; set; }
        public string csa_applicability { get; set; }
        public string csaactivity_gid { get; set; }
        public string csaactivity_name { get; set; }
        public string percentageoftotal_limit { get; set; }

    }
    public class producttypelist
    {
        public string product_type { get; set; }
        public string producttype_gid { get; set; }
    }
    public class productcharges_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class servicecharges_list
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
        public string application_gid { get; set; }
        public string application2servicecharge_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string product_type { get; set; }
        public string producttype_gid { get; set; }
        public string acctinsurance_collectiontype { get; set; }
    }
    public class MdlMstGST : result
    {
        public string institution2branch_gid { get; set; }
        public string institution_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_no { get; set; }
        public string gst_registered { get; set; }
        public InstitutionGSTDetails[] GSTArray { get; set; }
        public List<mstgst_list> mstgst_list { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
        public string headoffice_status { get; set; }
    }
    public class mstgst_list
    {
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
        public string headoffice_status { get; set; }
    }
    public class InstitutionGSTDetails
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
    public class MdlMstLicenseDetails : result
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
        public List<mstlicense_list> mstlicense_list { get; set; }
        public string opsinstitution2licensedtl_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstlicense_list
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
    public class institutionuploaddocument : result
    {
        public string institution2form60documentupload_gid { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string institution_gid { get; set; }
        public List<institutionupload_list> institutionupload_list { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }

        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
    }
    public class institutionupload_list
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
        public string  migration_flag { get; set; }
        public string documenttype_gid { get; set; }
        public string documenttype_name { get; set; }
    }
    public class MdlMstInstitutionAdd : result
    {
        public string msme_regi_no { get; set; }
        public string lei_no { get; set; }
        public string kin_no { get; set; }
        public string renewaldue_date { get; set; }
        public DateTime Renewaldue_date { get; set; }

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
        public List<institution_list> institution_list { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string opsapplication_gid { get; set; }
        public string opsapplication_no { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
        public string nearsamunnatiabranch_gid { get; set; }
        public string nearsamunnatiabranch_name { get; set; }
        public string udhayam_registration { get; set; }
        public string tan_number { get; set; }
        public string business_description { get; set; }
        public string tanstate_gid { get; set; }
        public string tanstate_name { get; set; }
        public string internalrating_gid { get; set; }
        public string internalrating_name { get; set; }
        public string sales { get; set; }
        public string purchase { get; set; }
        public string credit_summation { get; set; }
        public string cheque_bounce { get; set; }
        public string numberof_boardmeetings { get; set; }
        public string farmer_count { get; set; }
        public string crop_cycle { get; set; }
        public string calamities_prone { get; set; }
        public string Gstflag { get; set; }
        public string program_gid { get; set; }
        public string lspage { get; set; }


        public List<fpocity_list> fpocity_list { get; set; }
        public List<cityedit_list> cityedit_list { get; set; }
       
    }
    public class cityedit_list : result
    {
        public string city_gid { get; set; }
        public string city_name { get; set; }
    }
    public class fpocity_list : result
    {
        public string city_gid { get; set; }
        public string city_name { get; set; }
    }
    public class institution_list : result
    {
        public string guarantordelete_flag { get; set; }
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
        public string application_gid { get; set; }
    }
    public class MdlMstAddressDetails : result
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
        public List<mstaddress_list> mstaddress_list { get; set; }
        public string opsinstitution2address_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstaddress_list
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

    public class MdlMstBankDetails : result
    {
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string group2bank_gid { get; set; }
        public string group_gid { get; set; }
        public List<mstbank_list> mstbank_list { get; set; }
        public string opsgroup2bank_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class mstbank_list
    {
        public string group2bank_gid { get; set; }
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string opsgroup2bank_gid { get; set; }
    }

    public class MdlMstGroup : result
    {
        public string group_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string group_name { get; set; }
        public string date_of_formation { get; set; }
        public DateTime dateofformation { get; set; }
        public string group_type { get; set; }
        public string groupmember_count { get; set; }
        public string groupdocument_name { get; set; }
        public string groupurn_status { get; set; }
        public string group_urn { get; set; }
        public string group_status { get; set; }
        public List<group_list> group_list { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
        public string male_count { get; set; }
        public string female_count { get; set; }
        public string internalrating_gid { get; set; }
        public string internalrating_name { get; set; }
    }

    public class group_list
    {
        public string group_gid { get; set; }
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
        public string credit_status { get; set; }
        public string overallCovenantCount { get; set; }
        public string OverallDeferralcount { get; set; }
        public string verifieddeferraldoc { get; set; }
        public string verifiedcovenantdoc { get; set; }
        public string QueryPendingCount { get; set; }
        public string QueryClosedCount { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string application_gid { get; set; }
        public string bre_status { get; set; }

    }

    public class MdlGroupDocument : result
    {
        public string group2document_gid { get; set; }
        public string group_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<groupdocument_list> groupdocument_list { get; set; }
    }

    public class groupdocument_list
    {
        public string group2document_gid { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string opsgroup2document_gid { get; set; }
    }

    public class MdlCICGroup : result
    {
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string date_of_formation { get; set; }
        public string group_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public List<cicgroup_list> cicgroup_list { get; set; }
    }

    public class cicgroup_list
    {
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string date_of_formation { get; set; }
        public string group_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class MdlMstContact : result
    {
        public string contact_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string pan_status { get; set; }
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
        public List<cicuploadindividual_list> cicuploadindividual_list { get; set; }
        public List<uploadindividualdoc_list> uploadindividualdoc_list { get; set; }
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
        public List<panabsencereason_list> panabsencereason_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public List<contactpanabsencereason_list> contactpanabsencereason_list { get; set; }
        public string nearsamunnatiabranch_gid { get; set; }
        public string nearsamunnatiabranch_name { get; set; }
        public string physicalstatus_gid { get; set; }
        public string physicalstatus_name { get; set; }
        public string internalrating_gid { get; set; }
        public string internalrating_name { get; set; }
        public List<mstequipmentholding_list> mstequipmentholding_list { get; set; }
        public List<mstlivestockholding_list> mstlivestockholding_list { get; set; }
        public string program_gid { get; set; }
        public string lspage { get; set; }
    }
    public class contact_list
    {
        public string guarantordelete_flag { get; set; }
    public string contact_gid { get; set; }
        public string application_gid { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string individual_name { get; set; }
        public string stakeholder_type { get; set; }
        public string contact_status { get; set; }
        public string institution_name { get; set; }
        public string group_name { get; set; }
    }
    public class MdlContactDocument : result
    {
        public string contact2document_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<uploadindividualdoc_list> uploadindividualdoc_list { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }

        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
    }

    public class cicuploadindividual_list
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

    }

    public class uploadindividualdoc_list
    {
        public string contact2document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
        public string opscontact2document_gid { get; set; }
        public string migration_flag { get; set; }
        public string documenttype_name { get; set; }
    }

    public class uploadidproofdoc_list
    {
        public string tmpindividualproofdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class MdlContactMobileNo : result
    {
        public string contact2mobileno_gid { get; set; }
        public string contact_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public List<contactmobileno_list> contactmobileno_list { get; set; }
        public string opscontact2mobileno_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class contactmobileno_list
    {
        public string contact2mobileno_gid { get; set; }
        public string contact_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string opscontact2mobileno_gid { get; set; }
    }

    public class MdlContactEmail : result
    {
        public string contact2email_gid { get; set; }
        public string contact_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }
        public List<contactemail_list> contactemail_list { get; set; }
        public string opscontact_gid { get; set; }
        public string opscontact2email_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class contactemail_list
    {
        public string contact2email_gid { get; set; }
        public string contact_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }
        public string opscontact2email_gid { get; set; }
    }

    public class MdlContactAddress : result
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
        public List<contactaddress_list> contactaddress_list { get; set; }
        public string opscontact2address_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class contactaddress_list
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

    public class MdlContactIdProof : result
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
        public List<contactidproof_list> contactidproof_list { get; set; }
        public List<uploadidproofdoc_list> uploadidproofdoc_list { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }

        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }

    }

    public class contactidproof_list
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
    public class MdlCICIndividual : result
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
        public List<cicindividual_list> cicindividual_list { get; set; }
        public List<cicuploaddoc_list> cicuploaddoc_list { get; set; }
    }

    public class cicindividual_list
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
        public string application_gid { get; set; }
        public string guarantordelete_flag { get; set; }
    }

    public class MdlCICInstitution : result
    {
        public string institution_gid { get; set; }
        public string institution2bureau_gid { get; set; }
        public string company_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public DateTime bureauscoredate { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public DateTime bureauscoredate_edit { get; set; }
        public string bureauscoredateedit { get; set; }
        public List<cicinstitution_list> cicinstitution_list { get; set; }
        public List<cicuploaddoc_list> cicuploaddoc_list { get; set; }
    }

    public class cicinstitution_list
    {
        public string application_gid { get; set; }
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
        public string guarantordelete_flag { get; set; }
    }

    public class cicuploaddoc_list
    {
        public string tmpcicdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_content { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string contact_gid { get; set; }
        public string contact2bureau_gid { get; set; }
        public string institution_gid { get; set; }
        public string institution2bureau_gid { get; set; }
        public string migration_flag { get; set; }
    }
    public class MdlCICIndividualEdit : result
    {
        public string contact_gid { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public DateTime bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string cicdocument_path { get; set; }
        public string cicdocument_name { get; set; }

        public List<cicindividual_list> cicindividual_list { get; set; }
        public List<cicuploaddoc_list> cicuploaddoc_list { get; set; }
    }
    public class MdlMstApplicationEdit : result
    {
        public string application_gid { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }

    }
    public class MdlList : result
    {
        public List<product_list> product_list { get; set; }

    }
    public class product_list
    {
        public string producttype_gid { get; set; }
        public string product_type { get; set; }
    }
    public class grouplist
    {
        public string group_name { get; set; }
        public string group_gid { get; set; }
    }
    public class institutionlist
    {
        public string institution_name { get; set; }
        public string institution_gid { get; set; }
    }
    public class ApplicationCount
    {
        public string newapplication_count { get; set; }
        public string rejected_count { get; set; }
        public string hold_count { get; set; }
        public string ccapproved_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class AssignApplicationCount
    {   
        public string pending_count { get; set; }
        public string assigned_count { get; set; }
        public string submittedtocc_count { get; set; }
        public string ccapproved_count { get; set; }
        public string rejected_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }

    public class MdlContactBureau : result
    {
        public string contact2bureau_gid { get; set; }
        public string contact_gid { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureau_response { get; set; }
        public string observations { get; set; }
        public List<contactbureau_list> contactbureau_list { get; set; }
    }

    public class contactbureau_list
    {
        public string contact2bureau_gid { get; set; }
        public string contact_gid { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureau_response { get; set; }
        public string observations { get; set; }
    }

    public class MdlInstitutionBureau : result
    {
        public string institution2bureau_gid { get; set; }
        public string institution_gid { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureau_response { get; set; }
        public string observations { get; set; }
        public List<contactbureau_list> contactbureau_list { get; set; }
        public List<institutionbureau_list> institutionbureau_list { get; set; }
    }

    public class institutionbureau_list
    {
        public string institution2bureau_gid { get; set; }
        public string institution_gid { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureau_response { get; set; }
        public string observations { get; set; }
    }

    public class MdlApprovalHierarchy : result
    {
        public string lshierarchychange_flag { get; set; }   
        public string hierarchyupdated_flag { get; set; }
    }

    public class MdlApprovalHierarchyChange : result
    {
        public string clustermanager_gid { get; set; }
        public string clustermanager_name { get; set; }
        public string regionalhead_gid { get; set; }
        public string regionhead_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string zonalhead_name { get; set; }
        public string businesshead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string rm_name { get; set; }
        public string directreportingto_name { get; set; }
    }
    public class MdlMstUpdateApproval : result
    {
        public string clustermanager_gid { get; set; }
        public string clustermanager_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string zonalhead_name { get; set; }
        public string regionalhead_name { get; set; }
        public string regionalhead_gid { get; set; }
        public string businesshead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string application_gid { get; set; }
        public string hierarchyupdated_flag { get; set; }
    }
    public class MdlSectorcategory : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string valuechain_gid { get; set; }
        public string valuechain_name { get; set; }
        public string product_gid { get; set; }
        public List<varietyname_list> varietyname_list { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
    }
    public class varietyname_list : result
    {
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
    }
    public class MdlPANAbsenceReason : result
    {
        public string contact_gid { get; set; }
        public List<panabsencereason_list> panabsencereason_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public List<contactpanabsencereason_list> contactpanabsencereason_list { get; set; }
    }
    public class panabsencereason_list
    {
        public string panabsencereason { get; set; }
        public bool check_status { get; set; }
    }
    public class contactpanabsencereason_list
    {
        public string panabsencereason { get; set; }
    }
    public class MdlContactPANForm60 : result
    {
        public string contact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<contactpanform60_list> contactpanform60_list { get; set; }
    }

    public class contactpanform60_list
    {
        public string contact2panform60_gid { get; set; }
        public string sacontact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
    }
    public class MdlMstRenewalAdd : result
    {
        public string application_no { get; set; }
        public string application_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string sa_id { get; set; }
        public string sa_name { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string drm_gid { get; set; }
        public string drm_name { get; set; }
        public string clustermanager_gid { get; set; }
        public string clustermanager_name { get; set; }
        public string zonalhead_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string regionalhead_name { get; set; }
        public string regionalhead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string businesshead_gid { get; set; }
        public string vernacular_language { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string overalllimit_amount { get; set; }
        public string validityoveralllimit_year { get; set; }
        public string validityoveralllimit_month { get; set; }
        public string validityoveralllimit_days { get; set; }
        public string calculationoveralllimit_validity { get; set; }
        public string principalfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public string enduse_purpose { get; set; }
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
        public string application_status { get; set; }
        public string dateofsurvey { get; set; }
        public string objevtiveoffpo { get; set; }
        public string majorcrops { get; set; }
        public string alternativeincomesource { get; set; }
        public string overallfporating { get; set; }
        public string overallfpograde { get; set; }
        public string recommendation { get; set; }
        public string fpo_acscore { get; set; }
        public string numnerofaactive_fig { get; set; }
        public string existinglending_directandindirect { get; set; }
        public string outstandingportfolio_directandindirect { get; set; }
        public string par90_managedbyonlyinstitution_direct { get; set; }
        public string nonnegotiableconditions_met { get; set; }
        public string institution_directandindrectborrowing { get; set; }
        public string totaldisbursements_otherlenders { get; set; }
        public string saname_gid { get; set; }
        public string economical_flag { get; set; }
        public string productcharge_flag { get; set; }
        public string applicant_type { get; set; }
        public string customerref_name { get; set; }
        public string customerref_no { get; set; }
        public string productcharges_status { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string approval_flag { get; set; }
        public string gradingdraft_flag { get; set; }
        public string hypothecation_flag { get; set; }
        public string submitted_by { get; set; }
        public string submitted_date { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string sastatus { get; set; }
        public string approval_status { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string renewal_flag { get; set; }
        public string applicationrenewal_flag { get; set; }
        public string dateofapp { get; set; }
        public string refapplication_no { get; set; }
        public string refapplication_gid { get; set; }
        public string totalinstitution_gid { get; set; }
        public string totalcontact_gid { get; set; }
        public string totalgroup_gid { get; set; }
    }

    public class MdlMstProductDetailAdd : result
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
        public string searchrecord_flag { get; set; }
        public List<mstproduct_list> mstproduct_list { get; set; }
        public string csacommodity_average { get; set; }
        public string csapercentageoftotal_limit { get; set; }
    }
    public class MdlMstProductDetailList : result
    {

        public List<mstproduct_list> mstproduct_list { get; set; }
    }
    public class mstproduct_list
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
        public string csacommodity_average { get; set; }
        public string csapercentageoftotal_limit { get; set; }
    }
    public class MdlMstEquipmentHolding : result
    {
        public string institution2equipment_gid { get; set; }
        public string institution_gid { get; set; }
        public string equipment_gid { get; set; }
        public string equipment_name { get; set; }
        public string availablerenthire { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string insurance_status { get; set; }
        public string insurance_details { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string contact2equipment_gid { get; set; }
        public string contact_gid { get; set; }
        public string group2equipment_gid { get; set; }
        public string group_gid { get; set; }
        public List<mstequipmentholding_list> mstequipmentholding_list { get; set; }
    }
    public class mstequipmentholding_list : result
    {
        public string institution2equipment_gid { get; set; }
        public string institution_gid { get; set; }
        public string equipment_gid { get; set; }
        public string equipment_name { get; set; }
        public string availablerenthire { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public string insurance_status { get; set; }
        public string insurance_details { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string contact2equipment_gid { get; set; }
        public string contact_gid { get; set; }
        public string group2equipment_gid { get; set; }
        public string group_gid { get; set; }
    }
    
    public class MdlMstLivestock : result
    {
        public string institution2livestock_gid { get; set; }
        public string institution_gid { get; set; }
        public string livestock_gid { get; set; }
        public string livestock_name { get; set; }
        public string count { get; set; }
        public string Breed { get; set; }
        public string insurance_status { get; set; }
        public string insurance_details { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string contact2livestock_gid { get; set; }
        public string contact_gid { get; set; }
        public string group2livestock_gid { get; set; }
        public string group_gid { get; set; }
        public List<mstlivestockholding_list> mstlivestockholding_list { get; set; }
    }
    public class mstlivestockholding_list : result
    {
        public string institution2livestock_gid { get; set; }
        public string institution_gid { get; set; }
        public string livestock_gid { get; set; }
        public string livestock_name { get; set; }
        public string count { get; set; }
        public string Breed { get; set; }
        public string insurance_status { get; set; }
        public string insurance_details { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string contact2livestock_gid { get; set; }
        public string contact_gid { get; set; }
        public string group2livestock_gid { get; set; }
        public string group_gid { get; set; }
    }
    public class MdlGSTHeadOffice : result
    {
        public string institution2branch_gid { get; set; }
        public string institution_gid { get; set; }
        public string employee_gid { get; set; }
    }
    public class MdlMstReceivable : result
    {
        public string institution2receivable_gid { get; set; }
        public string institution_gid { get; set; }
        public string receivable_date { get; set; }
        public string onetothirty_days { get; set; }
        public string thirtyonetosixty_days { get; set; }
        public string sixtyonetoninety_days { get; set; }
        public string ninety_days { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<mstreceivable_list> mstreceivable_list { get; set; }
    }
    public class mstreceivable_list : result
    {
        public string institution2receivable_gid { get; set; }
        public string institution_gid { get; set; }
        public string receivable_date { get; set; }
        public string onetothirty_days { get; set; }
        public string thirtyonetosixty_days { get; set; }
        public string sixtyonetoninety_days { get; set; }
        public string ninety_days { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlMstEnhancementAdd : result
    {
        public string application_no { get; set; }
        public string application_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string sa_id { get; set; }
        public string sa_name { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string drm_gid { get; set; }
        public string drm_name { get; set; }
        public string clustermanager_gid { get; set; }
        public string clustermanager_name { get; set; }
        public string zonalhead_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string regionalhead_name { get; set; }
        public string regionalhead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string businesshead_gid { get; set; }
        public string vernacular_language { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string overalllimit_amount { get; set; }
        public string validityoveralllimit_year { get; set; }
        public string validityoveralllimit_month { get; set; }
        public string validityoveralllimit_days { get; set; }
        public string calculationoveralllimit_validity { get; set; }
        public string principalfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public string enduse_purpose { get; set; }
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
        public string application_status { get; set; }
        public string dateofsurvey { get; set; }
        public string objevtiveoffpo { get; set; }
        public string majorcrops { get; set; }
        public string alternativeincomesource { get; set; }
        public string overallfporating { get; set; }
        public string overallfpograde { get; set; }
        public string recommendation { get; set; }
        public string fpo_acscore { get; set; }
        public string numnerofaactive_fig { get; set; }
        public string existinglending_directandindirect { get; set; }
        public string outstandingportfolio_directandindirect { get; set; }
        public string par90_managedbyonlyinstitution_direct { get; set; }
        public string nonnegotiableconditions_met { get; set; }
        public string institution_directandindrectborrowing { get; set; }
        public string totaldisbursements_otherlenders { get; set; }
        public string saname_gid { get; set; }
        public string economical_flag { get; set; }
        public string productcharge_flag { get; set; }
        public string applicant_type { get; set; }
        public string customerref_name { get; set; }
        public string customerref_no { get; set; }
        public string productcharges_status { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string approval_flag { get; set; }
        public string gradingdraft_flag { get; set; }
        public string hypothecation_flag { get; set; }
        public string submitted_by { get; set; }
        public string submitted_date { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string sastatus { get; set; }
        public string approval_status { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string enhancement_flag { get; set; }
        public string applicationenhancement_flag { get; set; }
        public string dateofapp { get; set; }
        public string refapplication_no { get; set; }
        public string refapplication_gid { get; set; }
        public string totalinstitution_gid { get; set; }
        public string totalcontact_gid { get; set; }
        public string totalgroup_gid { get; set; }
        public string application_gid1 { get; set; }
        public string application_no1 { get; set; }
    }
    public class MdlCSAActivityDropDown : result
    { 
        public List<csactivity_list> csactivity_list { get; set; }
    }
    public class csactivity_list : result
    {
        public string sector_gid { get; set; }
        public string sector_name { get; set; }
    }
    public class MdlonboardValidatedtl : result
    {
        public string pan_no { get; set; }
        public string companypan_no { get; set; }
        public string lscompanypan_no { get; set; }
        public string lsrejectedpan { get; set; }
        public Int16 lsnotrejectcount { get; set; }
        public Int16 lsrejectcount { get; set; }
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
        public string panrenewal_flag { get; set; }
        public string Type { get; set; }
        public string credit_name { get; set; }
        public Int16 lstotalcount { get; set; }
        public Int16 lstotalpancount { get; set; }
        public char panrenewal_flage { get; set; }
        public Int16 applicant_panstatus { get; set; }
    }

    public class Agrupload_list : result
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string application_gid { get; set; }

    }

}
