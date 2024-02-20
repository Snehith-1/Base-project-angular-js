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
using System.Net;
using System.Net.Mail;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnMyTicket
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        OdbcDataReader objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid;
        HttpPostedFile httpPostedFile;
        string lspath, lssession_user, lsdocument_attached, message;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, body, sub, lscontent = string.Empty;
        string lscc_mail, lsstatus, count, approve_count, reject_count, app_count;
        int mnResult, ls_port, MailFlag, k;
        string lsrequest_refno, lsactivity_name, lsrequest_title, lsresponse_flag, lsrequested_by, lsraisedondtl, request_title;
        string lsgetapproval_remarks, lsassigndedtl, lsraiseddtl, lsforwardbydtl, lsforwardondtl, lscompletedbydtl, lscompletedondtl, lsrejectedbydtl, lsrejectedondtl, lsacknowledgedbydtl, lsacknowledgedondtl, lsrequest_description, lsassigned_membername, assigned_supportteamname, lsrequest_status;
        string lssupportteam_name, lssupportteam_gid, lsmember_name, lsmember_gid, lsgetforwardapproval_remarks, lsRaised_By, lsRaisedNo, lsBaselocation_Name, lsRaised_Date, lsemployee_mobileno, lslevel_zero, lslevel_one, lsemployee_number;
        string sToken = string.Empty;
        Random rand = new Random();
        string[] lsCCReceipients;
        string cc;
        string cc_mailid = string.Empty;
        string lscreated_by, lsemployee_gid, lsstatus_updatedby;
        string lsrequestreopen_gid;
        string lsmodulereportingto_gid;
       

        public void DaGetAllottedSummary(allottedlist values, string employee_gid)
        {
            msSQL = "  select distinct a.servicerequest_gid,a.activitymaster_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno , " +
                   " getapproval_flag,assigned_supportteamname,assigned_membername,raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,CASE WHEN request_status='Allotted' THEN CONCAT(FLOOR(timestampdiff(day, a.created_date,now())), ' days ',MOD(timestampdiff(hour, a.created_date,now()),'24'), ' Hrs ',MOD(timestampdiff(minute, a.created_date,now()),'60'), 'Mins') END as aging , " +
                    " assigned_membergid, assigned_supportteamgid, bankalert_flag, a.ticket_source as source , " +
             " (select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, bankalert2allocated_gid,customer_gid,(CASE WHEN (f.priority IS null  OR f.priority='')THEN 'None' ELSE f.priority END) AS priority from osd_trn_tservicerequest a " +                   
                   " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid  " +
                   " left join osd_trn_tpriority f on f.servicerequest_gid = a.servicerequest_gid AND f.priority_flag='Y'  " +
                   " where a.request_status='Allotted'  and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' order by a.created_date asc  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<allotteddtl>();
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
                                msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where approval_status = 'Rejected' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                                "as reject_count, (select count(*) from osd_trn_trequestapproval where approval_status = 'Approved' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "') as approve_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    count = objODBCDatareader["count"].ToString();
                                    approve_count = objODBCDatareader["approve_count"].ToString();
                                    reject_count = objODBCDatareader["reject_count"].ToString();
                                    if (count == approve_count)
                                    {
                                        lsstatus = "Approved";
                                    }
                                    else if (Convert.ToInt16(reject_count) != 0)
                                    {
                                        lsstatus = "Rejected";
                                    }
                                    else
                                    {
                                        lsstatus = "Pending";
                                    }
                                }
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

                    msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where ( approval_status = 'Cancelled') and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            "as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        count = objODBCDatareader["count"].ToString();
                        app_count = objODBCDatareader["app_count"].ToString();
                        if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                        {

                            lsstatus = "NotInitiated";
                        }
                    }
                    objODBCDatareader.Close();

                    //msSQL = " SELECT servicerequest_gid FROM osd_trn_trequestorcommunication " +
                    //     " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND response_new='Y' and created_by<>'" + employee_gid + "'";
                    //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //if (objODBCDatareader.HasRows == true)
                    //{
                    //    lsresponse_flag = "Y";
                    //}
                    //else
                    //{
                    //    lsresponse_flag = "N";
                    //}
                    //objODBCDatareader.Close();

                    msSQL = " SELECT servicerequest_gid FROM osd_trn_tservicerequest " +
                         " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND assignedmember_flag='Y' and created_by<>'" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }

                    if (lsstatus == "NotInitiated" || lsstatus == "Completed")
                    {
                        getallotteddtl.Add(new allotteddtl
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
                            request_refno = dt["request_refno"].ToString(),
                            assigned_team = dt["assigned_supportteamname"].ToString(),
                            getapproval_flag = dt["getapproval_flag"].ToString(),
                            approval_status = lsstatus,
                            response_flag = lsresponse_flag,
                            assigned_membergid = dt["assigned_membergid"].ToString(),
                            assigned_supportteamgid = dt["assigned_supportteamgid"].ToString(),
                            assigned_member = dt["assigned_membername"].ToString(),
                            bankalert_flag = dt["bankalert_flag"].ToString(),
                            bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                            activitymaster_gid = dt["activitymaster_gid"].ToString(),
                            Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                            priority = dt["priority"].ToString(),
                            aging = dt["aging"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            source = dt["source"].ToString()
                        });
                        values.allotteddtl = getallotteddtl;
                    }
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetWorkInProgressSummary(workinprogresslist values, string employee_gid)
        {
            msSQL = " select distinct a.servicerequest_gid,activity_name,a.activitymaster_gid,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                         " assigned_supportteamname,assigned_membername,raised_by as raisedby,CASE WHEN request_status='Work-In-Progress' THEN CONCAT(FLOOR(timestampdiff(day, a.created_date,now())),' days ',MOD(timestampdiff(hour, a.created_date,now()),'24'), ' Hrs ',MOD(timestampdiff(minute, a.created_date,now()),'60'),'Mins') END as aging , " +
                         " getapproval_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                         " assigned_membergid, assigned_supportteamgid, reopen_flag, a.ticket_source as source, bankalert_flag, bankalert2allocated_gid,customer_gid,(CASE WHEN (f.priority IS null  OR f.priority='')THEN 'None' ELSE f.priority END) AS priority from osd_trn_tservicerequest a  " +
                         " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                         " left join osd_trn_tpriority f on f.servicerequest_gid = a.servicerequest_gid AND f.priority_flag='Y' " +
                         " where a.request_status='Work-In-Progress' and a.assigned_status<>'Forward' and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' order by a.created_date asc  ";
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

                    msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where ( approval_status= 'Cancelled') and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                         "as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        count = objODBCDatareader["count"].ToString();
                        app_count = objODBCDatareader["app_count"].ToString();
                        if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                        {

                            lsstatus = "NotInitiated";
                        }
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT servicerequest_gid FROM osd_trn_tservicerequest " +
                             " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND assignedmember_flag='Y' and created_by<>'" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
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
                            request_refno = dt["request_refno"].ToString(),
                            assigned_team = dt["assigned_supportteamname"].ToString(),
                            getapproval_flag = dt["getapproval_flag"].ToString(),
                            approval_status = lsstatus,
                            response_flag = lsresponse_flag,
                            assigned_membergid = dt["assigned_membergid"].ToString(),
                            assigned_supportteamgid = dt["assigned_supportteamgid"].ToString(),
                            assigned_to = dt["assigned_membername"].ToString(),
                            reopen_flag = dt["reopen_flag"].ToString(),
                            Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                            bankalert_flag = dt["bankalert_flag"].ToString(),
                            bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                            priority = dt["priority"].ToString(),
                            aging = dt["aging"].ToString(),
                            activitymaster_gid = dt["activitymaster_gid"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            source = dt["source"].ToString()
                        });
                        values.workinprogressdtl = getallotteddtl;
                    }
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetCompletedSummary(completedlist values, string employee_gid)
        {
            msSQL = " select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description, a.ticket_source as source,raised_department as department_name,a.department_name as departmentname,request_status,request_refno,a.department_gid, " +
                     " a.assigned_supportteamname,raised_by as raisedby,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,CASE WHEN request_status = 'Completed' THEN CONCAT(FLOOR(timestampdiff(day, a.created_date, now())),' days ',MOD(timestampdiff(hour, a.created_date, now()), '24'), ' Hrs ',MOD(timestampdiff(minute, a.created_date, now()), '60'), 'Mins') END as aging, " +
                     " a.reopen_flag, a.bankalert_flag, bankalert2allocated_gid, customer_gid from osd_trn_tservicerequest a " +
                     " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                     " where a.request_status='Completed' and a.assigned_membergid='" + employee_gid + "'  and e.department_status='Y'  order by a.created_date asc ";
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
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        aging = dt["aging"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.completeddtl = getcompleteddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetClosedSummary(closedlist values, string employee_gid)
        {
            msSQL = " select a.servicerequest_gid,a.activity_name,a.request_title,a.request_description,raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                    " a.assigned_supportteamname, raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,CASE WHEN request_status = 'Closed' THEN CONCAT(FLOOR(timestampdiff(day, a.created_date, a.closed_date)),  ' days ',MOD(timestampdiff(hour, a.created_date, a.closed_date), '24'), ' Hrs ',MOD(timestampdiff(minute, a.created_date, a.closed_date), '60'),  'Mins') when request_status = 'Rejected' THEN CONCAT(FLOOR(timestampdiff(day, a.created_date, a.rejected_date)),  ' days ',MOD(timestampdiff(hour, a.created_date, a.rejected_date), '24'), ' Hrs ',MOD(timestampdiff(minute, a.created_date, a.rejected_date), '60'), 'Mins') END as aging, " +
                   "  a.reopen_flag, a.bankalert_flag, a.ticket_source as source ,a.bankalert2allocated_gid, (select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status,customer_gid from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where (a.request_status='Closed' or a.request_status='Rejected' or a.request_status='Cancelled') and a.assigned_membergid='" + employee_gid + "'  and e.department_status='Y'  order by a.created_date asc ";
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
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        aging = dt["aging"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.closeddtl = getallotteddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetAllotted360(string servicerequest_gid, allotteddtl values)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,d.department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                     " assigned_supportteamname, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as raisedby, " +
                     " assigned_membername,assigned_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,if(request_status='Completed',CONCAT(FLOOR((DATEDIFF(a.created_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.created_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.created_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') )  as reopened_aging," +
                     " rejected_remarks,date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as rejected_by," +
                     " forward_remarks, date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date, forward_to, forward_flag, completed_flag, rejected_flag, cancel_flag," +
                     " date_format(a.cancel_date,'%d-%m-%Y %h:%i %p') as cancel_date, transfer_flag, reopen_flag, reopen_reason, date_format(a.reopened_date,'%d-%m-%Y %h:%i %p') as reopened_date,h.baselocation_name, a.created_by as requestraisedby_gid  from osd_trn_tservicerequest a " +
                     " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                     " left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                     " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                     " left join adm_mst_tuser e on a.rejected_by = e.user_gid " +
                     "left join sys_mst_tbaselocation h on h.baselocation_gid=c.baselocation_gid " +
                     " where a.servicerequest_gid='" + servicerequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values.activity_name = objODBCDatareader["activity_name"].ToString();
                values.request_title = objODBCDatareader["request_title"].ToString();
                values.raised_by = objODBCDatareader["raisedby"].ToString();
                values.raised_department = objODBCDatareader["department_name"].ToString();
                values.departmentname = objODBCDatareader["departmentname"].ToString();
                values.department_gid = objODBCDatareader["department_gid"].ToString();
                values.request_status = objODBCDatareader["request_status"].ToString();
                values.raised_date = objODBCDatareader["raiseddate"].ToString();
                values.request_refno = objODBCDatareader["request_refno"].ToString();
                values.assigned_team = objODBCDatareader["assigned_supportteamname"].ToString();
                values.assigned_member = objODBCDatareader["assigned_membername"].ToString();
                values.request_description = objODBCDatareader["request_description"].ToString();
                values.assigned_status = objODBCDatareader["assigned_status"].ToString();
                values.forward_flag = objODBCDatareader["forward_flag"].ToString();
                values.forward_remarks = objODBCDatareader["forward_remarks"].ToString();
                values.forward_date = objODBCDatareader["forward_date"].ToString();
                values.forward_to = objODBCDatareader["forward_to"].ToString();
                values.transfer_flag = objODBCDatareader["transfer_flag"].ToString();
                values.reopen_flag = objODBCDatareader["reopen_flag"].ToString();
                values.reopen_reason = objODBCDatareader["reopen_reason"].ToString();
                values.reopened_date = objODBCDatareader["reopened_date"].ToString();
                values.servicerequest_gid = objODBCDatareader["servicerequest_gid"].ToString();
                values.completed_flag = objODBCDatareader["completed_flag"].ToString();
                values.rejected_flag = objODBCDatareader["rejected_flag"].ToString();
                values.rejected_remarks = objODBCDatareader["rejected_remarks"].ToString();
                values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                values.cancel_date = objODBCDatareader["cancel_date"].ToString();
                values.cancel_flag = objODBCDatareader["cancel_flag"].ToString();
                values.reopened_aging = objODBCDatareader["reopened_aging"].ToString();
                values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                values.requestraisedby_gid = objODBCDatareader["requestraisedby_gid"].ToString();

            }
            objODBCDatareader.Close();

            //msSQL = " update osd_trn_tservicerequest set assignedmember_flag='' where " +
            //        " servicerequest_gid='" + servicerequest_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            // When Message read Change Status
            //msSQL = "select response_new,requestor_gid from osd_trn_trequestorcommunication where servicerequest_gid='" + servicerequest_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        msSQL = " update osd_trn_trequestorcommunication set response_new='' where response_new='Y' and " +
            //                " servicerequest_gid='" + servicerequest_gid + "'";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //dt_datatable.Dispose();

            msSQL = "select created_by from osd_trn_tservicerequest" +
                 " where servicerequest_gid='" + servicerequest_gid + "'";
            lscreated_by = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select employee_gid from hrm_mst_temployee where user_gid= '" + lscreated_by + "'";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

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
                    "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
                    "  group by a.employeereporting_to ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                values.level_zero = objODBCDatareader["level_zero"].ToString();
                values.level_one = objODBCDatareader["level_one"].ToString();
                values.employee_number = objODBCDatareader["employee_number"].ToString();
            }
            objODBCDatareader.Close();



            msSQL = " select document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tservicereqdocument a" +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where a.servicerequest_gid='" + servicerequest_gid + "' order by servicereqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getalloteddocumentdtl = new List<alloteddocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.allofilename = file_name.ToArray();
                values.allofilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getalloteddocumentdtl.Add(new alloteddocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                    values.alloteddocumentdtl = getalloteddocumentdtl;
                }
            }
            dt_datatable.Dispose();
            // Reopen History
            msSQL = " select a.requestreopen_gid, a.servicerequest_gid, a.reopen_reason,date_format(a.reopened_date,'%d-%m-%Y %h:%i %p') as reopened_date, " +
                   " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as reopened_by,c.request_refno,c.request_title,if(request_status='Completed',CONCAT(FLOOR((DATEDIFF(c.created_date, c.created_date))), ' days ',MOD(HOUR(TIMEDIFF(c.created_date, c.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(c.created_date, c.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), c.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), c.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), c.created_date)), 'Mins') )  as created_aging from osd_trn_treqreopenhistory a" +
                   " left join adm_mst_tuser b on a.reopened_by = b.user_gid " +
                   "  left join osd_trn_tservicerequest c on a.servicerequest_gid = c.servicerequest_gid " +
                   " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid=c.requestreopen_gid order by requestreopen_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreopendtl = new List<reopendtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getreopendtl.Add(new reopendtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        reopen_reason = dt["reopen_reason"].ToString(),
                        reopened_date = dt["reopened_date"].ToString(),
                        reopened_by = dt["reopened_by"].ToString(),
                        requestreopen_gid = dt["requestreopen_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        created_aging = dt["created_aging"].ToString(),

                    });
                    values.reopendtl = getreopendtl;
                }
            }
            dt_datatable.Dispose();

            // Reopen Document Details
            msSQL = " select reopenreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
             " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_treopenreqdocument a" +
             " left join adm_mst_tuser b on a.created_by = b.user_gid " +
              " left join osd_trn_tservicerequest c on a.servicerequest_gid = c.servicerequest_gid " +
             " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid=c.requestreopen_gid order by reopenreqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreopendocumentdtl = new List<reopenrequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.rofilename = file_name.ToArray();
                values.rofilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getreopendocumentdtl.Add(new reopenrequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        reopenreqdocument_gid = dt["reopenreqdocument_gid"].ToString(),
                    });
                    values.reopenrequestdocumentdtl = getreopendocumentdtl;
                }
            }
            dt_datatable.Dispose();

            // Requestor Upload Document Details
            msSQL = " select servicereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                         " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tservicereqdocument a" +
                         " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                         " where a.servicerequest_gid='" + servicerequest_gid + "' order by servicereqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestdocumentdtl = new List<servicerequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.srfilename = file_name.ToArray();
                values.srfilepath = file_path.ToString();
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestdocumentdtl.Add(new servicerequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        servicereqdocument_gid = dt["servicereqdocument_gid"].ToString(),
                    });
                    values.servicerequestdocumentdtl = getservicerequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();

            //Normal Forward Document details
            msSQL = " select forwardreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
              " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tforwardreqdocument a" +
              " left join adm_mst_tuser b on a.created_by = b.user_gid " +
              " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid='' order by forwardreqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwarddocumentdtl = new List<forwarddocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.frfilename = file_name.ToArray();
                values.frfilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getforwarddocumentdtl.Add(new forwarddocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        forwardreqdocument_gid = dt["forwardreqdocument_gid"].ToString(),
                    });
                    values.forwarddocumentdtl = getforwarddocumentdtl;
                }
            }
            dt_datatable.Dispose();
            //Reopen Forward Document details
            msSQL = " select forwardreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
              " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tforwardreqdocument a" +
              " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                " left join osd_trn_tservicerequest c on a.servicerequest_gid = c.servicerequest_gid " +
              " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid=c.requestreopen_gid order by forwardreqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwardreopendocumentdtl = new List<forwardreopendocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.frrofilename = file_name.ToArray();
                values.frrofilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getforwardreopendocumentdtl.Add(new forwardreopendocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        forwardreqdocument_gid = dt["forwardreqdocument_gid"].ToString(),
                    });
                    values.forwardreopendocumentdtl = getforwardreopendocumentdtl;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGettempclear(string employee_gid, approvallist values)
        {
            string lsuser_gid;
            lsuser_gid = objdbconn.GetExecuteScalar("select user_gid from hrm_mst_temployee where employee_gid ='" + employee_gid + "'");
            msSQL = "delete from osd_tmp_treopenreqdocument where created_by='" + lsuser_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }

        public void DaGetForwardDocumentUploadtempclear(allottedlist values, string employee_gid)
        {
            string lsuser_gid;
            lsuser_gid = objdbconn.GetExecuteScalar("select user_gid from hrm_mst_temployee where employee_gid ='" + employee_gid + "'");
            msSQL = "delete from osd_tmp_tforwardreqdocument where created_by='" + lsuser_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }
        public void DaGetCompleteDocumentUploadtempclear(allottedlist values, string employee_gid)
        {
            string lsuser_gid;
            lsuser_gid = objdbconn.GetExecuteScalar("select user_gid from hrm_mst_temployee where employee_gid ='" + employee_gid + "'");
            msSQL = "delete from osd_tmp_tcompletereqdocument where created_by='" + lsuser_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
                values.status = false;
            }
        }
        public void DaPostUpdateAck(string user_gid, allotteddtl values)
        {
            msSQL = "update osd_mst_tbusinessstatusactivity set servicerequest_gid='" + values.servicerequest_gid + "'" +
                   " where servicerequest_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update osd_trn_tservicerequest set request_status='Work-In-Progress'," +
                    " acknowledge_by = '" + user_gid + "'," +
                    "acknowledge_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Acknowledgement Updated Successfully";
                msSQL = "select bankalert_flag from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsbankalert_flag = objdbconn.GetExecuteScalar(msSQL);
                if (lsbankalert_flag == "N")
                {
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
                            " left join hrm_mst_temployee b on a.created_by = b.user_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select request_refno,activity_name,request_title, a.request_status,a.request_title,a.assigned_supportteamname,a.assigned_membername,a.request_description," +
                              " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as acknowledge_by, " +
                              " date_format(a.acknowledge_date,'%d-%m-%Y %h:%i %p') as acknowledge_date,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                              " e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date from osd_trn_tservicerequest a " +
                              " left join adm_mst_tuser b on b.user_gid = a.acknowledge_by " +
                              " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                              " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                              " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                              " where servicerequest_gid='" + values.servicerequest_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                            lsactivity_name = objODBCDatareader["activity_name"].ToString();
                            request_title = objODBCDatareader["request_title"].ToString();
                            lsacknowledgedbydtl = objODBCDatareader["acknowledge_by"].ToString();
                            lsacknowledgedondtl = objODBCDatareader["acknowledge_date"].ToString();
                            lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                            lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                            lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                            lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                            lsrequest_status = objODBCDatareader["request_status"].ToString();
                            assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                            lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                            lsrequest_description = objODBCDatareader["request_description"].ToString();
                            lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();
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
                  "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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
                        //sub = "Ticket Acknowledged";
                        sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Ticket Acknowledged ";


                        lscontent = values.content;

                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " The ticket is assigned to me, working on it and might connect with you for any queries.<br />";
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
                        body = body + "<b>Acknowledged on :</b> " + HttpUtility.HtmlEncode(lsacknowledgedondtl) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Thanks & Regards, </b><br /> ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(lsacknowledgedbydtl) + "<br />";
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
                            //"'" + cc + "'," +
                            "'Service Request Acknowledged'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }

                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }
                }

            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

            msSQL = "update osd_mst_tbusinessstatusactivity set servicerequest_gid='" + values.servicerequest_gid + "'" +
                    " where servicerequest_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }
        public void DaPostAckCompletedRequest(string user_gid, ackcomplete values)

        {
            msSQL = "update osd_mst_tbusinessstatusactivity set servicerequest_gid='" + values.servicerequest_gid + "'" +
                  " where servicerequest_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update osd_trn_tservicerequest set request_status='Work-In-Progress'," +
                    " acknowledge_by = '" + user_gid + "'," +
                    "acknowledge_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Acknowledgement Updated Successfully";
            }
            msSQL = " select servicerequest_gid,approval_type,approval_status from osd_trn_trequestapproval where servicerequest_gid='" + values.servicerequest_gid + "' and approval_status='Pending'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Ticket Cannot be Completed.. Approval is Pending";
                return;
            }
            objODBCDatareader.Close();
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

            HashSet<string> validation = new HashSet<string>();

            validation.Add("E");
            validation.Add("");


            msGetGid = objcmnfunctions.GetMasterGID("RQCO");

            if (String.IsNullOrEmpty(msGetGid) || validation.Contains(msGetGid))
            {
                values.status = false;
                values.message = "Error Occurred while Raising Completed Request..!";
                return;
            }

            msSQL = "insert into osd_trn_tcompletedhistory(" +
                "requestcompleted_gid," +
                "servicerequest_gid," +
                "completed_remarks," +
                "completed_by," +
                "completed_date," +
                "requestreopen_gid)" +
                "values(" +
                "'" + msGetGid + "'," +
                "'" + values.servicerequest_gid + "'," +
                "'" + values.completed_remarks.Replace("'", "") + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + lsrequestreopen_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occurred while Raising Service Request..!";
                return;
            }
            else
            {

                msSQL = "update osd_mst_tbusinessstatusactivity set servicerequest_gid='" + values.servicerequest_gid + "'" +
                    " where servicerequest_gid='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select " +
                       "  concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as lsstatus_updatedby " +
                       "  from " +
                       "  adm_mst_tuser b " +
                       "  left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                       " where b.user_gid='" + user_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsstatus_updatedby = objODBCDatareader["lsstatus_updatedby"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " update osd_trn_tservicerequest set request_status='Completed', " +
                       " assigned_status='Completed'," +
                       " completed_remarks='" + values.completed_remarks.Replace("'", "") + "'," +
                       " completed_flag='Y'," +
                       " completed_by='" + user_gid + "'," +
                       " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ," +
                       " status_updatedby='" + lsstatus_updatedby + "' " +
                       " where servicerequest_gid='" + values.servicerequest_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select bankalert2allocated_gid from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsbankalert2allocated_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update osd_trn_tbankalert2allocated set operation_status='Completed',operationstatus_updated_by='" + user_gid + "'," +
                    " operationstatus_updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + lsbankalert2allocated_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select request_refno from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsrequest_refno = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='AssignMatched',knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',  knockoff_flag='Y',updated_by='" + user_gid  + "'" +
                                                   " where banktransc_gid = '" + lsrequest_refno + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update osd_trn_treqreopenhistory set reopencompleted_flag='Y' where requestreopen_gid='" + lsrequestreopen_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select tmpcompletereqdocument_gid, document_name, document_path from osd_tmp_tcompletereqdocument where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into osd_trn_tcompletereqdocument(" +
                         " completereqdocument_gid," +
                         " servicerequest_gid, " +
                         " document_name," +
                         " document_path," +
                         " created_by," +
                         " created_date," +
                         " requestreopen_gid)" +
                         " values(" +
                         "'" + msGetDocumentGid + "'," +
                         "'" + values.servicerequest_gid + "', " +
                         "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                         "'" + dt["document_path"].ToString() + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + lsrequestreopen_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = "delete from osd_tmp_tcompletereqdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Service Request Completed Successfully..!";
                }
            }
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
                    " left join hrm_mst_temployee b on a.created_by = b.user_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select request_refno,activity_name,request_title,  a.request_status,a.request_title,a.assigned_supportteamname,a.assigned_membername,a.request_description," +
                      " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as completed_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                      " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date  " +
                      " from osd_trn_tservicerequest a " +
                      " left join adm_mst_tuser b on b.user_gid = a.completed_by " +
                       " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                     " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                     " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                      " where servicerequest_gid='" + values.servicerequest_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                    lsactivity_name = objODBCDatareader["activity_name"].ToString();
                    request_title = objODBCDatareader["request_title"].ToString();
                    lscompletedbydtl = objODBCDatareader["completed_by"].ToString();
                    lscompletedondtl = objODBCDatareader["completed_date"].ToString();
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
          "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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

                msSQL = "select bankalert_flag from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsbankalert_flag = objdbconn.GetExecuteScalar(msSQL);

                if (lsbankalert_flag == "Y")
                {
                    //sub = " Escrow payment adjustment/refund completed ";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Escrow payment adjustment/refund completed ";
                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " The ticket is completed,the details are as follows,<br />";
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
                    body = body + "<b>Completed By  :</b> " + HttpUtility.HtmlEncode(lscompletedbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed On :</b> " + HttpUtility.HtmlEncode(lscompletedondtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Thanks & Regards, </b><br/> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
                    body = body + "<br />";
                }
                else
                {
                    //sub = "Service Request Completed";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno)+ "  Service Request Completed ";
                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " Service Request has been Completed and the details are as follows,<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Ref No :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
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
                    body = body + "<br />";
                    body = body + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed By  :</b> " + HttpUtility.HtmlEncode(lscompletedbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed On :</b> " + HttpUtility.HtmlEncode(lscompletedondtl) + "<br />";
                    body = body + "<br />";

                    body = body + "<b>Thanks & Regards, </b> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
                    body = body + "<br />";
                }

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
                    //"'" + cc + "'," +
                    "'Service Request Acknowledged and  Completed'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }

        public void DaGetBusinessUnitStatusMyActivityDrop(allottedlist values, string employee_gid)
        {
            string lsuser_gid;
            msSQL = " select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";

            lsuser_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select businessstatusactivity_gid,businessactivity_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_mst_tbusinessstatusactivity a left join adm_mst_tuser b on a.created_by = b.user_gid where " +
              " servicerequest_gid='" + lsuser_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit_list = new List<businessstatusunitmyactivity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunit_list.Add(new businessstatusunitmyactivity_list
                    {
                        businessstatusactivity_gid = (dr_datarow["businessstatusactivity_gid"].ToString()),
                        businessactivity_status = (dr_datarow["businessactivity_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.businessstatusunitmyactivity_list = getbusinessunit_list;


            }
            dt_datatable.Dispose();
        }

        public void DaGetBusinessUnitStatusMyActivityList(string servicerequest_gid, allottedlist values)
        {

            msSQL = "select businessstatusactivity_gid,businessactivity_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_mst_tbusinessstatusactivity a left join adm_mst_tuser b on a.created_by = b.user_gid where " +
              " servicerequest_gid='" + servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit_list = new List<businessstatusunitmyactivity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunit_list.Add(new businessstatusunitmyactivity_list
                    {
                        businessstatusactivity_gid = (dr_datarow["businessstatusactivity_gid"].ToString()),
                        businessactivity_status = (dr_datarow["businessactivity_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.businessstatusunitmyactivity_list = getbusinessunit_list;


            }
            dt_datatable.Dispose();
        }
        public void DaGetservicerequestactivityhistoryList(string servicerequest_gid, allottedlist values)
        {

            msSQL = "select activitymaster_gid,activity_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tservicerequestactivityhistory a left join adm_mst_tuser b on a.created_by = b.user_gid where " +
              " servicerequest_gid='" + servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit_list = new List<servicerequestactivityhistory_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunit_list.Add(new servicerequestactivityhistory_list
                    {
                        activitymaster_gid = (dr_datarow["activitymaster_gid"].ToString()),
                        activity_name = (dr_datarow["activity_name"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.servicerequestactivityhistory_list = getbusinessunit_list;


            }
            dt_datatable.Dispose();
        }

        public void DaGetBusinessUnitStatusMyActivityComplete(string servicerequest_gid, allottedlist values)
        {

            msSQL = "select businessstatusactivity_gid,businessactivity_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_mst_tbusinessstatusactivity a left join adm_mst_tuser b on a.created_by = b.user_gid where " +
              " servicerequest_gid='" + servicerequest_gid + "' order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit_list = new List<businessstatusunitmyactivity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunit_list.Add(new businessstatusunitmyactivity_list
                    {
                        businessstatusactivity_gid = (dr_datarow["businessstatusactivity_gid"].ToString()),
                        businessactivity_status = (dr_datarow["businessactivity_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.businessstatusunitmyactivity_list = getbusinessunit_list;


            }
            dt_datatable.Dispose();
        }
        public bool DaDeleteBusinessUnitStatusMyActivity(string businessstatusactivity_gid, result values)
        {
            //msSQL = "select department_gid from osd_mst_tactivedepartment where department_gid='" + businessstatus_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);

            msSQL = " delete from osd_mst_tbusinessstatusactivity where businessstatusactivity_gid='" + businessstatusactivity_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Unit Status Deleted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Delecting Business Unit Status";
                return false;
            }
        }
        public void DaGetBusinessUnitStatusMyActivityTempClear(allottedlist values, string employee_gid)
        {
            string lsuser_gid;
            msSQL = " select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";

            lsuser_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "delete from osd_mst_tbusinessstatusactivity where servicerequest_gid='" + lsuser_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            values.status = true;
        }


        //Query Conversation 
        public bool DaPostSendRequestor(string employee_gid, requestordtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RQCM");
            msSQL = " insert into osd_trn_trequestorcommunication(" +
                    " requestorcommunication_gid," +
                    " servicerequest_gid," +
                    " remarks," +
                    " response_new, " +
                    " requestor_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.servicerequest_gid + "'," +
                    //"'" + values.remarks + "'," +
                    "'" + values.remarks.Replace("'", "\\'") + "'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update osd_trn_trequestorcommunication set request_flag='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_tservicerequest set createdby_flag='Y', assignedmember_flag='Y', forwardmember_flag='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_ttaggedmemberlist set response_new='Y' where servicerequest_gid='" + values.servicerequest_gid + "' and tagmember_gid<>'" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_membertransfer set responce_new='Y' where servicerequest_gid='" + values.servicerequest_gid + "' and assigned_membergid<>'" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Requestor Communications are Sent Successfully..!";
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

        public void DaPostConverseUpload(HttpRequest httpRequest, upload_document objfilename, string employee_gid, string user_gid)
        {

            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string lsdocument_title = httpRequest.Form["document_title"];
            string servicerequest_gid = HttpContext.Current.Request.Params["servicerequest_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(lspath)))
                System.IO.Directory.CreateDirectory(lspath);

            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        MemoryStream ms = new MemoryStream();
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            objfilename.status = false;
                            return;
                        }


                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/ConversationDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("RQCM");
                        msSQL = " insert into osd_trn_trequestorcommunication(" +
                                " requestorcommunication_gid," +
                                " servicerequest_gid," +
                                  " requestor_gid," +
                                " document_name ," +
                                " document_path," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + servicerequest_gid + "'," +
                                  "'" + employee_gid + "'," +
                               "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                               "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        if (mnResult != 0)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured";
                        }
                    }
                }
            }
            catch
            {
            }
        }

        //Query Conversation Summary
        public bool DaGetRequestorlist(string employee_gid, string servicerequest_gid, requestorlist values)
        {

            msSQL = "select a.requestorcommunication_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,a.requestor_gid,a.document_name,a.document_path," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from osd_trn_trequestorcommunication a " +
                    " left join hrm_mst_temployee b on a.requestor_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.servicerequest_gid = '" + servicerequest_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestordtl = new List<requestordtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["requestor_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }
                    if (dr_datarow["document_name"].ToString() == "")
                    {
                        lsdocument_attached = "N";
                    }
                    else
                    {
                        lsdocument_attached = "Y";
                    }
                    getrequestordtl.Add(new requestordtl
                    {
                        requestorcommunication_gid = (dr_datarow["requestorcommunication_gid"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        session_user = lssession_user,
                        document_attached = lssession_user + "/" + lsdocument_attached
                    });
                }
                values.requestordtl = getrequestordtl;
            }
            dt_datatable.Dispose();

            msSQL = "select a.requestorcommunication_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,a.requestor_gid,a.document_name,a.document_path," +
        " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
        " from osd_trn_trequestorcommunicationhistory a " +
        " left join hrm_mst_temployee b on a.requestor_gid = b.employee_gid " +
        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
        " where a.servicerequest_gid = '" + servicerequest_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrequestordtlhistory = new List<requestordtlhistory>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["requestor_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }
                    if (dr_datarow["document_name"].ToString() == "")
                    {
                        lsdocument_attached = "N";
                    }
                    else
                    {
                        lsdocument_attached = "Y";
                    }
                    getrequestordtlhistory.Add(new requestordtlhistory
                    {
                        requestorcommunication_gid = (dr_datarow["requestorcommunication_gid"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        session_user = lssession_user,
                        document_attached = lssession_user + "/" + lsdocument_attached
                    });
                }
                values.requestordtlhistory = getrequestordtlhistory;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public void DaGetActivityCount(string employee_gid, countlist values)
        {
            //msSQL = "select count(*) as alloted_count from osd_trn_tservicerequest a " +
            //        " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
            //        " left join osd_trn_trequestapproval g on a.servicerequest_gid = g.servicerequest_gid " +
            //        " where a.request_status='Allotted' and ( g.approval_status != 'Pending' or   g.approval_status is null )  and a.assigned_membergid='" + employee_gid + "' and f.department_status='Y'";
            //values.alloted_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(*) as workinprogress_count from osd_trn_tservicerequest a " +
            //        " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
            //        " left join osd_trn_trequestapproval g on a.servicerequest_gid = g.servicerequest_gid " +
            //        "where a.request_status='Work-In-Progress'  and a.assigned_status<>'Forward' and ( g.approval_status != 'Pending' or   g.approval_status is null ) and a.assigned_membergid='" + employee_gid + "' and f.department_status='Y'";
            //values.workinprogress_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select distinct a.servicerequest_gid from osd_trn_tservicerequest a " +
                     " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    //" left join osd_trn_trequestapproval f on a.servicerequest_gid = f.servicerequest_gid " +
                    " where a.request_status='Allotted'  and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' order by a.servicerequest_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<allotteddtl>();
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
                        getallotteddtl.Add(new allotteddtl
                        {
                            servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        });
                        values.allotteddtl = getallotteddtl;
                    }
                }
            }
            values.lsallotedcount = getallotteddtl.Count;
            dt_datatable.Dispose();

            msSQL = " select distinct a.servicerequest_gid from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.request_status='Work-In-Progress' and a.assigned_status<>'Forward' and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' order by a.servicerequest_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinprogressddtl = new List<workinprogressdtl>();
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
                        getinprogressddtl.Add(new workinprogressdtl
                        {
                            servicerequest_gid = dt["servicerequest_gid"].ToString()
                        });
                        values.workinprogressdtl = getinprogressddtl;
                    }
                }
            }
            values.lsworkinprogress_count = getinprogressddtl.Count;
            dt_datatable.Dispose();

            msSQL = "select count(*) as completed_count from osd_trn_tservicerequest a " +
                 " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
                "where a.request_status='Completed' and a.assigned_membergid='" + employee_gid + "' and f.department_status='Y'";
            values.completed_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as closed_count from osd_trn_tservicerequest a " +
                 " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
                " where (a.request_status='Closed' or a.request_status='Rejected' or a.request_status='Cancelled') and a.assigned_membergid='" + employee_gid + "' and f.department_status='Y'";
            values.closed_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from ( select count(*) from osd_trn_tservicerequest a  " +
                    " left join osd_trn_membertransfer b on a.servicerequest_gid=b.servicerequest_gid" +
                     " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid  " +
                    " where a.transfer_flag='Y' and b.assigned_membergid='" + employee_gid + "' and f.department_status='Y' " +
                    " group by a.servicerequest_gid) as count";
            values.transfer_count = objdbconn.GetExecuteScalar(msSQL);

            //msSQL = "select count(*)  as forward_count from osd_trn_tservicerequest a " +
            //     " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
            //     "where a.assigned_status='Forward' and a.approvalrequest_flag = 'N' and a.assigned_membergid='" + employee_gid + "' and f.department_status='Y' and request_status != 'Closed' and request_status != 'Cancelled' and request_status != 'Rejected' ";
            //values.forward_count = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select distinct a.servicerequest_gid from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   //" left join osd_trn_trequestapproval f on a.servicerequest_gid = f.servicerequest_gid " +
                   " where a.assigned_status='Forward' and a.request_status != 'Closed' and request_status != 'Cancelled' and request_status != 'Rejected'  and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' order by a.servicerequest_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwarddtl = new List<allotteddtl>();
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

                    if (lsstatus != "Pending")
                    {
                        getforwarddtl.Add(new allotteddtl
                        {
                            servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        });
                        values.forwarddtl = getforwarddtl;
                    }
                }
            }
            values.lsforward_count = getforwarddtl.Count;
            dt_datatable.Dispose();

            msSQL = " select  count(distinct a.servicerequest_gid) as approvalpending_count from osd_trn_tservicerequest a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                   " left join osd_trn_trequestapproval f on a.servicerequest_gid = f.servicerequest_gid " +
                   " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   " left join osd_trn_tbankalert2allocated p on p.ticketref_no = a.request_refno " +
                   " left join osd_mst_tbusinessstatusactivity t on t.servicerequest_gid = a.servicerequest_gid  " +
                " where f.approval_status='Pending' and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y'";
            values.approvalpending_count = objdbconn.GetExecuteScalar(msSQL);

        }


        public void DaPostCompletedRequest(string user_gid, completed values)
        {
            msSQL = " select servicerequest_gid,approval_type,approval_status from osd_trn_trequestapproval where servicerequest_gid='" + values.servicerequest_gid + "' and approval_status='Pending'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Ticket Cannot be Completed.. Approval is Pending";
                return;
            }
            objODBCDatareader.Close();
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

            HashSet<string> validation = new HashSet<string>();

            validation.Add("E");
            validation.Add("");


            msGetGid = objcmnfunctions.GetMasterGID("RQCO");

            if (String.IsNullOrEmpty(msGetGid) || validation.Contains(msGetGid))
            {
                values.status = false;
                values.message = "Error Occurred while Raising Completed Request..!";
                return;
            }

            msSQL = "insert into osd_trn_tcompletedhistory(" +
                "requestcompleted_gid," +
                "servicerequest_gid," +
                "completed_remarks," +
                "completed_by," +
                "completed_date," +
                "requestreopen_gid)" +
                "values(" +
                "'" + msGetGid + "'," +
                "'" + values.servicerequest_gid + "'," +
                "'" + values.completed_remarks.Replace("'", "") + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + lsrequestreopen_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occurred while Raising Service Request..!";
                return;
            }
            else
            {

                msSQL = "update osd_mst_tbusinessstatusactivity set servicerequest_gid='" + values.servicerequest_gid + "'" +
                    " where servicerequest_gid='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select " +
                       "  concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as lsstatus_updatedby " +
                       "  from " +
                       "  adm_mst_tuser b " +
                       "  left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                       " where b.user_gid='" + user_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsstatus_updatedby = objODBCDatareader["lsstatus_updatedby"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " update osd_trn_tservicerequest set request_status='Completed', " +
                       " assigned_status='Completed'," +
                       " completed_remarks='" + values.completed_remarks.Replace("'", "") + "'," +
                       " completed_flag='Y'," +
                       " completed_by='" + user_gid + "'," +
                       " completed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ," +
                       " status_updatedby='" + lsstatus_updatedby + "' " +
                       " where servicerequest_gid='" + values.servicerequest_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select bankalert2allocated_gid from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsbankalert2allocated_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update osd_trn_tbankalert2allocated set operation_status='Completed',operationstatus_updated_by='" + user_gid + "'," +
                    " operationstatus_updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + lsbankalert2allocated_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select request_refno from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsrequest_refno = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " update brs_trn_tbanktransactiondetails set knockoff_status='AssignMatched', knockoff_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', knockoff_flag='Y',updated_by='" + user_gid  + "'" +
                                                   " where banktransc_gid = '" + lsrequest_refno + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update osd_trn_treqreopenhistory set reopencompleted_flag='Y' where requestreopen_gid='" + lsrequestreopen_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select  tmpcompletereqdocument_gid, document_name, document_path from osd_tmp_tcompletereqdocument where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("CRDO");

                        msSQL = " insert into osd_trn_tcompletereqdocument(" +
                         " completereqdocument_gid," +
                         " servicerequest_gid, " +
                         " document_name," +
                         " document_path," +
                         " created_by," +
                         " created_date," +
                         " requestreopen_gid)" +
                         " values(" +
                         "'" + msGetDocumentGid + "'," +
                         "'" + values.servicerequest_gid + "', " +
                         "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                         "'" + dt["document_path"].ToString() + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + lsrequestreopen_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = "delete from osd_tmp_tcompletereqdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Service Request Completed Successfully..!";
                }
            }
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
                    " left join hrm_mst_temployee b on a.created_by = b.user_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select request_refno,activity_name,request_title,  a.request_status,a.request_title,a.assigned_supportteamname,a.assigned_membername,a.request_description," +
                      " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as completed_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                      " date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date  " +
                      " from osd_trn_tservicerequest a " +
                      " left join adm_mst_tuser b on b.user_gid = a.completed_by " +
                       " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                     " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                     " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                      " where servicerequest_gid='" + values.servicerequest_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                    lsactivity_name = objODBCDatareader["activity_name"].ToString();
                    request_title = objODBCDatareader["request_title"].ToString();
                    lscompletedbydtl = objODBCDatareader["completed_by"].ToString();
                    lscompletedondtl = objODBCDatareader["completed_date"].ToString();
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
          "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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

                msSQL = "select bankalert_flag from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "'";
                string lsbankalert_flag = objdbconn.GetExecuteScalar(msSQL);

                if (lsbankalert_flag == "Y")
                {
                    //sub = " Escrow payment adjustment/refund completed ";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Escrow payment adjustment/refund completed ";
                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " The ticket is completed,the details are as follows,<br />";
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
                    body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed By  :</b> " + HttpUtility.HtmlEncode(lscompletedbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed On :</b> " + HttpUtility.HtmlEncode(lscompletedondtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Thanks & Regards, </b><br/> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
                    body = body + "<br />";
                }
                else
                {
                    //sub = "Service Request Completed";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Completed ";
                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " Service Request has been Completed and the details are as follows,<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Ref No :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
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
                    body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed By  :</b> " + HttpUtility.HtmlEncode(lscompletedbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Completed On :</b> " + HttpUtility.HtmlEncode(lscompletedondtl) + "<br />";
                    body = body + "<br />";

                    body = body + "<b>Thanks & Regards, </b> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
                    body = body + "<br />";
                }

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
                    //"'" + cc + "'," +
                    "'Service Request Completed'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
        }






        //public void DaPostrequestRejected(string user_gid, completed values)
        //{

        //    msSQL = "update osd_trn_tservicerequest set request_status ='Rejected' where servicerequest_gid='" + values.servicerequest_gid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


        //}

        public void DaPostApprovalGet(approvaldtl values, string user_gid)
        {
            msSQL = " update osd_trn_trequestapproval set approvalstatus_flag='N' where servicerequest_gid='" + values.servicerequest_gid + "'" +
                    " and approval_status<>'Pending' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            k = 1;

            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                ls_server = objODBCDatareader["pop_server"].ToString();
                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                ls_username = objODBCDatareader["pop_username"].ToString();
                ls_password = objODBCDatareader["pop_password"].ToString();
            }
            objODBCDatareader.Close();

            if (values.approval_type == "Parallel")
            {

                msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from osd_tmp_tapprovalmembers" +
                        " where servicerequest_gid='" + values.servicerequest_gid + "' order by created_date asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettagmemberdtl = new List<approvalmember>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        var lsemployee_name = dt["approval_name"].ToString();
                        var lsemployee_gid = dt["approval_gid"].ToString();

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("RQAP");

                        msSQL = "Insert into osd_trn_trequestapproval( " +
                               " requestapproval_gid, " +
                               " servicerequest_gid," +
                               " approval_gid," +
                               " approval_name," +
                               " approval_type," +
                               " hierary_level," +
                               " approval_token," +
                               " approval_basedon," +
                               " requestapproval_remarks," +
                               " approvalstatus_flag, " +
                               " approvalforward_flag, " +
                               " approvalrequest_flag," +
                               " approvalreopen_flag," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + values.servicerequest_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsemployee_name + "'," +
                               "'" + values.approval_type + "'," +
                               "'" + k + "'," +
                               "'" + sToken + "'," +
                               "'" + values.approval_basedon + "'," +
                               "'" + values.approval_remarks.Replace("'", "") + "'," +
                               "'Y'," +
                               "'" + values.approvalforward_flag + "'," +
                               "'" + values.approvalrequest_flag + "'," +
                               "'" + values.approvalreopen_flag + "'," +
                               "'" + user_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        k = k + 1;

                        if (values.approvalforward_flag != "Y" && values.approvalrequest_flag != "Y" && values.approvalreopen_flag != "Y")
                        {
                            msSQL = " update osd_trn_tservicerequest set getapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                     " getapproval_flag='Y' " +
                                                     " where servicerequest_gid='" + values.servicerequest_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else if (values.approvalforward_flag == "Y")
                        {
                            msSQL = " update osd_trn_tservicerequest set getforwardapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                    " approvalforward_flag='Y' " +
                                                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else if (values.approvalrequest_flag == "Y")
                        {
                            msSQL = " update osd_trn_tservicerequest set getrequestapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                    " approvalrequest_flag='Y' ," +
                                                     " getapproval_flag='Y' " +
                                                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        else if (values.approvalreopen_flag == "Y")
                        {
                            msSQL = " update osd_trn_tservicerequest set getreopenapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                    " approvalreopen_flag='Y' " +
                                                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsemployee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsto_mail = objODBCDatareader["employee_emailid"].ToString();
                        }
                        objODBCDatareader.Close();
                        string lsdepartmentgid;
                        lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");

                        msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscc_mail = objODBCDatareader["businessunit_emailaddress"].ToString();
                        }
                        objODBCDatareader.Close();


                        msSQL = " select request_refno,activity_name,request_title,getapproval_remarks,  a.request_status,a.request_title,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                                " concat(assigned_supportteamname, ' / ', assigned_membername) as assigndedtl,getforwardapproval_remarks ,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo," +
                                " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by, e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date , " +
                                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                                " from osd_trn_tservicerequest a " +
                                " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                                 " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                                 " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                                " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                                " where servicerequest_gid='" + values.servicerequest_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                            lsactivity_name = objODBCDatareader["activity_name"].ToString();
                            lsrequest_title = objODBCDatareader["request_title"].ToString();
                            request_title = objODBCDatareader["request_title"].ToString();
                            //lscompletedbydtl = objODBCDatareader["completed_by"].ToString();
                            //lscompletedondtl = objODBCDatareader["completed_date"].ToString();
                            lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                            lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                            lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                            lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                            lsrequest_status = objODBCDatareader["request_status"].ToString();
                            assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                            lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                            lsrequest_description = objODBCDatareader["request_description"].ToString();
                            lsgetapproval_remarks = objODBCDatareader["getapproval_remarks"].ToString();
                            lsgetforwardapproval_remarks = objODBCDatareader["getforwardapproval_remarks"].ToString();
                            lsassigndedtl = objODBCDatareader["assigndedtl"].ToString();
                            lsraiseddtl = objODBCDatareader["raised_by"].ToString();
                            lsraisedondtl = objODBCDatareader["created_date"].ToString();
                            lsraiseddtl = objODBCDatareader["raised_by"].ToString() + " / " + objODBCDatareader["created_date"].ToString();
                        }
                        objODBCDatareader.Close();

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
                  "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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
                        msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from adm_mst_tuser where user_gid='" + user_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequested_by = objODBCDatareader["requested_by"].ToString();
                        }
                        objODBCDatareader.Close();

                        message = "Dear Sir/Madam,  <br />";
                        message = message + "<br />";
                        message = message + "Greetings,  <br />";
                        message = message + "<br />";
                        message = message + HttpUtility.HtmlEncode(lsrequested_by) + " has been Initiated the Approval Request and the details are as follows,<br />";
                        message = message + "<br />";
                        message = message + "<b>Request Ref No :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                        message = message + "<br />";
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
                        //body = body + "<b>Assigned Team :</b> " + assigned_supportteamname + "<br />";
                        //body = body + "<br />";
                        //body = body + "<b>Assigned Member :</b> " + lsassigned_membername + "<br />";
                        //body = body + "<br />";
                        message = message + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
                        message = message + "<br />";
                        message = message + "<b>Assigned Team / Member :</b> " + HttpUtility.HtmlEncode(lsassigndedtl) + "<br />";
                        message = message + "<br />";
                        body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                        body = body + "<br />";

                        if (values.approvalforward_flag != "Y")
                        {
                            message = message + "<b>Remarks :</b> " + HttpUtility.HtmlEncode(lsgetapproval_remarks) + "<br />";
                        }
                        else
                        {
                            message = message + "<b>Remarks :</b> " + HttpUtility.HtmlEncode(lsgetforwardapproval_remarks) + "<br />";
                        }

                        message = message + "<br />";
                        message = message + "Kindly <a href=" + ConfigurationManager.AppSettings["approvalurl"].ToString() + "?id=" + sToken + "> Click Here</a> and do the needful.<br />";
                        message = message + "<br />";
                        message = message + "<b>Thanks & Regards, </b> ";
                        message = message + "<br />";
                        message = message + "<b> Team Business Process </b> ";
                        message = message + "<br />";

                        if (lscc_mail != null & lscc_mail != string.Empty & lscc_mail != "")
                        {
                            MailFlag = SendSMTP2(ls_username, ls_password, lsto_mail, "Request Initiation Mail", message, lscc_mail, "", "");

                            if (MailFlag == 1)
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
                                "'" + lsto_mail + "'," +
                                //"'" + lscc_mail + "'," +
                                "'Request Initiation Mail'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Approval Initiated Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }

                    dt_datatable.Dispose();
                }
                else
                {
                    dt_datatable.Dispose();
                    values.status = false;
                    values.message = "You Need to Select Atleast One Member for Approval";
                }

            }
            else if (values.approval_type == "Approval")
            {

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                msGetGid = objcmnfunctions.GetMasterGID("RQAP");

                msSQL = "Insert into osd_trn_trequestapproval( " +
                       " requestapproval_gid, " +
                       " servicerequest_gid," +
                       " approval_gid," +
                       " approval_name," +
                       " approval_type," +
                       " hierary_level," +
                       " approval_token," +
                       " approval_basedon," +
                       " requestapproval_remarks," +
                       " approvalstatus_flag," +
                       " approvalrequest_flag," +
                       " approvalforward_flag, " +
                       " approvalreopen_flag," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.servicerequest_gid + "'," +
                       "'" + values.approvalgid + "'," +
                       "'" + values.approvalname + "'," +
                       "'" + values.approval_type + "'," +
                       "'" + "0" + "'," +
                       "'" + sToken + "'," +
                       "'" + values.approval_basedon + "'," +
                       "'" + values.approval_remarks.Replace("'", "") + "'," +
                       "'Y'," +
                       "'" + values.approvalrequest_flag + "'," +
                       "'" + values.approvalforward_flag + "'," +
                       "'" + values.approvalreopen_flag + "', " +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                k = k + 1;

                //if (values.approvalforward_flag != "Y")
                //{
                //    msSQL = " update osd_trn_tservicerequest set getapproval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                //                             " getapproval_flag='Y' " +
                //                             " where servicerequest_gid='" + values.servicerequest_gid + "'";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //}
                //else
                //{
                //    msSQL = " update osd_trn_tservicerequest set getforwardapproval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                //                            " approvalforward_flag='Y' " +
                //                            " where servicerequest_gid='" + values.servicerequest_gid + "'";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //}

                msSQL = "select requestapproval_remarks from osd_trn_trequestapproval " +
                        " where approval_token='" + sToken + "'";
                lsgetapproval_remarks = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.approvalgid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsto_mail = objODBCDatareader["employee_emailid"].ToString();
                }
                objODBCDatareader.Close();

                string lsdepartmentgid;
                lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscc_mail = objODBCDatareader["businessunit_emailaddress"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select request_refno,activity_name,request_title,getapproval_remarks,  a.request_status,a.request_title,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                        " concat(assigned_supportteamname, ' / ', assigned_membername) as assigndedtl,getapproval_remarks,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                        " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date,a.getforwardapproval_remarks,  " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                        " from osd_trn_tservicerequest a " +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                        " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                        " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                        " where servicerequest_gid='" + values.servicerequest_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                    lsactivity_name = objODBCDatareader["activity_name"].ToString();
                    request_title = objODBCDatareader["request_title"].ToString();

                    lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                    lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                    lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                    lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                    lsrequest_status = objODBCDatareader["request_status"].ToString();
                    assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                    lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                    lsrequest_description = objODBCDatareader["request_description"].ToString();
                    lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();
                    lsgetforwardapproval_remarks = objODBCDatareader["getforwardapproval_remarks"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from adm_mst_tuser where user_gid='" + user_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequested_by = objODBCDatareader["requested_by"].ToString();
                }
                objODBCDatareader.Close();

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
          "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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

                message = "Dear Sir/Madam,  <br />";
                message = message + "<br />";
                message = message + "Greetings,  <br />";
                message = message + "<br />";
                message = message + HttpUtility.HtmlEncode(lsrequested_by) + " has been Initiated the Approval Request and the details are as follows,<br />";
                message = message + "<br />";
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
                message = message + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
                message = message + "<br />";
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
                message = message + "<b>Remarks :</b> " + HttpUtility.HtmlEncode(lsgetapproval_remarks.Replace("'", "")) + "<br />";
                message = message + "<br />";
                message = message + "Kindly <a href=" + ConfigurationManager.AppSettings["approvalurl"].ToString() + "?id=" + sToken + "> Click Here</a> and do the needful.<br />";
                message = message + "<br />";
                message = message + "<b>Thanks & Regards, </b> ";
                message = message + "<br />";
                message = message + "<b> Team Business Process </b> ";
                message = message + "<br />";

                MailFlag = SendSMTP2(ls_username, ls_password, lsto_mail, "Request Initiation Mail", message, lscc_mail, "", "");
                if (MailFlag == 1)
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
                    "'" + lsto_mail + "'," +
                    //"'" + lscc_mail + "'," +
                    "'Request Initiation Mail'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Approval Initiated Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
            else
            {
                k = 1;
                msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from osd_tmp_tapprovalmembers" +
                        " where servicerequest_gid='" + values.servicerequest_gid + "' order by created_date asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettagmemberdtl = new List<approvalmember>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        var lsemployee_name = dt["approval_name"].ToString();
                        var lsemployee_gid = dt["approval_gid"].ToString();

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        msGetGid = objcmnfunctions.GetMasterGID("RQAP");

                        msSQL = "Insert into osd_trn_trequestapproval( " +
                               " requestapproval_gid, " +
                               " servicerequest_gid," +
                               " approval_gid," +
                               " approval_name," +
                               " approval_type," +
                               " hierary_level," +
                               " approval_token," +
                               " approval_basedon," +
                               " requestapproval_remarks," +
                               " approvalstatus_flag," +
                               " approvalrequest_flag," +
                               " approvalforward_flag, " +
                               " approvalreopen_flag," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + values.servicerequest_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsemployee_name + "'," +
                               "'" + values.approval_type + "'," +
                               "'" + k + "'," +
                               "'" + sToken + "'," +
                               "'" + values.approval_basedon + "'," +
                               "'" + values.approval_remarks.Replace("'", "") + "'," +
                               "'Y'," +
                               "'" + values.approvalrequest_flag + "'," +
                               "'" + values.approvalforward_flag + "'," +
                               "'" + values.approvalreopen_flag + "'," +
                               "'" + user_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (k == 1)
                        {
                            if (values.approvalforward_flag != "Y" && values.approvalrequest_flag != "Y" && values.approvalreopen_flag != "Y")
                            {
                                msSQL = " update osd_trn_tservicerequest set getapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                         " getapproval_flag='Y' " +
                                                         " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else if (values.approvalforward_flag == "Y")
                            {
                                msSQL = " update osd_trn_tservicerequest set getforwardapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                        " approvalforward_flag='Y' " +
                                                        " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else if (values.approvalrequest_flag == "Y")
                            {
                                msSQL = " update osd_trn_tservicerequest set getrequestapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                        " approvalrequest_flag='Y', " +
                                                       " getapproval_flag='Y' " +
                                                        " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else if (values.approvalreopen_flag == "Y")
                            {
                                msSQL = " update osd_trn_tservicerequest set getreopenapproval_remarks='" + values.approval_remarks.Replace("'", "") + "'," +
                                                        " approvalreopen_flag='Y' " +
                                                        " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            msSQL = " update osd_trn_trequestapproval set seqhierarchy_view='Y'" +
                                 " where requestapproval_gid='" + msGetGid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " select b.employee_emailid from osd_trn_trequestapproval a " +
                                    "left join hrm_mst_temployee b on a.approval_gid = b.employee_gid " +
                                    "where a.servicerequest_gid ='" + values.servicerequest_gid + "'  and hierary_level ='1'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsto_mail = objODBCDatareader["employee_emailid"].ToString();
                            }
                            objODBCDatareader.Close();

                            string lsdepartmentgid;
                            lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                            msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lscc_mail = objODBCDatareader["businessunit_emailaddress"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select request_refno,activity_name,request_title,getapproval_remarks,a.assigned_supportteamname,a.assigned_membername,a.request_description,a.request_status, " +
                                    " concat(assigned_supportteamname, ' / ', assigned_membername) as assigndedtl,getforwardapproval_remarks, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as Raised_By,d.employee_mobileno as RaisedNo," +
                                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by, e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date ," +
                                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                                    " from osd_trn_tservicerequest a " +
                                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                          " left join hrm_mst_temployee d on b.user_gid = d.user_gid " +
                         " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                lsgetapproval_remarks = objODBCDatareader["getapproval_remarks"].ToString();
                                lsgetforwardapproval_remarks = objODBCDatareader["getforwardapproval_remarks"].ToString();
                                lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                                lsactivity_name = objODBCDatareader["activity_name"].ToString();
                                request_title = objODBCDatareader["request_title"].ToString();
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
                      "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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
                            msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from adm_mst_tuser where user_gid='" + user_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsrequested_by = objODBCDatareader["requested_by"].ToString();
                            }
                            objODBCDatareader.Close();

                            message = "Dear Sir/Madam,  <br />";
                            message = message + "<br />";
                            message = message + "Greetings,  <br />";
                            message = message + "<br />";
                            message = message + HttpUtility.HtmlEncode(lsrequested_by) + " has been Initiated the Approval Request and the details are as follows,<br />";
                            message = message + "<br />";
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
                            message = message + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
                            message = message + "<br />";
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

                            if (values.approvalforward_flag != "Y")
                            {
                                message = message + "<b>Remarks :</b> " + HttpUtility.HtmlEncode(lsgetapproval_remarks.Replace("'", "")) + "<br />";
                            }
                            else
                            {
                                message = message + "<b>Remarks :</b> " + HttpUtility.HtmlEncode(lsgetforwardapproval_remarks.Replace("'", "")) + "<br />";
                            }
                            message = message + "<br />";
                            message = message + "Kindly <a href=" + ConfigurationManager.AppSettings["approvalurl"].ToString() + "?id=" + sToken + "> Click Here</a> and do the needful.<br />";
                            message = message + "<br />";
                            message = message + "<b>Thanks & Regards, </b> ";
                            message = message + "<br />";
                            message = message + "<b> Team Business Process </b> ";
                            message = message + "<br />";

                            MailFlag = SendSMTP2(ls_username, ls_password, lsto_mail, "Request Initiation Mail", message, lscc_mail, "", "");
                            if (MailFlag == 1)
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
                                "'" + lsto_mail + "'," +
                                //"'" + lscc_mail + "'," +
                                "'Request Initiation Mail'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }
                        k = k + 1;

                    }
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Approval Initiated Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }

                    dt_datatable.Dispose();
                }
                else
                {
                    dt_datatable.Dispose();
                    values.status = false;
                    values.message = "You Need to Select Atleast One Member for Approval";
                }

            }
        }

        public bool DaGetApprovalDtls(string servicerequest_gid, string Employee_gid, approvallist values)
        {
            values.employee_gid = Employee_gid;
            msSQL = " select requestapproval_gid, approval_name, approval_type, approval_remarks,approval_status,case when approved_date IS NOT NULL " +
                    " then date_format(approved_date,'%d-%m-%Y %h:%i %p') when rejected_date IS NOT NULL then date_format(rejected_date, '%d-%m-%Y %h:%i %p') else date_format(cancelled_date, '%d-%m-%Y %h:%i %p') " +
                    " end as approvaldate, approval_token, concat(b.user_firstname,'', b.user_lastname,' / ', b.user_code) as approvalinitiated_by," +
                    " c.employee_gid as approvalemployee_gid, a.requestapproval_remarks, a.approval_gid,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as approvalreq_date" +
                    " from osd_trn_trequestapproval a " +                   
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                     "left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                    " where servicerequest_gid = '" + servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapprovaldetails = new List<approvaldetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getapprovaldetails.Add(new approvaldetails
                    {
                        requestapproval_gid = (dr_datarow["requestapproval_gid"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approval_date = (dr_datarow["approvaldate"].ToString()),
                        approved_by = (dr_datarow["approval_name"].ToString()),
                        approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                        approval_type = (dr_datarow["approval_type"].ToString()),
                        approval_token = (dr_datarow["approval_token"].ToString()),
                        approvalinitiated_by = (dr_datarow["approvalinitiated_by"].ToString()),
                        requestapproval_remarks = (dr_datarow["requestapproval_remarks"].ToString()),
                        approvalemployee_gid = (dr_datarow["approvalemployee_gid"].ToString()),
                        approvalreq_date = (dr_datarow["approvalreq_date"].ToString()),

                    });
                }
                values.approvaldetails = getapprovaldetails;
            }
            dt_datatable.Dispose();

            msSQL = " select requestapproval_gid, approval_name, approval_type, approval_remarks,approval_status,case when approved_date IS NOT NULL " +
                   " then date_format(approved_date,'%d-%m-%Y %h:%i %p') when rejected_date IS NOT NULL then date_format(rejected_date, '%d-%m-%Y %h:%i %p') else date_format(cancelled_date, '%d-%m-%Y %h:%i %p') " +
                   " end as approvaldate, approval_token, concat(b.user_firstname,'', b.user_lastname,' / ', b.user_code) as approvalinitiated_by " +
                   " from osd_trn_trequestapprovalhistory a " +
                   " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                   " where servicerequest_gid = '" + servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapprovaldetailshistory = new List<approvaldetailshistory>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getapprovaldetailshistory.Add(new approvaldetailshistory
                    {
                        requestapproval_gid = (dr_datarow["requestapproval_gid"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approval_date = (dr_datarow["approvaldate"].ToString()),
                        approved_by = (dr_datarow["approval_name"].ToString()),
                        approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                        approval_type = (dr_datarow["approval_type"].ToString()),
                        approval_token = (dr_datarow["approval_token"].ToString()),
                        approvalinitiated_by = (dr_datarow["approvalinitiated_by"].ToString()),
                    });
                }
                values.approvaldetailshistory = getapprovaldetailshistory;
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaGetAssetDtls(string employee_gid, approvallist values, string servicerequest_gid)
        {
            string lscreated_by = objdbconn.GetExecuteScalar("select created_by from osd_trn_tservicerequest where servicerequest_gid ='" + servicerequest_gid + "'");
            string lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + lscreated_by + "'");

            msSQL = " select a.asset_name,j.asset_id,date_format(c.invoice_date,'%d-%m-%Y %h:%i %p') as invoice_date,a.financial_year,b.status,j.oe_serial as serial_no,c.invoice_refno, " +
                    " b.issued_by as issued_by,date_format(b.issued_date,'%d-%m-%Y %h:%i %p') as issued_date,j.warrantyref_no,j.Warrantystart_date,j.radexpiry_date,j.extenderstart_date,j.extenderend_date" +
                    " from ams_trn_tasset2custodian b " +
                    " left join ams_trn_tassetserial j on b.assetserial_gid=j.assetserial_gid" +
                    " left join ams_mst_tasset a on b.asset_gid=a.asset_gid " +
                    " left join acp_trn_tinvoicedtl g on g.invoicedtl_gid=j.invoicedtl_gid " +
                    " left join acp_trn_tinvoice c on c.invoice_gid=g.invoice_gid " +
                    " left join hrm_mst_temployee k on b.employee_gid=k.employee_gid " +
                    " left join hrm_mst_tbranch br on k.branch_gid=br.branch_gid" +
                    " left join adm_mst_tuser l on k.user_gid=l.user_gid" +
                    " where b.employee_gid = '" + lsemployee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassetdetails = new List<assetdetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getassetdetails.Add(new assetdetails
                    {
                        asset_name = (dr_datarow["asset_name"].ToString()),
                        asset_id = (dr_datarow["asset_id"].ToString()),
                        invoice_date = (dr_datarow["invoice_date"].ToString()),
                        financial_year = (dr_datarow["financial_year"].ToString()),
                        status = (dr_datarow["status"].ToString()),
                        serial_no = (dr_datarow["serial_no"].ToString()),
                        invoice_refno = (dr_datarow["invoice_refno"].ToString()),
                        issued_by = (dr_datarow["issued_by"].ToString()),
                        issued_date = (dr_datarow["issued_date"].ToString()),
                        warrantyref_no = (dr_datarow["warrantyref_no"].ToString()),
                        Warrantystart_date = (dr_datarow["Warrantystart_date"].ToString()),
                        radexpiry_date = (dr_datarow["radexpiry_date"].ToString()),
                        extenderstart_date = (dr_datarow["extenderstart_date"].ToString()),
                        extenderend_date = (dr_datarow["extenderend_date"].ToString()),


                    });
                }
                values.assetdetails = getassetdetails;
            }
            dt_datatable.Dispose();


        }

        // Get Transfer Summary
        public void DaGetTransferSummary(transferlist values, string employee_gid)
        {
            msSQL = " select a.servicerequest_gid,activity_name,request_title,request_description, a.ticket_source as source,raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                    " a.assigned_supportteamname,raised_by as raisedby,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,if (request_status = 'Completed',CONCAT(FLOOR(timestampdiff(day, a.assigned_date, a.created_date)), ' days ', MOD(timestampdiff(hour, a.assigned_date, a.created_date), '24'), ' Hrs ', MOD(timestampdiff(minute, a.assigned_date, a.created_date), '60'), 'Mins'),CONCAT(FLOOR(timestampdiff(day, a.created_date, now())), ' days ', MOD(timestampdiff(hour, a.created_date, now()), '24'), ' Hrs ', MOD(timestampdiff(minute, a.created_date, now()), '60'), 'Mins'))as aging, " +
                    " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as transfer_to, a.reopen_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, a.assigned_membername, bankalert_flag, bankalert2allocated_gid, customer_gid from osd_trn_tservicerequest a " +
                   "  left join hrm_mst_temployee f on a.assigned_membergid = f.employee_gid " +
                    " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                    " left join osd_trn_membertransfer e on e.servicerequest_gid = a.servicerequest_gid " +
                     " left join osd_mst_tactivedepartment s on s.department_gid = a.department_gid " +
                    " where a.transfer_flag='Y' and e.assigned_membergid='" + employee_gid + "' and s.department_status='Y' group by a.servicerequest_gid  order by a.created_date asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettransferlistdtl = new List<transferlistdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " SELECT servicerequest_gid FROM osd_trn_membertransfer " +
                       " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND responce_new='Y' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }


                    gettransferlistdtl.Add(new transferlistdtl
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
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        response_flag = lsresponse_flag,
                        reopen_flag = dt["reopen_flag"].ToString(),
                        assigned_to = dt["transfer_to"].ToString(),
                        transfer_membername = dt["assigned_membername"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.transferlistdtl = gettransferlistdtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostForwardTicket(string user_gid, forwarddtl values)
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

            msSQL = " select assigned_supportteamgid,assigned_supportteamname,assigned_membergid,assigned_membername " +
                    " from osd_trn_tservicerequest " +
                    " where servicerequest_gid='" + values.servicerequest_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lssupportteam_gid = objODBCDatareader["assigned_supportteamgid"].ToString();
                lssupportteam_name = objODBCDatareader["assigned_supportteamname"].ToString();
                lsmember_gid = objODBCDatareader["assigned_membergid"].ToString();
                lsmember_name = objODBCDatareader["assigned_membername"].ToString();
            }
            objODBCDatareader.Close();

            msGetGid = objcmnfunctions.GetMasterGID("RQFR");

            msSQL = "insert into osd_trn_tforwardhistory(" +
                "requestforward_gid," +
                "servicerequest_gid," +
                 "assigned_supportteamgid," +
                "assigned_supportteamname," +
                 "assigned_membergid," +
                "assigned_membername," +
                "forwardto_gid," +
                "forward_to," +
                "forward_remarks," +
                "forward_by," +
                "forward_date," +
                "requestreopen_gid)" +
                "values(" +
                "'" + msGetGid + "'," +
                "'" + values.servicerequest_gid + "'," +
                "'" + lssupportteam_gid + "'," +
                "'" + lssupportteam_name + "'," +
                "'" + lsmember_gid + "'," +
                "'" + lsmember_name + "'," +
                "'" + values.forwardto_gid + "'," +
                "'" + values.forward_to + "'," +
                "'" + values.forward_remarks.Replace("'", "\\'") + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + lsrequestreopen_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = "update osd_trn_tservicerequest set assigned_status='Forward', " +
                   " forwardto_gid='" + values.forwardto_gid + "'," +
                   " forward_to='" + values.forward_to + "'," +
                   " forward_remarks='" + values.forward_remarks.Replace("'", "\\'") + "'," +
                    " forward_flag='Y'," +
                   " forward_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select  tmpforwardreqdocument_gid, document_name, document_path from osd_tmp_tforwardreqdocument where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("FRDO");

                        msSQL = " insert into osd_trn_tforwardreqdocument(" +
                         " forwardreqdocument_gid," +
                         " servicerequest_gid, " +
                         " document_name," +
                         " document_path," +
                         " created_by," +
                         " created_date," +
                         "requestreopen_gid)" +
                         " values(" +
                         "'" + msGetDocumentGid + "'," +
                         "'" + values.servicerequest_gid + "', " +
                         "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                         "'" + dt["document_path"].ToString() + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + lsrequestreopen_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = "delete from osd_tmp_tforwardreqdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Forwarded Successfully..!";
                    // Forward Mail
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
                            " left join hrm_mst_temployee b on a.forwardto_gid = b.employee_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select request_refno,activity_name,request_title, a.assigned_supportteamname,a.assigned_membername,a.request_description,a.request_status," +
                              " concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code) as forwarded_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                              " date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date " +
                              " from osd_trn_tservicerequest a " +
                              " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid " +
                              " left join adm_mst_tuser g on g.user_gid = b.user_gid " +
                                " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                              " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                              " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                              " where servicerequest_gid='" + values.servicerequest_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                            lsactivity_name = objODBCDatareader["activity_name"].ToString();
                            request_title = objODBCDatareader["request_title"].ToString();
                            lsforwardbydtl = objODBCDatareader["forwarded_by"].ToString();
                            lsforwardondtl = objODBCDatareader["forward_date"].ToString();
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
                  "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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
                        //sub = " Service Request Forwarded ";
                        sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Forwarded ";


                        lscontent = HttpUtility.HtmlEncode(values.content);

                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " The service ticket is forwarded to you,the details are as follows,<br />";
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
                        body = body + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
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
                        body = body + "<b>Forwarded By :</b> " + HttpUtility.HtmlEncode(lsforwardbydtl) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Forwarded on :</b> " + HttpUtility.HtmlEncode(lsforwardondtl) + "<br />";
                        body = body + "<br />";
                        body = body + " click the link to enter the web portal and respond <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                        body = body + "<br />";
                        body = body + "<b>Thanks & Regards, </b><br/> ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(lsforwardbydtl) + "<br />";
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
                            //"'" + cc + "'," +
                            "'Service Request Forwarded'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetForwardSummary(forwardlist values, string employee_gid)
        {
            msSQL = " select distinct a.servicerequest_gid,activity_name,request_title,request_description,raised_department as department_name, a.ticket_source as source,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                      " assigned_supportteamname,raised_by as raisedby,if (request_status = 'Completed', CONCAT(FLOOR(timestampdiff(day, a.created_date, a.assigned_date)), ' days ', MOD(timestampdiff(hour, a.created_date, a.assigned_date), '24'), ' Hrs ', MOD(timestampdiff(minute, a.created_date, a.assigned_date), '60'), 'Mins'),CONCAT(FLOOR(timestampdiff(day, a.created_date, now())), ' days ', MOD(timestampdiff(hour, a.created_date, now()), '24'), ' Hrs ', MOD(timestampdiff(minute, a.created_date, now()), '60'), 'Mins'))as aging, " +
                      " getapproval_flag,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, forward_to, a.reopen_flag,  bankalert_flag, bankalert2allocated_gid, customer_gid " +
                      " from osd_trn_tservicerequest a " +
                      " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                      " where a.assigned_status='Forward' and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' and request_status != 'Closed' and request_status != 'Cancelled' and request_status != 'Rejected' order by a.created_date asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwarddtl = new List<forwarddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select getapproval_flag from osd_trn_tservicerequest where servicerequest_gid= '" + dt["servicerequest_gid"].ToString() + "'";
                    var lsgetapproval_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsgetapproval_flag == "Y")
                    {
                        msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where (approval_status = 'Approved' or approval_status = 'Rejected' or approval_status = 'Cancelled') " +
                            " and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            " as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

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

                    msSQL = " SELECT servicerequest_gid FROM osd_trn_tservicerequest " +
                            " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND assignedmember_flag='Y' and created_by<>'" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }
                    if (lsstatus != "Pending")
                    {


                        getforwarddtl.Add(new forwarddtl
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
                            request_refno = dt["request_refno"].ToString(),
                            assigned_team = dt["assigned_supportteamname"].ToString(),
                            getapproval_flag = dt["getapproval_flag"].ToString(),
                            approval_status = lsstatus,
                            response_flag = lsresponse_flag,
                            forward_to = dt["forward_to"].ToString(),
                            reopen_flag = dt["reopen_flag"].ToString(),
                            bankalert_flag = dt["bankalert_flag"].ToString(),
                            Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                            bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                            aging = dt["aging"].ToString(),
                            customer_gid = dt["customer_gid"].ToString(),
                            source = dt["source"].ToString()

                        });
                    }
                    values.forwarddtl = getforwarddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetForward360(string servicerequest_gid, allotteddtl values)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,d.department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                     " assigned_supportteamname, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as raisedby, " +
                     " assigned_membername,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate," +
                     " forward_remarks, date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date, forward_to" +
                     " from osd_trn_tservicerequest a " +
                     " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                     " left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                     " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                     " where a.servicerequest_gid='" + servicerequest_gid + "' and assigned_status='Forward' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values.activity_name = objODBCDatareader["activity_name"].ToString();
                values.request_title = objODBCDatareader["request_title"].ToString();
                values.raised_by = objODBCDatareader["raisedby"].ToString();
                values.raised_department = objODBCDatareader["department_name"].ToString();
                values.departmentname = objODBCDatareader["departmentname"].ToString();
                values.department_gid = objODBCDatareader["department_gid"].ToString();
                values.request_status = objODBCDatareader["request_status"].ToString();
                values.raised_date = objODBCDatareader["raiseddate"].ToString();
                values.request_refno = objODBCDatareader["request_refno"].ToString();
                values.assigned_team = objODBCDatareader["assigned_supportteamname"].ToString();
                values.assigned_member = objODBCDatareader["assigned_membername"].ToString();
                values.request_description = objODBCDatareader["request_description"].ToString();
                values.forward_remarks = objODBCDatareader["forward_remarks"].ToString();
                values.forward_date = objODBCDatareader["forward_date"].ToString();
                values.forward_to = objODBCDatareader["forward_to"].ToString();
            }
            objODBCDatareader.Close();
        }

        public bool DaPostForwardDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("VRDO");

                        msSQL = " insert into osd_tmp_tforwardreqdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select tmpforwardreqdocument_gid,document_name,document_path from osd_tmp_tforwardreqdocument " +
                                " where created_by='" + user_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<upload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            // Create list
                            var file_name = new List<string>();
                            var file_path = string.Empty;

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                file_name.Add(dt["document_name"].ToString());
                                file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                            }
                            objfilename.forwardfilename = file_name.ToArray();
                            objfilename.forwardfilepath = file_path.ToString();

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new upload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    tmp_documentGid = dt["tmpforwardreqdocument_gid"].ToString(),
                                });
                                objfilename.upload_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetForwardtmpDocumentDelete(string tmp_documentGid, result values, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from osd_tmp_tforwardreqdocument where tmpforwardreqdocument_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select tmpforwardreqdocument_gid,document_name,document_path from osd_tmp_tforwardreqdocument " +
                    " where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        tmp_documentGid = dt["tmpforwardreqdocument_gid"].ToString(),
                    });
                    objfilename.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Documents are Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public bool DaPostCompleteDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/CompleteReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("VRDO");

                        msSQL = " insert into osd_tmp_tcompletereqdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select tmpcompletereqdocument_gid,document_name,document_path from osd_tmp_tcompletereqdocument " +
                                " where created_by='" + user_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<upload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            // Create list
                            var file_name = new List<string>();
                            var file_path = string.Empty;

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                file_name.Add(dt["document_name"].ToString());
                                file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                            }
                            objfilename.compfilename = file_name.ToArray();
                            objfilename.compfilepath = file_path.ToString();

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new upload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    tmp_documentGid = dt["tmpcompletereqdocument_gid"].ToString(),
                                });
                                objfilename.upload_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGettmpCompleteDocDelete(string tmp_documentGid, result values, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from osd_tmp_tcompletereqdocument where tmpcompletereqdocument_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select tmpcompletereqdocument_gid,document_name,document_path from osd_tmp_tcompletereqdocument " +
                    " where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        tmp_documentGid = dt["tmpcompletereqdocument_gid"].ToString(),
                    });
                    objfilename.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Documents are Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaGetMultipleForward(string servicerequest_gid, forwardlist values)
        {

            msSQL = " select forward_remarks, date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date,forward_to, " +
                         " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as forwardto from osd_trn_tforwardhistory a" +
                         " left join adm_mst_tuser b on a.forwardto_gid = b.user_gid " +
                         " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid='' order by requestforward_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestdocumentdtl = new List<forwarddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestdocumentdtl.Add(new forwarddtl
                    {
                        forward_remarks = dt["forward_remarks"].ToString(),
                        forward_date = dt["forward_date"].ToString(),
                        forward_to = dt["forward_to"].ToString(),
                    });
                    values.forwarddtl = getservicerequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select a.forward_remarks, date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date,a.forward_to, " +
                         " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as forwardto from osd_trn_tforwardhistory a" +
                         " left join adm_mst_tuser b on a.forwardto_gid = b.user_gid left join osd_trn_tservicerequest c on a.servicerequest_gid=c.servicerequest_gid " +
                         " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid = c.requestreopen_gid order by requestforward_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwardreopendtl = new List<forwardreopendtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getforwardreopendtl.Add(new forwardreopendtl
                    {
                        forward_remarks = dt["forward_remarks"].ToString(),
                        forward_date = dt["forward_date"].ToString(),
                        forward_to = dt["forward_to"].ToString(),
                    });
                    values.forwardreopendtl = getforwardreopendtl;
                }
            }
            dt_datatable.Dispose();

        }

        public void DaGetReopenHistory(string requestreopen_gid, reopenhistory values)
        {
            msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as reopened_by," +
                    " date_format(a.reopened_date,'%d-%m-%Y %h:%i %p') as reopened_date, reopencompleted_flag from osd_trn_treqreopenhistory  a" +
                    " left join adm_mst_tuser b on a.reopened_by = b.user_gid " +
                    " where a.requestreopen_gid='" + requestreopen_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values.raised_by = objODBCDatareader["reopened_by"].ToString();
                values.raised_date = objODBCDatareader["reopened_date"].ToString();
                values.reopencompleted_flag = objODBCDatareader["reopencompleted_flag"].ToString();
            }
            objODBCDatareader.Close();
            // Forward Details
            msSQL = " select forward_remarks, date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date,forward_to " +
                         " from osd_trn_tforwardhistory a" +
                         " left join adm_mst_tuser b on a.forwardto_gid = b.user_gid " +
                         " where a.requestreopen_gid='" + requestreopen_gid + "' order by requestforward_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwardreopendtl = new List<forwardreopendtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getforwardreopendtl.Add(new forwardreopendtl
                    {
                        forward_remarks = dt["forward_remarks"].ToString(),
                        forward_date = dt["forward_date"].ToString(),
                        forward_to = dt["forward_to"].ToString(),
                    });
                    values.forwardreopendtl = getforwardreopendtl;
                }
            }
            dt_datatable.Dispose();
            // Forward Document Details
            msSQL = " select forwardreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                   " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tforwardreqdocument a" +
                   " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                   " where a.requestreopen_gid='" + requestreopen_gid + "' order by forwardreqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwarddocumentdtl = new List<forwarddocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.fwdfilename = file_name.ToArray();
                values.fwdfilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getforwarddocumentdtl.Add(new forwarddocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        forwardreqdocument_gid = dt["forwardreqdocument_gid"].ToString(),
                    });
                    values.forwarddocumentdtl = getforwarddocumentdtl;
                }
            }
            dt_datatable.Dispose();
            // Transfer Details
            msSQL = " select a.transfer_supportteamname, a.transfer_membername,a.transfer_supportteamgid, a.transfer_membergid, a.transfer_date, " +
                  " a.assigned_supportteamname, a.assigned_membername, date_format(a.assigned_date,'%d-%m-%Y %h:%i %p') as assigned_date " +
                  "  from osd_trn_membertransfer a " +
                  " where a.requestreopen_gid = '" + requestreopen_gid + "'";
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
                        assigned_date = dt["assigned_date"].ToString()
                    });
                    values.transferlistdtlreopen = gettransferlistdtlreopen;
                }
            }
            dt_datatable.Dispose();
            // Complete Details
            msSQL = " select completed_remarks, date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, " +
                          " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as completed_by from osd_trn_tcompletedhistory a" +
                           " left join adm_mst_tuser b on a.completed_by = b.user_gid " +
                          " where a.requestreopen_gid='" + requestreopen_gid + "' order by requestcompleted_gid desc";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
                values.completed_by = objODBCDatareader["completed_by"].ToString();
            }
            objODBCDatareader.Close();

            // Complete Document Details
            msSQL = " select completereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tcompletereqdocument a" +
                " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                " where a.requestreopen_gid='" + requestreopen_gid + "' order by completereqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcompletereopendocumentdtl = new List<completereopendocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcompletereopendocumentdtl.Add(new completereopendocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        completereqdocument_gid = dt["completereqdocument_gid"].ToString(),
                    });
                    values.completereopendocumentdtl = getcompletereopendocumentdtl;
                }
            }
            dt_datatable.Dispose();
        }
        // Get Completed Details
        public void DaGetCompletedDetails(string servicerequest_gid, completedlist values)
        {
            msSQL = " select c.completed_flag, a.completed_remarks, date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, " +
                   " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as completed_by from osd_trn_tcompletedhistory a " +
                   " left join adm_mst_tuser b on a.completed_by = b.user_gid " +
                   " left join osd_trn_tservicerequest c on a.servicerequest_gid = c.servicerequest_gid " +
                   " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid='' order by requestcompleted_gid asc";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.completed_flag = objODBCDatareader["completed_flag"].ToString();
                values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
                values.completed_by = objODBCDatareader["completed_by"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
            }
            objODBCDatareader.Close();

            // Complete Document

            msSQL = " select completereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tcompletereqdocument a" +
                " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid=''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcompleterequestdocumentdtl = new List<completerequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.complefilename = file_name.ToArray();
                values.complefilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcompleterequestdocumentdtl.Add(new completerequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        completereqdocument_gid = dt["completereqdocument_gid"].ToString(),
                    });
                    values.completerequestdocumentdtl = getcompleterequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();

            //msSQL = " select c.completed_flag, a.completed_remarks, a.completed_date, " +
            //       " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as completed_by from osd_trn_tcompletedhistory a " +
            //       " left join adm_mst_tuser b on a.completed_by = b.user_gid " +
            //       " left join osd_trn_tservicerequest c on a.servicerequest_gid = c.servicerequest_gid " +
            //         " where a.servicerequest_gid='" + servicerequest_gid + "' and a.requestreopen_gid=c.requestreopen_gid order by requestcompleted_gid desc";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == true)
            //{
            //    values.completed_flag = objODBCDatareader["completed_flag"].ToString();
            //    values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
            //    values.completed_by = objODBCDatareader["completed_by"].ToString();
            //    values.completed_date = objODBCDatareader["completed_date"].ToString();
            //}
            //objODBCDatareader.Close();
        }

        public void DaPostRejectRequest(string user_gid, reject values)
        {
            msSQL = " update osd_trn_tservicerequest set request_status='Rejected', " +
                   " assigned_status='Closed'," +
                   " rejected_remarks='" + values.rejected_remarks.Replace("'", "") + "'," +
                   " rejected_flag='Y'," +
                   " rejected_by='" + user_gid + "'," +
                   " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Service Request Rejected Successfully..!";

                // Reject Mail
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


                    msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid ='" + lsdepartmentgid + "'";
                    cc = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                        " left join hrm_mst_temployee b on a.created_by = b.user_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select request_refno,activity_name,request_title,a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                          " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as rejected_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo,  " +
                          " date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date  " +
                          " from osd_trn_tservicerequest a " +
                          " left join adm_mst_tuser b on b.user_gid = a.rejected_by " +
                          " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                          " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                          " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                          " where servicerequest_gid='" + values.servicerequest_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsactivity_name = objODBCDatareader["activity_name"].ToString();
                        request_title = objODBCDatareader["request_title"].ToString();
                        lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                        lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                        lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                        lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                        lsrequest_status = objODBCDatareader["request_status"].ToString();
                        assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                        lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                        lsrequest_description = objODBCDatareader["request_description"].ToString();
                        lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();
                        lsrejectedbydtl = objODBCDatareader["rejected_by"].ToString();
                        lsrejectedondtl = objODBCDatareader["rejected_date"].ToString();
                    }
                    objODBCDatareader.Close();
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
              "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
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
                    //sub = " Service Request Rejected ";

                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Rejected ";

                    lscontent = HttpUtility.HtmlEncode(values.content);

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " The service ticket is rejected,the details are as follows,<br />";
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
                    body = body + "<b>Activity Name :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
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
                    body = body + "<b>Rejected By  :</b> " + HttpUtility.HtmlEncode(lsrejectedbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Rejected On  :</b> " + HttpUtility.HtmlEncode(lsrejectedondtl) + "<br />";
                    body = body + "<br />";
                    body = body + " click the link to enter the web portal to reply <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                    body = body + "<br />";
                    body = body + "<b>Thanks & Regards, </b><br/> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
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

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    if ((cc != "" || tomail_id != "") || (cc != string.Empty || tomail_id != string.Empty) || (cc != null || tomail_id != null))
                    {
                        smtp.Send(message);
                    }
                    //smtp.Send(message);

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
                        //"'" + cc + "'," +
                        "'Service Request Rejected'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                catch (Exception ex)
                {
                    values.message = "Request Rejected Successfully, Mail Not Sent..!";
                    values.status = false;
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Reject..!";
            }
            msSQL = "update osd_mst_tbusinessstatusactivity set servicerequest_gid='" + values.servicerequest_gid + "'" +
                  " where servicerequest_gid='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        // 
        public bool DaPostTempApprovalMember(string user_gid, approvaldtl values)
        {
            var gettagmemberdtl = new List<approvalmember>();            
            if (values.servicerequest_gid != null && values.servicerequest_gid != "" && values.approvalgid != null && values.approvalgid != "" && values.approvalname != null && values.approvalname != "")

            {
                msSQL = "select employee_gid from osd_tmp_tapprovalmembers a " +
                    " left join hrm_mst_temployee b on a.approval_gid=b.employee_gid where a.servicerequest_gid='" + values.servicerequest_gid + "' and a.approval_gid ='" + values.approvalgid + "' and a.approval_name='" + values.approvalname + "' ";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                msSQL = "select employee_gid from osd_trn_trequestapproval a " +
                  " left join hrm_mst_temployee b on a.approval_gid=b.employee_gid where a.servicerequest_gid='" + values.servicerequest_gid + "' and a.approval_gid ='" + values.approvalgid + "' and a.approval_name='" + values.approvalname + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader1.HasRows == false)
                {
                    msSQL = " insert into osd_tmp_tapprovalmembers(" +
                    " servicerequest_gid," +
                    " approval_gid," +
                    " approval_name," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + values.servicerequest_gid + "'," +
                    "'" + values.approvalgid + "'," +
                    "'" + values.approvalname + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from osd_tmp_tapprovalmembers" +
                         " where servicerequest_gid='" + values.servicerequest_gid + "' order by created_date asc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            gettagmemberdtl.Add(new approvalmember
                            {
                                employee_name = dt["approval_name"].ToString(),
                                employee_gid = dt["approval_gid"].ToString(),
                                tmpapprovalmember_gid = dt["tmpapprovalmember_gid"].ToString(),
                            });

                        }
                    }
                    values.approvalmember = gettagmemberdtl;
                    dt_datatable.Dispose();
                    values.status = true;
                    values.message = "Member Added Successfully";
                    return true;
                }
                else
                {
                    values.approvalmember = gettagmemberdtl;
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "This Member already selected for this Ticket";
                    return false;
                }
            }
            else
            {
                values.approvalmember = gettagmemberdtl;
                values.status = false;
                values.message = "Kindly pass the required parameters";
                return false;
            }
            //msSQL = " insert into osd_tmp_tapprovalmembers(" +
            //        " servicerequest_gid," +
            //        " approval_gid," +
            //        " approval_name," +
            //        " created_by, " +
            //        " created_date)" +
            //        " values(" +
            //        "'" + values.servicerequest_gid + "'," +
            //        "'" + values.approvalgid + "'," +
            //        "'" + values.approvalname + "'," +
            //        "'" + user_gid + "'," +
            //        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //if (mnResult != 0)
            //{

            //    values.status = true;
            //    return true;
            //}
            //else
            //{
            //    values.status = false;
            //    return false;
            //}
        }

        public void DaGetTmpApprovalMembersView(string servicerequest_gid, approvaldtl values)
        {

            msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from osd_tmp_tapprovalmembers" +
                    " where servicerequest_gid='" + servicerequest_gid + "' order by created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettagmemberdtl = new List<approvalmember>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettagmemberdtl.Add(new approvalmember
                    {
                        employee_name = dt["approval_name"].ToString(),
                        employee_gid = dt["approval_gid"].ToString(),
                        tmpapprovalmember_gid = dt["tmpapprovalmember_gid"].ToString(),
                    });
                    values.approvalmember = gettagmemberdtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetTmpApprovalMembersDelete(approvaldtl values)
        {
            msSQL = " delete from osd_tmp_tapprovalmembers where tmpapprovalmember_gid='" + values.tmpapprovalmember_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from osd_tmp_tapprovalmembers" +
        " where servicerequest_gid='" + values.servicerequest_gid + "' order by created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettagmemberdtl = new List<approvalmember>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettagmemberdtl.Add(new approvalmember
                    {
                        employee_name = dt["approval_name"].ToString(),
                        employee_gid = dt["approval_gid"].ToString(),
                        tmpapprovalmember_gid = dt["tmpapprovalmember_gid"].ToString(),
                    });
                    values.approvalmember = gettagmemberdtl;
                }
            }
            dt_datatable.Dispose();

            //if (mnResult != 0)
            //{
            //    values.status = true;
            //    values.message = "Documents are Deleted Successfully..!";
            //}
            //else
            //{
            //    values.status = false;
            //    values.message = "Error Occured..!";
            //}
        }
        public void DaGetTmpAllMembersDelete(approvaldtl values)
        {
            msSQL = " delete from osd_tmp_tapprovalmembers";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }
        public void DaGetTmpAllMembersDeleteFn(approvaldtl values, string servicerequest_gid, string user_gid)
        {
            msSQL = " delete from osd_tmp_tapprovalmembers where  created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaGetEmployeeNotIn(supportteamviewdtl values, string servicerequest_gid)
        {

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' and employee_gid not in (select distinct approval_gid from osd_trn_trequestapproval where servicerequest_gid='" + servicerequest_gid + "' and approval_status in ('Pending') order by a.user_firstname asc " +
               " )";
               

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettagmemberdtl = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettagmemberdtl.Add(new employeelist
                    {
                        employee_name = dt["employee_name"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                    });
                    values.employeelist = gettagmemberdtl;
                }
            }
            dt_datatable.Dispose();

        }

        public bool DaGetApprovalDtlsByToken(string requestapproval_gid, approvallist values)
        {
            msSQL = " select servicerequest_gid from osd_trn_trequestapproval " +
                    " where requestapproval_gid='" + requestapproval_gid + "'";
            string lsrequestapproval_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select requestapproval_gid, approval_name, approval_type, approval_remarks,approval_status,case when approved_date IS NOT NULL " +
                    " then date_format(approved_date,'%d-%m-%Y %h:%i %p') when rejected_date IS NOT NULL then date_format(rejected_date, '%d-%m-%Y %h:%i %p') else date_format(cancelled_date, '%d-%m-%Y %h:%i %p') " +
                    " end as approvaldate, concat(b.user_firstname,'', b.user_lastname,' / ', b.user_code) as approvalinitiated_by, requestapproval_remarks" +
                    " from osd_trn_trequestapproval a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where servicerequest_gid = '" + lsrequestapproval_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapprovaldetails = new List<approvaldetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapprovaldetails.Add(new approvaldetails
                    {
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approval_date = (dr_datarow["approvaldate"].ToString()),
                        approved_by = (dr_datarow["approval_name"].ToString()),
                        approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                        approval_type = (dr_datarow["approval_type"].ToString()),
                        approvalinitiated_by = (dr_datarow["approvalinitiated_by"].ToString()),
                        requestapproval_remarks = (dr_datarow["requestapproval_remarks"].ToString()),
                        requestapproval_gid = (dr_datarow["requestapproval_gid"].ToString()),
                    });
                }
                values.approvaldetails = getapprovaldetails;
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaGetServiceRequestForwardView360Update(string servicerequest_gid, servicerequestview values)
        {
            msSQL = " update osd_trn_tservicerequest set " +
                " assignedmember_flag=''" +
                " where servicerequest_gid='" + servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaGetServiceRequestTransferView360Update(string servicerequest_gid, servicerequestview values, string employee_gid)
        {
            msSQL = " update osd_trn_membertransfer set " +
                    " responce_new=''" +
                    " where servicerequest_gid='" + servicerequest_gid + "' and assigned_membergid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaGetRequestRemarks(requestapproval values, string requestapproval_gid)
        {
            msSQL = " select requestapproval_remarks from osd_trn_trequestapproval where requestapproval_gid='" + requestapproval_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.requestapproval_remarks = objODBCDatareader["requestapproval_remarks"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }
        public void DaGetBusinessunitStatusMyActivity(string servicerequest_gid, requestorlist values)
        {

            string lsdepartment_gid;
            msSQL = " select department_gid from osd_trn_tservicerequest where servicerequest_gid='" + servicerequest_gid + "'";

            lsdepartment_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select business_status,businessstatus_gid from osd_mst_tbusinessstatus where businessunit_gid ='" + lsdepartment_gid + "'" +
                    " order by businessstatus_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupportdtlList = new List<businessstatuslist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsupportdtlList.Add(new businessstatuslist
                    {
                        business_status = dt["business_status"].ToString(),
                        businessstatus_gid = dt["businessstatus_gid"].ToString(),

                    });
                    values.businessstatuslist = getsupportdtlList;
                }
            }
            dt_datatable.Dispose();
        }


        public bool DaPostBusinessUnitStatusMyActivity(forwarddtl values, string user_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into osd_mst_tbusinessstatusactivity(" +
                    " businessstatusactivity_gid," +
                    " servicerequest_gid," +
                    " businessactivity_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + user_gid + "'," +
                    "'" + values.business_status + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Business Unit Status Added Successfully";
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Business Unit Status";
                objdbconn.CloseConn();
                return false;
            }
        }

        public bool DaPostBusinessUnitStatusWorkinProgress(forwarddtl values, string user_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into osd_mst_tbusinessstatusactivity(" +
                    " businessstatusactivity_gid," +
                    " servicerequest_gid," +
                    " businessactivity_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.servicerequest_gid + "'," +
                    "'" + values.business_status + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Business Unit Status Added Successfully";
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Business Unit Status";
                objdbconn.CloseConn();
                return false;
            }
        }
        public void DaGetApprovalPendingSummary(allottedlist values, string employee_gid)
        {
            msSQL = " select distinct a.servicerequest_gid,activity_name,request_title,request_description,raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno," +
                   " getapproval_flag,assigned_supportteamname,assigned_membername,raised_by as raisedby,if (request_status = 'Completed', CONCAT(FLOOR(timestampdiff(day, a.created_date, a.assigned_date)), ' days ', MOD(timestampdiff(hour, a.created_date, a.assigned_date), '24'), ' Hrs ', MOD(timestampdiff(minute, a.created_date, a.assigned_date), '60'), 'Mins'),CONCAT(FLOOR(timestampdiff(day, a.created_date, now())), ' days ', MOD(timestampdiff(hour, a.created_date, now()), '24'), ' Hrs ', MOD(timestampdiff(minute, a.created_date, now()), '60'), 'Mins'))as aging, " +
                  "  date_format(a.created_date, '%d-%m-%Y %h:%i %p') as raiseddate,date_format(f.created_date, '%d-%m-%Y %h:%i %p') as approvalreq_date, " +
                  " assigned_membergid, assigned_supportteamgid,f.approval_status, a.ticket_source as source, bankalert_flag, (select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status,bankalert2allocated_gid, customer_gid from osd_trn_tservicerequest a " +
                  " left join osd_trn_trequestapproval f on a.servicerequest_gid = f.servicerequest_gid " +
                  " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                   " where f.approval_status='Pending' and a.assigned_membergid='" + employee_gid + "' and e.department_status='Y' order by a.created_date asc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getallotteddtl = new List<allotteddtl>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getallotteddtl.Add(new allotteddtl
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
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        getapproval_flag = dt["getapproval_flag"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        response_flag = lsresponse_flag,
                        assigned_membergid = dt["assigned_membergid"].ToString(),
                        assigned_supportteamgid = dt["assigned_supportteamgid"].ToString(),
                        assigned_member = dt["assigned_membername"].ToString(),
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        approvalreq_date = dt["approvalreq_date"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        aging = dt["aging"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.allotteddtl = getallotteddtl;
                }

            }
            dt_datatable.Dispose();
        }
        public int SendSMTP2(string strFrom, string strpwd, string strTo, string strSubject, string strBody, string strCC, string strBCC, string strAttachments)
        {

            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                ls_server = objODBCDatareader["pop_server"].ToString();
                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                ls_username = objODBCDatareader["pop_username"].ToString();
                ls_password = objODBCDatareader["pop_password"].ToString();
            }
            objODBCDatareader.Close();
            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(strFrom);
            // Set the recepient address of the mail message
            objMailMessage.To.Add(new MailAddress(strTo));


            if (strCC != null & strCC != string.Empty)
            {
                lsCCReceipients = strCC.Split(',');
                if (strCC.Length == 0)
                {
                    objMailMessage.CC.Add(new MailAddress(strCC));
                }
                else
                {
                    foreach (string CCEmail in lsCCReceipients)
                    {
                        objMailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    }
                }
            }

            if (strBCC != null & strBCC != string.Empty)
            {
                objMailMessage.Bcc.Add(new MailAddress(strBCC));
            }

            objMailMessage.Subject = strSubject;
            // Set the body of the mail message
            objMailMessage.Body = strBody;

            // Set the format of the mail message body as HTML
            objMailMessage.IsBodyHtml = true;
            //  Set the priority of the mail message to normal
            objMailMessage.Priority = MailPriority.Normal;
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = ls_server;
            objSmtpClient.Port = ls_port;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            objSmtpClient.Credentials = new NetworkCredential(strFrom, strpwd);
            try
            {
                objSmtpClient.Send(objMailMessage);
            }
            catch
            {
                return 0;
            }

            return 1;
        }
    }
}