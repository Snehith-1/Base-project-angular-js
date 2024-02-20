using ems.hrloan.DataAccess;
using ems.hrloan.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.hrloan.Controllers
{
    [RoutePrefix("api/MstHRLoanHRDocument")]
    [Authorize]
    public class MstHRLoanHRDocumentController : ApiController
    {
        DaMstHRLoanHRDocument objDaMstHRLoanHRDocument = new DaMstHRLoanHRDocument();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // HR Document
        [ActionName("GetHRDocument")]
        [HttpGet]
        public HttpResponseMessage GetHRDocument()
        {
            MdlMstHRLoanHRDocument objhrloanhrdocument = new MdlMstHRLoanHRDocument();
            objDaMstHRLoanHRDocument.DaGetHRDocument(objhrloanhrdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objhrloanhrdocument);
        }

        [ActionName("CreateHRDocument")]
        [HttpPost]
        public HttpResponseMessage CreateHRDocument(hrdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRDocument.DaCreateHRDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHRDocumentDropDown")]
        [HttpGet]
        public HttpResponseMessage GetHRDocumentDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanHRDocument values = new MdlMstHRLoanHRDocument();
            objDaMstHRLoanHRDocument.DaGetHRDocumentDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditHRDocument")]
        [HttpGet]
        public HttpResponseMessage EditHRDocument(string hrdocument_gid)
        {
            hrdocument values = new hrdocument();
            objDaMstHRLoanHRDocument.DaEditHRDocument(hrdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateHRDocument")]
        [HttpPost]
        public HttpResponseMessage UpdateHRDocument(hrdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRDocument.DaUpdateHRDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRDocument")]
        [HttpPost]
        public HttpResponseMessage InactiveHRDocument(hrloanhrdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRDocument.DaInactiveHRDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveHRDocumentHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveHRDocumentHistory(string hrdocument_gid)
        {
            HRLoanHRDocumentInactiveHistory objhrdocumenthistory = new HRLoanHRDocumentInactiveHistory();
            objDaMstHRLoanHRDocument.DaInactiveHRDocumentHistory(objhrdocumenthistory, hrdocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objhrdocumenthistory);
        }
        [ActionName("DeleteHRDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteHRDocument(string hrdocument_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRDocument.DaDeleteHRDocument(hrdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //CHECK LIST
        
        [ActionName("GetHRDocumentCheckList")]
        [HttpGet]
        public HttpResponseMessage GetHRDocumentCheckList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanHRDocument values = new MdlMstHRLoanHRDocument();
            objDaMstHRLoanHRDocument.DaGetHRDocumentCheckList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateHRDocumentCheckList")]
        [HttpPost]
        public HttpResponseMessage CreateHRDocumentCheckList(checklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstHRLoanHRDocument.DaCreateHRDocumentCheckList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteHRDocumentCheckList")]
        [HttpGet]
        public HttpResponseMessage DeleteHRDocumentCheckList(string hrdocumentchecklist_gid)
        {
            variety values = new variety();
            objDaMstHRLoanHRDocument.DaDeleteHRDocumentCheckList(hrdocumentchecklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetHRDocumentCheckListEditList")]
        [HttpGet]
        public HttpResponseMessage GetHRDocumentCheckListEditList(string hrdocument_gid)
        {
            MdlMstHRLoanHRDocument values = new MdlMstHRLoanHRDocument();
            objDaMstHRLoanHRDocument.DaGetHRDocumentCheckListEditList(hrdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetHRDocumentCheckListTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetHRDocumentCheckListTempEditList(string hrdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstHRLoanHRDocument values = new MdlMstHRLoanHRDocument();
            variety objvalues = new variety();
            objDaMstHRLoanHRDocument.DaGetHRDocumentCheckListTempEditList(hrdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HRDocumentCheckListTempClear")]
        [HttpGet]
        public HttpResponseMessage HRDocumentCheckListTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            variety objvalues = new variety();
            objDaMstHRLoanHRDocument.DaHRDocumentCheckListTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}