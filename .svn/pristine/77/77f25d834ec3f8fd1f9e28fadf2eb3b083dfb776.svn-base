using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.iasn.Models
{
    public class result
    {
        public string message { get; set; }
        public bool status { get; set; }
    }

    public class MdlIsnEmployeelist : result
    {
        public List <MdlIsnEmployee> MdlIsnEmployee { get; set; }
    }

    public class MdlIsnEmployee

    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_code { get; set; }
    }

    public class WorkItemListCount : result
    {
        public string count_workitem { get; set; }
        public string count_pushback { get; set; }
        public string count_forward { get; set; }
        public string count_close { get; set; }
        public string count_archival { get; set; }
        public string count_workitempending { get; set; }
        public string count_workitemassigned { get; set; }
        public string count_workitemtotal { get; set; }
        public string count_composemail { get; set; }
    }

    public class WorkItemList : result
    {
        public string workitemref_no {get;set;}
        public string updatedby_on { get; set; }
        public List<MdlWorkItem> MdlWorkItem { get; set; }
    }
    public class MdlWorkItem
    {
        public string closedremarks { get; set; }
        public string archivalremarks { get; set; }
        public string email_gid { get; set; }
        public string workitemref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string[] emailSubject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string status_attachment { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string message_id { get; set; }
        public string email_to { get; set; }
        public string rmemployee_gid { get; set; }
        public string archival_type { get; set; }
        public string zone_name { get; set; }
        public string zone_gid { get; set; }
        public string rmemployee_name { get; set; }
        public string rmemployee_mailid { get; set; }
        public string employee_gid { get; set; }
        public string checkeremployee_name { get; set; }
        public string status { get; set; }
        public string reference_id { get; set; }
        public string aging { get; set; }
        public string seen_flag { get; set; }
        public string email_address { get; set; }
        public string allottedby_on { get; set; }
        public string updatedby_on { get; set; }
        public string customer_name { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
     public string acknowledgement_status { get; set; }
        public string composemail_gid { get; set; }
        public string employee_name { get; set; }
        public string remarks { get; set; }
        public string composemail_refno { get; set; }
        public string Statusupdated_by { get; set; }
        public string Statusupdated_date { get; set; }
        public string Mail_Trigger { get; set; }
        public string assigned_remarks { get; set; }
        public string firstemail_subject { get; set; }
        public string originalmail_Subject { get; set; }
        public string hold_flag { get; set; }
        public string workitemhold_reason { get; set; }
        public string customer_type { get; set; }
        public string customer_gid { get; set; }
    }

    public class MdlAttachmentList
    {
        public string mailattachment_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_extension { get; set; }
        public string composemailattachment_gid { get; set; }
    }

    public class MdlAssignTo
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string email_gid { get; set; }
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string zone_flag { get; set; }
        public string acknowledgement_flag { get; set; }
        public string assign_remarks { get; set; }
    }

   

    public class MdlDecisionhistorySummary:result 
    {
        public List<MdlDecisionhistory> MdlDecisionhistory { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
    }

    public class MdlDecisionhistory
    {
        public string email_gid { get; set; }
        public string decision_gid { get; set; }
        public string decision { get; set; }
        public string remarks { get; set; }
        public string subject { get; set; }
        public string tomail_id { get; set; }
        public string ccmail_id { get; set; }
        public string bccmail_id { get; set; }
        public string mailcontent { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string message_id { get; set; }
        public string reference_id { get; set; }
        public string close_acknowledge { get; set; }
        public string frommail_id { get; set; }
    }

    public class mdlarchivalcount : result
    {
        public string workitem_count { get; set; }
        public string archivalcustomer_count { get; set; }
        public string archivalspecific_count { get; set; }
    }

    public class MdlArchival
    {
        public string[] email_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string sanction_gid { get; set; }
        public string sanctionref_no { get; set; }
        public string archival_type { get; set; }
        public string remarks { get; set; }
        public string composemail_gid { get; set; }
        public string status { get; set; }
        public string mailcontent { get; set; }
        public string tomail_id { get; set; }
        public string ccmail_id { get; set; }
        public string bccmail_id { get; set; }
        public string email_subject { get; set; }
    }
    public class MdlArchivalCustomerList:result 
    {
        public List<MdlArchivalCustomer> MdlArchivalCustomer { get; set; }
    }
    public class MdlArchivalCustomer
    {
        public string customer_gid { get;set;}
        public string customer_name { get; set; }
        public string vertical_code { get; set; }
        public string no_of_workitem { get; set; }
        public string sanctionref_no { get; set; }
        public string remarks { get; set; }
    }
    public class MdlTransferLogList:result 
    {
        public List<MdlTransferLog> MdlTransferLog { get; set; }
    }
    public class MdlTransferLog
    {
        public string transferlog_gid { get; set; }
        public string assignedby_name { get; set; }
        public string transferby_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlcDocList
    {
        public List<MdlDocDetails> MdlDocDetails { get; set; }
    }

    public class MdlDocDetails
    {
        public string id { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
    }

    public class MdlMailID
    {
        public string employee_emailid { get; set; }
    }

    public class MdlProfile : result
    {
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string user_photo { get; set; }
        public string user_designation { get; set; }
        public string user_department { get; set; }
        public string user_mobileno { get; set; }

    }
    public class MdlReferenceMailList : result
    {
        public List<MdlReferenceMail> MdlReferenceMail { get; set; }
       
    }
    public class MdlReferenceMail
    {
        public string reference_gid { get; set; }
        public string email_gid { get; set; }
        public string reference_id { get; set; }
        public string email_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_cc { get; set; }
        public string email_bcc { get; set; }
        public string[] email_subject { get; set; }
        public string email_content { get; set; }
        public string message_id { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
        public string composemail_gid { get; set; }
        public string emailsubject { get; set; }
    }

    public class MdlArchivalCondition : result
    {
        public string customer_gid { get; set; }
        public string archival_type { get; set; }
        public string customer_type { get; set; }
        public string customer_name { get; set; }
        public string email_gid { get; set; }
    }

    public class MdlConsolidateWorkItem : result
    {
        public string closedremarks { get; set; }
        public string archivalremarks { get; set; }
        public string email_gid { get; set; }
      
        public string allottedby_on { get; set; }
        public string updatedby_on { get; set; }
        public string customer_name { get; set; }
        public string excel_path { get; set; }
       
        public string lscloudpath { get; set; }
        public string excel_name { get; set; }
        public string acknowledgement_status { get; set; }
    }
    public class ComposeMailList : result
    {
        public List<MdlWorkItem> MdlWorkItem { get; set; }
        public string email_subject { get; set; }
        public string frommail_id { get; set; }
        public string tomail_id { get; set; }
        public string ccmail_id { get; set; }
        public string bccmail_id { get; set; }
        public string mailcontent { get; set; }
        public string attachment_flag { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
        public string composemail_gid { get; set; }
        public string zone_name { get; set; }
        public string email_date { get; set; }
        public string email_status { get; set; }
    }
    public class MyWorkItemListCount : result
    {
        public string count_myworkitempushback { get; set; }
        public string count_myworkitemforward { get; set; }
        public string count_myworkitemclose { get; set; }
        public string count_myworkitempending { get; set; }
        public List<MdlAssignedList> MdlAssignedList { get; set; }
    }
    public class hold : result
    {
        public string workitemhold_reason { get; set; }
        public string email_gid { get; set; }
        public char assigned_flag { get; set; }
    }
    public class MdlHoldLogList : result
    {
        public List<MdlholdLog> MdlholdLog { get; set; }
    }
    public class MdlholdLog
    {
        public string workitem2hold_gid { get; set; }
        public string workitemhold_reason { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlAssignedList
    {
        public string employee_gid { get; set; }
        public string assigned_to { get; set; }
        public string assigned_count { get; set; }
    }
}