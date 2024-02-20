using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{

    public class idasTrnLsaReportSummary : result
    {
        public List<customersanction_list> customersanction_list { get; set; }
        public List<idasTrnLsaReportSummaryList> idasTrnLsaReportSummaryList { get; set; }
        public string lsaref_no { get; set; }
        public string lsacreated_date { get; set; }
        public string sanctionref_no { get; set; }
        public string sanction_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string lsaapproved_by { get; set; }
        public string lsaapproved_date { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string customer2sanction_gid { get; set; }
        public string created_date { get; set; }

    }
    public class idasTrnLsaReportSummaryList
    {
        public string lsaref_no { get; set; }
        public string lsacreated_date { get; set; }
        public string sanctionref_no { get; set; }
        public string sanction_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string lsaapproved_by { get; set; }
        public string lsaapproved_date { get; set; }
        public string customer_gid { get; set; }
        public string customer2sanction_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
    }
    public class idasTrnLsaReport : result
    {
        public List<idasTrnLsaReportSummaryList> idasTrnLsaReportSummaryList { get; set; }
    }

    public class idasLsaReportSummary : result
    {
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string lscloudpath { get; set; }
        public string customer_gid { get; set; }
        public string customer2sanction_gid { get; set; }
    }
    public class customersanction_list
    {
        public string sanctionref_no { get; set; }
        public string customer2sanction_gid { get; set; }
    }
}