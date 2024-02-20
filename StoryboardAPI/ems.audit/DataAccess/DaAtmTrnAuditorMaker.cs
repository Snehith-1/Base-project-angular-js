using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web;
using ems.storage.Functions;
using System.Web.Http.Results;


namespace ems.audit.DataAccess
{
    public class DaAtmTrnAuditorMaker
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        Fnazurestorage objcmnstorage = new Fnazurestorage();

        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, count, lspath, samplecount, lsinitiated_gid, samplerecord, lsauditdepartment_value, lsobservation_percentage, lscapture_yesscore, lscapture_noscore, lscapture_partialscore, lscapture_nascore, lscapture_totalscore, msGetaudituniqueno, lsdue_date, lsreport_date, lsperiodfrom_date, lsauditperiod_to, lsauditname_value;
        string lsauditor, lschecker_gid, lsmaker_gid, lsapprover_gid, lsapproval_name, lsauditapproval_gid, lsauditorcommonname_flag;
        int mnResult, k, lstotal_amount, lsoverall_score;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsauditormakerchecker_flag,lsauditeemaker_gid, lsquery_title, lsdocument_attached,lssession_user, lsTo2members, lsauditeechecker_approvalflag, lsmultiauditeechecker_approvalflag, lsauditee_flag, lsauditcreation2checklist_gid, lsauditormakerapprover_flag, lsauditmaker_gid, lsauditorcheckerapprover_flag, lsauditchecker_gid, lsauditapprover_gid, lsto2members, lsauditorchecker_name, lsauditeechecker_gid, lsauditmapping_gid, lsemployee_gid, lsauditormaker_name, lsBccmail_id, lscreated_by, lstomembers, lscc2members, lsauditcreation_gid, lscreated_date, frommail_id, lscc_mail, strBCC, lsbcc_mail, lsaudit_name, lsaaudit_uniqueno, lsaudit_description, lsauditdepartment_name, lsaudittype_name, lscheckpointgroup_name;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;


