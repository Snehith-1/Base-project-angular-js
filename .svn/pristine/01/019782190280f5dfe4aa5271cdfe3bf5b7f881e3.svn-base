using System.Collections.Generic;

/// <summary>
/// (It's used for Designation Master) Designation Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlDesignation : result
    {
        public List<designation_list> designation_list { get; set; }
    }
    public class designation_list
    {
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string designation_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }
    }
    public class designation : result
    {
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string designation_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
    }
}