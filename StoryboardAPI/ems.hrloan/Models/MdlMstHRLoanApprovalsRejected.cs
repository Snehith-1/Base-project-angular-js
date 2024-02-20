using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models 
{
    public class MdlMstHRLoanApprovalsRejected : result
     {
        public string drm_remarks { get; set; }
        public string fh_remarks { get; set; }
        public string request_gid { get; set; }
        public List<Rejectedrequestsummary> Rejectedrequestsummary { get; set; }
        public List<RejectedApprovalsummary> RejectedApprovalsummary { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string role { get; set; }
        public string department { get; set; }
        public string reporting_manager { get; set; }
        public string functional_head { get; set; }
        public string functionalhead_gid { get; set; }
        public string hr_head { get; set; }
        public string hrhead_gid { get; set; }
        public string reportingmgr_gid { get; set; }
        public string department_gid { get; set; }
        public string official_mailid { get; set; }
        public string official_mobileno { get; set; }
        public string pers_mailid { get; set; }
        public string pers_mobileno { get; set; }
    }
    public class RejectedApprovalsummary : result
    {
        public string request_gid { get; set; }
        public string approval_Level { get; set; }
        public string approval_status { get; set; }
        public string approved_by { get; set; }
        public string updated_date { get; set; }
        public string approval_remarks { get; set; }
        

    }
        public class Rejectedrequestsummary : result
    {

        public string request_gid { get; set; }
        public string request_reason { get; set; }
        public string tenure { get; set; }
        public string severity_gid { get; set; }
        public string severity_name { get; set; }
        public string purpose_name { get; set; }
        public string purpose_gid { get; set; }
        public string amount { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string drm_status { get; set; }
        public string raised_department { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string department_name { get; set; }
        public string employee_role { get; set; }
        public string employee_name { get; set; }
        public string fintype_name { get; set; }
        public string fintype_gid { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string user_gid { get; set; }
        public string functional_head { get; set; }
        public string functionalhead_gid { get; set; }
        public string hr_head { get; set; }
        public string hrhead_gid { get; set; }
        public string reportingmgr_gid { get; set; }
        public string department_gid { get; set; }
        public string reporting_mgr { get; set; }
        public string entity_name { get; set; }

    }



}