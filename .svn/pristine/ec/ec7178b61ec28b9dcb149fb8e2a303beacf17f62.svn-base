using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Data.Odbc;
using ems.vp.Models;
using ems.utilities.Functions;
using ems.storage.Functions;

namespace ems.vp.DataAccess
{
    public class DaReadyToRelease
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGidREF, msGetGid_i2R;
        string lscompany_code;
        string lspath;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        string msGetGid_CD, msGetGid_HCD,msGetGid_Tmp, msGetGid_doc;
        int mnResult;
        string lsApplicationGid, lsApplicationName, lsVendor, lsVendorGid;
        int i = 0;

        public bool DaGetReleaseData(releaseData objGetReleaseData, string user_gid)
        {
            List<tabledata> getdata = null;
            try
            {
                

                msSQL = " select a.issue_refno,date_format(a.issue_date,'%d-%m-%Y') as issue_date,a.issue_type,a.issue_title,a.priority,a.issue_status,b.team_name, " +
                        " a.issuetracker_gid,a.application_gid,a.issue_remarks,a.Severity,a.priority,a.response_time" +
                        " FROM its_trn_tissuetracker a" +
                        " left join its_trn_tteam b on a.team_gid=b.team_gid" +
                        " WHERE a.issue_status='UAT - Approved' and a.issuetracker_gid not in (select issuetracker_gid from its_trn_tissue2release) and a.application_gid=(SELECT applicationmaster_gid " +
                        " FROM its_mst_tapplicationmaster WHERE application_code=(SELECT vendoruser_code FROM adm_mst_tvendoruser WHERE vendoruser_gid='" + user_gid + "')) order by issue_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    getdata = dt_datatable.AsEnumerable().Select(row =>
                      new tabledata
                      {
                          issuetracker_gid = row["issuetracker_gid"].ToString(),
                          applicationmaster_gid = row["application_gid"].ToString(),
                          application_gid = row["application_gid"].ToString(),
                          issue_refno = row["issue_refno"].ToString(),
                          issue_date = row["issue_date"].ToString(),
                          issue_title = row["issue_title"].ToString(),
                          issue_type = row["issue_type"].ToString(),
                          issue_remarks = row["issue_remarks"].ToString(),
                          Severity = row["Severity"].ToString(),
                          priority = row["priority"].ToString(),
                          response_time = row["response_time"].ToString(),
                          team_name = row["team_name"].ToString(),
                          issue_status = row["issue_status"].ToString()
                      }).ToList();
                    objGetReleaseData.tabledata = getdata;
                }
                dt_datatable.Dispose();

                objGetReleaseData.status = true;
                objGetReleaseData.message = "success";

