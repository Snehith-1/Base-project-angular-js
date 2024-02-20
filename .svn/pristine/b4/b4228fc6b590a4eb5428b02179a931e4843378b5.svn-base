using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.osd.Models;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnRequestApproval
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        string lspath, lssession_user, count;
        string lscc_mail;
        int mnResult, MailFlag, ls_port, hierarchy_level;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, body, sub, lscontent, messages, lstoken, lsuser_gid = string.Empty;
        string lsrequest_refno, lsactivity_name, lsrequest_title, lsresponse_flag, lsrequested_by,lsrequestondtl, request_title, lstransferondtl, lstransferbydtl, lsrequest_description, lsassigned_membername, assigned_supportteamname, lsrequest_status;
        string lsgetapproval_remarks, lsassigndedtl, lsraiseddtl, lsforwarddtl, lscompleteddtl,lsapprovalondtl, lsrejecteddtl,lsRaised_By, lsRaisedNo, lsBaselocation_Name, lsRaised_Date, lsraised_department, lsemployee_mobileno, lslevel_zero, lslevel_one, lsemployee_number,lsremarks, lsapproved_date, lsapproval_name;
        string lsdatabase = ConfigurationManager.AppSettings["externalportal"].ToString();
        string[] lsCCReceipients, lsBCCReceipients;
        string cc,lsbcc;
        string cc_mailid = string.Empty;
        string lsmodulereportingto_gid;
        string lsref_no, lsrm_gid, lsrm_name, lsrh_gid, lsrh_name, lscustomerurn, lscustomer_name;

        public void DaPostRequestApproved(requestapproval values,string employee_gid)
        {
            msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set approval_status='Approved', seqhierarchy_view='N', " +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where approval_gid ='" + employee_gid + "' and approval_token='" + values.approval_token + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Service Request Approved Successfully..!";

                if (values.approval_type == "Sequence")
                {
                    msSQL = " select count(*) as count from " + lsdatabase + ".osd_trn_trequestapproval where servicerequest_gid ='" + values.servicerequest_gid + "' and approval_type='Sequence'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        count = objODBCDatareader["count"].ToString();
                    }
                    objODBCDatareader.Close();

                    if (Convert.ToInt16(count) >= Convert.ToInt16(values.hierary_level))

                    {
                        int total = Convert.ToInt16(count);
                        int hierary_level = Convert.ToInt16(values.hierary_level);
                        while (total >= hierary_level)
                        {
                            msSQL = " select approval_status from " + lsdatabase + ".osd_trn_trequestapproval where servicerequest_gid ='" + values.servicerequest_gid + "' and approval_type='Sequence' and hierary_level='" + hierary_level + "' order by approval_status desc";
                            string status = objdbconn.GetExecuteScalar(msSQL);

                            if (status != "Cancelled")
                            {

                                msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set seqhierarchy_view='Y' " +
                                     " where servicerequest_gid='" + values.servicerequest_gid + "' and hierary_level='" + hierary_level + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM " + lsdatabase + ".adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = objODBCDatareader["pop_username"].ToString();
                                    ls_password = objODBCDatareader["pop_password"].ToString();
                                }
                                objODBCDatareader.Close();

                                //msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + values.approvalmember[i].employee_gid + "'";
                                msSQL = " select employee_emailid, a.created_by, approval_token from " + lsdatabase + ".osd_trn_trequestapproval a " +
                                        " left join " + lsdatabase + ".hrm_mst_temployee b on a.approval_gid = b.employee_gid " +
                                        " where servicerequest_gid ='" + values.servicerequest_gid + "'  and seqhierarchy_view = 'Y'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsto_mail = objODBCDatareader["employee_emailid"].ToString();
                                    lstoken = objODBCDatareader["approval_token"].ToString();
                                    lsuser_gid = objODBCDatareader["created_by"].ToString();
                                }
                                objODBCDatareader.Close();

                                //msSQL = "select email_id from " + lsdatabase + ".hrm_mst_tdepartment where department_name='Operations'";
                                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                //if (objODBCDatareader.HasRows == true)
                                //{
                                //    cc = objODBCDatareader["email_id"].ToString();
                                //}
                                //objODBCDatareader.Close();

                                string lsdepartmentgid;
                                lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                                msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    cc = objODBCDatareader["businessunit_emailaddress"].ToString();
                                }
                                objODBCDatareader.Close();

                                msSQL = " select request_refno,activity_name,request_title,getapproval_remarks,a.request_status,a.assigned_supportteamname,a.assigned_membername,department_name,a.request_description," +
                                        " concat(assigned_supportteamname, ' / ', assigned_membername) as assigndedtl,getapproval_remarks,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                                        " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date, " +
                                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                                        " from " + lsdatabase + ".osd_trn_tservicerequest a " +
                                        " left join " + lsdatabase + ".adm_mst_tuser b on a.created_by = b.user_gid " +
                                        " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                                        " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                                        " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                                        " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                                    lsactivity_name = objODBCDatareader["activity_name"].ToString();
                                    request_title = objODBCDatareader["request_title"].ToString();
                                    lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                                    lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                                    lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                                    lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();
                                    lsraised_department = objODBCDatareader["department_name"].ToString(); 
                                    lsrequest_status = objODBCDatareader["request_status"].ToString();
                                    assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                                    lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                                    lsrequest_description = objODBCDatareader["request_description"].ToString();
                                    lsgetapproval_remarks = objODBCDatareader["getapproval_remarks"].ToString();
                                    lsassigndedtl = objODBCDatareader["assigndedtl"].ToString();
                                    lsraiseddtl = objODBCDatareader["raised_by"].ToString();
                                    lsrequestondtl = objODBCDatareader["created_date"].ToString();
                                }
                                objODBCDatareader.Close();
                                msSQL = "select approval_name, date_format(approved_date, '%d-%m-%Y %h:%i %p') as approved_date from osd_trn_trequestapproval  where approval_token='" + values.approval_token + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsapproval_name = objODBCDatareader["approval_name"].ToString();
                                    lsapproved_date = objODBCDatareader["approved_date"].ToString();

                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from " + lsdatabase + ".adm_mst_tuser where user_gid='" + lsuser_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsrequested_by = objODBCDatareader["requested_by"].ToString();
                                }
                                objODBCDatareader.Close();
                                string lsemployee_gid;
                                lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + lsuser_gid + "'");

                                msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                                lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                "  from adm_mst_tmodule2employee a " +
                "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
                "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                "  where a.module_gid ='"+ lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'"+
                "  group by a.employeereporting_to ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                                    lslevel_zero = objODBCDatareader["level_zero"].ToString();
                                    lslevel_one = objODBCDatareader["level_one"].ToString();
                                    lsemployee_number = objODBCDatareader["employee_number"].ToString();
                                    //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                                    //values.level_zero = objODBCDatareader["level_zero"].ToString();
                                    //values.level_one = objODBCDatareader["level_one"].ToString();
                                    //values.employee_number = objODBCDatareader["employee_number"].ToString();
                                }
                                //sub = "Request your approval";
                                sub = " Service Request is approved ";
                                body = "Dear </b> " + HttpUtility.HtmlEncode(lsRaised_By) + ", <br />";
                                body = body + "<br />";
                                //body = body + "Greetings,  <br />";
                                //body = body + "<br />";
                                body = body + "Below service request has been approved. <br />";
                                body = body + "<br />";
                                body = body + "<b>Service Request Number :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Business Unit name   :</b> " + HttpUtility.HtmlEncode(lsraised_department) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Activity Title :</b> " + HttpUtility.HtmlEncode(lsactivity_name) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Approved by :</b> " + HttpUtility.HtmlEncode(lsapproval_name) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Approved date:</b> " + lsapproved_date + "<br />";
                                body = body + "<br />";                                
                                body = body + " Login <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> and do the needful</a> <br />";
                                body = body + "<br />";
                                //body = body + "<b>Thanks & Regards, </b> ";
                                //body = body + "<br />";
                                //body = body + "<b> Team Business Process </b> ";
                                //body = body + "<br />";
                                cc_mailid = "";
                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                message.To.Add(new MailAddress(lsto_mail));
                                //message.CC.Add(cc);
                                lsbcc = ConfigurationManager.AppSettings["osdbcc"].ToString();

                                if (cc != null & cc != string.Empty & cc != "")
                                {
                                    lsCCReceipients = cc.Split(',');
                                    if (cc.Length == 0)
                                    {
                                        message.CC.Add(new MailAddress(cc));
                                    }
                                    else
                                    {
                                        foreach (string CCEmail in lsCCReceipients)
                                        {
                                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                        }
                                    }
                                }

                                if (lsbcc != null & lsbcc != string.Empty & lsbcc != "")
                                {
                                    lsBCCReceipients = lsbcc.Split(',');
                                    if (lsbcc.Length == 0)
                                    {
                                        message.Bcc.Add(new MailAddress(cc));
                                    }
                                    else
                                    {
                                        foreach (string BCCEmail in lsBCCReceipients)
                                        {
                                            message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple CC email Id
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
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(message);

                                values.status = true;
                                if (values.status == true)
                                {
                                    msSQL = "Insert into " + lsdatabase + ".osd_trn_tmailcount( " +
                                    " servicerequest_gid," +
                                    " from_mail," +
                                    " to_mail," +
                                    //" cc_mail," +
                                    " mail_status," +
                                    " mail_senddate, " +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + values.servicerequest_gid + "'," +
                                    "'" + ls_username + "'," +
                                    "'" + lsto_mail + "'," +
                                    //"'" + lscc_mail + "'," +
                                    "'Request Initiation Mail'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + lsuser_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                break;
                            }
                            else
                            {
                                hierary_level += 1;
                            }
                        }
                    }
                }

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostRequestRejected(requestapproval values,string employee_gid)
        {
            msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set approval_status='Rejected', seqhierarchy_view='N', " +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where approval_gid ='" + employee_gid + "' and approval_token='" + values.approval_token + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Service Request Rejected Successfully..!";


                lsremarks = "Automatically Rejected By System";
                msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set approval_status='Rejected' ,seqhierarchy_view = 'N'," +
                         " approval_remarks='" + lsremarks + "'," +
                     " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                      " where servicerequest_gid='" + values.servicerequest_gid + "'" +
                                     " and approval_status not in ('Approved','Cancelled','Rejected') " + "";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set approval_status='Rejected'," +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where approval_gid ='" + employee_gid + "' and approval_token='" + values.approval_token + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //msSQL = "update " + lsdatabase + ".osd_trn_tservicerequest set request_status = 'Rejected', rejected_flag = 'Y' where " +
                //        "servicerequest_gid ='" + values.servicerequest_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                // For Sequence approval Mail Triggering to Next Person

                if (values.approval_type == "Sequence")
                {
                    msSQL = " select count(*) as count from " + lsdatabase + ".osd_trn_trequestapproval where servicerequest_gid ='" + values.servicerequest_gid + "' and approval_type='Sequence'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        count = objODBCDatareader["count"].ToString();
                    }
                    objODBCDatareader.Close();

                    if (Convert.ToInt16(count) >= Convert.ToInt16(values.hierary_level))
                    {

                        int total = Convert.ToInt16(count);
                        int hierary_level = Convert.ToInt16(values.hierary_level);
                        while (total >= hierary_level)
                        {
                            msSQL = " select approval_status from " + lsdatabase + ".osd_trn_trequestapproval where servicerequest_gid ='" + values.servicerequest_gid + "' and approval_type='Sequence' and hierary_level='" + hierary_level + "'";
                            string status = objdbconn.GetExecuteScalar(msSQL);

                            if (status != "Cancelled" && status== "Pending")
                            {
                                msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set seqhierarchy_view='Y' " +
                             " where servicerequest_gid='" + values.servicerequest_gid + "' and hierary_level='" + hierary_level + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM " + lsdatabase + ".adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = objODBCDatareader["pop_username"].ToString();
                                    ls_password = objODBCDatareader["pop_password"].ToString();
                                }
                                objODBCDatareader.Close();

                                msSQL = " select employee_emailid, a.created_by, approval_token from " + lsdatabase + ".osd_trn_trequestapproval a " +
                                        " left join " + lsdatabase + ".hrm_mst_temployee b on a.approval_gid = b.employee_gid " +
                                        " where servicerequest_gid ='" + values.servicerequest_gid + "'  and seqhierarchy_view = 'Y'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsto_mail = objODBCDatareader["employee_emailid"].ToString();
                                    lstoken = objODBCDatareader["approval_token"].ToString();
                                    lsuser_gid = objODBCDatareader["created_by"].ToString();
                                }
                                objODBCDatareader.Close();

                                //msSQL = "select email_id from " + lsdatabase + ".hrm_mst_tdepartment where department_name='Operations'";
                                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                //if (objODBCDatareader.HasRows == true)
                                //{
                                //    cc = objODBCDatareader["email_id"].ToString();
                                //}
                                //objODBCDatareader.Close();

                                string lsdepartmentgid;
                                lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                                msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    cc = objODBCDatareader["businessunit_emailaddress"].ToString();
                                }
                                objODBCDatareader.Close();


                                msSQL = " select request_refno,activity_name,request_title,getapproval_remarks, a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description," +
                                        " concat(assigned_supportteamname, ' / ', assigned_membername) as assigndedtl,getapproval_remarks,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                                        " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date, " +
                                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                                        " from " + lsdatabase + ".osd_trn_tservicerequest a " +
                                        " left join " + lsdatabase + ".adm_mst_tuser b on a.created_by = b.user_gid " +
                                        " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                                        " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                                        " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                                " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                                    lsactivity_name = objODBCDatareader["activity_name"].ToString();
                                    request_title = objODBCDatareader["request_title"].ToString();
                                    lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                                    lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                                    lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                                    lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                                    lsrequest_status = objODBCDatareader["request_status"].ToString();
                                    assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                                    lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                                    lsrequest_description = objODBCDatareader["request_description"].ToString();
                                    lsgetapproval_remarks = objODBCDatareader["getapproval_remarks"].ToString();
                                    lsassigndedtl = objODBCDatareader["assigndedtl"].ToString();
                                    lsraiseddtl = objODBCDatareader["raised_by"].ToString();
                                    lsapprovalondtl = objODBCDatareader["created_date"].ToString();
                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from " + lsdatabase + ".adm_mst_tuser where user_gid='" + lsuser_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsrequested_by = objODBCDatareader["requested_by"].ToString();
                                }
                                objODBCDatareader.Close();
                                string lsemployee_gid;
                                lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + lsuser_gid + "'");

                                msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                                lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                          "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                          "  from adm_mst_tmodule2employee a " +
                          "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                          "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
                          "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                          "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                          "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                          "  where a.module_gid ='"+ lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'"+
                          "  group by a.employeereporting_to ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                                    lslevel_zero = objODBCDatareader["level_zero"].ToString();
                                    lslevel_one = objODBCDatareader["level_one"].ToString();
                                    lsemployee_number = objODBCDatareader["employee_number"].ToString();
                                    //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                                    //values.level_zero = objODBCDatareader["level_zero"].ToString();
                                    //values.level_one = objODBCDatareader["level_one"].ToString();
                                    //values.employee_number = objODBCDatareader["employee_number"].ToString();
                                }
                                //sub = "Approval Received";
                                sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Approval Received ";
                                body = "Dear Sir/Madam,  <br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + " Approval completed,the details are as follows,<br />";
                                body = body + "<br />";
                                body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Approval By :</b> " + HttpUtility.HtmlEncode(lsraiseddtl) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Approval On :</b> " + HttpUtility.HtmlEncode(lsapprovalondtl) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                                body = body + "<br />";                                
                                //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                                //body = body + "<br />";
                                body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                                body = body + "<br />";
                           
                                body = body + " click the link to enter the web portal to approve <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                                body = body + "<br />";
                                body = body + "<b>Thanks & Regards, </b> ";
                                body = body + "<br />";
                                body = body + "<b> Team Business Process </b> ";
                                body = body + "<br />";
                                //cc_mailid = "";
                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                message.To.Add(new MailAddress(lsto_mail));
                                //message.CC.Add(cc);


                                if (cc != null & cc != string.Empty & cc != "")
                                {
                                    lsCCReceipients = cc.Split(',');
                                    if (cc.Length == 0)
                                    {
                                        message.CC.Add(new MailAddress(cc));
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
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(message);

                                values.status = true;
                                if (values.status == true)
                                {
                                    msSQL = "Insert into " + lsdatabase + ".osd_trn_tmailcount( " +
                                    " servicerequest_gid," +
                                    " from_mail," +
                                    " to_mail," +
                                    //" cc_mail," +
                                    " mail_status," +
                                    " mail_senddate, " +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + values.servicerequest_gid + "'," +
                                    "'" + ls_username + "'," +
                                    "'" + lsto_mail + "'," +
                                    //"'" + lscc_mail + "'," +
                                    "'Request Initiation Mail'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + lsuser_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                                break;
                            }
                            else
                            {
                                hierary_level += 1;
                            }
                        }
                    }
                }

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }


        public void DaPostRequestCancelled(requestapproval values, string user_gid)
        {
            msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set approval_status='Cancelled', seqhierarchy_view='N', " +
                    " approval_remarks='" + values.approval_remarks.Replace("'", "\\'") + "'," +
                    " cancelled_by='" + user_gid + "'," +
                    " cancelled_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where approval_token='" + values.approval_token + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Approval Member Cancelled Successfully..!";

                // For Sequence approval Mail Triggering to Next Person

                if (values.approval_type == "Sequence")
                {
                    string s = values.hierary_level;
                    //int number = Convert.ToInt32(s);
                    //number -= 4;
                    //string hierary_level = number.ToString();
                    //if (hierary_level != "0")
                    //{
                        msSQL = " select approval_status from " + lsdatabase + ".osd_trn_trequestapproval where servicerequest_gid ='" + values.servicerequest_gid + "' and approval_type='Sequence' and hierary_level='"+ values.hierary_level + "' order by created_date desc";
                        string status = objdbconn.GetExecuteScalar(msSQL);

                        if (status == "Pending")
                        {

                            msSQL = " select count(*) as count from " + lsdatabase + ".osd_trn_trequestapproval where servicerequest_gid ='" + values.servicerequest_gid + "' and approval_type='Sequence'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                count = objODBCDatareader["count"].ToString();

                            }
                            objODBCDatareader.Close();

                            if (Convert.ToInt16(count) >= Convert.ToInt16(values.hierary_level))

                            {
                                msSQL = " update " + lsdatabase + ".osd_trn_trequestapproval set seqhierarchy_view='Y' " +
                                     " where servicerequest_gid='" + values.servicerequest_gid + "' and hierary_level='" + values.hierary_level + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM " + lsdatabase + ".adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = objODBCDatareader["pop_username"].ToString();
                                    ls_password = objODBCDatareader["pop_password"].ToString();
                                }
                                objODBCDatareader.Close();

                                msSQL = " select employee_emailid, a.created_by, approval_token from " + lsdatabase + ".osd_trn_trequestapproval a " +
                                        " left join " + lsdatabase + ".hrm_mst_temployee b on a.approval_gid = b.employee_gid " +
                                        " where servicerequest_gid ='" + values.servicerequest_gid + "'  and seqhierarchy_view = 'Y'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsto_mail = objODBCDatareader["employee_emailid"].ToString();
                                    lstoken = objODBCDatareader["approval_token"].ToString();
                                    lsuser_gid = objODBCDatareader["created_by"].ToString();
                                }
                                objODBCDatareader.Close();

                            //msSQL = "select email_id from " + lsdatabase + ".hrm_mst_tdepartment where department_name='Operations'";
                            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            //if (objODBCDatareader.HasRows == true)
                            //{
                            //    cc = objODBCDatareader["email_id"].ToString();
                            //}
                            //objODBCDatareader.Close();

                            string lsdepartmentgid;
                            lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");


                            msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                cc = objODBCDatareader["businessunit_emailaddress"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select request_refno,activity_name,request_title,getapproval_remarks,a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                                        " concat(assigned_supportteamname, ' / ', assigned_membername) as assigndedtl,getapproval_remarks,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                                        " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date, " +
                                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                                        " from " + lsdatabase + ".osd_trn_tservicerequest a " +
                                        " left join " + lsdatabase + ".adm_mst_tuser b on a.created_by = b.user_gid " +
                                        " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                                        " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                                        " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                            " where servicerequest_gid='" + values.servicerequest_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                                lsactivity_name = objODBCDatareader["activity_name"].ToString();
                                request_title = objODBCDatareader["request_title"].ToString();
                                lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                                lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                                lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                                lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                                lsrequest_status = objODBCDatareader["request_status"].ToString();
                                assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                                lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                                lsrequest_description = objODBCDatareader["request_description"].ToString();
                                lsgetapproval_remarks = objODBCDatareader["getapproval_remarks"].ToString();
                                    lsassigndedtl = objODBCDatareader["assigndedtl"].ToString();
                                    lsraiseddtl = objODBCDatareader["raised_by"].ToString() + " / " + objODBCDatareader["created_date"].ToString();
                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(user_firstname, ' ', user_lastname, '/', user_code) as requested_by from " + lsdatabase + ".adm_mst_tuser where user_gid='" + lsuser_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsrequested_by = objODBCDatareader["requested_by"].ToString();
                                }
                                objODBCDatareader.Close();
                            string lsemployee_gid;
                            lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + user_gid + "'");

                            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                            lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                      "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
                      "  from adm_mst_tmodule2employee a " +
                      "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                      "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
                      "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                      "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                      "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                      "  where a.module_gid ='"+ lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'"+
                      "  group by a.employeereporting_to ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                                lslevel_zero = objODBCDatareader["level_zero"].ToString();
                                lslevel_one = objODBCDatareader["level_one"].ToString();
                                lsemployee_number = objODBCDatareader["employee_number"].ToString();
                                //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                                //values.level_zero = objODBCDatareader["level_zero"].ToString();
                                //values.level_one = objODBCDatareader["level_one"].ToString();
                                //values.employee_number = objODBCDatareader["employee_number"].ToString();
                            }
                            //sub = "Request Initiation Mail";
                            sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Request Initiation Mail ";
                                body = "Dear Sir/Madam,  <br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + HttpUtility.HtmlEncode(lsrequested_by) + " has been Initiated the Approval Request and the details are as follows,<br />";
                                body = body + "<br />";
                            body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                            body = body + "<br />";                            
                            //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                            //body = body + "<br />";
                            body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Remarks :</b> " + HttpUtility.HtmlEncode(lsgetapproval_remarks) + "<br />";
                                body = body + "<br />";
                                body = body + "Kindly <a href=" + ConfigurationManager.AppSettings["approvalurl"].ToString() + "?id=" + lstoken + "> Click Here</a> and do the needful.<br />";
                                body = body + "<br />";
                                body = body + "<b>Thanks & Regards, </b> ";
                                body = body + "<br />";
                                body = body + "<b> Team Business Process </b> ";
                                body = body + "<br />";
                                //cc_mailid = "";
                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                message.To.Add(new MailAddress(lsto_mail));
                            //message.CC.Add(cc);


                            if (cc != null & cc != string.Empty & cc != "")
                            {
                                lsCCReceipients = cc.Split(',');
                                if (cc.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc));
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
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.Send(message);

                                values.status = true;
                                if (values.status == true)
                                {
                                    msSQL = "Insert into " + lsdatabase + ".osd_trn_tmailcount( " +
                                    " servicerequest_gid," +
                                    " from_mail," +
                                    " to_mail," +
                                    //" cc_mail," +
                                    " mail_status," +
                                    " mail_senddate, " +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + values.servicerequest_gid + "'," +
                                    "'" + ls_username + "'," +
                                    "'" + lsto_mail + "'," +
                                    //"'" + lscc_mail + "'," +
                                    "'Request Initiation Mail'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    "'" + lsuser_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }

                        }
                    


                }

            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
        public void DaGetRequestDtl(string approval_token, requesttokendtl values)
        {
            msSQL = "select servicerequest_gid,approval_type,approval_status,approval_token from " + lsdatabase + ".osd_trn_trequestapproval where approval_token='" + approval_token + "' and approval_status='Pending'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = " select request_refno, a.servicerequest_gid, activity_name, request_title, c.requestapproval_remarks, c.hierary_level, c.approval_type, " +
                        " concat(assigned_supportteamname, ' - ', assigned_membername) as assigndedtl,getapproval_remarks, " +
                        " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                        " from " + lsdatabase + ".osd_trn_tservicerequest a " +
                        " left join " + lsdatabase + ".adm_mst_tuser b on a.created_by = b.user_gid " +
                        " left join " + lsdatabase + ".osd_trn_trequestapproval c on a.servicerequest_gid=c.servicerequest_gid " +
                        " where c.approval_token='" + approval_token + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.request_refno = objODBCDatareader["request_refno"].ToString();
                    values.servicerequest_gid = objODBCDatareader["servicerequest_gid"].ToString();
                    values.activity_name = objODBCDatareader["activity_name"].ToString();
                    values.request_title = objODBCDatareader["request_title"].ToString();
                    values.getapproval_remarks = objODBCDatareader["requestapproval_remarks"].ToString();
                    values.assigned_dtl = objODBCDatareader["assigndedtl"].ToString();
                    values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                    values.approval_type = objODBCDatareader["approval_type"].ToString();
                    values.raised_dtl = objODBCDatareader["raised_by"].ToString() + " / " + objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            else
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Approved / Rejected..!";
            }
            
            
        }

        public void DaGetApprovalSummary(requestapproval values, string employee_gid)
        {
            msSQL = " select a.requestapproval_gid, b.servicerequest_gid, a.approval_gid, a.hierary_level, a.approval_status, a.approval_remarks, b.request_refno, b.request_title, b.activity_name, a.seqhierarchy_view, " + 
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,"+ 
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as approvalreq_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as approvalreq_date"+
                    " from osd_trn_trequestapproval a "+
                    " left join osd_trn_tservicerequest b on a.servicerequest_gid = b.servicerequest_gid "+
                    " left join adm_mst_tuser c on c.user_gid = b.created_by "+
                    " left join adm_mst_tuser d on d.user_gid = a.created_by "+
                    " where approval_gid ='" + employee_gid + "'  and a.approval_status = 'Pending' " +
                    " and(case when a.approval_type = 'sequence' then  a.seqhierarchy_view = 'Y' "+
                    " else a.seqhierarchy_view = 'N'  end) order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getActivityList = new List<approvalsummarylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getActivityList.Add(new approvalsummarylist
                    {
                        requestapproval_gid = dt["requestapproval_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        approvalreq_by = dt["approvalreq_by"].ToString(),
                        approvalreq_date = dt["approvalreq_date"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        hierary_level = dt["hierary_level"].ToString(),
                    });
                    values.approvalsummarylist = getActivityList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select a.requestapproval_gid, b.servicerequest_gid, a.approval_gid, a.approval_status, a.approval_remarks, b.request_refno, b.request_title, b.activity_name, a.seqhierarchy_view, " +
        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date," +
        " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as approvalreq_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as approvalreq_date" +
        " from osd_trn_trequestapproval a " +
        " left join osd_trn_tservicerequest b on a.servicerequest_gid = b.servicerequest_gid " +
        " left join adm_mst_tuser c on c.user_gid = b.created_by " +
        " left join adm_mst_tuser d on d.user_gid = a.created_by " +
        " where approval_gid ='" + employee_gid + "' and (a.approval_status='Approved' or a.approval_status='Rejected' or a.approval_status='Cancelled') order by a.created_date desc";
        //" and(case when a.approval_type = 'sequence' then  a.seqhierarchy_view = 'N' " +
        //" else a.seqhierarchy_view = 'N'  end)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getApprovalList = new List<approvalcompletedlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getApprovalList.Add(new approvalcompletedlist
                    {
                        requestapproval_gid = dt["requestapproval_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        approvalreq_by = dt["approvalreq_by"].ToString(),
                        approvalreq_date = dt["approvalreq_date"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                    });
                    values.approvalcompletedlist = getApprovalList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetApprovaldetails(requestapproval values, string employee_gid, string requestapproval_gid)
        {
            msSQL = " select a.approval_token, a.approval_type, a.hierary_level, b.servicerequest_gid,b.request_description, a.approval_gid, a.approval_status, a.approval_remarks, b.request_refno, b.request_title, b.activity_name, a.seqhierarchy_view, " +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by,date_format(b.created_date, '%d-%m-%Y %h:%i %p') as created_date,requestapproval_remarks," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as approvalreq_by,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as approvalreq_date" +
                    " from osd_trn_trequestapproval a " +
                    " left join osd_trn_tservicerequest b on a.servicerequest_gid = b.servicerequest_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.created_by " +
                    " left join adm_mst_tuser d on d.user_gid = a.created_by " +
                    " where approval_gid ='" + employee_gid + "' and a.requestapproval_gid='" + requestapproval_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.request_refno = objODBCDatareader["request_refno"].ToString();
                values.servicerequest_gid = objODBCDatareader["servicerequest_gid"].ToString();
                values.request_title = objODBCDatareader["request_title"].ToString();
                values.approval_token = objODBCDatareader["approval_token"].ToString();
                values.approval_type = objODBCDatareader["approval_type"].ToString();
                values.hierary_level = objODBCDatareader["hierary_level"].ToString();
                values.approvalreq_by = objODBCDatareader["approvalreq_by"].ToString();
                values.approvalreqdate = objODBCDatareader["approvalreq_date"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.requestapproval_remarks = objODBCDatareader["requestapproval_remarks"].ToString();
                values.request_description = objODBCDatareader["request_description"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;


           
        }

        public void DaGetRHApprovalSummary(requestapproval values, string employee_gid)
        {
            msSQL = " select bankalertrefundapprl_gid,ticketref_no,customer_urn,customer_gid,assignedrm_name,approval_status,bankalert2allocated_gid " +
                    " from osd_trn_tbankalertrefundapprl where assignedrh_gid ='" + employee_gid + "'  and approval_status = 'Pending' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapprovalList = new List<rhapprovalsummarylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapprovalList.Add(new rhapprovalsummarylist
                    {
                        bankalertrefundapprl_gid = dt["bankalertrefundapprl_gid"].ToString(),
                        ref_no = dt["ticketref_no"].ToString(),
                        customerurn = dt["customer_urn"].ToString(),
                        customername = dt["customer_gid"].ToString(),
                        assignedrmname = dt["assignedrm_name"].ToString(),
                        approvalStatus = dt["approval_status"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),

                    });
                    values.rhapprovalsummarylist = getapprovalList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select bankalertrefundapprl_gid,ticketref_no,customer_urn,customer_gid,assignedrm_name,approval_status,bankalert2allocated_gid " +
                    " from osd_trn_tbankalertrefundapprl where ( assignedrh_gid ='" + employee_gid + "' )  and (approval_status in ('Approved','Rejected') ) ";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapprovalcompList = new List<rhapprovalcompletedlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapprovalcompList.Add(new rhapprovalcompletedlist
                    {
                        bankalertrefundapprl_gid = dt["bankalertrefundapprl_gid"].ToString(),
                        ref_no = dt["ticketref_no"].ToString(),
                        customerurn = dt["customer_urn"].ToString(),
                        customername = dt["customer_gid"].ToString(),
                        assignedrmname = dt["assignedrm_name"].ToString(),
                        approvalStatus = dt["approval_status"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),

                    });
                    values.rhapprovalcompletedlist = getapprovalcompList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetRHApprovaldetails(requestapproval values, string employee_gid, string bankalertrefundapprl_gid)
        {
            msSQL = " select bankalertrefundapprl_gid,ticketref_no,customer_urn,customer_gid,assignedrm_name,approval_status from osd_trn_tbankalertrefundapprl " +
                    " where assignedrh_gid ='" + employee_gid + "' and bankalertrefundapprl_gid='" + bankalertrefundapprl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.bankalertrefundapprl_gid = objODBCDatareader["bankalertrefundapprl_gid"].ToString();
                values.ref_no = objODBCDatareader["ticketref_no"].ToString();
                values.customerurn = objODBCDatareader["customer_urn"].ToString();
                values.customername = objODBCDatareader["customer_gid"].ToString();
                values.assignedrmname = objODBCDatareader["assignedrm_name"].ToString();
                values.approvalStatus = objODBCDatareader["approval_status"].ToString();             
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaPostRHApprovalUpdate(requestapproval values,string employee_gid)
        {
            msSQL = " update osd_trn_tbankalertrefundapprl set approval_status ='Approved'," +                      
                       " rh_remarks='" + values.rh_remarks.Replace("'", @"\'") + "' ," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where bankalertrefundapprl_gid='" + values.bankalertrefundapprl_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            { 
                msSQL = " select bankalert2allocated_gid from osd_trn_tbankalertrefundapprl " +
                        " where bankalertrefundapprl_gid = '" + values.bankalertrefundapprl_gid + "' ";

                string bankalert2allocated = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update osd_trn_tbankalert2allocated set " +
                        " seen_flag='N', " +
                        " allocated_status='Completed' " +
                        " where bankalert2allocated_gid='" + bankalert2allocated + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "RH Approved Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostRHApprovalReject(requestapproval values, string employee_gid)

        {
            msSQL = " Select bankalert2allocated_gid  from osd_trn_tbankalertrefundapprl " +
               " where bankalertrefundapprl_gid='" + values.bankalertrefundapprl_gid + "' ";
            string bankallocated_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select ticketref_no,relationshipmanagerlevel_gid, relationshipmanagerlevel_name, regionalheadlevel_gid, regionalheadlevel_name,customer_urn,customer_name " +
              " from osd_trn_tbankalert2allocated where bankalert2allocated_gid ='" + bankallocated_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsref_no = objODBCDatareader["ticketref_no"].ToString();
                lsrm_gid = objODBCDatareader["relationshipmanagerlevel_gid"].ToString();
                lsrm_name = objODBCDatareader["relationshipmanagerlevel_name"].ToString();
                lsrh_gid = objODBCDatareader["regionalheadlevel_gid"].ToString();
                lsrh_name = objODBCDatareader["regionalheadlevel_name"].ToString();
                lscustomerurn = objODBCDatareader["customer_urn"].ToString();
                lscustomer_name = objODBCDatareader["customer_name"].ToString();
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("BRHR");
            msSQL = " insert into osd_trn_tbankalertrefundrejct (" +
                                    " bankalertrefundrejct_gid," +
                                    " bankalert2allocated_gid," +
                                    " ticketref_no," +
                                    " customer_gid," +
                                    " customer_urn," +
                                    " assignedrm_gid," +
                                    " assignedrm_name," +
                                    " assignedrh_gid," +
                                    " assignedrh_name," +
                                    " rh_remarks," +
                                    " approval_status," +
                                    " rejected_by," +
                                    " rejected_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + bankallocated_gid + "'," +
                                    "'" + lsref_no + "'," +
                                    "'" + lscustomer_name + "'," +
                                    "'" + lscustomerurn + "'," +
                                    "'" + lsrm_gid + "'," +
                                    "'" + lsrm_name + "'," +
                                    "'" + lsrh_gid + "'," +
                                    "'" + lsrh_name + "'," +
                                    "'" + values.rh_remarks.Replace("'", @"\'") + "'," +
                                    "'Rejected'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           

            if (mnResult == 1)
            {
                msSQL = " update osd_trn_tbankalertrefundapprl set approval_status ='Rejected'," +
                       " rh_remarks='" + values.rh_remarks.Replace("'", @"\'") + "' ," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where bankalertrefundapprl_gid='" + values.bankalertrefundapprl_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update osd_trn_tbankalert2allocated set " +
                      " seen_flag='N', " +
                      " allocated_status='RH Rejected' " +
                      " where bankalert2allocated_gid='" + bankallocated_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "RH Rejected Successfully.";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public bool DaGetRHApprovalDtlsByToken(requestapproval values ,string bankalert2allocated_gid)
        {
            msSQL = " select bankalertrefundapprl_gid from osd_trn_tbankalertrefundapprl where bankalert2allocated_gid ='" + bankalert2allocated_gid + "'";
            string lsbankalert2allocated;
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbankalert2allocated = objODBCDatareader["bankalertrefundapprl_gid"].ToString();
            
                msSQL = " select bankalertrefundapprl_gid, assignedrh_name, rh_remarks,approval_status," +
                    " concat(c.user_firstname,'', c.user_lastname,' / ', c.user_code) as created_by, " +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date" +
                    " from osd_trn_tbankalertrefundapprl a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where bankalert2allocated_gid = '" + bankalert2allocated_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapprovaldetails = new List<rhapprovaldetails>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapprovaldetails.Add(new rhapprovaldetails
                        {
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            assignedrh_name = (dr_datarow["assignedrh_name"].ToString()),
                            rh_remarks = (dr_datarow["rh_remarks"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            approval_date = (dr_datarow["updated_date"].ToString()),
                            bankalertrefundapprl_gid = (dr_datarow["bankalertrefundapprl_gid"].ToString()),
                        });
                    }
                    values.rhapprovaldetails = getapprovaldetails;
                }
                dt_datatable.Dispose();
            }
            
            return true;
            
        }


        public bool DaGetRHRejectedDtlsByToken(requestapproval values, string bankalert2allocated_gid)
        {
            msSQL = " select bankalertrefundapprl_gid from osd_trn_tbankalertrefundapprl where bankalert2allocated_gid ='" + bankalert2allocated_gid + "'";
            string lsbankalert2allocated;
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsbankalert2allocated = objODBCDatareader["bankalertrefundapprl_gid"].ToString();

                msSQL = " select bankalertrefundrejct_gid, assignedrh_name, rh_remarks,approval_status," +
                    " concat(c.user_firstname, '', c.user_lastname, ' / ', c.user_code) as rejected_by, "+ 
                    " date_format(a.rejected_date, '%d-%m-%Y %h:%i %p') as rejected_date " +
                    " from osd_trn_tbankalertrefundrejct a " +
                    " left join hrm_mst_temployee b on a.rejected_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where bankalert2allocated_gid = '" + bankalert2allocated_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrejecteddetails = new List<rhrejecteddetails>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getrejecteddetails.Add(new rhrejecteddetails
                        {
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            assignedrh_name = (dr_datarow["assignedrh_name"].ToString()),
                            rh_remarks = (dr_datarow["rh_remarks"].ToString()),
                            created_by = (dr_datarow["rejected_by"].ToString()),
                            approval_date = (dr_datarow["rejected_date"].ToString()),
                            bankalertrefundapprl_gid = (dr_datarow["bankalertrefundrejct_gid"].ToString()),
                        });
                    }
                    values.rhrejecteddetails = getrejecteddetails;
                }
                dt_datatable.Dispose();
            }

            return true;

        }


    }
}