using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ems.hrm.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class countryname:result
    {
      public List<countryname_list> countryname_list { get; set; }
    }
    public class countryname_list 
    {
        public string country_gid { get; set; }
        public string country_name { get; set; }
    }

    public class employeedetails : result
    {
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string designation { get; set; }
        public string entity { get; set; }
        public string department { get; set; }
        public string branch { get; set; }
        public string joining_date { get; set; }
        public string user_status { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }
        public Double mobile { get; set; }
        public string employee_emailid { get; set; }
        public Double personal_number { get; set; }
        public string qualification { get; set; }
        public string experience { get; set; }
        public string blood_group { get; set; }
        public string permanent_address1 { get; set; }
        public string permanent_address2 { get; set; }
        public string permanent_city { get; set; }
        public string permanent_state { get; set; }
        public string permanent_country { get; set; }
        public Double permanent_postalcode { get; set; }
        public string temporary_address1 { get; set; }
        public string temporary_address2 { get; set; }
        public string temporary_city { get; set; }
        public string temporary_state { get; set; }
        public string temporary_country { get; set; }
        public Double temporary_postalcode { get; set; }
        public string employeereporting_to { get; set; }
        public string employeereporting_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_photo { get; set; }
       
    }
    public class employeePhotoUpload:result
    {
        public string filename { get; set; }
        public List<UploadDocumentList> filename_list { get; set; }
    }
 
    public class UploadDocumentList
    {
        public string filename { get; set; }
        public string path { get; set; }
        public string employee_photo { get; set; }
    }

    public class updatepassword : result
    {
        public string current_password { get; set; }
        public string new_password { get; set; }
        public string confirm_passsword { get; set; }
    }
}
