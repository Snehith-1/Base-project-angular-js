using System.Web;
using ems.utilities.Functions;
using ems.lp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Configuration;
using ems.storage.Functions;

namespace ems.lp.DataAccess
{
    public class DaLawyerLegalSR
    {
        
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objodbcdatareader;
        DataTable dt_datatable;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetDocumentGid;
        int mnresult;
        string lscompany_code, path, lspath;


        public void DaGetRaiseLegalSR(assignlegalSRList values,string user_gid)
        {
            try
            {
                msSQL = " select a.srref_no,a.account_name,a.constitution,a.financed_by,a.raiselegalSR_gid,a.customer_gid, " +
                   " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as assign_by, " +
                   " date_format(a.assigned_date, '%d-%m-%Y') as assign_date " +
                   " from lgl_trn_traiselegalSR a,  hrm_mst_temployee b, adm_mst_tuser c where " +
                   " a.assigned_by = b.employee_gid and b.user_gid = c.user_gid and SRassigned_lawyer ='" + user_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlegalSR = new List<assignlegalSR>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlegalSR.Add(new assignlegalSR
                        {
                            srref_no = dr_datarow["srref_no"].ToString(),
                            customer_name = (dr_datarow["account_name"].ToString()),
                            constitution = (dr_datarow["constitution"].ToString()),
                            financed_by = (dr_datarow["financed_by"].ToString()),
                            assign_by = (dr_datarow["assign_by"].ToString()),
                            assign_date = (dr_datarow["assign_date"].ToString()),
                            legalSR_gid = (dr_datarow["raiselegalSR_gid"].ToString()),
                            customer_gid = (dr_datarow["customer_gid"].ToString())
                        });
                    }
                    values.assignlegalSR = getlegalSR;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }
           
           
        }

        public void Uploadcorrecteddocument(HttpRequest httpRequest, UploadDocument_namesr objfilename, string user_gid)
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
            string raiselegalSR_gid = httpRequest.Form["raiselegalSR_gid"].ToString();


            msSQL = "SELECT company_code from adm_mst_tcompany where 1=1";
            objodbcdatareader = objdbconn.GetDataReader(msSQL);
            if (objodbcdatareader.HasRows == true)
            {
                objodbcdatareader.Read();
                lscompany_code = objodbcdatareader["company_code"].ToString();
            }
            objodbcdatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        string lsfile_gid = msdocument_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();

                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".pdf") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".txt") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".xls") || (FileExtension == ".xlsx"))
                        {
                            if(objcmnstorage.CheckIsExecutable(bytes) != true)
                            {
                                ls_readStream = httpPostedFile.InputStream;
                                ls_readStream.CopyTo(ms);
                                //CopyStream(ms, ls_readStream);
                                lspath = path;
                                bool status;
                                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                                ms.Close();
                                lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                                msGetGid = objcmnfunctions.GetMasterGID("LYSR");

                                msSQL = " insert into lgl_tmp_tlegalsrdocument( " +
                                         "tmplegalsr_documentgid," +
                                         " raiselegalSR_gid," +
                                         " document_name," +
                                         " document_path," +
                                         " created_by," +
                                         " created_date " +
                                         " )values(" +
                                         "'" + msGetGid + "'," +
                                         "'" + raiselegalSR_gid + "'," +
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
                            else
                            {
                                objfilename.message = "File Format not supported!";
                                break;
                            }
                        }
                        else
                        {
                            objfilename.message = "File Format not supported!";
                            break;
                        }
                    }
                    msSQL = " select a.tmplegalsr_documentgid,a.document_name,a.document_path " +
                             " from lgl_tmp_tlegalsrdocument a " +
                             " where a.created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<UploadDocumentModelsr>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentModelsr
                            {
                                tmplegalsr_documentgid = (dr_datarow["tmplegalsr_documentgid"].ToString()),
                                document_name = (dr_datarow["document_name"].ToString()),
                                document_path = HttpContext.Current.Server.MapPath(dr_datarow["document_path"].ToString()),
                            });
                        }
                        objfilename.filenamesr_list = get_filename;
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

        public void DaDocumentlist(string user_gid)
        {
            document values = new document();
            msSQL = "delete from lgl_tmp_tlegalsrdocument where created_by ='" + user_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = true;
            }
        }

        public void Dadocumentcancel(string tmplegalsr_documentgid, deleteDocument objdocumentcancel)
        {
            
            msSQL = " delete from lgl_tmp_tlegalsrdocument where tmplegalsr_documentgid= '" + tmplegalsr_documentgid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                objdocumentcancel.status = true;
            }
            else
            {
                objdocumentcancel.status = true;
            }
        }
    }
}