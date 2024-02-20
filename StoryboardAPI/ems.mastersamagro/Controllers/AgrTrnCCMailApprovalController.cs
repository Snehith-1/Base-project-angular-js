using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to mail trigger events from AC management flow.
    /// </summary>
    /// <remarks>Written by Sundar Rajan L, Venkatesh </remarks>

    [RoutePrefix("api/AgrTrnCCMailApproval")]
    [AllowAnonymous]
    public class AgrTrnCCMailApprovalController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrTrnCCMailApproval objDaAgrTrnCCMailApproval = new DaAgrTrnCCMailApproval();

        [ActionName("GetApprovalMailList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalMailList(string approval_token)
        {
            MdlMstCCschedule objvalues = new MdlMstCCschedule();
            objDaAgrTrnCCMailApproval.DaGetApprovalMailList(approval_token, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("PostCCMailApproved")]
        [HttpPost]
        public HttpResponseMessage PostCCMailApproved(MdlMstCCschedule values)
        {

            objDaAgrTrnCCMailApproval.DaPostCCMailApproved(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}