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
    [AllowAnonymous]
    [RoutePrefix("api/MstRuleEngine")]
    [Authorize]
    public class MstRuleEngineController : ApiController
    {
        DaMstRuleEngine objDaMstRuleEngine = new DaMstRuleEngine();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("AddRule")]
        [HttpPost]
        public HttpResponseMessage AddRule(addrule values)
        {   
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            //objDaMstRuleEngine.DaPostAddRule(values,"U1");
            objDaMstRuleEngine.DaPostAddRule(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRuleSummary")]
        [HttpGet]
        public HttpResponseMessage GetRuleSummary()
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetRuleSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AddTemplate")]
        [HttpPost]
        public HttpResponseMessage AddTemplate(addtemplate_assignrules values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaPostAddTemplate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateSummary")]
        [HttpGet]
        public HttpResponseMessage GetTemplateSummary()
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetTemplateSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateSummaryForConfiguration")]
        [HttpGet]
        public HttpResponseMessage GetTemplateSummaryForConfiguration(string ruletemplatemaster_gid)
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaTemplateSummaryForConfiguration(values, ruletemplatemaster_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("configRule")]
        [HttpGet]
        public HttpResponseMessage configRule(string ruleenginemaster_gid)
        {
            addrule values = new addrule();
            objDaMstRuleEngine.DaconfigRule(ruleenginemaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditconfigureRule")]
        [HttpGet]
        public HttpResponseMessage EditconfigureRule(string ruleenginemaster_gid)
        {
            addrule values = new addrule();
            objDaMstRuleEngine.DaEditconfigureRule(ruleenginemaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateconfigureRule")]
        [HttpPost]
        public HttpResponseMessage UpdateAuditMapping(addrule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaUpdateconfigureRule(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostconfigureRule")]
        [HttpPost]
        public HttpResponseMessage PostconfigureRule(addrule values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaPostconfigureRule(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
     

        [ActionName("GetTemplateDropDown")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDropDown()
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetTemplateDropDown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApplicationDropDown")]
        [HttpGet]
        public HttpResponseMessage GetApplicationDropDown()
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetApplicationDropDown(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostExecuteSummary")]
        [HttpGet]
        public HttpResponseMessage PostExecuteSummary()
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaPostExecuteSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostExecuteSummaryView")]
        [HttpGet]
        public HttpResponseMessage PostExecuteSummaryView(string postruleenginerun_gid)
        {
            MdlMstRuleEngine values = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaPostExecuteSummaryView(postruleenginerun_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Title - Start
        [ActionName("GetGroupTitle")]
        [HttpGet]
        public HttpResponseMessage GetGroupTitle()
        {
            MdlMstRuleEngine objMdlgrouptitle = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetGroupTitle(objMdlgrouptitle);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlgrouptitle);
        }

        [ActionName("CreateGroupTitle")]
        [HttpPost]
        public HttpResponseMessage CreateGroupTitle(GroupTitle values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaCreateGroupTitle(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGroupTitle")]
        [HttpGet]
        public HttpResponseMessage EditGroupTitle(string grouptitle_gid)
        {
            GroupTitle values = new GroupTitle();
            objDaMstRuleEngine.DaEditGroupTitle(grouptitle_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGroupTitle")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupTitle(GroupTitle values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaUpdateGroupTitle(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GroupTitleDelete")]
        //[HttpGet]
        //public HttpResponseMessage LoanTypeDelete(string loantype_gid)
        //{
        //    loantype values = new loantype();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaMstRuleEngine.DaLoanTypeDelete(loantype_gid, getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("GroupTitleStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage GroupTitleStatusUpdate(GroupTitle values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaGroupTitleStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupTitleInactiveLog")]
        [HttpGet]
        public HttpResponseMessage GetGroupTitleInactiveLog(string grouptitle_gid)
        {
            MdlMstRuleEngine objMdlgrouptitle = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetGroupTitleInactiveLog(grouptitle_gid, objMdlgrouptitle);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlgrouptitle);
        }

        // Answer Type - Start
        [ActionName("GetAnswerType")]
        [HttpGet]
        public HttpResponseMessage GetAnswerType()
        {
            MdlMstRuleEngine objMdlAnswertype = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetAnswerType(objMdlAnswertype);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAnswertype);
        }

        [ActionName("CreateAnswerType")]
        [HttpPost]
        public HttpResponseMessage CreateAnswerType(AnswerType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaCreateAnswerType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAnswerType")]
        [HttpGet]
        public HttpResponseMessage EditAnswerType(string answertype_gid)
        {
            AnswerType values = new AnswerType();
            objDaMstRuleEngine.DaEditAnswerType(answertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAnswerType")]
        [HttpPost]
        public HttpResponseMessage UpdateAnswerType(AnswerType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaUpdateAnswerType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        /* [actionname("answertypedelete")]
        [httpget]
        public httpresponsemessage answertypedelete(string answertype_gid)
        {
            answertype values = new answertype();
            string token = request.headers.getvalues("authorization").firstordefault();
            getsessionvalues = objgetgid.gettokenvalues(token);
            objdamstruleengine.daanswertypedelete(answertype_gid, getsessionvalues.employee_gid, values);
            return request.createresponse(httpstatuscode.ok, values);
        }*/

        [ActionName("AnswerTypeStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage AnswerTypeStatusUpdate(AnswerType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstRuleEngine.DaAnswerTypeStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAnswerTypeInactiveLog")]
        [HttpGet]
        public HttpResponseMessage GetLAnswerTypeInactiveLog(string answertype_gid)
        {
            MdlMstRuleEngine objMdlAnswertype = new MdlMstRuleEngine();
            objDaMstRuleEngine.DaGetAnswerTypeInactiveLog(answertype_gid, objMdlAnswertype);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlAnswertype);
        }

    }
}