using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.IO;
using ems.storage.Functions;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstVisitor")]
    [Authorize]
    public class MstVisitorController : ApiController
    {
        DaMstVisitor objDaVisitor = new DaMstVisitor();
        session_values Objgetgid = new session_values();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        logintoken getsessionvalues = new logintoken();
        string path = string.Empty;

        [ActionName("Branch")]
        [HttpGet]
        public HttpResponseMessage Branch()
        {
            MdlBranchname objMdlBranch = new MdlBranchname();
            objDaVisitor.DaGetBranch(objMdlBranch);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlBranch);
        }

        [ActionName("PostVisitorMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostVisitorMobileNo(VisitorMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaPostVisitorMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitMobileNo")]
        [HttpGet]
        public HttpResponseMessage GetVisitMobileNo()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorMobileNo values = new VisitorMobileNo();
            objDaVisitor.DaGetVisitMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetVisitMobileNoList(string visitor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorMobileNo values = new VisitorMobileNo();
            objDaVisitor.DaGetVisitMobileNoList(getsessionvalues.employee_gid, values, visitor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisitMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeletevisitMmobileno(string visitor2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorMobileNo values = new VisitorMobileNo();
            objDaVisitor.DaDeleteVisitMobileNo(visitor2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Mail Address
        [ActionName("PostVisitorEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostVisitorEmailAddress(VisitorEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaPostVisitorEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitorEmailAddress")]
        [HttpGet]
        public HttpResponseMessage GetVisitorEmailAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorEmailAddress values = new VisitorEmailAddress();
            objDaVisitor.DaGetVisitorEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisitorEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteVisitorEmailAddress(string visitor2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorEmailAddress values = new VisitorEmailAddress();
            objDaVisitor.DaDeleteVisitorEmailAddress(visitor2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Visitor Name
        [ActionName("PostVisitorName")]
        [HttpPost]
        public HttpResponseMessage PostVisitorName(VisitorName values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaPostVisitorName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
   
        [ActionName("GetVisitorName")]
        [HttpGet]
        public HttpResponseMessage GetVisitorName()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorName values = new VisitorName();
            objDaVisitor.DaGetVisitorName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete name
        [ActionName("DeleteVisitorName")]
        [HttpGet]
        public HttpResponseMessage DeleteVisitorName(string visitorname_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorName values = new VisitorName();
            objDaVisitor.DaDeleteVisitorName(visitorname_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Temp Clear
        [ActionName("GetVisitorTempClear")]
        [HttpGet]
        public HttpResponseMessage GetVisitorTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaVisitor.DaGetVisitorTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CreateVisitor")]
        [HttpPost]
        public HttpResponseMessage CreateVisitor(Visitor values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaCreateVisitor(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("VisitPhotoUpload")]
        [HttpPost]
        public HttpResponseMessage VisitPhotoUpload()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            VisitorUploadphotoList values = new VisitorUploadphotoList();

            objDaVisitor.DaPostVisitorUploadPhoto(httpRequest, values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitPhotos")]
        [HttpGet]
        public HttpResponseMessage GetVisitPhotos()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVisitor values = new MdlVisitor();
            objDaVisitor.DaGetVisitPhotos(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteVisittmpPhotoList")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpPhotoList(string visitor2photo_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorUploadphotoList values = new VisitorUploadphotoList();
            objDaVisitor.DaDeleteVisittmpPhotoList(visitor2photo_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitor")]
        [HttpGet]
        public HttpResponseMessage GetVisitor()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVisitor objVisitor = new MdlVisitor();
            objDaVisitor.DaGetVisitor(objVisitor, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }
        //Delete Visitor
        [ActionName("DeleteVisitor")]
        [HttpGet]
        public HttpResponseMessage DeleteVisitort(string visitor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Visitor values = new Visitor();
            objDaVisitor.DaDeleteVisitor(visitor_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitorView")]
        [HttpGet]
        public HttpResponseMessage GetVisitorView(string visitor_gid)
        {
           
            Visitor objVisitor = new Visitor();
            objDaVisitor.DaGetVisitorView( objVisitor, visitor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }

        [ActionName("GetVisitorTagView")]
        [HttpGet]
        public HttpResponseMessage GetVisitorTagView(string visitorname_gid)
        {
            visitortag objVisitor = new visitortag();
            objDaVisitor.DaGetVisitorTagView( objVisitor, visitorname_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }

        [ActionName("GetVisitorNameList")]
        [HttpGet]
        public HttpResponseMessage GetVisitorNameList(string visitor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorName values = new VisitorName();
            objDaVisitor.DaGetVisitorNameList(getsessionvalues.employee_gid, values, visitor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitorEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetVisitorEmailAddressList(string visitorname_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorEmailAddress values = new VisitorEmailAddress();
            objDaVisitor.DaGetVisitorEmailAddressList(getsessionvalues.employee_gid, visitorname_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Update Visitor
        [ActionName("UpdateVisitor")]
        [HttpPost]
        public HttpResponseMessage UpdateVisitor(Visitor values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaUpdateVisitor(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Visitor Tag
        [ActionName("UpdateVisitorTag")]
        [HttpPost]
        public HttpResponseMessage UpdateVisitorTag(visitortag values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaUpdateVisitorTag(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Mobile No
        [ActionName("PostEditVisitorMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostEditVisitorMobileNo(VisitorMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaPostEditVisitorMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Edit Mail Address
        [ActionName("PostEditVisitorEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEditVisitorEmailAddress(VisitorEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaPostEditVisitorEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Edit Visitor Name
        [ActionName("PostEditVisitorName")]
        [HttpPost]
        public HttpResponseMessage PostEditVisitorName(VisitorName values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaPostEditVisitorName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitPhotosList")]
        [HttpGet]
        public HttpResponseMessage GetVisitPhotosList(string visitorname_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVisitor values = new MdlVisitor();
            objDaVisitor.DaGetVisitPhotoList(getsessionvalues.employee_gid, values, visitorname_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetVistorTagpdf")]
        //[HttpGet]
        //public IHttpActionResult GetVistorTagpdf(string visitor_gid)
        //{         
        //    var restClient = new RestClient(Path.Combine(ConfigurationManager.AppSettings["report_api_path"], "MstVisitorTag/getVisitorTagpdf"));
        //    var restRequest = new RestRequest(Method.GET);
        //    restRequest.AddQueryParameter("visitor_gid", visitor_gid);
        //    IRestResponse restResponse = restClient.Execute(restRequest);
        //    path = JsonConvert.DeserializeObject<string>(restResponse.Content);           
        //    return Ok(path);

        //}
        [ActionName("GetVistorTagpdf")]
        [HttpGet]
        public HttpResponseMessage GetVistorTagpdf(string visitor_gid)
        {
            lsa_doc objlsadoc = new lsa_doc();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "MstVisitorTag/getVisitorTagpdf?visitor_gid=" + visitor_gid);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            string path = JsonConvert.DeserializeObject<string>(response.Content);
            var pathArray = path.Split(new string[] { "EMS/" }, StringSplitOptions.None);
            var fileNameArrray = path.Split(new string[] { "Report/" }, StringSplitOptions.None);
            objlsadoc.file_path = pathArray[1].ToString();
            objlsadoc.file_name = fileNameArrray[1].ToString();
            //string enc_patharray = objcmnstorage.EncryptData(patharray1);
            ls_response = objcmnstorage.DaFileUploadDocument(objlsadoc.file_path);
            objlsadoc.file_path = objcmnstorage.EncryptData(objlsadoc.file_path);
            var byName = (IDictionary<string, object>)ls_response;
            objlsadoc.status = (bool)byName["status"];
            return Request.CreateResponse(HttpStatusCode.OK, objlsadoc);
        }

        [ActionName("GetTaggedVisitor")]
        [HttpGet]
        public HttpResponseMessage GetTaggedVisitor()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVisitor objVisitor = new MdlVisitor();
            objDaVisitor.DaGetTaggedVisitor(objVisitor, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }
        [ActionName("GetHistoryVisitor")]
        [HttpGet]
        public HttpResponseMessage GetHistoryVisitor()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVisitor objVisitor = new MdlVisitor();
            objDaVisitor.DaGetHistoryVisitor(objVisitor, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }
        [ActionName("VisitorCount")]
        [HttpGet]
        public HttpResponseMessage AssignApplicationCount()
        {
            VisitorCount values = new VisitorCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaVisitorCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitorNameViewList")]
        [HttpGet]
        public HttpResponseMessage GetVisitorNameViewList(string visitor_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            VisitorNameView values = new VisitorNameView();
            objDaVisitor.DaGetVisitorNameViewList(getsessionvalues.employee_gid, values, visitor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitorUploadDoc")]
        [HttpGet]
        public HttpResponseMessage GetVisitorUploadDoc(string visitorname_gid)
        {

            MdlMstvisitordocview values = new MdlMstvisitordocview();
            objDaVisitor.DaGetVisitorUploadDoc(visitorname_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitorManage")]
        [HttpGet]
        public HttpResponseMessage GetVisitorManage(string branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVisitor objVisitor = new MdlVisitor();
            objDaVisitor.DaGetVisitorManage(objVisitor,branch_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }

        //Auto Populate
        [ActionName("ShowVisitor")]
        [HttpPost]
        public HttpResponseMessage ShowVisitor(VisitorName objVisitor)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaVisitor.DaShowVisitor(objVisitor);
            return Request.CreateResponse(HttpStatusCode.OK, objVisitor);
        }

        // Visitor Report Excel
        [ActionName("VisitorExportExcel")]
        [HttpGet]
        public HttpResponseMessage VisitorExportExcel()
        {
            VisitorExport values = new VisitorExport();
            objDaVisitor.DaVisitorExportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
