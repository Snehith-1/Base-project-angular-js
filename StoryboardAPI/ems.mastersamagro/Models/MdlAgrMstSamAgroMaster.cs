using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values for various masters in samagro and their summary, add, edit, view, active, inactive & delete records (Samagro Master includes Scope, Other creditor Applicant type, Milestone Payment, Sector classification, type of warehouse, Buyer/Supplier Type, Product desk mapping & Insurane company).
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Kalaiarsan, Praveen Raj.R</remarks>


    public class MdlAgrMstSamAgroMaster : result
    {
        public List<applicationmst_list> applicationmst_list { get; set; }
        //public List<BuyerSupplier_Type> BuyerSupplier_Type { get; set; }
        //public List<BuyerSupplier_Type> BuyerSupplier_Type { get; set; }
    }

    public class applicationmst_list : result
    {
        public string typeofsupplynature_gid { get; set; }
        public string typeofsupplynature_name { get; set; }
        public string sectorclassification_gid { get; set; }
        public string sectorclassification_name { get; set; }
        public string typeofwarehouse_gid { get; set; }
        public string typeofwarehouse_name { get; set; }

        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string api_code { get; set; }
    }
    public class applicationmst : result
    {
        public string typeofsupplynature_gid { get; set; }
        public string typeofsupplynature_name { get; set; }
        public string sectorclassification_gid { get; set; }
        public string sectorclassification_name { get; set; }
        public string typeofwarehouse_gid { get; set; }
        public string typeofwarehouse_name { get; set; }

        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    // Product Desk Mapping -- STARTING

    public class MdlProductDesk : result
    {
        public string productdesk_gid { get; set; }
        public string productdesk_id { get; set; }
        public string products_gid { get; set; }
        public string products_name { get; set; }
        public string productdesk_name { get; set; }
        public string productdesk_lms { get; set; }
        public string productdesk_bureau { get; set; }
        public string productdeskmember_gid { get; set; }
        public string productdeskmanager_gid { get; set; }
        public string productdesk_member { get; set; }
        public string productdesk_manager { get; set; }
        public string productdesk_status { get; set; }

        public List<ProductDesk> ProductDesk { get; set; }
        public List<ProductDeskMember> ProductDeskMember { get; set; }
        public List<ProductDeskManager> ProductDeskManager { get; set; }
        public List<ProductDeskDetails> ProductDeskDetails { get; set; }
        public List<ProductDesklog> ProductDesklog { get; set; }
        public List<ProductDeskManagerem_list> ProductDeskManagerem_list { get; set; }
        public List<ProductDeskMemberem_list> ProductDeskMemberem_list { get; set; }
        public List<Products_Name> Products_Name { get; set; }

    }

    public class ProductDesk : result
    {
        public string productdesk_gid { get; set; }
        public string productdesk_id { get; set; }
        public string products_gid { get; set; }
        public string products_name { get; set; }
        public string productdesk_name { get; set; }
        public string productdesk_status { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public char rbo_status { get; set; }
        public string productdesk_lms { get; set; }
        public string productdesk_bureau { get; set; }
        public string productdeskmember_gid { get; set; }
        public string productdeskmanager_gid { get; set; }
        public string api_code { get; set; }
    }

    public class ProductDeskMember
    {
        public string msGetproductdeskmember_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class ProductDeskManager
    {
        public string msGetproductdeskmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class ProductDeskMemberem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class ProductDeskManagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class ProductDeskDetails : result
    {
        public string productdesk_name { get; set; }
        public string productdesk_member { get; set; }
        public string productdesk_manager { get; set; }
    }

    public class ProductDesklog
    {
        public string productdesk_gid { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string productdesk_status { get; set; }
    }

    public class Products_Name
    {
        public string products_gid { get; set; }
        public string products_name { get; set; }
    }


    // Product Desk Mapping -- ENDING


    public class Mdltypeofsupplynature
    {
        public List<typeofsupplynature_list> typeofsupplynature_list { get; set; }
    }

    public class typeofsupplynature_list
    {
        public string typeofsupplynature_gid { get; set; }
        public string typeofsupplynature_name { get; set; }
    }

    public class Mdlsectorclassification
    {
        public List<Mdlsectorclassification_list> Mdlsectorclassification_list { get; set; }
    }

    public class Mdlsectorclassification_list
    {
        public string sectorclassification_gid { get; set; }
        public string sectorclassification_name { get; set; }
    }

    public class Mdlinsurancecompany
    {
        public List<Mdlinsurancecompany_list> Mdlinsurancecompany_list { get; set; }
    }

    public class Mdlinsurancecompany_list
    {
        public string insurancecompany_gid { get; set; }
        public string insurancecompany_name { get; set; }
    }

    public class MdlinsurancePolicy
    {
        public List<MdlinsurancePolicy_list> MdlinsurancePolicy_list { get; set; }
    }

    public class MdlinsurancePolicy_list
    {
        public string insurancecompany2policy_gid { get; set; }
        public string policy_name { get; set; }
    }

    public class MdlMstProductDropDown : result
    {
        public List<MstProductlist> MstProductlist { get; set; }
    }
    public class MstProductlist
    {
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
    }

    public class MdlMstSubProductDropDown : result
    {
        public List<MstSubProductlist> MstSubProductlist { get; set; }
    }
    public class MstSubProductlist
    {
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }
    }

    public class commoditygststatuslist:result
    {
        public List<commoditygststatus> commoditygststatus { get; set; }
    }

    public class commoditygststatus : result
    {
        public string commoditygststatus_gid { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string IGST_percent { get; set; }
        public string SGST_percent { get; set; }
        public string CGST_percent { get; set; }
        public string CESS_percent { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class commodityTradeProdctlist : result
    {
        public List<commodityTradeProdct> commodityTradeProdct { get; set; }
        public List<commoditycustomerpayment> commoditycustomerpayment { get; set; }
    }

    public class commoditycustomerpayment : result
    {
        public string customerpaymenttype_name { get; set; }
        public string maximumpercent_paymentterm { get; set; }

        public string paymenttypecustomer_gid { get; set; }

    }

        public class commodityTradeProdct : result
    {
        public string commoditytradeproductdtl_gid { get; set; }
        public string mstproduct_gid { get; set; }
        public string variety_gid { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string subproduct_gid { get; set; }
        public string subproduct_name { get; set; }
        public string insurancecompany_gid { get; set; }
        public string insurancecompany_name { get; set; }
        public string insurancepolicy_name { get; set; }
        public string insurancepolicy_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class commodityDocumentUploadlist : result
    {
        public List<commodityDocumentUpload> commodityDocumentUpload { get; set; }
    }

    public class commodityDocumentUpload : result
    {
        public string commoditydocument_gid { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string ason_date { get; set; }
        public string commodityreport_filename { get; set; }
        public string commodityreport_filepath { get; set; }
        public string riskanalysisreport_filename { get; set; }
        public string riskanalysisreport_filepath { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    } 

    public class MdlInsuranceCompany : result
    {
        public string insurancecompany_gid { get; set; }
        public string insurancecompany_name { get; set; }
        public string rbo_status { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public List<insurancecompany_list> insurancecompany_list { get; set; }
    }
    public class insurancecompany_list
    {
        public string insurancecompany_gid { get; set; }
        public string insurancecompany_name { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string api_code { get; set; }
    }

    public class MdlPolicy : result
    {
        public string insurancecompany2policy_gid { get; set; }
        public string insurancecompany_gid { get; set; }
        public string policy_name { get; set; }
        public string policy_number { get; set; }
        public string policy_amount { get; set; }
        public string policyperiod_from { get; set; }
        public string policyperiod_to { get; set; }
        public string premium_amount { get; set; }
        public string premiumpayment_status { get; set; }
        public string paid_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string editpolicyperiod_from { get; set; }
        public string editpolicyperiod_to { get; set; }
        public string editpaid_date { get; set; }
        public DateTime policyperiodfrom { get; set; }
        public DateTime policyperiodto { get; set; }
        public DateTime paiddate { get; set; }

        public List<policy_list> policy_list { get; set; }
        public List<policydoc_list> policydoc_list { get; set; }
    }

    public class policy_list
    {
        public string insurancecompany2policy_gid { get; set; }
        public string insurancecompany_gid { get; set; }
        public string policy_name { get; set; }
        public string policy_number { get; set; }
        public string policy_amount { get; set; }
        public string policyperiod_from { get; set; }
        public string policyperiod_to { get; set; }
        public string premium_amount { get; set; }
        public string premiumpayment_status { get; set; }
        public string paid_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class policydoc_list : result
    {
        public string insurancecompanypolicy2document_gid { get; set; }
        public string insurancecompany2policy_gid { get; set; }
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlWarehouseAddressList : result
    {
        public List<MdlWarehouseAddressdtl> MdlWarehouseAddressdtl { get; set; }
    }
    public class MdlWarehouseAddressdtl
    {
        public string warehouse2address_gid { get; set; }
        public string warehouseaddress_name { get; set; }
    }

    public class MdlWarehousedtlList : result
    {
        public List<MdlWarehousedtl> MdlWarehousedtl { get; set; }
    }

    public class MdlWarehousedtl
    {
        public string warehouse_gid { get; set; }
        public string warehouse_name { get; set; }
        public string warehouse_area { get; set; }
        public string warehousearea_uom { get; set; }
        public string warehousearea_uomgid { get; set; }
        public string totalcapacity_area { get; set; }
        public string totalcapacityarea_uomgid { get; set; }
        public string totalcapacityarea_uom { get; set; }
        public string totalcapacity_volume { get; set; }
        public string volume_uomgid { get; set; }
        public string volume_uom { get; set; }
        public string typeofwarehouse_gid { get; set; }
        public string typeofwarehouse_name { get; set; }
    }

    public class MdlSupplierDropdowndtlList
    {
        public List<MdlSupplierDropdowndtl> MdlSupplierDropdowndtl { get; set; }
    }

    public class MdlSupplierDropdowndtl : result
    {
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
    }

    public class MdlSupplierdtlList : result
    {
        public List<MdlSupplierdtl> MdlSupplierdtl { get; set; }
    }

    public class MdlSupplierdtl : result
    {
        public string apploan2supplierdtl_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string application_gid { get; set; }
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
        public string supplier_address { get; set; }
        public string supplier_emailid { get; set; }
        public string supplier_phoneno { get; set; }
        public string supplier_gstno { get; set; }
        public string supplier_pandtl { get; set; }
        public string milestone_applicable { get; set; }
        public string milestonepaymenttype_gid { get; set; }
        public string milestonepaymenttype_name { get; set; }
        public string supplier_vintage { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string supplier_margin { get; set; }
        public bool tmpadd_status { get; set; }
        public string lspage { get; set; }
        public string product_flag { get; set; }
        public string supplieragreement_id { get; set; }
        public string suppliername_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }

        public List<MdlSupplierGSTdtl> MdlSupplierGSTdtl { get; set; }

        public List<MdlSupplierDetailsActive> MdlSupplierDetailsActive { get; set; }

        public postagrapploan2supplierdtl_gid[] lsloan2supplierdtl_gid { get; set; }
    }

    public class postagrapploan2supplierdtl_gid
    {
        public string apploan2supplierdtl_gid { get; set; }

    }
    public class MdlSupplierDetailsActive : result
    {
        public string apploan2supplierdtl_gid { get; set; }        
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }

    }
    public class MdlSupplierGSTdtlList : result
    {
        public List<MdlSupplierGSTdtl> MdlSupplierGSTdtl { get; set; }
    }

    public class MdlSupplierGSTdtl : result
    {
        public string institution2branch_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_no { get; set; }
    }

    public class MdlSupplierPaymentdtlList : result
    {
        public List<MdlSupplierPaymentdtl> MdlSupplierPaymentdtl { get; set; }
    }

    public class MdlSupplierPaymentdtl : result
    {
        public string apploan2supplierpayment_gid { get; set; }
        public string application_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string commodity_gid { get; set; }
        public string commodity_name { get; set; }
        public string supplierpayment_type { get; set; }
        public string supplierpayment_typegid { get; set; }
        public string maxpercent_paymentterm { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public bool tmpadd_status { get; set; }
        public string product_flag { get; set; }
    }

    public class MdlPaymentdtlList : result
    {

        public string payment_value { get; set; }

        public string exceeded_value { get; set; }
        public List<MdlPaymentdtl> MdlPaymentdtl { get; set; }
    }

    public class MdlPaymentdtl : result
    {
        public string paymenttypecustomer_gid { get; set; }
        public string application_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string customerpaymenttype_gid { get; set; }
        public string customerpaymenttype_name { get; set; }
        public string maximumpercent_paymentterm { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public bool tmpadd_status { get; set; }
    }

    public class MdlCommodityDtls : result
    {
        public string milestone_applicability { get; set; }
        public string insurance_applicability { get; set; }
        public string milestonepayment_name { get; set; }
        public string sa_payout { get; set; }
        public string insurance_availability { get; set; }
        public string insurance_percent { get; set; }
        public string insurance_cost { get; set; }
        public string net_yield { get; set; }
        public string markto_marketvalue { get; set; }
        public string pricereference_source { get; set; }
        public string creditperiod_years { get; set; }
        public string creditperiod_months { get; set; }
        public string creditperiod_days { get; set; }
        public string overallcreditperiod_limit { get; set; }
        public string commodity_margin { get; set; }
        public string commoditynet_yield { get; set; }
        public string graceperiod_days { get; set; }
        public string customerpaymenttype_gid { get; set; }
        public string customerpaymenttype_name { get; set; }
        public string maximumpercent_paymentterm { get; set; }
    }

    public class MdlBuyerSupplierType : result
    {

        public List<BuyerSupplierType_List> BuyerSupplierType_List { get; set; }
    }

    public class BuyerSupplierType_List
    {
        public string buyersuppliertype_gid { get; set; }
        public string buyersuppliertype_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string Status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }


    }

    public class BuyerSupplierType : result
    {
        public string buyersuppliertype_gid { get; set; }
        public string buyersuppliertype_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }

    }

}