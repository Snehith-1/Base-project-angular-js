using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.lgl.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class MdlLawfirm : result
    {
        public string lawfirm_gid { get; set; }
        public string lawyerregister_gid { get; set; }
        public string firm_refno { get; set; }
        public string firm_name { get; set; }
        public string contact_details{ get; set; }
        public string mail_address { get; set; }
        public string fax_no { get; set; }
        public string firm_years { get; set; }
        public string firm_address { get; set; }
        public string lawyer { get; set; }
        public string lawyer_refno { get; set; }
        public string remarks { get; set; }
        public string website { get; set; }
        public List<lawfirm2lawyer_list> lawfirm2lawyer_list { get; set; }
    }
    public class mdllawfirmdtl : result
    {
        public List<lawfirm_list> lawfirm_list { get; set; }

    }
   

    public class lawfirmedit : result
    {
        public string lawfirm_gid { get; set; }
        public string txtfirm_refnoedit { get; set; }
        public string txtfirm_nameedit { get; set; }
        public string txtcontact_noedit { get; set; }
        public string txtemailaddressedit { get; set; }
        public string txtfaxnoedit { get; set; }
        public string txtexperienceedit { get; set; }
        public string txtaddress1edit { get; set; }
        public string txtremarksedit {get; set; }
        public string txtwebsiteedit { get; set; }
        public List<UploadDocument> UploadDocument { get; set; }
        public List<lawyerlist_edit> lawyerlist_edit { get; set; }
    }
    public class lawyerlist_edit
    {
        public string depAppGid { get; set; }
        public string lawyerregister_gid { get; set; }
        public string lawyer_name { get; set; }
        public string status { get; set; }
     
    }
    public class Lawfirmupload : result
    {
        public string lawfirm_gid { get; set; }
        public string firm_refno { get; set; }
        public string firm_name { get; set; }
        public string contact_details { get; set; }
        public string mail_address { get; set; }
        public string fax_no { get; set; }
        public string firm_years { get; set; }
        public string firm_address { get; set; }
        public string bankdocument_path { get; set; }
        public string bankdocument_name { get; set; }
        public string remarks { get; set;}
        public string website { get; set; }
        public string lawfirmuser_status { get; set; }
        public string lawfirmuser_password { get; set; }
        public string block_remarks { get; set; }
        public List<UploadDocument> UploadDocument { get; set; }
         public List<lawfirm_list> lawfirm_list { get; set; }
    }
    public class lawfirm_list
    {
        public string firm_refno { get; set; }
        public string firm_name { get; set; }
        public string email_address { get; set; }
        public string no_years { get; set; }
        public string lawfirm_gid { get; set; }
        public string created_date { get; set; }
        public string lawyerref_no { get; set; }
        public string lawyer_name { get; set; }
        public string mobile_no { get; set; }
        public string date_enrolment { get; set; }
        public string experience { get; set; }
        public string lawfirmuser_status { get; set; }
        public string created_by { get; set; }
    }
    public class UploadDocument : result
    {
        public string filename { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string lawfirm_gid { get; set; }
        public string document_gid { get; set; }
        public string lawyerref_no { get; set; }
        public string lawyer_name { get; set; }
        public string mobile_no { get; set; }
        public string date_enrolment { get; set; }
        public string bankdocument_path { get; set; }
        public string bankdocument_name { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }

    }
    public class document_cancel : result
    {

    }
    public class Lawfirm2lawyer : result
    {
        public List<lawfirm2lawyer_list> lawfirm2lawyer_list { get; set; }

    }
    public class lawfirm2lawyer_list
    {
        public string label { get; set; }
        public string lawyer_name { get; set; }
        public string lawyerregister_gid { get; set; }

    }
    public class mdlmembername:result
    {
        public string lawyer_name { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string designation { get; set; }
        public string remarks { get; set; }
        public string enrolment_no { get; set; }
        public string lawfirmmember_gid { get; set; }
        public string lawyerregister_gid { get; set; }
        public List<member_list> member_list { get; set; }
    }
    public class member_list
    {
        public string lawyer_name { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string designation { get; set; }
        public string remarks { get; set; }
        public string enrolment_no { get; set; }
        public string lawfirmmember_gid { get; set; }
        public string lawfirm_gid { get; set; }
    }
    public class lawfirmlogin : result
    {
        public string lawfirmuser_code { get; set; }
        public string lawfirm_userpassword { get; set; }
        public string lawfirmruser_email { get; set; }
        public string lawfirm_gid { get; set; }
        public string lawfirmuser_activation { get; set; }
        public string blockremarks { get; set; }
        public string lawfirmuser_password { get; set; }
    }
}

