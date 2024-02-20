using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This models will provide values to various third party api to verify customer data in credit stage
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>

    public class MdlTAN : result
    {
        public string tan_no { get; set; }
        public string remarks { get; set; }
        public List<tan_list> tan_list { get; set; }
    }
    public class tan_list
    {
        public string tan_no { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public string tandtl_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string validation_status { get; set; }
    }
    public class MdlCIN : result
    {
        public string cin_no { get; set; }
        public string remarks { get; set; }
        public List<cin_list> cin_list { get; set; }
    }
    public class cin_list
    {
        public string cin_no { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public string companyllpno_gid { get; set; }
        public string mcasignatories_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string validation_status { get; set; }
    }
    public class MdlCompanyLLPDetails : result
    {
        public string company_name { get; set; }
        public string roc_code { get; set; }
        public string registration_no { get; set; }
        public string company_category { get; set; }
        public string company_subcategory { get; set; }
        public string class_of_company { get; set; }
        public string number_of_members { get; set; }
        public string date_of_incorporation { get; set; }
        public string company_status { get; set; }
        public string registered_address { get; set; }
        public string alternative_address { get; set; }
        public string email_address { get; set; }
        public string listed_status { get; set; }
        public string suspended_at_stock_exchange { get; set; }
        public string date_of_last_AGM { get; set; }
        public string date_of_balance_sheet { get; set; }
        public string paid_up_capital { get; set; }
        public string authorised_capital { get; set; }

    }
    public class MdlIECDetailed : result
    {
        public string iec_no { get; set; }
        public string remarks { get; set; }
        public List<IECDetailed_list> IECDetailed_list { get; set; }
    }
    public class IECDetailed_list
    {
        public string iec_no { get; set; }
        public string name { get; set; }
        public string remarks { get; set; }
        public string iecdtl_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string validation_status { get; set; }
    }

    public class MdlFSSAI : result
    {
        public string reg_no { get; set; }
        public string remarks { get; set; }
        public List<fssai_list> fssai_list { get; set; }
    }
    public class fssai_list
    {
        public string reg_no { get; set; }
        public string lic_type { get; set; }
        public string remarks { get; set; }
        public string fssailicenseauthentication_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string validation_status { get; set; }
    }
    public class MdlFSSAIDetails : result
    {
        public string fssai_status { get; set; }
        public string license_type { get; set; }
        public string license_no { get; set; }
        public string firm_name { get; set; }
        public string address { get; set; }
    }
    public class MdlFDA : result
    {
        public string reg_no { get; set; }
        public string remarks { get; set; }
        public List<fda_list> fda_list { get; set; }
    }
    public class fda_list
    {
        public string license_no { get; set; }
        public string lic_type { get; set; }
        public string remarks { get; set; }
        public string fdalicenseauthentication_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string validation_status { get; set; }
    }


    public class MdlFDADetails : result
    {
        public string store_name { get; set; }
        public string contact_no { get; set; }
        public string license_detail { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class MdlMCASignatoryDetails : result
    {
        public string date_of_appointment { get; set; }
        public string designation { get; set; }
        public string dsc_expiry_date { get; set; }
        public string wheather_dsc_registered { get; set; }
        public string DINDPINPAN { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public List<mcasignatorydetails_list> mcasignatorydetails_list { get; set; }
    }

    public class mcasignatorydetails_list
    {
        public string date_of_appointment { get; set; }
        public string designation { get; set; }
        public string dsc_expiry_date { get; set; }
        public string wheather_dsc_registered { get; set; }
        public string DINDPINPAN { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
    }
    public class MdlGSTAuthentication : result
    {
        public string buiness_constitution { get; set; }
        public string registration_status { get; set; }
        public string business_legalname { get; set; }
        public string taxpayer { get; set; }
        public string trade_name { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
        public string is_defaulter { get; set; }
        public string is_any_delay { get; set; }
    }

    public class MdlIECProfileDetails : result
    {
        public ResultIEC result { get; set; }
        public string request_id { get; set; }
        [JsonProperty("status-code")]
        public string statuscode { get; set; }

    }

    public class ResultIEC
    {
        public string name { get; set; }
        public string ie_code { get; set; }
        public string address { get; set; }
        public string iecgate_status { get; set; }
        public string pan { get; set; }

        public Directors[] directors { get; set; }
        public Branches[] branches { get; set; }
        public RCMCDetails[] rcmc_details { get; set; }

        public string registration_details { get; set; }
        public string bank_details { get; set; }
        public string iec_allotment_date { get; set; }
        public string file_number { get; set; }
        public string file_date { get; set; }

        public string party_name_and_address { get; set; }
        public string phone_no { get; set; }
        public string e_mail { get; set; }
        public string exporter_type { get; set; }
        public string date_of_establishment { get; set; }

        public string bin_pan_extension { get; set; }
        public string pan_issue_date { get; set; }
        public string pan_issued_by { get; set; }
        public string nature_of_concern { get; set; }
        public string iec_status { get; set; }

        public string no_of_branches { get; set; }
    }

    public class Directors
    {
        public string father_name { get; set; }
        public string dir_name { get; set; }
        public string address { get; set; }
        public string contact_no { get; set; }
    }

    public class Branches
    {
        public string branch_code { get; set; }
        public string address { get; set; }
    }
    public class RCMCDetails
    {
        public string rcmc_no { get; set; }
        public string rcmc_issue_date { get; set; }
        public string rcmc_issued_by { get; set; }
        public string rcmc_exp_date { get; set; }
        public string rcmc_type { get; set; }

    }

    public class MdlGSPGSTReturnFilingDetails : result
    {
        public string requestId { get; set; }
        public ResultReturnFiling result { get; set; }
        public int statusCode { get; set; }
    }

    public class ResultReturnFiling
    {
        public string gstin { get; set; }
        public Compliance_Status compliance_status { get; set; }
        public ResultYearly[] result { get; set; }
    }

    public class Compliance_Status
    {
        public bool is_any_delay { get; set; }
        public bool is_defaulter { get; set; }
    }

    public class ResultYearly
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

    public class MdlGSTVerificationDetails : result
    {
        public string requestId { get; set; }
        public ResultGSTVerification result { get; set; }
        public int statusCode { get; set; }
    }

    public class ResultGSTVerification
    {
        public string stjCd { get; set; }
        public string dty { get; set; }
        public string lgnm { get; set; }
        public string stj { get; set; }
        public AdditionalAddress[] adadr { get; set; }
        public string cxdt { get; set; }
        public string gstin { get; set; }
        public string[] nba { get; set; }
        public string lstupdt { get; set; }
        public string ctb { get; set; }
        public string rgdt { get; set; }
        public PrimaryAddress pradr { get; set; }
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

    public class PrimaryAddress
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

    public class AdditionalAddress
    {
        public string addr { get; set; }
        public string ntr { get; set; }
        public string adr { get; set; }
        public string em { get; set; }
        public string lastUpdatedDate { get; set; }
        public string mb { get; set; }
    }

    public class MdlGSTAuthenticationDetails : result
    {
        public string requestId { get; set; }
        public ResultGSTAuthentication result { get; set; }
        public int statusCode { get; set; }
    }

    public class ResultGSTAuthentication
    {
        public object canFlag { get; set; }
        public Contacted1 contacted { get; set; }
        public object ppr { get; set; }
        public string cmpRt { get; set; }
        public string rgdt { get; set; }
        public string tradeNam { get; set; }
        public string[] nba { get; set; }
        public string[] mbr { get; set; }
        public AdditionalAddress1[] adadr { get; set; }
        public PrimaryAddress1 pradr { get; set; }
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

    public class PrimaryAddress1
    {
        public string adr { get; set; }
        public string em { get; set; }
        public string mb { get; set; }
        public string ntr { get; set; }
        public string addr { get; set; }
        public string lastUpdatedDate { get; set; }
    }

    public class AdditionalAddress1
    {
        public string adr { get; set; }
        public string em { get; set; }
        public string mb { get; set; }
        public string ntr { get; set; }
        public string addr { get; set; }
        public string lastUpdatedDate { get; set; }
    }

    public class MdlLPGID : result
    {
        public string lpg_id { get; set; }
        public string remarks { get; set; }
        public List<LPGID_list> LPGID_list { get; set; }
    }
    public class LPGID_list
    {
        public string lpg_id { get; set; }
        public string remarks { get; set; }
        public string lpgiddtl_gid { get; set; }
        public string validation_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlLPGIDAuthenticationDetails : result
    {
        public ResultLPGIDAuthentication result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statuscode { get; set; }
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

    public class MdlShop : result
    {
        public string regNo { get; set; }
        public string remarks { get; set; }
        public List<shop_list> shop_list { get; set; }
    }
    public class shop_list
    {
        public string regNo { get; set; }
        public string remarks { get; set; }
        public string shopandestablishment_gid { get; set; }
        public string validation_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }


    public class MdlShopAndEstablishmentDetails : result
    {
        public ResultShopAndEstablishment result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }
    }

    public class ResultShopAndEstablishment
    {
        public string category { get; set; }
        public string status { get; set; }
        public string commenceDate { get; set; }
        public string entityName { get; set; }
        public string totalWorkers { get; set; }

        [JsonProperty("fathersNameOfOccupier")]
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

    public class MdlRCAuthAdvanced : result
    {
        public string registrationNumber { get; set; }
        public string remarks { get; set; }
        public List<RCAuthAdvanced_list> RCAuthAdvanced_list { get; set; }
    }
    public class RCAuthAdvanced_list
    {
        public string registrationNumber { get; set; }
        public string remarks { get; set; }
        public string vehiclercauthadvanced_gid { get; set; }
        public string validation_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlVehicleRCAuthAdvancedDetails : result
    {
        public ResultVehicleRCAuthAdvanced result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }
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

    public class MdlRCSearch : result
    {
        public string engine_no { get; set; }
        public string chassis_no { get; set; }
        public string state { get; set; }
        public string remarks { get; set; }
        public List<RCSearch_list> RCSearch_list { get; set; }
    }
    public class RCSearch_list
    {
        public string engine_no { get; set; }
        public string remarks { get; set; }
        public string vehiclercsearch_gid { get; set; }
        public string validation_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlVehicleRCSearchDetails : result
    {
        public ResultVehicleRCSearch result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statuscode { get; set; }
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

    public class MdlPropertyTax : result
    {
        public string propertyNo { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string ulb { get; set; }
        public string remarks { get; set; }
        public List<PropertyTax_list> PropertyTax_list { get; set; }
    }
    public class PropertyTax_list
    {
        public string propertyNo { get; set; }
        public string remarks { get; set; }
        public string propertytax_gid { get; set; }
        public string validation_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlPropertyTaxDetails : result
    {
        public ResultPropertyTax result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }
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

    public class MdlGST : result
    {
        public List<gst_list> gst_list { get; set; }
    }
    public class gst_list
    {
        public string gspinverification_gid { get; set; }
        public string gstreturnfilling_gid { get; set; }
        public string gspinauthentication_gid { get; set; }
        public string function_gid { get; set; }
        public string gst_no { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string validation_status { get; set; }
    }

}