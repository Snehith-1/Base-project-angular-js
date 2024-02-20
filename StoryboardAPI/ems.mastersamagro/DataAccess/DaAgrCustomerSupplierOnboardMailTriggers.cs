using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to various Mail functionalities in Onboard menu (Onboard Buyer Mail, Welcome Mail,Onboard Supplier Mail)
    /// </summary>
    /// <remarks>Written by Premchander.K </remarks>
    public class DaAgrCustomerSupplierOnboardMailTriggers
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDataReader;
        string msSQL, sub, body, ls_server, ls_username, ls_password, institution_gid, contact_gid, customermail_id ;
        string tomail_id, cc_mailid, lsBccmail_id;
        string applicant_type, applicant_name, arn_no, overalllimit_amount, rm_name, rm_gid, rm_mobile_no, rh_mobile_no, ch_mobile_no, bh_mobile_no;
        // RM, Cluster Head, Regional Head, Zonal Head, Business Head
        string rm_mailid, ch_mailid, rh_mailid, zh_mailid, bh_mailid, cm_mailid, rcm_mailid, ncm_mailid;
        string ch_gid, rh_gid, zh_gid, bh_gid, cm_gid, rcm_gid, ncm_gid, rh_name, bh_name, ch_name, result;
        bool status, mail_send_result, mail_details_result;
        int mnResult;
        private int ls_port;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;


        public bool DaonboardedMail(string application_gid, string lsapproved_date, string lsto_mail, string lscreatedby_name, string lscustomerref_name, string lsapplication_no )
        {
            msSQL = " select a.institution_gid from agr_mst_tbyronboard2institution a where a.application_gid = '" + application_gid + "' " +
                " and a.stakeholder_type in ('Applicant')  group by a.application_gid;";
            institution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select b.contact_gid from agr_mst_tbyronboardcontact b where b.application_gid = '" + application_gid + "' " +
              " and b.stakeholder_type in ('Applicant')  group by b.application_gid;";
            contact_gid = objdbconn.GetExecuteScalar(msSQL);


            if (!string.IsNullOrEmpty(contact_gid))
            {

                msSQL = " select email_address from agr_mst_tbyronboardcontact2email  where primary_status = 'Yes' and contact_gid = '" + contact_gid + "' ";
                customermail_id = objdbconn.GetExecuteScalar(msSQL);
            }

            else if (!string.IsNullOrEmpty(institution_gid))
            {

                msSQL = " select email_address from agr_mst_tbyronboardinstitution2email  where primary_status = 'Yes' and institution_gid = '" + institution_gid + "' ";
                    customermail_id = objdbconn.GetExecuteScalar(msSQL);
            }
          
            sub = " Welcome to Samunnati  ";
            body = "Dear " + HttpUtility.HtmlEncode(lscustomerref_name) + ",<br><br>";
            body = body + "Welcome to Samunnati Agro-Solutions Private Limited!<br>";
            body = body + "<br>";
            body = body + "We are happy to see you on board and the onboarding request is successfully completed. Please reach out to Mr/ Ms. " + lscreatedby_name + " (" + lsto_mail +")  for any queries or concerns.<br><br>";
            body = body + "Requesting you to save this email so that you can refer to it later.<br><br>";
            body = body + "1.RM Name - " + HttpUtility.HtmlEncode(lscreatedby_name) + "<br><br>";
            body = body + "2.Customer ID - " + HttpUtility.HtmlEncode(lsapplication_no) + "<br><br>";
            body = body + "3.Date of Onboarding - " + HttpUtility.HtmlEncode(lsapproved_date) + "<br><br>";
            body = body + "You may also visit our website <a href='https://site.samunnati.com/contact-us/'>site.samunnati.com</a> to reach out to our Customer Service Helpline Number.<br><br>";
            body = body + "Regards,<br>";
            body = body + "Team Samunnati.<br><br>";
            body = body + "<i>Disclaimer: This is an automatically system generated email – please do not reply to it. The content of this email is confidential and intended for the recipient specified in the message only. Please do not share any part of this email with any third party, without a written consent of Samunnati. If you are not the intended recipient of this email, please follow with its deletion and reach out to us on [tradecommunications@samunnati.com], so that you do not receive such emails in the future. Further, this information is confidential to the intended recipient and any misuse of any such information data will be considered as breach and subject to legal action under applicable laws.</i>";


            status = DaWelcomeMailtoCustomer(sub, body, customermail_id, lsto_mail, application_gid);
            if (status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaWelcomeMailtoCustomer(string sub, string body, string customermail_id, string lsto_mail, string application_gid)
        {
            try
            {
                msSQL = " SELECT pop_server, pop_port, pop_username, pop_password  FROM adm_mst_tcompany";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    ls_server = objODBCDatareader1["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader1["pop_port"]);
                    ls_username = objODBCDatareader1["pop_username"].ToString();
                    ls_password = objODBCDatareader1["pop_password"].ToString();
                }
                objODBCDatareader1.Close();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(customermail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["OnboardMailBccMail"].ToString();
                //lsBccmail_id = cc_mailid + "," + lsBccmail_id;

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
                    lsCCReceipients = lsto_mail.Split(',');
                    if (lsto_mail.Length == 0)
                    {
                        message.CC.Add(new MailAddress(lsto_mail));
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
                try
                {
                    smtp.Send(message);
                    mail_send_result = true;
                    result = "Mail Send Successfully";

                }
                catch (Exception ex)
                {
                    mail_send_result = false;
                    result = ex.ToString();
                }
                mail_details_result = true;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
                mail_details_result = false;
            }

            //Mail Log
            msSQL = "Insert into agr_mst_tcustomermaillog( " +
                                   " application_gid," +             
                                   " cc," +
                                   " bcc," +
                                   " email_to," +
                                   " email_date," +
                                   " email_subject," +
                                   " email_content," +
                                   //" status," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + application_gid + "'," +                             
                                   "'" + lsto_mail + "'," +
                                   "'" + lsBccmail_id + "'," +
                                   "'" + customermail_id + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + sub.Replace("'", "") + "'," +
                                   "'" + body.Replace("'", "") + "'," +
                                   //"'" + result + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mail_send_result == true && mail_details_result == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DaonboardedSupplierMail(string application_gid, string lsapproved_date, string lsto_mail, string lscreatedby_name, string lscustomerref_name, string lsapplication_no)
        {
            msSQL = " select a.institution_gid from agr_mst_tsupronboard2institution a where a.application_gid = '" + application_gid + "' " +
                " and a.stakeholder_type in ('Applicant')  group by a.application_gid;";
            institution_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select b.contact_gid from agr_mst_tsupronboardcontact b where b.application_gid = '" + application_gid + "' " +
              " and b.stakeholder_type in ('Applicant')  group by b.application_gid;";
            contact_gid = objdbconn.GetExecuteScalar(msSQL);


            if (!string.IsNullOrEmpty(contact_gid))
            {

                msSQL = " select email_address from agr_mst_tsupronboardcontact2email  where primary_status = 'Yes' and contact_gid = '" + contact_gid + "' ";
                customermail_id = objdbconn.GetExecuteScalar(msSQL);
            }

            else if (!string.IsNullOrEmpty(institution_gid))
            {

                msSQL = " select email_address from agr_mst_tsupronboardinstitution2email  where primary_status = 'Yes' and institution_gid = '" + institution_gid + "' ";
                customermail_id = objdbconn.GetExecuteScalar(msSQL);
            }

            sub = " Welcome to Samunnati  ";
            body = "Dear " + HttpUtility.HtmlEncode(lscustomerref_name) + ",<br><br>";
            body = body + "Welcome to Samunnati Agro-Solutions Private Limited!<br>";
            body = body + "<br>";
            body = body + "We are happy to see you on board and the onboarding request is successfully completed. Please reach out to Mr/ Ms. " + lscreatedby_name + " (" + lsto_mail + ")  for any queries or concerns.<br><br>";
            body = body + "Requesting you to save this email so that you can refer to it later.<br><br>";
            body = body + "1.RM Name - " + HttpUtility.HtmlEncode(lscreatedby_name) + "<br><br>";
            body = body + "2.Supplier ID - " + HttpUtility.HtmlEncode(lsapplication_no) + "<br><br>";
            body = body + "3.Date of Onboarding - " + HttpUtility.HtmlEncode(lsapproved_date) + "<br><br>";
            body = body + "You may also visit our website <a href='https://site.samunnati.com/contact-us/'>site.samunnati.com</a> to reach out to our Customer Service Helpline Number.<br><br>";
            body = body + "Regards,<br>";
            body = body + "Team Samunnati.<br><br>";
            body = body + "<i>Disclaimer: This is an automatically system generated email – please do not reply to it. The content of this email is confidential and intended for the recipient specified in the message only. Please do not share any part of this email with any third party, without a written consent of Samunnati. If you are not the intended recipient of this email, please follow with its deletion and reach out to us on [tradecommunications@samunnati.com], so that you do not receive such emails in the future. Further, this information is confidential to the intended recipient and any misuse of any such information data will be considered as breach and subject to legal action under applicable laws.</i>";


            status = DaWelcomeMailtoCustomer(sub, body, customermail_id, lsto_mail, application_gid);
            if (status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}