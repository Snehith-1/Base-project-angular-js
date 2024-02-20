using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{

    /// <summary>
    /// This Models will store values to fetch data from CC Members master in custopedia.
    /// </summary>
    /// <remarks>Written by Abilash.A, Premchander.K </remarks>

    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string master_gid { get; set; }
        public string master_name { get; set; }
        public string deleted_by { get; set; }
        public string deleted_date { get; set; }
        public string master_value { get; set; }

    }
    public class MdlMstCCMember :result
    {
       public string CCMember_name { get; set; }
        public string ccmember_gid { get; set; }
        public string ccmembermaster_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string ccgroupname_gid { get; set; }
        public List<ccmember_list> ccmember_list { get; set; }
        public List<ccgroup_list> ccgroup_list { get; set; }
    }
    public class ccmember_list
    {
        public string CCMember_name { get; set; }
        public string ccmember_gid { get; set; }
        public string ccmembermaster_gid { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string attendance_status { get; set; }
        public string approval_status { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string ccapproval_flag { get; set; }
        public string overapproval_status { get; set; }
        public string momapproval_flag { get; set; }

    }
    public class ccmembers_list
    {
        public string CCMember_name { get; set; }
        public string ccmember_gid { get; set; }
        public string ccmembermaster_gid { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string attendance_status { get; set; }
        public string approval_status { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string ccapproval_flag { get; set; }
        public string overapproval_status { get; set; }
        public string approvalinitiate_by { get; set; }
        public string approved_date { get; set; }
        public string lsemployee_name { get; set; }
        public string lsemployee_gid { get; set; }



    }

    public class ccmail_list
    {
        public string application_no { get; set; }
        public string ccadmin_name { get; set; }
        public string customer_name { get; set; }
        public string loanfacility_amount { get; set; }
        public string ccmeeting_date { get; set; }
        public string Rm_name { get; set; }
       



    }
    public class ccmemberlog_list
    {
        public string CCMember_name { get; set; }
        public string ccmember_gid { get; set; }
        public string ccmembermaster_gid { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string attendance_status { get; set; }
        public string approval_status { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class ccgroupname:result
    {
        public string ccgroup_code { get; set; }
        public string ccgroup_name { get; set; }
        public string ccgroupname_gid { get; set; }
        public List<ccgroup_list> ccgroup_list { get; set; }
    }
    public class ccgroup_list
    {
        public string ccgroup_code { get; set; }
        public string ccgroup_name { get; set; }
        public string ccgroupname_gid { get; set; }
    }
}