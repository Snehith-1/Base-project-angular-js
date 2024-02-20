using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class MdlMstHRLoanRequestWithdrawn : result
     {       
        public List<hrloanrequestWithdrawn> hrloanrequestWithdrawn { get; set; }        
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
        public string request_gid { get; set; }
        
    }
    
    public class hrloanrequestWithdrawn : result
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
        public string status_updatedby { get; set; }
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
        public string raisequery_status { get; set; }
        public string withdraw_flag { get; set; }
        public string withdraw_remarks { get; set; }

    }



}