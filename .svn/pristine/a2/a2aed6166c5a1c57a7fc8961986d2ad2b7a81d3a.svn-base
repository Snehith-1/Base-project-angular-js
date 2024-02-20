using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.lp.Models
{
    public class mdlRequestcompliance : result
    {
        public string count_compliance { get; set; }
        public string count_legalSR { get; set; }
        public string count_invoice { get; set; }
        public List<requestcompliance_list> requestcompliance_list { get; set; }
        public string requestref_no { get; set; }
        public string request_type { get; set; }
        public string assigned_date { get; set; }
        public DateTime request_dateedit { get; set; }
        public string deadline_date { get; set; }
        public DateTime deadline_dateedit { get; set; }
        public string priority { get; set; }
        public string assigned_by { get; set; }
        public string remarks { get; set; }
        public string requestcompliance_gid { get; set; }
        public string document_type { get; set; }
        public string assign_lawyergid { get; set; }
        public string assign_lawyername { get; set; }
        public string seeklawyer_remarks { get; set; }
        public string request_status { get; set; }
        public string rejected_remarks { get; set; }
        public string requestcompliance2lawyerdtl_gid { get; set; }
        public List<upload_list> upload_list { get; set; }
        public List<document_list> document_list { get; set; }
    }
    public class requestcompliance_list
    {
        public string requestcompliance_gid { get; set; }
        public string requestref_no { get; set; }
        public string request_type { get; set; }
        public string assigned_date { get; set; }
        public string deadline_date { get; set; }
        public string priority { get; set; }
        public string assigned_by { get; set; }
        public string response_flag { get; set; }
        public string request_status { get; set; }
        public string rejected_remarks { get; set; }
        public string requestcompliance2lawyerdtl_gid { get; set; }
    }
    public class uploaddocument : result
    {
        public string uploaddocument_type { get; set; }
        public string uploaddocument_gid { get; set; }
        public string uploadremarks { get; set; }
        public string document_type { get; set; }
        public string requestcompliance_gid { get; set; }
        public string requestcompliance2lawyerdtl_gid { get; set; }
        public List<upload_list> upload_list { get; set; }
        public List<document_list> document_list { get; set; }

    }
    public class upload_list
    {
        public string uploaddocument_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string document_type { get; set; }
        public string correctedfile_name { get; set; }
        public string correctedfile_path { get; set; }
        public string uploadremarks { get; set; }
        public string uploaded_details { get; set; }
        public string lawyerdocument_gid { get; set; }
       
    }
    public class canceldocument : result
    {

    }
    public class document_list
    {
        public string lawyerdocument_gid { get; set; }
        public string uploaddocument_gid { get; set; }
        public string requestcompliance_gid { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string document_type { get; set; }
        public string correctedfile_name { get; set; }
        public string correctedfile_path { get; set; }
        public string lawyer_corrected { get; set; }
        public string remarks { get; set; }
    }
    public class MdlTaggedInfo : result
    {
        public List<taggedinfo_list> taggedinfo_list { get; set; }
        public List<taggeddoc_list> taggeddoc_list { get; set; }
    }
    public class taggedinfo_list
    {
        public string lawyeruser_code { get; set; }
        public string lawyeruser_name { get; set; }
        public string email_address { get; set; }
        public string tagged_date { get; set; }
        public string tagged_by { get; set; }
        public string seek_remarks { get; set; }
        public string requestcompliance2lawyerdtl_gid { get; set; }
    }
    public class taggeddoc_list
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string requestcompliance2lawyerdtl_gid { get; set; }
    }

    public class LegalDtls
    {
        public string lastconversation { get; set; }
        public string newmsg_count { get; set; }
    }
}