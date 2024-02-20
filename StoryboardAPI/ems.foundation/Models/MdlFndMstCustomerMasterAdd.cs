using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.foundation.Models
{

    public class MdlFndMstCustomerMasterAdd: result
    {

        public string openquerycount { get; set; }
        public string fndmanagement2cheque_gid { get; set; }
        public string fndmanagement_gid { get; set; }
        public string accountholder_name { get; set; }
        public string status_remarks { get; set; }     
        public string account_number { get; set; }
        public string msme_radio { get; set; }
        public string customerpending_count { get; set; }
        public string customerapprover_count { get; set; }
        public string customerreject_count { get; set; }
        public string bank_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string cheque_no { get; set; }
        public string ifsc_code { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string mergedbankingentity_gid { get; set; }
        public string mergedbankingentity_name { get; set; }
        public string special_condition { get; set; }
        public string general_remarks { get; set; }
        public string cts_enabled { get; set; }
        public string cheque_type { get; set; }
        public string date_chequetype { get; set; }
        public string date_chequepresentation { get; set; }
        public string status_chequepresentation { get; set; }
        public string date_chequeclearance { get; set; }
        public string status_chequeclearance { get; set; }
        public List<customerstatus_list> customerstatus_list { get; set; }
        public List<customerrejected_list> customerrejected_list { get; set; }
        public List<customerapprover_list> customerapprover_list { get; set; }
        public List<cheque_list> cheque_list { get; set; }
        public List<customergst_list> customergst_list { get; set; }
        public List<constitution_list> constitution_list { get; set; }
        public List<assessmentagency_list> assessmentagency_list { get; set; }
        public List<assessmentagencyrating_list> assessmentagencyrating_list { get; set; }
        public List<amlcategory_list> amlcategory_list { get; set; }
        public List<businesscategory_list> businesscategory_list { get; set; }
        public List<designation_list> designation_list { get; set; }
        public List<individualproof_list> individualproof_list { get; set; }      
        public List<state_list> state_list { get; set; }
        public List<customerpending_list> customerpending_list { get; set; }
        public List<customerraisequery_list> customerraisequery_list { get; set; }
        public string customer2address_gid { get; set; }
        public string customer_gid { get; set; }
        public string creditmapping_gid { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string coi_date { get; set; }
        public DateTime editcoi_date { get; set; }
        public DateTime editrating_date { get; set; }
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
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string individualproof_gid { get; set; }
        public string individualproof_name { get; set; }
        public string rating_date { get; set; }
        public string amlcategory_gid { get; set; }
        public string amlcategory_name { get; set; }
        public string businesscategory_gid { get; set; }
        public string businesscategory_name { get; set; }
        public string msme_registration { get; set; }      
        public string contactperson_mn { get; set; }
        public string contactperson_ln { get; set; }
        public string postal_code { get; set; }
        public string credit_status { get; set; }
        public string remarks { get; set; }        
        public List<customer_list> customer_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public List<customermobileno_list> customermobileno_list { get; set; }      
        public string content { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string queryresponse_remarks { get; set; }
        public string customerraisequery_gid { get; set; }
        public int Years;
        public int Months;
        public int Days;
    }
    public class uploaddocument : result
    {
        public List<upload_list> upload_list { get; set; }
    }

    public class customerpending_list
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
    }

    public class customerraisequery_list : result
    {
        public string customer_gid { get; set; }
        public string customerraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string customerraisequery_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string queryresponse_remarks { get; set; }
        public string query_responseby { get; set; }
        public string campaignmanager2employee_gid { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
    }


    public class upload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlDropDownUdc : result
    {
        public List<bankname_list> bankname_list { get; set; }

    }

    public class bankname_list
    {
        public string bankname_gid { get; set; }
        public string bankname_name { get; set; }
    }

    public class customerstatus_list
    {

        public string status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string customer_gid { get; set; }
        public string remarks { get; set; }
   
    }

    
    public class MdlcustomerAddress : result
    {
        public string customer2address_gid { get; set; }
        public string customer_gid { get; set; }
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
        public List<customeraddress_list> customeraddress_list { get; set; }
    }

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
        public string customer_gid { get; set; }
       

        

    }
    public class MdlcustomerMobileNo : result
    {
        public string customer2mobileno_gid { get; set; }
        public string customer_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public List<customermobileno_list> customermobileno_list { get; set; }
    }
    public class MdlbuyerGST : result
    {
        public string customer2gst_gid { get; set; }
        public string customer_gid { get; set; }
        public string gststate_name { get; set; }
        public string gst_no { get; set; }
        public string gstregister_status { get; set; }
        //public BuyerGSTDetails[] GSTArray { get; set; }
        //public List<buyergst_list> buyergst_list { get; set; }
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
    public class MdlcustomerEmailAddress : result
    {
        public string customer2emailaddress_gid { get; set; }
        public string customer_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public List<customeremailaddress_list> customeremailaddress_list { get; set; }
    }

    public class constitution_list
    {
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
    }
    public class assessmentagency_list
    {
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
    }
    public class assessmentagencyrating_list
    {
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
    }
    public class amlcategory_list
    {
        public string amlcategory_gid { get; set; }
        public string amlcategory_name { get; set; }
    }
    public class businesscategory_list
    {
        public string businesscategory_gid { get; set; }
        public string businesscategory_name { get; set; }
    }
    public class designation_list
    {
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
    }
    public class individualproof_list
    {
        public string individualproof_gid { get; set; }
        public string individualproof_name { get; internal set; }
        public string individualproof_type { get; set; }
    }

    

    public class state_list
    {
        public string state_gid { get; set; }
        public string state_name { get; set; }
    }


    public class MdlcustomerBank : result
    {
        public string customer2bank_gid { get; set; }
        public string customer_gid { get; set; }
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
        public List<customerbank_list> customerbank_list { get; set; }
    }
    public class Mdlmobile_no : result
    {
        public string dob { get; set; }
        public string age { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string customer_gid { get; set; }
        public string customer2mobileno_gid { get; set; }

        public List<mobileno_list> mobileno_list { get; set; }
    }
    public class mobileno_list
    {
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string customer2mobileno_gid { get; set; }
        public string whatsapp_mobileno { get; set; }

    }
    public class MdlEmail_address : result
    {
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string customer2emailaddress_gid { get; set; }
        public string customer_gid { get; set; }
        public List<email_list> email_list { get; set; }
    }
    public class email_list
    {
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string customer2emailaddress_gid { get; set; }
        public string customer_gid { get; set; }
    }
    public class MdlcustomerGST : result
    {
        public string customer2gst_gid { get; set; }
        public string customer_gid { get; set; }
        public string gststate_name { get; set; }
        public string gst_no { get; set; }
        public string gstregister_status { get; set; }
        public customerGSTDetails[] GSTArray { get; set; }
        public List<customergst_list> customergst_list { get; set; }
    }
    public class customerapprover_list
    {
        public string customer_gid { get; set; }
        public string created_by { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string coi_date { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string cin_no { get; set; }
        public string remarks { get; set; }
        public string pan_no { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string status { get; set; }
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
    public class customerrejected_list
    {
        public string customer_gid { get; set; }
        public string created_by { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string coi_date { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string cin_no { get; set; }
        public string remarks { get; set; }
        public string pan_no { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string status { get; set; }
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
        public class customer_list
    {
        public string customer_gid { get; set; }
        public string created_by { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string coi_date { get; set; }
        public string year_business { get; set; }
        public string month_business { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string cin_no { get; set; }
        public string remarks { get; set; }
        public string pan_no { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string status { get; set; }       
        public string gst_no { get; set; }
        public string gst_state { get; set; }
        public string contactperson_fn { get; set; }
        public string contactperson_mn { get; set; }
        public string contactperson_ln { get; set; }
        public string created_date { get; set; }
        public string creditActive_status { get; set; }
        public string credit_status { get; set; }
        public string status_remarks { get; set; }        
        public string creditstatus_Approval { get; set; }
    }

    public class customeraddress_list
    {
        public string customer2address_gid { get; set; }
        public string customer_gid { get; set; }
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

    public class customermobileno_list
    {
        public string customer2mobileno_gid { get; set; }
        public string customer_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
    }

    public class customeremailaddress_list
    {
        public string customer2emailaddress_gid { get; set; }
        public string customer_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
    }

    public class MdlCheque : result
    {
        public string fndmanagement2cheque_gid { get; set; }
        public string fndmanagement_gid { get; set; }
        public string accountholder_name { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string cheque_no { get; set; }
        public string ifsc_code { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
      
        

        public List<cheque_list> cheque_list { get; set; }
    }
    public class MdlChequeDocument : result
    {
        public string cheque2document_gid { get; set; }
        public string udcmanagement2cheque_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<chequedocument_list> chequedocument_list { get; set; }
    }
    public class chequedocument_list
    {
        public string cheque2document_gid { get; set; }
        public string fndmanagement2cheque_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }

    }
    public class cheque_list
    {
        public string fndmanagement2cheque_gid { get; set; }
        public string fndmanagement_gid { get; set; }
        public string stakeholder_gid { get; set; }
        public string stakeholder_name { get; set; }
        public string stakeholder_type { get; set; }
        public string designation { get; set; }
        public string accountholder_name { get; set; }
        public string account_number { get; set; }
        public string bank_name { get; set; }
        public string cheque_no { get; set; }
        public string ifsc_code { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string mergedbankingentity_gid { get; set; }
        public string mergedbankingentity_name { get; set; }
        public string special_condition { get; set; }
        public string general_remarks { get; set; }
        public string cts_enabled { get; set; }
        public string cheque_type { get; set; }
        public string date_chequetype { get; set; }
        public string date_chequepresentation { get; set; }
        public string status_chequepresentation { get; set; }
        public string date_chequeclearance { get; set; }
        public string status_chequeclearance { get; set; }
        public string customer_gid { get; set; }
        
    }



    public class customerbank_list
    {
        public string customer2bank_gid { get; set; }
        public string customer_gid { get; set; }
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

    public class customergst_list
    {
        public string customer2gst_gid { get; set; }
        public string customer_gid { get; set; }
        public string gststate_name { get; set; }
        public string gstregister_status { get; set; }
        public string gst_no { get; set; }
    }

    public class customerGSTDetails
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