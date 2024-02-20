using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.businessteam.Models
{
    public class MdlMstMarketingCallReceivedNumber : result
    {
        public List<marketingcallreceivednumber_list> marketingcallreceivednumber_list { get; set; }
    }
    public class marketingcallreceivednumber_list
    {
        public string marketingcallreceivednumber_gid { get; set; }
        public string marketingcallreceivednumber_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
    }
    public class marketingcallreceivednumber : result
    {
        public string marketingcallreceivednumber_gid { get; set; }
        public string marketingcallreceivednumber_name { get; set; }
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
    public class MarketingCallReceivedNumberInactiveHistory : result
    {
        public List<marketingcallreceivednumberinactivehistory_list> marketingcallreceivednumberinactivehistory_list { get; set; }
    }
    public class marketingcallreceivednumberinactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}