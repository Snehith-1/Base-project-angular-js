using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlPhyDocSummary : result
    {
        public string sanction_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string sanction_amount { get; set; }
        public string tagged_document { get; set; }
        public string phydocverified_count { get; set; }
        public string scandocverified_count { get; set; }
        public string batch_status { get; set; }
        public string fileref_no { get; set; }
        public string maker_name { get; set; }
        public string ecms_status { get; set; }
        public List<MdlEcmsStatus> MdlEcmsStatusList { get; set; }

    }
    public class MdlPhyDocSummaryList:result 
    {
        public List<MdlPhyDocSummary> MdlPhyDocSummary { get; set; }
       

    }

    public class MdlPhyDocUnverifiedCount
    {
        public string phydocunverified_count { get; set; }
    }

    public class MdlBatch:result
    {
        public string customer_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string sanction_gid { get; set; }
        public string sanctionref_no { get; set; }
    }

    public class MdlEcmsStatus
    {
        public string record_id { get; set; }
        public string approval_status { get; set; }
    }
}