using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstLANWaiver")]
    [Authorize]

    public class MstLANWaiverController : ApiController
    {
        DaLANWaiver objDaLANWaiver = new DaLANWaiver();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Add

        [ActionName("PostLANWaiver")]
        [HttpPost]
        public HttpResponseMessage PostLANWaiver(MdlMstLANWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLANWaiver.DaPostLANWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLANWaiver")]
        [HttpGet]
        public HttpResponseMessage GetLANWaiver()
        {
            MdlMstLANWaiver objmaster = new MdlMstLANWaiver();
            objDaLANWaiver.DaGetLANWaiver(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetLANEdit")]
        [HttpGet]
        public HttpResponseMessage GetLANEdit(string lanwaiver_gid)
        {
            MdlMstLANWaiver objmaster = new MdlMstLANWaiver();
            objDaLANWaiver.DaGetLANEdit(lanwaiver_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateLANWaiver")]
        [HttpPost]
        public HttpResponseMessage UpdateLANWaiver(MdlMstLANWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLANWaiver.DaUpdateLANWaiver(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLANWaiver")]
        [HttpPost]
        public HttpResponseMessage InactiveLANWaiver(MdlMstLANWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLANWaiver.DaInactiveLANWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLANWaiverHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveLANWaiverHistory(string lanwaiver_gid)
        {
            MdlMstLANWaiver objmaster = new MdlMstLANWaiver();
            objDaLANWaiver.DaInactiveLANWaiverHistory(objmaster, lanwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        ////Delete

        [ActionName("DeleteLANWaiver")]
        [HttpGet]
        public HttpResponseMessage DeleteLANWaiver(string lanwaiver_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaLANWaiver.DaDeleteLANWaiver(lanwaiver_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}