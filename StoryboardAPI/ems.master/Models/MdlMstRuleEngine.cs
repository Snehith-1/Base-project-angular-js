using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstRuleEngine : result
    {
        public List<rule_list> rule_list { get; set; }
        public List<ruletemplate_list> ruletemplate_list { get; set; }
        public List<templateforconfigure_list> templateforconfigure_list { get; set; }
        public templategidparametermap[] templategidparametermaparray { get; set; }
        public string[] ruletemplateparameter_gid { get; set; }
        public string[] rule_parameter { get; set; }
        public string template_code { get; set; }
        public string template_name { get; set; }
        public string application_no { get; set; }
        public List<Template_DropDown> Template_DropDown { get; set; }
        public List<Application_DropDown> Application_DropDown { get; set; }
        public List<postexecute_list> postexecute_list { get; set; }
        public string percentage { get; set; }
        public List<grouptitle_list> grouptitle_list { get; set; }
        public List<answertype_list> answertype_list { get; set; }


    }

    public class templategidparametermap
    {
        public string ruletemplateparameter_gid { get; set; }
        public string rule_parameter { get; set; }
    }

    public class addrule : result
    {
        public string ruleenginemaster_gid { get; set; }
        public string rule_id { get; set; }
        public string rule_title { get; set; } 
        public string rule_function { get; set; }
        public string rule_category { get; set; }
        public string rule_datatype { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }
        public string param_remarks { get; set; }


    }
    public class rule_list : result
    {
        public string ruleenginemaster_gid { get; set; }
        public string rule_id { get; set; }
        public string rule_title { get; set; }
        public string rule_function { get; set; }
        public string rule_datatype { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }
        public string param_remarks { get; set; }
        public string rule_category { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string api_code { get; set; }
    }

    public class addtemplate_assignrules : result
    {
       
        public string ruletemplateparameter_gid { get; set; }
        public string[] ruleenginemaster_gid { get; set; }
        public string rule_id { get; set; }
        public string rule_parameter { get; set; }
        public string rule_datatype { get; set; }
        public string ruletemplatemaster_gid { get; set; }
        public string template_code { get; set; }
        public string template_name { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }
        public string param_remarks { get; set; }

    }
    public class ruletemplate_list : result
    {

        public string ruletemplatemaster_gid { get; set; }
        public string template_code { get; set; }
        public string template_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string api_code { get; set; }
    }

    public class templateforconfigure_list : result
    {
       
        
        public string ruletemplateparameter_gid { get; set; }
        public string rule_parameter { get; set; }
        public string template_code { get; set; }
        public string template_name { get; set; }
        public string rule_id { get; set; }
        public string rule_title { get; set; }
        public string rule_function { get; set; }
        public string rule_datatype { get; set; }
        public string param_name { get; set; }
        public string param_value { get; set; }
        public string param_remarks { get; set; }

    }

    public class Template_DropDown : result
    {
        public string ruletemplatemaster_gid { get; set; }
        public string template_code { get; set; }
        public string template_name { get; set; }
    }

    public class Application_DropDown : result
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
       
    }

    public class postexecute_list : result
    {
       
        public string postruleenginerun_gid { get; set; }
        public string postruleenginerundetails_gid { get; set; }
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string template_code { get; set; }
        public string template_name { get; set; }
        public string rule_id { get; set; }
        public string rule_title { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string ruleengine_result { get; set; }
        public string ruleengineresult_details { get; set; }
        public string param_remarks { get; set; }

    }

    // Group Title Master --- STARTING
    //public class grouptitle_list
    //{
    //    public string grouptitle_gid { get;set;}
    //    public string grouptitle_name { get; set; }
    //    public string status { get; set; }
    //    public string lms_code { get; set; }
    //    public string bureau_code { get; set; }
    //    public string remarks { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //    public string updated_by { get; set; }
    //    public string updated_date { get; set; }

    //}

    //public class GroupTitle : result
    //{
    //    public string grouptitle_gid { get; set; }
    //    public string grouptitle_name { get; set; }
    //    public string Status { get; set; }
    //    public string lms_code { get; set; }
    //    public string bureau_code { get; set; }
    //    public string remarks { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //    public string updated_by { get; set; }
    //    public string updated_date { get; set; }

    //}

    //// Answer Type Master --- STARTING
    //public class answertype_list
    //{
    //    public string answertype_gid { get; set; }
    //    public string answertype_name { get; set; }
    //    public string status { get; set; }
    //    public string lms_code { get; set; }
    //    public string bureau_code { get; set; }
    //    public string remarks { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //    public string updated_by { get; set; }
    //    public string updated_date { get; set; }

    //}

    //public class AnswerType : result
    //{
    //    public string answertype_gid { get; set; }
    //    public string answertype_name { get; set; }
    //    public string Status { get; set; }
    //    public string lms_code { get; set; }
    //    public string bureau_code { get; set; }
    //    public string remarks { get; set; }
    //    public string created_by { get; set; }
    //    public string created_date { get; set; }
    //    public string updated_by { get; set; }
    //    public string updated_date { get; set; }

    //}

}


