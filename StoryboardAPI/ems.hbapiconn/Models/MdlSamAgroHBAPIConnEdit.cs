using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{
    //Update Models

    public class MdlBuyerGeneralUpdateHBAPI
    {
        public string vertical_name { get; set; }
        public string constitution_name { get; set; }
        public string company_name { get; set; }
    }

    public class MdlInstitutionAddressUpdateHBAPI
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

    public class MdlInstitutionAddressUpdateAddHBAPI
    {
        public AddressDetails addressDetails { get; set; }
    }

    public class MdlBuyerInstitutionContactUpdateAddHBAPI
    {
        public ContactPersonDetailsCompany contactPersonDetailsCompany { get; set; }
    }

    public class MdlInstitutionContactUpdateHBAPI
    {
        public string contactdetails_gid { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
    }

    public class MdlBuyerGeneralContactUpdateAddHBAPI
    {
        public ContactPersonDetailsGeneral contactPersonDetailsGeneral { get; set; }
    }

    public class MdlGeneralContactUpdateHBAPI
    {
        public string contactdetails_gid { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
    }

    public class MdlBuyerInstitutionUpdateHBAPI
    {
        public string officialemail_address { get; set; }
        public string official_telephoneno { get; set; }
        //public string company_name { get; set; }
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
        public string primary_gst { get; set; }
    }

    public static class UpdateContactHBAPIFrom
    {
        public const string
            Email = "Email",
            MobileNo = "MobileNo";

    }

    public static class LoggingTypeHBAPIUpdate
    {
        public const string
            Buyer = "Buyer",
            Supplier = "Supplier";
    }

    public static class UpdateAPIMetaList
    {
        public const string
            UpdateBuyerGeneralHBAPI = "UpdateBuyerGeneralHBAPI",
            UpdateBuyerGeneralContactHBAPI = "UpdateBuyerGeneralContactHBAPI",
            UpdateBuyerGeneralContactAddHBAPI = "UpdateBuyerGeneralContactAddHBAPI",
            UpdateBuyerInstitutionHBAPI = "UpdateBuyerInstitutionHBAPI",
            UpdateBuyerInstitutionAddressHBAPI = "UpdateBuyerInstitutionAddressHBAPI",
            UpdateBuyerInstitutionAddressAddHBAPI = "UpdateBuyerInstitutionAddressAddHBAPI",
            UpdateBuyerInstitutionContactHBAPI = "UpdateBuyerInstitutionContactHBAPI",
            UpdateBuyerInstitutionContactAddHBAPI = "UpdateBuyerInstitutionContactAddHBAPI",
            UpdateBuyerIndividualHBAPI = "UpdateBuyerIndividualHBAPI",
            UpdateBuyerIndividualAddressHBAPI = "UpdateBuyerIndividualAddressHBAPI",
            UpdateBuyerIndividualAddressAddHBAPI = "UpdateBuyerIndividualAddressAddHBAPI",
            UpdateBuyerIndividualContactHBAPI = "UpdateBuyerIndividualContactHBAPI",
            UpdateBuyerIndividualContactAddHBAPI = "UpdateBuyerIndividualContactAddHBAPI",
            UpdateSupplierGeneralHBAPI = "UpdateSupplierGeneralHBAPI",
            UpdateSupplierInstitutionHBAPI = "UpdateSupplierInstitutionHBAPI",
            UpdateSupplierInstitutionContactAddHBAPI = "UpdateSupplierInstitutionContactAddHBAPI",
            UpdateSupplierInstitutionAddressAddHBAPI = "UpdateSupplierInstitutionAddressAddHBAPI",
            UpdateSupplierInstitutionContactHBAPI = "UpdateSupplierInstitutionContactHBAPI",
            UpdateSupplierGeneralContactHBAPI = "UpdateSupplierGeneralContactHBAPI",
            UpdateSupplierGeneralContactAddHBAPI = "UpdateSupplierGeneralContactAddHBAPI",
            UpdateSupplierIndividualHBAPI = "UpdateSupplierIndividualHBAPI",
            UpdateSupplierIndividualAddressHBAPI = "UpdateSupplierIndividualAddressHBAPI",
            UpdateSupplierIndividualAddressAddHBAPI = "UpdateSupplierIndividualAddressAddHBAPI",
            UpdateSupplierIndividualContactHBAPI = "UpdateSupplierIndividualContactHBAPI",
            UpdateSupplierIndividualContactAddHBAPI = "UpdateSupplierIndividualContactAddHBAPI",
            UpdateBuyerTagStatusHBAPI = "UpdateBuyerTagStatusHBAPI",
            UpdateSupplierGeneralContactBasicHBAPI = "UpdateSupplierGeneralContactBasicHBAPI",
            UpdateSupplierInstitutionContactBasicHBAPI = "UpdateSupplierInstitutionContactBasicHBAPI",
            UpdateSupplierIndividualContactBasicHBAPI = "UpdateSupplierIndividualContactBasicHBAPI",
            UpdateSupplierInstitutionAddressHBAPI = "UpdateSupplierInstitutionAddressHBAPI",
            UpdateBuyerGeneralContactBasicHBAPI = "UpdateBuyerGeneralContactBasicHBAPI",
            UpdateBuyerInstitutionContactBasicHBAPI = "UpdateBuyerInstitutionContactBasicHBAPI",
            UpdateBuyerIndividualContactBasicHBAPI = "UpdateBuyerIndividualContactBasicHBAPI",
            UpdateBuyerRMHBAPI = "UpdateBuyerRMHBAPI",
            UpdateContractInstitutionAddressHBAPI = "UpdateContractInstitutionAddressHBAPI";
    }

    //Individual Models

    public class MdlBuyerUpdateIndividualHBAPI
    {
        public MdlBuyerUpdateIndividualHBAPI()
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

        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string aadhar_no { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }

        public string virtualaccount_number { get; set; }
        public string virtualcustomerbank_name { get; set; }
        public string virtualbranch_name { get; set; }
        public string virtualifsc_code { get; set; }
    }

    public class HBAPIIndividualCustomerUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_individual { get; set; }
    }

    public class MdlIndividualContactUpdateHBAPI
    {
        public string contactdetails_gid { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_middlename { get; set; }
        public string contactperson_lastname { get; set; }
        public string designation { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
    }

    //Supplier Individual

    public class HBAPISupplierUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_company { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
    }

    public class MdlSupplierUpdateIndividualHBAPI
    {
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string aadhar_no { get; set; }
    }

    public class MdlIndividualAddressUpdateHBAPI
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

    public class MdlIndividualAddressUpdateAddHBAPI
    {
        public AddressDetails addressDetails { get; set; }
    }

    public class HBAPIIndividualSupplierUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_individual { get; set; }
    }

    public class MdlSupplierIndividualContactUpdateAddHBAPI
    {
        public ContactPersonDetailsCompany contactPersonDetailsCompany { get; set; }
    }

    public class MdlBuyerIndividualContactUpdateAddHBAPI
    {
        public ContactPersonDetailsIndividual contactPersonDetailsIndividual { get; set; }
    }

    public class MdlBuyerTagStatusUpdateHBAPI
    {
        public string lgltag_status { get; set; }
    }

    public class MdlSupplierGeneralContactUpdateBasicHBAPI
    {
        public ContactPersonDetailsGeneral contactPersonDetailsGeneral { get; set; }
    }
    public class MdlBuyerGeneralContactUpdateBasicHBAPI
    {
        public ContactPersonDetailsGeneral contactPersonDetailsGeneral { get; set; }
    }

    public class MdlBuyerIndividualContactUpdateBasicHBAPI
    {
        public ContactPersonDetailsIndividual contactPersonDetailsIndividual { get; set; }
    }


    public class MdlBuyerInstitutionContactUpdateBasicHBAPI
    {
        public ContactPersonDetailsCompany contactPersonDetailsCompany { get; set; }
    }

    //public class MdlBuyerIndividualContactUpdateBasicHBAPI
    //{
    //    public ContactPersonDetailsIndividual contactPersonDetailsIndividual { get; set; }
    //}

    

    public static class HBUpdateAPINameRepo
    {
        public const string
            NetSuiteAPIUpdateCustomerStatus = "NetSuiteAPIUpdateCustomerStatus", //Update Status
            NetSuiteAPIUpdateCustomerSalesRep = "NetSuiteAPIUpdateCustomerSalesRep", //Update Sales Rep
            NetSuiteAPIUpdateCustomerGeneral = "NetSuiteAPIUpdateCustomerGeneral",//Update APIs - Customer
            NetSuiteAPIUpdateCustomerGenContact = "NetSuiteAPIUpdateCustomerGenContact",
            NetSuiteAPIUpdateCustomerGenContactAdd = "NetSuiteAPIUpdateCustomerGenContactAdd",
            NetSuiteAPIUpdateCustomerGenContactBasic = "NetSuiteAPIUpdateCustomerGenContactBasic",

            NetSuiteAPIUpdateCustomerInstitution = "NetSuiteAPIUpdateCustomerInstitution",
            NetSuiteAPIUpdateCustomerContact = "NetSuiteAPIUpdateCustomerContact",
            NetSuiteAPIUpdateCustomerContactAdd = "NetSuiteAPIUpdateCustomerContactAdd",
            NetSuiteAPIUpdateCustomerAddress = "NetSuiteAPIUpdateCustomerAddress",
            NetSuiteAPIUpdateCustomerAddressAdd = "NetSuiteAPIUpdateCustomerAddressAdd",
            NetSuiteAPIUpdateCustomerContactBasic = "NetSuiteAPIUpdateCustomerContactBasic",
            NetSuiteAPIUpdateCustomerIndividual = "NetSuiteAPIUpdateCustomerIndividual",
            NetSuiteAPIUpdateIndividualCustomerContactAdd = "NetSuiteAPIUpdateIndividualCustomerContactAdd",
            NetSuiteAPIUpdateIndividualCustomerContactBasic = "NetSuiteAPIUpdateIndividualCustomerContactBasic",

            NetSuiteAPIUpdateSupplierGeneral = "NetSuiteAPIUpdateSupplierGeneral", //Supplier
            NetSuiteAPIUpdateSupplierGenContact = "NetSuiteAPIUpdateSupplierGenContact",
            NetSuiteAPIUpdateSupplierGenContactadd = "NetSuiteAPIUpdateSupplierGenContactadd",

            NetSuiteAPIUpdateSupplierInstitution = "NetSuiteAPIUpdateSupplierInstitution",
            NetSuiteAPIUpdateSupplierContact = "NetSuiteAPIUpdateSupplierContact",
            NetSuiteAPIUpdateSupplierContactAdd = "NetSuiteAPIUpdateSupplierContactAdd",
            NetSuiteAPIUpdateSupplierAddress = "NetSuiteAPIUpdateSupplierAddress",
            NetSuiteAPIUpdateSupplierAddressAdd = "NetSuiteAPIUpdateSupplierAddressAdd",
            NetSuiteAPIUpdateSupplierIndividual = "NetSuiteAPIUpdateSupplierIndividual",
            NetSuiteAPIUpdateIndividualSupplierContactAdd = "NetSuiteAPIUpdateIndividualSupplierContactAdd",
            NetSuiteAPIUpdateSupplierGenContactBasic = "NetSuiteAPIUpdateSupplierGenContactBasic",
            NetSuiteAPIUpdateSupplierContactBasic = "NetSuiteAPIUpdateSupplierContactBasic",
            NetSuiteAPIUpdateIndividualSupplierContactBasic = "NetSuiteAPIUpdateIndividualSupplierContactBasic";

        //Contract
        public const string
            UpdateContractInstitution = "UpdateContractInstitution";



    }

    public class MdlBuyerRMUpdateHBAPI
    {
        public string rm_erpid { get; set; }
        public string application_no { get; set; }
        public string rm_custopediaid { get; set; }

    }
   

}