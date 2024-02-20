using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/TrnRuleEngine")]
    [Authorize]
    public class TrnRuleEngineController : ApiController
    {
        DaTrnRuleEngine objDaTrnRuleEngine = new DaTrnRuleEngine();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Submit Configured Values of Template
        [ActionName("SubmitConfiguredValuesofTemplate")]
        [HttpPost]
        public HttpResponseMessage SubmitConfiguredValuesofTemplate(MdlMstRuleEngine values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTrnRuleEngine.DaSubmitConfiguredValuesofTemplate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Rule Engine Execute
        [ActionName("RuleEngineExecute")]
        [HttpGet]
        public HttpResponseMessage RuleEngineExecute(string ruletemplatemaster_gid, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnRuleEngine values = new MdlTrnRuleEngine();
            objDaTrnRuleEngine.DaRuleEngineExecute(getsessionvalues.user_gid,ruletemplatemaster_gid, application_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}