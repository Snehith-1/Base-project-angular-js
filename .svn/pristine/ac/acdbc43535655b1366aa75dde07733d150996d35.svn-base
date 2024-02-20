using System.Collections.Generic;

namespace ems.master.Models
{
    public class MdlMstGroupWaiver : result
    {
        public string groupwaiver_gid { get; set; }
        public string groupwaiver_name { get; set; }
        public string description { get; set; }
        public string groupwaiver_code { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public List<groupwaiver> groupwaiver { get; set; }
        public List<groupwaiverinactivehistory_list> groupwaiverinactivehistory_list { get; set; }
        public List<groupassignmember> groupassignmember { get; set; }
        public List<assignmember> assignmember { get; set; }
        public List<assignmember_list> assignmember_list { get; set; }
    }

    public class groupwaiver : result
    {
        public string groupwaiver_gid { get; set; }
        public string groupwaiver_code { get; set; }
        public string groupwaiver_name { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string api_code { get; set; }
    }

    public class groupwaiverinactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    public class assignmember
    {
        public string assignmember_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class groupassignmember : result
    {
        public string groupassignmembers { get; set; }
       

    }
    public class assignmember_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
}