using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.osd.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnBankMail
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_table;
        string msGETGIDRef, msSQL, MSGETGID, msgetmail_gid, lsURN;
        int mnResult, ls_port;
        OdbcDataReader objOdbcDataReader, objodbcdatareader;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, tomailid_list, ccmail_id, ccmailid_list, body, sub, lscontent;
        public void DaPostMailContent(Mailcontent values, string bodyText)
        {
            try
            {
                values = JsonConvert.DeserializeObject<Mailcontent>(bodyText);

                msSQL = "SELECT bankalertemail_gid FROM osd_trn_tbankalert WHERE  message_id='" + values.message_id + "'";
                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objOdbcDataReader.HasRows == false)
                {
                    objOdbcDataReader.Close();

                    if (values.attachment_status == "True")
                    {
                        string[] fileextension_splits = values.file_name.Split('.');

                        if (fileextension_splits[1] == "pdf")
                        {
                            try
                            {
                                string[] filename_splits = values.file_name.Split('_');
                                string lsfilename = filename_splits[0] + filename_splits[1] + filename_splits[2];

                                if (lsfilename == "E-CollectionInwardAdvice")
                                {
                                    msgetmail_gid = objcmnfunctions.GetMasterGID("BALT");

                                    //string lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/samunnati/" + "OSD/BankAlert/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/Inbox/" + msgetmail_gid + "/";
                                    string lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/samunnati/" + "OSD/BankAlert/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/Inbox/" + msgetmail_gid + "/";
                                    {
                                        if ((!System.IO.Directory.Exists(lspath)))
                                            System.IO.Directory.CreateDirectory(lspath);
                                    }
                                    var filePath = Path.Combine(lspath, values.file_name);
                                    byte[] bytes = Encoding.ASCII.GetBytes(values.attachment);
                                    System.IO.File.WriteAllBytes(filePath, bytes);
                                    MemoryStream ms = new MemoryStream();
                                    string lsfiletable_path = string.Empty;

                                    //string lsfiletable_path = "../../../erp_documents" + "/samunnati" + "/OSD/BankAlert/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/Inbox/" + msgetmail_gid + "/" + values.file_name.Replace("'", @"\'");


                                    // lsfiletable_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/samunnati" + "/OSD/BankAlert/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                                    lsfiletable_path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/samunnati" + "/OSD/BankAlert/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                                    bool status;
                                    status = objcmnstorage.UploadStream("/erpdocument" + "/samunnati" + "/OSD/BankAlert/" + DateTime.Now.Year + " / " + DateTime.Now.Month + "/" + msgetmail_gid ,values.file_name, ms);
                                    ms.Close();
                                    lsfiletable_path = "/erpdocument" + "/samunnati" + "/OSD/BankAlert/" + DateTime.Now.Year + " / " + DateTime.Now.Month + "/";



                                    string name = values.file_name;
                                    string[] splits = name.Split('_');

                                    lsURN = splits[3];
                                    lsURN = lsURN.Replace("SFKMB", "");
                                    lsURN = lsURN.Replace("sfkmb", "");

                                    msSQL = "SELECT bankalertemail_gid FROM osd_trn_tbankalert WHERE  message_id='" + values.message_id + "'";
                                    objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                    if (objOdbcDataReader.HasRows == false)
                                    {
                                        msGETGIDRef = objcmnfunctions.GetMasterGID("BREF");
                                        msSQL = " insert into osd_trn_tbankalert(" +
                                                              " bankalertemail_gid," +
                                                              " bankalertticketref_no," +
                                                              " bankalertemail_to," +
                                                              " bankalertemail_from," +
                                                              " bankalertemail_date," +
                                                              " bankalert_cc," +
                                                              " bankalert_bcc," +
                                                              " bankalertemail_subject," +
                                                              " bankalertemail_content," +
                                                              " bankalert_mailheader," +
                                                              " document_name," +
                                                              " document_path," +
                                                              " customer_urn," +
                                                              " message_number," +
                                                              " message_id )" +
                                                              " values (" +
                                                              "'" + msgetmail_gid + "'," +
                                                              "'" + msGETGIDRef + "'," +
                                                              "'" + values.to + "'," +
                                                              "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                              "'" + values.cc + "'," +
                                                              "'" + values.bcc + "'," +
                                                              "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.header + "'," +
                                                              "'" + values.file_name.Replace("'", "") + "'," +
                                                              "'" + lsfiletable_path + "'," +
                                                              "'" + lsURN + "'," +
                                                              "'" + values.message + "'," +
                                                              "'" + values.message_id + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                        msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn='" + lsURN + "'";
                                        string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
                                        if (lscustomer_gid == "" || lscustomer_gid == null)
                                        {
                                            msSQL = "select tmpcustomer_gid from ocs_tmp_tcustomer where customer_urn='" + lsURN + "'";
                                            string lstmpcustomer_gid = objdbconn.GetExecuteScalar(msSQL);

                                            if (lstmpcustomer_gid == "" || lstmpcustomer_gid == null)

                                            {
                                                MSGETGID = objcmnfunctions.GetMasterGID("NALD");
                                                msSQL = "insert into osd_trn_tbankalert2notallocated" +
                                                              " ( bankalert2notallocated_gid," +
                                                              " ticketref_no," +
                                                              " customer_urn," +
                                                              " email_to," +
                                                              " email_from," +
                                                              " from_mailaddress," +
                                                              " email_date," +
                                                              " cc," +
                                                              " bcc," +
                                                              " email_subject," +
                                                              " email_content," +
                                                              " mailheader," +
                                                              " document_name," +
                                                              " document_path," +
                                                              " reason," +
                                                              " created_by," +
                                                              " created_date) values(" +
                                                              "'" + MSGETGID + "'," +
                                                              "'" + msGETGIDRef + "'," +
                                                              "'" + lsURN + "'," +
                                                              "'" + values.to + "'," +
                                                              "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.from + "'," +
                                                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                              "'" + values.cc + "'," +
                                                              "'" + values.bcc + "'," +
                                                              "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.header + "'," +
                                                              "'" + values.file_name.Replace("'", "") + "'," +
                                                              "'" + lsfiletable_path + "'," +
                                                              "'URN Mismatch'," +
                                                              "'From Scheduler'," +
                                                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                            }
                                            else
                                            {
                                                msSQL = "select customer_code, customername, relationship_manager, relationshipmgmt_name, cluster_manager_gid, cluster_manager_name, creditmanager_gid, creditmgmt_name from ocs_tmp_tcustomer  where customer_urn='" + lsURN + "'";
                                                objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                                if (objOdbcDataReader.HasRows == true)
                                                {
                                                    if (objOdbcDataReader["relationship_manager"].ToString() == null || objOdbcDataReader["relationship_manager"].ToString() == "")
                                                    {
                                                        MSGETGID = objcmnfunctions.GetMasterGID("NALD");
                                                        msSQL = "insert into osd_trn_tbankalert2notallocated" +
                                                                  " ( bankalert2notallocated_gid," +
                                                                  " ticketref_no," +
                                                                  " customer_urn," +
                                                                  " email_to," +
                                                                  " email_from," +
                                                                  " from_mailaddress," +
                                                                  " email_date," +
                                                                  " cc," +
                                                                  " bcc," +
                                                                  " email_subject," +
                                                                  " email_content," +
                                                                  " mailheader," +
                                                                  " document_name," +
                                                                  " document_path," +
                                                                  " reason," +
                                                                  " created_by," +
                                                                  " created_date) values(" +
                                                                  "'" + MSGETGID + "'," +
                                                                  "'" + msGETGIDRef + "'," +
                                                                  "'" + lsURN + "'," +
                                                                  "'" + values.to + "'," +
                                                                  "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                                  "'" + values.from + "'," +
                                                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                  "'" + values.cc + "'," +
                                                                  "'" + values.bcc + "'," +
                                                                  "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                                  "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                                  "'" + values.header + "'," +
                                                                  "'" + values.file_name.Replace("'", "") + "'," +
                                                                  "'" + lsfiletable_path + "'," +
                                                                  "'RM Empty'," +
                                                                  "'From Scheduler'," +
                                                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                    }
                                                    else
                                                    {
                                                        msSQL = " select user_status from adm_mst_tuser a left join hrm_mst_temployee b on a.user_gid=b.user_gid" +
                                                             " where employee_gid='" + objOdbcDataReader["relationship_manager"].ToString() + "'";
                                                        string lsuser_status = objdbconn.GetExecuteScalar(msSQL);
                                                        if (lsuser_status == "Y")
                                                        {
                                                            MSGETGID = objcmnfunctions.GetMasterGID("ALDB");
                                                            msSQL = "insert into osd_trn_tbankalert2allocated (" +
                                                                      " bankalert2allocated_gid," +
                                                                      " customer_name," +
                                                                      " customer_urn," +
                                                                      " customer_gid," +
                                                                      " relationshipmanager_name," +
                                                                      " relationshipmanager_gid," +
                                                                      " ticketref_no," +
                                                                      " email_to," +
                                                                      " email_from," +
                                                                      " from_mailaddress," +
                                                                      " email_date," +
                                                                      " cc," +
                                                                      " bcc," +
                                                                      " email_subject," +
                                                                      " email_content," +
                                                                      " mailheader," +
                                                                      " document_name," +
                                                                      " document_path," +
                                                                      " created_by," +
                                                                      " created_date) values(" +
                                                                      "'" + MSGETGID + "'," +
                                                                      "'" + objOdbcDataReader["customername"].ToString() + "'," +
                                                                      "'" + lsURN + "'," +
                                                                      "'" + objOdbcDataReader["tmpcustomer_gid"].ToString() + "'," +
                                                                      "'" + objOdbcDataReader["relationshipmgmt_name"].ToString() + "'," +
                                                                      "'" + objOdbcDataReader["relationship_manager"].ToString() + "'," +
                                                                      "'" + msGETGIDRef + "'," +
                                                                      "'" + values.to + "'," +
                                                                      "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                                      "'" + values.from + "'," +
                                                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                      "'" + values.cc + "'," +
                                                                      "'" + values.bcc + "'," +
                                                                      "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                                      "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                                      "'" + values.header + "'," +
                                                                      "'" + values.file_name.Replace("'", "") + "'," +
                                                                      "'" + lsfiletable_path + "'," +
                                                                      "'Auto Allocated'," +
                                                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                            try
                                                            {

                                                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                                                objodbcdatareader = objdbconn.GetDataReader(msSQL);
                                                                if (objodbcdatareader.HasRows == true)
                                                                {
                                                                    objodbcdatareader.Read();
                                                                    ls_server = objodbcdatareader["pop_server"].ToString();
                                                                    ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                                                                    ls_username = objodbcdatareader["pop_username"].ToString();
                                                                    ls_password = objodbcdatareader["pop_password"].ToString();

                                                                }
                                                                objodbcdatareader.Close();


                                                                MailMessage message = new MailMessage();
                                                                SmtpClient smtp = new SmtpClient();

                                                                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + objOdbcDataReader["relationship_manager"].ToString() + "'";

                                                                tomailid_list = objdbconn.GetExecuteScalar(msSQL);

                                                                msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                                                                ccmailid_list = objdbconn.GetExecuteScalar(msSQL);


                                                                sub = " Escrow payment received ";

                                                                body = "Dear Sir/Madam,  <br />";
                                                                body = body + "<br />";
                                                                body = body + "Greetings,  <br />";
                                                                body = body + "<br />";
                                                                body = body + " A payment has been received in the escrow account of your client URN: <b>" + HttpUtility.HtmlEncode(lsURN) + "</b>, Client : <b>" + HttpUtility.HtmlEncode(objOdbcDataReader["customername"].ToString()) + ", the details are as follows,<br/>";
                                                                body = body + "<br />";
                                                                body = body + "<b>Mail date & time:</b> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br />";
                                                                body = body + "<br />";
                                                                body = body + "Kindly do the needful.";
                                                                body = body + "<br />";
                                                                body = body + " If this customer is not mapped to you kindly transfer the ticket to the correct RM or Business process immediately.<br />";
                                                                body = body + "<br />";
                                                                body = body + "Kindly <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + " >Click Here</a>  to loginand update the necessary details.<br />";

                                                                body = body + "<br />";

                                                                body = body + "<b>Regards,</b> ";
                                                                body = body + "<br />";
                                                                body = body + "Business Process Team<br /> ";
                                                                body = body + "<br />";
                                                                body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
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
                                                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                                smtp.Send(message);


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
                                                                "'" + MSGETGID + "'," +
                                                                "'" + ls_username + "'," +
                                                                "'" + tomailid_list + "'," +
                                                                "'" + ccmailid_list + "'," +
                                                                "'Escrow payment received'," +
                                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                "'Auto Generated'," +
                                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MSGETGID = objcmnfunctions.GetMasterGID("NALD");
                                                            msSQL = "insert into osd_trn_tbankalert2notallocated" +
                                                                      " ( bankalert2notallocated_gid," +
                                                                      " ticketref_no," +
                                                                      " customer_urn," +
                                                                      " email_to," +
                                                                      " email_from," +
                                                                      " from_mailaddress," +
                                                                      " email_date," +
                                                                      " cc," +
                                                                      " bcc," +
                                                                      " email_subject," +
                                                                      " email_content," +
                                                                      " mailheader," +
                                                                      " document_name," +
                                                                      " document_path," +
                                                                      " reason," +
                                                                      " created_by," +
                                                                      " created_date) values(" +
                                                                      "'" + MSGETGID + "'," +
                                                                      "'" + msGETGIDRef + "'," +
                                                                      "'" + lsURN + "'," +
                                                                      "'" + values.to + "'," +
                                                                      "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                                      "'" + values.from + "'," +
                                                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                      "'" + values.cc + "'," +
                                                                      "'" + values.bcc + "'," +
                                                                      "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                                      "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                                      "'" + values.header + "'," +
                                                                      "'" + values.file_name.Replace("'", "") + "'," +
                                                                      "'" + lsfiletable_path + "'," +
                                                                      "'RM Inactive'," +
                                                                      "'From Scheduler'," +
                                                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                        }

                                                    }
                                                }
                                                objOdbcDataReader.Close();
                                            }

                                        }
                                        else
                                        {
                                            msSQL = "select customer_code, customername, contactperson, relationship_manager, relationshipmgmt_name from ocs_mst_tcustomer where customer_urn='" + lsURN + "'";
                                            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                                            if (objOdbcDataReader.HasRows == true)
                                            {
                                                if (objOdbcDataReader["relationship_manager"].ToString() == null || objOdbcDataReader["relationship_manager"].ToString() == "")
                                                {
                                                    MSGETGID = objcmnfunctions.GetMasterGID("NALD");
                                                    msSQL = "insert into osd_trn_tbankalert2notallocated" +
                                                              " (bankalert2notallocated_gid," +
                                                              " ticketref_no," +
                                                              " customer_urn," +
                                                              " email_to," +
                                                              " email_from," +
                                                              " from_mailaddress," +
                                                              " email_date," +
                                                              " cc," +
                                                              " bcc," +
                                                              " email_subject," +
                                                              " email_content," +
                                                              " mailheader," +
                                                              " document_name," +
                                                              " document_path," +
                                                              " reason," +
                                                              " created_by," +
                                                              " created_date) values(" +
                                                              "'" + MSGETGID + "'," +
                                                              "'" + msGETGIDRef + "'," +
                                                              "'" + lsURN + "'," +
                                                              "'" + values.to + "'," +
                                                              "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.from + "'," +
                                                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                              "'" + values.cc + "'," +
                                                              "'" + values.bcc + "'," +
                                                              "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                              "'" + values.header + "'," +
                                                              "'" + values.file_name.Replace("'", "") + "'," +
                                                              "'" + lsfiletable_path + "'," +
                                                              "'RM Empty'," +
                                                              "'From Scheduler'," +
                                                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                }
                                                else
                                                {
                                                    msSQL = " select user_status from adm_mst_tuser a left join hrm_mst_temployee b on a.user_gid=b.user_gid" +
                                                            " where employee_gid='" + objOdbcDataReader["relationship_manager"].ToString() + "'";
                                                    string lsuser_status = objdbconn.GetExecuteScalar(msSQL);
                                                    if (lsuser_status == "Y")
                                                    {
                                                        MSGETGID = objcmnfunctions.GetMasterGID("ALDB");
                                                        msSQL = "insert into osd_trn_tbankalert2allocated (" +
                                                                  " bankalert2allocated_gid," +
                                                                  " customer_name," +
                                                                  " customer_urn," +
                                                                  " customer_gid," +
                                                                  " relationshipmanager_name," +
                                                                  " relationshipmanager_gid," +
                                                                  " ticketref_no," +
                                                                  " email_to," +
                                                                  " email_from," +
                                                                  " from_mailaddress," +
                                                                  " email_date," +
                                                                  " cc," +
                                                                  " bcc," +
                                                                  " email_subject," +
                                                                  " email_content," +
                                                                  " mailheader," +
                                                                  " document_name," +
                                                                  " document_path," +
                                                                  " created_by," +
                                                                  " created_date) values(" +
                                                                  "'" + MSGETGID + "'," +
                                                                  "'" + objOdbcDataReader["customername"].ToString() + "'," +
                                                                  "'" + lsURN + "'," +
                                                                  "'" + objOdbcDataReader["customer_gid"].ToString() + "'," +
                                                                  "'" + objOdbcDataReader["relationshipmgmt_name"].ToString() + "'," +
                                                                  "'" + objOdbcDataReader["relationship_manager"].ToString() + "'," +
                                                                  "'" + msGETGIDRef + "'," +
                                                                  "'" + values.to + "'," +
                                                                  "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                                  "'" + values.from + "'," +
                                                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                  "'" + values.cc + "'," +
                                                                  "'" + values.bcc + "'," +
                                                                  "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                                  "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                                  "'" + values.header + "'," +
                                                                  "'" + values.file_name.Replace("'", "") + "'," +
                                                                      "'" + lsfiletable_path + "'," +
                                                                  "'Auto Allocated'," +
                                                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                        if (mnResult == 1)
                                                        {
                                                            try
                                                            {

                                                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                                                objodbcdatareader = objdbconn.GetDataReader(msSQL);
                                                                if (objodbcdatareader.HasRows == true)
                                                                {
                                                                    objodbcdatareader.Read();
                                                                    ls_server = objodbcdatareader["pop_server"].ToString();
                                                                    ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                                                                    ls_username = objodbcdatareader["pop_username"].ToString();
                                                                    ls_password = objodbcdatareader["pop_password"].ToString();

                                                                }
                                                                objodbcdatareader.Close();


                                                                MailMessage message = new MailMessage();
                                                                SmtpClient smtp = new SmtpClient();

                                                                msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + objOdbcDataReader["relationship_manager"].ToString() + "'";

                                                                tomailid_list = objdbconn.GetExecuteScalar(msSQL);

                                                                msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                                                                ccmailid_list = objdbconn.GetExecuteScalar(msSQL);


                                                                sub = " Escrow Payment Received ";

                                                                body = "Dear Sir/Madam,  <br />";
                                                                body = body + "<br />";
                                                                body = body + "Greetings,  <br />";
                                                                body = body + "<br />";
                                                                body = body + " A payment has been received in the escrow account of your client URN: <b>" + HttpUtility.HtmlEncode(lsURN) + "</b>, Client : <b>" + HttpUtility.HtmlEncode(objOdbcDataReader["customername"].ToString()) + " </b>, the details are as follows,<br/>";
                                                                body = body + "<br />";
                                                                body = body + "<b>Mail date & time: </b> " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "<br />";
                                                                body = body + "<br />";
                                                                body = body + "Kindly do the needful.";
                                                                body = body + "<br />";
                                                                body = body + " If this customer is not mapped to you kindly transfer the ticket to the correct RM or Business process immediately.<br />";
                                                                body = body + "<br />";
                                                                body = body + "Kindly <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + ">Click Here</a>  to login and update the necessary details.<br />";

                                                                body = body + "<br />";
                                                                body = body + "<b>Regards,</b> ";
                                                                body = body + "<br />";
                                                                body = body + "Business Process Team<br /> ";
                                                                body = body + "<br />";
                                                                body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                                                                body = body + "<br />";

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
                                                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                                                smtp.Send(message);


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
                                                                "'" + MSGETGID + "'," +
                                                                "'" + ls_username + "'," +
                                                                "'" + tomailid_list + "'," +
                                                                "'" + ccmailid_list + "'," +
                                                                "'Escrow payment received'," +
                                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                "'Auto Generated'," +
                                                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MSGETGID = objcmnfunctions.GetMasterGID("NALD");
                                                        msSQL = "insert into osd_trn_tbankalert2notallocated" +
                                                                  " ( bankalert2notallocated_gid," +
                                                                  " ticketref_no," +
                                                                  " customer_urn," +
                                                                  " email_to," +
                                                                  " email_from," +
                                                                  " from_mailaddress," +
                                                                  " email_date," +
                                                                  " cc," +
                                                                  " bcc," +
                                                                  " email_subject," +
                                                                  " email_content," +
                                                                  " mailheader," +
                                                                  " document_name," +
                                                                  " document_path," +
                                                                  " reason," +
                                                                  " created_by," +
                                                                  " created_date) values(" +
                                                                  "'" + MSGETGID + "'," +
                                                                  "'" + msGETGIDRef + "'," +
                                                                  "'" + lsURN + "'," +
                                                                 "'" + values.to + "'," +
                                                                 "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                                                 "'" + values.from + "'," +
                                                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                                 "'" + values.cc + "'," +
                                                                 "'" + values.bcc + "'," +
                                                                 "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                                                 "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                                                 "'" + values.header + "'," +
                                                                 "'" + values.file_name.Replace("'", "") + "'," +
                                                                 "'" + lsfiletable_path + "'," +
                                                                 "'RM Inactive'," +
                                                                 "'From Scheduler'," +
                                                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                                    }
                                                }
                                            }
                                            objOdbcDataReader.Close();
                                        }
                                    }
                                    objOdbcDataReader.Close();
                                }
                            }
                            catch (Exception e)
                            {
                                values.message = e.ToString();
                            }



                        }

                        else
                        {
                            msSQL = "SELECT unformatemail_gid FROM osd_trn_tbankalertunformat WHERE message_id='" + values.message_id + "'";
                            objOdbcDataReader = objdbconn.GetDataReader(msSQL);
                            if (objOdbcDataReader.HasRows == false)
                            {
                                var folder = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/");
                                if ((!System.IO.Directory.Exists(folder)))
                                    System.IO.Directory.CreateDirectory(folder);
                                var filePath = Path.Combine(folder, values.file_name);

                                byte[] bytes = Encoding.ASCII.GetBytes(values.attachment);
                                System.IO.File.WriteAllBytes(filePath, bytes);
                                string name = values.file_name;

                                try
                                {

                                    msSQL = " insert into osd_trn_tbankalertunformat(" +
                                              " unformatemail_gid," +
                                              " unformatticketref_no," +
                                              " unformatemail_to," +
                                              " unformatemail_from," +
                                              " unformatfrom_mailaddress," +
                                              " unformatemail_date," +
                                              " unformat_cc," +
                                              " unformat_bcc," +
                                              " unformatemail_subject," +
                                              " unformatemail_content," +
                                              " unformat_mailheader," +
                                              " document_name," +
                                              " document_path," +
                                              " customer_urn," +
                                              " message_number," +
                                              " message_id )" +
                                              " values (" +
                                              " '" + msgetmail_gid + "'," +
                                              " '" + msGETGIDRef + "'," +
                                              "'" + values.to + "'," +
                                              "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                              "'" + values.from + "'," +
                                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                              "'" + values.cc + "'," +
                                              "'" + values.bcc + "'," +
                                              "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                              "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                              "'" + values.header + "'," +
                                              "'" + values.file_name.Replace("'", "") + "'," +
                                              "'" + filePath + "'," +
                                              " '" + lsURN + "'," +
                                              "'" + values.message + "'," +
                                              "'" + values.message_id + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                catch
                                {

                                }
                            }
                            objOdbcDataReader.Close();
                        }

                    }
                    else
                    {
                        msSQL = " insert into osd_trn_tbankalertlog(" +
                                  " logemail_gid," +
                                  " logemail_to," +
                                  " logemail_from," +
                                  " logfrom_mailaddress," +
                                  " logemail_date," +
                                  " log_cc," +
                                  " log_bcc," +
                                  " logemail_subject," +
                                  " logemail_content," +
                                  " log_mailheader," +
                                  " remarks," +
                                  " message_number," +
                                  " message_id )" +
                                      " values (" +
                                      " '" + msgetmail_gid + "'," +
                                        "'" + values.to + "'," +
                                        "'" + values.from.Replace("'", "").Trim().ToString() + "'," +
                                        "'" + values.from + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        "'" + values.cc + "'," +
                                        "'" + values.bcc + "'," +
                                        "'" + values.subject.Replace("'", "").Trim().ToString() + "'," +
                                        "'" + values.body.Replace("'", "").Trim().ToString() + "'," +
                                        "'" + values.header + "'," +
                                      " 'Have more than attachment'," +
                                      "'" + values.message + "'," +
                                         "'" + values.message_id + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objOdbcDataReader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }
    }
}