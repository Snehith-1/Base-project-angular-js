using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// (It's used for Customer Report)Customer Report Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
   
        public class customerediturn : result
        {
            public string customer_gid { get; set; }
            public string customerCodeedit { get; set; }
            public string customerNameedit { get; set; }
            public string district_nameedit { get; set; }
            public string riskmanageredit { get; set; }
            public string risk_managernameedit { get; set; }
            public string contactPersonedit { get; set; }
            public string mobileNoedit { get; set; }
            public string contactnoedit { get; set; }
            public Double mobileNo_edit { get; set; }
            public Double contactno_edit { get; set; }
            public string emailedit { get; set; }
            public string addressline1edit { get; set; }
            public string regionedit { get; set; }
            public string addressline2edit { get; set; }
            public string tomailedit { get; set; }
            public string ccmailedit { get; set; }
            public string countryedit { get; set; }
            public string stateedit { get; set; }
            public string postalcodeedit { get; set; }
            public Double postalcode_edit { get; set; }
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
            public string gst_number { get; set; }
            public string pan_number { get; set; }
            public string constitution_nameedit { get; set; }
            public string constitution_gidedit { get; set; }
            public string major_corporateedit { get; set; }
        }
    public class sanctionloanurn : result
    {
        public List<upload_listurn> upload_listurn { get; set; }
        public List<sanctionloanListurn> sanctionloanListurn { get; set; }
        public List<loanfacilitytype_list> loanfacilitytype_list { get; set; }
    }
    public class sanctionloanListurn : result
    {
        public string sanction_refno { get; set; }
        public string sanction_gid { get; set; }
        public string sanction_date { get; set; }
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string facility_type { get; set; }
        public string sanction_type { get; set; }
        public string entity { get; set; }
        public string colanding_status { get; set; }
        public string colander_name { get; set; }
    }
    public class upload_listurn
    {
        public string document_name { get; set; }
        public string document_type { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
    }
    public class loanfacilitytype_list
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
    }

    public class MdlMisdataimportloanlist : result
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
        public string valid_date { get; set; }
        public string skip_reason { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
        public string TotalDemandDu { get; set; }
        public string Net_Payoff_Amt { get; set; }
        public string tempdn1format_gid { get; set; }
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
    public class MdlCibilSummarydtl : result
    {
        public List<cibilsummarydtl_list> cibilsummarydtl_list { get; set; }
    }
    public class cibilsummarydtl_list
    {
        public string account_no { get; set; }
        public string name { get; set; }
        public string indicator { get; set; }
        public string submission_type { get; set; }
        public string account_status { get; set; }
        public string overdue_amount { get; set; }
        public string cibildatadtl_gid { get; set; }
        public string reason { get; set; }
        public string cibildata_gid { get; set; }
        public string uploaded_date { get; set; }
        public string submitted_on { get; set; }
    }
    public class MdlCibilViewdtl : result
    {
        public string submission_type { get; set; }
        public string submitted_on { get; set; }
        public string indicator { get; set; }
        public string name { get; set; }
        public string account_no { get; set; }
        public string current_balance { get; set; }
        public string overdue_amount { get; set; }
        public string odbucket_01 { get; set; }
        public string odbucket_02 { get; set; }
        public string odbucket_03 { get; set; }
        public string odbucket_04 { get; set; }
        public string odbucket_05 { get; set; }
        public string od_days { get; set; }
        public string cibildatadtl_gid { get; set; }
        public string account_status { get; set; }
        public string closed_on { get; set; }
        public string cibil { get; set; }
        public string highmark { get; set; }
        public string experian { get; set; }
        public string euifax { get; set; }

    }
}