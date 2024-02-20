using ems.masterng.DataAccess;
using ems.masterng.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.masterng.Controllers
    {
        [RoutePrefix("api/MstNgApplicationEdit")]
        [Authorize]

        public class MstNgApplicationEditController : ApiController
        {
        DaMstNgApplicationEdit objMstNgApplicationEdit = new DaMstNgApplicationEdit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("GetEditProductDetailList")]
        [HttpGet]
        public HttpResponseMessage GetProductDetailList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstNgApplicationEdit values = new MdlMstNgApplicationEdit();
            objMstNgApplicationEdit.DaGetEditProductDetailList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditProductDetailAdd")]
        [HttpPost]
        public HttpResponseMessage PostEditProductDetailAdd(MdlMstNgApplicationEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationEdit.DaPostEditProductDetailAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditAppBasicDetail")]
        [HttpGet]
        public HttpResponseMessage EditAppBasicDetail(string application_gid)
        {
            MdlMstNgApplicationEdit values = new MdlMstNgApplicationEdit();
            objMstNgApplicationEdit.DaEditAppBasicDetail(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateAppBasicDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateAppBasicDetail(MdlMstNgApplicationEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationEdit.DaUpdateAppBasicDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitution")]
        [HttpGet]
        public HttpResponseMessage GetInstitution(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstNgApplicationEdit values = new MdlMstNgApplicationEdit();
            objMstNgApplicationEdit.DaGetInstitution(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
    }
