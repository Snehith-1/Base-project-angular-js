using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{   
    public class MdlMstHRLoanTenure : result
    {
        public List<hrloantenure_list> hrloantenure_list { get; set; }
        public List<typeofdocument_list> typeofdocument_list { get; set; }
        public List<tenure_list> tenure_list { get; set; }
    }
    public class hrloantenure_list
    {       
        public string hrloantenure_gid { get; set; }
        public string hrloantenure_name { get; set; }
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
        public string hrloantenurestart_date { get; set; }
        public string hrloantenureend_date { get; set; }             
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string tenure_in_months { get; set; }
        public string with_effect_from { get; set; }
        
    }
    public class hrloantenure : result
    {
        public string hrloantenure_gid { get; set; }
        public string hrloantenure_name { get; set; }        
        public string hrloantenurestart_date { get; set; }
        public string hrloantenureend_date { get; set; }
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
    public class tenure : result
    {
        public string hrloantenure_gid { get; set; }
        public string hrloantenure_name { get; set; }
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
        public string hrloantenurestart_date { get; set; }
        public string hrloantenureend_date { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string Status { get; set; }
    }

    public class tenure_list
    {
        public string hrloantenure_gid { get; set; }
        public string hrloantenure_name { get; set; }
    }
    public class typeofdocument_list
    {
        public string hrloantypeoffinancialassistance_gid { get; set; }
        public string hrloantypeoffinancialassistance_name { get; set; }
    }
    public class HRLoanTenureInactiveHistory : result
    {
        public List<tenureinactivehistory_list> tenureinactivehistory_list { get; set; }
    }
    public class tenureinactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}