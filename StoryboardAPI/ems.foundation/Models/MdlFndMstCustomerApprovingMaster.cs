using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ems.foundation.Models
{
    public class MdlFndMstCustomerApprovingMaster : result
    {


        public string customerapproving_gid { get; set; }
        public string customerapproving_name { get; set; }
        public string customerapproving_code { get; set; }
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
        public string customer_name { get; set; }
        public string customer_gid { get; set; }
        public string approver_name { get; set; }
        public string approver_gid { get; set; }
        public List<customerapproving_list> acustomerapproving_list { get; set; }
        public List<customerapproving_list> customerapproving_list { get; set; }
        public List<employeeem_list> employeeem_list { get; set; }
        public List<customerapprovingmaker_list> customerapprovingmaker_list { get; set; }
        public List<customerapprovingchecker_list> customerapprovingchecker_list { get; set; }
        public List<customerapprovingapprover_list> customerapprovingapprover_list { get; set; }
        public List<employee> employee { get; set; }
        //public List<customerapprovingchecker_list> customerapprovingchecker_list { get; set; }

    }
    public class customerapproving_list : result
    {
        public string customerapproving_gid { get; set; }
        public string customerapproving_name { get; set; }
        public string customerapproving_code { get; set; }
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
        public string customer_name { get; set; }
        public string approver_name { get; set; }
        public string approver_gid { get; set; }
    }
    public class inactivecustomerapproving_list
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
    public class customerapprovingmaker_list
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class customerapprovingchecker_list
    {
        public string employee_name { get; set; }
        public string customerapproving_gid { get; set; }
    }
    public class customerapprovingapprover_list
    {
        public string approver_name { get; set; }
        public string customerapproving2employee_gid { get; set; }
    }
    //public class customerapprovingchecker_list
    //{
    //    public string employee_name { get; set; }
    //    public string customerapproving_gid { get; set; }
    //}
    public class employee
    {
        public string customerapproving2employee_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class employeeem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class CustomerApprovingemployee : result
    {
        public string employee_name { get; set; }
        public string customerapproving_name { get; set; }
    }
}