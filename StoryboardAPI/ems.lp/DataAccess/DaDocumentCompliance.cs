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
    public class DaDocumentCompliance
    {
        
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objodbcdatareader;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        HttpPostedFile httpPostedFile;
        string msSQL, msGetGid, msGetDocumentGid;
        int mnresult;
        string lscompany_code, path, lspath;


        public void GetCompliancesummary(mdlRequestcompliance objrequestCompliance, string user_gid)
        {
            try
            {
                msSQL = " select a.requestref_no,concat(a.request_type, ' ', a.others_title) as request_type,date_format(b.tagged_date,'%d-%m-%Y') as created_date,"+
                        " concat(e.user_firstname, ' ', e.user_lastname) as tagged_by,a.requestcompliance_gid,b.requestcompliance2lawyerdtl_gid,request_status "+
                        " from lgl_mst_trequestcompliance a" +
                        " left join lgl_trn_trequestcompliance2lawyerdtl b on a.requestcompliance_gid = b.requestcompliance_gid" +
                        " left join lgl_mst_tlawyeruser c on b.lawyerregister_gid = c.lawyerregister_gid"+
                        " left join hrm_mst_temployee d on b.tagged_by = d.employee_gid"+
                        " left join adm_mst_tuser e on d.user_gid = e.user_gid where c.lawyeruser_gid = '"+user_gid+"' order by a.requestcompliance_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrequestcompliance = new List<requestcompliance_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getrequestcompliance.Add(new requestcompliance_list
                        {
                            requestcompliance_gid = (dr_datarow["requestcompliance_gid"].ToString()),
                            request_type = (dr_datarow["request_type"].ToString()),
                            assigned_date = (dr_datarow["created_date"].ToString()),
                            requestcompliance2lawyerdtl_gid = (dr_datarow["requestcompliance2lawyerdtl_gid"].ToString()),
                            requestref_no = (dr_datarow["requestref_no"].ToString()),
                            assigned_by = (dr_datarow["tagged_by"].ToString()),
                            request_status = (dr_datarow["request_status"].ToString()),
                        });
                    }
                    objrequestCompliance.requestcompliance_list = getrequestcompliance;
                }
                dt_datatable.Dispose();
                objrequestCompliance.status = true;
            }
            catch
            {
                objrequestCompliance.status = false ;
            }
          

        }
        public void GetComplianceManagementSummary(string user_gid, string requestcompliance_gid, mdlRequestcompliance values)
        {
            try
            {
                msSQL = "select requestcompliance_gid,request_type,date_format(assigned_date,'%d-%m-%Y') as assigned_date,b.employee_photo,a.assign_lawyergid,a.seeklawyer_remarks," +
                              " date_format(deadline_date,'%d-%m-%Y') as deadline_date,requestref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as assigned_by," +
                              " d.branch_name,e.department_name,f.designation_name,a.remarks from lgl_mst_trequestcompliance a,hrm_mst_temployee b , adm_mst_tuser c ," +
                              " hrm_mst_tbranch d, hrm_mst_tdepartment e,adm_mst_tdesignation f" +
                              " where a.assigned_by=b.employee_gid and b.user_gid=c.user_gid and  b.branch_gid=d.branch_gid and b.department_gid=e.department_gid" +
                              " and b.designation_gid = f.designation_gid and requestcompliance_gid ='" + requestcompliance_gid + "' order by  a.requestcompliance_gid desc";
                objodbcdatareader = objdbconn.GetDataReader(msSQL);

                if (objodbcdatareader.HasRows)
                {
                    values.requestref_no = objodbcdatareader["requestref_no"].ToString();
                    values.request_type = objodbcdatareader["request_type"].ToString();
                    values.deadline_date = objodbcdatareader["deadline_date"].ToString();
                    values.assigned_date = objodbcdatareader["assigned_date"].ToString();
                    values.assigned_by = objodbcdatareader["assigned_by"].ToString();
                    values.seeklawyer_remarks = objodbcdatareader["seeklawyer_remarks"].ToString();
                    values.requestcompliance_gid = objodbcdatareader["requestcompliance_gid"].ToString();

                }
                objodbcdatareader.Close();

                msSQL = "select a.document_path,a.document_name,a.seekdocument_gid,a.correctedfile_name,a.correctedfile_path,a.lawyer_corrected,a.remarks"+
                    " from lgl_trn_tseeklawyerdocument a"+
                    " left join lgl_mst_trequestcompliance b on a.request_compliancegid=b.requestcompliance_gid where a.request_compliancegid='" + requestcompliance_gid + "' and b.assign_lawyergid='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_fileseekname = new List<document_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_fileseekname.Add(new document_list
                        {
                            file_name = (dr_datarow["document_name"].ToString()),
                            file_path = (dr_datarow["document_path"].ToString()),
                            uploaddocument_gid = (dr_datarow["seekdocument_gid"].ToString()),
                            correctedfile_name = (dr_datarow["correctedfile_name"].ToString()),
                            correctedfile_path = (dr_datarow["correctedfile_path"].ToString()),
                            document_type = dr_datarow["document_path"].ToString(),
                            lawyer_corrected=dr_datarow["lawyer_corrected"].ToString (),
                            remarks = dr_datarow["remarks"].ToString()
                        });
                    }
                    values.document_list = get_fileseekname;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
           
        }


        public void Uploadcorrecteddocument(HttpRequest httpRequest, uploaddocument objfilename, string user_gid)
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
            string project_flag = "Default";
            string uploaddocumet_gid = httpRequest.Form["uploaddocument_gid"].ToString();


            msSQL = "SELECT company_code from adm_mst_tcompany";
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
                        string lsfile_gid = msdocument_gid;
                      
                       
                            lsfile_gid = lsfile_gid + FileExtension;
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);
                            //CopyStream(ms, ls_readStream);
                            lspath = path;
                        ////objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        ////lspath = "../../erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DocumentCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        msSQL = " update lgl_trn_tseeklawyerdocument set  " +
                                    " correctedfile_name='" + httpPostedFile.FileName + "'," +
                                    " correctedfile_path='" + lspath + "' ," +
                                    "lawyer_corrected='" + 'Y' + "'," +
                                    " updated_by='" + user_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " where seekdocument_gid='" + uploaddocumet_gid + "'";

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
                }
            }


            catch (Exception ex)
            {
                dt_datatable.Dispose();

                objfilename.status = false;
                objfilename.message = "Error Occured..!";
            }


        }
        public void Uploadcorrectedremarks(uploaddocument objfilename, string user_gid)
        {
            
            msSQL = " update lgl_trn_tseeklawyerdocument set remarks='" + objfilename.uploadremarks + "'," +
                "correcteddocument_type='" + objfilename.uploaddocument_type + "'" +
                " where seekdocument_gid='" + objfilename.uploaddocument_gid + "'";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                objfilename.status =  true;
            }
            else
            {
                objfilename.status = false;
            }
        }
        public bool DauploadlawyerCorrected_doc(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string project_flag = "Default";
            String path = lspath;
         
            string requestcompliance_gid = httpRequest.Form["requestcompliance_gid"].ToString();


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UECD");
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }


                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/ManageCompliance/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;



                        msGetGid = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into lgl_trn_tlawyeruploaddocument( " +
                                    " lawyerdocument_gid," +
                                    " requestcompliance2lawyerdtl_gid,"+
                                    " requestcompliance_gid ," +
                                    " file_name," +
                                    " file_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'"+ user_gid+"',"+
                                    "'" + user_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                }
                msSQL = " select lawyerdocument_gid,file_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,file_path " +
                     " from lgl_trn_tlawyeruploaddocument a where a.requestcompliance_gid='" + user_gid + "' and a.requestcompliance2lawyerdtl_gid='"+user_gid+"'"+
                     " and created_by='"+user_gid+"'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<upload_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new upload_list
                        {
                            file_name = (dr_datarow["file_name"].ToString()),
                            lawyerdocument_gid = (dr_datarow["lawyerdocument_gid"].ToString()),
                            
                        });
                    }
                    objfilename.upload_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            if (mnresult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document upload Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }

        }
        public void Dadeletecorrecteddo_upload(string lawyerdocument_gid, uploaddocument values)
        {
            msSQL = "delete from lgl_trn_tlawyeruploaddocument where lawyerdocument_gid='" + lawyerdocument_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 1)
            {
                values.status = true;
                values.message = "Document deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleting document";

            }
        }
        public void DasubmitComplianceCorrected_doc(string user_gid, uploaddocument values)
        {
            msSQL = "update lgl_trn_tlawyeruploaddocument set requestcompliance_gid='" + values.requestcompliance_gid + "',"+
                " requestcompliance2lawyerdtl_gid='"+ values.requestcompliance2lawyerdtl_gid + "' where requestcompliance_gid='" + user_gid + "' and requestcompliance2lawyerdtl_gid='" + user_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 1)
            {
                values.status = true;
                values.message = "Document Submited Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured while submiting document";

            }
        }
        public void Dagetcorrecteddocument( string requestcompliance2lawyerdtl_gid,string user_gid, uploaddocument values)
        {
            msSQL = " select lawyerdocument_gid,file_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,file_path " +
                     " from lgl_trn_tlawyeruploaddocument a where "+
                     " requestcompliance2lawyerdtl_gid='"+requestcompliance2lawyerdtl_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new upload_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = (dr_datarow["file_path"].ToString()),
                        lawyerdocument_gid = (dr_datarow["lawyerdocument_gid"].ToString()),
                       
                    });
                }
                values.upload_list = get_filename;
            }
            dt_datatable.Dispose();
            msSQL = " select lawyerdocument_gid,file_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,file_path " +
                     " from lgl_trn_tlawyeruploaddocument a where a.requestcompliance_gid='" + user_gid + "'and "+
                     " requestcompliance2lawyerdtl_gid = '"+ user_gid + "'";;

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getuploaddoc_list = new List<document_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getuploaddoc_list.Add(new document_list
                    {
                        file_name = (dr_datarow["file_name"].ToString()),
                        file_path = (dr_datarow["file_path"].ToString()),
                        lawyerdocument_gid = (dr_datarow["lawyerdocument_gid"].ToString()),

                    });
                }
                values.document_list = getuploaddoc_list;
            }
            dt_datatable.Dispose();
        }
        public void Dagetuploaddoc2lawyer(string user_gid,string requestcompliance2lawyerdtl_gid, MdlTaggedInfo values)
        {
            msSQL = "select a.document_name,a.document_path,a.requestcompliance2lawyerdtl_gid from lgl_trn_tseeklawyerdocument a" +
           " where a.requestcompliance2lawyerdtl_gid='" + requestcompliance2lawyerdtl_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlist = new List<taggeddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlist.Add(new taggeddoc_list
                    {
                        requestcompliance2lawyerdtl_gid = (dr_datarow["requestcompliance2lawyerdtl_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = (dr_datarow["document_path"].ToString()),
                    });
                }
                values.taggeddoc_list = getlist;
                dt_datatable.Dispose();
            }
        }

        public bool Daupdatestatus(string employee_gid, mdlRequestcompliance values)
        {

                   msSQL=  "update lgl_trn_trequestcompliance2lawyerdtl set"+
                            "  request_status='" + values.request_status + "',";
            if (values.request_status == "Rejected")
            {
                msSQL += "rejected_remarks='" + values.rejected_remarks + "',";
            }
            else
            {
                msSQL += "rejected_remarks=null,";
            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where requestcompliance2lawyerdtl_gid='" + values.requestcompliance2lawyerdtl_gid + "'";

            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {


                values.status = true;
                values.message = "Status updated Sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating ";
                return false;
            }

        }


        // Legal Conversation

        public void DaGetLegalDtls(string requestcompliance2lawyerdtl_gid, LegalDtls values)
        {


            msSQL = " select concat(SUBSTR(x.msgconversation,1,128),'...')" +
                    " from lgl_trn_tcompliance2lawyerconversation x" +
                    " where x.requestcompliance2lawyerdtl_gid='" + requestcompliance2lawyerdtl_gid + "'" +
                    " order by created_date Desc limit 0,1";
            values.lastconversation = objdbconn.GetExecuteScalar(msSQL);

            values.lastconversation = (values.lastconversation == "") ? "No Conversation" : values.lastconversation;
           msSQL = " select count(*)" +
                    " from lgl_trn_tcompliance2lawyerconversation x" +
                    " where x.requestcompliance2lawyerdtl_gid='" + requestcompliance2lawyerdtl_gid + "'" +
                    " and msgview_flag='N' and user_flag='Admin'";

            values.newmsg_count  = objdbconn.GetExecuteScalar(msSQL);
            values.newmsg_count = (values.newmsg_count == "") ? "0" : values.newmsg_count;


        }


    }
}