using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.master.Models;
using ems.master.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCreditMapping")]
    [Authorize]
    public class MstCreditMappingController : ApiController
    {
        DaMstCreditMapping objDaMstCreditMapping = new DaMstCreditMapping(); 
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Credit Group Add
        [ActionName("PostCreditGroupAdd")]
        [HttpPost]
        public HttpResponseMessage PostCreditGroupAdd(MdlCreditGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditgroupaddAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Group Summary
        [ActionName("GetCreditGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditGroupSummary()
        {
            MdlCreditGroup objmaster = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditGroupSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetCreditGroupEdit")]
        [HttpGet]
        public HttpResponseMessage GetCreditGroupEdit(string creditmapping_gid)
        {
            MdlCreditGroup objmaster = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditGroupEdit(creditmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostCreditGroupUpdate")]
        [HttpPost]
        public HttpResponseMessage PostCreditGroupUpdate(MdlCreditGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditGroupUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCredit2Heads")]
        [HttpGet]
        public HttpResponseMessage GetCredit2Heads(string creditmapping_gid)
        {
            MdlCreditGroup objmaster = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCredit2Heads(creditmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetCreditgroupHeads")]
        [HttpGet]
        public HttpResponseMessage GetCreditgroupHeads(string creditmapping_gid)
        {
            creditheads values = new creditheads();
            objDaMstCreditMapping.DaGetCreditgroupHeads(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditgroupInactive")]
        [HttpPost]
        public HttpResponseMessage PostCreditgroupInactive(CreditGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditgroupInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditgroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetCreditgroupInactiveLogview(string creditmapping_gid)
        {
            MdlCreditGroup values = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditgroupInactiveLogview(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreditassignUpdate")]
        [HttpPost]
        public HttpResponseMessage PostCreditassignUpdate(MdlCreditheadassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreditassignUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditReassignUpdate")]
        [HttpPost]
        public HttpResponseMessage GetCreditReassignUpdate(MdlCreditheadassign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaGetCreditReassignUpdate(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditgroupname")]
        [HttpGet]
        public HttpResponseMessage GetCreditgroupname(string creditmapping_gid)
        {
            MdlCreditGroup values = new MdlCreditGroup();
            objDaMstCreditMapping.DaGetCreditgroupname(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetReassignedLog")]
        [HttpGet]
        public HttpResponseMessage GetReassignedLog(string application_gid)
        {
            MdlreassignedlogInfo objreassignedlogInfo = new MdlreassignedlogInfo();
            objDaMstCreditMapping.DaGetReassignedLog(application_gid, objreassignedlogInfo);
            return Request.CreateResponse(HttpStatusCode.OK, objreassignedlogInfo);
        }

        [ActionName("GetCreditMappingLog")]
        [HttpGet]
        public HttpResponseMessage GetCreditMappingLog(string creditmapping_gid)
        {
            MdlCreditMappingLogInfo objreassignedlogInfo = new MdlCreditMappingLogInfo();
            objDaMstCreditMapping.DaGetCreditMappingLog(creditmapping_gid, objreassignedlogInfo);
            return Request.CreateResponse(HttpStatusCode.OK, objreassignedlogInfo);
        }
        //CC Export
        [ActionName("ExportMstCreditMapping")]
        [HttpGet]
        public HttpResponseMessage ExportMstCreditMapping()
        {
            MstApplicationReport objMstApplicationReport = new MstApplicationReport();
            objDaMstCreditMapping.DaExportMstCreditMapping(objMstApplicationReport);
            return Request.CreateResponse(HttpStatusCode.OK, objMstApplicationReport);
        }
         
        [ActionName("GetCreditGroupTitleList")]
        [HttpGet]
        public HttpResponseMessage GetCreditGroupTitleList(string creditmapping_gid)
        {
            CreditGroupTitle_list objmaster = new CreditGroupTitle_list();
            objDaMstCreditMapping.DaGetCreditGroupTitleList(creditmapping_gid,objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostCreateCreditRule")]
        [HttpPost]
        public HttpResponseMessage PostCreateCreditRule(MdlGroupTitleQuestion values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreateCreditRule(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditquestionsummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditquestionsummary(string creditmapping_gid)
        {
            MdlGroupTitleQuestion_list objmaster = new MdlGroupTitleQuestion_list();
            objDaMstCreditMapping.DaGetCreditquestionsummary(creditmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("Getquestionlistsummary")]
        [HttpGet]
        public HttpResponseMessage Getquestionlistsummary(string creditquestionrule_gid)
        {
            MdlGroupTitleQuestion objmaster = new MdlGroupTitleQuestion();
            objDaMstCreditMapping.DaGetquestionlistsummary(creditquestionrule_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        //Transaction
        [ActionName("GetCreditScorecarddtl")]
        [HttpGet]
        public HttpResponseMessage GetCreditScorecarddtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditGroupQuestiondtl values = new MdlCreditGroupQuestiondtl();
            objDaMstCreditMapping.DaGetCreditScorecarddtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditQuestionScore")]
        [HttpPost]
        public HttpResponseMessage GetCreditQuestionScore(MdlCreditGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            List<MdlCreditGroupScore> values = new List<MdlCreditGroupScore>();
            objDaMstCreditMapping.DaGetCreditQuestionScore(values, data, data.creditquestionrule_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeleteQuestionList")]
        [HttpGet]
        public HttpResponseMessage GetDeleteQuestionList(string creditquestionrule_gid)
        { 
            result values = new result();
            objDaMstCreditMapping.DaGetDeleteQuestionList(creditquestionrule_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditGroupList")]
        [HttpGet]
        public HttpResponseMessage GetCreditGroupList(string creditmapping_gid)
        {
            MdlCreditGroupdtl values = new MdlCreditGroupdtl();
            objDaMstCreditMapping.DaGetCreditGroupList(creditmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGroupOrder")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupOrder(MdlCreditGroupdtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaMstCreditMapping.DaUpdateGroupOrder(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("UpdateGroupQuestionOrder")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupQuestionOrder(MdlGroupTitleQuestion_list data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objDaMstCreditMapping.DaUpdateGroupQuestionOrder(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("SubmitScoreCard")]
        [HttpPost]
        public HttpResponseMessage SubmitScoreCard(MdlCreditGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaSubmitScoreCard(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("GetCreditScorecardViewdtl")]
        [HttpGet]
        public HttpResponseMessage GetCreditScorecardViewdtl(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCreditGroupQuestiondtl values = new MdlCreditGroupQuestiondtl();
            objDaMstCreditMapping.DaGetCreditScorecardViewdtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaveScoreCard")]
        [HttpPost]
        public HttpResponseMessage SaveScoreCard(MdlCreditGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaSaveScoreCard(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("GetVerticalGroupTitleList")]
        [HttpGet]
        public HttpResponseMessage GetVerticalGroupTitleList(string vertical_gid)
        {
            VerticalTitle_list objmaster = new VerticalTitle_list();
            objDaMstCreditMapping.DaGetVerticalGroupTitleList(vertical_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetVerticalquestionsummary")]
        [HttpGet]
        public HttpResponseMessage GetVerticalquestionsummary(string vertical_gid, string applicant_type)
        {
            MdlVerticalGroupTitleQuestion_list objmaster = new MdlVerticalGroupTitleQuestion_list();
            objDaMstCreditMapping.DaGetVerticalquestionsummary(vertical_gid, applicant_type, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostCreateVerticalRule")]
        [HttpPost]
        public HttpResponseMessage PostCreateVerticalRule(MdlVerticalTitleQuestion values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreateVerticalRule(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVerticalquestionlistsummary")]
        [HttpGet]
        public HttpResponseMessage GetVerticalquestionlistsummary(string verticalquestionrule_gid)
        {
            MdlVerticalTitleQuestion objmaster = new MdlVerticalTitleQuestion();
            objDaMstCreditMapping.DaGetVerticalquestionlistsummary(verticalquestionrule_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetDeleteVerticalQuestionList")]
        [HttpGet]
        public HttpResponseMessage GetDeleteVerticalQuestionList(string Verticalquestionrule_gid)
        {
            result values = new result();
            objDaMstCreditMapping.DaGetDeleteVerticalQuestionList(Verticalquestionrule_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateVerticalGroupQuestionOrder")]
        [HttpPost]
        public HttpResponseMessage UpdateVerticalGroupQuestionOrder(MdlVerticalGroupTitleQuestion_list data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaUpdateVerticalGroupQuestionOrder(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("UpdateVerticalGroupOrder")]
        [HttpPost]
        public HttpResponseMessage UpdateVerticalGroupOrder(MdlVerticaldtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaUpdateVerticalGroupOrder(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("GetVerticalGroupList")]
        [HttpGet]
        public HttpResponseMessage GetVerticalGroupList(string vertical_gid, string applicant_type)
        {
            MdlVerticaldtl values = new MdlVerticaldtl();
            objDaMstCreditMapping.DaGetVerticalGroupList(vertical_gid, applicant_type, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        
        // Vertical Transcation

        [ActionName("GetVerticalBasicView")]
        [HttpGet]
        public HttpResponseMessage GetVerticalBasicView(string vertical_gid, string applicanttype, string application_gid)
        {
            MdlTrnVertical values = new MdlTrnVertical();
            objDaMstCreditMapping.DaGetVerticalBasicView(vertical_gid, applicanttype, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVerticalScorecarddtl")]
        [HttpGet]
        public HttpResponseMessage GetVerticalScorecarddtl(string vertical_gid, string applicanttype)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVerticalGroupQuestiondtl values = new MdlVerticalGroupQuestiondtl();
            objDaMstCreditMapping.DaGetVerticalScorecarddtl(vertical_gid, applicanttype, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVerticalScorecardViewdtl")]
        [HttpGet]
        public HttpResponseMessage GetVerticalScorecardViewdtl(string vertical_gid, string applicanttype, string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVerticalGroupQuestiondtl values = new MdlVerticalGroupQuestiondtl();
            objDaMstCreditMapping.DaGetVerticalScorecardViewdtl(vertical_gid, applicanttype, application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetVerticalQuestionScore")]
        [HttpPost]
        public HttpResponseMessage GetVerticalQuestionScore(MdlVerticalGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            List<MdlVerticalGroupScore> values = new List<MdlVerticalGroupScore>();
            objDaMstCreditMapping.DaGetVerticalQuestionScore(values, data, data.verticalquestionrule_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VerticalSaveScoreCard")]
        [HttpPost]
        public HttpResponseMessage VerticalSaveScoreCard(MdlVerticalGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaVerticalSaveScoreCard(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ActionName("VerticalSubmitScoreCard")]
        [HttpPost]
        public HttpResponseMessage VerticalSubmitScoreCard(MdlVerticalGroupScoredtl data)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaVerticalSubmitScoreCard(data, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // Vertical Applicant Type Master 

        [ActionName("GetVerticalGroupTitleListAppType")]
        [HttpGet]
        public HttpResponseMessage GetVerticalGroupTitleListAppType(string vertical_gid)
        {
            VerticalTitle_list objmaster = new VerticalTitle_list();
            objDaMstCreditMapping.DaGetVerticalGroupTitleListAppType(vertical_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetVerticalquestionsummaryAppType")]
        [HttpGet]
        public HttpResponseMessage GetVerticalquestionsummaryAppType(string vertical_gid)
        {
            MdlVerticalGroupTitleQuestion_list objmaster = new MdlVerticalGroupTitleQuestion_list();
            objDaMstCreditMapping.DaGetVerticalquestionsummaryAppType(vertical_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostCreateVerticalRuleAppType")]
        [HttpPost]
        public HttpResponseMessage PostCreateVerticalRuleAppType(MdlVerticalTitleQuestion values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditMapping.DaPostCreateVerticalRuleAppType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeleteVerticalQuestionListAppType")]
        [HttpGet]
        public HttpResponseMessage GetDeleteVerticalQuestionListAppType(string verticalapplicanttyperule_gid)
        {
            result values = new result();
            objDaMstCreditMapping.DaGetDeleteVerticalQuestionListAppType(verticalapplicanttyperule_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVerticalGroupTitleListAppTypeEdit")]
        [HttpGet]
        public HttpResponseMessage GetVerticalGroupTitleListAppTypeEdit(string verticalapplicanttyperule_gid)
        {
            VerticalTitle_list objmaster = new VerticalTitle_list();
            objDaMstCreditMapping.DaGetVerticalGroupTitleListAppTypeEdit(verticalapplicanttyperule_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

    }
}
