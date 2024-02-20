using System.Collections.Generic;

/// <summary>
/// (It's used for Constitution Master) Constitution Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala and Logapriya</remarks>


namespace ems.master.Models
{
    public class MdlConstitution : result
    {
        public List<constitution_list> constitution_list { get; set; }
    }
    public class constitution_list
    {
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string constitution_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }
    }
    public class constitution : result
    {
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
        public string constitution_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
    public class constitutionSummary : result
    {
        public string lscloudpath { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
    }
}