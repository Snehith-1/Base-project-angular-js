using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{
    /// <summary>
    /// Classes modelling the structure of Contract(SamAgro) for posting to External ERP through HyperbridgeAPI  
    /// </summary>
    /// <remarks>Written by Praveen Raj</remarks>
    public class MdlSamAgroHBAPIContract
    {
    }

    public class MdlContractHBAPI
    {
        public MdlContractHBAPI()
        {
            GSTDetails taxregistration = new GSTDetails();
            AddressDetails addressbook = new AddressDetails();
        }

        public string ExternalID { get; set; }
        public string officialemail_address { get; set; }
        public string official_telephoneno { get; set; }
        public string company_name { get; set; }
        public string customer_id { get; set; }
        public string customer_erpid { get; set; }
        public string companypan_no { get; set; }
        public string companytype_name { get; set; }
        public string tan_number { get; set; }
        public string incometax_returnsstatus { get; set; }
        public string lastyear_turnover { get; set; }
        public GSTDetails gstDetails { get; set; }

        public AddressDetails addressDetails { get; set; }

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
        public string programlimit_validdfrom { get; set; }
        public string programlimit_validdto { get; set; }
        public string rate_interest { get; set; }
        public string penal_interest { get; set; }
        public string trade_orginatedby { get; set; }
        public string loanfacility_amount { get; set; }
        public string SA_Brokerage { get; set; }
        public string processing_collectiontype { get; set; }
        public string processing_fee { get; set; }
        public string doccharge_collectiontype { get; set; }
        public string doc_charges { get; set; }
        public string scopeof_transport { get; set; }
        public string scopeof_loading { get; set; }
        public string scopeof_unloading { get; set; }
        public string scopeof_qualityandquantity { get; set; }
        public string scopeof_moisturegainloss { get; set; }
        public string scopeof_insurance { get; set; }
        public string loan_type { get; set; }
        public string broker_name { get; set; }
        public string custentity_sam_deferral_status { get; set; }
        public string iec_licenseno { get; set; }
        public string productsub_type { get; set; }
        public string sa_name { get; set; }
        public string rm_erpid { get; set; }
        public string program_erpid { get; set; }
        public string facility_mode { get; set; }
        public string insurance_applicability { get; set; }
        public string insurance_limit { get; set; }
        public string deferral_status { get; set; }
        public string primary_gst { get; set; }

    }

    public class HBAPIContractResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string error_response { get; set; }
        public string contract_erpid { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
    }

    public static class HBAPINameRepoContract
    {
        public const string
            PostContract = "PostContract",
            UpdateContractInstitution = "UpdateContractInstitution",
            UpdateContractCharges = "UpdateContractCharges",
            UpdateContractTrade = "UpdateContractTrade",
            UpdateContractProduct = "UpdateContractProduct",
            UpdateContractBasicDetails = "UpdateContractBasicDetails",
            UpdateContractDeferralStatus = "UpdateContractDeferralStatus",
            PostCommodity = "PostCommodity",
            AddSupplierToContract = "AddSupplierToContract",
            UpdateContractExistingAddress = "UpdateContractExistingAddress",
            UpdateContractAddressAdd = "UpdateContractAddressAdd";
    }

    public static class ContractAPIMetaList
    {
        public const string
            PostContractHBAPI = "PostContractHBAPI",
            UpdateContractInstitutionHBAPI = "UpdateContractInstitutionHBAPI",
            UpdateContractChargesHBAPI = "UpdateContractChargesHBAPI",
            UpdateContractTradeHBAPI = "UpdateContractTradeHBAPI",
            UpdateContractProductHBAPI = "UpdateContractProductHBAPI",
            UpdateContractBasicDetailsHBAPI = "UpdateContractBasicDetailsHBAPI",
            UpdateContractDeferralStatusHBAPI = "UpdateContractDeferralStatusHBAPI",
            UpdateContractInstitutionAddressHBAPI = "UpdateContractInstitutionAddressHBAPI",
            UpdateContractInstitutionAddressAddHBAPI = "UpdateContractInstitutionAddressAddHBAPI";

    }

    public class MdlContractInstitutionUpdateHBAPI
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
        public string iec_licenseno { get; set; }

    }

    public class HBAPIContractUpdateResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> contactlist_generaldetails { get; set; }
        public Dictionary<string, string> contactlist_company { get; set; }
        public Dictionary<string, string> contactlist_individual { get; set; }
        public Dictionary<string, string> addresslist { get; set; }
    }

    public class MdlContractChargesUpdateHBAPI
    {
        public string processing_collectiontype { get; set; }
        public string processing_fee { get; set; }
        public string doccharge_collectiontype { get; set; }
        public string doc_charges { get; set; }
       
    }

    public class MdlContractTradeUpdateHBAPI
    {
        public string scopeof_transport { get; set; }
        public string scopeof_loading { get; set; }
        public string scopeof_unloading { get; set; }
        public string scopeof_qualityandquantity { get; set; }
        public string scopeof_moisturegainloss { get; set; }
        public string scopeof_insurance { get; set; }

    }

    public class MdlContractProductUpdateHBAPI
    {
        public string programlimit_validdfrom { get; set; }
        public string programlimit_validdto { get; set; }
        public string rate_interest { get; set; }
        public string penal_interest { get; set; }
        public string trade_orginatedby { get; set; }
        public string loanfacility_amount { get; set; }
        public string SA_Brokerage { get; set; }
        public string loan_type { get; set; }
        public string productsub_type { get; set; }
        public string facility_mode { get; set; }
        public string program_erpid { get; set; }        
        public string insurance_applicability { get; set; }
        public string insurance_limit { get; set; }

    }

    public class MdlContractBasicDetailsUpdateHBAPI
    {
        public string company_name { get; set; }
        public string sa_name { get; set; }

    }

    public class MdlCommodityHBAPI
    {
        public string contract_externalid { get; set; }
        public string commodity_erpid { get; set; }
        public string quantity { get; set; }
        public string uom { get; set; }
        public string margin { get; set; }
        public string creditperiod_days { get; set; }
        public bool customer_advance { get; set; }
        public bool customer_milestone { get; set; }
        public bool customer_retention { get; set; }
        public bool supplier_advance { get; set; }
        public bool supplier_milestone { get; set; }
        public bool supplier_retention { get; set; }
        public string supplier_advance_maxpercent { get; set; }
        public string supplier_milestone_maxpercent { get; set; }
        public string supplier_retention_maxpercent { get; set; }
    }

    public class MdlSupplierAddContractHBAPI
    {
        public string contract_externalid { get; set; }
        public string vendor_externalid { get; set; }

    }
    public static class HBAPICADProcessMenuGID
    {
        public const string
            SoftcopyVetting = "AGDMGTDTS";
    }

    public class MdlContractDeferralStatusUpdateHBAPI
    {
        public string deferral_status { get; set; }

    }

    public class MdlContractAddressUpdateHBAPI
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

    public class limit
    {
        public limit()
        {
            LIMITDETAILS LIMITDETAILS = new LIMITDETAILS();
        }
        public LIMITDETAILS LIMITDETAILS { get; set; }
        //public string contract_id {get;set;}
    }

    public class LIMITDETAILS
    {
        public LIMITDETAILS()
        {
            LIMITData[] limitlist = new LIMITData[] { };
        }
        public LIMITData[] limitlist { get; set; }
    }

    public class LIMITData
    {
        public string contract_id { get; set; }
    }

    public class MdlLimitResponse : result1234
    {
        public MdlLimitResponse()
        {
            MdlLimitResponseList limitResponselist = new MdlLimitResponseList();
        }
        public MdlLimitResponseList limitResponselist { get; set; }
    }

    public class MdlLimitResponseList
    {
        public MdlLimitResponseList()
        {
            MdlLimitResponseData[] limitresponsedata = new MdlLimitResponseData[] { };
        }
        public MdlLimitResponseData[] limitresponsedata { get; set; }
    }

    public class MdlLimitResponseData
    {
        public string contract_id { get; set; }
        public string overdue_balance { get; set; }
        public string credit_limit { get; set; }
    }

    public class MdlHBAPICommodityResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class MdlHBAPISupplierResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

}