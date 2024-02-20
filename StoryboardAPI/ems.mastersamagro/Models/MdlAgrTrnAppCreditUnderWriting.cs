using System;
using System.Collections.Generic;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This models will store values to add, edit, view datas in credit stages.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>

    public class MdlMstAppCreditUnderWriting : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string application_status { get; set; }
        public string proceed_flag { get; set; }
        public string applicant_type { get; set; }
        public string economical_flag { get; set; }
        public string institution_gid { get; set; }
        public string contact_gid { get; set; }
        public string group_gid { get; set; }
        public string startupasofloansanction_date { get; set; }
        public string occupation_gid { get; set; }
        public string occupation { get; set; }
        public string lineofactivity_gid { get; set; }
        public string lineofactivity { get; set; }
        public string bsrcode_gid { get; set; }
        public string bsrcode { get; set; }
        public string pslcategory_gid { get; set; }
        public string pslcategory { get; set; }
        public string weakersection_gid { get; set; }
        public string weakersection { get; set; }
        public string pslpurpose_gid { get; set; }
        public string pslpurpose { get; set; }
        public string totalsanction_financialinstitution { get; set; }
        public string pslsanction_limit { get; set; }
        public string natureofentity_gid { get; set; }
        public string natureofentity { get; set; }
        public string indulgeinmarketing_activity { get; set; }
        public string plantandmachineryinvestment_gid { get; set; }
        public string plantandmachineryinvestment { get; set; }
        public string turnover_gid { get; set; }
        public string turnover { get; set; }
        public string msmeclassification_gid { get; set; }
        public string msmeclassification { get; set; }
        public string loansanction_date { get; set; }
        public string entityincorporation_date { get; set; }
        public DateTime loansanctiondate { get; set; }
        public DateTime entityincorporationdate { get; set; }
        public string hq_metropolitancity { get; set; }
        public string clientdtl_gid { get; set; }
        public string clientdtl_name { get; set; }
        public string psltagging_flag { get; set; }
        public string company_name { get; set; }
        public string stakeholder_type { get; set; }
        public string urn_status { get; set; }
        public string urn { get; set; }
        public string individual_name { get; set; }
        public string group_type { get; set; }
        public string group_name { get; set; }
        public List<institutionsocialtrade_list> institutionsocialtrade_list { get; set; }
        public List<individualsocialtrade_list> individualsocialtrade_list { get; set; }
        public List<groupsocialtrade_list> groupsocialtrade_list { get; set; }
        public List<institutionpsltagging_list> institutionpsltagging_list { get; set; }
        public List<individualpsltagging_list> individualpsltagging_list { get; set; }
        public List<grouppsltagging_list> grouppsltagging_list { get; set; }
        public List<supplier_list> supplier_list { get; set; }
    }
    public class institutionpsltagging_list : result
    {
        public string startupasofloansanction_date { get; set; }
        public string occupation { get; set; }
        public string lineofactivity { get; set; }
        public string bsrcode { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class individualpsltagging_list : result
    {
        public string startupasofloansanction_date { get; set; }
        public string occupation { get; set; }
        public string lineofactivity { get; set; }
        public string bsrcode { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class grouppsltagging_list : result
    {
        public string startupasofloansanction_date { get; set; }
        public string occupation { get; set; }
        public string lineofactivity { get; set; }
        public string bsrcode { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class MdlMstCUWGeneticCode : result
    {
        public string creditgeneticcode_gid { get; set; }
        public string application_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string geneticcode_status { get; set; }
        public string geneticcode_remarks { get; set; }
        public string geneticcode_gid { get; set; }
        public string credit_gid { get; set; }
        public List<mstcuwgeneticcode_list> mstcuwgeneticcode_list { get; set; }
    }
    public class mstcuwgeneticcode_list
    {
        public string creditgeneticcode_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string geneticcode_status { get; set; }
        public string geneticcode_remarks { get; set; }
        public string geneticcode_gid { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class institutionsocialtrade_list : result
    {
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
    }
    public class individualsocialtrade_list : result
    {
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
    }
    public class groupsocialtrade_list : result
    {
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
    }
    public class MdlPSLDropDown : result
    {
        public List<occupation_list> occupation_list { get; set; }
        public List<lineofactivity_list> lineofactivity_list { get; set; }
        public List<bsrcode_list> bsrcode_list { get; set; }
        public List<pslcategorylist> pslcategorylist { get; set; }
        public List<weakersectionlist> weakersectionlist { get; set; }
        public List<pslpurpose_list> pslpurpose_list { get; set; }
        public List<natureofentitylist> natureofentitylist { get; set; }
        public List<turnoverlist> turnoverlist { get; set; }
        public List<msmelist> msmelist { get; set; }
        public List<investmentlist> investmentlist { get; set; }
        public List<lendertype_list> lendertype_list { get; set; }
        public List<creditaccountclassification_list> creditaccountclassification_list { get; set; }
        public List<creditinstalmentfrequency_list> creditinstalmentfrequency_list { get; set; }
        public List<fundedtypeindicator_list> fundedtypeindicator_list { get; set; }
        public List<creditunderwritingfacilitytype_list> creditunderwritingfacilitytype_list { get; set; }
        public List<bankdtl_list> bankdtl_list { get; set; }
        public List<clientdetail_list> clientdetail_list { get; set; }
    }
    public class clientdetail_list
    {
        public string clientdtl_gid { get; set; }
        public string clientdtl_name { get; set; }
    }
    public class bankdtl_list
    {
        public string bankdtl_gid { get; set; }
        public string bankdtl_name { get; set; }
    }
    public class creditunderwritingfacilitytype_list
    {
        public string creditunderwritingfacilitytype_gid { get; set; }
        public string credit_underwriting_facility_type { get; set; }
    }
    public class fundedtypeindicator_list
    {
        public string fundedtypeindicator_gid { get; set; }
        public string fundedtypeindicator_name { get; set; }
    }
    public class creditinstalmentfrequency_list
    {
        public string creditinstalmentfrequency_gid { get; set; }
        public string creditinstalmentfrequency_name { get; set; }
    }
    public class creditaccountclassification_list
    {
        public string creditaccountclassification_gid { get; set; }
        public string creditaccountclassification_name { get; set; }
    }
    public class lendertype_list
    {
        public string lendertype_gid { get; set; }
        public string lendertype_name { get; set; }
    }
    public class occupation_list
    {
        public string occupation_gid { get; set; }
        public string occupation_name { get; set; }
    }
    public class lineofactivity_list
    {
        public string lineofactivity_gid { get; set; }
        public string lineof_activity { get; set; }
    }
    public class bsrcode_list
    {
        public string bsrcode_gid { get; set; }
        public string bsr_code { get; set; }
    }
    public class pslcategorylist
    {
        public string pslcategory_gid { get; set; }
        public string psl_category { get; set; }
    }
    public class weakersectionlist
    {
        public string weakersection_gid { get; set; }
        public string weaker_section { get; set; }
    }
    public class pslpurpose_list
    {
        public string pslpurpose_gid { get; set; }
        public string psl_purpose { get; set; }
    }
    public class natureofentitylist
    {
        public string natureofentity_gid { get; set; }
        public string natureofentity_name { get; set; }
    }
    public class turnoverlist
    {
        public string turnover_gid { get; set; }
        public string turnover_name { get; set; }
    }
    public class msmelist
    {
        public string msme_gid { get; set; }
        public string msme_name { get; set; }
    }
    public class investmentlist
    {
        public string investment_gid { get; set; }
        public string investment_name { get; set; }
    }
    public class MdlMstCUWExistingBankFacility : result
    {
        public string existingbankfacility_gid { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string facilitysanctioned_on { get; set; }
        public string fundedtypeindicator_gid { get; set; }
        public string fundedtypeindicator_name { get; set; }
        public string sanctioned_limit { get; set; }
        public string instalmentfrequency_gid { get; set; }
        public string instalmentfrequency_name { get; set; }
        public string instalment_amount { get; set; }
        public string outstanding_amount { get; set; }
        public string record_date { get; set; }
        public string overdue_amount { get; set; }
        public string overdue_dpd { get; set; }
        public string accountclassification_gid { get; set; }
        public string account_classification { get; set; }
        public string remarks { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string facilitytype_gid { get; set; }
        public string facility_type { get; set; }
        public DateTime facilitysanctionedon { get; set; }
        public DateTime recorddate { get; set; }
        public List<cuwexistingbankfacility_list> cuwexistingbankfacility_list { get; set; }
    }
    public class cuwexistingbankfacility_list
    {
        public string existingbankfacility_gid { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
        public string facilitysanctioned_on { get; set; }
        public string fundedtypeindicator_gid { get; set; }
        public string fundedtypeindicator_name { get; set; }
        public string sanctioned_limit { get; set; }
        public string instalmentfrequency_gid { get; set; }
        public string instalmentfrequency_name { get; set; }
        public string instalment_amount { get; set; }
        public string outstanding_amount { get; set; }
        public string record_date { get; set; }
        public string overdue_amount { get; set; }
        public string overdue_dpd { get; set; }
        public string accountclassification_gid { get; set; }
        public string account_classification { get; set; }
        public string remarks { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string facilitytype_gid { get; set; }
        public string facility_type { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class MdlMstCUWRepaymentTrack : result
    {
        public string creditrepaymentdtl_gid { get; set; }
        public string lendertype_gid { get; set; }
        public string lender_type { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string nbfc_name { get; set; }
        public string branch_name { get; set; }
        public string facility_type { get; set; }
        public string sanctionreference_id { get; set; }
        public string sanctioned_on { get; set; }
        public DateTime sanctionedon { get; set; }
        public string sanction_amount { get; set; }
        public string accountstatus_on { get; set; }
        public DateTime accountstatuson { get; set; }
        public string currentoutsatnding_amount { get; set; }
        public string instalment_frequency { get; set; }
        public string instalment_amount { get; set; }
        public string demanddue_date { get; set; }
        public DateTime demandduedate { get; set; }
        public string oringinaltenure_year { get; set; }
        public string oringinaltenure_month { get; set; }
        public string oringinaltenure_days { get; set; }
        public string balancetenure_year { get; set; }
        public string balancetenure_month { get; set; }
        public string balancetenure_days { get; set; }
        public string accountclassification_gid { get; set; }
        public string account_classification { get; set; }
        public string overdue_amount { get; set; }
        public string numberofdays_overdue { get; set; }
        public string remarks { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public List<cuwrepaymenttrack_list> cuwrepaymenttrack_list { get; set; }
    }
    public class cuwrepaymenttrack_list
    {
        public string creditrepaymentdtl_gid { get; set; }
        public string lendertype_gid { get; set; }
        public string lender_type { get; set; }
        public string ifsc_code { get; set; }
        public string bank_name { get; set; }
        public string nbfc_name { get; set; }
        public string branch_name { get; set; }
        public string facility_type { get; set; }
        public string sanctionreference_id { get; set; }
        public string sanctioned_on { get; set; }
        public string sanction_amount { get; set; }
        public string accountstatus_on { get; set; }
        public string currentoutsatnding_amount { get; set; }
        public string instalment_frequency { get; set; }
        public string instalment_amount { get; set; }
        public string demanddue_date { get; set; }
        public string oringinaltenure_year { get; set; }
        public string oringinaltenure_month { get; set; }
        public string oringinaltenure_days { get; set; }
        public string balancetenure_year { get; set; }
        public string balancetenure_month { get; set; }
        public string balancetenure_days { get; set; }
        public string accountclassification_gid { get; set; }
        public string account_classification { get; set; }
        public string overdue_amount { get; set; }
        public string numberofdays_overdue { get; set; }
        public string remarks { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class MdlMstSupplier : result
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
        public string relationship_vintage_year { get; set; }
        public string relationship_vintage_month { get; set; }
        public string start_date { get; set; }
        public DateTime startdate { get; set; }
        public string end_date { get; set; }
        public DateTime enddate { get; set; }
        public string purchase_amount { get; set; }
        public string bankdebit_amount { get; set; }
        public string relationship_supplier { get; set; }
        public string creditsupplier_gid { get; set; }
        public List<supplier_list> supplier_list { get; set; }
    }
    public class supplier_list
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
        public string supplier_ref_no { get; set; }
        public string relationship_vintage_year { get; set; }
        public string relationship_vintage_month { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string purchase_amount { get; set; }
        public string bankdebit_amount { get; set; }
        public string relationship_supplier { get; set; }
        public string creditsupplier_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class MdlMstCreditBuyer : result
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string relationship_vintage_year { get; set; }
        public string relationship_vintage_month { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string purchase_amount { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string creditbuyer_gid { get; set; }
        public string balance_limit { get; set; }
        public string top_buyer { get; set; }
        public string bill_tenuredays { get; set; }
        public string margin { get; set; }
        public string bankcredit_date { get; set; }
        public string bankcredit_value { get; set; }
        public string source_deduction { get; set; }
        public string relationship_borrower { get; set; }
        public string enduse_monitoring { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public DateTime bankcreditdate { get; set; }
        public List<creditbuyer_list> creditbuyer_list { get; set; }
    }
    public class creditbuyer_list
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string buyer_gid { get; set; }
        public string buyer_name { get; set; }
        public string relationship_vintage_year { get; set; }
        public string relationship_vintage_month { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string purchase_amount { get; set; }
        public string buyer_limit { get; set; }
        public string availed_limit { get; set; }
        public string creditbuyer_gid { get; set; }
        public string balance_limit { get; set; }
        public string top_buyer { get; set; }
        public string bill_tenuredays { get; set; }
        public string margin { get; set; }
        public string bankcredit_date { get; set; }
        public string bankcredit_value { get; set; }
        public string source_deduction { get; set; }
        public string relationship_borrower { get; set; }
        public string enduse_monitoring { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string application2buyer_gid { get; set; }
    }
    public class MdlMstCreditObservation : result
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string creditpolicy_gid { get; set; }
        public string credit_policy { get; set; }
        public string complied_status { get; set; }
        public string observation { get; set; }
        public string creditobservation_gid { get; set; }
        public List<CreditObservation_list> CreditObservation_list { get; set; }
    }
    public class CreditObservation_list
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string creditpolicy_gid { get; set; }
        public string credit_policy { get; set; }
        public string complied_status { get; set; }
        public string observation { get; set; }
        public string creditobservation_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class MdlCreditBankAcc : result
    {
        public string creditbankdtl_gid { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebook_status { get; set; }
        public string accountopen_date { get; set; }
        public DateTime accountopendate { get; set; }
        public List<creditbankacc_list> creditbankacc_list { get; set; }
        public List<credituploaddocument_list> credituploaddocument_list { get; set; }
    }
    public class creditbankacc_list
    {
        public string creditbankdtl_gid { get; set; }
        public string buyer_gid { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string bank_address { get; set; }
        public string micr_code { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string accountholder_name { get; set; }
        public string accounttype_name { get; set; }
        public string joint_account { get; set; }
        public string jointaccountholder_name { get; set; }
        public string chequebookfacility_available { get; set; }
        public string accountopen_date { get; set; }
        public string joinaccount_status { get; set; }
        public string joinaccount_name { get; set; }
        public string chequebook_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
    }
    public class credituploaddocument : result
    {
        public string chequeleaf_name { get; set; }
        public string chequeleaf_path { get; set; }
        public string creditbankdtl_gid { get; set; }
        public string creditbankdtl2cheque_gid { get; set; }
        public List<credituploaddocument_list> credituploaddocument_list { get; set; }
    }
    public class credituploaddocument_list
    {
        public string chequeleaf_name { get; set; }
        public string document_title { get; set; }
        public string chequeleaf_path { get; set; }
        public string creditbankdtl_gid { get; set; }
        public string creditbankdtl2cheque_gid { get; set; }
        public string uploaded_by { get; set; }
        public string updated_date { get; set; }
    }
    public class MdlMstExistingRemarks : result
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string Existingbank_remarks { get; set; }
        public string existingbankfacility_gid { get; set; }
    }
    public class MdlMstRepaymentRemarks : result
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string Repayment_remarks { get; set; }
        public string creditrepaymentdtl_gid { get; set; }
    }   
    public class BankStatementExportExcel : result
    {
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }
    public class MdlMstCUWBankStatement : result
    {       
        public List<BankStatement_list> BankStatement_list { get; set; }
    }
    public class BankStatement_list
    {
        public string bankstatementanalysis_gid { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string total_debits { get; set; }
        public string total_credits { get; set; }
        public string accttransfer_debits { get; set; }
        public string accttransfer_credits { get; set; }
        public string loans_repayment { get; set; }
        public string cash_deposits { get; set; }
        public string purchase_payments { get; set; }
        public string sales_receipets { get; set; }
        public string chequeneft_inward { get; set; }
        public string chequeneft_outward { get; set; }
        public string overdrawings_cc { get; set; }
        public string sales_gst { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    // P & L Template 1

    public class MdlMstProfitLoss : result
    {
        public List<profitloss_list> profitloss_list { get; set; }
    }
    public class profitloss_list
    {
        public string audited { get; set; }
        public string domestic_sales { get; set; }
        public string export_sales { get; set; }
        public string totalgross_sales { get; set; }
        public string excise_duty { get; set; }
        public string otheroperating_income { get; set; }
        public string other_income { get; set; }
        public string net_sales { get; set; }
        public string increasenet_sales { get; set; }
        public string import_rawmaterial { get; set; }
        public string indigenous_rawmaterial { get; set; }
        public string import_spares { get; set; }
        public string indigenous_spares { get; set; }
        public string power_fuel { get; set; }
        public string direct_labour { get; set; }
        public string othersoperating_expenses { get; set; }
        public string depreciation { get; set; }
        public string repair_maintenance { get; set; }
        public string rent { get; set; }
        public string otherdirect_cost { get; set; }
        public string totalcost_sales { get; set; }
        public string openingstock_progress { get; set; }
        public string closingstock_progress { get; set; }
        public string costof_production { get; set; }
        public string copnet_sales { get; set; }
        public string openingstock_finishedgoods { get; set; }
        public string closingstock_finishedgoods { get; set; }
        public string costof_sales { get; set; }
        public string cosnet_sales { get; set; }
        public string pbit { get; set; }
        public string pbitnet_sales { get; set; }
        public string interest_finance_charges { get; set; }
        public string interest_financenet_sales { get; set; }
        public string pbt { get; set; }
        public string pbtnet_sales { get; set; }
        public string interest_earned { get; set; }
        public string mics_receipts { get; set; }
        public string divdend { get; set; }
        public string profitsales_invetments { get; set; }
        public string exchange_gain { get; set; }
        public string total_nonoperative_income { get; set; }
        public string wrieoff_provision { get; set; }
        public string proir_year { get; set; }
        public string baddebts_written_off { get; set; }
        public string othernoncash_expenses { get; set; }
        public string othernonoperating_exoeses { get; set; }
        public string total_nonoperative_expenses { get; set; }
        public string profitbefore_tax { get; set; }
        public string current_tax { get; set; }
        public string deferred_tax { get; set; }
        public string pat { get; set; }
        public string net_profitloss { get; set; }
        public string amount { get; set; }
        public string rate { get; set; }
        public string template_name { get; set; }
        public string retained_profit { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string sgam_expenses { get; set; }
        public string allfiguresin_inr { get; set; }
    }
    public class MdlMstFSASummary : result
    {
        public List<MstFSASummary_list> MstFSASummary_list { get; set; }
    }
    public class MstFSASummary_list
    {
        public string template_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string credit_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string application_gid { get; set; }
    }

    //Balance Sheet Template-1

    public class MdlMstCUWBalancesheettemplate1 : result
    {
        public List<creditbalancesheettemplate1_list> creditbalancesheettemplate1_list { get; set; }
    }

    public class creditbalancesheettemplate1_list
    {
        public string creditbalancesheet_gid { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string template_type { get; set; }
        public string allfiguresin_inr { get; set; }
        public string audit_type { get; set; }
        public string applicant_bank { get; set; }
        public string other_banks { get; set; }
        public string billpurchased_disc { get; set; }
        public string wcb_total { get; set; }
        public string shortterm_borrowings { get; set; }
        public string sundrycreditors_acceptances { get; set; }
        public string advancesreceived_customers { get; set; }
        public string provision_taxation { get; set; }
        public string dividend_taxthereon { get; set; }
        public string other_provisions { get; set; }
        public string instalmentsof_tl { get; set; }
        public string othercurrent_liabilitiesin1year { get; set; }
        public string dues_directors { get; set; }
        public string creditors_expenses { get; set; }
        public string unclaimed_debentures { get; set; }
        public string interestaccrued_loans { get; set; }
        public string othercurrent_liabilities { get; set; }
        public string total { get; set; }
        public string totalcurrent_liabilities { get; set; }
        public string debenturesin_1year { get; set; }
        public string prefshares_after1Year { get; set; }
        public string term_loans { get; set; }
        public string fixed_deposits { get; set; }
        public string otherterm_liabilities { get; set; }
        public string deferredtax_liability { get; set; }
        public string creditorscapital_expenses { get; set; }
        public string totallongterm_liabilities { get; set; }
        public string ordinaryshare_capital { get; set; }
        public string capitalredemption_reserve { get; set; }
        public string general_reserve { get; set; }
        public string investmentallowance_reserve { get; set; }
        public string surplusdeficitpandl_account { get; set; }
        public string prefsharecapital_debenture { get; set; }
        public string revaluation_reserve { get; set; }
        public string other_reserves { get; set; }
        public string capital_reserve { get; set; }
        public string sharepremium_account { get; set; }
        public string advancetowards_capital { get; set; }
        public string depositfriends_relatives { get; set; }
        public string totalnet_worth { get; set; }
        public string total_loabilities { get; set; }
        public string claimscmpyack_debts { get; set; }
        public string arrearsofsalary_susemployee { get; set; }
        public string estimated_capitalacct { get; set; }
        public string guaranteesbankers_company { get; set; }
        public string current_assets { get; set; }
        public string cash_balance { get; set; }
        public string bank_balance { get; set; }
        public string Govtandother_securities { get; set; }
        public string fixeddeposits_banks { get; set; }
        public string domestic_receivables { get; set; }
        public string export_receivables { get; set; }
        public string instalmentsdeferred_receivables { get; set; }
        public string rawmaterials_indigenous { get; set; }
        public string rawmaterial_imported { get; set; }
        public string stock_inprocess { get; set; }
        public string finished_goods { get; set; }
        public string other_sparesindigenous { get; set; }
        public string other_sparesindimported { get; set; }
        public string advances_suppliers { get; set; }
        public string advancepayment_tax { get; set; }
        public string othercurrent_asset { get; set; }
        public string advances_recoverable { get; set; }
        public string interestservice_receivable { get; set; }
        public string incomeaccrued_investments { get; set; }
        public string others { get; set; }
        public string totalcurrent_assets { get; set; }
        public string land_building { get; set; }
        public string computers { get; set; }
        public string plant_machinery { get; set; }
        public string furnitures_fixtures { get; set; }
        public string otherfixed_assets { get; set; }
        public string capitalwip_machniery { get; set; }
        public string less_depriciation { get; set; }
        public string net_block { get; set; }
        public string investmentssubsidiary_compy { get; set; }
        public string other_investments { get; set; }
        public string advancesto_subsidiaries { get; set; }
        public string receivables_sixmonths { get; set; }
        public string marginmoney_banks { get; set; }
        public string defferredrevenue_expenditure { get; set; }
        public string deposits_departments { get; set; }
        public string nonconsumablestores_spares { get; set; }
        public string othernoncurrent_assets { get; set; }
        public string total_othernoncurrentassets { get; set; }
        public string miscellaneous_expenses { get; set; }
        public string patents_good { get; set; }
        public string debitbalancepandl_account { get; set; }
        public string unsecureddebtors_doubtful { get; set; }
        public string total_intangibleassets { get; set; }
        public string total_assets { get; set; }
        public string totalliabilities_totalasset { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }

    }

    //Balance Sheet Template-2

    public class MdlMstCUWBalancesheettemplate2 : result
    {
        public List<creditbalancesheettemplate2_list> creditbalancesheettemplate2_list { get; set; }
    }
    public class creditbalancesheettemplate2_list
    {
        public string creditbalancesheet2_gid { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }
        public string template_type { get; set; }
        public string allfiguresin_inr { get; set; }
        public string audit_type { get; set; }
        public string demand_deposits { get; set; }
        public string term_deposits { get; set; }
        public string other_deposits { get; set; }
        public string deposits_total { get; set; }
        public string shortterm_borrowings { get; set; }
        public string term_borrowings { get; set; }
        public string other_borrowings { get; set; }
        public string borrowings_total { get; set; }
        public string provision_taxation { get; set; }
        public string provisionstandard_assets { get; set; }
        public string provisionnon_assets { get; set; }
        public string interestaccrued_loans { get; set; }
        public string creditors_expenses { get; set; }
        public string other_provisions { get; set; }
        public string othercurrent_liabilities { get; set; }
        public string deferredtax_liability { get; set; }
        public string otherprovision_total { get; set; }
        public string ordinaryshare_capital { get; set; }
        public string capitalredemption_reserve { get; set; }
        public string general_reserve { get; set; }
        public string investment_allowancereserve { get; set; }
        public string surplusdeficitpandl_account { get; set; }
        public string prefsharecapital_debenture { get; set; }
        public string revaluation_reserve { get; set; }
        public string other_reserves { get; set; }
        public string capital_reserves { get; set; }
        public string sharepremium_acct { get; set; }
        public string advancetowards_capital { get; set; }
        public string deposit_relatives { get; set; }
        public string net_worth { get; set; }
        public string total_liabilities { get; set; }
        public string claimscmpyack_debts { get; set; }
        public string arrearsofsalary_susemployee { get; set; }
        public string estimated_capitalacct { get; set; }
        public string guaranteesbankers_company { get; set; }
        public string incurrent_account { get; set; }
        public string fixeddeposits_banks { get; set; }
        public string othercash_bankbalance { get; set; }
        public string cashbankbalance_total { get; set; }
        public string government_securities { get; set; }
        public string otherapproved_securities { get; set; }
        public string shares { get; set; }
        public string debentures_bonds { get; set; }
        public string subsidiariesjoint_ventures { get; set; }
        public string other_investments { get; set; }
        public string investments_total { get; set; }
        public string loanspayable_demand { get; set; }
        public string term_loans { get; set; }
        public string other_advances { get; set; }
        public string advances_total { get; set; }
        public string interest_accrued { get; set; }
        public string advancetax_deducted { get; set; }
        public string asset_others { get; set; }
        public string othersssets_total { get; set; }
        public string land_building { get; set; }
        public string computers { get; set; }
        public string plant_machinery { get; set; }
        public string furnitures_fixtures { get; set; }
        public string otherfixed_assets { get; set; }
        public string capital_wip { get; set; }
        public string less_depreciation { get; set; }
        public string net_block { get; set; }
        public string miscellaneous_expenses { get; set; }
        public string patents_good { get; set; }
        public string debitbalancepandl_account { get; set; }
        public string unsecureddebtors_doubtful { get; set; }
        public string total_intangibleassets { get; set; }
        public string total_assets { get; set; }
        public string totalliabilities_totalasset { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }


    //Summary Template-1

    public class MdlSummaryTemplate1View : result
    {
        public List<summarytemplate1_list> summarytemplate1_list { get; set; }
    }

    public class summarytemplate1_list
    {
        public string summarytemplate1_gid { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }

        public string date { get; set; }
        public string audit_type { get; set; }
        public string net_sales { get; set; }
        public string other_income { get; set; }
        public string total_revenue { get; set; }
        public string growth_in_revenues { get; set; }
        public string ebitda { get; set; }
        public string ebitda_margin { get; set; }
        public string depreciation { get; set; }
        public string interest { get; set; }
        public string pat { get; set; }
        public string pat_margin { get; set; }

        public string total_outside_liabilities { get; set; }
        public string total_bank_borrowings { get; set; }
        public string tangible_net_worth { get; set; }
        public string current_ratio { get; set; }
        public string tol_tnw { get; set; }
        public string interest_coverage_ratio { get; set; }
        public string dscr { get; set; }
        public string sundry_creditors { get; set; }
        public string sundry_debtors { get; set; }
        public string inventories { get; set; }
        public string payable_noofdays { get; set; }
        public string recievable_noofdays { get; set; }
        public string inventory_noofdays { get; set; }
        public string workingcapital_noofdays { get; set; }
        public string debt_ebitda { get; set; }
        public string msme { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class MdlSummaryTemplate2View : result
    {
        public List<summarytemplate2_list> summarytemplate2_list { get; set; }
    }

    public class summarytemplate2_list
    {
        public string summarytemplate2_gid { get; set; }
        public string credit_gid { get; set; }
        public string application_gid { get; set; }

        public string date { get; set; }
        public string audit_type { get; set; }
        public string interest_earned { get; set; }
        public string other_income { get; set; }
        public string total_income { get; set; }
        public string growth_in_income { get; set; }
        public string interest_expenses { get; set; }
        public string operating_expenses { get; set; }
        public string provision_and_contingencies { get; set; }
        public string net_profit { get; set; }
        public string total_debt { get; set; }

        public string tangible_networth { get; set; }
        public string net_interest_income { get; set; }
        public string assets_undermanagement { get; set; }
        public string nim { get; set; }
        public string loan_disbursed { get; set; }
        public string crar { get; set; }
        public string debt_equity { get; set; }
        public string operational_selfsufficiency_ratio { get; set; }
        public string costtoincome_ratio { get; set; }
        public string returnon_avgassets { get; set; }
        public string returnon_avgnetworth { get; set; }
        public string gross_npa { get; set; }
        public string net_npa { get; set; }
        public string gross_npapercent { get; set; }
        public string net_npapercent { get; set; }
        public string netnpa_networth { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    // P & L Template 1

    public class MdlMstProfitLosstemp2 : result
    {
        public List<profitlosstemp2_list> profitlosstemp2_list { get; set; }
    }
    public class profitlosstemp2_list
    {
        public string audited { get; set; }
        public string interest_income { get; set; }
        public string incomeon_investments { get; set; }
        public string interest_others { get; set; }
        public string income_others { get; set; }
        public string totalinterest_earned { get; set; }
        public string other_income { get; set; }
        public string profit_sales { get; set; }
        public string miscellaneous_income { get; set; }
        public string totalother_income { get; set; }
        public string total_income { get; set; }
        public string increase_income { get; set; }
        public string expenditure { get; set; }
        public string expenedinterest_borrower { get; set; }
        public string expenedinterest_deposit { get; set; }
        public string expened_other { get; set; }
        public string totalinterest_expened { get; set; }
        public string operating_expenses { get; set; }
        public string employee_cost { get; set; }
        public string depreciation { get; set; }
        public string other_operating_cost { get; set; }
        public string total_operating_expenses { get; set; }
        public string provision_asset { get; set; }
        public string provision_nonasset { get; set; }
        public string provision_tax { get; set; }
        public string other_provision { get; set; }
        public string total_provision { get; set; }
        public string total_expenditure { get; set; }
        public string pbt { get; set; }
        public string income_tax { get; set; }
        public string pat { get; set; }
        public string amount { get; set; }
        public string rent { get; set; }
        public string retained_profit { get; set; }
        public string template_name { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string allfiguresin_inr { get; set; }
    }


    public class MdlMstcreditApprovalInfo : result
    {
        public string approval_remarks { get; set; } 
        public string creditmanager_name { get; set; }
        public string creditregionalmanager_name { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string credithead_name { get; set; }
        public string pendingapproval { get; set; }
        public string credithead_gid { get; set; }
        public string creditnationalmanager_gid { get; set; }
        public string creditregionalmanager_gid { get; set; }
        public string creditmanager_gid { get; set; }
        public List<creditApproval_list> creditApproval_list { get; set; }
    }
    public class creditApproval_list
    {
        public string approval_gid { get; set; }
        public string approval_name { get; set; }
        public string approval_status { get; set; }
        public string hierary_level { get; set; }
        public string approver { get; set; } 
    }
     public class MdlMstGetoldnewapplicationid : result
    {
        public string newapplication_gid { get; set; }
        public string oldapplication_gid { get; set; }
        public string newapplication2loan_gid { get; set; }
        public string oldapplication2loan_gid { get; set; }
        public string newapplicationno { get; set; }
        public string oldapplicationno { get; set; }  
        public string newproduct_type { get; set; }
        public string oldproduct_type { get; set; }
        public string oldproduct_name { get; set; }
        public string MdlNewSupplierPaymentlistflag { get; set; }
        public string mstnewproductdtl_listflag { get; set; }
        public string MdlNewSupplierdtllistflag { get; set; }
        public string Mdlsource_typeflag { get; set; }
        public string Mdlcollateralobservation_summaryflag { get; set; }
        public  string program_gid { get; set; }
        public string product_gid { get; set; }

        public string renewal_flag { get; set; }
        public string created_date { get; set; }
        public List<mstLoan_list1> mstLoan_list { get; set; }

        public List<mstcollateral_list1> mstcollateral_list { get; set; }

        public string amendment_flag { get; set; }
    }
    public class mstLoan_list1 : result
    {
        public string application_no { get; set; }
        public string application2loan_gid { get; set; }
        public string facilityrequested_date { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
        public string loanfacility_amount { get; set; }
        public string loan_type { get; set; }
        public string rate_interest { get; set; }
        public string penal_interest { get; set; }
        public string facilityoverall_limit { get; set; }
        public string tenureoverall_limit { get; set; }
        public string facility_type { get; set; }
        public string facility_mode { get; set; }
        public string principalfrequency_name { get; set; }
        public string interestfrequency_name { get; set; }
        public string interest_status { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }
        public string moratorium_startdate { get; set; }
        public string moratorium_enddate { get; set; }
        public string scheme_type { get; set; }
        public string tenureproduct_days { get; set; }
        public string tenureproduct_month { get; set; }
        public string limit_product { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string sector_name { get; set; }
        public string category_name { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string program_gid { get; set; }
        public string program { get; set; }
        public  string highlight_flag { get; set; }
        public string application_gid { get; set; }
        public string validityto_date { get; set; }
        public string validityfrom_date { get; set; }
    }

    public class mstcollateral_list1 : result
    {
        public string application2loan_gid { get; set; }
        public string source_type { get; set; }
        public string guideline_value { get; set; }
        public string guideline_date { get; set; }
        public string marketvalue_date { get; set; }
        public string market_value { get; set; }
        public string forcedsource_value { get; set; }
        public string collateralSSV_value { get; set; }
        public string forcedvalueassessed_on { get; set; }
        public string collateralobservation_summary { get; set; }
        public string product_type { get; set; }
        public string productsub_type { get; set; }
    }
}