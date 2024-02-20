using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class MdlRptEmployeeLoanReport : result
    {
        public List<SummaryReport_list> SummaryReport_list { get; set; }
        public string request_gid { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string status1 { get; set; }
        public string fintype_name { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_role { get; set; }
        public string department_name { get; set; }
        public string user_gid { get; set; }
        public string reporting_mgr { get; set; }
        public string created_date { get; set; }
        public string amount { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string lscloudpath { get; set; }

    }
    public class SummaryReport_list
    {
        public string request_gid { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string status1 { get; set; }
        public string fintype_name { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_role { get; set; }
        public string department_name { get; set; }
        public string user_gid { get; set; }
        public string reporting_mgr { get; set; }
        public string created_date { get; set; }
        public string amount { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string lscloudpath { get; set; }
    }

    public class EmployeeLoanReportcount : result
    {
        public string total_count { get; set; }
        public string pending_count { get; set; }
        public string rejected_count { get; set; }
        public string completed_count { get; set; }
        public string WithdrawnCount { get; set; }
    }

}