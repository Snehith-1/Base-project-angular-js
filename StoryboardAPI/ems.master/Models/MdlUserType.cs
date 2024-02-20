﻿using System.Collections.Generic;

namespace ems.master.Models
{
    public class ipandlogintimemodel : result
    {
        public string ip { get; set; }
        public string login_time { get; set; }
    }
    public class MdlUserType : result
    {
        public List<usertype_list> usertype_list { get; set; }
    }
    public class usertype_list
    {
        public string usertype_gid { get; set; }
        public string user_type { get; set; }
        public string user_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }
    }
    public class usertype : result
    {
        public string usertype_gid { get; set; }
        public string user_type { get; set; }
        public string user_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
}