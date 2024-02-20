using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
    public class MdlBRSMaster : result
    {
        //public string brsactivity_gid { get; set; }
        //public string brsactivity_name { get; set; }

        public List <BRSActivity_List> BRSActivity_List { get; set; }


    }

    public class BRSActivity_List
    {
        public string brsactivity_gid { get; set; }
        public string brsactivity_name { get; set; }
        public string remarks { get; set; }
        public string status_log { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class BRSActivity : result
    {
        public string brsactivity_gid { get; set; }
        public string brsactivity_name { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }

    }
}