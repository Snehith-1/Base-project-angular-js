﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    
    public class MdlMstSAOnboardingBussDevtVerification
    {
        public class result
        {
            public bool status { get; set; }
        }
    }
   
    public class MdlValuesList : result
    {
        public List<Gender_list> Gender_Grp { get; set; }
        public List<RM_List> Rm_Grp { get; set; }
        public List<Individual_list> Individual_listGrp { get; set; }
        public List<Institution_summarylist> Institution_summary { get; set; }
        public string Combo_name { get; set; }
        public string satype_gid { get; set; }
        public List<Combo_list> Combo { get; set; }

        public string tagging_flag { get; set; }
    }
   
        public class Combo_list
    {
        public string Combo_name { get; set; }

    }


    public class Gender_list
    {
        public string gender_gid { get; set; }
        public string gender_name { get; set; }
               public List<Gender_list> Gender_Grp { get; set; }
        
    }
    public class RM_List
    {
        public string user_gid { get; set; }
        public string RMName { get; set; }
        public List<RM_List> Rm_Grp { get; set; }
    }
    public class InstitutioneditVerification : result
    {
        public string interviewevalution { get; set; }
        public string applicationform { get; set; }
        public string yearsitreturns { get; set; }
        public string bankstatement { get; set; }
        public string kycdocuments { get; set; }
        public string prospect { get; set; }
        public string vettingstatus { get; set; }
        public string scannedcopyreception { get; set; }
        public string addressproof { get; set; }
        public string photographs { get; set; }
        public string cancelledcheckleaf { get; set; }
        public string houseofficeverification { get; set; }
        public string agreementexecutiondate { get; set; }
        public string agreementexpirydate { get; set; }
        public string agreementstatus { get; set; }
        public string bookletnumber { get; set; }
        public string verificationremarks { get; set; }
        public string approvalinitated_flag { get; set; }
        public string approval_flag { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string satype_name { get; set; }
        public string saentitytype_name { get; set; }
        public string sa_associatename { get; set; }
        public string sa_autogeneratedid { get; set; }
        public string sa_contactfirstname { get; set; }
        public string sa_contactmiddlename { get; set; }
        public string sa_contactlastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string sa_dateofincorporation { get; set; }
        public DateTime sadateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public DateTime sacompanystdate { get; set; }
        public string editsa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string rejected_remarks { get; set; }
        public string editsa_startdate { get; set; }
        public string editsa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string referred_by { get; set; }
        public List<Institution_Edit_list> Institution_BussVer_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }
        public string sa_updated_by { get; set; }
        public string sa_updated_date { get; set; }
        public string approved_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approvalstatus { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string utr_no { get; set; }

        public string sa_appcreditedamount { get; set; }
        public int Years;
        public int Months;
        public int Days;
        public string rm_tagging_id { get; set; }
        public string rm_tagging_name { get; set; }
        public string bureau_check { get; set; }
        public string crime_check { get; set; }
        public string training_status { get; set; }
        public string remarks { get; set; }
        public string verify_flag { get; set; }
        public string update_flag { get; set; }
        public string reporting_manager { get; set; }
        public string openquerycount { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string recordsource { get; set; }
        public string tagged_remarks { get; set; }
        public DateTime ratingas_date { get; set; }
        public string ratingas_datecredit { get; set; }

        public string tagging_remarks { get; set; }

        public List<taggedinstitution_list> taggedinstitution_list { get; set; }
    }
    public class taggedinstitution_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string taggedemployeeinstitution_name { get; set; }
        public string taggedemployeeinstitutionlog_gid { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string rmtagged_remarks { get; set; }

    }

    public class IndividualeditVerification : result
    {
        
        public string sa_updated_by { get; set; }
        public string sa_updated_date { get; set; }
        public string interviewevalution { get; set; }
        public string applicationform { get; set; }
        public string yearsitreturns { get; set; }
        public string bankstatement { get; set; }
        public string kycdocuments { get; set; }
        public string prospect { get; set; }
        public string vettingstatus { get; set; }
        public string scannedcopyreception { get; set; }
        public string addressproof { get; set; }
        public string photographs { get; set; }
        public string cancelledcheckleaf { get; set; }
        public string houseofficeverification { get; set; }
        public string agreementexecutiondate { get; set; }
        public string agreementexpirydate { get; set; }
        public string agreementstatus { get; set; }
        public string bookletnumber { get; set; }
        public string verificationremarks { get; set; }
        public string approvalinitated_flag { get; set; }
        public string approval_flag { get; set; }
        public string reporting_manager { get; set; }
        public string sacontact_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string satype_name { get; set; }
        public string saentitytype_name { get; set; }
        public string rejected_remarks { get; set; }
        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_autogeneratedid { get; set; }
        public string sa_pannumber { get; set; }
        public string pan_status { get; set; }
        public string openquerycount { get; set; }
        public string sa_appcrediteddate { get; set; }
        public DateTime saappcrediteddate { get; set; }
        public string tagged_remarks { get; set; }
        public string sa_aadharnumber { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }
        public string sa_onboard_flag { get; set; }
        public string gender { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public List<Institution_Edit_list> Institution_Edit_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }     
        public string approved_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approvalstatus { get; set; }
        public int Years;
        public int Months;
        public int Days;
        public List<contactpanabsencereasonsa_list> contactpanabsencereasonsa_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public string rm_tagging_id { get; set; }
        public string rm_tagging_name { get; set; }
        public string bureau_check { get; set; }
        public string crime_check { get; set; }
        public string training_status { get; set; }
        public string remarks { get; set; }
        public string verify_flag { get; set; }

        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public DateTime ratingas_date { get; set; }
        public string tagging_remarks { get; set; }

        public List<taggedindividual_list> taggedindividual_list { get; set; }
    }
    public class taggedindividual_list
    {
        public string sacontact_gid { get; set; }
        public string taggedemployeeindividuallog_gid { get; set; }
        public string individualtaggedemployee_name { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string rmtagged_remarks { get; set; }

    }
    public class institutionsamcodesupdate : result
    {
        public string samfin_code { get; set; }
        public string samagro_code { get; set; }
        public string codecreation_date { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sacontact_gid { get; set; }
    }

        public class institutionupdate:result
    {
        public string satype_gid { get; set; }        
        public string satype_name { get; set; }
        public string sbainstitution_gid { get; set; }
        public string pan_number { get; set; }
        public string sbainstitution_name { get; set; }
        public string constitution { get; set; }
        public string current_business { get; set; }
        public string date_of_incorporation { get; set; }
        public string sbafirst_name { get; set; }
        public string sbamiddle_name { get; set; }
        public string sbalast_name { get; set; }
        public string designation { get; set; }
        public string gender { get; set; }
        public string mobile_no { get; set; }
        public string Alternativemobile_no { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string email_add { get; set; }
        public string approved_flag { get; set; }
        public string bureau_check { get; set; }
        public string remarks { get; set; }
        public string rm_tagging { get; set; }
        public string training_status { get; set; }
        public string crime_check { get; set; }
        public string pan { get; set; }
        public string gst_certificate { get; set; }
       

    }
    public class InstitutionbussVerification : result
    {
        public string interviewevalution { get; set; }
        public string applicationform { get; set; }
        public string yearsitreturns { get; set; }
        public string bankstatement { get; set; }
        public string kycdocuments { get; set; }
        public string prospect { get; set; }
        public string vettingstatus { get; set; }
        public string scannedcopyreception { get; set; }
        public string addressproof { get; set; }
        public string photographs { get; set; }
        public string cancelledcheckleaf { get; set; }
        public string houseofficeverification { get; set; }
        public string agreementexecutiondate { get; set; }
        public string agreementexpirydate { get; set; }
        public string agreementstatus { get; set; }
        public string bookletnumber { get; set; }
        public string verificationremarks { get; set; }
        public string approvalinitated_flag { get; set; }
        public string approval_flag { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string satype_name { get; set; }
        public string saentitytype_name { get; set; }
        public string sa_associatename { get; set; }
        public string sa_autogeneratedid { get; set; }
        public string sa_contactfirstname { get; set; }
        public string sa_contactmiddlename { get; set; }
        public string sa_contactlastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string sa_dateofincorporation { get; set; }
        public DateTime sadateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public DateTime sacompanystdate { get; set; }
        public string editsa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string editsa_startdate { get; set; }
        public string editsa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public List<Institution_Edit_list> Institution_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }
        public string sa_updated_by { get; set; }
        public string sa_updated_date { get; set; }
        public string approved_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approvalstatus { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }
        public int Years;
        public int Months;
        public int Days;
        public string rm_tagging_id { get; set; }
        public string rm_tagging_name { get; set; }
        public string bureau_check { get; set; }
        public string crime_check { get; set; }
        public string training_status { get; set; }
        public string remarks { get; set; }

        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }

        public string rdbgstregister_status { get; set; }
        public DateTime ratingas_date { get; set; }

    }
    public class Individualverifedit : result
    {
        public string sa_updated_by { get; set; }
        public string sa_updated_date { get; set; }
        public string interviewevalution { get; set; }
        public string applicationform { get; set; }
        public string yearsitreturns { get; set; }
        public string bankstatement { get; set; }
        public string kycdocuments { get; set; }
        public string prospect { get; set; }
        public string vettingstatus { get; set; }
        public string scannedcopyreception { get; set; }
        public string addressproof { get; set; }
        public string photographs { get; set; }
        public string cancelledcheckleaf { get; set; }
        public string houseofficeverification { get; set; }
        public string agreementexecutiondate { get; set; }
        public string agreementexpirydate { get; set; }
        public string agreementstatus { get; set; }
        public string bookletnumber { get; set; }
        public string verificationremarks { get; set; }
        public string approvalinitated_flag { get; set; }
        public string approval_flag { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public DateTime ratingas_date { get; set; }
        public string ratingas_datecredit { get; set; }
        public string gender { get; set; }
        public string sacontact_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string satype_name { get; set; }
        public string saentitytype_name { get; set; }
        public string present_occupation { get; set; }
        public string work_experience { get; set; }
        public string Expagri_business { get; set; }

        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_autogeneratedid { get; set; }
        public string sa_pannumber { get; set; }
        public string pan_status { get; set; }

        public string sa_appcrediteddate { get; set; }
        public DateTime saappcrediteddate { get; set; }

        public string sa_aadharnumber { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }
        public string sa_onboard_flag { get; set; }

        public string micr { get; set; }
        public string branch_address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public List<Institution_Edit_list> Institution_Edit_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }

        public string approved_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approvalstatus { get; set; }
        public string update_flag { get; set; }
        public string verify_flag { get; set; }
        public string utr_no { get; set; }

        public string rm_tagging_id { get; set; }
        public string rm_tagging_name { get; set; }
        public string bureau_check { get; set; }
        public string crime_check { get; set; }
        public string training_status { get; set; }
        public string remarks { get; set; }
        public string referred_by { get; set; }
        public string recordsource { get; set; }

        public int Years;
        public int Months;
        public int Days;
        public List<contactpanabsencereasonsa_list> contactpanabsencereasonsa_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
    }

    public class Institution_Edit_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string sa_associatename { get; set; }
        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string sa_dateofincorporation { get; set; }
        public string editsa_dateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public string editsa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string editsa_startdate { get; set; }
        public string editsa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }
        public string rm_tagging { get; set; }
        public string bureau_check { get; set; }
        public string crime_check { get; set; }
        public string training_status { get; set; }
        public string remarks { get; set; }

    }
    public class Institution_summarylist
    {
        public string sbainstitution_gid { get; set; }
        public string pan_number { get; set; }
        public string sbainstitution_name { get; set; }
        public string constitution { get; set; }
        public string current_business { get; set; }
        public string date_of_incorporation { get; set; }
        public string sbafirst_name { get; set; }
        public string sbamiddle_name { get; set; }
        public string sbalast_name { get; set; }
        public string designation { get; set; }
        public string gender { get; set; }
        public string mobile_no { get; set; }
        public string Alternativemobile_no { get; set; }    
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string email_add { get; set; }
        public string approved_flag { get; set; }
        public string bureau_check { get; set; }
        public string remarks { get; set; }
        public string rm_tagging { get; set; }
        public string training_status { get; set; }
        public string crime_check { get; set; }
        public List<Institution_summarylist> Institution_summary { get; set; }

    }
    public class Individual_update:result
    {
        public string sbaindividual_gid { get; set; }
        public string pan_number { get; set; }
        public string sbafirst_name { get; set; }
        public string sbamiddle_name { get; set; }
        public string sbalast_name { get; set; }
        public string aadhar_number { get; set; }
        public string date_of_birth { get; set; }
        public string edu_Qualification { get; set; }
        public string present_occupation { get; set; }
        public string work_experience { get; set; }
        public string Expagri_business { get; set; }
        public string father_name { get; set; }
        public string mobile_no { get; set; }
        public string Alternativemobile_no { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string email_add { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string pincode    { get; set; }
        public string bureau_check { get; set; }
        public string remarks { get; set; }
        public string rm_tagging { get; set; }
        public string training_status { get; set; }
        public string crime_check { get; set; }
        public string approved_flag { get; set; }
        public string pan { get; set; }
        public string aadhar { get; set; }
        public string passport_photo { get; set; }
        public string satype_gid { get; set; }
        public string satype_name { get; set; }

        
    }
    public class Individual_list
    {
        public string sbaindividual_gid { get; set; }
        public string pan_number { get; set; }
        public string sbafirst_name { get; set; }
        public string sbamiddle_name { get; set; }
        public string sbalast_name { get; set; }
        public string aadhar_number { get; set; }
        public string date_of_birth { get; set; }
        public string edu_Qualification { get; set; }
        public string present_occupation { get; set; }
        public string work_experience { get; set; }
        public string Expagri_business { get; set; }
        public string father_name { get; set; }
        public string mobile_no { get; set; }
        public string Alternativemobile_no { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string email_add { get; set; }
        public string bureau_check { get; set; }
        public string remarks { get; set; }
        public string rm_tagging { get; set; }
        public string training_status { get; set; }
        public string crime_check { get; set; }
        public string approved_flag { get; set; }
        public List<Individual_list> Individual_listGrp { get; set; }
    }
    public class geSaOnboardingCount
    {
        public string institution_count { get; set; }
        public string individual_count { get; set; }
        public string institution_rejectedcount { get; set; }
        public string institution_groupingcount { get; set; }
        public string individual_rejectedcount { get; set; }
        public string individual_groupingcount { get; set; }
        public string institutioninitiated_count { get; set; }
        public string individualinitiated_count { get; set; }
        public string institutiontrash_count { get; set; }
        public string individualtrash_count { get; set; }
        public string institutiondeferred_count { get; set; }
        public string individualdeferred_count { get; set; }
    }
    public class SbaReport : result
    {
        public List<SbaReportList> SbaReportList { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
        public string satype_name { get; set; }
        public string approvalstatus { get; set; }
       
        
    }
    public class SbaReportList
    {
        public string satype_name { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_associatename { get; set; }
        public string saentitytype_name { get; set; }
        public string approvalstatus { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sa_companypan { get; set; }
        public string bureau_score { get; set; }
        public string training_status { get; set; }
        public string agreementexecutiondate { get; set; }
        public string agreementexpirydate { get; set; }
        public string agreementstatus { get; set; }
        public string agroagreementexecutiondate { get; set; }
        public string agroagreementexpirydate { get; set; }
        public string agroagreementstatus { get; set; }
        public string sa_appcreditedamount { get; set; }
        public string sa_apputr { get; set; }

    }
    public class reportValues : result
    {        
        public string approvalstatus { get; set; }
        public List<reportList> reportList { get; set; }
    }
    public class reportList
    {
        public string sa_autogeneratedid { get; set; }
        public string approvalstatus { get; set; }
        public string sa_associatename { get; set; }
        public string sba_associatetype { get; set; }
        public string saentitytype_name { get; set; }
        public string sa_updated_date { get; set; }
        public string sa_updated_by { get; set; }
        public string approvedBy { get; set; }
        public string sa_reportingmanager { get; set; }

    }
    public class bdemployee : result
    {
        public List<rmemployeelists> rmemployeelists { get; set; }
    }
    public class rmemployeelists
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Mdlbdinstitutionraisequery : result
    {

        public string sacontactinstitution_gid { get; set; }
        public string bdinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string bdinstitutionraisequery_status { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<bdinstitutionraisequery_list> bdinstitutionraisequery_list { get; set; }

    }
    public class bdinstitutionraisequery_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string bdinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string bdinstitutionraisequery_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
    }
    public class Mdlbdindividualraisequery : result
    {

        public string sacontact_gid { get; set; }
        public string bdindividualraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string bdindividualraisequery_status { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<bdindividualraisequery_list> bdindividualraisequery_list { get; set; }

    }
    public class bdindividualraisequery_list
    {
        public string sacontact_gid { get; set; }
        public string bdindividualraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string bdindividualraisequery_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
    }
    public class Codes
    {
        public string fincode { get; set; }
        public string agrocode { get; set; }
    }
    }