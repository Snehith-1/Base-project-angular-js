using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class customerlist : result
    {
        public List<customernamedtl> customernamedtl { get; set; }
    }

    public class customernamedtl : result
    {
        public string customer_gid { get; set; }
        public string customername { get; set; }
    }

    public class customerpromotorslist : result
    {
        public string PPAname { get; set; }
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
        public string customer_name { get; set; }
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string account_status { get; set; }
        public string loanref_no { get; set; }
        public string loan_title { get; set; }
        public string sanctionref_no { get; set; }
        public string sanction_date { get; set; }
    }

    public class assignedRM : result
    {
        public string zonal_gid { get; set; }
        public string zonal_name { get; set; }
        public string state_name { get; set; }
        public string state_gid { get; set; }
        public string zonal_riskmanager { get; set; }
        public string district_gid { get; set; }
        public string assigned_RM { get; set; }
        public string customer_name { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string ppa_gid { get; set; }
        public List<districtdtl> districtdtl { get; set; }
    }

    public class districtdtl
    {
        public string district_gid { get; set; }
        public string district_name { get; set; }
    }

    public class sanctionloanlist : result
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
        public string totalsanction_amount { get; set; }
        public string ppa_name { get; set; }
        public List<loandtl> loandtl { get; set; }
        public List<collateraldtl> collateraldtl { get; set; }
        public List<Guarantorsdtl> Guarantorsdtl { get; set; }
        public List<Promoterdtl> Promoterdtl { get; set; }
        public List<holdallocation> holdallocation { get; set; }
        public List<U2CMovedallocation> U2CMovedallocation { get; set; }
    }

    public class loandtl : result
    {
        public string loanref_no { get; set; }
        public string loan_title { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string vertical { get; set; }
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string sanction_gid { get; set; }
        public string facility_type { get; set; }
        public string tenure_months { get; set; }
        public string sanction_type { get; set; }
        public string entity { get; set; }
        public string colanding_status { get; set; }
        public string colander_name { get; set; }
    }

    public class collateraldtl : result
    {
        public string security_type { get; set; }
        public string security_description { get; set; }
        public string account_status { get; set; }
    }

    public class Promoterdtl : result
    {
        public string promoter_name { get; set; }
        public string designation { get; set; }
        public string promoter_age { get; set; }
        public string mobile { get; set; }
    }
    public class Guarantorsdtl : result
    {
        public string guarantors_name { get; set; }
        public string guarantor_age { get; set; }
        public string networth { get; set; }
        public string basisofNW { get; set; }
    }

    public class customerRMdtl : result
    {
        public List<customer_list> customer_list { get; set; }
    }

    public class customer_list
    {
        public string customer_gid { get; set; }
        public string customercode { get; set; }
        public string customername { get; set; }
        public string vertical_code { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string zonal_name { get; set; }
        public string zonal_riskmanager { get; set; }
        public string riskmanager_name { get; set; }
        public string customer_urn { get; set; }
        public string ppa_name { get; set; }
    }

    public class sanctionloan : result
    {
        public List<upload_list> upload_list { get; set; }
        public List<sanctionloanList> sanctionloanList { get; set; }
      
    }

    public class sanctionloanList : result
    {
        public string sanction_refno { get; set; }
        public string sanction_gid { get; set; }
        public string sanction_date { get; set; }
        public string sanction_amount { get; set; }
        public string sanction_limit { get; set; }
        public string facility_type { get; set; }
        public string tenure_months { get; set; }
        public string sanction_type { get; set; }
        public string entity { get; set; }
        public string colanding_status { get; set; }
        public string colander_name { get; set; }
    }

    public class loanListdetail : result
    {
        public List<loanList> loanList { get; set; }
    }
    public class loanList : result
    {
        public string loanref_no { get; set; }
        public string sanction_gid { get; set; }
        public string loan_title { get; set; }
        public string loan_date { get; set; }
        public string facility_type { get; set; }
        public string loanfacility_amount { get; set; }
        public string loanfacility_type { get; set; }
        public string document_limit { get; set; }
        public string expiry_date { get; set; }
        public string tenure { get; set; }
        public string loanfacilityref_no { get; set; }
        public string proposed_roi { get; set; }
    }

    public class allocationsanction : result
    {
        public string allocationdtl_gid { get; set; }
        public string sanction_gid { get; set; }
    }
    public class escrowSummaryList:result 
    {
        public List<escrowSummary> escrowSummary { get; set; }
    }
    public class escrowSummary
    {
        public string escrow_gid { get; set; }
        public string sanction_refno { get; set;}
        public string sanction_date { get; set; }
        public string disbursement_date { get; set; }
        public string transactionref_no { get; set; }
        public string transaction_date { get; set; }
        public string amount { get; set; }
        public string created_date { get; set; }
        public string escrow_account_no { get; set; }
    }

    public class escrow:result 
    {
        public string sanction_gid { get; set; }
        public string sanction_refno { get; set; }
        public string sanction_date { get; set; }
        public string facility_type { get; set; }
        public string customer_gid { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string customer_urn { get; set; }
        public string disbursement_date { get; set; }
        public string transaction_date { get; set; }
        public string transactionref_no { get; set; }
        public string escrow_account_no { get; set; }
        public string dealer_name { get; set; }
        public string master_account_no { get; set; }
        public string amount { get; set; }
        public string beneficiary_customer_account_name { get; set; }
        public string sender_customer_account_name { get; set; }
        public string sender_customer_account_no { get; set; }
        public string remittance_info { get; set; }
        public string sender_branch_IFSC { get; set; }
        public string reference { get; set; }
        public string credit_time { get; set; }
        public string remarks { get; set; }
    }
}