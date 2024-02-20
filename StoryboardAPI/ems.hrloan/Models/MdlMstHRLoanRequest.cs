using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrloan.Models
{
    public class MdlMstHRLoanRequest : result
     {
        public List<fintype_list> fintype_list { get; set; }
        public List<purpose_list> purpose_list { get; set; }
        public List<severity_list> severity_list { get; set; }
        public List<hrloanrequest> hrloanrequest { get; set; }        
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
        public string request_gid { get; set; }
        public string pendingrequests_count { get; set; }
        public string completedrequests_count { get; set; }
        public string rejectedrequests_count { get; set; }
        public string withdrawn_count { get; set; }
        public string totalcount { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        
    }
    public class fintype_list : result
    {
        public string fintype_gid { get; set; }
        public string fintype_name { get; set; }       
       
    }
    public class purpose_list : result
    {
        public string purpose_gid { get; set; }
        public string purpose_name { get; set; }
        public string purpose_note { get; set; }
        public string mandatory { get; set; }

    }
    public class severity_list : result
    {
        public string severity_gid { get; set; }
        public string severity_name { get; set; }       

    }
    public class uploaddocument : result
    {
        public string request_gid { get; set; }
        public List<upload_list> upload_list { get; set; }
        public string hrreqdocument_gid { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string[] compfilename { get; set; }
        public string compfilepath { get; set; }
        public string[] forwardfilename { get; set; }
        public string forwardfilepath { get; set; }

        public string[] doufilename { get; set; }
        public string doufilepath { get; set; }
    }
    public class MdlHRLoanDocumentUpload : result
    {
        public string hrreqdocument_gid { get; set; }
        public string request_gid { get; set; }
        public string tmp_documentGid { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }        
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public List<upload_list> upload_list { get; set; }
    }
    public class upload_list
    {
        public string hrreqdocument_gid { get; set; }
        public string request_gid { get; set; }
        public string tmp_documentGid { get; set; }
        public string document_id { get; set; }
        public string document_title { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }

    }
    public class hrloanrequest : result
    {
       
        public string request_gid { get; set; }
        public string request_reason { get; set; }        
        public string tenure { get; set; }
        public string interest { get; set; }
        public string severity_gid { get; set; }
        public string severity_name { get; set; }
        public string purpose_name { get; set; }
        public string purpose_gid { get; set; }
        public string amount { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status_updatedby { get; set; }
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
        public string raisequery_status { get; set; }
        public string withdraw_flag { get; set; }
        public string withdraw_remarks { get; set; }        
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string hrloanpurpose_gid { get; set; }
        public string hrloanpurpose_name { get; set; }
        public string purpose_note { get; set; }
        public string mandatory { get; set; }

    }



}