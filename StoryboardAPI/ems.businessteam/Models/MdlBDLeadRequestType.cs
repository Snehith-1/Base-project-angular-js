using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.businessteam.Models
{
    public class MdlBDLeadRequestType : result
    {

        public List<leadrequesttype_list> leadrequesttype_list { get; set; }
    }
    public class leadrequesttype_list
    {
        public string leadrequesttype_gid { get; set; }
        public string leadrequesttype_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
    }
    public class leadrequesttype : result
    {

        public string leadrequesttype_gid { get; set; }
        public string leadrequesttype_name { get; set; }
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
    public class leadrequesttypeInactiveHistory : result
    {
        public List<leadrequesttypeinactivehistory_list> leadrequesttypeinactivehistory_list { get; set; }
    }
    public class leadrequesttypeinactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}
