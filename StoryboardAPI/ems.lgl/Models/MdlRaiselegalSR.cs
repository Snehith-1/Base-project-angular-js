using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.lgl.Models
{
    public class MdlRaiselegalSR:result
    {
        public string contact_location { get; set; }
        public string contact_mobileno { get; set; }
        public string created_by { get; set; } 
        public string customer_urn { get; set; }
        public string legalSR_gid { get; set; }
        public string contact_name { get; set; }     
        public string legalsr_gid { get; set; }
        public string srref_no { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string account_name { get; set; }
        public string constitution { get; set; }
        public string financed_by { get; set; }
        public string deal_year { get; set; }
        public string business_activity { get; set; }
        public string email_id { get; set; }
        public string address { get; set; }
        public string primary_securities { get; set; }
        public string collateral_securities { get; set; }
        public string details_UDC_PDC { get; set; }
        public string unit_working_status { get; set; }
        public string other_banker_exposures { get; set; }
        public string other_banker_borrower { get; set; }
        public string cibil_data { get; set; }
        public string restructuring_data { get; set; }
        public string status_current_overdue { get; set; }
        public string other_group_companies { get; set; }
        public string meeting_details { get; set; }
        public string cycles_sanctiondated { get; set; }
        public string limit_sanction { get; set; }
        public string churing_account { get; set; }
        public string created_date { get; set; }
        public string otherbankers_borrower { get; set; }
        public string instances_PTP { get; set; }
        public string statuslegal_action { get; set; }
        public string demandnotice_details { get; set; }
        public string templegalsr_gid { get; set; }
        public string raiselegalSR_gid { get; set; }     
        public string mobile_no { get; set; }
        public string remarks { get; set; }
        public string auth_status { get; set; }
        public string auth_remarks { get; set; }
        public int lsdeal_year { get; set; }
        public string raised_by { get; set; }
        public string raised_date { get; set; }
        public string raised_by_department { get; set; }
        public string raised_by_mobileno { get; set; }
        public string raised_by_emailid { get; set; }
        public List<promoter_list> promoter_list { get; set; }
        public List<customer_list> customer_list { get; set; }
        public List<guarantor_list> guarantor_list { get; set; }
        public List<RaiselegalSR_list> RaiselegalSR_list { get; set; }
        public List<repaymentcases_list> repaymentcases_list { get; set; }
        public List<auth_remarks_list> auth_remarks_list { get; set; }

    }
    public class RaiselegalSR_list
    {
        public string legalsr_gid { get; set; }
        public string templegalsr_gid { get; set; }
        public string srref_no { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string account_name { get; set; }
        public string constitution { get; set; }
        public string financed_by { get; set; }
        public string mobile_no { get; set; }
        public string deal_year { get; set; }
        public string business_activity { get; set; }
        public string email_id { get; set; }
        public string address { get; set; }
        public string primary_securities { get; set; }
        public string collateral_securities { get; set; }
        public string details_UDC_PDC { get; set; }
        public string unit_working_status { get; set; }
        public string other_banker_exposures { get; set; }
        public string cibil_data { get; set; }
        public string restructuring_data { get; set; }
        public string status_current_overdue { get; set; }
        public string other_group_companies { get; set; }
        public string meeting_details { get; set; }
        public string raised_by { get; set; }
        public string raised_date { get; set; }
        public string raised_by_department { get; set; }
        public string auth_status { get; set; }
        public string auth_remarks { get; set; }       
        public string approval_status { get; set; }
        public string urn { get; set; }
    }
    public class promoter_list
    {
        public string customer_gid { get; set; }
        public string promoter_name { get; set; }
        public string promoter_designation { get; set; }
        public string promoter_age { get; set; }
        public string promotermobile_no { get; set; }
    }
    public class customer_list
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string contactperson { get; set; }
       
       
    }
    public class guarantor_list
    {
        public string customer_gid { get; set; }
        public string guarantors_name { get; set; }
        public string guarantor_age { get; set; }
        public string networth { get; set; }
        public string basisofNW { get; set; }
    }
    public class facility : result
    {
        public string facility_type { get; set; }
        public string limit { get; set; }
        public string outstanding { get; set; }
        public string customer_gid { get; set; }
        public List<facility_list> facility_list { get; set; }
    }
    public class facility_list
    {
        public string facility_gid { get; set; }
        public string facility_type { get; set; }
        public string limit { get; set; }
        public string outstanding { get; set; }
    }
    public class repaymentcases_list
    {
        public string misdata_gid { get; set; }
        public string account_no { get; set; }
        public string disbursement_date { get; set; }
        public string disbursement_amount { get; set; }
        public string maturity_date { get; set; }
        public string interest { get; set; }
        public string od_days { get; set; }
        public string latecharge_due { get; set; }
        public string latecharge { get; set; }
        public string ledger { get; set; }
        public string ac_status { get; set; }
        public string ac_closed_date { get; set; }
        public string last_payment { get; set; }
        public string payment { get; set; }
        public string urn { get; set; }
        public string tenure { get; set; }
        public string frequency { get; set; }
        public string schedulde_payment { get; set; }
        public string netpayoff_amount { get; set; }
        public string AccountName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string nextdemandrundat { get; set; }
        public string lastdemandrundate { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string RO_Name { get; set; }
        public string Customer_Type { get; set; }
        public string Vertical { get; set; }
    }
    public class approvalstatus:result 
    {
       
        public string  approval_remarks { get;set;}
        public string legalsr_gid { get; set; }
        public List<approvallist> approvallist { get; set; }
    }
    public class approvallist
    {
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string approval_date { get; set; }

    }
    public class dndetails : result
    {
        public List<dndetails_list> dndetails_list { get; set; }
    }
    public class dndetails_list
    {

    }
    public class contactdetailsRM_list : result
    {
        public string customer_gid { get; set; }
        public List <contactdetailsRM> contactdetailsRM { get; set; }
    }
    public class contactdetailsRM
    {
        public string customer_gid { get; set; }
        public string contact_name { get; set; }
        public string contact_location { get; set; }
        public string contact_mobileno { get; set; }
        public string tmpcontactdtl_gid { get; set; }
    }
  public class mdlcustomer:result
    {

        public string customer_gid { get; set; }
        public string customerCodeedit { get; set; }
        public string customerNameedit { get; set; }
        public string contactPersonedit { get; set; }
        public string mobileNoedit { get; set; }
        public string contactnoedit { get; set; }
        public string emailedit { get; set; }
        public string addressline1edit { get; set; }
        public string regionedit { get; set; }
        public string addressline2edit { get; set; }
        public string tomailedit { get; set; }
        public string ccmailedit { get; set; }
        public string countryedit { get; set; }
        public string stateedit { get; set; }
        public string postalcodeedit { get; set; }
        public string zonalGid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string creditmanager_name { get; set; }
        public string state_gid { get; set; }
        public string state { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string customer_urnedit { get; set; }
    }
    public class Demand_notice:result
    {
        public string demand_status { get; set; }
        public List <demandnotice_list> demandnotice_list { get; set; }
    }
    public class demandnotice_list
    {
        public string dn1status { get; set; }
        public string dn2status { get; set; }
        public string dn3status { get; set; }
        public string dn1send_date { get; set; }
        public string dn2send_date { get; set; }
        public string dn3send_date { get; set; }
        public string dn1send_by { get; set; }
        public string dn2send_by { get; set; }
        public string dn3send_by { get; set; }

    }

    public class sanctionloanlist : result
    {
        public string customer_gid { get; set; }   
        public string totalsanction_amount { get; set; }
        public List<customer2loandtl> customer2loandtl { get; set; }
       }
    public class auth_remarks_list : result
    {
        public string auth_remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string auth_status { get; set; }

        

    }


    

    public class customer2loandtl : result
    {
        public string loanref_no { get; set; }
        public string loan_title { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string vertical { get; set; }
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string sanction_gid { get; set; }
    }
    public class customerpromotorslist : result
    {
        public List<customerPromoter> customerPromoter { get; set; }
    }

    public class customerPromoter : result
    {
        public string customer2promotor_gid { get; set; }
        public string promoter_name { get; set; }
        public string designation { get; set; }
        public int promoter_age { get; set; }
        public double mobile { get; set; }
        public string customer_gid { get; set; }
    }

    public class customerGuarantorslist : result
    {
        public List<customerGuarantors> customerGuarantors { get; set; }
    }

    public class customerGuarantors : result
    {
        public string customer2guarantor_gid { get; set; }
        public string guarantors_name { get; set; }
        public int guarantor_age { get; set; }
        public string networth { get; set; }
        public string basisofNW { get; set; }
        public string customer_gid { get; set; }
    }
    public class customerCollaterallist : result
    {
        public List<customerCollateral> customerCollateral { get; set; }
    }
    public class customerCollateral : result
    {
       
        public string collateral_info { get; set; }
    }

}