using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// Branch Controller Class containing API methods for accessing the  Model class MdlBranch - to get the branch details
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class MdlBranch : result
        {
            public List<branch_list> branch_list { get; set; }
        }
        public class branch_list
        {
            public string branch_gid { get; set; }
            public string branch_name { get; set; }
        }
    }