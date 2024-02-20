//using ems.audit.Models;
//using ems.utilities.Functions;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Configuration;
//using System.Data.Odbc;
//using System.Linq;

//namespace ems.audit.DataAccess
//{
//    public class DaAtmTrnMyAuditTask
//    {

//        dbconn objdbconn = new dbconn();
//        cmnfunctions objcmnfunctions = new cmnfunctions();
//        DataTable dt_datatable;
//        OdbcDataReader objODBCDatareader;
//        string msSQL, msGetGid, count, lsauditdepartment_value, lscapture_yesscore, lscapture_noscore, lscapture_partialscore, lscapture_nascore, lscapture_totalscore, msGetaudituniqueno, lsdue_date, lsreport_date, lsperiodfrom_date, lsauditperiod_to, lsauditname_value;     
//        int mnResult, k;

//        public void DaGetMyAuditTask(MdlAtmTrnMyAuditTask values, string Employee_gid)
//        {
//            values.employee_gid = Employee_gid;
//            try
//            {

//                //b.employee_name as tag_user,b.employee_gid as taguser_gid,
//                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,a.due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name, c.auditmaker_name as auditee_Maker, c.auditchecker_name as auditee_checker," +
//                    " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,c.employee_gid as auditeemaker_gid,c.auditmapping_gid as auditeechecker_gid,group_concat(d.employee_gid )as raisequery_gid,group_concat(b.employee_gid)as taguser_gid,group_concat(g.employee_gid) as sampleraisequery_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
//                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//                    " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//                    " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//                   " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +
//                    " where (a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//                    " (b.employee_gid='" + Employee_gid + "') " +
//                    " or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "') order by a.auditcreation_gid desc ";
//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getmyaudittask_list = new List<myaudittask_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getmyaudittask_list.Add(new myaudittask_list
//                        {

//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//                            audit_name = (dr_datarow["audit_name"].ToString()),
//                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
//                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
//                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//                            due_date = (dr_datarow["due_date"].ToString()),
//                            status_update = (dr_datarow["status"].ToString()),
//                            status_flag = (dr_datarow["status_flag"].ToString()),
//                            approval_flag = (dr_datarow["approval_flag"].ToString()),
//                            approval_status = (dr_datarow["approval_status"].ToString()),
//                            //taguser_gid = (dr_datarow["taguser_gid"].ToString()),
//                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
//                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
//                            auditmaker_gid = (dr_datarow["auditmaker_gid"].ToString()),
//                            auditchecker_gid = (dr_datarow["auditchecker_gid"].ToString()),
//                            raisequery_gid = (dr_datarow["raisequery_gid"].ToString()),
//                            sampleraisequery_gid = (dr_datarow["sampleraisequery_gid"].ToString()),
//                            created_date = (dr_datarow["created_date"].ToString()),
//                            created_by = (dr_datarow["created_by"].ToString()),
//                            auditapprover_gid = (dr_datarow["auditapprover_gid"].ToString()),

//                        });
//                    }
//                    values.myaudittask_list = getmyaudittask_list;
//                }
//                dt_datatable.Dispose();
//                values.status = true;
//            }

//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }
//        public void DaMyAuditTaskView(string auditcreation_gid, MdlAtmTrnMyAuditTask values, string employee_gid)
//        {
//            try
//            {
//                msSQL = " select a.auditcreation2checklist_gid,a.auditcreation_gid,a.checklistmasteradd_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description," +
//                      " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score, a.partial_score, a.na_score, b.capture_score" +
//                      " from atm_trn_tauditcreation2checklist a " +
//                      " left join atm_trn_tcheckpointobservation b on a.auditcreation2checklist_gid=b.auditcreation2checklist_gid " +
//                        " where a.auditcreation_gid='" + auditcreation_gid + "'";
//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getcheckpointobservationview_list = new List<checkpointobservationview_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getcheckpointobservationview_list.Add(new checkpointobservationview_list
//                        {
//                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
//                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
//                            audittype_name = (dr_datarow["audittype_name"].ToString()),
//                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
//                            audit_name = (dr_datarow["audit_name"].ToString()),
//                            checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
//                            checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
//                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
//                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
//                            noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
//                            yes_score = (dr_datarow["yes_score"].ToString()),
//                            no_score = (dr_datarow["no_score"].ToString()),
//                            partial_score = (dr_datarow["partial_score"].ToString()),
//                            na_score = (dr_datarow["na_score"].ToString()),
//                            capture_score = (dr_datarow["capture_score"].ToString()),


//                        });
//                    }
//                    values.checkpointobservationview_list = getcheckpointobservationview_list;
//                }
//                dt_datatable.Dispose();
//                values.status = true;

//                msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount  where auditcreation_gid ='" + auditcreation_gid + "'";
//                values.total_score = objdbconn.GetExecuteScalar(msSQL);
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                //    }


//                //else
//                //{
//                //    values.status = false;
//                //    values.message = "No records to show";
//                //}

//            }

//            catch
//            {
//                values.status = false;
//            }
//        }
//        public void DaEditMyAuditTask(string auditcreation_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = " select a.auditcreation_gid,b.checklistmaster_gid,(b.auditmaker_name) as audit_makername,(b.auditchecker_name) as audit_checkername,audit_uniqueno,a.audit_name,auditpriority_gid,auditpriority_name,a.status,a.auditmapping_gid,a.auditmaker_name,a.employee_gid,auditmapping2employee_gid,a.auditchecker_name,auditapprover_name,auditfrequency_gid,auditfrequency_name, date_format(due_date,'%d-%m-%Y') as due_date , date_format(report_date,'%d-%m-%Y') as report_date , date_format(auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate , date_format(auditperiod_todate,'%d-%m-%Y') as auditperiod_todate from atm_trn_tauditcreation a " +
//            " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid" +
//            " where auditcreation_gid='" + auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
//                values.audit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
//                values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
//                values.audit_name = objODBCDatareader["audit_name"].ToString();
//                values.auditpriority_gid = objODBCDatareader["auditpriority_gid"].ToString();
//                values.auditpriority_name = objODBCDatareader["auditpriority_name"].ToString();
//                values.audit_maker = objODBCDatareader["auditmaker_name"].ToString();
//                values.auditmapping_gid = objODBCDatareader["auditmapping_gid"].ToString();
//                values.audit_checker = objODBCDatareader["auditchecker_name"].ToString();
//                values.auditfrequency_gid = objODBCDatareader["auditfrequency_gid"].ToString();
//                values.auditfrequency_name = objODBCDatareader["auditfrequency_name"].ToString();
//                values.audit_approver = objODBCDatareader["auditapprover_name"].ToString();
//                values.due_date = objODBCDatareader["due_date"].ToString();
//                values.report_date = objODBCDatareader["report_date"].ToString();
//                values.periodfrom_date = objODBCDatareader["auditperiod_fromdate"].ToString();
//                values.auditperiod_to = objODBCDatareader["auditperiod_todate"].ToString();
//                values.status_update = objODBCDatareader["status"].ToString();
//                values.auditmaker_name = objODBCDatareader["audit_makername"].ToString();
//                values.auditchecker_name = objODBCDatareader["audit_checkername"].ToString();
//            }
//            objODBCDatareader.Close();
//            values.status = true;
//        }


//        public void DaGetCheckpointObservation(MdlAtmTrnMyAuditTask values)
//        {
//            try
//            {
//                msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                        " FROM atm_trn_tauditcreation a" +
//                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.auditcreation_gid desc ";
//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getcheckpointobservation_list = new List<checkpointobservation_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getcheckpointobservation_list.Add(new checkpointobservation_list
//                        {

//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//                            audit_name = (dr_datarow["audit_name"].ToString()),
//                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//                            due_date = (dr_datarow["due_date"].ToString()),
//                            created_by = (dr_datarow["created_by"].ToString()),
//                            created_date = (dr_datarow["created_date"].ToString()),

//                        });
//                    }
//                    values.checkpointobservation_list = getcheckpointobservation_list;
//                }
//                dt_datatable.Dispose();
//                values.status = true;
//            }
//            catch
//            {
//                values.status = false;
//            }
//        }
//        public void DaCheckpointObservationView(string auditcreation_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = "select auditcreation_gid from atm_trn_tcheckpointobservation where  auditcreation_gid ='" + auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);

//            msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount  where auditcreation_gid ='" + auditcreation_gid + "'";
//            values.total_score = objdbconn.GetExecuteScalar(msSQL);
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//            if (objODBCDatareader.HasRows == true)
//            {

//                msSQL = " select a.auditcreation2checklist_gid,b.checklistmasteradd_gid,a.auditcreation_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description," +
//                              " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score, a.partial_score, a.na_score, b.capture_score" +
//                              " from atm_trn_tauditcreation2checklist a " +
//                              " left join atm_trn_tcheckpointobservation b on a.auditcreation2checklist_gid=b.auditcreation2checklist_gid " +
//                                " where a.auditcreation_gid='" + auditcreation_gid + "'";
//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getcheckpointobservationview_list = new List<checkpointobservationview_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getcheckpointobservationview_list.Add(new checkpointobservationview_list
//                        {
//                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
//                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
//                            audittype_name = (dr_datarow["audittype_name"].ToString()),
//                            checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
//                            audit_name = (dr_datarow["audit_name"].ToString()),
//                            checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
//                            checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
//                            riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
//                            positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
//                            noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
//                            yes_score = (dr_datarow["yes_score"].ToString()),
//                            no_score = (dr_datarow["no_score"].ToString()),
//                            partial_score = (dr_datarow["partial_score"].ToString()),
//                            na_score = (dr_datarow["na_score"].ToString()),
//                            capture_score = (dr_datarow["capture_score"].ToString()),


//                        });
//                    }
//                    values.checkpointobservationview_list = getcheckpointobservationview_list;
//                }
//                dt_datatable.Dispose();

//                values.status = true;
//            }
//            else
//            {
//                try
//                {

