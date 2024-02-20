using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;


namespace ems.businessteam.Models
{
    public class MdlMstEnquiryRequire:result 
    {
  
        public string enquiryrequire_gid { get; set; }
        public string enquiryrequire_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public List<enquiryrequire_list> enquiryrequire_list { get; set; }
        public List<inactiveenquiryrequire_list> inactiveenquiryrequire_list { get; set; }

    }
    public class enquiryrequire_list : result
    {
        public string enquiryrequire_gid { get; set; }
        public string enquiryrequire_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
    }
    public class inactiveenquiryrequire_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}