using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.hrloan.Models;
using ems.utilities.Functions;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.storage.Functions;

namespace ems.hrloan.DataAccess
{
    public class DaTrnHRLoanHRPayment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;



        int k;
        public string ls_server, lsassignee, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_Managermailid, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsrequest_refno, lsBccmail_id, lscc2members, lscreated_date;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;

        public void DaGetHRloanHRheadPaymentDetailscount(MdlTrnHRLoanHRPayment values, string user_gid, string employee_gid)
        {
            //msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
            //        " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
            //        " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
            //        " order by b.created_date desc limit 1";

            //string hr_name = objdbconn.GetExecuteScalar(msSQL);
            //if (hr_name != "")
            //{
            msSQL = " select count(*) as pendinghrpayment_count from hrl_trn_trequest a " +
                    " where a.request_status = 'HRVerify Approved'  ";
            values.pendinghrpayment_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as completedhrpayment_count from hrl_trn_trequest a " +
                    "  where a.request_status = 'Payment Completed' ";
            values.completedhrpayment_count = objdbconn.GetExecuteScalar(msSQL);
            //}
        }

        public void DaGetHRloanHRheadPaymentDetails(MdlTrnHRLoanHRPayment values, string user_gid, string employee_gid)
        {
            //msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
            //        " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
            //        " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
            //        " order by b.created_date desc limit 1";

            //string hr_name = objdbconn.GetExecuteScalar(msSQL);
            //if (hr_name != "")
            //{
            msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                     "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                     "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head,  a.created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure , a.entity_name," +
                     "  a.drm_status from hrl_trn_trequest a " +
                     "  where  a.request_status = 'HRVerify Approved'  " +
                     "  order by a.request_gid desc";

            //}

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethPaymentList = new List<payment_summary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethPaymentList.Add(new payment_summary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        entity_name = dt["entity_name"].ToString(),

                    });
                    values.payment_summary = gethPaymentList;
                }
            }
            dt_datatable.Dispose();
        }
        public bool DaPostHrLoanHRPaymentUpdate(MdlTrnHRLoanHRPayment values, string employee_gid)
        {

            msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
            string hrpayment_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update hrl_trn_trequest set request_status ='Payment Completed'," +
                    " hrpayment_status='Approved' ," +
                    " hrpayment_remarks='" + values.hrpayment_remarks.Replace("'", @"\'") + "' ," +
                    " hrpayment_approvedby='" + employee_gid + "'," +
                    " hrpayment_approvedbyname='" + hrpayment_approvedbyname + "'," +
                    " hrpayment_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where request_gid='" + values.request_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            values.message = "HR Payment Completed successfully";
            try
            {

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
                msSQL = " select  group_concat(distinct a.employee_gid) as CC2members, " +
                        " a.request_refno,a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                        " where a.request_gid = '" + values.request_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                    lsemployee_name = objODBCDatareader["employee_name"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        " where employee_gid = '" + employee_gid + "'";
                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                k = k + 1;

                lsto_mail = ConfigurationManager.AppSettings["HRVerifier_ToMail"].ToString();

                msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                         " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                         " where hrmapping_name = 'Manager')";
                cc_Managermailid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                         " where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                if (cc_Managermailid != null & cc_Managermailid != string.Empty & cc_Managermailid != "")
                {
                    cc_mailid = cc_mailid + "," + cc_Managermailid;
                }
                sub = "Employee Financial Assistance to be processed";
                body = "Dear Sir/Madam,";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " An employeee financial assistance request has been approved. Please process the payment and confirm. ";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<b> Remarks :</b> " + HttpUtility.HtmlEncode(values.hrpayment_remarks.Replace("'", @"\'")) + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(employee_name);
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                //message.To.Add(new MailAddress(lsto_mail));


                lsBccmail_id = ConfigurationManager.AppSettings["HRLoanbcc"].ToString();

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
                smtp.Send(message);

                values.status = true;
                if (values.status == true)
                {
                    msSQL = "Insert into hrl_trn_thrloanmailcount( " +
                    " request_gid," +
                    " from_mail," +
                    " to_mail," +
                    " cc_mail," +
                    " mail_status," +
                    " raisequery_gid, " +
                    " mail_senddate, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + values.request_gid + "'," +
                    "'" + employee_name + "'," +
                    "'" + lsto_mail + "'," +
                    "'" + cc_mailid + "'," +
                    "'Query Raised Successfully'," +
                       "'" + msGetGid + "'," +
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
            return true;
        }
        public void DaGetHRloanDetailsApprovedPayment(MdlTrnHRLoanHRPayment values, string user_gid, string employee_gid)
        {
            //msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
            //        " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
            //        " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
            //        " order by b.created_date desc limit 1";

            //string hr_name = objdbconn.GetExecuteScalar(msSQL);
            //if (hr_name != "")
            //{
            msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head, " +
                "  a.created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                "  a.raised_department,   a.amount,   a.purpose_name,a.entity_name,a.severity_name,  a.tenure ," +
                "  a.drm_status from hrl_trn_trequest a " +
                "  where a.request_status = 'Payment Completed' " +
                "  order by a.request_gid desc";
            //}

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getApprovedList = new List<payment_summary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getApprovedList.Add(new payment_summary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        entity_name = dt["entity_name"].ToString()

                    });
                    values.payment_summary = getApprovedList;
                }
            }
            dt_datatable.Dispose();
        }

    }
}