//                    msSQL = " select auditcreation2checklist_gid,b.checklistmasteradd_gid,a.auditcreation_gid, auditdepartment_name, audittype_name, checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
//                               "riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score, total_score " +
//                               " from atm_trn_tauditcreation a " +
//                               " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
//                                 " where b.auditcreation_gid='" + auditcreation_gid + "'";
//                    dt_datatable = objdbconn.GetDataTable(msSQL);
//                    var getcheckpointobservationview_list = new List<checkpointobservationview_list>();
//                    if (dt_datatable.Rows.Count != 0)
//                    {
//                        foreach (DataRow dr_datarow in dt_datatable.Rows)
//                        {
//                            getcheckpointobservationview_list.Add(new checkpointobservationview_list
//                            {
//                                auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
//                                checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
//                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                                auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
//                                audittype_name = (dr_datarow["audittype_name"].ToString()),
//                                checkpointgroup_name = (dr_datarow["checkpointgroup_name"].ToString()),
//                                audit_name = (dr_datarow["audit_name"].ToString()),
//                                checkpoint_intent = (dr_datarow["checkpoint_intent"].ToString()),
//                                checkpoint_description = (dr_datarow["checkpoint_description"].ToString()),
//                                riskcategory_name = (dr_datarow["riskcategory_name"].ToString()),
//                                positiveconfirmity_name = (dr_datarow["positiveconfirmity_name"].ToString()),
//                                noteto_auditor = (dr_datarow["noteto_auditor"].ToString()),
//                                yes_score = (dr_datarow["yes_score"].ToString()),
//                                no_score = (dr_datarow["no_score"].ToString()),
//                                partial_score = (dr_datarow["partial_score"].ToString()),
//                                na_score = (dr_datarow["na_score"].ToString()),
//                                total_score = (dr_datarow["total_score"].ToString()),


//                            });
//                        }
//                        values.checkpointobservationview_list = getcheckpointobservationview_list;
//                    }
//                    dt_datatable.Dispose();

//                    values.status = true;
//                }
//                catch
//                {
//                    values.status = false;
//                }
//            }
//        }
//        public void DaEditCheckpointObservation(string auditcreation_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = " select a.auditcreation_gid,b.checklistmaster_gid,(b.auditmaker_name) as audit_makername,(b.auditchecker_name) as audit_checkername,audit_uniqueno,a.audit_name,auditpriority_gid,auditpriority_name,a.auditmapping_gid,a.auditmaker_name,a.employee_gid,auditmapping2employee_gid,a.auditchecker_name,auditapprover_name,auditfrequency_gid,auditfrequency_name, date_format(due_date,'%d-%m-%Y') as due_date , date_format(report_date,'%d-%m-%Y') as report_date , date_format(auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate , date_format(auditperiod_todate,'%d-%m-%Y') as auditperiod_todate from atm_trn_tauditcreation a " +
//            " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid" +
//            " where auditcreation_gid='" + auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
//                values.audit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
//                values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
//                values.audit_name = objODBCDatareader["audit_name"].ToString();
//                values.auditpriority_gid = objODBCDatareader["auditpriority_gid"].ToString();
//                values.auditpriority_name = objODBCDatareader["auditpriority_name"].ToString();
//                values.audit_maker = objODBCDatareader["auditmaker_name"].ToString();
//                values.auditmapping_gid = objODBCDatareader["auditmapping_gid"].ToString();
//                values.audit_checker = objODBCDatareader["auditchecker_name"].ToString();
//                values.auditfrequency_gid = objODBCDatareader["auditfrequency_gid"].ToString();
//                values.auditfrequency_name = objODBCDatareader["auditfrequency_name"].ToString();
//                values.audit_approver = objODBCDatareader["auditapprover_name"].ToString();
//                values.due_date = objODBCDatareader["due_date"].ToString();
//                values.report_date = objODBCDatareader["report_date"].ToString();
//                values.periodfrom_date = objODBCDatareader["auditperiod_fromdate"].ToString();
//                values.auditperiod_to = objODBCDatareader["auditperiod_todate"].ToString();
//                values.auditmaker_name = objODBCDatareader["audit_makername"].ToString();
//                values.auditchecker_name = objODBCDatareader["audit_checkername"].ToString();
//            }
//            objODBCDatareader.Close();
//            values.status = true;
//        }


//        public void DaPostObservationTotalAmount(checkpointobservationadd values, string employee_gid)

//        {
//            msSQL = "select auditcreation2checklist_gid from atm_trn_tobservationtotalamount where  auditcreation2checklist_gid ='" + values.auditcreation2checklist_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                msSQL = "update atm_trn_tobservationtotalamount set capture_score='" + values.capture_totalscore + "'" + " where auditcreation2checklist_gid = '" + values.auditcreation2checklist_gid + "'";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                if (mnResult != 0)
//                {
//                    msGetGid = objcmnfunctions.GetMasterGID("OTAL");
//                    msSQL = " insert into atm_trn_tobservationtotalamountlog(" +
//                              " observationtotalamountlog_gid ," +
//                              " auditcreation2checklist_gid," +
//                              " auditcreation_gid," +
//                               " observationtotalamount_gid," +
//                              " checklistmasteradd_gid," +
//                              " capture_score, " +
//                              " updated_by, " +
//                              " updated_date) " +
//                              " values(" +
//                               "'" + msGetGid + "'," +
//                               "'" + values.auditcreation2checklist_gid + "', " +
//                               "'" + values.auditcreation_gid + "', " +
//                               "'" + values.observationtotalamount_gid + "', " +
//                             "'" + values.checklistmasteradd_gid + "', " +
//                               "'" + values.capture_totalscore + "'," +
//                               "'" + employee_gid + "'," +
//                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


//                    msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount  where auditcreation_gid ='" + values.auditcreation_gid + "'";
//                    values.total_amount = objdbconn.GetExecuteScalar(msSQL);
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    msSQL = "update atm_mst_tchecklistmasteradd set capture_flag ='Y'  where checklistmasteradd_gid ='" + values.checklistmasteradd_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    values.status = true;
//                    values.message = "Check Point Observation Added Successfully";
//                }
//                else
//                {
//                    values.message = "Error Occured While Adding";
//                    values.status = false;
//                }
//                objODBCDatareader.Close();
//            }

//            else
//            {

//                msGetGid = objcmnfunctions.GetMasterGID("OBTA");

//                msSQL = " insert into atm_trn_tobservationtotalamount(" +
//                    " observationtotalamount_gid," +
//                    " auditcreation2checklist_gid ," +
//                    " auditcreation_gid ," +
//                    " checkpointobservation_gid ," +
//                   " checklistmasteradd_gid ," +
//                    " capture_score," +
//                    " created_by," +
//                    " created_date)" +
//                    " values(" +
//                    "'" + msGetGid + "'," +
//                    "'" + values.auditcreation2checklist_gid + "', " +
//                    "'" + values.auditcreation_gid + "', " +
//                     "'" + employee_gid + "', " +
//                    "'" + values.checklistmasteradd_gid + "', " +
//                     "'" + values.capture_totalscore.Replace("'", "") + "'," +
//                    "'" + employee_gid + "'," +
//                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                if (mnResult != 0)
//                {
//                    msSQL = "select sum(capture_score) as total_amount from atm_trn_tobservationtotalamount where auditcreation_gid ='" + values.auditcreation_gid + "'";
//                    values.total_amount = objdbconn.GetExecuteScalar(msSQL);
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


//                    values.status = true;
//                    values.message = "Check Point Observation Added Successfully";
//                }
//                else
//                {
//                    values.message = "Error Occured While Adding";
//                    values.status = false;
//                }
//            }

//        }

//        public void DaPostCheckpointObservation(checkpointobservation values, string employee_gid)
//        {
//            msSQL = "select auditcreation_gid from atm_trn_tcheckpointobservation where  auditcreation_gid ='" + values.auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                foreach (string i in values.auditcreation2checklist_gid)
//                {

//                    msSQL = " select b.auditcreation2checklist_gid,a.auditcreation_gid,b.checklistmasteradd_gid, auditdepartment_name, audittype_name, checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
//                              "riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score,c.capture_score" +
//                              " from atm_trn_tauditcreation a " +
//                              " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
//                               " left join atm_trn_tobservationtotalamount c on b.auditcreation2checklist_gid=c.auditcreation2checklist_gid " +
//                                " where b.auditcreation2checklist_gid='" + i + "'";
//                    dt_datatable = objdbconn.GetDataTable(msSQL);

//                    if (dt_datatable.Rows.Count != 0)
//                    {
//                        foreach (DataRow dt in dt_datatable.Rows)
//                        {


//                            msSQL = " update atm_trn_tcheckpointobservation set" +
//                                    " checklistmasteradd_gid ='" + dt["checklistmasteradd_gid"].ToString() + "'," +
//                                    " auditcreation_gid ='" + dt["auditcreation_gid"].ToString() + "'," +
//                                    " auditdepartment_name ='" + dt["auditdepartment_name"].ToString() + "'," +
//                                    " audittype_name  ='" + dt["audittype_name"].ToString() + "'," +
//                                    " checkpointgroup_name = '" + dt["checkpointgroup_name"].ToString() + "'," +
//                                    " audit_name ='" + dt["audit_name"].ToString() + "'," +
//                                    " checkpoint_intent ='" + dt["checkpoint_intent"].ToString() + "'," +
//                                    " checkpoint_description= '" + dt["checkpoint_description"].ToString() + "'," +
//                                    " riskcategory_name ='" + dt["riskcategory_name"].ToString() + "'," +
//                                    " positiveconfirmity_name='" + dt["positiveconfirmity_name"].ToString() + "'," +
//                                    " noteto_auditor ='" + dt["noteto_auditor"].ToString() + "'," +
//                                    " yes_score = '" + dt["yes_score"].ToString() + "'," +
//                                    " no_score='" + dt["no_score"].ToString() + "'," +
//                                    " partial_score='" + dt["partial_score"].ToString() + "'," +
//                                    " na_score='" + dt["na_score"].ToString() + "'," +
//                                    " capture_score='" + dt["capture_score"].ToString() + "'," +
//                                   " updated_by='" + employee_gid + "'," +
//                                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
//                                     " where auditcreation2checklist_gid='" + i + "' ";

