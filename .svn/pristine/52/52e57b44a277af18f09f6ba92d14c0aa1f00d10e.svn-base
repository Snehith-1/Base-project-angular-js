using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.vp.Models
{

    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class releaseData : result
    {
        public string release_date { get; set; }
        public string release_remarks { get; set; }
        public string done_by { get; set; }
        public string release_status { get; set; }
        public string change_description { get; set; }
        public string impacted_module { get; set; }
        public string impacted_system { get; set; }
        public string reason_change { get; set; }
        public string alternative_way { get; set; }
        public string resources { get; set; }

        public List<tabledata> tabledata { get; set; }
        public List<uatdocument_list> uatdocument_list { get; set; }
    }

    public class uatdocument_list
    {
        public string uatdocument_gid { get; set; }
        public string document_name { get;set;}
        public string document_path { get; set;}
    }

    public class tabledata
    {
        public string applicationmaster_gid { get; set; }
        public string criticallity_issue { get; set; }
        public string type_issue { get; set; }
        public string application_gid { get; set; }
        public string complaint_gid { get; set; }
        public string type_gid { get; set; }
        public string issue_date { get; set; }
        public string issue_type { get; set; }
        public string issue_title { get; set; }
        public string issue_remarks { get; set; }
        public string Severity { get; set; }
        public string priority { get; set; }
        public string response_time { get; set; }
        public string issue_status { get; set; }
        public string approval_status { get; set; }
        public string issuetracker_gid { get; set; }
        public string ticket2issuetrackergid { get; set; }
        public string issue_refno { get; set; }
        public string team_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string reply_comments { get; set; }
        public string reply_date { get; set; }
        public string reply_flag { get; set; }

    }

    public class statusUpdate : result
    {
        public string issuetrackergid { get; set; }
        public string release_gid { get; set; }
        public string IssueStatus { get; set; }
        public string StatusRemarks { get; set; }
        public DateTime TargetIssuDate { get; set; }
        public string DoneBy { get; set; }
        public string releaseStatus { get; set; }
        public string[] issueGid { get; set; }
        public string change_description { get; set; }
        public string impacted_module { get; set; }
        public string impacted_system { get; set; }
        public string reasonsfor_change { get; set; }
        public string alternative_way { get; set; }
        public string resources { get; set; }
    }

    public class UploadDocumentname : result
    {
        public List<UploadDocumentModel> filename_list { get; set; }
    }

    public class UploadDocumentModel
    {
        public string filename { get; set; }
        public string id { get; set; }
    }

    public class UploadDocumentcancel:result
    {
        public string tmpuatdocument_gid { get; set; }
    }

    public class tmpdocumentclear:result
    {

    }
}