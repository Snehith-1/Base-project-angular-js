using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{
     
    public class statedtlList :result
    {
      public List<statedtl> statedtl { get; set; }
    }

    public class statedtl : result
    {
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string state_gid { get; set; }
        public string district_gid { get; set; }
    }

    public class mappingdtlList :result
    {
        public string count_NotAllocate { get; set; }
        public int count_upcoming { get; set; }
        public string count_transfer { get; set; }
        public string count_totalallocate { get; set; }
        public string count_external { get; set; }
        public string count_completed { get; set; }
        public int count_currentallo { get; set; }
        public string count_reportchanges { get; set; }
        public List<mappingdtl> mappingdtl { get; set; }
    }

    public class mappingdtl :result
    {
        public string zonal_gid { get; set; }
        public string zonal_name { get; set; }
        public string RMmapping_gid { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string assigned_RM { get; set; }
        public string assignedRM_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string customer_gid { get; set; }
        public string state_gid { get; set; }
        public string district_gid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string sanction_amount { get; set; }
        public string count_RMassigned { get; set; }
        public string ZonalRMname { get; set; }
        public string ZonalRM_gid { get; set; }
        public string vertical { get; set; }
        public string location { get; set; }
        public string allocation_flag { get; set; }
        public string allocation_status { get; set; }
        public string allocate_external { get; set; }
        public string completed_flag { get; set; }
        public string lastvisit_date { get; set; }
        public string count_lastvisit { get; set; }
        public string disbursement_date { get; set; }
        public string last_disb_date { get; set; }
        public string daypassed_disbursement { get; set; }
        public string completed_date { get; set; }
        public string constitution { get;set;}
        public string firstdisb_date { get; set; }
        public string credit_managername { get; set; }
        public string creditmanager_gid { get; set; }
        public string relationship_managername { get; set; }
        public string relationship_managerGid { get; set; }
        public string transferFrom_statename { get; set; }
        public string transferFrom_districtname { get; set; }
        public string transferfrom_assignedRM { get; set; }
        public string transferFrom_ZonalRMname { get; set; }
        public string totaldisb_amount { get; set; }
        public string PPA_gid { get; set; }
        public string PPA_name { get; set; }
        public string reportcancel_flag { get; set; }
        public string qualified_status { get; set; }
        public string Manual_Allocation { get; set; }
    }

    public class customerdetail:result
    {
        public string customername { get; set; }
        public string zonal_gid { get; set; }
        public string zonal_name { get; set; }
        public string state_name { get; set;}
        public string state_gid { get; set; }
        public string district_gid { get; set; }
        public string district_name { get; set; }
        public string ZonalRM_gid { get; set; }
        public string ZonalRMname { get; set; }
        public string assignedRM_gid { get; set; }
        public string assigned_RM { get; set; }
        public string qualified_status { get; set; }
        public List<customerlastvisit> customerlastvisit { get; set; }
    }
    public class customerlastvisit : result
    {
        public string assignedRM_name { get; set; }
        public string lastvisit_date { get; set; }
    }
    public class allocation : result
    {
        public string customer_gid { get; set; }
        public string allocationdtl_gid { get; set; }
        public string allocation_gid { get; set; }
        public string allocationSubmit { get; set; }
    }

   

}