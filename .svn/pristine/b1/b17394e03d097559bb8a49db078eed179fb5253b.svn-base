using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{

    public class uploaddocument : result
    {
        public List<upload_list> upload_list { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }
        
        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
    }
    public class upload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }

    }

    public class servicerequest : result
    {
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public string activitymaster_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string content { get; set; }
        public List<tagmemberdtl> tagmemberdtl { get; set; }
    }

    public class tagmemberdtl
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class servicerequestdtllist : result
    {
        public string department_gid { get; set; }
        public string activitymaster_gid { get; set; }
        public string supportteam_gid { get; set; }
        public string raisedmember_gid { get; set; }
        public string assignedmember_gid { get; set; }
        public string ticket_status { get; set; }
        public string request_title { get; set; }
        public string request_refno { get; set; }
        public string raised_date { get; set; }
        public string formattedDate { get; set; }
        public List<servicerequestdtl> servicerequestdtl { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string lscloudpath { get; set; }
        public string source { get; set; }
        public int lsallotted_count { get; set; }
    }

    public class servicerequestdtl
    {
        public string request_refno { get; set; }
        public string servicerequest_gid { get; set; }
        public string raised_date { get; set; }
        public string department_name { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_by { get; set; }
        public string raised_department { get; set; }
        public string request_status { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_membergid { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string created_date { get; set; }
        public string reopen_flag { get; set; }
        public string request_status1 { get; set; }
        public string cancel_flag { get; set; }
        public string rejected_flag { get; set; }
        public string closed_flag { get; set; }
        public string completed_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string approvalrequest_flag { get; set; }
        public string approval_status { get; set; }
        public string aging { get; set; }
        public string priority { get; set; }
        public string activitymaster_gid { get; set; }
        public string Businessactivity_Status { get; set; }
        public string source { get; set; }




    }
    public class rejectedreqlist : result
    {
        
        public List<rejectedlist> rejectedlist { get; set; }
        
    }

    public class rejectedlist
    {
        public string request_refno { get; set; }
        public string servicerequest_gid { get; set; }
        public string raised_date { get; set; }
        public string department_name { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_by { get; set; }
        public string raised_department { get; set; }
        public string request_status { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_membergid { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string created_date { get; set; }
        public string reopen_flag { get; set; }
        public string request_status1 { get; set; }
        public string cancel_flag { get; set; }
        public string rejected_flag { get; set; }
        public string closed_flag { get; set; }
        public string completed_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string approvalrequest_flag { get; set; }
        public string approval_status { get; set; }
        public string aging { get; set; }
        public string priority { get; set; }
        public string activitymaster_gid { get; set; }
        public string Businessactivity_Status { get; set; }
    }

    public class cancelledreqlist : result
    {

        public List<cancelledlist> cancelledlist { get; set; }

    }

    public class cancelledlist
    {
        public string request_refno { get; set; }
        public string servicerequest_gid { get; set; }
        public string raised_date { get; set; }
        public string department_name { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_by { get; set; }
        public string raised_department { get; set; }
        public string request_status { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string response_flag { get; set; }
        public string assigned_membergid { get; set; }
        public string assigned_supportteamgid { get; set; }
        public string created_date { get; set; }
        public string reopen_flag { get; set; }
        public string request_status1 { get; set; }
        public string cancel_flag { get; set; }
        public string rejected_flag { get; set; }
        public string closed_flag { get; set; }
        public string completed_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string approvalrequest_flag { get; set; }
        public string approval_status { get; set; }
        public string aging { get; set; }
        public string priority { get; set; }
        public string activitymaster_gid { get; set; }
        public string Businessactivity_Status { get; set; }
    }
    public class servicerequestview : result
    {
        public string servicerequest_gid { get; set; }
        public string raised_date { get; set; }
        public string request_refno { get; set; }
        public string department_name { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_by { get; set; }
        public string raised_department { get; set; }
        public string request_status { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string forward_remarks { get; set; }
        public string forward_date { get; set; }
        public string forward_to { get; set; }
        public List<tagmemberdtl> tagmemberdtl { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string reopen_reason { get; set; }
        public string reopened_date { get; set; }
        public string assigned_status { get; set; }
        public string reopen_flag { get; set; }
        public string completed_flag { get; set; }
        public string completed_remarks { get; set; }
        public string forward_flag { get; set; }
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string closed_by { get; set; }
        public string closed_date { get; set; }
        public string closed_flag { get; set; }
        public string rejected_flag { get; set; }
        public string cancel_flag { get; set; }
        public string cancel_date { get; set; }
        public string rejected_date { get; set; }
        public string rejected_remarks { get; set; }
        public string rejected_by { get; set; }
        public string baselocation_name { get; set; }
        public string content { get; set; }
        public string employee_number { get; set; }
        public string employee_mobileno { get; set; }
        public string manager_name { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string completedflag { get; set; }
        public string bankalert_flag { get; set; }
        public List<servicerequestdocumentdtl> servicerequestdocumentdtl { get; set; }
        public List<reopenrequestdocumentdtl> reopenrequestdocumentdtl { get; set; }
        public List<completerequestdocumentdtl> completerequestdocumentdtl { get; set; }
        public string[] srfilename { get; set; }
        public string srfilepath { get; set; }
        public string[] comfilename { get; set; }
        public string comfilepath { get; set; }
        public string[] reopenfilename { get; set; }
        public string reopenfilepath { get; set; }
        public string[] reopfilename { get; set; }
        public string reopfilepath { get; set; }
    }

    public class servicerequestdocumentdtl
    {
        public string servicereqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }

    public class requestcount
    {
        public string request_count { get; set; }
        public string tagged_count { get; set; }
        public string forward_count { get; set; }
        public string reopen_count { get; set; }
        public string close_count { get; set; }
        public string reject_count { get; set; }
        public string cancel_count { get; set; }
    }

    public class forwardrequestdtllist
    {
        public List<forwardrequestdtl> forwardrequestdtl { get; set; }
    }

    public class forwardrequestdtl
    {
        public string request_refno { get; set; }
        public string servicerequest_gid { get; set; }
        public string raised_date { get; set; }
        public string department_name { get; set; }
        public string departmentname { get; set; }
        public string department_gid { get; set; }
        public string raised_by { get; set; }
        public string raised_department { get; set; }
        public string request_status { get; set; }
        public string request_status1 { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string assigned_team { get; set; }
        public string assigned_to { get; set; }
        public string transfer_flag { get; set; }
        public string reopen_flag { get; set; }
        public string response_flag { get; set; }
        public string bankalert_flag { get; set; }
        public string bankalert2allocated_gid { get; set; }
        public string customer_gid { get; set; }
        public string getapproval_flag { get; set; }
        public string approval_status { get; set; }
        public string approvalforward_flag { get; set; }
        public string Businessactivity_Status { get; set; }

    }

    public class reopenrequest : result
    {
        public string servicerequest_gid { get; set; }
        public string assigned_status { get; set; }
        public string request_status { get; set; }
        public string reopened_date { get; set; }
        public string reopen_reason { get; set; }
        public string content { get; set; }
        public List<tagmemberdtl> tagmemberdtl { get; set; }
    }
    public class reopenrequestdocumentdtl : result
    {
        public string tmp_documentGid { get; set; }
        public string reopenreqdocument_gid { get; set; }
        public string servicereqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] reofilename { get; set; }
        public string reofilepath { get; set; }

    }
    public class completerequestdocumentdtl : result
    {
        public string tmp_documentGid { get; set; }
        public string completereqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }

        public string[] comfilename { get; set; }
        public string compath { get; set; }
    }

    public class MdlEmployee : result
    {
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class managerdtllist : result
    {
        public List<managerdtllist> managerdtl { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }

    }
}