using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.iasn.Models
{
    public class MdlCreateZone
    {
        public string zone_name { get; set; }
        public string acknowledgement_flag { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public List<employee_list> employee_list { get; set; }
    }

    public class MdlUpdateZone
    {
        public string zone_gid { get; set; }
        public string acknowledgement_flag { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string zone_name { get; set; }
        public List<employee_list> employee_list { get; set; }
    }

    public class MdlZoneEdit
    {
        public string zone_name { get; set; }
        public string zone_gid { get; set; }
        public string zoneref_no { get; set; }
        public List<employee_list> employee_list { get; set; }
        public List<MdlRm> MdlRmList { get; set; }
    }

    public class MdlZoneSummaryList
    { 
        public List<MdlZoneSummary> MdlZoneSummary { get; set; }
        public List<MdlRMStatusSummary> MdlRMStatusSummary { get; set; }
    }

    public class MdlRMStatusSummary : result
    {
        public string acknowledgement_status { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

    public class MdlZoneSummary
    {
        public string zone_name { get; set; }
        public string zone_gid { get; set; }
        public string zoneref_no { get; set; }
    }

    public class MdlEmployeeList
    {
        public List<employee_list> employee_list { get; set; }
    }
}