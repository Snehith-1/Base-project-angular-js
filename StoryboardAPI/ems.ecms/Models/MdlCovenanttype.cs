using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// covenanttype Controller Class containing API methods for accessing the  Model class mdlcreateconvenantType
    /// To create covenant type, update covenat type, delete covenant type, summary of covenant type
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class mdlcreateconvenantType:result
    {
        public string covenanttype_code { get; set; }
        public string covenanttype_name { get; set; }
        public string criticallity { get; set; }
        public string comments { get; set; }
    }
    public class MdlCovenanttype:result
    {
        public List<covenanttype_list> covenanttype_list { get; set; }
    }
    public class covenanttype_list
    {
        public string covenanttype_gid { get; set; }
        public string covenanttype_code { get; set; }
        public string covenanttype_name { get; set; }
        public string criticallity { get; set; }
        public string comments { get; set; }
    }
    public class covenantypeedit:result
    {
        public string covenanttype_gid { get; set; }
        public string covenantTypecodeedit { get; set; }
        public string covenantTypenameedit { get; set; }
        public string criticallity { get; set; }
        public string comments { get; set; }
    }
}