using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;
using System.Web;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This controllers will create virtual account for the onboarded customers
    /// </summary>
    /// <remarks>Written by Premchandar.K </remarks>

    [RoutePrefix("api/AgrVirtualAccount")]
    [Authorize]
    public class AgrVirtualAccountController : ApiController
    {
        DaAgrVirtualAccount objDaAgrVirtualAccount = new DaAgrVirtualAccount();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("CreateVirtualAccount")]
        [HttpPost]
        public HttpResponseMessage CreateVirtualAccount(MdlVirtualAccount values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var response = objDaAgrVirtualAccount.DaCreateVirtualAccount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



    }
}