using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ems.master.Models;
using System.Configuration;
using ems.storage.Functions;

namespace ems.master.DataAccess
{
    public class DaMstSPODocumentUpload
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();     
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        string lscustomer_gid, lscustomer2usertype_gid, lsurn, lspath;
        HttpPostedFile httpPostedFile;
        int mnResult;
        public void DaGetSOPdepartment_list(MdlMstSOPdepartment_list values)
        {
            try
            {
                msSQL = "select sopdepartment_name,sopdepartment_gid from ocs_trn_tsopdepartment order by sopdepartment_name asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsopdepartment_list = new List<sopdepartment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsopdepartment_list.Add(new sopdepartment_list
                        {
                            sopdepartment_gid = (dr_datarow["sopdepartment_gid"].ToString()),
                            sopdepartment_name = (dr_datarow["sopdepartment_name"].ToString()),
                        });
                    }
                    values.sopdepartment_list = getsopdepartment_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        //public bool DaPostSOPDocument(HttpRequest httpRequest, MdlSOPDocument_upload objfilename, string employee_gid)
        //{
        //    SOPDocument_upload objdocumentmodel = new SOPDocument_upload();
        //    HttpFileCollection httpFileCollection;
        //    string lsfilepath = string.Empty;
        //    string lsdocument_gid = string.Empty;
        //    MemoryStream ms = new MemoryStream();
        //    MemoryStream ms_stream = new MemoryStream();
        //    string document_gid = string.Empty;
        //    string lscompany_code = string.Empty;
        //    string pdfFilName = string.Empty;
        //    Stream ls_readStream;
        //    string lsdocumenttype_gid = string.Empty;
        //    String path = lspath;
        //    //string project_flag = httpRequest.Form["project_flag"].ToString();

        //    string lssopdocument_name = httpRequest.Form["sopdocument_name"].ToString();
        //    string lssop_code = httpRequest.Form["sop_code"].ToString();
        //    string lssopdepartment_name = httpRequest.Form["sopdepartment_name"].ToString();
        //    string lssop_versionno = httpRequest.Form["sop_versionno"].ToString();
        //    string lssopdepartment_gid = httpRequest.Form["sopdepartment_gid"].ToString();

        //    msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

        //    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
        //    path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "SOP/SOPDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
        //    {
        //        if ((!System.IO.Directory.Exists(path)))
        //            System.IO.Directory.CreateDirectory(path);
        //    }
        //    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
        //    string lsfirstdocument_filepath = string.Empty;

        //    httpFileCollection = httpRequest.Files;

        //    httpPostedFile = httpFileCollection[0];
        //    string FileExtension = httpPostedFile.FileName;
        //    //string lsfile_gid = msdocument_gid + FileExtension;
        //    string lsfile_gid = msdocument_gid;
        //    FileExtension = Path.GetExtension(FileExtension).ToLower();
        //    lsfile_gid = lsfile_gid + FileExtension;
        //    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
        //    {
        //        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SOP/SOPDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
        //        objcmnfunctions.uploadFile(lspath, lsfile_gid);

