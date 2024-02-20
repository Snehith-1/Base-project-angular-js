using ems.iasn.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;

namespace ems.iasn.DataAccess
{
    public class DaIasnTrnComposeSentMail
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        int mnResult;
        string msGetGID, msGetDocumentGid;
        result objResult = new Models.result();
        string lspop_server = string.Empty;
        int lspop_port;
        string lspop_username = string.Empty;
        string lspop_password = string.Empty;
        string[] bcc;
        string[] cc;
        string[] to;
        string lsattachment_flag = "N";
        string lsmailbody = string.Empty;

        public bool DaIasnComposeReplyMail(string composemail_gid, string decision, string tomail_id, string ccmail_id, string bccmail_id, string user_gid, string subject, string mailcontent)
        {
            try
            {
                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
              " FROM isn_trn_tmailcredentials" +
              " WHERE 1=1";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    lspop_server = objODBCDataReader["pop_server"].ToString();
                    lspop_port = Convert.ToInt16(objODBCDataReader["pop_port"].ToString());
                    lspop_username = objODBCDataReader["pop_username"].ToString();
                    lspop_password = objODBCDataReader["pop_password"].ToString();
                }
                objODBCDataReader.Close();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(lspop_username);

                
                if (tomail_id != "" && tomail_id != null)
                {
                    to = (tomail_id).Split(';');
                    if (to.Length > 0)
                    {
                        foreach (string toEmail in to)
                        {
                            if (toEmail != lspop_username)
                            {
                                message.To.Add(new MailAddress(toEmail));
                            }
                        }
                    }
                }

                if (ccmail_id != "" && ccmail_id != null)
                {
                    cc = (ccmail_id).Split(';');
                    if (cc.Length > 0)
                    {
                        foreach (string ccEmail in cc)
                        {
                            if (ccEmail != lspop_username)
                            {
                                message.CC.Add(new MailAddress(ccEmail));
                            }

                        }
                    }
                }

                if (bccmail_id != "" && bccmail_id != null)
                {
                    bcc = (bccmail_id).Split(',');
                    if (bcc.Length > 0)
                    {
                        foreach (string bccEmail in bcc)
                        {
                            if (bccEmail != lspop_username)
                            {
                                message.Bcc.Add(new MailAddress(bccEmail));
                            }

                        }
                    }
                }

                // Document Attachments
                msSQL = "select * from isn_tmp_tcomposemailattachement where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    lsattachment_flag = "Y";
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        Attachment data = new Attachment(HttpContext.Current.Server.MapPath(dt["document_path"].ToString()), MediaTypeNames.Application.Octet);
                        data.Name = dt["document_name"].ToString();
                        message.Attachments.Add(data);

                        msGetDocumentGid = objcmnfunctions.GetMasterGID("ISDO");
                        msSQL = " INSERT into isn_trn_tcomposemailattachement (" +
                                " composemailattachment_gid," +
                                " composemail_gid," +
                                " document_name," +
                                " document_path," +
                                " decision," +
                                " created_by," +
                                " created_date" +
                                " ) VALUES (" +
                                " '" + msGetDocumentGid + "'," +
                                " '" + composemail_gid + "'," +
                                " '" + dt["document_name"].ToString() + "'," +
                                " '" + dt["document_path"].ToString() + "'," +
                                " '" + decision + "'," +
                                "'" + user_gid + "'," +
                                " current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                dt_datatable.Dispose();

                msSQL = " DELETE FROM isn_tmp_tcomposemailattachement where created_by='" + user_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGID = objcmnfunctions.GetMasterGID("COSE");

                msSQL = " INSERT INTO isn_trn_tcomposesentmail(" +
                    " composesentmail_gid," +
                   " decision," +
                   " composemail_gid," +
                   " email_subject," +
                   " frommail_id," +
                   " tomail_id," +
                   " ccmail_id," +
                   " bccmail_id," +
                   " mailcontent," +
                   " attachment_flag," +
                   " created_date," +
                   " created_by)" +
                   " VALUES(" +
                   "'" + msGetGID + "'," +
                   "'" + decision + "'," +
                   "'" + composemail_gid + "'," +
                   "'" + subject + "'," +
                   "'" + lspop_username + "'," +
                   "'" + tomail_id + "'," +
                   "'" + ccmail_id + "'," +
                   "'" + bccmail_id + "'," +
                   "'" + mailcontent.Replace("'", "") + "'," +
                   "'" + lsattachment_flag + "'," +
                   "current_timestamp," +
                   "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
                msSQL = "select frommail_id,tomail_id,ccmail_id,bccmail_id,date_format(created_date,'%d-%m-%Y %h:%i %p') as email_date,email_subject,mailcontent" +
                 " from isn_trn_tcomposemail where  composemail_gid='" + composemail_gid + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lsmailbody += mailcontent + "<br /><br />";
                    lsmailbody += "-----Original Message-----<br />";
                    lsmailbody += "From: " + objODBCDataReader["frommail_id"] + "<br />";
                    lsmailbody += "Sent: " + objODBCDataReader["email_date"] + "<br />";
                    lsmailbody += "To: " + objODBCDataReader["tomail_id"] + "<br />";
                    lsmailbody += "Cc: " + objODBCDataReader["ccmail_id"] + "<br />";
                    lsmailbody += "Bcc: " + objODBCDataReader["bccmail_id"] + "<br />";
                    lsmailbody += "Subject: " + objODBCDataReader["email_subject"] + "<br />";

                    Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
                    var stripped = reg.Replace(objODBCDataReader["mailcontent"].ToString(), "");

                    var text = stripped.Replace("\n", "<br/>");
                    // For Replacing html code
                    var plainText = text.Replace("P {margin-top:0;margin-bottom:0;}", "<br/>");

                    lsmailbody += "<br />" + plainText;
                    objODBCDataReader.Close();
                }
                else
                {
                    objODBCDataReader.Close();
                }
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = lsmailbody;
                smtp.Port = lspop_port;
                smtp.Host = lspop_server;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(lspop_username, lspop_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return true;
            }
            catch (Exception ex)
            {

                return false;

            }


        }
    }
}