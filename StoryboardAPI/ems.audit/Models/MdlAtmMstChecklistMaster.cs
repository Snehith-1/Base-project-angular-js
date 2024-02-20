using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ems.audit.Models
{
    public class MdlAtmMstChecklistMaster : result
    {

        public List<checkpoint_list> checkpoint_list { get; set; }
        public List<checkpointaddgroup_list> checkpointaddgroup_list { get; set; }
        public List<checklistmaster_list> checklistmaster_list { get; set; }
        public List<checklistmasteradd_list> checklistmasteradd_list { get; set; }
        public List<checkpointgroupadd_list> checkpointgroupadd_list { get; set; }
        public List<checkpointgroup_list> checkpointgroup_list { get; set; }
        public string checklistmaster_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string auditdepartment_gid { get; set; }
        public string audittype_name { get; set; }
        public string audittype_gid { get; set; }
        public string checkpointgroup_gid { get; set; }
        public string checkpointgroup_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string auditmapping_gid { get; set; }
        public string auditmapping_name { get; set; }
        public string audit_name { get; set; }
        public string audit_maker { get; set; }
        public string audit_checker { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string checklistmasteradd_gid { get; set; }
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
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string positiveconfirmity_gid { get; set; }
        public string user_gid { get; set; }
        public string auditeemaker_name { get; set; }
        public string auditeechecker_name { get; set; }
        public string audit_description { get; set; }

    }


    public class checkpoint_list
    {

        public string checkpointgroup_gid { get; set; }
        public string checkpointgroup_name { get; set; }
    }
    public class checkpointaddgroup_list
    {

        public string checkpointgroup_gid { get; set; }
        public string checkpointgroup_name { get; set; }
    }

    public class checklistmaster_list
    {

        public string checklistmaster_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string auditdepartment_gid { get; set; }
        public string audittype_name { get; set; }
        public string audittype_gid { get; set; }
        public string checkpointgroup_gid { get; set; }
        public string checkpointgroup_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string auditmapping_gid { get; set; }
        public string auditmapping_name { get; set; }
        public string audit_name { get; set; }
        public string audit_maker { get; set; }
        public string audit_checker { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string positiveconfirmity_gid { get; set; }
        public string user_gid { get; set; }
        public string auditeemaker_name { get; set; }
        public string auditeechecker_name { get; set; }
        public string checklist_uniqueno { get; set; }
        public string auditdelete_flag { get; set; }
        public string audit_description { get; set; }

    }
    public class checklistmasteradd_list
    {
        public string checkpointgroup_gid { get; set; }
        public string checklistmaster_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
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
        public string auditdelete_flag { get; set; }
    }
    
}