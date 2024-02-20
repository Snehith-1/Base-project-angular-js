using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.iasn.Models;
using System.IO;
using System.Configuration;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using ems.storage.Functions;

namespace ems.iasn.DataAccess
{
    public class DaIasnTrnWorkItem
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        DataTable dt_child;
        string msSQL;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        int mnResult;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        string msGetGID, msGetGID1, msGetDocumentGid, msGet_RefNO;
        result objResult = new Models.result();
        HttpPostedFile httpPostedFile;
        string msGetChildGid = string.Empty;
        DaIasnTrnSentMail objSentMail = new DaIasnTrnSentMail();
        DaIasnTrnComposeSentMail objComposeSentMail = new DaIasnTrnComposeSentMail();
        bool lsstatus;
        string lsto_address = string.Empty;
        string lscc_address = string.Empty;
        string lsbcc_address = string.Empty;
        string lsmessage_id = string.Empty;
        string lsreference_id = string.Empty;
        string lssubject = string.Empty;
        string lsmailbody = string.Empty;
        string ls_server, ls_username, ls_password;
        int ls_port;
        string lsworkref_no, lsrmemployee_name, lsclosed_by, sub;
        string[] lsrmname, lsclosedby, lsassign_by, lstransfer_by, lsname, lstomail;
        string[] bcc;
        string[] cc;
        string[] to;
        string lsattachment_flag = "N";
        string lsassign_remarks = string.Empty;
        string lspath;

