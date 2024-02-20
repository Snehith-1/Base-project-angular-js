using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.lgl;

namespace EMS.Reports.Controllers.ems.lgl
{
    [RoutePrefix("api/DN1")]
    public class DN1Controller : ApiController
    {
        // GET: DN1

        [HttpGet]
        [ActionName("templatecontentdn1")]
        public HttpResponseMessage Gettemplatecontentdn1(string id)
        {
            rpt_DN1format objFunction = new rpt_DN1format();
            var path = objFunction.gettemplatecontentdn1(id);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
        [ActionName("templatecontentdn2")]
        [HttpGet]

        public HttpResponseMessage Gettemplatecontentdn2(string id)
        {
            rpt_DN1format objFunction = new rpt_DN1format();
            var path = objFunction.gettemplatecontentdn2(id);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
        [ActionName("templatecontentdn3")]
        [HttpGet]

        public HttpResponseMessage Gettemplatecontentdn3(string id)
        {
            rpt_DN1format objFunction = new rpt_DN1format();
            var path = objFunction.gettemplatecontentdn3(id);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
        [ActionName("templatecontentCBO")]
        [HttpGet]

        public HttpResponseMessage GettemplatecontentCBO(string id)
        {
            rpt_DN1format objFunction = new rpt_DN1format();
            var path = objFunction.gettemplatecontentCBO(id);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}