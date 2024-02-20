using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstTelecall:result
    {
        public List<telecall_list> telecall_list { get; set; }
    
    }
    public class telecall_list
    {
        public string telecall_gid { get; set; }
        public string account_no { get; set; }
        public string start_time { get; set; }
        public string completetion_time { get; set; }
        public string typeof_call { get; set; }
       
    }
    public class telecallaccount_info:result
    {
        public string telecall_gid { get; set; }
        public string account_no { get; set; }
        public string start_time { get; set; }
        public string completetion_time { get; set; }
        public string typeof_call { get; set; }
        public string URN { get; set; }
        public string email_address { get; set; }
        public string name { get; set; }
        public string relationship { get; set; }
        public string customer_name { get; set; }
        public string call_details { get; set; }
        public string spoken_to { get; set; }
        public string natureof_business { get; set; }
        public string reason_OD { get; set; }
        public string telecall_status { get; set; }
        public string courseof_action { get; set; }
        public string ptp_date { get; set; }
        public string ptp_amount { get; set; }
        public string remarksby_telecaller { get; set; }
        public string followup_date { get; set; }
        public string ledger_balance { get; set; }
        public string total_demand_due { get; set; }
       
    }
}