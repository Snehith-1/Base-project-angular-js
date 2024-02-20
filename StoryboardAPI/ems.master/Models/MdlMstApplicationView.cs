using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for CAD page in Samfin)CADApplication Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlMstApplicationView : result
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
        public string primaryvaluechain_name { get; set; }
        public string secondaryvaluechain_name { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string borrower_flag { get; set; }
        public string borrower_type { get; set; }
        public string momapproval_flag { get; set; }
        public string approval_status { get; set; }
        public List<mobilenumber_list> mobilenumber_list { get; set; }
        public List<mail_list> mail_list { get; set; }
        public List<geneticdetails_list> geneticdetails_list { get; set; }
        public string creditgroup_name { get; set; }
        public string businessapproved_date { get; set; }
        public string ccapproved_date { get; set; }
        public string region { get; set; }
        public string docchecklist_makerflag { get; set; }
        public string docchecklist_checkerflag { get; set; }
        public string docchecklist_approvalflag { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string customer_urnno { get; set; }
        public List<alldatamodified_List> alldatamodified_List { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string pan_status { get; set;}
        public string cccompleted_flag { get; set; }
        public string scorecard_submit { get; set; }
        public string hypo_flag { get; set; }

    }
    public class mobilenumber_list : result
    {
        public string application_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_mobileno { get; set; }
    }
    public class mail_list : result
    {
        public string application_gid { get; set; }
        public string application2email_gid { get; set; }
        public string email_address { get; set; }
    }
    public class geneticdetails_list : result
    {
        public string application_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }
    }

    public class instituionmobilenumber_list : result
    {
        public string institution_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_no { get; set; }
    }

    public class MdlMstInstitutionDtlView : result
    {
        public string application_gid { get; set; }
        public string institution_gid { get; set; }
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
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string businessstart_date { get; set; }

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
        public string city_name { get; set; }

        public List<mstgst_list> mstgst_list { get; set; }
        public List<instituionmail_list> instituionmail_list { get; set; }
        public List<instituionmobilenumber_list> instituionmobilenumber_list { get; set; }
        public List<mstaddress_list> mstaddress_list { get; set; }
        public List<institutionform60_list> institutionform60_list { get; set; }
        public List<institutiondoc_list> institutiondoc_list { get; set; }
        public List<mstlicense_list> mstlicense_list { get; set; }
        public List<mstequipmentholding_list> mstequipmentholding_list { get; set; }
        public List<mstlivestockholding_list> mstlivestockholding_list { get; set; }
        public List<mstreceivable_list> mstreceivable_list { get; set; }       
    }

   
    public class instituionmail_list : result
    {
        public string email_address { get; set; }
    }

    public class institutionform60_list : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string institution2form60documentupload_gid { get; set; }
    }

    public class institutiondoc_list : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
        public string document_id { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string migration_flag { get; set; }
    }

    public class MdlMstVisitPersonView : result
    {
        public string applicationvisit_gid { get; set; }
        public string application_gid { get; set; }
        public string applicationvisit_date { get; set; }
        public string clientkmp_activities { get; set; }
        public string promoter_background { get; set; }
        public string overall_observations { get; set; }
        public string inspectingofficial_recommenation { get; set; }
        public string trading_relationship { get; set; }
        public string summary { get; set; }
        public string inspectingofficials_name { get; set; }
        public string visitdone_name { get; set; }
        public string visitreport_id { get; set; }
        //public List<mdlvisitdone> mdlvisitdone { get; set; }
        //public List<mstinspectingofficials> mstinspectingofficials { get; set; }
        public List<mstVisitpersondtl_list> mstVisitpersondtl_list { get; set; }
        public List<mstVisitpersonaddress_list> mstVisitpersonaddress_list { get; set; }
        public List<VisitReport_List> VisitReport_List { get; set; }
        public List<UploadDocumentList> UploadDocumentList { get; set; }
        public List<UploadphotoList> UploadphotoList { get; set; }

    }
    public class VisitReport_List : result
    {
        public string visitreport_gid { get; set; }
        public string visitreport_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string inspectingofficials_name { get; set; }
        public string visitdone_name { get; set; }
        public string visitreport_id { get; set; }
    }

    //public class mstVisitpersondtl_list : result
    //{
    //    public string applicationvisit_gid { get; set; }
    //    public string applicationvisit2person_gid { get; set; }
    //    public string clientrepresentative_name { get; set; }
    //    public string clientrepresentative_designation { get; set; }
    //    public string clientrepresentative_personalmail { get; set; }
    //    public string clientrepresentative_officemail { get; set; }
    //    public List<mstVisitpersoncontact_list> mstVisitpersoncontact_list { get; set; }
    //}

    //public class mstVisitpersoncontact_list : result
    //{
    //    public string applicationvisitperson2contact_gid { get; set; }
    //    public string applicationvisit_gid { get; set; }
    //    public string mobile_no { get; set; }
    //    public string whatsapp_mobileno { get; set; }
    //    public string primary_status { get; set; }
    //}

    //public class UploadDocumentList : result
    //{
    //    public string applicationvisit2document_gid { get; set; }
    //    public string applicationvisit_gid { get; set; }
    //    public string document_name { get; set; }
    //    public string document_path { get; set; }
    //    public string photo_gid { get; set; }
    //    public string filename { get; set; }
    //    public string path { get; set; }
    //    public string created_date { get; set; }
    //    public string uploaded_by { get; set; }
    //    public string upload_by { get; set; }
    //    public string document_type { get; set; }
    //    public string updated_date { get; set; }
    //}
    //public class UploadphotoList : result
    //{
    //    public string applicationvisit2photo_gid { get; set; }
    //    public string applicationvisit_gid { get; set; }
    //    public string photo_name { get; set; }
    //    public string document_path { get; set; }
    //    public string filename { get; set; }
    //    public string created_date { get; set; }
    //    public string uploaded_by { get; set; }
    //    public string upload_by { get; set; }
    //    public string document_type { get; set; }
    //    public string updated_date { get; set; }
    //}

    //public class mstVisitpersonaddress_list : result
    //{
    //    public string applicationvisit2address_gid { get; set; }
    //    public string applicationvisit_gid { get; set; }
    //    public string addresstype_gid { get; set; }
    //    public string addresstype_name { get; set; }
    //    public string primary_status { get; set; }
    //    public string address_line1 { get; set; }
    //    public string address_line2 { get; set; }
    //    public string landmark { get; set; }
    //    public string postal_code { get; set; }
    //    public string city { get; set; }
    //    public string taluk { get; set; }
    //    public string district { get; set; }
    //    public string state_gid { get; set; }
    //    public string state_name { get; set; }
    //    public string country { get; set; }
    //}

    public class MdlMstGradeToolView : result
    {
        public string fpo_acscore { get; set; }
        public string numnerofaactive_fig { get; set; }
        public string existinglending_directandindirect { get; set; }
        public string outstandingportfolio_directandindirect { get; set; }
        public string nonnegotiableconditions_met { get; set; }
        public string institution_directandindrectborrowing { get; set; }
        public string totaldisbursements_otherlenders { get; set; }
        public string par90_managedbyonlyinstitution_direct { get; set; }
        public string fpo_recommendation { get; set; }
        public string majorcrops { get; set; }
        public string alternativeincomesource { get; set; }
        public string objevtiveoffpo { get; set; }
        public string recommendation { get; set; }
        public string overallfporating { get; set; }
        public string dateofsurvey { get; set; }
        public string overallfpograde { get; set; }
        public string numberofstates { get; set; }
        public string numberofdistricts { get; set; }
        public string numberofbranches { get; set; }
        public string numberofmembers { get; set; }
        public string numberof_activemembers { get; set; }
        public string numberofgroups { get; set; }
        public string zonaloffices { get; set; }
        public string regionaloffices { get; set; }
        public string branches { get; set; }
        public string adminstaff { get; set; }
        public string fieldstaff { get; set; }
        public string fieldstaff_ratio { get; set; }

        public List<mstassessment_list> mstassessment_list { get; set; }
        public List<mstgradetoolsummary_list> mstgradetoolsummary_list { get; set; }

    }
    public class mstassessment_list : result
    {
        public string assessmentcriteria { get; set; }
        public string maximumscored { get; set; }
        public string actualscored { get; set; }
        public string assessmentcriteria_in { get; set; }
        public string assessmentcriteria_ingrade { get; set; }
        public string shareholdermale_in { get; set; }
        public string shareholderfemale_in { get; set; }
        public string bodmale_in { get; set; }
        public string bodfemale_in { get; set; }
    }

    public class mstgradetoolsummary_list : result
    {
        public string fpo_acscore { get; set; }
        public string dateofsurvey { get; set; }
        public string overallfporating { get; set; }
        public string overallfpograde { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string application2gradingtool_gid { get; set; }

    }

    public class MdlMstIndividualDtlView : result
    {
        public string incometype_name { get; set; }
    public string application_gid { get; set; }
        public string borrower_type { get; set; }
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_dob { get; set; }
        public string age { get; set; }
        public string gender_name { get; set; }
        public string designation_name { get; set; }
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
        public string pan_status { get; set; }
        public string nearsamunnatiabranch_gid { get; set; }
        public string nearsamunnatiabranch_name { get; set; }       
        public string physicalstatus_gid { get; set; }
        public string physicalstatus_name { get; set; }
        public string internalrating_gid { get; set; }
        public string internalrating_name { get; set; }

        public List<contactaddress_list> contactaddress_list { get; set; }
        public List<contactemail_list> contactemail_list { get; set; }
        public List<contactmobileno_list> contactmobileno_list { get; set; }
        public List<contactidproof_list> contactidproof_list { get; set; }
        public List<uploadindividualdoc_list> uploadindividualdoc_list { get; set; }
        public List<contactpanabsencereasons_list> contactpanabsencereasons_list { get; set; }
        public List<mstequipmentholding_list> mstequipmentholding_list { get; set; }
        public List<mstlivestockholding_list> mstlivestockholding_list { get; set; }
    }

    public class contactpanabsencereasons_list
    {
        public string panabsencereason { get; set; }
    }

    public class MdlMstGurantorView : result
    {
        public List<GurantorIndividual_List> GurantorIndividual_List { get; set; }
        public List<GurantorInstitution_List> GurantorInstitution_List { get; set; }

    }
    public class GurantorIndividual_List : result
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
        public string application_gid { get; set; }
    }

    public class GurantorInstitution_List : result
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
    }

    public class MdlMstProductChargesView : result
    {
        public string overalllimit_amount { get; set; }
        public string validityoveralllimit_year { get; set; }
        public string validityoveralllimit_month { get; set; }
        public string validityoveralllimit_days { get; set; }
        public string calculationoveralllimit_validity { get; set; }
        public string enduse_purpose { get; set; }
        public string application2hypothecation_gid { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string security_value { get; set; }
        public string securityassessed_date { get; set; }
        public string asset_id { get; set; }
        public string roc_fillingid { get; set; }
        public string CERSAI_fillingid { get; set; }
        public string hypoobservation_summary { get; set; }
        public string primary_security { get; set; }
        public string program { get; set; }
        public string primaryvaluechain_name { get; set; }
        public string secondaryvaluechain_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string application2loan_gid { get; set; }
        public string application_gid { get; set; }
        public string facility_mode { get; set; }
        public string rate_interest { get; set; }
        public string tenureoverall_limit { get; set; }
        public string product_type { get; set; }
        public string processing_fees { get; set; }
        public string gst { get; set; }
        public string finance_charges { get; set; }
        public string od_amount { get; set; }
        public string escrow_payment { get; set; }
        public string nach_status { get; set; }
        public string remarks { get; set; }
        public string csa_applicability { get; set; }
        public string csaactivity_gid { get; set; }
        public string csaactivity_name { get; set; }
        public string percentageoftotal_limit { get; set; }
        public string loanfacility_amount { get; set; }

        public List<disbursementuploaddocument_list> disbursementuploaddocument_list { get; set; }
        public List<mstLoan_list> mstLoan_list { get; set; }
        public List<mstcollateral_list> mstcollateral_list { get; set; }
        public List<msthypo_list> msthypo_list { get; set; }
        public List<HypoDocumentList> HypoDocumentList { get; set; }
        public List<CollatralDocumentList> CollatralDocumentList { get; set; }
        public List<mstbuyer_list> mstbuyer_list { get; set; }
        public List<servicecharge_List> servicecharge_List { get; set; }
        public List<mstproductdtl_list> mstproductdtl_list { get; set; }
    }

    public class mstproductdtl_list
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

    public class servicecharge_List : result
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
        public string acctinsurance_collectiontype { get; set; }
    }

    public class mstLoan_list : result
    {
        public string application2loan_gid { get; set; }
        public string facilityrequested_date { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
        public string loanfacility_amount { get; set; }
        public string loan_type { get; set; }
        public string rate_interest { get; set; }
        public string roi_margin { get; set; }
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
        public string tenureproduct_days { get; set; }
        public string tenureproduct_month { get; set; }
        public string limit_product { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string program_gid { get; set; }
        public string program { get; set; }
    }

    public class mstcollateral_list : result
    {
        public string application2loan_gid { get; set; }
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string collateralobservation_summary { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
    }

    public class msthypo_list : result
    {
        public string application2hypothecation_gid { get; set; }
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

    public class HypoDocumentList
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
    }

    public class CollatralDocumentList
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
        public string application2collateral_gid { get; set; }
        public string migration_flag { get; set; }
    }

    public class MdlMstGroupMember : result
    {
        public string group_gid { get; set; }
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string stakeholder_type { get; set; }
        public List<groupmember_list> groupmember_list { get; set; }

    }

    public class groupmember_list : result
    {
        public string group_gid { get; set; }
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string stakeholder_type { get; set; }
        public string credit_status { get; set; }
        public string overallCovenantCount { get; set; }
        public string OverallDeferralcount { get; set; }
        public string verifieddeferraldoc { get; set; }
        public string verifiedcovenantdoc { get; set; }
        public string QueryPendingCount { get; set; }
        public string QueryClosedCount { get; set; }
    }

    public class MdlMstContactMobileNumbers : result
    {
        public string primaryindividual_mobileno { get; set; }
        public List<individualmobileno_list> individualmobileno_list { get; set; }

    }

    public class individualmobileno_list : result
    {
        public string contact_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_no { get; set; }
    }

    public class MdlMstContactEmails : result
    {
        public string primaryindividual_email { get; set; }
        public List<individualemail_list> individualemail_list { get; set; }

    }

    public class individualemail_list : result
    {
        public string email_address { get; set; }
    }

    public class MdlMstContactBureau : result
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

    public class MdlCreditView : result
    {
        public string stakeholder_type { get; set; }
        public string customer_name { get; set; }
        public List<individual_List> individual_List { get; set; }
        public List<institution_List> institution_List { get; set; }
    }
    public class individual_List : result
    {
        public string creditguarantordelete_flag { get; set; }
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_dob { get; set; }
        public string main_occupation { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string stakeholder_type { get; set; }
        public string company_name { get; set; }
        public string credit_status { get; set; }
        public string age { get; set; }
        public string designation_name { get; set; }
        public string overallCovenantCount { get; set; }
        public string OverallDeferralcount { get; set; }
        public string verifieddeferraldoc { get; set; }
        public string verifiedcovenantdoc { get; set; }
        public string crimecheck_flag { get; set; }
        public string rtcrimecheck_flag { get; set; }

        public string QueryPendingCount { get; set; }
        public string QueryClosedCount { get; set; }

        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string application_gid { get; set; }
        public string bre_status { get; set; }


    }

    public class institution_List : result
    {
        public string creditguarantordelete_flag { get; set; }
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string cin_no { get; set; }
        public string companytype_name { get; set; }
        public string date_incorporation { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string stakeholder_type { get; set; }
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
        public string crimecheck_flag { get; set; }
        public string rtcrimecheck_flag { get; set; }

    }

    public class MdlRMDtlView : result
    {
        public string department_name { get; set; }
        public string RM_Name { get; set; }
        public string applicationinitiated_date { get; set; }
        public string ccsubmitted_date { get; set; }
        public string ccsubmitted_by { get; set; }
        public string creditunderwritten_by { get; set; }
        public string creditunderwritten_date { get; set; }
    }

    public class MdlExcelImportApplication : result
    {
        public List<individualimport_List> individualimport_List { get; set; }
        public List<institutionimport_List> institutionimport_List { get; set; }
        public List<groupimport_List> groupimport_List { get; set; }
        public List<alldatamodified_List> alldatamodified_List { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }

    public class individualimport_List : result
    {
        public string individual_name { get; set; }
        public string reason { get; set; }    
    }

    public class institutionimport_List : result
    {
        public string company_name { get; set; }
        public string reason { get; set; }
    }

    public class groupimport_List : result
    {
        public string group_name { get; set; }
        public string reason { get; set; }
    }
    public class alldatamodified_List : result
    {
        public string urn { get; set; }
        public string lan { get; set; }
        public string account_status { get; set; }
        public string group_name { get; set; }
        public string reason { get; set; }
        public string penal_interest_raised { get; set; }
        public string penal_interest_collected { get; set; }
        public string penal_interest_pending { get; set; }
        public string ledger_balance { get; set; }
        public string maximum_od_day { get; set; }
        public string rbi_od_days { get; set; }
        public string vertical { get; set; }
        public string next_due_date { get; set; }
        public DateTime next_due1_date { get; set; }
        public string customer_name { get; set; }
        public string stackholder_type { get; set; }
        public string company_type { get; set; }
        public string uncollected_interest { get; set; }
        public string ac_closed_date { get; set; }
        public string Moratorium_Tenure { get; set; }
        public string Moratorium_Interest { get; set; }
        public string Net_Payoff_Amt { get; set; }
        public string late_charge { get; set; }
        public string rpa { get; set; }

    }
    public class MdlMstBankAccountDetails : result
    {
        public List<mstbankacctdtl_list> mstbankacctdtl_list { get; set; }
        public List<institutionbankacc_list> institutionbankacc_list { get; set; }
        public List<individualbankacc_list> individualbankacc_list { get; set; }
        public List<groupbankacc_list> groupbankacc_list { get; set; }
    }

    public class mstbankacctdtl_list : result
    {
        public string group2bank_gid { get; set; }
        public string group_gid { get; set; }
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string group_name { get; set; }
    }   
    public class institutionbankacc_list : result
    {
        public string creditbankdtl_gid { get; set; }
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
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class individualbankacc_list : result
    {
        public string creditbankdtl_gid { get; set; }
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
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class groupbankacc_list : result
    {
        public string creditbankdtl_gid { get; set; }
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
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }

}
