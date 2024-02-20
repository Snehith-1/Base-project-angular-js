using ems.idas.DataAccess;
using ems.idas.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/idasMstDigitalSignature")]
    [Authorize]
    public class IdasMstDigitalSignatureController : ApiController
    {
        DaIdasMstDigitalSignature objDaAccess = new DaIdasMstDigitalSignature();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetEmployeeList")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeList()
        {
            MdlIdsMstDigitalSignature values = new MdlIdsMstDigitalSignature();
            objDaAccess.DaGetEmployeeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SignatureUpload")]
        [HttpPost]
        public HttpResponseMessage SignatureUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadSignature documentname = new uploadSignature();
            objDaAccess.SignatureUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetDigitalSignatureList")]
        [HttpGet]
        public HttpResponseMessage GetDigitalSignatureList()
        {
            MdlIdsMstDigitalSignature values = new MdlIdsMstDigitalSignature();
            objDaAccess.DaGetDigitalSignatureList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSignature")]
        [HttpGet]
        public HttpResponseMessage DeleteSignature(string digitalsignature_gid)
        {
            MdlIdsMstDigitalSignature values = new MdlIdsMstDigitalSignature();
            objDaAccess.DaDeleteSignature(digitalsignature_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetSignatureView")]
        //[HttpGet]
        //public HttpResponseMessage GetSignatureView(string digitalsignature_gid)
        //{
        //    digitalsignaturelist values = new digitalsignaturelist();
        //    objDaAccess.DaGetSignatureView(digitalsignature_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
    }
}
