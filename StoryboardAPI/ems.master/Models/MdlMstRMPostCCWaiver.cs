using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlApplicationWaiverDetail : result
    {
        public List<string> sanctionrefno_list { get; set; }
        public List<string> urn_list { get; set; }
        public List<string> lan_list { get; set; }
        public string customer_info { get; set; }
    }

    public class MdlApplicationWaiverMaster : result
    {
        public List<sanctionwaiver_list> sanctionwaiver_list { get; set; }
        public List<lanwaiver_list> lanwaiver_list { get; set; }
        public List<waivergroup_list> waivergroup_list { get; set; }
    }
    public class sanctionwaiver_list
    {
        public string sanctionwaiver_gid { get; set; }
        public string sanctionwaiver_name { get; set; }
    }
    public class lanwaiver_list
    {
        public string lanwaiver_gid { get; set; }
        public string lanwaiver_name { get; set; }
    }
    public class waivergroup_list
    {
        public string groupwaiver_gid { get; set; }
        public string groupwaiver_name { get; set; }
    }

    public class MdlWaiverDocument : result
    {
        public string rmpostccwaiver2document_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<uploadwaiverdoc_list> uploadwaiverdoc_list { get; set; }
    }

    public class uploadwaiverdoc_list
    {
        public string rmpostccwaiver2document_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlWaiverApprovalMember : result
    {
        public string rmpostccwaiver2approvalmember_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string approval_type { get; set; }
        public string sequence_flag { get; set; }
        public string hierarchy_level { get; set; }
        public string approval_token { get; set; }
        public string approvalmember_gid { get; set; }
        public string approvalmember_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<waiverapprovalmember_list> waiverapprovalmember_list { get; set; }
    }

    public class waiverapprovalmember_list
    {
        public string rmpostccwaiver2approvalmember_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string approvalmember_gid { get; set; }
        public string approvalmember_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlRMPostCCWaiver : result
    {
        public string application_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string waiver_category { get; set; }
        public string sanction_refno { get; set; }
        public string lan { get; set; }
        public string urn { get; set; }
        public List<sanctionwaiver_list> sanctionwaiver_list { get; set; }
        public List<lanwaiver_list> lanwaiver_list { get; set; }
        public string customer_info { get; set; }
        public string waiver_title { get; set; }
        public List<waivergroup_list> waivergroup_list { get; set; }
        public string waiver_description { get; set; }
        public string waiver_amount { get; set; }
        public string approval_type { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<rmpostccwaiver_list> rmpostccwaiver_list { get; set; }
        public string sanctionwaiver_gid { get; set; }
        public string sanctionwaiver_name { get; set; }
        public string lanwaiver_gid { get; set; }
        public string lanwaiver_name { get; set; }
        public string groupwaiver_gid { get; set; }
        public string groupwaiver_name { get; set; }
        public List<sanctionwaiver_list> sanctionwaivergen_list { get; set; }
        public List<lanwaiver_list> lanwaivergen_list { get; set; }
        public List<waivergroup_list> waivergroupgen_list { get; set; }
        public List<string> lan_list { get; set; }
        public List<string> langen_list { get; set; }
    }

    public class rmpostccwaiver_list
    {
        public string rmpostccwaiver_gid { get; set; }
        public string application_gid { get; set; }
        public string waiver_category { get; set; }
        public string sanction_refno { get; set; }
        public string lan { get; set; }
        public string urn { get; set; }
        public List<sanctionwaiver_list> sanctionwaiver_list { get; set; }
        public List<lanwaiver_list> lanwaiver_list { get; set; }
        public string customer_info { get; set; }
        public string waiver_title { get; set; }
        public List<waivergroup_list> waivergroup_list { get; set; }
        public string waiver_description { get; set; }
        public string waiver_amount { get; set; }
        public string waivergroup_name { get; set; }
        public string approval_type { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }      
    }

    public class MdlWaiverApprovalDetail : result
    {       
        public List<waiverapprovaldetail_list> waiverapprovaldetail_list { get; set; }
        public string approval_token { get; set; }
    }

    public class waiverapprovaldetail_list
    {
        public string rmpostccwaiver2approvalmember_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string approvalmember_name { get; set; }
        public string approval_type { get; set; }
        public string approval_status { get; set; }
        public string apprej_date { get; set; }
        public string initiated_by { get; set; }
        public string approval_token { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class waiverapproval : result
    {
        public string rmpostccwaiver2approvalmember_gid { get; set; }
        public string rmpostccwaiver_gid { get; set; }
        public string approvalmember_name { get; set; }
        public string approval_type { get; set; }
        public string approval_status { get; set; }
        public string apprej_date { get; set; }
        public string approval_token { get; set; }
        public string initiated_by { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlDescDocAppPersons : result
    {
        public string rmpostccwaiver_gid { get; set; }
        public string waiver_description { get; set; }
        public List<uploadwaiverdoc_list> uploadwaiverdoc_list { get; set; }
        public List<waiverapprovalmember_list> waiverapprovalmember_list { get; set; }
    }

}