//                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                        }
//                    }
//                }
//                if (mnResult != 0)
//                {


//                    values.status = true;
//                    values.message = "Checklist Master Assigned Successfully";
//                }
//                else
//                {
//                    values.message = "Error Occured While Adding";
//                    values.status = false;
//                }
//                objODBCDatareader.Close();
//            }
//            else
//            {
//                foreach (string i in values.auditcreation2checklist_gid)
//                {

//                    msSQL = " select b.auditcreation2checklist_gid,a.auditcreation_gid,b.checklistmasteradd_gid, auditdepartment_name, audittype_name, checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
//                              "riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score,c.capture_score" +
//                              " from atm_trn_tauditcreation a " +
//                              " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
//                               " left join atm_trn_tobservationtotalamount c on b.auditcreation2checklist_gid=c.auditcreation2checklist_gid " +
//                                " where b.auditcreation2checklist_gid='" + i + "'";
//                    dt_datatable = objdbconn.GetDataTable(msSQL);

//                    if (dt_datatable.Rows.Count != 0)
//                    {
//                        foreach (DataRow dt in dt_datatable.Rows)
//                        {
//                            msGetGid = objcmnfunctions.GetMasterGID("CPOB");

//                            msSQL = " insert into atm_trn_tcheckpointobservation(" +
//                                    " checkpointobservation_gid," +
//                                    " auditcreation2checklist_gid," +
//                                     " checklistmasteradd_gid," +
//                                    " auditcreation_gid," +
//                                    " auditdepartment_name ," +
//                                    " audittype_name ," +
//                                    " checkpointgroup_name," +
//                                    " audit_name ," +
//                                    " checkpoint_intent," +
//                                    " checkpoint_description ," +
//                                    " riskcategory_name," +
//                                    " positiveconfirmity_name ," +
//                                    " noteto_auditor ," +
//                                    " yes_score ," +
//                                    " no_score ," +
//                                    " partial_score ," +
//                                    " na_score," +
//                                    " capture_score," +
//                                    " created_by," +
//                                    " created_date)" +
//                                    " values(" +
//                                    "'" + msGetGid + "'," +
//                                      "'" + i + "', " +
//                                "'" + dt["checklistmasteradd_gid"].ToString() + "'," +
//                                 "'" + dt["auditcreation_gid"].ToString() + "'," +
//                                  "'" + dt["auditdepartment_name"].ToString() + "'," +
//                                  "'" + dt["audittype_name"].ToString() + "'," +
//                                   "'" + dt["checkpointgroup_name"].ToString() + "'," +
//                                    "'" + dt["audit_name"].ToString() + "'," +
//                                    "'" + dt["checkpoint_intent"].ToString() + "'," +
//                                     "'" + dt["checkpoint_description"].ToString() + "'," +
//                                      "'" + dt["riskcategory_name"].ToString() + "'," +
//                                      "'" + dt["positiveconfirmity_name"].ToString() + "'," +
//                                      "'" + dt["noteto_auditor"].ToString() + "'," +
//                                      "'" + dt["yes_score"].ToString() + "'," +
//                                      "'" + dt["no_score"].ToString() + "'," +
//                                      "'" + dt["partial_score"].ToString() + "'," +
//                                      "'" + dt["na_score"].ToString() + "'," +
//                                       "'" + dt["capture_score"].ToString() + "'," +
//                                    "'" + employee_gid + "'," +
//                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                            msSQL = "update atm_trn_tauditcreation set status_flag ='Y'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                        }
//                    }
//                }
//                if (mnResult != 0)
//                {

//                    values.status = true;
//                    values.message = "Checklist Master Assigned Successfully";
//                }
//                else
//                {
//                    values.message = "Error Occured While Adding";
//                    values.status = false;
//                }
//            }
//        }
//        public void DaGetMyAuditTaskStatus(MdlAtmTrnMyAuditTask values, string employee_gid)
//        {
//            msSQL = " update atm_trn_tauditcreation set status='" + values.status_update + "'" +
//                    " where auditcreation_gid='" + values.auditcreation_gid + "' ";
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//            if (mnResult != 0)
//            {
//                values.status = true;
//                values.message = "Status updated Successfully..!";
//                msGetGid = objcmnfunctions.GetMasterGID("ASUL");
//                msSQL = " insert into atm_trn_tstatusupdatelog (" +
//                      " statusupdatelog_gid, " +
//                      " auditcreation_gid," +
//                      " status," +
//                      " updated_by," +
//                      " updated_date) " +
//                      " values (" +
//                      " '" + msGetGid + "'," +
//                      " '" + values.auditcreation_gid + "'," +
//                      " '" + values.status_update + "'," +
//                      " '" + employee_gid + "'," +
//                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//            }

//            else
//            {
//                values.status = false;
//                values.message = "Error Occurred";
//            }
//        }

//        public void DaGetMyClosedAuditTask(MdlAtmTrnMyAuditTask values, string Employee_gid)
//        {
//            values.employee_gid = Employee_gid;
//            try
//            {
//                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,a.due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name, c.auditmaker_name as auditee_Maker, c.auditchecker_name as auditee_checker," +
//                    " b.employee_name as tag_user,a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,b.employee_gid as taguser_gid,c.employee_gid as auditeemaker_gid,c.auditmapping_gid as auditeechecker_gid,d.employee_gid as raisequery_gid,g.employee_gid as sampleraisequery_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
//                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//                    " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//                    " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//                   " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +
//                    " where ((a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//                    " (b.employee_gid='" + Employee_gid + "') " +
//                    " or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "')) and a.status = 'closed'";



//                //" SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditfrequency_name,a.auditpriority_name,a.auditmaker_name,a.auditchecker_name,a.status,a.approval_status,a.status_flag,a.approval_flag,b.employee_gid as makeremployee_gid,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                //" concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
//                //" concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as auditmaker_name," +
//                //" concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as auditchecker_name" +
//                //" FROM atm_trn_tauditcreation a" +
//                //" left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                //"  left join hrm_mst_temployee d on a.auditmapping_gid = d.employee_gid" +
//                //" left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                //" left join adm_mst_tuser e on e.user_gid=d.user_gid" +
//                //" where a.employee_gid='" + Employee_gid + "'or a.auditmapping_gid='" + Employee_gid + "' and a.status = 'closed'";

//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getmyaudittask_list = new List<myaudittask_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getmyaudittask_list.Add(new myaudittask_list
//                        {

//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//                            audit_name = (dr_datarow["audit_name"].ToString()),
//                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
//                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
//                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//                            due_date = (dr_datarow["due_date"].ToString()),
//                            status_update = (dr_datarow["status"].ToString()),
//                            created_by = (dr_datarow["created_by"].ToString()),
//                            created_date = (dr_datarow["created_date"].ToString()),
//                            status_flag = (dr_datarow["status_flag"].ToString()),
//                            approval_flag = (dr_datarow["approval_flag"].ToString()),
//                            //makeremployee_gid = (dr_datarow["makeremployee_gid"].ToString()),
//                            approval_status = (dr_datarow["approval_status"].ToString()),
//                        });
//                    }
//                    values.myaudittask_list = getmyaudittask_list;
//                }
//                dt_datatable.Dispose();
//                values.status = true;
//            }

//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }
//        //    {
//        //        msSQL = " select f.auditcreation_gid,f.checklistmaster_gid,f.audit_name,f.audit_uniqueno,f.auditmaker_name,f.auditchecker_name,f.status,f.approval_status,f.status_flag,f.approval_flag,b.employee_gid as auditemployee_gid,d.employee_gid as checkeremployee_gid,date_format(f.due_date,'%d-%m-%Y') as due_date,date_format(f.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.auditmaker_name, a.employee_gid, a.auditchecker_name, a.auditmapping_gid," +
//        //       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
//        //       " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as auditmaker_name," +
//        //       " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as auditchecker_name" +
//        //       " from atm_mst_tchecklistmaster a" +
//        //       " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid" +
//        //       " left join hrm_mst_temployee d on a.auditmapping_gid = d.employee_gid" +
//        //       " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//        //       " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
//        //        " left join atm_trn_tauditcreation f on f.checklistmaster_gid = a.checklistmaster_gid" +
//        //       " where a.employee_gid='" + Employee_gid + "'or a.auditmapping_gid='" + Employee_gid + "' and f.status = 'closed'";

//        //        dt_datatable = objdbconn.GetDataTable(msSQL);

//        //        var getmyaudittasks_list = new List<myaudittask_list>();

//        //        if (dt_datatable.Rows.Count != 0)
//        //        {
//        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
//        //            {
//        //                getmyaudittasks_list.Add(new myaudittask_list
//        //                {

//        //                    auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//        //                    checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//        //                    auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
//        //                    auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
//        //                    audit_name = (dr_datarow["audit_name"].ToString()),
//        //                    audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//        //                    audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//        //                    audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//        //                    due_date = (dr_datarow["due_date"].ToString()),
//        //                    status_update = (dr_datarow["status"].ToString()),
//        //                    auditemployee_gid = (dr_datarow["auditemployee_gid"].ToString()),
//        //                    checkeremployee_gid = (dr_datarow["checkeremployee_gid"].ToString()),
//        //                    created_by = (dr_datarow["created_by"].ToString()),
//        //                    created_date = (dr_datarow["created_date"].ToString()),
//        //                    status_flag = (dr_datarow["status_flag"].ToString()),
//        //                    approval_flag = (dr_datarow["approval_flag"].ToString()),
//        //                    approval_status = (dr_datarow["approval_status"].ToString()),

//        //                });
//        //            }
//        //            values.myaudittask_list = getmyaudittasks_list;
//        //        }
//        //        dt_datatable.Dispose();
//        //        values.status = true;
//        //    }
//        //}






