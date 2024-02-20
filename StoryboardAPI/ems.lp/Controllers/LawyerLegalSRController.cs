using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.lp.Models;
using ems.lp.DataAccess;
using ems.utilities.Functions;
using System.Web;

namespace ems.lp.Controllers
{
    [RoutePrefix("api/lawyerlegalSR")]
    [AllowAnonymous]
    public class LawyerLegalSRController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLawyerLegalSR objdaLawyerLegalSR = new DaLawyerLegalSR();

        [ActionName("GetraiselegalSR")]
        [HttpGet]
        public HttpResponseMessage getraiselegalSR()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            assignlegalSRList objlegalSR = new assignlegalSRList();
           objdaLawyerLegalSR.DaGetRaiseLegalSR(objlegalSR, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }


        [ActionName("UploadlegalsrDocument")]
        [HttpPost]
        public HttpResponseMessage uploadcorrecteddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocument_namesr documentname = new UploadDocument_namesr();
            objdaLawyerLegalSR.Uploadcorrecteddocument(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("documentlist")]
        [HttpGet]
        public HttpResponseMessage tmpdocumentdelete(string lawsr_gid)
        {

            document values = new document();
            objdaLawyerLegalSR.DaDocumentlist( lawsr_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("documentdelete")]
        [HttpGet]
        public HttpResponseMessage getdocumentname(string tmplegalsr_documentgid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            deleteDocument objdocumentcancel = new deleteDocument();
            objdaLawyerLegalSR.Dadocumentcancel(tmplegalsr_documentgid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

    }
}
