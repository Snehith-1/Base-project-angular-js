using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.master.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using ems.master.DataAccess;

/// <summary>
/// (It's used for pages in CibilData )CibilData Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCibilData")]
    [Authorize]
    public class MstCibilDataController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCibilData objDaMstCibilData = new DaMstCibilData();

        [ActionName("UploadCibil")]
        [HttpPost]
        public HttpResponseMessage UploadCibil()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objsanctiondetails = new result();
            objDaMstCibilData.DaUploadCibil(httpRequest, getsessionvalues.employee_gid, objsanctiondetails);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetails);
        }
        [ActionName("GetCibilUploadSummary")]
        [HttpGet]
        public HttpResponseMessage GetCibilUploadSummary()
        {
            MdlMstCibilData objMdlMstCibilData = new MdlMstCibilData();
            objDaMstCibilData.DaGetCibilUploadSummary(objMdlMstCibilData);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstCibilData);
        }
        [ActionName("GetCibilSummary")]
        [HttpGet]
        public HttpResponseMessage GetCibilSummary(string cibildata_gid)
        {
            MdlCibilSummary objMdlMstCibilData = new MdlCibilSummary();
            objDaMstCibilData.DaGetCibilSummary(objMdlMstCibilData, cibildata_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstCibilData);
        }
        [ActionName("GetEditCibildata")]
        [HttpGet]
        public HttpResponseMessage GetEditCibildata(string cibildatadtl_gid)
        {
            MdlCibilEdit objMdlMstCibilData = new MdlCibilEdit();
            objDaMstCibilData.DaGetEditCibildata(objMdlMstCibilData, cibildatadtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstCibilData);
        }
        [ActionName("PostCibilData")]
        [HttpPost]
        public HttpResponseMessage PostCibilData(MdlCibilEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCibilData.DaPostCibilData(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCibilLog")]
        [HttpGet]
        public HttpResponseMessage GetCibilLog(string cibildata_gid)
        {
            MdlCibilSummary objMdlMstCibilData = new MdlCibilSummary();
            objDaMstCibilData.DaGetCibilLog(objMdlMstCibilData, cibildata_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstCibilData);
        }
    }
}
