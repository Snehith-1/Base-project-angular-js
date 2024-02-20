using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.audit.Models
{
    public class MdlAtmMstPositiveConfirmity : result
    {

        public string positiveconfirmity_gid { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string positiveconfirmity_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public List<positiveconfirmity_list> positiveconfirmity_list { get; set; }
        public List<inactivepositiveconfirmity_list> inactivepositiveconfirmity_list { get; set; }


    }


    public class positiveconfirmity_list : result
    {
        public string positiveconfirmity_gid { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string positiveconfirmity_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
    }

    public class inactivepositiveconfirmity_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

}
