using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using ems.storage.Functions;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Web.Http.Results;
using System.IO;
using System.Linq;
using System.Configuration;

namespace ems.master.DataAccess
{
    public class DaMstCourierManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msSQL1, msGetGid, msGetGidRef, msGetChildGid, msGetGidDoc, msGetGidMail;
        int mnResult;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDataReader;
        string frommail_id, ls_server, ls_username, ls_password, tomail_id, lspath, bcc_mailid, cc_mailid;
        int ls_port;
        string[] bcc;
        string[] cc;
        string body;
        string lscompany_name = string.Empty;
        string lscourierref_no, lsdate_of_courier, lssanction_gid, lssanctionref_no, lscustomer_gid, lscustomer_name, lsdocument_type, lssender_gid, lssender_name;
        string lspod_no, lscouriercompany_name, lscourierhandover_to_gid, lscourierhandover_to, lsaddress, lscourier_type, lsremarks, lscreated_by, lscreated_date, lscouriercompany_gid;
       
        public void DaGetCadCustomerName(string employee_gid, MdlCadCustomerName values)
        {          
            msSQL = " select application_gid, customer_name from ocs_trn_tcadapplication a" +
                    " order by created_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcadcustomer_list = new List<cadcustomer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcadcustomer_list.Add(new cadcustomer_list
                    {
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        customer_name = (dr_datarow["customer_name"].ToString()),
                    });
                }
                values.cadcustomer_list = getcadcustomer_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetcustomer2sanction(string application_gid, MdlCadCustomerName values)
        {
            msSQL = " SELECT application2sanction_gid,sanction_refno FROM ocs_trn_tapplication2sanction a" +
                      " where application_gid='" + application_gid + "' order by a.application2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctionrefno_list = new List<sanctionrefno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsanctionrefno_list.Add(new sanctionrefno_list
                    {
                        application2sanction_gid = (dr_datarow["application2sanction_gid"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                    });
                }
                values.sanctionrefno_list = getsanctionrefno_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public bool DaSubmitCourierDtl(string employee_gid, MdlCourierDtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRMG");

            if (values.courier_type == "Courier Inward")
            {
                msGetGidRef = objcmnfunctions.GetMasterGID("CI");
            }
            else if (values.courier_type == "Courier Outward")
            {
                msGetGidRef = objcmnfunctions.GetMasterGID("CO");
            }
            else if (values.courier_type == "Physical Inward")
            {
                msGetGidRef = objcmnfunctions.GetMasterGID("PI");
            }
            else if (values.courier_type == "Physical Outward")
            {
                msGetGidRef = objcmnfunctions.GetMasterGID("PO");
            }

            if (values.MdlCourierByList.Count != 0)
            {
                for (int i = 0; i < values.MdlCourierByList.Count; i++)
                {

                    msGetChildGid = objcmnfunctions.GetMasterGID("CRHB");
                    msSQL = " insert into ocs_trn_tcourierhandoverby(" +
                              " courierhandoverby_gid," +
                              " couriermgmt_gid ," +
                              " employee_gid ," +
                              " employee_name, " +
                              " created_date," +
                              " created_by" +
                              " )" +
                              " VALUES(" +
                              "'" + msGetChildGid + "'," +
                              "'" + msGetGid + "'," +
                              "'" + values.MdlCourierByList[i].employee_gid + "'," +
                              "'" + values.MdlCourierByList[i].employee_name + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (values.MdlCourierToList.Count != 0)
            {
                for (int i = 0; i < values.MdlCourierToList.Count; i++)
                {
                    msGetChildGid = objcmnfunctions.GetMasterGID("CRHT");
                    msSQL = " insert into ocs_trn_tcourierhandoverto(" +
                              " courierhandoverto_gid," +
                              " couriermgmt_gid ," +
                              " employee_gid ," +
                              " employee_name," +
                              " created_date," +
                              " created_by" +
                              " )" +
                              " VALUES(" +
                              "'" + msGetChildGid + "'," +
                              "'" + msGetGid + "'," +
                              "'" + values.MdlCourierToList[i].employee_gid + "'," +
                              "'" + values.MdlCourierToList[i].employee_name + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = "INSERT INTO ocs_trn_tcouriermgnt(" +
                  " couriermgmt_gid," +
                  " courierref_no," +
                  " date_of_courier," +
                  " sanction_gid," +
                  " sanctionref_no," +
                  " customer_gid," +
                  " customer_name," +
                  " document_type," +
                  " sender_gid," +
                  " sender_name," +
                  " pod_no," +
                  " couriercompany_gid," +
                  " couriercompany_name," +
                  " courierhandover_to_gid," +
                  " courierhandover_to," +
                  " address," +
                  " courier_type," +
                  " remarks," +
                  " created_by," +
                  " created_date)" +
                  " VALUES(" +
                  "'" + msGetGid + "'," +
                  "'" + msGetGidRef + "'," +
                  "'" + Convert.ToDateTime(values.date_of_courier).ToString("yyyy-MM-dd") + "'," +
                  "'" + values.sanction_gid + "'," +
                  "'" + values.sanctionref_no + "'," +
                  "'" + values.customer_gid + "'," +
                  "'" + values.customer_name.Replace("'", "") + "'," +
                  "'" + values.document_type.Replace("'", "") + "'," +
                  " (select group_concat(employee_gid) from ocs_trn_tcourierhandoverby where couriermgmt_gid='" + msGetGid + "')," +
                  " (select group_concat(employee_name) from ocs_trn_tcourierhandoverby where couriermgmt_gid='" + msGetGid + "')," +
                  "'" + values.pod_no.Replace("'", "") + "'," +
                  "'" + values.couriercompany_gid + "',";
                    if (values.couriercompany_name == null || values.couriercompany_name == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.couriercompany_name.Replace("'", "") + "',";

                    }
             msSQL += " (select group_concat(employee_gid) from ocs_trn_tcourierhandoverto where couriermgmt_gid='" + msGetGid + "')," +
                     " (select group_concat(employee_name) from ocs_trn_tcourierhandoverto where couriermgmt_gid='" + msGetGid + "'),";
                    if (values.address == null || values.address == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.address.Replace("'", "") + "',";

                    }
                msSQL += "'" + values.courier_type + "',";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.remarks.Replace("'", "") + "',";

                }
                 msSQL += "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

           
            if (mnResult == 1)
            {
                msSQL = " update ocs_trn_tcourierdocument set couriermgmt_gid ='" + msGetGid + "' where couriermgmt_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
                    string lscourier_date = string.Empty;
                    string lssanctionref_no = string.Empty;
                    string lscustomer_name = string.Empty;
                    string lsdocument_type = string.Empty;
                    string lssender_name = string.Empty;                   
                    string lspod_no = string.Empty;
                    string lscouriercompany_name = string.Empty;
                    string lscourierhandover_to = string.Empty;
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

                    msSQL = " select courierref_no, date_format(date_of_courier,'%d-%m-%Y') as date_of_courier,sanctionref_no," +
                            " customer_name, document_type, sender_name, sender_gid, pod_no, couriercompany_name, " +
                            " courierhandover_to, courierhandover_to_gid, courier_type,remarks" +
                            " from ocs_trn_tcouriermgnt" +
                            " where couriermgmt_gid='" + msGetGid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == true)
                    {
                        objODBCDataReader.Read();
                        lscourier_date = objODBCDataReader["date_of_courier"].ToString();
                        lssanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                        lscustomer_name = objODBCDataReader["customer_name"].ToString();
                        lsdocument_type = objODBCDataReader["document_type"].ToString();
                        lscourier_type = objODBCDataReader["courier_type"].ToString();
                        lssender_name = objODBCDataReader["sender_name"].ToString();
                        lscourierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                        lsremarks = objODBCDataReader["remarks"].ToString();
                        lscourierref_no = objODBCDataReader["courierref_no"].ToString();
                    }
                    objODBCDataReader.Close();

                    body = body + "Dear Sir/Madam,<br><br>";

                    body = body + "" + lssender_name + " <b>handover the document</b> with following details.<br><br>";

                    body = body + "<b>Courier Ref No.:</b> " + lscourierref_no + "<br><br>";

                    body = body + "<b>Courier Date :</b> " + lscourier_date + "<br><br>";

                    body = body + "<b>Customer Name        :</b> " + lscustomer_name + "<br><br>";

                    body = body + "<b>Sanction Ref No.     :</b> " + lssanctionref_no + "<br><br>";

                    body = body + "<b>Document Type        :</b> " + lsdocument_type + "<br><br>";

                    body = body + "<b>Sender of the Document :</b> " + lssender_name + "<br><br>";

                    body = body + "<b>Receiver of the Document :</b> " + lscourierhandover_to + "<br><br>";

                    body = body + "<b>Remarks  :</b> " + lsremarks + "<br><br>";

                    msSQL = " SELECT b.employee_emailid,a.employee_gid" +
                            " FROM ocs_trn_tcourierhandoverto a" +
                            " LEFT JOIN hrm_mst_temployee b on a.employee_gid=b.employee_gid" +
                            " WHERE couriermgmt_gid='" + msGetGid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt_datatable.Rows)
                        {
                            string body_message = string.Empty;
                            body_message = body_message + "Kindly <a href=" + ConfigurationManager.AppSettings["MasterCourierMgmt_URL"].ToString() + "?id=" + msGetGid + "&emp_gid=" + dr["employee_gid"] + "> Click Here</a> and Submit Your Acknowledgement.<br />";
                            body_message = body_message + "<br />";
                            body_message = body_message + " **This is an automated e-mail. Please do not reply to this mailbox " + "<br/>";
                            body_message = body_message + "<br/>";
                            tomail_id = dr["employee_emailid"].ToString();

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(tomail_id));
                            message.Subject = "REG : Master - Courier/Document Details Acknowledgement";
                            message.IsBodyHtml = true;
                            message.Body = body + body_message;
                            smtp.Port = ls_port;
                            smtp.Host = ls_server;
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);

                        }
                    }

                    msGetGidMail = objcmnfunctions.GetMasterGID("CMAL");
                    if (msGetGidMail == "E")
                    {
                        values.message = "Error in Sequence Code";
                        values.status = false;
                        return false;
                    }
                    msSQL = " insert into ocs_trn_tsentmail( " +
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
                           "'" + msGetGidMail + "'," +
                           "'" + msGetGid + "'," +
                           "'No Attachment Path'," +
                           "'No Attachment'," +
                           "'" + lscourierhandover_to + "'," +
                           "'No CC Mail'," +
                           "'No BCC Mail'," +
                           "'" + frommail_id + "'," +
                           "'REG : Master - Courier/Document Details Acknowledgement'," +
                           "'" + body.Replace("'", "") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {                       
                        values.message = "Courier Details Added Successfully..!";
                        values.status = true;                       
                        return true;
                    }
                    else
                    {
                        values.message = "Error while Sending the Mail";
                        values.status = false;
                        return false;
                    }                
               
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
            return true;
        }
        public bool DaCourierDocUpload(HttpRequest httpRequest, Mdluploadcourierdoc objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string lsdocument_title = httpRequest.Form["document_title"];


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "Master/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }


            string document_title = httpRequest.Form["document_title"].ToString();

            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    MemoryStream ms = new MemoryStream();
                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    httpPostedFile = httpFileCollection[i];
                    string FileExtension = httpPostedFile.FileName;                   
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;                   
                    ls_readStream = httpPostedFile.InputStream;
                    ls_readStream.CopyTo(ms);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/CourierDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                   
                    msGetGid = objcmnfunctions.GetMasterGID("CRDG");
                    msSQL = " insert into ocs_trn_tcourierdocument( " +
                                               " courierdocument_gid ," +
                                               " couriermgmt_gid," +
                                               " document_name ," +
                                               " document_title," +
                                               " document_gid ," +
                                               " document_path," +
                                               " created_by," +
                                               " created_date" +
                                               " )values(" +
                                               "'" + msGetGid + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + httpPostedFile.FileName + "'," +
                                               "'" + lsdocument_title.Replace("'", "") + "'," +
                                               "'" + msdocument_gid + "'," +
                                               "'" + lspath + msdocument_gid + FileExtension + "'," +
                                               "'" + employee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {                       
                        objfilename.status = true;
                        objfilename.message = "Courier Document Uploaded Successfully";
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "Error Occured While Uploading Courier Document";
                    }
                }
            }
            return true;
        }      
        public void DaGetcourierdoc(Mdlcourierdocumentlist values, string employee_gid)
        {
            msSQL = " select a.courierdocument_gid,a.document_name,a.document_path,a.document_title," +
                    " CONCAT(c.user_code,' / ',c.user_firstname,c.user_lastname) as created_by, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date" +
                    " from ocs_trn_tcourierdocument a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " where a.couriermgmt_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcourierdocument_list = new List<courierdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcourierdocument_list.Add(new courierdocument_list
                    {
                        courierdocument_gid = dt["courierdocument_gid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        document_title = dt["document_title"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()
                    });
                }
                values.courierdocument_list = getcourierdocument_list;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteCourierDoc(string courierdocument_gid, string employee_gid, result objResult)
        {            
            msSQL = " delete from ocs_trn_tcourierdocument where courierdocument_gid='" + courierdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);    
            
            if (mnResult != 0)
            {
                objResult.status = true;
                objResult.message = "Courier Document Deleted Successfully";
            }
            else
            {
                objResult.status = false;
            }
        }
        public void DaGetCourierCompany(MdlCourierCompany values)
        {
            msSQL = " select couriercompany_gid, couriercompany_name " +
                    " from ocs_mst_tcouriercompany where status='Y'" +
                    " order by created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcouriercompany_list = new List<couriercompany_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcouriercompany_list.Add(new couriercompany_list
                    {
                        couriercompany_gid = (dr_datarow["couriercompany_gid"].ToString()),
                        couriercompany_name = (dr_datarow["couriercompany_name"].ToString()),
                    });
                }
                values.couriercompany_list = getcouriercompany_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetEditCourierDtl(MdlEditCourierMgmt values, string courier_gid)
        {
            try
            {
                msSQL = " select couriermgmt_gid,courierref_no,couriercompany_gid, " +
                        " cast( date_of_courier  as char)as date_of_courier, " +
                        " address,sanction_gid,sanctionref_no,customer_gid," +
                        " customer_name,document_type,sender_name,pod_no,couriercompany_name," +
                        " courierhandover_to,courier_type,a.remarks,date_format(ack_date,'%d-%m-%Y %h:%i %p') as ack_date," +
                        " address,ack_status,ackby_name,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                        " CONCAT(c.user_code,' / ',c.user_firstname,c.user_lastname) as created_by from ocs_trn_tcouriermgnt a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where couriermgmt_gid='" + courier_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();

                    values.courierref_no = objODBCDataReader["courierref_no"].ToString();                   
                    values.date_of_courier = Convert.ToDateTime(objODBCDataReader["date_of_courier"]).ToString("MM-dd-yyyy");
                    values.sanction_gid = objODBCDataReader["sanction_gid"].ToString();
                    values.sanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                    values.customer_gid = objODBCDataReader["customer_gid"].ToString();
                    values.customer_name = objODBCDataReader["customer_name"].ToString();
                    values.document_type = objODBCDataReader["document_type"].ToString();
                    values.sender_name = objODBCDataReader["sender_name"].ToString();
                    values.pod_no = objODBCDataReader["pod_no"].ToString();
                    values.couriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                    values.courierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                    values.courier_type = objODBCDataReader["courier_type"].ToString();
                    values.address = objODBCDataReader["address"].ToString();
                    values.ack_status = objODBCDataReader["ack_status"].ToString();
                    values.remarks = objODBCDataReader["remarks"].ToString();
                    values.ack_date = objODBCDataReader["ack_date"].ToString();
                    values.ackby_name = objODBCDataReader["ackby_name"].ToString();
                    values.created_date = objODBCDataReader["created_date"].ToString();
                    values.created_by = objODBCDataReader["created_by"].ToString();
                    values.couriercompany_gid = objODBCDataReader["couriercompany_gid"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select a.courierdocument_gid, a.document_name,a.document_path,a.document_title," +
                        " CONCAT(c.user_code,' / ',c.user_firstname,c.user_lastname) as created_by, " +
                        " date_format(a.created_date,'%d-%m-%Y') as created_date" +
                        " from ocs_trn_tcourierdocument a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " WHERE a.couriermgmt_gid='" + courier_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getDocList = new List<uploadcourierdocument>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getDocList.Add(new uploadcourierdocument
                        {
                            courierdocument_gid = dt["courierdocument_gid"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            document_title = dt["document_title"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString()
                        });
                    }
                    values.uploadcourierdocument = getDocList;
                }
                dt_datatable.Dispose();

                msSQL = " select a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name," +
                        " b.employee_gid from adm_mst_tuser a " +
                        " left join hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                        " where user_status<>'N' ORDER BY a.user_firstname asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlEmployeeList = dt_datatable.AsEnumerable().Select(row => new MdlEmployeeList
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()
                    }
                    ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " SELECT employee_gid,employee_name from ocs_trn_tcourierhandoverby WHERE couriermgmt_gid='" + courier_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlCourierByList = dt_datatable.AsEnumerable().Select(row => new MdlCourierByList
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()

                    }).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " SELECT employee_gid,employee_name from ocs_trn_tcourierhandoverto WHERE couriermgmt_gid='" + courier_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    values.MdlCourierToList = dt_datatable.AsEnumerable().Select(row => new MdlCourierToList
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()

                    }).ToList();
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }
        }
        public void DaPostUpdateCourier(string user_gid, string employee_gid, MdlEditCourierMgmt values)
        {
            msSQL = " select couriermgmt_gid,courierref_no,couriercompany_gid, " +
                        " cast( date_of_courier  as char)as date_of_courier, " +
                        " address,sanction_gid,sanctionref_no,customer_gid," +
                        " customer_name,document_type,sender_gid,sender_name,pod_no,couriercompany_name," +
                        " courierhandover_to_gid,courierhandover_to,courier_type,a.remarks,a.ack_date," +
                        " address,ack_status,ackby_name,a.created_date," +
                        " a.created_by from ocs_trn_tcouriermgnt a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where couriermgmt_gid='" + values.courierMgmt_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Read();
                lscourierref_no = objODBCDataReader["courierref_no"].ToString();
                if (objODBCDataReader["date_of_courier"].ToString() == "")
                {
                }
                else
                {
                    lsdate_of_courier = Convert.ToDateTime(objODBCDataReader["date_of_courier"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                lssanction_gid = objODBCDataReader["sanction_gid"].ToString();
                lssanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                lscustomer_gid = objODBCDataReader["customer_gid"].ToString();
                lscustomer_name = objODBCDataReader["customer_name"].ToString();
                lsdocument_type = objODBCDataReader["document_type"].ToString();
                lssender_name = objODBCDataReader["sender_name"].ToString();
                lspod_no = objODBCDataReader["pod_no"].ToString();
                lscouriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                lscourierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                lscourier_type = objODBCDataReader["courier_type"].ToString();
                lsaddress = objODBCDataReader["address"].ToString();
                lsremarks = objODBCDataReader["remarks"].ToString();
                lscreated_date = Convert.ToDateTime(objODBCDataReader["created_date"]).ToString("yyyy-MM-dd HH:mm:ss");
                lscreated_by = objODBCDataReader["created_by"].ToString();
                lssender_gid = objODBCDataReader["sender_gid"].ToString();
                lscourierhandover_to_gid = objODBCDataReader["courierhandover_to_gid"].ToString();
                lscouriercompany_gid = objODBCDataReader["couriercompany_gid"].ToString();
            }
            objODBCDataReader.Close();

            if (values.MdlCourierByList.Count != 0)
            {
               msSQL = " delete from ocs_trn_tcourierhandoverby where couriermgmt_gid = '" + values.courierMgmt_gid + "'";
               mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        for (var i = 0; i < values.MdlCourierByList.Count; i++)
                        {
                            msGetChildGid = objcmnfunctions.GetMasterGID("CRHB");
                            msSQL = " insert into ocs_trn_tcourierhandoverby(" +
                                  " courierhandoverby_gid," +
                                  " couriermgmt_gid ," +
                                  " employee_gid ," +
                                  " employee_name," +
                                  " created_date," +
                                  " created_by" +
                                  " )" +
                                  " values (" +
                                  "'" + msGetChildGid + "'," +
                                  "'" + values.courierMgmt_gid + "'," +
                                  "'" + values.MdlCourierByList[i].employee_gid + "'," +
                                  "'" + values.MdlCourierByList[i].employee_name + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                  "'" + employee_gid + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }                   
               
            }
            if (values.MdlCourierToList.Count != 0)
            {
                msSQL = " delete from ocs_trn_tcourierhandoverto where couriermgmt_gid = '" + values.courierMgmt_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    for (var i = 0; i < values.MdlCourierToList.Count; i++)
                    {
                        msGetChildGid = objcmnfunctions.GetMasterGID("CRHT");
                        msSQL = " insert into ocs_trn_tcourierhandoverto(" +
                              " courierhandoverto_gid," +
                              " couriermgmt_gid ," +
                              " employee_gid ," +
                              " employee_name," +
                              " created_date," +
                              " created_by" +
                              " )" +
                              " values (" +
                              "'" + msGetChildGid + "'," +
                              "'" + values.courierMgmt_gid + "'," +
                              "'" + values.MdlCourierToList[i].employee_gid + "'," +
                              "'" + values.MdlCourierToList[i].employee_name + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "'" + employee_gid + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }                
            }

            msSQL = " update ocs_trn_tcouriermgnt set" +
                    " sanction_gid='" + values.sanction_gid + "'," +
                    " sanctionref_no='" + values.sanctionref_no + "'," +
                    " customer_gid='" + values.customer_gid + "'," +
                    " customer_name='" + values.customer_name.Replace("'", "") + "'," +
                    " document_type='" + values.document_type.Replace("'", "") + "'," +
                    " sender_name=(select group_concat(employee_name) from ocs_trn_tcourierhandoverby where couriermgmt_gid='" + values.courierMgmt_gid + "')," +
                    " remarks='" + values.remarks.Replace("'", "") + "'," +
                    " pod_no='" + values.pod_no.Replace("'", "") + "'," +
                    " couriercompany_gid='" + values.couriercompany_gid + "'," +
                    " couriercompany_name='" + values.couriercompany_name.Replace("'", "") + "'," +
                    " courierhandover_to=(select group_concat(employee_name) from ocs_trn_tcourierhandoverto where couriermgmt_gid='" + values.courierMgmt_gid + "')," +
                    " address='" + values.address.Replace("'", "") + "'," +
                    " courier_type='" + values.courier_type + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            try
            {
                msSQL += " date_of_courier='" + Convert.ToDateTime(values.date_of_courier).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         " updated_by='" + employee_gid + "'";
            }
            catch (Exception ex)
            {
                msSQL += " updated_by='" + employee_gid + "'";
            }
            msSQL += " where couriermgmt_gid='" + values.courierMgmt_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.ack_status == "Acknowledged")
            {
                msSQL = " update ocs_trn_tcouriermgnt set" +
                        " ack_status='" + values.ack_status + "'," +
                        " ack_date='" + Convert.ToDateTime(values.ack_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " ackby_gid='" + employee_gid + "'," +
                        " ackby_name=(select concat(user_code,' / ',user_firstname,user_lastname) from adm_mst_tuser where user_gid='" + user_gid + "')" +
                        " WHERE couriermgmt_gid='" + values.courierMgmt_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                msSQL = " update ocs_trn_tcourierdocument set couriermgmt_gid ='" + values.courierMgmt_gid + "' where couriermgmt_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("CRGL");
                msSQL = "INSERT INTO ocs_trn_tcouriermgntlog(" +
                      " couriermgmtlog_gid," +
                      " couriermgmt_gid," +
                      " courierref_no," +
                      " date_of_courier," +
                      " sanction_gid," +
                      " sanctionref_no," +
                      " customer_gid," +
                      " customer_name," +
                      " document_type," +
                      " sender_gid," +
                      " sender_name," +
                      " pod_no," +
                      " couriercompany_gid," +
                      " couriercompany_name," +
                      " courierhandover_to_gid," +
                      " courierhandover_to," +
                      " address," +
                      " courier_type," +
                      " remarks," +
                      " created_by," +
                      " created_date," +
                      " updated_by," +
                      " updated_date)" +
                      " VALUES(" +
                      "'" + msGetGid + "'," +
                      "'" + values.courierMgmt_gid + "'," +
                      "'" + lscourierref_no + "'," +
                      "'" + lsdate_of_courier + "'," +
                      "'" + lssanction_gid + "'," +
                      "'" + lssanctionref_no + "'," +
                      "'" + lscustomer_gid + "'," +
                      "'" + lscustomer_name + "'," +
                      "'" + lsdocument_type + "'," +
                      "'" + lssender_gid + "'," +
                      "'" + lssender_name + "'," + 
                      "'" + lspod_no + "'," +
                      "'" + lscouriercompany_gid + "'," +
                      "'" + lscouriercompany_name + "'," +
                      "'" + lscourierhandover_to_gid + "'," +
                      "'" + lscourierhandover_to + "'," +
                      "'" + lsaddress + "'," +
                      "'" + lscourier_type + "'," +
                      "'" + lsremarks + "'," +
                      "'" + lscreated_by + "'," +
                      "'" + lscreated_date + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Courier Details Updated Successfully..!";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }
        public bool DaGetCourierMgmt(string courier_type, MdlCourierManagement values)
        {
            msSQL = " SELECT a.courierref_no,a.couriermgmt_gid,date_format(a.date_of_courier,'%d-%m-%Y') as date_of_courier," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,a.courier_type," +
                    " concat(c.user_code,' ',c.user_firstname,' ',c.user_lastname) as created_by," +
                    " a.sanction_gid,a.sanctionref_no,a.customer_gid," +
                    " a.customer_name,a.document_type,a.sender_name,a.pod_no,a.couriercompany_name,a.courierhandover_to,a.ack_status " +
                    " from ocs_trn_tcouriermgnt a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " where a.courier_type='" + courier_type + "' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCourierMgmt = new List<CourierMgmt>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCourierMgmt.Add(new CourierMgmt
                    {
                        courierMgmt_gid = dt["couriermgmt_gid"].ToString(),
                        courierref_no = dt["courierref_no"].ToString(),
                        date_of_courier = dt["date_of_courier"].ToString(),
                        sanction_gid = dt["sanction_gid"].ToString(),
                        sanctionref_no = dt["sanctionref_no"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        document_type = dt["document_type"].ToString(),
                        sender_name = dt["sender_name"].ToString(),
                        pod_no = dt["pod_no"].ToString(),
                        couriercompany_name = dt["couriercompany_name"].ToString(),
                        courierhandover_to = dt["courierhandover_to"].ToString(),
                        ack_status = dt["ack_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        courier_type = dt["courier_type"].ToString(),
                    });
                }
                values.CourierMgmt = getCourierMgmt;
                values.status = true;
                values.message = "Data Fetched";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }
            dt_datatable.Dispose();

            return true;
        }
        public void DaGetCourierCount(courier_count values)
        {
            msSQL = " select count(*) from ocs_trn_tcouriermgnt where courier_type = 'Courier Inward'";
            values.courier_inward = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from ocs_trn_tcouriermgnt where courier_type = 'Courier Outward'";
            values.courier_outward = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from ocs_trn_tcouriermgnt where courier_type = 'Physical Inward'";
            values.physical_inward = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) from ocs_trn_tcouriermgnt where courier_type = 'Physical Outward'";
            values.physical_outward = objdbconn.GetExecuteScalar(msSQL);
        }
        public void DaGetACKNotification(string employee_gid, string user_gid, MdlCourierManagement values)
        {
            msSQL = " select ack_status from ocs_trn_tcouriermgnt a " +
                   " left join ocs_trn_tcourierhandoverto b on a.couriermgmt_gid=b.couriermgmt_gid" +
                   " where a.ack_status = 'Pending'  and b.employee_gid='" + employee_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.ack_status = objODBCDataReader["ack_status"].ToString();
            }
            objODBCDataReader.Close();
        }
        public void DaCourierAckList(string employee_gid, MdlCourierManagement values)
        {
            msSQL = " select a.courierref_no, date_format(a.date_of_courier,'%d-%m-%Y') as date_of_courier, " +
                    " a.couriermgmt_gid, a.ack_status," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,a.courier_type, " +
                    " concat(c.user_code,' ',c.user_firstname,' ',c.user_lastname) as created_by," +
                    " date_format(ack_date,'%d-%m-%Y') as ack_date, ackby_name from ocs_trn_tcouriermgnt a" +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " inner join ocs_trn_tcourierhandoverto d on a.couriermgmt_gid=d.couriermgmt_gid" +
                    " where d.employee_gid='" + employee_gid + "' and ack_status='Pending' order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCourierMgmtPendig = new List<CourierAckPending>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCourierMgmtPendig.Add(new CourierAckPending
                    {
                        courierMgmt_gid = dt["couriermgmt_gid"].ToString(),
                        courierref_no = dt["courierref_no"].ToString(),
                        date_of_courier = dt["date_of_courier"].ToString(),
                        ack_status = dt["ack_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        courier_type = dt["courier_type"].ToString(),
                        ack_date = dt["ack_date"].ToString(),
                        ackby_name = dt["ackby_name"].ToString(),
                    });
                }
                values.CourierAckPending = getCourierMgmtPendig;
            }
            dt_datatable.Dispose();

        }
        public void DaCourierAckView(string courierMgmt_gid, MdlEditCourierMgmt values)
        {
            try
            {
                msSQL = " select courierref_no, date_format(date_of_courier,'%d-%m-%Y') as date_of_courier," +
                        " address, sanctionref_no, customer_name, document_type, sender_name, pod_no, couriercompany_name," +
                        " courierhandover_to, courier_type, remarks,date_format(ack_date,'%d-%m-%Y') as ack_date, address, ack_status, ackby_name" +
                        " from ocs_trn_tcouriermgnt WHERE couriermgmt_gid='" + courierMgmt_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    objODBCDataReader.Read();

                    values.courierref_no = objODBCDataReader["courierref_no"].ToString();
                    values.date_of_courier = objODBCDataReader["date_of_courier"].ToString();
                    values.sanctionref_no = objODBCDataReader["sanctionref_no"].ToString();
                    values.customer_name = objODBCDataReader["customer_name"].ToString();
                    values.document_type = objODBCDataReader["document_type"].ToString();
                    values.sender_name = objODBCDataReader["sender_name"].ToString();
                    values.pod_no = objODBCDataReader["pod_no"].ToString();
                    values.couriercompany_name = objODBCDataReader["couriercompany_name"].ToString();
                    values.courierhandover_to = objODBCDataReader["courierhandover_to"].ToString();
                    values.courier_type = objODBCDataReader["courier_type"].ToString();
                    values.address = objODBCDataReader["address"].ToString();
                    values.ack_status = objODBCDataReader["ack_status"].ToString();
                    values.remarks = objODBCDataReader["remarks"].ToString();
                    values.ack_date = objODBCDataReader["ack_date"].ToString();
                    values.ackby_name = objODBCDataReader["ackby_name"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
                values.message = "Data Fetched";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
            }

        }
        public result DaAckStatus(string employee_gid, MdlEditCourierMgmt values)
        {
            msSQL = " UPDATE ocs_trn_tcouriermgnt SET" +
                    " ack_status='Acknowledged'," +
                    " ack_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " ackby_gid='" + employee_gid + "'," +
                    " ackby_name=(select concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) from adm_mst_tuser a left join hrm_mst_temployee b ON a.user_gid=b.user_gid where b.employee_gid='" + employee_gid + "')" +
                    " WHERE couriermgmt_gid='" + values.courierMgmt_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Acknowledged Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
            return values;
        }
        public void DaGetCourierDocTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ocs_trn_tcourierdocument where couriermgmt_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaGetEditCourierDoc(string employee_gid,string courier_gid,MdlEditCourierMgmt values)
        {
            msSQL = " select a.courierdocument_gid, a.document_name,a.document_path,a.document_title," +
                    " CONCAT(c.user_code,' / ',c.user_firstname,c.user_lastname) as created_by, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date" +
                    " from ocs_trn_tcourierdocument a " +
                    " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " WHERE a.couriermgmt_gid='" + courier_gid + "' or a.couriermgmt_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getDocList = new List<uploadcourierdocument>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getDocList.Add(new uploadcourierdocument
                        {
                            courierdocument_gid = dt["courierdocument_gid"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            document_title = dt["document_title"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString()
                        });
                    }
                    values.uploadcourierdocument = getDocList;
                }
                dt_datatable.Dispose();            
        }

    }
}