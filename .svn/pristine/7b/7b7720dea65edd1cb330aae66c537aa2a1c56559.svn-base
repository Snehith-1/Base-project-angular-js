using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.idas.Models;
using ems.idas.DataAccess;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasSanctionMIS")]
    [Authorize]

    public class IdasSanctionMISController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasSanctionMIS objDaIdasSanctionMIS = new DaIdasSanctionMIS();

        [ActionName("GetSanctionMISExport")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISExport()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            exportMIS values = new exportMIS();
            objDaIdasSanctionMIS.DaGetSanctionMISExport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionMISSummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISSummary()
        {
            sanctionMISSummary values = new sanctionMISSummary();
            objDaIdasSanctionMIS.DaGetSanctionMISSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionMISDetails")]
        [HttpGet]
        public HttpResponseMessage GetSanctionMISDetails(string sanction_gid)
        {
            sanctionMISviewdtl values = new sanctionMISviewdtl();
            objDaIdasSanctionMIS.DaGetSanctionMISDetails(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}
