﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
    public class MdlIciciReconcillation : result
    {
        public List<Icici_upload_list> upload_list { get; set; }
        public string bank_gid { get; set; }
        public string bank_name { get; set; }
    }
    public class Icici_upload_list
    {
        public string reconcildoc_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string lspath { get; set; }

    }
}