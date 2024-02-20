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
    [RoutePrefix("api/documentation")]
    [Authorize]

    public class documentationController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaDocumentation objDaDocumentation = new DaDocumentation();

        [ActionName("postdocumentationdtls")]
        [HttpPost]
        public HttpResponseMessage PostDocumentationDtls(documentationdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocumentation.DaPostDocumentationDtls(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getdocumentationdtlList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentationDtlList()
        {
            documentationlist objdocumentationlist = new documentationlist();
            objDaDocumentation.DaGetDocumentationDtlList(objdocumentationlist);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentationlist);
        }

        [ActionName("getrskdocumentationdtlList")]
        [HttpGet]
        public HttpResponseMessage GetrskDocumentationDtlList(string customer2sanction_gid)
        {
            documentationlist objdocumentationlist = new documentationlist();
            objDaDocumentation.DaGetrskDocumentationDtlList(customer2sanction_gid,objdocumentationlist);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentationlist);
        }
        [ActionName("getdocumentationdtl")]
        [HttpGet]
        public HttpResponseMessage GetDocumentationDtl(string customer2document_gid)
        {
            documentationdtl objdocumentationdtl = new documentationdtl();
            objDaDocumentation.DaGetDocumentationDtl(customer2document_gid, objdocumentationdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentationdtl);
        }

        [ActionName("documentationdelete")]
        [HttpGet]
        public HttpResponseMessage GetDocumentationDelete(string customer2document_gid)
        {
            resultsample objresult = new resultsample();
            objDaDocumentation.DaGetDocumentationDelete(customer2document_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("postdocumentationupdate")]
        [HttpPost]
        public HttpResponseMessage PostDocumentationUpdate(documentationdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocumentation.DaPostDocumentationUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
