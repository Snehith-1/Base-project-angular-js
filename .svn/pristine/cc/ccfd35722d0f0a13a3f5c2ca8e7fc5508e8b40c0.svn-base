using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This models will provide access to various functionalities embedded in supplier scanned document flow 
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Praveen Raj.R, Premchander.K</remarks>

    public static class getMenuClass
    {
        public const string
             DocumentChecklist = "AGDMGTDCL",
             Contract = "AGDMGTCON",
             ScannedDocument = "AGDMGTDTS",
             PhysicalDocument = "AGDMGTPYD",
             LSA = "CADMGTLSA",
             ChequeManagement = "AGDMGTCMS"; 
    }

    public static class deferralTagstatus
    {
        public const string
             Inactive = "0",
             Active = "1",
             DeferralTaken = "2",
             ApprovalPending = "3",
             ApprovalRejected = "4";
    }
    public static class Closequerystatus
    {
        public const string
             General = "1",
             ReDocumentSubmission = "2",
             DeferralRequest = "3";
    }

    public static class Raisequerystatus
    {
        public const string
             QueryRaised = "Query Raised",
             Pending = "Pending",
             Cancel = "Cancelled",
             Closed = "Closed";
    }

    public class ScannnedDocTaggedDocumentList : result
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public List<ScannnedDocTaggedDocument> ScannnedDocTaggedDocument { get; set; }
        public List<ScannnedCovenantDocTaggedDocument> ScannnedCovenantDocTaggedDocument { get; set; }

    }


   
    public class ScannnedDocTaggedDocument
    {
        public string documenttype_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string documenttype_count { get; set; }
        public string companydocument_name { get; set; }
        public string covenant_type { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string companydocument_gid { get; set; }
        public string taggedby { get; set; }
        public string softcopyquerystatus { get; set; }
        public string covenantperiod { get; set; }
        public string individual2document_gid { get; set; }
        public string group2document_gid { get; set; }
        public string tagged_name { get; set; }
        public string deferraltagdoc_gid { get; set; }
        public string deferraltag_status { get; set; }
        public string taggeddate { get; set; }
        public string due_date { get; set; }
        public string overall_docstatus { get; set; } 
        public string extendeddue_date { get; set; }
        public string scanned_documentcount { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string documentconfirmation_remarks { get; set; }
        public string waiverpendingcount { get; set; }
        public string checklistcount { get; set; }
    }

    public class ScannnedCovenantDocTaggedDocument
    {
        public string documenttype_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string documenttype_count { get; set; }
        public string companydocument_name { get; set; }
        public string covenant_type { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string companydocument_gid { get; set; }
        public string taggedby { get; set; }
        public string covenantperiod { get; set; }
        public string individual2document_gid { get; set; }
        public string group2document_gid { get; set; }
        public string tagged_name { get; set; }
        public string deferraltagdoc_gid { get; set; }
        public string deferraltag_status { get; set; }
        public string taggeddate { get; set; }
        public string due_date { get; set; }
        public string overall_docstatus { get; set; }
        public string extendeddue_date { get; set; }
        public string scanned_documentcount { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string covenant_periods { get; set; }
        public string documentconfirmation_remarks { get; set; }
        public string waiverpendingcount { get; set; }
        public string checklistcount { get; set; }
        public string softcopyquerystatus { get; set; }
        public string buffer_days { get; set; }
    }

    public class scanneduploaddocumentlist : result
    {
        public List<scanneduploaddocument> scanneduploaddocument { get; set; }
    }
    public class scanneduploaddocument
    {
        public string scanneddocument_gid { get; set; }
        public string physicaldocument_gid { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string file_path { get; set; }
        public string file_name { get; set; }
        public string replacementdocument_code { get; set; }
        public string scanneddocument_code { get; set; }
    }

    public class movedtosigneddoc : result
    {
        public string[] scanneddocument_gid { get; set; }
        public string documentcheckdtl_gid { get; set; }
    }

    public class deferraltaggedlist
    {
        public string document_name { get; set; }
        public string document_type { get; set; }
        public List<deferraltagged> deferraltagged { get; set; }
    }

    public class deferraltagged : result
    {
        public string deferraltagdoc_gid { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string tracking_id { get; set; }
        public string tagged_to { get; set; }
        public string due_date { get; set; }
        public DateTime Duedate { get; set; }
        public string cad_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string deferraltag_status { get; set; }
        public string scanneddoc_flag { get; set; }
        public string covenant_type { get; set; }
        public List<deferraltaggedchecklist> deferraltaggedchecklist { get; set; }
    }

    public class deferraltaggedchecklist
    {
        public string deferralchecklist_gid { get; set; }
        public string mstchecklist_gid { get; set; }
        public string checklist_name { get; set; }
        public bool deferraltagged { get; set; }
        public bool documentverified { get; set; }
        public string query_flag { get; set; }
    }

    public class Mstdeferraltag : result
    {
        public string companydocument_gid { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string document_code { get; set; }
        public List<MstChecklist> MstChecklist { get; set; }
    }
    public class MstChecklist
    {
        public string mstchecklist_gid { get; set; }
        public string checklist_name { get; set; }
    }

    public class mslcadquerylist : result
    {
        public string checklistcount { get; set; }
        public string waiverpendingcount { get; set; }
        public string deferraltagdoc_gid { get; set; }
        public string documentsubmission_date { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public List<mdlcadquery> mdlcadquery { get; set; }
    }

    public class MdlCADExportConversation : result
    {
        public string application_gid { get; set; }
        public string type_of_copy { get; set; }
        public string attachment_name { get; set; }
        public string attachment_path { get; set; }
        public string attachment_cloudpath { get; set; }
        public string lsacreate_gid { get; set; }
    }
    public class mdlcadquery : result
    {
        public string groupdocumentchecklist_gid { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string application_gid { get; set; }
        public string tagquery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string query_status { get; set; }
        public string query_to { get; set; }
        public string query_toname { get; set; }
        public string query_responseremarks { get; set; }
        public string query_responseddate { get; set; }
        public string query_responsedby { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string deferralchecklist_gid { get; set; }
        public string deferralchecklist_name { get; set; }
        public string query_code { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string queryclosed_status { get; set; }
        public string maker_flag { get; set; }
    }

    public class querydocumentlist : result
    {
        public List<queryuploaddocument> queryuploaddocument { get; set; }
    }
    public class queryuploaddocument
    {
        public string tagquerydocument_gid { get; set; }
        public string tagquery_gid { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string file_path { get; set; }
        public string file_name { get; set; }
        public string document_title { get; set; }
    }

    public class customerRMsummarylist
    {
        public List<customerRMsummary> customerRMsummary { get; set; }
    }

    public class customerRMsummary
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string customer_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string ccgroup_name { get; set; }
        public string overalllimit_amount { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string renewal_flag { get; set; }
        public string amendment_flag { get; set; }
        public string shortclosing_flag { get; set; }
        public string document_status { get; set; }
    }

    public class mdlinitiateextendwaiverlist : result
    {

        public List<mdlinitiateextendwaiver> mdlinitiateextendwaiver { get; set; }
        public List<mdlapprovaldtl> mdlapprovaldtl { get; set; }
    }

    public class mdlinitiateextendwaiver : result
    {
        public string documentcheckdtl_gid { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string application_gid { get; set; }
        public string initiateextendorwaiver_gid { get; set; }
        public string activity_type { get; set; }
        public string activity_title { get; set; }
        public string extendeddue_date { get; set; }
        public string reason { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string credit_gid { get; set; }
        public string due_date { get; set; }
        public string covenant_type { get; set; }
        public string scanneddoc_flag { get; set; }

        public string document_type { get; set; }
        public string approval_initiation { get; set; }
        public string tagquery_gid { get; set; }
        public List<mdlapproval> mdlapproval { get; set; }
    }

    public class mdlapproval : result
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string approval_person { get; set; }
    }

    public class mdlapprovaldtllist : result
    {
        public string activity_type { get; set; }
        public string activity_title { get; set; }
        public string extendeddue_date { get; set; }
        public string reason { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string fromphysical_document { get; set; }
        public string due_date { get; set; }
        public List<mdlapprovaldtl> mdlapprovaldtl { get; set; }
    }

    public class mdlScannedApprovaldtl
    {
        public string creditnationalmanager_gid { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string creditregionalmanager_gid { get; set; }
        public string creditregionalmanager_name { get; set; }
    }

    public class mdlapprovaldtl : result
    {
        public string extendorwaiverapproval_gid { get; set; }
        public string initiateextendorwaiver_gid { get; set; }
        public string approval_name { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string approvedrejected_date { get; set; }
        public string documentcheckdtl_gid { get; set; }
    }

    public class mdldeferralapprovallist : result
    {
        public List<mdldeferralapproval> mdldeferralapproval { get; set; }
    }

    public class mdldocumentapprovaldtllist : result
    {
        public List<mdlapprovaldtl> mdlapprovaldtl { get; set; }
    }

    public class mdldeferralapproval
    {
        public string initiateextendorwaiver_gid { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string extendorwaiverapproval_gid { get; set; }
        public string approval_initiationgid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string activity_type { get; set; }
        public string activity_title { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string application_gid { get; set; }
        public string fromphysical_document { get; set; }
    }

    public class mdldocumentconfirmation : result
    {
        public string overall_docstatus { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string documentconfirmation_remarks { get; set; }
    }

    public class mdlgroupdocumentchecklist
    {
        public string groupdocumentchecklist_gid { get; set; }
        public string groupcovdocumentchecklist_gid { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string mstdocument_gid { get; set; }
        public string mstdocument_name { get; set; }
        public string mstcovenant_type { get; set; }
        public string mstdocumenttype_gid { get; set; }
        public string mstdocumenttype_name { get; set; }
        public string tagged_flag { get; set; }
        public string tagged_by { get; set; }
        public string covenant_periods { get; set; }
        public string covenantperiod_updatedby { get; set; }
        public string covenantperiod_updateddate { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class mdlscannedcovenantperiodlist : result
    {
        public List<mdlscannedcovenantperiod> mdlscannedcovenantperiod { get; set; }
    }

    public class mdlscannedcovenantperiod : result
    {
        public string covenantperioddtl_gid { get; set; }
        public string groupdocumentdtl_gid { get; set; }
        public string credit_gid { get; set; }
        public string covenant_periods { get; set; }
        public string covenant_startdate { get; set; }
        public string covenant_enddate { get; set; }
        public string previous_covenantperiods { get; set; }
        public string covenant_submissiondate { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

        public string buffer_days { get; set; }

        public string previous_bufferdays { get; set; }
    }

    public class mdlscannedgeneral
    {
        public string RM_name { get; set; }
        public string maker_name { get; set; }
        public string checker_name { get; set; }
        public string approver_name { get; set; }
    }

    public class mdlmakercheckerconversationlist
    {
        public List<mdlmakercheckerconversation> mdlmakercheckerconversation { get; set; }
    }

    public class mdlmakercheckerconversation : result
    {
        public string makercheckerconversation_gid { get; set; }
        public string groupdocumentdtl_gid { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string send_message { get; set; }
        public string send_to { get; set; }
        public string maker_flag { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string session_user { get; set; }
    }

    public class CadScannedCount : result
    {
        public string MakerPendingCount { get; set; }
        public string MakerFollowUpCount { get; set; }
        public string CheckerPendingCount { get; set; }
        public string CheckerFollowUpCount { get; set; }
        public string ApproverPendingCount { get; set; }
        public string ApproverFollowUpCount { get; set; }
        public string CompletedCount { get; set; }
        public string CheckerApprovalPendingCount { get; set; }
    }

    public class GetCompletedInfo
    {
        public string PhysicalCompleted { get; set; }
        public string ScannedCompleted { get; set; }
    }

    public class MdlTagQueryCheckpointList
    {
        public List<MdlTagQueryCheckpoint> MdlTagQueryCheckpoint { get; set; }
    }

    public class MdlTagQueryCheckpoint
    {
        public string deferralchecklist_gid { get; set; }
    }


    public class mdlmultipleinitiateextendwaiver : result
    {
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string lsinitiate { get; set; }

        public string covenant_type { get; set; }
    }
    public class mdlcadquerystatusupdate : result
    {
        public string tagquery_gid { get; set; }
        public string query_status { get; set; }

        public string documentcheckdtl_gid { get; set; }
    }

    public class mdlsubmissiondateupdate : result
    {
        public string documentsubmission_date { get; set; }
        public string covenant_type { get; set; }
        public string groupdocumentdtl_gid { get; set; }
    }

    public class mdlGroupDocStatus
    {
        public string deferraltagdoc_gid { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string application_gid { get; set; }
        public string credit_gid { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string tracking_id { get; set; }
        public string tagged_to { get; set; }
        public string due_date { get; set; }
        public DateTime Duedate { get; set; }
        public string cad_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string deferraltag_status { get; set; }
        public string scanneddoc_flag { get; set; }
        public string covenant_type { get; set; }

        public string initiateextendorwaiver_gid { get; set; }
    }


}