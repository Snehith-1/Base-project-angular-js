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

namespace ems.iasn.Controllers
{
    [RoutePrefix("api/IasnMstTeam")]
    [Authorize]
    public class IasnMstTeamController : ApiController
    {
         DaIasnMstTeam objDaAccess = new DaIasnMstTeam();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        [ActionName("CreateTeam")]
        [HttpPost]
        public HttpResponseMessage CreateTeam(MdlAddTeam values)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult= objDaAccess.DaPostTeam (values,getsessionvalues .user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("TeamSummary")]
        [HttpGet]
        public HttpResponseMessage GetWorkItemSummary()
        {
            MdlTeamSummaryList values = new MdlTeamSummaryList();
            objDaAccess.DaGetTeam (values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditTeam")]
        [HttpGet]
        public HttpResponseMessage EditTeam(string team_gid)
        {
            MdlAddTeam values = new MdlAddTeam();
            objDaAccess.DaGetTeamEdit(team_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateTeam")]
        [HttpPost]
        public HttpResponseMessage UpdateTeam(MdlAddTeam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostUpdateTeam(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

    }
}
