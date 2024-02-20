using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.businessteam.Models
{
    //public class result
    //{
    //    public bool status { get; set; }
    //    public string message { get; set; }
    //}
    public class MdlMarketingCallMobileNo : result
    {
        public string marketingcall2mobileno_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_status { get; set; }
        public string sms_to { get; set; }
        public List<MarketingCallmobileno_list> MarketingCallmobileno_list { get; set; }
    }

    public class MarketingCallmobileno_list
    {
        public string marketingcall2mobileno_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string mobile_no { get; set; }
        public string primary_status { get; set; }
        public string whatsapp_status { get; set; }
        public string sms_to { get; set; }
    }

    public class MdlMarketingCallEmail : result
    {
        public string marketingcall2email_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }     
        public List<MarketingCallemail_list> MarketingCallemail_list { get; set; }
    }
    public class MdlMarketingCallLeadstatus : result
    {
        public string marketingcall2leadstatus_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string lead_type { get; set; }
        public string closure_status { get; set; }
        public string ticket_refid { get; set; }
        public string loanproduct_name { get; set; }
        public string loanproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loan_amount { get; set; }
               
        public List<MarketingCallLeadstatus_list> MarketingCallLeadstatus_list { get; set; }
    }

    public class MarketingCallemail_list
    {
        public string marketingcall2email_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string email_address { get; set; }
        public string primary_status { get; set; }
    }

    public class MarketingCallLeadstatus_list
    {
        public string marketingcall2leadstatus_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string lead_type { get; set; }
        public string closure_status { get; set; }
        public string loanproduct_name { get; set; }
        public string loanproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loan_amount { get; set; }
        public string ticket_refid { get; set; }

    }
    public class MdlMarketingCallFollowUp : result
    {
        public string marketingcall2followup_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string followup_date { get; set; }
        public string followup_time { get; set; }
        public string followup_remarks { get; set; }
        public string followup_status { get; set; }
        public DateTime Tfollowup_time { get; set; }
        public List<MarketingCallfollowup_list> MarketingCallfollowup_list { get; set; }

    }

    public class MarketingCallfollowup_list
    {
        public string marketingcall2followup_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string followup_date { get; set; }
        public string followup_time { get; set; }
        public string followup_status { get; set; }
        public string followup_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class marketingcallextendfollowup_list
    {
        public string marketingcall2followup_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string extendfollowup_date { get; set; }
        public string extendfollowup_time { get; set; }
        public string extendfollowup_remarks { get; set; }
        public string extendfollowup_by { get; set; }

    }

    public class MdlMarketingCallAddress : result
    {
        public string marketingcall2address_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }        
        public string primary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_gid { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }     
        public List<MarketingCalladdress_list> MarketingCalladdress_list { get; set; }      
    }
    public class MarketingCalladdress_list
    {
        public string marketingcall2address_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string addresstype_gid { get; set; }
        public string addresstype_name { get; set; }
        public string primary_status { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string landmark { get; set; }
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_gid { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class MdlMarketingCall : result
    {
        public string business_name { get; set; }

        public string marketingcall_gid { get; set; }
        public string ticket_refid { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }
        public string saloanproduct_gid { get; set; }
        public string rejected_remarks { get; set; }
        public string saloanproduct_name { get; set; }
        public string saloansubproduct_gid { get; set; }
        public string saloansubproduct_name { get; set; }
        public string ssloanproduct_gid { get; set; }
        public string ssloanproduct_name { get; set; }
        public string ssloansubproduct_gid { get; set; }
        public string ssloansubproduct_name { get; set; }
        public string loan_amount { get; set; }
        public string marketingsourceofcontact_gid { get; set; }
        public string marketingsourceofcontact_name { get; set; }
        public string marketingcallreceivednumber_gid { get; set; }
        public string marketingcallreceivednumber_name { get; set; }
        public string leadrequesttype_gid { get; set; }
        public string leadrequesttype_name { get; set; }
        public string customer_type { get; set; }    
        public string callreceived_date { get; set; }
        public string caller_name { get; set; }
        public string internalreference_gid { get; set; }
        
        public string internalreference_name { get; set; }
        public string callerassociate_company { get; set; }
        public string office_landlineno { get; set; }
        public string function_remarks { get; set; }
        public string tat_hours { get; set; }
        public string marketingcalltype_gid { get; set; }
        public string marketingcalltype_name { get; set; }
        public string marketingfunction_gid { get; set; }
        public string marketingfunction_name { get; set; }
        public string taguser { get; set; }
        public string closed { get; set; }
        public string requirement { get; set; }
        public string enquiry_description { get; set; }
        public string callclosure_status { get; set; }
        public string assignemployee_gid { get; set; }
        public string assignemployee_name { get; set; }
        public string assign_by { get; set; }
        public string assign_date { get; set; }
        public string transfer_by { get; set; }
        public string transfer_date { get; set; }
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string tagemployee_gid { get; set; }
        public string tagemployee_name { get; set; }
        public string assignclosure_remarks { get; set; }
        public string marketingcall_status { get; set; }
        public string reject_remarks { get; set; }
        public string completed_remarks { get; set;}
        public string closed_remarks { get; set; }
        public List<MarketingCall_list> MarketingCall_list { get; set; }
        public List<tagemployee_list> tagemployee_list { get; set; }
        public List<emp_list> emp_list { get; set; }
        public List<MarketingCalltransfer_list> MarketingCalltransfer_list { get; set; }
        public List<inboundentity_list> inboundentity_list { get; set; }
        public string followup_date { get; set; }
        public string followup_time { get; set; }
        public string followup_remarks { get; set; }      
        public string closure_status { get; set; }
        public List<upload_list> upload_list { get; set; }
        public List<MarketingCallType_list> MarketingCallType_list { get; set; }
        public List<MarketingSourceofContact_list> MarketingSourceofContact_list { get; set; }
        public List<MarketingTelecallingFunction_list> MarketingTelecallingFunction_list { get; set; }
        public List<MarketingCallReceivedNumber_list> MarketingCallReceivedNumber_list { get; set; }
        public List<leadrequest_list> leadrequest_list { get; set; }
        public List<samapplication_list> samapplication_list { get; set; }
        public string employee_gid { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string leadrequire_gid { get; set; }
        public string leadrequire_name { get; set; }
        public string milletrequire_gid { get; set; }
        public string milletrequire_name { get; set; }
        public string company_name { get; set; }
        public string industry_name { get; set; }
        public string message_name { get; set; }
        public string your_name { get; set; }
        public string origination { get; set; }
        public string created_date { get; set; }
        public List<document_list> document_list { get; set; }//milletdocument_list
        public List<document_list> milletdocument_list { get; set; }
        public List<enquirydocument_list> enquirydocument_list { get; set; }

        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string enquiryrequire_name { get; set; }
        public string enquiryrequire_gid { get; set; }
        public string startuprequire_name { get; set; }

        public string startuprequire_gid { get; set; }

    }
    public class milletdocument_list
    {
        public string document_name { get; set; }
        public string document_gid { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }
    public class document_list
    {
        public string document_name { get; set; }
        public string document_gid { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }
    public class enquirydocument_list
    {
        public string document_name { get; set; }
        public string document_gid { get; set; }
        public string document_path { get; set; }
        public string[] filename { get; set; }
        public string filepath { get; set; }
    }
    public class MarketingCallType_list
    {
        public string marketingcalltype_name { get; set; }
        public string marketingcalltype_gid { get; set; }
    }
    public class leadrequest_list
    {
        public string leadrequesttype_name { get; set; }
        public string leadrequesttype_gid { get; set; }
    }
    public class samapplication_list
    {
        public string loanproduct_gid { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
        public string loansubproduct_name { get; set; }
    }
    public class MarketingSourceofContact_list
    {
        public string marketingsourceofcontact_name { get; set; }
        public string marketingsourceofcontact_gid { get; set; }
    }
    public class MarketingTelecallingFunction_list
    {
        public string marketingtelecallingfunction_name { get; set; }
        public string marketingtelecallingfunction_gid { get; set; }
    }
    public class MarketingCallReceivedNumber_list
    {
        public string marketingcallreceivednumber_name { get; set; }
        public string marketingcallreceivednumber_gid { get; set; }
    }

    public class upload_list
    {
        public string tmp_documentGid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class MdlAttachmentList
    {
        public string mailattachment_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_extension { get; set; }
        public string composemailattachment_gid { get; set; }
    }
    public class tagemployee_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class emp_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class inboundentity_list
    {
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
    }


    public class MarketingCall_list
    {
        public string marketingcall_gid { get; set; }
        public string ticket_refid { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string sourceofcontact_gid { get; set; }
        public string sourceofcontact_name { get; set; }
        public string callreceivednumber_gid { get; set; }
        public string callreceivednumber_name { get; set; }
        public string customer_type { get; set; }
        public string callreceived_date { get; set; }
        public string caller_name { get; set; }
        public string internalreference_gid { get; set; }
        public string internalreference_name { get; set; }
        public string callerassociate_company { get; set; }
        public string office_landlineno { get; set; }
        public string requirement { get; set; }
        public string enquiry_description { get; set; }
        public string callclosure_status { get; set; }
        public string origination { get; set; }        
        public string assignemployee_gid { get; set; }
        public string assignemployee_name { get; set; }
        public string tagemployee_gid { get; set; }
        public string tagemployee_name { get; set; }
        public string assignclosure_remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string assign_by { get; set; }
        public string assign_date { get; set; }
        public string transfer_by { get; set; }
        public string tagged_by { get; set; }
        public string tagged_date { get; set; }
        public string transfer_date { get; set; }
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string followup_date { get; set; }
        public string followup_by { get; set; }
        public string closed_date { get; set; }
        public string closed_by { get; set; }
        public string rejected_by { get; set; }
        public string rejected_date { get; set; }
        public string closed_remarks { get; set; }
        public string leadrequesttype_name { get; set; }
    }

    public class MdlMarketingCallView : result
    {

        public string marketingcall_gid { get; set; }
        public string ticket_refid { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string sourceofcontact_gid { get; set; }
        public string sourceofcontact_name { get; set; }
        public string callreceivednumber_gid { get; set; }
        public string callreceivednumber_name { get; set; }
        public string customer_type { get; set; }
        public string closed { get; set; }
        public string callreceived_date { get; set; }
        public string caller_name { get; set; }
        public string internalreference_gid { get; set; }
        public string internalreference_name { get; set; }
        public string callerassociate_company { get; set; }
        public string office_landlineno { get; set; }
        public string calltype_gid { get; set; }
        public string calltype_name { get; set; }
        public string function_gid { get; set; }
        public string function_name { get; set; }
        public string requirement { get; set; }
        public string industry_name { get; set; }

        public string enquiry_description { get; set; }
        public string callclosure_status { get; set; }
        public string assignemployee_gid { get; set; }
        public string assignemployee_name { get; set; }
        public string tagemployee_gid { get; set; }
        public string tagemployee_name { get; set; }
        public string assignclosure_remarks { get; set; }
        public string marketingcall_status { get; set; }
        public string followup_time { get; set; }
        public string primary_mobileno { get; set; }
        public string assigningclosure_remarks { get; set; }
        public string tat_hours { get; set; }
        public string leadrequesttype_name { get; set; }
        public string baselocation_name { get; set; }
        public string function_remarks { get; set; }
        public string milletrequire_name { get; set; }
        public string leadrequire_name { get; set; }
        public string enquiryrequire_name { get; set; }
        public string startuprequire_name { get; set; }
        public string business_name { get; set; }
        public List<MarketingCallmobileno_list> MarketingCallmobileno_list { get; set; }
        public string primary_email { get; set; }
        public List<MarketingCallemail_list> MarketingCallemail_list { get; set; }

        public List<MarketingCalladdress_list> MarketingCalladdress_list { get; set; }

        public List<MarketingCallfollowup_list> MarketingCallfollowup_list { get; set; }
        public List<marketingcallextendfollowup_list> marketingcallextendfollowup_list { get; set; }
        public List<MarketingCallstatus_list> MarketingCallstatus_list { get; set; }
        public List<MarketingCalltaggedmember_list> MarketingCalltaggedmember_list { get; set; }
        public List<MarketingCalltransfer_list> MarketingCalltransfer_list { get; set; }
        public string assign_date { get; set; }
        public string completed_by { get; set; }
        public string closed_by { get; set; }
        public string completed_remarks { get; set; }
        public string closed_remarks { get; set; }
        public string completed_date { get; set; }
        public string closed_date { get; set; }
        public string followup_remarks { get; set; }
        public string acknowledge_date { get; set; }
        public string followup_date { get; set; }
        public string followup_by { get; set; }
        public string rejected_remarks { get; set; }
        public string rejected_by { get; set; }
        public string acknowledge_by { get; set; }
        public string rejected_date { get; set; }
        public string extendfollowup_date { get; set; }
        public string extendfollowup_time { get; set; }
        public string extendfollowup_remarks { get; set; }
        public string overall_detail { get; set; }
        public string loanproduct_name { get; set; }
        public string loansubproduct_name { get; set; }
        public string loan_amount { get; set; }
        public string origination { get; set; }
        
    }

    public class MarketingCalltaggedmember_list
    {
        public string taggedmember_name { get; set; }
        public string tagged_by { get; set; }
        public string tagged_date { get; set; }        
    }
    public class MarketingCallstatus_list
    {
        public string status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string remarks { get; set; }
        public string overall_detail { get; set; }
    }

    public class MdlMarketingCallTransfer : result
    {
        public string marketingcalltransferlog_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string ticket_refid { get; set; }
        public string transferfrom_gid { get; set; }
        public string transferfrom_name { get; set; }
        public string transferto_gid { get; set; }
        public string transferto_name { get; set; }
        public string transfer_remarks { get; set; }
        public string transfer_by { get; set; }
        public string transfer_date { get; set; }       
    }

    public class MarketingCalltransfer_list : result
    {
        public string marketingcalltransferlog_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string ticket_refid { get; set; }
        public string transferfrom_gid { get; set; }
        public string transferfrom_name { get; set; }
        public string transferto_gid { get; set; }
        public string transferto_name { get; set; }
        public string transfer_remarks { get; set; }
        public string transfer_by { get; set; }
        public string transfer_date { get; set; }
    }

    public class MarketingCallCount
    {
        public string unassignedcall_count { get; set; }
        public string  assignedcall_count { get; set; }        
        public string completedcall_count { get; set; }
        public string closedcall_count { get; set; }
        public string followupcall_count { get; set; }
        public string transfercall_count { get; set; }
        public string inprogresscall_count { get; set; }
        public string taggedcall_count { get; set; }
        public string rejectedcall_count { get; set; }
    }

    public class MdlMarketingCallcompleteView
    {
        public string completed_by { get; set; }
        public string completed_date { get; set; }
        public string completed_remarks { get; set; }
    }
    public class callproofuploaddocument : result
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string institution2form60documentupload_gid { get; set; }
        public string institution2documentupload_gid { get; set; }
        public string institution_gid { get; set; }
        public List<callproofupload_list> callproofupload_list { get; set; }
    }
    public class callproofupload_list
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string MarketingCallproofdocupload_gid { get; set; }
        public string marketingcall_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string document_title { get; set; }
        public string MarketingCallrecordingocupload_gid { get; set; }
    }

    public class MarketingReport : result
    {
        public List<MarketingReportList> MarketingReportList { get; set; }
        public string lspath { get; set; }
        public string lsname { get; set; }
        public string lscloudpath { get; set; }

    }
    public class MarketingReportList
    {
        public string entity_name { get; set; }
        public string ticket_refid { get; set; }
        public string caller_name { get; set; }
        public string customer_type { get; set; }
        public string marketingcall_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string origination { get; set; }        
        public string marketingcall_gid { get; set; }
        public string leadrequesttype_name { get; set; }

    }

}