using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.audit.Models
{



    public class MdlAtmMstAuditFrequency : result 
    {

        public string auditfrequency_gid { get; set; }
        public string auditfrequency_name { get; set; }
        public string auditfrequency_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public List<auditfrequency_list> auditfrequency_list { get; set; }
        public List<inactiveauditfrequency_list> inactiveauditfrequency_list { get; set; }

    }


    public class auditfrequency_list : result
    {
        public string auditfrequency_gid { get; set; }
        public string auditfrequency_name { get; set; }
        public string auditfrequency_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }

    }

    public class inactiveauditfrequency_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

}
