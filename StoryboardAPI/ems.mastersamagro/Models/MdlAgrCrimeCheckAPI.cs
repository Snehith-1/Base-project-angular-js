using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will provide values from UI and third party API for verifying the crime track records to our client's customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>

    public class MdlIndividualCrimeRecordRequest 
    {
        public string individual_name { get; set; }
        public string father_name { get; set; }
        public string individual_dob { get; set; }
        public string individual_address { get; set; }
        public string search_mode { get; set; }
        public string search_in { get; set; }
        public string contact_gid { get; set; }
        public string application_gid { get; set;  }
}

    public class MdlIndividualCrimeRecordResponse : result
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        public bool requestStatus { get; set; }
        public string searchTerm { get; set; }
        public string searchType { get; set; }
        public string matchType { get; set; }

        public int totalResult { get; set; }
        public int totalHits { get; set; }
        public int page { get; set; }
        public int resultsPerPage { get; set; }

        public Details[] details { get; set; }

        public string overallRiskType { get; set; }
        public string overallRiskSummary { get; set; }

    }

    public class Details
    {
        public string cinNumber { get; set; }
        public string caseStatus { get; set; }
        public string caseType { get; set; }
        public string petitioner { get; set; }
        public string petitionerAddress { get; set; }
        public string respondent { get; set; }
        public string respondentAddress { get; set; }
        public string caseTypeName { get; set; }
        public string caseName { get; set; }
        public string courtType { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string caseRegDate { get; set; }
        public string year { get; set; }
        public string regNumber { get; set; }
        public string gfc_updated_at { get; set; }
        public string gfc_uniqueid { get; set; }
        public string caseDetailsLink { get; set; }
        public string riskProfile { get; set; }

    }

    public class MdlCompanyCrimeRecordRequest
    {
        public string company_name { get; set; }
        public string company_cin { get; set; }
        public string company_address { get; set; }       
        public string search_mode { get; set; }
        public string search_in { get; set; }
        public string application_gid { get; set; }
        public string institution_gid { get; set; }
    }

    public class MdlCompanyCrimeRecordResponse : result
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        public bool requestStatus { get; set; }

        public string searchTerm { get; set; }
        public string searchType { get; set; }
        public string matchType { get; set; }

        public int totalResult { get; set; }
        public int totalHits { get; set; }
        public int page { get; set; }
        public int resultsPerPage { get; set; }

        public Details[] details { get; set; }

        public string overallRiskType { get; set; }
        public string overallRiskSummary { get; set; }

    }

    public class MdlIndividualCrimeReportRequest
    {
        public string contact_gid { get; set; }
        public string application_gid { get; set; }
        public string individual_name { get; set; }
        public string individual_pan { get; set; }
        public string individual_address { get; set; }     
        public string report_mode { get; set; }
        public string callback_url { get; set; }
    }

    public class MdlIndividualCrimeReportRequestResponse : result
    {
        [JsonProperty("status")]
        public string Status { get; set; }      
        public string requestTime { get; set; }
        public string requestId { get; set; }
        [JsonProperty("message")]
        public string responseMessage { get; set; }
        public string requestStatusMessage { get; set; }
    }

    public class MdlCallbackReportDetailsIndividual
    {
        public string individual_name { get; set; }
        public string individual_pan { get; set; }
        public string individual_address { get; set; }
        public string report_mode { get; set; }
    }

    public class MdlCallbackResponse
    {
        public string data { get; set; }      
    }

    public class MdlCompanyCrimeReportRequest
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string company_type { get; set; }
        public string company_cin { get; set; }
        public string company_address { get; set; }
        public string report_mode { get; set; }
        public string callback_url { get; set; }
        public string application_gid { get; set; }
    }

    public class MdlCompanyCrimeReportRequestResponse : result
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        public string requestTime { get; set; }
        public string requestId { get; set; }
        [JsonProperty("message")]
        public string responseMessage { get; set; }
        public string requestStatusMessage { get; set; }
    }

    public class MdlIndividualCrimeReport : result
    {
        public string individual_name { get; set; }
        public string individual_dob { get; set; }
        public string individual_address { get; set; }
    }

    public class MdlIndividualCrimeReportList : result
    {
        public List<individualcrimereport_list> individualcrimereport_list { get; set; }
    }

    public class individualcrimereport_list
    {
        public string crimereportcontact_gid { get; set; }
        public string contact_gid { get; set; }
        public string request_id { get; set; }
        public string request_time { get; set; }
        public string report_mode { get; set; }
        public string request_status { get; set; }
        public string report_status { get; set; }
        public string report_link { get; set; }
    }

    public class MdlIndividualCrimeReportResponse : result
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        public string requestId { get; set; }
        public string requestTime { get; set; }
        public string responseTime { get; set; }
        public string riskType { get; set; }
        public string riskSummary { get; set; }
        public string downloadLink { get; set; }  
        public CaseDetails[] caseDetails { get; set; }
    }

    public class CaseDetails
    {
        public string slNo { get; set; }
        public string petitioner { get; set; }
        public string respondent { get; set; }
        public string caseTypeName { get; set; }
        public string caseStatus { get; set; }

        public string caseType { get; set; }
        public string caseDetailsLink { get; set; }

        public string courtName { get; set; }
        public string courtType { get; set; }
        public string district { get; set; }
        public string state { get; set; }
       
        public string riskType { get; set; }
        public string riskSummary { get; set; }

        public string judgementSummary { get; set; }
        
    }

    public class MdlCompanyCrimeReportResponse : result
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        public string requestId { get; set; }
        public string requestTime { get; set; }
        public string responseTime { get; set; }
        public string riskType { get; set; }
        public string riskSummary { get; set; }
        public string downloadLink { get; set; }
        public CaseDetails[] caseDetails { get; set; }
    }

    public class MdlCompanyCrimeReportList : result
    {
        public List<companycrimereport_list> companycrimereport_list { get; set; }
    }

    public class companycrimereport_list
    {
        public string crimereportinstitution_gid { get; set; }
        public string institution_gid { get; set; }
        public string request_id { get; set; }
        public string request_time { get; set; }
        public string report_mode { get; set; }
        public string request_status { get; set; }
        public string report_status { get; set; }
        public string report_link { get; set; }
    }

    public class MdlCompanyCrimeReport : result
    {
        public string company_name { get; set; }
        public string company_address { get; set; }
    }

    public class MdlIndividualCrimeRecord : result
    {
        public string individual_name { get; set; }
        public string individual_fathername { get; set; }
        public string individual_dob { get; set; }
        public List<individualaddress_list> individualaddress_list { get; set; }
    }

    public class individualaddress_list
    {
        public string address_gid { get; set; }
        public string address { get; set; }
    }

    public class MdlCompanyCrimeRecord : result
    {
        public string company_name { get; set; }
        public string company_cin { get; set; }
        public List<companyaddress_list> companyaddress_list { get; set; }
    }

    public class companyaddress_list
    {
        public string address_gid { get; set; }
        public string address { get; set; }
    }

    public class MdlTagCaseIndividual : result
    {
        public string contact_gid { get; set; }
        public string cin_number { get; set; }
        public string case_type { get; set; }
        public string case_status { get; set; }
        public string petitioner { get; set; }
        public string petitioner_address { get; set; }
        public string respondent { get; set; }
        public string respondent_address { get; set; }
        public string casetype_name { get; set; }
        public string case_name { get; set; }
        public string court_type { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string year { get; set; }
        public string gfc_updated_at { get; set; }
        public string gfc_uniqueid { get; set; }
        public string casedetails_link { get; set; }

        public List<tagcaseindividual_list> tagcaseindividual_list { get; set; }
    }

    public class tagcaseindividual_list
    {
        public string crimecasetaggedcontact_gid { get; set; }
        public string contact_gid { get; set; }
        public string cin_number { get; set; }
        public string case_type { get; set; }
        public string case_status { get; set; }
        public string petitioner { get; set; }
        public string petitioner_address { get; set; }
        public string respondent { get; set; }
        public string respondent_address { get; set; }
        public string casetype_name { get; set; }
        public string case_name { get; set; }
        public string court_type { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string year { get; set; }
        public string gfc_updated_at { get; set; }
        public string gfc_uniqueid { get; set; }
        public string casedetails_link { get; set; }

    }

    public class MdlTagCaseInstitution : result
    {
        public string institution_gid { get; set; }
        public string cin_number { get; set; }
        public string case_type { get; set; }
        public string case_status { get; set; }
        public string petitioner { get; set; }
        public string petitioner_address { get; set; }
        public string respondent { get; set; }
        public string respondent_address { get; set; }
        public string casetype_name { get; set; }
        public string case_name { get; set; }
        public string court_type { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string year { get; set; }
        public string gfc_updated_at { get; set; }
        public string gfc_uniqueid { get; set; }
        public string casedetails_link { get; set; }

        public List<tagcaseinstitution_list> tagcaseinstitution_list { get; set; }
    }

    public class tagcaseinstitution_list
    {
        public string crimecasetaggedinstitution_gid { get; set; }
        public string institution_gid { get; set; }
        public string cin_number { get; set; }
        public string case_type { get; set; }
        public string case_status { get; set; }
        public string petitioner { get; set; }
        public string petitioner_address { get; set; }
        public string respondent { get; set; }
        public string respondent_address { get; set; }
        public string casetype_name { get; set; }
        public string case_name { get; set; }
        public string court_type { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string year { get; set; }
        public string gfc_updated_at { get; set; }
        public string gfc_uniqueid { get; set; }
        public string casedetails_link { get; set; }

    }

    public class MdlCCCaseTaggedIndividual : result
    {
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string stakeholder_type { get; set; }
        public string tagged_by { get; set; }
        public string no_of_cases { get; set; }
        public List<cccasetaggedindividual_list> cccasetaggedindividual_list { get; set; }
    }

    public class cccasetaggedindividual_list
    {
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string stakeholder_type { get; set; }
        public string tagged_by { get; set; }
        public string no_of_cases { get; set; }
        public List<tagcaseindividual_list> tagcaseindividual_list { get; set; }
    }

    public class MdlCCCaseTaggedInstitution : result
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string stakeholder_type { get; set; }
        public string tagged_by { get; set; }
        public string no_of_cases { get; set; }
        public string reportlink_realtime { get; set; }
        public string reportlink_default { get; set; }
        public List<cccasetaggedinstitution_list> cccasetaggedinstitution_list { get; set; }
    }

    public class cccasetaggedinstitution_list
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string stakeholder_type { get; set; }
        public string tagged_by { get; set; }
        public string no_of_cases { get; set; }
        public List<tagcaseinstitution_list> tagcaseinstitution_list { get; set; }
    }

    public class MdlCCReportInstitution : result
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string stakeholder_type { get; set; }
        public string no_of_reports { get; set; }
        public List<ccreportinstitution_list> ccreportinstitution_list { get; set; }
    }

    public class ccreportinstitution_list
    {
        public string institution_gid { get; set; }
        public string company_name { get; set; }
        public string stakeholder_type { get; set; }
        public string no_of_reports { get; set; }
        public List<reportinstitution_list> reportinstitution_list { get; set; }
    }

    public class reportinstitution_list
    {
        public string crimereportinstitution_gid { get; set; }
        public string request_id { get; set; }
        public string report_mode { get; set; }
        public string report_link { get; set; }
    }

    //Individual

    public class MdlCCReportIndividual : result
    {
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string stakeholder_type { get; set; }
        public string no_of_reports { get; set; }
        public List<ccreportindividual_list> ccreportindividual_list { get; set; }
    }

    public class ccreportindividual_list
    {
        public string contact_gid { get; set; }
        public string individual_name { get; set; }
        public string stakeholder_type { get; set; }
        public string no_of_reports { get; set; }
        public List<reportindividual_list> reportindividual_list { get; set; }
    }

    public class reportindividual_list
    {
        public string crimereportcontact_gid { get; set; }
        public string request_id { get; set; }
        public string report_mode { get; set; }
        public string report_link { get; set; }
    }

    public static class ErrorResponseCrimeCheck
    {
        public const string
            errorResponse = "Error Response obtained - ";

    }


}