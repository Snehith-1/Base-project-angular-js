using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.lp.Models
{
    public class invoicedtlList : result
    {
        public string case_type { get; set; }
        public string caseref_no { get; set; }
        public List<invoicedtl> invoicedtl { get; set; }
    }

    public class invoicedtl : result
    {
        public string lawyerinvoice_gid { get; set; }
        public string lawyerinvoicedtl_gid { get; set; }
        public string invoice_refno { get; set; }
        public string invoice_date { get; set; }
        public string invoice_amount { get; set; }
        public string invoice_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string case_type { get; set; }
        public string caseref_no { get; set; }
        public string servicerender_date { get; set; }
        public string service_type { get; set; }
        public string invoice_status { get; set; }
        public string servicetype_gid { get; set; }
        public string caseref_gid { get; set; }
        public string serviceypeothers_title { get; set; }
        public string invoice_count { get; set; }
        public DateTime servicerenderdate { get; set; }
        public List<UploadDocumentModel> uploaddocument { get; set; }
    }


    public class UploadDocument_name : result
    {
        public List<UploadDocumentModel> filename_list { get; set; }
    }

    public class UploadDocumentModel
    {
        public string tmpinvoice_documentgid { get; set; }
        public string invoice_documentgid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }
    public class Mslcasedtl :result
    {
        public string case_type { get; set; }
        public List<cases_list> cases_list { get; set; }
        public List<casestype_list> casestype_list { get; set; }
    }
    public class cases_list
    {
        public string caseref_no { get; set; }
        public string caseref_gid { get; set; }
    }
    public class casestype_list
    {
        public string casetype_gid { get; set; }
        public string case_type { get; set; }
    }
}