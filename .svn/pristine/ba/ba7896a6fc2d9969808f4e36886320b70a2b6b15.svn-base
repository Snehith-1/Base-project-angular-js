using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.iasn.Models;
using ems.iasn.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;

namespace ems.iasn.Controllers
{
    [RoutePrefix("api/IasnMergeWorkItem")]
    [Authorize]
    public class IasnMergeWorkItemController : ApiController
    {
        DaIasnTrnMergeWorkItem  objDaAccess = new DaIasnTrnMergeWorkItem ();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        [ActionName("WIEmailFromList")]
        [HttpGet]
        public HttpResponseMessage GetWIEmailFromList(string email_from)
        {
            MdlWISearchList values = new MdlWISearchList();
            objDaAccess.DaGetWIEmailFromList(email_from,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WISubjectList")]
        [HttpGet]
        public HttpResponseMessage GetWISubjectList(string email_subject)
        {
            MdlWISearchList values = new MdlWISearchList();
            objDaAccess.DaGetWISubjectList(email_subject, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WIZoneList")]
        [HttpGet]
        public HttpResponseMessage GetWIZoneList(string zone_name)
        {
            MdlWISearchList values = new MdlWISearchList();
            objDaAccess.DaGetWIZoneList(zone_name, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UnTaggedWISummary")]
        [HttpPost]
        public HttpResponseMessage GetUnTaggedWISummary(MdlWI objInputValues)
        {
            WorkItemList values = new Models.WorkItemList();
            objDaAccess.DaGetUnTaggedWISummary(objInputValues, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TaggedWISummary")]
        [HttpGet]
        public HttpResponseMessage GetTaggedWISummary(string email_gid)
        {
            WorkItemList values = new Models.WorkItemList();
            objDaAccess.DaGeTaggedWISummary(email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WIMerge")]
        [HttpPost]
        public HttpResponseMessage PostMerge(MdlEmailGId values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostMergeWI(values,getsessionvalues .user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WIUndoMerge")]
        [HttpGet]
        public HttpResponseMessage WIUndoMerge(string email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostUndoMergeWI(email_gid , getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK,objResult );
        }
    }
}