        //        msGetGid = objcmnfunctions.GetMasterGID("SOPD");
        //        msSQL = " insert into ocs_trn_tsopdocumentupload( " +
        //                     " sopdocumentupload_gid," +
        //                     " uploadeddocument_name, " +
        //                     " uploadeddocument_path," +
        //                     " sopdepartment_gid," +
        //                     " sopdepartment_name," +
        //                     " sopdocument_name," +
        //                     " sop_code, " +
        //                     " sop_versionno," +
        //                     " uploaded_by ," +
        //                     " uploaded_date " +
        //                     " )values(" +
        //                     "'" + msGetGid + "'," +
        //                     "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
        //                     "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
        //                     "'" + lssopdepartment_gid + "'," +
        //                     "'" + lssopdepartment_name + "',";
        //        if((lssopdocument_name==null)||(lssopdocument_name==""))
        //        {
        //            msSQL += "'',";
        //        }
        //        else
        //        {
        //            msSQL += "'" + lssopdocument_name.Replace("'","") + "',";
        //        }
        //        if ((lssop_code == null) || (lssop_code == ""))
        //        {
        //            msSQL += "'',";
        //        }
        //        else
        //        {
        //            msSQL += "'" + lssop_code.Replace("'", "") + "',";
        //        }
        //        if ((lssop_versionno == null) || (lssop_versionno == ""))
        //        {
        //            msSQL += "'',";
        //        }
        //        else
        //        {
        //            msSQL += "'" + lssop_versionno.Replace("'", "") + "',";
        //        }
        //       msSQL+="'" + employee_gid + "'," +
        //                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //        if (mnResult != 0)
        //        {
        //            msSQL = "select a.sopdepartment_name,a.sopdepartment_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
        //            " date_format(a.uploaded_date,'%d-%m-%Y %H:%i %p') as created_date from ocs_trn_tsopdocumentupload a " +
        //            " left join hrm_mst_temployee b on a.uploaded_by=b.employee_gid" +
        //            " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
        //            " group by a.sopdepartment_gid order by a.sopdocumentupload_gid desc";
        //            dt_datatable = objdbconn.GetDataTable(msSQL);
        //            var get_filename = new List<sopdepartment_list>();
        //            if (dt_datatable.Rows.Count != 0)
        //            {
        //                foreach (DataRow dr_datarow in dt_datatable.Rows)
        //                {
        //                    get_filename.Add(new sopdepartment_list
        //                    {
        //                        sopdepartment_name = dr_datarow["sopdepartment_name"].ToString(),
        //                        sopdepartment_gid = dr_datarow["sopdepartment_gid"].ToString(),
        //                        created_date = (dr_datarow["created_date"].ToString()),
        //                        created_by = (dr_datarow["created_by"].ToString()),
        //                    });
        //                }
        //                objfilename.sopdepartment_list = get_filename;
        //            }
        //            dt_datatable.Dispose();

        //            objfilename.status = true;
        //            objfilename.message = "SOP Document uploaded successfully";
        //            return true;
        //        }
        //        else
        //        {
        //            objfilename.status = false;
        //            objfilename.message = "Error Occured while uploading SOP document";
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        objfilename.status = false;
        //        objfilename.message = "File format is not supported";
        //        return false;
        //    }
        //}

        public bool DaPostSOPDocument(HttpRequest httpRequest, MdlSOPDocument_upload objfilename, string employee_gid)
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
            string project_flag = httpRequest.Form["project_flag"].ToString();

