using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.ecms.Models;
using ems.utilities.Functions;
using System.Net;
using System.Net.Mail;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// customerAlertGenerate Controller Class containing API methods for accessing the  DataAccess class DaCustomerAlertGenerate
    /// To get mail History View, Generate mail content, Mail mangament, Get customer mail details, Send mail , History of send mails
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaCustomerAlertGenerate
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
      
        OdbcDataReader objODBCDataReader;
        DataTable dt_datatable, dt_table;
        string msSQL;
        DateTime lspenalitysendDate;
        string lspenality_date;
        string deferral_gid = string.Empty;
        string msGetGidGenerate, msGetGidDeferral;
        string msGethistoryGid, lsdeferraltype_gid, lsrecord_id;
        string lsdeferral_name, lsdeferral_status, lsaging, lsdue_date;
        string lsremarks;
        int mnResult, i;
        string frommail_id, sub, tomail_id, contactperson, customer_name, body, ls_server, ls_username, ls_password, lscontent = string.Empty;
        string lsrelationshipmgmt_name, lsrelationshipmgmt_gid, lscluster_manager_name, lsclustermanager_gid = string.Empty;
        string lsbusinesshead_name, lsbusinesshead_gid, lszonal_name, lszonal_gid, lscreditmgmt_name, lscreditmgmt_gid = string.Empty;
        string sanction_refno, sanction_date, address, address2 = string.Empty;
        string lsvertical_code, lsmobileno = string.Empty;
        int ls_port;
        string[] bcc;
        string cc;
        string bcc_mailid = string.Empty;
        public void DaGetCustomerGenerate(MdlCustomerAlert values, string employee_gid)
        {
          msSQL = " select customername,email,contactperson,address,address2 from ocs_mst_tcustomer " +
                  " where customer_gid='" + values.customer_gid + "'";
           
            objODBCDataReader = objdbconn .GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                tomail_id = objODBCDataReader["email"].ToString();
                contactperson = objODBCDataReader["contactperson"].ToString();
                customer_name = objODBCDataReader["customername"].ToString();
                address = objODBCDataReader["address"].ToString();
                address2 = objODBCDataReader["address2"].ToString();
            }
            objODBCDataReader.Close();




            lscontent = values.template_content;
            lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
            lscontent = lscontent.Replace("customer_name", customer_name);
            //lscontent = lscontent.Replace("sanction_refno", sanction_refno);
            //lscontent = lscontent.Replace("sanction_date", sanction_date);
            lscontent = lscontent.Replace("Customer", customer_name + "<br/>" + address + "<br/>" + address2 + "<br/>" + "Mail: " + tomail_id);

            body = lscontent;



            msSQL = " select customeralert_gid from ocs_trn_tcustomeralertgenerate" +
                " where customer_gid='" + values.customer_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                msGetGidGenerate = objODBCDataReader["customeralert_gid"].ToString();
                msSQL = "update ocs_trn_tcustomeralertgenerate set" +
                        " template_content='" + body + "'," +
                        " mail_status = 'Generated'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " updated_by='" + employee_gid + "'" +
                        " where customer_gid='" + values.customer_gid + "'";
                objODBCDataReader.Close();
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

            }
            else
            {
                objODBCDataReader.Close();

                msGetGidGenerate = objcmnfunctions .GetMasterGID ("CAGI");

                msSQL = " insert into ocs_trn_tcustomeralertgenerate(" +
                        " customeralert_gid," +
                        " customer_gid," +
                        " template_content," +
                        " mail_status," +
                        " created_date," +
                        " created_by)" +
                        " values(" +
                        "'" + msGetGidGenerate + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + body + "'," +
                        "'Generated'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);




            }


            msSQL = " delete from ocs_trn_tcustomeralertdeferral " +
                    " where customer_gid='" + values.customer_gid + "'";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);




            foreach (string i in values.deferral_gid)
            {

                msGetGidDeferral = objcmnfunctions.GetMasterGID("CADG");

                msSQL = " insert into ocs_trn_tcustomeralertdeferral(" +
                        " customeralert_deferralgid," +
                        " customeralert_gid," +
                        " customer_gid," +
                        " deferral_gid)" +
                        " values(" +
                        "'" + msGetGidDeferral + "'," +
                        "'" + msGetGidGenerate + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + i + "')";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

            }


            msSQL = " select distinct b.sanction_refno,b.sanction_date" +
                   " from ocs_trn_tcustomeralertdeferral a" +
                   " left join  ocs_trn_tdeferral b on a.deferral_gid = b.deferral_gid" +
                   " left join ocs_trn_tdeferral2loan c on b.loan_gid = c.loan_gid" +
                   " where a.customeralert_gid = '" + msGetGidGenerate + "' group by b.loan_gid";
          
            objODBCDataReader = objdbconn .GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                sanction_refno = sanction_refno + "," + " " + objODBCDataReader["sanction_refno"].ToString();
                sanction_date = sanction_date + "," + " " + objODBCDataReader["sanction_date"].ToString();

            }
            objODBCDataReader.Close();


            body = body.Replace("sanction_refno", sanction_refno.TrimEnd(',').TrimStart(','));
            body = body.Replace("sanction_date", sanction_date.TrimEnd(',').TrimStart(','));

            msSQL = " update ocs_trn_tcustomeralertgenerate set " +
                    " template_content='" + body + "'" +
                    " where customeralert_gid='" + msGetGidGenerate + "'";
            mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);
           
            if (mnResult != 0)
            {
                values .status = true;
                values .message = "success";
            }
            else
            {
                values.status = false ;
                values.message = "failure";
            }

        }

        public void DaGetmailManagement(MdlCustomerAlert objCutomerAlert)
        {
            try
            {
                msSQL = " select a.customeralert_gid,a.customer_gid, b.customer_gid,b.customer_code,a.mail_status," +
                   " concat(b.customer_code,'/',b.customername) as customername,b.contactperson, " +
                   " if(a.updated_date is null,date_format(a.created_date,'%d-%m-%Y %h:%i:%s'),date_format(a.updated_date,'%d-%m-%Y %h:%i:%s')) as generated_date," +
                   " (select count(p.customer_gid) from ocs_trn_tcustomermail p where p.customer_gid=a.customer_gid group by p.customer_gid) as mail_count," +
                   " case when b.zonal_head = '' then 'NA' else b.zonal_name end as zonal_head,b.vertical_code," +
                   " case when b.business_head = '' then 'NA' else b.businesshead_name end as business_head, " +
                   " case when b.cluster_manager_gid = '' then 'NA' else b.cluster_manager_name end as cluster_manager, " +
                   " case when b.relationship_manager = '' then 'NA' else b.relationshipmgmt_name end as relationship_manager ," +
                   " case when b.creditmanager_gid = '' then 'NA' else b.creditmgmt_name end as creditmgmt_name " +
                   " from ocs_trn_tcustomeralertgenerate a " +
                   " left  join ocs_mst_tcustomer b on b.customer_gid = a.customer_gid" +
                   " order by a.customeralert_gid desc ";

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
                            contactperson = (dr_datarow["contactperson"].ToString()),
                            vertical_code = (dr_datarow["vertical_code"].ToString()),
                            zonalGid = (dr_datarow["zonal_head"].ToString()),
                            businessHeadGid = (dr_datarow["business_head"].ToString()),
                            relationshipMgmtGid = (dr_datarow["relationship_manager"].ToString()),
                            creditmanagerName = (dr_datarow["creditmgmt_name"].ToString()),
                            clustermanagerGid = (dr_datarow["cluster_manager"].ToString()),
                            generated_date = (dr_datarow["generated_date"].ToString()),
                            mail_status = (dr_datarow["mail_status"].ToString()),
                            mail_count = (dr_datarow["mail_count"].ToString()),
                        });
                    }
                    objCutomerAlert.customermail_list = getmail;
                }
                dt_datatable.Dispose();

                objCutomerAlert.status = true;

            }
            catch
            {
                objCutomerAlert.status = false;
            }

           }

        public void DaGetcustomerdetails(string customeralert_gid, mailalert values)
        {
            try
            {
                msSQL = " select b.customeralert_gid,a.customer_gid,a.vertical_code,a.customer_code,a.customername,b.template_content, " +
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



                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.customeralert_gid = objODBCDataReader["customeralert_gid"].ToString();
                    values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                    values.customercode = objODBCDataReader["customer_code"].ToString();
                    values.customername = objODBCDataReader["customername"].ToString();
                    values.vertical_code = objODBCDataReader["vertical_code"].ToString();
                    values.contactperson = objODBCDataReader["contactperson"].ToString();
                    values.addressline1edit = objODBCDataReader["address"].ToString();
                    values.addressline2edit = objODBCDataReader["address2"].ToString();
                    values.mobileNoedit = objODBCDataReader["mobileno"].ToString();
                    values.businessHeadGid = objODBCDataReader["business_head"].ToString();
                    values.zonalGid = objODBCDataReader["zonal_head"].ToString();

                    values.clustermanagerGid = objODBCDataReader["cluster_manager"].ToString();

                    values.relationshipMgmtGid = objODBCDataReader["relationship_manager"].ToString();

                    values.creditmanagerName = objODBCDataReader["creditmgmt_name"].ToString();
                    values.content = objODBCDataReader["template_content"].ToString();

                }
                objODBCDataReader.Close();
                body = "In case of any clarification / requirements, Kindly be in touch with Samunnati’s Relationship Manager";
                values.content_below = body;


                msSQL = " select b.record_id,b.tracking_type,b.deferral_catagory,b.tracking_type,date_format(b.due_date,'%d-%m-%Y') as due_date, " +
                        " case when m.approval_status='Closed' then '-' else b.aging end as aging," +
                        " case when m.approval_status='ReOpen' then 'Pending' else  m.approval_status end as approval_status,b.customer_remarks, " +
                        " case when b.customer_remarks<>'' then b.customer_remarks " +
                        " when m.approval_remarks <> '' then m.approval_remarks else b.remarks end as remarks," +
                        " case when m.approval_status='Closed' then 'Closed' else b.deferral_status end as deferral_status," +
                        " case when b.tracking_type='Deferral' then b.deferral_name else b.covenanttype_name end as deferral_name " +
                        " from ocs_trn_tcustomeralertdeferral a " +
                        " left join ocs_trn_tdeferral b on a.deferral_gid=b.deferral_gid " +
                        " join ocs_trn_tdeferralapproval m on m.deferral_gid = b.deferral_gid" +
                        " where a.customeralert_gid='" + customeralert_gid + "' and (b.deferral_status='Expired' or b.deferral_status='Live')  and (b.aging>=90 or b.aging>=60)";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_deferral = new List<mailalert_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_deferral.Add(new mailalert_list
                        {
                            due_date = (dr_datarow["due_date"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            tracking_type = (dr_datarow["tracking_type"].ToString()),
                            deferral_name = (dr_datarow["deferral_name"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            customer_remarks = (dr_datarow["remarks"].ToString()),
                            deferral_category = (dr_datarow["deferral_catagory"].ToString())

                        });
                    }
                    values.mailalert_list = get_deferral;


                }

                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
         
        }

        public void DaPostsendMail(mailalert values, string employee_gid)
        {
            try
            {

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDataReader = objdbconn .GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    frommail_id = objODBCDataReader["company_mail"].ToString();
                    ls_server = objODBCDataReader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDataReader["pop_port"]);
                    ls_username = objODBCDataReader["pop_username"].ToString();
                    ls_password = objODBCDataReader["pop_password"].ToString();

                }
                objODBCDataReader.Close();


                msSQL = "select email_id from hrm_mst_tdepartment where department_name like '%Credit Administration%'";
                bcc = objdbconn .GetExecuteScalar(msSQL).Split(',');


                msSQL = " select a.vertical_code, a.customername,a.email,b.employee_emailid,a.contactperson,a.address,a.address2," +
                    " a.mobileno,a.creditmanager_gid,a.zonal_head,a.business_head,a.cluster_manager_gid,a.relationship_manager," +
                     " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                    " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_name," +
                    " case when a.business_head = '' then 'NA' else a.businesshead_name end as businesshead_name, " +
                    " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager_name, " +
                    " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationshipmgmt_name " +
                    " from ocs_mst_tcustomer a" +
                    " left join hrm_mst_temployee b on a.relationship_manager=b.employee_gid" +
                    " where a.customer_gid='" + values.customer_gid + "'";
                
                objODBCDataReader = objdbconn .GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    tomail_id = objODBCDataReader["email"].ToString();
                    contactperson = objODBCDataReader["contactperson"].ToString();
                    customer_name = objODBCDataReader["customername"].ToString();
                    address = objODBCDataReader["address"].ToString();
                    address2 = objODBCDataReader["address2"].ToString();
                    lsvertical_code = objODBCDataReader["vertical_code"].ToString();
                    lsmobileno = objODBCDataReader["mobileno"].ToString();

                    lszonal_gid = objODBCDataReader["zonal_head"].ToString();
                    lszonal_name  = objODBCDataReader["zonal_name"].ToString();

                    lscreditmgmt_gid  = objODBCDataReader["creditmanager_gid"].ToString();
                    lscreditmgmt_name  = objODBCDataReader["creditmgmt_name"].ToString();

                    lsbusinesshead_gid  = objODBCDataReader["business_head"].ToString();
                    lsbusinesshead_name  = objODBCDataReader["businesshead_name"].ToString();

                    lsclustermanager_gid  = objODBCDataReader["cluster_manager_gid"].ToString();
                    lscluster_manager_name  = objODBCDataReader["cluster_manager_name"].ToString();

                    lsrelationshipmgmt_gid  = objODBCDataReader["relationship_manager"].ToString();
                    lsrelationshipmgmt_name  = objODBCDataReader["relationshipmgmt_name"].ToString();

                    cc = objODBCDataReader["employee_emailid"].ToString();
                }
                objODBCDataReader.Close();




                sub = "Compliance of Terms & Condition for the credit facilities granted to (" + HttpUtility.HtmlEncode(customer_name) + ") by Samunnati Financial Intermediation & Services Private Limited (”Samunnati”) ";


                //lscontent = HttpUtility.HtmlEncode(values.content);


                body = "<span style=\"font-family: Times New Roman;font-size:12pt;\" >" + values.content + "</span>";
                //body =  "Dear" + " " + "<b>" + contactperson + "</b>" + "," + "";


                body = body + "<table style='border:1px solid #060606;' style=\"font-family: Times New Roman;font-size:12pt;\" border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + ">";
                body = body + "<tr style='color: #fff;background: #00008B;text-align:center;'> ";
                body = body + "<td style=\"width:7%;\"><b> S. No </b></td> ";

                //body = body + "<td style='width:100px;'><b> Branch </b></td> ";
                //body = body + "<td style='width:100px;'><b> State </b></td> ";
                //body = body + "<td><b> Vertical </b></td> ";
                body = body + "<td style=\"width:20%;\"><b> Record ID </b></td> ";
                //body = body + "<td style='width:100px;'><b> Tracking Type </b></td> ";
                body = body + "<td style=\"width:25%;\"><b> Deferral Type </b> </td>";
                body = body + "<td style=\"width:15%;\"><b> Due Date </b></td> ";
                //body = body + "<td><b> Status </b> </td>";
                body = body + "<td style=\"width:90%;\"> <b> Remarks </b> </td>";
                //body = body + "<td style='width:100px;'> <b> Deferral Status </b> </td>";
                body = body + "</tr>";

                msGetGidDeferral = objcmnfunctions.GetMasterGID("SMTC");

                msSQL = " select b.deferral_gid,b.customer_name,b.branch_name,c.state,b.record_id, " +
                        " b.vertical_code, " +
                        " b.tracking_type,b.deferral_catagory,b.tracking_type, " +
                        " case when b.tracking_type='Deferral' then b.deferral_name else b.covenanttype_name end as deferral_name, " +
                        " date_format(b.due_date,'%d-%m-%Y') as due_date,d.approval_status, " +
                        " case when d.approval_status='Closed' then '-' else b.aging end as aging," +
                        " case when d.approval_status='Closed' then 'Closed' else b.deferral_status end as deferral_status," +
                        " case when b.customer_remarks<>'' then b.customer_remarks " +
                        " when d.approval_remarks <> '' then d.approval_remarks else b.remarks end as remarks " +
                        " from ocs_trn_tcustomeralertdeferral a " +
                        " left join ocs_trn_tdeferral b on a.deferral_gid=b.deferral_gid " +
                        " left join ocs_mst_tcustomer c on c.customer_gid=b.customer_gid " +
                        " left join ocs_trn_tdeferralapproval d on d.deferral_gid=b.deferral_gid " +
                        " where a.customeralert_gid='" + values.customeralert_gid + "'";

                dt_table = objdbconn .GetDataTable (msSQL);
                deferral_gid = "";
                for (int loopCount = 0; loopCount < dt_table.Rows.Count; loopCount++)
                {
                    i = loopCount + 1;
                    body = body + "<tr style='text-align:center;'>";
                    body = body + "<td> " + i + "</td>";

                    //body = body + "<td> " + " " + dt_table.Rows[loopCount]["branch_name"] + " " + "</td>";
                    //body = body + "<td> " + dt_table.Rows[loopCount]["state"] + "</td>";
                    //body = body + "<td> " + dt_table.Rows[loopCount]["vertical_code"] + "</td>";
                    body = body + "<td> " + dt_table.Rows[loopCount]["record_id"] + "</td>";
                    //body = body + "<td> " + dt_table.Rows[loopCount]["tracking_type"] + "</td>";
                    body = body + "<td> " + dt_table.Rows[loopCount]["deferral_name"] + "</td>";
                    body = body + "<td> " + dt_table.Rows[loopCount]["due_date"] + "</td>";
                    //body = body + "<td> " + dt_table.Rows[loopCount]["approval_status"] + "</td>";
                    body = body + "<td> " + dt_table.Rows[loopCount]["remarks"] + "</td>";
                    //body = body + "<td> " + dt_table.Rows[loopCount]["deferral_status"] + "</td>";
                    body = body + " </tr>";

                    deferral_gid = deferral_gid + "," + dt_table.Rows[loopCount]["deferral_gid"];
                    string lsdeferral_gid = dt_table.Rows[loopCount]["deferral_gid"].ToString();
                    lsrecord_id = dt_table.Rows[loopCount]["record_id"].ToString();
                    lsdeferral_name = dt_table.Rows[loopCount]["deferral_name"].ToString();
                    lsdeferral_status = dt_table.Rows[loopCount]["deferral_status"].ToString();
                    lsaging = dt_table.Rows[loopCount]["aging"].ToString();
                    lsdue_date = dt_table.Rows[loopCount]["due_date"].ToString();
                    lsremarks = dt_table.Rows[loopCount]["remarks"].ToString();

                    msSQL = " update ocs_trn_tcustomeralertdeferral set" +
                            " penalityalert_flag='Y'," +
                            " status='Sent'" +
                            " where deferral_gid='" + dt_table.Rows[loopCount]["deferral_gid"] + "' and customer_gid='" + values.customer_gid + "'";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                    msSQL = " update ocs_trn_tdeferral set" +
                            " customeralertmail_status='Sent'" +
                            " where deferral_gid='" + dt_table.Rows[loopCount]["deferral_gid"] + "'";
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                    msGethistoryGid = objcmnfunctions.GetMasterGID("CUAH");

                    msSQL = " select case when deferraltype_gid <> '' then deferraltype_gid else covenanttype_gid end as deferraltype_gid " +
                            " from ocs_trn_tdeferral where deferral_gid ='" + lsdeferral_gid + "'";
                    lsdeferraltype_gid = objdbconn .GetExecuteScalar(msSQL);

                    msSQL = " insert into ocs_trn_thistorycustomeralert(" +
                               " history_customeralertgid," +
                               " customermail_gid,"+
                               " customeralert_gid," +
                               " customer_gid, " +
                               " deferral_gid, " +
                               " record_id, " +
                               " deferraltype_gid, " +
                               " deferral_name, " +
                               " deferral_status, " +
                               " aging," +
                               " due_date," +
                               " deferral_remarks," +
                               " penalityalert_status," +
                               " created_by, " +
                               " created_date)" +
                               " values(" +
                               "'" + msGethistoryGid + "'," +
                               "'" + msGetGidDeferral +"'," +
                               "'" + values.customeralert_gid + "'," +
                               "'" + values.customer_gid + "'," +
                               "'" + lsdeferral_gid + "'," +
                               "'" + lsrecord_id + "'," +
                               "'" + lsdeferraltype_gid + "'," +
                               "'" + lsdeferral_name + "'," +
                               "'" + lsdeferral_status + "'," +
                               "'" + lsaging + "'," +
                               "'" + lsdue_date + "'," +
                               "'" + lsremarks + "'," +
                               "'Y'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
 
                    mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                }
                dt_table.Dispose();

                body = body + "</table>";
                body = body + "<br /><span style=\"font-family: Times New Roman;font-size:12pt;\" >";
                body = body + "In case of any clarification / requirements, Kindly be in touch with Samunnati’s Relationship Manager";
                body = body + "<br/>";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanking You,";
                body = body + "<br/>";
                body = body + "<br/>";
                body = body + "Yours Sincerely," + "<br/>";
                body = body + " Samunnati Financial Intermediation & Services Pvt Ltd </span>";

                deferral_gid = deferral_gid.TrimEnd(',');
                deferral_gid = deferral_gid.TrimStart(',');

                bcc_mailid = "";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail_id));
                // message.Bcc.Add(bcc);
                message.CC.Add(cc);
                if (bcc.Length > 0)
                {
                    foreach (string bccEmail in bcc)
                    {
                        bcc_mailid = bcc_mailid + ","+ bccEmail;
                        message.Bcc.Add(new MailAddress(bccEmail)); //Adding Multiple CC email Id
                    }
                }
                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);


                bcc_mailid = bcc_mailid.TrimEnd(',');
                bcc_mailid = bcc_mailid.TrimStart(',');

                msSQL = " insert into ocs_trn_tcustomermail(" +
                        " customermail_gid," +
                        " customeralert_gid," +
                        " customer_gid, " +
                        " customer_code, " +
                        " customer_name, " +
                        " contactperson,"+
                        " vertical_code,"+
                        " mobileno,"+
                        " address1,"+
                        " address2,"+
                        " relationshipmgmt_name,"+
                        " relationshipmgmt_gid,"+
                        " cluster_manager_name,"+
                        " clustermanager_gid,"+
                        " businesshead_name,"+
                        " businesshead_gid,"+
                        " zonal_name,"+
                        " zonal_gid,"+
                        " creditmgmt_name,"+
                        " creditmgmt_gid,"+
                        " frommail_id, " +
                        " tomail_id, " +
                        " cc_mailid,"+
                        " bcc_mailid,"+
                        " deferral_gid, " +
                        " content, " +
                        " sent_by, " +
                        " sent_date)" +
                        " values(" +
                        "'" + msGetGidDeferral + "'," +
                        "'" + values.customeralert_gid + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + values.customercode + "'," +
                        "'" + values.customername + "'," +
                        "'"+ contactperson +"',"+
                        "'"+lsvertical_code +"',"+
                        "'"+lsmobileno +"',"+
                        "'"+address +"',"+
                        "'"+address2 +"',"+
                        "'"+ lsrelationshipmgmt_name +"',"+
                        "'"+ lsrelationshipmgmt_gid +"',"+
                        "'"+ lscluster_manager_name +"',"+
                        "'"+lsclustermanager_gid   +"',"+
                        "'"+lsbusinesshead_name +"',"+
                        "'"+lsbusinesshead_gid +"',"+
                        "'"+lszonal_name +"',"+
                        "'"+lszonal_gid +"',"+
                        "'"+lscreditmgmt_name +"',"+
                        "'" + lscreditmgmt_gid +"',"+
                        "'" + ls_username + "'," +
                        "'" + tomail_id + "'," +
                        "'" + cc +"',"+
                        "'" + bcc_mailid + "',"+
                        "'" + deferral_gid + "'," +
                        "'" + values.content + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tcustomeralertgenerate set mail_status='Sent'," +
                       " mail_sentdate ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where customeralert_gid='" + values.customeralert_gid + "'";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                lspenalitysendDate = DateTime.Today.AddDays(15);

                lspenality_date = Convert.ToDateTime(lspenalitysendDate).ToString("yyyy-MM-dd");
                msSQL = " update ocs_trn_tcustomeralertgenerate set penality_alertdate='" + lspenality_date + "'" +
                  " where customeralert_gid='" + values.customeralert_gid + "'";
                mnResult = objdbconn .ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "success";
                
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
               
            }

        }


        public void DaGetmailHistory(mdlmailHistory objCutomerAlert, string customer_gid)
        {
            try
            {
                msSQL = " select a.customermail_gid,a.customeralert_gid,a.customer_gid,a.customer_code,a.customer_name, " +
                  " date_format(a.sent_date,'%d-%m-%Y %h:%i:%s %p') as sent_date,concat(c.user_code,'/',c.user_firstname,' ',c.user_lastname) as sent_by " +
                  " from ocs_trn_tcustomermail a " +
                  " left join hrm_mst_temployee b on a.sent_by=b.employee_gid " +
                  " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                  " where a.customer_gid='" + customer_gid + "' group by a.customermail_gid ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail = new List<mailhistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail.Add(new mailhistory_list
                        {
                            customermail_gid = (dr_datarow["customermail_gid"].ToString()),
                            customeralert_gid = (dr_datarow["customeralert_gid"].ToString()),
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            customercode = (dr_datarow["customer_code"].ToString()),
                            customername = (dr_datarow["customer_name"].ToString()),
                            sent_by = (dr_datarow["sent_by"].ToString()),
                            sent_date = (dr_datarow["sent_date"].ToString()),
                        });
                    }
                    objCutomerAlert.mailhistory_list = getmail;
                }
                dt_datatable.Dispose();
                objCutomerAlert.status = true;
             
            }
            catch
            {
                objCutomerAlert.status = false ;
            }

          
        }

        public void DaGetmailHistoryView(mdlmailHistory objCutomerAlert, string customermail_gid)
        {
            try
            {
                var getmail = new List<mailhistorydeferral_list>();
                msSQL = " select a.customer_gid,a.deferral_gid,a.customer_code,a.customer_name," +
                       "  a.content,a.vertical_code," +
                       "  case when a.address1 <> '' then a.address1 else '-' end as address,a.address2," +
                       "  case when a.mobileno <> '' then a.mobileno else '-' end as mobileno," +
                       "  case when a.contactperson <> '' then a.contactperson else '-' end as contactperson," +
                       " case when a.creditmgmt_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                       "  case when a.zonal_gid = '' then 'NA' else a.zonal_name end as zonal_head," +
                       "  case when a.businesshead_gid = '' then 'NA' else a.businesshead_name end as business_head," +
                        " case when a.clustermanager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager," +
                        " case when a.relationshipmgmt_gid = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager" +
                        " from ocs_trn_tcustomermail a  " +
                      " where a.customermail_gid='" + customermail_gid + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    objCutomerAlert.customer_gid = objODBCDataReader["customer_gid"].ToString();
                    objCutomerAlert.customercode = objODBCDataReader["customer_code"].ToString();
                    objCutomerAlert.customername = objODBCDataReader["customer_name"].ToString();
                    objCutomerAlert.vertical_code = objODBCDataReader["vertical_code"].ToString();

                    objCutomerAlert.businessHeadGid = objODBCDataReader["business_head"].ToString();
                    objCutomerAlert.zonalGid = objODBCDataReader["zonal_head"].ToString();

                    objCutomerAlert.clustermanagerGid = objODBCDataReader["cluster_manager"].ToString();

                    objCutomerAlert.relationshipMgmtGid = objODBCDataReader["relationship_manager"].ToString();

                    objCutomerAlert.creditmanagerName = objODBCDataReader["creditmgmt_name"].ToString();

                    objCutomerAlert.content = objODBCDataReader["content"].ToString();
                    deferral_gid = objODBCDataReader["deferral_gid"].ToString();

                    objCutomerAlert.contactperson = objODBCDataReader["contactperson"].ToString();
                    objCutomerAlert.addressline1edit = objODBCDataReader["address"].ToString();
                    objCutomerAlert.addressline2edit = objODBCDataReader["address2"].ToString();
                    objCutomerAlert.mobileNoedit = objODBCDataReader["mobileno"].ToString();

                }
                objODBCDataReader.Close();

                msSQL = " select customer_gid,deferral_gid,record_id,deferral_name,deferral_status,aging,due_date,deferral_remarks" +
                       " from ocs_trn_thistorycustomeralert" +
                       " where customermail_gid='" + customermail_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethistory = new List<mailhistorydeferral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethistory.Add(new mailhistorydeferral_list
                        {
                            customer_gid = (dr_datarow["customer_gid"].ToString()),
                            deferral_gid = (dr_datarow["deferral_gid"].ToString()),
                            record_id = (dr_datarow["record_id"].ToString()),
                            deferral_type = (dr_datarow["deferral_name"].ToString()),
                            deferral_status = (dr_datarow["deferral_status"].ToString()),
                            due_date = (dr_datarow["due_date"].ToString()),
                            remarks = (dr_datarow["deferral_remarks"].ToString()),
                            aging = (dr_datarow["aging"].ToString()),
                        });
                    }
                    objCutomerAlert.mailhistorydeferral_list = gethistory;
                }
                dt_datatable.Dispose();
                objCutomerAlert.status = true;

            }
            catch
            {
                objCutomerAlert.status = true;
            }
           
        }
    }
}