using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.lgl.Models
{

    public class MdlMisdataimportlist : result
    {
        public string dn_status { get; set; }
        public string DN1_status { get; set; }
        public string customer_name { get; set; }
        public string urn { get; set; }
        public string customer_mail { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string template_content { get; set; }
        public string dn2_content { get; set; }
        public string dn3_content { get; set; }
        public string max_od_days { get; set; }
        public string misdocumentimport_gid { get; set; }
        public string imported_date { get; set; }
        public string imported_by { get; set; }
        public string process_date { get; set; }
        public string status_data { get; set; }
        public string file_name { get; set; }
        public string dncase_gid { get; set; }
        public string DN1status { get; set; }
        public string DN2status { get; set; }
        public string DN3status { get; set; }
        public string cbotemplate_content { get; set; }
        public string courier_refno { get; set; }
        public string courier_center { get; set; }
        public DateTime courier_date { get; set; }
        public string couriered_by { get; set; }
        public string courier_remarks { get; set; }
        public string cbo_status { get; set; }
        public string remarks { get; set; }
        public List<MdlMisdataimport> mdlMisdataimport { get; set; }
        public List<generated_list> generated_list { get; set; }
        public List<skipped_list> skipped_list { get; set; }
        public List<imported_list> imported_list { get; set; }
        public List<otherloan_list> otherloan_list { get; set; }
        public List<exclusion_list> exclusion_list { get; set; }
        public string valid_date { get; set; }
        public string skip_reason { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
        public string TotalDemandDu { get; set; }
        public string Net_Payoff_Amt { get; set; }
        public string tempdn1format_gid { get; set; }
    }
    public class exclusion_list : result
    {
        public string misdata_gid { get; set; }
        public string account_no { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string maturity_date { get; set; }
        public string interest { get; set; }
        public string od_days { get; set; }
        public string latecharge_due { get; set; }
        public string latecharge { get; set; }
        public string ledger { get; set; }
        public string ac_status { get; set; }
        public string ac_closed_date { get; set; }
        public string last_payment { get; set; }
        public string payment { get; set; }
        public string urn { get; set; }
        public string tenure { get; set; }
        public string frequency { get; set; }
        public string schedulde_payment { get; set; }
        public string netpayoff_amount { get; set; }
        public string AccountName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string nextdemandrundat { get; set; }
        public string lastdemandrundate { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string RO_Name { get; set; }
        public string Customer_Type { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
        public string DN1status { get; set; }
        public string DN2status { get; set; }
        public string DN3status { get; set; }
        public string cbo_status { get; set; }
    }
    public class generated_list : result
    {
        public string misdata_gid { get; set; }
        public string account_no { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string maturity_date { get; set; }
        public string interest { get; set; }
        public string od_days { get; set; }
        public string latecharge_due { get; set; }
        public string latecharge { get; set; }
        public string ledger { get; set; }
        public string ac_status { get; set; }
        public string ac_closed_date { get; set; }
        public string last_payment { get; set; }
        public string payment { get; set; }
        public string urn { get; set; }
        public string tenure { get; set; }
        public string frequency { get; set; }
        public string schedulde_payment { get; set; }
        public string netpayoff_amount { get; set; }
        public string AccountName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string nextdemandrundat { get; set; }
        public string lastdemandrundate { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string RO_Name { get; set; }
        public string Customer_Type { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
        public string DN1status { get; set; }
        public string DN2status { get; set; }
        public string DN3status { get; set; }
        public string cbo_status { get; set; }
    }
    public class skipped_list : result
    {
        public string misdata_gid { get; set; }
        public string account_no { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string maturity_date { get; set; }
        public string interest { get; set; }
        public string od_days { get; set; }
        public string latecharge_due { get; set; }
        public string latecharge { get; set; }
        public string ledger { get; set; }
        public string ac_status { get; set; }
        public string ac_closed_date { get; set; }
        public string last_payment { get; set; }
        public string payment { get; set; }
        public string urn { get; set; }
        public string tenure { get; set; }
        public string frequency { get; set; }
        public string schedulde_payment { get; set; }
        public string netpayoff_amount { get; set; }
        public string AccountName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string nextdemandrundat { get; set; }
        public string lastdemandrundate { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string RO_Name { get; set; }
        public string Customer_Type { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
        public string DN1status { get; set; }
        public string DN2status { get; set; }
        public string DN3status { get; set; }
        public string cbo_status { get; set; }
    }
    public class MdlMisdataimport : result
    {
        public string misdata_gid { get; set; }
        public string account_no { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string maturity_date { get; set; }
        public string interest { get; set; }
        public string od_days { get; set; }
        public string latecharge_due { get; set; }
        public string latecharge { get; set; }
        public string ledger { get; set; }
        public string ac_status { get; set; }
        public string ac_closed_date { get; set; }
        public string last_payment { get; set; }
        public string payment { get; set; }
        public string urn { get; set; }
        public string tenure { get; set; }
        public string frequency { get; set; }
        public string schedulde_payment { get; set; }
        public string netpayoff_amount { get; set; }
        public string AccountName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string nextdemandrundat { get; set; }
        public string lastdemandrundate { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string RO_Name { get; set; }
        public string Customer_Type { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
       public string acknowledgement_status { get; set; }
        public string DN1status { get; set; }
        public string DN2status { get; set; }
        public string DN3status { get; set; }
        public string cbo_status { get; set; }
        public string TotalDemandDu { get; set; }
        public string Net_Payoff_Amt { get; set; }
    }
    public class customeredit : result
    {
        public string customer_gid { get; set; }
        public string customerCodeedit { get; set; }
        public string customerNameedit { get; set; }
        public string contactPersonedit { get; set; }
        public string mobileNoedit { get; set; }
        public string contactnoedit { get; set; }
        public string emailedit { get; set; }
        public string addressline1edit { get; set; }
        public string regionedit { get; set; }
        public string addressline2edit { get; set; }
        public string tomailedit { get; set; }
        public string ccmailedit { get; set; }
        public string countryedit { get; set; }
        public string stateedit { get; set; }
        public string postalcodeedit { get; set; }
        public string zonalGid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string creditmanager_name { get; set; }
        public string state_gid { get; set; }
        public string state { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string customer_urnedit { get; set; }

    }

    public class otherloan_list : result
    {
        public string misdata_gid { get; set; }
        public string account_no { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string maturity_date { get; set; }
        public string interest { get; set; }
        public string od_days { get; set; }
        public string latecharge_due { get; set; }
        public string latecharge { get; set; }
        public string ledger { get; set; }
        public string ac_status { get; set; }
        public string ac_closed_date { get; set; }
        public string last_payment { get; set; }
        public string payment { get; set; }
        public string urn { get; set; }
        public string tenure { get; set; }
        public string frequency { get; set; }
        public string schedulde_payment { get; set; }
        public string netpayoff_amount { get; set; }
        public string AccountName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string nextdemandrundat { get; set; }
        public string lastdemandrundate { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string RO_Name { get; set; }
        public string Customer_Type { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
    }
    public class import : result
    {
        public string emp { get; set; }
        public string lawfirm_gid { get; set; }
    }

    public class excel
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class imported_list
    {
        public string misdocumentimport_gid { get; set; }
        public string imported_date { get; set; }
        public string imported_by { get; set; }
        public string process_date { get; set; }
        public string status { get; set; }
        public string file_name { get; set; }
     
    }
    public class DNcount : result
    {
        public string dn1_total_count { get; set; }
        public string dn1_today_count { get; set; }
        public string dn2_total_count { get; set; }
        public string dn2_today_count { get; set; }
        public string dn3_total_count { get; set; }
        public string process_date { get; set; }
        public string employee_name { get; set; }
        public string import_date { get; set; }
        public string dn3_today_count { get; set; }
    }
    public class DNsttaus :result
    {
        public string dn1status { get; set; }
        public string dn2status { get; set; }
        public string dn3status { get; set; }
        public List <dnhistory_list> dnhistory_list { get; set; }
        public List<dn2history_list> dn2history_list { get; set; }
        public List<dn3history_list> dn3history_list { get; set; }
        public List<CBOhistory_list> CBOhistory_list { get; set; }
    }
    public class MdlDN_History : result
    {
        public string DN1template_content { get; set; }
        public string DN1status { get; set; }
        public string dn1status_created_by { get; set; }
        public string dn1status_created_date { get; set; }
        public string dn1couriersent_date { get; set; }
        public string dn1annexuredocument_path { get; set; }
        public string dn1annexuredocument_name { get; set; }
        public string tempdn1format_gid { get; set; }
        public string DN2template_content { get; set; }
        public string DN2status { get; set; }
        public string dn2status_created_by { get; set; }
        public string dn2status_created_date { get; set; }
        public string dn2couriersent_date { get; set; }
        public string dn2annexuredocument_path { get; set; }
        public string dn2annexuredocument_name { get; set; }
        public string DN3template_content { get; set; }
        public string DN3status { get; set; }
        public string dn3status_created_by { get; set; }
        public string dn3status_created_date { get; set; }
        public string dn3couriersent_date { get; set; }
        public string dn3annexuredocument_path { get; set; }
        public string dn3annexuredocument_name { get; set; }
        public string cbo_status { get; set; }
        public string cbostatus_updateddate { get; set; }
        public string cbostatus_updatedby { get; set; }
        public string cbo_courier_sentdate { get; set; }
    }
    public class dnhistory_list
    {
        public string DN1template_content { get; set; }
        public string DN1status { get; set; }
        public string dn1status_created_by { get; set; }
        public string dn1status_created_date { get; set; }
        public string dn1couriersent_date { get; set; }
        public string dn1annexuredocument_path { get; set; }
        public string dn1annexuredocument_name { get; set; }
        public string tempdn1format_gid { get; set; }
    }
    public class dn2history_list
    {
       public string DN2template_content { get; set; }
        public string DN2status { get; set; }
        public string dn2status_created_by { get; set; }
        public string dn2status_created_date { get; set; }
        public string dn2couriersent_date { get; set; }
        public string dn2annexuredocument_path { get; set; }
        public string dn2annexuredocument_name { get; set; }
        public string tempdn1format_gid { get; set; }

    }
    public class dn3history_list
    {
        public string DN3template_content { get; set; }
        public string DN3status { get; set; }
        public string dn3status_created_by { get; set; }
        public string dn3status_created_date { get; set; }
        public string dn3couriersent_date { get; set; }
        public string dn3annexuredocument_path { get; set; }
        public string dn3annexuredocument_name { get; set; }
        public string tempdn1format_gid { get; set; }
    }
    public class CBOhistory_list
    {

        public string cbo_status { get; set; }
        public string cbostatus_updateddate { get; set; }
        public string cbostatus_updatedby { get; set; }
        public string cbo_courier_sentdate { get; set; }
    }
    public class courierinfo: result
    {
        public string courier_refno { get; set; }
        public string courier_center { get; set; }
        public string courier_date { get; set; }
        public string couriered_by { get; set; }
        public string dn1courier_status { get; set; }
        public DateTime delivered_date { get; set; }
        public string courier_remarks { get; set; }
        public DateTime returened_date { get; set; }
        public string dn2courier_refno { get; set; }
        public string dn2courier_center { get; set; }
        public string dn2courier_date { get; set; }
        public string dn2couriered_by { get; set; }
        public string dn2remarks { get; set; }
        public string dn2courier_status { get; set; }
        public DateTime dn2delivered_date { get; set; }
        public DateTime dn2returened_date { get; set; }
        public string dn3courier_refno { get; set; }
        public string dn3courier_center { get; set; }
        public string dn3courier_date { get; set; }
        public string dn3couriered_by { get; set; }
        public string dn3remarks { get; set; }
        public DateTime dn3delivered_date { get; set; }
        public DateTime dn3returened_date { get; set; }
        public string dn3courier_status { get; set; }
        public string CBOcourier_refno { get; set; }
        public string CBOcourier_center { get; set; }
        public string CBOcourier_date { get; set; }
        public string CBOcouriered_by { get; set; }
        public string CBOremarks { get; set; }
        public DateTime CBOdelivered_date { get; set; }
        public DateTime CBOreturened_date { get; set; }
        public string CBOcourier_status { get; set; }
        public string courier_status { get; set; }
        public string urn { get; set; }
    }
    public class mdltemplate : result
    {
        public List<template_list> template_list { get; set; }
    }
    public class template_list : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string customer_mail { get; set; }
        public string customer_name { get; set; }
        public string max_od_days { get; set; }
        public string guarantor_name { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string dn1sanctionref_no { get; set; }
        public string dn1sanction_date { get; set; }
        public string dn1sanction_amount { get; set; }
        public string dn2sanctionref_no { get; set; }
        public string dn2sanction_date { get; set; }
        public string dn2sanction_amount { get; set; }
        public string dn3sanctionref_no { get; set; }
        public string dn3sanction_date { get; set; }
        public string dn3sanction_amount { get; set; }

        public string dncbo3sanctionref_no { get; set; }
        public string dncbo3sanction_date { get; set; }
        public string dncbo3sanction_amount { get; set; }

        public string dncbo2sanctionref_no { get; set; }
        public string dncbo2sanction_date { get; set; }
        public string dncbo2sanction_amount { get; set; }

        public string dn1ref_no { get; set; }
        public string dn2ref_no { get; set; }
        public string dn3ref_no { get; set; }

        public string dncbo2ref_no { get; set; }
        public string dncbo3ref_no { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string dnCBOsanctionref_no { get; set; }
        public string dnCBOsanction_date { get; set; }
        public string dnCBOsanction_amount { get; set; }
        public string dnCBOref_no { get; set; }
        
    }
    public class dnrevert: result
    {
        public string dn_status { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
    }
    public class sanctiondtl :result
    {
        public string dn1sanctionref_no { get; set; }
        public DateTime dn1sanction_date { get; set; }
        public string dn1_date { get; set; }
        public string dn2_date { get; set; }
        public string dn3_date { get; set; }
        public string dnCBO_date { get; set; }
        public string dncbo2_date { get; set; }
        public string dncbo3_date { get; set; }
        public string dn1sanction_amount { get; set; }
        public string dn2sanctionref_no { get; set; }
        public DateTime dn2sanction_date { get; set; }
        public string dn2sanction_amount { get; set; }
        public string dn3sanctionref_no { get; set; }
        public DateTime dn3sanction_date { get; set; }
        public string dn3sanction_amount { get; set; }
        public string urn { get; set; }
        public string dn1ref_no { get; set; }
        public string dn2ref_no { get; set; }
        public string dn3ref_no { get; set; }
        public string dn1_flag { get; set; }
        public string dn2_flag { get; set; }
        public string dn3_flag { get; set; }
        public string dnCBO_flag { get; set; }
        public string dncbo2_flag { get; set; }
        public string dncbo3_flag { get; set; }
        public string dn_flag { get; set; }
        public string dnCBOsanctionref_no { get; set; }
        public DateTime dnCBOsanction_date { get; set; }
        public string dnCBOsanction_amount { get; set; }
        public string dnCBOref_no { get; set; }
        public string dncbo2sanctionref_no { get; set; }
        public DateTime dncbo2sanction_date { get; set; }
        public string dncbo2sanction_amount { get; set; }
        public string dncbo2ref_no { get; set; }
        public string dncbo3sanctionref_no { get; set; }
        public DateTime dncbo3sanction_date { get; set; }
        public string dncbo3sanction_amount { get; set; }
        public string dncbo3ref_no { get; set; }
        public string dn_type { get; set; }
    }
    public class sanctionloan : result
    {
        public string sanction_refno { get; set; }
        public string sanction_gid { get; set; }
        public DateTime sanction_date { get; set; }
    
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string facility_type { get; set; }
    }

    public class exclusionhistorylist
    {
        public List<exclusionhistory> exclusionhistory { get; set; }
    }

    public class exclusionhistory
    {
        public string excluded_date { get; set; }
        public string excluded_by { get; set; }
        public string excluded_status { get; set; }
        public string exclusion_reason { get; set; }
    }

    public class pdfContent
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
        public bool status { get; set; }

    }
}