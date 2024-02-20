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
    public class DaIasnTrnMyWorkItem
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        int mnResult = 0;
        OdbcDataReader objODBCDataReader;

        public void DaGetMyWorkItemAllottedSummary(string employee_gid,string user_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status,b.zone_name," +
                    " b.rmemployee_gid,a.aging," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps," +
                    " b.seen_flag,"+
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " WHERE (b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='"+ user_gid + "') and a.history_flag='N'" +
                    " ORDER BY email_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    rmemployee_name =row["initial_caps"].ToString (),
                    aging =row["aging"].ToString (),
                    seen_flag =row["seen_flag"].ToString ()


                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
        }
        public void DaGetMyWorkItemPendingSummary(string employee_gid,string user_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status,b.zone_name," +
                    " b.rmemployee_gid,a.aging,b.checkeremployee_name," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,b.seen_flag" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " WHERE a.status='Pending' AND b.checkeremployee_gid= '" + employee_gid + "' and a.history_flag='N'" + 
                    " ORDER BY email_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    rmemployee_name =row["initial_caps"].ToString (),
                    aging =row["aging"].ToString (),
                    seen_flag=row["seen_flag"].ToString (),
                    checkeremployee_name = row["checkeremployee_name"].ToString(),

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
        }
        public void DaGetMyWorkItemPushbackSummary(string employee_gid,string user_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status,b.zone_name," +
                    " b.rmemployee_gid,a.aging,b.checkeremployee_name," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,b.seen_flag," +
                    " CONCAT(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on"+
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid"+
                    " WHERE  a.status='Pushback' and a.history_flag='N' AND (b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='" + user_gid + "')" +
                    " ORDER BY email_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    zone_name=row["zone_name"].ToString (),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    rmemployee_name =row["initial_caps"].ToString (),
                    aging =row["aging"].ToString (),
                    seen_flag =row["seen_flag"].ToString (),
                    updatedby_on =row["updatedby_on"].ToString (),
                    checkeremployee_name = row["checkeremployee_name"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
        }
        public void DaGetMyWorkItemForwardSummary(string employee_gid,string user_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status,b.zone_name," +
                    " b.rmemployee_gid,a.aging,b.checkeremployee_name," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,a.seen_flag," +
                    " CONCAT(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid"+
                    " WHERE a.status='Forward' and a.history_flag='N' AND ( b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='" + user_gid + "')" +
                    " ORDER BY email_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    rmemployee_name =row["initial_caps"].ToString (),
                    aging =row["aging"].ToString (),
                    seen_flag =row["seen_flag"].ToString (),
                    updatedby_on =row["updatedby_on"].ToString (),
                    checkeremployee_name = row["checkeremployee_name"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
        }
        public void DaGetMyWorkItemCloseSummary(string employee_gid,string user_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status,b.zone_name," +
                    " b.rmemployee_gid,a.aging,b.checkeremployee_name," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,b.seen_flag," +
                    " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on"+
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid"+
                    " WHERE a.status='Close' and a.history_flag='N' AND ( b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='" + user_gid + "')" +
                    " ORDER BY email_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    rmemployee_name =row["initial_caps"].ToString (),
                    aging=row["aging"].ToString (),
                    seen_flag =row["seen_flag"].ToString (),
                    updatedby_on =row["updatedby_on"].ToString (),
                    checkeremployee_name = row["checkeremployee_name"].ToString(),

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
        }
        public void DaGetMyWorkItemArchivalSummary(string employee_gid,string user_gid, WorkItemList values, MdlArchivalCondition objCondition)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status,b.zone_name," +
                    " b.rmemployee_gid,b.checkeremployee_name,c.archival_type,c.customer_name," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,b.seen_flag,a.aging," +
                    " concat(d.user_firstname,' ',d.user_lastname,'/',d.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on"+
                    " FROM isn_trn_tmaildetails a" +
                     " INNER JOIN isn_trn_tworkitemdecision c ON a.email_gid=c.email_gid" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " LEFT JOIN adm_mst_tuser d on a.updated_by=d.user_gid"+
                    " WHERE a.status='Archival' and a.history_flag='N' AND ( b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='"+ user_gid + "') ";
                     if (objCondition.customer_gid != "")
                       {
                     msSQL += " AND (c.archival_type = '" + objCondition.archival_type + "' AND c.customer_gid = '" + objCondition.customer_gid + "')";
                      }
                  msSQL +=" ORDER BY email_date DESC";
                  dt_datatable = objdbconn.GetDataTable(msSQL);
                  if (dt_datatable.Rows.Count != 0)
                  {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_name =row["initial_caps"].ToString (),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    aging=row["aging"].ToString (),
                    seen_flag=row["seen_flag"].ToString (),
                    updatedby_on=row["updatedby_on"].ToString (),
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    archival_type = row["archival_type"].ToString(),
                    customer_name = row["customer_name"].ToString()
                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
        }
        public void DaPostEmailSeen(string email_gid,string employee_gid)
        {
            msSQL = " UPDATE isn_trn_tworkitemassign SET" +
                   " seen_flag='Y'" +
                   " WHERE email_gid='" + email_gid + "' AND checkeremployee_gid='"+ employee_gid + "'";
           mnResult  = objdbconn.ExecuteNonQuerySQL(msSQL);

        }
        public void DaGetMyWorkItemCounts(string employee_gid, string user_gid, MyWorkItemListCount values)
        {

            msSQL = "SELECT COUNT(a.status) as WorkitemCount FROM isn_trn_tmaildetails a" +
                 " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                 " WHERE b.checkeremployee_gid= '" + employee_gid + "'  AND a.status ='Pending' AND a.history_flag='N'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_myworkitempending = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails a" +
                " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                " WHERE a.status='Pushback' and a.history_flag='N' and (b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='" + user_gid + "') ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_myworkitempushback = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails a" +
                " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                " WHERE a.status='Forward' and a.history_flag='N' and ( b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='" + user_gid + "') ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_myworkitemforward = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails a " +
                " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                " WHERE a.status='Close' AND a.history_flag='N' AND ( b.checkeremployee_gid= '" + employee_gid + "' OR a.updated_by='" + user_gid + "') ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_myworkitemclose = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();           
        }
    }
}