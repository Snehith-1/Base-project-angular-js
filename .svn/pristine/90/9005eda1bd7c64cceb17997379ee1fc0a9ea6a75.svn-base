using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.audit.Models
{
    
    public class MdlAtmMstAuditMapping : result
    {


        public string auditmapping_gid { get; set; }
        public string auditmapping_name { get; set; }
        public string auditmapping_code { get; set; }
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
        public List<auditmapping_list> auditmapping_list { get; set; }
        public List<inactivehistory_list> inactivehistory_list { get; set; }
        public List<employeeem_list> employeeem_list { get; set; }
        public List<auditmappingmaker_list> auditmappingmaker_list { get; set; }
        public List<auditmappingchecker_list> auditmappingchecker_list { get; set; }
        public List<auditmappingapprover_list> auditmappingapprover_list { get; set; }
        public List<employee> employee { get; set; }
        public List<auditorchecker_list> auditorchecker_list { get; set; }

    }
    public class auditmapping_list : result
    {
        public string auditmapping_gid { get; set; }
        public string auditmapping_name { get; set; }
        public string auditmapping_code { get; set; }
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
    public class inactiveauditmapping_list
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
    public class auditmappingmaker_list
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class auditmappingchecker_list
    {
        public string employee_name { get; set; }
        public string auditmapping_gid { get; set; }
    }
    public class auditmappingapprover_list
    {
        public string approver_name { get; set; }
        public string auditmapping2employee_gid { get; set; }
    }
    public class auditorchecker_list
    {
        public string employee_name { get; set; }
        public string auditmapping_gid { get; set; }
    }
    public class employee
    {
        public string auditmapping2employee_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class employeeem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class Auditmappingemployee : result
    {
        public string employee_name { get; set; }
        public string auditmapping_name { get; set; }
    }
}