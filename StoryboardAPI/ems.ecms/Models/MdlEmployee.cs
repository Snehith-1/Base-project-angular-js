using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// employee Controller Class containing API methods for accessing the  Model class MdlEmployee - to get the employee details from the employee table
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class MdlEmployee : result
    {
        public List<employee_list> employee_list { get; set; }
    }
    public class employee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
}