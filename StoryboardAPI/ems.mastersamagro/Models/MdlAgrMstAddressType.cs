using System.Collections.Generic;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values for Address Type master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>

    public class MdlAddressType : result
    {
        public List<addresstype_list> addresstype_list { get; set; }
    }
    public class addresstype_list
    {
        public string address_gid { get; set; }
        public string address_type { get; set; }
        public string address_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
    public class addresstype : result
    {
        public string address_gid { get; set; }
        public string address_type { get; set; }
        public string address_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
}