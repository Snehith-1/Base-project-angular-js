using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.foundation.Models
{
    //public class result
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; }
    //}
    public class MdlFndMstCategoryTypeMaster : result

    {
        public string categorytype_gid { get; set; }
        public string categorytype_name { get; set; }
        public string categorytype_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public List<categorytype_list> categorytype_list { get; set; }
        public List<inactivecategorytype_list> inactivecategorytype_list { get; set; }

    }
    public class categorytype_list : result
    {
        public string categorytype_gid { get; set; }
        public string categorytype_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string categorytype_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
    }
    public class inactivecategorytype_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}