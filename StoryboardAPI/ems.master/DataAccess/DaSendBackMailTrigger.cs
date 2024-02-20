using ems.master.DataAccess;
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
using ems.master.Models;
using System.Web.Hosting;
using static OfficeOpenXml.ExcelErrorValue;

namespace ems.master.DataAccess
{
    public class DaSendBackMailTrigger
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDataReader;
        string msSQL, sub, body, ls_server, ls_username, ls_password, institution_gid, contact_gid;
        string tomail_id, cc_mailid, lsBccmail_id;
        string applicant_type,applicant_name,arn_no,overalllimit_amount,rm_name,rm_gid,rm_mobile_no,rh_mobile_no,ch_mobile_no,bh_mobile_no;
        string rm_mailid, ch_mailid, rh_mailid, zh_mailid, bh_mailid, cm_mailid, rcm_mailid, ncm_mailid;
        string ch_gid, rh_gid, zh_gid, bh_gid, cm_gid,rcm_gid, ncm_gid, rh_name, bh_name, ch_name, result;
        bool status, mail_send_result, mail_details_result;
        int mnResult;
        private int ls_port;
        private string application_no;
        private string customer_name;
        private string relationshipmanager_name;
        private string relationshipmanager_mailid;
        private string cluster_head;
        private string zonal_head;
        private string allocated_by;
        private string creditassigned_date;
        public string lssource;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;    

        public bool Daccapprovedmail(string application_gid) 
        {
            msSQL = "select relationshipmanager_name,relationshipmanager_gid,creditassigned_date,application_no,clustermanager_gid,creditregionalmanager_gid,creditnationalmanager_gid,customerref_name from ocs_mst_tapplication where application_gid = '" + application_gid + "'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                customer_name = objODBCDatareader1["customerref_name"].ToString();
                rm_gid = objODBCDatareader1["relationshipmanager_gid"].ToString();
                arn_no = objODBCDatareader1["application_no"].ToString();
                ch_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                rcm_gid = objODBCDatareader1["creditregionalmanager_gid"].ToString();
                ncm_gid = objODBCDatareader1["creditnationalmanager_gid"].ToString();
                relationshipmanager_name = objODBCDatareader1["relationshipmanager_name"].ToString();
                creditassigned_date = objODBCDatareader1["creditassigned_date"].ToString();
            }
            
            lssource = ConfigurationManager.AppSettings["img_path"];
            objODBCDatareader1.Close();
            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ch_gid + "'";
            ch_mailid = objdbconn.GetExecuteScalar(msSQL);
           
            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rcm_gid + "'";
            rcm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + rm_gid + "'";
            rm_mailid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + ncm_gid + "'";
            ncm_mailid = objdbconn.GetExecuteScalar(msSQL);     
            cc_mailid =  ch_mailid + "," + rcm_mailid + "," + ncm_mailid + "," + rm_mailid;
            cc_mailid = cc_mailid.Replace(",,", ",");
            string[] cc_mail_id = cc_mailid.Split(',');
            string[] cc_mail_id_send = cc_mail_id.Distinct().ToArray();
            cc_mailid = cc_mail_id_send[0];
            for (int i = 1; i < cc_mail_id_send.Length; i++)
            {
                cc_mailid = cc_mailid + "," + cc_mail_id_send[i];
            }
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as allocated_by from ocs_mst_tapplication a left join hrm_mst_temployee b" +
                " on b.employee_gid = a.creditassigned_by left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + application_gid + "'";
            allocated_by = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select approval_gid from ocs_trn_tappcreditapproval where hierary_level='0' and application_gid = '" + application_gid + "'";
            tomail_id = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid = '" + tomail_id + "'";
            tomail_id = objdbconn.GetExecuteScalar(msSQL);

            sub = " Application: " + arn_no + " has been SendBack to Underwriting";
            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
            body = body + "<br />";
            body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
            body = body + "<br />";
            body = body + " &nbsp &nbsp Dear Sir / Madam, <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp Greetings! <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp The below application has been SendBack to Underwriting to you. <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp <b> Application Number : </b> " + arn_no + "  <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp <b> Customer Name : </b> " + HttpUtility.HtmlEncode(customer_name) + "  <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp <b> RM Name : </b> " + HttpUtility.HtmlEncode(relationshipmanager_name) + "  <br />";
            body = body + "<br />";          
            body = body + "&nbsp &nbsp <b> Allocated By :</b>  " + HttpUtility.HtmlEncode(allocated_by) + "  <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp <b> Allocation Time :</b>  " + creditassigned_date + "  <br />";
            body = body + "<br />";
            body = body + "&nbsp &nbsp Log into Sam - Custopedia and complete the necessary actions.";
            body = body + "<br />";
            body = body + "&nbsp &nbsp Regards";
            body = body + "<br />";
            body = body + "&nbsp &nbsp Sam-Custopedia <br /> ";
            body = body + "<br />";
            body = body + "&nbsp &nbsp<hr>&nbsp&nbsp";
            body = body + "&nbsp &nbsp Reach out to us at samcustopedia@samunnati.com <br /> ";
            body = body + "<br />";
            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";          
            status = Dasendmailtocredit(sub, body, tomail_id, cc_mailid, application_gid, arn_no);
            if (status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Dasendmailtocredit(string sub,string body,string tomail_id, string cc_mailid, string application_gid, string arn_no)
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
                message.To.Add(new MailAddress(tomail_id));
                lsBccmail_id = ConfigurationManager.AppSettings["SentBackBccMail"].ToString();          
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
                try { 
                    smtp.Send(message);
                    mail_send_result =  true;
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
            if (mail_send_result == true && mail_details_result == true)
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