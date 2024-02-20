using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rms.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }

    }
    public class JobList : result
    {
        public string lsuser_name { get; set; }
        public string spocvalidation { get; set; }
        public List<jobsummary> jobsummary { get; set; }
    }
    public class jobsummary
    {
        public string color_flag { get; set; }
        public string jobposition_gid { get; set; }
        public string jobposition2team_gid { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit2team_gid { get; set; }
        public string company_name { get; set; }
        public string job_code { get; set; }
        public string job_title { get; set; }
        public string spoc_name { get; set; }
        public string noof_position { get; set; }
        public string created_date { get; set; }
        public string businessunit_name { get; set; }
        public string transfer_flag { get; set; }
        public string jobcodefreeze_flag { get; set; }
        public string transfer_from { get; set; }
        public string transfer_to { get; set; }
        public string sharing { get; set; }

    }

    public class TodayInterviewList : result
    {

        public List<TodayInterviewSummary> TodayInterviewSummary { get; set; }
    }

    public class TodayInterviewSummary
    {
        public string today_count { get; set; }
        public string businessunit_name { get; set; }
        public string job_code { get; set; }
        public string job_title { get; set; }
        public string recruiter_name { get; set; }
        public string candidate_name { get; set; }
        public string candidate_mobileno { get; set; }
        public string candidate_dob { get; set; }
        public string experience { get; set; }
        public string interviewscheduled_time { get; set; }
        public string interview_gid { get; set; }
        public string company_name { get; set; }
        public string businessunitteam_name { get; set; }
    }

    public class InterviewList : result
    {

        public List<InterviewSummary> InterviewSummary { get; set; }
    }

    public class InterviewSummary
    {

        public string businessunit_name { get; set; }
        public string job_code { get; set; }
        public string job_title { get; set; }
        public string recruiter_name { get; set; }
        public string candidate_name { get; set; }
        public string candidate_mobileno { get; set; }
        public string candidate_dob { get; set; }
        public string experience { get; set; }
        public string interviewscheduled_time { get; set; }
        public string interview_gid { get; set; }
        public string company_name { get; set; }
        public string businessunitteam_name { get; set; }
    }
    public class AssignSPOCSummaryList : result
    {
        public string jobposition_gid { get; set; }
        public List<assignSPOCsummary> assignSPOCsummary { get; set; }
    }

    public class assignSPOCsummary
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string businessunit_name { get; set; }
        public string businessunitteam_name { get; set; }
    }

    public class SearchAssignSPOCSummaryList : result
    {
        public string jobposition_gid { get; set; }
        public string businessunit_gid { get; set; }
        public List<searchassignSPOCsummary> searchassignSPOCsummary { get; set; }
        public List<searchbusinessunitteam> searchbusinessunitteam { get; set; }
    }

    public class searchassignSPOCsummary
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string businessunit_name { get; set; }
        public string businessunitteam_name { get; set; }
    }

    public class UnAssignSPOCSummaryList : result
    {
        public string jobposition_gid { get; set; }
        public List<UnassignSPOCsummary> UnassignSPOCsummary { get; set; }
    }

    public class UnassignSPOCsummary
    {
        public string recruiter_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_code { get; set; }
        public string businessunit_name { get; set; }
        public string businessunitteam_name { get; set; }
    }

    public class ShareTeamSummaryList : result
    {
        public string jobposition_gid { get; set; }
        public List<ShareTeamsummary> ShareTeamsummary { get; set; }
    }

    public class ShareTeamsummary
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string businessunit_manager { get; set; }
    }

    public class searchbusinessunitteam
    {
        public string businessunit2team_gid { get; set; }
    }

    public class BusinessUnitList : result
    {

        public List<BusinessUnit> BusinessUnit { get; set; }
    }

    public class BusinessUnit
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
    }

    public class BusinessUnitteamList : result
    {

        public List<BusinessUnitteam> BusinessUnitteam { get; set; }
    }

    public class BusinessUnitteam
    {
        public string businessunit2team_gid { get; set; }
        public string businessunit2team_name { get; set; }
    }

    public class AssignSPOCList : result
    {

        public List<AssignSPOC> AssignSPOC { get; set; }
    }

    public class AssignSPOC
    {
        public string spoc_gid { get; set; }
        public string spoc_name { get; set; }
    }


    public class RecruiterList : result
    {
        public string businessunit2team_gid { get; set; }
        public List<Recruiter> Recruiter { get; set; }
    }

    public class Recruiter
    {
        public string businessunit2team_gid { get; set; }
        public string recruiter_gid { get; set; }
        public string recruiter_name { get; set; }
    }

    public class JobLocationList : result
    {
        public List<Joblocation> Joblocation { get; set; }
    }

    public class Joblocation
    {
        public string joblocation_gid { get; set; }
        public string joblocation_name { get; set; }
    }

    public class AddJob : result
    {
        public string job_type { get; set; }
        public string company_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunitname { get; set; }
        public string user_name { get; set; }
        public string employee_gid { get; set; }
        public string spoc_gid { get; set; }
        public string spoc_name { get; set; }
        public string domain_name { get; set; }
        public string website { get; set; }
        public string noof_position { get; set; }
        public string experience { get; set; }
        public string experience_max { get; set; }
        public string education_qualification { get; set; }
        public string ctc_budget { get; set; }
        public string ctc_max { get; set; }
        public string search_location { get; set; }
        public string noticeperiod { get; set; }
        public string duplicate_validity { get; set; }
        public string mandatory_skills { get; set; }
        public string job_description { get; set; }
        public string pnl_spoc { get; set; }
        public string pnl_spocdropdown { get; set; }
        public List<Joblocation> Joblocation { get; set; }
        public List<businessunit2team> businessunit2team { get; set; }
        public List<positionrecruiter_list> positionrecruiter_list { get; set; }
    }

    public class businessunit2team
    {
        public string businessunit2team_gid { get; set; }
        public string businessunit2team_name { get; set; }
    }

    public class positionrecruiter_list
    {
        public string recruiter_gid { get; set; }
        public string recuiter_name { get; set; }
    }

    public class JobDetails : result
    {
        public string jobposition_gid { get; set; }
        public string jobcode { get; set; }
        public string job_type { get; set; }
        public string company_name { get; set; }
        public string website { get; set; }
        public string assign_spoc { get; set; }
        public string domain_name { get; set; }
        public string joblocation { get; set; }
        public string noof_position { get; set; }
        public string skills { get; set; }
        public string search_location { get; set; }
        public string education_qualification { get; set; }
        public string experiencemax { get; set; }
        public string experience { get; set; }
        public string ctcbudget_min { get; set; }
        public string ctcbudget_max { get; set; }
        public string notice_period { get; set; }
        public string duplicate_validity { get; set; }
        public string jobdescription { get; set; }

    }

    public class AddAssignSPOC : result
    {
        public string jobposition_gid { get; set; }
        public string businessunit_gid { get; set; }
        public string[] employee_gid { get; set; }
    }


    public class AddShareTeam : result
    {
        public string jobposition_gid { get; set; }
        public string[] businessunit_gid { get; set; }
    }

    public class AddUnAssignSPOC : result
    {
        public string[] employee_gid { get; set; }
        public string jobposition_gid { get; set; }
    }

    public class JobRecruiterFreeze : result
    {
        public string jobposition_gid { get; set; }
        public string recruiter_gid { get; set; }
        public string recruiter_freezeremarks { get; set; }
    }

    public class JobcodeFreeze : result
    {
        public string jobposition_gid { get; set; }
        public string code_freezeremarks { get; set; }
    }

    public class JobFreezeDetails : result
    {
        public string jobPosition { get; set; }
        public string recruiter { get; set; }
    }

    public class inlinerecruiterList : result
    {
        public List<inlinerecruiterSummary> inlinerecruiterSummary { get; set; }
    }

    public class inlinerecruiterSummary
    {
        public string jobposition_gid { get; set; }
        public string freeze_flag { get; set; }
        public string recruiter_gid { get; set; }
        public string businessunitteam_name { get; set; }
        public string businessunit_name { get; set; }
        public string recruiter_name { get; set; }

    }

    public class JobRecruiterFreezeDetails : result
    {
        public string jobposition_gid { get; set; }
        public string jobPosition { get; set; }
        public string recruiter_gid { get; set; }
        public string recruiter { get; set; }
    }

    public class businessunitteamList : result
    {
        public List<businessunitteamdetail> businessunitteamdetail { get; set; }
    }

    public class businessunitteamdetail
    {
        public string user_gid { get; set; }
        public string employee_gid { get; set; }
        public string businessunitteam_name { get; set; }

    }

    public class sharingdetailList :result
    {
        public List<sharingdetail> sharingdetail { get; set; }
    }

    public class sharingdetail
    {
        public string sharing { get; set; }      

    }

    //public class colordetailList
    //{
    //    public List<colordetail> colordetail { get; set; }
    //}

    //public class colordetail
    //{
    //    public string sharing { get; set; }

    //}
}