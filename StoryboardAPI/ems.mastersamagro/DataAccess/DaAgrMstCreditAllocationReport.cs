using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Reflection;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to Export Credit Report., Credit Summary
    /// </summary>
    /// <remarks>Written by Abilash.A, Premchander.K </remarks>
    public class DaAgrMstCreditAllocationReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL;

        //CreditSummary
        public void DaMstCreditSummary(MdlAgrMstCreditAllocationReport objMstCreditSummary)
        {
            msSQL = " select a.application_gid, a.application_no,a.customerref_name as customer_name,a.vertical_name, a.region,a.applicant_type, " +
                    "a.creditgroup_name,date_format(a.creditassigned_date, '%d-%m-%Y %h:%i %p')as 'creditassigned_date', " +
                    "concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as creditassigned_by,a.remarks," +
                    "a.credithead_name,a.creditnationalmanager_name,a.creditregionalmanager_name," +
                    " date_format(a.headapproval_date, '%d-%m-%Y %h:%i %p') as headapproval_date,a.creditmanager_name,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " (h.approved_date) as ccsubmitted_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from agr_mst_tapplication a" +
                    " left join agr_trn_tAppcreditapproval h on (a.application_gid = h.application_gid and h.hierary_level = '3') " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on a.creditassigned_by = f.employee_gid " +
                    " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                    " where a.approval_status not in ('Incomplete','Pending','Submitted to Approval','Hold By Business','Rejected By Business') " +
                    " order by a.application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);


            var objGetMstCreditSummary = new List<MstCreditSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    objGetMstCreditSummary.Add(new MstCreditSummaryList
                    {
                        application_gid = dr_datarow["application_gid"].ToString(),
                        application_no = dr_datarow["application_no"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        vertical_name = dr_datarow["vertical_name"].ToString(),
                        region = dr_datarow["region"].ToString(),
                        overalllimit_amount = dr_datarow["overalllimit_amount"].ToString(),
                        creditgroup_name = dr_datarow["creditgroup_name"].ToString(),
                        headapproval_date = dr_datarow["headapproval_date"].ToString(),
                        creditassigned_to = dr_datarow["creditmanager_name"].ToString(),
                        creditassigned_date = dr_datarow["creditassigned_date"].ToString(),
                        creditassigned_by = dr_datarow["creditassigned_by"].ToString(),
                        creditregionalmanager_name = dr_datarow["creditregionalmanager_name"].ToString(),
                        creditnationalmanager_name = dr_datarow["creditnationalmanager_name"].ToString(),
                        credithead_name = dr_datarow["credithead_name"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        ccsubmitted_date = dr_datarow["ccsubmitted_date"].ToString(),
                        updated_date = dr_datarow["updated_date"].ToString(),
                        updated_by = dr_datarow["updated_by"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objMstCreditSummary.MstCreditSummaryList = objGetMstCreditSummary;
            }
            dt_datatable.Dispose();
            objMstCreditSummary.status = true;
            objMstCreditSummary.message = "Success";
        }

        public void DaExportMstCreditReport(MdlAgrMstCreditAllocationReport objMstCreditAllocationReport)
        {
            msSQL = " select a.application_gid, a.application_no, a.customerref_name as customer_name,a.vertical_name, a.region,a.applicant_type, " +
                    "a.creditgroup_name,date_format(a.creditassigned_date, '%d-%m-%Y %h:%i %p') as 'creditassigned_date' , " +
                    " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as creditassigned_by,a.remarks," +
                    "a.credithead_name,a.creditnationalmanager_name,a.creditregionalmanager_name," +
                    " date_format(a.headapproval_date, '%d-%m-%y %h:%i %p') as 'headapproval_date', a.creditmanager_name,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " (h.approved_date) as ccsubmitted_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from agr_mst_tapplication a" +
                    " left join agr_trn_tAppcreditapproval h on (a.application_gid = h.application_gid and h.hierary_level = '3') " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on a.creditassigned_by = f.employee_gid " +
                    " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                    " where a.approval_status not in ('Incomplete','Pending','Submitted to Approval','Hold By Business','Rejected By Business') " +
                    " order by a.application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var objGetMstCreditSummary = new List<MstCreditExcelList>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    objGetMstCreditSummary.Add(new MstCreditExcelList
                    {
                        Application_Ref_number = dr_datarow["application_no"].ToString(),
                        Application_Name = dr_datarow["customer_name"].ToString(),
                        Submitted_Date = dr_datarow["submitted_date"].ToString(),
                        Submitted_By = dr_datarow["created_by"].ToString(),
                        Vertical = dr_datarow["vertical_name"].ToString(),
                        CustomerSupplier_Type = dr_datarow["vertical_name"].ToString(),
                        Region = dr_datarow["region"].ToString(),
                        Overall_Limit = dr_datarow["overalllimit_amount"].ToString(),
                        ProductCreditgroup = dr_datarow["creditgroup_name"].ToString(),
                        Approval_Date = dr_datarow["headapproval_date"].ToString(),
                        Allocation_To = dr_datarow["creditmanager_name"].ToString(),
                        Allocation_Date = dr_datarow["creditassigned_date"].ToString(),
                        Allocation_By = dr_datarow["creditassigned_by"].ToString(),
                        RCM = dr_datarow["creditregionalmanager_name"].ToString(),
                        NCM = dr_datarow["creditnationalmanager_name"].ToString(),
                        CH = dr_datarow["credithead_name"].ToString(),
                        Allocation_Remark = dr_datarow["remarks"].ToString(),
                        Status = dr_datarow["approval_status"].ToString(),
                        RCM_Approved_Reject_Remark = objdbconn.GetExecuteScalar("select approval_remarks from agr_trn_tAppcreditapproval where application_gid = '" + dr_datarow["application_gid"].ToString() + "' and hierary_level = '1'"),
                        NCM_Approved_Reject_Remark = objdbconn.GetExecuteScalar("select approval_remarks from agr_trn_tAppcreditapproval where application_gid = '" + dr_datarow["application_gid"].ToString() + "' and hierary_level = '2'"),
                        CH_Approved_Reject_Remark = objdbconn.GetExecuteScalar("select approval_remarks from agr_trn_tAppcreditapproval where application_gid = '" + dr_datarow["application_gid"].ToString() + "' and hierary_level = '3'"),
                        CC_Submitted_Date = dr_datarow["ccsubmitted_date"].ToString()

                    });
                }
                objMstCreditAllocationReport.MstCreditExcelList = objGetMstCreditSummary;

            }
            dt_datatable.Dispose();
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            ListtoDataTable lsttodt = new ListtoDataTable();
            DataTable creditlist = lsttodt.ToDataTable(objGetMstCreditSummary);
            var workSheet = excel.Workbook.Worksheets.Add("Credit Allocation Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstCreditAllocationReport.lsname = "Credit Allocation Report.xlsx";
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //objMstCreditAllocationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCreditAllocationReport.lsname;
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstCreditAllocationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCreditAllocationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(creditlist, true);
                FileInfo file = new FileInfo(objMstCreditAllocationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 21])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objMstCreditAllocationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCreditAllocationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstCreditAllocationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstCreditAllocationReport.status = false;
                objMstCreditAllocationReport.message = "Failure";
            }
            objMstCreditAllocationReport.lscloudpath = objcmnstorage.EncryptData(objMstCreditAllocationReport.lscloudpath);
            objMstCreditAllocationReport.lspath = objcmnstorage.EncryptData(objMstCreditAllocationReport.lspath);
            objMstCreditAllocationReport.status = true;
            objMstCreditAllocationReport.message = "Success";
        }

        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }

        //Supplier CreditSummary
        public void DaMstSupplierCreditSummary(MdlAgrMstCreditAllocationReport objMstCreditSummary)
        {
            msSQL = " select a.application_gid, a.application_no,a.customerref_name as customer_name,a.vertical_name, a.region,a.applicant_type, " +
                    "a.creditgroup_name,date_format(a.creditassigned_date, '%d-%m-%Y %h:%i %p')as 'creditassigned_date', " +
                    "concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as creditassigned_by,a.remarks," +
                    "a.credithead_name,a.creditnationalmanager_name,a.creditregionalmanager_name," +
                    " date_format(a.headapproval_date, '%d-%m-%Y %h:%i %p') as headapproval_date,a.creditmanager_name,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " (h.approved_date) as ccsubmitted_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from agr_mst_tsuprapplication a" +
                    " left join agr_trn_tsuprappcreditapproval h on (a.application_gid = h.application_gid and h.hierary_level = '3') " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on a.creditassigned_by = f.employee_gid " +
                    " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                    " where a.approval_status not in ('Incomplete','Pending','Submitted to Approval','Hold By Business','Rejected By Business') " +
                    " order by a.application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);


            var objGetMstCreditSummary = new List<MstCreditSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    objGetMstCreditSummary.Add(new MstCreditSummaryList
                    {
                        application_gid = dr_datarow["application_gid"].ToString(),
                        application_no = dr_datarow["application_no"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        vertical_name = dr_datarow["vertical_name"].ToString(),
                        region = dr_datarow["region"].ToString(),
                        overalllimit_amount = dr_datarow["overalllimit_amount"].ToString(),
                        creditgroup_name = dr_datarow["creditgroup_name"].ToString(),
                        headapproval_date = dr_datarow["headapproval_date"].ToString(),
                        creditassigned_to = dr_datarow["creditmanager_name"].ToString(),
                        creditassigned_date = dr_datarow["creditassigned_date"].ToString(),
                        creditassigned_by = dr_datarow["creditassigned_by"].ToString(),
                        creditregionalmanager_name = dr_datarow["creditregionalmanager_name"].ToString(),
                        creditnationalmanager_name = dr_datarow["creditnationalmanager_name"].ToString(),
                        credithead_name = dr_datarow["credithead_name"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        ccsubmitted_date = dr_datarow["ccsubmitted_date"].ToString(),
                        updated_date = dr_datarow["updated_date"].ToString(),
                        updated_by = dr_datarow["updated_by"].ToString(),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                objMstCreditSummary.MstCreditSummaryList = objGetMstCreditSummary;
            }
            dt_datatable.Dispose();
            objMstCreditSummary.status = true;
            objMstCreditSummary.message = "Success";
        }

        public void DaExportMstSupplierCreditReport(MdlAgrMstCreditAllocationReport objMstCreditAllocationReport)
        {
            msSQL = " select a.application_gid, a.application_no, a.customerref_name as customer_name,a.vertical_name, a.region,a.applicant_type, " +
                    "a.creditgroup_name,date_format(a.creditassigned_date, '%d-%m-%Y %h:%i %p') as 'creditassigned_date' , " +
                    " concat(g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as creditassigned_by,a.remarks," +
                    "a.credithead_name,a.creditnationalmanager_name,a.creditregionalmanager_name," +
                    " date_format(a.headapproval_date, '%d-%m-%y %h:%i %p') as 'headapproval_date', a.creditmanager_name,a.approval_status,a.overalllimit_amount," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p')as 'created_date'," +
                    " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                    " date_format(a.submitted_date, '%d-%m-%Y %h:%i %p')as 'submitted_date'," +
                    " date_format(a.updated_date, '%d-%m-%Y %h:%i %p')as 'updated_date'," +
                    " (h.approved_date) as ccsubmitted_date," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) " +
                    " as 'updated_by' from agr_mst_tsuprapplication a" +
                    " left join agr_trn_tsuprAppcreditapproval h on (a.application_gid = h.application_gid and h.hierary_level = '3') " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                    " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on a.creditassigned_by = f.employee_gid " +
                    " left join adm_mst_tuser g on f.user_gid = g.user_gid " +
                    " where a.approval_status not in ('Incomplete','Pending','Submitted to Approval','Hold By Business','Rejected By Business') " +
                    " order by a.application_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            var objGetMstCreditSummary = new List<MstCreditExcelList>();
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    objGetMstCreditSummary.Add(new MstCreditExcelList
                    {
                        Application_Ref_number = dr_datarow["application_no"].ToString(),
                        Application_Name = dr_datarow["customer_name"].ToString(),
                        Submitted_Date = dr_datarow["submitted_date"].ToString(),
                        Submitted_By = dr_datarow["created_by"].ToString(),
                        //Vertical = dr_datarow["vertical_name"].ToString(),
                        CustomerSupplier_Type = dr_datarow["vertical_name"].ToString(),
                        Region = dr_datarow["region"].ToString(),
                        Overall_Limit = dr_datarow["overalllimit_amount"].ToString(),
                        ProductCreditgroup = dr_datarow["creditgroup_name"].ToString(),
                        Approval_Date = dr_datarow["headapproval_date"].ToString(),
                        Allocation_To = dr_datarow["creditmanager_name"].ToString(),
                        Allocation_Date = dr_datarow["creditassigned_date"].ToString(),
                        Allocation_By = dr_datarow["creditassigned_by"].ToString(),
                        RCM = dr_datarow["creditregionalmanager_name"].ToString(),
                        NCM = dr_datarow["creditnationalmanager_name"].ToString(),
                        CH = dr_datarow["credithead_name"].ToString(),
                        Allocation_Remark = dr_datarow["remarks"].ToString(),
                        Status = dr_datarow["approval_status"].ToString(),
                        RCM_Approved_Reject_Remark = objdbconn.GetExecuteScalar("select approval_remarks from agr_trn_tAppcreditapproval where application_gid = '" + dr_datarow["application_gid"].ToString() + "' and hierary_level = '1'"),
                        NCM_Approved_Reject_Remark = objdbconn.GetExecuteScalar("select approval_remarks from agr_trn_tAppcreditapproval where application_gid = '" + dr_datarow["application_gid"].ToString() + "' and hierary_level = '2'"),
                        CH_Approved_Reject_Remark = objdbconn.GetExecuteScalar("select approval_remarks from agr_trn_tAppcreditapproval where application_gid = '" + dr_datarow["application_gid"].ToString() + "' and hierary_level = '3'"),
                        CC_Submitted_Date = dr_datarow["ccsubmitted_date"].ToString()

                    });
                }
                objMstCreditAllocationReport.MstCreditExcelList = objGetMstCreditSummary;

            }
            dt_datatable.Dispose();
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            ListtoDataTable lsttodt = new ListtoDataTable();
            DataTable creditlist = lsttodt.ToDataTable(objGetMstCreditSummary);
            var workSheet = excel.Workbook.Worksheets.Add("Supplier Credit Allocation Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objMstCreditAllocationReport.lsname = "Supplier Credit Allocation Report.xlsx";
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //objMstCreditAllocationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCreditAllocationReport.lsname;
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Supplier Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objMstCreditAllocationReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/Supplier Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCreditAllocationReport.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(creditlist, true);
                FileInfo file = new FileInfo(objMstCreditAllocationReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 21])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                objMstCreditAllocationReport.lscloudpath = lscompany_code + "/" + "SamAgro/Supplier Credit Allocation Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objMstCreditAllocationReport.lsname;
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", objMstCreditAllocationReport.lscloudpath, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objMstCreditAllocationReport.status = false;
                objMstCreditAllocationReport.message = "Failure";
            }
            objMstCreditAllocationReport.lscloudpath = objcmnstorage.EncryptData(objMstCreditAllocationReport.lscloudpath);
            objMstCreditAllocationReport.lspath = objcmnstorage.EncryptData(objMstCreditAllocationReport.lspath);
            objMstCreditAllocationReport.status = true;
            objMstCreditAllocationReport.message = "Success";
        }
    }
}