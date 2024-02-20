using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{
    
    public class supportteamdtl : result
    {
        public string supportteam_gid { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public string team_code { get; set; }
        public string team_name { get; set; }
        public string team_description { get; set; }
        public string employee_gid { get; set; }
        public List<teammembers> teammembers { get; set; }
    }

    public class teammembers
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string supportteam2member_gid { get; set; }
    }


    public class supportdtllist
    {
        public List<supportdtl> supportdtl { get; set; }
    }
    public class activitylist
    {
        public List<activitydtl> activitydtl { get; set; }
    }
    public class supportdtl
    {
        public string supportteam_gid { get; set; }
        public string department_name { get; set; }
        public string department_gid { get; set; }
        public string team_code { get; set; }
        public string team_name { get; set; }
        public string team_description { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class supportdtlview
    {
        public string supportteam_gid { get; set; }
        public string team_code { get; set; }
        public string team_name { get; set; }
        public string team_description { get; set; }
    }


    public class supportteamviewdtl
    {
        public string supportteam_gid { get; set; }
        public string department_name { get; set; }
        public string department_gid { get; set; }
        public string team_code { get; set; }
        public string team_name { get; set; }
        public string team_description { get; set; }
        public string servicerequest_gid { get; set; }
        public List<teammembers> teammembers { get; set; }
        public List<employeelist> employeelist { get; set; }
    }

    public class employeelist
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

}