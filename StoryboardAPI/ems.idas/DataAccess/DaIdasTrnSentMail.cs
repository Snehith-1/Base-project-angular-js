using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace ems.idas.DataAccess
{
    public class DaIdasTrnSentMail
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader;
        int mnResult;
        string msGetGID;
        string lsuser_name = string.Empty;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        string frommail_id, ls_server, ls_username, ls_password, tomail_id, bcc_mailid, cc_mailid;
        int ls_port;
        string[] bcc;
        string[] cc;
        string body;
        result objResult = new result();

        // sent Mail
        public bool DaPostSendMail(sendmail objsendmail, string user_gid)
        {

            try
            {
                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
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


                if (objsendmail.to_mail == "" && objsendmail.to_mail == null)
                {
                    objsendmail.message = "To mail is null..";
                    objsendmail.status = false;
                    return false;
                }
                tomail_id = objsendmail.to_mail;
                bcc_mailid = "";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail_id));
                if (objsendmail.cc_mail != "" && objsendmail.cc_mail != null)
                {
                    cc = (objsendmail.cc_mail).Split(',');
                    if (cc.Length > 0)
                    {
                        foreach (string ccEmail in cc)
                        {
                            cc_mailid = cc_mailid + "," + ccEmail;
                            message.CC.Add(new MailAddress(ccEmail));
                        }
                    }
                }
                if (objsendmail.bcc_mail != "" && objsendmail.bcc_mail != null)
                {
                    bcc = (objsendmail.bcc_mail).Split(',');
                    if (bcc.Length > 0)
                    {
                        foreach (string bccEmail in bcc)
                        {
                            bcc_mailid = bcc_mailid + "," + bccEmail;
                            message.Bcc.Add(new MailAddress(bccEmail));
                        }
                    }
                }



                string file = objsendmail.document_path;

                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                message.Subject = objsendmail.subject;
                message.IsBodyHtml = true;
                message.Body = objsendmail.body_content;
                data.Name = "IDAS Document Check List.xls";
                message.Attachments.Add(data);
                smtp.Port = ls_port;
                smtp.Host = ls_server;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                msGetGID = objcmnfunctions.GetMasterGID("MAIL");
                if (msGetGID == "E")
                {
                    objsendmail.message = "Error in Sequence Code";
                    objsendmail.status = false;
                    return false;
                }
                objsendmail.document_name = "IDAS Document Check List.xls";
                string lspath;

                lspath = HttpContext.Current.Server.MapPath("../../../erpdocument/SAMUNNATI/IDAS/SentMail/" + msGetGID + "/");
                if ((!System.IO.Directory.Exists(lspath)))
                {

                    System.IO.Directory.CreateDirectory(lspath);
                }
                string source_path, destination_path;
                source_path = objsendmail.document_path.Replace("EMS//", "EMS/");
                destination_path = lspath + objsendmail.document_name;
                File.Copy(source_path, destination_path);

                destination_path = "../../../erpdocument/SAMUNNATI/IDAS/SentMail/" + msGetGID + "/" + objsendmail.document_name;

                msSQL = " insert into ids_trn_tsentmail( " +
                        " sentmail_gid, " +
                        " sanction_gid, " +
                        " document_path," +
                        " document_name," +
                        " to_mail," +
                        " cc_mail," +
                        " bcc_mail," +
                        " from_mail," +
                        " subject," +
                        " content," +
                        " created_by," +
                        " created_date) " +
                        " values( " +
                        "'" + msGetGID + "'," +
                        "'" + objsendmail.sanction_gid + "'," +
                        "'" + destination_path + "'," +
                        "'" + objsendmail.document_name + "'," +
                        "'" + tomail_id + "'," +
                        "'" + objsendmail.cc_mail + "'," +
                        "'" + objsendmail.bcc_mail + "'," +
                        "'" + frommail_id + "'," +
                        "'" + objsendmail.subject + "'," +
                        "'" + objsendmail.body_content + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    objsendmail.message = "Mail Sent Successfully";
                    objsendmail.status = true;
                    return true;
                }
                else
                {
                    objsendmail.message = "Error while Sending the Mail";
                    objsendmail.status = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                objsendmail.message = "Error while Sending the Mail";
                objsendmail.status = false;
                return false;
            }
        }

        // Sent Mail Summary
        public void DaGetSentMailSummary(string sanction_gid, sendmail_list values)
        {

            msSQL = " SELECT a.sentmail_gid, a.sanction_gid, a.document_path, a.document_name," +
                    " a.to_mail, a.cc_mail, a.bcc_mail, a.from_mail, a.content," +
                    " a.subject, concat(b.user_firstname,' ',b.user_lastname) as user_name," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date" +
                    " FROM ids_trn_tsentmail a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " WHERE sanction_gid = '" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocList = new List<sendmail>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getDocList.Add(new sendmail
                    {
                        sentmail_gid = dt["sentmail_gid"].ToString(),
                        sanction_gid = dt["sanction_gid"].ToString(),
                        document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        document_name = dt["document_name"].ToString(),
                        to_mail = dt["to_mail"].ToString(),
                        cc_mail = dt["cc_mail"].ToString(),
                        bcc_mail = dt["bcc_mail"].ToString(),
                        from_mail = dt["from_mail"].ToString(),
                        body_content = dt["content"].ToString(),
                        subject = dt["subject"].ToString(),
                        created_by = dt["user_name"].ToString(),
                        created_date = dt["created_date"].ToString(),

                    });
                }
                values.sendmail = getDocList;
            }
            dt_datatable.Dispose();
        }


        public result DaPostRetreivalReqMail(string retrievalrequest_gid, string user_gid)
        {
            result objResult = new result();

            try
            {
                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
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

                msSQL = "select group_concat(employee_mailid) as to_mail from ocs_trn_tmailcclist where mailtrigger_function='IDAS - Retrieval Req Alert'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);



                msSQL = " SELECT " +
                       " a.retrievalrequest_gid,DATE_FORMAT(a.requested_date,'%d-%m-%Y') as requested_date," +
                       " a.requestedby_name,DATE_FORMAT(a.approved_date,'%d-%m-%Y') as approved_date,a.approvalby_name," +
                       " a.documentretrieved_status,a.retrieval_type,a.requested_for,a.req_remarks," +
                       " CONCAT(b.user_code,' / ',b.user_firstname,b.user_lastname) as created_by " +
                       " FROM ids_trn_tretrievalrequest a" +
                       " LEFT JOIN adm_mst_tuser b on a.created_by=b.user_gid" +
                       " WHERE retrievalrequest_gid='" + retrievalrequest_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();
                    body = "Dear Sir, <br><br>";
                    body = body + "" + objODBCDataReader["created_by"].ToString() + " has create the<b> IDAS - Document Retrieval Request</b> with following details.<br><br>";
                    body = body + " <table width='100%' border='0' cellpadding='1' cellspacing='1'> <tr><td width='45%' style='color:#2980B9;'><b>Request Date </b></td><td width='10%'></td><td width='45%' style='color:#2980B9;'><b>Request By</b></td></tr>";
                    body = body + "<tr><td>" + objODBCDataReader["requested_date"].ToString() + " </td><td></td><td>" + objODBCDataReader["requestedby_name"].ToString() + " </td></tr>";
                    body = body + "<tr style='color:#2980B9;'><td><b>Approved Date</b></td><td></td><td><b>Approved By</b></td></tr>";
                    body = body + "<tr><td>" + objODBCDataReader["approved_date"].ToString() + " </td><td></td><td>" + objODBCDataReader["approvalby_name"].ToString() + " </td></tr>";
                    body = body + "<tr style='color:#2980B9;'><td><b>Retrieval Type  </b></td><td></td><td><b>Requested For  </b></td></tr>";
                    body = body + "<tr><td>" + objODBCDataReader["retrieval_type"].ToString() + " </td><td></td><td>" + objODBCDataReader["requested_for"].ToString() + " </td></tr>";
                    body = body + "<tr style='color:#2980B9;'><td colspan='3'><b>Remarks</b></td></tr>";
                    body = body + "<tr><td colspan='3'>" + objODBCDataReader["req_remarks"].ToString() + "</td></tr>";
                    body = body + "</table> ";
                    body = body + "<br>";

                }
                objODBCDataReader.Close();

                body = body + "<b>Retrieval Request - File Details </b>";

                body = body + "<table padding='8px' style='border:1px solid #060606;' cellpadding='2' cellspacing='2'>";
                body = body + "<tr style='background-color: #2980B9;'><td width='5%'><b> S.No</b></td><td width='35%'><b>Customer</b></td><td width='30%'><b>Despath</b></td><td width='30%'><b>Box</b></td><td width='30%'><b>File</b></td></tr>";

                var count = 0;
                msSQL = " SELECT " +
                        " CONCAT(b.customer_urn,' / ',b.customername) as customer_name," +
                        " CONCAT(d.boxref_no, ' || ', IF(d.stampref_no IS NULL, '', d.stampref_no)) as boxref_no," +
                        " CONCAT(e.despatchref_no,' || ',IF(e.stampref_no IS NULL,'',e.stampref_no)) as despatchref_no," +
                        " CONCAT(c.batchref_no,' || ' ,IF(c.stampref_no IS NULL,'',c.stampref_no)) AS batchref_no" +
                        " FROM ids_trn_tretrievalrequestdtls a" +
                        " INNER JOIN ocs_mst_tcustomer b on a.customer_gid=b.customer_gid" +
                        " INNER JOIN ids_trn_tbatch c on a.batch_gid=c.batch_gid" +
                        " INNER JOIN ids_trn_tcartonbox d on a.box_gid=d.cartonbox_gid" +
                        " INNER JOIN ids_trn_tdespatch e on a.despatch_gid=e.despatch_gid" +
                        " WHERE a.retrievalrequest_gid='" + retrievalrequest_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow row in dt_datatable.Rows)
                    {
                        count = count + 1;
                        body = body + "<tr><td>" + (count) + "</td><td>" + row["customer_name"].ToString() + "</td><td>" + row["despatchref_no"].ToString() + "</td>";
                        body = body + "<td>" + row["boxref_no"].ToString() + "</td><td>" + row["batchref_no"].ToString() + "</td></tr>";

                    }
                    body = body + "</table><br><br><br>";

                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox " + "<br/>";
                    body = body + "<br/>";
                }
                dt_datatable.Dispose();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail_id));

                message.Subject = "REG : IDAS - Retrieval Request";
                message.IsBodyHtml = true;
              
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                msGetGID = objcmnfunctions.GetMasterGID("MAIL");
                if (msGetGID == "E")
                {
                    objResult.message = "Error in Sequence Code";
                    objResult.status = false;
                    return objResult;
                }
                msSQL = " insert into ids_trn_tsentmail( " +
                   " sentmail_gid, " +
                   " sanction_gid, " +
                   " document_path," +
                   " document_name," +
                   " to_mail," +
                   " cc_mail," +
                   " bcc_mail," +
                   " from_mail," +
                   " subject," +
                   " content," +
                   " created_by," +
                   " created_date) " +
                   " values( " +
                   "'" + msGetGID + "'," +
                   "'" + retrievalrequest_gid + "'," +
                   "'No Attachment Path'," +
                   "'No Attachment'," +
                   "'" + tomail_id + "'," +
                   "'No CC Mail'," +
                   "'No BCC Mail'," +
                   "'" + frommail_id + "'," +
                   "'REG : IDAS - Retrieval Request'," +
                   "'" + body.Replace("'","") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    objResult.message = "Mail Sent";
                    objResult.status = true;
                    return objResult;
                }
                else
                {
                    objResult.message = "Mail Not Sent";
                    objResult.status = false;
                    return objResult;
                }


            }
            catch
            {
                objResult.message = "Error Occured While Sending Mail";
                objResult.status = false;
                return objResult;
            }



        }

        public result DaPostCourierMail(string courierMgmt_gid,string user_gid)
        {

            try
            {
                string lscourier_date = string.Empty;
                string lssanctionref_no = string.Empty;
                string lscustomer_name = string.Empty;
                string lsdocument_type = string.Empty;
                string lssender_name = string.Empty;
              //  string lssender_gid = string.Empty;
                string lspod_no = string.Empty;
                string lscouriercompany_name = string.Empty;
                string lscourierhandover_to = string.Empty;
               // string lscourierhandover_to_gid = string.Empty;
                string lscourier_type = string.Empty;
                string lsremarks = string.Empty;
                string lscourierref_no = string.Empty;
              
               



                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
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

                msSQL = " SELECT courierref_no, date_format(date_of_courier,'%d-%m-%Y') as date_of_courier,sanctionref_no," +
                        " customer_name, document_type, sender_name, sender_gid, pod_no, couriercompany_name, " +
                        " courierhandover_to, courierhandover_to_gid, courier_type,remarks" +
                        " FROM ids_trn_tcouriermgnt" +
                        " WHERE couriermgmt_gid='" + courierMgmt_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader .HasRows ==true)
                {
                    objODBCDataReader.Read();
                    lscourier_date = objODBCDataReader["date_of_courier"].ToString();
                    lssanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                    lscustomer_name = objODBCDataReader["customer_name"].ToString();
                    lsdocument_type = objODBCDataReader["document_type"].ToString();
                    lscourier_type = objODBCDataReader["courier_type"].ToString();
                   // lspod_no = objODBCDataReader["pod_no"].ToString();
                   // lscouriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                    lssender_name = objODBCDataReader["sender_name"].ToString();
                   // lssender_gid = objODBCDataReader["sender_gid"].ToString();
                    lscourierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                  //  lscourierhandover_to_gid = objODBCDataReader["courierhandover_to_gid"].ToString();
                    lsremarks = objODBCDataReader["remarks"].ToString();
                    lscourierref_no = objODBCDataReader["courierref_no"].ToString();


                }
                objODBCDataReader.Close();

                body = body + "Dear Sir/Madam,<br><br>";

                body = body + ""+lssender_name+ " <b>handover the document</b> with following details.<br><br>";

                body = body + "<b>Courier Ref No.:</b> "+ lscourierref_no + "<br><br>";

                body = body + "<b>Courier Date :</b> "+lscourier_date +"<br><br>";


                body = body + "<b>Customer Name        :</b> "+lscustomer_name +"<br><br>";

                body = body + "<b>Sanction Ref No.     :</b> "+lssanctionref_no +"<br><br>";

                body = body + "<b>Document Type        :</b> "+lsdocument_type +"<br><br>";

                body = body + "<b>Sender of the Document :</b> " + lssender_name  +"<br><br>";

                body = body + "<b>Receiver of the Document :</b> " + lscourierhandover_to  +"<br><br>";

                body = body + "<b>Remarks  :</b> "+lsremarks +"<br><br>";

               

                msSQL = " SELECT b.employee_emailid,a.employee_gid" +
                        " FROM ids_trn_tcourierhandoverto a"+
                        " LEFT JOIN hrm_mst_temployee b on a.employee_gid=b.employee_gid"+
                        " WHERE couriermgmt_gid='" + courierMgmt_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable .Rows.Count !=0)
                {
                    foreach (DataRow dr in dt_datatable.Rows )
                    {
                        string body_message = string.Empty;
                        body_message = body_message+ "Kindly <a href=" + ConfigurationManager.AppSettings["CourierMgmt_URL"].ToString() + "?id=" + courierMgmt_gid + "&emp_gid="+dr["employee_gid"] +"> Click Here</a> and Submit Your Acknowledgement.<br />";
                        body_message = body_message+"<br />";
                        body_message = body_message+ " **This is an automated e-mail. Please do not reply to this mailbox " + "<br/>";
                        body_message = body_message+ "<br/>";
                        tomail_id = dr["employee_emailid"].ToString();

                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        message.To.Add(new MailAddress(tomail_id));
                        message.Subject = "REG : IDAS - Courier/Document Details Acknowledgement";
                        message.IsBodyHtml = true;
                        message.Body = body + body_message;
                        smtp.Port = ls_port;
                        smtp.Host = ls_server;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                       
                    }
                }

               

                  msGetGID = objcmnfunctions.GetMasterGID("MAIL");
                if (msGetGID == "E")
                {
                    objResult.message = "Error in Sequence Code";
                    objResult.status = false;
                    return objResult;
                }
                msSQL = " insert into ids_trn_tsentmail( " +
                   " sentmail_gid, " +
                   " sanction_gid, " +
                   " document_path," +
                   " document_name," +
                   " to_mail," +
                   " cc_mail," +
                   " bcc_mail," +
                   " from_mail," +
                   " subject," +
                   " content," +
                   " created_by," +
                   " created_date) " +
                   " values( " +
                   "'" + msGetGID + "'," +
                   "'" + courierMgmt_gid  + "'," +
                   "'No Attachment Path'," +
                   "'No Attachment'," +
                   "'" + lscourierhandover_to + "'," +
                   "'No CC Mail'," +
                   "'No BCC Mail'," +
                   "'" + frommail_id + "'," +
                   "'REG : IDAS - Courier/Document Details Acknowledgement'," +
                   "'" + body.Replace("'", "") + "'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    objResult.message = "Mail Sent";
                    objResult.status = true;
                    return objResult;
                }
                else
                {
                    objResult.message = "Mail Not Sent";
                    objResult.status = false;
                    return objResult;
                }



            }
            catch (Exception ex)
            {
                objResult.message = "Error Occured While Sending Mail";
                objResult.status = false;
                return objResult;
            }
        }
    }
}