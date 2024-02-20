using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.lgl.Models;
using ems.utilities.Functions;
using System.Configuration;

namespace ems.lgl.DataAccess
{
    public class DaLglTrnDNTrackerAE
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        int mnResult;
        private string customer_gid;

        public bool DaGetAEPendingList(string employee_gid, MdlDNpendingList values)
        {
            msSQL = " (select max(od_days) as max_od_days,urn,acknowledgement_status,a.Customer_name,Vertical,DN_status,GurantorCustomerId,Guarantor_Name" +
                    " from lgl_trn_tmisdata a where Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') and natureof_credit_amount >= 5000 "+
                    " and dn_status = 'Pending' and a.exclusion_flag = 'N' and od_days<> '0' group by urn order by od_days desc) UNION" +
                    " (select  max(od_days) as max_od_days, b.urn, acknowledgement_status,Customer_name, Vertical," +
                    " DN_status,GurantorCustomerId,Guarantor_Name from lgl_trn_tmisdata  a" +
                    " left join lgl_trn_tcourierdetails b on a.urn = b.urn where ((DATEDIFF(delivered_date,now())) >= '15' or(DATEDIFF(dn2delivered_date,now())) >= '15'" +
                    " or(DATEDIFF(dn3delivered_date,now())) >= '15'  or(DATEDIFF(CBOdelivered_date,now())) >= '15') and " +
                    " vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') and natureof_credit_amount >= 5000 and "+
                    " a.exclusion_flag = 'N' and od_days<> '0' and dn_status not in ('DN Skip','Legal SR')" +
                    " group by a.urn order by od_days desc )";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDNpending_list = new List<DNpending_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDNpending_list.Add(new DNpending_list
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        GurantorCustomerId = (dr_datarow["GurantorCustomerId"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                    });
                }
                values.DNpending_list = getDNpending_list;
            }
            dt_datatable.Dispose();           
            values.status = true;
            return true;
        }
        public bool DaGetAEGeneratedList(string employee_gid, MdlDNGeneratedList values)
        {
            msSQL = " select max(od_days) as max_od_days,urn,acknowledgement_status,Customer_name,GurantorCustomerId,Guarantor_Name,Vertical,DN_status from lgl_trn_tmisdata  " +
                    " where Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') " +
                    " and natureof_credit_amount >=5000 and dn_status not in ('Pending','Legal SR','DN Skip')" +
                    " and exclusion_flag = 'N' group by urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgenerated_list = new List<DNgenerated_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgenerated_list.Add(new DNgenerated_list
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        GurantorCustomerId = (dr_datarow["GurantorCustomerId"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                       acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                       
                    });
                }
                values.DNgenerated_list = getgenerated_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetAESkippedList(string employee_gid, MdlDNSkippedList values)
        {
            msSQL = " select max(od_days) as max_od_days,a.urn,Customer_name,GurantorCustomerId,Guarantor_Name,Vertical," +
                    " date_format(validity, '%d-%m-%Y') as validity,  date_format(created_date, '%d-%m-%Y') as Skipped_on " +
                    " from lgl_trn_tdnskipcase a" +
                    " left join lgl_tmp_tmisdata b on a.urn = b.urn" +
                    " where Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises')" +
                    " group by b.urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getskipped_list = new List<DNskipped_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getskipped_list.Add(new DNskipped_list
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        GurantorCustomerId = (dr_datarow["GurantorCustomerId"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        Skipped_on = dr_datarow["Skipped_on"].ToString(),
                        validity = dr_datarow["validity"].ToString(),
                    });
                }
                values.DNskipped_list = getskipped_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetAEExclusionList(string employee_gid, MdlDNexclusionList values)
        {
            msSQL = "select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status," +
                      " a.Customer_name,Guarantor_Name,Vertical,DN_status,date_format(a.exclusion_updateddate, '%d-%m-%Y') as excluded_date," +
                      " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as excluded_by from lgl_trn_tmisdata a" +
                       " left join hrm_mst_temployee b on a.exclusion_updatedby = b.employee_gid" +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                       " Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises')" +
                       " and natureof_credit_amount >= 5000 and exclusion_flag = 'Y' group by urn order by od_days desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getexclusion_list = new List<DNexclusion_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getexclusion_list.Add(new DNexclusion_list
                    {
                        od_days = (dr_datarow["max_od_days"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                        excluded_date = dr_datarow["excluded_date"].ToString(),
                        excluded_by = dr_datarow["excluded_by"].ToString()
                    });
                }
                values.DNexclusion_list = getexclusion_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetAELegalSRList(MdlDNlegalsrList values, string employee_gid)
        {

            msSQL = " select a.customer_gid, a.templegalsr_gid,d.email,d.mobileno,a.auth_status,a.approval_status," +
                    " a.srref_no,a.account_name," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by, " +
                    " d.customer_urn,d.customername " +
                    " from lgl_tmp_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " where a.created_by='" + employee_gid + "' and vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises')"+
                    " order by a.created_date desc , d.customername asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlegalSR = new List<DNlegalsr_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlegalSR.Add(new DNlegalsr_list
                    {
                        templegalsr_gid = dr_datarow["templegalsr_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        email_id = dr_datarow["email"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        mobile_no = dr_datarow["mobileno"].ToString()
                    });
                }
                values.DNlegalsr_list = getlegalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public void DaGetCustomer(string urn, customereditlist values)
        {
            try
            {
                msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + urn + "'";
                string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
                if(lscustomer_gid==""||lscustomer_gid ==null)
                {
                    msSQL = "select tmpcustomer_gid from ocs_tmp_tcustomer where customer_urn='" + urn + "'";
                     lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
                }
                msSQL = " select customer_code,customername,contactperson,mobileno,gst_number,pan_number," +
    " contact_no,email,address,address2,region,vertical_gid,vertical_code,ccmail_text," +
    " country,state_gid,postalcode,business_head,zonal_head,zonal_name,businesshead_name," +
    " cluster_manager_gid,cluster_manager_name,relationshipmgmt_name,relationship_manager,creditmanager_gid,creditmgmt_name,customer_urn," +
    " major_corporate,constitution_name,constitution_gid,zonal_riskmanager,zonal_riskmanagerName,assigned_RM,assigned_RMName, " +
    " riskMonitoring_GID,riskMonitoring_Name from ocs_mst_tcustomer where customer_gid='" + lscustomer_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                    values.customerNameedit = objODBCDatareader["customername"].ToString();
                    values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                    values.customer_gid = lscustomer_gid;
                    values.zonal_riskmanagerName = objODBCDatareader["zonal_riskmanagerName"].ToString();
                    values.riskMonitoring_GID = objODBCDatareader["riskMonitoring_GID"].ToString();
                    values.riskMonitoring_Name = objODBCDatareader["riskMonitoring_Name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public bool DaGetAllocationHistory(string urn, overallhistoryallocationlist values)
        {
            msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + urn + "'";
            string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lscustomer_gid == "" || lscustomer_gid == null)
            {
                msSQL = "select tmpcustomer_gid from ocs_tmp_tcustomer where customer_urn='" + urn + "'";
                lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
            }
            msSQL = " select a.customer_gid,b.customername,c.cancel_flag,b.customer_urn,k.observation_reportgid, " +
                   " a.allocationdtl_gid,a.allocation_status,a.allocate_external, " +
                   " concat(a.state_name,' / ',a.district_name) as allocatedLocation,a.allocation_flag, " +
                   " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) as assigned_RM, " +
                   " date_format(a.lastvisit_date,'%d-%m-%Y') as lastvisit_date, " +
                   " concat(h.user_firstname, ' ', h.user_lastname, ' / ', h.user_code) as ZonalRMname," +
                   " concat(j.user_firstname, ' ', j.user_lastname, ' / ', j.user_code) as AllocatedBy,l.tier1format_gid, " +
                   " date_format(a.created_date,'%d-%m-%Y') as allocated_date,date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date" +
                   " from rsk_trn_tallocationdtl a " +
                   " left join ocs_mst_tcustomer b on a.customer_gid=b.customer_gid " +
                   " left join rsk_trn_tvisitreportgenerate c on c.allocationdtl_gid=a.allocationdtl_gid " +
                   " left join hrm_mst_temployee e on e.employee_gid = a.allocation_assignedRM " +
                   " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                   " left join hrm_mst_temployee g on g.employee_gid = a.allocation_zonalRM " +
                   " left join adm_mst_tuser h on h.user_gid = g.user_gid " +
                   " left join hrm_mst_temployee i on i.employee_gid = a.allocated_by " +
                   " left join adm_mst_tuser j on i.user_gid = j.user_gid " +
                   " left join rsk_trn_tobservationreport k on k.allocationdtl_gid = a.allocationdtl_gid " +
                   " left join rsk_trn_ttier1format l on l.allocationdtl_gid=a.allocationdtl_gid " +
                   " where a.customer_gid='" + lscustomer_gid + "' and a.completed_flag='Y' order by a.allocationdtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<overallhistoryallocationdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_mappingdtl.Add(new overallhistoryallocationdtl
                    {
                        allocationdtl_gid = dt["allocationdtl_gid"].ToString(),
                        customername = dt["customername"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        location = dt["allocatedLocation"].ToString(),
                        assigned_RM = dt["assigned_RM"].ToString(),
                        ZonalRMname = dt["ZonalRMname"].ToString(),
                        allocation_flag = dt["allocation_flag"].ToString(),
                        allocation_status = dt["allocation_status"].ToString(),
                        lastvisit_date = dt["lastvisit_date"].ToString(),
                        created_by = dt["AllocatedBy"].ToString(),
                        created_date = dt["allocated_date"].ToString(),
                        completed_date = dt["completed_date"].ToString(),
                        reportcancel_flag = dt["cancel_flag"].ToString(),
                        allocate_external = dt["allocate_external"].ToString(),
                        observation_reportgid = dt["observation_reportgid"].ToString(),
                        tier1format_gid = dt["tier1format_gid"].ToString(),
                    });
                }
                values.overallhistoryallocationdtl = get_mappingdtl;
            }
            dt_datatable.Dispose();


            return true;
        }
        public bool DaGetAE_Count(string employee_gid,count_dtl values)
        {

            msSQL = " select count(distinct urn) " +
                    " from lgl_trn_tmisdata a where Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') and natureof_credit_amount >= 5000 " +
                    " and dn_status = 'Pending' and a.exclusion_flag = 'N' and od_days<> '0' ";
            string lspending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select  count(distinct a.urn)  from lgl_trn_tmisdata  a" +
             " left join lgl_trn_tcourierdetails b on a.urn = b.urn where ((DATEDIFF(now(), delivered_date)) >= '15' or(DATEDIFF(now(), dn2delivered_date)) >= '15'" +
             " or(DATEDIFF(now(), dn3delivered_date)) >= '15'  or(DATEDIFF(now(), CBOdelivered_date)) >= '15') and " +
             " vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') and natureof_credit_amount >= 5000 and " +
             " a.exclusion_flag = 'N' and od_days<> '0' and dn_status not in ('DN Skip','Legal SR')";
            string lspending1_count = objdbconn.GetExecuteScalar(msSQL);

            int lsvalue = Convert.ToInt32(lspending_count);
            int lsvalue1 = Convert.ToInt32(lspending1_count);
            string lspending = (lsvalue1 + lsvalue).ToString();

            values.lblpending_count = lspending;

            msSQL = " select count(distinct urn) from lgl_trn_tmisdata  " +
                    " where Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') " +
                    " and natureof_credit_amount >=5000 and dn_status not in ('Pending','Legal SR','DN Skip')" +
                    " and exclusion_flag = 'N' ";
            values.lblgenerated_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lblgenerated_count == "")
            {
                values.lblgenerated_count = "0";
            }

            msSQL = " select count(distinct a.urn) " +
                    " from lgl_trn_tdnskipcase a" +
                    " left join lgl_tmp_tmisdata b on a.urn = b.urn where Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises') ";
            values.lblskipped_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lblskipped_count == "")
            {
                values.lblskipped_count = "0";
            }
            msSQL = "select count(distinct a.urn) from lgl_trn_tmisdata a" +
                      " left join hrm_mst_temployee b on a.exclusion_updatedby = b.employee_gid" +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                      " Vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises')" +
                      " and natureof_credit_amount >= 5000 and exclusion_flag = 'Y' ";
            values.lblexclusion_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lblexclusion_count == "")
            {
                values.lblexclusion_count = "0";
            }
            msSQL = " select count(distinct a.customer_urn) from lgl_tmp_traiselegalSR a" +
                 " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                 " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                 " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                 " where a.created_by='" + employee_gid + "' and vertical in ('AE','Agri Enterprises','Agri Enterprise','AgriEnterprises')";
            values.lbllegalsr_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lbllegalsr_count == "")
            {
                values.lbllegalsr_count = "0";
            }
            return true;
        }

    }

}