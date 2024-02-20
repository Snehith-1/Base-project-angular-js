using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class allocationTransfer : result
    {
        public string[] allocationdtl_gid { get; set; }
        public string allocation_Gid { get; set; }
        public string transferred_to { get; set; }
        public string transferTo_stategid { get; set; }
        public string transferTo_districtgid { get; set; }
        public string transferred_from { get; set; }
        public string customerdtl { get; set; }
        public string state_gid { get; set; }
        public string location { get; set; }
        public string assignedRM_name { get; set; }
        public string zonalRM_name { get; set; }
        public string zonalRM_gid { get; set; }
        public string zonal_gid { get; set; }
        public List<assignedRMlist> assignedRM { get; set; }
    }

    public class assignedRMlist : result
    {
        public string assigned_RMname { get; set; }
        public string assignedRM_gid { get; set; }
    }

    public class externalAllcoation : result
    {
        public string customerdtl { get; set; }
        public string externalname { get; set; }
        public string requested_remarks { get; set; }
        public string target_date { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public List<upload_list> upload_list { get; set; }
    }

    public class externaldtlList : result
    {
        public List<externaldtl> externaldtl { get; set; }
    }
    public class externaldtl : result
    {
        public string external_usergid { get; set; }
        public string external_username { get; set; }

    }

    public class externalAllocate : result
    {
        public string allocationdtl_gid { get; set; }
        public string external_usergid { get; set; }
        public string external_name { get; set; }
        public string external_allocateRemarks { get; set; }
        public string target_date { get; set; }
    }

    public class uploaddocument : result
    {
        public string visitreport_photoGid { get; set; }
        public List<upload_list> upload_list { get; set; }
    }
    public class upload_list
    {
        public string tmp_documentGid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string allocation_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_type { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
    }

    public class document
    {
        public string message { get; set; }
        public bool status { get; set; }
    }

    public class customerList : result
    {
        public List<customerdtl> customerdtl { get; set; }
    }

    public class customerdtl : result
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
    }

    public class lastvisitdtl : result
    {
        public DateTime lastvisit_date { get; set; }
        public string customer_gid { get; set; }
    }
    public class visistreportcancelList : result
    {
        public List<visistreportcancel> visistreportcancel { get; set; }
    }
    public class visistreportcancel : result
    {
        public string cancel_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class qualifiedallocationlist : result
    {
        public string count_current { get; set; }
        public string count_upcoming { get; set; }
        public string count_breached { get; set; }
        public string count_completed { get; set; }
        public string count_external { get; set; }
        public string count_reportchanges { get; set; }
        public string count_qualified { get; set; }
        public int count_unmatchedqualified { get; set; }
        public List<qualifiedallocation> qualifiedallocation { get; set; }
    }

    public class qualifiedallocation : result
    {
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string disbursement_date { get; set; }
        public string daypassed_disbursement { get; set; }
        public string lastvisit_date { get; set; }
        public string daypassed_visit { get; set; }
        public string qualified_status { get; set; }
        public string total_sanction { get; set; }
        public string vertical { get; set; }
    }
    public class customergid : result
    {
        public string customer_gid { get; set; }
    }

    public class allocationlist : result
    {
        public int count_reportcancel { get; set; }
        public int count_completedallo { get; set; }
        public int count_currentallo { get; set; }
        public int count_upcoming { get; set; }
        public int count_external { get; set; }
        public List<allocationdtl> allocationdtl { get; set; }
    }
    public class allocationdtl : result
    {
        public string allocationdtl_gid { get; set; }
        public string allocated_date { get; set; }
        public string customer_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string cutoff_date { get; set; }
        public string vertical { get; set; }
        public string location { get; set; }
        public string assigned_RM { get; set; }
        public string ZonalRMname { get; set; }
        public string allocation_flag { get; set; }
        public string allocation_status { get; set; }
        public string allocate_external { get; set; }
        public string lastvisit_date { get; set; }
        public string count_lastvisit { get; set; }
        public string disbursement_date { get; set; }
        public string daypassed_disbursement { get; set; }
        public string sanction_amount { get; set; }
        public string allocate_externalname { get; set; }
        public string target_date { get; set; }
        public string visit_allocatemonth { get; set; }
        public string constitution { get; set; }
        public string created_by { get; set; }
        public string typeof_loanvertical { get; set; }
        public string typeof_riskreview { get; set; }
        public string turnover_lastFY { get; set; }
        public string observation_flag { get; set; }
        public string tier1_approvalstatus { get; set; }
        public string tier2_flag { get; set; }
        public string tier3_flag { get; set; }
        public string visited_date { get; set; }
        public string appointment_date { get; set; }
        public string appointment_time { get; set; }
        public string schedule_status { get; set; }
        public string entity_name { get; set; }
        public string sanction_date { get; set; }
        public string tier1format_gid { get; set; }
        public string allocatedmonth { get; set; }
        public string tier2_approval_status { get; set; }
        public string zonal_name { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string Manual_Allocation { get; set; }
    }

    public class rmallocationlist : result
    {
        public int count_current { get; set; }
        public int count_completed { get; set; }
        public int count_upcoming { get; set; }
        public List<rmallocation> rmallocation { get; set; }
    }

    public class rmallocation : result
    {
        public string allocationdtl_gid { get; set; }
        public string observation_flag { get; set; }
        public string observation_reportgid { get; set; }
        public string customer_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string assigned_RM { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string allocation_status { get; set; }
        public string ZonalRMname { get; set; }
        public string completed_flag { get; set; }
        public string visit_status { get; set; }
        public string visit_allocatemonth { get; set; }
        public string tier1_approvalstatus { get; set; }
        public string tier3_status { get; set; }
        public string tier2_flag { get; set; }
        public string tier3_flag { get; set; }
        public string lastvisit_date { get; set; }
        public string count_lastvisit { get; set; }
        public string ATR_flag { get; set; }
    }

    public class breachedlist : result
    {
        public int count_breached { get; set; }
        public List<breacheddtl> breacheddtl { get; set; }
    }

    public class breacheddtl : result
    {
        public string allocationdtl_gid { get; set; }
        public string visit_allocatemonth { get; set; }
        public string allocated_date { get; set; }
        public string customer_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string vertical { get; set; }
        public string location { get; set; }
        public string assigned_RM { get; set; }
        public string ZonalRMname { get; set; }
        public string allocation_flag { get; set; }
        public string allocation_status { get; set; }
        public string lastvisit_date { get; set; }
        public string count_lastvisit { get; set; }
        public string disbursement_date { get; set; }
        public string daypassed_disbursement { get; set; }
        public string sanction_amount { get; set; }
    }

    public class holdallocation : result
    {
        public string previous_allocatedate { get; set; }
        public string current_allocatedate { get; set; }
        public string allocationhold_reason { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class U2CMovedallocation : result
    {
        public string previous_allocatedate { get; set; }
        public string current_allocatedate { get; set; }
        public string allocationmoved_reason { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }


    public class overallzonalcountList : result
    {
        public string ADM_updatedby { get; set; }
        public string ADM_updateddate { get; set; }
        public string count_current { get; set; }
        public string count_upcoming { get; set; }
        public string count_breached { get; set; }
        public string count_completed { get; set; }
        public string count_external { get; set; }
        public string count_reportchanges { get; set; }
        public string count_qualified { get; set; }
        public int count_unmatchedqualified { get; set; }
        public List<overallzonalcount> overallzonalcount { get; set; }
    }

    public class overallzonalcount : result
    {
        public string zonalmapping_gid { get; set; }
        public string zonal_name { get; set; }
        public string zonal_riskmanager { get; set; }
        public int zonalpending_count { get; set; }
    }
    public class zonalwisecountList : result
    {
        public List<zonalwisecount> zonalwisecount { get; set; }
    }

    public class zonalwisecount : result
    {
        public string riskmanager_name { get; set; }
        public int pending_count { get; set; }
        public string count_status { get; set; }
        public string assigned_RM { get; set; }
        public string count_fresh { get; set; }
        public string count_revisit { get; set; }
    }

    public class overallhistoryallocationlist : result
    {
        public List<overallhistoryallocationdtl> overallhistoryallocationdtl { get; set; }
    }

    public class overallhistoryallocationdtl
    {
        public string allocationdtl_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string location { get; set; }
        public string assigned_RM { get; set; }
        public string ZonalRMname { get; set; }
        public string allocation_flag { get; set; }
        public string allocation_status { get; set; }
        public string lastvisit_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string completed_date { get; set; }
        public string reportcancel_flag { get; set; }
        public string allocate_external { get; set; }
        public string observation_reportgid { get; set; }
        public string observation_flag { get; set; }
        public string tier1format_gid { get; set; }
    }

    public class exclusionAllocationlist
    {
        public int count_exclusion { get; set; }
        public List<exclusionAllocation> exclusionAllocation { get; set; }
    }

    public class exclusionAllocation
    {
        public string customer_gid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string assigned_RM { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string allocation_status { get; set; }
        public string ZonalRMname { get; set; }
        public string visit_allocatemonth { get; set; }
        public string lastvisit_date { get; set; }
        public string count_lastvisit { get; set; }
    }
    public class exportAllocation : result
    {
        public string excel_path { get; set; }
        public string excel_name { get; set; }
    }
    public class allocationSummary : result
    {
        public List<allocationdtl> allocationdtl { get; set; }
        public string customer_gid { get; set; }
        public string vertical { get; set; }
        public string zonalmapping_gid { get; set; }
        public string zonalrisk_manager { get; set; }
        public string risk_manager { get; set; }
    }
    public class CustomersList : result
    {
        public List<Customers> Customers { get; set; }
    }


    public class Customers : result
    {
        public string customer_gid { get; set; }
        public string customername { get; set; }
    }
    public class visitReportPDFContent
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
        public bool status { get; set; }

    }

}