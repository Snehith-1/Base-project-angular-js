using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


/// <summary>
/// (It's used for Constitution Master)Constitution Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>>Written by Sumala and Logapriya </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/Constitution")]
    [Authorize]

    public class ConstitutionController : ApiController
    {
        DaConstitution objDaConstitution = new DaConstitution();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("Getconstitution")]
        [HttpGet]
        public HttpResponseMessage getconstitution()
        {
            MdlConstitution objMdlConstitution = new MdlConstitution();
            objDaConstitution.DaGetconstitution(objMdlConstitution);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlConstitution);
        }

        [ActionName("CreateConstitution")]
        [HttpPost]
        public HttpResponseMessage CreateConstitution(constitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConstitution.DaCreateConstitution(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditConstitution")]
        [HttpGet]
        public HttpResponseMessage editconstitution(string constitution_gid)
        {
            constitution values = new constitution();
            objDaConstitution.DaEditConstitution(constitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateConstitution")]
        [HttpPost]
        public HttpResponseMessage updateconstitution(constitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConstitution.DaUpdateConstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteConstitution")]
        [HttpGet]
        public HttpResponseMessage deleteconstitution(string constitution_gid)
        {
            constitution values = new constitution();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConstitution.DaDeleteConstitution(constitution_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //-----------Get Constitution - order by ASC----------//
        [ActionName("GetconstitutionASC")]
        [HttpGet]
        public HttpResponseMessage getConstitutionASC()
        {
            MdlConstitution objMdlConstitution = new MdlConstitution();
            objDaConstitution.DaGetconstitutionASC(objMdlConstitution);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlConstitution);
        }
        [ActionName("constitutionStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage VerticalStatusUpdate(constitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaConstitution.DaconstitutionStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string constitution_gid)
        {
            MdlConstitution values = new MdlConstitution();
            objDaConstitution.DaGetActiveLog(constitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Constitution List Export
        [ActionName("ExportConstitution")]
        [HttpGet]
        public HttpResponseMessage GetConstitutionData()
        {
            constitutionSummary objconstitutionSummary = new constitutionSummary();
            objDaConstitution.DaGetConstitutionData(objconstitutionSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objconstitutionSummary);
        }
    }
}