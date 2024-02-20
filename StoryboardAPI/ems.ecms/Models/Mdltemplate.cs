using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// template Controller Class containing API methods for accessing the  Model class Mdltemplate
    ///  Teplate - Get template from DB, Get content from DB
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class Mdltemplate:result
    {
        public List<template_list> template_list { get; set; }
    }
    public class template_list:result 
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set;}
    }
}