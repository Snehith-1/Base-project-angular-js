using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Globalization;
using OfficeOpenXml;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using System.Net.Mail;
using ems.storage.Functions;
using System.Configuration;

namespace ems.audit.DataAccess
{
    public class DaAtmTrnMyAuditTaskAuidtee
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;

        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGettaguser2audit_gid, lspath, lsdocument_attached, lsquery_status, lssession_user, count, lsauditdepartment_value, lscapture_yesscore, lscapture_noscore, lscapture_partialscore, lscapture_nascore, lscapture_totalscore, msGetaudituniqueno, lsdue_date, lsreport_date, lsperiodfrom_date, lsauditperiod_to, lsauditname_value;
        int mnResult;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, employee_reporting_to;
        int k, ls_port;
        string lsemployee_name,lsauditmaker_gid,lsauditormakerchecker_flag,lsauditormakerapprover_flag,lsauditorcheckerapprover_flag,lsauditchecker_gid,lsauditapprover_gid, lsemployee_gid,lsquery_title, lssample_name, lsauditeemaker_gid, lsTo2members, lsauditorchecker_name, lsauditeechecker_gid, lsauditmapping_gid, lsauditormaker_name, lsBccmail_id, lscreated_by, lscc2members, lsto2members, lsauditcreation_gid, lscreated_date, lsto_mail, frommail_id, lscc_mail, strBCC, lsbcc_mail, lsaudit_name, lsaaudit_uniqueno, lsaudit_description, lsauditdepartment_name, lsaudittype_name, lscheckpointgroup_name;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;
        public string[] lsToReceipients;

        public void DaGetMyAuditTaskAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditquery_flag,a.auditfrequency_name,a.auditpriority_name,a.auditmaker_name, a.auditchecker_name,a.auditapprover_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, " +
                    " c.auditmaker_name as auditee_Maker, c.auditchecker_name as auditee_checker,c.employee_gid as auditeemaker_gid,group_concat(g.sampleimport_gid) as sampleimport_gid,c.auditmapping_gid as auditeechecker_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
                    " left join atm_trn_ttaguser2audit d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " left join atm_trn_tsampleimport g on g.auditcreation_gid = a.auditcreation_gid " +
                    " where (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "'or d.employee_gid='" + Employee_gid + "') group by a.auditcreation_gid desc ";
                //"  (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') order by a.auditcreation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittask_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittask_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            auditquery_flag = (dr_datarow["auditquery_flag"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittask_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetMyAuditTaskAuditeeMaker(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = " select distinct a.auditcreation_gid,a.audit_name,a.auditdepartment_name,a.audittype_name,a.auditquery_flag,a.auditfrequency_name,a.auditpriority_name,a.auditmaker_name, " +
                    " a.auditchecker_name,a.auditapprover_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag, " +
                    " a.checklistmaster_gid, a.auditeemaker_gid,a.auditeechecker_gid,group_concat(g.sampleimport_gid) as sampleimport_gid, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by " +
                    " from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
                    " left join atm_mst_ttaguser2employee d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " left join atm_trn_tsampleimport g on g.auditcreation_gid = a.auditcreation_gid " +
                    " where (d.employee_gid='" + Employee_gid + "') and a.status not in ('Hold','Closed','Completed') and (auditapproval_flag = 'Y') group by d.auditcreation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittask_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittask_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            auditquery_flag = (dr_datarow["auditquery_flag"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittask_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetMyAuditTaskAuditeeChecker(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditquery_flag,a.auditfrequency_name,a.auditpriority_name,a.auditmaker_name, a.auditchecker_name,a.auditapprover_name,a.audit_uniqueno,a.approval_status,a.due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, " +
                    " h.auditeemaker_gid,h.auditeechecker_gid,group_concat(g.sampleimport_gid) as sampleimport_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
                    " left join atm_trn_ttaguser2audit d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " left join atm_trn_tsampleimport g on g.auditcreation_gid = a.auditcreation_gid " +
                    " left join atm_trn_tauditagainstmultipleauditeechecker h on h.auditcreation_gid = a.auditcreation_gid " +
                    " where (h.auditeechecker_gid='" + Employee_gid + "') and a.auditormaker_approvalflag='Y' " +
                    " and (h.auditeechecker_approvalflag='N' or h.auditeechecker_approvalflag='Y') and a.status not in ('Hold','Closed','Completed') " +
                    " group by h.auditcreation_gid desc, a.auditeechecker_approvalflag asc  ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittask_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittask_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            auditquery_flag = (dr_datarow["auditquery_flag"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittask_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }



