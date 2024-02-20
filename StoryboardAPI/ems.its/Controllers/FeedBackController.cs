using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.its.Models;
using ems.utilities.Functions;
using System.Web;
using ems.utilities.Models;
using ems.its.DataAccess;

namespace ems.its.Controllers
{
    [RoutePrefix("api/FeedBack")]
    [AllowAnonymous]

    public class FeedBackController : ApiController
    {
        DaFeedBack objDaFeedBack = new DaFeedBack();

        // Feedback ...//

        [ActionName("GetFeedbackDtl")]
        [HttpGet]
        public HttpResponseMessage GetFeedback(string token)
        {
            viewticket_details values = new viewticket_details();
            objDaFeedBack.GetFeedbackDtl(token, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostFeedbackdtl")]
        [HttpPost]
        public HttpResponseMessage PostFeedbackdtl(feedbackdtl values)
        {
            objDaFeedBack.DaPostFeedbackdtl(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
