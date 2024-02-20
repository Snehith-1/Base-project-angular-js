using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace ems.businessteam.Models
{
    public class MdlMarMstmilletRequire : result

    {
        public string milletrequire_gid { get; set; }
        public string milletrequire_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public List<milletrequire_list> milletrequire_list { get; set; }
        public List<inactivemilletrequire_list> inactivemilletrequire_list { get; set; }

    }
    public class milletrequire_list : result
    {
        public string milletrequire_gid { get; set; }
        public string milletrequire_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
    }
    public class inactivemilletrequire_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}