using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.lgl.Models;
using ems.utilities.Functions;
using System.Web;
using ems.lgl.DataAccess;

namespace StoryboardAPI.Controllers.ems.lgl
{
    [RoutePrefix("api/lglDashboard")]
    [Authorize]

    public class LglDashboardController : ApiController
    {
        DaLglDashboard objdalgl = new DaLglDashboard();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("getGID")]
        [HttpGet]
        public HttpResponseMessage getGID()
        {
            mdllglDashboard values = new mdllglDashboard();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);     
            var status = objdalgl.DaGetGID(values, getsessionvalues.user_gid);          
            if (status == true)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }          
           return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetdntrackerReport")]
        [HttpGet]
        public HttpResponseMessage getdntrackerReport(string date)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlLglReport values = new MdlLglReport();
            objdalgl.DaGetdntrackerReport(date, values);          
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetdntrackerReport_IST")]
        [HttpGet]
        public HttpResponseMessage GetdntrackerReport_IST(string date)
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlLglReport values = new MdlLglReport();
            objdalgl.DaGetdntrackerReport_IST(date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getdnTAT")]
        [HttpGet]
        public HttpResponseMessage getdnTAT()
        {
            MdlLglReport values = new MdlLglReport();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdalgl.DaGetdnTAT(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DNexport")]
        [HttpPost]
        public HttpResponseMessage DNexport(dn_list objdn_list)
        {

            var employee_gid = "";
            objdalgl.DaGetDNexport(objdn_list, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdn_list);
        }

        [ActionName("Getlegalsrreport")]
        [HttpGet]
        public HttpResponseMessage getlegalsrreport()
        {
            mdllglDashboard objlegalSR = new mdllglDashboard();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdalgl.DaGetlegalsrreport(objlegalSR, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }


        [ActionName("GetSRexport")]
        [HttpPost]
        public HttpResponseMessage GetSRexport(legalSR_list objdn_list)
        {

            var employee_gid = "";
            objdalgl.DaGetSRexport(objdn_list, employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdn_list);
        }

        [ActionName("GetCustomerName")]
        [HttpGet]
        public HttpResponseMessage GetCustomerName(string customername)
        {
            mdllglDashboard values = new mdllglDashboard();
            objdalgl.DaGetCustomerName(customername, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //[ActionName("Getmonth")]
        //[HttpGet]
        //public HttpResponseMessage Getmonth(string created_date)
        //{
        //    mdllglDashboard values = new mdllglDashboard();
        //    objdalgl.DaGetmonth(created_date, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}




        //[ActionName("Getyear")]
        //[HttpGet]
        //public HttpResponseMessage Getyear(string created_date)
        //{
        //    mdllglDashboard values = new mdllglDashboard();
        //    objdalgl.DaGetyear(created_date, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        //[ActionName("PostLegalreportsummary")]
        //[HttpPost]
        //public HttpResponseMessage PostLegalreportsummary(string customername, string year_date, string month_date)
        //{
        //    mdllglDashboard values = new mdllglDashboard();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objdalgl.DaPostLegalreportsummary(year_date, month_date, customername, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        //[ActionName("PostLegalreportsummary")]
        //[HttpPost]
        //public HttpResponseMessage PostLegalreportsummary(string customername, string year_date, string month_date)
        //{
        //    mdllglDashboard values = new mdllglDashboard();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objdalgl.DaPostLegalreportsummary(year_date, month_date, customername, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        [ActionName("GetLegalreportsummary")]
        [HttpPost]
        public HttpResponseMessage GetLegalreportsummary(mdllglDashboard values)
        {
            //mdllglDashboard values = new mdllglDashboard();
            objdalgl.DaGetLegalreportsummary( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetLegalreportsummary")]
        //[HttpPost]
        //public HttpResponseMessage GetLegalreportsummary(string user_gid)
        //{
        //    mdllglDashboard values = new mdllglDashboard();
        //    objdalgl.DaGetLegalreportsummary(user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);

        //    var user_gid = "";
        //    values.DaGetLegalreportsummary(values, user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("GetExploreCustomer")]
        //[HttpGet]
        //public HttpResponseMessage getExploreCustomer()
        //{
        //    customer_list objlegalSR = new customer_list();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objdalgl.DaGetlegalsrreport(objlegalSR, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        //}




        //[ActionName("GetExploreCustomer")]
        //[HttpGet]
        //public HttpResponseMessage getExploreCustomer()
        //{
        //    mdllglDashboard objCustomer = new mdllglDashboard();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objdalgl.DaGetExploreCustomer(values, getsessionvalues.customer_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        //[ActionName("Getlegalsrreport")]
        //[HttpGet]
        //public HttpResponseMessage getlegalsrreport()
        //{
        //    MdlLglReport objlegalSR = new MdlLglReport();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objdalgl.DaGetlegalsrreport(objdalgl,getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, objdalgl);
        //}



    }
}