//        //public void DaGetMyClosedAuditTask(MdlAtmTrnMyAuditTask values)
//        //{
//        //    try
//        //    {
//        //        msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//        //                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//        //                " FROM atm_trn_tauditcreation a" +
//        //                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//        //                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//        //                " where status = 'closed'" + " order by a.auditcreation_gid desc ";
//        //        dt_datatable = objdbconn.GetDataTable(msSQL);
//        //        var getmyaudittask_list = new List<myaudittask_list>();
//        //        if (dt_datatable.Rows.Count != 0)
//        //        {
//        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
//        //            {
//        //                getmyaudittask_list.Add(new myaudittask_list
//        //                {

//        //                    auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//        //                    checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//        //                    audit_name = (dr_datarow["audit_name"].ToString()),
//        //                    audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//        //                    audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//        //                    audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//        //                    due_date = (dr_datarow["due_date"].ToString()),
//        //                    status_update = (dr_datarow["status"].ToString()),
//        //                    created_by = (dr_datarow["created_by"].ToString()),
//        //                    created_date = (dr_datarow["created_date"].ToString()),

//        //                });
//        //            }
//        //            values.myaudittask_list = getmyaudittask_list;
//        //        }
//        //        dt_datatable.Dispose();
//        //        values.status = true;
//        //    }
//        //    catch
//        //    {
//        //        values.status = false;
//        //    }
//        //}





//        public void DaGetMyOpenAuditTask(MdlAtmTrnMyAuditTask values, string Employee_gid)
//        {
//            values.employee_gid = Employee_gid;
//            try
//            {
//                    msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,a.due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name, c.auditmaker_name as auditee_Maker, c.auditchecker_name as auditee_checker," +
//                    " b.employee_name as tag_user,a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,b.employee_gid as taguser_gid,c.employee_gid as auditeemaker_gid,c.auditmapping_gid as auditeechecker_gid,d.employee_gid as raisequery_gid,g.employee_gid as sampleraisequery_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
//                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//                    " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//                    " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//                   " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +
//                    " where ((a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//                    " (b.employee_gid='" + Employee_gid + "') " +
//                    " or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "')) and a.status = 'Open'";




//                //" SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,a.approval_status,a.status_flag,a.approval_flag,b.employee_gid as makeremployee_gid,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                //" concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
//                //" concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as auditmaker_name," +
//                //" concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as auditchecker_name" +
//                //" FROM atm_trn_tauditcreation a" +
//                //" left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                //"  left join hrm_mst_temployee d on a.auditmapping_gid = d.employee_gid" +
//                //" left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                //" left join adm_mst_tuser e on e.user_gid=d.user_gid" +
//                //" where a.employee_gid='" + Employee_gid + "'or a.auditmapping_gid='" + Employee_gid + "' and a.status = 'Open'";

//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                    var getmyaudittask_list = new List<myaudittask_list>();
//                    if (dt_datatable.Rows.Count != 0)
//                    {
//                        foreach (DataRow dr_datarow in dt_datatable.Rows)
//                        {
//                            getmyaudittask_list.Add(new myaudittask_list
//                            {

//                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                                checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//                                audit_name = (dr_datarow["audit_name"].ToString()),
//                                audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//                                audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//                                audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//                                auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
//                                auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
//                                due_date = (dr_datarow["due_date"].ToString()),
//                                status_update = (dr_datarow["status"].ToString()),
//                                created_by = (dr_datarow["created_by"].ToString()),
//                                created_date = (dr_datarow["created_date"].ToString()),
//                                status_flag = (dr_datarow["status_flag"].ToString()),
//                                approval_flag = (dr_datarow["approval_flag"].ToString()),
//                                //makeremployee_gid = (dr_datarow["makeremployee_gid"].ToString()),
//                                approval_status = (dr_datarow["approval_status"].ToString()),
//                            });
//                        }
//                        values.myaudittask_list = getmyaudittask_list;
//                    }
//                    dt_datatable.Dispose();
//                    values.status = true;
//                }
//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }

//        //        msSQL = " select f.auditcreation_gid,f.checklistmaster_gid,f.audit_name,f.audit_uniqueno,f.auditmaker_name,f.auditchecker_name,f.status,f.approval_status,f.status_flag,f.approval_flag,b.employee_gid as auditemployee_gid,d.employee_gid as checkeremployee_gid,date_format(f.due_date,'%d-%m-%Y') as due_date,date_format(f.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.auditmaker_name, a.employee_gid, a.auditchecker_name, a.auditmapping_gid," +
//        //       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
//        //       " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as auditmaker_name," +
//        //       " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as auditchecker_name" +
//        //       " from atm_mst_tchecklistmaster a" +
//        //       " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid" +
//        //       " left join hrm_mst_temployee d on a.auditmapping_gid = d.employee_gid" +
//        //       " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//        //       " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
//        //        " left join atm_trn_tauditcreation f on f.checklistmaster_gid = a.checklistmaster_gid" +
//        //       " where a.employee_gid='" + Employee_gid + "'or a.auditmapping_gid='" + Employee_gid + "' and f.status = 'Open'";

//        //        dt_datatable = objdbconn.GetDataTable(msSQL);

//        //        var getmyaudittasks_list = new List<myaudittask_list>();

//        //        if (dt_datatable.Rows.Count != 0)
//        //        {
//        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
//        //            {
//        //                getmyaudittasks_list.Add(new myaudittask_list
//        //                {

//        //                    auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//        //                    checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//        //                    audit_name = (dr_datarow["audit_name"].ToString()),
//        //                    audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//        //                    audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//        //                    audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//        //                    due_date = (dr_datarow["due_date"].ToString()),
//        //                    status_update = (dr_datarow["status"].ToString()),
//        //                    auditemployee_gid = (dr_datarow["auditemployee_gid"].ToString()),
//        //                    checkeremployee_gid = (dr_datarow["checkeremployee_gid"].ToString()),
//        //                    created_by = (dr_datarow["created_by"].ToString()),
//        //                    created_date = (dr_datarow["created_date"].ToString()),
//        //                    status_flag = (dr_datarow["status_flag"].ToString()),
//        //                    approval_flag = (dr_datarow["approval_flag"].ToString()),
//        //                    approval_status = (dr_datarow["approval_status"].ToString()),

//        //                });
//        //            }
//        //            values.myaudittask_list = getmyaudittasks_list;
//        //        }
//        //        dt_datatable.Dispose();
//        //        values.status = true;
//        //    }
//        //}






//        //public void DaGetMyOpenAuditTask(MdlAtmTrnMyAuditTask values)
//        //{
//        //    try
//        //    {
//        //        msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//        //                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//        //                " FROM atm_trn_tauditcreation a" +
//        //                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//        //                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//        //                " where status = 'Open'" + " order by a.auditcreation_gid desc ";
//        //        dt_datatable = objdbconn.GetDataTable(msSQL);
//        //        var getmyaudittask_list = new List<myaudittask_list>();
//        //        if (dt_datatable.Rows.Count != 0)
//        //        {
//        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
//        //            {
//        //                getmyaudittask_list.Add(new myaudittask_list
//        //                {

//        //                    auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//        //                    checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//        //                    audit_name = (dr_datarow["audit_name"].ToString()),
//        //                    audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//        //                    audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//        //                    audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//        //                    due_date = (dr_datarow["due_date"].ToString()),
//        //                    status_update = (dr_datarow["status"].ToString()),
//        //                    created_by = (dr_datarow["created_by"].ToString()),
//        //                    created_date = (dr_datarow["created_date"].ToString()),

//        //                });
//        //            }
//        //            values.myaudittask_list = getmyaudittask_list;
//        //        }
//        //        dt_datatable.Dispose();
//        //        values.status = true;
//        //    }
//        //    catch
//        //    {
//        //        values.status = false;
//        //    }
//        //}

//        public void DaGetMyOnholdAuditTask(MdlAtmTrnMyAuditTask values, string Employee_gid)
//        {
//            values.employee_gid = Employee_gid;
//            try
//            {

//                    msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,a.due_date,a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name, c.auditmaker_name as auditee_Maker, c.auditchecker_name as auditee_checker," +
//                    " b.employee_name as tag_user,a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid,b.employee_gid as taguser_gid,c.employee_gid as auditeemaker_gid,c.auditmapping_gid as auditeechecker_gid,d.employee_gid as raisequery_gid,g.employee_gid as sampleraisequery_gid, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by  from atm_trn_tauditcreation a " +
//                    " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//                    " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//                    " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//                    " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//                    " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//                   " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +
//                    " where ((a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//                    " (b.employee_gid='" + Employee_gid + "') " +
//                    " or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "')) and a.status = 'Hold'";


//                //" SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,a.approval_status,a.status_flag,a.approval_flag,b.employee_gid as makeremployee_gid,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                //" concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
//                //" concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as auditmaker_name," +
//                //" concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as auditchecker_name" +
//                //" FROM atm_trn_tauditcreation a" +
//                //" left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                //"  left join hrm_mst_temployee d on a.auditmapping_gid = d.employee_gid" +
//                //" left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                //" left join adm_mst_tuser e on e.user_gid=d.user_gid" +
//                //" where a.employee_gid='" + Employee_gid + "'or a.auditmapping_gid='" + Employee_gid + "' and a.status = 'Hold'";

//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                    var getmyaudittask_list = new List<myaudittask_list>();
//                    if (dt_datatable.Rows.Count != 0)
//                    {
//                        foreach (DataRow dr_datarow in dt_datatable.Rows)
//                        {
//                            getmyaudittask_list.Add(new myaudittask_list
//                            {

