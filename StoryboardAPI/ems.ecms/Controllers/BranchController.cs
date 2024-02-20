using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.DataAccess;
using ems.ecms.Models;

namespace ems.ecms.Controllers
{
    /// <summary>
    /// Branch Controller Class containing API methods for accessing the  DataAccess class DaBranch - to get the branch details
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/branch")]
    [Authorize]
    public class BranchController : ApiController
    {
   
        [ActionName("Branch")]
        [HttpGet]
        public HttpResponseMessage Branch()
        {
            MdlBranch objMdlBranch = new MdlBranch();
            DaBranch objDaBranch = new DaBranch();
            objDaBranch.DaGetBranch(objMdlBranch);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlBranch);
        }
    }
}
