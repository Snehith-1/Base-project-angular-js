using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{
    public class requestapproval : result
    {
        public string approval_remarks { get; set; }
        public string approval_token { get; set; }
        public string servicerequest_gid { get; set; }
        public string hierary_level { get; set; }
        public string approval_type { get; set; }
        public string request_refno { get; set; }
        public string request_title { get; set; }
        public string requesteddtl { get; set; }
        public string approvalreq_by { get; set; }
        public string approvalreqdate { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string requestapproval_gid { get; set; }
        public string requestapproval_remarks { get; set; }
        public string request_description { get; set; }
        public List<approvalsummarylist> approvalsummarylist { get; set; }
        public List<approvalcompletedlist> approvalcompletedlist { get; set; }
        public List<rhapprovalsummarylist> rhapprovalsummarylist { get; set; }
        public List<rhapprovalcompletedlist> rhapprovalcompletedlist { get; set; }
        public string bankalertrefundapprl_gid { get; set; }
        public string ref_no { get; set; }
        public string customerurn { get; set; }
        public string customername { get; set; }
        public string assignedrmname { get; set; }
        public string approvalStatus { get; set; }
        public string rh_remarks { get; set; }
        public List<rhapprovaldetails> rhapprovaldetails { get; set; }
        public List<rhrejecteddetails> rhrejecteddetails { get; set; }
    }

    public class requesttokendtl:result
    {
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string raised_department { get; set; }
        public string raised_dtl { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string assigned_dtl { get; set; }
        public string getapproval_remarks { get; set; }
        public string hierary_level { get; set; }
        public string servicerequest_gid { get; set; }
        public string approval_type { get; set; }
    }
    public class approvalsummarylist
    {
        public string activity_name { get; set; }
        public string servicerequest_gid { get; set; }
        public string request_title { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string request_refno { get; set; }
        public string approvalreq_date { get; set; }
        public string approvalreq_by { get; set; }
        public string approval_status { get; set; }
        public string hierary_level { get; set; }
        public string requestapproval_gid { get; set; }
    }
    public class approvalcompletedlist
    {
        public string requestapproval_gid { get; set; }
        public string activity_name { get; set; }
        public string request_title { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string request_refno { get; set; }
        public string approvalreq_date { get; set; }
        public string approvalreq_by { get; set; }
        public string approval_status { get; set; }
        public string servicerequest_gid { get; set; }
    }
    public class allocatedtl:result
    {
        public string servicerequest_gid { get; set; }
        public string remarks { get; set; }
        public string assigned_membername { get; set; }
        public string assigned_membergid { get; set; }
        public string allocate_managergid { get; set; }
        public string allocate_managername { get; set; }
        public string content { get; set; }
    }
    public class selfallocatelist : result
    {
        public List<allocatelistdtl> allocatelistdtl { get; set; }

        public List<allocatelistdtlreopen> allocatelistdtlreopen { get; set; }
        public string assigned_membername { get; set; }
        public string assigned_membergid { get; set; }
        public string allocate_managergid { get; set; }
        public string allocate_managername { get; set; }
    }
    public class allocatelistdtl : result
    {
        public string assigned_membername { get; set; }
        public string assigned_membergid { get; set; }
        public string allocate_managergid { get; set; }
        public string allocate_managername { get; set; }
        public string allocate_by { get; set; }
        public string allocate_date { get; set; }
        public string transfer_flag { get; set; }
        public string assigned_date { get; set;}

    }

    public class allocatelistdtlreopen : result
    {
        public string assigned_membername { get; set; }
        public string assigned_membergid { get; set; }
        public string allocate_managergid { get; set; }
        public string allocate_managername { get; set; }
        public string allocate_by { get; set; }
        public string allocate_date { get; set; }
        public string transfer_flag { get; set; }
        public string assigned_date { get; set; }
    }
    public class rhapprovalsummarylist
    {
        public string bankalertrefundapprl_gid { get; set; }
        public string ref_no { get; set; }
        public string customerurn { get; set; }
        public string customername { get; set; }
        public string assignedrmname { get; set; }
        public string approvalStatus { get; set; }
        public string bankalert2allocated_gid { get; set; }
    }
    public class rhapprovalcompletedlist
    {
        public string bankalertrefundapprl_gid { get; set; }
        public string ref_no { get; set; }
        public string customerurn { get; set; }
        public string customername { get; set; }
        public string assignedrmname { get; set; }
        public string approvalStatus { get; set; }
        public string bankalert2allocated_gid { get; set; }
    }
    public class rhapprovaldetails
    {
        public string approval_status { get; set; }
        public string assignedrh_name { get; set; }
        public string rh_remarks { get; set; }
        public string created_by { get; set; }
        public string approval_date { get; set; }
        public string bankalertrefundapprl_gid { get; set; }
        

    }



    public class rhrejecteddetails
    {
        public string approval_status { get; set; }
        public string assignedrh_name { get; set; }
        public string rh_remarks { get; set; }
        public string created_by { get; set; }
        public string approval_date { get; set; }
        public string bankalertrefundapprl_gid { get; set; }


    }
}