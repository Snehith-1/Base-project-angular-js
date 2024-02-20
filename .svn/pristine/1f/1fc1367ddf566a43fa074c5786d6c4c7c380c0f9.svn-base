using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{

    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }

        public string lsname { get; set; }
        public string lscloudpath { get; set; }
        public string lspath { get; set; }
        public string banktransc_gid { get; set; }
    }

    public class activityadd :result
    {
        public string activitymaster_gid { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public string activity_code { get; set; }
        public string supportteam_gid { get; set; }
        public string activity_name { get; set; }
        public string supportteam_name { get; set; }
        public string activity_tat { get; set; }
        public string servicerequest_gid { get; set; }
    }

    public class actvitydtllist
    {
        public List<activitydtl> activitydtl { get; set; }
    }
    public class activitydtl
    {
        public string activitymaster_gid { get; set; }
        public string activity_code { get; set; }
        public string department_name { get; set; }
        public string activity_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string supportteam_name { get; set; }
        public string activity_tat { get; set; }
        public string servicerequest_gid { get; set; }
    }
}