using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.marketing.Models;
using ems.marketing.DataAccess;

namespace ems.marketing.Controllers
{
    [RoutePrefix("api/MarketingDashboard")]
    [Authorize]
    public class MarketingDashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMarketingDashboard objDaAccess = new DaMarketingDashboard();

        [ActionName("GetNewTabDtl")]
        [HttpGet]
        public HttpResponseMessage GetNewTabDtl()
        {
            MdlNewTab values = new MdlNewTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetNewTabDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFollowUpTabDtl")]
        [HttpGet]
        public HttpResponseMessage GetFollowUpTabDtl()
        {
            MdlFollowUpTab values = new MdlFollowUpTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetFollowUpTabDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProspectTabDtl")]
        [HttpGet]
        public HttpResponseMessage GetProspectTabDtl()
        {
            MdlProspectTab values = new MdlProspectTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetProspectTabDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPotentialTabDtl")]
        [HttpGet]
        public HttpResponseMessage GetPotentialTabDtl()
        {
            MdlPotentialTab values = new MdlPotentialTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetPotentialTabDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDropTabDtl")]
        [HttpGet]
        public HttpResponseMessage GetDropTabDtl()
        {
            MdlDropTab values = new MdlDropTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetDropTabDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllTabDtl")]
        [HttpGet]
        public HttpResponseMessage GetAllTabDtl()
        {
            MdlAllTab values = new MdlAllTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetAllTabDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDashboardCount")]
        [HttpGet]
        public HttpResponseMessage GetDashboardCount()
        {
            MdlDashboardCount values = new MdlDashboardCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetDashboardCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Individual Employee Count
        [ActionName("GetOverAllLeadCount")]
        [HttpGet]
        public HttpResponseMessage GetOverAllLeadCount()
        {
            MdlDashboardCount objvalues = new MdlDashboardCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetOverAllLeadCount(getsessionvalues.employee_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("GetOverAllCount")]
        [HttpGet]
        public HttpResponseMessage GetOverAllCount()
        {
            MdlNewTab objvalues = new MdlNewTab();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetOverAllCount(getsessionvalues.employee_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
    }
}
