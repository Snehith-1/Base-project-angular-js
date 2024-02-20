using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// (It's used for Bureau API) Bureau API Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj and Gnanesh</remarks>
namespace ems.master.Models
{
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

    public class BureauLogResponse
    {
        public string bureau_score { get; set; }
        public string bureau_response { get; set; }
        public string document_content { get; set; }
        public string failure_reason { get; set; }
    }

    public class TransUnionConsumerHighRiskAlert
    {
        public string noofphones_rep3months { get; set; }
        public string noofaddresses_rep3months { get; set; }
        public string noofdistphones_rep3months { get; set; }
        public string noofdistddresses_rep3months { get; set; }
        public string noofdistids_rep3months { get; set; }
        public string noofdistpincodes_rep3months { get; set; }
        public string enqdifflenders_30days { get; set; }
        public string newloansopened_30days { get; set; }
        public string distunsecuredenq_3months { get; set; }
        public string ranksegment_hml { get; set; }
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