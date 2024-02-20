using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.lgl.Models
{
    public class sanctionloanurn : result
    {
        public List<upload_listurn> upload_listurn { get; set; }
        public List<sanctionloanListurn> sanctionloanListurn { get; set; }
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

    public class loanListdetailurn : result
    {
        public List<loanListurn> loanListurn { get; set; }
    }
    public class loanListurn : result
    {
        public string loanref_no { get; set; }
        public string sanction_gid { get; set; }
        public string loan_title { get; set; }
        public string loan_date { get; set; }
        public string facility_type { get; set; }
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
    }
   
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
    public class mdlcustomer2userdtl : result
    {
        public string customer_urn { get; set; }
        public string vertical { get; set; }
        public string constitution { get; set; }
        public string business_unit { get; set; }
        public string primaryvalue_chain { get; set; }
        public string secondaryvalue_chain { get; set; }
        public string sa_idname { get; set; }
        public string sa_payout { get; set; }
        public string sa_status { get; set; }
        public string zonal_head { get; set; }
        public string business_head { get; set; }
        public string cluster_manager { get; set; }
        public string rm_name { get; set; }
        public string credit_manager { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string personalemail_address { get; set; }
        public string officailemail_address { get; set; }
        public string telephone_no { get; set; }
        public string contact_person { get; set; }
        public string user_type { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string cin_date { get; set; }
        public string cin_no { get; set; }
        public string landmark { get; set; }
        public string month_business { get; set; }
        public string year_business { get; set; }
        public string credit_rating { get; set; }
        public string escrow { get; set; }
        public string contactperson_designation { get; set; }
        public string gst_no { get; set; }
        public string company_type { get; set; }
        public string customer_type { get; set; }
        public string photo_path { get; set; }
        public string photo_name { get; set; }
        public List<customer2userdtl_list> customer2userdtl_list { get; set; }
        public List<institutionmember_list> institutionmember_list { get; set; }
        public List<address_list> address_list { get; set; }
        public List<mobileno_list> mobileno_list { get; set; }
        public List<idproof_list> idproof_list { get; set; }
    }
    public class address_list
    {
        public string customer2address_gid { get; set; }
        public string address_type { get; set; }
        public string primary_address { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_gid { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string country { get; set; }
    }
    public class idproof_list
    {
        public string customer2identityproof_gid { get; set; }
        public string idproof_no { get; set; }
        public string idproof_type { get; set; }
    }
    public class mobileno_list
    {
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string customer2mobileno_gid { get; set; }
    }
    public class institutionmember_list
    {
        public string member_name { get; set; }
        public string member_designation { get; set; }
        public string customer2member_gid { get; set; }
    }
    public class customer2userdtl_list
    {
        public string customer2usertype_gid { get; set; }
        public string name { get; set; }
        public string dob { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string personalemail_address { get; set; }
        public string officailemail_address { get; set; }
        public string telephone_no { get; set; }
        public string contact_person { get; set; }
        public string user_type { get; set; }
        public string aadhar_no { get; set; }
        public string pan_no { get; set; }
        public string cin_date { get; set; }
        public string cin_no { get; set; }
        public string landmark { get; set; }
        public string month_business { get; set; }
        public string year_business { get; set; }
        public string credit_rating { get; set; }
        public string escrow { get; set; }
        public string contactperson_designation { get; set; }
        public string gst_no { get; set; }
        public string company_type { get; set; }
        public string customer_type { get; set; }
    }

    public class DNsanctiondtl : result
    {
        public string dnsanctionref_no { get; set; }
        public DateTime dnsanction_date { get; set; }
        public string dn_date { get; set; }
        public string user_type { get; set; }
        public string template_type { get; set; }
        public string guarantor_name { get; set; }
        public string dn2_date { get; set; }
        public string dn3_date { get; set; }
        public string dnCBO_date { get; set; }
        public string dnsanction_amount { get; set; }
        public string dn2sanctionref_no { get; set; }
        public DateTime dn2sanction_date { get; set; }
        public string dn2sanction_amount { get; set; }
        public string dn3sanctionref_no { get; set; }
        public DateTime dn3sanction_date { get; set; }
        public string dn3sanction_amount { get; set; }
        public string urn { get; set; }
        public string dnref_no { get; set; }
        public string dn2ref_no { get; set; }
        public string dn3ref_no { get; set; }
        public string dn1_flag { get; set; }
        public string dn2_flag { get; set; }
        public string dn3_flag { get; set; }
        public string dnCBO_flag { get; set; }
        public string dn_flag { get; set; }
        public string dnCBOsanctionref_no { get; set; }
        public DateTime dnCBOsanction_date { get; set; }
        public string dnCBOsanction_amount { get; set; }
        public string dnCBOref_no { get; set; }
        public string template_gid { get; set; }
        public string guarantor_gid { get; set; }
    }
    public class MdlDNSkipHistory:result
    {
        public List <dnskiphistory_list> dnskiphistory_list { get; set; }
    }
    public class dnskiphistory_list
    {
        public string skipped_by { get; set; }
        public string skipped_date { get; set; }
        public string skip_reason { get; set; }
        public string validity { get; set; }
    }
}