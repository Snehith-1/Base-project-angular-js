using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.its.Models
{
    //  My Approval ............//

    public class myapproval : result
    {
        public List<departmentapproval_list> departmentapproval_list { get; set; }
        public List<serviceapproval_list> serviceapproval_list { get; set; }
        public List<managerapproval_list> managerapproval_list { get; set; }
        public List<approvalhistory_list> approvalhistory_list { get; set; }
        public List<dependencyapproval_list> dependencyapproval_list { get; set; }
        public List<cabapproval_list> cabapproval_list { get; set; }
        public List<myapprovalcount> myapprovalcount { get; set; }
        public List<cachistory_list> cachistory_list { get; set; }
        public List<dependencyhistory_list> dependencyhistory_list { get; set; }
    }

    public class departmentapproval_list
    {
        public string complaint_gid { get; set; }
        public string category_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
        public string lsinternalapproval { get; set; }
    }
    public class serviceapproval_list
    {
        public string complaint_gid { get; set; }
        public string category_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
        public string lsinternalapproval { get; set; }
    }

    public class managerapproval_list
    {
        public string complaint_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
    }

    public class  approvalhistory_list
    {
        public string complaint_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
    }

    public class dependencyapproval_list
    {
        public string ref_no { get; set; }
        public string release_gid { get; set; }
        public string issue_releasecount { get; set; }
        public string application { get; set; }
        public string done_by { get; set; }
        public string vendor_name { get; set; }
        public string release_status { get; set; }
        public string release_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string release_remarks { get; set; }
        public string created_date { get; set; }
    }

    public class dependencyhistory_list
    {
        public string ref_no { get; set; }
        public string release_gid { get; set; }
        public string issue_releasecount { get; set; }
        public string application { get; set; }
        public string done_by { get; set; }
        public string vendor_name { get; set; }
        public string release_status { get; set; }
        public string release_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string release_remarks { get; set; }
        public string created_date { get; set; }
    }

    public class cabapproval_list
    {
        public string ref_no { get; set; }
        public string release_gid { get; set; }
        public string issue_releasecount { get; set; }
        public string application { get; set; }
        public string done_by { get; set; }
        public string vendor_name { get; set; }
        public string release_status { get; set; }
        public string release_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string release_remarks { get; set; }
        public string approval_remarks { get; set; }

        public string created_date { get; set; }
    }

    public class cachistory_list
    {
        public string ref_no { get; set; }
        public string release_gid { get; set; }
        public string issue_releasecount { get; set; }
        public string application { get; set; }
        public string done_by { get; set; }
        public string vendor_name { get; set; }
        public string release_status { get; set; }
        public string release_date { get; set; }
        public string created_by { get; set; }
        public string approval_status { get; set; }
        public string release_remarks { get; set; }
        public string created_date { get; set; }
    }

    public class myapprovalcount
    {
        public string departmentapprovalcount { get; set; }
        public string serviceapprovalcount { get; set; }
        public string managementapprovalcount { get; set; }
        public string overallservice_approval { get; set; }
        public string dependencyapprovalcount { get; set; }
        public string cabapprovalcount { get; set; }
        public string overallchangemana_approval { get; set; }
        public string task_approval { get; set; }

    }

    // Department - Approve & Reject & Internal Approval ...//

    public class departmentapproved : result
    {
        public string category_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string lsinternalapproval { get; set; }
        public string remarks { get; set; }
    }

    public class departmentreject : result
    {
        public string serviceapproval_gid { get; set; }
        public string remarks { get; set; }
    }

    public class departmentinternal : result
    {
        public string complaint_gid { get; set; }
        public string serviceapproval_gid { get; set; }
    }

    // Service - Approve & Reject & Internal Approval ........//
    public class serviceapprove : result
    {
        public string serviceapproval_gid { get; set; }
        public string category_gid { get; set; }
        public string complaint_gid { get; set; }
        public string task_gid { get; set; }
        public string remarks { get; set; }
    }

    public class servicereject : result
    {
        public string serviceapproval_gid { get; set; }
        public string remarks { get; set; }
    }

    public class serviceinternal : result
    {
        public string serviceapproval_gid { get; set; }
    }

    // Management - Approve & Reject .......//

    public class managementapprove : result
    {
        public string serviceapproval_gid { get; set; }
        public string remarks { get; set; }
    }

    public class managementreject : result
    {
        public string serviceapproval_gid { get; set; }
        public string remarks { get; set; }
    }

    // View Department Ticket Details......//

    public class viewdepartment : result
    {
        public string complaint_gid { get; set; }
        public string category_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
        public string department_approval { get; set; }
        public string department_approveddate { get; set; }
        public string department_approvedstatus { get; set; }
        public string management_approval { get; set; }
        public string management_approveddate { get; set; }
        public string management_approvedstatus { get; set; }
        public string service_approval { get; set; }
        public string service_approveddate { get; set; }
        public string service_approvedstatus { get; set; }
        public string departmentrejected_remarks { get; set; }
        public string servicerejected_remarks { get; set; }
        public string managementrejected_remarks { get; set; }
    }

    // View Service Ticket Details......//

    public class viewservice : result
    {
        public string complaint_gid { get; set; }
        public string category_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
        public string department_approval { get; set; }
        public string department_approveddate { get; set; }
        public string department_approvedstatus { get; set; }
        public string management_approval { get; set; }
        public string management_approveddate { get; set; }
        public string management_approvedstatus { get; set; }
        public string service_approval { get; set; }
        public string service_approveddate { get; set; }
        public string service_approvedstatus { get; set; }
        public string departmentrejected_remarks { get; set; }
        public string servicerejected_remarks { get; set; }
        public string managementrejected_remarks { get; set; }
    }

    // View Management Ticket Details......//

    public class viewmanagement : result
    {
        public string complaint_gid { get; set; }
        public string serviceapproval_gid { get; set; }
        public string complaint_refno { get; set; }
        public string complaint_date { get; set; }
        public string complaint_title { get; set; }
        public string complaint_remarks { get; set; }
        public string raisedfor_employee { get; set; }
        public string category_name { get; set; }
        public string approvalstatus { get; set; }
        public string department_approval { get; set; }
        public string department_approveddate { get; set; }
        public string department_approvedstatus { get; set; }
        public string management_approval { get; set; }
        public string management_approveddate { get; set; }
        public string management_approvedstatus { get; set; }
        public string service_approval { get; set; }
        public string service_approveddate { get; set; }
        public string service_approvedstatus { get; set; }
        public string departmentrejected_remarks { get; set; }
        public string servicerejected_remarks { get; set; }
        public string managementrejected_remarks { get; set; }
    }

    // Dependency Approve...//

    public class dependencyapprove : result
    {
        public string dependentapproval_gid { get; set; }
        public string release_gid { get; set; }
        public string issuetracker_gid { get; set; }
    }

    // Dependency Reject...//

    public class dependencyreject : result
    {
        public string dependentapproval_gid { get; set; }
        public string release_gid { get; set; }
        public string issuetracker_gid { get; set; }
    }

    // CAB Approve...//

    public class cabapprove : result
    {
        public string cacapproval_gid { get; set; }
        public string release_gid { get; set; }
        public string approval_remarks { get; set; }
        public string application { get; set; }


    }

    // CAB Reject...//

    public class cabreject : result
    {
        public string cacapproval_gid { get; set; }
        public string reject_remarks { get; set; }
    }

    // Release Details...//

    public class releaseapprovaldetails : result
    {
        public string release_gid { get; set; }
        public string applicationname { get; set; }
        public string vendorname { get; set; }
        public string approval_status { get; set; }
        public string release_notes { get; set; }
        public string release_remarks { get; set; }
        public string ref_no { get; set; }
        public string released_by { get; set; }
        public string released_on { get; set; }
        public string release_status { get; set; }
        public string release_date { get; set; }
        public string issuerelease_count { get; set; }
        public string dependency_count { get; set; }
        public string cab_count { get; set; }
        public string change_description { get; set; }
        public string impacted_module { get; set; }
        public string impacted_system { get; set; }
        public string reason_change { get; set; }
        public string alternative_way { get; set; }
        public string resources { get; set; }
        public List<releaseissue_list> releaseissue_list;
        public List<dependency_list> dependency_list;
        public List<cab_list> cab_list;
        public List<uatdocument_list> uatdocument_list;
        public List<MdlApprovalDocuments> ApprovalDocuments_List;

    }

    public class releaseissue_list : result
    {
        public string issuetracker_gid { get; set; }
        public string issue_refno { get; set; }
        public string issue_status { get; set; }
        public string issue_date { get; set; }
        public string issue_type { get; set; }
        public string issue_title { get; set; }
        public string issue_remarks { get; set; }
        public string Severity { get; set; }
        public string priority { get; set; }
    }
    public class uatlog : result
    {
        public List<uatlog_list> uatlog_list { get; set; }
    }
    public class uatlog_list
    {
        public string issuetracker_gid { get; set; }
        public string issuestatuslog_gid { get; set; }
        public string remarks { get; set; }
        public string issue_status { get; set; }
        public string uat_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class uatdocument_list
    {
        public string uatdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
    }

    public class dependency_list
    {
        public string dependentapproval_gid { get; set; }
        public string dependencyapprovalpending_gid { get; set; }
        public string applicationdependent { get; set; }
        public string dependency_status { get; set; }
        public string stakeholder_name { get; set; }
        public string application_name { get; set; }
        public string approval { get; set; }
        public string approved_date { get; set; }
        public string approval_gid { get; set; }
        public string dependency_approval { get; set; }
    }

    public class cab_list
    {
        public string cacapproval_gid { get; set; }
        public string cacapprovalpending_gid { get; set; }
        public string approval_member { get; set; }
        public string approval_status { get; set; }
        public string department_name { get; set; }
        public string approval_date { get; set; }
        public string approval_requested { get; set; }
        public string approval_name { get; set; }
        public string cab_approval { get; set; }
        public string approval_remarks { get; set; }
    }


    
    public class MdlApprovalDocuments
    {
        public string document_gid { get; set; }
        public string release_gid { get; set; }
        public string documentref_no { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
}
