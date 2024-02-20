using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.idas.Models
{
    public class MdlIdasMstTemplate:result 
    {
        
            public string template_gid { get; set; }
            public string template_name { get; set; }
            public string template_content { get; set; }
        
    }

    public class MdlTemplateDtls : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string templatetype_gid { get; set; }
        public string templatetype_name { get; set; }
        public string template_content { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string template_path { get; set; }
        public List<templatetype_list> templatetype_list { get; set; }
        public List<template_list> template_list { get; set; }
    }

    public class templatetype_list
    {
        public string templatetype_gid { get; set; }
        public string templatetype_name { get; set; }
    }

    public class MdlTemplateDtlsList : result
    {
        public List<MdlTemplateDtls> TemplateDtlsList { get; set; }
    }
}