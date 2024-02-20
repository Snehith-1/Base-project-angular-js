using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// (It's used for Business Unit Master)Bureau API Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>
namespace ems.master.Controllers
{
    [RoutePrefix("api/BusinessUnit")]
    [Authorize]

    public class BusinessUnitController : ApiController
    {
        DaBusinessUnit objDaBusinessUnit = new DaBusinessUnit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetBusinessUnit")]
        [HttpGet]
        public HttpResponseMessage getBusinessUnit()
        {
            MdlBusinessUnit objMdlBusinessUnit = new MdlBusinessUnit();
            objDaBusinessUnit.DaGetBusinessUnit(objMdlBusinessUnit);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlBusinessUnit);
        }

        [ActionName("CreateBusinessUnit")]
        [HttpPost]
        public HttpResponseMessage createBusinessUnit(businessunit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBusinessUnit.DaCreateBusinessUnit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBusinessUnit")]
        [HttpGet]
        public HttpResponseMessage editbusinessunit(string businessunit_gid)
        {
            businessunit values = new businessunit();
            objDaBusinessUnit.DaEditBusinessUnit(businessunit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBusinessUnit")]
        [HttpPost]
        public HttpResponseMessage updatebusinessunit(businessunit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBusinessUnit.DaUpdateBusinessUnit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBusinessUnit")]
        [HttpGet]
        public HttpResponseMessage deleteBusinessUnit(string businessunit_gid)
        {
            businessunit values = new businessunit();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBusinessUnit.DaDeleteBusinessUnit(businessunit_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //------Get Business Unit - order by asc--------//
        [ActionName("GetBusinessUnitASC")]
        [HttpGet]
        public HttpResponseMessage getBusinessUnitASC()
        {
            MdlBusinessUnit objMdlBusinessUnit = new MdlBusinessUnit();
            objDaBusinessUnit.DaGetBusinessUnitASC(objMdlBusinessUnit);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlBusinessUnit);
        }
        [ActionName("BusinessUnitStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage BusinessUnitStatusUpdate(businessunit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBusinessUnit.DaBusinessUnitStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string businessunit_gid)
        {
            MdlBusinessUnit values = new MdlBusinessUnit();
            objDaBusinessUnit.DaGetActiveLog(businessunit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}