using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.hrm.Models;
using ems.hrm.DataAccess;
using System.Net.Http;
using System.Web.Http;

namespace StoryboardAPI.Controllers.ems.hrm
{
    [RoutePrefix("api/companyPolicy")]
    [Authorize]

    public class companyPolicyController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCompanyPolicy objDaCompanyPolicy = new DaCompanyPolicy();

        [ActionName("policy")]
        [HttpGet]

        public HttpResponseMessage getcompanypolicy()
        {
            company_policy objcompanypolicy = new company_policy();
            objDaCompanyPolicy.companypolicy_da(objcompanypolicy);
            return Request.CreateResponse(HttpStatusCode.OK, objcompanypolicy);
        }

    }
}