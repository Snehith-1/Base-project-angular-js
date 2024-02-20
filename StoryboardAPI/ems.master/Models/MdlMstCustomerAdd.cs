using System.Collections.Generic;


namespace ems.master.Models
{
    public class MdlMstaddresstype : result
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
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address_typegid { get; set; }
        public string landmark { get; set; }
        public string buyer2address_gid { get; set; }
        public string buyer_gid { get; set; }
        public List<address_list> address_list { get; set; }
    }
    public class MdlID_proof : result
    {
        public string customer2identityproof_gid { get; set; }
        public string idproof_no { get; set; }
        public string idproof_type { get; set; }
        public List<idproof_list> idproof_list { get; set; }
    }
    public class Mdlmobile_no: result
    {
        public string dob { get; set; }
        public string age { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string buyer_gid { get; set; }
        public string customer2mobileno_gid { get; set; } 
        public string buyer2mobileno_gid { get; set; }
        public List <mobileno_list> mobileno_list { get; set; }    
    }
    public class MdlMember : result
    {
        public string member_name { get; set; }
        public string member_designation { get; set; }
        public string customer2member_gid { get; set; }
        public List<member_list> member_list { get; set; }
    }
    public class mobileno_list
    {
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string customer2mobileno_gid { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string buyer2mobileno_gid { get; set; }
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
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address_typegid { get; set; }
        public string landmark { get; set; }
        public string buyer2address_gid { get; set; }
        public string buyer_gid { get; set; }
    }
    public class idproof_list
    {
        public string customer2identityproof_gid { get; set; }
        public string idproof_no { get; set; }
        public string idproof_type { get; set; }
    }
    public class member_list
    {
        public string member_name { get; set; }
        public string member_designation { get; set; }
        public string customer2member_gid { get; set; }
    }
    public class UploadDocumentname : result
    {
        public string photo_name { get; set; }
        public string photo_path { get; set; }
    }
    public class mdlcustomer2userdtl : result
    {
        public string lstmpcount { get; set; }
        public string lsmstcount { get; set; }
        public string customer_gid { get; set; }
        public string customer2usertype_gid { get; set; }
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
        public string zonal_riskmanagerGID { get; set; }
        public string zonal_riskmanagerName { get; set; }
        public string risk_managerGID { get; set; }
        public string riskmanager_name { get; set; }
        public string riskMonitoring_GID { get; set; }
        public string riskMonitoring_Name { get; set; }
        public string ccmail { get; set; }
        public string major_corporate { get; set; }
        public string usertype_gid { get; set; }
        public string guarantor_id { get; set; }
        public string customername { get; set; }
        public List <customer2userdtl_list> customer2userdtl_list { get; set; }
        public List<institutionmember_list> institutionmember_list { get; set; }
        public List<member_list> member_list { get; set; }
        public List<address_list> address_list { get; set; }
        public List<mobileno_list> mobileno_list { get; set; }
        public List<idproof_list> idproof_list { get; set; }
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
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string customer_type { get; set; }
        public string customer_gid { get; set; }
    }
    public class mdlcreatecustomer : result
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string mobileno { get; set; }
        public string contactnumber { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string region { get; set; }
        public string state { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string state_gid { get; set; }
        public string customer_urn { get; set; }
        public string gst_number { get; set; }
        public string pan_number { get; set; }
        public string constitution_name { get; set; }
        public string constitution_gid { get; set; }
        public string zonalGid { get; set; }
        public string creditmanagerGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string major_corporate { get; set; }
        public string creditmanager_name { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string zonal_riskmanagerGID { get; set; }
        public string zonal_riskmanagerName { get; set; }
        public string risk_managerGID { get; set; }
        public string riskmanager_name { get; set; }
        public string riskMonitoring_GID { get; set; }
        public string riskMonitoring_Name { get; set; }
        public string businessunit_name { get; set; }
        public string businessunit_gid { get; set; }
        public string sa_status { get; set; }
        public string sa_idname { get; set; }
        public string sa_payout { get; set; }
        public string sa_id_gid { get; set; }
        public string ccmail { get; set; }
        public string relationship_manager { get; set; }
        public List<primaryvaluechain_list> primaryvaluechain_list { get; set; }
        public List<secondaryvaluechain_list> secondaryvaluechain_list { get; set; }
        public List<customer2userdtl_list> customer2userdtl_list { get; set; }
    }
    public class primaryvaluechain_list
    {
        public string valuechain_name { get; set; }
        public string valuechain_gid { get; set; }
    }
    public class secondaryvaluechain_list
    {
        public string valuechain_name { get; set; }
        public string valuechain_gid { get; set; }
    }

  
    public class MdlCustomer : result
    {
        public List<customer_list> customer_list { get; set; }  
    }
    public class customer_list
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string vertical_code { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerName { get; set; }
        public string mail_count { get; set; }
        public string customer_urn { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_type { get; set; }
       
    }
}