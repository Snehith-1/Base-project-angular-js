using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.idas.Models;
using ems.idas.DataAccess;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasMstDocList")]
    [Authorize]
    public class IdasMstDocListController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaDocList objDaDocList = new DaDocList();
        result objResult = new result();

        [ActionName("GetDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetDocumentList()
        {
            DocumentList  objDocumentList = new DocumentList();
            objDaDocList.DaGetDocList(objDocumentList);
            return Request.CreateResponse(HttpStatusCode.OK, objDocumentList);
        }
        [ActionName ("PostCreationDocList")]
        [HttpPost]
        public HttpResponseMessage PostCreationDocList(IDASDocument values)
        {
           
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocList.DaPostCreationDocList(values ,getsessionvalues .user_gid ,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("PostDocDelete")]
        [HttpGet]
        public HttpResponseMessage PostDocDelete(string doclist_gid)
        {
           
           objDaDocList.DaPostDelete (doclist_gid ,objResult );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetEditDocList")]
        [HttpGet]
        public HttpResponseMessage GetEditDocList(string doclist_gid)
        {
            IDASDocument values = new IDASDocument();
            objDaDocList.DaGetEditDocList(values, doclist_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateDoc")]
        [HttpPost]
        public HttpResponseMessage PostUpdateDoc(IDASDocument values)
        {
          
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocList.DaPostUpdateDoc(values,getsessionvalues .user_gid,objResult );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Document List Export
        [ActionName("ExportDocument")]
        [HttpGet]
        public HttpResponseMessage GetDocumentData()
        {
            documentSummary objdocumentsummary = new documentSummary();
            objDaDocList.DaGetDocumentData(objdocumentsummary);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentsummary);
        }
        // Template Updation
        [ActionName("PostDocTemplate")]
        [HttpPost]
        public HttpResponseMessage PostDocTemplate(IDASDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocList.DaPostDocTemplate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Doc Template Content
        [ActionName("GetDocTemplateContent")]
        [HttpGet]
        public HttpResponseMessage GetDocTemplateContent(string doclist_gid)
        {
            IDASDocument values = new IDASDocument();
            objDaDocList.DaGetDocTemplateContent(values, doclist_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocument2SanctionList")]
        [HttpGet]
        public HttpResponseMessage GetDocument2SanctionList(string sanction_gid)
        {
            DocumentList objDocumentList = new DocumentList();
            objDaDocList.DaGetDocument2SanctionList(sanction_gid, objDocumentList);
            return Request.CreateResponse(HttpStatusCode.OK, objDocumentList);
        }
        // Document List Template Generation
        [ActionName("PostDocTemplateGenerate")]
        [HttpPost]
        public HttpResponseMessage PostDocTemplateGenerate(IDASDocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocList.DaPostDocTemplateGenerate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
               
        [ActionName("GetEditDoc2sanction")]
        [HttpGet]
        public HttpResponseMessage GetEditDoc2sanction(string document_gid, string sanction_gid)
        {
            IDASDocument values = new IDASDocument();
            objDaDocList.DaGetEditDoc2sanction(values, document_gid, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocWordGenerate")]
        [HttpGet]
        public HttpResponseMessage GetDocWordGenerate(string documentlist_gid, string sanction_gid)
        {
            template_list values = new template_list();
            objDaDocList.DaGetDocWordGenerate(documentlist_gid, sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGeneratedDocList")]
        [HttpGet]
        public HttpResponseMessage GetGeneratedDocList()
        {
            DocumentList objDocumentList = new DocumentList();
            objDaDocList.DaGetGeneratedDocList(objDocumentList);
            return Request.CreateResponse(HttpStatusCode.OK, objDocumentList);
        }
    }
}
