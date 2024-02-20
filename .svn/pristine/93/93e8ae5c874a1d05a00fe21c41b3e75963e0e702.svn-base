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
    public class DaMstHRLoanDrmApproval
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;
        string lspath;
        HttpPostedFile httpPostedFile;
        string hrhead_approvedbyname;
        int k;
        public string ls_server, lsassignee, ls_username, ls_password, tomail_id, tomail_id1, tomail_id2, sub, body, employeename, cc_mailid, lsto_mail, employee_reporting_to;
        int ls_port;
        string lsemployee_name, cc_managermailid, lsTo2members, lsrequest_refno, lsdepartment_name, lsfintype_name, lspurpose_name, lsseverity_name, lsamount, lsfunctional_head, lshr_head, lsBccmail_id, lscc2members, lscreated_date;
        string sToken = string.Empty;
        Random rand = new Random();
        public string[] lsToReceipients;
        public string[] lsCCReceipients;
        public string[] lsBCCReceipients;

        public void DaGetHRloanApprovalSummary(MdlMstHRLoanDrmApproval values, string user_gid, string employee_gid, string request_gid)
        {
            msSQL = " select request_gid,'Direct Reporting Manager' as approval_Level,drm_status as approval_status,drm_approvedbyname as approved_byname,date_format(drm_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,drm_remarks as approval_remarks from hrl_trn_trequest " +
                    " where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'Functional Head' as approval_Level ,fh_status as approval_status,fh_approvedbyname as approved_byname,date_format(fh_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,fh_remarks as approval_remarks  from  " +
                    " hrl_trn_trequest where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'HR Head' as approval_Level,hrhead_status as approval_status,hrhead_approvedbyname as approved_byname,date_format(hrhead_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,hrhead_remarks as approval_remarks " +
                    " from hrl_trn_trequest where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'HR Manager' as approval_Level,hrverify_status as approval_status,hrverify_approvedbyname as approved_byname,date_format(hrverify_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,hrverify_remarks as approval_remarks " +
                    " from hrl_trn_trequest where request_gid = '" + request_gid + "' " +
                    " union all " +
                    " select request_gid,'Payment Approval' as approval_Level,hrpayment_status as approval_status,hrpayment_approvedbyname as approved_byname,date_format(hrpayment_updateddate,'%d-%m-%Y %h:%i %p') as updated_date,hrpayment_remarks as approval_remarks " +
                    " from hrl_trn_trequest where request_gid = '" + request_gid + "' ";



            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getApprovalsummary = new List<Approvalsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getApprovalsummary.Add(new Approvalsummary
                    {
                        request_gid = dt["request_gid"].ToString(),
                        approval_Level = dt["approval_Level"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        approved_by = dt["approved_byname"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        approval_remarks = dt["approval_remarks"].ToString(),

                    });
                    values.Approvalsummary = getApprovalsummary;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaUploadDocumentsDelete(string hrreuploaddocument_gid, MdlReuploaddeletDocument values)
        {
            msSQL = "delete from hrl_trn_thrreuploaddocument where hrreuploaddocument_gid='" + hrreuploaddocument_gid + "'";
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
        public void DaGetDropDown(string employee_gid, MdlhrDropDownList values)
        {

            //DocumentList

            msSQL = "select hrdocument_gid, hrdocument_name from hrl_mst_thrdocument where status = 'Y' order by hrdocument_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegmentdoc = new List<hrddocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegmentdoc.Add(new hrddocument_list
                    {
                        hrdocumentlist_gid = (dr_datarow["hrdocument_gid"].ToString()),
                        hrdocumentlist_name = (dr_datarow["hrdocument_name"].ToString()),

                    });
                }
                values.hrdocument_list = getSegmentdoc;
            }
            dt_datatable.Dispose();

        }
        public void DaUploadList(string request_gid, string employee_gid, MdlhrDocument values)
        {
            msSQL = " select hrspecialdocument_gid, document_id, document_title, hrdocument_gid, document_name " +
           "from hrl_trn_thrspecialdocument where request_gid = '" + request_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocuments_list = new List<HrReuploadDocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDocuments_list.Add(new HrReuploadDocument_list
                    {
                        request_gid = (dr_datarow["hrspecialdocument_gid"].ToString()),
                        document_id = (dr_datarow["document_id"].ToString()),
                        document_title = (dr_datarow["document_title"].ToString()),
                        hrdocument_gid = (dr_datarow["hrdocument_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        //  document_path = (dr_datarow["document_path"].ToString()),
                    });

                    values.HrReuploadDocument_list = getDocuments_list;
                }
                dt_datatable.Dispose();
            }
        }
        public bool ReUploadDocument(HttpRequest httpRequest, uploaddocument2 objfilename, string employee_gid, string user_gid)
        {
            upload_list1 objdocumentmodel = new upload_list1();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsid_document = httpRequest.Form["document_id"].ToString();
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string request_gid = httpRequest.Form["request_gid"].ToString();
            string hrdocument_gid = httpRequest.Form["hrdocument_name"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();


            String path = lspath;


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/HRRaiserequestDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
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
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "HRL/HRRaiseRequestDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "HRL/HRRaiseRequestDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        msGetGid = objcmnfunctions.GetMasterGID("HRRE");
                        msSQL = " insert into hrl_trn_thrreuploaddocument(" +
                                " hrreuploaddocument_gid," +
                                " request_gid," +
                                " hrspecialdocument_gid," +
                                " document_title ," +
                                " document_name," +
                                " document_id," +
                                " document_path," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + request_gid + "'," +
                                "'" + hrdocument_gid + "'," +
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
        public void DaGetUploadDocumentsList(string request_gid, string employee_gid, MdlhrreuploadDocument values)
        {
            msSQL = "   select hrreuploaddocument_gid,document_name,document_title,document_path,document_id " +
                       " from hrl_trn_thrreuploaddocument where request_gid = '" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getDocuments_list = new List<hrReuploadDocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getDocuments_list.Add(new hrReuploadDocument_list
                    {
                        hrreuploaddocument_gid = (dr_datarow["hrreuploaddocument_gid"].ToString()),
                        document_id = (dr_datarow["document_id"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_title = (dr_datarow["document_title"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.HrReuploadDocument_list = getDocuments_list;
                }
                dt_datatable.Dispose();
            }

        }
        public void DaGetHRloanRequestDetailscount(MdlMstHRLoanDrmApproval values, string user_gid, string employee_gid)
        {
            msSQL = " select count(*) as pendingapprovals_count from hrl_trn_trequest a where " +
                     "  ((a.reportingmgr_gid='" + employee_gid + "' and  a.request_status = 'DRM Pending') or  (a.reportingmgr_gid='" + employee_gid + "' and a.request_status = 'Query Raised By DRM')) or  " +
                        " ((a.functionalhead_gid='" + employee_gid + "' and  a.request_status = 'FH Pending') or (a.functionalhead_gid='" + employee_gid + "' and  a.request_status = 'Query Raised By FH' ))  ";
            values.pendingapprovals_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as approvedapprovals_count from hrl_trn_trequest a " +
                    "  where ((a.reportingmgr_gid='" + employee_gid + "') and  ( a.request_status = 'FH Pending' or  a.request_status = 'HR Pending'" +
                         "  or  a.request_status = 'Reupload Pending' or a.request_status = 'Reupload Completed' or a.request_status = 'HRVerify Pending' or a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed')) " +
                    "  or ((a.functionalhead_gid='" + employee_gid + "')  and  ( a.request_status = 'HR Pending'" +
                         "  or  a.request_status = 'Reupload Pending' or a.request_status = 'Reupload Completed' or a.request_status = 'HRVerify Pending' or a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed')) ";
            values.approvedapprovals_count = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select count(*) as rejectedapprovals_count from hrl_trn_trequest a " +
                    "  where (a.reportingmgr_gid='" + employee_gid + "' and  a.request_status = 'DRM Rejected') " +
                    "  or (a.functionalhead_gid='" + employee_gid + "' and  a.request_status = 'FH Rejected')";
            values.rejectedapprovals_count = objdbconn.GetExecuteScalar(msSQL);
        }
        public void DaGetHRloanRequestDetails(MdlMstHRLoanDrmApproval values, string user_gid, string employee_gid)
        {
            msSQL = " select a.request_gid, a.request_refno, a.request_status,  a.fintype_name, " +
                        " a.employee_gid, a.employee_name, a.employee_role, a.department_name, " +
                        " a.user_gid, a.reporting_mgr,  a.functional_head,a.hr_head, a.created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " a.raised_department,  a.amount,  a.purpose_name,  a.severity_name, a.tenure  ," +
                         " a.drm_status from hrl_trn_trequest a " +
                        " where ((a.reportingmgr_gid='" + employee_gid + "' and  a.request_status = 'DRM Pending') or  (a.reportingmgr_gid='" + employee_gid + "' and a.request_status = 'Query Raised By DRM')) or  " +
                        " ((a.functionalhead_gid='" + employee_gid + "' and  a.request_status = 'FH Pending') or (a.functionalhead_gid='" + employee_gid + "' and  a.request_status = 'Query Raised By FH' )) order by a.request_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<requestsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethrequestList.Add(new requestsummary
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
                    values.requestsummary = gethrequestList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRloanHRheadRequestDetailscount(MdlMstHRLoanDrmApproval values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Approver' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = " select count(*) as hrpendingapprovals_count from hrl_trn_trequest a " +
                        "  where  a.request_status = 'HR Pending'  ";
                values.hrpendingapprovals_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as hrapprovedapprovals_count from hrl_trn_trequest a " +
                        "  where (a.request_status = 'HR Approved'" +
                         "  or  a.request_status = 'Reupload Pending'  or  a.request_status = 'HRVerify Pending' or a.request_status = 'Reupload Completed' " +
                         "  or a.request_status = 'HRVerify Approved' or  a.request_status = 'Payment Completed') ";
                values.hrapprovedapprovals_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(*) as hrrejectedapprovals_count from hrl_trn_trequest a " +
                        "  where a.request_status = 'HR Rejected'  ";
                values.hrrejectedapprovals_count = objdbconn.GetExecuteScalar(msSQL);
            }
        }
        public void DaGetHRloanHRheadRequestDetails(MdlMstHRLoanDrmApproval values, string user_gid, string employee_gid)
        {
            msSQL = " select b.employee_name from hrl_mst_thrmapping a " +
                    " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                    " where hrmapping_name = 'Approver' and b.employee_gid ='" + employee_gid + "' " +
                    " order by b.created_date desc limit 1";

            string hr_name = objdbconn.GetExecuteScalar(msSQL);
            if (hr_name != "")
            {
                msSQL = "  select  a.request_gid,  a.request_refno,  a.request_status,   a.fintype_name, " +
                         "  a.employee_gid,  a.employee_name,  a.employee_role,  a.department_name, " +
                         "  a.user_gid,  a.reporting_mgr,   a.functional_head, a.hr_head,  a.created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                         "  a.raised_department,   a.amount,   a.purpose_name,   a.severity_name,  a.tenure ," +
                         "  a.drm_status from hrl_trn_trequest a " +
                         "  where  a.request_status = 'HR Pending' or a.request_status = 'Query Raised By HR' " +
                         "  order by a.request_gid desc";

            }


            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<requestsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    gethrequestList.Add(new requestsummary
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
                    values.requestsummary = gethrequestList;
                }
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRloanRequestviewDetails(requestsummary values, string user_gid, string employee_gid, string request_gid)
        {
            msSQL = " select request_gid, request_refno, request_status,approved_interest,approved_tenure, fintype_name, " +
                     " employee_gid, employee_name, employee_role, department_name, " +
                     " user_gid, reporting_mgr,  functional_head,hr_head, created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                     " raised_department,  amount, interest,  purpose_name,  severity_name, tenure ,drm_status,date_format(approvedtenure_startdate,'%b-%Y') as approvedtenure_startdate ,  " +
                     " date_format(approvedtenure_enddate,'%b-%Y') as approvedtenure_enddate,entity_name from hrl_trn_trequest a " +
                     " where a.request_gid='" + request_gid + "' ";


            dt_datatable = objdbconn.GetDataTable(msSQL);

            var gethrequestList = new List<requestsummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {


                    values.request_gid = dt["request_gid"].ToString();
                    values.request_refno = dt["request_refno"].ToString();
                    values.request_status = dt["request_status"].ToString();
                    values.drm_status = dt["drm_status"].ToString();
                    values.employee_gid = dt["employee_gid"].ToString();
                    values.employee_name = dt["employee_name"].ToString();
                    values.employee_role = dt["employee_role"].ToString();
                    values.department_name = dt["department_name"].ToString();
                    values.user_gid = dt["user_gid"].ToString();
                    values.reporting_mgr = dt["reporting_mgr"].ToString();
                    values.functional_head = dt["functional_head"].ToString();
                    values.hr_head = dt["hr_head"].ToString();
                    values.amount = dt["amount"].ToString();
                    values.purpose_name = dt["purpose_name"].ToString();
                    values.severity_name = dt["severity_name"].ToString();
                    values.tenure = dt["tenure"].ToString();
                    values.fintype_name = dt["fintype_name"].ToString();
                    values.created_date = dt["created_date"].ToString();
                    values.interest = dt["interest"].ToString();
                    values.approved_interest = dt["approved_interest"].ToString();
                    values.approved_tenure = dt["approved_tenure"].ToString();
                    values.approvedtenure_startdate = dt["approvedtenure_startdate"].ToString();
                    values.approvedtenure_enddate = dt["approvedtenure_enddate"].ToString();
                    values.entity_name = dt["entity_name"].ToString();
                }
            }
            dt_datatable.Dispose();
        }
        public bool DaPostHrLoanDRMApprovalUpdate(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_tdrmraisequery where request_gid = '" + values.request_gid + "'" +
                " and raisequery_status = 'Query Raised'";
            values.drmopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.drmopenquerycount == "0")
            {
                //msSQL = "update hrl_trn_trequest set status = 'N' where created_by='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string drm_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='FH Pending'," +
                        " drm_status ='Approved' ," +
                        " drm_remarks='" + values.drm_remarks.Replace("'", @"\'") + "' ," +
                        " drm_approvedby='" + employee_gid + "'," +
                        " drm_approvedbyname='" + drm_approvedbyname + "'," +
                        " drm_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Direct Reporting Manager approved successfully";
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
                    msSQL = " select  group_concat(distinct a.employee_gid,',',a.reportingmgr_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.functionalhead_gid, " +
                            " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as functional_head, " +
                            " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                            " a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.functionalhead_gid = b.employee_gid " +
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
                        lsfunctional_head = objODBCDatareader["functional_head"].ToString();
                        lsTo2members = objODBCDatareader["functionalhead_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee"].ToString();
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



                    sub = "Functional Head Approval requested for employee financial assistance.";
                    body = "Dear " + HttpUtility.HtmlEncode(lsfunctional_head) + ",";
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
                    body = body + "Click <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + ">here</a>  to view and approve.<br />";
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
        public bool DaPostHrLoanDRMRejectUpdate(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_tdrmraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.drmopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.drmopenquerycount == "0")
            {
                //msSQL = "update hrl_trn_trequest set status = 'N' where created_by='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string drm_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='DRM Rejected'," +
                        " drm_status='Rejected' ," +
                        " drm_remarks='" + values.drm_remarks.Replace("'", @"\'") + "' ," +
                        " drm_approvedby='" + employee_gid + "'," +
                        " drm_approvedbyname='" + drm_approvedbyname + "'," +
                        " drm_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Direct Reporting Manager rejected successfully";
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
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.functionalhead_gid, " +
                            " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as functional_head, " +
                            " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                            " a.created_date,a.created_by,a.reportingmgr_gid from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.functionalhead_gid = b.employee_gid " +
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
                        lsfunctional_head = objODBCDatareader["functional_head"].ToString();
                        lsTo2members = objODBCDatareader["reportingmgr_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee"].ToString();
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
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);



                    sub = "Functional Head Approval rejected for employee financial assistance.";
                    body = "Dear " + HttpUtility.HtmlEncode(lsfunctional_head) + ",";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has rejected a financial assistance request";
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
                    body = body + "Click <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + ">here</a>  to view and approve.<br />";
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
                values.message = "Reject can't be done,the query is still open";

            }
            return true;

        }
        public bool DaPostHrLoanFHApprovalUpdate(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_tfhraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.fhopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.fhopenquerycount == "0")
            {
                //msSQL = "update hrl_trn_trequest set status = 'N' where created_by='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string fh_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='HR Pending'," +
                        " fh_status='Approved' ," +
                        " fh_remarks='" + values.fh_remarks.Replace("'", @"\'") + "' ," +
                        " fh_approvedby='" + employee_gid + "'," +
                        " fh_approvedbyname='" + fh_approvedbyname + "'," +
                        " fh_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Functional Head approved successfully";


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
                    msSQL = " select  group_concat(distinct a.employee_gid,',',a.functionalhead_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.hrhead_gid, " +
                            " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as hr_head, " +
                            " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.hrhead_gid = b.employee_gid " +
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
                        lshr_head = objODBCDatareader["hr_head"].ToString();
                        lsTo2members = objODBCDatareader["hrhead_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee"].ToString();
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



                    sub = "HR Head Approval requested for employee financial assistance.";
                    body = "Dear " + HttpUtility.HtmlEncode(lshr_head) + ",";
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
            //HR Head Auto Approve for Less Than 50000
            msSQL = " select count(*) as openquery from hrl_apr_thrraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.hropenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.hropenquerycount == "0")
            {
                msSQL = " select amount,fintype_name from hrl_trn_trequest where request_gid = '" + values.request_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                long lsrequest_amount=0;
                long lsreqamount = 50000;
                string lsfintype_name = "";
                if (objODBCDatareader.HasRows == true)
                {
                    lsrequest_amount = Convert.ToInt64(objODBCDatareader["amount"].ToString().Replace(",", ""));
                    lsfintype_name = objODBCDatareader["fintype_name"].ToString().Replace(" ","").ToLower();
                }
                objODBCDatareader.Close();


                if ((lsrequest_amount <= lsreqamount) && (lsfintype_name == "salaryadvance"))
                {
                    msSQL = " select count(*) as openquery from hrl_apr_thrraisequery where request_gid = '" + values.request_gid + "'" +
                            " and raisequery_status = 'Query Raised'";
                    values.hropenquerycount = objdbconn.GetExecuteScalar(msSQL);
                    if (values.hropenquerycount == "0")
                    {
                        msSQL = " select a.employee_name as employee_name  from hrl_mst_thrmapping2employee a" +
                    " left join hrl_mst_thrmapping b on b.hrmapping_gid = a.hrmapping_gid " +
                    " where hrmapping_name = 'Approver'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                             hrhead_approvedbyname = objdbconn.GetExecuteScalar(msSQL);
                        }
                        objODBCDatareader.Close();
                        //msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                        //    " from hrm_mst_temployee a " +
                        //    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                        //    " where employee_gid = '" + employee_gid + "'";
                        //string hrhead_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " update hrl_trn_trequest set request_status ='HRVerify Pending'," +
                    " hrhead_status='Approved' ," +
                    " hrhead_remarks='Auto Approved By System' ," +
                    " hrhead_approvedby='" + employee_gid + "'," +
                    " hrhead_approvedbyname='" + hrhead_approvedbyname + "'," +
                    //" request_status ='HRVerify Pending'," +
                    " hrhead_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where request_gid='" + values.request_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Functional Head approved successfully";
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
                            msSQL = " select  group_concat(distinct a.employee_gid, ',', a.reportingmgr_gid) as CC2members, " +
                                    " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount, " +
                                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                                    " a.created_date,a.created_by from hrl_trn_trequest a " +
                                    " left join hrm_mst_temployee d on a.employee_gid = d.employee_gid " +
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
                                lsemployee_name = objODBCDatareader["employee"].ToString();
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


                            sub = "Update Employee Financial Assistance repayment and T&C details";
                            body = "Dear Sir/Madam,";
                            body = body + "<br />";
                            body = body + "<br />";
                            body = body + "Greetings,  <br />";
                            body = body + "<br />";
                            body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has raised a financial assistance request. Login in one.samunnati.com and provide the loan details and Terms and conditions";
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
                }
            }
            else
            {

                values.status = false;
                values.message = "Approval can't be done,the query is still open";

            }

            return true;

        }
        public bool DaPostHrLoanFHRejectUpdate(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_tfhraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.fhopenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.fhopenquerycount == "0")
            {
                //msSQL = "update hrl_trn_trequest set status = 'N' where created_by='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string fh_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='FH Rejected'," +
                        " fh_status ='Rejected' ," +
                        " fh_remarks='" + values.fh_remarks.Replace("'", @"\'") + "' ," +
                        " fh_approvedby='" + employee_gid + "'," +
                        " fh_approvedbyname='" + fh_approvedbyname + "'," +
                        " fh_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Functional Head rejected successfully";
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
                    msSQL = " select  group_concat(distinct a.reportingmgr_gid,',',functionalhead_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount,a.hrhead_gid, " +
                            " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as hr_head, " +
                            " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                            " a.employee_name,a.created_date,a.created_by,a.employee_gid from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee b on a.hrhead_gid = b.employee_gid " +
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
                        lshr_head = objODBCDatareader["hr_head"].ToString();
                        lsTo2members = objODBCDatareader["employee_gid"].ToString();
                        lsemployee_name = objODBCDatareader["employee"].ToString();
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



                    sub = "HR Head Approval rejected for employee financial assistance.";
                    body = "Dear " + HttpUtility.HtmlEncode(lshr_head) + ",";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has rejected a financial assistance request";
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
                    body = body + "Click <a href=" + ConfigurationManager.AppSettings["URL"].ToString() + ">here</a>  to view and approve.<br />";
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
                values.message = "Reject can't be done,the query is still open";

            }
            return true;

        }
        public bool DaPostHrLoanHRApprovalUpdate(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_thrraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.hropenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.hropenquerycount == "0")
            {
                //msSQL = "update hrl_trn_trequest set status = 'N' where created_by='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string hrhead_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='HRVerify Pending'," +
                        " hrhead_status='Approved' ," +
                        " hrhead_remarks='" + values.hrhead_remarks.Replace("'", @"\'") + "' ," +
                        " hrhead_approvedby='" + employee_gid + "'," +
                        " hrhead_approvedbyname='" + hrhead_approvedbyname + "'," +
                        " request_status ='HRVerify Pending'," +
                        " hrhead_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "HR Head approved successfully";
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
                    msSQL = " select  group_concat(distinct a.employee_gid, ',', a.hrhead_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount, " +
                            " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                            " a.created_date,a.created_by from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee d on a.employee_gid = d.employee_gid " +
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
                        lsemployee_name = objODBCDatareader["employee"].ToString();
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


                    sub = "Update Employee Financial Assistance repayment and T&C details";
                    body = "Dear Sir/Madam,";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has raised a financial assistance request. Login in one.samunnati.com and provide the loan details and Terms and conditions";
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
        public bool DaPostHrLoanHRRejectUpdate(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
            msSQL = " select count(*) as openquery from hrl_apr_thrraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
            values.hropenquerycount = objdbconn.GetExecuteScalar(msSQL);
            if (values.hropenquerycount == "0")
            {
                //msSQL = "update hrl_trn_trequest set status = 'N' where created_by='" + employee_gid + "'";
                //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as employee_name " +
                    " from hrm_mst_temployee a " +
                    " left join adm_mst_tuser c on a.user_gid = c.user_gid " +
                    " where employee_gid = '" + employee_gid + "'";
                string hrhead_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='HR Rejected'," +
                        " hrhead_status ='Rejected' ," +
                        " hrhead_remarks='" + values.hrhead_remarks.Replace("'", @"\'") + "' ," +
                        " hrhead_approvedby='" + employee_gid + "'," +
                        " hrhead_approvedbyname='" + hrhead_approvedbyname + "'," +
                        " request_status='HR Rejected'," +
                        " hrhead_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where request_gid='" + values.request_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "HR Head rejected successfully";
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
                    msSQL = " select  group_concat(distinct a.reportingmgr_gid, ',', a.functionalhead_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount, " +
                            " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as employee, " +
                            " a.created_date,a.created_by,a.employee_gid from hrl_trn_trequest a " +
                            " left join hrm_mst_temployee d on a.employee_gid = d.employee_gid " +
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
                        lsemployee_name = objODBCDatareader["employee"].ToString();
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


                    msSQL = " select group_concat(employee_emailid) from  hrm_mst_temployee where employee_gid in (select b.employee_gid from hrl_mst_thrmapping a " +
                             " left join hrl_mst_thrmapping2employee b on a.hrmapping_gid = b.hrmapping_gid " +
                             " where hrmapping_name = 'Approver')";
                    cc_managermailid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);
                    if (cc_managermailid != null & cc_managermailid != string.Empty & cc_managermailid != "")
                    {
                        cc_mailid = cc_mailid + "," + cc_managermailid;
                    }


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);


                    sub = "Reject Employee Financial Assistance repayment and T&C details";
                    body = "Dear Sir/Madam,";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has rejected a financial assistance request. Login in one.samunnati.com and provide the loan details and Terms and conditions";
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
                values.message = "Reject can't be done,the query is still open";

            }
            return true;

        }
        public void DaGetEmployeeDetails(MdlMstHRLoanRequest objHRLoanRequest, string employee_gid)
        {
            try
            {

                msSQL = " select r.employee_gid,c.department_manager,a.user_code,a.user_firstname,a.user_gid , " +
                        " c.department_gid,c.department_name ,n.role_name, " +
                        " concat(rt.user_code, ' || ', rt.user_firstname, ' ', rt.user_lastname) as reporting_mgr," +
                        " concat(fh.user_code, ' || ', fh.user_firstname, ' ', fh.user_lastname) as functional_head, " +
                        " b.employee_personalno as personal_phone_no,b.personal_emailid, " +
                        " f.employee_gid as functionalhead_gid,r.employee_gid as reportingmgr_gid ," +
                        " b.employee_emailid,b.employee_mobileno " +
                        " FROM adm_mst_tuser a " +
                        " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment c on c.department_gid = b.department_gid " +
                        " left join hrm_mst_trole n on n.role_gid = b.role_gid " +
                        " left join hrm_mst_temployee r on b.employeereporting_to = r.employee_gid " +
                        " left join adm_mst_tuser rt on rt.user_gid = r.user_gid " +
                        " left join hrm_mst_temployee f on r.employeereporting_to = f.employee_gid  " +
                        " left join adm_mst_tuser fh on fh.user_gid = f.user_gid " +
                        " where b.employee_gid = '" + employee_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {

                    objHRLoanRequest.employee_name = objODBCDatareader["user_firstname"].ToString();
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
                }
                objODBCDatareader.Close();

                objHRLoanRequest.status = true;
            }
            catch
            {
                objHRLoanRequest.status = false;
            }

        }
        public void DaPostDRMRaiseQuery(MdlMstHRLoanDrmApproval values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("HRDR");
            msSQL = "Insert into hrl_apr_tdrmraisequery( " +
                   " drmraisequery_gid, " +
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
                         "request_status='Query Raised By DRM'" +
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
                    msSQL = " select  group_concat(distinct a.reportingmgr_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as reporting_mgr, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.reportingmgr_gid = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.reportingmgr_gid = b.employee_gid" +
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


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
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
        public void DaPostFHRaiseQuery(MdlMstHRLoanDrmApproval values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("HRFH");
            msSQL = "Insert into hrl_apr_tfhraisequery( " +
                   " fhraisequery_gid, " +
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
                        "request_status='Query Raised By FH'" +
                        " where request_gid='" + values.request_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Query raised  successfully";
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
                    msSQL = " select  group_concat(distinct a.functionalhead_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as functional_head, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.functionalhead_gid = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.functionalhead_gid = b.employee_gid" +
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


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
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
        public void DaPostHRRaiseQuery(MdlMstHRLoanDrmApproval values, string employee_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("HRHR");
            msSQL = "Insert into hrl_apr_thrraisequery( " +
                   " hrraisequery_gid, " +
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
                         "request_status='Query Raised By HR'" +
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
                    msSQL = " select  group_concat(distinct a.hrhead_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as hr_head, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.hrhead_gid = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.hrhead_gid = b.employee_gid" +
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


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
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
        public void DaGetDRMRaiseQuery(string request_gid, MdlMstHRLoanDrmApproval values)
        {
            msSQL = " select a.request_gid,a.drmraisequery_gid,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from hrl_apr_tdrmraisequery a " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.request_gid = '" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdrmraisequery_list = new List<drmraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdrmraisequery_list.Add(new drmraisequery_list
                    {
                        request_gid = (dr_datarow["request_gid"].ToString()),
                        drmraisequery_gid = (dr_datarow["drmraisequery_gid"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.drmraisequery_list = getdrmraisequery_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetFHRaiseQuery(string request_gid, MdlMstHRLoanDrmApproval values)
        {
            msSQL = " select a.request_gid,a.fhraisequery_gid,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from hrl_apr_tfhraisequery a " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.request_gid = '" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfhraisequery_list = new List<fhraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getfhraisequery_list.Add(new fhraisequery_list
                    {
                        request_gid = (dr_datarow["request_gid"].ToString()),
                        fhraisequery_gid = (dr_datarow["fhraisequery_gid"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.fhraisequery_list = getfhraisequery_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetHRRaiseQuery(string request_gid, MdlMstHRLoanDrmApproval values)
        {
            msSQL = " select a.request_gid,a.hrraisequery_gid,a.query_title,a.query_description,a.raisequery_status,a.queryresponse_remarks," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(d.user_firstname, ' ', d.user_lastname, ' / ', d.user_code) as created_by, " +
                    " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as query_responseby " +
                    " from hrl_apr_thrraisequery a " +
                    " left join hrm_mst_temployee c on a.created_by = c.employee_gid" +
                     " left join hrm_mst_temployee e on a.query_responseby = e.employee_gid" +
                    " left join adm_mst_tuser d on c.user_gid = d.user_gid " +
                    " left join adm_mst_tuser f on e.user_gid = f.user_gid " +
                    " where a.request_gid = '" + request_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gethrraisequery_list = new List<hrraisequery_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gethrraisequery_list.Add(new hrraisequery_list
                    {
                        request_gid = (dr_datarow["request_gid"].ToString()),
                        hrraisequery_gid = (dr_datarow["hrraisequery_gid"].ToString()),
                        query_title = (dr_datarow["query_title"].ToString()),
                        query_description = (dr_datarow["query_description"].ToString()),
                        queryresponse_remarks = (dr_datarow["queryresponse_remarks"].ToString()),
                        query_responseby = (dr_datarow["query_responseby"].ToString()),
                        raisequery_status = (dr_datarow["raisequery_status"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())

                    });
                }
                values.hrraisequery_list = gethrraisequery_list;
            }
            dt_datatable.Dispose();
        }
        public void DaPostDRMresponsequery(MdlMstHRLoanDrmApproval values, string employee_gid)
        {

            msSQL = " update hrl_apr_tdrmraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", @"\'") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " raisequery_status='Closed' " +
                   " where drmraisequery_gid='" + values.drmraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                msSQL = " select count(*) as openquery from hrl_apr_tdrmraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
                values.drmopenquerycount = objdbconn.GetExecuteScalar(msSQL);
                if (values.drmopenquerycount == "0")
                {
                    msSQL = " update hrl_trn_trequest set " +
                       " raisequery_status='Closed', " +
                       "request_status='DRM Pending'" +
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

                    msSQL = " select  group_concat(distinct a.reportingmgr_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as reporting_mgr, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.reportingmgr_gid = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.reportingmgr_gid = b.employee_gid" +
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
                        lsTo2members = objODBCDatareader["created_by"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select query_title from hrl_apr_tdrmraisequery  " +
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


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee a " +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                            " where b.user_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);



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
                           "'" + values.drmraisequery_gid + "'," +
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
        public void DaPostFHresponsequery(MdlMstHRLoanDrmApproval values, string employee_gid)
        {
           
                msSQL = " update hrl_apr_tfhraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", @"\'") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " raisequery_status='Closed' " +
                   " where fhraisequery_gid='" + values.fhraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select count(*) as openquery from hrl_apr_tfhraisequery where request_gid = '" + values.request_gid + "'" +
               " and raisequery_status = 'Query Raised'";
                values.fhopenquerycount = objdbconn.GetExecuteScalar(msSQL);
                if (values.fhopenquerycount == "0")
                {

                    msSQL = " update hrl_trn_trequest set " +
                       " raisequery_status='Closed', " +
                        "request_status='FH Pending'" +
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

                    msSQL = " select  group_concat(distinct a.functionalhead_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as functional_head, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.functionalhead_gid = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.functionalhead_gid = b.employee_gid" +
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
                        lsTo2members = objODBCDatareader["created_by"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select query_title from hrl_apr_tfhraisequery  " +
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


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee a " +
                      " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                          " where b.user_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);



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
                           "'" + values.fhraisequery_gid + "'," +
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
        public void DaPostHRresponsequery(MdlMstHRLoanDrmApproval values, string employee_gid)
        {

            msSQL = " update hrl_apr_thrraisequery set queryresponse_remarks ='" + values.queryresponse_remarks.Replace("'", @"\'") + "'," +
                   " query_responseby='" + employee_gid + "'," +
                   " query_responsedate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " raisequery_status='Closed' " +
                   " where hrraisequery_gid='" + values.hrraisequery_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult == 1)
            {
                msSQL = " select count(*) as openquery from hrl_apr_thrraisequery where request_gid = '" + values.request_gid + "'" +
              " and raisequery_status = 'Query Raised'";
                values.hropenquerycount = objdbconn.GetExecuteScalar(msSQL);
                if (values.hropenquerycount == "0")
                {
                    msSQL = " update hrl_trn_trequest set " +
                       " raisequery_status='Closed', " +
                         "request_status='HR Pending'" +
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

                    msSQL = " select  group_concat(distinct a.hrhead_gid)  as CC2members," +
                           " a.request_refno, a.fintype_name,a.employee_gid,  a.employee_name,a.department_name, " +
                           " a.created_by, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as hr_head, " +
                           " a.created_date  from hrl_trn_trequest a" +
                           " left join hrl_mst_thrmapping2employee d on a.hrhead_gid = d.employee_gid" +
                            " left join hrl_mst_thrmapping h on h.hrmapping_gid = d.hrmapping_gid" +
                           " left join hrm_mst_temployee b on a.hrhead_gid = b.employee_gid" +
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
                        lsTo2members = objODBCDatareader["created_by"].ToString();
                        lscreated_date = objODBCDatareader["created_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = "select query_title from hrl_apr_thrraisequery  " +
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


                    msSQL = " select group_concat(employee_emailid)  from hrm_mst_temployee a " +
                       " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                           " where b.user_gid in ('" + lsTo2members.Replace(",", "', '") + "')";
                    cc_mailid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select group_concat(employee_emailid) from hrm_mst_temployee where employee_gid in ('" + lscc2members.Replace(",", "', '") + "')";
                    lsto_mail = objdbconn.GetExecuteScalar(msSQL);



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
                        "'" + values.hrraisequery_gid + "'," +
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
        public void DaPostHrLoantremsandconditionacknwlg(MdlMstHRLoanDrmApproval values, string employee_gid)
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
                string hrdocverify_approvedbyname = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update hrl_trn_trequest set request_status ='Reupload Completed'," +
                        " hrdocverify_status='Verified' ," +
                        " hrdocverify_by='" + employee_gid + "'," +
                        " hrdocverify_byname='" + hrdocverify_approvedbyname + "'," +
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
                    msSQL = " select  group_concat(distinct a.employee_gid) as CC2members, " +
                            " a.request_refno, a.fintype_name,a.purpose_name,a.severity_name,a.amount, " +
                            " a.employee_name,a.created_date,a.created_by from hrl_trn_trequest a " +
                            " where a.request_gid = '" + values.request_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscc2members = objODBCDatareader["CC2members"].ToString();
                        lsrequest_refno = objODBCDatareader["request_refno"].ToString();
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


                    sub = "Employee Financial Assistance repayment and T&C details";
                    body = "Dear Sir/Madam,";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + HttpUtility.HtmlEncode(lsemployee_name) + " has accepted the T&C and uploaded the documents ";
                    body = body + "<br />";
                    body = body + "<br />";
                    body = body + "Login in one.samunnati.com and vet the soft copies";
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
                    values.status = true;
                    values.message = "Document reupload successfully";

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
        }
    }
}