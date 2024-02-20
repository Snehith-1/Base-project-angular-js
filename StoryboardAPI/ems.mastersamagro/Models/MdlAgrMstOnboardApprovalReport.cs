using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values to export buyer and supplier approved record details.
    /// </summary>
    /// <remarks>Written by Kalaiarasan</remarks>

    public class MdlAgrMstOnboardApprovalReport : result
    {
        public List<Company_BuyerOnboardReport_list> Company_BuyerOnboardReport_list { get; set; }
        public List<General_BuyerOnboardReport_list> General_BuyerOnboardReport_list { get; set; }
        public List<Individual_BuyerOnboardReport_list> Individual_BuyerOnboardReport_list { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }

    public class Company_BuyerOnboardReport_list
    {
        public string application_no { get; set; }
        public string urn { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string gst_registered { get; set; }
        public string gst_no { get; set; }
        public string gst_state { get; set; }
        public string date_incorporation { get; set; }
        public string businessstart_date { get; set; }
        public string cin_no { get; set; }
        public string tan_number { get; set; }
        public string companytype_name { get; set; }
        public string official_telephoneno { get; set; }
        public string officialemail_address { get; set; }
        public string stakeholder_type { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string amlcategory_name { get; set; }
        public string businesscategory_name { get; set; }
        public string creditrating_agencyname { get; set; }
        public string creditrating_name { get; set; }
        public string assessed_on { get; set; }
        public string creditrating_link { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string email_address { get; set; }
        public string addresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccounttype_name { get; set; }
        public string joinaccount_status { get; set; }
        public string joinaccount_name { get; set; }
        public string chequebook_status { get; set; }
        public string accountopen_date { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string escrow { get; set; }
        public string lastyear_turnover { get; set; }
        public string incometax_returnsstatus { get; set; }
        public string revenue { get; set; }
        public string profit { get; set; }
        public string fixed_assets { get; set; }
        public string sundrydebt_adv { get; set; }
        public string document_title { get; set; }
        public string document_id { get; set; }
        public string document_name { get; set; }
        public string licensetype_name { get; set; }
        public string license_no { get; set; }
        public string issue_date { get; set; }
        public string expiry_date { get; set; }
        
    }

    public class General_BuyerOnboardReport_list
    {
        public string application_no { get; set; }
        public string virtualaccount_number { get; set; }
        public string customer_urn { get; set; }
        public string customerref_name { get; set; }
        public string constitution_name { get; set; }
        public string sa_status { get; set; }
        public string sa_name { get; set; }
        public string vertical_name { get; set; }
        public string vernacular_language { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string hsn_code { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string geneticcode_name { get; set; }
        public string genetic_status { get; set; }
        public string genetic_remarks { get; set; }

    }

    public class Individual_BuyerOnboardReport_list
    {
        public string urn { get; set; }
        public string institution_name { get; set; }
        public string stakeholder_type { get; set; }
        public string aadhar_no { get; set; }
        public string pan_status { get; set; }
        public string pan_no { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        
    }

}