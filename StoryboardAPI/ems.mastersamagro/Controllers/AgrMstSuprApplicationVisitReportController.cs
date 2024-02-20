using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to various functions in supplier visit report (Visit updates, address, document upload & download events)
    /// </summary>
    /// <remarks>Written by Abilash.A, Premchander.K </remarks>

    [RoutePrefix("api/AgrMstSuprApplicationVisitReport")]
    [Authorize]
    public class AgrMstSuprApplicationVisitReportController : ApiController
    {
        DaAgrMstSuprApplicationVisitReport objAgrMstSuprApplicationAdd = new DaAgrMstSuprApplicationVisitReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetVisitedplace")]
        [HttpGet]
        public HttpResponseMessage getAddressTypeASC()
        {
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisitedplace(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetInspectingOfficials")]
        //[HttpGet]
        //public HttpResponseMessage GetInspectingOfficials()
        //{
        //    MdlMstVisitPerson values = new MdlMstVisitPerson();
        //    objAgrMstSuprApplicationAdd.GetInspectingOfficials(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //Visit Person Details 
        [ActionName("PostPersonDetails")]
        [HttpPost]
        public HttpResponseMessage PostPersonDetails(mstVisitpersondtl_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostPersonDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Contact Person Mobile No

        [ActionName("PostVisitContactNo")]
        [HttpPost]
        public HttpResponseMessage PostVisitContactNo(mstVisitpersoncontact_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostVisitContactNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostVisitAddress")]
        [HttpPost]
        public HttpResponseMessage PostMobileNo(mstVisitpersonaddress_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostVisitAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VisitDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage VisitDocumentUpload()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            UploadDocumentList values = new UploadDocumentList();

            objAgrMstSuprApplicationAdd.DaPostVisitDocumentUpload(httpRequest, values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
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

            UploadphotoList values = new UploadphotoList();

            objAgrMstSuprApplicationAdd.DaPostVisitUploadPhoto(httpRequest, values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisittmpContactList")]
        [HttpGet]
        public HttpResponseMessage GetVisittmpContactList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objAgrMstSuprApplicationAdd.DaGetVisittmpContactList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetVisitContactList")]
        [HttpGet]
        public HttpResponseMessage GetVisitContactList(string applicationvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objAgrMstSuprApplicationAdd.DaGetVisitContactList(getsessionvalues.employee_gid, applicationvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditVisitContactList")]
        [HttpGet]
        public HttpResponseMessage GetEditVisitContactList(string applicationvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objAgrMstSuprApplicationAdd.DaGetEditVisitContactList(getsessionvalues.employee_gid, applicationvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLVisittmpPersondtlList")]
        [HttpGet]
        public HttpResponseMessage GetLVisittmpPersondtlList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisittmpPersondtlList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisittmpAddressList")]
        [HttpGet]
        public HttpResponseMessage GetVisittmpAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisittmpAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitPersondtlList")]
        [HttpGet]
        public HttpResponseMessage GetLVisitPersondtlList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisitPersondtlList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditVisitPersondtlList")]
        [HttpGet]
        public HttpResponseMessage GetEditVisitPersondtlList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetEditVisitPersondtlList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitAddressList")]
        [HttpGet]
        public HttpResponseMessage GetVisitAddressList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisitAddressList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditVisitAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEditVisitAddressList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetEditVisitAddressList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisittmpDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetVisittmpDocumentList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisittmpDocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetVisitDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetVisitDocumentList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisitDocumentList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditVisitDocumentList")]
        [HttpGet]
        public HttpResponseMessage GetEditVisitDocumentList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetEditVisitDocumentList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisittmpPhotosList")]
        [HttpGet]
        public HttpResponseMessage GetVisittmpPhotosList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisittmpPhotoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetVisitPhotosList")]
        [HttpGet]
        public HttpResponseMessage GetVisitPhotosList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisitPhotoList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditVisitPhotosList")]
        [HttpGet]
        public HttpResponseMessage GetEditVisitPhotosList(string applicationvisit_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetEditVisitPhotoList(getsessionvalues.employee_gid, applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisittmpContactList")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpContactList(string applicationvisitperson2contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersoncontact_list values = new mstVisitpersoncontact_list();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpContactList(applicationvisitperson2contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteVisittmppersondtlList")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmppersondtlList(string applicationvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmppersondtlList(applicationvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteVisittmpAddressList")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpAddressList(string applicationvisit2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersonaddress_list values = new mstVisitpersonaddress_list();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpAddressList(applicationvisit2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisittmpDocumentList")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpDocumentList(string applicationvisit2document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentList values = new UploadDocumentList();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpDocumentList(applicationvisit2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisittmpPhotoList")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpPhotoList(string applicationvisit2photo_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadphotoList values = new UploadphotoList();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpPhotoList(applicationvisit2photo_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisittmpContact")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpContact()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersoncontact_list values = new mstVisitpersoncontact_list();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpContact(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteVisittmppersondtl")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmppersondtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmppersondtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteVisittmpAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpAddress()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersonaddress_list values = new mstVisitpersonaddress_list();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisittmpDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpDocument()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentList values = new UploadDocumentList();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVisittmpPhoto")]
        [HttpGet]
        public HttpResponseMessage DeleteVisittmpPhoto()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadphotoList values = new UploadphotoList();
            objAgrMstSuprApplicationAdd.DaDeleteVisittmpPhoto(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostApplicationVisitReport")]
        [HttpPost]
        public HttpResponseMessage PostApplicationVisitReport(MdlMstVisitPerson values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostApplicationVisitReport(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSubmitApplicationVisitReport")]
        [HttpPost]
        public HttpResponseMessage PostSubmitApplicationVisitReport(MdlMstVisitPerson values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostSubmitApplicationVisitReport(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditApplicationVisitaddress")]
        [HttpGet]
        public HttpResponseMessage EditApplicationVisitaddress(string applicationvisit2address_gid)
        {
            mstVisitpersonaddress_list values = new mstVisitpersonaddress_list();
            objAgrMstSuprApplicationAdd.DaEditApplicationVisitaddress(applicationvisit2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditApplicationVisitpersondtl")]
        [HttpGet]
        public HttpResponseMessage EditApplicationVisitpersondtl(string applicationvisit2person_gid)
        {
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objAgrMstSuprApplicationAdd.DaEditApplicationVisitpersondtl(applicationvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostPersonDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage PostPersonDetailsUpdate(mstVisitpersondtl_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostPersonDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostPersonaddressUpdate")]
        [HttpPost]
        public HttpResponseMessage PostPersonaddressUpdate(mstVisitpersonaddress_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostPersonaddressUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSubmitApplicationVisitReportUpdate")]
        [HttpPost]
        public HttpResponseMessage PostSubmitApplicationVisitReportUpdate(MdlMstVisitPerson values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostSubmitApplicationVisitReportUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateApplicationVisitReportUpdate")]
        [HttpPost]
        public HttpResponseMessage PostUpdateApplicationVisitReportUpdate(MdlMstVisitPerson values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostUpdateApplicationVisitReportUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSaveApplicationVisitReportUpdate")]
        [HttpPost]
        public HttpResponseMessage PostSaveApplicationVisitReportUpdate(MdlMstVisitPerson values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstSuprApplicationAdd.DaPostSaveApplicationVisitReportUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitReportList")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportList(string application_gid, string statusupdated_by)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaGetVisitReportList(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditApplicationVisitReport")]
        [HttpGet]
        public HttpResponseMessage EditApplicationVisitReport(string applicationvisit_gid)
        {
            MdlMstVisitPerson values = new MdlMstVisitPerson();
            objAgrMstSuprApplicationAdd.DaEditApplicationVisitReport(applicationvisit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVisitReportDtls")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportDtls(string visitreport_gid)
        {

            MdlMstVisitPersonView values = new MdlMstVisitPersonView();
            objAgrMstSuprApplicationAdd.DaGetVisitReportDtls(visitreport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}