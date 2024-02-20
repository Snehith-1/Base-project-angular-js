using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{

    public class AccessTokenResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
    }


    //Actual Models

    public class MdlBuyerHBAPI
    {
        public MdlBuyerHBAPI()
        {
            GSTDetails taxregistration = new GSTDetails();
            AddressDetails addressbook = new AddressDetails();
        }

        public string ExternalID { get; set; }

        public string vertical_name { get; set; }
        public string constitution_name { get; set; }
        public string officialemail_address { get; set; }
        public string official_telephoneno { get; set; }
        public int subsidiary { get; set; }
        public int entitystatus { get; set; }

        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string companytype_name { get; set; }
        public string tan_number { get; set; }
        public string incometax_returnsstatus { get; set; }
        public string lastyear_turnover { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string lgltag_status { get; set; }




        public GSTDetails gstDetails { get; set; }

        public AddressDetails addressDetails { get; set; }

        public ContactPersonDetailsGeneral contactPersonDetailsGeneral { get; set; }

        public ContactPersonDetailsCompany contactPersonDetailsCompany { get; set; }

        public ContactPersonDetailsIndividual contactPersonDetailsIndividual { get; set; }


        public string virtualaccount_number { get; set; }
        public string virtualcustomerbank_name { get; set; }
        public string virtualbranch_name { get; set; }
        public string virtualifsc_code { get; set; }

        public string bankaccount_number { get; set; }
        public string accountholder_name { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string micr_code { get; set; }
        public string bank_address { get; set; }
        public string branch_name { get; set; }

        public string fssai_licenseno { get; set; }
        public string apmc_licenseno  { get; set; }
        public string iec_licenseno { get; set; }
        public string rm_erpid { get; set; }
        public string primary_gst { get; set; }

    }

    public class GSTDetails
    {
        public GSTDetails()
        {
            GSTData[] gstlist = new GSTData[] { };
        }
        public GSTData[] gstlist { get; set; }
    }

    public class GSTData
    {
        public string gst_no { get; set; }
        public string gst_state { get; set; }
        public string externalid { get; set; }
    }

    public class AddressDetails
    {
        public AddressDetails()
        {
            AddressData[] addresslist = new AddressData[] { };
        }
        public AddressData[] addresslist { get; set; }
    }



    public class AddressData
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

    public class HBAPICustomerResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
        public string customererp_id { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_company { get; set; }
        public Dictionary<string, string> addresslist { get; set; }

    }

    public class ContactPersonDetailsGeneral
    {

        public ContactPersonData[] contactpersonlist { get; set; }
    }

    public class ContactPersonDetailsCompany
    {
        public ContactPersonData[] contactpersonlist { get; set; }
    }

    public class ContactPersonDetailsIndividual
    {
        public ContactPersonData[] contactpersonlist { get; set; }
    }

    public class ContactPersonData
    {
        public string contactdetails_gid { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
    }




    public class MdlEmployeeHBAPI
    {
        public string employee_externalid { get; set; }
        public string entity_name { get; set; }
        public string employee_code { get; set; }
        public string employee_firstname { get; set; }
        public string employee_lastname { get; set; }
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string baselocation { get; set; }
        public string employeereporting_to { get; set; }
        public string entity { get; set; }
        public string department { get; set; }
    }

    public class HBAPIEmployeeResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
        public string employeeerp_id { get; set; }
    }

    public class MdlHBAPIConnControllerResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
    }

    public static class UpdateBuyerHBAPIFrom
    {
        public const string
            GeneralDetails = "GeneralDetails",
            Company = "Company",
            Individual = "Individual",
            ApplicantBank = "ApplicantBank";
    }

    public class UpdateHBAPIBuyerRequest
    {
        public string institution_gid { get; set; }
        public string vertical_name { get; set; }
        public string constitution_name { get; set; }

        public string official_mailid { get; set; }
        public string official_telephoneno { get; set; }
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string companytype_name { get; set; }
        public string tan_number { get; set; }
        public string incometax_returnsstatus { get; set; }
        public string lastyear_turnover { get; set; }

        public string bankaccount_number { get; set; }
        public string accountholder_name { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string micr_code { get; set; }
        public string bank_address { get; set; }
        public string branch_name { get; set; }


        public string erp_id { get; set; }
    }


    public class HBAPICustomerUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_company { get; set; }
        public Dictionary<string, string> contactlist_individual { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
    }

    public class UpdateHBAPIEmployeeRequest
    {
        public string employee_gid { get; set; }
        public string entity_name { get; set; }
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string user_firstname { get; set; }
        public string user_lastname { get; set; }
        public string user_code { get; set; }
        public string erp_id { get; set; }
        public string employeereporting_to { get; set; }
        public string baselocation { get; set; }
        public string department_gid { get; set; }

    }

    public class HBAPIEmployeeUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
    }

    public class ContactDetails
    {
        public string primary_status { get; set; }
        public string mobileno { get; set; }
        public string email { get; set; }
        public string mobileno_gid { get; set; }
        public string email_gid { get; set; }
    }

    public class MdlIndividualBuyerHBAPI
    {
        public MdlIndividualBuyerHBAPI()
        {

            AddressDetails addressbook = new AddressDetails();
        }

        public string ExternalID { get; set; }

        public string vertical_name { get; set; }
        public string constitution_name { get; set; }
        public string officialemail_address { get; set; }
        public string official_telephoneno { get; set; }
        public int subsidiary { get; set; }
        public int entitystatus { get; set; }

        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string aadhar_no { get; set; }


        public AddressDetails addressDetails { get; set; }

        public ContactPersonDetailsGeneral contactPersonDetailsGeneral { get; set; }

        public ContactPersonDetailsCompany contactPersonDetailsCompany { get; set; }

        public ContactPersonDetailsIndividual contactPersonDetailsIndividual { get; set; }


        public string virtualaccount_number { get; set; }
        public string virtualcustomerbank_name { get; set; }
        public string virtualbranch_name { get; set; }
        public string virtualifsc_code { get; set; }

        public string bankaccount_number { get; set; }
        public string accountholder_name { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string micr_code { get; set; }
        public string bank_address { get; set; }
        public string branch_name { get; set; }

    }

    public class MdlHBAPIConnDAResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
    }

    public static class HBPostAPINameRepo
    {
        public const string
            NetSuiteAPIDTrnPostCustomer = "NetSuiteAPIDTrnPostCustomer", //Create APIs
            NetSuiteAPIDTrnPostIndividualCustomer = "NetSuiteAPIDTrnPostIndividualCustomer",
            NetSuiteAPIDTrnPostSupplier = "NetSuiteAPIDTrnPostSupplier",
            NetSuiteAPIDTrnPostIndividualSupplier = "NetSuiteAPIDTrnPostIndividualSupplier",
            NetSuiteAPIDTrnPostEmployee = "NetSuiteAPIDTrnPostEmployee",

            NetSuiteAPIDTrnUpdateEmployee = "NetSuiteAPIDTrnUpdateEmployee", //Update Employee
            NSLimitAPI = "NSLimitAPI";
    }

}