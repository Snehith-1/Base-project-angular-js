﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
   
    public class Mdlemployee :  result
    {
        public List<employeelist> employeelist { get; set; }
    }
    public class employeelist
    {
        public string program2approval_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
}