        public void DaMyAuditTaskViewAuditee(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {
            try
            {
                msSQL = " select a.auditcreation2checklist_gid,a.auditcreation_gid,a.checklistmasteradd_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description," +
                      " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score, a.partial_score, a.na_score, b.capture_score" +
                      " from atm_trn_tauditcreation2checklist a " +
                      " left join atm_trn_tcheckpointobservation b on a.auditcreation2checklist_gid=b.auditcreation2checklist_gid " +
                        " where a.auditcreation_gid='" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcheckpointobservationviewAuditee_list = new List<checkpointobservationviewAuditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcheckpointobservationviewAuditee_list.Add(new checkpointobservationviewAuditee_list
                        {
                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                            checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                            noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                            yes_score = (dr_datarow["yes_score"].ToString()),
                            no_score = (dr_datarow["no_score"].ToString()),
                            partial_score = (dr_datarow["partial_score"].ToString()),
                            na_score = (dr_datarow["na_score"].ToString()),
                            capture_score = (dr_datarow["capture_score"].ToString()),


                        });
                    }
                    values.checkpointobservationviewAuditee_list = getcheckpointobservationviewAuditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount  where auditcreation_gid ='" + auditcreation_gid + "'";
                values.total_score = objdbconn.GetExecuteScalar(msSQL);
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            catch
            {
                values.status = false;
            }
        }

        public void DaEditMyAuditTaskAuditee(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select a.auditcreation_gid,b.checklistmaster_gid,(b.auditmaker_name) as audit_makername,(b.auditchecker_name) as audit_checkername,audit_uniqueno,a.audit_name,auditpriority_gid,auditpriority_name,a.status,a.auditmapping_gid,a.auditmaker_name,a.employee_gid,auditmapping2employee_gid,a.auditchecker_name,auditapprover_name,auditfrequency_gid,auditfrequency_name, date_format(due_date,'%d-%m-%Y') as due_date , date_format(report_date,'%d-%m-%Y') as report_date , date_format(auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate , date_format(auditperiod_todate,'%d-%m-%Y') as auditperiod_todate from atm_trn_tauditcreation a " +
            " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid" +
            " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                values.audit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                values.audit_name = objODBCDatareader["audit_name"].ToString();
                values.auditpriority_gid = objODBCDatareader["auditpriority_gid"].ToString();
                values.auditpriority_name = objODBCDatareader["auditpriority_name"].ToString();
                values.audit_maker = objODBCDatareader["auditmaker_name"].ToString();
                values.auditmapping_gid = objODBCDatareader["auditmapping_gid"].ToString();
                values.audit_checker = objODBCDatareader["auditchecker_name"].ToString();
                values.auditfrequency_gid = objODBCDatareader["auditfrequency_gid"].ToString();
                values.auditfrequency_name = objODBCDatareader["auditfrequency_name"].ToString();
                values.audit_approver = objODBCDatareader["auditapprover_name"].ToString();
                values.due_date = objODBCDatareader["due_date"].ToString();
                values.report_date = objODBCDatareader["report_date"].ToString();
                values.periodfrom_date = objODBCDatareader["auditperiod_fromdate"].ToString();
                values.auditperiod_to = objODBCDatareader["auditperiod_todate"].ToString();
                values.status_update = objODBCDatareader["status"].ToString();
                values.auditmaker_name = objODBCDatareader["audit_makername"].ToString();
                values.auditchecker_name = objODBCDatareader["audit_checkername"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }
        public void DaGetCheckpointObservationAuditee(MdlAtmTrnMyAuditTaskAuditee values)
        {
            try
            {
                msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcheckpointobservationAuditee_list = new List<checkpointobservationAuditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcheckpointobservationAuditee_list.Add(new checkpointobservationAuditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),

                        });
                    }
                    values.checkpointobservationAuditee_list = getcheckpointobservationAuditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaEditCheckpointObservationAuditee(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select a.auditcreation_gid,b.checklistmaster_gid,(b.auditmaker_name) as audit_makername,(b.auditchecker_name) as audit_checkername,audit_uniqueno,a.audit_name,auditpriority_gid,auditpriority_name,a.auditmapping_gid,a.auditmaker_name,a.employee_gid,auditmapping2employee_gid,a.auditchecker_name,auditapprover_name,auditfrequency_gid,auditfrequency_name, date_format(due_date,'%d-%m-%Y') as due_date , date_format(report_date,'%d-%m-%Y') as report_date , date_format(auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate , date_format(auditperiod_todate,'%d-%m-%Y') as auditperiod_todate from atm_trn_tauditcreation a " +
            " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid" +
            " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                values.audit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                values.audit_name = objODBCDatareader["audit_name"].ToString();
                values.auditpriority_gid = objODBCDatareader["auditpriority_gid"].ToString();
                values.auditpriority_name = objODBCDatareader["auditpriority_name"].ToString();
                values.audit_maker = objODBCDatareader["auditmaker_name"].ToString();
                values.auditmapping_gid = objODBCDatareader["auditmapping_gid"].ToString();
                values.audit_checker = objODBCDatareader["auditchecker_name"].ToString();
                values.auditfrequency_gid = objODBCDatareader["auditfrequency_gid"].ToString();
                values.auditfrequency_name = objODBCDatareader["auditfrequency_name"].ToString();
                values.audit_approver = objODBCDatareader["auditapprover_name"].ToString();
                values.due_date = objODBCDatareader["due_date"].ToString();
                values.report_date = objODBCDatareader["report_date"].ToString();
                values.periodfrom_date = objODBCDatareader["auditperiod_fromdate"].ToString();
                values.auditperiod_to = objODBCDatareader["auditperiod_todate"].ToString();
                values.auditmaker_name = objODBCDatareader["audit_makername"].ToString();
                values.auditchecker_name = objODBCDatareader["audit_checkername"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }


        public void DaGetMyAuditTaskStatus(MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {
            msSQL = " update atm_trn_tauditcreation set status='" + values.status_update + "'" +
                    " where auditcreation_gid='" + values.auditcreation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Status updated Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("ASUL");
                msSQL = " insert into atm_trn_tstatusupdatelog (" +
                      " statusupdatelog_gid, " +
                      " auditcreation_gid," +
                      " status," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditcreation_gid + "'," +
                      " '" + values.status_update + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }


        public void DaGetAuditeeIntent(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select checkpoint_intent  from atm_trn_tauditcreation2checklist " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetAuditeeDescription(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select checkpoint_description  from atm_trn_tauditcreation2checklist " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetAuditeeNotes(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select noteto_auditor  from atm_trn_tauditcreation2checklist " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
            }
            objODBCDatareader.Close();

        }



        public void DaAssignedQuerySummary(string employee_gid, MdlAtmTrnMyAuditTaskAuditee values, string auditcreation_gid)
        {
            try
            {
                msSQL = " SELECT a.raisequery_gid, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, a.description, " +
                    " (a.employee_name) as assigned_to,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    "  a.employee_gid " +
                    " FROM atm_trn_traisequery a " +
                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = a.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                " WHERE a.auditcreation_gid= '" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getAssignedQueryListAuditee = new List<AssignedQueryListAuditee>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getAssignedQueryListAuditee.Add(new AssignedQueryListAuditee

                        {

                            raisequery_gid = (dr_datarow["raisequery_gid"].ToString()),
                            assigned_by = (dr_datarow["created_by"].ToString()),
                            assigned_to = (dr_datarow["assigned_to"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            description = (dr_datarow["description"].ToString()),

                        });
                    }
                    values.AssignedQueryListAuditee = getAssignedQueryListAuditee;
                }

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaEditAssignedQuery(string raisequery_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select raisequery_gid from atm_trn_traisequery " +
                    " where raisequery_gid='" + raisequery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.raisequery_gid = objODBCDatareader["raisequery_gid"].ToString();

            }
            objODBCDatareader.Close();
            values.status = true;
        }


        public void DaPostReplyToQuery(MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {

            msSQL = " update atm_trn_traisequery set reply_query='" + values.reply_query.Replace("'", "") + "'" +
                   " where raisequery_gid='" + values.raisequery_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            {
                msGetGid = objcmnfunctions.GetMasterGID("AROQ");
                msSQL = "Insert into atm_trn_treplytoquery( " +
                       " replytoquery_gid, " +
                       " raisequery_gid, " +
                       " reply_query," +
                       " updated_by," +
                       " updated_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.raisequery_gid + "'," +
                        "'" + values.reply_query.Replace("'", "") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Replied to Query Successfully";
            }
            else
            {
                values.message = "Error Occured While Replying to Query";
                values.status = false;
            }
        }

        public bool DaPostQuerydetail(string employee_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AUQU");
            msSQL = " insert into atm_trn_tauditqueries(" +
                    " auditquery_gid," +
                    " auditcreation_gid," +
                    " remarks," +
                    " creatorresponder_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.auditcreation_gid + "'," +
                    "'" + values.remarks + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " update atm_trn_tauditcreation set auditquery_flag ='Y'  where auditcreation_gid='" + values.auditcreation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Query Raised Successfully";
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
        public bool DaGetQuerydetaillist(string employee_gid, string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {

            msSQL = "select a.auditquery_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,a.creatorresponder_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from atm_trn_tauditqueries a " +
                    " left join hrm_mst_temployee b on a.creatorresponder_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.auditcreation_gid = '" + auditcreation_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getQuerydetaillist = new List<Querydetaillist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["creatorresponder_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }

                    getQuerydetaillist.Add(new Querydetaillist
                    {
                        auditquery_gid = (dr_datarow["auditquery_gid"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        session_user = lssession_user,
                    });
                }
                values.Querydetaillist = getQuerydetaillist;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public void DaGetMyAuditTaskCounts(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            msSQL = " select ( select count(a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
                " left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid where (a.auditeemaker_gid='" + Employee_gid + "') and (b.status = 'Hold') and (auditapproval_flag='Y')) as auditsonhold_count , " +
" (select count(a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid where (a.auditeemaker_gid='" + Employee_gid + "') and (b.status = 'Closed') and (auditapproval_flag='Y')) as closedaudit_count, " +
" (select count(a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid where a.auditeemaker_gid='" + Employee_gid + "' and b.status = 'Completed') as completedaudit_count, " +
" (select count(distinct a.auditcreation_gid) from atm_mst_ttaguser2employee a left join atm_trn_tauditcreation b on b.auditcreation_gid = a.auditcreation_gid where (a.employee_gid ='" + Employee_gid + "') and (b.status not in ('Hold','Closed','Completed')) and (b.auditapproval_flag='Y')) as taggedsample_count , " +
" (select count(a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid where (a.auditeemaker_gid='" + Employee_gid + "') and b.auditormaker_approvalflag='Y' and (a.auditeechecker_approvalflag='N' or a.auditeechecker_approvalflag='Y') and b.status not in ('Hold','Closed','Completed') ) as pendingapproval_count, " +
" (select count(distinct a.auditcreation_gid)  from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid and a.auditeemaker_gid='" + Employee_gid + "' where (a.auditeemaker_gid='" + Employee_gid + "') and (b.status not in ('Hold','Closed','Completed')) and (b.auditapproval_flag='Y'))  as openaudit_count";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditsonhold_count = objODBCDatareader["auditsonhold_count"].ToString();
                values.closedaudit_count = objODBCDatareader["closedaudit_count"].ToString();
                values.completedaudit_count = objODBCDatareader["completedaudit_count"].ToString();
                values.openaudit_count = objODBCDatareader["openaudit_count"].ToString();
                values.taggedsample_count = objODBCDatareader["taggedsample_count"].ToString();
                values.pendingapproval_count = objODBCDatareader["pendingapproval_count"].ToString();

            }
            objODBCDatareader.Close();
        }
        
        public void DaGetMyAuditTaskCheckerCounts(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {

            msSQL = " select (select count(a.auditcreation_gid)  from atm_trn_tauditagainstmultipleauditeechecker a " + 
                " left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid where (a.auditeechecker_gid='" + Employee_gid + "') and (b.status = 'Hold') and ((auditapproval_flag='Y'))) as auditscheckeronhold_count, " +
" (select count(a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid  where (a.auditeechecker_gid='" + Employee_gid + "') and (b.status = 'Closed') and (auditapproval_flag='Y')) as closedcheckeraudit_count, " +
" (select count(distinct a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid and a.auditeechecker_gid='" + Employee_gid + "'  where (a.auditeechecker_gid='" + Employee_gid + "') and (b.status not in ('Hold','Closed','Completed')) and ((b.auditapproval_flag='Y') and (b.auditormaker_approvalflag='N') and b.auditorchecker_approvalflag='N' and a.auditeechecker_approvalflag='N' and b.approval_status not in ('Checker - Auditee Pending','Checker Approval pending')))  as opencheckeraudit_count, " +
" (select count(auditcreation_gid) from atm_mst_ttaguser2employee where employee_gid ='" + Employee_gid + "') as taggedsample_count, " +
"  (select count(a.auditcreation_gid)  from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid  where a.auditeechecker_gid='" + Employee_gid + "' and b.status = 'Completed') as completedaudit_count," +
" (select count(distinct a.auditcreation_gid) from atm_trn_tauditagainstmultipleauditeechecker a " +
" left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid  where (a.auditeechecker_gid='" + Employee_gid + "') and b.auditormaker_approvalflag='Y' and (a.auditeechecker_approvalflag='N' or a.auditeechecker_approvalflag='Y') and b.status not in ('Hold','Closed','Completed')) as pendingapproval_count";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditscheckeronhold_count = objODBCDatareader["auditscheckeronhold_count"].ToString();
                values.closedcheckeraudit_count = objODBCDatareader["closedcheckeraudit_count"].ToString();
                values.opencheckeraudit_count = objODBCDatareader["opencheckeraudit_count"].ToString();
                values.taggedsample_count = objODBCDatareader["taggedsample_count"].ToString();
                values.pendingapproval_count = objODBCDatareader["pendingapproval_count"].ToString();
                values.completedaudit_count = objODBCDatareader["completedaudit_count"].ToString();

            }
            objODBCDatareader.Close();
        }



        public void DaPostSampleQuerydetail(string employee_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    "where b.employee_gid ='" + employee_gid + "'";
            employeename = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("AUSQ");
            msSQL = " insert into atm_trn_tsamplequeries2response(" +
                    " samplequeries2response_gid," +
                    " sampleraisequery_gid, " +
                    " auditcreation_gid," +
                    " sampleimport_gid," +
                    " remarks," +
                    " replied_by," +
                     " raisequery_replyby," +
                    " sampleresponse_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.sampleraisequery_gid + "'," +
                    "'" + values.auditcreation_gid + "'," +
                    "'" + values.sampleimport_gid + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + values.replied_by + "'," +
                       "'" + employeename + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " update atm_trn_tsamplequeries set samplequery_status='Open'  where sampleimport_gid='" + values.sampleimport_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Message Posted Successfully";
                values.status = true;


                msSQL = " select  auditcreation_gid,auditeechecker_gid, auditeemaker_gid from atm_trn_tauditagainstmultipleauditeechecker " +
                                   " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsauditeechecker_gid = objODBCDatareader["auditeechecker_gid"].ToString();
                    lsauditeemaker_gid = objODBCDatareader["auditeemaker_gid"].ToString();
                }
                objODBCDatareader.Close();
                if (lsauditeechecker_gid == employee_gid || lsauditeemaker_gid == employee_gid)
                {
                    try
                    {


                        msSQL = " select  auditcreation_gid,employee_gid,created_by, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                            lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        }
                        objODBCDatareader.Close();
                        if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditapprover_gid && lsauditapprover_gid == lsauditmaker_gid)

                        {
                            k = 1;
                            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                            }
                            objODBCDatareader.Close();
                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["created_by"].ToString();
                                lscc2members = objODBCDatareader["CC2members"].ToString();
                                lsTo2members = objODBCDatareader["auditmapping_gid"].ToString();
                                lscreated_date = objODBCDatareader["created_date"].ToString();
                                lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                            }
                            objODBCDatareader.Close();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            k = k + 1;
                            lsTo2members = lsTo2members.Replace(employee_gid, " ");
                            lsTo2members.Replace(",,", ",");

                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                            lscc2members = lscc2members.Replace(employee_gid, "");
                            lscc2members.Replace(",,", ",");

                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                   " from atm_trn_tauditcreation a  " +
                                   " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                   " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                lsquery_title = objODBCDatareader["query_title"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                            " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);



                            sub = " RE: Query Response  ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Response The Query.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employee_name);
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(lsto_mail));


                            lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }
                            if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                            {
                                lsToReceipients = lsto_mail.Split(',');
                                if (lsto_mail.Length == 0)
                                {
                                    message.To.Add(new MailAddress(lsto_mail));
                                }
                                else
                                {
                                    foreach (string ToEmail in lsToReceipients)
                                    {
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }

                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
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
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);

                            values.status = true;
                            if (values.status == true)
                            {
                                msSQL = "Insert into atm_trn_tauditmailcount( " +
                                   " auditcreation_gid," +
                                   " sampleimport_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + values.sampleimport_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Response Received'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else
                        {

                            msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                            if (lsauditormakerchecker_flag == "Y")

                            {
                                k = 1;
                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                    ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                }
                                objODBCDatareader.Close();
                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                                " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                    lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lscreated_by = objODBCDatareader["created_by"].ToString();
                                    lscc2members = objODBCDatareader["CC2members"].ToString();
                                    lsTo2members = objODBCDatareader["To2members"].ToString();
                                    lscreated_date = objODBCDatareader["created_date"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                }
                                objODBCDatareader.Close();

                                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                sToken = "";
                                int Length = 100;
                                for (int j = 0; j < Length; j++)
                                {
                                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                    sToken += sTempChars;
                                }

                                k = k + 1;
                                lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                lsTo2members.Replace(",,", ",");

                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                lscc2members = lscc2members.Replace(employee_gid, "");
                                lscc2members.Replace(",,", ",");

                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                       " from atm_trn_tauditcreation a  " +
                                       " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                    lsquery_title = objODBCDatareader["query_title"].ToString();
                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                " where employee_gid = '" + employee_gid + "'";
                                string employee_name = objdbconn.GetExecuteScalar(msSQL);



                                sub = " RE: Query Response  ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to Response The Query.";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "Thanks & Regards, ";
                                body = body + "<br />";
                                body = body + HttpUtility.HtmlEncode(employee_name);
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                //message.To.Add(new MailAddress(lsto_mail));


                                lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                {
                                    lsBCCReceipients = lsBccmail_id.Split(',');
                                    if (lsBccmail_id.Length == 0)
                                    {
                                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                                    }
                                    else
                                    {
                                        foreach (string BCCEmail in lsBCCReceipients)
                                        {
                                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                        }
                                    }
                                }
                                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                {
                                    lsToReceipients = lsto_mail.Split(',');
                                    if (lsto_mail.Length == 0)
                                    {
                                        message.To.Add(new MailAddress(lsto_mail));
                                    }
                                    else
                                    {
                                        foreach (string ToEmail in lsToReceipients)
                                        {
                                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
                                        }
                                    }
                                }

                                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                {
                                    lsCCReceipients = cc_mailid.Split(',');
                                    if (cc_mailid.Length == 0)
                                    {
                                        message.CC.Add(new MailAddress(cc_mailid));
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
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(message);

                                values.status = true;
                                if (values.status == true)
                                {
                                    msSQL = "Insert into atm_trn_tauditmailcount( " +
                                       " auditcreation_gid," +
                                       " sampleimport_gid," +
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + values.sampleimport_gid + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Response Received'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            else
                            {

                                msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                                if (lsauditormakerapprover_flag == "Y")

                                {
                                    k = 1;
                                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        ls_server = objODBCDatareader["pop_server"].ToString();
                                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                    }
                                    objODBCDatareader.Close();
                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                                " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lscreated_by = objODBCDatareader["created_by"].ToString();
                                        lscc2members = objODBCDatareader["CC2members"].ToString();
                                        lsTo2members = objODBCDatareader["To2members"].ToString();
                                        lscreated_date = objODBCDatareader["created_date"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                    sToken = "";
                                    int Length = 100;
                                    for (int j = 0; j < Length; j++)
                                    {
                                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                        sToken += sTempChars;
                                    }

                                    k = k + 1;
                                    lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                    lsTo2members.Replace(",,", ",");

                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                    lscc2members = lscc2members.Replace(employee_gid, "");
                                    lscc2members.Replace(",,", ",");

                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                           " from atm_trn_tauditcreation a  " +
                                           " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                           " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                        lsquery_title = objODBCDatareader["query_title"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                    " where employee_gid = '" + employee_gid + "'";
                                    string employee_name = objdbconn.GetExecuteScalar(msSQL);



                                    sub = " RE: Query Response  ";
                                    body = "Dear All,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to Response The Query.";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "Thanks & Regards, ";
                                    body = body + "<br />";
                                    body = body + HttpUtility.HtmlEncode(employee_name);
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                    MailMessage message = new MailMessage();
                                    SmtpClient smtp = new SmtpClient();
                                    message.From = new MailAddress(ls_username);
                                    //message.To.Add(new MailAddress(lsto_mail));


                                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                    {
                                        lsBCCReceipients = lsBccmail_id.Split(',');
                                        if (lsBccmail_id.Length == 0)
                                        {
                                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                                        }
                                        else
                                        {
                                            foreach (string BCCEmail in lsBCCReceipients)
                                            {
                                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                            }
                                        }
                                    }
                                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                    {
                                        lsToReceipients = lsto_mail.Split(',');
                                        if (lsto_mail.Length == 0)
                                        {
                                            message.To.Add(new MailAddress(lsto_mail));
                                        }
                                        else
                                        {
                                            foreach (string ToEmail in lsToReceipients)
                                            {
                                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
                                            }
                                        }
                                    }

                                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                    {
                                        lsCCReceipients = cc_mailid.Split(',');
                                        if (cc_mailid.Length == 0)
                                        {
                                            message.CC.Add(new MailAddress(cc_mailid));
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
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Send(message);

                                    values.status = true;
                                    if (values.status == true)
                                    {
                                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                                           " auditcreation_gid," +
                                           " sampleimport_gid," +
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + values.sampleimport_gid + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Response Received'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                                else
                                {

                                    msSQL = " select auditorcheckerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                    lsauditorcheckerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                                    if (lsauditorcheckerapprover_flag == "Y")
                                    {
                                        k = 1;
                                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            ls_server = objODBCDatareader["pop_server"].ToString();
                                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                        }
                                        objODBCDatareader.Close();
                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                                " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["created_by"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
                                            lsTo2members = objODBCDatareader["To2members"].ToString();
                                            lscreated_date = objODBCDatareader["created_date"].ToString();
                                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                        sToken = "";
                                        int Length = 100;
                                        for (int j = 0; j < Length; j++)
                                        {
                                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                            sToken += sTempChars;
                                        }

                                        k = k + 1;
                                        lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                        lsTo2members.Replace(",,", ",");

                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        lscc2members = lscc2members.Replace(employee_gid, "");
                                        lscc2members.Replace(",,", ",");

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                            lsquery_title = objODBCDatareader["query_title"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                        " where employee_gid = '" + employee_gid + "'";
                                        string employee_name = objdbconn.GetExecuteScalar(msSQL);



                                        sub = " RE: Query Response  ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Response The Query.";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employee_name);
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                        MailMessage message = new MailMessage();
                                        SmtpClient smtp = new SmtpClient();
                                        message.From = new MailAddress(ls_username);
                                        //message.To.Add(new MailAddress(lsto_mail));


                                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                        if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                        {
                                            lsBCCReceipients = lsBccmail_id.Split(',');
                                            if (lsBccmail_id.Length == 0)
                                            {
                                                message.Bcc.Add(new MailAddress(lsBccmail_id));
                                            }
                                            else
                                            {
                                                foreach (string BCCEmail in lsBCCReceipients)
                                                {
                                                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                                }
                                            }
                                        }
                                        if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                        {
                                            lsToReceipients = lsto_mail.Split(',');
                                            if (lsto_mail.Length == 0)
                                            {
                                                message.To.Add(new MailAddress(lsto_mail));
                                            }
                                            else
                                            {
                                                foreach (string ToEmail in lsToReceipients)
                                                {
                                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
                                                }
                                            }
                                        }

                                        if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                        {
                                            lsCCReceipients = cc_mailid.Split(',');
                                            if (cc_mailid.Length == 0)
                                            {
                                                message.CC.Add(new MailAddress(cc_mailid));
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
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Send(message);

                                        values.status = true;
                                        if (values.status == true)
                                        {
                                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                                               " auditcreation_gid," +
                                               " sampleimport_gid," +
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + values.sampleimport_gid + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Response Received'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }

                                    else
                                    {

                                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            ls_server = objODBCDatareader["pop_server"].ToString();
                                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                        }
                                        objODBCDatareader.Close();
                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                                " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["created_by"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
                                            lsTo2members = objODBCDatareader["To2members"].ToString();
                                            lscreated_date = objODBCDatareader["created_date"].ToString();
                                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                        sToken = "";
                                        int Length = 100;
                                        for (int j = 0; j < Length; j++)
                                        {
                                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                            sToken += sTempChars;
                                        }

                                        k = k + 1;
                                        lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                        lsTo2members.Replace(",,", ",");

                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        lscc2members = lscc2members.Replace(employee_gid, "");
                                        lscc2members.Replace(",,", ",");

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                            lsquery_title = objODBCDatareader["query_title"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                        " where employee_gid = '" + employee_gid + "'";
                                        string employee_name = objdbconn.GetExecuteScalar(msSQL);



                                        sub = " RE: Query Response  ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Response The Query.";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employee_name);
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                        MailMessage message = new MailMessage();
                                        SmtpClient smtp = new SmtpClient();
                                        message.From = new MailAddress(ls_username);
                                        //message.To.Add(new MailAddress(lsto_mail));


                                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                        if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                        {
                                            lsBCCReceipients = lsBccmail_id.Split(',');
                                            if (lsBccmail_id.Length == 0)
                                            {
                                                message.Bcc.Add(new MailAddress(lsBccmail_id));
                                            }
                                            else
                                            {
                                                foreach (string BCCEmail in lsBCCReceipients)
                                                {
                                                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                                }
                                            }
                                        }
                                        if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                        {
                                            lsToReceipients = lsto_mail.Split(',');
                                            if (lsto_mail.Length == 0)
                                            {
                                                message.To.Add(new MailAddress(lsto_mail));
                                            }
                                            else
                                            {
                                                foreach (string ToEmail in lsToReceipients)
                                                {
                                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
                                                }
                                            }
                                        }

                                        if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                        {
                                            lsCCReceipients = cc_mailid.Split(',');
                                            if (cc_mailid.Length == 0)
                                            {
                                                message.CC.Add(new MailAddress(cc_mailid));
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
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Send(message);

                                        values.status = true;
                                        if (values.status == true)
                                        {
                                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                                               " auditcreation_gid," +
                                               " sampleimport_gid," +
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + values.sampleimport_gid + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Response Received'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                    }
                                }
                            }
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
                    try
                    {
                        k = 1;


                        msSQL = " select  auditcreation_gid,employee_gid,created_by, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                            lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        }
                        objODBCDatareader.Close();
                        if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditapprover_gid && lsauditapprover_gid == lsauditmaker_gid)

                        {

                            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                            }
                            objODBCDatareader.Close();

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid,d.auditeemaker_gid,d.auditeechecker_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                    " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["created_by"].ToString();
                                lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                lsTo2members = objODBCDatareader["auditeechecker_gid"].ToString();
                                lscreated_date = objODBCDatareader["created_date"].ToString();
                                lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                            }
                            objODBCDatareader.Close();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }



                            k = k + 1;

                            lsTo2members = lsTo2members.Replace(employee_gid, " ");
                            lsTo2members.Replace(",,", ",");

                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                            lscc2members = lscc2members.Replace(employee_gid, "");
                            lscc2members.Replace(",,", ",");

                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                   " from atm_trn_tauditcreation a  " +
                                   " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                   " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                lsquery_title = objODBCDatareader["query_title"].ToString();

                            }
                            objODBCDatareader.Close();

                            sub = " RE: Query Response  ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                            body = body + "<br />";
                            //body = body + "Kindly log into systems to Approve the Audit.";
                            //body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employee_name);
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(lsto_mail));


                            lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }
                            if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                            {
                                lsToReceipients = lsto_mail.Split(',');
                                if (lsto_mail.Length == 0)
                                {
                                    message.To.Add(new MailAddress(lsto_mail));
                                }
                                else
                                {
                                    foreach (string ToEmail in lsToReceipients)
                                    {
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                    }
                                }
                            }

                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
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
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);

                            values.status = true;
                            if (values.status == true)
                            {
                                msSQL = "Insert into atm_trn_tauditmailcount( " +
                                   " auditcreation_gid," +
                                   " sampleimport_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + values.sampleimport_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Response Received'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }
                        else
                        {

                            msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                            if (lsauditormakerchecker_flag == "Y")


                            {

                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                    ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                }
                                objODBCDatareader.Close();

                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid,d.auditeemaker_gid,d.auditeechecker_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                 " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                    " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                    lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lscreated_by = objODBCDatareader["created_by"].ToString();
                                    lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                    lsTo2members = objODBCDatareader["auditeechecker_gid"].ToString();
                                    lscreated_date = objODBCDatareader["created_date"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                }
                                objODBCDatareader.Close();

                                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                sToken = "";
                                int Length = 100;
                                for (int j = 0; j < Length; j++)
                                {
                                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                    sToken += sTempChars;
                                }



                                k = k + 1;

                                lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                lsTo2members.Replace(",,", ",");

                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                lscc2members = lscc2members.Replace(employee_gid, "");
                                lscc2members.Replace(",,", ",");

                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                    " where employee_gid = '" + employee_gid + "'";
                                string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                       " from atm_trn_tauditcreation a  " +
                                       " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                    lsquery_title = objODBCDatareader["query_title"].ToString();

                                }
                                objODBCDatareader.Close();

                                sub = " RE: Query Response  ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                body = body + "<br />";
                                //body = body + "Kindly log into systems to Approve the Audit.";
                                //body = body + "<br />";
                                body = body + "<br />";
                                body = body + "Thanks & Regards, ";
                                body = body + "<br />";
                                body = body + HttpUtility.HtmlEncode(employee_name);
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                //message.To.Add(new MailAddress(lsto_mail));


                                lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                {
                                    lsBCCReceipients = lsBccmail_id.Split(',');
                                    if (lsBccmail_id.Length == 0)
                                    {
                                        message.Bcc.Add(new MailAddress(lsBccmail_id));
                                    }
                                    else
                                    {
                                        foreach (string BCCEmail in lsBCCReceipients)
                                        {
                                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                        }
                                    }
                                }
                                if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                {
                                    lsToReceipients = lsto_mail.Split(',');
                                    if (lsto_mail.Length == 0)
                                    {
                                        message.To.Add(new MailAddress(lsto_mail));
                                    }
                                    else
                                    {
                                        foreach (string ToEmail in lsToReceipients)
                                        {
                                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                        }
                                    }
                                }

                                if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                {
                                    lsCCReceipients = cc_mailid.Split(',');
                                    if (cc_mailid.Length == 0)
                                    {
                                        message.CC.Add(new MailAddress(cc_mailid));
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
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(message);

                                values.status = true;
                                if (values.status == true)
                                {
                                    msSQL = "Insert into atm_trn_tauditmailcount( " +
                                       " auditcreation_gid," +
                                       " sampleimport_gid," +
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + values.sampleimport_gid + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Response Received'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                            }
                            else
                            {

                                msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                                if (lsauditormakerapprover_flag == "Y")

                                {

                                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        ls_server = objODBCDatareader["pop_server"].ToString();
                                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid,d.auditeemaker_gid,d.auditeechecker_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                            " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lscreated_by = objODBCDatareader["created_by"].ToString();
                                        lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                        lsTo2members = objODBCDatareader["auditeechecker_gid"].ToString();
                                        lscreated_date = objODBCDatareader["created_date"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                    sToken = "";
                                    int Length = 100;
                                    for (int j = 0; j < Length; j++)
                                    {
                                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                        sToken += sTempChars;
                                    }



                                    k = k + 1;

                                    lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                    lsTo2members.Replace(",,", ",");

                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                    lscc2members = lscc2members.Replace(employee_gid, "");
                                    lscc2members.Replace(",,", ",");

                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                        " where employee_gid = '" + employee_gid + "'";
                                    string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                           " from atm_trn_tauditcreation a  " +
                                           " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                           " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                        lsquery_title = objODBCDatareader["query_title"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    sub = " RE: Query Response  ";
                                    body = "Dear All,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                    body = body + "<br />";
                                    //body = body + "Kindly log into systems to Approve the Audit.";
                                    //body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "Thanks & Regards, ";
                                    body = body + "<br />";
                                    body = body + employee_name;
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                    MailMessage message = new MailMessage();
                                    SmtpClient smtp = new SmtpClient();
                                    message.From = new MailAddress(ls_username);
                                    //message.To.Add(new MailAddress(lsto_mail));


                                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                    {
                                        lsBCCReceipients = lsBccmail_id.Split(',');
                                        if (lsBccmail_id.Length == 0)
                                        {
                                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                                        }
                                        else
                                        {
                                            foreach (string BCCEmail in lsBCCReceipients)
                                            {
                                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                            }
                                        }
                                    }
                                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                    {
                                        lsToReceipients = lsto_mail.Split(',');
                                        if (lsto_mail.Length == 0)
                                        {
                                            message.To.Add(new MailAddress(lsto_mail));
                                        }
                                        else
                                        {
                                            foreach (string ToEmail in lsToReceipients)
                                            {
                                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                            }
                                        }
                                    }

                                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                    {
                                        lsCCReceipients = cc_mailid.Split(',');
                                        if (cc_mailid.Length == 0)
                                        {
                                            message.CC.Add(new MailAddress(cc_mailid));
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
                                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.Send(message);

                                    values.status = true;
                                    if (values.status == true)
                                    {
                                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                                           " auditcreation_gid," +
                                           " sampleimport_gid," +
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + values.sampleimport_gid + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Response Received'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }

                                }
                                else
                                {
                                    msSQL = " select auditorcheckerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                    lsauditorcheckerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                                    if (lsauditorcheckerapprover_flag == "Y")

                                    {

                                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            ls_server = objODBCDatareader["pop_server"].ToString();
                                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                        }
                                        objODBCDatareader.Close();

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as To2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                       " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                               " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                               " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                           " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["created_by"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
                                            lsTo2members = objODBCDatareader["To2members"].ToString();
                                            lscreated_date = objODBCDatareader["created_date"].ToString();
                                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                        sToken = "";
                                        int Length = 100;
                                        for (int j = 0; j < Length; j++)
                                        {
                                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                            sToken += sTempChars;
                                        }



                                        k = k + 1;

                                        lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                        lsTo2members.Replace(",,", ",");

                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        lscc2members = lscc2members.Replace(employee_gid, "");
                                        lscc2members.Replace(",,", ",");

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                            " where employee_gid = '" + employee_gid + "'";
                                        string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                            lsquery_title = objODBCDatareader["query_title"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        sub = " RE: Query Response  ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Query Title :</b> " + lsquery_title + "<br />";
                                        body = body + "<br />";
                                        //body = body + "Kindly log into systems to Approve the Audit.";
                                        //body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employee_name);
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                        MailMessage message = new MailMessage();
                                        SmtpClient smtp = new SmtpClient();
                                        message.From = new MailAddress(ls_username);
                                        //message.To.Add(new MailAddress(lsto_mail));


                                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                        if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                        {
                                            lsBCCReceipients = lsBccmail_id.Split(',');
                                            if (lsBccmail_id.Length == 0)
                                            {
                                                message.Bcc.Add(new MailAddress(lsBccmail_id));
                                            }
                                            else
                                            {
                                                foreach (string BCCEmail in lsBCCReceipients)
                                                {
                                                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                                }
                                            }
                                        }
                                        if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                        {
                                            lsToReceipients = lsto_mail.Split(',');
                                            if (lsto_mail.Length == 0)
                                            {
                                                message.To.Add(new MailAddress(lsto_mail));
                                            }
                                            else
                                            {
                                                foreach (string ToEmail in lsToReceipients)
                                                {
                                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                                }
                                            }
                                        }

                                        if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                        {
                                            lsCCReceipients = cc_mailid.Split(',');
                                            if (cc_mailid.Length == 0)
                                            {
                                                message.CC.Add(new MailAddress(cc_mailid));
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
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Send(message);

                                        values.status = true;
                                        if (values.status == true)
                                        {
                                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                                               " auditcreation_gid," +
                                               " sampleimport_gid," +
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + values.sampleimport_gid + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Response Received'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }

                                    }

                                    else
                                    {

                                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            ls_server = objODBCDatareader["pop_server"].ToString();
                                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                        }
                                        objODBCDatareader.Close();

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as To2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                                " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["created_by"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
                                            lsTo2members = objODBCDatareader["To2members"].ToString();
                                            lscreated_date = objODBCDatareader["created_date"].ToString();
                                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();

                                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                        sToken = "";
                                        int Length = 100;
                                        for (int j = 0; j < Length; j++)
                                        {
                                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                            sToken += sTempChars;
                                        }



                                        k = k + 1;

                                        lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                        lsTo2members.Replace(",,", ",");

                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        lscc2members = lscc2members.Replace(employee_gid, "");
                                        lscc2members.Replace(",,", ",");

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                            " where employee_gid = '" + employee_gid + "'";
                                        string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select a.auditcreation_gid,b.sampleraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tsampleraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                            lsquery_title = objODBCDatareader["query_title"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        sub = " RE: Query Response  ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + "You have a response for the query in the audit. The details are as follows, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title)+ "<br />";
                                        body = body + "<br />";
                                        //body = body + "Kindly log into systems to Approve the Audit.";
                                        //body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employee_name);
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                        MailMessage message = new MailMessage();
                                        SmtpClient smtp = new SmtpClient();
                                        message.From = new MailAddress(ls_username);
                                        //message.To.Add(new MailAddress(lsto_mail));


                                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                                        if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                                        {
                                            lsBCCReceipients = lsBccmail_id.Split(',');
                                            if (lsBccmail_id.Length == 0)
                                            {
                                                message.Bcc.Add(new MailAddress(lsBccmail_id));
                                            }
                                            else
                                            {
                                                foreach (string BCCEmail in lsBCCReceipients)
                                                {
                                                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                                }
                                            }
                                        }
                                        if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                                        {
                                            lsToReceipients = lsto_mail.Split(',');
                                            if (lsto_mail.Length == 0)
                                            {
                                                message.To.Add(new MailAddress(lsto_mail));
                                            }
                                            else
                                            {
                                                foreach (string ToEmail in lsToReceipients)
                                                {
                                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                                                }
                                            }
                                        }

                                        if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                                        {
                                            lsCCReceipients = cc_mailid.Split(',');
                                            if (cc_mailid.Length == 0)
                                            {
                                                message.CC.Add(new MailAddress(cc_mailid));
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
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtp.Send(message);

                                        values.status = true;
                                        if (values.status == true)
                                        {
                                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                                               " auditcreation_gid," +
                                               " sampleimport_gid," +
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + values.sampleimport_gid + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Response Received'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                }
                            }
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
                values.message = "Error Occured..!";
                values.status = false;

            }

        }
        public bool DaGetSampleQuerydetaillist(string employee_gid, string sampleraisequery_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {

            msSQL = "select a.samplequeries2response_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date , a.document_name,a.document_path," +
                    " a.sampleresponse_gid, a.sampleimport_gid,a.replied_by ," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from atm_trn_tsamplequeries2response a " +
                    " left join hrm_mst_temployee b on a.sampleresponse_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.sampleraisequery_gid = '" + sampleraisequery_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSampleQuerydetaillist = new List<SampleQuerydetaillist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["sampleresponse_gid"].ToString() == employee_gid)
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
                    getSampleQuerydetaillist.Add(new SampleQuerydetaillist
                    {
                        samplequeries2response_gid = (dr_datarow["samplequeries2response_gid"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),            
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        session_user = lssession_user,
                        replied_by = (dr_datarow["replied_by"].ToString()),
                        document_attached = lssession_user + "/" + lsdocument_attached
                    });
                }
                values.SampleQuerydetaillist = getSampleQuerydetaillist;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public void DaResponseDocUpload(HttpRequest httpRequest, responsedoc_upload objfilename, string employee_gid, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string lsdocument_title = httpRequest.Form["document_title"];
            string sampleraisequery_gid = HttpContext.Current.Request.Params["sampleraisequery_gid"];
            string auditcreation_gid = HttpContext.Current.Request.Params["auditcreation_gid"];
            string sampleimport_gid = HttpContext.Current.Request.Params["sampleimport_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "AUDIT/ResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "AUDIT/ResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AUDIT/ResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "AUDIT/ResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("AUSQ");
                        msSQL = " insert into atm_trn_tsamplequeries2response(" +
                                " samplequeries2response_gid," +
                                " sampleraisequery_gid, " +
                                " auditcreation_gid," +
                                " sampleimport_gid," +
                                " sampleresponse_gid," +
                                " document_name ," +
                                " document_path," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + sampleraisequery_gid + "'," +
                                "'" + auditcreation_gid + "'," +
                                "'" + sampleimport_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName + "'," +
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

        public void DaGetTaggedSampleTask(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " b.employee_name as tag_user,group_concat(d.sampleimport_gid)as sampleimport_gid, a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,b.employee_gid as taguser_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
                    " left join atm_trn_tsampleimport d on d.auditcreation_gid = b.auditcreation_gid " +
                    " where (b.employee_gid='" + Employee_gid + "')  group by a.auditcreation_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        

        public void DaGetCompletedAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditdepartment_name,a.audittype_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(c.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join atm_trn_tauditapproval  c on a.auditcreation_gid = c.auditcreation_gid " +
                    " left join hrm_mst_temployee f on c.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid" +
                    " where g.auditeemaker_gid='" + Employee_gid + "' and a.status = 'Completed' group by g.auditcreation_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetClosedAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.auditdepartment_name,a.audit_name,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid" +
                    " where (g.auditeemaker_gid='" + Employee_gid + "') and (a.status = 'Closed') and (auditapproval_flag='Y') group by g.auditcreation_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetOpenAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditfrequency_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                   " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                   " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                   " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                   " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                   " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid and g.auditeemaker_gid='" + Employee_gid + "'" +
                   " where (g.auditeemaker_gid='" + Employee_gid + "') and (a.status not in ('Hold','Closed','Completed')) and (auditapproval_flag='Y') group by g.auditcreation_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetHoldAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.auditdepartment_name,a.audit_name,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid" +
                    " where (g.auditeemaker_gid='" + Employee_gid + "') and (a.status = 'Hold') and (auditapproval_flag='Y') group by g.auditcreation_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetCheckerCompletedAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join atm_trn_tauditapproval c on c.auditcreation_gid = a.auditcreation_gid" +
                    " left join hrm_mst_temployee f on c.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid" +
                    " where g.auditeechecker_gid='" + Employee_gid + "' and a.status = 'Completed' group by g.auditcreation_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetCheckerClosedAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditdepartment_name,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid" +
                    " where (g.auditeechecker_gid='" + Employee_gid + "') and (a.status = 'closed') and (a.auditapproval_flag='Y')" +
                    "  group by g.auditcreation_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetCheckerOpenAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditdepartment_name,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                   " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                   " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                   " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                   " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                   " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid and g.auditeechecker_gid='" + Employee_gid + "'" +
                   " where (g.auditeechecker_gid='" + Employee_gid + "') and (a.status not in ('Hold','Closed','Completed')) " +
                    " and ((a.auditormaker_approvalflag='N') " +
                    " and a.auditorchecker_approvalflag='N' and g.auditeechecker_approvalflag='N'and a.auditapproval_flag='Y' " +
                    " and a.approval_status not in ('Checker - Auditee Pending','Checker Approval pending')) group by g.auditcreation_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetCheckerHoldAuditee(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditdepartment_name,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,g.auditeemaker_gid,g.auditeechecker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_tchecklistmaster b on b.checklistmaster_gid = a.checklistmaster_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker g on g.auditcreation_gid = a.auditcreation_gid" +
                    " where (g.auditeechecker_gid='" + Employee_gid + "') and (a.status = 'Hold') and (a.auditapproval_flag='Y')" +
                    "  group by g.auditcreation_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetTaggedSampleChecker(MdlAtmTrnMyAuditTaskAuditee values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " b.employee_name as tag_user,group_concat(d.sampleimport_gid)as sampleimport_gid, a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,b.employee_gid as taguser_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
                    " left join atm_trn_tsampleimport d on d.auditcreation_gid = b.auditcreation_gid " +
                    " where (b.employee_gid='" + Employee_gid + "')  group by a.auditcreation_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyaudittaskauditee_list = new List<myaudittaskauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyaudittaskauditee_list.Add(new myaudittaskauditee_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.myaudittaskauditee_list = getmyaudittaskauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaPostTagUserAudit(MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {

            for (var i = 0; i < values.tagemployelist.Count; i++)
            {
                msGettaguser2audit_gid = objcmnfunctions.GetMasterGID("ARQT");
                msSQL = "Insert into atm_trn_ttaguser2audit( " +
                       " taguser2audit_gid, " +
                       " auditcreation_gid," +
                       " audit_name," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGettaguser2audit_gid + "'," +
                       "'" + values.auditcreation_gid + "'," +
                       "'" + values.audit_name + "'," +
                       "'" + values.tagemployelist[i].employee_gid + "'," +
                       "'" + values.tagemployelist[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Employee Tagged successfully";
            }
            else
            {
                values.message = "Error Occured while Tagging";
                values.status = false;
            }

        }

        public void DaGetTagUserAuditview(MdlAtmTrnMyAuditTaskAuditee values, string auditcreation_gid)
        {
            try
            {
                msSQL = " SELECT a.taguser2audit_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, group_concat(a.employee_name) as employee_name , a.auditcreation_gid, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_ttaguser2audit a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.auditcreation_gid ='" + auditcreation_gid + "'group by a.auditcreation_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaudittaguser_list = new List<audittaguser_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaudittaguser_list.Add(new audittaguser_list
                        {
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.audittaguser_list = getaudittaguser_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)

            {
                values.status = false;
            }
        }


        public void Daclosequery(MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("AQCL");

            msSQL = " insert into atm_trn_tauditqueriescloselog (" +
                      " auditquerycloselog_gid, " +
                      " auditcreation_gid," +
                      " closing_description," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditcreation_gid + "'," +
                      " '" + values.closing_description.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tauditqueries  set auditraisequery_status='Closed' where auditcreation_gid='" + values.auditcreation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Message Posted Successfully";
            }



            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void Daclosequerysummary(MdlAtmTrnMyAuditTaskAuditee values, string auditcreation_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.auditquerycloselog_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, a.closing_description, a.auditcreation_gid, d.auditraisequery_status, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by" +
                        " FROM atm_trn_tauditqueriescloselog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tauditqueries d on d.auditcreation_gid = a.auditcreation_gid " +
                        " where a.auditcreation_gid = '" + auditcreation_gid + "' and d.auditraisequery_status = 'Closed'";
                //" where a.auditcreation_gid ='" + values.auditcreation_gid + "' order by a.taguser2audit_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclosequery_list = new List<closequery_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclosequery_list.Add(new closequery_list
                        {
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            closing_description = (dr_datarow["closing_description"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            auditraisequery_status = (dr_datarow["auditraisequery_status"].ToString())
                        });
                    }
                    values.closequery_list = getclosequery_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = "select auditraisequery_status from atm_trn_tauditqueries " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";
                values.auditraisequery_status = objdbconn.GetExecuteScalar(msSQL);
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            catch (Exception ex)

            {
                values.status = false;
            }
        }



        public void DaGetEmployeeName(string auditcreation_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msSQL = " select distinct group_concat(employee_name) as employee_name  from atm_trn_ttaguser2audit " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaPostcloseSamplequery(sampleraiseclosequery_list values, string employee_gid)
        {
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    "where b.employee_gid ='" + employee_gid + "'";
            employeename = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update atm_trn_tsampleraisequery set  " +
                    " close_remarks='" + values.close_remarks.Replace("'", "") + "'," +
                    " raisequery_closedby='" + employeename + "'," +
                    " closed_by='" + employee_gid + "'," +
                    " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " raisequery_flag='N'," + 
                    " sampleraisequery_status='Closed'" +
                    " where sampleraisequery_gid='" + values.sampleraisequery_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Sample Query Closed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }


        public void Daclosequerysample(MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("ASQL");

            msSQL = " insert into atm_trn_tsamplequeriescloselog (" +
                      " samplequerycloselog_gid, " +
                      " auditcreation_gid," +
                      " sampleimport_gid," +
                      " closing_description," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditcreation_gid + "'," +
                      " '" + values.sampleimport_gid + "'," +
                      " '" + values.closing_description.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msSQL = " update atm_trn_tsamplequeries set samplequery_status='Closed' where sampleimport_gid='" + values.sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Message Posted Successfully";


                values.status = true;
                values.message = "Message Posted Successfully";
            }


            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }


        public void DaGetclosequeryAudit(MdlAtmTrnMyAuditTaskAuditee values, string auditcreation_gid)
        {

            msSQL = "select auditraisequery_status from atm_trn_tauditqueries " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditraisequery_status = objODBCDatareader["auditraisequery_status"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void Daclosesamplequerysummary(MdlAtmTrnMyAuditTaskAuditee values, string sampleimport_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.sampleimport_gid, a.samplequerycloselog_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, a.closing_description, a.auditcreation_gid, d.samplequery_status, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by" +
                        " FROM atm_trn_tsamplequeriescloselog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tsamplequeries d on d.sampleimport_gid = a.sampleimport_gid " +
                        " where d.sampleimport_gid = '" + sampleimport_gid + "' and d.samplequery_status = 'Closed'";
                //" where a.auditcreation_gid ='" + values.auditcreation_gid + "' order by a.taguser2audit_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclosequerysample_list = new List<closequerysample_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclosequerysample_list.Add(new closequerysample_list
                        {
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            closing_description = (dr_datarow["closing_description"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            samplequery_status = (dr_datarow["samplequery_status"].ToString())
                        });
                    }
                    values.closequerysample_list = getclosequerysample_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)

            {
                values.status = false;
            }
        }

        public void DaGetSampleclosequery(MdlAtmTrnMyAuditTaskAuditee values, string sampleimport_gid)
        {

            msSQL = "select samplequery_status from atm_trn_tsamplequeries " +
                  " where sampleimport_gid='" + sampleimport_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.samplequery_status = objODBCDatareader["samplequery_status"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostAuditeeCheckerApproval(auditeecheckerapproval values, string employee_gid)
        {

            msSQL = " select a.auditcreation_gid,a.employee_gid,a.auditeechecker_name,a.auditeechecker_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                         " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                         " FROM atm_trn_tauditcreation a" +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                          " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getauditeecheckerapproval_list = new List<auditeecheckerapproval_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var lsinitiated_gid = dt["employee_gid"].ToString();
                    var lsauditeechecker_gid = dt["auditeechecker_gid"].ToString();
                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                    var lsapproval_name = dt["auditeechecker_name"].ToString();
                    var lsinitiated_name = dt["auditmaker_name"].ToString();

                    msGetGid = objcmnfunctions.GetMasterGID("OBAP");

                    msSQL = "Insert into atm_trn_tcheckerapproval( " +
                            " checkerapproval_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                           " auditchecker_gid," +
                           " approval_name," +
                           " initiateapproval," +
                           " approval_status," +
                           " approval_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                             "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                           "'" + lsauditeechecker_gid + "'," +
                           "'" + lsapproval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "' AuditeeChecker Approved'," +
                           "'" + values.auditeeapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {

                    msSQL = "update atm_trn_tauditcreation set approval_flag ='Y'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update atm_trn_tauditcreation set approval_status ='Checker Approved'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update atm_trn_tauditcreation set auditeeapproval_status ='AuditeeChecker Approved'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Auditee Checker Approved  Successfully ..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
                dt_datatable.Dispose();
            }
        }

        public void DaPostAuditeeCheckerRejected(auditeecheckerapproval values, string employee_gid)
        {

            msSQL = " select a.auditcreation_gid,a.employee_gid,a.auditeechecker_name,a.auditeechecker_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                          " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                          " FROM atm_trn_tauditcreation a" +
                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                           " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getauditeecheckerapproval_list = new List<auditeecheckerapproval_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var lsinitiated_gid = dt["employee_gid"].ToString();
                    var lsauditeechecker_gid = dt["auditeechecker_gid"].ToString();
                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                    var lsapproval_name = dt["auditeechecker_name"].ToString();
                    var lsinitiated_name = dt["auditmaker_name"].ToString();

                    msGetGid = objcmnfunctions.GetMasterGID("OBAP");

                    msSQL = "Insert into atm_trn_tcheckerapproval( " +
                            " checkerapproval_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                           " auditchecker_gid," +
                           " approval_name," +
                           " initiateapproval," +
                           " approval_status," +
                           " approval_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                             "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                           "'" + lsauditeechecker_gid + "'," +
                           "'" + lsapproval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "' Auditee Checker Rejected'," +
                           "'" + values.auditeeapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                if (mnResult != 0)
                {
                    msSQL = "update atm_trn_tauditcreation set approval_flag ='N'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update atm_trn_tauditcreation set status_flag ='N'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update atm_trn_tauditcreation set approval_status ='Auditee Checker Rejected'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "  Auditee Checker Rejected  ..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
                dt_datatable.Dispose();
            }
        }
        public void DaAuditeeMakerView(string auditcreation_gid,MdlAtmTrnAuditorMaker values, string Employee_gid)
        {

            try
            {
                msSQL = " select a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                      " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                      " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.capture_score, a.total_score, " +
                      " a.partial_score, a.na_score, a.capture_field, a.yes_disposition, a.no_disposition, a.partial_disposition, " +
                      " a.na_disposition from atm_trn_tauditcreation2checklist a " +
                      " left join atm_trn_tauditagainstmultipleauditeechecker b on b.checkpointgroupadd_gid = a.checkpointgroupadd_gid and b.auditcreation_gid = a.auditcreation_gid" +
                      " where b.auditeemaker_gid='" + Employee_gid + "'and b.auditcreation_gid='" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                        {
                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                            checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                            noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                            yes_score = (dr_datarow["yes_score"].ToString()),
                            no_score = (dr_datarow["no_score"].ToString()),
                            partial_score = (dr_datarow["partial_score"].ToString()),
                            na_score = (dr_datarow["na_score"].ToString()),
                            capture_score = (dr_datarow["capture_score"].ToString()),
                            yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                            no_disposition = (dr_datarow["no_disposition"].ToString()),
                            partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                            na_disposition = (dr_datarow["na_disposition"].ToString()),
                            capture_field = (dr_datarow["capture_field"].ToString()),
                            total_score = (dr_datarow["total_score"].ToString()),
                        });
                    }
                    values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaAuditeeSampleMakerView(string sampleimport_gid, string auditcreation_gid,MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            try
            {
                msSQL = " select observationscoresample_gid from atm_trn_tobservationscoresample where " +
                         " sampleimport_gid='" + sampleimport_gid + "'";
                values.sampleimport_gid = objdbconn.GetExecuteScalar(msSQL);
                if (values.sampleimport_gid == "")
                {
                    msSQL = " select a.observationscoresample_gid,a.sampleimport_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                          " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                          " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
                          " a.partial_score, a.na_score, a.samplecapture_field, a.yes_disposition,a.sampleoverall_score, a.no_disposition, a.partial_disposition, " +
                          " a.na_disposition from atm_trn_tobservationscoresample a " +
                      " left join atm_trn_tauditagainstmultipleauditeechecker b on b.checkpointgroupadd_gid = a.checkpointgroupadd_gid and b.auditcreation_gid = a.auditcreation_gid" +
                        " where b.auditeemaker_gid='" + Employee_gid + "' and  b.auditcreation_gid='" + auditcreation_gid + "' and a.sampleimport_gid is null ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                            {
                                observationscoresample_gid = (dr_datarow["observationscoresample_gid"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                                audittype_name = (dr_datarow["audittype_name"].ToString()),
                                checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                                audit_name = (dr_datarow["audit_name"].ToString()),
                                checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                                checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                                riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                                positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                                noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                                yes_score = (dr_datarow["yes_score"].ToString()),
                                no_score = (dr_datarow["no_score"].ToString()),
                                partial_score = (dr_datarow["partial_score"].ToString()),
                                na_score = (dr_datarow["na_score"].ToString()),
                                capture_score = (dr_datarow["samplecapture_score"].ToString()),
                                yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                                no_disposition = (dr_datarow["no_disposition"].ToString()),
                                partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                                na_disposition = (dr_datarow["na_disposition"].ToString()),
                                capture_field = (dr_datarow["samplecapture_field"].ToString()),
                                total_score = (dr_datarow["total_score"].ToString()),
                                sampleoverall_score = (dr_datarow["sampleoverall_score"].ToString()),


                            });
                        }
                        values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                    }
                    dt_datatable.Dispose();
                    //msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                    //values.total_score = objdbconn.GetExecuteScalar(msSQL);
                    //values.status = true;
                }

                else
                {
                    msSQL = " select a.observationscoresample_gid,a.sampleimport_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                            " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                                             " a.riskcategory_name, a.positiveconfirmity_name,samplechecklistverified_flag, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
                                             " a.partial_score, a.na_score, a.samplecapture_field, a.yes_disposition,a.sampleoverall_score, a.no_disposition, a.partial_disposition, " +
                                             " a.na_disposition from atm_trn_tobservationscoresample a " +
                                             " left join atm_trn_tauditagainstmultipleauditeechecker b on b.checkpointgroupadd_gid = a.checkpointgroupadd_gid and b.auditcreation_gid = a.auditcreation_gid" +
                                            " where b.auditeemaker_gid='" + Employee_gid + "' and  b.auditcreation_gid='" + auditcreation_gid + "' and a.sampleimport_gid='" + sampleimport_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                            {
                                observationscoresample_gid = (dr_datarow["observationscoresample_gid"].ToString()),
                                auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                                audittype_name = (dr_datarow["audittype_name"].ToString()),
                                checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                                audit_name = (dr_datarow["audit_name"].ToString()),
                                checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                                checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                                riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                                positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                                noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                                yes_score = (dr_datarow["yes_score"].ToString()),
                                no_score = (dr_datarow["no_score"].ToString()),
                                partial_score = (dr_datarow["partial_score"].ToString()),
                                na_score = (dr_datarow["na_score"].ToString()),
                                capture_score = (dr_datarow["samplecapture_score"].ToString()),
                                yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                                no_disposition = (dr_datarow["no_disposition"].ToString()),
                                partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                                na_disposition = (dr_datarow["na_disposition"].ToString()),
                                capture_field = (dr_datarow["samplecapture_field"].ToString()),
                                total_score = (dr_datarow["total_score"].ToString()),
                                sampleoverall_score = (dr_datarow["sampleoverall_score"].ToString()),
                                samplechecklistverified_flag = (dr_datarow["samplechecklistverified_flag"].ToString()),

                            });
                        }
                        values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                    }
                    dt_datatable.Dispose();
                    //msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                    //values.total_score = objdbconn.GetExecuteScalar(msSQL);
                    //values.status = true;
                }
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaAuditeeCheckerView(string auditcreation_gid,MdlAtmTrnAuditorMaker values, string Employee_gid)
        {

            try
            {
                msSQL = " select a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                      " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                      " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.capture_score, a.total_score, " +
                      " a.partial_score, a.na_score, a.capture_field, a.yes_disposition, a.no_disposition, a.partial_disposition, " +
                      " a.na_disposition from atm_trn_tauditcreation2checklist a " +
                      " left join atm_trn_tauditagainstmultipleauditeechecker b on b.checkpointgroupadd_gid = a.checkpointgroupadd_gid and b.auditcreation_gid = a.auditcreation_gid" +
                      " where b.auditeechecker_gid='" + Employee_gid + "'and b.auditcreation_gid='" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                        {
                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                            checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                            noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                            yes_score = (dr_datarow["yes_score"].ToString()),
                            no_score = (dr_datarow["no_score"].ToString()),
                            partial_score = (dr_datarow["partial_score"].ToString()),
                            na_score = (dr_datarow["na_score"].ToString()),
                            capture_score = (dr_datarow["capture_score"].ToString()),
                            yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                            no_disposition = (dr_datarow["no_disposition"].ToString()),
                            partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                            na_disposition = (dr_datarow["na_disposition"].ToString()),
                            capture_field = (dr_datarow["capture_field"].ToString()),
                            total_score = (dr_datarow["total_score"].ToString()),
                        });
                    }
                    values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaAuditeeSampleCheckerView(string sampleimport_gid, string auditcreation_gid, MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            try
            {
                msSQL = " select observationscoresample_gid from atm_trn_tobservationscoresample where " +
                         " sampleimport_gid='" + sampleimport_gid + "'";
                values.sampleimport_gid = objdbconn.GetExecuteScalar(msSQL);
                if (values.sampleimport_gid == "")
                {
                    msSQL = " select a.observationscoresample_gid,a.sampleimport_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                          " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                          " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
                          " a.partial_score, a.na_score, a.samplecapture_field, a.yes_disposition,a.sampleoverall_score, a.no_disposition, a.partial_disposition, " +
                          " a.na_disposition from atm_trn_tobservationscoresample a " +
                      " left join atm_trn_tauditagainstmultipleauditeechecker b on b.checkpointgroupadd_gid = a.checkpointgroupadd_gid and b.auditcreation_gid = a.auditcreation_gid" +
                        " where b.auditeechecker_gid='" + Employee_gid + "' and  b.auditcreation_gid='" + auditcreation_gid + "' and a.sampleimport_gid is null ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                            {
                                observationscoresample_gid = (dr_datarow["observationscoresample_gid"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                                audittype_name = (dr_datarow["audittype_name"].ToString()),
                                checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                                audit_name = (dr_datarow["audit_name"].ToString()),
                                checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                                checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                                riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                                positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                                noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                                yes_score = (dr_datarow["yes_score"].ToString()),
                                no_score = (dr_datarow["no_score"].ToString()),
                                partial_score = (dr_datarow["partial_score"].ToString()),
                                na_score = (dr_datarow["na_score"].ToString()),
                                capture_score = (dr_datarow["samplecapture_score"].ToString()),
                                yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                                no_disposition = (dr_datarow["no_disposition"].ToString()),
                                partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                                na_disposition = (dr_datarow["na_disposition"].ToString()),
                                capture_field = (dr_datarow["samplecapture_field"].ToString()),
                                total_score = (dr_datarow["total_score"].ToString()),
                                sampleoverall_score = (dr_datarow["sampleoverall_score"].ToString()),


                            });
                        }
                        values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                    }
                    dt_datatable.Dispose();
                    //msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                    //values.total_score = objdbconn.GetExecuteScalar(msSQL);
                    //values.status = true;
                }

                else
                {
                    msSQL = " select a.observationscoresample_gid,a.sampleimport_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                            " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                                             " a.riskcategory_name, a.positiveconfirmity_name,samplechecklistverified_flag, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
                                             " a.partial_score, a.na_score, a.samplecapture_field, a.yes_disposition,a.sampleoverall_score, a.no_disposition, a.partial_disposition, " +
                                             " a.na_disposition from atm_trn_tobservationscoresample a " +
                                             " left join atm_trn_tauditagainstmultipleauditeechecker b on b.checkpointgroupadd_gid = a.checkpointgroupadd_gid and b.auditcreation_gid = a.auditcreation_gid" +
                                            " where b.auditeechecker_gid='" + Employee_gid + "' and  b.auditcreation_gid='" + auditcreation_gid + "' and a.sampleimport_gid='" + sampleimport_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                            {
                                observationscoresample_gid = (dr_datarow["observationscoresample_gid"].ToString()),
                                auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                                audittype_name = (dr_datarow["audittype_name"].ToString()),
                                checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
                                audit_name = (dr_datarow["audit_name"].ToString()),
                                checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
                                checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
                                riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
                                positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
                                noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
                                yes_score = (dr_datarow["yes_score"].ToString()),
                                no_score = (dr_datarow["no_score"].ToString()),
                                partial_score = (dr_datarow["partial_score"].ToString()),
                                na_score = (dr_datarow["na_score"].ToString()),
                                capture_score = (dr_datarow["samplecapture_score"].ToString()),
                                yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                                no_disposition = (dr_datarow["no_disposition"].ToString()),
                                partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                                na_disposition = (dr_datarow["na_disposition"].ToString()),
                                capture_field = (dr_datarow["samplecapture_field"].ToString()),
                                total_score = (dr_datarow["total_score"].ToString()),
                                sampleoverall_score = (dr_datarow["sampleoverall_score"].ToString()),
                                samplechecklistverified_flag = (dr_datarow["samplechecklistverified_flag"].ToString()),

                            });
                        }
                        values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                    }
                    dt_datatable.Dispose();
                    //msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                    //values.total_score = objdbconn.GetExecuteScalar(msSQL);
                    //values.status = true;
                }
            }
            catch
            {
                values.status = false;
            }
        }
    }
}
