using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.foundation.Models;

namespace ems.foundation.Models
{
   
    public class Mdlcampaigntype : Questionnarieresult
    {
       public List<campaigntype_list> campaigntype_list { get; set; }
       

    }
    public class campaign_type : Mdlcampaigntype
    {
        public string campaigntype_gid { get; set; }
        public string campaigntype_name { get; set; }
    }
    public class Questionnarieresult 
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class MdlMstQuestionnarie : Questionnarieresult

    {
        
        public string categorytype_gid { get; set; }
        public string category_gid { get; set; }
        public string Questionnarie_gid { get; set; }
        public string Questionnarie_name { get; set; }
        public string Campaigntype_gid { get; set; }
        public string Questionnarie_type{ get; set; }
        public string Questionnarie_answer { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string answer_desc { get; set; }
        public string rbo_mandatory { get; set; }
        public List<Questionnarie_list> Questionnarie_list { get; set; }
        public List<inactiveQuestionnarie_list> inactiveQuestionnarie { get; set; }
        public List<campaigntype_list> campaigntype_list { get; set; }
        public string campaigntype_gid { get; set; }
      
    }
    public class Questionnarie_list : Questionnarieresult
    {
        public string Questionnarie_gid { get; set; }
        public string Questionnarie_name { get; set; }
        public string Campaigntype_name { get; set; }
        public string Campaigntype_gid { get; set; }
        public string Questionnarie_type { get; set; }
        public string Questionnarie_answer { get; set; }
        public string mandatory { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string categorytype_name { get; set; }
        public string Questionnarie_code { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string categorytype_gid { get; set; }
        public string campaigntype_gid { get; set; }
    }
    //public class EditQuestionnarie_list
    //{
    //    public string Questionnarie_gid { get; set; }
    //    public string Questionnarie_name { get; set; }
    //    public string questionnarieanswer_gid { get; set; }
    //    public string Campaigntype_name { get; set; }
    //    public string Questionnarie_type { get; set; }
    //    public string mandatory { get; set; }
    //    public string created_date { get; set; }
    //    public string created_by { get; set; }
    //    public string updated_date { get; set; }
    //    public string updated_by { get; set; }
    //    public string remarks { get; set; }
    //    public string Status { get; set; }
    //    public char rbo_status { get; set; }
    //    public string status { get; set; }
    //    public string Questionnarie_code { get; set; }
    //    public string lms_code { get; set; }
    //    public string bureau_code { get; set; }
    //}
    public class inactiveQuestionnarie_list
    {
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class questionnarieanswer_list:Questionnarieresult
    {
        public string questionnarieanswer_gid { get; set; }
        public string questionnarie_type { get; set; }
        public string questionnarie_answer { get; set; }
        public List<questionnarieanswer_list> questionnarieanswerlist { get; set; }
    }
    public class questionnarieanswerlist 
    {
        public List<questionnarieanswer_list> campaigntype_list { get; set; }


    }
}