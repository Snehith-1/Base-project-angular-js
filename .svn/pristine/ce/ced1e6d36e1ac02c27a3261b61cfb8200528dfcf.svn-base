using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlRMMapping : result
    {
        public string employee_count { get; set; }
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_gid { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string level_two { get; set; }
        public string level_three { get; set; }
        public List<employee_list1> employee_list1 { get; set; }
    }
    public class employee_list1
    {
        public string employee_gid { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string level_two { get; set; }
        public string level_three { get; set; }
        public List<employee_list2> employee_list2 { get; set; }
    }
    public class employee_list2
    {
        public string employee_gid { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string level_two { get; set; }
        public string level_three { get; set; }
        public List<employee_list3> employee_list3 { get; set; }
    }
    public class employee_list3
    {
        public string employee_gid { get; set; }
        public string level_four { get; set; }
        public string level_two { get; set; }
        public string level_three { get; set; }
        public List<employee_list4> employee_list4 { get; set; }
    }
    public class employee_list4
    {
        public string employee_gid { get; set; }
        public string level_five { get; set; }
        public string level_two { get; set; }
        public string level_three { get; set; }

    }
    public class MdlRMMappingview : result
    {
        public string employee_count { get; set; }
        public string baselocation_gid { get; set; }
        public string vertical_gid { get; set; }
        public string employeegid { get; set; }
        public string program_gid { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
        public List <MdlRMMappingviewlist> MdlRMMappingviewlist { get; set; }
        public List<MdlRMMappingexllist> MdlRMMappingexllist { get; set; }
    }
    public class MdlRMMappingviewlist : result
    {
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string clusterhead { get; set; }
        public string regionhead { get; set; }
        public string zonalhead { get; set; }
        public string businesshead { get; set; }
    }
    public class MdlRMMappingexllist
    {
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string clusterhead { get; set; }
        public string regionhead { get; set; }
        public string zonalhead { get; set; }
        public string businesshead { get; set; }
        public string vertical { get; set; }
        public string program { get; set; }
    } 

    public class locationemployee : result
    {
        public List<employeelists> employeelists { get; set; }
    }
    public class employeelists
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class MdlRMMappingverticalview :result
    {
        public string employee_gid { get; set; }
        public string employee_count { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; } 
        public List<verticallist> verticallist { get; set; }
       
    }
    public class verticallist
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_name { get; set; }
        public List<viewlist> viewlist { get; set; }
    }

    public class viewlist
    {
        public string clusterhead { get; set; }
        public string regionhead { get; set; }
        public string zonalhead { get; set; }
        public string businesshead { get; set; }
        public string program_name { get; set; }
        public List <headviewlist> headviewlist { get; set; }
    }
    public class headviewlist
    {
        public string clusterhead { get; set; }
        public string regionhead { get; set; }
        public string zonalhead { get; set; }
        public string businesshead { get; set; }
    }
}