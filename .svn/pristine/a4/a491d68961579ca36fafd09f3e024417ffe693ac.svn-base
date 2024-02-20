using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.idas.Models;
using System.Configuration;
using ems.utilities.Functions;


using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasNocAndNdc
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL;
        int mnResult;
        string msGetGid;
        string lspath = string.Empty;
        string lscompany_code = string.Empty;
        HttpPostedFile httpPostedFile;
        string lsnocandndc_date, lsnoc_issuance_date, lssanction_date, lsloan_closure_date;

        //ADD

        public void DaGetIdasNocAndNdc(MdlIdasNocAndNdc objnocandndc)
        {
            try
            {
                msSQL = " SELECT nocandndc_gid,maker_name,checker_name,vertical_gid,vertical_name,customer_name,date_format(a.nocandndc_date,'%d-%m-%Y') as nocandndc_date,date_format(a.loan_closure_date,'%d-%m-%Y') as loan_closure_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                        " FROM ids_trn_tnocandndc a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by nocandndc_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnocandndc_list = new List<nocandndc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnocandndc_list.Add(new nocandndc_list
                        {
                            nocandndc_gid = (dr_datarow["nocandndc_gid"].ToString()),
                            maker_name = (dr_datarow["maker_name"].ToString()),
                            checker_name = (dr_datarow["checker_name"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            nocandndc_date = (dr_datarow["nocandndc_date"].ToString()),
                        });
                    }
                    objnocandndc.nocandndc_list = getnocandndc_list;
                }
                dt_datatable.Dispose();
                objnocandndc.status = true;
            }
            catch
            {
                objnocandndc.status = false;
            }
        }

        public void DaCreateIdasNocAndNdc(nocandndc values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("NOCT");
            msSQL = " insert into ids_trn_tnocandndc(" +
                    " nocandndc_gid," +
                    " maker_gid," +
                    " maker_name," +
                    " checker_gid," +
                    " checker_name," +
                    " nocandndc_date," +
                    " vertical_gid," +
                    " vertical_name," +
                    " customer_name," +
                    " sanction_ref_no," +
                    " sanction_date," +
                    " loan_account_no," +
                    " noc_issuance_date," +
                    " loan_closure_date," +
                    " created_by," +
                    " created_date," +
                    " updated_by," +
                    " updated_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.maker_gid + "'," +
                    "'" + values.maker_name + "'," +
                    "'" + values.checker_gid + "'," +
                    "'" + values.checker_name + "'," +
                    "'" + Convert.ToDateTime(values.nocandndc_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.vertical_gid + "'," +
                    "'" + values.vertical_name + "'," +
                    "'" + values.customer_name + "'," +
                    "'" + values.sanction_ref_no + "'," +
                    "'" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.loan_account_no + "'," +
                    "'" + Convert.ToDateTime(values.noc_issuance_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + Convert.ToDateTime(values.loan_closure_date).ToString("yyyy-MM-dd") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Noc & Ndc Added Successfully";

                msSQL = "update ids_trn_tnocandndcdocument set nocandndc_gid ='" + msGetGid + "' where nocandndc_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.message = "Error Occured While Adding";
                values.status = false;
            }
        }
        public void DaGetDropDown(string employee_gid, MdlDropDown values)
        {
            //Vertical
            msSQL = " SELECT a.vertical_gid,a.vertical_name,vertical_code " +
                    " FROM ocs_mst_tvertical a  where status_log='Y' order by a.vertical_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        vertical_code = (dr_datarow["vertical_code"].ToString()),
                    });
                }
                values.vertical_list = getSegment;
            }
            dt_datatable.Dispose();
        }

        //DOCUMENT

        public void DaGetNocDocumentList(string employee_gid, MdlIdasNocAndNdc values)
        {
            msSQL = "select nocandndcdocument_gid,document_name,document_path,file_name from ids_trn_tnocandndcdocument where " +
                    " nocandndc_gid ='" + employee_gid + "'" + "order by nocandndc_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadDocumentList = new List<UploadNocDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUploadDocumentList.Add(new UploadNocDocumentList
                    {
                        nocandndcdocument_gid = (dr_datarow["nocandndcdocument_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        file_name = (dr_datarow["file_name"].ToString())
                    });
                }
                values.UploadNocDocumentList = getUploadDocumentList;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaGetNocDocumentEditList(string nocandndc_gid, MdlIdasNocAndNdc values)
        {
            msSQL = " select nocandndcdocument_gid ,document_name,file_name,document_path, nocandndc_gid from ids_trn_tnocandndcdocument " +
                    " where nocandndc_gid ='" + nocandndc_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadNocDocumentList = new List<UploadNocDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getUploadNocDocumentList.Add(new UploadNocDocumentList
                    {
                        file_name = dt["file_name"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        nocandndcdocument_gid = dt["nocandndcdocument_gid"].ToString(),
                        nocandndc_gid = dt["nocandndc_gid"].ToString(),
                    });
                    values.UploadNocDocumentList = getUploadNocDocumentList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetNocDocumentTempEditList(string nocandndc_gid, string employee_gid, MdlIdasNocAndNdc values)
        {
            msSQL = " select nocandndcdocument_gid ,document_name,file_name,document_path, nocandndc_gid from ids_trn_tnocandndcdocument " +
                    " where nocandndc_gid = '" + employee_gid + "' or nocandndc_gid ='" + nocandndc_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUploadNocDocumentList = new List<UploadNocDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getUploadNocDocumentList.Add(new UploadNocDocumentList
                    {
                        file_name = dt["file_name"].ToString(),
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        nocandndcdocument_gid = dt["nocandndcdocument_gid"].ToString(),
                        nocandndc_gid = dt["nocandndc_gid"].ToString(),
                    });
                    values.UploadNocDocumentList = getUploadNocDocumentList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaNocDocumentUpload(HttpRequest httpRequest, MdlIdasNocAndNdc objfilename, string user_gid, string employee_gid)
        {
            UploadNocDocumentList objdocumentmodel = new UploadNocDocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string file_name = httpRequest.Form["file_name"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);

            //path = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "Idas/NocDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "Idas/NocDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

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
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Idas/NocDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "Idas/NocDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Idas/NocDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "Idas/NocDocumentUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";


                        msGetGid = objcmnfunctions.GetMasterGID("NOCD");

                        msSQL = " insert into ids_trn_tnocandndcdocument( " +
                                " nocandndcdocument_gid ," +
                                " nocandndc_gid," +
                                " file_name ," +
                                " document_path," +
                                " document_name ," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName.Replace("'", " ") + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + file_name.Replace("'", " ") + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
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
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        //DELETE

        public void DaGetNocDocumentDelete(string nocandndcdocument_gid, MdlIdasNocAndNdc values, string employee_gid, string nocandndc_gid)
        {
            msSQL = " delete from ids_trn_tnocandndcdocument where nocandndcdocument_gid ='" + nocandndcdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetNocDocumentAddDelete(string nocandndcdocument_gid, MdlIdasNocAndNdc objfilename, string employee_gid)
        {
            msSQL = " delete from ids_trn_tnocandndcdocument where nocandndcdocument_gid ='" + nocandndcdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document Deleted Successfully..!";
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured..!";
            }
        }

        public void DaNocandNdcDelete(string nocandndc_gid, result values, MdlIdasNocAndNdc objvalues, string employee_gid, string user_gid)
        {  
            msSQL = " update ids_trn_tnocandndc   set delete_flag='Y'," +
                   " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " deleted_by='" + employee_gid + "'" +
                  " where nocandndc_gid='" + nocandndc_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
  
            if (mnResult != 0)
            {
                msSQL = " delete from ids_trn_tnocandndcdocument where nocandndc_gid ='" + nocandndc_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "NOC & NDC Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaTempClear(string employee_gid, result values)
        {
            msSQL = "delete from ids_trn_tnocandndcdocument where nocandndc_gid='" + employee_gid + "'";
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

       

        //Edit

        public void DaEditNoc(string nocandndc_gid, MdlIdasNocAndNdc values)
        {
            try
            {
                msSQL = " SELECT nocandndc_gid,maker_name,checker_name, maker_gid,checker_gid,customer_name," +
                        " vertical_gid,vertical_name,sanction_ref_no,date_format(sanction_date,'%d-%m-%Y') as sanction_date," +
                        " loan_account_no,date_format(noc_issuance_date,'%d-%m-%Y') as noc_issuance_date," +
                        " date_format(loan_closure_date, '%d-%m-%Y') as loan_closure_date," +
                        " date_format(nocandndc_date,'%d-%m-%Y') as nocandndc_date FROM ids_trn_tnocandndc" +
                        " where nocandndc_gid='" + nocandndc_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.nocandndc_gid = objODBCDatareader["nocandndc_gid"].ToString();
                    values.maker_name = objODBCDatareader["maker_name"].ToString();
                    values.checker_name = objODBCDatareader["checker_name"].ToString();
                    values.maker_gid = objODBCDatareader["maker_gid"].ToString();
                    values.checker_gid = objODBCDatareader["checker_gid"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.sanction_ref_no = objODBCDatareader["sanction_ref_no"].ToString();
                    values.sanction_date = objODBCDatareader["sanction_date"].ToString();
                    values.loan_account_no = objODBCDatareader["loan_account_no"].ToString();
                    values.nocandndc_date = objODBCDatareader["nocandndc_date"].ToString();
                    values.noc_issuance_date = objODBCDatareader["noc_issuance_date"].ToString();
                    values.loan_closure_date = objODBCDatareader["loan_closure_date"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateNoc(string employee_gid, MdlIdasNocAndNdc values)
        {
            msSQL = " select updated_by, updated_date,maker_name, maker_gid, checker_name, checker_gid, customer_name," +
                    "vertical_gid, vertical_name, sanction_ref_no,date_format(loan_closure_date, '%d-%m-%Y') as loan_closure_date," +
                    "date_format(sanction_date,'%d-%m-%Y') as sanction_date, nocandndc_date," +
                    "loan_account_no, date_format(noc_issuance_date,'%d-%m-%Y') as noc_issuance_date, date_format(nocandndc_date,'%d-%m-%Y') as nocandndc_date " +
                    " from ids_trn_tnocandndc where nocandndc_gid='" + values.nocandndc_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();


                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("NOCL");
                    msSQL = " insert into ids_trn_tnocandndclog(" +
                            " nocandndc_loggid," +
                            " nocandndc_gid," +
                            " maker_gid," +
                            " maker_name," +
                            " checker_gid," +
                            " checker_name," +
                            " nocandndc_date," +
                            " vertical_gid," +
                            " vertical_name," +
                            " customer_name," +
                            " sanction_ref_no," +
                            " sanction_date," +
                            " loan_account_no," +
                            " noc_issuance_date," +
                            " loan_closure_date," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.nocandndc_gid + "'," +
                            "'" + objODBCDatareader["maker_gid"].ToString() + "'," +
                            "'" + objODBCDatareader["maker_name"].ToString() + "'," +
                            "'" + objODBCDatareader["checker_gid"].ToString() + "'," +
                            "'" + objODBCDatareader["checker_name"].ToString() + "'," +
                            "'" + objODBCDatareader["nocandndc_date"].ToString() + "'," +
                            "'" + objODBCDatareader["vertical_gid"].ToString() + "'," +
                            "'" + objODBCDatareader["vertical_name"].ToString() + "'," +
                            "'" + objODBCDatareader["customer_name"].ToString() + "'," +
                            "'" + objODBCDatareader["sanction_ref_no"].ToString() + "'," +
                            "'" + objODBCDatareader["sanction_date"].ToString() + "'," +
                            "'" + objODBCDatareader["loan_account_no"].ToString() + "'," +
                            "'" + objODBCDatareader["noc_issuance_date"].ToString() + "'," +
                            "'" + objODBCDatareader["loan_closure_date"].ToString() + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = " select date_format(nocandndc_date,'%d-%m-%Y') as nocandndc_date, date_format(noc_issuance_date,'%d-%m-%Y') as noc_issuance_date,date_format(loan_closure_date, '%d-%m-%Y') as loan_closure_date, " +
                    " date_format(sanction_date, '%d-%m-%Y') as sanction_date from ids_trn_tnocandndc where nocandndc_gid='" + values.nocandndc_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsnocandndc_date = objODBCDatareader["nocandndc_date"].ToString();
                lsnoc_issuance_date = objODBCDatareader["noc_issuance_date"].ToString();
                lssanction_date = objODBCDatareader["sanction_date"].ToString();
                lsloan_closure_date = objODBCDatareader["loan_closure_date"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " update ids_trn_tnocandndc set ";
            if (lsnocandndc_date == values.nocandndc_date)
            {
            }
            else
            {
                msSQL += " nocandndc_date='" + Convert.ToDateTime(values.nocandndc_date).ToString("yyyy-MM-dd") + "',";
            }

            msSQL += " maker_gid='" + values.maker_gid + "'," +
                     " maker_name='" + values.maker_name + "'," +
                     " checker_gid='" + values.checker_gid + "'," +
                     " checker_name='" + values.checker_name + "'," +
                     " vertical_gid='" + values.vertical_gid + "'," +
                     " vertical_name='" + values.vertical_name + "'," +
                     " customer_name='" + values.customer_name + "'," +
                     " sanction_ref_no='" + values.sanction_ref_no + "'," +
                     " loan_account_no='" + values.loan_account_no + "',";
            if (lsnoc_issuance_date == values.noc_issuance_date)
            {
            }
            else
            {
                msSQL += " noc_issuance_date='" + Convert.ToDateTime(values.noc_issuance_date).ToString("yyyy-MM-dd") + "',";
            }
            if (lssanction_date == values.sanction_date)
            {
            }
            else
            {
                msSQL += " sanction_date='" + Convert.ToDateTime(values.sanction_date).ToString("yyyy-MM-dd") + "',";
            }
            if (lsloan_closure_date == values.loan_closure_date)
            {
            }
            else
            {
                msSQL += " loan_closure_date='" + Convert.ToDateTime(values.loan_closure_date).ToString("yyyy-MM-dd") + "',";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where nocandndc_gid='" + values.nocandndc_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Noc & Ndc Updated Successfully";

                msSQL = "update ids_trn_tnocandndcdocument set nocandndc_gid ='" + values.nocandndc_gid + "' where nocandndc_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.message = "Error Occured While Updating";
                values.status = false;
            }
        }

        // Noc & Ndc Report page

        //Report Summary

        public void DaGetIdasNocReportSummary(MdlIdasNocAndNdc objnocandndc)
        {
            try
            {
                msSQL = " SELECT nocandndc_gid,maker_name,checker_name,vertical_gid,vertical_name,customer_name,sanction_ref_no,loan_account_no,date_format(a.nocandndc_date,'%d-%m-%Y') as nocandndc_date," +
                        " date_format(sanction_date, '%d-%m-%Y') as sanction_date,date_format(noc_issuance_date,'%d-%m-%Y') as noc_issuance_date,date_format(a.loan_closure_date,'%d-%m-%Y') as loan_closure_date,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                        " FROM ids_trn_tnocandndc a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by nocandndc_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getnocandndc_list = new List<nocandndc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getnocandndc_list.Add(new nocandndc_list
                        {
                            nocandndc_gid = (dr_datarow["nocandndc_gid"].ToString()),
                            maker_name = (dr_datarow["maker_name"].ToString()),
                            checker_name = (dr_datarow["checker_name"].ToString()),
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            nocandndc_date = (dr_datarow["nocandndc_date"].ToString()),
                            sanction_ref_no = (dr_datarow["sanction_ref_no"].ToString()),
                            loan_account_no = (dr_datarow["loan_account_no"].ToString()),
                            sanction_date = (dr_datarow["sanction_date"].ToString()),
                            loan_closure_date = (dr_datarow["loan_closure_date"].ToString()),  
                            noc_issuance_date = (dr_datarow["noc_issuance_date"].ToString()),
                        });
                    }
                    objnocandndc.nocandndc_list = getnocandndc_list;
                }
                dt_datatable.Dispose();
                objnocandndc.status = true;
            }
            catch
            {
                objnocandndc.status = false;
            }
        }

        //Export Excel

        public void DaExportExcelNoc(MdlIdasNocAndNdc objnocandndc)
        {

            msSQL = " SELECT customer_name as 'Name of the Customer',vertical_name as 'Vertical',sanction_ref_no as 'Sanction Ref number'," +
                    " loan_account_no as 'Loan Account Numbers',date_format(sanction_date, '%d-%m-%Y') as 'Sanction Date', " +
                    " date_format(a.loan_closure_date,'%d-%m-%Y') as 'Loan Closure Date'," +
                    " date_format(a.nocandndc_date,'%d-%m-%Y') as 'NDC NOC Request Date'," +
                    " date_format(noc_issuance_date,'%d-%m-%Y') as 'NOC NDC Issuance Date'," +
                    " maker_name as 'Maker Name',checker_name as 'Checker Name'," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as 'Created By' ," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as 'Created Date' " +
                    " FROM ids_trn_tnocandndc a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.nocandndc_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("NOC & NDC List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objnocandndc.lsname = "NOC & NDC Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Idas/NOC & NDC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objnocandndc.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Idas/NOC & NDC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objnocandndc.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objnocandndc.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 12])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                objnocandndc.lspath =lscompany_code + "/" + "Idas/NOC & NDC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objnocandndc.lsname;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Idas/NOC & NDC Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objnocandndc.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objnocandndc.status = false;
                objnocandndc.message = "Failure";
            }
            objnocandndc.lspath = objcmnstorage.EncryptData(objnocandndc.lspath);
            objnocandndc.status = true;
            objnocandndc.message = "Success";
        }
    }
}
