using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstGuarantor:result
    {
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
        public List<guarantor_list> guarantor_list { get; set; }
        public List<guarantormember_list> guarantormember_list { get; set; }
        public List<guarantoraddress_list> guarantoraddress_list { get; set; }
        public List<guarantormobileno_list> guarantormobileno_list { get; set; }
        public List<guarantoridproof_list> guarantoridproof_list { get; set; }
    }
    public class guarantor_list
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
        public string photo_path { get; set; }
        public string photo_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string guarantor_id { get; set; }
    }
    public class guarantormobileno_list
    {
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string customer2mobileno_gid { get; set; }
    }
    public class guarantoraddress_list
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
    public class guarantoridproof_list
    {
        public string customer2identityproof_gid { get; set; }
        public string idproof_no { get; set; }
        public string idproof_type { get; set; }
    }
    public class guarantormember_list
    {
        public string member_name { get; set; }
        public string member_designation { get; set; }
        public string customer2member_gid { get; set; }
    }
}