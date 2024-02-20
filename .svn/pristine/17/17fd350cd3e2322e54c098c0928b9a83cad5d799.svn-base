using ems.foundation.DataAccess;
using ems.foundation.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.foundation.Controllers
{
    [RoutePrefix("api/FndMstCategoryTypeMaster")]
    [Authorize]
    public class FndMstCategoryTypeMasterController : ApiController
    {
        DaFndMstCategoryTypeMaster objDaFndMstCategoryTypeMaster = new DaFndMstCategoryTypeMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetCategoryType")]
        [HttpGet]
        public HttpResponseMessage GetCategoryType()
        {
            MdlFndMstCategoryTypeMaster values = new MdlFndMstCategoryTypeMaster();
            objDaFndMstCategoryTypeMaster.DaGetCategoryType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCategoryType")]
        [HttpPost]
        public HttpResponseMessage CreateCategoryType(MdlFndMstCategoryTypeMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCategoryTypeMaster.DaCreateCategoryType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCategoryType")]
        [HttpGet]
        public HttpResponseMessage EditCategoryType(string categorytype_gid)
        {
            MdlFndMstCategoryTypeMaster values = new MdlFndMstCategoryTypeMaster();
            objDaFndMstCategoryTypeMaster.DaEditCategoryType(categorytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCategoryType")]
        [HttpPost]
        public HttpResponseMessage UpdateCategoryType(MdlFndMstCategoryTypeMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCategoryTypeMaster.DaUpdateCategoryType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCategoryType")]
        [HttpPost]
        public HttpResponseMessage InactiveCategoryType(MdlFndMstCategoryTypeMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCategoryTypeMaster.DaInactiveCategoryType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCategoryType")]
        [HttpGet]
        public HttpResponseMessage DeleteCategoryType(string categorytype_gid)
        {
            MdlFndMstCategoryTypeMaster values = new MdlFndMstCategoryTypeMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCategoryTypeMaster.DaDeleteCategoryType(categorytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CategoryTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CategoryTypeInactiveLogview(string categorytype_gid)
        {
            MdlFndMstCategoryTypeMaster values = new MdlFndMstCategoryTypeMaster();
            objDaFndMstCategoryTypeMaster.DaCategoryTypeInactiveLogview(categorytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}