using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rsk.Models;
using ems.rsk.DataAccess;


namespace ems.rsk.Controllers
{
    [RoutePrefix("api/VisitReportCancel")]
    [Authorize]

    public class VisitReportCancelController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaVisitReportCancel objDaVisitReportCancel = new DaVisitReportCancel();

        [ActionName("PostCancelReport")]
        [HttpPost]
        public HttpResponseMessage PostCancelReport(visitreportcancel values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReportCancel.DaPostCancelReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCancelReportSubmit")]
        [HttpPost]
        public HttpResponseMessage PostCancelReportSubmit(visitreport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitReportCancel.DaPostCancelReportSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitCancelLog")]
        [HttpGet]
        public HttpResponseMessage GetVisitCancelLog(string allocationdtl_gid)
        {
            visistreportcancelList values = new visistreportcancelList();
            objDaVisitReportCancel.DaGetVisitCancelLog(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExternalVisitCancelLog")]
        [HttpGet]
        public HttpResponseMessage GetExternalVisitCancelLog(string allocationdtl_gid)
        {
            visistreportcancelList values = new visistreportcancelList();
            objDaVisitReportCancel.DaGetExternalVisitCancelLog(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostVisitStatus")]
        [HttpPost]
        public HttpResponseMessage PostVisitStatus(visitstatus values)
        {
            objDaVisitReportCancel.DaPostVisitStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
