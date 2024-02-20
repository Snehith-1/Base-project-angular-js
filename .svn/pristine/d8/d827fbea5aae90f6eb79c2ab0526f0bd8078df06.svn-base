using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.hrm.Models;
using ems.hrm.DataAccess;

namespace ems.hrm.Controllers
{
    public class MyProfileController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMyProfile objDaMyProfile = new DaMyProfile();

        [ActionName("country")]
        [HttpGet]
        public HttpResponseMessage getcountryname()
        {
            countryname objcountry = new countryname();
            objDaMyProfile.DaGetCountry(objcountry);
            return Request.CreateResponse(HttpStatusCode.OK, objcountry);
        }

        [ActionName("employeedetails")]
        [HttpGet]
        public HttpResponseMessage getemployeedetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            employeedetails objemployeedetails = new employeedetails();
            objDaMyProfile.DaGetEmployeedetails(objemployeedetails,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployeedetails);
        }

        [ActionName("updateemployeedetails")]
        [HttpPost]
        public HttpResponseMessage postemployeedetails(employeedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyProfile.Daupdateemployeedetails(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updatepassword")]
        [HttpPost]
        public HttpResponseMessage postpassword(updatepassword values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyProfile.Daupdatepassword(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getaddressdetails")]
        [HttpGet]
        public HttpResponseMessage getaddressdetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            employeedetails objemployeedetails = new employeedetails();
            objDaMyProfile.DaGetAddressdetails(getsessionvalues.employee_gid, getsessionvalues.user_gid, objemployeedetails);
            return Request.CreateResponse(HttpStatusCode.OK, objemployeedetails);
        }

        [ActionName("updateaddressdetails")]
        [HttpPost]
        public HttpResponseMessage postaddressdetails(employeedetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMyProfile.Daupdateaddressdetails(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("uploadEmployeePhoto")]
        [HttpPost]
        public HttpResponseMessage postuploadEmployeePhoto()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            employeePhotoUpload documentname = new employeePhotoUpload();
             objDaMyProfile.DauploadEmployeePhoto(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
    }
}
