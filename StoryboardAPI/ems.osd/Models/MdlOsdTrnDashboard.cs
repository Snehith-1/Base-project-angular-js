using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{
    public class MdlOsdTrnDashboard : result
    {
        public string privilege_deferral { get; set; }
    }
    public class osdprivilege
    {
        public List<osdprivilege_list> osdprivilege_list { get; set; }
    }
    public class osdprivilege_list
    {
        public string osdprivilege { get; set; }

    }
}