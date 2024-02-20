using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.iasn.Models;
using ems.iasn.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;


namespace ems.iasn.Controllers
{
    [RoutePrefix("api/IasnMstZone")]
    [Authorize]
    public class IasnMstZoneController : ApiController
    {
        DaIasnMstZone objDaAccess = new DaIasnMstZone();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        [ActionName("CreateZone")]
        [HttpPost]
        public HttpResponseMessage CreateZone(MdlCreateZone values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostZone(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("UpdateZone")]
        [HttpPost]
        public HttpResponseMessage UpdateZone(MdlUpdateZone values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaUpdateZone(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("EditZone")]
        [HttpGet]
        public HttpResponseMessage EditZone(string zone_gid)
        {

            MdlZoneEdit values = new Models.MdlZoneEdit();
            objDaAccess.DaGetZoneEdit (zone_gid ,values );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ZoneSummary")]
        [HttpGet]
        public HttpResponseMessage ZoneSummary()
        {

            MdlZoneSummaryList values = new Models.MdlZoneSummaryList();
            objDaAccess.DaGetZoneSummary (values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage Employee()
        {

            MdlEmployeeList values = new Models.MdlEmployeeList();
            objDaAccess.DaGetEmployeeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RMStatusSummary")]
        [HttpGet]
        public HttpResponseMessage RMStatusSummary(string zone_name)
        {

            MdlZoneSummaryList values = new Models.MdlZoneSummaryList();
            objDaAccess.DaGetRMStatusSummary(zone_name,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // RM Delete
        [ActionName("RM_Delete")]
        [HttpGet]
        public HttpResponseMessage RM_Delete(string employee_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRMStatusSummary values = new MdlRMStatusSummary();
            objDaAccess.DaRM_Delete(employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Zone Delete
        [ActionName("Zone_Delete")]
        [HttpGet]
        public HttpResponseMessage Zone_Delete(string zone_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRMStatusSummary values = new MdlRMStatusSummary();
            objDaAccess.DaZone_Delete(zone_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Add RM Name
        [ActionName("PostRMName")]
        [HttpPost]
        public HttpResponseMessage PostRMName(MdlCreateZone values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostRMName(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}
