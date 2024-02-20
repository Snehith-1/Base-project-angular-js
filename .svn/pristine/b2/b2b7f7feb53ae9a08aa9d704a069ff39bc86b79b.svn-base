using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// (It's used for Bureau API)Bureau API Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Praveen Raj  </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/CADBureauAPI")]
    [Authorize]

    public class CADBureauAPIController : ApiController
    {
        DaCADBureauAPI objDaCADBureauAPI = new DaCADBureauAPI();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHighmarkCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaCADBureauAPI.DaGetHighmarkCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaCADBureauAPI.DaGetHighmarkHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionConsumerCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionConsumerCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaCADBureauAPI.DaGetTransUnionConsumerCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetTransunionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaCADBureauAPI.DaGetTransUnionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighmarkInstitutionCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaCADBureauAPI.DaGetHighmarkInstitutionCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkInstitutionHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaCADBureauAPI.DaGetHighmarkInstitutionHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighRiskAlertDetails")]
        [HttpGet]
        public HttpResponseMessage GetHighRiskAlertDetails(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHighRiskAlertDetails values = new MdlHighRiskAlertDetails();
            objDaCADBureauAPI.DaGetHighRiskAlertDetails(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionCommercialCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionCommercialCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaCADBureauAPI.DaGetTransUnionCommercialCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetTransunionInstitutionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionInstitutionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaCADBureauAPI.DaGetTransunionInstitutionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}