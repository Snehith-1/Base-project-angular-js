﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.hrm.Models
{
    public class mdlmyLeave :result
    {
        public List<leavetype_details> leavetype_details { get; set; }
    }
    public class leavetype_details
    {
        public string month { get; set; }
        public string leavetype { get; set; }
        public string count_leavetype { get; set; }
        public string duration { get; set; }
        public string count_sl { get; set; }
        public string count_cl { get; set; }
        public string count_compoff { get; set; }
    }
    //public class leave_listing
    //{
    //    public List<leaves> leaver { get; set; }
    //}
    //public class leaves
    //{
    //    public Dictionary<string,string> leaveses { get; set; }
    //}
}
