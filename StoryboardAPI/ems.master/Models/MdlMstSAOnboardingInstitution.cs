using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.master.Models
{
    public class MdlMstSAOnboardingInstitution
    {
    }
    public class MdlContactMobileNoSAInstituion : result
    {
        public string sainstitution2mobileno_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string samobile_no { get; set; }
        public string saprimary_status { get; set; }
        public string sawhatsapp_no { get; set; }
        public List<SacontactInstimobileno_list> SacontactInstimobileno_list { get; set; }
        public string opscontact2mobileno_gid { get; set; }
        public string opscontact_gid { get; set; }
        public string statusupdated_by { get; set; }
    }

    public class SacontactInstimobileno_list
    {
        public string sainstitution2mobileno_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string samobile_no { get; set; }
        public string saprimary_status { get; set; }
        public string sawhatsapp_no { get; set; }
        public string opscontact2mobileno_gid { get; set; }
    }
    public class MdlsaOnboardInstiEmailAddress : result
    {
        public string sainstitution2email_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string saemail_address { get; set; }
        public string saprimary_status { get; set; }
        public string samail_type { get; set; }
        public List<saOnboardInstiemailaddress_list> saOnboardInstiemailaddress_list { get; set; }
    }

    public class saOnboardInstiemailaddress_list
    {
        public string sainstitution2email_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string saemail_address { get; set; }
        public string saprimary_status { get; set; }
        public string samail_type { get; set; }
    }
    public class MdlSaOnboardInstiAddress : result
    {
        public string sainstitution2address_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string saaddresstype_gid { get; set; }
        public string saaddresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string saprimary_status { get; set; }
        public string salandmark { get; set; }
        public string sapostal_code { get; set; }
        public string sacity { get; set; }
        public string sataluka { get; set; }
        public string sadistrict { get; set; }
        // public string state_gid { get; set; }
        public string sastate { get; set; }
        public string sacountry { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public List<saOnboardInstiaddress_list> saOnboardInstiaddress_list { get; set; }
    }

    public class saOnboardInstiaddress_list
    {
        public string sainstitution2address_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string saaddresstype_gid { get; set; }
        public string saaddresstype_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string saprimary_status { get; set; }
        public string salandmark { get; set; }
        public string sapostal_code { get; set; }
        public string sacity { get; set; }
        public string sataluka { get; set; }
        public string sadistrict { get; set; }
        // public string state_gid { get; set; }
        public string sastate { get; set; }
        public string sacountry { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    public class MdlSAOnboardInstiGST : result
    {
        public string sainstitution2gst_gid { get; set; }
        public string institution_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gststate_name { get; set; }
        public string gst_no { get; set; }
        public string gstregister_status { get; set; }
        public string gst_registered { get; set; }
        //public GSTDetails[] GSTArray { get; set; }
        public InstitutionGSTDetail[] GSTArray { get; set; }

        public List<gst_Onboard_list> gst_Onboard_list { get; set; }
    }
    public class gst_Onboard_list
    {
        public string sainstitution2gst_gid { get; set; }
        public string institution_gid { get; set; }
        public string gststate_name { get; set; }
        public string gstregister_status { get; set; }
        public string gst_no { get; set; }
    }
    public class InstitutionGSTDetail
    {
        public string authStatus { get; set; }
        public string applicationStatus { get; set; }
        public string emailId { get; set; }
        public string gstinId { get; set; }
        public string gstinRefId { get; set; }
        public string mobNum { get; set; }
        public string pan { get; set; }
        public string regType { get; set; }
        public string registrationName { get; set; }
        public string tinNumber { get; set; }
    }
    public class MdlMstSAOnboardInstiIndividual : result
    {
        public string sainst_individual_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_pannumber { get; set; }
        public string sa_aadharnumber { get; set; }
        public string designation_type { get; set; }
        public List<onboard_IndividualInsti_list> onboard_IndividualInsti_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }

    }
    public class onboard_IndividualInsti_list
    {
        public string sainst_individual_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_pannumber { get; set; }
        public string sa_aadharnumber { get; set; }
        public string designation_type { get; set; }
    }
    public class MdlsaOnboardIstitutionProspects : result
    {
        public string saprospects_institution_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string salead_name { get; set; }
        public string sasector_industry { get; set; }
        public List<saOnboardInstiProspects_list> saOnboardInstiProspects_list { get; set; }
    }

    public class saOnboardInstiProspects_list
    {
        public string saprospects_institution_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string salead_name { get; set; }
        public string sasector_industry { get; set; }
    }
    public class MdlsaOnboardInstitutionDocument : result
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string sainstidocument_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sadocument_name { get; set; }
        public string sadocument_id { get; set; }
        public string safile_path { get; set; }
        public List<saOnboardInstiDocument_list> saOnboardInstiDocument_list { get; set; }
    }

    public class saOnboardInstiDocument_list
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string sainstidocument_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sadocument_name { get; set; }
        public string sadocument_id { get; set; }
        public string safile_path { get; set; }
        public string document_title { get; set; }

    }
    public class MdlMstSAOnboardInstitution : result
    {
        public string reportingmanager_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string satype_name { get; set; }
        public string saentitytype_name { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string sa_associatename { get; set; }
        public string sa_contactfirstname { get; set; }
        public string sa_contactmiddlename { get; set; }
        public string sa_contactlastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string samobile_no { get; set; }
        public string sa_dateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string state { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string approvalstatus { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }

        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string ratingas_date { get; set; }
        public string ratingas_datecredit { get; set; }

        public string rdbgstregister_status { get; set; }

        public string lsname { get; set; }
        public string lspath { get; set; }
        public string lscloudpath { get; set; }

        public List<onboard_Institution_list> onboard_Institution_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }
        public int Years;
        public int Months;
        public int Days;
    }
    public class onboard_Institution_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string sa_associatename { get; set; }
        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string sa_dateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }

    }

    public class Institutionedit : result
    {
        public string reportingmanager_gid { get; set; }
        public string recordsource { get; set; }
        public string tagging_flag { get; set; }
        public string closeflag { get; set; }
        public string saveasdraftadd_flag { get; set; }
        public string raisequery_flag { get; set; }
        public string institutionsaveasdraft_flag { get; set; }
        public string interviewevalution { get; set; }
        public string applicationform { get; set; }
        public string yearsitreturns { get; set; }
        public string rejected_date { get; set; }
        public string bankstatement { get; set; }
        public string rejected_by { get; set; }
        public string kycdocuments { get; set; }
        public string rejected_remarks { get; set; }
        public string bureau_check { get; set; }
        public string crime_check { get; set; }
        public string remarks { get; set; }
        public string training_status { get; set; }
        public string update_flag { get; set; }
        public string prospect { get; set; }
        public string vettingstatus { get; set; }
        public string scannedcopyreception { get; set; }
        public string addressproof { get; set; }
        public string photographs { get; set; }
        public string cancelledcheckleaf { get; set; }
        public string houseofficeverification { get; set; }
        public string agreementexecutiondate { get; set; }
        public string agreementexpirydate { get; set; }
        public string agreementstatus { get; set; }
        public string agroagreementexecutiondate { get; set; }
        public string agroagreementexpirydate { get; set; }
        public string agroagreementstatus { get; set; }
        public string bookletnumber { get; set; }
        public string verificationremarks { get; set; }
        public string approvalinitated_flag { get; set; }
        public string approval_flag { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string satype_name { get; set; }
        public string saentitytype_name { get; set; }
        public string sa_associatename { get; set; }
        public string sa_autogeneratedid { get; set; }
        public string sa_contactfirstname { get; set; }
        public string sa_contactmiddlename { get; set; }
        public string sa_contactlastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string sa_dateofincorporation { get; set; }
        public DateTime sadateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public DateTime sacompanystdate { get; set; }
        public string editsa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string editsa_startdate { get; set; }
        public string editsa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string designation_gid { get; set; }
        public string designation_type { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string origination { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string micr { get; set; }
        public string branch_address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        //public List<Institution_Edit_list> Institution_BussVer_list { get; set; }
        public List<postalcodedetails_list> postalcodedetails_list { get; set; }
        public string content { get; set; }
        public string sa_updated_by { get; set; }
        public string sa_updated_date { get; set; }
        public string approved_date { get; set; }
        public string approved_by { get; set; }
        public string approval_remarks { get; set; }
        public string approvalstatus { get; set; }
        public string sa_appcrediteddate { get; set; }
        public DateTime saappcrediteddate { get; set; }

        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }
        public string samfin_code { get; set; }
        public string samagro_code { get; set; }
        public string rm_tagging_id { get; set; }
        public string renewal_flag { get; set; }

        public string rm_tagging_view { get; set; }
        public int Years;
        public int Months;
        public int Days;


        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string ratingas_date { get; set; }
      public string referred_by { get; set; }
        public string ratingas_datecredit { get; set; }
        public string codecreation_date { get; set; }
        public string utr_no { get; set; }

    }

    public class Institution_BussVer_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string sa_associatename { get; set; }
        public string sa_firstname { get; set; }
        public string sa_middlename { get; set; }
        public string sa_lastname { get; set; }
        public string sa_designation { get; set; }
        public string sa_companypan { get; set; }
        public string sa_dateofincorporation { get; set; }
        public string editsa_dateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public string editsa_companystdate { get; set; }
        public string sa_yearsinbusiness { get; set; }
        public string sa_monthsinbusiness { get; set; }
        public string sa_startdate { get; set; }
        public string sa_enddate { get; set; }
        public string editsa_startdate { get; set; }
        public string editsa_enddate { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabank_name { get; set; }
        public string sabranch_name { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }

    }

    public class MdlMstInitiateApprovalList : result
    {
        public string sacontactinstitution_gid { get; set; }
        public string sa_reportingmanager { get; set; }
        public string interviewevalution { get; set; }
        public string applicationform { get; set; }
        public string yearsitreturns { get; set; }
        public string bankstatement { get; set; }
        public string kycdocuments { get; set; }
        public string prospect { get; set; }
        public string vettingstatus { get; set; }
        public string scannedcopyreception { get; set; }
        public string addressproof { get; set; }
        public string photographs { get; set; }
        public string cancelledcheckleaf { get; set; }
        public string houseofficeverification { get; set; }
        public string agreementexecutiondate { get; set; }
        public DateTime agreementexecution_date { get; set; }
        public string agreementexpirydate { get; set; }
        public DateTime agreementexpiry_date { get; set; }

        public string agreementstatus { get; set; }
        public string bookletnumber { get; set; }
        public string verificationremarks { get; set; }
        public string approvalinitiated_by { get; set; }
        public string approvalinitiated_date { get; set; }
        public string institutionchecker_name { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string checkeremployee_gid { get; set; }
        public string checkeremployee_name { get; set; }
        public string samapping_name { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public DateTime ratingas_date { get; set; }
        public string sa_associatename { get; set; }
        public string sa_dateofincorporation { get; set; }
        public DateTime sadateofincorporation { get; set; }
        public string sa_companystdate { get; set; }
        public DateTime sacompanystdate { get; set; }
        public string designation_type { get; set; }
        public string satype_name { get; set; }
        public string satype_gid { get; set; }
        public string saentitytype_gid { get; set; }
        public string saentitytype_name { get; set; }
        public string designation_gid { get; set; }
        public string sa_companypan { get; set; }
        public string sa_contactfirstname { get; set; }
        public string sa_contactmiddlename { get; set; }
        public string sa_contactlastname { get; set; }
        public string sa_annualturnover { get; set; }
        public string saifsc_code { get; set; }
        public string saaccount_number { get; set; }
        public string saaccountholder_name { get; set; }
        public string sacanccheque_number { get; set; }
        public string sabranch_name { get; set; }
        public string sabank_name { get; set; }
        public string openquerycount { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string micr { get; set; }
        public string city { get; set; }
        public string branch_address { get; set; }
        public string confirmbankaccountnumber { get; set; }
        public string agroagreementexecutiondate { get; set; }
        public DateTime agroagreementexecution_date { get; set; }
        public string agroagreementexpirydate { get; set; }
        public DateTime agroagreementexpiry_date { get; set; }
        public string agroagreementstatus { get; set; }
        public string sa_appcrediteddate { get; set; }
        public string sa_apputr { get; set; }
        public string sa_appcreditedamount { get; set; }
        public DateTime saappcrediteddate { get; set; }
        public List<samappinginstitutionassign_list> samappinginstitutionassign_list { get; set; }
    }
    public class samappinginstitutionassign_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string samappingassigninstitutionlog_gid { get; set; }
        public string institutionmaker_name { get; set; }
        public string institutionchecker_name { get; set; }
       public string created_by { get; set; }
    }

        public class MdlSACICIndividual : result
    {
        public string saindividual2bureau_gid { get; set; }
        public string sacontact_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sainstitution2bureau_gid { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public DateTime ratingas_date { get; set; }
        public string observations { get; set; }
        public string bureau_response { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public DateTime bureauscoredate { get; set; }
        public string cicdocument_name { get; set; }
        public string cicdocument_path { get; set; }
        public string document_content { get; set; }
        public string social_capital { get; set; }
        public string trade_capital { get; set; }
        public string overalllimit_amount { get; set; }
        public string processing_fee { get; set; }
        public string doc_charges { get; set; }
        public string application_gid { get; set; }
        public string bureauscoredateedit { get; set; }

        public List<sabureauuploaddoc_list> sabureauuploaddoc_list { get; set; }
    }

    public class sabureauuploaddoc_list
    {
        public string tmpcicdocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string document_content { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string contact_gid { get; set; }
        public string contact2bureau_gid { get; set; }
        public string institution_gid { get; set; }
        public string institution2bureau_gid { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_response { get; set; }

    }

    public class MdlSAInstituteBureau : result
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string sainstitution2bureau_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureau_response { get; set; }
        public string observations { get; set; }
        public List<sainstitutebureau_list> sainstitutebureau_list { get; set; }
        public List<sauploaddoc_list> sauploaddoc_list { get; set; }
    }

    public class sainstitutebureau_list
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string saindividual2bureau_gid { get; set; }
        public string sainstitution2bureau_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string bureau_score { get; set; }
        public string bureauscore_date { get; set; }
        public string bureau_response { get; set; }
        public string observations { get; set; }
    }

    public class sauploaddoc_list
    {
        public string[] filename { get; set; }
        public string filepath { get; set; }
        public string sacontact_gid { get; set; }
        public string saindividualmaildocument_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string sainstitutionmaildocument_gid { get; set; }
        public string saindividual2bureau_gid { get; set; }
        public string individualsabureaudocumentupload_gid { get; set; }
        public string sainstitutionverifydocument_gid { get; set; }
        public string sainstitution2bureau_gid { get; set; }
        public string saindividualverifydocument_gid { get; set; }
        public string document_name { get; set; }
        public string document_title { get; set; }
        public string document_path { get; set; }
        public string document_content { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string institutionsabureaudocumentupload_gid { get; set; }
        public string contact2bureau_gid { get; set; }
        public string institution_gid { get; set; }
        public string institution2bureau_gid { get; set; }
    }


    public class MdlSaChequeDocument : result
    {
        public string institutioncancelchequeupload_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public List<sachequedocument_list> sachequedocument_list { get; set; }
    }

    public class sachequedocument_list
    {
        public string individualcancelchequeupload_gid { get; set; }
        public string institutioncancelchequeupload_gid { get; set; }
        public string sacontactinstitution_gid { get; set; }
        public string document_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }

    }

    public class MdlApprove : result
    {
        public string sacontactinstitution_gid { get; set; }
        public string approval_status { get; set; }
        public string remarks { get; set; }
        public string rejected_remarks { get; set; }
        public string approval_flag { get; set; }
        public string sa_reportingmanager { get; set; }
        public string openquerycount { get; set; }
    }

    public class Mdlmakerinstitutionraisequery : result
    {

        public string sacontactinstitution_gid { get; set; }
        public string makerinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string makerinstitutionraisequery_status { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<makerinstitutionraisequery_list> makerinstitutionraisequery_list { get; set; }

    }
    public class makerinstitutionraisequery_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string makerinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string makerinstitutionraisequery_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
    }
    public class mdlcheckerinstitutionraisequery : result
    {

        public string sacontactinstitution_gid { get; set; }
        public string checkerinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string checkerinstitutionraisequery_status { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<checkerinstitutionraisequery_list> checkerinstitutionraisequery_list { get; set; }

    }
    public class checkerinstitutionraisequery_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string checkerinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string checkerinstitutionraisequery_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
    }
    public class Mdlapproverinstitutionraisequery : result
    {

        public string sacontactinstitution_gid { get; set; }
        public string approverinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string approverinstitutionraisequery_status { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public List<approverinstitutionraisequery_list> approverinstitutionraisequery_list { get; set; }

    }
    public class approverinstitutionraisequery_list
    {
        public string sacontactinstitution_gid { get; set; }
        public string approverinstitutionraisequery_gid { get; set; }
        public string query_title { get; set; }
        public string query_description { get; set; }
        public string approverinstitutionraisequery_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string queryresponse_remarks { get; set; }
        public string queryresponse_by { get; set; }
        public string queryresponse_date { get; set; }
    }
}