using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.master.Models;
using Newtonsoft.Json;
using System.Configuration;
using ems.master.DataAccess;

/// <summary>
/// (It's used for Customer Report)Customer Report Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>
namespace ems.master.Controllers
{
    [RoutePrefix("api/CustomerReport")]
    [Authorize]
    public class CustomerReportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCustomerReport objDaCustomerReport = new DaCustomerReport();
        //Customer GID Base
        [ActionName("Getcustomerupdatedetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerupdatedetails(string customer_gid)
        {
            customerediturn values = new customerediturn();
            objDaCustomerReport.DaGetEditCustomerurn(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerdetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerdetails(string customer_gid)
        {
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDaCustomerReport.DaGetcustomerdetails(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getsanctionloandetails")]
        [HttpGet]
        public HttpResponseMessage Getsanctionloandetails(string customer_gid)
        {
            sanctionloanurn values = new sanctionloanurn();
            objDaCustomerReport.DaGetsanctionloandetails(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetloanFacilityDetails")]
        [HttpGet]
        public HttpResponseMessage DaGetloanFacilityDetails(string sanction_gid)
        {
            sanctionloanurn values = new sanctionloanurn();
            objDaCustomerReport.DaGetloanFacilityDetails(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomer2Loandtls")]
        [HttpGet]
        public HttpResponseMessage DaGetCustomer2Loandtls(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMisdataimportloanlist values = new MdlMisdataimportloanlist();
            objDaCustomerReport.DaGetCustomer2Loandtls(customer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerCibilSummary")]
        [HttpGet]
        public HttpResponseMessage DaGetCustomerCibilSummary(string account_no)
        {

            MdlCibilSummarydtl values = new MdlCibilSummarydtl();
            objDaCustomerReport.DaGetCustomerCibilSummary(account_no, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerCibilView")]
        [HttpGet]
        public HttpResponseMessage DaGetCustomerCibilSView(string cibildatadtl_gid)
        {

            MdlCibilViewdtl values = new MdlCibilViewdtl();
            objDaCustomerReport.DaGetCustomerCibilView(cibildatadtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
