using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ems.ecms.Models
{
    /// <summary>
    /// ecmsdashboard Controller Class containing API methods for accessing the  Model class MdlDashboardecms
    /// Checking the preivilege for deferral and dashboard
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class MdlDashboardecms:result
    {           
            public string privilege_deferral { get; set; }               
    }
    public class ecmsprivilege
    {
        public List<ecmsprivilege_list> ecmsprivilege_list { get; set; }
    }
    public class ecmsprivilege_list
    {
        public string ecmsprivilege { get; set; }

    }

}