using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rms.Models
{
    public class MdlRmsTrnMyRecruitment:result
    {
        public string recruitmentadd_gid { get; set; }
        public string jobposition_gid { get; set; }
        public string candidate_name { get; set; }
        public string candidate_mobileno { get; set; }
        public string candidateemail_address1 { get; set; }
        public string candidateemail_address2 { get; set; }
        public string candidate_gender { get; set; }
        public string candidate_dob { get; set; }
        public string candidate_location { get; set; }
        public string current_company { get; set; }
        public string current_ctc { get; set; }
        public string designation { get; set; }
        public string education { get; set; }
        public string candidate_status { get; set; }
        public string candidate_remarks { get; set; }
        public string experience { get; set; }
        public List<myrecruitmentsummary_list> myrecruitmentsummary_list { get; set; }
        public List<candidate_list> candidate_list { get; set; }
        public List<jobcode_list> jobcode_list { get; set; }
    }
    public class myrecruitmentsummary_list
    {
        public string created_date { get; set; }
        public string company_name { get; set; }
        public string job_code { get; set; }
        public string job_title { get; set; }
        public string noof_position { get; set; }
        public string candidate_fetched { get; set; }
        public string position_pending { get; set; }
        public string position_closed { get; set; }
        public string spoc_name { get; set; }
        public string businessunit_name { get; set; }
        public string jobcodefreeze_flag { get; set; }
       public string jobposition_gid { get; set; }
    }
    public class MdlJobinfo:result
    {
        public string job_code { get; set; }
      
        public string job_title { get; set; }
        public string noof_position { get; set; }
        public string spoc_name { get; set; }
        public string experience { get; set; }
        public string ctc_budget { get; set; }
        public string job_location { get; set; }
        public string skills { get; set; }
        public string qualification { get; set; }
        public string job_description { get; set; }
        public string notice_period { get; set; }
    }
    public class mdlcandidateinfo : result
    {
        public string firstname { get; set; }
        public string jobposition_gid { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string mobile_no { get; set; }
        public string emailaddress1 { get; set; }
        public string emailaddress2 { get; set; }
        public string candidate_status { get; set; }
        public string dob { get; set; }
        public string qualification { get; set; }
        public string currentcompany { get; set; }
        public string current_location { get; set; }
        public string current_designation { get; set; }
        public string current_ctc { get; set; }
        public string experience { get; set; }
        public string remarks { get; set; }
        public DateTime dateof_birth { get; set; }
        public string recruitmentadd_gid { get;set;}
        public Double mobilenumber { get; set; }
        public  string joblocation_gid { get; set; }
    }
    public  class candidate_list
    {
        public string candidate_name { get; set; }     
        public string mobile_no { get; set; }
        public string emailaddress1 { get; set; }
        public string emailaddress2 { get; set; }
        public string candidate_status { get; set; }
        public string experience { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
        public string recruitmentadd_gid { get; set; }
        public string recruiter_gid { get; set; }
    }
    public class Mdlinterviewprocess: result
    {
        
        public string validation_status { get; set; }
        public string recruitmentadd_gid { get; set; }
        public string jobposition_gid { get; set; }
        public string interview_scheduletime { get; set; }
        public string interview_date { get; set; }
        public string interview_type { get; set; }
        public string interview_status { get; set; }
        public string interview_remarks { get; set; }
        public string joining_status { get; set; }
        public string joining_date { get; set; }
        public string offered_ctc { get; set; }
        public string invoice_date { get; set; }
        public string remarks { get; set; }
        public string invoice_no { get; set; }
    }
    public class MdlCloning : result
    {
        public List<jobcode_list> jobcode_list { get; set; }
        public string businessunit_gid { get; set; }
        public string[] recruitmentadd_gid { get; set; }
    }
    public class jobcode_list
    {
      
        public string jobcodetitle { get; set; }
        public string jobposition_gid { get; set; }
    }
   
}