using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.audit.Models
{
    //public class result
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; }
    //}
    public class MdlAtmMstCheckpointGroup : result
    {
        

        public string checkpointgroup_gid { get; set; }
        public string checkpointgroup_name { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string checkpointgroup_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string noteto_auditor { get; set; }
        public string yes_score { get; set; }
        public string yes_disposition { get; set; }
        public string no_score { get; set; }
        public string no_disposition { get; set; }
        public string partial_score { get; set; }
        public string partial_disposition { get; set; }
        public string na_score { get; set; }
        public string na_disposition { get; set; }
        public string total_score { get; set; }
        public string total_mark { get; set; }
        public string riskcategory_gid { get; set; }
        public string riskcategory_name { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string positiveconfirmity_gid { get; set; }
        public string user_gid { get; set; }
        public string status_flag { get; set; }
        public string checkpointgroupadd_gid { get; set; }
        //public string lspositiveconfirmity_gid { get; set; }
        //public string lsriskcategory_gid { get; set; }
        public string auditor_maker { get; set; }
        public string auditormaker_gid { get; set; }
        public string auditor_checker { get; set; }
        public string auditorchecker_gid { get; set; }
        public string auditor_approver { get; set; }
        public string auditorapprover_gid { get; set; }

        public List<riskcategoryinfo> riskcategoryinfo { get; set; }
        public List<positiveconfirmityinfo> positiveconfirmityinfo { get; set; }
        public List<checkpointgroup_list> checkpointgroup_list { get; set; }
        public List<inactivehistory_list> inactivehistory_list { get; set; }
        public List<checkpointgroupadd_list> checkpointgroupadd_list { get; set; }

    }
    public class checkpointgroup_list : result
    {

        public string checkpointgroup_gid { get; set; }
        public string checkpointgroup_name { get; set; }
        public string checklistmaster_gid { get; set; }
        public string checkpointgroup_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string auditdelete_flag { get; set; }
    }
    public class inactivehistory_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    public class checkpointgroupadd_list
    {
        public string checklistmasteradd_gid { get; set; }
        public string checkpointgroup_gid { get; set; }
        public string checkpointgroupadd_gid { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string noteto_auditor { get; set; }
        public string yes_score { get; set; }
        public string yes_disposition { get; set; }
        public string no_score { get; set; }
        public string no_disposition { get; set; }
        public string partial_score { get; set; }
        public string partial_disposition { get; set; }
        public string na_score { get; set; }
        public string na_disposition { get; set; }
        public string total_score { get; set; }
        public string riskcategory_gid { get; set; }
        public string riskcategory_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string positiveconfirmity_gid { get; set; }
        public string user_gid { get; set; }
        public string status_flag { get; set; }
        public string checkpointgroup_name { get; set; }
        public string checkpointgroup_flag { get; set; }
    }

    public class riskcategoryinfo
    {

        public string lsriskcategory_name { get; set; }
    }

    public class positiveconfirmityinfo
    {

        public string lspositiveconfirmity_name { get; set; }
    }

    public class checklistcheckpoint : result
    {
        public string auditcreation_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string checklist2checkpoint { get; set; }
        public string sampleimport_gid { get; set; }
        public string checklist_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string checkpointgroup_gid { get; set; }
        public string[] checkpointgroupadd_gid { get; set; }
        public List<checklistcheckpoint_list> checklistcheckpoint_list { get; set; }

    }
    public class checklistsamplecheckpoint : result
    {
        public string auditcreation_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string checklist2checkpoint { get; set; }
        public string sampleimport_gid { get; set; }
        public string checklist_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string checkpointgroup_gid { get; set; }
        public string checkpointgroupadd_gid { get; set; }
        public List<checklistcheckpoint_list> checklistcheckpoint_list { get; set; }

    }

    public class checklistcheckpoint_list
    {
        public string auditcreation_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string checklist2checkpoint { get; set; }
        public string checklist_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public string checkpointgroupadd_gid { get; set; }
        public string overall_detail { get; set; }
        public string checklist_verified { get; set; }
        public string samplechecklistverified_flag { get; set; }
        public string sample2checkpoint { get; set; }
        public string checklistverified_flag { get; set; }
    }
}