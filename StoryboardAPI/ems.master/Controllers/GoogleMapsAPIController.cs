using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.master.Models;
using ems.master.DataAccess;

/// <summary>
/// (It's used for GoogleMapsAPI)GoogleMapsAPI Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Praveen Raj </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/GoogleMapsAPI")]
    [Authorize]
    public class GoogleMapsAPIController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaGoogleMapsAPI objDaGoogleMapsAPI = new DaGoogleMapsAPI();

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