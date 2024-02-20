using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.hrloan.Models;
using ems.utilities.Functions;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.storage.Functions;

namespace ems.hrloan.DataAccess
{
    public class DaTrnHRLoanHRVerifications
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGethrTC;
        int mnResult;

        string lstc, lsspdoc, lsreupdoc, lsstatus;
        string lspath;
        HttpPostedFile httpPostedFile;

        int k;
        public string ls_server, lsassignee, ls_username, ls_password, tomail_id, cc_managermailid,tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, cc_Managermailid , cc_approverrmailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsTo2members, lsrequest_refno, lsdepartment_name, lsfintype_name, lsBccmail_id,  lscc2members,  lscreated_date;
        string sToken = string.Empty;
        string lstcflag;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;

        public void DaGetHRloanHRheadVerificationsDetailscount(MdlTrnHRLoanHRVerifications values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = " select count(*) as pendinghrVerify_count from hrl_trn_trequest a where a.request_status = 'HR Approved' or a.request_status = 'HRVerify Pending'  " +
                         "  or a.request_status = 'Reupload Pending' or a.request_status = 'Query Raised By Manager' or a.request_status = 'Reupload Completed' " ;
                values.pendinghrVerify_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as approvedhrVerify_count from hrl_trn_trequest a " +
                        "  where  a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed' ";
                values.approvedhrVerify_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(*) as rejectedhrVerify_count from hrl_trn_trequest a " +
                         "  where a.request_status = 'HRVerify Rejected' ";
                values.rejectedhrVerify_count = objdbconn.GetExecuteScalar(msSQL);
                
            }
        }
        public void DaGetHRloanHRheadVerificationsDetails(MdlTrnHRLoanHRVerifications values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                         "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                         "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head,  a.created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                         "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure ," +                       
                         "  a.drm_status from hrl_trn_trequest a " +
                         " where  a.request_status = 'HR Approved' or a.request_status = 'HRVerify Pending'  " +
                         "  or a.request_status = 'Reupload Pending' or a.request_status = 'Query Raised By Manager' or a.request_status = 'Reupload Completed' " +
                         "  order by a.request_gid desc";

            }

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethverificationsList = new List<verifications_summary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethverificationsList.Add(new verifications_summary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        //Querystatus = dt["Querystatus"].ToString(),

                    });
                    values.verifications_summary = gethverificationsList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRloanHRheadVerificationsDetailsApproved(MdlTrnHRLoanHRVerifications values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                    "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                    "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head, " +
                    "  a.created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure ," +
                    "  a.drm_status from hrl_trn_trequest a " +
                    "  where  a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed' " +
                    "  order by a.request_gid desc";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getVerifyApprovedList = new List<verifications_summary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getVerifyApprovedList.Add(new verifications_summary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),

                    });
                    values.verifications_summary = getVerifyApprovedList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRloanHRheadVerificationsDetailsRejected(MdlTrnHRLoanHRVerifications values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Manager' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                    "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                    "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head, " +
                    "  a.created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                    "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure ," +
                    "  a.drm_status from hrl_trn_trequest a " +
                    "  where a.request_status = 'HRVerify Rejected' " +
                    "  order by a.request_gid desc";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getVerifyRejectedList = new List<verifications_summary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getVerifyRejectedList.Add(new verifications_summary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        drm_status = dt["drm_status"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        functional_head = dt["functional_head"].ToString(),
                        hr_head = dt["hr_head"].ToString(),
                        amount = dt["amount"].ToString(),
                        purpose_name = dt["purpose_name"].ToString(),
                        severity_name = dt["severity_name"].ToString(),
                        tenure = dt["tenure"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        created_date = dt["created_date"].ToString(),

                    });
                    values.verifications_summary = getVerifyRejectedList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRLoanDropDown(string employee_gid, MdlTrnHRLoanHRVerifications values)
        {
            try
            {
                msSQL = " SELECT hrloantermsandconditions_gid, hrloantermsandconditions_name " +
                    " FROM hrl_mst_thrloantermsandconditions  where status='Y' order by hrloantermsandconditions_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getTermsandConditions = new List<hrtermsandconditions_list>();
                if (dt_datatable.Rows.Count != 0)
                {               
                    values.hrtermsandconditions_list = dt_datatable.AsEnumerable().Select(row =>
                         new hrtermsandconditions_list
                         {
                             hrloantermsandconditions_gid = row["hrloantermsandconditions_gid"].ToString(),
                             hrloantermsandconditions_name = row["hrloantermsandconditions_name"].ToString()
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

            msSQL = " SELECT hrdocument_gid, hrdocument_name " +
                    " FROM hrl_mst_thrdocument  where status='Y' order by hrdocument_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocument = new List<hrdocumentname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdocument.Add(new hrdocumentname_list
                    {
                        hrdocument_gid = (dr_datarow["hrdocument_gid"].ToString()),
                        hrdocument_name = (dr_datarow["hrdocument_name"].ToString()),

                    });
                }
                values.hrdocumentname_list = getdocument;
            }
            dt_datatable.Dispose();
        }
        public void DaTempDocumentsList(string employee_gid, MdlHRLoanDocumentUpload1 values)
        {
            msSQL = "delete from hrl_trn_thrspecialdocument where request_gid='" + employee_gid + "'";
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
        public void DaGetUploadList(string request_gid, string employee_gid, MdlHRLoanDocumentUpload1 values)
        {
            msSQL = " select hrspecialdocument_gid,document_id,document_title,document_name,document_path " +
                   " from hrl_trn_thrspecialdocument where request_gid='" + request_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocuments_list = new List<HRDocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDocuments_list.Add(new HRDocument_list
                    {
                        hrspecialdocument_gid = (dr_datarow["hrspecialdocument_gid"].ToString()),
                        document_id = (dr_datarow["document_id"].ToString()),
                        document_title = (dr_datarow["document_title"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.HRDocument_list = getDocuments_list;
                }
                dt_datatable.Dispose();
            }
        }
        public bool DaHRLoanDocumentUpload(HttpRequest httpRequest, uploaddocument1 objfilename, string employee_gid , string user_gid)
        {
            HRDocument_list objdocumentmodel = new HRDocument_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsid_document = httpRequest.Form["document_id"].ToString();
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string request_gid = httpRequest.Form["request_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = "select hrdocument_gid from hrl_mst_thrdocument where " +
                    " hrdocument_name = '" + lsdocument_title.Replace("'", @"\'") + "'";
            string lshrdocument_gid = objdbconn.GetExecuteScalar(msSQL);           

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/HRVerifyDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "HRL/HRVerifyDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "HRL/HRVerifyDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("HSPL");
                        msSQL = " insert into hrl_trn_thrspecialdocument(" +
                                " hrspecialdocument_gid," +
                                " request_gid," +
                                " document_title ," +
                                " hrdocument_gid ," +
                                " document_name," +
                                " document_id," +
                                " document_path," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + request_gid + "'," +
                                "'" + lsdocument_title.Replace("'",@"\'") + "'," +
                                "'" + lshrdocument_gid + "'," +
                                "'" + httpPostedFile.FileName.Replace("'", @"\'") + "'," +
                                "'" + lsid_document.Replace("'",@"\'") + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                       

                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document uploaded successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error occured..!";
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
        public void DaHRLoanDocumentList(string request_gid, string employee_gid, MdlHRLoanDocumentUpload1 values)
        {
            msSQL = " select hrspecialdocument_gid,document_id,document_name,document_path,document_title " +
                 " from hrl_trn_thrspecialdocument where request_gid='" + request_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocuments_list = new List<HRDocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDocuments_list.Add(new HRDocument_list
                    {
                        hrspecialdocument_gid = (dr_datarow["hrspecialdocument_gid"].ToString()),
                        document_id = (dr_datarow["document_id"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        document_title = (dr_datarow["document_title"].ToString()),
                    });

                    values.HRDocument_list = getDocuments_list;
                }
                dt_datatable.Dispose();


            }

        }
        public void DaUploadDocumentsDelete(string hrspecialdocument_gid, MdlHRLoanDocumentUpload1 values)
        {
            msSQL = "delete from hrl_trn_thrspecialdocument where hrspecialdocument_gid='" + hrspecialdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the document";
                values.status = false;

            }
        }
        public void DaPostHrLoantermsandcondtn(MdlTrnHRLoanHRVerifications values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_tmangraisequery where request_gid = '" + values.request_gid + "'" +
              " and raisequery_status = 'Query Raised'";
            values.mangopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.mangopenquerycount == "0")
            {
                msGetGid = objcmnfunctions.GetMasterGID("HTCN");

                msSQL = " insert into hrl_trn_thrtermsandconditions(" +
                            " hrtermsandconditions_gid ," +
                            " request_gid," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.request_gid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                                   " from hrm_mst_temployee a " +
                                   " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                                   " where employee_gid = '" + employee_gid + "'";
                string hrdocverify_approvedbyname = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " update hrl_trn_trequest set request_status ='Reupload Pending'," +
                        " hrdocverify_status='Verified' ," +
                        " hrdocverify_by='" + employee_gid + "'," +
                        " hrdocverify_byname='" + hrdocverify_approvedbyname + "'," +
                        " approved_interest='" + values.approved_interest + "'," +
                         " approved_tenure='" + values.approved_tenure + "'," +
                         " approvedtenure_startdate='" + Convert.ToDateTime(values.approvedtenure_startdate).ToString("yyyy-MM-dd") + "'," +
                         " approvedtenure_enddate='" + Convert.ToDateTime(values.approvedtenure_enddate).ToString("yyyy-MM-dd") + "'," +
                        " hrdocverify_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                try
                {

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select  group_concat(distinct a.reportingmgr_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.employee_gid, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                            " where a.request_gid = '" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsTo2members = objODBCDatareader["employee_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                            " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);
                    
                    //msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                           " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                           " where hrmapping_name = 'Manager')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "Employee Financial Assistance repayment and T&C details";
                    body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name) + ",";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " Your financial assistance request has been approved.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Login in one.samunnati.com and check the repayment and T&C details";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["HRLoanbcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into hrl_trn_thrloanmailcount( " +
                        " request_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.request_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                           "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }

                foreach (string i in values.termsandconditionslist_gid)
                {
                    string lshrloantermsandconditions_name;
                    msSQL = " SELECT  hrloantermsandconditions_name " +
                        " FROM hrl_mst_thrloantermsandconditions  where hrloantermsandconditions_gid='" + i + "'";
                    lshrloantermsandconditions_name = objdbconn.GetExecuteScalar(msSQL);


                    msGethrTC = objcmnfunctions.GetMasterGID("HTAC");
                    msSQL = "Insert into hrl_trn_thrtermsandconditions2( " +
                           " hrtermsandconditions2_gid, " +
                           " hrtermsandconditions_gid," +
                           " hrloantermsandconditions_gid," +
                           " hrloantermsandconditions_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGethrTC + "'," +
                           "'" + msGetGid + "'," +
                           "'" + i + "'," +
                           "'" + lshrloantermsandconditions_name.Replace("'", @"\'") + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Terms and Conditions submitted successfully";
                    }
                    else
                    {
                        values.message = "Error occured while adding";
                        values.status = false;
                    }
                }
            }
            else
            {
                values.status = false;
                values.message = "Approval can't be done,the query is still open";
            }
        }
        public void Datermsandcondtnview(Mdltermsandcondt objtermsandcondt, string request_gid)
        {
            try
            {
                msSQL = " select a.hrloantermsandconditions_gid,a.hrloantermsandconditions_name from hrl_trn_thrtermsandconditions2 a "+
                        " left join hrl_trn_thrtermsandconditions b on b.hrtermsandconditions_gid = a.hrtermsandconditions_gid "+
                        " where b.request_gid = '" + request_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_termsandconditions = new List<hrtermsandconditions_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objtermsandcondt.hrtermsandconditions_list = dt_datatable.AsEnumerable().Select(row =>
                      new hrtermsandconditions_list
                      {
                          hrloantermsandconditions_gid = row["hrloantermsandconditions_gid"].ToString(),
                          hrloantermsandconditions_name = row["hrloantermsandconditions_name"].ToString(),                          
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objtermsandcondt.status = true;
            }
            catch (Exception ex)
            {
                objtermsandcondt.status = false;
            }


        }
        public void DaGetTCflag(Mdltermsandcondt objtermsandcondt, string request_gid)
        {
           
            try
            {
                msSQL = " select count(*) as tc_flag from hrl_trn_thrtermsandconditions " +
    
                        " where request_gid = '" + request_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lstcflag = objODBCDatareader["tc_flag"].ToString();
                 
                }
                objODBCDatareader.Close();
                if(lstcflag =="0")
                {
                    objtermsandcondt.tc_flag = "N";
                }
                else
                {
                    objtermsandcondt.tc_flag = "Y";
                }
                objtermsandcondt.status = true;
            }
            catch (Exception ex)
            {
                objtermsandcondt.status = false;
            }


        }
        public bool DaPostHrLoanHRVerifyApprovalUpdate(MdlTrnHRLoanHRVerifications values, string employee_gid)
        {           
            msSQL = " select count(*) as openquery from hrl_apr_tmangraisequery where request_gid = '" + values.request_gid + "'" +
              " and raisequery_status = 'Query Raised'";
            values.mangopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.mangopenquerycount == "0")
            {
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string hrverify_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='HRVerify Approved'," +
                        " hrverify_status='Approved' ," +
                        " hrverify_remarks='" + values.hrverify_remarks.Replace("'", @"\'") + "' ," +
                        " hrverify_approvedby='" + employee_gid + "'," +
                        " hrverify_approvedbyname='" + hrverify_approvedbyname + "'," +
                        " request_status ='HRVerify Approved'," +
                        " hrverify_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "HRVerify approved successfully";
                try
                {

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select  group_concat(distinct a.employee_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.employee_gid, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                            " where a.request_gid = '" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsTo2members = objODBCDatareader["employee_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                            " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    //msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                    //        " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    //lsto_mail = objdbconn.GetExecuteScalar(msSQL);
                    lsto_mail = ConfigurationManager.AppSettings["HRVerifier_ToMail"].ToString();
                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                     " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                     " where hrmapping_name = 'Manager')";
                    cc_Managermailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                             " where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);

                    if (cc_Managermailid != null & cc_Managermailid != string.Empty & cc_Managermailid != "")
                    {
                        cc_mailid = cc_mailid + "," + cc_Managermailid;
                    }

                    //msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                    //       " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    //       " where hrmapping_name = 'Manager')";
                    //cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "Employee Financial Assistance repayment and T&C details";
                    body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name) + ",";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " Soft copies of financial assistance request is approved. Please courier the originals";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["HRLoanbcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');

                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into hrl_trn_thrloanmailcount( " +
                        " request_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.request_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                        "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
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
                values.message = "Approval can't be done,the query is still open";

            }
            return true;
        }
        public bool DaPostHrLoanHRVerifyRejectUpdate(MdlTrnHRLoanHRVerifications values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_tmangraisequery where request_gid = '" + values.request_gid + "'" +
              " and raisequery_status = 'Query Raised'";
            values.mangopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.mangopenquerycount == "0")
            {
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string hrverify_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='HRVerify Rejected'," +
                        " hrverify_status ='Rejected' ," +
                        " hrverify_remarks='" + values.hrverify_remarks.Replace("'", @"\'") + "' ," +
                        " hrverify_approvedby='" + employee_gid + "'," +
                        " hrverify_approvedbyname='" + hrverify_approvedbyname + "'," +
                        " request_status='HRVerify Rejected'," +
                        " hrverify_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "HRVerify rejected successfully";
                try
                {

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select  group_concat(distinct a.reportingmgr_gid,',',a.functionalhead_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.employee_gid, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                            " where a.request_gid = '" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsTo2members = objODBCDatareader["employee_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                            " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;



                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                           " where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                            " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                            " where hrmapping_name = 'Manager')";
                    cc_managermailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                            " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                            " where hrmapping_name = 'Approver')";
                    cc_approverrmailid = objdbconn.GetExecuteScalar(msSQL);

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        if (cc_managermailid != null & cc_managermailid != string.Empty & cc_managermailid != "")
                        {
                            if (cc_approverrmailid != null & cc_approverrmailid != string.Empty & cc_approverrmailid != "")
                            {
                                cc_mailid = cc_mailid + "," + cc_managermailid + "," + cc_approverrmailid;
                            }
                            else
                            {
                                cc_mailid = cc_mailid + "," + cc_managermailid;
                            }
                        }
                        else
                        {
                            cc_mailid = cc_mailid;
                        }
                    }
                    else
                    {
                        cc_mailid = "";
                    }
                   
                    sub = "Employee Financial Assistance repayment and T&C details";
                    body = "Dear " + HttpUtility.HtmlEncode(lsemployee_name) + ",";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " Soft copies of financial assistance request is Rejected. ";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["HRLoanbcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into hrl_trn_thrloanmailcount( " +
                        " request_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.request_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                        "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
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
                values.message = "Approval can't be done,the query is still open";

            }            
                return true;

        }
        public bool DaPostHrLoanVerifyApprovalUpdate(MdlTrnHRLoanHRVerifications values, string employee_gid)
        {
            msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                " from hrm_mst_temployee a " +
                " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                " where employee_gid = '" + employee_gid + "'";
            string hrdocverify_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update hrl_trn_trequest set request_status ='Reupload Pending'," +
                    " hrdocverify_status='Verified' ," +                   
                    " hrdocverify_by='" + employee_gid + "'," +
                    " hrdocverify_byname='" + hrdocverify_approvedbyname + "'," +                   
                    " hrdocverify_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where request_gid='" + values.request_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            values.message = "HRDoc verified successfully";
            return true;
        }
        public void DaPostManagerRaiseQuery(MdlTrnHRLoanHRVerifications values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("HRMN");
            msSQL = "Insert into hrl_apr_tmangraisequery( " +
                   " mangraisequery_gid, " +
                   " request_gid," +
                   " query_title, " +
                   " query_description," +
                   " raisequery_status, " +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.request_gid + "', " +
                   "'" + values.query_title.Replace("'", @"\'") + "'," +
                   "'" + values.query_description.Replace("'", @"\'") + "'," +
                   "'Query Raised'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = " update hrl_trn_trequest set " +
                        " raisequery_status='Open', " +
                        " request_status='Query Raised By Manager'" +
                        " where request_gid='" + values.request_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Query raised successfully";
                try
                {

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL =" select  group_concat(distinct a.employee_gid)  as CC2members,"+
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as hrverify_approvedbyname, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.hrverify_approvedby = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.hrverify_approvedby = b.employee_gid" +
                           " left join hrm_mst_temployee f on a.employee_gid = f.employee_gid" +
                           " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +                          
                           " where a.request_gid ='" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsfintype_name = objODBCDatareader["fintype_name"].ToString();
                        lsdepartment_name = objODBCDatareader["department_name"].ToString();
                        lsemployee_name = objODBCDatareader["employee_name"].ToString();            
                        lsTo2members = objODBCDatareader["employee_gid"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                            " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee " +
                            " where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                   // msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                      msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                          " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                          " where hrmapping_name = 'Manager')";
                    
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = " Query Raised ";
                    body = "Dear Sir/Madam,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Query has been raised. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Reference Number :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                    body = body + "<br />";                    
                    body = body + "<b> Request By :</b> " + HttpUtility.HtmlEncode(lsemployee_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Department :</b> " + HttpUtility.HtmlEncode(lsdepartment_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Financial Type :</b> " + HttpUtility.HtmlEncode(lsfintype_name) + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["HRLoanbcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;
                    if (values.status == true)
                    {
                        msSQL = "Insert into hrl_trn_thrloanmailcount( " +
                        " request_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.request_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Raised Successfully'," +
                           "'" + msGetGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
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
                values.message = "Error occured while adding";
                values.status = false;
            }
        }
        public void DaGetManagerRaiseQuery(string request_gid, MdlTrnHRLoanHRVerifications values)
        {
            msSQL = " select a.request_gid,a.mangraisequery_gid,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from hrl_apr_tmangraisequery a " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.request_gid = '" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmangraisequery_list = new List<mangraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmangraisequery_list.Add(new mangraisequery_list
                    {
                        request_gid = (dr_datarow["request_gid"].ToString()),
                        mangraisequery_gid = (dr_datarow["mangraisequery_gid"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.mangraisequery_list = getmangraisequery_list;
            }
            dt_datatable.Dispose();
        }
        public void DaPostManagerresponsequery(MdlTrnHRLoanHRVerifications values, string employee_gid)
        {

            msSQL = " update hrl_apr_tmangraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", @"\'") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " raisequery_status='Closed' " +
                   " where mangraisequery_gid='" + values.mangraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            
            if (mnResult == 1)
            {
                msSQL = " select count(*) as request_gid from hrl_trn_thrtermsandconditions where request_gid = '" + values.request_gid + "'";

                lstc = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(*) as request_gid from hrl_trn_thrspecialdocument where request_gid = '" + values.request_gid + "'";

                lsspdoc = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " select count(*) as request_gid from hrl_trn_thrreuploaddocument where request_gid = '" + values.request_gid + "'";

                lsreupdoc = objdbconn.GetExecuteScalar(msSQL);
               if(lstc!="0" && lsspdoc!="0" && lsreupdoc != "0")
                {
                    lsstatus = "Reupload Completed";
                }
               else if(lstc != "0" && lsspdoc != "0" && lsreupdoc=="0"){
                    lsstatus = "Reupload Pending";
                }
                else if (lstc== "0" && lsspdoc== "0" && lsreupdoc == "0")
                {
                    lsstatus = "HRVerify Pending";
                }
                msSQL = " select count(*) as openquery from hrl_apr_tmangraisequery where request_gid = '" + values.request_gid + "'" +
              " and raisequery_status = 'Query Raised'";
                values.mangopenquerycount = objdbconn.GetExecuteScalar(msSQL);
                if (values.mangopenquerycount == "0")
                {
                    msSQL = " update hrl_trn_trequest set " +
                       " raisequery_status='Closed' ," +
                       " request_status='" + lsstatus + "' " +
                       " where request_gid='" + values.request_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Query closed successfully..!";
                try
                {

                    k = 1;

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select  group_concat(distinct d.employee_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as hrverify_approvedbyname, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.hrverify_approvedby = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.hrverify_approvedby = b.employee_gid" +
                           " left join hrm_mst_temployee f on a.employee_gid = f.employee_gid" +
                           " left join adm_mst_tuser c on c.user_gid = b.user_gid  " +
                           " where a.request_gid ='" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsfintype_name = objODBCDatareader["fintype_name"].ToString();
                        lsdepartment_name = objODBCDatareader["department_name"].ToString();
                        lsemployee_name = objODBCDatareader["employee_name"].ToString();
                        lsTo2members = objODBCDatareader["employee_gid"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select query_title from hrl_apr_tmangraisequery  " +
                          " where request_gid = '" + values.request_gid + "'";
                    string lsquery_title = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                            " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                            " where employee_gid = '" + employee_gid + "'";
                    string employee_name = objdbconn.GetExecuteScalar(msSQL);

                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    k = k + 1;
                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                          " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                          " where hrmapping_name = 'Manager')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "   select group_concat(employee_emailid)  from hrm_mst_temployee   where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = " Query Response ";
                    body = "Dear Sir/Madam,<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "Query has been response. The details are as follows, <br />";
                    body = body + "<br />";
                    body = body + "<b> Reference Number :</b> " + HttpUtility.HtmlEncode(lsrequest_refno) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Request By :</b> " + HttpUtility.HtmlEncode(lsemployee_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Department :</b> " + HttpUtility.HtmlEncode(lsdepartment_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Financial Type :</b> " + HttpUtility.HtmlEncode(lsfintype_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Query Title :</b> " + HttpUtility.HtmlEncode(lsquery_title) + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly log into systems to view more details.";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Thanks & Regards, ";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name);
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + " **This is an automated e-mail. Please do not reply to this mailbox**";


                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(ls_username);
                    //message.To.Add(new MailAddress(lsto_mail));


                    lsBccmail_id = ConfigurationManager.AppSettings["HRLoanbcc"].ToString();

                    if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                    {
                        lsBCCReceipients = lsBccmail_id.Split(',');
                        if (lsBccmail_id.Length == 0)
                        {
                            message.Bcc.Add(new MailAddress(lsBccmail_id));
                        }
                        else
                        {
                            foreach (string BCCEmail in lsBCCReceipients)
                            {
                                message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                            }
                        }
                    }
                    if (lsto_mail != null & lsto_mail != string.Empty & lsto_mail != "")
                    {
                        lsToReceipients = lsto_mail.Split(',');
                        if (lsto_mail.Length == 0)
                        {
                            message.To.Add(new MailAddress(lsto_mail));
                        }
                        else
                        {
                            foreach (string ToEmail in lsToReceipients)
                            {
                                message.To.Add(new MailAddress(ToEmail)); //Adding Multiple To email Id
                            }
                        }
                    }

                    if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                    {
                        lsCCReceipients = cc_mailid.Split(',');
                        if (cc_mailid.Length == 0)
                        {
                            message.CC.Add(new MailAddress(cc_mailid));
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into hrl_trn_thrloanmailcount( " +
                        " request_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                          " raisequery_gid, " +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.request_gid + "'," +
                        "'" + employee_name + "'," +
                        "'" + lsto_mail + "'," +
                        "'" + cc_mailid + "'," +
                        "'Query Close Successfully'," +
                           "'" + values.mangraisequery_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
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
                values.message = "Error occured..!";
            }
        }
        public bool DaHRLoanPaymentDocumentUpload(HttpRequest httpRequest, uploadpaymentdocument objfilename, string employee_gid, string user_gid)
        {
            PaymentDocument_list objdocumentmodel = new PaymentDocument_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;            
            string request_gid = httpRequest.Form["request_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/HRRepaymentDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "HRL/HRRepaymentDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "HRL/HRRepaymentDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("HRPY");
                        msSQL = " insert into hrl_trn_thrrepaymentdocument(" +
                                " hrrepaymentdocument_gid," +
                                " request_gid," +                               
                                " document_name," +                              
                                " document_path," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + request_gid + "'," +                                
                                "'" + httpPostedFile.FileName.Replace("'", @"\'") + "'," +                               
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + user_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                        

                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document uploaded successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error occured..!";
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
        public void DaHRLoanPaymentDocumentList(string request_gid, string employee_gid, MdlHRLoanPaymentDocumentUpload values)
        {
            msSQL = " select hrrepaymentdocument_gid,document_name,document_path " +
                    " from hrl_trn_thrrepaymentdocument where request_gid='" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getPaymentDocument_list = new List<PaymentDocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getPaymentDocument_list.Add(new PaymentDocument_list
                    {
                        hrrepaymentdocument_gid = (dr_datarow["hrrepaymentdocument_gid"].ToString()),                       
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.PaymentDocument_list = getPaymentDocument_list;
                }
                dt_datatable.Dispose();


            }

        }
        public void DaHRLoanGetApprovedInterest(string request_gid, MdlHRLoanPaymentDocumentUpload values)
        {
            msSQL = " select approved_interest,approved_tenure,date_format(approvedtenure_startdate,'%b-%Y') as approvedtenure_startdate ," +
                "date_format(approvedtenure_enddate,'%b-%Y') as approvedtenure_enddate " +
                    " from hrl_trn_trequest where request_gid='" + request_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //var getapproved_list = new List<approved_list>();

            if (objODBCDatareader.HasRows == true)
            {

                values.approved_interest = objODBCDatareader["approved_interest"].ToString();
                values.approved_tenure = objODBCDatareader["approved_tenure"].ToString();
                values.approvedtenure_startdate = objODBCDatareader["approvedtenure_startdate"].ToString();
                values.approvedtenure_enddate = objODBCDatareader["approvedtenure_enddate"].ToString();

            }
            objODBCDatareader.Close();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dr_datarow in dt_datatable.Rows)
            //    {
            //        getapproved_list.Add(new approved_list
            //        {
            //            approved_interest = (dr_datarow["approved_interest"].ToString()),

            //        });

            //        values.approved_interest = getapproved_list;
            //    }
            //dt_datatable.Dispose();


            //}

        }
        public void DaUploadPaymentDocumentsDelete(string hrrepaymentdocument_gid, MdlHRLoanPaymentDocumentUpload values)
        {
            msSQL = "delete from hrl_trn_thrrepaymentdocument where hrrepaymentdocument_gid='" + hrrepaymentdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error occured while deleting the document";
                values.status = false;

            }
        }        
    }
}