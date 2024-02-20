using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.idas.Models;
using ems.idas.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/idasMstTemplate")]
    [Authorize]
    public class idasMstTemplateController : ApiController
    {
        DaIdasMstTemplate objDaAccess = new DaIdasMstTemplate();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("MailContent")]
        [HttpGet]
        public HttpResponseMessage GetDocumentMail()
        {
            MdlIdasMstTemplate objTemplateContent = new MdlIdasMstTemplate();
            DaIdasMstTemplate objDaTemplate = new DaIdasMstTemplate();
            objDaTemplate.DaGetTemplateContent(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }

        [ActionName("GetTemplateSummary")]
        [HttpGet]
        public HttpResponseMessage GetTemplateSummary()
        {
            MdlTemplateDtlsList objTemplateSummary = new MdlTemplateDtlsList();
            DaIdasMstTemplate objDaTemplate = new DaIdasMstTemplate();
            objDaTemplate.DaGetTemplateSummary(objTemplateSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateSummary);
        }

        [ActionName("IdasTemplateSubmit")]
        [HttpPost]
        public HttpResponseMessage PostTemplateDetail(MdlTemplateDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DapostTemplateDtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDtl")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDtl(string template_gid)
        {
            MdlTemplateDtls objtemplatedtl = new MdlTemplateDtls();
            objDaAccess.DaGetTemplateDtl(template_gid, objtemplatedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objtemplatedtl);
        }

        [ActionName("GetTemplateType")]
        [HttpGet]
        public HttpResponseMessage GetTemplateType()
        {
            MdlTemplateDtls objtemplatetype = new MdlTemplateDtls();
            objDaAccess.DaGetTemplateType(objtemplatetype);
            return Request.CreateResponse(HttpStatusCode.OK, objtemplatetype);
        }

        [ActionName("IdasUpdateTemplate")]
        [HttpPost]
        public HttpResponseMessage UpdateTemplateDetail(MdlTemplateDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaUpdateTemplateDtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
