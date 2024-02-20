using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstRepayment:result
    {
        public List<repayment_list> repayment_list { get; set; }
        public List<repaymentaccount_list> repaymentaccount_list { get; set; }
    }
    public class repayment_list
    {
        public string repyment_gid { get; set; }
        public string account_no { get; set; }
        public string repayment_date { get; set; }
        public string transaction_date { get; set; }
        public string principal { get; set; }
        public string repayment_amount { get; set; }
        public string repayment_type { get; set; }
        public string dpd { get; set; }
    }
    public class repaymentaccount_list
    {
        public string repyment_gid { get; set; }
        public string account_no { get; set; }
        public string count { get; set; }
        public string transaction_date { get; set; }
        public string principal { get; set; }
        public string repayment_amount { get; set; }


    }
    public class MdlRepaymentInfo : result
    {
        public string repyment_gid { get; set; }
        public string account_no { get; set; }
        public string repayment_date { get; set; }
        public string transaction_date { get; set; }
        public string principal { get; set; }
        public string repayment_amount { get; set; }
        public string remarks { get; set; }
        public string transaction_id { get; set; }
        public string normal_interest { get; set; }
        public string penal_interest { get; set; }
        public string for_feiture_waiver { get; set; }
        public string user_id { get; set; }
        public string instrument { get; set; }
        public string repayment_type { get; set; }
        public string dpd { get; set; }
        public string old_dpd { get; set; }
        public string account_code { get; set; }
        public string interest_tds { get; set; }
        public string URN { get; set; }
    }
}