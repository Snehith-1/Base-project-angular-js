using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ep.Models
{

    public class allocationdtlList : result
    {
        public List<allocationdtl> allocationdtl { get; set; }
    }

    public class allocationdtl : result
    {
        public string RMmapping_gid { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string assigned_RM { get; set; }
        public string assignedRM_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_gid { get; set; }
        public string state_gid { get; set; }
        public string district_gid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string requested_by { get; set; }
        public string requested_date { get; set; }
        public string ZonalRM_gid { get; set; }
        public string vertical { get; set; }
        public string location { get; set; }
        public string allocation_flag { get; set; }
        public string completed_flag { get; set; }
        public string allocation_status { get; set; }
        public string external_assignedGid { get; set; }
        public string target_date { get; set; }
    }
        public class visistreportcancelList : result
        {
            public List<visistreportcancel> visistreportcancel { get; set; }
        }
        public class visistreportcancel : result
        {
            public string cancel_remarks { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }
        }

    }

