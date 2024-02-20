using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// Deferral Controller Class containing API methods for accessing the  Model class createDeferral
    /// Create defferal, show deferral records, set deferral to loan, show rm details in a table, usercode of employee, export excel of npa, export excel for defferal,
    /// 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class createDeferral : result
    {
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string deferral_name { get; set; }
        public string deferral_gid { get; set; }
        public string deferral_category { get; set; }
        public string due_date { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string criticallity { get; set; }
        public string loanGID{ get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string covenanttype_gid { get; set; }
        public string deferraltype_gid { get; set; }
        public string covenanttype_name { get; set; }
        public string remarks { get; set; }
        public string customerremarks { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmanager_gid { get; set; }
        public string creditmgmt_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string checker_remarks { get; set; }
        public string checker_status { get; set; }
        public List<loan_data> loans { get; set; }
        public string regionalhead_name { get; set; }
        public string regionalhead_GID { get; set; }
    }
    public class loan_data
    {
        public string value { get; set; }
        public string label { get; set; }

    }
    public class loan2Deferral : result
    {
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string deferraltype_gid { get; set; }
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string criticallity { get; set; }
        //public string segment_gid { get; set; }
        public string vertical_code { get; set; }
        public string deferral_name { get; set; }
        public string deferral_gid { get; set; }
        public string deferral_category { get; set; }
        public string due_date { get; set; }
        public string sanctionRefno { get; set; }
        public string sanctionDate { get; set; }
        public string loan_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string covenanttype_gid { get; set; }
        public string covenanttype_name { get; set; }
        public string remarks { get; set; }
        public string customerremarks { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanager_gid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmgmt_name { get; set; }
        public string regionalhead_name { get; set; }
        public string regionalhead_gid { get; set; }
    }
    public class usercode : result
    {
        public string user_code { get; set; }
        public string user_status { get; set; }
    }

        public class deferralSummary : result
    {
        public List<deferralSummaryDtls> deferralSummaryDtls { get; set; }
        public string[] deferral_gid { get; set; }
        public string vertical_gid { get; set; }
        public string branch_gid { get; set; }
        public string entity_gid { get; set; }
        public string relationshipMgmt { get; set; }
        public string zonalHead { get; set; }
        public string businessHead { get; set; }
        public string clustermanager { get; set; }
        public string creditmanager { get; set; }
        public string customer_gid { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string criticallity { get; set; }
        public string aging { get; set; }
        public string Created_by { get; set; }
        public string count_extension { get; set; }
        public string regionalhead_name { get; set; }
        public string state_gid { get; set; }
    }

    public class deferralSummaryDtls : deferralSummary
    {
        public string deferral_gid { get; set; }
        public string  legaltag_flag { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string loan_gid { get; set; }
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string deferral_name { get; set; }
        public string rm_mail { get; set; }
        public string vertical_code { get; set; }
        public string relationshipmgmt_gid { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmgmt_name { get; set; }
        public string zonal_rm { get; set; }
        public string risk_manager { get; set; }
        public string riskmonitoring_head { get; set; }
        public string branch_name { get; set; }
        public string entity_name { get; set; }
        public string count { get; set; }
        public string deferral_catagory { get; set; }
        public DateTime due_date { get; set; }
        public string extended_date { get; set; }
        public string duedate { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string remarks { get; set; }
        public string state { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        //public string loanRefNo { get; set; }
        public string loanTitle { get; set; }
        public string checker_status { get; set; }
        public string deferral_status { get; set; }
        public string approval_status { get; set; }
    }
    public class deferral : result
    {
        public List<deferral_list> deferral_list { get; set; }
    }
    public class deferral_list
    {
        public string deferral_gid { get; set; }
        public string deferraltype_gid { get; set; }
        public string deferral_name { get; set; }
        public string deferral_code { get; set; }
        public string criticallity { get; set; }
        public string comments { get; set; }
    }

  
        public class UploadDocumentname : result
    {
        public string deferral_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanctiondate { get; set; }
        public string record_id { get; set; }
        public string deferral_name { get; set; }
        public string customer_code { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string credit_manager { get; set; }
        public string loanref_no { get; set; }
        public string loan_title { get; set; }
        public string approval_status { get; set; }
        public string def_status { get; set; }
        public string remarks { get; set; }
        public string customer_remarks { get; set; }
        public string criticallity { get; set; }
        public string rm_name { get; set; }
        public string duedate { get; set; }
        public string extended_date { get; set; }
        public string deferral_catagory { get; set; }
        public string vertical_code { get; set; }
        public string entity_name { get; set; }
        public string branch_name { get; set; }
        public DateTime due_date { get; set; }
        public string tracking_type { get; set; }
        public List<UploadDocumentList> filename_list { get; set; }
        public List<DeferralstageList> stage_list { get; set; }
        public List<checker_list> checker_list { get; set; }
        public string regionalhead_name { get; set; }
    }
    public class DeferralstageList
    {
        public string approval_remarks { get; set; }
        public string approval_status { get; set; }
        public string approved_by { get; set; }
        public string extended_date { get; set; }

    }

    public class deferrelmaster:result 
    {
        public string deferral_name { get; set; }
        public string deferral_code { get; set; }
        public string criticallity { get; set; }
        public string comments { get; set; }
    }
    public class UploadDocumentList
    {
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string time { get; set; }
        public string id { get; set; }
        public string deferral_gid { get; set; }
        public string by { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
    }

    public class checker_list
    {
        public string checker_status { get; set; }
        public string checker_remarks { get; set; }
        public string checked_by { get; set; }
    }

    public class mdldeferralgetapproval:result
    {
        public string[] deferral_gid { get; set; }
        public string def_gid { get; set; }
        public string checker_remarks { get; set; }
        public string employee_gid { get; set; }
        public string deferral_status { get; set; }
        public string  due_date { get; set; }
        public string approval_remarks { get; set; }
        public string customer_remarks { get; set; }
        public string applied_remarks { get; set; }
        public string extend_type { get; set; }
    }

    public class mdlcustomer2loan : result
    {
        public List<loan_list> loan  { get; set; }
      
    }
    public class loan_list
    {
        public string label { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }

    }

    public class loandetails : result
    {
        public List<loanlist> loan_details { get; set; }

    }
    public class loanlist
    {
        public string loan_gid { get; set; }
        public string loanTitle { get; set; }
    }

    public class deferraledit : result
    {
        public string deferral_gid { get; set; }
        public string deferralCodeedit { get; set; }
        public string deferralNameedit { get; set; }
        public string criticallity { get; set; }
        public string comments { get; set; }
    }

    public class deferralsupdate : result
    {
        public string deferral_gid { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string checker_status { get; set; }
        //public string loan_refNo { get; set; }
        public string zonalhead_gid { get; set; }
        public string zonalhead_name { get; set; }
        public string businesshead_gid { get; set; }
        public string businesshead_name { get; set; }
        public string clustermgr_gid { get; set; }
        public string clusterhead_name { get; set; }
        public string relationmgr_gid { get; set; }
        public string relationmgr_name { get; set; }
        public string creditmgr_gid { get; set; }
        public string creditmgr_name { get; set; }
        public string category_gid { get; set; }
        public string tracking_type { get; set; }
        public string deferraltype_gid { get; set; }
        public string deferraltype_name { get; set; }
        public string covenanttype_gid { get; set; }
        public string covenanttype_name { get; set; }
        public string duedate { get; set; }
        public string criticallity { get; set; }
        public string remarks { get; set; }
        public string customerremarks { get; set; }
        public string deferral_status { get; set; }
        public string approval_status { get; set; }
        public string checker_remarks { get; set; }
        public string def_gid { get; set; }
        public string approval_remarks { get; set; }
        public string customer_remarks { get; set; }
        public string regional_headname { get; set; }
        public string regionalhead_GID { get; set; }
    }

    public class managedeferralSummary : result
    {
        public List<managedeferralSummaryDtls> managedeferralSummaryDtls { get; set; }
      
    }

    public class managedeferralSummaryDtls : managedeferralSummary
    {
        public string deferral_gid { get; set; }
        public string customername { get; set; }
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string deferral_name { get; set; }
        public string vertical_code { get; set; }
        public string branch_name { get; set; }
        public string entity_name { get; set; }
        public string deferral_catagory { get; set; }
        public DateTime due_date { get; set; }
        public string duedate { get; set; }
        public string extended_date { get; set; }
        public string created_date { get; set; }
        public string Created_by { get; set; }
        public string loanTitle { get; set; }
        public string criticallity { get; set; }
        public string aging { get; set; }
        public string deferral_status { get; set; }
        public string approval_status { get; set; }
        public string checker_status { get; set; }
    }
    public class rmdeferralSummary : result
    {
        public List<rmdeferralSummaryDtls> rmdeferralSummaryDtls { get; set; }

    }

    public class rmdeferralSummaryDtls : rmdeferralSummary
    {
        public string deferral_gid { get; set; }
        public string customername { get; set; }
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string deferral_name { get; set; }
        public string vertical_code { get; set; }
        public string branch_name { get; set; }
        public string entity_name { get; set; }
        public string deferral_catagory { get; set; }
        public DateTime due_date { get; set; }
        public string duedate { get; set; }
        public string extended_date { get; set; }
        public string loanTitle { get; set; }
        public string criticallity { get; set; }
        public string aging { get; set; }
        public string deferral_status { get; set; }
        public string approval_status { get; set; }
    }
    public class user2report : result
    {
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string entity_gid { get; set; }
        public string state_gid { get; set; }
        public string vertical_gid { get; set; }
    }
}