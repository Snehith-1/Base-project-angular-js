﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.masterng.Models;
using ems.masterng.DataAccess;


namespace ems.master.Controllers
{
    [RoutePrefix("api/GoogleMapsAPINg")]
    [Authorize]
    public class GoogleMapsAPINgController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaGoogleMapsAPINg objDaGoogleMapsAPI = new DaGoogleMapsAPINg();

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