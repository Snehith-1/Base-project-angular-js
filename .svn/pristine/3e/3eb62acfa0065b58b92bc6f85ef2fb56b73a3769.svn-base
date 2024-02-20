using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.master.DataAccess;
using ems.master.Models;


namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCourierAck")]
    [AllowAnonymous]
    public class MstCourierAckController : ApiController
    {
        DaMstCourierAck objDaMstCourierAck = new DaMstCourierAck();
        result objResult = new result();

        // Mail - Courier Acknowledgement Form 
        [ActionName("GetAcknowledgeForm")]
        [HttpGet]
        public HttpResponseMessage GetAcknowledgeForm(string courierMgmt_gid)
        {
            MdlCourierAckDtl values = new MdlCourierAckDtl();
            objDaMstCourierAck.DaGetAcknowledgeForm(courierMgmt_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Courier - Acknowledge Status
        [ActionName("PostAckStatus")]
        [HttpPost]
        public HttpResponseMessage PostFeedbackdtl(MdlMstCourierAck values)
        {
            objResult = objDaMstCourierAck.DaPostAckStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}