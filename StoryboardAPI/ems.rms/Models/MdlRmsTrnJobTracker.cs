using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rms.Models
{

    public class JobTrackerList : result
    {

        public List<jobTrackersummary> jobTrackersummary { get; set; }
    }

    public class jobTrackersummary
    {
        public string color_flag { get; set; }
        public string jobposition_gid { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit2team_gid { get; set; }
        public string company_name { get; set; }
        public string job_code { get; set; }
        public string spoc_name { get; set; }
        public string noof_position { get; set; }
        public string created_date { get; set; }
        public string businessunit_name { get; set; }
        public string transfer_flag { get; set; }
        public string jobcodefreeze_flag { get; set; }
        public string transfer_from { get; set; }
        public string position_closed { get; set; }
        public string job_status { get; set; }
        public string candidate_fetched { get; set; }
        public string position_pending { get; set; }

    }

    public class jobPositionDetails : result
    {
        public string jobposition_gid { get; set; }
        public string jobcode { get; set; }
        public string job_type { get; set; }
        public string company_name { get; set; }
        public string noof_position { get; set; }
        public string Accepted_candidate { get; set; }
    }

    public class jobCodeViewDetails : result
    {
        public string jobposition_gid { get; set; }
        public string jobcode { get; set; }
        public string job_type { get; set; }
        public string noof_position { get; set; }
        public string experience { get; set; }
        public string ctc_budget { get; set; }
        public string education_qualification { get; set; }
        public string job_description { get; set; }
        public string mandatroy_skills { get; set; }
        public string notice_period { get; set; }
        public string domain { get; set; }
        public string job_location { get; set; }


    }

    public class recruiterList : result
    {
        public List<recruiterSummary> recruiterSummary { get; set; }
    }

    public class recruiterSummary
    {
       
        public string candidate_fetched { get; set; }
        public string position_closed { get; set; }
        public string position_pending { get; set; }
        public string jobposition_gid { get; set; }
        public string freeze_flag { get; set; }
        public string recruiter_gid { get; set; }
        public string businessunitteam_name { get; set; }
        public string businessunit_name { get; set; }
        public string recruiter_name { get; set; }
        public string noof_position { get; set; }

    }

      
}