            string lssopdocument_name = httpRequest.Form["sopdocument_name"].ToString();
            string lssop_code = httpRequest.Form["sop_code"].ToString();
            string lssopdepartment_name = httpRequest.Form["sopdepartment_name"].ToString();
            string lssop_versionno = httpRequest.Form["sop_versionno"].ToString();
            string lssopdepartment_gid = httpRequest.Form["sopdepartment_gid"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "SOP/SOPDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }


            //string document_title = httpRequest.Form["document_title"].ToString();

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
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    {



                        ls_readStream = httpPostedFile.InputStream;
                    ls_readStream.CopyTo(ms);


                    // Check Document validation;

                    byte[] bytes = ms.ToArray();
                    if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                    {
                        objfilename.message = "File format is not supported";
                        return false;
                    }
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SOP/SOPDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "SOP/SOPDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                    msGetGid = objcmnfunctions.GetMasterGID("SOPD");
                    msSQL = " insert into ocs_trn_tsopdocumentupload( " +
                                 " sopdocumentupload_gid," +
                                 " uploadeddocument_name, " +
                                 " uploadeddocument_path," +
                                 " sopdepartment_gid," +
                                 " sopdepartment_name," +
                                 " sopdocument_name," +
                                 " sop_code, " +
                                 " sop_versionno," +
                                 " uploaded_by ," +
                                 " uploaded_date " +
                                 " )values(" +
                                 "'" + msGetGid + "'," +
                                 "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                 "'" + lspath + msdocument_gid + FileExtension.Replace("'", " ") + "'," +
                                 "'" + lssopdepartment_gid + "'," +
                                 "'" + lssopdepartment_name + "',";
                    if ((lssopdocument_name == null) || (lssopdocument_name == ""))
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + lssopdocument_name.Replace("'", "") + "',";
                    }
                    if ((lssop_code == null) || (lssop_code == ""))
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + lssop_code.Replace("'", "") + "',";
                    }
                    if ((lssop_versionno == null) || (lssop_versionno == ""))
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + lssop_versionno.Replace("'", "") + "',";
                    }
                    msSQL += "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "select a.sopdepartment_name,a.sopdepartment_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                               " date_format(a.uploaded_date,'%d-%m-%Y %H:%i %p') as created_date from ocs_trn_tsopdocumentupload a " +
                               " left join hrm_mst_temployee b on a.uploaded_by=b.employee_gid" +
                               " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                               " group by a.sopdepartment_gid order by a.sopdocumentupload_gid desc";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var get_filename = new List<sopdepartment_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                get_filename.Add(new sopdepartment_list
                                {
                                    sopdepartment_name = dr_datarow["sopdepartment_name"].ToString(),
                                    sopdepartment_gid = dr_datarow["sopdepartment_gid"].ToString(),
                                    created_date = (dr_datarow["created_date"].ToString()),
                                    created_by = (dr_datarow["created_by"].ToString()),
                                });
                            }
                            objfilename.sopdepartment_list = get_filename;
                        }
                        dt_datatable.Dispose();

                        objfilename.status = true;
                        objfilename.message = "SOP Document uploaded successfully";
                        return true;
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "Error Occured while uploading SOP document";
                        return false;
                    }
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File Format is Not Supported";
                    }
                }
            }
            return true;
        }
        public void DaGetSOPDocumentSummary(MdlMstSOPdepartment_list values)
        {
            try
            {
                msSQL = "select a.sopdepartment_name,a.sopdepartment_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,"+
                    " date_format(a.uploaded_date,'%d-%m-%Y %H:%i %p') as created_date from ocs_trn_tsopdocumentupload a " +
                    " left join hrm_mst_temployee b on a.uploaded_by=b.employee_gid"+
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid"+
                    " group by a.sopdepartment_gid order by a.sopdocumentupload_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsopdepartment_list = new List<sopdepartment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getsopdepartment_list.Add(new sopdepartment_list
                        {
                            sopdepartment_gid = (dr_datarow["sopdepartment_gid"].ToString()),
                            sopdepartment_name = (dr_datarow["sopdepartment_name"].ToString()),
                            created_date= (dr_datarow["created_date"].ToString()),
                            created_by= (dr_datarow["created_by"].ToString()),
                        });
                    }
                    values.sopdepartment_list = getsopdepartment_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public void DaGetSOPdocument_list(MdlSOPDocument_upload values,string sopdepartment_gid)
        {
            try
            {
                msSQL = "select a.sopdepartment_name,a.sopdocumentupload_gid,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.uploaded_date,'%d-%m-%Y %H:%i %p') as created_date,uploadeddocument_name,uploadeddocument_path,"+
                    " sopdocument_name,sop_code,sop_versionno from ocs_trn_tsopdocumentupload a " +
                    " left join hrm_mst_temployee b on a.uploaded_by=b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid where sopdepartment_gid='"+ sopdepartment_gid  + "'" +
                    " order by a.sopdocumentupload_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSOPDocument_upload = new List<SOPDocument_upload>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSOPDocument_upload.Add(new SOPDocument_upload
                        {
                            sopdocumentupload_gid = (dr_datarow["sopdocumentupload_gid"].ToString()),
                            sopdepartment_name = (dr_datarow["sopdepartment_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            document_name = (dr_datarow["uploadeddocument_name"].ToString()),
                           // document_type = (dr_datarow["uploadeddocument_path"].ToString()),
                            sopdocument_name = (dr_datarow["sopdocument_name"].ToString()),
                            sop_code = (dr_datarow["sop_code"].ToString()),
                            sop_versionno = (dr_datarow["sop_versionno"].ToString()),
                            document_path = objcmnstorage.EncryptData(dr_datarow["uploadeddocument_path"].ToString()),
                        });
                    }
                    values.SOPDocument_upload = getSOPDocument_upload;
                }
                dt_datatable.Dispose();
                msSQL = "select a.sopdepartment_name from ocs_trn_tsopdocumentupload a " +
                    " where sopdepartment_gid='" + sopdepartment_gid + "' group by sopdepartment_gid";
                values.sopdepartment_name = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public void DaDeleteSOPDocument(string sopdocumentupload_gid, MdlMstSOPdepartment_list values)
        {

            msSQL = " delete from ocs_trn_tsopdocumentupload where sopdocumentupload_gid='" + sopdocumentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }
        public void DaGetITflag(string employee_gid, MdlMstSOPdepartment_list values)
        {

            msSQL = " select department_gid from hrm_mst_temployee where employee_gid='"+employee_gid+ "' and department_gid='HDPM1811210059'";
            string lsdepartment_gid = objdbconn.GetExecuteScalar(msSQL);

            if(lsdepartment_gid==""||lsdepartment_gid==null)
            {
                values.department_flag = "N";
            }
            else
            {
                values.department_flag = "Y";
            }
            values.status = true;
        }
    }
}