using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rms.Models;
using ems.rms.DataAccess;

namespace ems.rms.Controllers
{
    [RoutePrefix("api/RmsTrnRecruitment")]
    [Authorize]
    public class RmsTrnRecruitmentController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRmsTrnRecruitment objDaDocList = new DaRmsTrnRecruitment();
        [ActionName("GetRecruitmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetDocumentList()
        {
            DocumentList objDocumentList = new DocumentList();
            objDaDocList.DaGetDocList(objDocumentList);
            return Request.CreateResponse(HttpStatusCode.OK, objDocumentList);
        }
    }
}
