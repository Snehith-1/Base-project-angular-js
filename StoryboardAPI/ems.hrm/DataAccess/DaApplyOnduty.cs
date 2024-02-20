using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.hrm.Models;
namespace ems.hrm.DataAccess
{
    public class DaApplyOnduty
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        DataTable objTblRQ;
        DataTable table;
        string msSQL;
        string msGetGID;
        int mnResult;
        DateTime onduty_date;
        DateTime onduty_from;
        DateTime onduty_to;
        DateTime onduty_duration;
        string onduty_reason;
        string ondutytracker_status;
        string approved_by;
        DateTime approved_date;

        public bool postapplyonduty_da(string employee_gid, string user_gid, applyondutydetails values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("HLVP");
            if (msGetGID == "E")
            {
                return false;
            }
            msSQL = " Insert into hrm_trn_tondutytracker" +
                " (ondutytracker_gid, " +
                " employee_gid," +
                " onduty_fromtime," +
                " onduty_totime, " +
                " from_minutes, " +
                " to_minutes, " +
                " onduty_duration," +
                " onduty_reason," +
                " ondutytracker_date , " +
                " ondutytracker_status," +
                " half_day, " +
                " half_session, " +
                " onduty_count, " +
                " created_by," +
                " created_date)" +
                "  Values  (" +
                "'" + msGetGID + "'," +
                "'" + employee_gid + "'," +
                "'" + values.od_fromhr + "'," +
                "'" + values.od_tohr + "'," +
                "'" + values.od_frommin + "'," +
                "'" + values.od_tomin + "'," +
                "'" + values.total_duration + "'," +
                "'" + values.od_reason + "'," +
                "'" + values.od_date + "'," +
                "'" + values.ondutytracker_status + "'," +
                "'" + values.half_day_flag + "'," +
                "'" + values.half_session + "'," +
                "'" + values.onduty_count + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if (mnResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool getondutysummary_da(string employee_gid, onduty_detail_list objonduty_details)
        {
            msSQL = " select a.employee_gid,concat(onduty_fromtime, ':', from_minutes, ':00') as onduty_from,onduty_reason," +
                    " concat(onduty_totime, ':', to_minutes, ':00') as onduty_to," +
                    " date_format(ondutytracker_date,'%d-%m-%Y') as ondutytracker_date,onduty_duration,ondutytracker_status," +
                    " concat(c.user_firstname, ' ', c.user_lastname) as onduty_approveby,onduty_approvedate" +
                    " from hrm_trn_tondutytracker a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.onduty_approveby" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " where a.employee_gid = '" + employee_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var onduty_details = new List<onduty_details>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    onduty_details.Add(new onduty_details
                    {

                        onduty_from = dr_datarow["onduty_from"].ToString(),
                        onduty_to = dr_datarow["onduty_to"].ToString(),
                        onduty_date = (dr_datarow["ondutytracker_date"].ToString()),
                        onduty_duration = dr_datarow["onduty_duration"].ToString(),
                        ondutytracker_status = dr_datarow["ondutytracker_status"].ToString(),
                        approved_by = dr_datarow["onduty_approveby"].ToString(),
                        approved_date = dr_datarow["onduty_approvedate"].ToString(),
                        onduty_reason = dr_datarow["onduty_reason"].ToString()

                    });
                }
                objonduty_details.onduty_details = onduty_details;
            }
            dt_datatable.Dispose();
            

            return true;
        }
    }
}
