using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.vp.Models
{
    // Get Vendor Detail
    public class vendordtl : result
    {
        public string vendor_gid { get; set; }
        public string vendor_code { get; set; }
        public string vendor_companyname { get; set; }
        public string contactperson_name { get; set; }
        public string contact_telephonenumber { get; set; }
        public string active_flag { get; set; }
        public string address_gid { get; set; }
        public string email_id { get; set; }
        public string tin_number { get; set; }
        public string excise_details { get; set; }
        public string pan_number { get; set; }
        public string servicetax_number { get; set; }
        public string cst_number { get; set; }
        public string bank_details { get; set; }
        public string ifsc_code { get; set; }
        public string currencyexchange_gid { get; set; }
        public string gst_number { get; set; }
        public string payment_terms { get; set; }
        public string application_code { get; set; }
        public string application_name { get; set; }
        public string current_status { get; set; }
        public string department_name { get; set; }
        public string team_name { get; set; }
        public List<app_details> applications { get; set; }
    }
    public class app_details
    {
        public string app_code { get; set; }
        public string app_name { get; set; }
        public string app_status { get; set; }
        public string dept { get; set; }
        public string team_name { get; set; }
        public string current_status { get; set; }
        public string newTiclketCount { get; set; }
    }

    // Get Issue Tracker Table 

    public class issuetrackertable : result
    {
        public string count_open { get; set; }
        public string count_new { get; set; }
        public string count_closed { get; set; }
        public string count_uatacknowledged { get; set; }
        public string count_uat { get; set; }
        public string count_replycomments { get; set; }
        public string count_changedetails { get; set; }
        public List<tabledata> tabledata { get; set; }
    }

    public class viewIssueDoc : result
    {
        public string issuetracker_gid { get; set; }
        public string issue_status { get; set; }
        public string issue_refno { get; set; }
        public string issue_date { get; set; }
        public string issue_title { get; set; }
        public string issue_type { get; set; }
        public string issue_remarks { get; set; }
        public string docStatus { get; set; }
        public string priority { get; set; }
        public List<documents> path { get; set; }
        public List<issuestatuslog> issuestatuslog { get; set; }
    }
    public class documents
    {
        public string ticketdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
    }

    public class issuestatuslog
    {
        public string issue_status { get; set; }
        public string issue_remarks { get; set; }
        public string reply_comments { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class issuechat : result
    {
        public string chatstatus { get; set; }
        public List<chatdate> datelist { get; set; }
    }
    public class chatdate
    {
        public string dates { get; set; }
        public List<issueChatdtl> Chatdtl { get; set; }
    }
    public class issueChatdtl
    {
        public string response_gid { get; set; }
        public string onprimise_text { get; set; }
        public string vendor_text { get; set; }
        public string read_status { get; set; }
        public string response_time { get; set; }
    }

    public class chatlog : result
    {
        public string issue_gid { get; set; }
        public string vendor_text { get; set; }
    }

    public class issuestate : result
    {
        public string issue_status { get; set; }
    }
}
