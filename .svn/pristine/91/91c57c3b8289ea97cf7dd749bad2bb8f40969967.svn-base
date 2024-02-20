using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.businessteam.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class MdlMstMarketingSourceOfContact : result
    {
        public List<application_list> application_list { get; set; }    
    }
    public class application_list
    {
        public string marketingsourceofcontact_gid { get; set; }
        public string marketingsourceofcontact_name { get; set; }      
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
    }
    public class application360 : result
    {
        public string marketingsourceofcontact_gid { get; set; }
        public string marketingsourceofcontact_name { get; set; }      
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
    public class ApplicationInactiveHistory : result
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