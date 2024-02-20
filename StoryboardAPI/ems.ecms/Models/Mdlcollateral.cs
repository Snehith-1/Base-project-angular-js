using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// collateral Controller Class containing API methods for accessing the  Model class Mdlcollateral 
    /// To create collateral details, delete collateral, get collateral details and update collateral details
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class Mdlcollateral:result 
    {
        public List<customercollateral> customercollateral_list { get; set; }
        
    }
    public class customercollateral:result
    {
        public string collateral_gid { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string securitytype_gid { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string account_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] loan_gid { get; set; }
        public List<collateralloandetails> collateralloandetails_list { get; set; }

    }
    public class mdlcollaterloandetails_list:result
    {
        public List<collateralloandetails> collateralloandetails_list { get; set; }
    }
    public class collateralloandetails
    {
        public string loan_title { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
    }
    
}