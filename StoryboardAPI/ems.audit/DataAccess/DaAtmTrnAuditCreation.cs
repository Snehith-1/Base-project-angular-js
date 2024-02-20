using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;


namespace ems.audit.DataAccess
{
    public class DaAtmTrnAuditCreation
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGid1, lsauditdepartment_value, msGetaudituniqueno, lsdue_date, lsreport_date, lsend_date, lsperiodfrom_date, lsauditperiod_to, lsauditname_value;
        int mnResult;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, employee_reporting_to;
        int k, ls_port;
        string lsemployee_name, lsemployee_gid, lsauditmaker_gid,lstransist,lsconcurrent, lsauditormakerchecker_flag, lsauditorcheckerapprover_flag, lsauditormakerapprover_flag, lsauditchecker_gid, lsauditapprover_gid, lsauditormaker_name, lsBccmail_id, lscreated_by, lscc2members, lsauditcreation_gid, lscreated_date, lsto_mail, frommail_id, lscc_mail, strBCC, lsbcc_mail, lsaudit_name, lsaaudit_uniqueno, lsaudit_description, lsauditdepartment_name, lsaudittype_name, lscheckpointgroup_name;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;
        public string[] lsToReceipients;
        string lscheckpointgroupadd_gid;


        public void DaGetAuditCreation(MdlAtmTrnAuditCreation values)
        {
            try
            {
                msSQL = " SELECT distinct a.auditcreation_gid,a.checklistmaster_gid,d.sampleimport_gid,a.auditapprover_name,auditdepartment_name,  " +
                        " a.auditmapping2employee_gid, a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno, " +
                        " a.status,a.approval_status,a.auditmaker_name,a.auditchecker_name,a.status_flag, " +
                        " case when a.approval_status is null then '-'" +
                        " else a.approval_status end as approval_status," +
                        " date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate, " +
                        " date_format(a.auditperiod_todate,'%d-%m-%Y') as auditperiod_todate, a.auditeemaker_name, a.auditeechecker_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join atm_trn_tsampleexcelimport d on d.auditcreation_gid = a.auditcreation_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid group by a.auditcreation_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditapprover_name = (dr_datarow["auditapprover_name"].ToString()),
                            auditmapping2employee_gid = (dr_datarow["auditmapping2employee_gid"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditperiod_fromdate = (dr_datarow["auditperiod_fromdate"].ToString()),
                            auditperiod_todate = (dr_datarow["auditperiod_todate"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                        });
                    }
                    values.auditcreation_list = getauditcreation_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAssignedInformation(string auditcreation_gid, assignedinformation values)
        {
            try
            {
                msSQL = " SELECT a.auditapprover_name, a.auditmaker_name,a.auditchecker_name, " +
                        " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate, " +
                        " date_format(a.auditperiod_todate,'%d-%m-%Y') as auditperiod_todate, a.auditeemaker_name, a.auditeechecker_name " +
                        " FROM atm_trn_tauditcreation a where auditcreation_gid='" + auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.audit_maker = objODBCDatareader["auditmaker_name"].ToString();
                    values.audit_checker = objODBCDatareader["auditchecker_name"].ToString();
                    values.auditapprover_name = objODBCDatareader["auditapprover_name"].ToString();
                    values.auditperiod_fromdate = objODBCDatareader["auditperiod_fromdate"].ToString();
                    values.auditperiod_todate = objODBCDatareader["auditperiod_todate"].ToString();
                    values.auditeemaker_name = objODBCDatareader["auditeemaker_name"].ToString();
                    values.auditeechecker_name = objODBCDatareader["auditeechecker_name"].ToString();
                }
                objODBCDatareader.Close();


            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAudit360View(MdlAtmTrnAuditCreation values, string auditcreation_gid)
        {
            try
            {
                msSQL = " SELECT a.auditcreation_gid,a.checklistmaster_gid,a.auditapprover_name,a.auditmapping2employee_gid, a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.status,a.approval_status,a.auditmaker_name,a.auditchecker_name,a.status_flag,date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                        " where auditcreation_gid='" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditapprover_name = (dr_datarow["auditapprover_name"].ToString()),
                            auditmapping2employee_gid = (dr_datarow["auditmapping2employee_gid"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }

        public void DaGetSample(string auditcreation_gid, MdlAtmTrnSampling values, string employee_gid)
        {
            try
            {

                msSQL = " SELECT distinct a.sampleimport_gid,d.audit_uniqueno, a.sample_name,a.auditcreation_gid,e.employee_gid,e.auditmapping_gid, d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                     " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                     " left join atm_mst_tchecklistmaster e on e.checklistmaster_gid = d.checklistmaster_gid " +
                     " left join atm_trn_tsamplequeries f on f.sampleimport_gid = a.sampleimport_gid " +
                     " left join atm_mst_ttaguser2employee g on g.auditcreation_gid = a.auditcreation_gid" +
                     " where a.auditcreation_gid ='" + auditcreation_gid + "' group by a.sampleimport_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getssample_list = new List<sample_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getssample_list.Add(new sample_list
                        {
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            audit_uniqueno = (dr_datarow["sampleimport_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            sample_name = (dr_datarow["sample_name"].ToString()),                       
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            replyquery_flag = (dr_datarow["replyquery_flag"].ToString()),

                        });
                    }
                    values.sample_list = getssample_list;
                }
                dt_datatable.Dispose();
                values.status = true;

            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaPostAuditCreation(MdlAtmTrnAuditCreation values, string employee_gid)

        {

           
            string[] sampleimportgid_array = values.checkpointgroup_gid.Split(',');

            int array_length = sampleimportgid_array.Length;
            string sample_querystring = "";
            for (int i = 0; i < sampleimportgid_array.Length; i++)
            {
                if (i == sampleimportgid_array.Length - 1)
                {
                    sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "'";
                }
                else
                {
                    sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "',";
                }
            }


            msSQL = "select auditeechecker_gid,auditeemaker_gid,multipleauditee_gid from atm_trn_tmultipleauditee " +
            " where checkpointgroup_gid in (" + sample_querystring + ") ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Kindly assign the auditee maker and auditee checker for checkpointgroup against";
                return;
            }


            msGetGid = objcmnfunctions.GetMasterGID("ATCT");

            msSQL = " insert into atm_trn_tauditcreation(" +
                    " auditcreation_gid," +
                    " audit_uniqueno ," +
                    " checklistmaster_gid ," +
                    " checklistmasteradd_gid ," +
                    " audit_name," +
                    " auditdepartment_gid ," +
                    " auditdepartment_name," +
                    " audittype_gid ," +
                    " audittype_name," +
                    " due_date ," +
                    " auditperiod_fromdate ," +
                    " auditperiod_todate ," +
                    " auditpriority_gid ," +
                    " auditpriority_name ," +
                    " employee_gid ," +
                    " auditmaker_name," +
                    " auditmapping_gid ," +
                    " auditchecker_name," +
                    " auditmapping2employee_gid ," +
                    " auditapprover_name ," +
                    //" auditeemaker_gid ," +
                    //" auditeemaker_name," +
                    //" auditeechecker_gid ," +
                    //" auditeechecker_name," +
                    " entity_name ," +
                    " checkpointgroup_name ," +
                    " audit_description," +
                    " auditdelete_flag, " +
                    " status, " +
                    " auditobservation_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetGid + "', " +
                    "'" + values.checklistmaster_gid + "', " +
                    "'" + values.checklistmasteradd_gid + "', " +
                    "'" + values.audit_name.Replace("'", "") + "'," +
                    "'" + values.auditdepartment_gid + "', " +
                    "'" + values.auditdepartment_name.Replace("'", "") + "'," +
                    "'" + values.audittype_gid + "', " +
                    "'" + values.audittype_name.Replace("'", "") + "'," +
                    "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + Convert.ToDateTime(values.periodfrom_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + Convert.ToDateTime(values.auditperiod_to).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.auditpriority_gid + "', " +
                    "'" + values.auditpriority_name.Replace("'", "") + "'," +
                    "'" + values.auditormaker_gid + "', " +
                    "'" + values.audit_maker + "', " +
                    "'" + values.auditorchecker_gid + "', " +
                    "'" + values.audit_checker.Replace("'", "") + "'," +
                    "'" + values.auditorapprover_gid + "', " +
                    "'" + values.audit_approver.Replace("'", "") + "'," +
                    //"'" + values.auditeemaker_gid + "', " +
                    //"'" + values.auditeemaker_name + "', " +
                    //"'" + values.auditeechecker_gid + "', " +
                    //"'" + values.auditeechecker_name + "', " +
                    "'" + values.entity_name + "', " +
                    "'" + values.checkpointgroup_name + "', " +
                    "'" + values.audit_description.Replace("'", "") + "'," +
                    "'Y'," +
                    "'Open'," +
                     "'" + values.auditobservation_name + "', " +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //lstransist = ConfigurationManager.AppSettings["transist"].ToString();
            //lsconcurrent = ConfigurationManager.AppSettings["concurrent"].ToString();

            //if ((values.audittype_name == lstransist) || (values.audittype_name == lsconcurrent))
            //{
            //    msSQL = " update atm_trn_tauditcreation set audittype_flag = 'Y', auditee_visible='Not Visible to Auditee'" +
            //                     " where auditcreation_gid = '" + msGetGid + "' ";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //}
            //else
            //{
            //    msSQL = " update atm_trn_tauditcreation set audittype_flag = 'N', auditee_visible='Visible to Auditee'" +
            //                                        " where auditcreation_gid = '" + msGetGid + "' ";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //}


            if (mnResult != 0)
            {
                
                values.status = true;
                values.message = "Audit Initiated  Successfully";

                //int array_length = sampleimportgid_array.Length;
                //string sample_querystring = "";
                //for (int i = 0; i < sampleimportgid_array.Length; i++)
                //{
                //    if (i == sampleimportgid_array.Length - 1)
                //    {
                //        sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "'";
                //    }
                //    else
                //    {
                //        sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "',";
                //    }
                //}


                msSQL = "select auditeechecker_gid,auditeemaker_gid,auditeechecker_name,auditeemaker_name,checkpointgroup_gid,checkpointgroupadd_gid from atm_trn_tmultipleauditee " +
                " where checkpointgroup_gid in (" + sample_querystring + ") ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //string lscheckpointgroupadd_gid = (dt["checkpointgroupadd_gid"].ToString());

                        msGetGid1 = objcmnfunctions.GetMasterGID("AAMA");

                        msSQL = " insert into atm_trn_tauditagainstmultipleauditeechecker(" +
                               " auditagainstmultipleauditeechecker_gid, " +
                                " auditeemaker_gid," +
                                " auditeechecker_gid," +
                                " auditeemaker_name," +
                                 " auditeechecker_name," +
                                " auditcreation_gid," +
                                " checkpointgroup_gid ," +
                                  " checkpointgroupadd_gid ," +
                                     " auditeechecker_approvalstatus ," +
                                  " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + dt["auditeemaker_gid"].ToString() + "'," +
                                 "'" + dt["auditeechecker_gid"].ToString() + "'," +
                                  "'" + dt["auditeemaker_name"].ToString() + "'," +
                                 "'" + dt["auditeechecker_name"].ToString() + "'," +
                                 "'" + msGetGid + "'," +
                                "'" + dt["checkpointgroup_gid"].ToString() + "'," +
                                  "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                                   "' --'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }

            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
            //}
        }
        public void DaEditAuditCreation(string auditcreation_gid, MdlAtmTrnAuditCreation values)
        {
            msSQL = " select a.auditcreation_gid,b.checklistmaster_gid,(b.auditmaker_name) as audit_makername,(b.auditchecker_name) as audit_checkername, a.observation_percentage, " +
                 " audit_uniqueno,a.audit_name,auditpriority_gid,a.audittype_gid,auditpriority_name,a.auditmapping_gid,a.auditmaker_name,a.status,auditorapprover_approvalflag, " +
                 " a.employee_gid,auditmapping2employee_gid,a.auditeemaker_name,a.rejected_remarks,a.auditeemaker_gid,a.auditeechecker_gid, " +
                 " a.auditeechecker_name,a.auditchecker_name,a.auditobservation_name,auditapprover_name,a.auditdepartment_gid,a.auditdepartment_name, " +
                 " date_format(due_date,'%d-%m-%Y') as due_date , date_format(report_date,'%d-%m-%Y') as report_date , " +
                 " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,observation_fill, " +
                 " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.observation_score,a.auditormaker_approvalflag, " +
                 " date_format(auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate ,auditeechecker_approvalflag,auditorchecker_approvalflag, " +
                 " date_format(auditperiod_todate,'%d-%m-%Y') as auditperiod_todate,a.samplestatus_flag,a.checklistverified_flag, a.entity_name, a.audittype_name, a.checkpointgroup_name, a.audit_description " +
                 " from atm_trn_tauditcreation a " +
                " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid" +
                " left join hrm_mst_temployee d on a.created_by = d.employee_gid" +
                " left join adm_mst_tuser c on c.user_gid = d.user_gid " +
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
                values.employee_gid = objODBCDatareader["employee_gid"].ToString();
                values.audit_maker = objODBCDatareader["auditmaker_name"].ToString();
                values.auditmapping_gid = objODBCDatareader["auditmapping_gid"].ToString();
                values.audit_checker = objODBCDatareader["auditchecker_name"].ToString();
                values.auditdepartment_gid = objODBCDatareader["auditdepartment_gid"].ToString();
                values.auditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                values.audittype_gid = objODBCDatareader["audittype_gid"].ToString();
                values.audittype_name = objODBCDatareader["audittype_name"].ToString();
                values.auditmapping2employee_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                values.audit_approver = objODBCDatareader["auditapprover_name"].ToString();
                values.end_date = objODBCDatareader["due_date"].ToString();
                values.periodfrom_date = objODBCDatareader["auditperiod_fromdate"].ToString();
                values.auditperiod_to = objODBCDatareader["auditperiod_todate"].ToString();
                values.auditmaker_name = objODBCDatareader["audit_makername"].ToString();
                values.auditchecker_name = objODBCDatareader["audit_checkername"].ToString();
                values.auditeemaker_gid = objODBCDatareader["auditeemaker_gid"].ToString();
                values.auditeechecker_gid = objODBCDatareader["auditeechecker_gid"].ToString();
                values.auditeemaker_name = objODBCDatareader["auditeemaker_name"].ToString();
                values.auditeechecker_name = objODBCDatareader["auditeechecker_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.status_update = objODBCDatareader["status"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                values.audit_description = objODBCDatareader["audit_description"].ToString();
                values.auditorchecker_approvalflag = objODBCDatareader["auditorchecker_approvalflag"].ToString();
                values.auditeechecker_approvalflag = objODBCDatareader["auditeechecker_approvalflag"].ToString();
                values.auditorapprover_approvalflag = objODBCDatareader["auditorapprover_approvalflag"].ToString();
                values.observation_score = objODBCDatareader["observation_score"].ToString();
                values.observation_fill = objODBCDatareader["observation_fill"].ToString();
                values.auditormaker_approvalflag = objODBCDatareader["auditormaker_approvalflag"].ToString();
                values.observation_percentage = objODBCDatareader["observation_percentage"].ToString();
                values.samplestatus_flag = objODBCDatareader["samplestatus_flag"].ToString();
                values.checklistverified_flag = objODBCDatareader["checklistverified_flag"].ToString();
                values.auditobservation_name = objODBCDatareader["auditobservation_name"].ToString();
                values.rejected_remarks = objODBCDatareader["rejected_remarks"].ToString();

            }
            objODBCDatareader.Close();

            msSQL = " select count(*) as openquery from atm_trn_tsampleraisequery where auditcreation_gid = '" + auditcreation_gid + "'" +
                    " and sampleraisequery_status = 'Open'";
            values.openquerycount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as openquery from atm_trn_tauditraisequery where auditcreation_gid = '" + auditcreation_gid + "'" +
                   " and auditraisequery_status = 'Open'";
            values.auditopenquerycount = objdbconn.GetExecuteScalar(msSQL);

            values.status = true;
        }
        public void DaEditAuditee(string multipleauditee_gid, MdlAtmTrnAuditCreation values)
        {
            msSQL = " select multipleauditee_gid,auditcreation_gid, auditeemaker_gid,auditeechecker_gid " +
                 " from atm_trn_tmultipleauditee  " +
                " where multipleauditee_gid='" + multipleauditee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.multipleauditee_gid = objODBCDatareader["multipleauditee_gid"].ToString();
                values.auditeemaker_gid = objODBCDatareader["auditeemaker_gid"].ToString();
                values.auditeechecker_gid = objODBCDatareader["auditeechecker_gid"].ToString();
                             

            }
            objODBCDatareader.Close();
         
            values.status = true;
        }
        public void DaUpdateAuditee(string employee_gid, MdlAtmTrnAuditCreation values)
        {
           
            msSQL = " update atm_trn_tauditcreation set " +
                 " auditcreation_gid='" + values.auditcreation_gid + "'," +
                   " auditeemaker_gid='" + values.auditeemaker_gid + "'," +
                  " auditeemaker_name='" + values.auditeemaker_name + "'," +
                   " auditeechecker_gid='" + values.auditeechecker_gid + "'," +
                  " auditeechecker_name='" + values.auditeechecker_name + "'" +
                  
            //msSQL += " updated_by='" + employee_gid + "'," +
            //     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where multipleauditee_gid='" + values.multipleauditee_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

           

            if (mnResult != 0)
            {
              
                values.status = true;
                values.message = " Audit Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaUpdateAuditCreation(string employee_gid, MdlAtmTrnAuditCreation values)
        {
           

            msSQL = " select date_format(due_date,'%d-%m-%Y') as due_date, date_format(report_date,'%d-%m-%Y') as report_date,date_format(auditperiod_fromdate, '%d-%m-%Y') as auditperiod_fromdate, " +
                   " date_format(auditperiod_todate, '%d-%m-%Y') as auditperiod_todate from atm_trn_tauditcreation where auditcreation_gid='" + values.auditcreation_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsend_date = objODBCDatareader["due_date"].ToString();
                lsperiodfrom_date = objODBCDatareader["auditperiod_fromdate"].ToString();
                lsauditperiod_to = objODBCDatareader["auditperiod_todate"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " update atm_trn_tauditcreation set " +
                 " checklistmaster_gid='" + values.checklistmaster_gid + "'," +
                 " audit_uniqueno='" + values.audit_uniqueno + "'," +
                  " audit_name='" + values.audit_name.Replace("'", "") + "'," +
                   " auditpriority_gid='" + values.auditpriority_gid + "'," +
                  " auditpriority_name='" + values.auditpriority_name.Replace("'", "") + "'," +
                   " audittype_gid='" + values.audittype_gid + "'," +
                  " audittype_name='" + values.audittype_name.Replace("'", "") + "'," +
                   " employee_gid='" + values.employee_gid + "'," +
                  " auditmaker_name='" + values.audit_maker.Replace("'", "") + "'," +
                   " auditmapping_gid='" + values.auditmapping_gid + "'," +
                  " auditchecker_name='" + values.audit_checker.Replace("'", "") + "'," +
                   " auditdepartment_gid='" + values.auditdepartment_gid + "'," +
                  " auditdepartment_name='" + values.auditdepartment_name.Replace("'", "") + "'," +
                  " auditmapping2employee_gid='" + values.auditmapping2employee_gid + "'," +
                   " auditapprover_name='" + values.audit_approver + "'," +                    
                    //   " auditeemaker_gid='" + values.auditeemaker_gid + "'," +
                    //" auditeemaker_name='" + values.auditeemaker_name.Replace("'", "") + "'," +
                    // " auditeechecker_gid='" + values.auditeechecker_gid + "'," +
                    //" auditeechecker_name='" + values.auditeechecker_name.Replace("'", "") + "'," +
                    " auditobservation_name='" + values.auditobservation_name.Replace("'", "") + "'," +
                  " auditpriority_name='" + values.auditpriority_name.Replace("'", "") + "',";
            if (lsend_date == values.end_date)
            {
            }
            else
            {
                msSQL += " due_date='" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd") + "',";
            }
            if (lsperiodfrom_date == values.periodfrom_date)
            {
            }
            else
            {
                msSQL += " auditperiod_fromdate='" + Convert.ToDateTime(values.periodfrom_date).ToString("yyyy-MM-dd") + "',";
            }
            if (lsauditperiod_to == values.auditperiod_to)
            {
            }
            else
            {
                msSQL += " auditperiod_todate='" + Convert.ToDateTime(values.auditperiod_to).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where auditcreation_gid='" + values.auditcreation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //if ((values.audittype_name == "Transist") || (values.audittype_name == "Concurrent"))
            //{
            //    msSQL = " update atm_trn_tauditcreation set audittype_flag = 'Y'" +
            //                     " where auditcreation_gid = '" + values.auditcreation_gid + "' ";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //}
            //else
            //{
            //    msSQL = " update atm_trn_tauditcreation set audittype_flag = 'N'" +
            //            " where auditcreation_gid = '" + values.auditcreation_gid + "' ";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //}

            string checkpointgroupname_gid = values.checkpointgroup_gid;

            if (!string.IsNullOrEmpty(checkpointgroupname_gid))
            {


                string[] sampleimportgid_array = checkpointgroupname_gid.Split(',');

                int array_length = sampleimportgid_array.Length;
                string sample_querystring = "";
                for (int i = 0; i < sampleimportgid_array.Length; i++)
                {
                    if (i == sampleimportgid_array.Length - 1)
                    {
                        sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "'";
                    }
                    else
                    {
                        sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "',";
                    }
                }


                msSQL = " update atm_trn_tauditcreation set newcheckpointgroupname_flag='Y' where auditcreation_gid='" + values.auditcreation_gid + "' and approval_status ='Initiate Audit Approval pending'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);    

                msSQL = "delete from atm_trn_tauditagainstmultipleauditeechecker where auditcreation_gid = '" + values.auditcreation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select auditeechecker_gid,auditeemaker_gid,auditeechecker_name,auditeemaker_name,checkpointgroup_gid,checkpointgroupadd_gid from atm_trn_tmultipleauditee " +
                             " where checkpointgroup_gid in (" + sample_querystring + ") ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        //string lscheckpointgroupadd_gid = (dt["checkpointgroupadd_gid"].ToString());

                        msGetGid1 = objcmnfunctions.GetMasterGID("AAMA");

                        msSQL = " insert into atm_trn_tauditagainstmultipleauditeechecker(" +
                               " auditagainstmultipleauditeechecker_gid, " +
                                " auditeemaker_gid," +
                                " auditeechecker_gid," +
                                " auditeemaker_name," +
                                 " auditeechecker_name," +
                                " auditcreation_gid," +
                                " checkpointgroup_gid ," +
                                  " checkpointgroupadd_gid ," +
                                     " auditeechecker_approvalstatus ," +
                                  " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + dt["auditeemaker_gid"].ToString() + "'," +
                                 "'" + dt["auditeechecker_gid"].ToString() + "'," +
                                  "'" + dt["auditeemaker_name"].ToString() + "'," +
                                 "'" + dt["auditeechecker_name"].ToString() + "'," +
                                 "'" + values.auditcreation_gid + "'," +
                                "'" + dt["checkpointgroup_gid"].ToString() + "'," +
                                  "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                                   "' --'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            

            if (mnResult != 0)
            {

              
                //msSQL = " update atm_trn_tmultipleauditee set auditcreation_gid='" + values.auditcreation_gid + "' where auditcreation_gid='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);             
                values.status = true;
                values.message = " Audit Updated Successfully";
                
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaTrnCheckpointCreation(string auditcreation_gid, MdlAtmTrnAuditCreation values)
        {
            try
            {
                msSQL = " select  a.checklistmasteradd_gid,a.checkpointgroupadd_gid, a.auditcreation_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name,  " +
                         " a.audit_name, a.checkpoint_intent, a.checkpoint_description,  a.riskcategory_name, a.positiveconfirmity_name, " +
                         " a.noteto_auditor, a.yes_score, a.no_score, a.partial_score, a.na_score, a.total_score, " +
                         " a.yes_disposition, a.no_disposition, a.partial_disposition, a.na_disposition " +
                         " from atm_trn_tauditcreation2checklist a " +
                         " left join atm_trn_tauditcreation b on a.auditcreation_gid = b.auditcreation_gid " +
                         " where a.auditcreation_gid = '" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcheckpointsummary_list = new List<auditcheckpointsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcheckpointsummary_list.Add(new auditcheckpointsummary_list
                        {
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
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
                            total_score = (dr_datarow["total_score"].ToString()),
                            yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                            no_disposition = (dr_datarow["no_disposition"].ToString()),
                            partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                            na_disposition = (dr_datarow["na_disposition"].ToString())
                        });
                    }
                    values.auditcheckpointsummary_list = getauditcheckpointsummary_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaCheckpointAuditcreation(string checklistmaster_gid, string auditcreation_gid, MdlAtmTrnAuditCreation values)
        {
            //msSQL = " update atm_mst_tchecklistmasteradd set checklistmaster_gid='" + checklistmaster_gid + "'" +
            //       " where checklistmaster_gid is null ";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
            try
            {
                msSQL = " select distinct a.checklistmaster_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid,a.checkpointgroup_gid,a.auditcreation_gid, " +
                        " a.auditdepartment_name, a.audittype_name,a.capture_flag, a.checkpointgroup_name, " +
                        " a.audit_name, a.checkpoint_intent, a.checkpoint_description,  a.riskcategory_name, a.positiveconfirmity_name, " +
                        " a.noteto_auditor, a.yes_score, a.no_score, b.partial_score, a.na_score, a.total_score, " +
                        " a.yes_disposition, a.no_disposition, a.partial_disposition, a.na_disposition, " +
                        " (select count(*) from atm_trn_tauditcreation2checklist a where a.auditcreation_gid = '" + auditcreation_gid + "' " +
                        " and checklistmasteradd_gid = b.checklistmasteradd_gid) as checklist_flag " +
                        " from atm_trn_tauditcreation2checklist a" +
                        " left join atm_mst_tchecklistmasteradd b on a.checklistmaster_gid = b.checklistmaster_gid" +
                        " where a.auditcreation_gid = '" + auditcreation_gid + "' " +
                        " group by b.checklistmasteradd_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                msSQL = " select  a.checklistmaster_gid,a.checklistmasteradd_gid,a.checkpointgroupadd_gid, a.auditcreation_gid, a.auditdepartment_name, a.audittype_name, a.checkpointgroup_name,  " +
                         " a.audit_name, a.checkpoint_intent, a.checkpoint_description,  a.riskcategory_name, a.positiveconfirmity_name, " +
                         " a.noteto_auditor, a.yes_score, a.no_score, a.partial_score, a.na_score, a.total_score, " +
                         " a.yes_disposition, a.no_disposition, a.partial_disposition, a.na_disposition,a.checklist_flag " +
                         " from atm_trn_tauditcreation2checklist a " +
                         " where a.auditcreation_gid = '" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcheckpointsummary_list = new List<auditcheckpointsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcheckpointsummary_list.Add(new auditcheckpointsummary_list
                        {

                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
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
                            total_score = (dr_datarow["total_score"].ToString()),
                            checklist_flag = (dr_datarow["checklist_flag"].ToString()),
                            //capture_flag = (dr_datarow["capture_flag"].ToString()),
                            yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                            no_disposition = (dr_datarow["no_disposition"].ToString()),
                            partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                            na_disposition = (dr_datarow["na_disposition"].ToString())
                        });
                    }
                    values.auditcheckpointsummary_list = getauditcheckpointsummary_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaCheckpointCreation(string checklistmaster_gid, string auditcreation_gid, MdlAtmTrnAuditCreation values)
        {
            //msSQL = " update atm_mst_tchecklistmasteradd set checklistmaster_gid='" + checklistmaster_gid + "'" +
            //       " where checklistmaster_gid is null ";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
            try
            {
                msSQL = " select distinct a.checklistmaster_gid,b.checkpointgroupadd_gid,b.checklistmasteradd_gid,b.checkpointgroup_gid,c.auditcreation_gid, " +
                        " a.auditdepartment_name, a.audittype_name,b.capture_flag, b.checkpointgroup_name, " +
                        " a.audit_name, b.checkpoint_intent, b.checkpoint_description,  b.riskcategory_name, b.positiveconfirmity_name, " +
                        " b.noteto_auditor, b.yes_score, b.no_score, b.partial_score, b.na_score, b.total_score, " +
                        " b.yes_disposition, b.no_disposition, b.partial_disposition, b.na_disposition, " +
                        " (select count(*) from atm_trn_tauditcreation2checklist where auditcreation_gid = '" + auditcreation_gid + "' " +
                        " and checklistmasteradd_gid = b.checklistmasteradd_gid) as checklist_flag " +
                        " from atm_mst_tchecklistmaster a" +
                        " left join atm_trn_tauditcreation c on a.checklistmaster_gid = c.checklistmaster_gid" +
                        " left join atm_mst_tchecklistmasteradd b on a.checklistmaster_gid = b.checklistmaster_gid" +
                        " left join atm_mst_tcheckpointadd f on f.checkpointgroup_gid = b.checkpointgroup_gid " +
                        " where a.checklistmaster_gid = '" + checklistmaster_gid + "' and b.checklistmaster_gid = '" + checklistmaster_gid + "' " +
                        " group by b.checklistmasteradd_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getauditcheckpointsummary_list = new List<auditcheckpointsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcheckpointsummary_list.Add(new auditcheckpointsummary_list
                        {

                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            checklistmasteradd_gid = (dr_datarow["checklistmasteradd_gid"].ToString()),
                            checkpointgroupadd_gid = (dr_datarow["checkpointgroupadd_gid"].ToString()),
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
                            total_score = (dr_datarow["total_score"].ToString()),
                            checklist_flag = (dr_datarow["checklist_flag"].ToString()),
                            //capture_flag = (dr_datarow["capture_flag"].ToString()),
                            yes_disposition = (dr_datarow["yes_disposition"].ToString()),
                            no_disposition = (dr_datarow["no_disposition"].ToString()),
                            partial_disposition = (dr_datarow["partial_disposition"].ToString()),
                            na_disposition = (dr_datarow["na_disposition"].ToString())
                        });
                    }
                    values.auditcheckpointsummary_list = getauditcheckpointsummary_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaPostChecklistAssign(auditchecklistassign values, string employee_gid)
        {

            //msSQL = "delete from atm_trn_tauditcreation2checklist " +
            //" where checklistmaster_gid='" + values.checklistmaster_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "update atm_mst_tchecklistmasteradd set checklist_flag ='N'  where checklistmaster_gid = '" + values.checklistmaster_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.submitevent != "Y")
            {
                msSQL = " update atm_trn_tauditcreation set auditapproval_flag='N', " +
                        " approval_status ='Initiate Audit Approval pending', status='Initiate Audit Approval pending' " +
                        " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            //msSQL = " delete from atm_trn_tauditagainstmultipleauditeechecker where  auditcreation_gid = '" + values.auditcreation_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




            foreach (string i in values.checklistmasteradd_gid)
            {
                

                msSQL = " select  a.checklistmaster_gid, b.checklistmasteradd_gid,b.checkpointgroupadd_gid,c.auditcreation_gid,a.auditdepartment_name, " +
                        " a.audittype_name, b.checkpointgroup_name, a.audit_name, b.checkpoint_intent, b.checkpoint_description, " +
                        " b.riskcategory_name, b.positiveconfirmity_name, b.noteto_auditor, b.yes_score, b.no_score, b.partial_score, " +
                        " b.na_score, b.total_score,b.yes_disposition,b.no_disposition,b.partial_disposition,b.na_disposition " +
                        " from atm_mst_tchecklistmaster a " +
                        " left join atm_trn_tauditcreation c on a.checklistmaster_gid = c.checklistmaster_gid" +
                        " left join atm_mst_tchecklistmasteradd b on a.checklistmaster_gid = b.checklistmaster_gid " +
                        " where b.checklistmasteradd_gid = '" + i + "' and c.auditcreation_gid = '" + values.auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("AC2C");

                        msSQL = " insert into atm_trn_tauditcreation2checklist(" +
                                " auditcreation2checklist_gid," +
                                " checklistmasteradd_gid," +
                                " checklistmaster_gid," +
                                " checkpointgroupadd_gid," +
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
                                " total_score ," +
                                " yes_disposition, " +
                                " no_disposition, " +
                                " partial_disposition, " +
                                " na_disposition, " +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + i + "', " +
                                "'" + dt["checklistmaster_gid"].ToString() + "'," +
                                 "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
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
                                "'" + dt["total_score"].ToString() + "'," +
                                "'" + dt["yes_disposition"].ToString() + "'," +
                                "'" + dt["no_disposition"].ToString() + "'," +
                                "'" + dt["partial_disposition"].ToString() + "'," +
                                "'" + dt["na_disposition"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid1 = objcmnfunctions.GetMasterGID("OBSS");

                        msSQL = " insert into atm_trn_tobservationscoresample(" +
                               " observationscoresample_gid, " +
                                " checklistmasteradd_gid," +
                                " auditcreation2checklist_gid," +
                                " checklistmaster_gid," +
                                " checkpointgroupadd_gid," +
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
                                " total_score ," +
                                " yes_disposition, " +
                                " no_disposition, " +
                                " partial_disposition, " +
                                " na_disposition," +
                                  " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + i + "', " +
                                "'" + msGetGid + "'," +
                                 "'" + dt["checklistmaster_gid"].ToString() + "'," +
                                "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
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
                                "'" + dt["total_score"].ToString() + "'," +
                                "'" + dt["yes_disposition"].ToString() + "'," +
                                "'" + dt["no_disposition"].ToString() + "'," +
                                "'" + dt["partial_disposition"].ToString() + "'," +
                                "'" + dt["na_disposition"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                   // msSQL = " select a.checkpointgroupadd_gid from atm_mst_tchecklistmasteradd a " +
                   //     " left join atm_trn_tauditcreation b on b.checklistmaster_gid = a.checklistmaster_gid " +
                   //" where a.checklistmasteradd_gid = '" + i + "'  and b.auditcreation_gid='" + values.auditcreation_gid + "'";
                    
                   //  lscheckpointgroupadd_gid = objdbconn.GetExecuteScalar(msSQL);

                   // msSQL = "select auditeechecker_gid,auditeemaker_gid,auditeechecker_name,auditeemaker_name,checkpointgroup_gid,checkpointgroupadd_gid from atm_trn_tmultipleauditee " +
                   // " where checkpointgroupadd_gid ='" + lscheckpointgroupadd_gid + "'";
                   // dt_datatable = objdbconn.GetDataTable(msSQL);

                   // if (dt_datatable.Rows.Count != 0)
                   // {
                   //     foreach (DataRow dt in dt_datatable.Rows)
                   //     {
                   //         //string lscheckpointgroupadd_gid = (dt["checkpointgroupadd_gid"].ToString());

                   //         msGetGid1 = objcmnfunctions.GetMasterGID("AAMA");

                   //         msSQL = " insert into atm_trn_tauditagainstmultipleauditeechecker(" +
                   //                " auditagainstmultipleauditeechecker_gid, " +
                   //                 " auditeemaker_gid," +
                   //                 " auditeechecker_gid," +
                   //                 " auditeemaker_name," +
                   //                  " auditeechecker_name," +
                   //                 " auditcreation_gid," +
                   //                 " checkpointgroup_gid ," +
                   //                   " checkpointgroupadd_gid ," +
                   //                      " auditeechecker_approvalstatus ," +
                   //                   " created_by," +
                   //                 " created_date)" +
                   //                 " values(" +
                   //                 "'" + msGetGid1 + "'," +
                   //                 "'" + dt["auditeemaker_gid"].ToString() + "'," +
                   //                  "'" + dt["auditeechecker_gid"].ToString() + "'," +
                   //                   "'" + dt["auditeemaker_name"].ToString() + "'," +
                   //                  "'" + dt["auditeechecker_name"].ToString() + "'," +
                   //                  "'" + values.auditcreation_gid + "'," +
                   //                 "'" + dt["checkpointgroup_gid"].ToString() + "'," +
                   //                   "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                   //                    "' --'," +
                   //                 "'" + employee_gid + "'," +
                   //                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                   //         mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                   //     }
                   // }
            }
           
            
            }
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Submitted to Audit Approval Successfully..!";
                msSQL = " select  auditcreation_gid,employee_gid,created_by, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                               " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                    lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                    lscreated_by = objODBCDatareader["created_by"].ToString();

                }
                objODBCDatareader.Close();

                if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditchecker_gid && lscreated_by == lsauditapprover_gid)
                {
                    msSQL = " update atm_trn_tauditcreation set auditapproval_flag='Y', " +
                        " approval_status ='Initiate Audit Approved', status='Initiate Audit Approved', " +
                        " auditapproved_by ='" + employee_gid + "', " +
                        " auditapproved_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

                    msSQL = " select  a.auditcreation_gid,d.auditeemaker_gid,d.auditeechecker_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                          " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                        lsemployee_gid = objODBCDatareader["auditeechecker_gid"].ToString();
                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                        lscreated_by = objODBCDatareader["created_by"].ToString();
                        lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
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

                    sub = " Audit Creation Approval ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "The following Audit Creation is Approval. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name).ToString()+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name) + "<br />";
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
                           "'" + employee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Audit Initated for Approval'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }

                else
                {

                    msSQL = " select  auditcreation_gid,employee_gid,created_by, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                                   " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                        lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        lscreated_by = objODBCDatareader["created_by"].ToString();

                    }
                    objODBCDatareader.Close();

                    if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditmaker_gid && lscreated_by == lsauditchecker_gid)
                    {
                        msSQL = " update atm_trn_tauditcreation set auditapproval_flag='Y', " +
                            " approval_status ='Initiate Audit Approved', status='Initiate Audit Approved', " +
                            " auditapproved_by ='" + employee_gid + "', " +
                            " auditapproved_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                             " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',', a.auditmapping2employee_gid)  as CC2members , a.auditmapping_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
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


                        sub = " Audit Creation Approval ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "The following Audit Creation is Approval. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name) + "<br />";
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
                               "'" + employee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit Initated for Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    else
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

                            msSQL = " update atm_trn_tauditcreation set auditorcommonname_flag='Y'" +
                                   " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
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

                            sub = " Request for Audit Approval ";
                            body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "The following Audit is submitted for your Approval. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name) + "<br />";
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
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit Initated for Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else
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
                            if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditmaker_gid)

                            {
                                msSQL = " update atm_trn_tauditcreation set auditormakerchecker_flag='Y'" +
                                  " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


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
                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',', a.auditmapping2employee_gid)  as CC2members, a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                           " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                   " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                               " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
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

                                sub = " Request for Audit Approval ";
                                body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "The following Audit is submitted for your Approval. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
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
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Audit Initated for Approval'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }

                            else
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
                                if (lsauditmaker_gid == lsauditapprover_gid && lsauditapprover_gid == lsauditmaker_gid)

                                {
                                    msSQL = " update atm_trn_tauditcreation set auditormakerapprover_flag='Y'" +
                                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid, ',',a.employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
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

                                    sub = " Request for Audit Approval ";
                                    body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "The following Audit is submitted for your Approval. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name )+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
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
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Audit Initated for Approval'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }

                                else
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
                                    if (lsauditchecker_gid == lsauditapprover_gid && lsauditapprover_gid == lsauditchecker_gid)

                                    {
                                        msSQL = " update atm_trn_tauditcreation set auditorcheckerapprover_flag='Y'" +
                                 " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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

                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid, ',',a.employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
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

                                        sub = " Request for Audit Approval ";
                                        body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + "The following Audit is submitted for your Approval. The details are as follows, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
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
                                               "'" + employee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Audit Initated for Approval'," +
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

                                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',', a.employee_gid, ',', d.auditeechecker_gid, ',', a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
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


                                            sub = " Request for Audit Approval ";
                                            body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                            body = body + "<br />";
                                            body = body + "Greetings,  <br />";
                                            body = body + "<br />";
                                            body = body + "The following Audit is submitted for your Approval. The details are as follows, <br />";
                                            body = body + "<br />";
                                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                            body = body + "<br />";
                                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
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
                                                   "'" + employee_gid + "'," +
                                                   "'" + lsto_mail + "'," +
                                                   "'" + cc_mailid + "'," +
                                                   "'Audit Initated for Approval'," +
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
                }
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
            }
        }



        public void DaPostAuditApprovalChecklistAssign(auditcreationapprovallist values, string employee_gid)
        {
            msSQL = " delete from atm_trn_tauditagainstmultipleauditeechecker where  auditcreation_gid = '" + values.auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select newcheckpointgroupname_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";                  
           string newcheckpointgroupname_flag = objdbconn.GetExecuteScalar(msSQL);

            if (newcheckpointgroupname_flag == "Y")
            {
                msSQL = " delete from atm_trn_tauditcreation2checklist " +
                           " where  auditcreation_gid='" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from atm_trn_tobservationscoresample " +
                       " where auditcreation_gid='" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }




            List<auditcreationapproval> ExistingCheckpoint = values.auditcreationapproval.Where(a => a.checklist_flag == "Y" && a.Checked == true).ToList();
            List<auditcreationapproval> NewCheckpoint = values.auditcreationapproval.Where(a => a.checklist_flag == "N" && a.Checked == true).ToList();
            List<auditcreationapproval> RemoveExistingCheckpoint = values.auditcreationapproval.Where(a => a.checklist_flag == "Y" && a.Checked == false).ToList();
            string checklistmaster_gid, auditcreation_gid, auditdepartment_name, audittype_name, checkpointgroup_name, audit_name, checkpoint_intent;
            string checkpoint_description, riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score, total_score;
            string yes_disposition, no_disposition, partial_disposition, na_disposition;
            if (ExistingCheckpoint.Count > 0)
            {
                foreach (var i in ExistingCheckpoint)
                {
                    msSQL = " select  a.checklistmaster_gid,b.checkpointgroupadd_gid, b.checklistmasteradd_gid,c.auditcreation_gid,a.auditdepartment_name, " +
                         " a.audittype_name, b.checkpointgroup_name, a.audit_name, b.checkpoint_intent, b.checkpoint_description," +
                         " b.riskcategory_name, b.positiveconfirmity_name, b.noteto_auditor, b.yes_score, b.no_score, b.partial_score, " +
                         " b.na_score, b.total_score, b.yes_disposition,b.no_disposition,b.partial_disposition,b.na_disposition " +
                         " from atm_mst_tchecklistmaster a" +
                         " left join atm_trn_tauditcreation c on a.checklistmaster_gid = c.checklistmaster_gid" +
                         " left join atm_mst_tchecklistmasteradd b on a.checklistmaster_gid = b.checklistmaster_gid " +
                         " where b.checklistmasteradd_gid = '" + i.checklistmasteradd_gid + "' and c.auditcreation_gid = '" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                        auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                        auditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        audittype_name = objODBCDatareader["audittype_name"].ToString();
                        checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                        audit_name = objODBCDatareader["audit_name"].ToString();
                        checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
                        checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
                        riskcategory_name = objODBCDatareader["riskcategory_name"].ToString();
                        positiveconfirmity_name = objODBCDatareader["positiveconfirmity_name"].ToString();
                        noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
                        yes_score = objODBCDatareader["yes_score"].ToString();
                        no_score = objODBCDatareader["no_score"].ToString();
                        partial_score = objODBCDatareader["partial_score"].ToString();
                        na_score = objODBCDatareader["na_score"].ToString();
                        total_score = objODBCDatareader["total_score"].ToString();
                        yes_disposition = objODBCDatareader["yes_disposition"].ToString();
                        no_disposition = objODBCDatareader["no_disposition"].ToString();
                        partial_disposition = objODBCDatareader["partial_disposition"].ToString();
                        na_disposition = objODBCDatareader["na_disposition"].ToString();
                        lscheckpointgroupadd_gid = objODBCDatareader["checkpointgroupadd_gid"].ToString();
                        objODBCDatareader.Close();

                        msSQL = " update atm_trn_tauditcreation2checklist set " +
                                " auditdepartment_name ='" + auditdepartment_name + "', " +
                                " audittype_name = '" + audittype_name + "', " +
                                " checkpointgroup_name = '" + checkpointgroup_name + "', " +
                                " audit_name ='" + audit_name.Replace("'", "") + "'," +
                                " checkpoint_intent ='" + checkpoint_intent.Replace("'", "") + "'," +
                                " checkpoint_description ='" + checkpoint_description.Replace("'", "") + "'," +
                                " riskcategory_name ='" + riskcategory_name + "', " +
                                " positiveconfirmity_name ='" + positiveconfirmity_name + "', " +
                                " noteto_auditor='" + noteto_auditor.Replace("'", "") + "'," +
                                " yes_score='" + yes_score + "', " +
                                " no_score='" + no_score + "', " +
                                " partial_score='" + partial_score + "', " +
                                " na_score ='" + na_score + "', " +
                                " total_score='" + total_score + "', " +
                                " yes_disposition='" + yes_disposition.Replace("'", "") + "'," +
                                " no_disposition='" + no_disposition.Replace("'", "") + "'," +
                                " partial_disposition='" + partial_disposition.Replace("'", "") + "'," +
                                " na_disposition='" + na_disposition.Replace("'", "") + "'," +
                                " updated_by ='" + employee_gid + "', " +
                                " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where checklistmasteradd_gid ='" + i.checklistmasteradd_gid + " ' and auditcreation_gid ='" + values.auditcreationapproval + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update atm_trn_tobservationscoresample set " +
                               " auditdepartment_name ='" + auditdepartment_name + "', " +
                               " audittype_name = '" + audittype_name + "', " +
                               " checkpointgroup_name = '" + checkpointgroup_name + "', " +
                               " audit_name ='" + audit_name.Replace("'", "") + "'," +
                               " checkpoint_intent ='" + checkpoint_intent.Replace("'", "") + "'," +
                               " checkpoint_description ='" + checkpoint_description.Replace("'", "") + "'," +
                               " riskcategory_name ='" + riskcategory_name + "', " +
                               " positiveconfirmity_name ='" + positiveconfirmity_name + "', " +
                               " noteto_auditor='" + noteto_auditor.Replace("'", "") + "'," +
                               " yes_score='" + yes_score + "', " +
                               " no_score='" + no_score + "', " +
                               " partial_score='" + partial_score + "', " +
                               " na_score ='" + na_score + "', " +
                               " total_score='" + total_score + "', " +
                               " yes_disposition='" + yes_disposition.Replace("'", "") + "'," +
                               " no_disposition='" + no_disposition.Replace("'", "") + "'," +
                               " partial_disposition='" + partial_disposition.Replace("'", "") + "'," +
                               " na_disposition='" + na_disposition.Replace("'", "") + "'," +
                               " updated_by ='" + employee_gid + "', " +
                               " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                               " where checklistmasteradd_gid ='" + i.checklistmasteradd_gid + " ' and auditcreation_gid ='" + values.auditcreationapproval + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    

                    msSQL = "select auditeechecker_gid,auditeemaker_gid,auditeechecker_name,auditeemaker_name,checkpointgroup_gid,checkpointgroupadd_gid from atm_trn_tmultipleauditee " +
                    " where checkpointgroupadd_gid ='" + lscheckpointgroupadd_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            //string lscheckpointgroupadd_gid = (dt["checkpointgroupadd_gid"].ToString());

                            msGetGid1 = objcmnfunctions.GetMasterGID("AAMA");

                            msSQL = " insert into atm_trn_tauditagainstmultipleauditeechecker(" +
                                   " auditagainstmultipleauditeechecker_gid, " +
                                    " auditeemaker_gid," +
                                    " auditeechecker_gid," +
                                    " auditeemaker_name," +
                                     " auditeechecker_name," +
                                    " auditcreation_gid," +
                                    " checkpointgroup_gid ," +
                                      " checkpointgroupadd_gid ," +
                                         " auditeechecker_approvalstatus ," +
                                      " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + dt["auditeemaker_gid"].ToString() + "'," +
                                     "'" + dt["auditeechecker_gid"].ToString() + "'," +
                                      "'" + dt["auditeemaker_name"].ToString() + "'," +
                                     "'" + dt["auditeechecker_name"].ToString() + "'," +
                                     "'" + values.auditcreation_gid + "'," +
                                    "'" + dt["checkpointgroup_gid"].ToString() + "'," +
                                      "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                                       "' --'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
            }
            if (NewCheckpoint.Count > 0)
            {
                foreach (var i in NewCheckpoint)
                {
                    msSQL = " select  a.checklistmaster_gid,b.checkpointgroupadd_gid, b.checklistmasteradd_gid,c.auditcreation_gid,a.auditdepartment_name, " +
                         " a.audittype_name, b.checkpointgroup_name, a.audit_name, b.checkpoint_intent, b.checkpoint_description," +
                         " b.riskcategory_name, b.positiveconfirmity_name, b.noteto_auditor, b.yes_score, b.no_score, b.partial_score, " +
                         " b.na_score, b.total_score, b.yes_disposition,b.no_disposition,b.partial_disposition,b.na_disposition " +
                         " from atm_mst_tchecklistmaster a" +
                         " left join atm_trn_tauditcreation c on a.checklistmaster_gid = c.checklistmaster_gid" +
                         " left join atm_mst_tchecklistmasteradd b on a.checklistmaster_gid = b.checklistmaster_gid " +
                         " where b.checklistmasteradd_gid = '" + i.checklistmasteradd_gid + "' and c.auditcreation_gid = '" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                        auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                        auditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        audittype_name = objODBCDatareader["audittype_name"].ToString();
                        checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                        audit_name = objODBCDatareader["audit_name"].ToString();
                        checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
                        checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
                        riskcategory_name = objODBCDatareader["riskcategory_name"].ToString();
                        positiveconfirmity_name = objODBCDatareader["positiveconfirmity_name"].ToString();
                        noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
                        yes_score = objODBCDatareader["yes_score"].ToString();
                        no_score = objODBCDatareader["no_score"].ToString();
                        partial_score = objODBCDatareader["partial_score"].ToString();
                        na_score = objODBCDatareader["na_score"].ToString();
                        total_score = objODBCDatareader["total_score"].ToString();
                        yes_disposition = objODBCDatareader["yes_disposition"].ToString();
                        no_disposition = objODBCDatareader["no_disposition"].ToString();
                        partial_disposition = objODBCDatareader["partial_disposition"].ToString();
                        na_disposition = objODBCDatareader["na_disposition"].ToString();
                        lscheckpointgroupadd_gid = objODBCDatareader["checkpointgroupadd_gid"].ToString();

                        objODBCDatareader.Close();

                        msGetGid = objcmnfunctions.GetMasterGID("AC2C");
                        msSQL = " insert into atm_trn_tauditcreation2checklist(" +
                                " auditcreation2checklist_gid," +
                                " checklistmasteradd_gid," +
                                " checklistmaster_gid," +
                                " checkpointgroupadd_gid," +
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
                                " yes_disposition, " +
                                " no_disposition, " +
                                " partial_disposition, " +
                                " na_disposition, " +
                                " total_score ," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + i.checklistmasteradd_gid + "', " +
                               "'" + checklistmaster_gid + "'," +
                               "'" + lscheckpointgroupadd_gid + "'," +                              
                               "'" + auditcreation_gid + "'," +
                               "'" + auditdepartment_name + "'," +
                               "'" + audittype_name + "'," +
                               "'" + checkpointgroup_name + "'," +
                               "'" + audit_name.Replace("'", "") + "'," +
                               "'" + checkpoint_intent.Replace("'", "") + "'," +
                               "'" + checkpoint_description.Replace("'", "") + "'," +
                               "'" + riskcategory_name + "'," +
                               "'" + positiveconfirmity_name + "'," +
                               "'" + noteto_auditor.Replace("'", "") + "'," +
                               "'" + yes_score.Replace("'", "") + "'," +
                               "'" + no_score.Replace("'", "") + "'," +
                               "'" + partial_score.Replace("'", "") + "'," +
                               "'" + na_score.Replace("'", "") + "'," +
                               "'" + yes_disposition.Replace("'", "") + "'," +
                               "'" + no_disposition.Replace("'", "") + "'," +
                               "'" + partial_disposition.Replace("'", "") + "'," +
                               "'" + na_disposition.Replace("'", "") + "'," +
                               "'" + total_score + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msGetGid1 = objcmnfunctions.GetMasterGID("OBSS");

                        msSQL = " insert into atm_trn_tobservationscoresample(" +
                              " observationscoresample_gid, " +
                               " checklistmasteradd_gid," +
                               " auditcreation2checklist_gid," +
                               " checklistmaster_gid," +
                               " checkpointgroupadd_gid," +
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
                               " total_score ," +
                               " yes_disposition, " +
                               " no_disposition, " +
                               " partial_disposition, " +
                               " na_disposition," +
                                 " created_by," +
                               " created_date)" +
                               " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + i.checklistmasteradd_gid + "', " +
                                 "'" + msGetGid + "'," +
                               "'" + checklistmaster_gid + "'," +
                               "'" + lscheckpointgroupadd_gid + "'," +
                               "'" + auditcreation_gid + "'," +
                               "'" + auditdepartment_name + "'," +
                               "'" + audittype_name + "'," +
                               "'" + checkpointgroup_name + "'," +
                               "'" + audit_name.Replace("'", "") + "'," +
                               "'" + checkpoint_intent.Replace("'", "") + "'," +
                               "'" + checkpoint_description.Replace("'", "") + "'," +
                               "'" + riskcategory_name + "'," +
                               "'" + positiveconfirmity_name + "'," +
                               "'" + noteto_auditor.Replace("'", "") + "'," +
                               "'" + yes_score.Replace("'", "") + "'," +
                               "'" + no_score.Replace("'", "") + "'," +
                               "'" + partial_score.Replace("'", "") + "'," +
                               "'" + na_score.Replace("'", "") + "'," +
                               "'" + total_score + "'," +
                               "'" + yes_disposition.Replace("'", "") + "'," +
                               "'" + no_disposition.Replace("'", "") + "'," +
                               "'" + partial_disposition.Replace("'", "") + "'," +
                               "'" + na_disposition.Replace("'", "") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    msSQL = "select auditeechecker_gid,auditeemaker_gid,auditeechecker_name,auditeemaker_name,checkpointgroup_gid,checkpointgroupadd_gid from atm_trn_tmultipleauditee " +
                    " where checkpointgroupadd_gid ='" + lscheckpointgroupadd_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            //string lscheckpointgroupadd_gid = (dt["checkpointgroupadd_gid"].ToString());

                            msGetGid1 = objcmnfunctions.GetMasterGID("AAMA");

                            msSQL = " insert into atm_trn_tauditagainstmultipleauditeechecker(" +
                                   " auditagainstmultipleauditeechecker_gid, " +
                                    " auditeemaker_gid," +
                                    " auditeechecker_gid," +
                                    " auditeemaker_name," +
                                     " auditeechecker_name," +
                                    " auditcreation_gid," +
                                    " checkpointgroup_gid ," +
                                      " checkpointgroupadd_gid ," +
                                         " auditeechecker_approvalstatus ," +
                                      " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + dt["auditeemaker_gid"].ToString() + "'," +
                                     "'" + dt["auditeechecker_gid"].ToString() + "'," +
                                      "'" + dt["auditeemaker_name"].ToString() + "'," +
                                     "'" + dt["auditeechecker_name"].ToString() + "'," +
                                     "'" + values.auditcreation_gid + "'," +
                                    "'" + dt["checkpointgroup_gid"].ToString() + "'," +
                                      "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
                                       "' --'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
            }
            if (RemoveExistingCheckpoint.Count > 0)
            {
                foreach (var i in RemoveExistingCheckpoint)
                {
                    msSQL = " delete from atm_trn_tauditcreation2checklist " +
                            " where checklistmasteradd_gid='" + i.checklistmasteradd_gid + "' and auditcreation_gid='" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " delete from atm_trn_tobservationscoresample " +
                           " where checklistmasteradd_gid='" + i.checklistmasteradd_gid + "' and auditcreation_gid='" + values.auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tauditcreation set auditapproval_flag='Y', " +
                        " approval_status ='Initiate Audit Approved', status='Initiate Audit Approved', " +
                       " auditee_visible = 'Visible to Auditee', " +
                        " auditapproved_by ='" + employee_gid + "', " +
                        " auditapproved_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Audit Approved Successfully!";

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

                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,(a.created_by) as auditcreate_gid, a.auditmapping_gid,d.auditeemaker_gid, d.auditeechecker_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                   " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                        lsemployee_gid = objODBCDatareader["auditeechecker_gid"].ToString();
                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                        lscreated_by = objODBCDatareader["auditcreate_gid"].ToString();
                        lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
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

                    sub = " Audit Creation Approved  ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Audit has been Approved. Kindly find the details below, <br />";
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
                           "'" + employee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Audit Creation Approved'," +
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

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid, ',' , a.auditmapping2employee_gid )  as CC2members, a.auditmapping_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
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

                        sub = " Audit Creation Approved  ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Audit has been Approved. Kindly find the details below, <br />";
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
                               "'" + employee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit Creation Approved'," +
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

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid)  as CC2members, a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                       " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                               " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                           " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
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

                            sub = " Audit Creation Approved  ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "Audit has been Approved. Kindly find the details below, <br />";
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
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit Creation Approved'," +
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

                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, group_concat(distinct d.auditeemaker_gid, ',', d.auditeechecker_gid)  as CC2members, a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                       " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                 " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
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

                                sub = " Audit Creation Approved  ";
                                body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name)+ ",<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "Audit has been Approved. Kindly find the details below, <br />";
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
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Audit Creation Approved'," +
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

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',', a.employee_gid, ',', d.auditeechecker_gid, ',', a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                        " left join atm_trn_tauditagainstmultipleauditeechecker d on d.auditcreation_gid = a.auditcreation_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
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


                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
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

                                    sub = " Audit Creation Approved  ";
                                    body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name) + ",<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "Audit has been Approved. Kindly find the details below, <br />";
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
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Audit Creation Approved'," +
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
                values.message = "Error Occured..!";
                values.status = false;
            }

        }

        public void Daobservationfill(string auditcreation_gid, result values)
        {
            msSQL = " update atm_trn_tauditcreation set observation_fill='Y' where auditcreation_gid = '" + auditcreation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }


        public void DaChecklistAssignView(string auditcreation_gid, MdlAtmTrnAuditCreation values)
        {
            try
            {

                msSQL = " select b.auditcreation2checklist_gid,a.auditcreation_gid, auditdepartment_name, audittype_name, checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
                          "riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score, total_score " +
                          " from atm_trn_tauditcreation a " +
                          " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
                            " where b.auditcreation_gid='" + auditcreation_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchecklistassignview_list = new List<checklistassignview_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getchecklistassignview_list.Add(new checklistassignview_list
                        {
                            auditcreation2checklist_gid = (dr_datarow["auditcreation2checklist_gid"].ToString()),
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
                            total_score = (dr_datarow["total_score"].ToString()),


                        });
                    }
                    values.checklistassignview_list = getchecklistassignview_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaDeleteChecklistAssign(auditchecklistassign values, string employee_gid)
        {

            foreach (string i in values.auditcreation2checklist_gid)
            {
                msSQL = " select auditcreation2checklist_gid,a.auditcreation_gid, auditdepartment_name, audittype_name, checkpointgroup_name, a.audit_name, checkpoint_intent, checkpoint_description," +
                         "riskcategory_name, positiveconfirmity_name, noteto_auditor, yes_score, no_score, partial_score, na_score, total_score " +
                         " from atm_trn_tauditcreation a " +
                         " left join atm_trn_tauditcreation2checklist b on a.auditcreation_gid=b.auditcreation_gid " +
                           " where b.auditcreation2checklist_gid='" + i + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " delete from atm_trn_tauditcreation2checklist where auditcreation2checklist_gid='" + i + "'";


                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Checklist Assigned Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }


        public void DaGetAuditCreationIntent(string auditcreation2checklist_gid, auditchecklistassign values)
        {
            msSQL = " select checkpoint_intent  from atm_trn_tauditcreation2checklist " +
                  " where auditcreation2checklist_gid='" + auditcreation2checklist_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_intent = objODBCDatareader["checkpoint_intent"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetAuditCreationDescription(string auditcreation2checklist_gid, auditchecklistassign values)
        {
            msSQL = " select checkpoint_description  from atm_trn_tauditcreation2checklist " +
                  " where auditcreation2checklist_gid='" + auditcreation2checklist_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checkpoint_description = objODBCDatareader["checkpoint_description"].ToString();
            }
            objODBCDatareader.Close();

        }
        public void DaGetAuditCreationAuditor(string auditcreation2checklist_gid, auditchecklistassign values)
        {
            msSQL = " select noteto_auditor  from atm_trn_tauditcreation2checklist " +
                  " where auditcreation2checklist_gid='" + auditcreation2checklist_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.noteto_auditor = objODBCDatareader["noteto_auditor"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaGetMyClosedAuditTask(MdlAtmTrnAuditCreation values)
        {
            try
            {
                msSQL = " SELECT a.auditcreation_gid,a.auditfrequency_name,a.audittype_name,a.auditpriority_name,a.checklistmaster_gid,a.audit_name, a.auditdepartment_name, " +
                        " a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,date_format(a.due_date,'%d-%m-%Y') as due_date, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.approval_status, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where status = 'closed' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
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
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetMyOpenAuditTask(MdlAtmTrnAuditCreation values)
        {
            try
            {
                msSQL = " SELECT a.auditcreation_gid,a.auditfrequency_name,a.auditpriority_name,a.audittype_name,a.checklistmaster_gid,a.audit_name,a.auditdepartment_name, " +
                        " a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,date_format(a.due_date,'%d-%m-%Y') as due_date, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " case when a.approval_status is null then '-'" +
                        " else a.approval_status end as approval_status," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where status in ('Open','Initiate Audit Approval pending') order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaGetMyApprovedAuditTask(MdlAtmTrnAuditCreation values)
        {
            try
            {
                msSQL = " SELECT distinct a.auditcreation_gid,a.audittype_name,a.auditfrequency_name,a.auditpriority_name,a.checklistmaster_gid,a.audit_name,auditdepartment_name, " +
                        " a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,date_format(a.due_date,'%d-%m-%Y') as due_date, " +
                        " date_format(a.auditapproved_date,'%d-%m-%Y %h:%i %p') as created_date,a.approval_status, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as auditapproved_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where status = 'Initiate Audit Approved' and auditapproval_flag='Y' order by a.auditapproved_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["auditapproved_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetMyOnholdAuditTask(MdlAtmTrnAuditCreation values)
        {
            try
            {
                msSQL = " SELECT a.auditcreation_gid,a.auditfrequency_name,a.audittype_name,a.auditpriority_name,a.checklistmaster_gid,auditdepartment_name, " +
                        " a.audit_name,a.audit_uniqueno,a.auditmaker_name,a.auditchecker_name,a.status,a.approval_status, " +
                        " date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where status = 'Hold' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaGetAuditCreationCounts(getAuditCount values)
        {
            msSQL = " select (select count(auditcreation_gid) from atm_trn_tauditcreation where status = 'Hold') AS auditsonhold_count, " +
                    " (select count(auditcreation_gid) from atm_trn_tauditcreation where status = 'Initiate Audit Approved' and auditapproval_flag='Y') As Approvedaudit_count, " +
                    " (select count(auditcreation_gid) from atm_trn_tauditcreation where status in ('Open','Initiate Audit Approval pending')) As openaudit_count, " +
                    " (select count(auditcreation_gid) from atm_trn_tauditcreation where status = 'closed') As closedaudit_count," +
                   " (select count(auditcreation_gid) from atm_trn_tauditcreation where status = 'Completed') As completedaudit_count," +
            " (select count(auditcreation_gid) from atm_trn_tauditcreation where approval_status = 'Initiate Audit Rejected') As rejectedaudit_count";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditsonhold_count = objODBCDatareader["auditsonhold_count"].ToString();
                values.openaudit_count = objODBCDatareader["openaudit_count"].ToString();
                values.closedaudit_count = objODBCDatareader["closedaudit_count"].ToString();
                values.Approvedaudit_count = objODBCDatareader["Approvedaudit_count"].ToString();
                values.completedaudit_count = objODBCDatareader["completedaudit_count"].ToString();
                values.rejectedaudit_count = objODBCDatareader["rejectedaudit_count"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void DaGetDeleteSampleImport(result values, string sampleimport_gid)
        {
            msSQL = "DELETE FROM atm_trn_tsampleexcelimport WHERE sampleimport_gid ='" + sampleimport_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from atm_trn_tsampleimport WHERE sampleimport_gid ='" + sampleimport_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Selected Sample Data deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public string DaGetvalueswithComma(string[] array)
        {
            var value = "";
            foreach (var h in array)
            {
                value += "'" + h + "',";
            }
            value = value.TrimEnd(',');
            return value;
        }
        public void DaGetAuditName(string auditdepartment_gid, MdlAtmTrnAuditCreation values)

        {
            try
            {
                msSQL = "select distinct checklistmaster_gid,audit_name,auditdepartment_name,auditdepartment_gid,entity_name from atm_mst_tchecklistmaster " +
                                 " where auditdepartment_gid = '" + auditdepartment_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditname_list = new List<auditname_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditname_list.Add(new auditname_list
                        {
                            auditdepartment_gid = (dr_datarow["auditdepartment_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),

                        });
                    }
                    values.auditname_list = getauditname_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetAuditDepartmentList(string auditdepartment_gid, MdlAtmTrnAuditCreation values)

        {
            try
            {
                msSQL = "select distinct checklistmaster_gid,auditdepartment_name,auditdepartment_gid from atm_mst_tchecklistmaster " +
                                 " where auditdepartment_gid = '" + auditdepartment_gid + "' group by auditdepartment_name ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditdepartmentname_list = new List<auditdepartmentname_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditdepartmentname_list.Add(new auditdepartmentname_list
                        {
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            auditdepartment_gid = (dr_datarow["auditdepartment_gid"].ToString()),

                        });
                    }
                    values.auditdepartmentname_list = getauditdepartmentname_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetAuditnameDetail(string checklistmaster_gid, MdlAtmTrnAuditCreation values)
        {
            msSQL = " SELECT checklistmaster_gid,checkpointgroup_gid,audittype_name,entity_name,audit_description,checkpointgroup_name FROM atm_mst_tchecklistmaster " +
                    " where checklistmaster_gid = '" + checklistmaster_gid + "' order by checklistmaster_gid desc ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                values.audittype_name = objODBCDatareader["audittype_name"].ToString();
                values.entity_name = objODBCDatareader["entity_name"].ToString();
                values.audit_description = objODBCDatareader["audit_description"].ToString();
                values.checkpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                values.checkpointgroup_gid = objODBCDatareader["checkpointgroup_gid"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetAuditCreationApprover(MdlAtmTrnAuditCreation values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.auditcreation_gid,a.checklistmaster_gid,a.audittype_name,d.sampleimport_gid,a.auditapprover_name,auditdepartment_name, " +
                        " a.auditmapping2employee_gid, a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.auditapproval_flag, " +
                        " a.status,a.approval_status,a.auditmaker_name,a.auditchecker_name,a.status_flag,a.auditmapping_gid, " +
                        " date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate, " +
                        " date_format(a.auditperiod_todate,'%d-%m-%Y') as auditperiod_todate, a.auditeemaker_name, a.auditeechecker_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join atm_trn_tsampleexcelimport d on d.auditcreation_gid = a.auditcreation_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where approval_status ='Initiate Audit Approval pending' and  status = 'Initiate Audit Approval pending' and a.auditmapping_gid= '" + employee_gid + "' group by a.auditcreation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditapprover_name = (dr_datarow["auditapprover_name"].ToString()),
                            auditmapping2employee_gid = (dr_datarow["auditmapping2employee_gid"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditperiod_fromdate = (dr_datarow["auditperiod_fromdate"].ToString()),
                            auditperiod_todate = (dr_datarow["auditperiod_todate"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }
        public void DaGetAuditCreationRejected(MdlAtmTrnAuditCreation values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.auditcreation_gid,a.checklistmaster_gid,a.audittype_name,d.sampleimport_gid,a.auditapprover_name,auditdepartment_name, " +
                        " a.auditmapping2employee_gid, a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.auditapproval_flag, " +
                        " a.status,a.approval_status,a.auditmaker_name,a.auditchecker_name,a.status_flag,a.auditmapping_gid, " +
                        " date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate, " +
                        " date_format(a.auditperiod_todate,'%d-%m-%Y') as auditperiod_todate, a.auditeemaker_name, a.auditeechecker_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join atm_trn_tsampleexcelimport d on d.auditcreation_gid = a.auditcreation_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where approval_status ='Initiate Audit Rejected' group by a.auditcreation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreation_list = new List<auditcreation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreation_list.Add(new auditcreation_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditapprover_name = (dr_datarow["auditapprover_name"].ToString()),
                            auditmapping2employee_gid = (dr_datarow["auditmapping2employee_gid"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditperiod_fromdate = (dr_datarow["auditperiod_fromdate"].ToString()),
                            auditperiod_todate = (dr_datarow["auditperiod_todate"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreation_list = getauditcreation_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }

        }

        public void DaGetMyApprovedAudit(MdlAtmTrnAuditCreation values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.auditcreation_gid,a.audittype_name,a.checklistmaster_gid,d.sampleimport_gid,a.auditapprover_name,auditdepartment_name, " +
                        " a.auditmapping2employee_gid, a.audit_name,a.auditfrequency_name,a.auditpriority_name,a.audit_uniqueno,a.auditapproval_flag, " +
                        " a.status,a.approval_status,a.auditmaker_name,a.auditchecker_name,a.status_flag,a.auditmapping_gid, " +
                        " date_format(a.due_date,'%d-%m-%Y') as due_date,date_format(a.auditapproved_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " date_format(a.auditperiod_fromdate,'%d-%m-%Y') as auditperiod_fromdate, " +
                        " date_format(a.auditperiod_todate,'%d-%m-%Y') as auditperiod_todate, a.auditeemaker_name, a.auditeechecker_name," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as auditapproved_by" +
                        " FROM atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.auditapproved_by = b.employee_gid" +
                        " left join atm_trn_tsampleexcelimport d on d.auditcreation_gid = a.auditcreation_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        //" where approval_status ='Approved' and  status = 'Approved' and a.auditmapping_gid= '" + employee_gid + "' group by a.auditcreation_gid desc ";
                        " where status not in ('Open','Initiate Audit Approval pending') and a.auditmapping_gid= '" + employee_gid + "' group by a.auditcreation_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditcreationapproved_list = new List<auditcreationapproved_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditcreationapproved_list.Add(new auditcreationapproved_list
                        {

                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            checklistmaster_gid = (dr_datarow["checklistmaster_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            audit_uniqueno = (dr_datarow["audit_uniqueno"].ToString()),
                            audit_maker = (dr_datarow["auditmaker_name"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                            audit_checker = (dr_datarow["auditchecker_name"].ToString()),
                            auditapprover_name = (dr_datarow["auditapprover_name"].ToString()),
                            auditmapping2employee_gid = (dr_datarow["auditmapping2employee_gid"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            status_update = (dr_datarow["status"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            status_flag = (dr_datarow["status_flag"].ToString()),
                            created_by = (dr_datarow["auditapproved_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            auditperiod_fromdate = (dr_datarow["auditperiod_fromdate"].ToString()),
                            auditperiod_todate = (dr_datarow["auditperiod_todate"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                        });
                    }
                    values.auditcreationapproved_list = getauditcreationapproved_list;

                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }



        }

        public void DaGetApprovedAuditCounts(getAuditCount values, string employee_gid)
        {
            msSQL = " select (select count(auditcreation_gid) from atm_trn_tauditcreation where approval_status ='Initiate Audit Approval pending' and status = 'Initiate Audit Approval pending' and auditmapping_gid= '" + employee_gid + "') AS pendingapproval_count, " +
                    //" (select count(auditcreation_gid) from atm_trn_tauditcreation  where approval_status ='Approved' and  status = 'Approved' and auditmapping_gid= '" + employee_gid + "') As approved_count ";
                    " (select count(auditcreation_gid) from atm_trn_tauditcreation  where  status not in ('Open','Initiate Audit Approval pending') and auditmapping_gid= '" + employee_gid + "') As approved_count, " +
            " (select count(auditcreation_gid) from atm_trn_tauditcreation  where  approval_status='Initiate Audit Rejected' and auditmapping_gid= '" + employee_gid + "') As rejected_count ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.pendingapproval_count = objODBCDatareader["pendingapproval_count"].ToString();
                values.approved_count = objODBCDatareader["approved_count"].ToString();
                values.rejected_count = objODBCDatareader["rejected_count"].ToString();

            }
            objODBCDatareader.Close();
        }

        public void DaGetCompletedAudit(MdlAtmTrnCompletedAudit values, string Employee_gid)
        {

            try
            {
                msSQL = "select distinct a.auditcreation_gid,a.audit_name,a.audittype_name,a.auditdepartment_name,a.auditpriority_name,a.audit_uniqueno,a.approval_status,a.due_date, " +
                        " a.status,a.approval_flag,a.status_flag,a.checklistmaster_gid, a.auditmaker_name, a.auditchecker_name,a.auditapprover_name," +
                        " a.employee_gid as auditmaker_gid,a.auditmapping_gid as auditchecker_gid,a.auditmapping2employee_gid as auditapprover_gid, " +
                        " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as created_by " +
                        " from atm_trn_tauditcreation a " +
                        " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                        " left join adm_mst_tuser e on e.user_gid = f.user_gid" +
                        " where a.status = 'Completed' order by a.created_date desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompletedaudit_list = new List<completedaudit_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcompletedaudit_list.Add(new completedaudit_list
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
                    values.completedaudit_list = getcompletedaudit_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaPostSampleAssign(auditchecklistassign values, string employee_gid)
        {

            
            //foreach (string i in values.checklistmasteradd_gid)
            //{
               

                msSQL = " select a.observationscoresample_gid,a.checklistmaster_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
                     " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
                     " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
                     " a.partial_score, a.na_score, a.samplecapture_field,a.sampleobservation_percentage,a.sampleobservation_score,a.yes_disposition, a.no_disposition, a.partial_disposition, " +
                     " a.na_disposition from atm_trn_tobservationscoresample a " +
                       " where a.auditcreation_gid = '" + values.auditcreation_gid + "' and a.sampleimport_gid is null ";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                       
                        msGetGid1 = objcmnfunctions.GetMasterGID("OBSS");

                        msSQL = " insert into atm_trn_tobservationscoresample(" +
                               " observationscoresample_gid, " +
                                " checklistmasteradd_gid," +
                                " auditcreation2checklist_gid," +
                                " sampleimport_gid," +
                                " checklistmaster_gid," +
                                " checkpointgroupadd_gid," +
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
                                " total_score ," +
                                " yes_disposition, " +
                                " no_disposition, " +
                                " partial_disposition, " +
                                " na_disposition," +
                                 " samplecapture_score," +
                                  " samplecapture_field," +
                                   " sampleobservation_score," +
                                  " sampleobservation_percentage," +
                                  " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid1 + "'," +
                                "'" + dt["checklistmasteradd_gid"].ToString() + "', " +
                                 "'" + dt["auditcreation2checklist_gid"].ToString() + "'," +
                                  "'" + values.sampleimport_gid + "'," +
                                 "'" + dt["checklistmaster_gid"].ToString() + "'," +
                                "'" + dt["checkpointgroupadd_gid"].ToString() + "'," +
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
                                "'" + dt["total_score"].ToString() + "'," +
                                "'" + dt["yes_disposition"].ToString() + "'," +
                                "'" + dt["no_disposition"].ToString() + "'," +
                                "'" + dt["partial_disposition"].ToString() + "'," +
                                "'" + dt["na_disposition"].ToString() + "'," +
                                "'" + dt["samplecapture_score"].ToString() + "'," +
                                "'" + dt["samplecapture_field"].ToString() + "'," +
                                 "'" + dt["sampleobservation_score"].ToString() + "'," +
                                "'" + dt["sampleobservation_percentage"].ToString() + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            
            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tobservationscoresample set status_flag='Y',samplecapture_score ='Null',samplecapture_field ='Null',sampleobservation_percentage = '0',sampleobservation_score = '0'" +
                      " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is Null";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tobservationscoresample set samplechecklistverified_flag='Y'" +
                     " where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid ='" + values.sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update atm_trn_tauditcreation set checklistverified_flag='Y',samplestatus_flag='Y'" +
                     " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Sample observation score added Successfully..!";
               
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
            }
        }
        public void DaPostSampleAssignUpdate(auditchecklistassign values, string employee_gid)
        {


            //foreach (string i in values.checklistmasteradd_gid)
            //{


            //msSQL = " select a.observationscoresample_gid,a.checklistmaster_gid,a.auditcreation2checklist_gid,a.auditcreation_gid,a.checkpointgroupadd_gid,a.checklistmasteradd_gid, a.auditdepartment_name, " +
            //     " a.audittype_name, a.checkpointgroup_name, a.audit_name, a.checkpoint_intent, a.checkpoint_description, " +
            //     " a.riskcategory_name, a.positiveconfirmity_name, a.noteto_auditor, a.yes_score, a.no_score,a.samplecapture_score, a.total_score, " +
            //     " a.partial_score, a.na_score, a.samplecapture_field,a.sampleobservation_percentage,a.sampleobservation_score,a.yes_disposition, a.no_disposition, a.partial_disposition, " +
            //     " a.na_disposition from atm_trn_tobservationscoresample a " +
            //       " where a.auditcreation_gid = '" + values.auditcreation_gid + "' and a.sampleimport_gid = '" + values.sampleimport_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);

            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {

                    //msGetGid1 = objcmnfunctions.GetMasterGID("OBSS");

                    msSQL = " update atm_trn_tobservationscoresample set " +
                            //" observationscoresample_gid ='" + dt["observationscoresample_gid"].ToString() + "', " +
                             //" checklistmasteradd_gid ='" + dt["checklistmasteradd_gid"].ToString() + "', " +
                             //" auditcreation2checklist_gid ='" + dt["auditcreation2checklist_gid"].ToString() + "', " +
                             // " checklistmaster_gid ='" + dt["checklistmaster_gid"].ToString() + "', " +
                             //" checkpointgroupadd_gid ='" + dt["checkpointgroupadd_gid"].ToString() + "', " +
                             //" auditcreation_gid ='" + dt["auditcreation_gid"].ToString() + "', " +
                             // " auditdepartment_name ='" + dt["auditdepartment_name"].ToString() + "', " +
                             // " audittype_name ='" + dt["audittype_name"].ToString() + "', " +
                             // " checkpointgroup_name ='" + dt["checkpointgroup_name"].ToString() + "', " +
                             // " audit_name ='" + dt["audit_name"].ToString() + "', " +
                             //" checkpoint_intent ='" + dt["checkpoint_intent"].ToString() + "', " +
                             //" checkpoint_description ='" + dt["checkpoint_description"].ToString() + "', " +
                             //" riskcategory_name ='" + dt["riskcategory_name"].ToString() + "', " +
                             // " positiveconfirmity_name ='" + dt["positiveconfirmity_name"].ToString() + "', " +
                             // " noteto_auditor ='" + dt["noteto_auditor"].ToString() + "', " +
                             //" yes_score ='" + dt["yes_score"].ToString() + "', " +
                             //" no_score ='" + dt["no_score"].ToString() + "', " +
                             //" partial_score ='" + dt["partial_score"].ToString() + "', " +
                             //" na_score ='" + dt["na_score"].ToString() + "', " +
                             // " total_score ='" + dt["total_score"].ToString() + "', " +
                             // " yes_disposition ='" + dt["yes_disposition"].ToString() + "', " +
                             //" no_disposition ='" + dt["no_disposition"].ToString() + "', " +
                             //  " partial_disposition ='" + dt["partial_disposition"].ToString() + "', " +
                             // " na_disposition ='" + dt["na_disposition"].ToString() + "', " +
                             //" samplecapture_score ='" + dt["samplecapture_score"].ToString() + "', " +
                             //" samplecapture_field ='" + dt["samplecapture_field"].ToString() + "', " +
                             //" sampleobservation_score ='" + dt["sampleobservation_score"].ToString() + "', " +
                             //" sampleobservation_percentage ='" + dt["sampleobservation_percentage"].ToString() + "', " +
                              " updated_by='" + employee_gid + "'," +
                             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where sampleimport_gid='" + values.sampleimport_gid + "' ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //}

            if (mnResult != 0)
            {
               
                values.status = true;
                values.message = "Sample observation score added Successfully..!";

            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
            }
        }
        public void DaSampleObservationScore(string auditcreation_gid, string sampleimport_gid, string observationscoresample_gid, MdlAtmTrnAuditCreation values)
        {
            msSQL = " select observationscoresample_gid from atm_trn_tobservationscoresample where " +
                        " sampleimport_gid='" + sampleimport_gid + "'";
            values.sampleimport_gid = objdbconn.GetExecuteScalar(msSQL);
            if (values.sampleimport_gid == "")
            {
                msSQL = " select auditcreation_gid,checklistmaster_gid,total_score,observation_fill,sampleobservation_percentage,sampleobservation_score" +
                 " from atm_trn_tobservationscoresample  " +
                " where auditcreation_gid='" + auditcreation_gid + "' and sampleimport_gid is null";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                    values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                    values.sampleobservation_score = objODBCDatareader["sampleobservation_score"].ToString();
                    values.sampleobservation_percentage = objODBCDatareader["sampleobservation_percentage"].ToString();
                    values.total_score = objODBCDatareader["total_score"].ToString();
                    values.observation_fill = objODBCDatareader["observation_fill"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = " select count(*) as openquery from atm_trn_tsampleraisequery where auditcreation_gid = '" + auditcreation_gid + "'" +
                        " and sampleraisequery_status = 'Open'";
                values.openquerycount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as openquery from atm_trn_tobservationscoresample where auditcreation_gid = '" + values.auditcreation_gid + "' and sampleimport_gid is null and samplecapture_field = 'Null'";
                values.samplecapture_field = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
            }
            else
            {
                msSQL = " select auditcreation_gid,checklistmaster_gid,total_score,observation_fill,samplechecklistverified_flag,sampleobservation_percentage,sampleobservation_score" +
                " from atm_trn_tobservationscoresample  " +
               " where auditcreation_gid='" + auditcreation_gid + "' and sampleimport_gid='" + sampleimport_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                    values.checklistmaster_gid = objODBCDatareader["checklistmaster_gid"].ToString();
                    values.sampleobservation_score = objODBCDatareader["sampleobservation_score"].ToString();
                    values.sampleobservation_percentage = objODBCDatareader["sampleobservation_percentage"].ToString();
                    values.total_score = objODBCDatareader["total_score"].ToString();
                    values.samplechecklistverified_flag = objODBCDatareader["samplechecklistverified_flag"].ToString();
                    values.observation_fill = objODBCDatareader["observation_fill"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = " select count(*) as openquery from atm_trn_tsampleraisequery where auditcreation_gid = '" + auditcreation_gid + "'" +
                        " and sampleraisequery_status = 'Open'";
                values.openquerycount = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
            }
        }
        public void DaGetAuditeeList(string employee_gid,multipleauditee values)
        {
            try
            {
                msSQL = "select multipleauditee_gid,auditeechecker_name,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tmultipleauditee where " +
                   " auditcreation_gid ='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),

                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaPostMultipleAuditee(multipleauditee values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("MUAU");
            msSQL = " insert into atm_trn_tmultipleauditee(" +
                    " multipleauditee_gid," +
                    " auditcreation_gid," +
                    " auditeemaker_gid," +
                     "auditeemaker_name," +
                    " auditeechecker_gid," +
                    " auditeechecker_name," +
                   " auditeeapproval_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.auditeemaker_gid + "'," +
                     "'" + values.auditeemaker_name + "'," +
                    "'" + values.auditeechecker_gid + "'," +
                     "'" + values.auditeechecker_name + "'," +
                      "' --'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Auditee Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
       
        public void DaGetAuditeeSummaryList(string auditcreation_gid, multipleauditee values)
        {
            try
            {
                msSQL = "select auditcreation_gid,auditeechecker_name,auditeechecker_approvalstatus,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tauditagainstmultipleauditeechecker where " +
                   " auditcreation_gid ='" + auditcreation_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_approvalstatus = (dr_datarow["auditeechecker_approvalstatus"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),

                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public bool DaMultipleAuditeeEdit(multipleauditee values,string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("MUAU");
            msSQL = " insert into atm_trn_tmultipleauditee(" +
                    " multipleauditee_gid," +
                    " auditcreation_gid," +
                    " auditeemaker_gid," +
                     "auditeemaker_name," +
                    " auditeechecker_gid," +
                    " auditeechecker_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.auditeemaker_gid + "'," +
                     "'" + values.auditeemaker_name + "'," +
                    "'" + values.auditeechecker_gid + "'," +
                     "'" + values.auditeechecker_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            objdbconn.CloseConn();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Auditee added successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error occured";
                return false;
            }
        }
        public void DaTempDeleteAuditee(string employee_gid, multipleauditee values)
        {
            msSQL = "delete from atm_trn_tmultipleauditee where checkpointgroupadd_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Auditee deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the Auditee";
                values.status = false;

            }
        }
        public void DaDeleteAuditee(string multipleauditee_gid, multipleauditee values)
        {
            msSQL = "delete from atm_trn_tmultipleauditee where multipleauditee_gid='" + multipleauditee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Auditee deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the Auditee";
                values.status = false;

            }
        }
        public void DaGetTempAssignedAuditeeList(string auditcreation_gid, string employee_gid, multipleauditee values)
        {
            values.employee_gid = employee_gid;

            {
                msSQL = "select multipleauditee_gid,auditeemaker_gid,auditeemaker_name,auditeechecker_name,auditeechecker_gid,auditeechecker_approvalflag from atm_trn_tmultipleauditee where " +
                   " auditcreation_gid ='" + employee_gid + "' or auditcreation_gid='" + auditcreation_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {
                            multipleauditee_gid = (dr_datarow["multipleauditee_gid"].ToString()),
                            auditeechecker_approvalflag = (dr_datarow["auditeechecker_approvalflag"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
        }
        public void DaDeleteAuditeeList(string multipleauditee_gid, multipleauditee values)
        {
            msSQL = "delete from atm_trn_tmultipleauditee where multipleauditee_gid='" + multipleauditee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Auditee deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the auditee";
                values.status = false;

            }
        }
        public void DaInitiateAuditRejected(string employee_gid, multipleauditee values)
        {
         
                msSQL = " update atm_trn_tauditcreation set " +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " rejected_remarks='" + values.rejected_remarks.Replace("'", "") + "'," +
                     " approval_status='Initiate Audit Rejected'," +
                     " status='Initiate Audit Rejected'" +
                     " where auditcreation_gid='" + values.auditcreation_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Rejected successfully";
                    return;
                }
            
            else
            {
                values.status = false;
                values.message = "Error Occur";
                return;
            }

        }
        public void DaGetEditAuditeeList(string multipleauditee_gid, multipleauditee values)
        {
            try
            {
                msSQL = "select auditcreation_gid,auditeechecker_name,auditeechecker_gid,auditeemaker_gid,auditeemaker_name from atm_trn_tmultipleauditee where " +
                   " multipleauditee_gid ='" + multipleauditee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmultipleauditee_list = new List<multipleauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmultipleauditee_list.Add(new multipleauditee_list
                        {

                            auditeechecker_name = (dr_datarow["auditeechecker_name"].ToString()),
                            auditeechecker_gid = (dr_datarow["auditeechecker_gid"].ToString()),
                            auditeemaker_gid = (dr_datarow["auditeemaker_gid"].ToString()),
                            auditeemaker_name = (dr_datarow["auditeemaker_name"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),

                        });
                    }
                    values.multipleauditee_list = getmultipleauditee_list;
                }
                dt_datatable.Dispose();
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_manageremployee = new List<employeeauditee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.employeeauditee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employeeauditee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        
    }
}