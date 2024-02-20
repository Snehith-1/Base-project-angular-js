using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Net;
using ems.storage.Functions;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ems.audit.DataAccess
{
    public class DaAtmTrnSampling
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        string msSQL,Query, msGetGid, msGetGid1, lssession_user, lssample_id, lssampleaudit_gid, lssample_value, lssample_refno, msGettaguseremployee_gid, lscompany_code, lsentity_name, lssample_name, lssamfin_code, lssamagro_code, lscodecreation_date,
            lsfield_1, lsfield_2, lsfield_3, lsfield_4, lsfield_5, lsfield_6, lsfield_7, lsfield_8, lsfield_9, lsfield_10, lspath, lsdocument_attached;
        DateTime ldcodecreation_date;
        string excelRange, endRange;
        int rowCount, columnCount;
        int insertCount = 0, logCount = 0;
        string lserrflag = "No";
        string sampleexcelimportlog_message = "";
        int mnResult;

        int k, lstotal_amount, lsoverall_score;
        public string ls_server, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsto2members, lsauditmaker_gid, lsauditormakerapprover_flag, lsauditormakerchecker_flag, lsauditorcheckerapprover_flag, lsauditchecker_gid, lsauditapprover_gid, lsemployee_gid, lsTo2members, lsauditormaker_name, lsBccmail_id, lscreated_by, lstomembers, lsdescription, lssample, lscc2members, lstonames, lsauditcreation_gid, lscreated_date, frommail_id, lscc_mail, strBCC, lsbcc_mail, lsaudit_name, lsaaudit_uniqueno, lsaudit_description, lsauditdepartment_name, lsaudittype_name, lscheckpointgroup_name;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;

        public string dateFormatStandardizer(string sentDate)
        {
            string[] dateArr = sentDate.Split(' ');
            DateTime ldreturnDate = DateTime.ParseExact(dateArr[0], "d/M/yyyy", CultureInfo.InvariantCulture);
            string returnDate = ldreturnDate.ToString("dd-MM-yyyy");
            return returnDate;
        }
        public void logforAudit(string strVal)
        {

            string loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + "ErrorLog/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            if ((!System.IO.Directory.Exists(loglspath)))
                System.IO.Directory.CreateDirectory(loglspath);

            loglspath = loglspath + "log.txt";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
            sw.WriteLine(strVal);
            sw.Close();

        }
        public void DaAtmTrnSamplingimportexcel(HttpRequest httpRequest, string employee_gid, result objResult, MdlAtmTrnSampling values)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, lssampleaudit_gid;
                string lsaudit_gid = httpRequest.Form["auditcreation_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Audit/SampleImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                byte[] bytes = ms.ToArray();
                if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                {
                    objResult.message = "File format is not supported";
                    return;
                }


                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                try
                {
                    using (ExcelPackage xlPackage = new ExcelPackage(ms))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                        rowCount = worksheet.Dimension.End.Row;
                        columnCount = worksheet.Dimension.End.Column;
                        endRange = worksheet.Dimension.End.Address;
                    }
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid, ms);
                    file.Close();
                    ms.Close();

                    objcmnfunctions.uploadFile(lspath, lsfile_gid);
                }
                catch (Exception ex)
                {
                   
                    objResult.status = false;
                    objResult.message = ex.ToString();
                    logforAudit("------ path creation " + ex.ToString() + " -------");
                    return;

                }
                //Excel To DataTable


                try
                {
                    lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";
                    excelRange = "A1:" + endRange + rowCount.ToString();
                    dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "The Source Contains No Datarows";
                    logforAudit("------ Excel To DataTable " + ex.ToString() + " -------");
                    return;
                }
                Nullable<DateTime> ldcodecreation_date;

                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
                string Header_name = "", Header_value = "";

                int null_count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    lssample_name = row["*Sample Name"].ToString();
                    if (string.IsNullOrEmpty(lssample_name))
                    {
                        null_count = null_count + 1;
                    }
                }
                if (null_count != 0)
                {
                    objResult.status = false;
                    objResult.message = "Kindly Add Sample Column Data";
                    return;

                }
                //foreach (var i in columnNames)
                //{
                //    Header_name = i.Trim();

                //    char[] one = Header_name.ToCharArray();
                //    char[] two = new char[one.Length];

                //    for (int j = 0; j < one.Length; j++)
                //    {
                //        if (!Char.IsLetterOrDigit(one[j]))
                //        {
                //            objResult.status = false;
                //            objResult.message = "Kindly Remove Special characters";
                //            return;
                //        }
                //    }
                //}
                foreach (var i in columnNames)
                {
                    Header_name = i.Trim();
                   
                    var withoutSpecial = new string(Header_name.Where(c => Char.IsLetterOrDigit(c)
                                                            || Char.IsWhiteSpace(c)).ToArray());

                    if (Header_name == "*Sample Name")
                    {

                    }

                   else if (Header_name != withoutSpecial)
                    {
                        objResult.status = false;
                        objResult.message = "Kindly Remove Special characters";
                        return;
                    }
                }


              

                foreach (DataRow row in dt.Rows)
                {
                    lssample_name = row["*Sample Name"].ToString();

                    msGetGid = objcmnfunctions.GetMasterGID("AUSI");
                    msSQL = " insert into atm_trn_tsampleimport(" +
                    " sampleimport_gid," +
                    " auditcreation_gid," +
                    " sample_name, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + lsaudit_gid + "'," +
                    "'" + lssample_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        foreach (var i in columnNames)
                        {
                            Header_name = i.Trim();
                            Header_name = Header_name.Replace("*", "");
                            Header_name = Header_name.Replace(" ", "_");
                            Header_value = row[i].ToString();

                            msGetGid1 = objcmnfunctions.GetMasterGID("SIDE");
                            msSQL = " insert into atm_trn_tsampleexcelimport(" +
                                    " excelimport_gid," +
                                    " sampleimport_gid, " +
                                    " auditcreation_gid, " +
                                    " header_names," +
                                    " header_values," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid1 + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsaudit_gid + "'," +
                                    "'" + Header_name.Replace("'", "") + "'," +
                                    "'" + Header_value.Replace("'", "") + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                    }
                    insertCount++;
                }

                if (mnResult != 0)
                {

                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";

                    msSQL = " select  auditcreation_gid,employee_gid,created_by, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                                " where auditcreation_gid = '" + lsaudit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                        lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                        lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                        lscreated_by = objODBCDatareader["created_by"].ToString();
                    }
                    objODBCDatareader.Close();
                    if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditapprover_gid && lsauditapprover_gid == lsauditmaker_gid && lscreated_by == lsauditapprover_gid)

                    {
                        k = 1;

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                        }
                        objODBCDatareader.Close();

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,d.auditeechecker_gid, a.auditmaker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                        " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + lsaudit_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                            lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                            lsto2members = objODBCDatareader["auditeechecker_gid"].ToString();
                            lscreated_date = objODBCDatareader["created_date"].ToString();
                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                        }
                        objODBCDatareader.Close();

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        k = k + 1;


                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + lsaudit_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                       "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                       "where b.employee_gid ='" + employee_gid + "'";
                        employeename = objdbconn.GetExecuteScalar(msSQL);

                        sub = " Audit Samples ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Samples have been uploaded for the Audit. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        //body = body + "Kindly log into systems to Approve the Audit.";
                        //body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + HttpUtility.HtmlEncode(employeename);
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(lsto_mail));


                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                               " auditcreation_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Audit has been Approval'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + lsemployee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }


                    else
                    {

                        msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                                    " where auditcreation_gid = '" + lsaudit_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                            lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                            lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();

                        }
                        objODBCDatareader.Close();
                        if (lsauditmaker_gid == lsauditapprover_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditchecker_gid)

                        {
                            k = 1;

                            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                            }
                            objODBCDatareader.Close();

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name,d.auditeemaker_gid,d.auditeechecker_gid, a.auditmaker_name, a.auditmapping_gid,a.employee_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                          " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                           " where a.auditcreation_gid ='" + lsaudit_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditmaker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["auditmaker_name"].ToString();
                                lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                                lsto2members = objODBCDatareader["auditeechecker_gid"].ToString();
                                lscreated_date = objODBCDatareader["created_date"].ToString();
                                lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                            }
                            objODBCDatareader.Close();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            k = k + 1;


                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsto2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + lsaudit_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                              " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                              " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);

                            sub = " Audit Samples ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "Samples have been uploaded for the Audit. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            //body = body + "Kindly log into systems to Approve the Audit.";
                            //body = body + "<br />";
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


                            lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                msSQL = "Insert into atm_trn_tauditmailcount( " +
                                   " auditcreation_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Audit has been Approval'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + lsemployee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else
                        {

                            msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                            if (lsauditormakerchecker_flag == "Y")

                            {
                                k = 1;

                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                    ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                }
                                objODBCDatareader.Close();

                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                           " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                         " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                      " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                       " where a.auditcreation_gid ='" + lsaudit_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                    lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lscreated_by = objODBCDatareader["created_by"].ToString();
                                    lscc2members = objODBCDatareader["CC2members"].ToString();
                                    lsTo2members = objODBCDatareader["To2members"].ToString();
                                    lscreated_date = objODBCDatareader["created_date"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                }
                                objODBCDatareader.Close();


                                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                sToken = "";
                                int Length = 100;
                                for (int j = 0; j < Length; j++)
                                {
                                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                    sToken += sTempChars;
                                }



                                k = k + 1;
                                lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                lsTo2members.Replace(",,", ",");

                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                lscc2members.Replace(employee_gid, "");
                                lscc2members.Replace(",,", ",");

                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                         " from atm_trn_tauditcreation  " +
                        " where auditcreation_gid ='" + lsaudit_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                }
                                objODBCDatareader.Close();

                                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                  " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                              " where employee_gid = '" + employee_gid + "'";
                                string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                sub = " Audit Samples ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "Samples have been uploaded for the Audit. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                body = body + "<br />";
                                //body = body + "Kindly log into systems to Approve the Audit.";
                                //body = body + "<br />";
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


                                lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                    msSQL = "Insert into atm_trn_tauditmailcount( " +
                                       " auditcreation_gid," +
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + lsemployee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Audit has been Approval'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + lsemployee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }

                            else
                            {

                                msSQL = " select auditorcheckerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                lsauditorcheckerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                                if (lsauditorcheckerapprover_flag == "Y")

                                {
                                    k = 1;

                                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        ls_server = objODBCDatareader["pop_server"].ToString();
                                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                              " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                               " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                                " where a.auditcreation_gid ='" + lsaudit_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lscreated_by = objODBCDatareader["created_by"].ToString();
                                        lscc2members = objODBCDatareader["CC2members"].ToString();
                                        lsTo2members = objODBCDatareader["To2members"].ToString();
                                        lscreated_date = objODBCDatareader["created_date"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();


                                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                    sToken = "";
                                    int Length = 100;
                                    for (int j = 0; j < Length; j++)
                                    {
                                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                        sToken += sTempChars;
                                    }



                                    k = k + 1;
                                    lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                    lsTo2members.Replace(",,", ",");

                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                    lscc2members.Replace(employee_gid, "");
                                    lscc2members.Replace(",,", ",");

                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                             " from atm_trn_tauditcreation  " +
                            " where auditcreation_gid ='" + lsaudit_gid + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                      " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                  " where employee_gid = '" + employee_gid + "'";
                                    string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                    sub = " Audit Samples ";
                                    body = "Dear All,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "Samples have been uploaded for the Audit. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    //body = body + "Kindly log into systems to Approve the Audit.";
                                    //body = body + "<br />";
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


                                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                                           " auditcreation_gid," +
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + lsemployee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Audit has been Approval'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                           "'" + lsemployee_gid + "'," +
                                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    }
                                }
                                else
                                {
                                    msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                                    lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                                    if (lsauditormakerapprover_flag == "Y")

                                    {
                                        k = 1;

                                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            ls_server = objODBCDatareader["pop_server"].ToString();
                                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                        }
                                        objODBCDatareader.Close();


                                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                                  " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                            " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                              " where a.auditcreation_gid ='" + lsaudit_gid + "'";
                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                            lscreated_by = objODBCDatareader["created_by"].ToString();
                                            lscc2members = objODBCDatareader["CC2members"].ToString();
                                            lsTo2members = objODBCDatareader["To2members"].ToString();
                                            lscreated_date = objODBCDatareader["created_date"].ToString();
                                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                        }
                                        objODBCDatareader.Close();


                                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                        sToken = "";
                                        int Length = 100;
                                        for (int j = 0; j < Length; j++)
                                        {
                                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                            sToken += sTempChars;
                                        }



                                        k = k + 1;
                                        lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                        lsTo2members.Replace(",,", ",");

                                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                        lscc2members.Replace(employee_gid, "");
                                        lscc2members.Replace(",,", ",");

                                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                                 " from atm_trn_tauditcreation  " +
                                " where auditcreation_gid ='" + lsaudit_gid + "'";

                                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader.HasRows == true)
                                        {
                                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                        }
                                        objODBCDatareader.Close();

                                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                          " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                      " where employee_gid = '" + employee_gid + "'";
                                        string employee_name = objdbconn.GetExecuteScalar(msSQL);


                                        sub = " Audit Samples ";
                                        body = "Dear All,<br />";
                                        body = body + "<br />";
                                        body = body + "Greetings,  <br />";
                                        body = body + "<br />";
                                        body = body + "Samples have been uploaded for the Audit. The details are as follows, <br />";
                                        body = body + "<br />";
                                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Reference Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                        body = body + "<br />";
                                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                        body = body + "<br />";
                                        //body = body + "Kindly log into systems to Approve the Audit.";
                                        //body = body + "<br />";
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


                                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                                               " auditcreation_gid," +
                                               " from_mail," +
                                               " to_mail," +
                                               " cc_mail," +
                                               " mail_status," +
                                               " mail_senddate, " +
                                               " created_by," +
                                               " created_date)" +
                                               " values(" +
                                               "'" + values.auditcreation_gid + "'," +
                                               "'" + lsemployee_gid + "'," +
                                               "'" + lsto_mail + "'," +
                                               "'" + cc_mailid + "'," +
                                               "'Audit has been Approval'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                               "'" + lsemployee_gid + "'," +
                                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {

                                            k = 1;

                                            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                            if (objODBCDatareader.HasRows == true)
                                            {
                                                ls_server = objODBCDatareader["pop_server"].ToString();
                                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                                ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                                ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                            }
                                            objODBCDatareader.Close();

                                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as CC2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as To2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                            " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                            " where a.auditcreation_gid ='" + lsaudit_gid + "'";
                                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                            if (objODBCDatareader.HasRows == true)
                                            {
                                                lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                                lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                                lscreated_by = objODBCDatareader["created_by"].ToString();
                                                lscc2members = objODBCDatareader["CC2members"].ToString();
                                                lsTo2members = objODBCDatareader["To2members"].ToString();
                                                lscreated_date = objODBCDatareader["created_date"].ToString();
                                                lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                            }
                                            objODBCDatareader.Close();


                                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                            sToken = "";
                                            int Length = 100;
                                            for (int j = 0; j < Length; j++)
                                            {
                                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                                sToken += sTempChars;
                                            }

                                            k = k + 1;
                                            lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                            lsTo2members.Replace(",,", ",");

                                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                                    " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                            lscc2members.Replace(employee_gid, "");
                                            lscc2members.Replace(",,", ",");

                                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                                     " from atm_trn_tauditcreation  " +
                                    " where auditcreation_gid ='" + lsaudit_gid + "'";

                                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                            if (objODBCDatareader.HasRows == true)
                                            {
                                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                            }
                                            objODBCDatareader.Close();

                                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                                                                              " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                          " where employee_gid = '" + employee_gid + "'";
                                            string employee_name = objdbconn.GetExecuteScalar(msSQL);

                                            sub = " Audit Samples ";
                                            body = "Dear All,<br />";
                                            body = body + "<br />";
                                            body = body + "Greetings,  <br />";
                                            body = body + "<br />";
                                            body = body + "Samples have been uploaded for the Audit. The details are as follows, <br />";
                                            body = body + "<br />";
                                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                            body = body + "<br />";
                                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                            body = body + "<br />";
                                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                            body = body + "<br />";
                                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                            body = body + "<br />";
                                            //body = body + "Kindly log into systems to Approve the Audit.";
                                            //body = body + "<br />";
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


                                            lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                                msSQL = "Insert into atm_trn_tauditmailcount( " +
                                                   " auditcreation_gid," +
                                                   " from_mail," +
                                                   " to_mail," +
                                                   " cc_mail," +
                                                   " mail_status," +
                                                   " mail_senddate, " +
                                                   " created_by," +
                                                   " created_date)" +
                                                   " values(" +
                                                   "'" + lsaudit_gid + "'," +
                                                   "'" + employee_gid + "'," +
                                                   "'" + lsto_mail + "'," +
                                                   "'" + cc_mailid + "'," +
                                                   "'Audit Samples Uploaded'," +
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
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                            dt.Dispose();

                        }
                    

            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = "The Source Contains No Datarows";
            }
        }
    

        
        public void DaGetSamplesummary(string auditcreation_gid, MdlAtmTrnSampling values, string employee_gid)
        {
            try
            {

                msSQL = " SELECT a.sampleimport_gid,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM atm_trn_tsampleimport a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                       " where a.auditcreation_gid='" + auditcreation_gid + "' order  by a.sampleimport_gid desc ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsample_list = new List<sample_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsample_list.Add(new sample_list
                        {
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            sample_name = (dr_datarow["sample_name"].ToString()),
                            samfin_code = (dr_datarow["samfin_code"].ToString()),
                            samagro_code = (dr_datarow["samagro_code"].ToString()),
                            codecreation_date = (dr_datarow["codecreation_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                        });
                    }
                    values.sample_list = getsample_list;
                }
                dt_datatable.Dispose();
                values.status = true;

            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetSampleDynamicdata(string auditcreation_gid, sampledynamicdatadtl values, string employee_gid)
        {

            try
            {
                //msSQL = "CALL GetparticularSampledata ('" + auditcreation_gid + "')";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                msSQL = " select auditobservation_name from atm_trn_tauditcreation where " +
                         " auditcreation_gid='" + auditcreation_gid + "'";
                values.auditobservation_name = objdbconn.GetExecuteScalar(msSQL);
                if (values.auditobservation_name == "Observation score overall audit")
                {
                    msSQL = " update atm_trn_tobservationscoresample set auditobservation_name ='Observation score overall audit'" +
                         " where auditcreation_gid ='" + auditcreation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "CALL GetAuditSampledata ('" + auditcreation_gid + "')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        Query = objODBCDatareader["sqlquery"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = Query + ", a.sampleimport_gid,b.raisedquery_flag,b.taguser_flag,c.checklistmaster_gid,d.observationscoresample_gid,d.auditobservation_name " +
                   " from atm_trn_tsampleexcelimport a " +
                   " left join atm_trn_tsampleimport b on a.sampleimport_gid = b.sampleimport_gid" +
                   " left join atm_trn_tauditcreation2checklist c on a.auditcreation_gid = c.auditcreation_gid" +
                   " left join atm_trn_tobservationscoresample d on a.auditcreation_gid = d.auditcreation_gid" +
                    " where a.auditcreation_gid = '" + auditcreation_gid + "' GROUP BY a.sampleimport_gid ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);



                    if (dt_datatable.Rows.Count != 0)
                    {
                        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                        Dictionary<string, object> childRow;
                        foreach (DataRow row in dt_datatable.Rows)
                        {
                            childRow = new Dictionary<string, object>();
                            foreach (DataColumn col in dt_datatable.Columns)
                            {
                                childRow.Add(col.ColumnName, row[col]);
                            }
                            parentRow.Add(childRow);
                        }
                        string json = jsSerializer.Serialize(parentRow);
                        values.JSONdata = json;
                        values.status = true;
                    }
                    dt_datatable.Dispose();

                    values.status = true;

                }
            
                else
                {
                msSQL = "CALL GetAuditSampledata ('" + auditcreation_gid + "')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    Query = objODBCDatareader["sqlquery"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = Query + ", a.sampleimport_gid,b.raisedquery_flag,b.taguser_flag,c.checklistmaster_gid,d.observationscoresample_gid " +
               " from atm_trn_tsampleexcelimport a " +
               " left join atm_trn_tsampleimport b on a.sampleimport_gid = b.sampleimport_gid" +
               " left join atm_trn_tauditcreation2checklist c on a.auditcreation_gid = c.auditcreation_gid" +
               " left join atm_trn_tobservationscoresample d on a.auditcreation_gid = d.auditcreation_gid" +
                " where a.auditcreation_gid = '" + auditcreation_gid + "' GROUP BY a.sampleimport_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);



                if (dt_datatable.Rows.Count != 0)
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                    Dictionary<string, object> childRow;
                    foreach (DataRow row in dt_datatable.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dt_datatable.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                    string json = jsSerializer.Serialize(parentRow);
                    values.JSONdata = json;
                    values.status = true;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
        }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetSampleRaiseQueryDynamicdata(string auditcreation_gid, sampledynamicdatadtl values, string employee_gid)
        {
            try
            {
                msSQL = "CALL GetparticularTaggedUserSample ('" + auditcreation_gid + "','" + employee_gid + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                //var getSampleDynamicdata = new List<SampleDynamicdata>();


                if (dt_datatable.Rows.Count != 0)
                {
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                    Dictionary<string, object> childRow;
                    foreach (DataRow row in dt_datatable.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dt_datatable.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                    string json = jsSerializer.Serialize(parentRow);
                    values.JSONdata = json;
                    values.status = true;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        //public void DaGetTagFlag(string sampleimport_gid, MdlAtmTrnSampling values, string employee_gid)
        //{
        //    msSQL = "select a.sampleimport_gid,b.raisedquery_flag,b.taguser_flag,b.tagged_employee FROM atm_trn_tsampleexcelimport a " +
        //        "left join atm_trn_tsampleimport b on a.sampleimport_gid = b.sampleimport_gid " +
        //          " where a.sampleimport_gid='" + sampleimport_gid + "'";
        //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //    if (objODBCDatareader.HasRows == true)
        //    {
        //        values.taguser_flag = objODBCDatareader["taguser_flag"].ToString();
        //        values.tagged_employee = objODBCDatareader["tagged_employee"].ToString();
        //    }
        //    objODBCDatareader.Close();


        //}

        public void DaGetSampleAuditor(string auditcreation_gid, MdlAtmTrnSampling values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.sampleimport_gid,d.audit_uniqueno,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date,e.employee_gid,e.auditmapping_gid, d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                         " left join atm_mst_tchecklistmaster e on e.checklistmaster_gid = d.checklistmaster_gid " +
                         " where(a.created_by ='" + employee_gid + "' or d.created_by = '" + employee_gid + "' or  d.auditmapping2employee_gid = '" + employee_gid + "' or " +
                         " d.auditmapping_gid = '" + employee_gid + "' or d.employee_gid = '" + employee_gid + "' or e.auditmapping_gid = '" + employee_gid + "'or e.employee_gid = '" + employee_gid + "') and a.auditcreation_gid ='" + auditcreation_gid + "' group by a.sampleimport_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)

                {
                    msSQL = " SELECT distinct a.sampleimport_gid,d.audit_uniqueno,case when f.samplequery_status is null then 'NoQuery' else f.samplequery_status end as samplequery_status, a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date,e.employee_gid,e.auditmapping_gid, d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                         " left join atm_mst_tchecklistmaster e on e.checklistmaster_gid = d.checklistmaster_gid " +
                         " left join atm_trn_tsamplequeries f on f.sampleimport_gid = a.sampleimport_gid " +
                         " where(a.created_by ='" + employee_gid + "' or d.created_by = '" + employee_gid + "' or  d.auditmapping2employee_gid = '" + employee_gid + "' or " +
                         " d.auditmapping_gid = '" + employee_gid + "' or d.employee_gid = '" + employee_gid + "' or e.auditmapping_gid = '" + employee_gid + "'or e.employee_gid = '" + employee_gid + "') and a.auditcreation_gid ='" + auditcreation_gid + "' group by a.sampleimport_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getssample_list = new List<sample_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getssample_list.Add(new sample_list
                            {
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                audit_uniqueno = (dr_datarow["sampleimport_gid"].ToString()),
                                samplequery_status = (dr_datarow["samplequery_status"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                sample_name = (dr_datarow["sample_name"].ToString()),
                                samfin_code = (dr_datarow["samfin_code"].ToString()),
                                samagro_code = (dr_datarow["samagro_code"].ToString()),
                                codecreation_date = (dr_datarow["codecreation_date"].ToString()),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                replyquery_flag = (dr_datarow["replyquery_flag"].ToString()),

                            });
                        }
                        values.sample_list = getssample_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                else
                {

                    msSQL = " SELECT distinct a.sampleimport_gid,d.audit_uniqueno,case when f.samplequery_status is null then 'NoQuery' else f.samplequery_status end as samplequery_status,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date,group_concat(e.employee_name) as tag_user,d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                          " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid" +
                          " left join atm_mst_ttaguser2employee e on e.auditcreation_gid = a.auditcreation_gid" +
                          " left join atm_trn_tsamplequeries f on f.sampleimport_gid = a.sampleimport_gid " +
                          " where a.auditcreation_gid = '" + auditcreation_gid + "' and (e.employee_gid = '" + employee_gid + "')  group by a.sampleimport_gid desc";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsample_list = new List<sample_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getsample_list.Add(new sample_list
                            {
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                audit_uniqueno = (dr_datarow["sampleimport_gid"].ToString()),
                                samplequery_status = (dr_datarow["samplequery_status"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                sample_name = (dr_datarow["sample_name"].ToString()),
                                samfin_code = (dr_datarow["samfin_code"].ToString()),
                                samagro_code = (dr_datarow["samagro_code"].ToString()),
                                codecreation_date = (dr_datarow["codecreation_date"].ToString()),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                replyquery_flag = (dr_datarow["replyquery_flag"].ToString()),
                            });
                        }


                        values.sample_list = getsample_list;
                    }

                    dt_datatable.Dispose();
                    values.status = true;

                }

            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }




        public void DaGetSample(string auditcreation_gid, string sampleimport_gid, MdlAtmTrnSampling values, string employee_gid)
        {
            try
            {
                string[] sampleimportgid_array = sampleimport_gid.Split(',');
                msSQL = " SELECT distinct a.sampleimport_gid,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date,e.employee_gid,e.auditmapping_gid, d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                         " left join atm_mst_tchecklistmaster e on e.checklistmaster_gid = d.checklistmaster_gid " +
                         " where(a.created_by ='" + employee_gid + "' or d.created_by = '" + employee_gid + "' or" +
                         " d.auditmapping_gid = '" + employee_gid + "' or d.employee_gid = '" + employee_gid + "' or e.auditmapping_gid = '" + employee_gid + "'or e.employee_gid = '" + employee_gid + "') and a.auditcreation_gid ='" + auditcreation_gid + "' group by a.sampleimport_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)

                {
                    msSQL = " SELECT distinct a.sampleimport_gid,d.audit_uniqueno, case when f.samplequery_status is null then 'NoQuery' else f.samplequery_status end as samplequery_status,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date,e.employee_gid,e.auditmapping_gid, d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                         " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid " +
                         " left join atm_mst_tchecklistmaster e on e.checklistmaster_gid = d.checklistmaster_gid " +
                         " left join atm_trn_tsamplequeries f on f.sampleimport_gid = a.sampleimport_gid " +
                         " where(a.created_by ='" + employee_gid + "' or d.created_by = '" + employee_gid + "' or" +
                         " d.auditmapping_gid = '" + employee_gid + "' or d.employee_gid = '" + employee_gid + "' or e.auditmapping_gid = '" + employee_gid + "'or e.employee_gid = '" + employee_gid + "') and a.auditcreation_gid ='" + auditcreation_gid + "' group by a.sampleimport_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getssample_list = new List<sample_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getssample_list.Add(new sample_list
                            {
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                audit_uniqueno = (dr_datarow["sampleimport_gid"].ToString()),
                                samplequery_status = (dr_datarow["samplequery_status"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                sample_name = (dr_datarow["sample_name"].ToString()),
                                samfin_code = (dr_datarow["samfin_code"].ToString()),
                                samagro_code = (dr_datarow["samagro_code"].ToString()),
                                codecreation_date = (dr_datarow["codecreation_date"].ToString()),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                replyquery_flag = (dr_datarow["replyquery_flag"].ToString()),

                            });
                        }
                        values.sample_list = getssample_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }


                else
                {
                    int array_length = sampleimportgid_array.Length;
                    string sample_querystring = "";
                    for (int i = 0; i < sampleimportgid_array.Length; i++)
                    {
                        if (i == sampleimportgid_array.Length - 1)
                        {
                            sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "'";
                        }
                        else
                        {
                            sample_querystring = sample_querystring + "'" + sampleimportgid_array[i] + "',";
                        }
                    }

                    msSQL = " SELECT distinct a.sampleimport_gid,d.audit_uniqueno,case when f.samplequery_status is null then 'NoQuery' else f.samplequery_status end as samplequery_status,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date,(e.employee_name) as tag_user,d.employee_gid,d.auditmapping_gid,a.replyquery_flag,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by FROM atm_trn_tsampleimport a " +
                              " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                              " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                              " left join atm_trn_tauditcreation d on d.auditcreation_gid = a.auditcreation_gid" +
                              " left join atm_mst_ttaguser2employee e on e.auditcreation_gid = a.auditcreation_gid" +
                               " left join atm_trn_tsamplequeries f on f.sampleimport_gid = a.sampleimport_gid " +
                              " where a.auditcreation_gid = '" + auditcreation_gid + "' and (e.employee_gid = '" + employee_gid + "') and a.sampleimport_gid in (" + sample_querystring + ") group by a.sampleimport_gid desc";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getsample_list = new List<sample_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getsample_list.Add(new sample_list
                            {
                                auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                                audit_uniqueno = (dr_datarow["sampleimport_gid"].ToString()),
                                samplequery_status = (dr_datarow["samplequery_status"].ToString()),
                                sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                                sample_name = (dr_datarow["sample_name"].ToString()),
                                samfin_code = (dr_datarow["samfin_code"].ToString()),
                                samagro_code = (dr_datarow["samagro_code"].ToString()),
                                codecreation_date = (dr_datarow["codecreation_date"].ToString()),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                replyquery_flag = (dr_datarow["replyquery_flag"].ToString()),
                            });
                        }


                        values.sample_list = getsample_list;
                    }

                    dt_datatable.Dispose();
                    values.status = true;

                }

            }

            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public void DaGetSampleName(string sampleimport_gid, MdlAtmTrnSampling values)
        {
            msSQL = " select  sample_name, sampleimport_gid  from atm_trn_tsampleimport " +
                  " where sampleimport_gid='" + sampleimport_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sample_name = objODBCDatareader["sample_name"].ToString();
                values.sampleimport_gid = objODBCDatareader["sampleimport_gid"].ToString();
            }
            objODBCDatareader.Close();


        }

        public void DaGetSampleView(string sampleimport_gid, MdlAtmTrnSampling values)
        {

            msSQL = " SELECT a.sampleimport_gid,a.sample_name,a.samfin_code,a.auditcreation_gid,a.samagro_code,a.codecreation_date, a.field1, a.field2, a.field3, a.field4, a.field5, a.field6, a.field7, a.field8, a.field9, a.field10 FROM atm_trn_tsampleimport a " +
                    " where a.sampleimport_gid='" + sampleimport_gid +
                    "' order  by a.sampleimport_gid desc ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                values.sampleimport_gid = objODBCDatareader["sampleimport_gid"].ToString();
                values.sample_name = objODBCDatareader["sample_name"].ToString();
                values.samfin_code = objODBCDatareader["samfin_code"].ToString();
                values.samagro_code = objODBCDatareader["samagro_code"].ToString();
                values.codecreation_date = objODBCDatareader["codecreation_date"].ToString();
                values.field1 = objODBCDatareader["field1"].ToString();
                values.field2 = objODBCDatareader["field2"].ToString();
                values.field3 = objODBCDatareader["field3"].ToString();
                values.field4 = objODBCDatareader["field4"].ToString();
                values.field5 = objODBCDatareader["field5"].ToString();
                values.field6 = objODBCDatareader["field6"].ToString();
                values.field7 = objODBCDatareader["field7"].ToString();
                values.field8 = objODBCDatareader["field8"].ToString();
                values.field9 = objODBCDatareader["field9"].ToString();
                values.field10 = objODBCDatareader["field10"].ToString();

            }
            objODBCDatareader.Close();
            values.status = true;


        }


        public void DaGetTagUser(MdlAtmTrnSampling values, string employee_gid)
        {

            msSQL = " update atm_trn_tsampleimport set tagged_employee ='Y'" +
                    " where sampleimport_gid='" + values.sampleimport_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            string msGetgroup_tagusergid = objcmnfunctions.GetMasterGID("GTUG");
            for (var i = 0; i < values.employelist.Count; i++)
            {
                msGettaguseremployee_gid = objcmnfunctions.GetMasterGID("AISL");
                msSQL = "Insert into atm_mst_ttaguser2employee( " +
                       " taguser2employee_gid, " +
                       " auditcreation_gid," +
                       " sampleimport_gid," +
                       " group_tagusergid, " +
                       " sample_name," +
                       " tag_remarks," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGettaguseremployee_gid + "'," +
                       "'" + values.auditcreation_gid + "'," +
                       "'" + values.sampleimport_gid + "'," +
                       "'" + msGetgroup_tagusergid + "'," +
                       "'" + values.sample_name + "'," +
                       "'" + values.description + "'," +
                       "'" + values.employelist[i].employee_gid + "'," +
                       "'" + values.employelist[i].employee_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //lstomembers = values.employelist[i].employee_gid;
                lssample = values.sample_name;
                lsdescription = values.description;
                //lsemployee_name = values.employelist[i].employee_name;
                lsemployee_gid = employee_gid;

            }

            if (mnResult != 0)
            {
                msSQL = " update atm_trn_tsampleimport set taguser_flag ='Y' where sampleimport_gid='" + values.sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Sample Tagged successfully";

                msSQL = " select  auditcreation_gid,employee_gid, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                                  " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                    lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();

                }
                objODBCDatareader.Close();
                if (lsauditmaker_gid == lsauditchecker_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditmaker_gid)
                {

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                    }
                    objODBCDatareader.Close();


                    msSQL = " select  a.auditcreation_gid, group_concat(d.employee_gid) as Tomembers, group_concat(d.employee_name) as Tonames,  group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',' , a.employee_gid )  as CC2members  from atm_trn_tauditcreation a" +
                    " left join atm_mst_ttaguser2employee d on a.auditcreation_gid = d.auditcreation_gid" +
                         " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                        " left join atm_trn_tauditagainstmultipleauditeechecker e on a.auditcreation_gid = e.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "' and d.sampleimport_gid = '" + values.sampleimport_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lstomembers = objODBCDatareader["Tomembers"].ToString();
                        //lscreated_date = dr_datarow["created_date"].ToString();
                        lstonames = objODBCDatareader["Tonames"].ToString();
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();

                        //lscc2members = lscc2members.Replace(employee_gid, " ");
                        //lscc2members.Replace(",,", ",");
                    }
                    objODBCDatareader.Close();

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;

                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name from atm_trn_tauditcreation a" +
            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                    }
                    objODBCDatareader.Close();

                    sub = " RE: Tagged Audit ";
                    body = "Dear " + HttpUtility.HtmlEncode(lstonames)+ ",<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "You have been tagged for the sample in the Audit. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Sample :</b> " + HttpUtility.HtmlEncode(lssample)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Description :</b> " + HttpUtility.HtmlEncode(lsdescription)+ "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + "Audit Team";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));

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


                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                           " auditcreation_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.auditcreation_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Tagged Audit Sample'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }


                else
                {
                    msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                    lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                    if (lsauditormakerchecker_flag == "Y")

                    {

                        k = 1;

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                        }
                        objODBCDatareader.Close();


                        msSQL = " select  a.auditcreation_gid, group_concat(d.employee_gid) as Tomembers, group_concat(d.employee_name) as Tonames,  group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',' , a.employee_gid, ',', a.auditmapping2employee_gid )  as CC2members  from atm_trn_tauditcreation a" +
                        " left join atm_mst_ttaguser2employee d on a.auditcreation_gid = d.auditcreation_gid" +
                        " left join atm_trn_tauditagainstmultipleauditeechecker e on a.auditcreation_gid = e.auditcreation_gid  " +
                             " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                                " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "' and d.sampleimport_gid = '" + values.sampleimport_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            lstomembers = objODBCDatareader["Tomembers"].ToString();
                            //lscreated_date = dr_datarow["created_date"].ToString();
                            lstonames = objODBCDatareader["Tonames"].ToString();
                            lscc2members = objODBCDatareader["CC2members"].ToString();
                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();

                            lscc2members = lscc2members.Replace(employee_gid, " ");
                            lscc2members.Replace(",,", ",");
                        }
                        objODBCDatareader.Close();

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        k = k + 1;

                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " select a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name from atm_trn_tauditcreation a" +
                " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                        }
                        objODBCDatareader.Close();

                        sub = " RE: Tagged Audit ";
                        body = "Dear " + HttpUtility.HtmlEncode(lstonames)+ ",<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "You have been tagged for the sample in the Audit. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Sample :</b> " + HttpUtility.HtmlEncode(lssample)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Description :</b> " + HttpUtility.HtmlEncode(lsdescription)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to view more details.";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "Thanks & Regards, ";
                        body = body + "<br />";
                        body = body + "Audit Team";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + "<br />";
                        body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress(ls_username);
                        //message.To.Add(new MailAddress(lsto_mail));

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


                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                        smtp.Send(message);

                        values.status = true;

                        if (values.status == true)
                        {
                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                               " auditcreation_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Tagged Audit Sample'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                    }
                    else
                    {
                        msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                        lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                        if (lsauditormakerapprover_flag == "Y")

                        {

                            k = 1;

                            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                            }
                            objODBCDatareader.Close();


                            msSQL = " select  a.auditcreation_gid, group_concat(d.employee_gid) as Tomembers, group_concat(d.employee_name) as Tonames,  group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',' , a.employee_gid, ',', a.auditmapping_gid )  as CC2members  from atm_trn_tauditcreation a" +
                            " left join atm_mst_ttaguser2employee d on a.auditcreation_gid = d.auditcreation_gid" +
                           " left join atm_trn_tauditagainstmultipleauditeechecker e on a.auditcreation_gid = e.auditcreation_gid  " +
                                 " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                                    " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                " where a.auditcreation_gid ='" + values.auditcreation_gid + "' and d.sampleimport_gid = '" + values.sampleimport_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                lstomembers = objODBCDatareader["Tomembers"].ToString();
                                //lscreated_date = dr_datarow["created_date"].ToString();
                                lstonames = objODBCDatareader["Tonames"].ToString();
                                lscc2members = objODBCDatareader["CC2members"].ToString();
                                lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();

                                lscc2members = lscc2members.Replace(employee_gid, " ");
                                lscc2members.Replace(",,", ",");
                            }
                            objODBCDatareader.Close();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            k = k + 1;

                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name from atm_trn_tauditcreation a" +
                    " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                            }
                            objODBCDatareader.Close();

                            sub = " RE: Tagged Audit ";
                            body = "Dear " + HttpUtility.HtmlEncode(lstonames)+ ",<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "You have been tagged for the sample in the Audit. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Sample :</b> " + HttpUtility.HtmlEncode(lssample)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Description :</b> " + HttpUtility.HtmlEncode(lsdescription)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to view more details.";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Thanks & Regards, ";
                            body = body + "<br />";
                            body = body + "Audit Team";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            //message.To.Add(new MailAddress(lsto_mail));

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


                            lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                            smtp.Send(message);

                            values.status = true;

                            if (values.status == true)
                            {
                                msSQL = "Insert into atm_trn_tauditmailcount( " +
                                   " auditcreation_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Tagged Audit Sample'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }


                        else
                        {
                            msSQL = " select auditorcheckerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            lsauditorcheckerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                            if (lsauditorcheckerapprover_flag == "Y")

                            {

                                k = 1;

                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                    ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                }
                                objODBCDatareader.Close();


                                msSQL = " select  a.auditcreation_gid, group_concat(d.employee_gid) as Tomembers, group_concat(d.employee_name) as Tonames,  group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',' , a.employee_gid, ',', a.auditmapping_gid)  as CC2members  from atm_trn_tauditcreation a" +
                                " left join atm_mst_ttaguser2employee d on a.auditcreation_gid = d.auditcreation_gid" +
                              " left join atm_trn_tauditagainstmultipleauditeechecker e on a.auditcreation_gid = e.auditcreation_gid  " +
                                     " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                                        " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                    " where a.auditcreation_gid ='" + values.auditcreation_gid + "' and d.sampleimport_gid = '" + values.sampleimport_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {

                                    lstomembers = objODBCDatareader["Tomembers"].ToString();
                                    //lscreated_date = dr_datarow["created_date"].ToString();
                                    lstonames = objODBCDatareader["Tonames"].ToString();
                                    lscc2members = objODBCDatareader["CC2members"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();

                                    lscc2members = lscc2members.Replace(employee_gid, " ");
                                    lscc2members.Replace(",,", ",");
                                }
                                objODBCDatareader.Close();

                                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                sToken = "";
                                int Length = 100;
                                for (int j = 0; j < Length; j++)
                                {
                                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                    sToken += sTempChars;
                                }

                                k = k + 1;

                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                msSQL = " select a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name from atm_trn_tauditcreation a" +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                }
                                objODBCDatareader.Close();

                                sub = " RE: Tagged Audit ";
                                body = "Dear " + lstonames + ",<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "You have been tagged for the sample in the Audit. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Sample :</b> " + HttpUtility.HtmlEncode(lssample)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Description :</b> " + HttpUtility.HtmlEncode(lsdescription)+ "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to view more details.";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "Thanks & Regards, ";
                                body = body + "<br />";
                                body = body + "Audit Team";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + "<br />";
                                body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                MailMessage message = new MailMessage();
                                SmtpClient smtp = new SmtpClient();
                                message.From = new MailAddress(ls_username);
                                //message.To.Add(new MailAddress(lsto_mail));

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


                                lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                smtp.Send(message);

                                values.status = true;

                                if (values.status == true)
                                {
                                    msSQL = "Insert into atm_trn_tauditmailcount( " +
                                       " auditcreation_gid," +
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Tagged Audit Sample'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }

                            }

                            else
                            {
                                try
                                {

                                    k = 1;

                                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        ls_server = objODBCDatareader["pop_server"].ToString();
                                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                    }
                                    objODBCDatareader.Close();


                                    msSQL = " select  a.auditcreation_gid, group_concat(d.employee_gid) as Tomembers, group_concat(d.employee_name) as Tonames,  group_concat(distinct e.auditeemaker_gid, ',', e.auditeechecker_gid, ',' , a.employee_gid, ',', a.auditmapping_gid, ',', a.auditmapping2employee_gid )  as CC2members  from atm_trn_tauditcreation a" +
                                    " left join atm_mst_ttaguser2employee d on a.auditcreation_gid = d.auditcreation_gid" +
                                     " left join atm_trn_tauditagainstmultipleauditeechecker e on a.auditcreation_gid = e.auditcreation_gid  " +
                                         " left join hrm_mst_temployee b on d.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "' and d.sampleimport_gid = '" + values.sampleimport_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {

                                        lstomembers = objODBCDatareader["Tomembers"].ToString();
                                        //lscreated_date = dr_datarow["created_date"].ToString();
                                        lstonames = objODBCDatareader["Tonames"].ToString();
                                        lscc2members = objODBCDatareader["CC2members"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();

                                        lscc2members = lscc2members.Replace(employee_gid, " ");
                                        lscc2members.Replace(",,", ",");
                                    }
                                    objODBCDatareader.Close();

                                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                    sToken = "";
                                    int Length = 100;
                                    for (int j = 0; j < Length; j++)
                                    {
                                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                        sToken += sTempChars;
                                    }

                                    k = k + 1;

                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lstomembers.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                                    msSQL = " select a.audit_uniqueno, a.audit_name, a.auditdepartment_name, a.checkpointgroup_name from atm_trn_tauditcreation a" +
                            " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();

                                    }
                                    objODBCDatareader.Close();

                                    sub = " RE: Tagged Audit ";
                                    body = "Dear " + lstonames + ",<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "You have been tagged for the sample in the Audit. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Sample :</b> " + HttpUtility.HtmlEncode(lssample)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Description :</b> " + HttpUtility.HtmlEncode(lsdescription)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to view more details.";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "Thanks & Regards, ";
                                    body = body + "<br />";
                                    body = body + "Audit Team";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + "<br />";
                                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                                    MailMessage message = new MailMessage();
                                    SmtpClient smtp = new SmtpClient();
                                    message.From = new MailAddress(ls_username);
                                    //message.To.Add(new MailAddress(lsto_mail));

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


                                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                    smtp.Send(message);

                                    values.status = true;

                                    if (values.status == true)
                                    {
                                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                                           " auditcreation_gid," +
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Tagged Audit Sample'," +
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
                            }
                        }
                    }
                }
                    }
            else
            {
                        values.message = "Error Occured while Tagging Sample";
                        values.status = false;
                    }
                }
            
        



        public void DaGetEmployeeName(string sampleimport_gid, employelist values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from atm_mst_ttaguser2employee " +
                  " where sampleimport_gid='" + sampleimport_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select sample_name from atm_trn_tsampleimport" +
                 " where sampleimport_gid='" + sampleimport_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sample_name = objODBCDatareader["sample_name"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaEditSampleQuery(string sampleimport_gid, MdlAtmTrnSampling values)
        {
            msSQL = " select sampleimport_gid from atm_trn_tsampleimport " +
                    " where sampleimport_gid='" + sampleimport_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sampleimport_gid = objODBCDatareader["sampleimport_gid"].ToString();

            }
            objODBCDatareader.Close();
            values.status = true;
        }


        public void DaPostRaiseQuery(samplequerydata values, string employee_gid)
        {

            //msSQL = " update atm_trn_tsampleimport set description='" + values.description.Replace("'", "") + "'" +
            //       " where sampleimport_gid='" + values.sampleimport_gid + "' ";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "select concat(user_firstname,'',user_lastname,'/',user_code) from adm_mst_tuser a " +
                      "left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                      "where b.employee_gid ='" + employee_gid + "'";
            employeename = objdbconn.GetExecuteScalar(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("ASRQ");
            msSQL = "Insert into atm_trn_tsampleraisequery( " +
                   " sampleraisequery_gid, " +
                   " sampleimport_gid," +
                   " auditcreation_gid," +
                   " sample_name, " +
                   " query_title, " +
                   " query_to, " +
                   " query_toname, " +
                   " sampleraisequery_status, " +
                   " description," +
                   " raisequery_raisedby," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.sampleimport_gid + "', " +
                   "'" + values.auditcreation_gid + "', " +
                   "'" + values.sample_name + "'," +
                   "'" + values.query_title.Replace("'", "") + "'," +
                   "'" + values.query_to + "'," +
                   "'" + values.query_toname + "'," +
                   "'Open'," +
                   "'" + values.description.Replace("'", "") + "'," +
                    "'" + employeename + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                msSQL = " update atm_trn_tsampleimport set raisedquery_flag='Y'  where sampleimport_gid='" + values.sampleimport_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Query Raised Successfully";

                msSQL = " select  auditcreation_gid,employee_gid,created_by, auditmapping_gid,auditmapping2employee_gid from atm_trn_tauditcreation " +
                           " where auditcreation_gid = '" + values.auditcreation_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsauditmaker_gid = objODBCDatareader["employee_gid"].ToString();
                    lsauditchecker_gid = objODBCDatareader["auditmapping_gid"].ToString();
                    lsauditapprover_gid = objODBCDatareader["auditmapping2employee_gid"].ToString();
                }
                objODBCDatareader.Close();

                if (lsauditmaker_gid == lsauditapprover_gid && lsauditchecker_gid == lsauditmaker_gid && lsauditapprover_gid == lsauditchecker_gid)
                {
                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                    }
                    objODBCDatareader.Close();

                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid,d.auditeemaker_gid,d.auditeechecker_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                        " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                        lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                        lscreated_by = objODBCDatareader["created_by"].ToString();
                        lscc2members = objODBCDatareader["auditeemaker_gid"].ToString();
                        lsTo2members = objODBCDatareader["auditeechecker_gid"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                    }
                    objODBCDatareader.Close();

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;
                    //lsTo2members = lsTo2members.Replace(employee_gid, " ");
                    //lsTo2members.Replace(",,", ",");

                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    //lscc2members.Replace(employee_gid, "");
                    //lscc2members.Replace(",,", ",");

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
             " from atm_trn_tauditcreation  " +
            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                    }
                    objODBCDatareader.Close();


                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                         " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                     " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    sub = "Audit Query   ";
                    body = "Dear All,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to View the details.";
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


                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                           " auditcreation_gid," +
                           " sampleimport_gid," +
                           " from_mail," +
                           " to_mail," +
                           " cc_mail," +
                           " mail_status," +
                           " mail_senddate, " +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + values.auditcreation_gid + "'," +
                           "'" + values.sampleimport_gid + "'," +
                           "'" + employee_gid + "'," +
                           "'" + lsto_mail + "'," +
                           "'" + cc_mailid + "'," +
                           "'Sample Query Raised'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {

                    msSQL = " select auditormakerchecker_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                    lsauditormakerchecker_flag = objdbconn.GetExecuteScalar(msSQL);

                    if (lsauditormakerchecker_flag == "Y")

                    {
                        k = 1;

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                            ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                        }
                        objODBCDatareader.Close();

                        msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as To2members ,group_concat(distinct a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                           " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                      " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                            lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                            lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                            lscreated_by = objODBCDatareader["created_by"].ToString();
                            lscc2members = objODBCDatareader["CC2members"].ToString();
                            lsTo2members = objODBCDatareader["To2members"].ToString();
                            lscreated_date = objODBCDatareader["created_date"].ToString();
                            lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                        }
                        objODBCDatareader.Close();

                        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                        sToken = "";
                        int Length = 100;
                        for (int j = 0; j < Length; j++)
                        {
                            string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                            sToken += sTempChars;
                        }

                        k = k + 1;
                        lsTo2members = lsTo2members.Replace(employee_gid, " ");
                        lsTo2members.Replace(",,", ",");

                        msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                        lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                        lscc2members.Replace(employee_gid, "");
                        lscc2members.Replace(",,", ",");

                        msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                        cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                 " from atm_trn_tauditcreation  " +
                " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsaudit_name = objODBCDatareader["audit_name"].ToString();
                            lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                            lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                            lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                        }
                        objODBCDatareader.Close();


                        msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                             " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                         " where employee_gid = '" + employee_gid + "'";
                        string employee_name = objdbconn.GetExecuteScalar(msSQL);

                        sub = "Audit Query   ";
                        body = "Dear All,<br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                        body = body + "<br />";
                        body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                        body = body + "<br />";
                        body = body + "Kindly log into systems to View the details.";
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


                        lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                    message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                            msSQL = "Insert into atm_trn_tauditmailcount( " +
                               " auditcreation_gid," +
                               " sampleimport_gid," +
                               " from_mail," +
                               " to_mail," +
                               " cc_mail," +
                               " mail_status," +
                               " mail_senddate, " +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + values.auditcreation_gid + "'," +
                               "'" + values.sampleimport_gid + "'," +
                               "'" + employee_gid + "'," +
                               "'" + lsto_mail + "'," +
                               "'" + cc_mailid + "'," +
                               "'Sample Query Raised'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }

                    else
                    {

                        msSQL = " select auditormakerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                        lsauditormakerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                        if (lsauditormakerapprover_flag == "Y")
                        {
                            k = 1;

                            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                            }
                            objODBCDatareader.Close();

                            msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as To2members ,group_concat(distinct a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                        " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                lscreated_by = objODBCDatareader["created_by"].ToString();
                                lscc2members = objODBCDatareader["CC2members"].ToString();
                                lsTo2members = objODBCDatareader["To2members"].ToString();
                                lscreated_date = objODBCDatareader["created_date"].ToString();
                                lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                            }
                            objODBCDatareader.Close();

                            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                            sToken = "";
                            int Length = 100;
                            for (int j = 0; j < Length; j++)
                            {
                                string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                sToken += sTempChars;
                            }

                            k = k + 1;
                            lsTo2members = lsTo2members.Replace(employee_gid, " ");
                            lsTo2members.Replace(",,", ",");

                            msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                    " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                            lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                            lscc2members.Replace(employee_gid, "");
                            lscc2members.Replace(",,", ",");

                            msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                            cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                            msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                     " from atm_trn_tauditcreation  " +
                    " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                            }
                            objODBCDatareader.Close();


                            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                 " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                             " where employee_gid = '" + employee_gid + "'";
                            string employee_name = objdbconn.GetExecuteScalar(msSQL);

                            sub = "Audit Query   ";
                            body = "Dear All,<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                            body = body + "<br />";
                            body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno) + "<br />";
                            body = body + "<br />";
                            body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                            body = body + "<br />";
                            body = body + "Kindly log into systems to View the details.";
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


                            lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                        message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                                msSQL = "Insert into atm_trn_tauditmailcount( " +
                                   " auditcreation_gid," +
                                   " sampleimport_gid," +
                                   " from_mail," +
                                   " to_mail," +
                                   " cc_mail," +
                                   " mail_status," +
                                   " mail_senddate, " +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + values.auditcreation_gid + "'," +
                                   "'" + values.sampleimport_gid + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + lsto_mail + "'," +
                                   "'" + cc_mailid + "'," +
                                   "'Sample Query Raised'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                        }
                        else
                        {

                            msSQL = " select auditorcheckerapprover_flag from atm_trn_tauditcreation where auditcreation_gid = '" + values.auditcreation_gid + "'";

                            lsauditorcheckerapprover_flag = objdbconn.GetExecuteScalar(msSQL);

                            if (lsauditorcheckerapprover_flag == "Y")
                            {
                                k = 1;

                                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    ls_server = objODBCDatareader["pop_server"].ToString();
                                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                    ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                    ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                }
                                objODBCDatareader.Close();

                                msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as To2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                           " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                     " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                       " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                    lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                    lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                    lscreated_by = objODBCDatareader["created_by"].ToString();
                                    lscc2members = objODBCDatareader["CC2members"].ToString();
                                    lsTo2members = objODBCDatareader["To2members"].ToString();
                                    lscreated_date = objODBCDatareader["created_date"].ToString();
                                    lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                }
                                objODBCDatareader.Close();

                                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                sToken = "";
                                int Length = 100;
                                for (int j = 0; j < Length; j++)
                                {
                                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                    sToken += sTempChars;
                                }

                                k = k + 1;
                                lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                lsTo2members.Replace(",,", ",");

                                msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                lscc2members.Replace(employee_gid, "");
                                lscc2members.Replace(",,", ",");

                                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                         " from atm_trn_tauditcreation  " +
                        " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == true)
                                {
                                    lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                    lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                    lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                    lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                }
                                objODBCDatareader.Close();


                                msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                     " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                 " where employee_gid = '" + employee_gid + "'";
                                string employee_name = objdbconn.GetExecuteScalar(msSQL);

                                sub = "Audit Query   ";
                                body = "Dear All,<br />";
                                body = body + "<br />";
                                body = body + "Greetings,  <br />";
                                body = body + "<br />";
                                body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                                body = body + "<br />";
                                body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                body = body + "<br />";
                                body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name) + "<br />";
                                body = body + "<br />";
                                body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name) + "<br />";
                                body = body + "<br />";
                                body = body + "Kindly log into systems to View the details.";
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


                                lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                            message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                                    msSQL = "Insert into atm_trn_tauditmailcount( " +
                                       " auditcreation_gid," +
                                       " sampleimport_gid," +
                                       " from_mail," +
                                       " to_mail," +
                                       " cc_mail," +
                                       " mail_status," +
                                       " mail_senddate, " +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + values.auditcreation_gid + "'," +
                                       "'" + values.sampleimport_gid + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + lsto_mail + "'," +
                                       "'" + cc_mailid + "'," +
                                       "'Sample Query Raised'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }

                            else
                            {
                                try
                                {
                                    k = 1;

                                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        ls_server = objODBCDatareader["pop_server"].ToString();
                                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                        ls_username = ConfigurationManager.AppSettings["SamunnatiAuditEmail"];
                                        ls_password = ConfigurationManager.AppSettings["SamunnatiAuditEmailPassword"];
                                    }
                                    objODBCDatareader.Close();

                                    msSQL = " select  a.auditcreation_gid, a.auditchecker_name, a.auditmaker_name,a.auditapprover_name, a.auditmapping_gid, group_concat(distinct d.auditeemaker_gid, ',',d.auditeechecker_gid)  as To2members ,group_concat(distinct a.employee_gid, ',',a.auditmapping_gid, ',',a.auditmapping2employee_gid)  as CC2members , concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.created_date from atm_trn_tauditcreation a" +
                                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                            " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                                     " left join atm_trn_tauditagainstmultipleauditeechecker d on a.auditcreation_gid = d.auditcreation_gid  " +
                                        " where a.auditcreation_gid ='" + values.auditcreation_gid + "'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsemployee_name = objODBCDatareader["auditchecker_name"].ToString();
                                        lsemployee_gid = objODBCDatareader["auditmapping_gid"].ToString();
                                        lsauditormaker_name = objODBCDatareader["auditmaker_name"].ToString();
                                        lscreated_by = objODBCDatareader["created_by"].ToString();
                                        lscc2members = objODBCDatareader["CC2members"].ToString();
                                        lsTo2members = objODBCDatareader["To2members"].ToString();
                                        lscreated_date = objODBCDatareader["created_date"].ToString();
                                        lsauditcreation_gid = objODBCDatareader["auditcreation_gid"].ToString();
                                    }
                                    objODBCDatareader.Close();

                                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                                    sToken = "";
                                    int Length = 100;
                                    for (int j = 0; j < Length; j++)
                                    {
                                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                                        sToken += sTempChars;
                                    }

                                    k = k + 1;
                                    lsTo2members = lsTo2members.Replace(employee_gid, " ");
                                    lsTo2members.Replace(",,", ",");

                                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                                    lscc2members.Replace(employee_gid, "");
                                    lscc2members.Replace(",,", ",");

                                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                                    msSQL = " select audit_uniqueno, audit_name, auditdepartment_name, checkpointgroup_name" +
                             " from atm_trn_tauditcreation  " +
                            " where auditcreation_gid ='" + values.auditcreation_gid + "'";

                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsaudit_name = objODBCDatareader["audit_name"].ToString();
                                        lsaaudit_uniqueno = objODBCDatareader["audit_uniqueno"].ToString();
                                        lsauditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();
                                        lscheckpointgroup_name = objODBCDatareader["checkpointgroup_name"].ToString();
                                    }
                                    objODBCDatareader.Close();


                                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +

                                                                         " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                                                     " where employee_gid = '" + employee_gid + "'";
                                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                                    sub = "Audit Query   ";
                                    body = "Dear All,<br />";
                                    body = body + "<br />";
                                    body = body + "Greetings,  <br />";
                                    body = body + "<br />";
                                    body = body + "Query has been rasied for the sample in the audit. The details are as follows, <br />";
                                    body = body + "<br />";
                                    body = body + "<b> Audit Name :</b> " + HttpUtility.HtmlEncode(lsaudit_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Refernce Number :</b> " + HttpUtility.HtmlEncode(lsaaudit_uniqueno)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Audit Department :</b> " + HttpUtility.HtmlEncode(lsauditdepartment_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "<b>Checkpoint Group :</b> " + HttpUtility.HtmlEncode(lscheckpointgroup_name)+ "<br />";
                                    body = body + "<br />";
                                    body = body + "Kindly log into systems to View the details.";
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


                                    lsBccmail_id = ConfigurationManager.AppSettings["auditbcc"].ToString();

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
                                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple CC email Id
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
                                        msSQL = "Insert into atm_trn_tauditmailcount( " +
                                           " auditcreation_gid," +
                                           " sampleimport_gid," +
                                           " from_mail," +
                                           " to_mail," +
                                           " cc_mail," +
                                           " mail_status," +
                                           " mail_senddate, " +
                                           " created_by," +
                                           " created_date)" +
                                           " values(" +
                                           "'" + values.auditcreation_gid + "'," +
                                           "'" + values.sampleimport_gid + "'," +
                                           "'" + employee_gid + "'," +
                                           "'" + lsto_mail + "'," +
                                           "'" + cc_mailid + "'," +
                                           "'Sample Query Raised'," +
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
                            }
                        }
                    }
                }
            }
            else
            {

                values.message = "Error Occured While Raising Query";
                values.status = false;
            }
            }
        

        public void DaAssignedQuerySummary(string employee_gid, MdlAtmTrnSampling values, string auditcreation_gid)
        {
            try
            {
                msSQL = " SELECT a.sampleraisequery_gid,a.auditcreation_gid, a.sampleraisequery_status, a. sampleimport_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, a.description," +
                    " (a.employee_name) as assigned_to,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    "  a.employee_gid " +
                    " FROM atm_trn_tsampleraisequery a " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.created_by  " +
                    " left join adm_mst_tuser c on c.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = a.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = f.user_gid " +
                " WHERE a.auditcreation_gid= '" + auditcreation_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSampleAssignedQueryList = new List<SampleAssignedQueryList>();
                if (dt_datatable.Rows.Count != 0)

                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSampleAssignedQueryList.Add(new SampleAssignedQueryList

                        {

                            sampleraisequery_gid = (dr_datarow["sampleraisequery_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            assigned_by = (dr_datarow["created_by"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            sampleraisequery_status = (dr_datarow["sampleraisequery_status"].ToString()),
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                        });
                    }
                    values.SampleAssignedQueryList = getSampleAssignedQueryList;
                }

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }




        public void DaEditAssignedQuery(string sampleraisequery_gid, MdlAtmTrnSampling values)
        {
            msSQL = " select sampleraisequery_gid from atm_trn_tsampleraisequery " +
                    " where sampleraisequery_gid='" + sampleraisequery_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.sampleraisequery_gid = objODBCDatareader["sampleraisequery_gid"].ToString();

            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public bool DaPostSampleQuerydetail(string employee_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AUSQ");
            msSQL = " insert into atm_trn_tsamplequeries(" +
                    " samplequery_gid," +
                    " auditcreation_gid," +
                    " sampleimport_gid," +
                    " remarks," +
                    " sampleresponse_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.auditcreation_gid + "'," +
                    "'" + values.sampleimport_gid + "'," +
                    "'" + values.remarks.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                //msSQL = " update atm_trn_tsamplequeries set samplequery_status='Open'  where sampleimport_gid='" + values.sampleimport_gid + "' ";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Message Posted Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }
        }
        public bool DaGetSampleQuerydetaillist(string employee_gid, string sampleimport_gid, MdlAtmTrnMyAuditTaskAuditee values)
        {

            msSQL = "select a.samplequeries2response_gid,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,a.sampleresponse_gid, a.sampleimport_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from atm_trn_tsamplequeries2response a " +
                    " left join hrm_mst_temployee b on a.sampleresponse_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.sampleimport_gid = '" + sampleimport_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSampleQuerydetaillist = new List<SampleQuerydetaillist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["sampleresponse_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }

                    getSampleQuerydetaillist.Add(new SampleQuerydetaillist
                    {
                        samplequeries2response_gid = (dr_datarow["samplequeries2response_gid"].ToString()),
                        remarks = (dr_datarow["remarks"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        session_user = lssession_user,
                    });
                }
                values.SampleQuerydetaillist = getSampleQuerydetaillist;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }


        public void Daclosequerysample(MdlAtmTrnMyAuditTaskAuditee values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("ASQL");

            msSQL = " insert into atm_trn_tsamplequeriescloselog (" +
                      " samplequerycloselog_gid, " +
                      " auditcreation_gid," +
                      " sampleimport_gid," +
                      " closing_description," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditcreation_gid + "'," +
                      " '" + values.sampleimport_gid + "'," +
                      " '" + values.closing_description.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                msSQL = " update atm_trn_tsamplequeries set samplequery_status='Closed' where sampleimport_gid='" + values.sampleimport_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update atm_trn_tsampleraisequery set sampleraisequery_status='Closed'  where sampleimport_gid='" + values.sampleimport_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Message Posted Successfully";


                values.status = true;
                values.message = "Message Posted Successfully";
            }


            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }


        public void Daclosesamplequerysummary(MdlAtmTrnMyAuditTaskAuditee values, string sampleimport_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.sampleimport_gid, a.samplequerycloselog_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, a.closing_description, a.auditcreation_gid, d.samplequery_status, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by" +
                        " FROM atm_trn_tsamplequeriescloselog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join atm_trn_tsamplequeries d on d.sampleimport_gid = a.sampleimport_gid " +
                        " where d.sampleimport_gid = '" + sampleimport_gid + "' and d.samplequery_status = 'Closed'";
                //" where a.auditcreation_gid ='" + values.auditcreation_gid + "' order by a.taguser2audit_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getclosequerysample_list = new List<closequerysample_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getclosequerysample_list.Add(new closequerysample_list
                        {
                            sampleimport_gid = (dr_datarow["sampleimport_gid"].ToString()),
                            auditcreation_gid = (dr_datarow["auditcreation_gid"].ToString()),
                            closing_description = (dr_datarow["closing_description"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            samplequery_status = (dr_datarow["samplequery_status"].ToString())
                        });
                    }
                    values.closequerysample_list = getclosequerysample_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)

            {
                values.status = false;
            }
        }

        public void DaGetSampleclosequery(MdlAtmTrnMyAuditTaskAuditee values, string sampleimport_gid)
        {

            msSQL = "select samplequery_status from atm_trn_tsamplequeries " +
                  " where sampleimport_gid='" + sampleimport_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.samplequery_status = objODBCDatareader["samplequery_status"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetSampleimportexcel(HttpRequest httpRequest, string employee_gid, result objResult, MdlAtmTrnSampling values)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath, lssampleaudit_gid, GetText;
                string lsaudit_gid = httpRequest.Form["auditcreation_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid, ms);
                file.Close();
                ms.Close();


                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";


                excelRange = "A1:" + endRange + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);

                Nullable<DateTime> ldcodecreation_date;

                //string[] sampleimportgid_array = auditcreation_gid.Split(',');

                //int iCellCount = 2;  // 
                //var cellValue = string.Empty;

                //foreach (ExcelReportCell excelCell in excelRow) // 
                //{
                //    cellValue = excelCell.GetText().ToString(); // 
                //    for (int i = 1; i <= iCellCount; i++)
                //    {
                //        dt.Columns.Add();
                //        // Expected to assign each column values
                //    }
                //}
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);




                foreach (DataRow row in dt.Rows)
                {

                    lssampleaudit_gid = row["* Audit ID"].ToString();


                    lssample_name = row["* Sample Name"].ToString();
                    lssamfin_code = row["* SamFin Code "].ToString();
                    lssamagro_code = row["* SamAgro Code"].ToString();

                    lscodecreation_date = row["* Date of Code Creation(DD-MM-YYYY)"].ToString();


                    lsfield_1 = row["* Field 1"].ToString();
                    lsfield_2 = row["* Field 2"].ToString();
                    lsfield_3 = row["* Field 3"].ToString();
                    lsfield_4 = row["* Field 4"].ToString();
                    lsfield_5 = row["* Field 5"].ToString();
                    lsfield_6 = row["* Field 6"].ToString();
                    lsfield_7 = row["* Field 7"].ToString();
                    lsfield_8 = row["* Field 8"].ToString();
                    lsfield_9 = row["* Field 9"].ToString();
                    lsfield_10 = row["* Field 10"].ToString();


                    msGetGid = objcmnfunctions.GetMasterGID("AUSI");

                    msSQL = " insert into atm_trn_tsampleimport(" +
                    " sampleimport_gid," +
                    " auditcreation_gid," +
                    " sample_name," +
                    " samfin_code," +
                    " samagro_code," +
                    " codecreation_date," +
                    " field1," +
                    " field2," +
                    " field3," +
                    " field4," +
                    " field5," +
                    " field6," +
                    " field7," +
                    " field8," +
                    " field9," +
                    " field10," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + lssampleaudit_gid + "'," +
                    "'" + lssample_name + "'," +
                    "'" + lssamfin_code + "'," +
                    "'" + lssamagro_code + "'," +
                    "'" + lscodecreation_date + "'," +
                    "'" + lsfield_1.Replace("'", "") + "'," +
                    "'" + lsfield_2.Replace("'", "") + "'," +
                    "'" + lsfield_3.Replace("'", "") + "'," +
                    "'" + lsfield_4.Replace("'", "") + "'," +
                    "'" + lsfield_5.Replace("'", "") + "'," +
                    "'" + lsfield_6.Replace("'", "") + "'," +
                    "'" + lsfield_7.Replace("'", "") + "'," +
                    "'" + lsfield_8.Replace("'", "") + "'," +
                    "'" + lsfield_9.Replace("'", "") + "'," +
                    "'" + lsfield_10.Replace("'", "") + "',";
                    msSQL += "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";


                    //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //msSQL = " update atm_trn_tsamplequeries set samplequery_status='NoQuery'  where auditcreation_gid='" + auditcreation_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    insertCount++;
                }

                if (mnResult != 0)
                {

                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }


            }


            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }

        }

        public void DaAtmTrnSampleexcelupload(HttpRequest httpRequest, string employee_gid, result objResult, MdlAtmTrnSampling values)
        {
            try
            {
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string auditcreation_gid = httpRequest.Form["auditcreation_gid"];

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("/erpdocument" + "/" + lscompany_code + "/Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Audit/ImportExcelDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid, ms);
                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";


                excelRange = "A1:" + endRange + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);

                Nullable<DateTime> ldcodecreation_date;

                foreach (DataRow row in dt.Rows)
                {
                    sampleexcelimportlog_message = "";

                    lssample_name = row["* Sample Name"].ToString();
                    if (lssample_name == "")
                    {
                        sampleexcelimportlog_message = "Sample Name is Empty";
                    }

                    else
                    {
                        lssample_name = row["* Sample Name"].ToString();
                        lssamfin_code = row["* SamFin Code "].ToString();
                        lssamagro_code = row["* SamAgro Code"].ToString();

                        lscodecreation_date = row["* Date of Code Creation(DD-MM-YYYY)"].ToString();

                        lsfield_1 = row["* Field 1"].ToString();
                        lsfield_2 = row["* Field 2"].ToString();
                        lsfield_3 = row["* Field 3"].ToString();
                        lsfield_4 = row["* Field 4"].ToString();
                        lsfield_5 = row["* Field 5"].ToString();
                        lsfield_6 = row["* Field 6"].ToString();
                        lsfield_7 = row["* Field 7"].ToString();
                        lsfield_8 = row["* Field 8"].ToString();
                        lsfield_9 = row["* Field 9"].ToString();
                        lsfield_10 = row["* Field 10"].ToString();



                        msGetGid = objcmnfunctions.GetMasterGID("AUSI");

                        msSQL = " insert into atm_trn_tsampleimport(" +
                        " sampleimport_gid," +
                        " auditcreation_gid," +
                        " sample_name," +
                        " samfin_code," +
                        " samagro_code," +
                        " codecreation_date," +
                        " field1," +
                        " field2," +
                        " field3," +
                        " field4," +
                        " field5," +
                        " field6," +
                        " field7," +
                        " field8," +
                        " field9," +
                        " field10," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + auditcreation_gid + "'," +
                        "'" + lssample_name + "'," +
                        "'" + lssamfin_code + "'," +
                        "'" + lssamagro_code + "'," +
                        "'" + lscodecreation_date + "'," +
                        "'" + lsfield_1.Replace("'", "") + "'," +
                        "'" + lsfield_2.Replace("'", "") + "'," +
                        "'" + lsfield_3.Replace("'", "") + "'," +
                        "'" + lsfield_4.Replace("'", "") + "'," +
                        "'" + lsfield_5.Replace("'", "") + "'," +
                        "'" + lsfield_6.Replace("'", "") + "'," +
                        "'" + lsfield_7.Replace("'", "") + "'," +
                        "'" + lsfield_8.Replace("'", "") + "'," +
                        "'" + lsfield_9.Replace("'", "") + "'," +
                        "'" + lsfield_10.Replace("'", "") + "',";
                        msSQL += "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";




                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                        //msGetGid = objcmnfunctions.GetMasterGID("AISL");

                        //msSQL = " insert into atm_trn_tsampleimportlog(" +
                        //" sampleimportlog_gid," +
                        //" sampleimport_gid," +
                        //" sample_name," +
                        //" samfin_code," +
                        //" samagro_code," +
                        //" codecreation_date," +
                        //" created_by," +
                        //" created_date)" +
                        //" values(" +
                        //"'" + msGetGid + "'," +
                        //"'" + values.sampleimport_gid + "'," +
                        //"'" + lssample_name + "'," +
                        //"'" + lssamfin_code + "'," +
                        //"'" + lssamagro_code + "',";
                        //if ((ldcodecreation_date == null))
                        //{
                        //    msSQL += "null,";
                        //}
                        //else
                        //{
                        //    msSQL += "'" + Convert.ToDateTime(ldcodecreation_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        //}
                        //msSQL += "'" + employee_gid + "'," +
                        //"'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    if (mnResult != 0)
                    {
                        insertCount++;
                        objResult.status = true;
                        objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                    }
                    else
                    {
                        objResult.status = false;
                        objResult.message = "Error occured in uploading Excel Sheet Details";
                    }

                    dt.Dispose();

                }

            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }

        }

        public void DaAssignedTagUserSummary(string sampleimport_gid, MdlAtmTrnSampling values)
        {
            try
            {
                msSQL = " select group_concat(a.employee_name) as employee_name, a.tag_remarks as tag_remarks, " +
                      " concat(c.user_firstname, c.user_lastname, '/', c.user_code) as created_by, " +
                      " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                      " from atm_mst_ttaguser2employee a " +
                      " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                      " where a.sampleimport_gid = '" + sampleimport_gid + "' group by a.group_tagusergid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSampleAssignedQueryList = new List<SampleAssignedQueryList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSampleAssignedQueryList.Add(new SampleAssignedQueryList
                        {
                            assigned_to = (dr_datarow["employee_name"].ToString()),
                            assigned_by = (dr_datarow["created_by"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            description = (dr_datarow["tag_remarks"].ToString()),
                        });
                    }
                    values.SampleAssignedQueryList = getSampleAssignedQueryList;
                }

                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }


        public bool DaGetDocUploadlist(string employee_gid, string auditcreation_gid, DocUploadlog values)
        {

            msSQL = "select a.raisequerydoc_gid,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date ,a.docuploader_gid,a.document_name,a.document_path," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/ ', c.user_code) as sender_name " +
                    " from atm_trn_traisequerydoc a " +
                    " left join hrm_mst_temployee b on a.docuploader_gid = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " where a.auditcreation_gid = '" + auditcreation_gid + "' order by a.created_date asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocUploadlogdtl = new List<DocUploadlogdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                var file_name = new List<string>();
                var file_path = string.Empty;

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["docuploader_gid"].ToString() == employee_gid)
                    {
                        lssession_user = "Y";
                    }
                    else
                    {
                        lssession_user = "N";
                    }
                    if (dr_datarow["document_name"].ToString() == "")
                    {
                        lsdocument_attached = "N";
                    }
                    else
                    {
                        lsdocument_attached = "Y";
                    }                   
                    getDocUploadlogdtl.Add(new DocUploadlogdtl
                    {
                        raisequerydoc_gid = (dr_datarow["raisequerydoc_gid"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        sender_name = (dr_datarow["sender_name"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        session_user = lssession_user,
                        document_attached = lssession_user + "/" + lsdocument_attached
                    });
                }
                values.DocUploadlogdtl = getDocUploadlogdtl;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public void DaQueryDocUpload(HttpRequest httpRequest, document_upload objfilename, string employee_gid, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            String path = lspath;
            string lsdocument_title = httpRequest.Form["document_title"];
            string auditcreation_gid = HttpContext.Current.Request.Params["auditcreation_gid"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "AUDIT/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }


                        lspath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "AUDIT/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                      
                        objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "AUDIT/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "AUDIT/QueryDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("AQUD");
                        msSQL = " insert into atm_trn_traisequerydoc(" +
                                " raisequerydoc_gid," +
                                " auditcreation_gid," +
                                " docuploader_gid," +
                                " document_name ," +
                                " document_path," +
                                " created_by, " +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + auditcreation_gid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        
                    }

                    if (mnResult != 0)
                    {
                        objfilename.status = true;
                        objfilename.message = "Document Uploaded Successfully";
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "Error Occured";
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }


    }
}



