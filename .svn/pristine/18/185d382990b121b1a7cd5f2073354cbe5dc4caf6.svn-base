using ems.idas.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.idas.DataAccess
{
    public class DaIdasMstDigitalSignature
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGid;
        string msSQL;
        int mnResult;
        string lspath;

        public void DaGetEmployeeList(MdlIdsMstDigitalSignature values)
        {
           
            msSQL =" SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name, " +
                   " b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' AND b.employee_gid NOT IN (SELECT employee_gid FROM ids_mst_tdigitalsignature) order by a.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployeeList = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getemployeeList.Add(new employeelist
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    values.employeelist = getemployeeList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool SignatureUpload(HttpRequest httpRequest, uploadSignature objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string lsemployee_gid = httpRequest.Form["employee_gid"];
            string lsemployee_name = httpRequest.Form["employee_name"];
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/DigitalSignature/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
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
                            return false;
                        }
                        lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/DigitalSignature/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/DigitalSignature/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();
                        lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/DigitalSignature/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("MDSG");

                        msSQL = " insert into ids_mst_tdigitalsignature( " +
                                    " digitalsignature_gid ," +
                                    " employee_gid ," +
                                    " employee_name," +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + lsemployee_gid + "'," +
                                    "'" + lsemployee_name + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Digital Signature Uploaded Successfully";
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
                    objfilename.message = "Kindly Upload Signature";
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetDigitalSignatureList(MdlIdsMstDigitalSignature values)
        {
            msSQL = " select digitalsignature_gid, employee_gid, employee_name, document_name, document_path, date_format(a.created_date,'%d-%m-%Y') as created_date," +
                    " concat(b.user_firstname, b.user_lastname, '/', b.user_code) as created_by" +
                    " from ids_mst_tdigitalsignature a " +
                    " left join adm_mst_tuser b on b.user_gid=a.created_by order by created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdigitalsignaturelist = new List<digitalsignaturelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdigitalsignaturelist.Add(new digitalsignaturelist
                    {
                        digitalsignature_gid = dt["digitalsignature_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                });
                    values.digitalsignaturelist = getdigitalsignaturelist;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteSignature(string digitalsignature_gid, MdlIdsMstDigitalSignature values)
        {
            msSQL = " delete from ids_mst_tdigitalsignature where digitalsignature_gid='"+ digitalsignature_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult == 1)
            {
                values.status = true;
                values.message = "Digital Signature Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Deleting";
            }
        }

        //public void DaGetSignatureView(string digitalsignature_gid, digitalsignaturelist values)
        //{
        //    msSQL = " select digitalsignature_gid, employee_name, document_name, document_path from ids_mst_tdigitalsignature " +
        //            " where digitalsignature_gid='" + digitalsignature_gid + "'";
        //    objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //    if (objODBCDatareader.HasRows == true)
        //    {
        //        values.employee_name = objODBCDatareader["employee_name"].ToString();
        //        values.document_name = objODBCDatareader["document_name"].ToString();
        //        values.document_path = HttpContext.Current.Server.MapPath(objODBCDatareader["document_path"].ToString());
        //    }
        //    objODBCDatareader.Close();
        //}
    }
}