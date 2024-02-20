using ems.osd.Models;
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
using System.Linq;
using ems.storage.Functions;

namespace ems.osd.DataAccess
{
    public class DaOsdTrnBankAlertReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable; 
        string msSQL;


        public void DaGetBankReportSummary(OsdTrnBankAlertReport objBankReportSummary)
        {

            msSQL = "call osd_rpt_spbankalertreportsummary";
       
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetBankReportSummary = new List<BankReportSummaryList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    objGetBankReportSummary.Add(new BankReportSummaryList
                    {
                        department_name = dr_datarow["department_name"].ToString(),
                        ticketref_no = dr_datarow["ticketref_no"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                        email_subject = dr_datarow["email_subject"].ToString(),
                        customer_urn = dr_datarow["customer_urn"].ToString(),
                        customer_name = dr_datarow["customer_name"].ToString(),                       
                        assigned_toname = dr_datarow["assigned_toname"].ToString(),
                        assigned_date = dr_datarow["assigned_date"].ToString(),
                        operationstatus_updated_date = dr_datarow["operationstatus_updated_date"].ToString(),
                        //source = dr_datarow["source"].ToString(),

                    });
                }
                objBankReportSummary.BankReportSummaryList = objGetBankReportSummary;
            }
            dt_datatable.Dispose();
            objBankReportSummary.status = true;
            objBankReportSummary.message = "Success";
        }

        // export excel CC

        public void DaGetExportBankReport(OsdTrnBankAlertReport objExportBankReport)
        {
            msSQL = "call osd_rpt_spbankalertreport";
            
            dt_datatable = objdbconn.GetDataTable(msSQL);
            dt_datatable.Columns.Remove("bankalert2allocated_gid");

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);

            var workSheet = excel.Workbook.Worksheets.Add("BankAlert_Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objExportBankReport.lsname = "BankAlert_Report.xlsx";
                //var path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //objExportBankReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname;
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objExportBankReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname;
                objExportBankReport.lscloudpath = lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname;
                bool exists = System.IO.Directory.Exists(path); 
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objExportBankReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 25])  //Address "A1:A19"

                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);

                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname, ms);
                objExportBankReport.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname);
                objExportBankReport.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "Osd/BankAlertReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objExportBankReport.lsname);

                ms.Close();
            }
            catch (Exception ex)
            {
                objExportBankReport.status = false;
                objExportBankReport.message = "Failure";
            }
            objExportBankReport.status = true;
            objExportBankReport.message = "Success";
        }
    }
}