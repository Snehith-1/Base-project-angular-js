using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System;

/// <summary>
/// (It's used for pages in CC Schedule Mail Approval)CCMailApproval Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{

    [RoutePrefix("api/MstCCMailApproval")]
    [AllowAnonymous]
    public class MstCCMailApprovalController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCCMailApproval objDaCCMailApproval = new DaCCMailApproval();

        [ActionName("GetApprovalMailList")]
        [HttpGet]
        public HttpResponseMessage GetApprovalMailList(string approval_token)
        {
            MdlMstCCschedule objvalues = new MdlMstCCschedule();
            objDaCCMailApproval.DaGetApprovalMailList(approval_token, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("PostCCMailApproved")]
        [HttpPost]
        public HttpResponseMessage PostCCMailApproved(MdlMstCCschedule values)
        {

            objDaCCMailApproval.DaPostCCMailApproved(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}