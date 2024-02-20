using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.rms.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.rms.DataAccess;

namespace StoryboardAPI.Controllers.ems.rms
{
    [RoutePrefix("api/dashboard")]
    [Authorize]
    public class dashboardController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        daRMSdashboard objDabusinessunit = new daRMSdashboard();

        [ActionName("businessunit")]
        [HttpGet]
        public HttpResponseMessage getbusinessunit()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdldashboard objbusinessunit = new mdldashboard();
            objDabusinessunit.DagetRMSdashboard( getsessionvalues.employee_gid, objbusinessunit);
         
            return Request.CreateResponse(HttpStatusCode.OK, objbusinessunit);
        }

        [ActionName("Recruitersummary")]
        [HttpGet]
        public HttpResponseMessage getRecruitersummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlRecruitersummary objRecruitersummary = new mdlRecruitersummary();
             objDabusinessunit.DaRecruiterSummary(getsessionvalues.employee_gid, objRecruitersummary);
            return Request.CreateResponse(HttpStatusCode.OK, objRecruitersummary);
        }
        [ActionName("overallstatus")]
        [HttpGet]
        public HttpResponseMessage getoverallstatus()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdloverallstatus objoverallstatus = new mdloverallstatus();
            objDabusinessunit.Dagetoverallstatus(objoverallstatus, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objoverallstatus);
        }
        [ActionName("dashboardprivilege")]
        [HttpGet]
        public HttpResponseMessage getdashboardprivilege()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            dashboardprivilege objresult = new dashboardprivilege();
            objDabusinessunit.DaprojectPrivilege(getsessionvalues.employee_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("Getemployeemail")]
        [HttpPost]
        public HttpResponseMessage Getemployeemail(employeemail values)
        {
            //lawyerdetails values = new lawyerdetails();
            objDabusinessunit.Daemployeemail(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatepassword")]
        [HttpPost]
        public HttpResponseMessage updatelawyerpassword(employeemail values)
        {
            //lawyerdetails values = new lawyerdetails();
            objDabusinessunit.Daupdatelawyerpassword(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
