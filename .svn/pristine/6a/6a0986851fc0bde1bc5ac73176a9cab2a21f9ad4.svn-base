using ems.foundation.DataAccess;
using ems.foundation.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.foundation.Controllers
{
    [RoutePrefix("api/FndMstCampaignTypeMaster")]
    [Authorize]
    public class FndMstCampaignTypeMasterController : ApiController
    {
        DaFndMstCampaignTypeMaster objDaFndMstCampaignTypeMaster = new DaFndMstCampaignTypeMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetCampaignType")]
        [HttpGet]
        public HttpResponseMessage GetCampaignType()
        {
            MdlFndMstCampaignTypeMaster values = new MdlFndMstCampaignTypeMaster();
            objDaFndMstCampaignTypeMaster.DaGetCampaignType(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCampaignType")]
        [HttpPost]
        public HttpResponseMessage CreateCampaignType(MdlFndMstCampaignTypeMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCampaignTypeMaster.DaCreateCampaignType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCampaignType")]
        [HttpGet]
        public HttpResponseMessage EditCampaignType(string campaigntype_gid)
        {
            MdlFndMstCampaignTypeMaster values = new MdlFndMstCampaignTypeMaster();
            objDaFndMstCampaignTypeMaster.DaEditCampaignType(campaigntype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCampaignType")]
        [HttpPost]
        public HttpResponseMessage UpdateCampaignType(MdlFndMstCampaignTypeMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCampaignTypeMaster.DaUpdateCampaignType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCampaignType")]
        [HttpPost]
        public HttpResponseMessage InactiveCampaignType(MdlFndMstCampaignTypeMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCampaignTypeMaster.DaInactiveCampaignType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCampaignType")]
        [HttpGet]
        public HttpResponseMessage DeleteCampaignType(string campaigntype_gid)
        {
            MdlFndMstCampaignTypeMaster values = new MdlFndMstCampaignTypeMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCampaignTypeMaster.DaDeleteCampaignType(campaigntype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CampaignTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CampaignTypeInactiveLogview(string campaigntype_gid)
        {
            MdlFndMstCampaignTypeMaster values = new MdlFndMstCampaignTypeMaster();
            objDaFndMstCampaignTypeMaster.DaCampaignTypeInactiveLogview(campaigntype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}