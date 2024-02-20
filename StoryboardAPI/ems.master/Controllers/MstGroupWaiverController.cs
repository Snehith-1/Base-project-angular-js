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
    [RoutePrefix("api/MstGroupWaiver")]
    [Authorize]

    public class MstGroupWaiverController : ApiController
    {
        DaGroupWaiver objDaGroupWaiver = new DaGroupWaiver();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Add

        [ActionName("PostGroupWaiver")]
        [HttpPost]
        public HttpResponseMessage PostGroupWaiver(MdlMstGroupWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaGroupWaiver.DaPostGroupWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupWaiver")]
        [HttpGet]
        public HttpResponseMessage GetGroupWaiver()
        {
            MdlMstGroupWaiver objmaster = new MdlMstGroupWaiver();
            objDaGroupWaiver.DaGetGroupWaiver(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetAssignMember")]
        [HttpGet]
        public HttpResponseMessage GetAssignMember(string groupwaiver_gid)
        {
            groupassignmember values = new groupassignmember();
            objDaGroupWaiver.DaGetAssignMember(groupwaiver_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit

        [ActionName("GetGroupWaiverEdit")]
        [HttpGet]
        public HttpResponseMessage GetGroupWaiverEdit(string groupwaiver_gid)
        {
            MdlMstGroupWaiver objmaster = new MdlMstGroupWaiver();
            objDaGroupWaiver.DaGetGroupWaiverEdit(groupwaiver_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateGroupWaiver")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupWaiver(MdlMstGroupWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaGroupWaiver.DaUpdateGroupWaiver(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Status

        [ActionName("InactiveGroupWaiver")]
        [HttpPost]
        public HttpResponseMessage InactiveGroupWaiver(MdlMstGroupWaiver values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaGroupWaiver.DaInactiveGroupWaiver(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveGroupWaiverHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveGroupWaiverHistory(string groupwaiver_gid)
        {
            MdlMstGroupWaiver objmaster = new MdlMstGroupWaiver();
            objDaGroupWaiver.DaInactiveGroupWaiverHistory(objmaster, groupwaiver_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //Delete

        [ActionName("DeleteGroupWaiver")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupWaiver(string groupwaiver_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaGroupWaiver.DaDeleteGroupWaiver(groupwaiver_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}