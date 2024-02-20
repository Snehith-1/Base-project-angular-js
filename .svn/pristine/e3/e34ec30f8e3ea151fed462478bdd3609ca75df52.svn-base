using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.lgl.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using ems.lgl.DataAccess;
namespace ems.lgl.Controllers
{
    [RoutePrefix("api/LglTrnDNTrackerVertical")]
    [Authorize]
    public class LglTrnDNTrackerVerticalController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaLglTrnDNTrackerVertical objDaLglTrnDNTrackerVertical = new DaLglTrnDNTrackerVertical();

        [ActionName("GetExclusionCustomer")]
        [HttpGet]
        public HttpResponseMessage GetExclusionCustomer(string customer_urn, string exclusion_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaLglTrnDNTrackerVertical.DaGetExclusionCustomer(customer_urn, exclusion_reason, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActivationCustomer")]
        [HttpGet]
        public HttpResponseMessage GetActivationCustomer(string customer_urn, string exclusion_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaLglTrnDNTrackerVertical.DaGetActivationCustomer(customer_urn, exclusion_reason, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetExclusionCustomerHistory")]
        [HttpGet]
        public HttpResponseMessage GetExclusionCustomerHistory(string customer_urn)
        {
            exclusionhistorylist values = new exclusionhistorylist();
            objDaLglTrnDNTrackerVertical.DaGetExclusionCustomerHistory(customer_urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("template_content")]
        [HttpGet]
        public HttpResponseMessage getTemplatecontent(string urn)
        {
            MdlMisdataimportlist objTemplateContent = new MdlMisdataimportlist();
            objDaLglTrnDNTrackerVertical.DaGetTemplateContent(urn, objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("raiselegalsr")]
        [HttpPost]
        public HttpResponseMessage postregisterlawyer(MdlRaiselegalSR values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaLglTrnDNTrackerVertical.DaPostRaiseLegalSR(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        }
}
