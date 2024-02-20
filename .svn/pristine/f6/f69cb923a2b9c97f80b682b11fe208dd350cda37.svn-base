using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Functions;
using ems.vp.Models;
using System.Web;
using ems.utilities.Models;
using ems.vp.DataAccess;
namespace StoryboardAPI.Controllers.ems.vendorPortal
{
    [RoutePrefix("api/readyToRelease")]
    [Authorize]
    public class readyToReleaseController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaReadyToRelease objDaReadyToRelease = new DaReadyToRelease();

        [ActionName("readytoRelease")]
        [HttpGet]
        public HttpResponseMessage GetReleasesData()
        {
            releaseData objReleaseData = new releaseData();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaReadyToRelease.DaGetReleaseData(objReleaseData, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objReleaseData);
        }

        [ActionName("Release")]
        [HttpPost]
        public HttpResponseMessage PostRelease(statusUpdate values)
        {
            statusUpdate objstatusUpdate = new statusUpdate();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaReadyToRelease.DaPostRelease(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Upload Document...//

        [ActionName("uatuploaddocument")]
        [HttpPost]
        public HttpResponseMessage PostUatDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            var status = objDaReadyToRelease.DaPostUatDocument(httpRequest, getsessionvalues.employee_gid, getsessionvalues.user_gid, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("tmpuatdocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpUatDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objtmpuatdocument = new UploadDocumentname();
            objDaReadyToRelease.DaGetTmpUatDocument(objtmpuatdocument, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objtmpuatdocument);
        }

        [ActionName("documentcancel")]
        [HttpPost]
        public HttpResponseMessage PostDocumentCancel(UploadDocumentcancel values)
        {
            objDaReadyToRelease.DaPostDocumentCancel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Temp Document Clear ..//

        [ActionName("tmpuatdocumentclear")]
        [HttpGet]
        public HttpResponseMessage GetTmpDocumentClear()
        {
            tmpdocumentclear objtmpclearDocument = new tmpdocumentclear();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaReadyToRelease.DaGetTmpDocumentClear(getsessionvalues.user_gid, objtmpclearDocument);
            return Request.CreateResponse(HttpStatusCode.OK, objtmpclearDocument);
        }

    }
}