//                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                                checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//                                audit_name = (dr_datarow["audit_name"].ToString()),
//                                audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//                                audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//                                audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//                                auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
//                                auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
//                                due_date = (dr_datarow["due_date"].ToString()),
//                                status_update = (dr_datarow["status"].ToString()),
//                                created_by = (dr_datarow["created_by"].ToString()),
//                                created_date = (dr_datarow["created_date"].ToString()),
//                                status_flag = (dr_datarow["status_flag"].ToString()),
//                                approval_flag = (dr_datarow["approval_flag"].ToString()),
//                                approval_status = (dr_datarow["approval_status"].ToString()),
//                            });
//                        }
//                        values.myaudittask_list = getmyaudittask_list;
//                    }
//                    dt_datatable.Dispose();
//                    values.status = true;
//                }

//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }

//        //    else
//        //    {
//        //        msSQL = " select f.auditcreation_gid,f.checklistmaster_gid,f.audit_name,f.audit_uniqueno,f.auditmaker_name,f.auditchecker_name,f.status,f.approval_status,f.status_flag,f.approval_flag,b.employee_gid as auditemployee_gid,d.employee_gid as checkeremployee_gid,date_format(f.due_date,'%d-%m-%Y') as due_date,date_format(f.created_date,'%d-%m-%Y %h:%i %p') as created_date, a.auditmaker_name, a.employee_gid, a.auditchecker_name, a.auditmapping_gid," +
//        //       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
//        //       " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as auditmaker_name," +
//        //       " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as auditchecker_name" +
//        //       " from atm_mst_tchecklistmaster a" +
//        //       " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid" +
//        //       " left join hrm_mst_temployee d on a.auditmapping_gid = d.employee_gid" +
//        //       " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//        //       " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
//        //        " left join atm_trn_tauditcreation f on f.checklistmaster_gid = a.checklistmaster_gid" +
//        //       " where a.employee_gid='" + Employee_gid + "'or a.auditmapping_gid='" + Employee_gid + "' and f.status = 'Hold'";

//        //        dt_datatable = objdbconn.GetDataTable(msSQL);

//        //        var getmyaudittasks_list = new List<myaudittask_list>();

//        //        if (dt_datatable.Rows.Count != 0)
//        //        {
//        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
//        //            {
//        //                getmyaudittasks_list.Add(new myaudittask_list
//        //                {

//        //                    auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//        //                    checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//        //                    audit_name = (dr_datarow["audit_name"].ToString()),
//        //                    audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//        //                    audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//        //                    audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//        //                    due_date = (dr_datarow["due_date"].ToString()),
//        //                    status_update = (dr_datarow["status"].ToString()),
//        //                    auditemployee_gid = (dr_datarow["auditemployee_gid"].ToString()),
//        //                    checkeremployee_gid = (dr_datarow["checkeremployee_gid"].ToString()),
//        //                    created_by = (dr_datarow["created_by"].ToString()),
//        //                    created_date = (dr_datarow["created_date"].ToString()),
//        //                    status_flag = (dr_datarow["status_flag"].ToString()),
//        //                    approval_flag = (dr_datarow["approval_flag"].ToString()),
//        //                    approval_status = (dr_datarow["approval_status"].ToString()),

//        //                });
//        //            }
//        //            values.myaudittask_list = getmyaudittasks_list;
//        //        }

//        //        dt_datatable.Dispose();
//        //        values.status = true;

//        //    }
//        //}



//        //public void DaGetMyOnholdAuditTask(MdlAtmTrnMyAuditTask values)
//        //{
//        //    try
//        //    {
//        //        msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//        //                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//        //                " FROM atm_trn_tauditcreation a" +
//        //                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//        //                " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//        //                " where status = 'Hold'" + " order by a.auditcreation_gid desc ";
//        //        dt_datatable = objdbconn.GetDataTable(msSQL);
//        //        var getmyaudittask_list = new List<myaudittask_list>();
//        //        if (dt_datatable.Rows.Count != 0)
//        //        {
//        //            foreach (DataRow dr_datarow in dt_datatable.Rows)
//        //            {
//        //                getmyaudittask_list.Add(new myaudittask_list
//        //                {

//        //                    auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//        //                    checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
//        //                    audit_name = (dr_datarow["audit_name"].ToString()),
//        //                    audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
//        //                    audit_maker = (dr_datarow["auditmaker_name"].ToString()),
//        //                    audit_checker = (dr_datarow["auditchecker_name"].ToString()),
//        //                    due_date = (dr_datarow["due_date"].ToString()),
//        //                    status_update = (dr_datarow["status"].ToString()),
//        //                    created_by = (dr_datarow["created_by"].ToString()),
//        //                    created_date = (dr_datarow["created_date"].ToString()),

//        //                });
//        //            }
//        //            values.myaudittask_list = getmyaudittask_list;
//        //        }
//        //        dt_datatable.Dispose();
//        //        values.status = true;
//        //    }
//        //    catch
//        //    {
//        //        values.status = false;
//        //    }
//        //}

//        public void DaGetMyAuditTaskCounts(MdlAtmTrnMyAuditTask values, string Employee_gid)
//        {
//            msSQL = " select count(auditcreation_gid) as auditsonhold_count from atm_trn_tauditcreation where status = 'Hold'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.auditsonhold_count = objODBCDatareader["auditsonhold_count"].ToString();

//            }
//            objODBCDatareader.Close();

//            msSQL = "select count(auditcreation_gid) as openaudit_count from atm_trn_tauditcreation where status = 'open' ";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows)
//            {
//                values.openaudit_count = objODBCDatareader["openaudit_count"].ToString();

//            }

//            objODBCDatareader.Close();

//            msSQL = "select count(auditcreation_gid) as closedaudit_count from atm_trn_tauditcreation where status = 'closed' ";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows)
//            {
//                values.closedaudit_count = objODBCDatareader["closedaudit_count"].ToString();

//            }

//            objODBCDatareader.Close();
//        }

//        //    values.employee_gid = Employee_gid;

//            //    msSQL = " select count(a.auditcreation_gid) as auditsonhold_count from atm_trn_tauditcreation a "+
//            //        " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//            //            " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//            //            " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//            //            " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//            //            " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//            //           " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +

//            //        "where ((a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//            //            " (b.employee_gid='" + Employee_gid + "') " +
//            //            " or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "')) and status = 'Hold'";
//            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            //    if (objODBCDatareader.HasRows == true)
//            //    {
//            //        values.auditsonhold_count = objODBCDatareader["auditsonhold_count"].ToString();

//            //    }
//            //    objODBCDatareader.Close();

//            //    msSQL = " select count(a.auditcreation_gid) as openaudit_count from atm_trn_tauditcreation a " +
//            //        " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//            //            " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//            //            " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//            //            " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//            //            " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//            //           " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +
//            //        "where ((a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//            //" (b.employee_gid='" + Employee_gid + "') " +
//            //" or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "')) and  status = 'open' ";
//            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            //    if (objODBCDatareader.HasRows)
//            //    {
//            //        values.openaudit_count = objODBCDatareader["openaudit_count"].ToString();

//            //    }

//            //    objODBCDatareader.Close();

//            //    msSQL = " select count(a.auditcreation_gid) as closedaudit_count from atm_trn_tauditcreation a " +
//            //        " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
//            //            " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
//            //            " left join atm_mst_ttaguser2employee b on a.auditcreation_gid = b.auditcreation_gid " +
//            //            " left join atm_mst_tchecklistmaster c on a.checklistmaster_gid = c.checklistmaster_gid " +
//            //            " left join atm_trn_traisequery d on a.auditcreation_gid = d.auditcreation_gid " +
//            //           " left join atm_trn_tsampleraisequery g on a.auditcreation_gid = g.auditcreation_gid " +
//            //        "where ((a.employee_gid='" + Employee_gid + "' or a.auditmapping_gid ='" + Employee_gid + "' or a.auditmapping2employee_gid ='" + Employee_gid + "')  or " +
//            //" (b.employee_gid='" + Employee_gid + "') " +
//            //" or (c.employee_gid='" + Employee_gid + "' or c.auditmapping_gid='" + Employee_gid + "') or (d.employee_gid='" + Employee_gid + "') or (g.employee_gid='" + Employee_gid + "')) and  status = 'closed' ";
//            //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            //    if (objODBCDatareader.HasRows)
//            //    {
//            //        values.closedaudit_count = objODBCDatareader["closedaudit_count"].ToString();

//            //    }

//            //    objODBCDatareader.Close();

//            //}


//            //public void DaGetInitiateapproval(MdlAtmTrnMyAuditTask values, string employee_gid)
//            //{
//            //    msSQL = " update atm_trn_tauditcreation set initiate_action='" + values.initiate_action + "'," +
//            //            " remarks='" + values.remarks.Replace("'", "") + "'" +
//            //            " where auditcreation_gid='" + values.auditcreation_gid + "' ";
//            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//            //    if (mnResult != 0)
//            //    {
//            //        msGetGid = objcmnfunctions.GetMasterGID("AIAL");

//            //        msSQL = " insert into atm_mst_tinitiateactionlog (" +
//            //              " initiateactionlog_gid, " +
//            //              " auditcreation_gid," +
//            //              " audit_name," +
//            //              " initiate_action," +
//            //              " remarks," +
//            //              " updated_by," +
//            //              " updated_date) " +
//            //              " values (" +
//            //              " '" + msGetGid + "'," +
//            //              " '" + values.auditcreation_gid + "'," +
//            //              " '" + values.audit_name + "'," +
//            //              " '" + values.initiate_action + "'," +
//            //              " '" + values.remarks.Replace("'", "") + "'," +
//            //              " '" + employee_gid + "'," +
//            //              " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//            //        values.status = true;
//            //        values.message = "Action Initiated Successfully";

//            //    }
//            //    else
//            //    {
//            //        values.status = false;
//            //        values.message = "Error Occurred";
//            //    }
//            //}


//        public void DaGetAuditCreationIntent(string auditcreation_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = " select checkpoint_intent  from atm_trn_tauditcreation2checklist " +
//                  " where auditcreation_gid='" + auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
//            }
//            objODBCDatareader.Close();

//        }
//        public void DaGetAuditCreationDescription(string auditcreation_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = " select checkpoint_description  from atm_trn_tauditcreation2checklist " +
//                  " where auditcreation_gid='" + auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
//            }
//            objODBCDatareader.Close();

