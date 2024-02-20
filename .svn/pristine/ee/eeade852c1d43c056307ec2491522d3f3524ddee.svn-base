using ems.foundation.DataAccess;
using ems.foundation.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;

namespace ems.foundation.Controllers
{
    [RoutePrefix("api/FndQuestionnarieMaster")]
    [Authorize]
    public class FndQuestionnarieMasterController : ApiController
    {
        DaFndMstQuestionnarie objDaFndMstQuestionnarie = new DaFndMstQuestionnarie();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetCampaigntype")]
        [HttpGet]
        public HttpResponseMessage Getconstitution()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlcampaigntype objcampaigntype = new Mdlcampaigntype();
            objDaFndMstQuestionnarie.DaGetCampaignType(objcampaigntype, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objcampaigntype);
        }
        [ActionName("GetQuestionnarie")]
        [HttpGet]
        public HttpResponseMessage GetCustomerApproving()
        {
            MdlMstQuestionnarie values = new MdlMstQuestionnarie();
            objDaFndMstQuestionnarie.DaGetQuestionnarie(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateQuestionnarie")]
        [HttpPost]
        public HttpResponseMessage CreateCampaignType(MdlMstQuestionnarie values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstQuestionnarie.DaCreateCampaignType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("QuestionnarieEditSubmit")]
        [HttpPost]
        public HttpResponseMessage QuestionnarieEditSubmit(MdlMstQuestionnarie values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstQuestionnarie.DaEditQuestionnarieSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostAnswerDesc")]
        [HttpPost]
        public HttpResponseMessage PostAnswerDesc(MdlMstQuestionnarie values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstQuestionnarie.DaPostAnswerDesc( values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostEditAnswerDesc")]
        [HttpPost]
        public HttpResponseMessage PostEditAnswerDesc(MdlMstQuestionnarie values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstQuestionnarie.DaEditPostAnswerDesc(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAnswerDesc")]
        [HttpGet]
        public HttpResponseMessage GetAnswerDesc(string Questionnarie_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            questionnarieanswer_list  values = new questionnarieanswer_list();
            objDaFndMstQuestionnarie.DaGetAnswerDesc(values, getsessionvalues.employee_gid, Questionnarie_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditAnswerDesc")]
        [HttpGet]
        public HttpResponseMessage GetEditAnswerDesc(string Questionnarie_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            questionnarieanswer_list values = new questionnarieanswer_list();
            objDaFndMstQuestionnarie.DaGeteditAnswerDesc(values, getsessionvalues.employee_gid, Questionnarie_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        [ActionName("DeleteAnswerDesc")]
        [HttpGet]
        public HttpResponseMessage deleteAnswerDesc(string questionnarieanswer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstQuestionnarie values = new MdlMstQuestionnarie();
            objDaFndMstQuestionnarie.DaDeleteAnswerDesc(questionnarieanswer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("QuestionnarieEdit")]
        [HttpGet]
        public HttpResponseMessage QuestionnarieEdit(string Questionnarie_gid)
        {
            Questionnarie_list values = new Questionnarie_list();
           objDaFndMstQuestionnarie.DaQuestionnarieEdit(Questionnarie_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }      


        [ActionName("InactiveQuestionnarie")]
        [HttpPost]
        public HttpResponseMessage InactiveCampaignType(MdlMstQuestionnarie values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstQuestionnarie.DaInactiveQuestionnarie(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteQuestionnarie")]
        [HttpGet]
        public HttpResponseMessage DeleteCampaignType(string Questionnarie_gid)
        {
            MdlMstQuestionnarie values = new MdlMstQuestionnarie();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstQuestionnarie.DaDeleteQuestionnarie( values, Questionnarie_gid, getsessionvalues.employee_gid);

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("QuestionnarieInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage QuestionnarieInactiveLogview(string Questionnarie_gid)
        {
            MdlMstQuestionnarie  values = new MdlMstQuestionnarie();
           objDaFndMstQuestionnarie.DaQuestionnarieInactiveLogview(Questionnarie_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ImportExcelSample")]
        [HttpPost]
        public HttpResponseMessage ImportExcelSample()
        {
            HttpRequest httpRequest;
            MdlMstQuestionnarie values = new MdlMstQuestionnarie();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaFndMstQuestionnarie.DaQuestionnarieimportexcel(httpRequest, getsessionvalues.employee_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

    }
}