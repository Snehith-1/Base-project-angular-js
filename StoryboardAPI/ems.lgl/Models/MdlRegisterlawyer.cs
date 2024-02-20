using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.lgl.Models
{
    public class resultvalue
    {
        public bool status { get; set; }
        public string message { get; set; }

    }
    public class mdllawyer : result
    {
        public List<lawyer_list> lawyer_list { get; set; }
      
    }

    public class lawyer_list
    {
        public string lawyer_refno { get; set; }
        public string lawyer_name { get; set; }
        public string mobile_no { get; set; }
        public string date_enroll { get; set; }
        public string experience { get; set; }
        public string lawyerregister_gid { get; set; }
        public string remarks { get; set; }
        public string lawyeruser_status { get; set; }
        public string enrolment_no { get; set; }
        public string login_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class mdlRegisterlawyer:result
    {
        public string lawyerref_no { get; set; }
        public string lawyer_name { get; set; }
        public string  dob { get; set; }
        public string gender { get; set; }
        public Double mobile_no { get; set; }
        public Double telephone_no { get; set; }
        public string email_address { get; set; }
        public string educational_qualification { get; set; }
        public string date_enrolment { get; set; }
        public string pan_no { get; set; }
        public string experience { get; set; }
        public string place_practice { get; set; }
        public string enrolment_certificate { get; set; }
        public string lawyer_photo { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public Double postal_code { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string lawyerregister_gid { get; set; }
        public string enrolment_no { get; set; }
        public string aadhar_no { get; set; }
        public string bank_name { get; set; }
        public string account_no { get; set; }
        public string ifsc_code { get; set; }
        public List<UploadDocumentList> filename_list { get; set; }
    }

    public class lawyeredit : result
    {
        public string lawyerregister_gid { get; set; }
        public string txtlawyerref_noedit { get; set; }
        public string txtlawyernameedit { get; set; }
        public string dobedit { get; set; }
        public DateTime dob_edit { get; set; }
      
        public string genderedit { get; set; }
        public Double txtmobilenoedit { get; set; }
        public Double txttelephonenoedit { get; set; }
        public string txtemailaddressedit { get; set; }
        public string txtqualificationedit { get; set; }
        public string txtdateenrol_counciledit { get; set; }
      
        public DateTime txt_dateenrol_counciledit { get; set; }
        public string txtpannoedit { get; set; }
        public string txtexperienceedit { get; set; }
        public string txtplace_practiceedit { get; set; }
        public string txtaddress1edit { get; set; }
        public string txtaddress2edit { get; set; }
        public string txtstateedit { get; set; }
        public string txtcountryedit { get; set; }
        public Double txtpostalcodeedit { get; set; }
        public string txtenrolment_no { get; set; }
        public string aadhar_no { get; set; }
        public string bank_name { get; set; }
        public string account_no { get; set; }
        public string ifsc_code { get; set; }
        public List<UploadDocumentList> UploadDocumentList { get; set; }

    }
    public class UploadDocumentname : result
    {
        public string lawyerref_no { get; set; }
        public string lawyer_name { get; set; }
        public string lawyeruser_status { get; set; }
        public string lawyeruser_password { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public Double mobile_no { get; set; }
        public Double telephone_no { get; set; }
        public string email_address { get; set; }
        public string educational_qualification { get; set; }
        public string date_enrolment { get; set; }
        public string pan_no { get; set; }
        public string experience { get; set; }
        public string place_practice { get; set; }
        public string enrolment_certificate { get; set; }
        public string lawyerphoto_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string lawyerphoto_path { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public Double postal_code { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string lawyerregister_gid { get; set; }
        public string enrolment_no { get; set; }
        public string block_remarks { get; set; }
        public string aadhar_no { get; set; }
        public string bank_name { get; set; }
        public string account_no { get; set; }
        public string ifsc_code { get; set; }
        public List<UploadDocumentList> UploadDocumentList { get; set; }
      
    }
    public class UploadDocumentList
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_gid { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }
  

    public class lawyerlogin : result
    {
        public string lawyeruser_code { get; set; }
        public string lawyer_userpassword { get; set; }
        public string lawyeruser_email { get; set; }
        public string lawyerregister_gid { get; set; }
        public string lawyeruser_activation { get; set; }
        public string blockremarks { get; set; }
        public string lawyeruser_password { get; set; }
    }
}