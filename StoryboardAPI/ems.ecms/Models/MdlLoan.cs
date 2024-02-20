using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.ecms.Models
{
    /// <summary>
    /// loan Controller Class containing API methods for accessing the  Model class createLoan
    ///     Loan  - Create loan, sanction loan, loan details, loan details summary, 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class createLoan : result
    {
        //public string entityGid { get; set; }
        //public string branchGid { get; set; }
        public string loanRefNo { get; set; }
        public string sanctionGid { get; set; }
        public string sanctionRefno { get; set; }
        public DateTime sanctiondate { get; set; }
        public string sanctionDate { get; set; }
        public string customerGid { get; set; }
        public string loanmaster_gid { get; set; }
        public string loanTitle { get; set; }
        public string remarks { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanager_gid { get; set; }
        public string customer_name { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmanager_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string rm_name { get; set; }
    }

    public class loan : result
    {
        public List<LoanDetails> loanDetails { get; set; }
    }
    public class LoanDetails : loan
    {
        public string entityName { get; set; }
        public string branchName { get; set; }
        public string vertical_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string loanRefNo { get; set; }
        public string sanctionRefno { get; set; }
        public string sanctionDate { get; set; }
        public string loanmaster_gid { get; set; }
        public string loanTitle { get; set; }
        public string customer_code { get; set; }
        public string customerName { get; set; }
        public string customer_gid { get; set; }
        public string loan_gid { get; set; }
        public string entity_gid { get; set; }
        public string branch_gid { get; set; }
        public string vertical_gid { get; set; }
        public string zonal_gid { get; set; }
        public string businesshead_gid { get; set; }
        public string relationshipmgmt_gid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanager_gid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmgmt_name { get; set; }
        public string rm_name { get; set; }
        public string criticallity { get; set; }
        public string sanction_Gid { get; set; }
        public string sanction_date { get; set; }
    }


    public class MDLcriticallity : loan
    {
        
        public string criticallity { get; set; }
        public string comments { get; set; }
    }

    public class loanmaster : result
    {
        public List<loanmasterdtls> loanmasterdtls { get; set; }
    }
    
    public class loanmasterdtls : loanmaster
    {
        public string loanmaster_gid { get; set; }
        public string loanTitle { get; set; }
    }

    public class mdlheadsofcustomer : result
    {
        public string zonalGid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string creditmanager_gid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmgmt_name { get; set; }
        public string rm_name { get; set; }
        public List<sanctiondtl> sanctiondtl { get; set; }

    }
    public class sanctiondtl :result
    {
        public string sanction_Gid { get; set; }
        public string sanctionrefno { get; set; }
        public string sanctiondate { get; set; }
        public string Sanction_Date { get; set; }
        public string facility_type { get; set; }
        public string facilitytype_gid { get; set; }
    }

    public class mdlloan : result
    {
        public string loan_gid { get; set; }
        public string sanctionrefno { get; set; }
        public string sanctiondate { get; set; }
       }

    public class loanedit : result
    {
        public string loan_gid { get; set; }
        public string branch { get; set; }
        public string entity { get; set; }
        public string customer_gid { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_code { get; set; }
        public string customer_name { get; set; }      
        public string loanRefNoedit { get; set; }
        public string sanctionrefnoedit { get; set; }
        public DateTime  sanctionDateedit { get; set; }
        public string loanmaster_gid { get; set; }
        public string loanTitleedit { get; set; }
        public string zonalGid { get; set; }
        public string businessHeadGid { get; set; }
        public string relationshipMgmtGid { get; set; }
        public string clustermanagerGid { get; set; }
        public string zonal_name { get; set; }
        public string businesshead_name { get; set; }
        public string relationshipmgmt_name { get; set; }
        public string cluster_manager_name { get; set; }
        public string creditmanager_gid { get; set; }
        public string creditmanager_name { get; set; }
    }

    public class loanfaciity_list : result
    {
        public int count_loan {get; set;}
        public List<loanfacility> loanfacility { get; set; }
    }

    public class loanfacility
    {
        public string facility_gid{ get; set; }
        public string facility_type{ get; set; }
    }

   public class NewloanRef:result 
    {
        public string loan_gid { get; set; }
        public string oldloanref_no { get; set; }
        public string newloanref_no { get; set; }
    }
}
