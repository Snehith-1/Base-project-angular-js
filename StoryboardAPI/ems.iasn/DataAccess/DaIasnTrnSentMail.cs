using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.iasn.Models;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Text.RegularExpressions;
namespace ems.iasn.DataAccess
{
    public class DaIasnTrnSentMail
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader, objODBCDataReader1;
        int mnResult;
        string msGetGID;
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

        public bool DaIasnReplyMail(string email_gid, string decision_gid, string decision, string message_id,string reference_id,string tomail_id,string ccmail_id,string bccmail_id,string user_gid,string subject,string mailcontent)
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
                

                    if (string.IsNullOrEmpty(reference_id))
                    {
                        message.Headers.Add("In-Reply-To", message_id);
                        message.Headers.Add("References", message_id);
                    }
                    else
                    {
                        message.Headers.Add("In-Reply-To", reference_id);
                        message.Headers.Add("References", message_id + reference_id);
                    }
              
             

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

                msGetGID = objcmnfunctions.GetMasterGID("MAIL");
              
                msSQL = " SELECT document_name,document_path " +
                       " FROM isn_tmp_tmaildetailsattachement" +
                       " WHERE created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    lsattachment_flag = "Y";
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        Attachment data = new Attachment(HttpContext.Current.Server.MapPath(dt["document_path"].ToString()), MediaTypeNames.Application.Octet);
                        data.Name = dt["document_name"].ToString();
                        message.Attachments.Add(data);

                        msSQL = " INSERT into isn_trn_tmaildetailsattachement (" +
                                " email_gid," +
                                " document_name," +
                                " document_path," +
                                " created_date" +
                                " ) VALUES (" +
                                " '" + msGetGID + "'," +
                                " '" + dt["document_name"].ToString() + "'," +
                                " '" + dt["document_path"].ToString() + "'," +
                                " current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                dt_datatable.Dispose();

               
                msSQL = " INSERT INTO isn_trn_tsentmail(" +
                    " sentmail_gid," +
                   " decision_gid," +
                   " decision," +
                   " email_gid," +
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
                   "'" + decision_gid + "'," +
                   "'" + decision + "'," +
                   "'" + email_gid + "'," +
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

                lsmailbody += mailcontent + "<br /><br />";

                msSQL = "select frommail_id,tomail_id,ccmail_id,bccmail_id,date_format(created_date,'%d-%m-%Y %h:%i %p') as email_date,email_subject,mailcontent" +
               " from isn_trn_tsentmail where email_gid='" + email_gid + "' and sentmail_gid <> '" + msGetGID + "' order by email_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        lsmailbody += "<br /><br />";
                        lsmailbody += "-----Original Message-----<br />";
                        lsmailbody += "From: " + dt["frommail_id"] + "<br />";
                        lsmailbody += "Sent: " + dt["email_date"] + "<br />";
                        lsmailbody += "To: " + dt["tomail_id"] + "<br />";
                        lsmailbody += "Cc: " + dt["ccmail_id"] + "<br />";
                        lsmailbody += "Bcc: " + dt["bccmail_id"] + "<br />";
                        lsmailbody += "Subject: " + dt["email_subject"] + "<br />";

                        //Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
                        //var stripped = reg.Replace(dt["mailcontent"].ToString(), "");

                        var text = dt["mailcontent"].ToString();
                        // For Replacing html code
                        var plainText = text.Replace("P {margin-top:0;margin-bottom:0;}", "<br/>");

                        lsmailbody += "<br />" + plainText;
                    }
                }
                dt_datatable.Dispose();

                msSQL = "select email_from,email_to,email_cc,email_bcc,date_format(email_date,'%d-%m-%Y %h:%i %p') as email_date,email_subject,email_content" +
                  " from isn_trn_treferencemail where email_gid='" + email_gid + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lsmailbody += "-----Original Message-----<br />";
                    lsmailbody += "From: " + objODBCDataReader["email_from"] + "<br />";
                    lsmailbody += "Sent: " + objODBCDataReader["email_date"] + "<br />";
                    lsmailbody += "To: " + objODBCDataReader["email_to"] + "<br />";
                    lsmailbody +="Cc: " + objODBCDataReader["email_cc"] + "<br />";
                    lsmailbody +="Bcc: " + objODBCDataReader["email_bcc"] + "<br />";
                    lsmailbody += "Subject: " + objODBCDataReader["email_subject"] + "<br />";
                    
                     var text = objODBCDataReader["email_content"].ToString();
                    lsmailbody += "<br />" + text;
                    objODBCDataReader.Close();
                }
                else
                {
                    objODBCDataReader.Close();
                }
                
                msSQL = "select email_from,email_to,cc,bcc,date_format(email_date,'%d-%m-%Y %h:%i %p') as email_date,email_subject,email_content" +
                 " from isn_trn_tmaildetails where email_gid='" + email_gid + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    lsmailbody += "<br /><br />";
                    lsmailbody += "-----Original Message-----<br />";
                    lsmailbody += "From: " + objODBCDataReader["email_from"] + "<br />";
                    lsmailbody += "Sent: " + objODBCDataReader["email_date"] + "<br />";
                    lsmailbody += "To: " + objODBCDataReader["email_to"] + "<br />";
                    lsmailbody += "Cc: " + objODBCDataReader["cc"] + "<br />";
                    lsmailbody += "Bcc: " + objODBCDataReader["bcc"] + "<br />";
                    lsmailbody += "Subject: " + objODBCDataReader["email_subject"] + "<br />";

                    var text = objODBCDataReader["email_content"].ToString();
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