using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net;
using System.Net.Http;
using ems.master.DataAccess;
using ems.master.Models;

/// <summary>
/// (It's used for BRE in Samfin)BRE Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sherin </remarks>

namespace ems.master.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/MstBRE")]
    [Authorize]
    public class MstBREController : ApiController

    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstBRE objDaMstBRE = new DaMstBRE();

        // Group Title - Start
        [ActionName("GetGroupTitle")]
        [HttpGet]
        public HttpResponseMessage GetGroupTitle()
        {
            MdlMstBRE objMdlgrouptitle = new MdlMstBRE();
            objDaMstBRE.DaGetGroupTitle(objMdlgrouptitle);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlgrouptitle);
        }

        [ActionName("CreateGroupTitle")]
        [HttpPost]
        public HttpResponseMessage CreateGroupTitle(GroupTitle values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstBRE.DaCreateGroupTitle(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGroupTitle")]
        [HttpGet]
        public HttpResponseMessage EditGroupTitle(string grouptitle_gid)
        {
            GroupTitle values = new GroupTitle();
            objDaMstBRE.DaEditGroupTitle(grouptitle_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGroupTitle")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupTitle(GroupTitle values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstBRE.DaUpdateGroupTitle(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GroupTitleDelete")]
        //[HttpGet]
        //public HttpResponseMessage LoanTypeDelete(string loantype_gid)
        //{
        //    loantype values = new loantype();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaMstBRE.DaLoanTypeDelete(loantype_gid, getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("GroupTitleStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage GroupTitleStatusUpdate(GroupTitle values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstBRE.DaGroupTitleStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupTitleInactiveLog")]
        [HttpGet]
        public HttpResponseMessage GetGroupTitleInactiveLog(string grouptitle_gid)
        {
            MdlMstBRE objMdlgrouptitle = new MdlMstBRE();
            objDaMstBRE.DaGetGroupTitleInactiveLog(grouptitle_gid, objMdlgrouptitle);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlgrouptitle);
        }

        // Answer Type - Start
        [ActionName("GetAnswerType")]
        [HttpGet]
        public HttpResponseMessage GetAnswerType()
        {
            MdlMstBRE objMdlAnswertype = new MdlMstBRE();
            objDaMstBRE.DaGetAnswerType(objMdlAnswertype);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAnswertype);
        }

        [ActionName("CreateAnswerType")]
        [HttpPost]
        public HttpResponseMessage CreateAnswerType(AnswerType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstBRE.DaCreateAnswerType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAnswerType")]
        [HttpGet]
        public HttpResponseMessage EditAnswerType(string answertype_gid)
        {
            AnswerType values = new AnswerType();
            objDaMstBRE.DaEditAnswerType(answertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAnswerType")]
        [HttpPost]
        public HttpResponseMessage UpdateAnswerType(AnswerType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstBRE.DaUpdateAnswerType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        /* [actionname("answertypedelete")]
        [httpget]
        public httpresponsemessage answertypedelete(string answertype_gid)
        {
            answertype values = new answertype();
            string token = request.headers.getvalues("authorization").firstordefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objDaMstBRE.daanswertypedelete(answertype_gid, getsessionvalues.employee_gid, values);
            return request.createresponse(httpstatuscode.ok, values);
        }*/

        [ActionName("AnswerTypeStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage AnswerTypeStatusUpdate(AnswerType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstBRE.DaAnswerTypeStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAnswerTypeInactiveLog")]
        [HttpGet]
        public HttpResponseMessage GetLAnswerTypeInactiveLog(string answertype_gid)
        {
            MdlMstBRE objMdlAnswertype = new MdlMstBRE();
            objDaMstBRE.DaGetAnswerTypeInactiveLog(answertype_gid, objMdlAnswertype);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAnswertype);
        }

    }
}