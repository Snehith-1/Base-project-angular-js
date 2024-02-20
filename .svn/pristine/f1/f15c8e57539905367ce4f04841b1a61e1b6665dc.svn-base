using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{
    public class MdlSamAgroHAPIOtherCreditor
    {
    }
    //Other Creditor
    public class HBAPIOtherCreditorResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
        public string othercreditor_erpid { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_company { get; set; }
        public Dictionary<string, string> addresslist { get; set; }

    }

    //Other Creditor Update
    public class HBAPIOtherCreditorUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_company { get; set; }
        public Dictionary<string, string> contactlist_individual { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
    }

    //Other Creditor Response
    public class MdlHAPIOtherCreditorResponse
    {
        public string ExternalID { get; set; } //creditorref_no
        //#customForm
        public string Applicant_type { get; set; } //category - refName
        public string Applicant_category { get; set; }  //isPerson
        public string contactperson_name { get; set; } //=>instead of Contact Person Details      
        public string email_id { get; set; } //email
        public string contact_no { get; set; } //phone
        //#subsidiary 
        public string aadhar_no { get; set; } //custentity_aadhar_number
        //#entitystatus
        public string Applicant_name { get; set; } //companyname
        public string pan_no { get; set; } //custentity_permanent_account_number
        //#custentity_in_gst_vendor_regist_type - refName

        public GSTDetails gstDetails { get; set; }

        public AddressDetails addressDetails { get; set; }

        public string account_number { get; set; } //custentity_acc_num

        public string accountholder_name { get; set; } //custentity_sam_account_name

        public string ifsc_code { get; set; } //custentity_sam_ifsc_code

        public string branch_address { get; set; } //custentity_sam_cust_bank_address

        public string bank_name { get; set; } //custentity_supplier_bank_name
        public string branch_name { get; set; } //custentity_supplier_bank_name
        public string micr { get; set; } //custentity_sam_cust_micr_code
        public string agreementinvolvement_type { get; set; }
        public string creditor2agreement_no { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string company_type { get; set; }

        public string primary_gst { get; set; }
    }

    //Other Creditor Response Update
    public class MdlHAPIOtherCreditorResponseUpdate
    {
        public string ExternalID { get; set; } //creditorref_no
        //#customForm
        public string Applicant_type { get; set; } //category - refName
        public string Applicant_category { get; set; }  //isPerson
        public string contactperson_name { get; set; } //=>instead of Contact Person Details      
        public string email_id { get; set; } //email
        public string contact_no { get; set; } //phone
        //#subsidiary 
        public string aadhar_no { get; set; } //custentity_aadhar_number
        //#entitystatus
        public string Applicant_name { get; set; } //companyname
        public string pan_no { get; set; } //custentity_permanent_account_number
        //#custentity_in_gst_vendor_regist_type - refName

        public GSTDetails gstDetails { get; set; }

        public AddressDetails addressDetails { get; set; }

        public string account_number { get; set; } //custentity_acc_num

        public string accountholder_name { get; set; } //custentity_sam_account_name

        public string ifsc_code { get; set; } //custentity_sam_ifsc_code

        public string branch_address { get; set; } //custentity_sam_cust_bank_address
        public string branch_name { get; set; }
        public string bank_name { get; set; } //custentity_supplier_bank_name

        public string micr { get; set; } //custentity_sam_cust_micr_code
        public string agreementinvolvement_type { get; set; }
        public string creditor2agreement_no { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string primary_gst { get; set; }
    }

    public static class HBUpdateAPINameRepo_OtherCreditor
    {
        public const string
            UpdateOtherCreditorGeneral = "UpdateOtherCreditorGeneral",
            UpdateOtherCreditorAddress = "UpdateOtherCreditorAddress",
            UpdateOtherCreditorAddressAdd = "UpdateOtherCreditorAddressAdd";
            
    }

    public static class HBPostAPINameRepo_OtherCreditor
    {
        public const string
            PostOtherCreditor = "PostOtherCreditor";
    }

    public static class LoggingTypeHBAPIUpdate_OtherCreditor
    {
        public const string
            OtherCreditor = "OtherCreditor";
    }

    public static class UpdateAPIMetaList_OtherCreditor
    {
        public const string
        UpdateOtherCreditorAddressAddHBAPI = "UpdateOtherCreditorAddressAddHBAPI",
        UpdateOtherCreditorAddressHBAPI = "UpdateOtherCreditorAddressHBAPI",
        UpdateOtherCreditorHBAPI = "UpdateOtherCreditorHBAPI";
    }

    //Other Creditor Address Update
    public class MdlOtherCreditorAddressUpdateHBAPI
    {
        public string addresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postal_code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string externalid { get; set; }
    }
    public class MdlOtherCreditorAddressUpdateAddHBAPI
    {
        public AddressDetails addressDetails { get; set; }
    }

}