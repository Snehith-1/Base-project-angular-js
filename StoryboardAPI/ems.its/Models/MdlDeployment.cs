using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.its.Models
{
    public class client_list
    {
        public List<client> clients { get; set; }
    }
    public class client
    {
        public string client_gid { get; set; }
        public string client_name { get; set; }
    }
    public class project_list
    {
        public List<project> projects { get; set; }
    }
    public class project
    {
        public string project_gid { get; set; }
        public string project_name { get; set; }
    }

    public class add
    {
        public string client_gid { get; set; }
        public string client_name { get; set; }
        public string description { get; set; }
        public string need { get; set; }
        public bool new_page { get; set; }
        public bool new_report { get; set; }
        public string pages { get; set; }
        public string reports { get; set; }
        public string stage { get; set; }
        public List<project> projects { get; set; }
        public string priority { get; set; }
        public List<approval> approvals { get; set; }
        public string approver_name { get; set; }
        public bool mailFlag { get; set; }
    }
    public class approval
    {
        public string depAppGid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string status { get; set; }
        public string approver { get; set; }
    }

    public class summary_list
    {
        public string count_uat { get; set; }
        public string count_test { get; set; }
        public string count_live { get; set; }
        public string count_approvalPending { get; set; }
        public string count_approvalRejected { get; set; }
        public string count_deploymentPending { get; set; }
        public string count_deploymentDone { get; set; }
        public string count_deploymentRejected { get; set; }
        public bool deployer { get; set; }
        public List<summary> summaries { get; set; }
    }
    public class summary
    {
        public string deployment_gid { get; set; }
        public string deployment_code { get; set; }
        public string created_date { get; set; }
        public string stage { get; set; }
        public string client_name { get; set; }
        public string module_name { get; set; }
        public string status { get; set; }

        public string raised_by { get; set; }

    }

    public class edit
    {
        public string client_gid { get; set; }
        public string module_gid { get; set; }
        public string stage { get; set; }
        public string need { get; set; }
        public string pages { get; set; }
        public string reports { get; set; }
        public string description { get; set; }
        public List<approval> approvals { get; set; }
        public string priority { get; set; }
    }

    public class update : add
    {
        public string deployment_gid { get; set; }
        public bool status { get; set; }
    }

    public class view : edit
    {
        public string employee_gid { get; set; }
        public string deployment_code { get; set; }
        public string date { get; set; }
        public string client_name { get; set; }
        public string project_name { get; set; }
        public string status { get; set; }
        public string request_from { get; set; }
        public string deployed_by { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    public class deploymentStatus
    {
        public string deployment_gid { get; set; }
        public string statusDep { get; set; }
        public string mailDep { get; set; }
    }

    public class GroupPayload
    {
        public string group_admin { get; set; }
        public string group_name { get; set; }
        public string message { get; set; }
    }
}