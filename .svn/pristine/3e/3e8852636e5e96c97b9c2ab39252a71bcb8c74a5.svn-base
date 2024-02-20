using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstCreditApproval
    {
        public List <applicaition_list> applicaition_list { get; set; }
    }
    public class applicaition_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical_name { get; set; }      
        public string overalllimit_amount { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string productcharge_flag { get; set; }
        public string economical_flag { get; set; }
        public string application_status { get; set; }
        public string applicant_type { get; set; }
        public string opsapplication_gid { get; set; }
        public string updated_date { get; set; }
        public string approval_status { get; set; }
        public string creditgroup_gid { get; set; }
        public string region { get; set; }
        public string createdby { get; set; }
        public string initiate_flag { get; set; }
        public string appcreditapproval_gid { get; set; }
        public string creditheadapproval_status { get; set; }
        public string creditheadapproval_date { get; set; }
        public string approval_flag { get; set; }
        public string creditassigned_date { get; set; }
        public string creditassigned_by { get; set; }
        public string submitted_by { get; set; }
        public string submitted_date { get; set; }
        public string underwriting_flag { get; set; }
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string ccmeetingskipcolor_flag { get; set; }
        public string renewal_flag { get; set; }
        public string enhancement_flag { get; set; }
    }
    public class MdlappCreditassign : result
    {      
        public string creditgroup_name { get; set; }
        public string creditmanager_name { get; set; }
        public string regionalcredit_name { get; set; }
        public string nationalcredit_name { get; set; }
        public string credithead_name { get; set; }
        public string remarks { get; set; }
        public string cctocredit_reason { get; set; }
        public string processtype_remarks { get; set; }
    }

    public class mdlcreditquery : result
    {
        public string appcreditapproval_gid { get; set; }
        public string application_gid { get; set; }
        public string querytitle { get; set; }
        public string querydesc { get; set; }
        public string queryraised_to { get; set; }
        public string approval_status { get; set; }
        public string ccmeetingskipcolor_flag { get; set; }
    }
    public class mdlcreditquerydesc : result
    {
        public string querydesc { get; set; }

    }
    public class applcreditapproval : result
    {
        public List<appcreditapprovallist> appcreditapprovallist { get; set; }
        public List<appcreditquerylist> appcreditquerylist { get; set; }
    }
    public class appcreditapprovallist
    {
        public string appcreditapprovallog_gid { get; set; }
        public string appcreditapproval_gid { get; set; }
        public string approval_name { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string approved_date { get; set; }
        public string hierary_level { get; set; }
        public string user_code { get; set; }
        public string rejected_date { get; set; }
        public string hold_date { get; set; }
    }
    public class appcreditquerylist
    {
        public string appcreditquery_gid { get; set; }
        public string appcreditapproval_gid { get; set; }
        public string application_gid { get; set; }
        public string querytitle { get; set; }
        public string querydesc { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string querystatus { get; set; }
        public string hierary_level { get; set; }
        public string close_remarks { get; set; }
        public string query_to { get; set; }
        public string rmquery_flag { get; set; }
        public string application_no { get; set; }
    }
    public class mdlappcreditapproval : result
    {
        public string appcreditapproval_gid { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string application_gid { get; set; }
        public string created_by { get; set; }
    }
   
    public class mdlquerystatus : result
    {
        public string querystatus_flag { get; set; }
        public string approved_flag { get; set; }
        public string submitapproval_flag { get; set; }
        public string appcreditapproval_gid { get; set; }
    }
    public class credtiApplicationCount : result
    {
        public string upcomingcreditapplication_count { get; set; }
        public string newcreditapplication_count { get; set; }
        public string submitted2ccapp_count { get; set; }
        public string ccapproved_count { get; set; }
        public Int16 rejectholdapplication_count { get; set; }
        public Int16 lstotalcount { get; set; }
        public string approvedapplication_count { get; set; }
        public List<rejectholdlist> rejectholdlist { get; set; }
    }
    public class rejectholdlist
    {
        public string reject_counts { get; set; }
    }

    public class pslcsacomplete : result
    {
        public string employee_gid { get; set; }
        public string application_gid { get; set; }
        public string pslcompleted_flag { get; set; }
        public string pslupdated_by { get; set; }
        public string pslupdated_date { get; set; }
        public string pslcompleteremarks { get; set; }

    }
    public class MdlappcreditManagerreject : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string appcreditapproval_gid { get; set; }
        //public string created_by { get; set; }
        public string rejectstatus_flag { get; set; }
        
    }

    public class Applcreditrejected : result
    {
        public List<Getappcreditrejectedlist> Getappcreditrejectedlist { get; set; }
        //public List<appcreditrejectedlist> appcreditrejectedlist { get; set; }
    }
    public class Getappcreditrejectedlist
    {
        public string creditmanagerrejectlog_gid { get; set; }
        public string rejected_by { get; set; }
        public string rejected_status { get; set; }
        public string rejected_remarks { get; set; }
        public string rejected_date { get; set; }
        public string rejectstatus_flag { get; set; }

    }
   
}