using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Controllers will store values from Amendment master to Add, edit, view, active, inactive the records
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>

    public class MdlAgrMstAmendment : result
    {
        public List<amendment_list> amendment_list { get; set; }
    }
    public class amendment_list
    {
        public string amendment_gid { get; set; }
        public string amendment { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string api_code { get; set; }
    }
    public class amendmentlist : result
    {
        public string amendment_gid { get; set; }
        public string amendment { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class AmendmentInactiveHistory : result
    {
        public List<amendmentinactivehistory_list> amendmentinactivehistory_list { get; set; }
    }
    public class amendmentinactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}