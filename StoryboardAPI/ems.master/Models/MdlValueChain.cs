using System.Collections.Generic;

namespace ems.master.Models
{
    public class MdlValueChain : result
    {
        public List<valuechain_list> valuechain_list { get; set; }
    }
    public class valuechain_list
    {
        public string valuechain_gid { get; set; }
        public string valuechain_code { get; set; }
        public string valuechain_name { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }
    }
    public class valuechain : result
    {
        public string valuechain_gid { get; set; }
        public string valuechain_code { get; set; }
        public string valuechain_name { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
}