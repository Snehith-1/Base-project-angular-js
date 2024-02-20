using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///(It's used for pages in CibilData )CibilData Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Models
{
    public class MdlMstCibilData : result
    {
          public List<uploadedcibil_data> uploadedcibil_data { get; set; }
    }
    public class uploadedcibil_data
    {
        public string cibilhistory_flag { get; set; }
        public string uploaded_date { get; set; }
        public string uploaded_by { get; set; }
        public string file_name { get; set; }
        public string cibildata_gid { get; set; }
    }
    public class MdlCibilSummary:result
    {
         public List<cibilsummary_list> cibilsummary_list { get; set; }
    }
    public class cibilsummary_list
    {
        public string account_no { get; set; }
        public string name { get; set; }
        public string indicator { get; set; }
        public string submission_type { get; set; }
        public string account_status { get; set; }
        public string overdue_amount { get; set; }
        public string cibildatadtl_gid { get; set; }
        public string reason { get; set; }
        public string cibildata_gid { get; set; }
        public string uploaded_date { get; set; }
        public string submitted_on { get; set; }
    }

    public class MdlCibilEdit:result
    {
        public string submission_type { get; set; }
        public string submitted_on { get; set; }
        public string txtsubmitted_on { get; set; }
        public DateTime submittedon { get; set; }
        public string indicator { get; set; }
        public string name { get; set; }
        public string account_no { get; set; }
        public string current_balance { get; set; }
        public string overdue_amount { get; set; }
        public string odbucket_01 { get; set; }
        public string odbucket_02 { get; set; }
        public string odbucket_03 { get; set; }
        public string odbucket_04 { get; set; }
        public string odbucket_05 { get; set; }
        public string od_days { get; set; }
        public string cibildatadtl_gid { get; set; }
        public string account_status { get; set; }
        public DateTime closedon { get; set; }
        public string closed_on { get; set; }
        public string cibil { get; set; }
        public string highmark { get; set; }
        public string experian { get; set; }
        public string euifax { get; set; }
      
    }
}