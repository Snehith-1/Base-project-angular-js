using System;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using System.Drawing;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various events (Get Geo Coding, Static Map Url
    /// Place Image) in Google MapsAPI.
    /// </summary>
    /// <remarks>Written by Praveen </remarks>
    public class DaAgrGoogleMapsAPI
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();

        public GeoCodingResponse DaGetGeoCoding(string address)
        {
            GeoCodingResponse ObjGeoCodingResponse = new GeoCodingResponse();
            
                string basicURL = ConfigurationManager.AppSettings["geocodingapiurl"].ToString();
                string addressString = "address=" + address.Replace(" ", "+");
                string keyString = "key=" + ConfigurationManager.AppSettings["googlemapsapikey"].ToString();
                
                string requestUri = String.Concat(basicURL + addressString + "&" + keyString);

                var client = new RestClient(requestUri);
                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);
                ObjGeoCodingResponse = JsonConvert.DeserializeObject<GeoCodingResponse>(response.Content);
          
            return ObjGeoCodingResponse;
        }

        public string DaGetStaticMapUrl(string latitude, string longitude)
        {
            string basicURL = ConfigurationManager.AppSettings["staticmapapiurl"].ToString();
            string locationData = String.Concat("center=", latitude, ",", longitude);
            string zoomData = String.Concat("zoom=", ConfigurationManager.AppSettings["staticmapzoomlevel"].ToString());
            string sizeData = String.Concat("size=", ConfigurationManager.AppSettings["staticmapimagesize"].ToString());
            string keyData = String.Concat("key=", ConfigurationManager.AppSettings["googlemapsapikey"].ToString());
            string markerData = String.Concat("markers=color:red|label:S|", latitude, ",", longitude);

            string StaticMapUrl = String.Concat(basicURL, locationData, "&", zoomData, "&", sizeData, "&", markerData, "&", keyData);

            return StaticMapUrl;
        }

        public string[] DaGetPlaceImage(string address)
        {
            FindPlaceResponse ObjFindPlaceResponse = new FindPlaceResponse();
            PlaceDetailsResponse ObjPlaceDetailsResponse = new PlaceDetailsResponse();
            string[] PlacePhotoUrlList = new string[10];

            string basicURLPlace = ConfigurationManager.AppSettings["findplaceapiurl"].ToString();
            string fieldsString = "fields=place_id";
            address = address.Replace(" ", "+");
            address = address.Replace(",", "%2C");
            string inputString = "input=" + address;
            string inputtypeString = "inputtype=textquery";
            string keyString = "key=" + ConfigurationManager.AppSettings["googlemapsapikey"].ToString();

            string requestUriPlace = String.Concat(basicURLPlace, fieldsString, "&", inputString, "&", inputtypeString, "&", keyString);

            var client = new RestClient(requestUriPlace);
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            ObjFindPlaceResponse = JsonConvert.DeserializeObject<FindPlaceResponse>(response.Content);

            if(ObjFindPlaceResponse.status == "OK")
            {
                string basicURLPlaceDetail = ConfigurationManager.AppSettings["placedetailapiurl"].ToString();
                string fieldspdString = "fields=photos";
                string placeidString = "place_id=" + ObjFindPlaceResponse.candidates[0].place_id;

                string requestUriPlaceDetail = String.Concat(basicURLPlaceDetail, fieldspdString, "&", placeidString, "&", keyString);

                var clientpd = new RestClient(requestUriPlaceDetail);
                var requestpd = new RestRequest(Method.POST);
                IRestResponse responsepd = clientpd.Execute(requestpd);
                ObjPlaceDetailsResponse = JsonConvert.DeserializeObject<PlaceDetailsResponse>(responsepd.Content);

                if(ObjPlaceDetailsResponse.status == "OK")
                {
                    string basicURLPhoto = ConfigurationManager.AppSettings["placephotoapiurl"].ToString();
                    string maxwidthData = "maxwidth=400";
                    if(ObjPlaceDetailsResponse.result.photos != null)
                    {
                        for (int i = 0; i < ObjPlaceDetailsResponse.result.photos.Length; i++)
                        {
                            string photoreferenceData = "photo_reference=" + ObjPlaceDetailsResponse.result.photos[i].photo_reference;
                            PlacePhotoUrlList[i] = String.Concat(basicURLPhoto, maxwidthData, "&", photoreferenceData, "&", keyString);
                        }
                    }                  
                }               
            }       
            return PlacePhotoUrlList;
        }

    }
}