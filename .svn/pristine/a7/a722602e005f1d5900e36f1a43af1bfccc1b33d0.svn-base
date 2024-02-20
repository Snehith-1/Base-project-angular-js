using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will help to push and pull data's for the physical copy flow
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A, Premchandar.K </remarks>


    public class PhysicalDocTaggedDocumentList : result
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public List<PhysicalDocTaggedDocument> PhysicalDocTaggedDocument { get; set; }
        public List<PhysicalCovenantDocTaggedDocument> PhysicalCovenantDocTaggedDocument { get; set; }
    }

    public class PhysicalDocTaggedDocument
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
        public string physical_documentcount { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string documentconfirmation_remarks { get; set; }
        public string waiverpendingcount { get; set; }
        public string checklistcount { get; set; }
        public string physicaloverall_docstatus { get; set; }
        public string physicalcopyquerystatus { get; set; }
    }

    public class PhysicalCovenantDocTaggedDocument
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
        public string physical_documentcount { get; set; }
        public string physicaloverall_docstatus { get; set; }
        public string groupdocumentchecklist_gid { get; set; }
        public string covenant_periods { get; set; }
        public string documentconfirmation_remarks { get; set; }
        public string waiverpendingcount { get; set; }
        public string checklistcount { get; set; }

        public string buffer_days { get; set; }
        public string physicalcopyquerystatus { get; set; }
    }

    public class physicaluploaddocumentlist : result
    {
        public List<physicaluploaddocument> physicaluploaddocument { get; set; }
    }
    public class physicaluploaddocument
    {
        public string scanneddocument_gid { get; set; }
        public string documentcheckdtl_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string file_path { get; set; }
        public string file_name { get; set; }
    }
     

    public class physicaldeferraltaggedlist
    {
        public string document_name { get; set; }
        public string document_type { get; set; }
        public List<physicaldeferraltagged> physicaldeferraltagged { get; set; }
    }

    public class physicaldeferraltagged : result
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
        public List<physicaldeferraltaggedchecklist> physicaldeferraltaggedchecklist { get; set; }
    }

    public class physicaldeferraltaggedchecklist
    {
        public string deferralchecklist_gid { get; set; }
        public string mstchecklist_gid { get; set; }
        public string checklist_name { get; set; }
        public bool deferraltagged { get; set; }
        public bool documentverified { get; set; }
    }  

    public class CadPhysicalCount : result
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

    public class physicalmakerapplicationlist : result
    {
        public List<physicalmakerapplication> physicalmakerapplication { get; set; }
    }

    public class Mdldocumentsend : result
    {
        public string documentcheckdtl_gid { get; set; }
        public string application_gid { get; set; }

        public string scanneddoc_flag { get; set; }
        public string covenant_type { get; set; }
        public string document_status { get; set; }
    }

    public class physicalmakerapplication
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
        public string cadgroupname { get; set; }
        public string cadaccepted_by { get; set; }
        public string cadaccepted_date { get; set; }
        public string approval { get; set; }
        public string processtypeassign_gid { get; set; }
        public string overall_approvalstatus { get; set; }
        public string completed_on { get; set; }
        public string followupstatus { get; set; }
    }
}