using ems.lp.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;
using ems.storage.Functions;

namespace ems.lp.DataAccess
{
    public class DaLawyerInvoice
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objodbcdatareader;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetGidINV;
        int mnresult;
        string lscompany_code, path, lspath;

        public void DaPostInvoiceDetails(string user_gid, invoicedtl values)
        {
            msSQL = "select caseref_gid from lgl_trn_tlawyerinvoice where caseref_gid='" + values.caseref_gid + "'";
            string lscaseref_gid = objdbconn.GetExecuteScalar(msSQL);
            if(lscaseref_gid=="")
            {
                msGetGidINV = objcmnfunctions.GetMasterGID("LAIV");
                msSQL ="insert into lgl_trn_tlawyerinvoice("+
                    " lawyerinvoice_gid,"+
                    " caseref_gid,"+
                    " case_type,"+
                    " caseref_no,"+
                    " created_by,"+
                    " created_date)"+
                    " values("+
                    "'" + msGetGidINV + "', " +
                     "'" + values.caseref_gid + "'," +
                    "'" + values.case_type + "'," +
                    "'" + values.caseref_no + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            msGetGid = objcmnfunctions.GetMasterGID("LIDL");

            msSQL = " Insert into lgl_trn_tlawyerinvoicedtl( " +
                          " lawyerinvoicedtl_gid," +
                          " lawyerinvoice_gid," +
                          " invoice_refno," +
                          " invoice_date," +
                          " invoice_amount," +
                          " invoice_remarks," +
                          " case_type," +
                          " caseref_no," +
                          " servicerender_date," +
                          " service_type," +
                          " caseref_gid," +
                          " servicetype_gid," +
                          " serviceypeothers_title," +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msGetGid + "', " +
                          "'" + msGetGidINV + "', " +
                          "'" + values.invoice_refno + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          "'" + values.invoice_amount.Replace("'", "\\'") + "'," +
                          "'" + values.invoice_remarks.Replace("'", "\\'") + "'," +
                          "'" + values.case_type + "'," +
                          "'" + values.caseref_no + "'," +
                          "'" + Convert.ToDateTime(values.servicerender_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          "'" + values.service_type + "'," +
                          "'" + values.caseref_gid + "'," +
                             "'" + values.servicetype_gid + "',";
            if(values.service_type=="Others")
            {
                msSQL += "'" + values.serviceypeothers_title.Replace("'", "\\'") + "',";
            }
            else
            {
                msSQL += "'',";
            }
                       msSQL+=   "'" + user_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
               mnresult = objdbconn .ExecuteNonQuerySQL(msSQL);

         
               msSQL = "update  lgl_trn_tinvoicedocument set  lawyerinvoicedtl_gid ='" + msGetGid + "' where lawyerinvoicedtl_gid='" + user_gid+"' ";
               mnresult = objdbconn .ExecuteNonQuerySQL(msSQL);
                    
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Invoice Created Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaTmpDocumentDelete(string user_gid)
        {
            document values = new document();
            msSQL = "delete from lgl_trn_tinvoicedocument where lawyerinvoicedtl_gid ='" + user_gid + "'";
            mnresult = objdbconn .ExecuteNonQuerySQL(msSQL);
          
            if (mnresult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = true;
            }
        }
        public void DaGetInvoicedtlList(string user_gid, invoicedtlList values)
        {           
          msSQL = " select lawyerinvoicedtl_gid,invoice_refno,date_format(invoice_date,'%d-%m-%Y') as invoice_date,invoice_amount, " +
                    " invoice_remarks,date_format(created_date, '%d-%m-%Y %h:%i %p') as created_date,invoice_status,concat(case_type,' - ',caseref_no) as case_type " +
                    " from lgl_trn_tlawyerinvoicedtl where created_by='" + user_gid + "' order by lawyerinvoicedtl_gid desc";
            dt_datatable = objdbconn .GetDataTable (msSQL);
            var get_invoicedtl = new List<invoicedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_invoicedtl.Add(new invoicedtl
                    {
                        lawyerinvoicedtl_gid = dt["lawyerinvoicedtl_gid"].ToString(),
                        invoice_refno = dt["invoice_refno"].ToString(),
                        invoice_date = dt["invoice_date"].ToString(),
                        invoice_amount = dt["invoice_amount"].ToString(),
                        invoice_remarks = dt["invoice_remarks"].ToString(),
                        invoice_status = dt["invoice_status"].ToString(),
                        case_type = dt["case_type"].ToString()
                    });
                }
                values.invoicedtl = get_invoicedtl;
            }
            dt_datatable.Dispose();        
            if (mnresult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }
        public void DaGetInvoicedtl(string lawyerinvoicedtl_gid, invoicedtl values)
         {
           
       msSQL = " select lawyerinvoicedtl_gid,invoice_refno,date_format(invoice_date,'%d-%m-%Y') as invoice_date,invoice_amount,servicetype_gid,caseref_gid, " +
                    " invoice_remarks,date_format(created_date, '%d-%m-%Y %h:%i %p') as created_date,invoice_status,case_type,caseref_no,service_type,serviceypeothers_title, " +
                    " servicerender_date from lgl_trn_tlawyerinvoicedtl where lawyerinvoicedtl_gid='" + lawyerinvoicedtl_gid + "'";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                values.invoice_refno = objodbcdatareader["invoice_refno"].ToString();
              //  values.invoice_date = objodbcdatareader["invoice_date"].ToString();
                values.invoice_amount = objodbcdatareader["invoice_amount"].ToString();
                values.invoice_remarks = objodbcdatareader["invoice_remarks"].ToString();
                values.created_date = objodbcdatareader["created_date"].ToString();
                values.invoice_status = objodbcdatareader["invoice_status"].ToString();
                values.case_type = objodbcdatareader["case_type"].ToString();
                values.caseref_no = objodbcdatareader["caseref_no"].ToString();
              //  values.servicerender_date = objodbcdatareader["servicerender_date"].ToString();
                values.service_type = objodbcdatareader["service_type"].ToString();
                values.servicetype_gid = objodbcdatareader["servicetype_gid"].ToString();
                values.caseref_gid = objodbcdatareader["caseref_gid"].ToString();
                values.serviceypeothers_title= objodbcdatareader["serviceypeothers_title"].ToString();
                if (objodbcdatareader["servicerender_date"].ToString() == "")
                {
                }
                else
                {
                    values.servicerender_date = Convert.ToDateTime(objodbcdatareader["servicerender_date"]).ToString("MM-dd-yyyy");
                }
                if (objodbcdatareader["invoice_date"].ToString() == "")
                {
                }
                else
                {
                    values.invoice_date = objodbcdatareader["invoice_date"].ToString(); ;
                }
            }
            objodbcdatareader.Close();

            msSQL = " select concat(document_name,' ' ,date_format(created_date,'%d-%m-%Y %h:%i %p')) as document_name,document_path,invoice_documentgid " +
                    " from lgl_trn_tinvoicedocument where lawyerinvoicedtl_gid='" + lawyerinvoicedtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable (msSQL);
            var get_documentdtl = new List<UploadDocumentModel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_documentdtl.Add(new UploadDocumentModel
                    {
                        invoice_documentgid = dt["invoice_documentgid"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = (dt["document_path"].ToString()),
                    });
                }
                values.uploaddocument = get_documentdtl;
            }
            dt_datatable.Dispose();

            if (mnresult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = true;
            }
        }
        public void DaGetCancelDocument(string invoice_documentgid, UploadDocument_name values,string user_gid)
        {
           

            msSQL = "delete from lgl_trn_tinvoicedocument where invoice_documentgid ='" + invoice_documentgid + "'";
            mnresult = objdbconn .ExecuteNonQuerySQL(msSQL);

            msSQL = " select a.invoice_documentgid,concat(a.document_name,' ' ,date_format(a.created_date,'%d-%m-%Y %h:%i %p')) as document_name,a.document_path " +
                    " from lgl_trn_tinvoicedocument a " +
                    " where a.lawyerinvoicedtl_gid='" + user_gid + "'";
            dt_datatable = objdbconn .GetDataTable (msSQL);
            var get_filename = new List<UploadDocumentModel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentModel
                    {
                        tmpinvoice_documentgid = (dr_datarow["invoice_documentgid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = (dr_datarow["document_path"].ToString()),
                    });
                }
                values.filename_list = get_filename;
            }
            dt_datatable.Dispose();

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Document Cancelled Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }
        public void DaGetDocumentUpload(HttpRequest httpRequest, string user_gid, UploadDocument_name objfilename)
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
            string project_flag = "Default";


            msSQL = "SELECT company_code from adm_mst_tcompany";
            objodbcdatareader = objdbconn .GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                objodbcdatareader.Read();
                lscompany_code = objodbcdatareader["company_code"].ToString();
            }
            objodbcdatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/InvoiceDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID ("LINV");
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
                       
                      
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);
                            //CopyStream(ms, ls_readStream);
                            lspath = path;
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/InvoiceDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/InvoiceDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                         
                       
                    msSQL = " Insert into lgl_trn_tinvoicedocument( " +
                              " invoice_documentgid," +
                              " lawyerinvoicedtl_gid," +
                              " document_name," +
                              " document_path," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msdocument_gid + "', " +
                              "'" + user_gid + "'," +
                              "'" + httpPostedFile.FileName + "'," +
                              "'" + lspath + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnresult = objdbconn .ExecuteNonQuerySQL(msSQL);
                            if (mnresult == 1)
                            {
                                objfilename.status = true;
                                objfilename.message = "Document Uploaded Successfully..!";
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "Error Occured..!";

                            }
                        }

                    msSQL = " select a.invoice_documentgid,concat(a.document_name,' ',date_format(a.created_date,'%d-%m-%Y %h:%i %p')) as document_name,a.document_path " +
                            " from lgl_trn_tinvoicedocument a " +
                            " where a.lawyerinvoicedtl_gid='" + user_gid + "'";
                    dt_datatable = objdbconn .GetDataTable (msSQL);
                    var get_filename = new List<UploadDocumentModel>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentModel
                            {
                                tmpinvoice_documentgid = (dr_datarow["invoice_documentgid"].ToString()),
                                document_name = (dr_datarow["document_name"].ToString()),
                                document_path = (dr_datarow["document_path"].ToString()),
                            });
                        }
                        objfilename.filename_list = get_filename;
                    }
                    dt_datatable.Dispose();

                }
            }
            catch (Exception ex)
            {
                dt_datatable.Dispose();
               
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
            }
           
           
        }
        public void DaEditUploadDocument(HttpRequest httpRequest, string user_gid, UploadDocument_name objfilename)
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
            string lawyerinvoicedtl_gid = httpRequest.Form["lawyerinvoicedtl_gid"].ToString();
            string project_flag = "Default";

            msSQL = "SELECT company_code from adm_mst_tcompany";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                objodbcdatareader.Read();
                lscompany_code = objodbcdatareader["company_code"].ToString();
            }
            objodbcdatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/InvoiceDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("LINV");
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


                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //CopyStream(ms, ls_readStream);
                        lspath = path;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/InvoiceDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/InvoiceDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
  
                        msSQL = " Insert into lgl_trn_tinvoicedocument( " +
                                  " invoice_documentgid," +
                                  " lawyerinvoicedtl_gid," +
                                  " document_name," +
                                  " document_path," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msdocument_gid + "', " +
                                  "'" + user_gid + "'," +
                                  "'" + httpPostedFile.FileName + "'," +
                                  "'" + lspath + "'," +
                                  "'" + user_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnresult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";

                        }
                    }

                    msSQL = " select a.invoice_documentgid,concat(a.document_name,' ',date_format(a.created_date,'%d-%m-%Y %h:%i %p')) as document_name,a.document_path " +
                            " from lgl_trn_tinvoicedocument a " +
                            " where ( a.lawyerinvoicedtl_gid='" + user_gid + "' or a.lawyerinvoicedtl_gid='" + lawyerinvoicedtl_gid + "')";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<UploadDocumentModel>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentModel
                            {
                                tmpinvoice_documentgid = (dr_datarow["invoice_documentgid"].ToString()),
                                document_name = (dr_datarow["document_name"].ToString()),
                                document_path = (dr_datarow["document_path"].ToString()),
                            });
                        }
                        objfilename.filename_list = get_filename;
                    }
                    dt_datatable.Dispose();

                }
            }
            catch (Exception ex)
            {
                dt_datatable.Dispose();

                objfilename.status = false;
                objfilename.message = "Error Occured..!";
            }


        }
        public void DaGetlegalsr(string user_gid, Mslcasedtl values)
        {
            msSQL = "select account_name,raiselegalSR_gid from lgl_trn_traiselegalSR where  SRassigned_lawyer='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcases_list = new List<cases_list>();
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getcases_list.Add(new cases_list
                    {
                        caseref_no = (dr_datarow["account_name"].ToString()),
                        caseref_gid = (dr_datarow["raiselegalSR_gid"].ToString())

                    });
                }
            values.cases_list = getcases_list;

            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetlegalservices(string case_type,string user_gid, Mslcasedtl values)
        {
            if(case_type == "Legal Services")
            {
                msSQL = " select concat(requestref_no,'/',request_type,' ',others_title) as requestref_no,a.requestcompliance_gid from  lgl_trn_trequestcompliance2lawyerdtl a" +
                                   " left join lgl_mst_trequestcompliance b on a.requestcompliance_gid = b.requestcompliance_gid" +
                                   " left join lgl_mst_tlawyeruser c on a.lawyerregister_gid = c.lawyerregister_gid where lawyeruser_gid ='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcases_list = new List<cases_list>();
                if (dt_datatable.Rows.Count != 0)
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        getcases_list.Add(new cases_list
                        {
                            caseref_no = (dr_datarow["requestref_no"].ToString()),
                            caseref_gid = (dr_datarow["requestcompliance_gid"].ToString())

                        });
                    }
                values.cases_list = getcases_list;

                dt_datatable.Dispose();
            }
            else if(case_type == "Legal SR")
            {
                msSQL = "select account_name,raiselegalSR_gid from lgl_trn_traiselegalSR where  SRassigned_lawyer='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcases_list = new List<cases_list>();
                if (dt_datatable.Rows.Count != 0)
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        getcases_list.Add(new cases_list
                        {
                            caseref_no = (dr_datarow["account_name"].ToString()),
                            caseref_gid = (dr_datarow["raiselegalSR_gid"].ToString())

                        });
                    }
                values.cases_list = getcases_list;

                dt_datatable.Dispose();
            }
           
            values.status = true;
        }
        public void Daupdateinvoicedetails(string user_gid, invoicedtl values)
        {
            msSQL = "select caseref_gid from lgl_trn_tlawyerinvoice where caseref_gid='" + values.caseref_gid + "'";
            string lscaseref_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lscaseref_gid == "")
            {
                msGetGidINV = objcmnfunctions.GetMasterGID("LAIV");
                msSQL = "insert into lgl_trn_tlawyerinvoice(" +
                    " lawyerinvoice_gid," +
                    " caseref_gid," +
                    " case_type," +
                    " caseref_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGidINV + "', " +
                     "'" + values.caseref_gid + "'," +
                    "'" + values.case_type + "'," +
                    "'" + values.caseref_no + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            msSQL = "select  concat(requestref_no ,' - ',request_type) as requestref_no from lgl_mst_trequestcompliance where requestcompliance_gid = '" + values.caseref_gid + "'";
            string lscaseref_no = objdbconn.GetExecuteScalar(msSQL);
            if(lscaseref_no=="")
            {
                msSQL = "select account_name from lgl_trn_traiselegalSR where raiselegalSR_gid = '" + values.caseref_gid + "'";
                 lscaseref_no = objdbconn.GetExecuteScalar(msSQL);
            }

            msSQL = "select service_type from lgl_mst_tservicetype where servicetype_gid = '" + values.servicetype_gid + "'";
            string lsservice_type = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update lgl_trn_tlawyerinvoicedtl set invoice_refno='" + values.invoice_refno + "'," +
                " invoice_amount='" + values.invoice_amount + "'," +
                " invoice_refno='" + values.invoice_refno + "'," +
                " invoice_remarks='" + values.invoice_remarks.Replace("'", "") + "'," +
                " servicetype_gid='" + values.servicetype_gid + "'," +
                " service_type='" + lsservice_type + "'," +
               " caseref_gid='" + values.caseref_gid + "'," +
               " caseref_no='" + lscaseref_no + "'," +
                " case_type='" + values.case_type + "',";
            if (lsservice_type == "Others")
            {
                msSQL += "serviceypeothers_title='" + values.serviceypeothers_title.Replace("'", "\\'") + "'";
            }
            else
            {
                msSQL += "serviceypeothers_title=''";
            }
           msSQL +=" where lawyerinvoicedtl_gid='" + values.lawyerinvoicedtl_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (Convert.ToDateTime(values.servicerenderdate).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL = "update lgl_trn_tlawyerinvoicedtl set servicerender_date='" + Convert.ToDateTime(values.servicerenderdate).AddDays(1).ToString("yyyy-MM-dd 00:00:00") + "'" +
                  " where lawyerinvoicedtl_gid='" + values.lawyerinvoicedtl_gid + "'";
                mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "update  lgl_trn_tinvoicedocument set  lawyerinvoicedtl_gid ='" + values.lawyerinvoicedtl_gid + "' where lawyerinvoicedtl_gid='" + user_gid + "' ";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Invoice updated Successfully..";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

        public void geteditdocument(string lawyerinvoicedtl_gid, string user_gid, UploadDocument_name objfilename)
        {
            msSQL = " select a.invoice_documentgid,a.document_name,a.document_path " +
                           " from lgl_trn_tinvoicedocument a " +
                           " where ( a.lawyerinvoicedtl_gid='" + user_gid + "' or a.lawyerinvoicedtl_gid='" + lawyerinvoicedtl_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentModel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentModel
                    {
                        tmpinvoice_documentgid = (dr_datarow["invoice_documentgid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = (dr_datarow["document_path"].ToString()),
                    });
                }
                objfilename.filename_list = get_filename;
            }
            dt_datatable.Dispose();
        }

        public void Daupdateinvoicestatus(string user_gid, invoicedtl values)
        {

            

            msSQL = "update lgl_trn_tlawyerinvoicedtl set invoice_status='" + values.invoice_status + "'" +    
               " where lawyerinvoicedtl_gid='" + values.lawyerinvoicedtl_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Invoice Status Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }
        public void GetCaseType(string user_gid, Mslcasedtl values)
        {
            msSQL = " select case_type,casetype_gid from lgl_trn_tcasetype order by casetype_gid asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcases_list = new List<casestype_list>();
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getcases_list.Add(new casestype_list
                    {
                        casetype_gid = (dr_datarow["casetype_gid"].ToString()),
                        case_type = (dr_datarow["case_type"].ToString())

                    });
                }
            values.casestype_list = getcases_list;

            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGeteditlegalservices( string user_gid, Mslcasedtl values)
        {
             msSQL = " select concat(requestref_no,'/',request_type,' ',others_title) as requestref_no,a.requestcompliance_gid from  lgl_trn_trequestcompliance2lawyerdtl a" +
                                   " left join lgl_mst_trequestcompliance b on a.requestcompliance_gid = b.requestcompliance_gid" +
                                   " left join lgl_mst_tlawyeruser c on a.lawyerregister_gid = c.lawyerregister_gid where lawyeruser_gid ='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcases_list = new List<cases_list>();
                if (dt_datatable.Rows.Count != 0)
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        getcases_list.Add(new cases_list
                        {
                            caseref_no = (dr_datarow["requestref_no"].ToString()),
                            caseref_gid = (dr_datarow["requestcompliance_gid"].ToString())

                        });
                    }
                values.cases_list = getcases_list;

                dt_datatable.Dispose();
         

            values.status = true;
        }
        public void DaGetPaymentSummary(string invoicedtl, invoicedtlList values)
        {
            msSQL = "select a.case_type,a.caseref_no,a.caseref_gid,(select count(lawyerinvoicedtl_gid) from lgl_trn_tlawyerinvoicedtl b "+
                    " where a.lawyerinvoice_gid = b.lawyerinvoice_gid  group by b.lawyerinvoice_gid) as invoice_count from lgl_trn_tlawyerinvoice a  ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinvoicedtl = new List<invoicedtl>();
            if (dt_datatable.Rows.Count != 0)
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getinvoicedtl.Add(new invoicedtl
                    {
                        caseref_no = (dr_datarow["caseref_no"].ToString()),
                        case_type = (dr_datarow["case_type"].ToString()),
                        caseref_gid=dr_datarow["caseref_gid"].ToString(),
                        invoice_count= dr_datarow["invoice_count"].ToString()
                    });
                }
            values.invoicedtl = getinvoicedtl;

            dt_datatable.Dispose();


            values.status = true;
        }
        public void DaGetinvoiceListSummary(string caseref_gid, invoicedtlList values)
        {
            msSQL = " select caseref_no,case_type,a.lawyerinvoicedtl_gid,a.invoice_refno,date_format(a.invoice_date, '%d-%m-%Y') as invoice_date,a.invoice_amount,a.invoice_status, " +
                    " a.invoice_remarks,date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date,a.invoice_status,a.case_type,a.caseref_no,a.servicerender_date, " +
                    " a.service_type,concat(b.lawyeruser_name, ' / ', b.lawyeruser_code) as lawyername from lgl_trn_tlawyerinvoicedtl a " +
                    " left join lgl_mst_tlawyeruser b on a.created_by = b.lawyeruser_gid where a.caseref_gid='" + caseref_gid+ "' order by lawyerinvoicedtl_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinvoicelistdtl = new List<invoicedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinvoicelistdtl.Add(new invoicedtl
                    {
                        lawyerinvoicedtl_gid = (dr_datarow["lawyerinvoicedtl_gid"].ToString()),
                        invoice_refno = (dr_datarow["invoice_refno"].ToString()),
                        invoice_date = (dr_datarow["invoice_date"].ToString()),
                        invoice_amount = (dr_datarow["invoice_amount"].ToString()),
                        invoice_remarks = (dr_datarow["invoice_remarks"].ToString()),
                        created_by = (dr_datarow["lawyername"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        invoice_status = (dr_datarow["invoice_status"].ToString()),
                        caseref_no = (dr_datarow["caseref_no"].ToString()),
                        case_type = (dr_datarow["case_type"].ToString()),
                    });
                }
                values.invoicedtl = getinvoicelistdtl;
            }
            dt_datatable.Dispose();

            msSQL = "select caseref_no,case_type from lgl_trn_tlawyerinvoicedtl group by lawyerinvoice_gid";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
                if(objodbcdatareader.HasRows==true)
            {
                values.caseref_no = objodbcdatareader["caseref_no"].ToString();
                values.case_type = objodbcdatareader["case_type"].ToString();
            }
            objodbcdatareader.Close();
            values.status = true;
        }
    }
}