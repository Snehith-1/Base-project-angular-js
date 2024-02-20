using ems.idas.DataAccess;
using ems.idas.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasMstCourierCompany")]
    [Authorize]
    public class IdasMstCourierCompanyController : ApiController
    {
        DaIdasMstCourierCompany objDaAccess = new DaIdasMstCourierCompany();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        result objResult = new result();

        [ActionName("CourierCompany")]
        [HttpPost]
        public HttpResponseMessage CourierCompany(MdlCourierCompany values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult= objDaAccess.DaPostCourierCompany( values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("UpdateCourierCompany")]
        [HttpPost]
        public HttpResponseMessage UpdateCourierCompany(MdlCourierCompany values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult= objDaAccess.DaPostUpdateCourierCompany(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("DeleteCourierCompany")]
        [HttpGet]
        public HttpResponseMessage DeleteCourierCompany(string couriercompany_gid)
        {
           
            objResult=objDaAccess.DaPostDeleteCourierCompany (couriercompany_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("CourierCompanySummary")]
        [HttpGet]
        public HttpResponseMessage DaGetCourierCompanySummary()
        {
            MdlCourierCompanySummary values = new MdlCourierCompanySummary();
            objDaAccess.DaGetCourierCompanySummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
