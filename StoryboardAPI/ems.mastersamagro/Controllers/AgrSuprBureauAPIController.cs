using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to third party API to fetch highmark and transunion records to our client's customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>


    [RoutePrefix("api/AgrSuprBureauAPI")]
    [Authorize]

    public class AgrSuprBureauAPIController : ApiController
    {
        DaAgrSuprBureauAPI objDaAgrSuprBureauAPI = new DaAgrSuprBureauAPI();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHighmarkCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrSuprBureauAPI.DaGetHighmarkCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaAgrSuprBureauAPI.DaGetHighmarkHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionConsumerCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionConsumerCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrSuprBureauAPI.DaGetTransUnionConsumerCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetTransunionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaAgrSuprBureauAPI.DaGetTransUnionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighmarkInstitutionCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrSuprBureauAPI.DaGetHighmarkInstitutionCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkInstitutionHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaAgrSuprBureauAPI.DaGetHighmarkInstitutionHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighRiskAlertDetails")]
        [HttpGet]
        public HttpResponseMessage GetHighRiskAlertDetails(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHighRiskAlertDetails values = new MdlHighRiskAlertDetails();
            objDaAgrSuprBureauAPI.DaGetHighRiskAlertDetails(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}