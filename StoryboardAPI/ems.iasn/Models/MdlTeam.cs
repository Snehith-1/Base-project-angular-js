using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.iasn.Models
{
    public class MdlAddTeam
    {
        public string team_gid { get; set; }
        public string team_code { get; set; }
        public string team_name { get; set; }
        public string description { get; set; }
        public string zonal_name { get; set; }
        public string team_mailid { get; set; }
        public List<MdlRm> MdlRmList { get; set; }
        public List<MdlChecker> MdlCheckerList { get; set; }
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class MdlRm
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string acknowledgement_status { get; set; }
    }
    public class MdlChecker
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
       
    }
    public class MdlTeamSummaryList:result
    {
        public List<MdlTeamSummary> MdlTeamSummary { get; set; }
    }

    public class MdlTeamSummary
    {
        public string team_gid { get; set; }
        public string team_code { get; set; }
        public string team_name { get; set; }
        public string team_mailid { get; set; }
        public string zone_name { get; set; }
        public string description { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
}