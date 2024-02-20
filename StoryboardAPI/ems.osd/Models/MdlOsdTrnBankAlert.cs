using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{
    public class MdlOsdTrnBankAlert : result
    {
       
        public string user_name { get; set; }
        public string kotakAPI_flag { get; set; }
        public string detailsreceived_at { get; set; }
        public string source { get; set; }
        public string Master_Acc_No { get; set; }
        public string Remitt_Info { get; set; }
        public string Remit_Name { get; set; }
        public string Remit_Ifsc { get; set; }
        public string Amount { get; set; }
        public string Txn_Ref_No { get; set; }
        public string Utr_No { get; set; }
        public string Pay_Mode { get; set; }
        public string E_Coll_Acc_No { get; set; }
        public string Remit_Ac_Nmbr { get; set; }
        public string Creditdateandtime { get; set; }
        public string Txn_Date { get; set; }
        public string Bene_Cust_Acname { get; set; }
        public string REF1 { get; set; }
        public string REF2 { get; set; }
        public string REF3 { get; set; }
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string aging { get; set; }
        public string customer_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_urnname { get; set; }
        public string vertical { get; set; }
        public string constitution { get; set; }
        public string zonal_head { get; set; }
        public string business_head { get; set; }
        public string cluster_manager { get; set; }
        public string rm_name { get; set; }
        public string credit_manager { get; set; }
        public string zonal_riskmanagerName { get; set; }
        public string riskmanager_name { get; set; }
        public string riskMonitoring_Name { get; set; }
        public string relationshipmanager_name { get; set; }
        public string allocated_status { get; set; }
        public string contact_person { get; set; }
        public string customer_name { get; set; }
        public string mobile_no { get; set; }
        public string address_type { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_gid { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string email_cc { get; set; }
        public string email_bcc { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string email_to { get; set; }
        public string from_mailaddress { get; set; }
        public string rm_status { get; set; }
        public string operation_status { get; set; }
        public string servicerequest_gid { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_type { get; set; }
        public string transferinitiated_date { get; set; }
        public string transferedinitiated_by { get; set; }
        public string transfer_remarks { get; set; }
        public string transferto_name { get; set; }
        public string transferto_gid { get; set; }
        public string rmupdated_flag { get; set; }
        public string lsrelationshipmanager_name { get; set; }
        public string employee_gid { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string zonalhead_name { get; set; }
        public string businesshead_name { get; set; }
        public string customername_application { get; set; }
        public string zonalriskmanager_name { get; set; }
        public string vertical_name { get; set; }
        public string risk_managername { get; set; }
        public string constitution_name { get; set; }
        public string headriskmonitoring_name { get; set; }
        public string relationship_managername { get; set; }
        public string drm_name { get; set; }
        public string clustermanager_name { get; set; }
        public string creditmanager_name { get; set; }
        public string contactpersonfirst_name { get; set; }
        public string regionalhead_name { get; set; }
        public string credithead_name { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string creditregionalmanager_name { get; set; }
    }
    public class MdlSendbacktobrs : result
    {
        public string banktransc_gid { get; set; }
        public string sendback_reason { get; set; }
        public string bankalert2allocated_gid { get; set; }        

    }
    public class MdlDocumentUpload : result
    {
        public string folder_name { get; set; }
        public string directory_type { get; set; }
        public string parent_directorygid { get; set; }
        public string customer_gid { get; set; }
        public string customer2sanction_gid { get; set; }
        public string remarks { get; set; }
    }
    public class MdlBankAlertAllocated : result
    {
        public List<BankAlertAllocated_list> BankAlertAllocated_list { get; set; }
        public List<BankAlerttransfer_list> BankAlerttransfer_list { get; set; }
        public List<BankAlertAllocatedAssigned_list> BankAlertAllocatedAssigned_list { get; set; }
        public List<BankAlertUnreconciliation_list> BankAlertUnreconciliation_list { get; set; }

    }
    public class MdlBankAlertCompleted : result
    {
        public List<BankAlertCompleted_list> BankAlertCompleted_list { get; set; }
    }
    public class BankAlerttransfer_list
    {
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string seen_flag { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string rm_status { get; set; }
        public string customer_name { get; set; }
        public string reason { get; set; }
        public string operation_status { get; set; }
        public string bankalert2notallocated_gid { get; set; }
        public string relationshipmanager_name { get; set; }
        public string assigned_toname { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_type { get; set; }
        public string transferto_name { get; set; }
        public string department_name { get; set; }
        public string kotakAPI_flag { get; set; }
    }
    public class BankAlertAllocatedAssigned_list
    {
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string seen_flag { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string rm_status { get; set; }
        public string customer_name { get; set; }
        public string reason { get; set; }
        public string operation_status { get; set; }
        public string bankalert2notallocated_gid { get; set; }
        public string relationshipmanager_name { get; set; }
        public string assigned_toname { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_type { get; set; }
        public string transferto_name { get; set; }
        public string department_name { get; set; }
        public string assigned_rh { get; set; }
        public string kotakAPI_flag { get; set; }
    }
    public class BankAlertAllocated_list
    {
        public string regionalheadlevel_name { get; set; }
        public string kotakAPI_flag { get; set; }
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string seen_flag { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string rm_status { get; set; }
        public string customer_name { get; set; }
        public string reason { get; set; }
        public string operation_status { get; set; }
        public string bankalert2notallocated_gid { get; set; }
        public string relationshipmanager_name { get; set; }
        public string assigned_toname { get; set; }
        public string transfer_flag { get; set; }
        public string department_name { get; set; }
        public string brs_flag { get; set; }
      

    }
    public class BankAlertUnreconciliation_list
    {
        public string customer_urn { get; set; }
        public string email_date { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string seen_flag { get; set; }
        public string ticketref_no { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string tagemployee_gid { get; set; }
        public string banktransc_gid { get; set; }
        public string taggedmember_gid { get; set; }
        public string taggedmember_name { get; set; }
        public string tagged_remarks { get; set; }
        public string tagged_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string trn_date { get; set; }
        public string custref_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string acc_no { get; set; }
        public string transc_balance { get; set; }
        public string tagged_status { get; set; }
        public string operation_status { get; set; }
        public string brs_flag { get; set; }
        public string assigned_date { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }


    }
    public class MdlOsdTrnunreconBankAlert : result
    {
        public string customer_urn { get; set; }
        public string email_date { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string seen_flag { get; set; }
        public string ticketref_no { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string tagemployee_gid { get; set; }
        public string banktransc_gid { get; set; }
        public string taggedmember_gid { get; set; }
        public string taggedmember_name { get; set; }
        public string tagged_remarks { get; set; }
        public string tagged_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string trn_date { get; set; }
        public string custref_no { get; set; }
        public string bank_name { get; set; }
        public string branch_name { get; set; }
        public string employee_gid { get; set; }
        public string kotakAPI_flag { get; set; }
        public string acc_no { get; set; }
        public string transc_balance { get; set; }
        public string tagged_status { get; set; }
        public string operation_status { get; set; }
        public string brs_flag { get; set; }
        public string chq_no { get; set; }
        public string remarks { get; set; }
        public string source { get; set; }
        public string value_date { get; set; }
        public string payment_date { get; set; }
        public string debit_amt { get; set; }
        public string credit_amt { get; set; }
        public string bank_gid { get; set; }
        public string cr_dr { get; set; }
        public string transact_particulars { get; set; }
        public string transact_val { get; set; }

    }
    public class BankAlertCompleted_list
    {
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string allocated_status { get; set; }
        public string aging { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string seen_flag { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string rm_status { get; set; }
        public string customer_name { get; set; }
        public string reason { get; set; }
        public string operation_status { get; set; }
        public string bankalert2notallocated_gid { get; set; }
        public string relationshipmanager_name { get; set; }
        public string assigned_toname { get; set; }
        public string transfer_flag { get; set; }
        public string department_name { get; set; }
        public string kotakAPI_flag { get; set; }

    }
    public class mdlMailContent : result
    {
        public string body { get; set; }
    }
    public class MdlRMStatus : result
    {

        public string rm_status { get; set; }
        public string rm_remarks { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string assigned_to { get; set; }
        public string assigned_toname { get; set; }
        public string assigned_remarks { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string transfer_type { get; set; }
        public string tickettransfer_remarks { get; set; }
        public string rmtransfer_remarks { get; set; }
        public List<rmdocument_list> rmdocument_list { get; set; }
        public string[] rmfilename { get; set; }
        public string rmfilepath { get; set; }
        public string banktransc_gid { get; set; }
        public string updation_remarks { get; set; }
        public string cbounreconciliation_status { get; set; }
        public string fileupload_gid { get; set; }



    }
    public class MdlViewRMStatus : result
    {
        public string rm_status { get; set; }
        public string rm_remarks { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string[] rmhfilename { get; set; }
        public string rmhfilepath { get; set; }
        public List<rmdocument_list> rmdocument_list { get; set; }

    }
    public class rmdocument_list
    {
        public string fileupload_gid { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }
    public class MdlBankAlertCount : result
    {
        public string rhApprovePending_count { get; set; }
        public string allocated_count { get; set; }
        public string operation_count { get; set; }
        public string notallocated_count { get; set; }
        public string transfer_count { get; set; }
        public string allocatedassigned_count { get; set; }
        public string unreconciliation_count { get; set; }
    }

    public class MdlBankNotification : result
    {
        public string allocated_new { get; set; }
        public string allocatedtransfer_new { get; set; }
        public string notallocated_new { get; set; }
        public string privilege { get; set; }
        public string display { get; set; }
    }
    public class Mailcontent : result
    {
        public string to { get; set; }
        public string body { get; set; }
        public string from { get; set; }
        public string attachment { get; set; }
        public string file_name { get; set; }
        public string subject { get; set; }
        public string cc { get; set; }
        public string message_id { get; set; }
        public string received_time { get; set; }
        public string attachment_status { get; set; }
        public string bcc { get; set; }
        public string header { get; set; }
    }

    public class Headers
    {
        public string Pragma { get; set; }
        public string TransferEncoding { get; set; }
        public string RetryAfter { get; set; }
        public string Vary { get; set; }
        public string xmsrequestid { get; set; }
        public string StrictTransportSecurity { get; set; }
        public string XContentTypeOptions { get; set; }
        public string XFrameOptions { get; set; }
        public string CacheControl { get; set; }
        public string Location { get; set; }
        public string SetCookie { get; set; }
        public string TimingAllowOrigin { get; set; }
        public string xmsapihubcachedresponse { get; set; }
        public string Date { get; set; }
        public string ContentType { get; set; }
        public string Expires { get; set; }
        public string ContentLength { get; set; }
    }

    //public class Body
    //{
    //    public string id { get; set; }
    //    public DateTime receivedDateTime { get; set; }
    //    public bool hasAttachments { get; set; }
    //    public string internetMessageId { get; set; }
    //    public string subject { get; set; }
    //    public string bodyPreview { get; set; }
    //    public string importance { get; set; }
    //    public string conversationId { get; set; }
    //    public bool isRead { get; set; }
    //    public bool isHtml { get; set; }
    //    public string body { get; set; }
    //    public string from { get; set; }
    //    public string toRecipients { get; set; }
    //    public Attachment[] attachments { get; set; }
    //}

    //public class Attachment
    //{
    //    public string odatatype { get; set; }
    //    public string id { get; set; }
    //    public DateTime lastModifiedDateTime { get; set; }
    //    public string name { get; set; }
    //    public string contentType { get; set; }
    //    public int size { get; set; }
    //    public bool isInline { get; set; }
    //    public string contentId { get; set; }
    //    public byte[] contentBytes { get; set; }
    //}

}