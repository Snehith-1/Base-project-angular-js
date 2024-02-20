using ems.lgl.Models;
using ems.utilities.Functions;
using ems.storage.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ems.lgl.DataAccess
{
    public class DaCustomerDashboard
    {

        ems.utilities.Functions.dbconn objdbconn = new ems.utilities.Functions.dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcConnection objODBCconnection;
        OdbcDataReader objodbcdatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnresult;
        HttpPostedFile httpPostedFile;
        string lscompany_code, path, lspath, ls_username, ls_password;
        string ls_server, msGetDocumentGid;
        int ls_port;

        public bool DaGetCustomerLoanDetails(loanlist values, string customer_gid)
        {
            
            msSQL = " select loanref_no,sanction_refno,sanction_date,loan_title " +
                    " from ocs_trn_tloan where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanlistdtl = new List<loandtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanlistdtl.Add(new loandtl
                    {
                        loanref_no = (dr_datarow["loanref_no"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        loan_title = (dr_datarow["loan_title"].ToString()),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                    });
                }
                values.loandtl = getloanlistdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetLawyerSRAssign(assignSRLawyer values, string legalSR_gid)
        {
            msSQL = " select concat(b.lawyeruser_name,'/',b.lawyeruser_code) as lawyeruser_name,b.lawyeruser_gid,"+
                    " date_format(a.assigned_date,'%d-%m-%Y') as assigned_date,a.SLN_remarks, " +
                    " concat(d.user_firstname,' ',d.user_lastname,' / ',d.user_code) as assignedlawyer_by  from lgl_trn_traiselegalSR a " +
                    " left join lgl_mst_tlawyeruser b on a.SRassigned_lawyer = b.lawyeruser_gid " +
                    " left join hrm_mst_temployee c on c.employee_gid=a.assigned_by" +
                    " left join adm_mst_tuser d on d.user_gid=c.user_gid" +
                    " where a.raiselegalSR_gid = '" + legalSR_gid + "'";
            objodbcdatareader = objdbconn .GetDataReader(msSQL);
            if(objodbcdatareader.HasRows==true)
            {
                if(objodbcdatareader["lawyeruser_gid"].ToString()!="")
                {
                    values.assign_lawyername = objodbcdatareader["lawyeruser_name"].ToString();
                    values.assign_lawyergid = objodbcdatareader["lawyeruser_gid"].ToString();
                    values.assignedlawyer_by = objodbcdatareader["assignedlawyer_by"].ToString();
                    values.assigned_date = objodbcdatareader["assigned_date"].ToString();
                    values.SLN_remarks = objodbcdatareader["SLN_remarks"].ToString();
                }
                else
                {
                    values.assign_lawyergid = "Y";
                }
            }
            objodbcdatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetCustomerDocumentDetails(UploadDocument_name values, string customer_gid)
        {           
            msSQL = " select a.customer_documentgid,a.document_name,a.document_path, " +
                    " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,a.created_date " +
                    " from lgl_trn_tcustomerdocument a " +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable (msSQL);
            var get_filename = new List<UploadDocumentModel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentModel
                    {
                        customer_documentgid = (dr_datarow["customer_documentgid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(dr_datarow["document_path"].ToString())),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                values.filename_list = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetCustomerMailDetails(composemail_list values, string customer_gid)
        {            
            msSQL = " select a.composemail_gid,a.from_mail,a.to_mail,a.cc_mail,a.bcc_mail, " +
                    " a.mail_subject,a.mail_content,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) " +
                    " as created_by,a.created_date from lgl_trn_tcustomercomposemail a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.customer_gid = '" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable (msSQL);
            var get_composemail = new List<composemail>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_composemail.Add(new composemail
                    {
                        composemail_gid = (dr_datarow["composemail_gid"].ToString()),
                        from_mail = (dr_datarow["from_mail"].ToString()),
                        to_mail = (dr_datarow["to_mail"].ToString()),
                        cc_mail = (dr_datarow["cc_mail"].ToString()),
                        bcc_mail = (dr_datarow["bcc_mail"].ToString()),
                        subject_mail = (dr_datarow["mail_subject"].ToString()),
                        content_mail = (dr_datarow["mail_content"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),

                    });
                }
                values.composemail = get_composemail;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetCustomerMail(composemail values, string composemail_gid)
        {
            msSQL = " select a.composemail_gid,a.from_mail,a.to_mail,a.cc_mail,a.bcc_mail, " +
                    " a.mail_subject,a.mail_content,concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) " +
                    " as created_by,a.created_date from lgl_trn_tcustomercomposemail a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    " where a.composemail_gid = '" + composemail_gid + "'";
            objodbcdatareader = objdbconn .GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.composemail_gid = (objodbcdatareader["composemail_gid"].ToString());
                values.from_mail = (objodbcdatareader["from_mail"].ToString());
                values.to_mail = (objodbcdatareader["to_mail"].ToString());
                values.cc_mail = (objodbcdatareader["cc_mail"].ToString());
                values.bcc_mail = (objodbcdatareader["bcc_mail"].ToString());
                values.subject_mail = (objodbcdatareader["mail_subject"].ToString());
                values.content_mail = (objodbcdatareader["mail_content"].ToString());
                values.created_by = (objodbcdatareader["created_by"].ToString());
                values.created_date = (objodbcdatareader["created_date"].ToString());
            }
            objodbcdatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaPostDocumentUpload(HttpRequest httpRequest, string employee_gid, UploadDocument_name objfilename)
        {
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string document_name = httpRequest.Form["document_name"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = "SELECT * from adm_mst_tcompany where 1=1";
            objodbcdatareader = objdbconn .GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                objodbcdatareader.Read();
                lscompany_code = objodbcdatareader["company_code"].ToString();
            }
            objodbcdatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "LGL/CustomerDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month+ "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("LECU");
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
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        string lsfile_gid = msdocument_gid + FileExtension;
                   
                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".pdf") || (FileExtension == ".odt") || (FileExtension == ".csv") || (FileExtension == ".txt") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".msg") || (FileExtension == ".ppt")||(FileExtension == ".pptx")|| (FileExtension == ".oft") || (FileExtension == ".html"))
                        {
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);
                            //CopyStream(ms, ls_readStream);

                            byte[] bytes = ms.ToArray();
                            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                            {
								objfilename.status = false;
                                objfilename.message = "File format is not supported";
                                return false;
                            }

                            lspath = path;
                            objcmnfunctions.uploadFile(lspath, lsfile_gid);
                            lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/CustomerDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;


                            msGetGid = objcmnfunctions.GetMasterGID("LECU");

                            msSQL = " insert into lgl_trn_tcustomerdocument( " +
                                    " customer_documentgid," +
                                    " customer_gid, " +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date " +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + httpRequest.Form["customer_gid"].ToString() + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnresult == 1) 
                            {
                                objfilename.message = "Document Uploaded Successfully..!";
                                objfilename.status = true;
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "Error Occured..!";
                            }
                        }

                    }

                    msSQL = " select a.customer_documentgid,a.document_name,a.document_path, " +
                            " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,a.created_date " +
                            " from lgl_trn_tcustomerdocument a " +
                            " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                            " where a.customer_gid='" + httpRequest.Form["customer_gid"].ToString() + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<UploadDocumentModel>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentModel
                            {
                                customer_documentgid = (dr_datarow["customer_documentgid"].ToString()),
                                document_name = (dr_datarow["document_name"].ToString()),
                                document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(dr_datarow["document_path"].ToString())),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString())
                            });
                        }
                        objfilename.filename_list = get_filename;
                    }
                    dt_datatable.Dispose();

                }
            }
            catch (Exception ex)
            {
                objfilename.status = false;
            }
            return true;
        }

        public bool DaPostComposeMail(composemail values, string employee_gid)
        {
            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows)
            {
                ls_server = objodbcdatareader["pop_server"].ToString();
                ls_port = Convert.ToInt32(objodbcdatareader["pop_port"]);
                ls_username = objodbcdatareader["pop_username"].ToString();
                ls_password = objodbcdatareader["pop_password"].ToString();
            }
            objodbcdatareader.Close();
            //MailFlag = 1;
            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(ls_username);

            if (values.to_mail != null & values.to_mail != string.Empty)
            {
                if (values.to_mail.Length > 0)
                {
                    string toEmail = values.to_mail;

                    string[] ToEmail = toEmail.Split(',');

                    foreach (string TO_Email in ToEmail)
                    {
                        objMailMessage.To.Add(new MailAddress(TO_Email));
                    }
                }
            }

            if (values.cc_mail != null & values.cc_mail != string.Empty)
            {
                if (values.cc_mail.Length > 0)
                {
                    string ccEmail = values.cc_mail;

                    string[] CCEmail = ccEmail.Split(',');

                    foreach (string CC_Email in CCEmail)
                    {
                        objMailMessage.CC.Add(new MailAddress(CC_Email));
                    }
                }
            }

            if (values.bcc_mail != null & values.bcc_mail != string.Empty)
            {
                if (values.bcc_mail.Length > 0)
                {
                    string bcc_mail = values.bcc_mail;

                    string[] BccEmail = bcc_mail.Split(',');

                    foreach (string BCC_Email in BccEmail)
                    {
                        objMailMessage.Bcc.Add(new MailAddress(BCC_Email));
                    }
                }
            }


            objMailMessage.Subject = values.subject_mail;
            objMailMessage.Body = values.content_mail;
            objMailMessage.IsBodyHtml = true;
            objMailMessage.Priority = MailPriority.Normal;
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = ls_server;
            objSmtpClient.Port = ls_port;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = true;
            objSmtpClient.Credentials = new NetworkCredential(ls_username, ls_password);
            try
            {
                objSmtpClient.Send(objMailMessage);
            }
            catch
            {
                values.message = "Error Occured While Mail Sending..!";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("CUMA");

            msSQL = " Insert into lgl_trn_tcustomercomposemail(" +
                    " composemail_gid," +
                    " customer_gid," +
                    " from_mail," +
                    " to_mail," +
                    " cc_mail," +
                    " bcc_mail," +
                    " mail_subject," +
                    " mail_content," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'" + values.customer_gid + "'," +
                    "'" + ls_username + "'," +
                    "'" + values.to_mail + "'," +
                    "'" + values.cc_mail + "'," +
                    "'" + values.bcc_mail + "'," +
                    "'" + values.subject_mail + "'," +
                    "'" + values.content_mail + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.message = "Mail Sent Successfully..!";
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

        public bool DaGetDemandNotice(demandnotice values,string customer_gid)
        {
            msSQL = " select count(urn),max(od_days) as max_od_days, date_format(disbursement_date, '%d-%m-%Y') as disbursement_date,disbursement_amount, " +
                    " date_format(maturity_date, '%d-%m-%Y') as maturity_date,od_days,ac_status,ac_closed_date,last_payment,payment,urn, " +
                    " tenure,frequency,date_format(schedulde_payment, '%d-%m-%Y') as schedulde_payment,AccountName,ProductType,ProductCode, " +
                    " date_format(nextdemandrundat, '%d-%m-%Y') as nextdemandrundat,date_format(lastdemandrundate, '%d-%m-%Y') as lastdemandrundate, " +
                    " Customer_name,Guarantor_Name,Customer_Type,Vertical,RO_Name,DN_status from lgl_trn_tmisdata where od_days between 16 and 30 " +
                    " and urn not in (select urn from lgl_trn_tmisdata where od_days between 31 and 120 group by urn) " +
                    " group by urn order by count(urn)desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_demandNotice1 = new List<demandnotice1>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_demandNotice1.Add(new demandnotice1
                    {
                        urn_number = (dr_datarow["urn"].ToString()),
                        OD_days = (dr_datarow["max_od_days"].ToString()),
                        DN_status = (dr_datarow["DN_status"].ToString())
                    });
                }
                values.demandnotice1 = get_demandNotice1;
            }
            dt_datatable.Dispose();

            msSQL = " select count(urn),max(od_days) as max_od_days, date_format(disbursement_date, '%d-%m-%Y') as disbursement_date,disbursement_amount, " +
                   " date_format(maturity_date, '%d-%m-%Y') as maturity_date,od_days,ac_status,ac_closed_date,last_payment,payment,urn, " +
                   " tenure,frequency,date_format(schedulde_payment, '%d-%m-%Y') as schedulde_payment,AccountName,ProductType,ProductCode, " +
                   " date_format(nextdemandrundat, '%d-%m-%Y') as nextdemandrundat,date_format(lastdemandrundate, '%d-%m-%Y') as lastdemandrundate, " +
                   " Customer_name,Guarantor_Name,Customer_Type,Vertical,RO_Name,DN_status from lgl_trn_tmisdata where od_days between 31 and 45 " +
                   " and urn not in (select urn from lgl_trn_tmisdata where od_days between 31 and 120 group by urn) " +
                   " group by urn order by count(urn)desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_demandNotice2 = new List<demandnotice2>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_demandNotice2.Add(new demandnotice2
                    {
                        urn_number = (dr_datarow["urn"].ToString()),
                        OD_days = (dr_datarow["max_od_days"].ToString()),
                        DN_status = (dr_datarow["DN_status"].ToString())
                    });
                }
                values.demandnotice2 = get_demandNotice2;
            }
            dt_datatable.Dispose();

            msSQL = " select count(urn),max(od_days) as max_od_days, date_format(disbursement_date, '%d-%m-%Y') as disbursement_date,disbursement_amount, " +
                   " date_format(maturity_date, '%d-%m-%Y') as maturity_date,od_days,ac_status,ac_closed_date,last_payment,payment,urn, " +
                   " tenure,frequency,date_format(schedulde_payment, '%d-%m-%Y') as schedulde_payment,AccountName,ProductType,ProductCode, " +
                   " date_format(nextdemandrundat, '%d-%m-%Y') as nextdemandrundat,date_format(lastdemandrundate, '%d-%m-%Y') as lastdemandrundate, " +
                   " Customer_name,Guarantor_Name,Customer_Type,Vertical,RO_Name,DN_status from lgl_trn_tmisdata where od_days between 46 and 60 " +
                   " and urn not in (select urn from lgl_trn_tmisdata where od_days between 31 and 120 group by urn) " +
                   " group by urn order by count(urn)desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_demandNotice3 = new List<demandnotice3>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_demandNotice3.Add(new demandnotice3
                    {
                        urn_number = (dr_datarow["urn"].ToString()),
                        OD_days = (dr_datarow["max_od_days"].ToString()),
                        DN_status = (dr_datarow["DN_status"].ToString())
                    });
                }
                values.demandnotice3 = get_demandNotice3;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaGetLawyerPayment(invoicedtlList values)
        {
            msSQL = " select a.lawyerinvoice_gid,a.invoice_refno,date_format(a.invoice_date, '%d-%m-%Y') as invoice_date,a.invoice_amount,a.invoice_status, " +
                    " a.invoice_remarks,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,a.invoice_status,a.case_type,a.caseref_no,a.servicerender_date, " +
                    " a.service_type,concat(b.lawyeruser_name, ' / ', b.lawyeruser_code) as lawyername from lgl_trn_tlawyerinvoice a " +
                    " left join lgl_mst_tlawyeruser b on a.created_by = b.lawyeruser_gid order by lawyerinvoice_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinvoicelistdtl = new List<invoicedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinvoicelistdtl.Add(new invoicedtl
                    {
                        lawyerinvoice_gid = (dr_datarow["lawyerinvoice_gid"].ToString()),
                        invoice_refno = (dr_datarow["invoice_refno"].ToString()),
                        invoice_date = (dr_datarow["invoice_date"].ToString()),
                        invoice_amount = (dr_datarow["invoice_amount"].ToString()),
                        invoice_remarks = (dr_datarow["invoice_remarks"].ToString()),
                        created_by = (dr_datarow["lawyername"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                      invoice_status= (dr_datarow["invoice_status"].ToString()),
                    });
                }
                values.invoicedtl = getinvoicelistdtl;
            }
            dt_datatable.Dispose();
            return true;
        }

        public bool DaPostAssignCompliance(assignlawyer values, string employee_gid)
        {
            msSQL = " update lgl_trn_traiselegalSR set SRassigned_lawyer='" + values.lawyeruser_gid + "'," +
                    " SLN_remarks='" + values.SLN_remarks + "'," +
                    " assigned_by ='" + employee_gid + "'," +
                    " assigned_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where raiselegalSR_gid='" + values.legalSR_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select * from lgl_tmp_tSLNdocument where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("SLDO");

                    msSQL = " Insert into lgl_trn_tSLNdocument( " +
                              " SLNdocument_gid," +
                              " legal_SRgid," +
                              " document_name," +
                              " document_path," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetDocumentGid + "', " +
                              "'" + values.legalSR_gid + "'," +
                              "'" + dt["document_name"].ToString() + "'," +
                              "'" + dt["document_path"].ToString() + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnresult = objdbconn.ExecuteNonQuerySQL (msSQL);

                    if (mnresult == 1)
                    {
                        msSQL = "delete from lgl_tmp_tSLNdocument where tmpSLN_documentgid ='" + dt["tmpSLN_documentgid"].ToString() + "'";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            dt_datatable.Dispose();

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "SLN Assigned Successfully..!";
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }
    }
}