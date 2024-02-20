using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{

    public class allottedlist : result
    {
        public List<allotteddtl> allotteddtl { get; set; }
    
        public List<businessstatusunitmyactivity_list> businessstatusunitmyactivity_list { get; set; }
        public List<servicerequestactivityhistory_list> servicerequestactivityhistory_list { get; set; }


    }

    public class businessstatusunitmyactivity_list : result
    {
        public string businessstatusactivity_gid { get; set; }
        public string businessactivity_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class servicerequestactivityhistory_list : result
    {
        public string activitymaster_gid { get; set; }
        public string activity_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class allotteddtl : result
    {

        public string servicerequest_gid { get; set; }
        public string priority { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_member { get; set; }
        public string getapproval_flag { get; set; }
        public string request_description { get; set; }
        public string forward_remarks { get; set; }
        public string forward_date { get; set; }
        public string forward_to { get; set; }
        public string forward_flag { get; set; }
        public string assigned_status { get; set; }
        public string approval_status { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string assigned_membergid { get; set; }
        public string reopen_flag { get; set; }
        public string reopen_reason { get; set; }
        public string reopened_date { get; set; }
        public string completed_flag { get; set; }
        public string rejected_flag { get; set; }
        public string rejected_remarks { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string cancel_flag { get; set; }
        public string cancel_date { get; set; }
        public string content { get; set; }
        public string reopened_aging { get; set; }
        public string baselocation_name { get; set; }
        public string requestraisedby_gid { get; set; }
        public string activitymaster_gid { get; set; }
        public  string source { get; set; }
        public List<alloteddocumentdtl> alloteddocumentdtl { get; set; }
        public List<forwarddocumentdtl> forwarddocumentdtl { get; set; }
        public List<servicerequestdocumentdtl> servicerequestdocumentdtl { get; set; }
        public List<reopenrequestdocumentdtl> reopenrequestdocumentdtl { get; set; }
        public List<reopendtl> reopendtl { get; set; }
        public List<forwardreopendocumentdtl> forwardreopendocumentdtl { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string employee_number { get; set; }
        public string employee_mobileno { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string aging { get; set; }
        public string approvalreq_date { get; set; }
        public string Businessactivity_Status { get; set; }
        public string[] allofilename { get; set; }
        public string allofilepath { get; set; }
        public string[] rofilename { get; set; }
        public string rofilepath { get; set; }
        public string[] srfilename { get; set; }
        public string srfilepath { get; set; }
        public string[] frfilename { get; set; }
        public string frfilepath { get; set; }
        public string[] fwdfilename { get; set; }
        public string fwdfilepath { get; set; }
        public string[] frrofilename { get; set; }
        public string frrofilepath { get; set; }

    }
    public class forwardreopendocumentdtl : result
    {
        public string forwardreqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class alloteddocumentdtl : result
    {
        public string servicereqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] allofilename { get; set; }
        public string allofilepath { get; set; }
    }
    public class forwarddocumentdtl : result
    {
        public string forwardreqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }

    }

    public class workinprogresslist
    {
        public int lsworkinprogress_count { get; set; }
        public List<workinprogressdtl> workinprogressdtl { get; set; }
    }

    public class workinprogressdtl : result
    {
        public string servicerequest_gid { get; set; }

        public string source { get;set; }
         public string brs_flag { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string getapproval_flag { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_team { get; set; }
        public string transfer_to { get; set; }
        public string approval_status { get; set; }
        public string response_flag { get; set; }
        public string assigned_membergid { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string reopen_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string aging { get; set; }
        public string activitymaster_gid { get; set; }
        public string priority { get; set; }
        public string Businessactivity_Status { get; set; }

    }
    public class ackcomplete : result
    {
        public string servicerequest_gid { get; set; }
        public string priority { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_member { get; set; }
        public string getapproval_flag { get; set; }
        public string request_description { get; set; }
        public string forward_remarks { get; set; }
        public string forward_date { get; set; }
        public string forward_to { get; set; }
        public string forward_flag { get; set; }
        public string assigned_status { get; set; }
        public string approval_status { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string assigned_membergid { get; set; }
        public string reopen_flag { get; set; }
        public string reopen_reason { get; set; }
        public string reopened_date { get; set; }
        public string completed_flag { get; set; }
        public string rejected_flag { get; set; }
        public string rejected_remarks { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string cancel_flag { get; set; }
        public string cancel_date { get; set; }
        public string content { get; set; }
        public string reopened_aging { get; set; }
        public string baselocation_name { get; set; }
        public string requestraisedby_gid { get; set; }
        public string activitymaster_gid { get; set; }
        public List<alloteddocumentdtl> alloteddocumentdtl { get; set; }
        public List<forwarddocumentdtl> forwarddocumentdtl { get; set; }
        public List<servicerequestdocumentdtl> servicerequestdocumentdtl { get; set; }
        public List<reopenrequestdocumentdtl> reopenrequestdocumentdtl { get; set; }
        public List<reopendtl> reopendtl { get; set; }
        public List<forwardreopendocumentdtl> forwardreopendocumentdtl { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string employee_number { get; set; }
        public string employee_mobileno { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string aging { get; set; }
        public string approvalreq_date { get; set; }
        public string Businessactivity_Status { get; set; }
        public string[] allofilename { get; set; }
        public string allofilepath { get; set; }
        public string[] rofilename { get; set; }
        public string rofilepath { get; set; }
        public string[] srfilename { get; set; }
        public string srfilepath { get; set; }
        public string[] frfilename { get; set; }
        public string frfilepath { get; set; }
        public string[] fwdfilename { get; set; }
        public string fwdfilepath { get; set; }
        public string[] frrofilename { get; set; }
        public string frrofilepath { get; set; }
        public List<completeddtl> completeddtl { get; set; }
        public List<completerequestdocumentdtl> completerequestdocumentdtl { get; set; }
       
        public string completed_remarks { get; set; }
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string[] complefilename { get; set; }
        public string complefilepath { get; set; }
    }

    public class completedlist
    {
        public List<completeddtl> completeddtl { get; set; }
        public List<completerequestdocumentdtl> completerequestdocumentdtl { get; set; }
        public string completed_flag { get; set; }
        public string completed_remarks { get; set; }
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string[] complefilename { get; set; }
        public string complefilepath { get; set; }
    }

    public class completeddtl : result
    {
        public string source { get; set; }
        public string servicerequest_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_team { get; set; }
        public string transfer_to { get; set; }
        public string reopen_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string aging { get; set; }
        public string Businessactivity_Status { get; set; }
    }

    public class closedlist
    {
        public List<closeddtl> closeddtl { get; set; }
        public List<rejectcanceldtl> rejectcanceldtl { get; set; }
    }

    public class rejectcanceldtl : result
    {
        public string servicerequest_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string source { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_team { get; set; }
        public string transfer_to { get; set; }
        public string reopen_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string aging { get; set; }
        public string Businessactivity_Status { get; set; }

    }

    public class closeddtl : result
    {

        public string source { get; set; }
        public string servicerequest_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_department { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string transfer_team { get; set; }
        public string transfer_to { get; set; }
        public string reopen_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string aging { get; set; }
        public string baselocation_name { get; set; }
        public string Businessactivity_Status { get; set; }




    }

    public class forwardlist
    {
        public List<forwarddtl> forwarddtl { get; set; }
        public List<forwardreopendtl> forwardreopendtl { get; set; }
    }

    public class forwarddtl : result
    {
        public string servicerequest_gid { get; set; }
        public string source { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string getapproval_flag { get; set; }
        public string forward_to { get; set; }
        public string forwardto_gid { get; set; }
        public string forward_remarks { get; set; }
        public string transfer_flag { get; set; }
        public string approval_status { get; set; }
        public string forward_date { get; set; }
        public string reopen_flag { get; set; }
        public string content { get; set; }
        public string response_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string aging { get; set; }
        public string business_status { get; set; }
        public string Businessactivity_Status { get; set; }


    }

    public class countlist : result
    {
        public string alloted_count { get; set; }
        public string workinprogress_count { get; set; }
        public string approvalpending_count { get; set; }
        public string completed_count { get; set; }
        public string closed_count { get; set; }
        public int  lsforward_count { get; set; }
        public string forward_count { get; set; }

        public string transfer_count { get; set; }
        public string rejectcancel_count { get; set; }
        public int lsallotedcount { get; set; }
        public int lsworkinprogress_count { get; set; }
        public List<allotteddtl> allotteddtl { get; set; }
        public List<allotteddtl> forwarddtl { get; set; }
        public List<workinprogressdtl> workinprogressdtl { get; set; }
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

    public class requestorlist : result
    {
        public List<requestordtl> requestordtl { get; set; }
        public List<requestordtlhistory> requestordtlhistory { get; set; }
        public List<businessstatuslist> businessstatuslist { get; set; }

    }
    public class businessstatuslist : result
    {
        public string business_status { get; set; }
        public string businessstatus_gid { get; set; }

    }
    public class requestordtl : result
    {
        public string requestorcommunication_gid { get; set; }
        public string servicerequest_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string sender_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string session_user { get; set; }
        public string document_attached { get; set; }
        public string lsflag { get; set; }
    }
    public class requestordtlhistory : result
    {
        public string requestorcommunication_gid { get; set; }
        public string servicerequest_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string sender_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string session_user { get; set; }
        public string document_attached { get; set; }
        public string lsflag { get; set; }
    }
    public class transferdtl : result
    {
        public List<activitydtl> activitydtl { get; set; }
        public string servicerequest_gid { get; set; }
        public string transferteam_gid { get; set; }
        public string transferteam_name { get; set; }
        public string transferemployee_gid { get; set; }
        public string transferemployee_name { get; set; }
        public string remarks { get; set; }
        public string assigned_supportteam { get; set; }
        public string assigned_member { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string assigned_membergid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string content { get; set; }
        public string priority { get; set; }
        public string activity_name { get; set; }
        public string activitymaster_gid { get; set; }



    }
    public class approvaldtl : result
    {
        public string servicerequest_gid { get; set; }
        public string approval_remarks { get; set; }
        public string approval_type { get; set; }
        public string approval_basedon { get; set; }
        public string approvalname { get; set; }
        public string approvalgid { get; set; }
        public string tmpapprovalmember_gid { get; set; }
        public string approvalforward_flag { get; set; }
        public string approvalrequest_flag { get; set; }
        public string approvalreopen_flag { get; set; }
        public List<approvalmember> approvalmember { get; set; }
    }

    public class approvalmember
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string tmpapprovalmember_gid { get; set; }
    }

    public class taggedlist
    {
        public List<taggeddtl> taggeddtl { get; set; }
    }

    public class taggeddtl : result
    {
        public string servicerequest_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string reopen_flag { get; set; }
        public string request_status1 { get; set; }
        public string response_flag { get; set; }
        public string Businessactivity_Status { get; set; }

    }

    public class approvallist : result
    {
        public string employee_gid { get; set; }
        public List<approvaldetails> approvaldetails { get; set; }
        public List<assetdetails> assetdetails { get; set; }
        public List<approvaldetailshistory> approvaldetailshistory { get; set; }
    }

    public class approvaldetails
    {
        public string approval_status { get; set; }
        public string approval_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approval_type { get; set; }
        public string requestapproval_gid { get; set; }
        public string approval_token { get; set; }
        public string approvalinitiated_by { get; set; }
        public string requestapproval_remarks { get; set; }
        public string cancel_flag { get; set; }
        public string approvalemployee_gid { get; set; }
        public string approvalreq_date { get; set; }


    }
    public class assetdetails
    {
        public string asset_name { get; set; }
        public string asset_id { get; set; }
        public string invoice_date { get; set; }
        public string financial_year { get; set; }
        public string status { get; set; }
        public string serial_no { get; set; }
        public string invoice_refno { get; set; }
        public string issued_by { get; set; }
        public string issued_date { get; set; }
        public string warrantyref_no { get; set; }
        public string Warrantystart_date { get; set; }
        public string radexpiry_date { get; set; }
        public string extenderstart_date { get; set; }
        public string extenderend_date { get; set; }




    }

    public class approvaldetailshistory
    {
        public string approval_status { get; set; }
        public string approval_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approval_type { get; set; }
        public string requestapproval_gid { get; set; }
        public string approval_token { get; set; }
        public string approvalinitiated_by { get; set; }
    }

    public class transferlist
    {
        public List<transferlistdtl> transferlistdtl { get; set; }
        public List<prioritylistdtl> prioritylistdtl { get; set; }

        public List<transferlistdtlreopen> transferlistdtlreopen { get; set; }
    }

    public class transferlistdtl : result
    {
        public string servicerequest_gid { get; set; }
        public string source { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_membergid { get; set; }
        public string transfer_supportteamgid { get; set; }
        public string transfer_membername { get; set; }
        public string transfer_supportteamname { get; set; }
        public string transfer_date { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_date { get; set; }
        public string reopen_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string aging { get; set; }
        public string priority { get; set; }
        public string Businessactivity_Status { get; set; }


    }

    public class transferlistdtlreopen : result
    {
        public string servicerequest_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_membergid { get; set; }
        public string transfer_supportteamgid { get; set; }
        public string transfer_membername { get; set; }
        public string transfer_supportteamname { get; set; }
        public string transfer_date { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_date { get; set; }
        public string reopen_flag { get; set; }
    }
    public class prioritylistdtl : result
    {
        public string created_date { get; set; }
        public string priority { get; set; }
        public string created_by { get; set; }


    }
    public class completed : result
    {
        public string servicerequest_gid { get; set; }
        public string completed_remarks { get; set; }
        public string content { get; set; }

    }
    public class reopenreqlist
    {
        public List<reopendtl> reopendtl { get; set; }
    }

    public class reopendtl : result
    {
        public string servicerequest_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_date { get; set; }
        public string raised_by { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string reopen_flag { get; set; }
        public string request_status1 { get; set; }
        public string reopen_reason { get; set; }
        public string reopened_date { get; set; }
        public string reopened_by { get; set; }
        public string requestreopen_gid { get; set; }
        public string response_flag { get; set; }
        public string approvalreopen_flag { get; set; }
        public string approval_status { get; set; }
        public string created_aging { get; set; }
        public string Businessactivity_Status { get; set; }


    }
    public class forwardreopendtl : result
    {
        public string forward_to { get; set; }
        public string forwardto_gid { get; set; }
        public string forward_remarks { get; set; }
        public string forward_date { get; set; }
    }
    public class reopenhistory : result
    {
        public List<forwardreopendtl> forwardreopendtl { get; set; }
        public List<transferlistdtlreopen> transferlistdtlreopen { get; set; }
        public List<forwarddocumentdtl> forwarddocumentdtl { get; set; }
        public List<completereopendocumentdtl> completereopendocumentdtl { get; set; }
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string completed_remarks { get; set; }
        public string raised_by { get; set; }
        public string raised_date { get; set; }
        public string request_refno { get; set; }
        public string reopencompleted_flag { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] fwdfilename { get; set; }
        public string fwdfilepath { get; set; }
    }
    public class completereopendocumentdtl : result
    {
        public string completereqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class reject : result
    {
        public string servicerequest_gid { get; set; }
        public string rejected_remarks { get; set; }
        public string content { get; set; }

    }
}