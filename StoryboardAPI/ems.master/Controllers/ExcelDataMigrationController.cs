using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

/// <summary>
/// (It's used for Samfin IPL Migration)Samfin IPL Migration Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sherin,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/ExcelDataMigration")]
    [Authorize]

    public class ExcelDataMigrationController : ApiController
    {
        DaExcelDataMigration objDaExcelDataMigration = new DaExcelDataMigration();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("ImportApplicationCreationData")]
        [HttpPost]
        public HttpResponseMessage ImportApplicationCreationData()
        {
            HttpRequest httpRequest; 
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaExcelDataMigration.DaImportApplicationCreationData(httpRequest, getsessionvalues.employee_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

    }
}