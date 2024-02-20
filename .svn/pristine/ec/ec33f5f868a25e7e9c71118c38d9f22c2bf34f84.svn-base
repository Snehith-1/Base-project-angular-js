using System.Collections.Generic;

namespace ems.master.Models
{
    public class MdlMstSanctionWaiver : result
    {
        public string sanctionwaiver_gid { get; set; }
        public string sanctionwaiver_name { get; set; }
        public string description { get; set; }
        public string sanctionwaiver_code { get; set; }
        public string Status { get; set; }   
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public List<sanctionwaiver> sanctionwaiver { get; set; }
        public List<sanctioninactivehistory_list> sanctioninactivehistory_list { get; set; }
    }

    public class sanctionwaiver : result
    {
        public string sanctionwaiver_gid { get; set; }
        public string sanctionwaiver_code { get; set; }
        public string sanctionwaiver_name { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string api_code { get; set; }


    }
   
    public class sanctioninactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

}