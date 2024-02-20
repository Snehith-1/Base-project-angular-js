using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will provide access to product desk flow by posting and getting necessary data for the applications
    /// </summary>
    /// <remarks>Written by Sherin Augusta.A & Logapriya.S </remarks>

    public static class getProductAppStatus
    {
        public const string
              NoProductDesk = "N",
              InitiatedProductDesk = "Y",
              PendingApproval = "P",
              Completed = "C",
              Rejected = "R",
              Holded = "H";
    }

    public class MdlProductGroup : result
    {
        public string productdesk_gid { get; set; }
        public string productdesk_name { get; set; }
        public List<ProductMemberGroup> ProductMemberGroup { get; set; }
        public List<ProductManagerGroup> ProductManagerGroup { get; set; }
    }
    public class ProductMemberGroup
    {
        public string productdeskmember_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class ProductManagerGroup
    {
        public string productdeskmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class Mdlproductdeskassign : result
    {
        public string appproductapproval_gid { get; set; }
        public string application_gid { get; set; }
        public string productdesk_gid { get; set; }
        public string productdesk_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string product_managergid { get; set; }
        public string product_managername { get; set; }
        public string productmanager_approvalflag { get; set; }
        public string product_membergid { get; set; }
        public string product_membername { get; set; }
        public string productmember_approvalflag { get; set; }
        public string assign_remarks { get; set; }
    }

    public class MdlProductreassignedlogInfo : result
    {
        public List<Productreassignedloglist> Productreassignedloglist { get; set; }
    }
    public class Productreassignedloglist
    {
        public string application_gid { get; set; }
        public string product_managergid { get; set; }
        public string product_managername { get; set; }
        public string reassignto_product_managergid { get; set; }
        public string reassignto_product_managername { get; set; }

        public string product_membergid { get; set; }
        public string product_membername { get; set; }
        public string reassignto_product_membergid { get; set; }
        public string reassignto_product_membername { get; set; }

        public string remarks { get; set; }
        public string reassign_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class MdlMstProductApproval : result
    {
        public List<applicationProduct_list> applicationProduct_list { get; set; }
    }
    public class applicationProduct_list
    {
        public string application_gid { get; set; }
        public string application_no { get; set; }
        public string customer_urn { get; set; }
        public string customer_name { get; set; }
        public string vertical_name { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string application_status { get; set; }
        public string applicant_type { get; set; }
        public string updated_date { get; set; }
        public string approval_status { get; set; }
        public string productdesk_gid { get; set; }
        public string region { get; set; }
        public string createdby { get; set; }
        public string appproductapproval_gid { get; set; }
        public string productquery_status { get; set; }
        public string underwriting_flag { get; set; }
        public string productdesk_name { get; set; }
        public string productmember_approvaldate { get; set; }
        public string product_membername { get; set; }
        public string productmanager_approvaldate { get; set; }
        public string product_managername { get; set; }
         public string onboarding_status { get; set; }
        public string renewal_flag { get; set; }
        public string amendment_flag { get; set; }
        public string shortclosing_flag { get; set; }

    }

    public class MdlProductApplicationCount : result
    {
        public string newapplication_count { get; set; }
        public string rejectholdapplication_count { get; set; }
        public Int16 lstotalcount { get; set; }
        public string approvedapplication_count { get; set; }
        public string Advanceapplication_count { get; set; }
    }
    public class mdlproductquery : result
    {
        public string appproductquery_gid { get; set; }
        public string application_gid { get; set; }
        public string querytitle { get; set; }
        public string querydesc { get; set; }
        public string queryraised_to { get; set; }
        public string appproductapproval_gid { get; set; }
        public string product_membergid { get; set; }
    }
    public class applproductapproval : result
    {
        public List<appproductquerylist> appproductquerylist { get; set; }
        public List<appmanagerquerylist> appmanagerquerylist { get; set; }
        public string product_membergid { get; set; }
        public string product_managergid { get; set; }
    }
    public class appproductquerylist
    {
        public string appproductquery_gid { get; set; }
        public string appproductapproval_gid { get; set; }
        public string application_gid { get; set; }
        public string querytitle { get; set; }
        public string querydesc { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string querystatus { get; set; }
        public string hierary_level { get; set; }
        public string close_remarks { get; set; }
        public string query_to { get; set; }
        public string rmquery_flag { get; set; }
    }
    public class appmanagerquerylist
    {
        public string appproductquery_gid { get; set; }
        public string appproductapproval_gid { get; set; }
        public string application_gid { get; set; }
        public string querytitle { get; set; }
        public string querydesc { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string querystatus { get; set; }
        public string hierary_level { get; set; }
        public string close_remarks { get; set; }
        public string query_to { get; set; }
        public string rmquery_flag { get; set; }
    }

    public class MdlProductApprovaldtl :result
    {
        public string appproductapproval_gid { get; set; }
        public string approval_status { get; set; }
        public string approval_remarks { get; set; }
        public string application_gid { get; set; }
        public string member_name { get; set; }
        public string memberapproval_date { get; set; }
        public string memberapproval_flag { get; set; }

        public string manager_approvaldate { get; set; }

        public string manager_approvalflag { get; set; }

        public string manager_approvalremarks { get; set; }

        public string manager_name { get; set; }    
    } 
}