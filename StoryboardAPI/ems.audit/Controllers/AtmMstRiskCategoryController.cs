using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.audit.Controllers
{
    [RoutePrefix("api/AtmMstRiskCategory")]
    [Authorize]
    public class AtmMstRiskCategoryController : ApiController
    {
        DaAtmMstRiskCategory objDaAtmMstRiskCategory = new DaAtmMstRiskCategory();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetRiskCategory")]
        [HttpGet]
        public HttpResponseMessage GetRiskCategory()
        {
            MdlAtmMstRiskCategory values = new MdlAtmMstRiskCategory();
            objDaAtmMstRiskCategory.DaGetRiskCategory(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateRiskCategory")]
        [HttpPost]
        public HttpResponseMessage CreateRiskCategory(MdlAtmMstRiskCategory values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstRiskCategory.DaCreateRiskCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditRiskCategory")]
        [HttpGet]
        public HttpResponseMessage EditRiskCategory(string riskcategory_gid)
        {
            MdlAtmMstRiskCategory values = new MdlAtmMstRiskCategory();
            objDaAtmMstRiskCategory.DaEditRiskCategory(riskcategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateRiskCategory")]
        [HttpPost]
        public HttpResponseMessage UpdateRiskCategory(MdlAtmMstRiskCategory values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstRiskCategory.DaUpdateRiskCategory(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveRiskCategory")]
        [HttpPost]
        public HttpResponseMessage InactiveRiskCategory(MdlAtmMstRiskCategory values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstRiskCategory.DaInactiveRiskCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRiskCategory")]
        [HttpGet]
        public HttpResponseMessage DeleteRiskCategory(string riskcategory_gid)
        {
            MdlAtmMstRiskCategory values = new MdlAtmMstRiskCategory();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmMstRiskCategory.DaDeleteRiskCategory(riskcategory_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RiskCategoryInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage RiskCategoryInactiveLogview(string riskcategory_gid)
        {
            MdlAtmMstRiskCategory values = new MdlAtmMstRiskCategory();
            objDaAtmMstRiskCategory.DaRiskCategoryInactiveLogview(riskcategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRiskCategoryActive")]
        [HttpGet]
        public HttpResponseMessage GetRiskCategoryActive()
        {
            MdlAtmMstRiskCategory values = new MdlAtmMstRiskCategory();
            objDaAtmMstRiskCategory.DaGetRiskCategoryActive(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}