using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class observationreportdtl : result
    {
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_code { get; set; }
        public string RMD_visitname { get; set; }
        public string RMD_visitGid { get; set; }
        public string relationship_manager_gid { get; set; }
        public string relationship_manager_name { get; set; }
        public string visit_date { get; set; }
        public string sanction_amount { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string contact_details1 { get; set; }
        public string contact_details2 { get; set; }
        public string PPA_name { get; set; }
        public string totalloan_outstanding { get; set; }
        public string risk_code { get; set; }
        public string riskcode_classification { get; set; }
    }

    public class observationdtl : result
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
        public string relationship_manager_gid { get; set; }
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
        public string customer_gid { get; set; }
    }

    public class criticalobservationlist : result
    {
        public List<criticalobservation> criticalobservation { get; set; }
    }

    public class criticalobservation : result
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

    public class observationreportlist : result
    {
        public string count_pending { get; set; }
        public string count_approved { get; set; }
        public List<observationreport> observationreport { get; set; }
    }

    public class observationreport : result
    {
        public string observation_reportgid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical { get; set; }
        public string dateof_RMDvisit { get; set; }
        public string RMDvisit_officialname { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string observation_flag { get; set; }
    }

    public class criticalobservationdtl : result
    {
        public string observation_reportgid { get; set; }
        public string critical_observationgid { get; set; }
        public string relationshipmanager_remarks { get; set; }
    }

    public class tier1format : result
    {
        public string tier1code_changereason { get; set; }
        public string tier1format_gid { get; set; }
        public string observation_reportgid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical { get; set; }
        public string tier1_observations { get; set; }
        public string tier1_code { get; set; }
        public string tier1_justification { get; set; }
        public string tier1_processgap { get; set; }
        public string tier1_processrecommendation { get; set; }
        public string tier1_managementcomments { get; set; }
        public string tier1_reverts_actionplan { get; set; }
        public string tier1_atrdate { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string tier1_approvalstatus { get; set; }
        public string tier3_status { get; set; }
        public string tier1_approveddate { get; set; }
        public string tier1code_changeflag { get; set; }
        public List<tier1approvallog> tier1approvallog { get; set; }
        public List<tier1rejectlog> tier1rejectlog { get; set; }
        public List<tier1doc> tier1doc { get; set; }
        public string customer_gid { get; set; }
        public string tier1_rejectedremarks { get; set; }
    }

    public class tier1approvallog : result
    {
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string month_name { get; set; }
        public string month { get; set; }
    }
    public class tier1doc : result
    {
        public string tier1document_gid { get; set; }
        public string document_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
    }
    public class tier1rejectlog : result
    {
        public string reject_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    
    public class atrPDFcontent
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
        public bool status { get; set; }
    }

}