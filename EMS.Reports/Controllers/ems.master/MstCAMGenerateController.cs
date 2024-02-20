using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.Reports.ems.master;

namespace EMS.Reports.Controllers.ems.master
{
    [RoutePrefix("api/MstCAMGenerate")]
    public class MstCAMGenerateController : ApiController
    {
        [HttpGet]
        [ActionName("Generate")]
        public HttpResponseMessage GetCAMGenerate(string id)
        {
            DaMstGenerateCAM objFunction = new DaMstGenerateCAM();
            var path = objFunction.GetCAMGenerate(id);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}
