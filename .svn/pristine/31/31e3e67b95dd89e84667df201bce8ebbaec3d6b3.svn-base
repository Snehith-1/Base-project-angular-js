using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// (It's used for Designation Master)Designation Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/Designation")]
    [Authorize]

    public class DesignationController : ApiController
    {
        DaDesignation objDaDesignation = new DaDesignation();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetDesignation")]
        [HttpGet]
        public HttpResponseMessage getDesignation()
        {
            MdlDesignation objMdlDesignation = new MdlDesignation();
            objDaDesignation.DaGetDesignation(objMdlDesignation);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlDesignation);
        }

        [ActionName("CreateDesignation")]
        [HttpPost]
        public HttpResponseMessage createdesignation(designation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDesignation.DaCreateDesignation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditDesignation")]
        [HttpGet]
        public HttpResponseMessage editdesignation(string designation_gid)
        {
            designation values = new designation();
            objDaDesignation.DaEditDesignation(designation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateDesignation")]
        [HttpPost]
        public HttpResponseMessage updatedesignation(designation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDesignation.DaUpdateDesignation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteDesignation")]
        [HttpGet]
        public HttpResponseMessage deletedesignation(string designation_gid)
        {
            designation values = new designation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDesignation.DaDeleteDesignation(designation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----Get Designation- order by ASC----//
        [ActionName("GetDesignationASC")]
        [HttpGet]
        public HttpResponseMessage getDesignationASC()
        {
            MdlDesignation objMdlDesignation = new MdlDesignation();
            objDaDesignation.DaGetDesignationASC(objMdlDesignation);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlDesignation);
        }
        [ActionName("DesignationStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage DesignationStatusUpdate(designation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDesignation.DaDesignationStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string designation_gid)
        {
            MdlDesignation values = new MdlDesignation();
            objDaDesignation.DaGetActiveLog(designation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}