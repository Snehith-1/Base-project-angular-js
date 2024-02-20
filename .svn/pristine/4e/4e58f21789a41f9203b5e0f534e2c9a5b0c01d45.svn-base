using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class MdlTrnHRLoanHRVerifications : result
    {
        public string hrverify_remarks { get; set; }
        public string hrdocverify_remarks { get; set; }
        public string request_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string role { get; set; }
        public string department { get; set; }
        public string reporting_manager { get; set; }
        public string functional_head { get; set; }
        public string functionalhead_gid { get; set; }
        public string hr_head { get; set; }
        public string hrhead_gid { get; set; }
        public string reportingmgr_gid { get; set; }
        public string department_gid { get; set; }
        public string official_mailid { get; set; }
        public string official_mobileno { get; set; }
        public string pers_mailid { get; set; }
        public string pers_mobileno { get; set; }
        public string mangopenquerycount { get; set; }
        public string pendinghrVerify_count { get; set; }
        public string approvedhrVerify_count { get; set; }
        public string rejectedhrVerify_count { get; set; }
        public string totalcount { get; set; }

        public string mangraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string close_remarks { get; set; }
        public string raisequery_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string queryresponse_remarks { get; set; }
        public string query_responseby { get; set; }
        public string  approved_interest { get; set; }
        public string approved_tenure { get; set; }
        public string approvedtenure_startdate { get; set; }
        public string approvedtenure_enddate { get; set; }

        public string[] termsandconditionslist_gid { get; set; }
        

        public List<verifications_summary> verifications_summary { get; set; }        
        public List<hrtermsandconditions_list> hrtermsandconditions_list { get; set; }
        public List<hrdocumentname_list> hrdocumentname_list { get; set; }
        public List<HRDocument_list> HRDocument_list { get; set; }
        public List<mangraisequery_list> mangraisequery_list { get; set; }
    }
    public class Mdltermsandcondt : result
    {
        public List<hrtermsandconditions_list> hrtermsandconditions_list { get; set; }
        public string tc_flag { get; set; }
    }
    public class verifications_summary : result
    {

        public string request_gid { get; set; }
        public string request_reason { get; set; }
        public string tenure { get; set; }
        public string severity_gid { get; set; }
        public string severity_name { get; set; }
        public string purpose_name { get; set; }
        public string purpose_gid { get; set; }
        public string amount { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string drm_status { get; set; }
        public string raised_department { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string department_name { get; set; }
        public string employee_role { get; set; }
        public string employee_name { get; set; }
        public string fintype_name { get; set; }
        public string fintype_gid { get; set; }
        public string request_refno { get; set; }
        public string request_status { get; set; }
        public string user_gid { get; set; }
        public string functional_head { get; set; }
        public string functionalhead_gid { get; set; }
        public string hr_head { get; set; }
        public string hrhead_gid { get; set; }
        public string reportingmgr_gid { get; set; }
        public string department_gid { get; set; }
        public string reporting_mgr { get; set; }
        public string Querystatus { get; set; }
    }   
    public class hrtermsandconditions_list
    {
        public string hrloantermsandconditions_gid { get; set; }
        public string hrloantermsandconditions_name { get; set; }
    }
    public class hrdocumentname_list
    {
        public string hrdocument_gid { get; set; }
        public string hrdocument_name { get; set; }
    }
    public class MdlHRLoanDocumentUpload1 : result
    {
        public string hrspecialdocument_gid { get; set; }        
        public string request_gid { get; set; }
        public string document_id { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public List<HRDocument_list> HRDocument_list { get; set; }       
    }
    public class uploaddocument1 : result
    {
        public string request_gid { get; set; }
        public string hrspecialdocument_gid { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }
        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
        public List<HRDocument_list> HRDocument_list { get; set; }       
    }
    public class HRDocument_list
    {
        public string hrspecialdocument_gid { get; set; }
        public string request_gid { get; set; }
        public string document_id { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }

    }
    public class MdlHRLoanPaymentDocumentUpload : result
    {       
        public string hrrepaymentdocument_gid { get; set; }
        public string request_gid { get; set; }
        public string document_id { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string approved_interest { get; set; }
        public string  approved_tenure { get; set; }
        public string approvedtenure_startdate { get; set; }
        public string approvedtenure_enddate { get; set; }
        public List<PaymentDocument_list> PaymentDocument_list { get; set; }
        //public List<approved_list> approved_interest { get; set; }
    }

    public class approved_list : result
    {
        public string approved_interest { get; set; }
    }
        public class uploadpaymentdocument : result
    {
        public string request_gid { get; set; }
        public string hrrepaymentdocument_gid { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }
        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
        public List<PaymentDocument_list> PaymentDocument_list { get; set; }
    }
    public class PaymentDocument_list
    {
        public string hrrepaymentdocument_gid { get; set; }
        public string request_gid { get; set; }       
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string approved_interest { get; set; }
    }
    public class mangraisequery_list : result
    {
        public string request_gid { get; set; }
        public string mangraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string close_remarks { get; set; }
        public string raisequery_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string queryresponse_remarks { get; set; }
        public string query_responseby { get; set; }
    }
   
   
}