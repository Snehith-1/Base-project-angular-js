using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.foundation.Models
{
    public class MdlFndTrnMyCampaignSummary : result
    {
        public string questionnarie_gid { get; set; }
        public string category_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
        public string display_order { get; set; }
        public string file_path { get; set; }
        public string campaigndtl_gid { get; set; }
        public string campaign_gid { get; set; }
        public string questionnarie_status { get; set; }
        public string form_type { get; set; }
        public string importance { get; set; }
        public string mycampaignpending_count { get; set; }
        public string campaignapproved_count { get; set; }
        public string campaignrejected_count { get; set; }
        public string campaignspending_count { get; set; }
        public string campaignsapproved_count { get; set; }
        public List<campaigntype_list> campaigntype_list { get; set; }
        //public List<customer_list> customer_list { get; set; }
        public List<singleform_list> singleform_list { get; set; }
        public List<multipleform_list> multipleform_list { get; set; }
        public List<singleformanswer_list> singleformanswer_list { get; set; }
        public List<multipleformanswer_list> multipleformanswer_list { get; set; }
        public List<multipleformTeamanswer_list> multipleformTeamanswer_list { get; set; }
        public string customer_gid { get; set; }
        public string employee_name { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
        public List<mycampaign_list> mycampaign_list { get; set; }
        public List<mycampiagnraisequery_list> mycampiagnraisequery_list { get; set; }
        public List<campaignmanager_list> campaignmanager_list { get; set; }
        public string mycampaignsingle_gid { get; set; }
        public string mycampaignmultiple_gid { get; set; }
        public List<employee_list> employee_list { get; set; }

        public string manager_gid { get; set; }
        public int Years;
        public int Months;
        public int Days;

        public string campaign_name { get; set; }
        public string campaignrefno { get; set; }
        public string campaign_type { get; set; }
        public string customer_name { get; set; }
        public string contactperson_fn { get; set; }
        public string contactperson_mobile { get; set; }
        public string contactperson_email { get; set; }
        public string campaign_mgr { get; set; }
        public string campaign_apr { get; set; }
        public string campaign_cost { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string assesment_date { get; set; }
        public string osAssesment_date { get; set; }
        public string loan_availed { get; set; }
        public string query_title { get; set; }
        public string mycampaignapproval_remarks { get; set; }
        public string campaignapproval_remarks { get; set; }
        public string lsFlag { get; set; }
        public string mycampaignraisequery_gid { get; set; }
        public string queryresponse_remarks { get; set; }
        public string query_description { get; set; }
        public string manager_name { get; set; }
        public string openquerycount { get; set; }
    }
    public class singleformanswer_list
    {
        public string quest_Id { get; set; }
        public string quest_name { get; set; }
        public string quest_type { get; set; }
        public string quest_answer { get; set; }
        public string mycampaignsingle_gid { get; set; }


    }
    public class campaignmanager_list
    {
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
      
    }
    public class employee_list
    {
        public string manager2employee_gid { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
    }

    public class multipleformanswer_list
    {
        public string quest_Id { get; set; }
        public string reference_Id { get; set; }
        public string quest_name { get; set; }
        public string quest_type { get; set; }
        public string quest_answer { get; set; }
        public string mycampaignmultiple_gid { get; set; }
        public string created_by { get; set; }


    }
    public class multipleformTeamanswer_list
    {
        public string quest_Id { get; set; }
        public string reference_Id { get; set; }
        public string quest_name { get; set; }
        public string quest_type { get; set; }
        public string quest_answer { get; set; }
        public string mycampaignmultiple_gid { get; set; }
        public string created_by { get; set; }


    }
    public class SingleMultiFormReport : result
    {       
        public string lspath { get; set; }
        public string lscloudpath { get; set; }
        public string lsname { get; set; }
    }

    public class mycampaign_list
    {
        public string campaign_gid { get; set; }
        public string campaign_code { get; set; }
        public string campaign_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string assesment_date { get; set; }
        public string campaign_approver { get; set; }
        public string campaign_manager { get; set; }
        public string campaign_status { get; set; }
        public string status_flag { get; set; }
        public string status { get; set; }
        public string campaigntype_name  { get; set; }
        public string mycampaignmultiple_gid { get; set; }
    }
    public class mycampiagnraisequery_list : result
    {
        public string campaign_gid { get; set; }
        public string mycampaignraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string raisequery_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string queryresponse_remarks { get; set; }
        public string query_responseby { get; set; }
        public string campaignmanager2employee_gid { get; set; }
        public string manager_gid { get; set; }
        public string manager_name { get; set; }
    }



    public class sampledynamicdatadtl : result
    {
        public string JSONdata { get; set; }
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }
        public List<QuestionnarieID_list> QuestionnarieID_list { get; set; }
    }
    public class QuestionnarieID_list
    {
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }      


    }
}