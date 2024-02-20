using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.lgl.Models;
using ems.lgl.DataAccess;

namespace StoryboardAPI.Controllers.ems.lgl
{
    [RoutePrefix("api/lawFirm")]
    [Authorize]
    public class lawFirmController : ApiController
    {
        DaLawfirm objlmsdataaccess = new DaLawfirm();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("createLawfirm")]
        [HttpPost]
        public HttpResponseMessage postcreateLawfirm(MdlLawfirm values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.PostLawfirm(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("lawfirmdetail")]
        [HttpGet]
        public HttpResponseMessage getlawfirmdetail()
        {
            mdllawfirmdtl objLawfirm = new mdllawfirmdtl();
            objlmsdataaccess.DaGetLawFirmDetail(objLawfirm);           
            return Request.CreateResponse(HttpStatusCode.OK, objLawfirm);
        }

        [ActionName("Getlawfirmdetails")]
        [HttpGet]
        public HttpResponseMessage getlawfirmdetails(string lawfirm_gid)
        {
            lawfirmedit values = new lawfirmedit();
            objlmsdataaccess.DaGetFirmDetails(getsessionvalues.user_gid , lawfirm_gid, values);           
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updateLawfirm")]
        [HttpPost]
        public HttpResponseMessage postupdatecustomer(lawfirmedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.DaPostUpdateLawFirm(getsessionvalues.employee_gid,getsessionvalues.user_gid, values);        
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawfirmDelete")]
        [HttpGet]
        public HttpResponseMessage getlawfirmdelete(string lawfirm_gid)
        {
            lawfirmedit values = new lawfirmedit();
            objlmsdataaccess.DaGetLawFirm(lawfirm_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Lawfirmtag")]
        [HttpPost]
        public HttpResponseMessage getlawfirmtag(MdlLawfirm values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.DaGetLawfirmTag(getsessionvalues.employee_gid, values);          
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tag")]
        [HttpGet]
        public HttpResponseMessage getlawfirm(string lawfirm_gid)
        {
            MdlLawfirm values = new MdlLawfirm();
            objlmsdataaccess.DaGetLaw(lawfirm_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawfirmView")]
        [HttpGet]
        public HttpResponseMessage getlawfirmView(string lawfirm_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Lawfirmupload values = new Lawfirmupload();
            objlmsdataaccess.DaGetLawFirmView(lawfirm_gid, values, getsessionvalues.user_gid); 
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawfirmDetails")]
        [HttpGet]
        public HttpResponseMessage getlawfirmDetails(string lawfirm_gid)
        {
            Lawfirmupload objLawfirmdtl = new Lawfirmupload();
            objlmsdataaccess.DaGetLawFirmDetails(objLawfirmdtl, lawfirm_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objLawfirmdtl);
        }
        [ActionName("Uploadbankcertificate")]
        [HttpPost]
        public HttpResponseMessage postuploadbankcertificate()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Lawfirmupload documentname = new Lawfirmupload();
            objlmsdataaccess.DaUploadEnrollCertificate(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);  
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Editploadbankcertificate")]
        [HttpPost]
        public HttpResponseMessage Editploadbankcertificate()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Lawfirmupload documentname = new Lawfirmupload();
            objlmsdataaccess.DaEditUploadEnrollcertificate_da(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);          
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("documentdelete")]
        [HttpGet]
        public HttpResponseMessage getdocumentname(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objlmsdataaccess.DaGetDocumentCancel(document_gid, objdocumentcancel);            
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("lawfirm2lawyer")]
        [HttpGet]
        public HttpResponseMessage getlawfirm2lawyer()
        {
            Lawfirm2lawyer values = new Lawfirm2lawyer();
            objlmsdataaccess.DaGetLawFirm2Lawyer(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempdelete()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objlmsdataaccess.DaGetTempDelete(getsessionvalues.user_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("postmembers")]
        [HttpPost]
        public HttpResponseMessage postmembers(mdlmembername values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.Dapostpostmembers(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Gettempmember")]
        [HttpGet]
        public HttpResponseMessage Gettempmember()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmembername objmdlmembername = new mdlmembername();
            objlmsdataaccess.DaGettempmember(getsessionvalues.employee_gid, objmdlmembername);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlmembername);
        }
        [ActionName("memberdelete")]
        [HttpGet]
        public HttpResponseMessage memberdelete(string lawfirmmember_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objlmsdataaccess.Damemberdelete(lawfirmmember_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("getlawfirm2member")]
        [HttpGet]
        public HttpResponseMessage getlawfirm2member(string lawfirm_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmembername objmdlmembername = new mdlmembername();
            objlmsdataaccess.Dagetlawfirm2member(lawfirm_gid, objmdlmembername);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlmembername);
        }
        [ActionName("getEditlawfirm2member")]
        [HttpGet]
        public HttpResponseMessage getEditlawfirm2member(string lawfirm_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmembername objmdlmembername = new mdlmembername();
            objlmsdataaccess.DagetEditlawfirm2member(getsessionvalues.employee_gid, lawfirm_gid, objmdlmembername);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlmembername);
        }
        //---------------Lawfirm Account Creation--------------//
        [ActionName("lawfirmlogincreation")]
        [HttpPost]
        public HttpResponseMessage postlawfirmlogincreation(lawfirmlogin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.Dapostlawfirmlogincreation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("lawfirmactivationstatus")]
        [HttpPost]
        public HttpResponseMessage Postlawfirmactivationstatus(lawfirmlogin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.DaPostlawfirmactivationstatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("viewmember")]
        [HttpGet]
        public HttpResponseMessage getviewmember(string lawfirm_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmembername objmdlmembername = new mdlmembername();
            objlmsdataaccess.DaGetviewmember(getsessionvalues.employee_gid, lawfirm_gid, objmdlmembername);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlmembername);
        }
        [ActionName("TagLawyer2lawfirm")]
        [HttpPost]
        public HttpResponseMessage TagLawyer2lawfirm(mdlmembername values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlmsdataaccess.DaTagLawyer2lawfirm(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Checking Enrolment Number Duplication---------------//
        [ActionName("GetLawfirmenrolment_validation")]
        [HttpGet]
        public HttpResponseMessage GetLawfirmenrolment_validation(string enrolment_no)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmembername objmdlRegisterlawyer = new mdlmembername();
            objlmsdataaccess.DaGetLawfirmenrolment_validation(objmdlRegisterlawyer, enrolment_no);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlRegisterlawyer);
        }
    }
}
