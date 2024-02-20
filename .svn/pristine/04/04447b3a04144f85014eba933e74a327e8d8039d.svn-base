using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store data to Product & PMG Approval master and corresponding approvals in warehouse masters.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K</remarks>

    public class PmgApprovallist
    {
        public List<PmgApprovaldtl> PmgApprovaldtl { get; set; }
    }

    public class PmgApprovaldtl : result
    { 
        public string mstpmgapproval_gid { get; set; }
        public string pmgapproval_ID { get; set; }
        public string pmgapproval_gid { get; set; }
        public string pmgapproval_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string warehouse_gid { get; set; }
        public string api_code { get; set; }
        public List<pmgwarehouselist> pmgwarehouselist { get; set; }
    }

    public class pmgwarehouselist : result
    {
        public string warehouse_gid { get; set; }
    }
    public class ProductApprovallist
    {
        public List<ProductApprovaldtl> ProductApprovaldtl { get; set; }
    }

    public class ProductApprovaldtl : result
    { 
        public string mstproductapproval_gid { get; set; }
        public string productapproval_ID { get; set; }
        public string productapproval_gid { get; set; }
        public string productapproval_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }  
        public string remarks { get; set; }
        public string warehouse_gid { get; set; }
        public string api_code { get; set; }

        public List<productwarehouselist> productwarehouselist { get; set; }
    }

    public class productwarehouselist : result
    {
        public string warehouse_gid { get; set; }
    }
        public class PendingProductApprovallist
    {

        public string pmgapproval { get; set; }
        public string productapproval { get; set; }
        public List<PendingProductApprovaldtl> PendingProductApprovaldtl { get; set; }
    }

    public class PendingProductApprovaldtl : result
    {
        public string mstproductapproval_gid { get; set; }
        public string warehouse2approval_gid { get; set; }
        public string productapproval_ID { get; set; }
        public string productapproval_gid { get; set; }
        public string productapproval_name { get; set; }
        public string mstpmgapproval_gid { get; set; }
        public string pmgapproval_ID { get; set; }
        public string pmgapproval_gid { get; set; }
        public string pmgapproval_name { get; set; }
        public string warehouse_gid { get; set; }
        public string warehouse_ref_no { get; set; }
        public string warehouse_name { get; set; }
        public string product_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string warehouse_approveddate { get; set; }
        public string approval_status { get; set; }
    }

}