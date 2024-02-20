using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{

    public class approvalcountdetails : result
    {
        public string count_approvalpending { get; set; }
        public string count_approval { get; set; }
        public string count_rejected { get; set; }
        public string count_history { get; set; }

        public string approved_leave { get; set; }
        public string approved_login { get; set; }
        public string approved_logout { get; set; }
        public string approved_onduty { get; set; }
        public string approved_compoff { get; set; }
        public string approved_permission { get; set; }

        public string rejected_leave { get; set; }
        public string rejected_login { get; set; }
        public string rejected_logout { get; set; }
        public string rejected_onduty { get; set; }
        public string rejected_compoff { get; set; }
        public string rejected_permission { get; set; }

        public string pending_leave { get; set; }
        public string pending_login { get; set; }
        public string pending_logout { get; set; }
        public string pending_onduty { get; set; }
        public string pending_compoff { get; set; }
        public string pending_permission { get; set; }
    }
    public class GetapproveLeavedetails : result
    {
        public string leave_gid { get; set; }
        public string leavetype_name { get; set; }
        public string leave_fromdate { get; set; }
        public string leave_todate { get; set; }
        public string leave_noofdays { get; set; }
        public string leave_reason { get; set; }
        public string leave_status { get; set; }
        public string applied_by { get; set; }

    }


    public class getleavesummarydetails : result
    {
        public List<login_list> login_list { get; set; }
        public List<logout_list> logout_list { get; set; }
        public List<od_list> od_list { get; set; }
        public List<compoffdtl_list> compoffdtl_list { get; set; }
        public List<permission_list> permission_list { get; set; }
    }
    public class login_list
    {

        public string loginapply_date { get; set; }
        public string loginattendence_date { get; set; }
        public string login_time { get; set; }
        public string login_reason { get; set; }
        public string login_status { get; set; }
        public string employee_name { get; set; }
    }
    public class logout_list
    {
        public string logoutapply_date { get; set; }
        public string logoutattendence_date { get; set; }
        public string logout_time { get; set; }
        public string logout_reason { get; set; }
        public string employee_name { get; set; }
        public string logout_status { get; set; }
    }

    public class od_list
    {
        public string onduty_date { get; set; }
        public string onduty_from { get; set; }
        public string onduty_to { get; set; }
        public string onduty_duration { get; set; }
        public string employee_name { get; set; }
        public string onduty_reason { get; set; }
        public string ondutytracker_status { get; set; }
    }

    public class compoffdtl_list
    {
        public string Compoff_from { get; set; }
        public string Compoff_to { get; set; }
        public string Compoff_duration { get; set; }
        public string Compoff_reason { get; set; }
        public string employee_name { get; set; }
        public string Compoff_status { get; set; }

    }

    public class permission_list
    {
        public string permission_date { get; set; }
        public string permission_from { get; set; }
        public string permission_to { get; set; }
        public string permission_duration { get; set; }
        public string permission_reason { get; set; }
        public string permission_status { get; set; }
        public string employee_name { get; set; }
    }
    public class approvepermission : result
    {
        public string permission_gid { get; set; }
        public List<approvepermission_list> approvepermission_list { get; set; }

    }
    public class approvepermission_list : approvepermission
    {

        public string employee_gid { get; set; }
        public string attendance_date { get; set; }
    }
    public class rejectpermission : result
    {
        public string permission_gid { get; set; }
        public List<rejectpermission_list> rejectpermission_list { get; set; }

    }
    public class rejectpermission_list : rejectpermission
    {

        public string employee_gid { get; set; }
        public string attendance_date { get; set; }
    }
    public class rejectcompoff : result
    {
        public string permission_gid { get; set; }
        public List<rejectcompoff_list> rejectcompoff_list { get; set; }

    }
    public class rejectcompoff_list : rejectcompoff
    {

        public string employee_gid { get; set; }
        public string attendance_date { get; set; }
    }
}