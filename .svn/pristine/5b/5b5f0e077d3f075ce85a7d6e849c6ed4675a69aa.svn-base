using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to import bulk datas in Buyer & supplier Onboard stage.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K </remarks>


    [RoutePrefix("api/AgrMstBuyerOnboardExcelDataMigration")]
    [Authorize]

    public class AgrMstBuyerOnboardExcelDataMigrationController : ApiController
    {

        DaBuyerOnboardExcelDataMigration objDaExcelDataMigration = new DaBuyerOnboardExcelDataMigration();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("OnboardDataImport")]
        [HttpPost]
        public HttpResponseMessage OnboardDataImport()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaExcelDataMigration.DaOnboardDataImport(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("SupplierOnboardDataImport")]
        [HttpPost]
        public HttpResponseMessage SupplierOnboardDataImport()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaExcelDataMigration.DaSupplierOnboardDataImport(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
    }
}