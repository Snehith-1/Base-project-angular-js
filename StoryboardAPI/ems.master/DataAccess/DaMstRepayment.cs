using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;
using System.Drawing;

namespace ems.master.DataAccess
{
    public class DaMstRepayment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        public void DaGetRepayment(string urn, MdlMstRepayment values)
        {
            msSQL = " select  /*+ MAX_EXECUTION_TIME(300000) */ distinct repyment_gid,account_no,date_format(repayment_date,'%d-%m-%Y') as repayment_date,date_format(transaction_date,'%d-%m-%Y') as transaction_date," +
                   " transaction_id,repayment_amount,principal,count(account_no) as count" +
                   " from ocs_trn_trepaymentinfo where URN='" + urn + "' group by account_no";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrepayment_list = new List<repaymentaccount_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getrepayment_list.Add(new repaymentaccount_list
                    {
                        account_no = (dr_datarow["account_no"].ToString()),
                        count = (dr_datarow["count"].ToString()),
                        repyment_gid = (dr_datarow["repyment_gid"].ToString()),
                    });
                }
                values.repaymentaccount_list = getrepayment_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetRepayment_list(string account_no, MdlMstRepayment values)
        {
            msSQL = " select  /*+ MAX_EXECUTION_TIME(300000) */ distinct repyment_gid,account_no,date_format(repayment_date,'%d-%m-%Y') as repayment_date,date_format(transaction_date,'%d-%m-%Y') as transaction_date," +
                   " transaction_id,repayment_amount,principal," +
                   " normal_interest,penal_interest,for_feiture_waiver,user_id,instrument,repayment_type," +
                   " dpd,old_dpd,account_code,interest_tds,remarks,URN from ocs_trn_trepaymentinfo where account_no='"+account_no+"'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrepayment_list = new List<repayment_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getrepayment_list.Add(new repayment_list
                    {
                        account_no = (dr_datarow["account_no"].ToString()),
                        repayment_date = (dr_datarow["repayment_date"].ToString()),
                        transaction_date = (dr_datarow["transaction_date"].ToString()),
                        repayment_amount = (dr_datarow["repayment_amount"].ToString()),
                        principal = (dr_datarow["principal"].ToString()),
                        repyment_gid = (dr_datarow["repyment_gid"].ToString()),
                        dpd = (dr_datarow["dpd"].ToString()),
                        repayment_type = (dr_datarow["repayment_type"].ToString()),
                    });
                }
                values.repayment_list = getrepayment_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetRepayment_info(string repyment_gid, MdlRepaymentInfo values)
        {
            try { 
            msSQL = " select repyment_gid,account_no,date_format(repayment_date,'%d-%m-%Y') as repayment_date,date_format(transaction_date,'%d-%m-%Y') as transaction_date," +
                   " transaction_id,repayment_amount,principal," +
                   " normal_interest,penal_interest,for_feiture_waiver,user_id,instrument,repayment_type," +
                   " dpd,old_dpd,account_code,interest_tds,remarks,URN from ocs_trn_trepaymentinfo where repyment_gid='" + repyment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if(objODBCDatareader .HasRows==true)
            {
                values.account_no = objODBCDatareader["account_no"].ToString();
                values.repayment_date = objODBCDatareader["repayment_date"].ToString();
                values.transaction_date = objODBCDatareader["transaction_date"].ToString();
                values.transaction_id = objODBCDatareader["transaction_id"].ToString();
                values.repayment_amount = objODBCDatareader["repayment_amount"].ToString();
                values.principal = objODBCDatareader["principal"].ToString();
                values.normal_interest = objODBCDatareader["normal_interest"].ToString();
                values.penal_interest = objODBCDatareader["penal_interest"].ToString();
                values.for_feiture_waiver = objODBCDatareader["for_feiture_waiver"].ToString();
                values.user_id = objODBCDatareader["user_id"].ToString();
                values.instrument = objODBCDatareader["instrument"].ToString();
                values.repayment_type = objODBCDatareader["repayment_type"].ToString();
                values.dpd = objODBCDatareader["dpd"].ToString();
                values.old_dpd = objODBCDatareader["old_dpd"].ToString();
                values.account_code = objODBCDatareader["account_code"].ToString();
                values.interest_tds = objODBCDatareader["interest_tds"].ToString();
                values.remarks = objODBCDatareader["remarks"].ToString();
                values.URN = objODBCDatareader["URN"].ToString();
                objODBCDatareader.Close();
            }
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
    }
}