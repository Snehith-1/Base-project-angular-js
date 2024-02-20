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

namespace ems.lgl.Controllers
{
    [RoutePrefix("api/lglMstServiceType")]
    [Authorize]
    public class lglMstServiceTypeController : ApiController
    {
        DaLglMstServiceType objServiceType = new DaLglMstServiceType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("Postservicetype")]
        [HttpPost]
        public HttpResponseMessage Postservicetype(MdlLglMstServiceType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objServiceType.DaPostservicetype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("getservicetype")]
        [HttpGet]
        public HttpResponseMessage getservicetype()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLglMstServiceType objLglMstServiceType = new MdlLglMstServiceType();
            objServiceType.Dagetservicetype(objLglMstServiceType, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objLglMstServiceType);
        }
        [ActionName("updateservicetype")]
        [HttpPost]
        public HttpResponseMessage updateservicetype(MdlLglMstServiceType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objServiceType.Daupdateservicetype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("editservicetype")]
        [HttpGet]
        public HttpResponseMessage editservicetype(string servicetype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLglMstServiceType values = new MdlLglMstServiceType();
            objServiceType.Daeditservicetype(getsessionvalues.employee_gid, servicetype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deleteservicetype")]
        [HttpGet]
        public HttpResponseMessage deleteservicetype(string servicetype_gid)
        {

            MdlLglMstServiceType values = new MdlLglMstServiceType();
            objServiceType.Dadeleteservicetype(servicetype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getservicetype2invoice")]
        [HttpGet]
        public HttpResponseMessage getservicetype2invoice()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLglMstServiceType objLglMstServiceType = new MdlLglMstServiceType();
            objServiceType.Dagetservicetype2invoice(objLglMstServiceType, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objLglMstServiceType);
        }
    }
}
