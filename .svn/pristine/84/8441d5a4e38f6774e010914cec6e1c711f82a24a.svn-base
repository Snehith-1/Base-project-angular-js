using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


/// <summary>
/// (It's used for Kyc View) Kyc View DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj</remarks>
/// 

namespace ems.master.Models
{
    public class MdlPANAuthentication : result
    {
        public string pan_no { get; set; }
        public string pan_name { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<panauthentication_list> panauthentication_list { get; set; }
    }

    public class panauthentication_list
    {
        public string pan_no { get; set; }
        public string pan_name { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlPANAadhaarLink : result
    {
        public string pan_no { get; set; }
        public string panaadhaarlink_status { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<panaadhaarlink_list> panaadhaarlink_list { get; set; }
    }

    public class panaadhaarlink_list
    {
        public string pan_no { get; set; }
        public string panaadhaarlink_status { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlDLAuthentication : result
    {
        public string kycdlauthentication_gid { get; set; }
        public string dlno { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<dlauthentication_list> dlauthentication_list { get; set; }
    }

    public class dlauthentication_list
    {
        public string kycdlauthentication_gid { get; set; }
        public string dlno { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlEPICAuthentication : result
    {
        public string kycepicauthentication_gid { get; set; }
        public string epic_no { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<epicauthentication_list> epicauthentication_list { get; set; }
    }

    public class epicauthentication_list
    {
        public string kycepicauthentication_gid { get; set; }
        public string epic_no { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlIFSCAuthentication : result
    {
        public string kycifscauthentication_gid { get; set; }
        public string ifsc { get; set; }
        public string bank { get; set; }
        public string branch { get; set; }
        public string address { get; set; }
        public string micr { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<ifscauthentication_list> ifscauthentication_list { get; set; }
    }

    public class ifscauthentication_list
    {
        public string kycifscauthentication_gid { get; set; }
        public string ifsc { get; set; }
        public string bank { get; set; }
        public string branch { get; set; }
        public string address { get; set; }
        public string micr { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlBankAccVerification : result
    {
        public string kycbankaccverification_gid { get; set; }
        public string ifsc { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<bankaccverification_list> bankaccverification_list { get; set; }
    }

    public class bankaccverification_list
    {
        public string kycbankaccverification_gid { get; set; }
        public string ifsc { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlGSTSBPAN : result
    {
        public string kycgstsbpan_gid { get; set; }
        public string pan { get; set; }
        public string gstValues { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<gstsbpan_list> gstsbpan_list { get; set; }
    }

    public class gstsbpan_list
    {
        public string kycgstsbpan_gid { get; set; }
        public string pan { get; set; }
        public string gstValues { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlDrivingLicenseDetails : result
    {
        public int statusCode { get; set; }
        public string requestId { get; set; }
        public ResultDrivingLicense result { get; set; }
    }

    public class ResultDrivingLicense
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
        public Nullable<int> pin { get; set; }
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

    public class MdlVoterIDDetails : result
    {
        public ResultVoterID result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statusCode { get; set; }
    }

    public class ResultVoterID
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

    public class MdlPassportDetails : result
    {
        public ResultPassport result { get; set; }
        public string request_id { get; set; }
        public string statusCode { get; set; }
    }

    public class ResultPassport
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

    public class MdlPassportAuthentication : result
    {
        public string kycpassportauthentication_gid { get; set; }
        public string fileNo { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<passportauthentication_list> passportauthentication_list { get; set; }
    }

    public class passportauthentication_list
    {
        public string kycpassportauthentication_gid { get; set; }
        public string fileNo { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlGSTSBPANDetails : result
    {
        public string requestId { get; set; }
        public ResultGSTSBPAN[] result { get; set; }
        public int statusCode { get; set; }
    }

    public class ResultGSTSBPAN
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

    public class MdlIfscVerificationDetails : result
    {
        [JsonProperty("status-code")]
        public string statusCode { get; set; }

        public string request_id { get; set; }
        public ResultIFSC result { get; set; }
    }

    public class ResultIFSC
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

    public class MdlBankAccVerificationDetails : result
    {
        [JsonProperty("status-code")]
        public string statusCode { get; set; }

        public string request_id { get; set; }
        public ResultBankAcc result { get; set; }
    }

    public class ResultBankAcc
    {
        public bool bankTxnStatus { get; set; }
        public string accountNumber { get; set; }
        public string ifsc { get; set; }
        public string accountName { get; set; }
        public string bankResponse { get; set; }
    }

    public class MdlUDYAMAuthentication : result
    {
        public string udyamreg_no { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<udyamauthentication_list> udyamauthentication_list { get; set; }
    }

    public class udyamauthentication_list
    {
        public string udyamreg_no { get; set; }
        public string remarks { get; set; }
        public string validation_status { get; set; }
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

}