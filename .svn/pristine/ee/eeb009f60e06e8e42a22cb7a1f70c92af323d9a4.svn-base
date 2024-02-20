using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class MdlMstHRLoanHRMappingApprovals : result
    {
        public string hrmapping_gid { get; set; }
        public string hrmapping_name { get; set; }
        public string hrmapping_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public List<hrmapping_list> hrmapping_list { get; set; }
        public List<inactivehistory_list> inactivehistory_list { get; set; }
        public List<employeeem_list> employeeem_list { get; set; }
        public List<employeelist> employeelist { get; set; }        
        public List<employee> employee { get; set; }
    }
    public class hrmapping_list : result
    {
        public string hrmapping_gid { get; set; }
        public string hrmapping_name { get; set; }
        public string hrmapping_code { get; set; }
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
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class inactivehrmapping_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class employeelist
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
   
   
    public class employeeem_list
    {
        public string hrmapping2employee_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class employee
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class hrmappingemployee : result
    {
        public string employee_name { get; set; }
        public string hrmapping_name { get; set; }
    }
}