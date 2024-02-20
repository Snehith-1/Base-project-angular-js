using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace vcidex_kyc.Models
{

    /// <summary>
    /// This Models will provide values from UI and third party API for verifying the KYC details of the customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>


    public class MdlAgrKyc
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class Common
    {
        public string consent { get; set; }
        public string dob { get; set; }
    }

    public class PanNumberModel : Common
    {
        public string pan { get; set; }
    }

    public class PanVerificationModel : PanNumberModel
    {
        public string name { get; set; }
    }


    public class DrivingLicenseModel : Common
    {
        public string dlno { get; set; }
    }
    public class VoterIDModel : Common
    {
        public string epic_no { get; set; }
    }

    public class PassportModel : Common
    {
        public string pname { get; set; }
        public string fileNo { get; set; }
        public string passportNo { get; set; }
        public string doi { get; set; }
        public string dob { get; set; }

    }

    public class GSTVerificationModel : Common
    {
        public string gstin { get; set; }
        public string tan { get; set; }
        public string institution2branch_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class GSTAuthenticationModel : GSTVerificationModel
    {
        public bool additionalData { get; set; }
    }

    public class CompanyLLP : Common
    {
        public string company_name { get; set; }
        //  public string cin_no { get; set; }
    }

    public class MCASignatories : Common
    {
        public string function_gid { get; set; }
        public string cin { get; set; }
        public string statuscode { get; set; }
        public string application_gid { get; set; }
    }

    public class uam : Common
    {
        public string uan { get; set; }
        public string mobile { get; set; }
    }

    public class Fssai : Common
    {
        public string reg_no { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class Fda : Common
    {
        public string licence_no { get; set; }
        public string state { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class EBA : Common
    {
        public string consumer_id { get; set; }
        public string service_provider { get; set; }
    }

    public class TelephoneAuthentication : Common
    {
        public string tel_no { get; set; }
        public string city { get; set; }
    }

    public class IfscVerification : Common
    {
        public string ifsc { get; set; }
    }

    public class BankAccVerification : Common
    {
        public string ifsc { get; set; }
        public string accountNumber { get; set; }
    }

    public class PanNumberResponse : MdlAgrKyc
    {
        public Result result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
    }


    public class PanVerificationResponse : MdlAgrKyc
    {
        public Result1 result { get; set; }
        public string request_id { get; set; }
        public string statuscode { get; set; }
    }

    public class Result1
    {
        public string status { get; set; }
        public object duplicate { get; set; }
        public bool nameMatch { get; set; }
        public bool dobMatch { get; set; }
    }

    public class DrivingLicenseResponse : MdlAgrKyc
    {
        public int statusCode { get; set; }
        public string requestId { get; set; }
        public Result1 result { get; set; }
        public string error { get; set; }
    }

    public class Result2
    {
        public string status { get; set; }

        [JsonProperty("father/husband")]
        public string fatherhusband { get; set; }

        public Validity validity { get; set; }
        public Address[] address { get; set; }
        public Covdetail[] covDetails { get; set; }
        public string bloodGroup { get; set; }
        public string dlNumber { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string dob { get; set; }
        public string issueDate { get; set; }
        public Statusdetails statusDetails { get; set; }
    }

    public class Validity
    {
        public string nonTransport { get; set; }
        public string transport { get; set; }
    }

    public class Statusdetails
    {
        public string remarks { get; set; }
        public string to { get; set; }
        public string from { get; set; }
    }

    public class Address
    {
        public string district { get; set; }
        public int pin { get; set; }
        public string completeAddress { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string addressLine1 { get; set; }
        public string type { get; set; }
    }

    public class Covdetail
    {
        public string issueDate { get; set; }
        public string cov { get; set; }
    }


    public class VoterIDResponse : MdlAgrKyc
    {
        public Result3 result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result3
    {
        public string name { get; set; }
        public string rln_name { get; set; }
        public string rln_type { get; set; }
        public string gender { get; set; }
        public string district { get; set; }
        public string ac_name { get; set; }
        public string pc_name { get; set; }
        public string state { get; set; }
        public string epic_no { get; set; }
        public string dob { get; set; }
        public int age { get; set; }
        public string part_no { get; set; }
        public string slno_inpart { get; set; }
        public string ps_name { get; set; }
        public string part_name { get; set; }
        public string last_update { get; set; }
        public string ps_lat_long { get; set; }
        public string rln_name_v1 { get; set; }
        public string rln_name_v2 { get; set; }
        public string rln_name_v3 { get; set; }
        public string section_no { get; set; }
        public string id { get; set; }
        public string name_v1 { get; set; }
        public string name_v2 { get; set; }
        public string name_v3 { get; set; }
        public string ac_no { get; set; }
        public string st_code { get; set; }
        public string house_no { get; set; }
    }


    public class PassportDetailsResponse : MdlAgrKyc
    {
        public Result4 result { get; set; }
        public string request_id { get; set; }
        public string statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result4
    {
        public Name name { get; set; }
        public Passportnumber passportNumber { get; set; }
        public Dateofissue dateOfIssue { get; set; }
        public string typeOfApplication { get; set; }
        public string applicationDate { get; set; }
    }

    public class Name
    {
        public string nameFromPassport { get; set; }
        public string surnameFromPassport { get; set; }
        public Nullable<bool> nameMatch { get; set; }
        public Nullable<float> nameScore { get; set; }
    }

    public class Passportnumber
    {
        public string passportNumberFromSource { get; set; }
        public Nullable<bool> passportNumberMatch { get; set; }
    }

    public class Dateofissue
    {
        public string dispatchedOnFromSource { get; set; }
        public Nullable<bool> dateOfIssueMatch { get; set; }
    }


    public class GSTSBPANDetailsResponse : MdlAgrKyc
    {
        public string requestId { get; set; }
        public Result5[] result { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result5
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

    public class GSTVerificationResponse : MdlAgrKyc
    {
        public string requestId { get; set; }
        public Result6 result { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result6
    {
        public string stjCd { get; set; }
        public string dty { get; set; }
        public string lgnm { get; set; }
        public string stj { get; set; }
        public Adadr[] adadr { get; set; }
        public string cxdt { get; set; }
        public string gstin { get; set; }
        public string[] nba { get; set; }
        public string lstupdt { get; set; }
        public string ctb { get; set; }
        public string rgdt { get; set; }
        public Pradr pradr { get; set; }
        public string ctjCd { get; set; }
        public string sts { get; set; }
        public string tradeNam { get; set; }
        public string ctj { get; set; }
        public object[] mbr { get; set; }
        public string canFlag { get; set; }
        public string cmpRt { get; set; }
        public Contacted contacted { get; set; }
        public string ppr { get; set; }
    }

    public class Pradr
    {
        public string addr { get; set; }
        public string ntr { get; set; }
        public string adr { get; set; }
        public string em { get; set; }
        public string lastUpdatedDate { get; set; }
        public string mb { get; set; }
    }

    public class Contacted
    {
        public object email { get; set; }
        public object mobNum { get; set; }
        public object name { get; set; }
    }

    public class Adadr
    {
        public string addr { get; set; }
        public string ntr { get; set; }
        public string adr { get; set; }
        public string em { get; set; }
        public string lastUpdatedDate { get; set; }
        public string mb { get; set; }
    }


    public class GSPGSTReturnFilingResponse : MdlAgrKyc
    {
        public string requestId { get; set; }
        public Result7 result { get; set; }
        [JsonProperty("status-code")]
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result7
    {
        public string gstin { get; set; }
        public Compliance_Status compliance_status { get; set; }
        public Result8[] result { get; set; }
    }

    public class Compliance_Status
    {
        public bool is_any_delay { get; set; }
        public bool is_defaulter { get; set; }
    }

    public class Result8
    {
        public Efiledlist[] EFiledlist { get; set; }
        public string financial_year { get; set; }
        public FilingFrequency[] filing_frequency { get; set; }
    }

    public class FilingFrequency
    {
        public string startPeriod { get; set; }
        public string endPeriod { get; set; }
        public string frequency { get; set; }
        public string quarter { get; set; }
    }

    public class Efiledlist
    {
        public string valid { get; set; }
        public string mof { get; set; }
        public string dof { get; set; }
        public string rtntype { get; set; }
        public string ret_prd { get; set; }
        public string arn { get; set; }
        public string status { get; set; }
        public Nullable<bool> is_delay { get; set; }
    }


    public class GSTAuthenticationResponse : MdlAgrKyc
    {
        public string requestId { get; set; }
        public Result9 result { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class Result9
    {
        public object canFlag { get; set; }
        public Contacted1 contacted { get; set; }
        public object ppr { get; set; }
        public string cmpRt { get; set; }
        public string rgdt { get; set; }
        public string tradeNam { get; set; }
        public string[] nba { get; set; }
        public string[] mbr { get; set; }
        public Adadr1[] adadr { get; set; }
        public Pradr1 pradr { get; set; }
        public object stjCd { get; set; }
        public object lstupdt { get; set; }
        public string gstin { get; set; }
        public object ctjCd { get; set; }
        public string stj { get; set; }
        public string dty { get; set; }
        public object cxdt { get; set; }
        public string ctb { get; set; }
        public string sts { get; set; }
        public string lgnm { get; set; }
        public string ctj { get; set; }
    }

    public class Contacted1
    {
        public object email { get; set; }
        public object mobNum { get; set; }
        public object name { get; set; }
    }

    public class Pradr1
    {
        public string adr { get; set; }
        public string em { get; set; }
        public string mb { get; set; }
        public string ntr { get; set; }
        public string addr { get; set; }
        public string lastUpdatedDate { get; set; }
    }

    public class Adadr1
    {
        public string adr { get; set; }
        public string em { get; set; }
        public string mb { get; set; }
        public string ntr { get; set; }
        public string addr { get; set; }
        public string lastUpdatedDate { get; set; }
    }


    public class CompanyLLPResponse : MdlAgrKyc
    {
        public Result10[] result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string companyID { get; set; }
        public string companyName { get; set; }
    }

    public class Result10
    {
        public string companyID { get; set; }
        public string companyName { get; set; }

    }


    public class McaSignatoriesResponse : MdlAgrKyc
    {
        public Result11[] result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string error { get; set; }
    }

    public class Result11
    {
        public string date_of_appointment { get; set; }
        public string designation { get; set; }
        public string dsc_expiry_date { get; set; }
        public string wheather_dsc_registered { get; set; }
        [JsonProperty("DIN/DPIN/PAN")]
        public string DINDPINPAN { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string statuscode { get; set; }
    }


    public class UamResponse : MdlAgrKyc
    {
        public Result12 result { get; set; }
        public string request_id { get; set; }
        public string statuscode { get; set; }
    }

    public class Result12
    {
        public string pin { get; set; }
        public string DateOFCommencement { get; set; }
        public string aadhar { get; set; }
        public string district { get; set; }
        public string DistrictIndustryCentre { get; set; }
        public string NameofEnterPrise { get; set; }
        public string NumberofEmp { get; set; }
        public string state { get; set; }
        public string OwnerName { get; set; }
        public string MajorActivity { get; set; }
        public string email { get; set; }
        public string pan { get; set; }
        public string appliedDate { get; set; }
        public string ifsc { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string social_category { get; set; }
        public string AccountNumber { get; set; }
        public string type_of_org { get; set; }
        public string gender { get; set; }
        public string EntType { get; set; }
        public string NIC_Digit_Code { get; set; }
        public string Investment { get; set; }
        public string addedOn { get; set; }
        public string modifiedDate { get; set; }
    }


    public class FSSAIResponse : MdlAgrKyc
    {
        public Result13 result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string error { get; set; }
    }

    public class Result13
    {
        public string Status { get; set; }
        public string LicType { get; set; }
        public string LicNO { get; set; }
        public string FirmName { get; set; }
        public string Address { get; set; }
    }


    public class FDAResponse : MdlAgrKyc
    {
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string request_id { get; set; }
        public Result14 result { get; set; }
        public string error { get; set; }
    }

    public class Result14
    {
        public string store_name { get; set; }
        public string contact_number { get; set; }
        public string license_detail { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }


    public class EBAResponse : MdlAgrKyc
    {
        public Result15 result { get; set; }
        public string request_id { get; set; }
        public string statuscode { get; set; }
    }

    public class Result15
    {
        public string bill_no { get; set; }
        public string bill_due_date { get; set; }
        public string consumer_number { get; set; }
        public string bill_amount { get; set; }
        public string amount_payable { get; set; }
        public string address { get; set; }
        public string bill_issue_date { get; set; }
        public string bill_date { get; set; }
        public string mobile_number { get; set; }
        public string consumer_name { get; set; }
        public string email_address { get; set; }
        public string total_amount { get; set; }
    }


    public class TelephoneAuthenticationResponse : MdlAgrKyc
    {
        public Result16 result { get; set; }
        public string request_id { get; set; }
        public string statuscode { get; set; }
    }

    public class Result16
    {
        public string name { get; set; }
        public string telephone_no { get; set; }
        public string address { get; set; }
    }


    public class BankAccVerificationResponse : MdlAgrKyc
    {
        [JsonProperty("status-code")]
        public string statusCode { get; set; }

        public string request_id { get; set; }
        public Result17 result { get; set; }
        public string error { get; set; }
    }

    public class Result17
    {
        public bool bankTxnStatus { get; set; }
        public string accountNumber { get; set; }
        public string ifsc { get; set; }
        public string accountName { get; set; }
        public string bankResponse { get; set; }
    }


    public class ITRVOCRResponse : MdlAgrKyc
    {
        public string request_id { get; set; }
        public Result18 result { get; set; }
        public string statuscode { get; set; }
    }

    public class Result18
    {
        public string AadhaarNumber { get; set; }
        public string AcknowledgementNumber { get; set; }
        public string AreaLocality { get; set; }
        public string AssessmentYear { get; set; }
        public string DateOfFiling { get; set; }
        public string FlatDoorBlockNo { get; set; }
        public string FormNo { get; set; }
        public string FormType { get; set; }
        public string Name { get; set; }
        public string NameOfPremisesBuildingVillage { get; set; }
        public string PAN { get; set; }
        public string Pin { get; set; }
        public string RoadStreetPostOffice { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public TAXDetails TAXDetails { get; set; }
        public string TownCityDistrict { get; set; }
    }

    public class TAXDetails
    {
        public string AdvanceTax { get; set; }
        public string Agriculture { get; set; }
        public string CurrentYearlossifany { get; set; }
        public string DeductionsunderChapterVIA { get; set; }
        public string ExemptIncome { get; set; }
        public string GrossTotalIncome { get; set; }
        public string InterestPayable { get; set; }
        public string NetTaxPayable { get; set; }
        public string Others { get; set; }
        public string Refund7e6 { get; set; }
        public string SelfAssessmentTax { get; set; }
        public string TCS { get; set; }
        public string TDS { get; set; }
        public string TaxPayable67e { get; set; }
        public string TaxesPaid { get; set; }
        public string TotalIncome { get; set; }
        public string TotalTaxandInterestPayable { get; set; }
        public string TotalTaxesPaid7a7b7c7d { get; set; }
    }


    public class ChequeOCRResponse : MdlAgrKyc
    {
        public int statusCode { get; set; }
        public string requestId { get; set; }
        public Result19 result { get; set; }
    }

    public class Result19
    {
        public string[] name { get; set; }
        public string ifsc { get; set; }
        public string micr { get; set; }
        public string chequeNo { get; set; }
        public Bankdetails bankDetails { get; set; }
        public string bank { get; set; }
        public string accNo { get; set; }
    }

    public class Bankdetails
    {
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string contact { get; set; }
        public string branch { get; set; }
        public string address { get; set; }
    }

    public class PanAadhaarLinkResponse : MdlAgrKyc
    {
        public Result20 result { get; set; }
        public string requestId { get; set; }
        public string statusCode { get; set; }
    }

    public class Result20
    {
        public bool isAadhaarLinked { get; set; }
    }

    public class IfscVerificationResponse : MdlAgrKyc
    {
        [JsonProperty("status-code")]
        public string statusCode { get; set; }

        public string request_id { get; set; }
        public Result21 result { get; set; }
        public string error { get; set; }
    }

    public class Result21
    {
        public string city { get; set; }
        public string office { get; set; }
        public string district { get; set; }
        public string ifsc { get; set; }
        public string micr { get; set; }
        public string state { get; set; }
        public string contact { get; set; }
        public string branch { get; set; }
        public string address { get; set; }
        public string bank { get; set; }
    }

    public class tan : Common
    {
        public string Name { get; set; }
        public string tan_no { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }
    public class CIN : Common
    {
        public string cin_no { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
        // public string tan_no { get; set; }
    }
    public class CINLLPResponse : MdlAgrKyc
    {
        public Result22 result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string cin { get; set; }
        public string Company_Name { get; set; }
        public string ROC_Code { get; set; }
        public string Registration_Number { get; set; }
        public string Company_Category { get; set; }
        public string Company_SubCategory { get; set; }
        public string Class_of_Company { get; set; }
        [JsonProperty("Authorised_Capital(Rs)")]
        public string Authorised_Capital { get; set; }
        [JsonProperty("Paid_up_Capital(Rs)")]
        public string Paid_up_Capital { get; set; }
        public string Number_of_Members { get; set; }
        public string Date_of_Incorporation { get; set; }
        public string Registered_Address { get; set; }
        public string alternative_address { get; set; }
        public string Email_Id { get; set; }
        public string Whether_Listed_or_not { get; set; }
        public string Suspended_at_stock_exchange { get; set; }
        public string Date_of_last_AGM { get; set; }
        public string Company_Status { get; set; }
        public string Date_of_Balance_Sheet { get; set; }
        public string error { get; set; }
    }

    public class Result22
    {
        public string cin { get; set; }
        public string Company_Name { get; set; }
        public string ROC_Code { get; set; }
        public string Registration_Number { get; set; }
        public string Company_Category { get; set; }
        public string Company_SubCategory { get; set; }
        public string Class_of_Company { get; set; }
        [JsonProperty("Authorised_Capital(Rs)")]
        public string Authorised_Capital { get; set; }
        [JsonProperty("Paid_up_Capital(Rs)")]
        public string Paid_up_Capital { get; set; }
        public string Number_of_Members { get; set; }
        public string Date_of_Incorporation { get; set; }
        public string Registered_Address { get; set; }
        public string alternative_address { get; set; }
        public string Email_Id { get; set; }
        public string Whether_Listed_or_not { get; set; }
        public string Suspended_at_stock_exchange { get; set; }
        public string Date_of_last_AGM { get; set; }
        public string Company_Status { get; set; }
        public string Date_of_Balance_Sheet { get; set; }
    }
    public class TANPResponse : MdlAgrKyc
    {
        public Result23 result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string name { get; set; }
        public string error { get; set; }

    }

    public class Result23
    {
        public string name { get; set; }
    }
    public class iec_detailed : Common
    {
        public string iec_no { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }
    public class iecdetailedPResponse : MdlAgrKyc
    {
        public Result24 result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string iec { get; set; }
        public string address { get; set; }
        public string iecgate_status { get; set; }
        public string pan { get; set; }
        public string error { get; set; }

    }

    public class Result24
    {
        public string ie_code { get; set; }

        public string iec_allotment_date { get; set; }
        public string file_number { get; set; }
        public string file_date { get; set; }
        public string party_name_and_address { get; set; }

        public string phone_no { get; set; }

        public string e_mail { get; set; }
        public string exporter_type { get; set; }
        public string iec_status { get; set; }
        public string date_of_establishment { get; set; }
        public string bin_pan_extension { get; set; }

        public string pan_issue_date { get; set; }
        public string pan_issued_by { get; set; }
        public string nature_of_concern { get; set; }

        public string iecgate_status { get; set; }
        public string pan { get; set; }

        public string no_of_branches { get; set; }

        public string address { get; set; }

    }

    public class ShopAndEstablishmentRequest : Common
    {
        public string regNo { get; set; }
        public string areaCode { get; set; }
        public string pdfRequired { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class ShopAndEstablishmentResponse : MdlAgrKyc
    {
        public ResultShopAndEstablishment result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class ResultShopAndEstablishment
    {
        public string category { get; set; }
        public string status { get; set; }
        public string commenceDate { get; set; }
        public string entityName { get; set; }
        public string totalWorkers { get; set; }

        [JsonProperty("father'sNameOfOccupier")]
        public string fatherNameOfOccupier { get; set; }

        public string email { get; set; }
        public string websiteUrl { get; set; }
        public string pdfLink { get; set; }
        public string contact { get; set; }
        public string ownerName { get; set; }
        public string address { get; set; }
        public string applicantName { get; set; }
        public string validFrom { get; set; }
        public string natureOfBusiness { get; set; }
        public string validTo { get; set; }
        public string registrationDate { get; set; }
    }

    public class PropertyTaxRequest : Common
    {
        public string state { get; set; }
        public string city { get; set; }
        public string propertyNo { get; set; }
        public string district { get; set; }
        public string ulb { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class PropertyTaxResponse : MdlAgrKyc
    {
        public ResultPropertyTax result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class ResultPropertyTax
    {
        public PropertyDetails propertyDetails { get; set; }
        public TaxCalculations[] taxCalculations { get; set; }
        public Penalty penalty { get; set; }
        public OwnerDetails[] ownerDetails { get; set; }
        public PaymentDetails[] paymentDetails { get; set; }
    }

    public class PropertyDetails
    {
        public string propertyId { get; set; }
        public string propertyAddress { get; set; }
        public string plotAreaInSqYrd { get; set; }
        public string plotAreaInSqMtrs { get; set; }
        public string vacantAreaInSqYrd { get; set; }
        public string constructedArea { get; set; }
        public string exemptionCategory { get; set; }
        public string multipurposeOwnership { get; set; }
        public string ownershipType { get; set; }
        public string registrationDocNo { get; set; }
        public string registrationDocDate { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }

        public FloorDetails[] floorDetails { get; set; }

    }

    public class FloorDetails
    {
        public string floor { get; set; }
        public string areaInSqft { get; set; }
        public string firmName { get; set; }
        public string occupancy { get; set; }
        public string occupantName { get; set; }
        public string constructionDate { get; set; }
        public string effectiveFromDate { get; set; }
        public string length { get; set; }
        public string breadth { get; set; }
    }

    public class TaxCalculations
    {
        public string taxType { get; set; }
        public string totalTax { get; set; }
        public string totalTaxDue { get; set; }
        public string totalamountPaidRs { get; set; }
        public string paymentStatus { get; set; }
    }

    public class Penalty
    {
        public string debitAmount { get; set; }
        public string creditAmount { get; set; }
        public string balanceTaxAmount { get; set; }
    }

    public class OwnerDetails
    {
        public string mobileNo { get; set; }
        public string ownerName { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string age { get; set; }
        public string panNo { get; set; }
        public string bankAccountNo { get; set; }
    }

    public class PaymentDetails
    {
        public string receiptNo { get; set; }
        public string dateOfPayment { get; set; }
        public string amountPaidRs { get; set; }
        public string emaipaymentModel { get; set; }
        public string valid { get; set; }
        public string serialNo { get; set; }
    }

    public class VehicleRCAuthAdvancedRequest : Common
    {
        public string registrationNumber { get; set; }
        public double version { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class VehicleRCAuthAdvancedResponse : MdlAgrKyc
    {
        public ResultVehicleRCAuthAdvanced result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class ResultVehicleRCAuthAdvanced
    {
        public string statePermitIssuedDate { get; set; }
        public string color { get; set; }
        public string bodyTypeDescription { get; set; }
        public string fatherName { get; set; }
        public string taxPaidUpto { get; set; }
        public string insuranceUpto { get; set; }
        public string pucNumber { get; set; }
        public string ownerSerialNumber { get; set; }
        public string blackListStatus { get; set; }

        public string nationalPermitIssuedBy { get; set; }
        public string makerDescription { get; set; }
        public string numberOfCylinders { get; set; }
        public string vehicleClassDescription { get; set; }
        public string chassisNumber { get; set; }
        public string manufacturedMonthYear { get; set; }
        public string statePermitNumber { get; set; }
        public string cubicCapacity { get; set; }
        public string nationalPermitNumber { get; set; }

        public string registrationNumber { get; set; }
        public string seatingCapacity { get; set; }
        public string engineNumber { get; set; }
        public string statePermitType { get; set; }
        public string insuranceCompany { get; set; }
        public string sleeperCapacity { get; set; }
        public string fitnessUpto { get; set; }
        public string presentAddress { get; set; }
        public string ownerName { get; set; }

        public string financier { get; set; }
        public string grossVehicleWeight { get; set; }
        public string unladenWeight { get; set; }
        public string insurancePolicyNumber { get; set; }
        public string nationalPermitExpiryDate { get; set; }
        public string registeredAt { get; set; }
        public string pucExpiryDate { get; set; }
        public string makerModel { get; set; }
        public string permanentAddress { get; set; }

        public string standingCapacity { get; set; }
        public string normsDescription { get; set; }
        public string statusAsOn { get; set; }
        public string registrationDate { get; set; }
        public string fuelDescription { get; set; }
        public string statePermitExpiryDate { get; set; }
        public string wheelbase { get; set; }
    }

    public class VehicleRCSearchRequest : Common
    {
        public string engine_no { get; set; }
        public string chassis_no { get; set; }
        public string state { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class VehicleRCSearchResponse : MdlAgrKyc
    {
        public ResultVehicleRCSearch result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public string error { get; set; }
    }

    public class ResultVehicleRCSearch
    {
        public string rc_manu_month_yr { get; set; }
        public string rc_maker_model { get; set; }
        public string rc_f_name { get; set; }
        public string rc_eng_no { get; set; }
        public string rc_owner_name { get; set; }
        public string rc_vh_class_desc { get; set; }

        public string rc_present_address { get; set; }
        public string rc_color { get; set; }
        public string rc_regn_no { get; set; }
        public string tax_paid_upto { get; set; }
        public string rc_maker_desc { get; set; }
        public string rc_chasi_no { get; set; }

        public string rc_mobile_no { get; set; }
        public string rc_registered_at { get; set; }
        public string rc_valid_upto { get; set; }
        public string rc_regn_dt { get; set; }
        public string rc_financer { get; set; }
        public string rc_permanent_address { get; set; }

    }

    public class LPGIDAuthenticationRequest : Common
    {
        public string lpg_id { get; set; }
        public string function_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class LPGIDAuthenticationResponse : MdlAgrKyc
    {
        public ResultLPGIDAuthentication result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public int statuscode { get; set; }
        public string error { get; set; }
    }

    public class ResultLPGIDAuthentication
    {
        public string status { get; set; }
        public string ApproximateSubsidyAvailed { get; set; }
        public string SubsidizedRefillConsumed { get; set; }
        public string pin { get; set; }
        public string ConsumerEmail { get; set; }
        public string DistributorCode { get; set; }

        public string BankName { get; set; }
        public string IFSCCode { get; set; }

        [JsonProperty("city/town")]
        public string city_town { get; set; }

        public string AadhaarNo { get; set; }
        public string ConsumerContact { get; set; }
        public string DistributorAddress { get; set; }

        public string ConsumerName { get; set; }
        public string ConsumerNo { get; set; }
        public string DistributorName { get; set; }
        public string BankAccountNo { get; set; }
        public string GivenUpSubsidy { get; set; }
        public string ConsumerAddress { get; set; }

        public string LastBookingDate { get; set; }
        public string TotalRefillConsumed { get; set; }

    }

    public static class ErrorResponseKyc
    {
        public const string
            errorResponse = "Error Response obtained - ";

    }



}