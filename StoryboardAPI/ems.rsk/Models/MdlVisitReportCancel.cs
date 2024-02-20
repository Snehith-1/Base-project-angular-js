using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{
   
    public class visitreportcancel:result
    {
        public string allocationdtl_gid { get; set; }
        public string cancel_reason { get; set; }
    }
    public class visitstatus : result
    {
        public string allocationdtl_gid { get; set; }
        public string visit_status { get; set; }
    }


}