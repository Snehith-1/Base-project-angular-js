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
    /// vertical Controller Class containing API methods for accessing the  DataAccess class DaVertical - 
    ///  Vertical - summary, add, update, edit, update log, delete
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/vertical")]
    [Authorize]
    public class VerticalController : ApiController
    {
        DaVertical objDaVertical = new DaVertical();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("createVertical")]
        [HttpPost]
        public HttpResponseMessage CreateVertical(mdlcreateVertical values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaVertical.DaPostCreatVvertical(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("vertical")]
        [HttpGet]
        public HttpResponseMessage getVertical()
        {
            MdlVertical objMdlVertical = new MdlVertical();
            objDaVertical.DaGetVertical(objMdlVertical);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlVertical);
        }
        

        [ActionName("Getverticalupdate")]
        [HttpGet]
        public HttpResponseMessage getverticaldetails(string vertical_gid)
        {
            verticaledit values = new verticaledit();
            objDaVertical.DaGetEditVertical(vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("verticalUpdate")]
        [HttpPost]
        public HttpResponseMessage updatevertical(verticaledit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           objDaVertical.DaPostUpdateVertical(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("verticalDelete")]
        [HttpGet]
        public HttpResponseMessage deletevertical(string vertical_gid)
        {
            verticaledit values = new verticaledit();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVertical.DaPostDeleteVertical (vertical_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("VerticalStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage VerticalStatusUpdate(verticaledit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVertical.DaVerticalStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string vertical_gid)
        {
            MdlVertical values = new MdlVertical();
            objDaVertical.DaGetActiveLog(vertical_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
