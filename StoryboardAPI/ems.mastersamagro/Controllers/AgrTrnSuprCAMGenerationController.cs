using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will help to generate CAM document in supplier credit and pmg stages
    /// </summary>
    /// <remarks>Written by Sherin Augusta </remarks>


    [RoutePrefix("api/AgrTrnSuprCAMGeneration")]
    [Authorize]

    public class AgrTrnSuprCAMGenerationController : ApiController
    {
        DaAgrTrnCAMGeneration objDaAccess = new DaAgrTrnCAMGeneration();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //Get CAM Template
        [ActionName("GetCAMTemplate")]
        [HttpGet]
        public HttpResponseMessage GetCAMTemplate(string application_gid)
        {
            MdlMstCAMGeneration objtemplatedtl = new MdlMstCAMGeneration();
            objDaAccess.DaGetCAMTemplate(application_gid, objtemplatedtl);
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
            MdlMstCAMGeneration objtemplatedtl = new MdlMstCAMGeneration();
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