                return true;
            }
            catch (Exception ex)
            {
                objGetReleaseData.status = false;
                objGetReleaseData.message = "failure";
                return false;
            }

        }
        public bool DaPostRelease(statusUpdate values, string user_gid)
        {

            

            msSQL = " select applicationmaster_gid ,application_name,vendor_gid,vendor_name" +
                   "  from its_mst_tapplicationmaster a" +
                   "  left join adm_mst_tvendoruser b on a.application_code = b.vendoruser_code" +
                   "  where b.vendoruser_gid='" + user_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lsApplicationGid = objODBCDatareader["applicationmaster_gid"].ToString();
                lsApplicationName = objODBCDatareader["application_name"].ToString();
                lsVendor = objODBCDatareader["vendor_name"].ToString();
                lsVendorGid = objODBCDatareader["vendor_gid"].ToString();
            }
            objODBCDatareader.Close();


            msGetGid = objcmnfunctions.GetMasterGID("RELMN");
            msGetGidREF = objcmnfunctions.GetMasterGID("RR");
            msSQL = " Insert into its_trn_trelease (" +
               " release_gid, " +
               " ref_no," +
               " application_gid, " +
               " application, " +
               " release_date, " +
               " vendor_gid, " +
               " vendor, " +
               " release_remarks, " +
               " release_status," +
               " releasesub_status," +
               " created_by, " +
               " done_by," +
               " created_date " +
               " )values(" +
               "'" + msGetGid + "'," +
               "'" + msGetGidREF + "'," +
               "'" + lsApplicationGid + "'," +
               "'" + lsApplicationName + "'," +
               "'" + values.TargetIssuDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
               "'" + lsVendorGid + "'," +
               "'" + lsVendor + "'," +
               "'" + values.StatusRemarks.Replace("\'", "") + "'," +
               "'Ready To Release'," +
               "'Ready To Release'," +
               "'" + user_gid + "'," +
               "'" + values.DoneBy + "'," +
               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            foreach (string i in values.issueGid)
            {
                msSQL = " Update its_trn_tissuetracker SET issue_status='Ready to Release'," +
                    " issuetracker_status='" + values.releaseStatus + "'," +
                    " remarks='" + values.StatusRemarks + "'," +
                    " release_gid='" + msGetGid + "'," +
                    " target_releasedate='" + values.TargetIssuDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " release_remarks='" + values.StatusRemarks + "'" +
                    " WHERE issuetracker_gid='" + i + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid_i2R = objcmnfunctions.GetMasterGID("IS2R");

                msSQL = " insert into its_trn_tissue2release ( " +
                  " issue2release_gid ," +
                  " release_gid, " +
                  " issuetracker_gid," +
                  " created_by," +
                  " created_date ) " +
                  " values(" +
                  "'" + msGetGid_i2R + "'," +
                  "'" + msGetGid + "'," +
                  "'" + i + "'," +
                  "'" + values.DoneBy + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msSQL = " update its_trn_tuattracker set release_gid='" + values.release_gid + "' " +
                            " where issuetracker_gid ='" + i + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGidREF = objcmnfunctions.GetMasterGID("ISRS");
                msSQL = " insert into its_trn_tissuestatuslog (" +
                         " issuestatuslog_gid, " +
                         " issue_status," +
                         " remarks," +
                         " created_date," +
                         " created_by," +
                         " done_by," +
                         " issuetracker_gid)" +
                         "Values(" +
                         "'" + msGetGidREF + "'," +
                         "'" + values.releaseStatus + "'," +
                         "'" + values.StatusRemarks + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                         "'" + user_gid + "'," +
                         "'" + values.DoneBy + "'," +
                         "'" + i + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msGetGid_CD = objcmnfunctions.GetMasterGID("AMCR");
            msSQL = "insert into its_trn_tchangedetails (" +
                " changedtl_gid, " +
                " release_gid," +
                " change_description ," +
                " impacted_module ," +
                " impacted_system ," +
                " reason_change ," +
                " alternative_way ," +
                " resources ," +
                " created_date ," +
                " created_by )" +
                " values (" +
                "'" + msGetGid_CD + "'," +
                "'" + msGetGid + "'," +
                "'" + values.change_description.Replace("\'", "") + "'," +
                "'" + values.impacted_module.Replace("\'", "") + "'," +
                "'" + values.impacted_system.Replace("\'", "") + "'," +
                "'" + values.reasonsfor_change.Replace("\'", "") + "'," +
                "'" + values.alternative_way.Replace("\'", "") + "'," +
                "'" + values.resources.Replace("\'", "") + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid_HCD = objcmnfunctions.GetMasterGID("HCD");
            msSQL = "insert into its_trn_thistorychangedetails (" +
            " historychangedtl_gid, " +
            " release_gid," +
            " change_description ," +
            " impacted_module ," +
            " impacted_system ," +
            " reason_change ," +
            " alternative_way ," +
            " resources ," +
            " created_date ," +
            " created_by )" +
            " values (" +
            "'" + msGetGid_CD + "'," +
            "'" + msGetGid + "'," +
            "'" + values.change_description.Replace("\'", "") + "'," +
            "'" + values.impacted_module.Replace("\'", "") + "'," +
            "'" + values.impacted_system.Replace("\'", "") + "'," +
            "'" + values.reasonsfor_change.Replace("\'", "") + "'," +
            "'" + values.alternative_way.Replace("\'", "") + "'," +
            "'" + values.resources.Replace("\'", "") + "'," +
            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmpuatdocument_gid,document_name,document_path from its_tmp_tuatdocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count!=0)
            {
                foreach( DataRow dt in dt_datatable.Rows)
                {
                    msGetGid_doc = objcmnfunctions.GetMasterGID("UAT");

                    msSQL = " insert into its_trn_uatdocument( " +
                                " uatdocument_gid," +
                                " release_gid," +
                                " document_path," +
                                " document_name," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid_doc + "'," +
                                "'" + msGetGid + "'," +
                                "'" + dt["document_path"] + "'," +
                                "'" + dt["document_name"] + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult==1)
                    {
                        msSQL = "delete from its_tmp_tuatdocument where tmpuatdocument_gid='" + dt["tmpuatdocument_gid"] + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "failure";
                return false;
            }
        }



        public UploadDocumentname DaPostUatDocument(HttpRequest httpRequest, string employee_gid, string user_gid, UploadDocumentname objfilename)  
        {
            UploadDocumentModel objdocumentmodel = new UploadDocumentModel();
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
            String path = lspath;



            msSQL = "SELECT company_code from adm_mst_tcompany where 1=1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lscompany_code = objODBCDatareader["company_code"].ToString();
            }
            objODBCDatareader.Close();
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ITS/UATDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        //httpPostedFile = httpFileCollection[i];
                        //string FileExtension = httpPostedFile.FileName;
                        //string lsfile_gid = msdocument_gid;
                        //FileExtension = Path.GetExtension(FileExtension).ToLower();
                        //if ((FileExtension == ".jpg") || (FileExtension == ".msg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".pdf") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".txt") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".xls") || (FileExtension == ".xlsx"))
                        //{
                        //    lsfile_gid = lsfile_gid + FileExtension;
                        //    ls_readStream = httpPostedFile.InputStream;
                        //    ls_readStream.CopyTo(ms);
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return objfilename;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "ITS/UATDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "ITS/UATDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
 
                            msGetGid_Tmp = objcmnfunctions.GetMasterGID("HUD");

                            msSQL = " insert into its_tmp_tuatdocument( " +
                          " tmpuatdocument_gid ," +
                          " document_path," +
                          " document_name," +
                          " created_by" +
                          " )values(" +
                          "'" + msGetGid_Tmp + "'," +
                          "'" + lspath + "'," +
                          "'" + httpPostedFile.FileName + "'," +
                          "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                objfilename.status = true;
                            }
                            else
                            {
                                objfilename.status = false;
                            }
                        //}
                    }

                    msSQL = "select tmpuatdocument_gid,document_name from its_tmp_tuatdocument where created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<UploadDocumentModel>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentModel
                            {
                                filename = (dr_datarow["document_name"].ToString()),
                                id = (dr_datarow["tmpuatdocument_gid"].ToString())
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
            return objfilename;
        }

        public bool DaPostDocumentCancel(UploadDocumentcancel values)
        {
            
            msSQL = "delete from its_tmp_tuatdocument where tmpuatdocument_gid='" + values.tmpuatdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult!=0)
            {
                values.status = true;
                values.message = "success";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "failure";
                return false;
            }
        }

        public bool DaGetTmpDocumentClear(string user_gid, tmpdocumentclear values)
        {
            
            msSQL = "delete from its_tmp_tuatdocument where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "success";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "failure";
                return false;
            }
        }

        public bool DaGetTmpUatDocument(UploadDocumentname objfilename, string user_gid)
        {
            
            msSQL = "select tmpuatdocument_gid,document_name from its_tmp_tuatdocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentModel>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentModel
                    {
                        filename = (dr_datarow["document_name"].ToString()),
                        id = (dr_datarow["tmpuatdocument_gid"].ToString())
                    });
                }
                objfilename.filename_list = get_filename;
            }
            dt_datatable.Dispose();

            objfilename.status = true;
            return true;
        }

    }
}