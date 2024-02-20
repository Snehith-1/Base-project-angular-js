using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// <summary>
// This Models provide access for various Single fields and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Warehouse Master
// </summary>
// <remarks>Written by Premchander.K </remarks>

namespace ems.mastersamagro.Models
{
    public class MdlAgrMstWarhouseAdd : result
    {
        public List<spocid_list> spocid_list { get; set; }
        public List<MdlAgrMstWarehouseCreation> MdlAgrMstWarehouseCreation { get; set; }

        public bool status { get; set; }
        public string message { get; set; }
        public string master_gid { get; set; }
        public string master_name { get; set; }
        public string deleted_by { get; set; }
        public string deleted_date { get; set; }
        public string master_value { get; set; }
        public string warehouse_gid { get; set; }
        public string warehouse2address_gid { get; set; }
        public string warehouse_address { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string remarks { get; set; }

        //public string spoc_list { get; set; }
        public string spoc_phoneno { get; set; }
        public string spoc_phonenolist { get; set; }
        public string spoc_id { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string lsemployee_gid { get; set; }
        
        public List<agrmstwarhouse_upload> agrmstwarhouse_upload { get; set; }
        public List<Mdlagrmstspoc> Mdlagrmstspoc { get; set; }
        public List<Mdlagrmstagreementdtllist> Mdlagrmstagreementdtllist { get; set; }
        public List<warehousetype_list> warehousetype_list { get; set; }


    }

    public class Mdlagrmstagreementdtllist : result

    {
        public string warehouse2agreement_gid { get; set; }
        public string warehouseagreement_address { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string warehouse_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string warehouse2address_gid { get; set; }
        public string warehouse_address { get; set; }
    }

    public class Mdlapprovalremark : result 
    {
        public string remarks { get; set; }
        public string warehouse_gid { get; set; }
        public string warehouse2approval_gid { get; set; } 
        public string product_approvalflag { get; set; }
        public string approvalstatus { get; set; }
    }

    public class warehousetype_list 
    {
        public string typeofwarehouse_gid { get; set; }
        public string typeofwarehouse_name { get; set; }
       
    }

    public class MdlWarehouseSummary
    {
        public List<MdlAgrMstWarehouseCreation> MdlAgrMstWarehouseCreation { get; set; }
    }

    public class MdlAgrMstWarehouseCreation : result

    {
        public List<warehouse2facility_list> warehouse2facility_list { get; set; }

        public string approval_status { get; set; }
        public string warehouse_gid { get; set; }
        public string Gstflag { get; set; }
        public string warehouse_ref_no { get; set; }
        public string warehouse_name { get; set; }
        public string owned_by { get; set; }
        public string warehouse_pan { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string subsidiarywarshouse_name { get; set; }
        public string warehouse_area { get; set; }
        public string warehousearea_uom { get; set; }
        public string totalcapacity_area { get; set; }
        public string warehousefacility_name { get; set; }
        public string totalcapacity_volume { get; set; }
        public string volume_uom { get; set; }
        public string area_uom { get; set; }
        public string charges { get; set; }
        public string capacity { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string product_name { get; set; }
        public string product_gid { get; set; }
        public string warehouse2approval_gid { get; set; }
        public string productapproval_flag { get; set; }
        public string pmgapproval_flag { get; set; }
        public string warehousesubmit_flag { get; set; }
        public string warehousearea_uomgid { get; set; }
        public string totalcapacityarea_uomgid { get; set; }
        public string volume_uomgid { get; set; }
        public string typeofwarehouse_name { get; set; }
        public string typeofwarehouse_gid { get; set; }
        public string Applicant_name { get; set; }
        public string creditor_gid { get; set; }

        public List<facility_list> facility_list { get; set; }
        
    }


    public class warehouse2facility_list
    {
      
        public string warehousefacility_gid { get; set; }
        public string warehousefacility_name { get; set; }
    }

    public class facility_list
    {
        public string warehouse2facility_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string warehousefacility_gid { get; set; }
        public string warehousefacility_name { get; set; }
    }

    public class MdlAgrDropDown : result
    {
        public List<vertical_list> vertical_list { get; set; }
        public List<program_list> program_list { get; set; }
        public List<MdlAgrMstAddressDetails> MdlAgrMstAddressDetails { get; set; }

    }

    public class spocid_list : result
    {
        public string spoc_list { get; set; }
        public string spoc_phoneno { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string lsemployee_gid { get; set; }

    }

    public class agrmstwarhouse_upload : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string warehouse_gid { get; set; }
        public string warehouse2docupload_gid { get; set; }
        public string warehouse2address_gid { get; set; }
        public string warehouse_address { get; set; }
        public string execution_date { get; set; }
        public string expiry_date { get; set; }
        public string warehouseagreement_gid { get; set; }
    }

    public class MdlAgrMstAddressDetails : result
    {
        public string warehouse_gid { get; set; }
        public string warehouse_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string sa_status { get; set; }
        public string saname_gid { get; set; }
        public string sa_name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string vernacular_language { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string overalllimit_amount { get; set; }
        public string processing_fee { get; set; }
        public string doc_charges { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string warehouse_status { get; set; }
        public string proceed_flag { get; set; }
        public string applicant_type { get; set; }
        public string economical_flag { get; set; }
        public string productcharge_flag { get; set; }
        public string productcharges_status { get; set; }
        public string loanfacility_amount { get; set; }
        public string hypothecation_flag { get; set; }
        public List<warehouseadd_list> warehouseadd_list { get; set; }
        public List<warehouselist> warehouselist { get; set; }
        public List<basicdetails_list> basicdetails_list { get; set; }
        public List<valuechainlist> valuechainlist { get; set; }
        public List<vernacularlang_list> vernacularlang_list { get; set; }
        //public List<agrmstwarhouse_upload> agrmstwarhouse_upload { get; set; }
        public string dob;
        public string opswarehouse_gid { get; set; }
        public string opswarehouse_no { get; set; }
        public string statusupdated_by { get; set; }
        public string cluster_head { get; set; }
        public string regional_head { get; set; }
        public string zonal_head { get; set; }
        public string business_head { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string approveinitiated_flag { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }

    }


    public class warehouselist : result
    {
        public string warehouse_gid { get; set; }
        public string warehouse_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
    }
    public class warehouseadd_list
    {
        public string warehouse_gid { get; set; }
        public string warehouse_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string sa_status { get; set; }
        public string sa_id { get; set; }
        public string sa_name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string vernacular_language { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string contactpersonmiddle_name { get; set; }
        public string contactpersonlast_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string landline_no { get; set; }
        public string overalllimit_amount { get; set; }
        public string processing_fee { get; set; }
        public string doc_charges { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string productcharge_flag { get; set; }
        public string economical_flag { get; set; }
        public string warehouse_status { get; set; }
        public string applicant_type { get; set; }
        public string opswarehouse_gid { get; set; }
        public string updated_date { get; set; }
        public string approval_status { get; set; }
        public string submitted_date { get; set; }
        public string ccsubmitted_date { get; set; }
        public string submitted_by { get; set; }
        public string ccsubmitted_by { get; set; }
        public string region { get; set; }
        public string ccgroup_name { get; set; }
        public string ccmeeting_date { get; set; }
        public string ccmeting_time { get; set; }
        public string scheduled_date { get; set; }
        public string now_date { get; set; }
        public string cccompleted_date { get; set; }
        public string updated_by { get; set; }
        public string createdby { get; set; }
        public string warehouseapproval_gid { get; set; }
        public string headapproval_status { get; set; }
        public string initiate_flag { get; set; }
        public string headapproval_date { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditheadapproval_status { get; set; }
        public string creditgroup_name { get; set; }
        public string creditassigned_date { get; set; }
        public string creditassigned_by { get; set; }
        public string creditassigned_to { get; set; }
        public string rmquery_flag { get; set; }
        public string momupdated_by { get; set; }
        public string momupdated_date { get; set; }
    }

    public class MdlagrmstMobileNo : result
    {
        public string warehouse2contact_gid { get; set; }
        public string whatsapp_no { get; set; }
        public string warehouse2mobileno_gid { get; set; }
        public string institution_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public List<agrmstmobileno_list> agrmstmobileno_list { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opswarehouse_gid { get; set; }
        public string opswarehouse2contact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class agrmstmobileno_list
    {
        public string warehouse2contact_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_mobileno { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string warehouse2mobileno_gid { get; set; }
        public string opsinstitution2mobileno_gid { get; set; }
        public string opswarehouse2contact_gid { get; set; }
    }
    public class MdlagrmstEmailAddress : result
    {
        public string warehouse2email_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string institution_gid { get; set; }
        public List<agrmstemailaddress_list> agrmstemailaddress_list { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opswarehouse2email_gid { get; set; }
        public string opswarehouse_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class agrmstemailaddress_list
    {
        public string warehouse2email_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string email_address { get; set; }
        public string primary_emailaddress { get; set; }
        public string primary_status { get; set; }
        public string institution2email_gid { get; set; }
        public string opsinstitution2email_gid { get; set; }
        public string opswarehouse2email_gid { get; set; }
        public string opswarehouse_gid { get; set; }
    }


    public class WarehouseDocumentname : result
    {
        public List<WarehouseDocumentList> WarehouseDocumentList { get; set; }
    }
    public class WarehouseDocumentList
    {
        public string WarehouseDocument_name { get; set; }
        public string WarehouseDocument_path { get; set; }
        public string WarehouseDocument_gid { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string WarehouseDocument_type { get; set; }
        public string updated_date { get; set; }
        public string WarehouseDocument_title { get; set; }
        public string warehouse2hypothecation_gid { get; set; }
        public string warehouse2collateral_gid { get; set; }
        public string warehouse2loan_gid { get; set; }
        public string opswarehouse2hypothecation_gid { get; set; }
    }

    public class MdlagrmstGST : result
    {
        public string warehouse2branch_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_no { get; set; }
        public string gst_registered { get; set; }
        public warehouseGSTDetails[] GSTArray { get; set; }
        public List<agrmstgst_list> agrmstgst_list { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class agrmstgst_list
    {
        public string warehouse2branch_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_registered { get; set; }
        public string headoffice_status { get; set; }
        public string gst_no { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string state_code { get; set; }
        public string authentication_status { get; set; }
        public string returnfilling_status { get; set; }
        public string verification_status { get; set; }
    }
    public class MdlGSTwarehouseHeadOffice: result
    {
        public string warehouse2branch_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string employee_gid { get; set; }
    }
    public class warehouseGSTDetails
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

    public class MdlagrmstAddressDetails : result
    {
        public string address_type { get; set; }
        public string primary_address { get; set; }
        public string primary_status { get; set; }
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
        public string warehouse2address_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string group2address_gid { get; set; }
        public string group_gid { get; set; }
        public List<agrmstaddress_list> agrmstaddress_list { get; set; }
        public string opsinstitution2address_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class agrmstaddress_list
    {
        public string warehouse2address_gid { get; set; }
        public string group2address_gid { get; set; }
        public string address_type { get; set; }
        public string primary_address { get; set; }
        public string primary_status { get; set; }
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
        public string opsinstitution2address_gid { get; set; }
        public string opsgroup2address_gid { get; set; }
    }

    public class MdlagrmstBankDetails : result
    {
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string group2bank_gid { get; set; }
        public string group_gid { get; set; }
        public List<agrmstbank_list> agrmstbank_list { get; set; }
        public string opsgroup2bank_gid { get; set; }
        public string opsgroup_gid { get; set; }
        public string statusupdated_by { get; set; }
    }
    public class agrmstbank_list
    {
        public string group2bank_gid { get; set; }
        public string ifsc_code { get; set; }
        public string bank_accountno { get; set; }
        public string accountholder_name { get; set; }
        public string bank_name { get; set; }
        public string bank_branch { get; set; }
        public string opsgroup2bank_gid { get; set; }
    }


    public class warehouseCount
    {
        public string newwarehouse_count { get; set; }
        public string rejected_count { get; set; }
        public string hold_count { get; set; }
        public string ccapproved_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class AssignwarehouseCount
    {
        public string pending_count { get; set; }
        public string assigned_count { get; set; }
        public string submittedtocc_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }


    public class MdlAgrSectorcategory : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string valuechain_gid { get; set; }
        public string valuechain_name { get; set; }
        public string product_gid { get; set; }
        public List<varietyname_list> varietyname_list { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
    }
    public class Agrvarietyname_list : result
    {
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
    }
    public class MdlAgrPANAbsenceReason : result
    {
        public string contact_gid { get; set; }
        public List<panabsencereason_list> panabsencereason_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public List<contactpanabsencereason_list> contactpanabsencereason_list { get; set; }
    }
    public class Agrpanabsencereason_list
    {
        public string panabsencereason { get; set; }
        public bool check_status { get; set; }
    }
    public class Agrcontactpanabsencereason_list
    {
        public string panabsencereason { get; set; }
    }
    public class MdlAgrContactPANForm60 : result
    {
        public string sacontact2panform60_gid { get; set; }
        public string contact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string WarehouseDocument_name { get; set; }
        public string WarehouseDocument_path { get; set; }
        public List<contactpanform60_list> contactpanform60_list { get; set; }
    }

    public class Agrcontactpanform60_list
    {
        public string sacontact2panform60_gid { get; set; }
        public string contact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string Document_name { get; set; }
        public string Document_path { get; set; }
    }

    public class MdlWarehouseSectorcategory : result
    {
        public List<Warehousevarietyname_list> Warehousevarietyname_list { get; set; }
    }


    public class Warehousevarietyname_list : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string warehouse2commodity_gid { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string warehouse_gid { get; set; }
        public string hsn_code { get; set; }
    }

    public class Mdlagrmstspoc : result
    {
        public List<Warehousespoc_list> Warehousespoc_list { get; set; }

        public string warehouse2spoc_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string spoc_id { get; set; }
        public string spoc_name { get; set; }
        public string spocmobile_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }

    public class Warehousespoc_list : result
    {

        public string warehouse2spoc_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string spoc_id { get; set; }
        public string spoc_name { get; set; }
        public string spocmobile_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }


    public class WarehouseCountdtl
    {
        public string My_Warehouse { get; set; }
        public string ApprovalPending_Warehouse { get; set; }
        public string Approved_Warehouse { get; set; }
        public string Rejected_Warehouse { get; set; }
        public string PendingProduct_Warehouse { get; set; }
        public string PendingPMG_Warehouse { get; set; }
        public string pmgapproval { get; set; }
        public string productapproval { get; set; }

    }

    public static class getWarehouseStatusClass
    {
        public const string
             Approved = "A",
             Rejected = "R",
             Pending = "Y",
             New = "N"; 
    }

    public class Warehouseapprovaldtl
    {
        public List<Warehouseapproval_list> Warehouseapproval_list { get; set; }
    }

    public class Warehouseapproval_list : result
    { 
        public string approval_name { get; set; }
        public string approval_remarks { get; set; }
        public string approved_date { get; set; }
        public string approval_flag { get; set; } 

    }

    public class MdlEmployeeList
    {
        public List<employeedtl> employeedtl { get; set; }
    }

    public class employeedtl
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

    public class mdlwarehouseraisequery : result
    {
        public string warehouse2query_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string query_title { get; set; }
        public string description { get; set; }
        public string openquery_flag { get; set; }
        public string close_remarks { get; set; }
        public string pmgapproval_flag { get; set; }
        public string productapproval_flag { get; set;}
        public string query_from { get; set; }
        public List<warehouseraisequerylist> warehouseraisequerylist { get; set; }
    }

    public class warehouseraisequerylist
    {
        public string warehouse2query_gid { get; set; }
        public string warehouse_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string query_status { get; set; }
        public string close_remarks { get; set; }
        public string createdby_name { get; set; }
        public string query_from { get; set; }
    }
}