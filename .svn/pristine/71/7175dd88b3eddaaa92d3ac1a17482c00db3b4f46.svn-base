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

/// <summary>
/// (It's used for pages in CC member and CC group master )CCMember Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.idas.Controllers
{
    [RoutePrefix("api/MstCCMember")]
    [Authorize]
    public class MstCCMemberController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCCMemeber objDaCCMember = new DaMstCCMemeber();

        [ActionName("postccmember")]
        [HttpPost]
        public HttpResponseMessage postccmember(MdlMstCCMember values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCCMember.Dapostccmember(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getccmember")]
        [HttpGet]
        public HttpResponseMessage Getccmember()
        {
            MdlMstCCMember objccmember = new MdlMstCCMember();
            objDaCCMember.DaGetccmember(objccmember);
            return Request.CreateResponse(HttpStatusCode.OK, objccmember);
        }
       
        [ActionName("deleteccmember")]
        [HttpGet]
        public HttpResponseMessage Deleteccmember(string ccmembermaster_gid)
        {
            MdlMstCCMember values = new MdlMstCCMember();
            objDaCCMember.DaGetdeleteccmember(ccmembermaster_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getccgroup2member")]
        [HttpGet]
        public HttpResponseMessage Getccgroup2member(string ccgroupname_gid)
        {
            MdlMstCCMember values = new MdlMstCCMember();
            objDaCCMember.DaGetccgroup2member( values, ccgroupname_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("postccgroup")]
        [HttpPost]
        public HttpResponseMessage postccgroup(ccgroupname values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCCMember.Dapostccgroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getccgroup")]
        [HttpGet]
        public HttpResponseMessage Getccgroup()
        {
            ccgroupname objccmember = new ccgroupname();
            objDaCCMember.DaGetccgroup(objccmember);
            return Request.CreateResponse(HttpStatusCode.OK, objccmember);
        }
        [ActionName("geteditccgroup")]
        [HttpGet]
        public HttpResponseMessage geteditccgroup(string ccgroupname_gid)
        {
            ccgroupname values = new ccgroupname();
            objDaCCMember.Dageteditccgroup(values, ccgroupname_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updateccgroup")]
        [HttpPost]
        public HttpResponseMessage updateccgroup(ccgroupname values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCCMember.Daupdateccgroup(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("geteditccmember")]
        [HttpGet]
        public HttpResponseMessage geteditccmember(string ccmembermaster_gid)
        {
            MdlMstCCMember values = new MdlMstCCMember();
            objDaCCMember.Dageteditccmember(values, ccmembermaster_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updateccmember")]
        [HttpPost]
        public HttpResponseMessage updateccmember(MdlMstCCMember values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCCMember.Daupdateccmember(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
