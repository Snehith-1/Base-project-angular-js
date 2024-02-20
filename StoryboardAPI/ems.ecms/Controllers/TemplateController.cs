using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;
using ems.ecms.DataAccess;


namespace ems.ecms.Controllers
{
    /// <summary>
    /// template Controller Class containing API methods for accessing the  DataAccess class DaTemplate 
    ///  Teplate - Get template from DB, Get content from DB
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/template")]
    [Authorize]
    public class TemplateController : ApiController
    {
        DaTemplate objDaTemplate = new DaTemplate();
        [ActionName("template")]
        [HttpGet]
        public HttpResponseMessage getTemplate()
        {
            Mdltemplate objMdlTemplate = new Mdltemplate();
            objDaTemplate .DaGetTemplate (objMdlTemplate);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlTemplate);
        }


        [ActionName("Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateContent()
        {
            template_list objTemplateContent = new template_list();
            objDaTemplate .DaGetTemplateContent(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
    }
}
