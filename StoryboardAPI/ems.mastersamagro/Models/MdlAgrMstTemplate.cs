using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will provide access to template and editor doc for contract flow in samagro
    /// </summary>
    /// <remarks>Written by Venkatesh </remarks>

    public static class getTemplateClass1
    {
        public const string
             CAMGeneration = "CAM",
             Contract = "Contract";
    }

    public class MdlMstTemplate1 : result
    {

        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }

    }


    public class MdlTemplateDtls : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
        public string program_name { get; set; }
        public string program_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string template_path { get; set; }
        public string template_type { get; set; }
        public List<templateinputtype_list> templateinputtype_list { get; set; }
        public List<template_programlist> template_programlist { get; set; }
    }

    public class template_programlist
    {
        public string program_gid { get; set; }
        public string program_name { get; set; }
    }

    public class templateinputtype_list
    {
        public string templateinputdtl_gid { get; set; }
        public string template_gid { get; set; }
        public string input_fieldid { get; set; }
        public string input_fieldname { get; set; }
        public string inputgroup_gid { get; set; }
        public string inputgroup_name { get; set; }
        public string input_type { get; set; }
        public string input_placeholder { get; set; }
        public string inputmax_length { get; set; }
        public string input_mandatory { get; set; }
        public string input_previewtext { get; set; }
        public string vertical_gid { get; set; }
        public string program_gid { get; set; }
        public string DBfield_type { get; set; }
        public string fieldmapping_gid { get; set; }
        public List<inputlistarray> inputlistarray { get; set; }
    }

    public class inputlistarray
    {
        public string templateinputdtl_gid { get; set; }
        public string preview_text { get; set; }
        public string Value { get; set; }
    }

    public class templatetype_list
    {
        public string templatetype_gid { get; set; }
        public string templatetype_name { get; set; }
    }

    public class MdlTemplateDtlsList : result
    {
        public string vertical_name { get; set; }
        public List<MdlTemplateDtls> TemplateDtlsList { get; set; }
    }

    public class MdlInputTypeList : result
    {
        public string template_gid { get; set; }
        public string templatetype_gid { get; set; }
        public string defaulttemplate_content { get; set; }
        public string template_content { get; set; }
        public List<MdlInputDtls> MdlInputDtls { get; set; }
    }

    public class MdlInputDtls
    {
        public string templateinputdtl_gid { get; set; }
        public string inputgroup_name { get; set; }
        public string inputgroup_gid { get; set; }
        public string input_type { get; set; }
        public string input_placeholder { get; set; }

        public string inputmax_length { get; set; }
        public string input_mandatory { get; set; }
        public string input_fieldid { get; set; }
        public string input_fieldname { get; set; }
        public string input_givendata { get; set; }

        public string input_givenvalue { get; set; }
        public string input_previewtext { get; set; }
        public string input_fieldvalue { get; set; }

        public bool DBInput { get; set; }
        public List<MdlInputlistDtls> MdlInputlistDtls { get; set; }
        public List<inputlistarray> inputlistarray { get; set; }
    }

    public class MdlInputlistDtls
    {
        public string trntemplateinputlistdtl_gid { get; set; }
        public string templateinputdtl_gid { get; set; }
        public string preview_text { get; set; }
        public string Value { get; set; }
    }

    public class MdlTemplatelist1 : result
    {
        public string template_content { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string mstcontent_flag { get; set; }
        public string defaulttemplate_content { get; set; }
        public string template_name { get; set; }
        public string template_gid { get; set; }

        public List<MdlTemplatedtl1> MdlTemplatedtl1 { get; set; }

    }

    public class MdlTemplatedtl1
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }
    }

    public class MdlTemplateGeneratedtl : result
    {
        public string template_gid { get; set; }
        public string lstemplatefrom { get; set; }
        public string templatetype_gid { get; set; }
    }

    // Field Mapping 

    public class MdlFieldMappinglist : result
    {
        public List<MdlFieldMapping> MdlFieldMapping { get; set; }
    }

    public class MdlFieldMapping
    {
        public string fieldmapping_gid { get; set; }
        public string fieldmapping_code { get; set; }
        public string fieldmapping_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlFieldMappingdtl : result
    {
        public string fieldmapping_gid { get; set; }
        public string fieldmapping_code { get; set; }
        public string fieldmapping_name { get; set; }
        public string input_type { get; set; }
        public string stage_fieldid { get; set; }
        public string stage_fieldname { get; set; }
        public string stage_fieldvalue { get; set; }
        public string menu_fieldname { get; set; }
        public string menu_fieldvalue { get; set; }
        public string menu_tablevalue { get; set; }
        public string fieldname { get; set; }
        public string field_value { get; set; }
        public string field_tablevalue { get; set; }
        public string field_columnvalue { get; set; }
        public string subfield_flag { get; set; }
        public string subfield_fieldname { get; set; }
        public string subfield_fieldvalue { get; set; }
        public string subfield_tablevalue { get; set; }
        public string subfield_columnvalue { get; set; }
        public string subfield_joincolumn { get; set; }
        public string subfield_joincolumn1 { get; set; }
        public string subfield_joincolumn2 { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }


    public class MdlFieldMappingDropdownlist : result
    {
        public List<MdlFieldMappingDropdown> MdlFieldMappingDropdown { get; set; }
    }

    public class MdlFieldMappingDropdown
    {
        public string fieldmapping_gid { get; set; }
        public string fieldmapping_code { get; set; }
        public string fieldmapping_name { get; set; }
        public string subfield_flag { get; set; }
        public string stage_fieldname { get; set; }
        public string menu_fieldname { get; set; }
        public string fieldname { get; set; }
        public string subfield_fieldname { get; set; }
    }

    public class MdlDBInputList : result
    {
        public List<MdlDBInputDtls> MdlDBInputDtls { get; set; }
    }
    public class MdlDBInputDtls
    {
        public string templateinputdtl_gid { get; set; }
        public string input_type { get; set; }
        public string input_fieldid { get; set; }
        public string input_fieldvalue { get; set; }
        public string field_value { get; set; }
        public string menu_tablevalue { get; set; }
        public string input_fieldname { get; set; }
        public string input_placeholder { get; set; }
        public string field_tablevalue { get; set; }
        public string field_columnvalue { get; set; }
        public string subfield_flag { get; set; }
        public string subfield_fieldvalue { get; set; }
        public string subfield_tablevalue { get; set; }
        public string subfield_columnvalue { get; set; }
        public string subfield_joincolumn { get; set; }
        public string subfield_joincolumn1 { get; set; }
        public string subfield_joincolumn2 { get; set; }
        public string input_givendata { get; set; }
        public List<MdlInputGivenList> MdlInputGivenList { get; set; }
    }
    public class MdlInputGivenList
    {
        public string input_fieldvalue { get; set; }
    }
}