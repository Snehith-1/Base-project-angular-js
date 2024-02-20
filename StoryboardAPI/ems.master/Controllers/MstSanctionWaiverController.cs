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
    [RoutePrefix("api/MstSanctionWaiver")]
    [Authorize]

    public class MstSanctionWaiverController : ApiController
    {
        DaSanctionWaiver objDaSanctionWaiver = new DaSanctionWaiver();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Add

        [ActionName("PostSanctionWaiver")]
        [HttpPost]
        public HttpResponseMessage PostSanctionWaiver(MdlMstSanctionWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionWaiver.DaPostSanctionWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionWaiver")]
        [HttpGet]
        public HttpResponseMessage GetSanctionWaiver()
        {
            MdlMstSanctionWaiver objmaster = new MdlMstSanctionWaiver();
            objDaSanctionWaiver.DaGetSanctionWaiver(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetSanctionEdit")]
        [HttpGet]
        public HttpResponseMessage GetSanctionEdit(string sanctionwaiver_gid)
        {
            MdlMstSanctionWaiver objmaster = new MdlMstSanctionWaiver();
            objDaSanctionWaiver.DaGetSanctionEdit(sanctionwaiver_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateSanctionWaiver")]
        [HttpPost]
        public HttpResponseMessage UpdateSanctionWaiver(MdlMstSanctionWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionWaiver.DaUpdateSanctionWaiver(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSanctionWaiver")]
        [HttpPost]
        public HttpResponseMessage InactiveSanctionWaiver(MdlMstSanctionWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionWaiver.DaInactiveSanctionWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSanctionWaiverHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveSanctionWaiverHistory(string sanctionwaiver_gid)
        {
            MdlMstSanctionWaiver objmaster = new MdlMstSanctionWaiver();
            objDaSanctionWaiver.DaInactiveSanctionWaiverHistory(objmaster, sanctionwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        ////Delete

        [ActionName("DeleteSanctionWaiver")]
        [HttpGet]
        public HttpResponseMessage DeleteSanctionWaiver(string sanctionwaiver_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanctionWaiver.DaDeleteSanctionWaiver(sanctionwaiver_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}