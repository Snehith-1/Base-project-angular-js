using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to third party API to view the Map view of the location provided by the customer to the client 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>


    [RoutePrefix("api/AgrGoogleMapsAPI")]
    [Authorize]
    public class AgrGoogleMapsAPIController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrGoogleMapsAPI objDaGoogleMapsAPI = new DaAgrGoogleMapsAPI();

        [AllowAnonymous]
        [ActionName("GetGeoCoding")]
        [HttpGet]
        public HttpResponseMessage GetGeoCoding(string address)
        {            
            var response = objDaGoogleMapsAPI.DaGetGeoCoding(address);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [AllowAnonymous]
        [ActionName("GetStaticMapUrl")]
        [HttpGet]
        public HttpResponseMessage GetStaticMap(string latitude, string longitude)
        {         
            var response = objDaGoogleMapsAPI.DaGetStaticMapUrl(latitude, longitude);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [AllowAnonymous]
        [ActionName("GetPlaceImage")]
        [HttpGet]
        public HttpResponseMessage GetPlaceImage(string address)
        {            
            var response = objDaGoogleMapsAPI.DaGetPlaceImage(address);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


    }
}