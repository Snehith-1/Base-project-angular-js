using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using ems.storage.Functions;

using System.Configuration;



namespace ems.idas.DataAccess
{
    public class DaIdasTrnDocumentUpload
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        string msGetGID;
        OdbcDataReader objODBCDataReader;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        HttpPostedFile httpPostedFile;
        List<FolderDtls> FolderDtlsList = new List<FolderDtls>();
        int count = 0;

        public void DaPostFileUpload1(HttpRequest httpRequest, MdlIdasDocumentUpload objfilename, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string parent_directorygid = httpRequest.Form["parent_directorygid"];
            string directory_type = httpRequest.Form["directory_type"];
            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

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
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");

                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("UPLO");
                        msSQL = " insert into ids_trn_tfileupload( " +
                                    " fileupload_gid ," +
                                    " parent_directorygid," +
                                    " document_name ," +
                                    " document_path," +
                                    " directory_type," +
                                    " created_by" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + parent_directorygid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + directory_type + "'," +
                                    "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
                else
                {
                    objfilename.status = false;
                    objfilename.message = "Error Occured";

                }


            }
            catch (Exception ex)
            {
                objfilename.status = false;
                objfilename.message = ex.Message;
            }
        }

        public void DaPostFileUpload(HttpRequest httpRequest, MdlIdasDocumentUpload objfilename, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string parent_directorygid = httpRequest.Form["parent_directorygid"];
            string directory_type = httpRequest.Form["directory_type"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/FileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("UPLO");
                        msSQL = " insert into ids_trn_tfileupload( " +
                                    " fileupload_gid ," +
                                    " parent_directorygid," +
                                    " document_name ," +
                                    " document_path," +
                                    " directory_type," +
                                    " created_by" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + parent_directorygid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + directory_type + "'," +
                                    "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
        }

        public result DaPostCreateFolder(MdlIdasDocumentUpload values, string user_gid)
        {
            result objResult = new Models.result();
            msGetGID = objcmnfunctions.GetMasterGID("UPLO");

            msSQL = " INSERT INTO ids_trn_tfileupload(" +
                " fileupload_gid," +
                " parent_directorygid," +
                " folder_name," +
                " directory_type," +
                " created_by)" +
                " VALUES(" +
                "'" + msGetGID + "'," +
                "'" + values.parent_directorygid + "'," +
                "'" + values.folder_name.Replace("'", "") + "'," +
                "'" + values.directory_type + "'," +
                "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Folder Created Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public void DaGetFolderDtls(string parent_directorygid, DirectoryDtlsList values)
        {
            msSQL = " select a.fileupload_gid,a.folder_name,a.document_name,a.document_path," +
                    " directory_type,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " if(a.updated_by is null,concat(b.user_code,'/',b.user_firstname,b.user_lastname),concat(c.user_code,'/',c.user_firstname,c.user_lastname)  ) as created_by" +
                    " from ids_trn_tfileupload a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " left join adm_mst_tuser c on a.updated_by=c.user_gid" +
                    " where parent_directorygid='" + parent_directorygid + "' order by a.directory_type asc,a.created_date desc,a.updated_date desc,a.folder_name,document_name";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.DirectoryDtls = dt_datatable.AsEnumerable().Select(row => new DirectoryDtls
                {
                    fileupload_gid = row["fileupload_gid"].ToString(),
                    folder_name = row["folder_name"].ToString(),
                    created_date = row["created_date"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    directory_type = row["directory_type"].ToString(),
                    created_by = row["created_by"].ToString(),


                }).ToList();
                values.status = true;
                values.message = "Success";

            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }

        public result DaPostRenameFolder(FolderDtls values, string user_gid)
        {
            result objResult = new Models.result();
            msSQL = " select parent_directorygid from ids_trn_tfileupload  where fileupload_gid = '" + values.fileupload_gid + "'";
            var parent_directorygid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select fileupload_gid from ids_trn_tfileupload where parent_directorygid='" + parent_directorygid + "' and folder_name='" + values.folder_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                if (objODBCDataReader["fileupload_gid"].ToString() != values.fileupload_gid)
                {
                    objODBCDataReader.Close();
                    objResult.status = false;
                    objResult.message = " Folder Name Already Exits";
                    return objResult;

                }
                objODBCDataReader.Close();
            }

            msSQL = " update ids_trn_tfileupload set" +
                    " folder_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                    " where fileupload_gid='" + values.fileupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = " Folder Renamed Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public result DaPostRenameFile(FolderDtls values, string user_gid)
        {
            result objResult = new Models.result();

            msSQL = " select parent_directorygid from ids_trn_tfileupload  where fileupload_gid = '" + values.fileupload_gid + "'";
            var parent_directorygid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select fileupload_gid from ids_trn_tfileupload where parent_directorygid='" + parent_directorygid + "' and document_name='" + values.folder_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                if (objODBCDataReader["fileupload_gid"].ToString() != values.fileupload_gid)
                {
                    objODBCDataReader.Close();
                    objResult.status = false;
                    objResult.message = " File Name Already Exits";
                    return objResult;

                }
                objODBCDataReader.Close();
            }
            msSQL = " update ids_trn_tfileupload set" +
                    " document_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                    " where fileupload_gid='" + values.fileupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = " File Renamed Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public void DaGetBreadCrumb(string fileupload_gid, breadCrumbList values)
        {

            msSQL = " SELECT parent_directorygid,fileupload_gid,folder_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ids_trn_tfileupload" +
                    " where fileupload_gid='" + fileupload_gid + "' and directory_type='Folder'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                count = count + 1;
                if (objODBCDataReader["parent_directorygid"].ToString() == "$")
                {

                    FolderDtlsList.Add(new FolderDtls
                    {

                        fileupload_gid = objODBCDataReader["fileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    objODBCDataReader.Close();
                }
                else
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        fileupload_gid = objODBCDataReader["fileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    var gid = objODBCDataReader["parent_directorygid"].ToString();
                    objODBCDataReader.Close();
                    DaGetBreadCrumb(gid, values);
                }
                values.FolderDtls = FolderDtlsList;
            }
            objODBCDataReader.Close();

        }

        public result DaDelete(string fileupload_gid)
        {
            result objResult = new Models.result();

            msSQL = " select fileupload_gid from ids_trn_tfileupload where parent_directorygid='" + fileupload_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                objResult.status = false;
                objResult.message = "Folder Has File,You Could Not Delete";
                return objResult;
            }
            objODBCDataReader.Close();
            msSQL = " delete from ids_trn_tfileupload where fileupload_gid='" + fileupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Folder Deleted Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public bool DaGetsanction2customer(string customer_gid, Mdlsanction2customer values)
        {
            msSQL = " select sanction_refno,customer2sanction_gid, date_format(sanction_date,'%d-%m-%Y') as sanction_date, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " format((sanction_amount),2) as sanction_amount  " +
                    " from ocs_mst_tcustomer2sanction a" +
                     " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid" +
                   " LEFT JOIN hrm_mst_temployee c ON a.updated_by=c.employee_gid" +
                   " LEFT JOIN adm_mst_tuser d ON c.user_gid=d.user_gid" +
                    " where a.customer_gid='" + customer_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<sanction2customer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer.Add(new sanction2customer_list
                    {
                        sanctionref_no = (dr_datarow["sanction_refno"].ToString()),
                        customer2sanction_gid = (dr_datarow["customer2sanction_gid"].ToString()),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.sanction2customer_list = getcustomer;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetDocumentsofSanction(Mdlsanction2customer values, string customer2sanction_gid)
        {
            msSQL = " select momdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                " from ids_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                " and b.user_gid = c.user_gid and a.customer2sanction_gid ='" + customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMOM_filename = new List<MOM_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        document_gid = (dr_datarow["momdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.MOM_DocumentList = getMOM_filename;
            }
            dt_datatable.Dispose();

            msSQL = " select camdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
              " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
              " from ids_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
              " and b.user_gid = c.user_gid and a.customer2sanction_gid ='" + customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getCAM_filename = new List<COM_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        document_gid = (dr_datarow["camdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }

            }
            dt_datatable.Dispose();

            msSQL = " select sanctionletter_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tuploadsanctionletter a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and a.customer2sanction_gid ='" + customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var get_sanfilename = new List<MOM_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["sanctionletter_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                    });
                }
                //values.MOM_DocumentList = getMOM_filename;
            }
            dt_datatable.Dispose();

            msSQL = " select generaldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                   " from ids_trn_tuploadgeneraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                   " and b.user_gid = c.user_gid and a.customer2sanction_gid ='" + customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getfilename = new List<MOM_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["generaldocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                    });
                }

            }
            dt_datatable.Dispose();

            msSQL = " select esdeclaration_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                     " from ids_trn_tuploadesdeclarationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                     " and b.user_gid = c.user_gid and customer2sanction_gid='" + customer2sanction_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var get_sanfilename = new List<MOM_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["esdeclaration_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                    });
                }
                //values.MOM_DocumentList = getMOM_filename;
            }
            dt_datatable.Dispose();

            msSQL = " select maildocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tdeviationmaildocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and customer2sanction_gid='" + customer2sanction_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var get_sanfilename = new List<MOM_DocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["maildocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                    });
                }
                //values.MOM_DocumentList = getMOM_filename;
            }
            dt_datatable.Dispose();

            msSQL = " select  addbuyer_gid,if (document_name is null,'---',document_name) as document_name,baldocument_gid," +
                   " concat(date_format(a.created_date, '%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,buyer_name," +
                   " buyer_exposure,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as uploaded_by from ids_mst_taddbuyer a" +
                   " left join ids_trn_tuploadbaldocument d on a.addbuyer_gid = d.buyer_gid" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where" +
                    " a.customer2sanction_gid ='" + customer2sanction_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOM_DocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = (objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())),
                        buyer_gid = (dr_datarow["addbuyer_gid"].ToString()),
                        buyer_name = dr_datarow["buyer_name"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        buyer_exposure = dr_datarow["buyer_exposure"].ToString(),
                        baldocument_gid = dr_datarow["baldocument_gid"].ToString()
                    });
                }
                //values.MOM_DocumentList = getMOM_filename;
            }

            dt_datatable.Dispose();

            values.MOM_DocumentList = getMOM_filename;
            values.status = true;
            return true;
        }

        public void DaPostCreateDocumentLabel(MdlDocumentLabel values, string user_gid)
        {

            msSQL = "select documentlabel_gid from ids_mst_tdocumentlabel where documentlabel_name='" + values.documentlabel_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                objODBCDataReader.Close();
                values.status = false;
                values.message = "Document Label Name Already Exists";
                return;
            }
            objODBCDataReader.Close();
            if (values.documentlabel_desc == null)
            {
                values.documentlabel_desc = "";
            }
            else
            {
                values.documentlabel_desc = values.documentlabel_desc;
            }

            msGetGID = objcmnfunctions.GetMasterGID("DCTL");

            msSQL = " INSERT INTO ids_mst_tdocumentlabel(" +
                " documentlabel_gid," +
                " documentlabel_name," +
                " documentlabel_desc," +
                " department_gid," +
                " department_name," +
                " created_by," +
                " created_date)" +
                " VALUES(" +
                "'" + msGetGID + "'," +
                "'" + values.documentlabel_name.Replace("'", "") + "'," +
                "'" + values.documentlabel_desc.Replace("'", "") + "'," +
                "'" + values.department_gid + "'," +
                "'" + values.department_name + "'," +
                "'" + user_gid + "'," +
                "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Document Label Created Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured";

            }
        }

        public result PostUpdateDocumentLabel(MdlDocumentLabel values, string user_gid)
        {
            result objResult = new Models.result();
            msSQL = " select documentlabel_gid from ids_mst_tdocumentlabel " +
                     " where documentlabel_name='" + values.documentlabel_name.Replace("'", "") + "'";
            msGetGID = objdbconn.GetExecuteScalar(msSQL);
            if (msGetGID != "")
            {
                if (msGetGID != values.documentlabel_gid)
                {
                    objResult.message = "Document Label Already Exists";
                    objResult.status = false;
                    return objResult;
                }
            }
            if (values.documentlabel_desc == null)
            {
                values.documentlabel_desc = "";
            }
            else
            {
                values.documentlabel_desc = values.documentlabel_desc;
            }

            msSQL = " update ids_mst_tdocumentlabel set " +
                " documentlabel_name='" + values.documentlabel_name.Replace("'", "") + "'," +
                " documentlabel_desc='" + values.documentlabel_desc.Replace("'", "") + "'," +
                " department_gid='" + values.department_gid + "'," +
                " department_name='" + values.department_name + "'," +
                " updated_by='" + user_gid + "'," +
                " updated_date=current_timestamp where documentlabel_gid='" + values.documentlabel_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Label Updated Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public result DaDocumentLabelDelete(string documentlabel_gid)
        {
            result objResult = new Models.result();

            msSQL = " select customerfileupload_gid from ids_trn_tcustomerfileupload where documentlabel_gid='" + documentlabel_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                objResult.status = false;
                objResult.message = "Document Label Has Been Already Used,You Could Not Delete";
                return objResult;
            }
            objODBCDataReader.Close();
            msSQL = " delete from ids_mst_tdocumentlabel where documentlabel_gid='" + documentlabel_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Document Label Deleted Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public void DaGetDocumentLabelSummary(MdlDocumentLabel values)
        {
            try
            {
                msSQL = " select * from ids_mst_tdocumentlabel order by documentlabel_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<DocumentLabelList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new DocumentLabelList
                        {
                            documentlabel_gid = (dr_datarow["documentlabel_gid"].ToString()),
                            documentlabel_name = (dr_datarow["documentlabel_name"].ToString()),
                            documentlabel_desc = (dr_datarow["documentlabel_desc"].ToString()),
                            department_name = (dr_datarow["department_name"].ToString())
                        });
                    }
                    values.DocumentLabelList = getcustomer;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetDocumentLabel(MdlDocumentLabel values)
        {
            try
            {
                msSQL = " select * from ids_mst_tdocumentlabel order by documentlabel_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer = new List<DocumentLabelList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer.Add(new DocumentLabelList
                        {
                            documentlabel_gid = (dr_datarow["documentlabel_gid"].ToString()),
                            documentlabel_name = (dr_datarow["documentlabel_name"].ToString())
                        });
                    }
                    values.DocumentLabelList = getcustomer;
                }
                dt_datatable.Dispose();

                // Credit Admin Dropdown
                msSQL = " select * from ids_mst_tdocumentlabel where department_name like '%Credit Administration%' order by documentlabel_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditadmin = new List<CreditAdminDocumentLabelList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditadmin.Add(new CreditAdminDocumentLabelList
                        {
                            documentlabel_gid = (dr_datarow["documentlabel_gid"].ToString()),
                            documentlabel_name = (dr_datarow["documentlabel_name"].ToString())
                        });
                    }
                    values.CreditAdminDocumentLabelList = getcreditadmin;
                }
                dt_datatable.Dispose();
                // Credit Underwriting Dropdown
                msSQL = " select * from ids_mst_tdocumentlabel where department_name like '%Credit Underwriting%' order by documentlabel_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditunderwriting = new List<CreditUnderwritingDocumentLabelList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditunderwriting.Add(new CreditUnderwritingDocumentLabelList
                        {
                            documentlabel_gid = (dr_datarow["documentlabel_gid"].ToString()),
                            documentlabel_name = (dr_datarow["documentlabel_name"].ToString())
                        });
                    }
                    values.CreditUnderwritingDocumentLabelList = getcreditunderwriting;
                }
                dt_datatable.Dispose();
                // Credit Operations Dropdown
                msSQL = " select * from ids_mst_tdocumentlabel where department_name like '%Credit Operations%' order by documentlabel_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcreditoperation = new List<CreditOperationsDocumentLabelList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcreditoperation.Add(new CreditOperationsDocumentLabelList
                        {
                            documentlabel_gid = (dr_datarow["documentlabel_gid"].ToString()),
                            documentlabel_name = (dr_datarow["documentlabel_name"].ToString())
                        });
                    }
                    values.CreditOperationsDocumentLabelList = getcreditoperation;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetGetDocumentLabel(string documentlabel_gid, MdlDocumentLabel values)
        {
            try
            {
                msSQL = " select * from ids_mst_tdocumentlabel" +
                        " where documentlabel_gid='" + documentlabel_gid + "' ";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.documentlabel_name = objODBCDataReader["documentlabel_name"].ToString();
                    values.documentlabel_desc = objODBCDataReader["documentlabel_desc"].ToString();
                    values.department_gid = objODBCDataReader["department_gid"].ToString();
                    values.department_name = objODBCDataReader["department_name"].ToString();
                    objODBCDataReader.Close();
                }
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        //Document Tagging
        public result DaPostCreateCustomerFolder(MdlIdasDocumentUpload values, string user_gid)
        {
            result objResult = new Models.result();
            msGetGID = objcmnfunctions.GetMasterGID("CFUD");

            msSQL = " INSERT INTO ids_trn_tcustomerfileupload(" +
                " customerfileupload_gid," +
                " customer_gid," +
                " customer2sanction_gid," +
                " parent_directorygid," +
                " folder_name," +
                " directory_type," +
                " created_by)" +
                " VALUES(" +
                "'" + msGetGID + "'," +
                "'" + values.customer_gid + "'," +
                "'" + values.customer2sanction_gid + "'," +
                "'" + values.parent_directorygid + "'," +
                "'" + values.folder_name.Replace("'", "") + "'," +
                "'" + values.directory_type + "'," +
                "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Folder Created Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public void DaGetCustomerFolderDtls(string customer2sanction_gid, string parent_directorygid, string customer_gid, DirectoryDtlsList values)
        {
            msSQL = " select a.customerfileupload_gid,a.documentlabel_name,a.folder_name,a.document_name,a.document_path," +
                    " directory_type,a.remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " if(a.updated_by is null,concat(b.user_code,'/',b.user_firstname,b.user_lastname),concat(c.user_code,'/',c.user_firstname,c.user_lastname)  ) as created_by" +
                    " from ids_trn_tcustomerfileupload a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " left join adm_mst_tuser c on a.updated_by=c.user_gid" +
                    " where a.customer2sanction_gid='" + customer2sanction_gid + "' and a.parent_directorygid='" + parent_directorygid + "' and a.customer_gid='" + customer_gid + "'" +
                    " order by a.directory_type asc,a.created_date desc,a.updated_date desc,a.folder_name,document_name";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.DirectoryDtls = dt_datatable.AsEnumerable().Select(row => new DirectoryDtls
                {
                    customerfileupload_gid = row["customerfileupload_gid"].ToString(),
                    folder_name = row["folder_name"].ToString(),
                    documentlabel_name = row["documentlabel_name"].ToString(),
                    created_date = row["created_date"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    directory_type = row["directory_type"].ToString(),
                    remarks = row["remarks"].ToString(),
                    created_by = row["created_by"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";



            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }

        public void DaPostCustomerFileUpload(HttpRequest httpRequest, MdlIdasDocumentUpload objfilename, string user_gid)
        {
            // upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string parent_directorygid = httpRequest.Form["parent_directorygid"];
            string directory_type = httpRequest.Form["directory_type"];
            string customer_gid = httpRequest.Form["customer_gid"];
            string customer2sanction_gid = httpRequest.Form["customer2sanction_gid"];
            string documentlabel_gid = httpRequest.Form["documentlabel_gid"];
            string documentlabel_name = httpRequest.Form["documentlabel_name"];
            string remarks = httpRequest.Form["remarks"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            // path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CustomerFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/CustomerFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CustomerFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CustomerFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CustomerFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/CustomerFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("CFUD");
                        msSQL = " insert into ids_trn_tcustomerfileupload( " +
                                    " customerfileupload_gid ," +
                                    " customer_gid," +
                                    " customer2sanction_gid," +
                                    " documentlabel_gid," +
                                    " documentlabel_name," +
                                    " parent_directorygid," +
                                    " document_name ," +
                                    " document_path," +
                                    " directory_type," +
                                    " remarks," +
                                    " created_by" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + customer_gid + "'," +
                                    "'" + customer2sanction_gid + "'," +
                                    "'" + documentlabel_gid + "'," +
                                    "'" + documentlabel_name + "'," +
                                    "'" + parent_directorygid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + directory_type + "'," +
                                    "'" + remarks.Replace("'", "") + "'," +
                                    "'" + user_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
        }

        public void DaGetCustomerFilesBreadCrumb(string customerfileupload_gid, breadCrumbList values)
        {

            msSQL = " SELECT parent_directorygid,customerfileupload_gid,folder_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ids_trn_tcustomerfileupload" +
                    " where customerfileupload_gid='" + customerfileupload_gid + "' and directory_type='Folder'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                count = count + 1;
                if (objODBCDataReader["parent_directorygid"].ToString() == "$")
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        customerfileupload_gid = objODBCDataReader["customerfileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    objODBCDataReader.Close();
                }
                else
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        customerfileupload_gid = objODBCDataReader["customerfileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    var gid = objODBCDataReader["parent_directorygid"].ToString();
                    objODBCDataReader.Close();
                    DaGetCustomerFilesBreadCrumb(gid, values);
                }
                values.FolderDtls = FolderDtlsList;
            }
            objODBCDataReader.Close();
        }

        public result DaPostRenameCustomerFile(FolderDtls values, string user_gid)
        {
            result objResult = new Models.result();

            msSQL = " select parent_directorygid from ids_trn_tcustomerfileupload  where customerfileupload_gid = '" + values.customerfileupload_gid + "'";
            var parent_directorygid = objdbconn.GetExecuteScalar(msSQL);


            if (values.type == "File")
            {

                msSQL = " select customerfileupload_gid from ids_trn_tcustomerfileupload where parent_directorygid='" + parent_directorygid + "' and document_name='" + values.folder_name.Replace("'", "") + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    if (objODBCDataReader["customerfileupload_gid"].ToString() != values.customerfileupload_gid)
                    {
                        objODBCDataReader.Close();
                        objResult.status = false;
                        objResult.message = " File Name Already Exits";
                        return objResult;

                    }
                    objODBCDataReader.Close();
                }

                msSQL = " update ids_trn_tcustomerfileupload set" +
                 " document_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                 " where customerfileupload_gid='" + values.customerfileupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.type == "Folder")
            {

                msSQL = " select customerfileupload_gid from ids_trn_tcustomerfileupload where parent_directorygid='" + parent_directorygid + "' and folder_name='" + values.folder_name.Replace("'", "") + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    if (objODBCDataReader["customerfileupload_gid"].ToString() != values.customerfileupload_gid)
                    {
                        objODBCDataReader.Close();
                        objResult.status = false;
                        objResult.message = " Folder Name Already Exits";
                        return objResult;

                    }
                    objODBCDataReader.Close();
                }

                msSQL = " update ids_trn_tcustomerfileupload set" +
                 " folder_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                 " where customerfileupload_gid='" + values.customerfileupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = " File Renamed Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public result DaFileDelete(string customerfileupload_gid)
        {
            result objResult = new Models.result();

            msSQL = " select customerfileupload_gid from ids_trn_tcustomerfileupload where parent_directorygid='" + customerfileupload_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                objResult.status = false;
                objResult.message = "Folder Has File,You Could Not Delete";
                return objResult;
            }
            objODBCDataReader.Close();
            msSQL = " delete from ids_trn_tcustomerfileupload where customerfileupload_gid='" + customerfileupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Folder Deleted Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public bool DaGetCadTeamFlag(MdlCadTeamFlag values, string employee_gid)
        {
            msSQL = " select a.department_gid from hrm_mst_temployee a " +
                    " left join hrm_mst_tdepartment b on b.department_gid=a.department_gid " +
                    " where a.employee_gid='" + employee_gid + "'" +
                    " and b.department_name like '%Credit Administration%'";
            string dep_gid = objdbconn.GetExecuteScalar(msSQL);
            if (dep_gid == "")
            {
                values.cadteam_flag = "N";
            }
            else
            {
                values.cadteam_flag = "Y";
            }
            values.status = true;
            return true;
        }

        public void DaGetDocumentTaggedCustomer(MdlTaggedCustomer objCustomer)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y') as created_date" +
                  " from ocs_mst_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                  " where a.customer_gid in (select customer_gid from ids_trn_tcustomerfileupload) order by a.customer_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }

        }
        public void DaGetDocumentUnTaggedCustomer(MdlTaggedCustomer objCustomer)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_code,a.customer_urn,a.customername,a.contactperson, " +
                  " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                  " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                  " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                  " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager, " +
                  " case when a.creditmanager_gid = '' then 'NA' else a.creditmgmt_name end as creditmgmt_name, " +
                  " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y') as created_date" +
                  " from ocs_mst_tcustomer a " +
                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                  " where a.customer_gid not in (select customer_gid from ids_trn_tcustomerfileupload) order by a.customer_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    objCustomer.customer_list = dt_datatable.AsEnumerable().Select(row =>
                    new customer_list
                    {
                        customer_gid = (row["customer_gid"].ToString()),
                        customercode = (row["customer_code"].ToString()),
                        customername = (row["customername"].ToString()),
                        contactperson = (row["contactperson"].ToString()),
                        vertical_code = (row["vertical_code"].ToString()),
                        zonalGid = (row["zonal_head"].ToString()),
                        businessHeadGid = (row["business_head"].ToString()),
                        relationshipMgmtGid = (row["relationship_manager"].ToString()),
                        clustermanagerGid = (row["cluster_manager"].ToString()),
                        customer_urn = (row["customer_urn"].ToString()),
                        creditmanagerName = (row["creditmgmt_name"].ToString()),
                        created_by = (row["created_by"].ToString()),
                        created_date = (row["created_date"].ToString())
                    }).ToList();
                    dt_datatable.Dispose();

                }
                objCustomer.status = true;
            }
            catch
            {
                objCustomer.status = false;
            }



        }

        public void DaGetDocumentCustomerCount(MdlTaggedCustomer values)
        {
            msSQL = "select count(*) as tagged_count from ocs_mst_tcustomer where customer_gid in (select customer_gid from ids_trn_tcustomerfileupload)";
            values.tagged_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as tagged_count from ocs_mst_tcustomer where customer_gid not in (select customer_gid from ids_trn_tcustomerfileupload)";
            values.untagged_count = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetWorkItemArchivalCustomerSummary(WorkItemList values, MdlArchivalCondition objCondition)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date, '%d-%m-%Y %h:%i %p') as email_date, " +
           " a.email_subject,a.email_content,date_format(a.updated_date, '%d-%m-%Y') as created_date,a.aging,a.seen_flag, b.rmemployee_gid,c.archival_type, " +
           " c.customer_name, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by, " +
           " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as rmemployee_name, c.remarks " +
           " FROM isn_trn_tmaildetails a " +
           " INNER JOIN isn_trn_tworkitemdecision c ON a.email_gid = c.email_gid " +
           " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid = b.email_gid " +
           " LEFT JOIN adm_mst_tuser d on a.updated_by = d.user_gid " +
           " WHERE 1 = 1 AND a.status = 'Archival' AND c.decision = 'Archival' ";
            if (objCondition.customer_gid != "")
            {
                msSQL += " AND (c.archival_type = 'Customer' AND c.customer_gid = '" + objCondition.customer_gid + "')";
            }
            msSQL += " GROUP BY a.email_gid UNION " +
            " SELECT a.composemail_gid, a.composemail_refno, a.frommail_id, date_format(a.created_date, '%d-%m-%Y %h:%i %p') as email_date, " +
            " a.email_subject,a.mailcontent, date_format(a.updated_date, '%d-%m-%Y') as created_date,b.aging,b.seen_flag, e.rmemployee_gid, c.archival_type, " +
            " c.customer_name, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by, " +
            " if (e.rmemployee_name is null,'No', upper(substr(e.rmemployee_name, 1, 1))) as rmemployee_name, c.remarks " +
            " FROM isn_trn_tcomposemail a " +
            " INNER JOIN isn_trn_tworkitemdecision c ON a.composemail_gid = c.email_gid " +
            " LEFT JOIN isn_trn_tmaildetails b ON b.email_gid = c.email_gid " +
            " LEFT JOIN adm_mst_tuser d on a.updated_by = d.user_gid " +
            " LEFT JOIN isn_trn_tworkitemassign e ON e.email_gid = b.email_gid " +
            " WHERE 1 = 1 AND a.status = 'Archival' AND c.decision = 'Archival' ";
            if (objCondition.customer_gid != "")
            {
                msSQL += " AND (c.archival_type = 'Customer' AND c.customer_gid = '" + objCondition.customer_gid + "')";
            }
            msSQL += " GROUP BY a.composemail_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    aging = row["aging"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    archival_type = row["archival_type"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    remarks = row["remarks"].ToString(),
                    archival_by = row["created_by"].ToString(),
                    archival_date = row["created_date"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }


        }
        public void DaGetWorkItemArchivalSpecificSummary(WorkItemList values, MdlArchivalCondition objCondition)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date, '%d-%m-%Y %h:%i %p') as email_date, " +
          " a.email_subject,a.email_content,date_format(a.updated_date, '%d-%m-%Y') as created_date,a.aging,a.seen_flag, b.rmemployee_gid,c.archival_type, " +
          " c.customer_name, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by, " +
          " if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as rmemployee_name, c.remarks " +
          " FROM isn_trn_tmaildetails a " +
          " INNER JOIN isn_trn_tworkitemdecision c ON a.email_gid = c.email_gid " +
          " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid = b.email_gid " +
          " LEFT JOIN adm_mst_tuser d on a.updated_by = d.user_gid " +
          " WHERE 1 = 1 AND a.status = 'Archival' AND c.decision = 'Archival' ";
            if (objCondition.customer_gid != "")
            {
                msSQL += " AND (c.archival_type = 'Specific' AND c.customer_gid = '" + objCondition.customer_gid + "' AND c.customer2sanction_gid = '" + objCondition.customer2sanction_gid + "')";
            }
            msSQL += " GROUP BY a.email_gid UNION " +
            " SELECT a.composemail_gid, a.composemail_refno, a.frommail_id, date_format(a.created_date, '%d-%m-%Y %h:%i %p') as email_date, " +
            " a.email_subject,a.mailcontent, date_format(a.updated_date, '%d-%m-%Y') as created_date,b.aging,b.seen_flag, e.rmemployee_gid, c.archival_type, " +
            " c.customer_name, concat(d.user_code,' / ',d.user_firstname,d.user_lastname) as created_by, " +
            " if (e.rmemployee_name is null,'No', upper(substr(e.rmemployee_name, 1, 1))) as rmemployee_name, c.remarks " +
            " FROM isn_trn_tcomposemail a " +
            " INNER JOIN isn_trn_tworkitemdecision c ON a.composemail_gid = c.email_gid " +
            " LEFT JOIN isn_trn_tmaildetails b ON b.email_gid = c.email_gid " +
            " LEFT JOIN adm_mst_tuser d on a.updated_by = d.user_gid " +
            " LEFT JOIN isn_trn_tworkitemassign e ON e.email_gid = b.email_gid " +
            " WHERE 1 = 1 AND a.status = 'Archival' AND c.decision = 'Archival' ";
            if (objCondition.customer_gid != "")
            {
                msSQL += " AND (c.archival_type = 'Specific' AND c.customer_gid = '" + objCondition.customer_gid + "' AND c.customer2sanction_gid = '" + objCondition.customer2sanction_gid + "')";
            }
            msSQL += " GROUP BY a.composemail_gid";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    aging = row["aging"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["rmemployee_name"].ToString(),
                    seen_flag = row["seen_flag"].ToString(),
                    archival_type = row["archival_type"].ToString(),
                    customer_name = row["customer_name"].ToString(),
                    remarks = row["remarks"].ToString(),
                    archival_by = row["created_by"].ToString(),
                    archival_date = row["created_date"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }


        }

        // Credit Underwriting Document Upload

        public void DaCreditFileUpload(HttpRequest httpRequest, MdlIdasDocumentUpload objfilename, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string parent_directorygid = httpRequest.Form["parent_directorygid"];
            string directory_type = httpRequest.Form["directory_type"];
            string customer_gid = httpRequest.Form["customer_gid"];
            string documentlabel_gid = httpRequest.Form["documentlabel_gid"];
            string documentlabel_name = httpRequest.Form["documentlabel_name"];
            string remarks = httpRequest.Form["remarks"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CreditFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/CreditFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CreditFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CreditFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CreditFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/CreditFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("CFUG");
                        msSQL = " insert into ids_trn_tcreditfileupload( " +
                                    " creditfileupload_gid ," +
                                    " customer_gid," +
                                    " parent_directorygid," +
                                    " document_name ," +
                                    " document_path," +
                                    " directory_type," +
                                    " documentlabel_gid," +
                                    " documentlabel_name," +
                                    " remarks," +
                                    " created_by," +
                                    " created_date " +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + customer_gid + "'," +
                                    "'" + parent_directorygid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + directory_type + "'," +
                                    "'" + documentlabel_gid + "'," +
                                    "'" + documentlabel_name + "',";
                        if (remarks == null || remarks == "" || remarks == "undefined")
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + remarks.Replace("'", "") + "',";
                        }
                        msSQL += "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
        }

        public void DaCreditFolderDtls(string parent_directorygid, string customer_gid, DirectoryDtlsList values)
        {
            msSQL = " select a.creditfileupload_gid,a.folder_name,a.document_name,a.document_path, directory_type,documentlabel_name,remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by" +
                    " from ids_trn_tcreditfileupload a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " left join adm_mst_tuser c on a.updated_by=c.user_gid" +
                    " where a.parent_directorygid='" + parent_directorygid + "' and a.customer_gid='" + customer_gid + "' order by creditfileupload_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.DirectoryDtls = dt_datatable.AsEnumerable().Select(row => new DirectoryDtls
                {
                    creditfileupload_gid = row["creditfileupload_gid"].ToString(),
                    folder_name = row["folder_name"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    directory_type = row["directory_type"].ToString(),
                    documentlabel_name = row["documentlabel_name"].ToString(),
                    remarks = row["remarks"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                }).ToList();

                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }

        public void DaCreateCreditFolder(MdlIdasDocumentUpload values, string user_gid)
        {
            msGetGID = objcmnfunctions.GetMasterGID("CFUG");

            msSQL = " INSERT INTO ids_trn_tcreditfileupload(" +
                " creditfileupload_gid," +
                " customer_gid," +
                " parent_directorygid," +
                " folder_name," +
                " directory_type," +
                " created_by," +
                " created_date " +
                " )VALUES(" +
                "'" + msGetGID + "'," +
                "'" + values.customer_gid + "'," +
                "'" + values.parent_directorygid + "'," +
                "'" + values.folder_name.Replace("'", "") + "'," +
                "'" + values.directory_type + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Folder Created Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public bool DaRenameCreditFile(FolderDtls values, string user_gid)
        {
            msSQL = " select parent_directorygid from ids_trn_tcreditfileupload  where creditfileupload_gid = '" + values.creditfileupload_gid + "'";
            var parent_directorygid = objdbconn.GetExecuteScalar(msSQL);


            if (values.type == "File")
            {

                msSQL = " select creditfileupload_gid from ids_trn_tcreditfileupload where parent_directorygid='" + parent_directorygid + "' and document_name='" + values.folder_name.Replace("'", "") + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    if (objODBCDataReader["creditfileupload_gid"].ToString() != values.creditfileupload_gid)
                    {
                        objODBCDataReader.Close();
                        values.status = false;
                        values.message = " File Name Already Exits";
                        return false;
                    }
                    objODBCDataReader.Close();
                }

                msSQL = " update ids_trn_tcreditfileupload set" +
                 " document_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                 " where creditfileupload_gid='" + values.creditfileupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.type == "Folder")
            {

                msSQL = " select creditfileupload_gid from ids_trn_tcreditfileupload where parent_directorygid='" + parent_directorygid + "' and folder_name='" + values.folder_name.Replace("'", "") + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    if (objODBCDataReader["creditfileupload_gid"].ToString() != values.creditfileupload_gid)
                    {
                        objODBCDataReader.Close();
                        values.status = false;
                        values.message = " Folder Name Already Exits";
                        return false;
                    }
                    objODBCDataReader.Close();
                }

                msSQL = " update ids_trn_tcreditfileupload set" +
                 " folder_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                 " where creditfileupload_gid='" + values.creditfileupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = " File Renamed Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaCreditFileDelete(string creditfileupload_gid, result values)
        {
            msSQL = " select creditfileupload_gid from ids_trn_tcreditfileupload where parent_directorygid='" + creditfileupload_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                values.status = false;
                values.message = "Folder Has File,You Could Not Delete";
                return false;
            }
            objODBCDataReader.Close();
            msSQL = " delete from ids_trn_tcreditfileupload where creditfileupload_gid='" + creditfileupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Folder Deleted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaCreditFilesBreadCrumb(string creditfileupload_gid, breadCrumbList values)
        {
            msSQL = " SELECT parent_directorygid,creditfileupload_gid,folder_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ids_trn_tcreditfileupload where creditfileupload_gid='" + creditfileupload_gid + "' and directory_type='Folder'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                count = count + 1;
                if (objODBCDataReader["parent_directorygid"].ToString() == "$")
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        creditfileupload_gid = objODBCDataReader["creditfileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    objODBCDataReader.Close();
                }
                else
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        creditfileupload_gid = objODBCDataReader["creditfileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    var gid = objODBCDataReader["parent_directorygid"].ToString();
                    objODBCDataReader.Close();
                    DaCreditFilesBreadCrumb(gid, values);
                }
                values.FolderDtls = FolderDtlsList;
            }
            objODBCDataReader.Close();
        }

        public bool DaGetCreditTeamFlag(MdlCreditTeamFlag values, string employee_gid)
        {
            msSQL = " select a.department_gid from hrm_mst_temployee a " +
                    " left join hrm_mst_tdepartment b on b.department_gid=a.department_gid " +
                    " where a.employee_gid='" + employee_gid + "'" +
                    " and b.department_name like '%Credit Underwriting%'";
            string dep_gid = objdbconn.GetExecuteScalar(msSQL);
            if (dep_gid == "")
            {
                values.creditteam_flag = "N";
            }
            else
            {
                values.creditteam_flag = "Y";
            }
            values.status = true;
            return true;
        }

        // Credit Operations Document Upload

        public void DaCreditOperationsFileUpload(HttpRequest httpRequest, MdlIdasDocumentUpload objfilename, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;

            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;

            string lsdocumenttype_gid = string.Empty;
            string parent_directorygid = httpRequest.Form["parent_directorygid"];
            string directory_type = httpRequest.Form["directory_type"];
            string customer_gid = httpRequest.Form["customer_gid"];
            string documentlabel_gid = httpRequest.Form["documentlabel_gid"];
            string documentlabel_name = httpRequest.Form["documentlabel_name"];
            string remarks = httpRequest.Form["remarks"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CreditOperationsFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CreditOperationsFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/CreditOperationsFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/CreditOperationsFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/CreditOperationsFileUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("COFU");
                        msSQL = " insert into ids_trn_tcreditoperationsfileupload( " +
                                    " creditoperationsfileupload_gid ," +
                                    " customer_gid," +
                                    " parent_directorygid," +
                                    " document_name ," +
                                    " document_path," +
                                    " directory_type," +
                                    " documentlabel_gid," +
                                    " documentlabel_name," +
                                    " remarks," +
                                    " created_by," +
                                    " created_date " +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + customer_gid + "'," +
                                    "'" + parent_directorygid + "'," +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + directory_type + "'," +
                                    "'" + documentlabel_gid + "'," +
                                    "'" + documentlabel_name + "',";
                        if (remarks == null || remarks == "" || remarks == "undefined")
                        {
                            msSQL += "'',";
                        }
                        else
                        {
                            msSQL += "'" + remarks.Replace("'", "") + "',";
                        }
                        msSQL += "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
        }

        public void DaCreditOperationsFolderDtls(string parent_directorygid, string customer_gid, DirectoryDtlsList values)
        {
            msSQL = " select a.creditoperationsfileupload_gid,a.folder_name,a.document_name,a.document_path, directory_type,documentlabel_name,remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by" +
                    " from ids_trn_tcreditoperationsfileupload a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " left join adm_mst_tuser c on a.updated_by=c.user_gid" +
                    " where a.parent_directorygid='" + parent_directorygid + "' and a.customer_gid='" + customer_gid + "' order by creditoperationsfileupload_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.DirectoryDtls = dt_datatable.AsEnumerable().Select(row => new DirectoryDtls
                {
                    creditoperationsfileupload_gid = row["creditoperationsfileupload_gid"].ToString(),
                    folder_name = row["folder_name"].ToString(),
                    document_name = row["document_name"].ToString(),
                    document_path = objcmnstorage.EncryptData(row["document_path"].ToString()),
                    directory_type = row["directory_type"].ToString(),
                    documentlabel_name = row["documentlabel_name"].ToString(),
                    remarks = row["remarks"].ToString(),
                    created_by = row["created_by"].ToString(),
                    created_date = row["created_date"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
            dt_datatable.Dispose();
        }

        public void DaCreateCreditOperationsFolder(MdlIdasDocumentUpload values, string user_gid)
        {
            msGetGID = objcmnfunctions.GetMasterGID("COFU");

            msSQL = " INSERT INTO ids_trn_tcreditoperationsfileupload(" +
                " creditoperationsfileupload_gid," +
                " customer_gid," +
                " parent_directorygid," +
                " folder_name," +
                " directory_type," +
                " created_by," +
                " created_date " +
                " )VALUES(" +
                "'" + msGetGID + "'," +
                "'" + values.customer_gid + "'," +
                "'" + values.parent_directorygid + "'," +
                "'" + values.folder_name.Replace("'", "") + "'," +
                "'" + values.directory_type + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Folder Created Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Creating Folder";
            }
        }

        public bool DaRenameCreditOperationsFile(FolderDtls values, string user_gid)
        {
            msSQL = " select parent_directorygid from ids_trn_tcreditoperationsfileupload where creditoperationsfileupload_gid = '" + values.creditoperationsfileupload_gid + "'";
            var parent_directorygid = objdbconn.GetExecuteScalar(msSQL);


            if (values.type == "File")
            {
                msSQL = " select creditoperationsfileupload_gid from ids_trn_tcreditoperationsfileupload where parent_directorygid='" + parent_directorygid + "' and document_name='" + values.folder_name.Replace("'", "") + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    if (objODBCDataReader["creditoperationsfileupload_gid"].ToString() != values.creditoperationsfileupload_gid)
                    {
                        objODBCDataReader.Close();
                        values.status = false;
                        values.message = " File Name Already Exits";
                        return false;
                    }
                    objODBCDataReader.Close();
                }

                msSQL = " update ids_trn_tcreditoperationsfileupload set" +
                 " document_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                 " where creditoperationsfileupload_gid='" + values.creditoperationsfileupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else if (values.type == "Folder")
            {

                msSQL = " select creditoperationsfileupload_gid from ids_trn_tcreditoperationsfileupload where parent_directorygid='" + parent_directorygid + "' and folder_name='" + values.folder_name.Replace("'", "") + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    objODBCDataReader.Read();
                    if (objODBCDataReader["creditoperationsfileupload_gid"].ToString() != values.creditoperationsfileupload_gid)
                    {
                        objODBCDataReader.Close();
                        values.status = false;
                        values.message = " Folder Name Already Exits";
                        return false;
                    }
                    objODBCDataReader.Close();
                }

                msSQL = " update ids_trn_tcreditoperationsfileupload set" +
                 " folder_name='" + values.folder_name.Replace("'", "") + "',updated_by='" + user_gid + "',updated_date=current_timestamp" +
                 " where creditoperationsfileupload_gid='" + values.creditoperationsfileupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = " File Renamed Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaCreditOperationsFileDelete(string creditoperationsfileupload_gid, result values)
        {
            msSQL = " select creditoperationsfileupload_gid from ids_trn_tcreditoperationsfileupload where parent_directorygid='" + creditoperationsfileupload_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Close();
                values.status = false;
                values.message = "Folder Has File,You Could Not Delete";
                return false;
            }
            objODBCDataReader.Close();
            msSQL = " delete from ids_trn_tcreditoperationsfileupload where creditoperationsfileupload_gid='" + creditoperationsfileupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Folder Deleted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Deleting the Folder";
                return false;
            }
        }

        public void DaCreditOperationsFilesBreadCrumb(string creditoperationsfileupload_gid, breadCrumbList values)
        {
            msSQL = " SELECT parent_directorygid,creditoperationsfileupload_gid,folder_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ids_trn_tcreditoperationsfileupload where creditoperationsfileupload_gid='" + creditoperationsfileupload_gid + "' and directory_type='Folder'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                count = count + 1;
                if (objODBCDataReader["parent_directorygid"].ToString() == "$")
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        creditoperationsfileupload_gid = objODBCDataReader["creditoperationsfileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    objODBCDataReader.Close();
                }
                else
                {
                    FolderDtlsList.Add(new FolderDtls
                    {
                        creditoperationsfileupload_gid = objODBCDataReader["creditoperationsfileupload_gid"].ToString(),
                        folder_name = objODBCDataReader["folder_name"].ToString(),
                        serial_no = Convert.ToString(count),
                    });
                    var gid = objODBCDataReader["parent_directorygid"].ToString();
                    objODBCDataReader.Close();
                    DaCreditOperationsFilesBreadCrumb(gid, values);
                }
                values.FolderDtls = FolderDtlsList;
            }
            objODBCDataReader.Close();
        }

        public bool DaGetCreditOperationsTeamFlag(MdlCreditTeamFlag values, string employee_gid)
        {
            msSQL = " select a.department_gid from hrm_mst_temployee a " +
                    " left join hrm_mst_tdepartment b on b.department_gid=a.department_gid " +
                    " where a.employee_gid='" + employee_gid + "'" +
                    " and b.department_name like '%Credit Operations%'";
            string dep_gid = objdbconn.GetExecuteScalar(msSQL);
            if (dep_gid == "")
            {
                values.creditoperationteam_flag = "N";
            }
            else
            {
                values.creditoperationteam_flag = "Y";
            }
            values.status = true;
            return true;
        }

        public void DaGetDepartmentList(Departmentname values)
        {
            msSQL = " select department_gid, department_name from hrm_mst_tdepartment order by department_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdepartment_list = new List<department_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdepartment_list.Add(new department_list
                    {
                        department_name = dt["department_name"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                    });
                    values.department_list = getdepartment_list;
                }
            }
            dt_datatable.Dispose();
        }
    }
}

