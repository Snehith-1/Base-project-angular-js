using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstLSA
    {
    }
    public static class getChargeType
    {
        public const string
             ProcessingFees = "1",
             DocumentCharges = "2",
             FieldVisitCharges = "3",
             AdhocFee = "4",
             TermLifeInsurance = "5",
             PersonalAccidentInsurance = "6";
    }
    public class MdlLSAMakerSummaryList
    {
        public List<MdlLSAMakerSummary> MdlLSAMakerSummary { get; set; }
    }
    public class MdlLSAMakerSummary : result
    {
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string customer_name { get; set; }
        public string ccapproved_date { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string overall_lsastatus { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }

    }


    public class MdlLSACheckerSummaryList
    {
        public List<MdlLSACheckerSummary> MdlLSACheckerSummary { get; set; }
    }
    public class MdlLSACheckerSummary : result
    {
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string customer_name { get; set; }
        public string ccapproved_date { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string overall_lsastatus { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
    }

    public class MdlLSAApproverSummaryList
    {
        public List<MdlLSAApproverSummary> MdlLSAApproverSummary { get; set; }
    }
    public class MdlLSAApproverSummary : result
    {
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string customer_name { get; set; }
        public string ccapproved_date { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string overall_lsastatus { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }        
        public string reinitiate_eligibleflag { get; set; }   
        public string lsa_refno { get; set; }
    }

    //LSA Report Summary
    public class MdlLSAReportSummaryList
    {
        public List<MdlLSAReportSummary> MdlLSAReportSummary { get; set; }
    }
    public class MdlLSAReportSummary : result
    {
        public string lsa_refno { get; set; }
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string customer_name { get; set; }
        public string ccapproved_date { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string overall_lsastatus { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
        public string reinitiate_eligibleflag { get; set; }
    }

    //LSA Report Excel
    public class MdlLSAReportExcel : result
    {
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string customer_name { get; set; }
        public string ccapproved_date { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string overall_lsastatus { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
        public string reinitiate_eligibleflag { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }


    public class CadLSACount : result
    {
        public string MakerPendingCount { get; set; }
        public string MakerFollowUpCount { get; set; }
        public string CheckerPendingCount { get; set; }
        public string CheckerFollowUpCount { get; set; }
        public string ApproverPendingCount { get; set; }
        public string CompletedCount { get; set; }
    }

    public class MdlGenerateLSAMakerSummaryList
    {
        public List<MdlGenerateLSAMakerSummary> MdlGenerateLSAMakerSummary { get; set; }
    }
    public class MdlGenerateLSAMakerSummary : result
    {
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string limitproduct_filled { get; set; }
        public string overall_lsastatus { get; set; }
    }

    public class limitandproductslist : result
    {
        public List<limitandproducts> limitandproducts { get; set; }
        public string total_limitreleased { get; set; }
        public string total_existinglimit { get; set; }
        public string maker_name { get; set; }
    }

    public class limitandproducts : result
    {
        public string limitproductinfodtl_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string interchangeability { get; set; }
        public string report_structure { get; set; }
        public string existing_limit { get; set; }
        public string limit_released { get; set; }
        public string limit_remarks { get; set; }
        public string odlim_amount { get; set; }
        public string odlim_condition { get; set; }
        public string application2sanction_gid { get; set; }
        public string application_gid { get; set; }
        public string report_structuregid { get; set; }
        public string limitinfo_remarks { get; set; }
        public string application2loan_gid { get; set; }
        public string producttype_gid { get; set; }
        public string product_type { get; set; }
        public string productsubtype_gid { get; set; }
        public string productsub_type { get; set; }
        public string loanfacility_amount { get; set; }
        public string documented_limit { get; set; }
        public string dateof_Expiry { get; set; }
        public DateTime dateofExpiry { get; set; }
        public string lsfilledlimitproduct { get; set; }
        public string lsatotalreleased_amount { get; set; }
        public string lsatotalreleased_approved { get; set; }

        
    }

    public class MdlBankAccountlist : result
    {
        public List<MdlBankAccount> MdlBankAccount { get; set; }
        public string lsoveralldisbursement_flag { get; set; }
        public string application_gid { get; set; }
        public string lsbankaccount_status { get; set; }    }

    public class MdlBankAccount : result
    {
        public string creditbankdtl_gid { get; set; }
        public string credit_gid { get; set; }
        public string lsabankaccdtl_gid { get; set; }
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
        public string accountopen_date1 { get; set; }
        public string disbursementaccount_status { get; set; }
        public string disbursementamount_flag { get; set; }
        public string lsdisbursement_flag { get; set; }
        public string lsoveralldisbursement_flag { get; set; }
        public string lsgeneratelsa_flag { get; set; }
        public string disbapplicantbankdtl_gid { get; set; }
          public string rmdisbursementrequest_gid { get; set; }
        public string confirmbankaccount_number { get; set; }
        public string initiated_by { get; set; }
        public DateTime accountopendate { get; set; }
        public List<lsabankacc_list> lsabankacc_list { get; set; }
        public List<credituploaddocument_list> credituploaddocument_list { get; set; }
  public List<disbapplicantbankacctdtl_list> disbapplicantbankacctdtl_list { get; set; }
    }

    public class lsabankacc_list
    {
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_number { get; set; }
    }

    public class lsauploaddocument : result
    {
        public List<lsauploaddocument_list> lsauploaddocument_list { get; set; }
    }
    public class lsauploaddocument_list
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string lsachequeleafdocument_gid { get; set; }
        public string lsabankaccdtl_gid { get; set; }
    }

    public class bankapplicationNameinfolist
    {
        public List<bankapplicationNameinfo> bankapplicationNameinfo { get; set; }
    }

    public class bankapplicationNameinfo
    {
        public string holder_name { get; set; }
        public string stakeholder_type { get; set; }
        public string holder_gid { get; set; }
        public string credit_gid { get; set; }
    }

    public class lsaFeecharges : result
    {
        public List<lsaFeecharges_list> lsaFeecharges_list { get; set; }
    }

    public class lsaFeecharges_list
    {
        public string lsafeescharge_gid { get; set; }
        public string processing_fee { get; set; }
        public string processing_collectiontype { get; set; }
        public string doc_charges { get; set; }
        public string doccharge_collectiontype { get; set; }
        public string fieldvisit_charge { get; set; }
        public string fieldvisit_collectiontype { get; set; }
        public string adhoc_fee { get; set; }
        public string adhoc_collectiontype { get; set; }
        public string life_insurance { get; set; }
        public string lifeinsurance_collectiontype { get; set; }
        public string acct_insurance { get; set; }
        public string total_collect { get; set; }
        public string total_deduct { get; set; }
        public string application_gid { get; set; }
        public string application2servicecharge_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string product_type { get; set; }
        public string producttype_gid { get; set; }
        public string acctinsurance_collectiontype { get; set; }
        public string processingfees_flag { get; set; }
        public string documentcharges_flag { get; set; }
        public string fieldvisitcharges_flag { get; set; }
        public string adhocfee_flag { get; set; }
        public string termlifeinsurance_flag { get; set; }
        public string personalaccidentinsurance { get; set; }
        public string lsachargestype_gid { get; set; }
        public string charge_typename { get; set; }    }

    public class UploadLSADocumentname : result
    {
        public List<UploadLSADocumentList> UploadLSADocumentList { get; set; }

    }

    public class UploadLSADocumentList
    {
        public string document_name { get; set; }
        public string document_gid { get; set; }
        public string document_path { get; set; }
        public string document_type { get; set; }
        public string uploaded_by { get; set; }
        public string created_date { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string migration_flag { get; set; }
    }

    public class LsaProductnamelist : result
    {
        public List<LsaProductname> LsaProductname { get; set; }
    }

    public class LsaProductname
    {
        public string application2loan_gid { get; set; }
        public string product_type { get; set; }
    }

    public class MdlBankNamelist : result
    {
        public List<MdlBankName> MdlBankName { get; set; }
    }

    public class MdlBankName
    {
        public string bankname_gid { get; set; }
        public string bank_name { get; set; }
    }

    public class lsafeescharge
    {
        public string lsafeescharge_gid { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string processing_fee { get; set; }
        public string processing_collectiontype { get; set; }
        public string doc_charges { get; set; }
        public string doccharge_collectiontype { get; set; }
        public string fieldvisit_charges { get; set; }
        public string fieldvisit_charges_collectiontype { get; set; }
        public string adhoc_fee { get; set; }
        public string adhoc_collectiontype { get; set; }
        public string life_insurance { get; set; }
        public string lifeinsurance_collectiontype { get; set; }
        public string acct_insurance { get; set; }
        public string total_collect { get; set; }
        public string total_deduct { get; set; }
        public string product_type { get; set; }
        public string producttype_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class lsachargetype_list : result
    {
        public List<lsadocumentationcharge> lsadocumentationcharge { get; set; }
    }

    public class lsadocumentationcharge : result
    {
        public string lsachargestype_gid { get; set; }
        public string lsafeescharge_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string charge_typeid { get; set; }
        public string charge_typename { get; set; }
        public string charge_applicable { get; set; }
        public string charge_remarks { get; set; }
        public string alreadycol_recoveredamount { get; set; }
        public string alreadycol_gstinpercentage { get; set; }
        public string alreadycol_gst { get; set; }
        public string alreadycol_totalamount { get; set; }
        public string collected_recovered_amount { get; set; }
        public string alreadycol_remainingamountcollected { get; set; }
        public string fieldvisit_charges_collectiontype { get; set; }
        public string alreadycol_Chequenodetails { get; set; }
        public string alreadycol_Cheque_date { get; set; }
        public string alreadycol_banknamegid { get; set; }
        public string alreadycol_bankname { get; set; }
        public string alreadycol_accountnumber { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public DateTime alreadycol_ChequeDate { get; set; }
        public DateTime tobecol_ChequeDate { get; set; }
        public string charge_amount { get; set; }
        public string tobecol_recoveredamount { get; set; }
        public string tobecol_gstinpercentage { get; set; }
        public string tobecol_gst { get; set; }
        public string tobecol_totalamount { get; set; }
        public string tobecol_remainingamountcollected { get; set; }
        public string tobecol_Chequenodetails { get; set; }
        public string tobecol_Cheque_date { get; set; }
        public string tobecol_banknamegid { get; set; }
        public string tobecol_bankname { get; set; }
        public string tobecol_accountnumber { get; set; }
    }

    public class lsaprocessingfees : result
    {
        public string lsaprocessingfees_gid { get; set; }
        public string lsafeescharge_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string recovered_status { get; set; }
        public string recovered_amount { get; set; }
        public string Chequeno_details { get; set; }
        public string Cheque_date { get; set; }
        public string processingfees_remarks { get; set; }
        public string bank_namegid { get; set; }
        public string bank_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class lsacollateraldetails : result
    {
        public string lsacollateraldetails_gid { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string loantype_gid { get; set; }
        public string loan_type { get; set; }
        public string product_type { get; set; }
        public string producttype_gid { get; set; }
        public string product_subtype { get; set; }
        public string productsubtype_gid { get; set; }
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_assessedon { get; set; }
        public string market_value { get; set; }
        public string marketvalue_assessedon { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralFSV_value { get; set; }
        public string forcedvalue_assessedon { get; set; }
        public string collateralobservation_summary { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public DateTime guideline_date { get; set; }
        public DateTime marketvalue_date { get; set; }
        public DateTime forcedvalue_date { get; set; }
    }

    public class lsacollateraldocument
    {
        public string lsacollateraldocument_gid { get; set; }
        public string lsacollateraldetails_gid { get; set; }
        public string application_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class lsacompliancecheck : result
    {
        public string lsacompliancecheckdetail_gid { get; set; }
        public string generatelsa_gid { get; set; }
        public string nachmandateform_held { get; set; }
        public string nachmandateform_heldremarks { get; set; }
        public string signmatching_nachform { get; set; }
        public string signmatching_nachformremarks { get; set; }
        public string namesign_kycmatching { get; set; }
        public string namesign_kycmatchingremarks { get; set; }
        public string escrowaccount_opened { get; set; }
        public string escrowaccount_openedremarks { get; set; }
        public string appropriate_stamping { get; set; }
        public string appropriate_stampingremarks { get; set; }
        public string rocfiling_initiated { get; set; }
        public string rocfiling_initiatedremarks { get; set; }
        public string cersai_initiated { get; set; }
        public string cersai_initiatedremarks { get; set; }
        public string alldeferralcovenant_captured { get; set; }
        public string allpredisbursement_stipulated { get; set; }
        public string maker_signaturename { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class branchlistdtl : result
    {
        public List<branch_list> branch_list { get; set; }
    }
    public class branch_list
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
    }

    public class lsa_doc
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
        public bool status { get; set; }
    }

    public class MdlLSAReinitiate : result
    {
        public string generatelsa_gid { get; set; }
        public string reinitatelsa_remarks { get; set; }
    }
}