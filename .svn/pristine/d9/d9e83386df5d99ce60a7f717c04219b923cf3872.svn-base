using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using System.Net.Mail;
using System.Net;
using ems.osd.Models;
using ems.storage.Functions;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using Ionic.Zip;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;


namespace ems.osd.DataAccess
{
    public class DaOsdTrnBankAlert
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_table;
        string msGetGID, msSQL, msGetDocumentGid, msGet_RefNo, msGetGid, msGetGid2;
        string lscustref_no, lsbank_name, MSGETGID;
        OdbcDataReader objODBCDatareader;
        int mnResult, ls_port;
        string lscustomer2usertype_gid, lsapplication_gid, lsapplicant_type, lscustomer_gid, lscustomer_urn;
        HttpPostedFile httpPostedFile;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        string lscontent = string.Empty;
        string cc_mailid = string.Empty;
        string lsRaisedBy, lsraised_department, status_updatedby;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, tomailid_list, ccmail_id, ccmailid_list, body, sub, lawyeruser_password, lawyeruser_code, lawyeruser_name = string.Empty;
        string lsref_no, lsrm_gid, lsrm_name, lsrh_gid, lsrh_name, lscustomerurn, lscustomername;
        public void DaGetAllocatedSummary(MdlBankAlertAllocated values, string employee_gid)
        {


            msSQL = " SELECT a.kotakAPI_flag,a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status," +
                    " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging,a.seen_flag,customer_urn," +
                    " a.customer_name,a.customer_gid,operation_status,if(assigned_toname is null,'-',assigned_toname) as assigned_toname,transfer_flag, " +
                    " if(a.kotakAPI_flag ='Y','Kotak API','Email') as source FROM osd_trn_tbankalert2allocated a" +
                    " WHERE a.allocated_status in ('Pending','RH Approval Pending','Completed','RH Rejected') and a.relationshipmanager_gid='" + employee_gid + "' and " +
                    "  (transferinitiated_by <>relationshipmanager_gid  or transferinitiated_by is null) order by a.bankalert2allocated_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocated_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocated_list
                {
                    kotakAPI_flag = row["kotakAPI_flag"].ToString(),
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    assigned_toname = row["assigned_toname"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetAllocatedAssignedSummary(MdlBankAlertAllocated values, string employee_gid)
        {


            msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date," +
                  " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status," +
                  " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                  " a.customer_name,a.customer_gid,operation_status,if(relationshipmanager_name is null,'-',relationshipmanager_name) as assigned_rh," +
                  " if(assigned_toname is null,'-',assigned_toname) as assigned_toname,transfer_flag,a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source " +
                  " FROM osd_trn_tbankalert2allocated a" +
                  " WHERE a.allocated_status in ('Pending','RH Approval Pending','Completed','RH Rejected') and " +
                  " (a.regionalheadlevel_gid = '" + employee_gid + "' and a.relationshipmanager_gid <> '" + employee_gid + "' ) " +
                   " or (a.drmlevel_gid ='" + employee_gid + "' and a.relationshipmanager_gid <> '" + employee_gid + "') " +
                   " or (a.clustermanagerlevel_gid = '" + employee_gid + "' and a.relationshipmanager_gid <> '" + employee_gid + "') or " +
                   " (a.zonalheadlevel_gid = '" + employee_gid + "' and a.relationshipmanager_gid <> '" + employee_gid + "') or " +
                   " (a.businessheadlevel_gid = '" + employee_gid + "' and a.relationshipmanager_gid <> '" + employee_gid + "') and " +
                  "  (transferinitiated_by <>relationshipmanager_gid  or transferinitiated_by is null) order by a.bankalert2allocated_gid asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocatedAssigned_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocatedAssigned_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    assigned_toname = row["assigned_toname"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                    assigned_rh = row["assigned_rh"].ToString(),
                    kotakAPI_flag = row["kotakAPI_flag"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }


        public void DaGetRMTransferSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.transferinitiated_date,'%d-%m-%Y || %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status," +
                    " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging,a.seen_flag,customer_urn," +
                    " a.customer_name,a.customer_gid,operation_status,if(assigned_toname is null,'-',assigned_toname) as assigned_toname,transfer_flag, " +
                    " transfer_type,if(transferto_name is null,'-',transferto_name) as transferto_name FROM osd_trn_tbankalert2allocated a " +
                    " WHERE a.transferinitiated_by='" + employee_gid + "' and transfer_flag='Y' " +
                    " order by a.bankalert2allocated_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlerttransfer_list = dt_datatable.AsEnumerable().Select(row => new BankAlerttransfer_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    assigned_toname = row["assigned_toname"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                    transfer_type = row["transfer_type"].ToString(),
                    transferto_name = row["transferto_name"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetAllocatedDtl(string bankalert2allocated_gid, string customer_gid, MdlOsdTrnBankAlert values, string employee_gid)
        {

            values.employee_gid = employee_gid;

            msSQL = " select a.ticketref_no,a.bankalert2allocated_gid,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date,a.email_subject,a.email_content,a.allocated_status," +
                    " customer_name,customer_urn,relationshipmanager_name,relationshipmanager_gid,ticketref_no,cc,bcc,document_name,document_path,email_to,from_mailaddress," +
                    " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging,transfer_flag,transfer_type, " +
                    " operation_status,a.servicerequest_gid,if(transferto_name is null,'-',transferto_name) as transferto_name FROM osd_trn_tbankalert2allocated a" +
                    " where a.bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.relationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                values.allocated_status = objODBCDatareader["allocated_status"].ToString();
                values.email_cc = objODBCDatareader["cc"].ToString();
                values.email_bcc = objODBCDatareader["bcc"].ToString();
                values.document_name = objODBCDatareader["document_name"].ToString();
                values.document_path = objcmnstorage.EncryptData(objODBCDatareader["document_path"].ToString());
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.operation_status = objODBCDatareader["operation_status"].ToString();
                values.servicerequest_gid = objODBCDatareader["servicerequest_gid"].ToString();
                values.transfer_flag = objODBCDatareader["transfer_flag"].ToString();
                values.transfer_type = objODBCDatareader["transfer_type"].ToString();
                values.transferto_name = objODBCDatareader["transferto_name"].ToString();
            }
            objODBCDatareader.Close();

            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_gid ='" + customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    lscustomer_gid = objODBCDatareader["customer_gid"].ToString();

                    objODBCDatareader.Close();
                    msSQL = "select customername,customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name," +
                     " contactperson,zonal_riskmanagerName,assigned_RMName,riskMonitoring_Name,mobileno from ocs_mst_tcustomer" +
                     " where customer_gid='" + lscustomer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.customer_name = objODBCDatareader["customername"].ToString();
                        values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                        values.vertical = objODBCDatareader["vertical_code"].ToString();
                        values.zonal_head = objODBCDatareader["zonal_name"].ToString();
                        values.business_head = objODBCDatareader["businesshead_name"].ToString();
                        values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                        values.cluster_manager = objODBCDatareader["cluster_manager_name"].ToString();
                        values.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                        values.constitution = objODBCDatareader["constitution_name"].ToString();
                        values.contact_person = objODBCDatareader["contactperson"].ToString();
                        values.zonal_riskmanagerName = objODBCDatareader["zonal_riskmanagerName"].ToString();
                        values.riskmanager_name = objODBCDatareader["assigned_RMName"].ToString();
                        values.riskMonitoring_Name = objODBCDatareader["riskMonitoring_Name"].ToString();
                        values.mobile_no = objODBCDatareader["mobileno"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where customer_gid='" + lscustomer_gid + "' and user_type='Applicant'";
                    lscustomer2usertype_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                        " from ocs_mst_tcustomer2address where " +
                      " customer2usertype_gid='" + lscustomer2usertype_gid + "' and primary_address='Yes'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.address_type = objODBCDatareader["address_type"].ToString();
                        values.addressline1 = objODBCDatareader["addressline1"].ToString();
                        values.addressline2 = objODBCDatareader["addressline2"].ToString();
                        values.city = objODBCDatareader["city"].ToString();
                        values.state = objODBCDatareader["state"].ToString();
                        values.taluka = objODBCDatareader["taluka"].ToString();
                        values.district = objODBCDatareader["district"].ToString();
                        values.postal_code = objODBCDatareader["postal_code"].ToString();
                        values.country = objODBCDatareader["country"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();

                    msSQL = " select tmpcustomer_gid from ocs_tmp_tcustomer where tmpcustomer_gid ='" + customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_gid = objODBCDatareader["tmpcustomer_gid"].ToString();
                        objODBCDatareader.Close();
                        msSQL = "select customername,customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name," +
                                " contactperson,zonal_riskmanagerName,assigned_RMName,riskMonitoring_Name,mobileno from ocs_tmp_tcustomer where tmpcustomer_gid='" + lscustomer_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.customer_name = objODBCDatareader["customername"].ToString();
                            values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                            values.vertical = objODBCDatareader["vertical_code"].ToString();
                            values.zonal_head = objODBCDatareader["zonal_name"].ToString();
                            values.business_head = objODBCDatareader["businesshead_name"].ToString();
                            values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                            values.cluster_manager = objODBCDatareader["cluster_manager_name"].ToString();
                            values.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                            values.constitution = objODBCDatareader["constitution_name"].ToString();
                            values.zonal_riskmanagerName = objODBCDatareader["zonal_riskmanagerName"].ToString();
                            values.riskmanager_name = objODBCDatareader["assigned_RMName"].ToString();
                            values.riskMonitoring_Name = objODBCDatareader["riskMonitoring_Name"].ToString();
                            values.mobile_no = objODBCDatareader["mobileno"].ToString();
                        }
                        objODBCDatareader.Close();
                        msSQL = "select customer2usertype_gid from ocs_tmp_tcustomer2userdtl where tmpcustomer_gid='" + lscustomer_gid + "' and user_type='Applicant'";
                        lscustomer2usertype_gid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select address_type,addressline1,addressline2,city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                            " from ocs_tmp_tcustomer2address where " +
                          " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.address_type = objODBCDatareader["address_type"].ToString();
                            values.addressline1 = objODBCDatareader["addressline1"].ToString();
                            values.addressline2 = objODBCDatareader["addressline2"].ToString();
                            values.city = objODBCDatareader["city"].ToString();
                            values.state = objODBCDatareader["state"].ToString();
                            values.taluka = objODBCDatareader["taluka"].ToString();
                            values.district = objODBCDatareader["district"].ToString();
                            values.postal_code = objODBCDatareader["postal_code"].ToString();
                            values.country = objODBCDatareader["country"].ToString();
                        }
                        objODBCDatareader.Close();


                        values.status = true;
                        values.message = "success";
                    }

                }
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }
        public void DaGetServiceReqDtl(MdlOsdTrnBankAlert values, string bankalert2allocated_gid)
        {
            msSQL = "select servicerequest_gid from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.servicerequest_gid = objODBCDatareader["servicerequest_gid"].ToString();
                
            }
            objODBCDatareader.Close();
        }

        public void DaGetEmployee(MdlEmployee objemployee, string user_gid)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by (case when a.user_gid='" + user_gid + "' then 0 else 1 end),a.user_firstname asc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }
        public void DaGetEmployeeName(string user_gid, MdlOsdTrnBankAlert values,string employee_gid)
        {
            msSQL = "select concat(user_firstname,' ' ,user_lastname,'||',user_code) as user_name from adm_mst_tuser where user_gid='" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.user_name = objODBCDatareader["user_name"].ToString();
                values.employee_gid = employee_gid;
            }
            objODBCDatareader.Close();
        }
        public void DaGetAllocatedDetail(string bankalert2allocated_gid, string customer_gid, string customer_urn, MdlOsdTrnBankAlert values, string employee_gid)
        {

            values.employee_gid = employee_gid;

            msSQL = " select a.kotakAPI_flag,b.apiresponse_time as detailsreceived_at, (CASE WHEN(kotakAPI_flag = 'Y') THEN 'KOTAK-API'  WHEN(brs_flag = 'Y') THEN 'BRS' ELSE 'E-mail'END)  as source," +
                    " c.Master_Acc_No,c.Remitt_Info,c.Remit_Name,c.Remit_Ifsc,c.Amount,c.Txn_Ref_No,c.Utr_No,c.Pay_Mode,c.E_Coll_Acc_No,c.Remit_Ac_Nmbr," +
                    " c.Creditdateandtime,c.Txn_Date,c.Bene_Cust_Acname,c.REF1,c.REF2,c.REF3," +
                    " a.ticketref_no,a.bankalert2allocated_gid,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date,a.email_subject,a.email_content,a.allocated_status," +
                    " customer_name,customer_urn,relationshipmanager_name,relationshipmanager_gid,ticketref_no,cc,bcc,document_name,document_path,email_to,from_mailaddress," +
                    " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging,transfer_flag,transfer_type, " +
                    " operation_status,a.servicerequest_gid,if(transferto_name is null,'-',transferto_name) as transferto_name FROM osd_trn_tbankalert2allocated a" +
                    " left join osd_trn_tecollectionresponsefromsambtrn b on a.ecollectionresponsefromsambtrn_gid=b.ecollectionresponsefromsambtrn_gid " +
                    " left join osd_trn_tecollectionresponsefromsambtrndtls c on a.ecollectionresponsefromsambtrndtls_gid=c.ecollectionresponsefromsambtrndtls_gid" +
                    " where a.bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.kotakAPI_flag = objODBCDatareader["kotakAPI_flag"].ToString();
                values.detailsreceived_at = objODBCDatareader["detailsreceived_at"].ToString();
                values.source = objODBCDatareader["source"].ToString();
                values.Master_Acc_No = objODBCDatareader["Master_Acc_No"].ToString();
                values.Remitt_Info = objODBCDatareader["Remitt_Info"].ToString();
                values.Remit_Name = objODBCDatareader["Remit_Name"].ToString();
                values.Remit_Ifsc = objODBCDatareader["Remit_Ifsc"].ToString();
                values.Amount = objODBCDatareader["Amount"].ToString();
                values.Txn_Ref_No = objODBCDatareader["Txn_Ref_No"].ToString();
                values.Utr_No = objODBCDatareader["Utr_No"].ToString();
                values.Pay_Mode = objODBCDatareader["Pay_Mode"].ToString();
                values.E_Coll_Acc_No = objODBCDatareader["E_Coll_Acc_No"].ToString();
                values.Remit_Ac_Nmbr = objODBCDatareader["Remit_Ac_Nmbr"].ToString();
                values.Creditdateandtime = objODBCDatareader["Creditdateandtime"].ToString();
                values.Txn_Date = objODBCDatareader["Txn_Date"].ToString();
                values.Bene_Cust_Acname = objODBCDatareader["Bene_Cust_Acname"].ToString();
                values.REF1 = objODBCDatareader["REF1"].ToString();
                values.REF2 = objODBCDatareader["REF2"].ToString();
                values.REF3 = objODBCDatareader["REF3"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.relationshipmanager_gid = objODBCDatareader["relationshipmanager_gid"].ToString();
                values.allocated_status = objODBCDatareader["allocated_status"].ToString();
                values.email_cc = objODBCDatareader["cc"].ToString();
                values.email_bcc = objODBCDatareader["bcc"].ToString();
                values.document_name = objODBCDatareader["document_name"].ToString();
                values.document_path = objcmnstorage.EncryptData(objODBCDatareader["document_path"].ToString());
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.operation_status = objODBCDatareader["operation_status"].ToString();
                values.servicerequest_gid = objODBCDatareader["servicerequest_gid"].ToString();
                values.transfer_flag = objODBCDatareader["transfer_flag"].ToString();
                values.transfer_type = objODBCDatareader["transfer_type"].ToString();
                values.transferto_name = objODBCDatareader["transferto_name"].ToString();
            }
            objODBCDatareader.Close();

            try
            {

                msSQL = "select customer_name,customer_urn,vertical_name,regionalhead_name,zonalriskmanager_name,riskmanager_name,contactpersonfirst_name,headriskmonitoring_name,constitution_name, " +
                        " relationshipmanager_name,drm_name,clustermanager_name,zonalhead_name,businesshead_name,creditmanager_name, " +
                        " credithead_name,creditnationalmanager_name,creditregionalmanager_name " +
                        " from ocs_trn_tcadapplication" +
                        " where customer_urn='" + customer_urn + "' order by created_date desc limit 1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.customer_urnname = objODBCDatareader["customer_urn"].ToString();
                    values.zonalhead_name = objODBCDatareader["zonalhead_name"].ToString();
                    values.regionalhead_name = objODBCDatareader["regionalhead_name"].ToString();
                    values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                    values.customername_application = objODBCDatareader["customer_name"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.zonalriskmanager_name = objODBCDatareader["zonalriskmanager_name"].ToString();
                    values.risk_managername = objODBCDatareader["riskmanager_name"].ToString();
                    values.headriskmonitoring_name = objODBCDatareader["headriskmonitoring_name"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.relationship_managername = objODBCDatareader["relationshipmanager_name"].ToString();
                    values.drm_name = objODBCDatareader["drm_name"].ToString();
                    values.clustermanager_name = objODBCDatareader["clustermanager_name"].ToString();
                    values.creditmanager_name = objODBCDatareader["creditmanager_name"].ToString();
                    values.contactpersonfirst_name = objODBCDatareader["contactpersonfirst_name"].ToString();
                    values.credithead_name = objODBCDatareader["credithead_name"].ToString();
                    values.creditnationalmanager_name = objODBCDatareader["creditnationalmanager_name"].ToString();
                    values.creditregionalmanager_name = objODBCDatareader["creditregionalmanager_name"].ToString();

                }
                objODBCDatareader.Close();
                if (customer_urn != "")
                {
                    msSQL = "select application_gid,applicant_type from ocs_trn_tcadapplication " +
                            " where customer_urn ='" + customer_urn + "'  order by created_date desc limit 1";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsapplication_gid = objODBCDatareader["application_gid"].ToString();
                        lsapplicant_type = objODBCDatareader["applicant_type"].ToString();
                    }
                    objODBCDatareader.Close();


                    if (lsapplicant_type == "Institution")
                    {
                        msSQL = " select b.addresstype_name,b.addressline1,b.addressline2,b.city,b.state,b.taluka," +
                                " b.country,b.district,b.postal_code" +
                                " from" +
                                " ocs_mst_tinstitution a" +
                                " left join ocs_trn_tcadinstitution2address b on a.institution_gid = b.institution_gid" +
                                " where a.application_gid = '" + lsapplication_gid + "'";
                    }
                    else
                    {
                        msSQL = " select b.addresstype_name,b.addressline1,b.addressline2,b.city,b.state,b.taluka, " +
                                " b.country,b.district,b.postal_code" +
                                " from " +
                                " ocs_mst_tcontact a " +
                                " left join ocs_trn_tcadcontact2address b on a.contact_gid = b.contact_gid " +
                                " where a.application_gid = '" + lsapplication_gid + "'";
                    }

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.address_type = objODBCDatareader["addresstype_name"].ToString();
                        values.addressline1 = objODBCDatareader["addressline1"].ToString();
                        values.addressline2 = objODBCDatareader["addressline2"].ToString();
                        values.city = objODBCDatareader["city"].ToString();
                        values.state = objODBCDatareader["state"].ToString();
                        values.taluka = objODBCDatareader["taluka"].ToString();
                        values.district = objODBCDatareader["district"].ToString();
                        values.postal_code = objODBCDatareader["postal_code"].ToString();
                        values.country = objODBCDatareader["country"].ToString();
                    }
                    objODBCDatareader.Close();

                    values.status = true;
                    values.message = "success";
                }




            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetNotAllocatedDtl(string bankalert2notallocated_gid, MdlOsdTrnBankAlert values)
        {
            msSQL = " select a.kotakAPI_flag,a.customer_urn,b.apiresponse_time as detailsreceived_at,if (a.kotakAPI_flag = 'Y','Kotak API','Email') as source," +
                    " c.Master_Acc_No,c.Remitt_Info,c.Remit_Name,c.Remit_Ifsc,c.Amount,c.Txn_Ref_No,c.Utr_No,c.Pay_Mode,c.E_Coll_Acc_No,c.Remit_Ac_Nmbr," +
                    " c.Creditdateandtime,c.Txn_Date,c.Bene_Cust_Acname,c.REF1,c.REF2,c.REF3," +
                    " a.ticketref_no,a.bankalert2notallocated_gid,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date,a.email_subject,a.email_content," +
                    " ticketref_no,cc,bcc,document_name,document_path,email_to,from_mailaddress,CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') as aging " +
                    " FROM osd_trn_tbankalert2notallocated a" +
                    " left join osd_trn_tecollectionresponsefromsambtrn b on a.ecollectionresponsefromsambtrn_gid=b.ecollectionresponsefromsambtrn_gid " +
                    " left join osd_trn_tecollectionresponsefromsambtrndtls c on a.ecollectionresponsefromsambtrndtls_gid=c.ecollectionresponsefromsambtrndtls_gid" +
                    " where a.bankalert2notallocated_gid='" + bankalert2notallocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.kotakAPI_flag = objODBCDatareader["kotakAPI_flag"].ToString();
                values.detailsreceived_at = objODBCDatareader["detailsreceived_at"].ToString();
                values.source = objODBCDatareader["source"].ToString();
                values.Master_Acc_No = objODBCDatareader["Master_Acc_No"].ToString();
                values.Remitt_Info = objODBCDatareader["Remitt_Info"].ToString();
                values.Remit_Name = objODBCDatareader["Remit_Name"].ToString();
                values.Remit_Ifsc = objODBCDatareader["Remit_Ifsc"].ToString();
                values.Amount = objODBCDatareader["Amount"].ToString();
                values.Txn_Ref_No = objODBCDatareader["Txn_Ref_No"].ToString();
                values.Utr_No = objODBCDatareader["Utr_No"].ToString();
                values.Pay_Mode = objODBCDatareader["Pay_Mode"].ToString();
                values.E_Coll_Acc_No = objODBCDatareader["E_Coll_Acc_No"].ToString();
                values.Remit_Ac_Nmbr = objODBCDatareader["Remit_Ac_Nmbr"].ToString();
                values.Creditdateandtime = objODBCDatareader["Creditdateandtime"].ToString();
                values.Txn_Date = objODBCDatareader["Txn_Date"].ToString();
                values.Bene_Cust_Acname = objODBCDatareader["Bene_Cust_Acname"].ToString();
                values.REF1 = objODBCDatareader["REF1"].ToString();
                values.REF2 = objODBCDatareader["REF2"].ToString();
                values.REF3 = objODBCDatareader["REF3"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_cc = objODBCDatareader["cc"].ToString();
                values.email_bcc = objODBCDatareader["bcc"].ToString();
                values.document_name = objODBCDatareader["document_name"].ToString();
                values.document_path = objcmnstorage.EncryptData(objODBCDatareader["document_path"].ToString());
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
            }
            objODBCDatareader.Close();


        }

        public void DaRMDocumentUpload(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            String lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            if ((!System.IO.Directory.Exists(lspath)))
                System.IO.Directory.CreateDirectory(lspath);


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
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        MemoryStream ms = new MemoryStream();
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = " insert into ocs_trn_trmuploadocument( " +
                                    " fileupload_gid  ," +
                                    " bankalert2allocated_gid ," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msdocument_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
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
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured";
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;
            }
        }

        public void DaGetRMUploadedDoc(MdlcDocList objfileDtls, string employee_gid)
        {
            msSQL = " SELECT fileupload_gid,bankalert2allocated_gid,document_name,document_path FROM ocs_trn_trmuploadocument WHERE bankalert2allocated_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlDocDetails>();
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
                objfileDtls.filename = file_name.ToArray();
                objfileDtls.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new MdlDocDetails
                    {
                        id = dt["fileupload_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                    });
                }
                objfileDtls.MdlDocDetails = getDocList;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteRMUploadedDoc(string id, result objResult)
        {
            msSQL = " DELETE FROM ocs_trn_trmuploadocument WHERE fileupload_gid='" + id + "'";
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

        }
        public void DaPostRMSendback(MdlSendbacktobrs values, string employee_gid)
        {

            msSQL = "select concat(b.user_firstname ,' ',b.user_lastname,' / ',b.user_code) as rm_name from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid where employee_gid='" + employee_gid + "'";
            string lsrm_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update brs_trn_tbanktransactiondetails set " +
                    " assigned_rm = '" + lsrm_name + "'," +
                    " rmsendback_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " sendback_reason ='" + values.sendback_reason.Replace("'", "") + "', " +                    
                    " tagged_status = 'Reassign' where " +
                    " banktransc_gid = '" + values.banktransc_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update osd_trn_tbankalert2allocated set " +
                    "  allocated_status='Reassign' " +
                    " where " +
                    " bankalert2allocated_gid = '" + values.bankalert2allocated_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid2 = objcmnfunctions.GetMasterGID("BURL");
            msSQL = " insert into brs_trn_tunreconnlog(" +
                    " unreconnlog_gid," +
                    " banktransc_gid," +
                    " reason," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid2 + "'," +
                    "'" + values.banktransc_gid + "'," +
                    "'" + values.sendback_reason.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            values.message = "UnReconciliation Details Sent Back to BRS Successfully";
        }
        public void DaPostRMUpdation(MdlRMStatus values, string employee_gid)
        {
            msSQL = "update osd_trn_tbankalert2allocated set " +
                " seen_flag='N', " +
                " allocated_status='Completed',";
            if (values.rm_status == null || values.rm_status == "")
            {
                msSQL += "rm_status='',";
            }
            else
            {
                msSQL += "rm_status='" + values.rm_status.Replace("'", "") + "',";
            }
            if (values.rm_remarks == null || values.rm_remarks == "")
            {
                msSQL += "rm_remarks='',";
            }
            else
            {
                msSQL += "rm_remarks='" + values.rm_remarks.Replace("'", "") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ocs_trn_trmuploadocument set bankalert2allocated_gid='" + values.bankalert2allocated_gid + "' where bankalert2allocated_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        tomail_id = "";
                        tomail_id += dr_datarow["email_id"].ToString();
                        message.To.Add(new MailAddress(tomail_id));
                        tomailid_list += "" + tomail_id + ",";
                    }
                    dt_table.Dispose();
                    tomailid_list = tomailid_list.TrimEnd(',');



                    msSQL = "select concat(b.user_firstname ,' ',b.user_lastname,' / ',b.user_code) as rm_name from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid where employee_gid='" + employee_gid + "'";
                    string lsrm_name = objdbconn.GetExecuteScalar(msSQL);


                    sub = " Escrow payment confirmed";

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<b>" + HttpUtility.HtmlEncode(lsrm_name) + " </b>has confirmed the payment details,<br />";
                    body = body + "<br />";
                    body = body + "<b>Confirmation date & time :</b> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly initiate the required process and do the needful. <br />";
                    body = body + "<br />";
                    body = body + "<b>Regards,</b><br /> ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsrm_name) + ".<br /> ";
                    body = body + "<br />";





                    message.From = new MailAddress(ls_username);


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
                        msGetGID = objcmnfunctions.GetMasterGID("ALDB");
                        msSQL = "Insert into osd_trn_tbankalertmailcount( " +
                                                                                      " bankalertmailcount_gid," +
                                                                                      " from_mail," +
                                                                                      " to_mail," +
                                                                                      " cc_mail," +
                                                                                      " mail_status," +
                                                                                      " mail_senddate, " +
                                                                                      " created_by," +
                                                                                      " created_date)" +
                                                                                      " values(" +
                                                                                      "'" + msGetGID + "'," +
                                                                                      "'" + ls_username + "'," +
                                                                                      "'" + tomailid_list + "'," +
                                                                                      "'" + ccmailid_list + "'," +
                                                                                      "'RM Updated to Opertaion'," +
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

                values.status = true;
                values.message = "RM Status Updated Successfully";


            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating RM Status";
            }
        }
        public void DaPostRHapproval(MdlRMStatus values, string employee_gid)
        {
            msSQL = " select ticketref_no,relationshipmanagerlevel_gid, relationshipmanagerlevel_name, regionalheadlevel_gid, regionalheadlevel_name,customer_urn " +
                    " from osd_trn_tbankalert2allocated where bankalert2allocated_gid ='" + values.bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsref_no = objODBCDatareader["ticketref_no"].ToString();
                lsrm_gid = objODBCDatareader["relationshipmanagerlevel_gid"].ToString();
                lsrm_name = objODBCDatareader["relationshipmanagerlevel_name"].ToString();
                lsrh_gid = objODBCDatareader["regionalheadlevel_gid"].ToString();
                lsrh_name = objODBCDatareader["regionalheadlevel_name"].ToString();
                lscustomerurn = objODBCDatareader["customer_urn"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select allocated_status from osd_trn_tbankalert2allocated " +
                " where bankalert2allocated_gid ='" + values.bankalert2allocated_gid + "'";
            string approval_status = objdbconn.GetExecuteScalar(msSQL);
            if (approval_status == "RH Rejected")
            {
                msSQL = " select bankalertrefundapprl_gid from osd_trn_tbankalertrefundapprl " +
               " where bankalert2allocated_gid ='" + values.bankalert2allocated_gid + "'";
                string bankapproval_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "update osd_trn_tbankalertrefundapprl set approval_status='Pending',rh_remarks='',updated_date=null " +
                     " where bankalertrefundapprl_gid ='" + bankapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("BRHA");
                msSQL = " insert into osd_trn_tbankalertrefundapprl (" +
                                        " bankalertrefundapprl_gid," +
                                        " bankalert2allocated_gid," +
                                        " ticketref_no," +
                                        " customer_gid," +
                                        " customer_urn," +
                                        " assignedrm_gid," +
                                        " assignedrm_name," +
                                        " assignedrh_gid," +
                                        " assignedrh_name," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + values.bankalert2allocated_gid + "'," +
                                        "'" + lsref_no + "'," +
                                        "'" + values.customer_gid + "'," +
                                        "'" + lscustomerurn + "'," +
                                        "'" + lsrm_gid + "'," +
                                        "'" + lsrm_name + "'," +
                                        "'" + lsrh_gid + "'," +
                                        "'" + lsrh_name + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {
                msSQL = "update osd_trn_tbankalert2allocated set " +
                     " allocated_status='RH Approval Pending',";
                if (values.rm_status == null || values.rm_status == "")
                {
                    msSQL += "rm_status='',";
                }
                else
                {
                    msSQL += "rm_status='" + values.rm_status.Replace("'", @"\'") + "',";
                }
                if (values.rm_remarks == null || values.rm_remarks == "")
                {
                    msSQL += "rm_remarks='',";
                }
                else
                {
                    msSQL += "rm_remarks='" + values.rm_remarks.Replace("'", @"\'") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                    "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ocs_trn_trmuploadocument set bankalert2allocated_gid='" + values.bankalert2allocated_gid + "' where bankalert2allocated_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        tomail_id = "";
                        tomail_id += dr_datarow["email_id"].ToString();
                        message.To.Add(new MailAddress(tomail_id));
                        tomailid_list += "" + tomail_id + ",";
                    }
                    dt_table.Dispose();
                    tomailid_list = tomailid_list.TrimEnd(',');



                    msSQL = "select concat(b.user_firstname ,' ',b.user_lastname,' / ',b.user_code) as rm_name from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid where employee_gid='" + employee_gid + "'";
                    string lsrm_name = objdbconn.GetExecuteScalar(msSQL);


                    sub = " Escrow payment confirmed";

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<b>" + HttpUtility.HtmlEncode(lsrm_name) + " </b>has confirmed the payment details,<br />";
                    body = body + "<br />";
                    body = body + "<b>Confirmation date & time :</b> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly initiate the required process and do the needful. <br />";
                    body = body + "<br />";
                    body = body + "<b>Regards,</b><br /> ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsrm_name) + ".<br /> ";
                    body = body + "<br />";





                    message.From = new MailAddress(ls_username);


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
                        msGetGID = objcmnfunctions.GetMasterGID("ALDB");
                        msSQL = "Insert into osd_trn_tbankalertmailcount( " +
                                                                                      " bankalertmailcount_gid," +
                                                                                      " from_mail," +
                                                                                      " to_mail," +
                                                                                      " cc_mail," +
                                                                                      " mail_status," +
                                                                                      " mail_senddate, " +
                                                                                      " created_by," +
                                                                                      " created_date)" +
                                                                                      " created_date)" +
                                                                                      " values(" +
                                                                                      "'" + msGetGID + "'," +
                                                                                      "'" + ls_username + "'," +
                                                                                      "'" + tomailid_list + "'," +
                                                                                      "'" + ccmailid_list + "'," +
                                                                                      "'RM Updated to Opertaion'," +
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

                values.status = true;
                values.message = "RM Status Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating RM Status";
            }
        }
        public void DaGetRMStatusDtl(string bankalert2allocated_gid, MdlViewRMStatus values)
        {
            msSQL = "select a.rm_remarks,a.rm_status,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                " concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as updated_by from osd_trn_tbankalert2allocated a" +
                " left join hrm_mst_temployee b on a.updated_by=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.rm_remarks = objODBCDatareader["rm_remarks"].ToString();
                values.rm_status = objODBCDatareader["rm_status"].ToString();
                values.updated_date = objODBCDatareader["updated_date"].ToString();
                values.updated_by = objODBCDatareader["updated_by"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select a.fileupload_gid,a.document_name,a.document_path,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                " concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by from ocs_trn_trmuploadocument a" +
                " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<rmdocument_list>();
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
                values.rmhfilename = file_name.ToArray();
                values.rmhfilepath = file_path.ToString();



                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new rmdocument_list
                    {
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        fileupload_gid = dt["fileupload_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),

                    });

                }
                values.rmdocument_list = getDocList;
            }
            dt_datatable.Dispose();
        }

        public void DaGetBAMpendingSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,a.department_name," +
                 " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                 " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                 " a.customer_name,a.customer_gid,operation_status, relationshipmanager_name,transfer_flag,a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source\r\n " +
                 " FROM osd_trn_tbankalert2allocated a where a.allocated_status in ('pending','RH Approval Pending') and brs_flag = 'N' order by a.bankalert2allocated_gid asc";
            }
            else
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,a.department_name," +
                   " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                   " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                   " a.customer_name,a.customer_gid,operation_status, relationshipmanager_name,transfer_flag,a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source\r\n " +
                   " FROM osd_trn_tbankalert2allocated a where a.allocated_status in ('pending','RH Approval Pending') and " +
                   " (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "')) and brs_flag = 'N' " +
                   " order by a.bankalert2allocated_gid asc";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocated_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocated_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    relationshipmanager_name = row["relationshipmanager_name"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                    department_name = row["department_name"].ToString(),
                    kotakAPI_flag = row["KotakAPI_flag"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetBAMtransferSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.transferinitiated_date,'%d-%m-%Y || %h:%i %p') as email_date,a.department_name," +
                   " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                   " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                   " a.customer_name,a.customer_gid,operation_status, relationshipmanager_name,transfer_flag ,transfer_type,if(transferto_name is null,'-',transferto_name) as transferto_name" +
                   " FROM osd_trn_tbankalert2allocated a  WHERE a.allocated_status in ('Pending') and transfer_flag='Y'" +
                   " and transferfrom_name is null order by a.bankalert2allocated_gid desc";
            }
            else
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.transferinitiated_date,'%d-%m-%Y || %h:%i %p') as email_date,a.department_name," +
                   " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                   " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                   " a.customer_name,a.customer_gid,operation_status, relationshipmanager_name,transfer_flag ,transfer_type,if(transferto_name is null,'-',transferto_name) as transferto_name" +
                   " FROM osd_trn_tbankalert2allocated a  WHERE a.allocated_status in ('Pending') and transfer_flag='Y'" +
                   " and transferfrom_name is null and " +
                   " (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " order by a.bankalert2allocated_gid desc";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlerttransfer_list = dt_datatable.AsEnumerable().Select(row => new BankAlerttransfer_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    relationshipmanager_name = row["relationshipmanager_name"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                    transfer_type = row["transfer_type"].ToString(),
                    transferto_name = row["transferto_name"].ToString(),
                    department_name = row["department_name"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetBAMDtlpendingSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " SELECT a.bankalert2notallocated_gid,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,a.ticketref_no,a.department_name," +
                  " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,customer_urn,reason," +
                  " CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')  as aging, " +
                  " a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source FROM osd_trn_tbankalert2notallocated a order by a.bankalert2notallocated_gid desc ";
            }
            else
            {
                msSQL = " SELECT a.bankalert2notallocated_gid,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,a.ticketref_no,a.department_name," +
                  " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,customer_urn,reason," +
                  " CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')  as aging, " +
                  " a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source  FROM osd_trn_tbankalert2notallocated a where " +
                  " (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                  " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                  " order by a.bankalert2notallocated_gid desc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocated_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocated_list
                {
                    bankalert2notallocated_gid = row["bankalert2notallocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    reason = row["reason"].ToString(),
                    aging = row["aging"].ToString(),
                    department_name = row["department_name"].ToString(),
                    kotakAPI_flag = row["kotakAPI_flag"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetBAMOperationpendingSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date, '%d-%m-%Y || %h:%i %p') as email_date,if (assigned_toname is null,'-',assigned_toname) as assigned_toname,a.department_name , " +
                    "  a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status,a.seen_flag ," +
                    " brs_flag,operation_status,customer_urn,customer_gid,customer_name,if (a.operation_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') )  as aging, " +
                   " a.kotakAPI_flag, (CASE WHEN(kotakAPI_flag = 'Y') THEN 'KOTAK-API' " +
                       " WHEN(brs_flag = 'Y') THEN 'BRS'" +
                        " ELSE 'E-mail' " +
                          "  END)  as source,a.regionalheadlevel_name FROM osd_trn_tbankalert2allocated a WHERE a.allocated_status in ('Completed')  " +
                   " and operation_status<>'Completed' order by a.bankalert2allocated_gid asc ";
            }
            else
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date, '%d-%m-%Y || %h:%i %p') as email_date,if (assigned_toname is null,'-',assigned_toname) as assigned_toname,a.department_name , " +
                    "  a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status,a.seen_flag ," +
                    " brs_flag,operation_status,customer_urn,customer_gid,customer_name,if (a.operation_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') )  as aging, " +
                   " a.kotakAPI_flag, (CASE WHEN(kotakAPI_flag = 'Y') THEN 'KOTAK-API' " +
                       " WHEN(brs_flag = 'Y') THEN 'BRS'" +
                        " ELSE 'E-mail' " +
                          "  END)  as source,a.regionalheadlevel_name FROM osd_trn_tbankalert2allocated a WHERE a.allocated_status in ('Completed') and " +
                   " (a.department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid ='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid = '" + employee_gid + "')) " +
                   " and operation_status<>'Completed' order by a.created_date asc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocated_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocated_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    assigned_toname = row["assigned_toname"].ToString(),
                    department_name = row["department_name"].ToString(),
                    kotakAPI_flag = row["kotakAPI_flag"].ToString(),
                    brs_flag = row["brs_flag"].ToString(),
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetBAMRHApprovalpendingSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,a.department_name," +
                 " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                 " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                 " a.customer_name,a.customer_gid,operation_status, relationshipmanager_name,transfer_flag,a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source,a.regionalheadlevel_name " +
                 " FROM osd_trn_tbankalert2allocated a where a.allocated_status in ('RH Approval Pending') and brs_flag = 'N' order by a.bankalert2allocated_gid asc";
            }
            else
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,a.department_name," +
                   " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                   " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging,a.seen_flag,customer_urn," +
                   " a.customer_name,a.customer_gid,operation_status, relationshipmanager_name,transfer_flag,a.kotakAPI_flag,if(a.kotakAPI_flag ='Y','Kotak API','Email') as source,a.regionalheadlevel_name " +
                   " FROM osd_trn_tbankalert2allocated a where a.allocated_status in ('RH Approval Pending') and " +
                   " (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "')) and brs_flag = 'N' " +
                   " order by a.bankalert2allocated_gid asc";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocated_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocated_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    relationshipmanager_name = row["relationshipmanager_name"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                    department_name = row["department_name"].ToString(),
                    kotakAPI_flag = row["KotakAPI_flag"].ToString(),
                    regionalheadlevel_name = row["regionalheadlevel_name"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaPost2AssignOperationTeamandAckComplete(MdlRMStatus values, string employee_gid, string user_gid)
        {
            msSQL = "update osd_trn_tbankalert2allocated set assigned_to ='" + values.assigned_to + "'," +
                 " assigned_toname = '" + values.assigned_toname + "'," +
                 " operation_status ='Assigned',";
            if (values.assigned_remarks == null || values.assigned_remarks == "")
            {
                msSQL += "assigned_remarks='',";
            }
            else
            {
                msSQL += "assigned_remarks='" + values.assigned_remarks.Replace("'", " ") + "',";
            }
            msSQL += " assigned_by='" + employee_gid + "'," +
                "assigned_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("SERQ");
                msSQL = "select ticketref_no from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                string lsticketref_no = objdbconn.GetExecuteScalar(msSQL);
                string lsdepartmentgid = objdbconn.GetExecuteScalar("select businessunit_gid from osd_mst_tbusinessunit where businessunit_name='Business Process'");
                //msGet_RefNo = objcmnfunctions.GetMasterGID("BNK");

                msSQL = " select " +
                  " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by," +
                  "  d.department_name  from " +
                  "  adm_mst_tuser b " +
                  "  left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                  " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                  " where b.user_gid='" + user_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsraised_department = objODBCDatareader["department_name"].ToString();
                    lsRaisedBy = objODBCDatareader["raised_by"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select brs_flag from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                string lsbrs_flag = objdbconn.GetExecuteScalar(msSQL);

                if (lsbrs_flag == "Y")
                {
                    msSQL = " insert into osd_trn_tservicerequest(" +
                        " servicerequest_gid," +
                        " bankalert2allocated_gid," +
                        " customer_gid," +
                        " request_refno," +
                        " activity_name," +
                        " request_title," +
                        " request_description," +
                        " assigned_membergid, " +
                        " assigned_membername, " +
                        " assigned_status, " +
                        " assigned_date, " +
                        " request_status," +
                        " bankalert_flag," +
                         " ticket_source," +
                        " department_gid," +
                        " department_name," +
                        " created_by," +
                        " created_date,raised_by,raised_department)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.bankalert2allocated_gid + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + lsticketref_no + "'," +
                        "'NA'," +
                        "'BRS'," +
                        "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                        "'" + values.assigned_to + "'," +
                        "'" + values.assigned_toname + "'," +
                        "'New'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Allotted'," +
                        "'Y'," +
                        "'BRS'," +
                        "'" + lsdepartmentgid + "'," +
                        "'Business Process'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + lsRaisedBy + "','" + lsraised_department + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " insert into osd_trn_tservicerequest(" +
                    " servicerequest_gid," +
                    " bankalert2allocated_gid," +
                    " customer_gid," +
                    " request_refno," +
                    " activity_name," +
                    " request_title," +
                    " request_description," +
                    " assigned_membergid, " +
                    " assigned_membername, " +
                    " assigned_status, " +
                    " assigned_date, " +
                    " request_status," +
                    " bankalert_flag," +
                    " ticket_source," +
                    " department_gid," +
                    " department_name," +
                    " created_by," +
                    " created_date,raised_by,raised_department)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.bankalert2allocated_gid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + lsticketref_no + "'," +
                    "'NA'," +
                    "'Bank Alert'," +
                    "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                    "'" + values.assigned_to + "'," +
                    "'" + values.assigned_toname + "'," +
                    "'New'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'Allotted'," +
                    "'Y'," +
                    "'Kotak_API'," +
                    "'" + lsdepartmentgid + "'," +
                    "'Business Process'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " '" + lsRaisedBy + "','" + lsraised_department + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                if (mnResult == 0)
                {
                    values.status = false;
                    values.message = "Error Occurred while Raising Service Request..!";
                    return;
                }
                else
                {

                    msSQL = " update osd_trn_tbankalert2allocated  set servicerequest_gid= '" + msGetGid + "'" +
                        " where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = "select document_name,document_path from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("SRDO");

                            msSQL = " insert into osd_trn_tservicereqdocument(" +
                             " servicereqdocument_gid," +
                             " servicerequest_gid, " +
                             " document_name," +
                             " document_path," +
                             " created_by," +
                             " created_date)" +
                             " values(" +
                             "'" + msGetDocumentGid + "'," +
                             "'" + msGetGid + "', " +
                             "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                             "'" + dt["document_path"].ToString() + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                msSQL = "delete from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                }
                dt_datatable.Dispose();

                if (mnResult != 0)
                {
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();

                        }
                        objODBCDatareader.Close();


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.assigned_to + "'";

                        tomailid_list = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                        ccmailid_list = objdbconn.GetExecuteScalar(msSQL);




                        sub = " Escrow Payment Assigned";
                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "The ticket is assigned to you, the details are as follows,<br />";
                        body = body + "<br />";
                        body = body + "<b>Assigned on:</b> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br />";
                        body = body + "<br />";
                        body = body + "Kindly process further and close the ticket so that the RM will get a confirmation.<br />";
                        body = body + "<br />";
                        body = body + "In case of any queries, kindly contact Operations team.<br/>";
                        body = body + "<br />";
                        body = body + "<b>Regards,</b><br/> ";
                        body = body + "<br />";
                        body = body + "Team Business Process.<br /> ";


                        message.From = new MailAddress(ls_username);

                        message.To.Add(tomailid_list);
                        message.CC.Add(ccmailid_list);
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
                            msGetGID = objcmnfunctions.GetMasterGID("ALDB");
                            msSQL = "Insert into osd_trn_tbankalertmailcount( " +
                                                                                          " bankalertmailcount_gid," +
                                                                                          " from_mail," +
                                                                                          " to_mail," +
                                                                                          " cc_mail," +
                                                                                          " mail_status," +
                                                                                          " mail_senddate, " +
                                                                                          " created_by," +
                                                                                          " created_date)" +
                                                                                          " values(" +
                                                                                          "'" + msGetGID + "'," +
                                                                                          "'" + ls_username + "'," +
                                                                                          "'" + tomailid_list + "'," +
                                                                                          "'" + ccmailid_list + "'," +
                                                                                          "'On Ticket Assigning to Operation Person'," +
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

                    values.status = true;
                    values.message = "Service Requests are Raised Successfully..!";


                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }

            values.status = true;
            values.message = "Assigned to Operation team Successfully";


        }
        public void DaPost2OperationTeam(MdlRMStatus values, string employee_gid, string user_gid)
        {
            msSQL = "update osd_trn_tbankalert2allocated set assigned_to ='" + values.assigned_to + "'," +
                 " assigned_toname = '" + values.assigned_toname + "'," +
                 " operation_status ='Assigned',";
            if (values.assigned_remarks == null || values.assigned_remarks == "")
            {
                msSQL += "assigned_remarks='',";
            }
            else
            {
                msSQL += "assigned_remarks='" + values.assigned_remarks.Replace("'", " ") + "',";
            }
            msSQL += " assigned_by='" + employee_gid + "'," +
                "assigned_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("SERQ");
                msSQL = "select ticketref_no from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                string lsticketref_no = objdbconn.GetExecuteScalar(msSQL);
                string lsdepartmentgid = objdbconn.GetExecuteScalar("select businessunit_gid from osd_mst_tbusinessunit where businessunit_name='Business Process'");
                //msGet_RefNo = objcmnfunctions.GetMasterGID("BNK");

                msSQL = " select " +
                  " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by," +
                  "  d.department_name  from " +
                  "  adm_mst_tuser b " +
                  "  left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                  " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                  " where b.user_gid='" + user_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    lsraised_department = objODBCDatareader["department_name"].ToString();
                    lsRaisedBy = objODBCDatareader["raised_by"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select brs_flag from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                string lsbrs_flag = objdbconn.GetExecuteScalar(msSQL);

                if (lsbrs_flag == "Y") 
                {
                    msSQL = " insert into osd_trn_tservicerequest(" +
                        " servicerequest_gid," +
                        " bankalert2allocated_gid," +
                        " customer_gid," +
                        " request_refno," +
                        " activity_name," +
                        " request_title," +
                        " request_description," +
                        " assigned_membergid, " +
                        " assigned_membername, " +
                        " assigned_status, " +
                        " assigned_date, " +
                        " request_status," +
                        " bankalert_flag," +
                         " ticket_source," +
                        " department_gid," +
                        " department_name," +
                        " created_by," +
                        " created_date,raised_by,raised_department)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.bankalert2allocated_gid + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + lsticketref_no + "'," +
                        "'NA'," +
                        "'BRS'," +
                        "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                        "'" + values.assigned_to + "'," +
                        "'" + values.assigned_toname + "'," +
                        "'New'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Allotted'," +
                        "'Y'," +
                        "'BRS'," +
                        "'" + lsdepartmentgid + "'," +
                        "'Business Process'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + lsRaisedBy + "','" + lsraised_department + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else {
                    msSQL = " insert into osd_trn_tservicerequest(" +
                    " servicerequest_gid," +
                    " bankalert2allocated_gid," +
                    " customer_gid," +
                    " request_refno," +
                    " activity_name," +
                    " request_title," +
                    " request_description," +
                    " assigned_membergid, " +
                    " assigned_membername, " +
                    " assigned_status, " +
                    " assigned_date, " +
                    " request_status," +
                    " bankalert_flag," +
                    " ticket_source," +
                    " department_gid," +
                    " department_name," +
                    " created_by," +
                    " created_date,raised_by,raised_department)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.bankalert2allocated_gid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + lsticketref_no + "'," +
                    "'NA'," +
                    "'Bank Alert'," +
                    "'" + values.assigned_remarks.Replace("'", "\\'") + "'," +
                    "'" + values.assigned_to + "'," +
                    "'" + values.assigned_toname + "'," +
                    "'New'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'Allotted'," +
                    "'Y'," +
                    "'Kotak_API'," +
                    "'" + lsdepartmentgid + "'," +
                    "'Business Process'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " '" + lsRaisedBy + "','" + lsraised_department + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
               

                if (mnResult == 0)
                {
                    values.status = false;
                    values.message = "Error Occurred while Raising Service Request..!";
                    return;
                }
                else
                {

                    msSQL = " update osd_trn_tbankalert2allocated  set servicerequest_gid= '" + msGetGid + "'" +
                        " where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = "select document_name,document_path from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("SRDO");

                            msSQL = " insert into osd_trn_tservicereqdocument(" +
                             " servicereqdocument_gid," +
                             " servicerequest_gid, " +
                             " document_name," +
                             " document_path," +
                             " created_by," +
                             " created_date)" +
                             " values(" +
                             "'" + msGetDocumentGid + "'," +
                             "'" + msGetGid + "', " +
                             "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                             "'" + dt["document_path"].ToString() + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                msSQL = "delete from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                    }
                }
                dt_datatable.Dispose();

                if (mnResult != 0)
                {
                    try
                    {

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();

                        }
                        objODBCDatareader.Close();


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();

                        msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.assigned_to + "'";

                        tomailid_list = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                        ccmailid_list = objdbconn.GetExecuteScalar(msSQL);




                        sub = " Escrow Payment Assigned";
                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "The ticket is assigned to you, the details are as follows,<br />";
                        body = body + "<br />";
                        body = body + "<b>Assigned on:</b> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br />";
                        body = body + "<br />";
                        body = body + "Kindly process further and close the ticket so that the RM will get a confirmation.<br />";
                        body = body + "<br />";
                        body = body + "In case of any queries, kindly contact Operations team.<br/>";
                        body = body + "<br />";
                        body = body + "<b>Regards,</b><br/> ";
                        body = body + "<br />";
                        body = body + "Team Business Process.<br /> ";


                        message.From = new MailAddress(ls_username);
                        
                        message.To.Add(tomailid_list);
                        message.CC.Add(ccmailid_list);
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
                            msGetGID = objcmnfunctions.GetMasterGID("ALDB");
                            msSQL = "Insert into osd_trn_tbankalertmailcount( " +
                                                                                          " bankalertmailcount_gid," +
                                                                                          " from_mail," +
                                                                                          " to_mail," +
                                                                                          " cc_mail," +
                                                                                          " mail_status," +
                                                                                          " mail_senddate, " +
                                                                                          " created_by," +
                                                                                          " created_date)" +
                                                                                          " values(" +
                                                                                          "'" + msGetGID + "'," +
                                                                                          "'" + ls_username + "'," +
                                                                                          "'" + tomailid_list + "'," +
                                                                                          "'" + ccmailid_list + "'," +
                                                                                          "'On Ticket Assigning to Operation Person'," +
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

                    values.status = true;
                    values.message = "Service Requests are Raised Successfully..!";


                }

                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }

            values.status = true;
            values.message = "Assigned to Operation team Successfully";
        }
        public void DaPosttranasfe2Assign(MdlRMStatus values, string employee_gid, string user_gid)
        {
            msSQL = "select relationshipmanager_gid, relationshipmanager_name from osd_trn_tbankalert2allocated where " +
                " bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                msSQL = "update osd_trn_tbankalert2allocated set transferfrom_gid ='" + objODBCDatareader["relationshipmanager_gid"].ToString() + "'," +
                " transferfrom_name = '" + objODBCDatareader["relationshipmanager_name"].ToString() + "'," +
                " transferto_gid= '" + values.assigned_to + "'," +
                " transferto_name= '" + values.assigned_toname + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("BTRG");
                msSQL = "insert into osd_trn_ttickettransferlog(" +
                     " transferlog_gid," +
                     " bankalert2allocated_gid," +
                     " relationshipmanager_gid ," +
                     " relationshipmanager_name ," +
                     " updated_by," +
                     " updated_date) values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.bankalert2allocated_gid + "'," +
                     "'" + objODBCDatareader["relationshipmanager_gid"].ToString() + "'," +
                     "'" + objODBCDatareader["relationshipmanager_name"].ToString() + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = "update osd_trn_tbankalert2allocated set relationshipmanager_gid ='" + values.assigned_to + "'," +
                 " relationshipmanager_name = '" + values.assigned_toname + "'," +
               " updated_by='" + user_gid + "'," +
                "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = " Transfer Ticket Assigned Successfully..!";
            }

            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
        public void DaGetOperationStatusDtl(string bankalert2allocated_gid, MdlRMStatus values)
        {
            msSQL = "select a.assigned_remarks,a.assigned_toname,date_format(a.assigned_date,'%d-%m-%Y %h:%i %p') as assigned_date," +
                " concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as assigned_by from osd_trn_tbankalert2allocated a" +
                " left join hrm_mst_temployee b on a.assigned_by=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_toname = objODBCDatareader["assigned_toname"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = "select a.servicereqdocument_gid,a.document_name,a.document_path,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                " concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by from osd_trn_tservicereqdocument a" +
                " left join osd_trn_tservicerequest d on a.servicerequest_gid=d.servicerequest_gid" +
                " left join adm_mst_tuser c on a.created_by=c.user_gid where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<rmdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {

                // Create file list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.rmfilename = file_name.ToArray();
                values.rmfilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new rmdocument_list
                    {
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        fileupload_gid = dt["servicereqdocument_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                    });
                }
                values.rmdocument_list = getDocList;
            }
            dt_datatable.Dispose();
        }
        public void DaGetCount(MdlBankAlertCount values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status in ('pending','RH Approval Pending')";
                values.allocated_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2notallocated ";

                values.notallocated_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status in ('Assigned','Completed') and operation_status<>'Completed'";
                values.operation_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status='Pending' and transfer_flag='Y' and transferfrom_name is null";
                values.transfer_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status='RH Approval Pending'";
                values.rhApprovePending_count = objdbconn.GetExecuteScalar(msSQL);
            }
            else
            {
                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status in ('pending','RH Approval Pending') and " +
                   " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                  " department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))";
                values.allocated_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2notallocated where " +
                         " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                        " department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))";

                values.notallocated_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status in ('Assigned','Completed')  and " +
                         " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                        " department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "')) and operation_status<>'Completed'";
                values.operation_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status='Pending' and transfer_flag='Y' and transferfrom_name is null  and " +
                         " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                        " department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))";
                values.transfer_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from osd_trn_tbankalert2allocated where allocated_status='RH Approval Pending' and " +
                   " (department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                  " department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))";
                values.rhApprovePending_count = objdbconn.GetExecuteScalar(msSQL);
            }
            values.status = true;
        }
        public void DaGetRMTempDelete(MdlBankAlertAllocated values, string employee_gid)
        {
            msSQL = "delete from ocs_trn_trmuploadocument WHERE bankalert2allocated_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
        }
        public void DaGetOperationTempDelete(MdlBankAlertAllocated values, string user_gid)
        {
            msSQL = "delete from osd_tmp_tservicereqdocument WHERE created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
        }
        public void DaDeleteOperationUploadedDoc(string id, uploaddocument objfilename, string user_gid)
        {
            msSQL = " DELETE FROM osd_tmp_tservicereqdocument WHERE tmpservicereqdocument_gid='" + id + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select tmpservicereqdocument_gid,document_name,document_path from osd_tmp_tservicereqdocument " +
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
                objfilename.doufilename = file_name.ToArray();
                objfilename.doufilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        tmp_documentGid = dt["tmpservicereqdocument_gid"].ToString(),
                    });
                    objfilename.upload_list = getdocumentdtlList;
                }
            }

            dt_datatable.Dispose();
        }
        public void DaPosttransfer(string employee_gid, MdlRMStatus values)
        {
            msSQL = "update osd_trn_tbankalert2allocated set transfer_flag='Y',seen_flag='N', transfer_type='" + values.transfer_type + "',";
            if (values.transfer_type == "Ticket Transfer")
            {
                msSQL += "tickettransfer_remarks='" + values.tickettransfer_remarks + "',";
            }
            else
            {
                msSQL += "rmtransfer_remarks='" + values.rmtransfer_remarks + "',";
            }
            msSQL += "transferinitiated_by='" + employee_gid + "'," +
             " transferinitiated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Transfer Initiated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Transfer Initiated Successfully";
            }
        }
        public void DaGetticketTransferLog(MdlOsdTrnBankAlert values, string bankalert2allocated_gid)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) transferedinitiated_by,a.relationshipmanager_name," +
                " date_format(a.transferinitiated_date,'%d-%m-%Y %h:%i %p') as transferinitiated_date,transfer_type," +
                " case when transfer_type='RM Transfer' then rmtransfer_remarks else tickettransfer_remarks end as transfer_remarks from osd_trn_tbankalert2allocated a " +
                 " left join hrm_mst_temployee b on b.employee_gid=a.transferinitiated_by" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where " +
                " a.bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.relationshipmanager_name = objODBCDatareader["relationshipmanager_name"].ToString();
                values.transferedinitiated_by = objODBCDatareader["transferedinitiated_by"].ToString();
                values.transferinitiated_date = objODBCDatareader["transferinitiated_date"].ToString();
                values.transfer_type = objODBCDatareader["transfer_type"].ToString();
                values.transfer_remarks = objODBCDatareader["transfer_remarks"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }
        public void GetAllocatedCount(MdlBankAlertCount values, string employee_gid)
        {
            msSQL = "select count(*) from osd_trn_tbankalert2allocated a where a.allocated_status in ('Pending') and a.relationshipmanager_gid='" + employee_gid + "' and " +
                    "  (transferinitiated_by <>relationshipmanager_gid  or transferinitiated_by is null)";
            values.allocated_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from osd_trn_tbankalert2allocated a where a.allocated_status in ('Pending') and (a.relationshipmanagerlevel_gid='" + employee_gid + "') or (a.drmlevel_gid='" + employee_gid + "') or (a.clustermanagerlevel_gid='" + employee_gid + "') or" +
                   " (a.zonalheadlevel_gid='" + employee_gid + "') or ( a.businessheadlevel_gid='" + employee_gid + "') and " +
                    "  (transferinitiated_by <>relationshipmanager_gid  or transferinitiated_by is null)";
            values.allocatedassigned_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from osd_trn_tbankalert2allocated a where a.transferinitiated_by='" + employee_gid + "' and transfer_flag='Y' ";
            values.transfer_count = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;

            msSQL = " select count(*) from osd_trn_tbankalert2allocated a " +  
                " left join brs_trn_tbanktransactiondetails b on a.ticketref_no = b.banktransc_gid " +
              " where a.allocated_status in ('Pending', 'Completed') and b.taggedmember_gid = '" + employee_gid + "' ";
            values.unreconciliation_count = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
        public void DaGetCustomer2RM(string bankalert2allocated_gid, MdlOsdTrnBankAlert values)
        {
            string lsrelationshipmanager_gid, lscustomer_urn, lsbankrelationship_gid;

            msSQL = "select customer_urn from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            lscustomer_urn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select relationship_manager from ocs_mst_tcustomer where customer_urn='" + lscustomer_urn + "'";
            lsrelationshipmanager_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select relationshipmanager_gid " +
                        " from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            lsbankrelationship_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsrelationshipmanager_gid == lsbankrelationship_gid)
            {
                values.rmupdated_flag = "N";
            }
            else
            {
                msSQL = "select relationshipmgmt_name from ocs_mst_tcustomer where customer_urn='" + lscustomer_urn + "'";
                values.relationshipmanager_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select relationshipmanager_name from osd_trn_tbankalert2allocated where customer_urn='" + lscustomer_urn + "'";
                values.lsrelationshipmanager_name = objdbconn.GetExecuteScalar(msSQL);

                values.rmupdated_flag = "Y";
            }
            values.status = true;
        }

        public void DaPostRM(string employee_gid, MdlRMStatus values)
        {
            msSQL = "select customer_urn from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            string lscustomer_urn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select relationshipmanager_name from osd_trn_tbankalert2allocated where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'";
            string lsrelationshipmanager_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select relationship_manager,relationshipmgmt_name from ocs_mst_tcustomer where customer_urn='" + lscustomer_urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                msSQL = "update osd_trn_tbankalert2allocated set relationshipmanager_name='" + objODBCDatareader["relationshipmgmt_name"].ToString() + "'," +
               " relationshipmanager_gid='" + objODBCDatareader["relationship_manager"].ToString() + "'," +
                 " transferto_gid='" + objODBCDatareader["relationship_manager"].ToString() + "'," +
                     " transferto_name='" + objODBCDatareader["relationshipmgmt_name"].ToString() + "'," +
               "  transferfrom_name='" + objODBCDatareader["relationship_manager"].ToString() + "' where bankalert2allocated_gid='" + values.bankalert2allocated_gid + "'" +
               " and allocated_status='Pending'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msGetGid = objcmnfunctions.GetMasterGID("CUPL");
                msSQL = "insert into osd_trn_tbankalertrmlog(" +
                    " bankalertrmlog," +
                    " bankalert2allocated_gid," +
                    " relationshipmanager_gid," +
                    " created_by," +
                    " created_date) values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.bankalert2allocated_gid + "'," +
                    "'" + lsrelationshipmanager_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();
            if (mnResult != 0)
            {


                values.status = true;
                values.message = "Rm Information Updated Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating";
            }
        }

        public bool DaGetSeenHistory(string employee_gid, string user_gid, MdlBankNotification values)
        {
            msSQL = " SELECT bankalert2allocated_gid FROM osd_trn_tbankalert2allocated  WHERE allocated_status in ('Completed')  and seen_flag='N' order by bankalert2allocated_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "UPDATE osd_trn_tbankalert2allocated SET seen_flag='Y' WHERE bankalert2allocated_gid='" + dr_datarow["bankalert2allocated_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();
            msSQL = " SELECT bankalert2allocated_gid FROM osd_trn_tbankalert2allocated  WHERE allocated_status in ('Pending') and transfer_flag = 'Y'  and seen_flag='N' order by bankalert2allocated_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "UPDATE osd_trn_tbankalert2allocated SET seen_flag='Y' WHERE bankalert2allocated_gid='" + dr_datarow["bankalert2allocated_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();
            msSQL = " SELECT bankalert2notallocated_gid FROM osd_trn_tbankalert2notallocated   WHERE  seen_flag='N' order by bankalert2notallocated_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "UPDATE osd_trn_tbankalert2notallocated SET seen_flag='Y' WHERE bankalert2notallocated_gid='" + dr_datarow["bankalert2notallocated_gid"].ToString() + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();
            if (mnResult != 0)
            {
                values.status = true;
                return true;

            }
            else
            {
                values.status = false;
                return false;

            }
        }

        public void DaGetBankalertNotification(string employee_gid, string user_gid, MdlBankNotification values)
        {
            //         


            int departmentnameman = Convert.ToInt16(objdbconn.GetExecuteScalar("select count(*) from osd_mst_tactivedepartment2manager where manager_gid ='" + employee_gid + "' and department_name='Business Process'"));
            int departmentnamemem = Convert.ToInt16(objdbconn.GetExecuteScalar("select count(*) from osd_mst_tactivedepartment2member where member_gid ='" + employee_gid + "' and department_name='Business Process'"));

            if (departmentnamemem != 0 || departmentnameman != 0)
            {
                values.display = "true";
                msSQL = " SELECT bankalert2allocated_gid FROM osd_trn_tbankalert2allocated  WHERE allocated_status = 'Completed'  and seen_flag='N' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.allocated_new = "true";
                }
                else
                {
                    objODBCDatareader.Close();
                    values.allocated_new = "false";
                }

                msSQL = " SELECT bankalert2allocated_gid FROM osd_trn_tbankalert2allocated  WHERE allocated_status in ('Pending') and transfer_flag = 'Y'  and seen_flag='N' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.allocatedtransfer_new = "true";
                }
                else
                {
                    objODBCDatareader.Close();
                    values.allocatedtransfer_new = "false";
                }


                msSQL = " SELECT bankalert2notallocated_gid FROM osd_trn_tbankalert2notallocated  WHERE seen_flag='N' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.notallocated_new = "true";
                }
                else
                {
                    objODBCDatareader.Close();
                    values.notallocated_new = "false";
                }


                msSQL = " SELECT privilege_gid FROM adm_mst_tprivilege  WHERE user_gid='" + user_gid + "' and module_gid = 'OSDBAMMAL'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.privilege = "true";
                }
                else
                {
                    objODBCDatareader.Close();
                    values.privilege = "false";
                }

            }
            else
            {
                values.display = "false";
            }

        }

        public void DaGetServiceprivilege(string user_gid, string employee_gid, MdlBankNotification values)
        {
            //string lsdepartmentnamemem, lsdepartmentnameman;
            int departmentnameman = Convert.ToInt16(objdbconn.GetExecuteScalar("select count(*) from osd_mst_tactivedepartment2manager where manager_gid ='" + employee_gid + "' and department_name='Business Process'"));
            int departmentnamemem = Convert.ToInt16(objdbconn.GetExecuteScalar("select count(*) from osd_mst_tactivedepartment2manager where manager_gid ='" + employee_gid + "' and department_name='Business Process'"));
            //lsdepartmentnameman = objdbconn.GetExecuteScalar("select department_name from osd_mst_tactivedepartment2manager where manager_gid ='" + employee_gid + "' and department_name='Business Process'");
            //lsdepartmentnamemem = objdbconn.GetExecuteScalar("select department_name from osd_mst_tactivedepartment2member where member_gid ='" + employee_gid + "' and department_name='Business Process'");
            if (departmentnamemem != 0 || departmentnameman != 0)
            {
                msSQL = " SELECT privilege_gid FROM adm_mst_tprivilege  WHERE user_gid='" + user_gid + "' and module_gid='OSDTRNTIR'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    values.privilege = "true";
                }
                else
                {
                    objODBCDatareader.Close();
                    values.privilege = "false";
                }

            }
            else
            {
                values.privilege = "false";
            }
        }

        public void DaGetBAMOperationCompletedSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,if(assigned_toname is null,'-',assigned_toname) as assigned_toname,a.department_name," +
                   " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status,a.seen_flag," +
                   " operation_status,customer_urn,customer_gid,customer_name,if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') ) as aging" +
                   " FROM osd_trn_tbankalert2allocated a WHERE operation_status='Completed' order by a.bankalert2allocated_gid desc ";
            }
            else
            {
                msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date,if(assigned_toname is null,'-',assigned_toname) as assigned_toname,a.department_name," +
                   " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status,a.seen_flag," +
                   " operation_status,customer_urn,customer_gid,customer_name,if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins') )  as aging" +
                   " FROM osd_trn_tbankalert2allocated a WHERE " +
                   " (a.department_gid in (select department_gid from  osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                   " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                   " and operation_status='Completed' order by a.bankalert2allocated_gid desc ";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertAllocated_list = dt_datatable.AsEnumerable().Select(row => new BankAlertAllocated_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    assigned_toname = row["assigned_toname"].ToString(),
                    department_name = row["department_name"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaGetCompletedSummary(MdlBankAlertCompleted values, string employee_gid)
        {
            msSQL = " SELECT a.bankalert2allocated_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y || %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status," +
                    " if(a.operation_status ='Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins'))  as aging,a.seen_flag,customer_urn," +
                    " a.customer_name,a.customer_gid,operation_status,if(assigned_toname is null,'-',assigned_toname) as assigned_toname,transfer_flag , if(a.kotakAPI_flag ='Y','Kotak API','Email') as source ,a.kotakAPI_flag " +
                    " FROM osd_trn_tbankalert2allocated a" +
                    " WHERE a.allocated_status in ('Completed') and a.relationshipmanager_gid='" + employee_gid + "' and " +
                    "  (transferinitiated_by <>relationshipmanager_gid  or transferinitiated_by is null) order by a.bankalert2allocated_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertCompleted_list = dt_datatable.AsEnumerable().Select(row => new BankAlertCompleted_list
                {
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    assigned_toname = row["assigned_toname"].ToString(),
                    transfer_flag = row["transfer_flag"].ToString(),
                    kotakAPI_flag = row["kotakAPI_flag"].ToString(),
                }).ToList();

            }
            dt_datatable.Dispose();
        }
        public void DaGetUnReconciliationSummary(MdlBankAlertAllocated values, string employee_gid)
        {
            //msSQL = " SELECT a.bankalert2allocated_gid,a.brs_flag,a.ticketref_no,date_format(a.email_date, '%d-%m-%Y || %h:%i %p') as email_date, " +
            //        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
            //         " if (a.operation_status = 'Completed',CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date))), ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging, " +
            //         " a.seen_flag,a.customer_urn,a.created_by, b.trn_date,b.transc_balance,b.banktransc_gid,b.taggedmember_gid,b.taggedmember_name,b.bank_gid,c.bank_name,c.branch_name, " +
            //         " a.customer_name,a.customer_gid,a.operation_status,if (a.brs_flag = 'Y','BRS','Email') as source, " +
            //         " DATE_FORMAT(f.created_date, '%d-%m-%Y %h:%i %p') as assigned_date, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as assigned_by , f.taggedmember_name as assigned_to " +
            //         " FROM osd_trn_tbankalert2allocated a " +
            //         " left join brs_trn_tbanktransactiondetails  b on a.ticketref_no = b.banktransc_gid " +
            //         " left join brs_mst_tbank c on b.bank_gid = c.bank_gid " +
            //         " left join brs_trn_ttagemployee f on a.ticketref_no = f.banktransc_gid " +
            //         " left join adm_mst_tuser g on f.created_by= g.user_gid " +
            //        " WHERE a.allocated_status in ('Pending', 'Completed') and b.taggedmember_gid = '" + employee_gid + "' order by a.created_date desc ";
            //" where tagged_status='Assigned' and c.taggedmember_gid ='" + employee_gid + "' order by a.created_date desc ";

            msSQL = " SELECT a.bankalert2allocated_gid,a.brs_flag,a.ticketref_no," +
                  " date_format(a.email_date, '%d-%m-%Y || %h:%i %p') as email_date, " +
                  " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
                  " a.allocated_status,  if (a.operation_status = 'Completed'," +
                  " CONCAT(FLOOR((DATEDIFF(a.operationstatus_updated_date, a.created_date))), ' days '," +
                  " MOD(HOUR(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 24), ' Hrs '," +
                  " MINUTE(TIMEDIFF(a.operationstatus_updated_date, a.created_date)), 'Mins'),CONCAT(FLOOR((DATEDIFF(now(), a.created_date)))," +
                  " ' days ', MOD(HOUR(TIMEDIFF(now(), a.created_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.created_date)), 'Mins')) as aging, " +
                  " a.seen_flag,a.customer_urn,a.created_by, b.trn_date,b.transc_balance,b.banktransc_gid," +
                  " b.taggedmember_gid,b.bank_gid,c.bank_name,c.branch_name,  a.customer_name," +
                  " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as assigned_by , " +
                  " a.customer_gid,a.operation_status,if (a.brs_flag = 'Y','BRS','Email') as source ," +
                  " b.taggedmember_name as assigned_to," +
                  " case when DATE_FORMAT(f.transfer_date, '%d-%m-%Y %h:%i %p') is null then  " +
                  " date_format(a.email_date, '%d-%m-%Y || %h:%i %p') else  " +
                  " (select DATE_FORMAT(max(te.transfer_date), '%d-%m-%Y %h:%i %p') from brs_trn_ttransferemployee te " +
                  " where te.banktransc_gid = b.banktransc_gid) end as 'assigned_date', " +
                   " case when f.created_by is null then (select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttagemployee te " +
                "left join adm_mst_tuser g on te.created_by = g.user_gid " +
                " where te.banktransc_gid = b.banktransc_gid group by te.banktransc_gid) else " +
               " (select  concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) from brs_trn_ttransferemployee te " +
                " left join adm_mst_tuser g on te.created_by = g.user_gid " +
                " where te.banktransc_gid = b.banktransc_gid and te.transfer_date = (select max(transfer_date) " +
                " from brs_trn_ttransferemployee f   where f.banktransc_gid = b.banktransc_gid))end as  'taggedmember_name' " +
                  " FROM osd_trn_tbankalert2allocated a   left join brs_trn_tbanktransactiondetails  b on" +
                  " a.ticketref_no = b.banktransc_gid " +
                  " left join brs_mst_tbank c on b.bank_gid = c.bank_gid  " +
                  " left join brs_trn_ttagemployee e on b.banktransc_gid = e.banktransc_gid  " +
                  " left join adm_mst_tuser g on c.created_by= g.user_gid  " +
                  " left join brs_trn_ttransferemployee f on b.banktransc_gid = f.banktransc_gid " +
                  " WHERE a.allocated_status in ('Pending', 'Completed')  " +
                  " and b.taggedmember_gid = '" + employee_gid + "' group by a.ticketref_no " +
                  "  order by a.created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.BankAlertUnreconciliation_list = dt_datatable.AsEnumerable().Select(row => new BankAlertUnreconciliation_list
                {
                    brs_flag = row["brs_flag"].ToString(),
                    bankalert2allocated_gid = row["bankalert2allocated_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    allocated_status = row["allocated_status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    customer_gid = row["customer_gid"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    operation_status = row["operation_status"].ToString(),
                    taggedmember_gid = row["taggedmember_gid"].ToString(),
                    taggedmember_name = row["taggedmember_name"].ToString(),
                    created_by = row["created_by"].ToString(),
                    bank_name = row["bank_name"].ToString(),
                    branch_name = row["branch_name"].ToString(),
                    banktransc_gid = row["banktransc_gid"].ToString(),
                    trn_date = row["trn_date"].ToString(),
                    transc_balance = row["transc_balance"].ToString(),
                    email_date = row["email_date"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),

                    //trn_date = row["trn_date"].ToString(),
                    //custref_no = row["custref_no"].ToString(),
                    //bank_name = row["bank_name"].ToString(),
                    //branch_name = row["branch_name"].ToString(),
                    //acc_no = row["acc_no"].ToString(),
                    //transc_balance = row["transc_balance"].ToString(),
                    //tagged_status = row["tagged_status"].ToString(),
                }).ToList();
            }
            dt_datatable.Dispose();
        }
        public void DaUnReconDocumentUpload(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string document_name = httpRequest.Form["document_name"];
            String lspath;
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/BRSDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(lspath)))
                System.IO.Directory.CreateDirectory(lspath);


            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("URDU");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsbanktransc_gid = httpRequest.Form["banktransc_gid"].ToString();
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        MemoryStream ms = new MemoryStream();
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                       
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            return;
                        }

                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/RMDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msSQL = "Insert into osd_trn_tunreconciliationdocument( " +
                                                                              " fileupload_gid," +
                                                                              " banktransc_gid," +
                                                                              " document_name," +
                                                                              " document_path," +
                                                                              " created_by," +
                                                                              " created_date)" +
                                                                              " values(" +
                                                                              "'" + msdocument_gid + "'," +
                                                                              "'" + lsbanktransc_gid + "'," +
                                                                              "'" + document_name.Replace("'", "") + "'," +
                                                                              "'" + lspath + msdocument_gid + FileExtension + "'," +
                                                                              "'" + employee_gid + "'," +
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
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occured";
                }
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.Message;
            }
        }
        public void DaGetUnReconUploadedDoc(MdlcDocList objfileDtls, string employee_gid, string banktransc_gid)
        {
            msSQL =" SELECT a.fileupload_gid,a.banktransc_gid,a.document_name,a.document_path, " +
              " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date ,  " +
             " concat(c.user_firstname, ' ', c.user_lastname, '||', c.user_code) as created_by " +
              " FROM osd_trn_tunreconciliationdocument  a " +
               " left join hrm_mst_temployee b on  a.created_by = b.employee_gid " +
             " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " WHERE banktransc_gid='" + banktransc_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlDocDetails>();
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
                objfileDtls.filename = file_name.ToArray();
                objfileDtls.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getDocList.Add(new MdlDocDetails
                    {
                        id = dt["fileupload_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_by = dt["created_by"].ToString(),
                        created_date= dt["created_date"].ToString()
                    }) ;
                }
                objfileDtls.MdlDocDetails = getDocList;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteUnReconUploadedDoc(string fileupload_gid, result objResult)
        {
            msSQL = " DELETE FROM osd_trn_tunreconciliationdocument WHERE fileupload_gid='" + fileupload_gid + "'";
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

        }
        public void DaPostUnconciliationStatusUpdation(MdlRMStatus values, string user_gid, string employee_gid)
        {
            try
            {

                msGetGID = objcmnfunctions.GetMasterGID("BUTA");
                msSQL = "Insert into osd_trn_tbrsunreconciliation2allocated( " +
                " brs2allocated_gid," +
                " banktransc_gid," +
                " unreconciliation_status," +
                " updation_remarks," +
                " brs_status," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                "'" + values.banktransc_gid + "'," +
                "'" + values.cbounreconciliation_status.Replace("'", "") + "'," +
                "'" + values.updation_remarks.Replace("'", "") + "'," +
                "'Completed'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update osd_trn_tbrsunreconciliation2allocated set fileupload_gid='" + values.fileupload_gid + "'  " +
                                      " where banktransc_gid = '" + values.banktransc_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "update osd_trn_tbankalert2allocated set " +
               " seen_flag='N', " +
               " allocated_status='Completed',";
                    if (values.cbounreconciliation_status == null || values.cbounreconciliation_status == "")
                    {
                        msSQL += "rm_status='',";
                    }
                    else
                    {
                        msSQL += "rm_status='" + values.cbounreconciliation_status.Replace("'", "") + "',";
                    }
                    if (values.updation_remarks == null || values.updation_remarks == "")
                    {
                        msSQL += "rm_remarks='',";
                    }
                    else
                    {
                        msSQL += "rm_remarks='" + values.updation_remarks.Replace("'", "") + "',";
                    }
                    msSQL += " updated_by='" + employee_gid + "'," +
                        "updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ticketref_no='" + values.banktransc_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //   msSQL = " SELECT a.banktransc_gid,a.banktransc_refno,a.bankconfig_gid,a.bank_gid,b.bank_name,b.branch_name,b.acc_no,b.custref_no,a.reconcildoc_gid,DATE_FORMAT(a.trn_date, '%d-%m-%Y %h:%i %p') as trn_date, DATE_FORMAT(a.value_date, '%d-%m-%Y %h:%i %p') as value_date," +
                    // "DATE_FORMAT(a.payment_date, '%d-%m-%Y %h:%i %p') as payment_date , a.transact_particulars, a.debit_amt, a.credit_amt,a.transact_val, a.remarks, a.cr_dr, a.chq_no, a.created_by,  DATE_FORMAT(a.created_date, '%d-%m-%Y %h:%i %p') as created_date ," +
                    // "  a.transc_balance FROM brs_trn_tbanktransactiondetails a  " +
                    // " left join brs_mst_tbank b  on a.bank_gid =b.bank_gid  " +
                    //     " where a.banktransc_gid='" + values.banktransc_gid + "' order by a.created_date desc ";

                    //   objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    //   if (objODBCDatareader.HasRows == true)
                    //   {
                    //        lscustref_no = objODBCDatareader["custref_no"].ToString();
                    //      lsbank_name = objODBCDatareader["bank_name"].ToString();

                    //   }
                    //   string lsdepartmentgid = objdbconn.GetExecuteScalar("select businessunit_gid from osd_mst_tbusinessunit where businessunit_name='Business Process'");
                    //   MSGETGID = objcmnfunctions.GetMasterGID("ALDB");
                    //   msSQL = "Insert into osd_trn_tbankalert2allocated( " +
                    // " bankalert2allocated_gid," +
                    // " customer_name," +
                    // " customer_urn," +
                    // " customer_gid," +
                    // " ticketref_no," +
                    // " allocated_status," +
                    // " rm_remarks," +
                    //  "rm_status,"+
                    // " updated_by," +
                    //  " updated_date," +
                    //  " created_by," +
                    //  " mapping_to,"+
                    //  "department_gid,"+
                    //  "department_name," +
                    //  "email_date," +
                    // " created_date)" +
                    // " values(" +
                    //"'" + MSGETGID + "'," +
                    // "'" + lsbank_name + "'," +
                    //  "'" + lscustref_no + "'," +
                    //   "'" + lsbank_name + "'," +
                    //   "'" + values.banktransc_gid + "'," +
                    //    "'Completed'," +
                    //"'" + values.updation_remarks + "'," +
                    // "'" + values.cbounreconciliation_status + "'," +                 
                    // "'" + employee_gid + "'," +
                    // "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    //   "'" + user_gid + "'," +
                    //  "'BRS'," +
                    //  "'" + lsdepartmentgid + "'," +
                    // "'Business Process'," +                  
                    //  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    // "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //   mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }
            values.status = true;
            values.message = "Status Updated Successfully";
        }
        public void DaGetunreconAllocatedDetail(string bankalert2allocated_gid, string customer_gid, string customer_urn, MdlOsdTrnunreconBankAlert values, string employee_gid)
        {
            values.employee_gid = employee_gid;

            msSQL = " SELECT a.ticketref_no,date_format(a.email_date, '%d-%m-%Y || %h:%i %p') as email_date, " + 
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.allocated_status, " +
                     " a.brs_flag,a.customer_urn,a.kotakAPI_flag, b.trn_date,b.transc_balance,b.taggedmember_gid,b.taggedmember_name,b.bank_gid,c.bank_name,c.branch_name,if (a.brs_flag = 'Y','BRS','Email') as source, " +
                     " a.customer_name,a.customer_gid,a.operation_status,c.acc_no,b.cr_dr,b.transact_val,b.chq_no,b.value_date,b.payment_date,b.transact_particulars,b.debit_amt,b.credit_amt,b.remarks " +
                    " FROM  osd_trn_tbankalert2allocated a " +
                     " left join brs_trn_tbanktransactiondetails b on a.ticketref_no = b.banktransc_gid " +
                     " left join brs_mst_tbank c on b.bank_gid = c.bank_gid " +
                    " where a.bankalert2allocated_gid='" + bankalert2allocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.source = objODBCDatareader["source"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.allocated_status = objODBCDatareader["allocated_status"].ToString();
                values.brs_flag = objODBCDatareader["brs_flag"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.trn_date = objODBCDatareader["trn_date"].ToString();
                values.transc_balance = objODBCDatareader["transc_balance"].ToString();
                values.taggedmember_gid = objODBCDatareader["taggedmember_gid"].ToString();
                values.bank_gid = objODBCDatareader["bank_gid"].ToString();
                values.bank_name = objODBCDatareader["bank_name"].ToString();
                values.branch_name = objODBCDatareader["branch_name"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.operation_status = objODBCDatareader["operation_status"].ToString();
                values.acc_no = objODBCDatareader["acc_no"].ToString();
                values.cr_dr = objODBCDatareader["cr_dr"].ToString();
                values.transact_val = objODBCDatareader["transact_val"].ToString();
                values.chq_no = objODBCDatareader["chq_no"].ToString();
                values.value_date = objODBCDatareader["value_date"].ToString();
                values.payment_date = objODBCDatareader["payment_date"].ToString();
                values.transact_particulars = objODBCDatareader["transact_particulars"].ToString();
                values.debit_amt = objODBCDatareader["debit_amt"].ToString();
                values.credit_amt = objODBCDatareader["credit_amt"].ToString();
                values.remarks = objODBCDatareader["remarks"].ToString();
                values.allocated_status = objODBCDatareader["allocated_status"].ToString();
                values.ticketref_no= objODBCDatareader["ticketref_no"].ToString();
                values.kotakAPI_flag = objODBCDatareader["kotakAPI_flag"].ToString();
            }
            objODBCDatareader.Close();
        }
        //public void DaGetAssigntoEmployee(MdlEmployee objemployee)
        //{
        //    try
        //    {
        //        msSQL = " select employee_gid,user_gid,department_gid from adm_mst_ttoken WHERE token = '" + token + "'";
              

        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //        var get_employee = new List<employee_list>();
        //        if (dt_datatable.Rows.Count != 0)
        //        {
        //            objemployee.employee_list = dt_datatable.AsEnumerable().Select(row =>
        //              new employee_list
        //              {
        //                  employee_gid = row["employee_gid"].ToString(),
        //                  employee_name = row["employee_name"].ToString()
        //              }
        //            ).ToList();
        //        }
        //        dt_datatable.Dispose();
        //        objemployee.status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        objemployee.status = false;
        //    }


        //}

    }



}