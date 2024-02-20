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
/// <remarks>Written by Praveen Raj and Gnanesh </remarks>
namespace ems.master.Controllers
{
    [RoutePrefix("api/BureauAPI")]
    [Authorize]

    public class BureauAPIController : ApiController
    {
        DaBureauAPI objDaBureauAPI = new DaBureauAPI();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHighmarkCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaBureauAPI.DaGetHighmarkCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaBureauAPI.DaGetHighmarkHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionConsumerCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionConsumerCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaBureauAPI.DaGetTransUnionConsumerCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        } 

        [ActionName("GetTransunionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaBureauAPI.DaGetTransUnionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighmarkInstitutionCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaBureauAPI.DaGetHighmarkInstitutionCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        } 

        [ActionName("GetHighmarkInstitutionHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaBureauAPI.DaGetHighmarkInstitutionHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighRiskAlertDetails")]
        [HttpGet]
        public HttpResponseMessage GetHighRiskAlertDetails(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHighRiskAlertDetails values = new MdlHighRiskAlertDetails();
            objDaBureauAPI.DaGetHighRiskAlertDetails(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionCommercialCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionCommercialCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaBureauAPI.DaGetTransUnionCommercialCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetTransunionInstitutionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionInstitutionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaBureauAPI.DaGetTransunionInstitutionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}