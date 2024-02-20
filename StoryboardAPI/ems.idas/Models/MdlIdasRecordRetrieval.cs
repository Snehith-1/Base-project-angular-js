using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlRecordReqView
    {
        public string retrievalrequest_gid { get; set; }
        public string requested_date { get; set; }
        public string requestedby_name { get; set; }
        public string retrieval_type { get; set; }
        public string approved_date { get; set; }
        public string approvalby_name { get; set; }
        public string req_remarks { get; set; }
        public string requested_for { get; set; }
        public string documentretrieved_status { get; set; }
        public string documentretrieved_mode { get; set; }
        public List<MdlIdasUploadDocument> MdlIdasUploadDocument { get; set; }
        public List<MdlCustomerDtls> MdlCustomerDtlsList { get;set;}
        public List<MdlBoxDtls> MdlBoxDtlsList { get; set; }
       //
    }
    public class MdlCustomerDtls
    { 
        public string customer_gid { get; set; }
        public string customer_urn { get; set; }
        public string customername { get; set; }
        public string vertical_code { get; set; }
        public string businesshead_name { get; set; }
        public string zonal_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmgmt_name { get; set; }
        public string relationshipmgmt_name { get; set; }
    }

    public class MdlBoxDtls
    {
        public string box_gid { get; set; }
        public string boxref_no { get; set; }
        public string stampref_no { get; set; }
        public string cartonbox_date { get; set; }
        public string remarks { get; set; }
    }
   
    public class MdlIdasRecordRequest
    {
        public string requested_date { get; set; }
        public string requestedby_name { get; set; }
        public string retrieval_type { get; set; }
        public string approved_date { get; set; }
        public string approvalby_name { get; set; }
        public string req_remarks { get; set; }
        public string requested_for { get; set; }
        public string documentretrieved_mode { get; set; }
        public string[,] reteival_record { get; set; }
    }

    public class MdlIdasRecordDtls
    {
        public string retrievalrequest_gid { get; set; }
        public string documentretrieved_mode { get; set; }
        public string box_gid { get; set; }
        public string customer_gid { get; set; }
    }
    public class MdlTrnRequiredlist
    {
        public List<MdlTrnRequired> MdlTrnRequired { get; set; }

    }
   
    public class MdlTrnRequired
    {
        public string trn_gid { get; set; }
        public string customer_name { get; set; }
        public string despatch_gid { get; set; }
        public string box_gid { get; set; }
        public string batch_gid { get; set; }
        public string despatchref_no { get; set; }
        public string batchref_no { get; set; }
        public string boxref_no { get; set; }
        public string file_status { get; set; }
        public string despatch_date { get; set; }
        public string contact_person { get; set; }
        public string despatched_by { get; set; }
        public string cartonbox_date { get; set; }
    }
    public class MdlIdasRecordReqSummarylist:result 
    {
        public List<MdlIdasRecordReqSummary> MdlIdasRecordReqSummary{ get; set; }
    }
    public class MdlIdasRecordReqSummary
    {
        public string retrievalrequest_gid { get; set; }
        public string requested_date { get; set; }
        public string created_date { get; set; }
        public string user_name { get; set; }
        public string total_count { get; set; }
        public string approved_date { get; set; }
        public string retrieval_type { get; set; }
        public string requested_for { get; set; }
        public string documentretrieved_status { get; set; }

    }
    public class MdlRedespatch360: MdlReDespatchSummary
    {
        public string retrievalrequest_gid { get; set; }
        public string requested_date { get; set; }
        public string requestedby_name { get; set; }
        public string retrieval_type { get; set; }
        public string approved_date { get; set; }
        public string approvalby_name { get; set; }
        public string req_remarks { get; set; }
        public string requested_for { get; set; }
        public string documentretrieved_status { get; set; }
        public string documentretrieved_mode { get; set; }
        public List<MdlIdasUploadDocument> MdlIdasUploadDocument { get; set; }
        public List<MdlTrnRequired> MdlTrnRequired { get; set; }
    }
    public class MdlRedespatch
    {
        public string redespatched_date { get; set; }
        public string redespatchedby_name { get; set; }
        public string contact_person { get; set; }
        public string remarks { get; set; }
        public string [] retrievalrequestdtls_gid { get; set; }
    }
    public class MdlReDespatchSummaryList:result 
    {
        public List<MdlReDespatchSummary> MdlReDespatchSummary { get; set; }
    }
    public class MdlReDespatchSummary
    {
        public string redespatch_gid { get; set; }
        public string redespatched_date { get; set; }
        public string redespatchedby_name { get; set; }
        public string contact_person { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }

    }
    public class MdlIdasRecordReceivedSummaryList : result
    {
        public List<MdlIdasRecordReceivedSummary> MdlIdasRecordReceivedSummary { get; set; }
    }

    public class MdlIdasRecordReceivedSummary
    {
        public string retrievalrequest_gid { get; set; }
        public string retrievalrequestdtls_gid { get; set; }
        public string requested_date { get; set; }
        public string requestedby_name { get; set; }
        public string documentreceivedto_name { get; set; }
        public string documentreceived_date { get; set; }
        public string contactperson_name { get; set; }
        public string filestampref_no { get; set; }
        public string boxstampref_no { get; set; }
        public string documentretrieved_status { get; set; }
        public string customer_name { get; set; }
        public string ensure_flag { get; set; }

    }

    public class MdlIdasUploadDocumentList:result 
    {
        public List<MdlIdasUploadDocument> MdlIdasUploadDocument { get; set; }
    }
    public class MdlIdasUploadDocument
    {
        public string uploaddocument_gid { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
        public string document_title { get; set; }
    }
    public class MdlReconciliationCount:result
    {
        public string file_count { get; set; }
        public string despatched_count { get; set; }
        public string permanet_count { get; set; }
        public string temporary_count { get; set; }
    }
    public class MdlReferene
    {
        public string reference_type { get; set; }
        public List<MdlGetBox> MdlGetBox { get; set; }
        public List<MdlCustomer> MdlCustomer { get; set; }
    }
 
  
    public class MdlBatchList:result
    {
        public List<MdlGetBatch> MdlGetBatch;
    }
    public class MdlGetBatch
    {
        public string batch_gid { get; set; }
        public string customer_gid { get; set; }
        public string cartonbox_gid { get; set; }
        public string despatch_gid { get; set; }
        public string customername { get; set; }
        public string fileref_no { get; set; }
        public string filestampref_no { get; set; }
        public string boxstampref_no { get; set; }
        public string cartonbox_date { get; set; }
        public string despatchref_no { get; set; }
        public string despatch_date { get; set; }
        public string contact_person { get; set; }
        public string despatched_by { get; set; }
    }
    public class MdlBoxList : result
    {
        public List<MdlGetBox> MdlGetBox { get; set; }
    }

    public class MdlGetBox
    {
        public string box_gid { get; set; }
        public string boxref_no { get; set; }
    }

    public class MdlDocumentReceived
    {
        public string retrievalrequest_gid { get; set; }
        public string documentretrieved_mode { get; set; }
        public string documentretrieved_date { get; set; }
        public string documentreceivedto_gid { get; set; }
        public string documentreceivedto_name { get; set; }
        public string contactperson_name { get; set; }
        public string mobile_no { get; set; }
        public string received_mode { get;set;}
    }

  

    public class MdlPostDocDtlReceived
    {
        public string retrievalrequestdtls_gid { get; set; }
        public string received_type { get; set; }
    }
    public class MdlCustomerList
    {
        public List<MdlCustomer> MdlCustomer { get; set; }
    }
    public class MdlCustomer
    {
        public string customer_gid { get; set; }
        public string customername { get; set; }
    }
}