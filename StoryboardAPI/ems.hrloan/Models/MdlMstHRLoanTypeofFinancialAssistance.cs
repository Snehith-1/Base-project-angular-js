using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class MdlMstHRLoanTypeofFinancialAssistance : result
    {
        public List<typeoffinancialassistance_list> typeoffinancialassistance_list { get; set; }
    }
    public class typeoffinancialassistance_list
    {
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string tenure { get; set; }
    }
    public class typeoffinancialassistance : result
    {
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class TypeofFinancialAssistanceInactiveHistory : result
    {
        public List<inactivehistory_list> inactivehistory_list { get; set; }
    }
    public class inactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}