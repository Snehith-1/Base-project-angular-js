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
    public class DaCourierReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL;
        // GET: DaIdasTrnCourierReport
        public void DaCourierReportSummary(CourierReportSummary objCourierReportSummary)
        {
            msSQL = " SELECT a.courierref_no,date_format(a.date_of_courier,'%d-%m-%Y') as date_of_courier," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,a.courier_type," +
                    " concat(b.user_code,' ',b.user_firstname,' ',b.user_lastname) as created_by," +
                    " a.customer_gid, a.customer_name,a.sender_name,a.couriercompany_name,a.courierhandover_to,a.ack_status " +
                    " FROM ids_trn_tcouriermgnt a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetCourierReport = new List<CourierReportSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    objGetCourierReport.Add(new CourierReportSummaryDtls
                    {

                        courierref_no = dt["courierref_no"].ToString(),
                        date_of_courier = dt["date_of_courier"].ToString(),
                        courier_type = dt["courier_type"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        sender_name = dt["sender_name"].ToString(),
                        couriercompany_name = dt["couriercompany_name"].ToString(),
                        courierhandover_to = dt["courierhandover_to"].ToString(),
                        ack_status = dt["ack_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()
                    });
                }
                objCourierReportSummary.CourierReportSummaryDtls = objGetCourierReport;
            }
            dt_datatable.Dispose();
            objCourierReportSummary.status = true;
            objCourierReportSummary.message = "Success";
        }

        public void DaReportSearch(string courier_type, string customer_name, CourierReportSummary objCourierReport)
        {
            msSQL = " SELECT a.courierref_no,date_format(a.date_of_courier,'%d-%m-%Y') as date_of_courier," +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date,a.courier_type," +
                    " concat(b.user_code,' ',b.user_firstname,' ',b.user_lastname) as created_by," +
                    " a.customer_gid,  a.customer_name,a.sender_name,a.couriercompany_name,a.courierhandover_to,a.ack_status " +
                    " FROM ids_trn_tcouriermgnt a" +
                    " left join adm_mst_tuser b on a.created_by=b.user_gid" +
                    " where ";
            if (courier_type == null || courier_type == "")
            {
                msSQL += " 1=1 ";
            }
            else
            {
                msSQL += " a.courier_type = '" + courier_type + "'";
            }
            if (customer_name == null || customer_name == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.customer_name = '" + customer_name + "'";
            }
            msSQL += " order by a.created_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var objGetCourierReport = new List<CourierReportSummaryDtls>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    objGetCourierReport.Add(new CourierReportSummaryDtls
                    {
                        courierref_no = dt["courierref_no"].ToString(),
                        date_of_courier = dt["date_of_courier"].ToString(),
                        courier_type = dt["courier_type"].ToString(),
                        customer_gid = dt["customer_gid"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        sender_name = dt["sender_name"].ToString(),
                        couriercompany_name = dt["couriercompany_name"].ToString(),
                        courierhandover_to = dt["courierhandover_to"].ToString(),
                        ack_status = dt["ack_status"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString()
                        
                    });
                }
                objCourierReport.CourierReportSummaryDtls = objGetCourierReport;
            }
            dt_datatable.Dispose();
            objCourierReport.status = true;
            objCourierReport.message = "Success";
        }
        public void DaExportReport(string courier_type, string customer_name, CourierReportSummary objCourierReport)
        {
            msSQL = "SELECT  courierref_no as `Courier Ref No`, customer_name as ` Customer Name`,sanctionref_no as `Sanction Ref No`,courier_type as `Courier Type`," +
                    " date_format(date_of_courier, '%d-%m-%Y') as `Courier Date`, document_type as `Document Type`," +
                    " if (courier_type = 'Physical Inward',sender_name,'---') as `Document Handover By `," +
                    " if (courier_type = 'Physical Outward',sender_name,'---') as `Document Handover By `," +
                    " if (courier_type = 'Courier Inward',sender_name,'---') as `Courier Despatched By `," +
                    " if (courier_type = 'Courier Outward',sender_name,'---') as `Courier Initiated By `," +
                    " if (courier_type = 'Physical Inward',courierhandover_to,'---') as `Document Handover To `," +
                    " if (courier_type = 'Physical Outward',courierhandover_to,'---') as `Document Handover To`," +
                    " if (courier_type = 'Courier Inward',courierhandover_to,'---') as `Courier Handover to `," +
                    " if (courier_type = 'Courier Outward',courierhandover_to,'---') as `Courier Addressed To`," +
                    " replace(replace(address, '-', ''), '" + "', '') as `Address`," +
                    " couriercompany_name as `Courier Company Name`, pod_no as `POD No`, ack_status as `Acknowledgement Status`," +
                    " ackby_name as `Acknowledgement Name`," +
                    " date_format(ack_date,'%d-%m-%Y %h:%i %p') as `Acknowledgement Date`," +
                    " date_format(a.created_date, '%d-%m-%Y') as `Mail Sent Sate`," +
                    " CONCAT(b.user_code, ' / ', b.user_firstname, b.user_lastname) as `Mail Sent By` ," +
                    " replace(replace(remarks, '-', ''), '" + "', '') as `Remarks` FROM ids_trn_tcouriermgnt a" +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid " +
                    " where ";
            if (courier_type == null || courier_type == "")
            {
                msSQL += " 1=1 ";
            }
            else
            {
                msSQL += " a.courier_type = '" + courier_type + "'";
            }
            if (customer_name == null || customer_name == "")
            {
                msSQL += " and 1=1 ";
            }
            else
            {
                msSQL += " and a.customer_name = '" + customer_name + "'";
            }


            dt_datatable = objdbconn.GetDataTable(msSQL);
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Courier Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                objCourierReport.lsname = "Courier Report.xlsx";
                var path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/Document/CourierReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                objCourierReport.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "IDAS/Document/CourierReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objCourierReport.lsname;
                objCourierReport.lscloudpath = lscompany_code + "/" + "IDAS/Document/CourierReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objCourierReport.lsname + ".xlsx";
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(objCourierReport.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 23])  //Address "A1:A23"
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/Document/CourierReport/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + objCourierReport.lsname + ".xlsx", ms);
                ms.Close();
         
            }
            catch (Exception ex)
            {
                objCourierReport.status = false;
                objCourierReport.message = "Failure";
            }
            objCourierReport.lspath = objcmnstorage.EncryptData(objCourierReport.lspath);
            objCourierReport.lscloudpath = objcmnstorage.EncryptData(objCourierReport.lscloudpath);
            objCourierReport.status = true;
            objCourierReport.message = "Success";
        }
        
    }
}