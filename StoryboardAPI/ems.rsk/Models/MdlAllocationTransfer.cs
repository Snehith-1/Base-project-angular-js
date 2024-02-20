using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.rsk.Models
{

    public class allocationtransferList : result
    {
        public List<allocationtransferdtl> allocationtransferdtl { get; set; }
        public List<zonalapproval> zonalapproval { get; set; }
        public string count_myapproval { get; set; }
        public string count_mypendingApproval { get; set; }
        public string count_myApproved { get; set; }
        public string count_myrejected { get; set; }
        public string count_OverallTransfer { get; set; }
        public string count_mywithinzonalApproval { get; set; }
        public string count_mycrosszonalApproval { get; set; }
    }

    public class allocationtransferdtl : result
    {
        public string allocationdtl_gid { get; set; }
        public string allocation_transfergid { get; set; }
        public string transferapproval_gid { get; set; }
        public string location { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string ZonalRMname { get; set; }
        public string customer_gid { get; set; }
        public string transfer_from { get; set; }
        public string transfer_to { get; set; }
        public string transferto_name { get; set; }
        public string zonal_approvalfrom { get; set; }
        public string zonal_approvalfromname { get; set; }
        public string zonal_approvalto { get; set; }
        public string zonal_approvaltoname { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string transfer_remarks { get; set; }
        public string transfer_zonal { get; set; }
        public string zonalapprovalto_remarks { get; set; }
        public string zonalapprovalfrom_remarks { get; set; }
        public string approvalCountStatus { get; set; }
        public string overallApproval_status { get; set; }
        public string transferto_stategid { get; set; }
        public string transferto_statename { get; set; }
        public string transferto_districtgid { get; set; }
        public string transferto_districtname { get; set; }
        public string transfer_zonalGid { get; set; }
        public string zonaltransferdtl { get; set; }
        public string zonalapprovalto_Flag { get; set; }
        public string zonalapprovalFrom_Flag { get; set; }
        public string transferFrom_zonalGid { get; set; }
        public string transferTo_zonalGid { get; set; }
    }
    public class zonalapproval : result
    {
        public string allocationdtl_gid { get; set; }
        public string allocation_transfergid { get; set; }
        public string transferapproval_gid { get; set; }
        public string location { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string ZonalRMname { get; set; }
        public string customer_gid { get; set; }
        public string transfer_from { get; set; }
        public string transfer_to { get; set; }
        public string transferto_name { get; set; }
        public string zonal_approvalfrom { get; set; }
        public string zonal_approvalfromname { get; set; }
        public string zonal_approvalto { get; set; }
        public string zonal_approvaltoname { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
    }

    public class approvalcount : result
    {
        public string count_myapproval { get; set; }
        public string count_mypendingApproval { get; set; }
        public string count_myApproved { get; set; }
        public string count_myrejected { get; set; }
        public string count_OverallTransfer { get; set; }
    }

    public class transferapproval : result
    {
        public string transferapproval_gid { get; set; }
        public string approval_Remarks { get; set; }
    }


    public class viewtransferdtl : result
    {
        public string zonalapprovalFrom_remarks { get; set; }
        public string zonalapprovalFromTo_remarks { get; set; }
        public string allocationdtl_gid { get; set; }
        public string transferapproval_gid { get; set; }
        public string location { get; set; }
        public string customername { get; set; }
        public string customer_urn { get; set; }
        public string ZonalRMname { get; set; }
        public string customer_gid { get; set; }
        public string transfer_from { get; set; }
        public string transfer_to { get; set; }
        public string transferto_name { get; set; }
        public string zonal_approvalfrom { get; set; }
        public string zonal_approvalfromname { get; set; }
        public string zonal_approvalto { get; set; }
        public string zonal_approvaltoname { get; set; }
        public string approval_status { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string transfer_remarks { get; set; }
        public string transfer_status { get; set; }
        public string overallApproval_status { get; set; }
        public string zonalapprovalfrom_status { get; set; }
        public string zonalapprovalTo_status { get; set; }
        public string zonalapprovalFrom_Date { get; set; }
        public string zonalapprovalTo_Date { get; set; }
        public string allocatedLocation { get; set; }
        public string zonaltransferdtl { get; set; }
    }

    public class allocateRMdtl : result
    {
        public string assigned_RMGid { get; set; }
        public string assigned_RMname { get; set; }
        public string zonal_RMGid { get; set; }
    }
}