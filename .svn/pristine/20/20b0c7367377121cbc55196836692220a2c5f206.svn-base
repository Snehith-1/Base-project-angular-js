using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;
using ems.ecms.DataAccess;
using ems.utilities.Functions;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using ems.utilities.Models;

namespace ems.ecms.Controllers
{
    /// <summary>
    /// ecmsdashboard Controller Class containing API methods for accessing the  DataAccess class DaDashboardecms
    /// Checking the preivilege for deferral and dashboard
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/ecmsdashboard")]
    [Authorize]
    public class EcmsDashboardController : ApiController
    {
        DaDashboardecms objDaAccess = new DaDashboardecms();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("checkpreivilegedeferral")]
        [HttpGet]
        public HttpResponseMessage checkpreivilegedeferral()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlDashboardecms values = new MdlDashboardecms();
            objDaAccess.Dacheckpreivilegedeferral(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("checkpreivilegedeferral1")]
        [HttpGet]
        public HttpResponseMessage checkpreivilegedeferral1()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlDashboardecms values = new MdlDashboardecms();
           objDaAccess.Dacheckpreivilegedeferral1(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ecmmsprivilege")]
        [HttpGet]
        public HttpResponseMessage getPrivilege(string user_gid)
        {
            ecmsprivilege objresult = new ecmsprivilege();
            objDaAccess.DaecmsPrevilege(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

    }
}
