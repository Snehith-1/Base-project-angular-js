using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.hrm.Models;
using ems.hrm.DataAccess;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace StoryboardAPI.Controllers.ems.hrm
{
    [AllowAnonymous]
    [RoutePrefix("api/geoMap")]

    public class geoMapController : ApiController
    {
        [HttpPost]
        [ActionName("locate")]
        public HttpResponseMessage GetLocation(getLocation values)
        {
            address objResult = new address();
            StringBuilder sb = new StringBuilder();
            sb.Append(values.lat);
            sb.Append(",");
            sb.Append(values.lon);
            var client = new RestSharp.RestClient("https://atlas.microsoft.com/search/address/reverse/json");
            var request = new RestRequest(Method.GET);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("api-version", "1.0");
            request.AddParameter("subscription-key", "uYsh9CG084Em15TPYkTRUtaBHu0VPYijY4I0JNG-M5M");
            request.AddParameter("query", sb);
            IRestResponse response = client.Execute(request);
            sb.Clear();
            var address = response.Content;
            dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(address);
            objResult.buildingNumber = newobj.addresses[0].address.buildingNumber;
            objResult.streetNumber = newobj.addresses[0].address.streetNumber;
            objResult.street = newobj.addresses[0].address.street;
            objResult.streetName = newobj.addresses[0].address.streetName;
            objResult.countryCode = newobj.addresses[0].address.countryCode;
            objResult.countrySubdivision = newobj.addresses[0].address.countrySubdivision;
            objResult.countrySecondarySubdivision = newobj.addresses[0].address.countrySecondarySubdivision;
            objResult.municipality = newobj.addresses[0].address.municipality;
            objResult.postalCode = newobj.addresses[0].address.postalCode;
            objResult.freeformAddress = newobj.addresses[0].address.freeformAddress;
            objResult.municipalitySubdivision = newobj.addresses[0].address.municipalitySubdivision;
            objResult.country = newobj.addresses[0].address.country;
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

    }
}
