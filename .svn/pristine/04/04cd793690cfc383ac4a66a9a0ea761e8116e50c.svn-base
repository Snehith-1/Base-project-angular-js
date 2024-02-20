using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.storage.Functions;
using System.Net.Mail;
using System.Net;
using ems.osd.Models;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnCustomerQueryMgmt
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable, dt_child;
        string msSQL, msGetGid, msGetGid1;
        OdbcDataReader objODBCDatareader;
        int mnResult;
        int ls_port;
        DataTable dt_table;
        string[] lsCCReceipients;
        string cc, commoncc;
        string cc_mailid = string.Empty;
        string ccmail_id = string.Empty;
        string ticketref_no, lsqueryraisedbydtl, lsqueryraisedfordtl, lsqueryraisedondtl = string.Empty;
        string frommail_id, sub, from_mailid, tomail_id, lsqueryraisedby, contactperson, customer_name, body, message, ls_server, ls_username, ls_password, lscontent = string.Empty;
        private string email_gid;
        result objResult = new Models.result();
        HttpPostedFile httpPostedFile;
        string msGetChildGid = string.Empty;
        DaOsdTrnSentMail objSentMail = new DaOsdTrnSentMail();
        bool lsstatus;
        string lsto_address = string.Empty;
        string lscc_address = string.Empty;
        string lsbcc_address = string.Empty;
        string lsmessage_id = string.Empty;
        string lsreference_id = string.Empty;
        string lssubject = string.Empty;
        string lsmailbody = string.Empty;
        string lsticketref_no, lsemail_subject;
        string lsquerytransfer_by, lsqueryclosedbydtl, lsqueryclosedondtl, lstransfer_date;
        string lsattachment_flag = "N";
        string lsassign_remarks = string.Empty;
        string lstransfer_remarks = string.Empty;

        public void DaCustomerQueryPendingSummary(MdlQueryPending values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.status,CONCAT(FLOOR((DATEDIFF(now(), a.email_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.email_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.email_date)), 'Mins')  as aging ,a.seen_flag" +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.status ='Pending' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryPendingList = dt_datatable.AsEnumerable().Select(row => new QueryPendingList
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    seen_flag = row["seen_flag"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }


        }

        public void DaCustomerQueryView(string email_gid, MdlQueryview values)
        {

            msSQL = "select a.ticketref_no,a.email_gid,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date,a.email_subject,a.email_content,a.status,CONCAT(FLOOR(HOUR(TIMEDIFF(now(), a.email_date)) / 24), ' days ',MOD(HOUR(TIMEDIFF(now(), a.email_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.email_date)), 'Mins')  as aging,a.email_to, a.cc,a.bcc,a.from_mailaddress " +
                    " FROM osd_trn_tmaildetails a" +
                    " where a.email_gid='" + email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaGetCustomerQueryAttachments(string email_gid, MdlQueryAttachments values)
        {
            msSQL = " SELECT mailattachment_gid,document_path,document_name, substring_index(document_name,'.',-1) as document_extension" +
                    " FROM osd_trn_tmaildetailsattachement " +
                    " WHERE email_gid = '" + email_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAttachmentsList = dt_datatable.AsEnumerable().Select(row => new QueryAttachmentsList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaPostTicketAssign(Ticketassign values, string user_gid)
        {
            if (values.assigned_remarks == "" || values.assigned_remarks == null)
            {
                lsassign_remarks = "";
            }
            else
            {
                lsassign_remarks = values.assigned_remarks;
            }
            msGetGid = objcmnfunctions.GetMasterGID("TICA");
            
            msSQL = " insert into osd_trn_tticketassign (" +
                    " ticketassign_gid," +
                    " email_gid, " +
                    " employee_gid," +
                    " employee_name," +
                    " allotted_on," +
                    " allotted_by," +
                    " assigned_remarks ," +
                    " status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.email_gid + "', " +
                    "'" + values.employee_gid + "'," +
                    "'" + values.employee_name + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
                    "'" + lsassign_remarks.Replace("'", "") + "'," +
                    "'Assigned'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/CustomerAlertLOG/CustomerAlertAssignsubmit/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
            if ((!System.IO.Directory.Exists(lspath)))
                System.IO.Directory.CreateDirectory(lspath);



            lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
            sw.WriteLine("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********Exception-" + "error" + "*********Query-" + msSQL);
            sw.Close();

            if (mnResult !=0) { 
            msSQL = " INSERT INTO osd_trn_tauditlog(" +
                           " email_gid," +
                           " action_taken," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + values.email_gid + "', " +
                           "'Assigned'," +
                           "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Assigned Successfully..!";

                    msSQL = " update osd_trn_tmaildetails set " +
                            " status= 'Assigned' " +
                            " where email_gid='" + values.email_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    try
                    {
                        msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                      " FROM osd_trn_tmailcredentials" +
                                      " WHERE 1=1";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select email_id from hrm_mst_tdepartment where department_name='Operations'";
                        dt_table = objdbconn.GetDataTable(msSQL);
                        foreach (DataRow dr_datarow in dt_table.Rows)
                        {
                            commoncc += "" + (dr_datarow["email_id"].ToString()) + ",";
                        }
                        dt_table.Dispose();
                        commoncc += cc;

                        cc = commoncc.TrimEnd(',');

                        msSQL = "select b.employee_emailid from osd_trn_tticketassign a " +
                            " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid where ticketassign_gid='" + msGetGid + "'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as allotted_for, email_from as allotted_by," +
                              " date_format(b.allotted_on,'%d-%m-%Y %h:%i %p') as allotted_on from osd_trn_tmaildetails a " +
                                " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                                " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                                " where b.ticketassign_gid ='" + msGetGid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                            lsqueryraisedbydtl = objODBCDatareader["allotted_by"].ToString();
                            lsqueryraisedfordtl = objODBCDatareader["allotted_for"].ToString();
                            lsqueryraisedondtl = objODBCDatareader["allotted_on"].ToString();
                        }
                        objODBCDatareader.Close();

                        //sub = " New Query Assigned ";
                        sub = " " + HttpUtility.HtmlEncode(ticketref_no) + "  New Query Assigned ";

                        lscontent = HttpUtility.HtmlEncode(values.content);

                        body = "Dear Sir/Madam, <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " A query raised by our client and assigned to you, the details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "<b>Ticket Ref No :</b> " + HttpUtility.HtmlEncode(ticketref_no) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Query Raised By  :</b> " + HttpUtility.HtmlEncode(lsqueryraisedbydtl) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Query Raised On  :</b> " + HttpUtility.HtmlEncode(lsqueryraisedondtl) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Query Title:</b> New Query Assigned <br />";
                        body = body + "<br />";
                        body = body + " Please ensure that a timely response is made to our customers.<br />";
                        body = body + "<br />";
                        body = body + "<b> Regards, </b> <br />";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(lsqueryraisedfordtl) + "<br />";
                        body = body + "<br />";

                        from_mailid = ConfigurationManager.AppSettings["customermailalert"].ToString();

                        cc_mailid = "";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(from_mailid);
                        message.To.Add(new MailAddress(tomail_id));
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
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);

                        values.status = true;

                        //if (values.status == true)
                        //{
                        //    msSQL = "Insert into osd_trn_tmailcount( " +
                        //    " servicerequest_gid," +
                        //    " from_mail," +
                        //    " to_mail," +
                        //    " cc_mail," +
                        //    " mail_status," +
                        //    " mail_senddate, " +
                        //    " created_by," +
                        //    " created_date)" +
                        //    " values(" +
                        //    "'" + msGetGid + "'," +
                        //    "'" + ls_username + "'," +
                        //    "'" + tomail_id + "'," +
                        //    "'" + cc + "'," +
                        //    "'Service Request Assigned'," +
                        //    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        //    "'" + user_gid + "'," +
                        //    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //}

                    }
                    catch (Exception ex)
                    {
                        values.message = ex.ToString();
                        values.status = false;

                    }
                }
            
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
               
                
            }

            
        }

        public void DaCustomerQueryAssignSummary(MdlQueryAssign values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date,a.from_mailaddress," +
                    " a.email_subject, a.status, CONCAT(FLOOR((DATEDIFF(now(), a.email_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.email_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.email_date)), 'Mins')  as aging " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " WHERE  a.status <> 'Close' and a.status <> 'Pending' ORDER BY b.allotted_on desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignList = dt_datatable.AsEnumerable().Select(row => new QueryAssignList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    from_mailaddress = row["from_mailaddress"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public void DaCustomerAssignedQuery360(string email_gid, MdlAssignedQuery360 values)
        {

            msSQL = " SELECT a.email_gid, a.ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assignedto_date, " +
                    " a.email_from, a.email_date, a.status, CONCAT(FLOOR((DATEDIFF(now(), a.email_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.email_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.email_date)), 'Mins')  as aging, a.email_subject, a.email_content,a.email_to, a.cc,a.bcc,a.from_mailaddress,b.assigned_remarks " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " where a.email_gid='" + email_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.email_gid = objODBCDatareader["email_gid"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_to = objODBCDatareader["assigned_to"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.status = objODBCDatareader["status"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date, a.status, CONCAT(FLOOR((DATEDIFF(now(), a.email_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.email_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.email_date)), 'Mins')  as aging,a.cc,a.bcc, a.email_to " +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.email_gid = '" + email_gid + "' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.Query360list = dt_datatable.AsEnumerable().Select(row => new Query360list
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    email_to = row["email_to"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.email_gid, a.document_path,a.document_name," +
                       " substring_index(a.document_name,'.',-1) as document_extension, a.mailattachment_gid " +
                       " FROM osd_trn_tmaildetailsattachement a" +
                       " WHERE a.email_gid = '" + email_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaGetAuditLogSummary(string email_gid, MdlAuditLogHistory values)
        {
            msSQL = " SELECT a.action_taken,DATE_FORMAT(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                  " concat(b.user_firstname,'',b.user_lastname,'/',b.user_code) as created_by" +
                  " FROM osd_trn_tauditlog a" +
                  " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid" +
                  " WHERE a.email_gid='" + email_gid + "' AND a.created_by NOT IN (SELECT user_gid FROM adm_mst_tuser WHERE user_gid ='U1' or user_gid='SUSM1907240067') " +
                  " ORDER BY a.created_date DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAuditLog = dt_datatable.AsEnumerable().Select(row => new MdlAuditLog
                {
                    action_taken = row["action_taken"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),

                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Record Found";

            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record";
            }

        }

        public void DaPostAuditView(string email_gid, string user_gid)
        {
            msSQL = " INSERT INTO osd_trn_tauditlog(" +
                        " email_gid," +
                        " action_taken," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + email_gid + "'," +
                        "'View'," +
                        "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }

        public void DaCustomerAssignedQuerySummary(string employee_gid, MdlAssignedQuery values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date,a.from_mailaddress," +
                    " a.email_subject, b.status,CONCAT(FLOOR((DATEDIFF(now(), a.email_date))), ' days ',MOD(HOUR(TIMEDIFF(now(), a.email_date)), 24), ' Hrs ', MINUTE(TIMEDIFF(now(), a.email_date)), 'Mins')  as aging, b.employee_gid " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " WHERE a.status in ('Assigned','Transfer') AND b.employee_gid= '" + employee_gid + "'" +
                    " ORDER BY b.allotted_on DESC";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.AssignedQueryList = dt_datatable.AsEnumerable().Select(row => new AssignedQueryList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    from_mailaddress = row["from_mailaddress"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public result DaPostEmailSignature(MdlEmailSignature values, string user_gid)
        {
            result objResult = new Models.result();

            msSQL = " SELECT emailsignautre_gid FROM osd_trn_temailsignature" +
                    " WHERE created_by='" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == false)
            {
                objODBCDatareader.Close();
                msGetGid = objcmnfunctions.GetMasterGID("ESIG");

                msSQL = " INSERT INTO osd_trn_temailsignature(" +
                  " emailsignautre_gid," +
                  " emailsignature," +
                  " created_by)" +
                  " VALUES(" +
                  "'" + msGetGid + "'," +
                  "'" + values.emailsignature.Replace("'", "\'") + "'," +
                  "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Email Signature Created Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occureed";
                }
                return objResult;

            }
            else
            {
                objODBCDatareader.Close();

                msSQL = " UPDATE osd_trn_temailsignature SET" +
                   " emailsignature='" + values.emailsignature.Replace("'", "\'") + "'," +
                   " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                   " WHERE created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    objResult.status = true;
                    objResult.message = "Email Signature Updated Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error Occureed";
                }
                return objResult;

            }


        }

        public void DaGetEmailSignature(string user_gid, MdlEmailSignature values)
        {
            msSQL = " SELECT emailsignature" +
                    " FROM osd_trn_temailsignature" +
                    " WHERE created_by='" + user_gid + "'";
            values.emailsignature = objdbconn.GetExecuteScalar(msSQL);
        }

        public result DaPostDecision(MdlDecisionhistory values, string user_gid)
        {
            msSQL = " UPDATE osd_trn_tmaildetails SET" +
                    " status='" + values.decision + "'," +
                    " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                    " WHERE email_gid='" + values.email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("TDEC");

            msSQL = " INSERT INTO osd_trn_tticketdecision(" +
                              " decision_gid," +
                              " decision," +
                              " email_gid," +
                              " remarks," +
                              " created_date," +
                              " created_by)" +
                              " VALUES(" +
                              "'" + msGetGid + "'," +
                              "'" + values.decision + "'," +
                              "'" + values.email_gid + "'," +
                              //"'" + values.remarks.Replace("'", "") + "'," +
                              "'" + values.remarks.Replace("'", "\\'") + "'," +
                              "current_timestamp," +
                              "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                msSQL = " INSERT INTO osd_trn_tauditlog(" +
                             " email_gid," +
                             " action_taken," +
                             " created_by)" +
                             " VALUES(" +
                             "'" + values.email_gid + "'," +
                             "'" + values.decision + "'," +
                             "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.decision == "Reply" || values.decision == "Forward")
                {
                    msSQL = "SELECT ticketref_no FROM osd_trn_tmaildetails WHERE email_gid = '" + values.email_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        objODBCDatareader.Read();
                        ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                    }
                    objODBCDatareader.Close();

                    lsmailbody = lsmailbody + "<br/>";
                    lsmailbody = lsmailbody + "<br/>";
                    lsmailbody = lsmailbody + values.mailcontent.Replace("'", "") + "<br/>";

                    lsmailbody = lsmailbody + "<br/>";
                    msSQL = "select email_subject from osd_trn_tmaildetails where email_gid='" + values.email_gid + "'";
                    string lssubject = objdbconn.GetExecuteScalar(msSQL);

                    lsstatus = objSentMail.DaOSDReplyMail(values.email_gid, values.decision_gid, values.decision, values.message_id, values.reference_id, values.tomail_id, values.ccmail_id, values.bccmail_id, user_gid, lssubject, lsmailbody);
                    if (lsstatus == true && values.decision == "Reply")
                    {
                        msSQL = " DELETE FROM osd_tmp_tmaildetailsattachement" +
                           " WHERE created_by='" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        objResult.status = true;
                        objResult.message = "Mail Replied Successfully";
                    }
                    else if (lsstatus == true && values.decision == "Forward")
                    {
                        msSQL = " DELETE FROM osd_tmp_tmaildetailsattachement" +
                           " WHERE created_by='" + user_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        objResult.status = true;
                        objResult.message = "Mail Forwarded Successfully";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error Occured While Mail Sent";
                    }
                }

                else if (values.decision == "Close")
                {
                    msSQL = " SELECT pop_server, pop_port, pop_username, pop_password FROM osd_trn_tmailcredentials";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt16(objODBCDatareader["pop_port"].ToString());
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    //msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='OSD-CCMail'";
                    //cc = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select cc from osd_trn_tmaildetails where email_gid='" + values.email_gid + "'";
                    cc = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select from_mailaddress from osd_trn_tmaildetails where email_gid='" + values.email_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select ticketref_no, email_subject, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as closed_by, " +
                          " date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as closed_date " +
                          " from osd_trn_tmaildetails a " +
                          " left join adm_mst_tuser b on b.user_gid = a.updated_by " +
                          " where email_gid='" + values.email_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsticketref_no = objODBCDatareader["ticketref_no"].ToString();
                        lsemail_subject = objODBCDatareader["email_subject"].ToString();
                        lsqueryclosedbydtl = objODBCDatareader["closed_by"].ToString();
                        lsqueryclosedondtl = objODBCDatareader["closed_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    //sub = "Query Closed";
                    sub = " " + lsticketref_no + "  Query Closed ";
                    lsmailbody = "Dear Sir/Madam,  <br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "Greetings from samunnati,<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + " Your Query is resolved,and the details are as follows,<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "<b>Ticket Ref No :</b> " + lsticketref_no + "<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "<b>Closed By :</b> " + lsqueryclosedbydtl + "<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "<b>Closed On :</b> " + lsqueryclosedondtl + "<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "<b>Query Title:</b> Query Closed<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "We believe the details provided, answers your query completely. You can always reopen the ticket and request for more details if required.<br />";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "<b>Sincerely, </b><br /> ";
                    lsmailbody = lsmailbody + "<br />";
                    lsmailbody = lsmailbody + "<b> Team Samunnati </b> ";
                    lsmailbody = lsmailbody + "<br />";

                    from_mailid = ConfigurationManager.AppSettings["customermailalert"].ToString();

                    msGetGid = objcmnfunctions.GetMasterGID("OSDM");
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
                  "'" + msGetGid + "'," +
                  "'" + values.decision_gid + "'," +
                  "'" + values.decision + "'," +
                  "'" + values.email_gid + "'," +
                  "'" + values.subject + "'," +
                  "'" + from_mailid + "'," +
                  "'" + tomail_id + "'," +
                  "'" + cc + "'," +
                  "'" + values.bccmail_id + "'," +
                  "'" + values.mailcontent.Replace("'", "") + "'," +
                  "'" + lsattachment_flag + "'," +
                  "current_timestamp," +
                  "'" + user_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        objResult.status = true;
                        objResult.message = "Ticket Closed Successfully";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error Occured While Inserting the data";
                    }
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(from_mailid);
                    message.To.Add(new MailAddress(tomail_id));
                    //message.CC.Add(new MailAddress(cc));
                    if (cc != null & cc != string.Empty & cc != "")
                    {
                        lsCCReceipients = cc.Split(';');
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
                    message.IsBodyHtml = true;
                    message.Body = lsmailbody;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    objResult.status = true;
                }
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured While Inserting Data";
            }

            return objResult;


        }

        public result DaPostUploadAttachment(HttpRequest httpRequest, string employee_gid, string user_gid)
        {

            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            String lspath;

            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
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
                            //string lsfile_gid = msdocument_gid + FileExtension;
                            string lsfile_gid = msdocument_gid;
                            FileExtension = Path.GetExtension(FileExtension).ToLower();
                            lsfile_gid = lsfile_gid + FileExtension;
                            Stream ls_readStream;
                            ls_readStream = httpPostedFile.InputStream;
                            // MemoryStream ms = new MemoryStream();
                            ls_readStream.CopyTo(ms);

                            byte[] bytes = ms.ToArray();
                            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                            {
                                objResult.message = "File format is not supported";
                                objResult.status = false;
                                return objResult;
                            }
                            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                            //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();

                            //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                            // lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "OSD/MailAttachment/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msSQL = " insert into osd_tmp_tmaildetailsattachement( " +
                                        " document_name ," +
                                        " document_path," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
                                        "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                        "'" + lspath + msdocument_gid + FileExtension + "'," +
                                        "'" + user_gid + "'," +
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
                        return objResult;

                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error Occured";

                        return objResult;
                    }
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = ex.Message;

                    return objResult;
                }
            }
        }

        public void DaGetMailAttachment(MdlcDocList objfileDtls, string user_gid)
        {

            msSQL = " SELECT mailattachment_gid, document_name,document_path FROM osd_tmp_tmaildetailsattachement WHERE created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<MdlDocDetails>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new MdlDocDetails
                    {
                        id = dt["mailattachment_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),

                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),

                    });
                }
                objfileDtls.MdlDocDetails = getDocList;
            }
            dt_datatable.Dispose();
        }

        public result DaPostMailAttachmentDelete(string id)
        {
            msSQL = " DELETE FROM osd_tmp_tmaildetailsattachement WHERE mailattachment_gid='" + id + "'";
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
            return objResult;
        }

        public void DaMailtempdelete(string user_gid)
        {
            msSQL = "delete from osd_tmp_tmaildetailsattachement where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                objResult.status = true;
            }
            else
            {
                objResult.status = false;
            }
        }

        public void DaGetTransferLog(string lsemail_gid, MdlTransferLogList values)
        {
            msSQL = " SELECT a.transferlog_gid,a.assignedto_name,a.transferby_name," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as transfered_date," +
                    " concat(b.user_firstname, b.user_lastname, '/', b.user_code) as created_by, a.transfer_remarks" +
                    " FROM osd_trn_ttransferlog a" +
                    " LEFT JOIN adm_mst_tuser b on b.user_gid = a.created_by" +
                    " WHERE a.email_gid = '" + lsemail_gid + "' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlTransferLog = dt_datatable.AsEnumerable().Select(row => new Models.MdlTransferLog
                {
                    transferlog_gid = row["transferlog_gid"].ToString(),
                    assignedby_name = row["assignedto_name"].ToString(),
                    transferby_name = row["transferby_name"].ToString(),
                    transfered_date = row["transfered_date"].ToString(),
                    created_by = row["created_by"].ToString(),
                    transfer_remarks = row["transfer_remarks"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record";
            }

        }

        public void DaPostTicketTransfer(MdlAssignTo values, string employee_gid, string user_gid)
        {

            msSQL = " SELECT employee_name,employee_gid" +
                       " FROM osd_trn_tticketassign" +
                       " WHERE email_gid='" + values.email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                string lscheckeremployee_name = string.Empty;
                string lscheckeremployee_gid = string.Empty;


                lscheckeremployee_name = objODBCDatareader["employee_name"].ToString();
                lscheckeremployee_gid = objODBCDatareader["employee_gid"].ToString();

                objODBCDatareader.Close();

                if (values.transfer_remarks == "" || values.transfer_remarks == null)
                {
                    lstransfer_remarks = "";
                }
                else
                {
                    lstransfer_remarks = values.transfer_remarks;
                }

                msGetGid = objcmnfunctions.GetMasterGID("TRNS");

                msSQL = " INSERT INTO osd_trn_ttransferlog(" +
                                                " transferlog_gid," +
                                                " email_gid," +
                                                " assignedto_name," +
                                                " assignedto_gid," +
                                                " transferby_gid," +
                                                " transferby_name," +
                                                " transfer_remarks," +
                                                " created_date, " +
                                                " created_by)" +
                                                " VALUES(" +
                                                "'" + msGetGid + "'," +
                                                "'" + values.email_gid + "'," +
                                                "'" + lscheckeremployee_name + "'," +
                                                "'" + lscheckeremployee_gid + "'," +
                                                "'" + values.employee_gid + "'," +
                                                "'" + values.employee_name + "'," +
                                                "'" + lstransfer_remarks.Replace("'", "") + "'," +
                                                "current_timestamp, " +
                                                "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " INSERT INTO osd_trn_tauditlog(" +
                        " email_gid," +
                        " action_taken," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + values.email_gid + "'," +
                        "'Transfer'," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    msSQL = " UPDATE osd_trn_tmaildetails SET" +
                            " status='Transfer'," +
                            " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                            " WHERE email_gid='" + values.email_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " UPDATE osd_trn_tticketassign SET" +
                            " employee_gid='" + values.employee_gid + "'," +
                            " employee_name='" + values.employee_name + "'," +
                            " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                            " WHERE email_gid='" + values.email_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Ticket Transferred Successfully...!";
                        try
                        {

                            msSQL = "SELECT ticketref_no FROM osd_trn_tmaildetails WHERE email_gid = '" + values.email_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                objODBCDatareader.Read();
                                lsticketref_no = objODBCDatareader["ticketref_no"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "SELECT employee_emailid, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                                 " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                                " WHERE employee_gid = '" + values.employee_gid + "' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                objODBCDatareader.Read();
                                lsto_address = objODBCDatareader["employee_emailid"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = " select allotted_by as gid, b.employee_emailid as mail from osd_trn_tticketassign a left join hrm_mst_temployee b on b.user_gid = a.allotted_by " +
                                " WHERE a.employee_gid = '" + values.employee_gid + "' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                objODBCDatareader.Read();
                                lscc_address = objODBCDatareader["mail"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "SELECT employee_emailid, concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as created_by " +
                               " FROM hrm_mst_temployee a left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                              " WHERE employee_gid = '" + employee_gid + "' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                objODBCDatareader.Read();
                                lsquerytransfer_by = objODBCDatareader["created_by"].ToString();
                            }
                            objODBCDatareader.Close();
                            msSQL = "SELECT date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date FROM osd_trn_ttransferlog WHERE email_gid = '" + values.email_gid + "' ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows)
                            {
                                objODBCDatareader.Read();
                                lstransfer_date = objODBCDatareader["created_date"].ToString();
                            }
                            objODBCDatareader.Close();
                            msSQL = "select email_subject from osd_trn_tmaildetails where email_gid='" + values.email_gid + "'";
                            string lssubject = objdbconn.GetExecuteScalar(msSQL);
                            
                            lssubject = " Query transferred  ";
                            lsmailbody = "Dear Sir/Madam,  <br />";
                            lsmailbody = lsmailbody + "<br />";
                            lsmailbody = lsmailbody + "Greetings,  <br />";
                            lsmailbody = lsmailbody + "<br />";
                            lsmailbody = lsmailbody + " A customer query is transferred to you, please ensure that a timely response is made to our customers.  <br />";
                            lsmailbody = lsmailbody + "<br/>";
                            lsmailbody = lsmailbody + "The ticket details are as follows, <br />";
                            lsmailbody = lsmailbody + "<br/>";
                            lsmailbody = lsmailbody + "<b>Ticket Ref No :</b> " + lsticketref_no + "<br />";
                            lsmailbody = lsmailbody + "<br/>";
                            lsmailbody = lsmailbody + "<b> Query transferred by  :</b> " + lsquerytransfer_by + "<br />";
                            lsmailbody = lsmailbody + "<br/>";
                            lsmailbody = lsmailbody + "<b> Query transferred On:</b> " + lstransfer_date + "<br />";
                            lsmailbody = lsmailbody + "<br/>";
                            lsmailbody = lsmailbody + "<b>Query Title:</b> Query transferred  <br />";
                            lsmailbody = lsmailbody + "<br />";
                            lsmailbody = lsmailbody + "<b> Thanks & Regards </b><br />,";
                            lsmailbody = lsmailbody + "<br />";
                            lsmailbody = lsmailbody + lsquerytransfer_by + "<br />";
                            lsmailbody = lsmailbody + "<br />";

                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                   " FROM osd_trn_tmailcredentials" +
                                   " WHERE 1=1";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            from_mailid = ConfigurationManager.AppSettings["customermailalert"].ToString();

                            int MailFlag;
                            MailFlag = objcmnfunctions.SendSMTP2(ls_username, ls_password, lsto_address, lssubject, lsmailbody, lscc_address, "", "");
                            
                            MailMessage objMailMessage = new MailMessage();
                            objMailMessage.From = new MailAddress(from_mailid);
                            // Set the recepient address of the mail message
                            objMailMessage.To.Add(new MailAddress(lsto_address));


                            if (lscc_address != null & lscc_address != string.Empty)
                            {
                                lsCCReceipients = lscc_address.Split(',');
                                if (lscc_address.Length == 0)
                                {
                                    objMailMessage.CC.Add(new MailAddress(lscc_address));
                                }
                                else
                                {
                                    foreach (string CCEmail in lsCCReceipients)
                                    {
                                        objMailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                                    }
                                }
                            }

                            //if (strBCC != null & strBCC != string.Empty)
                            //{
                            //    objMailMessage.Bcc.Add(new MailAddress(strBCC));
                            //}

                            objMailMessage.Subject = lssubject;
                            // Set the body of the mail message
                            objMailMessage.Body = lsmailbody;

                            // Set the format of the mail message body as HTML
                            objMailMessage.IsBodyHtml = true;
                            //  Set the priority of the mail message to normal
                            objMailMessage.Priority = MailPriority.Normal;
                            SmtpClient objSmtpClient = new SmtpClient();
                            objSmtpClient.Host = ls_server;
                            objSmtpClient.Port = ls_port;
                            objSmtpClient.EnableSsl = true;
                            objSmtpClient.UseDefaultCredentials = true;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            objSmtpClient.Credentials = new NetworkCredential(ls_username, ls_password);
                            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                           
                            try {
                                objSmtpClient.Send(objMailMessage);
                                MailFlag = 1;
                                    }
                            catch
                            {
                                MailFlag = 0;
                            }
                            if (MailFlag == 1)
                            {
                                msGetGid1 = objcmnfunctions.GetMasterGID("MAIL");
                                msSQL = " INSERT INTO osd_trn_tsentmail(" +
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
                                   "'" + msGetGid1 + "'," +
                                   "'" + msGetGid + "'," +
                                   "'Transfer'," +
                                   "'" + values.email_gid + "'," +
                                   "'" + lssubject + "'," +
                                   "'" + ls_username + "'," +
                                   "'" + lsto_address + "'," +
                                   "'" + lscc_address + "'," +
                                   "'" + lsbcc_address + "'," +
                                   "'" + lsmailbody.Replace("'", "") + "'," +
                                   "'Y'," +
                                   "current_timestamp," +
                                   "'" + user_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            else
                            {
                                values.status = false;
                                values.message = "Error Occured..!";
                            }
                        }
                        catch (Exception ex)
                        {
                            values.message = " Mail Not Sent ";
                            values.status = false;
                        }

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while Transfering the ticket...!";
                    }

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }
        }

        public void DaCustomerQueryCloseSummary(MdlQueryClose values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date," +
                    " a.email_subject, a.status, a.aging, concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as closed_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as closed_date " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " WHERE a.status ='Close'  group by a.email_gid ORDER BY g.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryCloseList = dt_datatable.AsEnumerable().Select(row => new QueryCloseList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    closed_by = row["closed_by"].ToString(),
                    closed_date = row["closed_date"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public void DaCustomerQueryClose360(string email_gid, MdlQueryClose360 values)
        {

            msSQL = " SELECT a.email_gid, a.ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assignedto_date, " +
                    " a.email_from, a.email_date, a.status, a.aging, a.email_subject, a.email_content,a.email_to, a.cc,a.bcc,a.from_mailaddress,b.assigned_remarks, " +
                    " concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as closed_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as closed_date, g.remarks as closed_remarks  " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " where a.email_gid='" + email_gid + "' and g.decision='Close'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.email_gid = objODBCDatareader["email_gid"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_to = objODBCDatareader["assigned_to"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.status = objODBCDatareader["status"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
                values.closed_remarks = objODBCDatareader["closed_remarks"].ToString();
                values.closed_date = objODBCDatareader["closed_date"].ToString();
                values.closed_by = objODBCDatareader["closed_by"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.status, a.aging,a.cc,a.bcc, a.email_to " +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.email_gid = '" + email_gid + "' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryClose360list = dt_datatable.AsEnumerable().Select(row => new QueryClose360list
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    email_to = row["email_to"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.email_gid, a.document_path,a.document_name," +
                     " substring_index(a.document_name,'.',-1) as document_extension, a.mailattachment_gid " +
                     " FROM osd_trn_tmaildetailsattachement a" +
                     " WHERE a.email_gid = '" + email_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaCustomerAssignQueryCloseSummary(string employee_gid, MdlAssignedQueryClose values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date," +
                    " a.email_subject, a.status, a.aging, concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as closed_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as closed_date " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " WHERE a.status ='Close' AND b.employee_gid= '" + employee_gid + "' group by a.email_gid ORDER BY g.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignedCloseList = dt_datatable.AsEnumerable().Select(row => new QueryAssignedCloseList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    closed_by = row["closed_by"].ToString(),
                    closed_date = row["closed_date"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public void DaCustomerAssignQueryClose360(string email_gid, MdlAssignQueryClose360 values)
        {

            msSQL = " SELECT a.email_gid, a.ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assignedto_date, " +
                    " a.email_from, a.email_date, a.status, a.aging, a.email_subject, a.email_content,a.email_to, a.cc,a.bcc,a.from_mailaddress,b.assigned_remarks, " +
                    " concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as closed_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as closed_date, g.remarks as closed_remarks  " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " where a.email_gid='" + email_gid + "' and g.decision='Close'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.email_gid = objODBCDatareader["email_gid"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_to = objODBCDatareader["assigned_to"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.status = objODBCDatareader["status"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
                values.closed_remarks = objODBCDatareader["closed_remarks"].ToString();
                values.closed_date = objODBCDatareader["closed_date"].ToString();
                values.closed_by = objODBCDatareader["closed_by"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date, a.status, a.aging,a.cc,a.bcc, a.email_to " +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.email_gid = '" + email_gid + "' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignClose360list = dt_datatable.AsEnumerable().Select(row => new QueryAssignClose360list
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    email_to = row["email_to"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.email_gid, a.document_path,a.document_name," +
                       " substring_index(a.document_name,'.',-1) as document_extension, a.mailattachment_gid " +
                       " FROM osd_trn_tmaildetailsattachement a" +
                       " WHERE a.email_gid = '" + email_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaCustomerAssignQueryReplySummary(string employee_gid, MdlAssignedQueryReply values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date," +
                    " a.email_subject, a.status, a.aging, concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as reply_by, date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as reply_date,a.from_mailaddress " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = a.updated_by " +
                    " WHERE a.status in ('Reply','Forward') AND b.employee_gid= '" + employee_gid + "' group by a.email_gid ORDER BY g.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignedReplyList = dt_datatable.AsEnumerable().Select(row => new QueryAssignedReplyList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    reply_by = row["reply_by"].ToString(),
                    reply_date = row["reply_date"].ToString(),
                    from_mailaddress = row["from_mailaddress"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public void DaCustomerAssignQueryReply360(string email_gid, MdlAssignQueryReply360 values)
        {

            msSQL = " SELECT a.email_gid, a.ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assignedto_date, " +
                    " a.email_from, a.email_date, a.status, a.aging, a.email_subject, a.email_content,a.email_to, a.cc,a.bcc,a.from_mailaddress,b.assigned_remarks, " +
                    " concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as reply_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as reply_date, g.remarks as reply_remarks  " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " where a.email_gid='" + email_gid + "' group by email_gid";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.email_gid = objODBCDatareader["email_gid"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_to = objODBCDatareader["assigned_to"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.status = objODBCDatareader["status"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
                values.reply_remarks = objODBCDatareader["reply_remarks"].ToString();
                values.reply_date = objODBCDatareader["reply_date"].ToString();
                values.reply_by = objODBCDatareader["reply_by"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.status, a.aging,a.cc,a.bcc, a.email_to " +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.email_gid = '" + email_gid + "' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignReply360list = dt_datatable.AsEnumerable().Select(row => new QueryAssignReply360list
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    email_to = row["email_to"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.email_gid, a.document_path,a.document_name," +
                       " substring_index(a.document_name,'.',-1) as document_extension, a.mailattachment_gid " +
                       " FROM osd_trn_tmaildetailsattachement a" +
                       " WHERE a.email_gid = '" + email_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaCustomerAssignQueryForwardSummary(string employee_gid, MdlAssignedQueryForward values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date," +
                    " a.email_subject, a.status, a.aging, concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as forward_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as forward_date, a.from_mailaddress " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " WHERE a.status ='Forward' AND b.employee_gid= '" + employee_gid + "' group by a.email_gid ORDER BY g.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignedForwardList = dt_datatable.AsEnumerable().Select(row => new QueryAssignedForwardList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    forward_by = row["forward_by"].ToString(),
                    forward_date = row["forward_date"].ToString(),
                    from_mailaddress = row["from_mailaddress"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public void DaCustomerAssignQueryForward360(string email_gid, MdlAssignQueryForward360 values)
        {

            msSQL = " SELECT a.email_gid, a.ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assignedto_date, " +
                    " a.email_from, a.email_date, a.status, a.aging, a.email_subject, a.email_content,a.email_to, a.cc,a.bcc,a.from_mailaddress,b.assigned_remarks, " +
                    " concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as forward_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as forward_date " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_tticketdecision g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " where a.email_gid='" + email_gid + "' group by email_gid";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.email_gid = objODBCDatareader["email_gid"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_to = objODBCDatareader["assigned_to"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.status = objODBCDatareader["status"].ToString();
                values.aging = objODBCDatareader["aging"].ToString();
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
                values.forward_by = objODBCDatareader["forward_by"].ToString();
                values.forward_date = objODBCDatareader["forward_date"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, a.status, a.aging,a.cc,a.bcc, a.email_to " +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.email_gid = '" + email_gid + "' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignForward360list = dt_datatable.AsEnumerable().Select(row => new QueryAssignForward360list
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    email_to = row["email_to"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();

        }

        public void DaCustomerAssignQueryTransferSummary(string employee_gid, MdlAssignedQueryTransfer values)
        {
            msSQL = " SELECT a.email_gid,a.ticketref_no,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by," +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to,date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date," +
                    " a.email_subject, a.status, a.aging, g.transferby_name as transfer_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as transfer_date,g.transfer_remarks " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_ttransferlog g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " WHERE g.assignedto_gid= '" + employee_gid + "' ORDER BY g.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignedTransferList = dt_datatable.AsEnumerable().Select(row => new QueryAssignedTransferList
                {
                    email_gid = row["email_gid"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    assigned_by = row["assigned_by"].ToString(),
                    assigned_to = row["assigned_to"].ToString(),
                    assigned_date = row["assigned_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    transfer_by = row["transfer_by"].ToString(),
                    transfer_date = row["transfer_date"].ToString(),
                    transfer_remarks = row["transfer_remarks"].ToString()
                }).ToList();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }
        }

        public void DaCustomerAssignQueryTransfer360(string email_gid, MdlAssignQueryTransfer360 values)
        {

            msSQL = " SELECT a.email_gid, a.ticketref_no, concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assigned_by, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assigned_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, '/', e.user_code) as assigned_to, date_format(b.allotted_on, '%d-%m-%Y %h:%i %p') as assignedto_date, " +
                    " a.email_from, a.email_date, a.status, a.aging, a.email_subject, a.email_content,a.email_to, a.cc,a.bcc,a.from_mailaddress,b.assigned_remarks, " +
                    " concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) as transfer_by, date_format(g.created_date, '%d-%m-%Y %h:%i %p') as transfer_date, g.transfer_remarks as transfer_remarks  " +
                    " FROM osd_trn_tmaildetails a " +
                    " left join osd_trn_tticketassign b on a.email_gid = b.email_gid " +
                    " left join adm_mst_tuser c on b.allotted_by = c.user_gid " +
                    " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = b.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                    " left join osd_trn_ttransferlog g on g.email_gid = a.email_gid " +
                    " left join adm_mst_tuser h on h.user_gid = g.created_by " +
                    " where g.email_gid='" + email_gid + "' group by email_gid";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.email_gid = objODBCDatareader["email_gid"].ToString();
                values.ticketref_no = objODBCDatareader["ticketref_no"].ToString();
                values.assigned_by = objODBCDatareader["assigned_by"].ToString();
                values.assigned_date = objODBCDatareader["assigned_date"].ToString();
                values.assigned_to = objODBCDatareader["assigned_to"].ToString();
                values.email_from = objODBCDatareader["email_from"].ToString();
                values.email_date = objODBCDatareader["email_date"].ToString();
                values.status = objODBCDatareader["status"].ToString();
                values.aging = objODBCDatareader["aging"].ToString(); 
                values.email_subject = objODBCDatareader["email_subject"].ToString();
                values.email_content = objODBCDatareader["email_content"].ToString();
                values.email_to = objODBCDatareader["email_to"].ToString();
                values.cc = objODBCDatareader["cc"].ToString();
                values.bcc = objODBCDatareader["bcc"].ToString();
                values.from_mailaddress = objODBCDatareader["from_mailaddress"].ToString();
                values.assigned_remarks = objODBCDatareader["assigned_remarks"].ToString();
                values.transfer_remarks = objODBCDatareader["transfer_remarks"].ToString();
                values.transfer_date = objODBCDatareader["transfer_date"].ToString();
                values.transfer_by = objODBCDatareader["transfer_by"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " SELECT a.email_gid,a.ticketref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                    " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date, a.status, a.aging,a.cc,a.bcc, a.email_to " +
                    " FROM osd_trn_tmaildetails a" +
                    " WHERE a.email_gid = '" + email_gid + "' ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.QueryAssignTransferlist = dt_datatable.AsEnumerable().Select(row => new QueryAssignTransferlist
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    ticketref_no = row["ticketref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    status = row["status"].ToString(),
                    aging = row["aging"].ToString(),
                    cc = row["cc"].ToString(),
                    bcc = row["bcc"].ToString(),
                    email_to = row["email_to"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.email_gid, a.document_path,a.document_name," +
                     " substring_index(a.document_name,'.',-1) as document_extension, a.mailattachment_gid " +
                     " FROM osd_trn_tmaildetailsattachement a" +
                     " WHERE a.email_gid = '" + email_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlAttachmentList = dt_datatable.AsEnumerable().Select(row => new MdlAttachmentList
                {
                    mailattachment_gid = row["mailattachment_gid"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    document_extension = row["document_extension"].ToString()
                }).ToList();

            }
            dt_datatable.Dispose();
        }

        public void DaComposeMail360(string email_gid, MdlComposeMail360List values)
        {

            msSQL = "  SELECT '' as status,email_gid,email_content as mailcontent,email_subject,email_from as frommail_id,email_to as tomail_id,email_cc as ccmail_id,email_bcc as bccmail_id," +
                    " date_format(email_date, '%d-%m-%Y %h:%i %p') as email_date" +
                    " FROM osd_trn_treferencemail" +
                    "  WHERE email_gid='" + email_gid + "' GROUP BY email_gid " +
                    " UNION " +
                    " SELECT decision as status,email_gid ,mailcontent, email_subject, " +
                    " frommail_id, tomail_id, ccmail_id, bccmail_id," +
                    " date_format(created_date, '%d-%m-%Y %h:%i %p') as email_date " +
                    " FROM osd_trn_tsentmail " +
                    " WHERE email_gid='" + email_gid + "' and decision<>'Transfer' ORDER BY email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                var getDocList = new List<MdlComposeMaillist>();
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " SELECT a.email_gid, a.document_path,a.document_name," +
                        " substring_index(a.document_name,'.',-1) as document_extension, b.decision, a.mailattachment_gid " +
                        " FROM osd_trn_tmaildetailsattachement a" +
                        " left join osd_trn_tticketdecision b on b.email_gid = a.email_gid " +
                        " WHERE a.email_gid = '" + dt["email_gid"].ToString() + "' and decision='" + dt["status"].ToString() + "' ";
                    dt_child = objdbconn.GetDataTable(msSQL);
                    var attachmentlist = new List<MdlAttachmentList>();
                    if (dt_child.Rows.Count != 0)
                    {
                        attachmentlist = dt_child.AsEnumerable().Select(row => new MdlAttachmentList
                        {
                            mailattachment_gid = row["mailattachment_gid"].ToString(),
                            document_name = row["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                            document_extension = row["document_extension"].ToString()
                        }).ToList();

                    }
                    dt_child.Dispose();

                    getDocList.Add(new MdlComposeMaillist
                    {
                        email_gid = dt["email_gid"].ToString(),
                        email_from = dt["frommail_id"].ToString(),
                        email_date = dt["email_date"].ToString(),
                        emailsubject = dt["email_subject"].ToString(),
                        email_content = dt["mailcontent"].ToString(),
                        email_to = dt["tomail_id"].ToString(),
                        email_cc = dt["ccmail_id"].ToString(),
                        email_bcc = dt["bccmail_id"].ToString(),
                        MdlAttachmentList = attachmentlist,

                    });
                    values.MdlComposeMaillist = getDocList;
                }

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                dt_datatable.Dispose();
                values.status = false;
                values.message = "No Record Found";
            }

        }

        public void DaAssignedQueryCount(string user_gid, string employee_gid, AssignedQueryCount values)
        {
            msSQL = "select count(*) as assigned_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where b.employee_gid = '" + employee_gid + "' and a.status in ('Assigned','Transfer')";
            values.assigned_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as reply_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where b.employee_gid = '" + employee_gid + "' and a.status in ('Reply', 'Forward')";
            values.reply_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as forward_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where b.employee_gid = '" + employee_gid + "' and a.status = 'Forward'";
            values.forward_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as transfer_count from osd_trn_ttransferlog where assignedto_gid = '" + employee_gid + "' ";
            values.transfer_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as close_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where b.employee_gid = '" + employee_gid + "' and a.status = 'Close'";
            values.close_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaQueryAssignmentCount(string user_gid, string employee_gid, QueryAssignmentCount values)
        {
            msSQL = "select count(a.email_gid) as pending_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where a.status = 'Pending'";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(a.email_gid) as assign_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where a.status <> 'Close' and a.status <> 'Pending'";
            values.assign_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(a.email_gid) as close_count from osd_trn_tmaildetails a left join osd_trn_tticketassign b on a.email_gid = b.email_gid where a.status = 'Close'";
            values.close_count = objdbconn.GetExecuteScalar(msSQL);

        }

        public void DaTransferEmployee(string employee_gid, MdlTransferEmployeeList values)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid" +
                        " from adm_mst_tuser a " +
                        " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                        " where user_status<>'N' and b.employee_gid <> '" + employee_gid + "' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<TransferEmployeeList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.TransferEmployeeList = dt_datatable.AsEnumerable().Select(row =>
                      new TransferEmployeeList
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }


        }

        public void PostEmailSeen(string email_gid)
        {
            msSQL = " UPDATE osd_trn_tmaildetails SET" +
                   " seen_flag='Y'" +
                   " WHERE email_gid='" + email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        public void DaReferenceMail(string email_gid, MdlQueryview values)
        {

            msSQL = " SELECT email_gid,email_content, email_subject,email_from as frommail_id,email_to as tomail_id,email_cc as ccmail_id," +
                    " email_bcc as bccmail_id, date_format(email_date, '%d-%m-%Y %h:%i %p') as email_date" +
                    " FROM osd_trn_treferencemail" +
                    " WHERE email_gid='" + email_gid + "' GROUP BY email_gid";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.frommail_id = objODBCDatareader["frommail_id"].ToString();
                values.tomail_id = objODBCDatareader["tomail_id"].ToString();
                values.ccmail_id = objODBCDatareader["ccmail_id"].ToString();
                values.bccmail_id = objODBCDatareader["bccmail_id"].ToString();
                values.mail_date = objODBCDatareader["email_date"].ToString();
                values.mail_subject = objODBCDatareader["email_subject"].ToString();
                values.mailcontent = objODBCDatareader["email_content"].ToString();
            }
            objODBCDatareader.Close();

        }
    }
}