//        }
//        public void DaGetAuditCreationAuditor(string auditcreation_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = " select noteto_auditor  from atm_trn_tauditcreation2checklist " +
//                  " where auditcreation_gid='" + auditcreation_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
//            }
//            objODBCDatareader.Close();

//        }   
//        public bool DaPostTempApprovalMember(string user_gid, initialapprovaldtl values)
//        {
//            msGetGid = objcmnfunctions.GetMasterGID("TEAM");
//            msSQL = " insert into atm_tmp_tapprovalmember(" +
//                    " tmpapprovalmember_gid," +
//                    " auditcreation_gid," +
//                    " approval_gid," +
//                    " approval_name," +
//                    " created_by, " +
//                    " created_date)" +
//                    " values(" +
//                     "'" + msGetGid + "'," +
//                    "'" + values.auditcreation_gid + "'," +
//                    "'" + values.approvalgid + "'," +
//                    "'" + values.approvalname + "'," +
//                    "'" + user_gid + "'," +
//                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//            if (mnResult != 0)
//            {

//                values.status = true;
//                return true;
//            }
//            else
//            {
//                values.status = false;
//                return false;
//            }
//        }
//        public void DaGetTmpAllMembersDelete(initialapprovaldtl values)
//        {
//            msSQL = " delete from atm_tmp_tapprovalmember";
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//        }
//        public void DaGetTmpApprovalMembersView(string auditcreation_gid, initialapprovaldtl values)
//        {

//            msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from atm_tmp_tapprovalmember" +
//                    " where auditcreation_gid='" + auditcreation_gid + "' order by created_date asc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var gettagmemberdtl = new List<approvalmember>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    gettagmemberdtl.Add(new approvalmember
//                    {
//                        employee_name = dt["approval_name"].ToString(),
//                        employee_gid = dt["approval_gid"].ToString(),
//                        tmpapprovalmember_gid = dt["tmpapprovalmember_gid"].ToString(),
//                    });
//                    values.approvalmember = gettagmemberdtl;
//                }
//            }
//            dt_datatable.Dispose();
//        }

//        public void DaGetTmpApprovalMembersDelete(initialapprovaldtl values)
//        {
//            msSQL = " delete from atm_tmp_tapprovalmember where tmpapprovalmember_gid='" + values.tmpapprovalmember_gid + "'";
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


//            msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from atm_tmp_tapprovalmember" +
//        " where auditcreation_gid='" + values.auditcreation_gid + "' order by created_date asc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var gettagmemberdtl = new List<approvalmember>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    gettagmemberdtl.Add(new approvalmember
//                    {
//                        employee_name = dt["approval_name"].ToString(),
//                        employee_gid = dt["approval_gid"].ToString(),
//                        tmpapprovalmember_gid = dt["tmpapprovalmember_gid"].ToString(),
//                    });
//                    values.approvalmember = gettagmemberdtl;
//                }
//            }
//            dt_datatable.Dispose();

            
//        }


//        public void DaPostApprovalGet(initialapprovaldtl values, string employee_gid)
//        {

//            msSQL = " select tmpapprovalmember_gid, approval_name, approval_gid, created_date from atm_tmp_tapprovalmember" +
//                    " where auditcreation_gid='" + values.auditcreation_gid + "' order by created_date asc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var gettagmemberdtl = new List<approvalmember>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    var lsemployee_name = dt["approval_name"].ToString();
//                    var lsemployee_gid = dt["approval_gid"].ToString();

//                    msGetGid = objcmnfunctions.GetMasterGID("INIT");

//                    msSQL = "Insert into atm_trn_tinitialapproval( " +
//                           " initialapproval_gid, " +
//                           " auditcreation_gid," +
//                           " approval_gid," +
//                           " approval_name," +
//                           " approval_type," +
//                           " approval_status," +
//                           " approval_remarks," +
//                           " created_by," +
//                           " created_date)" +
//                           " values(" +
//                           "'" + msGetGid + "'," +
//                           "'" + values.auditcreation_gid + "'," +
//                           "'" + lsemployee_gid + "'," +
//                           "'" + lsemployee_name + "'," +
//                           "'" + values.approve_type + "'," +
//                           "'Initiated'," +
//                           "'" + values.approve_remarks + "'," +
//                           "'" + employee_gid + "'," +
//                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                }

//                if (mnResult != 0)
//                {
//                    msSQL = "update atm_trn_tauditcreation set approval_status ='Initiated'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                    msSQL = "update atm_trn_tauditcreation set approval_flag ='Y'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                    values.status = true;
//                    values.message = "Approval Initiated  Successfully Assigned ..!";
//                }
//                else
//                {
//                    values.status = false;
//                    values.message = "Error Occured..!";
//                }
//                dt_datatable.Dispose();
//            }
//        }
//        public void DaGetInitialApproval(initialapprovaldtl values, string Employee_gid)
//        {

//            msSQL = " SELECT a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,a.approval_remarks,a.initialapproval_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                " FROM atm_trn_tinitialapproval a" +
//                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
//                    " where a.approval_gid='" + Employee_gid + "' and a.initialapproval_flag = 'N'";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getinitialapproval_list = new List<initialapproval_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dr_datarow in dt_datatable.Rows)
//                {
//                    getinitialapproval_list.Add(new initialapproval_list
//                    {
//                        initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
//                        auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                        approval_gid = (dr_datarow["approval_gid"].ToString()),
//                        approval_name = (dr_datarow["approval_name"].ToString()),
//                        approval_type = (dr_datarow["approval_type"].ToString()),
//                        approval_status = (dr_datarow["approval_status"].ToString()),
//                        approve_remarks = (dr_datarow["approval_remarks"].ToString()),
//                        created_by = (dr_datarow["created_by"].ToString()),
//                        created_date = (dr_datarow["created_date"].ToString()),
//                    });
//                }
//                values.initialapproval_list = getinitialapproval_list;

//            }
//            dt_datatable.Dispose();

//            msSQL = " SELECT a.observationapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,a.approve_remark,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                 " FROM atm_trn_tobservationapproval a" +
//                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
//                     " where a.approval_gid='" + Employee_gid + "' and (a.approval_status = 'Checker Approved' or a.approval_status = 'Checker Rejected')order by a.created_date desc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getapprovalhistory_list = new List<approvalhistory_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dr_datarow in dt_datatable.Rows)
//                {
//                    getapprovalhistory_list.Add(new approvalhistory_list
//                    {
//                        observationapproval_gid = (dr_datarow["observationapproval_gid"].ToString()),
//                        initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
//                        auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                        approval_gid = (dr_datarow["approval_gid"].ToString()),
//                        approval_name = (dr_datarow["approval_name"].ToString()),
//                        approval_type = (dr_datarow["approval_type"].ToString()),
//                        approval_status = (dr_datarow["approval_status"].ToString()),
//                        approve_remark = (dr_datarow["approve_remark"].ToString()),
//                        created_by = (dr_datarow["created_by"].ToString()),
//                        created_date = (dr_datarow["created_date"].ToString()),
//                    });
//                }
//                values.approvalhistory_list = getapprovalhistory_list;
//            }

//            dt_datatable.Dispose();
//        }

//        public void DaGetInitialApprovalView(initialapprovaldtl values, string initialapproval_gid)
//        {
//            try
//            {
//                msSQL = " SELECT a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                        " FROM atm_trn_tinitialapproval a" +
//                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                         " where a.initialapproval_gid='" + initialapproval_gid + "'";
//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getinitialapprovalview_list = new List<initialapprovalview_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getinitialapprovalview_list.Add(new initialapprovalview_list
//                        {
//                            initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            approval_gid = (dr_datarow["approval_gid"].ToString()),
//                            approval_name = (dr_datarow["approval_name"].ToString()),
//                            approval_type = (dr_datarow["approval_type"].ToString()),
//                            approval_status = (dr_datarow["approval_status"].ToString()),
//                            created_by = (dr_datarow["created_by"].ToString()),
//                            created_date = (dr_datarow["created_date"].ToString()),
//                        });
//                    }
//                    values.initialapprovalview_list = getinitialapprovalview_list;

//                }
//                dt_datatable.Dispose();
//                values.status = true;

//            }
//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }
//        public void DaPostObservationApproval(initialapprovaldtl values, string employee_gid)
//        {

//            msSQL = " SELECT a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                         " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                         " FROM atm_trn_tinitialapproval a" +
//                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                         " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                          " where a.initialapproval_gid='" + values.initialapproval_gid + "'";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getinitialapprovalview_list = new List<initialapprovalview_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    var lsinitialapproval_gid = dt["initialapproval_gid"].ToString();
//                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
//                    var lsapproval_gid = dt["approval_gid"].ToString();
//                    var lsapproval_name = dt["approval_name"].ToString();
//                    var lsapproval_type = dt["approval_type"].ToString();

//                    msGetGid = objcmnfunctions.GetMasterGID("OBAP");

//                    msSQL = "Insert into atm_trn_tobservationapproval( " +
//                            " observationapproval_gid, " +
//                           " initialapproval_gid, " +
//                           " auditcreation_gid," +
//                           " approval_gid," +
//                           " approval_name," +
//                           " approval_type," +
//                           " approval_status," +
//                           " approve_remark," +
//                           " created_by," +
//                           " created_date)" +
//                           " values(" +
//                           "'" + msGetGid + "'," +
//                             "'" + lsinitialapproval_gid + "'," +
//                           "'" + lsauditcreation_gid + "'," +
//                           "'" + lsapproval_gid + "'," +
//                           "'" + lsapproval_name + "'," +
//                           "'" + lsapproval_type + "'," +
//                           "'Checker Approved'," +
//                           "'" + values.approve_remark + "'," +
//                           "'" + employee_gid + "'," +
//                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                }
//                msSQL = "update atm_trn_tauditcreation set approval_status ='Checker Approved'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                if (mnResult != 0)
//                {

