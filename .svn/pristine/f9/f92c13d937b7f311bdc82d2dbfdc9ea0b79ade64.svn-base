using ems.brs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.brs.Dataacess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.brs.Controllers
{
    [RoutePrefix("api/ConfigurationReconcillation")]
    [Authorize]
    public class ConfigurationReconcillationController : ApiController
    {

        DaConfigurationReconcillation objDaConfigurationReconcillation = new DaConfigurationReconcillation();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetConfigurationSummary")]
        [HttpGet]
        public HttpResponseMessage GetConfigurationSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlConfigurationReConcillation objConfigurationReconcillation = new MdlConfigurationReConcillation();
            objDaConfigurationReconcillation.DaGetConfigurationSummary(objConfigurationReconcillation, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objConfigurationReconcillation);
        }
        [ActionName("Addbankconfiguration")]
        [HttpPost]
        public HttpResponseMessage Addbankconfiguration(MdlConfigurationReConcillation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConfigurationReconcillation.DaAddbankconfiguration(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Editbankconfiguration")]
        [HttpGet]
        public HttpResponseMessage Editbankconfiguration(string bankconfig_gid)
        {
            MdlConfigurationReConcillation values = new MdlConfigurationReConcillation();
            objDaConfigurationReconcillation.DaEditbankconfiguration(bankconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteConfigurationdata")]
        [HttpGet]
        public HttpResponseMessage DeleteConfigurationdata(string bankconfig_gid)
        {
            MdlConfigurationReConcillation values = new MdlConfigurationReConcillation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConfigurationReconcillation.DaDeleteConfigurationdata(bankconfig_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatebankconfiguration")]
        [HttpPost]
        public HttpResponseMessage Updatebankconfiguration(MdlConfigurationReConcillation values)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConfigurationReconcillation.DaUpdatebankconfiguration(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
