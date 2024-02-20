using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.osd.Models;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;


namespace ems.osd.DataAccess
{
    public class DaOsdTrnServiceRequest
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGids, msGetGidsactivity, msGetDocumentGid, msGet_ccmemberGid, msGet_RefNo, mspSQL;
        string lspath;
        int mnResult;
        string frommail_id, sub, tomail_id, contactperson, customer_name, body, message, ls_server, ls_username, ls_password, lscontent = string.Empty;
        HttpPostedFile httpPostedFile;
        string lsteam_gid, lsnoofassigned_employee, lsinprogress_count, lsresponse_flag, lsRaised_By, lsRaisedNo, lsBaselocation_Name, lsRaised_Date, lsemployee_mobileno, lslevel_zero, lslevel_one, lsemployee_number;
        string lsrequest_refno, lsactivity_name, reopen_reason, lsrequested_by, request_title, lsraisedbydtl, lsraisedondtl, lscloseddtl, lscancelledbydtl, lscancelledondtl, lsreopenedbydtl, lsreopenedondtl, lstransferondtl, lstransferbydtl, lsrequest_description, lsassigned_membername, assigned_supportteamname, lsrequest_status;
        int ls_port;
        DataTable dt_table;
        string[] lsCCReceipients;
        string cc, commoncc;
        string cc_mailid = string.Empty;
        string lsdatabase = ConfigurationManager.AppSettings["externalportal"].ToString();
        string lsmember_gid, lssupportteam_gid, lssuportteam_name, lsmember_name, lsstatus, count, app_count, lsAssignedmember_gid;
        string lsapprovalforwardflag;
        string lscreated_by, lsemployee_gid;
        string lsRaisedBy, lsraised_department, status_updatedby;
        string lsmodulereportingto_gid;
        string lsAssignTo;
        string lsapproval_status;

