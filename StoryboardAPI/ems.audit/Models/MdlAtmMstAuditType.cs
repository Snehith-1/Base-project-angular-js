using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.audit.Models
{
    public class MdlAtmMstAuditType : result
   
    {
        public string audittype_gid { get; set; }
        public string audittype_name { get; set; }
        public string audittype_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public List<audittype_list> audittype_list { get; set; }
        public List<inactiveaudittype_list> inactiveaudittype_list { get; set; }

    }
    public class audittype_list : result
    {
        public string audittype_gid { get; set; }
        public string audittype_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string audittype_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
    }
    public class inactiveaudittype_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}