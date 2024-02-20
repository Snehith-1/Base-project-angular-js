using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.help.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }

    }

    public class querylist : result

    {
        public List<querysummary>querysummary { get; set; }
    }

    public class querysummary
    {
        public string moduleparent_gid { get; set; }
        public string query_code { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string raisequery_gid { get; set; }
        public string page_name { get; set; }
    }

    public class replyquerylist : result

    {
        public List <replyquerysummary> replyquerysummary { get; set; }
    }

    public class replyquerysummary
    {
        public string raisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string reply_avoid { get; set; }
        public string reply_cause { get; set; }
        public string reply_resolve { get; set; }
        public string page_name { get; set; }
    }

    public class FaqList : result
    {

        public List<faqsummary> faqsummary { get; set; }
    }

    public class mdlpagename : result
    {
        public List<pagename_list> pagename_list { get; set; }
    }
    public class pagename_list
    {
        public string page_gid { get; set; }
        public string page_name { get; set; }
    }
    public class faqsummary
    {
        public string faqdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_ques { get; set; }
        public string document_ans { get; set; }
        public string page_name { get; set; }
        public string module_name { get; set; }
    }

    public class logo : result
    {
        public string company_code { get; set; }
        public string company_uilogo_path { get; set; }
    }

    public class modulename : result
    {
        public string module_name { get; set; }
        public string module_gid { get; set; }
    }

    public class moduleList : result
    {
        public List<modulesummary> modulesummary { get; set; }
    }

    public class modulesummary
    {
        public string module_name { get; set; }
        public string module_gid { get; set; }
    }

    public class UsefulResourceList : result
    {
        public List<usefulresourcesummary> usefulresourcesummary { get; set; }
    }
    public class usefulresourcesummary
    {
        public string usefulresourcesdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
        public string page_name { get; set; }
        public string module_name { get; set; }
    }

    public class HowToUseList : result
    {
        public List<howtousesummary> howtousesummary { get; set; }
    }
    public class howtousesummary
    {
        public string howtousedocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_title { get; set; }
        public string page_name { get; set; }
        public string module_name { get; set; }
    }

    public class usedocument : result
    {
        public string usefulresourcesdocument_gid { get; set; }
        public string document_path { get; set; }
    }
    public class uploaddocument : result
    {
        public List<upload_list> upload_list { get; set; }
    }
    public class upload_list
    {

        public string document_name { get; set; }
        public string document_type { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
    }

    public class mdlquery : result
    {
        public string page_gid { get; set; }
        public string txt_querytitle { get; set; }
        public string txtquerydescription { get; set; }
        public string moduleparent_name { get; set; }
    }

    public class attachphotolist : result
    {
        public List<attachphoto> attachphoto { get; set; }
    }

    public class attachphoto
    {
        public string id { get; set; }
        public string file_name { get; set; }
        public string path { get; set; }
    }
}