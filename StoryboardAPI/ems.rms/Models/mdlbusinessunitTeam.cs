using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rms.Models
{
    public class mdlbusinessunitteam : result
    {
        public string businessunit_name { get; set; }
        public string openingjob_count { get; set; }
        public string closedjob_count { get; set; }
        public string jobfreeze_count { get; set; }
        public List<businessunitteamname_list> businessunitteamname_list { get; set; }
    }
    public class businessunitteamname_list
    {
        public string businessunit_gid { get; set; }
        public string businessunitteam_name { get; set; }
        public string teamcandidate_sourced { get; set; }
        public string teamcandidate_joined { get; set; }
        public string employee_photo { get; set; }
        public string teamleader_name { get; set; }
        public string businessunit2team_gid { get; set; }
    }

}
