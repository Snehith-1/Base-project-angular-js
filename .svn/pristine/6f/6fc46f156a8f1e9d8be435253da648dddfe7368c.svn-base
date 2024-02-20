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

namespace StoryboardAPI.Controllers.ems.rsk
{

    [RoutePrefix("api/externalVendor")]
    [Authorize]

    public class externalVendorController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaExternalVendor objDaExternalVendor = new DaExternalVendor();

        [ActionName("postexternalRegistration")]
        [HttpPost]
        public HttpResponseMessage PostExternalRegistration(externalvendordtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaExternalVendor.DaPostExternalRegistration(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getexternalRegisterdtl")]
        [HttpGet]
        public HttpResponseMessage GetExternalRegisterdtl(string externalregister_gid)
        {
            externalvendordtl objexternalvendordtl = new externalvendordtl();
            objDaExternalVendor.DaGetExternalRegisterdtl(externalregister_gid, objexternalvendordtl);
            return Request.CreateResponse(HttpStatusCode.OK, objexternalvendordtl);
        }

        [ActionName("updateexternalRegistration")]
        [HttpPost]
        public HttpResponseMessage PostUpdateExternalRegistration(externalvendordtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaExternalVendor.DaPostUpdateExternalRegistration(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getexternalRegistersummary")]
        [HttpGet]
        public HttpResponseMessage GetexternalRegisterSummary()
        {
            externalvendorList objexternalvendorList = new externalvendorList();
            objDaExternalVendor.DaGetexternalRegisterSummary(objexternalvendorList);
            return Request.CreateResponse(HttpStatusCode.OK, objexternalvendorList);
        }

        [ActionName("getexternallogindtl")]
        [HttpGet]
        public HttpResponseMessage GetExternalLoginDtl(string externalregister_gid)
        {
            externalVendorlogin objexternalvendordtl = new externalVendorlogin();
            objDaExternalVendor.DaGetExternalLoginDtl(externalregister_gid, objexternalvendordtl);
            return Request.CreateResponse(HttpStatusCode.OK, objexternalvendordtl);
        }

        [ActionName("postExternalLogin")]
        [HttpPost]
        public HttpResponseMessage PostExternalLogin(externalVendorlogin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaExternalVendor.DaPostExternalLogin(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postExternalLoginStatus")]
        [HttpPost]
        public HttpResponseMessage PostExternalLoginStatus(externalVendorlogin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaExternalVendor.DaPostExternalLoginStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExternalphotoUpload")]
        [HttpPost]
        public HttpResponseMessage PostUploadDocument()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            externalphoto documentname = new externalphoto();
            objDaExternalVendor.DaPostUploadDocument(httpRequest, getsessionvalues.employee_gid, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("tmpexternalphotoclear")]
        [HttpGet]
        public HttpResponseMessage GetTmpExternalPhotoClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            document values = new document();
            objDaExternalVendor.DaGetTmpExternalPhotoClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
