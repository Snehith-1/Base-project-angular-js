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
    public class DaMstTelecall
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        public void DaGetTelecall(string urn, MdlMstTelecall values)
        {
            msSQL = " select  /*+ MAX_EXECUTION_TIME(300000) */ distinct telecall_gid,start_time,completetion_time,account_no,typeof_call from ocs_trn_ttelecalinfo " +
                    " where URN='" + urn + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettelecall_list = new List<telecall_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettelecall_list.Add(new telecall_list
                    {
                        account_no = (dr_datarow["account_no"].ToString()),
                        telecall_gid = (dr_datarow["telecall_gid"].ToString()),
                        start_time = (dr_datarow["start_time"].ToString()),
                        completetion_time = (dr_datarow["completetion_time"].ToString()),
                        typeof_call = (dr_datarow["typeof_call"].ToString()),
                      
                    });
                }
                values.telecall_list = gettelecall_list;
            }
            dt_datatable.Dispose();
        }
     
        public void DaGetTelecall_info(string telecall_gid, telecallaccount_info values)
        {
            try
            {
                msSQL = " select start_time,completetion_time,email_address,name,relationship,customer_name,account_no,typeof_call,call_details,spoken_to,"+
                        " natureof_business,reason_OD,status,courseof_action,date_format(ptp_date,'%d-%m-%Y') as ptp_date,ptp_amount,remarksby_telecaller,"+
                        " date_format(followup_date,'%d-%m-%Y') as followup_date,ledger_balance," +
                        " total_demand_due,URN from ocs_trn_ttelecalinfo where telecall_gid='" + telecall_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.start_time = objODBCDatareader["start_time"].ToString();
                    values.completetion_time = objODBCDatareader["completetion_time"].ToString();
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.name = objODBCDatareader["name"].ToString();
                    values.relationship = objODBCDatareader["relationship"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.account_no = objODBCDatareader["account_no"].ToString();
                    values.typeof_call = objODBCDatareader["typeof_call"].ToString();
                    values.call_details = objODBCDatareader["call_details"].ToString();
                    values.spoken_to = objODBCDatareader["spoken_to"].ToString();
                    values.natureof_business = objODBCDatareader["natureof_business"].ToString();
                    values.reason_OD = objODBCDatareader["reason_OD"].ToString();
                    values.telecall_status = objODBCDatareader["status"].ToString();
                    values.courseof_action = objODBCDatareader["courseof_action"].ToString();
                    values.ptp_date = objODBCDatareader["ptp_date"].ToString();
                    values.ptp_amount = objODBCDatareader["ptp_amount"].ToString();
                    values.remarksby_telecaller = objODBCDatareader["remarksby_telecaller"].ToString();
                    values.followup_date = objODBCDatareader["followup_date"].ToString();
                    values.ledger_balance = objODBCDatareader["ledger_balance"].ToString();
                    values.total_demand_due = objODBCDatareader["total_demand_due"].ToString();
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