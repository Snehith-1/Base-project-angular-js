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
    /// This Controllers will provide access to third party API to fetch highmark and transunion Records to our client's customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>
    

    [RoutePrefix("api/AgrBureauAPI")]
    [Authorize]

    public class AgrBureauAPIController : ApiController
    {
        DaAgrBureauAPI objDaAgrBureauAPI = new DaAgrBureauAPI();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetHighmarkCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrBureauAPI.DaGetHighmarkCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaAgrBureauAPI.DaGetHighmarkHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionConsumerCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionConsumerCreditInfo(string contact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrBureauAPI.DaGetTransUnionConsumerCreditInfo(getsessionvalues.employee_gid, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetTransunionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaAgrBureauAPI.DaGetTransUnionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighmarkInstitutionCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrBureauAPI.DaGetHighmarkInstitutionCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetHighmarkInstitutionHTMLContent")]
        [HttpGet]
        public HttpResponseMessage GetHighmarkInstitutionHTMLContent(string tmpcicdocument_gid)
        {
            MdlHighmarkResponse values = new MdlHighmarkResponse();
            objDaAgrBureauAPI.DaGetHighmarkInstitutionHTMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHighRiskAlertDetails")]
        [HttpGet]
        public HttpResponseMessage GetHighRiskAlertDetails(string contact2bureau_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlHighRiskAlertDetails values = new MdlHighRiskAlertDetails();
            objDaAgrBureauAPI.DaGetHighRiskAlertDetails(contact2bureau_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTransUnionCommercialCreditInfo")]
        [HttpGet]
        public HttpResponseMessage GetTransUnionCommercialCreditInfo(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrBureauAPI.DaGetTransUnionCommercialCreditInfo(getsessionvalues.employee_gid, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetTransunionInstitutionXMLContent")]
        [HttpGet]
        public HttpResponseMessage GetTransunionInstitutionXMLContent(string tmpcicdocument_gid)
        {
            MdlTransUnionResponse values = new MdlTransUnionResponse();
            objDaAgrBureauAPI.DaGetTransunionInstitutionXMLContent(tmpcicdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}