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
    [RoutePrefix("api/sanction")]
    [Authorize]

    public class sanctionController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSanction objDaSanction = new DaSanction();

        [ActionName("postsanctiondetails")]
        [HttpPost]
        public HttpResponseMessage PostSanctionDetails(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("postsanctiondocument")]
        [HttpPost]
        public HttpResponseMessage postsanctiondocument(sanctiondcoument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostsanctiondocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postUploadSanctionDocument")]
        [HttpPost]
        public HttpResponseMessage postUploaddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadidassanctiondocument values = new uploadidassanctiondocument();            
            objDaSanction.DaPostUploadSanctionDocument(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("postUploadidasSanctionDocument")]
        [HttpPost]
        public HttpResponseMessage postUploadidasSanctionDocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadidassanctiondocument documentname = new uploadidassanctiondocument();
            objDaSanction.DapostUploadidasSanctionDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("postUploadrskdocument")]
        [HttpPost]
        public HttpResponseMessage postUploadrskdocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadrsksanctiondocument documentname = new uploadrsksanctiondocument();
            objDaSanction.DaPostrsksanctiondocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
            
        }

        [ActionName("UploadDocDelete")]
        [HttpGet]
        public HttpResponseMessage UploadDocDelete(string document_gid)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            result objResult = new result();
            objDaSanction.DaPostUploadDocDelete(document_gid,getsessionvalues .user_gid ,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);

        }

        [ActionName("postUploadidasrskdocument")]
        [HttpPost]
        public HttpResponseMessage postUploadidasrskdocument(uploadidassanctiondocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostidasrsksanctiondocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("getsanctiondtlList")]
        [HttpGet]
        public HttpResponseMessage GetSanctionDtlList()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaGetSanctionDtlList(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("getsanctiondelete")]
        [HttpGet]
        public HttpResponseMessage getsanctiondelete(string customer2sanction_gid)
        {
            resultsample objresult = new resultsample();
            objDaSanction.DaGetSanctionDelete(customer2sanction_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("getsanctiondetails")]
        [HttpGet]
        public HttpResponseMessage getsanctiondetails(string customer2sanction_gid)
        {
            sanctiondetails objsanctiondetailsList = new sanctiondetails();
            objDaSanction.DaGetSanctionDetails(customer2sanction_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("postsanctionupdate")]
        [HttpPost]
        public HttpResponseMessage postsanctionupdate(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIdasSanctionDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetIdasSanctionDocumentList(string customer2sanction_gid)
        {
            idassanctiondocumentList objsanctiondocumentList = new idassanctiondocumentList();
            objDaSanction.DaGetIdasSanctionDocumentList(customer2sanction_gid,objsanctiondocumentList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondocumentList);
        }


        [ActionName("GetRskSanctionDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetRskSanctionDocumentList(string customer2sanction_gid)
        {
            rsksanctiondocumentList objsanctiondocumentList = new rsksanctiondocumentList();
            objDaSanction.DaGetRskSanctionDocumentList(customer2sanction_gid, objsanctiondocumentList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondocumentList);
        }

       
        [ActionName("postexcelupload")]
        [HttpPost]
        public HttpResponseMessage PostExcelUpload()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            resultsample objsanctiondetails = new resultsample();
            objDaSanction.DaPostExcelUpload(httpRequest, getsessionvalues.employee_gid, objsanctiondetails);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetails);
        }


        [ActionName("GetSanctionDtls")]
        [HttpGet]
        public HttpResponseMessage GetSanctionDtls(string sanction_gid)
        {
            sanctionviewdtl values = new sanctionviewdtl();
            objDaSanction.DaGetSanctionDtls(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //public IHttpActionResult Save(List<EmployeeTable> employeeList)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    foreach (var data in employeeList)
        //    {
        //        db.EmployeeTables.Add(data);
        //    }
        //    db.SaveChanges();

        //    return StatusCode(HttpStatusCode.OK);

        //}



    }
}