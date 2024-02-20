using System.Collections.Generic;

/// <summary>
/// (It's used for Business Unit Master)  Business Unit Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>
namespace ems.master.Models
{
    public class MdlBusinessUnit : result
    {
        public List<businessunit_list> businessunit_list { get; set; }
    }
    public class businessunit_list
    {
        public string businessunit_gid { get; set; }
        public string businessunit_code { get; set; }
        public string businessunit_name { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }
    }
    public class businessunit : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_code { get; set; }
        public string businessunit_name { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
}