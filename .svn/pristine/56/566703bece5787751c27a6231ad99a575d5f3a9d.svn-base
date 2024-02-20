
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for pages in CAD Group Master page)CADGroupAssignment Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlMstCADGroupAssignment : result
    {
        internal List<Cadmaker> Cadmaker_list;

        public string cadgroupassign_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_name { get; set; }
        public string program_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string cadgroup_gid { get; set; }
        public string menu_name { get; set; }
        public string menu_gid { get; set; }
        public string maker_name { get; set; }
        public string maker_gid { get; set; }
        public string checker_name { get; set; }
        public string checker_gid { get; set; }
        public string approver_name { get; set; }
        public string approver_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public List<MdlCADmenulist> menu_list { get; set; }
        public List<Cadmaker> maker { get; set; }
        public List<Cadchecker>checker { get; set; }
        public List<cadapprover> approver { get; set; }
        public List<MdlCADGroupAssignment> group_list { get; set; }
        public List<Cadassign_list> Cadassign_list { get; set; }
    }
    public class MdlCADGroupAssignment : result
    {
        public string cadgroupassign_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_name { get; set; }
        public string program_gid { get; set; }
        public string cadgroup_name { get; set; }
        public string cadgroup_gid { get; set; }
        public string menu_name { get; set; }
        public string menu_gid { get; set; }
        public string maker_name { get; set; }
        public string maker_gid { get; set; }
        public string checker_name { get; set; }
        public string checker_gid { get; set; }
        public string approver_name { get; set; }
        public string approver_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string api_code { get; set; }
    }
    public class MdlCADmenulist : result
    {
        public string menu_name { get; set; }
        public string menu_gid { get; set; }
    }
    public class Cadmaker :result
    {
        public string cadgroupmaker_gid { get; set; }
        public string cadgroupmaker { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class Cadchecker : result
    {
        public string cadgroupchecker_gid { get; set; }
        public string cadgroupchecker { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class cadapprover : result
    {
        public string cadgroupapprover_gid { get; set; }
        public string cadgroupapprover { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    //public class cadgroupmembers : result
    //{
    //    public string cadgroupmaker { get; set; }
    //    public string cadgroupchecker { get; set; }
    //}
    public class Cadassign_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
}