using System.Collections.Generic;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values from designation master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>

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