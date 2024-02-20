using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.foundation.Models;
using ems.foundation.DataAccess;


namespace ems.foundation.Controllers
{
    [RoutePrefix("api/FndTrnCampaign")]
    [Authorize]
    public class FndTrnCampaignController : ApiController
    {
        DaTrnCampaign objDaFndTrnCampaign = new DaTrnCampaign();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetCampaigntype")]
        [HttpGet]
        public HttpResponseMessage GetCampaigntype()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign objcampaigntype = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignType(objcampaigntype, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objcampaigntype);
        }

        [ActionName("GetCustomer")]
        [HttpGet]
        public HttpResponseMessage GetCustomer()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign objcampaigntype = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCustomer(objcampaigntype, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objcampaigntype);
        }

        [ActionName("GetQuestionnarie")]
        [HttpGet]
        public HttpResponseMessage GetQuestionnarie(string categorytype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetQuestionnarie(values, categorytype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCampaignQuestionnarie")]
        [HttpGet]
        public HttpResponseMessage GetCampaignQuestionnarie(string categorytype_gid,string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignQuestionnarie(values, categorytype_gid,campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMultipleListQuestionnarie")]
        [HttpGet]
        public HttpResponseMessage GetMultipleListQuestionnarie(string categorytype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetMultipleListQuestionnarie(values, categorytype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMultipleSelectListQuestionnarie")]
        [HttpGet]
        public HttpResponseMessage GetMultipleSelectListQuestionnarie(string categorytype_gid, string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetMultipleSelectListQuestionnarie(values, categorytype_gid, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetQuestionCategory")]
        [HttpGet]
        public HttpResponseMessage GetQuestionCategory()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign objcampaigntype = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetQuestionCategory(objcampaigntype, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objcampaigntype);
        }
        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            MdlTrnCampaign objmaster = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetEmployeelist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetCustomerdtls")]
        [HttpGet]
        public HttpResponseMessage GetCustomerdtls(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCustomerdtl(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignSummary")]
        [HttpGet]
        public HttpResponseMessage GetCampaignSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetCampaignApprovalRejected")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApprovalRejected()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignApprovalRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCampaignApprovalApproved")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApprovalApproved()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignApprovalApproved(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCampaignClosed")]
        [HttpGet]
        public HttpResponseMessage GetCampaignClosed()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignClosed(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignCounts")]
        [HttpGet]
        public HttpResponseMessage GetCampaignCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignApprovalCounts")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApprovalCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignApprovalCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignApprovalpending")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApprovalpending()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignApprovalpending(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCampaignApproval")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApproval()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignRejected")]
        [HttpGet]
        public HttpResponseMessage GetCampaignRejected()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSingleform")]
        [HttpPost]
        public HttpResponseMessage PostAnswerDesc(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostSingleform(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSingleform")]
        [HttpGet]
        public HttpResponseMessage GetSingleform(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetSingleform(values, getsessionvalues.employee_gid, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        
        [ActionName("GetCampaignSingleform")]
        [HttpGet]
        public HttpResponseMessage GetCampaignSingleform(string category_gid)
        {
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignSingleform(values, category_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSingleformEdit")]
        [HttpGet]
        public HttpResponseMessage GetSingleformEdit(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetSingleformEdit(values, getsessionvalues.employee_gid, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("campaignEditUpdate")]
        [HttpPost]
        public HttpResponseMessage campaignEditUpdate(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnCampaign.DacampaignEditUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCampaignApproved")]
        [HttpPost]
        public HttpResponseMessage PostCampaignApproved(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostCampaignApproved(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCampaignRejected")]
        [HttpPost]
        public HttpResponseMessage PostCampaignRejected(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostCampaignRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //[ActionName("GetCampaignApproved")]
        //[HttpPost]
        //public HttpResponseMessage GetCampaignApproved(string campaign_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlTrnCampaign values = new MdlTrnCampaign();
        //    objDaFndTrnCampaign.DaGetCampaignApproved(values, getsessionvalues.employee_gid, campaign_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}



        //[ActionName("GetCampaignRejected")]
        //[HttpPost]
        //public HttpResponseMessage GetCampaignRejected(string campaign_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlTrnCampaign values = new MdlTrnCampaign();
        //    objDaFndTrnCampaign.DaGetCampaignRejected(values, getsessionvalues.employee_gid, campaign_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("PostSingleformEdit")]
        [HttpPost]
        public HttpResponseMessage PostSingleformEdit(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostSingleformEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMultipleform")]
        [HttpPost]
        public HttpResponseMessage PostMultipleform(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostMultipleform(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMultipleformEdit")]
        [HttpPost]
        public HttpResponseMessage PostMultipleformEdit(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostMultipleformEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMultipleform")]
        [HttpGet]
        public HttpResponseMessage GetMultipleform(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetMultipleform(values, getsessionvalues.employee_gid, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetMultipleformEdit")]
        [HttpGet]
        public HttpResponseMessage GetMultipleformEdit(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetMultipleformEdit(values, getsessionvalues.employee_gid, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSingleform")]
        [HttpGet]
        public HttpResponseMessage DeleteSingleform(string campaigndtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaDeleteSingleform(campaigndtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CampaignSave")]
        [HttpPost]
        public HttpResponseMessage CampaignSave(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaCampaignSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CampaignSubmit")]
        [HttpPost]
        public HttpResponseMessage CampaignSubmit(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaCampaignSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteMultipleform")]
        [HttpGet]

        public HttpResponseMessage DeleteMultipleform(string campaigndtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaDeleteMultipleform(campaigndtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetCampaignDetails")]
        //[HttpGet]
        //public HttpResponseMessage GetCampaignDetails(string campaign_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlTrnCampaign values = new MdlTrnCampaign();
        //    objDaFndTrnCampaign.DaGetCampaignDetails(values, getsessionvalues.employee_gid,campaign_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("GetCampaignDetails")]
        [HttpGet]
        public HttpResponseMessage GetCampaignDetails(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignDetails(values, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        //[ActionName("GetCampaignDetails")]
        //[HttpGet]
        //public HttpResponseMessage GetCampaignDetails(string campaign_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlTrnCampaign values = new MdlTrnCampaign();
        //    objDaFndTrnCampaign.DaGetCampaignDetails(campaign_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("campaignDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage campaignDetailsEdit(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DacampaignDetailsEdit(campaign_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("campaignDetailsView")]
        [HttpGet]
        public HttpResponseMessage campaignDetailsView(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DacampaignDetailsView(campaign_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCampaign")]
        [HttpGet]
        public HttpResponseMessage DeleteCampaignType(string campaign_gid)
        {
            MdlFndMstCampaignTypeMaster values = new MdlFndMstCampaignTypeMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaDeleteCampaign(campaign_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostRaiseQuery(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetCampaignRaiseQuery(string campaign_gid)
        {
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnCampaign.DaGetCampaignRaiseQuery(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCampaignresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostCampaignresponsequery(MdlTrnCampaign values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnCampaign.DaPostCampaignresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}