using ems.osd.Models;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using ems.storage.Functions;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.osd.DataAccess
{
    public class DaOsdRptAllTickets
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetDocumentGid, msGet_ccmemberGid, msGet_RefNo, mspSQL, Query;
        string lspath;
        int mnResult;

        public void DaGetAllTicketsSummary(servicerequestdtllist values, string employee_gid)
        {
            string activitymaster_gid, supportteam_gid, assignedmember_gid, ticket_status, raised_date, department_gid;

            activitymaster_gid = values.activitymaster_gid;
            supportteam_gid = values.supportteam_gid;
            assignedmember_gid = values.assignedmember_gid;
            ticket_status = values.ticket_status;
            raised_date = values.raised_date;
            department_gid = values.department_gid;

            msSQL = "call osd_rpt_spallticketssummary(" + "'" + values.department_gid + "'" + "," + "'" + employee_gid + "'" + "," + "'" + values.activitymaster_gid + "'" + "," + "'" + values.supportteam_gid + "'" + "," + "'" + values.assignedmember_gid + "'" + "," + "'" + values.ticket_status + "'" + "," + "'" + values.raised_date + "'" + ")";

        
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
                        bankalert_flag = dt["bankalert_flag"].ToString(),
                        //bankalert2allocated_gid = dt["bankalert2allocated_gid"].ToString(),
                        //customer_gid = dt["customer_gid"].ToString(),
                        department_name = dt["departmentname"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.servicerequestdtl = getservicerequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostAllTicketsSummarySearch(servicerequestdtllist values, string employee_gid)
        {
            string activitymaster_gid, supportteam_gid, assignedmember_gid, ticket_status, raised_date, department_gid, ticket_source;

            activitymaster_gid = values.activitymaster_gid;
            supportteam_gid = values.supportteam_gid;
            assignedmember_gid = values.assignedmember_gid;
            ticket_status = values.ticket_status;
            raised_date = values.raised_date;
            department_gid = values.department_gid;
            ticket_source = values.source;

            msSQL = "call osd_rpt_spallticketssummarysearch(" + "'" + values.department_gid + "'" + "," + "'" + employee_gid + "'" + "," + "'" + values.activitymaster_gid + "'" + "," + "'" + values.supportteam_gid + "'" + "," + "'" + values.assignedmember_gid + "'" + "," + "'" + values.ticket_status + "'" + "," + "'" + values.raised_date + "'" + "," + "'" + values.source + "'" + ")";

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
                        raised_by = dt["raisedby"].ToString(),
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
                        department_name = dt["departmentname"].ToString(),
                        source = dt["source"].ToString()
                    });
                    values.servicerequestdtl = getservicerequestList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaTicketExport(servicerequestdtllist values, string employee_gid)
        {         

            string activitymaster_gid, supportteam_gid, assignedmember_gid, ticket_status, raised_date, department_gid, ticket_source;
            
            activitymaster_gid = values.activitymaster_gid;
            supportteam_gid = values.supportteam_gid;
            assignedmember_gid = values.assignedmember_gid;
            ticket_status = values.ticket_status;
            raised_date = values.raised_date;
            department_gid= values.department_gid;
            ticket_source = values.source;

            msSQL = "call osd_rpt_spallticketsreport(" + "'" + values.department_gid + "'" + "," + "'" + employee_gid + "'" + "," + "'" + values.activitymaster_gid + "'" + "," + "'" + values.supportteam_gid + "'" + "," + "'" + values.assignedmember_gid + "'" + "," + "'" + values.ticket_status + "'" + "," + "'" + values.raised_date + "'" + "," + "'" + values.source + "'" + ")";


            dt_datatable = objdbconn.GetDataTable(msSQL);

            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            var workSheet = excel.Workbook.Worksheets.Add("Ticket Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
               
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "Ticket_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath = lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;

                workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 41])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument",lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname, ms);
                values.lspath = objcmnstorage.EncryptData(ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);
                values.lscloudpath = objcmnstorage.EncryptData(lscompany_code + "/" + "OSD/AllTicket/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname);

                ms.Close();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }
        }
    }
}