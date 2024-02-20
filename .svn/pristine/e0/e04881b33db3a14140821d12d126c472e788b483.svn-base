using ems.foundation.DataAccess;
using ems.foundation.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
namespace ems.foundation.Controllers
{
    [RoutePrefix("api/FndTrnMyCampaignSummary")]
    [Authorize]
    public class FndTrnMyCampaignSummaryController : ApiController
    {
        DaFndTrnMyCampaignSummary objDaFndTrnMyCampaignSummary = new DaFndTrnMyCampaignSummary();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();



        [ActionName("GetCampaignSummary")]
        [HttpGet]
        public HttpResponseMessage GetCampaignSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetCampaignSummary(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCampaignDetails")]
        [HttpGet]
        public HttpResponseMessage GetCampaignDetails(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnMyCampaignSummary.DaGetCampaignDetails(values, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PastDateCheck")]
        [HttpGet]
        public HttpResponseMessage PastDateCheck(string date)
        {
            result values = new result();
            objDaFndTrnMyCampaignSummary.DaPastDateCheck(date, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("FutureDateCheck")]
        [HttpGet]
        public HttpResponseMessage FutureDateCheck(string date)
        {
            result values = new result();
            objDaFndTrnMyCampaignSummary.DaFutureDateCheck(date, values);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignEditSingle")]
        [HttpGet]
        public HttpResponseMessage GetCampaignEditSingle(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
           objDaFndTrnMyCampaignSummary.DaGetMycampaignSingle(values, campaign_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignMultipleDetails")]
        [HttpGet]
        //Add Multiple form
        public HttpResponseMessage GetCampaignMultipleDetails(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnMyCampaignSummary.GetCampaignMultipleDetails(values, campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CampaignFinalSubmit")]
        [HttpPost]
        public HttpResponseMessage CampaignFinalSubmit(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           
            objDaFndTrnMyCampaignSummary.CampaignFinalSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignSummaryApproved")]
        [HttpGet]
        public HttpResponseMessage GetCampaignSummaryApproved()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetCampaignSummaryApproved(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCampaignSummaryRejected")]
        [HttpGet]
        public HttpResponseMessage GetCampaignSummaryRejected()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetCampaignSummaryRejected(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMyCampaignCounts")]
        [HttpGet]
        public HttpResponseMessage GetMyCampaignCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetMyCampaignCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyCampaignSummaryCounts")]
        [HttpGet]
        public HttpResponseMessage GetMyCampaignSummaryCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetMyCampaignSummaryCounts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCampaignApprovalpending")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApprovalpending()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetCampaignApprovalpending(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MyCampaignApprovedSubmit")]
        [HttpPost]
        public HttpResponseMessage MyCampaignApprovalFinalSubmit(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnMyCampaignSummary.MyCampaignApprovedSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MyCampaignRejectSubmit")]
        [HttpPost]
        public HttpResponseMessage MyCampaignRejectSubmit(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnMyCampaignSummary.MyCampaignRejectSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ImportExcelSample")]
        [HttpPost]
        public HttpResponseMessage ImportExcelSample()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
          
            result objResult = new result();
           // 
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("ExcelTemplate")]
        [HttpGet]
        public HttpResponseMessage ExcelTemplate(string campaign_gid)
        {
            sampledynamicdatadtl values = new sampledynamicdatadtl();
            SingleMultiFormReport objSingleMultiFormReport = new SingleMultiFormReport();
            //HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            //httpRequest = HttpContext.Current.Request;
            result objResult = new result();           
            objDaFndTrnMyCampaignSummary.DaDynamicExcelTemplate(values, campaign_gid, getsessionvalues.employee_gid, objSingleMultiFormReport);
           // objDaFndTrnMyCampaignSummary.DaAtmTrnSamplingDynamicExcelTemplate(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objSingleMultiFormReport);
        }
        [ActionName("MyCampaignSubmit")]
        [HttpPost]
        public HttpResponseMessage MyCampaignSubmit(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
          
            objDaFndTrnMyCampaignSummary.DaMyCampaignSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MyCampaignUpdate")]
        [HttpPost]
        public HttpResponseMessage MyCampaignUpdate(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnMyCampaignSummary.DaMyCampaignUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MyCampaignRadioSubmit")]
        [HttpPost]
        public HttpResponseMessage MyCampaignRadioSubmit(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnMyCampaignSummary.MyCampaignRadioSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MyCampaignMultipleUpdate")]
        [HttpPost]
        public HttpResponseMessage MyCampaignMultipleUpdate(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnMyCampaignSummary.DaMyCampaignMultipleUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMycampaignSingle")]
        [HttpGet]
        public HttpResponseMessage GetMycampaignSingle(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnMyCampaignSummary.DaGetMycampaignSingle(values, campaign_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSingleCampaignSummary")]
        [HttpGet]
        public HttpResponseMessage GetSingleCampaignSummary(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnMyCampaignSummary.DaGetSingleCampaignSummary(getsessionvalues.employee_gid, values,  campaign_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSingleFormView")]
        [HttpGet]
        public HttpResponseMessage GetSingleFormView(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnMyCampaignSummary.DaGetSingleformView(values, campaign_gid,getsessionvalues.employee_gid  );
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMycampaignMultiple")]
        [HttpGet]
        //Multiple summary by logged in user
        public HttpResponseMessage GetMycampaignMultiple(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sampledynamicdatadtl values = new sampledynamicdatadtl();
            objDaFndTrnMyCampaignSummary.DaGetMycampaignMultiple(values, campaign_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GeteditMycampaignMultiple")]
        [HttpGet]
        //Edit Multiple
        public HttpResponseMessage GeteditMycampaignMultiple(string campaign_gid, string reference_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTrnCampaign values = new MdlTrnCampaign();
            objDaFndTrnMyCampaignSummary.DaGeteditMycampaignMultiple(values, campaign_gid, reference_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMycampaignTeamActivity")]
        [HttpGet]
        //Multiple summary by team
        public HttpResponseMessage GetMycampaignTeamActivity(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sampledynamicdatadtl values = new sampledynamicdatadtl();
            objDaFndTrnMyCampaignSummary.DaGetMycampaignTeamActivity(values, campaign_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        [ActionName("GetMycampaignApprovalTeamActivity")]
        [HttpGet]
        public HttpResponseMessage GetMycampaignApprovalTeamActivity(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetMycampaignApprovalTeamActivity(values, campaign_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MyCampaignMultipleSubmit")]
        [HttpPost]
        public HttpResponseMessage MyCampaignMultipleSubmit(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaFndTrnMyCampaignSummary.DaMyCampaignMultipleSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MyCampaignExcelSubmit")]
        [HttpPost]
        public HttpResponseMessage MyCampaignExcelSubmit()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
          MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            result objResult = new result();
            // objDaFndTrnMyCampaignSummary.DaCampaignimportexcel(values, getsessionvalues.employee_gid);
            objDaFndTrnMyCampaignSummary.DaCampaignimportexcel(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);

        }
        
        [ActionName("GetCampaignApprovalclose")]
        [HttpGet]
        public HttpResponseMessage GetCampaignApprovalclose()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetCampaignApprovalclose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSampleDynamicdata")]
        [HttpGet]
        public HttpResponseMessage GetSampleDynamicdata(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sampledynamicdatadtl values = new sampledynamicdatadtl();
            objDaFndTrnMyCampaignSummary.DaGetSampleDynamicdata(campaign_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string campaign_gid)
        {
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetEmployeeName(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostMyCampaignRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostMyCampaignRaiseQuery(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnMyCampaignSummary.DaPostMyCampaignRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetMyCampaignRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetMyCampaignRaiseQuery(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetMyCampaignRaiseQuery(campaign_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMyCampaignApprovalRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetMyCampaignApprovalRaiseQuery(string campaign_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetMyCampaignApprovalRaiseQuery(campaign_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostMyCampaignresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostMyCampaignresponsequery(MdlFndTrnMyCampaignSummary values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnMyCampaignSummary.DaPostMyCampaignresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCampaignManager")]
        [HttpGet]
        public HttpResponseMessage GetCampaignManager(string campaign_gid)
        {
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetCampaignManager(campaign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetManagerEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetManagerEmployeeName(string campaignmanager2employee_gid)
        {
            MdlFndTrnMyCampaignSummary values = new MdlFndTrnMyCampaignSummary();
            objDaFndTrnMyCampaignSummary.DaGetManagerEmployeeName(campaignmanager2employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ExportSingleMultipleFormDetails")]
        [HttpGet]
        public HttpResponseMessage ExportSingleMultipleFormDetails(string campaign_gid)
        {
            SingleMultiFormReport objSingleMultiFormReport = new SingleMultiFormReport();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndTrnMyCampaignSummary.DaExportSingleMultipleFormDetails(objSingleMultiFormReport, campaign_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objSingleMultiFormReport);
        }


    }
}