        public void DaGetActivityList(actvitydtllist values)
        {
            msSQL = " select activitymaster_gid,activity_code,activity_name from osd_mst_tactivitymaster " +
                    " order by activitymaster_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getActivityList = new List<activitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getActivityList.Add(new activitydtl
                    {
                        activity_name = dt["activity_name"].ToString(),
                        activitymaster_gid = dt["activitymaster_gid"].ToString(),
                        activity_code = dt["activity_code"].ToString(),
                    });
                    values.activitydtl = getActivityList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetDeptActivity(actvitydtllist values, string department_gid)
        {
            msSQL = " select activitymaster_gid,activity_code,activity_name from osd_mst_tactivitymaster where department_gid ='" + department_gid + "'" +
                    " order by activitymaster_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getActivityList = new List<activitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getActivityList.Add(new activitydtl
                    {
                        activity_name = dt["activity_name"].ToString(),
                        activitymaster_gid = dt["activitymaster_gid"].ToString(),
                        activity_code = dt["activity_code"].ToString(),
                    });
                    values.activitydtl = getActivityList;
                }
            }
            dt_datatable.Dispose();
        }



        public bool DaPostDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        MemoryStream ms = new MemoryStream();
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();

                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("VRDO");

                        msSQL = " insert into osd_tmp_tservicereqdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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

                        msSQL = " select tmpservicereqdocument_gid,document_name,document_path from osd_tmp_tservicereqdocument " +
                                " where created_by='" + user_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<upload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            // Create list
                            var file_name = new List<string>();
                            var file_path = string.Empty;

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                file_name.Add(dt["document_name"].ToString());
                                file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                            }
                            objfilename.filename = file_name.ToArray();
                            objfilename.filepath = file_path.ToString();

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new upload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    tmp_documentGid = dt["tmpservicereqdocument_gid"].ToString(),
                                });
                                objfilename.upload_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGettmpDocumentDelete(string tmp_documentGid, result values, uploaddocument objfilename, string user_gid)
        {
            msSQL = " delete from osd_tmp_tservicereqdocument where tmpservicereqdocument_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select tmpservicereqdocument_gid,document_name,document_path from osd_tmp_tservicereqdocument " +
                    " where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new upload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        tmp_documentGid = dt["tmpservicereqdocument_gid"].ToString(),
                    });
                    objfilename.upload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Documents are Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaPostServiceRequest(servicerequest values, string user_gid)
        {
            msSQL = " SELECT  servicerequest_gid, assigned_supportteamgid" +
                    " FROM osd_trn_tservicerequest WHERE activitymaster_gid='" + values.activitymaster_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = " select distinct a.member_gid, a.supportteam_gid from osd_mst_tsupportteam2member a " +
                        " left join osd_mst_tactivitymaster b on a.supportteam_gid = b.supportteam_gid " +
                        " where activitymaster_gid = '" + values.activitymaster_gid + "' and " +
                        " a.member_gid not in (select assigned_membergid from osd_trn_tservicerequest where activitymaster_gid = '" + values.activitymaster_gid + "' ) ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    lsmember_gid = objODBCDatareader["member_gid"].ToString();
                    lssupportteam_gid = objODBCDatareader["supportteam_gid"].ToString();
                    objODBCDatareader.Close();
                }
                else
                {

                    objODBCDatareader.Close();
                    msSQL = " select a.created_date,b.member_gid,a.assigned_supportteamgid from osd_trn_tservicerequest  a " +
                      " left join osd_mst_tsupportteam2member b on a.assigned_membergid = b.member_gid and a.assigned_supportteamgid = b.supportteam_gid  " +
                      " left join osd_mst_tactivitymaster c on c.supportteam_gid = a.assigned_supportteamgid  " +
                      " where c.activitymaster_gid = '" + values.activitymaster_gid + "' " +
                      " order by a.created_date desc limit 1";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lsmember_gid = objODBCDatareader["member_gid"].ToString();

                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.member_gid,count(b.assigned_membergid) as min_count,b.assigned_supportteamgid from osd_mst_tsupportteam2member a " +
                            " left join osd_trn_tservicerequest b on b.assigned_membergid = a.member_gid and b.assigned_supportteamgid = a.supportteam_gid " +
                            " left join osd_mst_tactivitymaster c on c.supportteam_gid = b.assigned_supportteamgid " +
                            " where c.activitymaster_gid = '" + values.activitymaster_gid + "' " +
                            " group by a.member_gid order by assigned_supportteamgid asc  ";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    dt_datatable = objdbconn.GetDataTable(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        lsAssignedmember_gid = objODBCDatareader["member_gid"].ToString();
                        lssupportteam_gid = objODBCDatareader["assigned_supportteamgid"].ToString();
                    }

                    if (dt_datatable.Rows.Count != 0)
                    {
                        string lsflag = "N";
                        foreach (DataRow row in dt_datatable.Rows)
                        {
                            if (lsflag == "Y")
                            {
                                lsAssignTo = row["member_gid"].ToString();
                                break;
                            }
                            else
                            {
                                lsAssignTo = lsAssignedmember_gid;
                            }


                            if (lsmember_gid == row["member_gid"].ToString())
                            {
                                lsflag = "Y";
                                continue;

                            }

                        }

                        lsmember_gid = lsAssignTo;
                    }

                }
            }

            else
            {
                objODBCDatareader.Close();
                msSQL = " select a.member_gid,b.supportteam_gid from osd_mst_tsupportteam2member a " +
                       " left join osd_mst_tactivitymaster b on b.supportteam_gid = a.supportteam_gid " +
                       " where b.activitymaster_gid = '" + values.activitymaster_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsmember_gid = objODBCDatareader["member_gid"].ToString();
                    lssupportteam_gid = objODBCDatareader["supportteam_gid"].ToString();
                }
                objODBCDatareader.Close();

            }

            msSQL = " select team_name,member_name from osd_mst_tsupportteam a " +
                   " left join osd_mst_tsupportteam2member b on a.supportteam_gid = b.supportteam_gid " +
                   " where member_gid = '" + lsmember_gid + "' and a.supportteam_gid = '" + lssupportteam_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lssuportteam_name = objODBCDatareader["team_name"].ToString();
                lsmember_name = objODBCDatareader["member_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select " +
                    " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by," +
                    "  d.department_name  from " +
                    "  adm_mst_tuser b " +
                    "  left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                    " where b.user_gid='" + user_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsRaisedBy = objODBCDatareader["Raised_By"].ToString();
                lsraised_department = objODBCDatareader["department_name"].ToString();
            }
            objODBCDatareader.Close();


            msGetGid = objcmnfunctions.GetMasterGID("SERQ");
            string lsrefno = objdbconn.GetExecuteScalar("select businessunit_prefix from osd_mst_tbusinessunit where businessunit_gid='" + values.department_gid + "'");
            lsrefno += "RRN";
            msGet_RefNo = objcmnfunctions.GetMasterGID(lsrefno);


            HashSet<string> validation = new HashSet<string>();

            validation.Add("E");
            validation.Add("");

            if (String.IsNullOrEmpty(msGetGid) || String.IsNullOrEmpty(msGet_RefNo) || validation.Contains(msGetGid) || validation.Contains(msGet_RefNo))
            {
                values.status = false;
                values.message = "Error Occurred while Raising Service Request..!";
                return;
            }

            string lsrequest_desc = Regex.Replace(values.request_description, "[|\\|/|]", string.Empty);
            string lsactivity_name = Regex.Replace(values.activity_name, "[|\\|/|]", string.Empty);
            string lsrequest_title = Regex.Replace(values.request_title, "[|\\|/|]", string.Empty);

            msSQL = " insert into osd_trn_tservicerequest(" +
             " servicerequest_gid," +
             " request_refno," +
             " department_gid," +
             " department_name," +
             " activitymaster_gid, " +
             " activity_name," +
             " request_title," +
             " request_description," +
             " assigned_supportteamgid," +
             " assigned_supportteamname," +
             " assigned_membergid, " +
             " assigned_membername, " +
             " assigned_status, " +
             " assigned_date, " +
             " request_status," +
             " ticket_source," +
             " created_by," +
             " created_date,raised_by,raised_department)" +
             " values(" +
             "'" + msGetGid + "'," +
             "'" + msGet_RefNo + "'," +
             "'" + values.department_gid + "'," +
             "'" + values.department_name + "'," +
             "'" + values.activitymaster_gid + "', " +
             "'" + lsactivity_name.Replace("'", "\\'") + "'," +
             "'" + lsrequest_title.Replace("'", "\\'") + "'," +
             "'" + lsrequest_desc.Replace("'", "\\'") + "'," +
             "'" + lssupportteam_gid + "'," +
             "'" + lssuportteam_name + "'," +
             "'" + lsmember_gid + "'," +
             "'" + lsmember_name + "'," +
             "'New'," +
              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
             "'Allotted'," +
             "'Internal'," +
             "'" + user_gid + "'," +
             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
             " '" + lsRaisedBy + "','" + lsraised_department + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 0)
            {
                values.status = false;
                values.message = "Error Occurred while Raising Service Request..!";
                return;
            }
            else
            {
                msGetGidsactivity = objcmnfunctions.GetMasterGID("RQTR");
                msSQL = "insert into osd_trn_tservicerequestactivityhistory(" +
                   "servicerequestactivityhistory_gid," +
                   "servicerequest_gid," +
                   "activitymaster_gid," +
                   "activity_name," +
                   "activity_flag," +
                   "created_by," +
                   "created_date)" +
                   "values(" +
                   "'" + msGetGidsactivity + "'," +
                   "'" + msGetGid + "'," +
                   "'" + values.activitymaster_gid + "', " +
                   "'" + values.activity_name.Replace("'", "\\'") + "'," +
                   "'Y'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msGetGids = objcmnfunctions.GetMasterGID("RQTR");
                msSQL = "insert into osd_trn_tpriority(" +
                   "priority_gid," +
                   "servicerequest_gid," +
                   "priority," +
                   "priority_flag," +
                   "created_by," +
                   "created_date)" +
                   "values(" +
                   "'" + msGetGids + "'," +
                   "'" + msGetGid + "'," +
                   "'None'," +
                   "'Y'," +
                   "'" + user_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.tagmemberdtl != null)
                {
                    for (var i = 0; i < values.tagmemberdtl.Count; i++)
                    {
                        msGet_ccmemberGid = objcmnfunctions.GetMasterGID("CCML");

                        msSQL = "Insert into osd_trn_ttaggedmemberlist( " +
                               " tagmemberlist_gid, " +
                               " servicerequest_gid," +
                               " tagmember_gid," +
                               " tagmember_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGet_ccmemberGid + "'," +
                               "'" + msGetGid + "'," +
                               "'" + values.tagmemberdtl[i].employee_gid + "'," +
                               "'" + values.tagmemberdtl[i].employee_name + "'," +
                               "'" + user_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "select tmpservicereqdocument_gid, document_name, document_path from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("SRDO");

                        msSQL = " insert into osd_trn_tservicereqdocument(" +
                         " servicereqdocument_gid," +
                         " servicerequest_gid, " +
                         " document_name," +
                         " document_path," +
                         " created_by," +
                         " created_date)" +
                         " values(" +
                         "'" + msGetDocumentGid + "'," +
                         "'" + msGetGid + "', " +
                         "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                         "'" + dt["document_path"].ToString() + "'," +
                         "'" + user_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                msSQL = "delete from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Service Request Raised Successfully..!";
            }

            try
            {

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    frommail_id = objODBCDatareader["company_mail"].ToString();
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();

                }
                objODBCDatareader.Close();

                msSQL = "select group_concat(distinct employee_emailid) as ccmail_id from osd_trn_ttaggedmemberlist a " +
                    " left join hrm_mst_temployee b on a.tagmember_gid = b.employee_gid where servicerequest_gid='" + msGetGid + "'";
                cc = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + values.department_gid + "'";
                //commoncc = objdbconn.GetExecuteScalar(msSQL);
                dt_table = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dr_datarow in dt_table.Rows)
                {
                    if (dr_datarow["businessunit_emailaddress"].ToString() != null & dr_datarow["businessunit_emailaddress"].ToString() != string.Empty & dr_datarow["businessunit_emailaddress"].ToString() != "")
                    {
                        commoncc += "" + (dr_datarow["businessunit_emailaddress"].ToString()) + ",";
                    }

                }
                dt_table.Dispose();

                commoncc += cc;

                cc = commoncc.TrimEnd(',');

                msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                    " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid where servicerequest_gid='" + msGetGid + "'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select request_refno,activity_name,request_title,getapproval_remarks, a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                      " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as raised_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                      " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date " +
                      " from osd_trn_tservicerequest a " +
                      " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                      " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                      " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                      " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                      " where servicerequest_gid='" + msGetGid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                    lsactivity_name = objODBCDatareader["activity_name"].ToString();
                    request_title = objODBCDatareader["request_title"].ToString();
                    lsraisedbydtl = objODBCDatareader["raised_by"].ToString();
                    lsraisedondtl = objODBCDatareader["created_date"].ToString();

                    lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                    lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                    lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                    lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                    lsrequest_status = objODBCDatareader["request_status"].ToString();
                    assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                    lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                    lsrequest_description = objODBCDatareader["request_description"].ToString();
                    lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();
                }
                objODBCDatareader.Close();
                lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + user_gid + "'");

                msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
              "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
              "  from adm_mst_tmodule2employee a " +
              "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
              "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
              "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
              "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
              "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
              "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
              "  group by a.employeereporting_to ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    lslevel_zero = objODBCDatareader["level_zero"].ToString();
                    lslevel_one = objODBCDatareader["level_one"].ToString();
                    lsemployee_number = objODBCDatareader["employee_number"].ToString();
                    //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    //values.level_zero = objODBCDatareader["level_zero"].ToString();
                    //values.level_one = objODBCDatareader["level_one"].ToString();
                    //values.employee_number = objODBCDatareader["employee_number"].ToString();
                }
                //sub = "New Service Request Assigned ";
                sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  New Service Request Assigned ";

                lscontent = HttpUtility.HtmlEncode(values.content);

                body = "Dear Sir/Madam,  <br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + " A new ticket is assigned to you,the details are as follows,<br />";
                body = body + "<br />";
                body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                body = body + "<br />";
                body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                body = body + "<br />";
                body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                body = body + "<br />";
                body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                body = body + "<br />";
                body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                body = body + "<br />";
                //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                //body = body + "<br />";
                body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                body = body + "<br />";
                body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                body = body + "<br />";
                body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                body = body + "<br />";
                body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                body = body + "<br />";
                body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                body = body + "<br />";
                body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                body = body + "<br />";
                body = body + "<b>Thanks & Regards, </b><br/> ";
                body = body + "<br />";
                body = body + "<b> Team Business Process </b> ";
                body = body + "<br />";

                cc_mailid = "";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail_id));
                //message.CC.Add(cc);


                if (cc != null & cc != string.Empty & cc != "")
                {
                    lsCCReceipients = cc.Split(',');
                    if (cc.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc));
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
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                values.status = true;

                if (values.status == true)
                {
                    msSQL = "Insert into osd_trn_tmailcount( " +
                    " servicerequest_gid," +
                    " from_mail," +
                    " to_mail," +
                    " cc_mail," +
                    " mail_status," +
                    " mail_senddate, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + ls_username + "'," +
                    "'" + tomail_id + "'," +
                    "'" + cc + "'," +
                    "'Service Request Assigned'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + user_gid + "'," +
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

        public void DaGetServiceRequestSummary(servicerequestdtllist values, string user_gid, string employee_gid)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid, request_status, request_refno, completed_flag, " +
                    " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, " +
                    " raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate, cancel_flag, closed_flag, " +
                    " assigned_supportteamname, assigned_membername,a.approvalrequest_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                    " transfer_flag, reopen_flag, rejected_flag from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.created_by='" + user_gid + "' and reopen_flag<>'Y' and bankalert_flag<>'Y'  and e.department_status='Y' and request_status != 'Closed' and request_status != 'Cancelled' and request_status != 'Rejected' order by servicerequest_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestList = new List<servicerequestdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select approvalrequest_flag from osd_trn_tservicerequest where servicerequest_gid= '" + dt["servicerequest_gid"].ToString() + "'";
                    var lsapprovalrequest_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapprovalrequest_flag == "Y")
                    {
                        msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where (approval_status = 'Approved' or " +
                            " approval_status = 'Rejected' or approval_status = 'Cancelled') and approvalrequest_flag = 'Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            " as app_count from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            count = objODBCDatareader["count"].ToString();
                            app_count = objODBCDatareader["app_count"].ToString();
                            if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                            {
                                msSQL = " select count(*) as app_count from osd_trn_trequestapproval where (approval_status = 'Rejected') and" +
                             " approvalrequest_flag='Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                                if (objODBCDatareader.HasRows == true)
                                {
                                    app_count = objODBCDatareader["app_count"].ToString();

                                    msSQL = "select approval_status from osd_trn_trequestapproval where " +
                            " approvalstatus_flag='Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'  and approval_status not in ('Cancelled','Pending')  order by requestapproval_gid desc limit 1";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == true)
                                    {
                                        lsapproval_status = objODBCDatareader["approval_status"].ToString();
                                    }
                                    else { lsapproval_status = ""; }
                                    if (lsapproval_status == null || lsapproval_status == "")
                                    {
                                        lsstatus = "Pending";
                                    }
                                    else 
                                    { 
                                     
                                        if ((Convert.ToInt16(app_count) > 0) && lsapproval_status != "Approved")
                                        {

                                            lsstatus = "Rejected";
                                        }
                                        else
                                        {
                                            lsstatus = "Approved";

                                        }
                                    }
                                    
                                }
                                else
                                {
                                    lsstatus = "Pending";
                                }
                            }
                            else
                            {
                                lsstatus = "Pending";
                            }
                        }
                        else
                        {
                            lsstatus = "Pending";
                        }
                        objODBCDatareader.Close();
               
                    }
                    else
                    {
                        lsstatus = "NotInitiated";
                    }

                    msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where ( approval_status = 'Cancelled') and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                       "as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        count = objODBCDatareader["count"].ToString();
                        app_count = objODBCDatareader["app_count"].ToString();
                        if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                        {

                            lsstatus = "";
                        }
                    }
                    objODBCDatareader.Close();

                    msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where ( approval_status = 'Rejected') and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                      "as app_count  from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        count = objODBCDatareader["count"].ToString();
                        app_count = objODBCDatareader["app_count"].ToString();
                        //if (Convert.ToInt16(app_count) != 0)
                        //{

                        //    lsstatus = "Rejected";
                        //}
                    }
                    objODBCDatareader.Close();

                    msSQL = " SELECT servicerequest_gid FROM osd_trn_tservicerequest " +
                          " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND createdby_flag='Y' and created_by<>'" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }


                    if (dt["request_status1"].ToString() == "Completed" || dt["request_status1"].ToString() == "Reopen-Completed" || dt["request_status1"].ToString() == "Closed" || dt["request_status1"].ToString() == "Reopen-Closed" || dt["request_status"].ToString() == "Cancelled")
                    {
                        lsapprovalrequest_flag = "C";
                    }
                    else
                    {
                        lsapprovalrequest_flag = dt["approvalrequest_flag"].ToString();
                    }

                    getservicerequestList.Add(new servicerequestdtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_description = dt["request_description"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        cancel_flag = dt["cancel_flag"].ToString(),
                        rejected_flag = dt["rejected_flag"].ToString(),
                        closed_flag = dt["closed_flag"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        approval_status = lsstatus,
                        approvalrequest_flag = lsapprovalrequest_flag,
                        response_flag = lsresponse_flag,
                    });
                    values.servicerequestdtl = getservicerequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetServiceRequestView(string servicerequest_gid, servicerequestview values)
        {

            msSQL = " select request_refno,activity_name,request_title,request_description,d.department_name,a.department_name as departmentname,forward_remarks, request_status,a.department_gid, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as raisedby, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate," +
                    " assigned_supportteamname, assigned_membername, completed_flag, completed_remarks, forward_to," +
                    " date_format(a.forward_date,'%d-%m-%Y %h:%i %p') as forward_date, date_format(a.completed_date,'%d-%m-%Y %h:%i %p') as completed_date, " +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as completed_by, closed_flag," +
                    " date_format(a.closed_date,'%d-%m-%Y %h:%i %p') as closed_date, CASE WHEN a.auto_closeflag = 'Y' THEN 'Auto Closed' ELSE " +
                    " concat(f.user_firstname,' ',f.user_lastname,' / ',f.user_code) END as closed_by , " +
                    " date_format(a.cancel_date,'%d-%m-%Y %h:%i %p') as cancel_date, rejected_remarks , " +
                    " date_format(a.rejected_date,'%d-%m-%Y %h:%i %p') as rejected_date, concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as rejected_by , " +
                    " transfer_flag, reopen_reason, date_format(reopened_date,'%d-%m-%Y %h:%i %p') as reopened_date, assigned_status,a.bankalert_flag, reopen_flag, forward_flag, rejected_flag, cancel_flag,h.baselocation_name from osd_trn_tservicerequest a " +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " left join hrm_mst_temployee c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_tdepartment d on d.department_gid = c.department_gid " +
                    " left join adm_mst_tuser e on a.completed_by = e.user_gid " +
                    " left join adm_mst_tuser f on a.closed_by = f.user_gid " +
                    " left join adm_mst_tuser g on a.rejected_by = g.user_gid " +
                    "left join sys_mst_tbaselocation h on h.baselocation_gid=c.baselocation_gid " +
                    " where a.servicerequest_gid='" + servicerequest_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {


                values.activity_name = objODBCDatareader["activity_name"].ToString();
                values.request_title = objODBCDatareader["request_title"].ToString();
                values.request_description = objODBCDatareader["request_description"].ToString();
                values.forward_remarks = objODBCDatareader["forward_remarks"].ToString();
                values.raised_department = objODBCDatareader["department_name"].ToString();
                values.departmentname = objODBCDatareader["departmentname"].ToString();
                values.department_gid = objODBCDatareader["department_gid"].ToString();
                values.request_status = objODBCDatareader["request_status"].ToString();
                values.raised_date = objODBCDatareader["raiseddate"].ToString();
                values.raised_by = objODBCDatareader["raisedby"].ToString();
                values.request_refno = objODBCDatareader["request_refno"].ToString();
                values.assigned_team = objODBCDatareader["assigned_supportteamname"].ToString();
                values.assigned_to = objODBCDatareader["assigned_membername"].ToString();
                values.transfer_flag = objODBCDatareader["transfer_flag"].ToString();
                values.reopen_reason = objODBCDatareader["reopen_reason"].ToString();
                values.reopened_date = objODBCDatareader["reopened_date"].ToString();
                values.assigned_status = objODBCDatareader["assigned_status"].ToString();
                values.reopen_flag = objODBCDatareader["reopen_flag"].ToString();
                values.completed_flag = objODBCDatareader["completed_flag"].ToString();
                values.completed_remarks = objODBCDatareader["completed_remarks"].ToString();
                values.forward_flag = objODBCDatareader["forward_flag"].ToString();
                values.forward_to = objODBCDatareader["forward_to"].ToString();
                values.forward_date = objODBCDatareader["forward_date"].ToString();
                values.completed_by = objODBCDatareader["completed_by"].ToString();
                values.completed_date = objODBCDatareader["completed_date"].ToString();
                values.closed_by = objODBCDatareader["closed_by"].ToString();
                values.closed_date = objODBCDatareader["closed_date"].ToString();
                values.closed_flag = objODBCDatareader["closed_flag"].ToString();
                values.rejected_flag = objODBCDatareader["rejected_flag"].ToString();
                values.cancel_flag = objODBCDatareader["cancel_flag"].ToString();
                values.cancel_date = objODBCDatareader["cancel_date"].ToString();
                values.rejected_date = objODBCDatareader["rejected_date"].ToString();
                values.rejected_remarks = objODBCDatareader["rejected_remarks"].ToString();
                values.rejected_by = objODBCDatareader["rejected_by"].ToString();
                values.baselocation_name = objODBCDatareader["baselocation_name"].ToString();
                values.bankalert_flag = objODBCDatareader["bankalert_flag"].ToString();


            }
            objODBCDatareader.Close();

            msSQL = "select created_by from osd_trn_tservicerequest" +
                 " where servicerequest_gid='" + servicerequest_gid + "'";
            lscreated_by = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select employee_gid from hrm_mst_temployee where user_gid= '" + lscreated_by + "'";
            lsemployee_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
            lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
                    "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  ,b.employee_mobileno " +
                    "  from adm_mst_tmodule2employee a " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                    "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                    "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                    "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
                    "  group by a.employeereporting_to";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {


                values.level_zero = objODBCDatareader["level_zero"].ToString();
                values.level_one = objODBCDatareader["level_one"].ToString();
                values.employee_number = objODBCDatareader["employee_number"].ToString();
                values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select tagmember_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_ttaggedmemberlist a" +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where servicerequest_gid='" + servicerequest_gid + "' order by tagmemberlist_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettagmemberdtl = new List<tagmemberdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettagmemberdtl.Add(new tagmemberdtl
                    {
                        employee_name = dt["tagmember_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });
                    values.tagmemberdtl = gettagmemberdtl;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select servicereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                    " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tservicereqdocument a" +
                    " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where a.servicerequest_gid='" + servicerequest_gid + "' order by servicereqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestdocumentdtl = new List<servicerequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.srfilename = file_name.ToArray();
                values.srfilepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestdocumentdtl.Add(new servicerequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        servicereqdocument_gid = dt["servicereqdocument_gid"].ToString(),
                    });
                    values.servicerequestdocumentdtl = getservicerequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();
            // Reopen Document Details
            msSQL = " select reopenreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
             " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_treopenreqdocument a" +
             " left join adm_mst_tuser b on a.created_by = b.user_gid " +
             " where a.servicerequest_gid='" + servicerequest_gid + "' order by reopenreqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreopendocumentdtl = new List<reopenrequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.reopenfilename = file_name.ToArray();
                values.reopenfilepath = file_path.ToString();
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getreopendocumentdtl.Add(new reopenrequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        reopenreqdocument_gid = dt["reopenreqdocument_gid"].ToString(),
                    });
                    values.reopenrequestdocumentdtl = getreopendocumentdtl;
                }
            }
            dt_datatable.Dispose();

            // Complete Document

            msSQL = " select completereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tcompletereqdocument a" +
                " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                " where a.servicerequest_gid='" + servicerequest_gid + "' order by completereqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcompleterequestdocumentdtl = new List<completerequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {

                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.comfilename = file_name.ToArray();
                values.comfilepath = file_path.ToString();
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcompleterequestdocumentdtl.Add(new completerequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        completereqdocument_gid = dt["completereqdocument_gid"].ToString(),
                    });
                    values.completerequestdocumentdtl = getcompleterequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();

            // requestor communication update
            //msSQL = "update osd_trn_trequestorcommunication set request_flag='' where servicerequest_gid='" + values.servicerequest_gid + "' and request_flag='Y'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "select response_new,requestor_gid from osd_trn_trequestorcommunication where servicerequest_gid='" + servicerequest_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        msSQL = " update osd_trn_trequestorcommunication set response_new='' where response_new='Y' and " +
            //                " servicerequest_gid='" + servicerequest_gid + "'";
            //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //    }
            //dt_datatable.Dispose();
        }

        public void DaGettempdelete(string user_gid, result values)
        {
            msSQL = "delete from osd_tmp_tservicereqdocument where created_by='" + user_gid + "'";
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

        public void DaGetCloseSummary(servicerequestdtllist values, string user_gid)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid, request_status, request_refno, completed_flag, " +
                    " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, " +
                    " raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate, cancel_flag, closed_flag, " +
                    " assigned_supportteamname, assigned_membername,a.approvalrequest_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                    " transfer_flag, reopen_flag, rejected_flag from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.created_by='" + user_gid + "' and reopen_flag<>'Y' and bankalert_flag<>'Y'  and e.department_status='Y' and a.request_status = 'Closed' order by servicerequest_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestList = new List<servicerequestdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestList.Add(new servicerequestdtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_description = dt["request_description"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        cancel_flag = dt["cancel_flag"].ToString(),
                        rejected_flag = dt["rejected_flag"].ToString(),
                        closed_flag = dt["closed_flag"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        approval_status = lsstatus,

                        response_flag = lsresponse_flag,


                    });
                    values.servicerequestdtl = getservicerequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetClosedRequest(string user_gid, string servicerequest_gid, completed values)
        {
            msSQL = " update osd_trn_tservicerequest set request_status='Closed', " +
                    " closed_flag='Y'," +
                    " closed_by='" + user_gid + "'," +
                    " closed_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where servicerequest_gid='" + servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Service Request Closed Successfully..!";

                // Closed Mail Trigger
                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        frommail_id = objODBCDatareader["company_mail"].ToString();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();
                    string lsdepartmentgid;
                    lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + servicerequest_gid + "'");

                    msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                    cc = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                        " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid where servicerequest_gid='" + servicerequest_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select request_refno,activity_name,request_title, a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                          " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as closed_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                          " date_format(a.closed_date,'%d-%m-%Y %h:%i %p') as closed_date ,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date" +
                          " from osd_trn_tservicerequest a " +
                          " left join adm_mst_tuser b on b.user_gid = a.closed_by " +
                         " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                         " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                         " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                          " where servicerequest_gid='" + servicerequest_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsactivity_name = objODBCDatareader["activity_name"].ToString();
                        request_title = objODBCDatareader["request_title"].ToString();

                        lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                        lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                        lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                        lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                        lsrequest_status = objODBCDatareader["request_status"].ToString();
                        assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                        lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                        lsrequest_description = objODBCDatareader["request_description"].ToString();
                        lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();
                        lscloseddtl = objODBCDatareader["closed_by"].ToString() + " / " + objODBCDatareader["closed_date"].ToString();
                    }
                    objODBCDatareader.Close();
                    lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + user_gid + "'");

                    msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                    lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
              "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
              "  from adm_mst_tmodule2employee a " +
              "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
              "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
              "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
              "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
              "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
              "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
              "  group by a.employeereporting_to ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                        lslevel_zero = objODBCDatareader["level_zero"].ToString();
                        lslevel_one = objODBCDatareader["level_one"].ToString();
                        lsemployee_number = objODBCDatareader["employee_number"].ToString();
                        //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                        //values.level_zero = objODBCDatareader["level_zero"].ToString();
                        //values.level_one = objODBCDatareader["level_one"].ToString();
                        //values.employee_number = objODBCDatareader["employee_number"].ToString();
                    }
                    //sub = " Service Request Closed ";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Closed ";

                    lscontent = HttpUtility.HtmlEncode(values.content);

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " Service Request has been Closed and the details are as follows,<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Closed By / On :</b> " + HttpUtility.HtmlEncode(lscloseddtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                    body = body + "<br />";
                    //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                    //body = body + "<br />";
                    body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                    body = body + "<br />";


                    body = body + "<b>Thanks & Regards, </b> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
                    body = body + "<br />";

                    //cc_mailid = "";
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    //message.CC.Add(cc);


                    if (cc != null & cc != string.Empty & cc != "")
                    {
                        lsCCReceipients = cc.Split(',');
                        if (cc.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc));
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into osd_trn_tmailcount( " +
                        " servicerequest_gid," +
                        " from_mail," +
                        " to_mail," +
                        //" cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + servicerequest_gid + "'," +
                        "'" + ls_username + "'," +
                        "'" + tomail_id + "'," +
                        //"'" + cc + "'," +
                        "'Service Request Closed'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "'," +
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
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetTaggedSummary(taggedlist values, string employee_gid)
        {
            msSQL = " select a.servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                   " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, " +
                   " assigned_supportteamname, assigned_membername,raised_by  as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status," +
                   " transfer_flag, reopen_flag from osd_trn_tservicerequest a " +
                   " left join osd_trn_ttaggedmemberlist e on e.servicerequest_gid = a.servicerequest_gid " +
                   " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
                   " where e.tagmember_gid='" + employee_gid + "'  and f.department_status='Y'  order by a.servicerequest_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettaggeddtl = new List<taggeddtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " SELECT servicerequest_gid FROM osd_trn_ttaggedmemberlist " +
                         " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND response_new='Y' and tagmember_gid ='" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }

                    gettaggeddtl.Add(new taggeddtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        response_flag = lsresponse_flag,
                        reopen_flag = dt["reopen_flag"].ToString()
                    });
                    values.taggeddtl = gettaggeddtl;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetServiceRequestCount(string user_gid, string employee_gid, requestcount values)
        {
            msSQL = "select count(*) as request_count from osd_trn_tservicerequest a " +
                 " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
                " where a.created_by='" + user_gid + "' and a.reopen_flag<>'Y' and a.bankalert_flag<>'Y' and a.request_status!= 'Closed' and a.request_status!= 'Cancelled' and a.request_status!= 'Rejected' and f.department_status='Y'";
            values.request_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as tagged_count from osd_trn_ttaggedmemberlist a " +
                " left join osd_trn_tservicerequest b on b.servicerequest_gid = a.servicerequest_gid " +
                  " left join osd_mst_tactivedepartment f on f.department_gid = b.department_gid " +
                     " where a.tagmember_gid = '" + employee_gid + "' and f.department_status='Y'";
            values.tagged_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as forward_count from osd_trn_tservicerequest a " +
               " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
                " where a.forward_flag='Y' and a.forwardto_gid='" + employee_gid + "' and f.department_status='Y'";
            values.forward_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as reopen_count from osd_trn_tservicerequest a " +
                " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid " +
                " where a.created_by='" + user_gid + "' and a.reopen_flag='Y' and f.department_status='Y'";
            values.reopen_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as close_count from  osd_trn_tservicerequest a" +
                " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid" +
                " where a.created_by='" + user_gid + "'  and a.rejected_flag ='Y' and f.department_status='Y' and a.request_status = 'Rejected'";
            values.reject_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as close_count from  osd_trn_tservicerequest a" +
                " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid" +
                " where a.created_by='" + user_gid + "' and reopen_flag<>'Y' and bankalert_flag<>'Y' and f.department_status='Y' and a.request_status = 'Closed'";
            values.close_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) as cancel_count from  osd_trn_tservicerequest a" +
               " left join osd_mst_tactivedepartment f on f.department_gid = a.department_gid" +
               " where a.created_by='" + user_gid + "'  and a.cancel_flag ='Y' and f.department_status='Y' and a.request_status = 'Cancelled'";
            values.cancel_count = objdbconn.GetExecuteScalar(msSQL);


        }

        public void DaGetForwardRequestSummary(forwardrequestdtllist values, string employee_gid)
        {

            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid,request_status,request_refno, " +
                    " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, a.approvalforward_flag, " +
                    " raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                    " assigned_supportteamname, assigned_membername, reopen_flag, bankalert_flag, bankalert2allocated_gid, customer_gid, a.getapproval_flag " +
                    " from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.forwardto_gid='" + employee_gid + "' and e.department_status='Y'  order by servicerequest_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getforwardrequestList = new List<forwardrequestdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select approvalforward_flag from osd_trn_tservicerequest where servicerequest_gid= '" + dt["servicerequest_gid"].ToString() + "'";
                    var lsapprovalforward_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapprovalforward_flag == "Y")
                    {
                        msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where (approval_status = 'Approved' or " +
                            " approval_status = 'Rejected' or approval_status = 'Cancelled') and approvalforward_flag = 'Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            " as app_count from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            count = objODBCDatareader["count"].ToString();
                            app_count = objODBCDatareader["app_count"].ToString();
                            if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                            {
                                msSQL = " select count(*) as app_count from osd_trn_trequestapproval where (approval_status = 'Rejected') and" +
                             " approvalforward_flag='Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                                if (objODBCDatareader.HasRows == true)
                                {
                                    app_count = objODBCDatareader["app_count"].ToString();
                                    if (Convert.ToInt16(app_count) > 0)
                                    {

                                        lsstatus = "Rejected";
                                    }
                                    else
                                    {
                                        lsstatus = "Approved";

                                    }
                                }
                                else
                                {
                                    lsstatus = "Pending";
                                }
                            }
                            else
                            {
                                lsstatus = "Pending";
                            }
                        }
                        else
                        {
                            lsstatus = "Pending";
                        }
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        lsstatus = "NotInitiated";
                    }

                    msSQL = " SELECT servicerequest_gid FROM osd_trn_tservicerequest " +
                            " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND forwardmember_flag='Y' and created_by<>'" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }


                    if (dt["request_status1"].ToString() == "Completed" || dt["request_status1"].ToString() == "Reopen-Completed" || dt["request_status1"].ToString() == "Closed" || dt["request_status1"].ToString() == "Reopen-Closed")
                    {
                        lsapprovalforwardflag = "C";
                    }
                    else
                    {
                        lsapprovalforwardflag = dt["approvalforward_flag"].ToString();
                    }
                    getforwardrequestList.Add(new forwardrequestdtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_description = dt["request_description"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        response_flag = lsresponse_flag,
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        getapproval_flag = dt["getapproval_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        approval_status = lsstatus,
                        approvalforward_flag = lsapprovalforwardflag,
                    });
                    values.forwardrequestdtl = getforwardrequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public bool DaPostRequestViewDocUpload(HttpRequest httpRequest, servicerequestview objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string servicerequest_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


            //MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        string lscompany_document_flag = string.Empty;
                        MemoryStream ms = new MemoryStream();
                        servicerequest_gid = HttpContext.Current.Request.Params["servicerequest_gid"];
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        //MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetDocumentGid = objcmnfunctions.GetMasterGID("SRDO");
                        msSQL = " insert into osd_trn_tservicereqdocument(" +
                                       " servicereqdocument_gid," +
                                      " servicerequest_gid, " +
                                      " document_name," +
                                      " document_path," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + servicerequest_gid + "', " +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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

                        msSQL = " select servicereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tservicereqdocument a" +
                                " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                                " where a.servicerequest_gid='" + servicerequest_gid + "' order by servicereqdocument_gid desc";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getservicerequestdocumentdtl = new List<servicerequestdocumentdtl>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            // Create list
                            var file_name = new List<string>();
                            var file_path = string.Empty;

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                file_name.Add(dt["document_name"].ToString());
                                file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                            }
                            objfilename.filename = file_name.ToArray();
                            objfilename.filepath = file_path.ToString();

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getservicerequestdocumentdtl.Add(new servicerequestdocumentdtl
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    created_date = dt["uploadeddate"].ToString(),
                                    created_by = dt["created_by"].ToString(),
                                    servicereqdocument_gid = dt["servicereqdocument_gid"].ToString(),
                                });
                                objfilename.servicerequestdocumentdtl = getservicerequestdocumentdtl;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetTrnDocumentDelete(string servicereqdocument_gid, result values, servicerequestview objfilename, string user_gid)
        {
            msSQL = " select servicerequest_gid from osd_trn_tservicereqdocument where servicereqdocument_gid='" + servicereqdocument_gid + "'";
            objfilename.servicerequest_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " delete from osd_trn_tservicereqdocument where servicereqdocument_gid='" + servicereqdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select servicereqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                 " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tservicereqdocument a" +
                 " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                 " where a.servicerequest_gid='" + objfilename.servicerequest_gid + "' order by servicereqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestdocumentdtl = new List<servicerequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestdocumentdtl.Add(new servicerequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        servicereqdocument_gid = dt["servicereqdocument_gid"].ToString(),
                    });
                    objfilename.servicerequestdocumentdtl = getservicerequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();

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

        // Forward View Document Upload

        public bool DaPostForwardViewDocUpload(HttpRequest httpRequest, allotteddtl objfilename, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string servicerequest_gid = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            //MemoryStream ms = new MemoryStream();
            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        servicerequest_gid = HttpContext.Current.Request.Params["servicerequest_gid"];
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);


                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/ForwardReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetDocumentGid = objcmnfunctions.GetMasterGID("FRDO");
                        msSQL = " insert into osd_trn_tforwardreqdocument(" +
                                       " forwardreqdocument_gid," +
                                      " servicerequest_gid, " +
                                      " document_name," +
                                      " document_path," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + servicerequest_gid + "', " +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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

                        msSQL = " select forwardreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tforwardreqdocument a" +
                                " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                                " where a.servicerequest_gid='" + servicerequest_gid + "' order by forwardreqdocument_gid desc";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getservicerequestdocumentdtl = new List<forwarddocumentdtl>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            // Create list
                            var file_name = new List<string>();
                            var file_path = string.Empty;

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                file_name.Add(dt["document_name"].ToString());
                                file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                            }
                            objfilename.fwdfilename = file_name.ToArray();
                            objfilename.fwdfilepath = file_path.ToString();

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getservicerequestdocumentdtl.Add(new forwarddocumentdtl
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    created_date = dt["uploadeddate"].ToString(),
                                    created_by = dt["created_by"].ToString(),
                                    forwardreqdocument_gid = dt["forwardreqdocument_gid"].ToString(),
                                });
                                objfilename.forwarddocumentdtl = getservicerequestdocumentdtl;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetTrnForwardDocDelete(string forwardreqdocument_gid, result values, allotteddtl objfilename, string user_gid)
        {
            msSQL = " select servicerequest_gid from osd_trn_tforwardreqdocument where forwardreqdocument_gid='" + forwardreqdocument_gid + "'";
            objfilename.servicerequest_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " delete from osd_trn_tforwardreqdocument where forwardreqdocument_gid='" + forwardreqdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " select forwardreqdocument_gid, document_name,document_path, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as uploadeddate, " +
                 " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_trn_tforwardreqdocument a" +
                 " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                 " where a.servicerequest_gid='" + objfilename.servicerequest_gid + "' order by forwardreqdocument_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getservicerequestdocumentdtl = new List<forwarddocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getservicerequestdocumentdtl.Add(new forwarddocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        created_date = dt["uploadeddate"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        forwardreqdocument_gid = dt["forwardreqdocument_gid"].ToString(),
                    });
                    objfilename.forwarddocumentdtl = getservicerequestdocumentdtl;
                }
            }
            dt_datatable.Dispose();

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

        // Reopen Submit
        public void DaPostReopenRequest(reopenrequest values, string user_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RQRO");

            msSQL = " update osd_trn_tservicerequest set assigned_status='Reopen'," +
                    "reopen_reason = '" + values.reopen_reason + "'," +
                    "reopen_flag = 'Y', " +
                    "request_status = 'Work-In-Progress', " +
                    "getapproval_flag = 'N', " +
                    "requestreopen_gid = '" + msGetGid + "'," +
                    "reopened_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "insert into osd_trn_treqreopenhistory(" +
                "requestreopen_gid," +
                "servicerequest_gid," +
                "reopen_reason," +
                "reopened_by," +
                "reopened_date)" +
                "values(" +
                "'" + msGetGid + "'," +
                "'" + values.servicerequest_gid + "'," +
                "'" + values.reopen_reason + "'," +
                "'" + user_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (values.tagmemberdtl != null)
            {
                for (var i = 0; i < values.tagmemberdtl.Count; i++)
                {
                    msGet_ccmemberGid = objcmnfunctions.GetMasterGID("CCML");

                    msSQL = "Insert into osd_trn_ttaggedmemberlist( " +
                           " tagmemberlist_gid, " +
                           " servicerequest_gid," +
                           " tagmember_gid," +
                           " tagmember_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_ccmemberGid + "'," +
                           "'" + values.servicerequest_gid + "'," +
                           "'" + values.tagmemberdtl[i].employee_gid + "'," +
                           "'" + values.tagmemberdtl[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = "select tmpreopenreqdocument_gid, document_name, document_path from osd_tmp_treopenreqdocument where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetDocumentGid = objcmnfunctions.GetMasterGID("RODC");

                    msSQL = " insert into osd_trn_treopenreqdocument(" +
                     " reopenreqdocument_gid," +
                     " servicerequest_gid, " +
                     " document_name," +
                     " document_path," +
                     " created_by," +
                     " created_date," +
                     " requestreopen_gid)" +
                     " values(" +
                     "'" + msGetDocumentGid + "'," +
                     "'" + values.servicerequest_gid + "', " +
                     "'" + dt["document_name"].ToString().Replace("'", "") + "'," +
                     "'" + dt["document_path"].ToString() + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + msGetGid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            dt_datatable.Dispose();
            // Temp Table Upload Document Delete
            msSQL = "delete from osd_tmp_treopenreqdocument where created_by='" + user_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select servicerequest_gid, approval_type, approval_status,approval_token from osd_trn_trequestapproval where servicerequest_gid='" + values.servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                msSQL = " insert into osd_trn_trequestapprovalhistory select requestapproval_gid, servicerequest_gid, approval_gid, approval_name, approval_remarks, approval_type, approval_status, approved_date, rejected_date, created_by, created_date, approval_token, requestapproval_remarks, seqhierarchy_view, hierary_level, approval_basedon, cancelled_by, cancelled_date, approvalstatus_flag, approvalforward_flag, approvalrequest_flag, approvalreopen_flag from osd_trn_trequestapproval where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();
            msSQL = "delete from osd_trn_trequestapproval where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            // Communication History
            msSQL = "select  requestorcommunication_gid, servicerequest_gid, remarks, response_new, requestor_gid, requesthandler_gid, request_flag, created_by, created_date, document_name, document_path from osd_trn_trequestorcommunication where servicerequest_gid='" + values.servicerequest_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                msSQL = " insert into osd_trn_trequestorcommunicationhistory select requestorcommunication_gid, servicerequest_gid, remarks, response_new, requestor_gid, requesthandler_gid, request_flag, created_by, created_date, document_name, document_path from osd_trn_trequestorcommunication where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();
            msSQL = "delete from osd_trn_trequestorcommunication where servicerequest_gid='" + values.servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request Reopened Successfully..!";

                // Reopen Mail Trigger
                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        frommail_id = objODBCDatareader["company_mail"].ToString();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();
                    string lsdepartmentgid;
                    lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + values.servicerequest_gid + "'");

                    msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                    cc = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                        " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid where servicerequest_gid='" + values.servicerequest_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select request_refno,activity_name,request_title, reopen_reason, a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description, " +
                          " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as reopened_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo,  " +
                          " date_format(a.reopened_date,'%d-%m-%Y %h:%i %p') as reopened_date,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date " +
                          " from osd_trn_tservicerequest a " +
                          " left join adm_mst_tuser b on b.user_gid = a.created_by " +
                          " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                          " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                          " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                          " where servicerequest_gid='" + values.servicerequest_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsactivity_name = objODBCDatareader["activity_name"].ToString();
                        request_title = objODBCDatareader["request_title"].ToString();
                        lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                        lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                        lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                        lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                        lsrequest_status = objODBCDatareader["request_status"].ToString();
                        assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                        lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                        lsrequest_description = objODBCDatareader["request_description"].ToString();

                        reopen_reason = objODBCDatareader["reopen_reason"].ToString();
                        lsreopenedbydtl = objODBCDatareader["reopened_by"].ToString();
                        lsreopenedondtl = objODBCDatareader["reopened_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    //sub = " Service Request Reopened ";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "  Service Request Reopened ";

                    lscontent = HttpUtility.HtmlEncode(values.content);

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " The ticket is reopened, the details are as follows,<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reopened By :</b> " + HttpUtility.HtmlEncode(lsreopenedbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reopened On :</b> " + HttpUtility.HtmlEncode(lsreopenedondtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                    body = body + "<br />";
                    //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                    //body = body + "<br />";
                    body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                    body = body + "<br />";


                    body = body + "Click the link to enter the web portal and respond <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                    body = body + "<br />";
                    body = body + "<b>Thanks & Regards, </b><br/> ";
                    body = body + "<br />";
                    body = body + "<b> Team Business Process </b> ";
                    body = body + "<br />";

                    //cc_mailid = "";
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    //message.CC.Add(cc);


                    if (cc != null & cc != string.Empty & cc != "")
                    {
                        lsCCReceipients = cc.Split(',');
                        if (cc.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc));
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into osd_trn_tmailcount( " +
                        " servicerequest_gid," +
                        " from_mail," +
                        " to_mail," +
                        //" cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.servicerequest_gid + "'," +
                        "'" + ls_username + "'," +
                        "'" + tomail_id + "'," +
                        //"'" + cc + "'," +
                        "'Service Request Reopened'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "'," +
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
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        // Reopen Document Upload
        public bool DaPostReopenDocumentUpload(HttpRequest httpRequest, servicerequestview objfilename, string user_gid)
        {
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            {
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);
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
                        string lscompany_document_flag = string.Empty;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        MemoryStream ms = new MemoryStream();
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        ms.Dispose();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "OSD/ServiceReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = " insert into osd_tmp_treopenreqdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName.Replace("'", "") + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
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
                        msSQL = " select tmpreopenreqdocument_gid,document_name,document_path from osd_tmp_treopenreqdocument " +
                                " where created_by='" + user_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getreopendocumentlist = new List<reopenrequestdocumentdtl>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            // Create list
                            var file_name = new List<string>();
                            var file_path = string.Empty;

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                file_name.Add(dt["document_name"].ToString());
                                file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                            }
                            objfilename.reopfilename = file_name.ToArray();
                            objfilename.reopfilepath = file_path.ToString();

                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getreopendocumentlist.Add(new reopenrequestdocumentdtl
                                {
                                    document_name = dt["document_name"].ToString(),
                                    document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                                    tmp_documentGid = dt["tmpreopenreqdocument_gid"].ToString(),
                                });
                                objfilename.reopenrequestdocumentdtl = getreopendocumentlist;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        // Reopen Document Delete
        public void DaGetReopenDocumentDelete(string tmp_documentGid, result values, servicerequestview objfilename, string user_gid)
        {
            msSQL = " delete from osd_tmp_treopenreqdocument where tmpreopenreqdocument_gid='" + tmp_documentGid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select tmpreopenreqdocument_gid,document_name,document_path from osd_tmp_treopenreqdocument " +
                   " where created_by='" + user_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreopendocumentdtl = new List<reopenrequestdocumentdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                // Create list
                var file_name = new List<string>();
                var file_path = string.Empty;
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    file_name.Add(dt["document_name"].ToString());
                    file_path = objcmnstorage.EncryptData(dt["document_path"].ToString());
                }
                values.filename = file_name.ToArray();
                values.filepath = file_path.ToString();

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getreopendocumentdtl.Add(new reopenrequestdocumentdtl
                    {
                        document_name = dt["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        tmp_documentGid = dt["tmpreopenreqdocument_gid"].ToString(),
                    });
                    objfilename.reopenrequestdocumentdtl = getreopendocumentdtl;
                }
            }
            dt_datatable.Dispose();

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
        // Service Request Delete
        public void DaServiceRequestDelete(servicerequestview values, string servicerequest_gid, string user_gid)
        {

            msSQL = " update osd_trn_tservicerequest set request_status='Cancelled', " +
            " assigned_status='Closed'," +
            " cancel_flag='Y'," +
            " cancel_by='" + user_gid + "'," +
            " cancel_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where servicerequest_gid='" + servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update osd_trn_trequestapproval set approval_status='Cancelled', seqhierarchy_view='N', " +
                   " approval_remarks='Cancelled Remarks'," +
                   " cancelled_by='" + user_gid + "'," +
                   " cancelled_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where servicerequest_gid='" + servicerequest_gid + "' and approval_status='Pending'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Service Request Cancelled Successfully..!";

                // Cancel Mail
                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        frommail_id = objODBCDatareader["company_mail"].ToString();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();
                    string lsdepartmentgid;
                    lsdepartmentgid = objdbconn.GetExecuteScalar("select department_gid from osd_trn_tservicerequest where servicerequest_gid ='" + servicerequest_gid + "'");

                    msSQL = "select businessunit_emailaddress  from osd_mst_tbusinessunit where businessunit_gid='" + lsdepartmentgid + "'";
                    cc = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select b.employee_emailid from osd_trn_tservicerequest a " +
                        " left join hrm_mst_temployee b on a.assigned_membergid = b.employee_gid where servicerequest_gid='" + servicerequest_gid + "'";
                    tomail_id = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select request_refno,activity_name,request_title,a.request_status,a.assigned_supportteamname,a.assigned_membername,a.request_description," +
                          " concat(b.user_firstname, ' ', b.user_lastname, '/', b.user_code) as cancel_by,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as Raised_By,d.employee_mobileno as RaisedNo, " +
                          " date_format(a.cancel_date, '%d-%m-%Y %h:%i %p') as cancel_date,e.baselocation_name as Baselocation_Name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as Raised_Date " +
                          " from osd_trn_tservicerequest a " +
                          " left join adm_mst_tuser b on b.user_gid = a.cancel_by " +
                          " left join adm_mst_tuser c on  a.created_by = c.user_gid " +
                          " left join hrm_mst_temployee d on c.user_gid = d.user_gid " +
                          " left join sys_mst_tbaselocation e on e.baselocation_gid=d.baselocation_gid " +
                          " where servicerequest_gid='" + servicerequest_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsactivity_name = objODBCDatareader["activity_name"].ToString();
                        request_title = objODBCDatareader["request_title"].ToString();
                        lsRaised_By = objODBCDatareader["Raised_By"].ToString();
                        lsRaisedNo = objODBCDatareader["RaisedNo"].ToString();
                        lsBaselocation_Name = objODBCDatareader["Baselocation_Name"].ToString();
                        lsRaised_Date = objODBCDatareader["Raised_Date"].ToString();

                        lsrequest_status = objODBCDatareader["request_status"].ToString();
                        assigned_supportteamname = objODBCDatareader["assigned_supportteamname"].ToString();
                        lsassigned_membername = objODBCDatareader["assigned_membername"].ToString();
                        lsrequest_description = objODBCDatareader["request_description"].ToString();
                        lscancelledbydtl = objODBCDatareader["cancel_by"].ToString();
                        lscancelledondtl = objODBCDatareader["cancel_date"].ToString();
                    }
                    objODBCDatareader.Close();
                    lsemployee_gid = objdbconn.GetExecuteScalar("select employee_gid from hrm_mst_temployee where user_gid ='" + user_gid + "'");

                    msSQL = "select module_gid_parent from adm_mst_tmodule where module_gid in(select modulereportingto_gid from adm_mst_tcompany) ";
                    lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select a.employeereporting_to,f.employee_mobileno as employee_number,b.employee_mobileno,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as level_zero,b.employee_gid, " +
              "  concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one  " +
              "  from adm_mst_tmodule2employee a " +
              "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
              "  left join adm_mst_tprivilege h on h.user_gid = b.user_gid " +
              "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
              "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
              "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
              "  where a.module_gid ='" + lsmodulereportingto_gid + "' and c.user_status = 'Y' and b.employee_gid ='" + lsemployee_gid + "'" +
              "  group by a.employeereporting_to ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                        lslevel_zero = objODBCDatareader["level_zero"].ToString();
                        lslevel_one = objODBCDatareader["level_one"].ToString();
                        lsemployee_number = objODBCDatareader["employee_number"].ToString();
                        //values.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                        //values.level_zero = objODBCDatareader["level_zero"].ToString();
                        //values.level_one = objODBCDatareader["level_one"].ToString();
                        //values.employee_number = objODBCDatareader["employee_number"].ToString();
                    }
                    //sub = " Service Request Cancelled ";
                    sub = " " + HttpUtility.HtmlEncode(lsrequest_refno) + "   Service Request Cancelled ";

                    lscontent = HttpUtility.HtmlEncode(values.content);

                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + " The service ticket is cancelled,the details are as follows,<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Ref No   :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Cancelled By :</b> " + HttpUtility.HtmlEncode(lscancelledbydtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Cancelled On :</b> " + HttpUtility.HtmlEncode(lscancelledondtl) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Raised By   :</b> " + HttpUtility.HtmlEncode(lsRaised_By) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Base Location  :</b> " + HttpUtility.HtmlEncode(lsBaselocation_Name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Raised By Number  :</b> " + HttpUtility.HtmlEncode(lsRaisedNo) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Raised Date :</b> " + lsRaised_Date + "<br />";
                    body = body + "<br />";
                    //body = body + "<b>Reporting To Number :</b> " + lsemployee_number + "<br />";
                    //body = body + "<br />";
                    body = body + "<b>Request Title :</b> " + HttpUtility.HtmlEncode(request_title) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Assigned Team :</b> " + HttpUtility.HtmlEncode(assigned_supportteamname) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Assigned Member :</b> " + HttpUtility.HtmlEncode(lsassigned_membername) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Reporting To :</b> " + HttpUtility.HtmlEncode(lslevel_one) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Status :</b> " + HttpUtility.HtmlEncode(lsrequest_status) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Request Description :</b> " + HttpUtility.HtmlEncode(lsrequest_description) + "<br />";
                    body = body + "<br />";

                    body = body + " click the link to enter the web portal for more details <a href=" + ConfigurationManager.AppSettings["customerqueryurl"].ToString() + "> Click Here</a> <br />";
                    body = body + "<br />";
                    body = body + "<b>Thanks & Regards, </b><br/> ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lscancelledbydtl) + "<br />";
                    body = body + "<br />";

                    //cc_mailid = "";
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    message.To.Add(new MailAddress(tomail_id));
                    //message.CC.Add(cc);


                    if (cc != null & cc != string.Empty & cc != "")
                    {
                        lsCCReceipients = cc.Split(',');
                        if (cc.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc));
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
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into osd_trn_tmailcount( " +
                        " servicerequest_gid," +
                        " from_mail," +
                        " to_mail," +
                        //" cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + servicerequest_gid + "'," +
                        "'" + ls_username + "'," +
                        "'" + tomail_id + "'," +
                        //"'" + cc + "'," +
                        "'Service Request Cancelled'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                catch (Exception ex)
                {
                    values.message = "Request Cancelled Successfully, Mail Not Sent..!";
                    values.status = false;
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!, While Cancel";
            }
        }
        // Tag Employee when reopen

        public void DaGetEmployee(MdlEmployee objemployee, string servicerequest_gid, string employee_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name, " +
                   " b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " LEFT JOIN osd_trn_ttaggedmemberlist c on c.tagmember_gid = b.employee_gid" +
                   " where user_status<>'N' and b.employee_gid NOT IN (select (tagmember_gid) from osd_trn_ttaggedmemberlist where servicerequest_gid='" + servicerequest_gid + "') " +
                   " and b.employee_gid <> (select (employee_gid) from osd_trn_tservicerequest a left join hrm_mst_temployee b on a.created_by = b.user_gid where servicerequest_gid='" + servicerequest_gid + "') " +
                   " and b.employee_gid <> (select (assigned_membergid) from osd_trn_tservicerequest where servicerequest_gid='" + servicerequest_gid + "') " +
                   " and b.employee_gid <> (select if(forwardto_gid is null or forwardto_gid='' ,0,forwardto_gid) from osd_trn_tservicerequest where servicerequest_gid='" + servicerequest_gid + "') " +
                   " order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }

        public void DaGetEmployees(MdlEmployee values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT distinct a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name, " +
                   " b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " LEFT JOIN osd_trn_ttaggedmemberlist c on c.tagmember_gid = b.employee_gid" +
                   " where user_status<>'N' and b.employee_gid <> '" + employee_gid + "'" +
                   " order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.employee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }


        }

        // Reopen Summary
        public void DaGetReopenSummary(reopenreqlist values, string user_gid, string employee_gid)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid, request_status, request_refno, " +
                    " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, " +
                    " raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate, " +
                    " assigned_supportteamname, assigned_membername,a.approvalreopen_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                    " transfer_flag, reopen_flag from osd_trn_tservicerequest a " +
                     " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.created_by='" + user_gid + "' and reopen_flag = 'Y' and e.department_status='Y'  order by servicerequest_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getreopendtl = new List<reopendtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = "select approvalreopen_flag from osd_trn_tservicerequest where servicerequest_gid= '" + dt["servicerequest_gid"].ToString() + "'";
                    var lsapprovalreopen_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapprovalreopen_flag == "Y")
                    {
                        msSQL = "select count(*) as count, (select count(*) from osd_trn_trequestapproval where (approval_status = 'Approved' or " +
                            " approval_status = 'Rejected' or approval_status = 'Cancelled') and approvalreopen_flag = 'Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "')" +
                            " as app_count from osd_trn_trequestapproval where servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            count = objODBCDatareader["count"].ToString();
                            app_count = objODBCDatareader["app_count"].ToString();
                            if (Convert.ToInt16(count) == Convert.ToInt16(app_count) && Convert.ToInt16(count) != 0 && Convert.ToInt16(app_count) != 0)
                            {
                                msSQL = " select count(*) as app_count from osd_trn_trequestapproval where (approval_status = 'Rejected') and" +
                             " approvalreopen_flag='Y' and servicerequest_gid = '" + dt["servicerequest_gid"].ToString() + "'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                                if (objODBCDatareader.HasRows == true)
                                {
                                    app_count = objODBCDatareader["app_count"].ToString();
                                    if (Convert.ToInt16(app_count) > 0)
                                    {

                                        lsstatus = "Rejected";
                                    }
                                    else
                                    {
                                        lsstatus = "Approved";

                                    }
                                }
                                else
                                {
                                    lsstatus = "Pending";
                                }
                            }
                            else
                            {
                                lsstatus = "Pending";
                            }
                        }
                        else
                        {
                            lsstatus = "Pending";
                        }
                        objODBCDatareader.Close();
                    }
                    else
                    {
                        lsstatus = "NotInitiated";
                    }

                    msSQL = " SELECT servicerequest_gid FROM osd_trn_tservicerequest " +
                         " WHERE servicerequest_gid='" + dt["servicerequest_gid"].ToString() + "' AND createdby_flag='Y' and created_by<>'" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "Y";
                    }
                    else
                    {
                        objODBCDatareader.Close();
                        lsresponse_flag = "N";
                    }


                    if (dt["request_status1"].ToString() == "Completed" || dt["request_status1"].ToString() == "Reopen-Completed" || dt["request_status1"].ToString() == "Closed" || dt["request_status1"].ToString() == "Reopen-Closed")
                    {
                        lsapprovalreopen_flag = "C";
                    }
                    else
                    {
                        lsapprovalreopen_flag = dt["approvalreopen_flag"].ToString();
                    }

                    getreopendtl.Add(new reopendtl
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        approval_status = lsstatus,
                        approvalreopen_flag = lsapprovalreopen_flag,
                        response_flag = lsresponse_flag
                    });
                    values.reopendtl = getreopendtl;
                }
            }
            dt_datatable.Dispose();

        }
        //Reject Summary
        public void DaGetRejectedSummary(rejectedreqlist values, string user_gid, string employee_gid)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid, request_status, request_refno, completed_flag, " +
                    " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, " +
                    " raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate, cancel_flag, closed_flag, " +
                    " assigned_supportteamname, assigned_membername,a.approvalrequest_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                    " transfer_flag, reopen_flag, rejected_flag from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.created_by='" + user_gid + "' and a.rejected_flag ='Y' and e.department_status='Y' and a.request_status = 'Rejected' order by servicerequest_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrejectedlist = new List<rejectedlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getrejectedlist.Add(new rejectedlist
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_description = dt["request_description"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        cancel_flag = dt["cancel_flag"].ToString(),
                        rejected_flag = dt["rejected_flag"].ToString(),
                        closed_flag = dt["closed_flag"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        approval_status = lsstatus,

                        response_flag = lsresponse_flag,


                    });
                    values.rejectedlist = getrejectedlist;
                }
            }
            dt_datatable.Dispose();
        }

        //Cancel Summary
        public void DaGetCancelledSummary(cancelledreqlist values, string user_gid, string employee_gid)
        {
            msSQL = " select servicerequest_gid,activity_name,request_title,request_description,a.raised_department as department_name,a.department_name as departmentname,a.department_gid, request_status, request_refno, completed_flag, " +
                    " if(reopen_flag = 'Y', concat('Reopen','-',request_status) ,request_status) as request_status1, " +
                    " raised_by as raisedby,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raiseddate, cancel_flag, closed_flag, " +
                    " assigned_supportteamname, assigned_membername,a.approvalrequest_flag,(select businessactivity_status from osd_mst_tbusinessstatusactivity t where t.servicerequest_gid = a.servicerequest_gid order by created_date desc  limit 1 ) as Businessactivity_Status, " +
                    " transfer_flag, reopen_flag, cancel_flag from osd_trn_tservicerequest a " +
                    " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                    " where a.created_by='" + user_gid + "' and a.cancel_flag ='Y' and e.department_status='Y' and a.request_status = 'Cancelled' order by servicerequest_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcancelledlist = new List<cancelledlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcancelledlist.Add(new cancelledlist
                    {
                        servicerequest_gid = dt["servicerequest_gid"].ToString(),
                        activity_name = dt["activity_name"].ToString(),
                        request_title = dt["request_title"].ToString(),
                        request_description = dt["request_description"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        request_status1 = dt["request_status1"].ToString(),
                        raised_department = dt["department_name"].ToString(),
                        departmentname = dt["departmentname"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        raised_date = dt["raiseddate"].ToString(),
                        raised_by = dt["raisedby"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        assigned_team = dt["assigned_supportteamname"].ToString(),
                        assigned_to = dt["assigned_membername"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        reopen_flag = dt["reopen_flag"].ToString(),
                        cancel_flag = dt["cancel_flag"].ToString(),
                        //rejected_flag = dt["rejected_flag"].ToString(),
                        closed_flag = dt["closed_flag"].ToString(),
                        completed_flag = dt["completed_flag"].ToString(),
                        Businessactivity_Status = dt["Businessactivity_Status"].ToString(),
                        approval_status = lsstatus,
                        response_flag = lsresponse_flag


                    });
                    values.cancelledlist = getcancelledlist;
                }
            }
            dt_datatable.Dispose();
        }

        // Reopen Submit
        public void DaPostTagMemberInChat(reopenrequest values, string user_gid)
        {
            for (var i = 0; i < values.tagmemberdtl.Count; i++)
            {
                msGet_ccmemberGid = objcmnfunctions.GetMasterGID("CCML");

                msSQL = "insert into osd_trn_ttaggedmemberlist( " +
                       " tagmemberlist_gid, " +
                       " servicerequest_gid," +
                       " tagmember_gid," +
                       " tagmember_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGet_ccmemberGid + "'," +
                       "'" + values.servicerequest_gid + "'," +
                       "'" + values.tagmemberdtl[i].employee_gid + "'," +
                       "'" + values.tagmemberdtl[i].employee_name + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request Tagged Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured, While Tagging..!";
            }
        }

        public void DaGetServiceRequestViewUpdate(string servicerequest_gid, servicerequestview values)
        {

            msSQL = " update osd_trn_tservicerequest set " +
                " createdby_flag=''" +
                " where servicerequest_gid='" + servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //if (mnResult != 0)
            //{
            //    values.status = true;
            //    values.message = "Activity Details are Updated Successfully..!";
            //}
            //else
            //{
            //    values.status = false;
            //    values.message = "Error Occured..!";
            //}

        }


        //Query Conversation 
        public bool DaPostSendRequestorCreate(string employee_gid, requestordtl values)
        {
            // Regex pattern = new Regex("\'");

            //Regex pattern = new Regex("\'", "'");
            msGetGid = objcmnfunctions.GetMasterGID("RQCM");
            msSQL = " insert into osd_trn_trequestorcommunication(" +
                    " requestorcommunication_gid," +
                    " servicerequest_gid," +
                    " remarks," +
                    " response_new, " +
                    " requestor_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.servicerequest_gid + "'," +
                    // "'" + pattern.Replace(values.remarks, " ") + "'," +
                    "'" + values.remarks.Replace("'", "\\'") + "'," +
                    //"'" + values.remarks.Replace('\'', ' ' '''', ' ') + "'," +
                    //"'" + REPLACE(values.remarks, '\'', '') + "'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update osd_trn_trequestorcommunication set request_flag='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_tservicerequest set assignedmember_flag='Y', forwardmember_flag='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_ttaggedmemberlist set response_new='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_membertransfer set responce_new='Y' where servicerequest_gid='" + values.servicerequest_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Requestor Communications are Sent Successfully..!";
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
        public void DaGetServiceRequestForwardViewUpdate(string servicerequest_gid, servicerequestview values)
        {

            msSQL = " update osd_trn_tservicerequest set " +
                " forwardmember_flag=''" +
                " where servicerequest_gid='" + servicerequest_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

        //Query Conversation 
        public bool DaPostSendRequestorForward(string employee_gid, requestordtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("RQCM");
            msSQL = " insert into osd_trn_trequestorcommunication(" +
                    " requestorcommunication_gid," +
                    " servicerequest_gid," +
                    " remarks," +
                    " response_new, " +
                    " requestor_gid," +
                    " created_by, " +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.servicerequest_gid + "'," +
                    //"'" + values.remarks + "'," +
                    "'" + values.remarks.Replace("'", "\\'") + "'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = "update osd_trn_trequestorcommunication set request_flag='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_tservicerequest set assignedmember_flag='Y', createdby_flag='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_ttaggedmemberlist set response_new='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update osd_trn_membertransfer set responce_new='Y' where servicerequest_gid='" + values.servicerequest_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Requestor Communications are Sent Successfully..!";
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

        public void DaGetServiceRequestTagViewUpdate(string servicerequest_gid, servicerequestview values, string employee_gid)
        {

            msSQL = " update osd_trn_ttaggedmemberlist set " +
                " response_new=''" +
                " where servicerequest_gid='" + servicerequest_gid + "' and tagmember_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        }

    }
}