using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// (It's used for GoogleMapsAPI) GoogleMapsAPI Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj</remarks>

namespace ems.master.Models
{
    public class GeoCodingResponse
    {
        public GeoCodingResult[] results { get; set; }
        public string status { get; set; }
    }

    public class GeoCodingResult
    {
        public AddressComponent[] AddressComponents { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public string[] types { get; set; } 
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public ViewPort viewport { get; set; }
    }

    public class Location
    {
        public string lat { get; set; }
        public string lng { get; set; }
       
    }
    public class ViewPort
    {
        public NorthEast northeast { get; set; }
        public SouthWest southwest { get; set; }
    }
    public class NorthEast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class SouthWest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class PlusCode
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class FindPlaceResponse
    {
        public Candidates[] candidates { get; set; }
        public string status { get; set; }
    }

    public class Candidates
    {        
        public Photos[] photos { get; set; }
        public string place_id { get; set; }
    }

    public class Photos
    {
        public int height { get; set; }
        public string[] html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class PlaceDetailsResponse
    {
        public string[] html_attributions { get; set; }
        public ResultPlaceDetails result { get; set; }
        public string status { get; set; }
    }

    public class ResultPlaceDetails
    {
        public Photos[] photos { get; set; }       
    }

}