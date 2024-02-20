using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.sample;

namespace EMS.Reports.Controllers.ems.sample
{
    public class sampleController : ApiController
    {
        public HttpResponseMessage userData()
        {
            rpt_userData objFunction = new rpt_userData();
            var path = objFunction.getReport();
            return Request.CreateResponse(HttpStatusCode.OK,path);
        }
    }
}
