using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.master.Models;
using ems.master.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

/// <summary>
/// (It's used for pages in CAMGeneration)CAMGeneration Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCAMGeneration")]
    [Authorize]
    public class MstCAMGenerationController : ApiController
    {
        DaMstCAMGeneration objDaAccess = new DaMstCAMGeneration();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //Get CAM Template
        [ActionName("GetCAMTemplate")]
        [HttpGet]
        public HttpResponseMessage GetCAMTemplate(string application_gid )
        {
            MdlMstCAMGeneration objtemplatedtl = new MdlMstCAMGeneration();
            objDaAccess.DaGetCAMTemplate(application_gid,objtemplatedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objtemplatedtl);
        }
        [ActionName("WordGenerate")]
        [HttpPost]
        public HttpResponseMessage PostWordGenerate(MdlMstCAMGeneration values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostWordGenerate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetApp2CAM")]
        [HttpGet]
        public HttpResponseMessage GetApp2CAM(string application_gid)
        {
            MdlTemplatelist objtemplatedtl = new MdlTemplatelist();
            objDaAccess.DaGetApp2CAM(application_gid, objtemplatedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objtemplatedtl);
        }

        [ActionName("PostWordSave")]
        [HttpPost]
        public HttpResponseMessage PostWordSave(MdlMstCAMGeneration values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostWordSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