        public void DaGetCountofWorkitems(string employee_gid, string user_gid, WorkItemListCount values)
        {

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails WHERE history_flag='N'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_workitemtotal = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT /*+ MAX_EXECUTION_TIME(300000) */ COUNT(a.status) as WorkitemCount FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    " WHERE 1 = 1 AND b.checkeremployee_name IS NOT NULL AND a.status ='Pending' and a.history_flag='N' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_workitemassigned = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT /*+ MAX_EXECUTION_TIME(300000) */ COUNT(a.status) as WorkitemCount FROM isn_trn_tmaildetails a" +
                 " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                 " WHERE 1 = 1 AND b.checkeremployee_name IS NULL AND a.status ='Pending' AND a.reference_flag='Y' and a.history_flag='N'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_workitempending = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails WHERE status='Pushback' and history_flag='N' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_pushback = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails WHERE status='Forward' and history_flag='N' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_forward = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails WHERE status='Close' and history_flag='N' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_close = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(status) as WorkitemCount FROM isn_trn_tmaildetails WHERE status='Archival' and history_flag='N' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_archival = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT COUNT(*) as WorkitemCount FROM isn_trn_tcomposemail WHERE status<>'Archival' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.count_composemail = objODBCDataReader["WorkitemCount"].ToString();
            }
            objODBCDataReader.Close();
        }

        public void DaGetWorkItemSummary(WorkItemList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(600000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                    " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,a.status,b.zone_gid," +
                    " b.rmemployee_gid,if(b.rmemployee_name is null,'No',  " +
                    " upper(substr(b.rmemployee_name,1,1))) as initial_caps,a.aging,a.seen_flag, b.hold_flag" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    " WHERE 1 = 1 AND b.checkeremployee_name IS NOT NULL AND a.status ='Pending' and a.history_flag='N' ORDER BY a.email_date desc";
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
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    status = row["status"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["initial_caps"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    hold_flag = row["hold_flag"].ToString()

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
        public void DaGetWorkItemPendingSummary(WorkItemList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(600000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                    " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,b.zone_gid," +
                    " b.rmemployee_gid,if(b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name,1,1))) as initial_caps,a.aging,a.seen_flag," +
                    " case when c.acknowledgement_flag = 'Y' then 'Yes' when c.acknowledgement_flag = 'N' then 'No' " +
                    " else 'Not Assigned' end as acknowledgement_status, a.hold_flag FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    " LEFT JOIN isn_mst_temployeelist c ON b.rmemployee_gid=c.employee_gid" +
                    " WHERE 1 = 1 AND a.status='Pending' AND b.checkeremployee_name IS NULL AND a.reference_flag='Y' and a.history_flag='N' GROUP BY a.email_gid ORDER BY a.email_date desc";
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
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["initial_caps"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    acknowledgement_status = row["acknowledgement_status"].ToString(),
                    hold_flag = row["hold_flag"].ToString(),
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
        public void DaGetWorkItemPushbackSummary(WorkItemList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(600000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                    " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,b.rmemployee_gid," +
                    " if(b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name,1,1))) as initial_caps,a.aging,a.seen_flag," +
                    " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    "  LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid" +
                    " WHERE 1 = 1 AND a.status='Pushback' and a.history_flag='N' order by a.email_date desc";
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
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["initial_caps"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    updatedby_on = row["updatedby_on"].ToString(),

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
        public void DaGetWorkItemForwardSummary(WorkItemList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(600000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                     " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                     " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,b.rmemployee_gid," +
                     " if(b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name,1,1))) as initial_caps,a.aging,a.seen_flag," +
                     " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on" +
                     " FROM isn_trn_tmaildetails a" +
                     " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                     " LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid" +
                     " WHERE 1 = 1 AND a.status='Forward' and a.history_flag='N' order by a.email_date desc";
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
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["initial_caps"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    updatedby_on = row["updatedby_on"].ToString()

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
        public void DaGetWorkItemCloseSummary(WorkItemList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(600000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                     " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                     " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,b.rmemployee_gid," +
                     " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as rmemployee_name,a.aging,a.seen_flag," +
                     " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on" +
                     " FROM isn_trn_tmaildetails a" +
                     " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                     " LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid" +
                     " WHERE 1 = 1 AND a.status='Close' and a.history_flag='N' order by a.email_date desc";
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
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    updatedby_on = row["updatedby_on"].ToString()

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
        public void DaGetWorkItemArchivalSummary(WorkItemList values, MdlArchivalCondition objCondition)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date, '%d-%m-%Y %h:%i %p') as email_date, " +
            " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.aging,a.seen_flag, b.rmemployee_gid,c.archival_type, " +
            " c.customer_name, concat(d.user_firstname, ' ', d.user_lastname, '/', d.user_code, ' & ', date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as " +
            " updatedby_on, if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as rmemployee_name, c.remarks " +
            " FROM isn_trn_tmaildetails a " +
            " INNER JOIN isn_trn_tworkitemdecision c ON a.email_gid = c.email_gid "+
            " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid = b.email_gid " +
            " LEFT JOIN adm_mst_tuser d on a.updated_by = d.user_gid "+
            " WHERE 1 = 1 AND a.status = 'Archival' AND c.decision = 'Archival' ";
            if (objCondition.customer_gid != "")
            {
                msSQL += " AND (c.archival_type = '" + objCondition.archival_type + "' AND c.customer_gid = '" + objCondition.customer_gid + "')";
            }
            msSQL += " GROUP BY a.email_gid UNION " +
            " SELECT a.composemail_gid, a.composemail_refno, a.frommail_id, date_format(a.created_date, '%d-%m-%Y %h:%i %p') as email_date, " +
            " a.email_subject,a.mailcontent, date_format(b.created_date, '%d-%m-%Y') as created_date,b.aging,b.seen_flag, e.rmemployee_gid, c.archival_type, " +
            " c.customer_name, concat(d.user_firstname, ' ', d.user_lastname, '/', d.user_code, ' & ', date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on, " +
            " if (e.rmemployee_name is null,'No', upper(substr(e.rmemployee_name, 1, 1))) as rmemployee_name, c.remarks " +
            " FROM isn_trn_tcomposemail a "+
            " INNER JOIN isn_trn_tworkitemdecision c ON a.composemail_gid = c.email_gid " +
            " LEFT JOIN isn_trn_tmaildetails b ON b.email_gid = c.email_gid "+
            " LEFT JOIN adm_mst_tuser d on a.updated_by = d.user_gid " +
            " LEFT JOIN isn_trn_tworkitemassign e ON e.email_gid = b.email_gid " +
            " WHERE 1 = 1 AND a.status = 'Archival' AND c.decision = 'Archival' ";
            if (objCondition.customer_gid != "")
            {
                msSQL += " AND (c.archival_type = '" + objCondition.archival_type + "' AND c.customer_gid = '" + objCondition.customer_gid + "')";
            }

            msSQL += " GROUP BY a.composemail_gid";
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
                    aging = row["aging"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    updatedby_on = row["updatedby_on"].ToString(),
                    archival_type = row["archival_type"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    remarks = row["remarks"].ToString()
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
        public void DaGetMyTeamWorkItemSummary(string employee_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date,b.team_name," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " WHERE b.team_gid in (select distinct team_gid from isn_mst_temployeelist where employee_gid = '" + employee_gid + "' and employee_type='Checker')" +
                    " and a.history_flag='N' ORDER BY email_date DESC";

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
                    status = row["status"].ToString()
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
        public void DaGetMyWorkItemSummary(string employee_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date,b.team_name," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,a.status," +
                    " b.rmemployee_gid," +
                    " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b on a.email_gid = b.email_gid" +
                    " WHERE b.checkeremployee_gid= '" + employee_gid + "'" +
                    " and a.history_flag='N' ORDER BY email_date DESC";
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
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["initial_caps"].ToString()

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
        public void DaGetWorkItemview(string lsemail_gid, MdlWorkItem values)
        {

            string lsteam_gid = string.Empty;

            msSQL = " SELECT " +
                    " a.workitemref_no, a.email_gid, a.email_from, a.from_mailaddress, date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " b.email_subject, a.email_content,  a.status_attachment,a.cc, a.bcc, b.message_id,b.reference_id," +
                    " a.email_to,a.status,a.aging, a.email_subject as originalmail_Subject," +
                    " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code,' & ',date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on," +
                    " case when d.close_acknowledge='Y' then 'Yes' when d.close_acknowledge='N' then 'No' end as Mail_Trigger, assigned_remarks, a.hold_flag,"+
                    " a.customer_name, a.customer_gid, a.customer_type, a.workitemhold_reason" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_treferencemail b ON a.email_gid=b.email_gid" +
                    " LEFT JOIN adm_mst_tuser c on a.updated_by=c.user_gid" +
                    " LEFT JOIN isn_trn_tworkitemdecision d on a.email_gid = d.email_gid" +
                    " LEFT JOIN isn_trn_tworkitemassign e on a.email_gid = e.email_gid" +
                    " WHERE a.email_gid = '" + lsemail_gid + "' " +
                    " UNION " +
                    " SELECT a.composemail_refno, a.composemail_gid, a.frommail_id, a.frommail_id, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.emailsubject_Ref as email_subject, a.mailcontent, a.attachment_flag, a.ccmail_id, a.bccmail_id, '-', '-', a.tomail_id ," +
                    " case when a.status='Pending' then 'Sent Mail' when a.status='Forward' then 'Sent Mail' else a.status end as status, '-'," +
                    " email_subject as originalmail_Subject, concat(b.user_firstname,' ',b.user_lastname,'/',b.user_code,' & ', "+
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as updatedby_on, '-', '-', '-','-','-','-','-'" +
                    " FROM isn_trn_tcomposemail a" +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.updated_by " +
                    " LEFT JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                    " LEFT JOIN isn_mst_temployeelist d on d.employee_gid = c.employee_gid "+
                    " WHERE a.composemail_gid = '" + lsemail_gid + "' ";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.email_gid = objODBCDataReader["email_gid"].ToString();
                values.email_from = objODBCDataReader["email_from"].ToString();
                values.email_date = objODBCDataReader["email_date"].ToString();
                var emailSubject = objODBCDataReader["email_subject"].ToString().Split('>');
                values.email_subject = emailSubject[2];
                values.firstemail_subject = objODBCDataReader["email_subject"].ToString().Split('>').First();
                values.originalmail_Subject = objODBCDataReader["originalmail_Subject"].ToString();
                values.email_content = objODBCDataReader["email_content"].ToString();
                values.status_attachment = objODBCDataReader["status_attachment"].ToString();
                values.cc = objODBCDataReader["cc"].ToString();
                values.bcc = objODBCDataReader["bcc"].ToString();
                values.message_id = objODBCDataReader["message_id"].ToString();
                values.reference_id = objODBCDataReader["reference_id"].ToString();
                values.email_to = objODBCDataReader["email_to"].ToString();
                values.workitemref_no = objODBCDataReader["workitemref_no"].ToString();
                values.status = objODBCDataReader["status"].ToString();
                values.aging = objODBCDataReader["aging"].ToString();
                values.updatedby_on = objODBCDataReader["updatedby_on"].ToString();
                values.email_address = objODBCDataReader["from_mailaddress"].ToString();
                values.Mail_Trigger = objODBCDataReader["Mail_Trigger"].ToString();
                values.assigned_remarks = objODBCDataReader["assigned_remarks"].ToString();
                values.hold_flag = objODBCDataReader["hold_flag"].ToString();
                values.workitemhold_reason = objODBCDataReader["workitemhold_reason"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
                values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                values.customer_type = objODBCDataReader["customer_type"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT remarks FROM isn_trn_tworkitemdecision where email_gid='" + lsemail_gid + "' and decision ='close'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.closedremarks = objODBCDataReader["remarks"].ToString();

            }
            objODBCDataReader.Close();
            msSQL = "SELECT remarks FROM isn_trn_tworkitemdecision where email_gid='" + lsemail_gid + "' and decision ='Archival'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.archivalremarks = objODBCDataReader["remarks"].ToString();

            }
            objODBCDataReader.Close();
            
            msSQL = " SELECT a.zone_gid, a.zone_name,concat(a.allottedby_name,' & ',date_format(a.allotted_on,'%d-%m-%Y %h:%i %p')) as allottedby_on," +
                    " a.rmemployee_gid, a.rmemployee_name,a.rmemployee_mailid,a.checkeremployee_gid,a.checkeremployee_name" +
                    " FROM isn_trn_tworkitemassign a" +
                    " WHERE a.email_gid = '" + lsemail_gid + "'" +
                    " UNION " +
                    " SELECT d.zone_gid, d.zone_name,  concat(b.user_firstname,' ',b.user_lastname,' & ',date_format(a.created_date,'%d-%m-%Y %h:%i %p')) as allottedby_on," +
                    " c.employee_gid, concat(b.user_firstname,' ',b.user_lastname), c.employee_emailid, '-' , '-' " +
                    " FROM isn_trn_tcomposemail a" +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by " +
                    " LEFT JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                    " LEFT JOIN isn_mst_temployeelist d on d.employee_gid = c.employee_gid WHERE a.composemail_gid = '" + lsemail_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.zone_gid = objODBCDataReader["zone_gid"].ToString();
                values.zone_name = objODBCDataReader["zone_name"].ToString();
                values.rmemployee_gid = objODBCDataReader["rmemployee_gid"].ToString();
                values.rmemployee_name = objODBCDataReader["rmemployee_name"].ToString();
                values.rmemployee_mailid = objODBCDataReader["rmemployee_mailid"].ToString();
                values.employee_gid = objODBCDataReader["checkeremployee_gid"].ToString();
                values.checkeremployee_name = objODBCDataReader["checkeremployee_name"].ToString();
                values.allottedby_on = objODBCDataReader["allottedby_on"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT mailattachment_gid,document_path,document_name, substring_index(document_name,'.',-1) as document_extension" +
                    " FROM isn_trn_tmaildetailsattachement " +
                    " WHERE email_gid = '" + lsemail_gid + "'"+
                    " UNION " +
                    " SELECT composemailattachment_gid,document_path,document_name, substring_index(document_name,'.',-1) as document_extension" +
                    " FROM isn_trn_tcomposemailattachement " +
                    " WHERE composemail_gid = '" + lsemail_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = row["document_path"].ToString(),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();



        }

        public result DaPostWoktItemIncharge(MdlAssignTo values, string employee_gid, string user_gid)
        {
            msSQL = " UPDATE isn_trn_tmaildetails SET" +
                   " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                   " WHERE email_gid='" + values.email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " SELECT group_concat(employee_mailid) as employee_emailid FROM ocs_trn_tmailcclist WHERE mailtrigger_function = 'IAssign-CC Mail' ";
            string lsmailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select email_subject from isn_trn_treferencemail where email_gid='" + values.email_gid + "'";
            string lssubject = objdbconn.GetExecuteScalar(msSQL);

            if (values.employee_gid == null)
            {
                msSQL = " update isn_trn_tworkitemassign set" +
                      " checkeremployee_gid='" + employee_gid + "'," +
                      " checkeremployee_name=( SELECT concat(a.user_firstname, ' ', a.user_lastname, '/', a.user_code) as employee_name from adm_mst_tuser a " +
                      " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid WHERE b.employee_gid='" + employee_gid + "')," +
                      " checkeremployee_mailid=(select employee_emailid from hrm_mst_temployee where employee_gid = '" + employee_gid + "')," +
                      " allottedby_name=(SELECT concat(user_firstname, ' ', user_lastname, '/', user_code) as employee_name from adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                      " allotted_on=current_timestamp," +
                      " updated_by='" + user_gid + "'," +
                      " updated_date=current_timestamp" +
                      " where email_gid='" + values.email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {

                msSQL = " SELECT checkeremployee_name,checkeremployee_gid" +
                        " FROM isn_trn_tworkitemassign" +
                        " WHERE email_gid='" + values.email_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    string lscheckeremployee_name = string.Empty;
                    string lscheckeremployee_gid = string.Empty;


                    lscheckeremployee_name = objODBCDataReader["checkeremployee_name"].ToString();
                    lscheckeremployee_gid = objODBCDataReader["checkeremployee_gid"].ToString();

                    objODBCDataReader.Close();

                    msGetGID = objcmnfunctions.GetMasterGID("TRAN");
                    if (lscheckeremployee_name != "")
                    {
                        msSQL = " INSERT INTO isn_trn_ttransferlog(" +
                                              " transferlog_gid," +
                                              " email_gid," +
                                              " assignedto_name," +
                                              " assignedto_gid," +
                                              " transferby_gid," +
                                              " transferby_name," +
                                              " created_by)" +
                                              " VALUES(" +
                                              "'" + msGetGID + "'," +
                                              "'" + values.email_gid + "'," +
                                              "'" + lscheckeremployee_name + "'," +
                                              "'" + lscheckeremployee_name + "'," +
                                              "'" + values.employee_gid + "'," +
                                              "'" + values.employee_name + "'," +
                                              "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " INSERT INTO isn_trn_tauditlog(" +
                                " email_gid," +
                                " action_taken," +
                                " created_by)" +
                                " VALUES(" +
                                "'" + values.email_gid + "'," +
                                "'Transfer'," +
                                "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = "SELECT workitemref_no FROM isn_trn_tmaildetails WHERE email_gid = '" + values.email_gid + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Read();
                            lsworkref_no = objODBCDataReader["workitemref_no"].ToString();
                        }
                        objODBCDataReader.Close();

                        msSQL = "SELECT employee_emailid, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                             " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                            " WHERE employee_gid = '" + values.employee_gid + "' ";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Read();
                            lsto_address = objODBCDataReader["employee_emailid"].ToString();
                        }
                        objODBCDataReader.Close();

                        msSQL = "SELECT employee_emailid, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                           " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                          " WHERE employee_gid = '" + employee_gid + "' ";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Read();
                            lstransfer_by = objODBCDataReader["created_by"].ToString().Split('/');
                        }
                        objODBCDataReader.Close();

                        lsmailbody = "Dear " + values.employee_name + ",  <br />";
                        lsmailbody = lsmailbody + "<br />";
                        lsmailbody = lsmailbody + " The Work Item " + lsworkref_no + " has been transferred by " + lstransfer_by[0] + ".";
                        lsmailbody = lsmailbody + "<br/>";

                        lsmailbody = lsmailbody + "<br/>";
                        lsmailbody = lsmailbody + "Thanks & Regards,";
                        lsmailbody = lsmailbody + "<br />";
                        lsmailbody = lsmailbody + "Credit Team ";
                        lsmailbody = lsmailbody + "<br />";

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                               " FROM isn_trn_tmailcredentials" +
                               " WHERE 1=1";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == true)
                        {
                            ls_server = objODBCDataReader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDataReader["pop_port"]);
                            ls_username = objODBCDataReader["pop_username"].ToString();
                            ls_password = objODBCDataReader["pop_password"].ToString();
                        }
                        objODBCDataReader.Close();

                        int MailFlag;
                        MailFlag = objcmnfunctions.SendSMTP2(ls_username, ls_password, lsto_address, lssubject, lsmailbody, lsmailid, "", "");
                        if (MailFlag == 1)
                        {
                            msGetGID1 = objcmnfunctions.GetMasterGID("MAIL");
                            msSQL = " INSERT INTO isn_trn_tsentmail(" +
                                " sentmail_gid," +
                               " decision_gid," +
                               " decision," +
                               " email_gid," +
                               " email_subject," +
                               " frommail_id," +
                               " tomail_id," +
                               " ccmail_id," +
                               " bccmail_id," +
                               " mailcontent," +
                               " attachment_flag," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGID1 + "'," +
                               "'" + msGetGID + "'," +
                               "'Transfer'," +
                               "'" + values.email_gid + "'," +
                               "'" + lssubject + "'," +
                               "'" + ls_username + "'," +
                               "'" + lsto_address + "'," +
                               "'" + lscc_address + "'," +
                               "'" + lsbcc_address + "'," +
                               "'" + lsmailbody.Replace("'", "") + "'," +
                               "'Y'," +
                               "current_timestamp," +
                               "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                objResult.status = true;
                                objResult.message = "Work Item Transferred Successfully";

                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error Occured";
                            }
                        }

                    }
                    else
                    {
                        if (values.assign_remarks == "" || values.assign_remarks == null)
                        {
                            lsassign_remarks = "";
                        }
                        else
                        {
                            lsassign_remarks = values.assign_remarks;
                        }

                        msSQL = " update isn_trn_tworkitemassign set" +
                            " assigned_remarks='" + lsassign_remarks.Replace("'", "") + "'" +
                            " where email_gid='" + values.email_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " INSERT INTO isn_trn_tauditlog(" +
                              " email_gid," +
                              " action_taken," +
                              " created_by)" +
                              " VALUES(" +
                              "'" + values.email_gid + "'," +
                              "'Assign'," +
                              "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                             " FROM isn_trn_tmailcredentials" +
                             " WHERE 1=1";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == true)
                        {
                            ls_server = objODBCDataReader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDataReader["pop_port"]);
                            ls_username = objODBCDataReader["pop_username"].ToString();
                            ls_password = objODBCDataReader["pop_password"].ToString();
                        }
                        objODBCDataReader.Close();

                        msSQL = "SELECT workitemref_no FROM isn_trn_tmaildetails WHERE email_gid = '" + values.email_gid + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Read();
                            lsworkref_no = objODBCDataReader["workitemref_no"].ToString();
                        }
                        objODBCDataReader.Close();

                        msSQL = "SELECT employee_emailid, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                             " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                            " WHERE employee_gid = '" + values.employee_gid + "' ";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Read();
                            lsto_address = objODBCDataReader["employee_emailid"].ToString();
                        }
                        objODBCDataReader.Close();

                        msSQL = "SELECT employee_emailid, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                            " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                           " WHERE employee_gid = '" + employee_gid + "' ";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows)
                        {
                            objODBCDataReader.Read();
                            lsassign_by = objODBCDataReader["created_by"].ToString().Split('/');
                        }
                        objODBCDataReader.Close();

                        lsmailbody = "Dear " + values.employee_name + ",  <br />";
                        lsmailbody = lsmailbody + "<br />";
                        lsmailbody = lsmailbody + " The Work Item " + lsworkref_no + " has been Assigned by " + lsassign_by[0] + ".";
                        lsmailbody = lsmailbody + "<br/>";
                        lsmailbody = lsmailbody + " Remarks : " + lsassign_remarks.Replace("'", "") + "<br/>";
                        lsmailbody = lsmailbody + "<br/>";
                        lsmailbody = lsmailbody + "Thanks & Regards,";
                        lsmailbody = lsmailbody + "<br />";
                        lsmailbody = lsmailbody + "Credit Team ";
                        lsmailbody = lsmailbody + "<br />";

                        int MailFlag;
                        MailFlag = objcmnfunctions.SendSMTP2(ls_username, ls_password, lsto_address, lssubject, lsmailbody, lsmailid, "", "");
                        if (MailFlag == 1)
                        {
                            msGetGID1 = objcmnfunctions.GetMasterGID("MAIL");
                            msSQL = " INSERT INTO isn_trn_tsentmail(" +
                                " sentmail_gid," +
                               " decision_gid," +
                               " decision," +
                               " email_gid," +
                               " email_subject," +
                               " frommail_id," +
                               " tomail_id," +
                               " ccmail_id," +
                               " bccmail_id," +
                               " mailcontent," +
                               " assign_remarks," +
                               " attachment_flag," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGID1 + "'," +
                               "'" + msGetGID + "'," +
                               "'Assign'," +
                               "'" + values.email_gid + "'," +
                               "'" + lssubject + "'," +
                               "'" + ls_username + "'," +
                               "'" + lsto_address + "'," +
                               "'" + lscc_address + "'," +
                               "'" + lsbcc_address + "'," +
                               "'" + lsmailbody.Replace("'", "") + "'," +
                               "'" + lsassign_remarks.Replace("'", "") + "'," +
                               "'Y'," +
                               "current_timestamp," +
                               "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                objResult.status = true;
                                objResult.message = "Work Item Assigned Successfully";

                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error Occured";
                            }
                        }
                      
                    }
                }
                else
                {
                    objODBCDataReader.Close();
                }

                if (values.zone_flag == "N")
                {
                    msSQL = " SELECT from_mailaddress FROM isn_trn_tmaildetails" +
                            " where email_gid='" + values.email_gid + "'";
                    string lsrmmail_id = objdbconn.GetExecuteScalar(msSQL);
                    string lsrmemployee_gid = string.Empty;
                    string lsrmemployee_name = string.Empty;

                    msSQL = " SELECT concat(a.user_firstname, ' ', a.user_lastname, '/', a.user_code) as employee_name,b.employee_gid" +
                        " from adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid WHERE b.employee_emailid='" + lsrmmail_id + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lsrmemployee_gid = objODBCDataReader["employee_gid"].ToString();
                        lsrmemployee_name = objODBCDataReader["employee_name"].ToString();
                        objODBCDataReader.Close();

                        msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                        msSQL = " INSERT INTO isn_mst_temployeelist(" +
                                " employeelist_gid," +
                                " zone_gid ," +
                                " employee_gid ," +
                                " employee_name ," +
                                " employee_emailid ," +
                                " employee_type ," +
                                " created_by ," +
                                " created_date )" +
                                " VALUES(" +
                                "'" + msGetChildGid + "'," +
                                "'" + values.zone_gid + "'," +
                                "'" + lsrmemployee_gid + "'," +
                                "'" + lsrmemployee_name + "'," +
                                "'" + lsrmmail_id + "'," +
                                "'RM'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT email_gid FROM isn_trn_tmaildetails" +
                               " WHERE from_mailaddress='" + lsrmmail_id + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt_datatable.Rows)
                            {
                                msSQL = " SELECT workitemassign_gid FROM isn_trn_tworkitemassign " +
                                       " WHERE email_gid='" + dr["email_gid"].ToString() + "'";
                                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDataReader.HasRows == false)
                                {
                                    objODBCDataReader.Close();

                                    msGetGID = objcmnfunctions.GetMasterGID("T2EM");
                                    msSQL = " insert into isn_trn_tworkitemassign(" +
                                     " workitemassign_gid," +
                                     " email_gid," +
                                     " zone_gid," +
                                     " zone_name," +
                                     " rmemployee_gid," +
                                     " rmemployee_name," +
                                     " rmemployee_mailid," +
                                     " allottedby_name," +
                                     " allotted_on," +
                                     " assigned_remarks," +
                                     " created_by)" +
                                     " values(" +
                                     "'" + msGetGID + "'," +
                                     "'" + dr["email_gid"].ToString() + "'," +
                                     "'" + values.zone_gid + "'," +
                                     "'" + values.zone_name.Replace("'","\\'") + "'," +
                                     "'" + lsrmemployee_gid + "'," +
                                     "'" + lsrmemployee_name + "'," +
                                     "'" + lsrmmail_id + "'," +
                                     " (SELECT concat(user_firstname, ' ', user_lastname, '/', user_code) as employee_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                                     " current_timestamp," +
                                     "'" + lsassign_remarks.Replace("'", "") + "'," +
                                     "'" + user_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                objODBCDataReader.Close();

                            }
                        }
                        dt_datatable.Dispose();
                    }
                    else
                    {
                        objODBCDataReader.Close();
                        objResult.status = true;
                        objResult.message = "RM Mail ID Not In Employee Master.Please Check";
                        return objResult;
                    }
                }

                msSQL = " update isn_trn_tworkitemassign set" +
                              " checkeremployee_gid='" + values.employee_gid + "'," +
                              " checkeremployee_name='" + values.employee_name + "'," +
                              " checkeremployee_mailid=(select employee_emailid from hrm_mst_temployee where employee_gid = '" + values.employee_gid + "')," +
                              " allottedby_name=(SELECT concat(user_firstname, ' ', user_lastname, '/', user_code) as employee_name from adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                              " allotted_on=current_timestamp," +
                              " updated_by='" + user_gid + "'," +
                              " updated_date=current_timestamp" +
                              " where email_gid='" + values.email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            return objResult;
        }

        public void DaGetDecisionHistory(MdlDecisionhistorySummary values, string lsemail_gid)
        {
            msSQL = " select a.decision_gid,a.decision, a.remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by" +
                    " from isn_trn_tworkitemdecision a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " where  a.email_gid='" + lsemail_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlDecisionhistory = dt_datatable.AsEnumerable().Select(row => new MdlDecisionhistory
                {
                    decision_gid = row["decision_gid"].ToString(),
                    decision = row["decision"].ToString(),
                    remarks = row["remarks"].ToString(),
                    created_date = row["created_date"].ToString(),
                    created_by = row["created_by"].ToString()


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

        public result DaPostDecision(MdlDecisionhistory values, string user_gid)
        {
            msSQL = " UPDATE isn_trn_tmaildetails SET" +
                    " status='" + values.decision + "'," +
                    " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                    " WHERE email_gid='" + values.email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGID = objcmnfunctions.GetMasterGID("DESC");
            if (values.decision == "Close")
            {
                if (values.close_acknowledge == "" || values.close_acknowledge == null)
                {
                    values.close_acknowledge = "N";
                }
                msSQL = " INSERT INTO isn_trn_tworkitemdecision(" +
                  " decision_gid," +
                  " decision," +
                  " email_gid," +
                  " remarks," +
                  "close_acknowledge," +
                  " created_date," +
                  " created_by)" +
                  " VALUES(" +
                  "'" + msGetGID + "'," +
                  "'" + values.decision + "'," +
                  "'" + values.email_gid + "'," +
                  "'" + values.remarks.Replace("'", "") + "'," +
                  "'" + values.close_acknowledge + "'," +
                  "current_timestamp," +
                  "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " INSERT INTO isn_trn_tworkitemdecision(" +
                                  " decision_gid," +
                                  " decision," +
                                  " email_gid," +
                                  " remarks," +
                                  " created_date," +
                                  " created_by)" +
                                  " VALUES(" +
                                  "'" + msGetGID + "'," +
                                  "'" + values.decision + "'," +
                                  "'" + values.email_gid + "'," +
                                  "'" + values.mailcontent.Replace("'", "") + "'," +
                                  "current_timestamp," +
                                  "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                msSQL = " INSERT INTO isn_trn_tauditlog(" +
                             " email_gid," +
                             " action_taken," +
                             " created_by)" +
                             " VALUES(" +
                             "'" + values.email_gid + "'," +
                             "'" + values.decision + "'," +
                             "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.decision == "Pushback" || values.decision == "Forward")
                {
                    msSQL = "SELECT workitemref_no FROM isn_trn_tmaildetails WHERE email_gid = '" + values.email_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lsworkref_no = objODBCDataReader["workitemref_no"].ToString();
                    }
                    objODBCDataReader.Close();


                    msSQL = "SELECT concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                       " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                       "  WHERE employee_emailid = '" + values.tomail_id + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lstomail = objODBCDataReader["created_by"].ToString().Split('/');
                    }
                    else
                    {
                        
                    }
                    objODBCDataReader.Close();

                    lsmailbody = lsmailbody + "<br/>";
                    lsmailbody = lsmailbody + "<br/>";
                    lsmailbody = lsmailbody +  values.mailcontent.Replace("'", "") + "<br/>";

                    lsmailbody = lsmailbody + "<br/>";
                    msSQL = "select email_subject from isn_trn_treferencemail where email_gid='" + values.email_gid + "'";
                    string lssubject = objdbconn.GetExecuteScalar(msSQL);

                    lsstatus = objSentMail.DaIasnReplyMail(values.email_gid, values.decision_gid, values.decision, values.message_id, values.reference_id, values.tomail_id, values.ccmail_id, values.bccmail_id, user_gid, lssubject, lsmailbody);
                    if (lsstatus == true && values.decision == "Pushback")
                    {
                        msSQL = " DELETE FROM isn_tmp_tmaildetailsattachement" +
                           " WHERE created_by='" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        objResult.status = true;
                        objResult.message = "Mail Pushbacked Successfully";
                    }
                    else if (lsstatus == true && values.decision == "Forward")
                    {
                        msSQL = " DELETE FROM isn_tmp_tmaildetailsattachement" +
                           " WHERE created_by='" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        objResult.status = true;
                        objResult.message = "Mail Forwarded Successfully";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error Occured While Mail Sent";
                    }
                }
                else if (values.decision == "Close")
                {
                    msSQL = " SELECT  group_concat(DISTINCT a.email_to SEPARATOR';') AS 'to_address'," +
                            " group_concat(DISTINCT a.email_cc SEPARATOR';') AS 'cc_address'," +
                            " group_concat(DISTINCT a.email_bcc SEPARATOR';') AS 'bcc_address',a.email_subject," +
                            " a.message_id,group_concat(a.reference_id separator ' ') AS reference_id" +
                            " FROM isn_trn_treferencemail a" +
                            " WHERE  email_gid='" + values.email_gid + "'" +
                            " ORDER BY created_date ";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lsto_address = objODBCDataReader["to_address"].ToString();
                        lscc_address = objODBCDataReader["cc_address"].ToString();
                        lsbcc_address = objODBCDataReader["bcc_address"].ToString();
                        lsmessage_id = objODBCDataReader["message_id"].ToString();
                        lsreference_id = objODBCDataReader["reference_id"].ToString();
                        lssubject = objODBCDataReader["email_subject"].ToString();
                    }
                    objODBCDataReader.Close();
                    msSQL = "SELECT workitemref_no FROM isn_trn_tmaildetails WHERE email_gid = '" + values.email_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lsworkref_no = objODBCDataReader["workitemref_no"].ToString();
                    }
                    objODBCDataReader.Close();

                    msSQL = "SELECT rmemployee_name, checkeremployee_name FROM isn_trn_tworkitemassign WHERE email_gid = '" + values.email_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lsrmemployee_name = objODBCDataReader["rmemployee_name"].ToString();
                        lsrmname = objODBCDataReader["rmemployee_name"].ToString().Split('/');
                        lsmailbody = "Hi " + lsrmname[0] + ",  <br />";
                    }
                    else
                    {
                        lsmailbody = "Hi, <br />";
                    }
                    objODBCDataReader.Close();

                    msSQL = "SELECT concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                        " FROM isn_trn_tworkitemdecision a left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        "  WHERE decision='Close' and email_gid = '" + values.email_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lsclosed_by = objODBCDataReader["created_by"].ToString();
                        lsclosedby = objODBCDataReader["created_by"].ToString().Split('/');
                    }
                    objODBCDataReader.Close();

                    if (values.close_acknowledge == "Y")
                    {

                        sub = " Work Item Closed ";

                        lsmailbody = lsmailbody + "<br />";
                        lsmailbody = lsmailbody + " Work Item with Ref.No. " + HttpUtility.HtmlEncode(lsworkref_no) + " Closed with following remarks.";
                        lsmailbody = lsmailbody + "<br/>";
                        lsmailbody = lsmailbody + " Remarks :" + HttpUtility.HtmlEncode(values.remarks.Replace("'", "")) + "<br/>";

                        lsmailbody = lsmailbody + "<br/>";
                        lsstatus = objSentMail.DaIasnReplyMail(values.email_gid, values.email_gid, "Close", lsmessage_id, lsreference_id, lsto_address, lscc_address, lsbcc_address, user_gid, lssubject, lsmailbody);

                        if (lsstatus == false)
                        {
                            objResult.status = false;
                            objResult.message = "Error Occured While Senting Mail";
                        }
                        else
                        {
                            objResult.status = true;
                            objResult.message = "Work Item Closed Successfully";
                        }
                    }
                    else
                    {
                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password FROM isn_trn_tmailcredentials WHERE 1=1";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == true)
                        {
                            objODBCDataReader.Read();
                            ls_server = objODBCDataReader["pop_server"].ToString();
                            ls_port = Convert.ToInt16(objODBCDataReader["pop_port"].ToString());
                            ls_username = objODBCDataReader["pop_username"].ToString();
                            ls_password = objODBCDataReader["pop_password"].ToString();
                        }
                        objODBCDataReader.Close();
                        msGetGID = objcmnfunctions.GetMasterGID("MAIL");
                        msSQL = " INSERT INTO isn_trn_tsentmail(" +
                       " sentmail_gid," +
                      " decision_gid," +
                      " decision," +
                      " email_gid," +
                      " email_subject," +
                      " frommail_id," +
                      " tomail_id," +
                      " ccmail_id," +
                      " bccmail_id," +
                      " mailcontent," +
                      " attachment_flag," +
                      " created_date," +
                      " created_by)" +
                      " VALUES(" +
                      "'" + msGetGID + "'," +
                      "'" + values.decision_gid + "'," +
                      "'" + values.decision + "'," +
                      "'" + values.email_gid + "'," +
                      "'" + lssubject + "'," +
                      "'" + ls_username + "'," +
                      "'" + values.tomail_id + "'," +
                      "'" + values.ccmail_id + "'," +
                      "'" + values.bccmail_id + "'," +
                      "'" + values.remarks.Replace("'", "") + "'," +
                      "'" + lsattachment_flag + "'," +
                      "current_timestamp," +
                      "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            objResult.status = true;
                            objResult.message = "Work Item Closed Successfully";
                        }
                        else
                        {
                            objResult.status = false;
                            objResult.message = "Error Occured While Inserting the data";
                        }
                    }

                }
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured While Inserting Data";
            }

            return objResult;


        }

        public result DaPostUploadAttchment(HttpRequest httpRequest, string employee_gid, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string servicerequest_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IASN/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IASN/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            objResult.status = false;
                        }
                        else
                        {
                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IASN/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "IASN/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IASN/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                            //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();
                            //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IASN/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msSQL = " insert into isn_tmp_tmaildetailsattachement( " +
                                        " document_name ," +
                                        " document_path," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
                                        "'" + httpPostedFile.FileName + "'," +
                                        "'" + lspath + msdocument_gid + FileExtension + "'," +
                                        "'" + user_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Document Uploaded Successfully";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error Occured";
                            }
                        }


                        

                    }
                    return objResult;

                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured";

                    return objResult;
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;

                return objResult;
            }
        }

        public void DaGetMailAttachmentDoc(MdlcDocList objfileDtls, string user_gid)
        {

            msSQL = " SELECT mailattachment_gid, document_name,document_path FROM isn_tmp_tmaildetailsattachement WHERE created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlDocDetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlDocDetails
                    {
                        id = dt["mailattachment_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),

                        document_path = dt["document_path"].ToString(),

                    });
                }
                objfileDtls.MdlDocDetails = getDocList;
            }
            dt_datatable.Dispose();
        }


        public result DaPostArchival(string user_gid, MdlArchival values)
        {
            foreach (string i in values.email_gid)
            {
                msSQL = " UPDATE isn_trn_tmaildetails SET" +
                        " status='Archival'," +
                        " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                        " WHERE email_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " UPDATE isn_trn_tcomposemail SET" +
                       " status='Archival'," +
                       " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                       " WHERE composemail_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.archival_type == "" || values.archival_type == null)
                {
                    values.archival_type = "Customer";
                }
                msGetGID = objcmnfunctions.GetMasterGID("DESC");
                msSQL = " INSERT INTO isn_trn_tworkitemdecision(" +
                        " decision_gid," +
                        " decision," +
                        " archival_type," +
                        " email_gid," +
                        " remarks," +
                        " customer_gid," +
                        " customer_name," +
                        " customer2sanction_gid," +
                        " sanctionref_no," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'Archival'," +
                        "'" + values.archival_type + "'," +
                        "'" + i + "'," +
                        "'" + values.remarks.Replace("'", "") + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + values.customer_name + "'," +
                        "'" + values.sanction_gid + "'," +
                        "'" + values.sanctionref_no + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " INSERT INTO isn_trn_tauditlog(" +
                           " email_gid," +
                           " action_taken," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + i + "'," +
                           "'Archival'," +
                           "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = " Work items Archived Successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
            return objResult;

        }

        public void DaGetArchivalCounts(string employee_gid, string user_gid, mdlarchivalcount values)
        {

            msSQL = "SELECT COUNT(a.status) as workitem_count FROM isn_trn_tmaildetails a" +
                 " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                 " WHERE 1 = 1 AND a.status<>'Close' AND a.status<>'Archival' AND a.reference_flag='Y' and a.history_flag='N'";
            string lsarchival_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT COUNT(*) as WorkitemCount FROM isn_trn_tcomposemail WHERE status <> 'Archival' ";
            string lsarchival1_count = objdbconn.GetExecuteScalar(msSQL);

            int lsarchival = Convert.ToInt32(lsarchival_count);
            int lsarchival1 = Convert.ToInt32(lsarchival1_count);
            string lspending = (lsarchival + lsarchival1).ToString();
            
            values.workitem_count = lspending;
            
            msSQL = " SELECT COUNT(distinct (a.customer_gid)) as archivalcustomer_count" +
                   " FROM isn_trn_tworkitemdecision a" +
                   " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " WHERE decision = 'Archival' AND archival_type = 'Customer' AND a.history_flag='N'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.archivalcustomer_count = objODBCDataReader["archivalcustomer_count"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = " SELECT COUNT(distinct (a.customer_gid)) as archivalspecific_count" +
                  " FROM isn_trn_tworkitemdecision a" +
                  " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                  " WHERE decision = 'Archival' AND archival_type = 'Specific' AND a.history_flag='N'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.archivalspecific_count = objODBCDataReader["archivalspecific_count"].ToString();
            }
            objODBCDataReader.Close();

        }

        public void DaGetCustomerArchival(MdlArchivalCustomerList values)
        {
            msSQL = " SELECT a.customer_gid, concat(b.customer_urn,'/',b.customername) as customer_name," +
                    " b.vertical_code,a.remarks," +
                    " (SELECT COUNT(*) FROM isn_trn_tworkitemdecision x WHERE x.customer_gid = a.customer_gid AND  decision = 'Archival' AND archival_type = 'Customer' AND x.history_flag='N' ) as workitem_count" +
                    " FROM isn_trn_tworkitemdecision a" +
                    " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                    " WHERE decision = 'Archival' AND archival_type = 'Customer' and a.history_flag='N' GROUP BY a.customer_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlArchivalCustomer = dt_datatable.AsEnumerable().Select(row => new MdlArchivalCustomer
                {

                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    vertical_code = row["vertical_code"].ToString(),
                    no_of_workitem = row["workitem_count"].ToString(),
                    remarks = row["remarks"].ToString()

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

        public void DaGetSpecificArchival(MdlArchivalCustomerList values)
        {
            msSQL = " SELECT a.customer_gid, concat(b.customer_urn,'/',b.customername) as customer_name,a.sanctionref_no," +
                    " b.vertical_code,a.remarks," +
                    " (SELECT COUNT(*) FROM isn_trn_tworkitemdecision x WHERE x.customer_gid = a.customer_gid AND  decision = 'Archival' AND archival_type = 'Specific' AND x.history_flag='N') as workitem_count" +
                    " FROM isn_trn_tworkitemdecision a" +
                    " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                    " WHERE decision = 'Archival' AND archival_type = 'Specific' and a.history_flag='N' GROUP BY a.customer_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlArchivalCustomer = dt_datatable.AsEnumerable().Select(row => new MdlArchivalCustomer
                {

                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    vertical_code = row["vertical_code"].ToString(),
                    no_of_workitem = row["workitem_count"].ToString(),
                    sanctionref_no = row["sanctionref_no"].ToString(),
                    remarks = row["remarks"].ToString()

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

        public void DaGetTransferLog(string lsemail_gid, MdlTransferLogList values)
        {
            msSQL = " SELECT a.transferlog_gid,a.assignedto_name,a.transferby_name," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(b.user_firstname, b.user_lastname, '/', b.user_code) as created_by" +
                    " FROM isn_trn_ttransferlog a" +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by" +
                    " WHERE a.email_gid = '" + lsemail_gid + "' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlTransferLog = dt_datatable.AsEnumerable().Select(row => new Models.MdlTransferLog
                {
                    transferlog_gid = row["transferlog_gid"].ToString(),
                    assignedby_name = row["assignedto_name"].ToString(),
                    transferby_name = row["transferby_name"].ToString(),
                    created_date = row["created_date"].ToString(),
                    created_by = row["created_by"].ToString()
                }).ToList();
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }

        }

        public result DaPostMailAttchmentDelete(string id)
        {
            msSQL = " DELETE FROM isn_tmp_tmaildetailsattachement WHERE mailattachment_gid='" + id + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Mail Attachment Deleted successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
            return objResult;
        }

        public void DaGetEmployeeMailId(string employee_gid, MdlMailID values)
        {
            msSQL = " SELECT employee_emailid FROM hrm_mst_temployee WHERE employee_gid='" + employee_gid + "'";
            values.employee_emailid = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetMergedWorkItemview(string lsemail_gid, WorkItemList values)
        {

            string lsteam_gid = string.Empty;

            msSQL = " SELECT " +
                    " workitemref_no, email_gid, email_from,from_mailaddress,date_format(email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " email_subject, email_content,  status_attachment,cc, bcc, message_id, " +
                    " email_to" +
                    " FROM isn_trn_tmergedworkitem" +
                    " WHERE email_gid = '" + lsemail_gid + "'; ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new Models.MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status_attachment = row["status_attachment"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    message_id = row["message_id"].ToString(),
                    email_to = row["email_to"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),

                }).ToList();
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }
            dt_datatable.Dispose();
        }

        public void EmployeeProfile(string employee_gid, MdlProfile values)
        {
            try
            {
                msSQL = " SELECT a.user_code, CONCAT(a.user_firstname,' ',a.user_lastname) as user_name,d.employee_photo ,b.designation_name,c.department_name,d.employee_mobileno FROM hrm_mst_temployee d " +
                       " LEFT JOIN adm_mst_tuser a ON a.user_gid = d.user_gid " +
                       " LEFT JOIN adm_mst_tdesignation b ON b.designation_gid = d.designation_gid " +
                       " LEFT JOIN hrm_mst_tdepartment c ON c.department_gid = d.department_gid WHERE d.employee_gid = '" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();
                    values.user_code = objODBCDataReader["user_code"].ToString();
                    values.user_name = objODBCDataReader["user_name"].ToString();
                    if (objODBCDataReader["employee_photo"].ToString() != "")
                    {
                        values.user_photo = objODBCDataReader["employee_photo"].ToString();
                    }
                    else
                    {
                        values.user_photo = "N";
                    }

                    values.user_designation = objODBCDataReader["designation_name"].ToString();
                    values.user_department = objODBCDataReader["department_name"].ToString();
                    values.user_mobileno = objODBCDataReader["employee_mobileno"].ToString();
                    values.message = "success";
                    values.status = true;
                }
                else
                {
                    values.message = "failure";
                    values.status = false;
                }
                objODBCDataReader.Close();
            }
            catch
            {
                values.message = "error";
                values.status = false;
            }
        }

        public void PostEmailSeen(string email_gid)
        {
            msSQL = " UPDATE isn_trn_tmaildetails SET" +
                   " seen_flag='Y'" +
                   " WHERE email_gid='" + email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaGetReferenceMail(string email_gid, MdlReferenceMailList values)
        {

            msSQL = "  SELECT reference_gid,'' as status,email_gid,email_content,email_subject,email_from,email_to,email_cc,email_bcc," +
                    " date_format(email_date, '%d-%m-%Y %h:%i %p') as email_date," +
                    "  message_number,message_id ,reference_id" +
                    " FROM isn_trn_treferencemail" +
                    "  WHERE 1 = 1 AND email_gid='" + email_gid + "' GROUP BY email_gid " +
                    "  UNION" +
                    " SELECT a.sentmail_gid as reference_gid,a.decision as status,a.email_gid,a.mailcontent,a.email_subject, " +
                    " a.frommail_id,a.tomail_id,a.ccmail_id,a.bccmail_id," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as email_date ," +
                    " '' as message_number,'' as message_id ,'' as reference_id" +
                    " FROM isn_trn_tsentmail a" +
                    " WHERE   email_gid='" + email_gid + "' and (a.decision <> 'Transfer') and (a.decision <> 'Assign') "+
                    " UNION "+
                    " SELECT b.composesentmail_gid as reference_gid, b.decision as status, b.composemail_gid, b.mailcontent, b.email_subject, " +
                    " b.frommail_id, b.tomail_id, b.ccmail_id, b.bccmail_id," +
                    " date_format(b.created_date, '%d-%m-%Y %h:%i %p') as email_date, '-', '-', '-' " +
                    " FROM isn_trn_tcomposesentmail b " +
                    " WHERE   composemail_gid='" + email_gid + "' order by email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                var getDocList = new List<MdlReferenceMail>();
                foreach (DataRow dt in dt_datatable.Rows)
                {


                    msSQL = " SELECT mailattachment_gid,document_path,document_name," +
                        " substring_index(document_name,'.',-1) as document_extension" +
                        " FROM isn_trn_tmaildetailsattachement " +
                        " WHERE email_gid = '" + dt["reference_gid"].ToString() + "'; ";
                    dt_child = objdbconn.GetDataTable(msSQL);
                    var attachmentlist = new List<MdlAttachmentList>();
                    if (dt_child.Rows.Count != 0)
                    {

                        attachmentlist = dt_child.AsEnumerable().Select(row => new MdlAttachmentList
                        {
                            mailattachment_gid = row["mailattachment_gid"].ToString(),
                            document_name = row["document_name"].ToString(),
                            document_path = HttpContext.Current.Server.MapPath(row["document_path"].ToString()),
                            document_extension = row["document_extension"].ToString()
                        }).ToList();

                    }
                    dt_child.Dispose();


                    getDocList.Add(new MdlReferenceMail
                    {
                        email_gid = dt["email_gid"].ToString(),
                        email_from = dt["email_from"].ToString(),
                        email_date = dt["email_date"].ToString(),
                        email_subject = dt["email_subject"].ToString().Split('>'),
                        email_content = dt["email_content"].ToString(),
                        reference_gid = dt["reference_gid"].ToString(),
                        email_to = dt["email_to"].ToString(),
                        email_cc = dt["email_cc"].ToString(),
                        email_bcc = dt["email_bcc"].ToString(),
                        message_id = dt["message_id"].ToString(),
                        MdlAttachmentList = attachmentlist,

                    });
                    values.MdlReferenceMail = getDocList;

                }

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();


        }
        public void DaGetDecisionHistoryMail(MdlDecisionhistorySummary values, string lsemail_gid)
        {
            msSQL = " select a.decision_gid,a.decision,a.email_gid,a.mailcontent,a.email_subject," +
                    " a.frommail_id,a.tomail_id,a.ccmail_id,a.bccmail_id," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from isn_trn_tsentmail a" +
                    " where  a.email_gid='" + lsemail_gid + "' and a.decision in ('Pushback','Forward')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlDecisionhistory = dt_datatable.AsEnumerable().Select(row => new MdlDecisionhistory
                {
                    decision_gid = row["decision_gid"].ToString(),
                    decision = row["decision"].ToString(),
                    email_gid = row["email_gid"].ToString(),
                    mailcontent = row["mailcontent"].ToString(),
                    created_date = row["created_date"].ToString(),
                    subject = row["email_subject"].ToString(),
                    frommail_id = row["frommail_id"].ToString(),
                    tomail_id = row["tomail_id"].ToString(),
                    ccmail_id = row["ccmail_id"].ToString(),
                    bccmail_id = row["bccmail_id"].ToString()

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


        public void DaGetEmployee(string employee_gid, MdlIsnEmployeelist values)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid" +
                      " from adm_mst_tuser a " +
                     " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                     " left join hrm_mst_tdepartment c on b.department_gid=c.department_gid" +
                     " where user_status<>'N' and b.employee_gid <> '" + employee_gid + "' and b.department_gid='HDPM1811210068' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<MdlIsnEmployee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlIsnEmployee = dt_datatable.AsEnumerable().Select(row =>
                      new MdlIsnEmployee
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }


        }

        public void DaGetconsolidatedWorkItem(WorkItemList values)
        {
            msSQL = " (SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date, b.zone_name, b.zone_gid," +
                    " b.rmemployee_gid, b.rmemployee_name," +
                    " concat(c.user_firstname,'',c.user_lastname,'/',c.user_code ) as Statusupdated_by, a.updated_date," +
                    " a.status FROM isn_trn_tmaildetails a" +
                    " INNER JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    " INNER JOIN adm_mst_tuser c ON c.user_gid = a.updated_by " +
                    " WHERE 1 = 1 AND a.reference_flag='Y' AND a.history_flag='N' GROUP BY a.email_gid )" +
                    " UNION " +
                    " (SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.composemail_gid, a.composemail_refno, a.frommail_id, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject, a.mailcontent, date_format(a.created_date, '%d-%m-%Y') as created_date,d.zone_name, d.zone_gid," +
                    " c.employee_gid,concat(b.user_firstname,'',b.user_lastname,'/',b.user_code ) as rmemployee_name, " +
                    " concat(e.user_firstname, '', e.user_lastname, '/', e.user_code) as Statusupdated_by, a.updated_date, " +
                    " case when a.status='Pending' then 'Sent Mail' when a.status='Forward' then 'Sent Mail' else a.status end as status" +
                    " FROM isn_trn_tcomposemail a" +
                    " INNER JOIN adm_mst_tuser b on b.user_gid = a.created_by " +
                    " INNER JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                    " INNER JOIN isn_mst_temployeelist d on d.employee_gid = c.employee_gid " +
                    " INNER JOIN adm_mst_tuser e on e.user_gid = a.updated_by " +
                    " WHERE 1=1 GROUP BY a.composemail_gid )";
            
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
                    zone_name = row["zone_name"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    status = row["status"].ToString(),
                    Statusupdated_by = row["Statusupdated_by"].ToString(),
                    Statusupdated_date = row["updated_date"].ToString(),

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

        public result DaPostAssignZone(MdlAssignTo values, string employee_gid, string user_gid)
        {

            msSQL = " SELECT from_mailaddress FROM isn_trn_tmaildetails" +
                    " where email_gid='" + values.email_gid + "'";
            string lsrmmail_id = objdbconn.GetExecuteScalar(msSQL);
            string lsrmemployee_gid = string.Empty;
            string lsrmemployee_name = string.Empty;

            msSQL = " SELECT zonal_name FROM isn_mst_tzonal2rmmapping where zone_gid='" + values.zone_gid + "'";
            string lszone_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT concat(a.user_firstname, ' ', a.user_lastname, '/', a.user_code) as employee_name,b.employee_gid" +
                " from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid WHERE b.employee_emailid='" + lsrmmail_id + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Read();
                lsrmemployee_gid = objODBCDataReader["employee_gid"].ToString();
                lsrmemployee_name = objODBCDataReader["employee_name"].ToString();
                objODBCDataReader.Close();

                msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                msSQL = " INSERT INTO isn_mst_temployeelist(" +
                        " employeelist_gid," +
                        " zone_gid ," +
                        " employee_gid ," +
                        " employee_name ," +
                        " employee_emailid ," +
                        " employee_type ," +
                        " acknowledgement_flag ," +
                        " zone_name ," +
                        " created_by ," +
                        " created_date )" +
                        " VALUES(" +
                        "'" + msGetChildGid + "'," +
                        "'" + values.zone_gid + "'," +
                        "'" + lsrmemployee_gid + "'," +
                        "'" + lsrmemployee_name + "'," +
                        "'" + lsrmmail_id + "'," +
                        "'RM'," +
                        "'" + values.acknowledgement_flag + "'," +
                        "'" + lszone_name.Replace("'","\\'") + "'," +
                        "'" + user_gid + "'," +
                        "current_timestamp)";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " SELECT email_gid FROM isn_trn_tmaildetails" +
                       " WHERE from_mailaddress='" + lsrmmail_id + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt_datatable.Rows)
                    {
                        msSQL = " SELECT workitemassign_gid FROM isn_trn_tworkitemassign " +
                               " WHERE email_gid='" + dr["email_gid"].ToString() + "'";
                        objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDataReader.HasRows == false)
                        {
                            objODBCDataReader.Close();

                            msGetGID = objcmnfunctions.GetMasterGID("T2EM");
                            msSQL = " insert into isn_trn_tworkitemassign(" +
                             " workitemassign_gid," +
                             " email_gid," +
                             " zone_gid," +
                             " zone_name," +
                             " rmemployee_gid," +
                             " rmemployee_name," +
                             " rmemployee_mailid," +
                             " allottedby_name," +
                             " allotted_on," +
                             " created_by)" +
                             " values(" +
                             "'" + msGetGID + "'," +
                             "'" + dr["email_gid"].ToString() + "'," +
                             "'" + values.zone_gid + "'," +
                             "'" + values.zone_name.Replace("'","\\'") + "'," +
                             "'" + lsrmemployee_gid + "'," +
                             "'" + lsrmemployee_name + "'," +
                             "'" + lsrmmail_id + "'," +
                             " (SELECT concat(user_firstname, ' ', user_lastname, '/', user_code) as employee_name FROM adm_mst_tuser WHERE user_gid='" + user_gid + "')," +
                             " current_timestamp," +
                             "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDataReader.Close();

                    }
                }
                dt_datatable.Dispose();
            }
            else
            {
                objODBCDataReader.Close();
                objResult.status = true;
                objResult.message = "RM Mail ID Not In Employee Master.Please Check";
                return objResult;
            }

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Zone Assigned Successfully";

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
            return objResult;
        }

        public void DaWorkItemArchivalSummary(WorkItemList values)
        {
            msSQL = " (SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date," +
                    " b.rmemployee_gid, b.rmemployee_name," +
                    " a.status FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    " WHERE 1 = 1 AND a.status<>'Close' AND a.status<>'Archival' AND a.reference_flag='Y' and a.history_flag='N' group by a.email_gid )" +
                    " UNION " +
                     " (SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.composemail_gid, a.composemail_refno, a.frommail_id, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as email_date," +
                     " a.email_subject, a.mailcontent, date_format(a.created_date, '%d-%m-%Y') as created_date," +
                     " c.employee_gid,concat(b.user_firstname,'',b.user_lastname,'/',b.user_code ) as rmemployee_name, " +
                     " case when a.status='Pending' then 'Sent Mail' when a.status='Forward' then 'Sent Mail' else a.status end as status" +
                     " FROM isn_trn_tcomposemail a" +
                     " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by " +
                     " LEFT JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                     " WHERE a.status<>'Archival' GROUP BY a.composemail_gid )";

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
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    status = row["status"].ToString(),
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

        public bool DaGetConsolidateExcel(string employee_gid, MdlConsolidateWorkItem values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.workitemref_no as Workitemref_No, a.from_mailaddress as Sender_Mailaddress,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as Mail_SentDate," +
                    " case when c.acknowledgement_flag = 'Y' then 'Yes' when c.acknowledgement_flag = 'N' then 'No' " +
                    " else 'Not Assigned' end as Mail_Trigger, c.zone_name as Zone_Name," +
                    " CONCAT(e.user_firstname,' ',e.user_lastname) as Sender_Name,e.user_code as Sender_Code, d.employee_mobileno as Mobile_No," +
                    " a.email_subject as Mail_Subject, a.cc as SentMail_CC, a.aging as Aging, a.status as Status, g.remarks as Status_Remarks," +
                    " concat(f.user_firstname,' ',f.user_lastname,'/',f.user_code) as Status_Updated_By, (date_format(a.updated_date, '%d-%m-%Y %h:%i %p')) as Status_Updated_On, a.workitemhold_reason as WorkItemHold_reason" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_treferencemail b ON a.email_gid=b.email_gid" +
                    " LEFT JOIN isn_mst_temployeelist c ON c.employee_emailid=a.from_mailaddress " +
                    " LEFT JOIN hrm_mst_temployee d ON d.employee_emailid=a.from_mailaddress " +
                    " LEFT JOIN adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " LEFT JOIN adm_mst_tuser f on f.user_gid = a.updated_by" +
                    " LEFT JOIN isn_trn_tworkitemdecision g on g.email_gid = a.email_gid where a.history_flag='N' group by a.email_gid" +
                    " UNION " +
                    " SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.composemail_refno as Workitemref_No, c.employee_emailid as Sender_Mailaddress, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Mail_SentDate," +
                    " '-' as Mail_Trigger, d.zone_name as Zone_Name," +
                    " CONCAT(b.user_firstname,' ',b.user_lastname) as Sender_Name,b.user_code as Sender_Code, c.employee_mobileno as Mobile_No," +
                    " a.email_subject as Mail_Subject, a.ccmail_id as SentMail_CC, '-' as Aging, " +
                    " case when a.status='Pending' then 'Sent Mail' else a.status end as Status, '-' as Status_Remarks, '-' as Status_Updated_By, '-' as Status_Updated_On, '-'" +
                     " FROM isn_trn_tcomposemail a" +
                     " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by " +
                     " LEFT JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                     " LEFT JOIN isn_mst_temployeelist d on d.employee_gid = c.employee_gid WHERE a.status<>'Archival' GROUP BY a.composemail_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);

                
                var workSheet = excel.Workbook.Worksheets.Add("Consolidate_WorkItem");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IASN/Consolidate_WorkItem/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(values.excel_path)))
                            System.IO.Directory.CreateDirectory(values.excel_path);
                    }

                    values.excel_name = "Consolidate_WorkItem" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IASN/Consolidate_WorkItem/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;

                    values.lscloudpath = lscompany_code + "/" + "IASN/Consolidate_WorkItem/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;

                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(values.excel_path);
                    using (var range = workSheet.Cells[1, 1, 1, 29])  //Address "A1:A18"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IASN/Consolidate_WorkItem/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name, ms);
                    ms.Close();

                   // excel.SaveAs(file);
                    dt_datatable.Dispose();
                    values.status = true;
                    values.message = "Success";
                }
                catch (Exception ex)
                {
                    dt_datatable.Dispose();
                    values.message = ex.ToString();
                    values.status = false;
                }
            }
            else
            {
                dt_datatable.Dispose();
                values.message = "No Records Found..!";
                values.status = false;
            }
            return true;
        }

        public void DaComposeMailSummary(ComposeMailList values)
        {
            msSQL = " SELECT a.composemail_gid, a.tomail_id, a.ccmail_id, a.bccmail_id, a.email_subject, a.mailcontent, a.created_date, d.zone_name, c.employee_emailid," +
                    " c.employee_gid, if (b.user_firstname is null, 'No', upper(substr(b.user_firstname, 1, 1))) as initial_caps, " +
                    " a.status, a.composemail_refno FROM isn_trn_tcomposemail a " +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by " +
                    " LEFT JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                    " LEFT JOIN isn_mst_temployeelist d on d.employee_gid = c.employee_gid WHERE 1 = 1 AND a.status<>'Archival' GROUP BY a.composemail_gid ORDER BY composemail_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    composemail_gid = row["composemail_gid"].ToString(),
                    email_from = row["employee_emailid"].ToString(),
                    email_to = row["tomail_id"].ToString(),
                    cc = row["ccmail_id"].ToString(),
                    bcc = row["bccmail_id"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["mailcontent"].ToString(),
                    created_date = row["created_date"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    employee_gid = row["employee_gid"].ToString(),
                    employee_name = row["initial_caps"].ToString(),
                    status = row["status"].ToString(),
                    composemail_refno = row["composemail_refno"].ToString(),
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

        public bool DaComposeMail(ComposeMailList values, string user_gid)
        {
            try
            {
                msSQL = "SELECT pop_server, pop_port, pop_username, pop_password FROM isn_trn_tmailcredentials ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    ls_server = objODBCDataReader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDataReader["pop_port"].ToString());
                    ls_username = objODBCDataReader["pop_username"].ToString();
                    ls_password = objODBCDataReader["pop_password"].ToString();
                }
                objODBCDataReader.Close();

                msGetGID = objcmnfunctions.GetMasterGID("COMP");
                msGet_RefNO = objcmnfunctions.GetMasterGID("CAD");
                msSQL = " INSERT INTO isn_trn_tcomposemail(" +
                     " composemail_gid," +
                     " composemail_RefNO," +
                     " email_subject," +
                     " emailsubject_Ref," +
                     " frommail_id," +
                     " tomail_id," +
                     " ccmail_id," +
                     " bccmail_id," +
                     " mailcontent," +
                     " Mail_flag," +
                     " created_date," +
                     " created_by)" +
                     " VALUES(" +
                     "'" + msGetGID + "'," +
                     "'" + msGet_RefNO + "'," +
                     "'" + values.email_subject + "'," +
                     "'" + "##IA#<<" + msGet_RefNO + ">> " + values.email_subject + "'," +
                     "'" + ls_username + "'," +
                     "'" + values.tomail_id + "'," +
                     "'" + values.ccmail_id + "'," +
                     "'" + values.bccmail_id + "'," +
                     "'" + values.mailcontent.Replace("'", "") + "'," +
                     "'" + "N" + "'," +
                     "current_timestamp," +
                     "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if(mnResult == 1)
                {
                    lsmailbody = lsmailbody + "<br/>";
                    lsmailbody = lsmailbody + values.mailcontent.Replace("'", "") + "<br/>";

                    lsmailbody = lsmailbody + "<br/>";

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);

                    if (values.tomail_id != "" && values.tomail_id != null)
                    {
                        to = (values.tomail_id).Split(';');
                        if (to.Length > 0)
                        {
                            foreach (string toEmail in to)
                            {
                                if (toEmail != ls_username)
                                {
                                    message.To.Add(new MailAddress(toEmail));
                                }
                            }
                        }
                    }

                    if (values.ccmail_id != "" && values.ccmail_id != null)
                    {
                        cc = (values.ccmail_id).Split(';');
                        if (cc.Length > 0)
                        {
                            foreach (string ccEmail in cc)
                            {
                                if (ccEmail != ls_username)
                                {
                                    message.CC.Add(new MailAddress(ccEmail));
                                }

                            }
                        }
                    }

                    if (values.bccmail_id != "" && values.bccmail_id != null)
                    {
                        bcc = (values.bccmail_id).Split(',');
                        if (bcc.Length > 0)
                        {
                            foreach (string bccEmail in bcc)
                            {
                                if (bccEmail != ls_username)
                                {
                                    message.Bcc.Add(new MailAddress(bccEmail));
                                }

                            }
                        }
                    }

        // Document Attachments
                    msSQL = "select * from isn_tmp_tcomposemailattachement where created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        lsattachment_flag = "Y";
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            Attachment data = new Attachment(HttpContext.Current.Server.MapPath(dt["document_path"].ToString()), MediaTypeNames.Application.Octet);
                            data.Name = dt["document_name"].ToString();
                            message.Attachments.Add(data);

                            msGetDocumentGid = objcmnfunctions.GetMasterGID("ISDO");
                            msSQL = " INSERT into isn_trn_tcomposemailattachement (" +
                                    " composemailattachment_gid," +
                                    " composemail_gid," +
                                    " document_name," +
                                    " document_path," +
                                    " decision," +
                                    " created_by," +
                                    " created_date" +
                                    " ) VALUES (" +
                                    " '" + msGetDocumentGid + "'," +
                                    " '" + msGetGID + "'," +
                                    " '" + dt["document_name"].ToString() + "'," +
                                    " '" + dt["document_path"].ToString() + "'," +
                                    " 'Pending'," +
                                    "'" + user_gid + "'," +
                                    " current_timestamp)";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    dt_datatable.Dispose();

                    msSQL = " UPDATE isn_trn_tcomposemail SET attachment_flag = '"+ lsattachment_flag +"',"+
                        " updated_by = '"+ user_gid +"',"+
                        " updated_date = current_timestamp ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " DELETE FROM isn_tmp_tcomposemailattachement where created_by='" + user_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    message.Subject = values.email_subject;
                    message.IsBodyHtml = true;
                    message.Body = lsmailbody;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.message = "Mail Sent Successfully";
                    values.status = true;
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                values.message = "Error while Sending the Mail";
                values.status = false;
                return false;
            }
        }

        public result DaComposeMailAttachment(HttpRequest httpRequest, string employee_gid, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string servicerequest_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IASN/ComposeMailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IASN/ComposeMailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            objResult.status = false;
                        }
                        else
                        {
                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IASN/ComposeMailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "IASN/ComposeMailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IASN/ComposeMailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                            //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();
                            //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IASN/ComposeMailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msSQL = " insert into isn_tmp_tcomposemailattachement( " +
                                        " document_name ," +
                                        " document_path," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
                                        "'" + httpPostedFile.FileName + "'," +
                                        "'" + lspath + msdocument_gid + FileExtension + "'," +
                                        "'" + user_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                objResult.status = true;
                                objResult.message = "Document Uploaded Successfully";
                            }
                            else
                            {
                                objResult.status = false;
                                objResult.message = "Error Occured";
                            }
                        }

                       

                    }
                    return objResult;

                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured";

                    return objResult;
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;

                return objResult;
            }
        }

        public void DaGetComposeMailAttachment(MdlcDocList objfileDtls, string user_gid)
        {
            msSQL = " SELECT composemailattachment_gid, document_name,document_path FROM isn_tmp_tcomposemailattachement WHERE created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlDocDetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new MdlDocDetails
                    {
                        id = dt["composemailattachment_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = dt["document_path"].ToString(),
                    });
                }
                objfileDtls.MdlDocDetails = getDocList;
            }
            dt_datatable.Dispose();
        }

        public result DaDeleteComposeMailAttachment(string id)
        {
            msSQL = " DELETE FROM isn_tmp_tcomposemailattachement WHERE composemailattachment_gid='" + id + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Mail Attachment Deleted successfully";
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }
            return objResult;
        }

        public void DaMailtempdelete(string user_gid)
        {
            msSQL = "delete from isn_tmp_tcomposemailattachement where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                objResult.status = true;
            }
            else
            {
                objResult.status = false;
            }
        }

        public void DaComposeMailview(string lscomposemail_gid, ComposeMailList values)
        {
            msSQL = " SELECT a.composemail_gid, a.email_subject, a.frommail_id, a.tomail_id, a.ccmail_id, a.bccmail_id, a.mailcontent, a.created_date, d.zone_name, " +
                    "  case when a.status <> 'Pending' then a.status else '-' end as status FROM isn_trn_tcomposemail a " +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by " +
                    " LEFT JOIN hrm_mst_temployee c on c.user_gid = b.user_gid " +
                    " LEFT JOIN isn_mst_temployeelist d on d.employee_gid = c.employee_gid " +
                    " WHERE a.composemail_gid = '" + lscomposemail_gid + "' ";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.composemail_gid = objODBCDataReader["composemail_gid"].ToString();
                values.frommail_id = objODBCDataReader["frommail_id"].ToString();
                values.email_subject = objODBCDataReader["email_subject"].ToString();
                values.mailcontent = objODBCDataReader["mailcontent"].ToString();
                values.ccmail_id = objODBCDataReader["ccmail_id"].ToString();
                values.bccmail_id = objODBCDataReader["bccmail_id"].ToString();
                values.tomail_id = objODBCDataReader["tomail_id"].ToString();
                values.zone_name = objODBCDataReader["zone_name"].ToString();
                values.email_date = objODBCDataReader["created_date"].ToString();
                values.email_status = objODBCDataReader["status"].ToString();
            }
            objODBCDataReader.Close();
            
            msSQL = " SELECT composemailattachment_gid,document_path,document_name, substring_index(document_name,'.',-1) as document_extension" +
                    " FROM isn_trn_tcomposemailattachement WHERE composemail_gid = '" + lscomposemail_gid + "' and decision <>'Forward' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    composemailattachment_gid = row["composemailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = row["document_path"].ToString(),
                    document_extension = row["document_extension"].ToString()
                }).ToList();
            }
            dt_datatable.Dispose();
        }

        public result DaComposeMailDecision(string user_gid, MdlArchival values)
        {
            msSQL = " UPDATE isn_trn_tcomposemail SET" +
                       " status='" + values.status + "'," +
                       " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                       " WHERE composemail_gid='" + values.composemail_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.status == "Forward")
            {
                msGetGID = objcmnfunctions.GetMasterGID("DESC");
                msSQL = " INSERT INTO isn_trn_tcomposemaildecision(" +
                                 " decision_gid," +
                                 " decision," +
                                 " composemail_gid," +
                                 " tomail_id," +
                                 " ccmail_id," +
                                 " bccmail_id," +
                                 " email_subject," +
                                 " remarks," +
                                 " created_date," +
                                 " created_by)" +
                                 " VALUES(" +
                                 "'" + msGetGID + "'," +
                                 "'" + values.status + "'," +
                                 "'" + values.composemail_gid + "'," +
                                 "'" + values.tomail_id + "'," +
                                 "'" + values.ccmail_id + "'," +
                                 "'" + values.bccmail_id + "'," +
                                 "'" + values.email_subject.Replace("'", "") + "'," +
                                 "'" + values.mailcontent.Replace("'", "") + "'," +
                                 "current_timestamp," +
                                 "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
                if (mnResult == 1)
                {
                    msSQL = " INSERT INTO isn_trn_tcomposeauditlog(" +
                            " composemail_gid," +
                            " action_taken," +
                            " created_by)" +
                            " VALUES(" +
                            "'" + values.composemail_gid + "'," +
                            "'" + values.status + "'," +
                            "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "SELECT concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                       " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                       "  WHERE employee_emailid = '" + values.tomail_id + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        objODBCDataReader.Read();
                        lstomail = objODBCDataReader["created_by"].ToString().Split('/');
                        lsmailbody = "Hi " + lstomail[0] + ",  <br />";
                    }
                    else
                    {
                        lsmailbody = "Hi,  <br />";
                    }
                    objODBCDataReader.Close();

                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + " This Mail is forwarded for further process.";
                    lsmailbody = lsmailbody + "<br/>";
                    lsmailbody = lsmailbody + " Remarks :" + values.mailcontent.Replace("'", "") + "<br/>";

                    lsmailbody = lsmailbody + "<br/>";

                    lsstatus = objComposeSentMail.DaIasnComposeReplyMail(values.composemail_gid, values.status, values.tomail_id, values.ccmail_id, values.bccmail_id, user_gid, values.email_subject, lsmailbody);

                  if(lsstatus == true)
                    {
                        objResult.status = true;
                        objResult.message = "Mail Forwarded Successfully";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error Occured While Mail Sent";
                    }
                }
            }
            else
            {
                if (values.archival_type == "" || values.archival_type == null)
                {
                    values.archival_type = "Customer";
                }
                msGetGID = objcmnfunctions.GetMasterGID("DESC");
                msSQL = " INSERT INTO isn_trn_tworkitemdecision(" +
                        " decision_gid," +
                        " decision," +
                        " archival_type," +
                        " email_gid," +
                        " remarks," +
                        " customer_gid," +
                        " customer_name," +
                        " customer2sanction_gid," +
                        " sanctionref_no," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGID + "'," +
                        "'Archival'," +
                        "'" + values.archival_type + "'," +
                        "'" + values.composemail_gid + "'," +
                        "'" + values.remarks.Replace("'", "") + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + values.customer_name + "'," +
                        "'" + values.sanction_gid + "'," +
                        "'" + values.sanctionref_no + "'," +
                        "current_timestamp," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Mail Archived Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured";
                }
            }
            return objResult;
        }

        public void DaComposeReferenceMail(string composemail_gid, MdlReferenceMailList values)
        {

            msSQL = " SELECT composesentmail_gid, decision as status,composemail_gid ,mailcontent, email_subject, " +
                    " frommail_id, tomail_id, ccmail_id, bccmail_id," +
                    " date_format(created_date, '%d-%m-%Y %h:%i %p') as email_date " +
                    " FROM isn_trn_tcomposesentmail " +
                    " WHERE   composemail_gid='" + composemail_gid + "' ORDER BY created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                var getDocList = new List<MdlReferenceMail>();
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " SELECT composemailattachment_gid,document_path,document_name," +
                        " substring_index(document_name,'.',-1) as document_extension" +
                        " FROM isn_trn_tcomposemailattachement " +
                        " WHERE composemail_gid = '" + dt["composemail_gid"].ToString() + "' and decision='"+ dt["status"].ToString() +"' ";
                    dt_child = objdbconn.GetDataTable(msSQL);
                    var attachmentlist = new List<MdlAttachmentList>();
                    if (dt_child.Rows.Count != 0)
                    {
                        attachmentlist = dt_child.AsEnumerable().Select(row => new MdlAttachmentList
                        {
                            composemailattachment_gid = row["composemailattachment_gid"].ToString(),
                            document_name = row["document_name"].ToString(),
                            document_path = row["document_path"].ToString(),
                            document_extension = row["document_extension"].ToString()
                        }).ToList();

                    }
                    dt_child.Dispose();
                    
                    getDocList.Add(new MdlReferenceMail
                    {
                        composemail_gid = dt["composemail_gid"].ToString(),
                        email_from = dt["frommail_id"].ToString(),
                        email_date = dt["email_date"].ToString(),
                        emailsubject = dt["email_subject"].ToString(),
                        email_content = dt["mailcontent"].ToString(),
                        email_to = dt["tomail_id"].ToString(),
                        email_cc = dt["ccmail_id"].ToString(),
                        email_bcc = dt["bccmail_id"].ToString(),
                        MdlAttachmentList = attachmentlist,

                    });
                    values.MdlReferenceMail = getDocList;
                }

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();
        }

        public bool DaHoldWorkItem(string user_gid, hold values)
        {
                msGetGID = objcmnfunctions.GetMasterGID("WIHO");

                msSQL = "insert into isn_trn_tworkitem2hold(" +
                       " workitem2hold_gid ," +
                       " email_gid," +
                       " workitemhold_reason," +
                       " assigned_flag," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGID + "'," +
                       "'" + values.email_gid + "'," +
                       "'" + values.workitemhold_reason.Replace("'", "\\'") + "'," +
                       "'" + values.assigned_flag + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if(values.assigned_flag == 'Y')
            {
                msSQL = " update isn_trn_tworkitemassign set workitemhold_reason ='" + values.workitemhold_reason.Replace("'", "\\'") + "'," +
                   " hold_flag='Y' where email_gid = '" + values.email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update isn_trn_tmaildetails set workitemhold_reason ='" + values.workitemhold_reason.Replace("'", "\\'") + "'," +
                   " hold_flag='Y' where email_gid = '" + values.email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if(mnResult == 1)
            { 
                values.message = "Workitem Hold Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }
        }

        public void DaHoldLogDetails(string lsemail_gid, string assigned_flag, MdlHoldLogList values)
        {
            msSQL = " SELECT a.workitem2hold_gid,a.workitemhold_reason, date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(b.user_firstname, b.user_lastname, '/', b.user_code) as created_by" +
                    " FROM isn_trn_tworkitem2hold a" +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by" +
                    " WHERE a.email_gid = '" + lsemail_gid + "' and assigned_flag='" + assigned_flag + "' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlholdLog = dt_datatable.AsEnumerable().Select(row => new Models.MdlholdLog
                {
                    workitem2hold_gid = row["workitem2hold_gid"].ToString(),
                    workitemhold_reason = row["workitemhold_reason"].ToString(),
                    created_date = row["created_date"].ToString(),
                    created_by = row["created_by"].ToString()
                }).ToList();
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }
        }

        public void DaAssignedCountList(MyWorkItemListCount values)
        {
            msSQL = " select checkeremployee_gid, count(*) as assigned_count,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_to   " +
                    " FROM isn_trn_tworkitemassign a " +
                    " LEFT JOIN hrm_mst_temployee b on b.employee_gid = a.checkeremployee_gid  " +
                    " LEFT JOIN adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " LEFT JOIN isn_trn_tmaildetails d on a.email_gid=d.email_gid " +
                    " where a.checkeremployee_gid <> 'null' and d.status ='Pending' AND d.history_flag='N' group by a.checkeremployee_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAssignedList = dt_datatable.AsEnumerable().Select(row => new MdlAssignedList
                {
                    employee_gid = row["checkeremployee_gid"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_count = row["assigned_count"].ToString(),
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

        public void DaUpdateCustomer(MdlArchivalCondition values)
        {
            msSQL = " UPDATE isn_trn_tmaildetails SET" +
                    " customer_gid='" + values.customer_gid + "'," +
                    " customer_name='" + values.customer_name + "'," +
                    " customer_type='" + values.customer_type + "'" +
                    " WHERE email_gid='" + values.email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Customer Details Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        public void DaGetConsolidatedReport(WorkItemList values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date, b.zone_name, b.zone_gid," +
                    " b.rmemployee_gid, b.rmemployee_name," +
                    " concat(c.user_firstname,'',c.user_lastname,'/',c.user_code ) as Statusupdated_by, a.updated_date," +
                    " a.status FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                    " LEFT JOIN adm_mst_tuser c ON c.user_gid = a.updated_by " +
                    " WHERE a.reference_flag='Y' AND a.history_flag='N' GROUP BY a.email_gid ORDER BY a.email_gid DESC LIMIT 500";

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
                    zone_name = row["zone_name"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    status = row["status"].ToString(),
                    Statusupdated_by = row["Statusupdated_by"].ToString(),
                    Statusupdated_date = row["updated_date"].ToString(),

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

        public bool DaGetConsolidatedReportExcel(string emailfrom_date, string emailto_date, MdlConsolidateWorkItem values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(300000) */ a.workitemref_no as Workitemref_No, a.from_mailaddress as Sender_Mailaddress,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as Mail_SentDate," +
                    " case when c.acknowledgement_flag = 'Y' then 'Yes' when c.acknowledgement_flag = 'N' then 'No' " +
                    " else 'Not Assigned' end as Mail_Trigger, c.zone_name as Zone_Name," +
                    " CONCAT(e.user_firstname,' ',e.user_lastname) as Sender_Name,e.user_code as Sender_Code, d.employee_mobileno as Mobile_No," +
                    " a.email_subject as Mail_Subject, a.cc as SentMail_CC, a.aging as Aging, a.status as Status, " +
                    " concat(f.user_firstname,' ',f.user_lastname,'/',f.user_code) as Status_UpdatedBy, date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as Status_UpdatedOn, " +
                    " a.customer_type as Customer_Type, a.customer_name as Customer_Name, a.workitemhold_reason as WorkItemHold_reason,"+
                    " if(g.decision = 'Close', g.remarks ,'-') as Status_Remarks" +
                    " FROM isn_trn_tmaildetails a" +
                    " LEFT JOIN isn_mst_temployeelist c ON c.employee_emailid=a.from_mailaddress " +
                    " LEFT JOIN hrm_mst_temployee d ON d.employee_emailid=a.from_mailaddress " +
                    " LEFT JOIN adm_mst_tuser e on e.user_gid=d.user_gid" +
                    " LEFT JOIN adm_mst_tuser f on f.user_gid = a.updated_by" +
                    " INNER JOIN isn_trn_tworkitemdecision g ON g.email_gid = a.email_gid" +
                    " WHERE a.reference_flag='Y' AND a.from_mailaddress<>'iassign@samunnati.com' AND a.email_date>='" + emailfrom_date + "' AND a.email_date<='" + emailto_date + "' group by a.email_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);

                var workSheet = excel.Workbook.Worksheets.Add("Consolidate_Report");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IASN/Consolidate_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(values.excel_path)))
                            System.IO.Directory.CreateDirectory(values.excel_path);
                    }

                    values.excel_name = "Consolidate_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IASN/Consolidate_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;


                    values.lscloudpath = lscompany_code + "/" + "IASN/Consolidate_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;
                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(values.excel_path);
                    using (var range = workSheet.Cells[1, 1, 1, 18])  //Address "A1:A18"
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IASN/Consolidate_Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name, ms);
                    ms.Close();

                   // excel.SaveAs(file);
                    dt_datatable.Dispose();
                    values.status = true;
                    values.message = "Excel Downloaded Successfully";
                }
                catch (Exception ex)
                {
                    dt_datatable.Dispose();
                    values.message = ex.ToString();
                    values.status = false;
                }
            }
            else
            {
                dt_datatable.Dispose();
                values.message = "No Records Found..!";
                values.status = false;
            }
            return true;
        }
    }
}