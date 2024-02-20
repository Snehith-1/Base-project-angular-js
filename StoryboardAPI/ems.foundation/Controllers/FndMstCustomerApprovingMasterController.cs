using ems.foundation.DataAccess;
using ems.foundation.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.foundation.Controllers
{
    [RoutePrefix("api/FndMstCustomerApprovingMaster")]
    [Authorize]
    public class FndMstCustomerApprovingMasterController : ApiController
    {
        DaFndMstCustomerApprovingMaster objDaFndMstCustomerApprovingMaster = new DaFndMstCustomerApprovingMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetCustomerApproving")]
        [HttpGet]
        public HttpResponseMessage GetCustomerApproving()
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaGetCustomerApproving(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCustomerApproving")]
        [HttpPost]
        public HttpResponseMessage CreateCustomerApproving(MdlFndMstCustomerApprovingMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCustomerApprovingMaster.DaCreateCustomerApproving(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
     
        [ActionName("EditCustomerApproving")]
        [HttpGet]
        public HttpResponseMessage EditCustomerApproving(string customerapproving_gid)
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaEditCustomerApproving(customerapproving_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCustomerApproving")]
        [HttpPost]
        public HttpResponseMessage UpdateCustomerApproving(MdlFndMstCustomerApprovingMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCustomerApprovingMaster.DaUpdateCustomerApproving(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCustomerApproving")]
        [HttpPost]
        public HttpResponseMessage InactiveCustomerApproving(MdlFndMstCustomerApprovingMaster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCustomerApprovingMaster.DaInactiveCustomerApproving(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCustomerApproving")]
        [HttpGet]
        public HttpResponseMessage DeleteCustomerApproving(string customerapproving_gid)
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaFndMstCustomerApprovingMaster.DaDeleteCustomerApproving(customerapproving_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CustomerApprovingInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CustomerApprovingInactiveLogview(string customerapproving_gid)
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaCustomerApprovingInactiveLogview(customerapproving_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerApprovingMaker")]
        [HttpGet]
        public HttpResponseMessage GetCustomerApprovingMaker()
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaGetCustomerApprovingMaker(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerApprovingChecker")]
        [HttpGet]
        public HttpResponseMessage GetCustomerApprovingChecker()
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaGetCustomerApprovingChecker(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerApprovingApprover")]
        [HttpGet]
        public HttpResponseMessage GetCustomerApprovingApprover()
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaGetCustomerApprovingApprover(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string customerapproving_gid)
        {
            CustomerApprovingemployee values = new CustomerApprovingemployee();
            objDaFndMstCustomerApprovingMaster.DaGetEmployeeName(customerapproving_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCustomerApprovingChecker")]
        [HttpGet]
        public HttpResponseMessage GetCustomerApprovingChecker(string checklistmaster_gid)
        {
            MdlFndMstCustomerApprovingMaster values = new MdlFndMstCustomerApprovingMaster();
            objDaFndMstCustomerApprovingMaster.DaGetCustomerApprovingChecker(checklistmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}