using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.businessteam.Models
{
    public class MdlMstMarketingCallType : result
    {
        
        public List<marketingcalltype_list> marketingcalltype_list { get; set; }
    }
    public class marketingcalltype_list
    {     
        public string marketingcalltype_gid { get; set; }
        public string marketingcalltype_name { get; set; }      
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
    }
    public class marketingcalltype : result
    {

        public string marketingcalltype_gid { get; set; }
        public string marketingcalltype_name { get; set; }
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
    public class MarketingCallTypeInactiveHistory : result
    {
        public List<marketinginactivehistory_list> marketinginactivehistory_list { get; set; }
    }
    public class marketinginactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}
