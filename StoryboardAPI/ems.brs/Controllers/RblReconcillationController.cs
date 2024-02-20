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
    [RoutePrefix("api/RblReconcillation")]
    [Authorize]
    public class RblReconcillationController:ApiController
    {
        DaRblReconcillation objDaRblReconcillation = new DaRblReconcillation();
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
            MdlRblReconcillation documentname = new MdlRblReconcillation();
            objDaRblReconcillation.DaPostBRSExcelSample(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
    }
}