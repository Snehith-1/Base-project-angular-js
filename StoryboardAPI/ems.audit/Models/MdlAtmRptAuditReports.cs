using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.audit.Models
{
    public class MdlAtmRptAuditReports : result
    {

        public List<auditreport_list> auditreport_list { get; set; }
        public List<auditvisitreport_list> auditvisitreport_list { get; set; }

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
        public string total_score { get; set; }
        public string auditmapping2employee_gid { get; set; }
        public string checklistmasteradd_gid { get; set; }
        public string status_update { get; set; }
        public string auditsonhold_count { get; set; }
        public string openaudit_count { get; set; }
        public string closedaudit_count { get; set; }
        public string employee_flag { get; set; }
        public string auditmaker_name { get; set; }
        public string auditchecker_name { get; set; }
        public string end_date { get; set; }
        public string auditdepartment_gid { get; set; }
        public string auditeemaker_gid { get; set; }
        public string auditeemaker_name { get; set; }
        public string auditeechecker_gid { get; set; }
        public string auditeechecker_name { get; set; }
        public string entity_name { get; set; }
        public string audit_description { get; set; }
        public string openquerycount { get; set; }
        public string auditeechecker_approvalflag { get; set; }
        public string auditorchecker_approvalflag { get; set; }
        public string auditorapprover_approvalflag { get; set; }
        public string observation_score { get; set; }
        public string observation_fill { get; set; }
        public string auditormaker_approvalflag { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
        public string auditapprover_name { get; set; }
        public string approval_name { get; set; }
        public string tagged_user { get; set; }
        public string auditperiod_fromdate { get; set; }
        public string auditperiod_todate { get; set; }
        public string AuditorMakerInitiatedDate { get; set; }
        public string AuditeeCheckerInitiatedDate { get; set; }
        public string AuditorApprovedDate { get; set; }
        public string auditapproved_by { get; set; }
        public string auditapproved_date { get; set; }
        public string Auditorcheckerinitiated_date { get; set; }
    }
    public class auditvisitreport_list
    {
        public string samagrocustomer_gid { get; set; }
        public string samagrocustomer_name { get; set; }
        public string samfincustomer_name { get; set; }
        public string samfincustomer_gid { get; set; }
    }

        public class auditreport_list
    {
        public string auditcreation_gid { get; set; }
        public string auditpriority_name { get; set; }
        public string checkpointgroup_name { get; set; }
        public string auditpriority_gid { get; set; }
        public string audit_name { get; set; }
        public string audittype_name { get; set; }

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
        public string checklistmasteradd_gid { get; set; }
        public string status_update { get; set; }
        public string employee_flag { get; set; }
        public string status_flag { get; set; }
        public string approval_status { get; set; }
        public string auditapprover_name { get; set; }
        public string auditmapping2employee_gid { get; set; }
        public string sampleimport_gid { get; set; }
        public string auditperiod_fromdate { get; set; }
        public string auditperiod_todate { get; set; }
        public string auditeemaker_name { get; set; }
        public string auditeechecker_name { get; set; }
        public string auditdepartment_name { get; set; }
        public string entity_name { get; set; }
        public string auditcreation2checklist_gid { get; set; }

    }

    public class MdlAtmRptAuditVisitReport : result
    {
        public string auditvisit_gid { get; set; }
        public string reportingmanager_name { get; set; }
        public string customer_name { get; set; }
        public string auditvisitreportref_no { get; set; }
        public string application_gid { get; set; }
        public string samfincustomer_gid { get; set; }
        public string samagrocustomer_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string auditvisit_date { get; set; }
        public DateTime auditvisitdate { get; set; }
        public string clientkmp_activities { get; set; }
        public string promoter_background { get; set; }
        public string overall_observations { get; set; }
        public string inspectingofficial_recommenation { get; set; }
        public string trading_relationship { get; set; }
        public string summary { get; set; }
        public string inspectingofficials_name { get; set; }
        public string visitdone_name { get; set; }
        public string draft_flag { get; set; }
        public string statusupdated_by { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string samfincustomer_name { get; set; }
        public string samagrocustomer_name { get; set; }
        public string pending { get; set; }
        public string approved { get; set; }
        public string approval_status { get; set; }

        public string approval_pending { get; set; }
        public string approval_approved { get; set; }
        public string management_pending { get; set; }
        public string management_approved { get; set; }
        public string[] filesname { get; set; }
        public string filepath { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string lscloudpath { get; set; }
        public List<mdlvisitdone> mdlvisitdone { get; set; }
        public List<visitdone_list> visitdone_list { get; set; }
        public List<mstinspectingofficials> mstinspectingofficials { get; set; }
        public List<inspectemployee_list> employeelist { get; set; }
        public List<mstVisitpersondtl_list> mstVisitpersondtl_list { get; set; }
        public List<mstVisitpersonaddress_list> mstVisitpersonaddress_list { get; set; }
        public List<VisitReportList> VisitReportList { get; set; }
        public List<UploadDocumentList> UploadDocumentList { get; set; }
        public List<UploadphotoList> UploadphotoList { get; set; }

    }
    public class inspectemployee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class VisitReportList : result
    {
        public string draft_flag { get; set; }
        public string customer_name { get; set; }
        public string visitreport_gid { get; set; }
        public string visitreport_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string entity_name { get; set; }
        public string approval_status { get; set; }
        public string auditvisitreportref_no { get; set; }
    }
    public class mstinspectingofficials : result
    {
        public string auditvisit2inspectingofficial_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class mdlvisitdone : result
    {
        public string auditvisit2visitdone_gid { get; set; }
        public string visitdone_gid { get; set; }
        public string visitdone_name { get; set; }
    }

    public class visitdone_list : result
    {
        public string auditvisit2visitdone_gid { get; set; }
        public string visitdone_gid { get; set; }
        public string visitdone_name { get; set; }
    }
    public class mstVisitpersondtl_list : result
    {
        public string auditvisit_gid { get; set; }
        public string auditvisit2person_gid { get; set; }
        public string clientrepresentative_name { get; set; }
        public string clientrepresentative_designationgid { get; set; }
        public string clientrepresentative_designationname { get; set; }
        public string clientrepresentative_personalmail { get; set; }
        public string clientrepresentative_officemail { get; set; }
        public List<mstVisitpersoncontact_list> mstVisitpersoncontact_list { get; set; }
    }
    public class mstVisitpersoncontact_list : result
    {
        public string auditvisitperson2contact_gid { get; set; }
        public string auditvisit_gid { get; set; }
        public string mobile_no { get; set; }
        public string whatsapp_mobileno { get; set; }
        public string primary_status { get; set; }
    }
    public class mstVisitpersonaddress_list : result
    {
        public string auditvisit2address_gid { get; set; }
        public string auditvisit_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string primary_status { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string taluk { get; set; }
        public string district { get; set; }
        public string state_gid { get; set; }
        public string state_name { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    public class UploadDocumentList : result
    {
        public string auditvisit2document_gid { get; set; }
        public string auditvisit_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string photo_gid { get; set; }
        public string filename { get; set; }
        public string filepath { get; set; }
        public string[] filesname { get; set; }
       
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }
    public class UploadphotoList : result
    {
        public string auditvisit2photo_gid { get; set; }
        public string auditvisit_gid { get; set; }
        public string photo_name { get; set; }
        public string document_path { get; set; }
        public string filename { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
        public string filepath { get; set; }
        public string[] filesname { get; set; }

    }

}
