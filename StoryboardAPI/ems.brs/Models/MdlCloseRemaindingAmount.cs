using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.brs.Models
{
    /// <summary>
    /// The remianing is below 2 or 3  rupees the ticket will closed automatically condition based master
    /// </summary>
    ///<remarks>Written by Komathi</remarks>
    public class MdlCloseRemaindingAmount:result
    {
        public List<remaindingamount_list> remaindingamount_list { get; set;}
        public List<inactiveremaindingamount_list> inactiveremaindingamount_list { get; set; }

    }
    public class remaindingamount_list:result
    {
        public string remaindingamount_gid { get; set; }
        public string remaindingamount_name { get; set; }
        public string remaindingamount_amount { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set;}
        public string updated_by { get; set;}
        public string remainding_status { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public string activeamount_count { get; set; }

        public string remainding_amount { get; set; }

    }
    public class inactiveremaindingamount_list
    {
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string status { get; set; }
        public string remaindingamount_gid { get; set; }
    }

}