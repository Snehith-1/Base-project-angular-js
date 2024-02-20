using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.master.Models;
using ems.master.DataAccess;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCustomertmp")]
    [Authorize]
    public class MstCustomertmpController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCustomertmp objDaMstCustomerAdd = new DaMstCustomertmp();
        //----------View Information-----------//
        [ActionName("GetViewCustomer2UserDtl")]
        [HttpGet]
        public HttpResponseMessage getviewcustomer2userdtl(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaMstCustomerAdd.DaGetViewCustomer2UserDtl(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //-----Get Information--------//
        [ActionName("GetCustomer2UserInfo")]
        [HttpGet]
        public HttpResponseMessage getcustomer2userinfo(string customer2usertype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaMstCustomerAdd.DaGetCustomer2UserInfo(customer2usertype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------Get Count Information-----------//
        [ActionName("GetCount")]
        [HttpGet]
        public HttpResponseMessage GetCount()
        {
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaMstCustomerAdd.DaGetCount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        }
    }
