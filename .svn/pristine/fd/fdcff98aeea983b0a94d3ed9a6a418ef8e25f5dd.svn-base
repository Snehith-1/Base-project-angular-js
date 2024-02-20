using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values for Revoking options and Hierarchy changes for the Samagro flow records at various stages (Application creation, Business Approval, View, Delete, Upload, Download and Approvals) in Admin Page.
    /// </summary>
    /// <remarks>Written by L.Sundar Rajan </remarks>

    public class MdlRejectedAppl : result
    {
        public List<rejectedappl_list> rejectedappl_list { get; set; }
    }

    public class rejectedappl_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string history_flag { get; set; }

        public string created_gid { get; set; }
    }

    public class MdlHoldAppl : result
    {
        public List<holdappl_list> holdappl_list { get; set; }
    }
    public class MdlRevokedAppl : result
    {
        public List<revokedappl_list> revokedappl_list { get; set; }
        public List<pendingapplhis_list> pendingapplhis_list { get; set; }
        public List<businesshistory_list> businesshistory_list { get; set; }
        public List<holdapplhis_list> holdapplhis_list { get; set; }
        public string businessrejectrevokelog_gid { get; set; }
        public string business_remarks { get; set; }
        public string reason { get; set; }
        public string application_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string applicationapproval_gid { get; set; }
        public string rejecthold_remarks { get; set; }
    }

    public class holdappl_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string history_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class mdlrejectrevoke : result
    {
        public string applicationapproval_gid { get; set; }
        public string hierary_level { get; set; }
        public string hierarylevel_count { get; set; }
        public string approved_date { get; set; }
        public string application_gid { get; set; }
        public string reason { get; set; }
        public string headapproval_status { get; set; }
        public string headapproval_date { get; set; }
        public string approval_status { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string hold_by { get; set; }
        public string hold_date { get; set; }
        public string business_remarks { get; set; }
        public string approval_remarks { get; set; }
    }
    public class revokedappl_list : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class pendingapplhis_list : result
    {
        public string application_gid { get; set; }
        public string rejecttedhold_by { get; set; }
        public string rejecttedhold_date { get; set; }
        public string approval_remarks { get; set; }
        public string hierary_level { get; set; }
        public string approval_status { get; set; }
        public string applicationapproval_gid { get; set; }
    }
    public class businesshistory_list : result
    {
        public string application_gid { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string hold_by { get; set; }
        public string hold_date { get; set; }
        public string business_remarks { get; set; }
        public string revoked_by { get; set; }
        public string revoked_date { get; set; }
        public string revoked_remarks { get; set; }
        public string businessapproval_status { get; set; }
        public string businessrejectrevokelog_gid { get; set; }
    }
    public class holdapplhis_list : result
    {
        public string application_gid { get; set; }
        public string hold_by { get; set; }
        public string hold_date { get; set; }
        public string remarks { get; set; }
    }
    public class RevokeApplicationCount : result
    {
        public string businessreject_count { get; set; }
        public string businesshold_count { get; set; }
        public string businessrevoked_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class MdlCreditRejectedAppl : result
    {
        public List<creditrejectedappl_list> creditrejectedappl_list { get; set; }
        public List<creditmanagerrejectedappl_list> creditmanagerrejectedappl_list { get; set; }
    }

    public class creditmanagerrejectedappl_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string history_flag { get; set; }
    }

    public class creditrejectedappl_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string history_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class MdlCreditHoldAppl : result
    {
        public List<creditholdappl_list> creditholdappl_list { get; set; }
    }
    public class creditholdappl_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string history_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class CreditRevokeApplicationCount : result
    {
        public string creditreject_count { get; set; }
        public string credithold_count { get; set; }
        public string creditrevoked_count { get; set; }
        public string creditmanagerreject_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class Mdlcreditrevoke : result
    {
        public string appcreditapproval_gid { get; set; }
        public string hierary_level { get; set; }
        public string hierarylevel_count { get; set; }
        public string approved_date { get; set; }
        public string application_gid { get; set; }
        public string reason { get; set; }
        public string headapproval_status { get; set; }
        public string headapproval_date { get; set; }
        public string approval_status { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string hold_by { get; set; }
        public string hold_date { get; set; }
        public string credit_remarks { get; set; }
        public string approval_remarks { get; set; }
        public string productmember_approvalflag { get; set; }
        public string productmanager_approvalflag { get; set; }
        public string appproductapproval_gid { get; set; }
}
    public class MdlCreditRevokedAppl : result
    {
        public List<creditrevokedappl_list> creditrevokedappl_list { get; set; }
    }
    public class creditrevokedappl_list : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlCreditPendingHistoryAppl : result
    {
        public List<creditpendingapplhis_list> creditpendingapplhis_list { get; set; }
        public string appcreditapproval_gid { get; set; }
        public string creditrejecthold_remarks { get; set; }
        public string application_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string appproductapproval_gid { get; set; }
        public string productrejecthold_remarks { get; set; }
        public string productmanager_approvalflag { get; set; }
        public string productmanager_approvalremarks { get; set; }
        public string productmember_approvalflag { get; set; }
        public string assign_remarks { get; set; }
    }

    public class MdlProductPendingHistoryAppl : result
    {
        public List<productpendingapplhis_list> productpendingapplhis_list { get; set; }
        public string appproductapproval_gid { get; set; }
        public string productrejecthold_remarks { get; set; }
        public string application_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class productpendingapplhis_list : result
    {
        public string application_gid { get; set; }
        public string product_managername { get; set; }
        public string productmanager_approvaldate { get; set; }
        public string productmanager_approvalflag { get; set; }
        public string product_membername { get; set; }
        public string productmember_approvaldate { get; set; }
        public string assign_remarks { get; set; }
        public string appproductapproval_gid { get; set; }
        public string productmember_approvalflag { get; set; }
        public string productmanager_approvalremarks { get; set; }
    }
    public class creditpendingapplhis_list : result
    {
        public string application_gid { get; set; }
        public string rejecttedhold_by { get; set; }
        public string rejecttedhold_date { get; set; }
        public string approval_remarks { get; set; }
        public string hierary_level { get; set; }
        public string approval_status { get; set; }
        public string appcreditapproval_gid { get; set; }
    }
    public class MdlCreditHistoryLog : result
    {
        public List<credithistorylog_list> credithistorylog_list { get; set; }
    }
    public class credithistorylog_list : result
    {
        public string application_gid { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string hold_by { get; set; }
        public string hold_date { get; set; }
        public string credit_remarks { get; set; }
        public string revoked_by { get; set; }
        public string revoked_date { get; set; }
        public string revoked_remarks { get; set; }
        public string creditapproval_status { get; set; }
        public string creditrevokelog_gid { get; set; }
    }
    public class MdlCreditHistoryApplLog : result
    {
        public string creditrevokelog_gid { get; set; }
        public string credit_remarks { get; set; }
        public string reason { get; set; }
        public string application_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string productrevokelog_gid { get; set; }
        public string product_remarks { get; set; }
    }
    public class MdlBusinessHierarchyUpdate : result
    {
        public List<businessstage_list> businessstage_list { get; set; }
        public List<creditstage_list> creditstage_list { get; set; }
        public List<ccstage_list> ccstage_list { get; set; }
        public List<cadpendingstage_list> cadpendingstage_list { get; set; }
        public List<cadacceptedstage_list> cadacceptedstage_list { get; set; }
        public List<incompletestage_list> incompletestage_list { get; set; }
    }

    public class businessstage_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string hierarchyupdated_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class creditstage_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string hierarchyupdated_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class ccstage_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string hierarchyupdated_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class cadpendingstage_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string hierarchyupdated_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class cadacceptedstage_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string hierarchyupdated_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class MdlHierarchyUpdateApplCount : result
    {
        public string businessstage_count { get; set; }
        public string creditstage_count { get; set; }
        public string ccstage_count { get; set; }
        public string cadpendingstage_count { get; set; }
        public string cadacceptedstage_count { get; set; }
        public string incompletestage_count { get; set; }
        public string productstage_count { get; set; }
        public Int16 lstotalcount { get; set; }
    }
    public class Mdlapplicationdetials : result
    {
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string rm_name { get; set; }
    }
    public class MdlAdminRMMappingview : result
    {
        public string vertical_gid { get; set; }
        public string clusterhead { get; set; }
        public string regionhead { get; set; }
        public string zonalhead { get; set; }
        public string businesshead { get; set; }
        public string program_name { get; set; }
        public string level_zero { get; set; }
        public string level_one { get; set; }
        public string employee_count { get; set; }
        public string employee_gid { get; set; }
        public string program_gid { get; set; }
        public bool hierarchyavailable_status { get; set; }
    }
    public class MdlBusinessHierarchyUpdateDtl : result
    {
        public string application_gid { get; set; }
        public string approval_status { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string relationshipmanager_name { get; set; }
        public string drm_gid { get; set; }
        public string drm_name { get; set; }
        public string clustermanager_gid { get; set; }
        public string clustermanager_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string zonalhead_name { get; set; }
        public string regionalhead_gid { get; set; }
        public string regionalhead_name { get; set; }
        public string businesshead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string employee_gid { get; set; }
        public string updatedclusterhead_gid { get; set; }
        public string updatedclusterhead_name { get; set; }
        public string updatedregionhead_gid { get; set; }
        public string updatedregionhead_name { get; set; }
        public string updatedzohalhead_gid { get; set; }
        public string updatedzonalhead_name { get; set; }
        public string updatedbusinesshead_gid { get; set; }
        public string updatedbusinesshead_name { get; set; }
        public string updatedrelationshipmanager_gid { get; set; }
        public string updatedrelationshipmanager_name { get; set; }
        public string updateddrm_gid { get; set; }
        public string updateddrm_name { get; set; }
        public string updatedvertical_gid { get; set; }
        public string updatedvertical_name { get; set; }
        public string updatedprogram_gid { get; set; }
        public string updatedprogram_name { get; set; }
        public string application_stage { get; set; }
        public string hierarchyupdate_remarks { get; set; }
        public string hierary_level { get; set; }
        public string hierarylevel_count { get; set; }
        public string applicationapproval_gid { get; set; }
        public string submitvertical_gid { get; set; }
        public string submitprogram_gid { get; set; }
        public string submitprogram_name { get; set; }
        public string submitvertical_name { get; set; }
    }
    public class MdlHierarchyUpdateHistoryLog : result
    {
        public List<hierarchyupdatedhistory_list> hierarchyupdatedhistory_list { get; set; }
    }

    public class hierarchyupdatedhistory_list
    {
        public string businesshierarchyupdatelog_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string relationshipmanager_gid { get; set; }
        public string relationshipmanager_name { get; set; }
        public string drm_gid { get; set; }
        public string drm_name { get; set; }
        public string clustermanager_gid { get; set; }
        public string clustermanager_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string zonalhead_name { get; set; }
        public string regionalhead_gid { get; set; }
        public string regionalhead_name { get; set; }
        public string businesshead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string employee_gid { get; set; }
        public string updatedclustermanager_gid { get; set; }
        public string updatedclustermanager_name { get; set; }
        public string updatedregionalhead_gid { get; set; }
        public string updatedregionalhead_name { get; set; }
        public string updatedzonalhead_gid { get; set; }
        public string updatedzonalhead_name { get; set; }
        public string updatedbusinesshead_gid { get; set; }
        public string updatedbusinesshead_name { get; set; }
        public string updatedrelationshipmanager_gid { get; set; }
        public string updatedrelationshipmanager_name { get; set; }
        public string updateddrm_gid { get; set; }
        public string updateddrm_name { get; set; }
        public string updatedvertical_gid { get; set; }
        public string updatedvertical_name { get; set; }
        public string updatedprogram_gid { get; set; }
        public string updatedprogram_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class incompletestage_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string vertical_name { get; set; }
        public string approval_status { get; set; }
        public string applicant_type { get; set; }
        public string region { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string submitted_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string hierarchyupdated_flag { get; set; }
        public string created_gid { get; set; }
    }
    public class MdlHierarchyUpdateRemarks : result
    {
        public string businesshierarchyupdatelog_gid { get; set; }
        public string hierarchyupdate_remarks { get; set; }
    }
}