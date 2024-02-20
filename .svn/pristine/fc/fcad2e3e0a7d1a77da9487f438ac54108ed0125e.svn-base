using ems.brs.Dataacess;
using ems.brs.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ems.brs.Controllers
{
    [RoutePrefix("api/SbiReconcillation")]
    [Authorize]
    public class SbiReconcillationController:ApiController
    {
        DaSbiReconcillation objDaIdfcReconcillation = new DaSbiReconcillation();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //BRS IDFC template excel
        [ActionName("BRSExcelSample")]
        [HttpPost]
        public HttpResponseMessage BRSExcelSample()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlSbiReconcillation documentname = new MdlSbiReconcillation();
            objDaIdfcReconcillation.DaPostBRSExcelSample(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
    }
}