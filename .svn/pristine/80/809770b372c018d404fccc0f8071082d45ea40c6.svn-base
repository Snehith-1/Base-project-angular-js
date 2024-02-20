using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.master.Models;
using ems.utilities.Functions;
using System.Web;
using ems.utilities.Models;
using ems.master.DataAccess;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstRMMapping")]
    [Authorize]
    public class MstRMMappingController : ApiController
    {
        DaMstRMMapping DaMstRMMapping = new DaMstRMMapping();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHierarchyList")]
        [HttpGet]
        public HttpResponseMessage GetHierarchyList(string baselocation_gid, string vertical_gid,string employeegid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            MdlRMMappingview objMdlRMMapping = new MdlRMMappingview();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            DaMstRMMapping.DaGetHierarchyList(baselocation_gid, vertical_gid, employeegid, objMdlRMMapping, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlRMMapping);
        }
     

        //----------View Information-----------//
        [ActionName("GetRMViewCustomer2UserDtl")]
        [HttpGet]
        public HttpResponseMessage getRMViewCustomer2UserDtl(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            DaMstRMMapping.DaGetRMViewCustomer2UserDtl(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMCustomer2UserInfo")]
        [HttpGet]
        public HttpResponseMessage getRMCustomer2UserInfo(string customer2usertype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            DaMstRMMapping.DaGetRMCustomer2UserInfo(customer2usertype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Employee list 

        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            locationemployee objmaster = new locationemployee();
            DaMstRMMapping.DaGetEmployeelist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("PostAllHierarchyListSearch")]
        [HttpPost]
        public HttpResponseMessage PostAllTicketsSummarySearch(MdlRMMappingview values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            DaMstRMMapping.DaPostAllHierarchyListSearch(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Employee list 

        [ActionName("GetLocationEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetLocationEmployeelist(string baselocation_gid)
        {
            locationemployee objmaster = new locationemployee();
            DaMstRMMapping.DaGetLocationEmployeelist(objmaster, baselocation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("RMMappingExport")]
        [HttpPost]
        public HttpResponseMessage RMMappingExport(MdlRMMappingview values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            DaMstRMMapping.DaRMMappingviewExport(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DaPostAllHierarchyverticalListSearch")]
        [HttpPost]
        public HttpResponseMessage DaPostAllHierarchyverticalListSearch(MdlRMMappingverticalview values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            DaMstRMMapping.DaPostAllHierarchyverticalListSearch(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

      

       
    }
}
