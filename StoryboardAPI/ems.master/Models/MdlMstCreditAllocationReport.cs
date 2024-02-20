using System.Collections.Generic;


namespace ems.master.Models
{
    public class MstCreditAllocationReport : result
    {
        public List<MstCreditSummaryList> MstCreditSummaryList { get; set; }
        public List<MstCreditExcelList> MstCreditExcelList { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customerref_name { get; set; }
        public string vertical_name { get; set; }
        public string applicant_type { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string customer_name { get; set; }
        public string lscloudpath { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string created_date { get; set; }
        public string submitted_date { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string overalllimit_amount { get; set; }
        public string region { get; set; }
        public string creditgroup_name { get; set; }
        public string headapproval_date { get; set; }
        public string creditassigned_to { get; set; }
        public string creditassigned_date { get; set; }
        public string creditassigned_by { get; set; }
        public string creditregionalmanager_name { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string credithead_name { get; set; }
        public string remarks { get; set; }
        public string rcm_remarks { get; set; }
        public string ncm_remarks { get; set; }
        public string ch_remarks { get; set; }
    }
    public class MstCreditSummaryList
    {
     
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customerref_name { get; set; }
        public string vertical_name { get; set; }
        public string applicant_type { get; set; }
        public string created_by { get; set; }
        public string region { get; set; }
        public string creditgroup_name { get; set; }
        public string rcm_remarks { get; set; }
        public string ncm_remarks { get; set; }
        public string ch_remarks { get; set; }
        public string headapproval_date { get; set; }
        public string creditassigned_to { get; set; }
        public string creditassigned_date { get; set; }
        public string creditassigned_by { get; set; }
        public string creditregionalmanager_name { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string credithead_name { get; set; }
        public string remarks { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string submitted_date { get; set; }
        public string updated_date { get; set; }
        public string overalllimit_amount { get; set; }
        public string updated_by { get; set; }
        public string ccsubmitted_date { get; set; }
}

    public class MstCreditExcelList
    {

        public string Application_Ref_number { get; set; }
        public string Application_Name { get; set; }
        public string Submitted_Date { get; set; }
        public string Submitted_By { get; set; }
        public string Vertical { get; set; }
        public string Region { get; set; }
        public string Overall_Limit{ get; set; }
        public string Creditgroup { get; set; }
        public string Approval_Date { get; set; }
        public string Allocation_To { get; set; }
        public string Allocation_Date { get; set; }
        public string Allocation_By { get; set; }
        public string RCM { get; set; }
        public string NCM { get; set; }
        public string CH { get; set; }
        public string Allocation_Remark { get; set; }
        public string Status { get; set; }
        public string RCM_Approved_Reject_Remark { get; set; }
        public string NCM_Approved_Reject_Remark { get; set; }
        public string CH_Approved_Reject_Remark { get; set; }
        public string CM_Reject_Remark { get; set; }
        public string CC_Submitted_Date { get; set; }

    }
}