using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Controllers will store values for various masters custopedia
    /// </summary>
    /// <remarks>Written by Abilash.A, Kalaiarasan, Premchandar.K</remarks>


    public class MdlMstApplication360 : result
    {
        public string charge_flag { get; set; }
        public List<application_list> application_list { get; set; }
        public List<loantype_list> loantype_list { get; set; }
        public List<assessmentagency_list> assessmentagency_list { get; set; }
        public List<bureauname_list> bureauname_list { get; set; }
        public List<bankaccountlevel_list> bankaccountlevel_list { get; set; }
        public List<assessmentagencyrating_list> assessmentagencyrating_list { get; set; }
        public List<licensetype_list> licensetype_list { get; set; }
        public List<businesscategory_list> businesscategory_list { get; set; }
        public List<amlcategory_list> amlcategory_list { get; set; }
        public List<companytype_list> companytype_list { get; set; }
        public List<companydocument_list> companydocument_list { get; set; }
        public List<variety_list> variety_list { get; set; }
        public List<employee_list> employee_list { get; set; }

        public List<associatemaster_list> associatemaster_list { get; set; }
        public string product_gid { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string valuechain_gid { get; set; }
        public string valuechain_name { get; set; }

        public string variety_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public List<entity_list> entity_list { get; set; }
        public List<vertical_list> vertical_list { get; set; }

        public List<documentseverity_list> documentseverity_list { get; set; }
        public List<checklist_list> checklist_list { get; set; }
        public List<category_list> category_list { get; set; }
        public List<sector_list> sector_list { get; set; }
        public List<documenttype_list> documenttype_list { get; set; }
        public List<document_list> document_list { get; set; }
        public List<severity_list> severity_list { get; set; }
        public List<groupdoc_list> groupdoc_list { get; set; }

        public List<multipleloanproduct_list> multipleloanproduct_list { get; set; }

        public List<multipleloansubproduct_list> multipleloansubproduct_list { get; set; }

    }

    public class associatemaster_list

    {
        public string associatemaster_gid { get; set; }

        public string name { get; set; }

    }


    public class multipleloanproduct_list

    {
        public string loanproduct_gid { get; set; }

        public string loanproduct_name { get; set; }

    }

    public class multipleloansubproduct_list

    {
        public string loanproduct_gid { get; set; }

        public string loanproduct_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }

    }

    public class application_list
    {
        public string credittype_gid { get; set; }
        public string credittype_name { get; set; }
        public string credittypeoffacility_gid { get; set; }
        public string credittypeoffacility_name { get; set; }
        public string creditaccountclassification_gid { get; set; }
        public string creditaccountclassification_name { get; set; }
        public string creditinstalmentfrequency_name { get; set; }
        public string creditinstalmentfrequency_gid { get; set; }
        public string credittypeofexistingfunded_gid { get; set; }
        public string credittypeofexistingfunded_name { get; set; }
        public string businesscategory_gid { get; set; }
        public string businesscategory_name { get; set; }
        public string individualproof_name { get; set; }
        public string individualproof_gid { get; set; }
        public string businessindustrytype_gid { get; set; }
        public string businessindustrytype_name { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string relationship_gid { get; set; }
        public string relationship_name { get; set; }
        public string samunnatibranch_gid { get; set; }
        public string samunnatibranch_name { get; set; }
        public string samunnatibranchstate_gid { get; set; }
        public string samunnatibranchstate_name { get; set; }
        public string geneticcode_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string gender_gid { get; set; }
        public string gender_name { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string vernacular_language { get; set; }
        public string creditunderwritingfacilitytype_gid { get; set; }
        public string credit_underwriting_facility_type { get; set; }
        public string maritalstatus_gid { get; set; }
        public string maritalstatus_name { get; set; }
        public string educationalqualification_gid { get; set; }
        public string educationalqualification_name { get; set; }
        public string guaranteecoverage_gid { get; set; }
        public string guaranteecoverage_name { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string satype_gid { get; set; }
        public string satype_name { get; set; }
        public string incometype_gid { get; set; }
        public string incometype_name { get; set; }
        public string lendingarrangement_gid { get; set; }
        public string lendingarrangement_name { get; set; }
        public string partytype_gid { get; set; }
        public string partytype_name { get; set; }
        public string assetstype_name { get; set; }
        public string assetstype_gid { get; set; }
        public string caste_name { get; set; }
        public string caste_gid { get; set; }
        public string constructiontype_gid { get; set; }
        public string constructiontype_name { get; set; }
        public string typeofchargecreated_name { get; set; }
        public string typeofchargecreated_gid { get; set; }
        public string securitycoverage_gid { get; set; }
        public string securitycoverage_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_code { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string verticaltaggs_name { get; set; }
        public string vertical_code { get; set; }
        public string sadocumentlist_gid { get; set; }
        public string sadocumentlist_name { get; set; }
        public string saentitytype_gid { get; set; }
        public string saentitytype_name { get; set; }
        public string loanpurpose_name { get; set; }
        public string loanpurpose_gid { get; set; }
        public string typeofdebt_name { get; set; }
        public string typeofdebt_gid { get; set; }
        public string lms_code { get; set; }
        public string supplier_ref_no { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string religion_gid { get; set; }
        public string religion_name { get; set; }
        public string ownershiptype_gid { get; set; }
        public string ownershiptype_name { get; set; }
        public string residencetype_gid { get; set; }
        public string residencetype_name { get; set; }
        public string licensetype_gid { get; set; }
        public string licensetype_name { get; set; }
        public string areatype_gid { get; set; }
        public string areatype_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string principalfrequency_name { get; set; }
        public string fundedtypeindicator_gid { get; set; }
        public string fundedtypeindicator_name { get; set; }
        public string individualdocument_gid { get; set; }
        public string individualdocument_name { get; set; }
        public string companydocument_name { get; set; }
        public string companydocument_gid { get; set; }
        public string companytype_gid { get; set; }
        public string companytype_name { get; set; }
        public string amlcategory_gid { get; set; }
        public string amlcategory_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string securityclassification_gid { get; set; }
        public string securityclassification_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }
        public string colendingmaster_gid { get; set; }
        public string colendingmaster_name { get; set; }
        public string description { get; set; }
        public string lendertype_gid { get; set; }
        public string lendertype_name { get; set; }
        public string creditpolicycompliance_gid { get; set; }
        public string creditpolicycompliance_name { get; set; }
        public string assessmentcriteria_gid { get; set; }
        public string assessmentcriteria_name { get; set; }
        public string propertyin_gid { get; set; }
        public string propertyin_name { get; set; }
        public string lineofactivity_gid { get; set; }
        public string lineof_activity { get; set; }
        public string bsrcode_gid { get; set; }
        public string bsr_code { get; set; }
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
        public string pslcategory_gid { get; set; }
        public string psl_category { get; set; }
        public string weakersection_gid { get; set; }
        public string weaker_section { get; set; }
        public string pslpurpose_gid { get; set; }
        public string psl_purpose { get; set; }
        public string occupation_gid { get; set; }
        public string occupation_name { get; set; }
        public string turnover_gid { get; set; }
        public string turnover_name { get; set; }
        public string msme_gid { get; set; }
        public string msme_name { get; set; }
        public string natureofentity_gid { get; set; }
        public string natureofentity_name { get; set; }
        public string purposecolumn_gid { get; set; }
        public string purposecolumn_name { get; set; }
        public string investment_gid { get; set; }
        public string investment_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankname_gid { get; set; }
        public string bankname_name { get; set; }
        public string property_gid { get; set; }
        public string property_name { get; set; }
        public string clientdetails_gid { get; set; }
        public string clientdetails_name { get; set; }
        public string program_gid { get; set; }
        public string program { get; set; }
        public string sourceofcontact_gid { get; set; }
        public string sourceofcontact_name { get; set; }
        public string calltype_gid { get; set; }
        public string calltype_name { get; set; }
        public string telecallingfunction_gid { get; set; }
        public string telecallingfunction_name { get; set; }
        public string callreceivednumber_gid { get; set; }
        public string callreceivednumber_name { get; set; }
        public string Status { get; set; }
        public string product_gid { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string valuechain_gid { get; set; }
        public string valuechain_name { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string variety_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string groupdocument_gid { get; set; }
        public string groupdocument_name { get; set; }
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string individualchecklist_gid { get; set; }
        public string checklist_name { get; set; }
        public string companychecklist_gid { get; set; }
        public string groupchecklist_gid { get; set; }
        public string warehousefacility_gid { get; set; }
        public string warehousefacility_name { get; set; }
        public string scope_gid { get; set; }
        public string scope_name { get; set; }
        public string othercreditorapplicanttype_gid { get; set; }
        public string othercreditorapplicanttype_name { get; set; }
        public string milestonepaymenttype_gid { get; set; }
        public string milestonepaymenttype_name { get; set; }
        public string natureformstateofcommodity_gid { get; set; }
        public string natureformstateofcommodity_name { get; set; }
        public string api_code { get; set; }

        public List<employee_list> employee_list { get; set; }
        public List<checklist_list> checklist_list { get; set; }

    }
    public class application360 : result
    {
        public string credittypeoffacility_gid { get; set; }
        public string credittypeoffacility_name { get; set; }
        public string creditaccountclassification_gid { get; set; }
        public string creditaccountclassification_name { get; set; }
        public string creditinstalmentfrequency_gid { get; set; }
        public string creditinstalmentfrequency_name { get; set; }
        public string credittypeofexistingfunded_gid { get; set; }
        public string credittypeofexistingfunded_name { get; set; }
        public string credittype_gid { get; set; }
        public string credittype_name { get; set; }
        public string lms_code { get; set; }
        public string supplier_ref_no { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string remarks { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string incometype_gid { get; set; }
        public string incometype_name { get; set; }
        public string individualproof_name { get; set; }
        public string individualproof_gid { get; set; }
        public string businesscategory_name { get; set; }
        public string businesscategory_gid { get; set; }
        public string lendingarrangement_name { get; set; }
        public string lendingarrangement_gid { get; set; }
        public string partytype_gid { get; set; }
        public string partytype_name { get; set; }
        public string assetstype_gid { get; set; }
        public string assetstype_name { get; set; }
        public string caste_name { get; set; }
        public string caste_gid { get; set; }
        public string constructiontype_gid { get; set; }
        public string constructiontype_name { get; set; }
        public string typeofchargecreated_name { get; set; }
        public string typeofchargecreated_gid { get; set; }
        public string securitycoverage_gid { get; set; }
        public string securitycoverage_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_code { get; set; }
        public string verticaltaggs_name { get; set; }
        public string verticaltaggs_gid { get; set; }
        public string vertical_code { get; set; }
        public string sadocumentlist_gid { get; set; }
        public string sadocumentlist_name { get; set; }
        public string saentitytype_gid { get; set; }
        public string saentitytype_name { get; set; }
        public string loanpurpose_name { get; set; }
        public string loanpurpose_gid { get; set; }
        public string typeofdebt_name { get; set; }
        public string typeofdebt_gid { get; set; }
        public string amlcategory_gid { get; set; }
        public string amlcategory_name { get; set; }
        public string businessindustrytype_name { get; set; }
        public string businessindustrytype_gid { get; set; }
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
        public string relationship_gid { get; set; }
        public string relationship_name { get; set; }
        public string samunnatibranch_gid { get; set; }
        public string samunnatibranch_name { get; set; }
        public string samunnatibranchstate_gid { get; set; }
        public string samunnatibranchstate_name { get; set; }
        public string geneticcode_gid { get; set; }
        public string geneticcode_name { get; set; }
        public string gender_gid { get; set; }
        public string gender_name { get; set; }
        public string vernacularlanguage_gid { get; set; }
        public string vernacular_language { get; set; }
        public string creditunderwritingfacilitytype_gid { get; set; }
        public string credit_underwriting_facility_type { get; set; }
        public string maritalstatus_gid { get; set; }
        public string maritalstatus_name { get; set; }
        public string educationalqualification_gid { get; set; }
        public string educationalqualification_name { get; set; }
        public string guaranteecoverage_gid { get; set; }
        public string guaranteecoverage_name { get; set; }
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
        public string satype_gid { get; set; }
        public string satype_name { get; set; }
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
        public string religion_gid { get; set; }
        public string religion_name { get; set; }
        public string ownershiptype_gid { get; set; }
        public string ownershiptype_name { get; set; }
        public string residencetype_gid { get; set; }
        public string residencetype_name { get; set; }
        public string licensetype_gid { get; set; }
        public string licensetype_name { get; set; }
        public string areatype_gid { get; set; }
        public string areatype_name { get; set; }
        public string interestfrequency_gid { get; set; }
        public string interestfrequency_name { get; set; }
        public string principalfrequency_gid { get; set; }
        public string principalfrequency_name { get; set; }
        public string fundedtypeindicator_gid { get; set; }
        public string fundedtypeindicator_name { get; set; }
        public string individualdocument_gid { get; set; }
        public string individualdocument_name { get; set; }
        public string companydocument_name { get; set; }
        public string companydocument_gid { get; set; }
        public string companytype_gid { get; set; }
        public string companytype_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
        public string assessmentagencyrating_name { get; set; }
        public string securityclassification_gid { get; set; }
        public string securityclassification_name { get; set; }
        public string loansubproduct_gid { get; set; }
        public string loansubproduct_name { get; set; }
        public string colendingmaster_gid { get; set; }
        public string colendingmaster_name { get; set; }
        public string description { get; set; }
        public string lendertype_gid { get; set; }
        public string lendertype_name { get; set; }
        public string creditpolicycompliance_gid { get; set; }
        public string creditpolicycompliance_name { get; set; }
        public string assessmentcriteria_gid { get; set; }
        public string assessmentcriteria_name { get; set; }
        public string lineofactivity_gid { get; set; }
        public string lineof_activity { get; set; }
        public string bsrcode_gid { get; set; }
        public string bsr_code { get; set; }
        public string supplier_gid { get; set; }
        public string supplier_name { get; set; }
        public string pslcategory_gid { get; set; }
        public string psl_category { get; set; }
        public string weakersection_gid { get; set; }
        public string weaker_section { get; set; }
        public string pslpurpose_gid { get; set; }
        public string psl_purpose { get; set; }
        public string occupation_gid { get; set; }
        public string occupation_name { get; set; }
        public string turnover_gid { get; set; }
        public string turnover_name { get; set; }
        public string msme_gid { get; set; }
        public string msme_name { get; set; }
        public string natureofentity_gid { get; set; }
        public string natureofentity_name { get; set; }
        public string purposecolumn_gid { get; set; }
        public string purposecolumn_name { get; set; }
        public string investment_gid { get; set; }
        public string investment_name { get; set; }
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
        public string bankname_gid { get; set; }
        public string bankname_name { get; set; }
        public string property_gid { get; set; }
        public string property_name { get; set; }
        public string clientdetails_gid { get; set; }
        public string clientdetails_name { get; set; }
        public string program_gid { get; set; }
        public string program { get; set; }
        public string sourceofcontact_gid { get; set; }
        public string sourceofcontact_name { get; set; }
        public string calltype_gid { get; set; }
        public string calltype_name { get; set; }
        public string telecallingfunction_gid { get; set; }
        public string telecallingfunction_name { get; set; }
        public string callreceivednumber_gid { get; set; }
        public string callreceivednumber_name { get; set; }
        public string product_gid { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string valuechain_gid { get; set; }
        public string valuechain_name { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        //public string status { get; set; }
        public string groupdocument_gid { get; set; }
        public string groupdocument_name { get; set; }
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string warehousefacility_gid { get; set; }
        public string warehousefacility_name { get; set; }
        public string scope_gid { get; set; }
        public string scope_name { get; set; }
        public string othercreditorapplicanttype_gid { get; set; }
        public string othercreditorapplicanttype_name { get; set; }
        public string milestonepaymenttype_gid { get; set; }
        public string milestonepaymenttype_name { get; set; }
        public string natureformstateofcommodity_gid { get; set; }
        public string natureformstateofcommodity_name { get; set; }

        public List<documenttype_list> documenttype_list { get; set; }
        public List<employee_list> employee_list { get; set; }
        public string program_refno { get; set; }

        public string approved_date { get; set; }
        public string maximum_limit { get; set; }
        public string program_limit { get; set; }
        public string program_description { get; set; }
        public List<vertical_list> vertical_list { get; set; }

        public List<employeelist> employeelist { get; set; }

        public List<approvedby> approvedby { get; set; }
        public List<programvertical_list> programvertical_list { get; set; }
        public List<cadprogramapproval_list> cadprogramapproval_list { get; set; }

    }
    public class ApplicationInactiveHistory : result
    {
        public List<inactivehistory_list> inactivehistory_list { get; set; }
    }
    public class inactivehistory_list
    {
        public string status { get; set; }
        public string remarks { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }

    public class documenttype_list
    {
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
    }

    public class document_list
    {
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
    }

    public class severity_list
    {
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
    }
    public class groupdoc_list
    {
        public string groupdocument_gid { get; set; }
        public string groupdocument_name { get; set; }
    }

    //check list
    public class checklist : result
    {
        public string individualdocument_gid { get; set; }
        public string individualchecklist_gid { get; set; }
        public string checklist_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }

        public List<checklist_list> checklist_list { get; set; }
        public List<employee_list> employee_list { get; set; }

        public string companydocument_gid { get; set; }
        public string companychecklist_gid { get; set; }

    }


    public class checklist_list
    {
        public string individualdocument_gid { get; set; }
        public string individualchecklist_gid { get; set; }
        public string checklist_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public List<employee_list> employee_list { get; set; }
        public string companydocument_gid { get; set; }
        public string companychecklist_gid { get; set; }
        public string groupchecklist_gid { get; set; }
        public string groupdocument_gid { get; set; }

    }

    public class Mdlentitylist
    {
        public List<entitylist> entitylist { get; set; }
    }
    public class entitylist
    {
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
    }
    public class MdlSATypeList
    {
        public List<satype_list> satype_list { get; set; }
    }
    public class satype_list
    {
        public string satype_name { get; set; }
        public string satype_gid { get; set; }
    }

    public class loantype_list
    {
        public string loantype_gid { get; set; }
        public string loan_type { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
    public class loantype : result
    {
        public string loantype_gid { get; set; }
        public string loan_type { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
    public class countrycode : result
    {
        public string countrycode_gid { get; set; }
        public string country_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public List<countrycode_list> countrycode_list { get; set; }
    }
    public class countrycode_list
    {
        public string countrycode_gid { get; set; }
        public string country_code { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }

    public class Mdlloantermperiod : result
    {
        public string loantermperiod_gid { get; set; }
        public string loanterm_period { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public List<loanterm_period_list> loanterm_period_list { get; set; }
    }
    public class loanterm_period_list
    {
        public string loantermperiod_gid { get; set; }
        public string loanterm_period { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
    public class Mdlamortization_method : result
    {
        public string amortization_gid { get; set; }
        public string amortization_method { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
        public List<amortizationmethod_list> amortizationmethod_list { get; set; }
    }
    public class amortizationmethod_list
    {
        public string amortization_gid { get; set; }
        public string amortization_method { get; set; }
        public string bureau_code { get; set; }
        public string lms_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status_log { get; set; }
        public string remarks { get; set; }
    }
    public class MdlLoanProductList
    {
        public List<loanproduct_list> loanproduct_list { get; set; }
    }
    public class loanproduct_list
    {
        public string loanproduct_gid { get; set; }
        public string loanproduct_name { get; set; }
        public string loansubproduct_name { get; set; }
        public string program2loanproduct_gid { get; set; }
    }
    
    public class assessmentagency_list
    {
        public string assessmentagency_gid { get; set; }
        public string assessmentagency_name { get; set; }
    }
    public class assessmentagencyrating_list
    {
        public string assessmentagencyrating_name { get; set; }
        public string assessmentagencyrating_gid { get; set; }
    }
    public class bureauname_list
    {
        public string bureauname_gid { get; set; }
        public string bureauname_name { get; set; }
    }
    public class bankaccountlevel_list
    {
        public string bankaccountlevel_gid { get; set; }
        public string bankaccountlevel_name { get; set; }
    }
    public class licensetype_list
    {
        public string licensetype_gid { get; set; }
        public string licensetype_name { get; set; }
    }
    public class businesscategory_list
    {
        public string businesscategory_gid { get; set; }
        public string businesscategory_name { get; set; }
    }
    public class amlcategory_list
    {
        public string amlcategory_gid { get; set; }
        public string amlcategory_name { get; set; }
    }
    public class companytype_list
    {
        public string companytype_gid { get; set; }
        public string companytype_name { get; set; }
    }
    public class companydocument_list
    {
        public string companydocument_gid { get; set; }
        public string companydocument_name { get; set; }
    }
    public class bankaccounttype_list
    {
        public string bankaccounttype_gid { get; set; }
        public string bankaccounttype_name { get; set; }
    }

    public class variety_list
    {
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string variety_name { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }

        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public List<employee_list> employee_list { get; set; }


    }


    public class variety : result
    {
        public string product_gid { get; set; }
        public string variety_gid { get; set; }
        public string botanical_name { get; set; }
        public string alternative_name { get; set; }
        public string variety_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }


        public List<variety_list> variety_list { get; set; }
        public List<employee_list> employee_list { get; set; }
    }

    public class sector_list
    {
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }

    }
    public class category_list
    {
        public string valuechain_gid { get; set; }
        public string valuechain_name { get; set; }

    }

    public class MdlDocumentType : result
    {
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
        public string description { get; set; }
        public string documenttype_code { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public List<documenttype> documenttype { get; set; }
    }

    public class documenttype : result
    {
        public string documenttypes_gid { get; set; }
        public string documenttype_code { get; set; }
        public string documenttype_name { get; set; }
        public string description { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }


    }


    public class MdlProgram : result
    {
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string program_description { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string program_limit { get; set; }
        public string maximum_limit { get; set; }
        public string approved_date { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string program { get; set; }
        public string program_refno { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }

        public List<uploadprogramdocumentlist> uploadprogramdocumentlist { get; set; }
        public List<programvertical_list> programvertical_list { get; set; }
        public List<verticalpro> verticalpro { get; set; }
        public List<approvedby> approvedby { get; set; }
        public List<approvedby_list> approvedby_list { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<employeelist> employeelist { get; set; }
        public string Status { get; set; }
        public char rbo_status { get; set; }

    }




    public class verticalpro
    {
        public string program2vertical_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
    }

    public class approvedby
    {
        public string program2approval_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class approvedby_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class programvertical_list
    {
        public string program2vertical_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
    }

    public class cadprogramapproval_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class uploadprogramdocumentlist
    {
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string programdocument_gid { get; set; }
        public string program_gid { get; set; }
        public string file_name { get; set; }
        public string path { get; set; }
        public string created_date { get; set; }
        public string uploaded_by { get; set; }
        public string upload_by { get; set; }
        public string document_type { get; set; }
        public string updated_date { get; set; }
    }

    public class MstDigitalSignature : result
    {
        public List<employeelist> employeelist { get; set; }
        public List<digitalsignaturelist> digitalsignaturelist { get; set; }
    }

    public class uploadSignature : result
    {
        public List<upload_list> upload_list { get; set; }
    }

    public class digitalsignaturelist
    {
        public string digitalsignature_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string document_name { get; set; }
        public string document_path { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class entity_list
    {
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string entity_code { get; set; }

    }

    public class documentseverity_list
    {

        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
    }

    public class companydocument : result
    {
        public string companydocument_gid { get; set; }
        public string companydocument_name { get; set; }
        public string covenant_type { get; set; }
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string Status { get; set; }
    }

    public class groupdocument : result
    {
        public string groupdocument_gid { get; set; }
        public string groupdocument_name { get; set; }
        public string covenant_type { get; set; }
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string Status { get; set; }
    }

    public class individualdocument : result
    {
        public string individualdocument_name { get; set; }
        public string individualdocument_gid { get; set; }
        public string covenant_type { get; set; }
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
        public string documentseverity_gid { get; set; }
        public string documentseverity_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string Status { get; set; }
    }
    public class MdlCreditOpsGroupAdd : result
    {
        public string creditopsgroupmapping_gid { get; set; }
        public string creditopsgroup_id { get; set; }
        public string creditopsgroup_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public List<creditopsmaker> creditopsmaker { get; set; }
        public List<creditopschecker> creditopschecker { get; set; }
    }
    public class creditopsmaker
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class creditopschecker
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class MdlCreditOpsGroup : result
    {        
        public List<creditopsgroup_list> creditopsgroup_list { get; set; }
        public List<CreditOpsMakerList> CreditOpsMakerList { get; set; }
        public List<CreditOpsCheckerList> CreditOpsCheckerList { get; set; }
        public List<Creditmaker_list> Creditmaker_list { get; set; }
        public List<Creditchecker_list> Creditchecker_list { get; set; }
        public List<creditopsmaker> creditopsmaker { get; set; }
        public List<creditopschecker> creditopschecker { get; set; }
        public List<CreditOpsStatuslog> CreditOpsStatuslog { get; set; }
        public string creditopsgroupmapping_gid { get; set; }
        public string creditopsgroup_name { get; set; }
        public string creditopsgroup_status { get; set; }
        public string vertical_name { get; set; }
        public string vertical_gid { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
    }
    public class CreditOpsStatuslog
    {
        public string creditopsgroupmapping_gid { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
    }
    public class Creditmaker_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditchecker_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class CreditOpsMakerList
    {
        public string creditops2maker_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class CreditOpsCheckerList
    {
        public string creditops2checker_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class creditopsgroup_list
    {
        public string creditopsgroup_id { get; set; }
        public string creditopsgroupmapping_gid { get; set; }
        public string creditopsgroup_name { get; set; }
        public string creditopsgroup_status { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string vertical_name { get; set; }
    }
    public class creditopsheads : result
    {
        public string creditopsmaker_name { get; set; }
        public string creditopschecker_name { get; set; }
        public string creditopsgroupmapping_gid { get; set; }
        public string creditopsgroup_name { get; set; }
    }

    public static class getAutoApprovalClass
    {
        //public const string
        //     CreditGroupName = "Samagro System Credit",
        //     CCGroupName = "Samagro System CC";
        public const string
             CreditGroupName = "IT Internal Use - Do not select this",
             CCGroupName = "IT Internal Use - Do not select this";
        
    }

    public class MdlInstitutionDocType : result
    {
        public List<institutiondoctype_list> institutiondoctype_list { get; set; }
    }

    public class institutiondoctype_list
    {
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
    }
    public class MdlIndividualDocType : result
    {
        public List<individualdoctype_list> individualdoctype_list { get; set; }
    }

    public class individualdoctype_list
    {
        public string documenttypes_gid { get; set; }
        public string documenttype_name { get; set; }
    }

}