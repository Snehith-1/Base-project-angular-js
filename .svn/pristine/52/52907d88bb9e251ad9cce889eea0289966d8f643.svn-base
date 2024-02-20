using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rms.Models
{
   
    public class mdldashboard : result
    {
        public List<businessunit_list> businessunit_list { get; set; }
       

    }
    public class businessunit_list
    {
        public string businessunit_name { get; set; }
        public string businessunit_gid { get; set; }
        public string candidatesourced_count { get; set; }
        public string candidatejoined_count { get; set; }
        public string openingjob_count { get; set; }
        public string closedjob_count { get; set; }

    }
    public class mdlRecruitersummary : result
    {
        public string month { get; set; }
        public string Year { get; set; }
        public List<recruitersummary_list> recruitersummary_list { get; set; }
    }
    public class recruitersummary_list : mdlRecruitersummary
    {
        public string recruitername { get; set; }
        public string candidate_sourced { get; set; }
        public string businessunitteam_name { get; set; }
        public string candidate_joined { get; set; }
        public string businessunit_name { get; set; }
        public string recruiter_photo { get; set; }
    }
    public class mdloverallstatus : result
    {
        public string countsourced { get; set; }
        public string countinterest { get; set; }
        public string countprocess { get; set; }
        public string countselected { get; set; }
        public string countjoined { get; set; }
        public string countrejected { get; set; }
    }
    public class dashboardprivilege:result
    {
        public string privilege { get; set; }
    }
    public class employeemail : result
    {
        public string user_code { get; set; }
        public string employeeemail_id { get; set; }
      public string user_password { get; set; }
    }
}