//                    msSQL = "update atm_trn_tauditcreation set approval_flag ='Y'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    msSQL = "update atm_trn_tinitialapproval set initialapproval_flag ='Y'  where initialapproval_gid = '" + values.initialapproval_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    values.status = true;
//                    values.message = "Checker Approved  Successfully Assigned ..!";
//                }
//                else
//                {
//                    values.status = false;
//                    values.message = "Error Occured..!";
//                }
//                dt_datatable.Dispose();
//            }
//        }

//        public void DaPostObservationRejected(initialapprovaldtl values, string employee_gid)
//        {

//            msSQL = " SELECT a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                         " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                         " FROM atm_trn_tinitialapproval a" +
//                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                         " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                          " where a.initialapproval_gid='" + values.initialapproval_gid + "'";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getinitialapprovalview_list = new List<initialapprovalview_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    var lsinitialapproval_gid = dt["initialapproval_gid"].ToString();
//                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
//                    var lsapproval_gid = dt["approval_gid"].ToString();
//                    var lsapproval_name = dt["approval_name"].ToString();
//                    var lsapproval_type = dt["approval_type"].ToString();

//                    msGetGid = objcmnfunctions.GetMasterGID("OBAP");

//                    msSQL = "Insert into atm_trn_tobservationapproval( " +
//                            " observationapproval_gid, " +
//                           " initialapproval_gid, " +
//                           " auditcreation_gid," +
//                           " approval_gid," +
//                           " approval_name," +
//                           " approval_type," +
//                           " approval_status," +
//                            " rejected_remark," +
//                           " created_by," +
//                           " created_date)" +
//                           " values(" +
//                           "'" + msGetGid + "'," +
//                             "'" + lsinitialapproval_gid + "'," +
//                           "'" + lsauditcreation_gid + "'," +
//                           "'" + lsapproval_gid + "'," +
//                           "'" + lsapproval_name + "'," +
//                           "'" + lsapproval_type + "'," +
//                           "'Checker Rejected'," +
//                            "'" + values.rejected_remark + "'," +
//                           "'" + employee_gid + "'," +
//                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                }
//                msSQL = "update atm_trn_tauditcreation set approval_status ='Checker Rejected'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                if (mnResult != 0)
//                {
//                    msSQL = "update atm_trn_tauditcreation set approval_flag ='N'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    msSQL = "update atm_trn_tinitialapproval set initialapproval_flag ='Y'  where initialapproval_gid = '" + values.initialapproval_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    values.status = true;
//                    values.message = " Checker Rejected  Successfully  ..!";
//                }
//                else
//                {
//                    values.status = false;
//                    values.message = "Error Occured..!";
//                }
//                dt_datatable.Dispose();
//            }
//        }
//        public void DaGetAuditApprovalView(initialapprovaldtl values, string observationapproval_gid)
//        {
//            try
//            {
//                msSQL = " SELECT a.observationapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
//                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                        " FROM atm_trn_tobservationapproval a" +
//                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
//                         " where a.observationapproval_gid='" + observationapproval_gid + "'";
//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getobservationapprovalview_list = new List<observationapprovalview_list>();
//                if (dt_datatable.Rows.Count != 0)
//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getobservationapprovalview_list.Add(new observationapprovalview_list
//                        {
//                            observationapproval_gid = (dr_datarow["observationapproval_gid"].ToString()),
//                            initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
//                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                            approval_gid = (dr_datarow["approval_gid"].ToString()),
//                            approval_name = (dr_datarow["approval_name"].ToString()),
//                            approval_type = (dr_datarow["approval_type"].ToString()),
//                            approval_status = (dr_datarow["approval_status"].ToString()),
//                            created_by = (dr_datarow["created_by"].ToString()),
//                            created_date = (dr_datarow["created_date"].ToString()),
//                        });
//                    }
//                    values.observationapprovalview_list = getobservationapprovalview_list;

//                }
//                dt_datatable.Dispose();
//                values.status = true;

//            }
//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }
//        public void DaPostAuditApproval(initialapprovaldtl values, string employee_gid)
//        {

//            msSQL = " SELECT a.observationapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,a.approve_remark,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                     " FROM atm_trn_tobservationapproval a" +
//                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                      " left join atm_trn_tauditcreation d on a.auditcreation_gid = d.auditcreation_gid" +
//                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
//                     " where d.auditmapping2employee_gid='" + employee_gid + "' and(a.approval_status = 'Checker Approved' or a.approval_status = 'Checker Rejected')order by a.created_date desc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getinitialapprovalview_list = new List<approvalhistory_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    var lsobservationapproval_gid = dt["observationapproval_gid"].ToString();
//                    var lsinitialapproval_gid = dt["initialapproval_gid"].ToString();
//                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
//                    var lsapproval_gid = dt["approval_gid"].ToString();
//                    var lsapproval_name = dt["approval_name"].ToString();
//                    var lsapproval_type = dt["approval_type"].ToString();

//                    msGetGid = objcmnfunctions.GetMasterGID("AUAP");

//                    msSQL = "Insert into atm_trn_tauditapproval( " +
//                           " auditapproval_gid, " +
//                           " observationapproval_gid, " +
//                           " initialapproval_gid, " +
//                           " auditcreation_gid," +
//                           " approval_gid," +
//                           " approval_name," +
//                           " approval_type," +
//                           " approval_status," +
//                           " approve_remark," +
//                           " created_by," +
//                           " created_date)" +
//                           " values(" +
//                           "'" + msGetGid + "'," +
//                           "'" + lsobservationapproval_gid + "'," +
//                           "'" + lsinitialapproval_gid + "'," +
//                           "'" + lsauditcreation_gid + "'," +
//                           "'" + lsapproval_gid + "'," +
//                           "'" + lsapproval_name + "'," +
//                           "'" + lsapproval_type + "'," +
//                           "' Approved'," +
//                           "'" + values.approve_remark + "'," +
//                           "'" + employee_gid + "'," +
//                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                }
//                msSQL = "update atm_trn_tauditcreation set approval_status ='Approved'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                if (mnResult != 0)
//                {
//                    msSQL = "update atm_trn_tauditcreation set status ='Closed'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    msSQL = "update atm_trn_tauditcreation set approval_flag ='Y'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    msSQL = "update atm_trn_tobservationapproval set observationapproval_flag ='Y'  where observationapproval_gid = '" + values.observationapproval_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    values.status = true;
//                    values.message = "Approval  Successfully Assigned ..!";
//                }
//                else
//                {
//                    values.status = false;
//                    values.message = "Error Occured..!";
//                }
//                dt_datatable.Dispose();
//            }
//        }

//        public void DaPostAuditRejected(initialapprovaldtl values, string employee_gid)
//        {

//            msSQL = " SELECT a.observationapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,a.approve_remark,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                     " FROM atm_trn_tobservationapproval a" +
//                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                      " left join atm_trn_tauditcreation d on a.auditcreation_gid = d.auditcreation_gid" +
//                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
//                     " where d.auditmapping2employee_gid='" + employee_gid + "' and(a.approval_status = 'Checker Approved' or a.approval_status = 'Checker Rejected')order by a.created_date desc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getinitialapprovalview_list = new List<approvalhistory_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dt in dt_datatable.Rows)
//                {
//                    var lsobservationapproval_gid = dt["observationapproval_gid"].ToString();
//                    var lsinitialapproval_gid = dt["initialapproval_gid"].ToString();
//                    var lsauditcreation_gid = dt["auditcreation_gid"].ToString();
//                    var lsapproval_gid = dt["approval_gid"].ToString();
//                    var lsapproval_name = dt["approval_name"].ToString();
//                    var lsapproval_type = dt["approval_type"].ToString();

//                    msGetGid = objcmnfunctions.GetMasterGID("AUAP");

//                    msSQL = "Insert into atm_trn_tobservationapproval( " +
//                           " auditapproval_gid, " +
//                           " observationapproval_gid, " +
//                           " initialapproval_gid, " +
//                           " auditcreation_gid," +
//                           " approval_gid," +
//                           " approval_name," +
//                           " approval_type," +
//                           " approval_status," +
//                            " rejected_remark," +
//                           " created_by," +
//                           " created_date)" +
//                           " values(" +
//                           "'" + msGetGid + "'," +
//                           "'" + lsobservationapproval_gid + "'," +
//                           "'" + lsinitialapproval_gid + "'," +
//                           "'" + lsauditcreation_gid + "'," +
//                           "'" + lsapproval_gid + "'," +
//                           "'" + lsapproval_name + "'," +
//                           "'" + lsapproval_type + "'," +
//                           "'Rejected'," +
//                            "'" + values.approve_remark + "'," +
//                           "'" + employee_gid + "'," +
//                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//                }
//                msSQL = "update atm_trn_tauditcreation set approval_status ='Rejected'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                if (mnResult != 0)
//                {
//                    msSQL = "update atm_trn_tauditcreation set approval_flag ='N'  where auditcreation_gid = '" + values.auditcreation_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                  
//                    msSQL = "update atm_trn_tobservationapproval set observationapproval_flag ='Y'  where observationapproval_gid = '" + values.observationapproval_gid + "'";
//                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

//                    values.status = true;
//                    values.message = "Approval Rejected Successfully ..!";
//                }
//                else
//                {
//                    values.status = false;
//                    values.message = "Error Occured..!";
//                }
//                dt_datatable.Dispose();
//            }
//        }
//        public void DaGetAuditApprovalHistory(initialapprovaldtl values, string Employee_gid)
//        {

