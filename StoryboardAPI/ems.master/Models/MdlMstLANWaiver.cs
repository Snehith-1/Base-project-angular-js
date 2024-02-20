using System.Collections.Generic;

namespace ems.master.Models
{
    public class MdlMstLANWaiver : result
    {
        public string lanwaiver_gid { get; set; }
        public string lanwaiver_name { get; set; }
        public string description { get; set; }
        public string lanwaiver_code { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public List<lanwaiver> lanwaiver { get; set; }
        public List<laninactivehistory_list> laninactivehistory_list { get; set; }
    }

    public class lanwaiver : result
    {
        public string lanwaiver_gid { get; set; }
        public string lanwaiver_code { get; set; }
        public string lanwaiver_name { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string api_code { get; set; }


    }

    public class laninactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

}