using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class tier1formatlist : result
    {
        public string count_pending { get; set; }
        public string count_approved { get; set; }
        public string count_rejected { get; set; }
        public List<tier1format> tier1format { get; set; }
    }

    public class tier1approval : result
    {
        public string tier1format_gid { get; set; }
        public string approval_remarks { get; set; }
    }

    public class tier2zonaldtl : result
    {
        public string zonalmapping_gid { get; set; }
        public string zonal_name { get; set; }
        public List<monthname> monthname { get; set; }
    }

    public class monthname : result
    {
        public string month_name { get; set; }
        public string month { get; set; }
    }

    public class tier2preparationlist : result
    {
        public string count_pending { get; set; }
        public string count_approved { get; set; }
        public string count_rejected { get; set; }
        public List<tier2preparation> tier2preparation { get; set; }
    }

    public class tier2preparation : result
    {
        public string tier2preparation_gid { get; set; }
        public string tierallocation_gid { get; set; }
        public string zonalmapping_gid { get; set; }
        public string zonal_name { get; set; }
        public string tier2_month { get; set; }
        public string vertical_gid { get; set; }
        public string vertical { get; set; }
        public string headRMD_gid { get; set; }
        public string headRMD_name { get; set; }
        public string tier2_remarks { get; set; }
        public string tier2_approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class tier2viewdtl : result
    {
        public string tier2preparation_gid { get; set; }
        public string zonalmapping_gid { get; set; }
        public string zonal_name { get; set; }
        public string tier2_month { get; set; }
        public string tier2_monthname { get; set; }
        public string vertical_gid { get; set; }
        public string vertical { get; set; }
        public string headRMD_gid { get; set; }
        public string headRMD_name { get; set; }
        public string tier2_remarks { get; set; }
        public string tier2_approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string tier3_status { get; set; }
        public List<tier2document> tier2document { get; set; }
        public List<tier2approvallog> tier2approvallog { get; set; }
        public List<tierallocationdtl> tierallocationdtl { get; set; }
        public List<tierallocationdtlLatest> tierallocationdtlLatest { get; set; }
    }

    public class tier2approvallog : result
    {
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string approved_by { get; set; }
        public string approved_date { get; set; }
    }

    public class tier2documentlist : result
    {
        public List<tier2document> tier2document { get; set; }
    }

    public class tier2document : result
    {
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string tier2document_gid { get; set; }
        public string created_date { get; set; }
        public string tmp_documentGid { get; set; }
    }

    public class tier2approval : result
    {
        public string tier2preparation_gid { get; set; }
        public string approval_remarks { get; set; }
    }

    public class tierverticallist : result
    {
        public List<tiervertical> tiervertical { get; set; }
    }

    public class tiervertical : result
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string vertical_code { get; set; }
    }

    public class tier3preparationlist : result
    {
        public string count_pending { get; set; }
        public string count_completed { get; set; }
        public List<tier3preparation> tier3preparation { get; set; }
    }

    public class tier3preparation : result
    {
        public string tier3preparation_gid { get; set; }
        public string MLRC_date { get; set; }
        public string tier3_month { get; set; }
        public string tier3_monthname { get; set; }
        public string vertical_gid { get; set; }
        public string vertical { get; set; }
        public string follow_up { get; set; }
        public string tier3_status { get; set; }
        public string completed_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string completed_flag { get; set; }
    }

    public class tier3documentlist : result
    {
        public List<tier3document> tier3document { get; set; }
    }

    public class tier3document : result
    {
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string tier3document_gid { get; set; }
        public string created_date { get; set; }
        public string tmp_documentGid { get; set; }
    }

    public class tier1documentlist : result
    {
        public List<tier1document> tier1document { get; set; }
    }
    public class tier1document
    {
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string tier1document_gid { get; set; }
        public string created_date { get; set; }
        public string tmp_documentGid { get; set; }
    }

    public class tier3completedtl : result
    {
        public string completed_remarks { get; set; }
        public string tier3preparation_gid { get; set; }
    }

    public class tier3viewdtl : result
    {
        public string tier3preparation_gid { get; set; }
        public string MLRC_date { get; set; }
        public string tier3_month { get; set; }
        public string tier3_monthname { get; set; }
        public string vertical_gid { get; set; }
        public string vertical { get; set; }
        public string follow_up { get; set; }
        public string tier3_status { get; set; }
        public string completed_date { get; set; }
        public string completed_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string completed_flag { get; set; }
        public string completed_by { get; set; }
        public DateTime MLRC_Date { get; set; }
        public List<tier3document> tier3document { get; set; }
        public List<tierallocationdtl> tierallocationdtl { get; set; }
    }

    public class tierallocation : result
    {
        public string vertical_gid { get; set; }
        public string month { get; set; }
        public string tier2_flag { get; set; }
    }

    public class tierallocationdtllist : result
    {
        public List<tierallocationdtl> tierallocationdtl { get; set; }
    }

    public class tierallocationdtl : result
    {
        public string vertical { get; set; }
        public string tier1_approveddate { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string riskmanager { get; set; }
        public string zonal_name { get; set; }
        public string tier2_approveddate { get; set; }
        public string ZRM_name { get; set; }
        public string tier2preparation_gid { get; set; }
        public string tier1_code { get; set; }
        public string tier2_code { get; set; }
        public string tier3_code { get; set; }
        public string tier2_codechange { get; set; }
        public string tier1format_gid { get; set; }
        public string tierallocation_gid { get; set; }
    }

    public class tierallocationdtlLatest : result
    {
        public string vertical { get; set; }
        public string tier1_approveddate { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string riskmanager { get; set; }
        public string zonal_name { get; set; }
        public string tier2_approveddate { get; set; }
        public string ZRM_name { get; set; }
        public string tier2preparation_gid { get; set; }
    }

    public class tier2code 
    {
        public string tier1format_gid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string tier2_code { get; set; }
        public string tier2code_changereason { get; set; }
        public string tier2code_Trnchange { get; set; }
        public string tierallocation_gid { get; set; }
        public string tier2preparation_gid { get; set; }
    }

    public class tiercodedtllist:result
    {
        public string tier_stage { get; set; }
        public string tier_code { get; set; }
        public string tiercode_changereason { get; set; }
        public string tmptier2_codechange { get; set; }
        public List<tiercodedtl> tiercodedtl { get; set; }
    }

    public class tiercodedtl
    {
        public string tier_stage { get; set; }
        public string tier_code { get; set; }
        public string tiercode_changereason { get; set; }
        public string tmptier2_codechange { get; set; }
        public string delete_flag { get; set; }
    }

    public class tier3code
    {
        public string allocationdtl_gid { get; set; }
        public string tier3_code { get; set; }
        public string tier3code_changereason { get; set; }
        public string tierallocation_gid { get; set; }
        public string tier3code_Trnchange { get; set; }
    }

    public class tiercodechange
    {
        public string allocationdtl_gid { get; set; }
        public string tier_code { get; set; }
        public string tiercode_changereason { get; set; }
        public string tierallocation_gid { get; set; }
        public string tier3_flag { get; set; }
    }

    public class observationTierdtl : result
    {
        public string observation_reportgid { get; set; }
        public string visitreport_generategid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string risk_code { get; set; }
        public string dateof_RMDvisit { get; set; }
        public string report_pertainingto { get; set; }
        public string vertical { get; set; }
        public string disbursement_amount { get; set; }
        public string approving_authority { get; set; }
        public string loansanction_date { get; set; }
        public string relationship_manager_name { get; set; }
        public string PPA_name { get; set; }
        public string RMDvisit_officialname { get; set; }
        public string loandisbursement_date { get; set; }
        public string people_accompaniedRMD { get; set; }
        public string sanction_amount { get; set; }
        public string outstanding_amount { get; set; }
        public string current_DPD { get; set; }
        public string contact_details1 { get; set; }
        public string contact_details2 { get; set; }
        public string created_by { get; set; }
        public string observation_flag { get; set; }
        public string created_date { get; set; }
        public string atr_completiondate { get; set; }
        public string riskcode_classification { get; set; }
        public List<criticalTierobservation> criticalTierobservation { get; set; }
        public List<tierReportdtl> tierReportdtl { get; set; }
    }

  
    public class criticalTierobservation : result
    {
        public string observation_reportgid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string tmpcritical_observationgid { get; set; }
        public string critical_observationgid { get; set; }
        public string criteria { get; set; }
        public string RMD_observations { get; set; }
        public string actionable_recommended { get; set; }
        public string relationship_manager_remarks { get; set; }
        public string remarks_flag { get; set; }
        public string remarks_updatedby { get; set; }
        public string remarks_updateddate { get; set; }
        public string created_by { get; set; }
    }

 
    public class tierReportdtl
    {
        public string tier_stage { get; set; }
        public string tier_code { get; set; }
        public string tiercode_changereason { get; set; }
        public string tier_approvedby { get; set; }
        public string tier_approveddate { get; set; }
        public string tier_approvalstatus { get; set; }
    }

    public class tier2Reportviewdtl : result
    {
        public string tier2preparation_gid { get; set; }
        public string zonal_name { get; set; }
        public string tier2_monthname { get; set; }
        public string tier2_month { get; set; }
        public string vertical { get; set; }
        public string headRMD_name { get; set; }
        public string tier2_remarks { get; set; }
        public string tier2_approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string tier2_approveddate { get; set; }
        public List<tier2document> tier2document { get; set; }
        public List<tier2approvallog> tier2approvallog { get; set; }
    }

}