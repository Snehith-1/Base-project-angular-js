﻿//using OfficeOpenXml.FormulaParsing.LexicalAnalysis.TokenSeparatorHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;

namespace ems.master.Models
{
    public class MdlSanctionDropDown : result
    {
        public string application_gid { get; set; }
        public List<sanctionrefnolist> sanctionrefnolist { get; set; }
    }
    public class sanctionrefnolist
    {
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string generatelsa_gid { get; set; }
        public string application_gid { get; set; }
    }
    public class MdlSancLsApprovalFlag : result
    {
        public string application_gid { get; set; }
        public string approver_approvalflag { get; set; }
    }
    public class disbursementuploaddocument : result
    {
        public string chequeleaf_name { get; set; }
        public string chequeleaf_path { get; set; }
        public string creditbankdtl_gid { get; set; }
        public string creditbankdtl2cheque_gid { get; set; }
        public List<disbursementuploaddocument_list> disbursementuploaddocument_list { get; set; }
    }
    public class disbursementuploaddocument_list
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
        public string rmdisbursementdocument_gid { get; set; }
        public string uploaded_by { get; set; }
        public string updated_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class RMsanctiondetails : result
    {
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_amount { get; set; }
        public string entity { get; set; }
        public string entity_gid { get; set; }
        public string application_gid { get; set; }
        public string sanctionfrom_date { get; set; }
        public string sanctiontill_date { get; set; }
        public string sanction_date { get; set; }
        public List<lsabankaccount_list> lsabankaccount_list { get; set; }
        public List<creditbankaccount_list> creditbankaccount_list { get; set; }
    }
    public class lsabankaccount_list : result
    {  
        public string sanction_refno { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application_gid { get; set; }
        public string name { get; set; }
        public string stakeholder_type { get; set; }
        public string bank_name { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string accountholder_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string disbursement_accountstatus { get; set; }
        public string accountopen_date { get; set; }
        public DateTime accountopendate { get; set; }
        public string accounttype_name { get; set; }
        public string lsabankaccdtl_gid { get; set; }
        public string rmdisbursement_status { get; set; }
    }
    public class creditbankaccount_list : result
    {
        public string sanction_refno { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application_gid { get; set; }
        public string name { get; set; }
        public string stakeholder_type { get; set; }
        public string bank_name { get; set; }
        public string branch_address { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string accountholder_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string disbursement_accountstatus { get; set; }
        public string accountopen_date { get; set; }
        public DateTime accountopendate { get; set; }
        public string creditbankdtl_gid { get; set; }
        public string rmdisbursement_status { get; set; }
    }
    public class MdlMstProductView : result
    {
        public string institution_urn { get; set; }
        public string individual_urn { get; set; }
        public string group_urn { get; set; }
        public List<creditbankaccount_list> creditbankaccount_list { get; set; }
    }
    public class MdlLSAuploadeddocument : result
    {
        public string chequeleaf_name { get; set; }
        public string chequeleaf_path { get; set; }
        public string creditbankdtl_gid { get; set; }
        public string creditbankdtl2cheque_gid { get; set; }
        public List<lsauploadeddocument_list> lsauploadeddocument_list { get; set; }
    }
    public class lsauploadeddocument_list
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string lsabankaccdtl_gid { get; set; }
        public string lsachequeleafdocument_gid { get; set; }
        public string uploaded_by { get; set; }
        public string updated_date { get; set; }
    }
    public class MdlDisbursementRequestAdd : result
    {
        public string rmdisbursementrequest_gid { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string application2loan_gid { get; set; }
        public string product_type { get; set; }
        public string processing_fees { get; set; }
        public string gst { get; set; }
        public string finance_charges { get; set; }
        public string od_amount { get; set; }
        public string escrow_payment { get; set; }
        public string nach_status { get; set; }
        public string remarks { get; set; }
        public string updated_person { get; set; }
        public string amounttobe_disbursed { get; set; }
        public string loandisbursement_date { get; set; }
        public string editloandisbursement_date { get; set; }
        public string disbursementassign_status { get; set; }
        public string disbursement_to { get; set; }
        public string lsareference_gid { get; set; }
        public string lsareference_number { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string creditopsdisbursement_amount { get; set; }
        public string processing_gst { get; set; }
        public string dispgstprocessing_fees { get; set; }
        public string additionalcharges_gst { get; set; }
        public string dispgstadditionfees_charges { get; set; }
        public string maker_remarks { get; set; }
        public string checker_remarks { get; set; }
        public string customer_urn { get; set; }
        public String customer_name { get; set; }
        public string vertical_name { get; set; }
        public string program_name { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string disbursementassignment_gid { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string rejected_remarks { get; set;}
        public string approval_status { get; set; }
        public string creditopsmaker_gid { get; set; }
        public string creditopschecker_gid { get; set; }
    }    
    public class MdlDisbursementRequest : result
    {        
        public List<disbursementrequest_list> disbursementrequest_list { get; set; }
        public List<disbursementuploaddocument_list> disbursementuploaddocument_list { get; set; }
        public List<disbursementassigned_list> disbursementassigned_list { get; set; }
        public List<disbursementmeker_list> disbursementmeker_list { get; set; }
        public List<disbursementmekerfollowup_list> disbursementmekerfollowup_list { get; set; }
        public List<disbursementchecker_list> disbursementchecker_list { get; set; }
        public List<disbursementcompleted_list> disbursementcompleted_list { get; set; }
        public List<disbursementrejected_list> disbursementrejected_list { get; set; }
        public string application_gid { get; set; }
        public List<disbursementuploadeddocument_list> disbursementuploadeddocument_list { get; set; }
    }
    public class disbursementuploadeddocument_list
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
        public string rmdisbursementdocument_gid { get; set; }
        public string uploaded_by { get; set; }
        public string updated_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class disbursementcompleted_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string checker_name { get; set; }
        public string checker_approveddate { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementrequest_code { get; set; }
        public string lsareference_gid { get; set; }
        public string lsareference_number { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
        public string customer_urn { get; set; }
    }
    public class disbursementchecker_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementrequest_code { get; set; }
        public string lsareference_gid { get; set; }
        public string lsareference_number { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
        public string customer_urn { get; set; }
    }
    public class disbursementmekerfollowup_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementrequest_code { get; set; }
        public string lsareference_gid { get; set; }
        public string lsareference_number { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
    }
    public class disbursementmeker_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string customer_urn { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string lsareference_gid { get; set; }
        public string lsareference_number { get; set; }
        public string disbursementrequest_code { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
    }
    public class disbursementrequest_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementassign_status { get; set; }
        public string generatelsa_gid { get; set; }
        public string disbursementrequest_code { get; set; }
        public string lsareference_number { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
    }
    public class disbursementassigned_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
       
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string application_gid { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementrequest_code { get; set; }
        public string lsareference_number { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
    }
    public class disbursementrejected_list
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sanction_refno { get; set; }
        public string vertical_name { get; set; }
        public string region { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementrequest_code { get; set; }
        public string lsareference_number { get; set; }
        public string product_type { get; set; }
        public string disbursement_to { get; set; }
        public string rejected_date { get; set; }
        public string rejected_by { get; set; }
    }
    public class MdlConfirmDisbursementAcct : result
    {
        public string bankdtl_gid { get; set; }
        public string disbursement_status { get; set; }
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string creditbankdtl_gid { get; set; }
        public string credit_gid { get; set; }
        public string lsabankaccdtl_gid { get; set; }
        public string disbursementaccount_status { get; set; }
        public string disbursement_amount { get; set; }
        public string disbursementamount_flag { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string initiated_by { get; set; }
        public List<disbursementamount_list> disbursementamount_list { get; set; }

        public string disbursementamount_status { get; set; }
        public string validation_amount { get; set; }
         public string disbursementsupplier_gid { get; set; }
        public string farmercontact_gid { get; set; }
        //public string validation_amount { get; set; }
    }

    public class disbursementamount_list
    {
        public string sumdisbursement_amount { get; set; }
    }
    public class MdlCreditOpsGroupDropDown : result
    {
        public string application_gid { get; set; }
        public List<creditOpsGrouplist> creditOpsGrouplist { get; set; }
    }
    public class creditOpsGrouplist
    {
        public string creditopsgroupmapping_gid { get; set; }
        public string creditopsgroup_name { get; set; }
    }
    public class MdlCreditOps2Heads : result
    {
        public string creditopsgroupmapping_gid { get; set; }
        public List<Creditops_maker> Creditops_maker { get; set; }
        public List<Creditops_checker> Creditops_checker { get; set; }
    }
    public class Creditops_maker : result
    {
        public string creditops2maker_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditops_checker : result
    {
        public string creditops2checker_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class MdlDisbursementAssignment : result
    {
        public string disbursementassignment_gid { get; set; }
        public string application_gid { get; set; }
        public string creditopsgroup_gid { get; set; }
        public string creditopsgroup_name { get; set; }
        public string creditopsmaker_gid { get; set; }
        public string creditopsmaker_name { get; set; }
        public string creditopschecker_gid { get; set; }
        public string creditopschecker_name { get; set; }
        public string creditopsapprover_gid { get; set; }
        public string creditopsapprover_name { get; set; }
        public string remarks { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
    }
    public class DisbursementAssignCount : result
    {
        public string pending_count { get; set; }
        public string assigned_count { get; set; }
        public Int16 lstotalcount { get; set; }
        public string makerpending_count { get; set; }
        public string makerfollowup_count { get; set; }
        public string checker_count { get; set; }
        public string approvedcompleted_count { get; set; }
        public string rejected_count { get; set; }
    }
    public class MdlDisbursementDtlView : result
    {
        public string application_gid { get; set; }
        public string processing_fees { get; set; }
        public string gst { get; set; }
        public string finance_charges { get; set; }
        public string od_amount { get; set; }
        public string escrow_payment { get; set; }
        public string nach_status { get; set; }
        public string remarks { get; set; }
        public string updated_person { get; set; }
        public string application2sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string application2loan_gid { get; set; }
        public string product_type { get; set; }
        public string amounttobe_disbursed { get; set; }
        public string loandisbursement_date { get; set; }
        public string creditopsgroup_gid { get; set; }
        public string creditopsgroup_name { get; set; }
        public string creditopsmaker_gid { get; set; }
        public string creditopsmaker_name { get; set; }
        public string creditopschecker_gid { get; set; }
        public string creditopschecker_name { get; set; }
        public string checker_approveddate { get; set; }
        public string maker_approveddate { get; set; }
        public string credit_gid { get; set; }
        public string customer_name { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string disbursement_amount { get; set; }
        public string disbursementamount_gid { get; set; }
        public string makerdisbursement_amount { get; set; }
        public string checkerdisbursement_amount { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string encoreintegration_status { get; set; }
        public string encore_accountid { get; set; }
        public string disbursementbookingencore_status { get; set; }
        
    }
    public class MdlDisbDocumentDeferral : result
    {
        public string vertical_gid { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementdocdeferral_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string disbursementdocdeferral_status { get; set; }
        public string remarks { get; set; }
        public string disbdocdeferralapprovalconfig_gid { get; set; }
        public List<disbdocumentdeferral_list> disbdocumentdeferral_list { get; set; }
        public List<disbdocdefapprovalconfig_list> disbdocdefapprovalconfig_list { get; set; }
        public List<disbdefdoclog_list> disbdefdoclog_list { get; set; }
        public string disbursementbookingencore_status { get; set; }
        
    }
    public class disbdocumentdeferral_list
    {
        public string disbursementdocdeferral_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementdocdeferral_status { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class disbdefdoclog_list
    {
        public string disbursementdocdeferralinactivelog_gid { get; set; }
        public string disbursementdocdeferral_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementdocdeferral_status { get; set; }
        public string wef_date { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class disbdocdefapprovalconfig_list
    {
        public string disbdocdeferralapprovalconfig_gid { get; set; }
        public string disbursementdocdeferral_gid { get; set; }
        public string vertical_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementdocdeferral_status { get; set; }
    }
    public class MdlDeviationApprovalDropDown : result
    {
        public List<deviationgroup_list> deviationgroup_list { get; set; }
        public List<deviationsubgroup_list> deviationsubgroup_list { get; set; }
        public List<deviationmember_list> deviationmember_list { get; set; }
        public List<deviationmanager_list> deviationmanager_list { get; set; }
    }
    public class deviationgroup_list
    {
        public string deviationapprovalgroup_gid { get; set; }
        public string deviationapprovalgroup_name { get; set; }
    }
    public class deviationsubgroup_list
    {
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
    }
    public class deviationmember_list
    {
        public string deviationapprovalgroupmember_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class deviationmanager_list
    {
        public string deviationapprovalgroupmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class MdlMstProductBuyer : result
    {
        public List<mstproductbuyer_list> mstproductbuyer_list { get; set; }
    }
    public class mstproductbuyer_list
    {
        public string application2buyer_gid { get; set; }
        public string application_gid { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string balance_limit { get; set; }
        public string bill_tenure { get; set; }
        public string margin { get; set; }
    }
    public class MdlFarmerIndividualSummary : result
    {
        public string rmdisbursementrequest_gid { get; set; }
        public List<farmerindividualsummary_list> farmerindividualsummary_list { get; set; }

        public string batchencorefindcust_status { get; set; }
    }
    public class farmerindividualsummary_list : result
    {
        public string farmercontact_gid { get; set; }
        public string individual_name { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string designation_name { get; set; }
        public string main_occupation { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string contactcoapplicant_gid { get; set; }
        public string disbursement_amount { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string bankaccount_number { get; set; }
        public string rmfarmerdisbursement_amount { get; set; }
        public string farmerdisbursement_amount { get; set; }
        public string creditopsdisbursement_amount { get; set; }
        public string creditopscheckerdisbursement_amount { get; set; }
        public string urn { get; set; }
        public string encore_accountid { get; set; }
        public string encoreaccintegration_status { get; set; }
        public string urn_status { get; set; }
        public string disbursementbookingencore_status { get; set; }
        public string encorefindcust_status { get; set; }
        
    }
    public class MdlFarmerIndividualDtlView : result
    { 
        public string farmercontact_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string pan_status { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_name { get; set; }
        public string individual_dob { get; set; }
        public string gender_name { get; set; }
        public string designation_name { get; set; }
        public string educationalqualification_name { get; set; }
        public string main_occupation { get; set; }
        public string annual_income { get; set; }
        public string monthly_income { get; set; }
        public string pep_status { get; set; }
        public string pepverified_date { get; set; }
        public string stakeholder_type { get; set; }
        public string maritalstatus_name { get; set; }
        public string father_name { get; set; }
        public string father_dob { get; set; }
        public string mother_name { get; set; }
        public string mother_dob { get; set; }
        public string spouse_name { get; set; }
        public string spouse_dob { get; set; }
        public string currentresidence_years { get; set; }
        public string branch_distance { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string fathernominee_status { get; set; }
        public string mothernominee_status { get; set; }
        public string spousenominee_status { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string micr_code { get; set; }
        public string bankaccount_number { get; set; }
        public string accountholder_name { get; set; }
        public string account_type { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string accountopen_date { get; set; }
        public string mobile_no { get; set; }
        public string mobileno_primarystatus { get; set; }
        public string whatsapp_no { get; set; }
        public string email_address { get; set; }
        public string emailprimary_status { get; set; }
        public string addresstype_name { get; set; }
        public string addressprimary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string creditopsdisbursement_amount { get; set; }
        public string creditopscheckerdisbursement_amount { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementsupplier_gid { get; set; }
    }
    public class disbsupplieruploaddocument : result
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string disbsupplierbankdocument_gid { get; set; }
        public string disbapplicantbankdtl_gid { get; set; }
        public List<disbsupplieruploaddocument_list> disbsupplieruploaddocument_list { get; set; }
        public List<disbapplicantuploaddocument_list> disbapplicantuploaddocument_list { get; set; }
    }
    public class disbapplicantuploaddocument_list
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string disbapplicantbankdocument_gid { get; set; }
        public string disbapplicantbankdtl_gid { get; set; }
        public string uploaded_by { get; set; }
        public string updated_date { get; set; }
    }
    public class disbsupplieruploaddocument_list
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string disbsupplierbankdocument_gid { get; set; }
        public string uploaded_by { get; set; }
        public string updated_date { get; set; }
    }
    public class MdlDisbSupplierBankAcct : result
    {
        public string application_gid { get; set; }
        public string supplier_name { get; set; }
        public string ifsc_code { get; set; }
        public string micr_code { get; set; }
        public string branch_address { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccount_number { get; set; }
        public string accountholder_name { get; set; }
        public string disbursement_amount { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string creditopsdisbursement_amount { get; set; }
        public string applicant_name { get; set; }
        public string disbapplicantbankdtl_gid { get; set; }
        public string disbursementaccount_status { get; set; }
        public string initiated_by { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public List<disbsupplierdtl_list> disbsupplierdtl_list { get; set; }
        public string creditopscheckerdisbursement_amount { get; set; }
        public List<disbapplicantbankacctdtl_list> disbapplicantbankacctdtl_list { get; set; }
        public List<disbapplicantuploaddocument_list> disbapplicantuploaddocument_list { get; set; }
        public string farmercontact_gid { get; set; }
        public string supplier_gid { get; set; }
        public string supplier2bank_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string jointaccount_status { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebook_status { get; set; }
        public string accountopen_date { get; set; }
    }
    public class disbapplicantbankacctdtl_list
    {
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_number { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string applicant_name { get; set; }
        public string accountholder_name { get; set; }
        public string disbursement_amount { get; set; }
        public string rmdisbursement_amount { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string creditopsdisbursement_amount { get; set; }
        public string creditopscheckerdisbursement_amount { get; set; }
        public string disbapplicantbankdtl_gid { get; set; }
        public string disbursementaccount_status { get; set; }
        public string initiated_by { get; set; }
        public string confirmbankaccount_number { get; set; }
    }
    public class disbsupplierdtl_list
    {
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_number { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string supplier_name { get; set; }
        public string accountholder_name { get; set; } 
        public string disbursement_amount { get; set; }
        public string rmdisbursement_amount { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string creditopsdisbursement_amount { get; set; }
        public string creditopscheckerdisbursement_amount { get; set; }
        public string disbursementbookingencore_status { get; set; }
        

    }
    public class MdlMstCoApplicantContact : result
    {
        public string contact_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string pan_status { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string individual_dob { get; set; }
        public string age { get; set; }
        public string gender_gid { get; set; }
        public string gender_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_name { get; set; }
        public string educationalqualification_gid { get; set; }
        public string educationalqualification_name { get; set; }
        public string main_occupation { get; set; }
        public string annual_income { get; set; }
        public string monthly_income { get; set; }
        public string pep_status { get; set; }
        public string pepverified_date { get; set; }
        public string stakeholdertype_gid { get; set; }
        public string stakeholdertype_name { get; set; }
        public string stakeholder_type { get; set; }
        public string user_type { get; set; }
        public string maritalstatus_gid { get; set; }
        public string maritalstatus_name { get; set; }
        public string father_firstname { get; set; }
        public string father_middlename { get; set; }
        public string father_lastname { get; set; }
        public string father_dob { get; set; }
        public string father_age { get; set; }
        public string mother_firstname { get; set; }
        public string mother_middlename { get; set; }
        public string mother_lastname { get; set; }
        public string mother_dob { get; set; }
        public string mother_age { get; set; }
        public string spouse_firstname { get; set; }
        public string spouse_middlename { get; set; }
        public string spouse_lastname { get; set; }
        public string spouse_dob { get; set; }
        public string spouse_age { get; set; }
        public string ownershiptype_gid { get; set; }
        public string ownershiptype_name { get; set; }
        public string propertyholder_gid { get; set; }
        public string propertyholder_name { get; set; }
        public string residencetype_gid { get; set; }
        public string residencetype_name { get; set; }
        public string currentresidence_years { get; set; }
        public string branch_distance { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string cicuploaddocument_name { get; set; }
        public string cicuploaddocument_path { get; set; }
        public string contact_status { get; set; }
        public string incometype_gid { get; set; }
        public string incometype_name { get; set; }
        public DateTime spousedob { get; set; }
        public DateTime motherdob { get; set; }
        public DateTime fatherdob { get; set; }
        public DateTime pepverifieddate { get; set; }
        public DateTime individualdob { get; set; }  
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string profile { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string fathernominee_status { get; set; }
        public string mothernominee_status { get; set; }
        public string spousenominee_status { get; set; }
        public string othernominee_status { get; set; }
        public string relationshiptype { get; set; }
        public string nomineefirst_name { get; set; }
        public string nominee_middlename { get; set; }
        public string nominee_lastname { get; set; }
        public string nominee_dob { get; set; }
        public string nominee_age { get; set; }
        public string totallandinacres { get; set; }
        public string cultivatedland { get; set; }
        public string previouscrop { get; set; }
        public string prposedcrop { get; set; }
        public string institution_gid { get; set; }
        public string institution_name { get; set; }
        public string opscontact_gid { get; set; }
        public string opsapplication_gid { get; set; }
        public string opsapplication_no { get; set; }
        public string statusupdated_by { get; set; }
        public List<coapplicantuploadindividualdoc_list> coapplicantuploadindividualdoc_list { get; set; }
        public List<string> panabsencereason_selectedlist { get; set; }
        public string farmercontact_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_no { get; set; }
        public string email_address { get; set; }
        public string emailprimary_status { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string addressprimary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string micr_code { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccount_number { get; set; }
        public string accountholder_name { get; set; }       
    }    
    public class MdlCoapplicantContactDocument : result
    {
        public string contact2document_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<coapplicantuploadindividualdoc_list> coapplicantuploadindividualdoc_list { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }

        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
    }
    public class coapplicantuploadindividualdoc_list
    {
        public string coapplicantcontact2document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
        public string migration_flag { get; set; }
    }
    public class MdlCoapplicantContactPANForm60 : result
    {
        public string coapplicantcontact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<Coapplicantcontactpanform60_list> Coapplicantcontactpanform60_list { get; set; }
    }

    public class Coapplicantcontactpanform60_list
    {
        public string coapplicantcontact2panform60_gid { get; set; }
        public string contact_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlCoApplicantDtlView : result
    {
        public string farmercontact_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string pan_status { get; set; }
        public string pan_no { get; set; }
        public string aadhar_no { get; set; }
        public string individual_name { get; set; }
        public string individual_dob { get; set; }
        public string gender_name { get; set; }
        public string designation_name { get; set; }
        public string educationalqualification_name { get; set; }
        public string main_occupation { get; set; }
        public string annual_income { get; set; }
        public string monthly_income { get; set; }
        public string pep_status { get; set; }
        public string pepverified_date { get; set; }
        public string stakeholder_type { get; set; }
        public string maritalstatus_name { get; set; }
        public string father_name { get; set; }
        public string father_dob { get; set; }
        public string mother_name { get; set; }
        public string mother_dob { get; set; }
        public string spouse_name { get; set; }
        public string spouse_dob { get; set; }
        public string currentresidence_years { get; set; }
        public string branch_distance { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string fathernominee_status { get; set; }
        public string mothernominee_status { get; set; }
        public string spousenominee_status { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string branch_address { get; set; }
        public string micr_code { get; set; }
        public string bankaccount_number { get; set; }
        public string accountholder_name { get; set; }
        public string account_type { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string accountopen_date { get; set; }
        public string mobile_no { get; set; }
        public string mobileno_primarystatus { get; set; }
        public string whatsapp_no { get; set; }
        public string email_address { get; set; }
        public string emailprimary_status { get; set; }
        public string addresstype_name { get; set; }
        public string addressprimary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List <Coapplicantcontactpanform60_list> Coapplicantcontactpanform60_list { get; set; }
        public List<coapplicantuploadindividualdoc_list> coapplicantuploadindividualdoc_list { get; set; }
        public List<coapplicantpanabsencereasons_list> coapplicantpanabsencereasons_list { get; set; }
    }
    public class coapplicantpanabsencereasons_list
    {
        public string coapplicantpanabsencereason { get; set; }
    }
    public class MdlMstLSARefNoDropDown
    {
        public List<LSARefNoDropDown_list> LSARefNoDropDown_list { get; set; }
    }
    public class LSARefNoDropDown_list
    {
        public string generatelsa_gid { get; set; }
        public string lsa_refno { get; set; }
    }
    public class MdlDisbursementReject : result
    {
        public string updated_person { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string rejected_remarks { get; set; }
        public string approval_status { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
    }
    public class MdlDisbursementDocument : result
    {
        public string vertical_gid { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string verticaldisbursementdocument_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string verticaldisbursementdocument_status { get; set; }
        public string remarks { get; set; }
        public string disbursementdocumentapprovalconfig_gid { get; set; }
        public List<disbursementdocument_list> disbursementdocument_list { get; set; }
        public List<disbursementdocumentapprovalconfig_list> disbursementdocumentapprovalconfig_list { get; set; }
        public List<disbursementdocumentlog_list> disbursementdocumentlog_list { get; set; }
    }
    public class disbursementdocument_list
    {
        public string verticaldisbursementdocument_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string verticaldisbursementdocument_status { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class disbursementdocumentlog_list
    {
        public string verticaldisbursementdocumentinactivelog_gid { get; set; }
        public string verticaldisbursementdocument_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string verticaldisbursementdocument_status { get; set; }
        public string wef_date { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class disbursementdocumentapprovalconfig_list
    {
        public string disbursementdocumentapprovalconfig_gid { get; set; }
        public string verticaldisbursementdocument_gid { get; set; }
        public string vertical_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string verticaldisbursementdocument_status { get; set; }
    }
    public class MdlDisbursementBankAccount : result
    {
        public string vertical_gid { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementbankaccount_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string disbursementbankaccount_status { get; set; }
        public string remarks { get; set; }
        public string disbursementbankaccountapprovalconfig_gid { get; set; }
        public List<disbursementbankaccount_list> disbursementbankaccount_list { get; set; }
        public List<disbursementbankaccountapprovalconfig_list> disbursementbankaccountapprovalconfig_list { get; set; }
        public List<disbursementbankaccountlog_list> disbursementbankaccountlog_list { get; set; }
    }
    public class disbursementbankaccount_list
    {
        public string disbursementbankaccount_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementbankaccount_status { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class disbursementbankaccountlog_list
    {
        public string disbursementbankaccountinactivelog_gid { get; set; }
        public string disbursementbankaccount_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementbankaccount_status { get; set; }
        public string wef_date { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class disbursementbankaccountapprovalconfig_list
    {
        public string disbursementbankaccountapprovalconfig_gid { get; set; }
        public string disbursementbankaccount_gid { get; set; }
        public string vertical_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementbankaccount_status { get; set; }
    }
    public class MdlDisbursementODBelow30 : result
    {
        public string vertical_gid { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow30_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string disbursementodbelow30_status { get; set; }
        public string remarks { get; set; }
        public string disbursementodbelow30approvalconfig_gid { get; set; }
        public List<disbursementodbelow30_list> disbursementodbelow30_list { get; set; }
        public List<disbursementodbelow30approvalconfig_list> disbursementodbelow30approvalconfig_list { get; set; }
        public List<disbursementodbelow30log_list> disbursementodbelow30log_list { get; set; }
    }
    public class disbursementodbelow30_list
    {
        public string disbursementodbelow30_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow30_status { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class disbursementodbelow30log_list
    {
        public string disbursementodbelow30inactivelog_gid { get; set; }
        public string disbursementodbelow30_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow30_status { get; set; }
        public string wef_date { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class disbursementodbelow30approvalconfig_list
    {
        public string disbursementodbelow30approvalconfig_gid { get; set; }
        public string disbursementodbelow30_gid { get; set; }
        public string vertical_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow30_status { get; set; }
    }
    public class MdlDisbursementODBelow90 : result
    {
        public string vertical_gid { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow90_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string disbursementodbelow90_status { get; set; }
        public string remarks { get; set; }
        public string disbursementodbelow90approvalconfig_gid { get; set; }
        public List<disbursementodbelow90_list> disbursementodbelow90_list { get; set; }
        public List<disbursementodbelow90approvalconfig_list> disbursementodbelow90approvalconfig_list { get; set; }
        public List<disbursementodbelow90log_list> disbursementodbelow90log_list { get; set; }
    }
    public class disbursementodbelow90_list
    {
        public string disbursementodbelow90_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow90_status { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class disbursementodbelow90log_list
    {
        public string disbursementodbelow90inactivelog_gid { get; set; }
        public string disbursementodbelow90_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow90_status { get; set; }
        public string wef_date { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class disbursementodbelow90approvalconfig_list
    {
        public string disbursementodbelow90approvalconfig_gid { get; set; }
        public string disbursementodbelow90_gid { get; set; }
        public string vertical_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string disbursementodbelow90_status { get; set; }
    }
    public class MdlPenalWaiver : result
    {
        public string vertical_gid { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string penalwaiver_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string penalwaiver_status { get; set; }
        public string remarks { get; set; }
        public string penalwaiverapprovalconfig_gid { get; set; }
        public List<penalwaiver_list> penalwaiver_list { get; set; }
        public List<penalwaiverapprovalconfig_list> penalwaiverapprovalconfig_list { get; set; }
        public List<penalwaiverlog_list> penalwaiverlog_list { get; set; }
    }
    public class penalwaiver_list
    {
        public string penalwaiver_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string penalwaiver_status { get; set; }
        public string wef_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class penalwaiverlog_list
    {
        public string penalwaiverinactivelog_gid { get; set; }
        public string penalwaiver_gid { get; set; }
        public string vertical_gid { get; set; }
        public string customer_type { get; set; }
        public string penalwaiver_status { get; set; }
        public string wef_date { get; set; }
        public string remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class penalwaiverapprovalconfig_list
    {
        public string penalwaiverapprovalconfig_gid { get; set; }
        public string penalwaiver_gid { get; set; }
        public string vertical_gid { get; set; }
        public string group_gid { get; set; }
        public string group_name { get; set; }
        public string subgroup_gid { get; set; }
        public string subgroup_name { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
        public string member_gid { get; set; }
        public string member_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string wef_date { get; set; }
        public string customer_type { get; set; }
        public string penalwaiver_status { get; set; }
    }
    public class MdlDisbCreditOpsApplicantBankAcct : result
    {
        public string application_gid { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string disbursementamount_gid { get; set; }
        public string makerdisbursement_amount { get; set; }
        public string checkerdisbursement_amount { get; set; }
        public string disbursement_amount { get; set; }
        public string disbursementsupplier_gid { get; set; }
        public string farmercontact_gid { get; set; }
    }

    //instrument Encore Integration - Payment Option Dropdown 
    public class mdlinstrument : result
    {
        public List<instrumentlist> instrumentlist { get; set; }
    }
    public class instrumentlist
    {
        public string instrument_gid { get; set; }
        public string instrument { get; set; }
    }
    public class MdlMstSupplierName : result
    {
        public List<dispsupplier_list> dispsupplier_list { get; set; }
    }
    public class dispsupplier_list
    {
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
    }
    public class MdlMstSupplierIfscCode : result
    {
        public List<dispsupplierifsc_list> dispsupplierifsc_list { get; set; }
    }
    public class dispsupplierifsc_list
    {
        public string supplier2bank_gid { get; set; }
        public string ifsc_code { get; set; }
    }
    public class MdlDispSuplBankAcctDtl : result
    {
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string bankaccount_name { get; set; }
        public string micr_code { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string joinaccount_status { get; set; }
        public string joinaccount_name { get; set; }
        public string chequebook_status { get; set; }
        public string accountopen_date { get; set; }
    }
}