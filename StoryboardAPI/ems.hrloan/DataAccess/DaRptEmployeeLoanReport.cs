using ems.hrloan.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Linq;
using ems.storage.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ems.hrloan.DataAccess
{
    public class DaRptEmployeeLoanReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;      
        OdbcDataReader objODBCDataReader;
        string msSQL;
       

        //count
        public void DaGetReportCounts(EmployeeLoanReportcount values,string employee_gid)
        {
            msSQL = "SELECT COUNT(*) as TotalCount FROM hrl_trn_trequest ";                    
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.total_count = objODBCDataReader["TotalCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(*) as PendingCount FROM hrl_trn_trequest WHERE request_status='DRM Pending' "+
                      "or request_status='Query Raised By DRM' or request_status='Query Raised By FH'or request_status='Query Raised By HR' or request_status='Query Raised By Manager' or request_status='FH Pending'  or request_status='HR Pending' " +
                      "or request_status='Reupload Pending'  or request_status='HRVerify Pending'  or request_status='Reupload Completed'  or  request_status='HRVerify Approved'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.pending_count = objODBCDataReader["PendingCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(*) as RejectedCount FROM hrl_trn_trequest WHERE request_status='DRM Rejected' "+
                    "or request_status='FH Rejected' or request_status='HR Rejected' or request_status='HRVerify Rejected'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.rejected_count = objODBCDataReader["RejectedCount"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "SELECT COUNT(*) as CompletedCount FROM hrl_trn_trequest WHERE request_status='Payment Completed' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.completed_count = objODBCDataReader["CompletedCount"].ToString();
            }
            objODBCDataReader.Close();
            msSQL = "SELECT COUNT(*) as WithdrawnCount FROM hrl_trn_trequest WHERE request_status='Withdrawn' ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                objODBCDataReader.Read();
                values.WithdrawnCount = objODBCDataReader["WithdrawnCount"].ToString();
            }
            objODBCDataReader.Close();
        }

        public void DaGetEmployeeLoanReportSummary(MdlRptEmployeeLoanReport objRptSummary)
        {
            msSQL = " select request_gid, request_refno, request_status, fintype_name, " +
                      " employee_gid, employee_name, employee_role, department_name, " +
                      " created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                      " raised_department,  amount,  purpose_name,  severity_name, tenure" +
                      //" case when request_status = 'DRM Pending' then 'DRM Pending' " +
                      //" when request_status = 'DRM Approved' then 'FH Pending' " +
                      //" when request_status = 'FH Approved' then 'HR Pending' " +
                      //" when request_status = 'HR Approved' then 'HRVerify Pending' " +
                      //" when request_status = 'Reupload Pending' then 'HRVerify Pending' " +
                      //" when request_status = 'Reupload Completed' then 'HRVerify Pending' " +
                      //" when request_status = 'HRVerify Approved' then 'HRPayment Pending' " +
                      //" when request_status = 'Payment Completed' then 'Payment Completed' " +
                      //" when request_status = 'Withdrawn' then 'Withdrawn' " +
                      //" end as status1 " +
                      " from hrl_trn_trequest a order by request_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getRptSummaryList = new List<SummaryReport_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getRptSummaryList.Add(new SummaryReport_list
                    {
                        request_gid = dt["request_gid"].ToString(),
                        request_refno = dt["request_refno"].ToString(),
                        request_status = dt["request_status"].ToString(),
                        //status1 = dt["status1"].ToString(),
                        fintype_name = dt["fintype_name"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                        employee_role = dt["employee_role"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        amount = dt["amount"].ToString(),

                    });
                    objRptSummary.SummaryReport_list = getRptSummaryList;
                }
            }
            dt_datatable.Dispose();
            objRptSummary.status = true;
            objRptSummary.message = "Success";
        }
        public void DaGetExportLoanReport(MdlRptEmployeeLoanReport values)
        {
            msSQL = " select request_refno as `Reference Number`,employee_name as `Requestor Name`,  " +
                       "   employee_role as `Requestor Role`,entity_name as ` Entity`, department_name as `Department`,fintype_name as `Financial Type`, " +
                       "  purpose_name as `Purpose  `, amount  as ` Amount`,  severity_name as `Severity `, tenure as `Tenure `,interest as `Interest `," +
                       " date_format(a.created_date,'%d-%m-%Y %h:%i %p')   as `Request Raised On`, request_status as `Request Status`,drm_approvedbyname as `Direct Reporting Manager `," +
                       "fh_approvedbyname as `Functional Head `,hrhead_approvedbyname as `HR Head`,date_format(a.drm_updateddate,'%d-%m-%Y %h:%i %p') as `DRM Approved Date `,date_format(a.fh_updateddate,'%d-%m-%Y %h:%i %p') as `FH Approved Date`,date_format(a.hrhead_updateddate,'%d-%m-%Y %h:%i %p') as `HR Approved Date`," +
                       "drm_remarks as `DRM Approved Remarks`,fh_remarks as `FH Approved Remarks`,hrhead_remarks as `HR Approved Remarks`,hrverify_approvedbyname  as `HR Manager` ,date_format(a.hrverify_updateddate,'%d-%m-%Y %h:%i %p')  as `HR Manager Approved Date`" +
                       ",hrverify_remarks as `HR Manager Approved Remarks`,raisequery_status as `Query Status` ,hrpayment_approvedbyname as `Financial Approved By`,date_format(a.hrpayment_updateddate,'%d-%m-%Y %h:%i %p')  as `Financial Approved Date`,hrpayment_remarks as `Financial Approved Remarks`," +
                       "  approved_interest as `Approved Interest`," +
                       "  approved_tenure as `Approved Tenure`" +
                       " from hrl_trn_trequest a order by request_gid desc";


            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("Loan_Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lsname = "EmployeeLoan_Report.xlsx";
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //objExportBankReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname;
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/EmployeeLoanReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/EmployeeLoanReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "HRL/EmployeeLoanReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 31])  //Address "A1:A19"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "HRL/EmployeeLoanReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                values.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "HRL/EmployeeLoanReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                values.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "HRL/EmployeeLoanReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);

                ms.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
            values.status = true;
            values.message = "Success";
        }
    }
}
