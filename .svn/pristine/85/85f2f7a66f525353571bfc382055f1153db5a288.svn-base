using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.idas.Models;
using ems.idas.DataAccess;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/DaIdasTrnCourierAck")]
    [AllowAnonymous]
    public class DaIdasTrnCourierAckController : ApiController
    {
        DaTrnCourierAck objDaAccess = new DaTrnCourierAck();
        result objResult = new result();

        // Courier Ack Form ...//

        [ActionName("GetCourierDtl")]
        [HttpGet]
        public HttpResponseMessage GetFeedback(string courierMgmt_gid)
        {
            CourierMgmt values = new CourierMgmt();
            objDaAccess .DaGetAcknowledgeForm(courierMgmt_gid , values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAckStatus")]
        [HttpPost]
        public HttpResponseMessage PostFeedbackdtl(MdlCourierAck values)
        {
            objResult=objDaAccess.DaPostAckStatus (values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult );
        }
    }
}