        public void DaGetAuditorMaker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                //b.employee_name as tag_user,b.employee_gid as taguser_gid,
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.auditapproval_flag,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join atm_trn_ttaguser2audit d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " where (a.employee_gid='" + Employee_gid + "'or d.employee_gid='" + Employee_gid + "' )  order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditmaker_gid = (dr_datarow["auditmaker_gid"].ToString()),
                            auditchecker_gid = (dr_datarow["auditchecker_gid"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            auditapprover_gid = (dr_datarow["auditapprover_gid"].ToString()),
                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetAuditorCheckerSummary(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditee_visible,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping2employee_gid as auditapprover_gid,auditorcheckersample_flag,a.auditmapping_gid as auditchecker_gid,date_format(e.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join atm_trn_tmakerinitiateapproval e on a.auditcreation_gid = e. auditcreation_gid " +
                    " left join hrm_mst_temployee b on e.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join atm_trn_ttaguser2audit d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " where ( a.auditmapping_gid ='" + Employee_gid + "') and auditormaker_approvalflag='Y' and " +
                    " (auditorchecker_approvalflag='N' or auditorchecker_approvalflag='Y') and a.status not in ('Hold','Closed','Completed') " +
                    " order by a.auditcreation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditmaker_gid = (dr_datarow["auditmaker_gid"].ToString()),
                            auditchecker_gid = (dr_datarow["auditchecker_gid"].ToString()),
                            auditapprover_gid = (dr_datarow["auditapprover_gid"].ToString()),
                            auditorcheckersample_flag = (dr_datarow["auditorcheckersample_flag"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAuditorApproverSummary(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,e.multiauditeechecker_approvalflag,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid,a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join atm_trn_ttaguser2audit d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " left join atm_trn_tauditagainstmultipleauditeechecker e on e.auditcreation_gid = a.auditcreation_gid " +
                    " where ( a.auditmapping2employee_gid ='" + Employee_gid + "' and e.multiauditeechecker_approvalflag ='Y' " +
                    " and a.auditorapprover_approvalflag='N' and a.approval_status ='Final Approval Pending') order by a.auditcreation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditapprover_name = (dr_datarow["auditapprover_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditmaker_gid = (dr_datarow["auditmaker_gid"].ToString()),
                            auditchecker_gid = (dr_datarow["auditchecker_gid"].ToString()),
                            auditapprover_gid = (dr_datarow["auditapprover_gid"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetAuditorMakerSummary(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name,a.auditchecker_name,a.auditapprover_name, " +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " left join atm_trn_ttaguser2audit d on  a.auditcreation_gid = d. auditcreation_gid" +
                    " where (d.employee_gid='" + Employee_gid + "') order by a.created_by desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditmaker_gid = (dr_datarow["auditmaker_gid"].ToString()),
                            auditchecker_gid = (dr_datarow["auditchecker_gid"].ToString()),
                            auditapprover_gid = (dr_datarow["auditapprover_gid"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }




        public void DaAuditorMakerView(string auditcreation_gid, MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            try
            {
                msSQL = " select a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                      " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                      " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.capture_score, a.total_score, " +
                      " a.partial_score, a.na_score, a.capture_field, a.yes_disposition, a.no_disposition, a.partial_disposition, " +
                      " a.na_disposition from atm_trn_tauditcreation2checklist a " +
                      " where a.auditcreation_gid='" + auditcreation_gid + "'";
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
                msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                values.total_score = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAuditorMakerViewOverallscore(string auditcreation_gid, MdlAtmTrnAuditorMaker values, string employee_gid)
        {


            msSQL = " select sum(yes_score) as overall_score from atm_trn_tauditcreation2checklist " +
                "  where auditcreation_gid ='" + auditcreation_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.overall_score = objODBCDatareader["overall_score"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = " update atm_trn_tauditcreation set overall_score='" + values.overall_score + "' " +
                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaGetAuditorMakerobservationscore(string auditcreation_gid, MdlAtmTrnAuditorMaker values, string employee_gid)
        {


            msSQL = " select observation_percentage from atm_trn_tauditcreation2checklist " +
                "  where auditcreation_gid ='" + auditcreation_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.observation_percentage = objODBCDatareader["observation_percentage"].ToString();

            }
            objODBCDatareader.Close();

        }


        public void DaEditAuditorMaker(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
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


        public void DaGetAuditorMakerCheckpointObservation(MdlAtmTrnAuditorMaker values)
        {
            try
            {
                msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_by desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakercheckpointobservation_list = new List<makercheckpointobservation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakercheckpointobservation_list.Add(new makercheckpointobservation_list
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
                    values.makercheckpointobservation_list = getmakercheckpointobservation_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaAuditorMakerCheckpointObservationView(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
        {

            msSQL = "select auditcreation_gid from atm_trn_tsamplequeries where samplequery_status ='Open' and auditcreation_gid ='" + auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)

            {

                msSQL = " select a.auditcreation2checklist_gid,b.checklistmasteradd_gid,a.auditcreation_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description," +
                              " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score, a.partial_score, a.na_score, b.capture_score" +
                              " from atm_trn_tauditcreation2checklist a " +
                              " left join atm_trn_tcheckpointobservation b on a.auditcreation2checklist_gid=b.auditcreation2checklist_gid " +
                                " where a.auditcreation_gid='" + auditcreation_gid + "'  group by auditcreation_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakercheckpointobservationview_list = new List<makercheckpointobservationview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakercheckpointobservationview_list.Add(new makercheckpointobservationview_list
                        {
                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
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
                    values.makercheckpointobservationview_list = getmakercheckpointobservationview_list;
                }
                dt_datatable.Dispose();

                values.status = true;

                msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                values.total_score = objdbconn.GetExecuteScalar(msSQL);
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            {
                values.message = " Please Update the Sample Query  ";
                values.status = false;
            }

        }
        public void DaEditAuditorMakerCheckpointObservation(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
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


        public void DaPostAuditorMakerObservationTotalAmount(makercheckpointobservationadd values, string employee_gid)
        {
            msSQL = "select checklist2checkpoint,auditcreation_gid,checklist_name,overall_detail,checklistverified_flag,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                  " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "'and auditcreation_gid is null";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count == 0)
            {
                string lsyes_score = "", lsno_score = "", lspartial_score = "", lsna_score = "";
                msSQL = " select auditcreation2checklist_gid,yes_score,no_score,partial_score,na_score from atm_trn_tauditcreation2checklist " +
                           " where auditcreation2checklist_gid = '" + values.auditcreation2checklist_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsyes_score = objODBCDatareader["yes_score"].ToString();
                    lsno_score = objODBCDatareader["no_score"].ToString();
                    lspartial_score = objODBCDatareader["partial_score"].ToString();
                    lsna_score = objODBCDatareader["na_score"].ToString();

                }
                objODBCDatareader.Close();


                string getscore = values.capture_score == "Yes" ? lsyes_score : values.capture_score == "No" ? lsno_score : values.capture_score == "Partial" ? lspartial_score : lsna_score;

                msSQL = " update atm_trn_tauditcreation2checklist set capture_field='" + values.capture_score + "', capture_score='" + getscore + "' " +
                       " where auditcreation2checklist_gid = '" + values.auditcreation2checklist_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("OTAL");
                    msSQL = " insert into atm_trn_tobservationtotalamountlog(" +
                              " observationtotalamountlog_gid ," +
                              " auditcreation2checklist_gid," +
                              " auditcreation_gid," +
                               " observationtotalamount_gid," +
                              " checklistmasteradd_gid," +
                              " capture_score, " +
                              " capture_field, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                               "'" + msGetGid + "'," +
                               "'" + values.auditcreation2checklist_gid + "', " +
                               "'" + values.auditcreation_gid + "', " +
                               "'" + values.observationtotalamount_gid + "', " +
                               "'" + values.checklistmasteradd_gid + "', " +
                               "'" + getscore + "'," +
                               "'" + values.capture_score + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + values.auditcreation_gid + "'";
                    values.total_amount = objdbconn.GetExecuteScalar(msSQL);

                    var convertDecimal1 = Convert.ToDecimal(values.total_amount);

                    Decimal val1 = Decimal.Truncate(convertDecimal1);

                    msSQL = " select sum(yes_score)as overall_score from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + values.auditcreation_gid + "'";
                    values.overall_score = objdbconn.GetExecuteScalar(msSQL);

                    var convertDecimal2 = Convert.ToDecimal(values.overall_score);

                    Decimal val2 = Decimal.Truncate(convertDecimal2);

                    double observation_percentage = Math.Round((double)((val1 / val2) * 100), 2);

                    msSQL = " update atm_trn_tauditcreation set status_flag ='Y',observation_score='" + values.total_amount + "',observation_percentage = '" + observation_percentage.ToString() + "' " +
                            " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation set overall_score='" + values.overall_score + "' " +
                          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Check Point Observation Score Updated Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
            else
            {



                msSQL = "select overall_detail from atm_trn_tchecklist2checkpoint where auditcreation2checklist_gid ='" + values.auditcreation2checklist_gid + "' and checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Select atleast one Checkpoint";
                    return;
                }


                string lsyes_score = "", lsno_score = "", lspartial_score = "", lsna_score = "";
                msSQL = " select auditcreation2checklist_gid,yes_score,no_score,partial_score,na_score from atm_trn_tauditcreation2checklist " +
                           " where auditcreation2checklist_gid = '" + values.auditcreation2checklist_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsyes_score = objODBCDatareader["yes_score"].ToString();
                    lsno_score = objODBCDatareader["no_score"].ToString();
                    lspartial_score = objODBCDatareader["partial_score"].ToString();
                    lsna_score = objODBCDatareader["na_score"].ToString();

                }
                objODBCDatareader.Close();


                string getscore = values.capture_score == "Yes" ? lsyes_score : values.capture_score == "No" ? lsno_score : values.capture_score == "Partial" ? lspartial_score : lsna_score;

                msSQL = " update atm_trn_tauditcreation2checklist set capture_field='" + values.capture_score + "', capture_score='" + getscore + "' " +
                       " where auditcreation2checklist_gid = '" + values.auditcreation2checklist_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("OTAL");
                    msSQL = " insert into atm_trn_tobservationtotalamountlog(" +
                              " observationtotalamountlog_gid ," +
                              " auditcreation2checklist_gid," +
                              " auditcreation_gid," +
                               " observationtotalamount_gid," +
                              " checklistmasteradd_gid," +
                              " capture_score, " +
                              " capture_field, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                               "'" + msGetGid + "'," +
                               "'" + values.auditcreation2checklist_gid + "', " +
                               "'" + values.auditcreation_gid + "', " +
                               "'" + values.observationtotalamount_gid + "', " +
                               "'" + values.checklistmasteradd_gid + "', " +
                               "'" + getscore + "'," +
                               "'" + values.capture_score + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + values.auditcreation_gid + "'";
                    values.total_amount = objdbconn.GetExecuteScalar(msSQL);

                    var convertDecimal1 = Convert.ToDecimal(values.total_amount);

                    Decimal val1 = Decimal.Truncate(convertDecimal1);

                    msSQL = " select sum(yes_score)as overall_score from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + values.auditcreation_gid + "'";
                    values.overall_score = objdbconn.GetExecuteScalar(msSQL);

                    var convertDecimal2 = Convert.ToDecimal(values.overall_score);

                    Decimal val2 = Decimal.Truncate(convertDecimal2);

                    double observation_percentage = Math.Round((double)((val1 / val2) * 100), 2);

                    msSQL = " update atm_trn_tauditcreation set status_flag ='Y',observation_score='" + values.total_amount + "',observation_percentage = '" + observation_percentage.ToString() + "' " +
                            " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation set overall_score='" + values.overall_score + "' " +
                          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Check Point Observation Score Updated Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
                msSQL = "select count(*) from atm_trn_tauditcreation2checklist where auditcreation_gid = '" + values.auditcreation_gid + "' and capture_score is null";
                string lscount = objdbconn.GetExecuteScalar(msSQL);
                if (lscount == "0")
                    values.allobservationfilled = true;
                else
                    values.allobservationfilled = false;

            }

        }

        public void DaPostAuditorMakerCheckpointObservation(makercheckpointobservation values, string employee_gid)
        {
            msSQL = "select auditcreation_gid from atm_trn_tcheckpointobservation where  auditcreation_gid ='" + values.auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                foreach (string i in values.auditcreation2checklist_gid)
                {

                    msSQL = " select b.auditcreation2checklist_gid,a.auditcreation_gid,b.checklistmasteradd_gid, a.auditdepartment_name, a.audittype_name, " +
                            " b.checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
                            " riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score,c.capture_score" +
                            " from atm_trn_tauditcreation a " +
                            " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
                            " left join atm_trn_tobservationtotalamount c on b.auditcreation2checklist_gid=c.auditcreation2checklist_gid " +
                            " where b.auditcreation2checklist_gid='" + i + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msSQL = " update atm_trn_tcheckpointobservation set" +
                                    " checklistmasteradd_gid ='" + dt["checklistmasteradd_gid"].ToString() + "'," +
                                    " auditcreation_gid ='" + dt["auditcreation_gid"].ToString() + "'," +
                                    " auditdepartment_name ='" + dt["auditdepartment_name"].ToString() + "'," +
                                    " audittype_name  ='" + dt["audittype_name"].ToString() + "'," +
                                    " checkpointgroup_name = '" + dt["checkpointgroup_name"].ToString() + "'," +
                                    " audit_name ='" + dt["audit_name"].ToString() + "'," +
                                    " checkpoint_intent ='" + dt["checkpoint_intent"].ToString() + "'," +
                                    " checkpoint_description= '" + dt["checkpoint_description"].ToString() + "'," +
                                    " riskcategory_name ='" + dt["riskcategory_name"].ToString() + "'," +
                                    " positiveconfirmity_name='" + dt["positiveconfirmity_name"].ToString() + "'," +
                                    " noteto_auditor ='" + dt["noteto_auditor"].ToString() + "'," +
                                    " yes_score = '" + dt["yes_score"].ToString() + "'," +
                                    " no_score='" + dt["no_score"].ToString() + "'," +
                                    " partial_score='" + dt["partial_score"].ToString() + "'," +
                                    " na_score='" + dt["na_score"].ToString() + "'," +
                                    " capture_score='" + dt["capture_score"].ToString() + "'," +
                                   " updated_by='" + employee_gid + "'," +
                                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                     " where auditcreation2checklist_gid='" + i + "' ";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                dt_datatable.Dispose();
                msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount where auditcreation_gid ='" + values.auditcreation_gid + "'";
                values.total_amount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update atm_trn_tauditcreation set status_flag ='Y',observation_score='" + values.total_amount + "' " +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Checkpoint observation details are saved successfully..!";
                }
                else
                {
                    values.message = "Error Occured..!";
                    values.status = false;
                }

            }
            else
            {
                foreach (string i in values.auditcreation2checklist_gid)
                {

                    msSQL = " select b.auditcreation2checklist_gid,a.auditcreation_gid,b.checklistmasteradd_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
                              "riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score,c.capture_score" +
                              " from atm_trn_tauditcreation a " +
                              " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
                               " left join atm_trn_tobservationtotalamount c on b.auditcreation2checklist_gid=c.auditcreation2checklist_gid " +
                                " where b.auditcreation2checklist_gid='" + i + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetGid = objcmnfunctions.GetMasterGID("CPOB");

                            msSQL = " insert into atm_trn_tcheckpointobservation(" +
                                    " checkpointobservation_gid," +
                                    " auditcreation2checklist_gid," +
                                     " checklistmasteradd_gid," +
                                    " auditcreation_gid," +
                                    " auditdepartment_name ," +
                                    " audittype_name ," +
                                    " checkpointgroup_name," +
                                    " audit_name ," +
                                    " checkpoint_intent," +
                                    " checkpoint_description ," +
                                    " riskcategory_name," +
                                    " positiveconfirmity_name ," +
                                    " noteto_auditor ," +
                                    " yes_score ," +
                                    " no_score ," +
                                    " partial_score ," +
                                    " na_score," +
                                    " capture_score," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                      "'" + i + "', " +
                                "'" + dt["checklistmasteradd_gid"].ToString() + "'," +
                                 "'" + dt["auditcreation_gid"].ToString() + "'," +
                                  "'" + dt["auditdepartment_name"].ToString() + "'," +
                                  "'" + dt["audittype_name"].ToString() + "'," +
                                   "'" + dt["checkpointgroup_name"].ToString() + "'," +
                                    "'" + dt["audit_name"].ToString() + "'," +
                                    "'" + dt["checkpoint_intent"].ToString() + "'," +
                                     "'" + dt["checkpoint_description"].ToString() + "'," +
                                      "'" + dt["riskcategory_name"].ToString() + "'," +
                                      "'" + dt["positiveconfirmity_name"].ToString() + "'," +
                                      "'" + dt["noteto_auditor"].ToString() + "'," +
                                      "'" + dt["yes_score"].ToString() + "'," +
                                      "'" + dt["no_score"].ToString() + "'," +
                                      "'" + dt["partial_score"].ToString() + "'," +
                                      "'" + dt["na_score"].ToString() + "'," +
                                       "'" + dt["capture_score"].ToString() + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount where auditcreation_gid ='" + values.auditcreation_gid + "'";
                        values.total_amount = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " update atm_trn_tauditcreation set status_flag ='Y',observation_score='" + values.total_amount + "' " +
                                " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "Checkpoint observation details are saved successfully..!";
                }
                else
                {
                    values.message = "Error Occured..!";
                    values.status = false;
                }
            }
        }
        public void DaGetAuditorMakerStatus(MdlAtmTrnAuditorMaker values, string employee_gid)
        {

            msSQL = " update atm_trn_tauditcreation set " +
            " status='" + values.status_update + "'," +
            " created_by='" + employee_gid + "'," +
            " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
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
                       " status_remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditcreation_gid + "'," +
                      " '" + values.status_update + "'," +
                        " '" + values.status_remarks + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select employee_gid, auditcreation_gid from atm_trn_tauditcreation where employee_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update atm_trn_tstatusupdatelog set auditormaker_gid='" + employee_gid + "'," +
                        " auditmakerupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where statusupdatelog_gid='" + msGetGid + "' ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                msSQL = "select auditmapping_gid, auditcreation_gid from atm_trn_tauditcreation where auditmapping_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update atm_trn_tstatusupdatelog set auditorchecker_gid='" + employee_gid + "'," +
                        " auditorcheckerupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where statusupdatelog_gid='" + msGetGid + "' ";
                }

                msSQL = "select auditmapping2employee_gid, auditcreation_gid from atm_trn_tauditcreation where auditmapping2employee_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update atm_trn_tstatusupdatelog set auditorapprover_gid='" + employee_gid + "'," +
                        " auditorapproverupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                       " where statusupdatelog_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                msSQL = " update atm_trn_tauditcreation set status_remarks='" + values.status_remarks + "'" +
                   " where auditcreation_gid='" + values.auditcreation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetCompletedAuditorMaker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(b.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join atm_trn_tauditapproval b on b.auditcreation_gid = a.auditcreation_gid" +
                    " left join hrm_mst_temployee f on b.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " where a.employee_gid='" + Employee_gid + "' and a.status = 'Completed' group by b.auditcreation_gid order by b.auditcreation_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetClosedAuditorMaker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditee_visible,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " where a.employee_gid='" + Employee_gid + "' and a.status = 'Closed' order by a.created_by desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetOpenAuditorMaker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.auditee_visible,a.audit_name,a.audittype_name,a.auditdepartment_name,a.makerapprovaloverall_flag,a.auditpriority_name,a.auditapproval_flag, " +
                      " a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, " +
                      " a.auditchecker_name,a.auditapprover_name," +
                     " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) " +
                     " as created_by  from atm_trn_tauditcreation a " +
                     " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                     " where a.employee_gid='" + Employee_gid + "' and a.status not in ('Hold','Closed','Completed') and a.auditapproval_flag='Y' " +
                     " order by a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            makerapprovaloverall_flag = (dr_datarow["makerapprovaloverall_flag"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetOnholdAuditorMaker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audittype_name,a.audit_name,a.auditdepartment_name,a.auditpriority_name,a.auditee_visible,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                     " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                     " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                     " where a.employee_gid='" + Employee_gid + "' and a.status = 'Hold' order by a.created_by desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAuditorMakerCounts(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {


            msSQL = " select (select count(auditcreation_gid) from atm_trn_tauditcreation where employee_gid='" + Employee_gid + "' and status = 'Hold') as auditsonhold_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation where employee_gid='" + Employee_gid + "' and status not in ('Hold','Closed','Completed') and auditapproval_flag='Y') as openaudit_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation where employee_gid='" + Employee_gid + "' and status = 'Closed' ) as closedaudit_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation where employee_gid='" + Employee_gid + "' and status = 'Completed') as completedaudit_count ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditsonhold_count = objODBCDatareader["auditsonhold_count"].ToString();
                values.openaudit_count = objODBCDatareader["openaudit_count"].ToString();
                values.closedaudit_count = objODBCDatareader["closedaudit_count"].ToString();
                values.completedaudit_count = objODBCDatareader["completedaudit_count"].ToString();
            }
            objODBCDatareader.Close();
        }


        public void DaGetTaggedSampleMaker(MdlAtmTrnAuditorMaker values, string Employee_gid)
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
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetTaggedSampleChecker(MdlAtmTrnAuditorMaker values, string Employee_gid)
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
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetTaggedSampleApprover(MdlAtmTrnAuditorMaker values, string Employee_gid)
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
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetCompletedAuditorChecker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(b.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join atm_trn_tauditapproval b on b.auditcreation_gid = a.auditcreation_gid" +
                    " left join hrm_mst_temployee f on b.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " where a.auditmapping_gid='" + Employee_gid + "' and a.status = 'Completed' group by a.auditcreation_gid  order by a.auditcreation_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetClosedAuditorChecker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditee_visible,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " where a.auditmapping_gid='" + Employee_gid + "' and a.status = 'Closed' order by a.created_date desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetOpenAuditorChecker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.auditee_visible,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno, " +
                     " a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, " +
                     " a.auditchecker_name,a.auditapprover_name,  a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid, " +
                     " a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                     " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                     " where a.auditmapping_gid='" + Employee_gid + "' and a.status not in ('Hold','Closed','Completed') and " +
                     " ( approval_status ='Initiate Audit Approved' and a.auditapproval_flag='Y' and a.auditormaker_approvalflag='N' )  order by a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetOnholdAuditorChecker(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditee_visible,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                     " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                     " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                     " where a.auditmapping_gid='" + Employee_gid + "' and a.status = 'Hold' order by a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                            auditee_visible = (dr_datarow["auditee_visible"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAuditorCheckerCounts(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            msSQL = " select (select count(auditcreation_gid) from atm_trn_tauditcreation  where auditmapping_gid='" + Employee_gid + "' and status = 'Hold') as auditscheckeronhold_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation where auditmapping_gid='" + Employee_gid + "'  and status not in ('Hold','Closed','Completed') and ( approval_status ='Initiate Audit Approved' and auditapproval_flag='Y' ) ) as opencheckeraudit_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation  where auditmapping_gid='" + Employee_gid + "' and status = 'Closed') as closedcheckeraudit_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation  where ( auditmapping_gid ='" + Employee_gid + "') and auditormaker_approvalflag='Y' and (auditorchecker_approvalflag='N' or auditorchecker_approvalflag='Y') and status not in ('Hold','Closed','Completed') ) as pendingapprovalaudit_count," +
" (select count(auditcreation_gid) from atm_trn_tauditcreation where auditmapping_gid='" + Employee_gid + "' and status = 'Completed')  as completedcheckeraudit_count ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditscheckeronhold_count = objODBCDatareader["auditscheckeronhold_count"].ToString();
                values.opencheckeraudit_count = objODBCDatareader["opencheckeraudit_count"].ToString();
                values.closedcheckeraudit_count = objODBCDatareader["closedcheckeraudit_count"].ToString();
                values.completedcheckeraudit_count = objODBCDatareader["completedcheckeraudit_count"].ToString();
                values.pendingapprovalaudit_count = objODBCDatareader["pendingapprovalaudit_count"].ToString();
            }
            objODBCDatareader.Close();
        }


        public void DaGetClosedAuditorApprover(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " where a.auditmapping2employee_gid='" + Employee_gid + "' and a.status = 'Closed' order by a.created_date desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetOpenAuditorApprover(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno, " +
                      " a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, " +
                      " a.auditchecker_name,a.auditapprover_name," +
                     " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by " +
                     " from atm_trn_tauditcreation a " +
                     " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                     " where a.auditmapping2employee_gid='" + Employee_gid + "' and ( auditorapprover_approvalflag ='N') " +
                     " and a.status not in ('Hold','Closed','Completed') and approval_status not in ('Final Approval Pending') order by a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetOnholdAuditorApprover(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {

                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                     " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                     " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                     " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                     " where a.auditmapping2employee_gid='" + Employee_gid + "' and a.status = 'Hold' order by a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            approval_flag = (dr_datarow["approval_flag"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetCompletedAuditorApprover(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            values.employee_gid = Employee_gid;
            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,date_format(a.due_date,'%d-%m-%Y') as due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,date_format(b.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
                    " left join atm_trn_tauditapproval b on b.auditcreation_gid = a.auditcreation_gid" +
                    " left join hrm_mst_temployee f on b.created_by = f.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                    " where a.auditmapping2employee_gid='" + Employee_gid + "' and a.status = 'Completed' group by a.auditcreation_gid order by a.auditcreation_gid desc";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
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

                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAuditorApproverCounts(MdlAtmTrnAuditorMaker values, string Employee_gid)
        {
            msSQL = " select (select count(auditcreation_gid) from atm_trn_tauditcreation  where auditmapping2employee_gid='" + Employee_gid + "' and status = 'Hold') as auditsapproveronhold_count, " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation  where auditmapping2employee_gid='" + Employee_gid + "' and ( approval_status ='Initiate Audit Approved' and status='Initiate Audit Approved' )) as openapproveraudit_count , " +
" (select count(auditcreation_gid) from atm_trn_tauditcreation  where auditmapping2employee_gid='" + Employee_gid + "' and status = 'Closed' ) as closedapproveraudit_count, " +
" (select count(distinct a.auditcreation_gid) from atm_trn_tauditcreation a " +
" left join atm_trn_tauditagainstmultipleauditeechecker b on b.auditcreation_gid = a.auditcreation_gid " +
" where ( a.auditmapping2employee_gid ='" + Employee_gid + "' and b.multiauditeechecker_approvalflag ='Y' and a.auditorapprover_approvalflag='N' and a.approval_status ='Final Approval Pending')) as auditapproverpending_count," +
" (select count(auditcreation_gid) from atm_trn_tauditcreation  where auditmapping2employee_gid='" + Employee_gid + "' and status = 'Completed') as completedapproveraudit_count ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditsapproveronhold_count = objODBCDatareader["auditsapproveronhold_count"].ToString();
                values.openapproveraudit_count = objODBCDatareader["openapproveraudit_count"].ToString();
                values.closedapproveraudit_count = objODBCDatareader["closedapproveraudit_count"].ToString();
                values.auditapproverpending_count = objODBCDatareader["auditapproverpending_count"].ToString();
                values.completedapproveraudit_count = objODBCDatareader["completedapproveraudit_count"].ToString();
            }
            objODBCDatareader.Close();
        }



        public void DaGetAuditCreationIntent(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
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
        public void DaGetAuditCreationDescription(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
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
        public void DaGetAuditCreationAuditor(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
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

        public void DaPostRaiseQuery(MdlAtmTrnAuditorMaker values, string employee_gid)
        {

            msSQL = " update atm_trn_tauditcreation set description='" + values.description.Replace("'", "") + "'" +
                   " where auditcreation_gid='" + values.auditcreation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            for (var i = 0; i < values.employe.Count; i++)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AURQ");
                msSQL = "Insert into atm_trn_traisequery( " +
                       " raisequery_gid, " +
                       " auditcreation_gid," +
                       " description," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.auditcreation_gid + "', " +
                       "'" + values.description.Replace("'", "") + "'," +
                       "'" + values.employe[i].employee_gid + "'," +
                       "'" + values.employe[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Query Raised Added Successfully";
            }
            else
            {
                values.message = "Error Occured While Raising Query";
                values.status = false;
            }
        }


        public void DaAssignedQuerySummary(string employee_gid, MdlAtmTrnAuditorMaker values, string auditcreation_gid)
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
                " WHERE a.auditcreation_gid= '" + auditcreation_gid + "' and a.employee_gid = '" + employee_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakerReplyToQueryList = new List<makerReplyToQueryList>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakerReplyToQueryList.Add(new makerReplyToQueryList

                        {

                            raisequery_gid = (dr_datarow["raisequery_gid"].ToString()),
                            assigned_by = (dr_datarow["created_by"].ToString()),
                            assigned_to = (dr_datarow["assigned_to"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            description = (dr_datarow["description"].ToString()),

                        });
                    }
                    values.makerReplyToQueryList = getmakerReplyToQueryList;
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


        public void DaRaisedsampleQuerySummary(samplequerydatalist values, string sampleimport_gid)
        {
            try
            {
                msSQL = " select sampleraisequery_gid,description,raisequery_flag,query_title,query_toname,close_remarks, " +
                        "  concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,sampleraisequery_status " +
                         " from atm_trn_tsampleraisequery a " +
                         " left join hrm_mst_temployee f on a.created_by = f.employee_gid " +
                         " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                         " WHERE a.sampleimport_gid= '" + sampleimport_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsamplequerydata = new List<samplequerydata>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsamplequerydata.Add(new samplequerydata
                        {
                            sampleraisequery_gid = (dr_datarow["sampleraisequery_gid"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            raisequery_flag = (dr_datarow["raisequery_flag"].ToString()),
                            query_title = (dr_datarow["query_title"].ToString()),
                            query_toname = (dr_datarow["query_toname"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            sampleraisequery_status = (dr_datarow["sampleraisequery_status"].ToString()),
                            close_remarks = (dr_datarow["close_remarks"].ToString()),
                        });
                    }
                    values.samplequerydata = getsamplequerydata;
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

        public void DaEditAssignedQuery(string raisequery_gid, MdlAtmTrnAuditorMaker values)
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


        public void DaPostReplyToQuery(MdlAtmTrnAuditorMaker values, string employee_gid)
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

        public void DaRepliedQuerySummary(string employee_gid, MdlAtmTrnAuditorMaker values, string auditcreation_gid)
        {
            try
            {
                msSQL = " SELECT a.raisequery_gid, b.replytoquery_gid, b.reply_query, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, a.description, " +
                    " (a.employee_name) as assigned_to, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as updated_by, date_format(b.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    "  a.employee_gid " +
                    " FROM atm_trn_traisequery a " +
                    " left join atm_trn_treplytoquery b on a.raisequery_gid = b.raisequery_gid " +
                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.updated_by " +
                    " left join hrm_mst_temployee h on h.employee_gid = a.created_by " +
                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                    " left join adm_mst_tuser e on e.user_gid = h.user_gid " +
                " WHERE a.auditcreation_gid= '" + auditcreation_gid + "'";


                //" WHERE b.employee_gid= '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakerReplyToQueryList = new List<makerReplyToQueryList>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakerReplyToQueryList.Add(new makerReplyToQueryList

                        {

                            raisequery_gid = (dr_datarow["raisequery_gid"].ToString()),
                            replytoquery_gid = (dr_datarow["replytoquery_gid"].ToString()),
                            assigned_by = (dr_datarow["created_by"].ToString()),
                            assigned_to = (dr_datarow["assigned_to"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            reply_query = (dr_datarow["reply_query"].ToString()),
                            replied_by = (dr_datarow["updated_by"].ToString()),
                            replied_date = (dr_datarow["updated_date"].ToString()),
                        });
                    }
                    values.makerReplyToQueryList = getmakerReplyToQueryList;
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

        public void DaGetEmployeeName(string raisequery_gid, employelist values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from atm_trn_traisequery " +
                  " where raisequery_gid='" + raisequery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaPostAuditorCheckerApproval(initialapprovaldtl values, string employee_gid)
        {

            msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditchecker_name,a.employee_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                         " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                         " FROM atm_trn_tauditcreation a" +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                          " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinitialapprovalview_list = new List<initialapprovalview_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var lsinitiated_gid = dt["employee_gid"].ToString();
                    var lsauditchecker_gid = dt["auditmapping_gid"].ToString();
                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                    var lsapproval_name = dt["auditchecker_name"].ToString();
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
                           "'" + lsauditchecker_gid + "'," +
                           "'" + lsapproval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "' Auditor Checker Approval'," +
                           "'" + values.auditorapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {

                    msSQL = "update atm_trn_tauditcreation set approval_flag ='Y'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update atm_trn_tauditcreation set approval_status ='Auditor Checker Approval'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Auditor Checker Approved  Successfully  ..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
                dt_datatable.Dispose();
            }
        }

        public void DaPostAuditorCheckerRejected(initialapprovaldtl values, string employee_gid)
        {

            msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditchecker_name,a.employee_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                          " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                          " FROM atm_trn_tauditcreation a" +
                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                           " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinitialapprovalview_list = new List<initialapprovalview_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var lsinitiated_gid = dt["employee_gid"].ToString();
                    var lsauditchecker_gid = dt["auditmapping_gid"].ToString();
                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                    var lsapproval_name = dt["auditchecker_name"].ToString();
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
                           "'" + lsauditchecker_gid + "'," +
                           "'" + lsapproval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "' Auditor Checker Rejected'," +
                           "'" + values.auditorapproval_remark + "'," +
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

                    msSQL = "update atm_trn_tauditcreation set approval_status ='Auditor Checker Rejected'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Auditor Checker Rejected  Successfully  ..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
                dt_datatable.Dispose();
            }
        }

        public void DaGetMultipleAuditeecheckerApproval(initialapprovaldtl values, string auditcreation_gid, string employee_gid)
        {
            values.employee_gid = employee_gid;

            msSQL = "select auditeechecker_gid, auditeechecker_approvalflag from atm_trn_tauditagainstmultipleauditeechecker " +
                 "where auditcreation_gid = '" + auditcreation_gid + "' and auditeechecker_gid = '" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsauditeechecker_gid = objODBCDatareader["auditeechecker_gid"].ToString();
                lsauditeechecker_approvalflag = objODBCDatareader["auditeechecker_approvalflag"].ToString();
            }
            objODBCDatareader.Close();

            if (lsauditeechecker_approvalflag == "N" && lsauditeechecker_gid == employee_gid)
            {
                values.auditee_flag = "Y";
            }
            else
            {
                values.auditee_flag = "N";

            }
        }

        public void DaPostAuditeeGetApproval(initialapprovaldtl values, string employee_gid)
        {
            msSQL = " update atm_trn_tauditagainstmultipleauditeechecker set auditeechecker_approvalflag='Y',auditeechecker_approvalstatus='Approved' " +
                   " where auditcreation_gid = '" + values.auditcreation_gid + "' and auditeechecker_gid = '" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = " select count(*) as multiauditee from atm_trn_tauditagainstmultipleauditeechecker where auditcreation_gid = '" + values.auditcreation_gid + "' and auditeechecker_approvalflag='N'";

            values.openquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.openquerycount == "0")
            {
                msSQL = " update atm_trn_tauditagainstmultipleauditeechecker set multiauditeechecker_approvalflag='Y' " +
               " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                string lsapproval_staus = "";
                string lsinitiated_gid = "", lsauditcreation_gid = "", lsinitiated_name = "", lsauditorchecker_approvalflag = "", lsauditeechecker_approvalflag = "";
                msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditchecker_name,a.employee_gid,a.auditmaker_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,e.auditeechecker_approvalflag,e.multiauditeechecker_approvalflag,auditorchecker_approvalflag, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " left join atm_trn_tauditagainstmultipleauditeechecker e on a.auditcreation_gid = e.auditcreation_gid" +
                        " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsinitiated_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                    lsinitiated_name = objODBCDatareader["auditchecker_name"].ToString();
                    lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                    lsauditeechecker_approvalflag = objODBCDatareader["auditeechecker_approvalflag"].ToString();
                    lsmultiauditeechecker_approvalflag = objODBCDatareader["multiauditeechecker_approvalflag"].ToString();

                }
                objODBCDatareader.Close();
                if (lsauditorchecker_approvalflag == "Y" && lsmultiauditeechecker_approvalflag == "Y")
                    lsapproval_staus = "Final Approval Pending";
                else if (lsauditorchecker_approvalflag == "N" && (lsauditeechecker_approvalflag == "Y"))
                    lsapproval_staus = "Auditor Checker Approval Pending";
                else if (lsauditorchecker_approvalflag == "Y" && (lsauditeechecker_approvalflag == "N"))
                    lsapproval_staus = "Checker - Auditee Pending";

                msSQL = " update atm_trn_tauditcreation set approval_status ='" + lsapproval_staus + "'" +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lsauditorchecker_approvalflag == "Y" && lsauditeechecker_approvalflag == "Y")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AGAP");

                    msSQL = "Insert into atm_trn_tauditorapprovalget( " +
                            " auditorapprovalget_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                           " approver_gid," +
                           " approver_name," +
                           " initiateapproval," +
                           " approval_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                             "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                           "'" + values.approval_gid + "'," +
                           "'" + values.approval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "'" + values.getapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Proceed to Approver Successfully..!";
                    msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid,auditorcommonname_flag from atm_trn_tauditcreation " +
                                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                        lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        lsauditorcommonname_flag = objODBCDatareader["auditorcommonname_flag"].ToString();

                    }
                    objODBCDatareader.Close();
                    if (lsauditorcommonname_flag == "Y")
                    {

                        msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditmapping2employee_gid,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " FROM atm_trn_tauditcreation a" +
                                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                 " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                            lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                            lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                            lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                            lscreated_date = objODBCDatareader["created_date"].ToString();
                        }
                        objODBCDatareader.Close();

                        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                        msSQL = "Insert into atm_trn_tauditapproval( " +
                               " auditapproval_gid, " +
                               " initialapproval_gid, " +
                               " auditcreation_gid," +
                                " auditorapproval_gid, " +
                               " approval_name," +
                               " initiateapproval," +
                               " approval_status," +
                               " approve_remark," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + lsinitiated_gid + "'," +
                               "'" + lsauditcreation_gid + "'," +
                              "'" + lsauditapproval_gid + "'," +
                               "'" + lsapproval_name + "'," +
                                "'" + lsinitiated_name + "'," +
                               "' Auditor Approved'," +
                               "'" + values.auditorapproval_remark + "'," +
                               "'" + lscreated_by + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {

                            //msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                            //          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            values.status = true;
                            values.message = "  Auditee Checker has been Approved Successfully..!";
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,d.auditeechecker_gid, a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                lsto2members = objODBCDatareader["auditmapping_gid"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = " RE: Audit Final Approval ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + " The following audit has been submitted for approval, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Audit.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,a.employee_gid, a.auditmaker_name,d.auditeechecker_name,group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',', a.employee_gid)  as cc2members ,a.auditmapping2employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                lscc2members = objODBCDatareader["cc2members"].ToString();
                                lsto2members = objODBCDatareader["auditmapping2employee_gid"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);

                            sub = " RE: Auditee Approval ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + " The following audit has been submitted for approval, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Audit.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
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

                                msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditmapping2employee_gid,auditorchecker_approvalflag,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " FROM atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                     " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                                    lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                    lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                                    lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                                    lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                                    lscreated_date = objODBCDatareader["created_date"].ToString();
                                    lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                                }
                                objODBCDatareader.Close();
                                if (lsauditorchecker_approvalflag == "Y")
                                {
                                    msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                                    msSQL = "Insert into atm_trn_tauditapproval( " +
                                           " auditapproval_gid, " +
                                           " initialapproval_gid, " +
                                           " auditcreation_gid," +
                                            " auditorapproval_gid, " +
                                           " approval_name," +
                                           " initiateapproval," +
                                           " approval_status," +
                                           " approve_remark," +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + msGetGid + "'," +
                                           "'" + lsinitiated_gid + "'," +
                                           "'" + lsauditcreation_gid + "'," +
                                          "'" + lsauditapproval_gid + "'," +
                                           "'" + lsapproval_name + "'," +
                                            "'" + lsinitiated_name + "'," +
                                           "' Auditor Approved'," +
                                           "'" + values.auditorapproval_remark + "'," +
                                           "'" + lscreated_by + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    //msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                                    //         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + lsobservation_percentage + "'" +
                                             " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = " Auditee has been Approved Successfully..!";
                                }

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

                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name,group_concat(distinct d.auditeemaker_gid, ',', a.employee_gid)  as cc2members ,a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                 " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                    " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                    lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                    lscc2members = objODBCDatareader["cc2members"].ToString();
                                    lsto2members = objODBCDatareader["auditmapping_gid"].ToString();
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


                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                         " from atm_trn_tauditcreation  " +
                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                                employeename = objdbconn.GetExecuteScalar(msSQL);

                                sub = " RE: Audit Final Approval ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + " The following audit has been submitted for approval, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to Approve the Audit.";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "Thanks & Regards, ";
                                body = body + "<br />";
                                body = body + HttpUtility.HtmlEncode(employeename);
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
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + lsemployee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Audit has been Approval'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + lsemployee_gid + "'," +
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

                                    msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditmapping2employee_gid,auditorchecker_approvalflag,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " FROM atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                         " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                                        lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                                        lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                                        lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                                        lscreated_date = objODBCDatareader["created_date"].ToString();
                                        lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                                    }
                                    objODBCDatareader.Close();
                                    if (lsauditorchecker_approvalflag == "Y")
                                    {
                                        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                                        msSQL = "Insert into atm_trn_tauditapproval( " +
                                               " auditapproval_gid, " +
                                               " initialapproval_gid, " +
                                               " auditcreation_gid," +
                                                " auditorapproval_gid, " +
                                               " approval_name," +
                                               " initiateapproval," +
                                               " approval_status," +
                                               " approve_remark," +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + msGetGid + "'," +
                                               "'" + lsinitiated_gid + "'," +
                                               "'" + lsauditcreation_gid + "'," +
                                              "'" + lsauditapproval_gid + "'," +
                                               "'" + lsapproval_name + "'," +
                                                "'" + lsinitiated_name + "'," +
                                               "' Auditor Approved'," +
                                               "'" + values.auditorapproval_remark + "'," +
                                               "'" + lscreated_by + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        //msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                                        //         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + lsobservation_percentage + "'" +
                                                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        values.status = true;
                                        values.message = " Auditee Checker has been Approved Successfully..!";
                                    }
                                    if (mnResult != 0)
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

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,a.auditapprover_name a.auditmaker_name,a.auditeechecker_name,group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',', a.auditmapping_gid)  as cc2members , a.auditmapping2employee_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                                "left join atm_trn_tauditagainstmultipleauditeechecker e on  e.auditcreation_gid = a.auditcreation_gid" +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditapprover_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                            lscc2members = objODBCDatareader["cc2members"].ToString();
                                            lsto2members = objODBCDatareader["auditmapping2employee_gid"].ToString();
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


                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                                 " from atm_trn_tauditcreation  " +
                                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                                        employeename = objdbconn.GetExecuteScalar(msSQL);


                                        sub = " RE: Audit Final Approval ";
                                        body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + " The following audit has been submitted for approval, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Approve the Audit.";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employeename);
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
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + lsemployee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Audit has been Approval'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + lsemployee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }

                                }

                                else
                                {
                                    try
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

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name,e.auditeechecker_name, a.auditmapping_gid,a.auditmapping2employee_gid, group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',', a.employee_gid, ',', a.auditmapping_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                           "left join atm_trn_tauditagainstmultipleauditeechecker e on  e.auditcreation_gid = a.auditcreation_gid" +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditapprover_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                            lsauditorchecker_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
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


                                        msSQL = " select employee_emailid  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsemployee_gid.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                                        " from atm_trn_tauditcreation  " +
                                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                                        employeename = objdbconn.GetExecuteScalar(msSQL);


                                        sub = " RE: Audit Final Approval ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + " The following audit has been submitted for approval, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Approve the Audit.";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employeename);
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
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + lsauditeechecker_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Auditee Checker Approved'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + employee_gid + "'," +
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
                        }
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


                string lsapproval_staus = "";
                string lsinitiated_gid = "", lsauditcreation_gid = "", lsinitiated_name = "", lsauditorchecker_approvalflag = "", lsauditeechecker_approvalflag = "";
                msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditchecker_name,a.employee_gid,a.auditmaker_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,d.auditeechecker_approvalflag,d.multiauditeechecker_approvalflag,auditorchecker_approvalflag, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                     " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                        " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsinitiated_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                    lsinitiated_name = objODBCDatareader["auditchecker_name"].ToString();
                    lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                    lsauditeechecker_approvalflag = objODBCDatareader["auditeechecker_approvalflag"].ToString();
                    lsmultiauditeechecker_approvalflag = objODBCDatareader["multiauditeechecker_approvalflag"].ToString();

                }
                objODBCDatareader.Close();
                if (lsauditorchecker_approvalflag == "Y" && lsmultiauditeechecker_approvalflag == "Y")
                    lsapproval_staus = "Final Approval Pending";
                else if (lsauditorchecker_approvalflag == "N" && (lsauditeechecker_approvalflag == "Y"))
                    lsapproval_staus = "Auditor Checker Approval Pending";
                else if (lsauditorchecker_approvalflag == "Y" && (lsauditeechecker_approvalflag == "N"))
                    lsapproval_staus = "Checker - Auditee Pending";
                else if (lsauditorchecker_approvalflag == "Y" && (lsauditeechecker_approvalflag == "Y"))
                    lsapproval_staus = "Checker - Auditee Pending";

                msSQL = " update atm_trn_tauditcreation set approval_status ='" + lsapproval_staus + "'" +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lsauditorchecker_approvalflag == "Y" && lsauditeechecker_approvalflag == "Y")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AGAP");

                    msSQL = "Insert into atm_trn_tauditorapprovalget( " +
                            " auditorapprovalget_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                           " approver_gid," +
                           " approver_name," +
                           " initiateapproval," +
                           " approval_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                             "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                           "'" + values.approval_gid + "'," +
                           "'" + values.approval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "'" + values.getapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Auditee Checker  Approved Successfully..!";
                    msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid,auditorcommonname_flag from atm_trn_tauditcreation " +
                                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                        lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        lsauditorcommonname_flag = objODBCDatareader["auditorcommonname_flag"].ToString();

                    }
                    objODBCDatareader.Close();
                    if (lsauditorcommonname_flag == "Y")
                    {

                        msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditmapping2employee_gid,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                " FROM atm_trn_tauditcreation a" +
                                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                 " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                            lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                            lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                            lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                            lscreated_date = objODBCDatareader["created_date"].ToString();
                        }
                        objODBCDatareader.Close();

                        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                        msSQL = "Insert into atm_trn_tauditapproval( " +
                               " auditapproval_gid, " +
                               " initialapproval_gid, " +
                               " auditcreation_gid," +
                                " auditorapproval_gid, " +
                               " approval_name," +
                               " initiateapproval," +
                               " approval_status," +
                               " approve_remark," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + lsinitiated_gid + "'," +
                               "'" + lsauditcreation_gid + "'," +
                              "'" + lsauditapproval_gid + "'," +
                               "'" + lsapproval_name + "'," +
                                "'" + lsinitiated_name + "'," +
                               "' Auditor Approved'," +
                               "'" + values.auditorapproval_remark + "'," +
                               "'" + lscreated_by + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {

                            //msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                            //          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            values.status = true;
                            values.message = " Auditee Checker has been Approved Successfully..!";
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,e.auditeemaker_gid,e.auditeechecker_gid, a.auditmaker_name,e.auditeechecker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                               "left join atm_trn_tauditagainstmultipleauditeechecker e on  e.auditcreation_gid = a.auditcreation_gid" +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                lsto2members = objODBCDatareader["auditmapping_gid"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();


                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);

                            sub = " RE: Audit Final Approval ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + " The following audit has been submitted for approval, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno )+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Audit.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,a.employee_gid, a.auditmaker_name,d.auditeechecker_name,group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',', a.employee_gid)  as cc2members ,a.auditmapping2employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                lscc2members = objODBCDatareader["cc2members"].ToString();
                                lsto2members = objODBCDatareader["auditmapping2employee_gid"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                   " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                               " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);


                            sub = " RE: Audit Final Approval ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + " The following audit has been submitted for approval, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Audit.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
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

                                msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditmapping2employee_gid,auditorchecker_approvalflag,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                    " FROM atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                     " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                                    lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                    lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                                    lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                                    lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                                    lscreated_date = objODBCDatareader["created_date"].ToString();
                                    lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                                }
                                objODBCDatareader.Close();
                                if (lsauditorchecker_approvalflag == "Y")
                                {
                                    msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                                    msSQL = "Insert into atm_trn_tauditapproval( " +
                                           " auditapproval_gid, " +
                                           " initialapproval_gid, " +
                                           " auditcreation_gid," +
                                            " auditorapproval_gid, " +
                                           " approval_name," +
                                           " initiateapproval," +
                                           " approval_status," +
                                           " approve_remark," +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + msGetGid + "'," +
                                           "'" + lsinitiated_gid + "'," +
                                           "'" + lsauditcreation_gid + "'," +
                                          "'" + lsauditapproval_gid + "'," +
                                           "'" + lsapproval_name + "'," +
                                            "'" + lsinitiated_name + "'," +
                                           "' Auditor Approved'," +
                                           "'" + values.auditorapproval_remark + "'," +
                                           "'" + lscreated_by + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    //msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                                    //         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + lsobservation_percentage + "'" +
                                             " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    values.status = true;
                                    values.message = " Auditee Checker has been Approved Successfully..!";
                                }

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

                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name,group_concat(distinct d.auditeemaker_gid, ',', a.employee_gid)  as cc2members ,a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                  " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                    " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                    lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                    lscc2members = objODBCDatareader["cc2members"].ToString();
                                    lsto2members = objODBCDatareader["auditmapping_gid"].ToString();
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


                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                         " from atm_trn_tauditcreation  " +
                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                                employeename = objdbconn.GetExecuteScalar(msSQL);

                                sub = " RE: Audit Final Approval ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + " The following audit has been submitted for approval, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to Approve the Audit.";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "Thanks & Regards, ";
                                body = body + "<br />";
                                body = body + HttpUtility.HtmlEncode(employeename);
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
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + lsemployee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Audit has been Approval'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + lsemployee_gid + "'," +
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

                                    msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditmapping2employee_gid,auditorchecker_approvalflag,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                                        " FROM atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                                         " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                                        lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                                        lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                                        lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                                        lscreated_date = objODBCDatareader["created_date"].ToString();
                                        lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                                    }
                                    objODBCDatareader.Close();
                                    if (lsauditorchecker_approvalflag == "Y")
                                    {
                                        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                                        msSQL = "Insert into atm_trn_tauditapproval( " +
                                               " auditapproval_gid, " +
                                               " initialapproval_gid, " +
                                               " auditcreation_gid," +
                                                " auditorapproval_gid, " +
                                               " approval_name," +
                                               " initiateapproval," +
                                               " approval_status," +
                                               " approve_remark," +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + msGetGid + "'," +
                                               "'" + lsinitiated_gid + "'," +
                                               "'" + lsauditcreation_gid + "'," +
                                              "'" + lsauditapproval_gid + "'," +
                                               "'" + lsapproval_name + "'," +
                                                "'" + lsinitiated_name + "'," +
                                               "' Auditor Approved'," +
                                               "'" + values.auditorapproval_remark + "'," +
                                               "'" + lscreated_by + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        //msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                                        //         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + lsobservation_percentage + "'" +
                                                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        values.status = true;
                                        values.message = " Auditee Checker has been Approved Successfully..!";
                                    }
                                    if (mnResult != 0)
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

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,d.auditeechecker_name,group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',', a.auditmapping_gid)  as cc2members , a.auditmapping2employee_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                         " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                            lscc2members = objODBCDatareader["cc2members"].ToString();
                                            lsto2members = objODBCDatareader["auditmapping2employee_gid"].ToString();
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


                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                                 " from atm_trn_tauditcreation  " +
                                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                                        employeename = objdbconn.GetExecuteScalar(msSQL);


                                        sub = " RE: Audit Final Approval ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + " The following audit has been submitted for approval, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Approve the Audit.";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employeename);
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
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + lsemployee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Audit has been Approval'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + lsemployee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }

                                }

                                else
                                {
                                    try
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

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name,a.auditeechecker_name, a.auditmapping_gid,a.auditmapping2employee_gid, group_concat(distinct a.auditeemaker_gid, ',', a.auditeechecker_gid, ',', a.employee_gid, ',', a.auditmapping_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditapprover_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                            lsauditorchecker_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
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


                                        msSQL = " select employee_emailid  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsemployee_gid.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                                        " from atm_trn_tauditcreation  " +
                                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                                        employeename = objdbconn.GetExecuteScalar(msSQL);


                                        sub = " RE: Audit Final Approval ";
                                        body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + " The following audit has been submitted for approval, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Approve the Audit.";
                                        body = body + "<br />";
                                        body = body + "<br />";
                                        body = body + "Thanks & Regards, ";
                                        body = body + "<br />";
                                        body = body + HttpUtility.HtmlEncode(employeename);
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
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + lsauditeechecker_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Auditee Checker Approved'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + employee_gid + "'," +
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
                        }
                    }
                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }

        }


        public void DaPostAuditorGetApproval(initialapprovaldtl values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from atm_trn_tauditraisequery where auditcreation_gid = '" + values.auditcreation_gid + "'" +
                 " and auditraisequery_status = 'Open'";
            values.auditopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.auditopenquerycount != "0")
            {
                values.status = false;
                values.message = "Query Can't be Closed";
                return;
            }
            else
            {


                msSQL = " update atm_trn_tauditcreation set auditorchecker_approvalflag='Y',auditorcheckersample_flag='Y'," +
                    " auditorcheckerinitiated_by ='" + employee_gid + "', " +
                        " auditorcheckerinitiated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
         " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                string lsapproval_staus = "";
                string lsinitiated_gid = "", lsauditcreation_gid = "", lsinitiated_name = "", lsauditorchecker_approvalflag = "", lsauditeechecker_approvalflag = "";
                msSQL = " select a.auditcreation_gid,a.auditmapping_gid,a.auditchecker_name,a.employee_gid,a.auditmaker_name, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,d.multiauditeechecker_approvalflag,d.auditeechecker_approvalflag,auditorchecker_approvalflag, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                     " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                        " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsinitiated_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                    lscreated_by = objODBCDatareader["created_by"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                    lsinitiated_name = objODBCDatareader["auditchecker_name"].ToString();
                    lsauditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                    lsmultiauditeechecker_approvalflag = objODBCDatareader["multiauditeechecker_approvalflag"].ToString();
                    lsauditeechecker_approvalflag = objODBCDatareader["auditeechecker_approvalflag"].ToString();

                }
                objODBCDatareader.Close();

                if (lsauditorchecker_approvalflag == "Y" && lsmultiauditeechecker_approvalflag == "Y")
                    lsapproval_staus = "Final Approval Pending";
                else if (lsauditorchecker_approvalflag == "N" && (lsauditeechecker_approvalflag == "Y"))
                    lsapproval_staus = "Auditor Checker Approval Pending";
                else if (lsauditorchecker_approvalflag == "Y" && (lsauditeechecker_approvalflag == "N"))
                    lsapproval_staus = "Checker - Auditee Pending";
                else if (lsauditorchecker_approvalflag == "Y" && (lsauditeechecker_approvalflag == "Y"))
                    lsapproval_staus = "Checker - Auditee Pending";

                msSQL = " update atm_trn_tauditcreation set approval_status ='" + lsapproval_staus + "'" +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (lsauditorchecker_approvalflag == "Y" && lsmultiauditeechecker_approvalflag == "Y")
                {

                    msGetGid = objcmnfunctions.GetMasterGID("AGAP");

                    msSQL = "Insert into atm_trn_tauditorapprovalget( " +
                            " auditorapprovalget_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                           " approver_gid," +
                           " approver_name," +
                           " initiateapproval," +
                           " approval_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                             "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                           "'" + values.approval_gid + "'," +
                           "'" + values.approval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "'" + values.getapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                //msSQL = " select auditorcheckerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                //lsauditorcheckerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                //if (lsauditorcheckerapprover_flag == "Y")
                //{

                //    msSQL = " select a.auditcreation_gid,d.auditeechecker_approvalflag,d.multiauditeechecker_approvalflag,a.auditmapping_gid,a.auditmapping2employee_gid,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,a.auditchecker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                //        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                //        " FROM atm_trn_tauditcreation a" +
                //        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                //        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                //       " left join atm_trn_tmultipleauditee d on d.auditcreation_gid = a.auditcreation_gid  " +
                //         " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //    if (objODBCDatareader.HasRows == true)
                //    {
                //        lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                //        lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                //        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                //        lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                //        lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                //        lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                //        lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                //        lscreated_date = objODBCDatareader["created_date"].ToString();
                //        lsauditeechecker_approvalflag = objODBCDatareader["multiauditeechecker_approvalflag"].ToString();
                //    }
                //    objODBCDatareader.Close();
                //    if (lsauditeechecker_approvalflag == "Y")
                //    {
                //        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                //        msSQL = "Insert into atm_trn_tauditapproval( " +
                //               " auditapproval_gid, " +
                //               " initialapproval_gid, " +
                //               " auditcreation_gid," +
                //                " auditorapproval_gid, " +
                //               " approval_name," +
                //               " initiateapproval," +
                //               " approval_status," +
                //               " approve_remark," +
                //               " created_by," +
                //               " created_date)" +
                //               " values(" +
                //               "'" + msGetGid + "'," +
                //               "'" + lsinitiated_gid + "'," +
                //               "'" + lsauditcreation_gid + "'," +
                //              "'" + lsauditapproval_gid + "'," +
                //               "'" + lsapproval_name + "'," +
                //                "'" + lsinitiated_name + "'," +
                //               "' Auditor Approved'," +
                //               "'" + values.auditorapproval_remark + "'," +
                //               "'" + lscreated_by + "'," +
                //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                //                  " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + lsobservation_percentage + "'" +
                //                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //    }
                //    if (mnResult != 0)
                //    {
                //        values.status = true;
                //        values.message = " Final Audit has been Approved Successfully..!";

                //        k = 1;

                //        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //        if (objODBCDatareader.HasRows == true)
                //        {
                //            ls_server = objODBCDatareader["pop_server"].ToString();
                //            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                //            ls_username = objODBCDatareader["pop_username"].ToString();
                //            ls_password = objODBCDatareader["pop_password"].ToString();
                //        }
                //        objODBCDatareader.Close();

                //        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,group_concat(d.auditeechecker_gid, ',', d.auditeemaker_gid)  as cc2members , a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                //        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                //                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                //            " left join atm_trn_tmultipleauditee d on d.auditcreation_gid = a.auditcreation_gid  " +
                //            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //        if (objODBCDatareader.HasRows == true)
                //        {
                //            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                //            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                //            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                //            lscreated_by = objODBCDatareader["auditchecker_name"].ToString();
                //            lscc2members = objODBCDatareader["cc2members"].ToString();
                //            lsto2members = objODBCDatareader["employee_gid"].ToString();
                //            lscreated_date = objODBCDatareader["created_date"].ToString();
                //            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                //        }
                //        objODBCDatareader.Close();

                //        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                //        sToken = "";
                //        int Length = 100;
                //        for (int j = 0; j < Length; j++)
                //        {
                //            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                //            sToken += sTempChars;
                //        }

                //        k = k + 1;


                //        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                //                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                //        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                //        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                //        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                //        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                // " from atm_trn_tauditcreation  " +
                //" where auditcreation_gid ='" + values.auditcreation_gid + "'";

                //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //        if (objODBCDatareader.HasRows == true)
                //        {
                //            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                //            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                //            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                //            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                //        }
                //        objODBCDatareader.Close();

                //        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                //          "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                //          "where b.employee_gid ='" + employee_gid + "'";
                //        employeename = objdbconn.GetExecuteScalar(msSQL);

                //        sub = "Final Audit has been Approved  ";
                //        body = "Dear All,<br />";
                //        body = body + "<br />";
                //        body = body + "Greetings,  <br />";
                //        body = body + "<br />";
                //        body = body + " The following audit has been approved, <br />";
                //        body = body + "<br />";
                //        body = body + "<b> Audit Name :</b> " + lsaudit_name + "<br />";
                //        body = body + "<br />";
                //        body = body + "<b>Audit Reference Number :</b> " + lsaaudit_uniqueno + "<br />";
                //        body = body + "<br />";
                //        body = body + "<b>Audit Department :</b> " + lsauditdepartment_name + "<br />";
                //        body = body + "<br />";
                //        body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                //        body = body + "<br />";

                //        body = body + "<br />";
                //        body = body + "Thanks & Regards, ";
                //        body = body + "<br />";
                //        body = body + employeename;
                //        body = body + "<br />";
                //        body = body + "<br />";
                //        body = body + "<br />";
                //        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                //        MailMessage message = new MailMessage();
                //        SmtpClient smtp = new SmtpClient();
                //        message.From = new MailAddress(ls_username);



                //        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

                //        if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                //        {
                //            lsBCCReceipients = lsBccmail_id.Split(',');
                //            if (lsBccmail_id.Length == 0)
                //            {
                //                message.Bcc.Add(new MailAddress(lsBccmail_id));
                //            }
                //            else
                //            {
                //                foreach (string BCCEmail in lsBCCReceipients)
                //                {
                //                    message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                //                }
                //            }
                //        }
                //        if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                //        {
                //            lsToReceipients = lsto_mail.Split(',');
                //            if (lsto_mail.Length == 0)
                //            {
                //                message.To.Add(new MailAddress(lsto_mail));
                //            }
                //            else
                //            {
                //                foreach (string ToEmail in lsToReceipients)
                //                {
                //                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                //                }
                //            }
                //        }

                //        if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                //        {
                //            lsCCReceipients = cc_mailid.Split(',');
                //            if (cc_mailid.Length == 0)
                //            {
                //                message.CC.Add(new MailAddress(cc_mailid));
                //            }
                //            else
                //            {
                //                foreach (string CCEmail in lsCCReceipients)
                //                {
                //                    message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                //                }
                //            }
                //        }

                //        message.Subject = sub;
                //        message.IsBodyHtml = true; //to make message body as html  
                //        message.Body = body;
                //        smtp.Port = ls_port;
                //        smtp.Host = ls_server; //for gmail host  
                //        smtp.EnableSsl = true;
                //        smtp.UseDefaultCredentials = false;
                //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //        smtp.Send(message);

                //        values.status = true;

                //        if (values.status == true)
                //        {
                //            msSQL = "Insert into atm_trn_tauditmailcount( " +
                //               " auditcreation_gid," +
                //               " from_mail," +
                //               " to_mail," +
                //               " cc_mail," +
                //               " mail_status," +
                //               " mail_senddate, " +
                //               " created_by," +
                //               " created_date)" +
                //               " values(" +
                //               "'" + values.auditcreation_gid + "'," +
                //               "'" + lsemployee_gid + "'," +
                //               "'" + lsto_mail + "'," +
                //               "'" + cc_mailid + "'," +
                //               "'Audit has been Approval'," +
                //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                //               "'" + lsemployee_gid + "'," +
                //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //        }
                //    }

                //}

                msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid,auditormakerapprover_flag from atm_trn_tauditcreation " +
                            " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                    lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                    lsauditormakerapprover_flag = objODBCDatareader["auditormakerapprover_flag"].ToString();

                }
                objODBCDatareader.Close();
                if (lsauditormakerapprover_flag == "Y")
                {

                    msSQL = " select a.auditcreation_gid,auditmapping_gid,a.auditmapping2employee_gid,d.multiauditeechecker_approvalflag,d.auditeechecker_approvalflag,a.auditapprover_name,a.observation_percentage,a.employee_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                            " FROM atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                            " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                             " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsinitiated_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditapproval_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                        lsapproval_name = objODBCDatareader["auditapprover_name"].ToString();
                        lsinitiated_name = objODBCDatareader["auditmaker_name"].ToString();
                        lsobservation_percentage = objODBCDatareader["observation_percentage"].ToString();
                        lscreated_by = objODBCDatareader["auditmapping_gid"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                        lsauditeechecker_approvalflag = objODBCDatareader["multiauditeechecker_approvalflag"].ToString();
                    }
                    objODBCDatareader.Close();
                    if (lsauditeechecker_approvalflag == "Y")
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                        msSQL = "Insert into atm_trn_tauditapproval( " +
                               " auditapproval_gid, " +
                               " initialapproval_gid, " +
                               " auditcreation_gid," +
                                " auditorapproval_gid, " +
                               " approval_name," +
                               " initiateapproval," +
                               " approval_status," +
                               " approve_remark," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + lsinitiated_gid + "'," +
                               "'" + lsauditcreation_gid + "'," +
                              "'" + lsauditapproval_gid + "'," +
                               "'" + lsapproval_name + "'," +
                                "'" + lsinitiated_name + "'," +
                               "' Auditor Approved'," +
                               "'" + values.auditorapproval_remark + "'," +
                               "'" + lscreated_by + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + lsobservation_percentage + "'" +
                                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                    }

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = " Final Audit has been Approved Successfully..!";

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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,group_concat(distinct d.auditeechecker_gid, ',', d.auditeemaker_gid)  as cc2members , a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditchecker_name"].ToString();
                            lscc2members = objODBCDatareader["cc2members"].ToString();
                            lsto2members = objODBCDatareader["employee_gid"].ToString();
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


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = "Audit has been Approved  ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " The following audit has been approved, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name )+ "<br />";
                        body = body + "<br />";
                        //body = body + "Kindly log into systems to Approve the Audit.";
                        //body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
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
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                    }

                }
                else
                {

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = " Auditor Checker has been Approved Successfully..!";
                        try
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid,a.auditmapping2employee_gid, group_concat( distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',', a.employee_gid, ',', a.auditmapping_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                lsemployee_name = objODBCDatareader["auditapprover_name"].ToString();
                                lsemployee_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                lsauditorchecker_name = objODBCDatareader["auditchecker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditchecker_name"].ToString();
                                lscc2members = objODBCDatareader["CC2members"].ToString();
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


                            msSQL = " select employee_emailid  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsemployee_gid.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);

                            sub = "RE: Auditor Checker Approved";
                            body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + " The following audit has been submitted for approval, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name )+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Observation Score.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsauditeechecker_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Auditor Checker Approved'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
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
                        values.message = "Error Occured..!";

                    }
                }

            }
            
        }
        public void DaPostAuditorApproval(initialapprovaldtl values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from atm_trn_tauditraisequery where auditcreation_gid = '" + values.auditcreation_gid + "'" +
                  " and auditraisequery_status = 'Open'";
            values.auditopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.auditopenquerycount != "0")
            {
                values.status = false;
                values.message = "Query Can't be Closed";
                return;
            }

            else
            { 
                msSQL = " select a.auditcreation_gid,a.auditmapping2employee_gid,a.auditapprover_name,a.employee_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                         " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
                 dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        var lsinitiated_gid = dt["employee_gid"].ToString();
                        var lsauditapproval_gid = dt["auditmapping2employee_gid"].ToString();
                        var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                        var lsapproval_name = dt["auditapprover_name"].ToString();
                        var lsinitiated_name = dt["auditmaker_name"].ToString();

                        msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                        msSQL = "Insert into atm_trn_tauditapproval( " +
                               " auditapproval_gid, " +
                               " initialapproval_gid, " +
                               " auditcreation_gid," +
                                " auditorapproval_gid, " +
                               " approval_name," +
                               " initiateapproval," +
                               " approval_status," +
                               " approve_remark," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGetGid + "'," +
                               "'" + lsinitiated_gid + "'," +
                               "'" + lsauditcreation_gid + "'," +
                              "'" + lsauditapproval_gid + "'," +
                               "'" + lsapproval_name + "'," +
                                "'" + lsinitiated_name + "'," +
                               "' Auditor Approved'," +
                               "'" + values.auditorapproval_remark + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable.Dispose();

                    if (mnResult != 0)
                    {
                        msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                                " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                               " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        values.status = true;
                        values.message = "Final Audit Approval Approved Successfully..!";


                        msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid,auditormakerchecker_flag from atm_trn_tauditcreation " +
                                                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                            lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                            lsauditormakerchecker_flag = objODBCDatareader["auditormakerchecker_flag"].ToString();

                        }
                        objODBCDatareader.Close();
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid,group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid)  as cc2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                         " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                                lscc2members = objODBCDatareader["cc2members"].ToString();
                                lsto2members = objODBCDatareader["auditmapping_gid"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                          "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                          "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);

                            sub = "Final Audit has been Approved  ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + " The following audit has been approved, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name )+ "<br />";
                            body = body + "<br />";
                            //body = body + "Kindly log into systems to Approve the Audit.";
                            //body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }


                        }
                        else
                        {
                            try
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

                                msSQL = " select  a.auditcreation_gid,  a.auditapprover_name, a.auditmapping2employee_gid,  group_concat(distinct a.employee_gid, ',', a.auditmapping_gid)  as Tomembers ,   group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, d.created_date from atm_trn_tauditcreation a" +
                                " left join atm_trn_tauditapproval d on a.auditcreation_gid = d.auditcreation_gid" +
                                     " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                             " left join atm_trn_tauditagainstmultipleauditeechecker e on e.auditcreation_gid = a.auditcreation_gid  " +
                                    " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditapprover_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                    lscreated_by = objODBCDatareader["created_by"].ToString();
                                    lstomembers = objODBCDatareader["Tomembers"].ToString();
                                    lscc2members = objODBCDatareader["CC2members"].ToString();
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


                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                         " from atm_trn_tauditcreation  " +
                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                          "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                          "where b.employee_gid ='" + employee_gid + "'";
                                employeename = objdbconn.GetExecuteScalar(msSQL);


                                sub = " RE: Audit Final Approval ";
                                body = "Dear All,  <br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "The following Audit is has been Approved for completion. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to view more details.";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "Thanks & Regards, ";
                                body = body + "<br />";
                                body = body + HttpUtility.HtmlEncode(employeename);
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                //message.To.Add(new MailAddress(lsto_mail));

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
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + lsemployee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Auditor Approver Approved'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + lscreated_by + "'," +
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
                }
                else
                {
                    dt_datatable.Dispose();
                }

            }
        }

        public void DaPostAuditorSampleApproval(initialapprovaldtl values, string employee_gid)
        {

            msSQL = " select a.auditcreation_gid,a.auditmapping2employee_gid,a.auditapprover_name,a.employee_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                         " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var lsinitiated_gid = dt["employee_gid"].ToString();
                    var lsauditapproval_gid = dt["auditmapping2employee_gid"].ToString();
                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                    var lsapproval_name = dt["auditapprover_name"].ToString();
                    var lsinitiated_name = dt["auditmaker_name"].ToString();

                    msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                    msSQL = "Insert into atm_trn_tauditapproval( " +
                           " auditapproval_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                            " auditorapproval_gid, " +
                           " approval_name," +
                           " initiateapproval," +
                           " approval_status," +
                           " approve_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                          "'" + lsauditapproval_gid + "'," +
                           "'" + lsapproval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "' Auditor Approved'," +
                           "'" + values.auditorapproval_remark + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable.Dispose();

                if (mnResult != 0)
                {
                    msSQL = " update atm_trn_tauditcreation set approval_status ='Completed', status ='Completed',auditorapprover_approvalflag='Y', approval_flag ='Y' " +
                            " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Final Audit Approval Approved Successfully..!";


                    msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid,auditormakerchecker_flag from atm_trn_tauditcreation " +
                                                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                        lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        lsauditormakerchecker_flag = objODBCDatareader["auditormakerchecker_flag"].ToString();

                    }
                    objODBCDatareader.Close();
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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,a.auditmaker_name,d.auditeechecker_name, a.auditmapping_gid,a.employee_gid,group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid)  as cc2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                     " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditeechecker_name"].ToString();
                            lscc2members = objODBCDatareader["cc2members"].ToString();
                            lsto2members = objODBCDatareader["auditmapping_gid"].ToString();
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


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = "Final Audit has been Approved  ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " The following audit has been approved, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        //body = body + "Kindly log into systems to Approve the Audit.";
                        //body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
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
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }


                    }
                    else
                    {
                        try
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

                            msSQL = " select  a.auditcreation_gid,  a.auditapprover_name, a.auditmapping2employee_gid,  group_concat(distinct a.employee_gid, ',', a.auditmapping_gid)  as Tomembers ,   group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, d.created_date from atm_trn_tauditcreation a" +
                            " left join atm_trn_tauditapproval d on a.auditcreation_gid = d.auditcreation_gid" +
                                 " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                         " left join atm_trn_tauditagainstmultipleauditeechecker e on e.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditapprover_name"].ToString();
                                lsemployee_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                                lscreated_by = objODBCDatareader["created_by"].ToString();
                                lstomembers = objODBCDatareader["Tomembers"].ToString();
                                lscc2members = objODBCDatareader["CC2members"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = " RE: Audit Final Approval ";
                            body = "Dear All,  <br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "The following Audit is has been Approved for completion. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to view more details.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(lsto_mail));

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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Auditor Approver Approved'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lscreated_by + "'," +
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
                    dt_datatable.Dispose();
                }

            }
        }

        public void DaPostAuditorRejected(initialapprovaldtl values, string employee_gid)
        {
            msSQL = " select a.auditcreation_gid,a.auditmapping2employee_gid,a.auditapprover_name,a.employee_gid,a.auditmaker_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                         " where a.auditcreation_gid='" + values.auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinitialapprovalview_list = new List<initialapprovalview_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    var lsinitiated_gid = dt["employee_gid"].ToString();
                    var lsauditapproval_gid = dt["auditmapping2employee_gid"].ToString();
                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
                    var lsapproval_name = dt["auditapprover_name"].ToString();
                    var lsinitiated_name = dt["auditmaker_name"].ToString();

                    msGetGid = objcmnfunctions.GetMasterGID("AUAP");

                    msSQL = "Insert into atm_trn_tauditapproval( " +
                           " auditapproval_gid, " +
                           " initialapproval_gid, " +
                           " auditcreation_gid," +
                            " auditorapproval_gid, " +
                           " approval_name," +
                           " initiateapproval," +
                           " approval_status," +
                           " approve_remark," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + lsinitiated_gid + "'," +
                           "'" + lsauditcreation_gid + "'," +
                          "'" + lsauditapproval_gid + "'," +
                           "'" + lsapproval_name + "'," +
                            "'" + lsinitiated_name + "'," +
                           "' Approval Rejected'," +
                           "'" + values.auditorapproval_remark + "'," +
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

                    msSQL = "update atm_trn_tauditcreation set approval_status ='Approval Rejected'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Approval Rejected Successfully ..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
                dt_datatable.Dispose();
            }
        }
        public void DaCheckerApprovalView(initialapprovaldtl values, string auditcreation_gid)
        {

            msSQL = " SELECT a.checkerapproval_gid,a.auditchecker_gid,a.initialapproval_gid,a.auditcreation_gid,a.initiateapproval,a.approval_name,a.approval_status,a.approval_remark," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " FROM atm_trn_tcheckerapproval a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.auditcreation_gid='" + auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCheckerapprovalview_list = new List<Checkerapprovalview_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getCheckerapprovalview_list.Add(new Checkerapprovalview_list
                    {
                        checkerapproval_gid = (dr_datarow["checkerapproval_gid"].ToString()),
                        auditchecker_gid = (dr_datarow["auditchecker_gid"].ToString()),
                        initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
                        auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                        initiateapproval = (dr_datarow["initiateapproval"].ToString()),
                        approval_name = (dr_datarow["approval_name"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approval_remark = (dr_datarow["approval_remark"].ToString()),

                    });
                }
                values.Checkerapprovalview_list = getCheckerapprovalview_list;
            }

            dt_datatable.Dispose();
        }
        public void DaAuditorApprovalView(initialapprovaldtl values, string auditcreation_gid)
        {

            msSQL = " SELECT a.auditapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_name,a.approval_status,a.approve_remark,a.initiateapproval,a.auditorapproval_gid," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " FROM atm_trn_tauditapproval a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.auditcreation_gid='" + auditcreation_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getAuditorapprovalview_list = new List<Auditorapprovalview_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getAuditorapprovalview_list.Add(new Auditorapprovalview_list
                    {
                        auditapproval_gid = (dr_datarow["auditapproval_gid"].ToString()),
                        initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
                        auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                        approval_name = (dr_datarow["approval_name"].ToString()),
                        initiateapproval = (dr_datarow["initiateapproval"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approve_remark = (dr_datarow["approve_remark"].ToString()),
                        auditorapproval_gid = (dr_datarow["auditorapproval_gid"].ToString()),


                    });
                }
                values.Auditorapprovalview_list = getAuditorapprovalview_list;
            }

            dt_datatable.Dispose();
        }


