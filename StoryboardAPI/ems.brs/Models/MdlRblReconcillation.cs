﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
    public class MdlRblReconcillation : result
    {

        public List<upload_list1> upload_list { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
    }
        
        public class upload_list1
    {
            public string reconcildoc_gid { get; set; }
            public string file_name { get; set; }
            public string file_path { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
            public string lspath { get; set; }

        }
    }