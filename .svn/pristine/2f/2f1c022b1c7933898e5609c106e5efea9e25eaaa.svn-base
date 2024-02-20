using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.master;
namespace EMS.Reports.Controllers.ems.master
{
    [RoutePrefix("api/MstVisitorTag")]
    public class MstVisitorTagController : ApiController
    {
        [HttpGet]
        [ActionName("getVisitorTagpdf")]
        public HttpResponseMessage getVisitorTagpdf(string visitor_gid)
        {
            DaMstVisitorTag objFunction = new DaMstVisitorTag();
            var path = objFunction.DagetVisitorTagpdf(visitor_gid);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}