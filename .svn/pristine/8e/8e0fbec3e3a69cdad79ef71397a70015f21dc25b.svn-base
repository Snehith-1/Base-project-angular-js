using ems.idas.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using System.IO;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasTrnLsaReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid;
        int mnResult;
        string lsmaster_value;

        public void DaGetidasLsaSummary(idasTrnLsaReportSummary values)
        {
            msSQL = "select a.lsacreate_gid, a.customer_name,a.customer_urn," +
                    " a.sanctionref_no," + " date_format(a.sanction_date, '%d-%m-%Y %h:%i %p') as sanction_date," +
                    " a.approved_by," + " date_format(a.approved_date, '%d-%m-%Y') as approved_date," +
                    " date_format(a.lsacreated_date,'%d-%m-%Y %h:%i %p') as lsacreated_date," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as 'created_date'," +
                    " a.lsaref_no," + "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code)" +
                    " as created_by,a.proceed_flag,  a.approval_status," + " date_format(a.approvalupdated_date, '%d-%m-%Y %h:%i %p')" +
                    " as lsaapproved_date," + " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code)" +
                    " as lsaapproved_by from ids_trn_tlsa a " +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                    " left join hrm_mst_temployee d on a.approvalupdated_by=d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid "+
                    " where a.approval_status ='Approved' group by a.lsacreate_gid order by a.lsacreate_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetIdasTrnLsaReport = new List<idasTrnLsaReportSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetIdasTrnLsaReport.Add(new idasTrnLsaReportSummaryList
                    {

                        lsaref_no = dr_datarow["lsaref_no"].ToString(),
                        lsacreated_date = dr_datarow["lsacreated_date"].ToString(),
                        sanctionref_no = dr_datarow["sanctionref_no"].ToString(),
                        sanction_date = dr_datarow["sanction_date"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        lsaapproved_by = dr_datarow["lsaapproved_by"].ToString(),
                        lsaapproved_date = dr_datarow["lsaapproved_date"].ToString(),
                        customername = dr_datarow["customer_name"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString())
                    });
                }
                values.idasTrnLsaReportSummaryList = objGetIdasTrnLsaReport;
            }
            dt_datatable.Dispose();
            values.status = true;
            values.message = "Success";
        }

        //filter
        public void Dalsafilter(idasTrnLsaReportSummary objIdasTrnLsaReport)
        {
            try
            {
                msSQL = "select a.lsacreate_gid, a.customer_name,a.customer_urn," +
                    " a.sanctionref_no," + " date_format(a.sanction_date, '%d-%m-%Y') as sanction_date," +
                    " a.approved_by," + " date_format(a.approved_date, '%d-%m-%Y') as approved_date," +
                    " date_format(a.lsacreated_date,'%d-%m-%Y %h:%i %p') as lsacreated_date," +
                    " date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as 'created_date'," +
                    " a.lsaref_no," + "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code)" +
                    " as created_by,a.proceed_flag,  a.approval_status," + " date_format(a.approvalupdated_date, '%d-%m-%Y')" +
                    " as lsaapproved_date," + " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code)" +
                    " as lsaapproved_by from ids_trn_tlsa a " +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                    " left join ocs_mst_tcustomer2sanction f on a.customer2sanction_gid = f.customer2sanction_gid" +
                    " left join ocs_mst_tcustomer g on f.customer_gid= g.customer_gid " +
                    " left join hrm_mst_temployee d on a.approvalupdated_by=d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                    " where a.approval_status ='Approved' ";

                if (objIdasTrnLsaReport.customer_gid == null || objIdasTrnLsaReport.customer_gid == "")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and g.customer_gid = '" + objIdasTrnLsaReport.customer_gid + "'";
                }

                if (objIdasTrnLsaReport.customer2sanction_gid == null || objIdasTrnLsaReport.customer2sanction_gid == "" || objIdasTrnLsaReport.customer2sanction_gid == "All")
                {
                    msSQL += " and 1=1 ";
                }
                else
                {
                    msSQL += " and a.customer2sanction_gid = '" + objIdasTrnLsaReport.customer2sanction_gid + "'";
                }

                msSQL += " order by a.lsacreate_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objGetIdasTrnLsaReport = new List<idasTrnLsaReportSummaryList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objGetIdasTrnLsaReport.Add(new idasTrnLsaReportSummaryList
                        {
                            lsaref_no = (dr_datarow["lsaref_no"].ToString()),
                            lsacreated_date = (dr_datarow["lsacreated_date"].ToString()),
                            sanctionref_no = (dr_datarow["sanctionref_no"].ToString()),
                            sanction_date = (dr_datarow["sanction_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            approval_status = (dr_datarow["approval_status"].ToString()),
                            lsaapproved_by = (dr_datarow["lsaapproved_by"].ToString()),
                            lsaapproved_date = (dr_datarow["lsaapproved_date"].ToString()),
                            customername = (dr_datarow["customer_name"].ToString()),
                            customer_urn = (dr_datarow["customer_urn"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                        });
                    }
                    objIdasTrnLsaReport.idasTrnLsaReportSummaryList = objGetIdasTrnLsaReport;
                }
                dt_datatable.Dispose();
                objIdasTrnLsaReport.status = true;
                objIdasTrnLsaReport.message = "Success";
            }
            catch
            {
                objIdasTrnLsaReport.status = false;
                objIdasTrnLsaReport.message = "Failure";
            }
        }

        // export excel

        public void DaIdasExportExcel(idasLsaReportSummary objidasLsaReportSummary)
        {
            msSQL = " select lsaref_no as ` LSA Ref No`, date_format(a.created_date, '%d-%m-%Y') as `LSA Created Date`," +
                    " date_format(approvalupdated_date, '%d-%m-%Y') as `LSA Approval Date`," +
                    " c.customername as `Customer Name`,c.customer_urn as `Customer URN`,"  +
                    " b.sanction_refno as SanctionRef_No, date_format(b.sanction_date, '%d-%m-%Y') as Sanction_Date," +
                    " f.document_limit as Documented_Limit, f.limit_released as Limit_To_Be_Released "+
                    " from ids_trn_tlsa a " +
                    " left join ocs_mst_tcustomer2sanction b on a.customer2sanction_gid = b.customer2sanction_gid" +
                    " left join ocs_mst_tcustomer c on b.customer_gid = c.customer_gid" +
                    " left join ids_trn_tdocumentcharges d on a.lsacreate_gid=d.lsacreate_gid" +
                    " left join ids_trn_tprocessingfees e on a.lsacreate_gid=e.lsacreate_gid" +
                    " left join ids_trn_tlimitinfodtl f on a.lsacreate_gid=f.lsacreate_gid" +
                    " where a.approval_status ='Approved' ";

            if (objidasLsaReportSummary.customer_gid == null || objidasLsaReportSummary.customer_gid == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and c.customer_gid = '" + objidasLsaReportSummary.customer_gid + "'";
            }

            if (objidasLsaReportSummary.customer2sanction_gid == null || objidasLsaReportSummary.customer2sanction_gid == "" || objidasLsaReportSummary.customer2sanction_gid == "All")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and b.customer2sanction_gid = '" + objidasLsaReportSummary.customer2sanction_gid + "'";
            }

            msSQL += " order by a.lsacreate_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("LSA List");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objidasLsaReportSummary.lsname = "LSA Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Idas/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objidasLsaReportSummary.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Idas/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objidasLsaReportSummary.lsname;
                objidasLsaReportSummary.lscloudpath =lscompany_code + "/" + "Idas/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objidasLsaReportSummary.lsname;
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objidasLsaReportSummary.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 9])  //Address "A1:A9"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Idas/LSA Report/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objidasLsaReportSummary.lsname, ms);
                ms.Close();

            }
            catch (Exception ex)
            {
                objidasLsaReportSummary.status = false;
                objidasLsaReportSummary.message = "Failure";
            }
            objidasLsaReportSummary.status = true;
            objidasLsaReportSummary.message = "Success";
            objidasLsaReportSummary.lscloudpath = objcmnstorage.EncryptData(objidasLsaReportSummary.lscloudpath);
            objidasLsaReportSummary.lspath = objcmnstorage.EncryptData(objidasLsaReportSummary.lspath);
        }

        public bool DaGetcustomer2sanction(string customer_gid, idasTrnLsaReportSummary values)
        {
            msSQL = " select sanction_refno,customer2sanction_gid from ocs_mst_tcustomer2sanction where customer_gid='" + customer_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<customersanction_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                getcustomer.Add(new customersanction_list
                {
                    sanctionref_no = "All",
                    customer2sanction_gid = "All"
                });
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getcustomer.Add(new customersanction_list
                    {
                        sanctionref_no = (dr_datarow["sanction_refno"].ToString()),
                        customer2sanction_gid = (dr_datarow["customer2sanction_gid"].ToString()),
                    });
                }
                values.customersanction_list = getcustomer;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public void DaGetColletarlSummary(Mdlsecurity values, string employee_gid)
        {
            msSQL = " select collateral_gid,b.security_type,security_description,account_status,collateralref_no,security_code," +
                  " c.customer_name, c.sanctionref_no, c.lsaref_no" +
                  " from ocs_trn_tcustomercollateral a " +
                  " left join ocs_trn_tsecuritytype b on a.securitytype_gid=b.securitytype_gid " +
                  " left join ids_trn_tlsa c on a.lsacreate_gid = c.lsacreate_gid group by collateralref_no order by a.collateral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<customersecurity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new customersecurity_list
                    {
                        security_type = (dr_datarow["security_type"].ToString()),
                        security_description = (dr_datarow["security_description"].ToString()),
                        account_status = dr_datarow["account_status"].ToString(),
                        collateralref_no = dr_datarow["collateralref_no"].ToString(),
                        security_code = dr_datarow["security_code"].ToString(),
                        collateral_gid = dr_datarow["collateral_gid"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        sanctionref_no = dr_datarow["sanctionref_no"].ToString(),
                        lsaref_no = dr_datarow["lsaref_no"].ToString()
                    });
                }
                values.customersecurity_list = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaColletralReportExcel(Mdlsecurity values)
        {
            msSQL = " select c.customer_name as Customer_Name, c.customer_urn as Customer_URN, c.sanctionref_no as SactionRef_No, c.lsaref_no as LSARef_No, security_code as Security_Code, " +
                    " collateralref_no as CollateralRef_No, b.security_type as Security_Type, security_description as Security_Description, account_status as Security_Status," +
                    " concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as Created_By, date_format(a.created_date, '%d-%m-%Y %h:%i:%s %p') as Created_Date," +
                    " concat(g.user_firstname,' ',g.user_lastname,' / ',g.user_code) as Updated_By, date_format(a.updated_date, '%d-%m-%Y %h:%i:%s %p') as Updated_Date" +
                    " from ocs_trn_tcustomercollateral a " +
                    " left join ocs_trn_tsecuritytype b on a.securitytype_gid=b.securitytype_gid " +
                    " left join ids_trn_tlsa c on a.lsacreate_gid = c.lsacreate_gid "+
                    " left join hrm_mst_temployee d on a.created_by = d.employee_gid" +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid" +
                    " left join hrm_mst_temployee f on a.updated_by = f.employee_gid" +
                    " left join adm_mst_tuser g on g.user_gid = f.user_gid" +
                    " group by collateralref_no order by a.collateral_gid desc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Colletral Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/ColletralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Colletral_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/ColletralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "IDAS/ColletralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 13])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/ColletralReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                ms.Close();

                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
                values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
                values.lspath = objcmnstorage.EncryptData(values.lspath);
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
    }
}