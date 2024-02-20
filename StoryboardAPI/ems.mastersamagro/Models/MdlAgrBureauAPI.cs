using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will provide values from UI and third party API to fetch highmark and transunion Records to our client's customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>

    public class MdlHighmarkResponse : result
    {
        public string bureau_score { get; set; }
        public string bureau_response { get; set; }
        public string html_content { get; set; }       
    }

    public class MdlTransUnionResponse : result
    {
        public string bureau_score { get; set; }
        public string bureau_response { get; set; }
        public string xml_content { get; set; }
    }

    public class MdlHighRiskAlertDetails : result
    {
        public string noofph_rep3mon { get; set; }
        public string noofad_rep3mon { get; set; }
        public string noofdistph_rep3mon { get; set; }
        public string noofdistad_rep3mon { get; set; }
        public string noofdistid_rep3mon { get; set; }
        public string noofdistpin_3mon { get; set; }
        public string enqdifflend_30days { get; set; }
        public string newloanopened_30days { get; set; }
        public string distunsecenq_3mon { get; set; }
        public string ranksegment_hml { get; set; }
    }

    public class BureauLogResponse
    {
        public string bureau_score { get; set; }
        public string bureau_response { get; set; }
        public string document_content { get; set; }
        public string failure_reason { get; set; }
    }

    public class HighmarkConsumer
    {
        public string name { get; set; }
        public string pan { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string statetemp { get; set; }
        public string state { get; set; }
        public string pin { get; set; }
        public string phone_number { get; set; }
    }

    public class HighmarkCommercial
    {
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string mobile_no { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string statetemp { get; set; }
        public string pin { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string state_code { get; set; }
        public string gender_code { get; set; }
    }

    public class TransUnionConsumer
    {
        public string name { get; set; }
        public string pan { get; set; }
        public string dob { get; set; }
        public string gender_name { get; set; }
        public string telephone_no { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string state_code { get; set; }
        public string gender_code { get; set; }
    }

    public class TransUnionCommercial
    {
        public string company_name { get; set; }
        public string companypan_no { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string state_code { get; set; }
    }
}