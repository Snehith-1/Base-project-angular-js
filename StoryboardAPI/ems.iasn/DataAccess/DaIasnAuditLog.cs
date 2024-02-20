using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.iasn.Models;
using ems.utilities.Functions;

namespace ems.iasn.DataAccess
{
    public class DaIasnAuditLog
    {
        dbconn objdbconn = new dbconn();
        DataTable dt_datatable;
        string msSQL;
        int mnResult = 0;

        public void DaGetAuditLogSummary(string email_gid, MdlAuditLogHistory values)
        {
            msSQL = " SELECT a.action_taken,DATE_FORMAT(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " concat(b.user_firstname,'',b.user_lastname,'/',b.user_code) as created_by" +
                  " FROM isn_trn_tauditlog a" +
                  " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                  " WHERE a.email_gid='" + email_gid + "' AND a.created_by NOT IN (SELECT user_gid FROM adm_mst_tuser WHERE user_gid ='U1' or user_gid='SUSM1907240067') " +
                  " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAuditLog = dt_datatable.AsEnumerable().Select(row => new MdlAuditLog
                {
                    action_taken = row["action_taken"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Record Found";


            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }
            dt_datatable.Dispose();
        }

        public void DaPostAuditView(string email_gid, string user_gid)
        {
            msSQL = " INSERT INTO isn_trn_tauditlog(" +
                        " email_gid," +
                        " action_taken," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + email_gid + "'," +
                        "'View'," +
                        "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaComposeAuditLog(string composemail_gid, MdlAuditLogHistory values)
        {
            msSQL = " SELECT a.action_taken,DATE_FORMAT(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " concat(b.user_firstname,'',b.user_lastname,'/',b.user_code) as created_by" +
                  " FROM isn_trn_tcomposeauditlog a" +
                  " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                  " WHERE a.composemail_gid='" + composemail_gid + "' AND a.created_by NOT IN (SELECT user_gid FROM adm_mst_tuser WHERE user_gid ='U1' or user_gid='SUSM1907240067') " +
                  " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAuditLog = dt_datatable.AsEnumerable().Select(row => new MdlAuditLog
                {
                    action_taken = row["action_taken"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Record Found";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }
            dt_datatable.Dispose();
        }

        public void DaPostComposeAuditView(string composemail_gid, string user_gid)
        {
            msSQL = " INSERT INTO isn_trn_tcomposeauditlog(" +
                        " composemail_gid," +
                        " action_taken," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + composemail_gid + "'," +
                        "'View'," +
                        "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }
    }
}