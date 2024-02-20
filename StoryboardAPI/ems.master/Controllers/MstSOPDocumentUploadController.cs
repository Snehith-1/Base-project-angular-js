using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.master.Models;
using Newtonsoft.Json;
using System.Configuration;
using ems.master.DataAccess;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstSOPDocumentUpload")]
    [Authorize]
    public class MstSOPDocumentUploadController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstSPODocumentUpload objDaMstSPODocumentUploadt = new DaMstSPODocumentUpload();
       
        [ActionName("GetSOPdepartment_list")]
        [HttpGet]
        public HttpResponseMessage GetSOPdepartment_list()
        {
            MdlMstSOPdepartment_list values = new MdlMstSOPdepartment_list();
            objDaMstSPODocumentUploadt.DaGetSOPdepartment_list(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSOPDocument")]
        [HttpPost]
        public HttpResponseMessage PostSOPDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlSOPDocument_upload documentname = new MdlSOPDocument_upload();
            objDaMstSPODocumentUploadt.DaPostSOPDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetSOPDocumentSummary")]
        [HttpGet]
        public HttpResponseMessage GetSOPDocumentSummary()
        {
            MdlMstSOPdepartment_list values = new MdlMstSOPdepartment_list();
            objDaMstSPODocumentUploadt.DaGetSOPDocumentSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSOPdocument_list")]
        [HttpGet]
        public HttpResponseMessage GetSOPdocument_list(string sopdepartment_gid)
        {
            MdlSOPDocument_upload values = new MdlSOPDocument_upload();
            objDaMstSPODocumentUploadt.DaGetSOPdocument_list(values, sopdepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteSOPDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteSOPDocument(string sopdocumentupload_gid)
        {
            MdlMstSOPdepartment_list values = new MdlMstSOPdepartment_list();
            objDaMstSPODocumentUploadt.DaDeleteSOPDocument(sopdocumentupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetITflag")]
        [HttpGet]
        public HttpResponseMessage GetITflag()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstSOPdepartment_list values = new MdlMstSOPdepartment_list();
            objDaMstSPODocumentUploadt.DaGetITflag(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
