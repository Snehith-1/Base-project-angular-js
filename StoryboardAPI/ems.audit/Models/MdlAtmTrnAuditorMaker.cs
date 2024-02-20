using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ems.audit.Models
{
    public class MdlAtmTrnAuditorMaker : result
    {
        public List<myauditormaker_list> myauditormaker_list { get; set; }
        public List<myauditormakerview_list> myauditormakerview_list { get; set; }
        public List<makercheckpointobservation_list> makercheckpointobservation_list { get; set; }
        public List<makercheckpointobservationview_list> makercheckpointobservationview_list { get; set; }
        public List<makerobservationsampleview_list> makerobservationsampleview_list { get; set; }
        public List<makercheckpointobservationupdate_list> makercheckpointobservationupdate_list { get; set; }
        public List<makerAssignedQueryList> makerAssignedQueryList { get; set; }
        public List<makerReplyToQueryList> makerReplyToQueryList { get; set; }
        public List<sampleresponsequery_list> sampleresponsequery_list { get; set; }
        public List<employe> employe { get; set; }
        public string auditcreation_gid { get; set; }
        public string auditpriority_name { get; set; }
        public string auditpriority_gid { get; set; }
        public string audit_name { get; set; }
        public string sample_name { get; set; }
        public string samplecapture_field { get; set; }
        public string checklistcheckpoint_flag { get; set; }
        public string checklistmaster_gid { get; set; }
        public string auditfrequency_gid { get; set; }
        public string auditfrequency_name { get; set; }
        public string audit_approver { get; set; }
        public string auditmapping_gid { get; set; }
        public string audit_checker { get; set; }
        public string audit_maker { get; set; }
        public string document_verified { get; set; }
        public string checklist2checkpoint { get; set; }
        public string sample2checkpoint { get; set; }
        public string checkpointgroupadd_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string employee_gid { get; set; }
        public string due_date { get; set; }
        public string report_date { get; set; }
        public string periodfrom_date { get; set; }
        public string auditperiod_to { get; set; }
        public string audit_uniqueno { get; set; }
        public string audit_tat { get; set; }
        public string auditdepartment_name { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string audittype_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string noteto_auditor { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string riskcategory_name { get; set; }
        public string yes_score { get; set; }
        public string no_score { get; set; }
        public string partial_score { get; set; }
        public string na_score { get; set; }
        public string opencount { get; set; }
        public string total_score { get; set; }
        public string capture_totalscore { get; set; }
        public string total_amount { get; set; }
        public string status_update { get; set; }
        public string auditsonhold_count { get; set; }
        public string openaudit_count { get; set; }
        public string closedaudit_count { get; set; }
        public string initiate_action { get; set; }
        public string remarks { get; set; }
        public string request_approval { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string description { get; set; }
        public string reply_query { get; set; }
        public string raisequery_gid { get; set; }
        public string auditmaker_name { get; set; }
        public string auditchecker_name { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string makerinitiate_approvalflag { get; set; }
        public string makerstatus_flag { get; set; }
        public string makerapproval_flag { get; set; }
        public string maker_initiated { get; set; }
        public string sampleimport_gid { get; set; }
        public string observationscoresample_gid { get; set; }
        public string status_remarks { get; set; }
        public string auditscheckeronhold_count { get; set; }
        public string opencheckeraudit_count { get; set; }
        public string closedcheckeraudit_count { get; set; }
        public string auditsapproveronhold_count { get; set; }
        public string openapproveraudit_count { get; set; }
        public string closedapproveraudit_count { get; set; }
        public string approval_status { get; set; }
        public string completedaudit_count { get; set; }
        public string pendingapprovalaudit_count { get; set; }
        public string completedcheckeraudit_count { get; set; }
        public string auditapproverpending_count { get; set; }
        public string completedapproveraudit_count { get; set; }
        public string observation_remarks { get; set; }
        public string auditor_details { get; set; }
        public string overall_score { get; set; }
        public string observation_percentage { get; set; }
        public string sample_flag { get; set; }
        public string checklistverified_flag { get; set; }

    }
    public class myauditormaker_list
    {
        public string auditcreation_gid { get; set; }
        public string auditpriority_name { get; set; }
        public string auditpriority_gid { get; set; }
        public string audit_name { get; set; }
        public string auditorcheckersample_flag { get; set; }
        public string checklistmaster_gid { get; set; }
        public string auditdepartment_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string audit_approver { get; set; }
        public string auditmapping_gid { get; set; }
        public string audit_checker { get; set; }
        public string makerapprovaloverall_flag { get; set; }
        public string audit_maker { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string employee_gid { get; set; }
        public string audit_uniqueno { get; set; }
        public string audit_tat { get; set; }
        public string due_date { get; set; }
        public string status_update { get; set; }
        public string auditemployee_gid { get; set; }
        public string checkeremployee_gid { get; set; }
        public string audittagemployee_gid { get; set; }
        public string status_flag { get; set; }
        public string approval_flag { get; set; }
        public string makeremployee_gid { get; set; }
        public string approval_status { get; set; }
        public string audittype_name { get; set; }
        public string taguser_gid { get; set; }
        public string auditeemaker_gid { get; set; }
        public string auditeechecker_gid { get; set; }
        public string auditmaker_gid { get; set; }
        public string auditchecker_gid { get; set; }
        public string raisequery_gid { get; set; }
        public string auditapprover_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string auditapprover_name { get; set; }
        public string makerapproval_flag { get; set; }
        public string sampleimport_gid { get; set; }
        public string observation_remarks { get; set; }
        public string observationremarkslog_gid { get; set; }
        public string auditor_details { get; set; }
        public string auditee_visible { get; set; }

    }


    public class myauditormakerview_list
    {
        public string checklistmaster_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string audittype_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string noteto_auditor { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string riskcategory_name { get; set; }
        public string yes_score { get; set; }
        public string no_score { get; set; }
        public string partial_score { get; set; }
        public string na_score { get; set; }
        public string total_score { get; set; }
        public string audit_name { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string auditcreation_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }

    }
    public class makercheckpointobservation_list
    {
        public string auditcreation_gid { get; set; }
        public string auditpriority_name { get; set; }
        public string auditpriority_gid { get; set; }
        public string audit_name { get; set; }
        public string checklistmaster_gid { get; set; }
        public string auditfrequency_gid { get; set; }
        public string auditfrequency_name { get; set; }
        public string audit_approver { get; set; }
        public string auditmapping_gid { get; set; }
        public string audit_checker { get; set; }
        public string audit_maker { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string employee_gid { get; set; }
        public string audit_uniqueno { get; set; }
        public string audit_tat { get; set; }
        public string due_date { get; set; }
    }


    public class makercheckpointobservationview_list
    {
        public string checkpointgroupadd_gid { get; set; }
        public string checklistmaster_gid { get; set; }
        public string sampleimport_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string audittype_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string noteto_auditor { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string riskcategory_name { get; set; }
        public string yes_score { get; set; }
        public string no_score { get; set; }
        public string partial_score { get; set; }
        public string na_score { get; set; }
        public string total_score { get; set; }
        public string capture_score { get; set; }
        public string audit_name { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string auditcreation_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string yes_disposition { get; set; }
        public string no_disposition { get; set; }
        public string partial_disposition { get; set; }
        public string na_disposition { get; set; }
        public string capture_field { get; set; }
        public string sampleobservation_score { get; set; }
        public string samplecapture_score { get; set; }
        public string samplecapture_field { get; set; }
        public string sampleobservation_percentage { get; set; }
        public string sampleoverall_score { get; set; }
        public string observationscoresample_gid { get; set; }
        public string samplechecklistverified_flag { get; set; }

    }
    public class makercheckpointobservationupdate_list
    {
        public string checklistmaster_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string audittype_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string noteto_auditor { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string riskcategory_name { get; set; }
        public string yes_score { get; set; }
        public string no_score { get; set; }
        public string partial_score { get; set; }
        public string na_score { get; set; }
        public string total_score { get; set; }
        public string audit_name { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string auditcreation_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
    }
    public class makercheckpointobservation : result
    {
        public string checkpointobservation_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string[] auditcreation2checklist_gid { get; set; }
        public string checklistmaster_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string audittype_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string noteto_auditor { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string riskcategory_name { get; set; }
        public string yes_score { get; set; }
        public string no_score { get; set; }
        public string partial_score { get; set; }
        public string na_score { get; set; }
        public string total_score { get; set; }
        public string audit_name { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string total_amount { get; set; }
        public string capture_totalscore { get; set; }

    }

    public class makercheckpointobservationadd : result
    {
        public string checkpointobservation_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string total_score { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string total_amount { get; set; }
        public string capture_totalscore { get; set; }
        public string capture_score { get; set; }
        public string observationtotalamount_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public bool allobservationfilled { get; set; }
        public string observation_percentage { get; set; }
        public string overall_score { get; set; }
        public string lstotal_amount { get; set; }
        public string lsoverall_score { get; set; }
       public string observationscoresample_gid { get; set; }
        public string sampleimport_gid { get; set; }

        public string checkpointgroupadd_gid { get; set; }
    }


    public class employe
    {
        public string raisequery_gid { get; set; }
        public string replytoquery_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class makerAssignedQueryList
    {
        public string auditcreation_gid { get; set; }
        public string raisequery_gid { get; set; }
        public string description { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string reply_query { get; set; }

    }

    public class makerReplyToQueryList
    {
        public string auditcreation_gid { get; set; }
        public string raisequery_gid { get; set; }
        public string replytoquery_gid { get; set; }
        public string description { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string reply_query { get; set; }
        public string replied_by { get; set; }
        public string replied_date { get; set; }
    }

    public class initialapprovaldtl : result
    {
        public string observationapproval_gid { get; set; }
        public string initialapproval_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string approve_remarks { get; set; }
        public string approve_type { get; set; }
        public string approval_basedon { get; set; }
        public string approval_name { get; set; }
        public string openquerycount { get; set; }
        public string approval_gid { get; set; }
        public string getapproval_remark { get; set; }
        public string auditorapproval_remark { get; set; }
        public string approve_remark { get; set; }
        public string auditorrejected_remark { get; set; }
        public string rejected_remark { get; set; }
        public string checkerapproval_remark { get; set; }
        public string checkerrejected_remark { get; set; }
        public string get_approvalgid { get; set; }
        public string status_flag { get; set; }
        public string auditee_flag { get; set; }
        public string lsauditorcommonname_flag { get; set; }
        public string auditeechecker { get; set; }
        public string auditeechecker_gid { get; set; }
        public string employee_gid { get; set; }
        public string observation_percentage { get; set; }
        public string auditopenquerycount { get; set; }

        public List<initialapprovalview_list> initialapprovalview_list { get; set; }
        public List<Auditorapprovalview_list> Auditorapprovalview_list { get; set; }
        public List<Checkerapprovalview_list> Checkerapprovalview_list { get; set; }
        public List<raisequeryhistory> raisequeryhistory { get; set; }
        public List<SampleAssignedTag> SampleAssignedTag { get; set; }
    }
    public class raisequeryhistory
     {
        public string sampleimport_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string sample_name { get; set; }
        public string description { get; set; }
        public string query_title { get; set; }
        public string query_to { get; set; }
        public string query_toname { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string raisequery_flag { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sampleraisequery_status { get; set; }
        public string close_remarks { get; set; }

    }

    public class SampleAssignedTag
    {
        public string assigned_to { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string description { get; set; }
    }
public class initialapprovalview_list
    {
        public string checkerapproval_gid { get; set; }
        public string auditchecker_gid { get; set; }
        public string approval_name { get; set; }
        public string auditcreation_gid { get; set; }
        public string approval_type { get; set; }
        public string approval_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string approval_remark { get; set; }
        public string checkerapproval_remark { get; set; }
        public string checkerrejected_remark { get; set; }
        public string rejected_remark { get; set; }
        public string initialapproval_gid { get; set; }
        public string initiateapproval { get; set; }
    }
    public class Checkerapprovalview_list
    {
        public string checkerapproval_gid { get; set; }
        public string auditchecker_gid { get; set; }
        public string approval_name { get; set; }
        public string auditcreation_gid { get; set; }
        public string approval_type { get; set; }
        public string approval_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string approval_remark { get; set; }
        public string checkerapproval_remark { get; set; }
        public string checkerrejected_remark { get; set; }
        public string rejected_remark { get; set; }
        public string initialapproval_gid { get; set; }
        public string initiateapproval { get; set; }
    }
    public class Auditorapprovalview_list
    {
        public string auditorapproval_gid { get; set; }
        public string auditchecker_gid { get; set; }
        public string approve_remark { get; set; }
        public string auditcreation_gid { get; set; }
        public string approval_type { get; set; }
        public string approval_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string approval_remark { get; set; }
        public string checkerapproval_remark { get; set; }
        public string checkerrejected_remark { get; set; }
        public string rejected_remark { get; set; }
        public string initialapproval_gid { get; set; }
        public string initiateapproval { get; set; }
        public string auditapproval_gid { get; set; }
        public string approval_name { get; set; }
    }
    public class sampleresponsequery_list
    {       
        public string approval_status { get; set; }

    }
    public class makerobservationsampleview_list
    {
        public string checkpointgroupadd_gid { get; set; }
        public string checklistmaster_gid { get; set; }
        public string auditdepartment_name { get; set; }
        public string checkpoint_intent { get; set; }
        public string checkpoint_description { get; set; }
        public string audittype_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string noteto_auditor { get; set; }
        public string positiveconfirmity_name { get; set; }
        public string riskcategory_name { get; set; }
        public string yes_score { get; set; }
        public string no_score { get; set; }
        public string partial_score { get; set; }
        public string na_score { get; set; }
        public string total_score { get; set; }
        public string capture_score { get; set; }
        public string audit_name { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string auditcreation_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string auditcreation2checklist_gid { get; set; }
        public string yes_disposition { get; set; }
        public string no_disposition { get; set; }
        public string partial_disposition { get; set; }
        public string na_disposition { get; set; }
        public string capture_field { get; set; }
        public string sampleobservation_score { get; set; }
        public string samplecapture_score { get; set; }
        public string samplecapture_field { get; set; }
        public string sampleobservation_percentage { get; set; }
        public string sampleoverall_score { get; set; }
        public string observationscoresample_gid { get; set; }
        public string samplechecklistverified_flag { get; set; }

    }
    public class auditraisequery : result
    {
        public string auditcreation_gid { get; set; }
        public string sample_name { get; set; }
        public string query_description { get; set; }
        public string query_title { get; set; }
        public string query_to { get; set; }
        public string query_toname { get; set; }
        public string auditraisequery_gid { get; set; }
        public string auditraisequery_flag { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string auditraisequery_status { get; set; }
        public string close_remarks { get; set; }
        public string remarks { get; set; }
        public string sampleimport_gid { get; set; }

        public string replied_by { get; set; }

        public List<auditquerydata> auditquerydata { get; set; }
        public List<auditQuerydetaillist> auditQuerydetaillist { get; set; }

    }
    public class auditquerydata
    {
        public string auditcreation_gid { get; set; }
        public string sample_name { get; set; }
        public string query_description { get; set; }
        public string query_title { get; set; }
        public string query_to { get; set; }
        public string query_toname { get; set; }
        public string auditraisequery_gid { get; set; }
        public string auditraisequery_flag { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string auditraisequery_status { get; set; }
        public string close_remarks { get; set; }
    }
    public class auditQuerydetaillist : result
    {
        public string auditqueries2response_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string sender_name { get; set; }
        public string session_user { get; set; }
        public string replied_by { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_attached { get; set; }
    }
}