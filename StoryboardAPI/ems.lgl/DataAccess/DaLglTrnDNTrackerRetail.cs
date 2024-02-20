using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.lgl.Models;
using ems.utilities.Functions;

namespace ems.lgl.DataAccess
{
    public class DaLglTrnDNTrackerRetail
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        int mnResult;
        public bool DaGetRetailPendingList(string employee_gid, MdlDNpendingList values)
        {
            msSQL = " (select max(od_days) as max_od_days,urn,acknowledgement_status,a.Customer_name,GurantorCustomerId,Guarantor_Name,Vertical,DN_status" +
                    " from lgl_trn_tmisdata a where vertical ='Retail' and natureof_credit_amount >= 5000 " +
                    " and dn_status = 'Pending' and a.exclusion_flag = 'N' and od_days<> '0' group by urn order by od_days desc) UNION" +
                    " (select  max(od_days) as max_od_days, b.urn, acknowledgement_status,Customer_name,GurantorCustomerId, Guarantor_Name, Vertical," +
                    " DN_status from lgl_trn_tmisdata  a" +
                    " left join lgl_trn_tcourierdetails b on a.urn = b.urn where  ((DATEDIFF( delivered_date,now())) = '15' or(DATEDIFF( dn2delivered_date,now())) = '15'" +
                    " or(DATEDIFF(dn3delivered_date,now())) = '15'  or(DATEDIFF(CBOdelivered_date,now())) = '15') and " +
                    " vertical ='Retail' and natureof_credit_amount >= 5000 and " +
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
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                        GurantorCustomerId = (dr_datarow["GurantorCustomerId"].ToString()),
                    });
                }
                values.DNpending_list = getDNpending_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetRetailGeneratedList(string employee_gid, MdlDNGeneratedList values)
        {
            msSQL = " select max(od_days) as max_od_days,urn,acknowledgement_status,Customer_name,GurantorCustomerId,Guarantor_Name,Vertical,DN_status from lgl_trn_tmisdata  " +
                    " where vertical ='Retail'" +
                    " and natureof_credit_amount >=5000 and dn_status  not in ('Pending','Legal SR','DN Skip') "+
                    " and exclusion_flag = 'N'  group by urn order by od_days desc";
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
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        acknowledgement_status = dr_datarow["acknowledgement_status"].ToString(),
                        GurantorCustomerId = (dr_datarow["GurantorCustomerId"].ToString()),
                    });
                }
                values.DNgenerated_list = getgenerated_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetRetailSkippedList(string employee_gid, MdlDNSkippedList values)
        {
            msSQL = " select max(od_days) as max_od_days,a.urn,Customer_name,GurantorCustomerId,Guarantor_Name,Vertical," +
                    " date_format(validity, '%d-%m-%Y') as validity,  date_format(created_date, '%d-%m-%Y') as Skipped_on " +
                    " from lgl_trn_tdnskipcase a" +
                    " left join lgl_tmp_tmisdata b on a.urn = b.urn where vertical ='Retail'" +
                    "   group by urn order by od_days desc";
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
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        validity = dr_datarow["validity"].ToString(),
                        Skipped_on = dr_datarow["Skipped_on"].ToString(),
                        GurantorCustomerId = (dr_datarow["GurantorCustomerId"].ToString()),
                    });
                }
                values.DNskipped_list = getskipped_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetRetailExclusionList(string employee_gid, MdlDNexclusionList values)
        {
            msSQL = "select account_no,count(urn),max(od_days) as max_od_days,urn,acknowledgement_status," +
                      " a.Customer_name,Guarantor_Name,Vertical,DN_status,date_format(a.exclusion_updateddate, '%d-%m-%Y') as excluded_date," +
                      " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as excluded_by from lgl_trn_tmisdata a" +
                       " left join hrm_mst_temployee b on a.exclusion_updatedby = b.employee_gid" +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                      " vertical ='Retail'" +
                      " and natureof_credit_amount >=5000 and exclusion_flag='Y' group by urn order by od_days desc";
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
        public bool DaGetRetailLegalSRList(MdlDNlegalsrList values, string employee_gid)
        {

            msSQL = " select a.customer_gid, a.templegalsr_gid,d.email,d.mobileno,a.auth_status,a.approval_status," +
                    " a.srref_no,a.account_name," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by, " +
                    " d.customer_urn,d.customername " +
                    " from lgl_tmp_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " where a.created_by='" + employee_gid + "' and vertical ='Retail'" +
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
        public bool DaGetRetail_Count(string employee_gid, count_dtl values)
        {

            msSQL = " select count(distinct urn) " +
                    " from lgl_trn_tmisdata a where vertical ='Retail' and natureof_credit_amount >= 5000 " +
                    " and dn_status = 'Pending' and a.exclusion_flag = 'N' and od_days<> '0' ";
            string lspending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select  count(distinct a.urn)  from lgl_trn_tmisdata  a" +
             " left join lgl_trn_tcourierdetails b on a.urn = b.urn where ((DATEDIFF(now(), delivered_date)) >= '15' or(DATEDIFF(now(), dn2delivered_date)) >= '15'" +
             " or(DATEDIFF(now(), dn3delivered_date)) >= '15'  or(DATEDIFF(now(), CBOdelivered_date)) >= '15') and " +
             " vertical ='Retail' and natureof_credit_amount >= 5000 and " +
             " a.exclusion_flag = 'N' and od_days<> '0' and dn_status not in ('DN Skip','Legal SR')";
            string lspending1_count = objdbconn.GetExecuteScalar(msSQL);

            int lsvalue = Convert.ToInt32(lspending_count);
            int lsvalue1 = Convert.ToInt32(lspending1_count);
            string lspending = (lsvalue1 + lsvalue).ToString();

            values.lblpending_count = lspending;

            msSQL = " select count(distinct urn) from lgl_trn_tmisdata  " +
                    " where vertical ='Retail'" +
                    " and natureof_credit_amount >=5000 and dn_status not in ('Pending','Legal SR','DN Skip')" +
                    " and exclusion_flag = 'N' ";
            values.lblgenerated_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lblgenerated_count == "")
            {
                values.lblgenerated_count = "0";
            }

            msSQL = " select count(distinct a.urn) " +
                    " from lgl_trn_tdnskipcase a" +
                    " left join lgl_tmp_tmisdata b on a.urn = b.urn " + 
                    " where vertical ='Retail' ";
            values.lblskipped_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lblskipped_count == "")
            {
                values.lblskipped_count = "0";
            }
            msSQL = "select count(distinct a.urn) from lgl_trn_tmisdata a" +
                      " left join hrm_mst_temployee b on a.exclusion_updatedby = b.employee_gid" +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid where  vertical ='Retail' "+
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
                 " where a.created_by='" + employee_gid + "' and  vertical ='Retail'";
            values.lbllegalsr_count = objdbconn.GetExecuteScalar(msSQL);
            if (values.lbllegalsr_count == "")
            {
                values.lbllegalsr_count = "0";
            }
            return true;
        }
    }
}