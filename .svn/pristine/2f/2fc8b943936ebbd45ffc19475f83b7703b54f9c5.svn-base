using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to add and edit records for grading tool icon in the supplier application & Credit stage.
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>


    [RoutePrefix("api/AgrMstSuprApplicationGradingTool")]
    [Authorize]
    public class AgrMstSuprApplicationGradingToolController : ApiController
    {
        DaAgrMstSuprApplicationGradingTool objAgrMstApplicationGrade = new DaAgrMstSuprApplicationGradingTool();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GettmpGradingTool")]
        [HttpGet]
        public HttpResponseMessage GettmpGradingTool()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaGettmpGradingTool(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditGradingToolassesment")]
        [HttpGet]
        public HttpResponseMessage GetEditGradingToolassesment(string application2gradingtool_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaGetEditGradingToolassesment(application2gradingtool_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEdittmpGradingToolassesment")]
        [HttpGet]
        public HttpResponseMessage GetEdittmpGradingToolassesment(string application2gradingtool_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaGetEdittmpGradingToolassesment(getsessionvalues.employee_gid, application2gradingtool_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEditGradingTooltotal")]
        [HttpGet]
        public HttpResponseMessage GetEditGradingTooltotal(string application2gradingtool_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgeographicdetails values = new Mdlgeographicdetails();
            objAgrMstApplicationGrade.DaGetEditGradingTooltotal(application2gradingtool_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGradingTool")]
        [HttpGet]
        public HttpResponseMessage GetGradingTool(string application_gid, string statusupdated_by)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaGetGradingTool(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGradingassesment")]
        [HttpPost]
        public HttpResponseMessage SubmitGradingassesment(Mdlgradingtool values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstApplicationGrade.DaSubmitGradingassesment(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete Grading Assesment----------//
        [ActionName("Deletegradingassesment")]
        [HttpGet]
        public HttpResponseMessage Deletegradingassesment(string application2gradingassesment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaDeletegradingassesment(application2gradingassesment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Edit Institution GST

        [ActionName("EditAssessmentCriteriaDetails")]
        [HttpGet]
        public HttpResponseMessage EditAssessmentCriteriaDetails(string application2gradingassesment_gid)
        {
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaEditAssessmentCriteriaDetails(application2gradingassesment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Institution GST

        [ActionName("UpdateAssessmentCriteriaDetails")]
        [HttpPost]
        public HttpResponseMessage UpdateAssessmentCriteriaDetails(Mdlgradingtool values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstApplicationGrade.DaUpdateAssessmentCriteriaDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Assessment Criteria- Drop Down
        [ActionName("GetAssessmentCriteriaDropDown")]
        [HttpGet]
        public HttpResponseMessage GetAssessmentCriteriaDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlcriteria values = new mdlcriteria();
            objAgrMstApplicationGrade.DaGetAssessmentCriteriaDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGradingToolDetails")]
        [HttpPost]
        public HttpResponseMessage SubmitGradingToolDetails(Mdlgeographicdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstApplicationGrade.DaSubmitGradingToolDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGradingToolDetails")]
        [HttpPost]
        public HttpResponseMessage UpdateGradingToolDetails(Mdlgeographicdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstApplicationGrade.DaUpdateGradingToolDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SaveasDraftGradingToolDetails")]
        [HttpPost]
        public HttpResponseMessage SaveasDraftGradingToolDetails(Mdlgeographicdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstApplicationGrade.DaSaveasDraftGradingToolDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaveasEditDraftGradingToolDetails")]
        [HttpPost]
        public HttpResponseMessage SaveasEditDraftGradingToolDetails(Mdlgeographicdetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstApplicationGrade.DaSaveasEditDraftGradingToolDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //----------- Delete GradingTool----------//
        [ActionName("DeleteGradingToolDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteGradingToolDetails(string application2gradingtool_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaDeleteGradingToolDetails(application2gradingtool_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeletetmpGradingTool")]
        [HttpGet]
        public HttpResponseMessage DeletetmpGradingTool()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlgradingtool values = new Mdlgradingtool();
            objAgrMstApplicationGrade.DaDeletetmpGradingTool(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}