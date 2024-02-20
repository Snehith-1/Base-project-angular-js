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
    [RoutePrefix("api/registerLawyer")]
    [Authorize]
    public class registerLawyerController : ApiController
    {
        DaRegisterlawyer objdaregisterlawyer = new DaRegisterlawyer();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //----Lawyer Summary Page-------------//
        [ActionName("lawyerdetail")]
        [HttpGet]
        public HttpResponseMessage getlawyerdetail()
        {
            mdllawyer objLawyer = new mdllawyer();      
            objdaregisterlawyer.DaGetLawyerDetail(objLawyer);
            return Request.CreateResponse(HttpStatusCode.OK, objLawyer);
        }
        //---------------Submit------------------//
        [ActionName("registerlawyer")]
        [HttpPost]
        public HttpResponseMessage postregisterlawyer(mdlRegisterlawyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaregisterlawyer.DaPostRegisterLawyer(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //------------View and Edit Information----------//
        [ActionName("Getlawyerdetails")]
        [HttpGet]
        public HttpResponseMessage getlawyerdetails(string lawyerregister_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            lawyeredit values = new lawyeredit();
            objdaregisterlawyer.DaGetLawyerDetails(getsessionvalues.user_gid, lawyerregister_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawyerUpdate")]
        [HttpPost]
        public HttpResponseMessage updatecustomer(lawyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaregisterlawyer.DaUpdateLawyer(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawyerDelete")]
        [HttpGet]
        public HttpResponseMessage getlawyerdelete(string lawyerregister_gid)
        {
            lawyeredit values = new lawyeredit();
            objdaregisterlawyer.DaLawyerDelete(lawyerregister_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawyerView")]
        [HttpGet]
        public HttpResponseMessage getlawyerView(string lawyerregister_gid)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname values = new UploadDocumentname();
            objdaregisterlawyer.DaGetLawyerView(lawyerregister_gid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadEnrollcertificate")]
        [HttpPost]
        public HttpResponseMessage PostuploadEnrollcertificate()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objdaregisterlawyer.DaUploadEnrollCertificate(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("EdituploadEnrollcertificate")]
        [HttpPost]
        public HttpResponseMessage EdituploadEnrollcertificate()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objdaregisterlawyer.DaEdituploadEnrollCertificate(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Uploadphoto")]
        [HttpPost]
        public HttpResponseMessage postuploadphoto()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objdaregisterlawyer.DaPostUploadPhoto(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("documentdelete")]
        [HttpGet]
        public HttpResponseMessage getdocumentname(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objdaregisterlawyer.DaGetDocumentCancel(document_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempdelete()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objdaregisterlawyer.DaGetTempDelete(getsessionvalues.user_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("lawyerlogincreation")]
        [HttpPost]
        public HttpResponseMessage postlawyerlogincreation(lawyerlogin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaregisterlawyer.DaPostLawyerLoginCreation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("lawyeractivationstatus")]
        [HttpPost]
        public HttpResponseMessage postlawyeractivationstatus(lawyerlogin values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaregisterlawyer.DaPostLawyerActivationStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("tempdocument")]
        [HttpGet]
        public HttpResponseMessage gettempdocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objdaregisterlawyer.DaGetTempDocument(getsessionvalues.user_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        //----------Checking Enrolment Number Duplication---------------//
        [ActionName("Getenrolment_validation")]
        [HttpGet]
        public HttpResponseMessage Getenrolment_validation(string enrolment_no)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlRegisterlawyer objmdlRegisterlawyer = new mdlRegisterlawyer();
            objdaregisterlawyer.DaGetenrolment_validation(objmdlRegisterlawyer,enrolment_no);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlRegisterlawyer);
        }
    }
}
