using System;
using System.Collections.Generic;

namespace ems.foundation.Models
{
    public class MdlTrnCampaign : result
        {
        
       
        public string questionnarie_gid { get; set; }
        public string category_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
        public string display_order { get; set; }
        public string campaigndtl_gid { get; set; }
        public string campagin_code { get; set; }
        public string campagin_name { get; set; }   
        public string campaign_gid { get; set; }
        public string questionnarie_status { get; set; }
        public string campaigntype_name { get; set; }
        public string campaigntype_gid { get; set; }
        public string campaign_code { get; set; }
        public string contact_name { get; set; }
        public string contact_mobile { get; set; }
        public string contact_email { get; set; }
        public string campaign_approver { get; set; }
        public string campaignpending_count { get; set; }
        public string rejected_count { get; set; }
        public string approved_count { get; set; }
        public string closed_count { get; set; }    
        public string os_assesment_date { get; set; }
        public DateTime editstart_date { get; set; }
        public DateTime editend_date { get; set; }
        public DateTime editassesment_date { get; set; }
        public string campaignapprovalpending_count { get; set; }
        public string approvalapproved_count { get; set; }
        public string approvalrejected_count { get; set; }
        public string customerpending_count { get; set; }
        public string customerrejected_count { get; set; }
        public string customerapproved_count { get; set; }
        public string customerpendingview_count { get; set; }
        public string customerrejectedview_count { get; set; }
        public string customerapprovedview_count { get; set; }
        public List<campaign_details> campaign_details { get; set; }
        public List<multi_campaign_details> multi_campaign_details { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string openquerycount { get; set; }
        public string created_by { get; set; }
        public string lsFlag { get; set; }

        public string form_type { get; set; }
        public List<questionnarie_list> questionnarie_list { get; set; }
        public List<multiplequestionnarie_list> multiplequestionnarie_list { get; set; }      
        public List<campaigntype_list> campaigntype_list { get; set; }
        public List<customer_list> customer_list { get; set; }
        public List<singleform_list> singleform_list { get; set; }
        public List<multipleform_list> multipleform_list { get; set; }
        public List<campaign_list> campaign_list { get; set; }
        public List<employees> employees { get; set; }
        public List<assignee> assignee { get; set; }
        public List<assigneelist> assigneelist { get; set; }  
        public string customer_gid { get; set; }
        public string email_address { get; set; }
        public string mobile_no { get; set; }
        public string employee_gid { get; set; }
        public string manager_name { get; set; }
        public List<category_list> category_list { get; set; }
        public List<campiagnraisequery_list> campiagnraisequery_list { get; set; }
        public List<campaignsingleform_list> campaignsingleform_list { get; set; }

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
        public string reference_gid { get; set; }
        public string campaignapproval_remarks { get; set; }
        public string campaignraisequery_gid { get; set; }
        public string queryresponse_remarks { get; set; }
        public string campaign_status { get; set; }
    }
    //public class employee
    //{
    //    public string customerapproving2employee_gid { get; set; }
    //    public string employee_gid { get; set; }
    //    public string employee_name { get; set; }
    //}

    //public class employeeem_list
    //{
    //    public string employee_gid { get; set; }
    //    public string employee_name { get; set; }
    //}
    public class assignee
    {
        public string campaignapproving2employee_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class employees 
    {
        public string campaignapproving2employee_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class answerdesc_list
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool check { get; set; }
    }
    public class campaign_details
    {
        public string campaign_gid { get; set; }
        public string campaigndtl_gid { get; set; }
        public string questionnarie_gid { get; set; }
        public string answer_type { get; set; }
        public string answer_desc { get; set; }
        public string question { get; set; }
        public string campaign_name { get; set; }
        public string singleform_answer { get; set; }
        public List<answerdesc_list> answerdesc_list { get; set; }

        
        public string campaignrefno { get; set; }
        public string campaign_type { get; set; }
        public string customer_name { get; set; }
        public string contactperson_fn { get; set; }
        public string contactperson_mobile { get; set; }
        public string contactperson_email { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string assesment_date { get; set; }
        public string importance { get; set; }
        public string campaign_status { get; set; }



        public string mycampaignsingle_gid { get; set; }
       
        public string mycampaignmultiple_gid { get; set; }
        

    }
    public class multi_answerdesc_list
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class multi_campaign_details
    {
        public string campaign_gid { get; set; }
        public string campaigndtl_gid { get; set; }
        public string questionnarie_gid { get; set; }
        public string answer_type { get; set; }
        public string answer_desc { get; set; }
        public string question { get; set; }
        public string campaign_name { get; set; }
        public List<multi_answerdesc_list> multi_answerdesc_list { get; set; }
        public string importance { get; set; }

        public string campaignrefno { get; set; }
        public string campaign_type { get; set; }
        public string customer_name { get; set; }
        public string contactperson_fn { get; set; }
        public string contactperson_mobile { get; set; }
        public string contactperson_email { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string assesment_date { get; set; }

        public string multipleform_answer { get; set; }
        public string mycampaignmultiple_gid { get; set; }




    }

    public class assigneelist
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class campaign_list : result
    {
        public string campaign_gid { get; set; }
        public string campaign_code { get; set; }
        public string campaign_name { get; set; }
        public string Status { get; set; }
        public string campaign_status { get; set; }
        public string status_flag { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string campaigntype_name { get; set; }
      


    }
    public class singleform_list : result
    {
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
        public string questionnarie_category { get; set; }
        public string categorytype_gid { get; set; }


    }
    public class campaignsingleform_list : result
    {
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
        public string questionnarie_category { get; set; }


    }
    public class multipleform_list : result
        {
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
      
    }
    public class category_list : result
    {
        public string categorytype_gid { get; set; }
        public string categorytype_name { get; set; }
       
    }
    public class questionnarie_list : result
    {
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
    }
    public class multiplequestionnarie_list : result
    {
        public string questionnarie_gid { get; set; }
        public string questionnarie_name { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
    }

    public class campiagnraisequery_list : result
    {
        public string campaign_gid { get; set; }
        public string campaignraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string raisequery_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string queryresponse_remarks { get; set; }
        public string query_responseby { get; set; }
    }

}