using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.ecms.Models;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// penalityAlert Controller Class containing API methods for accessing the  DataAccess class DaPenalityAlert
    ///     penalitydetails - view, update,  send alert mails, show details based on customer, Alert with start and stop in system and mail.
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaPenalityAlert
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
      
        DataTable dt_datatable;
        string msSQL, msGetGid;
        string deferral_gid = string.Empty;
        int mnResult;
        string lsdepartment_mailid,lsccdep_mailid;
        int mailflag;
        string lspop_mail, lspop_password;
        string lsalert_totalcount, lspenality_deferralcount;
        string message, lscustomername, lscustomer_urn;
        string lsextend_flag, lsextended_date;
        DateTime lsmail_sentdate, lsapproval_date;
        string lscustomer_gid, lsrmmail_id, lscc_mail, lszonalmail_id, lsbusinessheadmail_id, lsclustermanagermail_id, lscreditmanagermail_id;
        string strRes = string.Empty;
        string[] cc;

        // penalityManagement_da
        public void DaPenalityMgmt(MdlCustomerAlert objCutomerAlert)
        {

            try
            {
                msSQL = " select a.customeralert_gid,a.customer_gid,a.mail_status,b.customer_code,b.customername,a.penality_flag, " +
                                  " date_format(a.created_date, '%d-%m-%Y %h:%i') as mailsent_date, " +
                                  " case when b.zonal_head = '' then 'NA' else b.zonal_name end as zonal_head,b.vertical_code," +
                                  " case when b.business_head = '' then 'NA' else b.businesshead_name end as business_head," +
                                  " case when b.cluster_manager_gid = '' then 'NA' else b.cluster_manager_name end as cluster_manager," +
                                  " case when b.relationship_manager = '' then 'NA' else b.relationshipmgmt_name end as relationship_manager ," +
                                  " case when b.creditmanager_gid = '' then 'NA' else b.creditmgmt_name end as creditmgmt_name" +
                                  " from ocs_trn_tcustomeralertgenerate a" +
                                  " left join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid" +
                                  " where a.alert_flag='Y' or (a.mail_status='Sent' or a.mail_status='Generated') and " +
                                  " (a.penality_alertdate like '%" + DateTime.Now.ToString("yyyy-MM-dd") + "%' or a.penality_flag='Y' or a.penality_flag='N') " +
                                  " order by a.customer_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail = new List<customermail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail.Add(new customermail_list
                        {
                            customeralert_gid = (dr_datarow["customeralert_gid"].ToString()),
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customercode = (dr_datarow["customer_code"].ToString()),
                            customername = (dr_datarow["customername"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            zonalGid = (dr_datarow["zonal_head"].ToString()),
                            businessHeadGid = (dr_datarow["business_head"].ToString()),
                            relationshipMgmtGid = (dr_datarow["relationship_manager"].ToString()),
                            creditmanagerName = (dr_datarow["creditmgmt_name"].ToString()),
                            clustermanagerGid = (dr_datarow["cluster_manager"].ToString()),
                            mailsent_date = (dr_datarow["mailsent_date"].ToString()),
                            startpenality_flag = (dr_datarow["penality_flag"].ToString()),
                        });

                        msSQL = "update ocs_trn_tcustomeralertgenerate set alert_flag='Y' where customeralert_gid='" + (dr_datarow["customeralert_gid"].ToString()) + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objCutomerAlert.customermail_list = getmail;
                }
                dt_datatable.Dispose();
                objCutomerAlert.status = true;
            }
            catch (Exception ex)
            {
                objCutomerAlert.status = false;
                objCutomerAlert.message = ex.Message.ToString();
            }
        }
        // Getcustomerpenalitydetails_da

        public void DaGetPenalityDetails(mailalert values, string customeralert_gid)
        {
            try
            {
               
                msSQL = " select b.customeralert_gid,a.customer_gid,a.vertical_code,a.customer_code,a.customername,a.customer_urn,b.template_content, " +
                    "date_format(penality_alertdate,'%d-%m-%Y') as penality_alertdate," +
                   " case when a.address<>'' then a.address else '-' end as address,address2, " +
                   " case when a.mobileno<>'' then a.mobileno else '-' end as mobileno, " +
                   " case when a.contactperson<>'' then a.contactperson else '-' end as contactperson, " +
                   " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                   " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                   " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                   " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                   " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager " +
                   " from ocs_trn_tcustomeralertgenerate  b " +
                   " left join ocs_mst_tcustomer a on a.customer_gid=b.customer_gid" +
                   " where b.customeralert_gid='" + customeralert_gid + "' ";
                objODBCDatareader = objdbconn .GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customeralert_gid = objODBCDatareader["customeralert_gid"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.customercode = objODBCDatareader["customer_code"].ToString();
                    values.customername = objODBCDatareader["customername"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.contactperson = objODBCDatareader["contactperson"].ToString();
                    values.addressline1edit = objODBCDatareader["address"].ToString();
                    values.addressline2edit = objODBCDatareader["address2"].ToString();
                    values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                    values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                    values.zonalGid = objODBCDatareader["zonal_head"].ToString();

                    values.clustermanagerGid = objODBCDatareader["cluster_manager"].ToString();

                    values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();

                    values.creditmanagerName = objODBCDatareader["creditmgmt_name"].ToString();
                    values.content = objODBCDatareader["template_content"].ToString();
                    values.penality_alertdate = objODBCDatareader["penality_alertdate"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select pop_username from adm_mst_tcompany where 1=1";
                values.from_mail = objdbconn .GetExecuteScalar(msSQL);

                msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                values.to_mail = objdbconn .GetExecuteScalar(msSQL);

                msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
                lsccdep_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select customer_gid from ocs_trn_tcustomeralertgenerate where customeralert_gid='" + customeralert_gid + "'";
                lscustomer_gid = objdbconn .GetExecuteScalar(msSQL);


                msSQL = " select d.employee_emailid as zonalmail_id,e.employee_emailid as businessheadmail_id,f.employee_emailid as clustermanagermail_id, " +
                        " b.employee_emailid as rmmail_id,g.employee_emailid as creditmanagermail_id " +
                        " from ocs_mst_tcustomer a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.relationship_manager " +
                        " left join hrm_mst_temployee d on d.employee_gid=a.zonal_head " +
                        " left join hrm_mst_temployee e on e.employee_gid=a.business_head " +
                        " left join hrm_mst_temployee f on f.employee_gid=a.cluster_manager_gid " +
                        " left join hrm_mst_temployee g on g.employee_gid=a.creditmanager_gid " +
                        " where a.customer_gid='" + lscustomer_gid + "'";
                objODBCDatareader = objdbconn .GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    lsrmmail_id = objODBCDatareader["rmmail_id"].ToString();
                    lszonalmail_id = objODBCDatareader["zonalmail_id"].ToString();
                    lsbusinessheadmail_id = objODBCDatareader["businessheadmail_id"].ToString();
                    lsclustermanagermail_id = objODBCDatareader["clustermanagermail_id"].ToString();
                    lscreditmanagermail_id = objODBCDatareader["creditmanagermail_id"].ToString();
                }
                objODBCDatareader.Close();

                if (lszonalmail_id != "")
                {
                    strRes = lszonalmail_id;
                }
                if (lsbusinessheadmail_id != "")
                {
                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsbusinessheadmail_id;
                    }
                    else
                    {
                        strRes = lsbusinessheadmail_id;
                    }
                }
                if (lsclustermanagermail_id != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsclustermanagermail_id;
                    }
                    else
                    {
                        strRes = lsclustermanagermail_id;
                    }
                }

                if (lsrmmail_id != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsrmmail_id;
                    }
                    else
                    {
                        strRes = lsrmmail_id;
                    }
                }
                if (lscreditmanagermail_id != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lscreditmanagermail_id;
                    }
                    else
                    {
                        strRes = lscreditmanagermail_id;
                    }
                }
                if (lsccdep_mailid != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsccdep_mailid;
                    }
                    else
                    {
                        strRes = lsccdep_mailid;
                    }
                }

                values.cc_mail = strRes;

                msSQL = "select date_format(mail_sentdate,'%Y-%m-%d') as mail_sentdate from ocs_trn_tcustomeralertgenerate where customeralert_gid='" + customeralert_gid + "'";
                objODBCDatareader = objdbconn .GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["mail_sentdate"].ToString() != "")
                    {
                        lsmail_sentdate = Convert.ToDateTime(objODBCDatareader["mail_sentdate"].ToString());
                    }
                }
                objODBCDatareader.Close();

                msSQL = " select a.record_id,a.deferral_gid,a.due_date,a.aging, date_format(created_date,'%d-%m-%Y %h:%i %p') as customeralert_sentdate," +
                          " a.deferral_status,a.deferral_remarks,a.deferral_name" +
                          " from ocs_trn_thistorycustomeralert a " +
                          " where a.customeralert_gid='" + customeralert_gid + "'  and history_penality is null";

                dt_datatable = objdbconn .GetDataTable (msSQL);
                var get_deferral = new List<mailalert_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " select date_format(approval_date,'%Y-%m-%d') as approval_date,date_format(extended_date,'%d-%m-%Y') as extended_date from ocs_trn_tdeferralapproval a " +
                               " where a.deferral_gid = '" + dr_datarow["deferral_gid"].ToString() + "' and a.approval_status = 'Extend'";
                        objODBCDatareader = objdbconn .GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            if (objODBCDatareader["approval_date"].ToString() != "")
                            {
                                lsapproval_date = Convert.ToDateTime(objODBCDatareader["approval_date"].ToString());

                            }
                            if (lsapproval_date >= lsmail_sentdate)
                            {
                                lsextended_date = objODBCDatareader["extended_date"].ToString();
                                lsextend_flag = "Y";
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            lsextend_flag = "N";
                            lsextended_date = "";
                            objODBCDatareader.Close();
                        }
                        
                        get_deferral.Add(new mailalert_list
                        {
                            due_date = (dr_datarow["due_date"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            remarks = (dr_datarow["deferral_remarks"].ToString()),
                            customeralert_sentdate = (dr_datarow["customeralert_sentdate"].ToString()),
                            extend_flag = lsextend_flag,
                            extend_date = lsextended_date,
                        });
                    }
                    values.mailalert_list = get_deferral;
                }
                dt_datatable.Dispose();

                msSQL = " select a.record_id,a.due_date,a.aging, date_format(created_date,'%d-%m-%Y %h:%i %p') as customeralert_sentdate," +
                         " a.deferral_status,a.deferral_remarks,a.deferral_name" +
                         " from ocs_trn_thistorycustomeralert a " +
                         " where a.customeralert_gid='" + customeralert_gid + "' and history_penality='Y'";

                dt_datatable = objdbconn .GetDataTable (msSQL);
                var get_deferralhistory = new List<mailhistorydeferral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_deferralhistory.Add(new mailhistorydeferral_list
                        {
                            due_date = (dr_datarow["due_date"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            remarks = (dr_datarow["deferral_remarks"].ToString()),
                            customeralert_sentdate = (dr_datarow["customeralert_sentdate"].ToString()),
                        });
                    }
                    values.mailhistorydeferral_list = get_deferralhistory;

                }
                dt_datatable.Dispose();


                msSQL = " select date_format(a.startpenality_date, '%d-%m-%Y %h:%i %p') as startpenalitydate,date_format(a.endpenality_date, '%d-%m-%Y %h:%i %p') as endpenalitydate , " +
                        " concat(c.user_code, ' / ', c.user_firstname, c.user_lastname) as sendername from ocs_trn_tpenalityalerthistory a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.customeralert_gid = '" + customeralert_gid + "'";

                dt_datatable = objdbconn .GetDataTable (msSQL);
                var get_penalityalert = new List<penalityalert_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (dr_datarow["startpenalitydate"].ToString() != "")
                        {
                            message = "Penal Start Alert On " + dr_datarow["startpenalitydate"].ToString() + "";
                        }
                        if (dr_datarow["endpenalitydate"].ToString() != "")
                        {
                            message = "Penal Stop Alert On " + dr_datarow["endpenalitydate"].ToString() + "";
                        }
                        get_penalityalert.Add(new penalityalert_list
                        {
                            penalityalert_start = message,
                            penalityalert_end = dr_datarow["endpenalitydate"].ToString(),
                            created_by = dr_datarow["sendername"].ToString(),
                        });
                    }
                    values.penalityalert_list = get_penalityalert;
                   
                }
                dt_datatable.Dispose();

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
            }

            values.status = true;
        }
        // Getpenalityrecorddetails_da
        public void DaGetPenalityRecords(string customeralert_gid, mailalert values)
        {
           
            msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                       " where customeralert_gid='" + customeralert_gid + "'";
            lsalert_totalcount = objdbconn .GetExecuteScalar(msSQL);

            msSQL = " select count(customeralert_gid) from ocs_trn_thistorycustomeralert " +
                 " where customeralert_gid='" + customeralert_gid + "' and penalityalert_status='N'";
            lspenality_deferralcount = objdbconn .GetExecuteScalar(msSQL);

            if (lsalert_totalcount == lspenality_deferralcount)
            {
                msSQL = " select b.customeralert_gid,a.customer_gid,a.vertical_code,a.customer_code,a.customername,a.customer_urn,b.template_content, " +
              "date_format(penality_alertdate,'%d-%m-%Y') as penality_alertdate," +
             " case when a.address<>'' then a.address else '-' end as address,address2, " +
             " case when a.mobileno<>'' then a.mobileno else '-' end as mobileno, " +
             " case when a.contactperson<>'' then a.contactperson else '-' end as contactperson, " +
             " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
             " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
             " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
             " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
             " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager " +
             " from ocs_trn_tcustomeralertgenerate  b " +
             " left join ocs_mst_tcustomer a on a.customer_gid=b.customer_gid" +
             " where b.customeralert_gid='" + customeralert_gid + "' ";
                objODBCDatareader = objdbconn .GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customeralert_gid = objODBCDatareader["customeralert_gid"].ToString();
                    values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                    values.customercode = objODBCDatareader["customer_code"].ToString();
                    values.customername = objODBCDatareader["customername"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.contactperson = objODBCDatareader["contactperson"].ToString();
                    values.addressline1edit = objODBCDatareader["address"].ToString();
                    values.addressline2edit = objODBCDatareader["address2"].ToString();
                    values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                    values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                    values.zonalGid = objODBCDatareader["zonal_head"].ToString();

                    values.clustermanagerGid = objODBCDatareader["cluster_manager"].ToString();

                    values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();

                    values.creditmanagerName = objODBCDatareader["creditmgmt_name"].ToString();
                    values.content = objODBCDatareader["template_content"].ToString();
                    values.penality_alertdate = objODBCDatareader["penality_alertdate"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select pop_username from adm_mst_tcompany where 1=1";
                values.from_mail = objdbconn .GetExecuteScalar(msSQL);

                msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                values.to_mail = objdbconn .GetExecuteScalar(msSQL);


                msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
                lsccdep_mailid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select customer_gid from ocs_trn_tcustomeralertgenerate where customeralert_gid='" + customeralert_gid + "'";
                lscustomer_gid = objdbconn .GetExecuteScalar(msSQL);


                msSQL = " select d.employee_emailid as zonalmail_id,e.employee_emailid as businessheadmail_id,f.employee_emailid as clustermanagermail_id, " +
                        " b.employee_emailid as rmmail_id,g.employee_emailid as creditmanagermail_id " +
                        " from ocs_mst_tcustomer a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.relationship_manager " +
                        " left join hrm_mst_temployee d on d.employee_gid=a.zonal_head " +
                        " left join hrm_mst_temployee e on e.employee_gid=a.business_head " +
                        " left join hrm_mst_temployee f on f.employee_gid=a.cluster_manager_gid " +
                        " left join hrm_mst_temployee g on g.employee_gid=a.creditmanager_gid " +
                        " where a.customer_gid='" + lscustomer_gid + "'";
                objODBCDatareader = objdbconn .GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    lsrmmail_id = objODBCDatareader["rmmail_id"].ToString();
                    lszonalmail_id = objODBCDatareader["zonalmail_id"].ToString();
                    lsbusinessheadmail_id = objODBCDatareader["businessheadmail_id"].ToString();
                    lsclustermanagermail_id = objODBCDatareader["clustermanagermail_id"].ToString();
                    lscreditmanagermail_id = objODBCDatareader["creditmanagermail_id"].ToString();
                }
                objODBCDatareader.Close();

                if (lszonalmail_id != "")
                {
                    strRes = lszonalmail_id;
                }
                if (lsbusinessheadmail_id != "")
                {
                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsbusinessheadmail_id;
                    }
                    else
                    {
                        strRes = lsbusinessheadmail_id;
                    }
                }
                if (lsclustermanagermail_id != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsclustermanagermail_id;
                    }
                    else
                    {
                        strRes = lsclustermanagermail_id;
                    }
                }

                if (lsrmmail_id != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsrmmail_id;
                    }
                    else
                    {
                        strRes = lsrmmail_id;
                    }
                }
                if (lscreditmanagermail_id != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lscreditmanagermail_id;
                    }
                    else
                    {
                        strRes = lscreditmanagermail_id;
                    }
                }
                if (lsccdep_mailid != "")
                {

                    if (strRes != "")
                    {
                        strRes = strRes + "," + lsccdep_mailid;
                    }
                    else
                    {
                        strRes = lsccdep_mailid;
                    }
                }
                //msSQL = "select email_id from hrm_mst_tdepartment where department_name like '%Credit Admin%'";
                values.cc_mail = strRes;

               
                values.status = true;
            }
            else
            {
                msSQL = " select record_id from ocs_trn_thistorycustomeralert " +
                       " where customeralert_gid = '" + customeralert_gid + "' and penalityalert_status = 'Y'";
                dt_datatable = objdbconn .GetDataTable (msSQL);
                for (int loopCount = 0; loopCount < dt_datatable.Rows.Count; loopCount++)
                {
                    deferral_gid = deferral_gid + "," + dt_datatable.Rows[loopCount]["record_id"];

                }

                values.message = "Penal Alert Cannot be Stopped. Since" + deferral_gid + " Deferrals are not Closed..!";
                dt_datatable.Dispose();

                values.status = false;
            }

        }

        public void DaPostStartPenalityAlert(string employee_gid, string customeralert_gid, mailalert values)
        {
           

          

            msSQL = " update ocs_trn_tcustomeralertgenerate set " +
                    " penality_flag ='Y'" +
                    " where customeralert_gid='" + customeralert_gid + "'";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

            msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
            lsdepartment_mailid = objdbconn .GetExecuteScalar(msSQL);

            msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
            lsccdep_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select customer_gid from ocs_trn_tcustomeralertgenerate where customeralert_gid='" + customeralert_gid + "'";
            lscustomer_gid = objdbconn .GetExecuteScalar(msSQL);


            msSQL = " select d.employee_emailid as zonalmail_id,e.employee_emailid as businessheadmail_id,f.employee_emailid as clustermanagermail_id, " +
                    " b.employee_emailid as rmmail_id,g.employee_emailid as creditmanagermail_id " +
                    " from ocs_mst_tcustomer a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.relationship_manager " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.zonal_head " +
                    " left join hrm_mst_temployee e on e.employee_gid=a.business_head " +
                    " left join hrm_mst_temployee f on f.employee_gid=a.cluster_manager_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid=a.creditmanager_gid " +
                    " where a.customer_gid='" + lscustomer_gid + "'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();            
                lsrmmail_id = objODBCDatareader["rmmail_id"].ToString();
                lszonalmail_id = objODBCDatareader["zonalmail_id"].ToString();
                lsbusinessheadmail_id = objODBCDatareader["businessheadmail_id"].ToString();
                lsclustermanagermail_id = objODBCDatareader["clustermanagermail_id"].ToString();
                lscreditmanagermail_id = objODBCDatareader["creditmanagermail_id"].ToString();
            }
            objODBCDatareader.Close();

            if (lszonalmail_id != "")
            {
                strRes = lszonalmail_id;
            }
            if (lsbusinessheadmail_id != "")
            {
                if (strRes != "")
                {
                    strRes = strRes + "," + lsbusinessheadmail_id;
                }
                else
                {
                    strRes = lsbusinessheadmail_id;
                }
            }
            if (lsclustermanagermail_id != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lsclustermanagermail_id;
                }
                else
                {
                    strRes = lsclustermanagermail_id;
                }
            }

            if (lsrmmail_id != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lsrmmail_id;
                }
                else
                {
                    strRes = lsrmmail_id;
                }
            }
            if (lscreditmanagermail_id != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lscreditmanagermail_id;
                }
                else
                {
                    strRes = lscreditmanagermail_id;
                }
            }
            if (lsccdep_mailid != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lsccdep_mailid;
                }
                else
                {
                    strRes = lsccdep_mailid;
                }
            }


            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany where company_gid='1'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lspop_mail = objODBCDatareader["pop_username"].ToString();
                lspop_password = objODBCDatareader["pop_password"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.customername,a.customer_urn from ocs_trn_tcustomeralertgenerate b " +
                   " left join ocs_mst_tcustomer a on a.customer_gid = b.customer_gid " +
                   " where b.customeralert_gid = '" + customeralert_gid + "'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomername = objODBCDatareader["customername"].ToString();
                lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
            }
            objODBCDatareader.Close();

            message = "Dear Team,  <br />";
            message = message + "<br />";
            message = message + "Penal interest of 0.25% per month to be levied with immediate effect on <br />";
            message = message + "<br />";
            message = message + "<b>M/s." + lscustomername + "</b><br />";
            message = message + "<br />";
            message = message + "URN No :<b> " + lscustomer_urn + "</b> for the non-compliance of terms & condition set by Samunnati.<br />";
            message = message + "<br />";
            message = message + "<b>Thanks & Regards, </b> ";
            message = message + "<br />";
            message = message + "<b>Credit Administration Team</b> ";
            message = message + "<br />";
            message = message + "<b>Samunnati Financial Intermediation & Services Pvt Ltd </b> ";
            message = message + "<br />";

            if ((lsdepartment_mailid != "") || lsdepartment_mailid != null)
            {
                mailflag = objcmnfunctions.SendSMTP2(lspop_mail, lspop_password, lsdepartment_mailid, " Penal Start Alert For the " + lscustomername + "'", message, strRes, "", "");
                if (mailflag != 0)
                {
                    msGetGid = objcmnfunctions  .GetMasterGID("PEAH");
                    msSQL = " insert into ocs_trn_tpenalityalerthistory ( " +
                        " penalityalerthistory_gid, " +
                        " customeralert_gid, " +
                        " from_mail, " +
                        " to_mail, " +
                        " cc_mail," +
                        " penalitystart_status," +
                        " startpenality_date, " +
                        " startpenality_content," +
                        " created_by ," +
                        " created_date " +
                        " ) values( " +
                        "'" + msGetGid + "'," +
                        "'" + customeralert_gid + "'," +
                        "'" + lspop_mail + "'," +
                        "'" + lsdepartment_mailid + "'," +
                        "'" + lscc_mail + "'," +
                        "'Y'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + message + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                    msSQL = "update ocs_trn_tcustomeralertgenerate set penality_flag='Y' where customeralert_gid='" + customeralert_gid + "'";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured While Sending the Mail..!";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Sending the Mail..!";
            }

            values.status = true;
            values.message = "Penal Start Alert Mail sent Successfully..!";
        }

        // postendpenalityalert_da
        public void DaPostendpPenalityAlert(string employee_gid, string customeralert_gid, mailalert values)
        {
           
            msSQL = " select deferral_gid from ocs_trn_thistorycustomeralert " +
                    " where customeralert_gid = '" + customeralert_gid + "'";
            dt_datatable = objdbconn .GetDataTable (msSQL);
            for (int i = 0; i < dt_datatable.Rows.Count; i++)
            {
                msSQL = " update ocs_trn_thistorycustomeralert set history_penality='Y' " +
                        " where customeralert_gid = '" + customeralert_gid + "' and deferral_gid='" + dt_datatable.Rows[i]["deferral_gid"] + "'";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            msSQL = " update ocs_trn_tcustomeralertgenerate set " +
                    " penality_flag ='N'" +
                    " where customeralert_gid='" + customeralert_gid + "'";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

            msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
            lsdepartment_mailid = objdbconn .GetExecuteScalar(msSQL);

            msSQL = "select email_id from hrm_mst_tdepartment where department_name='Credit Administration'";
            lsccdep_mailid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = " select customer_gid from ocs_trn_tcustomeralertgenerate where customeralert_gid='" + customeralert_gid + "'";
            lscustomer_gid = objdbconn .GetExecuteScalar(msSQL);


            msSQL = " select d.employee_emailid as zonalmail_id,e.employee_emailid as businessheadmail_id,f.employee_emailid as clustermanagermail_id, " +
                    " b.employee_emailid as rmmail_id,g.employee_emailid as creditmanagermail_id " +
                    " from ocs_mst_tcustomer a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.relationship_manager " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.zonal_head " +
                    " left join hrm_mst_temployee e on e.employee_gid=a.business_head " +
                    " left join hrm_mst_temployee f on f.employee_gid=a.cluster_manager_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid=a.creditmanager_gid " +
                    " where a.customer_gid='" + lscustomer_gid + "'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lsrmmail_id = objODBCDatareader["rmmail_id"].ToString();
                lszonalmail_id = objODBCDatareader["zonalmail_id"].ToString();
                lsbusinessheadmail_id = objODBCDatareader["businessheadmail_id"].ToString();
                lsclustermanagermail_id = objODBCDatareader["clustermanagermail_id"].ToString();
                lscreditmanagermail_id = objODBCDatareader["creditmanagermail_id"].ToString();
            }
            objODBCDatareader.Close();

            if (lszonalmail_id != "")
            {
                strRes = lszonalmail_id;
            }
            if (lsbusinessheadmail_id != "")
            {
                if (strRes != "")
                {
                    strRes = strRes + "," + lsbusinessheadmail_id;
                }
                else
                {
                    strRes = lsbusinessheadmail_id;
                }
            }
            if (lsclustermanagermail_id != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lsclustermanagermail_id;
                }
                else
                {
                    strRes = lsclustermanagermail_id;
                }
            }

            if (lsrmmail_id != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lsrmmail_id;
                }
                else
                {
                    strRes = lsrmmail_id;
                }
            }
            if (lscreditmanagermail_id != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lscreditmanagermail_id;
                }
                else
                {
                    strRes = lscreditmanagermail_id;
                }
            }
            if (lsccdep_mailid != "")
            {

                if (strRes != "")
                {
                    strRes = strRes + "," + lsccdep_mailid;
                }
                else
                {
                    strRes = lsccdep_mailid;
                }
            }


            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany where company_gid='1'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lspop_mail = objODBCDatareader["pop_username"].ToString();
                lspop_password = objODBCDatareader["pop_password"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.customername,a.customer_urn from ocs_trn_tcustomeralertgenerate b " +
                   " left join ocs_mst_tcustomer a on a.customer_gid = b.customer_gid " +
                   " where b.customeralert_gid = '" + customeralert_gid + "'";
            objODBCDatareader = objdbconn .GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscustomername = objODBCDatareader["customername"].ToString();
                lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
            }
            objODBCDatareader.Close();

            message = "Dear Team,  <br />";
            message = message + "<br />";
            message = message + "Penalty interest of 0.25% Per Month to be stopped with immediate effect on  <br />";
            message = message + "<br />";
            message = message + "<b>M/s." + lscustomername + "</b><br />";
            message = message + "<br />";
            message = message + "<b>URN No :</b> " + lscustomer_urn + "</b><br />";
            message = message + "<br />";
            message = message + "<b>Thanks & Regards, </b> ";
            message = message + "<br />";
            message = message + "<b>Credit Administration Team</b> ";
            message = message + "<br />";
            message = message + "<b>Samunnati Financial Intermediation & Services Pvt Ltd </b> ";
            message = message + "<br />";

            if ((lsdepartment_mailid != "") || lsdepartment_mailid != null)
            {
                mailflag = objcmnfunctions.SendSMTP2(lspop_mail, lspop_password, lsdepartment_mailid, "Penal Stop Alert For the " + lscustomername + "' ", message, strRes, "", "");
                if (mailflag != 0)
                {
                    msGetGid = objcmnfunctions  .GetMasterGID("PEAH");
                    msSQL = " insert into ocs_trn_tpenalityalerthistory ( " +
                        " penalityalerthistory_gid, " +
                        " customeralert_gid, " +
                        " from_mail, " +
                        " to_mail, " +
                        " cc_mail," +
                        " penalityend_status," +
                        " endpenality_date, " +
                        " endpenality_content," +
                        " created_by ," +
                        " created_date " +
                        " ) values( " +
                        "'" + msGetGid + "'," +
                        "'" + customeralert_gid + "'," +
                        "'" + lspop_mail + "'," +
                        "'" + lsdepartment_mailid + "'," +
                        "'" + lscc_mail + "'," +
                        "'Y'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + message + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                }
                else
                {
                  values .  message = "Error Occured While Sending the Mail...!";
                    values.status = false;
                    
                }
            }
            else
            {
                message = "Check the Mail ID...!";
                values.status = false;
            }

            values.status = true;
            values.message = "Penal Stop Alert Mail sent Successfully..!";
        }
    }
}