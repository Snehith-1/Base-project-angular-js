using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
/// <summary>
/// (It's used for Associate Master master) Associate Master Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/AssociateMaster")]
    [Authorize]

    public class AssociateMasterController : ApiController
    {
        DaAssociateMaster objDaAssociateMaster = new DaAssociateMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetAssociateMaster")]
        [HttpGet]
        public HttpResponseMessage getAssociateMaster()
        {
            MdlAssociateMaster objMdlAssociateMaster = new MdlAssociateMaster();
            objDaAssociateMaster.DaGetAssociateMaster(objMdlAssociateMaster);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAssociateMaster);
        }

        [ActionName("CreateAssociateMaster")]
        [HttpPost]
        public HttpResponseMessage createAssociateMaster(associatemaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAssociateMaster.DaCreateAssociateMaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssociateMaster")]
        [HttpGet]
        public HttpResponseMessage editassociatemaster(string associatemaster_gid)
        {
            associatemaster values = new associatemaster();
            objDaAssociateMaster.DaEditAssociateMaster(associatemaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAssociateMaster")]
        [HttpPost]
        public HttpResponseMessage updateassociatemaster(associatemaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAssociateMaster.DaUpdateAssociateMaster(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAssociateMaster")]
        [HttpGet]
        public HttpResponseMessage deleteAssociateMaster(string associatemaster_gid)
        {
            associatemaster values = new associatemaster();
            objDaAssociateMaster.DaDeleteAssociateMaster(associatemaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //--------Get Asscociate values with order by ASC---/
        [ActionName("GetAssociateMasterASC")]
        [HttpGet]
        public HttpResponseMessage getAssociateMasterASC()
        {
            MdlAssociateMaster objMdlAssociateMaster = new MdlAssociateMaster();
            objDaAssociateMaster.DaGetAssociateMasterASC(objMdlAssociateMaster);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAssociateMaster);
        }
    }
}