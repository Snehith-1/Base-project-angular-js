using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.audit.Models
{
    public class MdlAtmTrnSampling : result
    {
        public List<sample_list> sample_list { get; set; }
        public List<sampleview_list> sampleview_list { get; set; }
        public List<SampleAssignedQueryList> SampleAssignedQueryList { get; set; }
        public List<SampleReplyQueryList> SampleReplyQueryList { get; set; }
        public string sampleimport_gid { get; set; }
        public string sample_name { get; set; }
        public string samfin_code { get; set; }
        public string samagro_code { get; set; }
        public string codecreation_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string auditcreation_gid { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }
        public string field9 { get; set; }
        public string field10 { get; set; }
        public string taguser2employee_gid { get; set; }
        public string description { get; set; }
        public List<employelist> employelist { get; set; }
        public string raisequery_gid { get; set; }
        public string reply_query { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string samplereplytoquery_gid { get; set; }
        public string samplequery_status { get; set; }
        public string sampleimport_values { get; set; }
        public string taguser_flag { get; set; }
        public string tagged_employee { get; set; }
        public List<SampleDynamicdata> SampleDynamicdata { get; set; }
        Dictionary<string, string> myMap = new Dictionary<string, string>();
    }

    public class document_upload : result
    {
        public List<docupload_list> docupload_list { get; set; }
    }

    public class docupload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }

    public class DocUploadlog : result
    {
        public List<DocUploadlogdtl> DocUploadlogdtl { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }
    public class DocUploadlogdtl : result
    {
        public string raisequerydoc_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string sender_name { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string session_user { get; set; }
        public string document_attached { get; set; }
        public string lsflag { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }

    public class SampleDynamicdata
    {
        public string sampleimportdetail_gid { get; set; }
        public string samplename { get; set; }
        public string column_name { get; set; }
        public string column_value { get; set; }
    }


    public class sample_list
    {
        public string replyquery_flag { get; set; }
        public string sampleimport_gid { get; set; }
        public string sample_name { get; set; }
        public string samfin_code { get; set; }
        public string samagro_code { get; set; }
        public string codecreation_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string auditcreation_gid { get; set; }
        public string audit_uniqueno { get; set; }
        public string unique_id { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }
        public string field9 { get; set; }
        public string field10 { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string samplequery_status { get; set; }
        public string column_name { get; set; }
        public string column_value { get; set; }
    }

    public class employelist
    {
        public string taguser2employee_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string sample_name { get; set; }
    }


    public class sampleview_list
    {

        public string sampleimport_gid { get; set; }
        public string sample_name { get; set; }
        public string samfin_code { get; set; }
        public string samagro_code { get; set; }
        public string codecreation_date { get; set; }
        public string auditcreation_gid { get; set; }
        public string audit_uniqueno { get; set; }
        public string unique_id { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }
        public string field9 { get; set; }
        public string field10 { get; set; }
        public string raisequery_flag { get; set; }

    }
    public class SampleAssignedQueryList
    {
        public string sampleimport_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string description { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string reply_query { get; set; }
        public string raisequery_flag { get; set; }
        public string sample_name { get; set; }
        public string sampleraisequery_status { get; set; }
    }

    public class SampleReplyQueryList
    {
        public string sampleimport_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string samplereplytoquery_gid { get; set; }
        public string description { get; set; }
        public string assigned_by { get; set; }
        public string assigned_to { get; set; }
        public string assigned_date { get; set; }
        public string reply_query { get; set; }
        public string replied_by { get; set; }
        public string replied_date { get; set; }


    }

    public class sampledynamicdatadtl : result
    {
        public string JSONdata { get; set; } 
        public string auditobservation_name { get; set; }
    }

    public class samplequerydatalist : result
    {
        public List<samplequerydata> samplequerydata { get;set;}
        public string sampleraisequery_status { get; set; }
    } 

    public class samplequerydata : result
    {
        public string sampleimport_gid { get; set; }
        public string auditcreation_gid { get; set; }
        public string sample_name { get; set; }
        public string description { get; set; }
        public string query_title { get; set; }
        public string query_to { get; set; }
        public string query_toname { get; set; }
        public string sampleraisequery_gid { get; set; }
        public string raisequery_flag { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string sampleraisequery_status { get; set; }
        public string close_remarks { get; set; }
    }
}


