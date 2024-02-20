using ems.foundation.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Web;

namespace ems.foundation.DataAccess
{
    public class DaFndMstCustomerApprovingMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetcustomerapprovingemployee_gid, lsremarks, lscustomerapproving_value, lslms_code, lsbureau_code, lscustomerapproving_code;
        int mnResult;

        int k;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsemployee_gid, lscustomer_name, lcampaign_status, lsTo2members, lscampaign_type, lsname, lscampaign_name, lscampaign_apr, lscampaign_remarks, lsBccmail_id, lscreated_by, lstomembers, lsdescription, lscc2members, lstonames, lsauditcreation_gid, lscreated_date, frommail_id, lscc_mail, strBCC, lsbcc_mail;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;



        public void DaGetCustomerApproving(MdlFndMstCustomerApprovingMaster values)
        {
            try
            {
                msSQL = " SELECT a.customerapproving_gid,a.customer_name,a.employee_name,a.employee_gid,a.customerapproving_code,a.approver_gid,a.approver_name,a.remarks, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM fnd_mst_tcustomerapproving a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.customerapproving_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerapproving_list = new List<customerapproving_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerapproving_list.Add(new customerapproving_list
                        {
                            customerapproving_gid = (dr_datarow["customerapproving_gid"].ToString()),
                            customer_name = (dr_datarow["customer_name"].ToString()),

                            customerapproving_code = (dr_datarow["customerapproving_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            approver_name = (dr_datarow["approver_name"].ToString()),
                        });
                    }
                    values.customerapproving_list = getcustomerapproving_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateCustomerApproving(MdlFndMstCustomerApprovingMaster values, string employee_gid)
        {
            msSQL = "select customer_name from fnd_mst_tcustomerapproving where customer_name = '" + values.customer_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Customer Name Already Exist";
            }
            else
            {
                if (values.lms_code == null || values.lms_code == "")
                {
                    lslms_code = "";
                }
                else
                {
                    lslms_code = values.lms_code.Replace("'", "");
                }
                if (values.bureau_code == null || values.bureau_code == "")
                {
                    lsbureau_code = "";
                }
                else
                {
                    lsbureau_code = values.bureau_code.Replace("'", "");
                }

                if (values.customerapproving_code == null || values.customerapproving_code == "")
                {
                    lscustomerapproving_code = "";
                }
                else
                {
                    lscustomerapproving_code = values.customerapproving_code.Replace("'", "");
                }
                if (values.remarks == null || values.remarks == "")
                {
                    lsremarks = "";
                }
                else
                {
                    lsremarks = values.remarks.Replace("'", "");
                }

                msGetGid = objcmnfunctions.GetMasterGID("FCAP");
                lscustomerapproving_code = objcmnfunctions.GetMasterGID("IFDAP");

                msSQL = " insert into fnd_mst_tcustomerapproving(" +
                        " customerapproving_gid ," +
                        " customer_gid ," +
                        " customer_name," +
                        " customerapproving_code," +
                        " approver_gid,"+
                        " approver_name,"+
                        " lms_code," +
                        " bureau_code," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.customer_gid.Replace("'", "") + "'," +
                        "'" + values.customer_name.Replace("'", "") + "'," +
                        "'" + lscustomerapproving_code + "'," +
                        "'" + values.approver_gid + "'," +
                        "'" + values.approver_name + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + lsremarks + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //for (var i = 0; i < values.employee.Count; i++)
                //{
                //    msGetcustomerapprovingemployee_gid = objcmnfunctions.GetMasterGID("FD2E");
                //    msSQL = "Insert into fnd_mst_tcustomerapproving2employee( " +
                //           " customerapproving2employee_gid, " +
                //           " customerapproving_gid," +
                //           " employee_gid," +
                //           " employee_name," +
                //           " created_by," +
                //           " created_date)" +
                //           " values(" +
                //           "'" + msGetcustomerapprovingemployee_gid + "'," +
                //           "'" + msGetGid + "'," +
                //           "'" + values.employee[i].employee_gid + "'," +
                //           "'" + values.employee[i].employee_name + "'," +
                //           "'" + employee_gid + "'," +
                //           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //}

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Customer Approving Added successfully";

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

                    msSQL = " select  group_concat(distinct a.approver_gid)  as To2members, group_concat(distinct a.approver_name)  as employee_name, a.customer_name, a.created_date, concat(i.user_firstname,' ',i.user_lastname,' / ',i.user_code) as created_by  from fnd_mst_tcustomerapproving a" +
                            //" left join hrm_mst_temployee b on d.campaign_approver = b.employee_gid" +
                            " left join hrm_mst_temployee f on a.created_by = f.employee_gid" +
                            //" left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " left join adm_mst_tuser i on i.user_gid = f.user_gid  " +
                        " where a.customerapproving_gid ='" + msGetGid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsname = objODBCDatareader["created_by"].ToString();
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lsTo2members = objODBCDatareader["To2members"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
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
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    //msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    //cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "RE: Customer Approval ";
                    body = "Dear Team,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "A customer has been created for Approval.  <br />";
                    body = body + "<br />";
                    body = body + "<b> Customer :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Mapped employee :</b> " + HttpUtility.HtmlEncode(lsemployee_name) + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Audit Department :</b> " + lsauditdepartment_name + "<br />";
                    //body = body + "<br />";
                    //body = body + "<b>Checkpoint Group :</b> " + lscheckpointgroup_name + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsname);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["Foundationbcc"].ToString();

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

                    //if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    //{
                    //    lsCCReceipients = cc_mailid.Split(',');
                    //    if (cc_mailid.Length == 0)
                    //    {
                    //        message.CC.Add(new MailAddress(cc_mailid));
                    //    }
                    //    else
                    //    {
                    //        foreach (string CCEmail in lsCCReceipients)
                    //        {
                    //            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    //        }
                    //    }
                    //}

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


                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }

        }

        public void DaEditCustomerApproving(string customerapproving_gid, MdlFndMstCustomerApprovingMaster values)
        {
            msSQL = " select customerapproving_gid,customer_name,customerapproving_code,customer_gid,lms_code,remarks,approver_name,approver_gid, bureau_code, status as Status from fnd_mst_tcustomerapproving " +
                    " where customerapproving_gid='" + customerapproving_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customerapproving_gid = objODBCDatareader["customerapproving_gid"].ToString();
                values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customerapproving_code = objODBCDatareader["customerapproving_code"].ToString();
                values.approver_gid = objODBCDatareader["approver_gid"].ToString();
                values.approver_name = objODBCDatareader["approver_name"].ToString();
                values.lms_code = objODBCDatareader["lms_code"].ToString();
                values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                values.remarks = objODBCDatareader["remarks"].ToString();
                values.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();
            //msSQL = " select customerapproving2employee_gid,employee_gid,employee_name from fnd_mst_tcustomerapproving2employee " +
            //     " where customerapproving_gid='" + customerapproving_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getemployeeList = new List<employee>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getemployeeList.Add(new employee
            //        {
            //            customerapproving2employee_gid = dt["customerapproving2employee_gid"].ToString(),
            //            employee_gid = dt["employee_gid"].ToString(),
            //            employee_name = dt["employee_name"].ToString(),
            //        });
            //        values.employee = getemployeeList;
            //    }
            //}
            //dt_datatable.Dispose();
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_manageremployee = new List<employeeem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employeeem_list = dt_datatable.AsEnumerable().Select(row =>
                  new employeeem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            values.status = true;


        }
        public void DaUpdateCustomerApproving(string employee_gid, MdlFndMstCustomerApprovingMaster values)
        {
            //msSQL = "select customer_name from fnd_mst_tcustomerapproving where customer_name = '" + values.customer_name.Replace("'", "\\'") + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == true)
            //{
            //    values.status = false;
            //    values.message = "Customer Name Already Exist";
            //}
            //else
            //{

                msSQL = "select updated_by, updated_date,customer_name from fnd_mst_tcustomerapproving where customerapproving_gid ='" + values.customerapproving_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                    string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                    if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("FDAL");

                        msSQL = " insert into fnd_mst_tcustomerapprovinglog (" +
                               " customerapprovinglog_gid, " +
                               " customer_name," +
                               " updated_by," +
                               " updated_date) " +
                               " values (" +
                               " '" + msGetGid + "'," +
                               " '" + values.customerapproving_gid + "'," +
                               " '" + values.customer_name.Replace("'", "") + "'," +
                               " '" + employee_gid + "'," +
                               " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
           // msSQL = " select customer_gid from fnd_mst_tcustomer where customer_name='" + values.customer_name + "'";
           //string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as approver_name from hrm_mst_temployee a left join adm_mst_tuser b on b.user_gid = a.user_gid where employee_gid='" + values.approver_gid + "'";
            string lsapprover_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " update fnd_mst_tcustomerapproving set " +
                     " customer_name='" + values.customer_name.Replace("'", "") + "'," +
                     //" customer_gid='" + lscustomer_gid + "'," +
                     " customerapproving_code='" + values.customerapproving_code + "'," +
                     " approver_name='" + lsapprover_name + "'," +
                     " approver_gid='" + values.approver_gid + "'," +
                     " lms_code='" + values.lms_code + "'," +
                     " bureau_code='" + values.bureau_code + "'," +
                      " remarks='" + values.remarks + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where customerapproving_gid='" + values.customerapproving_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = " delete from fnd_mst_tcustomerapproving2employee where customerapproving_gid ='" + values.customerapproving_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //if (mnResult != 0)
                //{
                //    for (var i = 0; i < values.employee.Count; i++)
                //    {
                //        msGetcustomerapprovingemployee_gid = objcmnfunctions.GetMasterGID("FD2E");
                //        msSQL = "Insert into fnd_mst_tcustomerapproving2employee( " +
                //               " customerapproving2employee_gid, " +
                //               " customerapproving_gid," +
                //               " employee_gid," +
                //               " employee_name," +
                //               " created_by," +
                //               " created_date)" +
                //               " values(" +
                //               "'" + msGetcustomerapprovingemployee_gid + "'," +
                //               "'" + values.customerapproving_gid + "'," +
                //               "'" + values.employee[i].employee_gid + "'," +
                //               "'" + values.employee[i].employee_name + "'," +
                //               "'" + employee_gid + "'," +
                //               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //    }
                //}
            
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Customer Approving Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaInactiveCustomerApproving(MdlFndMstCustomerApprovingMaster values, string employee_gid)
        {
            msSQL = " update fnd_mst_tcustomerapproving set status='" + values.rbo_status + "'" +
                    " where customerapproving_gid='" + values.customerapproving_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("FAIL");

                msSQL = " insert into fnd_mst_tcustomerapprovinginactivelog (" +
                      " customerapprovinginactivelog_gid, " +
                      " customerapproving_gid," +
                      " customerapproving_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.customerapproving_gid + "'," +
                      " '" + values.customerapproving_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Customer Approving Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Customer ApprovingActivated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteCustomerApproving(string customerapproving_gid, string employee_gid, MdlFndMstCustomerApprovingMaster values)
        {

            msSQL = " select customerapproving_name from fnd_mst_tcustomerapproving where customerapproving_gid='" + customerapproving_gid + "'";
            lscustomerapproving_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from fnd_mst_tcustomerapproving where customerapproving_gid='" + customerapproving_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Customer Approving Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("FADL");
                msSQL = " insert into fnd_mst_tcustomerapprovingdeletelog(" +
                         "customerapprovingdeletelog_gid, " +
                         "customerapproving_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + customerapproving_gid + "', " +
                         "'Customer Approving'," +
                         "'" + lscustomerapproving_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaCustomerApprovingInactiveLogview(string customerapproving_gid, MdlFndMstCustomerApprovingMaster values)
        {
            try
            {
                msSQL = " SELECT a.customerapproving_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM fnd_mst_tcustomerapprovinginactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.customerapproving_gid ='" + customerapproving_gid + "' order by a.customerapprovinginactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerapproving_list = new List<customerapproving_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerapproving_list.Add(new customerapproving_list
                        {
                            customerapproving_gid = (dr_datarow["customerapproving_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.customerapproving_list = getcustomerapproving_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCustomerApprovingMaker(MdlFndMstCustomerApprovingMaster values)
        {
            try
            {

                msSQL = "select a.employee_name,a.employee_gid from fnd_mst_tcustomerapproving2employee a " +
                   " left join fnd_mst_tcustomerapproving b on a.customerapproving_gid = b.customerapproving_gid where b.customerapproving_name ='Maker'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerapprovingmaker_list = new List<customerapprovingmaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerapprovingmaker_list.Add(new customerapprovingmaker_list
                        {
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.customerapprovingmaker_list = getcustomerapprovingmaker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetCustomerApprovingChecker(MdlFndMstCustomerApprovingMaster values)
        {
            try
            {
                msSQL = "select a.employee_name,a.employee_gid from fnd_mst_tcustomerapproving2employee a " +
                   " left join fnd_mst_tcustomerapproving b on a.customerapproving_gid = b.customerapproving_gid where b.customerapproving_name ='Checker'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerapprovingchecker_list = new List<customerapprovingchecker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerapprovingchecker_list.Add(new customerapprovingchecker_list
                        {
                            customerapproving_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.customerapprovingchecker_list = getcustomerapprovingchecker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetCustomerApprovingApprover(MdlFndMstCustomerApprovingMaster values)
        {
            try
            {
                msSQL = "select a.employee_name,a.employee_gid from fnd_mst_tcustomerapproving2employee a " +
                                 " left join fnd_mst_tcustomerapproving b on a.customerapproving_gid = b.customerapproving_gid where b.customerapproving_name ='Approver'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerapprovingapprover_list = new List<customerapprovingapprover_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerapprovingapprover_list.Add(new customerapprovingapprover_list
                        {
                            customerapproving2employee_gid = (dr_datarow["employee_gid"].ToString()),
                            approver_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.customerapprovingapprover_list = getcustomerapprovingapprover_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetCustomerApprovingChecker(string checklistmaster_gid, MdlFndMstCustomerApprovingMaster values)
        {
            try
            {
                msSQL = "select (a.customerapprovingchecker_name )as employee_name,(a.customerapproving_gid) as employee_gid from fnd_trn_tcustomerapprovingcreation a " +
                        " left join fnd_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid where a.checklistmaster_gid = '" + checklistmaster_gid + "'" +
                         " union " +
                        " select (b.customerapprovingchecker_name) as employee_name,(b.customerapproving_gid) as employee_gid from fnd_trn_tcustomerapprovingcreation a " +
                        " left join fnd_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid where a.checklistmaster_gid = '" + checklistmaster_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomerapprovingchecker_list = new List<customerapprovingchecker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomerapprovingchecker_list.Add(new customerapprovingchecker_list
                        {
                            customerapproving_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.customerapprovingchecker_list = getcustomerapprovingchecker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetEmployeeName(string customerapproving_gid, CustomerApprovingemployee values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from fnd_mst_tcustomerapproving2employee " +
                  " where customerapproving_gid='" + customerapproving_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select customerapproving_name from fnd_mst_tcustomerapproving" +
                 " where customerapproving_gid='" + customerapproving_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customerapproving_name = objODBCDatareader["customerapproving_name"].ToString();
            }
            objODBCDatareader.Close();

        }
    }
}