using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Collections.Generic;

namespace ems.hbapiconn.Controllers
{
    [RoutePrefix("api/HBAPISamAgroConn")]
    [Authorize]
    public class HBAPISamAgroConnController : ApiController
    {
        FnHBAPISamAgroConn objFnHBAPISamAgroConn = new FnHBAPISamAgroConn();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        dbconn objdbconn = new dbconn();
        string msSQL, lsinstitution_gid, lscontact_gid;
        bool postResult;

        string APIKeyConfigured = ConfigurationManager.AppSettings["StoryboardAPIKey"].ToString();
        IEnumerable<string> headerAPIkeyValues = null;

        [AllowAnonymous]
        [ActionName("PostBuyerUALimit")]
        [HttpPost]
        public HttpResponseMessage PostBuyerUALimit(MdlRequestBuyerUALimitDetails values)
        {
            if (Request.Headers.TryGetValues("StoryboardAPIKey", out headerAPIkeyValues))
            {
                var secretKey = headerAPIkeyValues.First();
                if (!string.IsNullOrEmpty(secretKey) && APIKeyConfigured.Equals(secretKey))
                {
                    if (ModelState.IsValid)
                    {
                        var response = objFnHBAPISamAgroConn.DaPostBuyerUALimitDetails(values);
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "API key is invalid.");

            /*var response = objDaAgrMstBuyerOnboard.DaPostBuyerUALimit(values);
            return Request.CreateResponse(HttpStatusCode.OK, response);*/
        }


    }
}