using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ems.hrm.Models
{

    public class myteam : result
    {
        public List<myteam_list> myteam_list { get; set; }
    }
    public class myteam_list
    {
        public string employee_code { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string employee_mobileno { get; set; }
        public string employee_photo { get; set; }
    }
}
