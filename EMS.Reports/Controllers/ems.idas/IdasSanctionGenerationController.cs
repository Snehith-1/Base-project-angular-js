using EMS.Reports.ems.idas;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EMS.Reports.Controllers.ems.idas
{
    [RoutePrefix("api/IdasSanctionGeneration")]
    public class IdasSanctionGenerationController : ApiController
    {
        [HttpGet]
        [ActionName("sanctionlettercontent")]
        public HttpResponseMessage Gettemplatecontentdn1(string id)
        {
            rpt_idassanctiongeneration objFunction = new rpt_idassanctiongeneration();
            var path = objFunction.Dasanctionlettercontent(id);
            return Request.CreateResponse(HttpStatusCode.OK, path);
        }
    }
}