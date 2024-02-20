using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;
using ems.ecms.DataAccess;
using ems.utilities.Models;
using ems.utilities.Functions;

namespace ems.ecms.Controllers
{
    /// <summary>
    /// employee Controller Class containing API methods for accessing the  DataAccess class DaEmployee - to get the employee details from the employee table
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/employee")]
    [Authorize]
    public class EmployeeController : ApiController
    {
        DaEmployee  objDaEmployee = new DaEmployee ();
        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            MdlEmployee objMdlEmployee = new MdlEmployee();
            objDaEmployee.DaGetEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
    }
}
