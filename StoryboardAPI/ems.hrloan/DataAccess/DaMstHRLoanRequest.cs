using ems.hrloan.Models;
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
using System.Net.Mail;
using System.Net;

namespace ems.hrloan.DataAccess
{
    public class DaMstHRLoanRequest
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;
        string lspath, lshrheadname, lshrheadgid;
        HttpPostedFile httpPostedFile;

        int k;
        public string ls_server, lsassignee, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, lsTo2members, lsrequest_refno, lsfintype_name, lspurpose_name, lsseverity_name, lsamount, lsreporting_mgr, lsBccmail_id, lscc2members, lscreated_date;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;
        string lshrloantenure_gid, lstenureinmonths, lswitheffectfrom;

        public void DaGetFinType(MdlMstHRLoanRequest objHRLoanRequest, string employee_gid)
        {
            try
            {

                msSQL = " SELECT a.hrloantypeoffinancialassistance_gid,a.hrloantypeoffinancialassistance_name FROM hrl_mst_thrloantypeoffinancialassistance a " +
                        " where a.status='Y' order by hrloantypeoffinancialassistance_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getHRLoanRequest_list = new List<fintype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getHRLoanRequest_list.Add(new fintype_list
                        {
                            fintype_gid = (dr_datarow["hrloantypeoffinancialassistance_gid"].ToString()),
                            fintype_name = (dr_datarow["hrloantypeoffinancialassistance_name"].ToString()),
                        });
                    }
                    objHRLoanRequest.fintype_list = getHRLoanRequest_list;
                }
                dt_datatable.Dispose();

                objHRLoanRequest.status = true;
            }
            catch
            {
                objHRLoanRequest.status = false;
            }

        }

        public void DaGetSeverity(MdlMstHRLoanRequest objHRLoanRequest, string employee_gid)
        {
            try
            {

                msSQL = " SELECT hrloanseverity_gid,hrloanseverity_name FROM hrl_mst_thrloanseverity where status='Y' order by hrloanseverity_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getHRLoanSeverity_list = new List<severity_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getHRLoanSeverity_list.Add(new severity_list
                        {
                            severity_gid = (dr_datarow["hrloanseverity_gid"].ToString()),
                            severity_name = (dr_datarow["hrloanseverity_name"].ToString()),
                        });
                    }
                    objHRLoanRequest.severity_list = getHRLoanSeverity_list;
                }
                dt_datatable.Dispose();

                objHRLoanRequest.status = true;
            }
            catch
            {
                objHRLoanRequest.status = false;
            }

        }

        public void DaGetPurpose(MdlMstHRLoanRequest objHRLoanRequest, string employee_gid)
        {
            try
            {

                msSQL = " SELECT hrloanpurpose_gid,hrloanpurpose_name,purpose_note,mandatory FROM hrl_mst_thrloanpurpose where status='Y' order by hrloanpurpose_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getHRLoanpurpose_list = new List<purpose_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getHRLoanpurpose_list.Add(new purpose_list
                        {
                            purpose_gid = (dr_datarow["hrloanpurpose_gid"].ToString()),
                            purpose_name = (dr_datarow["hrloanpurpose_name"].ToString()),
                            purpose_note = (dr_datarow["purpose_note"].ToString()),
                            mandatory = (dr_datarow["mandatory"].ToString()),
                        });
                    }
                    objHRLoanRequest.purpose_list = getHRLoanpurpose_list;
                }
                dt_datatable.Dispose();

                objHRLoanRequest.status = true;
            }
            catch
            {
                objHRLoanRequest.status = false;
            }

        }

        public bool DaPostDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsid_document = httpRequest.Form["document_id"].ToString();
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsrequest_gid = httpRequest.Form["request_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/HRReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "HRL/HRReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "HRL/HRReqDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("HRDO");

                        msSQL = " insert into hrl_trn_thrreqdocument(" +
                                 " hrreqdocument_gid," +
                                 " request_gid," +
                                 " document_title ," +
                                 " document_name," +
                                 " document_id," +
                                 " document_path," +
                                 " created_by," +
                                 " created_date" +
                                 " )values(" +
                                 "'" + msGetGid + "'," +
                                 "'N'," +
                                 "'" + lsdocument_title.Replace("'", @"\'") + "'," +
                                 "'" + httpPostedFile.FileName.Replace("'", @"\'") + "'," +
                                 "'" + lsid_document.Replace("'", @"\'") + "'," +
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

        public void DaUploadDocumentsDelete(string hrreqdocument_gid, MdlHRLoanDocumentUpload values)
        {
            msSQL = "delete from hrl_trn_thrreqdocument where hrreqdocument_gid='" + hrreqdocument_gid + "'";
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

        public void DaGettempdelete(string user_gid, result values)
        {
            msSQL = "delete from hrl_trn_thrreqdocument  where created_by='" + user_gid + "' and request_gid = 'N'";
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

        public void DaGetTenureName(hrloanrequest objHRLoanRequest, result values, string employee_gid, string fintype_name)
        {

            msSQL = "select hrloantypeoffinancialassistance_gid from hrl_mst_thrloantypeoffinancialassistance where " +
                    " hrloantypeoffinancialassistance_name = '" + fintype_name.Replace("'", @"\'") + "'  order by created_date desc limit 1 ";
            string lsfintypegid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select hrloantenure_gid from hrl_mst_thrloantenure where " +
                    " hrloantypeoffinancialassistance_gid = '" + lsfintypegid + "'  order by created_date desc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lshrloantenure_gid = objODBCDatareader["hrloantenure_gid"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " select hrloantenure_gid,tenure_in_months,date_format(with_effect_from, '%Y-%m-%d') as with_effect_from " +
                   " from hrl_mst_thrltenure  WHERE with_effect_from >= CURRENT_DATE() and hrloantenure_gid = '" + lshrloantenure_gid + "'" +
                   " order by with_effect_from asc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                lshrloantenure_gid = objODBCDatareader["hrloantenure_gid"].ToString();
                lstenureinmonths = objODBCDatareader["tenure_in_months"].ToString();
                lswitheffectfrom = objODBCDatareader["with_effect_from"].ToString();
            }
            objODBCDatareader.Close();
            string lscurrentdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (lswitheffectfrom == lscurrentdate)
            {

                msSQL = " update hrl_mst_thrloantenure set " +
                         " hrloantenure_name='" + lstenureinmonths + "'," +
                         " hrloantenurestart_date='" + Convert.ToDateTime(lswitheffectfrom).ToString("yyyy-MM-dd") + "'" +
                         " where hrloantenure_gid='" + lshrloantenure_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select hrloantenure_name from hrl_mst_thrloantenure where " +
                   " hrloantypeoffinancialassistance_gid = '" + lsfintypegid + "'  order by created_date desc limit 1";
            objHRLoanRequest.tenure = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;

        }
        public void DaGetTenureNameEdit(hrloanrequest objHRLoanRequest, result values, string employee_gid, string fintype_gid)
        {

            msSQL = " select hrloantenure_gid from hrl_mst_thrloantenure where " +
                    " hrloantypeoffinancialassistance_gid = '" + fintype_gid + "'  order by created_date desc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lshrloantenure_gid = objODBCDatareader["hrloantenure_gid"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " select hrloantenure_gid,tenure_in_months,date_format(with_effect_from, '%Y-%m-%d') as with_effect_from " +
                   " from hrl_mst_thrltenure  WHERE with_effect_from >= CURRENT_DATE() and hrloantenure_gid = '" + lshrloantenure_gid + "'" +
                   " order by with_effect_from asc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                lshrloantenure_gid = objODBCDatareader["hrloantenure_gid"].ToString();
                lstenureinmonths = objODBCDatareader["tenure_in_months"].ToString();
                lswitheffectfrom = objODBCDatareader["with_effect_from"].ToString();
            }
            objODBCDatareader.Close();
            string lscurrentdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (lswitheffectfrom == lscurrentdate)
            {

                msSQL = " update hrl_mst_thrloantenure set " +
                         " hrloantenure_name='" + lstenureinmonths + "'," +
                         " hrloantenurestart_date='" + Convert.ToDateTime(lswitheffectfrom).ToString("yyyy-MM-dd") + "'" +
                         " where hrloantenure_gid='" + lshrloantenure_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select hrloantenure_name from hrl_mst_thrloantenure where " +
                   " hrloantypeoffinancialassistance_gid = '" + fintype_gid + "'  order by created_date desc limit 1";
            objHRLoanRequest.tenure = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;

        }
        public void DaGetHRloanDetailscount(MdlMstHRLoanRequest values, string user_gid, string employee_gid)
        {
            msSQL = " select count(*) as pendingrequests_count from hrl_trn_trequest a where (a.created_by='" + user_gid + "')  and " +
                    "  (a.request_status = 'Draft' or a.request_status = 'DRM Pending' or a.request_status = 'DRM Approved' or a.request_status = 'FH Approved'  " +
                       " or  a.request_status = 'Query Raised By DRM ' or  a.request_status = 'Query Raised By FH '" +
                       "or a.request_status = 'Query Raised By HR '" +
                        "or a.request_status = 'Query Raised By Manager '" +
                         "or a.request_status = 'HRVerify Pending '" +
                     " or  a.request_status = 'FH Pending' or  a.request_status = 'HR Pending'" +
                     "  or  a.request_status = 'Reupload Pending' or a.request_status = 'Reupload Completed' " +
                     " or a.request_status = 'HRVerify Approved') ";
            values.pendingrequests_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as completedrequests_count from hrl_trn_trequest a  where (a.created_by='" + user_gid + "') and " +
                    " (a.request_status = 'Payment Completed') ";
            values.completedrequests_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as rejectedrequests_count from hrl_trn_trequest a  where (a.created_by='" + user_gid + "') and " +
                    "  (a.request_status = 'DRM Rejected' or a.request_status = 'FH Rejected' or " +
                    "  a.request_status = 'HR Rejected' or a.request_status ='HRVerify Rejected') ";
            values.rejectedrequests_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as withdrawn_count from hrl_trn_trequest a where a.created_by='" + user_gid + "' and a.request_status = 'Withdrawn' ";
            values.withdrawn_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as totalcount from hrl_trn_trequest a where a.created_by='" + user_gid + "' ";
            values.totalcount = objdbconn.GetExecuteScalar(msSQL);
        }

        public void DaGetHRloanDetails(MdlMstHRLoanRequest values, string user_gid, string employee_gid)
        {
            msSQL = " select request_gid, request_refno, request_status, raisequery_status, fintype_name, " +
                     " employee_gid, employee_name, employee_role, department_name, " +
                     " user_gid, reporting_mgr,  functional_head, created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                     " raised_department,  amount,  purpose_name,  severity_name, tenure from hrl_trn_trequest a " +
                     " where (a.created_by='" + user_gid + "')  and  (a.request_status = 'Draft' or a.request_status = 'DRM Pending' or a.request_status = 'DRM Approved' or a.request_status = 'FH Approved'  " +
                       " or  a.request_status = 'Query Raised By DRM ' or  a.request_status = 'Query Raised By FH '" +
                       "or a.request_status = 'Query Raised By HR '" +
                        "or a.request_status = 'Query Raised By Manager '" +
                         "or a.request_status = 'HRVerify Pending '" +
                     " or  a.request_status = 'FH Pending' or  a.request_status = 'HR Pending'" +
                     "  or  a.request_status = 'Reupload Pending' or a.request_status = 'Reupload Completed' " +
                     " or a.request_status = 'HRVerify Approved') order by request_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<hrloanrequest>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethrequestList.Add(new hrloanrequest
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        raisequery_status = dt["raisequery_status"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        user_gid = dt["user_gid"].ToString(),
                        reporting_mgr = dt["reporting_mgr"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        amount = dt["amount"].ToString(),

                    });
                    values.hrloanrequest = gethrequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostHRLoanRequest(hrloanrequest values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name,b.employee_gid from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Approver' " +
                    " order by b.created_date desc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                lshrheadname = objODBCDatareader["employee_name"].ToString();
                lshrheadgid = objODBCDatareader["employee_gid"].ToString();
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("HRRQ");
            //string lsrefno = objdbconn.GetExecuteScalar("select businessunit_prefix from osd_mst_tbusinessunit where businessunit_gid='" + values.department_gid + "'");
            //string lsrefno += "RRN";
            //msGet_RefNo = "RFN"+ msGetGid;

            msSQL = " insert into hrl_trn_trequest(" +
                    " request_gid,request_refno, " +
                    " fintype_gid, " +
                    " fintype_name,employee_gid ," +
                    " employee_name,employee_role, " +
                    " department_gid,department_name ," +
                    " user_gid, reporting_mgr, " +
                    " reportingmgr_gid,functional_head, " +
                    " functionalhead_gid ,hr_head,hrhead_gid," +
                    " entity_gid,entity_name," +
                    " created_by , " +
                    " created_date," +
                    " amount, " +
                    " interest, " +
                    " purpose_gid,  purpose_name , " +
                    " severity_gid,  severity_name , " +
                    " tenure,  request_reason,drm_status,fh_status,hrhead_status, " +
                    " hrverify_status,hrpayment_status,request_status)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + msGetGid + "'," +
                     "'" + values.fintype_gid + "'," +
                     "'" + values.fintype_name.Replace("'", @"\'") + "'," +
                     "'" + values.employee_gid + "', " +
                     "'" + values.employee_name + "'," +
                     "'" + values.employee_role + "'," +
                     "'" + values.department_gid + "'," +
                     "'" + values.department_name + "'," +
                     "'" + user_gid + "'," +
                     "'" + values.reporting_mgr + "'," +
                     "'" + values.reportingmgr_gid + "'," +
                     "'" + values.functional_head + "'," +
                     "'" + values.functionalhead_gid + "'," +
                     "'" + lshrheadname + "'," +
                     "'" + lshrheadgid + "'," +
                     "'" + values.entity_gid + "'," +
                     "'" + values.entity_name + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     "'" + values.amount + "',";
            if (values.interest == "" || values.interest == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.interest.Replace("'", @"\'") + "',";
            }
            msSQL += "'" + values.purpose_gid + "'," +
             "'" + values.purpose_name.Replace("'", @"\'") + "'," +
             "'" + values.severity_gid + "'," +
             "'" + values.severity_name.Replace("'", @"\'") + "'," +
             "'" + values.tenure + "'," +
             "'" + values.request_reason.Replace("'", @"\'") + "','Pending','Pending','Pending','Pending','Pending','DRM Pending' " +
             ")";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update hrl_trn_thrreqdocument set request_gid ='" + msGetGid + "' " +
                    " where created_by='" + user_gid + "' and request_gid ='N'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request raised successfully..!";
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
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.reportingmgr_gid, " +
                            " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as reporting_mgr, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.reportingmgr_gid = b.employee_gid " +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                            " where a.request_gid = '" + msGetGid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsfintype_name = objODBCDatareader["fintype_name"].ToString();
                        lspurpose_name = objODBCDatareader["purpose_name"].ToString();
                        lsseverity_name = objODBCDatareader["severity_name"].ToString();
                        lsamount = objODBCDatareader["amount"].ToString();
                        lsreporting_mgr = objODBCDatareader["reporting_mgr"].ToString();
                        lsTo2members = objODBCDatareader["reportingmgr_gid"].ToString();
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


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                    sub = "A new employee financial assistance request has been raised";
                    body = "Dear " + HttpUtility.HtmlEncode(lsreporting_mgr) + ",";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(employee_name) + " has raised a financial assistance request";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Type of Financial Assistance :</b> " + HttpUtility.HtmlEncode(lsfintype_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Purpose :</b> " + HttpUtility.HtmlEncode(lspurpose_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Severity :</b> " + HttpUtility.HtmlEncode(lsseverity_name) + "<br />";
                    body = body + "<br />";
                    body = body + "<b> Amount :</b> " + HttpUtility.HtmlEncode(lsamount) + "<br />";
                    body = body + "<br />";
                    body = body + "Click <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + ">here</a> to view and approve.<br />";
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
                values.message = "Error occured..!";
            }
        }
        public bool DaHRLoanRequestSaveasdraft(string user_gid, hrloanrequest values)
        {
            msSQL = " select b.employee_name,b.employee_gid from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Approver' " +
                    " order by b.created_date desc limit 1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                lshrheadname = objODBCDatareader["employee_name"].ToString();
                lshrheadgid = objODBCDatareader["employee_gid"].ToString();
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("HRRQ");

            msSQL = " insert into hrl_trn_trequest(" +
                    " request_gid,request_refno, " +
                    " fintype_gid, " +
                    " fintype_name,employee_gid ," +
                    " employee_name,employee_role, " +
                    " department_gid,department_name ," +
                    " user_gid, reporting_mgr, " +
                    " reportingmgr_gid,functional_head, " +
                    " functionalhead_gid ,hr_head,hrhead_gid," +
                    " entity_gid,entity_name," +
                    "created_by , " +
                    " created_date," +
                    " amount, " +
                    " interest, " +
                    " purpose_gid,  purpose_name , " +
                    " severity_gid,  severity_name , " +
                    " tenure,  request_reason,drm_status,fh_status," +
                    " hrhead_status,hrverify_status,hrpayment_status,request_status)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + msGetGid + "'," +
                     "'" + values.fintype_gid + "'," +
                     "'" + values.fintype_name.Replace("'", @"\'") + "'," +
                     "'" + values.employee_gid + "', " +
                     "'" + values.employee_name + "'," +
                     "'" + values.employee_role + "'," +
                     "'" + values.department_gid + "'," +
                     "'" + values.department_name + "'," +
                     "'" + user_gid + "'," +
                     "'" + values.reporting_mgr + "'," +
                     "'" + values.reportingmgr_gid + "'," +
                     "'" + values.functional_head + "'," +
                     "'" + values.functionalhead_gid + "'," +
                     "'" + lshrheadname + "'," +
                     "'" + lshrheadgid + "'," +
                     "'" + values.entity_gid + "'," +
                     "'" + values.entity_name + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            if (values.amount == "" || values.amount == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.amount.Replace("'", @"\'") + "',";
            }
            if (values.interest == "" || values.interest == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.interest.Replace("'", @"\'") + "',";
            }
            msSQL += "'" + values.purpose_gid + "'," +
             "'" + values.purpose_name.Replace("'", @"\'") + "'," +
             "'" + values.severity_gid + "'," +
             "'" + values.severity_name.Replace("'", @"\'") + "'," +
             "'" + values.tenure + "',";
            if (values.request_reason == "" || values.request_reason == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.request_reason.Replace("'", @"\'") + "',";
            }
            msSQL += " 'Pending','Pending','Pending','Pending','Pending','Draft' " +
             ")";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = " update hrl_trn_thrreqdocument set request_gid ='" + msGetGid + "' " +
                   " where created_by='" + user_gid + "' and request_gid ='N'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request saved successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error occured while saving request";
                return false;
            }

        }
        public void DaPostHRLoanSaveasdraft(string user_gid, hrloanrequest values)
        {
            msSQL = " update hrl_trn_trequest set " +
                     " fintype_gid='" + values.fintype_gid + "'," +
                     " fintype_name='" + values.fintype_name.Replace("'", @"\'") + "'," +
                     " amount='" + values.amount.Replace("'", @"\'") + "'," +
                     " interest='" + values.interest.Replace("'", @"\'") + "'," +
                     " purpose_gid='" + values.purpose_gid + "'," +
                     " purpose_name='" + values.purpose_name.Replace("'", @"\'") + "'," +
                     " severity_gid='" + values.severity_gid + "'," +
                     " severity_name='" + values.severity_name.Replace("'", @"\'") + "'," +
                     " tenure='" + values.tenure + "'," +
                     " request_reason='" + values.request_reason.Replace("'", @"\'") + "'," +
                     " updated_by='" + user_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                     " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where request_gid='" + values.request_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update hrl_trn_trequest set request_status='DRM Pending'" +
                    " where request_gid='" + values.request_gid + "' and request_status = 'Draft'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update hrl_trn_thrreqdocument set request_gid ='" + values.request_gid + "' " +
                    " where created_by='" + user_gid + "' and request_gid ='N'";
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
                msSQL = " select  group_concat(distinct a.employee_gid) as CC2members, " +
                        " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.reportingmgr_gid, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as reporting_mgr, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                        " a.created_date,a.created_by from hrl_trn_trequest a " +
                        " left join hrm_mst_temployee b on a.reportingmgr_gid = b.employee_gid " +
                        " left join hrm_mst_temployee d on a.employee_gid = d.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join adm_mst_tuser e  on e.user_gid = d.user_gid " +
                        " where a.request_gid = '" + values.request_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscc2members = objODBCDatareader["CC2members"].ToString();
                    lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                    lsfintype_name = objODBCDatareader["fintype_name"].ToString();
                    lspurpose_name = objODBCDatareader["purpose_name"].ToString();
                    lsseverity_name = objODBCDatareader["severity_name"].ToString();
                    lsamount = objODBCDatareader["amount"].ToString();
                    lsreporting_mgr = objODBCDatareader["reporting_mgr"].ToString();
                    lsTo2members = objODBCDatareader["reportingmgr_gid"].ToString();
                    lsemployee_name = objODBCDatareader["employee"].ToString();
                    lscreated_date = objODBCDatareader["created_date"].ToString();
                }
                objODBCDatareader.Close();

                //msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name from hrm_mst_temployee a " +
                //        " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                //        " where employee_gid = '" + values.employee_gid + "'";
                //string employee_name = objdbconn.GetExecuteScalar(msSQL);

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


                msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                cc_mailid = objdbconn.GetExecuteScalar(msSQL);



                sub = "A new employee financial assistance request has been raised";
                body = "Dear " + HttpUtility.HtmlEncode(lsreporting_mgr) + ",";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has raised a financial assistance request";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "<b> Type of Financial Assistance :</b> " + HttpUtility.HtmlEncode(lsfintype_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Purpose :</b> " + HttpUtility.HtmlEncode(lspurpose_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Severity :</b> " + HttpUtility.HtmlEncode(lsseverity_name) + "<br />";
                body = body + "<br />";
                body = body + "<b> Amount :</b> " + HttpUtility.HtmlEncode(lsamount) + "<br />";
                body = body + "<br />";
                body = body + "Click <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + ">here</a> to view and approve.<br />";
                body = body + "<br />";
                body = body + "<br />";
                body = body + "Thanks & Regards, ";
                body = body + "<br />";
                body = body + HttpUtility.HtmlEncode(lsemployee_name);
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
                    "'" + values.employee_name + "'," +
                    "'" + lsto_mail + "'," +
                    "'" + cc_mailid + "'," +
                    "'Query Raised Successfully'," +
                       "'" + msGetGid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request submitted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occurred while updating";
            }
        }
        public void DaUpdateHRLoanSaveasdraft(string user_gid, hrloanrequest values)
        {
            msSQL = " update hrl_trn_trequest set " +
                     " fintype_gid='" + values.fintype_gid + "'," +
                     " fintype_name='" + values.fintype_name.Replace("'", @"\'") + "'," +
                     " amount='" + values.amount.Replace("'", @"\'") + "'," +
                     " interest='" + values.interest.Replace("'", @"\'") + "'," +
                     " purpose_gid='" + values.purpose_gid + "'," +
                     " purpose_name='" + values.purpose_name.Replace("'", @"\'") + "'," +
                     " severity_gid='" + values.severity_gid + "'," +
                     " severity_name='" + values.severity_name.Replace("'", @"\'") + "'," +
                     " tenure='" + values.tenure + "'," +
                     " request_reason='" + values.request_reason.Replace("'", @"\'") + "'," +
                     " updated_by='" + user_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where request_gid='" + values.request_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update hrl_trn_trequest set request_status='Draft'" +
                    " where request_gid='" + values.request_gid + "' and request_status = 'Draft'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update hrl_trn_thrreqdocument set request_gid ='" + values.request_gid + "' " +
                    " where created_by='" + user_gid + "' and request_gid ='N'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request saved successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occurred while updating";
            }
        }

        public void DaGetEmployeeDetails(MdlMstHRLoanRequest objHRLoanRequest, string employee_gid)
        {
            try
            {

                msSQL = " select r.employee_gid,c.department_manager,a.user_code,a.user_gid , " +
                        " concat(a.user_firstname, ' ', a.user_lastname, ' / ', a.user_code) as employee_name," +
                        " c.department_gid,c.department_name ,n.role_name, " +
                        " concat(rt.user_code, ' || ', rt.user_firstname, ' ', rt.user_lastname) as reporting_mgr," +
                        " concat(fh.user_code, ' || ', fh.user_firstname, ' ', fh.user_lastname) as functional_head, " +
                        " b.employee_personalno as personal_phone_no,b.personal_emailid, " +
                        " f.employee_gid as functionalhead_gid,r.employee_gid as reportingmgr_gid ," +
                        " b.employee_emailid,b.employee_mobileno,te.entity_gid,te.entity_name " +
                        " FROM adm_mst_tuser a " +
                        " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join hrm_mst_trole n on n.role_gid = b.role_gid " +
                        " left join hrm_mst_temployee r on b.employeereporting_to = r.employee_gid " +
                        " left join adm_mst_tuser rt on rt.user_gid = r.user_gid " +
                        " left join hrm_mst_temployee f on r.employeereporting_to = f.employee_gid  " +
                        " left join adm_mst_tuser fh on fh.user_gid = f.user_gid " +
                        " left join adm_mst_tentity te on te.entity_gid = b.entity_gid " +
                        " where b.employee_gid = '" + employee_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objHRLoanRequest.employee_name = objODBCDatareader["employee_name"].ToString();
                    objHRLoanRequest.role = objODBCDatareader["role_name"].ToString();
                    objHRLoanRequest.department = objODBCDatareader["department_name"].ToString();
                    objHRLoanRequest.reporting_manager = objODBCDatareader["reporting_mgr"].ToString();
                    objHRLoanRequest.functional_head = objODBCDatareader["functional_head"].ToString();
                    objHRLoanRequest.functionalhead_gid = objODBCDatareader["functionalhead_gid"].ToString();
                    objHRLoanRequest.reportingmgr_gid = objODBCDatareader["reportingmgr_gid"].ToString();
                    objHRLoanRequest.department_gid = objODBCDatareader["department_gid"].ToString();
                    objHRLoanRequest.employee_gid = employee_gid;
                    objHRLoanRequest.official_mailid = objODBCDatareader["employee_emailid"].ToString();
                    objHRLoanRequest.official_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    objHRLoanRequest.pers_mailid = objODBCDatareader["personal_emailid"].ToString();
                    objHRLoanRequest.pers_mobileno = objODBCDatareader["personal_phone_no"].ToString();
                    objHRLoanRequest.entity_gid = objODBCDatareader["entity_gid"].ToString();
                    objHRLoanRequest.entity_name = objODBCDatareader["entity_name"].ToString();
                }
                objODBCDatareader.Close();

                objHRLoanRequest.status = true;
            }
            catch
            {
                objHRLoanRequest.status = false;
            }

        }

        public void DaEditHRLoanRequest(string request_gid, hrloanrequest values)
        {
            try
            {
                msSQL = " SELECT request_gid,fintype_gid,fintype_name, " +
                        " amount,interest,purpose_gid,purpose_name,severity_gid, severity_name, tenure, request_reason, request_status,request_refno " +
                        " FROM hrl_trn_trequest where request_gid='" + request_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.request_gid = objODBCDatareader["request_gid"].ToString();
                    values.fintype_gid = objODBCDatareader["fintype_gid"].ToString();
                    values.fintype_name = objODBCDatareader["fintype_name"].ToString();
                    values.amount = objODBCDatareader["amount"].ToString();
                    values.interest = objODBCDatareader["interest"].ToString();
                    values.purpose_gid = objODBCDatareader["purpose_gid"].ToString();
                    values.purpose_name = objODBCDatareader["purpose_name"].ToString();
                    values.severity_gid = objODBCDatareader["severity_gid"].ToString();
                    values.severity_name = objODBCDatareader["severity_name"].ToString();
                    values.tenure = objODBCDatareader["tenure"].ToString();
                    values.request_reason = objODBCDatareader["request_reason"].ToString();
                    values.request_status = objODBCDatareader["request_status"].ToString();
                    values.request_refno = objODBCDatareader["request_refno"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " SELECT hrloanpurpose_gid,hrloanpurpose_name,purpose_note,mandatory FROM hrl_mst_thrloanpurpose a " +
                        " left join hrl_trn_trequest b on b.purpose_gid = a.hrloanpurpose_gid  where b.request_gid='" + request_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.hrloanpurpose_gid = objODBCDatareader["hrloanpurpose_gid"].ToString();
                    values.hrloanpurpose_name = objODBCDatareader["hrloanpurpose_name"].ToString();
                    values.purpose_note = objODBCDatareader["purpose_note"].ToString();
                    values.mandatory = objODBCDatareader["mandatory"].ToString();                    
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAddUploadDocumentsList(string employee_gid, MdlHRLoanDocumentUpload values)
        {
            msSQL = " select c.user_gid " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
            string user_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select hrreqdocument_gid,request_gid,document_id,document_title,document_name,document_path " +
                   " from hrl_trn_thrreqdocument where request_gid = 'N' and created_by='" + user_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUpload_list = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUpload_list.Add(new upload_list
                    {
                        hrreqdocument_gid = (dr_datarow["hrreqdocument_gid"].ToString()),
                        document_id = (dr_datarow["document_id"].ToString()),
                        document_title = (dr_datarow["document_title"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),

                    });

                    values.upload_list = getUpload_list;
                }
                dt_datatable.Dispose();
            }

        }
        public void DaGetUploadDocumentsList(string request_gid, string employee_gid, MdlHRLoanDocumentUpload values)
        {
            msSQL = " select hrreqdocument_gid,request_gid,document_id,document_title,document_name,document_path " +
                   " from hrl_trn_thrreqdocument where request_gid='" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getUpload_list = new List<upload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getUpload_list.Add(new upload_list
                    {
                        hrreqdocument_gid = (dr_datarow["hrreqdocument_gid"].ToString()),
                        document_id = (dr_datarow["document_id"].ToString()),
                        document_title = (dr_datarow["document_title"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),

                    });

                    values.upload_list = getUpload_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaUpdateHRLoanRequest(string user_gid, hrloanrequest values)
        {
            msSQL = " update hrl_trn_trequest set " +
                     " fintype_gid='" + values.fintype_gid + "'," +
                     " fintype_name='" + values.fintype_name.Replace("'", @"\'") + "'," +
                     " amount='" + values.amount.Replace("'", @"\'") + "'," +
                     " interest='" + values.interest.Replace("'", @"\'") + "'," +
                     " purpose_gid='" + values.purpose_gid + "'," +
                     " purpose_name='" + values.purpose_name.Replace("'", @"\'") + "'," +
                     " severity_gid='" + values.severity_gid + "'," +
                     " severity_name='" + values.severity_name.Replace("'", @"\'") + "'," +
                     " tenure='" + values.tenure + "'," +
                     " request_reason='" + values.request_reason.Replace("'", @"\'") + "'," +
                     " updated_by='" + user_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where request_gid='" + values.request_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update hrl_trn_trequest set request_status='DRM Pending'" +
                    " where request_gid='" + values.request_gid + "' and request_status = 'Draft'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update hrl_trn_thrreqdocument set request_gid ='" + values.request_gid + "' " +
                    " where created_by='" + user_gid + "' and request_gid ='N'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Request updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occurred while updating";
            }
        }

        public bool DaPostHrLoanwithdrawUpdate(hrloanrequest values, string employee_gid)
        {
            msSQL = "  select a.request_status from hrl_trn_trequest a where a.request_status='HR Approved' " +
                    "  and  a.employee_gid ='" + employee_gid + "' ";


            string request_status = objdbconn.GetExecuteScalar(msSQL);
            if (request_status == "HR Approved")
            {
                msSQL = " select request_gid from hrl_trn_trequest " +
               " where request_gid = '" + values.request_gid + "'";
                string request_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set withdraw_flag ='Y'," +
                        " request_status = 'Withdrawn'," +
                        " withdraw_remarks='" + values.withdraw_remarks.Replace("'", @"\'") + "' " +
                        " where request_gid='" + request_gid + "' ";
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
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " where a.request_gid = '" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
                        lsfintype_name = objODBCDatareader["fintype_name"].ToString();
                        lspurpose_name = objODBCDatareader["purpose_name"].ToString();
                        lsseverity_name = objODBCDatareader["severity_name"].ToString();
                        lsamount = objODBCDatareader["amount"].ToString();
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


                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                             " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                             " where hrmapping_name = 'Manager')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    sub = "A new employee financial assistance request has been withdrawn ";
                    body = "Dear Sir/Madam,";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has withdrawn a financial assistance request ";
                    body = body + "<br />";
                    body = body + "<br />";
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
                msSQL = " select request_gid from hrl_trn_trequest " +
               " where request_gid = '" + values.request_gid + "'";
                string request_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set withdraw_flag ='Y'," +
                        " request_status = 'Withdrawn'," +
                        " withdraw_remarks='" + values.withdraw_remarks.Replace("'", @"\'") + "' " +
                        " where request_gid='" + request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            values.status = true;
            values.message = "Request withdrawn successfully";
            return true;

        }
        public void DaGetHRLoanStatusRequest(hrloanrequest values, string request_gid)
        {
            msSQL = " select request_status,raisequery_status from hrl_trn_trequest " +

                    " where request_gid='" + request_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                values.request_status = objODBCDatareader["request_status"].ToString();
                values.raisequery_status = objODBCDatareader["raisequery_status"].ToString();
            }
            objODBCDatareader.Close();
        }
    }
}