using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.rsk.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using ems.storage.Functions;

namespace ems.rsk.DataAccess
{
    public class DaObservationReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string lsvisitreport_generateGid;
        string msSQL, msGetGid, msGet_observationGid;
        string lsvertical, message;
        string lszonal_riskmanagergid, lszonal_riskmanagername;
        string lsrelationship_manager, lszonal_name, lscustomer_name, lsvisit_date;
        string lsrisk_manager, lsto_riskmanager, lscc_zonalRM, lsallocationdtl_gid;
        string lsadd_days, lsto_mail, lscreated_date, lscc_mail;
        string ls_server, ls_username, ls_password, lsvertical_gid;
        string lscc_riskmanager, lsto_zonalRM, lstier1_atrdate, lstier1_code;
        string lsassignedRM_name;
        int ls_port;
        int mnResult, MailFlag;
        String[] lsCCReceipients;
        string msGetDocumentGid, lstier1_rejectedremarks;

        public bool DaGetViewObservationReportDtl(string allocationdtl_gid, observationreportdtl values)
        {
            msSQL = "select visitreport_generateGid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + allocationdtl_gid + "'";
            lsvisitreport_generateGid = objdbconn.GetExecuteScalar(msSQL);
            if (lsvisitreport_generateGid != "")
            {
                msSQL = " select a.customer_name,date_format(visit_date, '%d-%m-%Y') as visitdate,disbursement_amount,contact_details1,contact_details2, " +
                    " sanctionedamount_byclient, RM_name,RMD_visitedGid, b.customer_urn, vertical_code,relationship_managergid ," +
                    " RMD_visitedname,a.PPA_name,date_format(disbursement_date, '%d-%m-%Y') as disbursement_date,totalloan_outstanding,risk_code " +
                    " from rsk_trn_tvisitreportgenerate a " +
                    " left join rsk_trn_tallocatecustomerdtl b on a.allocationdtl_gid = b.allocationdtl_gid " +
                    " where visitreport_generateGid='" + lsvisitreport_generateGid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.RMD_visitname = objODBCDatareader["RMD_visitedname"].ToString();
                    values.RMD_visitGid = objODBCDatareader["RMD_visitedGid"].ToString();
                    values.relationship_manager_gid = objODBCDatareader["relationship_managergid"].ToString();
                    values.relationship_manager_name = objODBCDatareader["RM_name"].ToString();
                    values.visit_date = objODBCDatareader["visitdate"].ToString();
                    values.sanction_amount = objODBCDatareader["sanctionedamount_byclient"].ToString();
                    values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.PPA_name = objODBCDatareader["PPA_name"].ToString();
                    values.disbursement_date = objODBCDatareader["disbursement_date"].ToString();
                    values.contact_details1 = objODBCDatareader["contact_details1"].ToString();
                    values.contact_details2 = objODBCDatareader["contact_details2"].ToString();
                    values.totalloan_outstanding = objODBCDatareader["totalloan_outstanding"].ToString();
                    values.risk_code = objODBCDatareader["risk_code"].ToString();
                }
                objODBCDatareader.Close();
            }
            return true;
        }

        public bool DaGetViewObservationdtl(string observation_reportgid, observationdtl values)
        {
            msSQL = " select observation_reportgid,visitreport_generategid,allocationdtl_gid,customer_name,customer_urn,risk_code, " +
                    " date_format(dateof_RMDvisit, '%d-%m-%Y') as dateof_RMDvisit,report_pertainingto, " +
                    " vertical,format(disbursement_amount,2) as disbursement_amount,approving_authority," +
                    " date_format(loansanction_date, '%d-%m-%Y') as loansanction_date ,observation_flag," +
                    " relationship_manager_gid,relationship_manager_name,date_format(relationshipmanager_updateddate,'%d-%m-%Y') as atr_completiondate, " +
                    " PPA_name,RMDvisit_officialname, date_format(loandisbursement_date, '%d-%m-%Y') as loandisbursement_date , " +
                    " people_accompaniedRMD,format(sanction_amount,2) as sanction_amount,format(outstanding_amount,2) as outstanding_amount, " +
                    " current_DPD,contact_details1,contact_details2, " +
                    " concat(b.user_firstname, ' / ', b.user_lastname) as created_by,date_format(a.created_date, '%d-%m-%Y') as created_date " +
                    " from rsk_trn_tobservationreport a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid" +
                    " where observation_reportgid='" + observation_reportgid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.observation_reportgid = objODBCDatareader["observation_reportgid"].ToString();
                values.visitreport_generategid = objODBCDatareader["visitreport_generategid"].ToString();
                values.allocationdtl_gid = objODBCDatareader["allocationdtl_gid"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.dateof_RMDvisit = objODBCDatareader["dateof_RMDvisit"].ToString();
                values.report_pertainingto = objODBCDatareader["report_pertainingto"].ToString();
                values.vertical = objODBCDatareader["vertical"].ToString();
                values.disbursement_amount = objODBCDatareader["disbursement_amount"].ToString();
                values.approving_authority = objODBCDatareader["approving_authority"].ToString();
                values.loansanction_date = objODBCDatareader["loansanction_date"].ToString();
                values.relationship_manager_name = objODBCDatareader["relationship_manager_name"].ToString();
                values.PPA_name = objODBCDatareader["PPA_name"].ToString();
                values.RMDvisit_officialname = objODBCDatareader["RMDvisit_officialname"].ToString();
                values.loandisbursement_date = objODBCDatareader["loandisbursement_date"].ToString();
                values.people_accompaniedRMD = objODBCDatareader["people_accompaniedRMD"].ToString();
                values.sanction_amount = objODBCDatareader["sanction_amount"].ToString();
                values.outstanding_amount = objODBCDatareader["outstanding_amount"].ToString();
                values.current_DPD = objODBCDatareader["current_DPD"].ToString();
                values.contact_details1 = objODBCDatareader["contact_details1"].ToString();
                values.contact_details2 = objODBCDatareader["contact_details2"].ToString();
                values.atr_completiondate = objODBCDatareader["atr_completiondate"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.observation_flag = objODBCDatareader["observation_flag"].ToString();
                values.risk_code = objODBCDatareader["risk_code"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public bool DaGetViewObservationCriticalDtl(string observation_reportgid, criticalobservationlist values)
        {
            msSQL = " select critical_observationgid,criteria, RMD_observations, actionable_recommended, relationship_manager_remarks, remarks_flag, " +
                  " concat(b.user_firstname, ' / ', b.user_lastname) as remarks_updatedby,date_format(a.remarks_updateddate, '%d-%m-%Y') as remarks_updateddate, " +
                  " concat(c.user_firstname, ' / ', c.user_lastname) as created_by" +
                  " from rsk_trn_tcriticalobservation a " +
                  " left join adm_mst_tuser b on a.remarks_updatedby = b.user_gid" +
                  " left join adm_mst_tuser c on a.created_by = c.user_gid" +
                  " where observation_reportgid='" + observation_reportgid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_criticalobservation = new List<criticalobservation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_criticalobservation.Add(new criticalobservation
                    {
                        criteria = dt["criteria"].ToString(),
                        RMD_observations = dt["RMD_observations"].ToString(),
                        actionable_recommended = dt["actionable_recommended"].ToString(),
                        relationship_manager_remarks = dt["relationship_manager_remarks"].ToString(),
                        remarks_flag = dt["remarks_flag"].ToString(),
                        remarks_updatedby = dt["remarks_updatedby"].ToString(),
                        remarks_updateddate = dt["remarks_updateddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        critical_observationgid = dt["critical_observationgid"].ToString(),
                    });
                }
                values.criticalobservation = get_criticalobservation;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetTmpCriticaldtl(string allocationdtl_gid, criticalobservationlist values)
        {
            msSQL = " select tmpcritical_observationgid,criteria, RMD_observations, actionable_recommended, " +
                  " concat(b.user_firstname, ' / ', b.user_lastname) as created_by " +
                  " from rsk_tmp_tcriticalobservation a " +
                  " left join adm_mst_tuser b on a.created_by = b.user_gid" +
                  " where a.allocationdtl_gid='" + allocationdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_criticalobservation = new List<criticalobservation>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_criticalobservation.Add(new criticalobservation
                    {
                        tmpcritical_observationgid = dt["tmpcritical_observationgid"].ToString(),
                        criteria = dt["criteria"].ToString(),
                        RMD_observations = dt["RMD_observations"].ToString(),
                        actionable_recommended = dt["actionable_recommended"].ToString(),
                    });
                }
                values.criticalobservation = get_criticalobservation;
            }
            dt_datatable.Dispose();
            return true;
        }

        public void DaGetDeleteCriticalObser(string tmpcritical_observationgid, result values)
        {
            msSQL = "delete from rsk_tmp_tcriticalobservation where tmpcritical_observationgid='" + tmpcritical_observationgid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Critical Observation Details are deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void GetDeleteTrnCriticalObser(string critical_observationgid, result values)
        {
            msSQL = "delete from rsk_trn_tcriticalobservation where critical_observationgid='" + critical_observationgid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Critical Observation Details are deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostObservationReport(string user_gid, observationdtl values)
        {

            if (values.observation_reportgid != "")
            {
                msSQL = " update rsk_trn_tobservationreport" +
                         " set report_pertainingto='" + values.report_pertainingto.Replace("'", "\\'") + "'," +
                         " approving_authority='" + values.approving_authority + "',";
                try
                {
                    msSQL += " loansanction_date='" + Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd") + "', ";
                }
                catch
                { }
                finally
                {
                    msSQL += " PPA_name='" + values.PPA_name + "'," +
                        " RMDvisit_officialname='" + values.RMDvisit_officialname + "'," +
                        " people_accompaniedRMD='" + values.people_accompaniedRMD + "'," +
                        " current_DPD='" + values.current_DPD + "'," +
                        " contact_details1='" + values.contact_details1 + "'," +
                        " contact_details2='" + values.contact_details2 + "'," +
                        " remindermail_date=date_add(curdate(), interval 4 day)," +
                        " updated_by='" + user_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where observation_reportgid='" + values.observation_reportgid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Observation Report are submitted to Relationship Manager Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }
            else
            {
                msSQL = " select tmpcritical_observationgid from rsk_tmp_tcriticalobservation " +
                   " where allocationdtl_gid='" + values.allocationdtl_gid + "' and created_by='" + user_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Atleast One Critical Observation to Submit..!";
                    
                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = "select visitreport_generateGid from rsk_trn_tvisitreportgenerate where allocationdtl_gid='" + values.allocationdtl_gid + "'";
                    lsvisitreport_generateGid = objdbconn.GetExecuteScalar(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("OBRT");
                    msSQL = " insert into rsk_trn_tobservationreport(" +
                           " observation_reportgid," +
                           " visitreport_generategid," +
                           " allocationdtl_gid, " +
                           " customer_name," +
                           " customer_urn," +
                           " risk_code," +
                           " dateof_RMDvisit," +
                           " report_pertainingto," +
                           " vertical," +
                           " disbursement_amount," +
                           " approving_authority," +
                           " loansanction_date, " +
                           " relationship_manager_gid ," +
                           " relationship_manager_name, " +
                           " PPA_name," +
                           " RMDvisit_officialname," +
                           " loandisbursement_date," +
                           " people_accompaniedRMD," +
                           " sanction_amount," +
                           " outstanding_amount," +
                           " current_DPD," +
                           " contact_details1," +
                           " contact_details2," +
                           " remindermail_date," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + lsvisitreport_generateGid + "'," +
                           "'" + values.allocationdtl_gid + "'," +
                           "'" + values.customer_name.Replace("'", "\\'") + "'," +
                           "'" + values.customer_urn + "'," +
                           "'" + values.risk_code + "'," +
                           "'" + values.dateof_RMDvisit + "'," +
                           "'" + values.report_pertainingto.Replace("'", "\\'") + "'," +
                           "'" + values.vertical + "'," +
                           "'" + values.disbursement_amount.Replace(",", "") + "'," +
                           "'" + values.approving_authority + "'," +
                           "'" + Convert.ToDateTime(values.loansanction_date).ToString("yyyy-MM-dd") + "'," +
                           "'" + values.relationship_manager_gid + "'," +
                           "'" + values.relationship_manager_name + "'," +
                           "'" + values.PPA_name + "'," +
                           "'" + values.RMDvisit_officialname + "'," +
                           "'" + (values.loandisbursement_date) + "'," +
                           "'" + values.people_accompaniedRMD + "'," +
                           "'" + values.sanction_amount.Replace(",", "") + "'," +
                           "'" + values.outstanding_amount.Replace(",", "") + "'," +
                           "'" + values.current_DPD + "'," +
                           "'" + values.contact_details1 + "'," +
                           "'" + values.contact_details2 + "'," +
                           "date_add(curdate(),interval 4 day)," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = " select tmpcritical_observationgid,allocationdtl_gid,criteria,RMD_observations,actionable_recommended " +
                                " from rsk_tmp_tcriticalobservation where allocationdtl_gid='" + values.allocationdtl_gid + "' and created_by='" + user_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                msGet_observationGid = objcmnfunctions.GetMasterGID("CROB");
                                msSQL = " insert into rsk_trn_tcriticalobservation(" +
                                      " critical_observationgid," +
                                      " observation_reportgid," +
                                      " criteria, " +
                                      " RMD_observations," +
                                      " actionable_recommended," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                      "'" + msGet_observationGid + "'," +
                                      "'" + msGetGid + "'," +
                                      "'" + dt["criteria"].ToString().Replace("'", "\\'") + "'," +
                                      "'" + dt["RMD_observations"].ToString().Replace("'", "\\'") + "'," +
                                      "'" + dt["actionable_recommended"].ToString().Replace("'", "\\'") + "'," +
                                      "'" + user_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    msSQL = "delete from rsk_tmp_tcriticalobservation where tmpcritical_observationgid='" + dt["tmpcritical_observationgid"].ToString() + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                        }
                        dt_datatable.Dispose();
                    }
                    if (mnResult == 1)
                    {
                        msSQL = " select d.zonal_name,b.employee_emailid as to_mail,e.employee_emailid as cc_mail, " +
                                " date_add(curdate(),interval 3 day) as realert_date, " +
                                " relationship_manager_name,RMDvisit_officialname,zonal_name, " +
                                " concat(a.customer_name, '/', a.customer_urn) as customername, " +
                                " date_format(dateof_RMDvisit, '%d-%m-%Y') as visit_date from rsk_trn_tobservationreport a " +
                                " left join hrm_mst_temployee b on a.relationship_manager_gid = b.employee_gid " +
                                " left join rsk_trn_tallocationdtl c on a.allocationdtl_gid = c.allocationdtl_gid " +
                                " left join rsk_mst_tzonalmapping d on c.zonal_gid = d.zonalmapping_gid " +
                                " left join hrm_mst_temployee e on e.employee_gid=c.allocation_zonalRM " +
                                " where a.observation_reportgid='" + msGetGid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lszonal_name = objODBCDatareader["zonal_name"].ToString();
                            lsrelationship_manager = objODBCDatareader["relationship_manager_name"].ToString();
                            lscustomer_name = objODBCDatareader["customername"].ToString();
                            lsvisit_date = objODBCDatareader["visit_date"].ToString();
                            lsrisk_manager = objODBCDatareader["RMDvisit_officialname"].ToString();
                            lsadd_days = objODBCDatareader["realert_date"].ToString();
                            lsto_mail = objODBCDatareader["to_mail"].ToString();
                            lscc_mail = objODBCDatareader["cc_mail"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_emailid from hrm_mst_temployee where user_gid='" + user_gid + "'";
                        string lscc_riskmanager = objdbconn.GetExecuteScalar(msSQL);

                        lscc_mail = lscc_riskmanager + "," + lscc_mail;

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


                        message = " Date : <b>" + DateTime.Now.ToString("dd-MM-yyyy") + "</b> <br />";
                        message = message + "<br />";
                        message = message + "ZONE : <b>" + lszonal_name + "</b> <br />";
                        message = message + "<br />";
                        message = message + " Dear " + lsrelationship_manager + ",  <br />";
                        message = message + "<br />";
                        message = message + "This is to kindly inform you that the RMD has completed the visit for <b>" + lscustomer_name + "</b> on <b>" + lsvisit_date + "</b>.   <br />";
                        message = message + "<br />";
                        message = message + "The Risk Manager for this case is <b>" + lsrisk_manager + "</b>. We would request you to kindly login to RSK (Tracker) which is available on  <a href='https://" + ConfigurationManager.AppSettings["companylinkname"] + ".samunnati.com'>" + ConfigurationManager.AppSettings["companylinkname"] + ".samunnati.com </a> . You would need to login using your NT ID and Password.<br />";
                        message = message + "<br />";
                        message = message + "We would expect your response on the case on or before <b>" + Convert.ToDateTime(lsadd_days).ToString("dd-MM-yyyy") + "</b>. <br />";
                        message = message + "<br />";
                        message = message + "The report presented to you is called the ACTION TAKEN REPORT, which lists out key actionables / Observations / corrective measures. You are requested to provide your inputs.<br/>";
                        message = message + "<br />";
                        message = message + "<b>Action & Legends :</b>";
                        message = message + "<br />";
                        message = message + "<b>(a)	SUBMIT / APPROVE - </b> This accepts and authorizes the findings.";
                        message = message + "<br />";
                        message = message + "<b>(b) COMMENTS & SUBMIT - </b> You can provide reverts against each point which may / may not agree with the RMD Findings. Yet it will tell us your perspective. ";
                        message = message + "<br />";
                        message = message + "<b>(c)	RESCHEDULE - </b> If you click on this, the case discussion will get deferred to the next month. However, the number of cases so deferred will be provided to the CBO and Zonal Head for their suitable action and review.<br/> ";
                        message = message + "<br />";
                        message = message + "This is an auto-generated mail. Hence request you to kindly not respond on this. You would be expected to interact with your respective Risk Manager, indicated above.<br/>";
                        message = message + "<br />";
                        message = message + "Request your utmost cooperation and support in closing this case<br />";
                        message = message + "<br />";
                        message = message + "<b>Thanks & Regards, </b> ";
                        message = message + "<br />";
                        message = message + "<b> " + lsrisk_manager + " </b> ";
                        message = message + "<br />";
                        MailFlag = SendSMTP2(ls_username, ls_password, lsto_mail, "ATR CONFIRMATION MAIL", message, lscc_mail, "", "");
                        if (MailFlag == 1)
                        {
                            msSQL = " insert into rsk_trn_talertmail (" +
                                    " observation_reportgid, " +
                                    " from_mail, " +
                                    " to_mail," +
                                    " cc_mail," +
                                    " mail_content," +
                                    " mail_status," +
                                    " created_by," +
                                    " created_date)" +
                                      " values(" +
                                      "'" + msGetGid + "'," +
                                      "'" + ls_username + "'," +
                                      "'" + lsto_mail + "'," +
                                      "'" + lscc_mail + "'," +
                                      "'" + message.Replace("'","") + "'," +
                                      "'ATR Confirmation Mail'," +
                                      "'" + user_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Observation Report are submitted to Relationship Manager Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }
                }

                
            }
           
        }

        public bool DaPostObservationCritical(string user_gid, criticalobservation values)
        {

            if (values.observation_reportgid != "")
            {
                msGet_observationGid = objcmnfunctions.GetMasterGID("CROB");
                msSQL = " insert into rsk_trn_tcriticalobservation(" +
                      " critical_observationgid," +
                      " observation_reportgid," +
                      " criteria, " +
                      " RMD_observations," +
                      " actionable_recommended," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGet_observationGid + "'," +
                      "'" + values.observation_reportgid + "'," +
                      "'" + values.criteria.Replace("'", "\\'") + "'," +
                      "'" + values.RMD_observations.Replace("'", "\\'") + "'," +
                      "'" + values.actionable_recommended.Replace("'", "\\'") + "'," +
                      "'" + user_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("TMCO");
                msSQL = " insert into rsk_tmp_tcriticalobservation(" +
                      " tmpcritical_observationgid," +
                      " allocationdtl_gid," +
                      " criteria, " +
                      " RMD_observations," +
                      " actionable_recommended," +
                      " created_by," +
                      " created_date)" +
                      " values(" +
                      "'" + msGetGid + "'," +
                      "'" + values.allocationdtl_gid + "'," +
                      "'" + values.criteria.Replace("'", "\\'") + "'," +
                      "'" + values.RMD_observations.Replace("'", "\\'") + "'," +
                      "'" + values.actionable_recommended.Replace("'", "\\'") + "'," +
                      "'" + user_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Critical Observation Details are Added Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostObservationCriticalRemarks(string user_gid, criticalobservationdtl values)
        {

            msSQL = " update rsk_trn_tcriticalobservation set relationship_manager_remarks='" + values.relationshipmanager_remarks.Replace("'", "\\'") + "'," +
               " remarks_flag='Y'," +
               " remarks_updatedby='" + user_gid + "'," +
               " remarks_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where critical_observationgid='" + values.critical_observationgid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Critical Observation Remarks are Updated Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaPostObservationRemarksSubmit(string user_gid, criticalobservationdtl values)
        {
            msSQL = " select count(*) from rsk_trn_tcriticalobservation where remarks_flag='Y' " +
                    " and observation_reportgid='" + values.observation_reportgid + "'";
            string lsremarks_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from rsk_trn_tcriticalobservation " +
                    " where observation_reportgid='" + values.observation_reportgid + "'";
            string lsoverall_count = objdbconn.GetExecuteScalar(msSQL);

            if (lsremarks_count == lsoverall_count)
            {
                msSQL = " update rsk_trn_tobservationreport set " +
               " observation_flag='Y'," +
               " relationshipmanager_updatedby='" + user_gid + "'," +
               " relationshipmanager_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where observation_reportgid='" + values.observation_reportgid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select d.zonal_name,b.employee_emailid as to_riskmanagermail, " +
                       " e.employee_emailid as cc_ZRMmail,relationship_manager_name,RMDvisit_officialname, " +
                       " concat(a.customer_name, '/', a.customer_urn) as customername, " +
                       " date_format(dateof_RMDvisit, '%d-%m-%Y') as visit_date,date_format(a.created_date, '%d-%m-%Y') as created_date, c.allocationdtl_gid " +
                       " from rsk_trn_tobservationreport a " +
                       " left join rsk_trn_tallocationdtl c on a.allocationdtl_gid = c.allocationdtl_gid " +
                       " left join rsk_mst_tzonalmapping d on c.zonal_gid = d.zonalmapping_gid " +
                       " left join hrm_mst_temployee b on c.allocation_assignedRM = b.employee_gid " +
                       " left join hrm_mst_temployee e on c.allocation_zonalRM = e.employee_gid " +
                       " where a.observation_reportgid='" + values.observation_reportgid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lszonal_name = objODBCDatareader["zonal_name"].ToString();
                    lsrelationship_manager = objODBCDatareader["relationship_manager_name"].ToString();
                    lscustomer_name = objODBCDatareader["customername"].ToString();
                    lsvisit_date = objODBCDatareader["visit_date"].ToString();
                    lsrisk_manager = objODBCDatareader["RMDvisit_officialname"].ToString();
                    lsto_riskmanager = objODBCDatareader["to_riskmanagermail"].ToString();
                    lscc_zonalRM = objODBCDatareader["cc_ZRMmail"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                    lsallocationdtl_gid = objODBCDatareader["allocationdtl_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update rsk_trn_tallocationdtl set ATR_flag='Y' where allocationdtl_gid = '" + lsallocationdtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

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


                message = " <b> Date : </b> " + DateTime.Now.ToString("dd-MM-yyyy") + " <br />";
                message = message + "<br />";
                message = message + " <b> ZONE :</b>" + lszonal_name + " <br />";
                message = message + "<br />";
                message = message + "<b> REF </b> :	1. ATR Confirmation mail sent on " + lscreated_date + " <br />";
                message = message + "<br />";
                message = message + " Dear " + lsrisk_manager + ",  <br />";
                message = message + "<br />";
                message = message + "This is to kindly inform you that <b>" + lsrelationship_manager + "</b> has completed the ATR submission for <b>" + lscustomer_name + "</b> which was visited by RMD on <b>" + lsvisit_date + "</b>.   <br />";
                message = message + "<br />";
                message = message + "Request you to kindly proceed to present this case in TIER 1 Committee.  <br />";
                message = message + "<br />";
                message = message + "<br />";
                message = message + "<b>Thanks & Regards, </b> ";
                message = message + "<br />";
                message = message + "<b> RMD TEAM – (Auto Generated) </b> ";
                message = message + "<br />";
                MailFlag = SendSMTP2(ls_username, ls_password, lsto_riskmanager, "ATR COMPLETION MAIL", message, lscc_zonalRM, "", "");
                if (MailFlag == 1)
                {
                    msSQL = " insert into rsk_trn_talertmail (" +
                            " observation_reportgid, " +
                            " from_mail, " +
                            " to_mail," +
                            " cc_mail," +
                            " mail_content," +
                            " mail_status," +
                            " created_by," +
                            " created_date)" +
                              " values(" +
                              "'" + values.observation_reportgid + "'," +
                              "'" + ls_username + "'," +
                              "'" + lsto_riskmanager + "'," +
                              "'" + lscc_zonalRM + "'," +
                              "'" + message + "'," +
                              "'ATR Completion Mail'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Observation Report are Submitted to Risk Manager Successfully..!";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                    return false;
                }
            }
            else
            {
                values.status = false;
                values.message = "Kindly Update the Remarks..!";
            }
            return true;
        }

        public bool DaGetObservationReportSummary(string employee_gid, observationreportlist values)
        {
            msSQL = " select observation_reportgid,customer_name,customer_urn,vertical,observation_flag," +
                    " date_format(a.dateof_RMDvisit, '%d-%m-%Y') as dateof_RMDvisit,RMDvisit_officialname, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by " +
                    " from rsk_trn_tobservationreport a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where relationship_manager_gid = '" + employee_gid + "' order by observation_flag asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_criticalobservation = new List<observationreport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_criticalobservation.Add(new observationreport
                    {
                        observation_reportgid = dt["observation_reportgid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical = dt["vertical"].ToString(),
                        dateof_RMDvisit = dt["dateof_RMDvisit"].ToString(),
                        RMDvisit_officialname = dt["RMDvisit_officialname"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        observation_flag = dt["observation_flag"].ToString(),
                    });
                }
                values.observationreport = get_criticalobservation;
            }
            dt_datatable.Dispose();

            msSQL = " select a.count_pending , b.count_approved from " +
                    " (select count(*) as count_pending from rsk_trn_tobservationreport where " +
                    " observation_flag = 'N' and relationship_manager_gid = '" + employee_gid + "') as a, " +
                    " (select count(*) as count_approved from rsk_trn_tobservationreport where " +
                    " observation_flag = 'Y' and relationship_manager_gid = '" + employee_gid + "')  as b";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.count_pending = objODBCDatareader["count_pending"].ToString();
                values.count_approved = objODBCDatareader["count_approved"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }

        public void DaPostTier1Format(string user_gid, tier1format values)
        {
            if (values.tier1code_changereason == null || values.tier1code_changereason == "")
            {
                values.tier1code_changereason = "";
            }

            msSQL = " select b.zonalrisk_managerGid,b.zonalrisk_managername,c.vertical_code,c.vertical_gid from rsk_trn_tallocationdtl a " +
                    " left join rsk_mst_tzonalmapping b on a.zonal_gid=b.zonalmapping_gid " +
                    " left join rsk_trn_tallocatecustomerdtl c on a.allocationdtl_gid = c.allocationdtl_gid " +
                    " where a.allocationdtl_gid='" + values.allocationdtl_gid + "' group by a.allocationdtl_gid ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lszonal_riskmanagergid = objODBCDatareader["zonalrisk_managerGid"].ToString();
                lszonal_riskmanagername = objODBCDatareader["zonalrisk_managername"].ToString();
                lsvertical = objODBCDatareader["vertical_code"].ToString();
                lsvertical_gid = objODBCDatareader["vertical_gid"].ToString();
            }
            objODBCDatareader.Close();


            msGetGid = objcmnfunctions.GetMasterGID("TIFO");
            msSQL = " insert into rsk_trn_ttier1format(" +
                  " tier1format_gid," +
                  " observation_reportgid, " +
                  " allocationdtl_gid," +
                  " customer_name," +
                  " customer_urn, " +
                  " vertical_gid, " +
                  " vertical, " +
                  " tier1_observations, " +
                  " tier1_code," +
                  " tier1code_changereason, " +
                  " tier1code_changeflag," +
                  " tier1_justification," +
                  " tier1_processgap, " +
                  " tier1_processrecommendation, " +
                  " tier1_managementcomments," +
                  " tier1_reverts_actionplan, " +
                  " tier1_atrdate," +
                  " zonal_riskmanagergid, " +
                  " zonal_riskmanagername, " +
                  " tier1_approvalstatus," +
                  " tier1_approvalflag, " +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.observation_reportgid + "', " +
                  "'" + values.allocationdtl_gid + "', " +
                  "'" + values.customer_name + "'," +
                  "'" + values.customer_urn + "'," +
                  "'" + lsvertical_gid + "'," +
                  "'" + lsvertical + "'," +
                  "'" + values.tier1_observations.Replace("'", "\\'") + "'," +
                  "'" + values.tier1_code + "'," +
                  "'" + values.tier1code_changereason.Replace("'", "\\'") + "'," +
                  "'" + values.tier1code_changeflag + "'," +
                  "'" + values.tier1_justification.Replace("'", "\\'") + "'," +
                  "'" + values.tier1_processgap.Replace("'", "\\'") + "'," +
                  "'" + values.tier1_processrecommendation.Replace("'", "\\'") + "'," +
                  "'" + values.tier1_managementcomments.Replace("'", "\\'") + "'," +
                  "'" + values.tier1_reverts_actionplan.Replace("'", "\\'") + "'," +
                  "'" + values.tier1_atrdate + "'," +
                  "'" + lszonal_riskmanagergid + "'," +
                  "'" + lszonal_riskmanagername + "'," +
                  "'Pending'," +
                  "'Y'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.tier1code_changereason == "")
            {

            }
            else
            {
                msSQL = " insert into rsk_trn_tcodehistory( " +
                 " allocationdtl_gid ," +
                 " tier_code ," +
                 " tiercode_changereason ," +
                 " codechange_stage ," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + values.allocationdtl_gid + "', " +
                 "'" + values.tier1_code + "'," +
                 "'" + values.tier1code_changereason.Replace("'", "\\'") + "', " +
                 "'Tier 1', " +
                 "'" + user_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select d.zonal_name,b.employee_emailid as cc_riskmanagermail,date_format(a.tier1_atrdate, '%d-%m-%Y %h:%i %p') as tier1_atrdate, " +
                     " e.employee_emailid as to_ZRMmail,a.vertical,c.assignedRM_name, " +
                     " concat(a.customer_name, '/', a.customer_urn) as customername,a.tier1_code, " +
                     " date_format(a.created_date, '%d-%m-%Y') as created_date " +
                     " from rsk_trn_ttier1format a " +
                     " left join rsk_trn_tallocationdtl c on a.allocationdtl_gid = c.allocationdtl_gid " +
                     " left join rsk_mst_tzonalmapping d on c.zonal_gid = d.zonalmapping_gid " +
                     " left join hrm_mst_temployee b on c.allocation_assignedRM = b.employee_gid " +
                     " left join hrm_mst_temployee e on a.zonal_riskmanagergid = e.employee_gid " +
                     " where a.tier1format_gid='" + msGetGid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lszonal_name = objODBCDatareader["zonal_name"].ToString();
                lscustomer_name = objODBCDatareader["customername"].ToString();
                lscc_riskmanager = objODBCDatareader["cc_riskmanagermail"].ToString();
                lsto_zonalRM = objODBCDatareader["to_ZRMmail"].ToString();
                lscreated_date = objODBCDatareader["created_date"].ToString();
                lstier1_atrdate = objODBCDatareader["tier1_atrdate"].ToString();
                lstier1_code = objODBCDatareader["tier1_code"].ToString();
                lsassignedRM_name = objODBCDatareader["assignedRM_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select tmptier1document_gid,document_title,document_name,document_path from rsk_tmp_ttier1document where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("TI1D");

                    msSQL = " Insert into rsk_trn_ttier1document( " +
                              " tier1document_gid," +
                              " tier1format_gid," +
                              " document_title," +
                              " document_name," +
                              " document_path," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + msGetGid + "'," +
                              "'" + dt["document_title"].ToString() + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msSQL = "delete from rsk_tmp_ttier1document where tmptier1document_gid ='" + dt["tmptier1document_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }

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


            message = " <b> Date : </b> " + DateTime.Now.ToString("dd-MM-yyyy") + " <br />";
            message = message + "<br />";
            message = message + " <b> ZONE :</b>" + lszonal_name + " <br />";
            message = message + "<br />";
            message = message + " Dear " + lszonal_riskmanagername + ",  <br />";
            message = message + "<br />";
            message = message + "This is to kindly inform you that <b>" + lsrelationship_manager + "</b> has Initiated the Tier 1 Approval for <b>" + lscustomer_name + "</b> which was ATR Completed on <b>" + lstier1_atrdate + "</b>.   <br />";
            message = message + "<br />";
            message = message + "<b>Risk Code : </b> " + lstier1_code + "<br />";
            message = message + "<br />";
            message = message + "<b>Risk Manager : </b> " + lsassignedRM_name + "<br />";
            message = message + "<br />";
            message = message + "Request you to kindly Approve the same.  <br />";
            message = message + "<br />";
            message = message + "<br />";
            message = message + "<b>Thanks & Regards, </b> ";
            message = message + "<br />";
            message = message + "<b> RMD TEAM – (Auto Generated) </b> ";
            message = message + "<br />";
            MailFlag = SendSMTP2(ls_username, ls_password, lsto_zonalRM, "Tier 1 - Approval Initiation Mail ", message, lscc_riskmanager, "", "");
            if (MailFlag == 1)
            {
                msSQL = " insert into rsk_trn_talertmail (" +
                        " tier1format_gid, " +
                        " from_mail, " +
                        " to_mail," +
                        " cc_mail," +
                        " mail_content," +
                        " mail_status," +
                        " created_by," +
                        " created_date)" +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + ls_username + "'," +
                          "'" + lsto_riskmanager + "'," +
                          "'" + lscc_zonalRM + "'," +
                          "'" + message + "'," +
                          "'Tier 1 - Approval Initaition Mail'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 1 Fomat Submitted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public bool DaPostUpdateTier1Format(string user_gid, tier1format values)
        {
            msSQL = "select allocationdtl_gid from rsk_trn_ttier1format where tier1format_gid='" + values.tier1format_gid + "'";
            string lsallocation_gid = objdbconn.GetExecuteScalar(msSQL);
           
            if (values.tier1_rejectedremarks == "" || values.tier1_rejectedremarks == null)
            {
                lstier1_rejectedremarks = "";
            }
            else
            {
                lstier1_rejectedremarks = values.tier1_rejectedremarks;
            }
            msGetGid = objcmnfunctions.GetMasterGID("TIRR");

            msSQL = " insert into rsk_trn_ttier1RMRejectlog(" +
                  " tier1rmreject_loggid," +
                  " tier1format_gid, " +
                  " reject_remarks," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.tier1format_gid + "', " +
                  "'" + lstier1_rejectedremarks.Replace("'", "\\'") + "'," +
                  "'" + user_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("TIHF");
            msSQL = " insert into rsk_trn_ttier1history(historytier1_gid,tier1format_gid,tier1_observations, " +
                    " tier1_code,tier1_justification,tier1_processgap,tier1_processrecommendation, " +
                    " tier1_managementcomments,tier1_reverts_actionplan,tier1_atrdate,created_by,created_date) " +
                    " (select '" + msGetGid + "',tier1format_gid, tier1_observations, " +
                    " tier1_code, tier1_justification, tier1_processgap,tier1_processrecommendation, " +
                    " tier1_managementcomments, tier1_reverts_actionplan,tier1_atrdate,'" + user_gid + "',curdate() " +
                    " from rsk_trn_ttier1format where tier1format_gid= '" + values.tier1format_gid + "') ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.tier1code_changereason == "" || values.tier1code_changereason == null)
            {
                values.tier1code_changereason = "";
            }
            if (values.tier1code_changereason == "")
            {
                msSQL = " update rsk_trn_ttier1format set " +
         " tier1_observations='" + values.tier1_observations.Replace("'", "\\'") + "'," +
         " tier1_code='" + values.tier1_code.Replace("'", "\\'") + "'," +
         " tier1_justification='" + values.tier1_justification.Replace("'", "\\'") + "'," +
         " tier1_processgap='" + values.tier1_processgap.Replace("'", "\\'") + "'," +
         " tier1_processrecommendation='" + values.tier1_processrecommendation.Replace("'", "\\'") + "'," +
         " tier1_managementcomments='" + values.tier1_managementcomments.Replace("'", "\\'") + "'," +
         " tier1_reverts_actionplan='" + values.tier1_reverts_actionplan.Replace("'", "\\'") + "'," +
         " tier1_atrdate='" + values.tier1_atrdate + "'," +
         " tier1_rejectedremarks ='" + lstier1_rejectedremarks + "'," +
         " updated_by='" + user_gid + "'," +
         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
         " where tier1format_gid='" + values.tier1format_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = " update rsk_trn_ttier1format set " +
        " tier1_observations='" + values.tier1_observations.Replace("'", "\\'") + "'," +
        " tier1_code='" + values.tier1_code.Replace("'", "\\'") + "'," +
        " tier1code_changereason ='" + values.tier1code_changereason.Replace("'", "\\'") + "'," +
        " tier1code_changeflag='Y'," +
        " tier1_justification='" + values.tier1_justification.Replace("'", "\\'") + "'," +
        " tier1_processgap='" + values.tier1_processgap.Replace("'", "\\'") + "'," +
        " tier1_processrecommendation='" + values.tier1_processrecommendation.Replace("'", "\\'") + "'," +
        " tier1_managementcomments='" + values.tier1_managementcomments.Replace("'", "\\'") + "'," +
        " tier1_reverts_actionplan='" + values.tier1_reverts_actionplan.Replace("'", "\\'") + "'," +
        " tier1_atrdate='" + values.tier1_atrdate + "'," +
        " tier1_rejectedremarks ='" + lstier1_rejectedremarks + "'," +
        " updated_by='" + user_gid + "'," +
        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        " where tier1format_gid='" + values.tier1format_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " insert into rsk_trn_tcodehistory( " +
              " allocationdtl_gid ," +
              " tier_code ," +
              " tiercode_changereason ," +
              " codechange_stage ," +
              " created_by," +
              " created_date)" +
              " values(" +
              "'" + lsallocation_gid + "', " +
              "'" + values.tier1_code + "'," +
              "'" + values.tier1code_changereason.Replace("'", "\\'") + "', " +
              "'Tier 1', " +
              "'" + user_gid + "'," +
              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }


            msSQL = " update rsk_trn_ttier1format set tier1_approvalstatus='Pending', tier1_approvalflag='Y' " +
                     " where tier1format_gid='" + values.tier1format_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select d.zonal_name,b.employee_emailid as cc_riskmanagermail,date_format(a.tier1_atrdate, '%d-%m-%Y %h:%i %p') as tier1_atrdate, " +
                     " e.employee_emailid as to_ZRMmail,a.vertical,c.assignedRM_name,a.zonal_riskmanagername, " +
                     " concat(a.customer_name, '/', a.customer_urn) as customername,a.tier1_code, " +
                     " date_format(a.created_date, '%d-%m-%Y') as created_date " +
                     " from rsk_trn_ttier1format a " +
                     " left join rsk_trn_tallocationdtl c on a.allocationdtl_gid = c.allocationdtl_gid " +
                     " left join rsk_mst_tzonalmapping d on c.zonal_gid = d.zonalmapping_gid " +
                     " left join hrm_mst_temployee b on c.allocation_assignedRM = b.employee_gid " +
                     " left join hrm_mst_temployee e on a.zonal_riskmanagergid = e.employee_gid " +
                     " where a.tier1format_gid='" + values.tier1format_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lszonal_name = objODBCDatareader["zonal_name"].ToString();
                lscustomer_name = objODBCDatareader["customername"].ToString();
                lscc_riskmanager = objODBCDatareader["cc_riskmanagermail"].ToString();
                lsto_zonalRM = objODBCDatareader["to_ZRMmail"].ToString();
                lscreated_date = objODBCDatareader["created_date"].ToString();
                lstier1_atrdate = objODBCDatareader["tier1_atrdate"].ToString();
                lstier1_code = objODBCDatareader["tier1_code"].ToString();
                lsassignedRM_name = objODBCDatareader["assignedRM_name"].ToString();
                lszonal_riskmanagername = objODBCDatareader["zonal_riskmanagername"].ToString();
            }
            objODBCDatareader.Close();


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


            message = " <b> Date : </b> " + DateTime.Now.ToString("dd-MM-yyyy") + " <br />";
            message = message + "<br />";
            message = message + " <b> ZONE :</b>" + lszonal_name + " <br />";
            message = message + "<br />";
            message = message + " Dear " + lszonal_riskmanagername + ",  <br />";
            message = message + "<br />";
            message = message + "This is to kindly inform you that <b>" + lsrelationship_manager + "</b> has Initiated the Tier 1 Approval for <b>" + lscustomer_name + "</b> which was ATR Completed on <b>" + lstier1_atrdate + "</b>.   <br />";
            message = message + "<br />";
            message = message + "<b>Risk Code : </b> " + lstier1_code + "<br />";
            message = message + "<br />";
            message = message + "<b>Risk Manager : </b> " + lsassignedRM_name + "<br />";
            message = message + "<br />";
            message = message + "Request you to kindly Approve the same.  <br />";
            message = message + "<br />";
            message = message + "<br />";
            message = message + "<b>Thanks & Regards, </b> ";
            message = message + "<br />";
            message = message + "<b> RMD TEAM – (Auto Generated) </b> ";
            message = message + "<br />";
            MailFlag = SendSMTP2(ls_username, ls_password, lsto_zonalRM, "Tier 1 - Approval Re-Initiation Mail ", message, lscc_riskmanager, "", "");
            if (MailFlag == 1)
            {
                msSQL = " insert into rsk_trn_talertmail (" +
                        " tier1format_gid, " +
                        " from_mail, " +
                        " to_mail," +
                        " cc_mail," +
                        " mail_content," +
                        " mail_status," +
                        " created_by," +
                        " created_date)" +
                          " values(" +
                          "'" + values.tier1format_gid + "'," +
                          "'" + ls_username + "'," +
                          "'" + lsto_riskmanager + "'," +
                          "'" + lscc_zonalRM + "'," +
                          "'" + message + "'," +
                          "'Tier 1 - Approval Re-Initaition Mail'," +
                          "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Tier 1 Fomat Submitted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return true;
        }

        public bool DaGetTier1FormatDtl(string observation_reportgid, tier1format values)
        {

            msSQL = " select tier1format_gid,observation_reportgid,customer_name,customer_urn,tier1_observations, " +
                   " tier1_code,tier1_justification,tier1_managementcomments,tier1_approvalstatus, " +
                   " tier1_processgap,tier1_processrecommendation,tier1_reverts_actionplan,tier1code_changeflag, " +
                   " date_format(tier1_atrdate, '%d-%m-%Y') as tier1_atrdate,tier1code_changereason, " +
                   " date_format(created_date, '%d-%m-%Y') as created_date " +
                   " from rsk_trn_ttier1format where observation_reportgid = '" + observation_reportgid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.tier1format_gid = objODBCDatareader["tier1format_gid"].ToString();
                values.observation_reportgid = objODBCDatareader["observation_reportgid"].ToString();
                values.tier1_observations = objODBCDatareader["tier1_observations"].ToString();
                values.tier1_code = objODBCDatareader["tier1_code"].ToString();
                values.tier1_justification = objODBCDatareader["tier1_justification"].ToString();
                values.tier1_processgap = objODBCDatareader["tier1_processgap"].ToString();
                values.tier1_processrecommendation = objODBCDatareader["tier1_processrecommendation"].ToString();
                values.tier1_reverts_actionplan = objODBCDatareader["tier1_reverts_actionplan"].ToString();
                values.tier1_atrdate = objODBCDatareader["tier1_atrdate"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.tier1_managementcomments = objODBCDatareader["tier1_managementcomments"].ToString();
                values.tier1_approvalstatus = objODBCDatareader["tier1_approvalstatus"].ToString();
                values.tier1code_changereason = objODBCDatareader["tier1code_changereason"].ToString();
                values.tier1code_changeflag = objODBCDatareader["tier1code_changeflag"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select approval_status,approval_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as created_by, " +
                    " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_ttier1approvallog a " +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                    " where tier1format_gid='" + values.tier1format_gid + "' order by tier1approval_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1approvallog = new List<tier1approvallog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1approvallog.Add(new tier1approvallog
                    {
                        approval_status = dt["approval_status"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier1approvallog = get_tier1approvallog;
            }
            dt_datatable.Dispose();

            msSQL = " select reject_remarks,concat(b.user_firstname, ' ' ,b.user_lastname,' / ',b.user_code) as created_by, " +
                  " date_format(a.created_date, '%d-%m-%Y') as created_date from rsk_trn_ttier1RMRejectlog a " +
                  " left join adm_mst_tuser b on a.created_by=b.user_gid " +
                  " where tier1format_gid='" + values.tier1format_gid + "' order by tier1rmreject_loggid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1rejectlog = new List<tier1rejectlog>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1rejectlog.Add(new tier1rejectlog
                    {
                        reject_remarks = dt["reject_remarks"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                }
                values.tier1rejectlog = get_tier1rejectlog;
            }
            dt_datatable.Dispose();

            msSQL = " select tier1document_gid,document_title,document_name,document_path,date_format(a.created_date, '%d-%m-%Y') as created_date, " +
                   " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by from rsk_trn_ttier1document a " +
                   " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                   " where tier1format_gid='" + values.tier1format_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_tier1document = new List<tier1doc>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_tier1document.Add(new tier1doc
                    {
                        tier1document_gid = dt["tier1document_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                }
                values.tier1doc = get_tier1document;
            }
            dt_datatable.Dispose();

            return true;
        }

        public bool DaGetTmpTier1DocumentClear(string user_gid)
        {

            msSQL = "delete from rsk_tmp_ttier1document where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

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