using System;
using System.Collections.Generic;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values to various functions in Buyer Master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>

    public class MdlMstbuyer : result
        {
        
        public string buyer2address_gid { get; set; }
        public string buyer_gid { get; set; }
        public string creditmapping_gid { get; set; }
        public string buyer_code { get; set; }
        public string buyer_name { get; set; }
        public string coi_date { get; set; }
        public DateTime editcoi_date { get; set; }
        public string businessstart_date { get; set; }
        public DateTime editbusinessstart_date { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string cin_no { get; set; }
        public string pan_no { get; set; }
        public string gst_no { get; set; }
        public string gst_state { get; set; }
        public string contactperson_fn { get; set; }
        public string contactperson_mn { get; set; }
        public string contactperson_ln { get; set; }
        public string created_date { get; set; }
        public string postal_code { get; set; }
        public string credit_status { get; set;}
        public List<buyer_list> buyer_list { get; set; }
        public List<bankaccountlevel_list> bankaccountlevel_list { get; set; }
        public List<bankaccounttype_list> bankaccounttype_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }
        public int Years;
        public int Months;
        public int Days;
    }

        public class MdlbuyerAddress : result
        {
            public string buyer2address_gid { get; set; }
            public string buyer_gid { get; set; }
            public string addresstype_gid { get; set; }
            public string addresstype_name { get; set; }
            public string addressline1 { get; set; }
            public string addressline2 { get; set; }
            public string primary_address { get; set; }
            public string landmark { get; set; }
            public string postal_code { get; set; }
            public string city { get; set; }
            public string taluka { get; set; }
            public string district { get; set; }
            public string state_gid { get; set; }
            public string state_name { get; set; }
            public string country { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public List<buyeraddress_list> buyeraddress_list { get; set; }
        }

    public class MdlbuyerMobileNo : result
    {
        public string buyer2mobileno_gid { get; set; }
        public string buyer_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public List<buyermobileno_list> buyermobileno_list { get; set; }
    }

    public class MdlbuyerEmailAddress : result
    {
        public string buyer2emailaddress_gid { get; set; }
        public string buyer_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public List<buyeremailaddress_list> buyeremailaddress_list { get; set; }
    }

    public class MdlbuyerBank : result
    {
        public string buyer2bank_gid { get; set; }
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
        public List<buyerbank_list> buyerbank_list { get; set; }
    }

    public class MdlbuyerGST : result
    {
        public string buyer2gst_gid { get; set; }
        public string buyer_gid { get; set; }
        public string gststate_name { get; set; }
        public string gst_no { get; set; }
        public string gstregister_status { get; set; }
        public BuyerGSTDetails[] GSTArray { get; set; }
        public List<buyergst_list> buyergst_list { get; set; }
    }

    public class buyer_list
    {
        public string buyer_gid { get; set; }
        public string buyer_code { get; set; }
        public string buyer_name { get; set; }
        public string coi_date { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string cin_no { get; set; }
        public string pan_no { get; set; }
        public string gst_no { get; set; }
        public string gst_state { get; set; }
        public string contactperson_fn { get; set; }
        public string contactperson_mn { get; set; }
        public string contactperson_ln { get; set; }
        public string created_date { get; set; }
        public string creditActive_status { get; set; }
        public string credit_status { get; set; }
        public string creditstatus_Approval { get; set; }
    }

    public class buyeraddress_list
    {
        public string buyer2address_gid { get; set; }
        public string buyer_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string primary_address { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state_gid { get; set; }
        public string state_name { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class buyermobileno_list
    {
        public string buyer2mobileno_gid { get; set; }
        public string buyer_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
    }

    public class buyeremailaddress_list
    {
        public string buyer2emailaddress_gid { get; set; }
        public string buyer_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
    }

    public class buyerbank_list
    {
        public string buyer2bank_gid { get; set; }
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
    }

    public class buyergst_list
    {
        public string buyer2gst_gid { get; set; }
        public string buyer_gid { get; set; }
        public string gststate_name { get; set; }
        public string gstregister_status { get; set; }
        public string gst_no { get; set; }
    }

    public class BuyerGSTDetails
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

    public class postalcodedetails_list
    {
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
    }



}