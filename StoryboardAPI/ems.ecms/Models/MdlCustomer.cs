using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// customer Controller Class containing API methods for accessing the  Model class MdlCustomer
    /// To get customer details, state name, ccmail details, customer details submit, UnTag NPA, Tag NPA, Tagged NPA Customer List,
    /// NPA Tagged History List, Export Customer details to excel, Common Customer detail 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class MdlCustomer : result
    {
        public List<customer_list> customer_list { get; set; }
        public List<state_list> state_list { get; set; }
        public List<customerdeferral_list> customerdeferral_list { get; set; }
        public string ccmail { get; set; }
        public List<constitution_list> constitution_list { get; set; }

    }
    public class customerdeferral_list
    {
        public string record_id { get; set; }
        public string tracking_type { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string deferral_name { get; set; }
        public string deferral_gid { get; set; }
        public string deferral_category { get; set; }
        public string deferral_status { get; set; }
        public string due_date { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string criticallity { get; set; }
        public string loanGID { get; set; }
        public string loanTitle { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string covenanttype_gid { get; set; }
        public string deferraltype_gid { get; set; }
        public string covenanttype_name { get; set; }
        public string remarks { get; set; }
        public string customer_remarks { get; set; }
        public string status { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string entity_gid { get; set; }
        public string entity_name { get; set; }
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string aging { get; set; }
        public string mail_status { get; set; }
        public string extended_date { get; set; }
    }
    public class customer_list
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string vertical_code { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerName { get; set; }
        public string mail_count { get; set; }
        public string legaltag_flag { get; set; }
        public string legal_tagging { get; set; }
        public string npatag_flag { get; set; }
        public string npa_tagging { get; set; }
        public string customer_urn { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class customerSummary : result
    {        
        public string lspath { get; set; }
        public string lsname { get; set; }       
    }

    public class mdlcreatecustomer : result
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
        public string mobileno { get; set; }
        public string contactnumber { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string region { get; set; }
        public string state { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string regionalHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerGid { get; set; }
        public string tomail { get; set; }
        public string ccmail { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string regionalhead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string creditmanager_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string state_gid { get; set; }
        public string customer_urn { get; set; }
        public string gst_number { get; set; }
        public string pan_number { get; set; }
        public string constitution_name { get; set; }
        public string constitution_gid { get; set; }
        public string major_corporate { get; set; }
        public string zonal_riskmanagerGID { get; set; }
        public string zonal_riskmanagerName { get; set; }
        public string risk_managerGID { get; set; }
        public string riskmanager_name { get; set; }
        public string riskMonitoring_GID { get; set; }
        public string riskMonitoring_Name { get; set; }
    }
    public class customeredit : result
    {
        public string customer_gid { get; set; }
        public string customerCodeedit { get; set; }
        public string customerNameedit { get; set; }
        public string contactPersonedit { get; set; }
        public string mobileNoedit { get; set; }
        public string contactnoedit { get; set; }
        public Double mobileNo_edit { get; set; }
        public Double contactno_edit { get; set; }
        public string emailedit { get; set; }
        public string addressline1edit { get; set; }
        public string regionedit { get; set; }
        public string addressline2edit { get; set; }
        public string tomailedit { get; set; }
        public string ccmailedit { get; set; }
        public string countryedit { get; set; }
        public string stateedit { get; set; }
        public string postalcodeedit { get; set; }
        public Double postalcode_edit { get; set; }
        public string zonalGid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string businessHeadGid { get; set; }
        public string regionalHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string regionalhead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string creditmanager_name { get; set; }
        public string state_gid { get; set; }
        public string state { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string customer_urnedit { get; set; }
        public string gst_number { get; set; }
        public string pan_number { get; set; }
        public string constitution_nameedit { get; set; }
        public string constitution_gidedit { get; set; }
        public string major_corporateedit { get; set; }
        public string zonal_riskmanagerGID { get; set; }
        public string zonal_riskmanagerName { get; set; }
        public string risk_managerGID { get; set; }
        public string risk_managerName { get; set; }
        public string riskMonitoring_GID { get; set; }
        public string riskMonitoring_Name { get; set; }
    }

    public class state_list
    {
        public string state_name { get; set; }
        public string state_gid { get; set; }
    }
   public class constitution_list
    {
        public string constitution_gid { get; set; }
        public string constitution_name { get; set; }
    }

    public class customerurndetails:result
    {
        public string customer_gid { get; set; }
        public string newcustomer_urn { get; set; }
        public string currentcustomer_urn { get; set; }
    }

    public class CustomersList:result 
    {
        public List<Customers> Customers { get; set; }
    }


    public class Customers
    {
        public string customer_gid { get; set; }
        public string customername { get; set; }
    }

    public class mdltagtolegal : result
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string tag_remarks { get; set; }
        public string currentcustomer_urn { get; set; }
        public List<customertag_list> customertag_list { get; set; }
    }

    public class mdluntagtolegal : result
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string untag_remarks { get; set; }
        public string currentcustomer_urn { get; set; }
        public List<customeruntag_list> customeruntag_list { get; set; }
    }

    public class customertag_list
    {
        public string customer_gid { get; set; }
        public string customer2legalhistory_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical { get; set; }
        public string tagged_by { get; set; }
        public string tagged_date { get; set; }
        public string remarks { get; set; }
    }

    public class customeruntag_list
    {
        public string customer_gid { get; set; }
        public string customer2legalhistory_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical { get; set; }
        public string untagged_by { get; set; }
        public string untagged_date { get; set; }
        public string remarks { get; set; }
    }

    public class mdltagtonpa : result
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string tag_remarks { get; set; }
        public string currentcustomer_urn { get; set; }
        public List<customertagnpa_list> customertagnpa_list { get; set; }
    }

    public class mdluntagtonpa : result
    {
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string untag_remarks { get; set; }
        public string currentcustomer_urn { get; set; }
        public List<customertagnpa_list> customeruntagnpa_list { get; set; }
    }

    public class customertagnpa_list
    {
        public string customer_gid { get; set; }
        public string customer2npahistory_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical { get; set; }
        public string tagged_by { get; set; }
        public string tagged_date { get; set; }
        public string remarks { get; set; }
    }

    public class customeruntagnpa_list
    {
        public string customer_gid { get; set; }
        public string customer2npahistory_gid { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical { get; set; }
        public string untagged_by { get; set; }
        public string untagged_date { get; set; }
        public string remarks { get; set; }
    }

}