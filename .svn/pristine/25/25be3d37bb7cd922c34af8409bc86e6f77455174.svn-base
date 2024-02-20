using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.lgl.Models
{

    public class loanlist : result
    {
        public List<loandtl> loandtl { get; set; }
    }

    public class loandtl : result
    {
        public string loanref_no { get; set; }
        public string loan_title { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string vertical { get; set; }
    }

    public class UploadDocument_name : result
    {
        public List<UploadDocumentModel> filename_list { get; set; }
    }

    public class UploadDocumentModel
    {
        public string customer_documentgid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class composemail_list : result
    {
     public List<composemail> composemail { get; set; }
    }

    public class composemail : result
    {
        public string composemail_gid { get; set; }
        public string from_mail { get; set; }
        public string to_mail { get; set; }
        public string cc_mail { get; set; }
        public string bcc_mail { get; set; }
        public string subject_mail { get; set; }
        public string content_mail { get; set; }
        public string customer_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class demandnotice : result
    {
        public List<demandnotice1> demandnotice1 { get; set; }
        public List<demandnotice2> demandnotice2 { get; set; }
        public List<demandnotice3> demandnotice3 { get; set; }
    }
    public class demandnotice1 : result
    {
        public string urn_number { get; set; }
        public string OD_days { get; set; }
        public string DN_status { get; set; }
    }
    public class demandnotice2 : result
    {
        public string urn_number { get; set; }
        public string OD_days { get; set; }
        public string DN_status { get; set; }
    }
    public class demandnotice3 : result
    {
        public string urn_number { get; set; }
        public string OD_days { get; set; }
        public string DN_status { get; set; }
    }

    public class invoicedtlList : result
    {
        public List<invoicedtl> invoicedtl { get; set; }
    }

    public class invoicedtl : result
    {
        public string lawyerinvoice_gid { get; set; }
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
        public List<UploadDocumentModel> uploaddocument { get; set; }
    }

    public class assignSRLawyer:result
    {
        public string legalSR_gid { get; set; }
        public string assign_lawyername { get; set; }
        public string assign_lawyergid { get; set; }
        public string assignedlawyer_by { get; set; }
        public string assigned_date { get; set; }
        public string SLN_remarks { get; set; }
        public List<uploadseek_list> uploadseek_list { get; set; }
    }

}