//            msSQL = " SELECT a.auditapproval_gid,a.observationapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,a.approve_remark,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                    " FROM atm_trn_tauditapproval a" +
//                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
//                    " where a.created_by='" + Employee_gid + "' or (a.approval_status = 'Approved' or a.approval_status = 'Rejected')order by a.created_date desc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getauditapprovalhistory_list = new List<auditapprovalhistory_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dr_datarow in dt_datatable.Rows)
//                {
//                    getauditapprovalhistory_list.Add(new auditapprovalhistory_list
//                    {
//                        observationapproval_gid = (dr_datarow["observationapproval_gid"].ToString()),
//                        initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
//                        auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                        approval_gid = (dr_datarow["approval_gid"].ToString()),
//                        approval_name = (dr_datarow["approval_name"].ToString()),
//                        approval_type = (dr_datarow["approval_type"].ToString()),
//                        approval_status = (dr_datarow["approval_status"].ToString()),
//                        approve_remark = (dr_datarow["approve_remark"].ToString()),
//                        created_by = (dr_datarow["created_by"].ToString()),
//                        created_date = (dr_datarow["created_date"].ToString()),
//                    });
//                }
//                values.auditapprovalhistory_list = getauditapprovalhistory_list;
//            }

//            dt_datatable.Dispose();
//        }
//        public void DaGetObservationApproval(initialapprovaldtl values, string Employee_gid)
//        {
          
//            msSQL = " SELECT a.observationapproval_gid,a.initialapproval_gid,a.auditcreation_gid,a.approval_gid,a.approval_name,a.approval_type,a.approval_status,a.approve_remark,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
//                     " FROM atm_trn_tobservationapproval a" +
//                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
//                      " left join atm_trn_tauditcreation d on a.auditcreation_gid = d.auditcreation_gid" +
//                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
//                     " where d.auditmapping2employee_gid='" + Employee_gid + "' and a.observationapproval_flag = 'N'and(a.approval_status = 'Checker Approved' or a.approval_status = 'Checker Rejected')order by a.created_date desc";
//            dt_datatable = objdbconn.GetDataTable(msSQL);
//            var getapprovalhistory_list = new List<approvalhistory_list>();
//            if (dt_datatable.Rows.Count != 0)
//            {
//                foreach (DataRow dr_datarow in dt_datatable.Rows)
//                {
//                    getapprovalhistory_list.Add(new approvalhistory_list
//                    {
//                        observationapproval_gid = (dr_datarow["observationapproval_gid"].ToString()),
//                        initialapproval_gid = (dr_datarow["initialapproval_gid"].ToString()),
//                        auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
//                        approval_gid = (dr_datarow["approval_gid"].ToString()),
//                        approval_name = (dr_datarow["approval_name"].ToString()),
//                        approval_type = (dr_datarow["approval_type"].ToString()),
//                        approval_status = (dr_datarow["approval_status"].ToString()),
//                        approve_remark = (dr_datarow["approve_remark"].ToString()),
//                        created_by = (dr_datarow["created_by"].ToString()),
//                        created_date = (dr_datarow["created_date"].ToString()),
//                    });
//                }
//                values.approvalhistory_list = getapprovalhistory_list;
//            }

//            dt_datatable.Dispose();
//        }



//        public void DaPostRaiseQuery(MdlAtmTrnMyAuditTask values, string employee_gid)
//        {

//            msSQL = " update atm_trn_tauditcreation set description='" + values.description.Replace("'", "") + "'" +
//                   " where auditcreation_gid='" + values.auditcreation_gid + "' ";
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


//            for (var i = 0; i < values.employe.Count; i++)
//            {
//                msGetGid = objcmnfunctions.GetMasterGID("AURQ");
//                msSQL = "Insert into atm_trn_traisequery( " +
//                       " raisequery_gid, " +
//                       " auditcreation_gid," +
//                       " description," +
//                       " employee_gid," +
//                       " employee_name," +
//                       " created_by," +
//                       " created_date)" +
//                       " values(" +
//                       "'" + msGetGid + "'," +
//                       "'" + values.auditcreation_gid + "', " +
//                       "'" + values.description.Replace("'", "") + "'," +
//                       "'" + values.employe[i].employee_gid + "'," +
//                       "'" + values.employe[i].employee_name + "'," +
//                       "'" + employee_gid + "'," +
//                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//            }

//            if (mnResult != 0)
//            {
//                values.status = true;
//                values.message = "Query Raised Added Successfully";
//            }
//            else
//            {
//                values.message = "Error Occured While Raising Query";
//                values.status = false;
//            }
//        }


//        public void DaAssignedQuerySummary(string employee_gid, MdlAtmTrnMyAuditTask values, string auditcreation_gid)
//        {
//            try
//            {
//                msSQL = " SELECT a.raisequery_gid, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, a.description, " +
//                    " (a.employee_name) as assigned_to,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                    "  a.employee_gid " +
//                    " FROM atm_trn_traisequery a " +
//                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
//                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
//                    " left join hrm_mst_temployee f on f.employee_gid = a.employee_gid " +
//                    " left join adm_mst_tuser e on e.user_gid = f.user_gid "+
//                " WHERE a.auditcreation_gid= '" + auditcreation_gid + "'";
//                dt_datatable = objdbconn.GetDataTable(msSQL);

//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getAssignedQueryList = new List<AssignedQueryList>();
//                if (dt_datatable.Rows.Count != 0)

//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getAssignedQueryList.Add(new AssignedQueryList

//                        {

//                            raisequery_gid = (dr_datarow["raisequery_gid"].ToString()),
//                            assigned_by = (dr_datarow["created_by"].ToString()),
//                            assigned_to = (dr_datarow["assigned_to"].ToString()),
//                            assigned_date = (dr_datarow["created_date"].ToString()),
//                            description = (dr_datarow["description"].ToString()),

//                        });
//                    }
//                    values.AssignedQueryList = getAssignedQueryList;
//                }

//                dt_datatable.Dispose();
//                values.status = true;
//                values.message = "Success";
//            }
//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }


//        public void DaEditAssignedQuery(string raisequery_gid, MdlAtmTrnMyAuditTask values)
//        {
//            msSQL = " select raisequery_gid from atm_trn_traisequery " +
//                    " where raisequery_gid='" + raisequery_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.raisequery_gid = objODBCDatareader["raisequery_gid"].ToString();

//            }
//            objODBCDatareader.Close();
//            values.status = true;
//        }


//        public void DaPostReplyToQuery(MdlAtmTrnMyAuditTask values, string employee_gid)
//        {

//            msSQL = " update atm_trn_traisequery set reply_query='" + values.reply_query.Replace("'", "") + "'" +
//                   " where raisequery_gid='" + values.raisequery_gid + "' ";
//            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);         
//            {
//                msGetGid = objcmnfunctions.GetMasterGID("AROQ");
//                msSQL = "Insert into atm_trn_treplytoquery( " +
//                       " replytoquery_gid, " +
//                       " raisequery_gid, " +
//                       " reply_query," +                     
//                       " updated_by," +
//                       " updated_date)" +
//                       " values(" +
//                       "'" + msGetGid + "'," +
//                       "'" + values.raisequery_gid + "'," +
//                        "'" + values.reply_query.Replace("'", "") + "'," +                      
//                       "'" + employee_gid + "'," +
//                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
//                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
//            }

//            if (mnResult != 0)
//            {
//                values.status = true;
//                values.message = "Replied to Query Successfully";
//            }
//            else
//            {
//                values.message = "Error Occured While Replying to Query";
//                values.status = false;
//            }
//        }

//        public void DaRepliedQuerySummary(string employee_gid, MdlAtmTrnMyAuditTask values, string auditcreation_gid)
//        {
//            try
//            {
//                msSQL = " SELECT a.raisequery_gid, b.replytoquery_gid, b.reply_query, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, a.description, " +
//                    " (a.employee_name) as assigned_to, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as updated_by, date_format(b.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
//                    "  a.employee_gid " +
//                    " FROM atm_trn_traisequery a " +
//                    " left join atm_trn_treplytoquery b on a.raisequery_gid = b.raisequery_gid " +
//                    " left join adm_mst_tuser c on a.created_by = c.user_gid " +
//                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
//                    " left join hrm_mst_temployee f on f.employee_gid = b.updated_by " +
//                    " left join hrm_mst_temployee h on h.employee_gid = a.created_by " +
//                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
//                    " left join adm_mst_tuser e on e.user_gid = h.user_gid " +
//                " WHERE a.auditcreation_gid= '" + auditcreation_gid + "'";


//                //" WHERE b.employee_gid= '" + employee_gid + "'";
//                dt_datatable = objdbconn.GetDataTable(msSQL);

//                dt_datatable = objdbconn.GetDataTable(msSQL);
//                var getReplyToQueryList = new List<ReplyToQueryList>();
//                if (dt_datatable.Rows.Count != 0)

//                {
//                    foreach (DataRow dr_datarow in dt_datatable.Rows)
//                    {
//                        getReplyToQueryList.Add(new ReplyToQueryList

//                        {

//                            raisequery_gid = (dr_datarow["raisequery_gid"].ToString()),
//                            replytoquery_gid = (dr_datarow["replytoquery_gid"].ToString()),
//                            assigned_by = (dr_datarow["created_by"].ToString()),
//                            assigned_to = (dr_datarow["assigned_to"].ToString()),
//                            assigned_date = (dr_datarow["created_date"].ToString()),
//                            description = (dr_datarow["description"].ToString()),
//                            reply_query = (dr_datarow["reply_query"].ToString()),
//                            replied_by = (dr_datarow["updated_by"].ToString()),
//                            replied_date = (dr_datarow["updated_date"].ToString()),
//                        });
//                    }
//                    values.ReplyToQueryList = getReplyToQueryList;
//                }

//                dt_datatable.Dispose();
//                values.status = true;
//                values.message = "Success";
//            }
//            catch (Exception ex)
//            {
//                values.status = false;
//            }
//        }

//        public void DaGetEmployeeName(string raisequery_gid, employelist values)
//        {
//            msSQL = " select group_concat(employee_name) as employee_name  from atm_trn_traisequery " +
//                  " where raisequery_gid='" + raisequery_gid + "'";
//            objODBCDatareader = objdbconn.GetDataReader(msSQL);
//            if (objODBCDatareader.HasRows == true)
//            {
//                values.employee_name = objODBCDatareader["employee_name"].ToString();
//            }
//            objODBCDatareader.Close();
            

//        }


//    }
//}

