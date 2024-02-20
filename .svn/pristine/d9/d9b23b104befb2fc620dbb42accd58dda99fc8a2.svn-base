using System.Collections.Generic;

/// <summary>
///(It's used for pages in CADGroup)CADGroup Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlCadGroup : result
    {
        public string cadgroup_gid { get; set; }
        public string cadgroup_name { get; set; }
        public List<cadgroup> cadgroup { get; set; }
        public List<cadmanager> cadmanager { get; set; }
        public List<cadmembers> cadmembers { get; set; }
        public List<cadmanager_list> cadmanager_list { get; set; }
        public List<cadmembers_list> cadmembers_list { get; set; }

    }

    public class cadgroup : result
    {
        public string cadgroup_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string api_code { get; set; }

    }

    public class cadmanager
    {
        public string cadgroupmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class cadmembers
    {
        public string cadgroupmembers_gid { get; set; }
        public string member_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class cadgrouphead : result
    {
        public string cadgroupmanager { get; set; }
        public string cadgroupmember { get; set; }

    }
    public class cadmanager_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class cadmembers_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }


}