using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values for business approval process in aplication creation flow.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>

    public class mdlcomment : result
    {
        public string applicationapproval_gid { get; set; }
        public string application_gid { get; set; }
        public string commenttitle { get; set; }
        public string commentdesc { get; set; }

    }
    public class mdlcommentdesc : result
    {
        public string commentdesc { get; set; }

    }
    public class applicationapproval : result
    {
        public List<applicationapprovallist> applicationapprovallist { get; set; }
        public List<applicationcommentslist> applicationcommentslist { get; set; }
    }
    public class applicationapprovallist 
    {
        public string applicationapproval_gid { get; set; }
        public string approval_name { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string approved_date { get; set; }
        public string hierary_level { get; set; }
        public string user_code { get; set; }
        public string rejected_date { get; set; }
        public string hold_date { get; set; }
    }
    public class applicationcommentslist
    {
        public string applicationcomment_gid { get; set; }
        public string applicationapproval_gid { get; set; }
        public string applicaiton_gid { get; set; }
        public string commenttitle { get; set; }
        public string commentdesc { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string commenttatus { get; set; }
        public string hierary_level { get; set; }
        public string close_remarks { get; set; }
    }
 
    
    public class applicationhierarchy : result
    {
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string clusterhead { get; set; }
        public string regionhead { get; set; }
        public string zonalhead { get; set; }
        public string businesshead { get; set; }
    }
    public class applicationdetials : result
    {
        public string program_name { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string region { get; set; }
        public string overalllimit_amount { get; set; }
        public string shortclosing_reason { get; set; }
        public string expired_flag { get; set; }
        public List <productlist> productlist { get; set; }
    }
    public class productlist
    {
        public string product_name { get; set; }
        public string facility_limit { get; set; }
        public string tenure_limit { get; set; }
    }

    public class mdlappapproval : result
    {
        internal string created_by;

        public string applicationapproval_gid { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string application_gid { get; set; }
    }
    public class mdlcommentstatus :result
    {
        public string commentstatus_flag { get; set; }
        public string approved_flag { get; set; }
    }
    public class businessApplicationCount : result
    {
        public string newbusinessapplication_count { get; set; }
        public Int16 rejectedapplication_count { get; set; }
        public Int16 holdapplication_count { get; set; }
        public Int16 lstotalcount { get; set; }
        public string approvedapplication_count { get; set; }
        public string upcomingbusinessapplication_count { get; set; }
        public List <rejectedlist> rejectedlist { get; set; }
        public List<Holdlist> Holdlist { get; set; }
    }
    public class Mdlproceedapproval : result
    {
        public string proceedtoapproval_flag { get; set; }

    }
    public class rejectedlist
    {
        public string rejected_counts { get; set; }
    }
    public class Holdlist
    {
        public string hold_counts { get; set; }
    }
}