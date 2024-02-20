using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.osd.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnTicketManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGidsactivity, lscreated_by, lscreated_date, lsRaised_By, lsRaisedNo, lsBaselocation_Name, lsRaised_Date, lsemployee_mobileno, lslevel_zero, lslevel_one, lsemployee_number;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, body, sub, lscontent = string.Empty;
        string lsrequest_refno, lsactivity_name, lsrequest_title, lsresponse_flag, lsrequested_by, request_title, lstransferondtl, lstransferbydtl, lsrequest_description, lsassigned_membername, assigned_supportteamname, lsrequest_status;
        int mnResult, ls_port, MailFlag;
        string[] lsCCReceipients, lsBCCReceipients;
        string cc, count, app_count, lsstatus,lsbcc;
        string cc_mailid = string.Empty;
        string lsrequestreopen_gid;
        string lsallocatemanagername;
        string lsmodulereportingto_gid;

        public void DaGetServiceRequestSummary(servicerequestdtllist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {

                msSQL = " select a.servicerequest_gid,activity_name,a.activitymaster_gid,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,request_status,request_refno, a.assigned_supportteamname,a.department_gid, " +
                    " a.assigned_membername, raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,if(request_status='Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging, " +
                     " a.assigned_membergid, a.assigned_supportteamgid, a.ticket_source as source ,a.transfer_flag, a.reopen_flag, bankalert_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, bankalert2allocated_gid, customer_gid,(CASE WHEN (f.priority IS null  OR f.priority='')THEN 'None' ELSE f.priority END) AS priority,CASE WHEN a.reopen_flag='Y' THEN CONCAT(FLOOR((DATEDIFF(now(),a.reopened_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.reopened_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.reopened_date)), 'Mins')  ELSE if (a.request_status = 'Completed', CONCAT(FLOOR((DATEDIFF(a.assigned_date,a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date,a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.assigned_date,a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(),a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.created_date)), 'Mins') ) END as aging " +
                   " from osd_trn_tservicerequest a  " +
                  "  left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                  "  left join osd_trn_tpriority f on f.servicerequest_gid = a.servicerequest_gid  AND f.priority_flag='Y' " +
                   " where a.request_status='Allotted' order by a.created_date asc ";
            }
            else
            {
                msSQL = " select a.servicerequest_gid,activity_name,a.activitymaster_gid,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,request_status,request_refno, a.assigned_supportteamname,a.department_gid, " +
                  "  a.assigned_membername, raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,if(request_status='Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging, " +
                 "   a.assigned_membergid, a.assigned_supportteamgid,a.ticket_source as source ,a.transfer_flag, a.reopen_flag, bankalert_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, bankalert2allocated_gid, customer_gid,(CASE WHEN (f.priority IS null  OR f.priority='')THEN 'None' ELSE f.priority END) AS priority,CASE WHEN a.reopen_flag='Y' THEN CONCAT(FLOOR((DATEDIFF(now(),a.reopened_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.reopened_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.reopened_date)), 'Mins')  ELSE if (a.request_status = 'Completed', CONCAT(FLOOR((DATEDIFF(a.assigned_date,a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date,a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.assigned_date,a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(),a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.created_date)), 'Mins') ) END as aging  " +
                   " from osd_trn_tservicerequest a  " +
                  "  left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                   " left join osd_trn_tpriority f on f.servicerequest_gid = a.servicerequest_gid  AND f.priority_flag='Y' " +
                   " where ( a.department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " and( a.request_status='Allotted') and e.department_status='Y' order by a.created_date asc ";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestList = new List<servicerequestdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select getapproval_flag from osd_trn_tservicerequest where servicerequest_gid= '" + dt["servicerequest_gid"].ToString() + "'";
                    var lsgetapproval_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgetapproval_flag == "Y")
                    {

                        msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where (approval_status = 'Approved' or approval_status = 'Rejected' or approval_status = 'Cancelled') and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            "as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            count = objODBCDatareader["count"].ToString();
                            app_count = objODBCDatareader["app_count"].ToString();
                            if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                            {
                                lsstatus = "Completed";
                            }
                            else
                            {
                                lsstatus = "Pending";
                            }

                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            lsstatus = "Pending";
                        }

                    }
                    else
                    {
                        lsstatus = "NotInitiated";
                    }
                    if (lsstatus == "NotInitiated" || lsstatus == "Completed")
                    {
                        getservicerequestList.Add(new servicerequestdtl
                        {
                            servicerequest_gid = dt["servicerequest_gid"].ToString(),
                            activity_name = dt["activity_name"].ToString(),
                            request_title = dt["request_title"].ToString(),
                            request_description = dt["request_description"].ToString(),
                            request_status = dt["request_status"].ToString(),
                            raised_department = dt["department_name"].ToString(),
                            departmentname = dt["departmentname"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            raised_date = dt["raiseddate"].ToString(),
                            raised_by = dt["raisedby"].ToString(),
                            assigned_team = dt["assigned_supportteamname"].ToString(),
                            assigned_to = dt["assigned_membername"].ToString(),
                            request_refno = dt["request_refno"].ToString(),
                            transfer_flag = dt["transfer_flag"].ToString(),
                            assigned_membergid = dt["assigned_membergid"].ToString(),
                            assigned_supportteamgid = dt["assigned_supportteamgid"].ToString(),
                            reopen_flag = dt["reopen_flag"].ToString(),
                            bankalert_flag = dt["bankalert_flag"].ToString(),
                            bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                            aging = dt["aging"].ToString(),
                            priority = dt["priority"].ToString(),
                            activitymaster_gid = dt["activitymaster_gid"].ToString(),
                            approval_status = lsstatus,
                            source = dt["source"].ToString()
                        });
                        values.servicerequestdtl = getservicerequestList;
                    }
                }
            }
            values.lsallotted_count = getservicerequestList.Count;
            dt_datatable.Dispose();
        }

        public void DaGetMyWorkInProgressSummary(workinprogresslist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = "select a.servicerequest_gid,activity_name,a.activitymaster_gid,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,request_status, a.assigned_supportteamname, request_refno,a.department_gid ,  " +
                   "  a.assigned_membername,raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,if(request_status='Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging, " +
                   "  a.assigned_membergid, a.assigned_supportteamgid,a.ticket_source as source , a.transfer_flag, a.transfer_supportteamname, a.transfer_membername, a.reopen_flag, bankalert_flag, bankalert2allocated_gid,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, customer_gid,(CASE WHEN (f.priority IS null  OR f.priority='')THEN 'None' ELSE f.priority END) AS priority,CASE WHEN a.reopen_flag='Y' THEN CONCAT(FLOOR((DATEDIFF(now(),a.reopened_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.reopened_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.reopened_date)), 'Mins')  ELSE if (a.request_status = 'Completed', CONCAT(FLOOR((DATEDIFF(a.assigned_date,a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date,a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.assigned_date,a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(),a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.created_date)), 'Mins') ) END as aging from osd_trn_tservicerequest a  " +
                   " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                   "  left join osd_trn_tpriority f on f.servicerequest_gid = a.servicerequest_gid  AND f.priority_flag='Y' " +
                      " where ( a.request_status='Work-In-Progress' and  a.assigned_status<>'Forward') order by a.created_date asc ";
            }
            else
            {
                msSQL = " select a.servicerequest_gid,activity_name,a.activitymaster_gid,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,request_status, a.assigned_supportteamname, request_refno,a.department_gid, " +
                  " a.assigned_membername,raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,if(request_status='Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging," +
                 "  a.assigned_membergid, a.assigned_supportteamgid,a.ticket_source as source , a.transfer_flag, a.transfer_supportteamname, a.transfer_membername, a.reopen_flag, bankalert_flag, bankalert2allocated_gid,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, customer_gid,(CASE WHEN (f.priority IS null  OR f.priority='')THEN 'None' ELSE f.priority END) AS priority,CASE WHEN a.reopen_flag='Y' THEN CONCAT(FLOOR((DATEDIFF(now(),a.reopened_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.reopened_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.reopened_date)), 'Mins')  ELSE if (a.request_status = 'Completed', CONCAT(FLOOR((DATEDIFF(a.assigned_date,a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.assigned_date,a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.assigned_date,a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(),a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(),a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(now(),a.created_date)), 'Mins') ) END as aging from osd_trn_tservicerequest a  " +
                    "  left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                  "  left join osd_trn_tpriority f on f.servicerequest_gid = a.servicerequest_gid  AND f.priority_flag='Y' " +
                   " where (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " and ( a.request_status='Work-In-Progress' and  a.assigned_status<>'Forward') and e.department_status='Y' order by a.created_date asc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<workinprogressdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select getapproval_flag from osd_trn_tservicerequest where servicerequest_gid= '" + dt["servicerequest_gid"].ToString() + "'";
                    var lsgetapproval_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgetapproval_flag == "Y")
                    {

                        msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where (approval_status = 'Approved' or approval_status = 'Rejected' or approval_status = 'Cancelled') and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            "as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            count = objODBCDatareader["count"].ToString();
                            app_count = objODBCDatareader["app_count"].ToString();
                            if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                            {
                                lsstatus = "Completed";
                            }
                            else
                            {
                                lsstatus = "Pending";
                            }

                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            lsstatus = "Pending";
                        }

                    }
                    else
                    {
                        lsstatus = "NotInitiated";
                    }
                    if (lsstatus == "NotInitiated" || lsstatus == "Completed")
                    {
                        getallotteddtl.Add(new workinprogressdtl
                        {
                            servicerequest_gid = dt["servicerequest_gid"].ToString(),
                            activity_name = dt["activity_name"].ToString(),
                            request_title = dt["request_title"].ToString(),
                            request_status = dt["request_status"].ToString(),
                            raised_department = dt["department_name"].ToString(),
                            departmentname = dt["departmentname"].ToString(),
                            department_gid = dt["department_gid"].ToString(),
                            raised_date = dt["raiseddate"].ToString(),
                            raised_by = dt["raisedby"].ToString(),
                            assigned_team = dt["assigned_supportteamname"].ToString(),
                            assigned_to = dt["assigned_membername"].ToString(),
                            request_refno = dt["request_refno"].ToString(),
                            transfer_flag = dt["transfer_flag"].ToString(),
                            transfer_team = dt["transfer_supportteamname"].ToString(),
                            transfer_to = dt["transfer_membername"].ToString(),
                            assigned_membergid = dt["assigned_membergid"].ToString(),
                            assigned_supportteamgid = dt["assigned_supportteamgid"].ToString(),
                            reopen_flag = dt["reopen_flag"].ToString(),
                            bankalert_flag = dt["bankalert_flag"].ToString(),
                            Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                            bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                            aging = dt["aging"].ToString(),
                            priority = dt["priority"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            activitymaster_gid = dt["activitymaster_gid"].ToString(),
                            approval_status = lsstatus,
                            source = dt["source"].ToString(),                      
                        });
                    }
                    values.workinprogressdtl = getallotteddtl;
                }
            }
            values.lsworkinprogress_count = getallotteddtl.Count;
            dt_datatable.Dispose();
        }

        public void DaGetMyCompletedSummary(completedlist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as raised_department as department_name,a.department_name as departmentname,a.request_status, a.assigned_supportteamname, a.request_refno,a.department_gid, " +
                     "  a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging, " +
                     "  a.transfer_flag, a.transfer_supportteamname, a.transfer_membername,a.ticket_source as source,a.reopen_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, bankalert_flag, bankalert2allocated_gid, customer_gid from osd_trn_tservicerequest a " +
                    "  left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +                     
                     " where (a.request_status='Completed') order by a.created_date asc ";
            }
            else
            {
                msSQL = "  select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.request_status, a.assigned_supportteamname, a.request_refno,a.department_gid, " +
                    " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging," +
                    "  a.transfer_flag, a.transfer_supportteamname, a.transfer_membername, " +
                    " a.ticket_source  as source,a.reopen_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, bankalert_flag, bankalert2allocated_gid,customer_gid from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                    " where (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                    " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                    " and (a.request_status='Completed') and e.department_status='Y'  order by a.created_date asc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcompleteddtl = new List<completeddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcompleteddtl.Add(new completeddtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        transfer_team = dt["transfer_supportteamname"].ToString(),
                        transfer_to = dt["transfer_membername"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.completeddtl = getcompleteddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetMyClosedSummary(closedlist values, string employee_gid)
        {

            msSQL = "Call osd_trn_spactivitymanagementclosedsummary( '" + employee_gid + "')";
            
           dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<closeddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getallotteddtl.Add(new closeddtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        transfer_team = dt["transfer_supportteamname"].ToString(),
                        transfer_to = dt["transfer_membername"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.closeddtl = getallotteddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetRejectCancelSummary(closedlist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,a.request_status,a.assigned_supportteamname,a.request_refno, " +
                   " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,CASE WHEN request_status = 'Cancelled' THEN CONCAT(FLOOR((DATEDIFF(a.cancel_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.cancel_date, a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.cancel_date, a.created_date)), 'Mins')when request_status = 'Rejected' THEN CONCAT(FLOOR((DATEDIFF(a.rejected_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.rejected_date, a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.rejected_date, a.created_date)), ' Mins') END as aging, " +
                   " a.transfer_flag, a.transfer_supportteamname, a.transfer_membername,a.ticket_source as source ,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, a.reopen_flag, a.bankalert_flag, bankalert2allocated_gid,customer_gid from osd_trn_tservicerequest a " +
                   " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   " where (a.request_status='Rejected' or a.request_status='Cancelled') order by a.created_date asc ";
            }
            else
            {
                msSQL = " select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,a.request_status,a.assigned_supportteamname,a.request_refno, " +
                   " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,CASE WHEN request_status = 'Cancelled' THEN CONCAT(FLOOR((DATEDIFF(a.cancel_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.cancel_date, a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.cancel_date, a.created_date)), 'Mins')when request_status = 'Rejected' THEN CONCAT(FLOOR((DATEDIFF(a.rejected_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.rejected_date, a.created_date)), 24), ' Hrs ',MINUTE(TIMEDIFF(a.rejected_date, a.created_date)), ' Mins') END as aging, " +
                   " a.transfer_flag, a.transfer_supportteamname, a.transfer_membername,a.ticket_source as source ,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, a.reopen_flag, a.bankalert_flag, bankalert2allocated_gid,customer_gid from osd_trn_tservicerequest a " +
                  " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   " where (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " and (a.request_status='Rejected' or a.request_status='Cancelled') and e.department_status='Y'  order by a.created_date asc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<rejectcanceldtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getallotteddtl.Add(new rejectcanceldtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        transfer_team = dt["transfer_supportteamname"].ToString(),
                        transfer_to = dt["transfer_membername"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()

                    });
                    values.rejectcanceldtl = getallotteddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCountSummary(countlist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL =
               " select  (select count(*) as alloted_count from osd_trn_tservicerequest  where request_status='Allotted') as alloted_count, " +
               " (select count(*) as workinprogress_count from osd_trn_tservicerequest  where request_status='Work-In-Progress' and assigned_status<>'Forward') as workinprogress_count, " +
               " (select count(*) as completed_count from osd_trn_tservicerequest  where request_status='Completed') as completed_count, " +
               " (select count(*) as forward_count from osd_trn_tservicerequest  where assigned_status='Forward') as forward_count, " +
               " (select count(*) as closed_count from osd_trn_tservicerequest where request_status='Closed') as closed_count, " +
               " (select count(*) as closed_count from osd_trn_tservicerequest where (request_status='Rejected' or request_status='Cancelled')) as rejectcancel_count, " +
                 " (select count(distinct a.servicerequest_gid) as approvalpending_count " +
                   " from osd_trn_tservicerequest a " +
                   " left join osd_trn_trequestapproval f on f.servicerequest_gid = a.servicerequest_gid " +
                   " where f.approval_status='Pending' and (a.request_status='Allotted' or a.request_status='Work-In-Progress')) as approvalpending_count ";
            }
            else
            {
                msSQL =
             " select  (select count(*) as alloted_count from osd_trn_tservicerequest  where request_status='Allotted' and" +
             " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid=b.department_gid where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as alloted_count, " +
             " (select count(*) as workinprogress_count from osd_trn_tservicerequest  where request_status='Work-In-Progress' and assigned_status<>'Forward' and " +
             " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or  department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid=b.department_gid where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as workinprogress_count, " +
             " (select count(*) as completed_count from osd_trn_tservicerequest  where request_status='Completed' and " +
             " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid=b.department_gid where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as completed_count, " +
             " (select count(*) as forward_count from osd_trn_tservicerequest  where assigned_status='Forward' and " +
             " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid=b.department_gid where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as forward_count, " +
             " (select count(*) as closed_count from osd_trn_tservicerequest where request_status='Closed' and " +
             " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or  department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid=b.department_gid where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as closed_count, " +
             " (select count(*) as closed_count from osd_trn_tservicerequest where (request_status='Rejected' or request_status='Cancelled')and " +
             " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid=b.department_gid where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as rejectcancel_count, " +
             " (select count(distinct a.servicerequest_gid) from osd_trn_tservicerequest a  left join osd_trn_trequestapproval f on f.servicerequest_gid = a.servicerequest_gid where f.approval_status = 'Pending' and (a.request_status='Allotted' or a.request_status='Work-In-Progress') and " +
             " (department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid = '" + employee_gid + "') or department_gid in (select a.department_gid from osd_mst_tactivedepartment2manager a left join osd_mst_tactivedepartment b on a.department_gid = b.department_gid  where a.manager_gid = '" + employee_gid + "' and b.department_status = 'Y'))) as approvalpending_count";
            }
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.alloted_count = objODBCDatareader["alloted_count"].ToString();
                values.workinprogress_count = objODBCDatareader["workinprogress_count"].ToString();
                values.completed_count = objODBCDatareader["completed_count"].ToString();
                values.closed_count = objODBCDatareader["closed_count"].ToString();
                values.forward_count = objODBCDatareader["forward_count"].ToString();
                values.rejectcancel_count = objODBCDatareader["rejectcancel_count"].ToString();
                values.approvalpending_count = objODBCDatareader["approvalpending_count"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostTransferAllocation(transferdtl values, string user_gid)
        {
            msSQL = " select a.requestreopen_gid from osd_trn_treqreopenhistory a" +
                    "  left join osd_trn_tservicerequest b on a.servicerequest_gid = b.servicerequest_gid " +
                            " where b.servicerequest_gid='" + values.servicerequest_gid + "' and a.requestreopen_gid = b.requestreopen_gid";
            //string lsrequestreopen_gid = objdbconn.GetExecuteScalar(msSQL);
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsrequestreopen_gid = objODBCDatareader["requestreopen_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select date_format(assigned_date,'%Y-%m-%d %k:%i:%s') as assigned_date from osd_trn_tservicerequest " +
                             " where servicerequest_gid='" + values.servicerequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreated_date = objODBCDatareader["assigned_date"].ToString();
                if (lscreated_date == null || lscreated_date == "")
                {
                    lscreated_date = null;
                }
                else
                {
                    lscreated_date = lscreated_date;
                }
            }
            objODBCDatareader.Close();
            if (values.remarks != null)
            {
                values.remarks = values.remarks.Replace("'", "");
            }
            
            //if (values.priority == null)
            //{
            //    values.priority = "";
            //}
            //else
            //{
            //    values.priority = values.priority;
            //}

            HashSet<string> validation = new HashSet<string>();
            msGetGid = objcmnfunctions.GetMasterGID("RQTR");
            validation.Add("E");
            validation.Add("");

            if (String.IsNullOrEmpty(msGetGid))
            {
                values.status = false;
                values.message = "Error Occurred while Transferred Request..!";
                return;
            }

            msSQL = "insert into osd_trn_membertransfer(" +
                "requesttransfer_gid," +
                "servicerequest_gid," +
                "assigned_supportteamgid," +
                "assigned_supportteamname," +
                "assigned_membergid," +
                "assigned_membername," +
                "transfer_supportteamgid," +
                "transfer_supportteamname," +
                "transfer_membergid," +
                "transfer_membername," +
                "remarks," +
                "assigned_date," +
                "transfer_by," +
                "transfer_date," +
                "requestreopen_gid)" +
                "values(" +
                "'" + msGetGid + "'," +
                "'" + values.servicerequest_gid + "'," +
                "'" + values.assigned_supportteamgid + "'," +
                "'" + values.assigned_supportteam + "'," +
                "'" + values.assigned_membergid + "'," +
                "'" + values.assigned_member + "'," +
                "'" + values.transferteam_gid + "'," +
                "'" + values.transferteam_name + "'," +
                "'" + values.transferemployee_gid + "'," +
                "'" + values.transferemployee_name + "'," +
                "'" + values.remarks.Replace("'", "") + "'," +
                "'" + lscreated_date + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + lsrequestreopen_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occurred while Transferred Request..!";
                return;
            }
            if (mnResult == 1)
            {
                msSQL = " update osd_trn_tservicerequest set assigned_membergid = '" + values.transferemployee_gid + "', " +
                    "assigned_membername = '" + values.transferemployee_name + "', " +
                    "assigned_supportteamgid = '" + values.transferteam_gid + "', " +
                    "assigned_supportteamname = '" + values.transferteam_name + "', " +
                    "assigned_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " assigned_status='Transferred'," +
                    " transfer_flag='Y'" +
                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select activity_name from osd_mst_tactivitymaster where activitymaster_gid='" + values.activitymaster_gid + "'";
                string lsactivity_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update osd_trn_tservicerequestactivityhistory set activity_flag = 'N'" +
                  " where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msGetGidsactivity = objcmnfunctions.GetMasterGID("RQTR");
                msSQL = "insert into osd_trn_tservicerequestactivityhistory(" +
                   "servicerequestactivityhistory_gid," +
                   "servicerequest_gid," +
                   "activitymaster_gid," +
                   "activity_name," +
                   "activity_flag," +
                   "created_by," +
                   "created_date)" +
                   "values(" +
                   "'" + msGetGidsactivity + "'," +
                   "'" + values.servicerequest_gid + "'," +
                   "'" + values.activitymaster_gid + "', " +
                   "'" + lsactivity_name + "'," +
                   "'Y'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update osd_trn_tservicerequest set activity_name = '" + lsactivity_name + "',activitymaster_gid='"+ values.activitymaster_gid +"'" +
           " where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update osd_trn_tbankalert2allocated set assigned_to ='" + values.transferteam_gid + "'," +
                 " assigned_toname = '" + values.transferemployee_name + "'," +
                 " updated_by='" + user_gid + "'," +
                    " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where servicerequest_gid = '" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Request Transferred Successfully..!";


                    // Completed Mail
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            frommail_id = objODBCDatareader["company_mail"].ToString();
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();

                        }
                        objODBCDatareader.Close();

                        string lsdepartmentgid;
                        lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                        msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                        cc = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                                " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select request_refno,activity_name,request_title, b.transfer_date,a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                                " concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) as transfered_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                                " date_format(b.transfer_date, '%d-%m-%Y %h:%i %p') as transferdate,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date  " +
                                " from osd_trn_tservicerequest a " +
                                " left join osd_trn_membertransfer b on b.servicerequest_gid = a.servicerequest_gid " +
                                " left join adm_mst_tuser f on f.user_gid = b.transfer_by " +
                                 " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                              " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                              " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                                " where a.servicerequest_gid='" + values.servicerequest_gid + "' Order by b.transfer_date desc ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                            lsactivity_name = objODBCDatareader["activity_name"].ToString();
                            request_title = objODBCDatareader["request_title"].ToString();
                            lstransferbydtl = objODBCDatareader["transfered_by"].ToString();
                            lstransferondtl = objODBCDatareader["transferdate"].ToString();                         
                            lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                            lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                            lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                            lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                            lsrequest_status = objODBCDatareader["request_status"].ToString();
                            assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                            lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                            lsrequest_description = objODBCDatareader["request_description"].ToString();
                        }
                        objODBCDatareader.Close();
                         string lsemployee_gid;
                        lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + user_gid + "'");

                        msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                        lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                  "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                  "  from adm_mst_tmodule2employee a " +
                  "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                  "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
                  "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                  "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                  "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                  "  where a.module_gid ='"+ lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
                  "  group by a.employeereporting_to";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                            lslevel_zero = objODBCDatareader["level_zero"].ToString();
                            lslevel_one = objODBCDatareader["level_one"].ToString();
                            lsemployee_number = objODBCDatareader["employee_number"].ToString();
                            //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                            //values.level_zero = objODBCDatareader["level_zero"].ToString();
                            //values.level_one = objODBCDatareader["level_one"].ToString();
                            //values.employee_number = objODBCDatareader["employee_number"].ToString();
                        }
                            //sub = " Service Request Transferred ";
                            sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Transferred ";

                        lscontent = HttpUtility.HtmlEncode(values.content);

                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "The service ticket is transferred to you, the details are as follows,<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                        body = body + "<br />";                        
                        //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                        //body = body + "<br />";
                        body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Transferred By  :</b> " + HttpUtility.HtmlEncode(lstransferbydtl) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Transferred On  :</b> " + HttpUtility.HtmlEncode(lstransferondtl)+ "<br />";
                        body = body + "<br />";                      
                        body = body + " click the link to enter the web portal and respond <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                        body = body + "<br />";
                        body = body + "<b>Thanks & Regards, </b><br/> ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(lstransferbydtl) + "<br >";
                        body = body + "<br />";

                        //cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        //message.CC.Add(cc);


                        if (cc != null & cc != string.Empty & cc != "")
                        {
                            lsCCReceipients = cc.Split(',');
                            if (cc.Length == 0)
                            {
                                message.CC.Add(new MailAddress(cc));
                            }
                            else
                            {
                                foreach (string CCEmail in lsCCReceipients)
                                {
                                    message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                }
                            }
                        }

                        message.Subject = sub;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = body;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                        if (values.status == true)
                        {
                            msSQL = "Insert into osd_trn_tmailcount( " +
                            " servicerequest_gid," +
                            " from_mail," +
                            " to_mail," +
                            //" cc_mail," +
                            " mail_status," +
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.servicerequest_gid + "'," +
                            "'" + ls_username + "'," +
                            "'" + tomail_id + "'," +
                            //"'" + lscc_mail + "'," +
                            "'Service Request Transferred'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    catch (Exception ex)
                    {
                        values.message = " Mail Not Sent ";
                        values.status = false;
                    }

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostSelfAllocation(allocatedtl values, string user_gid)
        {
            msSQL = " select a.requestreopen_gid from osd_trn_treqreopenhistory a" +
                    "  left join osd_trn_tservicerequest b on a.servicerequest_gid = b.servicerequest_gid " +
                            " where b.servicerequest_gid='" + values.servicerequest_gid + "' and a.requestreopen_gid = b.requestreopen_gid";
            //string lsrequestreopen_gid = objdbconn.GetExecuteScalar(msSQL);
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsrequestreopen_gid = objODBCDatareader["requestreopen_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select date_format(assigned_date,'%Y-%m-%d %k:%i:%s') as assigned_date from osd_trn_tservicerequest " +
                             " where servicerequest_gid='" + values.servicerequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscreated_date = objODBCDatareader["assigned_date"].ToString();
                if (lscreated_date == null || lscreated_date == "")
                {
                    lscreated_date = null;
                }
                else
                {
                    lscreated_date = lscreated_date;
                }
            }
            objODBCDatareader.Close();
            if (values.remarks != null)
            {
                values.remarks = values.remarks.Replace("'", "");
            }
           string  lsins_refno = "RQAL" + DateTime.Now.ToString("yyyyMMdd");
            string msGETRef1 = objcmnfunctions.GetMasterGID("RQAL");
            msGETRef1 = msGETRef1.Replace("RQAL", "");
            lsins_refno = lsins_refno + msGETRef1;

            //msGetGid = objcmnfunctions.GetMasterGID('');
            string lsmsGetGid = "RQAL" + DateTime.Now.ToString("yyyyMMdd");
            msSQL = "insert into osd_trn_tmanagertransfer(" +
                "requestallocated_gid," +
                "servicerequest_gid," +                
                "assigned_membergid," +
                "assigned_membername," +
                "allocate_managergid," +
                "allocate_managername," +
                "remarks," +
                "assigned_date," +
                "allocate_by," +
                "allocate_date," +
                "requestreopen_gid)" +
                "values(" +
                "'" + lsins_refno + "'," +
                "'" + values.servicerequest_gid + "'," +            
                "'" + values.assigned_membergid + "'," +
                "'" + values.assigned_membername + "'," +              
                "'" + values.allocate_managergid + "'," +
                "'" + values.allocate_managername + "'," +
                "'" + values.remarks.Replace("'", "") + "'," +
                "'" + lscreated_date + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + lsrequestreopen_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " update osd_trn_tservicerequest set assigned_membergid = '" + values.allocate_managergid + "', " +
                    "assigned_membername = '" + values.allocate_managername + "', " +                  
                    "assigned_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " assigned_status='Self Allocated'," +
                    " transfer_flag='Y'" +
                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
               
               
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "update osd_trn_tbankalert2allocated set assigned_to ='" + values.allocate_managergid + "'," +
                 " assigned_toname = '" + values.allocate_managername + "'," +
                 " updated_by='" + user_gid + "'," +
                    " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where servicerequest_gid = '" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Request Allocated Successfully..!";


                    // Completed Mail
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            frommail_id = objODBCDatareader["company_mail"].ToString();
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();

                        }
                        objODBCDatareader.Close();

                        string lsdepartmentgid;
                        lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                        msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                        cc = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                                " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select a.request_refno,a.activity_name,a.request_title,a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                                " concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) as allocate_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                                " date_format(b.allocate_date, '%d-%m-%Y %h:%i %p') as allocatedate,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date  " +
                                " from osd_trn_tservicerequest a " +
                                " left join osd_trn_tmanagertransfer b on b.servicerequest_gid = a.servicerequest_gid " +
                                " left join adm_mst_tuser f on f.user_gid = b.allocate_by " +
                                 " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                              " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                              " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                                " where a.servicerequest_gid='" + values.servicerequest_gid + "' Order by b.allocate_date desc ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                            lsactivity_name = objODBCDatareader["activity_name"].ToString();
                            request_title = objODBCDatareader["request_title"].ToString();
                            lstransferbydtl = objODBCDatareader["allocate_by"].ToString();
                            lstransferondtl = objODBCDatareader["allocatedate"].ToString();
                            lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                            lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                            lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                            lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                            lsrequest_status = objODBCDatareader["request_status"].ToString();
                            assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                            lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                            lsrequest_description = objODBCDatareader["request_description"].ToString();
                        }
                        objODBCDatareader.Close();
                        string lsemployee_gid;
                        lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + user_gid + "'");

                        msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                        lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                  "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                  "  from adm_mst_tmodule2employee a " +
                  "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                  "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
                  "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                  "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                  "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                  "  where a.module_gid ='"+ lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
                  "  group by a.employeereporting_to ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                            lslevel_zero = objODBCDatareader["level_zero"].ToString();
                            lslevel_one = objODBCDatareader["level_one"].ToString();
                            lsemployee_number = objODBCDatareader["employee_number"].ToString();
                            //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                            //values.level_zero = objODBCDatareader["level_zero"].ToString();
                            //values.level_one = objODBCDatareader["level_one"].ToString();
                            //values.employee_number = objODBCDatareader["employee_number"].ToString();
                        }
                        //sub = " Service Request Transferred ";
                        sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Self Allocated ";

                        lscontent = HttpUtility.HtmlEncode(values.content);
                        lsallocatemanagername = HttpUtility.HtmlEncode(values.allocate_managername);

                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "The service ticket is self allocated to you, the details are as follows,<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Raised Date :</b> " + HttpUtility.HtmlEncode(lsRaised_Date) + "<br />";
                        body = body + "<br />";
                        //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                        //body = body + "<br />";
                        body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Assigned Manager :</b>" + HttpUtility.HtmlEncode(lsallocatemanagername) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Transferred By  :</b> " + HttpUtility.HtmlEncode(lstransferbydtl) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Transferred On  :</b> " + HttpUtility.HtmlEncode(lstransferondtl) + "<br />";
                        body = body + "<br />";
                        body = body + " click the link to enter the web portal and respond <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                        body = body + "<br />";
                        body = body + "<b>Thanks & Regards, </b><br/> ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(lstransferbydtl) + "<br >";
                        body = body + "<br />";

                        //cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        lsbcc = ConfigurationManager.AppSettings["osdbcc"].ToString();
                        //message.CC.Add(cc);
                        if (lsbcc != null & lsbcc != string.Empty & lsbcc != "")
                        {
                            lsBCCReceipients = lsbcc.Split(',');
                            if (lsbcc.Length == 0)
                            {
                                message.Bcc.Add(new MailAddress(cc));
                            }
                            else
                            {
                                foreach (string BCCEmail in lsBCCReceipients)
                                {
                                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple CC email Id
                                }
                            }
                        }


                        if (cc != null & cc != string.Empty & cc != "")
                        {
                            lsCCReceipients = cc.Split(',');
                            if (cc.Length == 0)
                            {
                                message.CC.Add(new MailAddress(cc));
                            }
                            else
                            {
                                foreach (string CCEmail in lsCCReceipients)
                                {
                                    message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                }
                            }
                        }

                        message.Subject = sub;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = body;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                        if (values.status == true)
                        {
                            msSQL = "Insert into osd_trn_tmailcount( " +
                            " servicerequest_gid," +
                            " from_mail," +
                            " to_mail," +
                            //" cc_mail," +
                            " mail_status," +
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + values.servicerequest_gid + "'," +
                            "'" + ls_username + "'," +
                            "'" + tomail_id + "'," +
                            //"'" + lscc_mail + "'," +
                            "'Service Request Transferred'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    catch (Exception ex)
                    {
                        values.message = " Mail Not Sent ";
                        values.status = false;
                    }

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
        public void DaPostPriority(transferdtl values, string user_gid)
        {
            msSQL = " select a.requestreopen_gid from osd_trn_treqreopenhistory a" +
                    "  left join osd_trn_tservicerequest b on a.servicerequest_gid = b.servicerequest_gid " +
                            " where b.servicerequest_gid='" + values.servicerequest_gid + "' and a.requestreopen_gid = b.requestreopen_gid";
            //string lsrequestreopen_gid = objdbconn.GetExecuteScalar(msSQL);
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsrequestreopen_gid = objODBCDatareader["requestreopen_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " update osd_trn_tpriority set priority_flag = 'N'" +                 
                     " where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msGetGid = objcmnfunctions.GetMasterGID("RQTR");

            msSQL = "insert into osd_trn_tpriority(" +
                "priority_gid," +
                "servicerequest_gid," +
                "priority," +
                "priority_flag," +
                "created_by," +
                "created_date)" +
                "values(" +
                "'" + msGetGid + "'," +
                "'" + values.servicerequest_gid + "'," +
                "'" + values.priority + "'," +
                "'Y'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
            values.message = "Priority Added Successfully";

        }
        public void DaGetActivityEdit(transferdtl values, string employee_gid, string servicerequest_gid)
        {
            msSQL = " select activitymaster_gid,activity_name from osd_trn_tservicerequest where servicerequest_gid='" + servicerequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.activitymaster_gid = objODBCDatareader["activitymaster_gid"].ToString();
                values.activity_name = objODBCDatareader["activity_name"].ToString();
              
                  
                }

            objODBCDatareader.Close();
        }

        public void DaGetTransferMemberlist(transferlist values, string employee_gid, string servicerequest_gid)
        {
            msSQL = " select a.transfer_supportteamname, a.transfer_membername,a.transfer_supportteamgid, a.transfer_membergid, a.transfer_date, " +
                    " a.assigned_supportteamname, a.assigned_membername, date_format(a.assigned_date,'%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " b.transfer_flag from osd_trn_membertransfer a left join osd_trn_tservicerequest b on a.servicerequest_gid=b.servicerequest_gid  " +
                    " where a.servicerequest_gid = '" + servicerequest_gid + "' and a.requestreopen_gid=''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransferlistdtl = new List<transferlistdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettransferlistdtl.Add(new transferlistdtl
                    {
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_supportteamname = dt["transfer_supportteamname"].ToString(),
                        transfer_membername = dt["transfer_membername"].ToString(),
                        transfer_supportteamgid = dt["transfer_supportteamgid"].ToString(),
                        transfer_membergid = dt["transfer_membergid"].ToString(),
                        transfer_date = dt["transfer_date"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        assigned_date = dt["assigned_date"].ToString()
                    });
                    values.transferlistdtl = gettransferlistdtl;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select a.transfer_supportteamname, a.transfer_membername,a.transfer_supportteamgid, a.transfer_membergid, a.transfer_date, " +
                   " a.assigned_supportteamname, a.assigned_membername, date_format(a.assigned_date,'%d-%m-%Y %h:%i %p') as assigned_date, " +
                   " b.transfer_flag from osd_trn_membertransfer a left join osd_trn_tservicerequest b on a.servicerequest_gid=b.servicerequest_gid  " +
                   " where a.servicerequest_gid = '" + servicerequest_gid + "' and a.requestreopen_gid = b.requestreopen_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransferlistdtlreopen = new List<transferlistdtlreopen>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettransferlistdtlreopen.Add(new transferlistdtlreopen
                    {
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_supportteamname = dt["transfer_supportteamname"].ToString(),
                        transfer_membername = dt["transfer_membername"].ToString(),
                        transfer_supportteamgid = dt["transfer_supportteamgid"].ToString(),
                        transfer_membergid = dt["transfer_membergid"].ToString(),
                        transfer_date = dt["transfer_date"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        assigned_date = dt["assigned_date"].ToString()
                    });
                    values.transferlistdtlreopen = gettransferlistdtlreopen;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetPrioritylist(transferlist values, string employee_gid, string servicerequest_gid)
        {
            msSQL = " select  a.priority, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by from osd_trn_tpriority a left join osd_trn_tservicerequest b on a.servicerequest_gid=b.servicerequest_gid  left join adm_mst_tuser c on a.created_by=c.user_gid " +
                    " where a.servicerequest_gid = '" + servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransferlistdtl = new List<prioritylistdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettransferlistdtl.Add(new prioritylistdtl
                    {
                        priority = dt["priority"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),

                    });
                    values.prioritylistdtl = gettransferlistdtl;
                }
            }
            dt_datatable.Dispose();

           
        }

        public void DaGetAllForwardSummary(forwardlist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,a.assigned_supportteamname,a.request_refno ," +
                   " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging," +
                  " a.transfer_flag, a.forward_to, a.reopen_flag,a.ticket_source as source ,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, a.bankalert_flag, bankalert2allocated_gid, customer_gid from osd_trn_tservicerequest a " +
                 " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where ( a.assigned_status='Forward') order by a.created_date asc ";
            }
            else
            {
                msSQL = "  select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,a.assigned_supportteamname,a.request_refno, " +
                 " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging, " +
                 "  a.transfer_flag, a.forward_to, a.reopen_flag,a.ticket_source as source ,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, a.bankalert_flag, bankalert2allocated_gid, customer_gid from osd_trn_tservicerequest a " +
                  " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   " where(a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid= '" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " and ( a.assigned_status='Forward') and e.department_status='Y'  order by a.created_date asc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<forwarddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getallotteddtl.Add(new forwarddtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        forward_to = dt["forward_to"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()


                    });
                    values.forwarddtl = getallotteddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public bool txtfile(servicerequest values, string employee_gid)
        {
            //string lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/test.txt";
            string lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/test.txt";
            string lscompany_code = string.Empty;

            lspath = lspath.Replace("/", "\\");
            msSQL = " select company_code from adm_mst_tcompany";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
           
            MemoryStream ms = new MemoryStream();
            bool status;
           
            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/Text/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lscompany_code, ms);

            ms.Close();
            lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/Text/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            if (!File.Exists(@lspath))
            {
                using (StreamWriter sw = File.CreateText(lspath))
                {
                    msSQL = " SELECT remarks, created_date FROM osd_trn_trequestorcommunication where servicerequest_gid='SERQ20201228206' ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            //Write a line of text
                            sw.WriteLine(dr_datarow["remarks"].ToString() + dr_datarow["created_date"].ToString() + "---");
                        }
                    }
                    dt_datatable.Dispose();
                }
            }




            //StreamWriter sw = new StreamWriter("F:\\Test.txt");
            //msSQL = " SELECT state_gid,state_name FROM ocs_mst_tstate ";



            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getState = new List<state_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        //Write a line of text
            //        sw.WriteLine(dr_datarow["state_gid"].ToString(),"   ", dr_datarow["state_gid"].ToString());
            //    }



            //}




            //Write a second line of text
            //sw.WriteLine("From the StreamWriter class");
            //Close the file
            //sw.Close();
            values.status = true;
            return true;
        }

        public void DaGetApprovalPendingSummary(servicerequestdtllist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " select distinct a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.request_status,a.request_refno, a.assigned_supportteamname,a.department_gid, " +
                   " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging," +
                   " a.assigned_membergid, a.assigned_supportteamgid, a.transfer_flag, a.reopen_flag,a.ticket_source as source , bankalert_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, bankalert2allocated_gid, customer_gid " +
                   "  from osd_trn_tservicerequest a " +
                   "  left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   "  left join osd_trn_trequestapproval f on f.servicerequest_gid = a.servicerequest_gid " +
                   " where f.approval_status='Pending' and (a.request_status='Allotted' or a.request_status='Work-In-Progress') order by a.created_date asc ";
            }
            else
            {
                msSQL = " select distinct a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,a.raised_department as department_name,a.department_name as departmentname,a.request_status,a.request_refno, a.assigned_supportteamname,a.department_gid, " +
                   " a.assigned_membername, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.assigned_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.assigned_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.assigned_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,  " +
                   " a.assigned_membergid, a.assigned_supportteamgid, a.transfer_flag, a.reopen_flag,a.ticket_source as source , bankalert_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status,bankalert2allocated_gid, customer_gid " +
                  "  from osd_trn_tservicerequest a " +
                  "  left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                  "  left join osd_trn_trequestapproval f on f.servicerequest_gid = a.servicerequest_gid " +
                   " where ( a.department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " and f.approval_status='Pending' and (a.request_status='Allotted' or a.request_status='Work-In-Progress') and e.department_status='Y' order by a.created_date asc ";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestList = new List<servicerequestdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestList.Add(new servicerequestdtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_description = dt["request_description"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        assigned_membergid = dt["assigned_membergid"].ToString(),
                        assigned_supportteamgid = dt["assigned_supportteamgid"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()


                    });
                    values.servicerequestdtl = getservicerequestList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetManagerSummary(managerdtllist values, string department_gid)
        {
            msSQL = " select department_gid, department_name, manager_gid, manager_name from osd_mst_tactivedepartment2manager where department_gid ='" + department_gid + "'";
                   
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmanagerdtllist = new List<managerdtllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getmanagerdtllist.Add(new managerdtllist
                    {
                        manager_gid = dt["manager_gid"].ToString(),
                        manager_name = dt["manager_name"].ToString()
                      
                    });
                    values.managerdtl = getmanagerdtllist;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetAllocateManagerlist(selfallocatelist values, string employee_gid, string servicerequest_gid)
        {
            msSQL = " select  a.assigned_membergid,a.assigned_membername, a.allocate_by, a.allocate_date, " +
                    " a.allocate_managername, a.allocate_managergid, date_format(a.assigned_date,'%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " b.transfer_flag from osd_trn_tmanagertransfer a left join osd_trn_tservicerequest b on a.servicerequest_gid=b.servicerequest_gid  " +
                    " where a.servicerequest_gid = '" + servicerequest_gid + "' and a.requestreopen_gid=''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallocatedlistdtl = new List<allocatelistdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getallocatedlistdtl.Add(new allocatelistdtl
                    {
                        assigned_membergid = dt["assigned_membergid"].ToString(),
                        assigned_membername = dt["assigned_membername"].ToString(),
                        allocate_managername = dt["allocate_managername"].ToString(),
                        allocate_managergid = dt["allocate_managergid"].ToString(),
                        allocate_by = dt["allocate_by"].ToString(),
                        allocate_date = dt["allocate_date"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        assigned_date = dt["assigned_date"].ToString()
                    });
                    values.allocatelistdtl = getallocatedlistdtl;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select  a.assigned_membergid,a.assigned_membername, a.allocate_by, a.allocate_date, " +
                    " a.allocate_managername, a.allocate_managergid, date_format(a.assigned_date,'%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " b.transfer_flag from osd_trn_tmanagertransfer a left join osd_trn_tservicerequest b on a.servicerequest_gid=b.servicerequest_gid  " +
                   " where a.servicerequest_gid = '" + servicerequest_gid + "' and a.requestreopen_gid = b.requestreopen_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallocatelistdtlreopen = new List<allocatelistdtlreopen>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getallocatelistdtlreopen.Add(new allocatelistdtlreopen
                    {
                        assigned_membergid = dt["assigned_membergid"].ToString(),
                        assigned_membername = dt["assigned_membername"].ToString(),
                        allocate_managername = dt["allocate_managername"].ToString(),
                        allocate_managergid = dt["allocate_managergid"].ToString(),
                        allocate_by = dt["allocate_by"].ToString(),
                        allocate_date = dt["allocate_date"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        assigned_date = dt["assigned_date"].ToString()
                    });
                    values.allocatelistdtlreopen = getallocatelistdtlreopen;
                }
            }
            dt_datatable.Dispose();
        }


    }

   

    
}