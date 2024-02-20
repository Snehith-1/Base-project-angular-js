using System.Collections.Generic;

/// <summary>
/// (It's used for Address type master) AddressType Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya,Praveen Raj,Sundar Rajan,Venkatesh and Pechiammal</remarks>
namespace ems.master.Models
{
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
        public string api_code { get; set; }
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