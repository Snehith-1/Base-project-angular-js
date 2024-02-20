using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for pages in CC Schedule)MstCC Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlMstCC: result
    {
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string submitted_date { get; set; }
        public string vertical_name { get; set; }
        public string submitted_by { get; set; }
        public string ccsubmitted_date { get; set; }
        public string ccsubmitted_by { get; set; }
        public string mom_description { get; set; }
        public string application_gid { get; set; }
        public string privilege_gid { get; set; }
        public string proceed_flag { get; set; }
        public string approval_status { get; set; }
        public string ccmeetingmember_gid { get; set; }
        public string approval_flag { get; set; }
        public string remarks { get; set; }
        public List<momdocument_list> momdocument_list { get; set; }
        public List<camdocument_list> camdocument_list { get; set; }
        public string cc_remarkslog { get; set; }
        public string approvalshow_flag { get; set; }
        public string mom_flag { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string approval_remarks { get; set; }
        public string ccapproval_flag { get; set; }
        public string mom_descflag { get; set; }
        public string mom_reapprove { get; set; }
        public string mom_ccmailflag { get; set; }
        
    }
    public class MdlMstCCschedule : result
    {
        public string application_gid { get; set; }
        public string ccschedulemeeting_gid { get; set; }
        public string ccmeeting_no { get; set; }
        public string ccmeeting_date { get; set; }
        public string meeting_date { get; set; }
        public DateTime meetingdate { get; set; }
        public DateTime Tstart_time { get; set; }
        public DateTime Tend_time { get; set; }
        public string ccmeeting_title { get; set; }
        public string ccmeeting_mode { get; set; }
        public string ccgroupname_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string otheruser_name { get; set; }
        public string non_users { get; set; }
        public string ccadmin_name { get; set; }
        public string ccmeeting_time { get; set; }
        public List<ccschedule_list> ccschedule_list { get; set; }
        public List<ccmember_list> ccmember_list { get; set; }
        public List<ccmembers_list> ccmembers_list { get; set; }
        public List<ccmemberlog_list> ccmemberlog_list { get; set; }
        public List<otheremployee_list> otheremployee_list { get; set; }
        public List<otheruser_list> otheruser_list { get; set; }
        public List<otheruserlog_list> otheruserlog_list { get; set; }
        public List<ccadmin_list> ccadmin_list { get; set; }
        public List<ccmail_list> ccmail_list { get; set; }
        public List<employee_lists> employee_lists { get; set; }
        public List<ccgroup_lists> ccgroup_lists { get; set; }

        public string ccmember_name { get; set; }
        public string ccmember_gid { get; set; }
        public string ccapproval_flag { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string loanfacility_amount { get; set; }
        public string rm_name { get; set; }
        public string approval_remarks { get; set; }
        public string approval_token { get; set; }
        public string approval_status { get; set; }
        public string mom_description { get; set; }

    }
    public class ccschedule_list
    {
        public string application_gid { get; set; }
        public string ccschedulemeeting_gid { get; set; }
        public string ccmeeting_no { get; set; }
        public string ccmeeting_date { get; set; }
        public string ccmeeting_title { get; set; }
        public string ccmeeting_mode { get; set; }
        public string ccgroupname_gid { get; set; }
        public string ccgroup_name { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string description { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlCCcount : result
    {
        public string scheduled_count { get; set; }
        public string pending_count { get; set; }
        public string completed_count { get; set; }
        public string cctocredit_count { get; set; }
        public string ccmeetingskip_count { get; set; }
    }
    public class MdlOtherEmployee : result
    {
        public List<ccgroup_list> ccgroup_list { get; set; }
        public List<otheremployee_list> otheremployee_list { get; set; }
    }
    public class otheremployee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class otheruser_list
    {
        public string attendance_status { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string ccmeeting2othermembers_gid { get; set; }
        public string approval_status { get; set; }
    }
    public class otheruserlog_list
    {
        public string attendance_status { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string ccmeeting2othermembers_gid { get; set; }
        public string approval_status { get; set; }
    }
    public class ccadmin_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class calendarevent : result
    {
        public List<createevent> createevent { get; set; }
    }

    public class createevent : result
    {
        public DateTime event_date { get; set; }
        public DateTime event_time { get; set; }
        public DateTime end_time { get; set; }
        public string event_title { get; set; }
    }
    public class momdocument_list : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
        public string application2momdoc_gid { get; set; }
       
    }
    public class camdocument_list : result
    {
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string application_gid { get; set; }
       public string application2camdoc_gid { get; set; }
    }
    public class ccrequestorlist : result
    {
        public List<ccrequestordtl> ccrequestordtl { get; set; }
        public List<ccrequestordtlhistory> ccrequestordtlhistory { get; set; }
    }
    public class ccrequestordtl : result
    {
        public string ccrequestorcommunication_gid { get; set; }
        public string application_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string sender_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string session_user { get; set; }
        public string document_attached { get; set; }
        public string lsflag { get; set; }
    }
    public class ccrequestordtlhistory : result
    {
        public string ccrequestorcommunication_gid { get; set; }
        public string application_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string sender_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string session_user { get; set; }
        public string document_attached { get; set; }
        public string lsflag { get; set; }
    }
    public class upload_document : result
    {
        public List<uploaddoc_list> uploaddoc_list { get; set; }
    }
    public class uploaddoc_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlCCAttendance : result
    {
        public string attendance_status { get; set; }
        public string ccapproval_flag { get; set; }
        public string ccmeeting2members_gid { get; set; }
        public string ccmeeting2othermembers_gid { get; set; }
    }
    public class MdlCCRevert : result
    {
        public string application_gid { get; set; }
        public string cctocredit_reason { get; set; }
        public List<cctocreditlog_list> cctocreditlog_list { get; set; }
    }
    public class cctocreditlog_list : result
    {
        public string application_gid { get; set; }
        public string cctocredit_reason { get; set; }
        public string cctocreditlog_gid { get; set; }
        public string sentbacktocc_date { get; set; }
        public string sentbacktocc_by { get; set; }
    }
    public class employee_lists
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class ccgroup_lists
    {
        public string ccgroup_code { get; set; }
        public string ccgroup_name { get; set; }
        public string ccgroupname_gid { get; set; }
    }
    public class MdlCCMeetingHistoryLog : result
    {
        public List<ccmeetinghistorylog_list> ccmeetinghistorylog_list { get; set; }
    }
    public class ccmeetinghistorylog_list
    {
        public string ccschedulemeetinglog_gid { get; set; }
        public string application_gid { get; set; }
        public string ccmeeting_no { get; set; }
        public string ccmeeting_title { get; set; }
        public string ccmeeting_date { get; set; }
        public string ccmeeting_mode { get; set; }
        public string ccmeeting_time { get; set; }
    }

    public class CCCount_list 
    {
        public string my_approval { get; set; } 
        public string cc_tagged { get; set; } 
        public string cc_completed { get; set; } 
        public string scheduled_meeting { get; set; } 
        public string total_count { get; set; }
        public string approval_pending { get; set; }
    }
    public class MdlCcMeetingSkip : result
    {
        public string application_gid { get; set; }
        public string ccmeetingskip_reason { get; set; }
        public string reason { get; set; }
        public List<ccmeetingskip_list> ccmeetingskip_list { get; set; }
        public List<ccmeetingskiphistory_list> ccmeetingskiphistory_list { get; set; }
        public string ccmeetingskip_remarks { get; set; }
        public string ccmeetingskip_by { get; set; }
        public string ccmeetingskip_date { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }

    }
    public class ccmeetingskip_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string application_status { get; set; }
        public string applicant_type { get; set; }
        public string submitted_date { get; set; }
        public string submitted_by { get; set; }
        public string region { get; set; }
        public string overalllimit_amount { get; set; }
        public string approval_status { get; set; }
        public string ccmeetingskip_by { get; set; }
        public string ccmeetingskip_date { get; set; }
        public string ccmeetingskip_gid { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
    }
    public class ccmeetingskiphistory_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string application_status { get; set; }
        public string applicant_type { get; set; }
        public string submitted_date { get; set; }
        public string submitted_by { get; set; }
        public string region { get; set; }
        public string overalllimit_amount { get; set; }
        public string approval_status { get; set; }
        public string ccmeetingskip_by { get; set; }
        public string ccmeetingskip_date { get; set; }
        public string ccmeetingskip_gid { get; set; }
    }
}