        public void DaPostAuditorMakerInitiateSample(MdlAtmTrnAuditorMaker values, string employee_gid)

        {


            msGetGid = objcmnfunctions.GetMasterGID("MIAP");
            msSQL = " insert into atm_trn_tmakerinitiateapproval (" +
                  " makerinitiateapproval_gid, " +
                  " auditcreation_gid," +
                  " makerinitiate_approvalflag," +
                  " created_by," +
                  " created_date) " +
                  " values (" +
                  " '" + msGetGid + "'," +
                  " '" + values.auditcreation_gid + "'," +
                  " '" + values.makerinitiate_approvalflag + "'," +
                  " '" + employee_gid + "'," +
                  " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update atm_trn_tauditcreation set makerapprovaloverall_flag ='Y',auditormakerobservationapprove_flag ='Y',samplestatus_flag ='N'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update atm_trn_tauditagainstmultipleauditeechecker set auditeechecker_approvalstatus='Pending'" +
                         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                                  " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();

            }
            objODBCDatareader.Close();
            if (lsauditmaker_gid == lsauditapprover_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditchecker_gid)
            {
                if (mnResult != 0)
                {
                    msSQL = " update atm_trn_tauditcreation set auditorchecker_approvalflag='Y', " +
                   " auditorcheckerinitiated_by ='" + employee_gid + "', " +
                       " auditorcheckerinitiated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y',auditorcommonname_flag='Y' " +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Audit has been Approved Successfully..!";
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

                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,d.auditeechecker_gid, a.auditmaker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                  " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                        lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                        lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                        lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                        lsto2members = objODBCDatareader["auditeechecker_gid"].ToString();
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


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
             " from atm_trn_tauditcreation  " +
            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = "Audit Approval Request  ";
                    body = "Dear Auditee Checker,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Kindly Approve for the Audit, <br />";
                    body = body + "<br />";
                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name) + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to Approve the Audit.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employeename);
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
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.auditcreation_gid + "'," +
                           "'" + lsemployee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Audit has been Approval'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + lsemployee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
            }
            else
            {


                msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                if (lsauditormakerchecker_flag == "Y")

                {
                    if (mnResult != 0)
                    {
                        msSQL = " update atm_trn_tauditcreation set auditorchecker_approvalflag='Y', " +
                       " auditorcheckerinitiated_by ='" + employee_gid + "', " +
                           " auditorcheckerinitiated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y'" +
                               " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                               " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        values.status = true;
                        values.message = "Proceed to Checker Approval Initiated Successfully..!";
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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeechecker_gid, a.auditmaker_name, a.auditmapping_gid,a.employee_gid,group_concat(distinct a.auditmapping2employee_gid, ',', d.auditeemaker_gid)  as cc2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                      " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                            lscc2members = objODBCDatareader["cc2members"].ToString();
                            lsto2members = objODBCDatareader["auditeechecker_gid"].ToString();
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


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = "Audit Observation Approval Request  ";
                        body = "Dear Auditee Checker,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Kindly Approve the Observation Score provided for the Audit, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to Approve the Audit.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
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
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }

                else
                {

                    msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                    lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                    if (lsauditormakerapprover_flag == "Y")
                    {


                        if (mnResult != 0)
                        {

                            msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y' " +
                                    " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                   " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            values.status = true;
                            values.message = "Proceed to Checker Approval Initiated Successfully..!";
                        }

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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.employee_gid,group_concat(distinct a.auditmapping_gid, ',', d.auditeechecker_gid)  as to2members ,d.auditeemaker_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                         " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                            lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
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


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);


                        sub = "Audit Approval Request  ";
                        body = "Dear Auditee Checker/Auditor Checker,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Kindly Approve for the Audit, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode (lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to Approve the Audit.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
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
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
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
                            msSQL = " update atm_trn_tauditcreation set auditorcheckerapprover_flag='Y' " +
                                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {

                                msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y' " +
                                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Proceed to Checker Approval Initiated Successfully..!";
                            }

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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid, a.auditmaker_name,group_concat(distinct a.auditmapping2employee_gid, ',', d.auditeechecker_gid)  as to2members ,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                        " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                                lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = "Audit Approval Request  ";
                            body = "Dear Auditee Checker/Auditor Checker,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "Kindly Approve for the Audit, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Audit.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }


                        else
                        {

                            if (mnResult != 0)
                            {

                                msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y' " +
                                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Proceed to Checker Approval Initiated Successfully..!";

                                try
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

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid,a.employee_gid, group_concat(distinct a.auditmapping2employee_gid)  as CC2members ,group_concat(distinct a.auditmapping_gid, ',', d.auditeechecker_gid)  as to2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                 " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                                        lscc2members = objODBCDatareader["CC2members"].ToString();
                                        lsto2members = objODBCDatareader["to2members"].ToString();
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


                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                             " from atm_trn_tauditcreation  " +
                            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                                    employeename = objdbconn.GetExecuteScalar(msSQL);


                                    sub = "Audit Observation Approval Request  ";
                                    body = "Dear Auditee Checker / Auditor Checker,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "Kindly Approve the Observation Score provided for the Audit, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to Approve the Observation Score.";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "Thanks & Regards, ";
                                    body = body + "<br />";
                                    body = body + HttpUtility.HtmlEncode(employeename);
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
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + lsemployee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Auditor Maker Initiated Approval'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + lsemployee_gid + "'," +
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
                                values.message = "Error Occured..!";
                            }
                        }
                    }
                }
            }

        }

        public void DaPostMakerInitiateApproval(MdlAtmTrnAuditorMaker values, string employee_gid)

        {

            msGetGid = objcmnfunctions.GetMasterGID("MIAP");
            msSQL = " insert into atm_trn_tmakerinitiateapproval (" +
                  " makerinitiateapproval_gid, " +
                  " auditcreation_gid," +
                  " observation_percentage," +
                  " makerinitiate_approvalflag," +
                  " created_by," +
                  " created_date) " +
                  " values (" +
                  " '" + msGetGid + "'," +
                  " '" + values.auditcreation_gid + "'," +
                  " '" + values.observation_percentage + "'," +
                  " '" + values.makerinitiate_approvalflag + "'," +
                  " '" + employee_gid + "'," +
                  " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update atm_trn_tauditcreation set makerapprovaloverall_flag ='Y',auditormakerobservationapprove_flag ='Y',samplestatus_flag ='N'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update atm_trn_tauditagainstmultipleauditeechecker set auditeechecker_approvalstatus='Pending'" +
                          " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                                  " where auditcreation_gid = '" + values.auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();

            }
            objODBCDatareader.Close();
            if (lsauditmaker_gid == lsauditapprover_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditchecker_gid)
            {
                if (mnResult != 0)
                {
                    msSQL = " update atm_trn_tauditcreation set auditorchecker_approvalflag='Y', " +
                   " auditorcheckerinitiated_by ='" + employee_gid + "', " +
                       " auditorcheckerinitiated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y',auditorcommonname_flag='Y' " +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Audit has been Approved Successfully..!";
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

                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,d.auditeechecker_gid, a.auditmaker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                  " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                        lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                        lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                        lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                        lsto2members = objODBCDatareader["auditeechecker_gid"].ToString();
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


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
             " from atm_trn_tauditcreation  " +
            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                    employeename = objdbconn.GetExecuteScalar(msSQL);


                    sub = "Audit Approval Request  ";
                    body = "Dear Auditee Checker,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Kindly Approve for the Audit, <br />";
                    body = body + "<br />";
                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to Approve the Audit.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employeename);
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
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.auditcreation_gid + "'," +
                           "'" + lsemployee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Audit has been Approval'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + lsemployee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
            }
            else
            {


                msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                if (lsauditormakerchecker_flag == "Y")

                {
                    if (mnResult != 0)
                    {
                        msSQL = " update atm_trn_tauditcreation set auditorchecker_approvalflag='Y', " +
                       " auditorcheckerinitiated_by ='" + employee_gid + "', " +
                           " auditorcheckerinitiated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y'" +
                               " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                               " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        values.status = true;
                        values.message = "Proceed to Checker Approval Initiated Successfully..!";
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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeechecker_gid, a.auditmaker_name, a.auditmapping_gid,a.employee_gid,group_concat(a.auditmapping2employee_gid, ',', d.auditeemaker_gid)  as cc2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                      " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                            lscc2members = objODBCDatareader["cc2members"].ToString();
                            lsto2members = objODBCDatareader["auditeechecker_gid"].ToString();
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


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = "Audit Observation Approval Request  ";
                        body = "Dear Auditee Checker,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Kindly Approve the Observation Score provided for the Audit, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to Approve the Audit.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
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
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                }

                else
                {

                    msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                    lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                    if (lsauditormakerapprover_flag == "Y")
                    {


                        if (mnResult != 0)
                        {

                            msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y' " +
                                    " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                   " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            values.status = true;
                            values.message = "Proceed to Checker Approval Initiated Successfully..!";
                        }

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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.employee_gid,group_concat(a.auditmapping_gid, ',', d.auditeechecker_gid)  as to2members ,d.auditeemaker_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                         " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                            lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                            lsto2members = objODBCDatareader["to2members"].ToString();
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


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);


                        sub = "Audit Approval Request  ";
                        body = "Dear Auditee Checker/Auditor Checker,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Kindly Approve for the Audit, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " +HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to Approve the Audit.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
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
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
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
                            msSQL = " update atm_trn_tauditcreation set auditorcheckerapprover_flag='Y' " +
                                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult != 0)
                            {

                                msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y' " +
                                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Proceed to Checker Approval Initiated Successfully..!";
                            }

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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid, a.auditmaker_name,group_concat(a.auditmapping2employee_gid, ',', d.auditeechecker_gid)  as to2members ,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                        " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                                lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                lsto2members = objODBCDatareader["to2members"].ToString();
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


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                            employeename = objdbconn.GetExecuteScalar(msSQL);


                            sub = "Audit Approval Request  ";
                            body = "Dear Auditee Checker/Auditor Checker,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "Kindly Approve for the Audit, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to Approve the Audit.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(employeename);
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
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }


                        else
                        {

                            if (mnResult != 0)
                            {

                                msSQL = " update atm_trn_tauditcreation set approval_status ='Checker Approval pending', auditormaker_approvalflag='Y' " +
                                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                msSQL = " update atm_trn_tauditcreation2checklist set observation_percentage ='" + values.observation_percentage + "'" +
                                       " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                values.status = true;
                                values.message = "Proceed to Checker Approval Initiated Successfully..!";

                                try
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

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid,a.employee_gid, group_concat(a.auditmapping2employee_gid)  as CC2members ,group_concat(a.auditmapping_gid, ',', d.auditeechecker_gid)  as to2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                 " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                                        lscc2members = objODBCDatareader["CC2members"].ToString();
                                        lsto2members = objODBCDatareader["to2members"].ToString();
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


                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                             " from atm_trn_tauditcreation  " +
                            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                                    employeename = objdbconn.GetExecuteScalar(msSQL);


                                    sub = "Audit Observation Approval Request  ";
                                    body = "Dear Auditee Checker / Auditor Checker,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "Kindly Approve the Observation Score provided for the Audit, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to Approve the Observation Score.";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "Thanks & Regards, ";
                                    body = body + "<br />";
                                    body = body + HttpUtility.HtmlEncode(employeename);
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
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + lsemployee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Auditor Maker Initiated Approval'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + lsemployee_gid + "'," +
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
                                values.message = "Error Occured..!";
                            }
                        }
                    }
                }
            }

        }
        public void DaGetMakerInitiateStatus(string auditcreation_gid, MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            values.employee_gid = employee_gid;

            msSQL = "select status_flag,approval_flag ,status from atm_trn_tauditcreation " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.makerstatus_flag = objODBCDatareader["status_flag"].ToString();
                values.makerapproval_flag = objODBCDatareader["approval_flag"].ToString();
                values.maker_initiated = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetStatusRemarks(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
        {
            msSQL = " select status_remarks  from atm_trn_tauditcreation " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status_remarks = objODBCDatareader["status_remarks"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaGetSampleResponseQuery(string auditcreation_gid, MdlAtmTrnAuditorMaker values)
        {
            msSQL = "select approval_status from atm_trn_tauditcreation " +
                  " where auditcreation_gid='" + auditcreation_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.approval_status = objODBCDatareader["approval_status"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void Daauditobservatioremarks(MdlAtmTrnAuditorMaker values, string employee_gid)
        {

            msSQL = " select auditcreation_gid,auditmapping_gid,employee_gid,auditmapping2employee_gid " +
                    " FROM atm_trn_tauditcreation " +
                    " where auditcreation_gid='" + values.auditcreation_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lschecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                lsmaker_gid = objODBCDatareader["employee_gid"].ToString();
                lsapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();

            }
            objODBCDatareader.Close();
            if (lschecker_gid == employee_gid)
                lsauditor = "Auditor Checker";
            else if (lsmaker_gid == employee_gid)
                lsauditor = "Auditor Maker";
            else if (lsapprover_gid == employee_gid)
                lsauditor = "Auditor Approver";

            msSQL = " update atm_trn_tauditcreation2checklist set observation_remarks='" + values.observation_remarks.Replace("'", "") + "'" +
                    " where auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AORL");

                msSQL = " insert into atm_trn_tobservationremarks_log (" +
                      " observationremarkslog_gid, " +
                      " auditcreation2checklist_gid," +
                      " auditcreation_gid," +
                      " observation_remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditcreation2checklist_gid + "'," +
                      " '" + values.auditcreation_gid + "'," +
                      " '" + values.observation_remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Observation Remarks Updated Successfully";

                msSQL = " update atm_trn_tobservationremarks_log set auditor_details='" + lsauditor + "'" +
                   " where observationremarkslog_gid='" + msGetGid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void Daauditobservatioremarksview(string auditcreation2checklist_gid, MdlAtmTrnAuditorMaker values)
        {
            try
            {
                msSQL = " SELECT a.observationremarkslog_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " a.observation_remarks, a.auditor_details" +
                        " FROM atm_trn_tobservationremarks_log a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.auditcreation2checklist_gid ='" + auditcreation2checklist_gid + "' order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmyauditormaker_list = new List<myauditormaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmyauditormaker_list.Add(new myauditormaker_list
                        {
                            observationremarkslog_gid = (dr_datarow["observationremarkslog_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            observation_remarks = (dr_datarow["observation_remarks"].ToString()),
                            auditor_details = (dr_datarow["auditor_details"].ToString()),
                        });
                    }
                    values.myauditormaker_list = getmyauditormaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaPostAuditorSampleObservationTotalAmount(makercheckpointobservationadd values, string employee_gid)
        {


            msSQL = "select sample2checkpoint,auditcreation_gid,checklist_name,checklist_verified,checklistverified_flag,checkpointgroupadd_gid from atm_trn_tsample2checkpoint where " +
                 " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "'and sampleimport_gid is null";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count == 0)
            {
                string lsyes_score = "", lsno_score = "", lspartial_score = "", lsna_score = "";
                msSQL = " select observationscoresample_gid,yes_score,no_score,partial_score,na_score from atm_trn_tobservationscoresample " +
                           " where observationscoresample_gid = '" + values.observationscoresample_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsyes_score = objODBCDatareader["yes_score"].ToString();
                    lsno_score = objODBCDatareader["no_score"].ToString();
                    lspartial_score = objODBCDatareader["partial_score"].ToString();
                    lsna_score = objODBCDatareader["na_score"].ToString();

                }
                objODBCDatareader.Close();


                string getscore = values.capture_score == "Yes" ? lsyes_score : values.capture_score == "No" ? lsno_score : values.capture_score == "Partial" ? lspartial_score : lsna_score;

                msSQL = " update atm_trn_tobservationscoresample set samplecapture_field='" + values.capture_score + "', samplecapture_score='" + getscore + "' " +
                       " where observationscoresample_gid = '" + values.observationscoresample_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select sum(samplecapture_score) as total_amount from atm_trn_tobservationscoresample  where auditcreation_gid ='" + values.auditcreation_gid + "' and sampleimport_gid is null";
                values.total_amount = objdbconn.GetExecuteScalar(msSQL);

                var convertDecimal1 = Convert.ToDecimal(values.total_amount);

                Decimal val1 = Decimal.Truncate(convertDecimal1);

                msSQL = " select sum(yes_score)as overall_score from atm_trn_tobservationscoresample  where auditcreation_gid ='" + values.auditcreation_gid + "'";
                values.overall_score = objdbconn.GetExecuteScalar(msSQL);

                var convertDecimal2 = Convert.ToDecimal(values.overall_score);

                Decimal val2 = Decimal.Truncate(convertDecimal2);

                double observation_percentage = Math.Round((double)((val1 / val2) * 100), 2);

                msSQL = " update atm_trn_tobservationscoresample set status_flag ='Y',sampleobservation_score='" + values.total_amount + "',sampleobservation_percentage = '" + observation_percentage.ToString() + "' " +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tobservationscoresample set sampleoverall_score='" + values.overall_score + "' " +
                      " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Check Point Observation Score Updated Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
                msSQL = "select count(*) from atm_trn_tobservationscoresample where auditcreation_gid = '" + values.auditcreation_gid + "' and samplecapture_score is null";
                string lscount = objdbconn.GetExecuteScalar(msSQL);
                if (lscount == "0")
                    values.allobservationfilled = true;
                else
                    values.allobservationfilled = false;
            }
            else
            {



                msSQL = "select checklist_verified from atm_trn_tsample2checkpoint where auditcreation2checklist_gid ='" + values.auditcreation2checklist_gid + "' and sampleimport_gid='" + values.sampleimport_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Select Atleast One Checkpoint";
                    return;
                }


                string lsyes_score = "", lsno_score = "", lspartial_score = "", lsna_score = "";
                msSQL = " select observationscoresample_gid,yes_score,no_score,partial_score,na_score from atm_trn_tobservationscoresample " +
                           " where observationscoresample_gid = '" + values.observationscoresample_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsyes_score = objODBCDatareader["yes_score"].ToString();
                    lsno_score = objODBCDatareader["no_score"].ToString();
                    lspartial_score = objODBCDatareader["partial_score"].ToString();
                    lsna_score = objODBCDatareader["na_score"].ToString();

                }
                objODBCDatareader.Close();


                string getscore = values.capture_score == "Yes" ? lsyes_score : values.capture_score == "No" ? lsno_score : values.capture_score == "Partial" ? lspartial_score : lsna_score;

                msSQL = " update atm_trn_tobservationscoresample set samplecapture_field='" + values.capture_score + "', samplecapture_score='" + getscore + "' " +
                       " where observationscoresample_gid = '" + values.observationscoresample_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select sum(samplecapture_score) as total_amount from atm_trn_tobservationscoresample  where auditcreation_gid ='" + values.auditcreation_gid + "' and sampleimport_gid is null";
                values.total_amount = objdbconn.GetExecuteScalar(msSQL);

                var convertDecimal1 = Convert.ToDecimal(values.total_amount);

                Decimal val1 = Decimal.Truncate(convertDecimal1);

                msSQL = " select sum(yes_score)as overall_score from atm_trn_tobservationscoresample  where auditcreation_gid ='" + values.auditcreation_gid + "'";
                values.overall_score = objdbconn.GetExecuteScalar(msSQL);

                var convertDecimal2 = Convert.ToDecimal(values.overall_score);

                Decimal val2 = Decimal.Truncate(convertDecimal2);

                double observation_percentage = Math.Round((double)((val1 / val2) * 100), 2);

                msSQL = " update atm_trn_tobservationscoresample set status_flag ='Y',sampleobservation_score='" + values.total_amount + "',sampleobservation_percentage = '" + observation_percentage.ToString() + "' " +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tobservationscoresample set sampleoverall_score='" + values.overall_score + "' " +
                      " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Check Point Observation Score Updated Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
                msSQL = "select count(*) from atm_trn_tobservationscoresample where auditcreation_gid = '" + values.auditcreation_gid + "' and samplecapture_score is null";
                string lscount = objdbconn.GetExecuteScalar(msSQL);
                if (lscount == "0")
                    values.allobservationfilled = true;
                else
                    values.allobservationfilled = false;
            }
        }
        public void DaPostAuditorUpdateSampleObservationTotalAmount(makercheckpointobservationadd values, string employee_gid)
        {



            string lsyes_score = "", lsno_score = "", lspartial_score = "", lsna_score = "";
            msSQL = " select observationscoresample_gid,yes_score,no_score,partial_score,na_score from atm_trn_tobservationscoresample " +
                       " where observationscoresample_gid = '" + values.observationscoresample_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsyes_score = objODBCDatareader["yes_score"].ToString();
                lsno_score = objODBCDatareader["no_score"].ToString();
                lspartial_score = objODBCDatareader["partial_score"].ToString();
                lsna_score = objODBCDatareader["na_score"].ToString();

            }
            objODBCDatareader.Close();


            string getscore = values.capture_score == "Yes" ? lsyes_score : values.capture_score == "No" ? lsno_score : values.capture_score == "Partial" ? lspartial_score : lsna_score;

            msSQL = " update atm_trn_tobservationscoresample set samplecapture_field='" + values.capture_score + "', samplecapture_score='" + getscore + "' " +
                   " where observationscoresample_gid = '" + values.observationscoresample_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select sum(samplecapture_score) as total_amount from atm_trn_tobservationscoresample  where auditcreation_gid ='" + values.auditcreation_gid + "' and sampleimport_gid ='" + values.sampleimport_gid + "'";
            values.total_amount = objdbconn.GetExecuteScalar(msSQL);

            var convertDecimal1 = Convert.ToDecimal(values.total_amount);

            Decimal val1 = Decimal.Truncate(convertDecimal1);

            msSQL = " select sum(no_score + yes_score + na_score + partial_score)as overall_score from atm_trn_tobservationscoresample  where auditcreation_gid ='" + values.auditcreation_gid + "'";
            values.overall_score = objdbconn.GetExecuteScalar(msSQL);

            var convertDecimal2 = Convert.ToDecimal(values.overall_score);

            Decimal val2 = Decimal.Truncate(convertDecimal2);

            double observation_percentage = Math.Round((double)((val1 / val2) * 100), 2);

            msSQL = " update atm_trn_tobservationscoresample set status_flag ='Y',sampleobservation_score='" + values.total_amount + "',sampleobservation_percentage = '" + observation_percentage.ToString() + "' " +
                    " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid = '" + values.sampleimport_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update atm_trn_tobservationscoresample set sampleoverall_score='" + values.overall_score + "' " +
                  " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is null";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Check Point Observation Score Updated Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
            msSQL = "select count(*) from atm_trn_tobservationscoresample where auditcreation_gid = '" + values.auditcreation_gid + "' and samplecapture_score is null";
            string lscount = objdbconn.GetExecuteScalar(msSQL);
            if (lscount == "0")
                values.allobservationfilled = true;
            else
                values.allobservationfilled = false;
        }
        public void DaPostMakerSampleInitiateApproval(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("MIAP");
            msSQL = " insert into atm_trn_tmakersampleinitiateapproval (" +
                  " makersampleinitiateapproval_gid, " +
                  " auditcreation_gid," +
                  " sampleobservation_percentage," +
                  " makerinitiate_approvalflag," +
                  " created_by," +
                  " created_date) " +
                  " values (" +
                  " '" + msGetGid + "'," +
                  " '" + values.auditcreation_gid + "'," +
                  " '" + values.observation_percentage + "'," +
                  " '" + values.makerinitiate_approvalflag + "'," +
                  " '" + employee_gid + "'," +
                  " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Check Point Observation Score Added Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }
        public void DaAuditorSampleView(string auditcreation_gid, string sampleimport_gid, MdlAtmTrnAuditorMaker values)
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
                          " where a.auditcreation_gid='" + auditcreation_gid + "' and a.sampleimport_gid is null";
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
                                             " where a.auditcreation_gid ='" + auditcreation_gid + "' and a.sampleimport_gid='" + sampleimport_gid + "'";
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
        public void DaGetAuditorSampleViewOverallscore(string auditcreation_gid, string sampleimport_gid, MdlAtmTrnAuditorMaker values)
        {
            msSQL = " select observationscoresample_gid from atm_trn_tobservationscoresample where " +
                         " sampleimport_gid='" + sampleimport_gid + "'";
            values.sampleimport_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.sampleimport_gid == "")
            {

                msSQL = " select sum(yes_score) as overall_score from atm_trn_tobservationscoresample " +
                    " where auditcreation_gid ='" + auditcreation_gid + "' and sampleimport_gid is null";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overall_score = objODBCDatareader["overall_score"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = " update atm_trn_tobservationscoresample set sampleoverall_score='" + values.overall_score + "' " +
                          " where auditcreation_gid ='" + auditcreation_gid + "' and sampleimport_gid is null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " select sum(yes_score) as overall_score from atm_trn_tobservationscoresample " +
                   " where auditcreation_gid ='" + auditcreation_gid + "' and sampleimport_gid='" + sampleimport_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overall_score = objODBCDatareader["overall_score"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = " update atm_trn_tobservationscoresample set sampleoverall_score='" + values.overall_score + "' " +
                          " where auditcreation_gid ='" + auditcreation_gid + "' and sampleimport_gid='" + sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
        }

        public void DaPostCheckpointAgainstObservation(MdlAtmTrnAuditorMaker values, string employee_gid)
        {

            msSQL = " update atm_trn_tchecklist2checkpoint set " +
                 " auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "'," +
                 //" auditcreation_gid='" + values.auditcreation_gid + "'," +
                 " overall_detail='" + values.document_verified + "'," +
                 " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where checklist2checkpoint='" + values.checklist2checkpoint + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = " Checkpoint Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }
        public void DaPostSampleCheckpointAgainstObservation(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            msSQL = " update atm_trn_tsample2checkpoint set " +
                 " auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "'," +
              " auditcreation_gid='" + values.auditcreation_gid + "'," +
                 " checklist_verified='" + values.document_verified + "'," +
                 " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where sample2checkpoint='" + values.sample2checkpoint + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = " Checkpoint Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        //public void DaPostCheckpointObservation(MdlAtmTrnAuditorMaker values, string employee_gid)
        //{

        //    msSQL = " update atm_trn_tchecklist2checkpoint set " +
        //         " checklistverified_flag='Y'," +
        //          " auditcreation_gid='" + values.auditcreation_gid + "'," +
        //         " created_by='" + employee_gid + "'," +
        //         " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        //         " where checkpointgroupadd_gid='" + values.checkpointgroupadd_gid + "' ";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    if (mnResult != 0)
        //    {
        //        msSQL = " update atm_trn_tchecklist2checkpoint set checklistverified_flag ='Y'" +
        //           " where checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "'";
        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        values.status = true;
        //        values.message = " Checkpoint Updated Successfully";
        //    }
        //    else
        //    {
        //        values.status = false;
        //        values.message = "Error Occurred While Updating";
        //    }
        //}
        public void DaPostCheckpointObservation(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            msSQL = "select overall_detail from atm_trn_tchecklist2checkpoint " +
           " where checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "' and auditcreation_gid is null";
            //msSQL = "select count(*) as overall_detail from atm_trn_tchecklist2checkpoint where checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and auditcreation_gid is null and overall_detail = ''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            int null_count = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsoverall_detail = dt["overall_detail"].ToString();
                    if (string.IsNullOrEmpty(lsoverall_detail))
                    {
                        null_count = null_count + 1;
                    }
                }

            }
            else
            {
                values.status = false;
                values.message = " Select Atleast One Checkpoint";
                return;

            }
            int dt_count = dt_datatable.Rows.Count;
            if (dt_count == null_count)
            {
                values.status = false;
                values.message = "Select Atleast One Checkpoint";
                return;

            }
            //else
            //{
            //    values.status = false;
            //    values.message = " Atleast One Checkpoint Selected";
            //    return;
            //}

            //values.opencount = objdbconn.GetExecuteScalar(msSQL);
            //if (values.opencount != "0")
            //{
            //    values.status = false;
            //    values.message = " Atleast One Checkpoint Selected";
            //    return;
            //}

            msSQL = "select checklist2checkpoint,checklist_name,auditcreation2checklist_gid,overall_detail,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint " +
            " where checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "' and auditcreation_gid is null";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("C2CP");

                    msSQL = " insert into atm_trn_tchecklist2checkpoint(" +
                           " checklist2checkpoint, " +
                            " checkpointgroupadd_gid," +
                            " auditcreation2checklist_gid," +
                            " auditcreation_gid," +
                            " checklist_name ," +
                              " overall_detail ," +
                              " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                             "'" + dt["auditcreation2checklist_gid"].ToString() + "'," +
                             "'" + values.auditcreation_gid + "'," +
                            "'" + dt["checklist_name"].ToString() + "'," +
                              "'" + dt["overall_detail"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            //}
            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tchecklist2checkpoint set overall_detail ='',auditcreation2checklist_gid = ''" +
                      " where auditcreation_gid is Null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tchecklist2checkpoint set checklistverified_flag ='Y'" +
                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Observation Checkpoint Added Successfully..!";

            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
            }
        }

        public void DaPostCheckpointObservationUpdate(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            //string[] checkpointgroupadd_gid_array = values.checkpointgroupadd_gid.Split(',');

            //int array_length = checkpointgroupadd_gid_array.Length;
            //string sample_querystring = "";
            //for (int i = 0; i < checkpointgroupadd_gid_array.Length; i++)
            //{
            //    if (i == checkpointgroupadd_gid_array.Length - 1)
            //    {
            //        sample_querystring = sample_querystring + "'" + checkpointgroupadd_gid_array[i] + "'";
            //    }
            //    else
            //    {
            //        sample_querystring = sample_querystring + "'" + checkpointgroupadd_gid_array[i] + "',";
            //    }
            //}

            msSQL = " update atm_trn_tchecklist2checkpoint set " +
                 //" checklistverified_flag='Y'," +
                 //" auditcreation_gid='" + values.auditcreation_gid + "'," +
                 " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where checkpointgroupadd_gid='" + values.checkpointgroupadd_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = " Checkpoint Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaGetSampleToCheckpoint(string checkpointgroupadd_gid, string sampleimport_gid, checklistcheckpoint values)
        {
            try
            {
                msSQL = " select sampleimport_gid from atm_trn_tsample2checkpoint where " +
                  " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and sampleimport_gid ='" + sampleimport_gid + "'";
                values.sampleimport_gid = objdbconn.GetExecuteScalar(msSQL);
                if (values.sampleimport_gid == "")
                {
                    msSQL = "select sample2checkpoint,checklist_name,checklist_verified,checkpointgroupadd_gid from atm_trn_tsample2checkpoint where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and sampleimport_gid is null ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                sample2checkpoint = (dr_datarow["sample2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                checklist_verified = (dr_datarow["checklist_verified"].ToString()),

                            });
                        }
                        values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                else
                {
                    msSQL = "select sample2checkpoint,checklist_name,checklist_verified,checkpointgroupadd_gid,samplechecklistverified_flag from atm_trn_tsample2checkpoint where " +
                  " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and sampleimport_gid ='" + sampleimport_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                sample2checkpoint = (dr_datarow["sample2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                checklist_verified = (dr_datarow["checklist_verified"].ToString()),
                                samplechecklistverified_flag = (dr_datarow["samplechecklistverified_flag"].ToString()),

                            });
                        }
                        values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaPostSampleCheckpointAssign(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            msSQL = "select checklist_verified,checkpointgroupadd_gid from atm_trn_tsample2checkpoint " +
               " where checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "' and sampleimport_gid is null";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            int null_count = 0;
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lsoverall_detail = dt["checklist_verified"].ToString();
                    if (string.IsNullOrEmpty(lsoverall_detail))
                    {
                        null_count = null_count + 1;
                    }
                }

            }
            else
            {
                values.status = false;
                values.message = " Select Atleast One Checkpoint";
                return;

            }
            int dt_count = dt_datatable.Rows.Count;
            if (dt_count == null_count)
            {
                values.status = false;
                values.message = " Select Atleast One Checkpoint";
                return;

            }


            msSQL = "select sample2checkpoint,checklist_name,sampleimport_gid,auditcreation2checklist_gid,checklist_verified,checkpointgroupadd_gid from atm_trn_tsample2checkpoint " +
                " where checkpointgroupadd_gid = '" + values.checkpointgroupadd_gid + "' and sampleimport_gid is null";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    //string lscheckpointgroupadd_gid = (dt["checkpointgroupadd_gid"].ToString());

                    msGetGid = objcmnfunctions.GetMasterGID("S2CP");

                    msSQL = " insert into atm_trn_tsample2checkpoint(" +
                           " sample2checkpoint, " +
                            " checkpointgroupadd_gid," +
                            " auditcreation2checklist_gid," +
                            " sampleimport_gid," +
                            " auditcreation_gid," +
                            " checklist_name ," +
                              " checklist_verified ," +
                              " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                             "'" + dt["auditcreation2checklist_gid"].ToString() + "'," +
                              "'" + values.sampleimport_gid + "'," +
                             "'" + values.auditcreation_gid + "'," +
                            "'" + dt["checklist_name"].ToString() + "'," +
                              "'" + dt["checklist_verified"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            //}
            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tsample2checkpoint set checklist_verified ='',auditcreation2checklist_gid = 'Null' " +
                      " where sampleimport_gid is Null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tsample2checkpoint set checklistverified_flag ='Y'" +
                     " where sampleimport_gid = '" + values.sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tauditcreation set samplestatus_flag ='Y'" +
                    " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " update atm_trn_tsample2checkpoint set samplechecklistverified_flag ='Y'" +
                    " where sampleimport_gid = '" + values.sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Submitted to Sample Checkpoint Successfully..!";

            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
            }
        }
        public void DaPostSampleCheckpointAssignUpdate(auditchecklistassign values, string employee_gid)
        {

            msSQL = " update atm_trn_tsample2checkpoint set " +
                 //" samplechecklistverified_flag='Y'," +
                 // " auditcreation_gid='" + values.auditcreation_gid + "'," +
                 //" sampleimport_gid='" + values.sampleimport_gid + "'," +
                 " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where checkpointgroupadd_gid='" + values.checkpointgroupadd_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = " Checkpoint Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        //public void DaPostSampleCheckpointAssignUpdate(auditchecklistassign values, string employee_gid)
        //{

        //    msSQL = "select sample2checkpoint,checklist_name,auditcreation_gid,sampleimport_gid,auditcreation2checklist_gid,checklist_verified,checkpointgroupadd_gid from atm_trn_tsample2checkpoint " +
        //    " where auditcreation_gid ='" + values.auditcreation_gid + "' and sampleimport_gid ='" + values.sampleimport_gid + "'";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);

        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dt in dt_datatable.Rows)
        //        {


        //            msSQL = " update atm_trn_tsample2checkpoint set " +
        //                " checkpointgroupadd_gid ='" + dt["checkpointgroupadd_gid"].ToString() + "', " +
        //                     " auditcreation2checklist_gid ='" + dt["auditcreation2checklist_gid"].ToString() + "', " +
        //                      " auditcreation_gid ='" + dt["auditcreation_gid"].ToString() + "', " +
        //                     " checklist_name ='" + dt["checklist_name"].ToString() + "', " +
        //                     " checklist_verified ='" + dt["checklist_verified"].ToString() + "', " +
        //                     " created_by='" + employee_gid + "'," +
        //                     " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        //                    " where sampleimport_gid='" + values.sampleimport_gid + "' ";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }
        //    }

        //    if (mnResult != 0)
        //    {
        //        //msSQL = " update atm_trn_tsample2checkpoint set checklist_verified ='Null'" +
        //        //      " where sampleimport_gid is Null";
        //        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        //msSQL = " update atm_trn_tsample2checkpoint set checklistverified_flag ='Y'" +
        //        //     " where sampleimport_gid = '" + values.sampleimport_gid + "'";
        //        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        //msSQL = " update atm_trn_tauditcreation set samplestatus_flag ='Y'" +
        //        //    " where auditcreation_gid = '" + values.auditcreation_gid + "'";
        //        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


        //        //msSQL = " update atm_trn_tsample2checkpoint set samplechecklistverified_flag ='Y'" +
        //        //    " where sampleimport_gid = '" + values.sampleimport_gid + "'";
        //        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        values.status = true;
        //        values.message = "Submitted to Audit Approval Successfully..!";

        //    }
        //    else
        //    {
        //        values.message = "Error Occured..!";
        //        values.status = false;
        //    }
        //}
        public void DaGetAuditorSampleFlag(string checkpointgroupadd_gid, string sampleimport_gid, MdlAtmTrnAuditorMaker values)
        {


            msSQL = " select group_concat(distinct samplechecklistverified_flag) as samplechecklistverified_flag from atm_trn_tsample2checkpoint " +
                " where checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and sampleimport_gid ='" + sampleimport_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sample_flag = objODBCDatareader["samplechecklistverified_flag"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void DaGetAuditorCheckpointFlag(string checkpointgroupadd_gid, string auditcreation_gid, MdlAtmTrnAuditorMaker values)
        {


            msSQL = " select group_concat(distinct checklistverified_flag) as checklistverified_flag from atm_trn_tchecklist2checkpoint " +
                " where checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "' and auditcreation_gid ='" + auditcreation_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checklistcheckpoint_flag = objODBCDatareader["checklistverified_flag"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void DaGetAuditorSampleName(string auditcreation_gid, string sampleimport_gid, MdlAtmTrnAuditorMaker values)
        {
            msSQL = " select sample_name from atm_trn_tsampleimport " +
                " where auditcreation_gid ='" + auditcreation_gid + "' and sampleimport_gid ='" + sampleimport_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sample_name = objODBCDatareader["sample_name"].ToString();

            }
            objODBCDatareader.Close();
        }
        public void DaMakerObservationSampleView(string sampleimport_gid, MdlAtmTrnAuditorMaker values)
        {
            try

            {
                msSQL = " select a.observationscoresample_gid,a.sampleimport_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                        " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                                         " a.riskcategory_name, a.positiveconfirmity_name,samplechecklistverified_flag, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
                                         " a.partial_score, a.na_score, a.samplecapture_field, a.yes_disposition,a.sampleoverall_score, a.no_disposition, a.partial_disposition, " +
                                         " a.na_disposition from atm_trn_tobservationscoresample a " +
                                         " where  a.sampleimport_gid ='" + sampleimport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmakerobservationsampleview_list = new List<makerobservationsampleview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmakerobservationsampleview_list.Add(new makerobservationsampleview_list
                        {
                            observationscoresample_gid = (dr_datarow["observationscoresample_gid"].ToString()),
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
                            samplechecklistverified_flag = (dr_datarow["samplechecklistverified_flag"].ToString()),

                        });
                    }
                    values.makerobservationsampleview_list = getmakerobservationsampleview_list;
                }
                dt_datatable.Dispose();
                //msSQL = "select sum(capture_score) as total_amount from atm_trn_tauditcreation2checklist  where auditcreation_gid ='" + auditcreation_gid + "'";
                //values.total_score = objdbconn.GetExecuteScalar(msSQL);
                //values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaPostOverallCheckpointAgainstObservation(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            try
            {

                msSQL = " select checklistverified_flag from atm_trn_tchecklist2checkpoint where " +
                 " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and auditcreation2checklist_gid ='" + values.auditcreation2checklist_gid + "'";
                values.checklistverified_flag = objdbconn.GetExecuteScalar(msSQL);
                if (values.checklistverified_flag == "Y")
                {
                    msSQL = "select checklist2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and auditcreation_gid = '" + values.auditcreation_gid + "' ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                            });
                            msSQL = " update atm_trn_tchecklist2checkpoint set " +
                      " auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "'," +
                      " auditcreation_gid='" + values.auditcreation_gid + "'," +
                      " overall_detail='" + values.document_verified + "'," +
                      " created_by='" + employee_gid + "'," +
                      " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where checklist2checkpoint='" + dr_datarow["checklist2checkpoint"] + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        //values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                else
                {



                    msSQL = "select checklist2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and auditcreation_gid  is null";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                            });
                            msSQL = " update atm_trn_tchecklist2checkpoint set " +
                      " auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "'," +
                      //" auditcreation_gid='" + values.auditcreation_gid + "'," +
                      " overall_detail='" + values.document_verified + "'," +
                      " created_by='" + employee_gid + "'," +
                      " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where checklist2checkpoint='" + dr_datarow["checklist2checkpoint"] + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        //values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaOverallSelectedSummary(string checkpointgroupadd_gid, checklistcheckpoint values)
        {
            try
            {
                               
                    msSQL = "select checklist2checkpoint,checklist_name,overall_detail,checklistverified_flag,checkpointgroupadd_gid from atm_trn_tchecklist2checkpoint where " +
                   " checkpointgroupadd_gid ='" + checkpointgroupadd_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                checklist2checkpoint = (dr_datarow["checklist2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
                                overall_detail = (dr_datarow["overall_detail"].ToString()),
                                checklistverified_flag = (dr_datarow["checklistverified_flag"].ToString()),
                            });
                        }
                        values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
            
            catch
            {
                values.status = false;
            }
        }
        public void DaPostOverallCheckpointAgainstSample(MdlAtmTrnAuditorMaker values, string employee_gid)
        {
            try
            {

                msSQL = " select checklistverified_flag from atm_trn_tsample2checkpoint where " +
                 " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and auditcreation2checklist_gid ='" + values.auditcreation2checklist_gid + "'";
                values.checklistverified_flag = objdbconn.GetExecuteScalar(msSQL);
                if (values.checklistverified_flag == "Y")
                {
                    msSQL = "select sample2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tsample2checkpoint where " +
                   " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and sampleimport_gid = '"  + values.sampleimport_gid + "' ";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                sample2checkpoint = (dr_datarow["sample2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                            });
                            msSQL = " update atm_trn_tsample2checkpoint set " +
                      " auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "'," +
                      " sampleimport_gid='" + values.sampleimport_gid + "'," +
                      " checklist_verified='" + values.document_verified + "'," +
                      " created_by='" + employee_gid + "'," +
                      " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where sample2checkpoint='" + dr_datarow["sample2checkpoint"] + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        //values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                else
                {



                    msSQL = "select sample2checkpoint,checklist_name,checkpointgroupadd_gid from atm_trn_tsample2checkpoint where " +
                   " checkpointgroupadd_gid ='" + values.checkpointgroupadd_gid + "' and sampleimport_gid  is null";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getchecklistcheckpoint_list = new List<checklistcheckpoint_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getchecklistcheckpoint_list.Add(new checklistcheckpoint_list
                            {
                                sample2checkpoint = (dr_datarow["sample2checkpoint"].ToString()),
                                checklist_name = (dr_datarow["checklist_name"].ToString()),
                                checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),

                            });
                            msSQL = " update atm_trn_tsample2checkpoint set " +
                      " auditcreation2checklist_gid='" + values.auditcreation2checklist_gid + "'," +
                      //" auditcreation_gid='" + values.auditcreation_gid + "'," +
                      " checklist_verified='" + values.document_verified + "'," +
                      " created_by='" + employee_gid + "'," +
                      " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where sample2checkpoint='" + dr_datarow["sample2checkpoint"] + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }

                        //values.checklistcheckpoint_list = getchecklistcheckpoint_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaRaiseQueryHistorySummary(string auditcreation_gid, initialapprovaldtl values)
        {
            try
            {
                msSQL = " select sampleraisequery_gid,sampleimport_gid,description,raisequery_flag,query_title,query_toname,close_remarks, " +
                        "  concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,sampleraisequery_status " +
                         " from atm_trn_tsampleraisequery a " +
                         " left join hrm_mst_temployee f on a.created_by = f.employee_gid " +
                         " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                         " WHERE a.auditcreation_gid= '" + auditcreation_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getraisequeryhistory = new List<raisequeryhistory>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getraisequeryhistory.Add(new raisequeryhistory
                        {
                            sampleraisequery_gid = (dr_datarow["sampleraisequery_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            raisequery_flag = (dr_datarow["raisequery_flag"].ToString()),
                            query_title = (dr_datarow["query_title"].ToString()),
                            query_toname = (dr_datarow["query_toname"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            sampleraisequery_status = (dr_datarow["sampleraisequery_status"].ToString()),
                            close_remarks = (dr_datarow["close_remarks"].ToString()),
                        });
                    }
                    values.raisequeryhistory = getraisequeryhistory;
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
        public void DaAssignedTagUser(string sampleimport_gid, initialapprovaldtl values)
        {
            try
            {
                msSQL = " select group_concat(a.employee_name) as employee_name, a.tag_remarks as tag_remarks, " +
                      " concat(c.user_firstname, c.user_lastname, '/', c.user_code) as created_by, " +
                      " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                      " from atm_mst_ttaguser2employee a " +
                      " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                      " where a.sampleimport_gid = '" + sampleimport_gid + "' group by a.group_tagusergid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSampleAssignedTag = new List<SampleAssignedTag>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSampleAssignedTag.Add(new SampleAssignedTag
                        {
                            assigned_to = (dr_datarow["employee_name"].ToString()),
                            assigned_by = (dr_datarow["created_by"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            description = (dr_datarow["tag_remarks"].ToString()),
                        });
                    }
                    values.SampleAssignedTag = getSampleAssignedTag;
                }

                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaPostAuditRaiseQuery(auditraisequery values, string employee_gid)
        {
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    "where b.employee_gid ='" + employee_gid + "'";
            employeename = objdbconn.GetExecuteScalar(msSQL);


            msGetGid = objcmnfunctions.GetMasterGID("ARAQ");
            msSQL = "Insert into atm_trn_tauditraisequery( " +
                   " auditraisequery_gid, " +
                   " auditcreation_gid," +
                   " query_title, " +
                   " auditraisequery_status, " +
                   " description," +
                   " raisequery_raisedby," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.auditcreation_gid + "', " +
                   "'" + values.query_title.Replace("'", "") + "'," +
                   "'Open'," +
                   "'" + values.query_description.Replace("'", "") + "'," +
                    "'" + employeename + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                //msSQL = " update atm_trn_tsampleimport set raisedquery_flag='Y'  where sampleimport_gid='" + values.sampleimport_gid + "' ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Query Raised Successfully";

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

                if (lsauditmaker_gid == lsauditapprover_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditchecker_gid)
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
                    //lsTo2members = lsTo2members.Replace(employee_gid, " ");
                    //lsTo2members.Replace(",,", ",");

                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    //lscc2members.Replace(employee_gid, "");
                    //lscc2members.Replace(",,", ",");

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
             " from atm_trn_tauditcreation  " +
            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                    }
                    objODBCDatareader.Close();


                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                         " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                     " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    sub = "Audit Query   ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to View the details.";
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
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.auditcreation_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Sample Query Raised'," +
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

                        lscc2members.Replace(employee_gid, "");
                        lscc2members.Replace(",,", ",");

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                        }
                        objODBCDatareader.Close();


                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                             " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                         " where employee_gid = '" + employee_gid + "'";
                        string employee_name = objdbconn.GetExecuteScalar(msSQL);

                        sub = "Audit Query   ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to View the details.";
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
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Sample Query Raised'," +
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
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

                            lscc2members.Replace(employee_gid, "");
                            lscc2members.Replace(",,", ",");

                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                            }
                            objODBCDatareader.Close();


                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                 " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                             " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);

                            sub = "Audit Query   ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to View the details.";
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
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Sample Query Raised'," +
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

                                lscc2members.Replace(employee_gid, "");
                                lscc2members.Replace(",,", ",");

                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                         " from atm_trn_tauditcreation  " +
                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                }
                                objODBCDatareader.Close();


                                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                     " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                 " where employee_gid = '" + employee_gid + "'";
                                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                                sub = "Audit Query   ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to View the details.";
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
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Sample Query Raised'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }

                            else
                            {
                                try
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

                                    lscc2members.Replace(employee_gid, "");
                                    lscc2members.Replace(",,", ",");

                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                             " from atm_trn_tauditcreation  " +
                            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                    }
                                    objODBCDatareader.Close();


                                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                         " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                     " where employee_gid = '" + employee_gid + "'";
                                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                                    sub = "Audit Query   ";
                                    body = "Dear All,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to View the details.";
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
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Sample Query Raised'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + employee_gid + "'," +
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
                    }
                }
            }
            else
            {

                values.message = "Error Occured While Raising Query";
                values.status = false;
            }
        }

        public void DaAuditRaisedQuerySummary(auditraisequery values, string auditcreation_gid)
        {
            try
            {
                msSQL = " select auditraisequery_gid,description,auditraisequery_flag,query_title,query_toname,close_remarks, " +
                        "  concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,auditraisequery_status " +
                         " from atm_trn_tauditraisequery a " +
                         " left join hrm_mst_temployee f on a.created_by = f.employee_gid " +
                         " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                         " WHERE a.auditcreation_gid= '" + auditcreation_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditquerydata = new List<auditquerydata>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditquerydata.Add(new auditquerydata
                        {
                            auditraisequery_gid = (dr_datarow["auditraisequery_gid"].ToString()),
                            query_description = (dr_datarow["description"].ToString()),
                            auditraisequery_flag = (dr_datarow["auditraisequery_flag"].ToString()),
                            query_title = (dr_datarow["query_title"].ToString()),
                            query_toname = (dr_datarow["query_toname"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            auditraisequery_status = (dr_datarow["auditraisequery_status"].ToString()),
                            close_remarks = (dr_datarow["close_remarks"].ToString()),
                        });
                    }
                    values.auditquerydata = getauditquerydata;
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
        public bool DaGetAuditQuerydetaillist(string employee_gid, string auditraisequery_gid, auditraisequery values)
        {

            msSQL = "select a.auditqueries2response_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date , a.document_name,a.document_path," +
                    " a.auditresponse_gid,a.replied_by ," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from atm_trn_tauditqueries2response a " +
                    " left join hrm_mst_temployee b on a.auditresponse_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.auditraisequery_gid = '" + auditraisequery_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getauditQuerydetaillist = new List<auditQuerydetaillist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["auditresponse_gid"].ToString() == employee_gid)
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
                    getauditQuerydetaillist.Add(new auditQuerydetaillist
                    {
                        auditqueries2response_gid = (dr_datarow["auditqueries2response_gid"].ToString()),
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
                values.auditQuerydetaillist = getauditQuerydetaillist;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public void DaPostAuditCloseQuery(auditraisequery values, string employee_gid)
        {
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
            employeename = objdbconn.GetExecuteScalar(msSQL); 

            msSQL = " update atm_trn_tauditraisequery set  " +
                    " close_remarks='" + values.close_remarks.Replace("'", "") + "'," +
                    " raisequery_closedby='" + employeename + "'," +
                    " closed_by='" + employee_gid + "'," +
                    " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " auditraisequery_flag='N'," +
                    " auditraisequery_status='Closed'" +
                    " where auditraisequery_gid='" + values.auditraisequery_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message =  "Query Closed Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }


        public void DaPostAuditQuerydetail(string employee_gid, auditraisequery values)
        {
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                    "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                    "where b.employee_gid ='" + employee_gid + "'";
            employeename = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("AUSQ");
            msSQL = " insert into atm_trn_tauditqueries2response(" +
                    " auditqueries2response_gid," +
                    " auditraisequery_gid, " +
                    " auditcreation_gid," +
                    " remarks," +
                    " replied_by," +
                     " raisequery_replyby," +
                    " auditresponse_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.auditraisequery_gid + "'," +
                    "'" + values.auditcreation_gid + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + values.replied_by + "'," +
                     "'" + employeename + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                //msSQL = " update atm_trn_tsamplequeries set samplequery_status='Open'  where sampleimport_gid='" + values.sampleimport_gid + "' ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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


                            msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                   " from atm_trn_tauditcreation a  " +
                                   " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                   " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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


                                msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                       " from atm_trn_tauditcreation a  " +
                                       " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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
                                body = body + "<b> Audit Name :</b> " + lsaudit_name + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + lsaaudit_uniqueno + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + lsauditdepartment_name + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Query Title :</b> " + lsquery_title + "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to Response The Query.";
                                body = body + "<br />";
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


                                    msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                           " from atm_trn_tauditcreation a  " +
                                           " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                           " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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
                                    body = body + "<b> Audit Name :</b> " + lsaudit_name + "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + lsaaudit_uniqueno + "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + lsauditdepartment_name + "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Query Title :</b> " + lsquery_title + "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to Response The Query.";
                                    body = body + "<br />";
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


                                        msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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
                                        body = body + "<b> Audit Name :</b> " + lsaudit_name + "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Refernce Number :</b> " + lsaaudit_uniqueno + "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + lsauditdepartment_name + "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Query Title :</b> " + lsquery_title + "<br />";
                                        body = body + "<br />";
                                        body = body + "Kindly log into systems to Response The Query.";
                                        body = body + "<br />";
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


                                        msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title " +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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
                                        body = body + "<b>Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title) + "<br />";
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


                            msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                   " from atm_trn_tauditcreation a  " +
                                   " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                   " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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


                                msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                       " from atm_trn_tauditcreation a  " +
                                       " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno )+ "<br />";
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


                                    msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                           " from atm_trn_tauditcreation a  " +
                                           " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                           " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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


                                        msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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


                                        msSQL = " select a.auditcreation_gid,b.auditraisequery_gid,a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name, b.query_title" +
                                               " from atm_trn_tauditcreation a  " +
                                               " left join atm_trn_tauditraisequery b on b.auditcreation_gid = a.auditcreation_gid" +
                                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "' or b.auditraisequery_gid='" + values.auditraisequery_gid + "' ";

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
        public void DaAuditResponseDocUpload(HttpRequest httpRequest, responsedoc_upload objfilename, string employee_gid, string user_gid)
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
            string auditraisequery_gid = HttpContext.Current.Request.Params["auditraisequery_gid"];
            string auditcreation_gid = HttpContext.Current.Request.Params["auditcreation_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "AUDIT/AuditResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                        lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "AUDIT/AuditResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AUDIT/AuditResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "AUDIT/AuditResponseDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("A2RQ");
                        msSQL = " insert into atm_trn_tauditqueries2response(" +
                                " auditqueries2response_gid," +
                                " auditraisequery_gid, " +
                                " auditcreation_gid," +
                                " auditresponse_gid," +
                                " document_name ," +
                                " document_path," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + auditraisequery_gid + "'," +
                                "'" + auditcreation_gid + "'," +
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
    }
}