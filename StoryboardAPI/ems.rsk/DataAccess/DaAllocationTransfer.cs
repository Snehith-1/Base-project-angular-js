using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;
using ems.storage.Functions;

namespace ems.rsk.DataAccess
{
    public class DaAllocationTransfer
    {
        dbconn objdbconn = new dbconn();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGet_appGid, msGet_documentGid;
        string lstransferred_from, lstransfrom_stategid, lstransfrom_districtgid;
        string lsstate_name, lsdistrict_name;
        int mnResult;
        string lspath;
        string msGetDocumentGid, lsallocation_flag;
        string lszonalapprovalto_status, lstransfer_zonal;
        string lscustomer_gid, lstransfrom_statename, lstransfrom_districtname;
        string lsassignedRM_name, lszonalRM_name;
        string lstransfer_to, lstransferTo_name, lstransferto_zonalRMname;
        string lstransferto_zonalRM, lstransferTo_stategid, lstransferTo_statename;
        string lstransferTo_districtgid, lstransferTo_districtname, lstransferTo_zonalgid;


        public bool DaGetAllocation(mappingdtlList values)
        {
            msSQL = " select a.allocationdtl_gid,a.customer_gid,b.customername,b.vertical_code,b.customer_urn,a.state_name, a.completed_flag," +
                    " a.district_name,assignedRM_name,zonalRM_name,a.allocation_status, " +
                    " concat(h.user_firstname,' ',h.user_lastname,' / ',h.user_code) as allocated_by, " +
                    " date_format(a.created_date,'%d-%m-%Y') as allocated_date" +
                    " from rsk_trn_tallocationdtl a " +
                    " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = a.allocated_by" +
                    " left join adm_mst_tuser h on h.user_gid=g.user_gid " +
                    " where (a.allocation_flag='Y') and a.allocationdtl_gid not in " +
                    " (select allocationdtl_gid from rsk_trn_tallocationtransfer where transferapproval_status<>'Approved') " +
                    " order by allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<mappingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new mappingdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical_code"].ToString(),
                        state_name = dt["state_name"].ToString(),
                        district_name = dt["district_name"].ToString(),
                        assigned_RM = dt["assignedRM_name"].ToString(),
                        created_by = dt["allocated_by"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        ZonalRMname = dt["zonalRM_name"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                    });
                }
                values.mappingdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostAllocationTransfer(string employee_gid, allocationtransferdtl values)
        {

            msSQL = " SELECT zonalRM_name,customer_gid,allocation_assignedRM,state_gid,assignedRM_name,state_name, " +
                    " district_name, district_gid from rsk_trn_tallocationdtl " +
                   " where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lstransferred_from = objODBCDatareader["allocation_assignedRM"].ToString();
                lstransfrom_stategid = objODBCDatareader["state_gid"].ToString();
                lstransfrom_statename = objODBCDatareader["state_name"].ToString();
                lstransfrom_districtgid = objODBCDatareader["district_gid"].ToString();
                lstransfrom_districtname = objODBCDatareader["district_name"].ToString();
                lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
                lsassignedRM_name = objODBCDatareader["assignedRM_name"].ToString();
                lszonalRM_name = objODBCDatareader["zonalRM_name"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("RKAT");
            msSQL = " insert into rsk_trn_tallocationtransfer(" +
                   " allocation_transfergid," +
                   " allocationdtl_gid," +
                   " customer_gid, " +
                   " transfer_remarks," +
                   " transfer_zonal," +
                   " transferFrom_zonalgid," +
                   " transferTo_zonalgid," +
                   " transfer_from," +
                   " transferFrom_name," +
                   " transferFrom_stategid, " +
                   " transferFrom_statename ," +
                   " transferFrom_districtgid, " +
                   " transferFrom_districtname," +
                   " transfer_to," +
                   " transferTo_name," +
                   " transferTo_stategid," +
                   " transferTo_statename," +
                   " transferTo_districtgid," +
                   " transferTo_districtname," +
                   " transferfrom_zonalRM," +
                   " transferfrom_zonalRMname," +
                   " transferto_zonalRM," +
                   " transferto_zonalRMname," +
                   " transferapproval_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.allocationdtl_gid + "'," +
                   "'" + lscustomer_gid + "'," +
                   "'" + values.transfer_remarks.Replace("'", "\\'") + "'," +
                   "'" + values.transfer_zonal + "'," +
                   "'" + values.transferFrom_zonalGid + "'," +
                   "'" + values.transferTo_zonalGid + "'," +
                   "'" + lstransferred_from + "'," +
                   "'" + lsassignedRM_name + "'," +
                   "'" + lstransfrom_stategid + "'," +
                   "'" + lstransfrom_statename + "'," +
                   "'" + lstransfrom_districtgid + "'," +
                   "'" + lstransfrom_districtname + "'," +
                   "'" + values.transfer_to + "'," +
                   "'" + values.transferto_name + "'," +
                   "'" + values.transferto_stategid + "'," +
                   "'" + values.transferto_statename + "'," +
                   "'" + values.transferto_districtgid + "'," +
                   "'" + values.transferto_districtname + "'," +
                   "'" + values.zonal_approvalfrom + "'," +
                   "'" + values.zonal_approvalfromname + "'," +
                   "'" + values.zonal_approvalto + "'," +
                   "'" + values.zonal_approvaltoname + "'," +
                   "'Pending'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msGet_appGid = objcmnfunctions.GetMasterGID("ATAL").ToString();

            msSQL = " Insert into rsk_trn_ttransferapproval( " +
                    " transferapproval_gid," +
                    " allocation_transfergid," +
                    " allocationdtl_gid," +
                    " customer_gid," +
                    " transfer_zonal," +
                    " zonal_approvalfrom, " +
                    " zonalapprovalfrom_name," +
                    " zonalapprovalfrom_status, " +
                    " zonal_approvalto, " +
                    " zonalapprovalto_name," +
                    " zonalapprovalto_status, " +
                    " transferapproval_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGet_appGid + "', " +
                    "'" + msGetGid + "'," +
                    "'" + values.allocationdtl_gid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + values.transfer_zonal + "'," +
                    "'" + values.zonal_approvalfrom + "'," +
                    "'" + values.zonal_approvalfromname + "'," +
                    "'Pending'," +
                    "'" + values.zonal_approvalto + "'," +
                    "'" + values.zonal_approvaltoname + "',";
            if (values.transfer_zonal == "WithinZone")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'Pending',";
            }
            msSQL += "'Pending'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update rsk_trn_tallocationdtl set allocatetransfer_flag='Y' where allocationdtl_gid='" + values.allocationdtl_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "RM Transfer Initiated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetTransferSummary(allocationtransferList values)
        {

            msSQL = " select b.transferapproval_gid,a.allocation_transfergid,a.customer_gid,c.customer_urn,c.customername,c.vertical_code,b.allocationdtl_gid, " +
                    " concat(k.zonal_name,' / ',a.transferFrom_statename, ' / ', a.transferFrom_districtname) as allocatedLocation, " +
                    " a.transferFrom_name,a.transferfrom_zonalRMname,transferTo_name, " +
                    " b.zonalapprovalfrom_name,b.zonalapprovalto_name,a.transferapproval_status,b.transfer_zonal, " +
                    " concat(l.zonal_name,' / ',a.transferTo_statename,' / ',a.transferTo_districtname) as zonaltransferdtl, " +
                    " case when b.zonal_approvalto<>'' " +
                    " then b.zonalapprovalto_name else b.zonalapprovalfrom_name end as transferzonalRM," +
                    " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as InitiatedBy, " +
                    " date_format(a.created_date, '%d-%m-%Y') as initiatedDate " +
                    " from rsk_trn_tallocationtransfer a " +
                    " left join rsk_trn_ttransferapproval b on a.allocation_transfergid = b.allocation_transfergid " +
                    " left join ocs_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                    " left join hrm_mst_temployee i on i.employee_gid = a.created_by " +
                    " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                    " left join rsk_mst_tzonalmapping k on a.transferFrom_zonalgid = k.zonalmapping_gid " +
                    " left join rsk_mst_tzonalmapping l on a.transferTo_zonalgid = l.zonalmapping_gid " +
                    " order by a.allocation_transfergid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_allocationtransferdtl = new List<allocationtransferdtl>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_allocationtransferdtl.Add(new allocationtransferdtl
                    {
                        allocation_transfergid = dt["allocation_transfergid"].ToString(),
                        transferapproval_gid = dt["transferapproval_gid"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        ZonalRMname = dt["transferfrom_zonalRMname"].ToString(),
                        transfer_from = dt["transferFrom_name"].ToString(),
                        transferto_name = dt["transferTo_name"].ToString(),
                        zonal_approvalfromname = dt["zonalapprovalfrom_name"].ToString(),
                        zonal_approvaltoname = dt["zonalapprovalto_name"].ToString(),
                        approval_status = dt["transferapproval_status"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customername = dt["customername"].ToString(),
                        created_by = dt["InitiatedBy"].ToString(),
                        created_date = dt["initiatedDate"].ToString(),
                        transfer_zonal = dt["transfer_zonal"].ToString(),
                        zonaltransferdtl = dt["zonaltransferdtl"].ToString(),
                        zonal_approvalto = dt["transferzonalRM"].ToString(),
                    });
                }
                values.allocationtransferdtl = get_allocationtransferdtl;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_OverallTransfer,b.count_pendingApproval,c.count_Approved,d.count_rejected from " +
                    " (select count(*) as count_OverallTransfer from rsk_trn_tallocationtransfer) as a, " +
                    " (select count(*) as count_pendingApproval from rsk_trn_tallocationtransfer where transferapproval_status = 'Pending' )as b, " +
                    " (select count(*) as count_Approved from rsk_trn_tallocationtransfer where transferapproval_status = 'Approved' )as c, " +
                    " (select count(*) as count_rejected from rsk_trn_tallocationtransfer where transferapproval_status = 'Rejected' )as d";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_OverallTransfer = objODBCDatareader["count_OverallTransfer"].ToString();
                values.count_mypendingApproval = objODBCDatareader["count_pendingApproval"].ToString();
                values.count_myApproved = objODBCDatareader["count_Approved"].ToString();
                values.count_myrejected = objODBCDatareader["count_rejected"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }


        public bool DaGetTransferApprovalSummary(string employee_gid, allocationtransferList values)
        {

            msSQL = " select a.allocation_transfergid,a.transferapproval_gid,b.customer_gid,c.customer_urn,c.customername,c.vertical_code,b.allocationdtl_gid, " +
                   " concat(b.transferFrom_statename, ' / ', b.transferFrom_districtname) as allocatedLocation, " +
                   " b.transferFrom_name,b.transferfrom_zonalRMname,b.transferTo_name, " +
                   " zonalapprovalfrom_name,zonalapprovalto_name,a.transferapproval_status, " +
                   " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as InitiatedBy, " +
                   " date_format(a.created_date, '%d-%m-%Y') as initiatedDate " +
                   " from rsk_trn_ttransferapproval a " +
                   " left join rsk_trn_tallocationtransfer b on a.allocation_transfergid = b.allocation_transfergid " +
                   " left join ocs_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                   " left join hrm_mst_temployee i on i.employee_gid = a.created_by " +
                   " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                   " where (a.transfer_zonal='WithinZone') " +
                   " and (a.zonal_approvalfrom='" + employee_gid + "') and (a.zonalapprovalfrom_status='Pending')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_zonalapproval = new List<zonalapproval>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_zonalapproval.Add(new zonalapproval
                    {
                        transferapproval_gid = dt["transferapproval_gid"].ToString(),
                        allocation_transfergid = dt["allocation_transfergid"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        ZonalRMname = dt["transferfrom_zonalRMname"].ToString(),
                        transfer_from = dt["transferFrom_name"].ToString(),
                        transferto_name = dt["transferTo_name"].ToString(),
                        zonal_approvalfromname = dt["zonalapprovalfrom_name"].ToString(),
                        zonal_approvaltoname = dt["zonalapprovalto_name"].ToString(),
                        approval_status = dt["transferapproval_status"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customername = dt["customername"].ToString(),
                        created_by = dt["InitiatedBy"].ToString(),
                        created_date = dt["initiatedDate"].ToString(),
                    });
                }
                values.zonalapproval = get_zonalapproval;
            }
            dt_datatable.Dispose();

            msSQL = " select a.allocation_transfergid,a.transferapproval_gid,b.customer_gid,c.customer_urn,c.customername,c.vertical_code,b.allocationdtl_gid, " +
                   " concat(b.transferFrom_statename, ' / ', b.transferFrom_districtname) as allocatedLocation, " +
                   " b.transferFrom_name,b.transferfrom_zonalRMname,b.transferTo_name, " +
                   " zonalapprovalfrom_name,zonalapprovalto_name,a.zonalapprovalto_status as transferapproval_status, " +
                   " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as InitiatedBy, " +
                   " date_format(a.created_date, '%d-%m-%Y') as initiatedDate " +
                   " from rsk_trn_ttransferapproval a " +
                   " left join rsk_trn_tallocationtransfer b on a.allocation_transfergid = b.allocation_transfergid " +
                   " left join ocs_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                   " left join hrm_mst_temployee i on i.employee_gid = a.created_by " +
                   " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                  " where a.transfer_zonal='CrossZone' and (a.zonal_approvalto='" + employee_gid + "') and (a.zonalapprovalto_status='Pending')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_allocationtransferdtl = new List<allocationtransferdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_allocationtransferdtl.Add(new allocationtransferdtl
                    {
                        transferapproval_gid = dt["transferapproval_gid"].ToString(),
                        allocation_transfergid = dt["allocation_transfergid"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        ZonalRMname = dt["transferfrom_zonalRMname"].ToString(),
                        transfer_from = dt["transferFrom_name"].ToString(),
                        transferto_name = dt["transferTo_name"].ToString(),
                        zonal_approvalfromname = dt["zonalapprovalfrom_name"].ToString(),
                        zonal_approvaltoname = dt["zonalapprovalto_name"].ToString(),
                        approval_status = dt["transferapproval_status"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customername = dt["customername"].ToString(),
                        created_by = dt["InitiatedBy"].ToString(),
                        created_date = dt["initiatedDate"].ToString(),
                        zonalapprovalto_Flag = "Y",
                    });
                }
                values.allocationtransferdtl = get_allocationtransferdtl;
            }
            dt_datatable.Dispose();

            msSQL = " select a.allocation_transfergid,a.transferapproval_gid,b.customer_gid,c.customer_urn,c.customername,c.vertical_code,b.allocationdtl_gid, " +
                    " concat(b.transferFrom_statename, ' / ', b.transferFrom_districtname) as allocatedLocation, " +
                    " b.transferFrom_name,b.transferfrom_zonalRMname,b.transferTo_name, " +
                    " zonalapprovalfrom_name,zonalapprovalto_name,a.zonalapprovalfrom_status as transferapproval_status, " +
                    " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as InitiatedBy, " +
                    " date_format(a.created_date, '%d-%m-%Y') as initiatedDate " +
                    " from rsk_trn_ttransferapproval a " +
                    " left join rsk_trn_tallocationtransfer b on a.allocation_transfergid = b.allocation_transfergid " +
                    " left join ocs_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                    " left join hrm_mst_temployee i on i.employee_gid = a.created_by " +
                    " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                    " where a.transfer_zonal='CrossZone' and (a.zonal_approvalfrom='" + employee_gid + "') and (a.zonalapprovalfrom_status='Pending')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_allocationtransferdtl.Add(new allocationtransferdtl
                    {
                        transferapproval_gid = dt["transferapproval_gid"].ToString(),
                        allocation_transfergid = dt["allocation_transfergid"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        ZonalRMname = dt["transferfrom_zonalRMname"].ToString(),
                        transfer_from = dt["transferFrom_name"].ToString(),
                        transferto_name = dt["transferTo_name"].ToString(),
                        zonal_approvalfromname = dt["zonalapprovalfrom_name"].ToString(),
                        zonal_approvaltoname = dt["zonalapprovalto_name"].ToString(),
                        approval_status = dt["transferapproval_status"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customername = dt["customername"].ToString(),
                        created_by = dt["InitiatedBy"].ToString(),
                        created_date = dt["initiatedDate"].ToString(),
                        zonalapprovalto_Flag = "N",
                    });
                }
                values.allocationtransferdtl = get_allocationtransferdtl;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_myapproval,b.count_mypendingApproval,c.count_myApproved,d.count_myrejected, " +
                " e.count_myzonalFromApproval,f.count_myzonalToApproval from " +
                " (select count(*) as count_myapproval from rsk_trn_ttransferapproval " +
                " where zonal_approvalto = '" + employee_gid + "' or zonal_approvalfrom = '" + employee_gid + "' ) as a, " +
                " (select count(*) as count_mypendingApproval from rsk_trn_ttransferapproval " +
                " where (zonal_approvalto = '" + employee_gid + "' and zonalapprovalto_status = 'Pending') " +
                " or ( zonal_approvalfrom = '" + employee_gid + "' and zonalapprovalfrom_status = 'Pending' ))as b, " +
                " (select count(*) as count_myApproved from rsk_trn_ttransferapproval " +
                " where (zonal_approvalto = '" + employee_gid + "' and  zonalapprovalto_status = 'Approved') " +
                " or ( zonal_approvalfrom = '" + employee_gid + "' and zonalapprovalfrom_status = 'Approved') )as c, " +
                " (select count(*) as count_myrejected from rsk_trn_ttransferapproval " +
                " where (zonal_approvalto = '" + employee_gid + "' and zonalapprovalto_status = 'Rejected') " +
                " or ( zonal_approvalfrom = '" + employee_gid + "' and zonalapprovalfrom_status = 'Rejected'))as d, " +
                " (select count(*) as count_myzonalFromApproval from rsk_trn_ttransferapproval " +
                " where (zonal_approvalfrom = '" + employee_gid + "' and zonalapprovalfrom_status = 'Pending' and transfer_zonal='WithinZone'))as e, " +
                " (select count(*) as count_myzonalToApproval from rsk_trn_ttransferapproval " +
                " where ((zonal_approvalfrom = '" + employee_gid + "' and zonalapprovalfrom_status = 'Pending'  and transfer_zonal='CrossZone')  " +
                " or ( zonal_approvalto = '" + employee_gid + "' and zonalapprovalto_status='Pending' and transfer_zonal='CrossZone')))as f ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_myapproval = objODBCDatareader["count_myapproval"].ToString();
                values.count_mypendingApproval = objODBCDatareader["count_mypendingApproval"].ToString();
                values.count_myApproved = objODBCDatareader["count_myApproved"].ToString();
                values.count_myrejected = objODBCDatareader["count_myrejected"].ToString();
                values.count_mywithinzonalApproval = objODBCDatareader["count_myzonalFromApproval"].ToString();
                values.count_mycrosszonalApproval = objODBCDatareader["count_myzonalToApproval"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }


        public bool DaPostTransferFromApprove(string employee_gid, transferapproval values)
        {

            msSQL = " update rsk_trn_ttransferapproval " +
                    " set zonalapprovalfrom_status ='Approved', " +
                    " zonalapprovalfrom_remarks='" + values.approval_Remarks.Replace("'", "\\'") + "'," +
                    " zonalapprovalfrom_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                    " where transferapproval_gid='" + values.transferapproval_gid + "' and  zonal_approvalfrom ='" + employee_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_transfergid from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
            string lsallocation_transfergid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " update rsk_trn_tallocationtransfer set transferapproval_status ='Rejected' " +
            //        " where allocation_transfergid='" + lsallocation_transfergid + "'";
            //mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select zonalapprovalto_status,transfer_zonal from rsk_trn_ttransferapproval  " +
                    " where transferapproval_gid='" + values.transferapproval_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lszonalapprovalto_status = objODBCDatareader["zonalapprovalto_status"].ToString();
                lstransfer_zonal = objODBCDatareader["transfer_zonal"].ToString();
            }
            objODBCDatareader.Close();


            if (lszonalapprovalto_status == "Approved" || lstransfer_zonal == "WithinZone")
            {
                msSQL = " update rsk_trn_ttransferapproval set transferapproval_status='Approved' " +
                        " where transferapproval_gid='" + values.transferapproval_gid + "'";
                mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select allocationdtl_gid from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
                string lsallocationdtl_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select allocation_gid from rsk_trn_tallocationdtl where allocationdtl_gid = '" + lsallocationdtl_gid + "'";
                string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select transfer_to,transferTo_zonalgid,transferto_zonalRM,transferTo_stategid,transferTo_statename, " +
                        " transferTo_districtgid,transferTo_districtname,transferTo_name,transferto_zonalRMname,transferto_zonalRM " +
                        " from rsk_trn_tallocationtransfer a " +
                        " left join rsk_trn_ttransferapproval b on a.allocation_transfergid = b.allocation_transfergid " +
                        " where b.transferapproval_gid = '" + values.transferapproval_gid+"'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lstransfer_to = objODBCDatareader["transfer_to"].ToString();
                    lstransferTo_name = objODBCDatareader["transferTo_name"].ToString();
                    lstransferTo_stategid = objODBCDatareader["transferTo_stategid"].ToString();
                    lstransferTo_statename = objODBCDatareader["transferTo_statename"].ToString();
                    lstransferTo_districtgid = objODBCDatareader["transferTo_districtgid"].ToString();
                    lstransferTo_districtname = objODBCDatareader["transferTo_districtname"].ToString();
                    lstransferto_zonalRMname = objODBCDatareader["transferto_zonalRMname"].ToString();
                    lstransferto_zonalRM = objODBCDatareader["transferto_zonalRM"].ToString();
                    lstransferTo_zonalgid = objODBCDatareader["transferTo_zonalgid"].ToString();
                }
                objODBCDatareader.Close();

                if (lstransfer_zonal == "WithinZone")
                {
                    msSQL = " update rsk_trn_tallocationdtl " +
                            " set allocation_assignedRM='" + lstransfer_to + "'," +
                            " assignedRM_name='" + lstransferTo_name + "'," +
                            " state_gid='" + lstransferTo_stategid + "'," +
                            " state_name='" + lstransferTo_statename + "'," +
                            " district_gid='" + lstransferTo_districtgid + "'," +
                            " district_name='" + lstransferTo_districtname + "'," +
                            " allocation_status='Allocated'" +
                            " where allocationdtl_gid='" + lsallocationdtl_gid + "'";
                    mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tallocation a " +
                           " set assigned_RM='" + lstransfer_to + "'," +
                           " state_gid='" + lstransferTo_stategid + "'," +
                           " district_gid='" + lstransferTo_districtgid + "'" +
                           " where allocation_gid='" + lsallocation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                    msSQL = " update rsk_trn_tallocationdtl " +
                      " set allocation_assignedRM='" + lstransfer_to + "'," +
                      " assignedRM_name='" + lstransferTo_name + "'," +
                      " zonalRM_name='" + lstransferto_zonalRMname + "'," +
                      " allocation_zonalRM='" + lstransferto_zonalRM + "'," +
                      " zonal_gid='" + lstransferTo_zonalgid + "'," +
                      " state_gid='" + lstransferTo_stategid + "'," +
                      " state_name='" + lstransferTo_statename + "'," +
                      " district_gid='" + lstransferTo_districtgid + "'," +
                      " district_name='" + lstransferTo_districtname + "'," +
                      " allocation_status='Allocated'" +
                      " where allocationdtl_gid='" + lsallocationdtl_gid + "'";
                    mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tallocation " +
                           " set assigned_RM='" + lstransfer_to + "'," +
                           " zonal_gid='" + lstransferTo_zonalgid + "',"+
                           " state_gid='" + lstransferTo_stategid + "'," +
                           " district_gid='" + lstransferTo_districtgid + "'" +
                           " where allocation_gid='" + lsallocation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " update rsk_trn_tallocationtransfer set transferapproval_status ='Approved' " +
                        " where allocation_transfergid='" + lsallocation_transfergid + "'";
                mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update rsk_trn_tallocationdtl set allocatetransfer_flag='N' where allocationdtl_gid='" + lsallocationdtl_gid + "'";
                mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Transfer Approved Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }


        public bool DaPostTransferToApprove(string employee_gid, transferapproval values)
        {

            msSQL = " update rsk_trn_ttransferapproval " +
                    " set zonalapprovalto_status ='Approved', " +
                    " zonalapprovalto_remarks='" + values.approval_Remarks.Replace("'", "\\'") + "'," +
                    " zonalapprovalto_approveddate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                    " where transferapproval_gid='" + values.transferapproval_gid + "' and  zonal_approvalto ='" + employee_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_transfergid from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
            string lsallocation_transfergid = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = " update rsk_trn_tallocationtransfer set transferapproval_status ='1 Approval Pending..' " +
            //        " where allocation_transfergid='" + lsallocation_transfergid + "'";
            //mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select zonalapprovalfrom_status from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
            string lszonalapprovalfrom_status = objdbconn.GetExecuteScalar(msSQL);

            if (lszonalapprovalfrom_status == "Approved")
            {
                msSQL = " update rsk_trn_ttransferapproval set transferapproval_status='Approved' " +
                       " where transferapproval_gid='" + values.transferapproval_gid + "'";
                mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select allocationdtl_gid from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
                string lsallocationdtl_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select allocation_gid from rsk_trn_tallocationdtl where allocationdtl_gid = '" + lsallocationdtl_gid + "'";
                string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select transfer_to,transferTo_zonalgid,transferto_zonalRM,transferTo_stategid,transferTo_statename, " +
                          " transferTo_districtgid,transferTo_districtname,transferTo_name,transferto_zonalRMname,transferto_zonalRM " +
                          " from rsk_trn_tallocationtransfer a " +
                          " left join rsk_trn_ttransferapproval b on a.allocation_transfergid = b.allocation_transfergid " +
                          " where b.transferapproval_gid = '" + values.transferapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lstransfer_to = objODBCDatareader["transfer_to"].ToString();
                    lstransferTo_name = objODBCDatareader["transferTo_name"].ToString();
                    lstransferTo_stategid = objODBCDatareader["transferTo_stategid"].ToString();
                    lstransferTo_statename = objODBCDatareader["transferTo_statename"].ToString();
                    lstransferTo_districtgid = objODBCDatareader["transferTo_districtgid"].ToString();
                    lstransferTo_districtname = objODBCDatareader["transferTo_districtname"].ToString();
                    lstransferto_zonalRM = objODBCDatareader["transferto_zonalRM"].ToString();
                    lstransferto_zonalRMname = objODBCDatareader["transferto_zonalRMname"].ToString();
                    lstransferTo_zonalgid = objODBCDatareader["transferTo_zonalgid"].ToString();
                }
                objODBCDatareader.Close();

                if (lstransfer_zonal == "WithinZone")
                {
                    msSQL = " update rsk_trn_tallocationdtl " +
                            " set allocation_assignedRM='" + lstransfer_to + "'," +
                            " assignedRM_name='" + lstransferTo_name + "'," +
                            " state_gid='" + lstransferTo_stategid + "'," +
                            " state_name='" + lstransferTo_statename + "'," +
                            " district_gid='" + lstransferTo_districtgid + "'," +
                            " district_name='" + lstransferTo_districtname + "'," +
                            " allocation_status='Allocated'" +
                            " where allocationdtl_gid='" + lsallocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tallocation a " +
                           " set assigned_RM='" + lstransfer_to + "'," +
                           " state_gid='" + lstransferTo_stategid + "'," +
                           " district_gid='" + lstransferTo_districtgid + "'" +
                           " where allocation_gid='" + lsallocation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update rsk_trn_tallocationdtl " +
                     " set allocation_assignedRM='" + lstransfer_to + "'," +
                     " assignedRM_name='" + lstransferTo_name + "'," +
                     " zonalRM_name='" + lstransferto_zonalRMname + "'," +
                     " allocation_zonalRM='" + lstransferto_zonalRM + "'," +
                     " zonal_gid='" + lstransferTo_zonalgid + "'," +
                     " state_gid='" + lstransferTo_stategid + "'," +
                     " state_name='" + lstransferTo_statename + "'," +
                     " district_gid='" + lstransferTo_districtgid + "'," +
                     " district_name='" + lstransferTo_districtname + "'," +
                     " allocation_status='Allocated'" +
                     " where allocationdtl_gid='" + lsallocationdtl_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update rsk_trn_tallocation " +
                           " set assigned_RM='" + lstransfer_to + "'," +
                           " zonal_gid='" + lstransferTo_zonalgid + "'," +
                           " state_gid='" + lstransferTo_stategid + "'," +
                           " district_gid='" + lstransferTo_districtgid + "'" +
                           " where allocation_gid='" + lsallocation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = " update rsk_trn_tallocationtransfer set transferapproval_status ='Approved' " +
                        " where allocation_transfergid='" + lsallocation_transfergid + "'";
                mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update rsk_trn_tallocationdtl set allocatetransfer_flag='N' where allocationdtl_gid='" + lsallocationdtl_gid + "'";
                mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Transfer Approved Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostTransferFromReject(string employee_gid, transferapproval values)
        {

            msSQL = " update rsk_trn_ttransferapproval " +
                    " set zonalapprovalfrom_status ='Rejected', " +
                    " zonalapprovalfrom_remarks='" + values.approval_Remarks.Replace("'", "\\'") + "'," +
                    " zonalapprovalfrom_rejecteddate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                    " where transferapproval_gid='" + values.transferapproval_gid + "' and  zonal_approvalfrom ='" + employee_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update rsk_trn_ttransferapproval set transferapproval_status='Rejected' where transferapproval_gid='" + values.transferapproval_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_transfergid from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
            string lsallocation_transfergid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update rsk_trn_tallocationtransfer set transferapproval_status ='Rejected' " +
                    " where allocation_transfergid='" + lsallocation_transfergid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Transfer Rejected Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }


        public bool DaPostTransferToReject(string employee_gid, transferapproval values)
        {

            msSQL = " update rsk_trn_ttransferapproval " +
                    " set zonalapprovalto_status ='Rejected', " +
                    " zonalapprovalto_remarks='" + values.approval_Remarks.Replace("'", "\\'") + "'," +
                    " zonalapprovalto_rejecteddate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                    " where transferapproval_gid='" + values.transferapproval_gid + "' and  zonal_approvalto ='" + employee_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update rsk_trn_ttransferapproval set transferapproval_status='Rejected' where transferapproval_gid='" + values.transferapproval_gid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select allocation_transfergid from rsk_trn_ttransferapproval where transferapproval_gid='" + values.transferapproval_gid + "'";
            string lsallocation_transfergid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update rsk_trn_tallocationtransfer set transferapproval_status ='Rejected' " +
                    " where allocation_transfergid='" + lsallocation_transfergid + "'";
            mnResult =  objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Transfer Rejected Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }


        public bool DaGetApprovalHistorySummary(string employee_gid, allocationtransferList values)
        {

            msSQL = " select b.allocation_transfergid,a.transferapproval_gid,b.customer_gid,c.customer_urn,c.customername,c.vertical_code,b.allocationdtl_gid, " +
                  " concat(b.transferFrom_statename, ' / ', b.transferFrom_districtname) as allocatedLocation, " +
                  " b.transferFrom_name,b.transferfrom_zonalRMname,b.transferTo_name, " +
                  " zonalapprovalfrom_name,zonalapprovalto_name,a.transferapproval_status, " +
                  " concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as InitiatedBy, " +
                  " date_format(a.created_date, '%d-%m-%Y') as initiatedDate, a.transfer_zonal " +
                  " from rsk_trn_ttransferapproval a " +
                  " left join rsk_trn_tallocationtransfer b on a.allocation_transfergid = b.allocation_transfergid " +
                  " left join ocs_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                  " left join hrm_mst_temployee i on i.employee_gid = a.created_by " +
                  " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                  " where  (a.zonal_approvalto='" + employee_gid + "' or a.zonal_approvalfrom='" + employee_gid + "') " +
                  " and (a.zonalapprovalto_status in ('Approved','Rejected') or a.zonalapprovalfrom_status in ('Approved','Rejected'))";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_allocationtransferdtl = new List<allocationtransferdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    get_allocationtransferdtl.Add(new allocationtransferdtl
                    {
                        transferapproval_gid = dt["transferapproval_gid"].ToString(),
                        allocation_transfergid = dt["allocation_transfergid"].ToString(),
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        ZonalRMname = dt["transferfrom_zonalRMname"].ToString(),
                        transfer_from = dt["transferFrom_name"].ToString(),
                        transferto_name = dt["transferTo_name"].ToString(),
                        zonal_approvalfromname = dt["zonalapprovalfrom_name"].ToString(),
                        zonal_approvaltoname = dt["zonalapprovalto_name"].ToString(),
                        approval_status = dt["transferapproval_status"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customername = dt["customername"].ToString(),
                        created_by = dt["InitiatedBy"].ToString(),
                        created_date = dt["initiatedDate"].ToString(),
                        transfer_zonal = dt["transfer_zonal"].ToString(),
                    });
                }
                values.allocationtransferdtl = get_allocationtransferdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetViewTransferApprovalDetails(string allocation_transfergid, viewtransferdtl values)
        {

            msSQL = " select concat(j.user_firstname,' ',j.user_lastname,' / ',j.user_code) as InitiatedBy,b.transfer_remarks,b.transfer_zonal," +
                    " date_format(b.created_date, '%d-%m-%Y') as initiatedDate,a.transferapproval_status, " +
                    " c.customer_urn,c.customername,c.vertical_code,concat(d.zonal_name,' / ' ,b.transferFrom_statename, ' / ', b.transferFrom_districtname) as allocatedLocation, " +
                    " b.transferFrom_name,b.transferfrom_zonalRMname,b.transferto_name,zonalapprovalfrom_name, " +
                    " zonalapprovalfrom_remarks,zonalapprovalto_remarks,zonalapprovalfrom_status,zonalapprovalto_status, " +
                    " concat(e.zonal_name,' / ' ,b.transferTo_statename,' / ',b.transferTo_districtname) as zonaltransferdtl," +
                    " case when a.zonal_approvalto<>'' then a.zonalapprovalto_name else a.zonalapprovalfrom_name end as zonalapprovalto_name ," +
                    " case when(zonalapprovalfrom_status = 'Approved') then date_format(zonalapprovalfrom_approveddate,'%d-%m-%Y') else " +
                    " date_format(zonalapprovalfrom_rejecteddate, '%d-%m-%Y') end as zonalapprovalFromDate, " +
                    " case when(zonalapprovalto_status = 'Approved') then date_format(zonalapprovalto_approveddate,'%d-%m-%Y') else " +
                    " date_format(zonalapprovalto_rejecteddate, '%d-%m-%Y') end as zonalapprovalToDate " +
                    " from rsk_trn_ttransferapproval a " +
                    " left join rsk_trn_tallocationtransfer b on a.allocation_transfergid = b.allocation_transfergid " +
                    " left join ocs_mst_tcustomer c on a.customer_gid = c.customer_gid " +
                    " left join rsk_mst_tzonalmapping d on b.transferFrom_zonalgid=d.zonalmapping_gid " +
                    " left join rsk_mst_tzonalmapping e on b.transferTo_zonalgid=e.zonalmapping_gid " +
                    " left join hrm_mst_temployee i on i.employee_gid = b.created_by " +
                    " left join adm_mst_tuser j on j.user_gid = i.user_gid " +
                    " where a.allocation_transfergid = '" + allocation_transfergid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.created_by = objODBCDatareader["InitiatedBy"].ToString();
                values.created_date = objODBCDatareader["initiatedDate"].ToString();
                values.allocatedLocation = objODBCDatareader["allocatedLocation"].ToString();
                values.zonaltransferdtl = objODBCDatareader["zonaltransferdtl"].ToString();
                values.overallApproval_status = objODBCDatareader["transferapproval_status"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customername = objODBCDatareader["customername"].ToString();
                values.location = objODBCDatareader["allocatedLocation"].ToString();
                values.transfer_from = objODBCDatareader["transferFrom_name"].ToString();
                values.transfer_to = objODBCDatareader["transferto_name"].ToString();
                values.ZonalRMname = objODBCDatareader["transferfrom_zonalRMname"].ToString();
                values.zonal_approvalfromname = objODBCDatareader["zonalapprovalfrom_name"].ToString();
                values.zonal_approvaltoname = objODBCDatareader["zonalapprovalto_name"].ToString();
                values.zonalapprovalFrom_remarks = objODBCDatareader["zonalapprovalfrom_remarks"].ToString();
                values.zonalapprovalFromTo_remarks = objODBCDatareader["zonalapprovalto_remarks"].ToString();
                values.zonalapprovalfrom_status = objODBCDatareader["zonalapprovalfrom_status"].ToString();
                values.zonalapprovalTo_status = objODBCDatareader["zonalapprovalto_status"].ToString();
                values.zonalapprovalFrom_Date = objODBCDatareader["zonalapprovalFromDate"].ToString();
                values.zonalapprovalTo_Date = objODBCDatareader["zonalapprovalToDate"].ToString();
                values.transfer_remarks = objODBCDatareader["transfer_remarks"].ToString();
                values.transfer_status = objODBCDatareader["transfer_zonal"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaGetAllocateDistritDtl(string state_gid, statedtlList values)
        {

            msSQL = " select district_gid, district_name from rsk_mst_tRMmapping a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.assigned_RM " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where state_gid = '" + state_gid + "' and assigned_RM<> ''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_statedtl = new List<statedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_statedtl.Add(new statedtl
                    {
                        district_name = dt["district_name"].ToString(),
                        district_gid = dt["district_gid"].ToString(),
                    });
                }
                values.statedtl = get_statedtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetDistrictAllocateRM(string district_gid, allocateRMdtl values)
        {
            msSQL = " select a.assigned_RM,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as assignedRM_name,zonal_RM from rsk_mst_tRMmapping a " +
                  " left join hrm_mst_temployee b on b.employee_gid = a.assigned_RM " +
                  " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                  " where district_gid = '" + district_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.assigned_RMGid = objODBCDatareader["assigned_RM"].ToString();
                values.assigned_RMname = objODBCDatareader["assignedRM_name"].ToString();
                values.zonal_RMGid = objODBCDatareader["zonal_RM"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }
    }
}