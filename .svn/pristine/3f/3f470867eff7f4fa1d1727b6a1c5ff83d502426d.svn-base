using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{
   public class externalvendorList :result
    {
        public List<externalvendordtl> externalvendordtl { get; set;}
    }

    public class externalvendordtl:result
    {
        public string externalregister_gid { get; set; }
        public string external_vendorcode { get; set; }
        public string external_vendorname { get; set; }
        public string contact_person { get; set; }
        public string contact_emailid { get; set; }
        public double contact_number { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string state_gid { get; set; }
        public string state_name { get; set; }
        public string district_gid { get; set; }
        public string district_name { get; set; }
        public string country_name { get; set; }
        public string postal_code { get; set; }
        public string external_status { get; set; }
        public string photo_path { get; set; }
    }

    public class externalVendorlogin :result
    {
        public string external_vendorCode { get; set; }
        public string external_Vendorname { get; set; }
        public string external_vendorPassword { get; set; }
        public string external_activeStatus { get; set; }
        public string externalregister_gid { get; set; }
    }

    public class externalphoto :result
    {
        public string external_registergid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<Rskexternalvendordoc> Rskexternalvendordoc { get; set; }
    }

    public class Rskexternalvendordoc : result
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
        //public string document_path { get; set; }
    }

}