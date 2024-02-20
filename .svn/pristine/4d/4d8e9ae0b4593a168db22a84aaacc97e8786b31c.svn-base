using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{
    public class MdlQueryPending : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryPendingList> QueryPendingList { get; set; }
    }
    public class QueryPendingList
    {
        public string closedremarks { get; set; }
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string emailSubject { get; set; }
        public string email_content { get; set; }
        public string created_date { get; set; }
        public string status_attachment { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string message_id { get; set; }
        public string email_to { get; set; }
        public string status { get; set; }
        public string reference_id { get; set; }
        public string aging { get; set; }
        public string seen_flag { get; set; }
        public string email_address { get; set; }
        public string updatedby_on { get; set; }
        public string acknowledgement_status { get; set; }
    }
    public class MdlQueryview : result
    {
        public string ticketref_no { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string aging { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string email_to { get; set; }
        public string from_mailaddress { get; set; }
        public string frommail_id { get; set; }
        public string tomail_id { get; set; }
        public string ccmail_id { get; set; }
        public string bccmail_id { get; set; }
        public string mail_date { get; set; }
        public string mail_subject { get; set; }
        public string mailcontent { get; set; }
    }
    public class MdlQueryAttachments : result
    {
        public List<QueryAttachmentsList> QueryAttachmentsList { get; set; }
    }
    public class QueryAttachmentsList
    {
        public string mailattachment_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_extension { get; set; }
        public string composemailattachment_gid { get; set; }
    }

     public class Ticketassign : result
    {
        public string email_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_mailid { get; set; }
        public string assigned_remarks { get; set; }
        public string created_date { get; set; }
        public string content { get; set; }
    }

    public class MdlQueryAssign : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryAssignList> QueryAssignList { get; set; }
    }
    public class QueryAssignList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string from_mailaddress { get; set; }
    }
    public class MdlAssignedQuery360 : result
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string assigned_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string from_mailaddress { get; set; }
        public string assigned_remarks { get; set; }
        public List<Query360list> Query360list { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
    }
    public class Query360list
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string created_date { get; set; }

    }
    public class MdlAuditLogHistory : result
    {
        public List<MdlAuditLog> MdlAuditLog { get; set; }
    }

    public class MdlAuditLog
    {
        public string action_taken { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlAssignedQuery : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<AssignedQueryList> AssignedQueryList { get; set; }
    }
    public class AssignedQueryList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string from_mailaddress { get; set; }
    }
    public class MdlEmailSignature
    {
        public string emailsignature { get; set; }
        public string frommail_id { get; set; }
    }
    public class MdlDecisionhistorySummary : result
    {
        public List<MdlDecisionhistory> MdlDecisionhistory { get; set; }
        //public List<MdlAttachmentList> MdlAttachmentList { get; set; }
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
    public class MdlTransferLogList : result
    {
        public List<MdlTransferLog> MdlTransferLog { get; set; }
    }
    public class MdlTransferLog
    {
        public string transferlog_gid { get; set; }
        public string assignedby_name { get; set; }
        public string transferby_name { get; set; }
        public string created_by { get; set; }
        public string transfered_date { get; set; }
        public string transfer_remarks { get; set; }
    }
    public class MdlAssignTo : result
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string email_gid { get; set; }
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string zone_flag { get; set; }
        public string acknowledgement_flag { get; set; }
        public string assign_remarks { get; set; }
        public string transfer_remarks { get; set; }
    }
    public class MdlcDocList
    {
        public List<MdlDocDetails> MdlDocDetails { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }

    public class MdlDocDetails
    {
        public string id { get; set; }
        public string document_path { get; set; }
        public string document_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class MdlQueryClose : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryCloseList> QueryCloseList { get; set; }
    }
    public class QueryCloseList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string closed_by { get; set; }
        public string closed_date { get; set; }
    }
    public class MdlQueryClose360 : result
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string assigned_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string from_mailaddress { get; set; }
        public string assigned_remarks { get; set; }
        public string closed_remarks { get; set; }
        public string closed_by { get; set; }
        public string closed_date { get; set; }
        public List<QueryClose360list> QueryClose360list { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
    }
    public class QueryClose360list
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string created_date { get; set; }

    }
    public class MdlAssignedQueryClose : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryAssignedCloseList> QueryAssignedCloseList { get; set; }
    }
    public class QueryAssignedCloseList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string closed_by { get; set; }
        public string closed_date { get; set; }
    }
    public class MdlAssignQueryClose360 : result
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string assigned_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string from_mailaddress { get; set; }
        public string assigned_remarks { get; set; }
        public string closed_remarks { get; set; }
        public string closed_by { get; set; }
        public string closed_date { get; set; }
        public List<QueryAssignClose360list> QueryAssignClose360list { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
    }
    public class QueryAssignClose360list
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string created_date { get; set; }

    }
    public class MdlAssignedQueryReply : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryAssignedReplyList> QueryAssignedReplyList { get; set; }
    }
    public class QueryAssignedReplyList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string reply_by { get; set; }
        public string reply_date { get; set; }
        public string from_mailaddress { get; set; }
    }
    public class MdlAssignQueryReply360 : result
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string assigned_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string from_mailaddress { get; set; }
        public string assigned_remarks { get; set; }
        public string reply_remarks { get; set; }
        public string reply_by { get; set; }
        public string reply_date { get; set; }
        public List<QueryAssignReply360list> QueryAssignReply360list { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
    }
    public class QueryAssignReply360list
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string created_date { get; set; }

    }
    public class MdlAssignedQueryForward : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryAssignedForwardList> QueryAssignedForwardList { get; set; }
    }
    public class QueryAssignedForwardList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string forward_by { get; set; }
        public string forward_date { get; set; }
        public string from_mailaddress { get; set; }
    }
    public class MdlAssignQueryForward360 : result
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string assigned_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string from_mailaddress { get; set; }
        public string assigned_remarks { get; set; }
        public string forward_by { get; set; }
        public string forward_date { get; set; }
        public List<QueryAssignForward360list> QueryAssignForward360list { get; set; }
    }
    public class QueryAssignForward360list
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string created_date { get; set; }

    }
    public class MdlAssignedQueryTransfer : result
    {
        public string ticketref_no { get; set; }
        public string updatedby_on { get; set; }
        public List<QueryAssignedTransferList> QueryAssignedTransferList { get; set; }
    }
    public class QueryAssignedTransferList
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string email_subject { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string transfer_by { get; set; }
        public string transfer_date { get; set; }
        public string transfer_remarks { get; set; }
    }
    public class MdlAssignQueryTransfer360 : result
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string assigned_by { get; set; }
        public string assigned_date { get; set; }
        public string assigned_to { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string from_mailaddress { get; set; }
        public string assigned_remarks { get; set; }
        public string transfer_by { get; set; }
        public string transfer_date { get; set; }
        public string transfer_remarks { get; set; }
        public List<QueryAssignTransferlist> QueryAssignTransferlist { get; set; }
        public List<MdlAttachmentList> MdlAttachmentList { get; set; }
    }
    public class QueryAssignTransferlist
    {
        public string email_gid { get; set; }
        public string ticketref_no { get; set; }
        public string email_subject { get; set; }
        public string email_content { get; set; }
        public string email_to { get; set; }
        public string cc { get; set; }
        public string bcc { get; set; }
        public string status { get; set; }
        public string aging { get; set; }
        public string email_from { get; set; }
        public string email_date { get; set; }
        public string created_date { get; set; }

    }
    public class MdlComposeMail360List : result
    {
        public List<MdlComposeMaillist> MdlComposeMaillist { get; set; }

    }
    public class MdlComposeMaillist
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
        public string emailsubject { get; set; }
    }
    public class MdlAttachmentList
    {
        public string mailattachment_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_extension { get; set; }
    }
    public class AssignedQueryCount
    {
        public string assigned_count { get; set; }
        public string reply_count { get; set; }
        public string forward_count { get; set; }
        public string transfer_count { get; set; }
        public string close_count { get; set; }
    }
    public class QueryAssignmentCount
    {
        public string pending_count { get; set; }
        public string assign_count { get; set; }
        public string close_count { get; set; }
    }
    public class MdlTransferEmployeeList : result
    {
        public List<TransferEmployeeList> TransferEmployeeList { get; set; }
    }

    public class TransferEmployeeList

    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_code { get; set; }
    }

}