using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class zonalMappinglist : result
    {
        public List<zonalMapping> zonalMapping { get; set; }
    }

    public class zonalMapping : result
    {
        public string zonalmapping_gid { get; set; }
        public string zonal_name { get; set; }
        public string zonalrisk_managerGid { get; set; }
        public string zonalrisk_managername { get; set; }
        public List<tagzonalmapping> tagzonalmapping { get; set; }
        public List<assignedRMlist> assignedRMlist { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string lszonalrisk_managerGid { get; set; }
        public string lszonalrisk_managerName { get; set; }
        public string lszonal_Name { get; set; }
    }

    public class employee : result
    {
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_id { get; set; }
    }

    public class tagzonalmappinglist : result
    {
        public string zonalmapping_gid { get; set; }
        public string zonalrisk_managerGid { get; set; }
        public string[] state_gid { get; set; }
        public List<tagzonalmapping> tagzonalmapping { get; set; }
    }

    public class tagzonalmapping : result
    {
        public string zonalmapping_gid { get; set; }
        public string zonalrisk_managerGid { get; set; }
        public string state_gid { get; set; }
        public string state_name { get; set; }

    }

    public class assignedRMdtl : result
    {
        public string state_gid { get; set; }
        public string state_name { get; set; }
        public string district_gid { get; set; }
        public string district_name { get; set; }
    }

}