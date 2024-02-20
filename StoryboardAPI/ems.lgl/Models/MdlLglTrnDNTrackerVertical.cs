using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.lgl.Models
{
    public class MdlDNpendingList : result
    {
        public List<DNpending_list> DNpending_list { get; set; }

        public string validity { get; set; }
        public string skip_reason { get; set; }
    }
    public class MdlDNGeneratedList : result
    {
        public List<DNgenerated_list> DNgenerated_list { get; set; }

    }
    public class MdlDNSkippedList : result
    {
        public List<DNskipped_list> DNskipped_list { get; set; }

    }
    public class MdlDNexclusionList : result
    {
        public List<DNexclusion_list> DNexclusion_list { get; set; }

    }
    public class MdlDNlegalsrList : result
    {
        public List<DNlegalsr_list> DNlegalsr_list { get; set; }

    }
    public class DNlegalsr_list
    {
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
        public string raised_by { get; set; }
        public string auth_status { get; set; }
        public string approval_status { get; set; }
        public string urn { get; set; }
    }
    public class DNexclusion_list : result
    {
        public string od_days { get; set; }
        public string urn { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string GurantorCustomerId { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
        public string excluded_by { get; set; }
        public string excluded_date { get; set; }
    }
    public class DNgenerated_list : result
    {
        public string od_days { get; set; }
        public string urn { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string GurantorCustomerId { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
        public string customer_gid { get; set; }
    }
    public class DNskipped_list
    {
        public string od_days { get; set; }
        public string urn { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string Vertical { get; set; }
        public string Skipped_on { get; set; }
        public string validity { get; set; }
        public string GurantorCustomerId { get; set; }
    }
    public class DNpending_list : result
    {
        public string od_days { get; set; }
        public string urn { get; set; }
        public string Customer_name { get; set; }
        public string Guarantor_Name { get; set; }
        public string Vertical { get; set; }
        public string DNstatus { get; set; }
        public string acknowledgement_status { get; set; }
        public string GurantorCustomerId { get; set; }
        public string customer_gid { get; set; }
    }
    public class customereditlist : result
    {
        public string customerCodeedit { get; set; }
        public string customerNameedit { get; set; }
        public string customer_urnedit { get; set; }
        public string customer_gid { get; set; }
        public string zonal_riskmanagerName { get; set; }
        public string riskMonitoring_GID { get; set; }
        public string riskMonitoring_Name { get; set; }
       
    }
    public class overallhistoryallocationlist : result
    {
        public List<overallhistoryallocationdtl> overallhistoryallocationdtl { get; set; }
    }
    public class overallhistoryallocationdtl
    {
        public string allocationdtl_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string customer_gid { get; set; }
        public string location { get; set; }
        public string assigned_RM { get; set; }
        public string ZonalRMname { get; set; }
        public string allocation_flag { get; set; }
        public string allocation_status { get; set; }
        public string lastvisit_date { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string completed_date { get; set; }
        public string reportcancel_flag { get; set; }
        public string allocate_external { get; set; }
        public string observation_reportgid { get; set; }
        public string observation_flag { get; set; }
        public string tier1format_gid { get; set; }
    }
    public class count_dtl    {
        public string lblpending_count { get; set; }
        public string lblgenerated_count { get; set; }
        public string lblskipped_count { get; set; }
        public string lblexclusion_count { get; set; }
        public string lbllegalsr_count { get; set; }
    }
}