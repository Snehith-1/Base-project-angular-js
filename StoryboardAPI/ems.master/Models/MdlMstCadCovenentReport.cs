using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for Softcopy and Original copy report page in Samfin)CadCovenentReport Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class reportscannedmakerapplicationlist : result
    {
        public List<reportscannedmakerapplication> reportscannedmakerapplication { get; set; }
    }

    public class reportscannedmakerapplication
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string ccapproved_date { get; set; }
        public string creditgroup_name { get; set; }
        public string ccgroup_name { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadgroup_name { get; set; }
        public string cadaccepted_date { get; set; }
        public string processtypeassign_gid { get; set; }
        public string maker_name { get; set; }

        public string checker_name { get; set; }
        public string approver_name { get; set; }
        public string customer_urn { get; set; }

        public string sanction_refno { get; set; }
    }

    public class reportphyiscalmakerapplicationlist : result
    {
        public List<reportphyiscalmakerapplication> reportphyiscalmakerapplication { get; set; }
    }

    public class reportphyiscalmakerapplication
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string ccapproved_date { get; set; }
        public string creditgroup_name { get; set; }
        public string ccgroup_name { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadgroup_name { get; set; }
        public string cadaccepted_date { get; set; }
        public string processtypeassign_gid { get; set; }
        public string maker_name { get; set; }

        public string checker_name { get; set; }

        public string approver_name { get; set; }

        public string overall_approvalstatus { get; set; }

        public string customer_urn { get; set; }

        public string sanction_refno { get; set; }
    }


    public class Mdlscannedapplexport : result
    {
        public string lsname { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }

        public string lsstatus { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string ccapproved_date { get; set; }
        public string creditgroup_name { get; set; }
        public string ccgroup_name { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadgroup_name { get; set; }
        public string cadaccepted_date { get; set; }
        public string processtypeassign_gid { get; set; }
        public string maker_name { get; set; }

        public string checker_name { get; set; }

        public string approver_name { get; set; }


        public string customer_urn { get; set; }

        public string sanction_refno { get; set; }

    }
}