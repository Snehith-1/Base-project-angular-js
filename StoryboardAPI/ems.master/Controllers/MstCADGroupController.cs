using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.master.Models;
using ems.master.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

/// <summary>
/// (It's used for pages in CADGroup)CADGroup Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{

    [RoutePrefix("api/MstCADGroup")]
    [Authorize]
    public class MstCADGroupController : ApiController
    {
        DaCADGroup objDaCADGroupSummary = new DaCADGroup();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Add

        [ActionName("PostCADGroup")]
        [HttpPost]
        public HttpResponseMessage PostCADGroup(MdlCadGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADGroupSummary.DaPostCADGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCADGroup")]
        [HttpGet]
        public HttpResponseMessage GetCADGroup()
        {
            MdlCadGroup objmaster = new MdlCadGroup();
            objDaCADGroupSummary.DaGetCADGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetCADGroupEmployee")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupManager(string cadgroup_gid)
        {
            cadgrouphead values = new cadgrouphead();
            objDaCADGroupSummary.DaGetCADGroupEmployee(cadgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        //Edit

        [ActionName("GetCADGroupEdit")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupEdit(string cadgroup_gid)
        {
            MdlCadGroup objmaster = new MdlCadGroup();
            objDaCADGroupSummary.DaGetCADGroupEdit(cadgroup_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CADGroupUpdate")]
        [HttpPost]
        public HttpResponseMessage CADGroupUpdate(MdlCadGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADGroupSummary.DaCADGroupUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete

        [ActionName("DeleteCADGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteCADGroup(string cadgroup_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADGroupSummary.DaDeleteCADGroup(cadgroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}