using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.iasn.Models;
using ems.iasn.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;

namespace ems.iasn.Controllers
{

    [RoutePrefix("api/IasnTrnEmailSignature")]
    [Authorize]
    public class IasnTrnEmailSignatureController : ApiController
    {
        DaIasnTrnEmailSignature  objDaAccess = new DaIasnTrnEmailSignature();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        [ActionName("PostEmailSignature")]
        [HttpPost]
        public HttpResponseMessage PostEmailSignature(MdlEmailSignature values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
         objResult  =objDaAccess.DaPostEmailSignature (values ,getsessionvalues .user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetEmailSignature")]
        [HttpGet]
        public HttpResponseMessage GetEmailSugnature()
        {
            MdlEmailSignature values = new MdlEmailSignature();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetEmailSugnature( getsessionvalues.user_gid,values );
            return Request.CreateResponse(HttpStatusCode.OK, values );
        }

       
    }
}
