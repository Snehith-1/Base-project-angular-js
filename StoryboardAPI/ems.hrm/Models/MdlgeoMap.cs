using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.hrm.Models
{
    public class getLocation
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
    public class address
    {
        public string buildingNumber { get; set; }
        public string streetNumber { get; set; }
        public string street { get; set; }
        public string streetName { get; set; }
        public string countryCode { get; set; }
        public string countrySubdivision { get; set; }
        public string countrySecondarySubdivision { get; set; }
        public string municipality { get; set; }
        public string postalCode { get; set; }
        public string municipalitySubdivision { get; set; }
        public string country { get; set; }
        public string freeformAddress { get; set; }
    }
}
