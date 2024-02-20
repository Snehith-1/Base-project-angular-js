using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
namespace ems.hrm.Models
{
    public class Result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class company_policy:Result
    {
        public List<policy_list> policy_list { get; set; }
    } 
    public class policy_list
    {
        public string company_policies { get; set; }
        public string policies_description { get; set; }
    }
}